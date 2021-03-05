using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Parking
{
    class ParkingCalcs
    {
        ParkingContentClass pccTotal;
        List<ParkingContentClass> lppcContent;
        Panel pPrincipal;
        DataTable dtOccupation_;


        internal ParkingCalcs(DataTable dtDistributRT, DataTable dtOccupation)
        {
            pPrincipal = null;
            pccTotal = AddContent(dtDistributRT,dtOccupation,"Total");
            lppcContent = new List<ParkingContentClass>();
            for (int i = 2; i < dtDistributRT.Columns.Count; i++)
            {
                lppcContent.Add(AddContent(dtDistributRT, dtOccupation, dtDistributRT.Columns[i].ColumnName));
            }
            UpdateLocation();
        }
        private ParkingContentClass AddContent(DataTable dtDistributRT, DataTable dtOccupation, String sTitle)
        {
            ParkingContentClass pcNewContent = new ParkingContentClass(sTitle=="Total", dtDistributRT, dtOccupation);
            dtOccupation_ = dtOccupation;
            Panel p = pcNewContent.getPanel();
            if (pPrincipal == null)
            {
                pPrincipal = new Panel();

                pPrincipal.AutoScroll = false;
                pPrincipal.Location = new System.Drawing.Point(0, 0);
                pPrincipal.Name = "Principal";
                pPrincipal.Size = new System.Drawing.Size(150, p.Height);
                pPrincipal.TabIndex = 0;
            }
            pPrincipal.Controls.Add(p);
            p.Parent = pPrincipal;
            p.Location = new Point(0, 0);
            p.Size = new Size(143, p.Height);
            p.Show();
            pPrincipal.PerformLayout();
            pcNewContent.Title = sTitle;
            return pcNewContent;
        }
        private void UpdateLocation()
        {
            pccTotal.getPanel().Location = new Point(0 , 0);
            int iTmp = 0;
            pPrincipal.Size = new System.Drawing.Size(150 * (lppcContent.Count+1), pccTotal.getPanel().Size.Height);
            foreach (ParkingContentClass ppcTmp in lppcContent)
            {
                iTmp +=150;
                ppcTmp.getPanel().Location = new Point(iTmp, 0);
            }
        }
        internal Panel getPanel()
        {
            return pPrincipal;
        }

        internal void FillControl(DataTable dtGeneral, DataTable dtRepartitionModale, DataTable dtOccupation, DataTable dtDistributRT)
        {
            pccTotal.CalcDelegate = null;
            foreach (ParkingContentClass ppcTmp in lppcContent)
                ppcTmp.CalcDelegate = null;

            pccTotal.FillControl(pccTotal.Title, dtGeneral, dtRepartitionModale, dtOccupation, dtDistributRT, 1);
            for (int i = 0; i < lppcContent.Count; i++)
            {
                lppcContent[i].FillControl(lppcContent[i].Title, dtGeneral, dtRepartitionModale, dtOccupation,dtDistributRT, i+2);
            }
            

            pccTotal.CalcDelegate = new UpdateCalcsDelegate(UpdateCalcs);
            foreach (ParkingContentClass ppcTmp in lppcContent)
                ppcTmp.CalcDelegate = new UpdateCalcsDelegate(UpdateCalcs);
            UpdateCalcs();
        }
        internal void SaveControl(DataTable dtGeneral, DataTable dtRepartitionModale, DataTable dtOccupation, DataTable dtDistributRT)
        {

            /*DataTable dtResults = new DataTable("ParkingResults");
            foreach(DataColumn dc in dtGeneral.Columns)
            {
                dtGeneral.Columns.Add(dc.ColumnName, dc.DataType);
            }
            DataRow drRow = dtGeneral.NewRow();
            drRow[0] = GlobalNames.sParkingGeneralLine17;
            dtGeneral.Rows.Add(drRow);
            drRow = dtGeneral.NewRow();
            drRow[0] = GlobalNames.sParkingGeneralLine19;
            dtGeneral.Rows.Add(drRow);
            drRow = dtGeneral.NewRow();
            drRow[0] = GlobalNames.sParkingGeneralLine20;
            dtGeneral.Rows.Add(drRow);
            drRow = dtGeneral.NewRow();
            drRow[0] = GlobalNames.sParkingGeneralLine21;
            dtGeneral.Rows.Add(drRow);
            OverallTools.DataFunctions.InitializeValues(dtGeneral);*/

            pccTotal.SaveControl(dtGeneral, dtRepartitionModale, dtOccupation, dtDistributRT, 1);
            for (int i = 0; i < lppcContent.Count; i++)
            {
                lppcContent[i].SaveControl(dtGeneral, dtRepartitionModale, dtOccupation,dtDistributRT, i + 2);
            }
        }

        /*internal void setUnites(NormalTable dtGeneral, NormalTable dtRepartitionModale, NormalTable dtOccupation, NormalTable dtDistributRT)
        {
            pccTotal.CalcDelegate = null;
            foreach (ParkingContentClass ppcTmp in lppcContent)
                ppcTmp.CalcDelegate = null;

            pccTotal.setUnites(dtGeneral, dtRepartitionModale, dtOccupation, dtDistributRT, 1);
            for (int i = 0; i < lppcContent.Count; i++)
            {
                lppcContent[i].setUnites( dtGeneral, dtRepartitionModale, dtOccupation, dtDistributRT, i + 2);
            }


            pccTotal.CalcDelegate = new UpdateCalcsDelegate(UpdateCalcs);
            foreach (ParkingContentClass ppcTmp in lppcContent)
                ppcTmp.CalcDelegate = new UpdateCalcsDelegate(UpdateCalcs);
            UpdateCalcs();
        }*/
        internal delegate void UpdateCalcsDelegate();
        bool bCalc = false;
        internal void UpdateCalcs()
        {
            if (bCalc)
                return;
            bCalc = true;
            Double TotalPaxA_D = pccTotal.getValue(GlobalNames.sParkingGeneralLine7);
            //pccTotal.setValue(GlobalNames.sParkingGeneralLine1, getSum(GlobalNames.sParkingGeneralLine1).ToString());
            //pccTotal.setValue(GlobalNames.sParkingGeneralLine5, getSum(GlobalNames.sParkingGeneralLine5).ToString());
            //pccTotal.setValue(GlobalNames.sParkingGeneralLine6, getSum(GlobalNames.sParkingGeneralLine6).ToString());
            foreach (ParkingContentClass ppcTmp in lppcContent)
            {
                //ppcTmp.setValue(GlobalNames.sParkingGeneralLine9, ppcTmp.getValue(GlobalNames.sParkingGeneralLine8).ToString());
                //ppcTmp.setValue(GlobalNames.sParkingGeneralLine10, ppcTmp.getValue(GlobalNames.sParkingGeneralLine11).ToString());
                if (TotalPaxA_D == 0)
                    ppcTmp.setValue(GlobalNames.sParkingGeneralLine12, "0");
                else
                    ppcTmp.setValue(GlobalNames.sParkingGeneralLine12, Math.Round(ppcTmp.getValue(GlobalNames.sParkingGeneralLine7) / TotalPaxA_D *100,2).ToString());
            }
            CalcVehicleNumber();
            CalcPlace();
            pccTotal.setValue(GlobalNames.sParkingGeneralLine19, getSum(GlobalNames.sParkingGeneralLine19).ToString());
            bCalc = false;
        }
        internal Double getSum(String sTag)
        {
            Double dSum = 0;
            foreach (ParkingContentClass ppcTmp in lppcContent)
            {
                dSum += ppcTmp.getValue(sTag);
            }
            return dSum;
        }
        internal Double CalcVehicleNumber()
        {
            //Flux total pointe * Repartition pax pendant la pointe * % utilisation Véhicule léger / Nombre pax par véhicule +
            //Flux total traffic complémentaire * Repartition pax pendant la pointe FR * % utilisation Véhicule léger / Nombre pax par véhicule +

            /*foreach (ParkingContentClass ppcTmp in lppcContent)
            {
                Dictionary<String, Double> ldFluxComplementaire = new Dictionary<String, double>();
                Dictionary<String, Double> ldRepartition = new Dictionary<String, double>();
                ldFluxComplementaire.Add(ppcTmp.Title, ppcTmp.getValue(GlobalNames.sParkingGeneralLine10));
                foreach (ParkingContentClass ppcTmp2 in lppcContent)
                {
                    if (ppcTmp.Title == ppcTmp2.Title)
                    {
                        ldRepartition.Add(ppcTmp2.Title, ppcTmp2.getValue(GlobalNames.sParkingDistribRushTimeLine2Begin + ppcTmp2.Title + GlobalNames.sParkingDistribRushTimeLine2End));
                    
                        continue;
                    }
                    ldRepartition.Add(ppcTmp2.Title, ppcTmp2.getValue(GlobalNames.sParkingDistribRushTimeLine2Begin + ppcTmp.Title + GlobalNames.sParkingDistribRushTimeLine2End));
                    
                    ldFluxComplementaire.Add(ppcTmp2.Title, ppcTmp.getValue(GlobalNames.sParkingDistribRushTimeLine1Begin + ppcTmp2.Title + GlobalNames.sParkingDistribRushTimeLine1End));
                }
                Double dReparitionModale = ppcTmp.getValue(GlobalNames.sParkingModalDistribLine1);
                Double dEmport = ppcTmp.getValue(GlobalNames.sParkingGeneralLine16);
                Double dValue = 0;
                if (dEmport != 0)
                {

                    foreach (ParkingContentClass ppcTmp2 in lppcContent)
                        dValue += (ldFluxComplementaire[ppcTmp2.Title] * ldRepartition[ppcTmp2.Title] * dReparitionModale) / (dEmport*10000);
                }
                ppcTmp.setValue(GlobalNames.sParkingGeneralLine17, Math.Round(dValue,2).ToString());
            }*/

            foreach (ParkingContentClass ppcTmp in lppcContent)
            {
                Dictionary<String, Double> ldFluxComplementaire = new Dictionary<String, double>();
                Dictionary<String, Double> ldRepartition = new Dictionary<String, double>();
                ldFluxComplementaire.Add(ppcTmp.Title, ppcTmp.getValue(GlobalNames.sParkingGeneralLine7));
                foreach (ParkingContentClass ppcTmp2 in lppcContent)
                {
                    if (ppcTmp.Title == ppcTmp2.Title)
                    {
                        ldRepartition.Add(ppcTmp2.Title, ppcTmp.getValue(GlobalNames.sParkingDistribRushTimeLine2Begin + ppcTmp2.Title + GlobalNames.sParkingDistribRushTimeLine2End));
                    
                        continue;
                    }
                    ldRepartition.Add(ppcTmp2.Title, ppcTmp.getValue(GlobalNames.sParkingDistribRushTimeLine2Begin + ppcTmp2.Title + GlobalNames.sParkingDistribRushTimeLine2End));
                    
                    ldFluxComplementaire.Add(ppcTmp2.Title, ppcTmp.getValue(GlobalNames.sParkingDistribRushTimeLine1Begin + ppcTmp2.Title + GlobalNames.sParkingDistribRushTimeLine1End));
                }
                Double dReparitionModale = ppcTmp.getValue(GlobalNames.sParkingModalDistribLine1);
                Double dEmport = ppcTmp.getValue(GlobalNames.sParkingGeneralLine16);
                Double dValue = 0;
                if (dEmport != 0)
                {

                    foreach (ParkingContentClass ppcTmp2 in lppcContent)
                        dValue += (ldFluxComplementaire[ppcTmp2.Title] * ldRepartition[ppcTmp2.Title] * dReparitionModale) / (dEmport*10000);
                }
                ppcTmp.setValue(GlobalNames.sParkingGeneralLine17, Math.Round(dValue,2).ToString());
            }
            return 0;
        }
        internal Double CalcPlace()
        {
            //Flux total pointe * Repartition pax pendant la pointe * % utilisation Véhicule léger / Nombre pax par véhicule +
            //Flux total traffic complémentaire * Repartition pax pendant la pointe FR * % utilisation Véhicule léger / Nombre pax par véhicule +

            foreach (ParkingContentClass ppcTmp in lppcContent)
            {
                Double dValue = 0;
                foreach (DataRow drRow in dtOccupation_.Rows)
                {
                    dValue += ppcTmp.getValue(drRow[0].ToString()) * pccTotal.getValue(drRow[0].ToString()) / 100;
                }
                /*Double dValue = ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine1 ) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine1 )/100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine2) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine2) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine3) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine3) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine4) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine4) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine5) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine5) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine6) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine6) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine7) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine7) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine8) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine8) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine9) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine9) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine10) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine10) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine11) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine11) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine12) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine12) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine13) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine13) / 100;
                dValue += ppcTmp.getValue(GlobalNames.sParkingOccupationTimeLine14) * pccTotal.getValue(GlobalNames.sParkingOccupationTimeLine14) / 100;
                */
                Double dTmp = dValue * ppcTmp.getValue(GlobalNames.sParkingGeneralLine17) * ppcTmp.getValue(GlobalNames.sParkingGeneralLine18);
                //dTmp = ppcTmp.getValue(GlobalNames.sParkingGeneralLine18);
                dValue = Math.Round(dValue * ppcTmp.getValue(GlobalNames.sParkingGeneralLine17) * ppcTmp.getValue(GlobalNames.sParkingGeneralLine18)) / 100;


                int iValue = ((int)Math.Floor(dValue)) % 10;
                iValue = (int)Math.Floor(dValue) + 10 - iValue;

                ppcTmp.setValue(GlobalNames.sParkingGeneralLine19, iValue.ToString());
            }

            return 0;
        }
    }
}
