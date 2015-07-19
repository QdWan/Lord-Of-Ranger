using System;
using System.Windows.Forms;

namespace LordOfRanger {

	/// <summary>
	/// 設定ファイルの追加フォーム
	/// 設定名を入力できる
	/// </summary>
	internal partial class AddSettingForm : Form {
		internal AddSettingForm() {
			InitializeComponent();

			this.result = Result.CANCEL;
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
			if( (byte)e.KeyCode == (byte)Keys.Escape ) {
				Close();
			} else if( (byte)e.KeyCode == (byte)Keys.Return ) {
				this.settingName = this.txtSettingName.Text;
				if( System.IO.File.Exists( Setting.Mass.SETTING_PATH + this.settingName + Setting.Mass.EXTENSION ) ) {
					MessageBox.Show( "Please give a unique name." );
					return;
				}
				Setting.Mass mass = new Setting.Mass();
				mass.name = this.settingName;
				mass.Save();
				this.result = Result.OK;
				Close();
			}
		}
		private void btnOk_Click(object sender, EventArgs e) {
			this.settingName = this.txtSettingName.Text;
			if( System.IO.File.Exists( Setting.Mass.SETTING_PATH + this.settingName + Setting.Mass.EXTENSION ) ) {
				MessageBox.Show( "Please give a unique name." );
				return;
			}
			Setting.Mass mass = new Setting.Mass();
			mass.name = this.settingName;
			mass.Save();
			this.result = Result.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			Close();
		}
	}
}
