using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			lblInfo.Text = "\n\n" + appProductName + "\n";
			lblInfo.Text += "Application Version : " + appVersion;
		}
	}
}
