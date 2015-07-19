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
			using( System.Threading.Mutex mutex = new System.Threading.Mutex( false, Application.ProductName ) ) {
				if( mutex.WaitOne( 0, false ) ) {
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault( false );
					if( Options.Options.options.startupState == (int)Options.Options.StartupState.NORMAL ) {
						Application.Run( new MainForm() );
					} else {
						new MainForm();
						Application.Run();
					}
				} else {
					MessageBox.Show( "Already been started!" );
				}
			}
		}
	}
}
