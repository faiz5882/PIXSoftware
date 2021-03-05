namespace SIMCORE_TOOL.Prompt
{
    partial class Distribution_Display
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Distribution_Display));
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.rb_Normal = new System.Windows.Forms.RadioButton();
            this.rb_Exponential = new System.Windows.Forms.RadioButton();
            this.rb_Gamma = new System.Windows.Forms.RadioButton();
            this.rb_LogNormal = new System.Windows.Forms.RadioButton();
            this.rb_Weibull = new System.Windows.Forms.RadioButton();
            this.lbl_Distribution = new System.Windows.Forms.Label();
            this.lbl_Parameters = new System.Windows.Forms.Label();
            this.lbl_Rank = new System.Windows.Forms.Label();
            this.lbl_N_P = new System.Windows.Forms.Label();
            this.lbl_E_P = new System.Windows.Forms.Label();
            this.lbl_G_P = new System.Windows.Forms.Label();
            this.lbl_L_P = new System.Windows.Forms.Label();
            this.lbl_W_P = new System.Windows.Forms.Label();
            this.lbl_N_R = new System.Windows.Forms.Label();
            this.lbl_E_R = new System.Windows.Forms.Label();
            this.lbl_G_R = new System.Windows.Forms.Label();
            this.lbl_L_R = new System.Windows.Forms.Label();
            this.lbl_W_R = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(319, 198);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Enabled = false;
            this.btn_Ok.Location = new System.Drawing.Point(12, 198);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 12;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // rb_Normal
            // 
            this.rb_Normal.AutoSize = true;
            this.rb_Normal.BackColor = System.Drawing.Color.Transparent;
            this.rb_Normal.Enabled = false;
            this.rb_Normal.Location = new System.Drawing.Point(29, 61);
            this.rb_Normal.Name = "rb_Normal";
            this.rb_Normal.Size = new System.Drawing.Size(58, 17);
            this.rb_Normal.TabIndex = 15;
            this.rb_Normal.TabStop = true;
            this.rb_Normal.Text = "Normal";
            this.rb_Normal.UseVisualStyleBackColor = false;
            this.rb_Normal.CheckedChanged += new System.EventHandler(this.rb_Normal_CheckedChanged);
            // 
            // rb_Exponential
            // 
            this.rb_Exponential.AutoSize = true;
            this.rb_Exponential.BackColor = System.Drawing.Color.Transparent;
            this.rb_Exponential.Enabled = false;
            this.rb_Exponential.Location = new System.Drawing.Point(29, 84);
            this.rb_Exponential.Name = "rb_Exponential";
            this.rb_Exponential.Size = new System.Drawing.Size(80, 17);
            this.rb_Exponential.TabIndex = 16;
            this.rb_Exponential.TabStop = true;
            this.rb_Exponential.Text = "Exponential";
            this.rb_Exponential.UseVisualStyleBackColor = false;
            this.rb_Exponential.CheckedChanged += new System.EventHandler(this.rb_Normal_CheckedChanged);
            // 
            // rb_Gamma
            // 
            this.rb_Gamma.AutoSize = true;
            this.rb_Gamma.BackColor = System.Drawing.Color.Transparent;
            this.rb_Gamma.Enabled = false;
            this.rb_Gamma.Location = new System.Drawing.Point(29, 107);
            this.rb_Gamma.Name = "rb_Gamma";
            this.rb_Gamma.Size = new System.Drawing.Size(61, 17);
            this.rb_Gamma.TabIndex = 17;
            this.rb_Gamma.TabStop = true;
            this.rb_Gamma.Text = "Gamma";
            this.rb_Gamma.UseVisualStyleBackColor = false;
            this.rb_Gamma.CheckedChanged += new System.EventHandler(this.rb_Normal_CheckedChanged);
            // 
            // rb_LogNormal
            // 
            this.rb_LogNormal.AutoSize = true;
            this.rb_LogNormal.BackColor = System.Drawing.Color.Transparent;
            this.rb_LogNormal.Enabled = false;
            this.rb_LogNormal.Location = new System.Drawing.Point(29, 130);
            this.rb_LogNormal.Name = "rb_LogNormal";
            this.rb_LogNormal.Size = new System.Drawing.Size(76, 17);
            this.rb_LogNormal.TabIndex = 18;
            this.rb_LogNormal.TabStop = true;
            this.rb_LogNormal.Text = "LogNormal";
            this.rb_LogNormal.UseVisualStyleBackColor = false;
            this.rb_LogNormal.CheckedChanged += new System.EventHandler(this.rb_Normal_CheckedChanged);
            // 
            // rb_Weibull
            // 
            this.rb_Weibull.AutoSize = true;
            this.rb_Weibull.BackColor = System.Drawing.Color.Transparent;
            this.rb_Weibull.Enabled = false;
            this.rb_Weibull.Location = new System.Drawing.Point(29, 151);
            this.rb_Weibull.Name = "rb_Weibull";
            this.rb_Weibull.Size = new System.Drawing.Size(60, 17);
            this.rb_Weibull.TabIndex = 19;
            this.rb_Weibull.TabStop = true;
            this.rb_Weibull.Text = "Weibull";
            this.rb_Weibull.UseVisualStyleBackColor = false;
            this.rb_Weibull.CheckedChanged += new System.EventHandler(this.rb_Normal_CheckedChanged);
            // 
            // lbl_Distribution
            // 
            this.lbl_Distribution.AutoSize = true;
            this.lbl_Distribution.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Distribution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Distribution.Location = new System.Drawing.Point(52, 32);
            this.lbl_Distribution.Name = "lbl_Distribution";
            this.lbl_Distribution.Size = new System.Drawing.Size(71, 13);
            this.lbl_Distribution.TabIndex = 20;
            this.lbl_Distribution.Text = "Distribution";
            // 
            // lbl_Parameters
            // 
            this.lbl_Parameters.AutoSize = true;
            this.lbl_Parameters.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Parameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Parameters.Location = new System.Drawing.Point(176, 32);
            this.lbl_Parameters.Name = "lbl_Parameters";
            this.lbl_Parameters.Size = new System.Drawing.Size(70, 13);
            this.lbl_Parameters.TabIndex = 21;
            this.lbl_Parameters.Text = "Parameters";
            // 
            // lbl_Rank
            // 
            this.lbl_Rank.AutoSize = true;
            this.lbl_Rank.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Rank.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Rank.Location = new System.Drawing.Point(316, 32);
            this.lbl_Rank.Name = "lbl_Rank";
            this.lbl_Rank.Size = new System.Drawing.Size(37, 13);
            this.lbl_Rank.TabIndex = 22;
            this.lbl_Rank.Text = "Rank";
            // 
            // lbl_N_P
            // 
            this.lbl_N_P.AutoSize = true;
            this.lbl_N_P.BackColor = System.Drawing.Color.Transparent;
            this.lbl_N_P.Enabled = false;
            this.lbl_N_P.Location = new System.Drawing.Point(176, 63);
            this.lbl_N_P.Name = "lbl_N_P";
            this.lbl_N_P.Size = new System.Drawing.Size(0, 13);
            this.lbl_N_P.TabIndex = 23;
            // 
            // lbl_E_P
            // 
            this.lbl_E_P.AutoSize = true;
            this.lbl_E_P.BackColor = System.Drawing.Color.Transparent;
            this.lbl_E_P.Enabled = false;
            this.lbl_E_P.Location = new System.Drawing.Point(176, 86);
            this.lbl_E_P.Name = "lbl_E_P";
            this.lbl_E_P.Size = new System.Drawing.Size(0, 13);
            this.lbl_E_P.TabIndex = 24;
            // 
            // lbl_G_P
            // 
            this.lbl_G_P.AutoSize = true;
            this.lbl_G_P.BackColor = System.Drawing.Color.Transparent;
            this.lbl_G_P.Enabled = false;
            this.lbl_G_P.Location = new System.Drawing.Point(176, 109);
            this.lbl_G_P.Name = "lbl_G_P";
            this.lbl_G_P.Size = new System.Drawing.Size(0, 13);
            this.lbl_G_P.TabIndex = 25;
            // 
            // lbl_L_P
            // 
            this.lbl_L_P.AutoSize = true;
            this.lbl_L_P.BackColor = System.Drawing.Color.Transparent;
            this.lbl_L_P.Enabled = false;
            this.lbl_L_P.Location = new System.Drawing.Point(176, 132);
            this.lbl_L_P.Name = "lbl_L_P";
            this.lbl_L_P.Size = new System.Drawing.Size(0, 13);
            this.lbl_L_P.TabIndex = 26;
            // 
            // lbl_W_P
            // 
            this.lbl_W_P.AutoSize = true;
            this.lbl_W_P.BackColor = System.Drawing.Color.Transparent;
            this.lbl_W_P.Enabled = false;
            this.lbl_W_P.Location = new System.Drawing.Point(176, 153);
            this.lbl_W_P.Name = "lbl_W_P";
            this.lbl_W_P.Size = new System.Drawing.Size(0, 13);
            this.lbl_W_P.TabIndex = 27;
            // 
            // lbl_N_R
            // 
            this.lbl_N_R.AutoSize = true;
            this.lbl_N_R.BackColor = System.Drawing.Color.Transparent;
            this.lbl_N_R.Enabled = false;
            this.lbl_N_R.Location = new System.Drawing.Point(316, 63);
            this.lbl_N_R.Name = "lbl_N_R";
            this.lbl_N_R.Size = new System.Drawing.Size(0, 13);
            this.lbl_N_R.TabIndex = 28;
            // 
            // lbl_E_R
            // 
            this.lbl_E_R.AutoSize = true;
            this.lbl_E_R.BackColor = System.Drawing.Color.Transparent;
            this.lbl_E_R.Enabled = false;
            this.lbl_E_R.Location = new System.Drawing.Point(316, 86);
            this.lbl_E_R.Name = "lbl_E_R";
            this.lbl_E_R.Size = new System.Drawing.Size(0, 13);
            this.lbl_E_R.TabIndex = 29;
            // 
            // lbl_G_R
            // 
            this.lbl_G_R.AutoSize = true;
            this.lbl_G_R.BackColor = System.Drawing.Color.Transparent;
            this.lbl_G_R.Enabled = false;
            this.lbl_G_R.Location = new System.Drawing.Point(316, 109);
            this.lbl_G_R.Name = "lbl_G_R";
            this.lbl_G_R.Size = new System.Drawing.Size(0, 13);
            this.lbl_G_R.TabIndex = 30;
            // 
            // lbl_L_R
            // 
            this.lbl_L_R.AutoSize = true;
            this.lbl_L_R.BackColor = System.Drawing.Color.Transparent;
            this.lbl_L_R.Enabled = false;
            this.lbl_L_R.Location = new System.Drawing.Point(316, 132);
            this.lbl_L_R.Name = "lbl_L_R";
            this.lbl_L_R.Size = new System.Drawing.Size(0, 13);
            this.lbl_L_R.TabIndex = 31;
            // 
            // lbl_W_R
            // 
            this.lbl_W_R.AutoSize = true;
            this.lbl_W_R.BackColor = System.Drawing.Color.Transparent;
            this.lbl_W_R.Enabled = false;
            this.lbl_W_R.Location = new System.Drawing.Point(316, 153);
            this.lbl_W_R.Name = "lbl_W_R";
            this.lbl_W_R.Size = new System.Drawing.Size(0, 13);
            this.lbl_W_R.TabIndex = 32;
            // 
            // Distribution_Display
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(406, 233);
            this.Controls.Add(this.lbl_W_R);
            this.Controls.Add(this.lbl_L_R);
            this.Controls.Add(this.lbl_G_R);
            this.Controls.Add(this.lbl_E_R);
            this.Controls.Add(this.lbl_N_R);
            this.Controls.Add(this.lbl_W_P);
            this.Controls.Add(this.lbl_L_P);
            this.Controls.Add(this.lbl_G_P);
            this.Controls.Add(this.lbl_E_P);
            this.Controls.Add(this.lbl_N_P);
            this.Controls.Add(this.lbl_Rank);
            this.Controls.Add(this.lbl_Parameters);
            this.Controls.Add(this.lbl_Distribution);
            this.Controls.Add(this.rb_Weibull);
            this.Controls.Add(this.rb_LogNormal);
            this.Controls.Add(this.rb_Gamma);
            this.Controls.Add(this.rb_Exponential);
            this.Controls.Add(this.rb_Normal);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Distribution_Display";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Distribution_Display";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.RadioButton rb_Normal;
        private System.Windows.Forms.RadioButton rb_Exponential;
        private System.Windows.Forms.RadioButton rb_Gamma;
        private System.Windows.Forms.RadioButton rb_LogNormal;
        private System.Windows.Forms.RadioButton rb_Weibull;
        private System.Windows.Forms.Label lbl_Distribution;
        private System.Windows.Forms.Label lbl_Parameters;
        private System.Windows.Forms.Label lbl_Rank;
        private System.Windows.Forms.Label lbl_N_P;
        private System.Windows.Forms.Label lbl_E_P;
        private System.Windows.Forms.Label lbl_G_P;
        private System.Windows.Forms.Label lbl_L_P;
        private System.Windows.Forms.Label lbl_W_P;
        private System.Windows.Forms.Label lbl_N_R;
        private System.Windows.Forms.Label lbl_E_R;
        private System.Windows.Forms.Label lbl_G_R;
        private System.Windows.Forms.Label lbl_L_R;
        private System.Windows.Forms.Label lbl_W_R;
    }
}