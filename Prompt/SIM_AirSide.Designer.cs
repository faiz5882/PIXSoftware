namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_AirSide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_AirSide));
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_NB_Parking = new System.Windows.Forms.Label();
            this.lbl_NB_Pistes = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(35, 156);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 32);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "&Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(196, 156);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 32);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // lbl_NB_Parking
            // 
            this.lbl_NB_Parking.AutoSize = true;
            this.lbl_NB_Parking.BackColor = System.Drawing.Color.Transparent;
            this.lbl_NB_Parking.Location = new System.Drawing.Point(73, 71);
            this.lbl_NB_Parking.Name = "lbl_NB_Parking";
            this.lbl_NB_Parking.Size = new System.Drawing.Size(35, 13);
            this.lbl_NB_Parking.TabIndex = 2;
            this.lbl_NB_Parking.Text = "label1";
            // 
            // lbl_NB_Pistes
            // 
            this.lbl_NB_Pistes.AutoSize = true;
            this.lbl_NB_Pistes.BackColor = System.Drawing.Color.Transparent;
            this.lbl_NB_Pistes.Location = new System.Drawing.Point(73, 108);
            this.lbl_NB_Pistes.Name = "lbl_NB_Pistes";
            this.lbl_NB_Pistes.Size = new System.Drawing.Size(35, 13);
            this.lbl_NB_Pistes.TabIndex = 3;
            this.lbl_NB_Pistes.Text = "label2";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(63, 23);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(163, 20);
            this.lbl_Title.TabIndex = 4;
            this.lbl_Title.Text = "AirSide parameters";
            // 
            // SIM_AirSide
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(292, 211);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.lbl_NB_Pistes);
            this.Controls.Add(this.lbl_NB_Parking);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SIM_AirSide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AirSide";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label lbl_NB_Parking;
        private System.Windows.Forms.Label lbl_NB_Pistes;
        private System.Windows.Forms.Label lbl_Title;
    }
}