using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SIMCORE_TOOL.com.crispico.EBS
{
    class EbsStatisticsGenerator
    {
        internal const string GLOBAL_EBS_SUMMARY_TABLE_NAME = "FPD_EBS_Summary";

        const int INVALID_NB = -1;

        public static Dictionary<string, EbsFlightStatistics> getEbsIssuesStatistics(List<EbsIssue> ebsIssues)
        {
            Dictionary<string, EbsFlightStatistics> ebsIssuesStatistics = new Dictionary<string, EbsFlightStatistics>();
            foreach (EbsIssue issue in ebsIssues)
            {
                EbsFlightStatistics ebsFlightStatistics = null;
                if (!ebsIssuesStatistics.ContainsKey(issue.flight.id))
                {
                    ebsFlightStatistics = new EbsFlightStatistics();
                    ebsFlightStatistics.relatedTableName = issue.relatedTableName;
                    ebsFlightStatistics.flight = issue.flight;
                    updateEbsFlightStatistics(ebsFlightStatistics, issue);
                    ebsIssuesStatistics.Add(ebsFlightStatistics.flight.id, ebsFlightStatistics);
                }
                else
                {
                    ebsFlightStatistics = ebsIssuesStatistics[issue.flight.id];
                    updateEbsFlightStatistics(ebsFlightStatistics, issue);
                }
            }
            return ebsIssuesStatistics;
        }

        private static void updateEbsFlightStatistics(EbsFlightStatistics ebsFlightStatistics, EbsIssue issue)
        {
            if (issue.processType == EbsIssue.EBS_PROCESS_TYPE.INPUT)
            {
                ebsFlightStatistics.inputOverflowTime = issue.overflowTime;
                if (issue.bagType == EbsIssue.BAG_TYPE.ECO)
                {
                    ebsFlightStatistics.inputOverflowNbEcoBags = issue.nbOverflowBags;
                }
                else if (issue.bagType == EbsIssue.BAG_TYPE.FB)
                {
                    ebsFlightStatistics.inputOverflowNbFbBags = issue.nbOverflowBags;
                }
            }
            else if (issue.processType == EbsIssue.EBS_PROCESS_TYPE.OUTPUT)
            {
                ebsFlightStatistics.outputOverflowTime = issue.overflowTime;
                if (issue.bagType == EbsIssue.BAG_TYPE.ECO)
                {
                    ebsFlightStatistics.outputOverflowNbEcoBags = issue.nbOverflowBags;
                }
                else if (issue.bagType == EbsIssue.BAG_TYPE.FB)
                {
                    ebsFlightStatistics.outputOverflowNbFbBags = issue.nbOverflowBags;
                }
            }
        }

        public static List<EbsSummaryKpi> getEbsSummaryKpis(DataTable statsTable)
        {
            List<EbsSummaryKpi> kpis = new List<EbsSummaryKpi>();
            if (statsTable == null)
            {
                return kpis;
            }
            int dataColumnIndex = statsTable.Columns.IndexOf(OverallTools.DataFunctions.STATS_TABLE_DATA_COLUMN_NAME);
            int minColumnIndex = statsTable.Columns.IndexOf(OverallTools.DataFunctions.STATS_TABLE_MIN_COLUMN_NAME);
            int avgColumnIndex = statsTable.Columns.IndexOf(OverallTools.DataFunctions.STATS_TABLE_AVG_COLUMN_NAME);
            int maxColumnIndex = statsTable.Columns.IndexOf(OverallTools.DataFunctions.STATS_TABLE_MAX_COLUMN_NAME);
            int sumColumnIndex = statsTable.Columns.IndexOf(OverallTools.DataFunctions.STATS_TABLE_SUM_COLUMN_NAME);
            if (dataColumnIndex == -1 || minColumnIndex == -1 || avgColumnIndex == -1
                || maxColumnIndex == -1 || sumColumnIndex == -1)
            {
                return kpis;
            }
            int id = 1;
            string name = "";
            double min = 0;
            double avg = 0;
            double max = 0;
            double sum = 0;
            string unit = "";
            foreach (DataRow row in statsTable.Rows)
            {
                if (row[dataColumnIndex] != null)
                {
                    name = row[dataColumnIndex].ToString();
                }
                else
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while retrieving the name of the EBS Summary kpi from \"" + statsTable.TableName + "\".");
                    id++;
                    continue;
                }
                if (row[minColumnIndex] == null || !double.TryParse(row[minColumnIndex].ToString(), out min))
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while retrieving the minimum value for the EBS Summary kpi from \"" + statsTable.TableName + "\".");
                    id++;
                    continue;
                }
                if (row[avgColumnIndex] == null || !double.TryParse(row[avgColumnIndex].ToString(), out avg))
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while retrieving the average value for the EBS Summary kpi from \"" + statsTable.TableName + "\".");
                    id++;
                    continue;
                }
                if (row[maxColumnIndex] == null || !double.TryParse(row[maxColumnIndex].ToString(), out max))
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while retrieving the maximum value for the EBS Summary kpi from \"" + statsTable.TableName + "\".");
                    id++;
                    continue;
                }
                if (row[sumColumnIndex] == null || !double.TryParse(row[sumColumnIndex].ToString(), out sum))
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while retrieving the total value for the EBS Summary kpi from \"" + statsTable.TableName + "\".");
                    id++;
                    continue;
                }
                unit = "Bag(s)";
                if (name.Contains("/ h"))
                {
                    unit = "Bag(s) / h";
                }
                EbsSummaryKpi kpi = new EbsSummaryKpi(id, name, sum, unit, min, avg, max, unit);
                kpis.Add(kpi);
                id++;
            }
            return kpis;
        }

        public static DataTable getEbsSummaryTableStructure(string tableName)
        {
            DataTable summaryTable = new DataTable(tableName);            
            summaryTable.Columns.Add(GlobalNames.SUMMARY_TABLE_KPI_ID_COLUMN_NAME, typeof(Int32));
            summaryTable.Columns.Add(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME, typeof(String));            
            summaryTable.Columns.Add(GlobalNames.SUMMARY_TABLE_VALUE_COLUMN_NAME, typeof(Double));
            summaryTable.Columns.Add(GlobalNames.BHS_STATS_TABLE_TOTAL_VALUE_UNIT_COLUMN_NAME, typeof(String));
            summaryTable.Columns.Add(GlobalNames.SUMMARY_TABLE_MIN_VALUE_COLUMN_NAME, typeof(Double));
            summaryTable.Columns.Add(GlobalNames.SUMMARY_TABLE_AVG_VALUE_COLUMN_NAME, typeof(Double));
            summaryTable.Columns.Add(GlobalNames.SUMMARY_TABLE_MAX_VALUE_COLUMN_NAME, typeof(Double));
            summaryTable.Columns.Add(GlobalNames.SUMMARY_TABLE_UNIT_COLUMN_NAME, typeof(String));
            return summaryTable;
        }

        public static void addKpisToEbsSummaryTable(DataTable summaryTable, List<EbsSummaryKpi> kpis)
        {            
            int kpiIdColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_KPI_ID_COLUMN_NAME);
            int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
            int totalValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_VALUE_COLUMN_NAME);
            int totalValueUnitColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.BHS_STATS_TABLE_TOTAL_VALUE_UNIT_COLUMN_NAME);
            int minValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MIN_VALUE_COLUMN_NAME);
            int avgValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_AVG_VALUE_COLUMN_NAME);
            int maxValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MAX_VALUE_COLUMN_NAME);
            int unitColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_UNIT_COLUMN_NAME);

            foreach (EbsSummaryKpi kpi in kpis)
            {
                DataRow row = summaryTable.NewRow();
                row[kpiIdColumnIndex] = kpi.id;
                row[kpiNameColumnIndex] = kpi.name;
                row[totalValueColumnIndex] = kpi.total;
                row[totalValueUnitColumnIndex] = kpi.totalUnit;
                row[minValueColumnIndex] = kpi.min;
                row[avgValueColumnIndex] = kpi.avg;
                row[maxValueColumnIndex] = kpi.max;
                row[unitColumnIndex] = kpi.unit;
                summaryTable.Rows.Add(row);
            }            
        }
    }
}
