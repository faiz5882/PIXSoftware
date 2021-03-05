namespace SIMCORE_TOOL.Assistant
{
    partial class AllocationAssistantManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllocationAssistantManager));
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Step = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Step = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Edit_btn = new System.Windows.Forms.Button();
            this.AllocationTable = new System.Windows.Forms.DataGridView();
            this.Add_btn = new System.Windows.Forms.Button();
            this.DeleteAll_btn = new System.Windows.Forms.Button();
            this.Delete_btn = new System.Windows.Forms.Button();
            this.Ok_btn = new System.Windows.Forms.Button();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dt_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dt_BeginDate = new System.Windows.Forms.DateTimePicker();
            this.Begin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StationsOpened = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassportType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlightCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Airlines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlightID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllocationTable)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Begin date";
            // 
            // lbl_Step
            // 
            this.lbl_Step.AutoSize = true;
            this.lbl_Step.Location = new System.Drawing.Point(408, 32);
            this.lbl_Step.Name = "lbl_Step";
            this.lbl_Step.Size = new System.Drawing.Size(29, 13);
            this.lbl_Step.TabIndex = 2;
            this.lbl_Step.Text = "Step";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "End date";
            // 
            // tb_Step
            // 
            this.tb_Step.Location = new System.Drawing.Point(445, 29);
            this.tb_Step.Name = "tb_Step";
            this.tb_Step.Size = new System.Drawing.Size(58, 20);
            this.tb_Step.TabIndex = 5;
            this.tb_Step.TextChanged += new System.EventHandler(this.tb_Step_TextChanged);
            this.tb_Step.Leave += new System.EventHandler(this.dt_BeginDate_Leave);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(25, 33);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(478, 108);
            this.listBox1.TabIndex = 6;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Edit_btn);
            this.groupBox1.Controls.Add(this.AllocationTable);
            this.groupBox1.Controls.Add(this.Add_btn);
            this.groupBox1.Controls.Add(this.DeleteAll_btn);
            this.groupBox1.Controls.Add(this.Delete_btn);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(29, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 307);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Allocation";
            // 
            // Edit_btn
            // 
            this.Edit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Edit_btn.Enabled = false;
            this.Edit_btn.Location = new System.Drawing.Point(399, 274);
            this.Edit_btn.Name = "Edit_btn";
            this.Edit_btn.Size = new System.Drawing.Size(75, 23);
            this.Edit_btn.TabIndex = 11;
            this.Edit_btn.Text = "Edit";
            this.Edit_btn.UseVisualStyleBackColor = true;
            this.Edit_btn.Click += new System.EventHandler(this.Edit_btn_Click);
            // 
            // AllocationTable
            // 
            this.AllocationTable.AllowUserToAddRows = false;
            this.AllocationTable.AllowUserToDeleteRows = false;
            this.AllocationTable.AllowUserToResizeRows = false;
            this.AllocationTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AllocationTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.AllocationTable.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.AllocationTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Begin,
            this.End,
            this.StationsOpened,
            this.TimeType,
            this.PassportType,
            this.ClassType,
            this.FlightCategory,
            this.Airlines,
            this.FlightID});
            this.AllocationTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.AllocationTable.Location = new System.Drawing.Point(25, 147);
            this.AllocationTable.Name = "AllocationTable";
            this.AllocationTable.ReadOnly = true;
            this.AllocationTable.RowHeadersVisible = false;
            this.AllocationTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AllocationTable.ShowEditingIcon = false;
            this.AllocationTable.Size = new System.Drawing.Size(478, 112);
            this.AllocationTable.TabIndex = 0;
            this.AllocationTable.SelectionChanged += new System.EventHandler(this.AllocationTable_SelectionChanged);
            // 
            // Add_btn
            // 
            this.Add_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Add_btn.Enabled = false;
            this.Add_btn.Location = new System.Drawing.Point(318, 274);
            this.Add_btn.Name = "Add_btn";
            this.Add_btn.Size = new System.Drawing.Size(75, 23);
            this.Add_btn.TabIndex = 10;
            this.Add_btn.Text = "Add";
            this.Add_btn.UseVisualStyleBackColor = true;
            this.Add_btn.Click += new System.EventHandler(this.Add_btn_Click);
            // 
            // DeleteAll_btn
            // 
            this.DeleteAll_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteAll_btn.Enabled = false;
            this.DeleteAll_btn.Location = new System.Drawing.Point(139, 274);
            this.DeleteAll_btn.Name = "DeleteAll_btn";
            this.DeleteAll_btn.Size = new System.Drawing.Size(75, 23);
            this.DeleteAll_btn.TabIndex = 9;
            this.DeleteAll_btn.Text = "Delete All";
            this.DeleteAll_btn.UseVisualStyleBackColor = true;
            this.DeleteAll_btn.Click += new System.EventHandler(this.DeleteAll_btn_Click);
            // 
            // Delete_btn
            // 
            this.Delete_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Delete_btn.Enabled = false;
            this.Delete_btn.Location = new System.Drawing.Point(53, 274);
            this.Delete_btn.Name = "Delete_btn";
            this.Delete_btn.Size = new System.Drawing.Size(75, 23);
            this.Delete_btn.TabIndex = 8;
            this.Delete_btn.Text = "Delete";
            this.Delete_btn.UseVisualStyleBackColor = true;
            this.Delete_btn.Click += new System.EventHandler(this.Delete_btn_Click);
            // 
            // Ok_btn
            // 
            this.Ok_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Ok_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok_btn.Location = new System.Drawing.Point(54, 412);
            this.Ok_btn.Name = "Ok_btn";
            this.Ok_btn.Size = new System.Drawing.Size(75, 23);
            this.Ok_btn.TabIndex = 8;
            this.Ok_btn.Text = "Ok";
            this.Ok_btn.UseVisualStyleBackColor = true;
            this.Ok_btn.Click += new System.EventHandler(this.Ok_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_btn.Location = new System.Drawing.Point(457, 412);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.Cancel_btn.TabIndex = 9;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dt_EndDate);
            this.groupBox2.Controls.Add(this.dt_BeginDate);
            this.groupBox2.Controls.Add(this.tb_Step);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lbl_Step);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(29, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(534, 67);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time settings";
            // 
            // dt_EndDate
            // 
            this.dt_EndDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dt_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_EndDate.Location = new System.Drawing.Point(274, 29);
            this.dt_EndDate.Name = "dt_EndDate";
            this.dt_EndDate.Size = new System.Drawing.Size(128, 20);
            this.dt_EndDate.TabIndex = 7;
            this.dt_EndDate.Leave += new System.EventHandler(this.dt_BeginDate_Leave);
            // 
            // dt_BeginDate
            // 
            this.dt_BeginDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dt_BeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_BeginDate.Location = new System.Drawing.Point(86, 29);
            this.dt_BeginDate.Name = "dt_BeginDate";
            this.dt_BeginDate.Size = new System.Drawing.Size(128, 20);
            this.dt_BeginDate.TabIndex = 1;
            this.dt_BeginDate.Leave += new System.EventHandler(this.dt_BeginDate_Leave);
            // 
            // Begin
            // 
            this.Begin.Frozen = true;
            this.Begin.HeaderText = "Begin";
            this.Begin.Name = "Begin";
            this.Begin.ReadOnly = true;
            this.Begin.Width = 59;
            // 
            // End
            // 
            this.End.HeaderText = "End";
            this.End.Name = "End";
            this.End.ReadOnly = true;
            this.End.Width = 51;
            // 
            // StationsOpened
            // 
            this.StationsOpened.HeaderText = "Stations Opened";
            this.StationsOpened.Name = "StationsOpened";
            this.StationsOpened.ReadOnly = true;
            this.StationsOpened.Width = 111;
            // 
            // TimeType
            // 
            this.TimeType.HeaderText = "Time Type";
            this.TimeType.Name = "TimeType";
            this.TimeType.ReadOnly = true;
            this.TimeType.Width = 82;
            // 
            // PassportType
            // 
            this.PassportType.HeaderText = "Passport Type";
            this.PassportType.Name = "PassportType";
            this.PassportType.ReadOnly = true;
            // 
            // ClassType
            // 
            this.ClassType.HeaderText = "Class Type";
            this.ClassType.Name = "ClassType";
            this.ClassType.ReadOnly = true;
            this.ClassType.Width = 84;
            // 
            // FlightCategory
            // 
            this.FlightCategory.HeaderText = "Flight Category";
            this.FlightCategory.Name = "FlightCategory";
            this.FlightCategory.ReadOnly = true;
            this.FlightCategory.Width = 102;
            // 
            // Airlines
            // 
            this.Airlines.HeaderText = "Airlines";
            this.Airlines.Name = "Airlines";
            this.Airlines.ReadOnly = true;
            this.Airlines.Width = 65;
            // 
            // FlightID
            // 
            this.FlightID.HeaderText = "Flight Identification";
            this.FlightID.Name = "FlightID";
            this.FlightID.ReadOnly = true;
            this.FlightID.Width = 120;
            // 
            // AllocationAssistantManager
            // 
            this.AcceptButton = this.Ok_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_btn;
            this.ClientSize = new System.Drawing.Size(591, 445);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.Ok_btn);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "AllocationAssistantManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Allocation Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AllocationAssistantManager_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AllocationTable)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Step;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Step;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Add_btn;
        private System.Windows.Forms.Button DeleteAll_btn;
        private System.Windows.Forms.Button Delete_btn;
        private System.Windows.Forms.Button Ok_btn;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.DataGridView AllocationTable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dt_EndDate;
        private System.Windows.Forms.DateTimePicker dt_BeginDate;
        private System.Windows.Forms.Button Edit_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Begin;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.DataGridViewTextBoxColumn StationsOpened;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn PassportType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassType;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlightCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Airlines;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlightID;
    }
}