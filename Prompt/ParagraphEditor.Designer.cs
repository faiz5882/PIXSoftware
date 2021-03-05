namespace SIMCORE_TOOL.Prompt
{
    partial class ParagraphEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParagraphEditor));
            this.pHtmlEditor = new System.Windows.Forms.Panel();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_title = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btn_change = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pHtmlEditor
            // 
            this.pHtmlEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pHtmlEditor.Location = new System.Drawing.Point(12, 36);
            this.pHtmlEditor.Name = "pHtmlEditor";
            this.pHtmlEditor.Size = new System.Drawing.Size(574, 273);
            this.pHtmlEditor.TabIndex = 0;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_name.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_name.Location = new System.Drawing.Point(12, 9);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(63, 19);
            this.lbl_name.TabIndex = 1;
            this.lbl_name.Text = "Name: ";
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_title.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_title.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_title.Location = new System.Drawing.Point(358, 9);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(51, 19);
            this.lbl_title.TabIndex = 2;
            this.lbl_title.Text = "Title: ";
            this.lbl_title.Visible = false;
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(81, 10);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(150, 20);
            this.tb_name.TabIndex = 3;
            this.tb_name.Leave += new System.EventHandler(this.tb_name_Leave);
            // 
            // tb_title
            // 
            this.tb_title.Location = new System.Drawing.Point(415, 10);
            this.tb_title.Name = "tb_title";
            this.tb_title.Size = new System.Drawing.Size(150, 20);
            this.tb_title.TabIndex = 4;
            this.tb_title.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(512, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(12, 315);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(74, 24);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btn_change
            // 
            this.btn_change.Location = new System.Drawing.Point(237, 8);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(64, 23);
            this.btn_change.TabIndex = 7;
            this.btn_change.Text = "Change";
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Visible = false;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // ParagraphEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(598, 351);
            this.Controls.Add(this.btn_change);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tb_title);
            this.Controls.Add(this.tb_name);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.pHtmlEditor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ParagraphEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report note  editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pHtmlEditor;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_title;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btn_change;
    }
}