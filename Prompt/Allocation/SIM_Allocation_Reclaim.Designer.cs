namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Allocation_Reclaim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Allocation_Reclaim));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txt_TitleTable = new System.Windows.Forms.TextBox();
            this.cb_FPDTable = new System.Windows.Forms.ComboBox();
            this.cb_AllocationRules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_FlightsAlowedPerBaggageClaim = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_EndTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_EndTime = new System.Windows.Forms.Label();
            this.dtp_BeginTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_BeginTime = new System.Windows.Forms.Label();
            this.txt_TimeStep = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_Terminal = new System.Windows.Forms.ComboBox();
            this.cb_AllocateFlightPlan = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Separation = new System.Windows.Forms.TextBox();
            this.cb_ColorFC = new System.Windows.Forms.CheckBox();
            this.cb_FlightsAlowedPerBaggageClaimLH = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_FlightCategories = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cb_Airline = new System.Windows.Forms.ComboBox();
            this.cb_ColorHandlers = new System.Windows.Forms.CheckBox();
            this.cb_AirlineColor = new System.Windows.Forms.CheckBox();
            this.lbl_Aircraft = new System.Windows.Forms.Label();
            this.cb_Aircraft = new System.Windows.Forms.ComboBox();
            this.cb_FPAsBasis = new System.Windows.Forms.CheckBox();
            this.gbColors = new System.Windows.Forms.GroupBox();
            this.gb_Segregation = new System.Windows.Forms.GroupBox();
            this.cb_GroundHandlersSegregation = new System.Windows.Forms.CheckBox();
            this.cb_LooseULD = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_UseInfeed = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_TerminalColumn = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.gb_Reclaim = new System.Windows.Forms.GroupBox();
            this.txt_InfeedSpeed = new System.Windows.Forms.TextBox();
            this.txt_DoubleInfeed = new System.Windows.Forms.TextBox();
            this.lbl_InfeedSpeed = new System.Windows.Forms.Label();
            this.lbl_Doubleinjection = new System.Windows.Forms.Label();
            this.gb_Tables = new System.Windows.Forms.GroupBox();
            this.chkb_BagConstraint = new System.Windows.Forms.CheckBox();
            this.cb_BagConstraint = new System.Windows.Forms.ComboBox();
            this.cb_LoadFactor = new System.Windows.Forms.ComboBox();
            this.lbl_LoadFactor = new System.Windows.Forms.Label();
            this.cb_BagsTable = new System.Windows.Forms.ComboBox();
            this.lbl_Bags = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbColors.SuspendLayout();
            this.gb_Segregation.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gb_Reclaim.SuspendLayout();
            this.gb_Tables.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(47, 13);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 24);
            this.btn_Ok.TabIndex = 24;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(448, 13);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 24);
            this.btn_Cancel.TabIndex = 25;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // txt_TitleTable
            // 
            this.txt_TitleTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_TitleTable.Location = new System.Drawing.Point(261, 29);
            this.txt_TitleTable.Name = "txt_TitleTable";
            this.txt_TitleTable.Size = new System.Drawing.Size(192, 20);
            this.txt_TitleTable.TabIndex = 1;
            // 
            // cb_FPDTable
            // 
            this.cb_FPDTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_FPDTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FPDTable.FormattingEnabled = true;
            this.cb_FPDTable.Location = new System.Drawing.Point(229, 13);
            this.cb_FPDTable.Name = "cb_FPDTable";
            this.cb_FPDTable.Size = new System.Drawing.Size(192, 21);
            this.cb_FPDTable.TabIndex = 2;
            this.cb_FPDTable.SelectedIndexChanged += new System.EventHandler(this.cb_FPDTable_SelectedIndexChanged);
            // 
            // cb_AllocationRules
            // 
            this.cb_AllocationRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_AllocationRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AllocationRules.FormattingEnabled = true;
            this.cb_AllocationRules.Location = new System.Drawing.Point(229, 107);
            this.cb_AllocationRules.Name = "cb_AllocationRules";
            this.cb_AllocationRules.Size = new System.Drawing.Size(192, 21);
            this.cb_AllocationRules.TabIndex = 3;
            this.cb_AllocationRules.SelectedIndexChanged += new System.EventHandler(this.cb_AllocationRules_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(15, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Openning closing times for Baggage Claim";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Flight plan arrival";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(149, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Table name :";
            // 
            // cb_FlightsAlowedPerBaggageClaim
            // 
            this.cb_FlightsAlowedPerBaggageClaim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FlightsAlowedPerBaggageClaim.FormattingEnabled = true;
            this.cb_FlightsAlowedPerBaggageClaim.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cb_FlightsAlowedPerBaggageClaim.Location = new System.Drawing.Point(229, 32);
            this.cb_FlightsAlowedPerBaggageClaim.Name = "cb_FlightsAlowedPerBaggageClaim";
            this.cb_FlightsAlowedPerBaggageClaim.Size = new System.Drawing.Size(69, 21);
            this.cb_FlightsAlowedPerBaggageClaim.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Flights allowed per Baggage claim";
            // 
            // dtp_EndTime
            // 
            this.dtp_EndTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_EndTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_EndTime.Location = new System.Drawing.Point(255, 576);
            this.dtp_EndTime.Name = "dtp_EndTime";
            this.dtp_EndTime.Size = new System.Drawing.Size(123, 20);
            this.dtp_EndTime.TabIndex = 11;
            // 
            // lbl_EndTime
            // 
            this.lbl_EndTime.AutoSize = true;
            this.lbl_EndTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndTime.Location = new System.Drawing.Point(41, 580);
            this.lbl_EndTime.Name = "lbl_EndTime";
            this.lbl_EndTime.Size = new System.Drawing.Size(50, 13);
            this.lbl_EndTime.TabIndex = 22;
            this.lbl_EndTime.Text = "End date";
            // 
            // dtp_BeginTime
            // 
            this.dtp_BeginTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_BeginTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_BeginTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_BeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_BeginTime.Location = new System.Drawing.Point(255, 550);
            this.dtp_BeginTime.Name = "dtp_BeginTime";
            this.dtp_BeginTime.Size = new System.Drawing.Size(123, 20);
            this.dtp_BeginTime.TabIndex = 10;
            // 
            // lbl_BeginTime
            // 
            this.lbl_BeginTime.AutoSize = true;
            this.lbl_BeginTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_BeginTime.Location = new System.Drawing.Point(41, 554);
            this.lbl_BeginTime.Name = "lbl_BeginTime";
            this.lbl_BeginTime.Size = new System.Drawing.Size(58, 13);
            this.lbl_BeginTime.TabIndex = 19;
            this.lbl_BeginTime.Text = "Begin date";
            // 
            // txt_TimeStep
            // 
            this.txt_TimeStep.Location = new System.Drawing.Point(255, 602);
            this.txt_TimeStep.Name = "txt_TimeStep";
            this.txt_TimeStep.Size = new System.Drawing.Size(56, 20);
            this.txt_TimeStep.TabIndex = 12;
            this.txt_TimeStep.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(41, 605);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Time step (min)";
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
            this.cb_Terminal.Location = new System.Drawing.Point(397, 523);
            this.cb_Terminal.Name = "cb_Terminal";
            this.cb_Terminal.Size = new System.Drawing.Size(58, 21);
            this.cb_Terminal.TabIndex = 9;
            // 
            // cb_AllocateFlightPlan
            // 
            this.cb_AllocateFlightPlan.AutoSize = true;
            this.cb_AllocateFlightPlan.BackColor = System.Drawing.Color.Transparent;
            this.cb_AllocateFlightPlan.Location = new System.Drawing.Point(255, 654);
            this.cb_AllocateFlightPlan.Name = "cb_AllocateFlightPlan";
            this.cb_AllocateFlightPlan.Size = new System.Drawing.Size(186, 17);
            this.cb_AllocateFlightPlan.TabIndex = 21;
            this.cb_AllocateFlightPlan.Text = "Update FPA with allocated values";
            this.cb_AllocateFlightPlan.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(41, 631);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Delay between 2 flights (min)";
            // 
            // txt_Separation
            // 
            this.txt_Separation.Location = new System.Drawing.Point(255, 628);
            this.txt_Separation.Name = "txt_Separation";
            this.txt_Separation.Size = new System.Drawing.Size(56, 20);
            this.txt_Separation.TabIndex = 13;
            this.txt_Separation.Text = "0";
            // 
            // cb_ColorFC
            // 
            this.cb_ColorFC.AutoSize = true;
            this.cb_ColorFC.BackColor = System.Drawing.Color.Transparent;
            this.cb_ColorFC.Checked = true;
            this.cb_ColorFC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_ColorFC.Location = new System.Drawing.Point(22, 19);
            this.cb_ColorFC.Name = "cb_ColorFC";
            this.cb_ColorFC.Size = new System.Drawing.Size(166, 17);
            this.cb_ColorFC.TabIndex = 1;
            this.cb_ColorFC.Text = "Define color by flight category";
            this.cb_ColorFC.UseVisualStyleBackColor = false;
            // 
            // cb_FlightsAlowedPerBaggageClaimLH
            // 
            this.cb_FlightsAlowedPerBaggageClaimLH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FlightsAlowedPerBaggageClaimLH.FormattingEnabled = true;
            this.cb_FlightsAlowedPerBaggageClaimLH.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cb_FlightsAlowedPerBaggageClaimLH.Location = new System.Drawing.Point(304, 32);
            this.cb_FlightsAlowedPerBaggageClaimLH.Name = "cb_FlightsAlowedPerBaggageClaimLH";
            this.cb_FlightsAlowedPerBaggageClaimLH.Size = new System.Drawing.Size(69, 21);
            this.cb_FlightsAlowedPerBaggageClaimLH.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(244, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "SH";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(308, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "LH";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(6, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(214, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "Short Haul flight categories (separate by \",\")";
            // 
            // txt_FlightCategories
            // 
            this.txt_FlightCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_FlightCategories.Location = new System.Drawing.Point(229, 59);
            this.txt_FlightCategories.Name = "txt_FlightCategories";
            this.txt_FlightCategories.Size = new System.Drawing.Size(192, 20);
            this.txt_FlightCategories.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(15, 137);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "Airline table";
            // 
            // cb_Airline
            // 
            this.cb_Airline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Airline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Airline.FormattingEnabled = true;
            this.cb_Airline.Location = new System.Drawing.Point(229, 134);
            this.cb_Airline.Name = "cb_Airline";
            this.cb_Airline.Size = new System.Drawing.Size(192, 21);
            this.cb_Airline.TabIndex = 4;
            this.cb_Airline.SelectedIndexChanged += new System.EventHandler(this.cb_Airline_SelectedIndexChanged);
            // 
            // cb_ColorHandlers
            // 
            this.cb_ColorHandlers.AutoSize = true;
            this.cb_ColorHandlers.BackColor = System.Drawing.Color.Transparent;
            this.cb_ColorHandlers.Checked = true;
            this.cb_ColorHandlers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_ColorHandlers.Location = new System.Drawing.Point(22, 65);
            this.cb_ColorHandlers.Name = "cb_ColorHandlers";
            this.cb_ColorHandlers.Size = new System.Drawing.Size(183, 17);
            this.cb_ColorHandlers.TabIndex = 3;
            this.cb_ColorHandlers.Text = "Define color by Ground Handlers ";
            this.cb_ColorHandlers.UseVisualStyleBackColor = false;
            // 
            // cb_AirlineColor
            // 
            this.cb_AirlineColor.AutoSize = true;
            this.cb_AirlineColor.BackColor = System.Drawing.Color.Transparent;
            this.cb_AirlineColor.Checked = true;
            this.cb_AirlineColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_AirlineColor.Location = new System.Drawing.Point(22, 42);
            this.cb_AirlineColor.Name = "cb_AirlineColor";
            this.cb_AirlineColor.Size = new System.Drawing.Size(155, 17);
            this.cb_AirlineColor.TabIndex = 2;
            this.cb_AirlineColor.Text = "Define color by Airline code";
            this.cb_AirlineColor.UseVisualStyleBackColor = false;
            // 
            // lbl_Aircraft
            // 
            this.lbl_Aircraft.AutoSize = true;
            this.lbl_Aircraft.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Aircraft.Location = new System.Drawing.Point(15, 164);
            this.lbl_Aircraft.Name = "lbl_Aircraft";
            this.lbl_Aircraft.Size = new System.Drawing.Size(89, 13);
            this.lbl_Aircraft.TabIndex = 42;
            this.lbl_Aircraft.Text = "Aircraft type table";
            // 
            // cb_Aircraft
            // 
            this.cb_Aircraft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Aircraft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Aircraft.FormattingEnabled = true;
            this.cb_Aircraft.Location = new System.Drawing.Point(229, 161);
            this.cb_Aircraft.Name = "cb_Aircraft";
            this.cb_Aircraft.Size = new System.Drawing.Size(192, 21);
            this.cb_Aircraft.TabIndex = 5;
            // 
            // cb_FPAsBasis
            // 
            this.cb_FPAsBasis.AutoSize = true;
            this.cb_FPAsBasis.BackColor = System.Drawing.Color.Transparent;
            this.cb_FPAsBasis.Location = new System.Drawing.Point(38, 654);
            this.cb_FPAsBasis.Name = "cb_FPAsBasis";
            this.cb_FPAsBasis.Size = new System.Drawing.Size(150, 17);
            this.cb_FPAsBasis.TabIndex = 20;
            this.cb_FPAsBasis.Text = "Use FP allocation as basis";
            this.cb_FPAsBasis.UseVisualStyleBackColor = false;
            this.cb_FPAsBasis.CheckedChanged += new System.EventHandler(this.cb_FPAsBasis_CheckedChanged);
            // 
            // gbColors
            // 
            this.gbColors.BackColor = System.Drawing.Color.Transparent;
            this.gbColors.Controls.Add(this.cb_ColorFC);
            this.gbColors.Controls.Add(this.cb_AirlineColor);
            this.gbColors.Controls.Add(this.cb_ColorHandlers);
            this.gbColors.Location = new System.Drawing.Point(32, 677);
            this.gbColors.Name = "gbColors";
            this.gbColors.Size = new System.Drawing.Size(221, 100);
            this.gbColors.TabIndex = 22;
            this.gbColors.TabStop = false;
            this.gbColors.Text = "Colors";
            // 
            // gb_Segregation
            // 
            this.gb_Segregation.BackColor = System.Drawing.Color.Transparent;
            this.gb_Segregation.Controls.Add(this.cb_GroundHandlersSegregation);
            this.gb_Segregation.Controls.Add(this.cb_LooseULD);
            this.gb_Segregation.Location = new System.Drawing.Point(279, 683);
            this.gb_Segregation.Name = "gb_Segregation";
            this.gb_Segregation.Size = new System.Drawing.Size(208, 91);
            this.gb_Segregation.TabIndex = 23;
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
            this.cb_GroundHandlersSegregation.TabIndex = 2;
            this.cb_GroundHandlersSegregation.Text = "Ground Handlers segregation";
            this.cb_GroundHandlersSegregation.UseVisualStyleBackColor = false;
            this.cb_GroundHandlersSegregation.CheckedChanged += new System.EventHandler(this.cb_LooseULD_CheckedChanged);
            // 
            // cb_LooseULD
            // 
            this.cb_LooseULD.AutoSize = true;
            this.cb_LooseULD.BackColor = System.Drawing.Color.Transparent;
            this.cb_LooseULD.Location = new System.Drawing.Point(12, 42);
            this.cb_LooseULD.Name = "cb_LooseULD";
            this.cb_LooseULD.Size = new System.Drawing.Size(88, 17);
            this.cb_LooseULD.TabIndex = 1;
            this.cb_LooseULD.Text = "Loose / ULD";
            this.cb_LooseULD.UseVisualStyleBackColor = false;
            this.cb_LooseULD.CheckedChanged += new System.EventHandler(this.cb_LooseULD_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.chk_UseInfeed);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cb_TerminalColumn);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.gb_Reclaim);
            this.panel1.Controls.Add(this.gb_Tables);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.gb_Segregation);
            this.panel1.Controls.Add(this.gbColors);
            this.panel1.Controls.Add(this.cb_FPAsBasis);
            this.panel1.Controls.Add(this.txt_TitleTable);
            this.panel1.Controls.Add(this.lbl_BeginTime);
            this.panel1.Controls.Add(this.dtp_BeginTime);
            this.panel1.Controls.Add(this.lbl_EndTime);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dtp_EndTime);
            this.panel1.Controls.Add(this.txt_Separation);
            this.panel1.Controls.Add(this.txt_TimeStep);
            this.panel1.Controls.Add(this.cb_AllocateFlightPlan);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cb_Terminal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 557);
            this.panel1.TabIndex = 43;
            // 
            // chk_UseInfeed
            // 
            this.chk_UseInfeed.AutoSize = true;
            this.chk_UseInfeed.Location = new System.Drawing.Point(51, 83);
            this.chk_UseInfeed.Name = "chk_UseInfeed";
            this.chk_UseInfeed.Size = new System.Drawing.Size(279, 17);
            this.chk_UseInfeed.TabIndex = 40;
            this.chk_UseInfeed.Text = "Use infeed limitation to calc the opening time per flight";
            this.chk_UseInfeed.UseVisualStyleBackColor = true;
            this.chk_UseInfeed.CheckedChanged += new System.EventHandler(this.chk_UseInfeed_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(299, 526);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "Observed terminal";
            // 
            // cb_TerminalColumn
            // 
            this.cb_TerminalColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_TerminalColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TerminalColumn.FormattingEnabled = true;
            this.cb_TerminalColumn.Location = new System.Drawing.Point(192, 523);
            this.cb_TerminalColumn.Name = "cb_TerminalColumn";
            this.cb_TerminalColumn.Size = new System.Drawing.Size(101, 21);
            this.cb_TerminalColumn.TabIndex = 50;
            this.cb_TerminalColumn.SelectedIndexChanged += new System.EventHandler(this.cb_TerminalColumn_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(38, 526);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 13);
            this.label14.TabIndex = 49;
            this.label14.Text = "Terminal information :";
            // 
            // gb_Reclaim
            // 
            this.gb_Reclaim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Reclaim.Controls.Add(this.txt_InfeedSpeed);
            this.gb_Reclaim.Controls.Add(this.txt_DoubleInfeed);
            this.gb_Reclaim.Controls.Add(this.lbl_InfeedSpeed);
            this.gb_Reclaim.Controls.Add(this.lbl_Doubleinjection);
            this.gb_Reclaim.Controls.Add(this.cb_FlightsAlowedPerBaggageClaim);
            this.gb_Reclaim.Controls.Add(this.cb_FlightsAlowedPerBaggageClaimLH);
            this.gb_Reclaim.Controls.Add(this.label9);
            this.gb_Reclaim.Controls.Add(this.label10);
            this.gb_Reclaim.Controls.Add(this.label4);
            this.gb_Reclaim.Controls.Add(this.label11);
            this.gb_Reclaim.Controls.Add(this.txt_FlightCategories);
            this.gb_Reclaim.Location = new System.Drawing.Point(32, 356);
            this.gb_Reclaim.Name = "gb_Reclaim";
            this.gb_Reclaim.Size = new System.Drawing.Size(439, 143);
            this.gb_Reclaim.TabIndex = 48;
            this.gb_Reclaim.TabStop = false;
            this.gb_Reclaim.Text = "Reclaim specification";
            // 
            // txt_InfeedSpeed
            // 
            this.txt_InfeedSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_InfeedSpeed.Enabled = false;
            this.txt_InfeedSpeed.Location = new System.Drawing.Point(229, 111);
            this.txt_InfeedSpeed.Name = "txt_InfeedSpeed";
            this.txt_InfeedSpeed.Size = new System.Drawing.Size(192, 20);
            this.txt_InfeedSpeed.TabIndex = 39;
            // 
            // txt_DoubleInfeed
            // 
            this.txt_DoubleInfeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DoubleInfeed.Enabled = false;
            this.txt_DoubleInfeed.Location = new System.Drawing.Point(229, 85);
            this.txt_DoubleInfeed.Name = "txt_DoubleInfeed";
            this.txt_DoubleInfeed.Size = new System.Drawing.Size(192, 20);
            this.txt_DoubleInfeed.TabIndex = 38;
            // 
            // lbl_InfeedSpeed
            // 
            this.lbl_InfeedSpeed.AutoSize = true;
            this.lbl_InfeedSpeed.Enabled = false;
            this.lbl_InfeedSpeed.Location = new System.Drawing.Point(6, 114);
            this.lbl_InfeedSpeed.Name = "lbl_InfeedSpeed";
            this.lbl_InfeedSpeed.Size = new System.Drawing.Size(177, 13);
            this.lbl_InfeedSpeed.TabIndex = 37;
            this.lbl_InfeedSpeed.Text = "Speed of the infeed (seconds/bags)";
            // 
            // lbl_Doubleinjection
            // 
            this.lbl_Doubleinjection.AutoSize = true;
            this.lbl_Doubleinjection.Enabled = false;
            this.lbl_Doubleinjection.Location = new System.Drawing.Point(6, 88);
            this.lbl_Doubleinjection.Name = "lbl_Doubleinjection";
            this.lbl_Doubleinjection.Size = new System.Drawing.Size(155, 13);
            this.lbl_Doubleinjection.TabIndex = 36;
            this.lbl_Doubleinjection.Text = "Number of reclaim with 2 infeed";
            // 
            // gb_Tables
            // 
            this.gb_Tables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Tables.BackColor = System.Drawing.Color.Transparent;
            this.gb_Tables.Controls.Add(this.chkb_BagConstraint);
            this.gb_Tables.Controls.Add(this.cb_BagConstraint);
            this.gb_Tables.Controls.Add(this.label2);
            this.gb_Tables.Controls.Add(this.cb_LoadFactor);
            this.gb_Tables.Controls.Add(this.cb_Airline);
            this.gb_Tables.Controls.Add(this.lbl_LoadFactor);
            this.gb_Tables.Controls.Add(this.label1);
            this.gb_Tables.Controls.Add(this.cb_BagsTable);
            this.gb_Tables.Controls.Add(this.label12);
            this.gb_Tables.Controls.Add(this.lbl_Bags);
            this.gb_Tables.Controls.Add(this.cb_AllocationRules);
            this.gb_Tables.Controls.Add(this.cb_Aircraft);
            this.gb_Tables.Controls.Add(this.cb_FPDTable);
            this.gb_Tables.Controls.Add(this.lbl_Aircraft);
            this.gb_Tables.Location = new System.Drawing.Point(32, 106);
            this.gb_Tables.Name = "gb_Tables";
            this.gb_Tables.Size = new System.Drawing.Size(439, 225);
            this.gb_Tables.TabIndex = 47;
            this.gb_Tables.TabStop = false;
            this.gb_Tables.Text = "Tables";
            // 
            // chkb_BagConstraint
            // 
            this.chkb_BagConstraint.AutoSize = true;
            this.chkb_BagConstraint.Enabled = false;
            this.chkb_BagConstraint.Location = new System.Drawing.Point(18, 190);
            this.chkb_BagConstraint.Name = "chkb_BagConstraint";
            this.chkb_BagConstraint.Size = new System.Drawing.Size(144, 17);
            this.chkb_BagConstraint.TabIndex = 49;
            this.chkb_BagConstraint.Text = "Baggage constraint table";
            this.chkb_BagConstraint.UseVisualStyleBackColor = true;
            this.chkb_BagConstraint.CheckedChanged += new System.EventHandler(this.chkb_BagConstraint_CheckedChanged);
            this.chkb_BagConstraint.EnabledChanged += new System.EventHandler(this.chkb_BagConstraint_CheckedChanged);
            // 
            // cb_BagConstraint
            // 
            this.cb_BagConstraint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_BagConstraint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_BagConstraint.Enabled = false;
            this.cb_BagConstraint.FormattingEnabled = true;
            this.cb_BagConstraint.Location = new System.Drawing.Point(229, 188);
            this.cb_BagConstraint.Name = "cb_BagConstraint";
            this.cb_BagConstraint.Size = new System.Drawing.Size(192, 21);
            this.cb_BagConstraint.TabIndex = 47;
            this.cb_BagConstraint.SelectedIndexChanged += new System.EventHandler(this.cb_BagConstraint_SelectedIndexChanged);
            // 
            // cb_LoadFactor
            // 
            this.cb_LoadFactor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_LoadFactor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_LoadFactor.Enabled = false;
            this.cb_LoadFactor.FormattingEnabled = true;
            this.cb_LoadFactor.Location = new System.Drawing.Point(229, 67);
            this.cb_LoadFactor.Name = "cb_LoadFactor";
            this.cb_LoadFactor.Size = new System.Drawing.Size(192, 21);
            this.cb_LoadFactor.TabIndex = 45;
            this.cb_LoadFactor.SelectedIndexChanged += new System.EventHandler(this.cb_LoadFactor_SelectedIndexChanged);
            // 
            // lbl_LoadFactor
            // 
            this.lbl_LoadFactor.AutoSize = true;
            this.lbl_LoadFactor.BackColor = System.Drawing.Color.Transparent;
            this.lbl_LoadFactor.Enabled = false;
            this.lbl_LoadFactor.Location = new System.Drawing.Point(15, 70);
            this.lbl_LoadFactor.Name = "lbl_LoadFactor";
            this.lbl_LoadFactor.Size = new System.Drawing.Size(94, 13);
            this.lbl_LoadFactor.TabIndex = 46;
            this.lbl_LoadFactor.Text = "Arrival load factors";
            // 
            // cb_BagsTable
            // 
            this.cb_BagsTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_BagsTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_BagsTable.Enabled = false;
            this.cb_BagsTable.FormattingEnabled = true;
            this.cb_BagsTable.Location = new System.Drawing.Point(229, 40);
            this.cb_BagsTable.Name = "cb_BagsTable";
            this.cb_BagsTable.Size = new System.Drawing.Size(192, 21);
            this.cb_BagsTable.TabIndex = 43;
            this.cb_BagsTable.SelectedIndexChanged += new System.EventHandler(this.cb_BagsTable_SelectedIndexChanged);
            // 
            // lbl_Bags
            // 
            this.lbl_Bags.AutoSize = true;
            this.lbl_Bags.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Bags.Enabled = false;
            this.lbl_Bags.Location = new System.Drawing.Point(15, 43);
            this.lbl_Bags.Name = "lbl_Bags";
            this.lbl_Bags.Size = new System.Drawing.Size(109, 13);
            this.lbl_Bags.TabIndex = 44;
            this.lbl_Bags.Text = "Number of Bags table";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.btn_Ok);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Cancel);
            this.splitContainer1.Size = new System.Drawing.Size(562, 601);
            this.splitContainer1.SplitterDistance = 557;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 44;
            // 
            // SIM_Allocation_Reclaim
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(562, 601);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(562, 610);
            this.Name = "SIM_Allocation_Reclaim";
            this.ShowInTaskbar = false;
            this.Text = "Reclaim allocation";
            this.gbColors.ResumeLayout(false);
            this.gbColors.PerformLayout();
            this.gb_Segregation.ResumeLayout(false);
            this.gb_Segregation.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gb_Reclaim.ResumeLayout(false);
            this.gb_Reclaim.PerformLayout();
            this.gb_Tables.ResumeLayout(false);
            this.gb_Tables.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ComboBox cb_FlightsAlowedPerBaggageClaim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_EndTime;
        private System.Windows.Forms.Label lbl_EndTime;
        private System.Windows.Forms.DateTimePicker dtp_BeginTime;
        private System.Windows.Forms.Label lbl_BeginTime;
        private System.Windows.Forms.TextBox txt_TimeStep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_Terminal;
        private System.Windows.Forms.CheckBox cb_AllocateFlightPlan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Separation;
        private System.Windows.Forms.CheckBox cb_ColorFC;
        private System.Windows.Forms.ComboBox cb_FlightsAlowedPerBaggageClaimLH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_FlightCategories;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cb_Airline;
        private System.Windows.Forms.CheckBox cb_ColorHandlers;
        private System.Windows.Forms.CheckBox cb_AirlineColor;
        private System.Windows.Forms.Label lbl_Aircraft;
        private System.Windows.Forms.ComboBox cb_Aircraft;
        private System.Windows.Forms.CheckBox cb_FPAsBasis;
        private System.Windows.Forms.GroupBox gbColors;
        private System.Windows.Forms.GroupBox gb_Segregation;
        private System.Windows.Forms.CheckBox cb_GroundHandlersSegregation;
        private System.Windows.Forms.CheckBox cb_LooseULD;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cb_BagsTable;
        private System.Windows.Forms.Label lbl_Bags;
        private System.Windows.Forms.ComboBox cb_LoadFactor;
        private System.Windows.Forms.Label lbl_LoadFactor;
        private System.Windows.Forms.GroupBox gb_Reclaim;
        private System.Windows.Forms.GroupBox gb_Tables;
        private System.Windows.Forms.TextBox txt_InfeedSpeed;
        private System.Windows.Forms.TextBox txt_DoubleInfeed;
        private System.Windows.Forms.Label lbl_InfeedSpeed;
        private System.Windows.Forms.Label lbl_Doubleinjection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_TerminalColumn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chk_UseInfeed;
        private System.Windows.Forms.ComboBox cb_BagConstraint;
        private System.Windows.Forms.CheckBox chkb_BagConstraint;
    }
}