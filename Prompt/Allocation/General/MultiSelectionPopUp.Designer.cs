namespace SIMCORE_TOOL.Prompt.Allocation.General
{
    partial class MultiSelectionPopUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiSelectionPopUp));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.matchRadioButton = new System.Windows.Forms.RadioButton();
            this.containRadioButton = new System.Windows.Forms.RadioButton();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.selectedItemsRadioButton = new System.Windows.Forms.RadioButton();
            this.availableItemsRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.removeAllItemsButton = new System.Windows.Forms.Button();
            this.addAllItemsButton = new System.Windows.Forms.Button();
            this.clearSelectionButton = new System.Windows.Forms.Button();
            this.selectedListBox = new System.Windows.Forms.ListBox();
            this.removeFromSelectionButton = new System.Windows.Forms.Button();
            this.addToSelectionButton = new System.Windows.Forms.Button();
            this.availableListBox = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search and Select";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.matchRadioButton);
            this.groupBox4.Controls.Add(this.containRadioButton);
            this.groupBox4.Controls.Add(this.searchButton);
            this.groupBox4.Controls.Add(this.searchTextBox);
            this.groupBox4.Location = new System.Drawing.Point(6, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(542, 70);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search criteria";
            // 
            // matchRadioButton
            // 
            this.matchRadioButton.AutoSize = true;
            this.matchRadioButton.Checked = true;
            this.matchRadioButton.Location = new System.Drawing.Point(6, 19);
            this.matchRadioButton.Name = "matchRadioButton";
            this.matchRadioButton.Size = new System.Drawing.Size(140, 17);
            this.matchRadioButton.TabIndex = 0;
            this.matchRadioButton.TabStop = true;
            this.matchRadioButton.Text = "Search items that Match";
            this.matchRadioButton.UseVisualStyleBackColor = true;
            // 
            // containRadioButton
            // 
            this.containRadioButton.AutoSize = true;
            this.containRadioButton.Location = new System.Drawing.Point(6, 42);
            this.containRadioButton.Name = "containRadioButton";
            this.containRadioButton.Size = new System.Drawing.Size(146, 17);
            this.containRadioButton.TabIndex = 1;
            this.containRadioButton.Text = "Search items that Contain";
            this.containRadioButton.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(420, 28);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(116, 23);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(164, 30);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(209, 20);
            this.searchTextBox.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.selectedItemsRadioButton);
            this.groupBox3.Controls.Add(this.availableItemsRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(6, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(542, 43);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search in";
            // 
            // selectedItemsRadioButton
            // 
            this.selectedItemsRadioButton.AutoSize = true;
            this.selectedItemsRadioButton.Location = new System.Drawing.Point(285, 19);
            this.selectedItemsRadioButton.Name = "selectedItemsRadioButton";
            this.selectedItemsRadioButton.Size = new System.Drawing.Size(95, 17);
            this.selectedItemsRadioButton.TabIndex = 1;
            this.selectedItemsRadioButton.Text = "Selected Items";
            this.selectedItemsRadioButton.UseVisualStyleBackColor = true;
            // 
            // availableItemsRadioButton
            // 
            this.availableItemsRadioButton.AutoSize = true;
            this.availableItemsRadioButton.Checked = true;
            this.availableItemsRadioButton.Location = new System.Drawing.Point(136, 19);
            this.availableItemsRadioButton.Name = "availableItemsRadioButton";
            this.availableItemsRadioButton.Size = new System.Drawing.Size(96, 17);
            this.availableItemsRadioButton.TabIndex = 0;
            this.availableItemsRadioButton.TabStop = true;
            this.availableItemsRadioButton.Text = "Available Items";
            this.availableItemsRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.removeAllItemsButton);
            this.groupBox2.Controls.Add(this.addAllItemsButton);
            this.groupBox2.Controls.Add(this.clearSelectionButton);
            this.groupBox2.Controls.Add(this.selectedListBox);
            this.groupBox2.Controls.Add(this.removeFromSelectionButton);
            this.groupBox2.Controls.Add(this.addToSelectionButton);
            this.groupBox2.Controls.Add(this.availableListBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(554, 302);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(401, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Selected Items";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Available Items";
            // 
            // removeAllItemsButton
            // 
            this.removeAllItemsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeAllItemsButton.Location = new System.Drawing.Point(330, 240);
            this.removeAllItemsButton.Name = "removeAllItemsButton";
            this.removeAllItemsButton.Size = new System.Drawing.Size(211, 23);
            this.removeAllItemsButton.TabIndex = 6;
            this.removeAllItemsButton.Text = "Remove all items";
            this.removeAllItemsButton.UseVisualStyleBackColor = true;
            this.removeAllItemsButton.Click += new System.EventHandler(this.removeAllItemsButton_Click);
            // 
            // addAllItemsButton
            // 
            this.addAllItemsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addAllItemsButton.Location = new System.Drawing.Point(175, 269);
            this.addAllItemsButton.Name = "addAllItemsButton";
            this.addAllItemsButton.Size = new System.Drawing.Size(211, 23);
            this.addAllItemsButton.TabIndex = 5;
            this.addAllItemsButton.Text = "Add all items ->";
            this.addAllItemsButton.UseVisualStyleBackColor = true;
            this.addAllItemsButton.Click += new System.EventHandler(this.addAllItemsButton_Click);
            // 
            // clearSelectionButton
            // 
            this.clearSelectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearSelectionButton.Location = new System.Drawing.Point(6, 240);
            this.clearSelectionButton.Name = "clearSelectionButton";
            this.clearSelectionButton.Size = new System.Drawing.Size(211, 23);
            this.clearSelectionButton.TabIndex = 4;
            this.clearSelectionButton.Text = "Clear selection";
            this.clearSelectionButton.UseVisualStyleBackColor = true;
            this.clearSelectionButton.Click += new System.EventHandler(this.clearSelectionButton_Click);
            // 
            // selectedListBox
            // 
            this.selectedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedListBox.FormattingEnabled = true;
            this.selectedListBox.Location = new System.Drawing.Point(330, 35);
            this.selectedListBox.Name = "selectedListBox";
            this.selectedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectedListBox.Size = new System.Drawing.Size(211, 199);
            this.selectedListBox.TabIndex = 3;
            // 
            // removeFromSelectionButton
            // 
            this.removeFromSelectionButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.removeFromSelectionButton.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_left;
            this.removeFromSelectionButton.Location = new System.Drawing.Point(251, 131);
            this.removeFromSelectionButton.Name = "removeFromSelectionButton";
            this.removeFromSelectionButton.Size = new System.Drawing.Size(45, 23);
            this.removeFromSelectionButton.TabIndex = 2;
            this.removeFromSelectionButton.UseVisualStyleBackColor = true;
            this.removeFromSelectionButton.Click += new System.EventHandler(this.removeFromSelectionButton_Click);
            // 
            // addToSelectionButton
            // 
            this.addToSelectionButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addToSelectionButton.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_right;
            this.addToSelectionButton.Location = new System.Drawing.Point(251, 102);
            this.addToSelectionButton.Name = "addToSelectionButton";
            this.addToSelectionButton.Size = new System.Drawing.Size(45, 23);
            this.addToSelectionButton.TabIndex = 1;
            this.addToSelectionButton.UseVisualStyleBackColor = true;
            this.addToSelectionButton.Click += new System.EventHandler(this.addToSelectionButton_Click);
            // 
            // availableListBox
            // 
            this.availableListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.availableListBox.FormattingEnabled = true;
            this.availableListBox.Location = new System.Drawing.Point(6, 35);
            this.availableListBox.Name = "availableListBox";
            this.availableListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.availableListBox.Size = new System.Drawing.Size(211, 199);
            this.availableListBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(154, 478);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(342, 478);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // MultiSelectionPopUp
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(575, 514);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiSelectionPopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.RadioButton containRadioButton;
        private System.Windows.Forms.RadioButton matchRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button removeFromSelectionButton;
        private System.Windows.Forms.Button addToSelectionButton;
        private System.Windows.Forms.ListBox availableListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button removeAllItemsButton;
        private System.Windows.Forms.Button addAllItemsButton;
        private System.Windows.Forms.Button clearSelectionButton;
        private System.Windows.Forms.ListBox selectedListBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton selectedItemsRadioButton;
        private System.Windows.Forms.RadioButton availableItemsRadioButton;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}