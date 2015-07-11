﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using HongliangSoft.Utilities.Gui;

namespace LordOfRanger {

	/// <summary>
	/// Setting.Massを読み込み、それを実行するクラス
	/// </summary>
	class Job {
		private static readonly byte[] arrowKeyList = new byte[] { (byte)Keys.Left, (byte)Keys.Right, (byte)Keys.Up, (byte)Keys.Down };
		private static Dictionary<byte, bool> EnablekeyF;
		private static Dictionary<byte, bool> EnablekeyE;
		private Dictionary<int, bool> EnableToggle;
		private static byte directionKey = (byte)Keys.Left;
		private static byte reverseDirectionKey = (byte)Keys.Right;
		private Thread CommandThread;
		private const int iconSize = 30;
		Setting.Mass mass;

		private bool _barrageEnable = true;

		/// <summary>
		/// Massファイルを読み込み、各変数に初期値を代入
		/// </summary>
		/// <param name="mass"></param>
		internal Job(Setting.Mass mass) {
			this.mass = mass;
			EnablekeyF = new Dictionary<byte, bool>();
			EnablekeyE = new Dictionary<byte, bool>();
			for( byte key = 0x00; key <= 0xff; key++ ) {
				EnablekeyF.Add( key, false );
				EnablekeyE.Add( key, false );
				if( key == 0xff ) {
					break;
				}
			}
			EnableToggle = new Dictionary<int, bool>();
			foreach( Setting.Toggle t in mass.toggleList ) {
				EnableToggle.Add( t.id, false );
			}
			iconUpdate();
		}

		/// <summary>
		/// 機能の有効無効を取得、設定する
		/// </summary>
		internal bool barrageEnable {
			get {
				return _barrageEnable;
			}
			set {
				_barrageEnable = value;
				iconUpdate();
			}
		}

		#region event

		/// <summary>
		/// キーアップ時に呼ばれる
		/// 該当するキーの押下フラグをfalseにする
		/// </summary>
		/// <param name="e"> 離されたキー情報 </param>
		internal void keyupEvent(KeyboardHookedEventArgs e) {
			if( e.ExtraInfo != (int)Key.ExtraInfo ) {
				EnablekeyF[(byte)e.KeyCode] = false;
				EnablekeyE[(byte)e.KeyCode] = false;
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
		internal void keydownEvent(KeyboardHookedEventArgs e) {
			byte key = (byte)e.KeyCode;
			if( !MainForm.activeWindow || !_barrageEnable ) {
				return;
			}

			//このキー入力がどこから発行されたものか判定
			if( e.ExtraInfo == (int)Key.ExtraInfo ) {
				//LORから
				if( key == (byte)Keys.Left ) {
					directionKey = (byte)Keys.Left;
					reverseDirectionKey = (byte)Keys.Right;
				} else if( key == (byte)Keys.Right ) {
					directionKey = (byte)Keys.Right;
					reverseDirectionKey = (byte)Keys.Left;
				}
				return;
			} else {
				// キーボードから
				if( CommandThread != null ) {
					if( ( new[] { ThreadState.Running, ThreadState.WaitSleepJoin } ).Contains( CommandThread.ThreadState ) ) {
						bool flag = true;
						foreach( Setting.Barrage b in mass.barrageList ) {
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

			EnablekeyF[key] = true;

			byte left = reverseDirectionKey;
			byte right = directionKey;

			//command
			foreach( Setting.Command c in mass.commandList ) {
				if( commandCheck( key, c.push ) ) {
					e.Cancel = true;
					CommandThread = new Thread( new ParameterizedThreadStart( threadCommand ) );
					CommandThread.Start( new object[] { c.sendList, left, right } );
					break;
				}
			}

			if( key == (byte)Keys.Left ) {
				directionKey = (byte)Keys.Left;
				reverseDirectionKey = (byte)Keys.Right;
			} else if( key == (byte)Keys.Right ) {
				directionKey = (byte)Keys.Right;
				reverseDirectionKey = (byte)Keys.Left;
			}

			//toggle
			foreach( Setting.Toggle t in mass.toggleList ) {
				if( commandCheck( key, t.push ) ) {
					//not typo 
					mass.changeEnable( t.id, t.enable = EnableToggle[t.id] = !EnableToggle[t.id] );
					iconUpdate();
				}
			}
			EnablekeyE[key] = true;
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
		private void threadCommand(object o) {
			object[] obj = (object[])o;
			byte[] sendList = (byte[])obj[0];
			byte left = (byte)obj[1];
			byte right = (byte)obj[2];
			if( Options.Options.options.commandUpArrowKeys ) {
				foreach( byte sendKey in arrowKeyList ) {
					if( EnablekeyE[sendKey] ) {
						Key.up( sendKey );
					}
				}
			}
			foreach( byte sendKey in sendList ) {
				byte _sendKey = sendKey;
				if( _sendKey == (byte)Keys.Right ) {
					_sendKey = right;
				} else if( _sendKey == (byte)Keys.Left ) {
					_sendKey = left;
				}
				keypush( _sendKey, Options.Options.options.commandUpDownInterval );
				sleep( Options.Options.options.commandInterval );
			}
			if( Options.Options.options.commandUpArrowKeys ) {
				foreach( byte sendKey in arrowKeyList ) {
					if( EnablekeyE[sendKey] ) {
						Key.down( sendKey );
					}
				}
			}
		}

		/// <summary>
		/// 一定時間ごとに呼ばれる
		/// ここで連打、トグルの処理を行う
		/// </summary>
		internal void timerEvent() {
			if( !MainForm.activeWindow || !_barrageEnable ) {
				return;
			}
			if( CommandThread != null ) {
				if( ( new[] { ThreadState.Running, ThreadState.WaitSleepJoin } ).Contains( CommandThread.ThreadState ) ) {
					return;
				}
			}
			//barrage
			foreach( Setting.Barrage b in mass.barrageList ) {
				if( EnablekeyE[b.push] ) {
					keypush( b.send );
				}
			}

			//toggle
			try {
				foreach( Setting.Toggle t in mass.toggleList ) {
					if( EnableToggle[t.id] ) {
						keypush( t.send );
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
		private bool commandCheck(byte key, byte key2) {
			/*
				押されたキーが判定するキーと一致しているかどうか
				押しっぱなしの2回目以降ではないか
				2回目以降の場合、EnablekeyEがtrueになっているため、実行するべきではないと判定される。
			*/
			if( key == key2 && !EnablekeyE[key2] && EnablekeyF[key2] ) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// レイヤーウィンドウの更新
		/// </summary>
		internal void iconUpdate() {
			if( !Options.Options.options.iconViewFlag ) {
				MainForm.skillLayer.Visible = false;
				return;
			}
			List<Bitmap> bmpList = new List<Bitmap>();
			foreach( Setting.DataAb da in mass.DataList ) {
				if( da.enable && _barrageEnable && MainForm.activeWindow ) {
					bmpList.Add( da.skillIcon );
				} else {
					bmpList.Add( da.disableSkillIcon );
				}
			}
			Bitmap bmp;
			if( bmpList.Count != 0 ) {
				Bitmap[] iconList = bmpList.ToArray();
				Arad.get();
				bmp = new Bitmap( Math.Min( iconList.Length, Options.Options.options.oneRowIcons ) * iconSize, (int)Math.Ceiling( (double)iconList.Length / Options.Options.options.oneRowIcons ) * iconSize );
				Graphics g = Graphics.FromImage( bmp );
				for( int i = 0; i < iconList.Length; i++ ) {
					if( iconList[i] == null ) {
						continue;
					}
					g.DrawImage( iconList[i], ( i % Options.Options.options.oneRowIcons ) * iconSize, (int)( Math.Floor( (double)( i / Options.Options.options.oneRowIcons ) ) ) * iconSize );
				}
			} else {
				bmp = new Bitmap( 1, 1 );
			}
			MainForm.skillLayer.Visible = true;
			MainForm.skillLayer.Size = new Size( bmp.Width, bmp.Height );
			switch( (Options.Options.ICON_DISPLAY_POSITION)Options.Options.options.iconDisplayPosition ) {
				case Options.Options.ICON_DISPLAY_POSITION.TOP_LEFT:
					MainForm.skillLayer.Top = Arad.y - bmp.Height;
					MainForm.skillLayer.Left = Arad.x;
					break;
				case Options.Options.ICON_DISPLAY_POSITION.TOP_RIGHT:
					MainForm.skillLayer.Top = Arad.y - bmp.Height;
					MainForm.skillLayer.Left = Arad.x + Arad.w - bmp.Width;
					break;
				case Options.Options.ICON_DISPLAY_POSITION.BOTTOM_LEFT:
					MainForm.skillLayer.Top = Arad.y + Arad.h;
					MainForm.skillLayer.Left = Arad.x;
					break;
				case Options.Options.ICON_DISPLAY_POSITION.BOTTOM_RIGHT:
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
			MainForm.skillLayer.toTop();
		}

		private void sleep(int sleeptime) {
			Thread.Sleep( sleeptime );
		}

		/// <summary>
		/// キー送信
		/// </summary>
		/// <param name="key"> 送信するキー </param>
		private void keypush(byte key) {
			Key.down( key );
			sleep( Options.Options.options.upDownInterval );
			Key.up( key );
		}

		/// <summary>
		/// キー送信
		/// </summary>
		/// <param name="key"> 送信するキー </param>
		/// <param name="sl"> キーダウンとキーアップの間の待ち時間(ms) </param>
		private void keypush(byte key, int sl) {
			try {
				Key.down( key );
				sleep( sl );
				Key.up( key );
			} catch( ThreadAbortException e ) {
				Key.up( key );
				throw e;
			}
		}
	}
}