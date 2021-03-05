namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_ColumnPosition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_ColumnPosition));
            this.clb_ColumnPosition = new System.Windows.Forms.ListBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Raise = new System.Windows.Forms.Button();
            this.btn_Lower = new System.Windows.Forms.Button();
            this.gb_ColumnPosition = new System.Windows.Forms.GroupBox();
            this.gb_ColumnPosition.SuspendLayout();
            this.SuspendLayout();
            // 
            // clb_ColumnPosition
            // 
            this.clb_ColumnPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_ColumnPosition.FormattingEnabled = true;
            this.clb_ColumnPosition.HorizontalScrollbar = true;
            this.clb_ColumnPosition.Location = new System.Drawing.Point(21, 19);
            this.clb_ColumnPosition.Name = "clb_ColumnPosition";
            this.clb_ColumnPosition.Size = new System.Drawing.Size(170, 160);
            this.clb_ColumnPosition.TabIndex = 0;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.Location = new System.Drawing.Point(33, 233);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 1;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(185, 233);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Raise
            // 
            this.btn_Raise.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Raise.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_up;
            this.btn_Raise.Location = new System.Drawing.Point(212, 57);
            this.btn_Raise.Name = "btn_Raise";
            this.btn_Raise.Size = new System.Drawing.Size(36, 30);
            this.btn_Raise.TabIndex = 3;
            this.btn_Raise.UseVisualStyleBackColor = true;
            this.btn_Raise.Click += new System.EventHandler(this.btn_Raise_Click);
            // 
            // btn_Lower
            // 
            this.btn_Lower.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Lower.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_down;
            this.btn_Lower.Location = new System.Drawing.Point(212, 104);
            this.btn_Lower.Name = "btn_Lower";
            this.btn_Lower.Size = new System.Drawing.Size(36, 29);
            this.btn_Lower.TabIndex = 4;
            this.btn_Lower.UseVisualStyleBackColor = true;
            this.btn_Lower.Click += new System.EventHandler(this.btn_Lower_Click);
            // 
            // gb_ColumnPosition
            // 
            this.gb_ColumnPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_ColumnPosition.Controls.Add(this.btn_Raise);
            this.gb_ColumnPosition.Controls.Add(this.btn_Lower);
            this.gb_ColumnPosition.Controls.Add(this.clb_ColumnPosition);
            this.gb_ColumnPosition.Location = new System.Drawing.Point(12, 23);
            this.gb_ColumnPosition.Name = "gb_ColumnPosition";
            this.gb_ColumnPosition.Size = new System.Drawing.Size(271, 195);
            this.gb_ColumnPosition.TabIndex = 5;
            this.gb_ColumnPosition.TabStop = false;
            this.gb_ColumnPosition.Text = "Column Position";
            // 
            // SIM_ColumnPosition
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(295, 266);
            this.Controls.Add(this.gb_ColumnPosition);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SIM_ColumnPosition";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Column Position";
            this.gb_ColumnPosition.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox clb_ColumnPosition;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Raise;
        private System.Windows.Forms.Button btn_Lower;
        private System.Windows.Forms.GroupBox gb_ColumnPosition;
    }
}