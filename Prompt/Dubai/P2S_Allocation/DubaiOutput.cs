using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Allocation.General;
using System.Data;

namespace SIMCORE_TOOL.Prompt.Dubai.P2S_Allocation
{
    class DubaiOutput
    {
        internal const string PARKING_STAND_TABLE_NAME = "Available Resources";//"Parking Stands";
        const string PARKING_STAND_COLUMN_NAME_ID = "Id";
        const string PARKING_STAND_COLUMN_NAME_CODE = "Code";
        const string PARKING_STAND_COLUMN_NAME_ZONE = "Zone";
        const string PARKING_STAND_COLUMN_NAME_CONCOURSE = "Concourse";
        const string PARKING_STAND_COLUMN_NAME_TERMINAL = "Terminal";
        const string PARKING_STAND_COLUMN_NAME_ALLOCATION_CODE = "Allocation Code";
        const string PARKING_STAND_COLUMN_NAME_IS_REMOTE = "Is Remote";
        
        public static DataTable generateParkingStandsTable(List<Resource> parkingStands, int terminalNb)
        {
            DataTable parkingStandsTable = new DataTable(PARKING_STAND_TABLE_NAME);
            int idColumnIndex = parkingStandsTable.Columns.Count;
            parkingStandsTable.Columns.Add(PARKING_STAND_COLUMN_NAME_ID, typeof(Int32));
            int codeColumnIndex = parkingStandsTable.Columns.Count;
            parkingStandsTable.Columns.Add(PARKING_STAND_COLUMN_NAME_CODE, typeof(String));
            int zoneColumnIndex = parkingStandsTable.Columns.Count;
            parkingStandsTable.Columns.Add(PARKING_STAND_COLUMN_NAME_ZONE, typeof(String));
            int concourseColumnIndex = parkingStandsTable.Columns.Count;
            parkingStandsTable.Columns.Add(PARKING_STAND_COLUMN_NAME_CONCOURSE, typeof(String));
            int terminalColumnIndex = parkingStandsTable.Columns.Count;
            parkingStandsTable.Columns.Add(PARKING_STAND_COLUMN_NAME_TERMINAL, typeof(String));
            int allocationCodeColumnIndex = parkingStandsTable.Columns.Count;
            parkingStandsTable.Columns.Add(PARKING_STAND_COLUMN_NAME_ALLOCATION_CODE, typeof(String));
            int isRemoteColumnIndex = parkingStandsTable.Columns.Count;
            parkingStandsTable.Columns.Add(PARKING_STAND_COLUMN_NAME_IS_REMOTE, typeof(Boolean));

            foreach (Resource parkingStand in parkingStands)
            {
                if (terminalNb == 1)
                {
                    if (parkingStand.concourse == DubaiTools.PARKING_CONCOURSE_CA
                        || parkingStand.concourse == DubaiTools.PARKING_CONCOURSE_CB
                        || parkingStand.concourse == DubaiTools.PARKING_CONCOURSE_CC)
                    {
                        continue;
                    }
                    if (parkingStand.zone == DubaiTools.PARKING_ZONE_GOLF
                        || parkingStand.zone == DubaiTools.PARKING_ZONE_ECHO)
                    {
                        continue;
                    }
                    if (parkingStand.zone == DubaiTools.PARKING_ZONE_CHARLIE
                        && !parkingStand.isRemote && parkingStand.concourse != DubaiTools.PARKING_CONCOURSE_CD)
                    {
                        continue;
                    }
                }
                else if (terminalNb == 2)
                {
                    if (parkingStand.zone != DubaiTools.PARKING_ZONE_ECHO)
                    {
                        continue;
                    }
                }
                else if (terminalNb == 3)
                {
                    if (parkingStand.concourse == DubaiTools.PARKING_CONCOURSE_CD)
                    {
                        continue;
                    }
                    if (parkingStand.zone == DubaiTools.PARKING_ZONE_ECHO)
                    {
                        continue;
                    }
                    if ((parkingStand.zone == DubaiTools.PARKING_ZONE_CHARLIE
                        || parkingStand.zone == DubaiTools.PARKING_ZONE_GOLF)
                        && !parkingStand.isRemote)
                    {
                        continue;
                    }
                }
                else  if (terminalNb == Liege.AllocationAssistant.T1_T3)
                {
                    if (parkingStand.zone == DubaiTools.PARKING_ZONE_ECHO)
                    {
                        continue;
                    }
                    if (parkingStand.zone == DubaiTools.PARKING_ZONE_GOLF
                        && !parkingStand.isRemote)
                    {
                        continue;
                    }
                    if (parkingStand.zone == DubaiTools.PARKING_ZONE_CHARLIE
                        && !parkingStand.isRemote 
                        && parkingStand.concourse != DubaiTools.PARKING_LOCATION_CODE_CD)
                    {
                        continue;
                    }
                }
                DataRow row = parkingStandsTable.NewRow();
                row[idColumnIndex] = parkingStand.id;
                row[codeColumnIndex] = parkingStand.code;
                row[zoneColumnIndex] = parkingStand.zone;
                row[concourseColumnIndex] = parkingStand.concourse;
                if (parkingStand.allowedTerminalNb == -1)
                    row[terminalColumnIndex] = "Any";
                else
                    row[terminalColumnIndex] = parkingStand.allowedTerminalNb;
                row[allocationCodeColumnIndex] = parkingStand.allowedAllocationCode;
                row[isRemoteColumnIndex] = parkingStand.isRemote;
                parkingStandsTable.Rows.Add(row);
            }
            return parkingStandsTable;
        }

        internal const string ALLOCATION_RESULT_TABLE_NAME = "Allocation Result";
        const string ALLOCATION_RESULT_COLUMN_RESOURCE_ID = "Resource Id";
        const string ALLOCATION_RESULT_COLUMN_RESOURCE_NAME = "Resource Name";
        const string ALLOCATION_RESULT_COLUMN_RESOURCE_ZONE = "Zone";
        const string ALLOCATION_RESULT_COLUMN_RESOURCE_CONCOURSE = "Concourse";
        const string ALLOCATION_RESULT_COLUMN_RESOURCE_IS_REMOTE = "Is Remote Stand";
        const string ALLOCATION_RESULT_COLUMN_RESOURCE_ALLOCATION_CODE = "Resource Allocation Code";
        const string ALLOCATION_RESULT_COLUMN_RESOURCE_ALLOWED_TERMINAL = "Resource Allowed Terminal";
        const string ALLOCATION_RESULT_COLUMN_FLIGHT_ID = "Flight Id";
        const string ALLOCATION_RESULT_COLUMN_FLIGHT_NUMBER = "Flight Number";
        const string ALLOCATION_RESULT_COLUMN_FLIGHT_DATE = "Flight Date";
        const string ALLOCATION_RESULT_COLUMN_PARKING_TERMINAL = "Flight Parking Terminal";
        const string ALLOCATION_RESULT_COLUMN_FLIGHT_ALLOCATION_CODE = "Flight Allocation Code";
        const string ALLOCATION_RESULT_COLUMN_OCCUPATION_START_DAY = "Occupation Start Day of Week";
        const string ALLOCATION_RESULT_COLUMN_OCCUPATION_START = "Occupation Start";
        const string ALLOCATION_RESULT_COLUMN_OCCUPATION_END_DAY = "Occupation End Day of Week";
        const string ALLOCATION_RESULT_COLUMN_OCCUPATION_END = "Occupation End";

        public static DataTable generateAllocationResultsTable(List<Resource> resources)
        {
            DataTable allocationResultTable = new DataTable(ALLOCATION_RESULT_TABLE_NAME);

            int resourceIdColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_RESOURCE_ID, typeof(Int32));

            int resourceNameColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_RESOURCE_NAME, typeof(String));

            int zoneColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_RESOURCE_ZONE, typeof(String));

            int concourseColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_RESOURCE_CONCOURSE, typeof(String));

            int isRemoteColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_RESOURCE_IS_REMOTE, typeof(Boolean));

            int resourceAllocationCodeColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_RESOURCE_ALLOCATION_CODE, typeof(String));

            int resourceAllowedTerminalColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_RESOURCE_ALLOWED_TERMINAL, typeof(String));
                        
            int flightIdColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_FLIGHT_ID, typeof(Int32));

            int flightNumberColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_FLIGHT_NUMBER, typeof(String));

            int flightCompleteDateColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_FLIGHT_DATE, typeof(DateTime));

            int parkingTerminalColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_PARKING_TERMINAL, typeof(Int32));

            int allocationTypeColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_FLIGHT_ALLOCATION_CODE, typeof(String));
                        
            int occupationStartDayOfWeekColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_OCCUPATION_START_DAY, typeof(String));

            int occupationStartColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_OCCUPATION_START, typeof(DateTime));

            int occupationEndDayOfWeekColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_OCCUPATION_END_DAY, typeof(String));

            int occupationEndColumnIndex = allocationResultTable.Columns.Count;
            allocationResultTable.Columns.Add(ALLOCATION_RESULT_COLUMN_OCCUPATION_END, typeof(DateTime));

            foreach (Resource resource in resources)
            {
                foreach (FlightResourceAllocation allocationResult in resource.flightAllocations)
                {
                    DataRow row = allocationResultTable.NewRow();
                    row[resourceIdColumnIndex] = allocationResult.resource.id;
                    row[resourceNameColumnIndex] = allocationResult.resource.code;
                    row[zoneColumnIndex] = allocationResult.resource.zone;
                    row[concourseColumnIndex] = allocationResult.resource.concourse;
                    row[isRemoteColumnIndex] = allocationResult.resource.isRemote;
                    row[resourceAllocationCodeColumnIndex] = resource.allowedAllocationCode;
                    if (resource.allowedTerminalNb == -1)
                        row[resourceAllowedTerminalColumnIndex] = "Any";
                    else
                        row[resourceAllowedTerminalColumnIndex] = resource.allowedTerminalNb;
                    row[flightIdColumnIndex] = allocationResult.allocatedFlight.id;
                    row[flightNumberColumnIndex] = allocationResult.allocatedFlight.flightNumber;
                    row[flightCompleteDateColumnIndex] = allocationResult.allocatedFlight.date.Add(allocationResult.allocatedFlight.time);
                    row[parkingTerminalColumnIndex] = allocationResult.allocatedFlight.parkingTerminalNb;
                    row[allocationTypeColumnIndex] = allocationResult.allocatedFlight.allocationCode;
                    row[occupationStartDayOfWeekColumnIndex] = allocationResult.occupationInterval.fromDate.DayOfWeek.ToString();
                    row[occupationStartColumnIndex] = allocationResult.occupationInterval.fromDate;
                    row[occupationEndDayOfWeekColumnIndex] = allocationResult.occupationInterval.toDate.DayOfWeek.ToString();
                    row[occupationEndColumnIndex] = allocationResult.occupationInterval.toDate;
                    allocationResultTable.Rows.Add(row);
                }
            }
            DataView view = allocationResultTable.DefaultView;
            view.Sort = ALLOCATION_RESULT_COLUMN_FLIGHT_DATE + " ASC";
            allocationResultTable = view.ToTable();
            return allocationResultTable;
        }

        internal const string UNALLOCATED_FLIGHTS_TABLE_NAME = "Allocation Issues";//"Unallocated Flights";
        const string UNALLOCATED_FLIGHTS_COLUMN_FLIGHT_ID = "Flight Id";
        const string UNALLOCATED_FLIGHTS_COLUMN_FLIGHT_NUMBER = "Flight Number";
        const string UNALLOCATED_FLIGHTS_COLUMN_DATE = "Flight Date";
        const string UNALLOCATED_FLIGHTS_COLUMN_PARKING_TERMINAL = "Parking Terminal";
        const string UNALLOCATED_FLIGHTS_COLUMN_ALLOCATION_CODE = "Allocation Code";        
        const string UNALLOCATED_FLIGHTS_COLUMN_OCCUPATION_START = "Occupation Start";        
        const string UNALLOCATED_FLIGHTS_COLUMN_OCCUPATION_END = "Occupation End";
        const string UNALLOCATED_FLIGHTS_COLUMN_MINUTES_DIFFERENCE = "Minutes From Arrival to Departure";

        public static DataTable generateUnallocatedFlightsTable(List<Flight> unallocatedFlights)
        {
            DataTable unallocatedFlightsTable = new DataTable(UNALLOCATED_FLIGHTS_TABLE_NAME);

            int flightIdColumnIndex = unallocatedFlightsTable.Columns.Count;
            unallocatedFlightsTable.Columns.Add(UNALLOCATED_FLIGHTS_COLUMN_FLIGHT_ID, typeof(Int32));
            int flightNumberColumnIndex = unallocatedFlightsTable.Columns.Count;
            unallocatedFlightsTable.Columns.Add(UNALLOCATED_FLIGHTS_COLUMN_FLIGHT_NUMBER, typeof(String));
            int flightDateColumnIndex = unallocatedFlightsTable.Columns.Count;
            unallocatedFlightsTable.Columns.Add(UNALLOCATED_FLIGHTS_COLUMN_DATE, typeof(DateTime));
            int parkingTerminalColumnIndex = unallocatedFlightsTable.Columns.Count;
            unallocatedFlightsTable.Columns.Add(UNALLOCATED_FLIGHTS_COLUMN_PARKING_TERMINAL, typeof(Int32));
            int allocationCodeColumnIndex = unallocatedFlightsTable.Columns.Count;
            unallocatedFlightsTable.Columns.Add(UNALLOCATED_FLIGHTS_COLUMN_ALLOCATION_CODE, typeof(String));
            int occupationStartColumnIndex = unallocatedFlightsTable.Columns.Count;
            unallocatedFlightsTable.Columns.Add(UNALLOCATED_FLIGHTS_COLUMN_OCCUPATION_START, typeof(DateTime));
            int occupationEndColumnIndex = unallocatedFlightsTable.Columns.Count;
            unallocatedFlightsTable.Columns.Add(UNALLOCATED_FLIGHTS_COLUMN_OCCUPATION_END, typeof(DateTime));
            int minutesDifferenceColumnIndex = unallocatedFlightsTable.Columns.Count;
            unallocatedFlightsTable.Columns.Add(UNALLOCATED_FLIGHTS_COLUMN_MINUTES_DIFFERENCE, typeof(Int32));

            foreach (Flight flight in unallocatedFlights)
            {
                DataRow row = unallocatedFlightsTable.NewRow();
                row[flightIdColumnIndex] = flight.id;
                row[flightNumberColumnIndex] = flight.flightNumber;
                row[flightDateColumnIndex] = flight.date.Add(flight.time);
                row[parkingTerminalColumnIndex] = flight.parkingTerminalNb;
                row[allocationCodeColumnIndex] = flight.allocationCode;
                row[occupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                row[occupationEndColumnIndex] = flight.occupiedInterval.toDate;
                row[minutesDifferenceColumnIndex] = flight.minutesFromArrivalToDeparture;
                unallocatedFlightsTable.Rows.Add(row);
            }
            DataView view = unallocatedFlightsTable.DefaultView;
            view.Sort = UNALLOCATED_FLIGHTS_COLUMN_DATE + " ASC";
            unallocatedFlightsTable = view.ToTable();
            return unallocatedFlightsTable;
        }

        public static DataTable generateFlightPlanInformationTable(List<Flight> allocatedFlights)
        {
            DataTable flightPlanInformationTable = new DataTable(GlobalNames.FPI_TableName);

            #region Column Indexes
            int flightIdColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_ID, typeof(int));
            int flightDateColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_DATE, typeof(DateTime));
            int flightSTDColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_Column_STD, typeof(TimeSpan));
            int flightAirlineCodeColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_AirlineCode, typeof(String));
            int flightNumberColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_FlightN, typeof(String));
            int flightAirportCodeColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_AirportCode, typeof(String));
            int flightCategoryColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_FlightCategory, typeof(String));
            int flightAircraftTypeColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_AircraftType, typeof(String));
            int flightTSAColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_Column_TSA, typeof(Boolean));
            int flightNbSeatsColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_NbSeats, typeof(int));

            int flightNbPaxColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Nb_Passengers, typeof(double));
            int flightNbBagsColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Nb_Bags, typeof(double));
            int flightContainerSizeColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Container_Size, typeof(String));

            int firstDeskColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add("Parking First Desk", typeof(int));
            int lastDeskColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add("Parking Last Desk", typeof(int));
            int nbResourcesUsedColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_NbOfResourcesUsed, typeof(int));

            int openingColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add("Parking Opening Time", typeof(DateTime));
            int closingColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add("Parking Closing Time", typeof(DateTime));
            int terminalNbColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add("Parking Terminal Nb", typeof(int));

            int groundHandlerColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPAirline_GroundHandlers, typeof(String));
            int user1ColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User1, typeof(String));
            int user2ColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User2, typeof(String));
            int user3ColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User3, typeof(String));
            int user4ColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User4, typeof(String));
            int user5ColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.sFPD_A_Column_User5, typeof(String));

            int allocationTypeColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Allocation_Type, typeof(String));
            int resourceTypeColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_Resource_Type_Name, typeof(String));
            int calculationTypeColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_CalculationType, typeof(String));
            int octTableUsedColumnIndex = flightPlanInformationTable.Columns.Count;
            flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_OCTTableUsed, typeof(String));
            #endregion

            foreach (Flight flight in allocatedFlights)
            {
                DataRow row = flightPlanInformationTable.NewRow();
                row[flightIdColumnIndex] = flight.id;
                row[flightDateColumnIndex] = flight.date;
                row[flightSTDColumnIndex] = flight.time;
                row[flightAirlineCodeColumnIndex] = flight.airlineCode;
                row[flightNumberColumnIndex] = flight.flightNumber;
                row[flightAirportCodeColumnIndex] = flight.airportCode;
                row[flightCategoryColumnIndex] = flight.flightCategory;
                row[flightAircraftTypeColumnIndex] = flight.aircraftType;
                row[flightTSAColumnIndex] = flight.tsa;
                row[flightNbSeatsColumnIndex] = flight.nbSeats;

                Allocation.General.Resource allocatedResource = null;
                if (flight.allocatedResources.Count > 0)
                {
                    allocatedResource = flight.allocatedResources[0];
                    row[firstDeskColumnIndex] = allocatedResource.id;
                    row[lastDeskColumnIndex] = allocatedResource.id;
                }
                row[nbResourcesUsedColumnIndex] = 1;

                
                row[openingColumnIndex] = flight.occupiedInterval.fromDate;
                row[closingColumnIndex] = flight.occupiedInterval.toDate;
                
                row[terminalNbColumnIndex] = flight.parkingTerminalNb;

                row[user1ColumnIndex] = flight.user1;
                row[user2ColumnIndex] = flight.user2;
                row[user3ColumnIndex] = flight.user3;
                row[user4ColumnIndex] = flight.user4;
                row[user5ColumnIndex] = flight.user5;

                row[allocationTypeColumnIndex] = "Parking";
                row[resourceTypeColumnIndex] = "Parking";

                row[calculationTypeColumnIndex] = "";
                row[octTableUsedColumnIndex] = "";
                flightPlanInformationTable.Rows.Add(row);
            }
            DataView view = flightPlanInformationTable.DefaultView;
            view.Sort = GlobalNames.sFPD_A_Column_DATE + ", " + GlobalNames.sFPD_Column_STD + " ASC";
            flightPlanInformationTable = view.ToTable();
            return flightPlanInformationTable;
        }

        internal const string FLIGHT_PLAN_TABLE_NAME = "Filtered Departure Flight Plan";
        const string FLIGHT_PLAN_COLUMN_NAME_OCCUPATION_START = "Occupation Start";
        const string FLIGHT_PLAN_COLUMN_NAME_OCCUPATION_END = "Occupation End";
        internal static DataTable generateFlightPlanTable(List<Flight> filteredFlightsList)
        {
            DataTable flightPlan = new DataTable(FLIGHT_PLAN_TABLE_NAME);

            #region Column Indexes
            int flightIdColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_ID, typeof(String));

            int flightDateColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_DATE, typeof(DateTime));

            int flightTimeColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_Column_STD, typeof(TimeSpan));

            int airlineCodeColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_AirlineCode, typeof(String));

            int flightNumberColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_FlightN, typeof(String));

            int airportCodeColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_AirportCode, typeof(String));

            int flightCategoryColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_FlightCategory, typeof(String));

            int aircraftTypeColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_AircraftType, typeof(String));

            int nbSeatsColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_NbSeats, typeof(Int32));

            int parkingTerminalColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_TerminalParking, typeof(Int32));

            int user1ColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_User1, typeof(String));
            int user2ColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_User2, typeof(String));
            int user3ColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_User3, typeof(String));
            int user4ColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_User4, typeof(String));
            int user5ColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(GlobalNames.sFPD_A_Column_User5, typeof(String));

            int occupationStartColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(FLIGHT_PLAN_COLUMN_NAME_OCCUPATION_START, typeof(DateTime));
            int occupationEndColumnIndex = flightPlan.Columns.Count;
            flightPlan.Columns.Add(FLIGHT_PLAN_COLUMN_NAME_OCCUPATION_END, typeof(DateTime));
            #endregion

            foreach (Flight flight in filteredFlightsList)
            {
                DataRow row = flightPlan.NewRow();
                row[flightIdColumnIndex] = flight.id;
                row[flightDateColumnIndex] = flight.date;
                row[flightTimeColumnIndex] = flight.time;
                row[airlineCodeColumnIndex] = flight.airlineCode;
                row[flightNumberColumnIndex] = flight.flightNumber;
                row[airportCodeColumnIndex] = flight.airportCode;
                row[flightCategoryColumnIndex] = flight.flightCategory;
                row[aircraftTypeColumnIndex] = flight.aircraftType;
                row[nbSeatsColumnIndex] = flight.nbSeats;
                row[parkingTerminalColumnIndex] = flight.parkingTerminalNb;
                row[user1ColumnIndex] = flight.user1;
                row[user2ColumnIndex] = flight.user2;
                row[user3ColumnIndex] = flight.user3;
                row[user4ColumnIndex] = flight.user4;
                row[user5ColumnIndex] = flight.user5;
                row[occupationStartColumnIndex] = flight.occupiedInterval.fromDate;
                row[occupationEndColumnIndex] = flight.occupiedInterval.toDate;                
                flightPlan.Rows.Add(row);
            }
            return flightPlan;
        }

        internal const string ALLOCATION_PARAMETER_NAME_TERMINAL_NB = "Terminal Number";

        internal const string ALLOCATION_PARAMETER_NAME_MAX_ACCEPTED_DOWN_TIME = "Maximum accepted Down time (min)";
        internal const string ALLOCATION_PARAMETER_NAME_DOWN_TIME_AFTER_STA = "Down time after STA (min)";
        internal const string ALLOCATION_PARAMETER_NAME_DOWN_TIME_BEFORE_STD = "Down time before STD (min)";

        internal const string ALLOCATION_DATE_FORMAT_DUBAI = "dd/MM/yyyy HH:mm";
        public static DataTable generateAllocationParametersTable(Liege.AllocationAssistant allocationAssistant)
        {
            DataTable allocationParametersTable = new DataTable(PAX2SIM.ALLOCATION_PARAMETERS_TABLE_NAME);

            int nameColumnIndex = allocationParametersTable.Columns.Count;
            allocationParametersTable.Columns.Add(Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETERS_TABLE_COLUM_NAME, typeof(String));

            int valueColumnIndex = allocationParametersTable.Columns.Count;
            allocationParametersTable.Columns.Add(Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETERS_TABLE_COLUM_VALUE, typeof(String));

            addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_SCENARIO_NAME,
                allocationAssistant.scenarioName, nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_ALLOCATION_TYPE,
                allocationAssistant.allocationType, nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_START_DATE,
                allocationAssistant.fromDate.ToString(ALLOCATION_DATE_FORMAT_DUBAI), nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_END_DATE,
                allocationAssistant.toDate.ToString(ALLOCATION_DATE_FORMAT_DUBAI), nameColumnIndex, valueColumnIndex);
            string terminal = "T";
            if (allocationAssistant.terminalNb == -1)
                terminal = "All";
            else if (allocationAssistant.terminalNb == Liege.AllocationAssistant.T1_T3)
                terminal += "1&T3";
            else
                terminal += allocationAssistant.terminalNb;
            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_TERMINAL_NB,
                terminal, nameColumnIndex, valueColumnIndex);

            string flightProcessingOrderParameter = allocationAssistant.mainSortType;
            if (allocationAssistant.secondarySortType != "")
                flightProcessingOrderParameter += " and " + allocationAssistant.secondarySortType;
            addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_FLIGHT_PROCESSING_ORDER,
                                        flightProcessingOrderParameter, nameColumnIndex, valueColumnIndex);

            addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_TIME_STEP,
                            allocationAssistant.timeStepInMinutes.ToString(), nameColumnIndex, valueColumnIndex);
            addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_ANALYSIS_RANGE,
                            allocationAssistant.analysisRangeInMinutes.ToString(), nameColumnIndex, valueColumnIndex);

            addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_DELAY_BETWEEN_CONS_FLIGHTS,
                allocationAssistant.delayBetweenConsecutiveFlightsInMinutes.ToString(), nameColumnIndex, valueColumnIndex);

            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_MAX_ACCEPTED_DOWN_TIME,
                allocationAssistant.downTimeMaxAcceptedValue.ToString(), nameColumnIndex, valueColumnIndex);

            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_DOWN_TIME_AFTER_STA,
                allocationAssistant.downTimeAfterSTA.ToString(), nameColumnIndex, valueColumnIndex);

            addParameterInfoToTable(allocationParametersTable, ALLOCATION_PARAMETER_NAME_DOWN_TIME_BEFORE_STD,
                allocationAssistant.downTimeBeforeSTD.ToString(), nameColumnIndex, valueColumnIndex);

            if (allocationAssistant.fpdTable != null)
            {
                addParameterInfoToTable(allocationParametersTable, Prompt.Liege.AllocationOutput.ALLOCATION_PARAMETER_NAME_FPD_TABLE,
                                allocationAssistant.fpdTable.TableName, nameColumnIndex, valueColumnIndex);
            }

            allocationParametersTable.AcceptChanges();
            return allocationParametersTable;
        }

        private static void addParameterInfoToTable(DataTable allocationParametersTable,
            string parameterName, string parameterValue, int nameColumnIndex, int valueColumnIndex)
        {
            DataRow row = allocationParametersTable.NewRow();
            row[nameColumnIndex] = parameterName;
            row[valueColumnIndex] = parameterValue;
            allocationParametersTable.Rows.Add(row);
        }
    
    }
}
