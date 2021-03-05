namespace SIMCORE_TOOL.Assistant
{
    partial class BHS_Arrival_Containers_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHS_Arrival_Containers_Assistant));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_FC = new System.Windows.Forms.ComboBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.lbl_First = new System.Windows.Forms.Label();
            this.lbl_Second = new System.Windows.Forms.Label();
            this.txt_FirstValue = new System.Windows.Forms.TextBox();
            this.txt_SecondValue = new System.Windows.Forms.TextBox();
            this.gb_Parameters = new System.Windows.Forms.GroupBox();
            this.cb_Laterals = new System.Windows.Forms.ComboBox();
            this.lbl_Third = new System.Windows.Forms.Label();
            this.gb_Parameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // cb_FC
            // 
            resources.ApplyResources(this.cb_FC, "cb_FC");
            this.cb_FC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FC.FormattingEnabled = true;
            this.cb_FC.Name = "cb_FC";
            this.cb_FC.SelectedIndexChanged += new System.EventHandler(this.cb_FC_SelectedIndexChanged);
            // 
            // BT_OK
            // 
            resources.ApplyResources(this.BT_OK, "BT_OK");
            this.BT_OK.BackColor = System.Drawing.Color.Transparent;
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.UseVisualStyleBackColor = false;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_CANCEL
            // 
            resources.ApplyResources(this.BT_CANCEL, "BT_CANCEL");
            this.BT_CANCEL.BackColor = System.Drawing.Color.Transparent;
            this.BT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.BT_CANCEL.UseVisualStyleBackColor = false;
            // 
            // lbl_First
            // 
            resources.ApplyResources(this.lbl_First, "lbl_First");
            this.lbl_First.BackColor = System.Drawing.Color.Transparent;
            this.lbl_First.Name = "lbl_First";
            // 
            // lbl_Second
            // 
            resources.ApplyResources(this.lbl_Second, "lbl_Second");
            this.lbl_Second.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Second.Name = "lbl_Second";
            // 
            // txt_FirstValue
            // 
            resources.ApplyResources(this.txt_FirstValue, "txt_FirstValue");
            this.txt_FirstValue.BackColor = System.Drawing.Color.White;
            this.txt_FirstValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_FirstValue.Name = "txt_FirstValue";
            this.txt_FirstValue.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // txt_SecondValue
            // 
            resources.ApplyResources(this.txt_SecondValue, "txt_SecondValue");
            this.txt_SecondValue.BackColor = System.Drawing.Color.White;
            this.txt_SecondValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txt_SecondValue.Name = "txt_SecondValue";
            this.txt_SecondValue.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // gb_Parameters
            // 
            resources.ApplyResources(this.gb_Parameters, "gb_Parameters");
            this.gb_Parameters.BackColor = System.Drawing.Color.Transparent;
            this.gb_Parameters.Controls.Add(this.cb_Laterals);
            this.gb_Parameters.Controls.Add(this.lbl_Third);
            this.gb_Parameters.Controls.Add(this.txt_FirstValue);
            this.gb_Parameters.Controls.Add(this.txt_SecondValue);
            this.gb_Parameters.Controls.Add(this.lbl_First);
            this.gb_Parameters.Controls.Add(this.lbl_Second);
            this.gb_Parameters.Name = "gb_Parameters";
            this.gb_Parameters.TabStop = false;
            // 
            // cb_Laterals
            // 
            resources.ApplyResources(this.cb_Laterals, "cb_Laterals");
            this.cb_Laterals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Laterals.FormattingEnabled = true;
            this.cb_Laterals.Items.AddRange(new object[] {
            resources.GetString("cb_Laterals.Items"),
            resources.GetString("cb_Laterals.Items1")});
            this.cb_Laterals.Name = "cb_Laterals";
            this.cb_Laterals.SelectedIndexChanged += new System.EventHandler(this.cb_Laterals_SelectedIndexChanged);
            // 
            // lbl_Third
            // 
            resources.ApplyResources(this.lbl_Third, "lbl_Third");
            this.lbl_Third.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Third.Name = "lbl_Third";
            // 
            // BHS_Arrival_Containers_Assistant
            // 
            this.AcceptButton = this.BT_OK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BT_CANCEL;
            this.Controls.Add(this.gb_Parameters);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_FC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BHS_Arrival_Containers_Assistant";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.BHS_Arrival_Containers_Assistant_Load);
            this.gb_Parameters.ResumeLayout(false);
            this.gb_Parameters.PerformLayout();
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
        private System.Windows.Forms.ComboBox cb_Laterals;
        private System.Windows.Forms.Label lbl_Third;
    }
}