namespace SIMCORE_TOOL.Simul8
{
    partial class Simul8SubForm
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
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.p_Simul8 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.t_Update = new System.Windows.Forms.Timer(this.components);
            this.cms_ContextMenuObject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.getResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.p_Simul8.SuspendLayout();
            this.cms_ContextMenuObject.SuspendLayout();
            this.SuspendLayout();
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.LargeChange = 200;
            this.hScrollBar1.Location = new System.Drawing.Point(1, 477);
            this.hScrollBar1.Maximum = 2048;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(916, 20);
            this.hScrollBar1.SmallChange = 50;
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.LargeChange = 200;
            this.vScrollBar1.Location = new System.Drawing.Point(917, 0);
            this.vScrollBar1.Maximum = 1536;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 477);
            this.vScrollBar1.SmallChange = 50;
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // p_Simul8
            // 
            this.p_Simul8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.p_Simul8.Controls.Add(this.button1);
            this.p_Simul8.Location = new System.Drawing.Point(0, 0);
            this.p_Simul8.Name = "p_Simul8";
            this.p_Simul8.Size = new System.Drawing.Size(917, 477);
            this.p_Simul8.TabIndex = 2;
            this.p_Simul8.Paint += new System.Windows.Forms.PaintEventHandler(this.p_Simul8_Paint);
            this.p_Simul8.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p_Simul8_MouseMove);
            this.p_Simul8.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.p_Simul8_MouseDoubleClick);
            this.p_Simul8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p_Simul8_MouseClick);
            this.p_Simul8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.p_Simul8_MouseDown);
            this.p_Simul8.Resize += new System.EventHandler(this.p_Simul8_Resize);
            this.p_Simul8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.p_Simul8_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // t_Update
            // 
            this.t_Update.Tick += new System.EventHandler(this.t_Update_Tick);
            // 
            // cms_ContextMenuObject
            // 
            this.cms_ContextMenuObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolStripMenuItem,
            this.toolStripMenuItem1,
            this.getResultsToolStripMenuItem,
            this.addToChartToolStripMenuItem,
            this.compareToolStripMenuItem});
            this.cms_ContextMenuObject.Name = "cms_ContextMenuObject";
            this.cms_ContextMenuObject.Size = new System.Drawing.Size(153, 120);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // getResultsToolStripMenuItem
            // 
            this.getResultsToolStripMenuItem.Name = "getResultsToolStripMenuItem";
            this.getResultsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.getResultsToolStripMenuItem.Text = "Get Results";
            // 
            // addToChartToolStripMenuItem
            // 
            this.addToChartToolStripMenuItem.Name = "addToChartToolStripMenuItem";
            this.addToChartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToChartToolStripMenuItem.Text = "Add to Chart";
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.compareToolStripMenuItem.Text = "Compare";
            // 
            // Simul8SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 499);
            this.Controls.Add(this.p_Simul8);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Simul8SubForm";
            this.Text = "Simul8SubForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Simul8SubForm_FormClosing);
            this.p_Simul8.ResumeLayout(false);
            this.cms_ContextMenuObject.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Panel p_Simul8;
        private System.Windows.Forms.Timer t_Update;
        private System.Windows.Forms.ContextMenuStrip cms_ContextMenuObject;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem getResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
    }
}