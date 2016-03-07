using System;
using System.Runtime.InteropServices;

namespace LordOfRanger.Win32 {
	static class Keyboard {
		[DllImport( "user32.dll" )]
		internal static extern uint keybd_event( byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo );

		[DllImport( "user32.dll", EntryPoint = "MapVirtualKeyA" )]
		internal static extern int MapVirtualKey( int wCode, int wMapType );
	}
}
