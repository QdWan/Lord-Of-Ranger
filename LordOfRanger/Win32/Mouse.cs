using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LordOfRanger.Win32 {
	class Mouse {

		[DllImport( "user32.dll" )]
		public static extern bool SetCursorPos( int x, int y );

		[DllImport( "user32.dll" )]
		public static extern void mouse_event( int e, int dx, int dy, uint data, UIntPtr extraInfo );

		[StructLayout( LayoutKind.Sequential )]
		public struct Win32Point {

			private int X;
			private int Y;

		}

	}
}
