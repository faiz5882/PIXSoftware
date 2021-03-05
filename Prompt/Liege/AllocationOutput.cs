using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIMCORE_TOOL.DataManagement;
using System.Drawing;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class AllocationOutput
    {
        internal const string AVAILABLE_RESOURCES_TABLE_NAME = "Available Resources";
        internal const string ALLOCATION_RESULT_TABLE_NAME = "Allocation Result";
        internal const string ALLOCATION_TEXT_GANTT_TABLE_NAME = "Allocation Text Gantt";
        internal const string ALLOCATION_ISSUES_TABLE_NAME = "Allocation Issues";
        internal const string OCCUPATION_ISSUES_TABLE_NAME = "Occupation Issues";

        internal const string RESOURCE_CLOSED_IN_INTERVAL_MARKER = "CLOSED";

        internal const string ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME = "Global Needed";
        internal const string ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME = "Global Occupied";
        internal const string ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME = "Global Number of Flights";

        internal const string ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NO_SPACE_ON_FP_RESOURCE = "Unallocated Flight - could not fit the Flight in the Flight Plan indicated Resource";
        internal const string ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NO_SPACE_ON_FP_RESOURCE_PIER = "Unallocated Flight - could not fit the Flight in the Flight Plan indicated Resource's Pier";
        internal const string ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_FP_RESOURCE_NOT_FOUND = "Unallocated Flight - could not find the Flight Plan indicated Resource";
        internal const string ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_USING_ALGORITHM = "Unallocated Flight - the allocation could not assign the Flight to a Resource";
        internal const string ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NOT_ALLOWED_TO_USE_FP_RESOURCE = "Unallocated Flight - Flight not allowed to use the Flight Plan indicated Resource";
        internal const string ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_FROM_FLIGHT_PLAN = "Unallocated Flight from Flight Plan";
        internal const string ALLOCATION_ISSUE_TYPE_ALLOCATED_TO_BACKUP = "Back-up Resource Used";
        internal const string ALLOCATION_ISSUE_TYPE_BROKE_SCHENGEN_CONSTRAINT = "Schengen Constraint Broked";
        internal const string ALLOCATION_ISSUE_TYPE_ALLOWED_BOARDING_ROOM_OVERLAPPING = "Allowed Boarding Room Overlapping";
        internal const string ALLOCATION_ISSUE_TYPE_ALLOCATED_USING_OCT = "Flight Allocated using Opening-Closing Times table";

        internal const string ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NO_SPACE_ON_PIER = "Unallocated Flight - the allocation could not assign the Flight to a Resource (no space on the Pier)";

        internal const string ALLOCATION_ISSUE_TYPE_BIG_FLIGHT = "Big Flight (Occupies 2 Boarding Rooms)";
        
        AllocationAssistant allocationAssistant = null;
        TimeInterval allocationTimeInterval = null;
        double samplingStep;
        double analysisRange;
        List<Resource> resources = new List<Resource>();
        List<Flight> allFlights = new List<Flight>();
        GestionDonneesHUB2SIM donnees;
        string scenarioName;
        internal string ciOCTAirlineAirportExceptionTableName = "";

        internal List<Flight> flightsWithAirlinesFromOCTExceptions = new List<Flight>();

        public AllocationOutput(string _scenarioName,
            List<Resource> _resources, List<Flight> _allFlights,
            TimeInterval _allocationTimeInterval, 
            double _allocationStep, double _analysisRange,
            AllocationAssistant _allocationAssistant,
            GestionDonneesHUB2SIM _donnees)
        {
            scenarioName = _scenarioName;
            resources.AddRange(_resources);
            allFlights.AddRange(_allFlights);
            allocationTimeInterval = _allocationTimeInterval;
            samplingStep = _allocationStep;
            analysisRange = _analysisRange;
            allocationAssistant = _allocationAssistant;
            donnees = _donnees;            
        }

        public DataTable getAvailableResourcesTable(string allocationType)
        {
            DataTable availableResourcesTable = new DataTable(AVAILABLE_RESOURCES_TABLE_NAME);

            int resourceIdColumnIndex = availableResourcesTable.Columns.Count;
            availableResourcesTable.Columns.Add("Resource Id", typeof(Int32));

            int resourceNameColumnIndex = availableResourcesTable.Columns.Count;
            availableResourcesTable.Columns.Add("Resource Name", typeof(String));

            int resourceCodeColumnIndex = availableResourcesTable.Columns.Count;
            availableResourcesTable.Columns.Add("Resource Code", typeof(String));

            int pierCodeColumnIndex = -1;
            if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                pierCodeColumnIndex = availableResourcesTable.Columns.Count;
                availableResourcesTable.Columns.Add("Resource Pier", typeof(String));
            }

            int resourceTypeColumnIndex = -1;
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                resourceTypeColumnIndex = availableResourcesTable.Columns.Count;
                availableResourcesTable.Columns.Add("Resource Type", typeof(String));
            }

            int initialAvailableTimeIntervalsColumnIndex = availableResourcesTable.Columns.Count;
            availableResourcesTable.Columns.Add("Resource Initial Available Time Intervals", typeof(String));

            foreach (Resource resource in resources)
            {
                foreach (TimeInterval interval in resource.initialAvailableTimeIntervals)
                {
                    DataRow row = availableResourcesTable.NewRow();
                                    
                    row[resourceIdColumnIndex] = resource.codeId;
                    row[resourceNameColumnIndex] = resource.name;
                    row[resourceCodeColumnIndex] = resource.code;
                    if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                    {
                        if (pierCodeColumnIndex != -1 && resource.pier != null)
                            row[pierCodeColumnIndex] = resource.pier.code;
                    }
                    if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                    {
                        if (resourceTypeColumnIndex != -1)
                        {
                            string resourceType = "";
                            if (Allocation.contactAreaResourceCodes.Contains(resource.code))
                                resourceType = Allocation.PARKING_CONTACT_AREA_RESOURCE_TYPE;
                            else if (Allocation.remoteResourceCodes.Contains(resource.code))
                                resourceType = Allocation.PARKING_REMOTE_RESOURCE_TYPE;
                            else if (Allocation.backUpResourceCodes.Contains(resource.code))
                                resourceType = Allocation.PARKING_BACKUP_RESOURCE_TYPE;
                            row[resourceTypeColumnIndex] = resourceType;
                        }
                    }
                    
                    string initialIntervalsAsString = "[" + interval.fromDate.DayOfWeek + ", " + interval.fromDate
                        + " - " + interval.toDate.DayOfWeek + ", " + interval.toDate + "] ";
                    row[initialAvailableTimeIntervalsColumnIndex] = initialIntervalsAsString;

                    availableResourcesTable.Rows.Add(row);
                }
            }

            availableResourcesTable.AcceptChanges();
            return availableResourcesTable;
        }

        public DataTable getAllocationResultTable(string allocationType)
        {
            DataTable allocationResultTable = new DataTable(ALLOCATION_RESULT_TABLE_NAME);

            int resourceNameColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Resource Name", typeof(String));

            //int isOccupiedBySchengenColumnIndex = allocationResultTable.Columns.Count;
            //allocationResultTable.Columns.Add("Resource Is Occupied by Schengen", typeof(Boolean));

            int flightIdColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Flight Id", typeof(Int32));

            int flightNumberColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Flight Number", typeof(String));

            int flightCompleteDateColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Flight Complete Date", typeof(DateTime));

            int flightCategoryColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Flight Category", typeof(String));

            int airlineCodeColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Airline Code", typeof(String));

            int airportCodeColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Airport Code", typeof(String));

            int aircraftTypeColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Aircraft Type", typeof(String));

            int aircraftBodyCategoryColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Aircraft Body Category", typeof(String));

            int occupationStartDayOfWeekColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Occupation Start Day of Week", typeof(String));

            int occupationStartColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Occupation Start", typeof(DateTime));

            int occupationEndDayOfWeekColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Occupation End Day of Week", typeof(String));

            int occupationEndColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Occupation End", typeof(DateTime));

            int pierCodeColumnIndex = -1;
            int pierOccupationStartDayOfWeekColumnIndex = -1;
            int pierOccupationStartColumnIndex = -1;
            int pierOccupationEndDayOfWeekColumnIndex = -1;
            int pierOccupationEndColumnIndex = -1;

            if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                pierCodeColumnIndex = allocationResultTable.Columns.Count;
                allocationResultTable.Columns.Add("Resource Pier", typeof(String));

                pierOccupationStartDayOfWeekColumnIndex = allocationResultTable.Columns.Count;
                allocationResultTable.Columns.Add("Pier Occupation Start Day of Week", typeof(String));
                pierOccupationStartColumnIndex = allocationResultTable.Columns.Count;
                allocationResultTable.Columns.Add("Pier Occupation Start", typeof(DateTime));

                pierOccupationEndDayOfWeekColumnIndex = allocationResultTable.Columns.Count;
                allocationResultTable.Columns.Add("Pier Occupation End Day of Week", typeof(String));
                pierOccupationEndColumnIndex = allocationResultTable.Columns.Count;
                allocationResultTable.Columns.Add("Pier Occupation End", typeof(DateTime));
            }

            int allocatedFromFlightPlanColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Allocated from Flight Plan", typeof(Boolean));

            int flightPlanAllocationColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add("Flight Plan Allocation", typeof(String));

            foreach (Resource resource in resources)
            {
                foreach (FlightAllocation flightAllocation in resource.flightAllocations)
                {
                    DataRow row = allocationResultTable.NewRow();
                    row[resourceNameColumnIndex] = flightAllocation.resource.name;                    
                    row[flightIdColumnIndex] = flightAllocation.allocatedFlight.id;
                    row[flightNumberColumnIndex] = flightAllocation.allocatedFlight.flightNumber;
                    row[flightCompleteDateColumnIndex] = flightAllocation.allocatedFlight.date.AddMinutes(flightAllocation.allocatedFlight.time.TotalMinutes);
                    row[flightCategoryColumnIndex] = flightAllocation.allocatedFlight.flightCategory;
                    row[airlineCodeColumnIndex] = flightAllocation.allocatedFlight.airlineCode;
                    row[airportCodeColumnIndex] = flightAllocation.allocatedFlight.airportCode;
                    row[aircraftTypeColumnIndex] = flightAllocation.allocatedFlight.aircraftType;
                    row[aircraftBodyCategoryColumnIndex] = flightAllocation.allocatedFlight.aircraftBodyCategory;
                    row[occupationStartDayOfWeekColumnIndex] = flightAllocation.occupationInterval.fromDate.DayOfWeek.ToString();
                    row[occupationStartColumnIndex] = flightAllocation.occupationInterval.fromDate;
                    row[occupationEndDayOfWeekColumnIndex] = flightAllocation.occupationInterval.toDate.DayOfWeek.ToString();
                    row[occupationEndColumnIndex] = flightAllocation.occupationInterval.toDate;

                    if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                    {
                        if (pierCodeColumnIndex != -1 && flightAllocation.resource.pier != null)
                            row[pierCodeColumnIndex] = flightAllocation.resource.pier.code;

                        if (pierOccupationStartDayOfWeekColumnIndex != -1)
                            row[pierOccupationStartDayOfWeekColumnIndex] = flightAllocation.allocatedFlight.pierOccupiedInterval.fromDate.DayOfWeek.ToString();
                        if (pierOccupationStartColumnIndex != -1)
                            row[pierOccupationStartColumnIndex] = flightAllocation.allocatedFlight.pierOccupiedInterval.fromDate;

                        if (pierOccupationEndDayOfWeekColumnIndex != -1)
                            row[pierOccupationEndDayOfWeekColumnIndex] = flightAllocation.allocatedFlight.pierOccupiedInterval.toDate.DayOfWeek.ToString();
                        if (pierOccupationEndColumnIndex != -1)
                            row[pierOccupationEndColumnIndex] = flightAllocation.allocatedFlight.pierOccupiedInterval.toDate;
                    }

                    row[allocatedFromFlightPlanColumnIndex] = flightAllocation.allocatedFlight.allocatedFromFlightPlan;
                    if (flightAllocation.allocatedFlight.fpAsBasisResourceToAllocateStartIndex == flightAllocation.allocatedFlight.fpAsBasisResourceToAllocateEndIndex)
                    {
                        row[flightPlanAllocationColumnIndex] = flightAllocation.allocatedFlight.fpAsBasisResourceToAllocateStartIndex.ToString();
                    }
                    else
                    {
                        row[flightPlanAllocationColumnIndex] = flightAllocation.allocatedFlight.fpAsBasisResourceToAllocateStartIndex
                            + " - " + flightAllocation.allocatedFlight.fpAsBasisResourceToAllocateEndIndex;
                    }
                    allocationResultTable.Rows.Add(row);
                }
            }
            
            string sortCriteria = "Occupation Start";
            DataView dv = new DataView(allocationResultTable);
            dv.Sort = sortCriteria;
            allocationResultTable = dv.ToTable();

            foreach (Flight flight in allFlights)
            {
                if (flight.allocatedResources.Count == 0)
                {
                    DataRow row = allocationResultTable.NewRow();
                    row[resourceNameColumnIndex] = getBinResourceNameByAllocationType(allocationAssistant.allocationType);
                    row[flightIdColumnIndex] = flight.id;
                    row[flightNumberColumnIndex] = flight.flightNumber;
                    row[flightCompleteDateColumnIndex] = flight.date.AddMinutes(flight.time.TotalMinutes);
                    row[flightCategoryColumnIndex] = flight.flightCategory;
                    row[airlineCodeColumnIndex] = flight.airlineCode;
                    row[airportCodeColumnIndex] = flight.airportCode;
                    row[aircraftTypeColumnIndex] = flight.aircraftType;
                    row[aircraftBodyCategoryColumnIndex] = flight.aircraftBodyCategory;
                    if (flight.occupiedInterval != null)
                    {
                        row[occupationStartDayOfWeekColumnIndex] = flight.occupiedInterval.fromDate.DayOfWeek.ToString();
                        row[occupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                        row[occupationEndDayOfWeekColumnIndex] = flight.occupiedInterval.toDate.DayOfWeek.ToString();
                        row[occupationEndColumnIndex] = flight.occupiedInterval.toDate;
                    }
                    if (flight.fpAsBasisResourceToAllocateStartIndex == flight.fpAsBasisResourceToAllocateEndIndex)
                    {
                        row[flightPlanAllocationColumnIndex] = flight.fpAsBasisResourceToAllocateStartIndex.ToString();
                    }
                    else
                    {
                        row[flightPlanAllocationColumnIndex] = flight.fpAsBasisResourceToAllocateStartIndex
                            + " - " + flight.fpAsBasisResourceToAllocateEndIndex;
                    }
                    allocationResultTable.Rows.Add(row);                    
                }
            }
            allocationResultTable.AcceptChanges();
                        
            return allocationResultTable;
        }

        private string getBinResourceNameByAllocationType(string allocationType)
        {
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                return Allocation.P0_BIN_NAME;
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                return Allocation.BG0_BIN_NAME;
            else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
                return Allocation.CI0_BIN_NAME;
            return "";
        }

        public DataTable generateTextAllocationTable()
        {
            DataTable textAllocationTable = createTextAllocationTableStructure();
            fillTextAllocationTable(textAllocationTable);
            fillTextAllocationStatisticsColumns(textAllocationTable);
            return textAllocationTable;
        }

        private DataTable createTextAllocationTableStructure()
        {
            DataTable textAllocationTable = new DataTable(ALLOCATION_TEXT_GANTT_TABLE_NAME);

            textAllocationTable.Columns.Add(GlobalNames.sColumnTime, typeof(DateTime));
            textAllocationTable.Columns.Add(ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME, typeof(Int32));
            textAllocationTable.Columns.Add(ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME, typeof(Int32));
            textAllocationTable.Columns.Add(ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME, typeof(Int32));
            foreach (Resource resource in resources)
            {
                textAllocationTable.Columns.Add(resource.name, typeof(String));
            }
            string binResourceName = getBinResourceNameByAllocationType(allocationAssistant.allocationType);
            textAllocationTable.Columns.Add(binResourceName, typeof(String));

            textAllocationTable.Rows.Clear();
            OverallTools.DataFunctions.initialiserLignes(textAllocationTable,
                allocationTimeInterval.fromDate, allocationTimeInterval.toDate, samplingStep);
            markClosedIntervals(textAllocationTable);

            textAllocationTable.AcceptChanges();
            return textAllocationTable;
        }
        
        private void markClosedIntervals(DataTable textAllocationTable)
        {
            foreach (Resource resource in resources)
            {
                if (textAllocationTable.Columns.IndexOf(resource.name) != -1)
                {
                    foreach (DataRow row in textAllocationTable.Rows)
                    {
                        row.BeginEdit();
                        DateTime currentRowDate = DateTime.MinValue;
                        if (row[GlobalNames.sColumnTime].ToString() != null 
                            && DateTime.TryParse(row[GlobalNames.sColumnTime].ToString(), out currentRowDate))
                        {
                            bool resourceIsOpened = false;
                            foreach (TimeInterval resourceInterval in resource.initialAvailableTimeIntervals)
                            {
                                if (resourceInterval.containsDateTime(currentRowDate))
                                {
                                    resourceIsOpened = true;
                                    break;
                                }
                            }
                            if (!resourceIsOpened)
                            {
                                row[resource.name] = RESOURCE_CLOSED_IN_INTERVAL_MARKER;
                            }
                        }                        
                    }
                }
            }
        }

        private void fillTextAllocationTable(DataTable textAllocationTable)
        {
            foreach (Resource resource in resources)
            {
                foreach (FlightAllocation flightAllocation in resource.flightAllocations)
                {
                    string flightTextInGantt = flightAllocation.allocatedFlight.id + "_" + flightAllocation.allocatedFlight.flightNumber;
                    addFlightToTextGantt(flightTextInGantt, flightAllocation.occupationInterval, resource.name, textAllocationTable, samplingStep, allocationTimeInterval.fromDate);
                }
            }
            string binResourceName = getBinResourceNameByAllocationType(allocationAssistant.allocationType);
            foreach (Flight flight in allFlights)
            {
                if (flight.allocatedResources.Count == 0)
                {
                    string flightTextInGantt = flight.id + "_" + flight.flightNumber;
                    addFlightToTextGantt(flightTextInGantt, flight.occupiedInterval, binResourceName, textAllocationTable, samplingStep, allocationTimeInterval.fromDate);
                }
            }
        }

        public static void addFlightToTextGantt(string flightTextInGantt, TimeInterval occupationInterval, string resourceName,
            DataTable textAllocationTable, double samplingStep, DateTime scenarioStart)
        {
            double fromDateOpeningTimeMinutesDiff = (occupationInterval.fromDate - scenarioStart).TotalMinutes;
            double fromDateClosingTimeMinutesDiff = (occupationInterval.toDate - scenarioStart).TotalMinutes;
            int openingTimeIndex = (int)(fromDateOpeningTimeMinutesDiff / samplingStep);
            int closingTimeIndex = -1;
            if (fromDateClosingTimeMinutesDiff % samplingStep == 0)
            {
                closingTimeIndex = (int)(fromDateClosingTimeMinutesDiff / samplingStep) - 1;
            }
            else
            {
                closingTimeIndex = (int)(fromDateClosingTimeMinutesDiff / samplingStep);
            }
            if (textAllocationTable.Columns.IndexOf(resourceName) != -1
                && openingTimeIndex >= 0 && closingTimeIndex >= 0)
            {
                for (int i = openingTimeIndex; i <= closingTimeIndex; i++)
                {
                    if (i < textAllocationTable.Rows.Count)
                    {
                        if (textAllocationTable.Rows[i][resourceName].ToString() == "")
                            textAllocationTable.Rows[i][resourceName] += flightTextInGantt;
                        else
                            textAllocationTable.Rows[i][resourceName] += ", " + flightTextInGantt;
                    }
                }
            }
        }

        private void fillTextAllocationStatisticsColumns(DataTable textAllocationTable)
        {
            int neededColumnIndex = textAllocationTable.Columns.IndexOf(ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME);
            int occupiedColumnIndex = textAllocationTable.Columns.IndexOf(ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME);
            int nbFlightsColumnIndex = textAllocationTable.Columns.IndexOf(ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME);

            foreach (DataRow row in textAllocationTable.Rows)
            {                
                int occupied = 0;
                int firstOccupied = -1;
                int lastOccupied = -1;                
                List<string> flightsPerRow = new List<string>();
                for (int i = 4; i < textAllocationTable.Columns.Count; i++)
                {
                    if (row[i] != null)
                    {
                        string cellInfo = row[i].ToString();
                        if (cellInfo != "" && cellInfo != RESOURCE_CLOSED_IN_INTERVAL_MARKER)
                        {
                            if (firstOccupied == -1)
                                firstOccupied = i - 3;
                            lastOccupied = i - 3;
                            occupied++;                            
                            string[] flights = cellInfo.Split(',');
                            foreach (string flight in flights)
                                if (!flightsPerRow.Contains(flight))
                                    flightsPerRow.Add(flight);
                        }
                    }
                }
                if (firstOccupied != -1 && lastOccupied != -1)
                    row[neededColumnIndex] = lastOccupied - firstOccupied + 1;
                else
                    row[neededColumnIndex] = 0;
                row[occupiedColumnIndex] = occupied;
                row[nbFlightsColumnIndex] = flightsPerRow.Count();
            }
        }

        public DataTable getAllocationIssuesTable(List<Flight> flights,
            List<Resource> backUpResources, string allocationType, bool disableAircraftLinks)
        {
            DataTable allocationIssuesTable = new DataTable(ALLOCATION_ISSUES_TABLE_NAME);

            int issueTypeColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Issue Type", typeof(String));

            int flightIdColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Flight Id", typeof(Int32));

            int flightCompleteDateColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Flight Date", typeof(DateTime));

            int flightNbColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Flight Number", typeof(String));

            int flightCategoryColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Flight Category", typeof(String));

            int airlineCodeColumnIndex = -1;
            if (allocationAssistant.allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE
                && flightsWithAirlinesFromOCTExceptions.Count > 0)
            {
                airlineCodeColumnIndex = allocationIssuesTable.Columns.Count;
                allocationIssuesTable.Columns.Add("Airline Code", typeof(String));
            }

            int resourceIdColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Resource Id", typeof(Int32));

            int resourceNameColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Resource Name", typeof(String));

            int flightOccupationStartColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Flight Occupation Start", typeof(DateTime));

            int flightOccupationEndColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Flight Occupation End", typeof(DateTime));
            
            int checkInOverlappingInformationColumnIndex = -1;
            if (allocationAssistant.allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                checkInOverlappingInformationColumnIndex = allocationIssuesTable.Columns.Count;
                allocationIssuesTable.Columns.Add("Overlapping Information", typeof(String));
            }

            int pierCodeColumnIndex = -1;            
            int pierOccupationStartColumnIndex = -1;            
            int pierOccupationEndColumnIndex = -1;

            if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                pierCodeColumnIndex = allocationIssuesTable.Columns.Count;
                allocationIssuesTable.Columns.Add("Pier", typeof(String));

                pierOccupationStartColumnIndex = allocationIssuesTable.Columns.Count;
                allocationIssuesTable.Columns.Add("Flight Pier Occupation Start", typeof(DateTime));

                pierOccupationEndColumnIndex = allocationIssuesTable.Columns.Count;
                allocationIssuesTable.Columns.Add("Flight Pier Occupation End", typeof(DateTime));
            }

            //int isSchengenResourceColumnIndex = allocationIssuesTable.Columns.Count;
            //allocationIssuesTable.Columns.Add("Resource Is Occupied by Schengen", typeof(Boolean));

            int flightPlanAllocationColumnIndex = allocationIssuesTable.Columns.Count;
            allocationIssuesTable.Columns.Add("Flight Plan Allocation", typeof(String));

            if (flights == null || flights.Count == 0)
            {
                return allocationIssuesTable;
            }

            foreach (Flight flight in flights)
            {
                if (flight.allocatedWithAlgorithm)
                {
                    if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                    {   
                        //Flight allocated using OCT and not Aircraft Links
                        if (!disableAircraftLinks && flight.allocatedUsingOCT)
                        {
                            foreach (Resource allocatedResource in flight.allocatedResources)
                            {
                                DataRow row = allocationIssuesTable.NewRow();
                                row[issueTypeColumnIndex] = ALLOCATION_ISSUE_TYPE_ALLOCATED_USING_OCT;
                                row[flightIdColumnIndex] = flight.id;
                                DateTime flightCompleteDate = flight.date.Add(flight.time);
                                row[flightCompleteDateColumnIndex] = flightCompleteDate;
                                row[flightNbColumnIndex] = flight.flightNumber;
                                row[flightCategoryColumnIndex] = flight.flightCategory;
                                row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                                row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;

                                row[resourceIdColumnIndex] = allocatedResource.codeId;
                                row[resourceNameColumnIndex] = allocatedResource.code;
                                //row[isSchengenResourceColumnIndex] = allocatedResource.isOccupiedBySchengen;
                                if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                                {
                                    if (pierCodeColumnIndex != -1 && allocatedResource.pier != null)
                                        row[pierCodeColumnIndex] = allocatedResource.pier.code;
                                    if (pierOccupationStartColumnIndex != -1 && flight.pierOccupiedInterval != null)
                                        row[pierOccupationStartColumnIndex] = flight.pierOccupiedInterval.fromDate;
                                    if (pierOccupationEndColumnIndex != -1 && flight.pierOccupiedInterval != null)
                                        row[pierOccupationEndColumnIndex] = flight.pierOccupiedInterval.toDate;
                                }
                                allocationIssuesTable.Rows.Add(row);
                            }
                        }
                    }
                }

                string allocationIssueType = "";

                if (allocationAssistant.useFPasBasis)
                {
                    if (flight.allocatedWithAlgorithm)
                    {
                        if (flight.allocatedWithAlgorithmByBreakingSchengenConstraint)
                            allocationIssueType = ALLOCATION_ISSUE_TYPE_BROKE_SCHENGEN_CONSTRAINT;
                        else if (flight.allocatedWithAlgorithmByAllowingBoardingRoomOverlapping)
                            allocationIssueType = ALLOCATION_ISSUE_TYPE_ALLOWED_BOARDING_ROOM_OVERLAPPING;
                    }
                    else
                    {
                        if (!flight.allocatedFromFlightPlan)
                        {
                            if (flight.withResourceFromFPnotFoundInAvailable)
                                allocationIssueType = ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_FP_RESOURCE_NOT_FOUND;
                            else if (flight.noSpaceOnFPIndicatedResource)
                                allocationIssueType = ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NO_SPACE_ON_FP_RESOURCE;
                            else if (flight.notAllowedToUseTheResourceFromFP)
                                allocationIssueType = ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NOT_ALLOWED_TO_USE_FP_RESOURCE;
                            else if (flight.noSpaceOnPierOfFPIndicatedResource)
                                allocationIssueType = ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NO_SPACE_ON_FP_RESOURCE_PIER;                            
                            else //didn't have Flight Plan allocation specified but couldn't be allocated with the algorithm
                                allocationIssueType = ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_USING_ALGORITHM;
                        }
                    }                                        
                }
                else
                {
                    if (!flight.allocatedWithAlgorithm)
                    {
                        if (flight.noSpaceOnPier)
                            allocationIssueType = ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NO_SPACE_ON_PIER;
                        else
                            allocationIssueType = ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_USING_ALGORITHM;
                    }
                    else if (flight.allocatedWithAlgorithm)
                    {
                        if (flight.allocatedWithAlgorithmByBreakingSchengenConstraint)
                            allocationIssueType = ALLOCATION_ISSUE_TYPE_BROKE_SCHENGEN_CONSTRAINT;
                        else if (flight.allocatedWithAlgorithmByAllowingBoardingRoomOverlapping)
                            allocationIssueType = ALLOCATION_ISSUE_TYPE_ALLOWED_BOARDING_ROOM_OVERLAPPING;
                    }
                }

                if (allocationIssueType == ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_USING_ALGORITHM)
                {
                    DataRow row = allocationIssuesTable.NewRow();
                    row[issueTypeColumnIndex] = allocationIssueType;
                    row[flightIdColumnIndex] = flight.id;
                    DateTime flightCompleteDate = flight.date.Add(flight.time);
                    row[flightCompleteDateColumnIndex] = flightCompleteDate;
                    row[flightNbColumnIndex] = flight.flightNumber;
                    row[flightCategoryColumnIndex] = flight.flightCategory;
                    row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                    row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;
                    if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                    {                        
                        if (pierOccupationStartColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationStartColumnIndex] = flight.pierOccupiedInterval.fromDate;
                        if (pierOccupationEndColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationEndColumnIndex] = flight.pierOccupiedInterval.toDate;
                    }
                    allocationIssuesTable.Rows.Add(row);
                } 
                else if (allocationIssueType == ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NO_SPACE_ON_PIER)
                {
                    DataRow row = allocationIssuesTable.NewRow();
                    row[issueTypeColumnIndex] = allocationIssueType;
                    row[flightIdColumnIndex] = flight.id;
                    DateTime flightCompleteDate = flight.date.Add(flight.time);
                    row[flightCompleteDateColumnIndex] = flightCompleteDate;
                    row[flightNbColumnIndex] = flight.flightNumber;
                    row[flightCategoryColumnIndex] = flight.flightCategory;
                    row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                    row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;
                    if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                    {
                        if (pierOccupationStartColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationStartColumnIndex] = flight.pierOccupiedInterval.fromDate;
                        if (pierOccupationEndColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationEndColumnIndex] = flight.pierOccupiedInterval.toDate;
                    }
                    allocationIssuesTable.Rows.Add(row);
                }
                else if (allocationIssueType == ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_FP_RESOURCE_NOT_FOUND)
                {
                    DataRow row = allocationIssuesTable.NewRow();
                    row[issueTypeColumnIndex] = allocationIssueType;
                    row[flightIdColumnIndex] = flight.id;
                    DateTime flightCompleteDate = flight.date.Add(flight.time);
                    row[flightCompleteDateColumnIndex] = flightCompleteDate;
                    row[flightNbColumnIndex] = flight.flightNumber;
                    row[flightCategoryColumnIndex] = flight.flightCategory;
                    row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                    row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;
                    if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                    {                        
                        if (pierOccupationStartColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationStartColumnIndex] = flight.pierOccupiedInterval.fromDate;
                        if (pierOccupationEndColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationEndColumnIndex] = flight.pierOccupiedInterval.toDate;
                    }
                    if (flight.fpAsBasisResourceToAllocateStartIndex > 0 && flight.fpAsBasisResourceToAllocateEndIndex > 0)
                    {
                        if (flight.fpAsBasisResourceToAllocateStartIndex == flight.fpAsBasisResourceToAllocateEndIndex)
                        {
                            row[flightPlanAllocationColumnIndex] = flight.fpAsBasisResourceToAllocateStartIndex.ToString();
                        }
                        else
                        {
                            row[flightPlanAllocationColumnIndex] = flight.fpAsBasisResourceToAllocateStartIndex
                                + " - " + flight.fpAsBasisResourceToAllocateEndIndex;
                        }
                    }
                    allocationIssuesTable.Rows.Add(row);
                }
                else if (allocationIssueType == ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NO_SPACE_ON_FP_RESOURCE)
                {
                    DataRow row = allocationIssuesTable.NewRow();
                    row[issueTypeColumnIndex] = allocationIssueType;
                    row[flightIdColumnIndex] = flight.id;
                    DateTime flightCompleteDate = flight.date.Add(flight.time);
                    row[flightCompleteDateColumnIndex] = flightCompleteDate;
                    row[flightNbColumnIndex] = flight.flightNumber;
                    row[flightCategoryColumnIndex] = flight.flightCategory;
                    row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                    row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;
                    if (flight.fpAsBasisResourceToAllocateStartIndex > 0 && flight.fpAsBasisResourceToAllocateEndIndex > 0)
                    {
                        if (flight.fpAsBasisResourceToAllocateStartIndex == flight.fpAsBasisResourceToAllocateEndIndex)
                        {
                            row[flightPlanAllocationColumnIndex] = flight.fpAsBasisResourceToAllocateStartIndex.ToString();
                        }
                        else
                        {
                            row[flightPlanAllocationColumnIndex] = flight.fpAsBasisResourceToAllocateStartIndex
                                + " - " + flight.fpAsBasisResourceToAllocateEndIndex;
                        }                        
                    }
                    if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                    {                        
                        if (pierOccupationStartColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationStartColumnIndex] = flight.pierOccupiedInterval.fromDate;
                        if (pierOccupationEndColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationEndColumnIndex] = flight.pierOccupiedInterval.toDate;
                    }
                    allocationIssuesTable.Rows.Add(row);
                }
                else if (allocationIssueType == ALLOCATION_ISSUE_TYPE_UNALLOCATED_FLIGHT_NOT_ALLOWED_TO_USE_FP_RESOURCE)
                {
                    DataRow row = allocationIssuesTable.NewRow();
                    row[issueTypeColumnIndex] = allocationIssueType;
                    row[flightIdColumnIndex] = flight.id;
                    DateTime flightCompleteDate = flight.date.Add(flight.time);
                    row[flightCompleteDateColumnIndex] = flightCompleteDate;
                    row[flightNbColumnIndex] = flight.flightNumber;
                    row[flightCategoryColumnIndex] = flight.flightCategory;
                    row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                    row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;
                    if (flight.fpAsBasisResourceToAllocateStartIndex > 0 && flight.fpAsBasisResourceToAllocateEndIndex > 0)
                    {
                        if (flight.fpAsBasisResourceToAllocateStartIndex == flight.fpAsBasisResourceToAllocateEndIndex)
                        {
                            row[flightPlanAllocationColumnIndex] = flight.fpAsBasisResourceToAllocateStartIndex.ToString();
                        }
                        else
                        {
                            row[flightPlanAllocationColumnIndex] = flight.fpAsBasisResourceToAllocateStartIndex
                                + " - " + flight.fpAsBasisResourceToAllocateEndIndex;
                        }
                    }
                    if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                    {                        
                        if (pierOccupationStartColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationStartColumnIndex] = flight.pierOccupiedInterval.fromDate;
                        if (pierOccupationEndColumnIndex != -1 && flight.pierOccupiedInterval != null)
                            row[pierOccupationEndColumnIndex] = flight.pierOccupiedInterval.toDate;
                    }
                    allocationIssuesTable.Rows.Add(row);
                }
                else if (allocationIssueType == ALLOCATION_ISSUE_TYPE_BROKE_SCHENGEN_CONSTRAINT)
                {
                    foreach (Resource allocatedResource in flight.allocatedResources)
                    {
                        DataRow row = allocationIssuesTable.NewRow();
                        row[issueTypeColumnIndex] = allocationIssueType;
                        row[flightIdColumnIndex] = flight.id;
                        DateTime flightCompleteDate = flight.date.Add(flight.time);
                        row[flightCompleteDateColumnIndex] = flightCompleteDate;
                        row[flightNbColumnIndex] = flight.flightNumber;
                        row[flightCategoryColumnIndex] = flight.flightCategory;
                        row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                        row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;

                        row[resourceIdColumnIndex] = allocatedResource.codeId;
                        row[resourceNameColumnIndex] = allocatedResource.code;
                        //row[isSchengenResourceColumnIndex] = allocatedResource.isOccupiedBySchengen;
                        allocationIssuesTable.Rows.Add(row);
                    }
                }
                else if (allocationIssueType == ALLOCATION_ISSUE_TYPE_ALLOWED_BOARDING_ROOM_OVERLAPPING)
                {
                    foreach (Resource allocatedResource in flight.allocatedResources)
                    {
                        DataRow row = allocationIssuesTable.NewRow();
                        row[issueTypeColumnIndex] = allocationIssueType;
                        row[flightIdColumnIndex] = flight.id;
                        DateTime flightCompleteDate = flight.date.Add(flight.time);
                        row[flightCompleteDateColumnIndex] = flightCompleteDate;
                        row[flightNbColumnIndex] = flight.flightNumber;
                        row[flightCategoryColumnIndex] = flight.flightCategory;
                        row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                        row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;

                        row[resourceIdColumnIndex] = allocatedResource.codeId;
                        row[resourceNameColumnIndex] = allocatedResource.code;

                        if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                        {
                            if (pierCodeColumnIndex != -1 && allocatedResource.pier != null)
                                row[pierCodeColumnIndex] = allocatedResource.pier.code;
                            if (pierOccupationStartColumnIndex != -1 && flight.pierOccupiedInterval != null)
                                row[pierOccupationStartColumnIndex] = flight.pierOccupiedInterval.fromDate;
                            if (pierOccupationEndColumnIndex != -1 && flight.pierOccupiedInterval != null)
                                row[pierOccupationEndColumnIndex] = flight.pierOccupiedInterval.toDate;
                        }
                        allocationIssuesTable.Rows.Add(row);
                    }
                }

                if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE
                    && flight.needsTwoBoardingRooms(allocationAssistant.lowerNsSeatsLimitForLargeAircrafts))
                {
                    foreach (Resource allocatedResource in flight.allocatedResources)
                    {
                        DataRow row = allocationIssuesTable.NewRow();
                        row[issueTypeColumnIndex] = ALLOCATION_ISSUE_TYPE_BIG_FLIGHT;
                        row[flightIdColumnIndex] = flight.id;
                        DateTime flightCompleteDate = flight.date.Add(flight.time);
                        row[flightCompleteDateColumnIndex] = flightCompleteDate;
                        row[flightNbColumnIndex] = flight.flightNumber;
                        row[flightCategoryColumnIndex] = flight.flightCategory;
                        row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                        row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;

                        row[resourceIdColumnIndex] = allocatedResource.codeId;
                        row[resourceNameColumnIndex] = allocatedResource.code;

                        if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                        {
                            if (pierCodeColumnIndex != -1 && allocatedResource.pier != null)
                                row[pierCodeColumnIndex] = allocatedResource.pier.code;
                            if (pierOccupationStartColumnIndex != -1 && flight.pierOccupiedInterval != null)
                                row[pierOccupationStartColumnIndex] = flight.pierOccupiedInterval.fromDate;
                            if (pierOccupationEndColumnIndex != -1 && flight.pierOccupiedInterval != null)
                                row[pierOccupationEndColumnIndex] = flight.pierOccupiedInterval.toDate;
                        }
                        allocationIssuesTable.Rows.Add(row);
                    }
                }
            }
            foreach (Resource backUpResource in backUpResources)
            {
                if (backUpResource.flightAllocations.Count > 0)
                {
                    foreach (FlightAllocation flightAllocation in backUpResource.flightAllocations)
                    {
                        DataRow row = allocationIssuesTable.NewRow();
                        row[issueTypeColumnIndex] = ALLOCATION_ISSUE_TYPE_ALLOCATED_TO_BACKUP;
                        row[flightIdColumnIndex] = flightAllocation.allocatedFlight.id;
                        DateTime flightCompleteDate = flightAllocation.allocatedFlight.date.Add(flightAllocation.allocatedFlight.time);
                        row[flightCompleteDateColumnIndex] = flightCompleteDate;
                        row[flightNbColumnIndex] = flightAllocation.allocatedFlight.flightNumber;
                        row[flightCategoryColumnIndex] = flightAllocation.allocatedFlight.flightCategory;
                        row[flightOccupationStartColumnIndex] = flightAllocation.allocatedFlight.occupiedInterval.fromDate;
                        row[flightOccupationEndColumnIndex] = flightAllocation.allocatedFlight.occupiedInterval.toDate;

                        row[resourceIdColumnIndex] = backUpResource.codeId;
                        row[resourceNameColumnIndex] = backUpResource.code;
                        //row[isSchengenResourceColumnIndex] = backUpResource.isOccupiedBySchengen;
                        allocationIssuesTable.Rows.Add(row);
                    }
                }
            }
            if (allocationAssistant.allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                if (allocationAssistant.ciUseOCTexceptions && airlineCodeColumnIndex != -1)
                {
                    foreach (Flight flight in flightsWithAirlinesFromOCTExceptions)
                    {
                        foreach (Resource allocatedResource in flight.allocatedResources)
                        {
                            DataRow row = allocationIssuesTable.NewRow();
                            row[issueTypeColumnIndex] = "The Flight's Airline was not found in the Opening/Closing Times Exceptions";
                            row[flightIdColumnIndex] = flight.id;
                            DateTime flightCompleteDate = flight.date.Add(flight.time);
                            row[flightCompleteDateColumnIndex] = flightCompleteDate;
                            row[flightNbColumnIndex] = flight.flightNumber;
                            row[flightCategoryColumnIndex] = flight.flightCategory;
                            row[airlineCodeColumnIndex] = flight.airlineCode;
                            row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                            row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;

                            row[resourceIdColumnIndex] = allocatedResource.id;
                            row[resourceNameColumnIndex] = allocatedResource.code;

                            allocationIssuesTable.Rows.Add(row);
                        }
                    }
                }

                foreach (Flight flight in flights)
                {
                    if (flight.overlappingFlights.Count > 0)
                    {
                        foreach (Flight overlappingFlight in flight.overlappingFlights)
                        {
                            DataRow row = allocationIssuesTable.NewRow();
                            row[issueTypeColumnIndex] = "The Flight " + flight.id + " is overlapped by the Flight " + overlappingFlight.id;
                            row[flightIdColumnIndex] = flight.id;
                            DateTime flightCompleteDate = flight.date.Add(flight.time);
                            row[flightCompleteDateColumnIndex] = flightCompleteDate;
                            row[flightNbColumnIndex] = flight.flightNumber;
                            row[flightCategoryColumnIndex] = flight.flightCategory;                            
                            string overlappingInformation = getOverlappingInformation(flight, overlappingFlight);
                            if (checkInOverlappingInformationColumnIndex != -1)
                                row[checkInOverlappingInformationColumnIndex] = overlappingInformation;
                            allocationIssuesTable.Rows.Add(row);
                        }
                    }
                }
            }
            allocationIssuesTable.AcceptChanges();
            return allocationIssuesTable;
        }

        private string getOverlappingInformation(Flight flight, Flight overlappingFlight)
        {
            string overlappingInformation = "";
            if (flight == null || overlappingFlight == null)
                return overlappingInformation;
            TimeInterval overlappingInterval = flight.entireAllocationOccupationNeed.occupationInterval
                .getIntersectionTimeInterval(overlappingFlight.entireAllocationOccupationNeed.occupationInterval);
            List<Resource> commonResources = getCommonResources(flight, overlappingFlight);
            string commonResourcesCodes = "";
            foreach (Resource res in commonResources)
            {
                commonResourcesCodes += res.code + " ";
            }
            overlappingInformation = "The flights use the resources " + commonResourcesCodes 
                + " from " + overlappingInterval.fromDate + " to " + overlappingInterval.toDate;
            return overlappingInformation;
        }

        private List<Resource> getCommonResources(Flight flight, Flight overlappingFlight)
        {
            List<Resource> commonResources = new List<Resource>();
            if (flight == null || overlappingFlight == null)
                return commonResources;
            foreach (Resource resource in flight.allocatedResources)
            {
                if (overlappingFlight.allocatedResources.Contains(resource))
                    commonResources.Add(resource);
            }
            return commonResources;
        }

        internal List<DataTable> generateOccupationIssuesLogTables(List<OutputOccupationFlight> occupationOutputFlights,
            Dictionary<string, List<Flight>> resourcesWithOwnOverlappingFlightsList, List<Resource> availableResources)
        {
            List<DataTable> occupationIssuesTables = new List<DataTable>();
            foreach (Resource availableResource in availableResources)
            {
                DataTable occupationIssuesLogTable = new DataTable(availableResource.code + PAX2SIM.OCCUPATION_ISSUES_LOG_SUFFIX_TABLE_NAME);

                int issueTypeColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Issue", typeof(String));

                int flightIdColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Flight Id", typeof(Int32));

                int flightCompleteDateColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Flight Date", typeof(DateTime));

                int flightNbColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Flight Number", typeof(String));

                int flightCategoryColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Flight Category", typeof(String));

                int flightOccupationStartColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Flight Occupation Start", typeof(DateTime));

                int flightOccupationEndColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Flight Occupation End", typeof(DateTime));

                int resourceIdColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Resource Id", typeof(Int32));

                int resourceNameColumnIndex = occupationIssuesLogTable.Columns.Count;
                occupationIssuesLogTable.Columns.Add("Resource Name", typeof(String));

                foreach (OutputOccupationFlight outputOccupationFlight in occupationOutputFlights)
                {
                    if (outputOccupationFlight.flight.allocatedResources != null)
                    {
                        bool isAllocatedToResource = false;
                        foreach (Resource resource in outputOccupationFlight.flight.allocatedResources)
                        {
                            if (resource.code == availableResource.code)
                                isAllocatedToResource = true;
                        }
                        if (isAllocatedToResource)
                        {
                            List<string> issueMessages = new List<string>();
                            if (outputOccupationFlight.excessInputPaxNb > 0)
                            {
                                issueMessages.Add(outputOccupationFlight.excessInputPaxNb
                                    + " passengers could not access the Boarding Room due to the Boarding Entering Speed constraint");
                            }
                            if (outputOccupationFlight.excessOutputPaxNb > 0)
                            {
                                issueMessages.Add(outputOccupationFlight.excessOutputPaxNb
                                        + " passengers could not exit the Boarding Room due to the Boarding Exiting Speed constraint");
                            }
                            foreach (string issueMessage in issueMessages)
                            {
                                foreach (Resource allocatedResource in outputOccupationFlight.flight.allocatedResources)
                                {
                                    if (allocatedResource.code == availableResource.code)
                                    {
                                        DataRow row = occupationIssuesLogTable.NewRow();
                                        row[issueTypeColumnIndex] = issueMessage;
                                        row[flightIdColumnIndex] = outputOccupationFlight.flight.id;
                                        DateTime flightCompleteDate = outputOccupationFlight.flight.date.Add(outputOccupationFlight.flight.time);
                                        row[flightCompleteDateColumnIndex] = flightCompleteDate;
                                        row[flightNbColumnIndex] = outputOccupationFlight.flight.flightNumber;
                                        row[flightCategoryColumnIndex] = outputOccupationFlight.flight.flightCategory;
                                        row[flightOccupationStartColumnIndex] = outputOccupationFlight.flight.occupiedInterval.fromDate;
                                        row[flightOccupationEndColumnIndex] = outputOccupationFlight.flight.occupiedInterval.toDate;

                                        row[resourceIdColumnIndex] = allocatedResource.codeId;
                                        row[resourceNameColumnIndex] = allocatedResource.code;
                                        occupationIssuesLogTable.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                }

                string OVERLAPPING_PREFIX = "Overlapping: ";
                //List<Flight> allOverlappingFlights = new List<Flight>();
                //foreach (KeyValuePair<string, List<Flight>> pair in resourcesWithOwnOverlappingFlightsList)
                //{
                //    foreach (Flight overlappingFlightByResource in pair.Value)
                //    {
                //        if (!allOverlappingFlights.Contains(overlappingFlightByResource))
                //            allOverlappingFlights.Add(overlappingFlightByResource);
                //    }
                //}
                if (resourcesWithOwnOverlappingFlightsList.ContainsKey(availableResource.code))
                {
                    List<Flight> overlappingFlights = resourcesWithOwnOverlappingFlightsList[availableResource.code];
                    foreach (Flight flight in overlappingFlights)
                    {
                        string overlappedFlightIds = "";
                        foreach (Flight overlappingFlight in flight.overlappingFlights)
                        {
                            overlappedFlightIds += overlappingFlight.id + " ";
                        }
                        DataRow row = occupationIssuesLogTable.NewRow();
                        row[issueTypeColumnIndex] = OVERLAPPING_PREFIX + "Flight " + flight.id + " overlaps the flights: " + overlappedFlightIds;
                        row[flightIdColumnIndex] = flight.id;
                        DateTime flightCompleteDate = flight.date.Add(flight.time);
                        row[flightCompleteDateColumnIndex] = flightCompleteDate;
                        row[flightNbColumnIndex] = flight.flightNumber;
                        row[flightCategoryColumnIndex] = flight.flightCategory;
                        row[flightOccupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                        row[flightOccupationEndColumnIndex] = flight.occupiedInterval.toDate;
                        row[resourceIdColumnIndex] = availableResource.codeId;
                        row[resourceNameColumnIndex] = availableResource.code;
                        occupationIssuesLogTable.Rows.Add(row);
                    }
                }
                occupationIssuesLogTable.AcceptChanges();

                if (occupationIssuesLogTable.Rows.Count > 0)
                    occupationIssuesTables.Add(occupationIssuesLogTable);
            }
            return occupationIssuesTables;
        }

        public DataTable generateFlightPlanInformationTable(List<Flight> allocatedFlights, string octTableName)
        {
            DataTable flightPlanInformationTable = createFlightPlanInformationTableStructure();
            fillFlightPlanInformationTable(flightPlanInformationTable, allocatedFlights, octTableName);
            return flightPlanInformationTable;
        }
                
        private DataTable createFlightPlanInformationTableStructure()
        {
            DataTable flightPlanInformationTable = new DataTable(GlobalNames.FPI_TableName);

            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_ID, typeof(int));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_DATE, typeof(DateTime));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_Column_STD, typeof(TimeSpan));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_AirlineCode, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_FlightN, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_AirportCode, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_FlightCategory, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_AircraftType, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_Column_TSA, typeof(Boolean));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_NbSeats, typeof(int));
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Nb_Passengers, typeof(double));
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Nb_Bags, typeof(double));
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Container_Size, typeof(String));

            flightPlanInformationTable.Columns.Add(allocationAssistant.resourceType + GlobalNames.FPI_FIRST_DESK_COLUMN_SUFFIX, typeof(int));
            flightPlanInformationTable.Columns.Add(allocationAssistant.resourceType + GlobalNames.FPI_LAST_DESK_COLUMN_SUFFIX, typeof(int));
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_NbOfResourcesUsed, typeof(int));
            flightPlanInformationTable.Columns.Add(allocationAssistant.resourceType + GlobalNames.FPI_OPENING_TIME_COLUMN_SUFFIX, typeof(DateTime));
            flightPlanInformationTable.Columns.Add(allocationAssistant.resourceType + GlobalNames.FPI_CLOSING_TIME_COLUMN_SUFFIX, typeof(DateTime));
            flightPlanInformationTable.Columns.Add(allocationAssistant.resourceType + GlobalNames.FPI_TERMINAL_NB_COLUMN_SUFFIX, typeof(int));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPAirline_GroundHandlers, typeof(String));

            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User1, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User2, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User3, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User4, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User5, typeof(String));

            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Allocation_Type, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Resource_Type_Name, typeof(String));

            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_CalculationType, typeof(String));
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_OCTTableUsed, typeof(String));

            //VisualisationMode myMode = new VisualisationMode(true, false, true, null, null, false, false,
            //    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.Cell,
            //    new int[] { 0 }, null, null, null, null);
            //donnees.AddReplaceModeVisualisation(scenarioName, flightPlanInformationTable.TableName, myMode);

            //List<string> editableColumnNames = new List<string>(new string[]{allocationAssistant.resourceType + " First Desk", allocationAssistant.resourceType + " Last Desk",
            //GlobalNames.FPI_Column_NbOfResourcesUsed});
            
            //foreach (DataColumn column in flightPlanInformationTable.Columns)
            //{
            //    if (editableColumnNames.Contains(column.ColumnName))
            //        column.ReadOnly = false;
            //    else 
            //        column.ReadOnly = true;
            //}

            return flightPlanInformationTable;
        }

        private void fillFlightPlanInformationTable(DataTable fpiTable,
            List<Flight> allocatedFlights, string octTableName)
        {
            #region Flight Plan Information Indexes
            List<int> fpiIndexes = new List<int>();
            int flightIdColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            fpiIndexes.Add(flightIdColumnIndex);

            int flightDateColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            fpiIndexes.Add(flightDateColumnIndex);

            int flightSTDColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
            fpiIndexes.Add(flightSTDColumnIndex);

            int airlineCodeColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            fpiIndexes.Add(airlineCodeColumnIndex);

            int flightNbColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            fpiIndexes.Add(flightNbColumnIndex);

            int airportCodeColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            fpiIndexes.Add(airportCodeColumnIndex);

            int flightCategoryColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            fpiIndexes.Add(flightCategoryColumnIndex);

            int aircraftTypeColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
            fpiIndexes.Add(aircraftTypeColumnIndex);

            int tsaColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
            fpiIndexes.Add(tsaColumnIndex);

            int nbSeatsColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
            fpiIndexes.Add(nbSeatsColumnIndex);

            int nbPaxColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_Nb_Passengers);
            fpiIndexes.Add(nbPaxColumnIndex);

            int containerColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_Container_Size);
            fpiIndexes.Add(containerColumnIndex);

            int firstDeskColumnIndex = fpiTable.Columns.IndexOf(allocationAssistant.resourceType + " First Desk");
            fpiIndexes.Add(firstDeskColumnIndex);

            int lastDeskColumnIndex = fpiTable.Columns.IndexOf(allocationAssistant.resourceType + " Last Desk");
            fpiIndexes.Add(lastDeskColumnIndex);

            int nbResUsedColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_NbOfResourcesUsed);
            fpiIndexes.Add(nbResUsedColumnIndex);

            int openingTimeColumnIndex = fpiTable.Columns.IndexOf(allocationAssistant.resourceType + " Opening Time");
            fpiIndexes.Add(openingTimeColumnIndex);

            int closingTimeColumnIndex = fpiTable.Columns.IndexOf(allocationAssistant.resourceType + " Closing Time");
            fpiIndexes.Add(closingTimeColumnIndex);

            int nbColumnIndex = fpiTable.Columns.IndexOf(allocationAssistant.resourceType + " Terminal Nb");
            fpiIndexes.Add(nbColumnIndex);

            int groundHandlerColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPAirline_GroundHandlers);
            fpiIndexes.Add(groundHandlerColumnIndex);

            int user1ColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
            fpiIndexes.Add(user1ColumnIndex);

            int user2ColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
            fpiIndexes.Add(user2ColumnIndex);

            int user3ColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
            fpiIndexes.Add(user3ColumnIndex);

            int user4ColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
            fpiIndexes.Add(user4ColumnIndex);

            int user5ColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
            fpiIndexes.Add(user5ColumnIndex);

            int allocTypeColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_Allocation_Type);
            fpiIndexes.Add(allocTypeColumnIndex);

            int resourceTypeNameColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_Resource_Type_Name);
            fpiIndexes.Add(resourceTypeNameColumnIndex);

            int calculationTypeColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_CalculationType);
            fpiIndexes.Add(calculationTypeColumnIndex);

            int octTableUsedColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_OCTTableUsed);
            fpiIndexes.Add(octTableUsedColumnIndex);
            #endregion

            if (!areValidColumnIndexes(fpiIndexes))
                return;
                        
            foreach (Flight allocatedFlight in allocatedFlights)
            {
                bool hasPartialCheckInAllocation = false;
                if (allocationAssistant.allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)                
                    hasPartialCheckInAllocation = (allocatedFlight.partialAllocationOccupationNeed != null);
                
                DataRow newFpiRow = fpiTable.NewRow();
                newFpiRow[flightIdColumnIndex] = allocatedFlight.id;
                newFpiRow[flightDateColumnIndex] = allocatedFlight.date;
                newFpiRow[flightSTDColumnIndex] = allocatedFlight.time;
                newFpiRow[airlineCodeColumnIndex] = allocatedFlight.airlineCode;
                newFpiRow[flightNbColumnIndex] = allocatedFlight.flightNumber;
                newFpiRow[airportCodeColumnIndex] = allocatedFlight.airportCode;
                newFpiRow[flightCategoryColumnIndex] = allocatedFlight.flightCategory;
                newFpiRow[aircraftTypeColumnIndex] = allocatedFlight.aircraftType;
                newFpiRow[tsaColumnIndex] = false;
                newFpiRow[nbSeatsColumnIndex] = allocatedFlight.nbSeats;
                //newFpiRow[nbPaxColumnIndex] = 0;
                //newFpiRow[containerColumnIndex] = 0;

                List<Resource> resourcesAllocatedWhileOverlapping = new List<Resource>();
                int firstResourceNb = 0;
                int lastResourceNb = 0;
                if (allocationAssistant.allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
                {                    
                    List<Resource> resourcesUsedEntireOccupationInterval = allocatedFlight
                        .getAllocatedResourcesByOccupationInterval(allocatedFlight.entireAllocationOccupationNeed.occupationInterval);
                    LiegeTools.getFirstAndLastOccupiedStationNb(resourcesUsedEntireOccupationInterval,
                         out firstResourceNb, out lastResourceNb);
                    newFpiRow[firstDeskColumnIndex] = firstResourceNb;
                    newFpiRow[lastDeskColumnIndex] = lastResourceNb;
                    if (firstResourceNb > 0 && lastResourceNb > 0)
                        newFpiRow[nbResUsedColumnIndex] = lastResourceNb - firstResourceNb + 1;
                    else
                        newFpiRow[nbResUsedColumnIndex] = 0;
                    newFpiRow[openingTimeColumnIndex] = allocatedFlight.entireAllocationOccupationNeed.occupationInterval.fromDate;
                    newFpiRow[closingTimeColumnIndex] = allocatedFlight.entireAllocationOccupationNeed.occupationInterval.toDate;
                    
                    if (allocatedFlight.partialAllocationOccupationNeed == null)
                        resourcesAllocatedWhileOverlapping = allocatedFlight.getResourcesAllocatedWhileOverlapping(resourcesUsedEntireOccupationInterval);
                }
                else
                {
                    LiegeTools.getFirstAndLastOccupiedStationNb(allocatedFlight.allocatedResources,
                        out firstResourceNb, out lastResourceNb);
                    newFpiRow[firstDeskColumnIndex] = firstResourceNb;
                    newFpiRow[lastDeskColumnIndex] = lastResourceNb;
                    if (firstResourceNb > 0 && lastResourceNb > 0)
                        newFpiRow[nbResUsedColumnIndex] = lastResourceNb - firstResourceNb + 1;
                    else
                        newFpiRow[nbResUsedColumnIndex] = 0;

                    newFpiRow[openingTimeColumnIndex] = allocatedFlight.occupiedInterval.fromDate;
                    newFpiRow[closingTimeColumnIndex] = allocatedFlight.occupiedInterval.toDate;
                }                
                newFpiRow[nbColumnIndex] = 1;

                newFpiRow[user1ColumnIndex] = allocatedFlight.user1;
                newFpiRow[user2ColumnIndex] = allocatedFlight.user2;
                newFpiRow[user3ColumnIndex] = allocatedFlight.user3;
                newFpiRow[user4ColumnIndex] = allocatedFlight.user4;
                newFpiRow[user5ColumnIndex] = allocatedFlight.user5;

                newFpiRow[allocTypeColumnIndex] = allocationAssistant.flightPlanInformationCompatibleAllocationType;
                newFpiRow[resourceTypeNameColumnIndex] = allocationAssistant.resourceType;

                newFpiRow[calculationTypeColumnIndex] = "OCT table based";
                newFpiRow[octTableUsedColumnIndex] = octTableName;                
                fpiTable.Rows.Add(newFpiRow);

                if (hasPartialCheckInAllocation
                    && !allocationAssistant.useFPasBasis)
                {
                    DataRow partialCheckInRow = fpiTable.NewRow();
                    partialCheckInRow.ItemArray = newFpiRow.ItemArray;
                    List<Resource> resourcesUsedPartially = allocatedFlight
                        .getAllocatedResourcesByOccupationInterval(allocatedFlight.partialAllocationOccupationNeed.occupationInterval);
                    LiegeTools.getFirstAndLastOccupiedStationNb(resourcesUsedPartially, 
                        out firstResourceNb, out lastResourceNb);
                    partialCheckInRow[firstDeskColumnIndex] = firstResourceNb;
                    partialCheckInRow[lastDeskColumnIndex] = lastResourceNb;
                    if (firstResourceNb > 0 && lastResourceNb > 0)
                        partialCheckInRow[nbResUsedColumnIndex] = lastResourceNb - firstResourceNb + 1;
                    else
                        partialCheckInRow[nbResUsedColumnIndex] = 0;
                    partialCheckInRow[openingTimeColumnIndex] = allocatedFlight.partialAllocationOccupationNeed.occupationInterval.fromDate;
                    partialCheckInRow[closingTimeColumnIndex] = allocatedFlight.partialAllocationOccupationNeed.occupationInterval.toDate;
                    fpiTable.Rows.Add(partialCheckInRow);
                }

                if (allocationAssistant.allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE
                    && resourcesAllocatedWhileOverlapping.Count > 0)
                {
                    DataRow extraCheckInRow = fpiTable.NewRow();
                    extraCheckInRow.ItemArray = newFpiRow.ItemArray;
                    
                    LiegeTools.getFirstAndLastOccupiedStationNb(resourcesAllocatedWhileOverlapping,
                        out firstResourceNb, out lastResourceNb);
                    TimeInterval occupationInterval 
                        = LiegeTools.getFirstOccupationIntervalByResourcesAndFlight(allocatedFlight, resourcesAllocatedWhileOverlapping);
                    if (occupationInterval != null)
                    {
                        extraCheckInRow[firstDeskColumnIndex] = firstResourceNb;
                        extraCheckInRow[lastDeskColumnIndex] = lastResourceNb;
                        if (firstResourceNb > 0 && lastResourceNb > 0)
                            extraCheckInRow[nbResUsedColumnIndex] = lastResourceNb - firstResourceNb + 1;
                        else
                            extraCheckInRow[nbResUsedColumnIndex] = 0;
                        extraCheckInRow[openingTimeColumnIndex] = occupationInterval.fromDate;
                        extraCheckInRow[closingTimeColumnIndex] = occupationInterval.toDate;
                        fpiTable.Rows.Add(extraCheckInRow);
                    }
                }
            }
            fpiTable.AcceptChanges();
        }

        private bool areValidColumnIndexes(List<int> columnIndexes)
        {
            foreach (int index in columnIndexes)
            {
                if (index == -1)
                    return false;
            }
            return true;
        }

        
        
        public DataTable generateOpeningClosingTimesTable(ExceptionTable octExcTable,
            string octTableName, bool useExceptions, List<string> defaultFlightCategoriesAdded)
        {
            List<string> airlinesFromOCTExceptions = new List<string>();
            return generateOpeningClosingTimesTable(octExcTable, octTableName, useExceptions,
                defaultFlightCategoriesAdded, out airlinesFromOCTExceptions);
        }

        public DataTable generateOpeningClosingTimesTable(ExceptionTable octExcTable,
            string octTableName, bool useExceptions, List<string> defaultFlightCategoriesAdded,
            out List<string> airlinesFromOCTExceptions)
        {
            airlinesFromOCTExceptions = new List<string>();
            DataTable octTable = new DataTable(octTableName);
            int appliedToColumnIndex = octTable.Columns.Count;
            octTable.Columns.Add("Applied To", typeof(String));
            
            if (octExcTable != null && octExcTable.Table != null)
            {
                bool checkInOCT = false;
                if (octExcTable.Table.Rows.Count == 6)
                    checkInOCT = true;

                DataRow openingTimeRow = octExcTable.Table.Rows[0];
                DataRow closingTimeRow = octExcTable.Table.Rows[1];

                DataRow partialTimeRow = null;
                DataRow nbAllocatedStationsRow = null;
                DataRow nbPartialStationsRow = null;
                DataRow nbAdditionalRow = null;
                if (checkInOCT)
                {
                    partialTimeRow = octExcTable.Table.Rows[2];
                    nbAllocatedStationsRow = octExcTable.Table.Rows[3];
                    nbPartialStationsRow = octExcTable.Table.Rows[4];
                    nbAdditionalRow = octExcTable.Table.Rows[5];
                }

                if (openingTimeRow != null && closingTimeRow != null
                    && openingTimeRow[0] != null && closingTimeRow[0] != null)
                {
                    if (checkInOCT
                        && (partialTimeRow == null || nbAllocatedStationsRow == null
                            || nbPartialStationsRow == null || nbAdditionalRow == null))
                    {
                        return octTable;
                    }

                    int openingTimeColumnIndex = octTable.Columns.Count;
                    octTable.Columns.Add(openingTimeRow[0].ToString());

                    int closingTimeColumnIndex = octTable.Columns.Count;
                    octTable.Columns.Add(closingTimeRow[0].ToString());

                    int partialTimeColumnIndex = -1;
                    int nbAllocatedColumnIndex = -1;
                    int nbAllocatedAtPartialColumnIndex = -1;
                    int nbAdditionalColumnIndex = -1;

                    if (checkInOCT)
                    {
                        partialTimeColumnIndex = octTable.Columns.Count;
                        octTable.Columns.Add(partialTimeRow[0].ToString());

                        nbAllocatedColumnIndex = octTable.Columns.Count;
                        octTable.Columns.Add(nbAllocatedStationsRow[0].ToString());

                        nbAllocatedAtPartialColumnIndex = octTable.Columns.Count;
                        octTable.Columns.Add(nbPartialStationsRow[0].ToString());

                        nbAdditionalColumnIndex = octTable.Columns.Count;
                        octTable.Columns.Add(nbAdditionalRow[0].ToString());
                    }

                    List<string> columnNames = getColumnNames(octExcTable.Table);
                    Dictionary<string, OCT> columnNamesAndValues
                        = getColumnNamesAndValues(octExcTable.Table);

                    foreach (string columnName in columnNames)
                    {
                        OCT oct = null;
                        if (columnNamesAndValues.TryGetValue(columnName, out oct))
                        {
                            DataRow row = octTable.NewRow();
                            row[appliedToColumnIndex] = columnName;
                            row[openingTimeColumnIndex] = oct.openingMinute;
                            row[closingTimeColumnIndex] = oct.closingMinute;
                            if (partialTimeColumnIndex != -1)
                                row[partialTimeColumnIndex] = oct.partialMinute;
                            if (nbAllocatedColumnIndex != -1)
                                row[nbAllocatedColumnIndex] = oct.nbAllocated;
                            if (nbAllocatedAtPartialColumnIndex != -1)
                                row[nbAllocatedAtPartialColumnIndex] = oct.nbAtPartial;
                            if (nbAdditionalColumnIndex != -1)
                                row[nbAdditionalColumnIndex] = oct.nbAdditional;
                            octTable.Rows.Add(row);
                        }
                    }
                    if (useExceptions)
                    {
                        if (octExcTable.ExceptionFlight != null
                            && octExcTable.ExceptionFlight.Table != null)
                        {
                            columnNames = getColumnNames(octExcTable.ExceptionFlight.Table);
                            columnNamesAndValues
                                = getColumnNamesAndValues(octExcTable.ExceptionFlight.Table);
                            foreach (string columnName in columnNames)
                            {
                                OCT oct = null;
                                if (columnNamesAndValues.TryGetValue(columnName, out oct))
                                {
                                    DataRow row = octTable.NewRow();
                                    row[appliedToColumnIndex] = columnName;
                                    row[openingTimeColumnIndex] = oct.openingMinute;
                                    row[closingTimeColumnIndex] = oct.closingMinute;
                                    if (partialTimeColumnIndex != -1)
                                        row[partialTimeColumnIndex] = oct.partialMinute;
                                    if (nbAllocatedColumnIndex != -1)
                                        row[nbAllocatedColumnIndex] = oct.nbAllocated;
                                    if (nbAllocatedAtPartialColumnIndex != -1)
                                        row[nbAllocatedAtPartialColumnIndex] = oct.nbAtPartial;
                                    if (nbAdditionalColumnIndex != -1)
                                        row[nbAdditionalColumnIndex] = oct.nbAdditional;
                                    octTable.Rows.Add(row);
                                }
                            }
                        }
                        if (octExcTable.ExceptionAirline != null
                            && octExcTable.ExceptionAirline.Table != null)
                        {
                            columnNames = getColumnNames(octExcTable.ExceptionAirline.Table);
                            columnNamesAndValues
                                = getColumnNamesAndValues(octExcTable.ExceptionAirline.Table);
                            foreach (string columnName in columnNames)
                            {
                                OCT oct = null;
                                if (columnNamesAndValues.TryGetValue(columnName, out oct))
                                {
                                    DataRow row = octTable.NewRow();
                                    row[appliedToColumnIndex] = columnName;
                                    row[openingTimeColumnIndex] = oct.openingMinute;
                                    row[closingTimeColumnIndex] = oct.closingMinute;
                                    if (partialTimeColumnIndex != -1)
                                        row[partialTimeColumnIndex] = oct.partialMinute;
                                    if (nbAllocatedColumnIndex != -1)
                                        row[nbAllocatedColumnIndex] = oct.nbAllocated;
                                    if (nbAllocatedAtPartialColumnIndex != -1)
                                        row[nbAllocatedAtPartialColumnIndex] = oct.nbAtPartial;
                                    if (nbAdditionalColumnIndex != -1)
                                        row[nbAdditionalColumnIndex] = oct.nbAdditional;
                                    octTable.Rows.Add(row);
                                    if (!airlinesFromOCTExceptions.Contains(columnName))
                                        airlinesFromOCTExceptions.Add(columnName);
                                }
                            }
                        }
                    }
                }
            }
            octTable.AcceptChanges();
            return octTable;
        }

        private List<string> getColumnNames(DataTable octTable)
        {
            List<string> columnNames = new List<string>();
            if (octTable != null)
            {
                for (int i = 1; i < octTable.Columns.Count; i++)
                {
                    if (octTable.Columns[i].ColumnName.Contains(FlightPlanUpdateWithAllocationResult.BIG_FLIGHTS_DUMMY_FP_DEFAULT_FLIGHT_CATEGORY_SUFFIX))
                        continue;
                    columnNames.Add(octTable.Columns[i].ColumnName);
                }
            }
            return columnNames;
        }

        private Dictionary<string, OCT> getColumnNamesAndValues(DataTable octTable)
        {
            Dictionary<string, OCT> columnNamesAndValues = new Dictionary<string, OCT>();
            if (octTable != null)
            {
                for (int i = 1; i < octTable.Columns.Count; i++)
                {
                    string columnName = octTable.Columns[i].ColumnName;
                    OCT oct = new OCT();
                    double openingMinute = -1;
                    double closingMinute = -1; 
                    Double.TryParse(octTable.Rows[0][i].ToString(), out openingMinute);
                    Double.TryParse(octTable.Rows[1][i].ToString(), out closingMinute);
                    oct.openingMinute = openingMinute;
                    oct.closingMinute = closingMinute;

                    if (octTable.Rows.Count == 6)
                    {
                        double value = -1;
                        Double.TryParse(octTable.Rows[2][i].ToString(), out value);
                        oct.partialMinute = value;
                        Double.TryParse(octTable.Rows[3][i].ToString(), out value);
                        oct.nbAllocated = value;
                        Double.TryParse(octTable.Rows[4][i].ToString(), out value);
                        oct.nbAtPartial = value;
                        Double.TryParse(octTable.Rows[5][i].ToString(), out value);
                        oct.nbAdditional = value;
                    }
                    columnNamesAndValues.Add(columnName, oct);
                }
            }
            return columnNamesAndValues;
        }
        internal class OCT
        {
            internal double openingMinute { get; set; }
            internal double closingMinute { get; set; }
            internal double partialMinute { get; set; }
            internal double nbAllocated { get; set; }
            internal double nbAtPartial { get; set; }
            internal double nbAdditional { get; set; }
        }

        internal const string ALLOCATION_PARAMETER_NAME_SCENARIO_NAME = "Scenario Name";
        internal const string ALLOCATION_PARAMETER_NAME_ALLOCATION_TYPE = "Allocation Type";
        internal const string ALLOCATION_PARAMETER_NAME_START_DATE = "Start Date";
        internal const string ALLOCATION_PARAMETER_NAME_END_DATE = "End Date";
        internal const string ALLOCATION_DATE_FORMAT_LIEGE = "HH:mm dd/MM/yyyy";

        internal const string ALLOCATION_PARAMETER_NAME_FLIGHT_PROCESSING_ORDER = "Flights Processing Order";
        internal const string ALLOCATION_PROCESSING_ORDER_SEPARATOR = " and ";

        internal const string ALLOCATION_PARAMETER_NAME_TIME_STEP = "Time Step (min)";
        internal const string ALLOCATION_PARAMETER_NAME_ANALYSIS_RANGE = "Analysis Range (min)";

        internal const string ALLOCATION_PARAMETER_NAME_DELAY_BETWEEN_CONS_FLIGHTS = "Delay Between Consecutive Flights (min)";
        internal const string ALLOCATION_PARAMETER_NAME_USE_FP_AS_BASIS = "Use Flight Plan as Basis";
        internal const string ALLOCATION_PARAMETER_NAME_UPDATE_FP_WITH_RESULTS = "Update Flight Plan with Allocated Values";

        internal const string ALLOCATION_PARAMETER_NAME_BOARDING_ENTERING_SPEED = "Boarding Entering Speed (pax per min)";
        internal const string ALLOCATION_PARAMETER_NAME_BOARDING_EXITING_SPEED = "Boarding Exiting Speed (pax per min)";
        internal const string ALLOCATION_PARAMETER_NAME_LOWER_LIMIT_LARGE_AIRCRAFT = "Lower Limit for Large Aircrafts (nb seats)";
        internal const string ALLOCATION_PARAMETER_NAME_CALCULATE_OCCUPATION = "Calculate Occupation";

        internal const string ALLOCATION_PARAMETER_NAME_FPD_TABLE = "Departure Flight Plans Table";

        internal const string ALLOCATION_PARAMETER_NAME_PRK_OCT = "Parking Opening Closing Times Table";
        internal const string ALLOCATION_PARAMETER_NAME_PRK_OCT_USE_EXC = "Use Exceptions for Parking Opening Closing Times";
        internal const string ALLOCATION_PARAMETER_NAME_BG_OCT = "Boarding Gate Opening Closing Times Table";
        internal const string ALLOCATION_PARAMETER_NAME_BG_OCT_USE_EXC = "Use Exceptions for Boarding Gate Opening Closing Times";

        internal const string ALLOCATION_PARAMETER_NAME_CI_OCT = "Check-In Opening Closing Times Table";
        internal const string ALLOCATION_PARAMETER_NAME_CI_OCT_USE_EXC = "Use Exceptions for Check-In Opening Closing Times";
        internal const string ALLOCATION_PARAMETER_NAME_CI_OCT_AIRLINE_AIRPORT = "Check-In Opening Closing Times Airline-Airport Exceptions Table";
        
        internal const string ALLOCATION_PARAMETER_NAME_CI_SHOWUP = "Check-In ShowUp Table";
        internal const string ALLOCATION_PARAMETER_NAME_CI_SHOWUP_USE_EXC = "Use Exceptions for Check-In ShowUp";
        internal const string ALLOCATION_PARAMETER_NAME_CI_SHOWUP_IGNORE = "Ignore Check-InShowUp Data";

        internal const string ALLOCATION_PARAMETER_NAME_AIRCRAFT_TYPES = "Aircraft Types Table";
        internal const string ALLOCATION_PARAMETER_NAME_AIRCRAFT_TYPES_USE_EXC = "Use Exceptions for Aircraft Types";

        internal const string ALLOCATION_PARAMETER_NAME_LF_DISABLE = "Disable Loading Factors Table";
        internal const string ALLOCATION_PARAMETER_NAME_LF = "Loading Factors Table";
        internal const string ALLOCATION_PARAMETER_NAME_LF_USE_EXC = "Use Exceptions for Loading Factors";

        internal const string ALLOCATION_PARAMETER_NAME_AIRCRAFT_LINKS = "Aircraft Links Table";
        internal const string ALLOCATION_PARAMETER_NAME_AIRCRAFT_LINKS_DISABLE = "Disable Aircraft Links Table";

        internal const string ALLOCATION_PARAMETER_NAME_PRK_PRIORITIES = "Parking Priorities Table";
        internal const string ALLOCATION_PARAMETER_NAME_BG_PRIORITIES = "Boarding Gate Priorities Table";

        internal const string ALLOCATION_PARAMETERS_TABLE_COLUM_NAME = "Name";
        internal const string ALLOCATION_PARAMETERS_TABLE_COLUM_VALUE = "Value";
        public DataTable generateAllocationParametersTable()
        {
            DataTable allocationParametersTable = new DataTable(PAX2SIM.ALLOCATION_PARAMETERS_TABLE_NAME);

            int nameColumnIndex = allocationParametersTable.Columns.Count;
            allocationParametersTable.Columns.Add(ALLOCATION_PARAMETERS_TABLE_COLUM_NAME, typeof(String));

            int valueColumnIndex = allocationParametersTable.Columns.Count;
            allocationParametersTable.Columns.Add(ALLOCATION_PARAMETERS_TABLE_COLUM_VALUE, typeof(String));

            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_SCENARIO_NAME,
                allocationAssistant.scenarioName, nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_ALLOCATION_TYPE,
                allocationAssistant.allocationType, nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_START_DATE,
                allocationAssistant.fromDate.ToString(ALLOCATION_DATE_FORMAT_LIEGE), nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_END_DATE,
                allocationAssistant.toDate.ToString(ALLOCATION_DATE_FORMAT_LIEGE), nameColumnIndex, valueColumnIndex);

            string flightProcessingOrderParameter = allocationAssistant.mainSortType;
            if (allocationAssistant.secondarySortType != "")
                flightProcessingOrderParameter += ALLOCATION_PROCESSING_ORDER_SEPARATOR + allocationAssistant.secondarySortType;
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_FLIGHT_PROCESSING_ORDER,
                                        flightProcessingOrderParameter, nameColumnIndex, valueColumnIndex);

            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_TIME_STEP,
                            allocationAssistant.timeStepInMinutes.ToString(), nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_ANALYSIS_RANGE,
                            allocationAssistant.analysisRangeInMinutes.ToString(), nameColumnIndex, valueColumnIndex);

            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_DELAY_BETWEEN_CONS_FLIGHTS,
                allocationAssistant.delayBetweenConsecutiveFlightsInMinutes.ToString(), nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_USE_FP_AS_BASIS,
                allocationAssistant.useFPasBasis.ToString(), nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_UPDATE_FP_WITH_RESULTS,
                allocationAssistant.updateFPwithAllocation.ToString(), nameColumnIndex, valueColumnIndex);

            if (allocationAssistant.allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_BOARDING_ENTERING_SPEED,
                        allocationAssistant.boardingEnteringSpeedPaxPerMinute.ToString(), nameColumnIndex, valueColumnIndex);
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_BOARDING_EXITING_SPEED,
                        allocationAssistant.boardingExitingSpeedPaxPerMinute.ToString(), nameColumnIndex, valueColumnIndex);
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_LOWER_LIMIT_LARGE_AIRCRAFT,
                        allocationAssistant.lowerNsSeatsLimitForLargeAircrafts.ToString(), nameColumnIndex, valueColumnIndex);
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_CALCULATE_OCCUPATION,
                        allocationAssistant.calculateOccupation.ToString(), nameColumnIndex, valueColumnIndex);
            }

            if (allocationAssistant.fpdTable != null)
            {
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_FPD_TABLE,
                                allocationAssistant.fpdTable.TableName, nameColumnIndex, valueColumnIndex);
            }
            if (allocationAssistant.allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_PRK_OCT,
                                allocationAssistant.parkingOCTTableName, nameColumnIndex, valueColumnIndex);
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_PRK_OCT_USE_EXC,
                                            allocationAssistant.parkingUseOCTexceptions.ToString(), nameColumnIndex, valueColumnIndex);
            }
            if (allocationAssistant.allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_BG_OCT,
                                allocationAssistant.bgOCTTableName, nameColumnIndex, valueColumnIndex);
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_BG_OCT_USE_EXC,
                                            allocationAssistant.bgUseOCTexceptions.ToString(), nameColumnIndex, valueColumnIndex);
            }
            
            if (allocationAssistant.allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE
                || allocationAssistant.allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_CI_OCT,
                                    allocationAssistant.ciOCTTableName, nameColumnIndex, valueColumnIndex);
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_CI_OCT_USE_EXC,
                                            allocationAssistant.ciUseOCTexceptions.ToString(), nameColumnIndex, valueColumnIndex);
            }
            if ( allocationAssistant.allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_CI_OCT_AIRLINE_AIRPORT,
                                    ciOCTAirlineAirportExceptionTableName, nameColumnIndex, valueColumnIndex);                
            }
            if (allocationAssistant.allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_CI_SHOWUP,
                                allocationAssistant.ciShowUpTableName, nameColumnIndex, valueColumnIndex);
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_CI_SHOWUP_USE_EXC,
                                            allocationAssistant.ciShowUpUseExceptions.ToString(), nameColumnIndex, valueColumnIndex);

                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_CI_SHOWUP_IGNORE,
                           allocationAssistant.ignoreCheckInShowUp.ToString(), nameColumnIndex, valueColumnIndex);
            }
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_AIRCRAFT_TYPES,
                                        allocationAssistant.aircraftTypesTableName, nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_AIRCRAFT_TYPES_USE_EXC,
                allocationAssistant.useAircraftTypesExceptions.ToString(), nameColumnIndex, valueColumnIndex);

            if (allocationAssistant.allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                if (allocationAssistant.disableLoadingFactors)
                {
                    addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_LF_DISABLE,
                        allocationAssistant.disableLoadingFactors.ToString(), nameColumnIndex, valueColumnIndex);
                }
                else
                {
                    addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_LF,
                                            allocationAssistant.loadingFactorsTableName, nameColumnIndex, valueColumnIndex);
                    addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_LF_USE_EXC,
                        allocationAssistant.useLoadingFactorsExceptions.ToString(), nameColumnIndex, valueColumnIndex);
                }
            }
            if (allocationAssistant.allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                if (!allocationAssistant.disableAircraftLinks)
                {
                    addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_AIRCRAFT_LINKS,
                                        allocationAssistant.aircraftLinksTableName, nameColumnIndex, valueColumnIndex);
                }
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_AIRCRAFT_LINKS_DISABLE,
                                        allocationAssistant.disableAircraftLinks.ToString(), nameColumnIndex, valueColumnIndex);
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_PRK_PRIORITIES,
                                        allocationAssistant.parkingPrioritiesTableName, nameColumnIndex, valueColumnIndex);                
            }            
            if (allocationAssistant.allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_BG_PRIORITIES,
                                        allocationAssistant.boardingGatesPrioritiesTableName, nameColumnIndex, valueColumnIndex);
            }

            allocationParametersTable.AcceptChanges();
            return allocationParametersTable;
        }

        private void addParameterInfoToTable(DataTable allocationParametersTable,
            string parameterName, string parameterValue,
            int nameColumnIndex, int valueColumnIndex)
        {
            DataRow row = allocationParametersTable.NewRow();
            row[nameColumnIndex] = parameterName;
            row[valueColumnIndex] = parameterValue;
            allocationParametersTable.Rows.Add(row);
        }

        internal DataTable generateAircraftTypesTable(ExceptionTable exceptionTable, bool useExceptions)
        {
            DataTable aircraftTypesTable = getOutputAircraftTypesTableStructure();

            List<OutputAircraftType> outputAircraftTypes = new List<OutputAircraftType>();
            if (exceptionTable != null && exceptionTable.Table != null)
            {
                outputAircraftTypes = getAircraftTypesForOutputTable(exceptionTable.Table);
                if (outputAircraftTypes != null && outputAircraftTypes.Count > 0)
                {
                    addOutputAircraftTypesToOutputTable(outputAircraftTypes, aircraftTypesTable);
                }
                if (useExceptions)
                {
                    if (exceptionTable.ExceptionAirline != null && exceptionTable.ExceptionAirline.Table != null)
                    {
                        outputAircraftTypes = getAircraftTypesForOutputTable(exceptionTable.ExceptionAirline.Table);
                        if (outputAircraftTypes != null && outputAircraftTypes.Count > 0)
                        {
                            addOutputAircraftTypesToOutputTable(outputAircraftTypes, aircraftTypesTable);
                        }
                    }
                    if (exceptionTable.ExceptionFC != null && exceptionTable.ExceptionFC.Table != null)
                    {
                        outputAircraftTypes = getAircraftTypesForOutputTable(exceptionTable.ExceptionFC.Table);
                        if (outputAircraftTypes != null && outputAircraftTypes.Count > 0)
                        {
                            addOutputAircraftTypesToOutputTable(outputAircraftTypes, aircraftTypesTable);
                        }
                    }
                    if (exceptionTable.ExceptionFlight != null && exceptionTable.ExceptionFlight.Table != null)
                    {
                        outputAircraftTypes = getAircraftTypesForOutputTable(exceptionTable.ExceptionFlight.Table);
                        if (outputAircraftTypes != null && outputAircraftTypes.Count > 0)
                        {
                            addOutputAircraftTypesToOutputTable(outputAircraftTypes, aircraftTypesTable);
                        }
                    }
                }
            }
            aircraftTypesTable.AcceptChanges();
            return aircraftTypesTable;
        }

        internal const string APPLIED_TO_COLUMN_NAME = "Applied To";
        private DataTable getOutputAircraftTypesTableStructure()
        {
            DataTable aircraftTypesTable = new DataTable(PAX2SIM.AIRCRAFT_TYPES_TABLE_NAME);
            aircraftTypesTable.Columns.Add(APPLIED_TO_COLUMN_NAME, typeof(String));            
            aircraftTypesTable.Columns.Add(GlobalNames.sFPAircraft_AircraftTypes, typeof(String));            
            aircraftTypesTable.Columns.Add(GlobalNames.sFPAircraft_Description, typeof(String));            
            aircraftTypesTable.Columns.Add(GlobalNames.sFPAircraft_Wake, typeof(String));            
            aircraftTypesTable.Columns.Add(GlobalNames.sFPAircraft_Body, typeof(String));            
            aircraftTypesTable.Columns.Add(GlobalNames.sFPAircraft_NumberSeats, typeof(String));            
            aircraftTypesTable.Columns.Add(GlobalNames.sTableColumn_ULDLoose, typeof(String));
            return aircraftTypesTable;
        }
        private List<OutputAircraftType> getAircraftTypesForOutputTable(DataTable sourceTable)
        {
            List<OutputAircraftType> outputAircraftTypes = new List<OutputAircraftType>();

            int airlineExceptionColumnIndex = sourceTable.Columns.IndexOf("Airline");
            int flightCategoryExceptionColumnIndex = sourceTable.Columns.IndexOf("Flight Category");
            int flightExceptionColumnIndex = sourceTable.Columns.IndexOf("Flight");

            int aircraftTypeColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.sFPAircraft_AircraftTypes);
            int descriptionColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.sFPAircraft_Description);
            int wakeColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.sFPAircraft_Wake);
            int bodyColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.sFPAircraft_Body);
            int nbSeatsColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.sFPAircraft_NumberSeats);
            int uldLooseColumnIndex = sourceTable.Columns.IndexOf(GlobalNames.sTableColumn_ULDLoose);

            if (sourceTable != null)
            {
                foreach (DataRow row in sourceTable.Rows)
                {
                    OutputAircraftType outputAircraftType = new OutputAircraftType();
                    if (airlineExceptionColumnIndex != -1 && row[airlineExceptionColumnIndex] != null)
                        outputAircraftType.appliedTo = row[airlineExceptionColumnIndex].ToString();
                    else if (flightCategoryExceptionColumnIndex != -1 && row[flightCategoryExceptionColumnIndex] != null)
                        outputAircraftType.appliedTo = row[flightCategoryExceptionColumnIndex].ToString();
                    else if (flightExceptionColumnIndex != -1 && row[flightExceptionColumnIndex] != null)
                        outputAircraftType.appliedTo = row[flightExceptionColumnIndex].ToString();
                    else
                        outputAircraftType.appliedTo = "General Rule";

                    if (aircraftTypeColumnIndex != -1 && row[aircraftTypeColumnIndex] != null)
                        outputAircraftType.aircraftType = row[aircraftTypeColumnIndex].ToString();
                    if (descriptionColumnIndex != -1 && row[descriptionColumnIndex] != null)
                        outputAircraftType.description = row[descriptionColumnIndex].ToString();
                    if (wakeColumnIndex != -1 && row[wakeColumnIndex] != null)
                        outputAircraftType.wake = row[wakeColumnIndex].ToString();
                    if (bodyColumnIndex != -1 && row[bodyColumnIndex] != null)
                        outputAircraftType.body = row[bodyColumnIndex].ToString();
                    if (nbSeatsColumnIndex != -1 && row[nbSeatsColumnIndex] != null)
                        outputAircraftType.nbSeats = row[nbSeatsColumnIndex].ToString();
                    if (uldLooseColumnIndex != -1 && row[uldLooseColumnIndex] != null)
                        outputAircraftType.uldLoose = row[uldLooseColumnIndex].ToString();

                    outputAircraftTypes.Add(outputAircraftType);
                }
            }
            return outputAircraftTypes;
        }
        private void addOutputAircraftTypesToOutputTable(List<OutputAircraftType> outputAircraftTypes, DataTable outputAircraftTypeTable)
        {
            if (outputAircraftTypeTable != null
                && outputAircraftTypes != null)
            {
                int appliedToColumnIndex = outputAircraftTypeTable.Columns.IndexOf(APPLIED_TO_COLUMN_NAME);
                int aircraftTypeColumnIndex = outputAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_AircraftTypes);
                int descriptionColumnIndex = outputAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Description);
                int wakeColumnIndex = outputAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Wake);
                int bodyColumnIndex = outputAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Body);
                int nbSeatsColumnIndex = outputAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_NumberSeats);
                int uldLooseColumnIndex = outputAircraftTypeTable.Columns.IndexOf(GlobalNames.sTableColumn_ULDLoose);

                if (appliedToColumnIndex != -1 && aircraftTypeColumnIndex != -1 && descriptionColumnIndex != -1
                    && wakeColumnIndex != -1 && bodyColumnIndex != -1 && nbSeatsColumnIndex != -1 && uldLooseColumnIndex != -1)
                {
                    foreach (OutputAircraftType outputAircraftType in outputAircraftTypes)
                    {
                        DataRow row = outputAircraftTypeTable.NewRow();
                        row[appliedToColumnIndex] = outputAircraftType.appliedTo;
                        row[aircraftTypeColumnIndex] = outputAircraftType.aircraftType;
                        row[descriptionColumnIndex] = outputAircraftType.description;
                        row[wakeColumnIndex] = outputAircraftType.wake;
                        row[bodyColumnIndex] = outputAircraftType.body;
                        row[nbSeatsColumnIndex] = outputAircraftType.nbSeats;
                        row[uldLooseColumnIndex] = outputAircraftType.uldLoose;
                        outputAircraftTypeTable.Rows.Add(row);
                    }
                }                
            }
        }
        private class OutputAircraftType
        {
            internal string appliedTo { get; set; }
            internal string aircraftType { get; set; }
            internal string description { get; set; }
            internal string wake { get; set; }
            internal string body { get; set; }
            internal string nbSeats { get; set; }
            internal string uldLoose { get; set; }
        }

        internal DataTable generateCheckInShowUpTable(ExceptionTable exceptionTable, bool useExceptions)
        {
            if (exceptionTable == null && exceptionTable.Table == null)
                return null;
            DataTable checkInShowUpTable = exceptionTable.Table.Copy();

            if (useExceptions)
            {
                if (exceptionTable.ExceptionAirline != null && exceptionTable.ExceptionAirline.Table != null
                    && exceptionTable.ExceptionAirline.Table.Columns.Count > 2)
                {

                    
                }
                if (exceptionTable.ExceptionFC != null && exceptionTable.ExceptionFC.Table != null)
                {

                }
                if (exceptionTable.ExceptionFlight != null && exceptionTable.ExceptionFlight.Table != null)
                {

                }
                if (exceptionTable.ExceptionFB != null && exceptionTable.ExceptionFB.Table != null)
                {

                }
                if (exceptionTable.ExceptionAirlineFB != null && exceptionTable.ExceptionAirlineFB.Table != null)
                {

                }
                if (exceptionTable.ExceptionFlightFB != null && exceptionTable.ExceptionFlightFB.Table != null)
                {

                }
            }

            checkInShowUpTable.AcceptChanges();            
            return checkInShowUpTable;
        }

        internal DataTable removeColumnsForDuplFlightCategories(DataTable table)
        {
            List<DataColumn> notNeededColumns = new List<DataColumn>();
            foreach (DataColumn column in table.Columns)
            {                
                if (column.ColumnName.Contains(FlightPlanUpdateWithAllocationResult.BIG_FLIGHTS_DUMMY_FP_DEFAULT_FLIGHT_CATEGORY_SUFFIX))
                    notNeededColumns.Add(column);
            }
            for (int j = 0; j < notNeededColumns.Count; j++)
            {
                table.Columns.Remove(notNeededColumns[j]);
            }
            return table;
        }

        public List<DataTable> getOccupationTables(List<Flight> allocatedFlights,
            ExceptionTable checkInShowUpExceptionTable, int boardingEnteringSpeedPaxPerMinute, int boardingExitingSpeedPaxPerMinute,
            bool ignoreCheckInShowUp, out List<OutputOccupationFlight> occupationOutputFlights, out Dictionary<string, List<Flight>> resourcesWithOwnOverlappingFlightsList)
        {
            occupationOutputFlights = new List<OutputOccupationFlight>();
            List<DataTable> occupationTables = new List<DataTable>();            
            resourcesWithOwnOverlappingFlightsList = getResourcesWithOwnOverlappingFlightsList(allocatedFlights);            
            updateWithOverlappedFlights(resourcesWithOwnOverlappingFlightsList, allocatedFlights);
            
            Dictionary<string, List<Flight>> resourcesWithOwnFlightsList = getResourcesWithOwnFlightsList(allocatedFlights);
            occupationTables
                = generateOccupationTables(resourcesWithOwnOverlappingFlightsList, resourcesWithOwnFlightsList, checkInShowUpExceptionTable,
                                            boardingEnteringSpeedPaxPerMinute, boardingExitingSpeedPaxPerMinute, 
                                            ignoreCheckInShowUp, out occupationOutputFlights);
            return occupationTables;
        }

        private List<Flight> getOverlappingFlights(List<Flight> allocatedFlights)
        {
            List<Flight> overlappingFlights = new List<Flight>();
            foreach (Flight flight in allocatedFlights)
            {
                if (flight.allocatedWithAlgorithmByAllowingBoardingRoomOverlapping)
                    overlappingFlights.Add(flight);
            }
            return overlappingFlights;
        }

        /// <summary>
        /// K = resource code (ex: BG_1)
        /// V = flights that overlap on the resource
        /// </summary>
        /// <param name="overlappingFlights"></param>
        /// <returns></returns>
        private Dictionary<string, List<Flight>> getResourcesWithOwnOverlappingFlightsList(List<Flight> allocatedFlights)
        {
            Dictionary<string, List<Flight>> resourcesWithListOfOverlappingFlights = new Dictionary<string, List<Flight>>();
            foreach (Flight flight in allocatedFlights)
            {
                if (flight.allocatedWithAlgorithmByAllowingBoardingRoomOverlapping)
                {
                    List<string> resourceCodes = getAllocatedResourcesCodesByFlight(flight);
                    foreach (string resourceCode in resourceCodes)
                    {
                        if (!resourcesWithListOfOverlappingFlights.ContainsKey(resourceCode))
                        {
                            List<Flight> flights = new List<Flight>();
                            flights.Add(flight);
                            resourcesWithListOfOverlappingFlights.Add(resourceCode, flights);
                        }
                        else
                        {
                            List<Flight> flights = resourcesWithListOfOverlappingFlights[resourceCode];
                            flights.Add(flight);
                        }
                    }
                }
            }
            return resourcesWithListOfOverlappingFlights;
        }

        private Dictionary<string, List<Flight>> getResourcesWithOwnFlightsList(List<Flight> allocatedFlights)
        {
            Dictionary<string, List<Flight>> resourcesWithListOfOwnFlights = new Dictionary<string, List<Flight>>();
            foreach (Flight flight in allocatedFlights)
            {
                List<string> resourceCodes = getAllocatedResourcesCodesByFlight(flight);
                foreach (string resourceCode in resourceCodes)
                {
                    if (!resourcesWithListOfOwnFlights.ContainsKey(resourceCode))
                    {
                        List<Flight> flights = new List<Flight>();
                        flights.Add(flight);
                        resourcesWithListOfOwnFlights.Add(resourceCode, flights);
                    }
                    else
                    {
                        List<Flight> flights = resourcesWithListOfOwnFlights[resourceCode];
                        flights.Add(flight);
                    }
                }
            }
            return resourcesWithListOfOwnFlights;
        }

        private List<string> getAllocatedResourcesCodesByFlight(Flight flight)
        {
            List<string> allocatedResourcesCodes = new List<string>();
            foreach (Resource resource in flight.allocatedResources)
            {
                if (!allocatedResourcesCodes.Contains(resource.code))
                    allocatedResourcesCodes.Add(resource.code);
            }
            return allocatedResourcesCodes;
        }

        private void updateWithOverlappedFlights(Dictionary<string, List<Flight>> resourcesWithOwnOverlappingFlightsList,
            List<Flight> allocatedFlights)
        {
            foreach (KeyValuePair<string, List<Flight>> pair in resourcesWithOwnOverlappingFlightsList)
            {
                List<Flight> overlappedFlights = new List<Flight>();
                foreach (Flight flight in pair.Value)
                {
                    overlappedFlights.AddRange(getOverlappedFlightsByFlightAndResourceCode(flight, pair.Key, allocatedFlights));
                }
                foreach (Flight flight in overlappedFlights)
                {
                    if (!pair.Value.Contains(flight))
                        pair.Value.Add(flight);
                }
            }
        }

        private List<Flight> getOverlappedFlightsByFlightAndResourceCode(Flight flight, string resourceCode,
            List<Flight> allocatedFlights)
        {
            List<Flight> overlappedFlights = new List<Flight>();
            foreach (Flight allocatedFlight in allocatedFlights)
            {
                TimeInterval allocatedFlightOccupiedTimeInterval = new TimeInterval(allocatedFlight.occupiedInterval.fromDate, 
                                allocatedFlight.occupiedInterval.toDate.AddMinutes(allocationAssistant.delayBetweenConsecutiveFlightsInMinutes));
                if (allocatedFlightOccupiedTimeInterval.intersectsTimeInterval(flight.occupiedInterval))
                {
                    //check if allocated on the same resource
                    //if (haveCommonResourceAllocated(flight, allocatedFlight))
                    if (areBothAllocatedOnTheSameResource(flight, allocatedFlight, resourceCode))
                    {
                        overlappedFlights.Add(allocatedFlight);

                        if (!flight.overlappingFlights.Contains(allocatedFlight)
                            && flight.id != allocatedFlight.id)
                        {
                            flight.overlappingFlights.Add(allocatedFlight);
                        }
                        if (!allocatedFlight.overlappingFlights.Contains(flight)
                            && allocatedFlight.id != flight.id)
                        {
                            allocatedFlight.overlappingFlights.Add(flight);
                        }
                    }
                }
            }
            return overlappedFlights;
        }

        private bool haveCommonResourceAllocated(Flight flightA, Flight flightB)
        {
            List<string> flightAResourceCodes = new List<string>();
            foreach (Resource resourceA in flightA.allocatedResources)
                flightAResourceCodes.Add(resourceA.code);
            foreach (Resource resourceB in flightB.allocatedResources)
            {
                if (flightAResourceCodes.Contains(resourceB.code))
                    return true;
            }
            return false;
        }

        private bool areBothAllocatedOnTheSameResource(Flight flightA, Flight flightB, string resouceCode)
        {
            List<string> flightAResourceCodes = new List<string>();
            bool foundResource = false;
            foreach (Resource resourceA in flightA.allocatedResources)
            {
                if (resourceA.code == resouceCode)
                    foundResource = true;
            }
            if (foundResource)
            {
                foreach (Resource resourceB in flightB.allocatedResources)
                {
                    if (resourceB.code == resouceCode)
                        return true;
                }
            }
            return false;
        }

        private List<DataTable> generateOccupationTables(Dictionary<string, List<Flight>> resourcesWithOwnOverlappingFlightsList,
            Dictionary<string, List<Flight>> resourcesWithOwnFlightsList,
            ExceptionTable checkInShowUpExceptionTable, int boardingEnteringSpeedPaxPerMinute, int boardingExitingSpeedPaxPerMinute,
            bool ignoreCheckInShowUp, out List<OutputOccupationFlight> occupationOutputFlights)
        {
            occupationOutputFlights = new List<OutputOccupationFlight>();
            List<DataTable> occupationTables = new List<DataTable>();
            foreach (KeyValuePair<string, List<Flight>> pair in resourcesWithOwnOverlappingFlightsList)
            {
                string occupationIssuesTableName = pair.Key + PAX2SIM.OCCUPATION_ISSUES_SUFFIX_TABLE_NAME;
                DataTable occupationTable
                    = generateOccupationTable(occupationIssuesTableName, pair.Value, checkInShowUpExceptionTable,
                                                boardingEnteringSpeedPaxPerMinute, boardingExitingSpeedPaxPerMinute, ignoreCheckInShowUp, out occupationOutputFlights);                
                if (occupationTable != null)
                    occupationTables.Add(occupationTable);
            }
            foreach (KeyValuePair<string, List<Flight>> pair in resourcesWithOwnFlightsList)
            {
                string occupationTableName = pair.Key + PAX2SIM.OCCUPATION_SUFFIX_TABLE_NAME;                                
                DataTable occupationTable
                    = generateOccupationTable(occupationTableName, pair.Value, checkInShowUpExceptionTable,
                                                boardingEnteringSpeedPaxPerMinute, boardingExitingSpeedPaxPerMinute, ignoreCheckInShowUp, out occupationOutputFlights);
                if (occupationTable != null)
                    occupationTables.Add(occupationTable);
            }
            return occupationTables;
        }

        private DataTable generateOccupationTable(string tableName, List<Flight> overlappingFlights,
            ExceptionTable checkInShowUpExceptionTable, int boardingEnteringSpeedPaxPerMinute, int boardingExitingSpeedPaxPerMinute,
            bool ignoreCheckInShowUp, out List<OutputOccupationFlight> occupationOutputFlights)
        {
            DataTable occupationTable = new DataTable(tableName);

            #region Table structure
            int timeColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Time", typeof(DateTime));

            int slotEndOccupationColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Slot-End Occupation", typeof(Double));
            int minOccupationColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Min Occupation", typeof(Double));// typeof(Int32));
            int avgOccupationColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Average Occupation", typeof(Double));
            int maxOccupationColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Max Occupation", typeof(Double));//typeof(Int32));
            int nbFlightsColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Nb Flights", typeof(Int32));

            int preInputNbColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Pre-Input Nb Pax");

            int nbInColumnIndex = -1;
            int nbInAccumulatedColumnIndex = -1;
            int throughputInColumnIndex = -1;
            int throughputInstantInColumnIndex = -1;
            int nbOutColumnIndex = -1;
            int nbOutAccumulatedColumnIndex = -1;
            int throughputOutColumnIndex = -1;
            int throughputInstantOutColumnIndex = -1;
            
            nbInColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Nb Pax (Input)", typeof(Double));//typeof(Int32));
            //nbInAccumulatedColumnIndex = occupationTable.Columns.Count;
            //occupationTable.Columns.Add("Nb Pax (Input) Accumulated", typeof(Double));
            throughputInColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Throughput Rolling (Input)", typeof(Int32));
            throughputInstantInColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add(GlobalNames.OCCUPATION_TABLE_THROUGHPUT_INSTANT_IN_COLUMN_NAME, typeof(Int32));

            nbOutColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Nb Pax (Output)", typeof(Double));
            //nbOutAccumulatedColumnIndex = occupationTable.Columns.Count;
            //occupationTable.Columns.Add("Nb Pax (Output) Accumulated", typeof(Double));
            throughputOutColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add("Throughput Rolling (Output)", typeof(Int32));
            throughputInstantOutColumnIndex = occupationTable.Columns.Count;
            occupationTable.Columns.Add(GlobalNames.OCCUPATION_TABLE_THROUGHPUT_INSTANT_OUT_COLUMN_NAME, typeof(Int32));
            #endregion
            
            #region Initialization
            OverallTools.DataFunctions.initialiserLignes(occupationTable, allocationTimeInterval.fromDate, 
                                                            allocationTimeInterval.toDate, samplingStep);
            foreach (DataRow Row in occupationTable.Rows)
            {
                Row[slotEndOccupationColumnIndex] = 0;
                Row[minOccupationColumnIndex] = 0;
                Row[avgOccupationColumnIndex] = 0.0d;
                Row[maxOccupationColumnIndex] = 0;
                Row[nbFlightsColumnIndex] = 0;
                Row[preInputNbColumnIndex] = 0;
                Row[nbInColumnIndex] = 0;
                //Row[nbInAccumulatedColumnIndex] = 0;
                Row[throughputInColumnIndex] = 0;
                Row[throughputInstantInColumnIndex] = 0;
                Row[nbOutColumnIndex] = 0;
                //Row[nbOutAccumulatedColumnIndex] = 0;
                Row[throughputOutColumnIndex] = 0;
                Row[throughputInstantOutColumnIndex] = 0;                
            }
            #endregion

            occupationOutputFlights = new List<OutputOccupationFlight>();
 
            List<OccupationInterval> occupationIntervalsFromTable
                = getOccupationIntervalsFromTable(occupationTable, timeColumnIndex, nbInColumnIndex, samplingStep);

            foreach (Flight flight in overlappingFlights)
            {
                int bigFlightTotalNbPax = 0;
                if (flight.needsTwoBoardingRooms(allocationAssistant.lowerNsSeatsLimitForLargeAircrafts))
                {
                    bigFlightTotalNbPax = flight.nbSeats;
                    double splitNbSeats = flight.nbSeats / 2;
                    flight.nbSeats = (int)Math.Floor(splitNbSeats);
                }

                double nbPaxExceedingInputCapacity = 0;
                double nbPaxExceedingOutputCapacity = 0;
                if (ignoreCheckInShowUp)
                {
                    updateOccupationTableIntervalsWithInputNbUsingOnlyBoardingSpeed(occupationIntervalsFromTable, flight.nbSeats, flight.occupiedInterval.fromDate,
                        flight.pierOccupiedInterval.toDate, boardingEnteringSpeedPaxPerMinute, out nbPaxExceedingInputCapacity);
                }
                else
                {
                    DateTime flightCompleteDate = flight.date.AddMinutes(flight.time.TotalMinutes);
                    int showUpEcoColumnIndex = -1;
                    DataTable showUpEco = checkInShowUpExceptionTable.GetInformationsColumns(0, "D_" + flight.id, flight.airlineCode, flight.flightCategory, out showUpEcoColumnIndex);
                    int showUpBeginColumnIndex = showUpEco.Columns.IndexOf(GlobalNames.sColumnBegin);
                    int showUpEndColumnIndex = showUpEco.Columns.IndexOf(GlobalNames.sColumnEnd);
                    List<OccupationInterval> occupationIntervalsFromShowUp
                        = getOccupationIntervalsFromShowUp(showUpEco, showUpBeginColumnIndex, showUpEndColumnIndex, showUpEcoColumnIndex, flight.nbSeats, flightCompleteDate);
                    updateOccupationTableIntervalsWithPreInputNb(occupationIntervalsFromTable, occupationIntervalsFromShowUp, boardingEnteringSpeedPaxPerMinute, flight.occupiedInterval.fromDate,
                        out nbPaxExceedingInputCapacity);                    
                }                
                updateOccupationTableIntervalsWithOutputNb(occupationIntervalsFromTable, flight.nbSeats,
                    flight.pierOccupiedInterval.fromDate, flight.pierOccupiedInterval.toDate, boardingExitingSpeedPaxPerMinute, out nbPaxExceedingOutputCapacity);

                if (nbPaxExceedingInputCapacity > 0 || nbPaxExceedingOutputCapacity > 0)
                {
                    OutputOccupationFlight outputFlight
                        = new OutputOccupationFlight(flight, nbPaxExceedingInputCapacity, nbPaxExceedingOutputCapacity);
                    occupationOutputFlights.Add(outputFlight);
                }

                if (flight.needsTwoBoardingRooms(allocationAssistant.lowerNsSeatsLimitForLargeAircrafts))
                {
                    flight.nbSeats = bigFlightTotalNbPax;                    
                }
            }
            //updateOccupationTableIntervalsWithIntegerInputValues(occupationIntervalsFromTable);
            //updateAverageOccupationForOccupationTableIntervals(occupationIntervalsFromTable);
            //updateAccumulationsForOccupationTableIntervals(occupationIntervalsFromTable);
            
            double previousNbInAccumulation = 0;
            double previousNbOutAccumulation = 0;
            foreach (DataRow row in occupationTable.Rows)
            {
                DateTime startTime = DateTime.MinValue;
                if (DateTime.TryParse(row[timeColumnIndex].ToString(), out startTime))
                {
                    OccupationInterval occupationInterval = null;
                    foreach (OccupationInterval interval in occupationIntervalsFromTable)
                    {
                        if (interval.timeInterval.fromDate == startTime)
                        {
                            occupationInterval = interval;
                            break;
                        }
                    }
                    if (occupationInterval != null)
                    {

                        if (occupationInterval.nbFlights > 0)
                        {
                            double slotEndOccupation = occupationInterval.nbInCumul - occupationInterval.nbOutCumul;
                            if (slotEndOccupation < 0)//happens because of rounding up -> -1
                                slotEndOccupation = 0;
                            row[slotEndOccupationColumnIndex] = slotEndOccupation;

                            double accumulationStartDifference = Math.Max(previousNbInAccumulation, previousNbOutAccumulation) - Math.Min(previousNbInAccumulation, previousNbOutAccumulation);
                            double accumulationEndDifference = Math.Max(occupationInterval.nbInCumul, occupationInterval.nbOutCumul) - Math.Min(occupationInterval.nbInCumul, occupationInterval.nbOutCumul);
                            double minOccupation = Math.Min(accumulationStartDifference, accumulationEndDifference);
                            double maxOccupation = Math.Max(accumulationStartDifference, accumulationEndDifference);
                            double avgOccupation = (minOccupation + maxOccupation) / 2;
                                                        
                            row[minOccupationColumnIndex] = minOccupation;
                            row[avgOccupationColumnIndex] = avgOccupation;
                            row[maxOccupationColumnIndex] = maxOccupation;

                            row[nbFlightsColumnIndex] = occupationInterval.nbFlights;                            

                            row[nbInColumnIndex] = occupationInterval.nbIn;
                            //row[nbInAccumulatedColumnIndex] = occupationInterval.nbInCumul;
                            row[nbOutColumnIndex] = occupationInterval.nbOut;
                            //row[nbOutAccumulatedColumnIndex] = occupationInterval.nbOutCumul;

                            previousNbInAccumulation = occupationInterval.nbInCumul;
                            previousNbOutAccumulation = occupationInterval.nbOutCumul;
                        }
                        row[preInputNbColumnIndex] = occupationInterval.showUpPaxPreInputNb;
                    }
                }
            }
            OverallTools.ResultFunctions.AnalyzePeak_SlidingHourDouble(occupationTable, nbInColumnIndex, throughputInColumnIndex, samplingStep, analysisRange);
            OverallTools.ResultFunctions.AnalyzePeak_SlidingHourDouble(occupationTable, nbInColumnIndex, throughputInstantInColumnIndex, samplingStep, samplingStep);

            OverallTools.ResultFunctions.AnalyzePeak_SlidingHourDouble(occupationTable, nbOutColumnIndex, throughputOutColumnIndex, samplingStep, analysisRange);
            OverallTools.ResultFunctions.AnalyzePeak_SlidingHourDouble(occupationTable, nbOutColumnIndex, throughputInstantOutColumnIndex, samplingStep, samplingStep);
            return occupationTable;
        }

        private List<OccupationInterval> getOccupationIntervalsFromTable(DataTable occupationTable, 
            int timeColumnIndex, int observedAccumulationColumnIndex, double samplingStep)
        {
            List<OccupationInterval> occupationIntervals = new List<OccupationInterval>();
            if (occupationTable != null 
                && timeColumnIndex < occupationTable.Columns.Count && occupationTable.Columns[timeColumnIndex].DataType == typeof(DateTime)
                && observedAccumulationColumnIndex < occupationTable.Columns.Count)
            {
                foreach (DataRow row in occupationTable.Rows)
                {
                    DateTime startTime = DateTime.MinValue;
                    double accumulation = 0;
                    if (DateTime.TryParse(row[timeColumnIndex].ToString(), out startTime)
                        && Double.TryParse(row[observedAccumulationColumnIndex].ToString(), out accumulation))
                    {
                        DateTime endTime = startTime.AddMinutes(samplingStep);                        
                        OccupationInterval occupationInterval = new OccupationInterval(startTime, endTime, accumulation);
                        occupationIntervals.Add(occupationInterval);
                    }
                }
            }
            return occupationIntervals;
        }

        private List<OccupationInterval> getOccupationIntervalsFromShowUp(DataTable showUpTable,
            int startMinuteColumnIndex, int endMinuteColumnIndex, int paxPercentColumnIndex, 
            int flightTotalNbPax, DateTime flightDepartureTime)
        {
            int addedPassengers = 0;
            List<OccupationInterval> occupationIntervals = new List<OccupationInterval>();
            if (showUpTable != null
                && startMinuteColumnIndex < showUpTable.Columns.Count
                && endMinuteColumnIndex < showUpTable.Columns.Count
                && paxPercentColumnIndex < showUpTable.Columns.Count)
            {
                double partialPaxAccumulation = 0;
                int accumulatedPax = 0;
                for (int i = 0; i < showUpTable.Rows.Count; i++)
                {
                    DataRow row = showUpTable.Rows[i];
                    int startMinute = 0;
                    int endMinute = 0;
                    double paxPercent = 0;
                    if (Int32.TryParse(row[startMinuteColumnIndex].ToString(), out startMinute)
                        && Int32.TryParse(row[endMinuteColumnIndex].ToString(), out endMinute)
                        && Double.TryParse(row[paxPercentColumnIndex].ToString(), out paxPercent))
                    {
                        DateTime startTime = flightDepartureTime.AddMinutes(-endMinute);
                        DateTime endTime = flightDepartureTime.AddMinutes(-startMinute);
                        double realNbPax = paxPercent * flightTotalNbPax / 100;
                        int nbPax = (int)Math.Floor(realNbPax);
                        accumulatedPax += nbPax;

                        partialPaxAccumulation = partialPaxAccumulation + (realNbPax - nbPax);
                        if (partialPaxAccumulation >= 1 && accumulatedPax < flightTotalNbPax)
                        {
                            nbPax++;
                            accumulatedPax++;
                            partialPaxAccumulation = partialPaxAccumulation - 1;
                        }

                        addedPassengers += nbPax;

                        OccupationInterval occupationInterval = new OccupationInterval(startTime, endTime, nbPax);
                        occupationIntervals.Add(occupationInterval);                        
                    }
                }
                if (flightTotalNbPax - addedPassengers > 0)
                {
                    if (occupationIntervals.Count > 0)
                        occupationIntervals[1].nbIn++;
                }
            }
            occupationIntervals = occupationIntervals.OrderBy(o => o.timeInterval.fromDate).ToList();
            return occupationIntervals;
        }

        private void updateOccupationTableIntervalsWithPreInputNb(List<OccupationInterval> occupationIntervalsFromTable,
            List<OccupationInterval> occupationIntervalsFromShowUp, int boardingEnteringSpeedPaxPerMinute, DateTime boardingRoomOccupationStartTime,
            out double nbPaxExceedingMaxCapacity)
        {
            nbPaxExceedingMaxCapacity = 0;
            double paxBeforeBoardingRoomOpening = 0;
            double nbPaxInAccumulated = 0;
            foreach (OccupationInterval tableOccupationInterval in occupationIntervalsFromTable)
            {
                double maxPaxCapacityPerOccupationInterval
                       = boardingEnteringSpeedPaxPerMinute * tableOccupationInterval.timeInterval.timeIntervalLengthInMinutes;
                bool tableOccupationIntervalAffectedByFlight = false;
                double nbInputPaxAddedToOccupationInterval = 0;
                foreach (OccupationInterval showUpOccupationInterval in occupationIntervalsFromShowUp)
                {
                    DateTime startTime = showUpOccupationInterval.timeInterval.fromDate;
                    DateTime endTime = showUpOccupationInterval.timeInterval.toDate.AddMinutes(-1);
                    TimeInterval showUpOccupationTimeIntervalExcludingLastMinute = new TimeInterval(startTime, endTime);
                    if (showUpOccupationTimeIntervalExcludingLastMinute.intersectsTimeInterval(tableOccupationInterval.timeInterval)
                        && tableOccupationInterval.timeInterval.toDate != showUpOccupationInterval.timeInterval.fromDate)
                    {
                        tableOccupationIntervalAffectedByFlight = true;
                        //get the intersection time interval in minutes and divide by ciShowUp time Interval to determine the factor to apply for calculating the nb of pax
                        TimeInterval timeIntervalIntersection
                            = tableOccupationInterval.timeInterval.getIntersectionTimeInterval(showUpOccupationInterval.timeInterval);
                        if (timeIntervalIntersection.timeIntervalLengthInSeconds > 0
                            && showUpOccupationInterval.timeInterval.timeIntervalLengthInSeconds > 0)
                        {
                            double factor = timeIntervalIntersection.timeIntervalLengthInSeconds / showUpOccupationInterval.timeInterval.timeIntervalLengthInSeconds;
                            double realNbPax = factor * showUpOccupationInterval.nbIn;

                            tableOccupationInterval.showUpPaxPreInputNb += realNbPax;
                            nbInputPaxAddedToOccupationInterval += realNbPax;
                        }
                    }
                }
                if (tableOccupationIntervalAffectedByFlight)
                {                    
                    if (tableOccupationInterval.timeInterval.fromDate < boardingRoomOccupationStartTime)
                    {
                        paxBeforeBoardingRoomOpening += nbInputPaxAddedToOccupationInterval;
                    }
                    else
                    {
                        double otherFlightNbInForThisInterval = tableOccupationInterval.nbIn;

                        tableOccupationInterval.nbFlights++;
                        tableOccupationInterval.nbIn = tableOccupationInterval.nbIn + paxBeforeBoardingRoomOpening + nbInputPaxAddedToOccupationInterval + nbPaxExceedingMaxCapacity;
                        paxBeforeBoardingRoomOpening = 0;
                        if (tableOccupationInterval.nbIn > maxPaxCapacityPerOccupationInterval)
                        {
                            nbPaxExceedingMaxCapacity = tableOccupationInterval.nbIn - maxPaxCapacityPerOccupationInterval;
                            tableOccupationInterval.nbIn = maxPaxCapacityPerOccupationInterval;
                        }
                        else
                        {
                            nbPaxExceedingMaxCapacity = 0;
                        }
                                                
                        nbPaxInAccumulated = nbPaxInAccumulated + (tableOccupationInterval.nbIn - otherFlightNbInForThisInterval);
                        tableOccupationInterval.nbInCumul += nbPaxInAccumulated;                        
                    }
                }
            }
        }

        private void updateOccupationTableIntervalsWithInputNbUsingOnlyBoardingSpeed(List<OccupationInterval> occupationIntervalsFromTable,
            int flightTotalNbPax, DateTime checkInOpeningTime, DateTime boardingGateClosingTime, int boardingEnteringSpeedPaxPerMin,
            out double nbPaxExceedingOutputCapacity)
        {
            nbPaxExceedingOutputCapacity = 0;
            updateOccupationTableIntervalsByAccessType(occupationIntervalsFromTable, flightTotalNbPax, checkInOpeningTime,
                boardingGateClosingTime, boardingEnteringSpeedPaxPerMin, BOARDING_ROOM_INPUT_ACESS_TYPE, out nbPaxExceedingOutputCapacity);
        }

        private void updateOccupationTableIntervalsWithOutputNb(List<OccupationInterval> occupationIntervalsFromTable,
            int flightTotalNbPax, DateTime boardingGateOpeningTime, DateTime boardingGateClosingTime, int boardingExitingSpeedPaxPerMin,
            out double nbPaxExceedingOutputCapacity)
        {
            nbPaxExceedingOutputCapacity = 0;
            updateOccupationTableIntervalsByAccessType(occupationIntervalsFromTable, flightTotalNbPax, boardingGateOpeningTime,
                boardingGateClosingTime, boardingExitingSpeedPaxPerMin, BOARDING_ROOM_OUTPUT_ACESS_TYPE, out nbPaxExceedingOutputCapacity);
        }

        private const string BOARDING_ROOM_INPUT_ACESS_TYPE = "EnterBoardingRoom";
        private const string BOARDING_ROOM_OUTPUT_ACESS_TYPE = "ExitBoardingRoom";
        private void updateOccupationTableIntervalsByAccessType(List<OccupationInterval> occupationIntervalsFromTable,
            int flightTotalNbPax, DateTime paxAccessStartTime, DateTime paxAccessEndTime, int boardingSpeedPaxPerMin,
            string accessType, out double nbPaxExceedingOutputCapacity)
        {
            nbPaxExceedingOutputCapacity = 0;
            TimeInterval accessTimeInterval = new TimeInterval(paxAccessStartTime, paxAccessEndTime);
            double nbPaxAccumulated = 0;
            foreach (OccupationInterval tableOccupationInterval in occupationIntervalsFromTable)
            {
                DateTime startTime = accessTimeInterval.fromDate;
                DateTime endTime = accessTimeInterval.toDate.AddMinutes(-1);
                TimeInterval accessTimeIntervalExcludingLastMinute = new TimeInterval(startTime, endTime);
                
                if (tableOccupationInterval.timeInterval.intersectsTimeInterval(accessTimeIntervalExcludingLastMinute)
                    && tableOccupationInterval.timeInterval.toDate != accessTimeInterval.fromDate)
                {
                    if (accessType == BOARDING_ROOM_INPUT_ACESS_TYPE)
                    {
                        tableOccupationInterval.nbFlights++;
                    }                    
                    int maxNbPaxAllowed = boardingSpeedPaxPerMin * (int)tableOccupationInterval.timeInterval.timeIntervalLengthInMinutes;
                    int accessingPaxNb = maxNbPaxAllowed;
                    if (flightTotalNbPax > 0)
                    {
                        if (flightTotalNbPax < maxNbPaxAllowed)
                        {
                            accessingPaxNb = flightTotalNbPax;
                            if (accessType == BOARDING_ROOM_INPUT_ACESS_TYPE)
                            {
                                tableOccupationInterval.nbIn += accessingPaxNb;
                                nbPaxAccumulated += accessingPaxNb;
                                tableOccupationInterval.nbInCumul += nbPaxAccumulated;
                            }
                            else if (accessType == BOARDING_ROOM_OUTPUT_ACESS_TYPE)
                            {
                                tableOccupationInterval.nbOut += accessingPaxNb;
                                nbPaxAccumulated += accessingPaxNb;
                                tableOccupationInterval.nbOutCumul += nbPaxAccumulated;
                            }
                            flightTotalNbPax = 0;
                        }
                        else
                        {
                            if (accessType == BOARDING_ROOM_INPUT_ACESS_TYPE)
                            {
                                tableOccupationInterval.nbIn += accessingPaxNb;
                                nbPaxAccumulated += accessingPaxNb;
                                tableOccupationInterval.nbInCumul += nbPaxAccumulated;
                            }
                            else if (accessType == BOARDING_ROOM_OUTPUT_ACESS_TYPE)
                            {
                                tableOccupationInterval.nbOut += accessingPaxNb;
                                nbPaxAccumulated += accessingPaxNb;
                                tableOccupationInterval.nbOutCumul += nbPaxAccumulated;
                            }
                            flightTotalNbPax = flightTotalNbPax - maxNbPaxAllowed;
                        }
                    }
                    else
                    {
                        if (accessType == BOARDING_ROOM_INPUT_ACESS_TYPE)
                        {
                            tableOccupationInterval.nbInCumul += nbPaxAccumulated;
                        }
                        else if (accessType == BOARDING_ROOM_OUTPUT_ACESS_TYPE)
                        {
                            tableOccupationInterval.nbOutCumul += nbPaxAccumulated;
                        }
                    }
                }
            }
            nbPaxExceedingOutputCapacity = flightTotalNbPax;
        }

        private void updateOccupationTableIntervalsWithIntegerInputValues(List<OccupationInterval> occupationIntervalsFromTable)
        {
            double partialPaxNb = 0;
            for (int i = 0; i < occupationIntervalsFromTable.Count; i++)
            {
                OccupationInterval tableOccupationInterval = occupationIntervalsFromTable[i];
                int nbPax = (int)Math.Floor(tableOccupationInterval.nbIn);
                bool hasPartialPax = (tableOccupationInterval.nbIn - Math.Floor(tableOccupationInterval.nbIn)) > 0;

                if (tableOccupationInterval.nbIn == 0 && partialPaxNb > 0
                    && i > 0)
                {
                    OccupationInterval previousInterval = occupationIntervalsFromTable[i - 1];
                    previousInterval.nbIn++;
                    partialPaxNb = 0;
                }
                if (partialPaxNb >= 1)
                {
                    nbPax++;
                    partialPaxNb = partialPaxNb - 1;
                }
                
                if (hasPartialPax)
                {
                    partialPaxNb += tableOccupationInterval.nbIn - Math.Floor(tableOccupationInterval.nbIn);                                        
                }
                tableOccupationInterval.nbIn = nbPax;
            }
        }

        private class OccupationInterval
        {
            internal TimeInterval timeInterval = new TimeInterval(DateTime.MinValue, DateTime.MaxValue);
            internal double slotEndOccupation { get; set; }
            internal double minOccupation = Double.MaxValue;
            internal double avgOccupation { get; set; }
            internal double maxOccupation = Double.MinValue;
            internal double showUpPaxPreInputNb { get; set; }
            internal double nbIn { get; set; }
            internal double nbInCumul { get; set; }
            internal double nbOut { get; set; }
            internal double nbOutCumul { get; set; }
            internal int nbFlights { get; set; }

            public OccupationInterval(DateTime _startTime,
                DateTime _endTime, double _nbIn)
            {
                timeInterval.fromDate = _startTime;
                timeInterval.toDate = _endTime;
                nbIn = _nbIn;
            }
        }

        public class OutputOccupationFlight
        {
            public Flight flight { get; set; }
            public double excessInputPaxNb { get; set; }
            public double excessOutputPaxNb { get; set; }

            public OutputOccupationFlight(Flight _flight,
                double _excessInputPaxNb, double _excessOutputPaxNb)
            {
                flight = _flight;
                excessInputPaxNb = _excessInputPaxNb;
                excessOutputPaxNb = _excessOutputPaxNb;
            }
        }
    }
}
