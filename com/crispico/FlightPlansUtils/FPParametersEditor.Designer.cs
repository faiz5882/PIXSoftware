namespace SIMCORE_TOOL.com.crispico.FlightPlansUtils
{
    partial class FPParametersEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPParametersEditor));
            this.selectionPanel = new System.Windows.Forms.Panel();
            this.flightIdComboBox = new System.Windows.Forms.ComboBox();
            this.arrivalOrDepartureComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flightAttributesPanel = new System.Windows.Forms.Panel();
            this.aircraftTypeTextBox = new System.Windows.Forms.TextBox();
            this.flightCategoryTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.flightNumberTextBox = new System.Windows.Forms.TextBox();
            this.airportTextBox = new System.Windows.Forms.TextBox();
            this.airlineTextBox = new System.Windows.Forms.TextBox();
            this.flightDateTextBox = new System.Windows.Forms.TextBox();
            this.flightDateLabel = new System.Windows.Forms.Label();
            this.flightParametersTabControl = new System.Windows.Forms.TabControl();
            this.nbPaxTabPage = new System.Windows.Forms.TabPage();
            this.nbTermFbPaxTextBox = new System.Windows.Forms.TextBox();
            this.nbTermEcoPaxTextBox = new System.Windows.Forms.TextBox();
            this.nbTransferringFbPaxTextBox = new System.Windows.Forms.TextBox();
            this.nbTransferringEcoPaxTextBox = new System.Windows.Forms.TextBox();
            this.nbOrigFbPaxTextBox = new System.Windows.Forms.TextBox();
            this.nbOrigEcoPaxTextBox = new System.Windows.Forms.TextBox();
            this.labelTermFbPax = new System.Windows.Forms.Label();
            this.labelTermEcoPax = new System.Windows.Forms.Label();
            this.labelTransferFbPax = new System.Windows.Forms.Label();
            this.labelTransferEcoPax = new System.Windows.Forms.Label();
            this.labelOrigFbPax = new System.Windows.Forms.Label();
            this.labelOrigEcoPax = new System.Windows.Forms.Label();
            this.nbBagsTabPage = new System.Windows.Forms.TabPage();
            this.nbTerminatingFbBagsTextBox = new System.Windows.Forms.TextBox();
            this.nbTerminatingEcoBagsTextBox = new System.Windows.Forms.TextBox();
            this.nbTransferringFbBagsTextBox = new System.Windows.Forms.TextBox();
            this.nbTransferringEcoBagsTextBox = new System.Windows.Forms.TextBox();
            this.nbOriginatingFbBagsTextBox = new System.Windows.Forms.TextBox();
            this.nbOriginatingEcoBagsTextBox = new System.Windows.Forms.TextBox();
            this.labelTerminatingFbBags = new System.Windows.Forms.Label();
            this.labelTerminatingEcoBags = new System.Windows.Forms.Label();
            this.labelTransferFbBags = new System.Windows.Forms.Label();
            this.labelTransferEcoBags = new System.Windows.Forms.Label();
            this.labelOrigFbBags = new System.Windows.Forms.Label();
            this.labelOrigEcoBags = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveParameterButton = new System.Windows.Forms.Button();
            this.multiEditPanel = new System.Windows.Forms.Panel();
            this.previewDataGridView = new System.Windows.Forms.DataGridView();
            this.filterFlightsButton = new System.Windows.Forms.Button();
            this.multiEditCheckBox = new System.Windows.Forms.CheckBox();
            this.selectionPanel.SuspendLayout();
            this.flightAttributesPanel.SuspendLayout();
            this.flightParametersTabControl.SuspendLayout();
            this.nbPaxTabPage.SuspendLayout();
            this.nbBagsTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.multiEditPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // selectionPanel
            // 
            this.selectionPanel.BackColor = System.Drawing.Color.Transparent;
            this.selectionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectionPanel.Controls.Add(this.flightIdComboBox);
            this.selectionPanel.Controls.Add(this.arrivalOrDepartureComboBox);
            this.selectionPanel.Controls.Add(this.label2);
            this.selectionPanel.Controls.Add(this.label1);
            this.selectionPanel.Location = new System.Drawing.Point(12, 12);
            this.selectionPanel.Name = "selectionPanel";
            this.selectionPanel.Size = new System.Drawing.Size(598, 52);
            this.selectionPanel.TabIndex = 0;
            // 
            // flightIdComboBox
            // 
            this.flightIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flightIdComboBox.FormattingEnabled = true;
            this.flightIdComboBox.Location = new System.Drawing.Point(237, 20);
            this.flightIdComboBox.Name = "flightIdComboBox";
            this.flightIdComboBox.Size = new System.Drawing.Size(75, 21);
            this.flightIdComboBox.TabIndex = 3;
            this.flightIdComboBox.SelectedIndexChanged += new System.EventHandler(this.flightIdComboBox_SelectedIndexChanged);
            // 
            // arrivalOrDepartureComboBox
            // 
            this.arrivalOrDepartureComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.arrivalOrDepartureComboBox.FormattingEnabled = true;
            this.arrivalOrDepartureComboBox.Location = new System.Drawing.Point(116, 20);
            this.arrivalOrDepartureComboBox.Name = "arrivalOrDepartureComboBox";
            this.arrivalOrDepartureComboBox.Size = new System.Drawing.Size(43, 21);
            this.arrivalOrDepartureComboBox.TabIndex = 2;
            this.arrivalOrDepartureComboBox.SelectedIndexChanged += new System.EventHandler(this.arrivalOrDepartureComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Flight Id";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arrival or Departure";
            // 
            // flightAttributesPanel
            // 
            this.flightAttributesPanel.BackColor = System.Drawing.Color.Transparent;
            this.flightAttributesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flightAttributesPanel.Controls.Add(this.aircraftTypeTextBox);
            this.flightAttributesPanel.Controls.Add(this.flightCategoryTextBox);
            this.flightAttributesPanel.Controls.Add(this.label19);
            this.flightAttributesPanel.Controls.Add(this.label18);
            this.flightAttributesPanel.Controls.Add(this.label17);
            this.flightAttributesPanel.Controls.Add(this.label16);
            this.flightAttributesPanel.Controls.Add(this.label15);
            this.flightAttributesPanel.Controls.Add(this.flightNumberTextBox);
            this.flightAttributesPanel.Controls.Add(this.airportTextBox);
            this.flightAttributesPanel.Controls.Add(this.airlineTextBox);
            this.flightAttributesPanel.Controls.Add(this.flightDateTextBox);
            this.flightAttributesPanel.Controls.Add(this.flightDateLabel);
            this.flightAttributesPanel.Location = new System.Drawing.Point(12, 70);
            this.flightAttributesPanel.Name = "flightAttributesPanel";
            this.flightAttributesPanel.Size = new System.Drawing.Size(598, 111);
            this.flightAttributesPanel.TabIndex = 1;
            // 
            // aircraftTypeTextBox
            // 
            this.aircraftTypeTextBox.Location = new System.Drawing.Point(390, 70);
            this.aircraftTypeTextBox.Name = "aircraftTypeTextBox";
            this.aircraftTypeTextBox.ReadOnly = true;
            this.aircraftTypeTextBox.Size = new System.Drawing.Size(127, 20);
            this.aircraftTypeTextBox.TabIndex = 11;
            // 
            // flightCategoryTextBox
            // 
            this.flightCategoryTextBox.Location = new System.Drawing.Point(390, 44);
            this.flightCategoryTextBox.Name = "flightCategoryTextBox";
            this.flightCategoryTextBox.ReadOnly = true;
            this.flightCategoryTextBox.Size = new System.Drawing.Size(127, 20);
            this.flightCategoryTextBox.TabIndex = 10;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(307, 73);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 13);
            this.label19.TabIndex = 9;
            this.label19.Text = "Aircraft Type";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(71, 73);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Airport";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(71, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Airline";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(307, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "Flight Category";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(307, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Flight Number";
            // 
            // flightNumberTextBox
            // 
            this.flightNumberTextBox.Location = new System.Drawing.Point(390, 18);
            this.flightNumberTextBox.Name = "flightNumberTextBox";
            this.flightNumberTextBox.ReadOnly = true;
            this.flightNumberTextBox.Size = new System.Drawing.Size(127, 20);
            this.flightNumberTextBox.TabIndex = 4;
            // 
            // airportTextBox
            // 
            this.airportTextBox.Location = new System.Drawing.Point(116, 70);
            this.airportTextBox.Name = "airportTextBox";
            this.airportTextBox.ReadOnly = true;
            this.airportTextBox.Size = new System.Drawing.Size(127, 20);
            this.airportTextBox.TabIndex = 3;
            // 
            // airlineTextBox
            // 
            this.airlineTextBox.Location = new System.Drawing.Point(116, 44);
            this.airlineTextBox.Name = "airlineTextBox";
            this.airlineTextBox.ReadOnly = true;
            this.airlineTextBox.Size = new System.Drawing.Size(127, 20);
            this.airlineTextBox.TabIndex = 2;
            // 
            // flightDateTextBox
            // 
            this.flightDateTextBox.Location = new System.Drawing.Point(116, 18);
            this.flightDateTextBox.Name = "flightDateTextBox";
            this.flightDateTextBox.ReadOnly = true;
            this.flightDateTextBox.Size = new System.Drawing.Size(127, 20);
            this.flightDateTextBox.TabIndex = 1;
            // 
            // flightDateLabel
            // 
            this.flightDateLabel.AutoSize = true;
            this.flightDateLabel.Location = new System.Drawing.Point(71, 21);
            this.flightDateLabel.Name = "flightDateLabel";
            this.flightDateLabel.Size = new System.Drawing.Size(30, 13);
            this.flightDateLabel.TabIndex = 0;
            this.flightDateLabel.Text = "Date";
            // 
            // flightParametersTabControl
            // 
            this.flightParametersTabControl.Controls.Add(this.nbPaxTabPage);
            this.flightParametersTabControl.Controls.Add(this.nbBagsTabPage);
            this.flightParametersTabControl.Location = new System.Drawing.Point(3, 1);
            this.flightParametersTabControl.Name = "flightParametersTabControl";
            this.flightParametersTabControl.SelectedIndex = 0;
            this.flightParametersTabControl.Size = new System.Drawing.Size(590, 123);
            this.flightParametersTabControl.TabIndex = 2;
            // 
            // nbPaxTabPage
            // 
            this.nbPaxTabPage.Controls.Add(this.nbTermFbPaxTextBox);
            this.nbPaxTabPage.Controls.Add(this.nbTermEcoPaxTextBox);
            this.nbPaxTabPage.Controls.Add(this.nbTransferringFbPaxTextBox);
            this.nbPaxTabPage.Controls.Add(this.nbTransferringEcoPaxTextBox);
            this.nbPaxTabPage.Controls.Add(this.nbOrigFbPaxTextBox);
            this.nbPaxTabPage.Controls.Add(this.nbOrigEcoPaxTextBox);
            this.nbPaxTabPage.Controls.Add(this.labelTermFbPax);
            this.nbPaxTabPage.Controls.Add(this.labelTermEcoPax);
            this.nbPaxTabPage.Controls.Add(this.labelTransferFbPax);
            this.nbPaxTabPage.Controls.Add(this.labelTransferEcoPax);
            this.nbPaxTabPage.Controls.Add(this.labelOrigFbPax);
            this.nbPaxTabPage.Controls.Add(this.labelOrigEcoPax);
            this.nbPaxTabPage.Location = new System.Drawing.Point(4, 22);
            this.nbPaxTabPage.Name = "nbPaxTabPage";
            this.nbPaxTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.nbPaxTabPage.Size = new System.Drawing.Size(582, 97);
            this.nbPaxTabPage.TabIndex = 0;
            this.nbPaxTabPage.Text = "Number of Pax";
            this.nbPaxTabPage.UseVisualStyleBackColor = true;
            // 
            // nbTermFbPaxTextBox
            // 
            this.nbTermFbPaxTextBox.Location = new System.Drawing.Point(519, 59);
            this.nbTermFbPaxTextBox.Name = "nbTermFbPaxTextBox";
            this.nbTermFbPaxTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbTermFbPaxTextBox.TabIndex = 11;
            this.nbTermFbPaxTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbTermFbPaxTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbTermEcoPaxTextBox
            // 
            this.nbTermEcoPaxTextBox.Location = new System.Drawing.Point(519, 20);
            this.nbTermEcoPaxTextBox.Name = "nbTermEcoPaxTextBox";
            this.nbTermEcoPaxTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbTermEcoPaxTextBox.TabIndex = 10;
            this.nbTermEcoPaxTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbTermEcoPaxTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbTransferringFbPaxTextBox
            // 
            this.nbTransferringFbPaxTextBox.Location = new System.Drawing.Point(324, 59);
            this.nbTransferringFbPaxTextBox.Name = "nbTransferringFbPaxTextBox";
            this.nbTransferringFbPaxTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbTransferringFbPaxTextBox.TabIndex = 9;
            this.nbTransferringFbPaxTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbTransferringFbPaxTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbTransferringEcoPaxTextBox
            // 
            this.nbTransferringEcoPaxTextBox.Location = new System.Drawing.Point(324, 20);
            this.nbTransferringEcoPaxTextBox.Name = "nbTransferringEcoPaxTextBox";
            this.nbTransferringEcoPaxTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbTransferringEcoPaxTextBox.TabIndex = 8;
            this.nbTransferringEcoPaxTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbTransferringEcoPaxTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbOrigFbPaxTextBox
            // 
            this.nbOrigFbPaxTextBox.Location = new System.Drawing.Point(138, 59);
            this.nbOrigFbPaxTextBox.Name = "nbOrigFbPaxTextBox";
            this.nbOrigFbPaxTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbOrigFbPaxTextBox.TabIndex = 7;
            this.nbOrigFbPaxTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbOrigFbPaxTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbOrigEcoPaxTextBox
            // 
            this.nbOrigEcoPaxTextBox.Location = new System.Drawing.Point(138, 20);
            this.nbOrigEcoPaxTextBox.Name = "nbOrigEcoPaxTextBox";
            this.nbOrigEcoPaxTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbOrigEcoPaxTextBox.TabIndex = 6;
            this.nbOrigEcoPaxTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbOrigEcoPaxTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // labelTermFbPax
            // 
            this.labelTermFbPax.AutoSize = true;
            this.labelTermFbPax.Location = new System.Drawing.Point(388, 64);
            this.labelTermFbPax.Name = "labelTermFbPax";
            this.labelTermFbPax.Size = new System.Drawing.Size(116, 13);
            this.labelTermFbPax.TabIndex = 5;
            this.labelTermFbPax.Text = "Nb Terminating FB Pax";
            // 
            // labelTermEcoPax
            // 
            this.labelTermEcoPax.AutoSize = true;
            this.labelTermEcoPax.Location = new System.Drawing.Point(388, 23);
            this.labelTermEcoPax.Name = "labelTermEcoPax";
            this.labelTermEcoPax.Size = new System.Drawing.Size(125, 13);
            this.labelTermEcoPax.TabIndex = 4;
            this.labelTermEcoPax.Text = "Nb Terminating Eco. Pax";
            // 
            // labelTransferFbPax
            // 
            this.labelTransferFbPax.AutoSize = true;
            this.labelTransferFbPax.Location = new System.Drawing.Point(192, 64);
            this.labelTransferFbPax.Name = "labelTransferFbPax";
            this.labelTransferFbPax.Size = new System.Drawing.Size(117, 13);
            this.labelTransferFbPax.TabIndex = 3;
            this.labelTransferFbPax.Text = "Nb Transferring FB Pax";
            // 
            // labelTransferEcoPax
            // 
            this.labelTransferEcoPax.AutoSize = true;
            this.labelTransferEcoPax.Location = new System.Drawing.Point(192, 23);
            this.labelTransferEcoPax.Name = "labelTransferEcoPax";
            this.labelTransferEcoPax.Size = new System.Drawing.Size(126, 13);
            this.labelTransferEcoPax.TabIndex = 2;
            this.labelTransferEcoPax.Text = "Nb Transferring Eco. Pax";
            // 
            // labelOrigFbPax
            // 
            this.labelOrigFbPax.AutoSize = true;
            this.labelOrigFbPax.Location = new System.Drawing.Point(8, 64);
            this.labelOrigFbPax.Name = "labelOrigFbPax";
            this.labelOrigFbPax.Size = new System.Drawing.Size(111, 13);
            this.labelOrigFbPax.TabIndex = 1;
            this.labelOrigFbPax.Text = "Nb Originating FB Pax";
            // 
            // labelOrigEcoPax
            // 
            this.labelOrigEcoPax.AutoSize = true;
            this.labelOrigEcoPax.Location = new System.Drawing.Point(8, 23);
            this.labelOrigEcoPax.Name = "labelOrigEcoPax";
            this.labelOrigEcoPax.Size = new System.Drawing.Size(120, 13);
            this.labelOrigEcoPax.TabIndex = 0;
            this.labelOrigEcoPax.Text = "Nb Originating Eco. Pax";
            // 
            // nbBagsTabPage
            // 
            this.nbBagsTabPage.Controls.Add(this.nbTerminatingFbBagsTextBox);
            this.nbBagsTabPage.Controls.Add(this.nbTerminatingEcoBagsTextBox);
            this.nbBagsTabPage.Controls.Add(this.nbTransferringFbBagsTextBox);
            this.nbBagsTabPage.Controls.Add(this.nbTransferringEcoBagsTextBox);
            this.nbBagsTabPage.Controls.Add(this.nbOriginatingFbBagsTextBox);
            this.nbBagsTabPage.Controls.Add(this.nbOriginatingEcoBagsTextBox);
            this.nbBagsTabPage.Controls.Add(this.labelTerminatingFbBags);
            this.nbBagsTabPage.Controls.Add(this.labelTerminatingEcoBags);
            this.nbBagsTabPage.Controls.Add(this.labelTransferFbBags);
            this.nbBagsTabPage.Controls.Add(this.labelTransferEcoBags);
            this.nbBagsTabPage.Controls.Add(this.labelOrigFbBags);
            this.nbBagsTabPage.Controls.Add(this.labelOrigEcoBags);
            this.nbBagsTabPage.Location = new System.Drawing.Point(4, 22);
            this.nbBagsTabPage.Name = "nbBagsTabPage";
            this.nbBagsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.nbBagsTabPage.Size = new System.Drawing.Size(582, 97);
            this.nbBagsTabPage.TabIndex = 1;
            this.nbBagsTabPage.Text = "Number of Bags";
            this.nbBagsTabPage.UseVisualStyleBackColor = true;
            // 
            // nbTerminatingFbBagsTextBox
            // 
            this.nbTerminatingFbBagsTextBox.Location = new System.Drawing.Point(519, 59);
            this.nbTerminatingFbBagsTextBox.Name = "nbTerminatingFbBagsTextBox";
            this.nbTerminatingFbBagsTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbTerminatingFbBagsTextBox.TabIndex = 11;
            this.nbTerminatingFbBagsTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbTerminatingFbBagsTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbTerminatingEcoBagsTextBox
            // 
            this.nbTerminatingEcoBagsTextBox.Location = new System.Drawing.Point(519, 20);
            this.nbTerminatingEcoBagsTextBox.Name = "nbTerminatingEcoBagsTextBox";
            this.nbTerminatingEcoBagsTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbTerminatingEcoBagsTextBox.TabIndex = 10;
            this.nbTerminatingEcoBagsTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbTerminatingEcoBagsTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbTransferringFbBagsTextBox
            // 
            this.nbTransferringFbBagsTextBox.Location = new System.Drawing.Point(324, 59);
            this.nbTransferringFbBagsTextBox.Name = "nbTransferringFbBagsTextBox";
            this.nbTransferringFbBagsTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbTransferringFbBagsTextBox.TabIndex = 9;
            this.nbTransferringFbBagsTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbTransferringFbBagsTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbTransferringEcoBagsTextBox
            // 
            this.nbTransferringEcoBagsTextBox.Location = new System.Drawing.Point(324, 20);
            this.nbTransferringEcoBagsTextBox.Name = "nbTransferringEcoBagsTextBox";
            this.nbTransferringEcoBagsTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbTransferringEcoBagsTextBox.TabIndex = 8;
            this.nbTransferringEcoBagsTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbTransferringEcoBagsTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbOriginatingFbBagsTextBox
            // 
            this.nbOriginatingFbBagsTextBox.Location = new System.Drawing.Point(138, 59);
            this.nbOriginatingFbBagsTextBox.Name = "nbOriginatingFbBagsTextBox";
            this.nbOriginatingFbBagsTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbOriginatingFbBagsTextBox.TabIndex = 7;
            this.nbOriginatingFbBagsTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbOriginatingFbBagsTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // nbOriginatingEcoBagsTextBox
            // 
            this.nbOriginatingEcoBagsTextBox.Location = new System.Drawing.Point(138, 20);
            this.nbOriginatingEcoBagsTextBox.Name = "nbOriginatingEcoBagsTextBox";
            this.nbOriginatingEcoBagsTextBox.Size = new System.Drawing.Size(44, 20);
            this.nbOriginatingEcoBagsTextBox.TabIndex = 6;
            this.nbOriginatingEcoBagsTextBox.TextChanged += new System.EventHandler(this.parameterTextBox_TextChanged);
            this.nbOriginatingEcoBagsTextBox.Leave += new System.EventHandler(this.parameterTextBox_FocusOut);
            // 
            // labelTerminatingFbBags
            // 
            this.labelTerminatingFbBags.AutoSize = true;
            this.labelTerminatingFbBags.Location = new System.Drawing.Point(388, 62);
            this.labelTerminatingFbBags.Name = "labelTerminatingFbBags";
            this.labelTerminatingFbBags.Size = new System.Drawing.Size(122, 13);
            this.labelTerminatingFbBags.TabIndex = 5;
            this.labelTerminatingFbBags.Text = "Nb Terminating FB Bags";
            // 
            // labelTerminatingEcoBags
            // 
            this.labelTerminatingEcoBags.AutoSize = true;
            this.labelTerminatingEcoBags.Location = new System.Drawing.Point(388, 23);
            this.labelTerminatingEcoBags.Name = "labelTerminatingEcoBags";
            this.labelTerminatingEcoBags.Size = new System.Drawing.Size(131, 13);
            this.labelTerminatingEcoBags.TabIndex = 4;
            this.labelTerminatingEcoBags.Text = "Nb Terminating Eco. Bags";
            // 
            // labelTransferFbBags
            // 
            this.labelTransferFbBags.AutoSize = true;
            this.labelTransferFbBags.Location = new System.Drawing.Point(192, 64);
            this.labelTransferFbBags.Name = "labelTransferFbBags";
            this.labelTransferFbBags.Size = new System.Drawing.Size(123, 13);
            this.labelTransferFbBags.TabIndex = 3;
            this.labelTransferFbBags.Text = "Nb Transferring FB Bags";
            // 
            // labelTransferEcoBags
            // 
            this.labelTransferEcoBags.AutoSize = true;
            this.labelTransferEcoBags.Location = new System.Drawing.Point(192, 23);
            this.labelTransferEcoBags.Name = "labelTransferEcoBags";
            this.labelTransferEcoBags.Size = new System.Drawing.Size(132, 13);
            this.labelTransferEcoBags.TabIndex = 2;
            this.labelTransferEcoBags.Text = "Nb Transferring Eco. Bags";
            // 
            // labelOrigFbBags
            // 
            this.labelOrigFbBags.AutoSize = true;
            this.labelOrigFbBags.Location = new System.Drawing.Point(8, 64);
            this.labelOrigFbBags.Name = "labelOrigFbBags";
            this.labelOrigFbBags.Size = new System.Drawing.Size(117, 13);
            this.labelOrigFbBags.TabIndex = 1;
            this.labelOrigFbBags.Text = "Nb Originating FB Bags";
            // 
            // labelOrigEcoBags
            // 
            this.labelOrigEcoBags.AutoSize = true;
            this.labelOrigEcoBags.Location = new System.Drawing.Point(8, 23);
            this.labelOrigEcoBags.Name = "labelOrigEcoBags";
            this.labelOrigEcoBags.Size = new System.Drawing.Size(126, 13);
            this.labelOrigEcoBags.TabIndex = 0;
            this.labelOrigEcoBags.Text = "Nb Originating Eco. Bags";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(154, 644);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(344, 644);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.saveParameterButton);
            this.panel1.Controls.Add(this.flightParametersTabControl);
            this.panel1.Location = new System.Drawing.Point(12, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 163);
            this.panel1.TabIndex = 5;
            // 
            // saveParameterButton
            // 
            this.saveParameterButton.Enabled = false;
            this.saveParameterButton.Location = new System.Drawing.Point(398, 132);
            this.saveParameterButton.Name = "saveParameterButton";
            this.saveParameterButton.Size = new System.Drawing.Size(131, 23);
            this.saveParameterButton.TabIndex = 3;
            this.saveParameterButton.Text = "Save Parameters";
            this.saveParameterButton.UseVisualStyleBackColor = true;
            this.saveParameterButton.Click += new System.EventHandler(this.updateParameterButton_Click);
            // 
            // multiEditPanel
            // 
            this.multiEditPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.multiEditPanel.BackColor = System.Drawing.Color.Transparent;
            this.multiEditPanel.Controls.Add(this.previewDataGridView);
            this.multiEditPanel.Controls.Add(this.filterFlightsButton);
            this.multiEditPanel.Enabled = false;
            this.multiEditPanel.Location = new System.Drawing.Point(12, 206);
            this.multiEditPanel.Name = "multiEditPanel";
            this.multiEditPanel.Size = new System.Drawing.Size(598, 261);
            this.multiEditPanel.TabIndex = 6;
            // 
            // previewDataGridView
            // 
            this.previewDataGridView.AllowUserToAddRows = false;
            this.previewDataGridView.AllowUserToDeleteRows = false;
            this.previewDataGridView.AllowUserToOrderColumns = true;
            this.previewDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.previewDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.previewDataGridView.Location = new System.Drawing.Point(4, 4);
            this.previewDataGridView.Name = "previewDataGridView";
            this.previewDataGridView.ReadOnly = true;
            this.previewDataGridView.RowHeadersVisible = false;
            this.previewDataGridView.ShowEditingIcon = false;
            this.previewDataGridView.Size = new System.Drawing.Size(586, 223);
            this.previewDataGridView.TabIndex = 1;
            // 
            // filterFlightsButton
            // 
            this.filterFlightsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filterFlightsButton.Location = new System.Drawing.Point(224, 233);
            this.filterFlightsButton.Name = "filterFlightsButton";
            this.filterFlightsButton.Size = new System.Drawing.Size(131, 23);
            this.filterFlightsButton.TabIndex = 0;
            this.filterFlightsButton.Text = "Filter Flights";
            this.filterFlightsButton.UseVisualStyleBackColor = true;
            this.filterFlightsButton.Click += new System.EventHandler(this.filterFlightsButton_Click);
            // 
            // multiEditCheckBox
            // 
            this.multiEditCheckBox.AutoSize = true;
            this.multiEditCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.multiEditCheckBox.Location = new System.Drawing.Point(12, 187);
            this.multiEditCheckBox.Name = "multiEditCheckBox";
            this.multiEditCheckBox.Size = new System.Drawing.Size(103, 17);
            this.multiEditCheckBox.TabIndex = 7;
            this.multiEditCheckBox.Text = "Enable multi-edit";
            this.multiEditCheckBox.UseVisualStyleBackColor = false;
            this.multiEditCheckBox.CheckedChanged += new System.EventHandler(this.multiEditCheckBox_CheckedChanged);
            // 
            // FPParametersEditor
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(617, 678);
            this.Controls.Add(this.multiEditCheckBox);
            this.Controls.Add(this.multiEditPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.flightAttributesPanel);
            this.Controls.Add(this.selectionPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FPParametersEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.selectionPanel.ResumeLayout(false);
            this.selectionPanel.PerformLayout();
            this.flightAttributesPanel.ResumeLayout(false);
            this.flightAttributesPanel.PerformLayout();
            this.flightParametersTabControl.ResumeLayout(false);
            this.nbPaxTabPage.ResumeLayout(false);
            this.nbPaxTabPage.PerformLayout();
            this.nbBagsTabPage.ResumeLayout(false);
            this.nbBagsTabPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.multiEditPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel selectionPanel;
        private System.Windows.Forms.ComboBox flightIdComboBox;
        private System.Windows.Forms.ComboBox arrivalOrDepartureComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel flightAttributesPanel;
        private System.Windows.Forms.TabControl flightParametersTabControl;
        private System.Windows.Forms.TabPage nbPaxTabPage;
        private System.Windows.Forms.TabPage nbBagsTabPage;
        private System.Windows.Forms.TextBox nbTermFbPaxTextBox;
        private System.Windows.Forms.TextBox nbTermEcoPaxTextBox;
        private System.Windows.Forms.TextBox nbTransferringFbPaxTextBox;
        private System.Windows.Forms.TextBox nbTransferringEcoPaxTextBox;
        private System.Windows.Forms.TextBox nbOrigFbPaxTextBox;
        private System.Windows.Forms.TextBox nbOrigEcoPaxTextBox;
        private System.Windows.Forms.Label labelTermFbPax;
        private System.Windows.Forms.Label labelTermEcoPax;
        private System.Windows.Forms.Label labelTransferFbPax;
        private System.Windows.Forms.Label labelTransferEcoPax;
        private System.Windows.Forms.Label labelOrigFbPax;
        private System.Windows.Forms.Label labelOrigEcoPax;
        private System.Windows.Forms.TextBox nbTerminatingFbBagsTextBox;
        private System.Windows.Forms.TextBox nbTerminatingEcoBagsTextBox;
        private System.Windows.Forms.TextBox nbTransferringFbBagsTextBox;
        private System.Windows.Forms.TextBox nbTransferringEcoBagsTextBox;
        private System.Windows.Forms.TextBox nbOriginatingFbBagsTextBox;
        private System.Windows.Forms.TextBox nbOriginatingEcoBagsTextBox;
        private System.Windows.Forms.Label labelTerminatingFbBags;
        private System.Windows.Forms.Label labelTerminatingEcoBags;
        private System.Windows.Forms.Label labelTransferFbBags;
        private System.Windows.Forms.Label labelTransferEcoBags;
        private System.Windows.Forms.Label labelOrigFbBags;
        private System.Windows.Forms.Label labelOrigEcoBags;
        private System.Windows.Forms.Label flightDateLabel;
        private System.Windows.Forms.TextBox flightDateTextBox;
        private System.Windows.Forms.TextBox aircraftTypeTextBox;
        private System.Windows.Forms.TextBox flightCategoryTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox flightNumberTextBox;
        private System.Windows.Forms.TextBox airportTextBox;
        private System.Windows.Forms.TextBox airlineTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveParameterButton;
        private System.Windows.Forms.Panel multiEditPanel;
        private System.Windows.Forms.CheckBox multiEditCheckBox;
        private System.Windows.Forms.Button filterFlightsButton;
        private System.Windows.Forms.DataGridView previewDataGridView;
    }
}