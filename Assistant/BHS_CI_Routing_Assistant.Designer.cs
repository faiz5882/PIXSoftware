namespace SIMCORE_TOOL.Assistant
{
    partial class BHS_CI_Routing_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHS_CI_Routing_Assistant));
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.p_Content = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_MU_Start = new System.Windows.Forms.Label();
            this.lbl_MES_End = new System.Windows.Forms.Label();
            this.lbl_MES_Start = new System.Windows.Forms.Label();
            this.lbl_HBS3_End = new System.Windows.Forms.Label();
            this.lbl_HBS3_Start = new System.Windows.Forms.Label();
            this.lbl_HBS1_End = new System.Windows.Forms.Label();
            this.lbl_HBS1_Start = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            resources.ApplyResources(this.btn_Cancel, "btn_Cancel");
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            resources.ApplyResources(this.btn_Ok, "btn_Ok");
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // p_Content
            // 
            resources.ApplyResources(this.p_Content, "p_Content");
            this.p_Content.BackColor = System.Drawing.Color.Transparent;
            this.p_Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p_Content.Name = "p_Content";
            this.p_Content.Paint += new System.Windows.Forms.PaintEventHandler(this.p_Content_Paint);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // lbl_MU_Start
            // 
            resources.ApplyResources(this.lbl_MU_Start, "lbl_MU_Start");
            this.lbl_MU_Start.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MU_Start.Name = "lbl_MU_Start";
            // 
            // lbl_MES_End
            // 
            resources.ApplyResources(this.lbl_MES_End, "lbl_MES_End");
            this.lbl_MES_End.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MES_End.Name = "lbl_MES_End";
            // 
            // lbl_MES_Start
            // 
            resources.ApplyResources(this.lbl_MES_Start, "lbl_MES_Start");
            this.lbl_MES_Start.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MES_Start.Name = "lbl_MES_Start";
            // 
            // lbl_HBS3_End
            // 
            resources.ApplyResources(this.lbl_HBS3_End, "lbl_HBS3_End");
            this.lbl_HBS3_End.BackColor = System.Drawing.Color.Transparent;
            this.lbl_HBS3_End.Name = "lbl_HBS3_End";
            // 
            // lbl_HBS3_Start
            // 
            resources.ApplyResources(this.lbl_HBS3_Start, "lbl_HBS3_Start");
            this.lbl_HBS3_Start.BackColor = System.Drawing.Color.Transparent;
            this.lbl_HBS3_Start.Name = "lbl_HBS3_Start";
            // 
            // lbl_HBS1_End
            // 
            resources.ApplyResources(this.lbl_HBS1_End, "lbl_HBS1_End");
            this.lbl_HBS1_End.BackColor = System.Drawing.Color.Transparent;
            this.lbl_HBS1_End.Name = "lbl_HBS1_End";
            // 
            // lbl_HBS1_Start
            // 
            resources.ApplyResources(this.lbl_HBS1_Start, "lbl_HBS1_Start");
            this.lbl_HBS1_Start.BackColor = System.Drawing.Color.Transparent;
            this.lbl_HBS1_Start.Name = "lbl_HBS1_Start";
            // 
            // BHS_CI_Routing_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.p_Content);
            this.Controls.Add(this.lbl_MU_Start);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.lbl_MES_End);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.lbl_MES_Start);
            this.Controls.Add(this.lbl_HBS1_Start);
            this.Controls.Add(this.lbl_HBS3_End);
            this.Controls.Add(this.lbl_HBS1_End);
            this.Controls.Add(this.lbl_HBS3_Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BHS_CI_Routing_Assistant";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.BHS_CI_Routing_Assistant_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Panel p_Content;
        private System.Windows.Forms.Label lbl_HBS1_Start;
        private System.Windows.Forms.Label lbl_HBS1_End;
        private System.Windows.Forms.Label lbl_HBS3_End;
        private System.Windows.Forms.Label lbl_HBS3_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_MU_Start;
        private System.Windows.Forms.Label lbl_MES_End;
        private System.Windows.Forms.Label lbl_MES_Start;

    }
}