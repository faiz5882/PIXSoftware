using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIMCORE_TOOL.Prompt.Liege;
using SIMCORE_TOOL.com.crispico.general_allocation.model;

namespace SIMCORE_TOOL.com.crispico.adapters
{
    class FlightPlanInformationProcessor
    {
        private DataTable flightPlanInformationTable;
                        
        private List<Flight> flights;        
        private Dictionary<string, Resource> resources;
        private List<Zone> zones;

        private FpiColumnIndexes fpiColumnIndexes;

        public DataTable getFpiTable()
        {
            return flightPlanInformationTable;
        }

        public FlightPlanInformationProcessor(DataTable flightPlanInformationTable)
        {
            this.flightPlanInformationTable = flightPlanInformationTable;

            fpiColumnIndexes = new FpiColumnIndexes(flightPlanInformationTable);

            processFlightPlanInformationTable();
        }
                
        public TimeInterval getFlightPlanInformationTimeInterval()
        {
            if (fpiColumnIndexes == null || !fpiColumnIndexes.isValid())
                return TimeInterval.EMPTY_TIME_INTERVAL;

            DateTime start = DateTime.MaxValue;
            DateTime end = DateTime.MinValue;
            if (flightPlanInformationTable != null)
            {
                foreach (DataRow row in flightPlanInformationTable.Rows)
                {
                    TimeInterval rowOccupation = getOccupationIntervalFromFpiRow(row);
                    if (start > rowOccupation.fromDate)
                        start = rowOccupation.fromDate;
                    if (end < rowOccupation.toDate)
                        end = rowOccupation.toDate;
                }
            }
            DateTime roundedStart = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
            DateTime roundedEnd = new DateTime(end.Year, end.Month, end.Day, end.Hour, 59, 0);
            return new TimeInterval(roundedStart, roundedEnd);
        }

        public List<Flight> getFlights()
        {
            if (flights == null)
            {
                if (!fpiColumnIndexes.isValid())
                    return flights;
                processFlightPlanInformationTable();
            }
            return flights;
        }

        public Dictionary<string, Resource> getResources()
        {
            if (resources == null)
            {
                if (!fpiColumnIndexes.isValid())
                    return resources;
                processFlightPlanInformationTable();
            }
            return resources;
        }

        public List<Zone> getZones()
        {
            if (zones == null)
            {
                if (!fpiColumnIndexes.isValid())
                    return zones;
                processFlightPlanInformationTable();
            }
            return zones;
        }

        private void processFlightPlanInformationTable()
        {
            flights = new List<Flight>();
            resources = new Dictionary<string, Resource>();
            zones = new List<Zone>();
            //var keshaw = flightPlanInformationTable.Rows;
            if (flightPlanInformationTable != null)
            {

                foreach (DataRow row in flightPlanInformationTable.Rows)
                {
                    Flight flight = getFlightFromFpiRow(row);
                    flights.Add(flight);
                    TimeInterval occupationInterval = getOccupationIntervalFromFpiRow(row);
                    if (flight == null || occupationInterval == TimeInterval.EMPTY_TIME_INTERVAL)
                        continue;

                    flight.occupiedInterval = occupationInterval;

                    List<Resource> resourcesFromFpiRow = getResourcesFromFpiRow(row);
                    foreach (Resource resource in resourcesFromFpiRow)
                    {
                        flight.allocatedResources.Add(resource);
                        FlightAllocation allocation = new FlightAllocation(flight, resource, occupationInterval);
                        resource.flightAllocations.Add(allocation);
                    }
                }
            }
            zones = getZonesByResourcesTerminalNb(resources);
        }

        private Flight getFlightFromFpiRow(DataRow row)
        {
            if (fpiColumnIndexes == null || !fpiColumnIndexes.isValid())
                return null;
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
            if (fpiColumnIndexes == null || !fpiColumnIndexes.isValid())
                return TimeInterval.EMPTY_TIME_INTERVAL;

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
            List<Resource> resourcesFromFpiRow = new List<Resource>();
            if (fpiColumnIndexes == null || !fpiColumnIndexes.isValid())
                return resourcesFromFpiRow;
            
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
            if (fpiColumnIndexes == null || !fpiColumnIndexes.isValid())
                return zones;

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
