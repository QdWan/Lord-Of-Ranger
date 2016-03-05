using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LordOfRanger.Win32 {
	class Keyboard {
		[DllImport( "user32.dll" )]
		internal static extern uint keybd_event( byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo );

		[DllImport( "user32.dll", EntryPoint = "MapVirtualKeyA" )]
		internal static extern int MapVirtualKey( int wCode, int wMapType );
	}
}
