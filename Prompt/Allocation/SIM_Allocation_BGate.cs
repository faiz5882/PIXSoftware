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
    public partial class SIM_Allocation_BGate : Form
    {
        GestionDonneesHUB2SIM Donnees;
        DataTable fpdTable;
        DataManagement.NormalTable AllocationTable;
        DataTable[] dtResults;
        VisualisationMode[] vmResults;
        public DataTable[] Results
        {
            get
            {
                return dtResults;
            }
        }
        public VisualisationMode[] VisualisationModeResults
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

            List<String> values = Donnees.getValidTables("Input", GlobalNames.FPDTableName);
            cb_FPDTable.Items.AddRange(values.ToArray());
            if (cb_FPDTable.Items.Count > 0)
                cb_FPDTable.SelectedIndex = 0;


            values = Donnees.getValidTables("Input", GlobalNames.FP_AirlineCodesTableName);
            cb_AirlineCode.Items.AddRange(values.ToArray());
            if (cb_AirlineCode.Items.Count > 0)
                cb_AirlineCode.SelectedIndex = 0;

            values = Donnees.getValidTables("Input", GlobalNames.FP_AircraftTypesTableName);
            cb_Aircraft.Items.AddRange(values.ToArray());
            if (cb_Aircraft.Items.Count > 0)
                cb_Aircraft.SelectedIndex = 0;

            values = Donnees.getValidTables("Input", GlobalNames.OCT_BoardGateTableName);
            cb_AllocationRules.Items.AddRange(values.ToArray());
            if (cb_AllocationRules.Items.Count > 0)
                cb_AllocationRules.SelectedIndex = 0;
            if (cb_Terminal.Items.Count > 0)
                cb_Terminal.SelectedIndex = 0;
            Donnees = gdhDonnees;
            dtResults = null;
        }
        public SIM_Allocation_BGate(GestionDonneesHUB2SIM gdhDonnees)
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
            ChangeDate(dtp_BeginTime, dtp_EndTime);
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
            if (cb_AirlineCode.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Airline table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (cb_Aircraft.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Aircraft table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            DataManagement.NormalTable dtAircraft = Donnees.GetTable("Input", cb_Aircraft.Text);
            DataTable dtAirline = Donnees.getTable("Input", cb_AirlineCode.Text);
            if (cb_AllocationRules.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a table for the allocations.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if ((cb_Terminal.SelectedIndex == -1)
                || (((!Int32.TryParse(sTerminal, out iTerminal)) || (iTerminal == 0)))
                )
            {
                MessageBox.Show("Please select a valid terminal.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            Double dTimeStep;
            if ((txt_TimeStep.Text.Length == 0) || (!Double.TryParse(txt_TimeStep.Text, out dTimeStep)) || (dTimeStep <= 0))
            {
                MessageBox.Show("Please specify a valid step", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            Double dTimeSeparation;
            if ((txt_Separation.Text.Length == 0) || (!Double.TryParse(txt_Separation.Text, out dTimeSeparation)) || (dTimeSeparation < 0))
            {
                MessageBox.Show("Please specify a valid separation between 2 fligts", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            ArrayList alErrors = new ArrayList();
            try
            {
                if (Donnees.UseAlphaNumericForFlightInfo)
                    GestionDonneesHUB2SIM.ConvertFPDInformations(fpdTable, Donnees.getRacine(), false);
                Classes.GenerateAllocationTool gatMakeUpAllocation
                    = new Classes.GenerateAllocationTool(txt_TitleTable.Text,
                        Classes.GenerateAllocationTool.TypeAllocation.BoardingGateAllocation,
                        "Boarding Gate", 
                        fpdTable,
                        AllocationTable,
                        dtAirline,
                        dtAircraft,
                        dtp_BeginTime.Value,
                        dtp_EndTime.Value,
                        dTimeStep,
                        dTimeSeparation);
                gatMakeUpAllocation.Terminal = iTerminal;
                gatMakeUpAllocation.AllocateFlightPlan = cb_AllocateFlightPlan.Checked;
                gatMakeUpAllocation.ColorByFC = cb_ColorFC.Checked;
                gatMakeUpAllocation.ColorByAirline = cb_AirlineColor.Checked;
                gatMakeUpAllocation.ObservedTerminal = cb_TerminalColumn.Text;
                gatMakeUpAllocation.UseFPContent = cb_FPAsBasis.Checked;
                gatMakeUpAllocation.Errors = new List<string>();


                gatMakeUpAllocation.sOpeningOCTDesk = GlobalNames.sOCT_CI_Line_Opening;
                gatMakeUpAllocation.sClosingOCTDesk = GlobalNames.sOCT_CI_Line_Closing;

                gatMakeUpAllocation.sColumnID = GlobalNames.sFPD_A_Column_ID;
                gatMakeUpAllocation.sColumnFLIGHTN = GlobalNames.sFPD_A_Column_FlightN;
                gatMakeUpAllocation.sColumnDate = GlobalNames.sFPD_A_Column_DATE;
                gatMakeUpAllocation.sColumnTime = GlobalNames.sFPD_Column_STD;
                gatMakeUpAllocation.sColumnTerminalResult = GlobalNames.sFPD_A_Column_TerminalGate;
                gatMakeUpAllocation.sColumnFirstColumnResult = GlobalNames.sFPD_Column_BoardingGate;
                gatMakeUpAllocation.sColumnLastColumnResult = GlobalNames.sFPD_Column_BoardingGate;
                
                gatMakeUpAllocation.sColumnFlightCategory = GlobalNames.sFPD_A_Column_FlightCategory;
                gatMakeUpAllocation.sColumnAircraft = GlobalNames.sFPD_A_Column_AircraftType;
                gatMakeUpAllocation.sColumnAirline = GlobalNames.sFPD_A_Column_AirlineCode;

                gatMakeUpAllocation.sColumnAircraftAircraft = GlobalNames.sFPAircraft_AircraftTypes;
                gatMakeUpAllocation.sColumnAircraftULDLoose = GlobalNames.sTableColumn_ULDLoose;
                gatMakeUpAllocation.sColumnAirlineAirline = GlobalNames.sFPAirline_AirlineCode;
                gatMakeUpAllocation.sColumnAirlineGroundHandlers = GlobalNames.sFPAirline_GroundHandlers;

                if (gatMakeUpAllocation.AllocateDesks())
                {
                    dtResults = gatMakeUpAllocation.TableResultat;
                    vmResults = gatMakeUpAllocation.AllocationVisualisation;
                }
                if (Donnees.UseAlphaNumericForFlightInfo)
                    GestionDonneesHUB2SIM.ConvertFPDInformations(fpdTable, Donnees.getRacine(), true);
            }
            catch (Exception excep)
            {
                dtResults = null;
                alErrors.Clear();
                alErrors.Add("Err00708 : Some errors appear during the execution : " + excep.Message);
                OverallTools.ExternFunctions.PrintLogFile("Err00708: (Some errors appear during the execution) " + this.GetType().ToString() + " throw an exception: " + excep.Message);
            }
            if (dtResults == null)
            {
                String Error = "";
                if (alErrors.Count > 0)
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

        private void cb_TerminalColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            fpdTable = Donnees.getTable("Input", cb_FPDTable.Text);
            ChangeTerminalIndex(fpdTable, cb_TerminalColumn.Text, cb_Terminal);
        }
    }
}