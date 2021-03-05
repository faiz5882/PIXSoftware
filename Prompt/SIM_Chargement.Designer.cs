namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Chargement
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
            this.pb_Chargement = new System.Windows.Forms.ProgressBar();
            this.lbl_Chargement = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Chargement
            // 
            this.pb_Chargement.Location = new System.Drawing.Point(12, 116);
            this.pb_Chargement.Name = "pb_Chargement";
            this.pb_Chargement.Size = new System.Drawing.Size(440, 23);
            this.pb_Chargement.TabIndex = 0;
            // 
            // lbl_Chargement
            // 
            this.lbl_Chargement.AutoSize = true;
            this.lbl_Chargement.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Chargement.Location = new System.Drawing.Point(23, 100);
            this.lbl_Chargement.Name = "lbl_Chargement";
            this.lbl_Chargement.Size = new System.Drawing.Size(35, 13);
            this.lbl_Chargement.TabIndex = 1;
            this.lbl_Chargement.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.Image = global::SIMCORE_TOOL.Properties.Resources.Bande_Logo_PAX2SIM_20132;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(440, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // SIM_Chargement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(469, 153);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_Chargement);
            this.Controls.Add(this.pb_Chargement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SIM_Chargement";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SIM_Chargement";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb_Chargement;
        private System.Windows.Forms.Label lbl_Chargement;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}