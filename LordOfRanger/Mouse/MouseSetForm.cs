using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LordOfRanger.Arad;

namespace LordOfRanger.Mouse {
	/// <summary>
	/// マウス操作を設定するフォーム
	/// 設定された操作はmouseDataから取得できる
	/// </summary>
	internal partial class MouseSetForm :Form {

		internal readonly MouseData mouseData;
		internal Result result;
		private MouseHook _mouseHook;
		private bool _autoInputFlag;
		private int _autoInputRowIndex;
		internal bool editedFlag;

		internal MouseSetForm() : this(new MouseData()) {}

		internal MouseSetForm(MouseData mouseData) {
			this.mouseData = mouseData;
			this.result = Result.CANCEL;
			KeyPreview = true;
			InitializeComponent();
		}


		internal enum Result {
			OK,
			CANCEL
		};
		private struct DgvCol {

			internal const string OPERATION = "dgvColOp";
			internal const string X = "dgvColX";
			internal const string Y = "dgvColY";
			internal const string SLEEP_BETWEEN = "dgvColSleepBetween";
			internal const string SLEEP_AFTER = "dgvColSleepAfter";
			internal const string AUTO_INPUT = "dgvColAutoInput";
			internal const string DELETE = "dgvColDelete";

		}

		private struct MouseOperationText {

			internal const string LEFT = "左クリック";
			internal const string RIGHT = "右クリック";
			internal const string MOVE = "移動";

		}

		private void MouseSetForm_Load( object sender, EventArgs e ) {
			foreach( var mouse in this.mouseData.Value ) {
				string op;
				switch( mouse.op ) {
					case Operation.LEFT:
						op = MouseOperationText.LEFT;
						break;
					case Operation.RIGHT:
						op = MouseOperationText.RIGHT;
						break;
					case Operation.MOVE:
						op = MouseOperationText.MOVE;
						break;
					default:
						continue;
				}
				var row = this.dgv.Rows.Add();
				this.dgv.Rows[row].Cells[DgvCol.OPERATION].Value = op;
				this.dgv.Rows[row].Cells[DgvCol.X].Value = mouse.x;
				this.dgv.Rows[row].Cells[DgvCol.Y].Value = mouse.y;
				this.dgv.Rows[row].Cells[DgvCol.SLEEP_BETWEEN].Value = mouse.sleepBetween;
				this.dgv.Rows[row].Cells[DgvCol.SLEEP_AFTER].Value = mouse.sleepAfter;
			}
			this._mouseHook = new MouseHook();
			this._mouseHook.MouseHooked += AutoInput;
			this.cmbSwitch.SelectedIndex = (int)this.mouseData.SwitchState;
			this.editedFlag = false;
		}
		private void MouseSetForm_FormClosed( object sender, FormClosedEventArgs e ) {
			this._mouseHook.MouseHooked -= AutoInput;

		}


		private void btnCancel_Click( object sender, EventArgs e ) {
			this.result = Result.CANCEL;
			Close();
		}

		private void btnOk_Click( object sender, EventArgs e ) {
			this.dgv.EndEdit();

			this.mouseData.SwitchState = (SwitchingStyle)this.cmbSwitch.SelectedIndex;

			foreach( DataGridViewRow row in this.dgv.Rows ) {
				uint i;
				if( !uint.TryParse(row.Cells[DgvCol.X].Value.ToString(),out i) ||
					!uint.TryParse( row.Cells[DgvCol.Y].Value.ToString(), out i ) ||
					!uint.TryParse( row.Cells[DgvCol.SLEEP_BETWEEN].Value.ToString(), out i ) ||
					!uint.TryParse( row.Cells[DgvCol.SLEEP_AFTER].Value.ToString(), out i ) ||
					(
						(string)row.Cells[DgvCol.OPERATION].EditedFormattedValue != MouseOperationText.LEFT &&
						(string)row.Cells[DgvCol.OPERATION].EditedFormattedValue != MouseOperationText.RIGHT &&
						(string)row.Cells[DgvCol.OPERATION].EditedFormattedValue != MouseOperationText.MOVE
					)
				) {
					MessageBox.Show( "未入力項目があります。" );
					return;
				}
			}

			this.result = Result.OK;
			var tmpList = new List<ActionPattern>();
			foreach( DataGridViewRow row in this.dgv.Rows ) {
				var x = int.Parse( row.Cells[DgvCol.X].Value.ToString() );
				var y = int.Parse( row.Cells[DgvCol.Y].Value.ToString() );
				var sleepBetween = int.Parse( row.Cells[DgvCol.SLEEP_BETWEEN].Value.ToString() );
				var sleepAfter = int.Parse( row.Cells[DgvCol.SLEEP_AFTER].Value.ToString() );
				var cb = (DataGridViewComboBoxCell)row.Cells[DgvCol.OPERATION];
				Operation op;
				switch( (string)cb.EditedFormattedValue ) {
					case MouseOperationText.LEFT:
						op = Operation.LEFT;
						break;
					case MouseOperationText.RIGHT:
						op = Operation.RIGHT;
						break;
					case MouseOperationText.MOVE:
						op = Operation.MOVE;
						break;
					default:
						continue;
				}
				tmpList.Add( new ActionPattern( op, x, y, sleepBetween, sleepAfter ) );
			}
			this.mouseData.Value = tmpList;
			Close();
		}

		class NotForcusButton :Button {
			public NotForcusButton() {
				SetStyle( ControlStyles.Selectable, false );
			}
		}
		private void dgv_CellEnter( object sender,DataGridViewCellEventArgs e ) {
			if( this.dgv.SelectedCells.Count == 0 ) {
				return;
			}
			if( this.dgv.Columns[this.dgv.SelectedCells[0].ColumnIndex].Name == DgvCol.OPERATION ) {
				this.dgv.BeginEdit( true );
				var edt = this.dgv.EditingControl as DataGridViewComboBoxEditingControl;
				if( edt != null ) {
					edt.DroppedDown = true;
				}
			}
		}

		private void btnAdd_Click( object sender, EventArgs e ) {
			var row = this.dgv.Rows.Add();
			this.dgv.Rows[row].Cells[DgvCol.OPERATION].Value = "";
			this.dgv.Rows[row].Cells[DgvCol.X].Value = "";
			this.dgv.Rows[row].Cells[DgvCol.Y].Value = "";
			this.dgv.Rows[row].Cells[DgvCol.SLEEP_BETWEEN].Value = "";
			this.dgv.Rows[row].Cells[DgvCol.SLEEP_AFTER].Value = "";
		}

		private void dgv_CellContentClick( object sender, DataGridViewCellEventArgs e ) {
			if( this.dgv.SelectedCells.Count != 1 ) {
				return;
			}
			switch( this.dgv.SelectedCells[0].OwningColumn.Name ) {
				case DgvCol.DELETE:
					if( MessageBox.Show( "この行を削除しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 ) == DialogResult.Yes ) {
						this.dgv.Rows.RemoveAt( this.dgv.SelectedCells[0].OwningRow.Index );
					}
					break;
				case DgvCol.AUTO_INPUT:
					this._autoInputFlag = true;
					this._autoInputRowIndex = this.dgv.SelectedCells[0].OwningRow.Index;
					this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.X].Style.BackColor = Color.LavenderBlush;
					this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.Y].Style.BackColor = Color.LavenderBlush;
					this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.OPERATION].Style.BackColor = Color.LavenderBlush;
					break;
				default:
					return;
			}
		}

		/// <summary>
		/// データグリッドビューの変更を検知し、フラグを立てておきメインフォームに伝える。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgv_CellValueChanged( object sender, DataGridViewCellEventArgs e ) {
			this.editedFlag = true;
		}

		private void AutoInput( object sender, MouseHookedEventArgs e ) {
			if( !this._autoInputFlag ) {
				return;
			}
			switch( e.Message ) {
				case MouseMessage.LUp:
				case MouseMessage.RUp:
					if( !Client.IsAlive || !Client.IsActiveWindow ) {
						return;
					}
					this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.X].Value = Math.Round(e.Point.X / Client.ratioW) - Client.x;
					this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.Y].Value = Math.Round( e.Point.Y / Client.ratioH) - Client.y;
					this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.OPERATION].Value = e.Message == MouseMessage.LUp ? MouseOperationText.LEFT : MouseOperationText.RIGHT;
					break;
				case MouseMessage.Move:
				case MouseMessage.LDown:
				case MouseMessage.RDown:
				case MouseMessage.MDown:
				case MouseMessage.MUp:
				case MouseMessage.Wheel:
				case MouseMessage.XDown:
				case MouseMessage.XUp:
					return;
				default:
					throw new ArgumentOutOfRangeException();
			}
			this._autoInputFlag = false;
			this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.X].Style.BackColor = Color.White;
			this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.Y].Style.BackColor = Color.White;
			this.dgv.Rows[this._autoInputRowIndex].Cells[DgvCol.OPERATION].Style.BackColor = Color.White;
		}

		private void dgv_EditingControlShowing( object sender, DataGridViewEditingControlShowingEventArgs e ) {
			var control = e.Control as DataGridViewTextBoxEditingControl;
			if( control != null ) {
				control.KeyPress += dgvTextBox_KeyPress;
			}
		}
		private static void dgvTextBox_KeyPress( object sender, KeyPressEventArgs e ) {
			if( (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 13 || e.KeyChar == '\b' ) {
				return;
			}
			e.Handled = true;
		}

		private void btnUp_Click( object sender, EventArgs e ) {
			var rowIndex = this.dgv.SelectedCells[0].OwningRow.Index;
			if( rowIndex >= 1 ) {
				this.editedFlag = true;
				var row = this.dgv.Rows[rowIndex];
				this.dgv.Rows.RemoveAt( rowIndex );
				this.dgv.Rows.Insert( rowIndex - 1, row );
				this.dgv.ClearSelection();
				this.dgv.Rows[rowIndex - 1].Selected = true;
			}
		}

		private void btnDown_Click( object sender, EventArgs e ) {
			var rowIndex = this.dgv.SelectedCells[0].OwningRow.Index;
			if( rowIndex < this.dgv.Rows.Count - 1 ) {
				this.editedFlag = true;
				var row = this.dgv.Rows[rowIndex];
				this.dgv.Rows.RemoveAt( rowIndex );
				this.dgv.Rows.Insert( rowIndex + 1, row );
				this.dgv.ClearSelection();
				this.dgv.Rows[rowIndex + 1].Selected = true;
			}
		}

		private void cmbSwitch_SelectedIndexChanged( object sender, EventArgs e ) {
			this.editedFlag = true;
		}
	}
}
