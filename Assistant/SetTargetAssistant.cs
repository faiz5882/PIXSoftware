using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Classes;
using System.Collections;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Assistant
{
    // >> Task #10156 Pax2Sim - Statistic dev - Target
    public partial class SetTargetAssistant : Form
    {
        private const String COMBOBOX_ITEM_NO_SELECTION = "No Selection";

        internal const String PROCESS_TYPE_AIRPORT = "Airport";
        private const String PROCESS_TYPE_TERMINAL = "Terminal";
        private const String PROCESS_TYPE_LEVEL = "Level";
        private const String PROCESS_TYPE_GROUP = "Group";
        private const String PROCESS_TYPE_DESK = "Desk";
                
        private string[] statisticTypesList = new string[] {GlobalNames.STATISTIC_TYPE_OCCUPATION, GlobalNames.STATISTIC_TYPE_DWELL_AREA,
            GlobalNames.STATISTIC_TYPE_TIME, GlobalNames.STATISTIC_TYPE_REMAINING_TIME, GlobalNames.STATISTIC_TYPE_UTILIZATION};

        private string[] overviewForDeskAndGroupStatisticAttributesList = new string[] { GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_OF_PAX,
                               GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_PROCESSED_PAX, GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_TRANSFER_PAX,
                               GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_MISSED_PAX, GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_STOPPED_PAX, 
                               GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_LOST_PAX};

        private string[] overviewForAirportTerminalAndLevelStatisticAttributesList = new string[] { GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_OF_PAX,
                               GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_TRANSFER_PAX, GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_MISSED_PAX,
                               GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_STOPPED_PAX, GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_LOST_PAX };

        private string[] occupationForDeskAndGroupStatisticAttributesList = new string[] { GlobalNames.STATISTIC_ATTRIBUTE_QUEUE_OCCUPATION, GlobalNames.STATISTIC_ATTRIBUTE_OCCUPATION_INPUT,
                               GlobalNames.STATISTIC_ATTRIBUTE_OCCUPATION_OUTPUT, GlobalNames.STATISTIC_ATTRIBUTE_THROUGHPUT_INPUT, GlobalNames.STATISTIC_ATTRIBUTE_THROUGHPUT_OUTPUT};

        private string[] occupationForAirportTerminalAndLevelStatisticAttributesList = new string[] { GlobalNames.STATISTIC_ATTRIBUTE_AREA_OCCUPATION };

        private string[] dwellAreaStatisticAttributeList = new string[] { GlobalNames.STATISTIC_ATTRIBUTE_DWELL_AREA };

        private string[] remainingTimeStatisticAttributesList = new string[] { GlobalNames.STATISTIC_ATTRIBUTE_REMAINING_TIME };

        private string[] timeStatisticAttributesList = new string[] { GlobalNames.TIME_STATS_TOTAL_TIME, GlobalNames.TIME_STATS_DELAY_TIME,
                GlobalNames.TIME_STATS_PROCESS_TIME, GlobalNames.TIME_STATS_WAITING_GROUP_TIME, GlobalNames.TIME_STATS_WAITING_DESK_TIME };

        private string[] groupUtilizationStatisticAttributesList = new string[] { GlobalNames.STATISTIC_ATTRIBUTE_UTILIZATION_DESK_NEED };

        private string[] deskUtilizationStatisticAttributesList = new string[] { GlobalNames.STATISTIC_ATTRIBUTE_UTILIZATION_PERCENT };

        private List<String> attributeDegreeList = new List<String> (new string[] { GlobalNames.ATTRIBUTE_DEGREE_MAX, GlobalNames.ATTRIBUTE_DEGREE_AVG, GlobalNames.ATTRIBUTE_DEGREE_MIN });

        private string[] comparisonTypesList = new string[] { "<", "<=", "=", ">=", ">" };

        private string[] statisticAttributesHavingTotalValue = new string[] {GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_OF_PAX,
                GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_PROCESSED_PAX, GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_TRANSFER_PAX,
                GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_MISSED_PAX, GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_STOPPED_PAX, 
                GlobalNames.STATISTIC_ATTRIBUTE_OVERVIEW_NB_LOST_PAX, GlobalNames.TIME_STATS_TOTAL_TIME, GlobalNames.TIME_STATS_DELAY_TIME,
                GlobalNames.TIME_STATS_PROCESS_TIME, GlobalNames.TIME_STATS_WAITING_GROUP_TIME, GlobalNames.TIME_STATS_WAITING_DESK_TIME, 
                GlobalNames.STATISTIC_ATTRIBUTE_DWELL_AREA, GlobalNames.STATISTIC_ATTRIBUTE_REMAINING_TIME, GlobalNames.STATISTIC_ATTRIBUTE_OCCUPATION_INPUT, 
                GlobalNames.STATISTIC_ATTRIBUTE_OCCUPATION_OUTPUT};

        TreeNode scenarioNode;

        Dictionary<String, TreeNode> terminalsDictionary;

        Dictionary<String, TreeNode> levelsDictionary;

        Dictionary<String, TreeNode> groupsDictionary;

        Dictionary<String, TreeNode> desksDictionary;

        GestionDonneesHUB2SIM donnees;

        // >> Task #11212 Pax2Sim - Target - add missing Attribute degree        
        private List<String> percentileAttributeDegreeList = new List<String>();
        
        private DataTable summaryTable;                
        // << Task #11212 Pax2Sim - Target - add missing Attribute degree

        public DataTable resultTable_;

        public SetTargetAssistant()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        public SetTargetAssistant(TreeNode _scenarioNode, Dictionary<String, TreeNode> _terminalsDict, Dictionary<String, TreeNode> _levelsDict,
            Dictionary<String, TreeNode> _groupsDict, Dictionary<String, TreeNode> _desksDict, GestionDonneesHUB2SIM _donnees, DataTable _summaryTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            scenarioNode = _scenarioNode;
            terminalsDictionary = _terminalsDict;
            levelsDictionary = _levelsDict;
            groupsDictionary = _groupsDict;
            desksDictionary = _desksDict;
            donnees = _donnees;
            summaryTable = _summaryTable;

            setPercentileAttributeDegreeList();

            setUpTerminalComboBox();
            setUpAttributeDegreeComboBox(false);
            setUpComparisonTypeComboBox();
        }

        public SetTargetAssistant(TreeNode _scenarioNode, TreeNode currentNode, Dictionary<String, TreeNode> _terminalsDict, Dictionary<String, TreeNode> _levelsDict,
            Dictionary<String, TreeNode> _groupsDict, Dictionary<String, TreeNode> _desksDict, GestionDonneesHUB2SIM _donnees, DataTable _summaryTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            scenarioNode = _scenarioNode;
            terminalsDictionary = _terminalsDict;
            levelsDictionary = _levelsDict;
            groupsDictionary = _groupsDict;
            desksDictionary = _desksDict;
            donnees = _donnees;
            summaryTable = _summaryTable;

            setPercentileAttributeDegreeList();

            setUpTerminalComboBox();
            setUpAttributeDegreeComboBox(false);
            setUpComparisonTypeComboBox();

            if (!currentNode.Name.Equals(GlobalNames.AIRPORT_REPORTS_NODE_NAME))
            {
                String terminalName = "";
                String levelName = "";
                String groupName = "";
                String deskName = "";
                
                String terminalNameAndDescription = "";
                String levelNameAndDescription = "";
                String groupNameAndDescription = "";
                String deskNameAndDescription = "";

                splitProcessNameFromCurrentNodeName(currentNode,
                    out terminalName, out levelName, out groupName, out deskName,
                    out terminalNameAndDescription, out levelNameAndDescription,
                    out groupNameAndDescription, out deskNameAndDescription);

                initProcessComboBoxWithValues(terminalNameAndDescription, levelNameAndDescription, groupNameAndDescription, deskNameAndDescription);
            }
        }

        // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
        private void setPercentileAttributeDegreeList()
        {
            percentileAttributeDegreeList.Clear();
            if (donnees != null && donnees.Levels != null)
            {
                for (int i = 0; i < donnees.Levels.Length; i++)
                {
                    String percentileLabel = donnees.Levels[i].ToString() + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX;
                    percentileAttributeDegreeList.Add(percentileLabel);
                }
            }
        }
        // << Task #11212 Pax2Sim - Target - add missing Attribute degree

        #region ComboBoxes initialization

        private void setUpTerminalComboBox()
        {
            terminalCombobox.Items.Clear();

            terminalCombobox.Items.Add(COMBOBOX_ITEM_NO_SELECTION);
            foreach (KeyValuePair<String, TreeNode> terminalPair in terminalsDictionary)
            {
                TreeNode terminalNode = terminalPair.Value;
                if (terminalNode != null)
                    terminalCombobox.Items.Add(terminalNode.Text.Trim());
            }

            terminalCombobox.SelectedItem = COMBOBOX_ITEM_NO_SELECTION;
        }

        private void setUpStatisticTypeComboBox()
        {
            statisticTypeCombobox.Items.Clear();

            String selectedProcessType = getCurrentSelectedProcessType();

            if (selectedProcessType.Equals(PROCESS_TYPE_AIRPORT) || selectedProcessType.Equals(PROCESS_TYPE_TERMINAL)
                || selectedProcessType.Equals(PROCESS_TYPE_LEVEL))
            {
                statisticTypeCombobox.Items.Add(GlobalNames.STATISTIC_TYPE_OVERVIEW);
                statisticTypeCombobox.Items.Add(GlobalNames.STATISTIC_TYPE_OCCUPATION);
            }
            else if (selectedProcessType.Equals(PROCESS_TYPE_GROUP) || selectedProcessType.Equals(PROCESS_TYPE_DESK))
            {
                statisticTypeCombobox.Items.Add(GlobalNames.STATISTIC_TYPE_OVERVIEW);
                statisticTypeCombobox.Items.Add(GlobalNames.STATISTIC_TYPE_OCCUPATION);
                statisticTypeCombobox.Items.Add(GlobalNames.STATISTIC_TYPE_DWELL_AREA);
                statisticTypeCombobox.Items.Add(GlobalNames.STATISTIC_TYPE_REMAINING_TIME);
                statisticTypeCombobox.Items.Add(GlobalNames.STATISTIC_TYPE_TIME);
                statisticTypeCombobox.Items.Add(GlobalNames.STATISTIC_TYPE_UTILIZATION);
            }

            if (statisticTypeCombobox.Items.Count > 0)
                statisticTypeCombobox.SelectedIndex = 0;
        }

        private void setUpAttributeDegreeComboBox(bool withPercentiles)
        {
            attributeDegreeCombobox.Items.Clear();

            if (statisticTypeCombobox != null && statisticTypeCombobox.Items != null)
            {
                String selectedStatisticType = statisticTypeCombobox.SelectedItem.ToString();
                if (selectedStatisticType != null && selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_OVERVIEW))
                {
                    attributeDegreeCombobox.Items.Add(GlobalNames.ATTRIBUTE_DEGREE_TOTAL);
                    attributeDegreeCombobox.SelectedIndex = 0;
                    return;
                }
            }

            attributeDegreeList
                = new List<String>(new string[] { GlobalNames.ATTRIBUTE_DEGREE_MAX, GlobalNames.ATTRIBUTE_DEGREE_AVG, 
                                                    GlobalNames.ATTRIBUTE_DEGREE_MIN, GlobalNames.ATTRIBUTE_DEGREE_TOTAL });

            if (withPercentiles)
            {
                addPercentilesToAttributesDegreeList(); // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
            }
            attributeDegreeCombobox.Items.AddRange(attributeDegreeList.ToArray());

            if (attributeDegreeCombobox.Items.Count > 0)
            {
                attributeDegreeCombobox.SelectedIndex = 0;
            }
        }
        private void addPercentilesToAttributesDegreeList() // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
        {
            if (percentileAttributeDegreeList != null && percentileAttributeDegreeList.Count > 0)
            {
                attributeDegreeList.AddRange(percentileAttributeDegreeList.ToArray());
            }            
        }

        private void setUpComparisonTypeComboBox()
        {
            comparisonTypeCombobox.Items.Clear();
            comparisonTypeCombobox.Items.AddRange(comparisonTypesList);
            if (comparisonTypeCombobox.Items.Count > 0)
                comparisonTypeCombobox.SelectedIndex = 0;
        }

        #endregion

        #region ComboBox init with values
        private void splitProcessNameFromCurrentNodeName(TreeNode currentNode,
            out String terminalName, out String levelName, out String groupName, out String deskName,
            out String terminalNameAndDescription, out String levelNameAndDescription, 
            out String groupNameAndDescription, out String deskNameAndDescription)
        {
            terminalName = "";
            levelName = "";
            groupName = "";
            deskName = "";

            terminalNameAndDescription = "";
            levelNameAndDescription = "";
            groupNameAndDescription = "";
            deskNameAndDescription = "";

            if (!currentNode.Name.Equals(GlobalNames.AIRPORT_REPORTS_NODE_NAME))
            {
                terminalName = currentNode.Name.Substring(0, 2);
                if (currentNode.Name.Length == 2)
                    terminalNameAndDescription = currentNode.Text.Trim();

                if (currentNode.Name.Length == 4)
                {
                    levelNameAndDescription = currentNode.Text.Trim();
                    if (currentNode.Parent != null)
                        terminalNameAndDescription = currentNode.Parent.Text.Trim();
                }

                if (currentNode.Name.Length >= 4)
                {
                    levelName = currentNode.Name.Substring(0, 4);
                }
                if (currentNode.Name.Length > 4)
                {
                    if (currentNode.Name.Contains("Group"))
                    {
                        groupName = currentNode.Name.Trim();
                        groupNameAndDescription = currentNode.Text.Trim();

                        if (currentNode.Parent != null)
                        {
                            TreeNode levelNode = currentNode.Parent;
                            levelNameAndDescription = levelNode.Text.Trim();

                            if (levelNode.Parent != null)
                                terminalNameAndDescription = levelNode.Parent.Text.Trim();
                        }
                    }
                    else
                    {
                        deskName = currentNode.Name.Trim();
                        deskNameAndDescription = currentNode.Text.Trim();

                        if (currentNode.Parent != null && currentNode.Parent.Parent != null)
                        {
                            TreeNode groupNode = currentNode.Parent.Parent;
                            groupName = groupNode.Name.Trim();
                            groupNameAndDescription = groupNode.Text.Trim();

                            if (groupNode.Parent != null)
                            {
                                TreeNode levelNode = groupNode.Parent;
                                levelNameAndDescription = levelNode.Text.Trim();

                                if (levelNode.Parent != null)
                                {
                                    TreeNode terminalNode = levelNode.Parent;
                                    terminalNameAndDescription = terminalNode.Text.Trim();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void initProcessComboBoxWithValues(String terminalName,
            String levelName, String groupName, String deskName)
        {
            if (terminalCombobox.Items.Contains(terminalName))
                terminalCombobox.SelectedItem = terminalName;

            if (levelName == "")
                levelCombobox.SelectedItem = COMBOBOX_ITEM_NO_SELECTION;
            else
            {
                if (levelCombobox.Items.Contains(levelName))
                    levelCombobox.SelectedItem = levelName;
            }

            if (groupName == "")
                groupCombobox.SelectedItem = COMBOBOX_ITEM_NO_SELECTION;
            else
            {
                if (groupCombobox.Items.Contains(groupName))
                    groupCombobox.SelectedItem = groupName;
            }

            if (deskName == "")
                deskCombobox.SelectedItem = COMBOBOX_ITEM_NO_SELECTION;
            else
            {
                if (deskCombobox.Items.Contains(deskName))
                    deskCombobox.SelectedItem = deskName;
            }
        }
        #endregion

        #region Event handlers

        private void terminalCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (terminalCombobox.SelectedItem.ToString().Equals(COMBOBOX_ITEM_NO_SELECTION))
            {
                levelCombobox.Enabled = false;
                levelCombobox.Items.Clear();
                groupCombobox.Enabled = false;
                groupCombobox.Items.Clear();
                deskCombobox.Enabled = false;
                deskCombobox.Items.Clear();

                summaryTable = donnees.getTable(scenarioNode.Name, GlobalNames.AIRPORT_REPORTS_NODE_NAME);
            }
            else
            {
                levelCombobox.Enabled = true;

                levelCombobox.Items.Clear();
                levelCombobox.Items.Add(COMBOBOX_ITEM_NO_SELECTION);

                TreeNode selectedTerminalNode = null;
                String selectedTerminalName = "";

                if (terminalCombobox.SelectedItem.ToString().IndexOf("(") != -1)
                {
                    selectedTerminalName = terminalCombobox.SelectedItem.ToString()
                        .Substring(0, terminalCombobox.SelectedItem.ToString().IndexOf("(")).Trim();
                }
                else
                {
                    selectedTerminalName = terminalCombobox.SelectedItem.ToString();
                }

                if (selectedTerminalName != null && terminalsDictionary.TryGetValue(selectedTerminalName, out selectedTerminalNode))
                {
                    foreach (TreeNode levelNode in selectedTerminalNode.Nodes)
                    {
                        TreeViewTag levelNodeTag = (TreeViewTag)levelNode.Tag;
                        if (levelNodeTag != null && levelNodeTag.isResultNode)
                            levelCombobox.Items.Add(levelNode.Text.Trim());//levelCombobox.Items.Add(levelNode.Name);
                    }
                }
                levelCombobox.SelectedItem = COMBOBOX_ITEM_NO_SELECTION;
                summaryTable = donnees.getTable(scenarioNode.Name, selectedTerminalName);
            }
            setUpStatisticTypeComboBox();
        }

        private void levelCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (levelCombobox.SelectedItem.ToString().Equals(COMBOBOX_ITEM_NO_SELECTION))
            {
                groupCombobox.Enabled = false;
                groupCombobox.Items.Clear();
                deskCombobox.Enabled = false;
                deskCombobox.Items.Clear();

                String selectedTerminalName = "";
                if (terminalCombobox.SelectedItem.ToString().IndexOf("(") != -1)
                {
                    selectedTerminalName = terminalCombobox.SelectedItem.ToString()
                        .Substring(0, terminalCombobox.SelectedItem.ToString().IndexOf("(")).Trim();
                }
                else
                {
                    selectedTerminalName = terminalCombobox.SelectedItem.ToString();
                }
                summaryTable = donnees.getTable(scenarioNode.Name, selectedTerminalName);
            }
            else
            {
                groupCombobox.Enabled = true;
                
                groupCombobox.Items.Clear();
                groupCombobox.Items.Add(COMBOBOX_ITEM_NO_SELECTION);

                TreeNode selectedLevelNode = null;
                String selectedLevelName = "";

                if (levelCombobox.SelectedItem.ToString().IndexOf("(") != -1)
                {
                    selectedLevelName = levelCombobox.SelectedItem.ToString()
                        .Substring(0, levelCombobox.SelectedItem.ToString().IndexOf("(")).Trim();
                }
                else
                {
                    selectedLevelName = levelCombobox.SelectedItem.ToString();
                }

                if (selectedLevelName != null && levelsDictionary.TryGetValue(selectedLevelName, out selectedLevelNode))
                {
                    foreach (TreeNode groupNode in selectedLevelNode.Nodes)
                    {
                        TreeViewTag groupNodeTag = (TreeViewTag)groupNode.Tag;
                        if (groupNodeTag != null && groupNodeTag.isResultNode)
                            groupCombobox.Items.Add(groupNode.Text.Trim());//groupCombobox.Items.Add(groupNode.Name);
                    }
                }
                groupCombobox.SelectedItem = COMBOBOX_ITEM_NO_SELECTION;
                summaryTable = donnees.getTable(scenarioNode.Name, selectedLevelName);
            }
            setUpStatisticTypeComboBox();
        }

        private void groupCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupCombobox.SelectedItem.ToString().Equals(COMBOBOX_ITEM_NO_SELECTION))
            {
                deskCombobox.Enabled = false;
                deskCombobox.Items.Clear();

                String selectedLevelName = "";
                if (levelCombobox.SelectedItem.ToString().IndexOf("(") != -1)
                {
                    selectedLevelName = levelCombobox.SelectedItem.ToString()
                        .Substring(0, levelCombobox.SelectedItem.ToString().IndexOf("(")).Trim();
                }
                else
                {
                    selectedLevelName = levelCombobox.SelectedItem.ToString();
                }
                summaryTable = donnees.getTable(scenarioNode.Name, selectedLevelName);
            }
            else
            {
                deskCombobox.Enabled = true;
                
                deskCombobox.Items.Clear();
                deskCombobox.Items.Add(COMBOBOX_ITEM_NO_SELECTION);

                TreeNode selectedGroupNode = null;
                String selectedGroupName = "";

                if (groupCombobox.SelectedItem.ToString().IndexOf("(") != -1)
                {
                    selectedGroupName = groupCombobox.SelectedItem.ToString()
                        .Substring(0, groupCombobox.SelectedItem.ToString().IndexOf("(")).Trim();
                }
                else
                {
                    selectedGroupName = groupCombobox.SelectedItem.ToString();
                }

                if (selectedGroupName != null && groupsDictionary.TryGetValue(selectedGroupName, out selectedGroupNode))
                {
                    foreach (TreeNode groupChildNode in selectedGroupNode.Nodes)
                    {
                        TreeViewTag groupChildNodeTag = (TreeViewTag)groupChildNode.Tag;
                        if (groupChildNodeTag != null && groupChildNodeTag.isDirectoryNode
                            && groupChildNode.Name.Equals("Details"))
                        {
                            foreach (TreeNode deskNode in groupChildNode.Nodes)
                            {
                                TreeViewTag deskNodeTag = (TreeViewTag)deskNode.Tag;
                                if (deskNodeTag != null && deskNodeTag.isResultNode)
                                    deskCombobox.Items.Add(deskNode.Text.Trim());//deskCombobox.Items.Add(deskNode.Name);
                            }
                        }
                    }                    
                }
                deskCombobox.SelectedItem = COMBOBOX_ITEM_NO_SELECTION;
                summaryTable = donnees.getTable(scenarioNode.Name, selectedGroupName);
            }
            setUpStatisticTypeComboBox();
        }
        
        private void statisticTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedStatisticType = statisticTypeCombobox.SelectedItem.ToString();
            String selectedProcessType = getCurrentSelectedProcessType();

            if (selectedStatisticType != null)
            {
                statisticAttributeCombobox.Items.Clear();

                if (selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_OVERVIEW))
                {
                    if (selectedProcessType.Equals(PROCESS_TYPE_AIRPORT) || selectedProcessType.Equals(PROCESS_TYPE_TERMINAL)
                        || selectedProcessType.Equals(PROCESS_TYPE_LEVEL))
                    {
                        statisticAttributeCombobox.Items.AddRange(overviewForAirportTerminalAndLevelStatisticAttributesList);
                    }
                    else
                    {
                        statisticAttributeCombobox.Items.AddRange(overviewForDeskAndGroupStatisticAttributesList);
                    }
                    setUpAttributeDegreeComboBox(false);  // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
                }
                else if (selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_OCCUPATION))
                {
                    if (selectedProcessType.Equals(PROCESS_TYPE_AIRPORT) || selectedProcessType.Equals(PROCESS_TYPE_TERMINAL)
                        || selectedProcessType.Equals(PROCESS_TYPE_LEVEL))
                    {
                        statisticAttributeCombobox.Items.AddRange(occupationForAirportTerminalAndLevelStatisticAttributesList);
                    }
                    else
                    {
                        statisticAttributeCombobox.Items.AddRange(occupationForDeskAndGroupStatisticAttributesList);
                    }
                    setUpAttributeDegreeComboBox(false);  // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
                }
                else if (selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_DWELL_AREA))
                {
                    statisticAttributeCombobox.Items.AddRange(dwellAreaStatisticAttributeList);
                    setUpAttributeDegreeComboBox(false);  // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
                }
                else if (selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_REMAINING_TIME))
                {
                    statisticAttributeCombobox.Items.AddRange(remainingTimeStatisticAttributesList);
                    setUpAttributeDegreeComboBox(true);  // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
                }
                else if (selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_TIME))
                {
                    statisticAttributeCombobox.Items.AddRange(timeStatisticAttributesList);
                    setUpAttributeDegreeComboBox(true);  // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
                }
                else if (selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_UTILIZATION))
                {
                    if (selectedProcessType.Equals(PROCESS_TYPE_GROUP))
                    {
                        statisticAttributeCombobox.Items.AddRange(groupUtilizationStatisticAttributesList);
                    }
                    else if (selectedProcessType.Equals(PROCESS_TYPE_DESK))
                    {
                        statisticAttributeCombobox.Items.AddRange(deskUtilizationStatisticAttributesList);
                    }
                    setUpAttributeDegreeComboBox(false);  // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
                }
                if (statisticAttributeCombobox.Items.Count > 0)
                    statisticAttributeCombobox.SelectedIndex = 0;
            }
        }

        private void deskCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedDesk = deskCombobox.SelectedItem.ToString();
            String selectedStatisticType = statisticTypeCombobox.SelectedItem.ToString();

            if (selectedDesk.Equals(COMBOBOX_ITEM_NO_SELECTION))
            {
                if (selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_UTILIZATION))
                {
                    statisticAttributeCombobox.Items.Clear();
                    statisticAttributeCombobox.Items.AddRange(groupUtilizationStatisticAttributesList);
                }

                String selectedGroupName = "";
                if (groupCombobox.SelectedItem.ToString().IndexOf("(") != -1)
                {
                    selectedGroupName = groupCombobox.SelectedItem.ToString()
                        .Substring(0, groupCombobox.SelectedItem.ToString().IndexOf("(")).Trim();
                }
                else
                {
                    selectedGroupName = groupCombobox.SelectedItem.ToString();
                }
                summaryTable = donnees.getTable(scenarioNode.Name, selectedGroupName);
            }
            else
            {
                if (selectedStatisticType.Equals(GlobalNames.STATISTIC_TYPE_UTILIZATION))
                {
                    statisticAttributeCombobox.Items.Clear();
                    statisticAttributeCombobox.Items.AddRange(deskUtilizationStatisticAttributesList);
                }

                String selectedDeskName = "";
                if (deskCombobox.SelectedItem.ToString().IndexOf("(") != -1)
                {
                    selectedDeskName = deskCombobox.SelectedItem.ToString()
                        .Substring(0, deskCombobox.SelectedItem.ToString().IndexOf("(")).Trim();
                }
                else
                {
                    selectedDeskName = deskCombobox.SelectedItem.ToString();
                }
                summaryTable = donnees.getTable(scenarioNode.Name, selectedDeskName);
            }
            if (statisticAttributeCombobox.Items.Count > 0)
                statisticAttributeCombobox.SelectedIndex = 0;            
        }

        private void statisticAttributeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (statisticAttributeCombobox.SelectedItem != null)
            {
                String selectedStatisticAttribute = statisticAttributeCombobox.SelectedItem.ToString();
                if (selectedStatisticAttribute != null)
                {
                    if (attributeDegreeCombobox != null && attributeDegreeCombobox.Items != null)
                    {
                        if (statisticAttributesHavingTotalValue.Contains(selectedStatisticAttribute))
                        {
                            if (!attributeDegreeCombobox.Items.Contains(GlobalNames.ATTRIBUTE_DEGREE_TOTAL))
                            {
                                attributeDegreeCombobox.Items.Add(GlobalNames.ATTRIBUTE_DEGREE_TOTAL);
                            }
                        }
                        else
                        {
                            if (attributeDegreeCombobox.Items.Contains(GlobalNames.ATTRIBUTE_DEGREE_TOTAL))
                            {
                                attributeDegreeCombobox.Items.RemoveAt(attributeDegreeCombobox.Items.IndexOf(GlobalNames.ATTRIBUTE_DEGREE_TOTAL));
                            }
                        }

                    }
                }
            }
        }

        private static char[] allowedCharsList = { '\b', '.', '-' };

        private void targetValueTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)
                || Array.IndexOf(allowedCharsList, e.KeyChar) > -1)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion

        private String getCurrentSelectedProcessType()
        {
            if (deskCombobox.Enabled)
            {
                if (deskCombobox.SelectedItem != null)
                {
                    if (deskCombobox.SelectedItem.ToString().Equals(COMBOBOX_ITEM_NO_SELECTION))
                        return PROCESS_TYPE_GROUP;
                    else
                        return PROCESS_TYPE_DESK;
                }
            }
            else if (groupCombobox.Enabled)
            {
                if (groupCombobox.SelectedItem != null)
                {
                    if (groupCombobox.SelectedItem.ToString().Equals(COMBOBOX_ITEM_NO_SELECTION))
                        return PROCESS_TYPE_LEVEL;
                    else
                        return PROCESS_TYPE_GROUP;
                }
            }
            else if (levelCombobox.Enabled)
            {
                if (levelCombobox.SelectedItem != null)
                {
                    if (levelCombobox.SelectedItem.ToString().Equals(COMBOBOX_ITEM_NO_SELECTION))
                        return PROCESS_TYPE_TERMINAL;
                    else
                        return PROCESS_TYPE_LEVEL;
                }
            }
            else if (terminalCombobox.SelectedItem != null)
            {
                if (terminalCombobox.SelectedItem.ToString().Equals(COMBOBOX_ITEM_NO_SELECTION))
                    return PROCESS_TYPE_AIRPORT;
                else 
                    return PROCESS_TYPE_TERMINAL;
            }
            return "";
        }

        private String getNameAndDescriptionForSelectedProcessObserved()
        {
            String selectedProcessType = getCurrentSelectedProcessType();
            String selectedProcessObservedName = "";

            switch (selectedProcessType)
            {
                case PROCESS_TYPE_AIRPORT:
                    selectedProcessObservedName = PROCESS_TYPE_AIRPORT; //we need the Airport_Occupation table
                    break;
                case PROCESS_TYPE_TERMINAL:
                    if (terminalCombobox.SelectedItem != null)
                        selectedProcessObservedName = terminalCombobox.SelectedItem.ToString();
                    break;
                case PROCESS_TYPE_LEVEL:
                    if (levelCombobox.SelectedItem != null)
                        selectedProcessObservedName = levelCombobox.SelectedItem.ToString();
                    break;
                case PROCESS_TYPE_GROUP:
                    if (groupCombobox.SelectedItem != null)
                        selectedProcessObservedName = groupCombobox.SelectedItem.ToString();
                    break;
                case PROCESS_TYPE_DESK:
                    if (deskCombobox.SelectedItem != null)
                        selectedProcessObservedName = deskCombobox.SelectedItem.ToString();
                    break;
                default:
                    break;
            }

            return selectedProcessObservedName;
        }

        private DataTable getStatisticTableForAnalysis()
        {
            DataTable statisticTable = null;
            String statisticTableName = "";

            String selectedProcessObservedNameAndDescription = getNameAndDescriptionForSelectedProcessObserved();
            String selectedProcessObservedName = "";
            String selectedStatisticTypeName = "";

            if (selectedProcessObservedNameAndDescription != null && selectedProcessObservedNameAndDescription != ""
                && selectedProcessObservedNameAndDescription.IndexOf("(") != -1)
            {
                selectedProcessObservedName = selectedProcessObservedNameAndDescription
                    .Substring(0, selectedProcessObservedNameAndDescription.IndexOf("(")).Trim();
            }
            else //if (selectedProcessObservedNameAndDescription.Equals(PROCESS_TYPE_AIRPORT))
            {
                selectedProcessObservedName = selectedProcessObservedNameAndDescription;
            }

            if (statisticTypeCombobox.SelectedItem != null)
                selectedStatisticTypeName = statisticTypeCombobox.SelectedItem.ToString();

            if (selectedProcessObservedName != null && selectedProcessObservedName != ""
                && selectedStatisticTypeName != "")
            {
                if (selectedStatisticTypeName.Equals(GlobalNames.STATISTIC_TYPE_TIME)
                    || selectedStatisticTypeName.Equals(GlobalNames.STATISTIC_TYPE_REMAINING_TIME))
                {
                    statisticTableName = selectedProcessObservedName + GlobalNames.IST_TABLE_SUFFIX;
                    statisticTable = donnees.getTable(scenarioNode.Name, statisticTableName);
                }
                else
                {
                    statisticTableName = selectedProcessObservedName + "_" + selectedStatisticTypeName;
                    statisticTable = donnees.getTable(scenarioNode.Name, statisticTableName);
                }
            }
            return statisticTable;
        }

        private int getTargetedColumnIndexFromStatisticTable(DataTable statisticTable, String statisticType, String statisticAttribute)
        {
            String columnName = "";
            int columnIndex = -1;

            if (statisticTable != null)
            {
                if (statisticType.Equals(GlobalNames.STATISTIC_TYPE_TIME))
                {
                    if (statisticAttribute.Equals(GlobalNames.TIME_STATS_TOTAL_TIME))
                        columnName = GlobalNames.IST_TOTAL_TIME_COLUMN_NAME;
                    else if (statisticAttribute.Equals(GlobalNames.TIME_STATS_DELAY_TIME))
                        columnName = GlobalNames.sPaxPlan_DelayTime;
                    else if (statisticAttribute.Equals(GlobalNames.TIME_STATS_PROCESS_TIME))
                        columnName = GlobalNames.IST_PROCESS_TIME_COLUMN_NAME;
                    else if (statisticAttribute.Equals(GlobalNames.TIME_STATS_WAITING_GROUP_TIME))
                        columnName = GlobalNames.IST_WAITING_GROUP_TIME_COLUMN_NAME;
                    else if (statisticAttribute.Equals(GlobalNames.TIME_STATS_WAITING_DESK_TIME))
                        columnName = GlobalNames.IST_WAITING_DESK_TIME_COLUMN_NAME;

                    columnIndex = statisticTable.Columns.IndexOf(columnName);
                }
                else if (statisticType.Equals(GlobalNames.STATISTIC_TYPE_REMAINING_TIME))
                {
                    columnName = GlobalNames.IST_REMAINING_TIME_COLUMN_NAME;
                    columnIndex = statisticTable.Columns.IndexOf(columnName);
                }
                else
                {
                    columnName = statisticAttribute;
                    columnIndex = statisticTable.Columns.IndexOf(columnName);
                }
            }
            return columnIndex;
        }
        
        private double getValueObserved(String statisticType,
            String statisticAttribute, String attributeDegree)
        {
            double valueObserved = 0;

            if (statisticType.Equals(GlobalNames.STATISTIC_TYPE_OVERVIEW) 
                || statisticType.Equals(GlobalNames.STATISTIC_TYPE_OCCUPATION) 
                || statisticType.Equals(GlobalNames.STATISTIC_TYPE_DWELL_AREA)
                || statisticType.Equals(GlobalNames.STATISTIC_TYPE_UTILIZATION)
                || statisticType.Equals(GlobalNames.STATISTIC_TYPE_TIME)
                || statisticType.Equals(GlobalNames.STATISTIC_TYPE_REMAINING_TIME))
            {
                if (attributeDegree.Equals(GlobalNames.ATTRIBUTE_DEGREE_MIN))
                {
                    valueObserved = getValueFromSummaryTable(statisticAttribute, GlobalNames.SUMMARY_TABLE_MIN_VALUE_COLUMN_NAME);                    
                    //valueObserved = OverallTools.DataFunctions.getMinValue(statisticTable, targetedColumnIndex);
                }
                else if (attributeDegree.Equals(GlobalNames.ATTRIBUTE_DEGREE_MAX))
                {
                    valueObserved = getValueFromSummaryTable(statisticAttribute, GlobalNames.SUMMARY_TABLE_MAX_VALUE_COLUMN_NAME);
                    //valueObserved = OverallTools.DataFunctions.getMaxValue(statisticTable, targetedColumnIndex);
                }
                else if (attributeDegree.Equals(GlobalNames.ATTRIBUTE_DEGREE_AVG))      // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
                {
                    valueObserved = getValueFromSummaryTable(statisticAttribute, GlobalNames.SUMMARY_TABLE_AVG_VALUE_COLUMN_NAME);
                    //valueObserved = OverallTools.DataFunctions.getAvgValue(statisticTable, targetedColumnIndex);
                }
                else if (percentileAttributeDegreeList.Contains(attributeDegree))       // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
                {
                    valueObserved = getValueFromSummaryTable(statisticAttribute, attributeDegree);
                }
                else if (attributeDegree.Equals(GlobalNames.ATTRIBUTE_DEGREE_TOTAL))    // >> Task #11217 Pax2Sim - Target - add missing Attribute degree stage2
                {                    
                    valueObserved = getValueFromSummaryTable(statisticAttribute, GlobalNames.SUMMARY_TABLE_VALUE_COLUMN_NAME);
                }
            }
            return valueObserved;
        }

        // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
        private double getValueFromSummaryTable(String statisticAttribute, String attributeDegree)
        {
            double valueFromSummary = -1;

            if (summaryTable != null && GlobalNames.targetStatisticAttributeSummaryTableStatisticNameMap.ContainsKey(statisticAttribute))
            {                 
                String summaryKpiName = "";
                if (GlobalNames.targetStatisticAttributeSummaryTableStatisticNameMap.TryGetValue(statisticAttribute, out summaryKpiName))
                {
                    int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
                    int valueColumnIndex = summaryTable.Columns.IndexOf(attributeDegree);

                    if (kpiNameColumnIndex != -1 && valueColumnIndex != -1)                    
                    {
                        String kpiNameFromTable = "";                        
                        foreach (DataRow row in summaryTable.Rows)
                        {
                            if (row[kpiNameColumnIndex] != null)
                            {
                                kpiNameFromTable = row[kpiNameColumnIndex].ToString();
                                if (summaryKpiName.Equals(kpiNameFromTable))
                                {
                                    if (row[valueColumnIndex] != null)
                                    {
                                        String valueFromTable = row[valueColumnIndex].ToString();
                                        Double.TryParse(valueFromTable, out valueFromSummary);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }                
            }
            return valueFromSummary;
        }
        // << Task #11212 Pax2Sim - Target - add missing Attribute degree
                
        private bool isTargetAchieved(double targetValue, double valueObserved, String comparisonType)
        {
            bool targetAchieved = false;            

            switch (comparisonType)
            {
                case "<":
                    if (valueObserved < targetValue)
                        targetAchieved = true;
                    break;
                case "<=":
                    if (valueObserved <= targetValue)
                        targetAchieved = true;
                    break;
                case "=":
                    if (valueObserved == targetValue)
                        targetAchieved = true;
                    break;
                case ">=":
                    if (valueObserved >= targetValue)
                        targetAchieved = true;
                    break;
                case ">":
                    if (valueObserved > targetValue)
                        targetAchieved = true;
                    break;
            }
            return targetAchieved;

        }

        private DataTable checkStatisticTableAgainstDefinedTarget()
        {
            DataTable resultTable = null;
            String processObservedName = getNameAndDescriptionForSelectedProcessObserved();
            String statisticType = statisticTypeCombobox.SelectedItem.ToString();
            String statisticAttribute = statisticAttributeCombobox.SelectedItem.ToString();
            String attributeDegree = attributeDegreeCombobox.SelectedItem.ToString();
            String comparisonType = comparisonTypeCombobox.SelectedItem.ToString();
            String targetAchievedString = "";

            double valueObserved = Double.MinValue;            
            valueObserved = getValueObserved(statisticType,
                                             statisticAttribute, attributeDegree);            
            if (valueObserved != Double.MinValue)
            {
                bool targetAchieved = false;
                double difference = 0;
                double percentSuccess = 0;
                
                double targetValue = 0;
                if (Double.TryParse(targetValueTextbox.Text, out targetValue))
                {
                    if (comparisonType.Contains(">"))
                    {
                        percentSuccess = (valueObserved / targetValue) * 100;
                        if (!comparisonType.Contains("=") && valueObserved == targetValue)
                            percentSuccess--;
                    }
                    else if (comparisonType.Contains("<"))
                    {
                        percentSuccess = (targetValue / valueObserved) * 100;
                        if (!comparisonType.Contains("=") && valueObserved == targetValue)
                            percentSuccess--;
                    }
                    else if (comparisonType.Equals("="))
                    {
                        percentSuccess = (Math.Min(targetValue, valueObserved) / Math.Max(targetValue, valueObserved)) * 100;
                        if (targetValue == valueObserved)
                            percentSuccess = 100;
                    }
                    percentSuccess = Math.Round(percentSuccess, 2);

                    difference = Math.Round(Math.Abs(targetValue - valueObserved), 2);

                    targetAchieved = isTargetAchieved(targetValue, valueObserved, comparisonType);
                    if (targetAchieved)
                    {
                        targetAchievedString = GlobalNames.TARGET_ACHIEVED_POSITIVE;                        
                    }
                    else
                    {
                        targetAchievedString = GlobalNames.TARGET_ACHIEVED_NEGATIVE;
                        difference = difference * (-1);
                    }
                }

                String selectedProcessObservedName = getNameAndDescriptionForSelectedProcessObserved();
                String resultTableName = selectedProcessObservedName + " " + GlobalNames.TARGET_TABLE_NAME_SUFIX;

                resultTable = donnees.getTable(scenarioNode.Name, resultTableName);
                if (resultTable == null)
                {
                    resultTable = createResultTable(resultTableName);
                    addRowToResultTable(resultTable, scenarioNode.Name, processObservedName,
                        statisticType, attributeDegree, statisticAttribute, comparisonType, targetValue,
                        valueObserved, targetAchievedString, difference, percentSuccess);
                }
                else
                {
                    addRowToResultTable(resultTable, scenarioNode.Name, processObservedName,
                        statisticType, attributeDegree, statisticAttribute, comparisonType, targetValue,
                        valueObserved, targetAchievedString, difference, percentSuccess);
                }
            }

            return resultTable;
        }

        private DataTable createResultTable(String tableName)
        {
            DataTable resultTable = new DataTable(tableName);

            resultTable.Columns.Add(GlobalNames.target_scenarioName_columnName, typeof(String));
            resultTable.Columns.Add(GlobalNames.target_processObserved_columnName, typeof(String));
            resultTable.Columns.Add(GlobalNames.target_statisticType_columnName, typeof(String));
            resultTable.Columns.Add(GlobalNames.target_attributeDegree_columnName, typeof(String));
            resultTable.Columns.Add(GlobalNames.target_statisticAttribute_columnName, typeof(String));            
            resultTable.Columns.Add(GlobalNames.target_comparisonType_columnName, typeof(String));
            resultTable.Columns.Add(GlobalNames.target_targetValue_columnName, typeof(Double));
            resultTable.Columns.Add(GlobalNames.target_valueObserved_columnName, typeof(Double));
            resultTable.Columns.Add(GlobalNames.target_targetAchived_columnName, typeof(String));
            resultTable.Columns.Add(GlobalNames.target_difference_columnName, typeof(Double));
            resultTable.Columns.Add(GlobalNames.target_percentSuccess_columnName, typeof(Double));

            return resultTable;
        }

        private void addRowToResultTable(DataTable resultTable, String scenarioName, String processObserved,
            String statisticType, String attributeDegree, String statisticAttribute, String comparisonType,
            double targetValue, double valueObserved, String targetAchieved, double difference, double percentSuccess)
        {
            int scenarioNameColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_scenarioName_columnName);
            int processObservedColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_processObserved_columnName);
            int statisticTypeColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_statisticType_columnName);
            int attributeDegreeColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_attributeDegree_columnName);
            int statisticAttributeColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_statisticAttribute_columnName);            
            int comparisonTypeColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_comparisonType_columnName);
            int targetValueColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_targetValue_columnName);
            int valueObservedColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_valueObserved_columnName);
            int targetAchievedColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_targetAchived_columnName);
            int differenceColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_difference_columnName);
            int percentSuccessColumnIndex = resultTable.Columns.IndexOf(GlobalNames.target_percentSuccess_columnName);

            if (scenarioNameColumnIndex != -1 && processObservedColumnIndex != -1 && statisticTypeColumnIndex != -1
                && attributeDegreeColumnIndex != -1 && statisticAttributeColumnIndex != -1 && comparisonTypeColumnIndex != -1 
                && targetValueColumnIndex != -1 && valueObservedColumnIndex != -1 && targetAchievedColumnIndex != -1 
                && differenceColumnIndex != -1 && percentSuccessColumnIndex != -1)
            {
                DataRow newResultRow = resultTable.NewRow();
                newResultRow[scenarioNameColumnIndex] = scenarioName;
                newResultRow[processObservedColumnIndex] = processObserved;
                newResultRow[statisticTypeColumnIndex] = statisticType;
                newResultRow[attributeDegreeColumnIndex] = attributeDegree;
                newResultRow[statisticAttributeColumnIndex] = statisticAttribute;                
                newResultRow[comparisonTypeColumnIndex] = comparisonType;
                newResultRow[targetValueColumnIndex] = targetValue;
                newResultRow[valueObservedColumnIndex] = valueObserved;
                newResultRow[targetAchievedColumnIndex] = targetAchieved;
                newResultRow[differenceColumnIndex] = difference;
                if (targetValue != 0 && valueObserved != 0)
                    newResultRow[percentSuccessColumnIndex] = percentSuccess;
                
                resultTable.Rows.Add(newResultRow);
                resultTable.AcceptChanges();
            }
        }

        private bool verifyInputData()
        {
            String selectedProcessObservedName = getNameAndDescriptionForSelectedProcessObserved();
            if (selectedProcessObservedName == null || selectedProcessObservedName == "")
                return false;

            if (statisticTypeCombobox.SelectedItem == null || statisticTypeCombobox.SelectedItem.ToString().Equals(""))
                return false;

            if (statisticAttributeCombobox.SelectedItem == null || statisticAttributeCombobox.SelectedItem.ToString().Equals(""))
                return false;

            if (attributeDegreeCombobox.SelectedItem == null || attributeDegreeCombobox.SelectedItem.ToString().Equals(""))
                return false;

            if (comparisonTypeCombobox.SelectedItem == null || comparisonTypeCombobox.SelectedItem.ToString().Equals(""))
                return false;
            
            double value = 0;
            if (!Double.TryParse(targetValueTextbox.Text, out value))
                return false;

            return true;                
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!verifyInputData())
            {
                MessageBox.Show("Please check the input data.");
                DialogResult = DialogResult.None;
                return;
            }

            DataTable statisticTable = getStatisticTableForAnalysis();

            if (summaryTable != null)
            {
                resultTable_ = checkStatisticTableAgainstDefinedTarget();
            }
        }

    }
}
