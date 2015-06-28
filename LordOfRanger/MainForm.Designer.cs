namespace LordOfRanger {
	partial class MainForm {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing) {
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.txtHotKey = new System.Windows.Forms.TextBox();
			this.btnHotKeyChange = new System.Windows.Forms.Button();
			this.btnAddRow = new System.Windows.Forms.Button();
			this.dgv = new System.Windows.Forms.DataGridView();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.lbSettingList = new System.Windows.Forms.ListBox();
			this.btnDeleteSetting = new System.Windows.Forms.Button();
			this.btnAddSetting = new System.Windows.Forms.Button();
			this.splitContainer5 = new System.Windows.Forms.SplitContainer();
			this.lblSettingName = new System.Windows.Forms.Label();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timerActiveWindowCheck = new System.Windows.Forms.Timer(this.components);
			this.timerBarrage = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.sETTINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skillIconExtractorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skillIcon = new System.Windows.Forms.DataGridViewImageColumn();
			this.disableSkillIcon = new System.Windows.Forms.DataGridViewImageColumn();
			this.mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.priority = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.push = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.send = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.up = new System.Windows.Forms.DataGridViewButtonColumn();
			this.down = new System.Windows.Forms.DataGridViewButtonColumn();
			this.delete = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
			this.splitContainer5.Panel1.SuspendLayout();
			this.splitContainer5.Panel2.SuspendLayout();
			this.splitContainer5.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dgv);
			this.splitContainer1.Size = new System.Drawing.Size(694, 608);
			this.splitContainer1.SplitterDistance = 39;
			this.splitContainer1.TabIndex = 8;
			this.splitContainer1.TabStop = false;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer3.IsSplitterFixed = true;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.label1);
			this.splitContainer3.Panel1.Controls.Add(this.txtHotKey);
			this.splitContainer3.Panel1.Controls.Add(this.btnHotKeyChange);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.btnAddRow);
			this.splitContainer3.Size = new System.Drawing.Size(694, 39);
			this.splitContainer3.SplitterDistance = 607;
			this.splitContainer3.TabIndex = 0;
			this.splitContainer3.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "HotKey:";
			// 
			// txtHotKey
			// 
			this.txtHotKey.Location = new System.Drawing.Point(57, 9);
			this.txtHotKey.Name = "txtHotKey";
			this.txtHotKey.ReadOnly = true;
			this.txtHotKey.Size = new System.Drawing.Size(96, 19);
			this.txtHotKey.TabIndex = 1;
			// 
			// btnHotKeyChange
			// 
			this.btnHotKeyChange.Location = new System.Drawing.Point(159, 6);
			this.btnHotKeyChange.Name = "btnHotKeyChange";
			this.btnHotKeyChange.Size = new System.Drawing.Size(67, 23);
			this.btnHotKeyChange.TabIndex = 0;
			this.btnHotKeyChange.Text = "change";
			this.btnHotKeyChange.UseVisualStyleBackColor = true;
			this.btnHotKeyChange.Click += new System.EventHandler(this.btnHotKeyChange_Click);
			// 
			// btnAddRow
			// 
			this.btnAddRow.Location = new System.Drawing.Point(3, 6);
			this.btnAddRow.Name = "btnAddRow";
			this.btnAddRow.Size = new System.Drawing.Size(75, 23);
			this.btnAddRow.TabIndex = 0;
			this.btnAddRow.Text = "Add Row";
			this.btnAddRow.UseVisualStyleBackColor = true;
			this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AllowUserToResizeColumns = false;
			this.dgv.AllowUserToResizeRows = false;
			this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
			this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.skillIcon,
            this.disableSkillIcon,
            this.mode,
            this.sequence,
            this.priority,
            this.push,
            this.send,
            this.up,
            this.down,
            this.delete});
			this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgv.Location = new System.Drawing.Point(0, 0);
			this.dgv.Name = "dgv";
			this.dgv.RowTemplate.Height = 21;
			this.dgv.Size = new System.Drawing.Size(694, 565);
			this.dgv.TabIndex = 7;
			this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
			this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(614, 2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(533, 2);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(0, 26);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
			this.splitContainer2.Panel1MinSize = 150;
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer5);
			this.splitContainer2.Size = new System.Drawing.Size(848, 640);
			this.splitContainer2.SplitterDistance = 150;
			this.splitContainer2.TabIndex = 9;
			this.splitContainer2.TabStop = false;
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer4.IsSplitterFixed = true;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.lbSettingList);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.btnDeleteSetting);
			this.splitContainer4.Panel2.Controls.Add(this.btnAddSetting);
			this.splitContainer4.Size = new System.Drawing.Size(150, 640);
			this.splitContainer4.SplitterDistance = 575;
			this.splitContainer4.TabIndex = 1;
			this.splitContainer4.TabStop = false;
			// 
			// lbSettingList
			// 
			this.lbSettingList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbSettingList.FormattingEnabled = true;
			this.lbSettingList.ItemHeight = 12;
			this.lbSettingList.Location = new System.Drawing.Point(0, 0);
			this.lbSettingList.Name = "lbSettingList";
			this.lbSettingList.Size = new System.Drawing.Size(150, 575);
			this.lbSettingList.TabIndex = 0;
			this.lbSettingList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSettingList_MouseDoubleClick);
			// 
			// btnDeleteSetting
			// 
			this.btnDeleteSetting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDeleteSetting.Location = new System.Drawing.Point(12, 34);
			this.btnDeleteSetting.Name = "btnDeleteSetting";
			this.btnDeleteSetting.Size = new System.Drawing.Size(125, 23);
			this.btnDeleteSetting.TabIndex = 1;
			this.btnDeleteSetting.Text = "Delete Setting";
			this.btnDeleteSetting.UseVisualStyleBackColor = true;
			this.btnDeleteSetting.Click += new System.EventHandler(this.btnDeleteSetting_Click);
			// 
			// btnAddSetting
			// 
			this.btnAddSetting.Location = new System.Drawing.Point(12, 6);
			this.btnAddSetting.Name = "btnAddSetting";
			this.btnAddSetting.Size = new System.Drawing.Size(125, 23);
			this.btnAddSetting.TabIndex = 0;
			this.btnAddSetting.Text = "Add Setting";
			this.btnAddSetting.UseVisualStyleBackColor = true;
			this.btnAddSetting.Click += new System.EventHandler(this.btnAddSetting_Click);
			// 
			// splitContainer5
			// 
			this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer5.Location = new System.Drawing.Point(0, 0);
			this.splitContainer5.Name = "splitContainer5";
			this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer5.Panel1
			// 
			this.splitContainer5.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitContainer5.Panel1.Controls.Add(this.lblSettingName);
			this.splitContainer5.Panel1.Controls.Add(this.btnCancel);
			this.splitContainer5.Panel1.Controls.Add(this.btnSave);
			// 
			// splitContainer5.Panel2
			// 
			this.splitContainer5.Panel2.Controls.Add(this.splitContainer1);
			this.splitContainer5.Size = new System.Drawing.Size(694, 640);
			this.splitContainer5.SplitterDistance = 28;
			this.splitContainer5.TabIndex = 9;
			this.splitContainer5.TabStop = false;
			// 
			// lblSettingName
			// 
			this.lblSettingName.AutoSize = true;
			this.lblSettingName.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblSettingName.Location = new System.Drawing.Point(11, 7);
			this.lblSettingName.Name = "lblSettingName";
			this.lblSettingName.Size = new System.Drawing.Size(0, 15);
			this.lblSettingName.TabIndex = 1;
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "ARAD";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(105, 26);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
			this.ExitToolStripMenuItem.Text = "EXIT";
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// timerActiveWindowCheck
			// 
			this.timerActiveWindowCheck.Interval = 500;
			this.timerActiveWindowCheck.Tick += new System.EventHandler(this.timerActiveWindowCheck_Tick);
			// 
			// timerBarrage
			// 
			this.timerBarrage.Interval = 30;
			this.timerBarrage.Tick += new System.EventHandler(this.timerBarrage_Tick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sETTINGToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(848, 26);
			this.menuStrip1.TabIndex = 10;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// sETTINGToolStripMenuItem
			// 
			this.sETTINGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem,
            this.skillIconExtractorToolStripMenuItem});
			this.sETTINGToolStripMenuItem.Name = "sETTINGToolStripMenuItem";
			this.sETTINGToolStripMenuItem.Size = new System.Drawing.Size(50, 22);
			this.sETTINGToolStripMenuItem.Text = "Tools";
			// 
			// optionToolStripMenuItem
			// 
			this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
			this.optionToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.optionToolStripMenuItem.Text = "Options...";
			this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
			// 
			// skillIconExtractorToolStripMenuItem
			// 
			this.skillIconExtractorToolStripMenuItem.Name = "skillIconExtractorToolStripMenuItem";
			this.skillIconExtractorToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.skillIconExtractorToolStripMenuItem.Text = "Skill IconExtractor";
			this.skillIconExtractorToolStripMenuItem.Click += new System.EventHandler(this.skillIconExtractorToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// skillIcon
			// 
			this.skillIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.skillIcon.Frozen = true;
			this.skillIcon.HeaderText = "Skill Icon";
			this.skillIcon.MinimumWidth = 50;
			this.skillIcon.Name = "skillIcon";
			this.skillIcon.Width = 50;
			// 
			// disableSkillIcon
			// 
			this.disableSkillIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.disableSkillIcon.Frozen = true;
			this.disableSkillIcon.HeaderText = "Disable Skill Icon";
			this.disableSkillIcon.MinimumWidth = 50;
			this.disableSkillIcon.Name = "disableSkillIcon";
			this.disableSkillIcon.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.disableSkillIcon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.disableSkillIcon.Width = 50;
			// 
			// mode
			// 
			this.mode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.mode.Frozen = true;
			this.mode.HeaderText = "Mode";
			this.mode.Name = "mode";
			this.mode.ReadOnly = true;
			this.mode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.mode.Width = 57;
			// 
			// sequence
			// 
			this.sequence.Frozen = true;
			this.sequence.HeaderText = "sequence";
			this.sequence.Name = "sequence";
			this.sequence.Visible = false;
			this.sequence.Width = 5;
			// 
			// priority
			// 
			this.priority.FillWeight = 47.41861F;
			this.priority.Frozen = true;
			this.priority.HeaderText = "Priority";
			this.priority.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
			this.priority.Name = "priority";
			this.priority.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.priority.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.priority.Visible = false;
			this.priority.Width = 5;
			// 
			// push
			// 
			this.push.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.push.FillWeight = 92.4663F;
			this.push.HeaderText = "Push Key";
			this.push.Name = "push";
			this.push.ReadOnly = true;
			this.push.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// send
			// 
			this.send.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.send.FillWeight = 92.4663F;
			this.send.HeaderText = "Send Key";
			this.send.Name = "send";
			this.send.ReadOnly = true;
			this.send.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// up
			// 
			this.up.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.NullValue = "Up";
			this.up.DefaultCellStyle = dataGridViewCellStyle1;
			this.up.HeaderText = "Up";
			this.up.MinimumWidth = 25;
			this.up.Name = "up";
			this.up.Text = "Up";
			this.up.Width = 25;
			// 
			// down
			// 
			this.down.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.NullValue = "Down";
			this.down.DefaultCellStyle = dataGridViewCellStyle2;
			this.down.HeaderText = "Down";
			this.down.Name = "down";
			this.down.Text = "Down";
			this.down.Width = 39;
			// 
			// delete
			// 
			this.delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.NullValue = "DELETE";
			this.delete.DefaultCellStyle = dataGridViewCellStyle3;
			this.delete.FillWeight = 92.4663F;
			this.delete.HeaderText = "Delete";
			this.delete.Name = "delete";
			this.delete.Text = "Delete";
			this.delete.Width = 44;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(848, 666);
			this.Controls.Add(this.splitContainer2);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "Main";
			this.Text = "Lord Of Ranger";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
			this.ClientSizeChanged += new System.EventHandler(this.Arad_ClientSizeChanged);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			this.splitContainer5.Panel1.ResumeLayout(false);
			this.splitContainer5.Panel1.PerformLayout();
			this.splitContainer5.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
			this.splitContainer5.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.Timer timerActiveWindowCheck;
		private System.Windows.Forms.Timer timerBarrage;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAddRow;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ListBox lbSettingList;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.Button btnAddSetting;
		private System.Windows.Forms.Button btnDeleteSetting;
		internal System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem sETTINGToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem skillIconExtractorToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer5;
		private System.Windows.Forms.Button btnHotKeyChange;
		private System.Windows.Forms.Label lblSettingName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtHotKey;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.DataGridViewImageColumn skillIcon;
		private System.Windows.Forms.DataGridViewImageColumn disableSkillIcon;
		private System.Windows.Forms.DataGridViewTextBoxColumn mode;
		private System.Windows.Forms.DataGridViewTextBoxColumn sequence;
		private System.Windows.Forms.DataGridViewComboBoxColumn priority;
		private System.Windows.Forms.DataGridViewTextBoxColumn push;
		private System.Windows.Forms.DataGridViewTextBoxColumn send;
		private System.Windows.Forms.DataGridViewButtonColumn up;
		private System.Windows.Forms.DataGridViewButtonColumn down;
		private System.Windows.Forms.DataGridViewButtonColumn delete;
	}
}

