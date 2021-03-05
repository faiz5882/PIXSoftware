namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Allocation_TransferInfeed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Allocation_TransferInfeed));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txt_TitleTable = new System.Windows.Forms.TextBox();
            this.cb_FPDTable = new System.Windows.Forms.ComboBox();
            this.cb_AllocationRules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_NumberOfMakeUp = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_EndTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_EndTime = new System.Windows.Forms.Label();
            this.dtp_BeginTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_BeginTime = new System.Windows.Forms.Label();
            this.txt_TimeStep = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_Terminal = new System.Windows.Forms.ComboBox();
            this.cb_AllocateFlightPlan = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Separation = new System.Windows.Forms.TextBox();
            this.cb_ColorFC = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(61, 400);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 12;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(240, 400);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // txt_TitleTable
            // 
            this.txt_TitleTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_TitleTable.Location = new System.Drawing.Point(216, 45);
            this.txt_TitleTable.Name = "txt_TitleTable";
            this.txt_TitleTable.Size = new System.Drawing.Size(121, 20);
            this.txt_TitleTable.TabIndex = 1;
            // 
            // cb_FPDTable
            // 
            this.cb_FPDTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_FPDTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FPDTable.FormattingEnabled = true;
            this.cb_FPDTable.Location = new System.Drawing.Point(216, 84);
            this.cb_FPDTable.Name = "cb_FPDTable";
            this.cb_FPDTable.Size = new System.Drawing.Size(121, 21);
            this.cb_FPDTable.TabIndex = 2;
            this.cb_FPDTable.SelectedIndexChanged += new System.EventHandler(this.cb_FPDTable_SelectedIndexChanged);
            // 
            // cb_AllocationRules
            // 
            this.cb_AllocationRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_AllocationRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AllocationRules.FormattingEnabled = true;
            this.cb_AllocationRules.Location = new System.Drawing.Point(216, 111);
            this.cb_AllocationRules.Name = "cb_AllocationRules";
            this.cb_AllocationRules.Size = new System.Drawing.Size(121, 21);
            this.cb_AllocationRules.TabIndex = 3;
            this.cb_AllocationRules.SelectedIndexChanged += new System.EventHandler(this.cb_AllocationRules_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(33, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Allocation Transfer Infeed rules";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(33, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Flight plan arrival";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(33, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Table name :";
            // 
            // cb_NumberOfMakeUp
            // 
            this.cb_NumberOfMakeUp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_NumberOfMakeUp.FormattingEnabled = true;
            this.cb_NumberOfMakeUp.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100",
            "110",
            "120",
            "130",
            "140",
            "150",
            "160",
            "170",
            "180",
            "190",
            "200"});
            this.cb_NumberOfMakeUp.Location = new System.Drawing.Point(216, 138);
            this.cb_NumberOfMakeUp.Name = "cb_NumberOfMakeUp";
            this.cb_NumberOfMakeUp.Size = new System.Drawing.Size(69, 21);
            this.cb_NumberOfMakeUp.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(33, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Number maximum of Transfer Infeed";
            // 
            // dtp_EndTime
            // 
            this.dtp_EndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_EndTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_EndTime.CustomFormat = "M/d/yyyy h:mm tt";
            this.dtp_EndTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_EndTime.Location = new System.Drawing.Point(216, 220);
            this.dtp_EndTime.Name = "dtp_EndTime";
            this.dtp_EndTime.Size = new System.Drawing.Size(121, 20);
            this.dtp_EndTime.TabIndex = 7;
            // 
            // lbl_EndTime
            // 
            this.lbl_EndTime.AutoSize = true;
            this.lbl_EndTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndTime.Location = new System.Drawing.Point(33, 224);
            this.lbl_EndTime.Name = "lbl_EndTime";
            this.lbl_EndTime.Size = new System.Drawing.Size(50, 13);
            this.lbl_EndTime.TabIndex = 22;
            this.lbl_EndTime.Text = "End date";
            // 
            // dtp_BeginTime
            // 
            this.dtp_BeginTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_BeginTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dtp_BeginTime.CustomFormat = "M/d/yyyy h:mm tt";
            this.dtp_BeginTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dtp_BeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_BeginTime.Location = new System.Drawing.Point(216, 194);
            this.dtp_BeginTime.Name = "dtp_BeginTime";
            this.dtp_BeginTime.Size = new System.Drawing.Size(121, 20);
            this.dtp_BeginTime.TabIndex = 6;
            // 
            // lbl_BeginTime
            // 
            this.lbl_BeginTime.AutoSize = true;
            this.lbl_BeginTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_BeginTime.Location = new System.Drawing.Point(33, 198);
            this.lbl_BeginTime.Name = "lbl_BeginTime";
            this.lbl_BeginTime.Size = new System.Drawing.Size(58, 13);
            this.lbl_BeginTime.TabIndex = 19;
            this.lbl_BeginTime.Text = "Begin date";
            // 
            // txt_TimeStep
            // 
            this.txt_TimeStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_TimeStep.Location = new System.Drawing.Point(216, 246);
            this.txt_TimeStep.Name = "txt_TimeStep";
            this.txt_TimeStep.Size = new System.Drawing.Size(121, 20);
            this.txt_TimeStep.TabIndex = 8;
            this.txt_TimeStep.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(33, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Time step (min)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(33, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Terminal";
            // 
            // cb_Terminal
            // 
            this.cb_Terminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Terminal.FormattingEnabled = true;
            this.cb_Terminal.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cb_Terminal.Location = new System.Drawing.Point(216, 166);
            this.cb_Terminal.Name = "cb_Terminal";
            this.cb_Terminal.Size = new System.Drawing.Size(69, 21);
            this.cb_Terminal.TabIndex = 5;
            // 
            // cb_AllocateFlightPlan
            // 
            this.cb_AllocateFlightPlan.AutoSize = true;
            this.cb_AllocateFlightPlan.BackColor = System.Drawing.Color.Transparent;
            this.cb_AllocateFlightPlan.Location = new System.Drawing.Point(106, 314);
            this.cb_AllocateFlightPlan.Name = "cb_AllocateFlightPlan";
            this.cb_AllocateFlightPlan.Size = new System.Drawing.Size(186, 17);
            this.cb_AllocateFlightPlan.TabIndex = 10;
            this.cb_AllocateFlightPlan.Text = "Update FPA with allocated values";
            this.cb_AllocateFlightPlan.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(33, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Delay between 2 flights (min)";
            // 
            // txt_Separation
            // 
            this.txt_Separation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Separation.Location = new System.Drawing.Point(216, 272);
            this.txt_Separation.Name = "txt_Separation";
            this.txt_Separation.Size = new System.Drawing.Size(121, 20);
            this.txt_Separation.TabIndex = 9;
            this.txt_Separation.Text = "10";
            // 
            // cb_ColorFC
            // 
            this.cb_ColorFC.AutoSize = true;
            this.cb_ColorFC.BackColor = System.Drawing.Color.Transparent;
            this.cb_ColorFC.Checked = true;
            this.cb_ColorFC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_ColorFC.Location = new System.Drawing.Point(106, 337);
            this.cb_ColorFC.Name = "cb_ColorFC";
            this.cb_ColorFC.Size = new System.Drawing.Size(166, 17);
            this.cb_ColorFC.TabIndex = 11;
            this.cb_ColorFC.Text = "Define color by flight category";
            this.cb_ColorFC.UseVisualStyleBackColor = false;
            // 
            // SIM_Allocation_TransferInfeed
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(367, 449);
            this.Controls.Add(this.cb_ColorFC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_Separation);
            this.Controls.Add(this.cb_AllocateFlightPlan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_Terminal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_TimeStep);
            this.Controls.Add(this.dtp_EndTime);
            this.Controls.Add(this.lbl_EndTime);
            this.Controls.Add(this.dtp_BeginTime);
            this.Controls.Add(this.lbl_BeginTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_NumberOfMakeUp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_AllocationRules);
            this.Controls.Add(this.cb_FPDTable);
            this.Controls.Add(this.txt_TitleTable);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(373, 481);
            this.Name = "SIM_Allocation_TransferInfeed";
            this.ShowInTaskbar = false;
            this.Text = "Transfer infeed allocation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txt_TitleTable;
        private System.Windows.Forms.ComboBox cb_FPDTable;
        private System.Windows.Forms.ComboBox cb_AllocationRules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_NumberOfMakeUp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_EndTime;
        private System.Windows.Forms.Label lbl_EndTime;
        private System.Windows.Forms.DateTimePicker dtp_BeginTime;
        private System.Windows.Forms.Label lbl_BeginTime;
        private System.Windows.Forms.TextBox txt_TimeStep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_Terminal;
        private System.Windows.Forms.CheckBox cb_AllocateFlightPlan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Separation;
        private System.Windows.Forms.CheckBox cb_ColorFC;
    }
}