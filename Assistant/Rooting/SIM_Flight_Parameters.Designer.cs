namespace SIMCORE_TOOL.Assistant
{
    partial class SIM_Flight_Parameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Flight_Parameters));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lst_Available = new System.Windows.Forms.ListBox();
            this.lst_Visible = new System.Windows.Forms.ListBox();
            this.bt_right = new System.Windows.Forms.Button();
            this.bt_left = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(69, 403);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 13;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(423, 403);
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
            this.lst_Available.Location = new System.Drawing.Point(12, 48);
            this.lst_Available.Name = "lst_Available";
            this.lst_Available.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_Available.Size = new System.Drawing.Size(227, 329);
            this.lst_Available.TabIndex = 15;
            this.lst_Available.DoubleClick += new System.EventHandler(this.bt_right_Click);
            // 
            // lst_Visible
            // 
            this.lst_Visible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_Visible.FormattingEnabled = true;
            this.lst_Visible.Location = new System.Drawing.Point(324, 48);
            this.lst_Visible.Name = "lst_Visible";
            this.lst_Visible.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_Visible.Size = new System.Drawing.Size(227, 329);
            this.lst_Visible.TabIndex = 16;
            this.lst_Visible.SelectedIndexChanged += new System.EventHandler(this.lst_Visible_SelectedIndexChanged);
            this.lst_Visible.DoubleClick += new System.EventHandler(this.bt_left_Click);
            // 
            // bt_right
            // 
            this.bt_right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_right.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_right;
            this.bt_right.Location = new System.Drawing.Point(262, 163);
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
            this.bt_left.Location = new System.Drawing.Point(262, 206);
            this.bt_left.Name = "bt_left";
            this.bt_left.Size = new System.Drawing.Size(47, 23);
            this.bt_left.TabIndex = 17;
            this.bt_left.UseVisualStyleBackColor = true;
            this.bt_left.Click += new System.EventHandler(this.bt_left_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(324, 383);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(45, 17);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "Ban";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Available Items";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(319, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Selected Items";
            // 
            // SIM_Flight_Parameters
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(569, 442);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.bt_right);
            this.Controls.Add(this.bt_left);
            this.Controls.Add(this.lst_Visible);
            this.Controls.Add(this.lst_Available);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(585, 480);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(577, 464);
            this.Name = "SIM_Flight_Parameters";
            this.ShowInTaskbar = false;
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}