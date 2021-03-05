namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    partial class IndividualLinksManagementAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndividualLinksManagementAssistant));
            this.availableScenariosComboBox = new System.Windows.Forms.ComboBox();
            this.availableLabel = new System.Windows.Forms.Label();
            this.linkButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tablesLabel = new System.Windows.Forms.Label();
            this.tablesComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.linksGroupBox = new System.Windows.Forms.GroupBox();
            this.linksGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // availableScenariosComboBox
            // 
            this.availableScenariosComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableScenariosComboBox.FormattingEnabled = true;
            this.availableScenariosComboBox.Location = new System.Drawing.Point(335, 41);
            this.availableScenariosComboBox.Name = "availableScenariosComboBox";
            this.availableScenariosComboBox.Size = new System.Drawing.Size(300, 21);
            this.availableScenariosComboBox.TabIndex = 1;
            // 
            // availableLabel
            // 
            this.availableLabel.AutoSize = true;
            this.availableLabel.BackColor = System.Drawing.Color.Transparent;
            this.availableLabel.Location = new System.Drawing.Point(332, 25);
            this.availableLabel.Name = "availableLabel";
            this.availableLabel.Size = new System.Drawing.Size(104, 13);
            this.availableLabel.TabIndex = 2;
            this.availableLabel.Text = "Available scenarios :";
            // 
            // linkButton
            // 
            this.linkButton.Location = new System.Drawing.Point(652, 39);
            this.linkButton.Name = "linkButton";
            this.linkButton.Size = new System.Drawing.Size(47, 23);
            this.linkButton.TabIndex = 3;
            this.linkButton.Text = "&Link";
            this.linkButton.UseVisualStyleBackColor = true;
            this.linkButton.Click += new System.EventHandler(this.linkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(447, 84);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // tablesLabel
            // 
            this.tablesLabel.AutoSize = true;
            this.tablesLabel.BackColor = System.Drawing.Color.Transparent;
            this.tablesLabel.Location = new System.Drawing.Point(6, 25);
            this.tablesLabel.Name = "tablesLabel";
            this.tablesLabel.Size = new System.Drawing.Size(45, 13);
            this.tablesLabel.TabIndex = 5;
            this.tablesLabel.Text = "Tables :";
            // 
            // tablesComboBox
            // 
            this.tablesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tablesComboBox.FormattingEnabled = true;
            this.tablesComboBox.Location = new System.Drawing.Point(9, 41);
            this.tablesComboBox.Name = "tablesComboBox";
            this.tablesComboBox.Size = new System.Drawing.Size(300, 21);
            this.tablesComboBox.TabIndex = 6;
            this.tablesComboBox.SelectedIndexChanged += new System.EventHandler(this.tablesComboBox_SelectedIndexChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(162, 84);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // linksGroupBox
            // 
            this.linksGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.linksGroupBox.Controls.Add(this.tablesLabel);
            this.linksGroupBox.Controls.Add(this.okButton);
            this.linksGroupBox.Controls.Add(this.availableScenariosComboBox);
            this.linksGroupBox.Controls.Add(this.tablesComboBox);
            this.linksGroupBox.Controls.Add(this.availableLabel);
            this.linksGroupBox.Controls.Add(this.linkButton);
            this.linksGroupBox.Controls.Add(this.cancelButton);
            this.linksGroupBox.Location = new System.Drawing.Point(12, 13);
            this.linksGroupBox.Name = "linksGroupBox";
            this.linksGroupBox.Size = new System.Drawing.Size(712, 130);
            this.linksGroupBox.TabIndex = 8;
            this.linksGroupBox.TabStop = false;
            // 
            // IndividualLinksManagementAssistant
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(736, 170);
            this.Controls.Add(this.linksGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IndividualLinksManagementAssistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Links Manager";
            this.linksGroupBox.ResumeLayout(false);
            this.linksGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox availableScenariosComboBox;
        private System.Windows.Forms.Label availableLabel;
        private System.Windows.Forms.Button linkButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label tablesLabel;
        private System.Windows.Forms.ComboBox tablesComboBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox linksGroupBox;
    }
}