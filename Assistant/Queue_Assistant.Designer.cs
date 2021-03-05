namespace SIMCORE_TOOL.Assistant
{
    partial class Queue_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Queue_Assistant));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cb_Group = new System.Windows.Forms.ComboBox();
            this.lbl_ObjectType = new System.Windows.Forms.Label();
            this.gb_Desk = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.chk_ApplyToAll = new System.Windows.Forms.CheckBox();
            this.lbl_Desk = new System.Windows.Forms.Label();
            this.lbl_DeskCapacity = new System.Windows.Forms.Label();
            this.cb_Desks = new System.Windows.Forms.ComboBox();
            this.txt_DeskCapacity = new System.Windows.Forms.TextBox();
            this.gb_Desk.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(25, 209);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(74, 24);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(158, 209);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(76, 24);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // cb_Group
            // 
            this.cb_Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Group.FormattingEnabled = true;
            this.cb_Group.Location = new System.Drawing.Point(107, 12);
            this.cb_Group.Name = "cb_Group";
            this.cb_Group.Size = new System.Drawing.Size(135, 21);
            this.cb_Group.TabIndex = 2;
            this.cb_Group.SelectedIndexChanged += new System.EventHandler(this.cb_Group_SelectedIndexChanged);
            // 
            // lbl_ObjectType
            // 
            this.lbl_ObjectType.AutoSize = true;
            this.lbl_ObjectType.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ObjectType.Location = new System.Drawing.Point(26, 15);
            this.lbl_ObjectType.Name = "lbl_ObjectType";
            this.lbl_ObjectType.Size = new System.Drawing.Size(61, 13);
            this.lbl_ObjectType.TabIndex = 5;
            this.lbl_ObjectType.Text = "Object type";
            // 
            // gb_Desk
            // 
            this.gb_Desk.BackColor = System.Drawing.Color.Transparent;
            this.gb_Desk.Controls.Add(this.label1);
            this.gb_Desk.Controls.Add(this.btn_Submit);
            this.gb_Desk.Controls.Add(this.chk_ApplyToAll);
            this.gb_Desk.Controls.Add(this.lbl_Desk);
            this.gb_Desk.Controls.Add(this.lbl_DeskCapacity);
            this.gb_Desk.Controls.Add(this.cb_Desks);
            this.gb_Desk.Controls.Add(this.txt_DeskCapacity);
            this.gb_Desk.Location = new System.Drawing.Point(22, 50);
            this.gb_Desk.Name = "gb_Desk";
            this.gb_Desk.Size = new System.Drawing.Size(220, 141);
            this.gb_Desk.TabIndex = 10;
            this.gb_Desk.TabStop = false;
            this.gb_Desk.Text = "Desk";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "(-1 = infinite)";
            // 
            // btn_Submit
            // 
            this.btn_Submit.Enabled = false;
            this.btn_Submit.Location = new System.Drawing.Point(37, 113);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(127, 22);
            this.btn_Submit.TabIndex = 12;
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // chk_ApplyToAll
            // 
            this.chk_ApplyToAll.AutoSize = true;
            this.chk_ApplyToAll.Location = new System.Drawing.Point(37, 19);
            this.chk_ApplyToAll.Name = "chk_ApplyToAll";
            this.chk_ApplyToAll.Size = new System.Drawing.Size(151, 17);
            this.chk_ApplyToAll.TabIndex = 11;
            this.chk_ApplyToAll.Text = "Apply capacity to all desks";
            this.chk_ApplyToAll.UseVisualStyleBackColor = true;
            this.chk_ApplyToAll.CheckedChanged += new System.EventHandler(this.chk_ApplyToAll_CheckedChanged);
            // 
            // lbl_Desk
            // 
            this.lbl_Desk.AutoSize = true;
            this.lbl_Desk.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Desk.Location = new System.Drawing.Point(6, 45);
            this.lbl_Desk.Name = "lbl_Desk";
            this.lbl_Desk.Size = new System.Drawing.Size(37, 13);
            this.lbl_Desk.TabIndex = 8;
            this.lbl_Desk.Text = "Desks";
            // 
            // lbl_DeskCapacity
            // 
            this.lbl_DeskCapacity.AutoSize = true;
            this.lbl_DeskCapacity.Location = new System.Drawing.Point(6, 77);
            this.lbl_DeskCapacity.Name = "lbl_DeskCapacity";
            this.lbl_DeskCapacity.Size = new System.Drawing.Size(75, 13);
            this.lbl_DeskCapacity.TabIndex = 10;
            this.lbl_DeskCapacity.Text = "Desk capacity";
            // 
            // cb_Desks
            // 
            this.cb_Desks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Desks.FormattingEnabled = true;
            this.cb_Desks.Location = new System.Drawing.Point(49, 42);
            this.cb_Desks.Name = "cb_Desks";
            this.cb_Desks.Size = new System.Drawing.Size(154, 21);
            this.cb_Desks.TabIndex = 7;
            this.cb_Desks.SelectedIndexChanged += new System.EventHandler(this.cb_Desks_SelectedIndexChanged);
            // 
            // txt_DeskCapacity
            // 
            this.txt_DeskCapacity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_DeskCapacity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_DeskCapacity.Location = new System.Drawing.Point(85, 74);
            this.txt_DeskCapacity.Name = "txt_DeskCapacity";
            this.txt_DeskCapacity.Size = new System.Drawing.Size(118, 20);
            this.txt_DeskCapacity.TabIndex = 9;
            this.txt_DeskCapacity.TextChanged += new System.EventHandler(this.txt_DeskCapacity_TextChanged);
            // 
            // Queue_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(257, 245);
            this.Controls.Add(this.gb_Desk);
            this.Controls.Add(this.lbl_ObjectType);
            this.Controls.Add(this.cb_Group);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Queue_Assistant";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Queue capacities";
            this.gb_Desk.ResumeLayout(false);
            this.gb_Desk.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cb_Group;
        private System.Windows.Forms.Label lbl_ObjectType;
        private System.Windows.Forms.ComboBox cb_Desks;
        private System.Windows.Forms.Label lbl_Desk;
        private System.Windows.Forms.GroupBox gb_Desk;
        private System.Windows.Forms.TextBox txt_DeskCapacity;
        private System.Windows.Forms.CheckBox chk_ApplyToAll;
        private System.Windows.Forms.Label lbl_DeskCapacity;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.Label label1;
    }
}