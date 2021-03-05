namespace SIMCORE_TOOL.Prompt.Liege
{
    partial class PrioritiesAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrioritiesAssistant));
            this.label1 = new System.Windows.Forms.Label();
            this.flightCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.moveRightButton = new System.Windows.Forms.Button();
            this.moveLeftButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.availableResourcesListBox = new System.Windows.Forms.ListBox();
            this.prioritiesListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the Flight Category :";
            // 
            // flightCategoryComboBox
            // 
            this.flightCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flightCategoryComboBox.FormattingEnabled = true;
            this.flightCategoryComboBox.Location = new System.Drawing.Point(222, 30);
            this.flightCategoryComboBox.Name = "flightCategoryComboBox";
            this.flightCategoryComboBox.Size = new System.Drawing.Size(226, 21);
            this.flightCategoryComboBox.TabIndex = 1;
            this.flightCategoryComboBox.SelectedIndexChanged += new System.EventHandler(this.flightCategoryComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(33, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Available Resources";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(314, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Priorities";
            // 
            // moveRightButton
            // 
            this.moveRightButton.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_right;
            this.moveRightButton.Location = new System.Drawing.Point(213, 228);
            this.moveRightButton.Name = "moveRightButton";
            this.moveRightButton.Size = new System.Drawing.Size(47, 23);
            this.moveRightButton.TabIndex = 6;
            this.moveRightButton.UseVisualStyleBackColor = true;
            this.moveRightButton.Click += new System.EventHandler(this.moveRightButton_Click);
            // 
            // moveLeftButton
            // 
            this.moveLeftButton.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_left;
            this.moveLeftButton.Location = new System.Drawing.Point(213, 257);
            this.moveLeftButton.Name = "moveLeftButton";
            this.moveLeftButton.Size = new System.Drawing.Size(47, 23);
            this.moveLeftButton.TabIndex = 7;
            this.moveLeftButton.UseVisualStyleBackColor = true;
            this.moveLeftButton.Click += new System.EventHandler(this.moveLeftButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_up;
            this.moveUpButton.Location = new System.Drawing.Point(304, 428);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(47, 23);
            this.moveUpButton.TabIndex = 8;
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_down;
            this.moveDownButton.Location = new System.Drawing.Point(357, 428);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(47, 23);
            this.moveDownButton.TabIndex = 9;
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(86, 468);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(316, 468);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // availableResourcesListBox
            // 
            this.availableResourcesListBox.FormattingEnabled = true;
            this.availableResourcesListBox.Location = new System.Drawing.Point(23, 106);
            this.availableResourcesListBox.Name = "availableResourcesListBox";
            this.availableResourcesListBox.Size = new System.Drawing.Size(184, 316);
            this.availableResourcesListBox.TabIndex = 12;
            // 
            // prioritiesListBox
            // 
            this.prioritiesListBox.FormattingEnabled = true;
            this.prioritiesListBox.Location = new System.Drawing.Point(266, 106);
            this.prioritiesListBox.Name = "prioritiesListBox";
            this.prioritiesListBox.Size = new System.Drawing.Size(184, 316);
            this.prioritiesListBox.TabIndex = 13;
            // 
            // PrioritiesAssistant
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(490, 503);
            this.Controls.Add(this.prioritiesListBox);
            this.Controls.Add(this.availableResourcesListBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.moveLeftButton);
            this.Controls.Add(this.moveRightButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flightCategoryComboBox);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrioritiesAssistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Priorities Table Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox flightCategoryComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button moveRightButton;
        private System.Windows.Forms.Button moveLeftButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ListBox availableResourcesListBox;
        private System.Windows.Forms.ListBox prioritiesListBox;
    }
}