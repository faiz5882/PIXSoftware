namespace SIMCORE_TOOL.Assistant.SubForms
{
    partial class Capacity_SubForm
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
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties2 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties3 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties4 = new PresentationControls.CheckBoxProperties();
            this.lbl_Type = new System.Windows.Forms.Label();
            this.lbl_Terminal = new System.Windows.Forms.Label();
            this.lbl_Group = new System.Windows.Forms.Label();
            this.lbl_GroupCapacity = new System.Windows.Forms.Label();
            this.cb_Type = new PresentationControls.CheckBoxComboBox();
            this.lbl_Station = new System.Windows.Forms.Label();
            this.lbl_StationCapacity = new System.Windows.Forms.Label();
            this.cb_Terminal = new PresentationControls.CheckBoxComboBox();
            this.cb_Group = new PresentationControls.CheckBoxComboBox();
            this.txt_GroupCapacity = new System.Windows.Forms.TextBox();
            this.cb_Stations = new PresentationControls.CheckBoxComboBox();
            this.txt_StationCapacity = new System.Windows.Forms.TextBox();
            this.gb_Group = new System.Windows.Forms.GroupBox();
            this.gb_Station = new System.Windows.Forms.GroupBox();
            this.stationProcessCapaTextBox = new System.Windows.Forms.TextBox();
            this.stationProcessCapaLabel = new System.Windows.Forms.Label();
            this.gb_Group.SuspendLayout();
            this.gb_Station.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Type
            // 
            this.lbl_Type.AutoSize = true;
            this.lbl_Type.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Type.Location = new System.Drawing.Point(18, 15);
            this.lbl_Type.Name = "lbl_Type";
            this.lbl_Type.Size = new System.Drawing.Size(31, 13);
            this.lbl_Type.TabIndex = 0;
            this.lbl_Type.Text = "Type";
            // 
            // lbl_Terminal
            // 
            this.lbl_Terminal.AutoSize = true;
            this.lbl_Terminal.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Terminal.Location = new System.Drawing.Point(18, 42);
            this.lbl_Terminal.Name = "lbl_Terminal";
            this.lbl_Terminal.Size = new System.Drawing.Size(47, 13);
            this.lbl_Terminal.TabIndex = 1;
            this.lbl_Terminal.Text = "Terminal";
            // 
            // lbl_Group
            // 
            this.lbl_Group.AutoSize = true;
            this.lbl_Group.Location = new System.Drawing.Point(6, 26);
            this.lbl_Group.Name = "lbl_Group";
            this.lbl_Group.Size = new System.Drawing.Size(36, 13);
            this.lbl_Group.TabIndex = 2;
            this.lbl_Group.Text = "Group";
            // 
            // lbl_GroupCapacity
            // 
            this.lbl_GroupCapacity.AutoSize = true;
            this.lbl_GroupCapacity.Location = new System.Drawing.Point(6, 53);
            this.lbl_GroupCapacity.Name = "lbl_GroupCapacity";
            this.lbl_GroupCapacity.Size = new System.Drawing.Size(79, 13);
            this.lbl_GroupCapacity.TabIndex = 3;
            this.lbl_GroupCapacity.Text = "Group capacity";
            // 
            // cb_Type
            // 
            this.cb_Type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties1.AutoSize = true;
            checkBoxProperties1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cb_Type.CheckBoxProperties = checkBoxProperties1;
            this.cb_Type.DisplayMemberSingleItem = "";
            this.cb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Type.FormattingEnabled = true;
            this.cb_Type.Location = new System.Drawing.Point(156, 12);
            this.cb_Type.Name = "cb_Type";
            this.cb_Type.Size = new System.Drawing.Size(113, 21);
            this.cb_Type.TabIndex = 0;
            this.cb_Type.SelectedIndexChanged += new System.EventHandler(this.cb_Type_SelectedIndexChanged);
            // 
            // lbl_Station
            // 
            this.lbl_Station.AutoSize = true;
            this.lbl_Station.Location = new System.Drawing.Point(6, 26);
            this.lbl_Station.Name = "lbl_Station";
            this.lbl_Station.Size = new System.Drawing.Size(45, 13);
            this.lbl_Station.TabIndex = 8;
            this.lbl_Station.Text = "Stations";
            // 
            // lbl_StationCapacity
            // 
            this.lbl_StationCapacity.AutoSize = true;
            this.lbl_StationCapacity.Location = new System.Drawing.Point(6, 53);
            this.lbl_StationCapacity.Name = "lbl_StationCapacity";
            this.lbl_StationCapacity.Size = new System.Drawing.Size(116, 13);
            this.lbl_StationCapacity.TabIndex = 9;
            this.lbl_StationCapacity.Text = "Station queue capacity";
            // 
            // cb_Terminal
            // 
            this.cb_Terminal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties2.AutoSize = true;
            checkBoxProperties2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cb_Terminal.CheckBoxProperties = checkBoxProperties2;
            this.cb_Terminal.DisplayMemberSingleItem = "";
            this.cb_Terminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Terminal.FormattingEnabled = true;
            this.cb_Terminal.Location = new System.Drawing.Point(156, 39);
            this.cb_Terminal.Name = "cb_Terminal";
            this.cb_Terminal.Size = new System.Drawing.Size(113, 21);
            this.cb_Terminal.TabIndex = 1;
            // 
            // cb_Group
            // 
            this.cb_Group.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties3.AutoSize = true;
            checkBoxProperties3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxProperties3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cb_Group.CheckBoxProperties = checkBoxProperties3;
            this.cb_Group.DisplayMemberSingleItem = "";
            this.cb_Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Group.FormattingEnabled = true;
            this.cb_Group.Location = new System.Drawing.Point(144, 23);
            this.cb_Group.Name = "cb_Group";
            this.cb_Group.Size = new System.Drawing.Size(113, 21);
            this.cb_Group.TabIndex = 3;
            this.cb_Group.SelectedIndexChanged += new System.EventHandler(this.cb_Group_SelectedIndexChanged);
            // 
            // txt_GroupCapacity
            // 
            this.txt_GroupCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_GroupCapacity.Location = new System.Drawing.Point(144, 50);
            this.txt_GroupCapacity.Name = "txt_GroupCapacity";
            this.txt_GroupCapacity.Size = new System.Drawing.Size(113, 20);
            this.txt_GroupCapacity.TabIndex = 4;
            this.txt_GroupCapacity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_GroupCapacity_KeyDown);
            this.txt_GroupCapacity.Validating += new System.ComponentModel.CancelEventHandler(this.txt_GroupCapacity_Validating);
            // 
            // cb_Stations
            // 
            this.cb_Stations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties4.AutoSize = true;
            checkBoxProperties4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxProperties4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cb_Stations.CheckBoxProperties = checkBoxProperties4;
            this.cb_Stations.DisplayMemberSingleItem = "";
            this.cb_Stations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Stations.FormattingEnabled = true;
            this.cb_Stations.Location = new System.Drawing.Point(144, 23);
            this.cb_Stations.Name = "cb_Stations";
            this.cb_Stations.Size = new System.Drawing.Size(113, 21);
            this.cb_Stations.TabIndex = 6;
            this.cb_Stations.SelectedIndexChanged += new System.EventHandler(this.cb_Stations_SelectedIndexChanged);
            // 
            // txt_StationCapacity
            // 
            this.txt_StationCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_StationCapacity.Location = new System.Drawing.Point(144, 50);
            this.txt_StationCapacity.Name = "txt_StationCapacity";
            this.txt_StationCapacity.Size = new System.Drawing.Size(113, 20);
            this.txt_StationCapacity.TabIndex = 7;
            this.txt_StationCapacity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_GroupCapacity_KeyDown);
            this.txt_StationCapacity.Validating += new System.ComponentModel.CancelEventHandler(this.txt_GroupCapacity_Validating);
            // 
            // gb_Group
            // 
            this.gb_Group.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Group.BackColor = System.Drawing.Color.Transparent;
            this.gb_Group.Controls.Add(this.lbl_Group);
            this.gb_Group.Controls.Add(this.lbl_GroupCapacity);
            this.gb_Group.Controls.Add(this.cb_Group);
            this.gb_Group.Controls.Add(this.txt_GroupCapacity);
            this.gb_Group.Location = new System.Drawing.Point(12, 66);
            this.gb_Group.Name = "gb_Group";
            this.gb_Group.Size = new System.Drawing.Size(269, 82);
            this.gb_Group.TabIndex = 2;
            this.gb_Group.TabStop = false;
            this.gb_Group.Text = "Groups information";
            // 
            // gb_Station
            // 
            this.gb_Station.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Station.BackColor = System.Drawing.Color.Transparent;
            this.gb_Station.Controls.Add(this.stationProcessCapaTextBox);
            this.gb_Station.Controls.Add(this.stationProcessCapaLabel);
            this.gb_Station.Controls.Add(this.lbl_Station);
            this.gb_Station.Controls.Add(this.lbl_StationCapacity);
            this.gb_Station.Controls.Add(this.txt_StationCapacity);
            this.gb_Station.Controls.Add(this.cb_Stations);
            this.gb_Station.Location = new System.Drawing.Point(12, 154);
            this.gb_Station.Name = "gb_Station";
            this.gb_Station.Size = new System.Drawing.Size(269, 106);
            this.gb_Station.TabIndex = 5;
            this.gb_Station.TabStop = false;
            this.gb_Station.Text = "Station information";
            // 
            // stationProcessCapaTextBox
            // 
            this.stationProcessCapaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stationProcessCapaTextBox.Location = new System.Drawing.Point(145, 80);
            this.stationProcessCapaTextBox.Name = "stationProcessCapaTextBox";
            this.stationProcessCapaTextBox.Size = new System.Drawing.Size(112, 20);
            this.stationProcessCapaTextBox.TabIndex = 11;
            this.stationProcessCapaTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_GroupCapacity_KeyDown);
            this.stationProcessCapaTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.txt_GroupCapacity_Validating);
            // 
            // stationProcessCapaLabel
            // 
            this.stationProcessCapaLabel.AutoSize = true;
            this.stationProcessCapaLabel.Location = new System.Drawing.Point(6, 83);
            this.stationProcessCapaLabel.Name = "stationProcessCapaLabel";
            this.stationProcessCapaLabel.Size = new System.Drawing.Size(123, 13);
            this.stationProcessCapaLabel.TabIndex = 10;
            this.stationProcessCapaLabel.Text = "Station process capacity";
            // 
            // Capacity_SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 269);
            this.Controls.Add(this.lbl_Type);
            this.Controls.Add(this.gb_Station);
            this.Controls.Add(this.lbl_Terminal);
            this.Controls.Add(this.gb_Group);
            this.Controls.Add(this.cb_Type);
            this.Controls.Add(this.cb_Terminal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(294, 269);
            this.Name = "Capacity_SubForm";
            this.Text = "Capacity_SubForm";
            this.Validating += new System.ComponentModel.CancelEventHandler(this.Capacity_SubForm_Validating);
            this.gb_Group.ResumeLayout(false);
            this.gb_Group.PerformLayout();
            this.gb_Station.ResumeLayout(false);
            this.gb_Station.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Type;
        private System.Windows.Forms.Label lbl_Terminal;
        private System.Windows.Forms.Label lbl_Group;
        private System.Windows.Forms.Label lbl_GroupCapacity;
        private PresentationControls.CheckBoxComboBox cb_Type;
        private System.Windows.Forms.Label lbl_Station;
        private System.Windows.Forms.Label lbl_StationCapacity;
        private PresentationControls.CheckBoxComboBox cb_Terminal;
        private PresentationControls.CheckBoxComboBox cb_Group;
        private System.Windows.Forms.TextBox txt_GroupCapacity;
        private PresentationControls.CheckBoxComboBox cb_Stations;
        private System.Windows.Forms.TextBox txt_StationCapacity;
        private System.Windows.Forms.GroupBox gb_Group;
        private System.Windows.Forms.GroupBox gb_Station;
        private System.Windows.Forms.Label stationProcessCapaLabel;
        private System.Windows.Forms.TextBox stationProcessCapaTextBox;
    }
}