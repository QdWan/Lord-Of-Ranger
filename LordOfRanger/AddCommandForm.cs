using System;
using System.Windows.Forms;

namespace LordOfRanger {
	/// <summary>
	/// 行追加時に、コマンド、連打切替、連打を選択するフォーム
	/// </summary>
	internal partial class AddCommandForm :Form {
		internal Result result;
		internal Type type;
		internal AddCommandForm() {
			InitializeComponent();

			this.result = Result.CANCEL;
			KeyPreview = true;
		}

		internal enum Result {
			OK,
			CANCEL
		}

		internal enum Type {
			COMMAND,
			BARRAGE,
			TOGGLE,
			MOUSE
		}

		private void btnOk_Click( object sender, EventArgs e ) {
			if( this.rbCommand.Checked ) {
				this.type = Type.COMMAND;
			} else if( this.rbBarrage.Checked ) {
				this.type = Type.BARRAGE;
			} else if( this.rbToggle.Checked ) {
				this.type = Type.TOGGLE;
			} else if( this.rbMouse.Checked ) {
				this.type = Type.MOUSE;
			} else {
				MessageBox.Show( "モードを選択してください。" );
				return;
			}
			this.result = Result.OK;
			Close();
		}

		private void btnCancel_Click( object sender, EventArgs e ) {
			Close();
		}

		private void AddCommandForm_KeyUp( object sender, KeyEventArgs e ) {
			switch( (byte)e.KeyCode ) {
				case (byte)Keys.Escape:
					Close();
					break;
				case (byte)Keys.Return:
					if( this.rbCommand.Checked ) {
						this.type = Type.COMMAND;
					} else if( this.rbBarrage.Checked ) {
						this.type = Type.BARRAGE;
					} else if( this.rbToggle.Checked ) {
						this.type = Type.TOGGLE;
					} else if( this.rbMouse.Checked ) {
						this.type = Type.MOUSE;
					} else {
						MessageBox.Show( "モードを選択してください。" );
						return;
					}
					this.result = Result.OK;
					Close();
					break;
				default:
					return;
			}
		}
	}
}
