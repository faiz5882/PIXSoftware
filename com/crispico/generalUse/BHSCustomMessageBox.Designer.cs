namespace SIMCORE_TOOL.com.crispico.generalUse
{
    partial class BHSCustomMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHSCustomMessageBox));
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.generateLocalISTCheckBox = new System.Windows.Forms.CheckBox();
            this.messageBoxLabel = new System.Windows.Forms.Label();
            this.generateGroupISTCheckBox = new System.Windows.Forms.CheckBox();
            this.generateMUPSegregationCheckBox = new System.Windows.Forms.CheckBox();
            this.copyOutputTablesCheckBox = new System.Windows.Forms.CheckBox();
            this.editResultFitlersButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // yesButton
            // 
            this.yesButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.yesButton.Location = new System.Drawing.Point(114, 187);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(75, 23);
            this.yesButton.TabIndex = 0;
            this.yesButton.Text = "&Yes";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // noButton
            // 
            this.noButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.noButton.Location = new System.Drawing.Point(300, 187);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(75, 23);
            this.noButton.TabIndex = 1;
            this.noButton.Text = "&No";
            this.noButton.UseVisualStyleBackColor = true;
            // 
            // generateLocalISTCheckBox
            // 
            this.generateLocalISTCheckBox.AutoSize = true;
            this.generateLocalISTCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.generateLocalISTCheckBox.Location = new System.Drawing.Point(262, 125);
            this.generateLocalISTCheckBox.Name = "generateLocalISTCheckBox";
            this.generateLocalISTCheckBox.Size = new System.Drawing.Size(162, 17);
            this.generateLocalISTCheckBox.TabIndex = 2;
            this.generateLocalISTCheckBox.Text = "Generate Stations IST tables";
            this.generateLocalISTCheckBox.UseVisualStyleBackColor = false;
            // 
            // messageBoxLabel
            // 
            this.messageBoxLabel.AutoSize = true;
            this.messageBoxLabel.BackColor = System.Drawing.Color.Transparent;
            this.messageBoxLabel.Location = new System.Drawing.Point(12, 9);
            this.messageBoxLabel.Name = "messageBoxLabel";
            this.messageBoxLabel.Size = new System.Drawing.Size(16, 13);
            this.messageBoxLabel.TabIndex = 3;
            this.messageBoxLabel.Text = "...";
            // 
            // generateGroupISTCheckBox
            // 
            this.generateGroupISTCheckBox.AutoSize = true;
            this.generateGroupISTCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.generateGroupISTCheckBox.Location = new System.Drawing.Point(262, 103);
            this.generateGroupISTCheckBox.Name = "generateGroupISTCheckBox";
            this.generateGroupISTCheckBox.Size = new System.Drawing.Size(158, 17);
            this.generateGroupISTCheckBox.TabIndex = 4;
            this.generateGroupISTCheckBox.Text = "Generate Groups IST tables";
            this.generateGroupISTCheckBox.UseVisualStyleBackColor = false;
            // 
            // generateMUPSegregationCheckBox
            // 
            this.generateMUPSegregationCheckBox.AutoSize = true;
            this.generateMUPSegregationCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.generateMUPSegregationCheckBox.Location = new System.Drawing.Point(27, 126);
            this.generateMUPSegregationCheckBox.Name = "generateMUPSegregationCheckBox";
            this.generateMUPSegregationCheckBox.Size = new System.Drawing.Size(173, 17);
            this.generateMUPSegregationCheckBox.TabIndex = 5;
            this.generateMUPSegregationCheckBox.Text = "Generate Make-up segregation";
            this.generateMUPSegregationCheckBox.UseVisualStyleBackColor = false;
            // 
            // copyOutputTablesCheckBox
            // 
            this.copyOutputTablesCheckBox.AutoSize = true;
            this.copyOutputTablesCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.copyOutputTablesCheckBox.Location = new System.Drawing.Point(27, 103);
            this.copyOutputTablesCheckBox.Name = "copyOutputTablesCheckBox";
            this.copyOutputTablesCheckBox.Size = new System.Drawing.Size(207, 17);
            this.copyOutputTablesCheckBox.TabIndex = 6;
            this.copyOutputTablesCheckBox.Text = "Copy output tables in the project folder";
            this.copyOutputTablesCheckBox.UseVisualStyleBackColor = false;
            // 
            // editResultFitlersButton
            // 
            this.editResultFitlersButton.Location = new System.Drawing.Point(269, 148);
            this.editResultFitlersButton.Name = "editResultFitlersButton";
            this.editResultFitlersButton.Size = new System.Drawing.Size(142, 23);
            this.editResultFitlersButton.TabIndex = 7;
            this.editResultFitlersButton.Text = "Edit Result Filters";
            this.editResultFitlersButton.UseVisualStyleBackColor = true;
            this.editResultFitlersButton.Click += new System.EventHandler(this.editResultFitlersButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.messageBoxLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 79);
            this.panel1.TabIndex = 8;
            // 
            // BHSCustomMessageBox
            // 
            this.AcceptButton = this.yesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.noButton;
            this.ClientSize = new System.Drawing.Size(496, 222);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.editResultFitlersButton);
            this.Controls.Add(this.copyOutputTablesCheckBox);
            this.Controls.Add(this.generateMUPSegregationCheckBox);
            this.Controls.Add(this.generateGroupISTCheckBox);
            this.Controls.Add(this.generateLocalISTCheckBox);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BHSCustomMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Warning";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.CheckBox generateLocalISTCheckBox;
        private System.Windows.Forms.Label messageBoxLabel;
        private System.Windows.Forms.CheckBox generateGroupISTCheckBox;
        private System.Windows.Forms.CheckBox generateMUPSegregationCheckBox;
        private System.Windows.Forms.CheckBox copyOutputTablesCheckBox;
        private System.Windows.Forms.Button editResultFitlersButton;
        private System.Windows.Forms.Panel panel1;
    }
}