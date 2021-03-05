namespace SIMCORE_TOOL.Prompt
{
    partial class IgnoredLigne
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IgnoredLigne));
            this.dgw_Table = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgw_Table)).BeginInit();
            this.SuspendLayout();
            // 
            // dgw_Table
            // 
            this.dgw_Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw_Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgw_Table.Location = new System.Drawing.Point(0, 0);
            this.dgw_Table.Name = "dgw_Table";
            this.dgw_Table.ReadOnly = true;
            this.dgw_Table.Size = new System.Drawing.Size(879, 510);
            this.dgw_Table.TabIndex = 0;
            // 
            // IgnoredLigne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 510);
            this.Controls.Add(this.dgw_Table);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IgnoredLigne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ignored lignes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IgnoredLigne_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgw_Table)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgw_Table;
    }
}