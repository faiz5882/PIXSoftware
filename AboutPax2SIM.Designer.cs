namespace SIMCORE_TOOL
{
    partial class AboutPax2SIM
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutPax2SIM));
            this.okButton = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.cms_AboutMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_SendMail = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsi_CopyLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsi_CopySupport = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_AboutMenu.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.tableLayoutPanel2.SetRowSpan(this.okButton, 2);
            // 
            // textBoxDescription
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.textBoxDescription, 2);
            this.textBoxDescription.ContextMenuStrip = this.cms_AboutMenu;
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Click += new System.EventHandler(this.textBoxDescription_Click);
            // 
            // cms_AboutMenu
            // 
            this.cms_AboutMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_SendMail,
            this.tmsi_CopyLink,
            this.tmsi_CopySupport});
            this.cms_AboutMenu.Name = "cms_AboutMenu";
            resources.ApplyResources(this.cms_AboutMenu, "cms_AboutMenu");
            // 
            // tsmi_SendMail
            // 
            this.tsmi_SendMail.Name = "tsmi_SendMail";
            resources.ApplyResources(this.tsmi_SendMail, "tsmi_SendMail");
            this.tsmi_SendMail.Click += new System.EventHandler(this.tsmi_SendMail_Click);
            // 
            // tmsi_CopyLink
            // 
            this.tmsi_CopyLink.Name = "tmsi_CopyLink";
            resources.ApplyResources(this.tmsi_CopyLink, "tmsi_CopyLink");
            this.tmsi_CopyLink.Click += new System.EventHandler(this.tmsi_CopyLink_Click);
            // 
            // tmsi_CopySupport
            // 
            this.tmsi_CopySupport.Name = "tmsi_CopySupport";
            resources.ApplyResources(this.tmsi_CopySupport, "tmsi_CopySupport");
            this.tmsi_CopySupport.Click += new System.EventHandler(this.tmsi_CopySupport_Click);
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelCompanyName, "labelCompanyName");
            this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCompanyName.Name = "labelCompanyName";
            // 
            // labelCopyright
            // 
            this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelCopyright, "labelCopyright");
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            // 
            // labelProductName
            // 
            resources.ApplyResources(this.labelProductName, "labelProductName");
            this.labelProductName.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName.Name = "labelProductName";
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Name = "labelVersion";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.labelCopyright, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.labelCompanyName, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.labelVersion, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelProductName, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.okButton, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.logoPictureBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxDescription, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 7);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.SetColumnSpan(this.logoPictureBox, 2);
            this.logoPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.logoPictureBox, "logoPictureBox");
            this.logoPictureBox.Image = global::SIMCORE_TOOL.Properties.Resources.logohub_performance_800x400;
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.Click += new System.EventHandler(this.logoPictureBox_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.SetColumnSpan(this.pictureBox1, 2);
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::SIMCORE_TOOL.Properties.Resources.Bande_Logo_PAX2SIM_20131;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdatesToolStripMenuItem, "checkForUpdatesToolStripMenuItem");
            // 
            // AboutPax2SIM
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutPax2SIM";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.cms_AboutMenu.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip cms_AboutMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SendMail;
        private System.Windows.Forms.ToolStripMenuItem tmsi_CopyLink;
        private System.Windows.Forms.ToolStripMenuItem tmsi_CopySupport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;

    }
}
