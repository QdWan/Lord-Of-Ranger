using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace LordOfRanger.Arad {
	/// <summary>
	/// アラド戦記クライアントに対する動作はここで定義する
	/// なお、アラド戦記が多数起動されていない前提で作成されている
	/// </summary>
	static class Client {

		private const int DEFAULT_W = 800;
		private const int DEFAULT_H = 600;
		internal static double ratioH;
		internal static double ratioW;
		private static Process _process;
		internal static int x;
		internal static int y;
		internal static int w = DEFAULT_W;
		internal static int h = DEFAULT_H;
		private static bool _isAlive;
		internal static int switchPosition = 0;


		/// <summary>
		/// 800x600の時のアイコンサイズ
		/// </summary>
		private static readonly Point ICON_SIZE = new Point( 29, 29 );
		private static readonly Point JUDGE_POINT = new Point(8,20);
		/// <summary>
		/// 800x600の時のクイックスロットの左上の座標
		/// </summary>
		private static readonly Point[] QUICK_SLOT_POINTS = {
			new Point(85,558),
			new Point(115,558),
			new Point(145,558),
			new Point(175,558),
			new Point(205,558),
			new Point(235,558)
		};

		private static readonly Color SWITCH_A_COLOR_MAX = Color.FromArgb( 88, 255, 102 );
		private static readonly Color SWITCH_A_COLOR_MIN = Color.FromArgb( 81, 254, 98 );
		private static readonly Color SWITCH_B_COLOR_MAX = Color.FromArgb( 255, 238, 88 );
		private static readonly Color SWITCH_B_COLOR_MIN = Color.FromArgb( 254, 233, 85 );

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
				var hWnd = Win32.Window.GetForegroundWindow();
				int id;
				Win32.Window.GetWindowThreadProcessId( hWnd, out id );
				return _process.Id == id;

			}
		}

		internal static SwitchingStyle SwitchState {
			get {
				var bmp = GetScreenShot(
					(int)Math.Round( QUICK_SLOT_POINTS[switchPosition].X * ratioW ),
					(int)Math.Round( QUICK_SLOT_POINTS[switchPosition].Y * ratioH ),
					(int)Math.Round( ICON_SIZE.X * ratioW ),
					(int)Math.Round( ICON_SIZE.Y * ratioH ) );
				var color = bmp.GetPixel( (int)Math.Round( JUDGE_POINT.X * ratioW ), (int)Math.Round( JUDGE_POINT.Y * ratioH ) );
				if( CheckColor(color,SWITCH_A_COLOR_MIN,SWITCH_A_COLOR_MAX )) {
					return SwitchingStyle.A;
				}
				if( CheckColor( color, SWITCH_B_COLOR_MIN, SWITCH_B_COLOR_MAX ) ) {
					return SwitchingStyle.B;
				}
				return SwitchingStyle.UNKNOWN;
			}
		}

		private static bool CheckColor( Color check, Color min, Color max ) {
			if( check.R >= min.R && check.R <= max.R && check.G >= min.G && check.G <= max.G && check.B >= min.B && check.B <= max.B ) {
				return true;
			}
			return false;

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

			_process = Process.GetProcessesByName( Properties.Settings.Default.processName ).FirstOrDefault( hProcess => !hProcess.HasExited && hProcess.MainWindowHandle != IntPtr.Zero );

			if( _process == null ) {
				_isAlive = false;
				return;
			}
			_isAlive = true;
			Win32.Window.RECT rect;
			try {
				Win32.Window.GetWindowRect( _process.MainWindowHandle, out rect );
			} catch( Exception ) {
				return;
			}
			x = rect.left + MARGIN_LEFT;
			y = rect.top + MARGIN_TOP;
			w = rect.right - rect.left - MARGIN_LEFT - MARGIN_RIGHT;
			h = rect.bottom - rect.top - MARGIN_BOTTOM - MARGIN_TOP;

			ratioH = ( (double)h / DEFAULT_H );
			ratioW = ( (double)w / DEFAULT_W );
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
			var disDc = Win32.DC.GetDC( IntPtr.Zero );
			using( var g = Graphics.FromImage( bmp ) ) {
				var hDc = g.GetHdc();
				Win32.Object.BitBlt( hDc, 0, 0, targetW, targetH, disDc, targetX, targetY, 13369376 );
				g.ReleaseHdc( hDc );
			}
			Win32.DC.ReleaseDC( IntPtr.Zero, disDc );
			return bmp;
		}
	}
}
