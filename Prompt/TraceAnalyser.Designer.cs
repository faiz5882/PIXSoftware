namespace SIMCORE_TOOL.Prompt
{
    partial class TraceAnalyser
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TraceAnalyser));
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lst_Content = new System.Windows.Forms.ListBox();
            this.gb_Content = new System.Windows.Forms.GroupBox();
            this.cb_Queue = new System.Windows.Forms.CheckBox();
            this.rbWorkingPoint = new System.Windows.Forms.RadioButton();
            this.rbTrackingPoint = new System.Windows.Forms.RadioButton();
            this.txt_DisplayedName = new System.Windows.Forms.TextBox();
            this.txt_TraceName = new System.Windows.Forms.TextBox();
            this.cb_Indexed = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudIgnoredColumns = new System.Windows.Forms.NumericUpDown();
            this.btn_Load = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.ofd_Open = new System.Windows.Forms.OpenFileDialog();
            this.sfd_SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.gb_Dates = new System.Windows.Forms.GroupBox();
            this.dtp_BeginTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_BeginTime = new System.Windows.Forms.Label();
            this.lbl_EndTime = new System.Windows.Forms.Label();
            this.cb_SamplingStep = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtp_EndTime = new System.Windows.Forms.DateTimePicker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_Trace = new System.Windows.Forms.TabPage();
            this.p_Main = new System.Windows.Forms.Panel();
            this.cb_ReadTrace = new System.Windows.Forms.CheckBox();
            this.gb_PP_Seed = new System.Windows.Forms.GroupBox();
            this.mb_PP_UseDefinedSeed = new System.Windows.Forms.MaskedTextBox();
            this.btn_DefineSeeds = new System.Windows.Forms.Button();
            this.cb_PP_UseDefinedSeed = new System.Windows.Forms.CheckBox();
            this.cb_UseSameSeed = new System.Windows.Forms.CheckBox();
            this.gb_Trace = new System.Windows.Forms.GroupBox();
            this.txt_SIM_WarmUp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Trace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tp_Content = new System.Windows.Forms.TabPage();
            this.p_Trace = new System.Windows.Forms.Panel();
            this.tp_InputData = new System.Windows.Forms.TabPage();
            this.p_InputData = new System.Windows.Forms.Panel();
            this.tp_Simulation = new System.Windows.Forms.TabPage();
            this.lbl_CurrentOperation = new System.Windows.Forms.Label();
            this.lbl_SIM_Current = new System.Windows.Forms.Label();
            this.btn_SIM_CancelSimulation = new System.Windows.Forms.Button();
            this.lbl_SIM_Total = new System.Windows.Forms.Label();
            this.lbl_SIM_Current_Name = new System.Windows.Forms.Label();
            this.btn_SIM_Start = new System.Windows.Forms.Button();
            this.pb_SIM_Current = new System.Windows.Forms.ProgressBar();
            this.pb_SIM_TotalOperations = new System.Windows.Forms.ProgressBar();
            this.gb_SIM_Model = new System.Windows.Forms.GroupBox();
            this.cb_SIM_DisplayModel = new System.Windows.Forms.CheckBox();
            this.lbl_SIM_ModelName = new System.Windows.Forms.Label();
            this.btn_SIM_OpenModel = new System.Windows.Forms.Button();
            this.lbl_ScenarioName = new System.Windows.Forms.Label();
            this.cb_Scenario = new System.Windows.Forms.ComboBox();
            this.gb_Content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIgnoredColumns)).BeginInit();
            this.gb_Dates.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tp_Trace.SuspendLayout();
            this.p_Main.SuspendLayout();
            this.gb_PP_Seed.SuspendLayout();
            this.gb_Trace.SuspendLayout();
            this.tp_Content.SuspendLayout();
            this.p_Trace.SuspendLayout();
            this.tp_InputData.SuspendLayout();
            this.tp_Simulation.SuspendLayout();
            this.gb_SIM_Model.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(29, 455);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(106, 27);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "&Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(497, 455);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(106, 27);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lst_Content
            // 
            this.lst_Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_Content.FormattingEnabled = true;
            this.lst_Content.Location = new System.Drawing.Point(10, 58);
            this.lst_Content.Name = "lst_Content";
            this.lst_Content.Size = new System.Drawing.Size(251, 225);
            this.lst_Content.TabIndex = 4;
            this.lst_Content.SelectedIndexChanged += new System.EventHandler(this.lst_Content_SelectedIndexChanged);
            // 
            // gb_Content
            // 
            this.gb_Content.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Content.BackColor = System.Drawing.Color.Transparent;
            this.gb_Content.Controls.Add(this.cb_Queue);
            this.gb_Content.Controls.Add(this.rbWorkingPoint);
            this.gb_Content.Controls.Add(this.rbTrackingPoint);
            this.gb_Content.Controls.Add(this.txt_DisplayedName);
            this.gb_Content.Controls.Add(this.txt_TraceName);
            this.gb_Content.Controls.Add(this.cb_Indexed);
            this.gb_Content.Controls.Add(this.label2);
            this.gb_Content.Controls.Add(this.label1);
            this.gb_Content.Location = new System.Drawing.Point(283, 144);
            this.gb_Content.Name = "gb_Content";
            this.gb_Content.Size = new System.Drawing.Size(266, 160);
            this.gb_Content.TabIndex = 5;
            this.gb_Content.TabStop = false;
            this.gb_Content.Text = "groupBox1";
            this.gb_Content.Visible = false;
            // 
            // cb_Queue
            // 
            this.cb_Queue.AutoSize = true;
            this.cb_Queue.Location = new System.Drawing.Point(119, 79);
            this.cb_Queue.Name = "cb_Queue";
            this.cb_Queue.Size = new System.Drawing.Size(76, 17);
            this.cb_Queue.TabIndex = 15;
            this.cb_Queue.Text = "Is a queue";
            this.cb_Queue.UseVisualStyleBackColor = true;
            // 
            // rbWorkingPoint
            // 
            this.rbWorkingPoint.AutoSize = true;
            this.rbWorkingPoint.Location = new System.Drawing.Point(21, 135);
            this.rbWorkingPoint.Name = "rbWorkingPoint";
            this.rbWorkingPoint.Size = new System.Drawing.Size(91, 17);
            this.rbWorkingPoint.TabIndex = 14;
            this.rbWorkingPoint.TabStop = true;
            this.rbWorkingPoint.Text = "Working point";
            this.rbWorkingPoint.UseVisualStyleBackColor = true;
            // 
            // rbTrackingPoint
            // 
            this.rbTrackingPoint.AutoSize = true;
            this.rbTrackingPoint.Location = new System.Drawing.Point(21, 112);
            this.rbTrackingPoint.Name = "rbTrackingPoint";
            this.rbTrackingPoint.Size = new System.Drawing.Size(93, 17);
            this.rbTrackingPoint.TabIndex = 13;
            this.rbTrackingPoint.TabStop = true;
            this.rbTrackingPoint.Text = "Tracking point";
            this.rbTrackingPoint.UseVisualStyleBackColor = true;
            // 
            // txt_DisplayedName
            // 
            this.txt_DisplayedName.Location = new System.Drawing.Point(97, 51);
            this.txt_DisplayedName.Name = "txt_DisplayedName";
            this.txt_DisplayedName.Size = new System.Drawing.Size(140, 20);
            this.txt_DisplayedName.TabIndex = 12;
            this.txt_DisplayedName.TextChanged += new System.EventHandler(this.txt_DisplayedName_TextChanged);
            // 
            // txt_TraceName
            // 
            this.txt_TraceName.Location = new System.Drawing.Point(97, 25);
            this.txt_TraceName.Name = "txt_TraceName";
            this.txt_TraceName.Size = new System.Drawing.Size(140, 20);
            this.txt_TraceName.TabIndex = 11;
            this.txt_TraceName.Validating += new System.ComponentModel.CancelEventHandler(this.txt_TraceName_Validating);
            // 
            // cb_Indexed
            // 
            this.cb_Indexed.AutoSize = true;
            this.cb_Indexed.Location = new System.Drawing.Point(9, 79);
            this.cb_Indexed.Name = "cb_Indexed";
            this.cb_Indexed.Size = new System.Drawing.Size(64, 17);
            this.cb_Indexed.TabIndex = 10;
            this.cb_Indexed.Text = "Indexed";
            this.cb_Indexed.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Displayed name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Trace name :";
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.Location = new System.Drawing.Point(286, 58);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 6;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Location = new System.Drawing.Point(286, 87);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete.TabIndex = 7;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(41, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Number of columns that are not part of the trace :";
            // 
            // nudIgnoredColumns
            // 
            this.nudIgnoredColumns.Location = new System.Drawing.Point(286, 15);
            this.nudIgnoredColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudIgnoredColumns.Name = "nudIgnoredColumns";
            this.nudIgnoredColumns.Size = new System.Drawing.Size(44, 20);
            this.nudIgnoredColumns.TabIndex = 9;
            this.nudIgnoredColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_Load
            // 
            this.btn_Load.Location = new System.Drawing.Point(461, 58);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(75, 23);
            this.btn_Load.TabIndex = 10;
            this.btn_Load.Text = "Load Items";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(461, 87);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 11;
            this.btn_Save.Text = "Save Items";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // ofd_Open
            // 
            this.ofd_Open.Filter = "Trace files (*.ta)|*.ta";
            // 
            // sfd_SaveDialog
            // 
            this.sfd_SaveDialog.Filter = "Trace files (*.ta)|*.ta";
            // 
            // gb_Dates
            // 
            this.gb_Dates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Dates.Controls.Add(this.dtp_BeginTime);
            this.gb_Dates.Controls.Add(this.lbl_BeginTime);
            this.gb_Dates.Controls.Add(this.lbl_EndTime);
            this.gb_Dates.Controls.Add(this.cb_SamplingStep);
            this.gb_Dates.Controls.Add(this.label8);
            this.gb_Dates.Controls.Add(this.dtp_EndTime);
            this.gb_Dates.Location = new System.Drawing.Point(70, 13);
            this.gb_Dates.Name = "gb_Dates";
            this.gb_Dates.Size = new System.Drawing.Size(451, 107);
            this.gb_Dates.TabIndex = 101;
            this.gb_Dates.TabStop = false;
            this.gb_Dates.Text = "Dates / Times settings";
            // 
            // dtp_BeginTime
            // 
            this.dtp_BeginTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_BeginTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_BeginTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_BeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_BeginTime.Location = new System.Drawing.Point(131, 19);
            this.dtp_BeginTime.Name = "dtp_BeginTime";
            this.dtp_BeginTime.Size = new System.Drawing.Size(118, 20);
            this.dtp_BeginTime.TabIndex = 16;
            // 
            // lbl_BeginTime
            // 
            this.lbl_BeginTime.AutoSize = true;
            this.lbl_BeginTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_BeginTime.Location = new System.Drawing.Point(44, 23);
            this.lbl_BeginTime.Name = "lbl_BeginTime";
            this.lbl_BeginTime.Size = new System.Drawing.Size(58, 13);
            this.lbl_BeginTime.TabIndex = 15;
            this.lbl_BeginTime.Text = "Begin date";
            // 
            // lbl_EndTime
            // 
            this.lbl_EndTime.AutoSize = true;
            this.lbl_EndTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndTime.Location = new System.Drawing.Point(52, 49);
            this.lbl_EndTime.Name = "lbl_EndTime";
            this.lbl_EndTime.Size = new System.Drawing.Size(50, 13);
            this.lbl_EndTime.TabIndex = 18;
            this.lbl_EndTime.Text = "End date";
            // 
            // cb_SamplingStep
            // 
            this.cb_SamplingStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SamplingStep.FormattingEnabled = true;
            this.cb_SamplingStep.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "10",
            "12",
            "15",
            "20",
            "30",
            "60"});
            this.cb_SamplingStep.Location = new System.Drawing.Point(131, 71);
            this.cb_SamplingStep.Name = "cb_SamplingStep";
            this.cb_SamplingStep.Size = new System.Drawing.Size(48, 21);
            this.cb_SamplingStep.TabIndex = 97;
            this.cb_SamplingStep.Tag = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(29, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 96;
            this.label8.Text = "Sampling step";
            // 
            // dtp_EndTime
            // 
            this.dtp_EndTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_EndTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_EndTime.Location = new System.Drawing.Point(131, 45);
            this.dtp_EndTime.Name = "dtp_EndTime";
            this.dtp_EndTime.Size = new System.Drawing.Size(117, 20);
            this.dtp_EndTime.TabIndex = 17;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tp_Trace);
            this.tabControl1.Controls.Add(this.tp_Content);
            this.tabControl1.Controls.Add(this.tp_InputData);
            this.tabControl1.Controls.Add(this.tp_Simulation);
            this.tabControl1.Location = new System.Drawing.Point(29, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(591, 358);
            this.tabControl1.TabIndex = 102;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tp_Trace
            // 
            this.tp_Trace.Controls.Add(this.p_Main);
            this.tp_Trace.Location = new System.Drawing.Point(4, 22);
            this.tp_Trace.Name = "tp_Trace";
            this.tp_Trace.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Trace.Size = new System.Drawing.Size(583, 332);
            this.tp_Trace.TabIndex = 1;
            this.tp_Trace.Text = "Trace informations";
            this.tp_Trace.UseVisualStyleBackColor = true;
            // 
            // p_Main
            // 
            this.p_Main.Controls.Add(this.gb_Dates);
            this.p_Main.Controls.Add(this.cb_ReadTrace);
            this.p_Main.Controls.Add(this.gb_PP_Seed);
            this.p_Main.Controls.Add(this.gb_Trace);
            this.p_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_Main.Location = new System.Drawing.Point(3, 3);
            this.p_Main.Name = "p_Main";
            this.p_Main.Size = new System.Drawing.Size(577, 326);
            this.p_Main.TabIndex = 107;
            // 
            // cb_ReadTrace
            // 
            this.cb_ReadTrace.AutoSize = true;
            this.cb_ReadTrace.Location = new System.Drawing.Point(77, 224);
            this.cb_ReadTrace.Name = "cb_ReadTrace";
            this.cb_ReadTrace.Size = new System.Drawing.Size(79, 17);
            this.cb_ReadTrace.TabIndex = 106;
            this.cb_ReadTrace.Text = "Read trace";
            this.cb_ReadTrace.UseVisualStyleBackColor = true;
            this.cb_ReadTrace.CheckedChanged += new System.EventHandler(this.cbTrace_CheckedChanged);
            // 
            // gb_PP_Seed
            // 
            this.gb_PP_Seed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_PP_Seed.BackColor = System.Drawing.Color.Transparent;
            this.gb_PP_Seed.Controls.Add(this.mb_PP_UseDefinedSeed);
            this.gb_PP_Seed.Controls.Add(this.btn_DefineSeeds);
            this.gb_PP_Seed.Controls.Add(this.cb_PP_UseDefinedSeed);
            this.gb_PP_Seed.Controls.Add(this.cb_UseSameSeed);
            this.gb_PP_Seed.Location = new System.Drawing.Point(71, 135);
            this.gb_PP_Seed.Name = "gb_PP_Seed";
            this.gb_PP_Seed.Size = new System.Drawing.Size(450, 72);
            this.gb_PP_Seed.TabIndex = 104;
            this.gb_PP_Seed.TabStop = false;
            this.gb_PP_Seed.Text = "Random seed";
            // 
            // mb_PP_UseDefinedSeed
            // 
            this.mb_PP_UseDefinedSeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.mb_PP_UseDefinedSeed.Enabled = false;
            this.mb_PP_UseDefinedSeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mb_PP_UseDefinedSeed.Location = new System.Drawing.Point(129, 32);
            this.mb_PP_UseDefinedSeed.Mask = "99999";
            this.mb_PP_UseDefinedSeed.Name = "mb_PP_UseDefinedSeed";
            this.mb_PP_UseDefinedSeed.Size = new System.Drawing.Size(39, 20);
            this.mb_PP_UseDefinedSeed.TabIndex = 4;
            this.mb_PP_UseDefinedSeed.Text = "1";
            // 
            // btn_DefineSeeds
            // 
            this.btn_DefineSeeds.Enabled = false;
            this.btn_DefineSeeds.Location = new System.Drawing.Point(266, 31);
            this.btn_DefineSeeds.Name = "btn_DefineSeeds";
            this.btn_DefineSeeds.Size = new System.Drawing.Size(97, 20);
            this.btn_DefineSeeds.TabIndex = 3;
            this.btn_DefineSeeds.Text = "Define seeds";
            this.btn_DefineSeeds.UseVisualStyleBackColor = true;
            this.btn_DefineSeeds.Visible = false;
            // 
            // cb_PP_UseDefinedSeed
            // 
            this.cb_PP_UseDefinedSeed.AutoSize = true;
            this.cb_PP_UseDefinedSeed.BackColor = System.Drawing.Color.Transparent;
            this.cb_PP_UseDefinedSeed.Location = new System.Drawing.Point(14, 34);
            this.cb_PP_UseDefinedSeed.Name = "cb_PP_UseDefinedSeed";
            this.cb_PP_UseDefinedSeed.Size = new System.Drawing.Size(109, 17);
            this.cb_PP_UseDefinedSeed.TabIndex = 1;
            this.cb_PP_UseDefinedSeed.Text = "Use defined seed";
            this.cb_PP_UseDefinedSeed.UseVisualStyleBackColor = false;
            this.cb_PP_UseDefinedSeed.CheckedChanged += new System.EventHandler(this.cb_PP_UseDefinedSeed_CheckedChanged);
            // 
            // cb_UseSameSeed
            // 
            this.cb_UseSameSeed.AutoSize = true;
            this.cb_UseSameSeed.BackColor = System.Drawing.Color.Transparent;
            this.cb_UseSameSeed.Checked = true;
            this.cb_UseSameSeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_UseSameSeed.Location = new System.Drawing.Point(9, 34);
            this.cb_UseSameSeed.Name = "cb_UseSameSeed";
            this.cb_UseSameSeed.Size = new System.Drawing.Size(242, 17);
            this.cb_UseSameSeed.TabIndex = 0;
            this.cb_UseSameSeed.Text = "Use same random seed for all random streams";
            this.cb_UseSameSeed.UseVisualStyleBackColor = false;
            this.cb_UseSameSeed.Visible = false;
            // 
            // gb_Trace
            // 
            this.gb_Trace.Controls.Add(this.txt_SIM_WarmUp);
            this.gb_Trace.Controls.Add(this.label6);
            this.gb_Trace.Controls.Add(this.txt_Trace);
            this.gb_Trace.Controls.Add(this.label4);
            this.gb_Trace.Enabled = false;
            this.gb_Trace.Location = new System.Drawing.Point(70, 225);
            this.gb_Trace.Name = "gb_Trace";
            this.gb_Trace.Size = new System.Drawing.Size(451, 91);
            this.gb_Trace.TabIndex = 105;
            this.gb_Trace.TabStop = false;
            this.gb_Trace.Text = "                         ";
            // 
            // txt_SIM_WarmUp
            // 
            this.txt_SIM_WarmUp.Location = new System.Drawing.Point(118, 59);
            this.txt_SIM_WarmUp.Name = "txt_SIM_WarmUp";
            this.txt_SIM_WarmUp.Size = new System.Drawing.Size(48, 20);
            this.txt_SIM_WarmUp.TabIndex = 105;
            this.txt_SIM_WarmUp.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 104;
            this.label6.Text = "Warm-Up  (h) :";
            // 
            // txt_Trace
            // 
            this.txt_Trace.Location = new System.Drawing.Point(118, 28);
            this.txt_Trace.Name = "txt_Trace";
            this.txt_Trace.Size = new System.Drawing.Size(237, 20);
            this.txt_Trace.TabIndex = 102;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 103;
            this.label4.Text = "File trace name :";
            // 
            // tp_Content
            // 
            this.tp_Content.Controls.Add(this.p_Trace);
            this.tp_Content.Location = new System.Drawing.Point(4, 22);
            this.tp_Content.Name = "tp_Content";
            this.tp_Content.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Content.Size = new System.Drawing.Size(583, 332);
            this.tp_Content.TabIndex = 0;
            this.tp_Content.Text = "Trace format";
            this.tp_Content.UseVisualStyleBackColor = true;
            // 
            // p_Trace
            // 
            this.p_Trace.Controls.Add(this.label3);
            this.p_Trace.Controls.Add(this.gb_Content);
            this.p_Trace.Controls.Add(this.lst_Content);
            this.p_Trace.Controls.Add(this.btn_Load);
            this.p_Trace.Controls.Add(this.nudIgnoredColumns);
            this.p_Trace.Controls.Add(this.btn_Delete);
            this.p_Trace.Controls.Add(this.btn_Add);
            this.p_Trace.Controls.Add(this.btn_Save);
            this.p_Trace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_Trace.Enabled = false;
            this.p_Trace.Location = new System.Drawing.Point(3, 3);
            this.p_Trace.Name = "p_Trace";
            this.p_Trace.Size = new System.Drawing.Size(577, 326);
            this.p_Trace.TabIndex = 12;
            // 
            // tp_InputData
            // 
            this.tp_InputData.Controls.Add(this.p_InputData);
            this.tp_InputData.Location = new System.Drawing.Point(4, 22);
            this.tp_InputData.Name = "tp_InputData";
            this.tp_InputData.Padding = new System.Windows.Forms.Padding(3);
            this.tp_InputData.Size = new System.Drawing.Size(583, 332);
            this.tp_InputData.TabIndex = 2;
            this.tp_InputData.Text = "Input data";
            this.tp_InputData.UseVisualStyleBackColor = true;
            // 
            // p_InputData
            // 
            this.p_InputData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_InputData.Location = new System.Drawing.Point(3, 3);
            this.p_InputData.Name = "p_InputData";
            this.p_InputData.Size = new System.Drawing.Size(577, 326);
            this.p_InputData.TabIndex = 0;
            // 
            // tp_Simulation
            // 
            this.tp_Simulation.Controls.Add(this.lbl_CurrentOperation);
            this.tp_Simulation.Controls.Add(this.lbl_SIM_Current);
            this.tp_Simulation.Controls.Add(this.btn_SIM_CancelSimulation);
            this.tp_Simulation.Controls.Add(this.lbl_SIM_Total);
            this.tp_Simulation.Controls.Add(this.lbl_SIM_Current_Name);
            this.tp_Simulation.Controls.Add(this.btn_SIM_Start);
            this.tp_Simulation.Controls.Add(this.pb_SIM_Current);
            this.tp_Simulation.Controls.Add(this.pb_SIM_TotalOperations);
            this.tp_Simulation.Controls.Add(this.gb_SIM_Model);
            this.tp_Simulation.Location = new System.Drawing.Point(4, 22);
            this.tp_Simulation.Name = "tp_Simulation";
            this.tp_Simulation.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Simulation.Size = new System.Drawing.Size(583, 332);
            this.tp_Simulation.TabIndex = 3;
            this.tp_Simulation.Text = "Simulation";
            this.tp_Simulation.UseVisualStyleBackColor = true;
            // 
            // lbl_CurrentOperation
            // 
            this.lbl_CurrentOperation.AutoSize = true;
            this.lbl_CurrentOperation.Location = new System.Drawing.Point(84, 251);
            this.lbl_CurrentOperation.Name = "lbl_CurrentOperation";
            this.lbl_CurrentOperation.Size = new System.Drawing.Size(0, 13);
            this.lbl_CurrentOperation.TabIndex = 49;
            // 
            // lbl_SIM_Current
            // 
            this.lbl_SIM_Current.AutoSize = true;
            this.lbl_SIM_Current.BackColor = System.Drawing.Color.Transparent;
            this.lbl_SIM_Current.Location = new System.Drawing.Point(170, 229);
            this.lbl_SIM_Current.Name = "lbl_SIM_Current";
            this.lbl_SIM_Current.Size = new System.Drawing.Size(0, 13);
            this.lbl_SIM_Current.TabIndex = 46;
            // 
            // btn_SIM_CancelSimulation
            // 
            this.btn_SIM_CancelSimulation.Enabled = false;
            this.btn_SIM_CancelSimulation.Location = new System.Drawing.Point(351, 193);
            this.btn_SIM_CancelSimulation.Name = "btn_SIM_CancelSimulation";
            this.btn_SIM_CancelSimulation.Size = new System.Drawing.Size(112, 27);
            this.btn_SIM_CancelSimulation.TabIndex = 48;
            this.btn_SIM_CancelSimulation.Text = "&Stop simulation";
            this.btn_SIM_CancelSimulation.UseVisualStyleBackColor = true;
            this.btn_SIM_CancelSimulation.Click += new System.EventHandler(this.btn_SIM_CancelSimulation_Click);
            // 
            // lbl_SIM_Total
            // 
            this.lbl_SIM_Total.AutoSize = true;
            this.lbl_SIM_Total.BackColor = System.Drawing.Color.Transparent;
            this.lbl_SIM_Total.Location = new System.Drawing.Point(65, 284);
            this.lbl_SIM_Total.Name = "lbl_SIM_Total";
            this.lbl_SIM_Total.Size = new System.Drawing.Size(89, 13);
            this.lbl_SIM_Total.TabIndex = 45;
            this.lbl_SIM_Total.Text = "Total operations :";
            // 
            // lbl_SIM_Current_Name
            // 
            this.lbl_SIM_Current_Name.AutoSize = true;
            this.lbl_SIM_Current_Name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_SIM_Current_Name.Location = new System.Drawing.Point(70, 229);
            this.lbl_SIM_Current_Name.Name = "lbl_SIM_Current_Name";
            this.lbl_SIM_Current_Name.Size = new System.Drawing.Size(94, 13);
            this.lbl_SIM_Current_Name.TabIndex = 44;
            this.lbl_SIM_Current_Name.Text = "Current operation :";
            // 
            // btn_SIM_Start
            // 
            this.btn_SIM_Start.Location = new System.Drawing.Point(96, 193);
            this.btn_SIM_Start.Name = "btn_SIM_Start";
            this.btn_SIM_Start.Size = new System.Drawing.Size(139, 27);
            this.btn_SIM_Start.TabIndex = 47;
            this.btn_SIM_Start.Text = "&Start simulation";
            this.btn_SIM_Start.UseVisualStyleBackColor = true;
            this.btn_SIM_Start.Click += new System.EventHandler(this.btn_SIM_Start_Click);
            // 
            // pb_SIM_Current
            // 
            this.pb_SIM_Current.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_SIM_Current.Location = new System.Drawing.Point(65, 271);
            this.pb_SIM_Current.Maximum = 10;
            this.pb_SIM_Current.Name = "pb_SIM_Current";
            this.pb_SIM_Current.Size = new System.Drawing.Size(440, 10);
            this.pb_SIM_Current.TabIndex = 43;
            // 
            // pb_SIM_TotalOperations
            // 
            this.pb_SIM_TotalOperations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_SIM_TotalOperations.Location = new System.Drawing.Point(65, 300);
            this.pb_SIM_TotalOperations.Name = "pb_SIM_TotalOperations";
            this.pb_SIM_TotalOperations.Size = new System.Drawing.Size(440, 10);
            this.pb_SIM_TotalOperations.TabIndex = 42;
            // 
            // gb_SIM_Model
            // 
            this.gb_SIM_Model.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_SIM_Model.Controls.Add(this.cb_SIM_DisplayModel);
            this.gb_SIM_Model.Controls.Add(this.lbl_SIM_ModelName);
            this.gb_SIM_Model.Controls.Add(this.btn_SIM_OpenModel);
            this.gb_SIM_Model.Location = new System.Drawing.Point(66, 26);
            this.gb_SIM_Model.Name = "gb_SIM_Model";
            this.gb_SIM_Model.Size = new System.Drawing.Size(440, 86);
            this.gb_SIM_Model.TabIndex = 38;
            this.gb_SIM_Model.TabStop = false;
            this.gb_SIM_Model.Text = "Model";
            // 
            // cb_SIM_DisplayModel
            // 
            this.cb_SIM_DisplayModel.AutoSize = true;
            this.cb_SIM_DisplayModel.Checked = true;
            this.cb_SIM_DisplayModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_SIM_DisplayModel.Location = new System.Drawing.Point(14, 49);
            this.cb_SIM_DisplayModel.Name = "cb_SIM_DisplayModel";
            this.cb_SIM_DisplayModel.Size = new System.Drawing.Size(91, 17);
            this.cb_SIM_DisplayModel.TabIndex = 35;
            this.cb_SIM_DisplayModel.Text = "Display model";
            this.cb_SIM_DisplayModel.UseVisualStyleBackColor = true;
            // 
            // lbl_SIM_ModelName
            // 
            this.lbl_SIM_ModelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SIM_ModelName.Location = new System.Drawing.Point(5, 28);
            this.lbl_SIM_ModelName.Name = "lbl_SIM_ModelName";
            this.lbl_SIM_ModelName.Size = new System.Drawing.Size(366, 18);
            this.lbl_SIM_ModelName.TabIndex = 33;
            // 
            // btn_SIM_OpenModel
            // 
            this.btn_SIM_OpenModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SIM_OpenModel.Location = new System.Drawing.Point(391, 23);
            this.btn_SIM_OpenModel.Name = "btn_SIM_OpenModel";
            this.btn_SIM_OpenModel.Size = new System.Drawing.Size(25, 23);
            this.btn_SIM_OpenModel.TabIndex = 34;
            this.btn_SIM_OpenModel.Text = "...";
            this.btn_SIM_OpenModel.UseVisualStyleBackColor = true;
            this.btn_SIM_OpenModel.Click += new System.EventHandler(this.btn_SIM_OpenModel_Click);
            // 
            // lbl_ScenarioName
            // 
            this.lbl_ScenarioName.AutoSize = true;
            this.lbl_ScenarioName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ScenarioName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ScenarioName.Location = new System.Drawing.Point(78, 15);
            this.lbl_ScenarioName.Name = "lbl_ScenarioName";
            this.lbl_ScenarioName.Size = new System.Drawing.Size(126, 17);
            this.lbl_ScenarioName.TabIndex = 103;
            this.lbl_ScenarioName.Text = "Scenario name :";
            // 
            // cb_Scenario
            // 
            this.cb_Scenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Scenario.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Scenario.FormattingEnabled = true;
            this.cb_Scenario.Location = new System.Drawing.Point(223, 10);
            this.cb_Scenario.Name = "cb_Scenario";
            this.cb_Scenario.Size = new System.Drawing.Size(316, 27);
            this.cb_Scenario.TabIndex = 105;
            this.cb_Scenario.SelectedIndexChanged += new System.EventHandler(this.cb_Scenario_SelectedIndexChanged);
            // 
            // TraceAnalyser
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(636, 494);
            this.Controls.Add(this.cb_Scenario);
            this.Controls.Add(this.lbl_ScenarioName);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(642, 490);
            this.Name = "TraceAnalyser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Appearence";
            this.gb_Content.ResumeLayout(false);
            this.gb_Content.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIgnoredColumns)).EndInit();
            this.gb_Dates.ResumeLayout(false);
            this.gb_Dates.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tp_Trace.ResumeLayout(false);
            this.p_Main.ResumeLayout(false);
            this.p_Main.PerformLayout();
            this.gb_PP_Seed.ResumeLayout(false);
            this.gb_PP_Seed.PerformLayout();
            this.gb_Trace.ResumeLayout(false);
            this.gb_Trace.PerformLayout();
            this.tp_Content.ResumeLayout(false);
            this.p_Trace.ResumeLayout(false);
            this.p_Trace.PerformLayout();
            this.tp_InputData.ResumeLayout(false);
            this.tp_Simulation.ResumeLayout(false);
            this.tp_Simulation.PerformLayout();
            this.gb_SIM_Model.ResumeLayout(false);
            this.gb_SIM_Model.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ListBox lst_Content;
        private System.Windows.Forms.GroupBox gb_Content;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.TextBox txt_DisplayedName;
        private System.Windows.Forms.TextBox txt_TraceName;
        private System.Windows.Forms.CheckBox cb_Indexed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbWorkingPoint;
        private System.Windows.Forms.RadioButton rbTrackingPoint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudIgnoredColumns;
        private System.Windows.Forms.CheckBox cb_Queue;
        private System.Windows.Forms.Button btn_Load;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.OpenFileDialog ofd_Open;
        private System.Windows.Forms.SaveFileDialog sfd_SaveDialog;
        private System.Windows.Forms.GroupBox gb_Dates;
        private System.Windows.Forms.DateTimePicker dtp_BeginTime;
        private System.Windows.Forms.Label lbl_BeginTime;
        private System.Windows.Forms.Label lbl_EndTime;
        private System.Windows.Forms.ComboBox cb_SamplingStep;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtp_EndTime;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_Content;
        private System.Windows.Forms.TabPage tp_Trace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Trace;
        private System.Windows.Forms.Label lbl_ScenarioName;
        private System.Windows.Forms.ComboBox cb_Scenario;
        private System.Windows.Forms.TabPage tp_InputData;
        private System.Windows.Forms.Panel p_InputData;
        private System.Windows.Forms.TabPage tp_Simulation;
        private System.Windows.Forms.GroupBox gb_SIM_Model;
        private System.Windows.Forms.CheckBox cb_SIM_DisplayModel;
        private System.Windows.Forms.Label lbl_SIM_ModelName;
        private System.Windows.Forms.Button btn_SIM_OpenModel;
        private System.Windows.Forms.Label lbl_CurrentOperation;
        private System.Windows.Forms.Label lbl_SIM_Current;
        private System.Windows.Forms.Button btn_SIM_CancelSimulation;
        private System.Windows.Forms.Label lbl_SIM_Total;
        private System.Windows.Forms.Label lbl_SIM_Current_Name;
        private System.Windows.Forms.Button btn_SIM_Start;
        private System.Windows.Forms.ProgressBar pb_SIM_Current;
        private System.Windows.Forms.ProgressBar pb_SIM_TotalOperations;
        private System.Windows.Forms.GroupBox gb_PP_Seed;
        private System.Windows.Forms.MaskedTextBox mb_PP_UseDefinedSeed;
        private System.Windows.Forms.Button btn_DefineSeeds;
        private System.Windows.Forms.CheckBox cb_PP_UseDefinedSeed;
        private System.Windows.Forms.CheckBox cb_UseSameSeed;
        private System.Windows.Forms.GroupBox gb_Trace;
        private System.Windows.Forms.CheckBox cb_ReadTrace;
        private System.Windows.Forms.Panel p_Trace;
        private System.Windows.Forms.TextBox txt_SIM_WarmUp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel p_Main;
    }
}