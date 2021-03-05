using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIMCORE_TOOL.Prompt.Liege;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.com.crispico.general_allocation.model;
using SIMCORE_TOOL.com.crispico.text_gantt;

namespace SIMCORE_TOOL.com.crispico.adapters
{
    class FlightPlanInformationToTextGantt
    {
        public const string TEXT_GANTT_COLUMN_NAME_LEGEND = "Legend";

        private const string TEXT_GANTT_TABLE_NAME_SUFFIX = "_TextGantt";
                
        private DataTable flightPlanInformationTable;
        private DataTable textGanttTable;
        
        private FpiColumnIndexes fpiColumnIndexes;

        private List<Zone> zones;
        private Dictionary<string, Resource> resources;
        private List<Flight> flights;

        private List<string> warnings;

        private FlightPlanInformationProcessor fpiProcessor;
        private TextGanttParameters textGanttParameters;        

        
        public static bool isFPITextGanttTable(string tableName)
        {
            if (tableName == null)
                return false;
            if (tableName.Contains(GlobalNames.FPI_TableName) && tableName.Contains(TEXT_GANTT_TABLE_NAME_SUFFIX))
                return true;
            return false;
        }

        public static string getTextGanttName(DataTable parentFlightPlanInformationTable)
        {
            if (parentFlightPlanInformationTable == null)
                return string.Empty;
            return parentFlightPlanInformationTable.TableName + TEXT_GANTT_TABLE_NAME_SUFFIX;
        }

/*
        public DataTable getFpiTable()
        {
            return flightPlanInformationTable;
        }*/

        /*
        public FlightPlanInformationToTextGantt(TextGanttParameters textGanttParameters, FlightPlanInformationProcessor fpiProcessor, DataTable flightPlanInformationTable)
        {
            this.fpiProcessor = fpiProcessor;
            this.textGanttParameters = textGanttParameters;
            this.flightPlanInformationTable = flightPlanInformationTable;

            resources = new Dictionary<string, Resource>();
            zones = new List<Zone>();
            warnings = new List<string>();

            fpiColumnIndexes = new FpiColumnIndexes(flightPlanInformationTable);
        }*/

        public FlightPlanInformationToTextGantt(TextGanttParameters textGanttParameters, FlightPlanInformationProcessor fpiProcessor)
        {
            this.fpiProcessor = fpiProcessor;
            this.textGanttParameters = textGanttParameters;
        }

        public List<Flight> getFilteredFlights()
        {
            List<Flight> filteredFlights = new List<Flight>();
            if (textGanttParameters == null || fpiProcessor.getFlights() == null)
                return filteredFlights;
            foreach (Flight flight in fpiProcessor.getFlights())
            {
                if (textGanttParameters.getTimeInterval().containsTimeInterval(flight.occupiedInterval))
                    filteredFlights.Add(flight);
            }
            return filteredFlights;
        }

        /*
        public DataTable _getTextGanttTable()
        {
            if (!fpiColumnIndexes.isValid())
                return textGanttTable;
            if (textGanttTable == null)
            {
                processFlightPlanInformationTable();
                createTextGanttTableStructure();
                fillTextGanttTable();
                updateTextGanttStatisticColumns();
            }
            return textGanttTable;
        }
        */

        public DataTable getTextGanttTable()
        {
            if (fpiProcessor == null)
                return textGanttTable;
            if (textGanttTable == null)
            {
                createTextGanttTableStructure();
                fillTextGanttTable();
                updateTextGanttStatisticColumns();
            }
            return textGanttTable;
        }
        /*
        public List<Flight> getFlights()
        {
            if (flights == null)
            {
                if (textGanttParameters == null || !fpiColumnIndexes.isValid())
                    return flights;
                processFlightPlanInformationTable();
            }
            return flights;
        }*/
        /*
        private void processFlightPlanInformationTable()
        {
            flights = new List<Flight>();
            foreach (DataRow row in flightPlanInformationTable.Rows)
            {
                Flight flight = getFlightFromFpiRow(row);
                flights.Add(flight);
                TimeInterval occupationInterval = getOccupationIntervalFromFpiRow(row);
                if (flight == null || occupationInterval == TimeInterval.EMPTY_TIME_INTERVAL
                    || !occupationInterval.intersectsTimeInterval(textGanttParameters.getTimeInterval()))
                {
                    continue;
                }
                List<Resource> resourcesFromFpiRow = getResourcesFromFpiRow(row);
                foreach (Resource resource in resourcesFromFpiRow)
                {
                    flight.allocatedResources.Add(resource);
                    FlightAllocation allocation = new FlightAllocation(flight, resource, occupationInterval);
                    resource.flightAllocations.Add(allocation);
                }
            }
            zones = getZonesByResourcesTerminalNb(resources);
        }*/
        /*
        private Flight getFlightFromFpiRow(DataRow row)
        {
            if (row[fpiColumnIndexes.flightIdColumn] == null || row[fpiColumnIndexes.flightNbColumn] == null)
                return null;
            int flightId = -1;
            if (!Int32.TryParse(row[fpiColumnIndexes.flightIdColumn].ToString(), out flightId))
                return null;
            Flight flight = new Flight();
            flight.id = flightId;
            flight.flightNumber = row[fpiColumnIndexes.flightNbColumn].ToString();
            if (row[fpiColumnIndexes.airlineColumn] != null)
                flight.airlineCode = row[fpiColumnIndexes.airlineColumn].ToString();
            if (row[fpiColumnIndexes.flightCategoryColumn] != null)
                flight.flightCategory = row[fpiColumnIndexes.flightCategoryColumn].ToString();
            if (row[fpiColumnIndexes.groundHandlerColumn] != null)
                flight.specificGroundHandler = row[fpiColumnIndexes.groundHandlerColumn].ToString();
            return flight;
        }

        private TimeInterval getOccupationIntervalFromFpiRow(DataRow row)
        {
            if (row[fpiColumnIndexes.openingIntervalColumn] == null || row[fpiColumnIndexes.closingIntervalColumn] == null)
                return TimeInterval.EMPTY_TIME_INTERVAL;
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MinValue;
            if (!DateTime.TryParse(row[fpiColumnIndexes.openingIntervalColumn].ToString(), out start)
                || !DateTime.TryParse(row[fpiColumnIndexes.closingIntervalColumn].ToString(), out end))
            {
                return TimeInterval.EMPTY_TIME_INTERVAL;
            }
            return new TimeInterval(start, end);
        }

        private List<Resource> getResourcesFromFpiRow(DataRow row)
        {
            List<Resource>  resourcesFromFpiRow = new List<Resource>();
            if (row[fpiColumnIndexes.firstDeskColumn] == null || row[fpiColumnIndexes.lastDeskColumn] == null
                || row[fpiColumnIndexes.resourceTypeColumn] == null || row[fpiColumnIndexes.terminalNbColumn] == null)
            {
                return resourcesFromFpiRow;
            }
            int firstDeskNb = -1;
            int lastDeskNb = -1;
            int terminalNb = -1;
            if (!Int32.TryParse(row[fpiColumnIndexes.firstDeskColumn].ToString(), out firstDeskNb)
                || !Int32.TryParse(row[fpiColumnIndexes.lastDeskColumn].ToString(), out lastDeskNb)
                || !Int32.TryParse(row[fpiColumnIndexes.terminalNbColumn].ToString(), out terminalNb))
            {
                return resourcesFromFpiRow;
            }
            string resourceType = row[fpiColumnIndexes.resourceTypeColumn].ToString();
            for (int i = firstDeskNb; i <= lastDeskNb; i++)
            {
                Resource potentialResource = new Resource(i, resourceType, terminalNb);
                if (!resources.ContainsKey(potentialResource.name))
                    resources.Add(potentialResource.name, potentialResource);
                resourcesFromFpiRow.Add(resources[potentialResource.name]);
            }
            return resourcesFromFpiRow;
        }

        private List<Zone> getZonesByResourcesTerminalNb(Dictionary<string, Resource> resources)
        {
            List<Zone> zones = new List<Zone>();
            foreach (Resource resource in resources.Values)
            {
                Zone zone = new Zone(resource.terminalNb);

                if (!zones.Contains(zone))
                {
                    zone.getResources().Add(resource);
                    resource.zone = zone;

                    zones.Add(zone);
                }
                else
                {
                    zones[zones.IndexOf(zone)].getResources().Add(resource);
                    resource.zone = zones[zones.IndexOf(zone)];
                }
            }
            return zones;
        }

        private DataTable _createTextGanttTableStructure()
        {
            string textGanttTableName = getTextGanttName(flightPlanInformationTable);
            if (textGanttTableName.Contains(PAX2SIM.CUSTOM_FPI_GANTT_MARKER))
                textGanttTableName = textGanttTableName.Replace(PAX2SIM.CUSTOM_FPI_GANTT_MARKER, "");
            textGanttTable = new DataTable(textGanttTableName);
                        
            textGanttTable.Columns.Add(GlobalNames.sColumnTime, typeof(DateTime));
            textGanttTable.Columns.Add(TEXT_GANTT_COLUMN_NAME_LEGEND, typeof(String));
            textGanttTable.Columns.Add(AllocationOutput.ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME, typeof(Int32));
            textGanttTable.Columns.Add(AllocationOutput.ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME, typeof(Int32));
            textGanttTable.Columns.Add(AllocationOutput.ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME, typeof(Int32));
            
            List<Resource> orderedResources = resources.Values.OrderBy(r => r.terminalNb).ThenBy(r => r.id).ToList();
            for (int i = 0; i < orderedResources.Count; i++)
            {
                Resource resource = orderedResources[i];
                if (i == 0)
                {
                    textGanttTable.Columns.Add(getNeededColumnNameByZone(resource.zone), typeof(Int32));
                    textGanttTable.Columns.Add(getOccupiedColumnNameByZone(resource.zone), typeof(Int32));
                    textGanttTable.Columns.Add(getNbFlightsColumnNameByZone(resource.zone), typeof(Int32));
                }
                else
                {
                    Resource previousResource = orderedResources[i - 1];
                    if (!resource.zone.Equals(previousResource.zone))
                    {
                        textGanttTable.Columns.Add(getNeededColumnNameByZone(resource.zone), typeof(Int32));
                        textGanttTable.Columns.Add(getOccupiedColumnNameByZone(resource.zone), typeof(Int32));
                        textGanttTable.Columns.Add(getNbFlightsColumnNameByZone(resource.zone), typeof(Int32));
                    }
                }
                textGanttTable.Columns.Add(resource.name, typeof(String));
            }
            OverallTools.DataFunctions.initialiserLignes(textGanttTable, 
                textGanttParameters.getTimeInterval().fromDate, textGanttParameters.getTimeInterval().toDate, textGanttParameters.getSamplingStep());
            textGanttTable.AcceptChanges();
            return textGanttTable;
        }
        */
        private DataTable createTextGanttTableStructure()
        {
            string textGanttTableName = getTextGanttName(fpiProcessor.getFpiTable());
            if (textGanttTableName.Contains(PAX2SIM.CUSTOM_FPI_GANTT_MARKER))
                textGanttTableName = textGanttTableName.Replace(PAX2SIM.CUSTOM_FPI_GANTT_MARKER, "");
            textGanttTable = new DataTable(textGanttTableName);

            textGanttTable.Columns.Add(GlobalNames.sColumnTime, typeof(DateTime));
            textGanttTable.Columns.Add(TEXT_GANTT_COLUMN_NAME_LEGEND, typeof(String));
            textGanttTable.Columns.Add(AllocationOutput.ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME, typeof(Int32));
            textGanttTable.Columns.Add(AllocationOutput.ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME, typeof(Int32));
            textGanttTable.Columns.Add(AllocationOutput.ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME, typeof(Int32));
            
            List<Resource> orderedResources = fpiProcessor.getResources().Values.OrderBy(r => r.terminalNb).ThenBy(r => r.id).ToList();
            for (int i = 0; i < orderedResources.Count; i++)
            {
                Resource resource = orderedResources[i];
                if (i == 0)
                {
                    textGanttTable.Columns.Add(getNeededColumnNameByZone(resource.zone), typeof(Int32));
                    textGanttTable.Columns.Add(getOccupiedColumnNameByZone(resource.zone), typeof(Int32));
                    textGanttTable.Columns.Add(getNbFlightsColumnNameByZone(resource.zone), typeof(Int32));
                }
                else
                {
                    Resource previousResource = orderedResources[i - 1];
                    if (!resource.zone.Equals(previousResource.zone))
                    {
                        textGanttTable.Columns.Add(getNeededColumnNameByZone(resource.zone), typeof(Int32));
                        textGanttTable.Columns.Add(getOccupiedColumnNameByZone(resource.zone), typeof(Int32));
                        textGanttTable.Columns.Add(getNbFlightsColumnNameByZone(resource.zone), typeof(Int32));
                    }
                }
                textGanttTable.Columns.Add(resource.name, typeof(String));
            }
            OverallTools.DataFunctions.initialiserLignes(textGanttTable,
                textGanttParameters.getTimeInterval().fromDate, textGanttParameters.getTimeInterval().toDate, textGanttParameters.getSamplingStep());
            textGanttTable.AcceptChanges();
            return textGanttTable;
        }
                
        private string getNeededColumnNameByZone(Zone zone)
        {
            return zone.nameByTerminal + " Needed";
        }
        private string getOccupiedColumnNameByZone(Zone zone)
        {
            return zone.nameByTerminal + " Occupied";
        }
        private string getNbFlightsColumnNameByZone(Zone zone)
        {
            return zone.nameByTerminal + " Nb Flights";
        }
        /*
        public TimeInterval getFlightPlanInformationTimeInterval()
        {
            if (fpiColumnIndexes == null || !fpiColumnIndexes.isValid())
                return TimeInterval.EMPTY_TIME_INTERVAL;

            DateTime start = DateTime.MaxValue;
            DateTime end = DateTime.MinValue;
            foreach (DataRow row in flightPlanInformationTable.Rows)
            {
                TimeInterval rowOccupation = getOccupationIntervalFromFpiRow(row);
                if (start > rowOccupation.fromDate)
                    start = rowOccupation.fromDate;
                if (end < rowOccupation.toDate)
                    end = rowOccupation.toDate;
            }
            DateTime roundedStart = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
            DateTime roundedEnd = new DateTime(end.Year, end.Month, end.Day, end.Hour, 59, 0);
            return new TimeInterval(roundedStart, roundedEnd);
        }*/

        private void fillTextGanttTable()
        {
            foreach (Resource resource in fpiProcessor.getResources().Values)
            {
                foreach (FlightAllocation flightAllocation in resource.flightAllocations)
                {
                    if (!textGanttParameters.getTimeInterval().containsTimeInterval(flightAllocation.occupationInterval))
                        continue;
                    string flightTextInGantt = getFlightTextInGantt(flightAllocation.allocatedFlight);
                    AllocationOutput.addFlightToTextGantt(flightTextInGantt, flightAllocation.occupationInterval, resource.name, textGanttTable,
                        textGanttParameters.getSamplingStep(), textGanttParameters.getTimeInterval().fromDate);
                }
            }
        }

        public static string getFlightTextInGantt(Flight flight)
        {
            if (flight == null)
                return string.Empty;
            return flight.id + "_" + flight.flightNumber;
        }

        public const int NO_ID = int.MinValue;
        public static int getFlightIdFromFlightTextInGantt(string flightTextInGantt)
        {
            if (flightTextInGantt == null || !flightTextInGantt.Contains("_"))
                return NO_ID;
            string[] components = flightTextInGantt.Split('_');
            string idAsString = components[0];
            int id = -1;
            if (!Int32.TryParse(idAsString, out id))
                return NO_ID;
            return id;
        }

        public static string getFlightNbFromFlightTextInGantt(string flightTextInGantt)
        {
            string flightText = flightTextInGantt;
            if (flightTextInGantt.Contains(","))
            {
                string[] flights = flightTextInGantt.Split(',');
                flightText = flights[0];
            }
            if (flightText == null || !flightText.Contains("_") || flightText.IndexOf("_") + 1 >= flightText.Length)
                return string.Empty;
            return flightText.Substring(flightText.IndexOf("_") + 1);
        }

        private void updateTextGanttStatisticColumns()
        {
            updateTextGanttZoneStatisticColumns(zones);
            updateTextGanttGlobalStatisticColumns(zones);
        }

        private void updateTextGanttZoneStatisticColumns(List<Zone> zones)
        {
            if (textGanttTable == null)
                return;
            foreach (Zone zone in fpiProcessor.getZones())
            {
                int neededColumnIndex = textGanttTable.Columns.IndexOf(getNeededColumnNameByZone(zone));
                int occupiedColumnIndex = textGanttTable.Columns.IndexOf(getOccupiedColumnNameByZone(zone));
                int nbFlightsColumnIndex = textGanttTable.Columns.IndexOf(getNbFlightsColumnNameByZone(zone));
                if (neededColumnIndex == -1 || occupiedColumnIndex == -1 || nbFlightsColumnIndex == -1)
                    continue;
                List<string> targetColumns = getTargetColumnsByZone(textGanttTable, zone);                
                foreach (DataRow row in textGanttTable.Rows)
                {                    
                    row[neededColumnIndex] = getNeededByTextGanttRow(row, targetColumns);                    
                    row[occupiedColumnIndex] = getOccupiedByTextGanttRow(row, targetColumns);
                    row[nbFlightsColumnIndex] = getNbFlightsByTextGanttRow(row, targetColumns);
                }
            }
        }

        private void updateTextGanttGlobalStatisticColumns(List<Zone> allZones)
        {
            if (textGanttTable == null)
                return;
            int neededColumnIndex = textGanttTable.Columns.IndexOf(AllocationOutput.ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME);
            int occupiedColumnIndex = textGanttTable.Columns.IndexOf(AllocationOutput.ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME);
            int nbFlightsColumnIndex = textGanttTable.Columns.IndexOf(AllocationOutput.ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME);
            if (neededColumnIndex == -1 || occupiedColumnIndex == -1 || nbFlightsColumnIndex == -1)
                return;
            foreach (DataRow row in textGanttTable.Rows)
            {
                row[neededColumnIndex] = getSumOfZonesFromRowForGlobalColumn(AllocationOutput.ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME, row, allZones);
                row[occupiedColumnIndex] = getSumOfZonesFromRowForGlobalColumn(AllocationOutput.ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME, row, allZones);
                row[nbFlightsColumnIndex] = getSumOfZonesFromRowForGlobalColumn(AllocationOutput.ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME, row, allZones);
            }
        }

        private int getSumOfZonesFromRowForGlobalColumn(string globalColumn, DataRow row, List<Zone> allZones)
        {
            int sum = 0;
            foreach (Zone zone in fpiProcessor.getZones())
            {
                int textGanttColumnIndex = -1;
                if (globalColumn == AllocationOutput.ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME)
                    textGanttColumnIndex = row.Table.Columns.IndexOf(getNeededColumnNameByZone(zone));
                else if (globalColumn == AllocationOutput.ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME)
                    textGanttColumnIndex = row.Table.Columns.IndexOf(getOccupiedColumnNameByZone(zone));
                else if (globalColumn == AllocationOutput.ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME)
                    textGanttColumnIndex = row.Table.Columns.IndexOf(getNbFlightsColumnNameByZone(zone));
                
                int valueByZone = 0;
                if (textGanttColumnIndex != -1 && row[textGanttColumnIndex] != null && Int32.TryParse(row[textGanttColumnIndex].ToString(), out valueByZone))
                    sum += valueByZone;
            }
            return sum;
        }

        private List<string> getTargetColumnsByZone(DataTable textGanttTable, Zone zone)
        {
            List<string> targetColumns = new List<string>();
            foreach (DataColumn column in textGanttTable.Columns)
            {
                if (columnBelongsToZone(column.ColumnName, zone.getResources()))
                    targetColumns.Add(column.ColumnName);
            }
            return targetColumns;
        }

        private bool columnBelongsToZone(string columnName, List<Resource> resourcesByZone)
        {
            foreach (Resource resource in resourcesByZone)
            {
                if (columnName.Equals(resource.name))
                    return true;
            }
            return false;
        }

        private int getNeededByTextGanttRow(DataRow row, List<string> targetColumns)
        {
            int nbNeeded = 0;
            int indexOfLastNonEmptyColumn = -1;
            for (int i = 0; i < targetColumns.Count; i++)
            {
                string columnName = targetColumns[i];
                if (row.Table.Columns.IndexOf(columnName) != -1 && row[columnName] != null && row[columnName].ToString() != string.Empty)
                    indexOfLastNonEmptyColumn = i;
            }
            if (indexOfLastNonEmptyColumn != -1)
                nbNeeded = indexOfLastNonEmptyColumn + 1;
            return nbNeeded;
        }
        
        private int getOccupiedByTextGanttRow(DataRow row, List<string> targetColumns)
        {
            int nbOccupied = 0;
            foreach (string columnName in targetColumns)
            {
                if (row.Table.Columns.IndexOf(columnName) != -1 && row[columnName] != null && row[columnName].ToString() != string.Empty)
                    nbOccupied++;
            }
            return nbOccupied;
        }

        private object getNbFlightsByTextGanttRow(DataRow row, List<string> targetColumns)
        {
            int nbFlights = 0;
            foreach (string columnName in targetColumns)
            {
                if (row.Table.Columns.IndexOf(columnName) != -1 && row[columnName] != null && row[columnName].ToString() != string.Empty)
                {
                    string content = row[columnName].ToString();
                    if (content.Contains(","))
                        nbFlights += content.Split(',').Length;
                    else
                        nbFlights++;
                }
            }
            return nbFlights;
        }

        private class FpiColumnIndexes
        {
            public int flightIdColumn { get; set; }
            public int flightNbColumn { get; set; }
            public int airlineColumn { get; set; }
            public int flightCategoryColumn { get; set; }
            public int groundHandlerColumn { get; set; }
            public int resourceTypeColumn { get; set; }
            public int firstDeskColumn { get; set; }
            public int lastDeskColumn { get; set; }
            public int openingIntervalColumn { get; set; }
            public int closingIntervalColumn { get; set; }
            public int terminalNbColumn { get; set; }

            private DataTable fpiTable;

            private readonly List<int> indexes;
                        
            public FpiColumnIndexes(DataTable fpiTable)
            {
                this.fpiTable = fpiTable;

                indexes = new List<int>(new int[] { flightIdColumn, flightNbColumn, airlineColumn, flightCategoryColumn, groundHandlerColumn, resourceTypeColumn, 
                    firstDeskColumn, lastDeskColumn, openingIntervalColumn, closingIntervalColumn, terminalNbColumn });

                if (fpiTable != null)
                    updateColumnIndexes();
            }

            public bool isValid()
            {
                foreach (int index in indexes)
                {
                    if (index == -1)
                        return false;
                }
                return true;
            }

            private void updateColumnIndexes()
            {
                flightIdColumn = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                flightNbColumn = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                airlineColumn = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                flightCategoryColumn = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                groundHandlerColumn = fpiTable.Columns.IndexOf(GlobalNames.sFPAirline_GroundHandlers);
                resourceTypeColumn = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_Resource_Type_Name);

                firstDeskColumn = getIndexOfColumnBySuffix(GlobalNames.FPI_FIRST_DESK_COLUMN_SUFFIX);
                lastDeskColumn = getIndexOfColumnBySuffix(GlobalNames.FPI_LAST_DESK_COLUMN_SUFFIX);
                openingIntervalColumn = getIndexOfColumnBySuffix(GlobalNames.FPI_OPENING_TIME_COLUMN_SUFFIX);
                closingIntervalColumn = getIndexOfColumnBySuffix(GlobalNames.FPI_CLOSING_TIME_COLUMN_SUFFIX);
                terminalNbColumn = getIndexOfColumnBySuffix(GlobalNames.FPI_TERMINAL_NB_COLUMN_SUFFIX);
            }

            private int getIndexOfColumnBySuffix(string suffix)
            {
                int result = -1;
                if (fpiTable != null)
                {
                    for (int i = 0; i < fpiTable.Columns.Count; i++)
                    {
                        DataColumn column = fpiTable.Columns[i];
                        if (column.ColumnName.EndsWith(suffix))
                        {
                            result = i;
                            break;
                        }
                    }
                }
                return result;
            }
            
        }

    }
}
