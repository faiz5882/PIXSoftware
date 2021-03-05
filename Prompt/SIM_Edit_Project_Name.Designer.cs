namespace SIMCORE_TOOL
{
    partial class SIM_Edit_Project_Name
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Edit_Project_Name));
            this.lbl_pn = new System.Windows.Forms.Label();
            this.txt_projectName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gb_FlightPlanAlloccation = new System.Windows.Forms.GroupBox();
            this.rb_UseDescription = new System.Windows.Forms.RadioButton();
            this.rb_UseIndexes = new System.Windows.Forms.RadioButton();
            this.gb_FlightPlanAlloccation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_pn
            // 
            this.lbl_pn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_pn.BackColor = System.Drawing.Color.Transparent;
            this.lbl_pn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pn.Location = new System.Drawing.Point(59, 18);
            this.lbl_pn.Name = "lbl_pn";
            this.lbl_pn.Size = new System.Drawing.Size(274, 24);
            this.lbl_pn.TabIndex = 0;
            this.lbl_pn.Text = "Project name :";
            this.lbl_pn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_projectName
            // 
            this.txt_projectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_projectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_projectName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_projectName.Location = new System.Drawing.Point(22, 54);
            this.txt_projectName.Name = "txt_projectName";
            this.txt_projectName.Size = new System.Drawing.Size(349, 22);
            this.txt_projectName.TabIndex = 1;
            this.txt_projectName.TextChanged += new System.EventHandler(this.txt_projectName_TextChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(22, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(289, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 22);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // gb_FlightPlanAlloccation
            // 
            this.gb_FlightPlanAlloccation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_FlightPlanAlloccation.BackColor = System.Drawing.Color.Transparent;
            this.gb_FlightPlanAlloccation.Controls.Add(this.rb_UseIndexes);
            this.gb_FlightPlanAlloccation.Controls.Add(this.rb_UseDescription);
            this.gb_FlightPlanAlloccation.Location = new System.Drawing.Point(31, 93);
            this.gb_FlightPlanAlloccation.Name = "gb_FlightPlanAlloccation";
            this.gb_FlightPlanAlloccation.Size = new System.Drawing.Size(330, 69);
            this.gb_FlightPlanAlloccation.TabIndex = 4;
            this.gb_FlightPlanAlloccation.TabStop = false;
            this.gb_FlightPlanAlloccation.Text = "Flight Plan Allocation Policy";
            // 
            // rb_UseDescription
            // 
            this.rb_UseDescription.AutoSize = true;
            this.rb_UseDescription.Location = new System.Drawing.Point(6, 42);
            this.rb_UseDescription.Name = "rb_UseDescription";
            this.rb_UseDescription.Size = new System.Drawing.Size(312, 17);
            this.rb_UseDescription.TabIndex = 0;
            this.rb_UseDescription.TabStop = true;
            this.rb_UseDescription.Text = "Use airport objects descriptions prefix for FlightPlan allocation";
            this.rb_UseDescription.UseVisualStyleBackColor = true;
            this.rb_UseDescription.CheckedChanged += new System.EventHandler(this.rb_UseDescription_CheckedChanged);
            // 
            // rb_UseIndexes
            // 
            this.rb_UseIndexes.AutoSize = true;
            this.rb_UseIndexes.Location = new System.Drawing.Point(6, 19);
            this.rb_UseIndexes.Name = "rb_UseIndexes";
            this.rb_UseIndexes.Size = new System.Drawing.Size(264, 17);
            this.rb_UseIndexes.TabIndex = 1;
            this.rb_UseIndexes.TabStop = true;
            this.rb_UseIndexes.Text = "Use airport objects indexes for FlightPlan allocation";
            this.rb_UseIndexes.UseVisualStyleBackColor = true;
            // 
            // SIM_Edit_Project_Name
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(393, 217);
            this.Controls.Add(this.gb_FlightPlanAlloccation);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_projectName);
            this.Controls.Add(this.lbl_pn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(401, 251);
            this.Name = "SIM_Edit_Project_Name";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project name";
            this.gb_FlightPlanAlloccation.ResumeLayout(false);
            this.gb_FlightPlanAlloccation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_pn;
        private System.Windows.Forms.TextBox txt_projectName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox gb_FlightPlanAlloccation;
        private System.Windows.Forms.RadioButton rb_UseIndexes;
        private System.Windows.Forms.RadioButton rb_UseDescription;
    }
}