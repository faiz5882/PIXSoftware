namespace SIMCORE_TOOL.Assistant
{
    partial class OCT_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OCT_Assistant));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_FC = new System.Windows.Forms.ComboBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.lbl_First = new System.Windows.Forms.Label();
            this.lbl_Second = new System.Windows.Forms.Label();
            this.txt_FirstValue = new System.Windows.Forms.TextBox();
            this.txt_SecondValue = new System.Windows.Forms.TextBox();
            this.gb_Parameters = new System.Windows.Forms.GroupBox();
            this.gb_Delivery = new System.Windows.Forms.GroupBox();
            this.txt_EBS_Delivery = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gb_Parameters.SuspendLayout();
            this.gb_Delivery.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(109, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Category :";
            // 
            // cb_FC
            // 
            this.cb_FC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FC.FormattingEnabled = true;
            this.cb_FC.Location = new System.Drawing.Point(225, 26);
            this.cb_FC.Name = "cb_FC";
            this.cb_FC.Size = new System.Drawing.Size(236, 21);
            this.cb_FC.TabIndex = 16;
            this.cb_FC.SelectedIndexChanged += new System.EventHandler(this.cb_FC_SelectedIndexChanged);
            // 
            // BT_OK
            // 
            this.BT_OK.BackColor = System.Drawing.Color.Transparent;
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Location = new System.Drawing.Point(48, 184);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(75, 23);
            this.BT_OK.TabIndex = 52;
            this.BT_OK.Text = "OK";
            this.BT_OK.UseVisualStyleBackColor = false;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_CANCEL
            // 
            this.BT_CANCEL.BackColor = System.Drawing.Color.Transparent;
            this.BT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_CANCEL.Location = new System.Drawing.Point(411, 184);
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.BT_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BT_CANCEL.TabIndex = 53;
            this.BT_CANCEL.Text = "Cancel";
            this.BT_CANCEL.UseVisualStyleBackColor = false;
            // 
            // lbl_First
            // 
            this.lbl_First.AutoSize = true;
            this.lbl_First.BackColor = System.Drawing.Color.Transparent;
            this.lbl_First.Location = new System.Drawing.Point(13, 32);
            this.lbl_First.Name = "lbl_First";
            this.lbl_First.Size = new System.Drawing.Size(35, 13);
            this.lbl_First.TabIndex = 54;
            this.lbl_First.Text = "label2";
            // 
            // lbl_Second
            // 
            this.lbl_Second.AutoSize = true;
            this.lbl_Second.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Second.Location = new System.Drawing.Point(13, 68);
            this.lbl_Second.Name = "lbl_Second";
            this.lbl_Second.Size = new System.Drawing.Size(35, 13);
            this.lbl_Second.TabIndex = 55;
            this.lbl_Second.Text = "label3";
            // 
            // txt_FirstValue
            // 
            this.txt_FirstValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_FirstValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_FirstValue.Location = new System.Drawing.Point(311, 29);
            this.txt_FirstValue.Name = "txt_FirstValue";
            this.txt_FirstValue.Size = new System.Drawing.Size(51, 20);
            this.txt_FirstValue.TabIndex = 56;
            this.txt_FirstValue.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            this.txt_FirstValue.Leave += new System.EventHandler(this.txt_FirstValue_Leave);
            // 
            // txt_SecondValue
            // 
            this.txt_SecondValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_SecondValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_SecondValue.Location = new System.Drawing.Point(311, 65);
            this.txt_SecondValue.Name = "txt_SecondValue";
            this.txt_SecondValue.Size = new System.Drawing.Size(51, 20);
            this.txt_SecondValue.TabIndex = 57;
            this.txt_SecondValue.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            this.txt_SecondValue.Leave += new System.EventHandler(this.txt_FirstValue_Leave);
            // 
            // gb_Parameters
            // 
            this.gb_Parameters.BackColor = System.Drawing.Color.Transparent;
            this.gb_Parameters.Controls.Add(this.txt_FirstValue);
            this.gb_Parameters.Controls.Add(this.txt_SecondValue);
            this.gb_Parameters.Controls.Add(this.lbl_First);
            this.gb_Parameters.Controls.Add(this.lbl_Second);
            this.gb_Parameters.Location = new System.Drawing.Point(57, 62);
            this.gb_Parameters.Name = "gb_Parameters";
            this.gb_Parameters.Size = new System.Drawing.Size(404, 100);
            this.gb_Parameters.TabIndex = 58;
            this.gb_Parameters.TabStop = false;
            this.gb_Parameters.Text = "Opening/Closing Times";
            // 
            // gb_Delivery
            // 
            this.gb_Delivery.BackColor = System.Drawing.Color.Transparent;
            this.gb_Delivery.Controls.Add(this.txt_EBS_Delivery);
            this.gb_Delivery.Controls.Add(this.label2);
            this.gb_Delivery.Location = new System.Drawing.Point(57, 168);
            this.gb_Delivery.Name = "gb_Delivery";
            this.gb_Delivery.Size = new System.Drawing.Size(404, 60);
            this.gb_Delivery.TabIndex = 59;
            this.gb_Delivery.TabStop = false;
            this.gb_Delivery.Text = "EBS delivery time";
            this.gb_Delivery.Visible = false;
            // 
            // txt_EBS_Delivery
            // 
            this.txt_EBS_Delivery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_EBS_Delivery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_EBS_Delivery.Location = new System.Drawing.Point(311, 23);
            this.txt_EBS_Delivery.Name = "txt_EBS_Delivery";
            this.txt_EBS_Delivery.Size = new System.Drawing.Size(51, 20);
            this.txt_EBS_Delivery.TabIndex = 58;
            this.txt_EBS_Delivery.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            this.txt_EBS_Delivery.Leave += new System.EventHandler(this.txt_FirstValue_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Minutes before STD";
            // 
            // OCT_Assistant
            // 
            this.AcceptButton = this.BT_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BT_CANCEL;
            this.ClientSize = new System.Drawing.Size(527, 229);
            this.Controls.Add(this.gb_Delivery);
            this.Controls.Add(this.gb_Parameters);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_FC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OCT_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OCT_Assistant";
            this.gb_Parameters.ResumeLayout(false);
            this.gb_Parameters.PerformLayout();
            this.gb_Delivery.ResumeLayout(false);
            this.gb_Delivery.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_FC;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_CANCEL;
        private System.Windows.Forms.Label lbl_First;
        private System.Windows.Forms.Label lbl_Second;
        private System.Windows.Forms.TextBox txt_FirstValue;
        private System.Windows.Forms.TextBox txt_SecondValue;
        private System.Windows.Forms.GroupBox gb_Parameters;
        private System.Windows.Forms.GroupBox gb_Delivery;
        private System.Windows.Forms.TextBox txt_EBS_Delivery;
        private System.Windows.Forms.Label label2;
    }
}