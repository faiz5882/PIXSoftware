namespace SIMCORE_TOOL.com.crispico.CountingFunctionality
{
    partial class CountingAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountingAssistant));
            this.label1 = new System.Windows.Forms.Label();
            this.attributeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.attributeComboBox = new System.Windows.Forms.ComboBox();
            this.countingReferenceGroupBox = new System.Windows.Forms.GroupBox();
            this.intervalRadioButton = new System.Windows.Forms.RadioButton();
            this.valueRadioButton = new System.Windows.Forms.RadioButton();
            this.intervalPanel = new System.Windows.Forms.Panel();
            this.lastIntervalValueDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.firstIintervalValueDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.includingLastValueCheckBox = new System.Windows.Forms.CheckBox();
            this.includingFirstValueCheckBox = new System.Windows.Forms.CheckBox();
            this.lastValueTextBox = new System.Windows.Forms.TextBox();
            this.firstValueTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.referencePanel = new System.Windows.Forms.Panel();
            this.referenceValueDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.booleanReferenceValueComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.referenceValueTextBox = new System.Windows.Forms.TextBox();
            this.comparisonTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.countingReferenceGroupBox.SuspendLayout();
            this.intervalPanel.SuspendLayout();
            this.referencePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type of attribute :";
            // 
            // attributeTypeComboBox
            // 
            this.attributeTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.attributeTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attributeTypeComboBox.FormattingEnabled = true;
            this.attributeTypeComboBox.Location = new System.Drawing.Point(108, 21);
            this.attributeTypeComboBox.Name = "attributeTypeComboBox";
            this.attributeTypeComboBox.Size = new System.Drawing.Size(211, 21);
            this.attributeTypeComboBox.TabIndex = 1;
            this.attributeTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.attributeTypeComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Attribute :";
            // 
            // attributeComboBox
            // 
            this.attributeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.attributeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attributeComboBox.FormattingEnabled = true;
            this.attributeComboBox.Location = new System.Drawing.Point(108, 55);
            this.attributeComboBox.Name = "attributeComboBox";
            this.attributeComboBox.Size = new System.Drawing.Size(211, 21);
            this.attributeComboBox.TabIndex = 4;
            this.attributeComboBox.SelectedIndexChanged += new System.EventHandler(this.attributeComboBox_SelectedIndexChanged);
            // 
            // countingReferenceGroupBox
            // 
            this.countingReferenceGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.countingReferenceGroupBox.Controls.Add(this.intervalRadioButton);
            this.countingReferenceGroupBox.Controls.Add(this.valueRadioButton);
            this.countingReferenceGroupBox.Location = new System.Drawing.Point(15, 92);
            this.countingReferenceGroupBox.Name = "countingReferenceGroupBox";
            this.countingReferenceGroupBox.Size = new System.Drawing.Size(303, 45);
            this.countingReferenceGroupBox.TabIndex = 5;
            this.countingReferenceGroupBox.TabStop = false;
            this.countingReferenceGroupBox.Text = "Counting Reference";
            // 
            // intervalRadioButton
            // 
            this.intervalRadioButton.AutoSize = true;
            this.intervalRadioButton.Location = new System.Drawing.Point(64, 19);
            this.intervalRadioButton.Name = "intervalRadioButton";
            this.intervalRadioButton.Size = new System.Drawing.Size(60, 17);
            this.intervalRadioButton.TabIndex = 1;
            this.intervalRadioButton.Text = "Interval";
            this.intervalRadioButton.UseVisualStyleBackColor = true;
            this.intervalRadioButton.CheckedChanged += new System.EventHandler(this.intervalRadioButton_CheckedChanged);
            // 
            // valueRadioButton
            // 
            this.valueRadioButton.AutoSize = true;
            this.valueRadioButton.Checked = true;
            this.valueRadioButton.Location = new System.Drawing.Point(6, 19);
            this.valueRadioButton.Name = "valueRadioButton";
            this.valueRadioButton.Size = new System.Drawing.Size(52, 17);
            this.valueRadioButton.TabIndex = 0;
            this.valueRadioButton.TabStop = true;
            this.valueRadioButton.Text = "Value";
            this.valueRadioButton.UseVisualStyleBackColor = true;
            this.valueRadioButton.CheckedChanged += new System.EventHandler(this.valueRadioButton_CheckedChanged);
            // 
            // intervalPanel
            // 
            this.intervalPanel.BackColor = System.Drawing.Color.Transparent;
            this.intervalPanel.Controls.Add(this.lastIntervalValueDateTimePicker);
            this.intervalPanel.Controls.Add(this.firstIintervalValueDateTimePicker);
            this.intervalPanel.Controls.Add(this.includingLastValueCheckBox);
            this.intervalPanel.Controls.Add(this.includingFirstValueCheckBox);
            this.intervalPanel.Controls.Add(this.lastValueTextBox);
            this.intervalPanel.Controls.Add(this.firstValueTextBox);
            this.intervalPanel.Controls.Add(this.label4);
            this.intervalPanel.Controls.Add(this.label3);
            this.intervalPanel.Location = new System.Drawing.Point(15, 146);
            this.intervalPanel.Name = "intervalPanel";
            this.intervalPanel.Size = new System.Drawing.Size(303, 84);
            this.intervalPanel.TabIndex = 6;
            // 
            // lastIntervalValueDateTimePicker
            // 
            this.lastIntervalValueDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.lastIntervalValueDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.lastIntervalValueDateTimePicker.Location = new System.Drawing.Point(81, 48);
            this.lastIntervalValueDateTimePicker.Name = "lastIntervalValueDateTimePicker";
            this.lastIntervalValueDateTimePicker.Size = new System.Drawing.Size(143, 20);
            this.lastIntervalValueDateTimePicker.TabIndex = 7;
            // 
            // firstIintervalValueDateTimePicker
            // 
            this.firstIintervalValueDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.firstIintervalValueDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.firstIintervalValueDateTimePicker.Location = new System.Drawing.Point(81, 14);
            this.firstIintervalValueDateTimePicker.Name = "firstIintervalValueDateTimePicker";
            this.firstIintervalValueDateTimePicker.Size = new System.Drawing.Size(143, 20);
            this.firstIintervalValueDateTimePicker.TabIndex = 6;
            // 
            // includingLastValueCheckBox
            // 
            this.includingLastValueCheckBox.AutoSize = true;
            this.includingLastValueCheckBox.Checked = true;
            this.includingLastValueCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includingLastValueCheckBox.Location = new System.Drawing.Point(224, 50);
            this.includingLastValueCheckBox.Name = "includingLastValueCheckBox";
            this.includingLastValueCheckBox.Size = new System.Drawing.Size(69, 17);
            this.includingLastValueCheckBox.TabIndex = 5;
            this.includingLastValueCheckBox.Text = "Including";
            this.includingLastValueCheckBox.UseVisualStyleBackColor = true;
            // 
            // includingFirstValueCheckBox
            // 
            this.includingFirstValueCheckBox.AutoSize = true;
            this.includingFirstValueCheckBox.Checked = true;
            this.includingFirstValueCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includingFirstValueCheckBox.Location = new System.Drawing.Point(224, 16);
            this.includingFirstValueCheckBox.Name = "includingFirstValueCheckBox";
            this.includingFirstValueCheckBox.Size = new System.Drawing.Size(69, 17);
            this.includingFirstValueCheckBox.TabIndex = 4;
            this.includingFirstValueCheckBox.Text = "Including";
            this.includingFirstValueCheckBox.UseVisualStyleBackColor = true;
            // 
            // lastValueTextBox
            // 
            this.lastValueTextBox.Location = new System.Drawing.Point(81, 48);
            this.lastValueTextBox.Name = "lastValueTextBox";
            this.lastValueTextBox.Size = new System.Drawing.Size(100, 20);
            this.lastValueTextBox.TabIndex = 3;
            this.lastValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // firstValueTextBox
            // 
            this.firstValueTextBox.Location = new System.Drawing.Point(81, 14);
            this.firstValueTextBox.Name = "firstValueTextBox";
            this.firstValueTextBox.Size = new System.Drawing.Size(100, 20);
            this.firstValueTextBox.TabIndex = 2;
            this.firstValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Last value :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "First value:";
            // 
            // referencePanel
            // 
            this.referencePanel.BackColor = System.Drawing.Color.Transparent;
            this.referencePanel.Controls.Add(this.referenceValueDateTimePicker);
            this.referencePanel.Controls.Add(this.booleanReferenceValueComboBox);
            this.referencePanel.Controls.Add(this.label5);
            this.referencePanel.Controls.Add(this.referenceValueTextBox);
            this.referencePanel.Controls.Add(this.comparisonTypeComboBox);
            this.referencePanel.Controls.Add(this.label6);
            this.referencePanel.Location = new System.Drawing.Point(15, 146);
            this.referencePanel.Name = "referencePanel";
            this.referencePanel.Size = new System.Drawing.Size(303, 84);
            this.referencePanel.TabIndex = 6;
            // 
            // referenceValueDateTimePicker
            // 
            this.referenceValueDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.referenceValueDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.referenceValueDateTimePicker.Location = new System.Drawing.Point(112, 48);
            this.referenceValueDateTimePicker.Name = "referenceValueDateTimePicker";
            this.referenceValueDateTimePicker.Size = new System.Drawing.Size(143, 20);
            this.referenceValueDateTimePicker.TabIndex = 5;
            // 
            // booleanReferenceValueComboBox
            // 
            this.booleanReferenceValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.booleanReferenceValueComboBox.FormattingEnabled = true;
            this.booleanReferenceValueComboBox.Location = new System.Drawing.Point(113, 48);
            this.booleanReferenceValueComboBox.Name = "booleanReferenceValueComboBox";
            this.booleanReferenceValueComboBox.Size = new System.Drawing.Size(68, 21);
            this.booleanReferenceValueComboBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Comparison type :";
            // 
            // referenceValueTextBox
            // 
            this.referenceValueTextBox.Location = new System.Drawing.Point(113, 48);
            this.referenceValueTextBox.Name = "referenceValueTextBox";
            this.referenceValueTextBox.Size = new System.Drawing.Size(68, 20);
            this.referenceValueTextBox.TabIndex = 3;
            this.referenceValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // comparisonTypeComboBox
            // 
            this.comparisonTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comparisonTypeComboBox.FormattingEnabled = true;
            this.comparisonTypeComboBox.Location = new System.Drawing.Point(113, 19);
            this.comparisonTypeComboBox.Name = "comparisonTypeComboBox";
            this.comparisonTypeComboBox.Size = new System.Drawing.Size(68, 21);
            this.comparisonTypeComboBox.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Reference value :";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(47, 241);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(210, 241);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // CountingAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 276);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.referencePanel);
            this.Controls.Add(this.intervalPanel);
            this.Controls.Add(this.countingReferenceGroupBox);
            this.Controls.Add(this.attributeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.attributeTypeComboBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CountingAssistant";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Counting Parameters";
            this.countingReferenceGroupBox.ResumeLayout(false);
            this.countingReferenceGroupBox.PerformLayout();
            this.intervalPanel.ResumeLayout(false);
            this.intervalPanel.PerformLayout();
            this.referencePanel.ResumeLayout(false);
            this.referencePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox attributeTypeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox attributeComboBox;
        private System.Windows.Forms.GroupBox countingReferenceGroupBox;
        private System.Windows.Forms.RadioButton intervalRadioButton;
        private System.Windows.Forms.RadioButton valueRadioButton;
        private System.Windows.Forms.Panel intervalPanel;
        private System.Windows.Forms.CheckBox includingLastValueCheckBox;
        private System.Windows.Forms.CheckBox includingFirstValueCheckBox;
        private System.Windows.Forms.TextBox lastValueTextBox;
        private System.Windows.Forms.TextBox firstValueTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel referencePanel;
        private System.Windows.Forms.TextBox referenceValueTextBox;
        private System.Windows.Forms.ComboBox comparisonTypeComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox booleanReferenceValueComboBox;
        private System.Windows.Forms.DateTimePicker referenceValueDateTimePicker;
        private System.Windows.Forms.DateTimePicker lastIntervalValueDateTimePicker;
        private System.Windows.Forms.DateTimePicker firstIintervalValueDateTimePicker;
    }
}