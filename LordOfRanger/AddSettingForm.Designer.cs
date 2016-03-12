namespace LordOfRanger {
	partial class AddSettingForm {
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
			this.txtSettingName = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtSettingName
			// 
			this.txtSettingName.Location = new System.Drawing.Point(12, 28);
			this.txtSettingName.Name = "txtSettingName";
			this.txtSettingName.Size = new System.Drawing.Size(169, 19);
			this.txtSettingName.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 53);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(63, 23);
			this.button1.TabIndex = 1;
			this.button1.TabStop = false;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(109, 53);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(63, 23);
			this.button2.TabIndex = 2;
			this.button2.TabStop = false;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "設定名";
			// 
			// AddSettingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(193, 90);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtSettingName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddSettingForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddSettingForm_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSettingName;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
	}
}