using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using RamGecTools;

namespace LordOfRanger {
	internal partial class MainForm : Form {

		private static KeyboardHook keyboardHook;
		private static Setting.Mass mass;
		private Job job;
		internal static bool activeWindow = true;
		internal static SkillLayer skillLayer;
		internal static string currentSettingFile;
		internal static Dictionary<byte, string> hotKeys;

		private class MODE {
			internal static string COMMAND = "Command";
			internal static string BARRAGE = "Barrage";
			internal static string TOGGLE = "Toggle";
		};

		/// <summary>
		/// コンストラクタ
		/// コンポーネント初期化のほかに、変数の初期化、設定の読み込み、タイマーのスタート、キーフックのスタートを行う
		/// </summary>
		internal MainForm() {
			InitializeComponent();
			hotKeys = new Dictionary<byte, string>();
			skillLayer = new SkillLayer();

			mass = new Setting.Mass();
			loadSettingList();
			currentSettingChange( lbSettingList.SelectedItem.ToString() );
			settingUpdate( true );

			job = new Job( mass );

			skillLayer.Show();
			if( Options.Options.options.activeWindowMonitoring ) {
				timerActiveWindowCheck.Interval = Options.Options.options.activeWindowMonitoringinterval;
				timerActiveWindowCheck.Start();
			}
			timerBarrage.Interval = Options.Options.options.timerInterval;
			timerBarrage.Start();


			keyboardHook = new KeyboardHook();
			keyboardHook.KeyDown += new KeyboardHook.KeyboardHookCallback( keyboardHook_KeyDown );
			keyboardHook.KeyUp += new KeyboardHook.KeyboardHookCallback( keyboardHook_KeyUp );
			keyboardHook.Install();
			Application.ApplicationExit += new EventHandler( Application_ApplicationExit );
		}

		#region form event

		private void Arad_ClientSizeChanged(object sender, EventArgs e) {
			try {
				if( WindowState == FormWindowState.Minimized ) {
					Hide();
					notifyIcon1.Visible = true;
				}
			} catch( Exception ) {

			}
		}

		private void notifyIcon1_DoubleClick(object sender, EventArgs e) {
			Visible = true;
			if( WindowState == FormWindowState.Minimized ) {
				WindowState = FormWindowState.Normal;
			}
			Activate();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
			skillLayer.Close();
			Application.Exit();
		}

		private void Main_FormClosed(object sender, FormClosedEventArgs e) {
			skillLayer.Close();
			Application.Exit();
		}

		private void Application_ApplicationExit(object sender, EventArgs e) {
			Options.OptionsForm.save();
		}

		private void optionToolStripMenuItem_Click(object sender, EventArgs e) {
			Options.OptionsForm.save();
			Options.OptionsForm of = new Options.OptionsForm();
			of.ShowDialog();
		}

		private void skillIconExtractorToolStripMenuItem_Click(object sender, EventArgs e) {
			SkillIconExtractorForm sief = new SkillIconExtractorForm();
			sief.Show();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			AboutBox ab = new AboutBox();
			ab.ShowDialog();
		}

		#endregion

		#region setting form

		private void btnAddSetting_Click(object sender, EventArgs e) {
			AddSettingForm asf = new AddSettingForm();
			asf.ShowDialog();
			if( asf.result == AddSettingForm.Result.OK ) {
				currentSettingChange( asf.settingName );
				settingUpdate();
			}
		}

		private void btnDeleteSetting_Click(object sender, EventArgs e) {
			string deleteFile = lbSettingList.SelectedItem.ToString();
			if( MessageBox.Show( "Are you sure you want to delete setting '" + deleteFile + "'?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 ) == DialogResult.Yes ) {
				File.Delete( Setting.Mass.setting_path + deleteFile + Setting.Mass.EXTENSION );
				settingUpdate();
			}
		}

		private void btnHotKeyChange_Click(object sender, EventArgs e) {
			KeySetForm ksf = new KeySetForm();
			ksf.ShowDialog();
			ksf.keyType = KeySetForm.KeyType.SINGLE;
			if( ksf.result == KeySetForm.Result.OK ) {
				mass.HotKey = ksf.keyData[0];
				txtHotKey.Text = keysToText( ksf.keyData );
			}
		}

		private void lbSettingList_MouseDoubleClick(object sender, MouseEventArgs e) {
			currentSettingChange( lbSettingList.SelectedItem.ToString() );
			settingUpdate();
		}

		/// <summary>
		/// 現在読み込まれている設定にそって、データグリッドビューの更新を行う
		/// </summary>
		private void settingView() {
			dgv.Rows.Clear();
			foreach( Setting.DataAb da in mass.DataList ) {
				int row = dgv.Rows.Add();
				string mode;
				switch( da.type ) {
					case Setting.DataAb.Type.COMMAND:
						dgv.Rows[row].Cells["push"].Value = keysToText( ( (Setting.Command)da ).push );
						dgv.Rows[row].Cells["send"].Value = keysToText( ( (Setting.Command)da ).sendList );
						mode = MODE.COMMAND;
						break;
					case Setting.DataAb.Type.BARRAGE:
						dgv.Rows[row].Cells["push"].Value = keysToText( ( (Setting.Barrage)da ).push );
						dgv.Rows[row].Cells["send"].Value = keysToText( ( (Setting.Barrage)da ).send );
						mode = MODE.BARRAGE;
						break;
					case Setting.DataAb.Type.TOGGLE:
						dgv.Rows[row].Cells["push"].Value = keysToText( ( (Setting.Toggle)da ).push );
						dgv.Rows[row].Cells["send"].Value = keysToText( ( (Setting.Toggle)da ).send );
						mode = MODE.TOGGLE;
						break;
					default:
						return;
				}
				dgv.Rows[row].Cells["sequence"].Value = da.id.ToString();
				dgv.Rows[row].Cells["mode"].Value = mode;
				dgv.Rows[row].Cells["priority"].Value = da.priority.ToString();
				dgv.Rows[row].Cells["skillIcon"].Value = da.skillIcon;
				dgv.Rows[row].Cells["disableSkillIcon"].Value = da.disableSkillIcon;
			}
		}

		/// <summary>
		/// 設定のリストの読み込みを行い、1つもなかった場合はnewという設定ファイルを作成する
		/// </summary>
		private void loadSettingList() {
			if( !Directory.Exists( Setting.Mass.setting_path ) ) {
				Directory.CreateDirectory( Setting.Mass.setting_path );
				Thread.Sleep( 300 );
			}

			string[] files = Directory.GetFiles( Setting.Mass.setting_path );
			if( files.Length == 0 ) {
				mass = new Setting.Mass();
				currentSettingChange( "new" );
				mass.Name = currentSettingFile;
				mass.save();
				loadSettingList();
				return;
			}
			lbSettingList.Items.Clear();
			foreach( string file in files ) {
				if( Regex.IsMatch( file, @"\" + Setting.Mass.EXTENSION + "$" ) ) {
					lbSettingList.Items.Add( Path.GetFileNameWithoutExtension( file ) );
				}
			}
			if( lbSettingList.FindStringExact( Options.Options.options.currentSettingName ) != ListBox.NoMatches ) {
				lbSettingList.SelectedItem = Options.Options.options.currentSettingName;
			} else {
				if( lbSettingList.Items.Count > 0 ) {
					lbSettingList.SelectedIndex = 0;
				}
			}
		}

		/// <summary>
		/// 全設定ファイルのホットキーの読み込みを行う。
		/// コンストラクタから呼ばれた場合のみ、同一ホットキーが存在する旨の警告を出す。
		/// </summary>
		/// <param name="firstFlag">コンストラクタから呼ばれた場合はtrue</param>
		private void loadHotKeys(bool firstFlag = false) {
			hotKeys.Clear();
			string[] files = Directory.GetFiles( Setting.Mass.setting_path );
			foreach( string file in files ) {
				if( Regex.IsMatch( file, @"\" + Setting.Mass.EXTENSION + "$" ) ) {
					string filename = Path.GetFileNameWithoutExtension( file );
					byte hotkey = Setting.Mass.getHotKey( filename );
					if( hotkey != 0x00 ) {
						string file2;
						if( !hotKeys.TryGetValue( hotkey, out file2 ) ) {
							hotKeys.Add( hotkey, filename );
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
		internal void settingUpdate(bool firstFlag = false) {
			loadSettingList();
			if( lbSettingList.FindStringExact( currentSettingFile ) == -1 ) {
				currentSettingChange( lbSettingList.Items[0].ToString() );
			}
			loadHotKeys( firstFlag );
			mass.load( currentSettingFile );
			settingView();
			job = new Job( mass );

			lblSettingName.Text = currentSettingFile;
			lbSettingList.SelectedItem = currentSettingFile;
			txtHotKey.Text = keysToText( mass.HotKey );
		}

		/// <summary>
		/// 設定ファイルの切り替え
		/// </summary>
		/// <param name="name">設定ファイルの名前</param>
		private void currentSettingChange(string name) {
			currentSettingFile = name;
			Options.Options.options.currentSettingName = name;
		}


		#endregion

		#region job

		/// <summary>
		/// タイマーから呼び出され、アラド戦記がアクティブになっているかどうか定期的にチェックする。
		/// </summary>
		private void ActiveWindowCheck() {
			try {
				IntPtr hWnd = API.GetForegroundWindow();
				int id;
				API.GetWindowThreadProcessId( hWnd, out id );
				string name = Process.GetProcessById( id ).ProcessName;
				if( name == Options.Options.options.ProcessName ) {
					if( !activeWindow ) {
						activeWindow = true;
						job.iconUpdate();
					}
				} else {
					if( activeWindow ) {
						activeWindow = false;
						job.iconUpdate();
					}
				}
			} catch( Exception ) {

			}
		}

		/// <summary>
		/// キーアップ時に呼ばれる。
		/// Jobのキーアップイベントの他、ホットキーによる設定切り替えや機能の有効化無効化もここで行う。
		/// </summary>
		/// <param name="key"></param>
		private void keyboardHook_KeyUp(KeyboardHook.VKeys key) {
			job.keyupEvent( (byte)key );

			//setting change hot key
			if( hotKeys.ContainsKey( (byte)key ) ) {
				currentSettingChange( hotKeys[(byte)key] );
				settingUpdate();
				return;
			}


			if( (byte)key == Options.Options.options.hotKeyLORSwitching ) {
				job.barrageEnable = !job.barrageEnable;
				return;
			}
		}

		/// <summary>
		/// キーダウン時に呼ばれる。
		/// </summary>
		/// <param name="key"></param>
		private void keyboardHook_KeyDown(KeyboardHook.VKeys key) {
			job.keydownEvent( (byte)key );
		}

		/// <summary>
		/// 定期的に呼ばれる。
		/// Jobのキータイマーイベントを呼び出す
		/// </summary>
		private void keyPushTimer() {
			job.timerEvent();
		}

		/// <summary>
		/// アクティブウィンドウのチェックを行うため定期的に呼び出される
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerActiveWindowCheck_Tick(object sender, EventArgs e) {
			ActiveWindowCheck();
		}

		/// <summary>
		/// キータイマーイベントのために定期的に呼び出される。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerBarrage_Tick(object sender, EventArgs e) {
			keyPushTimer();
		}

		#endregion

		#region job form

		/// <summary>
		/// データグリッドビューをダブルクリックした際に呼び出される。
		/// 主にキーの設定や、スキルアイコンの設定に使用する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
			if( dgv.SelectedCells.Count != 1 ) {
				return;
			}
			int sequence = int.Parse( (string)dgv.Rows[dgv.SelectedCells[0].OwningRow.Index].Cells["sequence"].Value );
			switch( dgv.SelectedCells[0].OwningColumn.Name ) {
				case "push":
				case "send":
					//textbox
					KeySetForm ksf = new KeySetForm();
					for( int i = 0; i < mass.DataList.Length; i++ ) {
						if( mass.DataList[i].id == sequence ) {
							switch( mass.DataList[i].type ) {
								case Setting.DataAb.Type.COMMAND:
									if( dgv.SelectedCells[0].OwningColumn.Name == "send" ) {
										ksf.keyType = KeySetForm.KeyType.MULTI;
									}
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										switch( dgv.SelectedCells[0].OwningColumn.Name ) {
											case "send":
												( (Setting.Command)( mass.DataList[i] ) ).sendList = ksf.keyData;
												break;
											case "push":
												( (Setting.Command)( mass.DataList[i] ) ).push = ksf.keyData[0];
												break;
										}
									}
									break;
								case Setting.DataAb.Type.BARRAGE:
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										switch( dgv.SelectedCells[0].OwningColumn.Name ) {
											case "send":
												( (Setting.Barrage)mass.DataList[i] ).send = ksf.keyData[0];
												break;
											case "push":
												( (Setting.Barrage)( mass.DataList[i] ) ).push = ksf.keyData[0];
												break;
										}
									}
									break;
								case Setting.DataAb.Type.TOGGLE:
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										switch( dgv.SelectedCells[0].OwningColumn.Name ) {
											case "send":
												( (Setting.Toggle)mass.DataList[i] ).send = ksf.keyData[0];
												break;
											case "push":
												( (Setting.Toggle)( mass.DataList[i] ) ).push = ksf.keyData[0];
												break;
										}
									}
									break;
							}
							break;
						}
					}
					if( ksf.result == KeySetForm.Result.OK && ksf.keyData.Length != 0 ) {
						dgv.Rows[dgv.SelectedCells[0].OwningRow.Index].Cells[dgv.SelectedCells[0].OwningColumn.Name].Value = keysToText( ksf.keyData );
					}

					ksf.Dispose();
					break;
				case "skillIcon":
				case "disableSkillIcon":
					OpenFileDialog ofd = new OpenFileDialog();
					ofd.Filter = "Image File(*.gif;*.jpg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.bmp;*.wmf;*.png";
					ofd.Title = "Please select skillIcon";
					ofd.InitialDirectory = Application.ExecutablePath;
					ofd.RestoreDirectory = true;
					if( ofd.ShowDialog() == DialogResult.OK ) {
						dgv.Rows[dgv.SelectedCells[0].OwningRow.Index].Cells[dgv.SelectedCells[0].OwningColumn.Name].Value = new Bitmap( ofd.FileName );
						for( int i = 0; i < mass.DataList.Length; i++ ) {
							if( mass.DataList[i].id == sequence ) {
								switch( dgv.SelectedCells[0].OwningColumn.Name ) {
									case "skillIcon":
										mass.DataList[i].skillIcon = new Bitmap( ofd.FileName );
										break;
									case "disableSkillIcon":
										mass.DataList[i].disableSkillIcon = new Bitmap( ofd.FileName );
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
		private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e) {
			if( dgv.SelectedCells.Count != 1 ) {
				return;
			}
			switch( dgv.SelectedCells[0].OwningColumn.Name ) {
				case "delete":
					if( MessageBox.Show( "Are you sure you want to delete this row?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 ) == DialogResult.Yes ) {
						int sequence = int.Parse( (string)dgv.Rows[dgv.SelectedCells[0].OwningRow.Index].Cells["sequence"].Value );
						mass.RemoveAt( sequence );
						dgv.Rows.RemoveAt( dgv.SelectedCells[0].OwningRow.Index );
					}
					break;
				case "up":
					{
						int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
						if( rowIndex >= 1 ) {
							int sequence = int.Parse( (string)dgv.Rows[dgv.SelectedCells[0].OwningRow.Index].Cells["sequence"].Value );
							mass.UpAt( sequence );
							DataGridViewRow row = dgv.Rows[rowIndex];
							dgv.Rows.RemoveAt( rowIndex );
							dgv.Rows.Insert( rowIndex - 1, row );
						}
					}
					break;
				case "down":
					{
						int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
						if( rowIndex < dgv.Rows.Count - 1 ) {
							int sequence = int.Parse( (string)dgv.Rows[dgv.SelectedCells[0].OwningRow.Index].Cells["sequence"].Value );
							mass.DownAt( sequence );
							DataGridViewRow row = dgv.Rows[rowIndex];
							dgv.Rows.RemoveAt( rowIndex );
							dgv.Rows.Insert( rowIndex + 1, row );
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
		private void btnAddRow_Click(object sender, EventArgs e) {
			AddCommandForm acf = new AddCommandForm();
			acf.ShowDialog();
			if( acf.result == AddCommandForm.Result.OK ) {
				string mode;
				int sequence;
				switch( acf.type ) {
					case AddCommandForm.Type.COMMAND:
						sequence = mass.Add( new Setting.Command() );
						mode = MODE.COMMAND;
						break;
					case AddCommandForm.Type.BARRAGE:
						sequence = mass.Add( new Setting.Barrage() );
						mode = MODE.BARRAGE;
						break;
					case AddCommandForm.Type.TOGGLE:
						sequence = mass.Add( new Setting.Toggle() );
						mode = MODE.TOGGLE;
						break;
					default:
						return;
				}
				int row = dgv.Rows.Add();
				dgv.Rows[row].Cells["sequence"].Value = sequence.ToString();
				dgv.Rows[row].Cells["mode"].Value = mode;
				dgv.Rows[row].Cells["priority"].Value = "0";
			}
		}

		/// <summary>
		/// 設定の保存を行う
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, EventArgs e) {
			mass.save();
			settingUpdate();
		}

		/// <summary>
		/// 設定の変更を破棄する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e) {
			mass.load( mass.Name );
			settingUpdate();
		}

		/// <summary>
		/// byteで表されるキーをテキストに変換
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private string keysToText(byte key) {
			return Key.keyText[key];
		}

		/// <summary>
		/// byte[]で表されるキーの配列をテキストに変換
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private string keysToText(byte[] keys) {
			string s = "";
			for( int i = 0; i < keys.Length; i++ ) {
				if( i != 0 ) {
					//	s += " + ";
				}
				s += Key.keyText[keys[i]];
			}
			return s;
		}

		#endregion
	}
}
