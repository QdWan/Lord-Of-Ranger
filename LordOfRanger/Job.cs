using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;



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
		private Thread _commandThread;
		private const int ICON_SIZE = 30;
		Setting.Mass _mass;

		private bool _barrageEnable = true;

		/// <summary>
		/// Massファイルを読み込み、各変数に初期値を代入
		/// </summary>
		/// <param name="mass"></param>
		internal Job(Setting.Mass mass) {
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
			foreach( Setting.Toggle t in mass.ToggleList ) {
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
		/// </summary>
		/// <param name="e"> 離されたキー情報 </param>
		internal void KeyupEvent(KeyboardHookedEventArgs e) {
			if( e.ExtraInfo != (int)Key.EXTRA_INFO ) {
				_enablekeyF[(byte)e.KeyCode] = false;
				_enablekeyE[(byte)e.KeyCode] = false;
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
		internal void KeydownEvent(KeyboardHookedEventArgs e) {
			byte key = (byte)e.KeyCode;
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
				if( this._commandThread != null ) {
					if( ( new[] { ThreadState.Running, ThreadState.WaitSleepJoin } ).Contains( this._commandThread.ThreadState ) ) {
						bool flag = true;
						foreach( Setting.Barrage b in this._mass.BarrageList ) {
							if( b.push == (byte)e.KeyCode ) {
								flag = false;
							}
						}
						if( flag ) {
							//コマンド中のキー入力で、且つ入力されたキーが連打設定されていない場合はキャンセル
							e.Cancel = true;
							return;
						}
					}
				}
			}

			_enablekeyF[key] = true;

			byte left = _reverseDirectionKey;
			byte right = _directionKey;

			//command
			foreach( Setting.Command c in this._mass.CommandList ) {
				if( CommandCheck( key, c.push ) ) {
					e.Cancel = true;
					this._commandThread = new Thread( ThreadCommand );
					this._commandThread.Start( new object[] { c.sendList, left, right } );
					break;
				}
			}

			if( key == (byte)Keys.Left ) {
				_directionKey = (byte)Keys.Left;
				_reverseDirectionKey = (byte)Keys.Right;
			} else if( key == (byte)Keys.Right ) {
				_directionKey = (byte)Keys.Right;
				_reverseDirectionKey = (byte)Keys.Left;
			}

			//toggle
			foreach( Setting.Toggle t in this._mass.ToggleList ) {
				if( CommandCheck( key, t.push ) ) {
					//not typo 
					this._mass.ChangeEnable( t.Id, t.Enable = this._enableToggle[t.Id] = !this._enableToggle[t.Id] );
					IconUpdate();
				}
			}
			_enablekeyE[key] = true;
		}

		/// <summary>
		/// コマンドを実行するスレッドから呼び出される関数
		/// 
		/// --オプション設定が有効の場合は
		/// 始めに押下中の方向キーを放し、
		/// sendList配列の中身のキーを順に押下していく
		/// 終わり次第押下中だった方向キーを押下し直す
		/// 
		/// --オプション設定が無効の場合は
		/// sendListの配列の中身のキーを順に押していく
		/// 
		/// また、左右の方向キーについては、右キー、左キーのうちどちらを最後に押したかによって、コマンドで使われるキーが変更される。
		/// </summary>
		/// <param name="o"></param>
		private void ThreadCommand(object o) {
			object[] obj = (object[])o;
			byte[] sendList = (byte[])obj[0];
			byte left = (byte)obj[1];
			byte right = (byte)obj[2];
			if( Options.Options.options.commandUpArrowKeys ) {
				foreach( byte sendKey in ARROW_KEY_LIST ) {
					if( _enablekeyE[sendKey] ) {
						Key.Up( sendKey );
					}
				}
			}
			foreach( byte sendKey in sendList ) {
				byte tmpSendKey = sendKey;
				if( tmpSendKey == (byte)Keys.Right ) {
					tmpSendKey = right;
				} else if( tmpSendKey == (byte)Keys.Left ) {
					tmpSendKey = left;
				}
				KeyPush( tmpSendKey, Options.Options.options.commandUpDownInterval );
				Sleep( Options.Options.options.commandInterval );
			}
			if( Options.Options.options.commandUpArrowKeys ) {
				foreach( byte sendKey in ARROW_KEY_LIST ) {
					if( _enablekeyE[sendKey] ) {
						Key.Down( sendKey );
					}
				}
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
			if( this._commandThread != null ) {
				if( ( new[] { ThreadState.Running, ThreadState.WaitSleepJoin } ).Contains( this._commandThread.ThreadState ) ) {
					return;
				}
			}
			//barrage
			foreach( Setting.Barrage b in this._mass.BarrageList ) {
				if( _enablekeyE[b.push] ) {
					KeyPush( b.send );
				}
			}

			//toggle
			try {
				foreach( Setting.Toggle t in this._mass.ToggleList ) {
					if( this._enableToggle[t.Id] ) {
						KeyPush( t.send );
					}
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
		private bool CommandCheck(byte key, byte key2) {
			/*
				押されたキーが判定するキーと一致しているかどうか
				押しっぱなしの2回目以降ではないか
				2回目以降の場合、EnablekeyEがtrueになっているため、実行するべきではないと判定される。
			*/
			if( key == key2 && !_enablekeyE[key2] && _enablekeyF[key2] ) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// レイヤーウィンドウの更新
		/// </summary>
		internal void IconUpdate() {
			if( !Options.Options.options.iconViewFlag ) {
				MainForm.skillLayer.Visible = false;
				return;
			}
			List<Bitmap> bmpList = new List<Bitmap>();
			foreach( Setting.DataAb da in this._mass.DataList ) {
				if( da.Enable && this._barrageEnable && MainForm.activeWindow ) {
					bmpList.Add( da.SkillIcon );
				} else {
					bmpList.Add( da.DisableSkillIcon );
				}
			}
			Bitmap bmp;
			if( bmpList.Count != 0 ) {
				Bitmap[] iconList = bmpList.ToArray();
				Arad.Get();
				bmp = new Bitmap( Math.Min( iconList.Length, Options.Options.options.oneRowIcons ) * ICON_SIZE, (int)Math.Ceiling( (double)iconList.Length / Options.Options.options.oneRowIcons ) * ICON_SIZE );
				Graphics g = Graphics.FromImage( bmp );
				for( int i = 0; i < iconList.Length; i++ ) {
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
			MainForm.skillLayer.DrawImage( bmp );
			MainForm.skillLayer.UpdateLayeredWindow();
			MainForm.skillLayer.ToTop();
		}

		private void Sleep(int sleeptime) {
			Thread.Sleep( sleeptime );
		}

		/// <summary>
		/// キー送信
		/// </summary>
		/// <param name="key"> 送信するキー </param>
		private void KeyPush(byte key) {
			Key.Down( key );
			Sleep( Options.Options.options.upDownInterval );
			Key.Up( key );
		}

		/// <summary>
		/// キー送信
		/// </summary>
		/// <param name="key"> 送信するキー </param>
		/// <param name="sl"> キーダウンとキーアップの間の待ち時間(ms) </param>
		private void KeyPush(byte key, int sl) {
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
