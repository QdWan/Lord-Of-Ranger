namespace LordOfRanger.Keyboard {
	partial class KeySetForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if( disposing && ( this.components != null ) ) {
				this.components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.btnOk = new LordOfRanger.Keyboard.KeySetForm.NotForcusButton();
			this.btnCancel = new LordOfRanger.Keyboard.KeySetForm.NotForcusButton();
			this.lblInputKey = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(19, 67);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.TabStop = false;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(113, 67);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.TabStop = false;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblInputKey
			// 
			this.lblInputKey.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInputKey.Location = new System.Drawing.Point(0, 0);
			this.lblInputKey.Name = "lblInputKey";
			this.lblInputKey.Size = new System.Drawing.Size(224, 64);
			this.lblInputKey.TabIndex = 2;
			this.lblInputKey.Text = "Press Any Key";
			this.lblInputKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// KeySetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(224, 95);
			this.Controls.Add(this.lblInputKey);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "KeySetForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeySetForm_FormClosed);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblInputKey;
		private NotForcusButton btnOk;
		private NotForcusButton btnCancel;
	}
}