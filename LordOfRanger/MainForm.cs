using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using LordOfRanger.Keyboard;
using LordOfRanger.Setting;



namespace LordOfRanger {
	internal partial class MainForm :Form {

		private static Mass _mass;
		private Job _job;
		internal static bool activeWindow = false;
		private static bool alive = false;
		internal static SkillLayer skillLayer;
		private static string _currentSettingFile;
		private static Dictionary<byte, string> _hotKeys;

		private struct Mode {
			internal const string COMMAND = "Command";
			internal const string BARRAGE = "Barrage";
			internal const string TOGGLE = "Toggle";
		};


		private struct DgvCol {

			internal const string ENABLE_SKILL_ICON = "dgvColSkillIcon";
			internal const string DISABLE_SKILL_ICON = "dgvColDisableSkillIcon";
			internal const string MODE = "dgvColMode";
			internal const string PRIORITY = "dgvColPriority";
			internal const string SEQUENCE = "dgvColSequence";
			internal const string SEND = "dgvColSend";
			internal const string PUSH = "dgvColPush";
			internal const string UP = "dgvColUp";
			internal const string DOWN = "dgvColDown";
			internal const string DELETE = "dgvColDelete";

		}

		/// <summary>
		/// コンストラクタ
		/// コンポーネント初期化のほかに、変数の初期化、設定の読み込み、タイマーのスタート、キーフックのスタートを行う
		/// </summary>
		internal MainForm() {
			InitializeComponent();
			_hotKeys = new Dictionary<byte, string>();
			skillLayer = new SkillLayer();

			_mass = new Mass();
			LoadSettingList();
			CurrentSettingChange( this.lbSettingList.SelectedItem.ToString() );
			SettingUpdate( true );

			this._job = new Job( _mass );

			skillLayer.Show();
			if( Options.Options.options.activeWindowMonitoring ) {
				this.timerActiveWindowCheck.Interval = Options.Options.options.activeWindowMonitoringinterval;
				this.timerActiveWindowCheck.Start();
			}
			this.timerBarrage.Interval = Options.Options.options.timerInterval;
			this.timerBarrage.Start();

			var keyboardHook = new KeyboardHook();
			keyboardHook.KeyboardHooked += KeyHookEvent;

			Application.ApplicationExit += Application_ApplicationExit;
		}


		#region form event

		private void Arad_ClientSizeChanged( object sender, EventArgs e ) {
			try {
				if( WindowState == FormWindowState.Minimized ) {
					Hide();
					this.notifyIcon1.Visible = true;
				}
			} catch( Exception ) {
				// ignored
			}
		}

		private void notifyIcon1_DoubleClick( object sender, EventArgs e ) {
			Visible = true;
			if( WindowState == FormWindowState.Minimized ) {
				WindowState = FormWindowState.Normal;
			}
			Activate();
		}

		private void ExitToolStripMenuItem_Click( object sender, EventArgs e ) {
			skillLayer.Close();
			Application.Exit();
		}

		private void Main_FormClosed( object sender, FormClosedEventArgs e ) {
			skillLayer.Close();
			Application.Exit();
		}

		private void Application_ApplicationExit( object sender, EventArgs e ) {
			Options.OptionsForm.SaveCnf();
		}

		private void optionToolStripMenuItem_Click( object sender, EventArgs e ) {
			Options.OptionsForm.SaveCnf();
			var of = new Options.OptionsForm();
			of.ShowDialog();
		}

		private void skillIconExtractorToolStripMenuItem_Click( object sender, EventArgs e ) {
			var sief = new SkillIconExtractorForm();
			sief.Show();
		}

		private void aboutToolStripMenuItem_Click( object sender, EventArgs e ) {
			var ab = new AboutBox();
			ab.ShowDialog();
		}

		#endregion

		#region setting form

		private void btnAddSetting_Click( object sender, EventArgs e ) {
			var asf = new AddSettingForm();
			asf.ShowDialog();
			if( asf.result == AddSettingForm.Result.OK ) {
				CurrentSettingChange( asf.settingName );
				SettingUpdate();
			}
		}

		private void btnDeleteSetting_Click( object sender, EventArgs e ) {
			var deleteFile = this.lbSettingList.SelectedItem.ToString();
			if( MessageBox.Show( "Are you sure you want to delete setting '" + deleteFile + "'?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 ) == DialogResult.Yes ) {
				File.Delete( Mass.SETTING_PATH + deleteFile + Mass.EXTENSION );
				SettingUpdate();
			}
		}

		private void btnHotKeyChange_Click( object sender, EventArgs e ) {
			var ksf = new KeySetForm();
			ksf.ShowDialog();
			ksf.keyType = KeySetForm.KeyType.SINGLE;
			if( ksf.result == KeySetForm.Result.OK ) {
				_mass.hotKey = ksf.KeyData[0];
				this.txtHotKey.Text = KeysToText( ksf.KeyData );
			}
		}

		private void lbSettingList_MouseDoubleClick( object sender, MouseEventArgs e ) {
			CurrentSettingChange( this.lbSettingList.SelectedItem.ToString() );
			SettingUpdate();
		}

		/// <summary>
		/// 現在読み込まれている設定にそって、データグリッドビューの更新を行う
		/// </summary>
		private void SettingView() {
			this.dgv.Rows.Clear();
			foreach( var da in _mass.DataList ) {
				var row = this.dgv.Rows.Add();
				string mode;
				switch( da.Type ) {
					case DataAb.InstanceType.COMMAND:
						this.dgv.Rows[row].Cells[DgvCol.PUSH].Value = KeysToText( ( (Command)da ).push );
						this.dgv.Rows[row].Cells[DgvCol.SEND].Value = KeysToText( ( (Command)da ).sendList );
						mode = Mode.COMMAND;
						break;
					case DataAb.InstanceType.BARRAGE:
						this.dgv.Rows[row].Cells[DgvCol.PUSH].Value = KeysToText( ( (Barrage)da ).push );
						this.dgv.Rows[row].Cells[DgvCol.SEND].Value = KeysToText( ( (Barrage)da ).send );
						mode = Mode.BARRAGE;
						break;
					case DataAb.InstanceType.TOGGLE:
						this.dgv.Rows[row].Cells[DgvCol.PUSH].Value = KeysToText( ( (Toggle)da ).push );
						this.dgv.Rows[row].Cells[DgvCol.SEND].Value = KeysToText( ( (Toggle)da ).send );
						mode = Mode.TOGGLE;
						break;
					default:
						return;
				}
				this.dgv.Rows[row].Cells[DgvCol.SEQUENCE].Value = da.Id.ToString();
				this.dgv.Rows[row].Cells[DgvCol.MODE].Value = mode;
				this.dgv.Rows[row].Cells[DgvCol.PRIORITY].Value = da.Priority.ToString();
				this.dgv.Rows[row].Cells[DgvCol.ENABLE_SKILL_ICON].Value = da.SkillIcon;
				this.dgv.Rows[row].Cells[DgvCol.DISABLE_SKILL_ICON].Value = da.DisableSkillIcon;
			}
		}

		/// <summary>
		/// 設定のリストの読み込みを行い、1つもなかった場合はnewという設定ファイルを作成する
		/// </summary>
		private void LoadSettingList() {
			if( !Directory.Exists( Mass.SETTING_PATH ) ) {
				Directory.CreateDirectory( Mass.SETTING_PATH );
				Thread.Sleep( 300 );
			}

			var files = Directory.GetFiles( Mass.SETTING_PATH );
			if( files.Length == 0 ) {
				_mass = new Mass();
				CurrentSettingChange( "new" );
				_mass.name = _currentSettingFile;
				_mass.Save();
				LoadSettingList();
				return;
			}
			this.lbSettingList.Items.Clear();
			foreach( var file in files.Where( file => Regex.IsMatch( file, @"\" + Mass.EXTENSION + "$" ) ) ) {
				this.lbSettingList.Items.Add( Path.GetFileNameWithoutExtension( file ) );
			}
			if( this.lbSettingList.FindStringExact( Options.Options.options.currentSettingName ) != ListBox.NoMatches ) {
				this.lbSettingList.SelectedItem = Options.Options.options.currentSettingName;
			} else {
				if( this.lbSettingList.Items.Count > 0 ) {
					this.lbSettingList.SelectedIndex = 0;
				}
			}
		}

		/// <summary>
		/// 全設定ファイルのホットキーの読み込みを行う。
		/// コンストラクタから呼ばれた場合のみ、同一ホットキーが存在する旨の警告を出す。
		/// </summary>
		/// <param name="firstFlag">コンストラクタから呼ばれた場合はtrue</param>
		private void LoadHotKeys( bool firstFlag = false ) {
			_hotKeys.Clear();
			var files = Directory.GetFiles( Mass.SETTING_PATH );
			foreach( var file in files ) {
				if( Regex.IsMatch( file, @"\" + Mass.EXTENSION + "$" ) ) {
					var filename = Path.GetFileNameWithoutExtension( file );
					var hotkey = Mass.GetHotKey( filename );
					if( hotkey != 0x00 ) {
						string file2;
						if( !_hotKeys.TryGetValue( hotkey, out file2 ) ) {
							_hotKeys.Add( hotkey, filename );
						} else {
							if( firstFlag ) {
								MessageBox.Show( "Item with Same HotKey has already been added. \n\n'" + filename + "' AND '" + file2 + "'" );
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 設定リストの再読み込みを行う。
		/// </summary>
		/// <param name="firstFlag"></param>
		private void SettingUpdate( bool firstFlag = false ) {
			LoadSettingList();
			if( this.lbSettingList.FindStringExact( _currentSettingFile ) == -1 ) {
				CurrentSettingChange( this.lbSettingList.Items[0].ToString() );
			}
			LoadHotKeys( firstFlag );
			_mass.Load( _currentSettingFile );
			SettingView();
			this._job = new Job( _mass );

			this.lblSettingName.Text = _currentSettingFile;
			this.lbSettingList.SelectedItem = _currentSettingFile;
			this.txtHotKey.Text = KeysToText( _mass.hotKey );
		}

		/// <summary>
		/// 設定ファイルの切り替え
		/// </summary>
		/// <param name="name">設定ファイルの名前</param>
		private void CurrentSettingChange( string name ) {
			_currentSettingFile = name;
			Options.Options.options.currentSettingName = name;
		}


		#endregion

		#region job

		/// <summary>
		/// キーフックイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyHookEvent( object sender, KeyboardHookedEventArgs e ) {
			if( e.UpDown == KeyboardUpDown.DOWN ) {
				//キーダウンイベント
				this._job.KeydownEvent( e );
			} else if( e.UpDown == KeyboardUpDown.UP ) {
				//キーアップイベント
				this._job.KeyupEvent( e );

				if( e.ExtraInfo != (int)Key.EXTRA_INFO ) {
					//setting change hot key
					if( _hotKeys.ContainsKey( (byte)e.KeyCode ) ) {
						CurrentSettingChange( _hotKeys[(byte)e.KeyCode] );
						SettingUpdate();
						return;
					}


					if( (byte)e.KeyCode == Options.Options.options.hotKeyLorSwitching ) {
						this._job.BarrageEnable = !this._job.BarrageEnable;
					}
				}
			}
		}
		/// <summary>
		/// タイマーから呼び出され、アラド戦記がアクティブになっているかどうか定期的にチェックする。
		/// </summary>
		private void ActiveWindowCheck() {
			try {
				if( Arad.IsAlive ) {
					if( !alive ) {
						alive = true;
						this._job.IconUpdate();
					}
					if( Arad.IsActiveWindow ) {
						if( !activeWindow ) {
							activeWindow = true;
							this._job.IconUpdate();
						}
					} else {
						if( activeWindow ) {
							activeWindow = false;
							this._job.IconUpdate();
						}
					}
				} else {
					if( alive ) {
						alive = false;
						activeWindow = false;
						this._job.IconUpdate();
					}
				}
			} catch( Exception ) {
				// ignored
			}
		}


		/// <summary>
		/// 定期的に呼ばれる。
		/// Jobのキータイマーイベントを呼び出す
		/// </summary>
		private void KeyPushTimer() {
			this._job.TimerEvent();
		}

		/// <summary>
		/// アクティブウィンドウのチェックを行うため定期的に呼び出される
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerActiveWindowCheck_Tick( object sender, EventArgs e ) {
			ActiveWindowCheck();
		}

		/// <summary>
		/// キータイマーイベントのために定期的に呼び出される。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerBarrage_Tick( object sender, EventArgs e ) {
			KeyPushTimer();
		}

		#endregion

		#region job form

		/// <summary>
		/// データグリッドビューをダブルクリックした際に呼び出される。
		/// 主にキーの設定や、スキルアイコンの設定に使用する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgv_CellDoubleClick( object sender, DataGridViewCellEventArgs e ) {
			if( this.dgv.SelectedCells.Count != 1 ) {
				return;
			}
			var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
			switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
				case DgvCol.PUSH:
				case DgvCol.SEND:
					//textbox
					var ksf = new KeySetForm();
					foreach( var dataAb in _mass.DataList ) {
						if( dataAb.Id == sequence ) {
							switch( dataAb.Type ) {
								case DataAb.InstanceType.COMMAND:
									if( this.dgv.SelectedCells[0].OwningColumn.Name == DgvCol.SEND ) {
										ksf.keyType = KeySetForm.KeyType.MULTI;
									}
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
											case DgvCol.SEND:
												( (Command)( dataAb ) ).sendList = ksf.KeyData;
												break;
											case DgvCol.PUSH:
												( (Command)( dataAb ) ).push = ksf.KeyData[0];
												break;
										}
									}
									break;
								case DataAb.InstanceType.BARRAGE:
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
											case DgvCol.SEND:
												( (Barrage)dataAb ).send = ksf.KeyData[0];
												break;
											case DgvCol.PUSH:
												( (Barrage)( dataAb ) ).push = ksf.KeyData[0];
												break;
										}
									}
									break;
								case DataAb.InstanceType.TOGGLE:
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
											case DgvCol.SEND:
												( (Toggle)dataAb ).send = ksf.KeyData[0];
												break;
											case DgvCol.PUSH:
												( (Toggle)( dataAb ) ).push = ksf.KeyData[0];
												break;
										}
									}
									break;
							}
							break;
						}
					}
					if( ksf.result == KeySetForm.Result.OK && ksf.KeyData.Length != 0 ) {
						this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[this.dgv.SelectedCells[0].OwningColumn.Name].Value = KeysToText( ksf.KeyData );
					}

					ksf.Dispose();
					break;
				case DgvCol.ENABLE_SKILL_ICON:
				case DgvCol.DISABLE_SKILL_ICON:
					var ofd = new OpenFileDialog();
					ofd.Filter = "Image File(*.gif;*.jpg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.bmp;*.wmf;*.png";
					ofd.Title = "Please select skillIcon";
					ofd.InitialDirectory = Application.ExecutablePath;
					ofd.RestoreDirectory = true;
					if( ofd.ShowDialog() == DialogResult.OK ) {
						this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[this.dgv.SelectedCells[0].OwningColumn.Name].Value = new Bitmap( ofd.FileName );
						foreach( var dataAb in _mass.DataList ) {
							if( dataAb.Id == sequence ) {
								switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
									case DgvCol.ENABLE_SKILL_ICON:
										dataAb.SkillIcon = new Bitmap( ofd.FileName );
										break;
									case DgvCol.DISABLE_SKILL_ICON:
										dataAb.DisableSkillIcon = new Bitmap( ofd.FileName );
										break;
								}
							}
						}
					}
					break;
			}
		}

		/// <summary>
		/// データグリッドビューをクリックした際に呼び出される。
		/// ボタンのクリックイベントがないため、このイベントで代用している。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgv_CellContentClick( object sender, DataGridViewCellEventArgs e ) {
			if( this.dgv.SelectedCells.Count != 1 ) {
				return;
			}
			switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
				case DgvCol.DELETE:
					if( MessageBox.Show( "Are you sure you want to delete this row?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 ) == DialogResult.Yes ) {
						var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
						_mass.RemoveAt( sequence );
						this.dgv.Rows.RemoveAt( this.dgv.SelectedCells[0].OwningRow.Index );
					}
					break;
				case DgvCol.UP:
					{
						var rowIndex = this.dgv.SelectedCells[0].OwningRow.Index;
						if( rowIndex >= 1 ) {
							var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
							_mass.UpAt( sequence );
							var row = this.dgv.Rows[rowIndex];
							this.dgv.Rows.RemoveAt( rowIndex );
							this.dgv.Rows.Insert( rowIndex - 1, row );
						}
					}
					break;
				case DgvCol.DOWN:
					{
						var rowIndex = this.dgv.SelectedCells[0].OwningRow.Index;
						if( rowIndex < this.dgv.Rows.Count - 1 ) {
							var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
							_mass.DownAt( sequence );
							var row = this.dgv.Rows[rowIndex];
							this.dgv.Rows.RemoveAt( rowIndex );
							this.dgv.Rows.Insert( rowIndex + 1, row );
						}
					}
					break;
			}
		}

		/// <summary>
		/// 設定の1行追加
		/// インスタンスを生成し、Massに追加する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddRow_Click( object sender, EventArgs e ) {
			var acf = new AddCommandForm();
			acf.ShowDialog();
			if( acf.result == AddCommandForm.Result.OK ) {
				string mode;
				int sequence;
				switch( acf.type ) {
					case AddCommandForm.Type.COMMAND:
						sequence = _mass.Add( new Command() );
						mode = Mode.COMMAND;
						break;
					case AddCommandForm.Type.BARRAGE:
						sequence = _mass.Add( new Barrage() );
						mode = Mode.BARRAGE;
						break;
					case AddCommandForm.Type.TOGGLE:
						sequence = _mass.Add( new Toggle() );
						mode = Mode.TOGGLE;
						break;
					default:
						return;
				}
				var row = this.dgv.Rows.Add();
				this.dgv.Rows[row].Cells[DgvCol.SEQUENCE].Value = sequence.ToString();
				this.dgv.Rows[row].Cells[DgvCol.MODE].Value = mode;
				this.dgv.Rows[row].Cells[DgvCol.PRIORITY].Value = "0";
			}
		}

		/// <summary>
		/// 設定の保存を行う
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click( object sender, EventArgs e ) {
			_mass.Save();
			SettingUpdate();
		}

		/// <summary>
		/// 設定の変更を破棄する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click( object sender, EventArgs e ) {
			_mass.Load( _mass.name );
			SettingUpdate();
		}

		/// <summary>
		/// byteで表されるキーをテキストに変換
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private static string KeysToText( byte key ) {
			return Key.KEY_TEXT[key];
		}

		/// <summary>
		/// byte[]で表されるキーの配列をテキストに変換
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		private static string KeysToText( byte[] keys ) {
			var s = "";
			for( var i = 0; i < keys.Length; i++ ) {
				if( i != 0 ) {
					//	s += " + ";
				}
				s += Key.KEY_TEXT[keys[i]];
			}
			return s;
		}

		#endregion
	}
}
