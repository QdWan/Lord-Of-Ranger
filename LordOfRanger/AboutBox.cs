using System;
using System.Windows.Forms;



namespace LordOfRanger {
	/// <summary>
	/// このアプリケーションの情報を表示するフォーム
	/// </summary>
	public partial class AboutBox : Form {
		public AboutBox() {
			InitializeComponent();

			string appVersion = Application.ProductVersion;
			string appProductName = Application.ProductName;

			this.lblInfo.Text = "\n\n" + appProductName + "\n";
			this.lblInfo.Text += "Application Version : " + appVersion;
		}

		private void btnOK_Click(object sender, EventArgs e) {
			Close();
		}
	}
}
