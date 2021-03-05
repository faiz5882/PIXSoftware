namespace SIMCORE_TOOL.Assistant
{
    partial class Oneof_AddFrequency_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Oneof_AddFrequency_Assistant));
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.BTN_Ok = new System.Windows.Forms.Button();
            this.LBL_Frequency = new System.Windows.Forms.Label();
            this.LBL_Value = new System.Windows.Forms.Label();
            this.TXT_Value = new System.Windows.Forms.TextBox();
            this.TXT_Frequency = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_Cancel.Location = new System.Drawing.Point(214, 150);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BTN_Cancel.TabIndex = 4;
            this.BTN_Cancel.Text = "&Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            // 
            // BTN_Ok
            // 
            this.BTN_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_Ok.Location = new System.Drawing.Point(39, 150);
            this.BTN_Ok.Name = "BTN_Ok";
            this.BTN_Ok.Size = new System.Drawing.Size(75, 23);
            this.BTN_Ok.TabIndex = 3;
            this.BTN_Ok.Text = "&Ok";
            this.BTN_Ok.UseVisualStyleBackColor = true;
            this.BTN_Ok.Click += new System.EventHandler(this.BTN_Ok_Click);
            // 
            // LBL_Frequency
            // 
            this.LBL_Frequency.AutoSize = true;
            this.LBL_Frequency.BackColor = System.Drawing.Color.Transparent;
            this.LBL_Frequency.Location = new System.Drawing.Point(57, 64);
            this.LBL_Frequency.Name = "LBL_Frequency";
            this.LBL_Frequency.Size = new System.Drawing.Size(57, 13);
            this.LBL_Frequency.TabIndex = 2;
            this.LBL_Frequency.Text = "Frequency";
            // 
            // LBL_Value
            // 
            this.LBL_Value.AutoSize = true;
            this.LBL_Value.BackColor = System.Drawing.Color.Transparent;
            this.LBL_Value.Location = new System.Drawing.Point(57, 90);
            this.LBL_Value.Name = "LBL_Value";
            this.LBL_Value.Size = new System.Drawing.Size(83, 13);
            this.LBL_Value.TabIndex = 3;
            this.LBL_Value.Text = "Value (seconds)";
            // 
            // TXT_Value
            // 
            this.TXT_Value.Location = new System.Drawing.Point(164, 87);
            this.TXT_Value.Name = "TXT_Value";
            this.TXT_Value.Size = new System.Drawing.Size(100, 20);
            this.TXT_Value.TabIndex = 2;
            this.TXT_Value.TextChanged += new System.EventHandler(this.TXT_Frequency_TextChanged);
            // 
            // TXT_Frequency
            // 
            this.TXT_Frequency.Location = new System.Drawing.Point(164, 61);
            this.TXT_Frequency.Name = "TXT_Frequency";
            this.TXT_Frequency.Size = new System.Drawing.Size(100, 20);
            this.TXT_Frequency.TabIndex = 1;
            this.TXT_Frequency.TextChanged += new System.EventHandler(this.TXT_Frequency_TextChanged);
            // 
            // Oneof_AddFrequency_Assistant
            // 
            this.AcceptButton = this.BTN_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BTN_Cancel;
            this.ClientSize = new System.Drawing.Size(328, 212);
            this.Controls.Add(this.TXT_Frequency);
            this.Controls.Add(this.TXT_Value);
            this.Controls.Add(this.LBL_Value);
            this.Controls.Add(this.LBL_Frequency);
            this.Controls.Add(this.BTN_Ok);
            this.Controls.Add(this.BTN_Cancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Oneof_AddFrequency_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add frequency for probability profile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Button BTN_Ok;
        private System.Windows.Forms.Label LBL_Frequency;
        private System.Windows.Forms.Label LBL_Value;
        private System.Windows.Forms.TextBox TXT_Value;
        private System.Windows.Forms.TextBox TXT_Frequency;
    }
}