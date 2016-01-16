namespace LordOfRanger {
	partial class SkillIconExtractorForm {
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
			this.btnExtract = new System.Windows.Forms.Button();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtDirectory = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnExtract
			// 
			this.btnExtract.Location = new System.Drawing.Point(2, 65);
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(75, 23);
			this.btnExtract.TabIndex = 0;
			this.btnExtract.Text = "Extract!";
			this.btnExtract.UseVisualStyleBackColor = true;
			this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(3, 27);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtDirectory
			// 
			this.txtDirectory.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtDirectory.Location = new System.Drawing.Point(0, 0);
			this.txtDirectory.Name = "txtDirectory";
			this.txtDirectory.ReadOnly = true;
			this.txtDirectory.Size = new System.Drawing.Size(300, 19);
			this.txtDirectory.TabIndex = 2;
			this.txtDirectory.TabStop = false;
			this.txtDirectory.Text = "C:\\Nexon\\ARAD\\ImagePacks2";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnBrowse);
			this.splitContainer1.Panel2.Controls.Add(this.btnExtract);
			this.splitContainer1.Size = new System.Drawing.Size(407, 106);
			this.splitContainer1.SplitterDistance = 300;
			this.splitContainer1.TabIndex = 3;
			this.splitContainer1.TabStop = false;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.txtDirectory);
			this.splitContainer2.Size = new System.Drawing.Size(300, 106);
			this.splitContainer2.SplitterDistance = 25;
			this.splitContainer2.TabIndex = 3;
			this.splitContainer2.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(260, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "NPKファイルの入っているディレクトリを選択してください。";
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.lblStatus);
			this.splitContainer3.Panel2.Controls.Add(this.progressBar1);
			this.splitContainer3.Panel2.Controls.Add(this.statusStrip1);
			this.splitContainer3.Size = new System.Drawing.Size(407, 135);
			this.splitContainer3.SplitterDistance = 106;
			this.splitContainer3.TabIndex = 4;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(3, 2);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(114, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 1;
			this.progressBar1.Visible = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 3);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(407, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(122, 8);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 12);
			this.lblStatus.TabIndex = 2;
			// 
			// SkillIconExtractorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(407, 135);
			this.Controls.Add(this.splitContainer3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SkillIconExtractorForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnExtract;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtDirectory;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label lblStatus;
	}
}