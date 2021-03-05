namespace SIMCORE_TOOL.Prompt
{
    partial class Appearence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Appearence));
            this.lbl_theme = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_FirstColor = new System.Windows.Forms.Button();
            this.btn_SecondColor = new System.Windows.Forms.Button();
            this.lbl_first = new System.Windows.Forms.Label();
            this.gb_fond = new System.Windows.Forms.GroupBox();
            this.nud_Angle = new System.Windows.Forms.NumericUpDown();
            this.lbl_Angle = new System.Windows.Forms.Label();
            this.lbl_second = new System.Windows.Forms.Label();
            this.cd_ChooseColor = new System.Windows.Forms.ColorDialog();
            this.gb_fond.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Angle)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_theme
            // 
            this.lbl_theme.AutoSize = true;
            this.lbl_theme.BackColor = System.Drawing.Color.Transparent;
            this.lbl_theme.Location = new System.Drawing.Point(26, 29);
            this.lbl_theme.Name = "lbl_theme";
            this.lbl_theme.Size = new System.Drawing.Size(40, 13);
            this.lbl_theme.TabIndex = 1;
            this.lbl_theme.Text = "Theme";
            this.lbl_theme.Visible = false;
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(85, 191);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(106, 27);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "&Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_FirstColor
            // 
            this.btn_FirstColor.Location = new System.Drawing.Point(170, 24);
            this.btn_FirstColor.Name = "btn_FirstColor";
            this.btn_FirstColor.Size = new System.Drawing.Size(25, 25);
            this.btn_FirstColor.TabIndex = 3;
            this.btn_FirstColor.UseVisualStyleBackColor = true;
            this.btn_FirstColor.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // btn_SecondColor
            // 
            this.btn_SecondColor.Location = new System.Drawing.Point(170, 49);
            this.btn_SecondColor.Name = "btn_SecondColor";
            this.btn_SecondColor.Size = new System.Drawing.Size(25, 25);
            this.btn_SecondColor.TabIndex = 4;
            this.btn_SecondColor.UseVisualStyleBackColor = true;
            this.btn_SecondColor.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // lbl_first
            // 
            this.lbl_first.AutoSize = true;
            this.lbl_first.BackColor = System.Drawing.Color.Transparent;
            this.lbl_first.Location = new System.Drawing.Point(19, 30);
            this.lbl_first.Name = "lbl_first";
            this.lbl_first.Size = new System.Drawing.Size(52, 13);
            this.lbl_first.TabIndex = 5;
            this.lbl_first.Text = "First color";
            // 
            // gb_fond
            // 
            this.gb_fond.BackColor = System.Drawing.Color.Transparent;
            this.gb_fond.Controls.Add(this.nud_Angle);
            this.gb_fond.Controls.Add(this.lbl_Angle);
            this.gb_fond.Controls.Add(this.lbl_second);
            this.gb_fond.Controls.Add(this.lbl_first);
            this.gb_fond.Controls.Add(this.btn_SecondColor);
            this.gb_fond.Controls.Add(this.btn_FirstColor);
            this.gb_fond.Location = new System.Drawing.Point(12, 29);
            this.gb_fond.Name = "gb_fond";
            this.gb_fond.Size = new System.Drawing.Size(246, 138);
            this.gb_fond.TabIndex = 6;
            this.gb_fond.TabStop = false;
            this.gb_fond.Text = "Backgroud";
            // 
            // nud_Angle
            // 
            this.nud_Angle.Location = new System.Drawing.Point(148, 84);
            this.nud_Angle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nud_Angle.Name = "nud_Angle";
            this.nud_Angle.Size = new System.Drawing.Size(47, 20);
            this.nud_Angle.TabIndex = 8;
            this.nud_Angle.ValueChanged += new System.EventHandler(this.nud_Angle_ValueChanged);
            // 
            // lbl_Angle
            // 
            this.lbl_Angle.AutoSize = true;
            this.lbl_Angle.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Angle.Location = new System.Drawing.Point(22, 91);
            this.lbl_Angle.Name = "lbl_Angle";
            this.lbl_Angle.Size = new System.Drawing.Size(34, 13);
            this.lbl_Angle.TabIndex = 7;
            this.lbl_Angle.Text = "Angle";
            // 
            // lbl_second
            // 
            this.lbl_second.AutoSize = true;
            this.lbl_second.BackColor = System.Drawing.Color.Transparent;
            this.lbl_second.Location = new System.Drawing.Point(19, 56);
            this.lbl_second.Name = "lbl_second";
            this.lbl_second.Size = new System.Drawing.Size(70, 13);
            this.lbl_second.TabIndex = 6;
            this.lbl_second.Text = "Second color";
            // 
            // Appearence
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ok;
            this.ClientSize = new System.Drawing.Size(294, 238);
            this.Controls.Add(this.gb_fond);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lbl_theme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 270);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 270);
            this.Name = "Appearence";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Appearence";
            this.gb_fond.ResumeLayout(false);
            this.gb_fond.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Angle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_theme;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_FirstColor;
        private System.Windows.Forms.Button btn_SecondColor;
        private System.Windows.Forms.Label lbl_first;
        private System.Windows.Forms.GroupBox gb_fond;
        private System.Windows.Forms.Label lbl_second;
        private System.Windows.Forms.Label lbl_Angle;
        private System.Windows.Forms.NumericUpDown nud_Angle;
        private System.Windows.Forms.ColorDialog cd_ChooseColor;
    }
}