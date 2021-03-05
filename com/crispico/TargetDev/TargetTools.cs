using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using SIMCORE_TOOL.Prompt.Vinci;
using SIMCORE_TOOL.Assistant;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.com.crispico.TargetDev
{
    class TargetTools
    {
        internal static List<DataTable> getAllTargetTablesByTargetDirectory(TreeNode targetNode, string scenarioName,
            GestionDonneesHUB2SIM donnees)
        {
            List<DataTable> targetTables = new List<DataTable>();
            if (targetNode != null && targetNode.Nodes != null 
                && targetNode.Nodes.Count > 0)
            {
                foreach (TreeNode targetChildNode in targetNode.Nodes)
                {
                    if (donnees.tableEstPresente(scenarioName, targetChildNode.Name))
                    {
                        DataTable targetTable = donnees.getTable(scenarioName, targetChildNode.Name);
                        if (targetTable != null && !targetTable.TableName.Equals(TargetOverview.TARGET_OVERVIEW_TABLE_NAME))
                        {
                            targetTables.Add(targetTable);
                        }
                    }
                }                
            }
            return targetTables;
        }

        internal static void updateTargetTable(DataTable targetTable, string scenarioName,
            GestionDonneesHUB2SIM donnees)
        {
            ParamScenario scenarioParams = donnees.GetScenario(scenarioName);   // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            if (targetTable != null && scenarioParams != null)
            {
                List<string> percentileAttributeDegreeList = getPercentileAttributeDegreeList(scenarioParams.percentilesLevels);
                int processObservedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_processObserved_columnName);
                int statisticTypeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_statisticType_columnName);
                int statisticAttributeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_statisticAttribute_columnName);
                int attributeDegreeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_attributeDegree_columnName);
                
                int comparisonTypeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_comparisonType_columnName);
                int targetValueColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_targetValue_columnName);

                int valueObservedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_valueObserved_columnName);

                int targetAchivedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_targetAchived_columnName);
                int differenceColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_difference_columnName);
                int percentSuccessColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_percentSuccess_columnName);

                if (processObservedColumnIndex != -1 && statisticTypeColumnIndex != -1 
                    && statisticAttributeColumnIndex != -1 && attributeDegreeColumnIndex != -1
                    && comparisonTypeColumnIndex != -1 && targetValueColumnIndex != -1
                    && valueObservedColumnIndex != -1)
                {
                    foreach (DataRow row in targetTable.Rows)
                    {
                        string processObserved = "";
                        string statisticType = "";
                        string statisticAttribute = "";
                        string attributeDegree = "";
                        string comparisonType = "";
                        double targetValue = -1;

                        if (row[processObservedColumnIndex] != null && row[statisticTypeColumnIndex] != null
                            && row[statisticAttributeColumnIndex] != null && row[attributeDegreeColumnIndex] != null
                            && row[comparisonTypeColumnIndex] != null
                            && row[targetValueColumnIndex] != null && Double.TryParse(row[targetValueColumnIndex].ToString(), out targetValue))
                        {
                            processObserved = row[processObservedColumnIndex].ToString();
                            statisticType = row[statisticTypeColumnIndex].ToString();
                            statisticAttribute = row[statisticAttributeColumnIndex].ToString();
                            attributeDegree = row[attributeDegreeColumnIndex].ToString();
                            comparisonType = row[comparisonTypeColumnIndex].ToString();

                            DataTable summaryTable = getSummaryTableByProcessObserved(processObserved, scenarioName, donnees);
                            if (summaryTable != null)
                            {
                                double valueObserved = getValueObserved(summaryTable, statisticType, statisticAttribute, attributeDegree, percentileAttributeDegreeList);
                                string calcTargetAchieved = "";
                                double calcDifference = 0;
                                double calcPercSuccess = 0;
                                recalculateTargetTableResults(comparisonType, valueObserved, targetValue,
                                    out calcTargetAchieved, out calcDifference, out calcPercSuccess);
                                if (valueObservedColumnIndex != -1 && targetAchivedColumnIndex != -1
                                    && differenceColumnIndex != -1 && percentSuccessColumnIndex != -1)
                                {
                                    row[valueObservedColumnIndex] = valueObserved;
                                    row[targetAchivedColumnIndex] = calcTargetAchieved;
                                    row[differenceColumnIndex] = calcDifference;
                                    row[percentSuccessColumnIndex] = calcPercSuccess;
                                }
                            }
                        }
                    }
                    targetTable.AcceptChanges();
                }
            }
        }

        private static DataTable getSummaryTableByProcessObserved(string processObserved, 
            string scenarioName, GestionDonneesHUB2SIM donnees)
        {
            DataTable summaryTable = null;
            if (processObserved != null)
            {
                if (processObserved == SetTargetAssistant.PROCESS_TYPE_AIRPORT)
                    processObserved = GlobalNames.AIRPORT_REPORTS_NODE_NAME;

                string summaryTableName = "";
                if (processObserved.IndexOf("(") != -1)
                {
                    summaryTableName = processObserved.Substring(0, processObserved.IndexOf("(")).Trim();
                }
                else
                {
                    summaryTableName = processObserved;
                }
                if (summaryTableName != "")
                {
                    summaryTable = donnees.getTable(scenarioName, summaryTableName);
                }
            }
            return summaryTable;
        }

        private static double getValueObserved(DataTable summaryTable, String statisticType,
            String statisticAttribute, String attributeDegree, List<string> percentileAttributeDegreeList)
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
                    valueObserved = getValueFromSummaryTable(summaryTable, statisticAttribute, GlobalNames.SUMMARY_TABLE_MIN_VALUE_COLUMN_NAME);
                }
                else if (attributeDegree.Equals(GlobalNames.ATTRIBUTE_DEGREE_MAX))
                {
                    valueObserved = getValueFromSummaryTable(summaryTable, statisticAttribute, GlobalNames.SUMMARY_TABLE_MAX_VALUE_COLUMN_NAME);
                }
                else if (attributeDegree.Equals(GlobalNames.ATTRIBUTE_DEGREE_AVG))
                {
                    valueObserved = getValueFromSummaryTable(summaryTable, statisticAttribute, GlobalNames.SUMMARY_TABLE_AVG_VALUE_COLUMN_NAME);
                }
                else if (percentileAttributeDegreeList.Contains(attributeDegree))
                {
                    valueObserved = getValueFromSummaryTable(summaryTable, statisticAttribute, attributeDegree);
                }
                else if (attributeDegree.Equals(GlobalNames.ATTRIBUTE_DEGREE_TOTAL))    // >> Task #11217 Pax2Sim - Target - add missing Attribute degree stage2
                {
                    valueObserved = getValueFromSummaryTable(summaryTable, statisticAttribute, GlobalNames.SUMMARY_TABLE_VALUE_COLUMN_NAME);
                }
            }
            else
            {
                valueObserved = getValueFromSummaryTable(summaryTable, statisticAttribute, attributeDegree);
            }
            return valueObserved;
        }
                
        private static double getValueFromSummaryTable(DataTable summaryTable, String statisticAttribute, String attributeDegree)
        {
            double valueFromSummary = -1;
            if (summaryTable == null)
                return valueFromSummary;

            String summaryKpiName = "";
            if (GlobalNames.targetStatisticAttributeSummaryTableStatisticNameMap.ContainsKey(statisticAttribute))
            {
                GlobalNames.targetStatisticAttributeSummaryTableStatisticNameMap.TryGetValue(statisticAttribute, out summaryKpiName);
            }
            else
            {
                summaryKpiName = statisticAttribute;
            }
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
            
            
            return valueFromSummary;
        }

        private static List<string> getPercentileAttributeDegreeList(List<double> percentilesList)
        {
            List<string> percentileAttributeDegreeList = new List<string>();
            if (percentilesList != null)
            {
                for (int i = 0; i < percentilesList.Count; i++)
                {
                    String percentileLabel = percentilesList[i].ToString() + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX;
                    percentileAttributeDegreeList.Add(percentileLabel);
                }
            }
            return percentileAttributeDegreeList;
        }

        private static void recalculateTargetTableResults(string comparisonType, double valueObserved, double targetValue,
            out string calcTargetAchieved, out double calcDifference, out double calcPercSuccess)
        {
            calcTargetAchieved = "";
            calcDifference = 0;
            calcPercSuccess = 0;
            
            if (comparisonType.Contains(">"))
            {
                calcPercSuccess = (valueObserved / targetValue) * 100;
                if (!comparisonType.Contains("=") && valueObserved == targetValue)
                    calcPercSuccess--;
            }
            else if (comparisonType.Contains("<"))
            {
                calcPercSuccess = (targetValue / valueObserved) * 100;
                if (!comparisonType.Contains("=") && valueObserved == targetValue)
                    calcPercSuccess--;
            }
            else if (comparisonType.Equals("="))
            {
                calcPercSuccess = (Math.Min(targetValue, valueObserved) / Math.Max(targetValue, valueObserved)) * 100;
                if (targetValue == valueObserved)
                    calcPercSuccess = 100;
            }
            calcPercSuccess = Math.Round(calcPercSuccess, 2);
            calcDifference = Math.Round(Math.Abs(targetValue - valueObserved), 2);
            if (isTargetAchieved(targetValue, valueObserved, comparisonType))
            {
                calcTargetAchieved = GlobalNames.TARGET_ACHIEVED_POSITIVE;
            }
            else
            {
                calcTargetAchieved = GlobalNames.TARGET_ACHIEVED_NEGATIVE;
                calcDifference = calcDifference * (-1);
            }
        }

        private static bool isTargetAchieved(double targetValue, double valueObserved, String comparisonType)
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

        // >> Task #13390 Targets for BHS Analysis output tables        
        internal static void updateBHSTargetTable(DataTable targetTable, string scenarioName,
            GestionDonneesHUB2SIM donnees)
        {
            if (targetTable != null)
            {
                //List<string> percentileAttributeDegreeList = getPercentileAttributeDegreeList(donnees);
                int processObservedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_processObserved_columnName);
                int statisticTypeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_statisticType_columnName);
                int statisticAttributeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_statisticAttribute_columnName);
                int attributeDegreeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_attributeDegree_columnName);

                int comparisonTypeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_comparisonType_columnName);
                int targetValueColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_targetValue_columnName);

                int valueObservedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_valueObserved_columnName);

                int targetAchivedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_targetAchived_columnName);
                int differenceColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_difference_columnName);
                int percentSuccessColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_percentSuccess_columnName);

                if (processObservedColumnIndex != -1 && statisticTypeColumnIndex != -1
                    && statisticAttributeColumnIndex != -1 && attributeDegreeColumnIndex != -1
                    && comparisonTypeColumnIndex != -1 && targetValueColumnIndex != -1
                    && valueObservedColumnIndex != -1)
                {
                    foreach (DataRow row in targetTable.Rows)
                    {
                        string processObserved = "";
                        string statisticType = "";
                        string statisticAttribute = "";
                        string attributeDegree = "";
                        string comparisonType = "";
                        double targetValue = -1;

                        if (row[processObservedColumnIndex] != null && row[statisticTypeColumnIndex] != null
                            && row[statisticAttributeColumnIndex] != null && row[attributeDegreeColumnIndex] != null
                            && row[comparisonTypeColumnIndex] != null
                            && row[targetValueColumnIndex] != null && Double.TryParse(row[targetValueColumnIndex].ToString(), out targetValue))
                        {
                            processObserved = row[processObservedColumnIndex].ToString();
                            statisticType = row[statisticTypeColumnIndex].ToString();
                            statisticAttribute = row[statisticAttributeColumnIndex].ToString();
                            attributeDegree = row[attributeDegreeColumnIndex].ToString();
                            comparisonType = row[comparisonTypeColumnIndex].ToString();

                            string summaryTableName = processObserved + "_Statistics" + GlobalNames.BHS_STATS_TABLE_SUFFIX;
                            DataTable summaryTable = donnees.getTable(scenarioName, summaryTableName);
                            if (summaryTable != null)
                            {
                                double valueObserved = getBHSValueObserved(summaryTable, processObserved, statisticAttribute, attributeDegree);
                                string calcTargetAchieved = "";
                                double calcDifference = 0;
                                double calcPercSuccess = 0;
                                recalculateTargetTableResults(comparisonType, valueObserved, targetValue,
                                    out calcTargetAchieved, out calcDifference, out calcPercSuccess);
                                if (valueObservedColumnIndex != -1 && targetAchivedColumnIndex != -1
                                    && differenceColumnIndex != -1 && percentSuccessColumnIndex != -1)
                                {
                                    row[valueObservedColumnIndex] = valueObserved;
                                    row[targetAchivedColumnIndex] = calcTargetAchieved;
                                    row[differenceColumnIndex] = calcDifference;
                                    row[percentSuccessColumnIndex] = calcPercSuccess;
                                }
                            }
                        }
                    }
                    targetTable.AcceptChanges();
                }
            }
        }

        private static double getBHSValueObserved(DataTable summaryTable, string processObserved, string statisticAttribute, string attributeDegree)
        {
            List<DataRow> observedRows = getObservedRowsByBHSObjectName(summaryTable, processObserved);
            double valueObserved = getBHSValueObservedFromObservedRows(observedRows, statisticAttribute, attributeDegree, summaryTable);
            return valueObserved;
        }

        private static List<DataRow> getObservedRowsByBHSObjectName(DataTable summaryTable, string processObserved)
        {
            string bhsObjectObserved = processObserved;
            List<DataRow> bhsObjectObservedStatsRows = new List<DataRow>();

            if (processObserved == null || processObserved.IndexOf('_') < 0)
                return bhsObjectObservedStatsRows;

            string bhsObjectType = processObserved.Substring(0, processObserved.IndexOf('_'));
            int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);

            if (kpiNameColumnIndex != -1)
            {
                List<DataRow> mainObjectStatsRows = new List<DataRow>();             

                bool foundChildObject = false;
                bool finishedRetrievingMainObjectStats = false;
                foreach (DataRow row in summaryTable.Rows)
                {
                    string kpiName = row[kpiNameColumnIndex].ToString();

                    if (kpiName.StartsWith(bhsObjectType))
                        finishedRetrievingMainObjectStats = true;
                    if (!finishedRetrievingMainObjectStats)
                    {                        
                        mainObjectStatsRows.Add(row);
                    }

                    if (kpiName.Equals(bhsObjectObserved))
                    {
                        foundChildObject = true;
                        continue;
                    }
                    else if (foundChildObject && kpiName.StartsWith(bhsObjectType))
                    {
                        break;
                    }
                    if (foundChildObject)
                    {                        
                        bhsObjectObservedStatsRows.Add(row);
                    }
                }                
                // we are observing the main bhs object
                if (bhsObjectObservedStatsRows.Count == 0
                    && mainObjectStatsRows.Count > 0)
                {
                    bhsObjectObservedStatsRows.AddRange(mainObjectStatsRows);
                }
            }
            return bhsObjectObservedStatsRows;
        }

        private static double getBHSValueObservedFromObservedRows(List<DataRow> observedRows, string kpiName, string degree,
            DataTable summaryTable)
        {
            double valueObserved = -1;
            if (summaryTable == null)
                return valueObserved;

            if (observedRows != null && observedRows.Count > 0)
            {
                int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
                int valueColumnIndex = summaryTable.Columns.IndexOf(degree);
                if (kpiNameColumnIndex != -1 && valueColumnIndex != -1)
                {
                    foreach (DataRow row in observedRows)
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
        // << Task #13390 Targets for BHS Analysis output tables

        internal static bool tryGetScenarioNameAndNodeNameFromTargetTableByRowIndex(DataTable targetTable, int rowIndex, out string scenarioName, out string nodeName)
        {
            scenarioName = "";
            nodeName = "";
            if (targetTable == null || targetTable.Rows.Count == 0 
                || targetTable.Columns.IndexOf(GlobalNames.target_scenarioName_columnName) == -1
                || targetTable.Columns.IndexOf(GlobalNames.target_processObserved_columnName) == -1)
            {
                return false;
            }
            int columnIndexScenarioName = targetTable.Columns.IndexOf(GlobalNames.target_scenarioName_columnName);
            int columnIndexProcessObserved = targetTable.Columns.IndexOf(GlobalNames.target_processObserved_columnName);
            for (int i = 0; i < targetTable.Rows.Count; i++)
            {
                if (i != rowIndex)
                {
                    continue;
                }
                DataRow row = targetTable.Rows[i];
                if (row[columnIndexScenarioName] != null && row[columnIndexProcessObserved] != null)
                {
                    scenarioName = row[columnIndexScenarioName].ToString();
                    nodeName = row[columnIndexProcessObserved].ToString();
                    break;
                }
            }
            if (nodeName.Contains(" ("))
            {
                nodeName = nodeName.Substring(0, nodeName.IndexOf(" ("));
            }
            return true;
        }
    }
}
