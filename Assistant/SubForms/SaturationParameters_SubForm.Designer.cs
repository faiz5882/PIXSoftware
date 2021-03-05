namespace SIMCORE_TOOL.Assistant.SubForms
{
    partial class SaturationParameters_SubForm
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
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties2 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties3 = new PresentationControls.CheckBoxProperties();
            this.cb_groupType = new PresentationControls.CheckBoxComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_terminal = new PresentationControls.CheckBoxComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gb_saturationParameters = new System.Windows.Forms.GroupBox();
            this.tb_reactionTime = new System.Windows.Forms.TextBox();
            this.tb_accumulation = new System.Windows.Forms.TextBox();
            this.tb_percentOpened = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gb_queueManagement = new System.Windows.Forms.GroupBox();
            this.rb_randomStation = new System.Windows.Forms.RadioButton();
            this.rb_saturateStation = new System.Windows.Forms.RadioButton();
            this.rb_firstStation = new System.Windows.Forms.RadioButton();
            this.cb_group = new PresentationControls.CheckBoxComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_applySaturationRules = new System.Windows.Forms.CheckBox();
            this.gb_saturationParameters.SuspendLayout();
            this.gb_queueManagement.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_groupType
            // 
            this.cb_groupType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties1.AutoSize = true;
            checkBoxProperties1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cb_groupType.CheckBoxProperties = checkBoxProperties1;
            this.cb_groupType.DisplayMemberSingleItem = "";
            this.cb_groupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_groupType.FormattingEnabled = true;
            this.cb_groupType.Location = new System.Drawing.Point(157, 26);
            this.cb_groupType.Name = "cb_groupType";
            this.cb_groupType.Size = new System.Drawing.Size(226, 21);
            this.cb_groupType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Group Type";
            // 
            // cb_terminal
            // 
            this.cb_terminal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties2.AutoSize = true;
            checkBoxProperties2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cb_terminal.CheckBoxProperties = checkBoxProperties2;
            this.cb_terminal.DisplayMemberSingleItem = "";
            this.cb_terminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_terminal.FormattingEnabled = true;
            this.cb_terminal.Location = new System.Drawing.Point(157, 54);
            this.cb_terminal.Name = "cb_terminal";
            this.cb_terminal.Size = new System.Drawing.Size(226, 21);
            this.cb_terminal.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(30, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Terminal";
            // 
            // gb_saturationParameters
            // 
            this.gb_saturationParameters.BackColor = System.Drawing.Color.Transparent;
            this.gb_saturationParameters.Controls.Add(this.tb_reactionTime);
            this.gb_saturationParameters.Controls.Add(this.tb_accumulation);
            this.gb_saturationParameters.Controls.Add(this.tb_percentOpened);
            this.gb_saturationParameters.Controls.Add(this.label6);
            this.gb_saturationParameters.Controls.Add(this.label5);
            this.gb_saturationParameters.Controls.Add(this.label4);
            this.gb_saturationParameters.Location = new System.Drawing.Point(12, 156);
            this.gb_saturationParameters.Name = "gb_saturationParameters";
            this.gb_saturationParameters.Size = new System.Drawing.Size(186, 139);
            this.gb_saturationParameters.TabIndex = 5;
            this.gb_saturationParameters.TabStop = false;
            this.gb_saturationParameters.Text = "Saturation Parameters";
            // 
            // tb_reactionTime
            // 
            this.tb_reactionTime.Location = new System.Drawing.Point(123, 92);
            this.tb_reactionTime.Name = "tb_reactionTime";
            this.tb_reactionTime.Size = new System.Drawing.Size(32, 20);
            this.tb_reactionTime.TabIndex = 7;
            // 
            // tb_accumulation
            // 
            this.tb_accumulation.Location = new System.Drawing.Point(123, 60);
            this.tb_accumulation.Name = "tb_accumulation";
            this.tb_accumulation.Size = new System.Drawing.Size(32, 20);
            this.tb_accumulation.TabIndex = 6;
            // 
            // tb_percentOpened
            // 
            this.tb_percentOpened.Location = new System.Drawing.Point(123, 28);
            this.tb_percentOpened.Name = "tb_percentOpened";
            this.tb_percentOpened.Size = new System.Drawing.Size(32, 20);
            this.tb_percentOpened.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Reaction Time (min)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Accumulation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Percent Opened";
            // 
            // gb_queueManagement
            // 
            this.gb_queueManagement.BackColor = System.Drawing.Color.Transparent;
            this.gb_queueManagement.Controls.Add(this.rb_randomStation);
            this.gb_queueManagement.Controls.Add(this.rb_saturateStation);
            this.gb_queueManagement.Controls.Add(this.rb_firstStation);
            this.gb_queueManagement.Location = new System.Drawing.Point(216, 156);
            this.gb_queueManagement.Name = "gb_queueManagement";
            this.gb_queueManagement.Size = new System.Drawing.Size(178, 139);
            this.gb_queueManagement.TabIndex = 6;
            this.gb_queueManagement.TabStop = false;
            this.gb_queueManagement.Text = "Select Queue Management";
            // 
            // rb_randomStation
            // 
            this.rb_randomStation.AutoSize = true;
            this.rb_randomStation.Location = new System.Drawing.Point(13, 93);
            this.rb_randomStation.Name = "rb_randomStation";
            this.rb_randomStation.Size = new System.Drawing.Size(99, 17);
            this.rb_randomStation.TabIndex = 10;
            this.rb_randomStation.TabStop = true;
            this.rb_randomStation.Text = "Random station";
            this.rb_randomStation.UseVisualStyleBackColor = true;
            // 
            // rb_saturateStation
            // 
            this.rb_saturateStation.AutoSize = true;
            this.rb_saturateStation.Location = new System.Drawing.Point(13, 61);
            this.rb_saturateStation.Name = "rb_saturateStation";
            this.rb_saturateStation.Size = new System.Drawing.Size(99, 17);
            this.rb_saturateStation.TabIndex = 9;
            this.rb_saturateStation.TabStop = true;
            this.rb_saturateStation.Text = "Saturate station";
            this.rb_saturateStation.UseVisualStyleBackColor = true;
            // 
            // rb_firstStation
            // 
            this.rb_firstStation.AutoSize = true;
            this.rb_firstStation.Location = new System.Drawing.Point(13, 27);
            this.rb_firstStation.Name = "rb_firstStation";
            this.rb_firstStation.Size = new System.Drawing.Size(78, 17);
            this.rb_firstStation.TabIndex = 8;
            this.rb_firstStation.TabStop = true;
            this.rb_firstStation.Text = "First station";
            this.rb_firstStation.UseVisualStyleBackColor = true;
            // 
            // cb_group
            // 
            this.cb_group.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties3.AutoSize = true;
            checkBoxProperties3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxProperties3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cb_group.CheckBoxProperties = checkBoxProperties3;
            this.cb_group.DisplayMemberSingleItem = "";
            this.cb_group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_group.FormattingEnabled = true;
            this.cb_group.Location = new System.Drawing.Point(157, 81);
            this.cb_group.Name = "cb_group";
            this.cb_group.Size = new System.Drawing.Size(226, 21);
            this.cb_group.TabIndex = 3;
            this.cb_group.SelectedIndexChanged += new System.EventHandler(this.setUpParametersValues);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(30, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Group";
            // 
            // cb_applySaturationRules
            // 
            this.cb_applySaturationRules.AutoSize = true;
            this.cb_applySaturationRules.BackColor = System.Drawing.Color.Transparent;
            this.cb_applySaturationRules.Checked = true;
            this.cb_applySaturationRules.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_applySaturationRules.Location = new System.Drawing.Point(113, 122);
            this.cb_applySaturationRules.Name = "cb_applySaturationRules";
            this.cb_applySaturationRules.Size = new System.Drawing.Size(151, 17);
            this.cb_applySaturationRules.TabIndex = 4;
            this.cb_applySaturationRules.Text = "Apply local saturation rules";
            this.cb_applySaturationRules.UseVisualStyleBackColor = false;
            this.cb_applySaturationRules.CheckedChanged += new System.EventHandler(this.cb_applySaturationRules_CheckedChanged);
            // 
            // SaturationParameters_SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 308);
            this.Controls.Add(this.cb_applySaturationRules);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_group);
            this.Controls.Add(this.gb_queueManagement);
            this.Controls.Add(this.gb_saturationParameters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_terminal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_groupType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(407, 308);
            this.Name = "SaturationParameters_SubForm";
            this.Text = "SaturationParameters_SubForm";
            this.gb_saturationParameters.ResumeLayout(false);
            this.gb_saturationParameters.PerformLayout();
            this.gb_queueManagement.ResumeLayout(false);
            this.gb_queueManagement.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PresentationControls.CheckBoxComboBox cb_groupType;
        private System.Windows.Forms.Label label1;
        private PresentationControls.CheckBoxComboBox cb_terminal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gb_saturationParameters;
        private System.Windows.Forms.GroupBox gb_queueManagement;
        private PresentationControls.CheckBoxComboBox cb_group;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_reactionTime;
        private System.Windows.Forms.TextBox tb_accumulation;
        private System.Windows.Forms.TextBox tb_percentOpened;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cb_applySaturationRules;
        private System.Windows.Forms.RadioButton rb_randomStation;
        private System.Windows.Forms.RadioButton rb_saturateStation;
        private System.Windows.Forms.RadioButton rb_firstStation;
    }
}