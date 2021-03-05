namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Axes_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Axes_Assistant));
            this.lbl_X = new System.Windows.Forms.Label();
            this.lbl_Y = new System.Windows.Forms.Label();
            this.lbl_Y2 = new System.Windows.Forms.Label();
            this.txt_X = new System.Windows.Forms.TextBox();
            this.txt_Y = new System.Windows.Forms.TextBox();
            this.txt_Y2 = new System.Windows.Forms.TextBox();
            this.Btn_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_X
            // 
            this.lbl_X.AutoSize = true;
            this.lbl_X.BackColor = System.Drawing.Color.Transparent;
            this.lbl_X.Location = new System.Drawing.Point(72, 28);
            this.lbl_X.Name = "lbl_X";
            this.lbl_X.Size = new System.Drawing.Size(14, 13);
            this.lbl_X.TabIndex = 0;
            this.lbl_X.Text = "X";
            // 
            // lbl_Y
            // 
            this.lbl_Y.AutoSize = true;
            this.lbl_Y.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Y.Location = new System.Drawing.Point(72, 58);
            this.lbl_Y.Name = "lbl_Y";
            this.lbl_Y.Size = new System.Drawing.Size(14, 13);
            this.lbl_Y.TabIndex = 1;
            this.lbl_Y.Text = "Y";
            // 
            // lbl_Y2
            // 
            this.lbl_Y2.AutoSize = true;
            this.lbl_Y2.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Y2.Location = new System.Drawing.Point(72, 91);
            this.lbl_Y2.Name = "lbl_Y2";
            this.lbl_Y2.Size = new System.Drawing.Size(20, 13);
            this.lbl_Y2.TabIndex = 2;
            this.lbl_Y2.Text = "Y2";
            // 
            // txt_X
            // 
            this.txt_X.Location = new System.Drawing.Point(107, 25);
            this.txt_X.Name = "txt_X";
            this.txt_X.Size = new System.Drawing.Size(100, 20);
            this.txt_X.TabIndex = 3;
            // 
            // txt_Y
            // 
            this.txt_Y.Location = new System.Drawing.Point(107, 55);
            this.txt_Y.Name = "txt_Y";
            this.txt_Y.Size = new System.Drawing.Size(100, 20);
            this.txt_Y.TabIndex = 4;
            // 
            // txt_Y2
            // 
            this.txt_Y2.Location = new System.Drawing.Point(107, 88);
            this.txt_Y2.Name = "txt_Y2";
            this.txt_Y2.Size = new System.Drawing.Size(100, 20);
            this.txt_Y2.TabIndex = 5;
            // 
            // Btn_ok
            // 
            this.Btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_ok.Location = new System.Drawing.Point(93, 137);
            this.Btn_ok.Name = "Btn_ok";
            this.Btn_ok.Size = new System.Drawing.Size(75, 23);
            this.Btn_ok.TabIndex = 6;
            this.Btn_ok.Text = "&Ok";
            this.Btn_ok.UseVisualStyleBackColor = true;
            // 
            // SIM_Axes_Assistant
            // 
            this.AcceptButton = this.Btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 187);
            this.Controls.Add(this.Btn_ok);
            this.Controls.Add(this.txt_Y2);
            this.Controls.Add(this.txt_Y);
            this.Controls.Add(this.txt_X);
            this.Controls.Add(this.lbl_Y2);
            this.Controls.Add(this.lbl_Y);
            this.Controls.Add(this.lbl_X);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SIM_Axes_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rename graphic axes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_X;
        private System.Windows.Forms.Label lbl_Y;
        private System.Windows.Forms.Label lbl_Y2;
        private System.Windows.Forms.TextBox txt_X;
        private System.Windows.Forms.TextBox txt_Y;
        private System.Windows.Forms.TextBox txt_Y2;
        private System.Windows.Forms.Button Btn_ok;
    }
}