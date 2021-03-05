namespace SIMCORE_TOOL.Assistant
{
    partial class Ressource_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ressource_Assistant));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showArrowListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveChangesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.discardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cb_Origin = new System.Windows.Forms.ComboBox();
            this.vsb_Liste = new System.Windows.Forms.VScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.hsb_Graph = new System.Windows.Forms.HScrollBar();
            this.vsb_Graph = new System.Windows.Forms.VScrollBar();
            this.ofd_FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.modifyItineraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_Itinary_Level.gif");
            this.imageList1.Images.SetKeyName(1, "icon_Itinary_CheckIn.gif");
            this.imageList1.Images.SetKeyName(2, "icon_Itinary_Passeport.gif");
            this.imageList1.Images.SetKeyName(3, "icon_Itinary_Security.gif");
            this.imageList1.Images.SetKeyName(4, "icon_Itinary_Arrivalgate.gif");
            this.imageList1.Images.SetKeyName(5, "icon_Itinary_Baggage.gif");
            this.imageList1.Images.SetKeyName(6, "icon_Itinary_TransferDesk.gif");
            this.imageList1.Images.SetKeyName(7, "icon_Itinary_Shuttle.GIF");
            this.imageList1.Images.SetKeyName(8, "icon_Itinary_Boardinggate.gif");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.entitiesToolStripMenuItem,
            this.modifyItineraryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(691, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showArrowListToolStripMenuItem,
            this.changeBackgroundToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveChangesToolStripMenuItem1,
            this.discardToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.fileToolStripMenuItem.Text = "Tools";
            // 
            // showArrowListToolStripMenuItem
            // 
            this.showArrowListToolStripMenuItem.Checked = true;
            this.showArrowListToolStripMenuItem.CheckOnClick = true;
            this.showArrowListToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showArrowListToolStripMenuItem.Name = "showArrowListToolStripMenuItem";
            this.showArrowListToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.showArrowListToolStripMenuItem.Text = "Show connections list";
            this.showArrowListToolStripMenuItem.CheckedChanged += new System.EventHandler(this.showArrowListToolStripMenuItem_Click);
            this.showArrowListToolStripMenuItem.Click += new System.EventHandler(this.showArrowListToolStripMenuItem_Click);
            // 
            // changeBackgroundToolStripMenuItem
            // 
            this.changeBackgroundToolStripMenuItem.Name = "changeBackgroundToolStripMenuItem";
            this.changeBackgroundToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.changeBackgroundToolStripMenuItem.Text = "Change background";
            this.changeBackgroundToolStripMenuItem.Click += new System.EventHandler(this.changeBackgroundToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 6);
            // 
            // saveChangesToolStripMenuItem1
            // 
            this.saveChangesToolStripMenuItem1.Name = "saveChangesToolStripMenuItem1";
            this.saveChangesToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.saveChangesToolStripMenuItem1.Text = "Save changes";
            this.saveChangesToolStripMenuItem1.Click += new System.EventHandler(this.saveChangesToolStripMenuItem1_Click);
            // 
            // discardToolStripMenuItem
            // 
            this.discardToolStripMenuItem.Name = "discardToolStripMenuItem";
            this.discardToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.discardToolStripMenuItem.Text = "Exit";
            this.discardToolStripMenuItem.Visible = false;
            this.discardToolStripMenuItem.Click += new System.EventHandler(this.discardToolStripMenuItem_Click);
            // 
            // entitiesToolStripMenuItem
            // 
            this.entitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAllToolStripMenuItem,
            this.toolStripMenuItem2});
            this.entitiesToolStripMenuItem.Name = "entitiesToolStripMenuItem";
            this.entitiesToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.entitiesToolStripMenuItem.Text = "Entities";
            // 
            // addAllToolStripMenuItem
            // 
            this.addAllToolStripMenuItem.Name = "addAllToolStripMenuItem";
            this.addAllToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addAllToolStripMenuItem.Text = "Add all";
            this.addAllToolStripMenuItem.Click += new System.EventHandler(this.addAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(114, 6);
            // 
            // cb_Origin
            // 
            this.cb_Origin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Origin.FormattingEnabled = true;
            this.cb_Origin.Location = new System.Drawing.Point(0, 27);
            this.cb_Origin.Name = "cb_Origin";
            this.cb_Origin.Size = new System.Drawing.Size(216, 21);
            this.cb_Origin.TabIndex = 6;
            this.cb_Origin.SelectedIndexChanged += new System.EventHandler(this.cb_Origin_SelectedIndexChanged);
            // 
            // vsb_Liste
            // 
            this.vsb_Liste.Dock = System.Windows.Forms.DockStyle.Right;
            this.vsb_Liste.Enabled = false;
            this.vsb_Liste.LargeChange = 323;
            this.vsb_Liste.Location = new System.Drawing.Point(200, 0);
            this.vsb_Liste.Maximum = 700;
            this.vsb_Liste.Name = "vsb_Liste";
            this.vsb_Liste.Size = new System.Drawing.Size(16, 308);
            this.vsb_Liste.TabIndex = 8;
            this.vsb_Liste.TabStop = true;
            this.vsb_Liste.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsb_Liste_Scroll);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panel1.Controls.Add(this.vsb_Liste);
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 308);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.hsb_Graph);
            this.panel2.Controls.Add(this.vsb_Graph);
            this.panel2.Location = new System.Drawing.Point(219, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 332);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.LightGray;
            this.panel3.Location = new System.Drawing.Point(452, 314);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 18);
            this.panel3.TabIndex = 5;
            // 
            // hsb_Graph
            // 
            this.hsb_Graph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hsb_Graph.LargeChange = 300;
            this.hsb_Graph.Location = new System.Drawing.Point(0, 314);
            this.hsb_Graph.Maximum = 4096;
            this.hsb_Graph.Name = "hsb_Graph";
            this.hsb_Graph.Size = new System.Drawing.Size(452, 18);
            this.hsb_Graph.SmallChange = 100;
            this.hsb_Graph.TabIndex = 4;
            this.hsb_Graph.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsb_Graph_Scroll);
            // 
            // vsb_Graph
            // 
            this.vsb_Graph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vsb_Graph.LargeChange = 300;
            this.vsb_Graph.Location = new System.Drawing.Point(452, 0);
            this.vsb_Graph.Maximum = 2048;
            this.vsb_Graph.Name = "vsb_Graph";
            this.vsb_Graph.Size = new System.Drawing.Size(20, 314);
            this.vsb_Graph.SmallChange = 100;
            this.vsb_Graph.TabIndex = 3;
            this.vsb_Graph.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsb_Graph_Scroll);
            // 
            // ofd_FileDialog
            // 
            this.ofd_FileDialog.Filter = "Image (*.jpg,*.jpeg,*.bmp,*.gif,*.png)|*.jpg;*.jpeg;*.bmp;*.gif;*.png";
            // 
            // modifyItineraryToolStripMenuItem
            // 
            this.modifyItineraryToolStripMenuItem.Name = "modifyItineraryToolStripMenuItem";
            this.modifyItineraryToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.modifyItineraryToolStripMenuItem.Text = "Modify itinerary";
            this.modifyItineraryToolStripMenuItem.Click += new System.EventHandler(this.modifyItineraryToolStripMenuItem_Click);
            // 
            // Ressource_Assistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ClientSize = new System.Drawing.Size(691, 358);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cb_Origin);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(265, 70);
            this.Name = "Ressource_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ressource_Assistant";
            this.SizeChanged += new System.EventHandler(this.Ressource_Assistant_Resize);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ressource_Assistant_FormClosing);
            this.Resize += new System.EventHandler(this.Ressource_Assistant_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem entitiesToolStripMenuItem;
        private System.Windows.Forms.ComboBox cb_Origin;
        private System.Windows.Forms.VScrollBar vsb_Liste;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.HScrollBar hsb_Graph;
        private System.Windows.Forms.VScrollBar vsb_Graph;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showArrowListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveChangesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem changeBackgroundToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofd_FileDialog;
        private System.Windows.Forms.ToolStripMenuItem modifyItineraryToolStripMenuItem;
    }
}