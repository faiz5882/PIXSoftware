namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    partial class ReportGroupEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportGroupEditor));
            this.reportGroupsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addNewGroupButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.newGroupTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // reportGroupsCheckedListBox
            // 
            this.reportGroupsCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportGroupsCheckedListBox.FormattingEnabled = true;
            this.reportGroupsCheckedListBox.Location = new System.Drawing.Point(12, 45);
            this.reportGroupsCheckedListBox.MultiColumn = true;
            this.reportGroupsCheckedListBox.Name = "reportGroupsCheckedListBox";
            this.reportGroupsCheckedListBox.Size = new System.Drawing.Size(320, 124);
            this.reportGroupsCheckedListBox.TabIndex = 0;
            this.reportGroupsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.reportGroupsCheckedListBox_ItemCheck);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(34, 184);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(110, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(194, 184);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(110, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // addNewGroupButton
            // 
            this.addNewGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addNewGroupButton.Location = new System.Drawing.Point(282, 8);
            this.addNewGroupButton.Name = "addNewGroupButton";
            this.addNewGroupButton.Size = new System.Drawing.Size(50, 23);
            this.addNewGroupButton.TabIndex = 3;
            this.addNewGroupButton.Text = "Add";
            this.addNewGroupButton.UseVisualStyleBackColor = true;
            this.addNewGroupButton.Click += new System.EventHandler(this.addNewGroupButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Create new report group";
            // 
            // newGroupTextBox
            // 
            this.newGroupTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newGroupTextBox.Location = new System.Drawing.Point(133, 10);
            this.newGroupTextBox.Name = "newGroupTextBox";
            this.newGroupTextBox.Size = new System.Drawing.Size(143, 20);
            this.newGroupTextBox.TabIndex = 5;
            this.newGroupTextBox.Leave += new System.EventHandler(this.newGroupTextBox_Leave);
            // 
            // ReportGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 219);
            this.Controls.Add(this.newGroupTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addNewGroupButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.reportGroupsCheckedListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportGroupEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Assign Report to Group";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox reportGroupsCheckedListBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addNewGroupButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newGroupTextBox;
    }
}