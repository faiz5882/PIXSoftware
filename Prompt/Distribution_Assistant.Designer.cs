namespace SIMCORE_TOOL.Prompt
{
    partial class Distribution_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Distribution_Assistant));
            this.lbl_Type = new System.Windows.Forms.Label();
            this.lbl_Parameters = new System.Windows.Forms.Label();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cb_DistributionType = new System.Windows.Forms.ComboBox();
            this.txt_FirstParam = new System.Windows.Forms.TextBox();
            this.txt_SecondParam = new System.Windows.Forms.TextBox();
            this.txt_ThirdParam = new System.Windows.Forms.TextBox();
            this.gb_Definition = new System.Windows.Forms.GroupBox();
            this.btn_CalcDistribution = new System.Windows.Forms.Button();
            this.rb_existingTable = new System.Windows.Forms.RadioButton();
            this.rb_FromFile = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Table = new System.Windows.Forms.Label();
            this.cb_Tables = new System.Windows.Forms.ComboBox();
            this.cb_Scenario = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_NameFile = new System.Windows.Forms.Label();
            this.btn_Open = new System.Windows.Forms.Button();
            this.cb_Column = new System.Windows.Forms.ComboBox();
            this.lbl_Column = new System.Windows.Forms.Label();
            this.gb_CalcDistribution = new System.Windows.Forms.GroupBox();
            this.btn_Analyse = new System.Windows.Forms.Button();
            this.ofd_OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.gb_Definition.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gb_CalcDistribution.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Type
            // 
            this.lbl_Type.AutoSize = true;
            this.lbl_Type.Location = new System.Drawing.Point(45, 19);
            this.lbl_Type.Name = "lbl_Type";
            this.lbl_Type.Size = new System.Drawing.Size(31, 13);
            this.lbl_Type.TabIndex = 1;
            this.lbl_Type.Text = "Type";
            // 
            // lbl_Parameters
            // 
            this.lbl_Parameters.AutoSize = true;
            this.lbl_Parameters.Location = new System.Drawing.Point(142, 19);
            this.lbl_Parameters.Name = "lbl_Parameters";
            this.lbl_Parameters.Size = new System.Drawing.Size(60, 13);
            this.lbl_Parameters.TabIndex = 2;
            this.lbl_Parameters.Text = "Parameters";
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(51, 370);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 10;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(288, 370);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 11;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // cb_DistributionType
            // 
            this.cb_DistributionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_DistributionType.FormattingEnabled = true;
            this.cb_DistributionType.Items.AddRange(new object[] {
            "Constant",
            "Uniform",
            "Normal",
            "Triangular",
            "Exponential",
            "Gamma",
            "LogNormal",
            "Weibull"});
            this.cb_DistributionType.Location = new System.Drawing.Point(15, 41);
            this.cb_DistributionType.Name = "cb_DistributionType";
            this.cb_DistributionType.Size = new System.Drawing.Size(100, 21);
            this.cb_DistributionType.TabIndex = 12;
            this.cb_DistributionType.SelectedIndexChanged += new System.EventHandler(this.cb_DistributionType_SelectedIndexChanged);
            // 
            // txt_FirstParam
            // 
            this.txt_FirstParam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_FirstParam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_FirstParam.Location = new System.Drawing.Point(150, 41);
            this.txt_FirstParam.Name = "txt_FirstParam";
            this.txt_FirstParam.Size = new System.Drawing.Size(41, 20);
            this.txt_FirstParam.TabIndex = 13;
            this.txt_FirstParam.EnabledChanged += new System.EventHandler(this.txt_FirstParam_EnabledChanged);
            this.txt_FirstParam.TextChanged += new System.EventHandler(this.txt_FirstParam_TextChanged);
            // 
            // txt_SecondParam
            // 
            this.txt_SecondParam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_SecondParam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_SecondParam.Location = new System.Drawing.Point(217, 41);
            this.txt_SecondParam.Name = "txt_SecondParam";
            this.txt_SecondParam.Size = new System.Drawing.Size(44, 20);
            this.txt_SecondParam.TabIndex = 14;
            this.txt_SecondParam.EnabledChanged += new System.EventHandler(this.txt_FirstParam_EnabledChanged);
            this.txt_SecondParam.TextChanged += new System.EventHandler(this.txt_FirstParam_TextChanged);
            // 
            // txt_ThirdParam
            // 
            this.txt_ThirdParam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_ThirdParam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_ThirdParam.Location = new System.Drawing.Point(285, 41);
            this.txt_ThirdParam.Name = "txt_ThirdParam";
            this.txt_ThirdParam.Size = new System.Drawing.Size(44, 20);
            this.txt_ThirdParam.TabIndex = 15;
            this.txt_ThirdParam.EnabledChanged += new System.EventHandler(this.txt_FirstParam_EnabledChanged);
            this.txt_ThirdParam.TextChanged += new System.EventHandler(this.txt_FirstParam_TextChanged);
            // 
            // gb_Definition
            // 
            this.gb_Definition.BackColor = System.Drawing.Color.Transparent;
            this.gb_Definition.Controls.Add(this.txt_ThirdParam);
            this.gb_Definition.Controls.Add(this.cb_DistributionType);
            this.gb_Definition.Controls.Add(this.lbl_Type);
            this.gb_Definition.Controls.Add(this.txt_FirstParam);
            this.gb_Definition.Controls.Add(this.lbl_Parameters);
            this.gb_Definition.Controls.Add(this.txt_SecondParam);
            this.gb_Definition.Location = new System.Drawing.Point(30, 11);
            this.gb_Definition.Name = "gb_Definition";
            this.gb_Definition.Size = new System.Drawing.Size(358, 76);
            this.gb_Definition.TabIndex = 16;
            this.gb_Definition.TabStop = false;
            this.gb_Definition.Text = "Definition";
            // 
            // btn_CalcDistribution
            // 
            this.btn_CalcDistribution.Location = new System.Drawing.Point(30, 93);
            this.btn_CalcDistribution.Name = "btn_CalcDistribution";
            this.btn_CalcDistribution.Size = new System.Drawing.Size(187, 23);
            this.btn_CalcDistribution.TabIndex = 17;
            this.btn_CalcDistribution.Text = "Calc distribution from values";
            this.btn_CalcDistribution.UseVisualStyleBackColor = true;
            this.btn_CalcDistribution.Click += new System.EventHandler(this.btn_CalcDistribution_Click);
            // 
            // rb_existingTable
            // 
            this.rb_existingTable.AutoSize = true;
            this.rb_existingTable.BackColor = System.Drawing.Color.Transparent;
            this.rb_existingTable.Location = new System.Drawing.Point(11, -2);
            this.rb_existingTable.Name = "rb_existingTable";
            this.rb_existingTable.Size = new System.Drawing.Size(135, 17);
            this.rb_existingTable.TabIndex = 18;
            this.rb_existingTable.TabStop = true;
            this.rb_existingTable.Text = "From the existing tables";
            this.rb_existingTable.UseVisualStyleBackColor = false;
            this.rb_existingTable.CheckedChanged += new System.EventHandler(this.rb_existingTable_CheckedChanged);
            // 
            // rb_FromFile
            // 
            this.rb_FromFile.AutoSize = true;
            this.rb_FromFile.BackColor = System.Drawing.Color.Transparent;
            this.rb_FromFile.Location = new System.Drawing.Point(11, -2);
            this.rb_FromFile.Name = "rb_FromFile";
            this.rb_FromFile.Size = new System.Drawing.Size(82, 17);
            this.rb_FromFile.TabIndex = 19;
            this.rb_FromFile.TabStop = true;
            this.rb_FromFile.Text = "From the file";
            this.rb_FromFile.UseVisualStyleBackColor = false;
            this.rb_FromFile.CheckedChanged += new System.EventHandler(this.rb_FromFile_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lbl_Table);
            this.groupBox1.Controls.Add(this.cb_Tables);
            this.groupBox1.Controls.Add(this.cb_Scenario);
            this.groupBox1.Controls.Add(this.rb_existingTable);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 53);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "                                             ";
            // 
            // lbl_Table
            // 
            this.lbl_Table.AutoSize = true;
            this.lbl_Table.Enabled = false;
            this.lbl_Table.Location = new System.Drawing.Point(127, 24);
            this.lbl_Table.Name = "lbl_Table";
            this.lbl_Table.Size = new System.Drawing.Size(34, 13);
            this.lbl_Table.TabIndex = 21;
            this.lbl_Table.Text = "Table";
            // 
            // cb_Tables
            // 
            this.cb_Tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Tables.Enabled = false;
            this.cb_Tables.FormattingEnabled = true;
            this.cb_Tables.Location = new System.Drawing.Point(167, 21);
            this.cb_Tables.Name = "cb_Tables";
            this.cb_Tables.Size = new System.Drawing.Size(169, 21);
            this.cb_Tables.TabIndex = 20;
            this.cb_Tables.SelectedIndexChanged += new System.EventHandler(this.cb_Tables_SelectedIndexChanged);
            // 
            // cb_Scenario
            // 
            this.cb_Scenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Scenario.Enabled = false;
            this.cb_Scenario.FormattingEnabled = true;
            this.cb_Scenario.Location = new System.Drawing.Point(11, 21);
            this.cb_Scenario.Name = "cb_Scenario";
            this.cb_Scenario.Size = new System.Drawing.Size(100, 21);
            this.cb_Scenario.TabIndex = 19;
            this.cb_Scenario.SelectedIndexChanged += new System.EventHandler(this.cb_Scenario_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lbl_NameFile);
            this.groupBox2.Controls.Add(this.btn_Open);
            this.groupBox2.Controls.Add(this.rb_FromFile);
            this.groupBox2.Location = new System.Drawing.Point(6, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 80);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "                           ";
            // 
            // lbl_NameFile
            // 
            this.lbl_NameFile.AutoSize = true;
            this.lbl_NameFile.Enabled = false;
            this.lbl_NameFile.Location = new System.Drawing.Point(19, 54);
            this.lbl_NameFile.Name = "lbl_NameFile";
            this.lbl_NameFile.Size = new System.Drawing.Size(0, 13);
            this.lbl_NameFile.TabIndex = 22;
            // 
            // btn_Open
            // 
            this.btn_Open.Enabled = false;
            this.btn_Open.Location = new System.Drawing.Point(15, 21);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(91, 24);
            this.btn_Open.TabIndex = 20;
            this.btn_Open.Text = "Open file";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // cb_Column
            // 
            this.cb_Column.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Column.Enabled = false;
            this.cb_Column.FormattingEnabled = true;
            this.cb_Column.Location = new System.Drawing.Point(111, 196);
            this.cb_Column.Name = "cb_Column";
            this.cb_Column.Size = new System.Drawing.Size(115, 21);
            this.cb_Column.TabIndex = 22;
            this.cb_Column.SelectedIndexChanged += new System.EventHandler(this.cb_Column_SelectedIndexChanged);
            // 
            // lbl_Column
            // 
            this.lbl_Column.AutoSize = true;
            this.lbl_Column.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Column.Enabled = false;
            this.lbl_Column.Location = new System.Drawing.Point(6, 199);
            this.lbl_Column.Name = "lbl_Column";
            this.lbl_Column.Size = new System.Drawing.Size(99, 13);
            this.lbl_Column.TabIndex = 23;
            this.lbl_Column.Text = "Column to analyse :";
            // 
            // gb_CalcDistribution
            // 
            this.gb_CalcDistribution.BackColor = System.Drawing.Color.Transparent;
            this.gb_CalcDistribution.Controls.Add(this.btn_Analyse);
            this.gb_CalcDistribution.Controls.Add(this.groupBox1);
            this.gb_CalcDistribution.Controls.Add(this.lbl_Column);
            this.gb_CalcDistribution.Controls.Add(this.groupBox2);
            this.gb_CalcDistribution.Controls.Add(this.cb_Column);
            this.gb_CalcDistribution.Location = new System.Drawing.Point(30, 122);
            this.gb_CalcDistribution.Name = "gb_CalcDistribution";
            this.gb_CalcDistribution.Size = new System.Drawing.Size(358, 231);
            this.gb_CalcDistribution.TabIndex = 24;
            this.gb_CalcDistribution.TabStop = false;
            this.gb_CalcDistribution.Text = "Calc distribution";
            // 
            // btn_Analyse
            // 
            this.btn_Analyse.Enabled = false;
            this.btn_Analyse.Location = new System.Drawing.Point(243, 191);
            this.btn_Analyse.Name = "btn_Analyse";
            this.btn_Analyse.Size = new System.Drawing.Size(90, 29);
            this.btn_Analyse.TabIndex = 24;
            this.btn_Analyse.Text = "Analyse";
            this.btn_Analyse.UseVisualStyleBackColor = true;
            this.btn_Analyse.Click += new System.EventHandler(this.btn_Analyse_Click);
            // 
            // ofd_OpenFile
            // 
            this.ofd_OpenFile.DefaultExt = "*.txt";
            this.ofd_OpenFile.FileName = "openFileDialog1";
            this.ofd_OpenFile.Filter = "Text files (*.txt)|*.txt|All (*.*)|*.*";
            // 
            // Distribution_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(403, 406);
            this.Controls.Add(this.gb_CalcDistribution);
            this.Controls.Add(this.btn_CalcDistribution);
            this.Controls.Add(this.gb_Definition);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Distribution_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Distribution";
            this.Shown += new System.EventHandler(this.Distribution_Assistant_Shown);
            this.gb_Definition.ResumeLayout(false);
            this.gb_Definition.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gb_CalcDistribution.ResumeLayout(false);
            this.gb_CalcDistribution.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Type;
        private System.Windows.Forms.Label lbl_Parameters;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cb_DistributionType;
        private System.Windows.Forms.TextBox txt_FirstParam;
        private System.Windows.Forms.TextBox txt_SecondParam;
        private System.Windows.Forms.TextBox txt_ThirdParam;
        private System.Windows.Forms.GroupBox gb_Definition;
        private System.Windows.Forms.Button btn_CalcDistribution;
        private System.Windows.Forms.RadioButton rb_existingTable;
        private System.Windows.Forms.RadioButton rb_FromFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_Tables;
        private System.Windows.Forms.ComboBox cb_Scenario;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_Table;
        private System.Windows.Forms.Label lbl_NameFile;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.ComboBox cb_Column;
        private System.Windows.Forms.Label lbl_Column;
        private System.Windows.Forms.GroupBox gb_CalcDistribution;
        private System.Windows.Forms.Button btn_Analyse;
        private System.Windows.Forms.OpenFileDialog ofd_OpenFile;
    }
}