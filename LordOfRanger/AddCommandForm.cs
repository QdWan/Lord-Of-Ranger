using System;
using System.Windows.Forms;

namespace LordOfRanger {
	internal partial class AddCommandForm : Form {
		internal Result result;
		internal Type type;
		internal AddCommandForm() {
			InitializeComponent();

			result = Result.CANCEL;
			KeyPreview = true;
		}

		internal enum Result {
			OK,
			CANCEL
		};

		internal enum Type {
			COMMAND,
			BARRAGE,
			TOGGLE
		}

		private void btnOk_Click(object sender, EventArgs e) {
			if( rbCommand.Checked ) {
				type = Type.COMMAND;
			} else if( rbBarrage.Checked ) {
				type = Type.BARRAGE;
			} else if( rbToggle.Checked ) {
				type = Type.TOGGLE;
			} else {
				MessageBox.Show( "Please select the type." );
				return;
			}
			result = Result.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void AddCommandForm_KeyUp(object sender, KeyEventArgs e) {
			if( (byte)e.KeyCode == (byte)RamGecTools.KeyboardHook.VKeys.ESCAPE ) {
				Close();
			} else if( (byte)e.KeyCode == (byte)RamGecTools.KeyboardHook.VKeys.RETURN ) {
				if( rbCommand.Checked ) {
					type = Type.COMMAND;
				} else if( rbBarrage.Checked ) {
					type = Type.BARRAGE;
				} else if( rbToggle.Checked ) {
					type = Type.TOGGLE;
				} else {
					MessageBox.Show( "Please select the type." );
					return;
				}
				result = Result.OK;
				Close();
			}
		}
	}
}
