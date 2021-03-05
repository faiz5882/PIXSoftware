namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Import_Table
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Import_Table));
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.tv_Reports = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(187, 230);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(88, 25);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Import
            // 
            this.btn_Import.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Import.Enabled = false;
            this.btn_Import.Location = new System.Drawing.Point(12, 230);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(95, 25);
            this.btn_Import.TabIndex = 8;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            // 
            // tv_Reports
            // 
            this.tv_Reports.Location = new System.Drawing.Point(12, 12);
            this.tv_Reports.Name = "tv_Reports";
            this.tv_Reports.Size = new System.Drawing.Size(268, 200);
            this.tv_Reports.TabIndex = 7;
            this.tv_Reports.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_Reports_AfterSelect);
            // 
            // SIM_Import_Table
            // 
            this.AcceptButton = this.btn_Import;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.tv_Reports);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SIM_Import_Table";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select the table";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.TreeView tv_Reports;
    }
}