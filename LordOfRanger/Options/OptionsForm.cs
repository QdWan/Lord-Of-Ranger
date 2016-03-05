using System;
using System.Windows.Forms;
using LordOfRanger.Keyboard;

namespace LordOfRanger.Options {
	internal partial class OptionsForm : Form {

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

		private void LoadOptions() {

			this.cmbGeneralStartupState.SelectedIndex = Properties.Settings.Default.startupState;
			this.txtGeneralProcessName.Text = Properties.Settings.Default.processName;

			/* barrage/toggle */
			this.nudIntervalToggleUpDown.Value = Properties.Settings.Default.upDownInterval;
			this.nudIntervalToggleTimer.Value = Properties.Settings.Default.timerInterval;

			/* command */
			this.nudIntervalCommandKeys.Value = Properties.Settings.Default.commandInterval;
			this.nudIntervalCommandUpDown.Value = Properties.Settings.Default.commandUpDownInterval;

			/* mouse */
			this.cmbMouseReClick.SelectedIndex = Properties.Settings.Default.mouseReClick;

			/* icon */
			this.chkSkillIconEnable.Checked = Properties.Settings.Default.iconViewFlag;
			this.nudOneRowIcons.Value = Properties.Settings.Default.oneRowIcons;
			this.cmbSkillIconDisplayPosition.SelectedIndex = Properties.Settings.Default.iconDisplayPosition;

			/* active window */
			this.chkOtherActiveWindowMonitoringEnable.Checked = Properties.Settings.Default.activeWindowMonitoring;
			this.nudOtherActiveWindowMonitoringTimerInterval.Value = Properties.Settings.Default.activeWindowMonitoringinterval;

			/* Hot Key */
			this._tmpHotKey = Properties.Settings.Default.hotKeyLorSwitching;
			this.txtOtherHotKeyLORSwitching.Text = Key.KEY_TEXT[this._tmpHotKey];

			this.panelSkillIcon.Enabled = this.chkSkillIconEnable.Checked;
			this.panelOtherActiveWindowMonitoring.Enabled = this.chkOtherActiveWindowMonitoringEnable.Checked;

		}

		private void SaveOptions() {

			Properties.Settings.Default.startupState = this.cmbGeneralStartupState.SelectedIndex;
			Properties.Settings.Default.processName = this.txtGeneralProcessName.Text;

			/* barrage/toggle */
			Properties.Settings.Default.upDownInterval = (int)this.nudIntervalToggleUpDown.Value;
			Properties.Settings.Default.timerInterval = (int)this.nudIntervalToggleTimer.Value;

			/* command */
			Properties.Settings.Default.commandInterval = (int)this.nudIntervalCommandKeys.Value;
			Properties.Settings.Default.commandUpDownInterval = (int)this.nudIntervalCommandUpDown.Value;

			/* mouse */
			Properties.Settings.Default.mouseReClick = this.cmbMouseReClick.SelectedIndex;

			/* icon */
			Properties.Settings.Default.iconViewFlag = this.chkSkillIconEnable.Checked;
			Properties.Settings.Default.oneRowIcons = (int)this.nudOneRowIcons.Value;
			Properties.Settings.Default.iconDisplayPosition = this.cmbSkillIconDisplayPosition.SelectedIndex;

			/* active window */
			Properties.Settings.Default.activeWindowMonitoring = this.chkOtherActiveWindowMonitoringEnable.Checked;
			Properties.Settings.Default.activeWindowMonitoringinterval = (int)this.nudOtherActiveWindowMonitoringTimerInterval.Value;

			/* Hot Key */
			Properties.Settings.Default.hotKeyLorSwitching = this._tmpHotKey;

			Properties.Settings.Default.Save();
		}
	}
}
