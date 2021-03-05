﻿namespace SIMCORE_TOOL.Assistant
{
    partial class LFA_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LFA_Assistant));
            this.cb_FC = new System.Windows.Forms.ComboBox();
            this.tb_FirstClass = new System.Windows.Forms.TextBox();
            this.tb_Local = new System.Windows.Forms.TextBox();
            this.tb_Terminating = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_SecondClass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_NotLocal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_Transferring = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_ReCheck = new System.Windows.Forms.TextBox();
            this.tb_Transfer = new System.Windows.Forms.TextBox();
            this.tb_LoadFactor = new System.Windows.Forms.TextBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txt_OOGTerm = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_OOGTransf = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lbl_PaxPerCar = new System.Windows.Forms.Label();
            this.txt_PaxPerCar = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_FC
            // 
            this.cb_FC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FC.FormattingEnabled = true;
            this.cb_FC.Location = new System.Drawing.Point(179, 24);
            this.cb_FC.Name = "cb_FC";
            this.cb_FC.Size = new System.Drawing.Size(129, 21);
            this.cb_FC.TabIndex = 0;
            this.cb_FC.SelectedIndexChanged += new System.EventHandler(this.cb_FC_SelectedIndexChanged);
            // 
            // tb_FirstClass
            // 
            this.tb_FirstClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tb_FirstClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tb_FirstClass.Location = new System.Drawing.Point(94, 19);
            this.tb_FirstClass.Name = "tb_FirstClass";
            this.tb_FirstClass.Size = new System.Drawing.Size(60, 20);
            this.tb_FirstClass.TabIndex = 1;
            this.tb_FirstClass.TextChanged += new System.EventHandler(this.mtb_FirstClass_TextChanged);
            this.tb_FirstClass.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // tb_Local
            // 
            this.tb_Local.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tb_Local.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tb_Local.Location = new System.Drawing.Point(107, 19);
            this.tb_Local.Name = "tb_Local";
            this.tb_Local.Size = new System.Drawing.Size(66, 20);
            this.tb_Local.TabIndex = 1;
            this.tb_Local.TextChanged += new System.EventHandler(this.mtb_Local_TextChanged);
            this.tb_Local.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // tb_Terminating
            // 
            this.tb_Terminating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tb_Terminating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tb_Terminating.Location = new System.Drawing.Point(94, 17);
            this.tb_Terminating.Name = "tb_Terminating";
            this.tb_Terminating.Size = new System.Drawing.Size(60, 20);
            this.tb_Terminating.TabIndex = 1;
            this.tb_Terminating.TextChanged += new System.EventHandler(this.mtb_Terminating_TextChanged);
            this.tb_Terminating.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_SecondClass);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tb_FirstClass);
            this.groupBox2.Location = new System.Drawing.Point(21, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 74);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Class distribution";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(9, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Eco.  (%)";
            // 
            // txt_SecondClass
            // 
            this.txt_SecondClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_SecondClass.Enabled = false;
            this.txt_SecondClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_SecondClass.Location = new System.Drawing.Point(94, 45);
            this.txt_SecondClass.Name = "txt_SecondClass";
            this.txt_SecondClass.Size = new System.Drawing.Size(60, 20);
            this.txt_SecondClass.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "F && B (%)";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.txt_NotLocal);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tb_Local);
            this.groupBox3.Location = new System.Drawing.Point(200, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(179, 74);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Passport type";
            // 
            // txt_NotLocal
            // 
            this.txt_NotLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_NotLocal.Enabled = false;
            this.txt_NotLocal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_NotLocal.Location = new System.Drawing.Point(107, 45);
            this.txt_NotLocal.Name = "txt_NotLocal";
            this.txt_NotLocal.Size = new System.Drawing.Size(66, 20);
            this.txt_NotLocal.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(12, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Not local (%)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(12, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Local (%)";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.txt_Transferring);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tb_Terminating);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(21, 179);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(173, 77);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Travel type";
            // 
            // txt_Transferring
            // 
            this.txt_Transferring.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Transferring.Enabled = false;
            this.txt_Transferring.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_Transferring.Location = new System.Drawing.Point(94, 44);
            this.txt_Transferring.Name = "txt_Transferring";
            this.txt_Transferring.Size = new System.Drawing.Size(60, 20);
            this.txt_Transferring.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(8, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Transferring (%)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(9, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Terminating (%)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(63, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Category :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(78, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Load factor (%) :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tb_ReCheck);
            this.groupBox1.Controls.Add(this.tb_Transfer);
            this.groupBox1.Location = new System.Drawing.Point(200, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 77);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TransferType";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(12, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Transfer desk (%)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(15, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Re-Check (%)";
            // 
            // tb_ReCheck
            // 
            this.tb_ReCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tb_ReCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tb_ReCheck.Location = new System.Drawing.Point(107, 17);
            this.tb_ReCheck.Name = "tb_ReCheck";
            this.tb_ReCheck.Size = new System.Drawing.Size(66, 20);
            this.tb_ReCheck.TabIndex = 1;
            this.tb_ReCheck.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // tb_Transfer
            // 
            this.tb_Transfer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tb_Transfer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tb_Transfer.Location = new System.Drawing.Point(107, 44);
            this.tb_Transfer.Name = "tb_Transfer";
            this.tb_Transfer.Size = new System.Drawing.Size(66, 20);
            this.tb_Transfer.TabIndex = 2;
            this.tb_Transfer.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // tb_LoadFactor
            // 
            this.tb_LoadFactor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tb_LoadFactor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tb_LoadFactor.Location = new System.Drawing.Point(166, 73);
            this.tb_LoadFactor.Name = "tb_LoadFactor";
            this.tb_LoadFactor.Size = new System.Drawing.Size(79, 20);
            this.tb_LoadFactor.TabIndex = 1;
            this.tb_LoadFactor.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // BT_OK
            // 
            this.BT_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BT_OK.BackColor = System.Drawing.Color.Transparent;
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Location = new System.Drawing.Point(47, 348);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(75, 23);
            this.BT_OK.TabIndex = 7;
            this.BT_OK.Text = "OK";
            this.BT_OK.UseVisualStyleBackColor = false;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_CANCEL
            // 
            this.BT_CANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_CANCEL.BackColor = System.Drawing.Color.Transparent;
            this.BT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_CANCEL.Location = new System.Drawing.Point(268, 348);
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.BT_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BT_CANCEL.TabIndex = 8;
            this.BT_CANCEL.Text = "Cancel";
            this.BT_CANCEL.UseVisualStyleBackColor = false;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.txt_OOGTerm);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txt_OOGTransf);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Location = new System.Drawing.Point(21, 262);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(173, 72);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Bags out of gauge (OOG)";
            // 
            // txt_OOGTerm
            // 
            this.txt_OOGTerm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_OOGTerm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_OOGTerm.Location = new System.Drawing.Point(94, 43);
            this.txt_OOGTerm.Name = "txt_OOGTerm";
            this.txt_OOGTerm.Size = new System.Drawing.Size(60, 20);
            this.txt_OOGTerm.TabIndex = 16;
            this.txt_OOGTerm.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(9, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Terminating (%)";
            // 
            // txt_OOGTransf
            // 
            this.txt_OOGTransf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_OOGTransf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_OOGTransf.Location = new System.Drawing.Point(94, 17);
            this.txt_OOGTransf.Name = "txt_OOGTransf";
            this.txt_OOGTransf.Size = new System.Drawing.Size(60, 20);
            this.txt_OOGTransf.TabIndex = 1;
            this.txt_OOGTransf.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(9, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Transferring (%)";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.lbl_PaxPerCar);
            this.groupBox6.Controls.Add(this.txt_PaxPerCar);
            this.groupBox6.Location = new System.Drawing.Point(200, 262);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(179, 42);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Number of passengers per car";
            // 
            // lbl_PaxPerCar
            // 
            this.lbl_PaxPerCar.AutoSize = true;
            this.lbl_PaxPerCar.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PaxPerCar.Location = new System.Drawing.Point(12, 20);
            this.lbl_PaxPerCar.Name = "lbl_PaxPerCar";
            this.lbl_PaxPerCar.Size = new System.Drawing.Size(93, 13);
            this.lbl_PaxPerCar.TabIndex = 2;
            this.lbl_PaxPerCar.Text = "Nb Of Passengers";
            // 
            // txt_PaxPerCar
            // 
            this.txt_PaxPerCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_PaxPerCar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_PaxPerCar.Location = new System.Drawing.Point(127, 17);
            this.txt_PaxPerCar.Name = "txt_PaxPerCar";
            this.txt_PaxPerCar.Size = new System.Drawing.Size(46, 20);
            this.txt_PaxPerCar.TabIndex = 1;
            this.txt_PaxPerCar.Validating += new System.ComponentModel.CancelEventHandler(this.Poucentage_Validating);
            // 
            // LFA_Assistant
            // 
            this.AcceptButton = this.BT_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BT_CANCEL;
            this.ClientSize = new System.Drawing.Size(404, 406);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.tb_LoadFactor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cb_FC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(410, 434);
            this.Name = "LFA_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Arrival load factor";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_FC;
        private System.Windows.Forms.TextBox tb_FirstClass;
        private System.Windows.Forms.TextBox tb_Local;
        private System.Windows.Forms.TextBox tb_Terminating;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_Transfer;
        private System.Windows.Forms.TextBox txt_SecondClass;
        private System.Windows.Forms.TextBox txt_NotLocal;
        private System.Windows.Forms.TextBox txt_Transferring;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_ReCheck;
        private System.Windows.Forms.TextBox tb_LoadFactor;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_CANCEL;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txt_OOGTransf;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_OOGTerm;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lbl_PaxPerCar;
        private System.Windows.Forms.TextBox txt_PaxPerCar;
    }
}