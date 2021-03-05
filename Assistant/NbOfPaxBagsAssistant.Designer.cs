namespace SIMCORE_TOOL.Assistant
{
    partial class NbOfPaxBagsAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NbOfPaxBagsAssistant));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.transferringDepartureTextBox = new System.Windows.Forms.TextBox();
            this.originatingDepartureTextBox = new System.Windows.Forms.TextBox();
            this.transferringDepartureLabel = new System.Windows.Forms.Label();
            this.originatingDepartureLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.transferringArrivalTextBox = new System.Windows.Forms.TextBox();
            this.terminatingArrivalTextBox = new System.Windows.Forms.TextBox();
            this.transferringArrivalLabel = new System.Windows.Forms.Label();
            this.terminatingArrivalLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(36, 225);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(163, 225);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.transferringDepartureTextBox);
            this.groupBox1.Controls.Add(this.originatingDepartureTextBox);
            this.groupBox1.Controls.Add(this.transferringDepartureLabel);
            this.groupBox1.Controls.Add(this.originatingDepartureLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Departure";
            // 
            // transferringDepartureTextBox
            // 
            this.transferringDepartureTextBox.Location = new System.Drawing.Point(181, 64);
            this.transferringDepartureTextBox.Name = "transferringDepartureTextBox";
            this.transferringDepartureTextBox.Size = new System.Drawing.Size(45, 20);
            this.transferringDepartureTextBox.TabIndex = 3;
            this.transferringDepartureTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // originatingDepartureTextBox
            // 
            this.originatingDepartureTextBox.Location = new System.Drawing.Point(181, 34);
            this.originatingDepartureTextBox.Name = "originatingDepartureTextBox";
            this.originatingDepartureTextBox.Size = new System.Drawing.Size(45, 20);
            this.originatingDepartureTextBox.TabIndex = 2;
            this.originatingDepartureTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // transferringDepartureLabel
            // 
            this.transferringDepartureLabel.AutoSize = true;
            this.transferringDepartureLabel.Location = new System.Drawing.Point(21, 64);
            this.transferringDepartureLabel.Name = "transferringDepartureLabel";
            this.transferringDepartureLabel.Size = new System.Drawing.Size(35, 13);
            this.transferringDepartureLabel.TabIndex = 1;
            this.transferringDepartureLabel.Text = "label2";
            // 
            // originatingDepartureLabel
            // 
            this.originatingDepartureLabel.AutoSize = true;
            this.originatingDepartureLabel.Location = new System.Drawing.Point(21, 37);
            this.originatingDepartureLabel.Name = "originatingDepartureLabel";
            this.originatingDepartureLabel.Size = new System.Drawing.Size(35, 13);
            this.originatingDepartureLabel.TabIndex = 0;
            this.originatingDepartureLabel.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.transferringArrivalTextBox);
            this.groupBox2.Controls.Add(this.terminatingArrivalTextBox);
            this.groupBox2.Controls.Add(this.transferringArrivalLabel);
            this.groupBox2.Controls.Add(this.terminatingArrivalLabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 92);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Arrival";
            // 
            // transferringArrivalTextBox
            // 
            this.transferringArrivalTextBox.Location = new System.Drawing.Point(181, 56);
            this.transferringArrivalTextBox.Name = "transferringArrivalTextBox";
            this.transferringArrivalTextBox.Size = new System.Drawing.Size(45, 20);
            this.transferringArrivalTextBox.TabIndex = 3;
            this.transferringArrivalTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // terminatingArrivalTextBox
            // 
            this.terminatingArrivalTextBox.Location = new System.Drawing.Point(181, 28);
            this.terminatingArrivalTextBox.Name = "terminatingArrivalTextBox";
            this.terminatingArrivalTextBox.Size = new System.Drawing.Size(45, 20);
            this.terminatingArrivalTextBox.TabIndex = 2;
            this.terminatingArrivalTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // transferringArrivalLabel
            // 
            this.transferringArrivalLabel.AutoSize = true;
            this.transferringArrivalLabel.Location = new System.Drawing.Point(21, 63);
            this.transferringArrivalLabel.Name = "transferringArrivalLabel";
            this.transferringArrivalLabel.Size = new System.Drawing.Size(35, 13);
            this.transferringArrivalLabel.TabIndex = 1;
            this.transferringArrivalLabel.Text = "label4";
            // 
            // terminatingArrivalLabel
            // 
            this.terminatingArrivalLabel.AutoSize = true;
            this.terminatingArrivalLabel.Location = new System.Drawing.Point(21, 31);
            this.terminatingArrivalLabel.Name = "terminatingArrivalLabel";
            this.terminatingArrivalLabel.Size = new System.Drawing.Size(35, 13);
            this.terminatingArrivalLabel.TabIndex = 0;
            this.terminatingArrivalLabel.Text = "label3";
            // 
            // NbOfPaxBagsAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NbOfPaxBagsAssistant";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label transferringDepartureLabel;
        private System.Windows.Forms.Label originatingDepartureLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label transferringArrivalLabel;
        private System.Windows.Forms.Label terminatingArrivalLabel;
        private System.Windows.Forms.TextBox transferringDepartureTextBox;
        private System.Windows.Forms.TextBox originatingDepartureTextBox;
        private System.Windows.Forms.TextBox transferringArrivalTextBox;
        private System.Windows.Forms.TextBox terminatingArrivalTextBox;
    }
}