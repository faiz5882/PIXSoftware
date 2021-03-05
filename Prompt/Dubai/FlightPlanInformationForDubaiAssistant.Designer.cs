namespace SIMCORE_TOOL.Prompt.Dubai
{
    partial class FlightPlanInformationForDubaiAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightPlanInformationForDubaiAssistant));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFilteredFlightPlanFileDialog = new System.Windows.Forms.Button();
            this.filteredFlightPlanComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openCplexAllocationFileDialog = new System.Windows.Forms.Button();
            this.cplexAllocationFileComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileDialogElement = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.partialAllocationComboBox = new System.Windows.Forms.ComboBox();
            this.openPartialAllocationFileDialog = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.openFilteredFlightPlanFileDialog);
            this.groupBox1.Controls.Add(this.filteredFlightPlanComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Flight Plan";
            // 
            // openFilteredFlightPlanFileDialog
            // 
            this.openFilteredFlightPlanFileDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openFilteredFlightPlanFileDialog.Location = new System.Drawing.Point(529, 19);
            this.openFilteredFlightPlanFileDialog.Name = "openFilteredFlightPlanFileDialog";
            this.openFilteredFlightPlanFileDialog.Size = new System.Drawing.Size(32, 23);
            this.openFilteredFlightPlanFileDialog.TabIndex = 2;
            this.openFilteredFlightPlanFileDialog.Text = "...";
            this.openFilteredFlightPlanFileDialog.UseVisualStyleBackColor = true;
            this.openFilteredFlightPlanFileDialog.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // filteredFlightPlanComboBox
            // 
            this.filteredFlightPlanComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filteredFlightPlanComboBox.FormattingEnabled = true;
            this.filteredFlightPlanComboBox.Location = new System.Drawing.Point(185, 21);
            this.filteredFlightPlanComboBox.Name = "filteredFlightPlanComboBox";
            this.filteredFlightPlanComboBox.Size = new System.Drawing.Size(327, 21);
            this.filteredFlightPlanComboBox.TabIndex = 1;
            this.filteredFlightPlanComboBox.Tag = "FilteredFlightPlan.txt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtered Departure Flight Plan :";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.openCplexAllocationFileDialog);
            this.groupBox2.Controls.Add(this.cplexAllocationFileComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 61);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Allocation Results";
            // 
            // openCplexAllocationFileDialog
            // 
            this.openCplexAllocationFileDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openCplexAllocationFileDialog.Location = new System.Drawing.Point(529, 19);
            this.openCplexAllocationFileDialog.Name = "openCplexAllocationFileDialog";
            this.openCplexAllocationFileDialog.Size = new System.Drawing.Size(32, 23);
            this.openCplexAllocationFileDialog.TabIndex = 2;
            this.openCplexAllocationFileDialog.Text = "...";
            this.openCplexAllocationFileDialog.UseVisualStyleBackColor = true;
            this.openCplexAllocationFileDialog.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // cplexAllocationFileComboBox
            // 
            this.cplexAllocationFileComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cplexAllocationFileComboBox.FormattingEnabled = true;
            this.cplexAllocationFileComboBox.Location = new System.Drawing.Point(185, 21);
            this.cplexAllocationFileComboBox.Name = "cplexAllocationFileComboBox";
            this.cplexAllocationFileComboBox.Size = new System.Drawing.Size(327, 21);
            this.cplexAllocationFileComboBox.TabIndex = 1;
            this.cplexAllocationFileComboBox.Tag = "CplexAllocation.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Allocation Results File :";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(129, 213);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(367, 213);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialogElement
            // 
            this.openFileDialogElement.FileName = "openFileDialog1";
            this.openFileDialogElement.Filter = "All file | *.*";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.openPartialAllocationFileDialog);
            this.groupBox3.Controls.Add(this.partialAllocationComboBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(572, 60);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Partial Allocation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Partial Allocation File (BIN2 flights) :";
            // 
            // partialAllocationComboBox
            // 
            this.partialAllocationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.partialAllocationComboBox.FormattingEnabled = true;
            this.partialAllocationComboBox.Location = new System.Drawing.Point(185, 19);
            this.partialAllocationComboBox.Name = "partialAllocationComboBox";
            this.partialAllocationComboBox.Size = new System.Drawing.Size(327, 21);
            this.partialAllocationComboBox.TabIndex = 1;
            this.partialAllocationComboBox.Tag = "PartialAllocationBIN2.txt";
            // 
            // openPartialAllocationFileDialog
            // 
            this.openPartialAllocationFileDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openPartialAllocationFileDialog.BackColor = System.Drawing.Color.Transparent;
            this.openPartialAllocationFileDialog.Location = new System.Drawing.Point(529, 17);
            this.openPartialAllocationFileDialog.Name = "openPartialAllocationFileDialog";
            this.openPartialAllocationFileDialog.Size = new System.Drawing.Size(32, 23);
            this.openPartialAllocationFileDialog.TabIndex = 2;
            this.openPartialAllocationFileDialog.Text = "...";
            this.openPartialAllocationFileDialog.UseVisualStyleBackColor = false;
            this.openPartialAllocationFileDialog.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // FlightPlanInformationForDubaiAssistant
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(596, 248);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlightPlanInformationForDubaiAssistant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Flight Plan Information For Dubai Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlightPlanInformationAssistant_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button openFilteredFlightPlanFileDialog;
        private System.Windows.Forms.ComboBox filteredFlightPlanComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openCplexAllocationFileDialog;
        private System.Windows.Forms.ComboBox cplexAllocationFileComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.OpenFileDialog openFileDialogElement;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button openPartialAllocationFileDialog;
        private System.Windows.Forms.ComboBox partialAllocationComboBox;
        private System.Windows.Forms.Label label3;
    }
}