namespace SIMCORE_TOOL.com.crispico.general_allocation.assistant
{
    partial class FlightGroupingRulesAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightGroupingRulesAssistant));
            this.ruleConditionsPanel = new System.Windows.Forms.Panel();
            this.addP2sAttributeButton = new System.Windows.Forms.Button();
            this.p2sAttributesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.previewTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ruleNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ruleDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ruleConditionsPanel
            // 
            this.ruleConditionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ruleConditionsPanel.AutoScroll = true;
            this.ruleConditionsPanel.BackColor = System.Drawing.Color.Transparent;
            this.ruleConditionsPanel.Location = new System.Drawing.Point(3, 107);
            this.ruleConditionsPanel.Name = "ruleConditionsPanel";
            this.ruleConditionsPanel.Size = new System.Drawing.Size(523, 256);
            this.ruleConditionsPanel.TabIndex = 0;
            // 
            // addP2sAttributeButton
            // 
            this.addP2sAttributeButton.Location = new System.Drawing.Point(300, 73);
            this.addP2sAttributeButton.Name = "addP2sAttributeButton";
            this.addP2sAttributeButton.Size = new System.Drawing.Size(75, 23);
            this.addP2sAttributeButton.TabIndex = 1;
            this.addP2sAttributeButton.Text = "Add";
            this.addP2sAttributeButton.UseVisualStyleBackColor = true;
            this.addP2sAttributeButton.Click += new System.EventHandler(this.addP2sAttributeButton_Click);
            // 
            // p2sAttributesComboBox
            // 
            this.p2sAttributesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.p2sAttributesComboBox.FormattingEnabled = true;
            this.p2sAttributesComboBox.Location = new System.Drawing.Point(148, 75);
            this.p2sAttributesComboBox.Name = "p2sAttributesComboBox";
            this.p2sAttributesComboBox.Size = new System.Drawing.Size(121, 21);
            this.p2sAttributesComboBox.TabIndex = 2;
            this.p2sAttributesComboBox.SelectedIndexChanged += new System.EventHandler(this.p2sAttributesComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "P2S Flight Plan Attribute:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(115, 381);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(373, 381);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // previewTextBox
            // 
            this.previewTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewTextBox.Location = new System.Drawing.Point(532, 107);
            this.previewTextBox.Multiline = true;
            this.previewTextBox.Name = "previewTextBox";
            this.previewTextBox.ReadOnly = true;
            this.previewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.previewTextBox.Size = new System.Drawing.Size(193, 256);
            this.previewTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(545, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "P2S Flight Plan Attribute Preview";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Rule name";
            // 
            // ruleNameTextBox
            // 
            this.ruleNameTextBox.Location = new System.Drawing.Point(148, 17);
            this.ruleNameTextBox.Name = "ruleNameTextBox";
            this.ruleNameTextBox.Size = new System.Drawing.Size(227, 20);
            this.ruleNameTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Rule description";
            // 
            // ruleDescriptionTextBox
            // 
            this.ruleDescriptionTextBox.Location = new System.Drawing.Point(148, 46);
            this.ruleDescriptionTextBox.Name = "ruleDescriptionTextBox";
            this.ruleDescriptionTextBox.Size = new System.Drawing.Size(378, 20);
            this.ruleDescriptionTextBox.TabIndex = 11;
            // 
            // FlightGroupingRulesAssistant
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(737, 425);
            this.Controls.Add(this.ruleDescriptionTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ruleNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.previewTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.p2sAttributesComboBox);
            this.Controls.Add(this.addP2sAttributeButton);
            this.Controls.Add(this.ruleConditionsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlightGroupingRulesAssistant";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit the flight grouping rule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ruleConditionsPanel;
        private System.Windows.Forms.Button addP2sAttributeButton;
        private System.Windows.Forms.ComboBox p2sAttributesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox previewTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ruleNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ruleDescriptionTextBox;
    }
}