namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    partial class CloneGraphicFilterNameEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloneGraphicFilterNameEditor));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chartNameTextBox = new System.Windows.Forms.TextBox();
            this.chartTitleTextBox = new System.Windows.Forms.TextBox();
            this.changeTitleCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(32, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter then new chart name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(32, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please enter the new chart title :";
            // 
            // chartNameTextBox
            // 
            this.chartNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chartNameTextBox.Location = new System.Drawing.Point(223, 24);
            this.chartNameTextBox.Name = "chartNameTextBox";
            this.chartNameTextBox.Size = new System.Drawing.Size(269, 20);
            this.chartNameTextBox.TabIndex = 2;
            // 
            // chartTitleTextBox
            // 
            this.chartTitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chartTitleTextBox.Enabled = false;
            this.chartTitleTextBox.Location = new System.Drawing.Point(222, 57);
            this.chartTitleTextBox.Name = "chartTitleTextBox";
            this.chartTitleTextBox.Size = new System.Drawing.Size(270, 20);
            this.chartTitleTextBox.TabIndex = 3;
            // 
            // changeTitleCheckBox
            // 
            this.changeTitleCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeTitleCheckBox.AutoSize = true;
            this.changeTitleCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.changeTitleCheckBox.Location = new System.Drawing.Point(508, 59);
            this.changeTitleCheckBox.Name = "changeTitleCheckBox";
            this.changeTitleCheckBox.Size = new System.Drawing.Size(100, 17);
            this.changeTitleCheckBox.TabIndex = 4;
            this.changeTitleCheckBox.Text = "Change the title";
            this.changeTitleCheckBox.UseVisualStyleBackColor = false;
            this.changeTitleCheckBox.CheckedChanged += new System.EventHandler(this.changeTitleCheckBox_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(252, 90);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(131, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // CloneGraphicFilterNameEditor
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 125);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.changeTitleCheckBox);
            this.Controls.Add(this.chartTitleTextBox);
            this.Controls.Add(this.chartNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloneGraphicFilterNameEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chart Name Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox chartNameTextBox;
        private System.Windows.Forms.TextBox chartTitleTextBox;
        private System.Windows.Forms.CheckBox changeTitleCheckBox;
        private System.Windows.Forms.Button okButton;
    }
}