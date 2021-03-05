namespace SIMCORE_TOOL.Prompt.Dubai
{
    partial class FlightPlanImportAssistantForDubai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightPlanImportAssistantForDubai));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFlightPlanFileDialogButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flightPlanComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileDialogElement = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.openFlightPlanFileDialogButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.flightPlanComboBox);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.MinimumSize = new System.Drawing.Size(500, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Flight Plan Import";
            // 
            // openFlightPlanFileDialogButton
            // 
            this.openFlightPlanFileDialogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openFlightPlanFileDialogButton.Location = new System.Drawing.Point(492, 19);
            this.openFlightPlanFileDialogButton.Name = "openFlightPlanFileDialogButton";
            this.openFlightPlanFileDialogButton.Size = new System.Drawing.Size(32, 23);
            this.openFlightPlanFileDialogButton.TabIndex = 2;
            this.openFlightPlanFileDialogButton.Text = "...";
            this.openFlightPlanFileDialogButton.UseVisualStyleBackColor = true;
            this.openFlightPlanFileDialogButton.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Flight Plan";
            // 
            // flightPlanComboBox
            // 
            this.flightPlanComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flightPlanComboBox.FormattingEnabled = true;
            this.flightPlanComboBox.Location = new System.Drawing.Point(115, 21);
            this.flightPlanComboBox.Name = "flightPlanComboBox";
            this.flightPlanComboBox.Size = new System.Drawing.Size(361, 21);
            this.flightPlanComboBox.TabIndex = 0;
            this.flightPlanComboBox.Tag = "FlightPlan.txt";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(129, 80);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(322, 80);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialogElement
            // 
            this.openFileDialogElement.FileName = "openFileDialog1";
            this.openFileDialogElement.Filter = "All file | *.*";
            // 
            // FlightPlanImportAssistantForDubai
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(547, 115);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlightPlanImportAssistantForDubai";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Flight Plan Import Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlightPlanImportAssistantForDubai_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button openFlightPlanFileDialogButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox flightPlanComboBox;
        private System.Windows.Forms.OpenFileDialog openFileDialogElement;
    }
}