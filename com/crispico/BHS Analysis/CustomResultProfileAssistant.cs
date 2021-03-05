using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Prompt;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis
{
    public partial class CustomResultProfileAssistant : Form
    {
        const string NO_SELECTION = "";
        const string CUSTOM_FILTER_NAME_PREFIX = "C_";
                
        private List<ParamScenario> bhsScenarios = new List<ParamScenario>();

        List<AnalysisResultFilter> basicResultFilters = new List<AnalysisResultFilter>();
        List<AnalysisResultFilter> advancedResultFilters = new List<AnalysisResultFilter>();

        List<AnalysisResultFilter> tempCustomResultFilters = new List<AnalysisResultFilter>();
        List<AnalysisResultFilter> globalCustomResultFilters = new List<AnalysisResultFilter>();

        List<AnalysisResultFilter> currentScenarioLinkedResultFilters = new List<AnalysisResultFilter>();

        private bool filtersWereModified = false;
        private bool filtersAreBeingModified
        {
            get
            {
                return !saveChangesButton.Enabled;
            }
        }

        public CustomResultProfileAssistant(GestionDonneesHUB2SIM _donnees, List<AnalysisResultFilter> _globalCustomResultFilters,
            List<AnalysisResultFilter> _basicResultFilters, List<AnalysisResultFilter> _advancedResultFilters, 
            List<AnalysisResultFilter> _currentScenarioLinkedResultFilters)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            
            setUpCreateFilterFlowTypeComboBox();
            setUpUpdateFilterFlowTypeComboBox();

            bhsScenarios = getBHSScenarioParameters(_donnees);
            
            basicResultFilters.Clear();
            basicResultFilters.AddRange(_basicResultFilters.ToArray());
            setUpBasicAdvancedFiltersComboboxes(basicProfilesCombobox, basicResultFilters);

            advancedResultFilters.Clear();
            advancedResultFilters.AddRange(_advancedResultFilters.ToArray());
            setUpBasicAdvancedFiltersComboboxes(advancedProfilesComboBox, advancedResultFilters);

            globalCustomResultFilters = _globalCustomResultFilters;
            tempCustomResultFilters.AddRange(_globalCustomResultFilters);
            updateAvailableCustomProfilesCombobox(tempCustomResultFilters);
                        
            currentScenarioLinkedResultFilters = _currentScenarioLinkedResultFilters;   //direct link - we modify the list here

            validateTextBox();
            setUpRouteComboBoxes();

            listAllCustomFilters();
        }

        private List<ParamScenario> getBHSScenarioParameters(GestionDonneesHUB2SIM donnees)
        {
            List<ParamScenario> scenarioParameters = new List<ParamScenario>();
            if (donnees == null)
                return scenarioParameters;
            List<string> scenarioNames = donnees.getScenarioPAXBHS();
            if (scenarioNames == null)
                return scenarioParameters;
            foreach (string scenarioName in scenarioNames)
            {
                ParamScenario scenario = donnees.GetScenario(scenarioName);
                if (scenario != null && scenario.BHSSimulation)
                    scenarioParameters.Add(scenario);
            }
            return scenarioParameters;
        }

        private void updateAvailableCustomProfilesCombobox(List<AnalysisResultFilter> customResultFilters)
        {
            availableCustomProfilesComboBox.Items.Clear();
            availableCustomProfilesComboBox.Items.Add(NO_SELECTION);
            availableCustomProfilesComboBox.SelectedIndex = 0;

            if (customResultFilters == null || customResultFilters.Count == 0)
                return;

            if (updateFilterFlowTypeComboBox == null || updateFilterFlowTypeComboBox.SelectedItem == null)
                return;
            string flowType = updateFilterFlowTypeComboBox.SelectedItem.ToString();

            customResultFilters = customResultFilters.OrderBy(o => o.filterName).ToList();

            foreach (AnalysisResultFilter resultFilter in customResultFilters)
            {
                if (resultFilter != null)
                {
                    if (flowType == AnalysisResultFilter.ALL_FLOW_TYPE_VISUAL_NAME)
                    {
                        availableCustomProfilesComboBox.Items.Add(resultFilter.filterName);
                        continue;
                    }
                    if (flowType == AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME
                        && (resultFilter.belongsToTechnicalFlowType(AnalysisResultFilter.ORIGINATING_TECHNICAL_FLOW_TYPE) || resultFilter.belongsToTechnicalFlowType(AnalysisResultFilter.TRANSFERRING_TECHNICAL_FLOW_TYPE)))
                    {
                        availableCustomProfilesComboBox.Items.Add(resultFilter.filterName);
                    }
                    else if (flowType == AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME && resultFilter.belongsToTechnicalFlowType(AnalysisResultFilter.ORIGINATING_TECHNICAL_FLOW_TYPE))
                    {
                        availableCustomProfilesComboBox.Items.Add(resultFilter.filterName);
                    }
                    else if (flowType == AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME && resultFilter.belongsToTechnicalFlowType(AnalysisResultFilter.TRANSFERRING_TECHNICAL_FLOW_TYPE))
                    {
                        availableCustomProfilesComboBox.Items.Add(resultFilter.filterName);
                    }
                }
            }
        }

        private const string filterRouteSeparator = " -> ";
        private void setUpBasicAdvancedFiltersComboboxes(ComboBox combobox, List<AnalysisResultFilter> filters)
        {
            combobox.Items.Clear();
            combobox.Items.Add(NO_SELECTION);

            if (createFilterFlowTypeComboBox1.SelectedItem == null)
                return;
            string flowType = createFilterFlowTypeComboBox1.SelectedItem.ToString();

            foreach (AnalysisResultFilter filter in filters)
            {
                if (flowType == AnalysisResultFilter.ALL_FLOW_TYPE_VISUAL_NAME 
                    || (OverallTools.BagTraceAnalysis.stationCodeBelongsToFlowTypeVisualName(filter.fromStationCode, flowType)
                        && OverallTools.BagTraceAnalysis.stationCodeBelongsToFlowTypeVisualName(filter.toStationCode, flowType)))
                {
                    string filterRoute = filter.fromStationCode + filterRouteSeparator + filter.toStationCode;
                    combobox.Items.Add(filterRoute);
                }
            }
        }

        private void setUpRouteComboBoxes()
        {
            fromStationComboBox.Items.Clear();
            toStationComboBox.Items.Clear();
            fromStationComboBox.Items.Add(NO_SELECTION);
            toStationComboBox.Items.Add(NO_SELECTION);

            if (createFilterFlowTypeComboBox1.SelectedItem == null)
                return;
            string selectedFlowType = createFilterFlowTypeComboBox1.SelectedItem.ToString();

            foreach (string s in OverallTools.BagTraceAnalysis.ROUTE_STATION_CODE_LIST)
            {
                if (selectedFlowType == AnalysisResultFilter.ALL_FLOW_TYPE_VISUAL_NAME
                    || OverallTools.BagTraceAnalysis.stationCodeBelongsToFlowTypeVisualName(s, selectedFlowType))
                {
                    fromStationComboBox.Items.Add(s);
                }
            }
            foreach (string s in OverallTools.BagTraceAnalysis.ROUTE_STATION_CODE_LIST)
            {
                if (selectedFlowType == AnalysisResultFilter.ALL_FLOW_TYPE_VISUAL_NAME
                    || OverallTools.BagTraceAnalysis.stationCodeBelongsToFlowTypeVisualName(s, selectedFlowType))
                {
                    toStationComboBox.Items.Add(s);
                }
            }
            
            fromStationTimeTypeComboBox.Items.Clear();            
            foreach (string s in OverallTools.BagTraceAnalysis.ROUTE_STATION_TIME_TYPES_LIST)
                fromStationTimeTypeComboBox.Items.Add(s);
            if (fromStationTimeTypeComboBox.Items.Contains(AnalysisResultFilter.STATION_TIME_TYPE_ARRIVING))
                fromStationTimeTypeComboBox.SelectedItem = AnalysisResultFilter.STATION_TIME_TYPE_ARRIVING;

            toStationTimeTypeComboBox.Items.Clear();            
            foreach (string s in OverallTools.BagTraceAnalysis.ROUTE_STATION_TIME_TYPES_LIST)
                toStationTimeTypeComboBox.Items.Add(s);
            if (toStationTimeTypeComboBox.Items.Contains(AnalysisResultFilter.STATION_TIME_TYPE_LEAVING))
                toStationTimeTypeComboBox.SelectedItem = AnalysisResultFilter.STATION_TIME_TYPE_LEAVING;
        }

        private void setUpCreateFilterFlowTypeComboBox()
        {
            createFilterFlowTypeComboBox1.Items.Clear();
            createFilterFlowTypeComboBox1.Items.Add(AnalysisResultFilter.ALL_FLOW_TYPE_VISUAL_NAME);
            createFilterFlowTypeComboBox1.Items.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
            createFilterFlowTypeComboBox1.Items.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
            createFilterFlowTypeComboBox1.Items.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
            createFilterFlowTypeComboBox1.SelectedIndex = 0;
        }

        private void setUpUpdateFilterFlowTypeComboBox()
        {
            updateFilterFlowTypeComboBox.Items.Clear();
            updateFilterFlowTypeComboBox.Items.Add(AnalysisResultFilter.ALL_FLOW_TYPE_VISUAL_NAME);
            updateFilterFlowTypeComboBox.Items.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
            updateFilterFlowTypeComboBox.Items.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
            updateFilterFlowTypeComboBox.Items.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
            updateFilterFlowTypeComboBox.SelectedIndex = 0;
        }


        private void updateFilterFlowTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updateFilterFlowTypeComboBox.SelectedItem == null)
                return;

            string selectedFlowType = updateFilterFlowTypeComboBox.SelectedItem.ToString();
            //if (listAllCustomProfilesButton != null)            
            //    listAllCustomProfilesButton.Text = "List All " + selectedFlowType + " Custom Result Filters";

            if (availableCustomRFLabel != null)
            {
                availableCustomRFLabel.Text = "Available " + selectedFlowType + " Custom Result Filters";
                updateAvailableCustomProfilesCombobox(tempCustomResultFilters);
            }
            listAvailableCustomResultFilters();
        }

        private void availableCustomProfilesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availableCustomProfilesComboBox.SelectedItem == null)
                return;
            string selectedFilterName = availableCustomProfilesComboBox.SelectedItem.ToString();
            if (selectedFilterName != NO_SELECTION)
            {
                AnalysisResultFilter selectedResultFilter = getResultFilterByFilterName(selectedFilterName);
                if (selectedResultFilter != null)
                    refreshProfilesPanel(new List<AnalysisResultFilter>(new AnalysisResultFilter[] { selectedResultFilter }));
            }
            else
                profilesPanel.Controls.Clear();
            saveChangesButton.Enabled = true;
        }

        private AnalysisResultFilter getResultFilterByFilterName(string filterName)
        {
            foreach (AnalysisResultFilter resFil in tempCustomResultFilters)
            {
                if (resFil.filterName == filterName)
                    return resFil;
            }
            return null;
        }

        private void refreshProfilesPanel(List<AnalysisResultFilter> selectedResultFilters)
        {
            profilesPanel.Controls.Clear();
            int groupBoxYLocation = 0;
            foreach (AnalysisResultFilter resultFilter in selectedResultFilters)
            {
                GroupBox gb = addGroupBoxToPanelByResultFilter(profilesPanel, resultFilter, groupBoxYLocation);
                fillGroupBoxWithResultFilterData(gb, resultFilter);
                SIM_Scenarios_Assistant.addCheckBoxToControl(profilesPanel, gb.Tag.ToString(), deleteLabelReference.Location.X + 11, groupBoxYLocation + 9,
                    false, true, new EventHandler(deleteCheckBox_Click));
                groupBoxYLocation += gb.Size.Height + 4;
            }
        }

        private GroupBox addGroupBoxToPanelByResultFilter(Panel profilesPanel, AnalysisResultFilter resultFilter,
            int groupBoxYLocation)
        {
            int groupBoxXLocation = 59;            
            GroupBox gb = new GroupBox();
            gb.Text = "";
            gb.Tag = resultFilter.filterName;
            gb.Size = new Size(groupBoxReference.Size.Width, 32);
            gb.Location = new Point(groupBoxXLocation, groupBoxYLocation);
            profilesPanel.Controls.Add(gb);
            return gb;
        }
        
        private void fillGroupBoxWithResultFilterData(GroupBox gb, AnalysisResultFilter resultFilter)
        {
            int groupBoxChildLabelYLocation = 13;
            int groupBoxChildComboboxYLocation = 9;
            SIM_Scenarios_Assistant.addLabelToGroupBox(gb, SIM_Scenarios_Assistant.nameLabelTag, resultFilter.filterName,
                namelabelreference.Location.X, groupBoxChildLabelYLocation, fromLabelReference.Location.X - namelabelreference.Location.X, 13);//120, 13);
            SIM_Scenarios_Assistant.addLabelToGroupBox(gb, SIM_Scenarios_Assistant.fromLabelTag, resultFilter.fromStationCode,
                fromLabelReference.Location.X, groupBoxChildLabelYLocation, 100, 13);            
            addComboBoxToGroupBox(gb, SIM_Scenarios_Assistant.fromTimeTypeLabelTag, OverallTools.BagTraceAnalysis.ROUTE_STATION_TIME_TYPES_LIST.ToArray(),
                resultFilter.fromStationTimeType, fromTimeTypeLabelReference.Location.X, groupBoxChildComboboxYLocation, 45, 13, comboBox_SelectedIndexChanged);

            SIM_Scenarios_Assistant.addLabelToGroupBox(gb, SIM_Scenarios_Assistant.toLabelTag, resultFilter.toStationCode,
                toLabelReference.Location.X, groupBoxChildLabelYLocation, 100, 13);            
            addComboBoxToGroupBox(gb, SIM_Scenarios_Assistant.toTimeTypeLabelTag, OverallTools.BagTraceAnalysis.ROUTE_STATION_TIME_TYPES_LIST.ToArray(),
                resultFilter.toStationTimeType, toTimeTypeLabelReference.Location.X, groupBoxChildComboboxYLocation, 45, 13, comboBox_SelectedIndexChanged);

            SIM_Scenarios_Assistant.addCheckBoxToControl(gb, SIM_Scenarios_Assistant.withFromSegCheckBoxTag,
                fromSegrLabelReference.Location.X + 7, groupBoxChildLabelYLocation, resultFilter.withFromSegregation, true, checkbox_checkedChanged);
            SIM_Scenarios_Assistant.addCheckBoxToControl(gb, SIM_Scenarios_Assistant.withToSegCheckBoxTag,
                toSegrLabelReference.Location.X + 2, groupBoxChildLabelYLocation, resultFilter.withToSegregation, true, checkbox_checkedChanged);
            SIM_Scenarios_Assistant.addCheckBoxToControl(gb, SIM_Scenarios_Assistant.withRecircCheckBoxTag,
                recircLabelReference.Location.X + 25, groupBoxChildLabelYLocation, resultFilter.withRecirculation, true, checkbox_checkedChanged);
            SIM_Scenarios_Assistant.addCheckBoxToControl(gb, SIM_Scenarios_Assistant.excludeEbsCheckBoxTag,
                excludeEBSLabelReference.Location.X + 25, groupBoxChildLabelYLocation, resultFilter.excludeEBSStorageTime, true, checkbox_checkedChanged);
            SIM_Scenarios_Assistant.addCheckBoxToControl(gb, SIM_Scenarios_Assistant.generateISTCheckBoxTag,
                generateISTReferenceLabel.Location.X + 25, groupBoxChildLabelYLocation, resultFilter.generateIST, true, checkbox_checkedChanged);
        }

        public void addComboBoxToGroupBox(GroupBox gb, string comboboxTag, string[] items, string selectedItem, 
            int locationX, int locationY, int width, int height, EventHandler selectedIndexChangedHandler)
        {
            ComboBox cb = new ComboBox();
            gb.Controls.Add(cb);
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Location = new Point(locationX, locationY);
            cb.Size = new Size(width, height);
            cb.Tag = comboboxTag;
                        
            cb.Items.AddRange(items);
            if (cb.Items.Contains(selectedItem))
                cb.SelectedItem = selectedItem;
            cb.SelectedIndexChanged += selectedIndexChangedHandler;
        }

        private void deleteCheckBox_Click(object sender, EventArgs e)
        {
            return;
      
            if (sender != null && sender is CheckBox)
            {
                CheckBox currentCheckBox = (CheckBox)sender;
                string correspondingGroupBoxUniqueId = "";
                if (currentCheckBox.Tag != null)
                    correspondingGroupBoxUniqueId = currentCheckBox.Tag.ToString();
                if (correspondingGroupBoxUniqueId == "" || correspondingGroupBoxUniqueId == null || currentCheckBox.Parent == null)
                    return;
                GroupBox gb = getGroupBoxByGroupBoxText(currentCheckBox.Parent, correspondingGroupBoxUniqueId);
                if (gb == null)
                    return;
                AnalysisResultFilter correspondingResultFilter = SIM_Scenarios_Assistant.getResultFilterByGroupBox(gb);
                if (correspondingResultFilter != null)
                {
                    correspondingResultFilter.filterName = gb.Tag.ToString();
                    if (correspondingResultFilter.hasSameConfigurationAndNameAsOneFromList(tempCustomResultFilters))
                        correspondingResultFilter.removeFromGivenListByNameAndConfiguration(tempCustomResultFilters);
                }
            }
        }

        private GroupBox getGroupBoxByGroupBoxText(Control parent, string groupBoxText)
        {
            foreach (Control profilesPanelChild in parent.Controls)
            {
                if (profilesPanelChild is GroupBox && ((GroupBox)profilesPanelChild).Tag != null
                    && ((GroupBox)profilesPanelChild).Tag.ToString() == groupBoxText)
                {
                    return (GroupBox)profilesPanelChild;
                }
            }
            return null;
        }

        private void listAllCustomFilters()
        {
            List<AnalysisResultFilter> availableResultFilters = new List<AnalysisResultFilter>();
            foreach (AnalysisResultFilter resultFilter in tempCustomResultFilters)            
                availableResultFilters.Add(resultFilter);
            availableResultFilters = availableResultFilters.OrderBy(o => o.filterName).ToList();

            if (availableResultFilters.Count > 0)
                refreshProfilesPanel(availableResultFilters);
            saveChangesButton.Enabled = true;
        }

        private void listAvailableCustomResultFilters()
        {
            if (availableCustomProfilesComboBox.Items.Count == 0)
                return;

            availableCustomProfilesComboBox.SelectedIndex = 0;
            List<AnalysisResultFilter> availableResultFilters = new List<AnalysisResultFilter>();
            foreach (string resultFilterName in availableCustomProfilesComboBox.Items)            
            {
                AnalysisResultFilter resultFilter = getResultFilterByFilterName(resultFilterName);
                if (resultFilter != null)
                    availableResultFilters.Add(resultFilter);
            }
            availableResultFilters = availableResultFilters.OrderBy(o => o.filterName).ToList();
            if (availableResultFilters.Count > 0)
                refreshProfilesPanel(availableResultFilters);
            saveChangesButton.Enabled = true;
        }

        private void profileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            validateTextBox();
        }

        private void validateTextBox()
        {

            int maxSequenceNb = getMaxValueFromResultFilterPrefixName();
            if (maxSequenceNb != -1)
            {
                string prefix = CUSTOM_FILTER_NAME_PREFIX + maxSequenceNb + "_";
                if (!profileNameTextBox.Text.StartsWith(prefix))
                {
                    profileNameTextBox.Text = prefix;
                    profileNameTextBox.SelectionStart = profileNameTextBox.Text.Length;
                }
            }
        }

        private int getMaxValueFromResultFilterPrefixName()
        {
            int max = -1;
            if (tempCustomResultFilters.Count == 0)
                return 1;
            foreach (AnalysisResultFilter resFil in tempCustomResultFilters)
            {
                if (resFil.filterName.IndexOf("_") == -1)
                    continue;
                string partialResult = resFil.filterName.Substring(resFil.filterName.IndexOf("_") + 1);
                if (partialResult.IndexOf("_") == -1)
                    continue;
                string nb = partialResult.Substring(0, partialResult.IndexOf("_"));
                int value = -1;
                if (Int32.TryParse(nb, out value) && max < value)
                    max = value;
            }
            if (max == -1)
                return max;
            return max + 1;
        }

        private void addToCustomProfilesButton_Click(object sender, EventArgs e)
        {
            string validationMessage = validateNewCustomFilterData();
            if (validationMessage == "")
            {
                AnalysisResultFilter newResultFilter = new AnalysisResultFilter(profileNameTextBox.Text,
                    fromStationComboBox.SelectedItem.ToString(), fromStationTimeTypeComboBox.SelectedItem.ToString(),
                    toStationComboBox.SelectedItem.ToString(), toStationTimeTypeComboBox.SelectedItem.ToString(),
                    withRecircNewCheckBox.Checked, withFromSegNewCheckBox.Checked, withToSegNewCheckBox.Checked, excludeEBSNewCheckBox.Checked,
                    generateISTCheckBox.Checked);
                tempCustomResultFilters.Add(newResultFilter);
                
                updateAvailableCustomProfilesCombobox(tempCustomResultFilters);
                clearAddNewProfileForm(true);

                basicProfilesCombobox.SelectedItem = NO_SELECTION;
                advancedProfilesComboBox.SelectedItem = NO_SELECTION;

                filtersWereModified = true;
                                
                listAvailableCustomResultFilters();
            }
            else
            {
                MessageBox.Show(validationMessage, "New Custom Filter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string validateNewCustomFilterData()
        {
            string validationErrorMessage = "";            
            if (fromStationComboBox.SelectedItem == null || fromStationComboBox.SelectedItem.ToString() == NO_SELECTION)
            {
                validationErrorMessage = "Please select the From Station Type.";
                return validationErrorMessage;
            }
            if (fromStationTimeTypeComboBox.SelectedItem == null || fromStationTimeTypeComboBox.SelectedItem.ToString() == NO_SELECTION)
            {
                validationErrorMessage = "Please select the From Station Type's reference time.";
                return validationErrorMessage;
            }
            if (toStationComboBox.SelectedItem == null || toStationComboBox.SelectedItem.ToString() == NO_SELECTION)
            {
                validationErrorMessage = "Please select the To Station Type.";
                return validationErrorMessage;
            }
            if (toStationTimeTypeComboBox.SelectedItem == null || toStationTimeTypeComboBox.SelectedItem.ToString() == NO_SELECTION)
            {
                validationErrorMessage = "Please select the To Station Type's reference time.";
                return validationErrorMessage;
            }
            return validationErrorMessage;
        }

        private void clearAddNewProfileForm(bool resetProfileName)
        {
            if (resetProfileName)
                profileNameTextBox.Text = "";
            fromStationComboBox.SelectedIndex = 0;
            if (fromStationTimeTypeComboBox.Items.Contains(AnalysisResultFilter.STATION_TIME_TYPE_ARRIVING))
                fromStationTimeTypeComboBox.SelectedItem = AnalysisResultFilter.STATION_TIME_TYPE_ARRIVING;
            toStationComboBox.SelectedIndex = 0;
            if (toStationTimeTypeComboBox.Items.Contains(AnalysisResultFilter.STATION_TIME_TYPE_LEAVING))
                toStationTimeTypeComboBox.SelectedItem = AnalysisResultFilter.STATION_TIME_TYPE_LEAVING;
            withFromSegNewCheckBox.Checked = false;
            withToSegNewCheckBox.Checked = false;
            withRecircNewCheckBox.Checked = false;
            excludeEBSNewCheckBox.Checked = true;
            generateISTCheckBox.Checked = false;
        }

        private void updateCustomProfilesButton_Click(object sender, EventArgs e)
        {
            List<AnalysisResultFilter> resultFiltersFromProfilePanel = getResultFiltersFromProfilePanel();
            if (resultFiltersFromProfilePanel.Count == 0)
                return;

            List<AnalysisResultFilter> filtersMarkedForDelete = new List<AnalysisResultFilter>();
            foreach (AnalysisResultFilter resultFilter in resultFiltersFromProfilePanel)
            {
                if (isResultFilterMarkedForDeletion(resultFilter.filterName))
                    filtersMarkedForDelete.Add(resultFilter);
            }
            if (filtersMarkedForDelete.Count > 0)
            {
                string warningMessage = "The following filters will be deleted and removed from their linked scenarios if necessary:" ;
                foreach (AnalysisResultFilter filter in filtersMarkedForDelete)
                {                    
                    string scenariosAffected = "";
                    foreach (ParamScenario scenario in bhsScenarios)
                    {
                        if (scenario.analysisResultsFilters != null && filter.hasSameConfigurationAndNameAsOneFromList(scenario.analysisResultsFilters))
                            scenariosAffected += Environment.NewLine + "\t" + " - " + scenario.Name;
                    }
                    if (filter.hasSameConfigurationAndNameAsOneFromList(currentScenarioLinkedResultFilters))
                        scenariosAffected += Environment.NewLine + "\t" + " - " + "Current Scenario";

                    if (scenariosAffected != "")                    
                        warningMessage += Environment.NewLine + " * " + filter.filterName + ", linked to the following scenario(s):" + scenariosAffected;                    
                    else                    
                        warningMessage += Environment.NewLine + " * " + filter.filterName + ", not linked to any scenario.";                    
                }
                warningMessage += Environment.NewLine + "Do you want to proceed with the update?";
                DialogResult dr = MessageBox.Show(warningMessage, "Delete Result Filters", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No)
                    return;
            }

            List<AnalysisResultFilter> updatedFitlers = new List<AnalysisResultFilter>();
            foreach (AnalysisResultFilter resultFilter in resultFiltersFromProfilePanel)
            {
                foreach (AnalysisResultFilter tempCustomResultFilter in tempCustomResultFilters)
                {
                    if (tempCustomResultFilter.filterName == resultFilter.filterName)                    
                        updatedFitlers.Add(tempCustomResultFilter);
                }
            }
            foreach (AnalysisResultFilter updatedFilter in updatedFitlers)            
                updatedFilter.removeFromGivenListByNameAndConfiguration(tempCustomResultFilters);            
            foreach (AnalysisResultFilter resultFilter in resultFiltersFromProfilePanel)
            {
                if (!isResultFilterMarkedForDeletion(resultFilter.filterName))
                    tempCustomResultFilters.Add(resultFilter);
            }
            updateAvailableCustomProfilesCombobox(tempCustomResultFilters);
            clearAddNewProfileForm(true);
            saveChangesButton.Enabled = true;
            filtersWereModified = true;
            
            listAvailableCustomResultFilters();
        }

        private List<AnalysisResultFilter> getResultFiltersFromProfilePanel()
        {
            List<AnalysisResultFilter> resultFilters = new List<AnalysisResultFilter>();
            foreach (Control control in profilesPanel.Controls)
            {
                if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    if (gb != null)
                    {
                        AnalysisResultFilter resultFilter = SIM_Scenarios_Assistant.getResultFilterByGroupBox(gb);
                        if (resultFilter != null)
                            resultFilters.Add(resultFilter);
                    }
                }
            }
            return resultFilters;
        }

        private bool isResultFilterMarkedForDeletion(string filterName)
        {
            foreach (Control control in profilesPanel.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox cb = (CheckBox)control;
                    if (cb != null && cb.Tag != null && cb.Tag.ToString() == filterName)
                    {
                        return cb.Checked;
                    }
                }
            }
            return false;
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            globalCustomResultFilters.Clear();
            globalCustomResultFilters.AddRange(tempCustomResultFilters);
            globalCustomResultFilters = globalCustomResultFilters.OrderBy(o => o.filterName).ToList();

            removeDeletedCustomFiltersFromOtherScenarios(bhsScenarios, globalCustomResultFilters);
        }

        private void removeDeletedCustomFiltersFromOtherScenarios(List<ParamScenario> bhsScenarios,
            List<AnalysisResultFilter> currentCustomResultFilters)
        {
            if (bhsScenarios == null || currentCustomResultFilters == null)
                return;
            foreach (ParamScenario scenario in bhsScenarios)
            {
                if (scenario.analysisResultsFilters == null)
                    continue;
                List<AnalysisResultFilter> filtersToDelete = new List<AnalysisResultFilter>();
                foreach (AnalysisResultFilter scenarioFilter in scenario.analysisResultsFilters)
                {
                    //if (scenarioFilter.isCustomFilter() && !scenarioFilter.hasSameConfigurationAndNameAsOneFromList(currentCustomResultFilters))
                        //filtersToDelete.Add(scenarioFilter);
                    if (scenarioFilter.isCustomFilter())
                    {
                        bool foundFilterWithSameName = false;
                        foreach (AnalysisResultFilter currentCustom in currentCustomResultFilters)
                        {
                            if (currentCustom.filterName == scenarioFilter.filterName)
                            {
                                scenarioFilter.copyConfigurationFromGivenFilter(currentCustom);
                                foundFilterWithSameName = true;
                                break;
                            }
                        }
                        if (!foundFilterWithSameName)
                            filtersToDelete.Add(scenarioFilter);
                    }
                }
                foreach (AnalysisResultFilter deleteMarkedFilter in filtersToDelete)
                {
                    if (!scenario.deletedCustomAnalysisResultsFilters.Contains(deleteMarkedFilter.filterName))
                        scenario.deletedCustomAnalysisResultsFilters.Add(deleteMarkedFilter.filterName);
                    scenario.analysisResultsFilters.Remove(deleteMarkedFilter);
                }
            }
            //remove from the currently created scenario
            List<AnalysisResultFilter> deleteFilters = new List<AnalysisResultFilter>();
            foreach (AnalysisResultFilter scenarioFilter in currentScenarioLinkedResultFilters)
            {
                //if (scenarioFilter.isCustomFilter() && !scenarioFilter.hasSameConfigurationAndNameAsOneFromList(currentCustomResultFilters))
                //    deleteFilters.Add(scenarioFilter);     
                if (scenarioFilter.isCustomFilter())
                {
                    bool foundFilterWithSameName = false;
                    foreach (AnalysisResultFilter currentCustom in currentCustomResultFilters)
                    {
                        if (currentCustom.filterName == scenarioFilter.filterName)
                        {
                            scenarioFilter.copyConfigurationFromGivenFilter(currentCustom);
                            foundFilterWithSameName = true;
                            break;
                        }
                    }
                    if (!foundFilterWithSameName)
                        deleteFilters.Add(scenarioFilter);
                }
            }
            foreach (AnalysisResultFilter del in deleteFilters)
                currentScenarioLinkedResultFilters.Remove(del);
        }

        private void profilesCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null && sender is ComboBox)
            {
                ComboBox cb = (ComboBox)sender;
                if (cb.SelectedItem != null && cb.SelectedItem.ToString().Contains(filterRouteSeparator))
                {
                    string[] filterNames = cb.SelectedItem.ToString().Split(new string[] { filterRouteSeparator }, StringSplitOptions.None);
                    if (filterNames.Length == 2)
                    {
                        if (fromStationComboBox.Items.Contains(filterNames[0]))
                            fromStationComboBox.SelectedItem = filterNames[0];
                        if (toStationComboBox.Items.Contains(filterNames[1]))
                            toStationComboBox.SelectedItem = filterNames[1];
                        withFromSegNewCheckBox.Checked = false;
                        withToSegNewCheckBox.Checked = false;
                        withRecircNewCheckBox.Checked = false;
                        excludeEBSNewCheckBox.Checked = true;
                        generateISTCheckBox.Checked = false;
                    }
                }
            }
        }

        private void basicProfilesCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (basicProfilesCombobox.SelectedItem != null && basicProfilesCombobox.SelectedItem.ToString() != NO_SELECTION)
            {
                advancedProfilesComboBox.SelectedItem = NO_SELECTION;
                profilesCombobox_SelectedIndexChanged(sender, e);
            }
            else
            {
                clearAddNewProfileForm(false);
            }
        }

        private void advancedProfilesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (advancedProfilesComboBox.SelectedItem != null && advancedProfilesComboBox.SelectedItem.ToString() != NO_SELECTION)
            {
                basicProfilesCombobox.SelectedItem = NO_SELECTION;
                profilesCombobox_SelectedIndexChanged(sender, e);
            }
            else
            {
                clearAddNewProfileForm(false);
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null && sender is ComboBox)
            {
                ComboBox cb = (ComboBox)sender;
                if (cb.Parent != null && cb.Parent is GroupBox)
                {
                    GroupBox gb = (GroupBox)cb.Parent;
                    gb.ForeColor = Color.Red;
                    saveChangesButton.Enabled = false;                    
                }
            }
        }

        private void checkbox_checkedChanged(object sender, EventArgs e)
        {
            if (sender != null && sender is CheckBox)
            {
                CheckBox cb = (CheckBox)sender;
                if (cb.Parent != null && cb.Parent is GroupBox)
                {
                    GroupBox gb = (GroupBox)cb.Parent;
                    gb.ForeColor = Color.Red;
                    saveChangesButton.Enabled = false;
                }
            }
        }

        private void cancelAllChangesButton_Click(object sender, EventArgs e)
        {
            if (filtersWereModified || filtersAreBeingModified)
            {
                if (MessageBox.Show("Are you sure you want to cancel all changes?", "Cancel All Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.No)
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }
        }

        private void createFilterFlowTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setUpBasicAdvancedFiltersComboboxes(basicProfilesCombobox, basicResultFilters);
            setUpBasicAdvancedFiltersComboboxes(advancedProfilesComboBox, advancedResultFilters);
            setUpRouteComboBoxes();
        }





    }
}
