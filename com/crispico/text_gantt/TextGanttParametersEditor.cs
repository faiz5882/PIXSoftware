using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.com.crispico.text_gantt
{
    public partial class TextGanttParametersEditor : Form
    {
        private const string DATE_TIME_PICKER_FORMAT = "dd/MM/yyyy HH:mm";
        private const int DEFAULT_SAMPLING_STEP = 1;
        private static readonly List<double> samplingStepValues = new List<double>(new double[] { 1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60 });

        private DataTable fpiTable;
        private TextGanttParameters parameters;

        public TextGanttParameters getParameters()
        {
            return parameters;
        }

        private bool usingManualTimeInterval()
        {
            return manualTimeIntervalCheckBox.Checked;
        }

        private TimeInterval getSelectedTimeInterval()
        {
            return new TimeInterval(fromDateTimePicker.Value, toDateTimePicker.Value);
        }

        private double getSelectedSamplingStep()
        {
            double value = -1;
            if (samplingStepComboBox.SelectedItem == null)
                return value;
            if (!Double.TryParse(samplingStepComboBox.SelectedItem.ToString(), out value))
                value = -1;
            return value;
        }
        
        public TextGanttParametersEditor(DataTable fpiTable, TextGanttParameters parameters)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            this.fpiTable = fpiTable;
            this.parameters = parameters;

            customizeEditorComponents();

            if (parameters != null)
                updateEditorWithInitialParameters(parameters);
        }
        
        private void customizeEditorComponents()
        {
            fromDateTimePicker.CustomFormat = DATE_TIME_PICKER_FORMAT;
            toDateTimePicker.CustomFormat = DATE_TIME_PICKER_FORMAT;

            samplingStepValues.ToList().ForEach(item => samplingStepComboBox.Items.Add(item));
        }

        private void updateEditorWithInitialParameters(TextGanttParameters initialParameters)
        {
            manualTimeIntervalCheckBox.Checked = initialParameters.getUseManualTimeInterval();
            // the check-box event for Checked must be triggered in any case (even if the check-box remains unchecked) so that the UI updates
            manualTimeIntervalCheckBox_CheckedChanged(null, null);

            if (samplingStepComboBox.Items.Contains(initialParameters.getSamplingStep()))
                samplingStepComboBox.SelectedIndex = samplingStepComboBox.Items.IndexOf(initialParameters.getSamplingStep());
        }

        private void manualTimeIntervalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            manualTimeIntervalGroupBox.Enabled = usingManualTimeInterval();
            DateTime fromDate = usingManualTimeInterval() ? parameters.getTimeInterval().fromDate : parameters.getFpiDefaultTimeInterval().fromDate;
            DateTime toDate = usingManualTimeInterval() ? parameters.getTimeInterval().toDate : parameters.getFpiDefaultTimeInterval().toDate;
            
            fromDateTimePicker.Value = (fromDate < fromDateTimePicker.MinDate || fromDate > fromDateTimePicker.MaxDate) ? fromDateTimePicker.MinDate : fromDate;
            toDateTimePicker.Value = (toDate < toDateTimePicker.MinDate || toDate > toDateTimePicker.MaxDate) ? toDateTimePicker.MinDate : toDate;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            parameters.setUseManualTimeInterval(usingManualTimeInterval());
            parameters.setManualTimeInterval(getSelectedTimeInterval());
            parameters.setSamplingStep(getSelectedSamplingStep());
        }


    }
}
