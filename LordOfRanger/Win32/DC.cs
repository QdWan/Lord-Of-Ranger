using System;
using System.Runtime.InteropServices;

namespace LordOfRanger.Win32 {
	static class DC {

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr CreateCompatibleDC( IntPtr hdc );

		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr GetDC( IntPtr hwnd );

		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr ReleaseDC( IntPtr hwnd, IntPtr hdc );

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern int DeleteDC( IntPtr hdc );

	}
}
