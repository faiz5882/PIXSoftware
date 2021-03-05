namespace SIMCORE_TOOL.Parking
{
    partial class ParkingScenario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParkingScenario));
            this.cb_Scenario = new System.Windows.Forms.ComboBox();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.cb_GeneralTable = new System.Windows.Forms.ComboBox();
            this.cb_Distribution = new System.Windows.Forms.ComboBox();
            this.cb_ModalDistribution = new System.Windows.Forms.ComboBox();
            this.cb_Occupation = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chk_GeneralTable = new System.Windows.Forms.CheckBox();
            this.chk_Distribution = new System.Windows.Forms.CheckBox();
            this.chk_ModalDistribution = new System.Windows.Forms.CheckBox();
            this.chk_Occupation = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cb_Scenario
            // 
            this.cb_Scenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Scenario.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Scenario.FormattingEnabled = true;
            this.cb_Scenario.Location = new System.Drawing.Point(187, 22);
            this.cb_Scenario.Name = "cb_Scenario";
            this.cb_Scenario.Size = new System.Drawing.Size(264, 27);
            this.cb_Scenario.TabIndex = 14;
            this.cb_Scenario.SelectedIndexChanged += new System.EventHandler(this.cb_Scenario_SelectedIndexChanged);
            this.cb_Scenario.TextChanged += new System.EventHandler(this.cb_Scenario_TextChanged);
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Name.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_Name.Location = new System.Drawing.Point(39, 25);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(133, 19);
            this.lbl_Name.TabIndex = 15;
            this.lbl_Name.Text = "Scenario name :";
            // 
            // cb_GeneralTable
            // 
            this.cb_GeneralTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_GeneralTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_GeneralTable.Enabled = false;
            this.cb_GeneralTable.FormattingEnabled = true;
            this.cb_GeneralTable.Location = new System.Drawing.Point(194, 98);
            this.cb_GeneralTable.Name = "cb_GeneralTable";
            this.cb_GeneralTable.Size = new System.Drawing.Size(243, 21);
            this.cb_GeneralTable.TabIndex = 16;
            // 
            // cb_Distribution
            // 
            this.cb_Distribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Distribution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Distribution.Enabled = false;
            this.cb_Distribution.FormattingEnabled = true;
            this.cb_Distribution.Location = new System.Drawing.Point(194, 125);
            this.cb_Distribution.Name = "cb_Distribution";
            this.cb_Distribution.Size = new System.Drawing.Size(243, 21);
            this.cb_Distribution.TabIndex = 17;
            // 
            // cb_ModalDistribution
            // 
            this.cb_ModalDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_ModalDistribution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ModalDistribution.Enabled = false;
            this.cb_ModalDistribution.FormattingEnabled = true;
            this.cb_ModalDistribution.Location = new System.Drawing.Point(194, 152);
            this.cb_ModalDistribution.Name = "cb_ModalDistribution";
            this.cb_ModalDistribution.Size = new System.Drawing.Size(243, 21);
            this.cb_ModalDistribution.TabIndex = 18;
            // 
            // cb_Occupation
            // 
            this.cb_Occupation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Occupation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Occupation.Enabled = false;
            this.cb_Occupation.FormattingEnabled = true;
            this.cb_Occupation.Location = new System.Drawing.Point(194, 179);
            this.cb_Occupation.Name = "cb_Occupation";
            this.cb_Occupation.Size = new System.Drawing.Size(243, 21);
            this.cb_Occupation.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(376, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "&Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(52, 227);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // chk_GeneralTable
            // 
            this.chk_GeneralTable.AutoSize = true;
            this.chk_GeneralTable.BackColor = System.Drawing.Color.Transparent;
            this.chk_GeneralTable.Location = new System.Drawing.Point(29, 100);
            this.chk_GeneralTable.Name = "chk_GeneralTable";
            this.chk_GeneralTable.Size = new System.Drawing.Size(89, 17);
            this.chk_GeneralTable.TabIndex = 26;
            this.chk_GeneralTable.Text = "General table";
            this.chk_GeneralTable.UseVisualStyleBackColor = false;
            this.chk_GeneralTable.CheckedChanged += new System.EventHandler(this.chk_GeneralTable_CheckedChanged);
            // 
            // chk_Distribution
            // 
            this.chk_Distribution.AutoSize = true;
            this.chk_Distribution.BackColor = System.Drawing.Color.Transparent;
            this.chk_Distribution.Location = new System.Drawing.Point(29, 127);
            this.chk_Distribution.Name = "chk_Distribution";
            this.chk_Distribution.Size = new System.Drawing.Size(162, 17);
            this.chk_Distribution.TabIndex = 27;
            this.chk_Distribution.Text = "Distribution of 30rt peak hour";
            this.chk_Distribution.UseVisualStyleBackColor = false;
            this.chk_Distribution.CheckedChanged += new System.EventHandler(this.chk_Distribution_CheckedChanged);
            // 
            // chk_ModalDistribution
            // 
            this.chk_ModalDistribution.AutoSize = true;
            this.chk_ModalDistribution.BackColor = System.Drawing.Color.Transparent;
            this.chk_ModalDistribution.Location = new System.Drawing.Point(29, 154);
            this.chk_ModalDistribution.Name = "chk_ModalDistribution";
            this.chk_ModalDistribution.Size = new System.Drawing.Size(110, 17);
            this.chk_ModalDistribution.TabIndex = 28;
            this.chk_ModalDistribution.Text = "Modal Distribution";
            this.chk_ModalDistribution.UseVisualStyleBackColor = false;
            this.chk_ModalDistribution.CheckedChanged += new System.EventHandler(this.chk_ModalDistribution_CheckedChanged);
            // 
            // chk_Occupation
            // 
            this.chk_Occupation.AutoSize = true;
            this.chk_Occupation.BackColor = System.Drawing.Color.Transparent;
            this.chk_Occupation.Location = new System.Drawing.Point(29, 181);
            this.chk_Occupation.Name = "chk_Occupation";
            this.chk_Occupation.Size = new System.Drawing.Size(118, 17);
            this.chk_Occupation.TabIndex = 29;
            this.chk_Occupation.Text = "Parking occupation";
            this.chk_Occupation.UseVisualStyleBackColor = false;
            this.chk_Occupation.CheckedChanged += new System.EventHandler(this.chk_Occupation_CheckedChanged);
            // 
            // ParkingScenario
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(511, 262);
            this.Controls.Add(this.chk_Occupation);
            this.Controls.Add(this.chk_ModalDistribution);
            this.Controls.Add(this.chk_Distribution);
            this.Controls.Add(this.chk_GeneralTable);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cb_Occupation);
            this.Controls.Add(this.cb_ModalDistribution);
            this.Controls.Add(this.cb_Distribution);
            this.Controls.Add(this.cb_GeneralTable);
            this.Controls.Add(this.cb_Scenario);
            this.Controls.Add(this.lbl_Name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(527, 300);
            this.Name = "ParkingScenario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parking Scenario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_Scenario;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.ComboBox cb_GeneralTable;
        private System.Windows.Forms.ComboBox cb_Distribution;
        private System.Windows.Forms.ComboBox cb_ModalDistribution;
        private System.Windows.Forms.ComboBox cb_Occupation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chk_GeneralTable;
        private System.Windows.Forms.CheckBox chk_Distribution;
        private System.Windows.Forms.CheckBox chk_ModalDistribution;
        private System.Windows.Forms.CheckBox chk_Occupation;
    }
}