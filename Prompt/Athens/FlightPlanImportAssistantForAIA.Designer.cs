namespace SIMCORE_TOOL.Prompt.Athens
{
    partial class FlightPlanImportAssistantForAIA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightPlanImportAssistantForAIA));
            this.openFileDialogElement = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFlightPlanFileDialogButton = new System.Windows.Forms.Button();
            this.flightPlanComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openMakeUpAllocationFileDialogButton = new System.Windows.Forms.Button();
            this.makeUpAllocationComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.openLocalAirportsFileDialogButton = new System.Windows.Forms.Button();
            this.localAirportsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogElement
            // 
            this.openFileDialogElement.FileName = "openFileDialog1";
            this.openFileDialogElement.Filter = "All file | *.*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Flight Plan";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.openFlightPlanFileDialogButton);
            this.groupBox1.Controls.Add(this.flightPlanComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(550, 58);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Flight Plan Information";
            // 
            // openFlightPlanFileDialogButton
            // 
            this.openFlightPlanFileDialogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openFlightPlanFileDialogButton.Location = new System.Drawing.Point(506, 21);
            this.openFlightPlanFileDialogButton.Name = "openFlightPlanFileDialogButton";
            this.openFlightPlanFileDialogButton.Size = new System.Drawing.Size(32, 23);
            this.openFlightPlanFileDialogButton.TabIndex = 2;
            this.openFlightPlanFileDialogButton.Text = "...";
            this.openFlightPlanFileDialogButton.UseVisualStyleBackColor = true;
            this.openFlightPlanFileDialogButton.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // flightPlanComboBox
            // 
            this.flightPlanComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flightPlanComboBox.FormattingEnabled = true;
            this.flightPlanComboBox.Location = new System.Drawing.Point(129, 23);
            this.flightPlanComboBox.Name = "flightPlanComboBox";
            this.flightPlanComboBox.Size = new System.Drawing.Size(361, 21);
            this.flightPlanComboBox.TabIndex = 1;
            this.flightPlanComboBox.Tag = "FlightPlan.txt";
            this.flightPlanComboBox.SelectedIndexChanged += new System.EventHandler(this.flightPlanComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.openMakeUpAllocationFileDialogButton);
            this.groupBox2.Controls.Add(this.makeUpAllocationComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(14, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(550, 52);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Allocation Information";
            // 
            // openMakeUpAllocationFileDialogButton
            // 
            this.openMakeUpAllocationFileDialogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openMakeUpAllocationFileDialogButton.Location = new System.Drawing.Point(506, 19);
            this.openMakeUpAllocationFileDialogButton.Name = "openMakeUpAllocationFileDialogButton";
            this.openMakeUpAllocationFileDialogButton.Size = new System.Drawing.Size(32, 23);
            this.openMakeUpAllocationFileDialogButton.TabIndex = 10;
            this.openMakeUpAllocationFileDialogButton.Text = "...";
            this.openMakeUpAllocationFileDialogButton.UseVisualStyleBackColor = true;
            this.openMakeUpAllocationFileDialogButton.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // makeUpAllocationComboBox
            // 
            this.makeUpAllocationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.makeUpAllocationComboBox.FormattingEnabled = true;
            this.makeUpAllocationComboBox.Location = new System.Drawing.Point(129, 21);
            this.makeUpAllocationComboBox.Name = "makeUpAllocationComboBox";
            this.makeUpAllocationComboBox.Size = new System.Drawing.Size(361, 21);
            this.makeUpAllocationComboBox.TabIndex = 8;
            this.makeUpAllocationComboBox.Tag = "MakeUpAllocation.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "MakeUp Allocation";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.openLocalAirportsFileDialogButton);
            this.groupBox3.Controls.Add(this.localAirportsComboBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(14, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(550, 67);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Airports Information";
            // 
            // openLocalAirportsFileDialogButton
            // 
            this.openLocalAirportsFileDialogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openLocalAirportsFileDialogButton.Location = new System.Drawing.Point(506, 22);
            this.openLocalAirportsFileDialogButton.Name = "openLocalAirportsFileDialogButton";
            this.openLocalAirportsFileDialogButton.Size = new System.Drawing.Size(32, 23);
            this.openLocalAirportsFileDialogButton.TabIndex = 7;
            this.openLocalAirportsFileDialogButton.Text = "...";
            this.openLocalAirportsFileDialogButton.UseVisualStyleBackColor = true;
            this.openLocalAirportsFileDialogButton.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // localAirportsComboBox
            // 
            this.localAirportsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.localAirportsComboBox.FormattingEnabled = true;
            this.localAirportsComboBox.Location = new System.Drawing.Point(131, 24);
            this.localAirportsComboBox.Name = "localAirportsComboBox";
            this.localAirportsComboBox.Size = new System.Drawing.Size(359, 21);
            this.localAirportsComboBox.TabIndex = 6;
            this.localAirportsComboBox.Tag = "LocalAirports.txt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Local Airports";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.endDatePicker);
            this.groupBox4.Controls.Add(this.startDatePicker);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(14, 223);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(550, 85);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Extraction Period";
            // 
            // endDatePicker
            // 
            this.endDatePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDatePicker.Location = new System.Drawing.Point(129, 50);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(150, 20);
            this.endDatePicker.TabIndex = 13;
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(129, 21);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(150, 20);
            this.startDatePicker.TabIndex = 12;
            this.startDatePicker.ValueChanged += new System.EventHandler(this.beginDatePicker_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "End Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Begin Date";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(155, 322);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 14;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(304, 322);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // FlightPlanImportAssistantForAIA
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(576, 357);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "FlightPlanImportAssistantForAIA";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Flight Plans Import Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlightPlanImportAssistantForAIA_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogElement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openFlightPlanFileDialogButton;
        private System.Windows.Forms.ComboBox flightPlanComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button openLocalAirportsFileDialogButton;
        private System.Windows.Forms.ComboBox localAirportsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button openMakeUpAllocationFileDialogButton;
        private System.Windows.Forms.ComboBox makeUpAllocationComboBox;
        private System.Windows.Forms.Label label2;
    }
}