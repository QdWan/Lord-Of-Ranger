using System;
using System.Runtime.InteropServices;
using System.Drawing;
// ReSharper disable NotAccessedField.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace LordOfRanger {

	/// <summary>
	/// API用
	/// </summary>
	internal static class Api {

		#region ウィンドウ操作関連

		//Get Active Window
		[DllImport( "user32.dll" )]
		internal static extern IntPtr GetForegroundWindow();
		[DllImport( "user32.dll" )]
		internal static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		[DllImport( "USER32.DLL", CharSet = CharSet.Auto )]
		internal static extern int ShowWindow( IntPtr hWnd, int nCmdShow );
		[DllImport( "USER32.DLL", CharSet = CharSet.Auto )]
		internal static extern bool SetForegroundWindow( IntPtr hWnd );
		internal const int SW_NORMAL = 1;

		//構造体
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct Rect {
			internal readonly int left;
			internal readonly int top;
			internal readonly int right;
			internal readonly int bottom;
		}

		//座標情報取得
		[DllImport( "user32.dll" )]
		internal static extern int GetWindowRect(IntPtr hWnd, out Rect rect);

		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern int DeleteObject(IntPtr hobject);

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern int DeleteDC(IntPtr hdc);


		//Layer Window
		internal struct Blendfunction {
			/// <summary>
			/// Specifies the source blend operation. Currently, the only source and destination blend operation that has been defined is AC_SRC_OVER. For details, see the following Remarks section.
			/// </summary>
			internal byte blendOp;

			/// <summary>
			/// Must be zero.
			/// </summary>
			internal byte blendFlags;

			/// <summary>
			/// Specifies an alpha transparency value to be used on the entire source bitmap.
			/// </summary>
			internal byte sourceConstantAlpha;

			/// <summary>
			/// This member controls the way the source and destination bitmaps are interpreted. 
			/// </summary>
			internal byte alphaFormat;

			/// <summary>
			/// <see>
			///     <cref>MMFrame.Windows.Win32Api.User32.BLENDFUNCTION</cref>
			/// </see>
			///     のコンストラクタ
			/// </summary>
			/// <param name="blendOp">Specifies the source blend operation.</param>
			/// <param name="blendFlags">Must be zero.</param>
			/// <param name="sourceConstantAlpha">Specifies an alpha transparency value to be used on the entire source bitmap.</param>
			/// <param name="alphaFormat">This member controls the way the source and destination bitmaps are interpreted.</param>
			internal Blendfunction(byte blendOp, byte blendFlags, byte sourceConstantAlpha, byte alphaFormat) {
				this.blendOp = blendOp;
				this.blendFlags = blendFlags;
				this.sourceConstantAlpha = sourceConstantAlpha;
				this.alphaFormat = alphaFormat;
			}
		}
		/// <summary>
		/// レイヤードウィンドウの位置、サイズ、形、内容、透明度を更新します。
		/// </summary>
		/// <param name="hwnd">レイヤードウィンドウのハンドル</param>
		/// <param name="hdcDst">画面のデバイスコンテキストのハンドル</param>
		/// <param name="pptDst">画面の新しい位置</param>
		/// <param name="psize">レイヤードウィンドウの新しいサイズ</param>
		/// <param name="hdcSrc">サーフェスのデバイスコンテキストのハンドル</param>
		/// <param name="pptSrc">レイヤの位置</param>
		/// <param name="crKey">カラーキー</param>
		/// <param name="pblend">ブレンド機能</param>
		/// <param name="dwFlags">フラグ</param>
		/// <returns>関数が成功すると、0 以外の値が返ります。</returns>
		[DllImport( "user32.dll" )]
		internal static extern int UpdateLayeredWindow(
			IntPtr hwnd,
			IntPtr hdcDst,
			[In]
			ref Point pptDst,
			[In]
			ref Size psize,
			IntPtr hdcSrc,
			[In]
			ref Point pptSrc,
			int crKey,
			[In]
			ref Blendfunction pblend,
			int dwFlags);

		#endregion

		#region マウス操作

		[DllImport( "user32.dll" )]
		public static extern bool SetCursorPos( int x, int y );

		[DllImport( "user32.dll" )]
		public static extern void mouse_event( int e, int dx, int dy, uint data, UIntPtr extraInfo );

		[StructLayout( LayoutKind.Sequential )]
		public struct Win32Point {

			private int X;
			private int Y;

		}

		#endregion

		#region キーボード操作

		[DllImport( "user32.dll" )]
		internal static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

		[DllImport( "user32.dll", EntryPoint = "MapVirtualKeyA" )]
		internal static extern int MapVirtualKey(int wCode, int wMapType);

		#endregion
	}

}
