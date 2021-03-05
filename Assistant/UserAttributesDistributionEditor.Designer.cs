namespace SIMCORE_TOOL.Assistant
{
    partial class UserAttributesDistributionEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserAttributesDistributionEditor));
            this.label1 = new System.Windows.Forms.Label();
            this.cbFlightCategories = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pContent = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.newValue = new System.Windows.Forms.TextBox();
            this.btnAddNewValue = new System.Windows.Forms.Button();
            this.lbl_Sum = new System.Windows.Forms.Label();
            this.lbl_Mean = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Flight Category";
            // 
            // cbFlightCategories
            // 
            this.cbFlightCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFlightCategories.FormattingEnabled = true;
            this.cbFlightCategories.Location = new System.Drawing.Point(100, 17);
            this.cbFlightCategories.Name = "cbFlightCategories";
            this.cbFlightCategories.Size = new System.Drawing.Size(105, 21);
            this.cbFlightCategories.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(57, 392);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(76, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(224, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(54, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(221, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Distribution";
            // 
            // pContent
            // 
            this.pContent.BackColor = System.Drawing.Color.Transparent;
            this.pContent.Location = new System.Drawing.Point(12, 142);
            this.pContent.Name = "pContent";
            this.pContent.Size = new System.Drawing.Size(317, 185);
            this.pContent.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "New Value";
            // 
            // newValue
            // 
            this.newValue.Location = new System.Drawing.Point(100, 69);
            this.newValue.Name = "newValue";
            this.newValue.Size = new System.Drawing.Size(105, 20);
            this.newValue.TabIndex = 8;
            // 
            // btnAddNewValue
            // 
            this.btnAddNewValue.Location = new System.Drawing.Point(225, 68);
            this.btnAddNewValue.Name = "btnAddNewValue";
            this.btnAddNewValue.Size = new System.Drawing.Size(93, 21);
            this.btnAddNewValue.TabIndex = 9;
            this.btnAddNewValue.Text = "Add New Value";
            this.btnAddNewValue.UseVisualStyleBackColor = true;
            this.btnAddNewValue.Click += new System.EventHandler(this.btnAddNewValue_Click);
            // 
            // lbl_Sum
            // 
            this.lbl_Sum.AutoSize = true;
            this.lbl_Sum.Location = new System.Drawing.Point(221, 330);
            this.lbl_Sum.Name = "lbl_Sum";
            this.lbl_Sum.Size = new System.Drawing.Size(31, 13);
            this.lbl_Sum.TabIndex = 10;
            this.lbl_Sum.Text = "Sum:";
            // 
            // lbl_Mean
            // 
            this.lbl_Mean.AutoSize = true;
            this.lbl_Mean.Location = new System.Drawing.Point(221, 352);
            this.lbl_Mean.Name = "lbl_Mean";
            this.lbl_Mean.Size = new System.Drawing.Size(79, 13);
            this.lbl_Mean.TabIndex = 11;
            this.lbl_Mean.Text = "Average Index:";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(225, 17);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(92, 21);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Clear Values";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // UserAttributesDistributionEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(341, 429);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lbl_Mean);
            this.Controls.Add(this.lbl_Sum);
            this.Controls.Add(this.btnAddNewValue);
            this.Controls.Add(this.newValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pContent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbFlightCategories);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserAttributesDistributionEditor";
            this.Text = "UserAttributesDistributionEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFlightCategories;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pContent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox newValue;
        private System.Windows.Forms.Button btnAddNewValue;
        private System.Windows.Forms.Label lbl_Sum;
        private System.Windows.Forms.Label lbl_Mean;
        private System.Windows.Forms.Button btnReset;
    }
}