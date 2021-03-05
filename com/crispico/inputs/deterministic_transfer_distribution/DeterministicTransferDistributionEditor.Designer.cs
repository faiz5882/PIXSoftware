namespace SIMCORE_TOOL.com.crispico.inputs.deterministic_transfer_distribution
{
    partial class DeterministicTransferDistributionEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeterministicTransferDistributionEditor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.flightIdComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.flightPlanComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.aircraftTypeTextBox = new System.Windows.Forms.TextBox();
            this.flightCategoryTextBox = new System.Windows.Forms.TextBox();
            this.flightNumberTextBox = new System.Windows.Forms.TextBox();
            this.airportTextBox = new System.Windows.Forms.TextBox();
            this.airlineTextBox = new System.Windows.Forms.TextBox();
            this.flightDateTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.parametersPanel = new System.Windows.Forms.Panel();
            this.saveParametersButton = new System.Windows.Forms.Button();
            this.rulesPanel = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.parametersPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.flightIdComboBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.flightPlanComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 133);
            this.panel1.TabIndex = 0;
            // 
            // flightIdComboBox
            // 
            this.flightIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flightIdComboBox.FormattingEnabled = true;
            this.flightIdComboBox.Location = new System.Drawing.Point(237, 74);
            this.flightIdComboBox.Name = "flightIdComboBox";
            this.flightIdComboBox.Size = new System.Drawing.Size(64, 21);
            this.flightIdComboBox.TabIndex = 5;
            this.flightIdComboBox.SelectedIndexChanged += new System.EventHandler(this.flightIdComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Flight ID";
            // 
            // flightPlanComboBox
            // 
            this.flightPlanComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flightPlanComboBox.FormattingEnabled = true;
            this.flightPlanComboBox.Location = new System.Drawing.Point(237, 36);
            this.flightPlanComboBox.Name = "flightPlanComboBox";
            this.flightPlanComboBox.Size = new System.Drawing.Size(181, 21);
            this.flightPlanComboBox.TabIndex = 3;
            this.flightPlanComboBox.SelectedIndexChanged += new System.EventHandler(this.flightPlanComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Flight Plan";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.aircraftTypeTextBox);
            this.panel2.Controls.Add(this.flightCategoryTextBox);
            this.panel2.Controls.Add(this.flightNumberTextBox);
            this.panel2.Controls.Add(this.airportTextBox);
            this.panel2.Controls.Add(this.airlineTextBox);
            this.panel2.Controls.Add(this.flightDateTextBox);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(12, 151);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(622, 113);
            this.panel2.TabIndex = 1;
            // 
            // aircraftTypeTextBox
            // 
            this.aircraftTypeTextBox.Location = new System.Drawing.Point(390, 70);
            this.aircraftTypeTextBox.Name = "aircraftTypeTextBox";
            this.aircraftTypeTextBox.ReadOnly = true;
            this.aircraftTypeTextBox.Size = new System.Drawing.Size(130, 20);
            this.aircraftTypeTextBox.TabIndex = 11;
            // 
            // flightCategoryTextBox
            // 
            this.flightCategoryTextBox.Location = new System.Drawing.Point(390, 44);
            this.flightCategoryTextBox.Name = "flightCategoryTextBox";
            this.flightCategoryTextBox.ReadOnly = true;
            this.flightCategoryTextBox.Size = new System.Drawing.Size(130, 20);
            this.flightCategoryTextBox.TabIndex = 10;
            // 
            // flightNumberTextBox
            // 
            this.flightNumberTextBox.Location = new System.Drawing.Point(390, 18);
            this.flightNumberTextBox.Name = "flightNumberTextBox";
            this.flightNumberTextBox.ReadOnly = true;
            this.flightNumberTextBox.Size = new System.Drawing.Size(130, 20);
            this.flightNumberTextBox.TabIndex = 9;
            // 
            // airportTextBox
            // 
            this.airportTextBox.Location = new System.Drawing.Point(116, 70);
            this.airportTextBox.Name = "airportTextBox";
            this.airportTextBox.ReadOnly = true;
            this.airportTextBox.Size = new System.Drawing.Size(130, 20);
            this.airportTextBox.TabIndex = 8;
            // 
            // airlineTextBox
            // 
            this.airlineTextBox.Location = new System.Drawing.Point(116, 44);
            this.airlineTextBox.Name = "airlineTextBox";
            this.airlineTextBox.ReadOnly = true;
            this.airlineTextBox.Size = new System.Drawing.Size(130, 20);
            this.airlineTextBox.TabIndex = 7;
            // 
            // flightDateTextBox
            // 
            this.flightDateTextBox.Location = new System.Drawing.Point(116, 18);
            this.flightDateTextBox.Name = "flightDateTextBox";
            this.flightDateTextBox.ReadOnly = true;
            this.flightDateTextBox.Size = new System.Drawing.Size(130, 20);
            this.flightDateTextBox.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(307, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Aircraft Type";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(307, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Flight Category";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(307, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Flight Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Airport";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Airline";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Date";
            // 
            // parametersPanel
            // 
            this.parametersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.parametersPanel.BackColor = System.Drawing.Color.Transparent;
            this.parametersPanel.Controls.Add(this.saveParametersButton);
            this.parametersPanel.Controls.Add(this.rulesPanel);
            this.parametersPanel.Location = new System.Drawing.Point(12, 270);
            this.parametersPanel.Name = "parametersPanel";
            this.parametersPanel.Size = new System.Drawing.Size(622, 257);
            this.parametersPanel.TabIndex = 2;
            // 
            // saveParametersButton
            // 
            this.saveParametersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveParametersButton.Enabled = false;
            this.saveParametersButton.Location = new System.Drawing.Point(420, 226);
            this.saveParametersButton.Name = "saveParametersButton";
            this.saveParametersButton.Size = new System.Drawing.Size(131, 23);
            this.saveParametersButton.TabIndex = 1;
            this.saveParametersButton.Text = "Save Parameters";
            this.saveParametersButton.UseVisualStyleBackColor = true;
            this.saveParametersButton.Click += new System.EventHandler(this.saveParametersButton_Click);
            // 
            // rulesPanel
            // 
            this.rulesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.rulesPanel.AutoScroll = true;
            this.rulesPanel.Location = new System.Drawing.Point(5, 3);
            this.rulesPanel.Name = "rulesPanel";
            this.rulesPanel.Size = new System.Drawing.Size(614, 217);
            this.rulesPanel.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(184, 545);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Tag = "OK";
            this.okButton.Text = "O&k";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(377, 545);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // DeterministicTransferDistributionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 576);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.parametersPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeterministicTransferDistributionEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deterministic Transfer Distribution Editor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.parametersPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox flightPlanComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox flightIdComboBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox aircraftTypeTextBox;
        private System.Windows.Forms.TextBox flightCategoryTextBox;
        private System.Windows.Forms.TextBox flightNumberTextBox;
        private System.Windows.Forms.TextBox airportTextBox;
        private System.Windows.Forms.TextBox airlineTextBox;
        private System.Windows.Forms.TextBox flightDateTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel parametersPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveParametersButton;
        private System.Windows.Forms.Panel rulesPanel;
    }
}