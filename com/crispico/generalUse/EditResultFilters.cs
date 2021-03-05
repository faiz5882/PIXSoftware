using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.com.crispico.BHS_Analysis;
using SIMCORE_TOOL.Prompt;

namespace SIMCORE_TOOL.com.crispico.generalUse
{
    public partial class EditResultFilters : Form
    {
        public List<AnalysisResultFilter> resultFilters;
        private GestionDonneesHUB2SIM Donnees;

        private List<AnalysisResultFilter> temporaryLinkedResultsFilters = new List<AnalysisResultFilter>();
        public List<string> flowTypes = new List<string>();    // >> Task #15683 PAX2SIM - Result Filters - Split by Flow Type

        public EditResultFilters(List<AnalysisResultFilter> _resultFilters, List<string> _flowTypes,
            GestionDonneesHUB2SIM _Donnees)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            resultFilters = new List<AnalysisResultFilter>();
            resultFilters.AddRange(_resultFilters);
            temporaryLinkedResultsFilters.Clear();
            temporaryLinkedResultsFilters.AddRange(resultFilters);
            Donnees = _Donnees;

            if (_flowTypes != null)
            {
                flowTypes = new List<string>();
                flowTypes.AddRange(_flowTypes);
            }
            
            setUpResultProfilesCombobox();
            setUpFlowTypeComboBox();
            updateSelectedFlowsLabel(flowTypes);
        }

        private void setUpFlowTypeComboBox()    // >> Task #15683 PAX2SIM - Result Filters - Split by Flow Type        
        {
            flowTypeComboBox.Items.Clear();
            flowTypeComboBox.Items.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
            flowTypeComboBox.Items.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
            flowTypeComboBox.Items.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
            flowTypeComboBox.SelectedIndex = 0;
        }

        private void setUpResultProfilesCombobox()
        {
            comboboxResultsProfile.Items.Clear();
            comboboxResultsProfile.Items.Add(SIM_Scenarios_Assistant.BASIC_FILTER_PROFILE);
            comboboxResultsProfile.Items.Add(SIM_Scenarios_Assistant.ADVANCED_FILTER_PROFILE);
            comboboxResultsProfile.Items.Add(SIM_Scenarios_Assistant.CUSTOM_FILTER_PROFILE);
            comboboxResultsProfile.SelectedIndex = 0;
        }

        private void comboboxResultsProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateShownResultProfiles();  
        }

        // >> Task #15683 PAX2SIM - Result Filters - Split by Flow Type
        private void flowTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateShownResultProfiles();
        }

        private void updateShownResultProfiles()
        {
            if (comboboxResultsProfile.SelectedItem == null)
                //|| flowTypeComboBox.SelectedItem == null)
            {
                return;
            }

            string selectedProfile = comboboxResultsProfile.SelectedItem.ToString();
            //string selectedFlowTypeVisualName = flowTypeComboBox.SelectedItem.ToString();
            profilesPanel.Controls.Clear();

            List<AnalysisResultFilter> shownResultFilters = new List<AnalysisResultFilter>();
            if (selectedProfile == SIM_Scenarios_Assistant.BASIC_FILTER_PROFILE)
            {
                shownResultFilters = filterListOfResultFiltersByFlowTypeVisualName(Donnees.globalBasicAnalysisResultFilterList, flowTypes);
                addResultFiltersToPanel(shownResultFilters, profilesPanel);
            }
            else if (selectedProfile == SIM_Scenarios_Assistant.ADVANCED_FILTER_PROFILE)
            {
                shownResultFilters = filterListOfResultFiltersByFlowTypeVisualName(Donnees.globalAdvancedAnalysisResultFilterList, flowTypes);
                addResultFiltersToPanel(shownResultFilters, profilesPanel);
            }
            else if (selectedProfile == SIM_Scenarios_Assistant.CUSTOM_FILTER_PROFILE)
            {
                shownResultFilters = filterListOfResultFiltersByFlowTypeVisualName(Donnees.globalCustomAnalysisResultFilterList, flowTypes);
                addResultFiltersToPanel(shownResultFilters, profilesPanel);
            }
            refreshAnalysisResultsFiltersTab(temporaryLinkedResultsFilters);

            activateAllCheckBox.Checked = areAllActivateCheckBoxesChecked();
        }

        private List<AnalysisResultFilter> filterListOfResultFiltersByFlowTypeVisualName(List<AnalysisResultFilter> resultFilters, List<string> flowTypes)
        {
            List<AnalysisResultFilter> result = new List<AnalysisResultFilter>();
            if (resultFilters == null)
                return result;

            foreach (AnalysisResultFilter rf in resultFilters)
            {
                bool useResultFilter = false;
                foreach (string flowType in flowTypes)
                {
                    if (flowType == AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME
                        && (rf.belongsToTechnicalFlowType(AnalysisResultFilter.ORIGINATING_TECHNICAL_FLOW_TYPE) || rf.belongsToTechnicalFlowType(AnalysisResultFilter.TRANSFERRING_TECHNICAL_FLOW_TYPE)))
                    {
                        useResultFilter = true;
                        break;
                    }
                    else if (flowType == AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME && rf.belongsToTechnicalFlowType(AnalysisResultFilter.ORIGINATING_TECHNICAL_FLOW_TYPE))
                    {
                        useResultFilter = true;
                        break;
                    }
                    else if (flowType == AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME && rf.belongsToTechnicalFlowType(AnalysisResultFilter.TRANSFERRING_TECHNICAL_FLOW_TYPE))
                    {
                        useResultFilter = true;
                        break;
                    }
                }
                if (useResultFilter)
                    result.Add(rf);
            }
            return result;
        }
        // << Task #15683 PAX2SIM - Result Filters - Split by Flow Type

        private void addResultFiltersToPanel(List<AnalysisResultFilter> analysisResultFilterList, Panel profilesPanel)
        {
            int groupBoxXLocation = 60;
            int groupBoxYLocation = 0;
            int groupBoxChildLabelYLocation = 9;
            Size groupBoxSize = new Size(groupBoxReference.Width, 25);

            for (int i = 0; i < analysisResultFilterList.Count; i++)
            {
                AnalysisResultFilter resultFilter = analysisResultFilterList[i];
                bool isLinkedToScenario = (resultFilter.hasSameConfigurationAsOneFromList(resultFilters));

                string groupBoxUniqueId = i.ToString();
                addGroupBoxByResultFitler(profilesPanel, resultFilter, groupBoxSize, groupBoxXLocation,
                    groupBoxYLocation, groupBoxChildLabelYLocation, groupBoxUniqueId, null);
                addCheckBoxToControl(profilesPanel, groupBoxUniqueId, activateReferenceLabel.Location.X + 16, groupBoxYLocation + 9,
                    isLinkedToScenario, true, new EventHandler(linkToScenarioCheckBox_Click));
                groupBoxYLocation += groupBoxSize.Height + 4;
            }
        }

        public void addGroupBoxByResultFitler(Panel parent, AnalysisResultFilter resultFilter,
            Size groupBoxSize, int groupBoxXLocation, int groupBoxYLocation, int groupBoxChildLabelYLocation,
            string groupBoxTag, EventHandler checkBoxEventHandler)
        {
            GroupBox gb = new GroupBox();
            gb.Tag = groupBoxTag;
            gb.Text = "";
            gb.Size = groupBoxSize;
            gb.Location = new Point(groupBoxXLocation, groupBoxYLocation);

            bool enableCheckBox = false;

            addLabelToGroupBox(gb, SIM_Scenarios_Assistant.nameLabelTag, resultFilter.filterName, nameLabelReference.Location.X, groupBoxChildLabelYLocation,
                fromLabelReference.Location.X - nameLabelReference.Location.X, 13);
            addLabelToGroupBox(gb, SIM_Scenarios_Assistant.fromLabelTag, resultFilter.fromStationCode, fromLabelReference.Location.X, groupBoxChildLabelYLocation, 100, 13);
            addLabelToGroupBox(gb, SIM_Scenarios_Assistant.fromTimeTypeLabelTag, resultFilter.fromStationTimeType, fromTimeTypeLabelReference.Location.X, groupBoxChildLabelYLocation, 30, 13);
            addLabelToGroupBox(gb, SIM_Scenarios_Assistant.toLabelTag, resultFilter.toStationCode, toLabelReference.Location.X, groupBoxChildLabelYLocation, 100, 13);
            addLabelToGroupBox(gb, SIM_Scenarios_Assistant.toTimeTypeLabelTag, resultFilter.toStationTimeType, toTimeTypeLabelReference.Location.X, groupBoxChildLabelYLocation, 30, 13);

            addCheckBoxToControl(gb, SIM_Scenarios_Assistant.withFromSegCheckBoxTag, fromSegrLabelReference.Location.X + 8, groupBoxChildLabelYLocation, resultFilter.withFromSegregation,
                enableCheckBox, checkBoxEventHandler);
            addCheckBoxToControl(gb, SIM_Scenarios_Assistant.withToSegCheckBoxTag, toSegrLabelReference.Location.X + 3, groupBoxChildLabelYLocation, resultFilter.withToSegregation,
                enableCheckBox, checkBoxEventHandler);
            addCheckBoxToControl(gb, SIM_Scenarios_Assistant.withRecircCheckBoxTag, recircLabelReference.Location.X + 27, groupBoxChildLabelYLocation, resultFilter.withRecirculation,
                enableCheckBox, checkBoxEventHandler);
            addCheckBoxToControl(gb, SIM_Scenarios_Assistant.excludeEbsCheckBoxTag, excludeEBSLabelReference.Location.X + 27, groupBoxChildLabelYLocation, resultFilter.excludeEBSStorageTime,
                enableCheckBox, checkBoxEventHandler);
            addCheckBoxToControl(gb, SIM_Scenarios_Assistant.generateISTCheckBoxTag, generateISTLabelReference.Location.X + 27, groupBoxChildLabelYLocation, resultFilter.generateIST,
                enableCheckBox, checkBoxEventHandler);
            parent.Controls.Add(gb);
        }
        public static void addLabelToGroupBox(GroupBox gb, string labelTagPrefix, string labelText, int locationX, int locationY, int width, int height)
        {
            Label label = new Label();
            label.Tag = labelTagPrefix;
            gb.Controls.Add(label);
            label.Text = labelText;
            label.Size = new Size(width, height);   //(100, 13);
            label.Location = new Point(locationX, locationY);
        }
        public static void addCheckBoxToControl(Control control, string tag, int locationX, int locationY, bool isChecked, bool isEnabled,
            EventHandler eventHandler)
        {
            CheckBox checkBox = new CheckBox();
            control.Controls.Add(checkBox);
            checkBox.Text = "";
            checkBox.Size = new Size(15, 14);
            checkBox.Tag = tag;
            checkBox.Checked = isChecked;
            checkBox.Enabled = isEnabled;
            checkBox.Location = new Point(locationX, locationY);
            if (eventHandler != null)
                checkBox.CheckedChanged += eventHandler;//checkBox.Click += eventHandler;
        }

        public void linkToScenarioCheckBox_Click(object sender, EventArgs e)
        {
            if (sender != null && sender is CheckBox)
            {
                CheckBox currentCheckBox = (CheckBox)sender;
                string correspondingGroupBoxUniqueId = "";
                if (currentCheckBox.Tag != null)
                    correspondingGroupBoxUniqueId = currentCheckBox.Tag.ToString();
                if (correspondingGroupBoxUniqueId == "" || correspondingGroupBoxUniqueId == null || currentCheckBox.Parent == null)
                    return;
                AnalysisResultFilter correspondingResultFilter = getResultFilterByGroupBoxUniqueId(currentCheckBox.Parent, correspondingGroupBoxUniqueId);
                if (correspondingResultFilter != null)
                {
                    if (currentCheckBox.Checked && !correspondingResultFilter.hasSameConfigurationAndNameAsOneFromList(temporaryLinkedResultsFilters))
                        temporaryLinkedResultsFilters.Add(correspondingResultFilter);
                    if (!currentCheckBox.Checked && correspondingResultFilter.hasSameConfigurationAndNameAsOneFromList(temporaryLinkedResultsFilters))
                        correspondingResultFilter.removeFromGivenListByNameAndConfiguration(temporaryLinkedResultsFilters);
                }
                if (!currentCheckBox.Checked)
                    activateAllCheckBox.Checked = false;
                else if (areAllActivateCheckBoxesChecked())
                    activateAllCheckBox.Checked = true;
            }
        }

        public AnalysisResultFilter getResultFilterByGroupBoxUniqueId(Control parent, string groupBoxUniqueId)
        {
            AnalysisResultFilter correspondingResultFilter = null;
            foreach (Control profilesPanelChild in parent.Controls)
            {
                if (profilesPanelChild is GroupBox && ((GroupBox)profilesPanelChild).Tag != null
                    && ((GroupBox)profilesPanelChild).Tag.ToString() == groupBoxUniqueId)
                {
                    GroupBox parentGroupBox = (GroupBox)profilesPanelChild;
                    correspondingResultFilter = getResultFilterByGroupBox(parentGroupBox);
                    break;
                }
            }
            return correspondingResultFilter;
        }

        public static AnalysisResultFilter getResultFilterByGroupBox(GroupBox groupBox)
        {
            string name = "";
            string fromStationCode = "";
            string fromTimeType = "";
            string toStationCode = "";
            string toTimeType = "";
            bool withFromSegr = false;
            bool withToSegr = false;
            bool withRecirc = false;
            bool excludeEBS = false;
            bool generateIST = false;
            foreach (Control child in groupBox.Controls)
            {
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == SIM_Scenarios_Assistant.nameLabelTag)
                    name = ((Label)child).Text;
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == SIM_Scenarios_Assistant.fromLabelTag)
                    fromStationCode = ((Label)child).Text;
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == SIM_Scenarios_Assistant.fromTimeTypeLabelTag)
                    fromTimeType = ((Label)child).Text;
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == SIM_Scenarios_Assistant.toLabelTag)
                    toStationCode = ((Label)child).Text;
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == SIM_Scenarios_Assistant.toTimeTypeLabelTag)
                    toTimeType = ((Label)child).Text;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == SIM_Scenarios_Assistant.withFromSegCheckBoxTag)
                    withFromSegr = ((CheckBox)child).Checked;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == SIM_Scenarios_Assistant.withToSegCheckBoxTag)
                    withToSegr = ((CheckBox)child).Checked;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == SIM_Scenarios_Assistant.withRecircCheckBoxTag)
                    withRecirc = ((CheckBox)child).Checked;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == SIM_Scenarios_Assistant.excludeEbsCheckBoxTag)
                    excludeEBS = ((CheckBox)child).Checked;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == SIM_Scenarios_Assistant.generateISTCheckBoxTag)
                    generateIST = ((CheckBox)child).Checked;
            }
            AnalysisResultFilter correspondingResultFilter = new AnalysisResultFilter(name, fromStationCode, fromTimeType, toStationCode, toTimeType,
                withRecirc, withFromSegr, withToSegr, excludeEBS, generateIST);
            return correspondingResultFilter;
        }

        private void refreshAnalysisResultsFiltersTab(List<AnalysisResultFilter> analysisResultsFilters)
        {
            foreach (Control profilesPanelChild in profilesPanel.Controls)
            {
                if (profilesPanelChild is GroupBox && ((GroupBox)profilesPanelChild).Tag != null)
                {
                    GroupBox groupBox = (GroupBox)profilesPanelChild;
                    AnalysisResultFilter resultFilterOnPanel = getResultFilterByGroupBox(groupBox);
                    CheckBox checkBox = getCheckBoxFromPanelByGroupBoxUniqueId(profilesPanel, groupBox.Tag.ToString());
                    if (checkBox != null)
                        checkBox.Checked = resultFilterOnPanel.hasSameConfigurationAndNameAsOneFromList(analysisResultsFilters);
                }
            }
        }

        private CheckBox getCheckBoxFromPanelByGroupBoxUniqueId(Control panel, string groupBoxUniqueId)
        {
            CheckBox checkBox = null;
            if (panel == null || panel.Controls == null)
                return checkBox;
            foreach (Control child in panel.Controls)
            {
                if (child is CheckBox && ((CheckBox)child).Tag != null
                    && ((CheckBox)child).Tag.ToString() == groupBoxUniqueId)
                {
                    return (CheckBox)child;
                }
            }
            return checkBox;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            resultFilters.Clear();
            resultFilters.AddRange(temporaryLinkedResultsFilters);
        }

        private void editCustomProfilesButton_Click(object sender, EventArgs e)
        {
            CustomResultProfileAssistant profileAssistant = new CustomResultProfileAssistant(Donnees, Donnees.globalCustomAnalysisResultFilterList, 
                Donnees.globalBasicAnalysisResultFilterList, Donnees.globalAdvancedAnalysisResultFilterList, temporaryLinkedResultsFilters);
            DialogResult drResult = profileAssistant.ShowDialog();
            if (drResult != DialogResult.OK && drResult != DialogResult.Yes)
                return;
            if (drResult == DialogResult.OK)
                comboboxResultsProfile_SelectedIndexChanged(null, null);
        }
                
        private void activateAllCheckBox_Click(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            List<CheckBox> checkBoxes = getActivateCheckBoxes();
            foreach (CheckBox cb in checkBoxes)
                cb.Checked = activateAllCheckBox.Checked;
        }

        private List<CheckBox> getActivateCheckBoxes()
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();
            foreach (Control profilesPanelControl in profilesPanel.Controls)
            {
                if (profilesPanelControl is CheckBox && ((CheckBox)profilesPanelControl).Tag != null)
                {
                    CheckBox cb = (CheckBox)profilesPanelControl;
                    string tag = cb.Tag.ToString();
                    int tagAsInteger = -1;
                    if (Int32.TryParse(tag, out tagAsInteger))
                        checkBoxes.Add(cb);
                }
            }
            return checkBoxes;
        }

        private bool areAllActivateCheckBoxesChecked()
        {
            List<CheckBox> checkBoxes = getActivateCheckBoxes();
            foreach (CheckBox cb in checkBoxes)
                if (!cb.Checked)
                    return false;
            return true;
        }

        private void flowTypeButton_Click(object sender, EventArgs e)
        {
            string entityType = "Flow Type";
            List<string> entitiesCodes = new List<string>();
            entitiesCodes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
            //entitiesCodes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
            entitiesCodes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
            //entitiesCodes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
            entitiesCodes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);

            MultipleCheckBoxSelector flowsSelector = new MultipleCheckBoxSelector(entityType, entitiesCodes, flowTypes);
            if (flowsSelector.ShowDialog() == DialogResult.OK)
            {
                flowTypes.Clear();
                flowTypes.AddRange(flowsSelector.selectedEntityCodes);
                updateSelectedFlowsLabel(flowTypes);
                //filter the shown result filters by selected flows
                updateShownResultProfiles();
            }
        }

        private void updateSelectedFlowsLabel(List<string> selectedFlows)
        {
            selectedFlowLabel.Text = "Selected Flow Types: ";
            if (selectedFlows.Count == 0)
                selectedFlowLabel.Text += "None.";
            else
            {
                for (int i = 0; i < selectedFlows.Count; i++)
                {
                    if (i < selectedFlows.Count - 1)
                        selectedFlowLabel.Text += selectedFlows[i] + ", ";
                    else
                        selectedFlowLabel.Text += selectedFlows[i] + ".";
                }
            }
        }

    }
}
