using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Parking
{
    public partial class ParkingContent : Form
    {
        public ParkingContent()
        {
            InitializeComponent();
            label1.Text = "";
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

        }
        internal void FillControl(String sTitle, DataTable dtGeneral, DataTable dtRepartitionModale, DataTable dtOccupation, int iFillingIndex)
        {
            label1.Text = sTitle;
            OverallTools.FormFunctions.FillControl(this, dtGeneral, 0, iFillingIndex);
            OverallTools.FormFunctions.FillControl(this, dtRepartitionModale, 0, iFillingIndex);
            /*Here we add 1 to \ref iFillindIndex because there is one extra column on that table "Median"*/
            OverallTools.FormFunctions.FillControl(this, dtOccupation, 0, iFillingIndex+1);
        }
        internal void SaveControl(DataTable dtGeneral, DataTable dtRepartitionModale, DataTable dtOccupation, int iFillingIndex)
        {
            OverallTools.FormFunctions.SaveControl(this, dtGeneral, 0, iFillingIndex);
            OverallTools.FormFunctions.SaveControl(this, dtRepartitionModale, 0, iFillingIndex);
            /*Here we add 1 to \ref iFillindIndex because there is one extra column on that table "Median"*/
            OverallTools.FormFunctions.SaveControl(this, dtOccupation, 0, iFillingIndex + 1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OverallTools.FormFunctions.CheckDouble(sender, e);
        }
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


    }
}
