namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Htmleditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Htmleditor));
            this.HTMLEditor = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.EditionMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.CutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.copyButton = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.RedoBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.imageButton = new System.Windows.Forms.ToolStripMenuItem();
            this.LinkButton = new System.Windows.Forms.ToolStripMenuItem();
            this.HorizontalRuleBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.TableBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.BoldButton = new System.Windows.Forms.ToolStripButton();
            this.ItalicButton = new System.Windows.Forms.ToolStripButton();
            this.UnderlineButton = new System.Windows.Forms.ToolStripButton();
            this.StrikeThroughButton = new System.Windows.Forms.ToolStripButton();
            this.StyleCB = new System.Windows.Forms.ToolStripComboBox();
            this.SizeCB = new System.Windows.Forms.ToolStripComboBox();
            this.TextColorBtn = new System.Windows.Forms.ToolStripButton();
            this.BackColorBtn = new System.Windows.Forms.ToolStripButton();
            this.JustifyLeftBtn = new System.Windows.Forms.ToolStripButton();
            this.JustifyCenterBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ChipBtn = new System.Windows.Forms.ToolStripButton();
            this.IndentsBtn = new System.Windows.Forms.ToolStripButton();
            this.OutdentBtn = new System.Windows.Forms.ToolStripButton();
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HTMLEditor
            // 
            this.HTMLEditor.AllowNavigation = false;
            this.HTMLEditor.AllowWebBrowserDrop = false;
            this.HTMLEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HTMLEditor.IsWebBrowserContextMenuEnabled = false;
            this.HTMLEditor.Location = new System.Drawing.Point(0, 0);
            this.HTMLEditor.MinimumSize = new System.Drawing.Size(20, 20);
            this.HTMLEditor.Name = "HTMLEditor";
            this.HTMLEditor.ScriptErrorsSuppressed = true;
            this.HTMLEditor.Size = new System.Drawing.Size(539, 330);
            this.HTMLEditor.TabIndex = 0;
            this.HTMLEditor.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.HTMLEditor_PreviewKeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditionMenu,
            this.InsertButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(539, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "Size";
            // 
            // EditionMenu
            // 
            this.EditionMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EditionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CutButton,
            this.copyButton,
            this.pasteButton,
            this.UndoBtn,
            this.RedoBtn});
            this.EditionMenu.Image = ((System.Drawing.Image)(resources.GetObject("EditionMenu.Image")));
            this.EditionMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditionMenu.Name = "EditionMenu";
            this.EditionMenu.Size = new System.Drawing.Size(57, 22);
            this.EditionMenu.Text = "Edition";
            // 
            // CutButton
            // 
            this.CutButton.Image = ((System.Drawing.Image)(resources.GetObject("CutButton.Image")));
            this.CutButton.Name = "CutButton";
            this.CutButton.Size = new System.Drawing.Size(103, 22);
            this.CutButton.Text = "Cut";
            this.CutButton.Click += new System.EventHandler(this.CutButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.Image = ((System.Drawing.Image)(resources.GetObject("copyButton.Image")));
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(103, 22);
            this.copyButton.Text = "Copy";
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // pasteButton
            // 
            this.pasteButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteButton.Image")));
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(103, 22);
            this.pasteButton.Text = "Paste";
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // UndoBtn
            // 
            this.UndoBtn.Image = ((System.Drawing.Image)(resources.GetObject("UndoBtn.Image")));
            this.UndoBtn.Name = "UndoBtn";
            this.UndoBtn.Size = new System.Drawing.Size(103, 22);
            this.UndoBtn.Text = "Undo";
            this.UndoBtn.Click += new System.EventHandler(this.UndoBtn_Click);
            // 
            // RedoBtn
            // 
            this.RedoBtn.Image = ((System.Drawing.Image)(resources.GetObject("RedoBtn.Image")));
            this.RedoBtn.Name = "RedoBtn";
            this.RedoBtn.Size = new System.Drawing.Size(103, 22);
            this.RedoBtn.Text = "Redo";
            this.RedoBtn.Click += new System.EventHandler(this.RedoBtn_Click);
            // 
            // InsertButton
            // 
            this.InsertButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.InsertButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageButton,
            this.LinkButton,
            this.HorizontalRuleBtn,
            this.TableBtn});
            this.InsertButton.Image = ((System.Drawing.Image)(resources.GetObject("InsertButton.Image")));
            this.InsertButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(49, 22);
            this.InsertButton.Text = "Insert";
            // 
            // imageButton
            // 
            this.imageButton.Image = ((System.Drawing.Image)(resources.GetObject("imageButton.Image")));
            this.imageButton.Name = "imageButton";
            this.imageButton.Size = new System.Drawing.Size(155, 22);
            this.imageButton.Text = "Image";
            this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
            // 
            // LinkButton
            // 
            this.LinkButton.Image = ((System.Drawing.Image)(resources.GetObject("LinkButton.Image")));
            this.LinkButton.Name = "LinkButton";
            this.LinkButton.Size = new System.Drawing.Size(155, 22);
            this.LinkButton.Text = "Link";
            this.LinkButton.Click += new System.EventHandler(this.LinkButton_Click);
            // 
            // HorizontalRuleBtn
            // 
            this.HorizontalRuleBtn.Image = ((System.Drawing.Image)(resources.GetObject("HorizontalRuleBtn.Image")));
            this.HorizontalRuleBtn.Name = "HorizontalRuleBtn";
            this.HorizontalRuleBtn.Size = new System.Drawing.Size(155, 22);
            this.HorizontalRuleBtn.Text = "Horizontal Rule";
            this.HorizontalRuleBtn.Visible = false;
            this.HorizontalRuleBtn.Click += new System.EventHandler(this.HorizontalRuleBtn_Click);
            // 
            // TableBtn
            // 
            this.TableBtn.Image = ((System.Drawing.Image)(resources.GetObject("TableBtn.Image")));
            this.TableBtn.Name = "TableBtn";
            this.TableBtn.Size = new System.Drawing.Size(155, 22);
            this.TableBtn.Text = "Table";
            this.TableBtn.Visible = false;
            this.TableBtn.Click += new System.EventHandler(this.TableBtn_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BoldButton,
            this.ItalicButton,
            this.UnderlineButton,
            this.StrikeThroughButton,
            this.StyleCB,
            this.SizeCB,
            this.TextColorBtn,
            this.BackColorBtn,
            this.JustifyLeftBtn,
            this.JustifyCenterBtn,
            this.toolStripButton1,
            this.ChipBtn,
            this.IndentsBtn,
            this.OutdentBtn});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(539, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // BoldButton
            // 
            this.BoldButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BoldButton.Image = ((System.Drawing.Image)(resources.GetObject("BoldButton.Image")));
            this.BoldButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BoldButton.Name = "BoldButton";
            this.BoldButton.Size = new System.Drawing.Size(23, 22);
            this.BoldButton.Text = "Bold";
            this.BoldButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BoldButton.Click += new System.EventHandler(this.BoldButton_Click);
            // 
            // ItalicButton
            // 
            this.ItalicButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ItalicButton.Image = ((System.Drawing.Image)(resources.GetObject("ItalicButton.Image")));
            this.ItalicButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ItalicButton.Name = "ItalicButton";
            this.ItalicButton.Size = new System.Drawing.Size(23, 22);
            this.ItalicButton.Text = "Italic";
            this.ItalicButton.Click += new System.EventHandler(this.ItalicButton_Click);
            // 
            // UnderlineButton
            // 
            this.UnderlineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnderlineButton.Image = ((System.Drawing.Image)(resources.GetObject("UnderlineButton.Image")));
            this.UnderlineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnderlineButton.Name = "UnderlineButton";
            this.UnderlineButton.Size = new System.Drawing.Size(23, 22);
            this.UnderlineButton.Text = "Underline";
            this.UnderlineButton.Click += new System.EventHandler(this.UnderlineButton_Click);
            // 
            // StrikeThroughButton
            // 
            this.StrikeThroughButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StrikeThroughButton.Image = ((System.Drawing.Image)(resources.GetObject("StrikeThroughButton.Image")));
            this.StrikeThroughButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StrikeThroughButton.Name = "StrikeThroughButton";
            this.StrikeThroughButton.Size = new System.Drawing.Size(23, 22);
            this.StrikeThroughButton.Text = "Strike Through";
            this.StrikeThroughButton.Click += new System.EventHandler(this.StrikeThroughButton_Click);
            // 
            // StyleCB
            // 
            this.StyleCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StyleCB.Items.AddRange(new object[] {
            "Helvetica",
            "Courier",
            "Times-Roman"});
            this.StyleCB.Name = "StyleCB";
            this.StyleCB.Size = new System.Drawing.Size(121, 25);
            this.StyleCB.SelectedIndexChanged += new System.EventHandler(this.StyleCB_SelectedIndexChanged);
            // 
            // SizeCB
            // 
            this.SizeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SizeCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.SizeCB.Name = "SizeCB";
            this.SizeCB.Size = new System.Drawing.Size(75, 25);
            this.SizeCB.SelectedIndexChanged += new System.EventHandler(this.SizeCB_SelectedIndexChanged);
            // 
            // TextColorBtn
            // 
            this.TextColorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TextColorBtn.Image = ((System.Drawing.Image)(resources.GetObject("TextColorBtn.Image")));
            this.TextColorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TextColorBtn.Name = "TextColorBtn";
            this.TextColorBtn.Size = new System.Drawing.Size(23, 22);
            this.TextColorBtn.Text = "Text Color";
            this.TextColorBtn.Click += new System.EventHandler(this.TextColorBtn_Click);
            // 
            // BackColorBtn
            // 
            this.BackColorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BackColorBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackColorBtn.Image")));
            this.BackColorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackColorBtn.Name = "BackColorBtn";
            this.BackColorBtn.Size = new System.Drawing.Size(23, 22);
            this.BackColorBtn.Text = "Back Color";
            this.BackColorBtn.Visible = false;
            this.BackColorBtn.Click += new System.EventHandler(this.BackColorBtn_Click);
            // 
            // JustifyLeftBtn
            // 
            this.JustifyLeftBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.JustifyLeftBtn.Image = ((System.Drawing.Image)(resources.GetObject("JustifyLeftBtn.Image")));
            this.JustifyLeftBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.JustifyLeftBtn.Name = "JustifyLeftBtn";
            this.JustifyLeftBtn.Size = new System.Drawing.Size(23, 22);
            this.JustifyLeftBtn.Text = "Justify Left";
            this.JustifyLeftBtn.Click += new System.EventHandler(this.JustifyLeftBtn_Click);
            // 
            // JustifyCenterBtn
            // 
            this.JustifyCenterBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.JustifyCenterBtn.Image = ((System.Drawing.Image)(resources.GetObject("JustifyCenterBtn.Image")));
            this.JustifyCenterBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.JustifyCenterBtn.Name = "JustifyCenterBtn";
            this.JustifyCenterBtn.Size = new System.Drawing.Size(23, 22);
            this.JustifyCenterBtn.Text = "Justify Center";
            this.JustifyCenterBtn.Click += new System.EventHandler(this.JustifyCenterBtn_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Justify Right";
            this.toolStripButton1.Click += new System.EventHandler(this.JustifyRightBtn_Click);
            // 
            // ChipBtn
            // 
            this.ChipBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ChipBtn.Image = ((System.Drawing.Image)(resources.GetObject("ChipBtn.Image")));
            this.ChipBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChipBtn.Name = "ChipBtn";
            this.ChipBtn.Size = new System.Drawing.Size(23, 22);
            this.ChipBtn.Text = "Chip";
            this.ChipBtn.Click += new System.EventHandler(this.ChipBtn_Click);
            // 
            // IndentsBtn
            // 
            this.IndentsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IndentsBtn.Image = ((System.Drawing.Image)(resources.GetObject("IndentsBtn.Image")));
            this.IndentsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IndentsBtn.Name = "IndentsBtn";
            this.IndentsBtn.Size = new System.Drawing.Size(23, 22);
            this.IndentsBtn.Text = "Indent";
            this.IndentsBtn.Click += new System.EventHandler(this.IndentsBtn_Click);
            // 
            // OutdentBtn
            // 
            this.OutdentBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OutdentBtn.Image = ((System.Drawing.Image)(resources.GetObject("OutdentBtn.Image")));
            this.OutdentBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OutdentBtn.Name = "OutdentBtn";
            this.OutdentBtn.Size = new System.Drawing.Size(23, 22);
            this.OutdentBtn.Text = "Outdent";
            this.OutdentBtn.Click += new System.EventHandler(this.OutdentBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.Location = new System.Drawing.Point(12, 336);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 5;
            this.OkBtn.Text = "Ok";
            this.OkBtn.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(452, 336);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.HTMLEditor);
            this.panel1.Controls.Add(this.CancelBtn);
            this.panel1.Controls.Add(this.OkBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(539, 372);
            this.panel1.TabIndex = 7;
            // 
            // SIM_Htmleditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(539, 422);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SIM_Htmleditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Note Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SIM_Htmleditor_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser HTMLEditor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton BoldButton;
        private System.Windows.Forms.ToolStripButton ItalicButton;
        private System.Windows.Forms.ToolStripButton UnderlineButton;
        private System.Windows.Forms.ToolStripButton StrikeThroughButton;
        private System.Windows.Forms.ToolStripComboBox StyleCB;
        private System.Windows.Forms.ToolStripComboBox SizeCB;
        private System.Windows.Forms.ToolStripDropDownButton EditionMenu;
        private System.Windows.Forms.ToolStripMenuItem CutButton;
        private System.Windows.Forms.ToolStripMenuItem copyButton;
        private System.Windows.Forms.ToolStripMenuItem pasteButton;
        private System.Windows.Forms.ToolStripDropDownButton InsertButton;
        private System.Windows.Forms.ToolStripMenuItem imageButton;
        private System.Windows.Forms.ToolStripMenuItem LinkButton;
        private System.Windows.Forms.ToolStripButton JustifyLeftBtn;
        private System.Windows.Forms.ToolStripButton JustifyCenterBtn;
        private System.Windows.Forms.ToolStripButton IndentsBtn;
        private System.Windows.Forms.ToolStripButton TextColorBtn;
        private System.Windows.Forms.ToolStripButton BackColorBtn;
        private System.Windows.Forms.ToolStripButton OutdentBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ToolStripButton ChipBtn;
        private System.Windows.Forms.ToolStripMenuItem UndoBtn;
        private System.Windows.Forms.ToolStripMenuItem RedoBtn;
        private System.Windows.Forms.ToolStripMenuItem HorizontalRuleBtn;
        private System.Windows.Forms.ToolStripMenuItem TableBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}