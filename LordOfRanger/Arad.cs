using System;
using System.Diagnostics;
using System.Drawing;
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
		private static bool _isAlive;
		internal static int switchPosition = 0;

		private static readonly Point ICON_SIZE = new Point( 29, 29 );
		private static readonly Point[] QUICK_SLOT_POINTS = {
			new Point(85,558),
			new Point(115,558),
			new Point(145,558),
			new Point(175,558),
			new Point(205,558),
			new Point(235,558)
		};

		private static readonly Color SWITCH_A_COLOR = Color.FromArgb( 97, 255, 109 );
		private static readonly Color SWITCH_B_COLOR = Color.FromArgb( 255, 238, 97 );

		internal static bool IsAlive {
			get {
				if( _isAlive ) {
					_isAlive = !_process.HasExited;
				} else {
					Get();
				}
				return _isAlive;
			}
		}

		internal static bool IsActiveWindow {
			get {
				var hWnd = Api.GetForegroundWindow();
				int id;
				Api.GetWindowThreadProcessId( hWnd, out id );
				return _process.Id == id;

			}
		}

		internal static SwitchingStyle SwitchState {
			get {
				var bmp = GetScreenShot( QUICK_SLOT_POINTS[switchPosition].X,QUICK_SLOT_POINTS[switchPosition].Y,ICON_SIZE.X,ICON_SIZE.Y );
				var color = bmp.GetPixel( 21, 7 );
				if( color == SWITCH_A_COLOR ) {
					return SwitchingStyle.A;
				}
				if( color == SWITCH_B_COLOR ) {
					return SwitchingStyle.B;
				}
				return SwitchingStyle.UNKNOWN;
			}
		}

		private const int MARGIN_LEFT = 0;
		private const int MARGIN_TOP = 0;
		private const int MARGIN_RIGHT = 0;
		private const int MARGIN_BOTTOM = 0;

		/// <summary>
		/// プロセスを取得し、x,y,w,hをクラスのメンバ変数に格納する
		/// </summary>
		/// <returns>アラド戦記プロセス</returns>
		internal static void Get() {

			_process = Process.GetProcessesByName( Options.Options.options.processName ).FirstOrDefault( hProcess => !hProcess.HasExited && hProcess.MainWindowHandle != IntPtr.Zero );

			if( _process == null ) {
				_isAlive = false;
				return;
			}
			_isAlive = true;
			Api.Rect rect;
			try {
				Api.GetWindowRect( _process.MainWindowHandle, out rect );
			} catch( Exception ) {
				return;
			}
			x = rect.left + MARGIN_LEFT;
			y = rect.top + MARGIN_TOP;
			w = rect.right - rect.left - MARGIN_LEFT - MARGIN_RIGHT;
			h = rect.bottom - rect.top - MARGIN_BOTTOM - MARGIN_TOP;
		}

		private static Bitmap GetScreenShot(int targetX = 0,int targetY = 0,int targetW = 0,int targetH = 0) {
			if( targetW == 0 ) {
				targetW = w;
			}
			if( targetH == 0 ) {
				targetH = h;
			}
			if( targetX + targetW > w || targetY + targetH > h ) {
				throw new ArgumentOutOfRangeException();
			}

			//アラドの相対座標から画面の絶対座標に変換
			targetX += x;
			targetY += y;

			var bmp = new Bitmap( targetW, targetH );
			var disDc = Api.GetDC( IntPtr.Zero );
			using( var g = Graphics.FromImage( bmp ) ) {
				var hDc = g.GetHdc();
				Api.BitBlt( hDc, 0, 0, targetW, targetH, disDc, targetX, targetY, 13369376 );
				g.ReleaseHdc( hDc );
			}
			Api.ReleaseDC( IntPtr.Zero, disDc );
			return bmp;
		}
	}
}
