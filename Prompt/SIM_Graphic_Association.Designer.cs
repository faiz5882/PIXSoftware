namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Graphic_Association
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Graphic_Association));
            this.cb_Scenario = new System.Windows.Forms.ComboBox();
            this.cb_Column = new System.Windows.Forms.ComboBox();
            this.lbl_Scenario = new System.Windows.Forms.Label();
            this.lbl_Column = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_DisplayedName = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_Abscissa = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cb_Scenario
            // 
            this.cb_Scenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Scenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Scenario.FormattingEnabled = true;
            this.cb_Scenario.Location = new System.Drawing.Point(140, 54);
            this.cb_Scenario.Name = "cb_Scenario";
            this.cb_Scenario.Size = new System.Drawing.Size(402, 21);
            this.cb_Scenario.TabIndex = 0;
            this.cb_Scenario.SelectedIndexChanged += new System.EventHandler(this.cb_Scenario_SelectedIndexChanged);
            // 
            // cb_Column
            // 
            this.cb_Column.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Column.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Column.FormattingEnabled = true;
            this.cb_Column.Location = new System.Drawing.Point(140, 404);
            this.cb_Column.Name = "cb_Column";
            this.cb_Column.Size = new System.Drawing.Size(402, 21);
            this.cb_Column.TabIndex = 2;
            this.cb_Column.SelectedIndexChanged += new System.EventHandler(this.cb_Column_SelectedIndexChanged);
            // 
            // lbl_Scenario
            // 
            this.lbl_Scenario.AutoSize = true;
            this.lbl_Scenario.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Scenario.Location = new System.Drawing.Point(12, 57);
            this.lbl_Scenario.Name = "lbl_Scenario";
            this.lbl_Scenario.Size = new System.Drawing.Size(117, 13);
            this.lbl_Scenario.TabIndex = 8;
            this.lbl_Scenario.Text = "Data group (Scenario) :";
            // 
            // lbl_Column
            // 
            this.lbl_Column.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Column.AutoSize = true;
            this.lbl_Column.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Column.Location = new System.Drawing.Point(12, 407);
            this.lbl_Column.Name = "lbl_Column";
            this.lbl_Column.Size = new System.Drawing.Size(61, 13);
            this.lbl_Column.TabIndex = 10;
            this.lbl_Column.Text = "Column (Y):";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(88, 12);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(113, 20);
            this.lbl_Title.TabIndex = 11;
            this.lbl_Title.Text = "Add new curve";
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(20, 468);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 12;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(446, 468);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(7, 434);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Displayed name :";
            // 
            // txt_DisplayedName
            // 
            this.txt_DisplayedName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DisplayedName.Location = new System.Drawing.Point(140, 431);
            this.txt_DisplayedName.Name = "txt_DisplayedName";
            this.txt_DisplayedName.Size = new System.Drawing.Size(402, 20);
            this.txt_DisplayedName.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(15, 81);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(527, 284);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Abscissa (X):";
            // 
            // cb_Abscissa
            // 
            this.cb_Abscissa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Abscissa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Abscissa.FormattingEnabled = true;
            this.cb_Abscissa.Location = new System.Drawing.Point(140, 377);
            this.cb_Abscissa.Name = "cb_Abscissa";
            this.cb_Abscissa.Size = new System.Drawing.Size(402, 21);
            this.cb_Abscissa.TabIndex = 15;
            // 
            // SIM_Graphic_Association
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(560, 506);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_Abscissa);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.txt_DisplayedName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.lbl_Column);
            this.Controls.Add(this.lbl_Scenario);
            this.Controls.Add(this.cb_Column);
            this.Controls.Add(this.cb_Scenario);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SIM_Graphic_Association";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add new curve";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_Scenario;
        private System.Windows.Forms.ComboBox cb_Column;
        private System.Windows.Forms.Label lbl_Scenario;
        private System.Windows.Forms.Label lbl_Column;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_DisplayedName;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_Abscissa;
    }
}