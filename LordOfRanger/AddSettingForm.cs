using System;
using System.Windows.Forms;

namespace LordOfRanger {
	internal partial class AddSettingForm : Form {
		internal AddSettingForm() {
			InitializeComponent();

			result = Result.CANCEL;
			KeyPreview = true;
		}
		internal enum Result {
			OK,
			CANCEL
		};

		internal Result result;

		internal enum Type {
			COMMAND,
			BARRAGE,
			TOGGLE
		}
		internal string settingName;

		private void AddSettingForm_KeyUp(object sender, KeyEventArgs e) {
			if( (byte)e.KeyCode == (byte)RamGecTools.KeyboardHook.VKeys.ESCAPE ) {
				Close();
			} else if( (byte)e.KeyCode == (byte)RamGecTools.KeyboardHook.VKeys.RETURN ) {
				settingName = txtSettingName.Text;
				if( System.IO.File.Exists( Setting.Mass.setting_path + settingName + Setting.Mass.EXTENSION ) ) {
					MessageBox.Show( "Please give a unique name." );
					return;
				}
				Setting.Mass mass = new Setting.Mass();
				mass.Name = settingName;
				mass.save();
				result = Result.OK;
				Close();
			}
		}
		private void btnOk_Click(object sender, EventArgs e) {
			settingName = txtSettingName.Text;
			if( System.IO.File.Exists( Setting.Mass.setting_path + settingName + Setting.Mass.EXTENSION ) ) {
				MessageBox.Show( "Please give a unique name." );
				return;
			}
			Setting.Mass mass = new Setting.Mass();
			mass.Name = settingName;
			mass.save();
			result = Result.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			Close();
		}
	}
}
