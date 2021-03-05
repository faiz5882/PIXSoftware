namespace SIMCORE_TOOL.Assistant
{
    partial class ICT_ShowUp_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ICT_ShowUp_Assistant));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_FC = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Sum = new System.Windows.Forms.Label();
            this.p_Content = new System.Windows.Forms.Panel();
            this.tb_Pas = new System.Windows.Forms.TextBox();
            this.tb_Start = new System.Windows.Forms.TextBox();
            this.tb_Nb = new System.Windows.Forms.TextBox();
            this.changeNbRowsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Category :";
            // 
            // cb_FC
            // 
            this.cb_FC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_FC.FormattingEnabled = true;
            this.cb_FC.Location = new System.Drawing.Point(151, 22);
            this.cb_FC.Name = "cb_FC";
            this.cb_FC.Size = new System.Drawing.Size(219, 21);
            this.cb_FC.TabIndex = 0;
            this.cb_FC.SelectedIndexChanged += new System.EventHandler(this.cb_FC_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(15, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Step (min)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(151, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Start (min)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(258, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Number of slots";
            // 
            // BT_OK
            // 
            this.BT_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BT_OK.BackColor = System.Drawing.Color.Transparent;
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Location = new System.Drawing.Point(75, 296);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(75, 23);
            this.BT_OK.TabIndex = 50;
            this.BT_OK.Text = "OK";
            this.BT_OK.UseVisualStyleBackColor = false;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_CANCEL
            // 
            this.BT_CANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_CANCEL.BackColor = System.Drawing.Color.Transparent;
            this.BT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_CANCEL.Location = new System.Drawing.Point(315, 296);
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.BT_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BT_CANCEL.TabIndex = 51;
            this.BT_CANCEL.Text = "Cancel";
            this.BT_CANCEL.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(112, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "End time";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(22, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Start time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(230, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 54;
            this.label7.Text = "Value";
            // 
            // lbl_Sum
            // 
            this.lbl_Sum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Sum.AutoSize = true;
            this.lbl_Sum.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Sum.Location = new System.Drawing.Point(179, 259);
            this.lbl_Sum.Name = "lbl_Sum";
            this.lbl_Sum.Size = new System.Drawing.Size(26, 13);
            this.lbl_Sum.TabIndex = 55;
            this.lbl_Sum.Text = "sum";
            // 
            // p_Content
            // 
            this.p_Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.p_Content.AutoScroll = true;
            this.p_Content.BackColor = System.Drawing.Color.Transparent;
            this.p_Content.Location = new System.Drawing.Point(18, 129);
            this.p_Content.Name = "p_Content";
            this.p_Content.Size = new System.Drawing.Size(450, 127);
            this.p_Content.TabIndex = 56;
            // 
            // tb_Pas
            // 
            this.tb_Pas.Location = new System.Drawing.Point(75, 65);
            this.tb_Pas.Name = "tb_Pas";
            this.tb_Pas.Size = new System.Drawing.Size(30, 20);
            this.tb_Pas.TabIndex = 57;
            // 
            // tb_Start
            // 
            this.tb_Start.Location = new System.Drawing.Point(211, 65);
            this.tb_Start.Name = "tb_Start";
            this.tb_Start.Size = new System.Drawing.Size(26, 20);
            this.tb_Start.TabIndex = 58;
            // 
            // tb_Nb
            // 
            this.tb_Nb.Location = new System.Drawing.Point(344, 65);
            this.tb_Nb.Name = "tb_Nb";
            this.tb_Nb.Size = new System.Drawing.Size(26, 20);
            this.tb_Nb.TabIndex = 59;
            // 
            // changeNbRowsButton
            // 
            this.changeNbRowsButton.Location = new System.Drawing.Point(393, 63);
            this.changeNbRowsButton.Name = "changeNbRowsButton";
            this.changeNbRowsButton.Size = new System.Drawing.Size(75, 23);
            this.changeNbRowsButton.TabIndex = 60;
            this.changeNbRowsButton.Text = "Adjust Rows";
            this.changeNbRowsButton.UseVisualStyleBackColor = true;
            this.changeNbRowsButton.Click += new System.EventHandler(this.AdjustRowsClick);
            // 
            // ICT_ShowUp_Assistant
            // 
            this.AcceptButton = this.BT_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BT_CANCEL;
            this.ClientSize = new System.Drawing.Size(499, 342);
            this.Controls.Add(this.changeNbRowsButton);
            this.Controls.Add(this.tb_Nb);
            this.Controls.Add(this.tb_Start);
            this.Controls.Add(this.tb_Pas);
            this.Controls.Add(this.p_Content);
            this.Controls.Add(this.lbl_Sum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_FC);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(393, 320);
            this.Name = "ICT_ShowUp_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ICT_ShowUp_Assistant";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_FC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_CANCEL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Sum;
        private System.Windows.Forms.Panel p_Content;
        private System.Windows.Forms.TextBox tb_Pas;
        private System.Windows.Forms.TextBox tb_Start;
        private System.Windows.Forms.TextBox tb_Nb;
        private System.Windows.Forms.Button changeNbRowsButton;
    }
}