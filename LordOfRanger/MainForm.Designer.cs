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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbSwitchPosition = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtHotKey = new System.Windows.Forms.TextBox();
			this.btnHotKeyChange = new System.Windows.Forms.Button();
			this.btnUpRow = new System.Windows.Forms.Button();
			this.btnAddRow = new System.Windows.Forms.Button();
			this.btnDownRow = new System.Windows.Forms.Button();
			this.dgv = new System.Windows.Forms.DataGridView();
			this.dgvColSkillIcon = new System.Windows.Forms.DataGridViewImageColumn();
			this.dgvColDisableSkillIcon = new System.Windows.Forms.DataGridViewImageColumn();
			this.dgvColMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColPriority = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dgvColPush = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColSend = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColKeyboardCancel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dgvColDelete = new System.Windows.Forms.DataGridViewButtonColumn();
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.sETTINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skillIconExtractorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.splitContainer1.Size = new System.Drawing.Size(694, 610);
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
			this.splitContainer3.Panel1.Controls.Add(this.label2);
			this.splitContainer3.Panel1.Controls.Add(this.cmbSwitchPosition);
			this.splitContainer3.Panel1.Controls.Add(this.label1);
			this.splitContainer3.Panel1.Controls.Add(this.txtHotKey);
			this.splitContainer3.Panel1.Controls.Add(this.btnHotKeyChange);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.btnUpRow);
			this.splitContainer3.Panel2.Controls.Add(this.btnAddRow);
			this.splitContainer3.Panel2.Controls.Add(this.btnDownRow);
			this.splitContainer3.Size = new System.Drawing.Size(694, 39);
			this.splitContainer3.SplitterDistance = 515;
			this.splitContainer3.TabIndex = 0;
			this.splitContainer3.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(252, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "スイッチ配置:";
			// 
			// cmbSwitchPosition
			// 
			this.cmbSwitchPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSwitchPosition.FormattingEnabled = true;
			this.cmbSwitchPosition.Items.AddRange(new object[] {
            "クイックスロット1",
            "クイックスロット2",
            "クイックスロット3",
            "クイックスロット4",
            "クイックスロット5",
            "クイックスロット6"});
			this.cmbSwitchPosition.Location = new System.Drawing.Point(320, 9);
			this.cmbSwitchPosition.Name = "cmbSwitchPosition";
			this.cmbSwitchPosition.Size = new System.Drawing.Size(121, 20);
			this.cmbSwitchPosition.TabIndex = 3;
			this.cmbSwitchPosition.SelectedIndexChanged += new System.EventHandler(this.cmbSwitchPosition_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "ホットキー:";
			// 
			// txtHotKey
			// 
			this.txtHotKey.Location = new System.Drawing.Point(63, 9);
			this.txtHotKey.Name = "txtHotKey";
			this.txtHotKey.ReadOnly = true;
			this.txtHotKey.Size = new System.Drawing.Size(96, 19);
			this.txtHotKey.TabIndex = 1;
			this.txtHotKey.TextChanged += new System.EventHandler(this.txtHotKey_TextChanged);
			// 
			// btnHotKeyChange
			// 
			this.btnHotKeyChange.Location = new System.Drawing.Point(165, 6);
			this.btnHotKeyChange.Name = "btnHotKeyChange";
			this.btnHotKeyChange.Size = new System.Drawing.Size(67, 23);
			this.btnHotKeyChange.TabIndex = 0;
			this.btnHotKeyChange.Text = "変更";
			this.btnHotKeyChange.UseVisualStyleBackColor = true;
			this.btnHotKeyChange.Click += new System.EventHandler(this.btnHotKeyChange_Click);
			// 
			// btnUpRow
			// 
			this.btnUpRow.Location = new System.Drawing.Point(5, 6);
			this.btnUpRow.Name = "btnUpRow";
			this.btnUpRow.Size = new System.Drawing.Size(38, 23);
			this.btnUpRow.TabIndex = 1;
			this.btnUpRow.Text = "上へ";
			this.btnUpRow.UseVisualStyleBackColor = true;
			this.btnUpRow.Click += new System.EventHandler(this.btnUpRow_Click);
			// 
			// btnAddRow
			// 
			this.btnAddRow.Location = new System.Drawing.Point(93, 6);
			this.btnAddRow.Name = "btnAddRow";
			this.btnAddRow.Size = new System.Drawing.Size(75, 23);
			this.btnAddRow.TabIndex = 0;
			this.btnAddRow.Text = "行の追加";
			this.btnAddRow.UseVisualStyleBackColor = true;
			this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
			// 
			// btnDownRow
			// 
			this.btnDownRow.Location = new System.Drawing.Point(49, 6);
			this.btnDownRow.Name = "btnDownRow";
			this.btnDownRow.Size = new System.Drawing.Size(38, 23);
			this.btnDownRow.TabIndex = 2;
			this.btnDownRow.Text = "下へ";
			this.btnDownRow.UseVisualStyleBackColor = true;
			this.btnDownRow.Click += new System.EventHandler(this.btnDownRow_Click);
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
            this.dgvColSkillIcon,
            this.dgvColDisableSkillIcon,
            this.dgvColMode,
            this.dgvColSequence,
            this.dgvColPriority,
            this.dgvColPush,
            this.dgvColSend,
            this.dgvColKeyboardCancel,
            this.dgvColDelete});
			this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgv.Location = new System.Drawing.Point(0, 0);
			this.dgv.MultiSelect = false;
			this.dgv.Name = "dgv";
			this.dgv.RowTemplate.Height = 21;
			this.dgv.Size = new System.Drawing.Size(694, 567);
			this.dgv.TabIndex = 7;
			this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
			this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
			this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
			this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
			// 
			// dgvColSkillIcon
			// 
			this.dgvColSkillIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.dgvColSkillIcon.Frozen = true;
			this.dgvColSkillIcon.HeaderText = "icon";
			this.dgvColSkillIcon.MinimumWidth = 50;
			this.dgvColSkillIcon.Name = "dgvColSkillIcon";
			this.dgvColSkillIcon.Width = 50;
			// 
			// dgvColDisableSkillIcon
			// 
			this.dgvColDisableSkillIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.dgvColDisableSkillIcon.Frozen = true;
			this.dgvColDisableSkillIcon.HeaderText = "無効icon";
			this.dgvColDisableSkillIcon.MinimumWidth = 50;
			this.dgvColDisableSkillIcon.Name = "dgvColDisableSkillIcon";
			this.dgvColDisableSkillIcon.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvColDisableSkillIcon.Width = 50;
			// 
			// dgvColMode
			// 
			this.dgvColMode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dgvColMode.Frozen = true;
			this.dgvColMode.HeaderText = "mode";
			this.dgvColMode.Name = "dgvColMode";
			this.dgvColMode.ReadOnly = true;
			this.dgvColMode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvColMode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dgvColMode.Width = 38;
			// 
			// dgvColSequence
			// 
			this.dgvColSequence.Frozen = true;
			this.dgvColSequence.HeaderText = "sequence";
			this.dgvColSequence.Name = "dgvColSequence";
			this.dgvColSequence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dgvColSequence.Visible = false;
			this.dgvColSequence.Width = 5;
			// 
			// dgvColPriority
			// 
			this.dgvColPriority.FillWeight = 47.41861F;
			this.dgvColPriority.Frozen = true;
			this.dgvColPriority.HeaderText = "優先度";
			this.dgvColPriority.Items.AddRange(new object[] {
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
			this.dgvColPriority.Name = "dgvColPriority";
			this.dgvColPriority.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvColPriority.Visible = false;
			this.dgvColPriority.Width = 5;
			// 
			// dgvColPush
			// 
			this.dgvColPush.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dgvColPush.FillWeight = 92.4663F;
			this.dgvColPush.HeaderText = "キーボードから入力されるキー";
			this.dgvColPush.Name = "dgvColPush";
			this.dgvColPush.ReadOnly = true;
			this.dgvColPush.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvColPush.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dgvColSend
			// 
			this.dgvColSend.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dgvColSend.FillWeight = 92.4663F;
			this.dgvColSend.HeaderText = "送信されるキー";
			this.dgvColSend.Name = "dgvColSend";
			this.dgvColSend.ReadOnly = true;
			this.dgvColSend.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvColSend.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dgvColKeyboardCancel
			// 
			this.dgvColKeyboardCancel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgvColKeyboardCancel.FalseValue = false;
			this.dgvColKeyboardCancel.HeaderText = "キー入力キャンセル";
			this.dgvColKeyboardCancel.Name = "dgvColKeyboardCancel";
			this.dgvColKeyboardCancel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvColKeyboardCancel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dgvColKeyboardCancel.TrueValue = true;
			this.dgvColKeyboardCancel.Width = 130;
			// 
			// dgvColDelete
			// 
			this.dgvColDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.NullValue = "削除";
			this.dgvColDelete.DefaultCellStyle = dataGridViewCellStyle1;
			this.dgvColDelete.FillWeight = 92.4663F;
			this.dgvColDelete.HeaderText = "削除";
			this.dgvColDelete.Name = "dgvColDelete";
			this.dgvColDelete.Text = "削除";
			this.dgvColDelete.Width = 32;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(614, 2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "キャンセル";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(533, 2);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "保存";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(0, 24);
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
			this.splitContainer2.Size = new System.Drawing.Size(848, 642);
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
			this.splitContainer4.Size = new System.Drawing.Size(150, 642);
			this.splitContainer4.SplitterDistance = 577;
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
			this.lbSettingList.Size = new System.Drawing.Size(150, 577);
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
			this.btnDeleteSetting.Text = "設定ファイル削除";
			this.btnDeleteSetting.UseVisualStyleBackColor = true;
			this.btnDeleteSetting.Click += new System.EventHandler(this.btnDeleteSetting_Click);
			// 
			// btnAddSetting
			// 
			this.btnAddSetting.Location = new System.Drawing.Point(12, 6);
			this.btnAddSetting.Name = "btnAddSetting";
			this.btnAddSetting.Size = new System.Drawing.Size(125, 23);
			this.btnAddSetting.TabIndex = 0;
			this.btnAddSetting.Text = "設定ファイル作成";
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
			this.splitContainer5.Size = new System.Drawing.Size(694, 642);
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
			this.contextMenuStrip1.Size = new System.Drawing.Size(97, 26);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
			this.ExitToolStripMenuItem.Text = "EXIT";
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// timerActiveWindowCheck
			// 
			this.timerActiveWindowCheck.Interval = 500;
			this.timerActiveWindowCheck.Tick += new System.EventHandler(this.timerActiveWindowCheck_Tick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sETTINGToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(848, 24);
			this.menuStrip1.TabIndex = 10;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// sETTINGToolStripMenuItem
			// 
			this.sETTINGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem,
            this.skillIconExtractorToolStripMenuItem});
			this.sETTINGToolStripMenuItem.Name = "sETTINGToolStripMenuItem";
			this.sETTINGToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.sETTINGToolStripMenuItem.Text = "ツール";
			// 
			// optionToolStripMenuItem
			// 
			this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
			this.optionToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.optionToolStripMenuItem.Text = "オプション";
			this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
			// 
			// skillIconExtractorToolStripMenuItem
			// 
			this.skillIconExtractorToolStripMenuItem.Name = "skillIconExtractorToolStripMenuItem";
			this.skillIconExtractorToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.skillIconExtractorToolStripMenuItem.Text = "スキルアイコン抽出";
			this.skillIconExtractorToolStripMenuItem.Click += new System.EventHandler(this.skillIconExtractorToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.helpToolStripMenuItem.Text = "ヘルプ";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.aboutToolStripMenuItem.Text = "Lord Of Rangerについて";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(848, 666);
			this.Controls.Add(this.splitContainer2);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "MainForm";
			this.Text = "Lord Of Ranger";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
		private System.Windows.Forms.Button btnUpRow;
		private System.Windows.Forms.Button btnDownRow;
		private System.Windows.Forms.DataGridViewImageColumn dgvColSkillIcon;
		private System.Windows.Forms.DataGridViewImageColumn dgvColDisableSkillIcon;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColMode;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSequence;
		private System.Windows.Forms.DataGridViewComboBoxColumn dgvColPriority;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColPush;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSend;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColKeyboardCancel;
		private System.Windows.Forms.DataGridViewButtonColumn dgvColDelete;
		private System.Windows.Forms.ComboBox cmbSwitchPosition;
		private System.Windows.Forms.Label label2;
	}
}

