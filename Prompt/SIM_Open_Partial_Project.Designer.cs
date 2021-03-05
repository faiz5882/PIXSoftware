namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Open_Partial_Project
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Open_Partial_Project));
            this.tv_Reports = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.lbl_title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tv_Reports
            // 
            this.tv_Reports.CheckBoxes = true;
            this.tv_Reports.Location = new System.Drawing.Point(12, 39);
            this.tv_Reports.Name = "tv_Reports";
            this.tv_Reports.Size = new System.Drawing.Size(268, 200);
            this.tv_Reports.TabIndex = 1;
            this.tv_Reports.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_Reports_AfterCheck);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "icon_Treeview_Airport.gif");
            this.imageList1.Images.SetKeyName(2, "icon_Treeview_Terminal.gif");
            this.imageList1.Images.SetKeyName(3, "Icon_Treeview_Level.gif");
            this.imageList1.Images.SetKeyName(4, "icon_Treeview_Checkin.gif");
            this.imageList1.Images.SetKeyName(5, "icon_Treeview_Police.gif");
            this.imageList1.Images.SetKeyName(6, "icon_Treeview_Security.gif");
            this.imageList1.Images.SetKeyName(7, "icon_Treeview_ArrivalGate.gif");
            this.imageList1.Images.SetKeyName(8, "icon_Treeview_Baggage.gif");
            this.imageList1.Images.SetKeyName(9, "icon_Treeview_TransferDesk.gif");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "Icon_Treeview_DepartureGate.gif");
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            this.imageList1.Images.SetKeyName(14, "");
            this.imageList1.Images.SetKeyName(15, "");
            this.imageList1.Images.SetKeyName(16, "");
            this.imageList1.Images.SetKeyName(17, "");
            this.imageList1.Images.SetKeyName(18, "");
            this.imageList1.Images.SetKeyName(19, "");
            this.imageList1.Images.SetKeyName(20, "");
            this.imageList1.Images.SetKeyName(21, "Graph.bmp");
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(187, 257);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(88, 25);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Import
            // 
            this.btn_Import.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Import.Location = new System.Drawing.Point(12, 257);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(95, 25);
            this.btn_Import.TabIndex = 5;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Location = new System.Drawing.Point(12, 9);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(180, 13);
            this.lbl_title.TabIndex = 7;
            this.lbl_title.Text = "Select the Tables/Scenario to import";
            // 
            // SIM_Open_Partial_Project
            // 
            this.AcceptButton = this.btn_Import;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(292, 297);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.tv_Reports);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SIM_Open_Partial_Project";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open partial project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_Reports;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Label lbl_title;
    }
}