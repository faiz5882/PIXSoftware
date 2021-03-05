using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace SIMCORE_TOOL.com.crispico.dashboard
{
    // >> Task #10553 Pax2Sim - Pax analysis - Summary - Dashboard
    /// <summary>
    /// Helper functions for the Dashboard task.
    /// </summary>
    class DashboardTools
    {
        // >> Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats
        public const String KPI_VISIBLE_BY_DEFAULT_GLOBAL_SUFFIX = "_Global";
        public const String KPI_VISIBLE_BY_DEFAULT_STANDARD_BAG_SUFFIX = "_Standard_Bags";
        public const String KPI_VISIBLE_BY_DEFAULT_MES_SUFFIX = "_MES";
        public const String KPI_VISIBLE_BY_DEFAULT_NO_MES_SUFFIX = "WithoutMES";
        public const String KPI_VISIBLE_BY_DEFAULT_INTERLINK_SUFFIX = "Interlink";
        public const String KPI_VISIBLE_BY_DEFAULT_EBS_SUFFIX = "EBS";
        public const String KPI_VISIBLE_BY_DEFAULT_ULD_SUFFIX = "ULD";
        // << Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats

        public static bool getLevelsPercentUsingSummaryTable(DataTable summaryTable, 
            out double level1, out double level2, out double level3)
        {
            level1 = -1;
            level2 = -1;
            level3 = -1;

            if (summaryTable == null)
                return false;

            List<String> percentileColumnNamesList = new List<String>();
            foreach (DataColumn column in summaryTable.Columns)
            {
                if (column.ColumnName.EndsWith(GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX))
                {
                    percentileColumnNamesList.Add(column.ColumnName);
                }
            }

            if (percentileColumnNamesList.Count != 3)
                return false;

            level1 = getPercentNbFromColumnNamesList(percentileColumnNamesList, 0);
            level2 = getPercentNbFromColumnNamesList(percentileColumnNamesList, 1);
            level3 = getPercentNbFromColumnNamesList(percentileColumnNamesList, 2);

            if (level1 != -1 && level2 != -1 && level3 != -1)
            {
                return true;
            }
            return false;
        }
        
        private static double getPercentNbFromColumnNamesList(List<String> columnNamesList, int index)
        {
            double level = -1;

            if (index < 0 || index > 2)
                return level;

            String partialColumnName = columnNamesList[index].Substring(0, columnNamesList[index].IndexOf(GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX));
            if (partialColumnName != null)
            {
                string percent = partialColumnName.Trim();
                if (!Double.TryParse(percent, out level))
                {
                    level = -1;
                }
            }
            return level;
        }

        public static List<string> kpisNotIncludedInDashboardList
            = new List<string>(new string[] { GlobalNames.SUMMARY_KPI_NAME_THROUGHPUT_INSTANT_INPUT, GlobalNames.SUMMARY_KPI_NAME_THROUGHPUT_INSTANT_OUTPUT });   // >> Task #10553 Pax2Sim - Pax analysis - Summary - Dashboard
        public static List<SummaryKPI> getSummaryKPIsListFromSummaryTable(DataTable summaryTable,
            double level1, double level2, double level3)
        {
            List<SummaryKPI> kpisList = new List<SummaryKPI>();

            if (summaryTable != null)
            {
                #region column indexes
                int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
                int valueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_VALUE_COLUMN_NAME);
                int minValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MIN_VALUE_COLUMN_NAME);
                int avgValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_AVG_VALUE_COLUMN_NAME);
                int maxValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MAX_VALUE_COLUMN_NAME);
                
                int level1MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(level1 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level2MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(level2 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level3MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(level3 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                
                /*  // >> Task #10615 Pax2Sim - Pax analysis - Summary - small changes
                int level1MinValueColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MIN_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME 
                                        + level1 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level1AvgValueColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_AVG_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME 
                                        + level1 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level1MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MAX_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME
                                        + level1 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level2MinValueColumnIndex = summaryTable
                     .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MIN_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME
                                        + level2 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level2AvgValueColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_AVG_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME
                                        + level2 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level2MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MAX_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME
                                        + level2 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level3MinValueColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MIN_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME
                                        + level3 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level3AvgValueColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_AVG_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME
                                        + level3 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level3MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MAX_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME
                                        + level3 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                 */ 
                int unitNameColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_UNIT_COLUMN_NAME);
                #endregion

                String valueFromTable = "";
                double value = 0;

                foreach (DataRow row in summaryTable.Rows)
                {
                    SummaryKPI kpi = new SummaryKPI();
                    if (kpiNameColumnIndex != -1)
                    {
                        valueFromTable = row[kpiNameColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            kpi.name = valueFromTable;
                        }
                    }

                    if (kpisNotIncludedInDashboardList.Contains(kpi.name))
                        continue;

                    if (valueColumnIndex != -1)
                    {
                        valueFromTable = row[valueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {

                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.value = value;
                            }
                        }
                    }
                    if (minValueColumnIndex != -1)
                    {
                        valueFromTable = row[minValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.minValue = value;
                            }
                        }
                    }
                    if (avgValueColumnIndex != -1)
                    {
                        valueFromTable = row[avgValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.avgValue = value;
                            }
                        }
                    }
                    if (maxValueColumnIndex != -1)
                    {
                        valueFromTable = row[maxValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.maxValue = value;
                            }
                        }
                    }
                    #region level 1
                    if (level1MaxValueColumnIndex != -1)
                    {
                        valueFromTable = row[level1MaxValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.level1MaxValue = value;
                            }
                        }
                    }
                    #endregion

                    #region level 2
                    if (level2MaxValueColumnIndex != -1)
                    {
                        valueFromTable = row[level2MaxValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.level2MaxValue = value;
                            }
                        }
                    }
                    #endregion

                    #region level 3 
                    if (level3MaxValueColumnIndex != -1)
                    {
                        valueFromTable = row[level3MaxValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.level3MaxValue = value;
                            }
                        }
                    }
                    #endregion

                    if (unitNameColumnIndex != -1)
                    {
                        valueFromTable = row[unitNameColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            kpi.unitName = valueFromTable;
                        }
                    }

                    setUpKPIType(kpi);

                    kpisList.Add(kpi);
                }
            }

            if (kpisList != null)
            {
                setUpKPIDescription(kpisList);
            }

            return kpisList;
        }

        private static void setUpKPIDescription(List<SummaryKPI> summaryKPIsList)
        {
            foreach (SummaryKPI summaryKPI in summaryKPIsList)
            {
                if (summaryKPI != null && summaryKPI.name != "")
                {
                    if (summaryKPI.value != -1)
                    {
                        switch (summaryKPI.name)
                        {
                            case GlobalNames.SUMMARY_KPI_NAME_TOTAL_PAX_NB:
                                {
                                    summaryKPI.description = "A number of " + summaryKPI.value + " passengers entered this Station.";
                                    break;
                                }
                            case GlobalNames.SUMMARY_KPI_NAME_TOTAL_PROCESSED_PAX_NB:
                                {
                                    summaryKPI.description = "A number of " + summaryKPI.value + " passengers were processed at this Station.";
                                    break;
                                }
                            case GlobalNames.SUMMARY_KPI_NAME_TRANSFER_PAX_NB:
                                {
                                    summaryKPI.description = "Out of the total number of passengers that entered this Station " + summaryKPI.value + " were transfer passengers.";
                                    break;
                                }
                            case GlobalNames.SUMMARY_KPI_NAME_MISSED_PAX_NB:
                                {
                                    summaryKPI.description = "Out of the total number of passengers that entered this Station " + summaryKPI.value + " passengers missed their flight.";
                                    break;
                                }
                            case GlobalNames.SUMMARY_KPI_NAME_STOPPED_PAX_NB:
                                {
                                    summaryKPI.description = "Out of the total number of passengers that entered this Station " + summaryKPI.value
                                                    + " passengers were stopped before reaching their destination because the simulation stopped.";
                                    break;
                                }
                            case GlobalNames.SUMMARY_KPI_NAME_LOST_PAX_NB:
                                {
                                    summaryKPI.description = "Out of the total number of passengers that entered this Station " + summaryKPI.value
                                                    + " passengers  were lost because they could not reach the next Station.";
                                    break;
                                }
                            case GlobalNames.SUMMARY_KPI_NAME_TOTAL_OPENING_TIME:
                                {
                                    summaryKPI.description = "The Station was opened for " + summaryKPI.value + " minutes.";
                                    break;
                                }
                            case GlobalNames.SUMMARY_KPI_NAME_TOTAL_PROCESS_TIME:
                                {
                                    summaryKPI.description = "The total time needed to process the passengers that entered this Station was " + summaryKPI.value + " minutes.";
                                    break;
                                }
                            case GlobalNames.SUMMARY_KPI_NAME_RATE:
                                {
                                    summaryKPI.description = "The rate gives information about how much time the Station was used out of the total time it was opened."
                                                    + "\nThe rate is " + summaryKPI.value + " and it is calculated as follows:"
                                                    + " the total process time / the total opening time.";
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                    else if (summaryKPI.minValue != -1 && summaryKPI.avgValue != -1 && summaryKPI.maxValue != -1) 
                    {
                        if (summaryKPI.name.Equals(GlobalNames.SUMMARY_KPI_NAME_AREA_OCCUPATION))
                        {
                            summaryKPI.description = "The minimum area occupation is " + summaryKPI.minValue + "."
                                                    + "\nThe average area occupation is " + summaryKPI.avgValue + "."
                                                    + "\nThe maximum area occupation is " + summaryKPI.maxValue + ".";
                        }
                    }
                }                
            }
        }

        private static void setUpKPIType(SummaryKPI summaryKPI)
        {
            String kpiName = summaryKPI.name;
            if (summaryKPI.realName != "")
            {
                kpiName = summaryKPI.realName;
            }

            switch (kpiName)
            {
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_PAX_NB:
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_PROCESSED_PAX_NB:
                case GlobalNames.SUMMARY_KPI_NAME_TRANSFER_PAX_NB:
                case GlobalNames.SUMMARY_KPI_NAME_MISSED_PAX_NB:
                case GlobalNames.SUMMARY_KPI_NAME_STOPPED_PAX_NB:
                case GlobalNames.SUMMARY_KPI_NAME_LOST_PAX_NB:
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_OPENING_TIME:
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_PROCESS_TIME:
                case GlobalNames.SUMMARY_KPI_NAME_RATE:
                case GlobalNames.BHS_KPI_NAME_NB_BAGS:
                case GlobalNames.BHS_KPI_NAME_NB_STOPPED_BAGS:
                case GlobalNames.BHS_KPI_NAME_NB_STOPPED_BAGS_AFTER_STATION:
                    {
                        summaryKPI.type = GlobalNames.SUMMARY_KPI_TYPE_TEXT;
                        break;
                    }
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_TIME:
                case GlobalNames.SUMMARY_KPI_NAME_WAITING:
                case GlobalNames.SUMMARY_KPI_NAME_DELAY_TIME:
                case GlobalNames.SUMMARY_KPI_NAME_WAITING_DESK:
                case GlobalNames.SUMMARY_KPI_NAME_WAITING_GROUP:
                case GlobalNames.SUMMARY_KPI_NAME_PROCESS:     //same as GlobalNames.BHS_KPI_NAME_PROCESS_TIME):
                case GlobalNames.SUMMARY_KPI_NAME_REMAINING_TIME:                
                case GlobalNames.BHS_KPI_NAME_TRAVEL_TIME_TO_NEXT_STATION:
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_WALKING_TIME:
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_WAITING_TIME:
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_PROCESS_TIME_PAX:
                case GlobalNames.SUMMARY_KPI_NAME_TOTAL_TIME_TO_BG:
                    {
                        summaryKPI.type = GlobalNames.SUMMARY_KPI_TYPE_CHART_WITH_TOTAL_VALUE_WITH_PERCENTILE;
                        break;
                    }
                case GlobalNames.SUMMARY_KPI_NAME_DWELL_AREA:       //same as GlobalNames.BHS_KPI_NAME_DWELL_AREA_TIME
                    {
                        summaryKPI.type = GlobalNames.SUMMARY_KPI_TYPE_CHART_WITH_TOTAL_VALUE_NO_PERCENTILE;
                        break;
                    }
                case GlobalNames.SUMMARY_KPI_NAME_THROUGHPUT_NB_PAX_IN:
                case GlobalNames.SUMMARY_KPI_NAME_THROUGHPUT_NB_PAX_OUT:
                case GlobalNames.SUMMARY_KPI_NAME_UTILIZATION_PERCENT:
                case GlobalNames.SUMMARY_KPI_NAME_DESK_NEED:
                case GlobalNames.BHS_KPI_NAME_INPUT_BAGS:
                case GlobalNames.BHS_KPI_NAME_OUTPUT_BAGS:
                case GlobalNames.BHS_KPI_NAME_QUEUE_EXIT_INPUT_BAGS:        // >> Task #17222 PAX2SIM - FromTo Times statistics - global summaries
                case GlobalNames.BHS_KPI_NAME_QUEUE_EXIT_OUTPUT_BAGS:                
                case GlobalNames.BHS_KPI_NAME_COLLECTOR_EXIT_INPUT_BAGS:
                case GlobalNames.BHS_KPI_NAME_COLLECTOR_EXIT_OUTPUT_BAGS:   // << Task #17222 PAX2SIM - FromTo Times statistics - global summaries
                case GlobalNames.BHS_KPI_NAME_STATION_UTILIZATION_PERCENT:
                case GlobalNames.BHS_KPI_NAME_STATION_NEED:
                    {
                        summaryKPI.type = GlobalNames.SUMMARY_KPI_TYPE_MIN_MAX_AVG_TOTAL_TEXT;
                        break;
                    }
                case GlobalNames.SUMMARY_KPI_NAME_AREA_OCCUPATION:
                case GlobalNames.SUMMARY_KPI_NAME_QUEUE_OCCUPATION:
                case GlobalNames.SUMMARY_KPI_NAME_THROUGHPUT_INPUT:     //same as GlobalNames.BHS_KPI_NAME_THROUGHPUT_INPUT
                case GlobalNames.SUMMARY_KPI_NAME_THROUGHPUT_OUTPUT:    //same as GlobalNames.BHS_KPI_NAME_THROUGHPUT_OUTPUT
                case GlobalNames.SUMMARY_KPI_NAME_THROUGHPUT_INSTANT_INPUT:     // >> Task #14902 Improve Dynamic Occupation tables (PAX & BHS) C#9 Instant Throughput KPIs
                case GlobalNames.SUMMARY_KPI_NAME_THROUGHPUT_INSTANT_OUTPUT:    // >> Task #14902 Improve Dynamic Occupation tables (PAX & BHS) C#9 Instant Throughput KPIs
                case GlobalNames.BHS_KPI_NAME_OCCUPATION:
                case GlobalNames.BHS_KPI_NAME_QUEUE_EXIT_OCCUPATION:                // >> Task #17222 PAX2SIM - FromTo Times statistics - global summaries                
                case GlobalNames.BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INPUT:
                case GlobalNames.BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_OUTPUT:
                case GlobalNames.BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INSTANT_INPUT:
                case GlobalNames.BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INSTANT_OUTPUT:
                case GlobalNames.BHS_KPI_NAME_COLLECTOR_EXIT_OCCUPATION:                
                case GlobalNames.BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INPUT:
                case GlobalNames.BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_OUTPUT:
                case GlobalNames.BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INSTANT_INPUT:
                case GlobalNames.BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INSTANT_OUTPUT: // << Task #17222 PAX2SIM - FromTo Times statistics - global summaries
                    {
                        summaryKPI.type = GlobalNames.SUMMARY_KPI_TYPE_CHART_NO_TOTAL_VALUE_NO_PERCENTILE;
                        break;
                    }
                default:
                    {
                        summaryKPI.type = getDefaultKpiType(summaryKPI);    //GlobalNames.SUMMARY_KPI_TYPE_TEXT;    // >> Task #9171 Pax2Sim - Static Analysis - EBS algorithm - Throughputs C#5
                        summaryKPI.showByDefault = true;
                        break;
                    }
            }
            // >> Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats            
            if (summaryKPI.isBHSTimesDirectoryKpi)
            {
                summaryKPI.type = GlobalNames.SUMMARY_KPI_TYPE_CHART_WITH_TOTAL_VALUE_WITH_PERCENTILE;
            }
            // << Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats
        }

        private static string getDefaultKpiType(SummaryKPI kpi)
        {
            if (kpi.minValue >= 0 && kpi.avgValue >= 0 && kpi.maxValue >= 0 && kpi.unitName != null && kpi.unitName != ""
                && kpi.value >= 0 && kpi.valueUnitName != null && kpi.valueUnitName != "")
            {
                if (kpi.level1MaxValue < 0 && kpi.level2MaxValue < 0 && kpi.level3MaxValue < 0)
                {
                    return GlobalNames.SUMMARY_KPI_TYPE_CHART_WITH_TOTAL_VALUE_NO_PERCENTILE;
                }
            }
            return GlobalNames.SUMMARY_KPI_TYPE_TEXT;
        }

        public static String serializeKPIsListForXML(List<SummaryKPI> kpisList)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer
                       = new System.Xml.Serialization.XmlSerializer(kpisList.GetType());
                using (StringWriter writer = new StringWriter())
                {
                    try
                    {
                        serializer.Serialize(writer, kpisList);
                        return writer.GetStringBuilder().ToString();
                    }
                    catch (Exception ex)
                    {
                        writer.Close();
                        writer.Dispose();
                        System.Windows.Forms.MessageBox
                            .Show("There was an error while serializing the KPIs list. " 
                                    + ex.Message + "\n" + ex.StackTrace);
                    }
                    return "";
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox
                               .Show("There was an error while serializing the KPIs list. " 
                                        + e.Message + "\n" + e.StackTrace);
                return "";
            }
        }

        // >> Task #10701 Pax2Sim - Pax analysis - saving the Dashboard configuration
        public static void saveDashboardConfigDataIntoTextFile(Dictionary<String, DashboardConfiguration> dashboardConfigDictionary,
            String projectDirectoryPath)
        {
            if (projectDirectoryPath != null && projectDirectoryPath != "\\")
            {
                String dashboardConfigFilePath = "";
                foreach (KeyValuePair<String, DashboardConfiguration> pair in dashboardConfigDictionary)
                {
                    String scenarioFromKey = pair.Key.Substring(0, pair.Key.IndexOf(GlobalNames.SCENARIO_DASHBOARD_MAP_KEY_DELIMITER));
                    dashboardConfigFilePath = projectDirectoryPath + GlobalNames.SCENARIO_OUTPUT_DIRECTORY_NAME + "\\" 
                        + scenarioFromKey + "\\" + GlobalNames.DASHBOARD_CONFIG_TEXT_FILE_NAME;

                    try
                    {
                        DashboardConfiguration dashboardConfig = pair.Value;
                        if (dashboardConfig == null)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("Error while recovering data from the dashboard configuration dictionary. The " 
                                + pair.Key + " doesn't have a valid value.(Attempting to write the data in the DashboardConfiguration.txt file)");
                        }
                        String preferenceName = dashboardConfig.preferenceInfoName;
                        String preferenceString = dashboardConfig.preferences;

                        System.IO.StreamWriter writer = new StreamWriter(dashboardConfigFilePath);
                        if (writer != null)
                        {
                            writer.WriteLine(pair.Key + GlobalNames.SEPARATOR_FOR_DASHBOARD_CONFIG_TEXT_FILE
                                + preferenceName + GlobalNames.SEPARATOR_FOR_DASHBOARD_CONFIG_TEXT_FILE
                                + preferenceString + GlobalNames.SEPARATOR_FOR_DASHBOARD_CONFIG_TEXT_FILE);

                            writer.Close();
                            writer.Dispose();
                        }
                    }
                    catch (Exception exc)
                    {
                        OverallTools.ExternFunctions
                            .PrintLogFile("Exception while saving the dashboard configuration. " + exc.Message);
                    }
                }
            }
        }

        public static void loadDashboardConfigurationFromFileIntoDictionary(String projectDirectoryPath, List<String> scenarioNames,
            Dictionary<String, DashboardConfiguration> dashboardConfigDictionary)
        {
            if (projectDirectoryPath != "\\" && dashboardConfigDictionary != null)
            {
                dashboardConfigDictionary.Clear();

                foreach (String scenarioName in scenarioNames)
                {
                    String dashboardConfigFilePath = projectDirectoryPath + GlobalNames.SCENARIO_OUTPUT_DIRECTORY_NAME + "\\"
                        + scenarioName + "\\" + GlobalNames.DASHBOARD_CONFIG_TEXT_FILE_NAME;

                    if (File.Exists(dashboardConfigFilePath))
                    {
                        try
                        {
                            System.IO.StreamReader reader = new StreamReader(dashboardConfigFilePath);
                            String configLine = "";

                            if (reader != null)
                            {
                                while ((configLine = reader.ReadLine()) != null)
                                {
                                    string dictionaryKey = "";
                                    string preferenceName = "";
                                    string preferenceString = "";
                                    string[] result = configLine.Split(';');

                                    if (result.Length >= 3)
                                    {
                                        dictionaryKey = result[0];
                                        preferenceName = result[1];
                                        preferenceString = result[2];

                                        DashboardConfiguration dashboardConfig = new DashboardConfiguration();
                                        dashboardConfig.preferenceInfoName = preferenceName;
                                        dashboardConfig.preferences = preferenceString;

                                        if (!dashboardConfigDictionary.ContainsKey(dictionaryKey))
                                        {
                                            dashboardConfigDictionary.Add(dictionaryKey, dashboardConfig);
                                        }
                                    }
                                }
                                reader.Close();
                                reader.Dispose();
                            }
                        }
                        catch (System.OutOfMemoryException oomExc)
                        {
                            OverallTools.ExternFunctions
                                .PrintLogFile("Out of memory while reading the dashboard configuration file. " + oomExc.Message);
                        }
                        catch (Exception exc)
                        {
                            OverallTools.ExternFunctions
                                .PrintLogFile("Exception while reading the dashboard configuration file. " + exc.Message);
                        }
                    }
                }
            }
        }
        // << Task #10701 Pax2Sim - Pax analysis - saving the Dashboard configuration

        // >> Task #10985 Pax2Sim - BHS dynamic analysis - adapt statistics tables for the Dashboard
        public static List<SummaryKPI> getSummaryKPIsListFromBHSSummaryTable(DataTable summaryTable,
          double level1, double level2, double level3)
        {
            List<SummaryKPI> kpisList = new List<SummaryKPI>();

            if (summaryTable != null)
            {
                #region column indexes
                int kpiIdColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_KPI_ID_COLUMN_NAME);
                int kpiNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
                int valueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_VALUE_COLUMN_NAME);
                int valueUnitNameColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.BHS_STATS_TABLE_TOTAL_VALUE_UNIT_COLUMN_NAME);
                int minValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MIN_VALUE_COLUMN_NAME);
                int avgValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_AVG_VALUE_COLUMN_NAME);
                int maxValueColumnIndex = summaryTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MAX_VALUE_COLUMN_NAME);

                int level1MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(level1 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level2MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(level2 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
                int level3MaxValueColumnIndex = summaryTable
                    .Columns.IndexOf(level3 + GlobalNames.SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX);
               
                int unitNameColumnIndex = summaryTable
                    .Columns.IndexOf(GlobalNames.SUMMARY_TABLE_UNIT_COLUMN_NAME);
                #endregion
                                
                String valueFromTable = "";
                double value = 0;
                String kpiNamePreffix = "";
                String childStationName = "";

                foreach (DataRow row in summaryTable.Rows)
                {
                    SummaryKPI kpi = new SummaryKPI();

                    if (kpiIdColumnIndex != -1)
                    {
                        int id = -1;
                        valueFromTable = row[kpiIdColumnIndex].ToString();
                        if (!Int32.TryParse(valueFromTable.ToString(), out id))
                        {
                            if (kpiNameColumnIndex != -1 && row[kpiNameColumnIndex] != null)
                            {
                                valueFromTable = row[kpiNameColumnIndex].ToString();
                                if (valueFromTable != null)
                                {
                                    kpiNamePreffix = valueFromTable + "_";
                                    childStationName = valueFromTable;
                                    continue;
                                }
                            }                    
                        }                        
                    }

                    if (kpiNameColumnIndex != -1)
                    {
                        valueFromTable = row[kpiNameColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            kpi.name = kpiNamePreffix + valueFromTable;
                            if (kpiNamePreffix != "")
                            {
                                kpi.realName = valueFromTable;
                            }
                            if (childStationName != "")
                            {
                                kpi.childStationName = childStationName;
                            }
                        }
                    }
                    if (valueColumnIndex != -1)
                    {
                        valueFromTable = row[valueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {

                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.value = value;
                            }
                        }
                    }
                    if (valueUnitNameColumnIndex != -1)
                    {
                        valueFromTable = row[valueUnitNameColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            kpi.valueUnitName = valueFromTable;
                        }
                    }
                    if (minValueColumnIndex != -1)
                    {
                        valueFromTable = row[minValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.minValue = value;
                            }
                        }
                    }
                    if (avgValueColumnIndex != -1)
                    {
                        valueFromTable = row[avgValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.avgValue = value;
                            }
                        }
                    }
                    if (maxValueColumnIndex != -1)
                    {
                        valueFromTable = row[maxValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.maxValue = value;
                            }
                        }
                    }
                    #region level 1
                    if (level1MaxValueColumnIndex != -1)
                    {
                        valueFromTable = row[level1MaxValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.level1MaxValue = value;
                            }
                        }
                    }
                    #endregion

                    #region level 2
                    if (level2MaxValueColumnIndex != -1)
                    {
                        valueFromTable = row[level2MaxValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.level2MaxValue = value;
                            }
                        }
                    }
                    #endregion

                    #region level 3
                    if (level3MaxValueColumnIndex != -1)
                    {
                        valueFromTable = row[level3MaxValueColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            if (Double.TryParse(valueFromTable, out value))
                            {
                                kpi.level3MaxValue = value;
                            }
                        }
                    }
                    #endregion

                    if (unitNameColumnIndex != -1)
                    {
                        valueFromTable = row[unitNameColumnIndex].ToString();
                        if (valueFromTable != null)
                        {
                            kpi.unitName = valueFromTable;
                        }
                    }
                    
                    kpi.isBHSkpi = true;
                    kpi.isBHSTimesDirectoryKpi = false;// isBHSTimesDirectoryTable(summaryTable);    // >> Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats
                    kpi.isVisibleByDefault = isVisibleByDefault(kpi.name);  // >> Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats
                    kpi.showByDefault = showByDefault(kpi.name);

                    setUpKPIType(kpi);

                    kpisList.Add(kpi);
                }
            }
            if (kpisList != null)
            {
                setUpKPIDescription(kpisList);
            }
            return kpisList;
        }

        private static bool showByDefault(string summaryKPIName)
        {
            if (GlobalNames.BHS_SUMMARY_VISIBLE_BY_DEFAULT_KPIS.Contains(summaryKPIName))    // >> Task #17222 PAX2SIM - FromTo Times statistics - global summaries
            {
                return true;
            }
            return false;
        }
        // << Task #10985 Pax2Sim - BHS dynamic analysis - adapt statistics tables for the Dashboard
        
        // >> Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats        
        private static bool isBHSTimesDirectoryTable(DataTable summaryTable)
        {
            if (summaryTable != null
                && (summaryTable.TableName.Contains(OverallTools.BagTraceAnalysis.TIMES_DIRECTORY_STATISTIC_TABLE_SUFFIX) 
                    || summaryTable.TableName.EndsWith(GlobalNames.BHS_STATS_TABLE_SUFFIX)))
            {
                return false; //return true;
            }
            return false;
        }

        private static bool isVisibleByDefault(String summaryKPIName)
        {
            if (summaryKPIName.EndsWith(KPI_VISIBLE_BY_DEFAULT_GLOBAL_SUFFIX) 
                || summaryKPIName.EndsWith(KPI_VISIBLE_BY_DEFAULT_STANDARD_BAG_SUFFIX)
                || GlobalNames.SUMMARY_PAX_DISTRIBUTION_TABLE_KPI_NAMES.Contains(summaryKPIName))                
            {
                return true;
            }
            return false;
        }
        // << Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats

        internal static bool tableHasDashboardStructure(DataTable sourceTable)  // >> Task #16728 PAX2SIM Improvements (Recurring) C#30 Bug.2.
        {
            if (sourceTable == null || sourceTable.Columns == null)
            {
                return false;
            }
            List<int> columnIndexes = new List<int>();
            int kpiIdColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_KPI_ID_COLUMN_NAME);
            columnIndexes.Add(kpiIdColumnIndex);
            int kpiNameColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_DATA_COLUMN_NAME);
            columnIndexes.Add(kpiNameColumnIndex);
            int valueColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_VALUE_COLUMN_NAME);
            columnIndexes.Add(valueColumnIndex);
            int minValueColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MIN_VALUE_COLUMN_NAME);
            columnIndexes.Add(minValueColumnIndex);
            int avgValueColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_AVG_VALUE_COLUMN_NAME);
            columnIndexes.Add(avgValueColumnIndex);
            int maxValueColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_MAX_VALUE_COLUMN_NAME);
            columnIndexes.Add(maxValueColumnIndex);
            int unitNameColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.SUMMARY_TABLE_UNIT_COLUMN_NAME);
            columnIndexes.Add(unitNameColumnIndex);

            foreach (int index in columnIndexes)
            {
                if (index == -1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
