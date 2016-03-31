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

		internal Mass addedMassInstance;

		internal string settingName;

		private void AddSettingForm_KeyDown( object sender, KeyEventArgs e ) {
			switch( (byte)e.KeyCode ) {
				case (byte)Keys.Escape:
					Close();
					break;
				case (byte)Keys.Return:
					AddMass();
					Close();
					break;
				default:
					return;
			}
		}

		private void btnOk_Click( object sender, EventArgs e ) {
			AddMass();
			Close();
		}

		private void btnCancel_Click( object sender, EventArgs e ) {
			Close();
		}

		private void AddMass() {
			this.settingName = this.txtSettingName.Text;
			if( System.IO.File.Exists( Mass.SETTING_PATH + this.settingName + Mass.EXTENSION ) ) {
				MessageBox.Show( "同じ設定名が存在します。" );
				return;
			}
			var mass = new Mass {
				name = this.settingName
			};
			Manager.Save( mass );
			this.addedMassInstance = mass;
			this.result = Result.OK;
		}
	}
}
