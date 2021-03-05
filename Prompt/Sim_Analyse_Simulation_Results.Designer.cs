namespace SIMCORE_TOOL.Prompt
{
    partial class Sim_Analyse_Simulation_Results
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sim_Analyse_Simulation_Results));
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.ofd_Open = new System.Windows.Forms.OpenFileDialog();
            this.btn_Open = new System.Windows.Forms.Button();
            this.tb_File = new System.Windows.Forms.TextBox();
            this.gb_BagtraceFile = new System.Windows.Forms.GroupBox();
            this.copyOutputTablesCheckBox = new System.Windows.Forms.CheckBox();
            this.editResultFiltersButton = new System.Windows.Forms.Button();
            this.generateMUPSegregationCheckBox = new System.Windows.Forms.CheckBox();
            this.generateGroupISTCheckBox = new System.Windows.Forms.CheckBox();
            this.generateLocalISTCheckBox = new System.Windows.Forms.CheckBox();
            this.gb_Pax = new System.Windows.Forms.GroupBox();
            this.tb_PaxTrace = new System.Windows.Forms.TextBox();
            this.btn_PaxOpen = new System.Windows.Forms.Button();
            this.cb_Scenario = new System.Windows.Forms.ComboBox();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.tt_Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.gb_SIM_AnalysisSettings = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.thirdLevelTextBox = new System.Windows.Forms.TextBox();
            this.secondLevelTextBox = new System.Windows.Forms.TextBox();
            this.firstLevelTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.analysisRangeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_EndTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_EndTime = new System.Windows.Forms.Label();
            this.dtp_BeginTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_BeginTime = new System.Windows.Forms.Label();
            this.txt_SIM_WarmUp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_SIM_AnalyseStep = new System.Windows.Forms.ComboBox();
            this.lbl_Step = new System.Windows.Forms.Label();
            this.gb_BagtraceFile.SuspendLayout();
            this.gb_Pax.SuspendLayout();
            this.gb_SIM_AnalysisSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(35, 464);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(82, 25);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "&Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(377, 464);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(82, 25);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // ofd_Open
            // 
            this.ofd_Open.FileName = "openFileDialog1";
            // 
            // btn_Open
            // 
            this.btn_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Open.Location = new System.Drawing.Point(398, 17);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(26, 23);
            this.btn_Open.TabIndex = 3;
            this.btn_Open.Text = "...";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // tb_File
            // 
            this.tb_File.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_File.Location = new System.Drawing.Point(11, 19);
            this.tb_File.Name = "tb_File";
            this.tb_File.Size = new System.Drawing.Size(381, 22);
            this.tb_File.TabIndex = 7;
            this.tb_File.TextChanged += new System.EventHandler(this.tb_File_TextChanged);
            // 
            // gb_BagtraceFile
            // 
            this.gb_BagtraceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_BagtraceFile.BackColor = System.Drawing.Color.Transparent;
            this.gb_BagtraceFile.Controls.Add(this.copyOutputTablesCheckBox);
            this.gb_BagtraceFile.Controls.Add(this.editResultFiltersButton);
            this.gb_BagtraceFile.Controls.Add(this.generateMUPSegregationCheckBox);
            this.gb_BagtraceFile.Controls.Add(this.generateGroupISTCheckBox);
            this.gb_BagtraceFile.Controls.Add(this.generateLocalISTCheckBox);
            this.gb_BagtraceFile.Controls.Add(this.tb_File);
            this.gb_BagtraceFile.Controls.Add(this.btn_Open);
            this.gb_BagtraceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_BagtraceFile.Location = new System.Drawing.Point(35, 73);
            this.gb_BagtraceFile.Name = "gb_BagtraceFile";
            this.gb_BagtraceFile.Size = new System.Drawing.Size(432, 140);
            this.gb_BagtraceFile.TabIndex = 8;
            this.gb_BagtraceFile.TabStop = false;
            this.gb_BagtraceFile.Text = "Bagtrace file";
            // 
            // copyOutputTablesCheckBox
            // 
            this.copyOutputTablesCheckBox.AutoSize = true;
            this.copyOutputTablesCheckBox.Location = new System.Drawing.Point(180, 87);
            this.copyOutputTablesCheckBox.Name = "copyOutputTablesCheckBox";
            this.copyOutputTablesCheckBox.Size = new System.Drawing.Size(253, 20);
            this.copyOutputTablesCheckBox.TabIndex = 12;
            this.copyOutputTablesCheckBox.Text = "Copy output tables in the project folder";
            this.copyOutputTablesCheckBox.UseVisualStyleBackColor = true;
            // 
            // editResultFiltersButton
            // 
            this.editResultFiltersButton.Location = new System.Drawing.Point(224, 111);
            this.editResultFiltersButton.Name = "editResultFiltersButton";
            this.editResultFiltersButton.Size = new System.Drawing.Size(151, 23);
            this.editResultFiltersButton.TabIndex = 11;
            this.editResultFiltersButton.Text = "Edit Result Filters";
            this.editResultFiltersButton.UseVisualStyleBackColor = true;
            this.editResultFiltersButton.Click += new System.EventHandler(this.editResultFiltersButton_Click);
            // 
            // generateMUPSegregationCheckBox
            // 
            this.generateMUPSegregationCheckBox.AutoSize = true;
            this.generateMUPSegregationCheckBox.Location = new System.Drawing.Point(180, 61);
            this.generateMUPSegregationCheckBox.Name = "generateMUPSegregationCheckBox";
            this.generateMUPSegregationCheckBox.Size = new System.Drawing.Size(214, 20);
            this.generateMUPSegregationCheckBox.TabIndex = 10;
            this.generateMUPSegregationCheckBox.Text = "Generate Make-up segregation";
            this.generateMUPSegregationCheckBox.UseVisualStyleBackColor = true;
            // 
            // generateGroupISTCheckBox
            // 
            this.generateGroupISTCheckBox.AutoSize = true;
            this.generateGroupISTCheckBox.Location = new System.Drawing.Point(5, 87);
            this.generateGroupISTCheckBox.Name = "generateGroupISTCheckBox";
            this.generateGroupISTCheckBox.Size = new System.Drawing.Size(170, 20);
            this.generateGroupISTCheckBox.TabIndex = 9;
            this.generateGroupISTCheckBox.Text = "Generate IST for groups";
            this.generateGroupISTCheckBox.UseVisualStyleBackColor = true;
            // 
            // generateLocalISTCheckBox
            // 
            this.generateLocalISTCheckBox.AutoSize = true;
            this.generateLocalISTCheckBox.Location = new System.Drawing.Point(5, 61);
            this.generateLocalISTCheckBox.Name = "generateLocalISTCheckBox";
            this.generateLocalISTCheckBox.Size = new System.Drawing.Size(174, 20);
            this.generateLocalISTCheckBox.TabIndex = 8;
            this.generateLocalISTCheckBox.Text = "Generate IST for stations";
            this.generateLocalISTCheckBox.UseVisualStyleBackColor = true;
            // 
            // gb_Pax
            // 
            this.gb_Pax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Pax.BackColor = System.Drawing.Color.Transparent;
            this.gb_Pax.Controls.Add(this.tb_PaxTrace);
            this.gb_Pax.Controls.Add(this.btn_PaxOpen);
            this.gb_Pax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Pax.Location = new System.Drawing.Point(35, 73);
            this.gb_Pax.Name = "gb_Pax";
            this.gb_Pax.Size = new System.Drawing.Size(432, 55);
            this.gb_Pax.TabIndex = 42;
            this.gb_Pax.TabStop = false;
            this.gb_Pax.Text = "Paxtrace file";
            // 
            // tb_PaxTrace
            // 
            this.tb_PaxTrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_PaxTrace.Location = new System.Drawing.Point(11, 19);
            this.tb_PaxTrace.Name = "tb_PaxTrace";
            this.tb_PaxTrace.Size = new System.Drawing.Size(381, 22);
            this.tb_PaxTrace.TabIndex = 7;
            this.tb_PaxTrace.TextChanged += new System.EventHandler(this.tb_File_TextChanged);
            // 
            // btn_PaxOpen
            // 
            this.btn_PaxOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PaxOpen.Location = new System.Drawing.Point(398, 17);
            this.btn_PaxOpen.Name = "btn_PaxOpen";
            this.btn_PaxOpen.Size = new System.Drawing.Size(26, 23);
            this.btn_PaxOpen.TabIndex = 3;
            this.btn_PaxOpen.Text = "...";
            this.btn_PaxOpen.UseVisualStyleBackColor = true;
            this.btn_PaxOpen.Click += new System.EventHandler(this.btn_OpenPax_Click);
            // 
            // cb_Scenario
            // 
            this.cb_Scenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Scenario.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Scenario.FormattingEnabled = true;
            this.cb_Scenario.Location = new System.Drawing.Point(216, 25);
            this.cb_Scenario.Name = "cb_Scenario";
            this.cb_Scenario.Size = new System.Drawing.Size(227, 27);
            this.cb_Scenario.TabIndex = 14;
            this.cb_Scenario.SelectedIndexChanged += new System.EventHandler(this.cb_Scenario_SelectedIndexChanged);
            this.cb_Scenario.TextChanged += new System.EventHandler(this.cb_Scenario_TextChanged);
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Name.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_Name.Location = new System.Drawing.Point(68, 28);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(142, 19);
            this.lbl_Name.TabIndex = 15;
            this.lbl_Name.Text = "Scenarios name :";
            // 
            // gb_SIM_AnalysisSettings
            // 
            this.gb_SIM_AnalysisSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_SIM_AnalysisSettings.BackColor = System.Drawing.Color.Transparent;
            this.gb_SIM_AnalysisSettings.Controls.Add(this.groupBox1);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.analysisRangeComboBox);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.label1);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.dtp_EndTime);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.lbl_EndTime);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.dtp_BeginTime);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.lbl_BeginTime);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.txt_SIM_WarmUp);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.label6);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.cb_SIM_AnalyseStep);
            this.gb_SIM_AnalysisSettings.Controls.Add(this.lbl_Step);
            this.gb_SIM_AnalysisSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_SIM_AnalysisSettings.Location = new System.Drawing.Point(35, 220);
            this.gb_SIM_AnalysisSettings.Name = "gb_SIM_AnalysisSettings";
            this.gb_SIM_AnalysisSettings.Size = new System.Drawing.Size(432, 238);
            this.gb_SIM_AnalysisSettings.TabIndex = 41;
            this.gb_SIM_AnalysisSettings.TabStop = false;
            this.gb_SIM_AnalysisSettings.Text = "Analysis settings";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.thirdLevelTextBox);
            this.groupBox1.Controls.Add(this.secondLevelTextBox);
            this.groupBox1.Controls.Add(this.firstLevelTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(11, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 63);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Percentiles (%)";
            // 
            // thirdLevelTextBox
            // 
            this.thirdLevelTextBox.Location = new System.Drawing.Point(319, 26);
            this.thirdLevelTextBox.MaxLength = 0;
            this.thirdLevelTextBox.Name = "thirdLevelTextBox";
            this.thirdLevelTextBox.Size = new System.Drawing.Size(45, 22);
            this.thirdLevelTextBox.TabIndex = 5;
            this.thirdLevelTextBox.TextChanged += new System.EventHandler(this.levelTextBox_TextChanged);
            this.thirdLevelTextBox.Leave += new System.EventHandler(this.thirdLevelTextBox_Leave);
            this.thirdLevelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.levelTextBox_KeyPress);
            // 
            // secondLevelTextBox
            // 
            this.secondLevelTextBox.Location = new System.Drawing.Point(193, 26);
            this.secondLevelTextBox.MaxLength = 0;
            this.secondLevelTextBox.Name = "secondLevelTextBox";
            this.secondLevelTextBox.Size = new System.Drawing.Size(45, 22);
            this.secondLevelTextBox.TabIndex = 4;
            this.secondLevelTextBox.TextChanged += new System.EventHandler(this.levelTextBox_TextChanged);
            this.secondLevelTextBox.Leave += new System.EventHandler(this.secondLevelTextBox_Leave);
            this.secondLevelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.levelTextBox_KeyPress);
            // 
            // firstLevelTextBox
            // 
            this.firstLevelTextBox.Location = new System.Drawing.Point(45, 26);
            this.firstLevelTextBox.MaxLength = 0;
            this.firstLevelTextBox.Name = "firstLevelTextBox";
            this.firstLevelTextBox.Size = new System.Drawing.Size(45, 22);
            this.firstLevelTextBox.TabIndex = 3;
            this.firstLevelTextBox.TextChanged += new System.EventHandler(this.levelTextBox_TextChanged);
            this.firstLevelTextBox.Leave += new System.EventHandler(this.firstLevelTextBox_Leave);
            this.firstLevelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.levelTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(274, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Third";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Second";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "First";
            // 
            // analysisRangeComboBox
            // 
            this.analysisRangeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.analysisRangeComboBox.FormattingEnabled = true;
            this.analysisRangeComboBox.Items.AddRange(new object[] {
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
            this.analysisRangeComboBox.Location = new System.Drawing.Point(201, 109);
            this.analysisRangeComboBox.Name = "analysisRangeComboBox";
            this.analysisRangeComboBox.Size = new System.Drawing.Size(48, 24);
            this.analysisRangeComboBox.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 16);
            this.label1.TabIndex = 46;
            this.label1.Text = "Analysis Range (min) :";
            // 
            // dtp_EndTime
            // 
            this.dtp_EndTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_EndTime.CustomFormat = "M/d/yyyy h:mm tt";
            this.dtp_EndTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_EndTime.Location = new System.Drawing.Point(201, 49);
            this.dtp_EndTime.Name = "dtp_EndTime";
            this.dtp_EndTime.Size = new System.Drawing.Size(162, 22);
            this.dtp_EndTime.TabIndex = 44;
            // 
            // lbl_EndTime
            // 
            this.lbl_EndTime.AutoSize = true;
            this.lbl_EndTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndTime.Location = new System.Drawing.Point(87, 53);
            this.lbl_EndTime.Name = "lbl_EndTime";
            this.lbl_EndTime.Size = new System.Drawing.Size(68, 16);
            this.lbl_EndTime.TabIndex = 45;
            this.lbl_EndTime.Text = "End date :";
            // 
            // dtp_BeginTime
            // 
            this.dtp_BeginTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_BeginTime.CustomFormat = "M/d/yyyy h:mm tt";
            this.dtp_BeginTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_BeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_BeginTime.Location = new System.Drawing.Point(201, 21);
            this.dtp_BeginTime.Name = "dtp_BeginTime";
            this.dtp_BeginTime.Size = new System.Drawing.Size(162, 22);
            this.dtp_BeginTime.TabIndex = 43;
            this.dtp_BeginTime.ValueChanged += new System.EventHandler(this.dtp_BeginTime_ValueChanged);
            // 
            // lbl_BeginTime
            // 
            this.lbl_BeginTime.AutoSize = true;
            this.lbl_BeginTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_BeginTime.Location = new System.Drawing.Point(76, 25);
            this.lbl_BeginTime.Name = "lbl_BeginTime";
            this.lbl_BeginTime.Size = new System.Drawing.Size(79, 16);
            this.lbl_BeginTime.TabIndex = 42;
            this.lbl_BeginTime.Text = "Begin date :";
            // 
            // txt_SIM_WarmUp
            // 
            this.txt_SIM_WarmUp.Location = new System.Drawing.Point(201, 141);
            this.txt_SIM_WarmUp.Name = "txt_SIM_WarmUp";
            this.txt_SIM_WarmUp.Size = new System.Drawing.Size(48, 22);
            this.txt_SIM_WarmUp.TabIndex = 41;
            this.txt_SIM_WarmUp.Text = "0";
            this.txt_SIM_WarmUp.TextChanged += new System.EventHandler(this.txt_SIM_WarmUp_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 40;
            this.label6.Text = "Warm-Up  (h) :";
            // 
            // cb_SIM_AnalyseStep
            // 
            this.cb_SIM_AnalyseStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SIM_AnalyseStep.FormattingEnabled = true;
            this.cb_SIM_AnalyseStep.Items.AddRange(new object[] {
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
            this.cb_SIM_AnalyseStep.Location = new System.Drawing.Point(201, 77);
            this.cb_SIM_AnalyseStep.Name = "cb_SIM_AnalyseStep";
            this.cb_SIM_AnalyseStep.Size = new System.Drawing.Size(48, 24);
            this.cb_SIM_AnalyseStep.TabIndex = 31;
            this.cb_SIM_AnalyseStep.SelectedIndexChanged += new System.EventHandler(this.cb_SIM_AnalyseStep_SelectedIndexChanged);
            // 
            // lbl_Step
            // 
            this.lbl_Step.AutoSize = true;
            this.lbl_Step.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Step.Location = new System.Drawing.Point(29, 80);
            this.lbl_Step.Name = "lbl_Step";
            this.lbl_Step.Size = new System.Drawing.Size(132, 16);
            this.lbl_Step.TabIndex = 30;
            this.lbl_Step.Text = "Sampling step (min) :";
            // 
            // Sim_Analyse_Simulation_Results
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(490, 512);
            this.Controls.Add(this.gb_Pax);
            this.Controls.Add(this.gb_SIM_AnalysisSettings);
            this.Controls.Add(this.cb_Scenario);
            this.Controls.Add(this.lbl_Name);
            this.Controls.Add(this.gb_BagtraceFile);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(485, 379);
            this.Name = "Sim_Analyse_Simulation_Results";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analyse simulation results";
            this.Shown += new System.EventHandler(this.Sim_Analyse_Simulation_Results_Shown);
            this.gb_BagtraceFile.ResumeLayout(false);
            this.gb_BagtraceFile.PerformLayout();
            this.gb_Pax.ResumeLayout(false);
            this.gb_Pax.PerformLayout();
            this.gb_SIM_AnalysisSettings.ResumeLayout(false);
            this.gb_SIM_AnalysisSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.OpenFileDialog ofd_Open;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.TextBox tb_File;
        private System.Windows.Forms.GroupBox gb_BagtraceFile;
        private System.Windows.Forms.ComboBox cb_Scenario;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.ToolTip tt_Tooltip;
        private System.Windows.Forms.GroupBox gb_SIM_AnalysisSettings;
        private System.Windows.Forms.TextBox txt_SIM_WarmUp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_SIM_AnalyseStep;
        private System.Windows.Forms.Label lbl_Step;
        private System.Windows.Forms.DateTimePicker dtp_EndTime;
        private System.Windows.Forms.Label lbl_EndTime;
        private System.Windows.Forms.DateTimePicker dtp_BeginTime;
        private System.Windows.Forms.Label lbl_BeginTime;
        private System.Windows.Forms.GroupBox gb_Pax;
        private System.Windows.Forms.TextBox tb_PaxTrace;
        private System.Windows.Forms.Button btn_PaxOpen;
        private System.Windows.Forms.ComboBox analysisRangeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox generateLocalISTCheckBox;
        private System.Windows.Forms.CheckBox generateGroupISTCheckBox;
        private System.Windows.Forms.CheckBox generateMUPSegregationCheckBox;
        private System.Windows.Forms.Button editResultFiltersButton;
        private System.Windows.Forms.CheckBox copyOutputTablesCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox thirdLevelTextBox;
        private System.Windows.Forms.TextBox secondLevelTextBox;
        private System.Windows.Forms.TextBox firstLevelTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}