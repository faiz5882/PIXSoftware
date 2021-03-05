namespace SIMCORE_TOOL.Prompt
{
    partial class TreeEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeEditor));
            this.bt_insert = new System.Windows.Forms.Button();
            this.tv_final = new System.Windows.Forms.TreeView();
            this.tv_original = new System.Windows.Forms.TreeView();
            this.bt_up = new System.Windows.Forms.Button();
            this.bt_down = new System.Windows.Forms.Button();
            this.bt_left = new System.Windows.Forms.Button();
            this.bt_right = new System.Windows.Forms.Button();
            this.bt_delete = new System.Windows.Forms.Button();
            this.bt_CreateDirectory = new System.Windows.Forms.Button();
            this.bt_CreateNote = new System.Windows.Forms.Button();
            this.bt_insertRoot = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lbl_original = new System.Windows.Forms.Label();
            this.lbl_final = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmsParagraph = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editParagraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteParagraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmsTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsiViewTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tscResizeTable = new System.Windows.Forms.ToolStripComboBox();
            this.cmsfolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRemoveIST = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_RemoveISTTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveBagPaxPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiChartView = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsParagraph.SuspendLayout();
            this.cmsTable.SuspendLayout();
            this.cmsfolder.SuspendLayout();
            this.cmsView.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_insert
            // 
            this.bt_insert.Image = global::SIMCORE_TOOL.Properties.Resources.insert_selected;
            this.bt_insert.Location = new System.Drawing.Point(470, 102);
            this.bt_insert.Name = "bt_insert";
            this.bt_insert.Size = new System.Drawing.Size(48, 26);
            this.bt_insert.TabIndex = 2;
            this.toolTip1.SetToolTip(this.bt_insert, "Add the selection of the left tree in the selection of the right tree");
            this.bt_insert.UseVisualStyleBackColor = true;
            this.bt_insert.Click += new System.EventHandler(this.bt_insert_Click);
            // 
            // tv_final
            // 
            this.tv_final.AllowDrop = true;
            this.tv_final.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tv_final.Location = new System.Drawing.Point(529, 31);
            this.tv_final.Name = "tv_final";
            this.tv_final.ShowNodeToolTips = true;
            this.tv_final.Size = new System.Drawing.Size(485, 451);
            this.tv_final.TabIndex = 5;
            this.tv_final.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tv_DoubleClick);
            this.tv_final.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tv_final_MouseClick);
            this.tv_final.Enter += new System.EventHandler(this.tv_EnterTree);
            this.tv_final.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
            this.tv_final.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv_final.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tv_final_MouseMove);
            this.tv_final.DragEnter += new System.Windows.Forms.DragEventHandler(this.tv_DragEnter);
            this.tv_final.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_BeforeSelect);
            this.tv_final.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tv_final_KeyDown);
            this.tv_final.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tv_ItemDrag);
            // 
            // tv_original
            // 
            this.tv_original.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tv_original.Location = new System.Drawing.Point(12, 31);
            this.tv_original.Name = "tv_original";
            this.tv_original.Size = new System.Drawing.Size(449, 451);
            this.tv_original.TabIndex = 0;
            this.tv_original.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tv_DoubleClick);
            this.tv_original.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tv_original_MouseClick);
            this.tv_original.Enter += new System.EventHandler(this.tv_EnterTree);
            this.tv_original.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv_original.DragEnter += new System.Windows.Forms.DragEventHandler(this.tv_DragEnter);
            this.tv_original.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_original_NodeMouseClick);
            this.tv_original.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_BeforeSelect);
            this.tv_original.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tv_ItemDrag);
            // 
            // bt_up
            // 
            this.bt_up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_up.ForeColor = System.Drawing.Color.White;
            this.bt_up.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_up;
            this.bt_up.Location = new System.Drawing.Point(574, 488);
            this.bt_up.Name = "bt_up";
            this.bt_up.Size = new System.Drawing.Size(47, 23);
            this.bt_up.TabIndex = 6;
            this.bt_up.UseVisualStyleBackColor = true;
            this.bt_up.Click += new System.EventHandler(this.bt_up_Click);
            // 
            // bt_down
            // 
            this.bt_down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_down.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_down;
            this.bt_down.Location = new System.Drawing.Point(627, 488);
            this.bt_down.Name = "bt_down";
            this.bt_down.Size = new System.Drawing.Size(47, 23);
            this.bt_down.TabIndex = 7;
            this.bt_down.UseVisualStyleBackColor = true;
            this.bt_down.Click += new System.EventHandler(this.bt_down_Click);
            // 
            // bt_left
            // 
            this.bt_left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_left.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_left;
            this.bt_left.Location = new System.Drawing.Point(733, 488);
            this.bt_left.Name = "bt_left";
            this.bt_left.Size = new System.Drawing.Size(47, 23);
            this.bt_left.TabIndex = 9;
            this.bt_left.UseVisualStyleBackColor = true;
            this.bt_left.Click += new System.EventHandler(this.bt_left_Click);
            // 
            // bt_right
            // 
            this.bt_right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_right.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_right;
            this.bt_right.Location = new System.Drawing.Point(786, 488);
            this.bt_right.Name = "bt_right";
            this.bt_right.Size = new System.Drawing.Size(47, 23);
            this.bt_right.TabIndex = 10;
            this.bt_right.UseVisualStyleBackColor = true;
            this.bt_right.Click += new System.EventHandler(this.bt_right_Click);
            // 
            // bt_delete
            // 
            this.bt_delete.AllowDrop = true;
            this.bt_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_delete.Image = global::SIMCORE_TOOL.Properties.Resources.trash7;
            this.bt_delete.Location = new System.Drawing.Point(680, 488);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(47, 23);
            this.bt_delete.TabIndex = 8;
            this.bt_delete.UseVisualStyleBackColor = true;
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            this.bt_delete.DragDrop += new System.Windows.Forms.DragEventHandler(this.bt_delete_DragDrop);
            this.bt_delete.DragEnter += new System.Windows.Forms.DragEventHandler(this.bt_delete_DragEnter);
            // 
            // bt_CreateDirectory
            // 
            this.bt_CreateDirectory.Image = global::SIMCORE_TOOL.Properties.Resources.Open;
            this.bt_CreateDirectory.Location = new System.Drawing.Point(470, 224);
            this.bt_CreateDirectory.Name = "bt_CreateDirectory";
            this.bt_CreateDirectory.Size = new System.Drawing.Size(48, 26);
            this.bt_CreateDirectory.TabIndex = 3;
            this.bt_CreateDirectory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.bt_CreateDirectory, "Add a chapter in the selection of the right tree");
            this.bt_CreateDirectory.UseVisualStyleBackColor = true;
            this.bt_CreateDirectory.MouseLeave += new System.EventHandler(this.DragDrop_MouseLeave);
            this.bt_CreateDirectory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragDrop_MouseMove);
            this.bt_CreateDirectory.Click += new System.EventHandler(this.bt_CreateDirectory_Click);
            this.bt_CreateDirectory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragDrop_MouseDown);
            this.bt_CreateDirectory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DragDrop_MouseUp);
            // 
            // bt_CreateNote
            // 
            this.bt_CreateNote.Image = global::SIMCORE_TOOL.Properties.Resources.edit;
            this.bt_CreateNote.Location = new System.Drawing.Point(470, 256);
            this.bt_CreateNote.Name = "bt_CreateNote";
            this.bt_CreateNote.Size = new System.Drawing.Size(48, 26);
            this.bt_CreateNote.TabIndex = 4;
            this.toolTip1.SetToolTip(this.bt_CreateNote, "Create a note note session of the left tree");
            this.bt_CreateNote.UseVisualStyleBackColor = true;
            this.bt_CreateNote.MouseLeave += new System.EventHandler(this.DragDrop_MouseLeave);
            this.bt_CreateNote.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragDrop_MouseMove);
            this.bt_CreateNote.Click += new System.EventHandler(this.bt_CreateNote_Click);
            this.bt_CreateNote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragDrop_MouseDown);
            this.bt_CreateNote.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DragDrop_MouseUp);
            // 
            // bt_insertRoot
            // 
            this.bt_insertRoot.Image = global::SIMCORE_TOOL.Properties.Resources.insert_root;
            this.bt_insertRoot.Location = new System.Drawing.Point(470, 70);
            this.bt_insertRoot.Name = "bt_insertRoot";
            this.bt_insertRoot.Size = new System.Drawing.Size(48, 26);
            this.bt_insertRoot.TabIndex = 1;
            this.toolTip1.SetToolTip(this.bt_insertRoot, "Add the selection of the left tree at the end of the right tree");
            this.bt_insertRoot.UseVisualStyleBackColor = true;
            this.bt_insertRoot.Click += new System.EventHandler(this.bt_insertRoot_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(933, 488);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(81, 44);
            this.btn_Cancel.TabIndex = 12;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(13, 488);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(81, 44);
            this.btn_OK.TabIndex = 11;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // lbl_original
            // 
            this.lbl_original.AutoSize = true;
            this.lbl_original.BackColor = System.Drawing.Color.Transparent;
            this.lbl_original.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_original.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_original.Location = new System.Drawing.Point(12, 9);
            this.lbl_original.Name = "lbl_original";
            this.lbl_original.Size = new System.Drawing.Size(86, 19);
            this.lbl_original.TabIndex = 20;
            this.lbl_original.Text = "Elements:";
            // 
            // lbl_final
            // 
            this.lbl_final.AutoSize = true;
            this.lbl_final.BackColor = System.Drawing.Color.Transparent;
            this.lbl_final.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_final.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_final.Location = new System.Drawing.Point(534, 9);
            this.lbl_final.Name = "lbl_final";
            this.lbl_final.Size = new System.Drawing.Size(140, 19);
            this.lbl_final.TabIndex = 21;
            this.lbl_final.Text = "Report structure:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(467, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Create:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(467, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Insert:";
            // 
            // cmsParagraph
            // 
            this.cmsParagraph.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editParagraphToolStripMenuItem,
            this.deleteParagraphToolStripMenuItem});
            this.cmsParagraph.Name = "cmsParagraph";
            this.cmsParagraph.ShowImageMargin = false;
            this.cmsParagraph.Size = new System.Drawing.Size(140, 48);
            this.cmsParagraph.MouseLeave += new System.EventHandler(this.cms_MouseLeave);
            // 
            // editParagraphToolStripMenuItem
            // 
            this.editParagraphToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.editParagraphToolStripMenuItem.Name = "editParagraphToolStripMenuItem";
            this.editParagraphToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.editParagraphToolStripMenuItem.Text = "Edit Paragraph";
            this.editParagraphToolStripMenuItem.Click += new System.EventHandler(this.tsmiEditParagraph_Click);
            // 
            // deleteParagraphToolStripMenuItem
            // 
            this.deleteParagraphToolStripMenuItem.Name = "deleteParagraphToolStripMenuItem";
            this.deleteParagraphToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.deleteParagraphToolStripMenuItem.Text = "Delete Paragraph";
            this.deleteParagraphToolStripMenuItem.Click += new System.EventHandler(this.tsmiDeleteParagraph_Click);
            // 
            // cmsTable
            // 
            this.cmsTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiViewTable,
            this.tscResizeTable});
            this.cmsTable.Name = "cmsTable";
            this.cmsTable.ShowImageMargin = false;
            this.cmsTable.Size = new System.Drawing.Size(177, 53);
            // 
            // tmsiViewTable
            // 
            this.tmsiViewTable.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tmsiViewTable.Name = "tmsiViewTable";
            this.tmsiViewTable.Size = new System.Drawing.Size(176, 22);
            this.tmsiViewTable.Text = "View table";
            this.tmsiViewTable.Click += new System.EventHandler(this.tsmiView_Click);
            // 
            // tscResizeTable
            // 
            this.tscResizeTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscResizeTable.Name = "tscResizeTable";
            this.tscResizeTable.Size = new System.Drawing.Size(141, 23);
            this.tscResizeTable.SelectedIndexChanged += new System.EventHandler(this.tscResizeTable_SelectedIndexChanged);
            // 
            // cmsfolder
            // 
            this.cmsfolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.toolStripSeparator1,
            this.tsmiRemoveIST,
            this.tsmi_RemoveISTTable,
            this.tsmiRemoveBagPaxPlan,
            this.tsmiRemoveDetails});
            this.cmsfolder.Name = "cmsfolder";
            this.cmsfolder.ShowImageMargin = false;
            this.cmsfolder.Size = new System.Drawing.Size(240, 120);
            this.cmsfolder.MouseLeave += new System.EventHandler(this.cms_MouseLeave);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.tsmiRename_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
            // tsmiRemoveIST
            // 
            this.tsmiRemoveIST.Name = "tsmiRemoveIST";
            this.tsmiRemoveIST.Size = new System.Drawing.Size(239, 22);
            this.tsmiRemoveIST.Text = "Remove IST data";
            this.tsmiRemoveIST.Click += new System.EventHandler(this.tsmiRemove_Click);
            // 
            // tsmi_RemoveISTTable
            // 
            this.tsmi_RemoveISTTable.Name = "tsmi_RemoveISTTable";
            this.tsmi_RemoveISTTable.Size = new System.Drawing.Size(239, 22);
            this.tsmi_RemoveISTTable.Text = "Remove IST tables (Keep charts)";
            this.tsmi_RemoveISTTable.Click += new System.EventHandler(this.tsmiRemove_Click);
            // 
            // tsmiRemoveBagPaxPlan
            // 
            this.tsmiRemoveBagPaxPlan.Name = "tsmiRemoveBagPaxPlan";
            this.tsmiRemoveBagPaxPlan.Size = new System.Drawing.Size(239, 22);
            this.tsmiRemoveBagPaxPlan.Text = "Remove BagPlan and PaxPlan tables";
            this.tsmiRemoveBagPaxPlan.Click += new System.EventHandler(this.tsmiRemove_Click);
            // 
            // tsmiRemoveDetails
            // 
            this.tsmiRemoveDetails.Name = "tsmiRemoveDetails";
            this.tsmiRemoveDetails.Size = new System.Drawing.Size(239, 22);
            this.tsmiRemoveDetails.Text = "Remove individual Stations results";
            this.tsmiRemoveDetails.Click += new System.EventHandler(this.tsmiRemove_Click);
            // 
            // cmsView
            // 
            this.cmsView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChartView});
            this.cmsView.Name = "cmsChart";
            this.cmsView.ShowImageMargin = false;
            this.cmsView.Size = new System.Drawing.Size(76, 26);
            this.cmsView.MouseLeave += new System.EventHandler(this.cms_MouseLeave);
            // 
            // tsmiChartView
            // 
            this.tsmiChartView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tsmiChartView.Name = "tsmiChartView";
            this.tsmiChartView.Size = new System.Drawing.Size(75, 22);
            this.tsmiChartView.Text = "View";
            this.tsmiChartView.Click += new System.EventHandler(this.tsmiView_Click);
            // 
            // TreeEditor
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(1026, 544);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_final);
            this.Controls.Add(this.lbl_original);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tv_original);
            this.Controls.Add(this.bt_insertRoot);
            this.Controls.Add(this.tv_final);
            this.Controls.Add(this.bt_delete);
            this.Controls.Add(this.bt_CreateNote);
            this.Controls.Add(this.bt_CreateDirectory);
            this.Controls.Add(this.bt_up);
            this.Controls.Add(this.bt_right);
            this.Controls.Add(this.bt_insert);
            this.Controls.Add(this.bt_left);
            this.Controls.Add(this.bt_down);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(919, 582);
            this.Name = "TreeEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report content editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TreeEditor_FormClosed);
            this.cmsParagraph.ResumeLayout(false);
            this.cmsTable.ResumeLayout(false);
            this.cmsfolder.ResumeLayout(false);
            this.cmsView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_insert;
        private System.Windows.Forms.TreeView tv_final;
        private System.Windows.Forms.TreeView tv_original;
        private System.Windows.Forms.Button bt_up;
        private System.Windows.Forms.Button bt_down;
        private System.Windows.Forms.Button bt_left;
        private System.Windows.Forms.Button bt_right;
        private System.Windows.Forms.Button bt_delete;
        private System.Windows.Forms.Button bt_CreateDirectory;
        private System.Windows.Forms.Button bt_CreateNote;
        private System.Windows.Forms.Button bt_insertRoot;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label lbl_original;
        private System.Windows.Forms.Label lbl_final;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmsParagraph;
        private System.Windows.Forms.ToolStripMenuItem editParagraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteParagraphToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip cmsTable;
        private System.Windows.Forms.ToolStripComboBox tscResizeTable;
        private System.Windows.Forms.ContextMenuStrip cmsfolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveIST;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveBagPaxPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveDetails;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip cmsView;
        private System.Windows.Forms.ToolStripMenuItem tsmiChartView;
        private System.Windows.Forms.ToolStripMenuItem tmsiViewTable;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RemoveISTTable;
    }
}