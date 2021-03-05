namespace SIMCORE_TOOL.Assistant
{
    partial class Nb_Bags_Visitors_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Nb_Bags_Visitors_Assistant));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_FC = new System.Windows.Forms.ComboBox();
            this.tb_Nb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Sum = new System.Windows.Forms.Label();
            this.lbl_Mean = new System.Windows.Forms.Label();
            this.pContent = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // cb_FC
            // 
            this.cb_FC.AccessibleDescription = null;
            this.cb_FC.AccessibleName = null;
            resources.ApplyResources(this.cb_FC, "cb_FC");
            this.cb_FC.BackgroundImage = null;
            this.cb_FC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FC.Font = null;
            this.cb_FC.FormattingEnabled = true;
            this.cb_FC.Name = "cb_FC";
            this.cb_FC.SelectedIndexChanged += new System.EventHandler(this.cb_FC_SelectedIndexChanged);
            // 
            // tb_Nb
            // 
            this.tb_Nb.AccessibleDescription = null;
            this.tb_Nb.AccessibleName = null;
            resources.ApplyResources(this.tb_Nb, "tb_Nb");
            this.tb_Nb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tb_Nb.BackgroundImage = null;
            this.tb_Nb.Font = null;
            this.tb_Nb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tb_Nb.Name = "tb_Nb";
            this.tb_Nb.TextChanged += new System.EventHandler(this.ChangeNumberOfRows);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = null;
            this.label4.Name = "label4";
            // 
            // BT_OK
            // 
            this.BT_OK.AccessibleDescription = null;
            this.BT_OK.AccessibleName = null;
            resources.ApplyResources(this.BT_OK, "BT_OK");
            this.BT_OK.BackColor = System.Drawing.Color.Transparent;
            this.BT_OK.BackgroundImage = null;
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Font = null;
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.UseVisualStyleBackColor = false;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_CANCEL
            // 
            this.BT_CANCEL.AccessibleDescription = null;
            this.BT_CANCEL.AccessibleName = null;
            resources.ApplyResources(this.BT_CANCEL, "BT_CANCEL");
            this.BT_CANCEL.BackColor = System.Drawing.Color.Transparent;
            this.BT_CANCEL.BackgroundImage = null;
            this.BT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_CANCEL.Font = null;
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.BT_CANCEL.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AccessibleDescription = null;
            this.label6.AccessibleName = null;
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = null;
            this.label6.Name = "label6";
            // 
            // label7
            // 
            this.label7.AccessibleDescription = null;
            this.label7.AccessibleName = null;
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = null;
            this.label7.Name = "label7";
            // 
            // lbl_Sum
            // 
            this.lbl_Sum.AccessibleDescription = null;
            this.lbl_Sum.AccessibleName = null;
            resources.ApplyResources(this.lbl_Sum, "lbl_Sum");
            this.lbl_Sum.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Sum.Font = null;
            this.lbl_Sum.Name = "lbl_Sum";
            // 
            // lbl_Mean
            // 
            this.lbl_Mean.AccessibleDescription = null;
            this.lbl_Mean.AccessibleName = null;
            resources.ApplyResources(this.lbl_Mean, "lbl_Mean");
            this.lbl_Mean.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mean.Font = null;
            this.lbl_Mean.Name = "lbl_Mean";
            // 
            // pContent
            // 
            this.pContent.AccessibleDescription = null;
            this.pContent.AccessibleName = null;
            resources.ApplyResources(this.pContent, "pContent");
            this.pContent.BackColor = System.Drawing.Color.Transparent;
            this.pContent.BackgroundImage = null;
            this.pContent.Font = null;
            this.pContent.Name = "pContent";
            // 
            // Nb_Bags_Visitors_Assistant
            // 
            this.AcceptButton = this.BT_OK;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.BT_CANCEL;
            this.Controls.Add(this.pContent);
            this.Controls.Add(this.lbl_Mean);
            this.Controls.Add(this.lbl_Sum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_Nb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_FC);
            this.Font = null;
            this.Name = "Nb_Bags_Visitors_Assistant";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_FC;
        private System.Windows.Forms.TextBox tb_Nb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_CANCEL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Sum;
        private System.Windows.Forms.Label lbl_Mean;
        private System.Windows.Forms.Panel pContent;
    }
}