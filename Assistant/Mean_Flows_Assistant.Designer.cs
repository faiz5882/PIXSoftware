namespace SIMCORE_TOOL.Assistant
{
    partial class Mean_Flows_Assistant
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mean_Flows_Assistant));
            this.lst_Groups = new System.Windows.Forms.ListBox();
            this.gb_NewElement = new System.Windows.Forms.GroupBox();
            this.txt_Flow_S = new System.Windows.Forms.TextBox();
            this.cb_End = new System.Windows.Forms.ComboBox();
            this.lbl_Alloc = new System.Windows.Forms.Label();
            this.cb_Begin = new System.Windows.Forms.ComboBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_End = new System.Windows.Forms.Label();
            this.lbl_Begin = new System.Windows.Forms.Label();
            this.txt_Number = new System.Windows.Forms.TextBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_DeleteAll = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_Step = new System.Windows.Forms.TextBox();
            this.lbl_StartTIme = new System.Windows.Forms.Label();
            this.lbl_EndTime = new System.Windows.Forms.Label();
            this.lbl_Step = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dtp_End = new System.Windows.Forms.DateTimePicker();
            this.dtp_Start = new System.Windows.Forms.DateTimePicker();
            this.gb_NewElement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lst_Groups
            // 
            this.lst_Groups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.lst_Groups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lst_Groups.FormattingEnabled = true;
            this.lst_Groups.Location = new System.Drawing.Point(31, 27);
            this.lst_Groups.Name = "lst_Groups";
            this.lst_Groups.Size = new System.Drawing.Size(190, 95);
            this.lst_Groups.TabIndex = 0;
            this.lst_Groups.SelectedIndexChanged += new System.EventHandler(this.lst_Groups_SelectedIndexChanged);
            // 
            // gb_NewElement
            // 
            this.gb_NewElement.BackColor = System.Drawing.Color.Transparent;
            this.gb_NewElement.Controls.Add(this.txt_Flow_S);
            this.gb_NewElement.Controls.Add(this.cb_End);
            this.gb_NewElement.Controls.Add(this.lbl_Alloc);
            this.gb_NewElement.Controls.Add(this.cb_Begin);
            this.gb_NewElement.Controls.Add(this.btn_Add);
            this.gb_NewElement.Controls.Add(this.lbl_End);
            this.gb_NewElement.Controls.Add(this.lbl_Begin);
            this.gb_NewElement.Controls.Add(this.txt_Number);
            this.gb_NewElement.Location = new System.Drawing.Point(12, 280);
            this.gb_NewElement.Name = "gb_NewElement";
            this.gb_NewElement.Size = new System.Drawing.Size(568, 68);
            this.gb_NewElement.TabIndex = 5;
            this.gb_NewElement.TabStop = false;
            this.gb_NewElement.Text = "New Element";
            // 
            // txt_Flow_S
            // 
            this.txt_Flow_S.Enabled = false;
            this.txt_Flow_S.Location = new System.Drawing.Point(411, 35);
            this.txt_Flow_S.Name = "txt_Flow_S";
            this.txt_Flow_S.Size = new System.Drawing.Size(59, 20);
            this.txt_Flow_S.TabIndex = 15;
            // 
            // cb_End
            // 
            this.cb_End.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_End.FormattingEnabled = true;
            this.cb_End.Location = new System.Drawing.Point(170, 33);
            this.cb_End.Name = "cb_End";
            this.cb_End.Size = new System.Drawing.Size(135, 21);
            this.cb_End.TabIndex = 6;
            // 
            // lbl_Alloc
            // 
            this.lbl_Alloc.AutoSize = true;
            this.lbl_Alloc.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Alloc.Location = new System.Drawing.Point(342, 19);
            this.lbl_Alloc.Name = "lbl_Alloc";
            this.lbl_Alloc.Size = new System.Drawing.Size(123, 13);
            this.lbl_Alloc.TabIndex = 14;
            this.lbl_Alloc.Text = "Flow (bags/h)    (bags/s)";
            this.toolTip1.SetToolTip(this.lbl_Alloc, "Hourly Flow per Station");
            // 
            // cb_Begin
            // 
            this.cb_Begin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Begin.FormattingEnabled = true;
            this.cb_Begin.Location = new System.Drawing.Point(19, 32);
            this.cb_Begin.Name = "cb_Begin";
            this.cb_Begin.Size = new System.Drawing.Size(135, 21);
            this.cb_Begin.TabIndex = 5;
            this.cb_Begin.SelectedIndexChanged += new System.EventHandler(this.cb_Begin_SelectedIndexChanged);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(502, 33);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(61, 21);
            this.btn_Add.TabIndex = 9;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_End
            // 
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
            this.lbl_Begin.AutoSize = true;
            this.lbl_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Begin.Location = new System.Drawing.Point(16, 16);
            this.lbl_Begin.Name = "lbl_Begin";
            this.lbl_Begin.Size = new System.Drawing.Size(34, 13);
            this.lbl_Begin.TabIndex = 11;
            this.lbl_Begin.Text = "Begin";
            // 
            // txt_Number
            // 
            this.txt_Number.BackColor = System.Drawing.Color.White;
            this.txt_Number.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_Number.Location = new System.Drawing.Point(345, 35);
            this.txt_Number.Name = "txt_Number";
            this.txt_Number.Size = new System.Drawing.Size(60, 20);
            this.txt_Number.TabIndex = 8;
            this.txt_Number.TextChanged += new System.EventHandler(this.txt_Number_TextChanged);
            // 
            // btn_Ok
            // 
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
            this.btn_Delete.Location = new System.Drawing.Point(514, 183);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(61, 25);
            this.btn_Delete.TabIndex = 6;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_DeleteAll
            // 
            this.btn_DeleteAll.Location = new System.Drawing.Point(514, 214);
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
            this.dataGridView1.Location = new System.Drawing.Point(31, 143);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(452, 131);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txt_Step
            // 
            this.txt_Step.Location = new System.Drawing.Point(324, 93);
            this.txt_Step.Name = "txt_Step";
            this.txt_Step.Size = new System.Drawing.Size(70, 20);
            this.txt_Step.TabIndex = 3;
            this.txt_Step.Leave += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // lbl_StartTIme
            // 
            this.lbl_StartTIme.AutoSize = true;
            this.lbl_StartTIme.BackColor = System.Drawing.Color.Transparent;
            this.lbl_StartTIme.Location = new System.Drawing.Point(250, 45);
            this.lbl_StartTIme.Name = "lbl_StartTIme";
            this.lbl_StartTIme.Size = new System.Drawing.Size(53, 13);
            this.lbl_StartTIme.TabIndex = 20;
            this.lbl_StartTIme.Text = "Start date";
            // 
            // lbl_EndTime
            // 
            this.lbl_EndTime.AutoSize = true;
            this.lbl_EndTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndTime.Location = new System.Drawing.Point(250, 71);
            this.lbl_EndTime.Name = "lbl_EndTime";
            this.lbl_EndTime.Size = new System.Drawing.Size(50, 13);
            this.lbl_EndTime.TabIndex = 21;
            this.lbl_EndTime.Text = "End date";
            // 
            // lbl_Step
            // 
            this.lbl_Step.AutoSize = true;
            this.lbl_Step.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Step.Location = new System.Drawing.Point(250, 96);
            this.lbl_Step.Name = "lbl_Step";
            this.lbl_Step.Size = new System.Drawing.Size(54, 13);
            this.lbl_Step.TabIndex = 22;
            this.lbl_Step.Text = "Step (min)";
            // 
            // dtp_End
            // 
            this.dtp_End.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_End.Location = new System.Drawing.Point(309, 67);
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.Size = new System.Drawing.Size(135, 20);
            this.dtp_End.TabIndex = 24;
            this.dtp_End.Leave += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // dtp_Start
            // 
            this.dtp_Start.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Start.Location = new System.Drawing.Point(309, 41);
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.Size = new System.Drawing.Size(135, 20);
            this.dtp_Start.TabIndex = 23;
            this.dtp_Start.Leave += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // Mean_Flows_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(606, 421);
            this.Controls.Add(this.dtp_End);
            this.Controls.Add(this.dtp_Start);
            this.Controls.Add(this.lbl_Step);
            this.Controls.Add(this.lbl_EndTime);
            this.Controls.Add(this.lbl_StartTIme);
            this.Controls.Add(this.txt_Step);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_DeleteAll);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.gb_NewElement);
            this.Controls.Add(this.lst_Groups);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Mean_Flows_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Allocation_Assistant";
            this.gb_NewElement.ResumeLayout(false);
            this.gb_NewElement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_Groups;
        private System.Windows.Forms.GroupBox gb_NewElement;
        private System.Windows.Forms.TextBox txt_Number;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.ComboBox cb_End;
        private System.Windows.Forms.ComboBox cb_Begin;
        private System.Windows.Forms.Label lbl_Begin;
        private System.Windows.Forms.Label lbl_End;
        private System.Windows.Forms.Label lbl_Alloc;
        private System.Windows.Forms.Button btn_DeleteAll;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_Step;
        private System.Windows.Forms.Label lbl_StartTIme;
        private System.Windows.Forms.Label lbl_EndTime;
        private System.Windows.Forms.Label lbl_Step;
        private System.Windows.Forms.TextBox txt_Flow_S;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dtp_End;
        private System.Windows.Forms.DateTimePicker dtp_Start;
    }
}