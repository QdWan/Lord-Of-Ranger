using System;
using System.Windows.Forms;

namespace LordOfRanger.Options {
	internal partial class OptionsForm : Form {
		private static string fileName = "./op.cnf";

		private byte tmpHotKey = 0x00;

		internal OptionsForm() {
			InitializeComponent();
		}
		#region Event

		private void btnOk_Click(object sender, EventArgs e) {
			saveOptions();
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void btnApply_Click(object sender, EventArgs e) {
			saveOptions();
		}

		private void chkSkillIconEnable_CheckedChanged(object sender, EventArgs e) {
			panelSkillIcon.Enabled = chkSkillIconEnable.Checked;
		}

		private void chkOtherActiveWindowMonitoringEnable_CheckedChanged(object sender, EventArgs e) {
			panelOtherActiveWindowMonitoring.Enabled = chkOtherActiveWindowMonitoringEnable.Checked;
		}

		private void txtOtherHotKeyLORSwitching_KeyUp(object sender, KeyEventArgs e) {
			if( (byte)e.KeyCode == (byte)Keys.Escape ) {
				tmpHotKey = 0x00;
				txtOtherHotKeyLORSwitching.Text = Key.keyText[tmpHotKey];
				return;
			}
			tmpHotKey = (byte)e.KeyCode;
			txtOtherHotKeyLORSwitching.Text = Key.keyText[tmpHotKey];
		}

		private void OptionsForm_Load(object sender, EventArgs e) {
			loadOptions();
		}
		#endregion

		internal static void load() {
			if( Options.options == null ) {
				Options.options = new Options();
			}
			if( System.IO.File.Exists( fileName ) ) {
				System.Xml.Serialization.XmlSerializer serializer2 = new System.Xml.Serialization.XmlSerializer( typeof( Options ) );
				System.IO.StreamReader sr = new System.IO.StreamReader( fileName, new System.Text.UTF8Encoding( false ) );
				Options.options = (Options)serializer2.Deserialize( sr );
				sr.Close();
			}
		}

		internal static void save() {
			System.Xml.Serialization.XmlSerializer serializer1 = new System.Xml.Serialization.XmlSerializer( typeof( Options ) );
			System.IO.StreamWriter sw = new System.IO.StreamWriter( fileName, false, new System.Text.UTF8Encoding( false ) );
			serializer1.Serialize( sw, Options.options );
			sw.Close();
		}

		private void loadOptions() {
			load();

			cmbGeneralStartupState.SelectedIndex = Options.options.startupState;
			txtGeneralProcessName.Text = Options.options.ProcessName;

			/* job */
			nudIntervalCommandKeys.Value = Options.options.commandInterval;
			nudIntervalCommandUpDown.Value = Options.options.commandUpDownInterval;
			nudIntervalToggleUpDown.Value = Options.options.upDownInterval;
			nudIntervalToggleTimer.Value = Options.options.timerInterval;

			/* icon */
			chkSkillIconEnable.Checked = Options.options.iconViewFlag;
			nudOneRowIcons.Value = Options.options.oneRowIcons;
			cmbSkillIconDisplayPosition.SelectedIndex = Options.options.iconDisplayPosition;

			/* active window */
			chkOtherActiveWindowMonitoringEnable.Checked = Options.options.activeWindowMonitoring;
			nudOtherActiveWindowMonitoringTimerInterval.Value = Options.options.activeWindowMonitoringinterval;

			/* Hot Key */
			tmpHotKey = Options.options.hotKeyLORSwitching;
			txtOtherHotKeyLORSwitching.Text = Key.keyText[tmpHotKey];

			/* Advanced */
			chkAdvancedCommandAnotherThread.Checked = Options.options.commandAnotherThread;


			panelSkillIcon.Enabled = chkSkillIconEnable.Checked;
			panelOtherActiveWindowMonitoring.Enabled = chkOtherActiveWindowMonitoringEnable.Checked;
		}

		private void saveOptions() {

			Options.options.startupState = cmbGeneralStartupState.SelectedIndex;
			Options.options.ProcessName = txtGeneralProcessName.Text;

			/* job */
			Options.options.commandInterval = (int)nudIntervalCommandKeys.Value;
			Options.options.commandUpDownInterval = (int)nudIntervalCommandUpDown.Value;
			Options.options.upDownInterval = (int)nudIntervalToggleUpDown.Value;
			Options.options.timerInterval = (int)nudIntervalToggleTimer.Value;

			/* icon */
			Options.options.iconViewFlag = chkSkillIconEnable.Checked;
			Options.options.oneRowIcons = (int)nudOneRowIcons.Value;
			Options.options.iconDisplayPosition = cmbSkillIconDisplayPosition.SelectedIndex;

			/* active window */
			Options.options.activeWindowMonitoring = chkOtherActiveWindowMonitoringEnable.Checked;
			Options.options.activeWindowMonitoringinterval = (int)nudOtherActiveWindowMonitoringTimerInterval.Value;

			/* Hot Key */
			Options.options.hotKeyLORSwitching = tmpHotKey;

			/* Advanced */
			Options.options.commandAnotherThread = chkAdvancedCommandAnotherThread.Checked;

			save();
			load();
		}
	}
}
