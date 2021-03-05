namespace SIMCORE_TOOL.Assistant
{
    partial class BHS_Flow_Split_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHS_Flow_Split_Assistant));
            this.lbl_FC = new System.Windows.Forms.Label();
            this.cb_FC = new System.Windows.Forms.ComboBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.gb_HBS = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Level5_Mean = new System.Windows.Forms.TextBox();
            this.txt_Level4_Mean = new System.Windows.Forms.TextBox();
            this.txt_Level3_Mean = new System.Windows.Forms.TextBox();
            this.txt_Level2_Mean = new System.Windows.Forms.TextBox();
            this.txt_Level5 = new System.Windows.Forms.TextBox();
            this.txt_Level4 = new System.Windows.Forms.TextBox();
            this.txt_Level3 = new System.Windows.Forms.TextBox();
            this.txt_Level2 = new System.Windows.Forms.TextBox();
            this.lbl_Level5 = new System.Windows.Forms.Label();
            this.lbl_Level4 = new System.Windows.Forms.Label();
            this.lbl_Level3 = new System.Windows.Forms.Label();
            this.lbl_Level2 = new System.Windows.Forms.Label();
            this.gb_MES = new System.Windows.Forms.GroupBox();
            this.txt_DMRC = new System.Windows.Forms.TextBox();
            this.lbl_DMRC = new System.Windows.Forms.Label();
            this.txt_MES_tran = new System.Windows.Forms.TextBox();
            this.txt_MES_Ori = new System.Windows.Forms.TextBox();
            this.lbl_Transfering = new System.Windows.Forms.Label();
            this.lbl_Originating = new System.Windows.Forms.Label();
            this.gb_Interlink = new System.Windows.Forms.GroupBox();
            this.txt_IntLnk_Orig = new System.Windows.Forms.TextBox();
            this.txt_IntLnk_Transf = new System.Windows.Forms.TextBox();
            this.lbl_Orig_IntLnk = new System.Windows.Forms.Label();
            this.lbl_Transfer = new System.Windows.Forms.Label();
            this.tt_Tooltips = new System.Windows.Forms.ToolTip(this.components);
            this.gb_HBS.SuspendLayout();
            this.gb_MES.SuspendLayout();
            this.gb_Interlink.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_FC
            // 
            this.lbl_FC.AccessibleDescription = null;
            this.lbl_FC.AccessibleName = null;
            resources.ApplyResources(this.lbl_FC, "lbl_FC");
            this.lbl_FC.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FC.Font = null;
            this.lbl_FC.Name = "lbl_FC";
            this.tt_Tooltips.SetToolTip(this.lbl_FC, resources.GetString("lbl_FC.ToolTip"));
            // 
            // cb_FC
            // 
            this.cb_FC.AccessibleDescription = null;
            this.cb_FC.AccessibleName = null;
            resources.ApplyResources(this.cb_FC, "cb_FC");
            this.cb_FC.BackgroundImage = null;
            this.cb_FC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FC.Font = null;
            this.cb_FC.FormattingEnabled = true;
            this.cb_FC.Name = "cb_FC";
            this.tt_Tooltips.SetToolTip(this.cb_FC, resources.GetString("cb_FC.ToolTip"));
            this.cb_FC.SelectedIndexChanged += new System.EventHandler(this.cb_FC_SelectedIndexChanged);
            // 
            // BT_OK
            // 
            this.BT_OK.AccessibleDescription = null;
            this.BT_OK.AccessibleName = null;
            resources.ApplyResources(this.BT_OK, "BT_OK");
            this.BT_OK.BackColor = System.Drawing.Color.Transparent;
            this.BT_OK.BackgroundImage = null;
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Font = null;
            this.BT_OK.Name = "BT_OK";
            this.tt_Tooltips.SetToolTip(this.BT_OK, resources.GetString("BT_OK.ToolTip"));
            this.BT_OK.UseVisualStyleBackColor = false;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_CANCEL
            // 
            this.BT_CANCEL.AccessibleDescription = null;
            this.BT_CANCEL.AccessibleName = null;
            resources.ApplyResources(this.BT_CANCEL, "BT_CANCEL");
            this.BT_CANCEL.BackColor = System.Drawing.Color.Transparent;
            this.BT_CANCEL.BackgroundImage = null;
            this.BT_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_CANCEL.Font = null;
            this.BT_CANCEL.Name = "BT_CANCEL";
            this.tt_Tooltips.SetToolTip(this.BT_CANCEL, resources.GetString("BT_CANCEL.ToolTip"));
            this.BT_CANCEL.UseVisualStyleBackColor = false;
            // 
            // gb_HBS
            // 
            this.gb_HBS.AccessibleDescription = null;
            this.gb_HBS.AccessibleName = null;
            resources.ApplyResources(this.gb_HBS, "gb_HBS");
            this.gb_HBS.BackColor = System.Drawing.Color.Transparent;
            this.gb_HBS.BackgroundImage = null;
            this.gb_HBS.Controls.Add(this.label1);
            this.gb_HBS.Controls.Add(this.txt_Level5_Mean);
            this.gb_HBS.Controls.Add(this.txt_Level4_Mean);
            this.gb_HBS.Controls.Add(this.txt_Level3_Mean);
            this.gb_HBS.Controls.Add(this.txt_Level2_Mean);
            this.gb_HBS.Controls.Add(this.txt_Level5);
            this.gb_HBS.Controls.Add(this.txt_Level4);
            this.gb_HBS.Controls.Add(this.txt_Level3);
            this.gb_HBS.Controls.Add(this.txt_Level2);
            this.gb_HBS.Controls.Add(this.lbl_Level5);
            this.gb_HBS.Controls.Add(this.lbl_Level4);
            this.gb_HBS.Controls.Add(this.lbl_Level3);
            this.gb_HBS.Controls.Add(this.lbl_Level2);
            this.gb_HBS.Name = "gb_HBS";
            this.gb_HBS.TabStop = false;
            this.tt_Tooltips.SetToolTip(this.gb_HBS, resources.GetString("gb_HBS.ToolTip"));
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            this.tt_Tooltips.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // txt_Level5_Mean
            // 
            this.txt_Level5_Mean.AcceptsReturn = true;
            this.txt_Level5_Mean.AccessibleDescription = null;
            this.txt_Level5_Mean.AccessibleName = null;
            resources.ApplyResources(this.txt_Level5_Mean, "txt_Level5_Mean");
            this.txt_Level5_Mean.BackgroundImage = null;
            this.txt_Level5_Mean.Font = null;
            this.txt_Level5_Mean.Name = "txt_Level5_Mean";
            this.tt_Tooltips.SetToolTip(this.txt_Level5_Mean, resources.GetString("txt_Level5_Mean.ToolTip"));
            // 
            // txt_Level4_Mean
            // 
            this.txt_Level4_Mean.AcceptsReturn = true;
            this.txt_Level4_Mean.AccessibleDescription = null;
            this.txt_Level4_Mean.AccessibleName = null;
            resources.ApplyResources(this.txt_Level4_Mean, "txt_Level4_Mean");
            this.txt_Level4_Mean.BackgroundImage = null;
            this.txt_Level4_Mean.Font = null;
            this.txt_Level4_Mean.Name = "txt_Level4_Mean";
            this.tt_Tooltips.SetToolTip(this.txt_Level4_Mean, resources.GetString("txt_Level4_Mean.ToolTip"));
            // 
            // txt_Level3_Mean
            // 
            this.txt_Level3_Mean.AcceptsReturn = true;
            this.txt_Level3_Mean.AccessibleDescription = null;
            this.txt_Level3_Mean.AccessibleName = null;
            resources.ApplyResources(this.txt_Level3_Mean, "txt_Level3_Mean");
            this.txt_Level3_Mean.BackgroundImage = null;
            this.txt_Level3_Mean.Font = null;
            this.txt_Level3_Mean.Name = "txt_Level3_Mean";
            this.tt_Tooltips.SetToolTip(this.txt_Level3_Mean, resources.GetString("txt_Level3_Mean.ToolTip"));
            // 
            // txt_Level2_Mean
            // 
            this.txt_Level2_Mean.AccessibleDescription = null;
            this.txt_Level2_Mean.AccessibleName = null;
            resources.ApplyResources(this.txt_Level2_Mean, "txt_Level2_Mean");
            this.txt_Level2_Mean.BackgroundImage = null;
            this.txt_Level2_Mean.Font = null;
            this.txt_Level2_Mean.Name = "txt_Level2_Mean";
            this.tt_Tooltips.SetToolTip(this.txt_Level2_Mean, resources.GetString("txt_Level2_Mean.ToolTip"));
            // 
            // txt_Level5
            // 
            this.txt_Level5.AccessibleDescription = null;
            this.txt_Level5.AccessibleName = null;
            resources.ApplyResources(this.txt_Level5, "txt_Level5");
            this.txt_Level5.BackgroundImage = null;
            this.txt_Level5.Font = null;
            this.txt_Level5.Name = "txt_Level5";
            this.txt_Level5.Tag = "% to HBS Level 5";
            this.tt_Tooltips.SetToolTip(this.txt_Level5, resources.GetString("txt_Level5.ToolTip"));
            this.txt_Level5.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // txt_Level4
            // 
            this.txt_Level4.AccessibleDescription = null;
            this.txt_Level4.AccessibleName = null;
            resources.ApplyResources(this.txt_Level4, "txt_Level4");
            this.txt_Level4.BackgroundImage = null;
            this.txt_Level4.Font = null;
            this.txt_Level4.Name = "txt_Level4";
            this.txt_Level4.Tag = "% to HBS Level 4";
            this.tt_Tooltips.SetToolTip(this.txt_Level4, resources.GetString("txt_Level4.ToolTip"));
            this.txt_Level4.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // txt_Level3
            // 
            this.txt_Level3.AccessibleDescription = null;
            this.txt_Level3.AccessibleName = null;
            resources.ApplyResources(this.txt_Level3, "txt_Level3");
            this.txt_Level3.BackgroundImage = null;
            this.txt_Level3.Font = null;
            this.txt_Level3.Name = "txt_Level3";
            this.txt_Level3.Tag = "% to HBS Level 3";
            this.tt_Tooltips.SetToolTip(this.txt_Level3, resources.GetString("txt_Level3.ToolTip"));
            this.txt_Level3.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // txt_Level2
            // 
            this.txt_Level2.AccessibleDescription = null;
            this.txt_Level2.AccessibleName = null;
            resources.ApplyResources(this.txt_Level2, "txt_Level2");
            this.txt_Level2.BackgroundImage = null;
            this.txt_Level2.Font = null;
            this.txt_Level2.Name = "txt_Level2";
            this.txt_Level2.Tag = "% to HBS Level 2";
            this.tt_Tooltips.SetToolTip(this.txt_Level2, resources.GetString("txt_Level2.ToolTip"));
            this.txt_Level2.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // lbl_Level5
            // 
            this.lbl_Level5.AccessibleDescription = null;
            this.lbl_Level5.AccessibleName = null;
            resources.ApplyResources(this.lbl_Level5, "lbl_Level5");
            this.lbl_Level5.Font = null;
            this.lbl_Level5.Name = "lbl_Level5";
            this.tt_Tooltips.SetToolTip(this.lbl_Level5, resources.GetString("lbl_Level5.ToolTip"));
            // 
            // lbl_Level4
            // 
            this.lbl_Level4.AccessibleDescription = null;
            this.lbl_Level4.AccessibleName = null;
            resources.ApplyResources(this.lbl_Level4, "lbl_Level4");
            this.lbl_Level4.Font = null;
            this.lbl_Level4.Name = "lbl_Level4";
            this.tt_Tooltips.SetToolTip(this.lbl_Level4, resources.GetString("lbl_Level4.ToolTip"));
            // 
            // lbl_Level3
            // 
            this.lbl_Level3.AccessibleDescription = null;
            this.lbl_Level3.AccessibleName = null;
            resources.ApplyResources(this.lbl_Level3, "lbl_Level3");
            this.lbl_Level3.Font = null;
            this.lbl_Level3.Name = "lbl_Level3";
            this.tt_Tooltips.SetToolTip(this.lbl_Level3, resources.GetString("lbl_Level3.ToolTip"));
            // 
            // lbl_Level2
            // 
            this.lbl_Level2.AccessibleDescription = null;
            this.lbl_Level2.AccessibleName = null;
            resources.ApplyResources(this.lbl_Level2, "lbl_Level2");
            this.lbl_Level2.Font = null;
            this.lbl_Level2.Name = "lbl_Level2";
            this.tt_Tooltips.SetToolTip(this.lbl_Level2, resources.GetString("lbl_Level2.ToolTip"));
            // 
            // gb_MES
            // 
            this.gb_MES.AccessibleDescription = null;
            this.gb_MES.AccessibleName = null;
            resources.ApplyResources(this.gb_MES, "gb_MES");
            this.gb_MES.BackColor = System.Drawing.Color.Transparent;
            this.gb_MES.BackgroundImage = null;
            this.gb_MES.Controls.Add(this.txt_DMRC);
            this.gb_MES.Controls.Add(this.lbl_DMRC);
            this.gb_MES.Controls.Add(this.txt_MES_tran);
            this.gb_MES.Controls.Add(this.txt_MES_Ori);
            this.gb_MES.Controls.Add(this.lbl_Transfering);
            this.gb_MES.Controls.Add(this.lbl_Originating);
            this.gb_MES.Name = "gb_MES";
            this.gb_MES.TabStop = false;
            this.tt_Tooltips.SetToolTip(this.gb_MES, resources.GetString("gb_MES.ToolTip"));
            // 
            // txt_DMRC
            // 
            this.txt_DMRC.AccessibleDescription = null;
            this.txt_DMRC.AccessibleName = null;
            resources.ApplyResources(this.txt_DMRC, "txt_DMRC");
            this.txt_DMRC.BackgroundImage = null;
            this.txt_DMRC.Font = null;
            this.txt_DMRC.Name = "txt_DMRC";
            this.txt_DMRC.Tag = "";
            this.tt_Tooltips.SetToolTip(this.txt_DMRC, resources.GetString("txt_DMRC.ToolTip"));
            this.txt_DMRC.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // lbl_DMRC
            // 
            this.lbl_DMRC.AccessibleDescription = null;
            this.lbl_DMRC.AccessibleName = null;
            resources.ApplyResources(this.lbl_DMRC, "lbl_DMRC");
            this.lbl_DMRC.Font = null;
            this.lbl_DMRC.Name = "lbl_DMRC";
            this.tt_Tooltips.SetToolTip(this.lbl_DMRC, resources.GetString("lbl_DMRC.ToolTip"));
            // 
            // txt_MES_tran
            // 
            this.txt_MES_tran.AccessibleDescription = null;
            this.txt_MES_tran.AccessibleName = null;
            resources.ApplyResources(this.txt_MES_tran, "txt_MES_tran");
            this.txt_MES_tran.BackgroundImage = null;
            this.txt_MES_tran.Font = null;
            this.txt_MES_tran.Name = "txt_MES_tran";
            this.txt_MES_tran.Tag = "% MES (Transfer Bags)";
            this.tt_Tooltips.SetToolTip(this.txt_MES_tran, resources.GetString("txt_MES_tran.ToolTip"));
            this.txt_MES_tran.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // txt_MES_Ori
            // 
            this.txt_MES_Ori.AccessibleDescription = null;
            this.txt_MES_Ori.AccessibleName = null;
            resources.ApplyResources(this.txt_MES_Ori, "txt_MES_Ori");
            this.txt_MES_Ori.BackgroundImage = null;
            this.txt_MES_Ori.Font = null;
            this.txt_MES_Ori.Name = "txt_MES_Ori";
            this.txt_MES_Ori.Tag = "% MES (Originating Bags)";
            this.tt_Tooltips.SetToolTip(this.txt_MES_Ori, resources.GetString("txt_MES_Ori.ToolTip"));
            this.txt_MES_Ori.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // lbl_Transfering
            // 
            this.lbl_Transfering.AccessibleDescription = null;
            this.lbl_Transfering.AccessibleName = null;
            resources.ApplyResources(this.lbl_Transfering, "lbl_Transfering");
            this.lbl_Transfering.Font = null;
            this.lbl_Transfering.Name = "lbl_Transfering";
            this.tt_Tooltips.SetToolTip(this.lbl_Transfering, resources.GetString("lbl_Transfering.ToolTip"));
            // 
            // lbl_Originating
            // 
            this.lbl_Originating.AccessibleDescription = null;
            this.lbl_Originating.AccessibleName = null;
            resources.ApplyResources(this.lbl_Originating, "lbl_Originating");
            this.lbl_Originating.Font = null;
            this.lbl_Originating.Name = "lbl_Originating";
            this.tt_Tooltips.SetToolTip(this.lbl_Originating, resources.GetString("lbl_Originating.ToolTip"));
            // 
            // gb_Interlink
            // 
            this.gb_Interlink.AccessibleDescription = null;
            this.gb_Interlink.AccessibleName = null;
            resources.ApplyResources(this.gb_Interlink, "gb_Interlink");
            this.gb_Interlink.BackColor = System.Drawing.Color.Transparent;
            this.gb_Interlink.BackgroundImage = null;
            this.gb_Interlink.Controls.Add(this.txt_IntLnk_Orig);
            this.gb_Interlink.Controls.Add(this.txt_IntLnk_Transf);
            this.gb_Interlink.Controls.Add(this.lbl_Orig_IntLnk);
            this.gb_Interlink.Controls.Add(this.lbl_Transfer);
            this.gb_Interlink.Font = null;
            this.gb_Interlink.Name = "gb_Interlink";
            this.gb_Interlink.TabStop = false;
            this.tt_Tooltips.SetToolTip(this.gb_Interlink, resources.GetString("gb_Interlink.ToolTip"));
            // 
            // txt_IntLnk_Orig
            // 
            this.txt_IntLnk_Orig.AccessibleDescription = null;
            this.txt_IntLnk_Orig.AccessibleName = null;
            resources.ApplyResources(this.txt_IntLnk_Orig, "txt_IntLnk_Orig");
            this.txt_IntLnk_Orig.BackgroundImage = null;
            this.txt_IntLnk_Orig.Font = null;
            this.txt_IntLnk_Orig.Name = "txt_IntLnk_Orig";
            this.txt_IntLnk_Orig.Tag = "% Interlink (Originating Bags)-optional";
            this.tt_Tooltips.SetToolTip(this.txt_IntLnk_Orig, resources.GetString("txt_IntLnk_Orig.ToolTip"));
            this.txt_IntLnk_Orig.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // txt_IntLnk_Transf
            // 
            this.txt_IntLnk_Transf.AccessibleDescription = null;
            this.txt_IntLnk_Transf.AccessibleName = null;
            resources.ApplyResources(this.txt_IntLnk_Transf, "txt_IntLnk_Transf");
            this.txt_IntLnk_Transf.BackgroundImage = null;
            this.txt_IntLnk_Transf.Font = null;
            this.txt_IntLnk_Transf.Name = "txt_IntLnk_Transf";
            this.txt_IntLnk_Transf.Tag = "% Interlink (Transfer Bags)-optional";
            this.tt_Tooltips.SetToolTip(this.txt_IntLnk_Transf, resources.GetString("txt_IntLnk_Transf.ToolTip"));
            this.txt_IntLnk_Transf.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // lbl_Orig_IntLnk
            // 
            this.lbl_Orig_IntLnk.AccessibleDescription = null;
            this.lbl_Orig_IntLnk.AccessibleName = null;
            resources.ApplyResources(this.lbl_Orig_IntLnk, "lbl_Orig_IntLnk");
            this.lbl_Orig_IntLnk.Font = null;
            this.lbl_Orig_IntLnk.Name = "lbl_Orig_IntLnk";
            this.tt_Tooltips.SetToolTip(this.lbl_Orig_IntLnk, resources.GetString("lbl_Orig_IntLnk.ToolTip"));
            // 
            // lbl_Transfer
            // 
            this.lbl_Transfer.AccessibleDescription = null;
            this.lbl_Transfer.AccessibleName = null;
            resources.ApplyResources(this.lbl_Transfer, "lbl_Transfer");
            this.lbl_Transfer.Font = null;
            this.lbl_Transfer.Name = "lbl_Transfer";
            this.tt_Tooltips.SetToolTip(this.lbl_Transfer, resources.GetString("lbl_Transfer.ToolTip"));
            // 
            // BHS_Flow_Split_Assistant
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.gb_Interlink);
            this.Controls.Add(this.gb_MES);
            this.Controls.Add(this.gb_HBS);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.lbl_FC);
            this.Controls.Add(this.cb_FC);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BHS_Flow_Split_Assistant";
            this.ShowInTaskbar = false;
            this.tt_Tooltips.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.gb_HBS.ResumeLayout(false);
            this.gb_HBS.PerformLayout();
            this.gb_MES.ResumeLayout(false);
            this.gb_MES.PerformLayout();
            this.gb_Interlink.ResumeLayout(false);
            this.gb_Interlink.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_FC;
        private System.Windows.Forms.ComboBox cb_FC;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_CANCEL;
        private System.Windows.Forms.GroupBox gb_HBS;
        private System.Windows.Forms.TextBox txt_Level5;
        private System.Windows.Forms.TextBox txt_Level4;
        private System.Windows.Forms.TextBox txt_Level3;
        private System.Windows.Forms.TextBox txt_Level2;
        private System.Windows.Forms.Label lbl_Level5;
        private System.Windows.Forms.Label lbl_Level4;
        private System.Windows.Forms.Label lbl_Level3;
        private System.Windows.Forms.Label lbl_Level2;
        private System.Windows.Forms.GroupBox gb_MES;
        private System.Windows.Forms.TextBox txt_MES_tran;
        private System.Windows.Forms.TextBox txt_MES_Ori;
        private System.Windows.Forms.Label lbl_Transfering;
        private System.Windows.Forms.Label lbl_Originating;
        private System.Windows.Forms.GroupBox gb_Interlink;
        private System.Windows.Forms.TextBox txt_IntLnk_Orig;
        private System.Windows.Forms.TextBox txt_IntLnk_Transf;
        private System.Windows.Forms.Label lbl_Orig_IntLnk;
        private System.Windows.Forms.Label lbl_Transfer;
        private System.Windows.Forms.ToolTip tt_Tooltips;
        private System.Windows.Forms.TextBox txt_Level5_Mean;
        private System.Windows.Forms.TextBox txt_Level4_Mean;
        private System.Windows.Forms.TextBox txt_Level3_Mean;
        private System.Windows.Forms.TextBox txt_Level2_Mean;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_DMRC;
        private System.Windows.Forms.Label lbl_DMRC;
    }
}