namespace SIMCORE_TOOL.Simul8
{
    partial class Simul8ControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Simul8ControlPanel));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_Reset = new System.Windows.Forms.ToolStripButton();
            this.tsb_StartStop = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowHideLinks = new System.Windows.Forms.ToolStripButton();
            this.LinkMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsl_Zoom = new System.Windows.Forms.ToolStripLabel();
            this.tscb_Zoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsl_Speed = new System.Windows.Forms.ToolStripLabel();
            this.tsb_Speed = new System.Windows.Forms.ToolStripProgressBar();
            this.tsb_Analyse = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsl_Time = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.ts_Element = new System.Windows.Forms.ToolStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer2.ContentPanel.SuspendLayout();
            this.toolStripContainer2.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Reset,
            this.tsb_StartStop,
            this.tsb_ShowHideLinks,
            this.LinkMode,
            this.toolStripSeparator1,
            this.tsl_Zoom,
            this.tscb_Zoom,
            this.toolStripSeparator2,
            this.tsl_Speed,
            this.tsb_Speed,
            this.tsb_Analyse,
            this.tsb_Cancel,
            this.tsl_Time});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(578, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_Reset
            // 
            this.tsb_Reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_Reset.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Reset.Image")));
            this.tsb_Reset.ImageTransparentColor = System.Drawing.Color.White;
            this.tsb_Reset.Name = "tsb_Reset";
            this.tsb_Reset.Size = new System.Drawing.Size(23, 22);
            this.tsb_Reset.Text = "Reset";
            this.tsb_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // tsb_StartStop
            // 
            this.tsb_StartStop.CheckOnClick = true;
            this.tsb_StartStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_StartStop.Image = ((System.Drawing.Image)(resources.GetObject("tsb_StartStop.Image")));
            this.tsb_StartStop.ImageTransparentColor = System.Drawing.Color.White;
            this.tsb_StartStop.Name = "tsb_StartStop";
            this.tsb_StartStop.Size = new System.Drawing.Size(23, 22);
            this.tsb_StartStop.Text = "Start/Stop";
            this.tsb_StartStop.Click += new System.EventHandler(this.cb_StartStop_Click);
            // 
            // tsb_ShowHideLinks
            // 
            this.tsb_ShowHideLinks.CheckOnClick = true;
            this.tsb_ShowHideLinks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ShowHideLinks.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowHideLinks.Image")));
            this.tsb_ShowHideLinks.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsb_ShowHideLinks.Name = "tsb_ShowHideLinks";
            this.tsb_ShowHideLinks.Size = new System.Drawing.Size(23, 22);
            this.tsb_ShowHideLinks.Text = "Show / Hide links";
            this.tsb_ShowHideLinks.Click += new System.EventHandler(this.cb_Showlinks_CheckedChanged);
            // 
            // LinkMode
            // 
            this.LinkMode.CheckOnClick = true;
            this.LinkMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LinkMode.Image = global::SIMCORE_TOOL.Properties.Resources.CreateLink3;
            this.LinkMode.ImageTransparentColor = System.Drawing.Color.Silver;
            this.LinkMode.Name = "LinkMode";
            this.LinkMode.Size = new System.Drawing.Size(23, 22);
            this.LinkMode.Text = "Create Links";
            this.LinkMode.Click += new System.EventHandler(this.LinkMode_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsl_Zoom
            // 
            this.tsl_Zoom.Name = "tsl_Zoom";
            this.tsl_Zoom.Size = new System.Drawing.Size(33, 22);
            this.tsl_Zoom.Text = "Zoom";
            // 
            // tscb_Zoom
            // 
            this.tscb_Zoom.Items.AddRange(new object[] {
            "10",
            "25",
            "50",
            "75",
            "100",
            "125",
            "150",
            "200",
            "300",
            "400"});
            this.tscb_Zoom.Name = "tscb_Zoom";
            this.tscb_Zoom.Size = new System.Drawing.Size(75, 25);
            this.tscb_Zoom.SelectedIndexChanged += new System.EventHandler(this.cb_Zoom_Leave);
            this.tscb_Zoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tscb_Zoom_KeyDown);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsl_Speed
            // 
            this.tsl_Speed.Name = "tsl_Speed";
            this.tsl_Speed.Size = new System.Drawing.Size(37, 22);
            this.tsl_Speed.Text = "Speed";
            // 
            // tsb_Speed
            // 
            this.tsb_Speed.Name = "tsb_Speed";
            this.tsb_Speed.Size = new System.Drawing.Size(100, 22);
            this.tsb_Speed.Value = 20;
            this.tsb_Speed.Paint += new System.Windows.Forms.PaintEventHandler(this.tsb_Speed_Paint);
            // 
            // tsb_Analyse
            // 
            this.tsb_Analyse.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Analyse.Image")));
            this.tsb_Analyse.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsb_Analyse.Name = "tsb_Analyse";
            this.tsb_Analyse.Size = new System.Drawing.Size(98, 22);
            this.tsb_Analyse.Text = "Analyse traces";
            this.tsb_Analyse.Visible = false;
            this.tsb_Analyse.Click += new System.EventHandler(this.tsb_Analyse_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.White;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(100, 22);
            this.tsb_Cancel.Text = "Cancel analysis";
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // tsl_Time
            // 
            this.tsl_Time.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsl_Time.Name = "tsl_Time";
            this.tsl_Time.Size = new System.Drawing.Size(113, 22);
            this.tsl_Time.Text = "01/01/2010  00:00:00";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 473);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Controls.Add(this.panel1);
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(748, 473);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer2.LeftToolStripPanel
            // 
            this.toolStripContainer2.LeftToolStripPanel.Controls.Add(this.ts_Element);
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(774, 498);
            this.toolStripContainer2.TabIndex = 2;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer2.TopToolStripPanel
            // 
            this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // ts_Element
            // 
            this.ts_Element.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ts_Element.Dock = System.Windows.Forms.DockStyle.None;
            this.ts_Element.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_Element.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Element.Location = new System.Drawing.Point(0, 3);
            this.ts_Element.Name = "ts_Element";
            this.ts_Element.Size = new System.Drawing.Size(26, 102);
            this.ts_Element.TabIndex = 0;
            this.ts_Element.Tag = "This toolbar will receive all the element that can be dropped on the Simul8 windo" +
                "w";
            this.ts_Element.Text = "Element";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Level");
            this.imageList1.Images.SetKeyName(1, "Check In");
            this.imageList1.Images.SetKeyName(2, "Passport");
            this.imageList1.Images.SetKeyName(3, "Security");
            this.imageList1.Images.SetKeyName(4, "Arrival Gate");
            this.imageList1.Images.SetKeyName(5, "Baggage Claim");
            this.imageList1.Images.SetKeyName(6, "Transfer");
            this.imageList1.Images.SetKeyName(7, "Shuttle");
            this.imageList1.Images.SetKeyName(8, "Boarding Gate");
            // 
            // Simul8ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 498);
            this.Controls.Add(this.toolStripContainer2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Simul8ControlPanel";
            this.Text = "Simul8ControlPanel";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer2.ContentPanel.ResumeLayout(false);
            this.toolStripContainer2.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_StartStop;
        private System.Windows.Forms.ToolStripButton tsb_Reset;
        private System.Windows.Forms.ToolStripComboBox tscb_Zoom;
        private System.Windows.Forms.ToolStripButton tsb_ShowHideLinks;
        private System.Windows.Forms.ToolStripLabel tsl_Time;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tsl_Zoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tsl_Speed;
        private System.Windows.Forms.ToolStripProgressBar tsb_Speed;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton tsb_Analyse;
        private System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.ToolStrip ts_Element;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton LinkMode;
    }
}