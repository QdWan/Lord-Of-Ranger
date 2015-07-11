namespace LordOfRanger.Options {
	partial class OptionsForm {
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.txtGeneralProcessName = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.cmbGeneralStartupState = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.tabInterval = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.nudIntervalToggleTimer = new System.Windows.Forms.NumericUpDown();
			this.nudIntervalToggleUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.nudIntervalCommandUpDown = new System.Windows.Forms.NumericUpDown();
			this.nudIntervalCommandKeys = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tabSkillIcon = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.chkSkillIconEnable = new System.Windows.Forms.CheckBox();
			this.panelSkillIcon = new System.Windows.Forms.Panel();
			this.cmbSkillIconDisplayPosition = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.nudOneRowIcons = new System.Windows.Forms.NumericUpDown();
			this.tabOther = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.txtOtherHotKeyLORSwitching = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.panelOtherActiveWindowMonitoring = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.nudOtherActiveWindowMonitoringTimerInterval = new System.Windows.Forms.NumericUpDown();
			this.chkOtherActiveWindowMonitoringEnable = new System.Windows.Forms.CheckBox();
			this.tabAdvanced = new System.Windows.Forms.TabPage();
			this.chkAdvancedCommandUpArrowKeys = new System.Windows.Forms.CheckBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.tabInterval.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalToggleTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalToggleUpDown)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalCommandUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalCommandKeys)).BeginInit();
			this.tabSkillIcon.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.panelSkillIcon.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudOneRowIcons)).BeginInit();
			this.tabOther.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panelOtherActiveWindowMonitoring.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudOtherActiveWindowMonitoringTimerInterval)).BeginInit();
			this.tabAdvanced.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Controls.Add(this.tabInterval);
			this.tabControl1.Controls.Add(this.tabSkillIcon);
			this.tabControl1.Controls.Add(this.tabOther);
			this.tabControl1.Controls.Add(this.tabAdvanced);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(420, 369);
			this.tabControl1.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.groupBox6);
			this.tabGeneral.Location = new System.Drawing.Point(4, 27);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(412, 338);
			this.tabGeneral.TabIndex = 2;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.txtGeneralProcessName);
			this.groupBox6.Controls.Add(this.label12);
			this.groupBox6.Controls.Add(this.cmbGeneralStartupState);
			this.groupBox6.Controls.Add(this.label9);
			this.groupBox6.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox6.Location = new System.Drawing.Point(6, 6);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(398, 91);
			this.groupBox6.TabIndex = 2;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "General";
			// 
			// txtGeneralProcessName
			// 
			this.txtGeneralProcessName.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtGeneralProcessName.Location = new System.Drawing.Point(257, 62);
			this.txtGeneralProcessName.Name = "txtGeneralProcessName";
			this.txtGeneralProcessName.Size = new System.Drawing.Size(121, 25);
			this.txtGeneralProcessName.TabIndex = 14;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(12, 64);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(96, 18);
			this.label12.TabIndex = 13;
			this.label12.Text = "Process Name:";
			// 
			// cmbGeneralStartupState
			// 
			this.cmbGeneralStartupState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGeneralStartupState.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmbGeneralStartupState.FormattingEnabled = true;
			this.cmbGeneralStartupState.Items.AddRange(new object[] {
            "Normal",
            "Minimized"});
			this.cmbGeneralStartupState.Location = new System.Drawing.Point(257, 26);
			this.cmbGeneralStartupState.Name = "cmbGeneralStartupState";
			this.cmbGeneralStartupState.Size = new System.Drawing.Size(121, 26);
			this.cmbGeneralStartupState.TabIndex = 1;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(12, 29);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(88, 18);
			this.label9.TabIndex = 0;
			this.label9.Text = "Startup State";
			// 
			// tabInterval
			// 
			this.tabInterval.Controls.Add(this.groupBox2);
			this.tabInterval.Controls.Add(this.groupBox1);
			this.tabInterval.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tabInterval.Location = new System.Drawing.Point(4, 27);
			this.tabInterval.Name = "tabInterval";
			this.tabInterval.Padding = new System.Windows.Forms.Padding(3);
			this.tabInterval.Size = new System.Drawing.Size(412, 338);
			this.tabInterval.TabIndex = 0;
			this.tabInterval.Text = "Interval";
			this.tabInterval.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.nudIntervalToggleTimer);
			this.groupBox2.Controls.Add(this.nudIntervalToggleUpDown);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox2.Location = new System.Drawing.Point(8, 124);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(396, 102);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Toggle And Barrage";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("メイリオ", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label11.Location = new System.Drawing.Point(110, 28);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(86, 14);
			this.label11.TabIndex = 13;
			this.label11.Text = "(Restart Required)";
			// 
			// nudIntervalToggleTimer
			// 
			this.nudIntervalToggleTimer.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.nudIntervalToggleTimer.Location = new System.Drawing.Point(244, 19);
			this.nudIntervalToggleTimer.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.nudIntervalToggleTimer.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.nudIntervalToggleTimer.Name = "nudIntervalToggleTimer";
			this.nudIntervalToggleTimer.Size = new System.Drawing.Size(73, 25);
			this.nudIntervalToggleTimer.TabIndex = 10;
			this.nudIntervalToggleTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudIntervalToggleTimer.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// nudIntervalToggleUpDown
			// 
			this.nudIntervalToggleUpDown.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.nudIntervalToggleUpDown.Location = new System.Drawing.Point(244, 66);
			this.nudIntervalToggleUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.nudIntervalToggleUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.nudIntervalToggleUpDown.Name = "nudIntervalToggleUpDown";
			this.nudIntervalToggleUpDown.Size = new System.Drawing.Size(73, 25);
			this.nudIntervalToggleUpDown.TabIndex = 11;
			this.nudIntervalToggleUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudIntervalToggleUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(6, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 18);
			this.label3.TabIndex = 5;
			this.label3.Text = "Timer Interval:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(6, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(194, 31);
			this.label2.TabIndex = 3;
			this.label2.Text = "KeyUp And KeyDown Interval:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.nudIntervalCommandUpDown);
			this.groupBox1.Controls.Add(this.nudIntervalCommandKeys);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox1.Location = new System.Drawing.Point(8, 10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(396, 99);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Command";
			// 
			// nudIntervalCommandUpDown
			// 
			this.nudIntervalCommandUpDown.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.nudIntervalCommandUpDown.Location = new System.Drawing.Point(244, 65);
			this.nudIntervalCommandUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.nudIntervalCommandUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.nudIntervalCommandUpDown.Name = "nudIntervalCommandUpDown";
			this.nudIntervalCommandUpDown.Size = new System.Drawing.Size(73, 25);
			this.nudIntervalCommandUpDown.TabIndex = 9;
			this.nudIntervalCommandUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudIntervalCommandUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// nudIntervalCommandKeys
			// 
			this.nudIntervalCommandKeys.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.nudIntervalCommandKeys.Location = new System.Drawing.Point(244, 27);
			this.nudIntervalCommandKeys.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.nudIntervalCommandKeys.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.nudIntervalCommandKeys.Name = "nudIntervalCommandKeys";
			this.nudIntervalCommandKeys.Size = new System.Drawing.Size(73, 25);
			this.nudIntervalCommandKeys.TabIndex = 8;
			this.nudIntervalCommandKeys.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudIntervalCommandKeys.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(6, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(194, 29);
			this.label1.TabIndex = 1;
			this.label1.Text = "Sleep Time After Key Input:";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(6, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(194, 31);
			this.label4.TabIndex = 7;
			this.label4.Text = "KeyUp And KeyDown Interval:";
			// 
			// tabSkillIcon
			// 
			this.tabSkillIcon.Controls.Add(this.groupBox5);
			this.tabSkillIcon.Location = new System.Drawing.Point(4, 27);
			this.tabSkillIcon.Name = "tabSkillIcon";
			this.tabSkillIcon.Padding = new System.Windows.Forms.Padding(3);
			this.tabSkillIcon.Size = new System.Drawing.Size(412, 338);
			this.tabSkillIcon.TabIndex = 1;
			this.tabSkillIcon.Text = "Skill Icon";
			this.tabSkillIcon.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.chkSkillIconEnable);
			this.groupBox5.Controls.Add(this.panelSkillIcon);
			this.groupBox5.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox5.Location = new System.Drawing.Point(9, 9);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(395, 142);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Skill Icon";
			// 
			// chkSkillIconEnable
			// 
			this.chkSkillIconEnable.AutoSize = true;
			this.chkSkillIconEnable.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.chkSkillIconEnable.Location = new System.Drawing.Point(16, 19);
			this.chkSkillIconEnable.Name = "chkSkillIconEnable";
			this.chkSkillIconEnable.Size = new System.Drawing.Size(65, 22);
			this.chkSkillIconEnable.TabIndex = 0;
			this.chkSkillIconEnable.Text = "Enable";
			this.chkSkillIconEnable.UseVisualStyleBackColor = true;
			this.chkSkillIconEnable.CheckedChanged += new System.EventHandler(this.chkSkillIconEnable_CheckedChanged);
			// 
			// panelSkillIcon
			// 
			this.panelSkillIcon.Controls.Add(this.cmbSkillIconDisplayPosition);
			this.panelSkillIcon.Controls.Add(this.label6);
			this.panelSkillIcon.Controls.Add(this.label5);
			this.panelSkillIcon.Controls.Add(this.nudOneRowIcons);
			this.panelSkillIcon.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.panelSkillIcon.Location = new System.Drawing.Point(8, 42);
			this.panelSkillIcon.Name = "panelSkillIcon";
			this.panelSkillIcon.Size = new System.Drawing.Size(381, 75);
			this.panelSkillIcon.TabIndex = 4;
			// 
			// cmbSkillIconDisplayPosition
			// 
			this.cmbSkillIconDisplayPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSkillIconDisplayPosition.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmbSkillIconDisplayPosition.FormattingEnabled = true;
			this.cmbSkillIconDisplayPosition.Items.AddRange(new object[] {
            "TopLeft",
            "TopRight",
            "BottomLeft",
            "BottomRight"});
			this.cmbSkillIconDisplayPosition.Location = new System.Drawing.Point(123, 41);
			this.cmbSkillIconDisplayPosition.Name = "cmbSkillIconDisplayPosition";
			this.cmbSkillIconDisplayPosition.Size = new System.Drawing.Size(121, 26);
			this.cmbSkillIconDisplayPosition.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(4, 43);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 18);
			this.label6.TabIndex = 4;
			this.label6.Text = "Display position:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(3, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(263, 18);
			this.label5.TabIndex = 1;
			this.label5.Text = "Number of icons to be displayed in one line:";
			// 
			// nudOneRowIcons
			// 
			this.nudOneRowIcons.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.nudOneRowIcons.Location = new System.Drawing.Point(304, 7);
			this.nudOneRowIcons.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudOneRowIcons.Name = "nudOneRowIcons";
			this.nudOneRowIcons.Size = new System.Drawing.Size(37, 25);
			this.nudOneRowIcons.TabIndex = 3;
			this.nudOneRowIcons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudOneRowIcons.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
			// 
			// tabOther
			// 
			this.tabOther.Controls.Add(this.groupBox4);
			this.tabOther.Controls.Add(this.groupBox3);
			this.tabOther.Location = new System.Drawing.Point(4, 27);
			this.tabOther.Name = "tabOther";
			this.tabOther.Padding = new System.Windows.Forms.Padding(3);
			this.tabOther.Size = new System.Drawing.Size(412, 338);
			this.tabOther.TabIndex = 3;
			this.tabOther.Text = "Other";
			this.tabOther.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.txtOtherHotKeyLORSwitching);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox4.Location = new System.Drawing.Point(6, 113);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(396, 51);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Hot Key";
			// 
			// txtOtherHotKeyLORSwitching
			// 
			this.txtOtherHotKeyLORSwitching.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.txtOtherHotKeyLORSwitching.Location = new System.Drawing.Point(290, 17);
			this.txtOtherHotKeyLORSwitching.Name = "txtOtherHotKeyLORSwitching";
			this.txtOtherHotKeyLORSwitching.ReadOnly = true;
			this.txtOtherHotKeyLORSwitching.Size = new System.Drawing.Size(88, 25);
			this.txtOtherHotKeyLORSwitching.TabIndex = 1;
			this.txtOtherHotKeyLORSwitching.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOtherHotKeyLORSwitching_KeyUp);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label8.Location = new System.Drawing.Point(7, 20);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(248, 18);
			this.label8.TabIndex = 0;
			this.label8.Text = "Load Of Ranger Enable/Disable Switching";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.panelOtherActiveWindowMonitoring);
			this.groupBox3.Controls.Add(this.chkOtherActiveWindowMonitoringEnable);
			this.groupBox3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox3.Location = new System.Drawing.Point(6, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(396, 99);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Active Window Monitoring";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(272, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(106, 18);
			this.label10.TabIndex = 12;
			this.label10.Text = "Restart Required";
			// 
			// panelOtherActiveWindowMonitoring
			// 
			this.panelOtherActiveWindowMonitoring.Controls.Add(this.label7);
			this.panelOtherActiveWindowMonitoring.Controls.Add(this.nudOtherActiveWindowMonitoringTimerInterval);
			this.panelOtherActiveWindowMonitoring.Location = new System.Drawing.Point(6, 60);
			this.panelOtherActiveWindowMonitoring.Name = "panelOtherActiveWindowMonitoring";
			this.panelOtherActiveWindowMonitoring.Size = new System.Drawing.Size(384, 33);
			this.panelOtherActiveWindowMonitoring.TabIndex = 11;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label7.Location = new System.Drawing.Point(6, 9);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(98, 18);
			this.label7.TabIndex = 9;
			this.label7.Text = "Timer Interval:";
			// 
			// nudOtherActiveWindowMonitoringTimerInterval
			// 
			this.nudOtherActiveWindowMonitoringTimerInterval.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.nudOtherActiveWindowMonitoringTimerInterval.Location = new System.Drawing.Point(216, 5);
			this.nudOtherActiveWindowMonitoringTimerInterval.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.nudOtherActiveWindowMonitoringTimerInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudOtherActiveWindowMonitoringTimerInterval.Name = "nudOtherActiveWindowMonitoringTimerInterval";
			this.nudOtherActiveWindowMonitoringTimerInterval.Size = new System.Drawing.Size(73, 25);
			this.nudOtherActiveWindowMonitoringTimerInterval.TabIndex = 10;
			this.nudOtherActiveWindowMonitoringTimerInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudOtherActiveWindowMonitoringTimerInterval.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// chkOtherActiveWindowMonitoringEnable
			// 
			this.chkOtherActiveWindowMonitoringEnable.AutoSize = true;
			this.chkOtherActiveWindowMonitoringEnable.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.chkOtherActiveWindowMonitoringEnable.Location = new System.Drawing.Point(15, 26);
			this.chkOtherActiveWindowMonitoringEnable.Name = "chkOtherActiveWindowMonitoringEnable";
			this.chkOtherActiveWindowMonitoringEnable.Size = new System.Drawing.Size(65, 22);
			this.chkOtherActiveWindowMonitoringEnable.TabIndex = 1;
			this.chkOtherActiveWindowMonitoringEnable.Text = "Enable";
			this.chkOtherActiveWindowMonitoringEnable.UseVisualStyleBackColor = true;
			this.chkOtherActiveWindowMonitoringEnable.CheckedChanged += new System.EventHandler(this.chkOtherActiveWindowMonitoringEnable_CheckedChanged);
			// 
			// tabAdvanced
			// 
			this.tabAdvanced.Controls.Add(this.chkAdvancedCommandUpArrowKeys);
			this.tabAdvanced.Location = new System.Drawing.Point(4, 27);
			this.tabAdvanced.Name = "tabAdvanced";
			this.tabAdvanced.Padding = new System.Windows.Forms.Padding(3);
			this.tabAdvanced.Size = new System.Drawing.Size(412, 338);
			this.tabAdvanced.TabIndex = 4;
			this.tabAdvanced.Text = "Advanced";
			this.tabAdvanced.UseVisualStyleBackColor = true;
			// 
			// chkAdvancedCommandUpArrowKeys
			// 
			this.chkAdvancedCommandUpArrowKeys.AutoSize = true;
			this.chkAdvancedCommandUpArrowKeys.Location = new System.Drawing.Point(21, 20);
			this.chkAdvancedCommandUpArrowKeys.Name = "chkAdvancedCommandUpArrowKeys";
			this.chkAdvancedCommandUpArrowKeys.Size = new System.Drawing.Size(271, 40);
			this.chkAdvancedCommandUpArrowKeys.TabIndex = 0;
			this.chkAdvancedCommandUpArrowKeys.Text = "When enter the command,\r\nif arrow key had been pressed,it releases.";
			this.chkAdvancedCommandUpArrowKeys.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnApply);
			this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
			this.splitContainer1.Panel2.Controls.Add(this.btnOk);
			this.splitContainer1.Size = new System.Drawing.Size(420, 398);
			this.splitContainer1.SplitterDistance = 369;
			this.splitContainer1.TabIndex = 1;
			this.splitContainer1.TabStop = false;
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(339, -1);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(258, -1);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(177, -1);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(420, 398);
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.tabInterval.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalToggleTimer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalToggleUpDown)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalCommandUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalCommandKeys)).EndInit();
			this.tabSkillIcon.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.panelSkillIcon.ResumeLayout(false);
			this.panelSkillIcon.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudOneRowIcons)).EndInit();
			this.tabOther.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.panelOtherActiveWindowMonitoring.ResumeLayout(false);
			this.panelOtherActiveWindowMonitoring.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudOtherActiveWindowMonitoringTimerInterval)).EndInit();
			this.tabAdvanced.ResumeLayout(false);
			this.tabAdvanced.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInterval;
        private System.Windows.Forms.TabPage tabSkillIcon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkSkillIconEnable;
		private System.Windows.Forms.NumericUpDown nudOneRowIcons;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panelSkillIcon;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.ComboBox cmbSkillIconDisplayPosition;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.NumericUpDown nudIntervalToggleTimer;
		private System.Windows.Forms.NumericUpDown nudIntervalToggleUpDown;
		private System.Windows.Forms.NumericUpDown nudIntervalCommandUpDown;
		private System.Windows.Forms.NumericUpDown nudIntervalCommandKeys;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.ComboBox cmbGeneralStartupState;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TabPage tabOther;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox txtOtherHotKeyLORSwitching;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Panel panelOtherActiveWindowMonitoring;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nudOtherActiveWindowMonitoringTimerInterval;
		private System.Windows.Forms.CheckBox chkOtherActiveWindowMonitoringEnable;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtGeneralProcessName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TabPage tabAdvanced;
		private System.Windows.Forms.CheckBox chkAdvancedCommandUpArrowKeys;
	}
}