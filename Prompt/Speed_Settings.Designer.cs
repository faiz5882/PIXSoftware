namespace SIMCORE_TOOL.Prompt
{
    partial class Speed_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Speed_Settings));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_Unit = new System.Windows.Forms.Label();
            this.txt_DistanceUnit = new System.Windows.Forms.TextBox();
            this.lbl_Initial = new System.Windows.Forms.Label();
            this.txt_DistanceInitial = new System.Windows.Forms.TextBox();
            this.gb_Distance = new System.Windows.Forms.GroupBox();
            this.gb_Time = new System.Windows.Forms.GroupBox();
            this.cb_TimeUnit = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_TimeInitial = new System.Windows.Forms.TextBox();
            this.gb_Speed = new System.Windows.Forms.GroupBox();
            this.lbl_Tolerance = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_Speed = new System.Windows.Forms.Label();
            this.txt_tolerance = new System.Windows.Forms.TextBox();
            this.txt_speed = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gb_Allocation_Step = new System.Windows.Forms.GroupBox();
            this.txt_AllocationStep = new System.Windows.Forms.TextBox();
            this.lbl_Allocation_Step = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_FirstLevel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_SecondLevel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_ThirdLevel = new System.Windows.Forms.TextBox();
            this.gb_Distance.SuspendLayout();
            this.gb_Time.SuspendLayout();
            this.gb_Speed.SuspendLayout();
            this.gb_Allocation_Step.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(35, 292);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(302, 292);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // lbl_Unit
            // 
            this.lbl_Unit.AutoSize = true;
            this.lbl_Unit.Location = new System.Drawing.Point(11, 22);
            this.lbl_Unit.Name = "lbl_Unit";
            this.lbl_Unit.Size = new System.Drawing.Size(32, 13);
            this.lbl_Unit.TabIndex = 2;
            this.lbl_Unit.Text = "Unit :";
            // 
            // txt_DistanceUnit
            // 
            this.txt_DistanceUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_DistanceUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_DistanceUnit.Location = new System.Drawing.Point(49, 19);
            this.txt_DistanceUnit.Name = "txt_DistanceUnit";
            this.txt_DistanceUnit.Size = new System.Drawing.Size(120, 20);
            this.txt_DistanceUnit.TabIndex = 3;
            // 
            // lbl_Initial
            // 
            this.lbl_Initial.AutoSize = true;
            this.lbl_Initial.Location = new System.Drawing.Point(11, 52);
            this.lbl_Initial.Name = "lbl_Initial";
            this.lbl_Initial.Size = new System.Drawing.Size(37, 13);
            this.lbl_Initial.TabIndex = 4;
            this.lbl_Initial.Text = "Initial :";
            // 
            // txt_DistanceInitial
            // 
            this.txt_DistanceInitial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_DistanceInitial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_DistanceInitial.Location = new System.Drawing.Point(130, 49);
            this.txt_DistanceInitial.Name = "txt_DistanceInitial";
            this.txt_DistanceInitial.Size = new System.Drawing.Size(39, 20);
            this.txt_DistanceInitial.TabIndex = 5;
            this.txt_DistanceInitial.TextChanged += new System.EventHandler(this.txt_DistanceInitial_TextChanged);
            // 
            // gb_Distance
            // 
            this.gb_Distance.BackColor = System.Drawing.Color.Transparent;
            this.gb_Distance.Controls.Add(this.txt_DistanceUnit);
            this.gb_Distance.Controls.Add(this.lbl_Unit);
            this.gb_Distance.Controls.Add(this.lbl_Initial);
            this.gb_Distance.Controls.Add(this.txt_DistanceInitial);
            this.gb_Distance.Location = new System.Drawing.Point(10, 55);
            this.gb_Distance.Name = "gb_Distance";
            this.gb_Distance.Size = new System.Drawing.Size(177, 77);
            this.gb_Distance.TabIndex = 10;
            this.gb_Distance.TabStop = false;
            this.gb_Distance.Text = "Distance";
            // 
            // gb_Time
            // 
            this.gb_Time.BackColor = System.Drawing.Color.Transparent;
            this.gb_Time.Controls.Add(this.cb_TimeUnit);
            this.gb_Time.Controls.Add(this.label1);
            this.gb_Time.Controls.Add(this.label2);
            this.gb_Time.Controls.Add(this.txt_TimeInitial);
            this.gb_Time.Location = new System.Drawing.Point(201, 55);
            this.gb_Time.Name = "gb_Time";
            this.gb_Time.Size = new System.Drawing.Size(177, 77);
            this.gb_Time.TabIndex = 11;
            this.gb_Time.TabStop = false;
            this.gb_Time.Text = "Time";
            // 
            // cb_TimeUnit
            // 
            this.cb_TimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TimeUnit.FormattingEnabled = true;
            this.cb_TimeUnit.Location = new System.Drawing.Point(72, 16);
            this.cb_TimeUnit.Name = "cb_TimeUnit";
            this.cb_TimeUnit.Size = new System.Drawing.Size(93, 21);
            this.cb_TimeUnit.TabIndex = 6;
            this.cb_TimeUnit.SelectedIndexChanged += new System.EventHandler(this.cb_TimeUnit_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Unit :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Initial :";
            // 
            // txt_TimeInitial
            // 
            this.txt_TimeInitial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_TimeInitial.Enabled = false;
            this.txt_TimeInitial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_TimeInitial.Location = new System.Drawing.Point(130, 49);
            this.txt_TimeInitial.Name = "txt_TimeInitial";
            this.txt_TimeInitial.Size = new System.Drawing.Size(39, 20);
            this.txt_TimeInitial.TabIndex = 5;
            this.txt_TimeInitial.TextChanged += new System.EventHandler(this.txt_DistanceInitial_TextChanged);
            // 
            // gb_Speed
            // 
            this.gb_Speed.BackColor = System.Drawing.Color.Transparent;
            this.gb_Speed.Controls.Add(this.lbl_Tolerance);
            this.gb_Speed.Controls.Add(this.label5);
            this.gb_Speed.Controls.Add(this.lbl_Speed);
            this.gb_Speed.Controls.Add(this.gb_Time);
            this.gb_Speed.Controls.Add(this.txt_tolerance);
            this.gb_Speed.Controls.Add(this.gb_Distance);
            this.gb_Speed.Controls.Add(this.txt_speed);
            this.gb_Speed.Controls.Add(this.label3);
            this.gb_Speed.Location = new System.Drawing.Point(25, 12);
            this.gb_Speed.Name = "gb_Speed";
            this.gb_Speed.Size = new System.Drawing.Size(392, 145);
            this.gb_Speed.TabIndex = 12;
            this.gb_Speed.TabStop = false;
            this.gb_Speed.Text = "Speed";
            // 
            // lbl_Tolerance
            // 
            this.lbl_Tolerance.AutoSize = true;
            this.lbl_Tolerance.Location = new System.Drawing.Point(198, 26);
            this.lbl_Tolerance.Name = "lbl_Tolerance";
            this.lbl_Tolerance.Size = new System.Drawing.Size(55, 13);
            this.lbl_Tolerance.TabIndex = 5;
            this.lbl_Tolerance.Text = "Tolerance";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "%";
            // 
            // lbl_Speed
            // 
            this.lbl_Speed.AutoSize = true;
            this.lbl_Speed.Location = new System.Drawing.Point(148, 26);
            this.lbl_Speed.Name = "lbl_Speed";
            this.lbl_Speed.Size = new System.Drawing.Size(0, 13);
            this.lbl_Speed.TabIndex = 3;
            // 
            // txt_tolerance
            // 
            this.txt_tolerance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_tolerance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_tolerance.Location = new System.Drawing.Point(259, 23);
            this.txt_tolerance.Name = "txt_tolerance";
            this.txt_tolerance.Size = new System.Drawing.Size(32, 20);
            this.txt_tolerance.TabIndex = 2;
            // 
            // txt_speed
            // 
            this.txt_speed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_speed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_speed.Location = new System.Drawing.Point(106, 23);
            this.txt_speed.Name = "txt_speed";
            this.txt_speed.Size = new System.Drawing.Size(36, 20);
            this.txt_speed.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Passenger speed";
            // 
            // gb_Allocation_Step
            // 
            this.gb_Allocation_Step.BackColor = System.Drawing.Color.Transparent;
            this.gb_Allocation_Step.Controls.Add(this.txt_AllocationStep);
            this.gb_Allocation_Step.Controls.Add(this.lbl_Allocation_Step);
            this.gb_Allocation_Step.Location = new System.Drawing.Point(221, 175);
            this.gb_Allocation_Step.Name = "gb_Allocation_Step";
            this.gb_Allocation_Step.Size = new System.Drawing.Size(177, 53);
            this.gb_Allocation_Step.TabIndex = 13;
            this.gb_Allocation_Step.TabStop = false;
            this.gb_Allocation_Step.Text = "Allocation specification (min)";
            // 
            // txt_AllocationStep
            // 
            this.txt_AllocationStep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_AllocationStep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_AllocationStep.Location = new System.Drawing.Point(93, 19);
            this.txt_AllocationStep.Name = "txt_AllocationStep";
            this.txt_AllocationStep.Size = new System.Drawing.Size(36, 20);
            this.txt_AllocationStep.TabIndex = 3;
            // 
            // lbl_Allocation_Step
            // 
            this.lbl_Allocation_Step.AutoSize = true;
            this.lbl_Allocation_Step.Location = new System.Drawing.Point(11, 22);
            this.lbl_Allocation_Step.Name = "lbl_Allocation_Step";
            this.lbl_Allocation_Step.Size = new System.Drawing.Size(76, 13);
            this.lbl_Allocation_Step.TabIndex = 2;
            this.lbl_Allocation_Step.Text = "Allocation step";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_FirstLevel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_SecondLevel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_ThirdLevel);
            this.groupBox1.Location = new System.Drawing.Point(25, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 107);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Intermediate distribution levels (%)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Third";
            // 
            // txt_FirstLevel
            // 
            this.txt_FirstLevel.Location = new System.Drawing.Point(97, 22);
            this.txt_FirstLevel.Name = "txt_FirstLevel";
            this.txt_FirstLevel.Size = new System.Drawing.Size(45, 20);
            this.txt_FirstLevel.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Second ";
            // 
            // txt_SecondLevel
            // 
            this.txt_SecondLevel.Location = new System.Drawing.Point(97, 49);
            this.txt_SecondLevel.Name = "txt_SecondLevel";
            this.txt_SecondLevel.Size = new System.Drawing.Size(45, 20);
            this.txt_SecondLevel.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "First";
            // 
            // txt_ThirdLevel
            // 
            this.txt_ThirdLevel.Location = new System.Drawing.Point(97, 75);
            this.txt_ThirdLevel.Name = "txt_ThirdLevel";
            this.txt_ThirdLevel.Size = new System.Drawing.Size(45, 20);
            this.txt_ThirdLevel.TabIndex = 16;
            // 
            // Speed_Settings
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(451, 330);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_Allocation_Step);
            this.Controls.Add(this.gb_Speed);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Speed_Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Properties";
            this.gb_Distance.ResumeLayout(false);
            this.gb_Distance.PerformLayout();
            this.gb_Time.ResumeLayout(false);
            this.gb_Time.PerformLayout();
            this.gb_Speed.ResumeLayout(false);
            this.gb_Speed.PerformLayout();
            this.gb_Allocation_Step.ResumeLayout(false);
            this.gb_Allocation_Step.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label lbl_Unit;
        private System.Windows.Forms.TextBox txt_DistanceUnit;
        private System.Windows.Forms.Label lbl_Initial;
        private System.Windows.Forms.TextBox txt_DistanceInitial;
        private System.Windows.Forms.GroupBox gb_Distance;
        private System.Windows.Forms.GroupBox gb_Time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_TimeInitial;
        private System.Windows.Forms.GroupBox gb_Speed;
        private System.Windows.Forms.Label lbl_Tolerance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_Speed;
        private System.Windows.Forms.TextBox txt_tolerance;
        private System.Windows.Forms.TextBox txt_speed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_TimeUnit;
        private System.Windows.Forms.GroupBox gb_Allocation_Step;
        private System.Windows.Forms.TextBox txt_AllocationStep;
        private System.Windows.Forms.Label lbl_Allocation_Step;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_FirstLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_SecondLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_ThirdLevel;
    }
}