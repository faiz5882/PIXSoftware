namespace SIMCORE_TOOL.Assistant
{
    partial class Process_Assistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Process_Assistant));
            this.cb_GroupID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_FirstDistribution = new System.Windows.Forms.GroupBox();
            this.p_Distribution = new System.Windows.Forms.Panel();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.gb_SecondDistribution = new System.Windows.Forms.GroupBox();
            this.p_SecondDistribution = new System.Windows.Forms.Panel();
            this.gb_waitingTimeDistribution = new System.Windows.Forms.GroupBox();
            this.referenceTimeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.p_waitingTimeDistribution = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.gb_FirstDistribution.SuspendLayout();
            this.gb_SecondDistribution.SuspendLayout();
            this.gb_waitingTimeDistribution.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_GroupID
            // 
            this.cb_GroupID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_GroupID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_GroupID.FormattingEnabled = true;
            this.cb_GroupID.Location = new System.Drawing.Point(84, 28);
            this.cb_GroupID.Name = "cb_GroupID";
            this.cb_GroupID.Size = new System.Drawing.Size(246, 21);
            this.cb_GroupID.TabIndex = 0;
            this.cb_GroupID.SelectedIndexChanged += new System.EventHandler(this.cb_GroupID_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(37, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Group";
            // 
            // gb_FirstDistribution
            // 
            this.gb_FirstDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_FirstDistribution.BackColor = System.Drawing.Color.Transparent;
            this.gb_FirstDistribution.Controls.Add(this.p_Distribution);
            this.gb_FirstDistribution.Location = new System.Drawing.Point(28, 61);
            this.gb_FirstDistribution.Name = "gb_FirstDistribution";
            this.gb_FirstDistribution.Size = new System.Drawing.Size(322, 54);
            this.gb_FirstDistribution.TabIndex = 13;
            this.gb_FirstDistribution.TabStop = false;
            this.gb_FirstDistribution.Text = "Distribution (s)";
            // 
            // p_Distribution
            // 
            this.p_Distribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.p_Distribution.Location = new System.Drawing.Point(6, 25);
            this.p_Distribution.Name = "p_Distribution";
            this.p_Distribution.Size = new System.Drawing.Size(310, 27);
            this.p_Distribution.TabIndex = 15;
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(150, 313);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(86, 32);
            this.btn_Ok.TabIndex = 14;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // gb_SecondDistribution
            // 
            this.gb_SecondDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_SecondDistribution.BackColor = System.Drawing.Color.Transparent;
            this.gb_SecondDistribution.Controls.Add(this.p_SecondDistribution);
            this.gb_SecondDistribution.Location = new System.Drawing.Point(28, 121);
            this.gb_SecondDistribution.Name = "gb_SecondDistribution";
            this.gb_SecondDistribution.Size = new System.Drawing.Size(322, 49);
            this.gb_SecondDistribution.TabIndex = 14;
            this.gb_SecondDistribution.TabStop = false;
            this.gb_SecondDistribution.Text = "Baggage distribution (s)";
            // 
            // p_SecondDistribution
            // 
            this.p_SecondDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.p_SecondDistribution.Location = new System.Drawing.Point(6, 20);
            this.p_SecondDistribution.Name = "p_SecondDistribution";
            this.p_SecondDistribution.Size = new System.Drawing.Size(310, 27);
            this.p_SecondDistribution.TabIndex = 16;
            // 
            // gb_waitingTimeDistribution
            // 
            this.gb_waitingTimeDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_waitingTimeDistribution.BackColor = System.Drawing.Color.Transparent;
            this.gb_waitingTimeDistribution.Controls.Add(this.referenceTimeComboBox);
            this.gb_waitingTimeDistribution.Controls.Add(this.label2);
            this.gb_waitingTimeDistribution.Controls.Add(this.p_waitingTimeDistribution);
            this.gb_waitingTimeDistribution.Location = new System.Drawing.Point(28, 203);
            this.gb_waitingTimeDistribution.Name = "gb_waitingTimeDistribution";
            this.gb_waitingTimeDistribution.Size = new System.Drawing.Size(322, 92);
            this.gb_waitingTimeDistribution.TabIndex = 15;
            this.gb_waitingTimeDistribution.TabStop = false;
            this.gb_waitingTimeDistribution.Text = "Waiting time before entering process group distribution (min)";
            // 
            // referenceTimeComboBox
            // 
            this.referenceTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.referenceTimeComboBox.FormattingEnabled = true;
            this.referenceTimeComboBox.Location = new System.Drawing.Point(122, 19);
            this.referenceTimeComboBox.Name = "referenceTimeComboBox";
            this.referenceTimeComboBox.Size = new System.Drawing.Size(96, 21);
            this.referenceTimeComboBox.TabIndex = 2;
            this.referenceTimeComboBox.SelectedIndexChanged += new System.EventHandler(this.referenceTimeComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Reference Time";
            // 
            // p_waitingTimeDistribution
            // 
            this.p_waitingTimeDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.p_waitingTimeDistribution.Location = new System.Drawing.Point(6, 46);
            this.p_waitingTimeDistribution.Name = "p_waitingTimeDistribution";
            this.p_waitingTimeDistribution.Size = new System.Drawing.Size(310, 27);
            this.p_waitingTimeDistribution.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(28, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 2);
            this.label3.TabIndex = 16;
            this.label3.Text = "label3";
            // 
            // Process_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 357);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gb_waitingTimeDistribution);
            this.Controls.Add(this.gb_SecondDistribution);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.gb_FirstDistribution);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_GroupID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Process_Assistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Processing times";
            this.gb_FirstDistribution.ResumeLayout(false);
            this.gb_SecondDistribution.ResumeLayout(false);
            this.gb_waitingTimeDistribution.ResumeLayout(false);
            this.gb_waitingTimeDistribution.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_GroupID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gb_FirstDistribution;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.GroupBox gb_SecondDistribution;
        private System.Windows.Forms.Panel p_Distribution;
        private System.Windows.Forms.Panel p_SecondDistribution;
        private System.Windows.Forms.GroupBox gb_waitingTimeDistribution;
        private System.Windows.Forms.Panel p_waitingTimeDistribution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox referenceTimeComboBox;
        private System.Windows.Forms.Label label3;
    }
}