namespace SIMCORE_TOOL.Assistant
{
    partial class OCT_Assistant_CheckIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OCT_Assistant_CheckIn));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.partialOpeningTimeTextBox = new System.Windows.Forms.TextBox();
            this.closingTimeTextBox = new System.Windows.Forms.TextBox();
            this.openingTimeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nbAdditionalCheckInsForOverlappingFlightsTextBox = new System.Windows.Forms.TextBox();
            this.nbOpenedCheckInsAtPartialOpeningTextBox = new System.Windows.Forms.TextBox();
            this.nbAllocatedCheckInsTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flightCategoriesComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.partialOpeningTimeTextBox);
            this.groupBox1.Controls.Add(this.closingTimeTextBox);
            this.groupBox1.Controls.Add(this.openingTimeTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(25, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opening/Closing Times";
            // 
            // partialOpeningTimeTextBox
            // 
            this.partialOpeningTimeTextBox.Location = new System.Drawing.Point(289, 81);
            this.partialOpeningTimeTextBox.Name = "partialOpeningTimeTextBox";
            this.partialOpeningTimeTextBox.Size = new System.Drawing.Size(51, 20);
            this.partialOpeningTimeTextBox.TabIndex = 5;
            this.partialOpeningTimeTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // closingTimeTextBox
            // 
            this.closingTimeTextBox.Location = new System.Drawing.Point(289, 55);
            this.closingTimeTextBox.Name = "closingTimeTextBox";
            this.closingTimeTextBox.Size = new System.Drawing.Size(51, 20);
            this.closingTimeTextBox.TabIndex = 4;
            this.closingTimeTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // openingTimeTextBox
            // 
            this.openingTimeTextBox.Location = new System.Drawing.Point(289, 29);
            this.openingTimeTextBox.Name = "openingTimeTextBox";
            this.openingTimeTextBox.Size = new System.Drawing.Size(51, 20);
            this.openingTimeTextBox.TabIndex = 3;
            this.openingTimeTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Check-In Partial Opening Time (Min before STD) :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Check-In Closing Time (Min before STD) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Check-In Opening Time (Min before STD) :";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.nbAdditionalCheckInsForOverlappingFlightsTextBox);
            this.groupBox2.Controls.Add(this.nbOpenedCheckInsAtPartialOpeningTextBox);
            this.groupBox2.Controls.Add(this.nbAllocatedCheckInsTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(25, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 104);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Allocated Check-In Stations per Flight";
            // 
            // nbAdditionalCheckInsForOverlappingFlightsTextBox
            // 
            this.nbAdditionalCheckInsForOverlappingFlightsTextBox.Location = new System.Drawing.Point(289, 74);
            this.nbAdditionalCheckInsForOverlappingFlightsTextBox.Name = "nbAdditionalCheckInsForOverlappingFlightsTextBox";
            this.nbAdditionalCheckInsForOverlappingFlightsTextBox.Size = new System.Drawing.Size(51, 20);
            this.nbAdditionalCheckInsForOverlappingFlightsTextBox.TabIndex = 5;
            this.nbAdditionalCheckInsForOverlappingFlightsTextBox.Leave += new System.EventHandler(this.textBox_TextChanged);
            // 
            // nbOpenedCheckInsAtPartialOpeningTextBox
            // 
            this.nbOpenedCheckInsAtPartialOpeningTextBox.Location = new System.Drawing.Point(289, 48);
            this.nbOpenedCheckInsAtPartialOpeningTextBox.Name = "nbOpenedCheckInsAtPartialOpeningTextBox";
            this.nbOpenedCheckInsAtPartialOpeningTextBox.Size = new System.Drawing.Size(51, 20);
            this.nbOpenedCheckInsAtPartialOpeningTextBox.TabIndex = 4;
            this.nbOpenedCheckInsAtPartialOpeningTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // nbAllocatedCheckInsTextBox
            // 
            this.nbAllocatedCheckInsTextBox.Location = new System.Drawing.Point(289, 22);
            this.nbAllocatedCheckInsTextBox.Name = "nbAllocatedCheckInsTextBox";
            this.nbAllocatedCheckInsTextBox.Size = new System.Drawing.Size(51, 20);
            this.nbAllocatedCheckInsTextBox.TabIndex = 3;
            this.nbAllocatedCheckInsTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(271, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Number of Additional Check-In(s) for overlapping flights :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(247, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Number of Opened Check-In(s) at Partial Opening :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Number of allocated Check-In(s) :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(77, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Category :";
            // 
            // flightCategoriesComboBox
            // 
            this.flightCategoriesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flightCategoriesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flightCategoriesComboBox.FormattingEnabled = true;
            this.flightCategoriesComboBox.Location = new System.Drawing.Point(175, 26);
            this.flightCategoriesComboBox.Name = "flightCategoriesComboBox";
            this.flightCategoriesComboBox.Size = new System.Drawing.Size(190, 21);
            this.flightCategoriesComboBox.TabIndex = 3;
            this.flightCategoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.flightCategoriesComboBox_SelectedIndexChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(65, 303);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(248, 303);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // OCT_Assistant_CheckIn
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(404, 343);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.flightCategoriesComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OCT_Assistant_CheckIn";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check-In Opening/Closing Times Assistant";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox flightCategoriesComboBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox partialOpeningTimeTextBox;
        private System.Windows.Forms.TextBox closingTimeTextBox;
        private System.Windows.Forms.TextBox openingTimeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nbAdditionalCheckInsForOverlappingFlightsTextBox;
        private System.Windows.Forms.TextBox nbOpenedCheckInsAtPartialOpeningTextBox;
        private System.Windows.Forms.TextBox nbAllocatedCheckInsTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}