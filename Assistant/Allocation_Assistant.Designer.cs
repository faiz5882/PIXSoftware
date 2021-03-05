namespace SIMCORE_TOOL.Assistant
{
    partial class Allocation_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Allocation_Assistant));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lst_Groups = new System.Windows.Forms.ListBox();
            this.gb_NewElement = new System.Windows.Forms.GroupBox();
            this.cb_End = new System.Windows.Forms.ComboBox();
            this.lbl_Alloc = new System.Windows.Forms.Label();
            this.cb_Begin = new System.Windows.Forms.ComboBox();
            this.lbl_Passport = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_End = new System.Windows.Forms.Label();
            this.cb_PassportType = new System.Windows.Forms.ComboBox();
            this.lbl_Begin = new System.Windows.Forms.Label();
            this.txt_Number = new System.Windows.Forms.TextBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_DeleteAll = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtp_Start = new System.Windows.Forms.DateTimePicker();
            this.dtp_End = new System.Windows.Forms.DateTimePicker();
            this.txt_Step = new System.Windows.Forms.TextBox();
            this.lbl_StartTIme = new System.Windows.Forms.Label();
            this.lbl_EndTime = new System.Windows.Forms.Label();
            this.lbl_Step = new System.Windows.Forms.Label();
            this.gb_NewElement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lst_Groups
            // 
            this.lst_Groups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.lst_Groups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lst_Groups.FormattingEnabled = true;
            resources.ApplyResources(this.lst_Groups, "lst_Groups");
            this.lst_Groups.Name = "lst_Groups";
            this.lst_Groups.SelectedIndexChanged += new System.EventHandler(this.lst_Groups_SelectedIndexChanged);
            // 
            // gb_NewElement
            // 
            this.gb_NewElement.BackColor = System.Drawing.Color.Transparent;
            this.gb_NewElement.Controls.Add(this.cb_End);
            this.gb_NewElement.Controls.Add(this.lbl_Alloc);
            this.gb_NewElement.Controls.Add(this.cb_Begin);
            this.gb_NewElement.Controls.Add(this.lbl_Passport);
            this.gb_NewElement.Controls.Add(this.btn_Add);
            this.gb_NewElement.Controls.Add(this.lbl_End);
            this.gb_NewElement.Controls.Add(this.cb_PassportType);
            this.gb_NewElement.Controls.Add(this.lbl_Begin);
            this.gb_NewElement.Controls.Add(this.txt_Number);
            resources.ApplyResources(this.gb_NewElement, "gb_NewElement");
            this.gb_NewElement.Name = "gb_NewElement";
            this.gb_NewElement.TabStop = false;
            // 
            // cb_End
            // 
            this.cb_End.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_End.FormattingEnabled = true;
            resources.ApplyResources(this.cb_End, "cb_End");
            this.cb_End.Name = "cb_End";
            // 
            // lbl_Alloc
            // 
            resources.ApplyResources(this.lbl_Alloc, "lbl_Alloc");
            this.lbl_Alloc.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Alloc.Name = "lbl_Alloc";
            // 
            // cb_Begin
            // 
            this.cb_Begin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Begin.FormattingEnabled = true;
            resources.ApplyResources(this.cb_Begin, "cb_Begin");
            this.cb_Begin.Name = "cb_Begin";
            this.cb_Begin.SelectedIndexChanged += new System.EventHandler(this.cb_Begin_SelectedIndexChanged);
            // 
            // lbl_Passport
            // 
            resources.ApplyResources(this.lbl_Passport, "lbl_Passport");
            this.lbl_Passport.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Passport.Name = "lbl_Passport";
            // 
            // btn_Add
            // 
            resources.ApplyResources(this.btn_Add, "btn_Add");
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_End
            // 
            resources.ApplyResources(this.lbl_End, "lbl_End");
            this.lbl_End.BackColor = System.Drawing.Color.Transparent;
            this.lbl_End.Name = "lbl_End";
            // 
            // cb_PassportType
            // 
            this.cb_PassportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PassportType.FormattingEnabled = true;
            this.cb_PassportType.Items.AddRange(new object[] {
            resources.GetString("cb_PassportType.Items"),
            resources.GetString("cb_PassportType.Items1"),
            resources.GetString("cb_PassportType.Items2")});
            resources.ApplyResources(this.cb_PassportType, "cb_PassportType");
            this.cb_PassportType.Name = "cb_PassportType";
            // 
            // lbl_Begin
            // 
            resources.ApplyResources(this.lbl_Begin, "lbl_Begin");
            this.lbl_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Begin.Name = "lbl_Begin";
            // 
            // txt_Number
            // 
            this.txt_Number.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Number.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_Number, "txt_Number");
            this.txt_Number.Name = "txt_Number";
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btn_Ok, "btn_Ok");
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_Cancel, "btn_Cancel");
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Delete
            // 
            resources.ApplyResources(this.btn_Delete, "btn_Delete");
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_DeleteAll
            // 
            resources.ApplyResources(this.btn_DeleteAll, "btn_DeleteAll");
            this.btn_DeleteAll.Name = "btn_DeleteAll";
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
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // dtp_Start
            // 
            resources.ApplyResources(this.dtp_Start, "dtp_Start");
            this.dtp_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.Leave += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // dtp_End
            // 
            resources.ApplyResources(this.dtp_End, "dtp_End");
            this.dtp_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.Leave += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // txt_Step
            // 
            resources.ApplyResources(this.txt_Step, "txt_Step");
            this.txt_Step.Name = "txt_Step";
            this.txt_Step.Leave += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // lbl_StartTIme
            // 
            resources.ApplyResources(this.lbl_StartTIme, "lbl_StartTIme");
            this.lbl_StartTIme.BackColor = System.Drawing.Color.Transparent;
            this.lbl_StartTIme.Name = "lbl_StartTIme";
            // 
            // lbl_EndTime
            // 
            resources.ApplyResources(this.lbl_EndTime, "lbl_EndTime");
            this.lbl_EndTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndTime.Name = "lbl_EndTime";
            // 
            // lbl_Step
            // 
            resources.ApplyResources(this.lbl_Step, "lbl_Step");
            this.lbl_Step.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Step.Name = "lbl_Step";
            // 
            // Allocation_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.Controls.Add(this.lbl_Step);
            this.Controls.Add(this.lbl_EndTime);
            this.Controls.Add(this.lbl_StartTIme);
            this.Controls.Add(this.txt_Step);
            this.Controls.Add(this.dtp_End);
            this.Controls.Add(this.dtp_Start);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_DeleteAll);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.gb_NewElement);
            this.Controls.Add(this.lst_Groups);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Allocation_Assistant";
            this.ShowInTaskbar = false;
            this.gb_NewElement.ResumeLayout(false);
            this.gb_NewElement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_Groups;
        private System.Windows.Forms.GroupBox gb_NewElement;
        private System.Windows.Forms.ComboBox cb_PassportType;
        private System.Windows.Forms.TextBox txt_Number;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.ComboBox cb_End;
        private System.Windows.Forms.ComboBox cb_Begin;
        private System.Windows.Forms.Label lbl_Begin;
        private System.Windows.Forms.Label lbl_End;
        private System.Windows.Forms.Label lbl_Passport;
        private System.Windows.Forms.Label lbl_Alloc;
        private System.Windows.Forms.Button btn_DeleteAll;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dtp_Start;
        private System.Windows.Forms.DateTimePicker dtp_End;
        private System.Windows.Forms.TextBox txt_Step;
        private System.Windows.Forms.Label lbl_StartTIme;
        private System.Windows.Forms.Label lbl_EndTime;
        private System.Windows.Forms.Label lbl_Step;
    }
}