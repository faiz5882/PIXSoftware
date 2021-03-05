namespace SIMCORE_TOOL.Prompt
{
    partial class SIM_Define_Step_Analysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIM_Define_Step_Analysis));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cb_AirportFlow = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_PassengerDistrib = new System.Windows.Forms.ComboBox();
            this.cb_PassengerDistributionLines = new System.Windows.Forms.ComboBox();
            this.cb_Occupation = new System.Windows.Forms.ComboBox();
            this.cb_Utilization = new System.Windows.Forms.ComboBox();
            this.cb_Remaining = new System.Windows.Forms.ComboBox();
            this.cb_RemainingLines = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(38, 216);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "&Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(239, 216);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // cb_AirportFlow
            // 
            this.cb_AirportFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AirportFlow.FormattingEnabled = true;
            this.cb_AirportFlow.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.cb_AirportFlow.Location = new System.Drawing.Point(184, 29);
            this.cb_AirportFlow.Name = "cb_AirportFlow";
            this.cb_AirportFlow.Size = new System.Drawing.Size(49, 21);
            this.cb_AirportFlow.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(45, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Airport flow :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(45, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Passenger distribution :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(45, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Occupation :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(45, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Utilization :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(45, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Remaining times :";
            // 
            // cb_PassengerDistrib
            // 
            this.cb_PassengerDistrib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PassengerDistrib.FormattingEnabled = true;
            this.cb_PassengerDistrib.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.cb_PassengerDistrib.Location = new System.Drawing.Point(184, 56);
            this.cb_PassengerDistrib.Name = "cb_PassengerDistrib";
            this.cb_PassengerDistrib.Size = new System.Drawing.Size(49, 21);
            this.cb_PassengerDistrib.TabIndex = 36;
            // 
            // cb_PassengerDistributionLines
            // 
            this.cb_PassengerDistributionLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PassengerDistributionLines.FormattingEnabled = true;
            this.cb_PassengerDistributionLines.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20"});
            this.cb_PassengerDistributionLines.Location = new System.Drawing.Point(239, 56);
            this.cb_PassengerDistributionLines.Name = "cb_PassengerDistributionLines";
            this.cb_PassengerDistributionLines.Size = new System.Drawing.Size(49, 21);
            this.cb_PassengerDistributionLines.TabIndex = 37;
            // 
            // cb_Occupation
            // 
            this.cb_Occupation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Occupation.FormattingEnabled = true;
            this.cb_Occupation.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.cb_Occupation.Location = new System.Drawing.Point(184, 83);
            this.cb_Occupation.Name = "cb_Occupation";
            this.cb_Occupation.Size = new System.Drawing.Size(49, 21);
            this.cb_Occupation.TabIndex = 38;
            // 
            // cb_Utilization
            // 
            this.cb_Utilization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Utilization.FormattingEnabled = true;
            this.cb_Utilization.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.cb_Utilization.Location = new System.Drawing.Point(184, 110);
            this.cb_Utilization.Name = "cb_Utilization";
            this.cb_Utilization.Size = new System.Drawing.Size(49, 21);
            this.cb_Utilization.TabIndex = 39;
            // 
            // cb_Remaining
            // 
            this.cb_Remaining.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Remaining.FormattingEnabled = true;
            this.cb_Remaining.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.cb_Remaining.Location = new System.Drawing.Point(184, 137);
            this.cb_Remaining.Name = "cb_Remaining";
            this.cb_Remaining.Size = new System.Drawing.Size(49, 21);
            this.cb_Remaining.TabIndex = 40;
            // 
            // cb_RemainingLines
            // 
            this.cb_RemainingLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_RemainingLines.FormattingEnabled = true;
            this.cb_RemainingLines.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20"});
            this.cb_RemainingLines.Location = new System.Drawing.Point(239, 137);
            this.cb_RemainingLines.Name = "cb_RemainingLines";
            this.cb_RemainingLines.Size = new System.Drawing.Size(49, 21);
            this.cb_RemainingLines.TabIndex = 41;
            // 
            // SIM_Define_Step_Analysis
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(369, 263);
            this.Controls.Add(this.cb_RemainingLines);
            this.Controls.Add(this.cb_Remaining);
            this.Controls.Add(this.cb_Utilization);
            this.Controls.Add(this.cb_Occupation);
            this.Controls.Add(this.cb_PassengerDistributionLines);
            this.Controls.Add(this.cb_PassengerDistrib);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_AirportFlow);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SIM_Define_Step_Analysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Define the analysis steps";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cb_AirportFlow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_PassengerDistrib;
        private System.Windows.Forms.ComboBox cb_PassengerDistributionLines;
        private System.Windows.Forms.ComboBox cb_Occupation;
        private System.Windows.Forms.ComboBox cb_Utilization;
        private System.Windows.Forms.ComboBox cb_Remaining;
        private System.Windows.Forms.ComboBox cb_RemainingLines;
    }
}