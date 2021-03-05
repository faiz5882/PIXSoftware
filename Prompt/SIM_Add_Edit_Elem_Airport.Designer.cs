namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Add_Edit_Elem_Airport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Add_Edit_Elem_Airport));
            this.BT_OK = new System.Windows.Forms.Button();
            this.TXT_To = new System.Windows.Forms.MaskedTextBox();
            this.TXT_From = new System.Windows.Forms.MaskedTextBox();
            this.CB_GroupDesc = new System.Windows.Forms.ComboBox();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.L_A0 = new System.Windows.Forms.Label();
            this.CB_ObjectType = new System.Windows.Forms.ComboBox();
            this.TXT_ItemDesc = new System.Windows.Forms.TextBox();
            this.L_A5 = new System.Windows.Forms.Label();
            this.L_A4 = new System.Windows.Forms.Label();
            this.CHK_Multiple = new System.Windows.Forms.CheckBox();
            this.L_A3 = new System.Windows.Forms.Label();
            this.L_A2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BT_OK
            // 
            this.BT_OK.BackColor = System.Drawing.Color.Transparent;
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Location = new System.Drawing.Point(50, 130);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(75, 23);
            this.BT_OK.TabIndex = 7;
            this.BT_OK.Text = "OK";
            this.BT_OK.UseVisualStyleBackColor = false;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // TXT_To
            // 
            this.TXT_To.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.TXT_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TXT_To.Location = new System.Drawing.Point(245, 96);
            this.TXT_To.Name = "TXT_To";
            this.TXT_To.PromptChar = ' ';
            this.TXT_To.Size = new System.Drawing.Size(41, 20);
            this.TXT_To.TabIndex = 6;
            this.TXT_To.Text = "1";
            this.TXT_To.ValidatingType = typeof(int);
            this.TXT_To.Visible = false;
            this.TXT_To.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT_From_KeyPress);
            this.TXT_To.TextChanged += new System.EventHandler(this.TXT_To_TextChanged);
            // 
            // TXT_From
            // 
            this.TXT_From.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.TXT_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TXT_From.Location = new System.Drawing.Point(147, 96);
            this.TXT_From.Name = "TXT_From";
            this.TXT_From.PromptChar = ' ';
            this.TXT_From.Size = new System.Drawing.Size(39, 20);
            this.TXT_From.TabIndex = 5;
            this.TXT_From.Text = "1";
            this.TXT_From.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT_From_KeyPress);
            this.TXT_From.TextChanged += new System.EventHandler(this.TXT_From_TextChanged);
            // 
            // CB_GroupDesc
            // 
            this.CB_GroupDesc.FormattingEnabled = true;
            this.CB_GroupDesc.Location = new System.Drawing.Point(147, 47);
            this.CB_GroupDesc.Name = "CB_GroupDesc";
            this.CB_GroupDesc.Size = new System.Drawing.Size(139, 21);
            this.CB_GroupDesc.TabIndex = 2;
            this.CB_GroupDesc.SelectedIndexChanged += new System.EventHandler(this.CB_GroupDesc_SelectedIndexChanged);
            this.CB_GroupDesc.TextChanged += new System.EventHandler(this.CB_GroupDesc_TextChanged);
            // 
            // BT_CANCEL
            // 
            this.BT_CANCEL.BackColor = System.Drawing.Color.Transparent;
            this.BT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_CANCEL.Location = new System.Drawing.Point(158, 130);
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.BT_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BT_CANCEL.TabIndex = 8;
            this.BT_CANCEL.Text = "Cancel";
            this.BT_CANCEL.UseVisualStyleBackColor = false;
            // 
            // L_A0
            // 
            this.L_A0.AutoSize = true;
            this.L_A0.BackColor = System.Drawing.Color.Transparent;
            this.L_A0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_A0.Location = new System.Drawing.Point(24, 27);
            this.L_A0.Name = "L_A0";
            this.L_A0.Size = new System.Drawing.Size(76, 13);
            this.L_A0.TabIndex = 31;
            this.L_A0.Text = "Object Type";
            // 
            // CB_ObjectType
            // 
            this.CB_ObjectType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CB_ObjectType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_ObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ObjectType.FormattingEnabled = true;
            this.CB_ObjectType.Location = new System.Drawing.Point(147, 24);
            this.CB_ObjectType.MaxDropDownItems = 10;
            this.CB_ObjectType.Name = "CB_ObjectType";
            this.CB_ObjectType.Size = new System.Drawing.Size(139, 21);
            this.CB_ObjectType.TabIndex = 1;
            this.CB_ObjectType.SelectedIndexChanged += new System.EventHandler(this.CB_ObjectType_SelectedIndexChanged);
            // 
            // TXT_ItemDesc
            // 
            this.TXT_ItemDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.TXT_ItemDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TXT_ItemDesc.Location = new System.Drawing.Point(147, 70);
            this.TXT_ItemDesc.Name = "TXT_ItemDesc";
            this.TXT_ItemDesc.Size = new System.Drawing.Size(139, 20);
            this.TXT_ItemDesc.TabIndex = 3;
            // 
            // L_A5
            // 
            this.L_A5.AutoSize = true;
            this.L_A5.BackColor = System.Drawing.Color.Transparent;
            this.L_A5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_A5.Location = new System.Drawing.Point(208, 99);
            this.L_A5.Name = "L_A5";
            this.L_A5.Size = new System.Drawing.Size(22, 13);
            this.L_A5.TabIndex = 28;
            this.L_A5.Text = "To";
            this.L_A5.Visible = false;
            // 
            // L_A4
            // 
            this.L_A4.AutoSize = true;
            this.L_A4.BackColor = System.Drawing.Color.Transparent;
            this.L_A4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_A4.Location = new System.Drawing.Point(95, 99);
            this.L_A4.Name = "L_A4";
            this.L_A4.Size = new System.Drawing.Size(34, 13);
            this.L_A4.TabIndex = 27;
            this.L_A4.Text = "From";
            // 
            // CHK_Multiple
            // 
            this.CHK_Multiple.AutoSize = true;
            this.CHK_Multiple.BackColor = System.Drawing.Color.Transparent;
            this.CHK_Multiple.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_Multiple.Location = new System.Drawing.Point(27, 98);
            this.CHK_Multiple.Name = "CHK_Multiple";
            this.CHK_Multiple.Size = new System.Drawing.Size(70, 17);
            this.CHK_Multiple.TabIndex = 4;
            this.CHK_Multiple.Text = "Multiple";
            this.CHK_Multiple.UseVisualStyleBackColor = false;
            this.CHK_Multiple.CheckedChanged += new System.EventHandler(this.CHK_Multiple_CheckedChanged);
            // 
            // L_A3
            // 
            this.L_A3.AutoSize = true;
            this.L_A3.BackColor = System.Drawing.Color.Transparent;
            this.L_A3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_A3.Location = new System.Drawing.Point(24, 73);
            this.L_A3.Name = "L_A3";
            this.L_A3.Size = new System.Drawing.Size(113, 13);
            this.L_A3.TabIndex = 25;
            this.L_A3.Text = "Item(s) Description";
            // 
            // L_A2
            // 
            this.L_A2.AutoSize = true;
            this.L_A2.BackColor = System.Drawing.Color.Transparent;
            this.L_A2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_A2.Location = new System.Drawing.Point(24, 50);
            this.L_A2.Name = "L_A2";
            this.L_A2.Size = new System.Drawing.Size(109, 13);
            this.L_A2.TabIndex = 24;
            this.L_A2.Text = "Group Description";
            // 
            // SIM_Add_Edit_Elem_Airport
            // 
            this.AcceptButton = this.BT_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.CancelButton = this.BT_CANCEL;
            this.ClientSize = new System.Drawing.Size(316, 163);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.TXT_To);
            this.Controls.Add(this.TXT_From);
            this.Controls.Add(this.CB_GroupDesc);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.L_A0);
            this.Controls.Add(this.CB_ObjectType);
            this.Controls.Add(this.TXT_ItemDesc);
            this.Controls.Add(this.L_A5);
            this.Controls.Add(this.L_A4);
            this.Controls.Add(this.CHK_Multiple);
            this.Controls.Add(this.L_A3);
            this.Controls.Add(this.L_A2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SIM_Add_Edit_Elem_Airport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add some objects";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.MaskedTextBox TXT_To;
        private System.Windows.Forms.MaskedTextBox TXT_From;
        private System.Windows.Forms.ComboBox CB_GroupDesc;
        private System.Windows.Forms.Button BT_CANCEL;
        private System.Windows.Forms.Label L_A0;
        private System.Windows.Forms.ComboBox CB_ObjectType;
        private System.Windows.Forms.TextBox TXT_ItemDesc;
        private System.Windows.Forms.Label L_A5;
        private System.Windows.Forms.Label L_A4;
        private System.Windows.Forms.CheckBox CHK_Multiple;
        private System.Windows.Forms.Label L_A3;
        private System.Windows.Forms.Label L_A2;
    }
}