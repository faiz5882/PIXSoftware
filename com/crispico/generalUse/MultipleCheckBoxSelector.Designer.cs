namespace SIMCORE_TOOL.com.crispico.generalUse
{
    partial class MultipleCheckBoxSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleCheckBoxSelector));
            this.AllGroupBox = new System.Windows.Forms.GroupBox();
            this.allCheckBox = new System.Windows.Forms.CheckBox();
            this.entitiesPanel = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.AllGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllGroupBox
            // 
            this.AllGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.AllGroupBox.Controls.Add(this.allCheckBox);
            this.AllGroupBox.Location = new System.Drawing.Point(12, 12);
            this.AllGroupBox.Name = "AllGroupBox";
            this.AllGroupBox.Size = new System.Drawing.Size(248, 44);
            this.AllGroupBox.TabIndex = 0;
            this.AllGroupBox.TabStop = false;
            // 
            // allCheckBox
            // 
            this.allCheckBox.AutoSize = true;
            this.allCheckBox.Location = new System.Drawing.Point(6, 19);
            this.allCheckBox.Name = "allCheckBox";
            this.allCheckBox.Size = new System.Drawing.Size(74, 17);
            this.allCheckBox.TabIndex = 0;
            this.allCheckBox.Text = "All Entities";
            this.allCheckBox.UseVisualStyleBackColor = true;
            this.allCheckBox.Click += new System.EventHandler(this.allCheckBox_Click);
            // 
            // entitiesPanel
            // 
            this.entitiesPanel.AutoScroll = true;
            this.entitiesPanel.BackColor = System.Drawing.Color.Transparent;
            this.entitiesPanel.Location = new System.Drawing.Point(12, 62);
            this.entitiesPanel.Name = "entitiesPanel";
            this.entitiesPanel.Size = new System.Drawing.Size(264, 176);
            this.entitiesPanel.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(42, 250);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(50, 21);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(190, 250);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(50, 21);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // MultipleCheckBoxSelector
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(288, 277);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.entitiesPanel);
            this.Controls.Add(this.AllGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultipleCheckBoxSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please select the Process Flow(s)";
            this.AllGroupBox.ResumeLayout(false);
            this.AllGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox AllGroupBox;
        private System.Windows.Forms.CheckBox allCheckBox;
        private System.Windows.Forms.Panel entitiesPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}