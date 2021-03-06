namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Allocation_MakeUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Allocation_MakeUp));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txt_TitleTable = new System.Windows.Forms.TextBox();
            this.cb_FPDTable = new System.Windows.Forms.ComboBox();
            this.cb_AllocationRules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_EndTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_EndTime = new System.Windows.Forms.Label();
            this.dtp_BeginTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_BeginTime = new System.Windows.Forms.Label();
            this.txt_TimeStep = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_Terminal = new System.Windows.Forms.ComboBox();
            this.cb_AllocateFlightPlan = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Separation = new System.Windows.Forms.TextBox();
            this.cb_ColorFC = new System.Windows.Forms.CheckBox();
            this.cb_Algorithme = new System.Windows.Forms.CheckBox();
            this.cb_AirlineColor = new System.Windows.Forms.CheckBox();
            this.cb_ColorHandlers = new System.Windows.Forms.CheckBox();
            this.cb_AirlineCode = new System.Windows.Forms.ComboBox();
            this.lbl_Airline = new System.Windows.Forms.Label();
            this.lbl_Aircraft = new System.Windows.Forms.Label();
            this.cb_Aircraft = new System.Windows.Forms.ComboBox();
            this.cb_FPAsBasis = new System.Windows.Forms.CheckBox();
            this.cb_LooseULD = new System.Windows.Forms.CheckBox();
            this.gb_Segregation = new System.Windows.Forms.GroupBox();
            this.cb_GroundHandlersSegregation = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_TerminalColumn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gb_Segregation.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(50, 506);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 12;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(450, 506);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // txt_TitleTable
            // 
            this.txt_TitleTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_TitleTable.Location = new System.Drawing.Point(188, 45);
            this.txt_TitleTable.Name = "txt_TitleTable";
            this.txt_TitleTable.Size = new System.Drawing.Size(368, 20);
            this.txt_TitleTable.TabIndex = 1;
            // 
            // cb_FPDTable
            // 
            this.cb_FPDTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_FPDTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FPDTable.FormattingEnabled = true;
            this.cb_FPDTable.Location = new System.Drawing.Point(188, 100);
            this.cb_FPDTable.Name = "cb_FPDTable";
            this.cb_FPDTable.Size = new System.Drawing.Size(368, 21);
            this.cb_FPDTable.TabIndex = 2;
            this.cb_FPDTable.SelectedIndexChanged += new System.EventHandler(this.cb_FPDTable_SelectedIndexChanged);
            // 
            // cb_AllocationRules
            // 
            this.cb_AllocationRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_AllocationRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AllocationRules.FormattingEnabled = true;
            this.cb_AllocationRules.Location = new System.Drawing.Point(188, 183);
            this.cb_AllocationRules.Name = "cb_AllocationRules";
            this.cb_AllocationRules.Size = new System.Drawing.Size(368, 21);
            this.cb_AllocationRules.TabIndex = 3;
            this.cb_AllocationRules.SelectedIndexChanged += new System.EventHandler(this.cb_AllocationRules_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(33, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Allocation Make-Up rules";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(33, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Flight plan departure";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(33, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Table name :";
            // 
            // dtp_EndTime
            // 
            this.dtp_EndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_EndTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_EndTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_EndTime.Location = new System.Drawing.Point(188, 264);
            this.dtp_EndTime.Name = "dtp_EndTime";
            this.dtp_EndTime.Size = new System.Drawing.Size(368, 20);
            this.dtp_EndTime.TabIndex = 7;
            // 
            // lbl_EndTime
            // 
            this.lbl_EndTime.AutoSize = true;
            this.lbl_EndTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndTime.Location = new System.Drawing.Point(33, 268);
            this.lbl_EndTime.Name = "lbl_EndTime";
            this.lbl_EndTime.Size = new System.Drawing.Size(50, 13);
            this.lbl_EndTime.TabIndex = 22;
            this.lbl_EndTime.Text = "End date";
            // 
            // dtp_BeginTime
            // 
            this.dtp_BeginTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_BeginTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_BeginTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_BeginTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_BeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_BeginTime.Location = new System.Drawing.Point(188, 238);
            this.dtp_BeginTime.Name = "dtp_BeginTime";
            this.dtp_BeginTime.Size = new System.Drawing.Size(368, 20);
            this.dtp_BeginTime.TabIndex = 6;
            // 
            // lbl_BeginTime
            // 
            this.lbl_BeginTime.AutoSize = true;
            this.lbl_BeginTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_BeginTime.Location = new System.Drawing.Point(33, 242);
            this.lbl_BeginTime.Name = "lbl_BeginTime";
            this.lbl_BeginTime.Size = new System.Drawing.Size(58, 13);
            this.lbl_BeginTime.TabIndex = 19;
            this.lbl_BeginTime.Text = "Begin date";
            // 
            // txt_TimeStep
            // 
            this.txt_TimeStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_TimeStep.Location = new System.Drawing.Point(188, 290);
            this.txt_TimeStep.Name = "txt_TimeStep";
            this.txt_TimeStep.Size = new System.Drawing.Size(368, 20);
            this.txt_TimeStep.TabIndex = 8;
            this.txt_TimeStep.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(33, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Time step (min)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(33, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Terminal information :";
            // 
            // cb_Terminal
            // 
            this.cb_Terminal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Terminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Terminal.FormattingEnabled = true;
            this.cb_Terminal.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cb_Terminal.Location = new System.Drawing.Point(480, 210);
            this.cb_Terminal.Name = "cb_Terminal";
            this.cb_Terminal.Size = new System.Drawing.Size(76, 21);
            this.cb_Terminal.TabIndex = 5;
            // 
            // cb_AllocateFlightPlan
            // 
            this.cb_AllocateFlightPlan.AutoSize = true;
            this.cb_AllocateFlightPlan.BackColor = System.Drawing.Color.Transparent;
            this.cb_AllocateFlightPlan.Location = new System.Drawing.Point(287, 353);
            this.cb_AllocateFlightPlan.Name = "cb_AllocateFlightPlan";
            this.cb_AllocateFlightPlan.Size = new System.Drawing.Size(187, 17);
            this.cb_AllocateFlightPlan.TabIndex = 10;
            this.cb_AllocateFlightPlan.Text = "Update FPD with allocated values";
            this.cb_AllocateFlightPlan.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(33, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Delay between 2 flights (min)";
            // 
            // txt_Separation
            // 
            this.txt_Separation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Separation.Location = new System.Drawing.Point(188, 316);
            this.txt_Separation.Name = "txt_Separation";
            this.txt_Separation.Size = new System.Drawing.Size(368, 20);
            this.txt_Separation.TabIndex = 9;
            this.txt_Separation.Text = "10";
            // 
            // cb_ColorFC
            // 
            this.cb_ColorFC.AutoSize = true;
            this.cb_ColorFC.BackColor = System.Drawing.Color.Transparent;
            this.cb_ColorFC.Checked = true;
            this.cb_ColorFC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_ColorFC.Location = new System.Drawing.Point(6, 19);
            this.cb_ColorFC.Name = "cb_ColorFC";
            this.cb_ColorFC.Size = new System.Drawing.Size(166, 17);
            this.cb_ColorFC.TabIndex = 11;
            this.cb_ColorFC.Text = "Define color by flight category";
            this.cb_ColorFC.UseVisualStyleBackColor = false;
            // 
            // cb_Algorithme
            // 
            this.cb_Algorithme.AutoSize = true;
            this.cb_Algorithme.BackColor = System.Drawing.Color.Transparent;
            this.cb_Algorithme.Location = new System.Drawing.Point(12, 19);
            this.cb_Algorithme.Name = "cb_Algorithme";
            this.cb_Algorithme.Size = new System.Drawing.Size(148, 17);
            this.cb_Algorithme.TabIndex = 32;
            this.cb_Algorithme.Text = "Allocate biggest flight first.";
            this.cb_Algorithme.UseVisualStyleBackColor = false;
            // 
            // cb_AirlineColor
            // 
            this.cb_AirlineColor.AutoSize = true;
            this.cb_AirlineColor.BackColor = System.Drawing.Color.Transparent;
            this.cb_AirlineColor.Checked = true;
            this.cb_AirlineColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_AirlineColor.Location = new System.Drawing.Point(6, 42);
            this.cb_AirlineColor.Name = "cb_AirlineColor";
            this.cb_AirlineColor.Size = new System.Drawing.Size(155, 17);
            this.cb_AirlineColor.TabIndex = 33;
            this.cb_AirlineColor.Text = "Define color by Airline code";
            this.cb_AirlineColor.UseVisualStyleBackColor = false;
            // 
            // cb_ColorHandlers
            // 
            this.cb_ColorHandlers.AutoSize = true;
            this.cb_ColorHandlers.BackColor = System.Drawing.Color.Transparent;
            this.cb_ColorHandlers.Checked = true;
            this.cb_ColorHandlers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_ColorHandlers.Location = new System.Drawing.Point(6, 65);
            this.cb_ColorHandlers.Name = "cb_ColorHandlers";
            this.cb_ColorHandlers.Size = new System.Drawing.Size(183, 17);
            this.cb_ColorHandlers.TabIndex = 34;
            this.cb_ColorHandlers.Text = "Define color by Ground Handlers ";
            this.cb_ColorHandlers.UseVisualStyleBackColor = false;
            // 
            // cb_AirlineCode
            // 
            this.cb_AirlineCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_AirlineCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AirlineCode.FormattingEnabled = true;
            this.cb_AirlineCode.Location = new System.Drawing.Point(188, 129);
            this.cb_AirlineCode.Name = "cb_AirlineCode";
            this.cb_AirlineCode.Size = new System.Drawing.Size(368, 21);
            this.cb_AirlineCode.TabIndex = 30;
            // 
            // lbl_Airline
            // 
            this.lbl_Airline.AutoSize = true;
            this.lbl_Airline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Airline.Location = new System.Drawing.Point(33, 132);
            this.lbl_Airline.Name = "lbl_Airline";
            this.lbl_Airline.Size = new System.Drawing.Size(92, 13);
            this.lbl_Airline.TabIndex = 31;
            this.lbl_Airline.Text = "Airline code Table";
            // 
            // lbl_Aircraft
            // 
            this.lbl_Aircraft.AutoSize = true;
            this.lbl_Aircraft.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Aircraft.Location = new System.Drawing.Point(33, 159);
            this.lbl_Aircraft.Name = "lbl_Aircraft";
            this.lbl_Aircraft.Size = new System.Drawing.Size(93, 13);
            this.lbl_Aircraft.TabIndex = 36;
            this.lbl_Aircraft.Text = "Aircraft type Table";
            // 
            // cb_Aircraft
            // 
            this.cb_Aircraft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Aircraft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Aircraft.FormattingEnabled = true;
            this.cb_Aircraft.Location = new System.Drawing.Point(188, 156);
            this.cb_Aircraft.Name = "cb_Aircraft";
            this.cb_Aircraft.Size = new System.Drawing.Size(368, 21);
            this.cb_Aircraft.TabIndex = 35;
            // 
            // cb_FPAsBasis
            // 
            this.cb_FPAsBasis.AutoSize = true;
            this.cb_FPAsBasis.BackColor = System.Drawing.Color.Transparent;
            this.cb_FPAsBasis.Location = new System.Drawing.Point(62, 353);
            this.cb_FPAsBasis.Name = "cb_FPAsBasis";
            this.cb_FPAsBasis.Size = new System.Drawing.Size(150, 17);
            this.cb_FPAsBasis.TabIndex = 37;
            this.cb_FPAsBasis.Text = "Use FP allocation as basis";
            this.cb_FPAsBasis.UseVisualStyleBackColor = false;
            this.cb_FPAsBasis.CheckedChanged += new System.EventHandler(this.cb_FPAsBasis_CheckedChanged);
            // 
            // cb_LooseULD
            // 
            this.cb_LooseULD.AutoSize = true;
            this.cb_LooseULD.BackColor = System.Drawing.Color.Transparent;
            this.cb_LooseULD.Location = new System.Drawing.Point(12, 42);
            this.cb_LooseULD.Name = "cb_LooseULD";
            this.cb_LooseULD.Size = new System.Drawing.Size(88, 17);
            this.cb_LooseULD.TabIndex = 38;
            this.cb_LooseULD.Text = "Loose / ULD";
            this.cb_LooseULD.UseVisualStyleBackColor = false;
            this.cb_LooseULD.CheckedChanged += new System.EventHandler(this.cb_LooseULD_CheckedChanged);
            // 
            // gb_Segregation
            // 
            this.gb_Segregation.BackColor = System.Drawing.Color.Transparent;
            this.gb_Segregation.Controls.Add(this.cb_GroundHandlersSegregation);
            this.gb_Segregation.Controls.Add(this.cb_LooseULD);
            this.gb_Segregation.Controls.Add(this.cb_Algorithme);
            this.gb_Segregation.Location = new System.Drawing.Point(265, 384);
            this.gb_Segregation.Name = "gb_Segregation";
            this.gb_Segregation.Size = new System.Drawing.Size(209, 91);
            this.gb_Segregation.TabIndex = 39;
            this.gb_Segregation.TabStop = false;
            this.gb_Segregation.Text = "Segregation";
            // 
            // cb_GroundHandlersSegregation
            // 
            this.cb_GroundHandlersSegregation.AutoSize = true;
            this.cb_GroundHandlersSegregation.BackColor = System.Drawing.Color.Transparent;
            this.cb_GroundHandlersSegregation.Location = new System.Drawing.Point(12, 65);
            this.cb_GroundHandlersSegregation.Name = "cb_GroundHandlersSegregation";
            this.cb_GroundHandlersSegregation.Size = new System.Drawing.Size(164, 17);
            this.cb_GroundHandlersSegregation.TabIndex = 39;
            this.cb_GroundHandlersSegregation.Text = "Ground Handlers segregation";
            this.cb_GroundHandlersSegregation.UseVisualStyleBackColor = false;
            this.cb_GroundHandlersSegregation.CheckedChanged += new System.EventHandler(this.cb_LooseULD_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cb_ColorFC);
            this.groupBox2.Controls.Add(this.cb_AirlineColor);
            this.groupBox2.Controls.Add(this.cb_ColorHandlers);
            this.groupBox2.Location = new System.Drawing.Point(56, 384);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 91);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Colors";
            // 
            // cb_TerminalColumn
            // 
            this.cb_TerminalColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_TerminalColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TerminalColumn.FormattingEnabled = true;
            this.cb_TerminalColumn.Location = new System.Drawing.Point(188, 210);
            this.cb_TerminalColumn.Name = "cb_TerminalColumn";
            this.cb_TerminalColumn.Size = new System.Drawing.Size(177, 21);
            this.cb_TerminalColumn.TabIndex = 41;
            this.cb_TerminalColumn.SelectedIndexChanged += new System.EventHandler(this.cb_TerminalColumn_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(382, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Observed terminal";
            // 
            // SIM_Allocation_MakeUp
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(593, 558);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_TerminalColumn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gb_Segregation);
            this.Controls.Add(this.cb_FPAsBasis);
            this.Controls.Add(this.lbl_Aircraft);
            this.Controls.Add(this.cb_Aircraft);
            this.Controls.Add(this.lbl_Airline);
            this.Controls.Add(this.cb_AirlineCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_Separation);
            this.Controls.Add(this.cb_AllocateFlightPlan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_Terminal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_TimeStep);
            this.Controls.Add(this.dtp_EndTime);
            this.Controls.Add(this.lbl_EndTime);
            this.Controls.Add(this.dtp_BeginTime);
            this.Controls.Add(this.lbl_BeginTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_AllocationRules);
            this.Controls.Add(this.cb_FPDTable);
            this.Controls.Add(this.txt_TitleTable);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(352, 472);
            this.Name = "SIM_Allocation_MakeUp";
            this.ShowInTaskbar = false;
            this.Text = "Make-Up allocation";
            this.gb_Segregation.ResumeLayout(false);
            this.gb_Segregation.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txt_TitleTable;
        private System.Windows.Forms.ComboBox cb_FPDTable;
        private System.Windows.Forms.ComboBox cb_AllocationRules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_EndTime;
        private System.Windows.Forms.Label lbl_EndTime;
        private System.Windows.Forms.DateTimePicker dtp_BeginTime;
        private System.Windows.Forms.Label lbl_BeginTime;
        private System.Windows.Forms.TextBox txt_TimeStep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_Terminal;
        private System.Windows.Forms.CheckBox cb_AllocateFlightPlan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Separation;
        private System.Windows.Forms.CheckBox cb_ColorFC;
        private System.Windows.Forms.CheckBox cb_Algorithme;
        private System.Windows.Forms.CheckBox cb_AirlineColor;
        private System.Windows.Forms.CheckBox cb_ColorHandlers;
        private System.Windows.Forms.ComboBox cb_AirlineCode;
        private System.Windows.Forms.Label lbl_Airline;
        private System.Windows.Forms.Label lbl_Aircraft;
        private System.Windows.Forms.ComboBox cb_Aircraft;
        private System.Windows.Forms.CheckBox cb_FPAsBasis;
        private System.Windows.Forms.CheckBox cb_LooseULD;
        private System.Windows.Forms.GroupBox gb_Segregation;
        private System.Windows.Forms.CheckBox cb_GroundHandlersSegregation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cb_TerminalColumn;
        private System.Windows.Forms.Label label4;
    }
}