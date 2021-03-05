namespace SIMCORE_TOOL.Assistant
{
    partial class usaStandardParamAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usaStandardParamAssistant));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_osPercent = new System.Windows.Forms.TextBox();
            this.tb_oogPercent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_edsScreenRatePerHour = new System.Windows.Forms.TextBox();
            this.tb_edsScreenRatePerMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_falseAlarmEdsInt = new System.Windows.Forms.TextBox();
            this.tb_falseAlarmEdsDom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_etdScreenRateForOS = new System.Windows.Forms.TextBox();
            this.tb_etdScreenRateIntNoImg = new System.Windows.Forms.TextBox();
            this.tb_etdScreenRateIntImg = new System.Windows.Forms.TextBox();
            this.tb_etdScreenRateDomNoImg = new System.Windows.Forms.TextBox();
            this.tb_etdScreenRateDomImg = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tb_lostInTrackingOS = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tb_clearRate = new System.Windows.Forms.TextBox();
            this.tb_osrProcessingRatePerHour = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tb_osPercent);
            this.groupBox1.Controls.Add(this.tb_oogPercent);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(30, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Special Bags Percentages";
            // 
            // tb_osPercent
            // 
            this.tb_osPercent.Location = new System.Drawing.Point(339, 21);
            this.tb_osPercent.Name = "tb_osPercent";
            this.tb_osPercent.Size = new System.Drawing.Size(62, 20);
            this.tb_osPercent.TabIndex = 3;
            this.tb_osPercent.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tb_oogPercent
            // 
            this.tb_oogPercent.Location = new System.Drawing.Point(123, 25);
            this.tb_oogPercent.Name = "tb_oogPercent";
            this.tb_oogPercent.Size = new System.Drawing.Size(57, 20);
            this.tb_oogPercent.TabIndex = 2;
            this.tb_oogPercent.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Oversize percent";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Out-of-Gauge percent";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.tb_edsScreenRatePerHour);
            this.groupBox2.Controls.Add(this.tb_edsScreenRatePerMin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(30, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(521, 65);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EDS Screening Rates";
            // 
            // tb_edsScreenRatePerHour
            // 
            this.tb_edsScreenRatePerHour.Location = new System.Drawing.Point(339, 29);
            this.tb_edsScreenRatePerHour.Name = "tb_edsScreenRatePerHour";
            this.tb_edsScreenRatePerHour.Size = new System.Drawing.Size(62, 20);
            this.tb_edsScreenRatePerHour.TabIndex = 3;
            this.tb_edsScreenRatePerHour.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.tb_edsScreenRatePerHour.Validated += new System.EventHandler(this.edsScreenRatesPerHourInput);
            // 
            // tb_edsScreenRatePerMin
            // 
            this.tb_edsScreenRatePerMin.Location = new System.Drawing.Point(120, 29);
            this.tb_edsScreenRatePerMin.Name = "tb_edsScreenRatePerMin";
            this.tb_edsScreenRatePerMin.Size = new System.Drawing.Size(57, 20);
            this.tb_edsScreenRatePerMin.TabIndex = 2;
            this.tb_edsScreenRatePerMin.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.tb_edsScreenRatePerMin.Validated += new System.EventHandler(this.edsScreenRatesPerMinuteInput);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Rate per hour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rate per minute";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.tb_falseAlarmEdsInt);
            this.groupBox3.Controls.Add(this.tb_falseAlarmEdsDom);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(30, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(521, 53);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "False Alarm";
            // 
            // tb_falseAlarmEdsInt
            // 
            this.tb_falseAlarmEdsInt.Location = new System.Drawing.Point(339, 26);
            this.tb_falseAlarmEdsInt.Name = "tb_falseAlarmEdsInt";
            this.tb_falseAlarmEdsInt.Size = new System.Drawing.Size(62, 20);
            this.tb_falseAlarmEdsInt.TabIndex = 3;
            this.tb_falseAlarmEdsInt.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tb_falseAlarmEdsDom
            // 
            this.tb_falseAlarmEdsDom.Location = new System.Drawing.Point(124, 26);
            this.tb_falseAlarmEdsDom.Name = "tb_falseAlarmEdsDom";
            this.tb_falseAlarmEdsDom.Size = new System.Drawing.Size(57, 20);
            this.tb_falseAlarmEdsDom.TabIndex = 2;
            this.tb_falseAlarmEdsDom.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(216, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "False Alarm EDS INT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "False Alarm EDS DOM";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.tb_etdScreenRateForOS);
            this.groupBox4.Controls.Add(this.tb_etdScreenRateIntNoImg);
            this.groupBox4.Controls.Add(this.tb_etdScreenRateIntImg);
            this.groupBox4.Controls.Add(this.tb_etdScreenRateDomNoImg);
            this.groupBox4.Controls.Add(this.tb_etdScreenRateDomImg);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(30, 235);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(521, 127);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ETD Screening Rates";
            // 
            // tb_etdScreenRateForOS
            // 
            this.tb_etdScreenRateForOS.Location = new System.Drawing.Point(280, 99);
            this.tb_etdScreenRateForOS.Name = "tb_etdScreenRateForOS";
            this.tb_etdScreenRateForOS.Size = new System.Drawing.Size(63, 20);
            this.tb_etdScreenRateForOS.TabIndex = 9;
            this.tb_etdScreenRateForOS.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tb_etdScreenRateIntNoImg
            // 
            this.tb_etdScreenRateIntNoImg.Location = new System.Drawing.Point(441, 69);
            this.tb_etdScreenRateIntNoImg.Name = "tb_etdScreenRateIntNoImg";
            this.tb_etdScreenRateIntNoImg.Size = new System.Drawing.Size(62, 20);
            this.tb_etdScreenRateIntNoImg.TabIndex = 8;
            this.tb_etdScreenRateIntNoImg.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tb_etdScreenRateIntImg
            // 
            this.tb_etdScreenRateIntImg.Location = new System.Drawing.Point(188, 69);
            this.tb_etdScreenRateIntImg.Name = "tb_etdScreenRateIntImg";
            this.tb_etdScreenRateIntImg.Size = new System.Drawing.Size(57, 20);
            this.tb_etdScreenRateIntImg.TabIndex = 7;
            this.tb_etdScreenRateIntImg.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tb_etdScreenRateDomNoImg
            // 
            this.tb_etdScreenRateDomNoImg.Location = new System.Drawing.Point(441, 38);
            this.tb_etdScreenRateDomNoImg.Name = "tb_etdScreenRateDomNoImg";
            this.tb_etdScreenRateDomNoImg.Size = new System.Drawing.Size(62, 20);
            this.tb_etdScreenRateDomNoImg.TabIndex = 6;
            this.tb_etdScreenRateDomNoImg.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tb_etdScreenRateDomImg
            // 
            this.tb_etdScreenRateDomImg.Location = new System.Drawing.Point(188, 38);
            this.tb_etdScreenRateDomImg.Name = "tb_etdScreenRateDomImg";
            this.tb_etdScreenRateDomImg.Size = new System.Drawing.Size(57, 20);
            this.tb_etdScreenRateDomImg.TabIndex = 5;
            this.tb_etdScreenRateDomImg.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(101, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(173, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Rate (bags/hour) for Oversize bags";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(264, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(170, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Rate (bags/hour) for INT no image";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(264, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(177, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Rate (bags/hour) for DOM no image";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(177, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Rate (bags/hour) for INT with image";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Rate (bags/hour) for DOM with image";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.tb_lostInTrackingOS);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Location = new System.Drawing.Point(30, 373);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(521, 60);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Lost in Tracking";
            // 
            // tb_lostInTrackingOS
            // 
            this.tb_lostInTrackingOS.Location = new System.Drawing.Point(205, 26);
            this.tb_lostInTrackingOS.Name = "tb_lostInTrackingOS";
            this.tb_lostInTrackingOS.Size = new System.Drawing.Size(78, 20);
            this.tb_lostInTrackingOS.TabIndex = 3;
            this.tb_lostInTrackingOS.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(163, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Rate of lost in tracking baggages";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(37, 448);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Clear rate";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(246, 448);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(147, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "OSR processing rate per hour";
            // 
            // tb_clearRate
            // 
            this.tb_clearRate.Location = new System.Drawing.Point(112, 445);
            this.tb_clearRate.Name = "tb_clearRate";
            this.tb_clearRate.Size = new System.Drawing.Size(100, 20);
            this.tb_clearRate.TabIndex = 7;
            this.tb_clearRate.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tb_osrProcessingRatePerHour
            // 
            this.tb_osrProcessingRatePerHour.Location = new System.Drawing.Point(399, 445);
            this.tb_osrProcessingRatePerHour.Name = "tb_osrProcessingRatePerHour";
            this.tb_osrProcessingRatePerHour.Size = new System.Drawing.Size(100, 20);
            this.tb_osrProcessingRatePerHour.TabIndex = 8;
            this.tb_osrProcessingRatePerHour.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(137, 489);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(310, 489);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // usaStandardParamAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 521);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tb_osrProcessingRatePerHour);
            this.Controls.Add(this.tb_clearRate);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(579, 560);
            this.MinimumSize = new System.Drawing.Size(579, 560);
            this.Name = "usaStandardParamAssistant";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "USA Standard Parameters Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tb_osPercent;
        private System.Windows.Forms.TextBox tb_oogPercent;
        private System.Windows.Forms.TextBox tb_edsScreenRatePerMin;
        private System.Windows.Forms.TextBox tb_edsScreenRatePerHour;
        private System.Windows.Forms.TextBox tb_falseAlarmEdsInt;
        private System.Windows.Forms.TextBox tb_falseAlarmEdsDom;
        private System.Windows.Forms.TextBox tb_etdScreenRateDomNoImg;
        private System.Windows.Forms.TextBox tb_etdScreenRateDomImg;
        private System.Windows.Forms.TextBox tb_etdScreenRateForOS;
        private System.Windows.Forms.TextBox tb_etdScreenRateIntNoImg;
        private System.Windows.Forms.TextBox tb_etdScreenRateIntImg;
        private System.Windows.Forms.TextBox tb_lostInTrackingOS;
        private System.Windows.Forms.TextBox tb_clearRate;
        private System.Windows.Forms.TextBox tb_osrProcessingRatePerHour;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}