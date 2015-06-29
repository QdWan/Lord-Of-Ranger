using System;
using System.Diagnostics;

namespace LordOfRanger {
	/// <summary>
	/// アラド戦記クライアントに対する動作はここで定義する
	/// なお、アラド戦記が多数起動されていない前提で作成されている
	/// </summary>
	class Arad {
		internal static Process process;
		internal static int x = 0;
		internal static int y = 0;
		internal static int w = 800;
		internal static int h = 600;

		private const int MARGIN_LEFT = 0;
		private static int MARGIN_TOP = 0;
		private const int MARGIN_RIGHT = 0;
		private const int MARGIN_BOTTOM = 0;

		/// <summary>
		/// プロセスを取得し、x,y,w,hをクラスのメンバ変数に格納する
		/// </summary>
		/// <returns>アラド戦記プロセス</returns>
		internal static Process get() {
			Process tempProcess = null;
			foreach( Process hProcess in Process.GetProcesses() ) {
				if( hProcess.ProcessName == Options.Options.options.ProcessName ) {
					tempProcess = hProcess;
					break;
				}
			}
			if( tempProcess == null ) {
				return null;
			}
			process = tempProcess;

			//MARGIN_TOP = API.GetSystemMetrics( API.SystemMetric.SM_CYSIZE ) + 4;


			API.RECT rect;
			try {
				API.GetWindowRect( process.MainWindowHandle, out rect );
			} catch( Exception ) {
				return process;
			}
			x = rect.left + MARGIN_LEFT;
			y = rect.top + MARGIN_TOP;
			w = rect.right - rect.left - MARGIN_LEFT - MARGIN_RIGHT;
			h = rect.bottom - rect.top - MARGIN_BOTTOM - MARGIN_TOP;

			return process;
		}
	}
}
