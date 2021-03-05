namespace SIMCORE_TOOL.Assistant
{
    partial class BHS_HBS3_Routing_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHS_HBS3_Routing_Assistant));
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.p_Content = new System.Windows.Forms.Panel();
            this.lbl_MES_End = new System.Windows.Forms.Label();
            this.lbl_MES_Start = new System.Windows.Forms.Label();
            this.lbl_HBS1_End = new System.Windows.Forms.Label();
            this.lbl_HBS1_Start = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(401, 382);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(77, 35);
            this.btn_Cancel.TabIndex = 11;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(73, 382);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 35);
            this.btn_Ok.TabIndex = 10;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // p_Content
            // 
            this.p_Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p_Content.AutoScroll = true;
            this.p_Content.BackColor = System.Drawing.Color.Transparent;
            this.p_Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p_Content.Location = new System.Drawing.Point(12, 48);
            this.p_Content.Name = "p_Content";
            this.p_Content.Size = new System.Drawing.Size(524, 296);
            this.p_Content.TabIndex = 57;
            // 
            // lbl_MES_End
            // 
            this.lbl_MES_End.AutoSize = true;
            this.lbl_MES_End.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MES_End.Location = new System.Drawing.Point(252, 9);
            this.lbl_MES_End.Name = "lbl_MES_End";
            this.lbl_MES_End.Size = new System.Drawing.Size(53, 26);
            this.lbl_MES_End.TabIndex = 7;
            this.lbl_MES_End.Text = "MES\r\nend index";
            this.lbl_MES_End.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MES_Start
            // 
            this.lbl_MES_Start.AutoSize = true;
            this.lbl_MES_Start.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MES_Start.Location = new System.Drawing.Point(162, 9);
            this.lbl_MES_Start.Name = "lbl_MES_Start";
            this.lbl_MES_Start.Size = new System.Drawing.Size(55, 26);
            this.lbl_MES_Start.TabIndex = 6;
            this.lbl_MES_Start.Text = "MES\r\nstart index";
            this.lbl_MES_Start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_HBS1_End
            // 
            this.lbl_HBS1_End.AutoSize = true;
            this.lbl_HBS1_End.BackColor = System.Drawing.Color.Transparent;
            this.lbl_HBS1_End.Location = new System.Drawing.Point(432, 9);
            this.lbl_HBS1_End.Name = "lbl_HBS1_End";
            this.lbl_HBS1_End.Size = new System.Drawing.Size(53, 26);
            this.lbl_HBS1_End.TabIndex = 3;
            this.lbl_HBS1_End.Text = "Make-Up\r\nend index";
            this.lbl_HBS1_End.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_HBS1_Start
            // 
            this.lbl_HBS1_Start.AutoSize = true;
            this.lbl_HBS1_Start.BackColor = System.Drawing.Color.Transparent;
            this.lbl_HBS1_Start.Location = new System.Drawing.Point(342, 9);
            this.lbl_HBS1_Start.Name = "lbl_HBS1_Start";
            this.lbl_HBS1_Start.Size = new System.Drawing.Size(55, 26);
            this.lbl_HBS1_Start.TabIndex = 0;
            this.lbl_HBS1_Start.Text = "Make-Up\r\nstart index";
            this.lbl_HBS1_Start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BHS_HBS3_Routing_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(548, 438);
            this.Controls.Add(this.p_Content);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.lbl_MES_End);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.lbl_MES_Start);
            this.Controls.Add(this.lbl_HBS1_Start);
            this.Controls.Add(this.lbl_HBS1_End);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(554, 466);
            this.Name = "BHS_HBS3_Routing_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BHS_General_Assistant";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Panel p_Content;
        private System.Windows.Forms.Label lbl_MES_End;
        private System.Windows.Forms.Label lbl_MES_Start;
        private System.Windows.Forms.Label lbl_HBS1_End;
        private System.Windows.Forms.Label lbl_HBS1_Start;

    }
}