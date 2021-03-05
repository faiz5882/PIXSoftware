namespace SIMCORE_TOOL.Assistant
{
    partial class SetTargetEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetTargetEditor));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comparisonTypeComboBox = new System.Windows.Forms.ComboBox();
            this.valueObservedTextBox = new System.Windows.Forms.TextBox();
            this.targetValueTextBox = new System.Windows.Forms.TextBox();
            this.statisticAttributeTextBox = new System.Windows.Forms.TextBox();
            this.attributeDegreeTextBox = new System.Windows.Forms.TextBox();
            this.statisticTypeTextBox = new System.Windows.Forms.TextBox();
            this.processObservedTextBox = new System.Windows.Forms.TextBox();
            this.scenarioNameTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.percSuccessTextBox = new System.Windows.Forms.TextBox();
            this.differenceTextBox = new System.Windows.Forms.TextBox();
            this.targetAchievedTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.targetRowComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.removeTargetRowBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.comparisonTypeComboBox);
            this.groupBox1.Controls.Add(this.valueObservedTextBox);
            this.groupBox1.Controls.Add(this.targetValueTextBox);
            this.groupBox1.Controls.Add(this.statisticAttributeTextBox);
            this.groupBox1.Controls.Add(this.attributeDegreeTextBox);
            this.groupBox1.Controls.Add(this.statisticTypeTextBox);
            this.groupBox1.Controls.Add(this.processObservedTextBox);
            this.groupBox1.Controls.Add(this.scenarioNameTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 235);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target information";
            // 
            // comparisonTypeComboBox
            // 
            this.comparisonTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comparisonTypeComboBox.FormattingEnabled = true;
            this.comparisonTypeComboBox.Location = new System.Drawing.Point(109, 156);
            this.comparisonTypeComboBox.Name = "comparisonTypeComboBox";
            this.comparisonTypeComboBox.Size = new System.Drawing.Size(58, 21);
            this.comparisonTypeComboBox.TabIndex = 6;
            this.comparisonTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.comparisonTypeComboBox_SelectedIndexChanged);
            // 
            // valueObservedTextBox
            // 
            this.valueObservedTextBox.Enabled = false;
            this.valueObservedTextBox.Location = new System.Drawing.Point(109, 208);
            this.valueObservedTextBox.Name = "valueObservedTextBox";
            this.valueObservedTextBox.Size = new System.Drawing.Size(58, 20);
            this.valueObservedTextBox.TabIndex = 15;
            // 
            // targetValueTextBox
            // 
            this.targetValueTextBox.Location = new System.Drawing.Point(109, 182);
            this.targetValueTextBox.Name = "targetValueTextBox";
            this.targetValueTextBox.Size = new System.Drawing.Size(58, 20);
            this.targetValueTextBox.TabIndex = 14;
            this.targetValueTextBox.Leave += new System.EventHandler(this.targetValueTextBox_Leave);
            this.targetValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.targetValueTextBox_KeyPress);
            // 
            // statisticAttributeTextBox
            // 
            this.statisticAttributeTextBox.Enabled = false;
            this.statisticAttributeTextBox.Location = new System.Drawing.Point(109, 104);
            this.statisticAttributeTextBox.Name = "statisticAttributeTextBox";
            this.statisticAttributeTextBox.Size = new System.Drawing.Size(119, 20);
            this.statisticAttributeTextBox.TabIndex = 12;
            // 
            // attributeDegreeTextBox
            // 
            this.attributeDegreeTextBox.Enabled = false;
            this.attributeDegreeTextBox.Location = new System.Drawing.Point(109, 130);
            this.attributeDegreeTextBox.Name = "attributeDegreeTextBox";
            this.attributeDegreeTextBox.Size = new System.Drawing.Size(119, 20);
            this.attributeDegreeTextBox.TabIndex = 11;
            // 
            // statisticTypeTextBox
            // 
            this.statisticTypeTextBox.Enabled = false;
            this.statisticTypeTextBox.Location = new System.Drawing.Point(109, 78);
            this.statisticTypeTextBox.Name = "statisticTypeTextBox";
            this.statisticTypeTextBox.Size = new System.Drawing.Size(119, 20);
            this.statisticTypeTextBox.TabIndex = 10;
            // 
            // processObservedTextBox
            // 
            this.processObservedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.processObservedTextBox.Enabled = false;
            this.processObservedTextBox.Location = new System.Drawing.Point(109, 52);
            this.processObservedTextBox.Name = "processObservedTextBox";
            this.processObservedTextBox.Size = new System.Drawing.Size(178, 20);
            this.processObservedTextBox.TabIndex = 9;
            // 
            // scenarioNameTextBox
            // 
            this.scenarioNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scenarioNameTextBox.Enabled = false;
            this.scenarioNameTextBox.Location = new System.Drawing.Point(109, 26);
            this.scenarioNameTextBox.Name = "scenarioNameTextBox";
            this.scenarioNameTextBox.Size = new System.Drawing.Size(178, 20);
            this.scenarioNameTextBox.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 211);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Value observed";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Target value";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Comparison type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Statistic attribute";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Attribute degree";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Statistic type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Process observed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scenario name";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.percSuccessTextBox);
            this.groupBox2.Controls.Add(this.differenceTextBox);
            this.groupBox2.Controls.Add(this.targetAchievedTextBox);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(336, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 235);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target results";
            // 
            // percSuccessTextBox
            // 
            this.percSuccessTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.percSuccessTextBox.Enabled = false;
            this.percSuccessTextBox.Location = new System.Drawing.Point(109, 78);
            this.percSuccessTextBox.Name = "percSuccessTextBox";
            this.percSuccessTextBox.Size = new System.Drawing.Size(50, 20);
            this.percSuccessTextBox.TabIndex = 5;
            // 
            // differenceTextBox
            // 
            this.differenceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.differenceTextBox.Enabled = false;
            this.differenceTextBox.Location = new System.Drawing.Point(109, 52);
            this.differenceTextBox.Name = "differenceTextBox";
            this.differenceTextBox.Size = new System.Drawing.Size(50, 20);
            this.differenceTextBox.TabIndex = 4;
            // 
            // targetAchievedTextBox
            // 
            this.targetAchievedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetAchievedTextBox.Enabled = false;
            this.targetAchievedTextBox.Location = new System.Drawing.Point(109, 26);
            this.targetAchievedTextBox.Name = "targetAchievedTextBox";
            this.targetAchievedTextBox.Size = new System.Drawing.Size(50, 20);
            this.targetAchievedTextBox.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "% Success";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Difference";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Target achieved";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(145, 300);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(352, 300);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // targetRowComboBox
            // 
            this.targetRowComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetRowComboBox.FormattingEnabled = true;
            this.targetRowComboBox.Location = new System.Drawing.Point(121, 19);
            this.targetRowComboBox.Name = "targetRowComboBox";
            this.targetRowComboBox.Size = new System.Drawing.Size(58, 21);
            this.targetRowComboBox.TabIndex = 4;
            this.targetRowComboBox.SelectedIndexChanged += new System.EventHandler(this.targetRowComboBox_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(18, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Select target row";
            // 
            // removeTargetRowBtn
            // 
            this.removeTargetRowBtn.Location = new System.Drawing.Point(196, 17);
            this.removeTargetRowBtn.Name = "removeTargetRowBtn";
            this.removeTargetRowBtn.Size = new System.Drawing.Size(146, 23);
            this.removeTargetRowBtn.TabIndex = 6;
            this.removeTargetRowBtn.Text = "Remove selected target";
            this.removeTargetRowBtn.UseVisualStyleBackColor = true;
            this.removeTargetRowBtn.Click += new System.EventHandler(this.removeTargetRowBtn_Click);
            // 
            // SetTargetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 334);
            this.Controls.Add(this.removeTargetRowBtn);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.targetRowComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetTargetEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Target Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox valueObservedTextBox;
        private System.Windows.Forms.TextBox targetValueTextBox;
        private System.Windows.Forms.TextBox statisticAttributeTextBox;
        private System.Windows.Forms.TextBox attributeDegreeTextBox;
        private System.Windows.Forms.TextBox statisticTypeTextBox;
        private System.Windows.Forms.TextBox processObservedTextBox;
        private System.Windows.Forms.TextBox scenarioNameTextBox;
        private System.Windows.Forms.TextBox percSuccessTextBox;
        private System.Windows.Forms.TextBox differenceTextBox;
        private System.Windows.Forms.TextBox targetAchievedTextBox;
        private System.Windows.Forms.ComboBox targetRowComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comparisonTypeComboBox;
        private System.Windows.Forms.Button removeTargetRowBtn;
    }
}