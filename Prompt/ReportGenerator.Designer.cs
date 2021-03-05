namespace SIMCORE_TOOL.Prompt
{
    partial class ReportGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportGenerator));
            this.tv_Reports = new System.Windows.Forms.TreeView();
            this.NodeMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllChildNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unckeckAllChildNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertCheckedNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Generate = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.generateHTMLCheckBox = new System.Windows.Forms.CheckBox();
            this.PageFormatLabel = new System.Windows.Forms.Label();
            this.PageFormatBox = new System.Windows.Forms.ComboBox();
            this.generatePDFCheckBox = new System.Windows.Forms.CheckBox();
            this.bt_template = new System.Windows.Forms.Button();
            this.lb_template = new System.Windows.Forms.Label();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.tb_template = new System.Windows.Forms.TextBox();
            this.AutorTextBox = new System.Windows.Forms.TextBox();
            this.purgedTreeCheckBox = new System.Windows.Forms.CheckBox();
            this.ReportNameComboBox = new System.Windows.Forms.ComboBox();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.generalGroupBox = new System.Windows.Forms.GroupBox();
            this.exportXmlConfigurationCheckBox = new System.Windows.Forms.CheckBox();
            this.exportSourceTextFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.ll_pdfPreview = new System.Windows.Forms.LinkLabel();
            this.ll_htmlPreview = new System.Windows.Forms.LinkLabel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.versionTextBox = new System.Windows.Forms.TextBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.dateTextBox = new System.Windows.Forms.TextBox();
            this.dateLabel = new System.Windows.Forms.Label();
            this.pdfGroupBox = new System.Windows.Forms.GroupBox();
            this.lbl_divider = new System.Windows.Forms.Label();
            this.cb_dividerLevel = new System.Windows.Forms.ComboBox();
            this.cb_useTemplate = new System.Windows.Forms.CheckBox();
            this.bt_browseLogo = new System.Windows.Forms.Button();
            this.lb_logo = new System.Windows.Forms.Label();
            this.tb_logo = new System.Windows.Forms.TextBox();
            this.bt_versionning = new System.Windows.Forms.Button();
            this.exportNoteCheckBox = new System.Windows.Forms.CheckBox();
            this.includPurgedToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.bt_editTree = new System.Windows.Forms.Button();
            this.lbl_editTree = new System.Windows.Forms.Label();
            this.newConfigurationButton = new System.Windows.Forms.Button();
            this.assignToReportGroupsButton = new System.Windows.Forms.Button();
            this.NodeMenuStrip.SuspendLayout();
            this.generalGroupBox.SuspendLayout();
            this.pdfGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv_Reports
            // 
            this.tv_Reports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tv_Reports.Location = new System.Drawing.Point(392, 48);
            this.tv_Reports.Name = "tv_Reports";
            this.tv_Reports.Size = new System.Drawing.Size(332, 392);
            this.tv_Reports.TabIndex = 10;
            this.tv_Reports.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_Reports_AfterCheck);
            this.tv_Reports.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_Reports_MouseDown);
            // 
            // NodeMenuStrip
            // 
            this.NodeMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllChildNodesToolStripMenuItem,
            this.unckeckAllChildNodesToolStripMenuItem,
            this.invertCheckedNodesToolStripMenuItem});
            this.NodeMenuStrip.Name = "NodeMenuStrip";
            this.NodeMenuStrip.Size = new System.Drawing.Size(199, 70);
            // 
            // checkAllChildNodesToolStripMenuItem
            // 
            this.checkAllChildNodesToolStripMenuItem.Name = "checkAllChildNodesToolStripMenuItem";
            this.checkAllChildNodesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.checkAllChildNodesToolStripMenuItem.Text = "Check All child nodes";
            this.checkAllChildNodesToolStripMenuItem.Click += new System.EventHandler(this.checkAllChildNodesToolStripMenuItem_Click);
            // 
            // unckeckAllChildNodesToolStripMenuItem
            // 
            this.unckeckAllChildNodesToolStripMenuItem.Name = "unckeckAllChildNodesToolStripMenuItem";
            this.unckeckAllChildNodesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.unckeckAllChildNodesToolStripMenuItem.Text = "Unckeck all child nodes";
            this.unckeckAllChildNodesToolStripMenuItem.Click += new System.EventHandler(this.unckeckAllChildNodesToolStripMenuItem_Click);
            // 
            // invertCheckedNodesToolStripMenuItem
            // 
            this.invertCheckedNodesToolStripMenuItem.Name = "invertCheckedNodesToolStripMenuItem";
            this.invertCheckedNodesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.invertCheckedNodesToolStripMenuItem.Text = "Invert Checked nodes";
            this.invertCheckedNodesToolStripMenuItem.Click += new System.EventHandler(this.invertCheckedNodesToolStripMenuItem_Click);
            // 
            // btn_Generate
            // 
            this.btn_Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Generate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Generate.Location = new System.Drawing.Point(12, 456);
            this.btn_Generate.Name = "btn_Generate";
            this.btn_Generate.Size = new System.Drawing.Size(95, 41);
            this.btn_Generate.TabIndex = 7;
            this.btn_Generate.Text = "Generate";
            this.btn_Generate.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(629, 456);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 41);
            this.btn_Cancel.TabIndex = 8;
            this.btn_Cancel.Text = "Ok";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(387, 12);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(110, 21);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save configuration";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // generateHTMLCheckBox
            // 
            this.generateHTMLCheckBox.AutoSize = true;
            this.generateHTMLCheckBox.Location = new System.Drawing.Point(16, 24);
            this.generateHTMLCheckBox.Name = "generateHTMLCheckBox";
            this.generateHTMLCheckBox.Size = new System.Drawing.Size(133, 17);
            this.generateHTMLCheckBox.TabIndex = 0;
            this.generateHTMLCheckBox.Text = "Generate HTML report";
            this.generateHTMLCheckBox.UseVisualStyleBackColor = true;
            this.generateHTMLCheckBox.Click += new System.EventHandler(this.ParamChange);
            // 
            // PageFormatLabel
            // 
            this.PageFormatLabel.AutoSize = true;
            this.PageFormatLabel.Location = new System.Drawing.Point(33, 75);
            this.PageFormatLabel.Name = "PageFormatLabel";
            this.PageFormatLabel.Size = new System.Drawing.Size(42, 13);
            this.PageFormatLabel.TabIndex = 4;
            this.PageFormatLabel.Text = "Format:";
            // 
            // PageFormatBox
            // 
            this.PageFormatBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PageFormatBox.FormattingEnabled = true;
            this.PageFormatBox.Location = new System.Drawing.Point(81, 72);
            this.PageFormatBox.Name = "PageFormatBox";
            this.PageFormatBox.Size = new System.Drawing.Size(273, 21);
            this.PageFormatBox.TabIndex = 2;
            this.PageFormatBox.SelectedIndexChanged += new System.EventHandler(this.ParamChange);
            // 
            // generatePDFCheckBox
            // 
            this.generatePDFCheckBox.AutoSize = true;
            this.generatePDFCheckBox.Location = new System.Drawing.Point(16, 47);
            this.generatePDFCheckBox.Name = "generatePDFCheckBox";
            this.generatePDFCheckBox.Size = new System.Drawing.Size(124, 17);
            this.generatePDFCheckBox.TabIndex = 1;
            this.generatePDFCheckBox.Text = "Generate PDF report";
            this.generatePDFCheckBox.UseVisualStyleBackColor = true;
            this.generatePDFCheckBox.Click += new System.EventHandler(this.ParamChange);
            // 
            // bt_template
            // 
            this.bt_template.Location = new System.Drawing.Point(279, 148);
            this.bt_template.Name = "bt_template";
            this.bt_template.Size = new System.Drawing.Size(75, 23);
            this.bt_template.TabIndex = 6;
            this.bt_template.Text = "Browse";
            this.bt_template.UseVisualStyleBackColor = true;
            this.bt_template.Click += new System.EventHandler(this.btn_browse1_Click);
            // 
            // lb_template
            // 
            this.lb_template.AutoSize = true;
            this.lb_template.Location = new System.Drawing.Point(21, 151);
            this.lb_template.Name = "lb_template";
            this.lb_template.Size = new System.Drawing.Size(54, 13);
            this.lb_template.TabIndex = 3;
            this.lb_template.Text = "Template:";
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(34, 49);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(41, 13);
            this.AuthorLabel.TabIndex = 2;
            this.AuthorLabel.Text = "Author:";
            // 
            // tb_template
            // 
            this.tb_template.Location = new System.Drawing.Point(81, 148);
            this.tb_template.Name = "tb_template";
            this.tb_template.Size = new System.Drawing.Size(192, 20);
            this.tb_template.TabIndex = 5;
            this.tb_template.Leave += new System.EventHandler(this.ParamChange);
            // 
            // AutorTextBox
            // 
            this.AutorTextBox.Location = new System.Drawing.Point(81, 46);
            this.AutorTextBox.Name = "AutorTextBox";
            this.AutorTextBox.Size = new System.Drawing.Size(273, 20);
            this.AutorTextBox.TabIndex = 1;
            this.AutorTextBox.Leave += new System.EventHandler(this.ParamChange);
            // 
            // purgedTreeCheckBox
            // 
            this.purgedTreeCheckBox.AutoSize = true;
            this.purgedTreeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.purgedTreeCheckBox.Location = new System.Drawing.Point(188, 478);
            this.purgedTreeCheckBox.Name = "purgedTreeCheckBox";
            this.purgedTreeCheckBox.Size = new System.Drawing.Size(163, 17);
            this.purgedTreeCheckBox.TabIndex = 6;
            this.purgedTreeCheckBox.Text = "Include unchecked elements";
            this.includPurgedToolTip.SetToolTip(this.purgedTreeCheckBox, "The report will include the name of the non checked elements.");
            this.purgedTreeCheckBox.UseVisualStyleBackColor = false;
            this.purgedTreeCheckBox.Visible = false;
            this.purgedTreeCheckBox.Click += new System.EventHandler(this.ParamChange);
            this.purgedTreeCheckBox.CheckedChanged += new System.EventHandler(this.ParamChange);
            // 
            // ReportNameComboBox
            // 
            this.ReportNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReportNameComboBox.FormattingEnabled = true;
            this.ReportNameComboBox.Location = new System.Drawing.Point(174, 12);
            this.ReportNameComboBox.Name = "ReportNameComboBox";
            this.ReportNameComboBox.Size = new System.Drawing.Size(177, 21);
            this.ReportNameComboBox.TabIndex = 1;
            this.ReportNameComboBox.SelectedIndexChanged += new System.EventHandler(this.ReportNameComboBox_changed);
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Name.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_Name.Location = new System.Drawing.Point(55, 12);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(113, 19);
            this.lbl_Name.TabIndex = 16;
            this.lbl_Name.Text = "Report name:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(503, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 21);
            this.button1.TabIndex = 3;
            this.button1.Text = "Delete configuration";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // generalGroupBox
            // 
            this.generalGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.generalGroupBox.Controls.Add(this.exportXmlConfigurationCheckBox);
            this.generalGroupBox.Controls.Add(this.exportSourceTextFilesCheckBox);
            this.generalGroupBox.Controls.Add(this.ll_pdfPreview);
            this.generalGroupBox.Controls.Add(this.ll_htmlPreview);
            this.generalGroupBox.Controls.Add(this.generateHTMLCheckBox);
            this.generalGroupBox.Controls.Add(this.generatePDFCheckBox);
            this.generalGroupBox.Location = new System.Drawing.Point(12, 48);
            this.generalGroupBox.Name = "generalGroupBox";
            this.generalGroupBox.Size = new System.Drawing.Size(374, 74);
            this.generalGroupBox.TabIndex = 4;
            this.generalGroupBox.TabStop = false;
            this.generalGroupBox.Text = "General options";
            // 
            // exportXmlConfigurationCheckBox
            // 
            this.exportXmlConfigurationCheckBox.AutoSize = true;
            this.exportXmlConfigurationCheckBox.Location = new System.Drawing.Point(220, 47);
            this.exportXmlConfigurationCheckBox.Name = "exportXmlConfigurationCheckBox";
            this.exportXmlConfigurationCheckBox.Size = new System.Drawing.Size(145, 17);
            this.exportXmlConfigurationCheckBox.TabIndex = 5;
            this.exportXmlConfigurationCheckBox.Text = "Export XML configuration";
            this.exportXmlConfigurationCheckBox.UseVisualStyleBackColor = true;
            this.exportXmlConfigurationCheckBox.Click += new System.EventHandler(this.ParamChange);
            // 
            // exportSourceTextFilesCheckBox
            // 
            this.exportSourceTextFilesCheckBox.AutoSize = true;
            this.exportSourceTextFilesCheckBox.Location = new System.Drawing.Point(220, 24);
            this.exportSourceTextFilesCheckBox.Name = "exportSourceTextFilesCheckBox";
            this.exportSourceTextFilesCheckBox.Size = new System.Drawing.Size(132, 17);
            this.exportSourceTextFilesCheckBox.TabIndex = 4;
            this.exportSourceTextFilesCheckBox.Text = "Export source text files";
            this.exportSourceTextFilesCheckBox.UseVisualStyleBackColor = true;
            this.exportSourceTextFilesCheckBox.Click += new System.EventHandler(this.ParamChange);
            // 
            // ll_pdfPreview
            // 
            this.ll_pdfPreview.AutoSize = true;
            this.ll_pdfPreview.Location = new System.Drawing.Point(149, 48);
            this.ll_pdfPreview.Name = "ll_pdfPreview";
            this.ll_pdfPreview.Size = new System.Drawing.Size(45, 13);
            this.ll_pdfPreview.TabIndex = 3;
            this.ll_pdfPreview.TabStop = true;
            this.ll_pdfPreview.Text = "Preview";
            this.ll_pdfPreview.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_pdfPreview_LinkClicked);
            // 
            // ll_htmlPreview
            // 
            this.ll_htmlPreview.AutoSize = true;
            this.ll_htmlPreview.Location = new System.Drawing.Point(149, 25);
            this.ll_htmlPreview.Name = "ll_htmlPreview";
            this.ll_htmlPreview.Size = new System.Drawing.Size(45, 13);
            this.ll_htmlPreview.TabIndex = 2;
            this.ll_htmlPreview.TabStop = true;
            this.ll_htmlPreview.Text = "Preview";
            this.ll_htmlPreview.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_htmlPreview_LinkClicked);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(45, 23);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(30, 13);
            this.titleLabel.TabIndex = 6;
            this.titleLabel.Text = "Title:";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(81, 20);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(273, 20);
            this.titleTextBox.TabIndex = 0;
            this.titleTextBox.Leave += new System.EventHandler(this.ParamChange);
            // 
            // versionTextBox
            // 
            this.versionTextBox.Location = new System.Drawing.Point(81, 99);
            this.versionTextBox.Name = "versionTextBox";
            this.versionTextBox.Size = new System.Drawing.Size(98, 20);
            this.versionTextBox.TabIndex = 3;
            this.versionTextBox.Leave += new System.EventHandler(this.ParamChange);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(30, 102);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(45, 13);
            this.versionLabel.TabIndex = 21;
            this.versionLabel.Text = "Version:";
            // 
            // dateTextBox
            // 
            this.dateTextBox.Location = new System.Drawing.Point(224, 99);
            this.dateTextBox.Name = "dateTextBox";
            this.dateTextBox.Size = new System.Drawing.Size(130, 20);
            this.dateTextBox.TabIndex = 4;
            this.dateTextBox.Leave += new System.EventHandler(this.ParamChange);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(185, 102);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(33, 13);
            this.dateLabel.TabIndex = 8;
            this.dateLabel.Text = "Date:";
            // 
            // pdfGroupBox
            // 
            this.pdfGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.pdfGroupBox.Controls.Add(this.assignToReportGroupsButton);
            this.pdfGroupBox.Controls.Add(this.lbl_divider);
            this.pdfGroupBox.Controls.Add(this.cb_dividerLevel);
            this.pdfGroupBox.Controls.Add(this.cb_useTemplate);
            this.pdfGroupBox.Controls.Add(this.bt_browseLogo);
            this.pdfGroupBox.Controls.Add(this.lb_logo);
            this.pdfGroupBox.Controls.Add(this.tb_logo);
            this.pdfGroupBox.Controls.Add(this.bt_versionning);
            this.pdfGroupBox.Controls.Add(this.titleLabel);
            this.pdfGroupBox.Controls.Add(this.titleTextBox);
            this.pdfGroupBox.Controls.Add(this.versionTextBox);
            this.pdfGroupBox.Controls.Add(this.PageFormatLabel);
            this.pdfGroupBox.Controls.Add(this.AuthorLabel);
            this.pdfGroupBox.Controls.Add(this.PageFormatBox);
            this.pdfGroupBox.Controls.Add(this.AutorTextBox);
            this.pdfGroupBox.Controls.Add(this.versionLabel);
            this.pdfGroupBox.Controls.Add(this.bt_template);
            this.pdfGroupBox.Controls.Add(this.dateLabel);
            this.pdfGroupBox.Controls.Add(this.dateTextBox);
            this.pdfGroupBox.Controls.Add(this.lb_template);
            this.pdfGroupBox.Controls.Add(this.tb_template);
            this.pdfGroupBox.Enabled = false;
            this.pdfGroupBox.Location = new System.Drawing.Point(12, 128);
            this.pdfGroupBox.Name = "pdfGroupBox";
            this.pdfGroupBox.Size = new System.Drawing.Size(374, 312);
            this.pdfGroupBox.TabIndex = 5;
            this.pdfGroupBox.TabStop = false;
            this.pdfGroupBox.Text = "PDF options";
            // 
            // lbl_divider
            // 
            this.lbl_divider.AutoSize = true;
            this.lbl_divider.Location = new System.Drawing.Point(7, 203);
            this.lbl_divider.Name = "lbl_divider";
            this.lbl_divider.Size = new System.Drawing.Size(68, 13);
            this.lbl_divider.TabIndex = 27;
            this.lbl_divider.Text = "Divider level:";
            // 
            // cb_dividerLevel
            // 
            this.cb_dividerLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_dividerLevel.FormattingEnabled = true;
            this.cb_dividerLevel.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cb_dividerLevel.Location = new System.Drawing.Point(81, 200);
            this.cb_dividerLevel.Name = "cb_dividerLevel";
            this.cb_dividerLevel.Size = new System.Drawing.Size(44, 21);
            this.cb_dividerLevel.TabIndex = 26;
            this.cb_dividerLevel.Leave += new System.EventHandler(this.ParamChange);
            // 
            // cb_useTemplate
            // 
            this.cb_useTemplate.AutoSize = true;
            this.cb_useTemplate.BackColor = System.Drawing.Color.Transparent;
            this.cb_useTemplate.Location = new System.Drawing.Point(6, 125);
            this.cb_useTemplate.Name = "cb_useTemplate";
            this.cb_useTemplate.Size = new System.Drawing.Size(88, 17);
            this.cb_useTemplate.TabIndex = 25;
            this.cb_useTemplate.Text = "Use template";
            this.cb_useTemplate.UseVisualStyleBackColor = false;
            this.cb_useTemplate.Click += new System.EventHandler(this.ParamChange);
            // 
            // bt_browseLogo
            // 
            this.bt_browseLogo.Location = new System.Drawing.Point(279, 174);
            this.bt_browseLogo.Name = "bt_browseLogo";
            this.bt_browseLogo.Size = new System.Drawing.Size(75, 23);
            this.bt_browseLogo.TabIndex = 24;
            this.bt_browseLogo.Text = "Browse";
            this.bt_browseLogo.UseVisualStyleBackColor = true;
            this.bt_browseLogo.Click += new System.EventHandler(this.btn_browse2_Click);
            // 
            // lb_logo
            // 
            this.lb_logo.AutoSize = true;
            this.lb_logo.Location = new System.Drawing.Point(41, 177);
            this.lb_logo.Name = "lb_logo";
            this.lb_logo.Size = new System.Drawing.Size(34, 13);
            this.lb_logo.TabIndex = 22;
            this.lb_logo.Text = "Logo:";
            // 
            // tb_logo
            // 
            this.tb_logo.Location = new System.Drawing.Point(81, 174);
            this.tb_logo.Name = "tb_logo";
            this.tb_logo.Size = new System.Drawing.Size(192, 20);
            this.tb_logo.TabIndex = 23;
            this.tb_logo.Leave += new System.EventHandler(this.ParamChange);
            // 
            // bt_versionning
            // 
            this.bt_versionning.Location = new System.Drawing.Point(33, 227);
            this.bt_versionning.Name = "bt_versionning";
            this.bt_versionning.Size = new System.Drawing.Size(311, 23);
            this.bt_versionning.TabIndex = 8;
            this.bt_versionning.Text = "Document versioning && approvals";
            this.bt_versionning.UseVisualStyleBackColor = true;
            this.bt_versionning.Click += new System.EventHandler(this.bt_versionning_Click);
            // 
            // exportNoteCheckBox
            // 
            this.exportNoteCheckBox.AutoSize = true;
            this.exportNoteCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.exportNoteCheckBox.Location = new System.Drawing.Point(188, 455);
            this.exportNoteCheckBox.Name = "exportNoteCheckBox";
            this.exportNoteCheckBox.Size = new System.Drawing.Size(85, 17);
            this.exportNoteCheckBox.TabIndex = 7;
            this.exportNoteCheckBox.Text = "Export notes";
            this.includPurgedToolTip.SetToolTip(this.exportNoteCheckBox, "The report will include the name of the non checked elements.");
            this.exportNoteCheckBox.UseVisualStyleBackColor = false;
            this.exportNoteCheckBox.Visible = false;
            this.exportNoteCheckBox.Click += new System.EventHandler(this.ParamChange);
            // 
            // bt_editTree
            // 
            this.bt_editTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_editTree.Location = new System.Drawing.Point(313, 446);
            this.bt_editTree.Name = "bt_editTree";
            this.bt_editTree.Size = new System.Drawing.Size(114, 23);
            this.bt_editTree.TabIndex = 17;
            this.bt_editTree.Text = "Edit content tree";
            this.bt_editTree.UseVisualStyleBackColor = true;
            this.bt_editTree.Visible = false;
            // 
            // lbl_editTree
            // 
            this.lbl_editTree.AutoSize = true;
            this.lbl_editTree.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_editTree.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_editTree.Location = new System.Drawing.Point(452, 208);
            this.lbl_editTree.Name = "lbl_editTree";
            this.lbl_editTree.Size = new System.Drawing.Size(209, 13);
            this.lbl_editTree.TabIndex = 18;
            this.lbl_editTree.Text = "Double click here to edit the report content";
            this.lbl_editTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDoubleClick);
            // 
            // newConfigurationButton
            // 
            this.newConfigurationButton.Location = new System.Drawing.Point(619, 12);
            this.newConfigurationButton.Name = "newConfigurationButton";
            this.newConfigurationButton.Size = new System.Drawing.Size(110, 21);
            this.newConfigurationButton.TabIndex = 19;
            this.newConfigurationButton.Text = "New configuration";
            this.newConfigurationButton.UseVisualStyleBackColor = true;
            this.newConfigurationButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // assignToReportGroupsButton
            // 
            this.assignToReportGroupsButton.Location = new System.Drawing.Point(33, 262);
            this.assignToReportGroupsButton.Name = "assignToReportGroupsButton";
            this.assignToReportGroupsButton.Size = new System.Drawing.Size(311, 23);
            this.assignToReportGroupsButton.TabIndex = 28;
            this.assignToReportGroupsButton.Text = "Assign to Report Groups";
            this.assignToReportGroupsButton.UseVisualStyleBackColor = true;
            this.assignToReportGroupsButton.Click += new System.EventHandler(this.assignToReportGroupsButton_Click);
            // 
            // ReportGenerator
            // 
            this.AcceptButton = this.btn_Generate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(736, 517);
            this.Controls.Add(this.newConfigurationButton);
            this.Controls.Add(this.lbl_editTree);
            this.Controls.Add(this.bt_editTree);
            this.Controls.Add(this.generalGroupBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_Name);
            this.Controls.Add(this.pdfGroupBox);
            this.Controls.Add(this.ReportNameComboBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.exportNoteCheckBox);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.tv_Reports);
            this.Controls.Add(this.purgedTreeCheckBox);
            this.Controls.Add(this.btn_Generate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(654, 504);
            this.Name = "ReportGenerator";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportGenerator_FormClosing);
            this.NodeMenuStrip.ResumeLayout(false);
            this.generalGroupBox.ResumeLayout(false);
            this.generalGroupBox.PerformLayout();
            this.pdfGroupBox.ResumeLayout(false);
            this.pdfGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_Reports;
        private System.Windows.Forms.Button btn_Generate;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox generateHTMLCheckBox;
        private System.Windows.Forms.CheckBox generatePDFCheckBox;
        private System.Windows.Forms.CheckBox purgedTreeCheckBox;
        private System.Windows.Forms.Label PageFormatLabel;
        private System.Windows.Forms.ComboBox PageFormatBox;
        private System.Windows.Forms.ComboBox ReportNameComboBox;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bt_template;
        private System.Windows.Forms.Label lb_template;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.TextBox tb_template;
        private System.Windows.Forms.TextBox AutorTextBox;
        private System.Windows.Forms.ContextMenuStrip NodeMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem checkAllChildNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unckeckAllChildNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertCheckedNodesToolStripMenuItem;
        private System.Windows.Forms.GroupBox generalGroupBox;
        private System.Windows.Forms.GroupBox pdfGroupBox;
        private System.Windows.Forms.ToolTip includPurgedToolTip;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.TextBox dateTextBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox versionTextBox;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.CheckBox exportNoteCheckBox;
        private System.Windows.Forms.Button bt_versionning;
        private System.Windows.Forms.Button bt_browseLogo;
        private System.Windows.Forms.Label lb_logo;
        private System.Windows.Forms.TextBox tb_logo;
        private System.Windows.Forms.CheckBox cb_useTemplate;
        private System.Windows.Forms.Button bt_editTree;
        private System.Windows.Forms.Label lbl_editTree;
        private System.Windows.Forms.Label lbl_divider;
        private System.Windows.Forms.ComboBox cb_dividerLevel;
        private System.Windows.Forms.LinkLabel ll_pdfPreview;
        private System.Windows.Forms.LinkLabel ll_htmlPreview;
        private System.Windows.Forms.CheckBox exportSourceTextFilesCheckBox;
        private System.Windows.Forms.CheckBox exportXmlConfigurationCheckBox;
        private System.Windows.Forms.Button newConfigurationButton;
        private System.Windows.Forms.Button assignToReportGroupsButton;
    }
}