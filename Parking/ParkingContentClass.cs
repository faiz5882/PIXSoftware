using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SIMCORE_TOOL.Parking
{
    class ParkingContentClass
    {
        private List<TextBox> ltbDistributionDuringRT;
        private List<TextBox> ltbOccupation;
        private List<Label> ltlDistributionDuringRT;
        private List<Label> ltlOccupation;
        #region Fonction for the recalcs of the controls values when a value is changed in the currents textboxes
        private ParkingCalcs.UpdateCalcsDelegate ucdCalcDelegate;
        internal ParkingCalcs.UpdateCalcsDelegate CalcDelegate
        {
            get
            {
                return ucdCalcDelegate;
            }
            set
            {
                ucdCalcDelegate = value;
            }
        }
        #endregion

        #region Constructors
        public ParkingContentClass(bool bMedian, DataTable dtDistributRT, DataTable dtOccupation)
        {
            CalcDelegate = null;
            InitializeComponent();
            label1.Text = "";
            AddTextBox(bMedian,dtDistributRT, dtOccupation); 
            
            this.txt_EmportVehicule.Tag = GlobalNames.sParkingGeneralLine16;
            this.txt_EstimationVeh.Tag = GlobalNames.sParkingGeneralLine18;
            this.txt_VL.Tag = GlobalNames.sParkingModalDistribLine1;
            this.txt_Bus.Tag = GlobalNames.sParkingModalDistribLine2;
            this.txt_Transfert.Tag = GlobalNames.sParkingModalDistribLine3;
            this.txt_Taxi.Tag = GlobalNames.sParkingModalDistribLine4;


            //this.txt_RepPointeCH_FR.Tag = GlobalNames.sParkingGeneralLine10;
            //this.txt_RepPointeCH_FR_A_D.Tag = GlobalNames.sParkingGeneralLine11;
            this.txt_RepPointeCH_FR_Pourcent.Tag = GlobalNames.sParkingGeneralLine12;

            //this.txt_PaxTotal.Tag = GlobalNames.sParkingGeneralLine9;
            //this.txt_PaxO_D.Tag = GlobalNames.sParkingGeneralLine8;
            this.txt_PaxA_D.Tag = GlobalNames.sParkingGeneralLine7;

            //this.txt_PaxOrig.Tag = GlobalNames.sParkingGeneralLine6;
            //this.txt_PaxDep.Tag = GlobalNames.sParkingGeneralLine5;
            //this.txt_TraficPaxChart.Tag = GlobalNames.sParkingGeneralLine4;
            //this.txt_TraficPaxReg.Tag = GlobalNames.sParkingGeneralLine3;
            //this.txt_TraficPaxLCC.Tag = GlobalNames.sParkingGeneralLine2;
            //this.txt_TraficPax.Tag = GlobalNames.sParkingGeneralLine1;
            this.textBox1.Tag = GlobalNames.sParkingGeneralLine17;
            this.textBox2.Tag = GlobalNames.sParkingGeneralLine19;
            this.textBox3.Tag = GlobalNames.sParkingGeneralLine20;
            this.textBox4.Tag = GlobalNames.sParkingGeneralLine21;


            /*
            this.textBox34.Tag = GlobalNames.sParkingOccupationTimeLine13;
            this.textBox29.Tag = GlobalNames.sParkingOccupationTimeLine12;
            this.textBox30.Tag = GlobalNames.sParkingOccupationTimeLine11;
            this.textBox31.Tag = GlobalNames.sParkingOccupationTimeLine10;
            this.textBox24.Tag = GlobalNames.sParkingOccupationTimeLine1;
            this.textBox32.Tag = GlobalNames.sParkingOccupationTimeLine9;
            this.textBox23.Tag = GlobalNames.sParkingOccupationTimeLine2;
            this.textBox25.Tag = GlobalNames.sParkingOccupationTimeLine8;
            this.textBox22.Tag = GlobalNames.sParkingOccupationTimeLine3;
            this.textBox26.Tag = GlobalNames.sParkingOccupationTimeLine7;
            this.textBox21.Tag = GlobalNames.sParkingOccupationTimeLine4;
            this.textBox27.Tag = GlobalNames.sParkingOccupationTimeLine6;
            this.textBox28.Tag = GlobalNames.sParkingOccupationTimeLine5;
            this.textBox33.Tag = GlobalNames.sParkingOccupationTimeLine14;*/
        }
        #endregion

        #region Fonctions to add new controls
        internal void AddTextBox(bool bMedian, DataTable dtDistributRT, DataTable dtOccupation)
        {
            ltbDistributionDuringRT = new List<TextBox>();
            ltbOccupation = new List<TextBox>();
            ltlDistributionDuringRT = new List<Label>();
            ltlOccupation = new List<Label>();
            if (dtDistributRT != null)
            {
                int i = 0;
                foreach (DataRow line in dtDistributRT.Rows)
                {
                    TextBox tbTmp = new TextBox();
                    ltbDistributionDuringRT.Add(tbTmp);
                    panel3.Controls.Add(tbTmp);
                    tbTmp.Location = new System.Drawing.Point(12, i * 22 );
                    tbTmp.Size = new System.Drawing.Size(100, 20);
                    tbTmp.Name = "txt_PendantPointeFR" + i.ToString();
                    tbTmp.TabIndex = 46 + i;
                    tbTmp.Tag = line[0].ToString();
                    tbTmp.TextChanged += new EventHandler(textBox1_TextChanged);

                    Label lblTmp = new Label();
                    ltlDistributionDuringRT.Add(lblTmp);
                    panel3.Controls.Add(lblTmp);
                    lblTmp.Location = new System.Drawing.Point(118, i * 22 + 3);

                    lblTmp.AutoSize = true;
                    lblTmp.Name = "lbl_PendantPointeFR" + i.ToString();
                    if((i % 2) ==1)
                        lblTmp.Text = "%";
                    else
                        lblTmp.Text = "Pax";
                    i++;
                }
                panel3.Size = new System.Drawing.Size(143, i * 22 + 35);
                if (i == 0)
                {
                    panel3.Visible = false;
                    panel4.Location = new System.Drawing.Point(0, 420);
                }
                else
                {
                    panel4.Location = new System.Drawing.Point(0, panel3.Location.Y + panel3.Size.Height);
                }
            }
            panel5.Location = new System.Drawing.Point(0, panel4.Location.Y + panel4.Size.Height); 
            if (dtOccupation != null)
            {
                int i = 0;
                foreach (DataRow line in dtOccupation.Rows)
                {
                    TextBox tbTmp = new TextBox();
                    ltbOccupation.Add(tbTmp);
                    panel5.Controls.Add(tbTmp);
                    tbTmp.Location = new System.Drawing.Point(12, i * 22 + 19);
                    tbTmp.Size = new System.Drawing.Size(100, 20);
                    tbTmp.Name = "txt_Occupation" + i.ToString();
                    tbTmp.TabIndex = 54 + i;
                    tbTmp.Tag = line[0].ToString();
                    tbTmp.TextChanged += new EventHandler(textBox1_TextChanged);

                    Label lblTmp = new Label();
                    ltlOccupation.Add(lblTmp);
                    panel5.Controls.Add(lblTmp);
                    lblTmp.Location = new System.Drawing.Point(118, i * 22 + 22);

                    lblTmp.AutoSize = true;
                    lblTmp.Name = "lbl_Occupation" + i.ToString();
                    if (bMedian)
                        lblTmp.Text = "h";
                    else
                        lblTmp.Text = "%";
                    i++;
                }
                panel5.Size = new System.Drawing.Size(143, (i+1) * 22);
                if (i == 0)
                {
                    panel5.Visible = false;
                    panel2.Location = new System.Drawing.Point(0, panel4.Location.Y + panel4.Size.Height); 
                }
                else
                {
                    panel2.Location = new System.Drawing.Point(0, panel5.Location.Y + panel5.Size.Height);
                }
            }
            panel1.Size = new System.Drawing.Size(143, panel2.Location.Y + panel2.Size.Height);
        }
        #endregion

        internal Double getValue(String sTag)
        {
            if ((sTag == "")||(sTag == null))
                return 0;
            return getValue(panel1, sTag);
        }
        private Double getValue(Control ctrl, String sTag)
        {
            if (ctrl == null)
                return 0;
            if ((ctrl.Tag != null) && (ctrl.Tag.GetType() == typeof(String)))
            {
                if (ctrl.Tag.ToString() == sTag)
                {
                    Double dValue = 0;
                    if (!Double.TryParse(ctrl.Text, out dValue))
                        return 0;
                    return dValue;
                }
            }
            if (ctrl.Controls.Count > 0)
            {
                foreach (Control ctrlTmp in ctrl.Controls)
                {
                    Double dValue = getValue(ctrlTmp, sTag);
                    if (dValue != 0)
                        return dValue;
                }
            }
            return 0;
        }

        internal void setValue(String sTag, String sValue)
        {
            if ((sTag == "") || (sTag == null))
                return;
            setValue(panel1, sTag, sValue );
        }
        private void setValue(Control ctrl, String sTag, String sValue)
        {
            if (ctrl == null)
                return;
            if ((ctrl.Tag != null) && (ctrl.Tag.GetType() == typeof(String)))
            {
                if (ctrl.Tag.ToString() == sTag)
                {
                    ctrl.Text = sValue;
                    return;
                }
            }
            if (ctrl.Controls.Count > 0)
            {
                foreach (Control ctrlTmp in ctrl.Controls)
                {
                    setValue(ctrlTmp, sTag, sValue);
                }
            }
        }

        #region Fonctions to fill and save the contents of the current subform
        internal void FillControl(String sTitle, DataTable dtGeneral, DataTable dtRepartitionModale, DataTable dtOccupation, DataTable dtDistributRT, int iFillingIndex)
        {
            label1.Text = sTitle;
            OverallTools.FormFunctions.FillControl(panel1, dtGeneral, 0, iFillingIndex);
            OverallTools.FormFunctions.FillControl(panel1, dtRepartitionModale, 0, iFillingIndex);
            OverallTools.FormFunctions.FillControl(panel1, dtDistributRT, 0, iFillingIndex);
            /*Here we add 1 to \ref iFillindIndex because there is one extra column on that table "Median"*/
            OverallTools.FormFunctions.FillControl(panel1, dtOccupation, 0, iFillingIndex + 1);
        }
        internal void SaveControl(DataTable dtGeneral, DataTable dtRepartitionModale, DataTable dtOccupation, DataTable dtDistributRT, int iFillingIndex)
        {
            OverallTools.FormFunctions.SaveControl(panel1, dtGeneral, 0, iFillingIndex);
            OverallTools.FormFunctions.SaveControl(panel1, dtRepartitionModale, 0, iFillingIndex);
            OverallTools.FormFunctions.SaveControl(panel1, dtDistributRT, 0, iFillingIndex);
            /*Here we add 1 to \ref iFillindIndex because there is one extra column on that table "Median"*/
            OverallTools.FormFunctions.SaveControl(panel1, dtOccupation, 0, iFillingIndex + 1);
        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!((TextBox)sender).ReadOnly)
            {
                OverallTools.FormFunctions.CheckDouble(sender, e);
            }
            if (ucdCalcDelegate != null)
                ucdCalcDelegate();
        }
        #region Accessors to access the different informations of the subform
        internal Panel getPanel()
        {
            return panel1;
        }
        internal String Title
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
        #endregion


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_EmportVehicule = new System.Windows.Forms.TextBox();
            this.txt_Taxi = new System.Windows.Forms.TextBox();
            this.txt_Transfert = new System.Windows.Forms.TextBox();
            this.txt_Bus = new System.Windows.Forms.TextBox();
            this.txt_VL = new System.Windows.Forms.TextBox();
            this.txt_EstimationVeh = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_RepPointeCH_FR_Pourcent = new System.Windows.Forms.TextBox();
            this.txt_PaxA_D = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_RepPointeCH_FR_Pourcent);
            this.panel1.Controls.Add(this.txt_PaxA_D);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 611);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Location = new System.Drawing.Point(0, 464);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(143, 116);
            this.panel2.TabIndex = 69;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(118, 95);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(26, 13);
            this.label20.TabIndex = 84;
            this.label20.Text = "Veh";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(118, 73);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(26, 13);
            this.label19.TabIndex = 83;
            this.label19.Text = "Veh";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(118, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(26, 13);
            this.label18.TabIndex = 82;
            this.label18.Text = "Veh";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(118, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(26, 13);
            this.label17.TabIndex = 81;
            this.label17.Text = "Veh";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(12, 92);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 71;
            this.textBox4.Tag = "30th RT occupation";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 70);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 70;
            this.textBox3.Tag = "Absolute max occupation";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 48);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 69;
            this.textBox2.Tag = "Space";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 68;
            this.textBox1.Tag = "Estimated number of vehicles";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(118, 60);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 13);
            this.label16.TabIndex = 84;
            this.label16.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Pax";
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(91, 428);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(143, 92);
            this.panel5.TabIndex = 71;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.txt_EmportVehicule);
            this.panel4.Controls.Add(this.txt_Taxi);
            this.panel4.Controls.Add(this.txt_Transfert);
            this.panel4.Controls.Add(this.txt_Bus);
            this.panel4.Controls.Add(this.txt_VL);
            this.panel4.Controls.Add(this.txt_EstimationVeh);
            this.panel4.Location = new System.Drawing.Point(0, 197);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(143, 221);
            this.panel4.TabIndex = 70;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(118, 196);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 13);
            this.label15.TabIndex = 83;
            this.label15.Text = "%";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(118, 174);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 13);
            this.label14.TabIndex = 82;
            this.label14.Text = "%";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(118, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 81;
            this.label13.Text = "%";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(118, 130);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 13);
            this.label12.TabIndex = 80;
            this.label12.Text = "%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(118, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 79;
            this.label11.Text = "%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(118, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 78;
            this.label10.Text = "Pax";
            // 
            // txt_EmportVehicule
            // 
            this.txt_EmportVehicule.Location = new System.Drawing.Point(12, 14);
            this.txt_EmportVehicule.Name = "txt_EmportVehicule";
            this.txt_EmportVehicule.Size = new System.Drawing.Size(100, 20);
            this.txt_EmportVehicule.TabIndex = 48;
            this.txt_EmportVehicule.Tag = "Number of Pax per Vehicle";
            this.txt_EmportVehicule.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_Taxi
            // 
            this.txt_Taxi.Location = new System.Drawing.Point(12, 193);
            this.txt_Taxi.Name = "txt_Taxi";
            this.txt_Taxi.Size = new System.Drawing.Size(100, 20);
            this.txt_Taxi.TabIndex = 53;
            this.txt_Taxi.Tag = "Taxi";
            this.txt_Taxi.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_Transfert
            // 
            this.txt_Transfert.Location = new System.Drawing.Point(12, 171);
            this.txt_Transfert.Name = "txt_Transfert";
            this.txt_Transfert.Size = new System.Drawing.Size(100, 20);
            this.txt_Transfert.TabIndex = 52;
            this.txt_Transfert.Tag = "Transfert services";
            this.txt_Transfert.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_Bus
            // 
            this.txt_Bus.Location = new System.Drawing.Point(12, 149);
            this.txt_Bus.Name = "txt_Bus";
            this.txt_Bus.Size = new System.Drawing.Size(100, 20);
            this.txt_Bus.TabIndex = 51;
            this.txt_Bus.Tag = "Public transport (Buses)";
            this.txt_Bus.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_VL
            // 
            this.txt_VL.Location = new System.Drawing.Point(12, 127);
            this.txt_VL.Name = "txt_VL";
            this.txt_VL.Size = new System.Drawing.Size(100, 20);
            this.txt_VL.TabIndex = 50;
            this.txt_VL.Tag = "Small vehicles";
            this.txt_VL.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_EstimationVeh
            // 
            this.txt_EstimationVeh.Location = new System.Drawing.Point(12, 68);
            this.txt_EstimationVeh.Name = "txt_EstimationVeh";
            this.txt_EstimationVeh.Size = new System.Drawing.Size(100, 20);
            this.txt_EstimationVeh.TabIndex = 49;
            this.txt_EstimationVeh.Tag = "Distribution access Parking (except incidents)";
            this.txt_EstimationVeh.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "label1";
            // 
            // txt_RepPointeCH_FR_Pourcent
            // 
            this.txt_RepPointeCH_FR_Pourcent.Location = new System.Drawing.Point(12, 57);
            this.txt_RepPointeCH_FR_Pourcent.Name = "txt_RepPointeCH_FR_Pourcent";
            this.txt_RepPointeCH_FR_Pourcent.ReadOnly = true;
            this.txt_RepPointeCH_FR_Pourcent.Size = new System.Drawing.Size(100, 20);
            this.txt_RepPointeCH_FR_Pourcent.TabIndex = 43;
            this.txt_RepPointeCH_FR_Pourcent.Tag = "Distribution of rush time (30th RT) %";
            this.txt_RepPointeCH_FR_Pourcent.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_PaxA_D
            // 
            this.txt_PaxA_D.Location = new System.Drawing.Point(12, 35);
            this.txt_PaxA_D.Name = "txt_PaxA_D";
            this.txt_PaxA_D.Size = new System.Drawing.Size(100, 20);
            this.txt_PaxA_D.TabIndex = 40;
            this.txt_PaxA_D.Tag = "Passengers 30th RT (A+D)";
            this.txt_PaxA_D.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(0, 106);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(143, 92);
            this.panel3.TabIndex = 46;
            // 
            // ParkingContent
            // 
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();

        }

        #endregion

        #region The different control of the subform (imported from ParkingConte.Designer.cs)
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_Taxi;
        private System.Windows.Forms.TextBox txt_Transfert;
        private System.Windows.Forms.TextBox txt_Bus;
        private System.Windows.Forms.TextBox txt_VL;
        private System.Windows.Forms.TextBox txt_EstimationVeh;
        private System.Windows.Forms.TextBox txt_EmportVehicule;
        private System.Windows.Forms.TextBox txt_RepPointeCH_FR_Pourcent;
        private System.Windows.Forms.TextBox txt_PaxA_D;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        #endregion

       }
}
