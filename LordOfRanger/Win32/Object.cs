using System;
using System.Runtime.InteropServices;

namespace LordOfRanger.Win32 {
	static class Object {
		[DllImport( "gdi32.dll" )]
		public static extern int BitBlt( IntPtr hDestDc, int x, int y, int nWidth, int nHeight, IntPtr hSrcDc, int xSrc, int ySrc, int dwRop );

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr SelectObject( IntPtr hdc, IntPtr hgdiobj );

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern int DeleteObject( IntPtr hobject );
	}
}
