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

		/// <summary>
		/// アラド戦記クライアントの横幅デフォルト値
		/// </summary>
		private const int DEFAULT_W = 800;

		/// <summary>
		/// アラド戦記クライアントの高さデフォルト値
		/// </summary>
		private const int DEFAULT_H = 600;

		/// <summary>
		/// アラド戦記のクライアントの実際の高さとデフォルト値との比率
		/// </summary>
		internal static double ratioH;

		/// <summary>
		/// アラド戦記のクライアントの実際の横幅とデフォルト値との比率
		/// </summary>
		internal static double ratioW;

		/// <summary>
		/// アラド戦記のプロセス
		/// </summary>
		private static Process _process;

		/// <summary>
		/// アラド戦記のクライアントの左上X座標
		/// </summary>
		internal static int x;

		/// <summary>
		/// アラド戦記のクライアントの左上Y座標
		/// </summary>
		internal static int y;

		/// <summary>
		/// アラド戦記のクライアントの横幅
		/// </summary>
		internal static int w = DEFAULT_W;

		/// <summary>
		/// アラド戦記のクライアントの高さ
		/// </summary>
		internal static int h = DEFAULT_H;

		/// <summary>
		/// アラド戦記のクライアントが立ち上がっているかどうかのフラグ
		/// </summary>
		private static bool _isAlive;

		/// <summary>
		/// スイッチングシステムがクイックスロットの何番目に配置されているか(0~5)
		/// </summary>
		internal static int switchPosition = 0;


		/// <summary>
		/// 800x600の時のアイコンサイズ
		/// </summary>
		private static readonly Point ICON_SIZE = new Point( 29, 29 );

		/// <summary>
		/// 800x600の時のアイコンの左上から、判定ポイントへの相対位置
		/// </summary>
		private static readonly Point JUDGE_POINT = new Point( 8, 20 );

		/// <summary>
		/// 800x600の時のクイックスロットの左上の座標
		/// </summary>
		private static readonly Point[] QUICK_SLOT_POINTS = {
			new Point(92,562),
			new Point(122,562),
			new Point(152,562),
			new Point(182,562),
			new Point(212,562),
			new Point(242,562)
		};

		/// <summary>
		/// スイッチアイコン判定時の判定ポイントの色閾値 A面 最大値
		/// </summary>
		private static readonly Color SWITCH_A_COLOR_MAX = Color.FromArgb( 88, 255, 102 );

		/// <summary>
		/// スイッチアイコン判定時の判定ポイントの色閾値 A面 最小値
		/// </summary>
		private static readonly Color SWITCH_A_COLOR_MIN = Color.FromArgb( 81, 254, 98 );

		/// <summary>
		/// スイッチアイコン判定時の判定ポイントの色閾値 B面 最大値
		/// </summary>
		private static readonly Color SWITCH_B_COLOR_MAX = Color.FromArgb( 255, 238, 88 );

		/// <summary>
		/// スイッチアイコン判定時の判定ポイントの色閾値 B面 最小値
		/// </summary>
		private static readonly Color SWITCH_B_COLOR_MIN = Color.FromArgb( 254, 233, 85 );

		/// <summary>
		/// アラド戦記のクライアントが立ち上がっているかどうかのフラグを取得する
		/// </summary>
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

		/// <summary>
		/// アラド戦記のクライアントが最前面にきているかどうかのフラグを取得する
		/// </summary>
		internal static bool IsActiveWindow {
			get {
				Get();
				var hWnd = Win32.Window.GetForegroundWindow();
				int id;
				Win32.Window.GetWindowThreadProcessId( hWnd, out id );
				return _process.Id == id;

			}
		}

		/// <summary>
		/// スイッチがA面、B面どちらの状態にあるかを判定する。
		/// A : SwitchingStyle.A
		/// B : SwitchingStyle.B
		/// 判定不可 : SwitchingStyle.UNKNOWN
		/// </summary>
		internal static SwitchingStyle SwitchState {
			get {
				var bmp = GetScreenShot(
					(int)Math.Round( QUICK_SLOT_POINTS[switchPosition].X * ratioW ),
					(int)Math.Round( QUICK_SLOT_POINTS[switchPosition].Y * ratioH ),
					(int)Math.Round( ICON_SIZE.X * ratioW ),
					(int)Math.Round( ICON_SIZE.Y * ratioH ) );
				var color = bmp.GetPixel( (int)Math.Round( JUDGE_POINT.X * ratioW ), (int)Math.Round( JUDGE_POINT.Y * ratioH ) );
				if( CheckColor( color, SWITCH_A_COLOR_MIN, SWITCH_A_COLOR_MAX ) ) {
					return SwitchingStyle.A;
				}
				if( CheckColor( color, SWITCH_B_COLOR_MIN, SWITCH_B_COLOR_MAX ) ) {
					return SwitchingStyle.B;
				}
				return SwitchingStyle.UNKNOWN;
			}
		}

		/// <summary>
		/// 色閾値の検証値が最小値と最大値の間に収まっているかを判定する
		/// </summary>
		/// <param name="check">検証値</param>
		/// <param name="min">最小値</param>
		/// <param name="max">最大値</param>
		/// <returns>結果</returns>
		private static bool CheckColor( Color check, Color min, Color max ) {
			if( check.R >= min.R && check.R <= max.R && check.G >= min.G && check.G <= max.G && check.B >= min.B && check.B <= max.B ) {
				return true;
			}
			return false;

		}

		/// <summary>
		/// Windowのマージン 左
		/// </summary>
		private const int MARGIN_LEFT = 0;

		/// <summary>
		/// Windowのマージン 上
		/// </summary>
		private const int MARGIN_TOP = 0;

		/// <summary>
		/// Windowのマージン 右
		/// </summary>
		private const int MARGIN_RIGHT = 0;


		/// <summary>
		/// Windowのマージン 下
		/// </summary>
		private const int MARGIN_BOTTOM = 0;

		/// <summary>
		/// プロセスを取得し、
		/// アラド戦記のクライアント生存状況,X,Y,W,Hの値を取得し
		/// さらにW,Hを元にデフォルト値からの比率を計算する。
		/// </summary>
		internal static void Get( ) {
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

		/// <summary>
		/// アラド戦記のクライアントのスクリーンショットを取得する。
		/// 引数情報により、スクリーンショットの範囲を変更できる。
		/// 何も渡さなければアラド戦記のクライアント全体が取得できる。
		/// </summary>
		/// <param name="targetX">左上X座標</param>
		/// <param name="targetY">左上Y座標</param>
		/// <param name="targetW">横幅</param>
		/// <param name="targetH">高さ</param>
		/// <returns></returns>
		private static Bitmap GetScreenShot( int targetX = 0, int targetY = 0, int targetW = 0, int targetH = 0 ) {
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
