namespace SIMCORE_TOOL.Assistant
{
    partial class OCT_AssistantMakeUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OCT_AssistantMakeUp));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_FC = new System.Windows.Forms.ComboBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_CANCEL = new System.Windows.Forms.Button();
            this.lbl_First = new System.Windows.Forms.Label();
            this.lbl_Second = new System.Windows.Forms.Label();
            this.txt_FirstValue = new System.Windows.Forms.TextBox();
            this.txt_SecondValue = new System.Windows.Forms.TextBox();
            this.gb_Parameters = new System.Windows.Forms.GroupBox();
            this.txt_Partialopening = new System.Windows.Forms.TextBox();
            this.lbl_PartialTime = new System.Windows.Forms.Label();
            this.gb_Delivery = new System.Windows.Forms.GroupBox();
            this.txt_EBS_Delivery = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gb_AllocatedMakeUp = new System.Windows.Forms.GroupBox();
            this.txt_NumberPartial = new System.Windows.Forms.TextBox();
            this.lbl_PartialNumber = new System.Windows.Forms.Label();
            this.txt_Allocated = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gb_Segregations = new System.Windows.Forms.GroupBox();
            this.txt_Segregation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gb_Containers = new System.Windows.Forms.GroupBox();
            this.txt_lateTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_ContainerPerLateral = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ContainerDeadTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_ContainerSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gb_Parameters.SuspendLayout();
            this.gb_Delivery.SuspendLayout();
            this.gb_AllocatedMakeUp.SuspendLayout();
            this.gb_Segregations.SuspendLayout();
            this.gb_Containers.SuspendLayout();
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
            this.cb_FC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FC.FormattingEnabled = true;
            resources.ApplyResources(this.cb_FC, "cb_FC");
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
            this.txt_FirstValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_FirstValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_FirstValue, "txt_FirstValue");
            this.txt_FirstValue.Name = "txt_FirstValue";
            this.txt_FirstValue.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            this.txt_FirstValue.Leave += new System.EventHandler(this.txt_FirstValue_Leave);
            // 
            // txt_SecondValue
            // 
            this.txt_SecondValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_SecondValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_SecondValue, "txt_SecondValue");
            this.txt_SecondValue.Name = "txt_SecondValue";
            this.txt_SecondValue.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            this.txt_SecondValue.Leave += new System.EventHandler(this.txt_FirstValue_Leave);
            // 
            // gb_Parameters
            // 
            this.gb_Parameters.BackColor = System.Drawing.Color.Transparent;
            this.gb_Parameters.Controls.Add(this.txt_Partialopening);
            this.gb_Parameters.Controls.Add(this.lbl_PartialTime);
            this.gb_Parameters.Controls.Add(this.txt_FirstValue);
            this.gb_Parameters.Controls.Add(this.txt_SecondValue);
            this.gb_Parameters.Controls.Add(this.lbl_First);
            this.gb_Parameters.Controls.Add(this.lbl_Second);
            resources.ApplyResources(this.gb_Parameters, "gb_Parameters");
            this.gb_Parameters.Name = "gb_Parameters";
            this.gb_Parameters.TabStop = false;
            this.gb_Parameters.Enter += new System.EventHandler(this.gb_Parameters_Enter);
            // 
            // txt_Partialopening
            // 
            this.txt_Partialopening.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Partialopening.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_Partialopening, "txt_Partialopening");
            this.txt_Partialopening.Name = "txt_Partialopening";
            this.txt_Partialopening.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            this.txt_Partialopening.Leave += new System.EventHandler(this.txt_FirstValue_Leave);
            // 
            // lbl_PartialTime
            // 
            resources.ApplyResources(this.lbl_PartialTime, "lbl_PartialTime");
            this.lbl_PartialTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PartialTime.Name = "lbl_PartialTime";
            // 
            // gb_Delivery
            // 
            this.gb_Delivery.BackColor = System.Drawing.Color.Transparent;
            this.gb_Delivery.Controls.Add(this.txt_EBS_Delivery);
            this.gb_Delivery.Controls.Add(this.label2);
            resources.ApplyResources(this.gb_Delivery, "gb_Delivery");
            this.gb_Delivery.Name = "gb_Delivery";
            this.gb_Delivery.TabStop = false;
            // 
            // txt_EBS_Delivery
            // 
            this.txt_EBS_Delivery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_EBS_Delivery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_EBS_Delivery, "txt_EBS_Delivery");
            this.txt_EBS_Delivery.Name = "txt_EBS_Delivery";
            this.txt_EBS_Delivery.Tag = "EBS delivery time (Min before STD)";
            this.txt_EBS_Delivery.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            this.txt_EBS_Delivery.Leave += new System.EventHandler(this.txt_FirstValue_Leave);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // gb_AllocatedMakeUp
            // 
            this.gb_AllocatedMakeUp.BackColor = System.Drawing.Color.Transparent;
            this.gb_AllocatedMakeUp.Controls.Add(this.txt_NumberPartial);
            this.gb_AllocatedMakeUp.Controls.Add(this.lbl_PartialNumber);
            this.gb_AllocatedMakeUp.Controls.Add(this.txt_Allocated);
            this.gb_AllocatedMakeUp.Controls.Add(this.label3);
            resources.ApplyResources(this.gb_AllocatedMakeUp, "gb_AllocatedMakeUp");
            this.gb_AllocatedMakeUp.Name = "gb_AllocatedMakeUp";
            this.gb_AllocatedMakeUp.TabStop = false;
            // 
            // txt_NumberPartial
            // 
            this.txt_NumberPartial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_NumberPartial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_NumberPartial, "txt_NumberPartial");
            this.txt_NumberPartial.Name = "txt_NumberPartial";
            this.txt_NumberPartial.Tag = "Number of Opened Make-Up at Partial Opening/Closing";
            this.txt_NumberPartial.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // lbl_PartialNumber
            // 
            resources.ApplyResources(this.lbl_PartialNumber, "lbl_PartialNumber");
            this.lbl_PartialNumber.Name = "lbl_PartialNumber";
            // 
            // txt_Allocated
            // 
            this.txt_Allocated.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Allocated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_Allocated, "txt_Allocated");
            this.txt_Allocated.Name = "txt_Allocated";
            this.txt_Allocated.Tag = "Number of allocated Make-Up per flight";
            this.txt_Allocated.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // gb_Segregations
            // 
            this.gb_Segregations.BackColor = System.Drawing.Color.Transparent;
            this.gb_Segregations.Controls.Add(this.txt_Segregation);
            this.gb_Segregations.Controls.Add(this.label4);
            resources.ApplyResources(this.gb_Segregations, "gb_Segregations");
            this.gb_Segregations.Name = "gb_Segregations";
            this.gb_Segregations.TabStop = false;
            // 
            // txt_Segregation
            // 
            this.txt_Segregation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Segregation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_Segregation, "txt_Segregation");
            this.txt_Segregation.Name = "txt_Segregation";
            this.txt_Segregation.Tag = "Segregation number";
            this.txt_Segregation.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // gb_Containers
            // 
            this.gb_Containers.BackColor = System.Drawing.Color.Transparent;
            this.gb_Containers.Controls.Add(this.txt_lateTime);
            this.gb_Containers.Controls.Add(this.label8);
            this.gb_Containers.Controls.Add(this.txt_ContainerPerLateral);
            this.gb_Containers.Controls.Add(this.label7);
            this.gb_Containers.Controls.Add(this.txt_ContainerDeadTime);
            this.gb_Containers.Controls.Add(this.label6);
            this.gb_Containers.Controls.Add(this.txt_ContainerSize);
            this.gb_Containers.Controls.Add(this.label5);
            resources.ApplyResources(this.gb_Containers, "gb_Containers");
            this.gb_Containers.Name = "gb_Containers";
            this.gb_Containers.TabStop = false;
            // 
            // txt_lateTime
            // 
            this.txt_lateTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_lateTime.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.txt_lateTime, "txt_lateTime");
            this.txt_lateTime.Name = "txt_lateTime";
            this.txt_lateTime.Tag = "Make-Up Late Time (Min before STD)";
            this.txt_lateTime.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txt_ContainerPerLateral
            // 
            this.txt_ContainerPerLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_ContainerPerLateral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_ContainerPerLateral, "txt_ContainerPerLateral");
            this.txt_ContainerPerLateral.Name = "txt_ContainerPerLateral";
            this.txt_ContainerPerLateral.Tag = "Number of container per Lateral";
            this.txt_ContainerPerLateral.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txt_ContainerDeadTime
            // 
            this.txt_ContainerDeadTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_ContainerDeadTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_ContainerDeadTime, "txt_ContainerDeadTime");
            this.txt_ContainerDeadTime.Name = "txt_ContainerDeadTime";
            this.txt_ContainerDeadTime.Tag = "DeadTime (Time to change the container) (s)";
            this.txt_ContainerDeadTime.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txt_ContainerSize
            // 
            this.txt_ContainerSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_ContainerSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_ContainerSize, "txt_ContainerSize");
            this.txt_ContainerSize.Name = "txt_ContainerSize";
            this.txt_ContainerSize.Tag = "Container size";
            this.txt_ContainerSize.TextChanged += new System.EventHandler(this.txt_FirstValue_TextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // OCT_AssistantMakeUp
            // 
            this.AcceptButton = this.BT_OK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BT_CANCEL;
            this.Controls.Add(this.gb_Containers);
            this.Controls.Add(this.gb_Segregations);
            this.Controls.Add(this.gb_AllocatedMakeUp);
            this.Controls.Add(this.gb_Delivery);
            this.Controls.Add(this.gb_Parameters);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_CANCEL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_FC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OCT_AssistantMakeUp";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.OCT_AssistantMakeUp_Load);
            this.gb_Parameters.ResumeLayout(false);
            this.gb_Parameters.PerformLayout();
            this.gb_Delivery.ResumeLayout(false);
            this.gb_Delivery.PerformLayout();
            this.gb_AllocatedMakeUp.ResumeLayout(false);
            this.gb_AllocatedMakeUp.PerformLayout();
            this.gb_Segregations.ResumeLayout(false);
            this.gb_Segregations.PerformLayout();
            this.gb_Containers.ResumeLayout(false);
            this.gb_Containers.PerformLayout();
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
        private System.Windows.Forms.GroupBox gb_Delivery;
        private System.Windows.Forms.TextBox txt_EBS_Delivery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gb_AllocatedMakeUp;
        private System.Windows.Forms.TextBox txt_Allocated;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gb_Segregations;
        private System.Windows.Forms.TextBox txt_Segregation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gb_Containers;
        private System.Windows.Forms.TextBox txt_ContainerSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_ContainerDeadTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_ContainerPerLateral;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Partialopening;
        private System.Windows.Forms.Label lbl_PartialTime;
        private System.Windows.Forms.TextBox txt_NumberPartial;
        private System.Windows.Forms.Label lbl_PartialNumber;
        private System.Windows.Forms.TextBox txt_lateTime;
        private System.Windows.Forms.Label label8;
    }
}