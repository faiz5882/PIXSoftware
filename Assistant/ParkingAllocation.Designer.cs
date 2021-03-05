namespace SIMCORE_TOOL.Assistant
{
    partial class ParkingAllocation
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
            this.lbl_Parking = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cb_Parking = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_Parking
            // 
            this.lbl_Parking.AutoSize = true;
            this.lbl_Parking.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Parking.Location = new System.Drawing.Point(40, 24);
            this.lbl_Parking.Name = "lbl_Parking";
            this.lbl_Parking.Size = new System.Drawing.Size(43, 13);
            this.lbl_Parking.TabIndex = 0;
            this.lbl_Parking.Text = "Parking";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(92, 71);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(81, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Use Shuttle";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(22, 116);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 6;
            this.btn_ok.Text = "&Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(191, 116);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // cb_Parking
            // 
            this.cb_Parking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Parking.FormattingEnabled = true;
            this.cb_Parking.Location = new System.Drawing.Point(110, 21);
            this.cb_Parking.Name = "cb_Parking";
            this.cb_Parking.Size = new System.Drawing.Size(147, 21);
            this.cb_Parking.TabIndex = 8;
            // 
            // ParkingAllocation
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(292, 163);
            this.Controls.Add(this.cb_Parking);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lbl_Parking);
            this.Name = "ParkingAllocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ParkingAllocation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Parking;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cb_Parking;
    }
}