using System;
using System.Runtime.InteropServices;

namespace LordOfRanger.Win32 {
	static class Mouse {

		[DllImport( "user32.dll" )]
		public static extern bool SetCursorPos( int x, int y );

		[DllImport( "user32.dll" )]
		public static extern void mouse_event( int e, int dx, int dy, uint data, UIntPtr extraInfo );

	}
}
