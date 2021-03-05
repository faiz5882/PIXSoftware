using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Allocation_Reclaim : Form
    {
        GestionDonneesHUB2SIM Donnees;
        DataTable fpdTable;
        DataManagement.NormalTable AllocationTable;
        DataTable dtAirline;
        DataManagement.ExceptionTable dtBagTable;
        DataManagement.ExceptionTable dtBagConstraint;
        DataManagement.ExceptionTable dtLoadFactor;
        DataTable []dtResults;
        VisualisationMode []vmResults;
        public DataTable []Results
        {
            get
            {
                return dtResults;
            }
        }
        public VisualisationMode []VisualisationModeResults
        {
            get
            {
                return vmResults;
            }
        }
        public String TablesNames
        {
            get
            {
                return txt_TitleTable.Text;
            }
        }
        private void Initialize(GestionDonneesHUB2SIM gdhDonnees)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            Donnees = gdhDonnees;

            List<String> values = Donnees.getValidTables("Input", GlobalNames.FPATableName);
            cb_FPDTable.Items.AddRange(values.ToArray());
            if (cb_FPDTable.Items.Count > 0)
                cb_FPDTable.SelectedIndex = 0;

            values = Donnees.getValidTables("Input", "OCT_BaggageClaimTable");
            cb_AllocationRules.Items.AddRange(values.ToArray());
            if (cb_AllocationRules.Items.Count > 0)
                cb_AllocationRules.SelectedIndex = 0;

            values = Donnees.getValidTables("Input", GlobalNames.FP_AircraftTypesTableName);
            cb_Aircraft.Items.AddRange(values.ToArray());
            if (cb_Aircraft.Items.Count > 0)
                cb_Aircraft.SelectedIndex = 0;

            values = Donnees.getValidTables("Input", GlobalNames.FP_AirlineCodesTableName);
            cb_Airline.Items.AddRange(values.ToArray());
            if (cb_Airline.Items.Count > 0)
                cb_Airline.SelectedIndex = 0;

            values = Donnees.getValidTables("Input", GlobalNames.NbBagsTableName);
            cb_BagsTable.Items.AddRange(values.ToArray());
            if (cb_BagsTable.Items.Count > 0)
                cb_BagsTable.SelectedIndex = 0;

            values = Donnees.getValidTables("Input", GlobalNames.FPA_LoadFactorsTableName);
            cb_LoadFactor.Items.AddRange(values.ToArray());
            if (cb_LoadFactor.Items.Count > 0)
                cb_LoadFactor.SelectedIndex = 0;

            values = Donnees.getValidTables("Input", "Baggage_Claim_Constraint");
            cb_BagConstraint.Items.AddRange(values.ToArray());
            if (cb_BagConstraint.Items.Count > 0)
                cb_BagConstraint.SelectedIndex = 0;


            if (cb_FlightsAlowedPerBaggageClaimLH.Items.Count > 0)
                cb_FlightsAlowedPerBaggageClaimLH.SelectedIndex = 0;
            if (cb_FlightsAlowedPerBaggageClaim.Items.Count > 0)
                cb_FlightsAlowedPerBaggageClaim.SelectedIndex = 0;
            if (cb_Terminal.Items.Count > 0)
            cb_Terminal.SelectedIndex = 0;
            Donnees = gdhDonnees;
            dtResults = null;
        }
        public SIM_Allocation_Reclaim(GestionDonneesHUB2SIM gdhDonnees)
        {
            Initialize(gdhDonnees);
        }
        
        private void cb_AllocationRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllocationTable = Donnees.GetTable("Input", cb_AllocationRules.Text);
        }
        private void cb_FPDTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            fpdTable = Donnees.getTable("Input", cb_FPDTable.Text);
            ChangeDate( dtp_BeginTime, dtp_EndTime);
            ChangeTerminalColumns(fpdTable, cb_TerminalColumn);
        }
        private static void ChangeTerminalColumns(DataTable dtTable, ComboBox cbDest)
        {
            String sOldValue = cbDest.Text;
            cbDest.Items.Clear();
            cbDest.Text = "";
            for (int i = 0; i < dtTable.Columns.Count; i++)
            {
                if (dtTable.Columns[i].ColumnName.Contains("Terminal"))
                    cbDest.Items.Add(dtTable.Columns[i].ColumnName);
            }
            if (cbDest.Items.Contains(sOldValue))
                cbDest.Text = sOldValue;
            else
                if (cbDest.Items.Count > 0)
                    cbDest.SelectedIndex = 0;
        }

        private static void ChangeTerminalIndex(DataTable dtTable, String sColumn, ComboBox cbDest)
        {
            if (!dtTable.Columns.Contains(sColumn))
                return;
            String sOldValue = cbDest.Text;
            cbDest.Items.Clear();
            cbDest.Text = "";
            ArrayList alList = new ArrayList();
            foreach (DataRow drRow in dtTable.Rows)
            {
                String sValue = drRow[sColumn].ToString();
                if (alList.Contains(sValue))
                    continue;
                alList.Add(sValue);
            }
            alList.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
            cbDest.Items.AddRange(alList.ToArray());
            if (cbDest.Items.Contains(sOldValue))
                cbDest.Text = sOldValue;
            else
                if (cbDest.Items.Count > 0)
                    cbDest.SelectedIndex = 0;
        }
        private void cb_Airline_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtAirline = Donnees.getTable("Input", cb_Airline.Text);
        }
        private void ChangeDate(DateTimePicker dateBegin, DateTimePicker dateEnd)
        {
            if (fpdTable == null)
            {
                return;
            }
            DateTime minFPD = new DateTime(),
                     maxFPD = new DateTime();
            bool bFPD = false;
            bFPD = findMinMaxValues(out minFPD, out maxFPD);
            if (!bFPD)
                return;

            dateBegin.MaxDate = DateTimePicker.MaximumDateTime;
            dateBegin.MinDate = DateTimePicker.MinimumDateTime;
            dateEnd.MinDate = DateTimePicker.MinimumDateTime;
            dateEnd.MaxDate = DateTimePicker.MaximumDateTime;

            dateBegin.Value = minFPD.Date;
            dateEnd.Value = maxFPD.Date.AddHours(23).AddMinutes(59);

            dateBegin.MaxDate = maxFPD.Date.AddDays(2);
            dateBegin.MinDate = minFPD.Date.AddDays(-1);
            dateEnd.MinDate = dateBegin.MinDate;
            dateEnd.MaxDate = dateBegin.MaxDate;
        }
        private bool findMinMaxValues(out DateTime min, out DateTime max)
        {

            if (fpdTable == null)
            {
                min = DateTimePicker.MinimumDateTime;
                max = DateTimePicker.MinimumDateTime;
                return false;
            }
            max = OverallTools.DataFunctions.valeurMaximaleDansColonne(fpdTable, 1, 2);
            min = OverallTools.DataFunctions.valeurMinimaleDansColonne(fpdTable, 1, 2);
            if ((max == DateTime.MinValue) || (min == DateTime.MinValue))
                return false;
            return true;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (txt_TitleTable.Text.Length == 0)
            {
                MessageBox.Show("Please specify a title for the new table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (cb_FPDTable.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a flight plan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if ((cb_BagsTable.SelectedIndex == -1) && (chk_UseInfeed.Checked))
            {
                MessageBox.Show("Please select a table for the baggage table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if ((cb_LoadFactor.SelectedIndex == -1)&&(chk_UseInfeed.Checked))
            {
                MessageBox.Show("Please select a table for the load factor table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (cb_AllocationRules.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a table for the allocations.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            int iNumberOfFlight;
            if ((cb_FlightsAlowedPerBaggageClaim.SelectedIndex == -1) || (!Int32.TryParse(cb_FlightsAlowedPerBaggageClaim.Text, out iNumberOfFlight)) || (iNumberOfFlight == 0))
            {
                MessageBox.Show("Please select a valid number of baggage claim for short hall.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (cb_Aircraft.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Aircraft table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if ((cb_BagConstraint.SelectedIndex == -1) && (chkb_BagConstraint.Checked))
            {
                MessageBox.Show("Please select a table for the baggage constraint table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            int iNumberDoubleInfeed=0;

            if ((chk_UseInfeed.Checked)&&((txt_DoubleInfeed.Text.Length == 0) || (!Int32.TryParse(txt_DoubleInfeed.Text, out iNumberDoubleInfeed))))
            {
                MessageBox.Show("Please specify a valid number of reclaim with double infeed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            
            Double dInfeedSpeed = 0;

            if ((chk_UseInfeed.Checked) && ((txt_InfeedSpeed.Text.Length == 0) || (!Double.TryParse(txt_InfeedSpeed.Text, out dInfeedSpeed))))
            {
                MessageBox.Show("Please specify a infeed speed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            DataManagement.NormalTable dtAircraft = Donnees.GetTable("Input", cb_Aircraft.Text);
            int iNumberOfFlightLH;
            if ((cb_FlightsAlowedPerBaggageClaimLH.SelectedIndex == -1) || (!Int32.TryParse(cb_FlightsAlowedPerBaggageClaimLH.Text, out iNumberOfFlightLH)) || (iNumberOfFlightLH == 0))
            {
                MessageBox.Show("Please select a valid number of baggage claim for long hall.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            String sTerminal = cb_Terminal.Text;
            if (Donnees.UseAlphaNumericForFlightInfo)
            {
                System.Xml.XmlNode xnTerminal = GestionDonneesHUB2SIM.getTerminalByDescription(Donnees.getRacine(), sTerminal);
                if (xnTerminal == null)
                    sTerminal = "";
                else
                    sTerminal = xnTerminal.Attributes["Index"].Value;
            }
            int iTerminal;
            if ((cb_Terminal.SelectedIndex == -1) || (!Int32.TryParse(sTerminal, out iTerminal)) || (iTerminal == 0))
            {
                MessageBox.Show("Please select a valid terminal.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            Double dTimeStep;
            if ((txt_TimeStep.Text.Length == 0)|| (!Double.TryParse(txt_TimeStep.Text,out dTimeStep))||(dTimeStep <= 0))
            {
                MessageBox.Show("Please specify a valid step", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            Double dTimeSeparation;
            if ((txt_Separation.Text.Length == 0) || (!Double.TryParse(txt_Separation.Text, out dTimeSeparation))||(dTimeSeparation<0))
            {
                MessageBox.Show("Please specify a valid separation between 2 fligts", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            ArrayList alErrors = new ArrayList();
            try
            {

                if (Donnees.UseAlphaNumericForFlightInfo)
                    GestionDonneesHUB2SIM.ConvertFPAInformations(fpdTable, Donnees.getRacine(), false);
                Classes.GenerateAllocationTool gatReclaimAllocation
                    = new Classes.GenerateAllocationTool(txt_TitleTable.Text,
                        Classes.GenerateAllocationTool.TypeAllocation.ReclaimAllocation,
                        "Baggage Claim", 
                        fpdTable, 
                        AllocationTable, 
                        dtAirline, 
                        dtAircraft, 
                        dtp_BeginTime.Value, 
                        dtp_EndTime.Value, 
                        dTimeStep,
                        dTimeSeparation);
                gatReclaimAllocation.Terminal = iTerminal;
                gatReclaimAllocation.AllocateFlightPlan = cb_AllocateFlightPlan.Checked;
                gatReclaimAllocation.ColorByFC = cb_ColorFC.Checked;
                gatReclaimAllocation.ColorByHandler = cb_ColorHandlers.Checked;
                gatReclaimAllocation.ColorByAirline = cb_AirlineColor.Checked;
                if (chk_UseInfeed.Checked)
                {
                    gatReclaimAllocation.dtLoadFactors = dtLoadFactor;
                    gatReclaimAllocation.dtNbBagage = dtBagTable;
                    if (chkb_BagConstraint.Checked)
                    {
                        gatReclaimAllocation.dtBagConstraint = dtBagConstraint;
                    }
                    else
                    {
                        gatReclaimAllocation.dInfeedSpeed = dInfeedSpeed;
                        gatReclaimAllocation.iNumberOfDoubleInfeed = iNumberDoubleInfeed;
                    }
                    gatReclaimAllocation.sLoadFactor = GlobalNames.sLFD_A_Line_Full;

                    gatReclaimAllocation.sContainerSize = "Container size (baggage)";
                    gatReclaimAllocation.sNBContainerPerCycle = "Number of processed container per cycle";
                    gatReclaimAllocation.sDeadTimeBetweenCycle = "Dead time between two cycles (minutes)";
                }

                gatReclaimAllocation.Errors = new List<string>();


                gatReclaimAllocation.sColumnID = GlobalNames.sFPD_A_Column_ID;
                gatReclaimAllocation.sColumnFLIGHTN = GlobalNames.sFPD_A_Column_FlightN;
                gatReclaimAllocation.sColumnDate = GlobalNames.sFPD_A_Column_DATE;
                gatReclaimAllocation.sColumnTime = GlobalNames.sFPA_Column_STA;
                gatReclaimAllocation.sColumnTerminalResult = GlobalNames.sFPA_Column_TerminalReclaim;
                gatReclaimAllocation.sColumnFirstColumnResult = GlobalNames.sFPA_Column_ReclaimObject;
                gatReclaimAllocation.sColumnLastColumnResult = GlobalNames.sFPA_Column_ReclaimObject;
                gatReclaimAllocation.sColumnFlightCategory = GlobalNames.sFPD_A_Column_FlightCategory;
                gatReclaimAllocation.sColumnAircraft = GlobalNames.sFPD_A_Column_AircraftType;
                gatReclaimAllocation.sColumnAirline = GlobalNames.sFPD_A_Column_AirlineCode;
                gatReclaimAllocation.ObservedTerminal = cb_TerminalColumn.Text;

                gatReclaimAllocation.sColumnAircraftAircraft = GlobalNames.sFPAircraft_AircraftTypes;
                gatReclaimAllocation.sColumnAircraftULDLoose = GlobalNames.sTableColumn_ULDLoose;
                gatReclaimAllocation.sColumnAirlineAirline = GlobalNames.sFPAirline_AirlineCode;
                gatReclaimAllocation.sColumnAirlineGroundHandlers = GlobalNames.sFPAirline_GroundHandlers;

                gatReclaimAllocation.sOpeningOCTDesk = "Baggage Claim Opening Time (Min after STA)";
                gatReclaimAllocation.sClosingOCTDesk = "Baggage Claim Closing Time (Min after STA)";


                /*private String sContainerSize;
                private String iNBContainerPerCycle;

                private String sDeadTimeBetweenCycle;*/

                gatReclaimAllocation.IsFPAFlight = true;
                gatReclaimAllocation.ShowFlightNumber = true;


                /*gatReclaimAllocation.FlightCategoriesSH = txt_FlightCategories.Text;
                gatReclaimAllocation.ToleranceLH = iNumberOfFlightLH;
                gatReclaimAllocation.ToleranceSH = iNumberOfFlight;*/

                gatReclaimAllocation.SegregateLooseULD = cb_LooseULD.Checked;
                gatReclaimAllocation.UseFPContent = cb_FPAsBasis.Checked;
                gatReclaimAllocation.SegregateGroundHandlers = cb_GroundHandlersSegregation.Checked;

                if (gatReclaimAllocation.AllocateDesks())
                {
                    dtResults = gatReclaimAllocation.TableResultat;
                    vmResults = gatReclaimAllocation.AllocationVisualisation;
                }
            }
            catch (Exception excep)
            {
                dtResults = null;
                alErrors.Clear();
                alErrors.Add("Err00717 : Some errors appear during the execution : " + excep.Message);
                OverallTools.ExternFunctions.PrintLogFile("Err00717: " + this.GetType().ToString() + " throw an exception: " + excep.Message);
            }
            if (Donnees.UseAlphaNumericForFlightInfo)
                GestionDonneesHUB2SIM.ConvertFPAInformations(fpdTable, Donnees.getRacine(), true);
            if (dtResults == null)
            {
                String Error = "";
                if(alErrors.Count>0)
                    Error = alErrors[0].ToString();

                MessageBox.Show("An error occurred during the analysis of the flight plan table " + Error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (alErrors.Count > 0)
            {
                if (MessageBox.Show("Some problems appear. The number of Make-Up may be not enough. Would you to see the errors list ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Errors err = new Errors(alErrors);
                    err.Show();   //err.ShowDialog();// >> Task #10347 Pax2Sim - Errors window - modeless
                }
            }
        }
        private void cb_FPAsBasis_CheckedChanged(object sender, EventArgs e)
        {
            bool bSegregationUse = !cb_FPAsBasis.Checked && !chk_UseInfeed.Checked;
            cb_GroundHandlersSegregation.Enabled = bSegregationUse;
            if (cb_GroundHandlersSegregation.Checked && !bSegregationUse)
                cb_GroundHandlersSegregation.Checked = false;
            cb_LooseULD.Enabled = bSegregationUse;
            if (cb_LooseULD.Checked && !bSegregationUse)
                cb_LooseULD.Checked = false;
        }

        private void cb_LooseULD_CheckedChanged(object sender, EventArgs e)
        {
            bool bEnabled = cb_GroundHandlersSegregation.Checked || cb_LooseULD.Checked;
            cb_FPAsBasis.Enabled = !bEnabled;
            if (cb_FPAsBasis.Checked && bEnabled)
                cb_FPAsBasis.Checked = false;
        }

        private void cb_TerminalColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            fpdTable = Donnees.getTable("Input", cb_FPDTable.Text);
            ChangeTerminalIndex(fpdTable, cb_TerminalColumn.Text, cb_Terminal);
        }

        private void cb_BagsTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataManagement.NormalTable TableTmp = Donnees.GetTable("Input", cb_BagsTable.Text);
            if(!(TableTmp is DataManagement.ExceptionTable))
            {
                dtBagTable = null;
                return;
            }
            dtBagTable = (DataManagement.ExceptionTable)TableTmp;
        }


        private void cb_BagConstraint_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataManagement.NormalTable TableTmp = Donnees.GetTable("Input", cb_BagConstraint.Text);
            if (!(TableTmp is DataManagement.ExceptionTable))
            {
                dtBagConstraint = null;
                return;
            }
            dtBagConstraint = (DataManagement.ExceptionTable)TableTmp;
        }
        private void cb_LoadFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataManagement.NormalTable TableTmp = Donnees.GetTable("Input", cb_LoadFactor.Text);
            if (!(TableTmp is DataManagement.ExceptionTable))
            {
                dtLoadFactor = null;
                return;
            }
            dtLoadFactor = (DataManagement.ExceptionTable)TableTmp;
        }

        private void chk_UseInfeed_CheckedChanged(object sender, EventArgs e)
        {
            cb_FPAsBasis_CheckedChanged(sender, e);
            cb_BagsTable.Enabled = chk_UseInfeed.Checked;
            cb_LoadFactor.Enabled = chk_UseInfeed.Checked;
            lbl_Bags.Enabled = chk_UseInfeed.Checked;
            lbl_LoadFactor.Enabled = chk_UseInfeed.Checked;
            txt_DoubleInfeed.Enabled = chk_UseInfeed.Checked;
            txt_InfeedSpeed.Enabled = chk_UseInfeed.Checked;
            lbl_Doubleinjection.Enabled = chk_UseInfeed.Checked;
            lbl_InfeedSpeed.Enabled = chk_UseInfeed.Checked;

            chkb_BagConstraint.Enabled = false;//chk_UseInfeed.Checked;
        }

        private void chkb_BagConstraint_CheckedChanged(object sender, EventArgs e)
        {
            cb_BagConstraint.Enabled = chkb_BagConstraint.Checked && chkb_BagConstraint.Enabled;
            txt_DoubleInfeed.Enabled = (!cb_BagConstraint.Enabled)&&(chk_UseInfeed.Checked);
            txt_InfeedSpeed.Enabled = (!cb_BagConstraint.Enabled)&&(chk_UseInfeed.Checked);
            lbl_Doubleinjection.Enabled = (!cb_BagConstraint.Enabled)&&(chk_UseInfeed.Checked);
            lbl_InfeedSpeed.Enabled = (!cb_BagConstraint.Enabled) && (chk_UseInfeed.Checked);
        }
    }
}