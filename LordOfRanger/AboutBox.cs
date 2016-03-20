using System;
using System.Windows.Forms;



namespace LordOfRanger {
	/// <summary>
	/// このアプリケーションの情報を表示するフォーム
	/// </summary>
	internal partial class AboutBox :Form {

		internal AboutBox() {
			InitializeComponent();

			var appVersion = Application.ProductVersion;
			var appProductName = Application.ProductName;

			this.lblInfo.Text = "\n\n" + appProductName + "\n";
			this.lblInfo.Text += "アプリケーションバージョン : " + appVersion;

		}

		private void btnOK_Click( object sender, EventArgs e ) {
			Close();
		}

		private void llblSiteLink_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e ) {
			this.llblSiteLink.LinkVisited = true;
			System.Diagnostics.Process.Start( "http://arad.test.jp.net/lor/" );
		}
	}
}
