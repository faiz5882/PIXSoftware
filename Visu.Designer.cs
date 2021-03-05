namespace SIMCORE_TOOL
{
    partial class Visu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visu));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_ChartPosition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_ChartAxis = new System.Windows.Forms.ComboBox();
            this.cb_Scrolbar = new System.Windows.Forms.CheckBox();
            this.nUD_Beginvalue = new System.Windows.Forms.NumericUpDown();
            this.nUD_AxisLength = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tb_AxisName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_ColumnOrder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resetAxisButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.barSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.barWidthPercentageTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gapPercentTextBox = new System.Windows.Forms.TextBox();
            this.barWidthTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Setpoint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Beginvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_AxisLength)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.barSettingsGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(15, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chart position :";
            // 
            // cb_ChartPosition
            // 
            this.cb_ChartPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ChartPosition.FormattingEnabled = true;
            this.cb_ChartPosition.Items.AddRange(new object[] {
            "Top left",
            "Top Right",
            "Bottom Left",
            "Bottom Right"});
            this.cb_ChartPosition.Location = new System.Drawing.Point(142, 93);
            this.cb_ChartPosition.Name = "cb_ChartPosition";
            this.cb_ChartPosition.Size = new System.Drawing.Size(98, 21);
            this.cb_ChartPosition.TabIndex = 1;
            this.cb_ChartPosition.SelectedIndexChanged += new System.EventHandler(this.cb_ChartPosition_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(15, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Chart axis :";
            // 
            // cb_ChartAxis
            // 
            this.cb_ChartAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ChartAxis.FormattingEnabled = true;
            this.cb_ChartAxis.Items.AddRange(new object[] {
            "X Axis",
            "Y Axis",
            "Y2 Axis"});
            this.cb_ChartAxis.Location = new System.Drawing.Point(142, 132);
            this.cb_ChartAxis.Name = "cb_ChartAxis";
            this.cb_ChartAxis.Size = new System.Drawing.Size(98, 21);
            this.cb_ChartAxis.TabIndex = 3;
            this.cb_ChartAxis.SelectedIndexChanged += new System.EventHandler(this.cb_ChartAxis_SelectedIndexChanged);
            // 
            // cb_Scrolbar
            // 
            this.cb_Scrolbar.AutoSize = true;
            this.cb_Scrolbar.BackColor = System.Drawing.Color.Transparent;
            this.cb_Scrolbar.Location = new System.Drawing.Point(18, 418);
            this.cb_Scrolbar.Name = "cb_Scrolbar";
            this.cb_Scrolbar.Size = new System.Drawing.Size(126, 17);
            this.cb_Scrolbar.TabIndex = 4;
            this.cb_Scrolbar.Text = "Show X axis scrollbar";
            this.cb_Scrolbar.UseVisualStyleBackColor = false;
            this.cb_Scrolbar.CheckedChanged += new System.EventHandler(this.cb_Scrolbar_CheckedChanged);
            // 
            // nUD_Beginvalue
            // 
            this.nUD_Beginvalue.Location = new System.Drawing.Point(177, 58);
            this.nUD_Beginvalue.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nUD_Beginvalue.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nUD_Beginvalue.Name = "nUD_Beginvalue";
            this.nUD_Beginvalue.Size = new System.Drawing.Size(85, 20);
            this.nUD_Beginvalue.TabIndex = 5;
            this.nUD_Beginvalue.ValueChanged += new System.EventHandler(this.nUD_Beginvalue_ValueChanged);
            // 
            // nUD_AxisLength
            // 
            this.nUD_AxisLength.Location = new System.Drawing.Point(177, 94);
            this.nUD_AxisLength.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nUD_AxisLength.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nUD_AxisLength.Name = "nUD_AxisLength";
            this.nUD_AxisLength.Size = new System.Drawing.Size(85, 20);
            this.nUD_AxisLength.TabIndex = 6;
            this.nUD_AxisLength.ValueChanged += new System.EventHandler(this.nUD_AxisLength_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Axis begin :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(9, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Axis range :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(117, 58);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(145, 20);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // tb_AxisName
            // 
            this.tb_AxisName.Location = new System.Drawing.Point(117, 22);
            this.tb_AxisName.Name = "tb_AxisName";
            this.tb_AxisName.Size = new System.Drawing.Size(145, 20);
            this.tb_AxisName.TabIndex = 10;
            this.tb_AxisName.TextChanged += new System.EventHandler(this.tb_AxisName_TextChanged);
            this.tb_AxisName.Leave += new System.EventHandler(this.tb_AxisName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(9, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Axis name :";
            // 
            // btn_ColumnOrder
            // 
            this.btn_ColumnOrder.Location = new System.Drawing.Point(142, 56);
            this.btn_ColumnOrder.Name = "btn_ColumnOrder";
            this.btn_ColumnOrder.Size = new System.Drawing.Size(98, 23);
            this.btn_ColumnOrder.TabIndex = 13;
            this.btn_ColumnOrder.Text = "Change";
            this.btn_ColumnOrder.UseVisualStyleBackColor = true;
            this.btn_ColumnOrder.Click += new System.EventHandler(this.btn_ColumnOrder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resetAxisButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nUD_AxisLength);
            this.groupBox1.Controls.Add(this.tb_AxisName);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.nUD_Beginvalue);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBox1.Location = new System.Drawing.Point(6, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(294, 128);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Axis settings";
            // 
            // resetAxisButton
            // 
            this.resetAxisButton.Location = new System.Drawing.Point(67, 117);
            this.resetAxisButton.Name = "resetAxisButton";
            this.resetAxisButton.Size = new System.Drawing.Size(141, 21);
            this.resetAxisButton.TabIndex = 4;
            this.resetAxisButton.Text = "Reset Axis Range";
            this.resetAxisButton.UseVisualStyleBackColor = true;
            this.resetAxisButton.Visible = false;
            this.resetAxisButton.Click += new System.EventHandler(this.resetAxisButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.barSettingsGroupBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cb_Scrolbar);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btn_Setpoint);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btn_ColumnOrder);
            this.groupBox2.Controls.Add(this.cb_ChartAxis);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cb_ChartPosition);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 443);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chart settings";
            // 
            // barSettingsGroupBox
            // 
            this.barSettingsGroupBox.Controls.Add(this.barWidthPercentageTextBox);
            this.barSettingsGroupBox.Controls.Add(this.label10);
            this.barSettingsGroupBox.Controls.Add(this.groupBox4);
            this.barSettingsGroupBox.Location = new System.Drawing.Point(6, 295);
            this.barSettingsGroupBox.Name = "barSettingsGroupBox";
            this.barSettingsGroupBox.Size = new System.Drawing.Size(294, 117);
            this.barSettingsGroupBox.TabIndex = 17;
            this.barSettingsGroupBox.TabStop = false;
            this.barSettingsGroupBox.Text = "Bar settings (0 = default)";
            // 
            // barWidthPercentageTextBox
            // 
            this.barWidthPercentageTextBox.Location = new System.Drawing.Point(177, 91);
            this.barWidthPercentageTextBox.Name = "barWidthPercentageTextBox";
            this.barWidthPercentageTextBox.Size = new System.Drawing.Size(40, 20);
            this.barWidthPercentageTextBox.TabIndex = 2;
            this.barWidthPercentageTextBox.Leave += new System.EventHandler(this.barWidthPercentageTextBox_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Bar width percent (X = string)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gapPercentTextBox);
            this.groupBox4.Controls.Add(this.barWidthTextBox);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(-6, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(300, 67);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // gapPercentTextBox
            // 
            this.gapPercentTextBox.Location = new System.Drawing.Point(183, 39);
            this.gapPercentTextBox.Name = "gapPercentTextBox";
            this.gapPercentTextBox.Size = new System.Drawing.Size(40, 20);
            this.gapPercentTextBox.TabIndex = 3;
            this.gapPercentTextBox.Leave += new System.EventHandler(this.gapPercentTextBox_Leave);
            // 
            // barWidthTextBox
            // 
            this.barWidthTextBox.Location = new System.Drawing.Point(183, 13);
            this.barWidthTextBox.Name = "barWidthTextBox";
            this.barWidthTextBox.Size = new System.Drawing.Size(40, 20);
            this.barWidthTextBox.TabIndex = 2;
            this.barWidthTextBox.Leave += new System.EventHandler(this.barWidthTextBox_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Gap percent";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Bar width percent (X = value)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Columns position :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Setpoints :";
            // 
            // btn_Setpoint
            // 
            this.btn_Setpoint.Image = ((System.Drawing.Image)(resources.GetObject("btn_Setpoint.Image")));
            this.btn_Setpoint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Setpoint.Location = new System.Drawing.Point(142, 19);
            this.btn_Setpoint.Name = "btn_Setpoint";
            this.btn_Setpoint.Size = new System.Drawing.Size(98, 22);
            this.btn_Setpoint.TabIndex = 12;
            this.btn_Setpoint.Text = "   Add / Edit";
            this.btn_Setpoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Setpoint.UseVisualStyleBackColor = true;
            this.btn_Setpoint.Click += new System.EventHandler(this.btn_Setpoint_Click);
            // 
            // Visu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 467);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Visu";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Axis Management";
            this.Load += new System.EventHandler(this.Visu_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Visu_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Beginvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_AxisLength)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.barSettingsGroupBox.ResumeLayout(false);
            this.barSettingsGroupBox.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_ChartPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_Scrolbar;
        private System.Windows.Forms.NumericUpDown nUD_Beginvalue;
        private System.Windows.Forms.NumericUpDown nUD_AxisLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cb_ChartAxis;
        private System.Windows.Forms.TextBox tb_AxisName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_ColumnOrder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Setpoint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox barSettingsGroupBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox gapPercentTextBox;
        private System.Windows.Forms.TextBox barWidthTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox barWidthPercentageTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button resetAxisButton;



    }
}