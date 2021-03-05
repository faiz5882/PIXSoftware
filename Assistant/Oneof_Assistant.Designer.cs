namespace SIMCORE_TOOL.Assistant
{
    partial class Oneof_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Oneof_Assistant));
            this.LBL_Name = new System.Windows.Forms.Label();
            this.BTN_AddDistrib = new System.Windows.Forms.Button();
            this.BTN_DeleteAll = new System.Windows.Forms.Button();
            this.BTN_Ok = new System.Windows.Forms.Button();
            this.BTN_Delete = new System.Windows.Forms.Button();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.BTN_AddFrequency = new System.Windows.Forms.Button();
            this.DGV_Table = new System.Windows.Forms.DataGridView();
            this.CB_DistribName = new System.Windows.Forms.ComboBox();
            this.BTN_Edit = new System.Windows.Forms.Button();
            this.nccGraphique = new Nevron.Chart.WinForm.NChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Table)).BeginInit();
            this.SuspendLayout();
            // 
            // LBL_Name
            // 
            this.LBL_Name.AutoSize = true;
            this.LBL_Name.BackColor = System.Drawing.Color.Transparent;
            this.LBL_Name.Location = new System.Drawing.Point(23, 38);
            this.LBL_Name.Name = "LBL_Name";
            this.LBL_Name.Size = new System.Drawing.Size(94, 13);
            this.LBL_Name.TabIndex = 1;
            this.LBL_Name.Text = "Distribution name :";
            // 
            // BTN_AddDistrib
            // 
            this.BTN_AddDistrib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_AddDistrib.Location = new System.Drawing.Point(560, 33);
            this.BTN_AddDistrib.Name = "BTN_AddDistrib";
            this.BTN_AddDistrib.Size = new System.Drawing.Size(61, 23);
            this.BTN_AddDistrib.TabIndex = 2;
            this.BTN_AddDistrib.Text = "Add distribution";
            this.BTN_AddDistrib.UseVisualStyleBackColor = true;
            this.BTN_AddDistrib.Click += new System.EventHandler(this.BTN_AddDistrib_Click);
            // 
            // BTN_DeleteAll
            // 
            this.BTN_DeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_DeleteAll.Enabled = false;
            this.BTN_DeleteAll.Location = new System.Drawing.Point(560, 205);
            this.BTN_DeleteAll.Name = "BTN_DeleteAll";
            this.BTN_DeleteAll.Size = new System.Drawing.Size(61, 23);
            this.BTN_DeleteAll.TabIndex = 7;
            this.BTN_DeleteAll.Text = "Delete All";
            this.BTN_DeleteAll.UseVisualStyleBackColor = true;
            this.BTN_DeleteAll.Click += new System.EventHandler(this.BTN_DeleteAll_Click);
            // 
            // BTN_Ok
            // 
            this.BTN_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BTN_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_Ok.Location = new System.Drawing.Point(54, 302);
            this.BTN_Ok.Name = "BTN_Ok";
            this.BTN_Ok.Size = new System.Drawing.Size(84, 34);
            this.BTN_Ok.TabIndex = 8;
            this.BTN_Ok.Text = "&Ok";
            this.BTN_Ok.UseVisualStyleBackColor = true;
            this.BTN_Ok.Click += new System.EventHandler(this.BTN_Ok_Click);
            // 
            // BTN_Delete
            // 
            this.BTN_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Delete.Enabled = false;
            this.BTN_Delete.Location = new System.Drawing.Point(560, 176);
            this.BTN_Delete.Name = "BTN_Delete";
            this.BTN_Delete.Size = new System.Drawing.Size(61, 23);
            this.BTN_Delete.TabIndex = 6;
            this.BTN_Delete.Text = "Delete";
            this.BTN_Delete.UseVisualStyleBackColor = true;
            this.BTN_Delete.Click += new System.EventHandler(this.BTN_Delete_Click);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_Cancel.Location = new System.Drawing.Point(509, 302);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(84, 34);
            this.BTN_Cancel.TabIndex = 9;
            this.BTN_Cancel.Text = "&Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            // 
            // BTN_AddFrequency
            // 
            this.BTN_AddFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_AddFrequency.Enabled = false;
            this.BTN_AddFrequency.Location = new System.Drawing.Point(560, 99);
            this.BTN_AddFrequency.Name = "BTN_AddFrequency";
            this.BTN_AddFrequency.Size = new System.Drawing.Size(61, 23);
            this.BTN_AddFrequency.TabIndex = 4;
            this.BTN_AddFrequency.Text = "Add";
            this.BTN_AddFrequency.UseVisualStyleBackColor = true;
            this.BTN_AddFrequency.Click += new System.EventHandler(this.BTN_AddFrequency_Click);
            // 
            // DGV_Table
            // 
            this.DGV_Table.AllowUserToAddRows = false;
            this.DGV_Table.AllowUserToDeleteRows = false;
            this.DGV_Table.AllowUserToResizeColumns = false;
            this.DGV_Table.AllowUserToResizeRows = false;
            this.DGV_Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV_Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Table.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGV_Table.Enabled = false;
            this.DGV_Table.Location = new System.Drawing.Point(270, 85);
            this.DGV_Table.Name = "DGV_Table";
            this.DGV_Table.RowHeadersVisible = false;
            this.DGV_Table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Table.Size = new System.Drawing.Size(266, 179);
            this.DGV_Table.TabIndex = 3;
            this.DGV_Table.SelectionChanged += new System.EventHandler(this.DGV_Table_SelectionChanged);
            // 
            // CB_DistribName
            // 
            this.CB_DistribName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_DistribName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_DistribName.FormattingEnabled = true;
            this.CB_DistribName.Location = new System.Drawing.Point(123, 35);
            this.CB_DistribName.Name = "CB_DistribName";
            this.CB_DistribName.Size = new System.Drawing.Size(431, 21);
            this.CB_DistribName.TabIndex = 1;
            this.CB_DistribName.SelectedIndexChanged += new System.EventHandler(this.CB_DistribName_SelectedIndexChanged);
            // 
            // BTN_Edit
            // 
            this.BTN_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Edit.Enabled = false;
            this.BTN_Edit.Location = new System.Drawing.Point(560, 128);
            this.BTN_Edit.Name = "BTN_Edit";
            this.BTN_Edit.Size = new System.Drawing.Size(61, 23);
            this.BTN_Edit.TabIndex = 5;
            this.BTN_Edit.Text = "Edit";
            this.BTN_Edit.UseVisualStyleBackColor = true;
            this.BTN_Edit.Click += new System.EventHandler(this.BTN_Edit_Click);
            // 
            // nccGraphique
            // 
            this.nccGraphique.AutoRefresh = false;
            this.nccGraphique.BackColor = System.Drawing.SystemColors.Control;
            this.nccGraphique.InputKeys = new System.Windows.Forms.Keys[0];
            this.nccGraphique.Location = new System.Drawing.Point(26, 85);
            this.nccGraphique.Name = "nccGraphique";
            this.nccGraphique.Size = new System.Drawing.Size(238, 179);
            this.nccGraphique.State = ((Nevron.Chart.WinForm.NState)(resources.GetObject("nccGraphique.State")));
            this.nccGraphique.TabIndex = 12;
            this.nccGraphique.Text = "nChartControl1";
            // 
            // Oneof_Assistant
            // 
            this.AcceptButton = this.BTN_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BTN_Cancel;
            this.ClientSize = new System.Drawing.Size(657, 348);
            this.Controls.Add(this.nccGraphique);
            this.Controls.Add(this.BTN_Edit);
            this.Controls.Add(this.CB_DistribName);
            this.Controls.Add(this.DGV_Table);
            this.Controls.Add(this.BTN_AddFrequency);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.BTN_Delete);
            this.Controls.Add(this.BTN_Ok);
            this.Controls.Add(this.BTN_DeleteAll);
            this.Controls.Add(this.BTN_AddDistrib);
            this.Controls.Add(this.LBL_Name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(665, 382);
            this.Name = "Oneof_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Define customized probability profiles";
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_Name;
        private System.Windows.Forms.Button BTN_AddDistrib;
        private System.Windows.Forms.Button BTN_DeleteAll;
        private System.Windows.Forms.Button BTN_Ok;
        private System.Windows.Forms.Button BTN_Delete;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Button BTN_AddFrequency;
        private System.Windows.Forms.DataGridView DGV_Table;
        private System.Windows.Forms.ComboBox CB_DistribName;
        private System.Windows.Forms.Button BTN_Edit;
        private Nevron.Chart.WinForm.NChartControl nccGraphique;
    }
}