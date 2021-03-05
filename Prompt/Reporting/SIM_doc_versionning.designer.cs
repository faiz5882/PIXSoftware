namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_doc_versionning
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_doc_versionning));
            this.dgv_versionning = new System.Windows.Forms.DataGridView();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReissueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Page = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_approval = new System.Windows.Forms.DataGridView();
            this.Kind = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Name_controleur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrgUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Signature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.p_form = new System.Windows.Forms.Panel();
            this.lb_partOf = new System.Windows.Forms.Label();
            this.tb_partOf = new System.Windows.Forms.TextBox();
            this.lb_appendices = new System.Windows.Forms.Label();
            this.tb_appendices = new System.Windows.Forms.TextBox();
            this.lb_issuedDept = new System.Windows.Forms.Label();
            this.tb_issuedDept = new System.Windows.Forms.TextBox();
            this.lb_projStep = new System.Windows.Forms.Label();
            this.tb_projStep = new System.Windows.Forms.TextBox();
            this.lb_docStatus = new System.Windows.Forms.Label();
            this.tb_docStatus = new System.Windows.Forms.TextBox();
            this.lb_extDocRef = new System.Windows.Forms.Label();
            this.tb_extDocRef = new System.Windows.Forms.TextBox();
            this.lb_intDocRef = new System.Windows.Forms.Label();
            this.tb_intDocRef = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_versionning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_approval)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.p_form.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_versionning
            // 
            this.dgv_versionning.AllowUserToOrderColumns = true;
            this.dgv_versionning.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_versionning.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Version,
            this.ReissueDate,
            this.ChangeDesc,
            this.Page,
            this.Department});
            this.dgv_versionning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_versionning.Location = new System.Drawing.Point(0, 0);
            this.dgv_versionning.Name = "dgv_versionning";
            this.dgv_versionning.Size = new System.Drawing.Size(730, 104);
            this.dgv_versionning.TabIndex = 1;
            // 
            // Version
            // 
            this.Version.Frozen = true;
            this.Version.HeaderText = "Version";
            this.Version.MinimumWidth = 20;
            this.Version.Name = "Version";
            this.Version.Width = 70;
            // 
            // ReissueDate
            // 
            this.ReissueDate.Frozen = true;
            this.ReissueDate.HeaderText = "Reissue Date";
            this.ReissueDate.MinimumWidth = 70;
            this.ReissueDate.Name = "ReissueDate";
            // 
            // ChangeDesc
            // 
            this.ChangeDesc.Frozen = true;
            this.ChangeDesc.HeaderText = "Description of Change";
            this.ChangeDesc.MinimumWidth = 100;
            this.ChangeDesc.Name = "ChangeDesc";
            this.ChangeDesc.Width = 200;
            // 
            // Page
            // 
            this.Page.Frozen = true;
            this.Page.HeaderText = "Page(s)";
            this.Page.MinimumWidth = 50;
            this.Page.Name = "Page";
            // 
            // Department
            // 
            this.Department.Frozen = true;
            this.Department.HeaderText = "Department";
            this.Department.MinimumWidth = 50;
            this.Department.Name = "Department";
            this.Department.Width = 130;
            // 
            // dgv_approval
            // 
            this.dgv_approval.AllowUserToOrderColumns = true;
            this.dgv_approval.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_approval.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kind,
            this.Name_controleur,
            this.OrgUnit,
            this.Signature,
            this.Date});
            this.dgv_approval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_approval.Location = new System.Drawing.Point(0, 0);
            this.dgv_approval.Name = "dgv_approval";
            this.dgv_approval.Size = new System.Drawing.Size(730, 122);
            this.dgv_approval.TabIndex = 2;
            // 
            // Kind
            // 
            this.Kind.HeaderText = "Kind";
            this.Kind.MinimumWidth = 50;
            this.Kind.Name = "Kind";
            // 
            // Name_controleur
            // 
            this.Name_controleur.HeaderText = "Name";
            this.Name_controleur.MinimumWidth = 50;
            this.Name_controleur.Name = "Name_controleur";
            // 
            // OrgUnit
            // 
            this.OrgUnit.HeaderText = "Org. unit";
            this.OrgUnit.MinimumWidth = 50;
            this.OrgUnit.Name = "OrgUnit";
            // 
            // Signature
            // 
            this.Signature.HeaderText = "Signature";
            this.Signature.MinimumWidth = 50;
            this.Signature.Name = "Signature";
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 50;
            this.Date.Name = "Date";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_versionning);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_approval);
            this.splitContainer1.Size = new System.Drawing.Size(730, 230);
            this.splitContainer1.SplitterDistance = 104;
            this.splitContainer1.TabIndex = 3;
            // 
            // p_form
            // 
            this.p_form.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.p_form.BackColor = System.Drawing.Color.Transparent;
            this.p_form.Controls.Add(this.lb_partOf);
            this.p_form.Controls.Add(this.tb_partOf);
            this.p_form.Controls.Add(this.lb_appendices);
            this.p_form.Controls.Add(this.tb_appendices);
            this.p_form.Controls.Add(this.lb_issuedDept);
            this.p_form.Controls.Add(this.tb_issuedDept);
            this.p_form.Controls.Add(this.lb_projStep);
            this.p_form.Controls.Add(this.tb_projStep);
            this.p_form.Controls.Add(this.lb_docStatus);
            this.p_form.Controls.Add(this.tb_docStatus);
            this.p_form.Controls.Add(this.lb_extDocRef);
            this.p_form.Controls.Add(this.tb_extDocRef);
            this.p_form.Controls.Add(this.lb_intDocRef);
            this.p_form.Controls.Add(this.tb_intDocRef);
            this.p_form.Location = new System.Drawing.Point(0, 236);
            this.p_form.Name = "p_form";
            this.p_form.Size = new System.Drawing.Size(730, 140);
            this.p_form.TabIndex = 3;
            // 
            // lb_partOf
            // 
            this.lb_partOf.AutoSize = true;
            this.lb_partOf.Location = new System.Drawing.Point(77, 33);
            this.lb_partOf.Name = "lb_partOf";
            this.lb_partOf.Size = new System.Drawing.Size(41, 13);
            this.lb_partOf.TabIndex = 13;
            this.lb_partOf.Text = "Part of:";
            // 
            // tb_partOf
            // 
            this.tb_partOf.Location = new System.Drawing.Point(124, 30);
            this.tb_partOf.Name = "tb_partOf";
            this.tb_partOf.Size = new System.Drawing.Size(169, 20);
            this.tb_partOf.TabIndex = 1;
            // 
            // lb_appendices
            // 
            this.lb_appendices.AutoSize = true;
            this.lb_appendices.Location = new System.Drawing.Point(52, 111);
            this.lb_appendices.Name = "lb_appendices";
            this.lb_appendices.Size = new System.Drawing.Size(66, 13);
            this.lb_appendices.TabIndex = 11;
            this.lb_appendices.Text = "Appendices:";
            // 
            // tb_appendices
            // 
            this.tb_appendices.Location = new System.Drawing.Point(124, 108);
            this.tb_appendices.Name = "tb_appendices";
            this.tb_appendices.Size = new System.Drawing.Size(169, 20);
            this.tb_appendices.TabIndex = 4;
            // 
            // lb_issuedDept
            // 
            this.lb_issuedDept.AutoSize = true;
            this.lb_issuedDept.Location = new System.Drawing.Point(21, 59);
            this.lb_issuedDept.Name = "lb_issuedDept";
            this.lb_issuedDept.Size = new System.Drawing.Size(97, 13);
            this.lb_issuedDept.TabIndex = 9;
            this.lb_issuedDept.Text = "Issued department:";
            // 
            // tb_issuedDept
            // 
            this.tb_issuedDept.Location = new System.Drawing.Point(124, 56);
            this.tb_issuedDept.Name = "tb_issuedDept";
            this.tb_issuedDept.Size = new System.Drawing.Size(169, 20);
            this.tb_issuedDept.TabIndex = 2;
            // 
            // lb_projStep
            // 
            this.lb_projStep.AutoSize = true;
            this.lb_projStep.Location = new System.Drawing.Point(52, 10);
            this.lb_projStep.Name = "lb_projStep";
            this.lb_projStep.Size = new System.Drawing.Size(66, 13);
            this.lb_projStep.TabIndex = 7;
            this.lb_projStep.Text = "Project step:";
            // 
            // tb_projStep
            // 
            this.tb_projStep.Location = new System.Drawing.Point(124, 4);
            this.tb_projStep.Name = "tb_projStep";
            this.tb_projStep.Size = new System.Drawing.Size(169, 20);
            this.tb_projStep.TabIndex = 0;
            // 
            // lb_docStatus
            // 
            this.lb_docStatus.AutoSize = true;
            this.lb_docStatus.Location = new System.Drawing.Point(28, 85);
            this.lb_docStatus.Name = "lb_docStatus";
            this.lb_docStatus.Size = new System.Drawing.Size(90, 13);
            this.lb_docStatus.TabIndex = 5;
            this.lb_docStatus.Text = "Document status:";
            // 
            // tb_docStatus
            // 
            this.tb_docStatus.Location = new System.Drawing.Point(124, 82);
            this.tb_docStatus.Name = "tb_docStatus";
            this.tb_docStatus.Size = new System.Drawing.Size(169, 20);
            this.tb_docStatus.TabIndex = 3;
            // 
            // lb_extDocRef
            // 
            this.lb_extDocRef.AutoSize = true;
            this.lb_extDocRef.Location = new System.Drawing.Point(316, 33);
            this.lb_extDocRef.Name = "lb_extDocRef";
            this.lb_extDocRef.Size = new System.Drawing.Size(110, 13);
            this.lb_extDocRef.TabIndex = 3;
            this.lb_extDocRef.Text = "Extern document No.:";
            // 
            // tb_extDocRef
            // 
            this.tb_extDocRef.Location = new System.Drawing.Point(432, 30);
            this.tb_extDocRef.Name = "tb_extDocRef";
            this.tb_extDocRef.Size = new System.Drawing.Size(169, 20);
            this.tb_extDocRef.TabIndex = 6;
            // 
            // lb_intDocRef
            // 
            this.lb_intDocRef.AutoSize = true;
            this.lb_intDocRef.Location = new System.Drawing.Point(319, 7);
            this.lb_intDocRef.Name = "lb_intDocRef";
            this.lb_intDocRef.Size = new System.Drawing.Size(107, 13);
            this.lb_intDocRef.TabIndex = 1;
            this.lb_intDocRef.Text = "Intern document No.:";
            // 
            // tb_intDocRef
            // 
            this.tb_intDocRef.Location = new System.Drawing.Point(432, 4);
            this.tb_intDocRef.Name = "tb_intDocRef";
            this.tb_intDocRef.Size = new System.Drawing.Size(169, 20);
            this.tb_intDocRef.TabIndex = 5;
            // 
            // SIM_doc_versionning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(730, 376);
            this.Controls.Add(this.p_form);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(738, 410);
            this.Name = "SIM_doc_versionning";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Version & Document Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveData);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_versionning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_approval)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.p_form.ResumeLayout(false);
            this.p_form.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_versionning;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReissueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Page;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private System.Windows.Forms.DataGridView dgv_approval;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel p_form;
        private System.Windows.Forms.Label lb_extDocRef;
        private System.Windows.Forms.TextBox tb_extDocRef;
        private System.Windows.Forms.Label lb_intDocRef;
        private System.Windows.Forms.TextBox tb_intDocRef;
        private System.Windows.Forms.Label lb_docStatus;
        private System.Windows.Forms.TextBox tb_docStatus;
        private System.Windows.Forms.Label lb_projStep;
        private System.Windows.Forms.TextBox tb_projStep;
        private System.Windows.Forms.Label lb_issuedDept;
        private System.Windows.Forms.TextBox tb_issuedDept;
        private System.Windows.Forms.Label lb_appendices;
        private System.Windows.Forms.TextBox tb_appendices;
        private System.Windows.Forms.Label lb_partOf;
        private System.Windows.Forms.TextBox tb_partOf;
        private System.Windows.Forms.DataGridViewComboBoxColumn Kind;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name_controleur;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrgUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Signature;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
    }
}