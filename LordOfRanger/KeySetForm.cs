using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RamGecTools;

namespace LordOfRanger {
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
			if( keycode == (byte)RamGecTools.KeyboardHook.VKeys.SHIFT ) {
				return;
			}
			if( keycode == (byte)RamGecTools.KeyboardHook.VKeys.RETURN ) {
				result = Result.OK;
				Close();
				return;
			} else if( keycode == (byte)RamGecTools.KeyboardHook.VKeys.ESCAPE && keyList.Count == 0 ) {
				result = Result.CANCEL;
				Close();
				return;
			}
			if( e.Shift ) {
				if( (byte)RamGecTools.KeyboardHook.VKeys.F1 <= keycode && keycode <= (byte)RamGecTools.KeyboardHook.VKeys.F12 ) {
					keycode += 12;
				}
			}
			switch( keyType ) {
				case KeyType.SINGLE:
					keyList.Clear();
					if( keycode == (byte)RamGecTools.KeyboardHook.VKeys.ESCAPE ) {
						lblInputKey.Text = "Press Any Key";
						return;
					}
					lblInputKey.Text = Key.keyText[keycode];
					keyList.Add( keycode );
					break;
				case KeyType.MULTI:
					if( (byte)e.KeyCode == (byte)RamGecTools.KeyboardHook.VKeys.ESCAPE ) {
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
