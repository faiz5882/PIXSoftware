using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    public partial class IndividualLinksManagementAssistant : Form
    {
        internal const string TABLE_ID_NAME_DELIMITER = " : ";
        internal const string TABLE_ID_SCENARIO_DELIMITER = " => ";

        internal Dictionary<int, PAX2SIM.TableScenarioInfoHolder> tableScenarioHolderDictionary = new Dictionary<int, PAX2SIM.TableScenarioInfoHolder>();

        private Dictionary<int, Label> labelsDictionary = new Dictionary<int, Label>();

        private List<string> commonScenarios = new List<string>();

        public CloneGraphicFilterNameEditor.Parameters cloneGraphicFilterNameParameters;

        private GestionDonneesHUB2SIM donnees;

        private TextBox chartNameTextBox;
        private TextBox chartTitleTextBox;
        private CheckBox changeChartTitleCheckbox;

        private string initialChartTitle;

        internal IndividualLinksManagementAssistant(string reportItemName, Dictionary<int, PAX2SIM.TableScenarioInfoHolder> _tableScenarioHolderDictionary,
            string prototypeGraphicFilterName, string prototypeGraphicFilterTitle, GestionDonneesHUB2SIM donnees)
            : this(reportItemName, _tableScenarioHolderDictionary)
        {
            addGraphicFilterNameParametersUI();

            this.donnees = donnees;
            initialChartTitle = prototypeGraphicFilterTitle;

            string proposedNewChartName = prototypeGraphicFilterName + "_";
            for (int i = 1; i < 1000; i++)
            {
                string nameToCheck = proposedNewChartName + i;
                if (!donnees.GraphicFilterExist(nameToCheck))
                {
                    proposedNewChartName = nameToCheck;
                    break;
                }
            }
            chartNameTextBox.Text = proposedNewChartName;
            chartTitleTextBox.Text = prototypeGraphicFilterTitle;
        }

        internal IndividualLinksManagementAssistant(string reportItemName, Dictionary<int, PAX2SIM.TableScenarioInfoHolder> _tableScenarioHolderDictionary)
        {
            initComp();

            this.Text = "Links manager : " + reportItemName;

            tableScenarioHolderDictionary = _tableScenarioHolderDictionary;

            commonScenarios = getCommonScenarios(tableScenarioHolderDictionary);

            setUpInitialTableCombobox(tableScenarioHolderDictionary);
            addInitialLabels(tableScenarioHolderDictionary, linksGroupBox);
        }

        internal IndividualLinksManagementAssistant(string noteName, Dictionary<int, PAX2SIM.TableScenarioInfoHolder> _tableScenarioHolderDictionary,
            bool forNotesOnGlobalCharts)
        {
            initComp();

            this.Text = "Links manager: " + noteName;

            tableScenarioHolderDictionary = _tableScenarioHolderDictionary;
            
            commonScenarios = getCommonScenarios(tableScenarioHolderDictionary);

            setUpInitialTableCombobox(tableScenarioHolderDictionary);
            addInitialLabels(tableScenarioHolderDictionary, linksGroupBox);
            if (forNotesOnGlobalCharts)
                resetLabels();            
        }

        private void resetLabels()
        {
            tablesLabel.Text = "Notes :";
            availableLabel.Text = "Available Global Charts :";
        }

        private void initComp()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        private const string ALL = "ALL";
        private void setUpInitialTableCombobox(Dictionary<int, PAX2SIM.TableScenarioInfoHolder> tableScenarioHolderDictionary)
        {
            if (tableScenarioHolderDictionary != null)
            {
                tablesComboBox.Items.Clear();
                tablesComboBox.Items.Add(ALL);
                foreach (KeyValuePair<int, PAX2SIM.TableScenarioInfoHolder> pair in tableScenarioHolderDictionary)
                {
                    string tableIdAndName = pair.Value.tableId + TABLE_ID_NAME_DELIMITER + pair.Value.tableName;
                    tablesComboBox.Items.Add(tableIdAndName);
                }
                if (tablesComboBox.Items.Count > 0)
                    tablesComboBox.SelectedIndex = 0;
            }
        }


        private void addGraphicFilterNameParametersUI()
        {
            GroupBox parentGroupBox = new GroupBox();
            this.Controls.Add(parentGroupBox);
            parentGroupBox.Location = new Point(12, 12);
            parentGroupBox.BackColor = Color.Transparent;
            
            parentGroupBox.Width = linksGroupBox.Width;
            parentGroupBox.Height = 100;
            
            linksGroupBox.Location = new Point(12, 12 + parentGroupBox.Height + 10);

            Label chartNameLabel = new Label();
            parentGroupBox.Controls.Add(chartNameLabel);
            chartNameLabel.Text = "New chart name: ";
            chartNameLabel.BackColor = Color.Transparent;
            chartNameLabel.Location = new Point(32, 27);

            chartNameTextBox = new TextBox();
            parentGroupBox.Controls.Add(chartNameTextBox);
            chartNameTextBox.Width = 350;
            chartNameTextBox.Location = new Point(150, 24);

            Label chartTitleLabel = new Label();
            parentGroupBox.Controls.Add(chartTitleLabel);
            chartTitleLabel.Text = "New chart title: ";
            chartTitleLabel.BackColor = Color.Transparent;
            chartTitleLabel.Location = new Point(32, 60);

            chartTitleTextBox = new TextBox();
            parentGroupBox.Controls.Add(chartTitleTextBox);
            chartTitleTextBox.Width = 350;
            chartTitleTextBox.Location = new Point(150, 57);
            chartTitleTextBox.Enabled = false;

            changeChartTitleCheckbox = new CheckBox();
            parentGroupBox.Controls.Add(changeChartTitleCheckbox);
            changeChartTitleCheckbox.Location = new Point(525, 55);
            changeChartTitleCheckbox.Text = "Change the title";
            changeChartTitleCheckbox.CheckedChanged += new EventHandler(changeTitleCheckBox_CheckedChanged);
        }

        private void changeTitleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            chartTitleTextBox.Enabled = changeChartTitleCheckbox.Checked;
            if (!chartTitleTextBox.Enabled)
                chartTitleTextBox.Text = initialChartTitle;            
        }

        private void tablesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshAvailableScenarios();
            if (availableScenariosComboBox.Items.Count == 0)
                linkButton.Enabled = false;
            else
                linkButton.Enabled = true;
        }

        private void refreshAvailableScenarios()
        {
            availableScenariosComboBox.Items.Clear();
            string selectedTableAndId = tablesComboBox.SelectedItem.ToString();
            if (selectedTableAndId == ALL)
            {
                availableScenariosComboBox.Items.AddRange(commonScenarios.ToArray());
            }
            else if (selectedTableAndId.IndexOf(TABLE_ID_NAME_DELIMITER) != -1)
            {
                string tableId = selectedTableAndId.Substring(0, selectedTableAndId.IndexOf(TABLE_ID_NAME_DELIMITER));
                int tableIdNb = -1;
                if (Int32.TryParse(tableId.ToString(), out tableIdNb)
                    && tableScenarioHolderDictionary.ContainsKey(tableIdNb))
                {
                    string currentlyLinkedScenario = tableScenarioHolderDictionary[tableIdNb].linkedScenarioName;
                    foreach (string scenarioName in tableScenarioHolderDictionary[tableIdNb].scenariosContainingTable)
                    {
                        if (scenarioName != currentlyLinkedScenario)
                            availableScenariosComboBox.Items.Add(scenarioName);                        
                    }                    
                }
            }
            if (availableScenariosComboBox.Items.Count > 0)
                availableScenariosComboBox.SelectedIndex = 0;
        }

        private void addInitialLabels(Dictionary<int, PAX2SIM.TableScenarioInfoHolder> tableScenarioHolderDictionary, Control parent)
        {
            int initialLocationY = 125;
            foreach (KeyValuePair<int, SIMCORE_TOOL.PAX2SIM.TableScenarioInfoHolder> pair in tableScenarioHolderDictionary)
            {
                string tableIdNameAndScenarioName = pair.Value.tableId + TABLE_ID_NAME_DELIMITER + pair.Value.tableName
                    + TABLE_ID_SCENARIO_DELIMITER + pair.Value.linkedScenarioName;
                
                Label label = new Label();
                label.Text = tableIdNameAndScenarioName;
                label.AutoSize = true;
                label.BackColor = Color.Transparent;
                label.Location = new Point(18, initialLocationY);

                parent.Controls.Add(label);
                parent.Height = parent.Height + label.Height * 2;

                initialLocationY = initialLocationY + label.Height * 2;

                labelsDictionary.Add(pair.Value.tableId, label);                
            }
        }

        private void linkButton_Click(object sender, EventArgs e)
        {
            refreshTableScenarioHolderDictionary(tableScenarioHolderDictionary);
            refreshLabels(labelsDictionary);
            refreshAvailableScenarios();
        }
        
        private void refreshTableScenarioHolderDictionary(Dictionary<int, PAX2SIM.TableScenarioInfoHolder> tableScenarioHolderDictionary)
        {
            string tableIdAndName = tablesComboBox.SelectedItem.ToString();
            string scenarioName = availableScenariosComboBox.SelectedItem.ToString();
            if (tableIdAndName == ALL)
            {
                foreach (PAX2SIM.TableScenarioInfoHolder holder in tableScenarioHolderDictionary.Values)
                    holder.linkedScenarioName = scenarioName;
            }
            else if (tableIdAndName.IndexOf(TABLE_ID_NAME_DELIMITER) != -1)
            {
                string tableId = tableIdAndName.Substring(0, tableIdAndName.IndexOf(TABLE_ID_NAME_DELIMITER));
                int tableIdNb = -1;
                if (Int32.TryParse(tableId.ToString(), out tableIdNb)
                    && tableScenarioHolderDictionary.ContainsKey(tableIdNb))
                {
                    tableScenarioHolderDictionary[tableIdNb].linkedScenarioName = scenarioName;
                }
            }
        }

        private void refreshLabels(Dictionary<int, Label> labelsDictionary)
        {
            string tableIdAndName = tablesComboBox.SelectedItem.ToString();
            string scenarioName = availableScenariosComboBox.SelectedItem.ToString();
            if (tableIdAndName == ALL)
            {
                foreach (Label label in labelsDictionary.Values)
                {
                    if (!label.Text.Contains(TABLE_ID_SCENARIO_DELIMITER))
                        continue;
                    string tableName = label.Text.Substring(0, label.Text.IndexOf(TABLE_ID_SCENARIO_DELIMITER));
                    label.Text = tableName + TABLE_ID_SCENARIO_DELIMITER + scenarioName;
                }
            }
            else if (tableIdAndName.IndexOf(TABLE_ID_NAME_DELIMITER) != -1)
            {
                string tableId = tableIdAndName.Substring(0, tableIdAndName.IndexOf(TABLE_ID_NAME_DELIMITER));
                int tableIdNb = -1;
                if (Int32.TryParse(tableId.ToString(), out tableIdNb)
                    && labelsDictionary.ContainsKey(tableIdNb))
                {
                    string tableIdNameAndScenarioName = tableIdAndName + TABLE_ID_SCENARIO_DELIMITER + scenarioName;
                    labelsDictionary[tableIdNb].Text = tableIdNameAndScenarioName;
                }
            }
        }

        private List<string> getCommonScenarios(Dictionary<int, PAX2SIM.TableScenarioInfoHolder> tableScenarioHolderDictionary)
        {
            List<string> commonScenarios = new List<string>();
            foreach (PAX2SIM.TableScenarioInfoHolder holder in tableScenarioHolderDictionary.Values)
            {
                if (commonScenarios.Count == 0)
                {
                    if (holder.scenariosContainingTable.Count == 0)
                        break;
                    else
                        commonScenarios.AddRange(holder.scenariosContainingTable);
                }
                else
                {
                    List<string> intersection = commonScenarios.Intersect(holder.scenariosContainingTable).ToList();
                    commonScenarios = new List<string>(intersection);
                }
                if (commonScenarios.Count == 0)
                    break;
            }
            return commonScenarios;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (isGraphicFilterNameParametersMode() && chartNameIsValid())
                cloneGraphicFilterNameParameters = new CloneGraphicFilterNameEditor.Parameters(chartNameTextBox.Text, chartTitleTextBox.Text, changeChartTitleCheckbox.Checked);
        }

        private bool isGraphicFilterNameParametersMode()
        {
            return chartNameTextBox != null && chartTitleTextBox != null && changeChartTitleCheckbox != null;
        }

        private bool chartNameIsValid()
        {
            if (donnees != null && donnees.GraphicFilterExist(chartNameTextBox.Text))
            {
                MessageBox.Show(this, "The selected name for the chart is already in use. " + Environment.NewLine + "Please provide another name for the chart.",
                    "Unique Chart Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
                chartNameTextBox.Focus();
                return false;
            }
            return true;
        }
    }
}
