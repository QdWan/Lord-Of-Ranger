using System;
using System.Windows.Forms;
using LordOfRanger.Keyboard;

namespace LordOfRanger.Options {
	internal partial class OptionsForm : Form {

		private const string FILE_NAME = "./op.cnf";

		private byte _tmpHotKey;

		internal OptionsForm() {
			InitializeComponent();
		}
		#region Event

		private void btnOk_Click(object sender, EventArgs e) {
			SaveOptions();
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void btnApply_Click(object sender, EventArgs e) {
			SaveOptions();
		}

		private void chkSkillIconEnable_CheckedChanged(object sender, EventArgs e) {
			this.panelSkillIcon.Enabled = this.chkSkillIconEnable.Checked;
		}

		private void chkOtherActiveWindowMonitoringEnable_CheckedChanged(object sender, EventArgs e) {
			this.panelOtherActiveWindowMonitoring.Enabled = this.chkOtherActiveWindowMonitoringEnable.Checked;
		}

		private void txtOtherHotKeyLORSwitching_KeyUp(object sender, KeyEventArgs e) {
			if( (byte)e.KeyCode == (byte)Keys.Escape ) {
				this._tmpHotKey = 0x00;
				this.txtOtherHotKeyLORSwitching.Text = Key.KEY_TEXT[this._tmpHotKey];
				return;
			}
			this._tmpHotKey = (byte)e.KeyCode;
			this.txtOtherHotKeyLORSwitching.Text = Key.KEY_TEXT[this._tmpHotKey];
		}

		private void OptionsForm_Load(object sender, EventArgs e) {
			LoadOptions();
		}
		#endregion

		internal static void LoadCnf() {
			if( Options.options == null ) {
				Options.options = new Options();
			}
			if( System.IO.File.Exists( FILE_NAME ) ) {
				var serializer2 = new System.Xml.Serialization.XmlSerializer( typeof( Options ) );
				var sr = new System.IO.StreamReader( FILE_NAME, new System.Text.UTF8Encoding( false ) );
				try {
					Options.options = (Options)serializer2.Deserialize( sr );
				} catch( InvalidOperationException ) {
					MessageBox.Show("op.cnfが壊れていて設定を読み込めませんでした。初期設定で起動します。");
				} finally {
					sr.Close();
				}
			}
		}

		internal static void SaveCnf() {
			var serializer1 = new System.Xml.Serialization.XmlSerializer( typeof( Options ) );
			var sw = new System.IO.StreamWriter( FILE_NAME, false, new System.Text.UTF8Encoding( false ) );
			serializer1.Serialize( sw, Options.options );
			sw.Close();
		}

		private void LoadOptions() {
			LoadCnf();

			this.cmbGeneralStartupState.SelectedIndex = Options.options.startupState;
			this.txtGeneralProcessName.Text = Options.options.processName;

			/* barrage/toggle */
			this.nudIntervalToggleUpDown.Value = Options.options.upDownInterval;
			this.nudIntervalToggleTimer.Value = Options.options.timerInterval;

			/* command */
			this.nudIntervalCommandKeys.Value = Options.options.commandInterval;
			this.nudIntervalCommandUpDown.Value = Options.options.commandUpDownInterval;

			/* mouse */
			this.cmbMouseReClick.SelectedIndex = Options.options.mouseReClick;

			/* icon */
			this.chkSkillIconEnable.Checked = Options.options.iconViewFlag;
			this.nudOneRowIcons.Value = Options.options.oneRowIcons;
			this.cmbSkillIconDisplayPosition.SelectedIndex = Options.options.iconDisplayPosition;

			/* active window */
			this.chkOtherActiveWindowMonitoringEnable.Checked = Options.options.activeWindowMonitoring;
			this.nudOtherActiveWindowMonitoringTimerInterval.Value = Options.options.activeWindowMonitoringinterval;

			/* Hot Key */
			this._tmpHotKey = Options.options.hotKeyLorSwitching;
			this.txtOtherHotKeyLORSwitching.Text = Key.KEY_TEXT[this._tmpHotKey];

			this.panelSkillIcon.Enabled = this.chkSkillIconEnable.Checked;
			this.panelOtherActiveWindowMonitoring.Enabled = this.chkOtherActiveWindowMonitoringEnable.Checked;

		}

		private void SaveOptions() {

			Options.options.startupState = this.cmbGeneralStartupState.SelectedIndex;
			Options.options.processName = this.txtGeneralProcessName.Text;

			/* barrage/toggle */
			Options.options.upDownInterval = (int)this.nudIntervalToggleUpDown.Value;
			Options.options.timerInterval = (int)this.nudIntervalToggleTimer.Value;

			/* command */
			Options.options.commandInterval = (int)this.nudIntervalCommandKeys.Value;
			Options.options.commandUpDownInterval = (int)this.nudIntervalCommandUpDown.Value;

			/* mouse */
			Options.options.mouseReClick = this.cmbMouseReClick.SelectedIndex;

			/* icon */
			Options.options.iconViewFlag = this.chkSkillIconEnable.Checked;
			Options.options.oneRowIcons = (int)this.nudOneRowIcons.Value;
			Options.options.iconDisplayPosition = this.cmbSkillIconDisplayPosition.SelectedIndex;

			/* active window */
			Options.options.activeWindowMonitoring = this.chkOtherActiveWindowMonitoringEnable.Checked;
			Options.options.activeWindowMonitoringinterval = (int)this.nudOtherActiveWindowMonitoringTimerInterval.Value;

			/* Hot Key */
			Options.options.hotKeyLorSwitching = this._tmpHotKey;

			SaveCnf();
			LoadCnf();
		}
	}
}
