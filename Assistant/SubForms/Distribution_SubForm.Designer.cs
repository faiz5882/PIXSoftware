namespace SIMCORE_TOOL.Assistant.SubForms
{
    partial class Distribution_SubForm
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
            this.txt_ThirdParam = new System.Windows.Forms.TextBox();
            this.cb_DistributionType = new System.Windows.Forms.ComboBox();
            this.txt_FirstParam = new System.Windows.Forms.TextBox();
            this.txt_SecondParam = new System.Windows.Forms.TextBox();
            this.lbl_Moving = new System.Windows.Forms.Label();
            this.btn_Determine = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cb_OneOf = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_ThirdParam
            // 
            this.txt_ThirdParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ThirdParam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_ThirdParam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_ThirdParam.Location = new System.Drawing.Point(212, 27);
            this.txt_ThirdParam.Name = "txt_ThirdParam";
            this.txt_ThirdParam.Size = new System.Drawing.Size(41, 20);
            this.txt_ThirdParam.TabIndex = 4;
            this.txt_ThirdParam.TextChanged += new System.EventHandler(this.txt_FirstParam_TextChanged);
            this.txt_ThirdParam.EnabledChanged += new System.EventHandler(this.txt_FirstParam_EnabledChanged);
            // 
            // cb_DistributionType
            // 
            this.cb_DistributionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_DistributionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_DistributionType.FormattingEnabled = true;
            this.cb_DistributionType.Items.AddRange(new object[] {
            "Constant",
            "Uniform",
            "Normal",
            "Triangular",
            "Exponential",
            "Gamma",
            "LogNormal",
            "Weibull",
            "Probability profiles"});
            this.cb_DistributionType.Location = new System.Drawing.Point(0, 27);
            this.cb_DistributionType.Name = "cb_DistributionType";
            this.cb_DistributionType.Size = new System.Drawing.Size(111, 21);
            this.cb_DistributionType.TabIndex = 1;
            this.cb_DistributionType.SelectedIndexChanged += new System.EventHandler(this.cb_DistributionType_SelectedIndexChanged);
            // 
            // txt_FirstParam
            // 
            this.txt_FirstParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_FirstParam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_FirstParam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_FirstParam.Location = new System.Drawing.Point(118, 28);
            this.txt_FirstParam.Name = "txt_FirstParam";
            this.txt_FirstParam.Size = new System.Drawing.Size(41, 20);
            this.txt_FirstParam.TabIndex = 2;
            this.txt_FirstParam.TextChanged += new System.EventHandler(this.txt_FirstParam_TextChanged);
            this.txt_FirstParam.EnabledChanged += new System.EventHandler(this.txt_FirstParam_EnabledChanged);
            // 
            // txt_SecondParam
            // 
            this.txt_SecondParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_SecondParam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_SecondParam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_SecondParam.Location = new System.Drawing.Point(165, 28);
            this.txt_SecondParam.Name = "txt_SecondParam";
            this.txt_SecondParam.Size = new System.Drawing.Size(41, 20);
            this.txt_SecondParam.TabIndex = 3;
            this.txt_SecondParam.TextChanged += new System.EventHandler(this.txt_FirstParam_TextChanged);
            this.txt_SecondParam.EnabledChanged += new System.EventHandler(this.txt_FirstParam_EnabledChanged);
            // 
            // lbl_Moving
            // 
            this.lbl_Moving.AutoSize = true;
            this.lbl_Moving.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Moving.Location = new System.Drawing.Point(2, 5);
            this.lbl_Moving.Name = "lbl_Moving";
            this.lbl_Moving.Size = new System.Drawing.Size(121, 13);
            this.lbl_Moving.TabIndex = 5;
            this.lbl_Moving.Text = "Walk time distribution (s)";
            // 
            // btn_Determine
            // 
            this.btn_Determine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Determine.Location = new System.Drawing.Point(129, 0);
            this.btn_Determine.Name = "btn_Determine";
            this.btn_Determine.Size = new System.Drawing.Size(103, 23);
            this.btn_Determine.TabIndex = 6;
            this.btn_Determine.Text = "Fit from data";
            this.btn_Determine.UseVisualStyleBackColor = true;
            this.btn_Determine.Visible = false;
            this.btn_Determine.Click += new System.EventHandler(this.btn_Determine_Click);
            // 
            // cb_OneOf
            // 
            this.cb_OneOf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_OneOf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OneOf.FormattingEnabled = true;
            this.cb_OneOf.Location = new System.Drawing.Point(117, 27);
            this.cb_OneOf.Name = "cb_OneOf";
            this.cb_OneOf.Size = new System.Drawing.Size(175, 21);
            this.cb_OneOf.TabIndex = 7;
            this.cb_OneOf.Visible = false;
            this.cb_OneOf.SelectedIndexChanged += new System.EventHandler(this.cb_OneOf_SelectedIndexChanged);
            this.cb_OneOf.Enter += new System.EventHandler(this.cb_OneOf_Enter);
            // 
            // Distribution_SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 51);
            this.Controls.Add(this.cb_OneOf);
            this.Controls.Add(this.btn_Determine);
            this.Controls.Add(this.lbl_Moving);
            this.Controls.Add(this.txt_ThirdParam);
            this.Controls.Add(this.cb_DistributionType);
            this.Controls.Add(this.txt_FirstParam);
            this.Controls.Add(this.txt_SecondParam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Distribution_SubForm";
            this.Text = "Distribution_SubForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ThirdParam;
        private System.Windows.Forms.ComboBox cb_DistributionType;
        private System.Windows.Forms.TextBox txt_FirstParam;
        private System.Windows.Forms.TextBox txt_SecondParam;
        private System.Windows.Forms.Label lbl_Moving;
        private System.Windows.Forms.Button btn_Determine;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cb_OneOf;
    }
}