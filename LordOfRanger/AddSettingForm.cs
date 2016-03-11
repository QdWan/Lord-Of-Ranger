using System;
using System.Windows.Forms;
using LordOfRanger.Behavior;

namespace LordOfRanger {

	/// <summary>
	/// 設定ファイルの追加フォーム
	/// 設定名を入力できる
	/// </summary>
	internal partial class AddSettingForm :Form {
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

		internal string settingName;

		private void AddSettingForm_KeyUp( object sender, KeyEventArgs e ) {
			switch( (byte)e.KeyCode ) {
				case (byte)Keys.Escape:
					Close();
					break;
				case (byte)Keys.Return:
					this.settingName = this.txtSettingName.Text;
					if( System.IO.File.Exists( Mass.SETTING_PATH + this.settingName + Mass.EXTENSION ) ) {
						MessageBox.Show( "同じ設定名が存在します。" );
						return;
					}
					var mass = new Mass {
						name = this.settingName
					};
					Manager.Save(mass);
					this.result = Result.OK;
					Close();
					break;
				default:
					return;
			}
		}

		private void btnOk_Click( object sender, EventArgs e ) {
			this.settingName = this.txtSettingName.Text;
			if( System.IO.File.Exists( Mass.SETTING_PATH + this.settingName + Mass.EXTENSION ) ) {
				MessageBox.Show( "同じ設定名が存在します。" );
				return;
			}
			var mass = new Mass {
				name = this.settingName
			};
			Manager.Save(mass);
			this.result = Result.OK;
			Close();
		}

		private void btnCancel_Click( object sender, EventArgs e ) {
			Close();
		}
	}
}
