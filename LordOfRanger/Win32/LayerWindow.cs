using System;
using System.Drawing;
using System.Runtime.InteropServices;
// ReSharper disable NotAccessedField.Global
// ReSharper disable MemberCanBePrivate.Global

namespace LordOfRanger.Win32 {
	static class LayerWindow {
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
			internal Blendfunction( byte blendOp, byte blendFlags, byte sourceConstantAlpha, byte alphaFormat ) {
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
			int dwFlags );

	}
}
