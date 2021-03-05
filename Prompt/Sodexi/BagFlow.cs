using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Prompt.Sodexi
{
    public partial class BagFlow : Form
    {
        GestionDonneesHUB2SIM Donnees;
        bool bProtectUpdateDate;
        bool bFlightPlanGeneration_;

        #region Constructeurs
        public BagFlow(GestionDonneesHUB2SIM Donnees_, bool bFlightPlanGeneration)
        {
            InitializeComponent();
            bFlightPlanGeneration_ = bFlightPlanGeneration;
            UpdateAvailable();
            bProtectUpdateDate = false;
            OverallTools.FonctionUtiles.MajBackground(this);
            button1.Tag = cb_ArrivalFlightPlan;
            button2.Tag = cb_DepartureFlightPlan;
            button3.Tag = cb_Mix;
            button4.Tag = cb_FretPoste;
            button5.Tag = cb_Parameters;
            button6.Tag = cbLocalAirports;  // Sodexi Task#7129 Bagplan Update
            Donnees = Donnees_;
            Dictionary<String,List<String>> dslsUserData = Donnees.getUserData();
            UpdateList(cb_ArrivalFlightPlan, dslsUserData);
            UpdateList(cb_DepartureFlightPlan, dslsUserData);
            UpdateList(cb_Mix, dslsUserData);
            UpdateList(cb_FretPoste, dslsUserData);
            UpdateList(cb_Parameters, dslsUserData);
            UpdateList(cbLocalAirports, dslsUserData);  // Sodexi Task#7129 Bagplan Update
            cb_SamplingStep.SelectedIndex = 8;

            if (!bFlightPlanGeneration)
                UpdateDate();
        }

        private void UpdateAvailable()
        {
            cb_ArrivalFlightPlan.Enabled = bFlightPlanGeneration_;
            cb_DepartureFlightPlan.Enabled = bFlightPlanGeneration_;
            cb_Mix.Enabled = bFlightPlanGeneration_;
            button1.Enabled = bFlightPlanGeneration_;
            button2.Enabled = bFlightPlanGeneration_;
            button3.Enabled = bFlightPlanGeneration_;


            cb_FretPoste.Enabled = !bFlightPlanGeneration_;
            button4.Enabled = !bFlightPlanGeneration_;
            button5.Enabled = true;
            cb_Parameters.Enabled = true;
            // << Sodexi Task#7129 Bagplan Update
            cbLocalAirports.Enabled = !bFlightPlanGeneration_;
            button6.Enabled = !bFlightPlanGeneration_;
            // >> Sodexi Task#7129 Bagplan Update            
            gb_Dates.Enabled = !bFlightPlanGeneration_;
            txt_ADSize.Enabled = !bFlightPlanGeneration_;
            txt_ADVolume.Enabled = !bFlightPlanGeneration_;
            txt_ADWeight.Enabled = !bFlightPlanGeneration_;
            txt_ContainerSize.Enabled = !bFlightPlanGeneration_;
            txt_ContainerVolume.Enabled = !bFlightPlanGeneration_;
            txt_ContainerWeight.Enabled = !bFlightPlanGeneration_;
            txt_LongStorageParameter.Enabled = !bFlightPlanGeneration_;
            txt_MeanProcessTime.Enabled = !bFlightPlanGeneration_;
            txt_MinTimeBetweenFlights.Enabled = !bFlightPlanGeneration_;
            txt_ScenarioName.Enabled = !bFlightPlanGeneration_;
        }

        private void UpdateList(ComboBox cbCombo, Dictionary<String,List<String>> dslsUserData)
        {
            if(cbCombo == null)
                return;
            if (cbCombo.Tag == null)
                return;
            String sSelectedTable = cbCombo.Text;
            cbCombo.Items.Clear();
            cbCombo.Text = "";
            if(dslsUserData == null)
                return;
            String sUserData = cbCombo.Tag.ToString();
            if (sUserData == null)
                return;
            if(!dslsUserData.ContainsKey(sUserData ))
                return;
            if (dslsUserData[sUserData] == null)
                return;
            foreach (String sValue in dslsUserData[sUserData])
            {
                cbCombo.Items.Add(sValue);
            }
            cbCombo.Text = sSelectedTable;
        }
        #endregion

        #region Accesseurs

        #region Pour les tables

        private void LoadTable(ComboBox cbCombo)
        {
            if(cbCombo == null)
                return;
            if (cbCombo.Tag == null)
                return;
            String File = cbCombo.Text;
            String sUserDataName = cbCombo.Tag.ToString();
            if (Donnees.IsUserData(File))
                return;
            Donnees.AddUserData(sUserDataName, File);
            String sFileName = System.IO.Path.GetFileName(File);
            cbCombo.Text = sFileName;
            UpdateList(cbCombo,Donnees.getUserData());
        }
        internal NormalTable ArrivalFlightPlan
        {
            get
            {
                LoadTable(cb_ArrivalFlightPlan);
                NormalTable ntTmp = Donnees.GetFormatedUserDataTable(cb_ArrivalFlightPlan.Text);
                if (ntTmp == null)
                    return null;
                if (!SodexiNames.IsValid(ntTmp.Table, SodexiNames.ArrivalFlightPlan))
                    return null;
                return ntTmp;
            }
        }

        internal NormalTable DepartureFlightPlan
        {
            get
            {
                LoadTable(cb_DepartureFlightPlan);
                NormalTable ntTmp = Donnees.GetFormatedUserDataTable(cb_DepartureFlightPlan.Text);
                if (ntTmp == null)
                    return null;
                if (!SodexiNames.IsValid(ntTmp.Table, SodexiNames.DepartureFlightPlan))
                    return null;
                return ntTmp;
            }
        }
        internal NormalTable Mix
        {
            get
            {
                LoadTable(cb_Mix);
                NormalTable ntTmp = Donnees.GetFormatedUserDataTable(cb_Mix.Text);
                if (ntTmp == null)
                    return null;
                if (!SodexiNames.IsValid(ntTmp.Table, SodexiNames.Mix))
                    return null;
                return ntTmp;
            }
        }
        internal NormalTable FretPoste
        {
            get
            {
                LoadTable(cb_FretPoste);
                NormalTable ntTmp = Donnees.GetFormatedUserDataTable(cb_FretPoste.Text);
                if (ntTmp == null)
                    return null;
                if (!SodexiNames.IsValid(ntTmp.Table, SodexiNames.FretPoste))
                    return null;
                return ntTmp;
            }
        }

        internal NormalTable Parameters
        {
            get
            {
                LoadTable(cb_Parameters);
                NormalTable ntTmp = Donnees.GetFormatedUserDataTable(cb_Parameters.Text);
                if (ntTmp == null)
                    return null;
                if (!SodexiNames.IsValid(ntTmp.Table, SodexiNames.Parameters))
                    return null;
                return ntTmp;
            }
        }
        // << Sodexi Task#7129 Bagplan Update
        internal NormalTable LocalAirports
        {
            get
            {
                LoadTable(cbLocalAirports);
                NormalTable ntTmp = Donnees.GetFormatedUserDataTable(cbLocalAirports.Text);
                if (ntTmp == null)
                    return null;
                if (!SodexiNames.IsValid(ntTmp.Table, SodexiNames.LocalAirports))
                    return null;
                return ntTmp;
            }
        }
        // >> Sodexi Task#7129 Bagplan Update
        internal String ArrivalFlightPlanName
        {
            get
            {
                return cb_ArrivalFlightPlan.Text;
            }
        }

        internal String DepartureFlightPlanName
        {
            get
            {
                return cb_DepartureFlightPlan.Text;
            }
        }
        internal String MixName
        {
            get
            {
                return cb_Mix.Text;
            }
        }
        internal String FretPosteName
        {
            get
            {
                return cb_FretPoste.Text;
            }
        }
        // << Sodexi Task#7129 Bagplan Update
        internal String LocalAirportsName
        {
            get
            {
                return cbLocalAirports.Text;
            }
        }
        // >> Sodexi Task#7129 Bagplan Update

        internal Dictionary<String, String> UserDataParameters
        {
            get
            {
                Dictionary<String, String> htResults = new Dictionary<string, string>();
                htResults.Add(cb_Parameters.Tag.ToString(), cb_Parameters.Text);
                htResults.Add(cb_FretPoste.Tag.ToString(), cb_FretPoste.Text);
                return htResults;
            }
        }
        #endregion

        #region Pour les autres informations
        internal DateTime StartDate
        {
            get
            {
                return dtp_BeginTime.Value;
            }
        }
        internal DateTime EndDate
        {
            get
            {
                return dtp_EndTime.Value;
            }
        }

        internal Double SamplingStep
        {
            get
            {
                return FonctionsType.getDouble(cb_SamplingStep.Text);
            }
        }

        internal Double AnalysisRange
        {
            get
            {
                return FonctionsType.getDouble(cb_AnalysisRange.Text);
            }
        }
        
        internal String ScenarioName
        {
            get
            {
                return txt_ScenarioName.Text;
            }
        }
        
        internal int MinProcessTime
        {
            get
            {
                return FonctionsType.getInt(txt_MinTimeBetweenFlights.Text);
            }
        }

        internal int MinTimeBetweenFlights
        {
            get
            {
                return FonctionsType.getInt(txt_MinTimeBetweenFlights.Text);
            }
        }
        internal int MeanDwellTime
        {
            get
            {
                return FonctionsType.getInt(txt_MeanProcessTime.Text);
            }
        }

        internal Double LongStorageParameter
        {
            get
            {
                return FonctionsType.getDouble(txt_LongStorageParameter.Text);
            }
        }
        internal int ADSize
        {
            get
            {
                return FonctionsType.getInt(txt_ADSize.Text);
            }
        }
        internal Double ADWeight
        {
            get
            {
                return FonctionsType.getDouble(txt_ADWeight.Text);
            }
        }
        internal Double ADVolume
        {
            get
            {
                return FonctionsType.getDouble(txt_ADVolume.Text);
            }
        }
        internal int ContainerSize
        {
            get
            {
                return FonctionsType.getInt(txt_ContainerSize.Text);
            }
        }
        internal Double ContainerWeight
        {
            get
            {
                return FonctionsType.getDouble(txt_ContainerWeight.Text);
            }
        }
        internal Double ContainerVolume
        {
            get
            {
                return FonctionsType.getDouble(txt_ContainerVolume.Text);
            }
        }
        #endregion

        #region Pour les informations sur les makeUp
        internal int IndexMupImportStart
        {
            get
            {
                return FonctionsType.getInt(txt_MupImportStart.Text);
            }
        }
        internal int IndexMupImportEnd
        {
            get
            {
                return FonctionsType.getInt(txt_MupImportEnd.Text);
            }
        }
        internal int IndexMupPosteStart
        {
            get
            {
                return FonctionsType.getInt(txt_MupPosteStart.Text);
            }
        }
        internal int IndexMupPosteEnd
        {
            get
            {
                return FonctionsType.getInt(txt_MupPosteEnd.Text);
            }
        }
        internal int IndexMupPosteCHRStart
        {
            get
            {
                return FonctionsType.getInt(txt_MupPosteCHRStart.Text);
            }
        }
        internal int IndexMupPosteCHREnd
        {
            get
            {
                return FonctionsType.getInt(txt_MupPosteCHREnd.Text);
            }
        }
        internal int IndexMupPosteCYMStart
        {
            get
            {
                return FonctionsType.getInt(txt_MupPosteCYMStart.Text);
            }
        }
        internal int IndexMupPosteCYMEnd
        {
            get
            {
                return FonctionsType.getInt(txt_MupPosteCYMEnd.Text);
            }
        }
        internal int IndexMupADAreaStart
        {
            get
            {
                return FonctionsType.getInt(txt_MupADAreaStart.Text);
            }
        }
        internal int IndexMupADAreaEnd
        {
            get
            {
                return FonctionsType.getInt(txt_MupADAreaEnd.Text);
            }
        }
        #endregion


        #endregion

        #region Lors de la fermeture du formulaire
        private void BagFlow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((DialogResult != System.Windows.Forms.DialogResult.OK) &&
                (DialogResult != System.Windows.Forms.DialogResult.Cancel))
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
        #endregion

        #region  Fonctions déclenchées lors de l'utilisation du formulaire (btn ok/ changement de dates)
        #region Boutons pour ouvrir de nouveaux fichiers.

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Button))
                return;
            if (((Button)sender).Tag == null)
                return;
            if (((Button)sender).Tag.GetType() != typeof(ComboBox))
                return;
            ComboBox cbCombo = (ComboBox)((Button)sender).Tag;
            if (cbCombo.Text != "")
                ofd_Dialog.FileName = cbCombo.Text;
            if (ofd_Dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            cbCombo.Text = ofd_Dialog.FileName;
            if((cbCombo == cb_DepartureFlightPlan) ||
                (cbCombo == cb_ArrivalFlightPlan))
                UpdateDate();
        }

        #region Fonctions pour le choix des dates min et max.


        private void ChangeDate(String FPA, String FPD, DateTimePicker dateBegin, DateTimePicker dateEnd)
        {
            if ((((FPA == "") || (FPA == null)) && ((FPD == "") || (FPD == null))))
            {
                return;
            }
            DateTime minFPA = new DateTime(),
                    minFPD = new DateTime(),
                    maxFPA = new DateTime(),
                    maxFPD = new DateTime();
            bool bFPA = false;
            bool bFPD = false;
            if ((FPA != "") && (FPA != null))
            {
                bFPA = findMinMaxValues(FPA, out minFPA, out maxFPA);
                /*if (!findMinMaxValues(FPA, out minFPA, out maxFPA))
                {
                    return;
                }*/
            }
            if ((FPD != "") && (FPD != null))
            {
                bFPD = findMinMaxValues(FPD, out minFPD, out maxFPD);
                /*                if (!findMinMaxValues(FPD, out minFPD, out maxFPD))
                                {
                                    return;
                                }*/
            }
            if ((!bFPA) && (!bFPD))
                return;
            if ((FPA != "") && (FPD != "") && bFPA && bFPD)
            {
                if (minFPA > minFPD)
                {
                    minFPA = minFPD;
                }
                if (maxFPA < maxFPD)
                {
                    maxFPA = maxFPD;
                }
            }
            else if ((FPD != "") && bFPD)
            {
                minFPA = minFPD;
                maxFPA = maxFPD;
            }

            minFPD = minFPA.Date.AddDays(-1);
            maxFPD = maxFPA.Date.AddDays(2);

            dateBegin.MaxDate = DateTimePicker.MaximumDateTime;
            dateBegin.MinDate = DateTimePicker.MinimumDateTime;
            dateEnd.MinDate = DateTimePicker.MinimumDateTime;
            dateEnd.MaxDate = DateTimePicker.MaximumDateTime;

            if ((dateBegin.Value < minFPD) ||
                (dateBegin.Value > maxFPD))
                dateBegin.Value = minFPA.Date;

            if ((dateEnd.Value < minFPD) ||
                    (dateEnd.Value > maxFPD))
                dateEnd.Value = maxFPA.Date.AddHours(23).AddMinutes(59);
            if (dateEnd.Value < dateBegin.Value)
                dateEnd.Value = maxFPD;
            dateBegin.MaxDate = maxFPD;
            dateBegin.MinDate = minFPD;
            dateEnd.MinDate = minFPD;
            dateEnd.MaxDate = maxFPD;
        }
        private bool findMinMaxValues(String Name, out DateTime min, out DateTime max)
        {
            DataTable fpTable = Donnees.getTable("Input", Name);
            if (fpTable == null)
            {
                min = DateTime.MinValue;
                max = DateTime.MinValue;
                return false;
            }
            max = OverallTools.DataFunctions.valeurMaximaleDansColonne(fpTable, 1, 2);
            min = OverallTools.DataFunctions.valeurMinimaleDansColonne(fpTable, 1, 2);
            if ((max == DateTime.MinValue) || (min == DateTime.MinValue))
                return false;
            return true;
        }

        private void UpdateDate()
        {
            ChangeDate(GlobalNames.FPATableName, GlobalNames.FPDTableName, dtp_BeginTime, dtp_EndTime);
        }
        private bool findMinMaxValues(DataTable dtTable, out DateTime min, out DateTime max)
        {
            if (dtTable == null)
            {
                min = DateTime.MinValue;
                max = DateTime.MinValue;
                return false;
            }
            max = getMaxValue(dtTable, SodexiNames.sArrivalFlightPlan_DateTime);
            min = getMinValue(dtTable, SodexiNames.sArrivalFlightPlan_DateTime);
            if ((max == DateTime.MinValue) || (min == DateTime.MinValue))
                return false;
            return true;
        }
        private DateTime getMaxValue(DataTable dtTable, String sColumnName)
        {
            if (dtTable == null)
                return DateTimePicker.MaximumDateTime;
            if (dtTable.Rows.Count == 0)
                return DateTimePicker.MaximumDateTime;
            int iIndexcolumn = dtTable.Columns.IndexOf(sColumnName);
            if (iIndexcolumn == -1)
                return DateTimePicker.MaximumDateTime;
            DateTime dtValue = FonctionsType.getDate(dtTable.Rows[0][iIndexcolumn]);
            foreach (DataRow drRow in dtTable.Rows)
            {
                DateTime dtTmp = FonctionsType.getDate(drRow[iIndexcolumn]);
                if (dtValue < dtTmp)
                    dtValue = dtTmp;
            }
            return dtValue;
        }
        private DateTime getMinValue(DataTable dtTable, String sColumnName)
        {
            if (dtTable == null)
                return DateTimePicker.MaximumDateTime;
            if (dtTable.Rows.Count == 0)
                return DateTimePicker.MaximumDateTime;
            int iIndexcolumn = dtTable.Columns.IndexOf(sColumnName);
            if (iIndexcolumn == -1)
                return DateTimePicker.MaximumDateTime;
            DateTime dtValue = FonctionsType.getDate(dtTable.Rows[0][iIndexcolumn]);
            foreach (DataRow drRow in dtTable.Rows)
            {
                DateTime dtTmp = FonctionsType.getDate(drRow[iIndexcolumn]);
                if (dtValue > dtTmp)
                    dtValue = dtTmp;
            }
            return dtValue;
        }
        #endregion
        #endregion

        private void cb_SamplingStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iMax = 60;
            int iValue;
            cb_AnalysisRange.Items.Clear();
            cb_AnalysisRange.Text = "";
            if (!Int32.TryParse(cb_SamplingStep.Text, out iValue))
            {
                return;
            }
            for (int i = iValue; i <= iMax; i++)
            {
                if (((i % iValue) == 0) && ((iMax % i) == 0))
                {
                    cb_AnalysisRange.Items.Add(i.ToString());
                }
            }
            cb_AnalysisRange.SelectedIndex = 0;
        }

        private void dtp_BeginTime_ValueChanged(object sender, EventArgs e)
        {
            dtp_EndTime.MinDate = dtp_BeginTime.Value;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            #region Vérification pour les tables
            if (bFlightPlanGeneration_)
            {
                if ((cb_ArrivalFlightPlan.Text == "") || (ArrivalFlightPlan == null))
                {
                    MessageBox.Show("Please specify a table for \"Arrival flight plan\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cb_ArrivalFlightPlan.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if ((cb_DepartureFlightPlan.Text == "") || (DepartureFlightPlan == null))
                {
                    MessageBox.Show("Please specify a table for \"Departure flight plan\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cb_DepartureFlightPlan.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if ((cb_Mix.Text == "") || (Mix == null))
                {
                    MessageBox.Show("Please specify a table for \"Mix\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cb_Mix.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
            }
            else
            {
                if ((cb_FretPoste.Text == "") || (FretPoste == null))
                {
                    MessageBox.Show("Please specify a table for \"FRET/POSTE\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cb_FretPoste.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if ((cb_Parameters.Text == "") || (Parameters == null))
                {
                    MessageBox.Show("Please specify a table for \"Parameters\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cb_Parameters.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                // << Sodexi Task#7129 Bagplan Update                
                if ((cbLocalAirports.Text == "") || (LocalAirports == null))
                {
                    MessageBox.Show("Please specify a table for \"LocalAirports\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbLocalAirports.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                // >> Sodexi Task#7129 Bagplan Update
            }
            #endregion

            #region Vérification pour les dates et pas d'analyses.
            if (!bFlightPlanGeneration_)
            {
                if (dtp_BeginTime.Value > dtp_EndTime.Value)
                {
                    MessageBox.Show("Please specify valid dates", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtp_BeginTime.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if (SamplingStep == 0)
                {
                    MessageBox.Show("Please specify a valid sampling step", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cb_SamplingStep.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                Double dDiff = OverallTools.DataFunctions.MinuteDifference(dtp_BeginTime.Value, dtp_EndTime.Value);
                if ((dDiff / SamplingStep) > 500)
                {
                    MessageBox.Show("The parameters for the date and sampling step are not compatible. The actual configuration will create a table with " + FonctionsType.getInt((dDiff / SamplingStep)).ToString() + " columns. (Should not exceed 500)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cb_SamplingStep.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
            }
            #endregion

            #region Vérification des autres textbox
            if (!bFlightPlanGeneration_)
            {

                //SGE-23/03/2012-Sodexi-Begin
                if ((txt_ScenarioName.Text == "") /*|| (Donnees.GetScenario(txt_ScenarioName.Text) != null)*/)
                //SGE-23/03/2012-Sodexi-End
                {
                    MessageBox.Show("Please specify a valid scenario Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_ScenarioName.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                //SGE-23/03/2012-Sodexi-Begin
                List<String> lsNamesAllScenario = Donnees.getScenarioNames();
                List<String> lsNames = Donnees.getScenarioPAXBHS();
                if ((lsNames != null) && (!lsNames.Contains(txt_ScenarioName.Text))&& (lsNamesAllScenario.Contains(txt_ScenarioName.Text)))
                {
                    MessageBox.Show("The scenario you selected is not valid for defining a Bagplan. Please select another scenario name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_ScenarioName.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if (lsNames.Contains(txt_ScenarioName.Text))
                {
                    if (MessageBox.Show("The scenario name you selected is already existing.\n If you use this scenario, inconsistent data might occurs.\n Do you want to continue?.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.Yes)
                    {
                        txt_ScenarioName.Focus();
                        DialogResult = System.Windows.Forms.DialogResult.None;
                    }
                }
                //SGE-23/03/2012-Sodexi-End

                if ((txt_MinTimeBetweenFlights.Text == "") || (FonctionsType.getInt(txt_MinTimeBetweenFlights.Text) < 0))
                {
                    MessageBox.Show("Please specify a valid number >=0 for the minimum process time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_MinTimeBetweenFlights.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                if ((txt_MeanProcessTime.Text == "") || (FonctionsType.getInt(txt_MeanProcessTime.Text) < 0))
                {
                    MessageBox.Show("Please specify a valid number >=0 for the minimum process time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_MeanProcessTime.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if (MinTimeBetweenFlights < MeanDwellTime)
                {
                    MessageBox.Show("The minimum time between 2 flights should be a value greather than the mean dwelling time in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_MeanProcessTime.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                if ((txt_LongStorageParameter.Text == "") || (FonctionsType.getDouble(txt_LongStorageParameter.Text) < 0))
                {
                    MessageBox.Show("Please specify a valid number >=0 for the delay for the long storage area.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_LongStorageParameter.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }


                if ((txt_ADSize.Text == "") || (FonctionsType.getInt(txt_ADSize.Text) <= 0))
                {
                    MessageBox.Show("Please specify a valid number >0 for the size of AD boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_ADSize.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if ((txt_ADWeight.Text == "") || (FonctionsType.getDouble(txt_ADWeight.Text) <= 0))
                {
                    MessageBox.Show("Please specify a valid value >0 for the weight of AD boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_ADWeight.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if ((txt_ADVolume.Text == "") || (FonctionsType.getDouble(txt_ADVolume.Text) <= 0))
                {
                    MessageBox.Show("Please specify a valid value >0 for the volume of AD boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_ADVolume.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if ((txt_ContainerSize.Text == "") || (FonctionsType.getInt(txt_ContainerSize.Text) <= 0))
                {
                    MessageBox.Show("Please specify a valid number >0 for the size of container size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_ContainerSize.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if ((txt_ContainerWeight.Text == "") || (FonctionsType.getDouble(txt_ContainerWeight.Text) <= 0))
                {
                    MessageBox.Show("Please specify a valid value >0 for the weight of Container", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_ContainerWeight.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if ((txt_ContainerVolume.Text == "") || (FonctionsType.getDouble(txt_ContainerVolume.Text) <= 0))
                {
                    MessageBox.Show("Please specify a valid value >0 for the volume of Container", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_ContainerVolume.Focus();
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
            }
            #endregion

            DataTable dtTable = Donnees.getTable("Input", cb_Parameters.Text);
            if((dtTable != null) && (!bFlightPlanGeneration_))
                OverallTools.FormFunctions.SaveControl(this, dtTable, 0, 1);

        }
        #endregion

        private void cb_ArrivalFlightPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bProtectUpdateDate)
                return;
            UpdateDate();
        }

        private void cb_Parameters_TextChanged(object sender, EventArgs e)
        {
            NormalTable ntTable = Parameters;
            if (ntTable == null)
                return;
            OverallTools.FormFunctions.FillControl(this, ntTable.Table, 0, 1);
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }
    }
}
