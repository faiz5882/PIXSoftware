using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SIMCORE_TOOL.com.crispico.EBS
{
    class EbsLogger
    {
        internal const string completeDateFormat = "dd/MM/yyyy HH:mm";
        internal const string dateFormat = "dd/MM/yyyy";

        internal const string GLOBAL_EBS_ISSUES_TABLE_NAME = "FPD_EBS_Issues";

        internal const string EBS_ISSUES_OVERFLOW_TIME_COLUMN_NAME = "Overflow Time (Input - EBS release Output - STD)";
        
        internal const string EBS_ISSUES_INPUT_OVERFLOW_TIME_COLUMN_NAME = "Input Overflow Time (EBS release)";        
        internal const string EBS_ISSUES_NB_INPUT_OVERFLOW_ECO_BAGS_COLUMN_NAME = "Number of input overflow Eco bags";
        internal const string EBS_ISSUES_NB_INPUT_OVERFLOW_FB_BAGS_COLUMN_NAME = "Number of input overflow Fb bags";
        internal const string EBS_ISSUES_NB_INPUT_OVERFLOW_TOTAL_BAGS_COLUMN_NAME = "Total Number of input overflow bags";

        internal const string EBS_ISSUES_OUTPUT_OVERFLOW_TIME_COLUMN_NAME = "Output Overflow Time (STD)";
        internal const string EBS_ISSUES_NB_OUTPUT_OVERFLOW_ECO_BAGS_COLUMN_NAME = "Number of output overflow Eco bags";
        internal const string EBS_ISSUES_NB_OUTPUT_OVERFLOW_FB_BAGS_COLUMN_NAME = "Number of output overflow Fb bags";
        internal const string EBS_ISSUES_NB_OUTPUT_OVERFLOW_TOTAL_BAGS_COLUMN_NAME = "Total Number of output overflow bags";

        internal const string EBS_ISSUES_NB_OVERFLOW_TOTAL_BAGS_COLUMN_NAME = "Total Number of overflow bags";
                
        const string EBS_ISSUES_FLIGHT_ID_COLUMN_NAME = "Flight Id";
        internal const string EBS_ISSUES_FLIGHT_COMPLETE_DATE_COLUMN_NAME = "Flight Date";
        const string EBS_ISSUES_AIRLINE_COLUMN_NAME = "Airline";
        const string EBS_ISSUES_FLIGHT_NUMBER_COLUMN_NAME = "Flight Number";
        const string EBS_ISSUES_FLIGHT_CATEGORY_COLUMN_NAME = "Flight Category";

        internal static EbsIssue getEbsIssue(DateTime overflowTime, string tableName, string flightId, string flightNb, string flightCategory, string airline,
            DateTime flightDate, TimeSpan flightTime, double nbOverflowBags, EbsIssue.BAG_TYPE bagType, EbsIssue.EBS_PROCESS_TYPE processType)
        {
            EbsFlight flight = new EbsFlight(flightId, flightNb, flightCategory, airline, flightDate, flightTime);
            EbsIssue issue = new EbsIssue(overflowTime, tableName, flight, nbOverflowBags, bagType, processType);
            return issue;
        }

        internal static DataTable createEbsIssuesTableStructure(string tableName)
        {
            DataTable ebsIssuesTable = new DataTable(tableName);

            ebsIssuesTable.Columns.Add(EBS_ISSUES_FLIGHT_ID_COLUMN_NAME, typeof(int));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_FLIGHT_COMPLETE_DATE_COLUMN_NAME, typeof(DateTime));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_AIRLINE_COLUMN_NAME, typeof(String));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_FLIGHT_NUMBER_COLUMN_NAME, typeof(String));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_FLIGHT_CATEGORY_COLUMN_NAME, typeof(String));
            
            ebsIssuesTable.Columns.Add(EBS_ISSUES_INPUT_OVERFLOW_TIME_COLUMN_NAME, typeof(DateTime));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_NB_INPUT_OVERFLOW_ECO_BAGS_COLUMN_NAME, typeof(Double));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_NB_INPUT_OVERFLOW_FB_BAGS_COLUMN_NAME, typeof(Double));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_NB_INPUT_OVERFLOW_TOTAL_BAGS_COLUMN_NAME, typeof(Double));

            ebsIssuesTable.Columns.Add(EBS_ISSUES_OUTPUT_OVERFLOW_TIME_COLUMN_NAME, typeof(DateTime));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_NB_OUTPUT_OVERFLOW_ECO_BAGS_COLUMN_NAME, typeof(Double));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_NB_OUTPUT_OVERFLOW_FB_BAGS_COLUMN_NAME, typeof(Double));
            ebsIssuesTable.Columns.Add(EBS_ISSUES_NB_OUTPUT_OVERFLOW_TOTAL_BAGS_COLUMN_NAME, typeof(Double));

            ebsIssuesTable.Columns.Add(EBS_ISSUES_NB_OVERFLOW_TOTAL_BAGS_COLUMN_NAME, typeof(Double));

            return ebsIssuesTable;
        }

        internal static void fillEbsIssuesTable(DataTable ebsIssuesTable, Dictionary<string, EbsFlightStatistics> ebsFlightStatistics)
        {
            List<int> indexes = new List<int>();

            int flightIdColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_FLIGHT_ID_COLUMN_NAME);
            indexes.Add(flightIdColumnIndex);
            int flightNbColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_FLIGHT_NUMBER_COLUMN_NAME);
            indexes.Add(flightNbColumnIndex);
            int flightDateColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_FLIGHT_COMPLETE_DATE_COLUMN_NAME);
            indexes.Add(flightDateColumnIndex);
            int flightCategoryColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_FLIGHT_CATEGORY_COLUMN_NAME);
            indexes.Add(flightCategoryColumnIndex);
            int airlineColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_AIRLINE_COLUMN_NAME);
            indexes.Add(airlineColumnIndex);

            int inputOverFlowTimeColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_INPUT_OVERFLOW_TIME_COLUMN_NAME);
            indexes.Add(inputOverFlowTimeColumnIndex);
            int nbInputOverflowEcoBagsColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_NB_INPUT_OVERFLOW_ECO_BAGS_COLUMN_NAME);
            indexes.Add(nbInputOverflowEcoBagsColumnIndex);
            int nbInputOverflowFbBagsColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_NB_INPUT_OVERFLOW_FB_BAGS_COLUMN_NAME);
            indexes.Add(nbInputOverflowFbBagsColumnIndex);
            int nbInputOverflowTotalBagsColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_NB_INPUT_OVERFLOW_TOTAL_BAGS_COLUMN_NAME);
            indexes.Add(nbInputOverflowFbBagsColumnIndex);

            int outputOverFlowTimeColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_OUTPUT_OVERFLOW_TIME_COLUMN_NAME);
            indexes.Add(outputOverFlowTimeColumnIndex);
            int nbOutputOverflowEcoBagsColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_NB_OUTPUT_OVERFLOW_ECO_BAGS_COLUMN_NAME);
            indexes.Add(nbOutputOverflowEcoBagsColumnIndex);
            int nbOutputOverflowFbBagsColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_NB_OUTPUT_OVERFLOW_FB_BAGS_COLUMN_NAME);
            indexes.Add(nbOutputOverflowFbBagsColumnIndex);
            int nbOutputOverflowTotalBagsColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_NB_OUTPUT_OVERFLOW_TOTAL_BAGS_COLUMN_NAME);
            indexes.Add(nbOutputOverflowTotalBagsColumnIndex);

            int nbOverflowTotalBagsColumnIndex = ebsIssuesTable.Columns.IndexOf(EBS_ISSUES_NB_OVERFLOW_TOTAL_BAGS_COLUMN_NAME);
            indexes.Add(nbOutputOverflowTotalBagsColumnIndex);

            if (!areValidColumnIndexes(indexes))
            {
                OverallTools.ExternFunctions.PrintLogFile("Error while inserting data into the EBS Issues table (\"" + ebsIssuesTable.TableName + "\"). "
                    + "The table is missing a column.");
                return;
            }
            foreach (KeyValuePair<string, EbsFlightStatistics> pair in ebsFlightStatistics)
            {
                EbsFlightStatistics stats = pair.Value;
                DataRow row = ebsIssuesTable.NewRow();
                int flightId = -1;
                if (Int32.TryParse(stats.flight.id, out flightId))
                {
                    row[flightIdColumnIndex] = flightId;
                    row[flightNbColumnIndex] = stats.flight.number;
                    row[flightDateColumnIndex] = stats.flight.flightDate.Add(stats.flight.flightTime);
                    row[flightCategoryColumnIndex] = stats.flight.flightCategory;
                    row[airlineColumnIndex] = stats.flight.airline;

                    row[inputOverFlowTimeColumnIndex] = stats.inputOverflowTime;
                    row[nbInputOverflowEcoBagsColumnIndex] = stats.inputOverflowNbEcoBags;
                    row[nbInputOverflowFbBagsColumnIndex] = stats.inputOverflowNbFbBags;
                    row[nbInputOverflowTotalBagsColumnIndex] = stats.inputOverflowNbTotalBags;

                    row[outputOverFlowTimeColumnIndex] = stats.outputOverflowTime;
                    row[nbOutputOverflowEcoBagsColumnIndex] = stats.outputOverflowNbEcoBags;
                    row[nbOutputOverflowFbBagsColumnIndex] = stats.outputOverflowNbFbBags;
                    row[nbOutputOverflowTotalBagsColumnIndex] = stats.outputOverflowNbTotalBags;

                    row[nbOverflowTotalBagsColumnIndex] = stats.totalNbOverflowBags;
                    ebsIssuesTable.Rows.Add(row);
                }
                else
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while inserting data into the EBS Issues table (\"" + ebsIssuesTable.TableName + "\"). "
                        + "The flight id \"" + flightId + "\" is not valid.");
                    continue;
                }
            }
        }

        public static bool areValidColumnIndexes(List<int> indexes)
        {
            foreach (int index in indexes)
            {
                if (index < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
