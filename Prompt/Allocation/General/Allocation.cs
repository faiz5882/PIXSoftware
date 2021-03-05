using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Liege;
using System.Data;
using SIMCORE_TOOL.Prompt.Dubai.P2S_Allocation;

namespace SIMCORE_TOOL.Prompt.Allocation.General
{
    class Allocation
    {
        TimeInterval scenarioInterval { get; set; }

        string allocationType { get; set; }
        string mainSortType { get; set; }
        string secondarySortType { get; set; }

        double timeStepMinutes { get; set; }
        double analysisRangeMinutes { get; set; }
        int delayBetweenConsecutiveFlightsMinutes { get; set; }

        bool useFlightPlanAsBasis { get; set; }

        /// <summary>
        /// Use this list to create the Available resources table.        
        /// </summary>
        internal List<Resource> initialAvailableResources = new List<Resource>();
                        
        internal List<Flight> unallocatedFlights = new List<Flight>();
        internal List<Flight> allocatedFlights = new List<Flight>();

        public Allocation(TimeInterval _scenarioInterval, string _allocationType,
            string _mainSortType, string _secondarySortType, double _timeStepMinutes,
            double _analysisRangeMintues, int _delayBetweenConsecutiveFlightsMinutes, 
            bool _useFlightPlanAsBasis)
        {
            scenarioInterval = _scenarioInterval;
            
            allocationType = _allocationType;
            mainSortType = _mainSortType;
            secondarySortType = _secondarySortType;
            
            timeStepMinutes = _timeStepMinutes;
            analysisRangeMinutes = _analysisRangeMintues;
            delayBetweenConsecutiveFlightsMinutes = _delayBetweenConsecutiveFlightsMinutes;

            useFlightPlanAsBasis = _useFlightPlanAsBasis;
        }

        internal List<Flight> getFlightsFromFilteredFlightPlan(DataTable filteredFlightPlan)
        {
            List<Flight> flightsFromFilteredFlightPlan = new List<Flight>();
            if (filteredFlightPlan == null || filteredFlightPlan.Rows.Count == 0)
                return flightsFromFilteredFlightPlan;

            #region Column indexes
            List<int> fpIndexes = new List<int>();
            int flightIdColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            fpIndexes.Add(flightIdColumnIndex);

            int flightDateColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            fpIndexes.Add(flightDateColumnIndex);

            int flightSTDColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
            fpIndexes.Add(flightSTDColumnIndex);

            int airlineCodeColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            fpIndexes.Add(airlineCodeColumnIndex);

            int flightNbColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            fpIndexes.Add(flightNbColumnIndex);

            int airportCodeColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            fpIndexes.Add(airportCodeColumnIndex);

            int flightCategoryColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            fpIndexes.Add(flightCategoryColumnIndex);

            int aircraftTypeColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
            fpIndexes.Add(aircraftTypeColumnIndex);

            int nbSeatsColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
            fpIndexes.Add(nbSeatsColumnIndex);

            int parkingStandTerminalColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
            fpIndexes.Add(parkingStandTerminalColumnIndex);
                        
            int user1ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
            fpIndexes.Add(user1ColumnIndex);

            int user2ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
            fpIndexes.Add(user2ColumnIndex);

            int user3ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
            fpIndexes.Add(user3ColumnIndex);

            int user4ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
            fpIndexes.Add(user4ColumnIndex);

            int user5ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
            fpIndexes.Add(user5ColumnIndex);

            if (!Utils.areValidColumnIndexes(fpIndexes))
                return flightsFromFilteredFlightPlan;
            #endregion

            foreach (DataRow row in filteredFlightPlan.Rows)
            {
                Flight flight = new Flight();

                #region Flight general data from the flight plan
                int flightId = -1;
                if (row[flightIdColumnIndex] != null)
                {
                    if (Int32.TryParse(row[flightIdColumnIndex].ToString(), out flightId))
                        flight.id = flightId;
                }
                DateTime date = DateTime.MinValue;
                if (row[flightDateColumnIndex] != null)
                {
                    if (DateTime.TryParse(row[flightDateColumnIndex].ToString(), out date))
                        flight.date = date;
                }
                TimeSpan time = TimeSpan.MinValue;
                if (row[flightSTDColumnIndex] != null)
                {
                    if (TimeSpan.TryParse(row[flightSTDColumnIndex].ToString(), out time))
                        flight.time = time;
                }
                if (row[airlineCodeColumnIndex] != null)
                {
                    flight.airlineCode = row[airlineCodeColumnIndex].ToString();
                }
                if (row[flightNbColumnIndex] != null)
                {
                    flight.flightNumber = row[flightNbColumnIndex].ToString();
                }
                if (row[airportCodeColumnIndex] != null)
                {
                    flight.airportCode = row[airportCodeColumnIndex].ToString();
                }
                if (row[flightCategoryColumnIndex] != null)
                {
                    flight.flightCategory = row[flightCategoryColumnIndex].ToString();
                }
                if (row[aircraftTypeColumnIndex] != null)
                {
                    flight.aircraftType = row[aircraftTypeColumnIndex].ToString();
                }
                int nbSeats = -1;
                if (row[nbSeatsColumnIndex] != null)
                {
                    if (Int32.TryParse(row[nbSeatsColumnIndex].ToString(), out nbSeats))
                        flight.nbSeats = nbSeats;
                }
                int parkingTerminalNb = -1;
                if (row[parkingStandTerminalColumnIndex] != null)
                {
                    if (Int32.TryParse(row[parkingStandTerminalColumnIndex].ToString(), out parkingTerminalNb))
                        flight.parkingTerminalNb = parkingTerminalNb;
                }
                if (row[user1ColumnIndex] != null)
                {
                    flight.user1 = row[user1ColumnIndex].ToString();
                }
                if (row[user2ColumnIndex] != null)
                {
                    flight.user2 = row[user2ColumnIndex].ToString();
                }
                if (row[user3ColumnIndex] != null)
                {
                    flight.user3 = row[user3ColumnIndex].ToString();
                }
                if (row[user4ColumnIndex] != null)
                {
                    flight.user4 = row[user4ColumnIndex].ToString();
                }
                if (row[user5ColumnIndex] != null)
                {
                    flight.user5 = row[user5ColumnIndex].ToString();
                }
                #endregion

                int minutesFromArrToDep = -1;
                if (Int32.TryParse(flight.user2, out minutesFromArrToDep))
                {
                    flight.minutesFromArrivalToDeparture = minutesFromArrToDep;
                    flight.occupiedInterval = getOccupationByFlightAndMinutesFromArrivalToDeparture(flight, flight.minutesFromArrivalToDeparture);
                    flightsFromFilteredFlightPlan.Add(flight);
                }
                else
                {
                    OverallTools.ExternFunctions.PrintLogFile("Allocation : Could not get flight from FP (down time could not be retrieved from User2). Flight Id = "
                        + flight.id + " User2 = " + flight.user2);
                }
            }

            return flightsFromFilteredFlightPlan;
        }

        private TimeInterval getOccupationByFlightAndMinutesFromArrivalToDeparture(Flight flight, double minutesFromArrivalToDeparture)
        {
            DateTime flightCompleteDate = flight.date.Add(flight.time);
            DateTime occupationStartDate = flightCompleteDate.AddMinutes(-minutesFromArrivalToDeparture);
            DateTime occupationEndDate = flightCompleteDate;
            TimeInterval occupationInterval = new TimeInterval(occupationStartDate, occupationEndDate);

            if (occupationInterval.fromDate < scenarioInterval.fromDate)
                occupationInterval.fromDate = scenarioInterval.fromDate;
            if (occupationInterval.toDate > scenarioInterval.toDate)
                occupationInterval.toDate = scenarioInterval.toDate;
            return occupationInterval;
        }

        internal List<Flight> sortFlights(List<Flight> flights, string mainSortType, string secondarySortType)
        {
            if (mainSortType == AllocationAssistant.SORT_TYPE_BY_STD
                && secondarySortType == AllocationAssistant.SORT_TYPE_BY_OCCUPATION_START)
            {
                flights = flights.OrderByDescending(f => f.allocationCode).ThenBy(f => f.date).ThenBy(f => f.time)
                                                    .ThenBy(f => f.occupiedInterval.fromDate).ToList();
            }
            else if (mainSortType == AllocationAssistant.SORT_TYPE_BY_OCCUPATION_START
                     && secondarySortType == AllocationAssistant.SORT_TYPE_BY_STD)
            {
                flights = flights.OrderByDescending(f => f.allocationCode).ThenBy(f => f.occupiedInterval.fromDate)
                                                    .ThenBy(f => f.date).ThenBy(f => f.time).ToList();
            }
            else if (mainSortType == AllocationAssistant.SORT_TYPE_BY_OCCUPATION_START
                     && secondarySortType == AllocationAssistant.SORT_TYPE_BY_OCCUPATION_START)
            {
                flights = flights.OrderByDescending(f => f.allocationCode).ThenBy(f => f.occupiedInterval.fromDate).ToList();
            }
            else if (mainSortType == AllocationAssistant.SORT_TYPE_BY_STD
                     && secondarySortType == AllocationAssistant.SORT_TYPE_BY_STD)
            {
                flights = flights.OrderByDescending(f => f.allocationCode).ThenBy(f => f.date).ThenBy(f => f.time).ToList();
            }
            return flights;
        }

        internal Dictionary<string, List<Resource>> getAvailableResources(TimeInterval scenarioInterval)
        {            
            //K = Parking Location Code / V = List with the parking stands. Ex: "CA" -> D1..D10, A1..A10            
            Dictionary<string, List<Resource>> parkingLocationsDictionary = new Dictionary<string, List<Resource>>();
            initialAvailableResources = new List<Resource>();
            int id = 1;
            string code = "";

            string zone = "";
            string allocationCode = "";
            string concourse = "";
            int terminal = -1;
            bool isRemote = false;
            List<TimeInterval> availableIntervals = new List<TimeInterval>();
            availableIntervals.Add(scenarioInterval);
            Resource res = null;

            #region No zone
            List<Resource> caParkings = new List<Resource>();
            //D1..D10
            for (int i = 1; i <= 10; i++)
            {
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CA;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                code = "D" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                caParkings.Add(res);
                id++;
            }
            //A1..A10
            for (int i = 1; i <= 10; i++)
            {
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CA;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                code = "A" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                caParkings.Add(res);
                id++;
            }
            parkingLocationsDictionary.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CA, caParkings);

            List<Resource> cbParkings = new List<Resource>();
            //B18, B21, B26LR
            for (int i = 18; i <= 26; i++)
            {
                if (i == 18 || i == 21)
                {
                    zone = "";
                    concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CB;
                    terminal = 3;
                    isRemote = false;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                    code = "B" + i + "_" + allocationCode;
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    cbParkings.Add(res);
                    id++;
                }
                else if (i == 26)
                {
                    zone = "";
                    concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CB;
                    terminal = 3;
                    isRemote = false;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                    code = "B" + i + "R" + "_" + allocationCode;
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    cbParkings.Add(res);
                    id++;

                    zone = "";
                    concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CB;
                    terminal = 3;
                    isRemote = false;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                    code = "B" + i + "L" + "_" + allocationCode;
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    cbParkings.Add(res);
                    id++;
                }
            }

            //F18..F22, F26, F27
            for (int i = 18; i <= 27; i++)
            {
                if (i >= 23 && i <= 25)
                    continue;
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CB;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                code = "F" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                cbParkings.Add(res);
                id++;
            }

            //B14..B17, B19, B20, B22..B25
            for (int i = 14; i <= 25; i++)
            {
                if (i == 18 || i == 21)
                    continue;
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CB;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                code = "B" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                cbParkings.Add(res);
                id++;
            }
            //F16..F17, F23..F25
            for (int i = 16; i <= 25; i++)
            {
                if (i >= 18 && i <= 22)
                    continue;
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CB;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                code = "F" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                cbParkings.Add(res);
                id++;
            }
            parkingLocationsDictionary.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CB, cbParkings);

            List<Resource> ccParkings = new List<Resource>();
            //B8..B12
            for (int i = 8; i <= 12; i++)
            {
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CC;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                code = "B" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                ccParkings.Add(res);
                id++;
            }
            //F2, F3, F6..F10, F12, F13
            for (int i = 2; i <= 13; i++)
            {
                if ((i >= 4 && i <= 5) || (i == 11))
                    continue;
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CC;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                code = "F" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                ccParkings.Add(res);
                id++;
            }

            //B1..B6
            for (int i = 1; i <= 6; i++)
            {
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CC;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                code = "B" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                ccParkings.Add(res);
                id++;
            }
            //F1, F4, F5, F14
            for (int i = 1; i <= 14; i++)
            {
                if (i == 2 || i == 3 || (i >= 6 && i <= 13))
                    continue;
                zone = "";
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CC;
                terminal = 3;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                code = "F" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                ccParkings.Add(res);
                id++;
            }
            parkingLocationsDictionary.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CC, ccParkings);
            #endregion

            #region CHARLIE
            List<Resource> remoteCharlieParkings = new List<Resource>();
            List<Resource> CDParkings = new List<Resource>();

            // C51, C53..C55
            for (int i = 51; i <= 55; i++)
            {
                if (i == 52)
                    continue;
                zone = Dubai.DubaiTools.PARKING_ZONE_CHARLIE;
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CD;
                terminal = 1;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                code = zone + "_" + "C" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                CDParkings.Add(res);
                id++;
            }

            //C49, C50, C52, C56..C64
            for (int i = 49; i <= 64; i++)
            {
                if (i == 51 || (i >= 53 && i <= 55))
                    continue;
                zone = Dubai.DubaiTools.PARKING_ZONE_CHARLIE;
                concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CD;
                terminal = 1;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                code = zone + "_" + "C" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                CDParkings.Add(res);
                id++;
            }

            //C48, C51LR, C53LR..C55LR
            for (int i = 48; i <= 55; i++)
            {
                if (i == 49 || i == 50 || i == 52)
                    continue;
                if (i == 48)
                {
                    zone = Dubai.DubaiTools.PARKING_ZONE_CHARLIE;
                    concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CD;
                    terminal = 1;
                    isRemote = false;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                    code = zone + "_" + "C" + i + "_" + allocationCode;
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    CDParkings.Add(res);
                    id++;
                }
                else
                {
                    zone = Dubai.DubaiTools.PARKING_ZONE_CHARLIE;
                    concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CD;
                    terminal = 1;
                    isRemote = false;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                    code = zone + "_" + "C" + i + "R" + "_" + allocationCode;
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    CDParkings.Add(res);
                    id++;

                    zone = Dubai.DubaiTools.PARKING_ZONE_CHARLIE;
                    concourse = Dubai.DubaiTools.PARKING_CONCOURSE_CD;
                    terminal = 1;
                    isRemote = false;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                    code = zone + "_" + "C" + i + "L" + "_" + allocationCode;
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    CDParkings.Add(res);
                    id++;
                }
            }

            // C24..C27
            for (int i = 24; i <= 27; i++)
            {

                zone = Dubai.DubaiTools.PARKING_ZONE_CHARLIE;
                concourse = "";
                terminal = -1;
                isRemote = true;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
                code = zone + "_" + "C" + i + "_" + allocationCode + "_" + "REMOTE";
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                remoteCharlieParkings.Add(res);
                id++;
            }

            // C1..C4, C28, C29, C30R, C31..C43            
            for (int i = 1; i <= 43; i++)
            {
                if (i >= 5 && i <= 27)
                    continue;

                zone = Dubai.DubaiTools.PARKING_ZONE_CHARLIE;
                concourse = "";
                terminal = -1;
                isRemote = true;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                if (i == 30)
                    code = zone + "_" + "C" + i + "R_" + allocationCode + "_" + "REMOTE";
                else
                    code = zone + "_" + "C" + i + "_" + allocationCode + "_" + "REMOTE";
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                remoteCharlieParkings.Add(res);
                id++;                
            }

            // C18..C23
            for (int i = 18; i <= 23; i++)
            {
                zone = Dubai.DubaiTools.PARKING_ZONE_CHARLIE;
                concourse = "";
                terminal = -1;
                isRemote = true;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                code = zone + "_" + "C" + i + "_" + allocationCode + "_" + "REMOTE";
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                remoteCharlieParkings.Add(res);
                id++;
            }
            
            parkingLocationsDictionary.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_CHARLIE, remoteCharlieParkings);
            parkingLocationsDictionary.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CD, CDParkings);
            #endregion

            #region GOLF
            List<Resource> remoteGolfParkings = new List<Resource>();

            // G1..G22
            for (int i = 1; i <= 22; i++)
            {                
                zone = Dubai.DubaiTools.PARKING_ZONE_GOLF;
                concourse = "";
                terminal = -1;
                isRemote = true;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                code = zone + "_" + "G" + i + "_" + allocationCode + "_REMOTE";
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                remoteGolfParkings.Add(res);
                id++;
            }
            parkingLocationsDictionary.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_GOLF, remoteGolfParkings);
            #endregion

            #region ECHO
            List<Resource> echoParkings = new List<Resource>();
            List<Resource> remoteEchoParkings = new List<Resource>();
                        
            //E1..E3, E19..E21
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 4 && i <= 18)
                    continue;
                zone = Dubai.DubaiTools.PARKING_ZONE_ECHO;
                concourse = "";
                terminal = 2;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                code = zone + "_" + "E" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                echoParkings.Add(res);
                id++;
            }

            //E4..E18
            for (int i = 4; i <= 18; i++)
            {
                zone = Dubai.DubaiTools.PARKING_ZONE_ECHO;
                concourse = "";
                terminal = 2;
                isRemote = false;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                code = zone + "_" + "E" + i + "_" + allocationCode;
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                echoParkings.Add(res);
                id++;
            }

            //E24..E26, E31, E32, E44..E46
            for (int i = 24; i <= 46; i++)
            {
                if (i >= 27 && i <= 30)
                    continue;
                if (i >= 33 && i <= 43)
                    continue;
                zone = Dubai.DubaiTools.PARKING_ZONE_ECHO;
                concourse = "";
                terminal = -1;
                isRemote = true;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E;
                code = zone + "_" + "E" + i + "_" + allocationCode + "_REMOTE";
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                remoteEchoParkings.Add(res);
                id++;
            }
                                    
            //E28
            zone = Dubai.DubaiTools.PARKING_ZONE_ECHO;
            concourse = "";
            terminal = -1;
            isRemote = true;
            allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F;
            code = zone + "_" + "E" + 28 + "_" + allocationCode + "_REMOTE";
            res = new Resource(28, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
            initialAvailableResources.Add(res);
            remoteEchoParkings.Add(res);
            id++;

            //E22..E23, E27..E30, E33..E34, E36..E43, E24LR..E26LR, E31LR..E32LR, E44LR..E46LR
            for (int i = 22; i <= 46; i++)
            {
                if (i == 35)
                    continue;

                if ((i >= 22 && i <= 23) || (i >= 27 && i <= 30)
                    || (i >= 33 && i <= 34) || (i >= 36 && i <= 43))
                {
                    zone = Dubai.DubaiTools.PARKING_ZONE_ECHO;
                    concourse = "";
                    terminal = -1;
                    isRemote = true;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                    code = zone + "_" + "E" + i + "_" + allocationCode + "_REMOTE";
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    remoteEchoParkings.Add(res);
                    id++;
                }
                else
                {
                    zone = Dubai.DubaiTools.PARKING_ZONE_ECHO;
                    concourse = "";
                    terminal = -1;
                    isRemote = true;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                    code = zone + "_" + "E" + i + "R" + "_" + allocationCode + "_REMOTE";
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    remoteEchoParkings.Add(res);
                    id++;

                    zone = Dubai.DubaiTools.PARKING_ZONE_ECHO;
                    concourse = "";
                    terminal = -1;
                    isRemote = true;
                    allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                    code = zone + "_" + "E" + i + "L" + "_" + allocationCode + "_REMOTE";
                    res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                    initialAvailableResources.Add(res);
                    remoteEchoParkings.Add(res);
                    id++;
                }                                                
            }
            //Q1..Q11
            for (int i = 1; i <= 11; i++)
            {
                zone = Dubai.DubaiTools.PARKING_ZONE_ECHO;
                concourse = "";
                terminal = -1;
                isRemote = true;
                allocationCode = Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C;
                code = zone + "_" + "Q" + i + "_" + allocationCode + "_REMOTE";
                res = new Resource(id, code, zone, concourse, allocationCode, terminal, isRemote, availableIntervals);
                initialAvailableResources.Add(res);
                remoteEchoParkings.Add(res);
                id++;
            }
            parkingLocationsDictionary.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_ECHO, echoParkings);
            parkingLocationsDictionary.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_ECHO, remoteEchoParkings);
            #endregion

            return parkingLocationsDictionary;
        }
        
        internal List<TerminalAllocationCode> getTerminalAllocationCodeRules()
        {
            List<TerminalAllocationCode> terminalAllocationCodeRules = new List<TerminalAllocationCode>();
            for (int terminalNb = 1; terminalNb <= 3; terminalNb++)
            {
                foreach (string allocationCode in Dubai.DubaiTools.PARKING_ALLOCATION_CODES_LIST)
                {
                    List<string> parkingLocations = new List<string>();
                    if (terminalNb == 3)
                    {
                        if (allocationCode == Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F)
                        {
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CA);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CB);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CC);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_GOLF);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_CHARLIE);
                        }
                        else if (allocationCode == Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E)
                        {
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CB);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CA);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CC);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_GOLF);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_CHARLIE);
                        }
                        else if (allocationCode == Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C)
                        {
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_GOLF);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_CHARLIE);
                        }
                        TerminalAllocationCode terminalAllocationCode
                            = new TerminalAllocationCode(terminalNb, allocationCode, parkingLocations);
                        terminalAllocationCodeRules.Add(terminalAllocationCode);
                    }
                    else if (terminalNb == 2)
                    {
                        if (allocationCode == Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F)
                        {
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_ECHO);                            
                        }
                        else if (allocationCode == Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E
                            || allocationCode == Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C)
                        {
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_ECHO);
                            parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_ECHO);
                        }
                        TerminalAllocationCode terminalAllocationCode
                            = new TerminalAllocationCode(terminalNb, allocationCode, parkingLocations);
                        terminalAllocationCodeRules.Add(terminalAllocationCode);
                    }
                    else if (terminalNb == 1)
                    {   
                        parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_CD);
                        parkingLocations.Add(Dubai.DubaiTools.PARKING_LOCATION_CODE_REMOTE_CHARLIE);                        
                        TerminalAllocationCode terminalAllocationCode
                            = new TerminalAllocationCode(terminalNb, allocationCode, parkingLocations);
                        terminalAllocationCodeRules.Add(terminalAllocationCode);
                    }
                }
            }            
            return terminalAllocationCodeRules;
        }

        internal void allocateFlights(List<Flight> flights, Dictionary<string, List<Resource>> availableResourcesDictionary,
            List<TerminalAllocationCode> terminalAllocationCodeRules)
        {
            if (flights == null || flights.Count == 0 || availableResourcesDictionary == null || availableResourcesDictionary.Count == 0)
                return;

            foreach (Flight flight in flights)
            {
                List<Resource> possibleResources 
                    = getPossibleResourcesByTerminalAndAllocationCode(flight.parkingTerminalNb, flight.allocationCode, 
                        availableResourcesDictionary, terminalAllocationCodeRules);

                List<Resource> possibleExtraResources = new List<Resource>();
                if (flight.allocationCode == Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E)
                {
                    possibleExtraResources 
                        = getPossibleResourcesByTerminalAndAllocationCode(flight.parkingTerminalNb, Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F,
                            availableResourcesDictionary, terminalAllocationCodeRules);
                    possibleResources.AddRange(possibleExtraResources);
                }
                else if (flight.allocationCode == Dubai.DubaiTools.PARKING_ALLOCATION_CODE_C)
                {
                    possibleExtraResources
                        = getPossibleResourcesByTerminalAndAllocationCode(flight.parkingTerminalNb, Dubai.DubaiTools.PARKING_ALLOCATION_CODE_E,
                            availableResourcesDictionary, terminalAllocationCodeRules);
                    possibleResources.AddRange(possibleExtraResources);
                    possibleExtraResources
                        = getPossibleResourcesByTerminalAndAllocationCode(flight.parkingTerminalNb, Dubai.DubaiTools.PARKING_ALLOCATION_CODE_F,
                            availableResourcesDictionary, terminalAllocationCodeRules);
                    possibleResources.AddRange(possibleExtraResources);
                }

                bool allocatedSuccessfully = false;
                foreach (Resource potentialResource in possibleResources)
                {
                    if (tryToAllocateFlightToResource(potentialResource, flight))
                    {                        
                        allocatedSuccessfully = true;
                        break;
                    }
                }
                if (allocatedSuccessfully)
                    allocatedFlights.Add(flight);
                else
                    unallocatedFlights.Add(flight);
            }
        }

        private List<Resource> getPossibleResourcesByTerminalAndAllocationCode(int terminalNb, string allocationCode,
            Dictionary<string, List<Resource>> availableResourcesDictionary, List<TerminalAllocationCode> terminalAllocationCodeRules)
        {
            List<Resource> possibleResources = new List<Resource>();
            List<string> parkingLocationCodes
                = getParkingLocationCodesByTerminalAndAllocationCode(terminalNb, allocationCode, terminalAllocationCodeRules);
            foreach (string parkingLocationCode in parkingLocationCodes)
            {
                if (availableResourcesDictionary.ContainsKey(parkingLocationCode))
                {
                    List<Resource> potentialResources = availableResourcesDictionary[parkingLocationCode];
                    foreach (Resource potentialResource in potentialResources)
                    {
                        if (potentialResource.allowedAllocationCode == allocationCode)
                            possibleResources.Add(potentialResource);
                    }
                }
            }
            return possibleResources;
        }

        private List<string> getParkingLocationCodesByTerminalAndAllocationCode(int terminalNb, string allocationCode, 
            List<TerminalAllocationCode> terminalAllocationCodeRules)
        {            
            foreach (TerminalAllocationCode rule in terminalAllocationCodeRules)
            {
                if (rule.terminalNb == terminalNb && rule.allocationCode == allocationCode)
                    return rule.parkingLocationCodes;
            }
            return new List<string>();
        }

        private bool tryToAllocateFlightToResource(Resource potentialResource, Flight flight)
        {
            if (potentialResource.hasAvailableInterval(flight.occupiedInterval))
            {
                allocateFlightToResource(potentialResource, flight);
                return true;                
            }
            return false;
        }

        private void allocateFlightToResource(Resource freeResource, Flight flight)
        {
            freeResource.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenConsecutiveFlightsMinutes);            
            flight.allocatedResources.Add(freeResource);
        }
    }
}
