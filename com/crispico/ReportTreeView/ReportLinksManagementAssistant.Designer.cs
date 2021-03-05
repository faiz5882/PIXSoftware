namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    partial class ReportLinksManagementAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportLinksManagementAssistant));
            this.availableScenariosLabel = new System.Windows.Forms.Label();
            this.availableScenariosComboBox = new System.Windows.Forms.ComboBox();
            this.linkButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tablesPanel = new System.Windows.Forms.Panel();
            this.manageAllTablesCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filtersPanel = new System.Windows.Forms.Panel();
            this.manageAllFiltersCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chartsPanel = new System.Windows.Forms.Panel();
            this.manageAllChartsCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.paragraphsPanel = new System.Windows.Forms.Panel();
            this.manageAllNotesCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tablesPanel.SuspendLayout();
            this.filtersPanel.SuspendLayout();
            this.chartsPanel.SuspendLayout();
            this.paragraphsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // availableScenariosLabel
            // 
            this.availableScenariosLabel.AutoSize = true;
            this.availableScenariosLabel.BackColor = System.Drawing.Color.Transparent;
            this.availableScenariosLabel.Location = new System.Drawing.Point(17, 15);
            this.availableScenariosLabel.Name = "availableScenariosLabel";
            this.availableScenariosLabel.Size = new System.Drawing.Size(101, 13);
            this.availableScenariosLabel.TabIndex = 2;
            this.availableScenariosLabel.Text = "Available scenarios:";
            // 
            // availableScenariosComboBox
            // 
            this.availableScenariosComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableScenariosComboBox.FormattingEnabled = true;
            this.availableScenariosComboBox.Location = new System.Drawing.Point(144, 12);
            this.availableScenariosComboBox.Name = "availableScenariosComboBox";
            this.availableScenariosComboBox.Size = new System.Drawing.Size(226, 21);
            this.availableScenariosComboBox.TabIndex = 3;
            // 
            // linkButton
            // 
            this.linkButton.Location = new System.Drawing.Point(411, 10);
            this.linkButton.Name = "linkButton";
            this.linkButton.Size = new System.Drawing.Size(91, 23);
            this.linkButton.TabIndex = 4;
            this.linkButton.Text = "&Link";
            this.linkButton.UseVisualStyleBackColor = true;
            this.linkButton.Click += new System.EventHandler(this.linkButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(249, 533);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(662, 533);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // tablesPanel
            // 
            this.tablesPanel.AutoScroll = true;
            this.tablesPanel.BackColor = System.Drawing.Color.Transparent;
            this.tablesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tablesPanel.Controls.Add(this.manageAllTablesCheckBox);
            this.tablesPanel.Controls.Add(this.label1);
            this.tablesPanel.Location = new System.Drawing.Point(12, 41);
            this.tablesPanel.Name = "tablesPanel";
            this.tablesPanel.Size = new System.Drawing.Size(490, 240);
            this.tablesPanel.TabIndex = 7;
            // 
            // manageAllTablesCheckBox
            // 
            this.manageAllTablesCheckBox.AutoSize = true;
            this.manageAllTablesCheckBox.Location = new System.Drawing.Point(340, 8);
            this.manageAllTablesCheckBox.Name = "manageAllTablesCheckBox";
            this.manageAllTablesCheckBox.Size = new System.Drawing.Size(123, 17);
            this.manageAllTablesCheckBox.TabIndex = 1;
            this.manageAllTablesCheckBox.Text = "Select / Deselect All";
            this.manageAllTablesCheckBox.UseVisualStyleBackColor = true;
            this.manageAllTablesCheckBox.CheckedChanged += new System.EventHandler(this.manageAllCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tables";
            // 
            // filtersPanel
            // 
            this.filtersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filtersPanel.AutoScroll = true;
            this.filtersPanel.BackColor = System.Drawing.Color.Transparent;
            this.filtersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filtersPanel.Controls.Add(this.manageAllFiltersCheckBox);
            this.filtersPanel.Controls.Add(this.label2);
            this.filtersPanel.Location = new System.Drawing.Point(12, 287);
            this.filtersPanel.Name = "filtersPanel";
            this.filtersPanel.Size = new System.Drawing.Size(490, 240);
            this.filtersPanel.TabIndex = 8;
            // 
            // manageAllFiltersCheckBox
            // 
            this.manageAllFiltersCheckBox.AutoSize = true;
            this.manageAllFiltersCheckBox.Location = new System.Drawing.Point(340, 11);
            this.manageAllFiltersCheckBox.Name = "manageAllFiltersCheckBox";
            this.manageAllFiltersCheckBox.Size = new System.Drawing.Size(123, 17);
            this.manageAllFiltersCheckBox.TabIndex = 2;
            this.manageAllFiltersCheckBox.Text = "Select / Deselect All";
            this.manageAllFiltersCheckBox.UseVisualStyleBackColor = true;
            this.manageAllFiltersCheckBox.CheckedChanged += new System.EventHandler(this.manageAllCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filters";
            // 
            // chartsPanel
            // 
            this.chartsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chartsPanel.AutoScroll = true;
            this.chartsPanel.BackColor = System.Drawing.Color.Transparent;
            this.chartsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartsPanel.Controls.Add(this.manageAllChartsCheckBox);
            this.chartsPanel.Controls.Add(this.label3);
            this.chartsPanel.Location = new System.Drawing.Point(511, 41);
            this.chartsPanel.Name = "chartsPanel";
            this.chartsPanel.Size = new System.Drawing.Size(490, 240);
            this.chartsPanel.TabIndex = 9;
            // 
            // manageAllChartsCheckBox
            // 
            this.manageAllChartsCheckBox.AutoSize = true;
            this.manageAllChartsCheckBox.Location = new System.Drawing.Point(336, 11);
            this.manageAllChartsCheckBox.Name = "manageAllChartsCheckBox";
            this.manageAllChartsCheckBox.Size = new System.Drawing.Size(123, 17);
            this.manageAllChartsCheckBox.TabIndex = 2;
            this.manageAllChartsCheckBox.Text = "Select / Deselect All";
            this.manageAllChartsCheckBox.UseVisualStyleBackColor = true;
            this.manageAllChartsCheckBox.CheckedChanged += new System.EventHandler(this.manageAllCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Charts";
            // 
            // paragraphsPanel
            // 
            this.paragraphsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.paragraphsPanel.AutoScroll = true;
            this.paragraphsPanel.BackColor = System.Drawing.Color.Transparent;
            this.paragraphsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paragraphsPanel.Controls.Add(this.manageAllNotesCheckBox);
            this.paragraphsPanel.Controls.Add(this.label4);
            this.paragraphsPanel.Location = new System.Drawing.Point(511, 287);
            this.paragraphsPanel.Name = "paragraphsPanel";
            this.paragraphsPanel.Size = new System.Drawing.Size(490, 240);
            this.paragraphsPanel.TabIndex = 10;
            // 
            // manageAllNotesCheckBox
            // 
            this.manageAllNotesCheckBox.AutoSize = true;
            this.manageAllNotesCheckBox.Location = new System.Drawing.Point(336, 8);
            this.manageAllNotesCheckBox.Name = "manageAllNotesCheckBox";
            this.manageAllNotesCheckBox.Size = new System.Drawing.Size(123, 17);
            this.manageAllNotesCheckBox.TabIndex = 2;
            this.manageAllNotesCheckBox.Text = "Select / Deselect All";
            this.manageAllNotesCheckBox.UseVisualStyleBackColor = true;
            this.manageAllNotesCheckBox.CheckedChanged += new System.EventHandler(this.manageAllCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Notes";
            // 
            // ReportLinksManagementAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 566);
            this.Controls.Add(this.paragraphsPanel);
            this.Controls.Add(this.chartsPanel);
            this.Controls.Add(this.filtersPanel);
            this.Controls.Add(this.tablesPanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.linkButton);
            this.Controls.Add(this.availableScenariosComboBox);
            this.Controls.Add(this.availableScenariosLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportLinksManagementAssistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Links Manager";
            this.tablesPanel.ResumeLayout(false);
            this.tablesPanel.PerformLayout();
            this.filtersPanel.ResumeLayout(false);
            this.filtersPanel.PerformLayout();
            this.chartsPanel.ResumeLayout(false);
            this.chartsPanel.PerformLayout();
            this.paragraphsPanel.ResumeLayout(false);
            this.paragraphsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label availableScenariosLabel;
        private System.Windows.Forms.ComboBox availableScenariosComboBox;
        private System.Windows.Forms.Button linkButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel tablesPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel filtersPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel chartsPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel paragraphsPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox manageAllTablesCheckBox;
        private System.Windows.Forms.CheckBox manageAllFiltersCheckBox;
        private System.Windows.Forms.CheckBox manageAllChartsCheckBox;
        private System.Windows.Forms.CheckBox manageAllNotesCheckBox;
    }
}