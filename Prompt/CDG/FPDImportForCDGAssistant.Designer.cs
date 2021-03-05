namespace SIMCORE_TOOL.Prompt.CDG
{
    partial class FPDImportForCDGAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPDImportForCDGAssistant));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.S3InterfaceFileComboBox = new System.Windows.Forms.ComboBox();
            this.S4ScenarioFileComboBox = new System.Windows.Forms.ComboBox();
            this.openS3InterfaceFileDialogButton = new System.Windows.Forms.Button();
            this.openS4ScenarioFileDialogButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.FPcomboBox = new System.Windows.Forms.ComboBox();
            this.openflightPlanButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.openflightPlanButton);
            this.groupBox1.Controls.Add(this.FPcomboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.openS4ScenarioFileDialogButton);
            this.groupBox1.Controls.Add(this.openS3InterfaceFileDialogButton);
            this.groupBox1.Controls.Add(this.S4ScenarioFileComboBox);
            this.groupBox1.Controls.Add(this.S3InterfaceFileComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(724, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Departure Flight Plan Files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "S3 (Interface) data file :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "S4(Scenario) data file :";
            // 
            // S3InterfaceFileComboBox
            // 
            this.S3InterfaceFileComboBox.FormattingEnabled = true;
            this.S3InterfaceFileComboBox.Location = new System.Drawing.Point(154, 34);
            this.S3InterfaceFileComboBox.Name = "S3InterfaceFileComboBox";
            this.S3InterfaceFileComboBox.Size = new System.Drawing.Size(494, 21);
            this.S3InterfaceFileComboBox.TabIndex = 2;
            this.S3InterfaceFileComboBox.Tag = "S3interface.txt";
            // 
            // S4ScenarioFileComboBox
            // 
            this.S4ScenarioFileComboBox.FormattingEnabled = true;
            this.S4ScenarioFileComboBox.Location = new System.Drawing.Point(154, 68);
            this.S4ScenarioFileComboBox.Name = "S4ScenarioFileComboBox";
            this.S4ScenarioFileComboBox.Size = new System.Drawing.Size(494, 21);
            this.S4ScenarioFileComboBox.TabIndex = 3;
            this.S4ScenarioFileComboBox.Tag = "S4Scenario.txt";
            // 
            // openS3InterfaceFileDialogButton
            // 
            this.openS3InterfaceFileDialogButton.Location = new System.Drawing.Point(664, 32);
            this.openS3InterfaceFileDialogButton.Name = "openS3InterfaceFileDialogButton";
            this.openS3InterfaceFileDialogButton.Size = new System.Drawing.Size(41, 23);
            this.openS3InterfaceFileDialogButton.TabIndex = 4;
            this.openS3InterfaceFileDialogButton.Text = "...";
            this.openS3InterfaceFileDialogButton.UseVisualStyleBackColor = true;
            this.openS3InterfaceFileDialogButton.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // openS4ScenarioFileDialogButton
            // 
            this.openS4ScenarioFileDialogButton.Location = new System.Drawing.Point(664, 66);
            this.openS4ScenarioFileDialogButton.Name = "openS4ScenarioFileDialogButton";
            this.openS4ScenarioFileDialogButton.Size = new System.Drawing.Size(41, 23);
            this.openS4ScenarioFileDialogButton.TabIndex = 5;
            this.openS4ScenarioFileDialogButton.Text = "...";
            this.openS4ScenarioFileDialogButton.UseVisualStyleBackColor = true;
            this.openS4ScenarioFileDialogButton.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(236, 170);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(459, 169);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "...";
            this.openFileDialog1.Filter = "All file | *.*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Flight Plan :";
            // 
            // FPcomboBox
            // 
            this.FPcomboBox.FormattingEnabled = true;
            this.FPcomboBox.Location = new System.Drawing.Point(154, 101);
            this.FPcomboBox.Name = "FPcomboBox";
            this.FPcomboBox.Size = new System.Drawing.Size(494, 21);
            this.FPcomboBox.TabIndex = 7;
            this.FPcomboBox.Tag = "FP.txt";
            // 
            // openflightPlanButton
            // 
            this.openflightPlanButton.Location = new System.Drawing.Point(664, 101);
            this.openflightPlanButton.Name = "openflightPlanButton";
            this.openflightPlanButton.Size = new System.Drawing.Size(41, 22);
            this.openflightPlanButton.TabIndex = 8;
            this.openflightPlanButton.Text = "...";
            this.openflightPlanButton.UseVisualStyleBackColor = true;
            this.openflightPlanButton.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // FPDImportForCDGAssistant
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(753, 204);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FPDImportForCDGAssistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FPDImportForCDGAssistant_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox S4ScenarioFileComboBox;
        private System.Windows.Forms.ComboBox S3InterfaceFileComboBox;
        private System.Windows.Forms.Button openS4ScenarioFileDialogButton;
        private System.Windows.Forms.Button openS3InterfaceFileDialogButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openflightPlanButton;
        private System.Windows.Forms.ComboBox FPcomboBox;
        private System.Windows.Forms.Label label3;
    }
}