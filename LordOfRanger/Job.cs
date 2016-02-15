using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LordOfRanger.Keyboard;
using LordOfRanger.Mouse;
using LordOfRanger.Setting;

namespace LordOfRanger {

	/// <summary>
	/// Setting.Massを読み込み、それを実行するクラス
	/// </summary>
	class Job {
		private static readonly byte[] ARROW_KEY_LIST = new byte[] { (byte)Keys.Left, (byte)Keys.Right, (byte)Keys.Up, (byte)Keys.Down };
		private static Dictionary<byte, bool> _enablekeyF;
		private static Dictionary<byte, bool> _enablekeyE;
		private Dictionary<int, bool> _enableToggle;
		private static byte _directionKey = (byte)Keys.Left;
		private static byte _reverseDirectionKey = (byte)Keys.Right;
		private Task _commandTask;
		private Task _mouseTask;
		private CancellationTokenSource _mouseTaskCancelToken;

		private const int ICON_SIZE = 30;
		private Mass _mass;

		private bool _barrageEnable = true;

		/// <summary>
		/// Massファイルを読み込み、各変数に初期値を代入
		/// </summary>
		/// <param name="mass"></param>
		internal Job( Mass mass ) {
			this._mass = mass;
			_enablekeyF = new Dictionary<byte, bool>();
			_enablekeyE = new Dictionary<byte, bool>();
			for( byte key = 0x00; key <= 0xff; key++ ) {
				_enablekeyF.Add( key, false );
				_enablekeyE.Add( key, false );
				if( key == 0xff ) {
					break;
				}
			}
			this._enableToggle = new Dictionary<int, bool>();
			foreach( var t in mass.toggleList.Value ) {
				this._enableToggle.Add( t.Id, false );
			}
			IconUpdate();
		}

		/// <summary>
		/// 機能の有効無効を取得、設定する
		/// </summary>
		internal bool BarrageEnable {
			get {
				return this._barrageEnable;
			}
			set {
				this._barrageEnable = value;
				IconUpdate();
			}
		}

		#region event

		/// <summary>
		/// キーアップ時に呼ばれる
		/// 該当するキーの押下フラグをfalseにする
		/// また、マウス操作もキーアップをトリガーとして発動させるようにする
		/// </summary>
		/// <param name="e"> 離されたキー情報 </param>
		internal void KeyupEvent( KeyboardHookedEventArgs e ) {
			if( e.ExtraInfo != (int)Key.EXTRA_INFO ) {
				_enablekeyF[(byte)e.KeyCode] = false;
				_enablekeyE[(byte)e.KeyCode] = false;
				if( !MainForm.activeWindow || !this._barrageEnable ) {
					return;
				}
				foreach( var m in this._mass.mouseList.Value.Where( m => !m.Push.Any( k => !_enablekeyE[k] && k != (byte)e.KeyCode) ) ) {
					if( this._mouseTask?.Status == TaskStatus.Running ) {
						if( Options.Options.options.mouseReClick == 0 ) {
							return;
						} else {
							this._mouseTaskCancelToken.Cancel();
							while( this._mouseTask?.Status == TaskStatus.Running ) {
							}
						}
					}
					this._mouseTaskCancelToken = new CancellationTokenSource();
					this._mouseTask = Task.Run( () => {
						foreach( var send in m.sendList ) {
							if( this._mouseTaskCancelToken.Token.IsCancellationRequested ) {
								throw new TaskCanceledException();
							}
							switch( send.op ) {
								case Set.Operation.LEFT:
									Api.SetCursorPos( Arad.x + send.x, Arad.y + send.y );
									Mouse.Click.Left( Arad.x + send.x, Arad.y + send.y, send.sleepBetween );
									break;
								case Set.Operation.RIGHT:
									Api.SetCursorPos( Arad.x + send.x, Arad.y + send.y );
									Mouse.Click.Right( Arad.x + send.x, Arad.y + send.y, send.sleepBetween );
									break;
								case Set.Operation.MOVE:
									Api.SetCursorPos( Arad.x + send.x, Arad.y + send.y );
									break;
							}
							if( this._mouseTaskCancelToken.Token.IsCancellationRequested ) {
								throw new TaskCanceledException();
							}
							Thread.Sleep( send.sleepAfter );
						}
					}, this._mouseTaskCancelToken.Token );
				}
			} else {

			}
		}

		/// <summary>
		/// キーダウン時に呼ばれる
		/// EnablekeyFは関数のはじめに、
		/// EnablekeyEは関数の終わりにそれぞれtrueに書き換えられる
		/// ここでは
		/// コマンドの呼び出し、
		/// トグルの有効無効の切り替え、
		/// 方向キーの記憶を行う
		/// </summary>
		/// <param name="e"> 押されたキー情報 </param>
		internal async void KeydownEvent( KeyboardHookedEventArgs e ) {
			var key = (byte)e.KeyCode;
			if( !MainForm.activeWindow || !this._barrageEnable ) {
				return;
			}
			//このキー入力がどこから発行されたものか判定
			if( e.ExtraInfo == (int)Key.EXTRA_INFO ) {
				//LORから
				if( key == (byte)Keys.Left ) {
					_directionKey = (byte)Keys.Left;
					_reverseDirectionKey = (byte)Keys.Right;
				} else if( key == (byte)Keys.Right ) {
					_directionKey = (byte)Keys.Right;
					_reverseDirectionKey = (byte)Keys.Left;
				}
				return;
			} else {
				// キーボードから
				if( this._commandTask?.Status == TaskStatus.Running ) {
					if( (byte)Keys.Left <= key && key <= (byte)Keys.Down ) {
						//コマンド中のキー入力で、且つ入力されたキーが方向キーだった場合キャンセル
						e.Cancel = true;
						return;
					}
					if( this._mass.barrageList.Value.Any( b => b.Push.Contains(key) && !b.Push.Any( k => !_enablekeyE[k] ) ) ) {
						//連打キーだった場合コマンドの終了待機
						await this._commandTask;
					}
					if( this._mass.commandList.Value.Any( c => CommandCheck( key, c.Push ) )) {
						//コマンドキーだった場合キャンセル
						e.Cancel = true;
						return;
					}
				}
			}

			_enablekeyF[key] = true;

			var left = _reverseDirectionKey;
			var right = _directionKey;

			//コマンド
			foreach( var c in this._mass.commandList.Value.Where( c => CommandCheck( key, c.Push ) ) ) {
				e.Cancel = true;
				this._commandTask = Task.Run( () => {
					ThreadCommand( new object[] {
						c.sendList, left, right
					} );
				} );
				break;
			}

			switch( key ) {
				case (byte)Keys.Left:
					_directionKey = (byte)Keys.Left;
					_reverseDirectionKey = (byte)Keys.Right;
					break;
				case (byte)Keys.Right:
					_directionKey = (byte)Keys.Right;
					_reverseDirectionKey = (byte)Keys.Left;
					break;
			}

			//切替
			foreach( var t in this._mass.toggleList.Value.Where( t => CommandCheck( key, t.Push ) ) ) {
				//not typo 
				this._mass.ChangeEnable( t.Id, t.Enable = this._enableToggle[t.Id] = !this._enableToggle[t.Id] );
				IconUpdate();
			}
			_enablekeyE[key] = true;
		}

		/// <summary>
		/// コマンドを実行するスレッドから呼び出される関数
		/// また、左右の方向キーについては、右キー、左キーのうちどちらを最後に押したかによって、コマンドで使われるキーが変更される。
		/// </summary>
		/// <param name="o"></param>
		private void ThreadCommand( object o ) {
			var obj = (object[])o;
			var sendList = (byte[])obj[0];
			var left = (byte)obj[1];
			var right = (byte)obj[2];
			foreach( var sendKey in sendList ) {
				var tmpSendKey = sendKey;
				switch( tmpSendKey ) {
					case (byte)Keys.Right:
						tmpSendKey = right;
						break;
					case (byte)Keys.Left:
						tmpSendKey = left;
						break;
				}
				KeyPush( tmpSendKey, Options.Options.options.commandUpDownInterval );
				Sleep( Options.Options.options.commandInterval );
			}
		}

		/// <summary>
		/// 一定時間ごとに呼ばれる
		/// ここで連打、トグルの処理を行う
		/// </summary>
		internal void TimerEvent() {
			if( !MainForm.activeWindow || !this._barrageEnable ) {
				return;
			}
			if( this._commandTask?.Status == TaskStatus.Running ) {
				return;
			}
			//連打
			foreach( var b in this._mass.barrageList.Value.Where( b => !b.Push.Any( k => !_enablekeyE[k] ) ) ) {
				KeyPush( b.send );
			}

			//切替
			try {
				foreach( var t in this._mass.toggleList.Value.Where( t => this._enableToggle[t.Id] ) ) {
					KeyPush( t.send );
				}
			} catch( KeyNotFoundException ) {
				//It happens at the timing of the changeover.
			}
		}

		#endregion

		/// <summary>
		/// コマンドを実行するべきかどうかのチェックを行う
		/// </summary>
		/// <param name="key"> 押下されたキー </param>
		/// <param name="key2"> 判定するキー </param>
		/// <returns> 実行すべきかどうか </returns>
		private bool CommandCheck( byte key, byte key2 ) {
			/*
				押されたキーが判定するキーと一致しているかどうか
				押しっぱなしの2回目以降ではないか
				2回目以降の場合、EnablekeyEがtrueになっているため、実行するべきではないと判定される。
			*/
			return key == key2 && !_enablekeyE[key2] && _enablekeyF[key2];
		}

		/// <summary>
		/// コマンドを実行するべきかどうかのチェックを行う
		/// </summary>
		/// <param name="key"> 押下されたキー </param>
		/// <param name="keyArr"> 判定するキー配列 </param>
		/// <returns> 実行すべきかどうか </returns>
		private bool CommandCheck( byte key, byte[] keyArr ) {
			/*
				押されたキーが判定するキーと一致しているかどうか
				押しっぱなしの2回目以降ではないか
				2回目以降の場合、EnablekeyEがtrueになっているため、実行するべきではないと判定される。
			*/
			if( !keyArr.Contains( key ) ) {
				return false;
			}
			if( keyArr.Any( k => !_enablekeyF[k] ) ) {
				return false;
			}

			return !_enablekeyE[key] && _enablekeyF[key];
		}

		/// <summary>
		/// レイヤーウィンドウの更新
		/// </summary>
		internal void IconUpdate() {
			Bitmap bmp;
			if( !Options.Options.options.iconViewFlag ) {
				bmp = new Bitmap( 1, 1 );
				goto gotoLabelDraw;
			}
			var bmpList = new List<Bitmap>();
			foreach( var da in this._mass.Value ) {
				if( da.Enable && this._barrageEnable) {
					bmpList.Add( da.SkillIcon );
				} else {
					bmpList.Add( da.DisableSkillIcon );
				}
			}
			if( bmpList.Count != 0 ) {
				var iconList = bmpList.ToArray();
				if( !Arad.IsAlive || !MainForm.activeWindow ) {
					bmp = new Bitmap( 1, 1 );
					goto gotoLabelDraw;
				}
				Arad.Get();
				bmp = new Bitmap( Math.Min( iconList.Length, Options.Options.options.oneRowIcons ) * ICON_SIZE, (int)Math.Ceiling( (double)iconList.Length / Options.Options.options.oneRowIcons ) * ICON_SIZE );
				var g = Graphics.FromImage( bmp );
				for( var i = 0; i < iconList.Length; i++ ) {
					if( iconList[i] == null ) {
						continue;
					}
					g.DrawImage( iconList[i], ( i % Options.Options.options.oneRowIcons ) * ICON_SIZE, (int)( Math.Floor( (double)i / Options.Options.options.oneRowIcons ) ) * ICON_SIZE );
				}
			} else {
				bmp = new Bitmap( 1, 1 );
			}
			MainForm.skillLayer.Visible = true;
			MainForm.skillLayer.Size = new Size( bmp.Width, bmp.Height );
			switch( (Options.Options.IconDisplayPosition)Options.Options.options.iconDisplayPosition ) {
				case Options.Options.IconDisplayPosition.TOP_LEFT:
					MainForm.skillLayer.Top = Arad.y - bmp.Height;
					MainForm.skillLayer.Left = Arad.x;
					break;
				case Options.Options.IconDisplayPosition.TOP_RIGHT:
					MainForm.skillLayer.Top = Arad.y - bmp.Height;
					MainForm.skillLayer.Left = Arad.x + Arad.w - bmp.Width;
					break;
				case Options.Options.IconDisplayPosition.BOTTOM_LEFT:
					MainForm.skillLayer.Top = Arad.y + Arad.h;
					MainForm.skillLayer.Left = Arad.x;
					break;
				case Options.Options.IconDisplayPosition.BOTTOM_RIGHT:
					MainForm.skillLayer.Top = Arad.y + Arad.h;
					MainForm.skillLayer.Left = Arad.x + Arad.w - bmp.Width;
					break;
				default:
					MainForm.skillLayer.Top = Arad.y - bmp.Height;
					MainForm.skillLayer.Left = Arad.x;
					break;

			}
			gotoLabelDraw:
			MainForm.skillLayer.DrawImage( bmp );
			MainForm.skillLayer.UpdateLayeredWindow();
			MainForm.skillLayer.ToTop();
		}

		private void Sleep( int sleeptime ) {
			Thread.Sleep( sleeptime );
		}

		/// <summary>
		/// キー送信
		/// </summary>
		/// <param name="key"> 送信するキー </param>
		private void KeyPush( byte key ) {
			Key.Down( key );
			Sleep( Options.Options.options.upDownInterval );
			Key.Up( key );
		}

		/// <summary>
		/// キー送信
		/// </summary>
		/// <param name="key"> 送信するキー </param>
		/// <param name="sl"> キーダウンとキーアップの間の待ち時間(ms) </param>
		private void KeyPush( byte key, int sl ) {
			try {
				Key.Down( key );
				Sleep( sl );
				Key.Up( key );
			} catch( ThreadAbortException ) {
				Key.Up( key );
				throw;
			}
		}
	}
}
