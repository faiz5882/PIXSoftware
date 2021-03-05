namespace SIMCORE_TOOL.Assistant
{
    partial class TargetAssistantBySummaryTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TargetAssistantBySummaryTable));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.objectObservedComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.targetValueTextBox = new System.Windows.Forms.TextBox();
            this.comparisonComboBox = new System.Windows.Forms.ComboBox();
            this.degreeComboBox = new System.Windows.Forms.ComboBox();
            this.kpiComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.objectObservedComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Process Observed";
            // 
            // objectObservedComboBox
            // 
            this.objectObservedComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectObservedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectObservedComboBox.FormattingEnabled = true;
            this.objectObservedComboBox.Location = new System.Drawing.Point(17, 30);
            this.objectObservedComboBox.Name = "objectObservedComboBox";
            this.objectObservedComboBox.Size = new System.Drawing.Size(298, 21);
            this.objectObservedComboBox.TabIndex = 0;
            this.objectObservedComboBox.SelectedIndexChanged += new System.EventHandler(this.processObservedComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.targetValueTextBox);
            this.groupBox2.Controls.Add(this.comparisonComboBox);
            this.groupBox2.Controls.Add(this.degreeComboBox);
            this.groupBox2.Controls.Add(this.kpiComboBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 148);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target Details";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(209, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Target Value :";
            // 
            // targetValueTextBox
            // 
            this.targetValueTextBox.Location = new System.Drawing.Point(289, 100);
            this.targetValueTextBox.Name = "targetValueTextBox";
            this.targetValueTextBox.Size = new System.Drawing.Size(60, 20);
            this.targetValueTextBox.TabIndex = 6;
            this.targetValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.targetValueTextbox_KeyPress);
            // 
            // comparisonComboBox
            // 
            this.comparisonComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comparisonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comparisonComboBox.FormattingEnabled = true;
            this.comparisonComboBox.Location = new System.Drawing.Point(88, 100);
            this.comparisonComboBox.Name = "comparisonComboBox";
            this.comparisonComboBox.Size = new System.Drawing.Size(94, 21);
            this.comparisonComboBox.TabIndex = 5;
            // 
            // degreeComboBox
            // 
            this.degreeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.degreeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.degreeComboBox.FormattingEnabled = true;
            this.degreeComboBox.Location = new System.Drawing.Point(88, 64);
            this.degreeComboBox.Name = "degreeComboBox";
            this.degreeComboBox.Size = new System.Drawing.Size(227, 21);
            this.degreeComboBox.TabIndex = 4;
            // 
            // kpiComboBox
            // 
            this.kpiComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kpiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kpiComboBox.FormattingEnabled = true;
            this.kpiComboBox.Location = new System.Drawing.Point(88, 24);
            this.kpiComboBox.Name = "kpiComboBox";
            this.kpiComboBox.Size = new System.Drawing.Size(227, 21);
            this.kpiComboBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Comparison :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Degree :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "KPI :";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(76, 267);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(76, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(237, 267);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(76, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // TargetAssistantBySummaryTable
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(398, 301);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TargetAssistantBySummaryTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Target Parameters";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox objectObservedComboBox;
        private System.Windows.Forms.ComboBox comparisonComboBox;
        private System.Windows.Forms.ComboBox degreeComboBox;
        private System.Windows.Forms.ComboBox kpiComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox targetValueTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label4;
    }
}