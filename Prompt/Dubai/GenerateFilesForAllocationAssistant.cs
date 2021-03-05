using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt.Dubai
{
    public partial class GenerateFilesForAllocationAssistant : Form
    {
        DataTable departureFlightPlanTable;

        internal DateTime fromDate
        {
            get
            {
                return fromDatePicker.Value;
            }
        }

        internal DateTime toDate
        {
            get
            {
                return toDatePicker.Value;
            }
        }

        internal int terminalNb
        {
            get
            {
                int terminalNb = -1;
                if (terminalCombobox.SelectedItem == null
                    || terminalCombobox.SelectedItem.ToString() == "All")
                    return terminalNb;
                Int32.TryParse(terminalCombobox.SelectedItem.ToString(), out terminalNb);
                return terminalNb;
            }
        }

        public GenerateFilesForAllocationAssistant(DataTable _departureFlightPlanTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            departureFlightPlanTable = _departureFlightPlanTable;
            setUpTimePickers();
            updateTerminalCombobox();
        }

        private void setUpTimePickers()
        {
            DateTime minDate = new DateTime();
            DateTime maxDate = new DateTime();

            if (getMinMaxDateFromFlightPlan(departureFlightPlanTable,
                                            out minDate, out maxDate))
            {
                fromDatePicker.Value = minDate.Date;
                toDatePicker.Value = maxDate;

                fromDatePicker.MinDate = minDate.AddDays(-1);
                fromDatePicker.MaxDate = maxDate;

                toDatePicker.MinDate = minDate;
                toDatePicker.MaxDate = maxDate.AddDays(1);
            }

        }
        private bool getMinMaxDateFromFlightPlan(DataTable flightPlan,
            out DateTime minDate, out DateTime maxDate)
        {
            if (flightPlan == null)
            {
                minDate = DateTime.MinValue;
                maxDate = DateTime.MinValue;
                return false;
            }
            minDate = OverallTools.DataFunctions.valeurMinimaleDansColonne(flightPlan, 1, 2);
            maxDate = OverallTools.DataFunctions.valeurMaximaleDansColonne(flightPlan, 1, 2);

            if (maxDate == DateTime.MinValue
                || minDate == DateTime.MinValue)
            {
                return false;
            }
            return true;
        }

        private void updateTerminalCombobox()
        {
            int parkingTerminalColumnIndex = departureFlightPlanTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
            if (parkingTerminalColumnIndex != -1)
            {
                int maxTerminalNb = (int)OverallTools.DataFunctions.getMaxValue(departureFlightPlanTable, parkingTerminalColumnIndex);
                if (maxTerminalNb > 0)
                {
                    terminalCombobox.Items.Clear();
                    terminalCombobox.Items.Add("All");
                    for (int i = 1; i <= maxTerminalNb; i++)
                    {
                        terminalCombobox.Items.Add(i);                        
                    }
                    terminalCombobox.SelectedIndex = 0;
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (fromDatePicker.Value > toDatePicker.Value)
            {
                MessageBox.Show("Please specify valid dates", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fromDatePicker.Focus();
                DialogResult = DialogResult.None;                
                return;
            }
        }
    }
}
