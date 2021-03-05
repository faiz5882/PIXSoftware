namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Htmleditor_AddTable
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Htmleditor_AddTable));
            this.TableSizeBox = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.RowsNumberLabel = new System.Windows.Forms.Label();
            this.ColumnNbLabel = new System.Windows.Forms.Label();
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.TableColorLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TitleCheckbox = new System.Windows.Forms.CheckBox();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.BorderCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BorderPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TableSizeBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BorderPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TableSizeBox
            // 
            this.TableSizeBox.BackColor = System.Drawing.Color.Transparent;
            this.TableSizeBox.Controls.Add(this.numericUpDown2);
            this.TableSizeBox.Controls.Add(this.numericUpDown1);
            this.TableSizeBox.Controls.Add(this.RowsNumberLabel);
            this.TableSizeBox.Controls.Add(this.ColumnNbLabel);
            this.TableSizeBox.Location = new System.Drawing.Point(12, 29);
            this.TableSizeBox.Name = "TableSizeBox";
            this.TableSizeBox.Size = new System.Drawing.Size(307, 100);
            this.TableSizeBox.TabIndex = 0;
            this.TableSizeBox.TabStop = false;
            this.TableSizeBox.Text = "Table Size";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(181, 64);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(181, 25);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RowsNumberLabel
            // 
            this.RowsNumberLabel.AutoSize = true;
            this.RowsNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.RowsNumberLabel.Location = new System.Drawing.Point(22, 64);
            this.RowsNumberLabel.Name = "RowsNumberLabel";
            this.RowsNumberLabel.Size = new System.Drawing.Size(78, 13);
            this.RowsNumberLabel.TabIndex = 1;
            this.RowsNumberLabel.Text = "Rows number :";
            // 
            // ColumnNbLabel
            // 
            this.ColumnNbLabel.AutoSize = true;
            this.ColumnNbLabel.BackColor = System.Drawing.Color.Transparent;
            this.ColumnNbLabel.Location = new System.Drawing.Point(22, 27);
            this.ColumnNbLabel.Name = "ColumnNbLabel";
            this.ColumnNbLabel.Size = new System.Drawing.Size(91, 13);
            this.ColumnNbLabel.TabIndex = 0;
            this.ColumnNbLabel.Text = "Columns number :";
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(13, 281);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 2;
            this.OkBtn.Text = "Ok";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CancelBtn.Location = new System.Drawing.Point(238, 281);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // TableColorLabel
            // 
            this.TableColorLabel.AutoSize = true;
            this.TableColorLabel.BackColor = System.Drawing.Color.Transparent;
            this.TableColorLabel.Location = new System.Drawing.Point(21, 90);
            this.TableColorLabel.Name = "TableColorLabel";
            this.TableColorLabel.Size = new System.Drawing.Size(67, 13);
            this.TableColorLabel.TabIndex = 4;
            this.TableColorLabel.Text = "Table Color :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Beige;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(180, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 13);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // TitleCheckbox
            // 
            this.TitleCheckbox.AutoSize = true;
            this.TitleCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.TitleCheckbox.Location = new System.Drawing.Point(24, 25);
            this.TitleCheckbox.Name = "TitleCheckbox";
            this.TitleCheckbox.Size = new System.Drawing.Size(46, 17);
            this.TitleCheckbox.TabIndex = 6;
            this.TitleCheckbox.Text = "Title";
            this.TitleCheckbox.UseVisualStyleBackColor = false;
            this.TitleCheckbox.CheckedChanged += new System.EventHandler(this.TitleCheckbox_CheckedChanged);
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(180, 22);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(120, 20);
            this.TitleTextBox.TabIndex = 7;
            // 
            // BorderCheckBox
            // 
            this.BorderCheckBox.AutoSize = true;
            this.BorderCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.BorderCheckBox.Location = new System.Drawing.Point(42, 59);
            this.BorderCheckBox.Name = "BorderCheckBox";
            this.BorderCheckBox.Size = new System.Drawing.Size(57, 17);
            this.BorderCheckBox.TabIndex = 8;
            this.BorderCheckBox.Text = "Border";
            this.BorderCheckBox.UseVisualStyleBackColor = false;
            this.BorderCheckBox.CheckedChanged += new System.EventHandler(this.BorderCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.BorderPictureBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TitleCheckbox);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.BorderCheckBox);
            this.groupBox1.Controls.Add(this.TableColorLabel);
            this.groupBox1.Controls.Add(this.TitleTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 122);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Table Informations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Text :";
            // 
            // BorderPictureBox
            // 
            this.BorderPictureBox.BackColor = System.Drawing.Color.Beige;
            this.BorderPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BorderPictureBox.Location = new System.Drawing.Point(180, 59);
            this.BorderPictureBox.Name = "BorderPictureBox";
            this.BorderPictureBox.Size = new System.Drawing.Size(51, 13);
            this.BorderPictureBox.TabIndex = 10;
            this.BorderPictureBox.TabStop = false;
            this.BorderPictureBox.Click += new System.EventHandler(this.BorderPictureBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Color :";
            // 
            // SIM_Htmleditor_AddTable
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(331, 316);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.TableSizeBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(337, 246);
            this.Name = "SIM_Htmleditor_AddTable";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Insert Table";
            this.TableSizeBox.ResumeLayout(false);
            this.TableSizeBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BorderPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox TableSizeBox;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label RowsNumberLabel;
        private System.Windows.Forms.Label ColumnNbLabel;
        private System.Windows.Forms.Label TableColorLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox TitleCheckbox;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.CheckBox BorderCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox BorderPictureBox;
        private System.Windows.Forms.Label label1;
    }
}