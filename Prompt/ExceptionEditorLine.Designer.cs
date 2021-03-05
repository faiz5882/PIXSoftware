namespace SIMCORE_TOOL.Prompt
{
    partial class ExceptionEditorLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionEditorLine));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lst_Available = new System.Windows.Forms.ListBox();
            this.lst_Visible = new System.Windows.Forms.ListBox();
            this.bt_right = new System.Windows.Forms.Button();
            this.bt_left = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(69, 391);
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
            this.btn_Cancel.Location = new System.Drawing.Point(421, 391);
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
            this.lst_Available.Location = new System.Drawing.Point(12, 12);
            this.lst_Available.Name = "lst_Available";
            this.lst_Available.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_Available.Size = new System.Drawing.Size(227, 355);
            this.lst_Available.TabIndex = 15;
            this.lst_Available.DoubleClick += new System.EventHandler(this.bt_right_Click);
            // 
            // lst_Visible
            // 
            this.lst_Visible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_Visible.FormattingEnabled = true;
            this.lst_Visible.Location = new System.Drawing.Point(322, 12);
            this.lst_Visible.Name = "lst_Visible";
            this.lst_Visible.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_Visible.Size = new System.Drawing.Size(227, 355);
            this.lst_Visible.TabIndex = 16;
            this.lst_Visible.DoubleClick += new System.EventHandler(this.bt_left_Click);
            // 
            // bt_right
            // 
            this.bt_right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_right.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_right;
            this.bt_right.Location = new System.Drawing.Point(260, 151);
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
            this.bt_left.Location = new System.Drawing.Point(260, 194);
            this.bt_left.Name = "bt_left";
            this.bt_left.Size = new System.Drawing.Size(47, 23);
            this.bt_left.TabIndex = 17;
            this.bt_left.UseVisualStyleBackColor = true;
            this.bt_left.Click += new System.EventHandler(this.bt_left_Click);
            // 
            // ExceptionEditorLine
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(561, 426);
            this.Controls.Add(this.bt_right);
            this.Controls.Add(this.bt_left);
            this.Controls.Add(this.lst_Visible);
            this.Controls.Add(this.lst_Available);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(577, 464);
            this.Name = "ExceptionEditorLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExceptionEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ListBox lst_Available;
        private System.Windows.Forms.ListBox lst_Visible;
        private System.Windows.Forms.Button bt_right;
        private System.Windows.Forms.Button bt_left;

    }
}