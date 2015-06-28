namespace LordOfRanger {
	/// <summary>
	/// レイヤードウィンドウ
	/// </summary>
	internal class LayeredWindowSurface : System.Windows.Forms.Form {
		protected System.ComponentModel.IContainer Components = null;

		/// <summary>
		/// マウス位置を取得、設定します。
		/// </summary>
		protected System.Drawing.Point MousePoint;

		/// <summary>
		/// オフスクリーンを取得、設定します。
		/// </summary>
		protected System.Drawing.Bitmap OffScreenCache;

		/// <summary>
		/// BLENDFUNCTION を取得、設定します。
		/// </summary>
		protected API.BLENDFUNCTION BlendFunction;



		/// <summary>
		/// ドラッグによる移動を可能にするかどうか設定します。
		/// </summary>
		internal bool AllowDragMove {
			set {
				MouseDown -= new System.Windows.Forms.MouseEventHandler( MouseDownEvent );
				MouseMove -= new System.Windows.Forms.MouseEventHandler( MouseMoveEvent );

				if( value ) {
					MouseDown += new System.Windows.Forms.MouseEventHandler( MouseDownEvent );
					MouseMove += new System.Windows.Forms.MouseEventHandler( MouseMoveEvent );
				}
			}
		}

		/// <summary>
		/// レイヤードウィンドウの透過度（0-255）を取得、設定します。
		/// </summary>
		internal int Alpha {
			get {
				return BlendFunction.SourceConstantAlpha;
			}
			set {
				BlendFunction.SourceConstantAlpha = ( 0 <= value && value <= 255 ) ? (byte)value : (byte)255;
			}
		}

		/// <summary>
		/// コントロールを識別するハンドルを作成するときに必要な作成パラメータを格納している <see cref="System.Windows.Forms.CreateParams"/> 。
		/// </summary>
		protected override System.Windows.Forms.CreateParams CreateParams {
			get {
				System.Windows.Forms.CreateParams createParam = base.CreateParams;
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
				if( this.OffScreenCache == null ) {
					this.OffScreenCache = new System.Drawing.Bitmap( this.Size.Width, this.Size.Height );
				}

				return this.OffScreenCache;
			}
			set {
				this.OffScreenCache = value;
			}
		}

		/// <summary>
		/// <see cref="MMFrame.Forms.LayeredWindows.LayeredWindowSurface"/> のコンストラクタ
		/// </summary>
		protected LayeredWindowSurface() {
			this.SuspendLayout();
			this.ShowInTaskbar = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.ResumeLayout( false );
			this.AllowDragMove = true;
			this.BlendFunction = new API.BLENDFUNCTION( 0, 0, 255, 1 );
			System.IntPtr dummy = this.Handle;
		}

		/// <summary>
		/// <see cref="System.Drawing.Bitmap"/> をオフスクリーンに書き込みます。
		/// </summary>
		/// <param name="srcBitmap">オフスクリーンに書き込む <see cref="System.Drawing.Bitmap"/></param>
		internal void DrawImage(System.Drawing.Bitmap srcBitmap) {
			using( System.Drawing.Graphics g = System.Drawing.Graphics.FromImage( this.OffScreen ) ) {
				g.DrawImage( srcBitmap, 0, 0, srcBitmap.Width, srcBitmap.Height );
			};
		}

		/// <summary>
		/// <see cref="System.Drawing.Bitmap"/> をオフスクリーンに書き込みます。
		/// </summary>
		/// <param name="srcBitmap">オフスクリーンに書き込む <see cref="System.Drawing.Bitmap"/></param>
		/// <param name="alpha"><paramref name="srcBitmap"/> の透過度（0-255）</param>
		internal void DrawImage(System.Drawing.Bitmap srcBitmap, int alpha) {
			System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix();
			cm.Matrix00 = 1;
			cm.Matrix11 = 1;
			cm.Matrix22 = 1;
			cm.Matrix33 = (float)alpha / 255F;
			cm.Matrix44 = 1;

			using( System.Drawing.Graphics g = System.Drawing.Graphics.FromImage( this.OffScreen ) )
			using( System.Drawing.Imaging.ImageAttributes ia = new System.Drawing.Imaging.ImageAttributes() ) {
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
		internal void DrawText(string text, System.Drawing.Font font, System.Drawing.Brush color, System.Drawing.Pen outlineColor, System.Drawing.RectangleF rectangle) {
			using( System.Drawing.Graphics g = System.Drawing.Graphics.FromImage( this.OffScreen ) ) {
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

				if( outlineColor == null ) {
					g.FillRectangle( System.Drawing.Brushes.Transparent, rectangle );
					g.DrawString( text, font, color, rectangle );
				} else {
					using( System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath() ) {
						float sizeInPixels = font.SizeInPoints * g.DpiY / 72;
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
			System.IntPtr screen = System.IntPtr.Zero;
			System.IntPtr memScreen = System.IntPtr.Zero;
			System.IntPtr gdiBitmap = System.IntPtr.Zero;
			System.IntPtr oldBitmap = System.IntPtr.Zero;

			try {
				screen = API.GetDC( System.IntPtr.Zero );
				memScreen = API.CreateCompatibleDC( screen );
				gdiBitmap = this.OffScreen.GetHbitmap( System.Drawing.Color.FromArgb( 0 ) );
				oldBitmap = API.SelectObject( memScreen, gdiBitmap );

				System.Drawing.Size size = this.Size;
				System.Drawing.Point pointSource = new System.Drawing.Point( 0, 0 );
				System.Drawing.Point topPos = this.Location;

				InvokeWithThreadSafe( () => {
					API.UpdateLayeredWindow( this.Handle, screen, ref topPos, ref size, memScreen, ref pointSource, 0, ref BlendFunction, 2 );
				} );
			} finally {
				API.ReleaseDC( System.IntPtr.Zero, screen );
				API.SelectObject( memScreen, oldBitmap );
				API.DeleteObject( gdiBitmap );
				API.DeleteDC( memScreen );
				this.OffScreen.Dispose();
				this.OffScreen = null;
			}
		}

		/// <summary>
		/// スレッドセーフでコードを実行します。
		/// </summary>
		/// <param name="action">スレッドセーフで実行する <see cref="System.Action"/></param>
		protected void InvokeWithThreadSafe(System.Action action) {
			if( this.InvokeRequired ) {
				this.Invoke( (System.Windows.Forms.MethodInvoker)delegate () {
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
		protected override void Dispose(bool disposing) {
			if( disposing ) {
				this.AllowDragMove = false;

				if( this.OffScreen != null ) {
					this.OffScreen.Dispose();
					this.OffScreen = null;
				}

				if( this.Components != null ) {
					Components.Dispose();
					Components = null;
				}

				if( this.ContextMenuStrip != null ) {
					this.ContextMenuStrip.Dispose();
					this.ContextMenuStrip = null;
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
		private void MouseDownEvent(object sender, System.Windows.Forms.MouseEventArgs e) {
			if( ( e.Button & System.Windows.Forms.MouseButtons.Left ) == System.Windows.Forms.MouseButtons.Left ) {
				this.MousePoint = new System.Drawing.Point( e.X, e.Y );
			}
		}

		/// <summary>
		/// マウス移動時のイベント
		/// </summary>
		/// <param name="sender"><see cref="System.Object"/></param>
		/// <param name="e"><see cref="System.Windows.Forms.MouseEventArgs"/></param>
		private void MouseMoveEvent(object sender, System.Windows.Forms.MouseEventArgs e) {
			if( ( e.Button & System.Windows.Forms.MouseButtons.Left ) == System.Windows.Forms.MouseButtons.Left ) {
				this.Location = new System.Drawing.Point( this.Location.X + e.X - this.MousePoint.X, this.Location.Y + e.Y - this.MousePoint.Y );
			}
		}
	}
}
