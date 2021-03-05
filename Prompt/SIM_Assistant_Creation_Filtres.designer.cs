namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Assistant_Creation_Filtres 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Assistant_Creation_Filtres));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txt_FilterName = new System.Windows.Forms.TextBox();
            this.lbl_FilterName = new System.Windows.Forms.Label();
            this.cb_CopyDatas = new System.Windows.Forms.CheckBox();
            this.gb_Column = new System.Windows.Forms.GroupBox();
            this.txt_Condition = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_OperationType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_TableColumns = new System.Windows.Forms.Label();
            this.cb_Functions = new System.Windows.Forms.ComboBox();
            this.cb_TableColumns = new System.Windows.Forms.ComboBox();
            this.cb_Display = new System.Windows.Forms.CheckBox();
            this.lbl_expression = new System.Windows.Forms.Label();
            this.txt_formule = new System.Windows.Forms.TextBox();
            this.lbl_columnName = new System.Windows.Forms.Label();
            this.txt_ColumnName = new System.Windows.Forms.TextBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_RemoveAll = new System.Windows.Forms.Button();
            this.clb_ColumnsToShow = new System.Windows.Forms.ListBox();
            this.rtb_Error = new System.Windows.Forms.RichTextBox();
            this.lbl_Error = new System.Windows.Forms.Label();
            this.gb_Column.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(50, 537);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(94, 29);
            this.btn_Ok.TabIndex = 7;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(291, 538);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(90, 28);
            this.btn_Cancel.TabIndex = 8;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // txt_FilterName
            // 
            this.txt_FilterName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_FilterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FilterName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_FilterName.Location = new System.Drawing.Point(132, 12);
            this.txt_FilterName.Name = "txt_FilterName";
            this.txt_FilterName.Size = new System.Drawing.Size(279, 20);
            this.txt_FilterName.TabIndex = 0;
            this.txt_FilterName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterName_KeyPress);
            // 
            // lbl_FilterName
            // 
            this.lbl_FilterName.AutoSize = true;
            this.lbl_FilterName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FilterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FilterName.Location = new System.Drawing.Point(47, 15);
            this.lbl_FilterName.Name = "lbl_FilterName";
            this.lbl_FilterName.Size = new System.Drawing.Size(79, 13);
            this.lbl_FilterName.TabIndex = 9;
            this.lbl_FilterName.Text = "Filter Name :";
            // 
            // cb_CopyDatas
            // 
            this.cb_CopyDatas.AutoSize = true;
            this.cb_CopyDatas.BackColor = System.Drawing.Color.Transparent;
            this.cb_CopyDatas.Location = new System.Drawing.Point(100, 61);
            this.cb_CopyDatas.Name = "cb_CopyDatas";
            this.cb_CopyDatas.Size = new System.Drawing.Size(215, 17);
            this.cb_CopyDatas.TabIndex = 1;
            this.cb_CopyDatas.Text = "Copy all the filter\'s results in a new table.";
            this.cb_CopyDatas.UseVisualStyleBackColor = false;
            // 
            // gb_Column
            // 
            this.gb_Column.BackColor = System.Drawing.Color.Transparent;
            this.gb_Column.Controls.Add(this.txt_Condition);
            this.gb_Column.Controls.Add(this.label3);
            this.gb_Column.Controls.Add(this.label1);
            this.gb_Column.Controls.Add(this.cb_OperationType);
            this.gb_Column.Controls.Add(this.label2);
            this.gb_Column.Controls.Add(this.lbl_TableColumns);
            this.gb_Column.Controls.Add(this.cb_Functions);
            this.gb_Column.Controls.Add(this.cb_TableColumns);
            this.gb_Column.Controls.Add(this.cb_Display);
            this.gb_Column.Controls.Add(this.lbl_expression);
            this.gb_Column.Controls.Add(this.txt_formule);
            this.gb_Column.Controls.Add(this.lbl_columnName);
            this.gb_Column.Controls.Add(this.txt_ColumnName);
            this.gb_Column.Enabled = false;
            this.gb_Column.Location = new System.Drawing.Point(12, 217);
            this.gb_Column.Name = "gb_Column";
            this.gb_Column.Size = new System.Drawing.Size(414, 314);
            this.gb_Column.TabIndex = 6;
            this.gb_Column.TabStop = false;
            this.gb_Column.Text = "Column";
            // 
            // txt_Condition
            // 
            this.txt_Condition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Condition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Condition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_Condition.Location = new System.Drawing.Point(18, 248);
            this.txt_Condition.Multiline = true;
            this.txt_Condition.Name = "txt_Condition";
            this.txt_Condition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Condition.Size = new System.Drawing.Size(381, 58);
            this.txt_Condition.TabIndex = 19;
            this.txt_Condition.Enter += new System.EventHandler(this.txt_formule_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Condition";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Operation type";
            this.label1.Visible = false;
            // 
            // cb_OperationType
            // 
            this.cb_OperationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OperationType.Enabled = false;
            this.cb_OperationType.FormattingEnabled = true;
            this.cb_OperationType.Location = new System.Drawing.Point(18, 95);
            this.cb_OperationType.Name = "cb_OperationType";
            this.cb_OperationType.Size = new System.Drawing.Size(130, 21);
            this.cb_OperationType.TabIndex = 16;
            this.cb_OperationType.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Functions";
            // 
            // lbl_TableColumns
            // 
            this.lbl_TableColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_TableColumns.AutoSize = true;
            this.lbl_TableColumns.Location = new System.Drawing.Point(175, 99);
            this.lbl_TableColumns.Name = "lbl_TableColumns";
            this.lbl_TableColumns.Size = new System.Drawing.Size(47, 13);
            this.lbl_TableColumns.TabIndex = 14;
            this.lbl_TableColumns.Text = "Columns";
            // 
            // cb_Functions
            // 
            this.cb_Functions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Functions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Functions.FormattingEnabled = true;
            this.cb_Functions.Location = new System.Drawing.Point(178, 71);
            this.cb_Functions.Name = "cb_Functions";
            this.cb_Functions.Size = new System.Drawing.Size(221, 21);
            this.cb_Functions.TabIndex = 4;
            this.cb_Functions.SelectedIndexChanged += new System.EventHandler(this.cb_Functions_SelectedIndexChanged);
            // 
            // cb_TableColumns
            // 
            this.cb_TableColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_TableColumns.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cb_TableColumns.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_TableColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TableColumns.FormattingEnabled = true;
            this.cb_TableColumns.Location = new System.Drawing.Point(178, 115);
            this.cb_TableColumns.Name = "cb_TableColumns";
            this.cb_TableColumns.Size = new System.Drawing.Size(221, 21);
            this.cb_TableColumns.TabIndex = 3;
            this.cb_TableColumns.SelectedIndexChanged += new System.EventHandler(this.cb_TableColumns_SelectedIndexChanged);
            // 
            // cb_Display
            // 
            this.cb_Display.AutoSize = true;
            this.cb_Display.BackColor = System.Drawing.Color.Transparent;
            this.cb_Display.Location = new System.Drawing.Point(18, 49);
            this.cb_Display.Name = "cb_Display";
            this.cb_Display.Size = new System.Drawing.Size(97, 17);
            this.cb_Display.TabIndex = 1;
            this.cb_Display.Text = "Display column";
            this.cb_Display.UseVisualStyleBackColor = false;
            // 
            // lbl_expression
            // 
            this.lbl_expression.AutoSize = true;
            this.lbl_expression.Location = new System.Drawing.Point(15, 126);
            this.lbl_expression.Name = "lbl_expression";
            this.lbl_expression.Size = new System.Drawing.Size(58, 13);
            this.lbl_expression.TabIndex = 7;
            this.lbl_expression.Text = "Expression";
            // 
            // txt_formule
            // 
            this.txt_formule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_formule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_formule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_formule.Location = new System.Drawing.Point(18, 142);
            this.txt_formule.Multiline = true;
            this.txt_formule.Name = "txt_formule";
            this.txt_formule.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_formule.Size = new System.Drawing.Size(381, 83);
            this.txt_formule.TabIndex = 2;
            this.txt_formule.Click += new System.EventHandler(this.txt_formule_Click);
            this.txt_formule.Enter += new System.EventHandler(this.txt_formule_Enter);
            // 
            // lbl_columnName
            // 
            this.lbl_columnName.AutoSize = true;
            this.lbl_columnName.Location = new System.Drawing.Point(15, 22);
            this.lbl_columnName.Name = "lbl_columnName";
            this.lbl_columnName.Size = new System.Drawing.Size(73, 13);
            this.lbl_columnName.TabIndex = 1;
            this.lbl_columnName.Text = "Column Name";
            // 
            // txt_ColumnName
            // 
            this.txt_ColumnName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_ColumnName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_ColumnName.Location = new System.Drawing.Point(94, 19);
            this.txt_ColumnName.Name = "txt_ColumnName";
            this.txt_ColumnName.Size = new System.Drawing.Size(163, 20);
            this.txt_ColumnName.TabIndex = 0;
            this.txt_ColumnName.Leave += new System.EventHandler(this.txt_ColumnName_Leave);
            //this.txt_ColumnName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterName_KeyPress);
            // 
            // btn_Add
            // 
            this.btn_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(325, 80);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(95, 26);
            this.btn_Add.TabIndex = 3;
            this.btn_Add.Text = "Add column";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.BackColor = System.Drawing.Color.Transparent;
            this.btn_Remove.Enabled = false;
            this.btn_Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Remove.Location = new System.Drawing.Point(325, 135);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(95, 26);
            this.btn_Remove.TabIndex = 4;
            this.btn_Remove.Text = "Remove column";
            this.btn_Remove.UseVisualStyleBackColor = false;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_RemoveAll
            // 
            this.btn_RemoveAll.BackColor = System.Drawing.Color.Transparent;
            this.btn_RemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveAll.Location = new System.Drawing.Point(325, 163);
            this.btn_RemoveAll.Name = "btn_RemoveAll";
            this.btn_RemoveAll.Size = new System.Drawing.Size(95, 26);
            this.btn_RemoveAll.TabIndex = 5;
            this.btn_RemoveAll.Text = "Remove all";
            this.btn_RemoveAll.UseVisualStyleBackColor = false;
            this.btn_RemoveAll.Click += new System.EventHandler(this.btn_RemoveAll_Click);
            // 
            // clb_ColumnsToShow
            // 
            this.clb_ColumnsToShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.clb_ColumnsToShow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.clb_ColumnsToShow.FormattingEnabled = true;
            this.clb_ColumnsToShow.Location = new System.Drawing.Point(12, 86);
            this.clb_ColumnsToShow.Name = "clb_ColumnsToShow";
            this.clb_ColumnsToShow.Size = new System.Drawing.Size(296, 121);
            this.clb_ColumnsToShow.TabIndex = 10;
            this.clb_ColumnsToShow.SelectedIndexChanged += new System.EventHandler(this.Listes_SelectedIndexChanged);
            this.clb_ColumnsToShow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LST_ListeSeries_MouseDown);
            this.clb_ColumnsToShow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LST_ListeSeries_KeyDown);
            // 
            // rtb_Error
            // 
            this.rtb_Error.Location = new System.Drawing.Point(445, 296);
            this.rtb_Error.Name = "rtb_Error";
            this.rtb_Error.ReadOnly = true;
            this.rtb_Error.Size = new System.Drawing.Size(296, 208);
            this.rtb_Error.TabIndex = 11;
            this.rtb_Error.Text = "";
            this.rtb_Error.TextChanged += new System.EventHandler(this.rtb_Error_TextChanged);
            // 
            // lbl_Error
            // 
            this.lbl_Error.AutoSize = true;
            this.lbl_Error.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Error.Location = new System.Drawing.Point(457, 262);
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.Size = new System.Drawing.Size(96, 13);
            this.lbl_Error.TabIndex = 12;
            this.lbl_Error.Text = "Errors / Warnings :";
            // 
            // SIM_Assistant_Creation_Filtres
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(443, 582);
            this.Controls.Add(this.lbl_Error);
            this.Controls.Add(this.rtb_Error);
            this.Controls.Add(this.clb_ColumnsToShow);
            this.Controls.Add(this.btn_RemoveAll);
            this.Controls.Add(this.btn_Remove);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.cb_CopyDatas);
            this.Controls.Add(this.gb_Column);
            this.Controls.Add(this.lbl_FilterName);
            this.Controls.Add(this.txt_FilterName);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SIM_Assistant_Creation_Filtres";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create/Edit filter";
            this.gb_Column.ResumeLayout(false);
            this.gb_Column.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txt_FilterName;
        private System.Windows.Forms.Label lbl_FilterName;
        private System.Windows.Forms.CheckBox cb_CopyDatas;
        private System.Windows.Forms.GroupBox gb_Column;
        private System.Windows.Forms.Label lbl_columnName;
        private System.Windows.Forms.TextBox txt_ColumnName;
        private System.Windows.Forms.TextBox txt_formule;
        private System.Windows.Forms.Label lbl_expression;
        private System.Windows.Forms.CheckBox cb_Display;
        private System.Windows.Forms.ComboBox cb_Functions;
        private System.Windows.Forms.ComboBox cb_TableColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_TableColumns;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_RemoveAll;
        private System.Windows.Forms.ComboBox cb_OperationType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Condition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox clb_ColumnsToShow;
        private System.Windows.Forms.RichTextBox rtb_Error;
        private System.Windows.Forms.Label lbl_Error;

    }
}