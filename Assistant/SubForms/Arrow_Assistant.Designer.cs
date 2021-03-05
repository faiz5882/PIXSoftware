namespace SIMCORE_TOOL.Assistant
{
    partial class Arrow_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Arrow_Assistant));
            this.txt_Distance = new System.Windows.Forms.TextBox();
            this.txt_GoalGroupe = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Weight = new System.Windows.Forms.Label();
            this.txt_Weight = new System.Windows.Forms.TextBox();
            this.gb_Link = new System.Windows.Forms.GroupBox();
            this.p_Distribution = new System.Windows.Forms.Panel();
            this.chk_Distance = new System.Windows.Forms.CheckBox();
            this.btn_del = new System.Windows.Forms.Button();
            this.cb_Suceed = new System.Windows.Forms.CheckBox();
            this.tt_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lbl_Description = new System.Windows.Forms.Label();
            this.gb_Link.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Distance
            // 
            this.txt_Distance.Enabled = false;
            this.txt_Distance.Location = new System.Drawing.Point(96, 16);
            this.txt_Distance.Name = "txt_Distance";
            this.txt_Distance.Size = new System.Drawing.Size(113, 20);
            this.txt_Distance.TabIndex = 1;
            this.txt_Distance.TextChanged += new System.EventHandler(this.txt_Distance_TextChanged);
            this.txt_Distance.EnabledChanged += new System.EventHandler(this.txt_Distance_EnabledChanged);
            // 
            // txt_GoalGroupe
            // 
            this.txt_GoalGroupe.AutoSize = true;
            this.txt_GoalGroupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_GoalGroupe.Location = new System.Drawing.Point(12, 4);
            this.txt_GoalGroupe.Name = "txt_GoalGroupe";
            this.txt_GoalGroupe.Size = new System.Drawing.Size(36, 13);
            this.txt_GoalGroupe.TabIndex = 5;
            this.txt_GoalGroupe.Text = "T1L1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(-3, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "_________________________________________________";
            // 
            // lbl_Weight
            // 
            this.lbl_Weight.AutoSize = true;
            this.lbl_Weight.Location = new System.Drawing.Point(4, 50);
            this.lbl_Weight.Name = "lbl_Weight";
            this.lbl_Weight.Size = new System.Drawing.Size(55, 13);
            this.lbl_Weight.TabIndex = 7;
            this.lbl_Weight.Text = "Weight(%)";
            this.lbl_Weight.Visible = false;
            // 
            // txt_Weight
            // 
            this.txt_Weight.Location = new System.Drawing.Point(63, 47);
            this.txt_Weight.Name = "txt_Weight";
            this.txt_Weight.Size = new System.Drawing.Size(100, 20);
            this.txt_Weight.TabIndex = 0;
            this.txt_Weight.Visible = false;
            this.txt_Weight.TextChanged += new System.EventHandler(this.txt_Weight_TextChanged);
            this.txt_Weight.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Weight_Validating);
            // 
            // gb_Link
            // 
            this.gb_Link.Controls.Add(this.p_Distribution);
            this.gb_Link.Controls.Add(this.chk_Distance);
            this.gb_Link.Controls.Add(this.txt_Distance);
            this.gb_Link.Location = new System.Drawing.Point(4, 71);
            this.gb_Link.Name = "gb_Link";
            this.gb_Link.Size = new System.Drawing.Size(247, 102);
            this.gb_Link.TabIndex = 2;
            this.gb_Link.TabStop = false;
            this.gb_Link.Text = "Walking time";
            // 
            // p_Distribution
            // 
            this.p_Distribution.Location = new System.Drawing.Point(6, 45);
            this.p_Distribution.Name = "p_Distribution";
            this.p_Distribution.Size = new System.Drawing.Size(238, 51);
            this.p_Distribution.TabIndex = 2;
            this.p_Distribution.Paint += new System.Windows.Forms.PaintEventHandler(this.p_Distribution_Paint);
            // 
            // chk_Distance
            // 
            this.chk_Distance.AutoSize = true;
            this.chk_Distance.Location = new System.Drawing.Point(7, 19);
            this.chk_Distance.Name = "chk_Distance";
            this.chk_Distance.Size = new System.Drawing.Size(68, 17);
            this.chk_Distance.TabIndex = 0;
            this.chk_Distance.Text = "Distance";
            this.chk_Distance.UseVisualStyleBackColor = true;
            this.chk_Distance.Paint += new System.Windows.Forms.PaintEventHandler(this.chk_Distance_Paint);
            this.chk_Distance.CheckedChanged += new System.EventHandler(this.chk_Distance_CheckedChanged);
            // 
            // btn_del
            // 
            this.btn_del.Image = ((System.Drawing.Image)(resources.GetObject("btn_del.Image")));
            this.btn_del.Location = new System.Drawing.Point(226, 4);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(25, 21);
            this.btn_del.TabIndex = 3;
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // cb_Suceed
            // 
            this.cb_Suceed.AutoSize = true;
            this.cb_Suceed.Location = new System.Drawing.Point(11, 48);
            this.cb_Suceed.Name = "cb_Suceed";
            this.cb_Suceed.Size = new System.Drawing.Size(151, 17);
            this.cb_Suceed.TabIndex = 8;
            this.cb_Suceed.Text = "When process succeeded";
            this.cb_Suceed.UseVisualStyleBackColor = true;
            this.cb_Suceed.Visible = false;
            this.cb_Suceed.CheckedChanged += new System.EventHandler(this.cb_Suceed_CheckedChanged);
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Location = new System.Drawing.Point(22, 24);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(60, 13);
            this.lbl_Description.TabIndex = 9;
            this.lbl_Description.Text = "Description";
            // 
            // Arrow_Assistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ClientSize = new System.Drawing.Size(257, 180);
            this.Controls.Add(this.lbl_Description);
            this.Controls.Add(this.cb_Suceed);
            this.Controls.Add(this.gb_Link);
            this.Controls.Add(this.txt_Weight);
            this.Controls.Add(this.lbl_Weight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_GoalGroupe);
            this.Controls.Add(this.btn_del);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Arrow_Assistant";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Arrow_Assistant";
            this.gb_Link.ResumeLayout(false);
            this.gb_Link.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Distance;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Label txt_GoalGroupe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Weight;
        private System.Windows.Forms.TextBox txt_Weight;
        private System.Windows.Forms.GroupBox gb_Link;
        private System.Windows.Forms.CheckBox chk_Distance;
        private System.Windows.Forms.Panel p_Distribution;
        private System.Windows.Forms.CheckBox cb_Suceed;
        private System.Windows.Forms.ToolTip tt_ToolTip;
        private System.Windows.Forms.Label lbl_Description;

    }
}