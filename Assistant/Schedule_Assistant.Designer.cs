namespace SIMCORE_TOOL.Assistant
{
    partial class Schedule_Assistant 
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Schedule_Assistant));
            this.lst_Groups = new System.Windows.Forms.ListBox();
            this.gb_NewElement = new System.Windows.Forms.GroupBox();
            this.dtp_End = new System.Windows.Forms.DateTimePicker();
            this.lbl_Table = new System.Windows.Forms.Label();
            this.dtp_Start = new System.Windows.Forms.DateTimePicker();
            this.cb_TableName = new System.Windows.Forms.ComboBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_End = new System.Windows.Forms.Label();
            this.lbl_Begin = new System.Windows.Forms.Label();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_DeleteAll = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gb_NewElement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lst_Groups
            // 
            this.lst_Groups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_Groups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.lst_Groups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lst_Groups.FormattingEnabled = true;
            this.lst_Groups.Location = new System.Drawing.Point(31, 27);
            this.lst_Groups.Name = "lst_Groups";
            this.lst_Groups.Size = new System.Drawing.Size(231, 95);
            this.lst_Groups.TabIndex = 0;
            this.lst_Groups.SelectedIndexChanged += new System.EventHandler(this.lst_Groups_SelectedIndexChanged);
            // 
            // gb_NewElement
            // 
            this.gb_NewElement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_NewElement.BackColor = System.Drawing.Color.Transparent;
            this.gb_NewElement.Controls.Add(this.dtp_End);
            this.gb_NewElement.Controls.Add(this.lbl_Table);
            this.gb_NewElement.Controls.Add(this.dtp_Start);
            this.gb_NewElement.Controls.Add(this.cb_TableName);
            this.gb_NewElement.Controls.Add(this.btn_Add);
            this.gb_NewElement.Controls.Add(this.lbl_End);
            this.gb_NewElement.Controls.Add(this.lbl_Begin);
            this.gb_NewElement.Location = new System.Drawing.Point(12, 280);
            this.gb_NewElement.Name = "gb_NewElement";
            this.gb_NewElement.Size = new System.Drawing.Size(619, 68);
            this.gb_NewElement.TabIndex = 5;
            this.gb_NewElement.TabStop = false;
            this.gb_NewElement.Text = "New Element";
            // 
            // dtp_End
            // 
            this.dtp_End.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtp_End.CustomFormat = "M/d/yyyy h:mm tt";
            this.dtp_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_End.Location = new System.Drawing.Point(177, 34);
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.Size = new System.Drawing.Size(135, 20);
            this.dtp_End.TabIndex = 2;
            this.dtp_End.Leave += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // lbl_Table
            // 
            this.lbl_Table.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Table.AutoSize = true;
            this.lbl_Table.Location = new System.Drawing.Point(327, 16);
            this.lbl_Table.Name = "lbl_Table";
            this.lbl_Table.Size = new System.Drawing.Size(63, 13);
            this.lbl_Table.TabIndex = 14;
            this.lbl_Table.Text = "Table name";
            // 
            // dtp_Start
            // 
            this.dtp_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtp_Start.CustomFormat = "M/d/yyyy h:mm tt";
            this.dtp_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Start.Location = new System.Drawing.Point(6, 34);
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.Size = new System.Drawing.Size(135, 20);
            this.dtp_Start.TabIndex = 1;
            this.dtp_Start.Leave += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // cb_TableName
            // 
            this.cb_TableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_TableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TableName.FormattingEnabled = true;
            this.cb_TableName.Location = new System.Drawing.Point(318, 32);
            this.cb_TableName.Name = "cb_TableName";
            this.cb_TableName.Size = new System.Drawing.Size(230, 21);
            this.cb_TableName.TabIndex = 13;
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.Location = new System.Drawing.Point(554, 33);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(61, 21);
            this.btn_Add.TabIndex = 9;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_End
            // 
            this.lbl_End.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_End.AutoSize = true;
            this.lbl_End.BackColor = System.Drawing.Color.Transparent;
            this.lbl_End.Location = new System.Drawing.Point(174, 16);
            this.lbl_End.Name = "lbl_End";
            this.lbl_End.Size = new System.Drawing.Size(26, 13);
            this.lbl_End.TabIndex = 12;
            this.lbl_End.Text = "End";
            // 
            // lbl_Begin
            // 
            this.lbl_Begin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Begin.AutoSize = true;
            this.lbl_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Begin.Location = new System.Drawing.Point(16, 16);
            this.lbl_Begin.Name = "lbl_Begin";
            this.lbl_Begin.Size = new System.Drawing.Size(34, 13);
            this.lbl_Begin.TabIndex = 11;
            this.lbl_Begin.Text = "Begin";
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(48, 370);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 35);
            this.btn_Ok.TabIndex = 8;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(435, 370);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(77, 35);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Location = new System.Drawing.Point(566, 183);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(61, 25);
            this.btn_Delete.TabIndex = 6;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_DeleteAll
            // 
            this.btn_DeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DeleteAll.Location = new System.Drawing.Point(566, 214);
            this.btn_DeleteAll.Name = "btn_DeleteAll";
            this.btn_DeleteAll.Size = new System.Drawing.Size(61, 25);
            this.btn_DeleteAll.TabIndex = 7;
            this.btn_DeleteAll.Text = "Delete all";
            this.btn_DeleteAll.UseVisualStyleBackColor = true;
            this.btn_DeleteAll.Click += new System.EventHandler(this.btn_DeleteAll_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(31, 128);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(529, 146);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Schedule_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(636, 421);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_DeleteAll);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.gb_NewElement);
            this.Controls.Add(this.lst_Groups);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Schedule_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Allocation_Assistant";
            this.gb_NewElement.ResumeLayout(false);
            this.gb_NewElement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lst_Groups;
        private System.Windows.Forms.GroupBox gb_NewElement;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Label lbl_Begin;
        private System.Windows.Forms.Label lbl_End;
        private System.Windows.Forms.Button btn_DeleteAll;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dtp_Start;
        private System.Windows.Forms.DateTimePicker dtp_End;
        private System.Windows.Forms.Label lbl_Table;
        private System.Windows.Forms.ComboBox cb_TableName;
    }
}