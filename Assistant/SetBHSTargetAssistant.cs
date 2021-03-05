using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class TargetAssistantBySummaryTable : Form
    {
        TreeNode scenarioNode;

        GestionDonneesHUB2SIM donnees;
            
        private DataTable summaryTable;

        public DataTable resultTable_;

        private List<String> percentileAttributeDegreeList = new List<String>();

        private string[] comparisonTypesList = new string[] { "<", "<=", "=", ">=", ">" };

        private string objectType = "";

        private List<DataRow> processObservedStatsRows = new List<DataRow>();

        private bool isPaxTimeDistributionSummary;

        public TargetAssistantBySummaryTable(TreeNode _scenarioNode, GestionDonneesHUB2SIM _donnees, 
            DataTable _summaryTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            scenarioNode = _scenarioNode;
            donnees = _donnees;
            summaryTable = _summaryTable;

            isPaxTimeDistributionSummary = checkIsPaxTimeDistributionSummary();

            objectType = getObjectType();
            setObservedObjectComboBox();
            setPercentileAttributeDegreeList();
            setUpDegreeComboBox();
            setUpComparisonComboBox();
        }

        private bool checkIsPaxTimeDistributionSummary()
        {
            if (summaryTable == null)
                return false;
            if (summaryTable.TableName.StartsWith(GlobalNames.PAX_DISTRIBUTION_TABLE_PREFIX)
                && summaryTable.TableName.EndsWith(GlobalNames.PAX_SUMMARY_DISTRIBUTION_TABLE_SUFFIX))
            {
                return true;
            }
            return false;
        }

        private string getObjectType()
        {
            string type = "";
            if (summaryTable == null)
                return type;
            if (isPaxTimeDistributionSummary)
                type = summaryTable.TableName;
            else if (summaryTable.TableName.IndexOf('_') != -1)
                type = summaryTable.TableName.Substring(0, summaryTable.TableName.IndexOf('_'));
            return type;
        }

        private void setObservedObjectComboBox()
        {
            if (summaryTable != null)
            {
                objectObservedComboBox.Items.Clear();
                if (isPaxTimeDistributionSummary)
                {
                    objectObservedComboBox.Items.Add(summaryTable.TableName);
                }
                else
                {
                    if (summaryTable.TableName.IndexOf("_Statistics_Dashboard") != -1)
                    {
                        objectObservedComboBox.Items
                            .Add(summaryTable.TableName.Substring(0, summaryTable.TableName.IndexOf("_Statistics_Dashboard")));
                    }
                    int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
                    if (kpiNameColumnIndex != -1)
                    {
                        foreach (DataRow row in summaryTable.Rows)
                        {
                            string kpiName = row[kpiNameColumnIndex].ToString();
                            if (kpiName.StartsWith(objectType))
                                objectObservedComboBox.Items.Add(kpiName);
                        }
                    }
                }
                if (objectObservedComboBox.Items.Count > 0)
                    objectObservedComboBox.SelectedIndex = 0;
            }
        }
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
        private void setUpDegreeComboBox()
        {
            degreeComboBox.Items.Clear();
            List<String> summaryTableColumnsNotUsedAsDegree
                = new List<String>(new string[] { GlobalNames.SUMMARY_TABLE_KPI_ID_COLUMN_NAME, GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME, 
                                                    GlobalNames.SUMMARY_TABLE_UNIT_COLUMN_NAME});
            foreach (DataColumn column in summaryTable.Columns)
            {
                if (summaryTableColumnsNotUsedAsDegree.Contains(column.ColumnName)
                    || column.ColumnName.Contains(GlobalNames.SUMMARY_TABLE_UNIT_COLUMN_NAME))
                {
                    continue;
                }
                else
                {
                    degreeComboBox.Items.Add(column.ColumnName);
                }
            }
            if (degreeComboBox.Items.Count > 0)
            {
                degreeComboBox.SelectedIndex = 0;
            }
        }
        private void setUpComparisonComboBox()
        {
            comparisonComboBox.Items.Clear();
            comparisonComboBox.Items.AddRange(comparisonTypesList);
            if (comparisonComboBox.Items.Count > 0)
                comparisonComboBox.SelectedIndex = 0;
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

        /// <summary>
        /// The summary table contains rows that represent the stats for each KPI.
        /// The global summary tables also contain their child stats: 
        /// first rows are the global stats, next row shows the child name in the KPI Name column and then the child stats and so on.
        /// Here we update the list containing the rows that represent the stats for the selected BHS object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void processObservedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bhsObjectObserved = objectObservedComboBox.Text;

            int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
            if (kpiNameColumnIndex != -1)
            {
                kpiComboBox.Items.Clear();
                processObservedStatsRows.Clear();
                List<DataRow> mainObjectStatsRows = new List<DataRow>();
                List<string> mainObjectKPIsNames = new List<string>();

                bool foundChildObject = false;
                bool finishedRetrievingMainObjectStats = false;
                foreach (DataRow row in summaryTable.Rows)
                {                    
                    string kpiName = row[kpiNameColumnIndex].ToString();

                    if (kpiName.StartsWith(objectType))
                        finishedRetrievingMainObjectStats = true;
                    if (!finishedRetrievingMainObjectStats)
                    {
                        mainObjectKPIsNames.Add(kpiName);
                        mainObjectStatsRows.Add(row);
                    }

                    if (kpiName.Equals(bhsObjectObserved))
                    {
                        foundChildObject = true;
                        continue;
                    }
                    else if (foundChildObject && kpiName.StartsWith(objectType))
                    {
                        break;
                    }
                    if (foundChildObject)
                    {
                        kpiComboBox.Items.Add(kpiName);
                        processObservedStatsRows.Add(row);
                    }
                }
                // we are observing the main bhs object
                if (kpiComboBox.Items.Count == 0
                    && mainObjectKPIsNames.Count > 0)
                {
                    kpiComboBox.Items.AddRange(mainObjectKPIsNames.ToArray());                    
                }
                // we are observing the main bhs object
                if (processObservedStatsRows.Count == 0
                    && mainObjectStatsRows.Count > 0)
                {
                    processObservedStatsRows.AddRange(mainObjectStatsRows);
                }
                if (kpiComboBox.Items.Count > 0)
                    kpiComboBox.SelectedIndex = 0;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!verifyInputData())
            {
                MessageBox.Show("Please check the input data.");
                DialogResult = DialogResult.None;
                return;
            }            
            if (summaryTable != null)
            {
                resultTable_ = checkStatisticTableAgainstDefinedTarget();
            }
        }
        
        private bool verifyInputData()
        {            
            if (!hasComboBoxValidData(objectObservedComboBox)
                || !hasComboBoxValidData(kpiComboBox)
                || !hasComboBoxValidData(degreeComboBox)
                || !hasComboBoxValidData(comparisonComboBox))
            {
                return false;
            }
            double value = 0;
            if (!Double.TryParse(targetValueTextBox.Text, out value))
                return false;
            return true;
        }
        private bool hasComboBoxValidData(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null || comboBox.SelectedItem.ToString() == "")
                return false;
            return true;
        }

        private DataTable checkStatisticTableAgainstDefinedTarget()
        {
            DataTable resultTable = null;
            string bhsObjectObservedName = objectObservedComboBox.SelectedItem.ToString();

            string kpiName = kpiComboBox.SelectedItem.ToString();
            string degree = degreeComboBox.SelectedItem.ToString();
            string comparisonType = comparisonComboBox.SelectedItem.ToString();
            string targetAchievedString = "";

            double valueObserved = Double.MinValue;
            valueObserved = getValueObserved(kpiName, degree);

            if (valueObserved != Double.MinValue)
            {
                bool targetAchieved = false;
                double difference = 0;
                double percentSuccess = 0;

                double targetValue = 0;
                if (Double.TryParse(targetValueTextBox.Text, out targetValue))
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
                                
                String resultTableName = bhsObjectObservedName + " " + GlobalNames.TARGET_TABLE_NAME_SUFIX;

                resultTable = donnees.getTable(scenarioNode.Name, resultTableName);
                if (resultTable == null)
                {
                    resultTable = createResultTable(resultTableName);
                    addRowToResultTable(resultTable, scenarioNode.Name, bhsObjectObservedName,
                        "", degree, kpiName, comparisonType, targetValue,
                        valueObserved, targetAchievedString, difference, percentSuccess);
                }
                else
                {
                    addRowToResultTable(resultTable, scenarioNode.Name, bhsObjectObservedName,
                        "", degree, kpiName, comparisonType, targetValue,
                        valueObserved, targetAchievedString, difference, percentSuccess);
                }
            }
            return resultTable;
        }

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

        private double getValueObserved(string kpiName, string degree)
        {
            double valueObserved = -1;
            if (processObservedStatsRows != null && processObservedStatsRows.Count > 0)
            {
                int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
                int valueColumnIndex = summaryTable.Columns.IndexOf(degree);
                if (kpiNameColumnIndex != -1 && valueColumnIndex != -1)
                {
                    foreach (DataRow row in processObservedStatsRows)
                    {
                        string currentKPIName = row[kpiNameColumnIndex].ToString();
                        if (currentKPIName == kpiName)
                        {
                            Double.TryParse(row[valueColumnIndex].ToString(), out valueObserved);
                            break;
                        }
                    }
                }
            }
            return valueObserved;
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

    }
}
