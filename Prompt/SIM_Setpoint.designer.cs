namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Setpoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Setpoint));
            this.label1 = new System.Windows.Forms.Label();
            this.valueBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.setpointColor = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Activate = new System.Windows.Forms.Button();
            this.SetPointList = new System.Windows.Forms.ListBox();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.AxisCBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.secondValueBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.FillColor = new System.Windows.Forms.PictureBox();
            this.rbtn_Line = new System.Windows.Forms.RadioButton();
            this.rbtn_Area = new System.Windows.Forms.RadioButton();
            this.beginDTime = new System.Windows.Forms.DateTimePicker();
            this.endDTime = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chartPositionComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.setpointColor)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FillColor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(73, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Value";
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(160, 64);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(75, 20);
            this.valueBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Axis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(289, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Stroke Color";
            // 
            // setpointColor
            // 
            this.setpointColor.BackColor = System.Drawing.Color.Black;
            this.setpointColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setpointColor.Location = new System.Drawing.Point(360, 68);
            this.setpointColor.Name = "setpointColor";
            this.setpointColor.Size = new System.Drawing.Size(33, 13);
            this.setpointColor.TabIndex = 5;
            this.setpointColor.TabStop = false;
            this.setpointColor.Click += new System.EventHandler(this.setpointColor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btn_Activate);
            this.groupBox1.Controls.Add(this.SetPointList);
            this.groupBox1.Controls.Add(this.btn_Edit);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.btn_Remove);
            this.groupBox1.Location = new System.Drawing.Point(22, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 194);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setpoint List";
            // 
            // btn_Activate
            // 
            this.btn_Activate.Enabled = false;
            this.btn_Activate.Location = new System.Drawing.Point(269, 150);
            this.btn_Activate.Name = "btn_Activate";
            this.btn_Activate.Size = new System.Drawing.Size(75, 23);
            this.btn_Activate.TabIndex = 10;
            this.btn_Activate.Text = "Activate";
            this.btn_Activate.UseVisualStyleBackColor = true;
            this.btn_Activate.Click += new System.EventHandler(this.btn_Activate_Click);
            // 
            // SetPointList
            // 
            this.SetPointList.ColumnWidth = 207;
            this.SetPointList.FormattingEnabled = true;
            this.SetPointList.HorizontalScrollbar = true;
            this.SetPointList.Location = new System.Drawing.Point(6, 19);
            this.SetPointList.Name = "SetPointList";
            this.SetPointList.Size = new System.Drawing.Size(242, 160);
            this.SetPointList.TabIndex = 0;
            this.SetPointList.SelectedIndexChanged += new System.EventHandler(this.SetPointList_SelectedIndexChanged);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Location = new System.Drawing.Point(269, 111);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(75, 23);
            this.btn_Edit.TabIndex = 9;
            this.btn_Edit.Text = "Edit";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(269, 27);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 7;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Location = new System.Drawing.Point(269, 71);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(75, 23);
            this.btn_Remove.TabIndex = 8;
            this.btn_Remove.Text = "Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(78, 343);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 10;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(266, 343);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 11;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // AxisCBox
            // 
            this.AxisCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AxisCBox.FormattingEnabled = true;
            this.AxisCBox.Items.AddRange(new object[] {
            "X",
            "Y",
            "Y2"});
            this.AxisCBox.Location = new System.Drawing.Point(227, 24);
            this.AxisCBox.Name = "AxisCBox";
            this.AxisCBox.Size = new System.Drawing.Size(58, 21);
            this.AxisCBox.TabIndex = 1;
            this.AxisCBox.SelectedIndexChanged += new System.EventHandler(this.AxisCBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(73, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Second Value";
            // 
            // secondValueBox
            // 
            this.secondValueBox.Location = new System.Drawing.Point(160, 106);
            this.secondValueBox.Name = "secondValueBox";
            this.secondValueBox.Size = new System.Drawing.Size(75, 20);
            this.secondValueBox.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(308, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Fill Color";
            // 
            // FillColor
            // 
            this.FillColor.BackColor = System.Drawing.Color.Black;
            this.FillColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FillColor.Location = new System.Drawing.Point(360, 109);
            this.FillColor.Name = "FillColor";
            this.FillColor.Size = new System.Drawing.Size(33, 13);
            this.FillColor.TabIndex = 18;
            this.FillColor.TabStop = false;
            this.FillColor.Click += new System.EventHandler(this.FillColor_Click);
            // 
            // rbtn_Line
            // 
            this.rbtn_Line.AutoSize = true;
            this.rbtn_Line.BackColor = System.Drawing.Color.Transparent;
            this.rbtn_Line.Location = new System.Drawing.Point(22, 65);
            this.rbtn_Line.Name = "rbtn_Line";
            this.rbtn_Line.Size = new System.Drawing.Size(45, 17);
            this.rbtn_Line.TabIndex = 2;
            this.rbtn_Line.TabStop = true;
            this.rbtn_Line.Text = "Line";
            this.rbtn_Line.UseVisualStyleBackColor = false;
            this.rbtn_Line.CheckedChanged += new System.EventHandler(this.rbtn_Line_CheckedChanged);
            // 
            // rbtn_Area
            // 
            this.rbtn_Area.AutoSize = true;
            this.rbtn_Area.BackColor = System.Drawing.Color.Transparent;
            this.rbtn_Area.Location = new System.Drawing.Point(22, 106);
            this.rbtn_Area.Name = "rbtn_Area";
            this.rbtn_Area.Size = new System.Drawing.Size(47, 17);
            this.rbtn_Area.TabIndex = 4;
            this.rbtn_Area.TabStop = true;
            this.rbtn_Area.Text = "Area";
            this.rbtn_Area.UseVisualStyleBackColor = false;
            this.rbtn_Area.CheckedChanged += new System.EventHandler(this.rbtn_Area_CheckedChanged);
            // 
            // beginDTime
            // 
            this.beginDTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.beginDTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.beginDTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.beginDTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.beginDTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.beginDTime.Location = new System.Drawing.Point(153, 64);
            this.beginDTime.Name = "beginDTime";
            this.beginDTime.Size = new System.Drawing.Size(125, 20);
            this.beginDTime.TabIndex = 3;
            // 
            // endDTime
            // 
            this.endDTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.endDTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.endDTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.endDTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.endDTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDTime.Location = new System.Drawing.Point(153, 106);
            this.endDTime.Name = "endDTime";
            this.endDTime.Size = new System.Drawing.Size(125, 20);
            this.endDTime.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(295, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(110, 17);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "Display on legend";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Chart Position";
            // 
            // chartPositionComboBox
            // 
            this.chartPositionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chartPositionComboBox.FormattingEnabled = true;
            this.chartPositionComboBox.Location = new System.Drawing.Point(90, 24);
            this.chartPositionComboBox.Name = "chartPositionComboBox";
            this.chartPositionComboBox.Size = new System.Drawing.Size(100, 21);
            this.chartPositionComboBox.TabIndex = 27;
            // 
            // SIM_Setpoint
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(409, 378);
            this.Controls.Add(this.chartPositionComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.endDTime);
            this.Controls.Add(this.beginDTime);
            this.Controls.Add(this.rbtn_Area);
            this.Controls.Add(this.rbtn_Line);
            this.Controls.Add(this.FillColor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.secondValueBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AxisCBox);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.setpointColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SIM_Setpoint";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setpoints Management";
            ((System.ComponentModel.ISupportInitialize)(this.setpointColor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FillColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox valueBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox setpointColor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_Edit;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ListBox SetPointList;
        private System.Windows.Forms.ComboBox AxisCBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox secondValueBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox FillColor;
        private System.Windows.Forms.RadioButton rbtn_Line;
        private System.Windows.Forms.RadioButton rbtn_Area;
        private System.Windows.Forms.DateTimePicker beginDTime;
        private System.Windows.Forms.DateTimePicker endDTime;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btn_Activate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox chartPositionComboBox;
    }
}