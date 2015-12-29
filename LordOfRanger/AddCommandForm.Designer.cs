namespace LordOfRanger {
	partial class AddCommandForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.rbToggle = new System.Windows.Forms.RadioButton();
			this.rbBarrage = new System.Windows.Forms.RadioButton();
			this.rbCommand = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(12, 82);
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
			this.btnCancel.Location = new System.Drawing.Point(93, 82);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.TabStop = false;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.rbToggle);
			this.panel1.Controls.Add(this.rbBarrage);
			this.panel1.Controls.Add(this.rbCommand);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(177, 117);
			this.panel1.TabIndex = 2;
			// 
			// rbToggle
			// 
			this.rbToggle.AutoSize = true;
			this.rbToggle.Location = new System.Drawing.Point(20, 56);
			this.rbToggle.Name = "rbToggle";
			this.rbToggle.Size = new System.Drawing.Size(71, 16);
			this.rbToggle.TabIndex = 4;
			this.rbToggle.Text = "連打切替";
			this.rbToggle.UseVisualStyleBackColor = true;
			// 
			// rbBarrage
			// 
			this.rbBarrage.AutoSize = true;
			this.rbBarrage.Location = new System.Drawing.Point(20, 34);
			this.rbBarrage.Name = "rbBarrage";
			this.rbBarrage.Size = new System.Drawing.Size(47, 16);
			this.rbBarrage.TabIndex = 3;
			this.rbBarrage.Text = "連打";
			this.rbBarrage.UseVisualStyleBackColor = true;
			// 
			// rbCommand
			// 
			this.rbCommand.AutoSize = true;
			this.rbCommand.Location = new System.Drawing.Point(20, 12);
			this.rbCommand.Name = "rbCommand";
			this.rbCommand.Size = new System.Drawing.Size(58, 16);
			this.rbCommand.TabIndex = 2;
			this.rbCommand.Text = "コマンド";
			this.rbCommand.UseVisualStyleBackColor = true;
			// 
			// AddCommandForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(177, 117);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddCommandForm";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AddCommandForm_KeyUp);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rbToggle;
		private System.Windows.Forms.RadioButton rbBarrage;
		private System.Windows.Forms.RadioButton rbCommand;
	}
}