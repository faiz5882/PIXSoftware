namespace SIMCORE_TOOL.com.crispico.text_gantt
{
    partial class TextGanttParametersEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextGanttParametersEditor));
            this.manualTimeIntervalGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.manualTimeIntervalCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.samplingStepComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.manualTimeIntervalGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // manualTimeIntervalGroupBox
            // 
            this.manualTimeIntervalGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.manualTimeIntervalGroupBox.Controls.Add(this.label2);
            this.manualTimeIntervalGroupBox.Controls.Add(this.label1);
            this.manualTimeIntervalGroupBox.Controls.Add(this.toDateTimePicker);
            this.manualTimeIntervalGroupBox.Controls.Add(this.fromDateTimePicker);
            this.manualTimeIntervalGroupBox.Location = new System.Drawing.Point(12, 12);
            this.manualTimeIntervalGroupBox.Name = "manualTimeIntervalGroupBox";
            this.manualTimeIntervalGroupBox.Size = new System.Drawing.Size(263, 91);
            this.manualTimeIntervalGroupBox.TabIndex = 0;
            this.manualTimeIntervalGroupBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From :";
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDateTimePicker.Location = new System.Drawing.Point(108, 57);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.Size = new System.Drawing.Size(132, 20);
            this.toDateTimePicker.TabIndex = 1;
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDateTimePicker.Location = new System.Drawing.Point(108, 31);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(132, 20);
            this.fromDateTimePicker.TabIndex = 0;
            // 
            // manualTimeIntervalCheckBox
            // 
            this.manualTimeIntervalCheckBox.AutoSize = true;
            this.manualTimeIntervalCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.manualTimeIntervalCheckBox.Location = new System.Drawing.Point(18, 11);
            this.manualTimeIntervalCheckBox.Name = "manualTimeIntervalCheckBox";
            this.manualTimeIntervalCheckBox.Size = new System.Drawing.Size(143, 17);
            this.manualTimeIntervalCheckBox.TabIndex = 4;
            this.manualTimeIntervalCheckBox.Text = "Use specific time interval";
            this.manualTimeIntervalCheckBox.UseVisualStyleBackColor = false;
            this.manualTimeIntervalCheckBox.CheckedChanged += new System.EventHandler(this.manualTimeIntervalCheckBox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.samplingStepComboBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 66);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // samplingStepComboBox
            // 
            this.samplingStepComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.samplingStepComboBox.FormattingEnabled = true;
            this.samplingStepComboBox.Location = new System.Drawing.Point(108, 23);
            this.samplingStepComboBox.Name = "samplingStepComboBox";
            this.samplingStepComboBox.Size = new System.Drawing.Size(63, 21);
            this.samplingStepComboBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sampling Step :";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(35, 180);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(176, 180);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // TextGanttParametersEditor
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(285, 214);
            this.Controls.Add(this.manualTimeIntervalCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.manualTimeIntervalGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextGanttParametersEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text Gantt Parameters";
            this.manualTimeIntervalGroupBox.ResumeLayout(false);
            this.manualTimeIntervalGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox manualTimeIntervalGroupBox;
        private System.Windows.Forms.CheckBox manualTimeIntervalCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker toDateTimePicker;
        private System.Windows.Forms.DateTimePicker fromDateTimePicker;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox samplingStepComboBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}