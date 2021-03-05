namespace SIMCORE_TOOL.Interface
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.p_Content = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.ts_Help = new System.Windows.Forms.ToolStrip();
            this.tsb_HideHelp = new System.Windows.Forms.ToolStripButton();
            this.tsb_HelpInInternWindow = new System.Windows.Forms.ToolStripButton();
            this.tsb_HelpInExternWindow = new System.Windows.Forms.ToolStripButton();
            this.tsb_SeeHelpOnBrowser = new System.Windows.Forms.ToolStripButton();
            this.tsb_HelpOnline = new System.Windows.Forms.ToolStripButton();
            this.tsb_SendEmail = new System.Windows.Forms.ToolStripButton();
            this.p_Content.SuspendLayout();
            this.ts_Help.SuspendLayout();
            this.SuspendLayout();
            // 
            // p_Content
            // 
            this.p_Content.Controls.Add(this.webBrowser1);
            this.p_Content.Controls.Add(this.ts_Help);
            resources.ApplyResources(this.p_Content, "p_Content");
            this.p_Content.Name = "p_Content";
            // 
            // webBrowser1
            // 
            resources.ApplyResources(this.webBrowser1, "webBrowser1");
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            // 
            // ts_Help
            // 
            this.ts_Help.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_HideHelp,
            this.tsb_HelpInInternWindow,
            this.tsb_HelpInExternWindow,
            this.tsb_SeeHelpOnBrowser,
            this.tsb_HelpOnline,
            this.tsb_SendEmail});
            resources.ApplyResources(this.ts_Help, "ts_Help");
            this.ts_Help.Name = "ts_Help";
            // 
            // tsb_HideHelp
            // 
            this.tsb_HideHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_HideHelp.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            resources.ApplyResources(this.tsb_HideHelp, "tsb_HideHelp");
            this.tsb_HideHelp.Name = "tsb_HideHelp";
            this.tsb_HideHelp.Click += new System.EventHandler(this.tsb_HideHelp_Click);
            // 
            // tsb_HelpInInternWindow
            // 
            this.tsb_HelpInInternWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_HelpInInternWindow.Image = global::SIMCORE_TOOL.Properties.Resources.Intern;
            resources.ApplyResources(this.tsb_HelpInInternWindow, "tsb_HelpInInternWindow");
            this.tsb_HelpInInternWindow.Name = "tsb_HelpInInternWindow";
            this.tsb_HelpInInternWindow.Click += new System.EventHandler(this.tsb_HelpInInternWindow_Click);
            // 
            // tsb_HelpInExternWindow
            // 
            this.tsb_HelpInExternWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_HelpInExternWindow.Image = global::SIMCORE_TOOL.Properties.Resources._extern;
            resources.ApplyResources(this.tsb_HelpInExternWindow, "tsb_HelpInExternWindow");
            this.tsb_HelpInExternWindow.Name = "tsb_HelpInExternWindow";
            this.tsb_HelpInExternWindow.Click += new System.EventHandler(this.tsb_HelpInExternWindow_Click);
            // 
            // tsb_SeeHelpOnBrowser
            // 
            this.tsb_SeeHelpOnBrowser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_SeeHelpOnBrowser, "tsb_SeeHelpOnBrowser");
            this.tsb_SeeHelpOnBrowser.Name = "tsb_SeeHelpOnBrowser";
            this.tsb_SeeHelpOnBrowser.Click += new System.EventHandler(this.tsb_SeeHelpOnBrowser_Click);
            // 
            // tsb_HelpOnline
            // 
            this.tsb_HelpOnline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_HelpOnline, "tsb_HelpOnline");
            this.tsb_HelpOnline.Name = "tsb_HelpOnline";
            this.tsb_HelpOnline.Click += new System.EventHandler(this.tsb_HelpOnline_Click);
            // 
            // tsb_SendEmail
            // 
            this.tsb_SendEmail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_SendEmail.Image = global::SIMCORE_TOOL.Properties.Resources.SendMail;
            resources.ApplyResources(this.tsb_SendEmail, "tsb_SendEmail");
            this.tsb_SendEmail.Name = "tsb_SendEmail";
            this.tsb_SendEmail.Click += new System.EventHandler(this.tsb_SendEmail_Click);
            // 
            // Help
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p_Content);
            this.KeyPreview = true;
            this.Name = "Help";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Help_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Help_KeyDown);
            this.p_Content.ResumeLayout(false);
            this.p_Content.PerformLayout();
            this.ts_Help.ResumeLayout(false);
            this.ts_Help.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel p_Content;
        private System.Windows.Forms.ToolStrip ts_Help;
        private System.Windows.Forms.ToolStripButton tsb_HideHelp;
        private System.Windows.Forms.ToolStripButton tsb_HelpInExternWindow;
        private System.Windows.Forms.ToolStripButton tsb_SeeHelpOnBrowser;
        private System.Windows.Forms.ToolStripButton tsb_HelpOnline;
        private System.Windows.Forms.ToolStripButton tsb_HelpInInternWindow;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripButton tsb_SendEmail;
    }
}