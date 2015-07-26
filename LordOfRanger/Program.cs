using System;
using System.Windows.Forms;

namespace LordOfRanger {
	static class Program {
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() {
			Options.OptionsForm.LoadCnf();
			// 多重起動対策 
			using( var mutex = new System.Threading.Mutex( false, Application.ProductName ) ) {
				if( mutex.WaitOne( 0, false ) ) {
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault( false );
					var mainForm = new MainForm();
					if( Options.Options.options.startupState == (int)Options.Options.StartupState.NORMAL ) {
						Application.Run( mainForm );
					} else {
						Application.Run();
					}
				} else {
					MessageBox.Show( "Already been started!" );
				}
			}
		}
	}
}
