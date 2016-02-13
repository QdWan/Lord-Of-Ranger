namespace LordOfRanger.Mouse {
	partial class MouseSetForm {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.dgv = new System.Windows.Forms.DataGridView();
			this.dgvColOp = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dgvColX = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColY = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColSleepBetween = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColSleepAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColAutoInput = new System.Windows.Forms.DataGridViewButtonColumn();
			this.dgvColDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.btnOk = new LordOfRanger.Mouse.MouseSetForm.NotForcusButton();
			this.btnCancel = new LordOfRanger.Mouse.MouseSetForm.NotForcusButton();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnOk);
			this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
			this.splitContainer1.Size = new System.Drawing.Size(561, 533);
			this.splitContainer1.SplitterDistance = 492;
			this.splitContainer1.TabIndex = 2;
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
			this.splitContainer2.Panel1.Controls.Add(this.btnDown);
			this.splitContainer2.Panel1.Controls.Add(this.btnUp);
			this.splitContainer2.Panel1.Controls.Add(this.btnAdd);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.dgv);
			this.splitContainer2.Size = new System.Drawing.Size(561, 492);
			this.splitContainer2.SplitterDistance = 34;
			this.splitContainer2.TabIndex = 0;
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(533, 6);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(22, 23);
			this.btnDown.TabIndex = 2;
			this.btnDown.Text = "下";
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(507, 6);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(22, 23);
			this.btnUp.TabIndex = 1;
			this.btnUp.Text = "上";
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(425, 6);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "行追加";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AllowUserToResizeColumns = false;
			this.dgv.AllowUserToResizeRows = false;
			this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColOp,
            this.dgvColX,
            this.dgvColY,
            this.dgvColSleepBetween,
            this.dgvColSleepAfter,
            this.dgvColAutoInput,
            this.dgvColDelete});
			this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgv.Location = new System.Drawing.Point(0, 0);
			this.dgv.Name = "dgv";
			this.dgv.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
			this.dgv.RowTemplate.Height = 21;
			this.dgv.Size = new System.Drawing.Size(561, 454);
			this.dgv.TabIndex = 8;
			this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
			this.dgv.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEnter);
			this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
			this.dgv.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
			// 
			// dgvColOp
			// 
			this.dgvColOp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dgvColOp.HeaderText = "オペレーション";
			this.dgvColOp.Items.AddRange(new object[] {
            "左クリック",
            "右クリック",
            "移動"});
			this.dgvColOp.Name = "dgvColOp";
			// 
			// dgvColX
			// 
			this.dgvColX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dgvColX.HeaderText = "X座標";
			this.dgvColX.Name = "dgvColX";
			this.dgvColX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dgvColX.Width = 38;
			// 
			// dgvColY
			// 
			this.dgvColY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dgvColY.HeaderText = "Y座標";
			this.dgvColY.Name = "dgvColY";
			this.dgvColY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dgvColY.Width = 38;
			// 
			// dgvColSleepBetween
			// 
			this.dgvColSleepBetween.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dgvColSleepBetween.HeaderText = "マウスクリックから離すまでの時間";
			this.dgvColSleepBetween.Name = "dgvColSleepBetween";
			this.dgvColSleepBetween.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dgvColSleepBetween.Width = 88;
			// 
			// dgvColSleepAfter
			// 
			this.dgvColSleepAfter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dgvColSleepAfter.HeaderText = "次の操作までの待機時間";
			this.dgvColSleepAfter.Name = "dgvColSleepAfter";
			this.dgvColSleepAfter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dgvColSleepAfter.Width = 77;
			// 
			// dgvColAutoInput
			// 
			this.dgvColAutoInput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.NullValue = "自動入力";
			this.dgvColAutoInput.DefaultCellStyle = dataGridViewCellStyle1;
			this.dgvColAutoInput.HeaderText = "自動入力";
			this.dgvColAutoInput.Name = "dgvColAutoInput";
			this.dgvColAutoInput.Text = "自動入力";
			// 
			// dgvColDelete
			// 
			this.dgvColDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.NullValue = "削除";
			this.dgvColDelete.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvColDelete.FillWeight = 92.4663F;
			this.dgvColDelete.HeaderText = "削除";
			this.dgvColDelete.Name = "dgvColDelete";
			this.dgvColDelete.Text = "削除";
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(372, 7);
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
			this.btnCancel.Location = new System.Drawing.Point(466, 7);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.TabStop = false;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// MouseSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 533);
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MouseSetForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MouseSetForm_FormClosed);
			this.Load += new System.EventHandler(this.MouseSetForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private NotForcusButton btnOk;
		private NotForcusButton btnCancel;
		private System.Windows.Forms.SplitContainer splitContainer2;
		internal System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.DataGridViewComboBoxColumn dgvColOp;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColX;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColY;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSleepBetween;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSleepAfter;
		private System.Windows.Forms.DataGridViewButtonColumn dgvColAutoInput;
		private System.Windows.Forms.DataGridViewButtonColumn dgvColDelete;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
	}
}