using System;
using System.Runtime.InteropServices;

namespace LordOfRanger.Win32 {
	static class Window {

		//Get Active Window
		[DllImport( "user32.dll" )]
		internal static extern IntPtr GetForegroundWindow();
		[DllImport( "user32.dll" )]
		internal static extern int GetWindowThreadProcessId( IntPtr hWnd, out int lpdwProcessId );

		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		internal static extern int ShowWindow( IntPtr hWnd, int nCmdShow );
		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		internal static extern bool SetForegroundWindow( IntPtr hWnd );
		internal const int SW_NORMAL = 1;

		[DllImport( "user32.dll" )]
		internal static extern int GetWindowRect( IntPtr hWnd, out RECT rect );

		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct RECT {
			internal readonly int left;
			internal readonly int top;
			internal readonly int right;
			internal readonly int bottom;
		}


	}
}
