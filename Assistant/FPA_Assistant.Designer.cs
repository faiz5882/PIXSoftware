namespace SIMCORE_TOOL.Assistant
{
    partial class FPA_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPA_Assistant));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_date = new System.Windows.Forms.Label();
            this.cb_Airline = new System.Windows.Forms.ComboBox();
            this.lbl_Airline = new System.Windows.Forms.Label();
            this.lbl_flightN = new System.Windows.Forms.Label();
            this.lbl_Airport = new System.Windows.Forms.Label();
            this.cb_Airport = new System.Windows.Forms.ComboBox();
            this.lbl_FlightCateg = new System.Windows.Forms.Label();
            this.cb_FlightCategory = new System.Windows.Forms.ComboBox();
            this.lbl_Aircraft = new System.Windows.Forms.Label();
            this.cb_AircraftType = new System.Windows.Forms.ComboBox();
            this.cb_TerminalGate = new System.Windows.Forms.ComboBox();
            this.lbl_TerminalGate = new System.Windows.Forms.Label();
            this.lbl_LevelGate = new System.Windows.Forms.Label();
            this.cb_LevelGate = new System.Windows.Forms.ComboBox();
            this.cb_LevelReclaim = new System.Windows.Forms.ComboBox();
            this.lbl_LevelReclaim = new System.Windows.Forms.Label();
            this.lbl_TerminalReclaim = new System.Windows.Forms.Label();
            this.cb_TerminalReclaim = new System.Windows.Forms.ComboBox();
            this.cb_Reclaim = new System.Windows.Forms.ComboBox();
            this.lbl_Reclaim = new System.Windows.Forms.Label();
            this.lbl_Gate = new System.Windows.Forms.Label();
            this.cb_Gate = new System.Windows.Forms.ComboBox();
            this.gb_FlightInformation = new System.Windows.Forms.GroupBox();
            this.cb_CBP = new System.Windows.Forms.CheckBox();
            this.cb_TSA = new System.Windows.Forms.CheckBox();
            this.txt_NbPax = new System.Windows.Forms.TextBox();
            this.lbl_NbPax = new System.Windows.Forms.Label();
            this.cb_Runway = new System.Windows.Forms.ComboBox();
            this.lbl_Runway = new System.Windows.Forms.Label();
            this.dtp_DateTime = new System.Windows.Forms.DateTimePicker();
            this.btn_AddAirline = new System.Windows.Forms.Button();
            this.btn_AddFC = new System.Windows.Forms.Button();
            this.btn_Aircraft = new System.Windows.Forms.Button();
            this.btn_AddAirport = new System.Windows.Forms.Button();
            this.txt_FlightN = new System.Windows.Forms.TextBox();
            this.lbl_Parking = new System.Windows.Forms.Label();
            this.cb_Parking = new System.Windows.Forms.ComboBox();
            this.txt_Id = new System.Windows.Forms.TextBox();
            this.lbl_Id = new System.Windows.Forms.Label();
            this.btn_addNew = new System.Windows.Forms.Button();
            this.tc_information = new System.Windows.Forms.TabControl();
            this.tp_ArrivalGate = new System.Windows.Forms.TabPage();
            this.tp_ReclaimBelt = new System.Windows.Forms.TabPage();
            this.tp_Parking = new System.Windows.Forms.TabPage();
            this.cb_TerminalParking = new System.Windows.Forms.ComboBox();
            this.lbl_TerminalParking = new System.Windows.Forms.Label();
            this.tp_TransferInfeed = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_ArrivalInfeedEnd = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_ArrivalInfeedStart = new System.Windows.Forms.ComboBox();
            this.lbl_Terminal_Transfer = new System.Windows.Forms.Label();
            this.cb_Terminal_TransferInfeed = new System.Windows.Forms.ComboBox();
            this.lbl_Transfer = new System.Windows.Forms.Label();
            this.cb_TransferInfeed = new System.Windows.Forms.ComboBox();
            this.tp_Comments = new System.Windows.Forms.TabPage();
            this.tc_Comments = new System.Windows.Forms.TabControl();
            this.tp_Comment1 = new System.Windows.Forms.TabPage();
            this.txt_Comment1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txt_Comment2 = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txt_Comment3 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txt_Comment4 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txt_Comment5 = new System.Windows.Forms.TextBox();
            this.tt_Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSearchAirline = new System.Windows.Forms.Button();
            this.btnSearchAirport = new System.Windows.Forms.Button();
            this.btnSearchAircraftType = new System.Windows.Forms.Button();
            this.gb_FlightInformation.SuspendLayout();
            this.tc_information.SuspendLayout();
            this.tp_ArrivalGate.SuspendLayout();
            this.tp_ReclaimBelt.SuspendLayout();
            this.tp_Parking.SuspendLayout();
            this.tp_TransferInfeed.SuspendLayout();
            this.tp_Comments.SuspendLayout();
            this.tc_Comments.SuspendLayout();
            this.tp_Comment1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(51, 505);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(86, 32);
            this.btn_Ok.TabIndex = 5;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(472, 505);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(86, 32);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.BackColor = System.Drawing.Color.Transparent;
            this.lbl_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_date.Location = new System.Drawing.Point(173, 21);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(34, 13);
            this.lbl_date.TabIndex = 2;
            this.lbl_date.Text = "Date";
            // 
            // cb_Airline
            // 
            this.cb_Airline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Airline.FormattingEnabled = true;
            this.cb_Airline.Location = new System.Drawing.Point(215, 69);
            this.cb_Airline.Name = "cb_Airline";
            this.cb_Airline.Size = new System.Drawing.Size(184, 21);
            this.cb_Airline.TabIndex = 3;
            // 
            // lbl_Airline
            // 
            this.lbl_Airline.AutoSize = true;
            this.lbl_Airline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Airline.Location = new System.Drawing.Point(172, 72);
            this.lbl_Airline.Name = "lbl_Airline";
            this.lbl_Airline.Size = new System.Drawing.Size(35, 13);
            this.lbl_Airline.TabIndex = 8;
            this.lbl_Airline.Text = "Airline";
            // 
            // lbl_flightN
            // 
            this.lbl_flightN.AutoSize = true;
            this.lbl_flightN.BackColor = System.Drawing.Color.Transparent;
            this.lbl_flightN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_flightN.Location = new System.Drawing.Point(153, 46);
            this.lbl_flightN.Name = "lbl_flightN";
            this.lbl_flightN.Size = new System.Drawing.Size(54, 13);
            this.lbl_flightN.TabIndex = 10;
            this.lbl_flightN.Text = "Flight n°";
            // 
            // lbl_Airport
            // 
            this.lbl_Airport.AutoSize = true;
            this.lbl_Airport.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Airport.Location = new System.Drawing.Point(173, 97);
            this.lbl_Airport.Name = "lbl_Airport";
            this.lbl_Airport.Size = new System.Drawing.Size(34, 13);
            this.lbl_Airport.TabIndex = 11;
            this.lbl_Airport.Text = "Origin";
            // 
            // cb_Airport
            // 
            this.cb_Airport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Airport.FormattingEnabled = true;
            this.cb_Airport.Location = new System.Drawing.Point(215, 94);
            this.cb_Airport.Name = "cb_Airport";
            this.cb_Airport.Size = new System.Drawing.Size(184, 21);
            this.cb_Airport.TabIndex = 5;
            // 
            // lbl_FlightCateg
            // 
            this.lbl_FlightCateg.AutoSize = true;
            this.lbl_FlightCateg.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FlightCateg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FlightCateg.Location = new System.Drawing.Point(118, 124);
            this.lbl_FlightCateg.Name = "lbl_FlightCateg";
            this.lbl_FlightCateg.Size = new System.Drawing.Size(91, 13);
            this.lbl_FlightCateg.TabIndex = 13;
            this.lbl_FlightCateg.Text = "Flight category";
            // 
            // cb_FlightCategory
            // 
            this.cb_FlightCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FlightCategory.FormattingEnabled = true;
            this.cb_FlightCategory.Location = new System.Drawing.Point(215, 121);
            this.cb_FlightCategory.Name = "cb_FlightCategory";
            this.cb_FlightCategory.Size = new System.Drawing.Size(184, 21);
            this.cb_FlightCategory.TabIndex = 7;
            // 
            // lbl_Aircraft
            // 
            this.lbl_Aircraft.AutoSize = true;
            this.lbl_Aircraft.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Aircraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Aircraft.Location = new System.Drawing.Point(131, 150);
            this.lbl_Aircraft.Name = "lbl_Aircraft";
            this.lbl_Aircraft.Size = new System.Drawing.Size(76, 13);
            this.lbl_Aircraft.TabIndex = 15;
            this.lbl_Aircraft.Text = "Aircraft type";
            // 
            // cb_AircraftType
            // 
            this.cb_AircraftType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AircraftType.FormattingEnabled = true;
            this.cb_AircraftType.Location = new System.Drawing.Point(215, 147);
            this.cb_AircraftType.Name = "cb_AircraftType";
            this.cb_AircraftType.Size = new System.Drawing.Size(184, 21);
            this.cb_AircraftType.TabIndex = 9;
            // 
            // cb_TerminalGate
            // 
            this.cb_TerminalGate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TerminalGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_TerminalGate.FormattingEnabled = true;
            this.cb_TerminalGate.Location = new System.Drawing.Point(211, 18);
            this.cb_TerminalGate.Name = "cb_TerminalGate";
            this.cb_TerminalGate.Size = new System.Drawing.Size(184, 21);
            this.cb_TerminalGate.TabIndex = 1;
            this.cb_TerminalGate.SelectedIndexChanged += new System.EventHandler(this.cb_TerminalGate_SelectedIndexChanged);
            // 
            // lbl_TerminalGate
            // 
            this.lbl_TerminalGate.AutoSize = true;
            this.lbl_TerminalGate.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TerminalGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TerminalGate.Location = new System.Drawing.Point(148, 21);
            this.lbl_TerminalGate.Name = "lbl_TerminalGate";
            this.lbl_TerminalGate.Size = new System.Drawing.Size(55, 13);
            this.lbl_TerminalGate.TabIndex = 18;
            this.lbl_TerminalGate.Text = "Terminal";
            // 
            // lbl_LevelGate
            // 
            this.lbl_LevelGate.AutoSize = true;
            this.lbl_LevelGate.BackColor = System.Drawing.Color.Transparent;
            this.lbl_LevelGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LevelGate.Location = new System.Drawing.Point(165, 48);
            this.lbl_LevelGate.Name = "lbl_LevelGate";
            this.lbl_LevelGate.Size = new System.Drawing.Size(38, 13);
            this.lbl_LevelGate.TabIndex = 19;
            this.lbl_LevelGate.Text = "Level";
            // 
            // cb_LevelGate
            // 
            this.cb_LevelGate.Enabled = false;
            this.cb_LevelGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_LevelGate.FormattingEnabled = true;
            this.cb_LevelGate.Location = new System.Drawing.Point(211, 45);
            this.cb_LevelGate.Name = "cb_LevelGate";
            this.cb_LevelGate.Size = new System.Drawing.Size(184, 21);
            this.cb_LevelGate.TabIndex = 2;
            // 
            // cb_LevelReclaim
            // 
            this.cb_LevelReclaim.Enabled = false;
            this.cb_LevelReclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_LevelReclaim.FormattingEnabled = true;
            this.cb_LevelReclaim.Location = new System.Drawing.Point(211, 45);
            this.cb_LevelReclaim.Name = "cb_LevelReclaim";
            this.cb_LevelReclaim.Size = new System.Drawing.Size(184, 21);
            this.cb_LevelReclaim.TabIndex = 2;
            // 
            // lbl_LevelReclaim
            // 
            this.lbl_LevelReclaim.AutoSize = true;
            this.lbl_LevelReclaim.BackColor = System.Drawing.Color.Transparent;
            this.lbl_LevelReclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LevelReclaim.Location = new System.Drawing.Point(165, 48);
            this.lbl_LevelReclaim.Name = "lbl_LevelReclaim";
            this.lbl_LevelReclaim.Size = new System.Drawing.Size(38, 13);
            this.lbl_LevelReclaim.TabIndex = 24;
            this.lbl_LevelReclaim.Text = "Level";
            // 
            // lbl_TerminalReclaim
            // 
            this.lbl_TerminalReclaim.AutoSize = true;
            this.lbl_TerminalReclaim.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TerminalReclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TerminalReclaim.Location = new System.Drawing.Point(148, 21);
            this.lbl_TerminalReclaim.Name = "lbl_TerminalReclaim";
            this.lbl_TerminalReclaim.Size = new System.Drawing.Size(55, 13);
            this.lbl_TerminalReclaim.TabIndex = 23;
            this.lbl_TerminalReclaim.Text = "Terminal";
            // 
            // cb_TerminalReclaim
            // 
            this.cb_TerminalReclaim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TerminalReclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_TerminalReclaim.FormattingEnabled = true;
            this.cb_TerminalReclaim.Location = new System.Drawing.Point(211, 18);
            this.cb_TerminalReclaim.Name = "cb_TerminalReclaim";
            this.cb_TerminalReclaim.Size = new System.Drawing.Size(184, 21);
            this.cb_TerminalReclaim.TabIndex = 1;
            this.cb_TerminalReclaim.SelectedIndexChanged += new System.EventHandler(this.cb_TerminalReclaim_SelectedIndexChanged);
            // 
            // cb_Reclaim
            // 
            this.cb_Reclaim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Reclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Reclaim.FormattingEnabled = true;
            this.cb_Reclaim.Location = new System.Drawing.Point(211, 72);
            this.cb_Reclaim.Name = "cb_Reclaim";
            this.cb_Reclaim.Size = new System.Drawing.Size(184, 21);
            this.cb_Reclaim.TabIndex = 3;
            this.cb_Reclaim.SelectedIndexChanged += new System.EventHandler(this.cb_Reclaim_SelectedIndexChanged);
            // 
            // lbl_Reclaim
            // 
            this.lbl_Reclaim.AutoSize = true;
            this.lbl_Reclaim.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Reclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Reclaim.Location = new System.Drawing.Point(126, 75);
            this.lbl_Reclaim.Name = "lbl_Reclaim";
            this.lbl_Reclaim.Size = new System.Drawing.Size(77, 13);
            this.lbl_Reclaim.TabIndex = 27;
            this.lbl_Reclaim.Text = "Reclaim belt";
            // 
            // lbl_Gate
            // 
            this.lbl_Gate.AutoSize = true;
            this.lbl_Gate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Gate.Location = new System.Drawing.Point(169, 75);
            this.lbl_Gate.Name = "lbl_Gate";
            this.lbl_Gate.Size = new System.Drawing.Size(34, 13);
            this.lbl_Gate.TabIndex = 22;
            this.lbl_Gate.Text = "Gate";
            // 
            // cb_Gate
            // 
            this.cb_Gate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Gate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Gate.FormattingEnabled = true;
            this.cb_Gate.Location = new System.Drawing.Point(211, 72);
            this.cb_Gate.Name = "cb_Gate";
            this.cb_Gate.Size = new System.Drawing.Size(184, 21);
            this.cb_Gate.TabIndex = 3;
            this.cb_Gate.SelectedIndexChanged += new System.EventHandler(this.cb_Gate_SelectedIndexChanged);
            // 
            // gb_FlightInformation
            // 
            this.gb_FlightInformation.BackColor = System.Drawing.Color.Transparent;
            this.gb_FlightInformation.Controls.Add(this.btnSearchAircraftType);
            this.gb_FlightInformation.Controls.Add(this.btnSearchAirport);
            this.gb_FlightInformation.Controls.Add(this.btnSearchAirline);
            this.gb_FlightInformation.Controls.Add(this.cb_CBP);
            this.gb_FlightInformation.Controls.Add(this.cb_TSA);
            this.gb_FlightInformation.Controls.Add(this.txt_NbPax);
            this.gb_FlightInformation.Controls.Add(this.lbl_NbPax);
            this.gb_FlightInformation.Controls.Add(this.cb_Runway);
            this.gb_FlightInformation.Controls.Add(this.lbl_Runway);
            this.gb_FlightInformation.Controls.Add(this.dtp_DateTime);
            this.gb_FlightInformation.Controls.Add(this.btn_AddAirline);
            this.gb_FlightInformation.Controls.Add(this.btn_AddFC);
            this.gb_FlightInformation.Controls.Add(this.btn_Aircraft);
            this.gb_FlightInformation.Controls.Add(this.btn_AddAirport);
            this.gb_FlightInformation.Controls.Add(this.txt_FlightN);
            this.gb_FlightInformation.Controls.Add(this.lbl_flightN);
            this.gb_FlightInformation.Controls.Add(this.cb_Airline);
            this.gb_FlightInformation.Controls.Add(this.cb_AircraftType);
            this.gb_FlightInformation.Controls.Add(this.lbl_date);
            this.gb_FlightInformation.Controls.Add(this.lbl_Aircraft);
            this.gb_FlightInformation.Controls.Add(this.lbl_Airline);
            this.gb_FlightInformation.Controls.Add(this.lbl_Airport);
            this.gb_FlightInformation.Controls.Add(this.cb_FlightCategory);
            this.gb_FlightInformation.Controls.Add(this.lbl_FlightCateg);
            this.gb_FlightInformation.Controls.Add(this.cb_Airport);
            this.gb_FlightInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_FlightInformation.Location = new System.Drawing.Point(29, 38);
            this.gb_FlightInformation.Name = "gb_FlightInformation";
            this.gb_FlightInformation.Size = new System.Drawing.Size(596, 278);
            this.gb_FlightInformation.TabIndex = 2;
            this.gb_FlightInformation.TabStop = false;
            this.gb_FlightInformation.Text = "Flight information";
            // 
            // cb_CBP
            // 
            this.cb_CBP.AutoSize = true;
            this.cb_CBP.Location = new System.Drawing.Point(215, 250);
            this.cb_CBP.Name = "cb_CBP";
            this.cb_CBP.Size = new System.Drawing.Size(207, 17);
            this.cb_CBP.TabIndex = 21;
            this.cb_CBP.Text = "Customs and Borders Protection (CBP)";
            this.cb_CBP.UseVisualStyleBackColor = true;
            // 
            // cb_TSA
            // 
            this.cb_TSA.AutoSize = true;
            this.cb_TSA.Location = new System.Drawing.Point(215, 227);
            this.cb_TSA.Name = "cb_TSA";
            this.cb_TSA.Size = new System.Drawing.Size(198, 17);
            this.cb_TSA.TabIndex = 13;
            this.cb_TSA.Text = "BHS : whole no-read flight (No BSM)";
            this.cb_TSA.UseVisualStyleBackColor = true;
            // 
            // txt_NbPax
            // 
            this.txt_NbPax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_NbPax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_NbPax.Location = new System.Drawing.Point(215, 174);
            this.txt_NbPax.Name = "txt_NbPax";
            this.txt_NbPax.Size = new System.Drawing.Size(184, 20);
            this.txt_NbPax.TabIndex = 11;
            // 
            // lbl_NbPax
            // 
            this.lbl_NbPax.AutoSize = true;
            this.lbl_NbPax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NbPax.Location = new System.Drawing.Point(123, 177);
            this.lbl_NbPax.Name = "lbl_NbPax";
            this.lbl_NbPax.Size = new System.Drawing.Size(84, 13);
            this.lbl_NbPax.TabIndex = 20;
            this.lbl_NbPax.Text = "Number of seats";
            // 
            // cb_Runway
            // 
            this.cb_Runway.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Runway.FormattingEnabled = true;
            this.cb_Runway.Location = new System.Drawing.Point(215, 200);
            this.cb_Runway.Name = "cb_Runway";
            this.cb_Runway.Size = new System.Drawing.Size(184, 21);
            this.cb_Runway.TabIndex = 12;
            // 
            // lbl_Runway
            // 
            this.lbl_Runway.AutoSize = true;
            this.lbl_Runway.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Runway.Location = new System.Drawing.Point(155, 203);
            this.lbl_Runway.Name = "lbl_Runway";
            this.lbl_Runway.Size = new System.Drawing.Size(52, 13);
            this.lbl_Runway.TabIndex = 17;
            this.lbl_Runway.Text = "Runway";
            // 
            // dtp_DateTime
            // 
            this.dtp_DateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_DateTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_DateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_DateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DateTime.Location = new System.Drawing.Point(215, 17);
            this.dtp_DateTime.Name = "dtp_DateTime";
            this.dtp_DateTime.Size = new System.Drawing.Size(184, 20);
            this.dtp_DateTime.TabIndex = 1;
            // 
            // btn_AddAirline
            // 
            this.btn_AddAirline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddAirline.Location = new System.Drawing.Point(405, 66);
            this.btn_AddAirline.Name = "btn_AddAirline";
            this.btn_AddAirline.Size = new System.Drawing.Size(18, 24);
            this.btn_AddAirline.TabIndex = 4;
            this.btn_AddAirline.Text = "+";
            this.btn_AddAirline.UseVisualStyleBackColor = true;
            this.btn_AddAirline.Click += new System.EventHandler(this.btn_AddAirline_Click);
            // 
            // btn_AddFC
            // 
            this.btn_AddFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddFC.Location = new System.Drawing.Point(405, 118);
            this.btn_AddFC.Name = "btn_AddFC";
            this.btn_AddFC.Size = new System.Drawing.Size(18, 24);
            this.btn_AddFC.TabIndex = 8;
            this.btn_AddFC.Text = "+";
            this.btn_AddFC.UseVisualStyleBackColor = true;
            this.btn_AddFC.Click += new System.EventHandler(this.btn_AddAirline_Click);
            // 
            // btn_Aircraft
            // 
            this.btn_Aircraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Aircraft.Location = new System.Drawing.Point(405, 144);
            this.btn_Aircraft.Name = "btn_Aircraft";
            this.btn_Aircraft.Size = new System.Drawing.Size(18, 24);
            this.btn_Aircraft.TabIndex = 10;
            this.btn_Aircraft.Tag = "FP_AircraftTypesTable";
            this.btn_Aircraft.Text = "+";
            this.btn_Aircraft.UseVisualStyleBackColor = true;
            this.btn_Aircraft.Click += new System.EventHandler(this.btn_AddAirline_Click);
            // 
            // btn_AddAirport
            // 
            this.btn_AddAirport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddAirport.Location = new System.Drawing.Point(405, 91);
            this.btn_AddAirport.Name = "btn_AddAirport";
            this.btn_AddAirport.Size = new System.Drawing.Size(18, 24);
            this.btn_AddAirport.TabIndex = 6;
            this.btn_AddAirport.Text = "+";
            this.btn_AddAirport.UseVisualStyleBackColor = true;
            this.btn_AddAirport.Click += new System.EventHandler(this.btn_AddAirline_Click);
            // 
            // txt_FlightN
            // 
            this.txt_FlightN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_FlightN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_FlightN.Location = new System.Drawing.Point(215, 43);
            this.txt_FlightN.Name = "txt_FlightN";
            this.txt_FlightN.Size = new System.Drawing.Size(184, 20);
            this.txt_FlightN.TabIndex = 2;
            // 
            // lbl_Parking
            // 
            this.lbl_Parking.AutoSize = true;
            this.lbl_Parking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Parking.Location = new System.Drawing.Point(153, 48);
            this.lbl_Parking.Name = "lbl_Parking";
            this.lbl_Parking.Size = new System.Drawing.Size(50, 13);
            this.lbl_Parking.TabIndex = 18;
            this.lbl_Parking.Text = "Parking";
            // 
            // cb_Parking
            // 
            this.cb_Parking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Parking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Parking.FormattingEnabled = true;
            this.cb_Parking.Location = new System.Drawing.Point(211, 45);
            this.cb_Parking.Name = "cb_Parking";
            this.cb_Parking.Size = new System.Drawing.Size(184, 21);
            this.cb_Parking.TabIndex = 2;
            // 
            // txt_Id
            // 
            this.txt_Id.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Id.Enabled = false;
            this.txt_Id.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_Id.Location = new System.Drawing.Point(244, 12);
            this.txt_Id.Name = "txt_Id";
            this.txt_Id.Size = new System.Drawing.Size(175, 20);
            this.txt_Id.TabIndex = 1;
            // 
            // lbl_Id
            // 
            this.lbl_Id.AutoSize = true;
            this.lbl_Id.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Id.Location = new System.Drawing.Point(214, 15);
            this.lbl_Id.Name = "lbl_Id";
            this.lbl_Id.Size = new System.Drawing.Size(22, 13);
            this.lbl_Id.TabIndex = 32;
            this.lbl_Id.Text = "Id :";
            // 
            // btn_addNew
            // 
            this.btn_addNew.Location = new System.Drawing.Point(149, 505);
            this.btn_addNew.Name = "btn_addNew";
            this.btn_addNew.Size = new System.Drawing.Size(114, 31);
            this.btn_addNew.TabIndex = 6;
            this.btn_addNew.Text = "Ok,  &Add new";
            this.btn_addNew.UseVisualStyleBackColor = true;
            this.btn_addNew.Click += new System.EventHandler(this.btn_addNew_Click);
            // 
            // tc_information
            // 
            this.tc_information.Controls.Add(this.tp_ArrivalGate);
            this.tc_information.Controls.Add(this.tp_ReclaimBelt);
            this.tc_information.Controls.Add(this.tp_Parking);
            this.tc_information.Controls.Add(this.tp_TransferInfeed);
            this.tc_information.Controls.Add(this.tp_Comments);
            this.tc_information.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc_information.Location = new System.Drawing.Point(29, 336);
            this.tc_information.Name = "tc_information";
            this.tc_information.SelectedIndex = 0;
            this.tc_information.Size = new System.Drawing.Size(596, 154);
            this.tc_information.TabIndex = 3;
            // 
            // tp_ArrivalGate
            // 
            this.tp_ArrivalGate.Controls.Add(this.lbl_Gate);
            this.tp_ArrivalGate.Controls.Add(this.cb_Gate);
            this.tp_ArrivalGate.Controls.Add(this.cb_TerminalGate);
            this.tp_ArrivalGate.Controls.Add(this.lbl_LevelGate);
            this.tp_ArrivalGate.Controls.Add(this.cb_LevelGate);
            this.tp_ArrivalGate.Controls.Add(this.lbl_TerminalGate);
            this.tp_ArrivalGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tp_ArrivalGate.Location = new System.Drawing.Point(4, 22);
            this.tp_ArrivalGate.Name = "tp_ArrivalGate";
            this.tp_ArrivalGate.Padding = new System.Windows.Forms.Padding(3);
            this.tp_ArrivalGate.Size = new System.Drawing.Size(588, 128);
            this.tp_ArrivalGate.TabIndex = 0;
            this.tp_ArrivalGate.Text = "Arrival gate information";
            this.tp_ArrivalGate.UseVisualStyleBackColor = true;
            // 
            // tp_ReclaimBelt
            // 
            this.tp_ReclaimBelt.Controls.Add(this.cb_Reclaim);
            this.tp_ReclaimBelt.Controls.Add(this.lbl_Reclaim);
            this.tp_ReclaimBelt.Controls.Add(this.cb_TerminalReclaim);
            this.tp_ReclaimBelt.Controls.Add(this.lbl_LevelReclaim);
            this.tp_ReclaimBelt.Controls.Add(this.lbl_TerminalReclaim);
            this.tp_ReclaimBelt.Controls.Add(this.cb_LevelReclaim);
            this.tp_ReclaimBelt.Location = new System.Drawing.Point(4, 22);
            this.tp_ReclaimBelt.Name = "tp_ReclaimBelt";
            this.tp_ReclaimBelt.Padding = new System.Windows.Forms.Padding(3);
            this.tp_ReclaimBelt.Size = new System.Drawing.Size(588, 128);
            this.tp_ReclaimBelt.TabIndex = 1;
            this.tp_ReclaimBelt.Text = "Reclaim belt information";
            this.tp_ReclaimBelt.UseVisualStyleBackColor = true;
            // 
            // tp_Parking
            // 
            this.tp_Parking.Controls.Add(this.cb_TerminalParking);
            this.tp_Parking.Controls.Add(this.lbl_TerminalParking);
            this.tp_Parking.Controls.Add(this.lbl_Parking);
            this.tp_Parking.Controls.Add(this.cb_Parking);
            this.tp_Parking.Location = new System.Drawing.Point(4, 22);
            this.tp_Parking.Name = "tp_Parking";
            this.tp_Parking.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Parking.Size = new System.Drawing.Size(588, 128);
            this.tp_Parking.TabIndex = 2;
            this.tp_Parking.Text = "Parking information";
            this.tp_Parking.UseVisualStyleBackColor = true;
            // 
            // cb_TerminalParking
            // 
            this.cb_TerminalParking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TerminalParking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_TerminalParking.FormattingEnabled = true;
            this.cb_TerminalParking.Location = new System.Drawing.Point(211, 18);
            this.cb_TerminalParking.Name = "cb_TerminalParking";
            this.cb_TerminalParking.Size = new System.Drawing.Size(184, 21);
            this.cb_TerminalParking.TabIndex = 1;
            this.cb_TerminalParking.SelectedIndexChanged += new System.EventHandler(this.cb_TerminalParking_SelectedIndexChanged);
            // 
            // lbl_TerminalParking
            // 
            this.lbl_TerminalParking.AutoSize = true;
            this.lbl_TerminalParking.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TerminalParking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TerminalParking.Location = new System.Drawing.Point(148, 21);
            this.lbl_TerminalParking.Name = "lbl_TerminalParking";
            this.lbl_TerminalParking.Size = new System.Drawing.Size(55, 13);
            this.lbl_TerminalParking.TabIndex = 25;
            this.lbl_TerminalParking.Text = "Terminal";
            // 
            // tp_TransferInfeed
            // 
            this.tp_TransferInfeed.Controls.Add(this.label2);
            this.tp_TransferInfeed.Controls.Add(this.cb_ArrivalInfeedEnd);
            this.tp_TransferInfeed.Controls.Add(this.label1);
            this.tp_TransferInfeed.Controls.Add(this.cb_ArrivalInfeedStart);
            this.tp_TransferInfeed.Controls.Add(this.lbl_Terminal_Transfer);
            this.tp_TransferInfeed.Controls.Add(this.cb_Terminal_TransferInfeed);
            this.tp_TransferInfeed.Controls.Add(this.lbl_Transfer);
            this.tp_TransferInfeed.Controls.Add(this.cb_TransferInfeed);
            this.tp_TransferInfeed.Location = new System.Drawing.Point(4, 22);
            this.tp_TransferInfeed.Name = "tp_TransferInfeed";
            this.tp_TransferInfeed.Padding = new System.Windows.Forms.Padding(3);
            this.tp_TransferInfeed.Size = new System.Drawing.Size(588, 128);
            this.tp_TransferInfeed.TabIndex = 3;
            this.tp_TransferInfeed.Text = "Infeeds information";
            this.tp_TransferInfeed.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(105, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Arrival Infeed end";
            // 
            // cb_ArrivalInfeedEnd
            // 
            this.cb_ArrivalInfeedEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ArrivalInfeedEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_ArrivalInfeedEnd.FormattingEnabled = true;
            this.cb_ArrivalInfeedEnd.Location = new System.Drawing.Point(211, 72);
            this.cb_ArrivalInfeedEnd.Name = "cb_ArrivalInfeedEnd";
            this.cb_ArrivalInfeedEnd.Size = new System.Drawing.Size(184, 21);
            this.cb_ArrivalInfeedEnd.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Arrival Infeed start";
            // 
            // cb_ArrivalInfeedStart
            // 
            this.cb_ArrivalInfeedStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ArrivalInfeedStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_ArrivalInfeedStart.FormattingEnabled = true;
            this.cb_ArrivalInfeedStart.Location = new System.Drawing.Point(211, 45);
            this.cb_ArrivalInfeedStart.Name = "cb_ArrivalInfeedStart";
            this.cb_ArrivalInfeedStart.Size = new System.Drawing.Size(184, 21);
            this.cb_ArrivalInfeedStart.TabIndex = 33;
            // 
            // lbl_Terminal_Transfer
            // 
            this.lbl_Terminal_Transfer.AutoSize = true;
            this.lbl_Terminal_Transfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Terminal_Transfer.Location = new System.Drawing.Point(148, 21);
            this.lbl_Terminal_Transfer.Name = "lbl_Terminal_Transfer";
            this.lbl_Terminal_Transfer.Size = new System.Drawing.Size(47, 13);
            this.lbl_Terminal_Transfer.TabIndex = 32;
            this.lbl_Terminal_Transfer.Text = "Terminal";
            // 
            // cb_Terminal_TransferInfeed
            // 
            this.cb_Terminal_TransferInfeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Terminal_TransferInfeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Terminal_TransferInfeed.FormattingEnabled = true;
            this.cb_Terminal_TransferInfeed.Location = new System.Drawing.Point(211, 18);
            this.cb_Terminal_TransferInfeed.Name = "cb_Terminal_TransferInfeed";
            this.cb_Terminal_TransferInfeed.Size = new System.Drawing.Size(184, 21);
            this.cb_Terminal_TransferInfeed.TabIndex = 1;
            this.cb_Terminal_TransferInfeed.SelectedIndexChanged += new System.EventHandler(this.cb_Terminal_TransferInfeed_SelectedIndexChanged);
            // 
            // lbl_Transfer
            // 
            this.lbl_Transfer.AutoSize = true;
            this.lbl_Transfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Transfer.Location = new System.Drawing.Point(117, 102);
            this.lbl_Transfer.Name = "lbl_Transfer";
            this.lbl_Transfer.Size = new System.Drawing.Size(78, 13);
            this.lbl_Transfer.TabIndex = 30;
            this.lbl_Transfer.Text = "Transfer infeed";
            // 
            // cb_TransferInfeed
            // 
            this.cb_TransferInfeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TransferInfeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_TransferInfeed.FormattingEnabled = true;
            this.cb_TransferInfeed.Location = new System.Drawing.Point(211, 99);
            this.cb_TransferInfeed.Name = "cb_TransferInfeed";
            this.cb_TransferInfeed.Size = new System.Drawing.Size(184, 21);
            this.cb_TransferInfeed.TabIndex = 2;
            // 
            // tp_Comments
            // 
            this.tp_Comments.Controls.Add(this.tc_Comments);
            this.tp_Comments.Location = new System.Drawing.Point(4, 22);
            this.tp_Comments.Name = "tp_Comments";
            this.tp_Comments.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Comments.Size = new System.Drawing.Size(588, 128);
            this.tp_Comments.TabIndex = 4;
            this.tp_Comments.Text = "Comments";
            this.tp_Comments.UseVisualStyleBackColor = true;
            // 
            // tc_Comments
            // 
            this.tc_Comments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tc_Comments.Controls.Add(this.tp_Comment1);
            this.tc_Comments.Controls.Add(this.tabPage2);
            this.tc_Comments.Controls.Add(this.tabPage1);
            this.tc_Comments.Controls.Add(this.tabPage3);
            this.tc_Comments.Controls.Add(this.tabPage4);
            this.tc_Comments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc_Comments.Location = new System.Drawing.Point(6, 6);
            this.tc_Comments.Name = "tc_Comments";
            this.tc_Comments.SelectedIndex = 0;
            this.tc_Comments.Size = new System.Drawing.Size(576, 116);
            this.tc_Comments.TabIndex = 0;
            // 
            // tp_Comment1
            // 
            this.tp_Comment1.Controls.Add(this.txt_Comment1);
            this.tp_Comment1.Location = new System.Drawing.Point(4, 22);
            this.tp_Comment1.Name = "tp_Comment1";
            this.tp_Comment1.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Comment1.Size = new System.Drawing.Size(568, 90);
            this.tp_Comment1.TabIndex = 0;
            this.tp_Comment1.Text = "Comment 1";
            this.tp_Comment1.UseVisualStyleBackColor = true;
            // 
            // txt_Comment1
            // 
            this.txt_Comment1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Comment1.Location = new System.Drawing.Point(3, 3);
            this.txt_Comment1.Multiline = true;
            this.txt_Comment1.Name = "txt_Comment1";
            this.txt_Comment1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Comment1.Size = new System.Drawing.Size(562, 84);
            this.txt_Comment1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txt_Comment2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(568, 90);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Comment 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txt_Comment2
            // 
            this.txt_Comment2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Comment2.Location = new System.Drawing.Point(3, 3);
            this.txt_Comment2.Multiline = true;
            this.txt_Comment2.Name = "txt_Comment2";
            this.txt_Comment2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Comment2.Size = new System.Drawing.Size(562, 84);
            this.txt_Comment2.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txt_Comment3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(568, 90);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Comment 3";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txt_Comment3
            // 
            this.txt_Comment3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Comment3.Location = new System.Drawing.Point(3, 3);
            this.txt_Comment3.Multiline = true;
            this.txt_Comment3.Name = "txt_Comment3";
            this.txt_Comment3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Comment3.Size = new System.Drawing.Size(562, 84);
            this.txt_Comment3.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txt_Comment4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(568, 90);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Comment 4";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txt_Comment4
            // 
            this.txt_Comment4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Comment4.Location = new System.Drawing.Point(3, 3);
            this.txt_Comment4.Multiline = true;
            this.txt_Comment4.Name = "txt_Comment4";
            this.txt_Comment4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Comment4.Size = new System.Drawing.Size(562, 84);
            this.txt_Comment4.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txt_Comment5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(568, 90);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Comment 5";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txt_Comment5
            // 
            this.txt_Comment5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Comment5.Location = new System.Drawing.Point(3, 3);
            this.txt_Comment5.Multiline = true;
            this.txt_Comment5.Name = "txt_Comment5";
            this.txt_Comment5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Comment5.Size = new System.Drawing.Size(562, 84);
            this.txt_Comment5.TabIndex = 1;
            // 
            // btnSearchAirline
            // 
            this.btnSearchAirline.BackgroundImage = global::SIMCORE_TOOL.Properties.Resources.Aperçu;
            this.btnSearchAirline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearchAirline.Location = new System.Drawing.Point(429, 66);
            this.btnSearchAirline.Name = "btnSearchAirline";
            this.btnSearchAirline.Size = new System.Drawing.Size(18, 24);
            this.btnSearchAirline.TabIndex = 22;
            this.tt_Tooltip.SetToolTip(this.btnSearchAirline, "Search Airline");
            this.btnSearchAirline.UseVisualStyleBackColor = true;
            this.btnSearchAirline.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchAirport
            // 
            this.btnSearchAirport.BackgroundImage = global::SIMCORE_TOOL.Properties.Resources.Aperçu;
            this.btnSearchAirport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearchAirport.Location = new System.Drawing.Point(429, 91);
            this.btnSearchAirport.Name = "btnSearchAirport";
            this.btnSearchAirport.Size = new System.Drawing.Size(18, 24);
            this.btnSearchAirport.TabIndex = 23;
            this.tt_Tooltip.SetToolTip(this.btnSearchAirport, "Search Airport");
            this.btnSearchAirport.UseVisualStyleBackColor = true;
            this.btnSearchAirport.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchAircraftType
            // 
            this.btnSearchAircraftType.BackgroundImage = global::SIMCORE_TOOL.Properties.Resources.Aperçu;
            this.btnSearchAircraftType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearchAircraftType.Location = new System.Drawing.Point(429, 144);
            this.btnSearchAircraftType.Name = "btnSearchAircraftType";
            this.btnSearchAircraftType.Size = new System.Drawing.Size(18, 24);
            this.btnSearchAircraftType.TabIndex = 24;
            this.tt_Tooltip.SetToolTip(this.btnSearchAircraftType, "Search Aircraft type");
            this.btnSearchAircraftType.UseVisualStyleBackColor = true;
            this.btnSearchAircraftType.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FPA_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(640, 554);
            this.Controls.Add(this.tc_information);
            this.Controls.Add(this.btn_addNew);
            this.Controls.Add(this.lbl_Id);
            this.Controls.Add(this.txt_Id);
            this.Controls.Add(this.gb_FlightInformation);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FPA_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add/Edit a new arrival flight";
            this.gb_FlightInformation.ResumeLayout(false);
            this.gb_FlightInformation.PerformLayout();
            this.tc_information.ResumeLayout(false);
            this.tp_ArrivalGate.ResumeLayout(false);
            this.tp_ArrivalGate.PerformLayout();
            this.tp_ReclaimBelt.ResumeLayout(false);
            this.tp_ReclaimBelt.PerformLayout();
            this.tp_Parking.ResumeLayout(false);
            this.tp_Parking.PerformLayout();
            this.tp_TransferInfeed.ResumeLayout(false);
            this.tp_TransferInfeed.PerformLayout();
            this.tp_Comments.ResumeLayout(false);
            this.tc_Comments.ResumeLayout(false);
            this.tp_Comment1.ResumeLayout(false);
            this.tp_Comment1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.ComboBox cb_Airline;
        private System.Windows.Forms.Label lbl_Airline;
        private System.Windows.Forms.Label lbl_flightN;
        private System.Windows.Forms.Label lbl_Airport;
        private System.Windows.Forms.ComboBox cb_Airport;
        private System.Windows.Forms.Label lbl_FlightCateg;
        private System.Windows.Forms.ComboBox cb_FlightCategory;
        private System.Windows.Forms.Label lbl_Aircraft;
        private System.Windows.Forms.ComboBox cb_AircraftType;
        private System.Windows.Forms.ComboBox cb_TerminalGate;
        private System.Windows.Forms.Label lbl_TerminalGate;
        private System.Windows.Forms.Label lbl_LevelGate;
        private System.Windows.Forms.ComboBox cb_LevelGate;
        private System.Windows.Forms.ComboBox cb_LevelReclaim;
        private System.Windows.Forms.Label lbl_LevelReclaim;
        private System.Windows.Forms.Label lbl_TerminalReclaim;
        private System.Windows.Forms.ComboBox cb_TerminalReclaim;
        private System.Windows.Forms.ComboBox cb_Reclaim;
        private System.Windows.Forms.Label lbl_Reclaim;
        private System.Windows.Forms.GroupBox gb_FlightInformation;
        private System.Windows.Forms.TextBox txt_Id;
        private System.Windows.Forms.Label lbl_Id;
        private System.Windows.Forms.TextBox txt_FlightN;
        private System.Windows.Forms.Label lbl_Gate;
        private System.Windows.Forms.ComboBox cb_Gate;
        private System.Windows.Forms.Button btn_addNew;
        private System.Windows.Forms.Button btn_AddAirline;
        private System.Windows.Forms.Button btn_AddFC;
        private System.Windows.Forms.Button btn_Aircraft;
        private System.Windows.Forms.Button btn_AddAirport;
        private System.Windows.Forms.DateTimePicker dtp_DateTime;
        private System.Windows.Forms.ComboBox cb_Runway;
        private System.Windows.Forms.Label lbl_Parking;
        private System.Windows.Forms.Label lbl_Runway;
        private System.Windows.Forms.ComboBox cb_Parking;
        private System.Windows.Forms.TabControl tc_information;
        private System.Windows.Forms.TabPage tp_ArrivalGate;
        private System.Windows.Forms.TabPage tp_ReclaimBelt;
        private System.Windows.Forms.TabPage tp_Parking;
        private System.Windows.Forms.ComboBox cb_TerminalParking;
        private System.Windows.Forms.Label lbl_TerminalParking;
        private System.Windows.Forms.TabPage tp_TransferInfeed;
        private System.Windows.Forms.Label lbl_Terminal_Transfer;
        private System.Windows.Forms.ComboBox cb_Terminal_TransferInfeed;
        private System.Windows.Forms.Label lbl_Transfer;
        private System.Windows.Forms.ComboBox cb_TransferInfeed;
        private System.Windows.Forms.TextBox txt_NbPax;
        private System.Windows.Forms.Label lbl_NbPax;
        private System.Windows.Forms.ToolTip tt_Tooltip;
        private System.Windows.Forms.CheckBox cb_TSA;
        private System.Windows.Forms.CheckBox cb_CBP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_ArrivalInfeedEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_ArrivalInfeedStart;
        private System.Windows.Forms.TabPage tp_Comments;
        private System.Windows.Forms.TabControl tc_Comments;
        private System.Windows.Forms.TabPage tp_Comment1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txt_Comment1;
        private System.Windows.Forms.TextBox txt_Comment2;
        private System.Windows.Forms.TextBox txt_Comment3;
        private System.Windows.Forms.TextBox txt_Comment4;
        private System.Windows.Forms.TextBox txt_Comment5;
        private System.Windows.Forms.Button btnSearchAirline;
        private System.Windows.Forms.Button btnSearchAircraftType;
        private System.Windows.Forms.Button btnSearchAirport;
    }
}