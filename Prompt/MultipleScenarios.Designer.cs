namespace SIMCORE_TOOL.Prompt
{
    partial class MultipleScenarios
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleScenarios));
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.lbl_prompt = new System.Windows.Forms.Label();
            this.lst_Scenarios = new System.Windows.Forms.ListBox();
            this.gb_BagtraceFile = new System.Windows.Forms.GroupBox();
            this.tb_File = new System.Windows.Forms.TextBox();
            this.btn_Open = new System.Windows.Forms.Button();
            this.ofd_Open = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Folder = new System.Windows.Forms.TextBox();
            this.btn_LocateFolder = new System.Windows.Forms.Button();
            this.cb_DisplayModel_General = new System.Windows.Forms.CheckBox();
            this.cb_DisplayModel = new System.Windows.Forms.CheckBox();
            this.gb_Scenario = new System.Windows.Forms.GroupBox();
            this.cb_Simulate = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gb_BagtraceFile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gb_Scenario.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(248, 516);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(104, 30);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(37, 516);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(104, 30);
            this.btn_Ok.TabIndex = 2;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // lbl_prompt
            // 
            this.lbl_prompt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_prompt.BackColor = System.Drawing.Color.Transparent;
            this.lbl_prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_prompt.Location = new System.Drawing.Point(0, 0);
            this.lbl_prompt.MinimumSize = new System.Drawing.Size(393, 56);
            this.lbl_prompt.Name = "lbl_prompt";
            this.lbl_prompt.Size = new System.Drawing.Size(394, 56);
            this.lbl_prompt.TabIndex = 3;
            this.lbl_prompt.Text = "Choose the different scenarios";
            this.lbl_prompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lst_Scenarios
            // 
            this.lst_Scenarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_Scenarios.FormattingEnabled = true;
            this.lst_Scenarios.Location = new System.Drawing.Point(27, 57);
            this.lst_Scenarios.Name = "lst_Scenarios";
            this.lst_Scenarios.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_Scenarios.Size = new System.Drawing.Size(340, 212);
            this.lst_Scenarios.TabIndex = 4;
            this.lst_Scenarios.SelectedIndexChanged += new System.EventHandler(this.lst_Scenarios_SelectedIndexChanged);
            // 
            // gb_BagtraceFile
            // 
            this.gb_BagtraceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_BagtraceFile.BackColor = System.Drawing.Color.Transparent;
            this.gb_BagtraceFile.Controls.Add(this.tb_File);
            this.gb_BagtraceFile.Controls.Add(this.btn_Open);
            this.gb_BagtraceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_BagtraceFile.Location = new System.Drawing.Point(27, 388);
            this.gb_BagtraceFile.Name = "gb_BagtraceFile";
            this.gb_BagtraceFile.Size = new System.Drawing.Size(340, 58);
            this.gb_BagtraceFile.TabIndex = 9;
            this.gb_BagtraceFile.TabStop = false;
            this.gb_BagtraceFile.Text = "Simulation project";
            // 
            // tb_File
            // 
            this.tb_File.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_File.Location = new System.Drawing.Point(6, 21);
            this.tb_File.Name = "tb_File";
            this.tb_File.Size = new System.Drawing.Size(296, 22);
            this.tb_File.TabIndex = 7;
            // 
            // btn_Open
            // 
            this.btn_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Open.Location = new System.Drawing.Point(308, 21);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(26, 23);
            this.btn_Open.TabIndex = 3;
            this.btn_Open.Text = "...";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // ofd_Open
            // 
            this.ofd_Open.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txt_Folder);
            this.groupBox1.Controls.Add(this.btn_LocateFolder);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(27, 452);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 58);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulation folder";
            // 
            // txt_Folder
            // 
            this.txt_Folder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Folder.Location = new System.Drawing.Point(6, 21);
            this.txt_Folder.Name = "txt_Folder";
            this.txt_Folder.Size = new System.Drawing.Size(296, 22);
            this.txt_Folder.TabIndex = 7;
            // 
            // btn_LocateFolder
            // 
            this.btn_LocateFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LocateFolder.Location = new System.Drawing.Point(308, 21);
            this.btn_LocateFolder.Name = "btn_LocateFolder";
            this.btn_LocateFolder.Size = new System.Drawing.Size(26, 23);
            this.btn_LocateFolder.TabIndex = 3;
            this.btn_LocateFolder.Text = "...";
            this.btn_LocateFolder.UseVisualStyleBackColor = true;
            this.btn_LocateFolder.Click += new System.EventHandler(this.btn_LocateFolder_Click);
            // 
            // cb_DisplayModel_General
            // 
            this.cb_DisplayModel_General.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_DisplayModel_General.AutoSize = true;
            this.cb_DisplayModel_General.BackColor = System.Drawing.Color.Transparent;
            this.cb_DisplayModel_General.Checked = true;
            this.cb_DisplayModel_General.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_DisplayModel_General.Location = new System.Drawing.Point(35, 293);
            this.cb_DisplayModel_General.Name = "cb_DisplayModel_General";
            this.cb_DisplayModel_General.Size = new System.Drawing.Size(106, 17);
            this.cb_DisplayModel_General.TabIndex = 11;
            this.cb_DisplayModel_General.Text = "Display Model (s)";
            this.cb_DisplayModel_General.ThreeState = true;
            this.toolTip1.SetToolTip(this.cb_DisplayModel_General, "General parameter, will be used in priority. ");
            this.cb_DisplayModel_General.UseVisualStyleBackColor = false;
            // 
            // cb_DisplayModel
            // 
            this.cb_DisplayModel.AutoSize = true;
            this.cb_DisplayModel.Location = new System.Drawing.Point(12, 19);
            this.cb_DisplayModel.Name = "cb_DisplayModel";
            this.cb_DisplayModel.Size = new System.Drawing.Size(102, 17);
            this.cb_DisplayModel.TabIndex = 12;
            this.cb_DisplayModel.Text = "Display model(s)";
            this.cb_DisplayModel.UseVisualStyleBackColor = true;
            // 
            // gb_Scenario
            // 
            this.gb_Scenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Scenario.BackColor = System.Drawing.Color.Transparent;
            this.gb_Scenario.Controls.Add(this.cb_DisplayModel);
            this.gb_Scenario.Enabled = false;
            this.gb_Scenario.Location = new System.Drawing.Point(27, 327);
            this.gb_Scenario.Name = "gb_Scenario";
            this.gb_Scenario.Size = new System.Drawing.Size(340, 55);
            this.gb_Scenario.TabIndex = 13;
            this.gb_Scenario.TabStop = false;
            this.gb_Scenario.Text = "                                                ";
            // 
            // cb_Simulate
            // 
            this.cb_Simulate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_Simulate.AutoSize = true;
            this.cb_Simulate.BackColor = System.Drawing.Color.Transparent;
            this.cb_Simulate.Enabled = false;
            this.cb_Simulate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.cb_Simulate.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.cb_Simulate.Location = new System.Drawing.Point(38, 323);
            this.cb_Simulate.Name = "cb_Simulate";
            this.cb_Simulate.Size = new System.Drawing.Size(151, 20);
            this.cb_Simulate.TabIndex = 14;
            this.cb_Simulate.Text = "Simulate Scenario(s)";
            this.cb_Simulate.UseVisualStyleBackColor = false;
            this.cb_Simulate.CheckedChanged += new System.EventHandler(this.cb_Simulate_CheckedChanged);
            // 
            // MultipleScenarios
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(393, 567);
            this.Controls.Add(this.cb_Simulate);
            this.Controls.Add(this.gb_Scenario);
            this.Controls.Add(this.cb_DisplayModel_General);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_BagtraceFile);
            this.Controls.Add(this.lst_Scenarios);
            this.Controls.Add(this.lbl_prompt);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_Cancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 580);
            this.Name = "MultipleScenarios";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change scenario  name";
            this.gb_BagtraceFile.ResumeLayout(false);
            this.gb_BagtraceFile.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gb_Scenario.ResumeLayout(false);
            this.gb_Scenario.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label lbl_prompt;
        private System.Windows.Forms.ListBox lst_Scenarios;
        private System.Windows.Forms.GroupBox gb_BagtraceFile;
        private System.Windows.Forms.TextBox tb_File;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.OpenFileDialog ofd_Open;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Folder;
        private System.Windows.Forms.Button btn_LocateFolder;
        private System.Windows.Forms.CheckBox cb_DisplayModel_General;
        private System.Windows.Forms.CheckBox cb_DisplayModel;
        private System.Windows.Forms.GroupBox gb_Scenario;
        private System.Windows.Forms.CheckBox cb_Simulate;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}