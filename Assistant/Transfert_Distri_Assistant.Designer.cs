namespace SIMCORE_TOOL.Assistant
{
    partial class Transfert_Distri_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transfert_Distri_Assistant));
            this.label1 = new System.Windows.Forms.Label();
            this.cbMainComboBox = new System.Windows.Forms.ComboBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Sum = new System.Windows.Forms.Label();
            this.pContent = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // cbMainComboBox
            // 
            this.cbMainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMainComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.cbMainComboBox, "cbMainComboBox");
            this.cbMainComboBox.Name = "cbMainComboBox";
            this.cbMainComboBox.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // BT_OK
            // 
            resources.ApplyResources(this.BT_OK, "BT_OK");
            this.BT_OK.BackColor = System.Drawing.Color.Transparent;
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.UseVisualStyleBackColor = false;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_CANCEL
            // 
            resources.ApplyResources(this.BT_CANCEL, "BT_CANCEL");
            this.BT_CANCEL.BackColor = System.Drawing.Color.Transparent;
            this.BT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.BT_CANCEL.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // lbl_Sum
            // 
            resources.ApplyResources(this.lbl_Sum, "lbl_Sum");
            this.lbl_Sum.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Sum.Name = "lbl_Sum";
            // 
            // pContent
            // 
            resources.ApplyResources(this.pContent, "pContent");
            this.pContent.BackColor = System.Drawing.Color.Transparent;
            this.pContent.Name = "pContent";
            // 
            // Transfert_Distri_Assistant
            // 
            this.AcceptButton = this.BT_OK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BT_CANCEL;
            this.Controls.Add(this.pContent);
            this.Controls.Add(this.lbl_Sum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbMainComboBox);
            this.Name = "Transfert_Distri_Assistant";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMainComboBox;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_CANCEL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Sum;
        private System.Windows.Forms.Panel pContent;
    }
}