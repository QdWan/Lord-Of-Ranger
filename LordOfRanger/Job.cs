using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LordOfRanger.Keyboard;
using LordOfRanger.Mouse;
using LordOfRanger.Setting;
// ReSharper disable RedundantIfElseBlock

namespace LordOfRanger {

	/// <summary>
	/// Setting.Massを読み込み、それを実行するクラス
	/// </summary>
	class Job {
		private static Dictionary<byte, bool> _enablekeyF;
		private static Dictionary<byte, bool> _enablekeyE;
		private readonly Dictionary<int, bool> _enableToggle;
		private static byte _frontDirection = (byte)Keys.Left;
		private static byte _backDirection = (byte)Keys.Right;
		private Task _commandTask;
		private Task _mouseTask;
		private CancellationTokenSource _mouseTaskCancelToken;
		private static SkillLayer _skillLayer;

		private const int ICON_SIZE = 30;
		private readonly Mass _mass;

		private bool _barrageEnable = true;
		private bool _alive = true;
		internal bool Alive {
			get {
				return this._alive;
			}
			set {
				if( this._alive != value ) {
					this._alive = value;
					if( !value ) {
						ActiveWindow = false;
					}
				}
			}
		}
		private bool _activeWindow = true;
		internal bool ActiveWindow {
			get {
				return this._activeWindow;
			}
			set {
				if( this._activeWindow != value ) {
					this._activeWindow = value;
					IconUpdate();
					if( !value ) {
						KeyAllUp();
					}
				}
			}
		}

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
			foreach( var t in mass.Toggles ) {
				this._enableToggle.Add( t.Id, false );
			}
			if( _skillLayer == null ) {
				_skillLayer = new SkillLayer();
				_skillLayer.Show();
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
				KeyAllUp();
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
				foreach( var m in this._mass.Mice.Where( m => !m.Push.Any( k => !_enablekeyE[k] && k != (byte)e.KeyCode) ) ) {
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
									Click.Left( Arad.x + send.x, Arad.y + send.y, send.sleepBetween );
									break;
								case Set.Operation.RIGHT:
									Api.SetCursorPos( Arad.x + send.x, Arad.y + send.y );
									Click.Right( Arad.x + send.x, Arad.y + send.y, send.sleepBetween );
									break;
								case Set.Operation.MOVE:
									Api.SetCursorPos( Arad.x + send.x, Arad.y + send.y );
									break;
								default:
									throw new ArgumentOutOfRangeException();
							}
							if( this._mouseTaskCancelToken.Token.IsCancellationRequested ) {
								throw new TaskCanceledException();
							}
							Thread.Sleep( send.sleepAfter );
						}
					}, this._mouseTaskCancelToken.Token );
				}
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

			//このキー入力がどこから発行されたものか判定
			if( e.ExtraInfo == (int)Key.EXTRA_INFO ) {
				//LORから
				switch( key ) {
					case (byte)Keys.Left:
						_frontDirection = (byte)Keys.Left;
						_backDirection = (byte)Keys.Right;
						break;
					case (byte)Keys.Right:
						_frontDirection = (byte)Keys.Right;
						_backDirection = (byte)Keys.Left;
						break;
					default:
						return;
				}
			} else {
				// キーボードから
				if( this._commandTask?.Status == TaskStatus.Running ) {
					//コマンド実行中で、
					if( this._mass.Barrages.Any( b => b.Push.Contains( key ) && !b.Push.Any( k => !_enablekeyE[k] ) ) ) {
						//連打キーだった場合コマンドの終了待機
						await this._commandTask;
					}
					if( this._mass.Commands.Any( c => CommandCheck( key, c.Push ) ) ) {
						//コマンドキーだった場合
						return;
					}
				}
			}

			_enablekeyF[key] = true;

			var left = _backDirection;
			var right = _frontDirection;

			//コマンド
			foreach( var c in this._mass.Commands.Where( c => CommandCheck( key, c.Push ) ) ) {
				this._commandTask = Task.Run( () => ThreadCommand( c.sendList, left, right ) );
				break;
			}

			// ReSharper disable once SwitchStatementMissingSomeCases
			switch( key ) {
				case (byte)Keys.Left:
					_frontDirection = (byte)Keys.Left;
					_backDirection = (byte)Keys.Right;
					break;
				case (byte)Keys.Right:
					_frontDirection = (byte)Keys.Right;
					_backDirection = (byte)Keys.Left;
					break;
			}

			//切替
			foreach( var t in this._mass.Toggles.Where( t => CommandCheck( key, t.Push ) ) ) {
				//not typo 
				this._mass.ChangeEnable( t.Id, t.Enable = this._enableToggle[t.Id] = !this._enableToggle[t.Id] );
				DIconUpdate();
			}
			_enablekeyE[key] = true;
		}

		/// <summary>
		/// コマンドを実行するスレッドから呼び出される関数
		/// また、左右の方向キーについては、右キー、左キーのうちどちらを最後に押したかによって、コマンドで使われるキーが変更される。
		/// </summary>
		/// <param name="sendList">送信するキー</param>
		/// <param name="front">キャラクターの向いている方向</param>
		/// <param name="back">キャラクターの背中側の方向</param>
		private static void ThreadCommand( IEnumerable<byte> sendList, byte front, byte back ) {
			foreach( var sendKey in sendList ) {
				var tmpSendKey = sendKey;
				// ReSharper disable once SwitchStatementMissingSomeCases
				switch( tmpSendKey ) {
					case (byte)Keys.Right:
						tmpSendKey = back;
						break;
					case (byte)Keys.Left:
						tmpSendKey = front;
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
			if( !ActiveWindow || !this._barrageEnable ) {
				return;
			}
			if( this._commandTask?.Status == TaskStatus.Running ) {
				return;
			}

			//連打
			foreach( var b in this._mass.Barrages.Where( b => !b.Push.Any( k => !_enablekeyE[k] ) ) ) {
				KeyPush( b.send );
			}

			//切替
			try {
				foreach( var t in this._mass.Toggles.Where( t => this._enableToggle[t.Id] ) ) {
					KeyPush( t.send );
				}
			} catch( KeyNotFoundException ) {

			}
		}

		#endregion



		/// <summary>
		/// コマンドを実行するべきかどうかのチェックを行う
		/// </summary>
		/// <param name="key"> 押下されたキー </param>
		/// <param name="keyArr"> 判定するキー配列 </param>
		/// <returns> 実行すべきかどうか </returns>
		private static bool CommandCheck( byte key, byte[] keyArr ) {
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
		/// 全てのキーを押していない状態にする。
		/// </summary>
		private static void KeyAllUp() {
			for( byte key = 0x00; key <= 0xff; key++ ) {
				_enablekeyF[key] = false;
				_enablekeyE[key] = false;
				if( key == 0xff ) {
					break;
				}
			}
		}

		/// <summary>
		/// レイヤーウィンドウの更新
		/// </summary>
		private void IconUpdate() {
			Bitmap bmp;
			if( !Options.Options.options.iconViewFlag ) {
				bmp = new Bitmap( 1, 1 );
				goto gotoLabelDraw;
			}
			var bmpList = new List<Bitmap>();
			foreach( var da in this._mass.Value ) {
				if( da.Enable && this._barrageEnable ) {
					bmpList.Add( da.SkillIcon );
				} else {
					bmpList.Add( da.DisableSkillIcon );
				}
			}
			if( bmpList.Count != 0 ) {
				var iconList = bmpList.ToArray();
				if( !Arad.IsAlive || !ActiveWindow ) {
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
			_skillLayer.Visible = true;
			_skillLayer.Size = new Size( bmp.Width, bmp.Height );
			switch( (Options.Options.IconDisplayPosition)Options.Options.options.iconDisplayPosition ) {
				case Options.Options.IconDisplayPosition.TOP_LEFT:
					_skillLayer.Top = Arad.y - bmp.Height;
					_skillLayer.Left = Arad.x;
					break;
				case Options.Options.IconDisplayPosition.TOP_RIGHT:
					_skillLayer.Top = Arad.y - bmp.Height;
					_skillLayer.Left = Arad.x + Arad.w - bmp.Width;
					break;
				case Options.Options.IconDisplayPosition.BOTTOM_LEFT:
					_skillLayer.Top = Arad.y + Arad.h;
					_skillLayer.Left = Arad.x;
					break;
				case Options.Options.IconDisplayPosition.BOTTOM_RIGHT:
					_skillLayer.Top = Arad.y + Arad.h;
					_skillLayer.Left = Arad.x + Arad.w - bmp.Width;
					break;
				default:
					_skillLayer.Top = Arad.y - bmp.Height;
					_skillLayer.Left = Arad.x;
					break;
			}
			gotoLabelDraw:
			_skillLayer.DrawImage( bmp );
			_skillLayer.UpdateLayeredWindow();
			_skillLayer.ToTop();
		}

		private void DIconUpdate() {
			Action dlg = IconUpdate;
			dlg();
		}

		private static void Sleep( int sleeptime ) {
			Thread.Sleep( sleeptime );
		}

		/// <summary>
		/// キー送信
		/// </summary>
		/// <param name="key"> 送信するキー </param>
		private static void KeyPush( byte key ) {
			Key.Down( key );
			Sleep( Options.Options.options.upDownInterval );
			Key.Up( key );
		}

		/// <summary>
		/// キー送信
		/// </summary>
		/// <param name="key"> 送信するキー </param>
		/// <param name="sl"> キーダウンとキーアップの間の待ち時間(ms) </param>
		private static void KeyPush( byte key, int sl ) {
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
