using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LordOfRanger.Mouse {
	class Click {
		private enum Operation:int{
			MOVE = 0x0001,
			LEFTDOWN = 0x0002,
			LEFTUP = 0x0004,
			RIGHTDOWN = 0x0008,
			RIGHTUP = 0x0010,
			MIDDLEDOWN = 0x0020,
			MIDDLEUP = 0x0040,
			XDOWN = 0x0080,
			XUP = 0x0100,
			WHEEL = 0x0800,
			ABSOLUTE = 0x8000
		}

		/// <summary>
		/// 左クリック
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="sleepBetween">MOUSEDOWNとMOUSEUPの間の時間(ms)</param>
		public static void Left( int x,int y,int sleepBetween) {
			Api.mouse_event( (int)Operation.LEFTDOWN, x, y, 0, UIntPtr.Zero );
			Thread.Sleep( 30 );
			Api.mouse_event( (int)Operation.LEFTUP, x, y, 0, UIntPtr.Zero );
		}

		/// <summary>
		/// 右クリック
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="sleepBetween">MOUSEDOWNとMOUSEUPの間の時間(ms)</param>
		public static void Right( int x, int y, int sleepBetween ) {
			Api.mouse_event( (int)Operation.RIGHTDOWN, x, y, 0, UIntPtr.Zero );
			Thread.Sleep( 30 );
			Api.mouse_event( (int)Operation.RIGHTUP, x, y, 0, UIntPtr.Zero );
		}
	}

}
