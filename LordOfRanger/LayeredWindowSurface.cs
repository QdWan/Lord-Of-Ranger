// ReSharper disable All
namespace LordOfRanger {
	/// <summary>
	/// レイヤードウィンドウ
	/// </summary>
	internal class LayeredWindowSurface :System.Windows.Forms.Form {
		protected System.ComponentModel.IContainer Components;

		/// <summary>
		/// マウス位置を取得、設定します。
		/// </summary>
		protected System.Drawing.Point mousePoint;

		/// <summary>
		/// オフスクリーンを取得、設定します。
		/// </summary>
		protected System.Drawing.Bitmap offScreenCache;

		/// <summary>
		/// BLENDFUNCTION を取得、設定します。
		/// </summary>
		protected Api.Blendfunction blendFunction;



		/// <summary>
		/// ドラッグによる移動を可能にするかどうか設定します。
		/// </summary>
		internal bool AllowDragMove {
			set {
				MouseDown -= MouseDownEvent;
				MouseMove -= MouseMoveEvent;

				if( value ) {
					MouseDown += MouseDownEvent;
					MouseMove += MouseMoveEvent;
				}
			}
		}

		/// <summary>
		/// レイヤードウィンドウの透過度（0-255）を取得、設定します。
		/// </summary>
		internal int Alpha {
			get {
				return this.blendFunction.sourceConstantAlpha;
			}
			set {
				this.blendFunction.sourceConstantAlpha = ( 0 <= value && value <= 255 ) ? (byte)value : (byte)255;
			}
		}

		/// <summary>
		/// コントロールを識別するハンドルを作成するときに必要な作成パラメータを格納している <see cref="System.Windows.Forms.CreateParams"/> 。
		/// </summary>
		protected override System.Windows.Forms.CreateParams CreateParams {
			get {
				var createParam = base.CreateParams;
				createParam.ExStyle = createParam.ExStyle | 0x00080000;

				if( ( createParam.ExStyle & 0x00000020 ) != 0 ) {
					createParam.ExStyle = createParam.ExStyle & 0x00000020;
				}

				return createParam;
			}
		}

		/// <summary>
		/// オフスクリーンを取得、設定します。
		/// </summary>
		protected System.Drawing.Bitmap OffScreen {
			get {
				if( this.offScreenCache == null ) {
					this.offScreenCache = new System.Drawing.Bitmap( Size.Width, Size.Height );
				}

				return this.offScreenCache;
			}
			set {
				this.offScreenCache = value;
			}
		}

		/// <summary>
		/// <see>
		///     <cref>MMFrame.Forms.LayeredWindows.LayeredWindowSurface</cref>
		/// </see>
		///     のコンストラクタ
		/// </summary>
		protected LayeredWindowSurface() {
			SuspendLayout();
			ShowInTaskbar = false;
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			ResumeLayout( false );
			AllowDragMove = true;
			this.blendFunction = new Api.Blendfunction( 0, 0, 255, 1 );
		}

		/// <summary>
		/// <see cref="System.Drawing.Bitmap"/> をオフスクリーンに書き込みます。
		/// </summary>
		/// <param name="srcBitmap">オフスクリーンに書き込む <see cref="System.Drawing.Bitmap"/></param>
		internal void DrawImage( System.Drawing.Bitmap srcBitmap ) {
			using( var g = System.Drawing.Graphics.FromImage( OffScreen ) ) {
				g.DrawImage( srcBitmap, 0, 0, srcBitmap.Width, srcBitmap.Height );
			}
		}

		/// <summary>
		/// <see cref="System.Drawing.Bitmap"/> をオフスクリーンに書き込みます。
		/// </summary>
		/// <param name="srcBitmap">オフスクリーンに書き込む <see cref="System.Drawing.Bitmap"/></param>
		/// <param name="alpha"><paramref name="srcBitmap"/> の透過度（0-255）</param>
		internal void DrawImage( System.Drawing.Bitmap srcBitmap, int alpha ) {
			var cm = new System.Drawing.Imaging.ColorMatrix();
			cm.Matrix00 = 1;
			cm.Matrix11 = 1;
			cm.Matrix22 = 1;
			cm.Matrix33 = alpha / 255F;
			cm.Matrix44 = 1;

			using( var g = System.Drawing.Graphics.FromImage( OffScreen ) )
			using( var ia = new System.Drawing.Imaging.ImageAttributes() ) {
				ia.SetColorMatrix( cm );
				g.DrawImage( srcBitmap, new System.Drawing.Rectangle( 0, 0, srcBitmap.Width, srcBitmap.Height ), 0, 0, srcBitmap.Width, srcBitmap.Height, System.Drawing.GraphicsUnit.Pixel, ia );
			}
		}

		/// <summary>
		/// 文字列をオフスクリーンに書き込みます。
		/// </summary>
		/// <param name="text">書き込む文字列</param>
		/// <param name="font"><paramref name="text"/> のフォント</param>
		/// <param name="color"><paramref name="text"/> の色</param>
		/// <param name="outlineColor"><paramref name="text"/> の縁取り色</param>
		/// <param name="rectangle"><paramref name="text"/> の書き込み位置及び範囲</param>
		internal void DrawText( string text, System.Drawing.Font font, System.Drawing.Brush color, System.Drawing.Pen outlineColor, System.Drawing.RectangleF rectangle ) {
			using( var g = System.Drawing.Graphics.FromImage( OffScreen ) ) {
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

				if( outlineColor == null ) {
					g.FillRectangle( System.Drawing.Brushes.Transparent, rectangle );
					g.DrawString( text, font, color, rectangle );
				} else {
					using( var gp = new System.Drawing.Drawing2D.GraphicsPath() ) {
						var sizeInPixels = font.SizeInPoints * g.DpiY / 72;
						gp.AddString( text, font.FontFamily, (int)font.Style, sizeInPixels, rectangle, null );
						g.DrawPath( outlineColor, gp );
						g.FillPath( color, gp );
					}
				}
			}
		}

		/// <summary>
		/// オフスクリーンを使用してレイヤードウィンドウを更新します。
		/// </summary>
		internal void UpdateLayeredWindow() {
			var screen = System.IntPtr.Zero;
			var memScreen = System.IntPtr.Zero;
			var gdiBitmap = System.IntPtr.Zero;
			var oldBitmap = System.IntPtr.Zero;

			try {
				screen = Api.GetDC( System.IntPtr.Zero );
				memScreen = Api.CreateCompatibleDC( screen );
				gdiBitmap = OffScreen.GetHbitmap( System.Drawing.Color.FromArgb( 0 ) );
				oldBitmap = Api.SelectObject( memScreen, gdiBitmap );

				var size = Size;
				var pointSource = new System.Drawing.Point( 0, 0 );
				var topPos = Location;

				InvokeWithThreadSafe( () => {
					Api.UpdateLayeredWindow( Handle, screen, ref topPos, ref size, memScreen, ref pointSource, 0, ref this.blendFunction, 2 );
				} );
			} finally {
				Api.ReleaseDC( System.IntPtr.Zero, screen );
				Api.SelectObject( memScreen, oldBitmap );
				Api.DeleteObject( gdiBitmap );
				Api.DeleteDC( memScreen );
				OffScreen.Dispose();
				OffScreen = null;
			}
		}

		/// <summary>
		/// スレッドセーフでコードを実行します。
		/// </summary>
		/// <param name="action">スレッドセーフで実行する <see cref="System.Action"/></param>
		protected void InvokeWithThreadSafe( System.Action action ) {
			if( InvokeRequired ) {
				Invoke( (System.Windows.Forms.MethodInvoker)delegate {
					action();
				} );
			} else {
				action();
			}
		}

		/// <summary>
		/// 割り当てられたリソースを解放します。
		/// </summary>
		/// <param name="disposing">マネージドリソースの解放をする場合は true</param>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				AllowDragMove = false;

				if( OffScreen != null ) {
					OffScreen.Dispose();
					OffScreen = null;
				}

				if( this.Components != null ) {
					this.Components.Dispose();
					this.Components = null;
				}

				if( ContextMenuStrip != null ) {
					ContextMenuStrip.Dispose();
					ContextMenuStrip = null;
				}
			}

			InvokeWithThreadSafe( () => {
				base.Dispose( disposing );
			} );
		}

		/// <summary>
		/// マウスクリック時のイベント
		/// </summary>
		/// <param name="sender"><see cref="System.Object"/></param>
		/// <param name="e"><see cref="System.Windows.Forms.MouseEventArgs"/></param>
		private void MouseDownEvent( object sender, System.Windows.Forms.MouseEventArgs e ) {
			if( ( e.Button & System.Windows.Forms.MouseButtons.Left ) == System.Windows.Forms.MouseButtons.Left ) {
				this.mousePoint = new System.Drawing.Point( e.X, e.Y );
			}
		}

		/// <summary>
		/// マウス移動時のイベント
		/// </summary>
		/// <param name="sender"><see cref="System.Object"/></param>
		/// <param name="e"><see cref="System.Windows.Forms.MouseEventArgs"/></param>
		private void MouseMoveEvent( object sender, System.Windows.Forms.MouseEventArgs e ) {
			if( ( e.Button & System.Windows.Forms.MouseButtons.Left ) == System.Windows.Forms.MouseButtons.Left ) {
				Location = new System.Drawing.Point( Location.X + e.X - this.mousePoint.X, Location.Y + e.Y - this.mousePoint.Y );
			}
		}
	}
}
