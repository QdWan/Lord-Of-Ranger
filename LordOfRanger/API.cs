using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace LordOfRanger {

	/// <summary>
	/// API用
	/// </summary>
	internal class API {

		#region ウィンドウ操作関連

		//Get Active Window
		[DllImport( "user32.dll" )]
		internal static extern IntPtr GetForegroundWindow();
		[DllImport( "user32.dll" )]
		internal static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		//構造体
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct RECT {
			internal int left;
			internal int top;
			internal int right;
			internal int bottom;
		}

		//座標情報取得
		[DllImport( "user32.dll" )]
		internal static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);
		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern int DeleteObject(IntPtr hobject);

		[DllImport( "gdi32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		internal static extern int DeleteDC(IntPtr hdc);
		internal const byte AC_SRC_OVER = 0;
		internal const byte AC_SRC_ALPHA = 1;
		internal const int ULW_ALPHA = 2;


		//Layer Window
		internal struct BLENDFUNCTION {
			/// <summary>
			/// Specifies the source blend operation. Currently, the only source and destination blend operation that has been defined is AC_SRC_OVER. For details, see the following Remarks section.
			/// </summary>
			internal byte BlendOp;

			/// <summary>
			/// Must be zero.
			/// </summary>
			internal byte BlendFlags;

			/// <summary>
			/// Specifies an alpha transparency value to be used on the entire source bitmap.
			/// </summary>
			internal byte SourceConstantAlpha;

			/// <summary>
			/// This member controls the way the source and destination bitmaps are interpreted. 
			/// </summary>
			internal byte AlphaFormat;

			/// <summary>
			/// <see cref="MMFrame.Windows.Win32Api.User32.BLENDFUNCTION"/> のコンストラクタ
			/// </summary>
			/// <param name="blendOp">Specifies the source blend operation.</param>
			/// <param name="blendFlags">Must be zero.</param>
			/// <param name="sourceConstantAlpha">Specifies an alpha transparency value to be used on the entire source bitmap.</param>
			/// <param name="alphaFormat">This member controls the way the source and destination bitmaps are interpreted.</param>
			internal BLENDFUNCTION(byte blendOp, byte blendFlags, byte sourceConstantAlpha, byte alphaFormat) {
				this.BlendOp = blendOp;
				this.BlendFlags = blendFlags;
				this.SourceConstantAlpha = sourceConstantAlpha;
				this.AlphaFormat = alphaFormat;
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
		[System.Runtime.InteropServices.DllImport( "user32.dll" )]
		internal static extern int UpdateLayeredWindow(
			IntPtr hwnd,
			IntPtr hdcDst,
			[System.Runtime.InteropServices.In()]
			ref Point pptDst,
			[System.Runtime.InteropServices.In()]
			ref Size psize,
			IntPtr hdcSrc,
			[System.Runtime.InteropServices.In()]
			ref Point pptSrc,
			int crKey,
			[System.Runtime.InteropServices.In()]
			ref BLENDFUNCTION pblend,
			int dwFlags);

		#endregion

		#region キーボード操作

		[DllImport( "user32.dll" )]
		internal static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

		[DllImport( "user32.dll", EntryPoint = "MapVirtualKeyA" )]
		internal static extern int MapVirtualKey(int wCode, int wMapType);

		#endregion
	}

}
