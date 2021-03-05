using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    public partial class ReportLinksManagementAssistant : Form
    {
        private const string SPLITTER = " (-) ";
        private const string SELECT_ALL_LABEL = "Select All";
        private const string DESELECT_ALL_LABEL = "Deselect All";

        PAX2SIM p2s;
        public ReportContent reportContent;
        List<string> allScenarioNames = new List<string>();

        /// <summary>
        /// K = scenario name
        /// V = list of report nodes linked to the scenario
        /// </summary>
        public Dictionary<string, List<TreeNode>> reportNodesAndScenariosDictionary = new Dictionary<string, List<TreeNode>>();
                
        internal ReportLinksManagementAssistant(ReportContent pReportContent, List<string> pAllScenarioNames, PAX2SIM pP2S)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            p2s = pP2S;
            reportContent = pReportContent;
            allScenarioNames.AddRange(pAllScenarioNames);
            availableScenariosComboBox.Items.AddRange(allScenarioNames.ToArray());

            updatePanelsWithReportContent(reportContent);
        }

        private void updatePanelsWithReportContent(ReportContent reportContent)
        {
            int previousY = 0;
            int locationY = 0;
                        
            if (reportContent.tableNodes.Count > 0)
            {
                updatePanelWithReportContent(reportContent.tableNodes, tablesPanel, previousY, out locationY);
            }            
            if (reportContent.filterNodes.Count > 0)
            {                
                updatePanelWithReportContent(reportContent.filterNodes, filtersPanel, previousY, out locationY);
            }            
            if (reportContent.paragraphNodes.Count > 0)
            {             
                updatePanelWithReportContent(reportContent.paragraphNodes, paragraphsPanel, previousY, out locationY);
            }            
            if (reportContent.chartNodes.Count > 0)
            {             
                updatePanelWithReportContent(reportContent.chartNodes, chartsPanel, previousY, out locationY);
                //updatePanelWithReportContent(reportContent.globalChartNodes, chartsPanel, locationY, out locationY);
            }
        }

        private void updatePanelWithReportContent(List<TreeNode> reportNodes, Panel panel,
            int previousY, out int locationY)
        {
            int locationX = 20;
            locationY = 30;
            if (previousY != 0)
                locationY = previousY;
            foreach (TreeNode node in reportNodes)
            {
                if (node == null || !(node.Tag is TreeViewTag))
                    continue;                
                TreeViewTag tag = (TreeViewTag)node.Tag;
                if (tag.isTableNode || tag.isFilterNode || tag.isParagraphNode || tag.isChartNode)
                {
                    if (tag.isParagraphNode && (tag.ScenarioName == null || tag.ScenarioName == ""))  //until dealing with the notes attached to global chart
                    {
                        continue;
                    }
                    if (tag.ScenarioName != null && tag.ScenarioName != "")
                    {
                        CheckBox cb = new CheckBox();
                        cb.Text = node.Text + SPLITTER + tag.ScenarioName;
                        cb.AutoSize = true;
                        cb.BackColor = Color.Transparent;
                        panel.Controls.Add(cb);
                        cb.Location = new Point(locationX, locationY);
                        cb.Tag = tag.ScenarioName;

                        if (tag.ScenarioName != "Input")
                        {
                            locationY += 18;

                            Label lb = new Label();
                            lb.Text = "Initial link: " + node.Text + SPLITTER + tag.ScenarioName;
                            lb.AutoSize = true;
                            lb.BackColor = Color.Transparent;
                            panel.Controls.Add(lb);
                            lb.Location = new Point(locationX, locationY);
                        }
                        else
                        {
                            cb.Enabled = false;
                        }
                        locationY += 20;
                    }
                    else
                    {
                        int locationXForGlobalChartLabel = locationX + 40;
                        Button b = new Button();
                        b.Text = "Link";
                        b.Size = new Size(37, 20);
                        panel.Controls.Add(b);
                        b.Location = new Point(locationX, locationY);
                        b.Click += new EventHandler(reportGlobalChartLinkButton_Click);
                        b.Tag = node.Text; // global chart name is unique per project

                        Label lb = new Label();
                        lb.Text = "Global chart: " + node.Text;
                        lb.AutoSize = true;
                        lb.BackColor = Color.Transparent;
                        panel.Controls.Add(lb);
                        lb.Location = new Point(locationXForGlobalChartLabel, locationY + 3);
                        lb.Tag = node.Text;

                        locationY += 25;
                    }
                }
            }
        }

        private void linkButton_Click(object sender, EventArgs e)
        {
            if (availableScenariosComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a scenario.", "Links manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string selectedScenarioName = availableScenariosComboBox.SelectedItem.ToString();
            updateCheckBoxesScenarioName(tablesPanel, selectedScenarioName);
            updateCheckBoxesScenarioName(filtersPanel, selectedScenarioName);
            updateCheckBoxesScenarioName(paragraphsPanel, selectedScenarioName);
            updateCheckBoxesScenarioName(chartsPanel, selectedScenarioName);
        }

        private void updateCheckBoxesScenarioName(Panel panel, string linkScenarioName)
        {
            foreach (Control child in panel.Controls)
            {
                if (!(child is CheckBox))
                    continue;
                CheckBox cb = (CheckBox)child;
                if (!cb.Checked)
                    continue;
                if (isReportNodeAndScenarioCheckBox(cb))
                {
                    string[] nodeAndScenarioName = cb.Text.Split(new string[] { SPLITTER }, StringSplitOptions.None);
                    string nodeName = nodeAndScenarioName[0];
                    cb.Text = nodeName + SPLITTER + linkScenarioName;
                }
            }
        }
                
        private void reportGlobalChartLinkButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button == null || button.Tag == null || button.Tag.ToString() == "")
            {
                return;
            }
            string uniqueGlobalChartName = button.Tag.ToString();
            //get global chart node from the Report tree-view - treeview1 nodes not reportParameters nodes.
            TreeNode globalChartNode = OverallTools.TreeViewFunctions.getGlobalChartNodeByUniqueName(reportContent.reportNode, uniqueGlobalChartName);
            if (globalChartNode != null)
            {
                Dictionary<int, SIMCORE_TOOL.PAX2SIM.TableScenarioInfoHolder> scenarioTableNames = null;
                if (reportContent.modifiedGlobalCharts.ContainsKey(uniqueGlobalChartName))
                {
                    scenarioTableNames = reportContent.modifiedGlobalCharts[uniqueGlobalChartName];
                }
                else
                {
                    scenarioTableNames = p2s.getScenarioAndTablePairsForGivenGlobalChart(globalChartNode);
                }
                if (manageReportGlobalChartLinks(reportContent, reportContent.reportNode, globalChartNode, scenarioTableNames))
                {
                    if (button.Parent is Panel)
                    {
                        Label label = getLabelByTagText((Panel)button.Parent, uniqueGlobalChartName);
                        label.Text = "Global chart: " + uniqueGlobalChartName + "  *";
                    }
                }
            }
        }

        // >> Task #13384 Report Tree-view C#37 (global chart links)
        private bool manageGlobalChartLinks(TreeNode chartNode, Dictionary<int, PAX2SIM.TableScenarioInfoHolder> scenarioTableNames)
        {
            if (chartNode == null || scenarioTableNames == null)
                return false;

            IndividualLinksManagementAssistant linksManager = new IndividualLinksManagementAssistant(chartNode.Text, scenarioTableNames);
            if (linksManager.ShowDialog() == DialogResult.OK)
            { 
                return true;
            }
            return false;
        }
        // << Task #13384 Report Tree-view C#37 (global chart links)

        private bool manageReportGlobalChartLinks(ReportContent reportContent, TreeNode parentReport, TreeNode chartNode,
            Dictionary<int, PAX2SIM.TableScenarioInfoHolder> scenarioTableNames)
        {            
            if (chartNode == null || scenarioTableNames == null)
                return false;
            if (parentReport == null)
                parentReport = p2s.getParentReportNode(chartNode);
            if (parentReport == null)
                return false;

            IndividualLinksManagementAssistant linksManager = new IndividualLinksManagementAssistant(chartNode.Text, scenarioTableNames);
            if (linksManager.ShowDialog() == DialogResult.OK)
            {
                if (reportContent.modifiedGlobalCharts.ContainsKey(chartNode.Text))
                {
                    reportContent.modifiedGlobalCharts[chartNode.Text] = linksManager.tableScenarioHolderDictionary;
                }
                else
                {
                    reportContent.modifiedGlobalCharts.Add(chartNode.Text, linksManager.tableScenarioHolderDictionary);
                }
                return true;
            }
            return false;
        }

        private Label getLabelByTagText(Panel panel, string tagText)
        {
            if (panel == null)
                return null;
            foreach (Control control in panel.Controls)
            {
                if (control is Label)
                {
                    Label label = (Label)control;
                    if (label.Tag != null && label.Tag.ToString() == tagText)
                    {
                        return label;
                    }
                }
            }
            return null;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            reportNodesAndScenariosDictionary = getReportsNodesAndScenariosDictionary(reportContent);
        }
                
        private Dictionary<string, List<TreeNode>> getReportsNodesAndScenariosDictionary(ReportContent reportContent)
        {
            Dictionary<string, List<TreeNode>> results = new Dictionary<string, List<TreeNode>>();
            retrieveResultsFromPanel(tablesPanel, reportContent.tableNodes, results);
            retrieveResultsFromPanel(filtersPanel, reportContent.filterNodes, results);
            retrieveResultsFromPanel(paragraphsPanel, reportContent.paragraphNodes, results);
            retrieveResultsFromPanel(chartsPanel, reportContent.chartNodes, results);
            return results;
        }

        private void retrieveResultsFromPanel(Panel panel, List<TreeNode> reportsNodes,
            Dictionary<string, List<TreeNode>> results)
        {
            foreach (Control child in panel.Controls)
            {
                if (!(child is CheckBox))
                    continue;
                CheckBox cb = (CheckBox)child;
                if (isReportNodeAndScenarioCheckBox(cb))
                {
                    string[] nodeAndScenarioName = cb.Text.Split(new string[] { SPLITTER }, StringSplitOptions.None);
                    string nodeName = nodeAndScenarioName[0];
                    string newScenarioName = nodeAndScenarioName[1];
                    string initialScenarioName = cb.Tag.ToString();
                    TreeNode node = getNodeByNameAndScenario(nodeName, initialScenarioName, reportsNodes);
                    if (results.ContainsKey(newScenarioName))
                    {
                        if (!results[newScenarioName].Contains(node))
                        {
                            results[newScenarioName].Add(node);
                        }
                    }
                    else
                    {
                        List<TreeNode> nodes = new List<TreeNode>();
                        nodes.Add(node);
                        results.Add(newScenarioName, nodes);
                    }
                }
            }
        }

        private TreeNode getNodeByNameAndScenario(string nodeName, string scenarioName,
            List<TreeNode> reportNodesByType)
        {
            foreach (TreeNode node in reportNodesByType)
            {
                if (node.Text == nodeName && node.Tag is TreeViewTag)
                {
                    TreeViewTag tag = (TreeViewTag)node.Tag;
                    if (tag.ScenarioName == scenarioName)
                        return node;
                }
            }
            return null;
        }

        private bool isReportNodeAndScenarioCheckBox(CheckBox cb)
        {
            if (cb.Tag != null && cb.Tag.ToString() != null && cb.Tag.ToString().Length > 0)
            {
                return true;
            }
            return false;
        }

        private void manageAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is CheckBox))
                return;
            CheckBox senderCb = (CheckBox)sender;
            if (senderCb.Parent == null)
                return;
            List<CheckBox> relatedCheckBoxes = getReportNodesCheckboxesByControl(senderCb.Parent);
            setAllCheckBoxesCheckedState(relatedCheckBoxes, senderCb.Checked);
        }

        private List<CheckBox> getReportNodesCheckboxesByControl(Control parentControl)
        {
            List<CheckBox> checkboxes = new List<CheckBox>();
            foreach (Control control in parentControl.Controls)
            {
                if (control is CheckBox && isReportNodeAndScenarioCheckBox((CheckBox)control))
                    checkboxes.Add((CheckBox)control);
            }
            return checkboxes;
        }

        private void setAllCheckBoxesCheckedState(List<CheckBox> checkboxes, bool checkedState)
        {
            foreach (CheckBox cb in checkboxes)
            {
                if (cb.Enabled)
                    cb.Checked = checkedState;
            }
        }

        private bool areAllCheckBoxesChecked(List<CheckBox> checkboxes)
        {
            foreach (CheckBox cb in checkboxes)
            {
                if (!cb.Enabled)
                    continue;
                if (!cb.Checked)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
