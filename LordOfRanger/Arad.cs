using System;
using System.Diagnostics;
using System.Linq;



namespace LordOfRanger {
	/// <summary>
	/// アラド戦記クライアントに対する動作はここで定義する
	/// なお、アラド戦記が多数起動されていない前提で作成されている
	/// </summary>
	static class Arad {

		private static Process _process;
		internal static int x;
		internal static int y;
		internal static int w = 800;
		internal static int h = 600;

		private const int MARGIN_LEFT = 0;
		private static int _marginTop = 0;
		private const int MARGIN_RIGHT = 0;
		private const int MARGIN_BOTTOM = 0;

		/// <summary>
		/// プロセスを取得し、x,y,w,hをクラスのメンバ変数に格納する
		/// </summary>
		/// <returns>アラド戦記プロセス</returns>
		internal static Process Get() {

			_process = Process.GetProcessesByName( "ARAD" ).FirstOrDefault( hProcess => !hProcess.HasExited && hProcess.MainWindowHandle != IntPtr.Zero );

			if( _process == null ) {
				return null;
			}

			Api.Rect rect;
			try {
				Api.GetWindowRect( _process.MainWindowHandle, out rect );
			} catch( Exception ) {
				return _process;
			}
			x = rect.left + MARGIN_LEFT;
			y = rect.top + _marginTop;
			w = rect.right - rect.left - MARGIN_LEFT - MARGIN_RIGHT;
			h = rect.bottom - rect.top - MARGIN_BOTTOM - _marginTop;

			return _process;
		}
	}
}
