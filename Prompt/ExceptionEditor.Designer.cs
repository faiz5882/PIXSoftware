namespace SIMCORE_TOOL.Prompt
{
    partial class ExceptionEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionEditor));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lst_Available = new System.Windows.Forms.ListBox();
            this.lst_Visible = new System.Windows.Forms.ListBox();
            this.bt_right = new System.Windows.Forms.Button();
            this.bt_left = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btnClearSelectedItems = new System.Windows.Forms.Button();
            this.cbMultipleSearch = new System.Windows.Forms.CheckBox();
            this.cbLoadAllAirlineCodes = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(69, 516);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 13;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(542, 516);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 14;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // lst_Available
            // 
            this.lst_Available.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lst_Available.FormattingEnabled = true;
            this.lst_Available.Location = new System.Drawing.Point(12, 116);
            this.lst_Available.Name = "lst_Available";
            this.lst_Available.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_Available.Size = new System.Drawing.Size(282, 368);
            this.lst_Available.TabIndex = 15;
            this.lst_Available.DoubleClick += new System.EventHandler(this.bt_right_Click);
            // 
            // lst_Visible
            // 
            this.lst_Visible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_Visible.FormattingEnabled = true;
            this.lst_Visible.Location = new System.Drawing.Point(378, 51);
            this.lst_Visible.Name = "lst_Visible";
            this.lst_Visible.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_Visible.Size = new System.Drawing.Size(282, 433);
            this.lst_Visible.TabIndex = 16;
            this.lst_Visible.DoubleClick += new System.EventHandler(this.bt_left_Click);
            // 
            // bt_right
            // 
            this.bt_right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_right.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_right;
            this.bt_right.Location = new System.Drawing.Point(311, 243);
            this.bt_right.Name = "bt_right";
            this.bt_right.Size = new System.Drawing.Size(47, 23);
            this.bt_right.TabIndex = 18;
            this.bt_right.UseVisualStyleBackColor = true;
            this.bt_right.Click += new System.EventHandler(this.bt_right_Click);
            // 
            // bt_left
            // 
            this.bt_left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_left.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_left;
            this.bt_left.Location = new System.Drawing.Point(311, 285);
            this.bt_left.Name = "bt_left";
            this.bt_left.Size = new System.Drawing.Size(47, 23);
            this.bt_left.TabIndex = 17;
            this.bt_left.UseVisualStyleBackColor = true;
            this.bt_left.Click += new System.EventHandler(this.bt_left_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(195, 59);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 23);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(12, 62);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(177, 20);
            this.tbSearch.TabIndex = 20;
            this.tbSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearch_KeyPress);
            // 
            // btnClearSelectedItems
            // 
            this.btnClearSelectedItems.Location = new System.Drawing.Point(12, 89);
            this.btnClearSelectedItems.Name = "btnClearSelectedItems";
            this.btnClearSelectedItems.Size = new System.Drawing.Size(282, 23);
            this.btnClearSelectedItems.TabIndex = 21;
            this.btnClearSelectedItems.Text = "Clear Selected Items";
            this.btnClearSelectedItems.UseVisualStyleBackColor = true;
            this.btnClearSelectedItems.Click += new System.EventHandler(this.btnClearSelectedItems_Click);
            // 
            // cbMultipleSearch
            // 
            this.cbMultipleSearch.AutoSize = true;
            this.cbMultipleSearch.BackColor = System.Drawing.Color.Transparent;
            this.cbMultipleSearch.Location = new System.Drawing.Point(12, 35);
            this.cbMultipleSearch.Name = "cbMultipleSearch";
            this.cbMultipleSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbMultipleSearch.Size = new System.Drawing.Size(145, 17);
            this.cbMultipleSearch.TabIndex = 22;
            this.cbMultipleSearch.Text = "Multiple Data Exact Input";
            this.cbMultipleSearch.UseVisualStyleBackColor = false;
            this.cbMultipleSearch.CheckedChanged += new System.EventHandler(this.cbMultipleSearch_CheckedChanged);
            // 
            // cbLoadAllAirlineCodes
            // 
            this.cbLoadAllAirlineCodes.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbLoadAllAirlineCodes.AutoSize = true;
            this.cbLoadAllAirlineCodes.Location = new System.Drawing.Point(12, 6);
            this.cbLoadAllAirlineCodes.Name = "cbLoadAllAirlineCodes";
            this.cbLoadAllAirlineCodes.Size = new System.Drawing.Size(116, 23);
            this.cbLoadAllAirlineCodes.TabIndex = 23;
            this.cbLoadAllAirlineCodes.Text = "Load all airline codes";
            this.cbLoadAllAirlineCodes.UseVisualStyleBackColor = true;
            this.cbLoadAllAirlineCodes.CheckedChanged += new System.EventHandler(this.cbLoadAllAirlineCodes_CheckedChanged);
            // 
            // ExceptionEditor
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(682, 551);
            this.Controls.Add(this.cbLoadAllAirlineCodes);
            this.Controls.Add(this.cbMultipleSearch);
            this.Controls.Add(this.btnClearSelectedItems);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.bt_right);
            this.Controls.Add(this.bt_left);
            this.Controls.Add(this.lst_Visible);
            this.Controls.Add(this.lst_Available);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(577, 464);
            this.Name = "ExceptionEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExceptionEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ListBox lst_Available;
        private System.Windows.Forms.ListBox lst_Visible;
        private System.Windows.Forms.Button bt_right;
        private System.Windows.Forms.Button bt_left;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button btnClearSelectedItems;
        private System.Windows.Forms.CheckBox cbMultipleSearch;
        private System.Windows.Forms.CheckBox cbLoadAllAirlineCodes;

    }
}