using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LordOfRanger.Win32 {
	class Object {
		[DllImport( "gdi32.dll" )]
		public static extern int BitBlt( IntPtr hDestDc, int x, int y, int nWidth, int nHeight, IntPtr hSrcDc, int xSrc, int ySrc, int dwRop );


		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr SelectObject( IntPtr hdc, IntPtr hgdiobj );

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern int DeleteObject( IntPtr hobject );
	}
}
