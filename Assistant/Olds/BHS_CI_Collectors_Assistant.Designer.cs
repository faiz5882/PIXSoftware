namespace SIMCORE_TOOL.Assistant
{
    partial class BHS_CI_Collectors_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHS_CI_Collectors_Assistant));
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.p_Content = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.AccessibleDescription = null;
            this.btn_Cancel.AccessibleName = null;
            resources.ApplyResources(this.btn_Cancel, "btn_Cancel");
            this.btn_Cancel.BackgroundImage = null;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = null;
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.AccessibleDescription = null;
            this.btn_Ok.AccessibleName = null;
            resources.ApplyResources(this.btn_Ok, "btn_Ok");
            this.btn_Ok.BackgroundImage = null;
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Font = null;
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // p_Content
            // 
            this.p_Content.AccessibleDescription = null;
            this.p_Content.AccessibleName = null;
            resources.ApplyResources(this.p_Content, "p_Content");
            this.p_Content.BackColor = System.Drawing.Color.Transparent;
            this.p_Content.BackgroundImage = null;
            this.p_Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p_Content.Font = null;
            this.p_Content.Name = "p_Content";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = null;
            this.label2.Name = "label2";
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
            // BHS_CI_Collectors_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.btn_Cancel;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.p_Content);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BHS_CI_Collectors_Assistant";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.BHS_CI_Collectors_Assistant_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Panel p_Content;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}