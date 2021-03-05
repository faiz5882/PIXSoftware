namespace SIMCORE_TOOL.Assistant
{
    partial class FP_ItemSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FP_ItemSelection));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchByCodeRadioBtn = new System.Windows.Forms.RadioButton();
            this.searchByDescrRadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.allItemsListBox = new System.Windows.Forms.ListBox();
            this.addItemsToSelectionBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.selectedItemsListBox = new System.Windows.Forms.ListBox();
            this.clearSelectionListBtn = new System.Windows.Forms.Button();
            this.removeItemFromSelectionBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.searchBtn);
            this.groupBox1.Controls.Add(this.searchTextBox);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search parameters";
            // 
            // searchBtn
            // 
            this.searchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBtn.Location = new System.Drawing.Point(273, 86);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 4;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(6, 88);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(248, 20);
            this.searchTextBox.TabIndex = 3;
            this.searchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchTextBox_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.searchByCodeRadioBtn);
            this.groupBox2.Controls.Add(this.searchByDescrRadioBtn);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 52);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search type";
            // 
            // searchByCodeRadioBtn
            // 
            this.searchByCodeRadioBtn.AutoSize = true;
            this.searchByCodeRadioBtn.Location = new System.Drawing.Point(15, 23);
            this.searchByCodeRadioBtn.Name = "searchByCodeRadioBtn";
            this.searchByCodeRadioBtn.Size = new System.Drawing.Size(122, 17);
            this.searchByCodeRadioBtn.TabIndex = 0;
            this.searchByCodeRadioBtn.TabStop = true;
            this.searchByCodeRadioBtn.Text = "Search by item code";
            this.searchByCodeRadioBtn.UseVisualStyleBackColor = true;
            // 
            // searchByDescrRadioBtn
            // 
            this.searchByDescrRadioBtn.AutoSize = true;
            this.searchByDescrRadioBtn.Location = new System.Drawing.Point(170, 23);
            this.searchByDescrRadioBtn.Name = "searchByDescrRadioBtn";
            this.searchByDescrRadioBtn.Size = new System.Drawing.Size(149, 17);
            this.searchByDescrRadioBtn.TabIndex = 1;
            this.searchByDescrRadioBtn.TabStop = true;
            this.searchByDescrRadioBtn.Text = "Search by item description";
            this.searchByDescrRadioBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.allItemsListBox);
            this.groupBox3.Controls.Add(this.addItemsToSelectionBtn);
            this.groupBox3.Location = new System.Drawing.Point(12, 133);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 206);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "All Items";
            // 
            // allItemsListBox
            // 
            this.allItemsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.allItemsListBox.FormattingEnabled = true;
            this.allItemsListBox.Location = new System.Drawing.Point(6, 19);
            this.allItemsListBox.Name = "allItemsListBox";
            this.allItemsListBox.Size = new System.Drawing.Size(352, 147);
            this.allItemsListBox.TabIndex = 2;
            this.allItemsListBox.DoubleClick += new System.EventHandler(this.addItemToSelectionBtn_Click);
            // 
            // addItemsToSelectionBtn
            // 
            this.addItemsToSelectionBtn.Location = new System.Drawing.Point(81, 172);
            this.addItemsToSelectionBtn.Name = "addItemsToSelectionBtn";
            this.addItemsToSelectionBtn.Size = new System.Drawing.Size(186, 23);
            this.addItemsToSelectionBtn.TabIndex = 1;
            this.addItemsToSelectionBtn.Text = "Add single item to selection list";
            this.addItemsToSelectionBtn.UseVisualStyleBackColor = true;
            this.addItemsToSelectionBtn.Click += new System.EventHandler(this.addItemToSelectionBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.selectedItemsListBox);
            this.groupBox4.Controls.Add(this.clearSelectionListBtn);
            this.groupBox4.Controls.Add(this.removeItemFromSelectionBtn);
            this.groupBox4.Location = new System.Drawing.Point(12, 344);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(364, 202);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selected items";
            // 
            // selectedItemsListBox
            // 
            this.selectedItemsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedItemsListBox.FormattingEnabled = true;
            this.selectedItemsListBox.Location = new System.Drawing.Point(6, 19);
            this.selectedItemsListBox.Name = "selectedItemsListBox";
            this.selectedItemsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectedItemsListBox.Size = new System.Drawing.Size(352, 147);
            this.selectedItemsListBox.TabIndex = 3;
            this.selectedItemsListBox.DoubleClick += new System.EventHandler(this.okBtn_Click);
            // 
            // clearSelectionListBtn
            // 
            this.clearSelectionListBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSelectionListBtn.Location = new System.Drawing.Point(220, 172);
            this.clearSelectionListBtn.Name = "clearSelectionListBtn";
            this.clearSelectionListBtn.Size = new System.Drawing.Size(92, 23);
            this.clearSelectionListBtn.TabIndex = 2;
            this.clearSelectionListBtn.Text = "Clear list";
            this.clearSelectionListBtn.UseVisualStyleBackColor = true;
            this.clearSelectionListBtn.Click += new System.EventHandler(this.clearSelectionListBtn_Click);
            // 
            // removeItemFromSelectionBtn
            // 
            this.removeItemFromSelectionBtn.Location = new System.Drawing.Point(42, 172);
            this.removeItemFromSelectionBtn.Name = "removeItemFromSelectionBtn";
            this.removeItemFromSelectionBtn.Size = new System.Drawing.Size(92, 23);
            this.removeItemFromSelectionBtn.TabIndex = 1;
            this.removeItemFromSelectionBtn.Text = "Remove item(s)";
            this.removeItemFromSelectionBtn.UseVisualStyleBackColor = true;
            this.removeItemFromSelectionBtn.Click += new System.EventHandler(this.removeItemFromSelectionBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(80, 561);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "&OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(218, 561);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "&Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // FP_ItemSelection
            // 
            this.AcceptButton = this.searchBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(388, 596);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FP_ItemSelection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Search";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton searchByCodeRadioBtn;
        private System.Windows.Forms.RadioButton searchByDescrRadioBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button addItemsToSelectionBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button clearSelectionListBtn;
        private System.Windows.Forms.Button removeItemFromSelectionBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.ListBox allItemsListBox;
        private System.Windows.Forms.ListBox selectedItemsListBox;
    }
}