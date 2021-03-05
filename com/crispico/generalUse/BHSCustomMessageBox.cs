using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.com.crispico.BHS_Analysis;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.com.crispico.generalUse
{
    public partial class BHSCustomMessageBox : Form
    {
        private string message;
        private bool showBHSOptions;
        public List<AnalysisResultFilter> resultFilters = null;
        private GestionDonneesHUB2SIM donnees = null;
        public List<string> flowTypes = null;
        private ScenarioParametersPopup scenarioParametersPopup;

        internal bool generateLocalIST
        {
            get
            {
                return generateLocalISTCheckBox.Checked;
            }
        }

        internal bool generateGroupIST
        {
            get
            {
                return generateGroupISTCheckBox.Checked;
            }
        }

        internal bool generateMUPSegregation
        {
            get
            {
                return generateMUPSegregationCheckBox.Checked;
            }
        }

        internal bool copyOutputTables
        {
            get
            {
                return copyOutputTablesCheckBox.Checked;
            }
        }

        public BHSCustomMessageBox()
        {
            init();
        }

        public BHSCustomMessageBox(string _message, bool _showBHSOptions,
            List<AnalysisResultFilter> _resultFilters, List<string> _flowTypes, GestionDonneesHUB2SIM _donnees, ScenarioParametersPopup scenarioParametersPopup)
        {
            showBHSOptions = _showBHSOptions;
            message = _message;
            if (_resultFilters != null)
            {
                resultFilters = new List<AnalysisResultFilter>();
                resultFilters.AddRange(_resultFilters);
            }
            if (_flowTypes != null)
            {
                flowTypes = new List<string>();
                flowTypes.AddRange(_flowTypes);
            }
            donnees = _donnees;
            this.scenarioParametersPopup = scenarioParametersPopup;
            init();
        }

        private void init()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            
            messageBoxLabel.Text = message;
            setBHSOptions();
        }

        private void setBHSOptions()
        {
            generateLocalISTCheckBox.Visible = showBHSOptions;
            generateGroupISTCheckBox.Visible = showBHSOptions;
            generateMUPSegregationCheckBox.Visible = showBHSOptions;
            editResultFitlersButton.Visible = showBHSOptions && (resultFilters != null);

            copyOutputTablesCheckBox.Checked = scenarioParametersPopup.copyOutputTables;
            generateMUPSegregationCheckBox.Checked = scenarioParametersPopup.generateMakeUpSegregation;
            generateGroupISTCheckBox.Checked = scenarioParametersPopup.generateGroupsIst;
            generateLocalISTCheckBox.Checked = scenarioParametersPopup.generateStationsIst;
        }

        private void editResultFitlersButton_Click(object sender, EventArgs e)
        {
            if (resultFilters != null && flowTypes != null && donnees != null)
            {
                EditResultFilters resultFiltersEditor = new EditResultFilters(resultFilters, flowTypes, donnees);
                if (resultFiltersEditor.ShowDialog() == DialogResult.OK)
                {
                    resultFilters.Clear();
                    resultFilters.AddRange(resultFiltersEditor.resultFilters);

                    flowTypes.Clear();
                    flowTypes.AddRange(resultFiltersEditor.flowTypes);
                }
            }
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            if (showBHSOptions)
            {
                if (resultFilters != null && resultFilters.Count == 0)
                {
                    DialogResult dr = MessageBox.Show("You did not activate any Result Filters." + Environment.NewLine + " Are you sure you want to continue?",
                       "Result Filters", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                        DialogResult = DialogResult.None;
                }
                if (flowTypes != null && flowTypes.Count == 0)
                {
                    DialogResult dr = MessageBox.Show("You did not activate any Flow Types." + Environment.NewLine + " Are you sure you want to continue?",
                       "Flow Types", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                        DialogResult = DialogResult.None;
                }
            }
        }

        public static ScenarioParametersPopup generateParametersForPopup(ParamScenario scenarioParameters)
        {
            if (scenarioParameters == null)
            {
                return ScenarioParametersPopup.defaultScenarioParametersPopup;
            }
            return new ScenarioParametersPopup(!scenarioParameters.SaveTraceMode, scenarioParameters.bhsGenerateMUPSegregation, 
                scenarioParameters.bhsGenerateGroupIST, scenarioParameters.bhsGenerateLocalIST);
        }

        public class ScenarioParametersPopup
        {
            public bool copyOutputTables { get; set; }
            public bool generateMakeUpSegregation { get; set; }
            public bool generateGroupsIst { get; set; }
            public bool generateStationsIst { get; set; }

            public ScenarioParametersPopup(bool copyOutputTables, bool generateMakeUpSegregation, bool generateGroupsIst, bool generateStationsIst)
            {
                this.copyOutputTables = copyOutputTables;
                this.generateMakeUpSegregation = generateMakeUpSegregation;
                this.generateGroupsIst = generateGroupsIst;
                this.generateStationsIst = generateStationsIst;
            }

            public static readonly ScenarioParametersPopup defaultScenarioParametersPopup = new ScenarioParametersPopup(false, false, false, false);
        }
    }
}
