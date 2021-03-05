namespace SIMCORE_TOOL.com.crispico.generalUse
{
    partial class OverwriteFilesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverwriteFilesDialog));
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.applyAllCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // yesButton
            // 
            this.yesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yesButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.yesButton.Location = new System.Drawing.Point(12, 83);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(115, 23);
            this.yesButton.TabIndex = 0;
            this.yesButton.Text = "&Yes";
            this.yesButton.UseVisualStyleBackColor = true;
            // 
            // noButton
            // 
            this.noButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.noButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.noButton.Location = new System.Drawing.Point(137, 83);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(115, 23);
            this.noButton.TabIndex = 1;
            this.noButton.Text = "&No";
            this.noButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(336, 83);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(115, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.BackColor = System.Drawing.Color.Transparent;
            this.messageLabel.Location = new System.Drawing.Point(6, 16);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(35, 13);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.Text = "label1";
            // 
            // applyAllCheckBox
            // 
            this.applyAllCheckBox.AutoSize = true;
            this.applyAllCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.applyAllCheckBox.Location = new System.Drawing.Point(12, 63);
            this.applyAllCheckBox.Name = "applyAllCheckBox";
            this.applyAllCheckBox.Size = new System.Drawing.Size(98, 17);
            this.applyAllCheckBox.TabIndex = 5;
            this.applyAllCheckBox.Text = "Apply to all files";
            this.applyAllCheckBox.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.messageLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 45);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // OverwriteFilesDialog
            // 
            this.AcceptButton = this.yesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(463, 119);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.applyAllCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OverwriteFilesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Overwrite files";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.CheckBox applyAllCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}