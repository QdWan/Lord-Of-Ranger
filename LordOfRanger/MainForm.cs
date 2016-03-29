using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using LordOfRanger.Behavior;
using LordOfRanger.Behavior.Action;
using LordOfRanger.Keyboard;
using LordOfRanger.Mouse;

namespace LordOfRanger {
	internal partial class MainForm :Form {

#if DEBUG
		readonly System.Diagnostics.Stopwatch _sw = new System.Diagnostics.Stopwatch();
#endif

		private readonly Common.Logging _logging;
		private Dictionary<string, Mass> _massList;
		private Job _job;
		private Mass CurrentSettingFile {
			get {
				return this._massList[CurrentSettingName];
			}
		}
		private string _currentSettingName;
		private string CurrentSettingName {
			get {
				return this._currentSettingName;
			}
			set {
				this._currentSettingName = value;
				Properties.Settings.Default.currentSettingName = value;
				SettingUpdate();
			}
		}


		private readonly Dictionary<byte, string> _hotKeys;
		private bool _otherWindowOpen;
		private bool _editedFlag;
		private bool EditedFlag {
			get {
				return this._editedFlag;
			}
			set {
				this._editedFlag = value;
				this.btnSave.Enabled = value;
				this.btnCancel.Enabled = value;
			}
		}

		private struct Mode {

			internal const string COMMAND = "コマンド";
			internal const string BARRAGE = "連打";
			internal const string TOGGLE = "連打切替";
			internal const string MOUSE = "マウス操作";

		}

		private struct DgvCol {

			internal const string ENABLE_SKILL_ICON = "dgvColSkillIcon";
			internal const string DISABLE_SKILL_ICON = "dgvColDisableSkillIcon";
			internal const string MODE = "dgvColMode";
			internal const string PRIORITY = "dgvColPriority";
			internal const string SEQUENCE = "dgvColSequence";
			internal const string SEND = "dgvColSend";
			internal const string PUSH = "dgvColPush";
			internal const string KEYBOARD_CANCEL = "dgvColKeyboardCancel";
			internal const string DELETE = "dgvColDelete";

		}

		/// <summary>
		/// コンストラクタ
		/// コンポーネント初期化のほかに、変数の初期化、設定の読み込み、タイマーのスタート、キーフックのスタートを行う
		/// </summary>
		internal MainForm() {
			InitializeComponent();
			this._logging = new Common.Logging( "main.log" );
			this._hotKeys = new Dictionary<byte, string>();

			LoadSettingList();

			if( Properties.Settings.Default.activeWindowMonitoring ) {
				this.timerActiveWindowCheck.Interval = Properties.Settings.Default.activeWindowMonitoringinterval;
				this.timerActiveWindowCheck.Start();
			}

			var keyboardHook = new KeyboardHook();
			keyboardHook.KeyboardHooked += KeyHookEvent;

			Application.ApplicationExit += Application_ApplicationExit;
		}

		/// <summary>
		/// 変更されていた場合、変更を保存するかどうかの確認をする。
		/// trueが帰ってきた場合、呼び出し元のイベントをキャンセルする必要がある。
		/// </summary>
		/// <returns>呼び出し元のイベントをキャンセルする必要があるかどうか</returns>
		private bool ConfirmSettingChange() {
			if( EditedFlag ) {
				var result = MessageBox.Show( "設定ファイルが変更されています。変更を保存しますか。", "変更が保存されていません。", MessageBoxButtons.YesNoCancel );
				// ReSharper disable once SwitchStatementMissingSomeCases
				switch( result ) {
					case DialogResult.Yes:
						EditedFlag = false;
						Manager.Save( CurrentSettingFile );
						break;
					case DialogResult.No:
						EditedFlag = false;
						break;
					case DialogResult.Cancel:
						return true;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			return false;
		}

		#region form event

		private void Arad_ClientSizeChanged( object sender, EventArgs e ) {
			try {
				if( WindowState == FormWindowState.Minimized ) {
					Hide();
					this.notifyIcon1.Visible = true;
				}
			} catch( Exception ex ) {
				this._logging.Write( ex );
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
			Application.Exit();
		}

		private void MainForm_FormClosing( object sender, FormClosingEventArgs e ) {
			if( ConfirmSettingChange() ) {
				e.Cancel = true;
			}
		}

		private void Main_FormClosed( object sender, FormClosedEventArgs e ) {
			Application.Exit();
		}

		private static void Application_ApplicationExit( object sender, EventArgs e ) {
			Properties.Settings.Default.Save();
		}

		private void optionToolStripMenuItem_Click( object sender, EventArgs e ) {
			this._otherWindowOpen = true;
			Properties.Settings.Default.Save();
			var of = new Options.OptionsForm();
			of.ShowDialog();
			this._otherWindowOpen = false;
		}

		private void skillIconExtractorToolStripMenuItem_Click( object sender, EventArgs e ) {
			this._otherWindowOpen = true;
			var sief = new SkillIconExtractorForm();
			sief.Left = Left + ( Width - sief.Width ) / 2;
			sief.Top = Top + ( Height - sief.Height ) / 2;
			sief.ShowDialog();
			this._otherWindowOpen = false;
		}

		private void aboutToolStripMenuItem_Click( object sender, EventArgs e ) {
			this._otherWindowOpen = true;
			var ab = new AboutBox();
			ab.ShowDialog();
			this._otherWindowOpen = false;
		}

		#endregion



		#region setting form

		private void btnAddSetting_Click( object sender, EventArgs e ) {
			if( ConfirmSettingChange() ) {
				return;
			}
			this._otherWindowOpen = true;
			var asf = new AddSettingForm();
			asf.ShowDialog();
			if( asf.result == AddSettingForm.Result.OK ) {
				LoadSettingList();
				CurrentSettingName = asf.settingName;
			}
			this._otherWindowOpen = false;
		}

		private void btnDeleteSetting_Click( object sender, EventArgs e ) {
			var deleteFile = this.lbSettingList.SelectedItem.ToString();
			if( MessageBox.Show( "'" + deleteFile + "'を削除しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 ) == DialogResult.Yes ) {
				File.Delete( Mass.SETTING_PATH + deleteFile + Mass.EXTENSION );
				LoadSettingList();
			}
		}

		private void btnHotKeyChange_Click( object sender, EventArgs e ) {
			this._otherWindowOpen = true;
			var ksf = new KeySetForm();
			ksf.ShowDialog();
			ksf.keyType = KeySetForm.KeyType.SINGLE;
			if( ksf.result == KeySetForm.Result.OK ) {
				CurrentSettingFile.hotKey = ksf.KeyData[0];
				this.txtHotKey.Text = KeysToText( ksf.KeyData );
			}
			this._otherWindowOpen = false;
		}

		private void lbSettingList_MouseDoubleClick( object sender, MouseEventArgs e ) {
			if( ConfirmSettingChange() ) {
				return;
			}
			CurrentSettingName = this.lbSettingList.SelectedItem.ToString();
		}

		/// <summary>
		/// 現在読み込まれている設定にそって、データグリッドビューの更新を行う
		/// </summary>
		private void SettingView() {
			this.dgv.Rows.Clear();
			foreach( var da in CurrentSettingFile.Value ) {
				var row = this.dgv.Rows.Add();
				string mode;
				switch( da.Type ) {
					case Act.InstanceType.COMMAND:
						this.dgv.Rows[row].Cells[DgvCol.PUSH].Value = KeysToText( ( (Command)da ).Push, " + " );
						this.dgv.Rows[row].Cells[DgvCol.SEND].Value = KeysToText( ( (Command)da ).sendList );
						mode = Mode.COMMAND;
						break;
					case Act.InstanceType.BARRAGE:
						this.dgv.Rows[row].Cells[DgvCol.PUSH].Value = KeysToText( ( (Barrage)da ).Push, " + " );
						this.dgv.Rows[row].Cells[DgvCol.SEND].Value = KeysToText( ( (Barrage)da ).send );
						mode = Mode.BARRAGE;
						break;
					case Act.InstanceType.TOGGLE:
						this.dgv.Rows[row].Cells[DgvCol.PUSH].Value = KeysToText( ( (Toggle)da ).Push, " + " );
						this.dgv.Rows[row].Cells[DgvCol.SEND].Value = KeysToText( ( (Toggle)da ).send );
						mode = Mode.TOGGLE;
						break;
					case Act.InstanceType.MOUSE:
						this.dgv.Rows[row].Cells[DgvCol.PUSH].Value = KeysToText( ( (Behavior.Action.Mouse)da ).Push, " + " );
						this.dgv.Rows[row].Cells[DgvCol.SEND].Value = "マウス操作[" + ( (Behavior.Action.Mouse)da ).mouseData.Value.Count + "]";
						mode = Mode.MOUSE;
						break;
					default:
						return;
				}
				this.dgv.Rows[row].Cells[DgvCol.SEQUENCE].Value = da.Id.ToString();
				this.dgv.Rows[row].Cells[DgvCol.MODE].Value = mode;
				this.dgv.Rows[row].Cells[DgvCol.PRIORITY].Value = da.Priority.ToString();
				this.dgv.Rows[row].Cells[DgvCol.ENABLE_SKILL_ICON].Value = da.SkillIcon;
				this.dgv.Rows[row].Cells[DgvCol.DISABLE_SKILL_ICON].Value = da.DisableSkillIcon;
				this.dgv.Rows[row].Cells[DgvCol.KEYBOARD_CANCEL].Value = da.KeyboardCancel;
			}

			this.lblSettingName.Text = CurrentSettingName;
			this.lbSettingList.SelectedItem = CurrentSettingName;
			this.txtHotKey.Text = KeysToText( CurrentSettingFile.hotKey );
			this.cmbSwitchPosition.SelectedIndex = CurrentSettingFile.SwitchPosition;
			EditedFlag = false;
		}

		/// <summary>
		/// 設定のリストの読み込みを行い、1つもなかった場合はnewという設定ファイルを作成する
		/// </summary>
		private void LoadSettingList() {
			while( true ) {
				if( !Directory.Exists( Mass.SETTING_PATH ) ) {
					Directory.CreateDirectory( Mass.SETTING_PATH );
					Thread.Sleep( 300 );
				}

				var files = Directory.GetFiles( Mass.SETTING_PATH );
				if( files.Length == 0 ) {
					var mass = new Mass();
					CurrentSettingName = "new";
					mass.name = CurrentSettingName;
					Manager.Save( mass );
					continue;
				}
				this._massList = new Dictionary<string, Mass>();
				this.lbSettingList.Items.Clear();
				foreach( var filename in files.Where( file => Regex.IsMatch( file, @"\" + Mass.EXTENSION + "$" ) ).Select( Path.GetFileNameWithoutExtension ).Where( filename => filename != null ) ) {
					this._massList.Add( filename, Manager.Load( filename ) );
					this.lbSettingList.Items.Add( filename );
				}
				if( this.lbSettingList.FindStringExact( Properties.Settings.Default.currentSettingName ) != ListBox.NoMatches ) {
					CurrentSettingName = Properties.Settings.Default.currentSettingName;
				} else {
					CurrentSettingName = this.lbSettingList.Items[0].ToString();
				}
				break;
			}
			LoadHotKeys();
		}

		/// <summary>
		/// 全設定ファイルのホットキーの読み込みを行う。
		/// </summary>
		private void LoadHotKeys() {
			this._hotKeys.Clear();
			var files = Directory.GetFiles( Mass.SETTING_PATH );
			foreach( var file in files ) {
				if( Regex.IsMatch( file, @"\" + Mass.EXTENSION + "$" ) ) {
					var filename = Path.GetFileNameWithoutExtension( file );
					var hotkey = Manager.GetHotKey( filename );
					if( hotkey != 0x00 ) {
						string file2;
						if( !this._hotKeys.TryGetValue( hotkey, out file2 ) ) {
							this._hotKeys.Add( hotkey, filename );
						} else {
							MessageBox.Show( "切替ホットキーが同じファイルが複数存在します。 \n\n'" + filename + "' , '" + file2 + "'" );
						}
					}
				}
			}
		}

		/// <summary>
		/// 設定ファイルの再読み込みを行う。
		/// </summary>
		private void SettingUpdate() {
			this._job?.Dispose();
			this._job = new Job( CurrentSettingFile );
			SettingView();
		}

		#endregion



		#region job

		/// <summary>
		/// キーフックイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyHookEvent( object sender, KeyboardHookedEventArgs e ) {
#if DEBUG
			this._sw.Start();
#endif
			if( this._otherWindowOpen ) {
#if DEBUG
				goto echo;
#else
				return;
#endif
			}

			if( this._job.ActiveWindow && this._job.BarrageEnable && e.ExtraInfo != (int)Key.EXTRA_INFO ) {
				if( CurrentSettingFile.CancelList.Contains( (byte)e.KeyCode ) ) {
					e.Cancel = true;
				}
			}
			switch( e.UpDown ) {
				case KeyboardUpDown.DOWN:
					if( this._job.ActiveWindow && this._job.BarrageEnable ) {
						//キーダウンイベント
						Task.Run( () => this._job.KeydownEvent( e ) );
					}
					break;
				case KeyboardUpDown.UP:
					if( this._job.ActiveWindow && this._job.BarrageEnable ) {
						//キーアップイベント
						Task.Run( () => this._job.KeyupEvent( e ) );
					}
					if( e.ExtraInfo != (int)Key.EXTRA_INFO ) {
						//setting change hot key
						if( this._hotKeys.ContainsKey( (byte)e.KeyCode ) ) {
							if( ConfirmSettingChange() ) {
								e.Cancel = true;
							}
							CurrentSettingName = this._hotKeys[(byte)e.KeyCode];
#if DEBUG
						goto echo;
#else
							return;
#endif
						}

						if( (byte)e.KeyCode == Properties.Settings.Default.hotKeyLorSwitching ) {
							this._job.BarrageEnable = !this._job.BarrageEnable;
						}
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
#if DEBUG
			echo:
			this._sw.Stop();
			if( this._sw.Elapsed.TotalMilliseconds > 0.05 ) {
				Console.WriteLine( this._sw.Elapsed.TotalMilliseconds + ":" + KeysToText((byte)e.KeyCode) + "," + e.UpDown);
			}
			this._sw.Reset();
#endif
		}

		/// <summary>
		/// タイマーから呼び出され、アラド戦記がアクティブになっているかどうか定期的にチェックする。
		/// </summary>
		private void ActiveWindowCheck() {
			try {
				this._job.Alive = Arad.Client.IsAlive;
				if( this._job.Alive ) {
					this._job.ActiveWindow = Arad.Client.IsActiveWindow;
				}
			} catch( Exception ex ) {
				this._logging.Write( ex );
			}
		}

		/// <summary>
		/// アクティブウィンドウのチェックを行うため定期的に呼び出される
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerActiveWindowCheck_Tick( object sender, EventArgs e ) {
			ActiveWindowCheck();
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
			this._otherWindowOpen = true;
			var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
			// ReSharper disable once SwitchStatementMissingSomeCases
			switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
				case DgvCol.PUSH: {
						//textbox
						var ksf = new KeySetForm();
						foreach( var act in CurrentSettingFile.Value.Where( act => act.Id == sequence ) ) {
							ksf.keyType = KeySetForm.KeyType.MULTI;
							ksf.ShowDialog();
							if( ksf.result == KeySetForm.Result.OK ) {
								act.Push = ksf.KeyData;
							}
							break;
						}
						if( ksf.result == KeySetForm.Result.OK && ksf.KeyData.Length != 0 ) {
							this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[this.dgv.SelectedCells[0].OwningColumn.Name].Value = KeysToText( ksf.KeyData, " + " );
						}

						ksf.Dispose();
						break;
					}
				case DgvCol.SEND: {
						//textbox
						var ksf = new KeySetForm();
						foreach( var act in CurrentSettingFile.Value.Where( act => act.Id == sequence ) ) {
							switch( act.Type ) {
								case Act.InstanceType.COMMAND:
									ksf.keyType = KeySetForm.KeyType.MULTI;
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										( (Command)( act ) ).sendList = ksf.KeyData;
									}
									break;
								case Act.InstanceType.BARRAGE:
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										( (Barrage)act ).send = ksf.KeyData[0];
									}
									break;
								case Act.InstanceType.TOGGLE:
									ksf.ShowDialog();
									if( ksf.result == KeySetForm.Result.OK ) {
										( (Toggle)act ).send = ksf.KeyData[0];
									}
									break;
								case Act.InstanceType.MOUSE:
									ksf.Dispose();
									var msf = new MouseSetForm( ( (Behavior.Action.Mouse)act ).mouseData );

									msf.ShowDialog();
									if( msf.result == MouseSetForm.Result.OK ) {
										if( msf.editedFlag ) {
											EditedFlag = true;
										}
										( (Behavior.Action.Mouse)act ).mouseData = msf.mouseData;
										this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[this.dgv.SelectedCells[0].OwningColumn.Name].Value = "マウス操作[" + msf.mouseData.Value.Count + "]";
									}
									this._otherWindowOpen = false;
									return;
								default:
									throw new ArgumentOutOfRangeException();
							}
							break;
						}
						if( ksf.result == KeySetForm.Result.OK && ksf.KeyData.Length != 0 ) {
							this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[this.dgv.SelectedCells[0].OwningColumn.Name].Value = KeysToText( ksf.KeyData );
						}

						ksf.Dispose();
						break;
					}
				case DgvCol.ENABLE_SKILL_ICON:
				case DgvCol.DISABLE_SKILL_ICON: {
						var ofd = new OpenFileDialog {
							Filter = "Image File(*.gif;*.jpg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.bmp;*.wmf;*.png",
							Title = "スキルアイコン画像を選択",
							InitialDirectory = Application.ExecutablePath,
							RestoreDirectory = true
						};
						if( ofd.ShowDialog() == DialogResult.OK ) {
							this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[this.dgv.SelectedCells[0].OwningColumn.Name].Value = new Bitmap( ofd.FileName );
							foreach( var act in CurrentSettingFile.Value.Where( act => act.Id == sequence ) ) {
								switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
									case DgvCol.ENABLE_SKILL_ICON:
										act.SkillIcon = new Bitmap( ofd.FileName );
										break;
									case DgvCol.DISABLE_SKILL_ICON:
										act.DisableSkillIcon = new Bitmap( ofd.FileName );
										break;
								}
							}
						}
						break;
					}
			}
			this._otherWindowOpen = false;
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
					if( MessageBox.Show( "この行を削除しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 ) == DialogResult.Yes ) {
						var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
						CurrentSettingFile.RemoveAt( sequence );
						this.dgv.Rows.RemoveAt( this.dgv.SelectedCells[0].OwningRow.Index );
						EditedFlag = true;
					}
					break;
				case DgvCol.KEYBOARD_CANCEL: {
						this.dgv.RefreshEdit();
						break;
					}
				default:
					return;
			}
		}

		/// <summary>
		/// データグリッドビューのセルのクリック時に呼ばれる
		/// チェックボックスが小さくて押しづらいためこのイベントで代用する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgv_CellClick( object sender, DataGridViewCellEventArgs e ) {
			if( this.dgv.SelectedCells.Count != 1 ) {
				return;
			}
			switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
				case DgvCol.KEYBOARD_CANCEL:
					var dgvcbc = (DataGridViewCheckBoxCell)this.dgv.SelectedCells[0];
					dgvcbc.Value = dgvcbc.Value == dgvcbc.TrueValue ? dgvcbc.FalseValue : dgvcbc.TrueValue;
					var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
					CurrentSettingFile.ChangeKeyboardCancel( sequence, (bool)dgvcbc.Value );
					this.dgv.RefreshEdit();
					break;
				default:
					return;
			}
		}

		/// <summary>
		/// セルの変更を検知し、変更フラグをたてる。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgv_CellValueChanged( object sender, DataGridViewCellEventArgs e ) {
			EditedFlag = true;
		}

		/// <summary>
		/// ホットキーの変更を検知し、変更フラグをたてる。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtHotKey_TextChanged( object sender, EventArgs e ) {
			EditedFlag = true;
		}

		/// <summary>
		/// スイッチングシステムの位置を設定する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbSwitchPosition_SelectedIndexChanged( object sender, EventArgs e ) {
			EditedFlag = true;
			CurrentSettingFile.SwitchPosition = this.cmbSwitchPosition.SelectedIndex;
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
						sequence = CurrentSettingFile.Add( new Command() );
						mode = Mode.COMMAND;
						break;
					case AddCommandForm.Type.BARRAGE:
						sequence = CurrentSettingFile.Add( new Barrage() );
						mode = Mode.BARRAGE;
						break;
					case AddCommandForm.Type.TOGGLE:
						sequence = CurrentSettingFile.Add( new Toggle() );
						mode = Mode.TOGGLE;
						break;
					case AddCommandForm.Type.MOUSE:
						sequence = CurrentSettingFile.Add( new Behavior.Action.Mouse() );
						mode = Mode.MOUSE;
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
			EditedFlag = false;
			Manager.Save( CurrentSettingFile );
			SettingUpdate();
		}

		/// <summary>
		/// 設定の変更を破棄する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click( object sender, EventArgs e ) {
			EditedFlag = false;
			this._massList[CurrentSettingName] = Manager.Load( CurrentSettingFile.name );
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
		/// <param name="separator">仕切り文字</param>
		/// <returns></returns>
		private static string KeysToText( IReadOnlyList<byte> keys, string separator = "" ) {
			var s = "";
			for( var i = 0; i < keys.Count; i++ ) {
				if( i != 0 ) {
					s += separator;
				}
				s += Key.KEY_TEXT[keys[i]];
			}
			return s;
		}

		private void btnUpRow_Click( object sender, EventArgs e ) {
			var rowIndex = this.dgv.SelectedCells[0].OwningRow.Index;
			if( rowIndex >= 1 ) {
				var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
				CurrentSettingFile.UpAt( sequence );
				var row = this.dgv.Rows[rowIndex];
				this.dgv.Rows.RemoveAt( rowIndex );
				this.dgv.Rows.Insert( rowIndex - 1, row );
				this.dgv.ClearSelection();
				this.dgv.Rows[rowIndex - 1].Selected = true;
				EditedFlag = true;
			}
		}

		private void btnDownRow_Click( object sender, EventArgs e ) {
			var rowIndex = this.dgv.SelectedCells[0].OwningRow.Index;
			if( rowIndex < this.dgv.Rows.Count - 1 ) {
				var sequence = int.Parse( (string)this.dgv.Rows[this.dgv.SelectedCells[0].OwningRow.Index].Cells[DgvCol.SEQUENCE].Value );
				CurrentSettingFile.DownAt( sequence );
				var row = this.dgv.Rows[rowIndex];
				this.dgv.Rows.RemoveAt( rowIndex );
				this.dgv.Rows.Insert( rowIndex + 1, row );
				this.dgv.ClearSelection();
				this.dgv.Rows[rowIndex + 1].Selected = true;
				EditedFlag = true;
			}
		}

		#endregion
	}
}
