using System;
using System.Diagnostics;
using System.Linq;
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
					var hThisProcess = Process.GetCurrentProcess();
					var hProcesses = Process.GetProcessesByName( hThisProcess.ProcessName );
					var iThisProcessId = hThisProcess.Id;

					foreach( var hProcess in hProcesses.Where( hProcess => hProcess.Id != iThisProcessId ) ) {
						Api.ShowWindow( hProcess.MainWindowHandle, Api.SW_NORMAL );
						Api.SetForegroundWindow( hProcess.MainWindowHandle );
						break;
					}
					MessageBox.Show( "Already been started!" );
				}
			}
		}
	}
}
