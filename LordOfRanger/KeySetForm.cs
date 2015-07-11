using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LordOfRanger {
	/// <summary>
	/// キーを設定するフォーム
	/// keyTypeがSINGLEの場合は1つだけ
	/// keyTypeがMULTIの場合は複数のキーを設定でき、入力されたキーはkeyDataから取得できる
	/// </summary>
	internal partial class KeySetForm : Form {

		private List<byte> keyList = new List<byte>();
		internal Result result;
		internal KeyType keyType;

		internal KeySetForm() {
			result = Result.CANCEL;
			keyType = KeyType.SINGLE;
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

		internal byte[] keyData {
			get {
				if( keyList.Count > 0 ) {
					return keyList.ToArray();
				} else {
					return new byte[1] { 0x00 };
				}
			}
		}

		private void KeySetForm_KeyUp(object sender, KeyEventArgs e) {
			byte keycode = (byte)e.KeyCode;
			if( keycode == (byte)Keys.Return ) {
				result = Result.OK;
				Close();
				return;
			} else if( keycode == (byte)Keys.Escape && keyList.Count == 0 ) {
				result = Result.CANCEL;
				Close();
				return;
			}
			if( e.Shift ) {
				if( (byte)Keys.F1 <= keycode && keycode <= (byte)Keys.F12 ) {
					keycode += 12;
				}
			}
			switch( keyType ) {
				case KeyType.SINGLE:
					keyList.Clear();
					if( keycode == (byte)Keys.Escape ) {
						lblInputKey.Text = "Press Any Key";
						return;
					}
					lblInputKey.Text = Key.keyText[keycode];
					keyList.Add( keycode );
					break;
				case KeyType.MULTI:
					if( (byte)e.KeyCode == (byte)Keys.Escape ) {
						lblInputKey.Text = "Press Any Key";
						keyList.Clear();
						return;
					}
					if( keyList.Count == 0 ) {
						keyList.Add( keycode );
						lblInputKey.Text = Key.keyText[keycode];
					} else {
						keyList.Add( keycode );
						lblInputKey.Text = lblInputKey.Text + " + " + Key.keyText[keycode];
					}
					break;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			keyList.Clear();
			result = Result.CANCEL;
			Close();
		}

		private void btnOk_Click(object sender, EventArgs e) {
			result = Result.OK;
			Close();
		}
	}
}
