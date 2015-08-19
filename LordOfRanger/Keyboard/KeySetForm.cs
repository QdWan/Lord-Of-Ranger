using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LordOfRanger.Keyboard {
	/// <summary>
	/// キーを設定するフォーム
	/// keyTypeがSINGLEの場合は1つだけ
	/// keyTypeがMULTIの場合は複数のキーを設定でき、入力されたキーはkeyDataから取得できる
	/// </summary>
	internal partial class KeySetForm :Form {

		private List<byte> _keyList = new List<byte>();
		internal Result result;
		internal KeyType keyType;

		internal KeySetForm() {
			this.result = Result.CANCEL;
			this.keyType = KeyType.SINGLE;
			KeyPreview = true;
			InitializeComponent();
		}

		internal enum Result {
			OK,
			CANCEL
		};

		internal enum KeyType {
			SINGLE,
			MULTI
		}

		internal byte[] KeyData {
			get {
				if( this._keyList.Count > 0 ) {
					return this._keyList.ToArray();
				}
				return new byte[] { 0x00 };
			}
		}

		private void KeySetForm_KeyUp( object sender, KeyEventArgs e ) {
			var keycode = (byte)e.KeyCode;
			if( keycode == (byte)Keys.Return ) {
				this.result = Result.OK;
				Close();
				return;
			} else if( keycode == (byte)Keys.Escape && this._keyList.Count == 0 ) {
				this.result = Result.CANCEL;
				Close();
				return;
			}
			if( e.Shift ) {
				if( (byte)Keys.F1 <= keycode && keycode <= (byte)Keys.F12 ) {
					keycode += 12;
				}
			}
			switch( this.keyType ) {
				case KeyType.SINGLE:
					this._keyList.Clear();
					if( keycode == (byte)Keys.Escape ) {
						this.lblInputKey.Text = "Press Any Key";
						return;
					}
					this.lblInputKey.Text = Key.KEY_TEXT[keycode];
					this._keyList.Add( keycode );
					break;
				case KeyType.MULTI:
					if( (byte)e.KeyCode == (byte)Keys.Escape ) {
						this.lblInputKey.Text = "Press Any Key";
						this._keyList.Clear();
						return;
					}
					if( this._keyList.Count == 0 ) {
						this._keyList.Add( keycode );
						this.lblInputKey.Text = Key.KEY_TEXT[keycode];
					} else {
						this._keyList.Add( keycode );
						this.lblInputKey.Text = this.lblInputKey.Text + " + " + Key.KEY_TEXT[keycode];
					}
					break;
			}
		}

		private void btnCancel_Click( object sender, EventArgs e ) {
			this._keyList.Clear();
			this.result = Result.CANCEL;
			Close();
		}

		private void btnOk_Click( object sender, EventArgs e ) {
			this.result = Result.OK;
			Close();
		}

		class NotForcusButton :Button {
			public NotForcusButton() {
				SetStyle( ControlStyles.Selectable, false );
			}
		}
	}
}
