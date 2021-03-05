using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIMCORE_TOOL.Prompt.Dubai;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class Allocation
    {
        internal const string CHECK_IN_ALLOCATION_TYPE = "Check-In Allocation";
        internal const string BOARDING_GATE_ALLOCATION_TYPE = "Boarding Gate Allocation";
        internal const string PARKING_ALLOCATION_TYPE = "Parking Allocation";

        internal const string CHECK_IN_RESOURCE_TYPE = "CheckIn";
        internal const string BOARDING_GATE_RESOURCE_TYPE = "Boarding Gate";
        internal const string PARKING_RESOURCE_TYPE = "Parking";

        internal const string CHECK_IN_FPI_ALLOC_TYPE = "CheckInAllocation";
        internal const string BOARDING_GATE_FPI_ALLOC_TYPE = "BoardingGateAllocation";
        internal const string PARKING_FPI_ALLOC_TYPE = "Parking";
        
        internal const int SORT_FIRST_BY_OCCUPATION_START_DATE = 1;
        internal const int SORT_FIRST_BY_DATE_AND_STD = 2;

        internal const string BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME = "Allocation Priority";
        private const int HIGH_ALLOCATION_PRIORITY = 1;
        private const int LOW_ALLOCATION_PRIORITY = 2;

        internal const string P20_PARKING_STAND_CODE = "P_20";
        internal const string P21_PARKING_STAND_CODE = "P_21";
        internal const string P22_PARKING_STAND_CODE = "P_22";
        internal const string P23_PARKING_STAND_CODE = "P_23";
        internal const string P24_PARKING_STAND_CODE = "P_24";
        internal const string P25_PARKING_STAND_CODE = "P_25";
        internal const string P26_PARKING_STAND_CODE = "P_26";
        internal const string P27_PARKING_STAND_CODE = "P_27";
        internal const string P28_PARKING_STAND_CODE = "P_28";
        internal const string P41_PARKING_STAND_CODE = "P_41";
        internal const string P42_PARKING_STAND_CODE = "P_42";
        internal const string P43_PARKING_STAND_CODE = "P_43";

        internal const string BG_1_BOARDING_GATE_CODE = "BG_1";
        internal const string BG_2_BOARDING_GATE_CODE = "BG_2";
        internal const string BG_3_BOARDING_GATE_CODE = "BG_3";
        internal const string BG_4_BOARDING_GATE_CODE = "BG_4";

        internal const string PARKING_CONTACT_AREA_RESOURCE_TYPE = "Contact Area";
        internal const string PARKING_REMOTE_RESOURCE_TYPE = "Remote";
        internal const string PARKING_BACKUP_RESOURCE_TYPE = "Backup";

        internal static List<string> contactAreaResourceCodes
            = new List<string>(new string[] { P23_PARKING_STAND_CODE, P24_PARKING_STAND_CODE, 
                                                P25_PARKING_STAND_CODE, P26_PARKING_STAND_CODE,
                                                P27_PARKING_STAND_CODE, P28_PARKING_STAND_CODE });
        internal static List<string> remoteResourceCodes
                    = new List<string>(new string[] { P20_PARKING_STAND_CODE, P21_PARKING_STAND_CODE, 
                                                        P22_PARKING_STAND_CODE });
        internal static List<string> backUpResourceCodes
                    = new List<string>(new string[] { P41_PARKING_STAND_CODE, P42_PARKING_STAND_CODE, 
                                                        P43_PARKING_STAND_CODE });

        List<Resource> contactAreaResources = new List<Resource>();

        private const string DEPARTURE_PREFIX = "D_";
        private const string ARRIVAL_PREFIX = "A_";

        List<string> mixedSchNonSchResourceCodes
            = new List<string>(new string[] { P20_PARKING_STAND_CODE, P21_PARKING_STAND_CODE, 
                                                P22_PARKING_STAND_CODE, P41_PARKING_STAND_CODE,
                                                    P42_PARKING_STAND_CODE,P43_PARKING_STAND_CODE });

        internal const int DUMMY_UNALLOCATED_RESOURCE_CODE_NB = 0;

        internal const int MIN_PARKING_STAND_CODE_NB = 20;
        internal const int MAX_PARKING_STAND_CODE_NB = 28;
        internal const int MIN_BACKUP_PARKING_STAND_CODE_NB = 41;
        internal const int MAX_BACKUP_PARKING_STAND_CODE_NB = 43;

        internal const int MIN_BOARDING_GATE_CODE_NB = 1;
        internal const int MAX_BOARDING_GATE_CODE_NB = 4;

        internal const int MIN_CHECK_IN_CODE_NB = 1;
        internal const int MAX_CHECK_IN_CODE_NB = 14;

        internal const string P0_BIN_NAME = "Parking_0";
        internal const string BG0_BIN_NAME = "Boarding Gate_0";
        internal const string CI0_BIN_NAME = "Check_In_0";

        GestionDonneesHUB2SIM donnees;
        string allocationType;
        string mainSortType;
        string secondarySortType;
        bool isDepartureAllocation;
        
        string parkingOCTTableName;
        bool useParkingOCTexceptions;

        string bgOCTTableName;
        bool useBGOCTexceptions;

        string ciOCTTableName;
        bool useCIOCTexceptions;

        string ciShowUpTableName;
        bool useCIShowUpExceptions;

        string loadingFactorsTableName;
        bool useLoadingFactorsExceptions;
        bool disableLoadingFactors;

        string aircraftTypeTableName;
        bool useAircraftTypeExceptions;

        string aircraftLinksTableName;
        string parkingPrioritiesTableName;

        string boardingGatesPrioritiesTableName;

        double samplingStep;
        double analysisRange;
        int delayBetweenFlights;        
        bool useFPasBasis;
        TimeInterval scenarioInterval;

        int boardingEnteringSpeedPaxPerMin;
        int boardingExitingSpeedPaxPerMin;
        //int delayBetweenBoardings;
        int lowerLargeAircraftLimit;

        internal ExceptionTable parkingOpeningClosingTimesExceptionTable;
        string parking_oct_opening_row_text = "";
        string parking_oct_closing_row_text = "";

        internal ExceptionTable bgOpeningClosingTimesExceptionTable;
        string bg_oct_opening_row_text = "";
        string bg_oct_closing_row_text = "";

        internal ExceptionTable ciOpeningClosingTimesExceptionTable;
        string ci_oct_opening_row_text = GlobalNames.sOCT_CI_Line_Opening;
        string ci_oct_closing_row_text = GlobalNames.sOCT_CI_Line_Closing;
        string ci_oct_partial_opening_row_text = GlobalNames.sOCT_CI_Line_PartialOpeningTime;
        string ci_oct_nb_allocated_row_text = GlobalNames.sOCT_CI_Line_Allocated;
        string ci_oct_nb_allocated_partial_row_text = GlobalNames.sOCT_CI_Line_NbStationsOpenedAtPartial;
        string ci_oct_nb_allocated_additional_row_text = GlobalNames.sOCT_CI_Line_NbAdditionalStationsForOverlappingFlights;

        internal ExceptionTable checkInShowUpExceptionTable;
        internal ExceptionTable loadingFactorsExceptionTable;
        internal ExceptionTable aircraftTypesExceptionTable;
        internal DataTable aircraftLinksTable;
        internal bool disableAircraftLinks;
        internal DataTable parkingPrioritiesTable;

        internal DataTable boardingGatesPrioritiesTable;

        internal List<Resource> initialAvailableResources = new List<Resource>();

        internal List<Flight> unallocatedFlights = new List<Flight>();
        internal List<Resource> backUpResources = new List<Resource>();
        internal List<Flight> brokeSchengenConstraintFlights = new List<Flight>();

        internal List<Flight> allocatedFlights = new List<Flight>();

        internal Dictionary<int, FlightLink> departureFlightlinkedArrivalDictionary = new Dictionary<int, FlightLink>();

        /// <summary>
        /// k = the id of the real flight to which the dummy flight corresponds
        /// v = the dummy flight with the desk allocations from the initial flight plan
        /// </summary>
        internal Dictionary<int, Flight> dummyFlightsDictionary = new Dictionary<int, Flight>();

        /// <summary>
        /// k = flight category
        /// v = list of parking priorities - for each parking an object that contains the parking code (P_x) and the priority
        /// </summary>
        private Dictionary<string, List<ResourcePriority>> flightCategoriesWithResourcePrioritiesDictionary = new Dictionary<string, List<ResourcePriority>>();

        internal Dictionary<string, CheckInOCTAirlineAirportException> checkInOCTAirlineAirportExceptionDictionary
                    = new Dictionary<string, CheckInOCTAirlineAirportException>();

        public Allocation(GestionDonneesHUB2SIM _donnees, string _allocationType, string _mainSortType, string _secondarySortType, bool _isDepartureAllocation,
            string _parkingOCTTableName, bool _useParkingOCTexceptions, string _bgOCTTableName, bool _useBGOCTexceptions,
            string _ciOCTTableName, bool _useCIOCTexceptions, string _ciShowUpTableName, bool _useciShowUpExceptions,
            string _loadingFactorsTableName, bool _useLoadingFactorsException, bool _disableLoadingFactors,
            string _aircraftTypeTableName, bool _useAircraftTypeExceptions,
            string _aircraftLinksTableName, bool _disableAircraftLinks,
            string _parkingPrioritiesTableName, string _boardingGatesPrioritiesTableName,
            double _samplingStep, double _analysisRange, int _delayBetweenFlights, bool _useFPasBasis, TimeInterval _scenarioInterval,
            int _boardingEnteringSpeedPaxPerMin, int _boardingExitingSpeedPaxPerMin,
            //int _delayBetweenBoardings,
            int _lowerLargeAircraftLimit)
        {
            donnees = _donnees;
            allocationType = _allocationType;
            mainSortType = _mainSortType;
            secondarySortType = _secondarySortType;

            isDepartureAllocation = _isDepartureAllocation;

            parkingOCTTableName = _parkingOCTTableName;
            useParkingOCTexceptions = _useParkingOCTexceptions;

            bgOCTTableName = _bgOCTTableName;
            useBGOCTexceptions = _useBGOCTexceptions;

            ciOCTTableName = _ciOCTTableName;
            useCIOCTexceptions = _useCIOCTexceptions;

            ciShowUpTableName = _ciShowUpTableName;
            useCIShowUpExceptions = _useciShowUpExceptions;

            loadingFactorsTableName = _loadingFactorsTableName;
            useLoadingFactorsExceptions = _useLoadingFactorsException;
            disableLoadingFactors = _disableLoadingFactors;

            aircraftTypeTableName = _aircraftTypeTableName;
            useAircraftTypeExceptions = _useAircraftTypeExceptions;

            aircraftLinksTableName = _aircraftLinksTableName;
            disableAircraftLinks = _disableAircraftLinks;
            parkingPrioritiesTableName = _parkingPrioritiesTableName;

            boardingGatesPrioritiesTableName = _boardingGatesPrioritiesTableName;

            samplingStep = _samplingStep;
            analysisRange = _analysisRange;
            delayBetweenFlights = _delayBetweenFlights;            
            useFPasBasis = _useFPasBasis;
            scenarioInterval = _scenarioInterval;

            boardingEnteringSpeedPaxPerMin = _boardingEnteringSpeedPaxPerMin;
            boardingExitingSpeedPaxPerMin = _boardingExitingSpeedPaxPerMin;
            //delayBetweenBoardings = _delayBetweenBoardings; same thing
            lowerLargeAircraftLimit = _lowerLargeAircraftLimit;

            setOpeningClosingTimeExceptionTables();
            setAircraftTypesExceptionTable();
            setLoadingFactorsExceptionTable();

            if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE))
            {
                setParkingAllocationSpecificInputTables();
                setResourcePriorities(parkingPrioritiesTable);
            }
            else if (allocationType.Equals(Allocation.BOARDING_GATE_ALLOCATION_TYPE))
            {
                setBoardingGateAllocationSpecificInputTables();
                setResourcePriorities(boardingGatesPrioritiesTable);
                setCheckInShowUpExceptionTable();
            }
        }

        private void setOpeningClosingTimeExceptionTables()
        {
            switch (allocationType)
            {
                case CHECK_IN_ALLOCATION_TYPE:
                    {
                        ciOpeningClosingTimesExceptionTable
                                = getExceptionTableByTableName(ciOCTTableName);
                        ciOpeningClosingTimesExceptionTable.UseException = useCIOCTexceptions;
                        ci_oct_opening_row_text = GlobalNames.sOCT_CI_Line_Opening;
                        ci_oct_closing_row_text = GlobalNames.sOCT_CI_Line_Closing;
                        break;
                    }
                case BOARDING_GATE_ALLOCATION_TYPE:
                    {
                        bgOpeningClosingTimesExceptionTable
                                = getExceptionTableByTableName(bgOCTTableName);
                        bgOpeningClosingTimesExceptionTable.UseException = useBGOCTexceptions;
                        bg_oct_opening_row_text = GlobalNames.sOCT_Board_Line_Opening;
                        bg_oct_closing_row_text = GlobalNames.sOCT_Board_Line_Closing;

                        ciOpeningClosingTimesExceptionTable
                                = getExceptionTableByTableName(ciOCTTableName);
                        ciOpeningClosingTimesExceptionTable.UseException = useCIOCTexceptions;
                        ci_oct_opening_row_text = GlobalNames.sOCT_CI_Line_Opening;
                        ci_oct_closing_row_text = GlobalNames.sOCT_CI_Line_Closing;
                        break;
                    }
                case PARKING_ALLOCATION_TYPE:
                    {
                        parkingOpeningClosingTimesExceptionTable
                                = getExceptionTableByTableName(parkingOCTTableName);
                        parkingOpeningClosingTimesExceptionTable.UseException = useParkingOCTexceptions;
                        parking_oct_opening_row_text = GlobalNames.sOCT_ParkingOpening;
                        parking_oct_closing_row_text = GlobalNames.sOCT_ParkingClosing;
                        break;
                    }
                default:
                    break;
            }
        }

        private void setAircraftTypesExceptionTable()
        {
            aircraftTypesExceptionTable
                        = getExceptionTableByTableName(aircraftTypeTableName);
            aircraftTypesExceptionTable.UseException = useAircraftTypeExceptions;
        }

        private void setCheckInShowUpExceptionTable()
        {
            if (ciShowUpTableName != "")
            {
                checkInShowUpExceptionTable
                    = getExceptionTableByTableName(ciShowUpTableName);
                ciOpeningClosingTimesExceptionTable.UseException = useCIShowUpExceptions;
            }
        }

        private void setLoadingFactorsExceptionTable()
        {
            if (loadingFactorsTableName != "")
            {
                loadingFactorsExceptionTable
                    = getExceptionTableByTableName(loadingFactorsTableName);
                loadingFactorsExceptionTable.UseException = useLoadingFactorsExceptions;
            }
        }

        private ExceptionTable getExceptionTableByTableName(string tableName)
        {
            NormalTable normalTable = donnees.GetTable("Input", tableName);
            if (normalTable is DataManagement.ExceptionTable)
                return (DataManagement.ExceptionTable)normalTable;
            return null;
        }

        private void setParkingAllocationSpecificInputTables()
        {
            if (aircraftLinksTableName != null 
                && aircraftLinksTableName != "")
            {
                if (donnees.getTable("Input", aircraftLinksTableName) != null)
                    aircraftLinksTable = donnees.getTable("Input", aircraftLinksTableName).Copy();
                aircraftLinksTable.TableName = PAX2SIM.AIRCRAFT_LINKS_TABLE_NAME;
            }
            if (parkingPrioritiesTableName != null
                && parkingPrioritiesTableName != "")
            {
                if (donnees.getTable("Input", parkingPrioritiesTableName) != null)
                    parkingPrioritiesTable = donnees.getTable("Input", parkingPrioritiesTableName).Copy();
                parkingPrioritiesTable.TableName = PAX2SIM.PARKING_PRIORITIES_TABLE_NAME;
            }
        }

        private void setBoardingGateAllocationSpecificInputTables()
        {
            if (boardingGatesPrioritiesTableName != null
                && boardingGatesPrioritiesTableName != "")
            {
                if (donnees.getTable("Input", boardingGatesPrioritiesTableName) != null)
                    boardingGatesPrioritiesTable = donnees.getTable("Input", boardingGatesPrioritiesTableName).Copy();
                boardingGatesPrioritiesTable.TableName = PAX2SIM.BOARDING_GATES_PRIORITIES_TABLE_NAME;
            }
        }
        
        private void setResourcePriorities(DataTable prioritiesTable)
        {
            if (prioritiesTable != null
                && prioritiesTable.Columns.Count > 1)
            {
                flightCategoriesWithResourcePrioritiesDictionary.Clear();
                for (int c = 1; c < prioritiesTable.Columns.Count; c++)
                {
                    DataColumn column = prioritiesTable.Columns[c];
                    string flightCategory = column.ColumnName;
                    List<ResourcePriority> resourcePriorities = new List<ResourcePriority>();
                    for (int r = 0; r < prioritiesTable.Rows.Count; r++)
                    {
                        DataRow row = prioritiesTable.Rows[r];
                        string resourceCode = "";
                        int priority = -1;
                        if (row.ItemArray != null && row.ItemArray.Length > 0 && row.ItemArray.Length > c
                            && row[0] != null && row[c] != null 
                            && Int32.TryParse(row[c].ToString(), out priority))
                        {
                            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                                resourceCode = getParkingCodeByParkingName(row[0].ToString());
                            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                                resourceCode = getBoardingGateCodeByBoardingGateName(row[0].ToString());

                            if (resourceCode != null && priority != -1)
                            {
                                ResourcePriority resourcePriority = new ResourcePriority(resourceCode, priority);
                                resourcePriorities.Add(resourcePriority);
                            }
                        }
                    }
                    resourcePriorities = resourcePriorities.OrderBy(pp => pp.priority).ToList();
                    flightCategoriesWithResourcePrioritiesDictionary.Add(flightCategory, resourcePriorities);
                }
            }
        }

        private string getParkingCodeByParkingName(string parkingName)
        {
            string parkingCode = null;
            if (parkingName != null && parkingName.IndexOf("_") != -1)
            {
                parkingCode = "P" + parkingName.Substring(parkingName.IndexOf("_"));
            }
            return parkingCode;
        }
        private string getBoardingGateCodeByBoardingGateName(string boardingGateName)
        {
            string boardingGateCode = null;
            if (boardingGateName != null && boardingGateName.IndexOf("_") != -1)
            {
                boardingGateCode = "BG" + boardingGateName.Substring(boardingGateName.IndexOf("_"));
            }
            return boardingGateCode;
        }

        internal DataTable getFilteredFlightPlan(DataTable flightPlan,
            TimeInterval scenarioInterval, int terminalNb)
        {
            if (flightPlan == null)
                return null;            

            DataTable filteredFlightPlan = flightPlan.Copy();
            filteredFlightPlan.TableName = PAX2SIM.FILTERED_FPD_TABLE_NAME;

            int flightIdColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int flightDateColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            int flightTimeColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_STD);

            int parkingTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
            int boardingGateTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int checkInTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
            
            if (flightIdColumnIndex == -1 || flightDateColumnIndex == -1 
                || flightTimeColumnIndex == -1 || parkingTerminalColumnIndex == -1
                || boardingGateTerminalColumnIndex == -1
                || (isDepartureAllocation && checkInTerminalColumnIndex == -1))
            {
                return flightPlan;
            }

            List<int> rowsToDelete = new List<int>();

            for (int i = 0; i < flightPlan.Rows.Count; i++)
            {
                DataRow row = flightPlan.Rows[i];

                DateTime flightDate = DateTime.MinValue;
                if (row[flightDateColumnIndex] != null)
                    flightDate = FonctionsType.getDate(row[flightDateColumnIndex]);
                if (flightDate != DateTime.MinValue)
                {
                    TimeSpan flightTime = TimeSpan.MinValue;
                    if (row[flightTimeColumnIndex] != null)
                        flightTime = FonctionsType.getTime(row[flightTimeColumnIndex]);
                    if (flightTime != TimeSpan.MinValue)
                    {
                        DateTime completeFlightDate = flightDate.Add(flightTime);
                        if (completeFlightDate < scenarioInterval.fromDate 
                            || completeFlightDate > scenarioInterval.toDate)
                        {
                            rowsToDelete.Add(i);
                        }
                    }
                }

                //if (terminalNb > 0)
                //{
                //    int currentTerminalNb = -1;
                //    if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                //        currentTerminalNb = getCheckedTerminal(parkingTerminalColumnIndex, row);
                //    else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                //        currentTerminalNb = getCheckedTerminal(boardingGateTerminalColumnIndex, row);
                //    else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
                //        currentTerminalNb = getCheckedTerminal(checkInTerminalColumnIndex, row);

                //    if (currentTerminalNb != terminalNb)
                //    {
                //        if (!rowsToDelete.Contains(i))
                //            rowsToDelete.Add(i);
                //    }
                //}

                //remove dummy flights => will not interfere with the allocation
                int flightId = -1;
                if (row[flightIdColumnIndex] != null
                    && (Int32.TryParse(row[flightIdColumnIndex].ToString(), out flightId)))
                {
                    if (flightId > FlightPlanUpdateWithAllocationResult.BIG_FLIGHTS_DUMMY_FP_ID_START)
                    {
                        if (!rowsToDelete.Contains(i))
                            rowsToDelete.Add(i);
                        
                        //save the dummy flight in a dictionary to have the desk allocations from initial FP
                        Flight dummyFlight = getDummyFlightByFPRow(filteredFlightPlan, row);
                        int realFlightId = flightId - FlightPlanUpdateWithAllocationResult.BIG_FLIGHTS_DUMMY_FP_ID_START;
                        if (dummyFlight != null && !dummyFlightsDictionary.ContainsKey(realFlightId))
                            dummyFlightsDictionary.Add(realFlightId, dummyFlight);
                    }
                }
            }

            for (int j = filteredFlightPlan.Rows.Count - 1; j >= 0; j--)
            {
                if (rowsToDelete.Contains(j))
                    filteredFlightPlan.Rows.RemoveAt(j);
            }

            DataTable enhancedFlightPlan = filteredFlightPlan;
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                enhancedFlightPlan = addPriorityInformation(filteredFlightPlan);
                enhancedFlightPlan = sortFlightPlan(enhancedFlightPlan);
            }
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                enhancedFlightPlan = updateNbSeatsFromAircraftTypes(enhancedFlightPlan);
                if (!disableLoadingFactors)
                {
                    filteredFlightPlan = updateFlightPlanWithNbOfPassengersUsingLoadingFactors(filteredFlightPlan);
                }
                enhancedFlightPlan = sortFlightPlan(enhancedFlightPlan);
            }
            else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                enhancedFlightPlan = sortFlightPlan(enhancedFlightPlan);
            }
            return enhancedFlightPlan;
        }

        internal DataTable sortFlightPlan(DataTable enhancedFlightPlan)
        {
            if (secondarySortType == AllocationAssistant.SORT_TYPE_BY_BODY_CATEGORY)
                enhancedFlightPlan = sortFlightPlanByBodyCategory(enhancedFlightPlan);
            else if (secondarySortType == AllocationAssistant.SORT_TYPE_BY_NB_SEATS)
                enhancedFlightPlan = sortFlightPlanByNbSeats(enhancedFlightPlan);
            else if (secondarySortType == AllocationAssistant.SORT_TYPE_BY_OCCUPATION_START)
                enhancedFlightPlan = sortFlightPlanByOccupationStartDate(enhancedFlightPlan);
            else if (secondarySortType == AllocationAssistant.SORT_TYPE_BY_STD)
                enhancedFlightPlan = sortFlightPlanBySTD(enhancedFlightPlan);

            if (mainSortType == AllocationAssistant.SORT_TYPE_BY_BODY_CATEGORY)
                enhancedFlightPlan = sortFlightPlanByBodyCategory(enhancedFlightPlan);
            else if (mainSortType == AllocationAssistant.SORT_TYPE_BY_NB_SEATS)
                enhancedFlightPlan = sortFlightPlanByNbSeats(enhancedFlightPlan);
            else if (mainSortType == AllocationAssistant.SORT_TYPE_BY_OCCUPATION_START)
                enhancedFlightPlan = sortFlightPlanByOccupationStartDate(enhancedFlightPlan);
            else if (mainSortType == AllocationAssistant.SORT_TYPE_BY_STD)
                enhancedFlightPlan = sortFlightPlanBySTD(enhancedFlightPlan);

            return enhancedFlightPlan;
        }

        private int getCheckedTerminal(int terminalColumnIndex, DataRow row)
        {
            int checkedTerminal = -1;
            if (row[terminalColumnIndex] != null)
                Int32.TryParse(row[terminalColumnIndex].ToString(), out checkedTerminal);
            return checkedTerminal;    
        }

        private Flight getDummyFlightByFPRow(DataTable filteredFlightPlan, DataRow row)
        {
            Flight dummyFlight = null;

            if (filteredFlightPlan == null || row == null)
                return dummyFlight;

            dummyFlight = new Flight();

            int ecoCheckInStartColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);             
            int ecoCheckInEndColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
            int boardingGateColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);
            int parkingStandColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);
                        
            int deskNb = -1;
            if (ecoCheckInStartColumnIndex != -1 && row[ecoCheckInStartColumnIndex] != null)
            {
                if (Int32.TryParse(row[ecoCheckInStartColumnIndex].ToString(), out deskNb))
                {
                    dummyFlight.flightPlanEcoCheckInStartDeskNb = deskNb;
                }
            }
            if (ecoCheckInEndColumnIndex != -1 && row[ecoCheckInEndColumnIndex] != null)
            {
                if (Int32.TryParse(row[ecoCheckInEndColumnIndex].ToString(), out deskNb))
                {
                    dummyFlight.flightPlanEcoCheckInEndDeskNb = deskNb;
                }
            }
            if (boardingGateColumnIndex != -1 && row[boardingGateColumnIndex] != null)
            {
                if (Int32.TryParse(row[boardingGateColumnIndex].ToString(), out deskNb))
                {
                    dummyFlight.flightPlanBoardingGateNb = deskNb;
                }
            }
            if (parkingStandColumnIndex != -1 && row[parkingStandColumnIndex] != null)
            {
                if (Int32.TryParse(row[parkingStandColumnIndex].ToString(), out deskNb))
                {
                    dummyFlight.flightPlanParkingStandNb = deskNb;
                }
            }
            
            return dummyFlight;
        }
        
        private DataTable addPriorityInformation(DataTable flightPlan)
        {
            if (flightPlan != null)
            {
                int flightIdColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                int airlineCodeColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                int flightCategoryColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                int aircraftTypeColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);

                int flightId = -1;
                string airlineCode = "";
                string flightCategory = "";
                string aircraftType = "";

                if (flightIdColumnIndex != -1 && airlineCodeColumnIndex != -1 
                    && flightCategoryColumnIndex != -1 && aircraftTypeColumnIndex != -1)
                {
                    int priorityColumnIndex = flightPlan.Columns.Count;
                    flightPlan.Columns.Add(BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME, typeof(String));

                    foreach (DataRow row in flightPlan.Rows)
                    {
                        if (row[flightIdColumnIndex] != null && row[airlineCodeColumnIndex] != null && row[flightCategoryColumnIndex] != null
                            && row[aircraftTypeColumnIndex] != null)
                        {
                            Int32.TryParse(row[flightIdColumnIndex].ToString(), out flightId);
                            airlineCode = row[airlineCodeColumnIndex].ToString();
                            flightCategory = row[flightCategoryColumnIndex].ToString();
                            aircraftType = row[aircraftTypeColumnIndex].ToString();

                            string prefix = ARRIVAL_PREFIX;
                            if (isDepartureAllocation)
                                prefix = DEPARTURE_PREFIX;
                            List<DataRow> aircraftTypesExceptionRows
                                = aircraftTypesExceptionTable.GetInformationsRows(0, prefix + flightId.ToString(),
                                                                                            airlineCode, flightCategory, aircraftType);
                            if (aircraftTypesExceptionRows != null && aircraftTypesExceptionRows.Count == 1)
                            {
                                int bodyColumnIndex = aircraftTypesExceptionRows[0].Table.Columns.IndexOf(GlobalNames.sFPAircraft_Body);
                                if (bodyColumnIndex != -1 && aircraftTypesExceptionRows[0][bodyColumnIndex] != null)
                                {
                                    row[priorityColumnIndex] = aircraftTypesExceptionRows[0][bodyColumnIndex].ToString();
                                }
                            }
                        }
                    }
                    flightPlan.AcceptChanges();
                }
            }
            return flightPlan;
        }

        private DataTable updateNbSeatsFromAircraftTypes(DataTable enhancedFlightPlan)
        {
            if (enhancedFlightPlan != null)
            {
                int flightIdColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                int airlineCodeColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                int flightCategoryColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                int aircraftTypeColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                int nbSeatsColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);

                int flightId = -1;
                string airlineCode = "";
                string flightCategory = "";
                string aircraftType = "";
                int nbSeats = -1;

                if (flightIdColumnIndex != -1 && airlineCodeColumnIndex != -1
                    && flightCategoryColumnIndex != -1 && aircraftTypeColumnIndex != -1
                    && nbSeatsColumnIndex != -1)
                {
                    foreach (DataRow row in enhancedFlightPlan.Rows)
                    {
                        if (row[flightIdColumnIndex] != null && row[airlineCodeColumnIndex] != null && row[flightCategoryColumnIndex] != null
                            && row[aircraftTypeColumnIndex] != null && row[nbSeatsColumnIndex] != null)
                        {
                            Int32.TryParse(row[flightIdColumnIndex].ToString(), out flightId);
                            airlineCode = row[airlineCodeColumnIndex].ToString();
                            flightCategory = row[flightCategoryColumnIndex].ToString();
                            aircraftType = row[aircraftTypeColumnIndex].ToString();
                            Int32.TryParse(row[nbSeatsColumnIndex].ToString(), out nbSeats);

                            if (nbSeats == 0)
                            {
                                string prefix = ARRIVAL_PREFIX;
                                if (isDepartureAllocation)
                                    prefix = DEPARTURE_PREFIX;
                                List<DataRow> aircraftTypesExceptionRows
                                    = aircraftTypesExceptionTable.GetInformationsRows(0, prefix + flightId.ToString(),
                                                                                                airlineCode, flightCategory, aircraftType);
                                if (aircraftTypesExceptionRows != null && aircraftTypesExceptionRows.Count == 1)
                                {
                                    int aircraftTypesNbSeatsColumnIndex = aircraftTypesExceptionRows[0].Table.Columns.IndexOf(GlobalNames.sFPAircraft_NumberSeats);
                                    if (aircraftTypesNbSeatsColumnIndex != -1 && aircraftTypesExceptionRows[0][aircraftTypesNbSeatsColumnIndex] != null)
                                    {
                                        row[nbSeatsColumnIndex] = aircraftTypesExceptionRows[0][aircraftTypesNbSeatsColumnIndex].ToString();
                                    }
                                }
                            }
                        }
                    }
                    enhancedFlightPlan.AcceptChanges();
                }
            }
            return enhancedFlightPlan;
        }

        private DataTable updateFlightPlanWithNbOfPassengersUsingLoadingFactors(DataTable enhancedFlightPlan)
        {
            if (enhancedFlightPlan != null)
            {
                int flightIdColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                int airlineCodeColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                int flightCategoryColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                int aircraftTypeColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                int nbSeatsColumnIndex = enhancedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);

                int flightId = -1;
                string airlineCode = "";
                string flightCategory = "";
                string aircraftType = "";
                int nbSeats = -1;

                if (flightIdColumnIndex != -1 && airlineCodeColumnIndex != -1
                    && flightCategoryColumnIndex != -1 && aircraftTypeColumnIndex != -1
                    && nbSeatsColumnIndex != -1)
                {
                    foreach (DataRow row in enhancedFlightPlan.Rows)
                    {
                        if (row[flightIdColumnIndex] != null && row[airlineCodeColumnIndex] != null && row[flightCategoryColumnIndex] != null
                            && row[aircraftTypeColumnIndex] != null && row[nbSeatsColumnIndex] != null)
                        {
                            Int32.TryParse(row[flightIdColumnIndex].ToString(), out flightId);
                            airlineCode = row[airlineCodeColumnIndex].ToString();
                            flightCategory = row[flightCategoryColumnIndex].ToString();
                            aircraftType = row[aircraftTypeColumnIndex].ToString();
                            
                            string prefix = ARRIVAL_PREFIX;
                            if (isDepartureAllocation)
                                prefix = DEPARTURE_PREFIX;
                            Dictionary<string, string> loadingFactorsExceptionsDictionary
                                = loadingFactorsExceptionTable.GetInformationsColumns(0, prefix + flightId.ToString(),
                                                                                            airlineCode, flightCategory);
                            if (loadingFactorsExceptionsDictionary != null && loadingFactorsExceptionsDictionary.ContainsKey(GlobalNames.sLFD_A_Line_Full))
                            {                               
                                double loadingPercent = -1;
                                if (Double.TryParse(loadingFactorsExceptionsDictionary[GlobalNames.sLFD_A_Line_Full].ToString(), out loadingPercent)
                                    && Int32.TryParse(row[nbSeatsColumnIndex].ToString(), out nbSeats))
                                {
                                    int nbPax = (int)Math.Floor(nbSeats * loadingPercent / 100);
                                    row[nbSeatsColumnIndex] = nbPax;
                                }
                            }
                        }
                    }
                    enhancedFlightPlan.AcceptChanges();
                }
            }
            return enhancedFlightPlan;
        }

        public DataTable sortFlightPlanByDateTimeAndAllocationPriority(DataTable filteredFlightPlan)
        {
            string time = GlobalNames.sFPD_Column_STD;
            string sortCriteria = GlobalNames.sFPD_A_Column_DATE + ", " + time + " ASC";

            DataView dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();

            sortCriteria = BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME + " DESC";

            dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();

            return filteredFlightPlan;
        }

        public DataTable sortFlightPlanByDateTimeAndNbSeats(DataTable filteredFlightPlan)
        {
            string time = GlobalNames.sFPD_Column_STD; 
            //if (isArrival)
                //time = GlobalNames.sFPA_Column_STA;
            string nbSeatsColumnName = GlobalNames.sFPD_A_Column_NbSeats;

            if (filteredFlightPlan.Columns.IndexOf(time) == -1
                || filteredFlightPlan.Columns.IndexOf(nbSeatsColumnName) == -1)
            {
                return filteredFlightPlan;
            }

            string sortCriteria = GlobalNames.sFPD_A_Column_DATE + ", " + time + " ASC";

            DataView dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();

            sortCriteria = nbSeatsColumnName + " DESC";

            dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();

            return filteredFlightPlan;
        }

        public DataTable sortFlightPlanBySTD(DataTable filteredFlightPlan)
        {
            string time = GlobalNames.sFPD_Column_STD;
            if (filteredFlightPlan.Columns.IndexOf(time) == -1)
            {
                return filteredFlightPlan;
            }
            string sortCriteria = GlobalNames.sFPD_A_Column_DATE + ", " + time + " ASC";
            DataView dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();
            return filteredFlightPlan;
        }

        public List<Resource> getResourcesListByAllocationType(TimeInterval scenarioInterval)
        {
            List<Resource> resources = new List<Resource>();

            if (allocationType == PARKING_ALLOCATION_TYPE)
                resources = getParkingStandsList(scenarioInterval);
            else if (allocationType == BOARDING_GATE_ALLOCATION_TYPE)
                resources = getBoardingGatesList(scenarioInterval);
            else if (allocationType == CHECK_IN_ALLOCATION_TYPE)
                resources = getCheckInStationsList(scenarioInterval);
            return resources;
        }
        
        private List<Resource> getParkingStandsList(TimeInterval scenarioInterval)
        {
            List<Resource> parkingStands = new List<Resource>();

            int id = 1;
            string name = "";
            string code = "";
            List<TimeInterval> initialAvailableIntervals = new List<TimeInterval>();

            int idCode = 20;
            int i = 1;
            for (i = 1; i <= 9; i++)
            {
                id = i;
                name = "Parking_" + idCode;
                code = "P_" + idCode;
                initialAvailableIntervals.Clear();
                initialAvailableIntervals.Add(scenarioInterval);

                Resource normalParkingStand = new Resource(id, name, code, idCode, initialAvailableIntervals);
                parkingStands.Add(normalParkingStand);

                idCode++;
            }            
            idCode = 41;
            for (i = 1; i <= 3; i++)
            {
                id = id + i;
                name = "Parking_" + idCode;
                code = "P_" + idCode;
                initialAvailableIntervals.Clear();
                initialAvailableIntervals = getSpecificTimeIntervalsFromGeneralInterval(scenarioInterval, 6, 20);

                Resource backUpParkingStand = new Resource(id, name, code, idCode, initialAvailableIntervals);
                parkingStands.Add(backUpParkingStand);
                backUpResources.Add(backUpParkingStand);

                idCode++;
            }
            return parkingStands;
        }

        private List<Resource> getBoardingGatesList(TimeInterval scenarioInterval)
        {
            List<Resource> boardingGates = new List<Resource>();            

            int id = 1;
            string name = "";
            string code = "";
            List<TimeInterval> initialAvailableIntervals = new List<TimeInterval>();

            Dictionary<int, Resource> piersDictionary = new Dictionary<int, Resource>();
            for (int i = 1; i <= 2; i++)
            {
                id = i;
                name = "Pier_" + i;
                code = "P_" + i;
                initialAvailableIntervals.Clear();
                initialAvailableIntervals.Add(scenarioInterval);
                Resource pier = new Resource(id, name, code, i, initialAvailableIntervals);
                piersDictionary.Add(i, pier);
            }
            for (int i = 1; i <= 4; i++)
            {
                id = i;
                name = "Boarding Gate_" + i;
                code = "BG_" + i;
                initialAvailableIntervals.Clear();
                initialAvailableIntervals.Add(scenarioInterval);
                Resource boardingGate = new Resource(id, name, code, i, initialAvailableIntervals);
                if (i <= 2)
                    boardingGate.pier = piersDictionary[1];
                else
                    boardingGate.pier = piersDictionary[2];
                boardingGates.Add(boardingGate);
            }
            return boardingGates;
        }
        
        private List<Resource> getCheckInStationsList(TimeInterval scenarioInterval)
        {
            List<Resource> checkInStations = new List<Resource>();

            int id = 1;
            string name = "";
            string code = "";
            List<TimeInterval> initialAvailableIntervals = new List<TimeInterval>();
                        
            for (int i = MIN_CHECK_IN_CODE_NB; i <= MAX_CHECK_IN_CODE_NB; i++)
            {
                id = i;
                name = "Check_In_" + i;
                code = "CI_" + i;
                initialAvailableIntervals.Clear();
                initialAvailableIntervals.Add(scenarioInterval);
                Resource checkIn = new Resource(id, name, code, i, initialAvailableIntervals);                
                checkInStations.Add(checkIn);
            }
            return checkInStations;
        }

        private List<TimeInterval> getSpecificTimeIntervalsFromGeneralIntervalByDaysAndTimeIntervals(TimeInterval generalInterval,
            List<DayWithTimeIntervals> daysWithTimeIntervals)
        {
            List<TimeInterval> initialAvailableIntervals = new List<TimeInterval>();

            foreach (DayWithTimeIntervals dayWithTimeIntervals in daysWithTimeIntervals)
            {
                foreach (DayWithTimeIntervals.HourMinuteTimeInterval hourMinuteInterval in dayWithTimeIntervals.hourMinuteIntervals)
                {
                    DateTime startDate = new DateTime(generalInterval.fromDate.Year, generalInterval.fromDate.Month,
                                                generalInterval.fromDate.Day, hourMinuteInterval.startHour, hourMinuteInterval.startMinute, 0);
                    while (startDate < generalInterval.toDate)
                    {
                        if (startDate.DayOfWeek == dayWithTimeIntervals.dayOfWeek)
                        {
                            DateTime endDate = new DateTime(startDate.Year, startDate.Month, startDate.Day,
                                                    hourMinuteInterval.endHour, hourMinuteInterval.endMinute, 0);
                            if (endDate <= generalInterval.toDate)
                            {
                                TimeInterval availableInterval = new TimeInterval(startDate, endDate);
                                initialAvailableIntervals.Add(availableInterval);
                            }
                            else
                            {
                                TimeInterval availableInterval = new TimeInterval(startDate, generalInterval.toDate);
                                initialAvailableIntervals.Add(availableInterval);
                            }
                            startDate = startDate.AddDays(1);
                        }
                        else
                        {
                            startDate = startDate.AddDays(1);
                        }
                    }
                }
            }
            initialAvailableIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
            return initialAvailableIntervals;
        }

        private List<TimeInterval> getSpecificTimeIntervalsFromGeneralInterval(TimeInterval generalInterval,
            int startDayHour,int endDayHour)
        {
            List<TimeInterval> initialAvailableIntervals = new List<TimeInterval>();

            DateTime startDate = new DateTime(generalInterval.fromDate.Year, generalInterval.fromDate.Month,
                                                generalInterval.fromDate.Day, startDayHour, 0, 0);

            while (startDate < generalInterval.toDate)
            {                
                DateTime endDate = new DateTime(startDate.Year, startDate.Month,
                                                    startDate.Day, endDayHour, 0, 0);
                if (endDate <= generalInterval.toDate)
                {
                    TimeInterval availableInterval = new TimeInterval(startDate, endDate);
                    initialAvailableIntervals.Add(availableInterval);
                }
                else
                {
                    TimeInterval availableInterval = new TimeInterval(startDate, generalInterval.toDate);
                    initialAvailableIntervals.Add(availableInterval);
                }
                startDate = startDate.AddDays(1);                
            }
            initialAvailableIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
            return initialAvailableIntervals;
        }

        public void setDepartureFlightLinkedArrivalDictionary()
        {
            departureFlightlinkedArrivalDictionary = new Dictionary<int, FlightLink>();
            if (aircraftLinksTable != null)
            {
                int arrivalFlightIdColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_FPAID);
                int arrivalFlightSTAColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_STA);
                int arrivalFlightNbColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_FPAName);

                int departureFlightIdColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_FPDID);
                int departureFlightSTDColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_STD);
                int departureFlightNbColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_FPDName);

                int durationColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_RotationDuration);

                if (arrivalFlightIdColumnIndex != -1 && arrivalFlightSTAColumnIndex != -1 && arrivalFlightNbColumnIndex != -1
                    && departureFlightIdColumnIndex != -1 && departureFlightSTDColumnIndex != -1 && departureFlightNbColumnIndex != -1
                    && durationColumnIndex != -1)
                {
                    int id = -1;
                    DateTime flightDate = DateTime.MinValue;                    
                    double duration = -1;

                    foreach (DataRow row in aircraftLinksTable.Rows)
                    {
                        bool valid = true;
                        LiegeTools.FlightInfoHolder arrivalFlight = new LiegeTools.FlightInfoHolder();
                        LiegeTools.FlightInfoHolder departureFlight = new LiegeTools.FlightInfoHolder();

                        if (row[arrivalFlightIdColumnIndex] != null && row[arrivalFlightSTAColumnIndex] != null
                            && row[arrivalFlightNbColumnIndex] != null)
                        {
                            if (!Int32.TryParse(row[arrivalFlightIdColumnIndex].ToString(), out id)
                                || !DateTime.TryParse(row[arrivalFlightSTAColumnIndex].ToString(), out flightDate))
                            {
                                valid = false;
                            }
                            else
                            {
                                arrivalFlight.id = id;
                                arrivalFlight.flightDate = flightDate;
                                arrivalFlight.flightNumber = row[arrivalFlightNbColumnIndex].ToString();
                            }
                        }
                        if (row[departureFlightIdColumnIndex] != null && row[departureFlightSTDColumnIndex] != null
                            && row[departureFlightNbColumnIndex] != null)
                        {
                            if (!Int32.TryParse(row[departureFlightIdColumnIndex].ToString(), out id)
                                || !DateTime.TryParse(row[departureFlightSTDColumnIndex].ToString(), out flightDate))
                            {
                                valid = false;
                            }
                            else
                            {
                                departureFlight.id = id;
                                departureFlight.flightDate = flightDate;
                                departureFlight.flightNumber = row[departureFlightNbColumnIndex].ToString();
                            }
                        }
                        if (row[durationColumnIndex] == null
                            || !Double.TryParse(row[durationColumnIndex].ToString(), out duration))
                        {
                            valid = false;
                        }
                        if (valid)
                        {
                            FlightLink flightLink = new FlightLink(arrivalFlight, departureFlight, duration);
                            departureFlightlinkedArrivalDictionary.Add(departureFlight.id, flightLink);
                        }
                    }
                }
            }
        }

        string FILTERED_FP_OCCUPATION_START_DATE_COLUMN_NAME = "Occupation Start Date";
        public List<Flight> getFlightsFromFilteredFlightPlan(DataTable filteredFlightPlan, GestionDonneesHUB2SIM DonneesEnCours)
        {
            List<Flight> flightsFromFilteredFlightPlan = new List<Flight>();

            DataTable arrivalFlightPlan = DonneesEnCours.getTable("Input", GlobalNames.FPATableName);

            if (filteredFlightPlan == null
                || (allocationType == Allocation.PARKING_ALLOCATION_TYPE && (parkingOpeningClosingTimesExceptionTable == null || arrivalFlightPlan == null))
                || (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE && (bgOpeningClosingTimesExceptionTable == null || ciOpeningClosingTimesExceptionTable == null)
                || (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE && ciOpeningClosingTimesExceptionTable == null))
                )
            {
                return flightsFromFilteredFlightPlan;
            }

            #region Flight Plan Indexes
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

            int noBSMColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
            if (!isDepartureAllocation)
                fpIndexes.Add(noBSMColumnIndex);

            int cbpColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
            if (!isDepartureAllocation)
                fpIndexes.Add(cbpColumnIndex);

            int tsaColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
            if (!isDepartureAllocation)
                fpIndexes.Add(tsaColumnIndex);

            #region Resources

            #region Arrival
            int arrivalGateColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate);
            if (!isDepartureAllocation)
                fpIndexes.Add(arrivalGateColumnIndex);

            int reclaimTerminalColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim);
            if (!isDepartureAllocation)
                fpIndexes.Add(reclaimTerminalColumnIndex);

            int reclaimDeskColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
            if (!isDepartureAllocation)
                fpIndexes.Add(reclaimDeskColumnIndex);

            int infeedTerminalColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
            if (!isDepartureAllocation)
                fpIndexes.Add(infeedTerminalColumnIndex);

            int arrivalInfeedStartColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject);
            if (!isDepartureAllocation)
                fpIndexes.Add(arrivalInfeedStartColumnIndex);

            int arrivalInfeedEndColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject);
            if (!isDepartureAllocation)
                fpIndexes.Add(arrivalInfeedEndColumnIndex);

            int transferInfeedColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);
            if (!isDepartureAllocation)
                fpIndexes.Add(transferInfeedColumnIndex);
            #endregion

            #region Departure
            int checkInTerminalColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
            if (isDepartureAllocation)
                fpIndexes.Add(checkInTerminalColumnIndex);
            
            int ecoCheckInStartColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
            if (isDepartureAllocation)
                fpIndexes.Add(ecoCheckInStartColumnIndex);
            int ecoCheckInEndColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
            if (isDepartureAllocation)
                fpIndexes.Add(ecoCheckInEndColumnIndex);
            int fbCheckInStartColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
            if (isDepartureAllocation)
                fpIndexes.Add(fbCheckInStartColumnIndex);
            int fbCheckInEndColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);
            if (isDepartureAllocation)
                fpIndexes.Add(fbCheckInEndColumnIndex);

            int ecoBagDropStartColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
            if (isDepartureAllocation)
                fpIndexes.Add(ecoBagDropStartColumnIndex);
            int ecoBagDropEndColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
            if (isDepartureAllocation)
                fpIndexes.Add(ecoBagDropEndColumnIndex);
            int fbBagDropStartColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
            if (isDepartureAllocation)
                fpIndexes.Add(fbBagDropStartColumnIndex);
            int fbBagDropEndColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);
            if (isDepartureAllocation)
                fpIndexes.Add(fbBagDropEndColumnIndex);

            int boardingGateColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);
            if (isDepartureAllocation) 
                fpIndexes.Add(boardingGateColumnIndex);

            int mupTerminalColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup);
            if (isDepartureAllocation)
                fpIndexes.Add(mupTerminalColumnIndex);
            int ecoMupStartColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
            if (isDepartureAllocation)
                fpIndexes.Add(ecoMupStartColumnIndex);
            int ecoMupEndColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
            if (isDepartureAllocation)
                fpIndexes.Add(ecoMupEndColumnIndex);
            int fbMupStartColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
            if (isDepartureAllocation)
                fpIndexes.Add(fbMupStartColumnIndex);
            int fbMupEndColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);
            if (isDepartureAllocation)
                fpIndexes.Add(fbMupEndColumnIndex);
            #endregion

            #region Mixed
            int gateTerminalColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            fpIndexes.Add(gateTerminalColumnIndex);

            int parkingTerminalColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
            fpIndexes.Add(parkingTerminalColumnIndex);

            int parkingStandColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);
            fpIndexes.Add(parkingStandColumnIndex);

            int runwayColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay);
            fpIndexes.Add(runwayColumnIndex);
            #endregion

            #endregion
            
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
                        
            int aircraftBodyCategoryColumnIndex = filteredFlightPlan.Columns.IndexOf(BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME);
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                fpIndexes.Add(aircraftBodyCategoryColumnIndex);
            }
            #endregion

            int firstResourceColumnIndex = -1;
            int lastResourceColumnIndex = -1;
            getAllocationResourceColumnIndexesFromFlightPlanByAllocationType(filteredFlightPlan,
                out firstResourceColumnIndex, out lastResourceColumnIndex);
            fpIndexes.Add(firstResourceColumnIndex);
            fpIndexes.Add(lastResourceColumnIndex);

            if (!areValidColumnIndexes(fpIndexes))
                return flightsFromFilteredFlightPlan;

            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                if (!disableAircraftLinks)
                    setDepartureFlightLinkedArrivalDictionary();
            }

            int occupationIntervalStartDateColumnIndex = filteredFlightPlan.Columns.Count;
            filteredFlightPlan.Columns.Add(FILTERED_FP_OCCUPATION_START_DATE_COLUMN_NAME, typeof(DateTime));

            foreach (DataRow row in filteredFlightPlan.Rows)
            {
                Flight flight = new Flight();

                #region gather flight info

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
                int ecoCheckInStartDeskNb = -1;
                if (row[ecoCheckInStartColumnIndex] != null)
                {
                    if (Int32.TryParse(row[ecoCheckInStartColumnIndex].ToString(), out ecoCheckInStartDeskNb))
                        flight.flightPlanEcoCheckInStartDeskNb = ecoCheckInStartDeskNb;
                }
                int ecoCheckInEndDeskNb = -1;
                if (row[ecoCheckInEndColumnIndex] != null)
                {
                    if (Int32.TryParse(row[ecoCheckInEndColumnIndex].ToString(), out ecoCheckInEndDeskNb))
                        flight.flightPlanEcoCheckInEndDeskNb = ecoCheckInEndDeskNb;
                }

                int boardingGateNb = -1;
                if (row[boardingGateColumnIndex] != null)
                {
                    if (Int32.TryParse(row[boardingGateColumnIndex].ToString(), out boardingGateNb))
                        flight.flightPlanBoardingGateNb = boardingGateNb;
                }

                int parkingStandNb = -1;
                if (row[parkingStandColumnIndex] != null)
                {
                    if (Int32.TryParse(row[parkingStandColumnIndex].ToString(), out parkingStandNb))
                        flight.flightPlanParkingStandNb = parkingStandNb;
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
                if (aircraftBodyCategoryColumnIndex != -1
                    && row[aircraftBodyCategoryColumnIndex] != null)
                {
                    flight.aircraftBodyCategory = row[aircraftBodyCategoryColumnIndex].ToString();
                }
                #endregion

                setFlightOccupationTimeInterval(flight);

                if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
                {
                    if (flight.entireAllocationOccupationNeed != null)
                        row[occupationIntervalStartDateColumnIndex] = flight.entireAllocationOccupationNeed.occupationInterval.fromDate;                    
                }
                else
                {
                    if (flight.occupiedInterval != null)
                        row[occupationIntervalStartDateColumnIndex] = flight.occupiedInterval.fromDate;
                }

                //allocation info
                int deskNb = -1;
                if (row[firstResourceColumnIndex] != null)
                {
                    if (Int32.TryParse(row[firstResourceColumnIndex].ToString(), out deskNb))
                    {
                        flight.fpAsBasisResourceToAllocateStartIndex = deskNb;
                    }
                }
                if (firstResourceColumnIndex == lastResourceColumnIndex
                    && dummyFlightsDictionary.ContainsKey(flightId))
                {
                    Flight dummyFlight = dummyFlightsDictionary[flightId];
                    if (dummyFlight != null)
                    {
                        if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE))
                            flight.fpAsBasisResourceToAllocateEndIndex = dummyFlight.flightPlanParkingStandNb;
                        else if (allocationType.Equals(Allocation.BOARDING_GATE_ALLOCATION_TYPE))
                            flight.fpAsBasisResourceToAllocateEndIndex = dummyFlight.flightPlanBoardingGateNb;
                    }
                }
                else
                {
                    if (row[lastResourceColumnIndex] != null)
                    {
                        if (Int32.TryParse(row[lastResourceColumnIndex].ToString(), out deskNb))
                        {
                            flight.fpAsBasisResourceToAllocateEndIndex = deskNb;
                        }
                    }
                }
            }
            filteredFlightPlan = sortFlightPlan(filteredFlightPlan);
            //if (sortType == SORT_FIRST_BY_DATE_AND_STD)
            //{
            //    if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            //    {
            //        filteredFlightPlan = sortFlightPlanByDateTimeAndAllocationPriority(filteredFlightPlan);
            //    }
            //    else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            //    {
            //        filteredFlightPlan = sortFlightPlanByDateTimeAndNbSeats(filteredFlightPlan);
            //    }
            //    else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            //    {
            //        filteredFlightPlan = sortFlightPlanBySTD(filteredFlightPlan);
            //    }
            //}
            //else if (sortType == SORT_FIRST_BY_OCCUPATION_START_DATE)
            //{
            //    if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            //    {
            //        filteredFlightPlan = sortFlightPlanByOccupationDateTimeAndBodyCategory(filteredFlightPlan);
            //    }
            //    else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            //    {
            //        filteredFlightPlan = sortFlightPlanByOccupationDateTimeAndNbSeats(filteredFlightPlan);
            //    }
            //    else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            //    {
            //        filteredFlightPlan = sortFlightPlanByOccupationStartDate(filteredFlightPlan);
            //    }
            //}

            foreach (DataRow row in filteredFlightPlan.Rows)
            {
                Flight flight = new Flight();

                #region gather flight info

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
                if (isDepartureAllocation)
                {
                    bool tsa = false;
                    if (row[tsaColumnIndex] != null
                        && Boolean.TryParse(row[tsaColumnIndex].ToString(), out tsa))
                    {
                        flight.tsa = tsa;
                    }

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
                if (aircraftBodyCategoryColumnIndex != -1 
                    && row[aircraftBodyCategoryColumnIndex] != null)
                {
                    flight.aircraftBodyCategory = row[aircraftBodyCategoryColumnIndex].ToString();
                }

                #region Mixed Resources
                int deskNb = -1;
                if (row[gateTerminalColumnIndex] != null
                    && Int32.TryParse(row[gateTerminalColumnIndex].ToString(), out deskNb))
                {
                    flight.flightPlanGateTerminalNb = deskNb;
                }
                if (row[parkingTerminalColumnIndex] != null
                    && Int32.TryParse(row[parkingTerminalColumnIndex].ToString(), out deskNb))
                {
                    flight.flightPlanParkingTerminalNb = deskNb;
                }
                if (row[parkingStandColumnIndex] != null
                    && Int32.TryParse(row[parkingStandColumnIndex].ToString(), out deskNb))
                {
                    flight.flightPlanParkingStandNb = deskNb;
                }
                if (row[runwayColumnIndex] != null
                    && Int32.TryParse(row[runwayColumnIndex].ToString(), out deskNb))
                {
                    flight.flightPlanRunwayNb = deskNb;
                }
                #endregion

                #region Departure Resources
                if (isDepartureAllocation)
                {
                    if (row[checkInTerminalColumnIndex] != null
                    && Int32.TryParse(row[checkInTerminalColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanCheckInTerminalNb = deskNb;
                    }
                    if (row[ecoCheckInStartColumnIndex] != null
                    && Int32.TryParse(row[ecoCheckInStartColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanEcoCheckInStartDeskNb = deskNb;
                    }
                    if (row[ecoCheckInEndColumnIndex] != null
                    && Int32.TryParse(row[ecoCheckInEndColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanEcoCheckInEndDeskNb = deskNb;
                    }
                    if (row[fbCheckInStartColumnIndex] != null
                    && Int32.TryParse(row[fbCheckInStartColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanFBCheckInStartDeskNb = deskNb;
                    }
                    if (row[fbCheckInEndColumnIndex] != null
                    && Int32.TryParse(row[fbCheckInEndColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanFBCheckInEndDeskNb = deskNb;
                    }

                    if (row[ecoBagDropStartColumnIndex] != null
                    && Int32.TryParse(row[ecoBagDropStartColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanEcoBagDropStartDeskNb = deskNb;
                    }
                    if (row[ecoBagDropEndColumnIndex] != null
                    && Int32.TryParse(row[ecoBagDropEndColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanEcoBagDropEndDeskNb = deskNb;
                    }
                    if (row[fbBagDropStartColumnIndex] != null
                    && Int32.TryParse(row[fbBagDropStartColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanFBBagDropStartDeskNb = deskNb;
                    }
                    if (row[fbBagDropEndColumnIndex] != null
                    && Int32.TryParse(row[fbBagDropEndColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanFBBagDropEndDeskNb = deskNb;
                    }

                    if (row[boardingGateColumnIndex] != null
                    && Int32.TryParse(row[boardingGateColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanBoardingGateNb = deskNb;
                    }

                    if (row[mupTerminalColumnIndex] != null
                    && Int32.TryParse(row[mupTerminalColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanMupTerminalNb = deskNb;
                    }
                    if (row[ecoMupStartColumnIndex] != null
                    && Int32.TryParse(row[ecoMupStartColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanEcoMupStartDeskNb = deskNb;
                    }
                    if (row[ecoMupEndColumnIndex] != null
                    && Int32.TryParse(row[ecoMupEndColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanEcoMupEndDeskNb = deskNb;
                    }
                    if (row[fbMupStartColumnIndex] != null
                    && Int32.TryParse(row[fbMupStartColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanFBMupStartDeskNb = deskNb;
                    }
                    if (row[fbMupEndColumnIndex] != null
                    && Int32.TryParse(row[fbMupEndColumnIndex].ToString(), out deskNb))
                    {
                        flight.flightPlanFBMupEndDeskNb = deskNb;
                    }
                }
                #endregion

                #endregion

                setFlightOccupationTimeInterval(flight);

                if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                {
                    if (flight.linkedFlight != null)
                        flight.linkedFlight = getArrivalFlightById(flight.linkedFlight.id, arrivalFlightPlan);
                }

                if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
                {
                    if (flight.entireAllocationOccupationNeed != null)
                        row[occupationIntervalStartDateColumnIndex] = flight.entireAllocationOccupationNeed.occupationInterval.fromDate;                    
                }
                else
                {
                    if (flight.occupiedInterval != null)                    
                        row[occupationIntervalStartDateColumnIndex] = flight.occupiedInterval.fromDate;
                }

                //allocation info                
                if (row[firstResourceColumnIndex] != null)
                {
                    if (Int32.TryParse(row[firstResourceColumnIndex].ToString(), out deskNb))
                    {
                        flight.fpAsBasisResourceToAllocateStartIndex = deskNb;
                    }
                }
                if (firstResourceColumnIndex == lastResourceColumnIndex
                    && dummyFlightsDictionary.ContainsKey(flightId))
                {
                    Flight dummyFlight = dummyFlightsDictionary[flightId];
                    if (dummyFlight != null)
                    {
                        if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE))
                        {
                            flight.fpAsBasisResourceToAllocateEndIndex = dummyFlight.flightPlanParkingStandNb;
                            flight.dummyFlightBoardingGateNb = dummyFlight.flightPlanBoardingGateNb;
                        }
                        else if (allocationType.Equals(Allocation.BOARDING_GATE_ALLOCATION_TYPE))
                        {
                            flight.fpAsBasisResourceToAllocateEndIndex = dummyFlight.flightPlanBoardingGateNb;
                            flight.dummyFlightParkingStandNb = dummyFlight.flightPlanParkingStandNb;
                        }
                    }
                }
                else
                {
                    if (row[lastResourceColumnIndex] != null)
                    {
                        if (Int32.TryParse(row[lastResourceColumnIndex].ToString(), out deskNb))
                            flight.fpAsBasisResourceToAllocateEndIndex = deskNb;                        
                    }
                    if (dummyFlightsDictionary.ContainsKey(flightId))
                    {
                        Flight dummyFlight = dummyFlightsDictionary[flightId];                        
                        if (dummyFlight != null
                            && allocationType.Equals(Allocation.CHECK_IN_ALLOCATION_TYPE))
                        {
                            flight.dummyFlightParkingStandNb = dummyFlight.flightPlanParkingStandNb;
                            flight.dummyFlightBoardingGateNb = dummyFlight.flightPlanBoardingGateNb;
                        }                        
                    }
                }                
                flightsFromFilteredFlightPlan.Add(flight);
            }

            return flightsFromFilteredFlightPlan;
        }

        private Flight getArrivalFlightById(int arrivalFlightId, DataTable arrivalFlightPlan)
        {
            Flight arrivalFlight = null;
            if (arrivalFlightId > 0 && arrivalFlightPlan != null)
            {
                #region column indexes
                List<int> indexes = new List<int>();
                #region flight information columns
                int idColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                indexes.Add(idColumnIndex);                                
                int flightDateColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                indexes.Add(flightDateColumnIndex);                                
                int flightTimeColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_STA);                
                indexes.Add(flightTimeColumnIndex);
                int airlineCodeColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                indexes.Add(airlineCodeColumnIndex);
                int flightNumberColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                indexes.Add(flightNumberColumnIndex);
                int airportCodeColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                indexes.Add(airportCodeColumnIndex);
                int flightCategoryColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                indexes.Add(flightCategoryColumnIndex);
                int aircraftTypeColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                indexes.Add(aircraftTypeColumnIndex);
                int nbSeatsColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
                indexes.Add(nbSeatsColumnIndex);                
                int noBSMColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
                indexes.Add(noBSMColumnIndex);
                int cbpColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
                indexes.Add(cbpColumnIndex);                
                #endregion

                #region desk allocation columns
                int gateTerminalColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
                indexes.Add(gateTerminalColumnIndex);
                int parkingTerminalColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
                indexes.Add(parkingTerminalColumnIndex);
                int parkingStandColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);
                indexes.Add(parkingStandColumnIndex);
                int runwayColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay);
                indexes.Add(runwayColumnIndex);                
                    
                int arrivalGateColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate);
                indexes.Add(arrivalGateColumnIndex);
                int reclaimTerminalColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim);
                indexes.Add(reclaimTerminalColumnIndex);
                int reclaimDeskColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
                indexes.Add(reclaimDeskColumnIndex);
                int infeedTerminalColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
                indexes.Add(infeedTerminalColumnIndex);
                int arrivalInfeedStartColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject);
                indexes.Add(arrivalInfeedStartColumnIndex);
                int arrivalInfeedEndColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject);
                indexes.Add(arrivalInfeedEndColumnIndex);
                int transferInfeedColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);
                indexes.Add(transferInfeedColumnIndex);
                #endregion

                #region user_x columns
                int user1ColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                indexes.Add(user1ColumnIndex);
                int user2ColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                indexes.Add(user2ColumnIndex);
                int user3ColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                indexes.Add(user3ColumnIndex);
                int user4ColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                indexes.Add(user4ColumnIndex);
                int user5ColumnIndex = arrivalFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
                indexes.Add(user5ColumnIndex);
                #endregion

                #endregion

                if (FlightPlanUpdateWithAllocationResult.areIndexesValid(indexes))
                {
                    foreach (DataRow row in arrivalFlightPlan.Rows)
                    {
                        int currentId = -1;
                        if (row[idColumnIndex] != null
                            && Int32.TryParse(row[idColumnIndex].ToString(), out currentId)
                            && arrivalFlightId == currentId)
                        {
                            arrivalFlight = new Flight();
                            arrivalFlight.id = arrivalFlightId;

                            int nb = -1;
                            bool b = false;
                            DateTime date = DateTime.MinValue;
                            TimeSpan time = TimeSpan.MinValue;

                            if (row[flightDateColumnIndex] != null && DateTime.TryParse(row[flightDateColumnIndex].ToString(), out date))
                                arrivalFlight.date = date;
                            if (row[flightTimeColumnIndex] != null && TimeSpan.TryParse(row[flightTimeColumnIndex].ToString(), out time))
                                arrivalFlight.time = time;
                            if (row[airlineCodeColumnIndex] != null)
                                arrivalFlight.airlineCode = row[airlineCodeColumnIndex].ToString();
                            if (row[flightNumberColumnIndex] != null)
                                arrivalFlight.flightNumber = row[flightNumberColumnIndex].ToString();
                            if (row[airportCodeColumnIndex] != null)
                                arrivalFlight.airportCode = row[airportCodeColumnIndex].ToString();
                            if (row[flightCategoryColumnIndex] != null)
                                arrivalFlight.flightCategory = row[flightCategoryColumnIndex].ToString();
                            if (row[aircraftTypeColumnIndex] != null)
                                arrivalFlight.aircraftType = row[aircraftTypeColumnIndex].ToString();

                            if (row[noBSMColumnIndex] != null && Boolean.TryParse(row[noBSMColumnIndex].ToString(), out b))
                                arrivalFlight.noBSM = b;
                            if (row[cbpColumnIndex] != null && Boolean.TryParse(row[cbpColumnIndex].ToString(), out b))
                                arrivalFlight.cbp = b;

                            if (row[nbSeatsColumnIndex] != null && Int32.TryParse(row[nbSeatsColumnIndex].ToString(), out nb))
                                arrivalFlight.nbSeats = nb;

                            if (row[gateTerminalColumnIndex] != null && Int32.TryParse(row[gateTerminalColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanGateTerminalNb = nb;
                            if (row[arrivalGateColumnIndex] != null && Int32.TryParse(row[arrivalGateColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanArrivalGateNb = nb;

                            if (row[reclaimTerminalColumnIndex] != null && Int32.TryParse(row[reclaimTerminalColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanReclaimTerminalNb = nb;
                            if (row[reclaimDeskColumnIndex] != null && Int32.TryParse(row[reclaimDeskColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanReclaimDeskNb = nb;

                            if (row[infeedTerminalColumnIndex] != null && Int32.TryParse(row[infeedTerminalColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanInfeedTerminalNb = nb;
                            if (row[arrivalInfeedStartColumnIndex] != null && Int32.TryParse(row[arrivalInfeedStartColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanArrivalInfeedStartDeskNb = nb;
                            if (row[arrivalInfeedEndColumnIndex] != null && Int32.TryParse(row[arrivalInfeedEndColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanArrivalInfeedEndDeskNb = nb;
                            if (row[transferInfeedColumnIndex] != null && Int32.TryParse(row[transferInfeedColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanTransferInfeedNb = nb;

                            if (row[parkingTerminalColumnIndex] != null && Int32.TryParse(row[parkingTerminalColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanParkingTerminalNb = nb;
                            if (row[parkingStandColumnIndex] != null && Int32.TryParse(row[parkingStandColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanParkingStandNb = nb;

                            if (row[runwayColumnIndex] != null && Int32.TryParse(row[transferInfeedColumnIndex].ToString(), out nb))
                                arrivalFlight.flightPlanRunwayNb = nb;
                            break;
                        }
                    }
                }
            }
            return arrivalFlight;
        }

        public DataTable sortFlightPlanByOccupationDateTimeAndBodyCategory(DataTable filteredFlightPlan)
        {
            if (filteredFlightPlan.Columns.IndexOf(FILTERED_FP_OCCUPATION_START_DATE_COLUMN_NAME) == -1
                   || filteredFlightPlan.Columns.IndexOf(BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME) == -1)
            {
                return filteredFlightPlan;
            }
            //string sortCriteria = FILTERED_FP_OCCUPATION_START_DATE_COLUMN_NAME + " ASC, " + BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME + " DESC";

            //DataView dv = new DataView(filteredFlightPlan);
            //dv.Sort = sortCriteria;
            //filteredFlightPlan = dv.ToTable();

            string sortCriteria = FILTERED_FP_OCCUPATION_START_DATE_COLUMN_NAME + " ASC";

            DataView dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();

            sortCriteria = BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME + " DESC";

            dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();

            return filteredFlightPlan;
        }

        public DataTable sortFlightPlanByOccupationStartDate(DataTable filteredFlightPlan)
        {
            if (filteredFlightPlan.Columns.IndexOf(FILTERED_FP_OCCUPATION_START_DATE_COLUMN_NAME) == -1)            
                return filteredFlightPlan;            
            string sortCriteria = FILTERED_FP_OCCUPATION_START_DATE_COLUMN_NAME + " ASC";
            DataView dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();
            return filteredFlightPlan;
        }


        public DataTable sortFlightPlanByBodyCategory(DataTable filteredFlightPlan)
        {
            if (filteredFlightPlan.Columns.IndexOf(BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME) == -1)            
                return filteredFlightPlan;
            string sortCriteria = BODY_CATEGORY_PRIORITY_FLIGHT_PLAN_COLUMN_NAME + " DESC";
            DataView dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();
            return filteredFlightPlan;
        }

        public DataTable sortFlightPlanByOccupationDateTimeAndNbSeats(DataTable filteredFlightPlan)
        {
            string occupationStartDateColumnName = FILTERED_FP_OCCUPATION_START_DATE_COLUMN_NAME;
            string nbSeatsColumnName = GlobalNames.sFPD_A_Column_NbSeats;

            if (filteredFlightPlan.Columns.IndexOf(occupationStartDateColumnName) == -1
                || filteredFlightPlan.Columns.IndexOf(nbSeatsColumnName) == -1)
            {
                return filteredFlightPlan;
            }
            string sortCriteria = occupationStartDateColumnName + " ASC";

            DataView dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();

            sortCriteria = nbSeatsColumnName + " DESC";

            dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();

            return filteredFlightPlan;
        }
        
        public DataTable sortFlightPlanByNbSeats(DataTable filteredFlightPlan)
        {            
            string nbSeatsColumnName = GlobalNames.sFPD_A_Column_NbSeats;
            if (filteredFlightPlan.Columns.IndexOf(nbSeatsColumnName) == -1)            
                return filteredFlightPlan;            
            string sortCriteria = nbSeatsColumnName + " DESC";
            DataView dv = new DataView(filteredFlightPlan);
            dv.Sort = sortCriteria;
            filteredFlightPlan = dv.ToTable();
            return filteredFlightPlan;
        }
        
        private void getAllocationResourceColumnIndexesFromFlightPlanByAllocationType(DataTable flightPlan, 
            out int firstResourceColumnIndex, out int lastResourceColumnIndex)
        {
            firstResourceColumnIndex = -1;
            lastResourceColumnIndex = -1;

            switch (allocationType)
            {
                case CHECK_IN_ALLOCATION_TYPE:
                    firstResourceColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
                    lastResourceColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);                    
                    break;
                case BOARDING_GATE_ALLOCATION_TYPE:
                    firstResourceColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);
                    lastResourceColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);                    
                    break;
                case PARKING_ALLOCATION_TYPE:
                    firstResourceColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);
                    lastResourceColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);                    
                    break;
                default:
                    break;
            }            
        }

        private void setFlightOccupationTimeInterval(Flight flight)
        {
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                if (departureFlightlinkedArrivalDictionary != null
                    && departureFlightlinkedArrivalDictionary.Count > 0
                    && departureFlightlinkedArrivalDictionary.ContainsKey(flight.id))
                {
                    FlightLink flightLink = departureFlightlinkedArrivalDictionary[flight.id];
                    if (flightLink != null && flightLink.departureFlight != null)
                    {
                        flight.occupiedInterval
                            = getOccupationByFlightAndMinutesFromArrivalToDeparture(flightLink.departureFlight, flightLink.minutesFromArrivalToDeparture);
                        flight.linkedFlight = new Flight();
                        flight.linkedFlight.id = flightLink.arrivalFlight.id;
                    }
                }
                else
                {
                    flight.occupiedInterval = getOccupationByFlightAndOCT(flight);
                    flight.allocatedUsingOCT = true;
                }
            }
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                flight.occupiedInterval = getRoomOccupationForBoardingGatesAllocation(flight, ciOpeningClosingTimesExceptionTable, 
                                                                                        ci_oct_opening_row_text, ci_oct_closing_row_text);
                flight.pierOccupiedInterval = getPierOccupationForBoardingGatesAllocation(flight, bgOpeningClosingTimesExceptionTable, 
                                                                                            bg_oct_opening_row_text, bg_oct_closing_row_text);
                flight.allocatedUsingOCT = true;
            }
            else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                setCheckInOccupationNeed(flight);
            }
        }

        private void setCheckInOccupationNeed(Flight flight)
        {                           
            if (flight == null || ciOpeningClosingTimesExceptionTable == null)
                return;
                        
            int openingTimeInMinutes = -1;
            int closingTimeInMinutes = -1;
            int partialOpeningTimeInMinutes = -1;
            int nbStationsAllocatedInitially = -1;
            int nbStationsOpenedAfterPartial = -1;
            int nbAdditionalStations = -1;

            bool dataAquired = false;
            if (checkInOCTAirlineAirportExceptionDictionary
                .ContainsKey(flight.airlineCode + CheckInOCTAirlineAirportException.UNIQUE_IDENTIFIER_DELIMITER + flight.airportCode))
            {
                CheckInOCTAirlineAirportException ciOCTException
                    = checkInOCTAirlineAirportExceptionDictionary[flight.airlineCode + CheckInOCTAirlineAirportException.UNIQUE_IDENTIFIER_DELIMITER + flight.airportCode];
                openingTimeInMinutes = ciOCTException.openingTimeInMinutes;
                closingTimeInMinutes = ciOCTException.closingTimeInMinutes;
                partialOpeningTimeInMinutes = ciOCTException.partialOpeningTimeInMintues;
                nbStationsAllocatedInitially = ciOCTException.nbAllocatedStations;
                nbStationsOpenedAfterPartial = ciOCTException.nbOpenedStationsAtPartial;
                nbAdditionalStations = ciOCTException.nbAdditionalStations;
                dataAquired = true;
                flight.allocatedUsingOCTExceptions = true;
            }
            else
            {
                string prefix = DEPARTURE_PREFIX;
                Dictionary<String, String> octDictionary
                    = ciOpeningClosingTimesExceptionTable.GetInformationsColumns(0, prefix + flight.id.ToString(),
                                                                                flight.airlineCode, flight.flightCategory);
                if (octDictionary != null
                && octDictionary.ContainsKey(ci_oct_opening_row_text) && octDictionary.ContainsKey(ci_oct_closing_row_text)
                && octDictionary.ContainsKey(ci_oct_partial_opening_row_text) && octDictionary.ContainsKey(ci_oct_nb_allocated_row_text)
                && octDictionary.ContainsKey(ci_oct_nb_allocated_partial_row_text) && octDictionary.ContainsKey(ci_oct_nb_allocated_additional_row_text)
                && Int32.TryParse(octDictionary[ci_oct_opening_row_text], out openingTimeInMinutes)
                && Int32.TryParse(octDictionary[ci_oct_closing_row_text], out closingTimeInMinutes)
                && Int32.TryParse(octDictionary[ci_oct_partial_opening_row_text], out partialOpeningTimeInMinutes)
                && Int32.TryParse(octDictionary[ci_oct_nb_allocated_row_text], out nbStationsAllocatedInitially)
                && Int32.TryParse(octDictionary[ci_oct_nb_allocated_partial_row_text], out nbStationsOpenedAfterPartial)
                && Int32.TryParse(octDictionary[ci_oct_nb_allocated_additional_row_text], out nbAdditionalStations))
                {
                    flight.allocatedUsingOCT = true;
                    dataAquired = true;
                }
            }
            if (!dataAquired)
                return;

            DateTime flightCompleteDate = flight.date.AddMinutes(flight.time.TotalMinutes);
            if (openingTimeInMinutes == partialOpeningTimeInMinutes
                || nbStationsAllocatedInitially == nbStationsOpenedAfterPartial)
            {
                ResourceOccupationNeed ron = getResourceOccupationNeed(openingTimeInMinutes,
                    closingTimeInMinutes, nbStationsAllocatedInitially, nbAdditionalStations, flightCompleteDate);
                 flight.entireAllocationOccupationNeed = ron;
                 flight.occupiedInterval = ron.occupationInterval;
            }
            else
            {
                ResourceOccupationNeed ron = getResourceOccupationNeed(openingTimeInMinutes,
                    closingTimeInMinutes, nbStationsOpenedAfterPartial, nbAdditionalStations, flightCompleteDate);
                flight.entireAllocationOccupationNeed = ron;
                flight.occupiedInterval = ron.occupationInterval;

                ron = getResourceOccupationNeed(openingTimeInMinutes,
                    partialOpeningTimeInMinutes, nbStationsAllocatedInitially - nbStationsOpenedAfterPartial, nbAdditionalStations, flightCompleteDate);
                flight.partialAllocationOccupationNeed = ron;
            }            
            return;
        }
        private ResourceOccupationNeed getResourceOccupationNeed(int openingTimeInMinutes, int closingTimeInMinutes,
            int nbAllocatedStations, int nbAdditionalStations, DateTime flightCompleteDate)
        {            
            DateTime occupationStartDate = flightCompleteDate.AddMinutes(-openingTimeInMinutes);
            DateTime occupationEndDate = flightCompleteDate.AddMinutes(-closingTimeInMinutes);
            TimeInterval occupationInterval = new TimeInterval(occupationStartDate, occupationEndDate);
            if (occupationInterval.fromDate < scenarioInterval.fromDate)
                occupationInterval.fromDate = scenarioInterval.fromDate;
            if (occupationInterval.toDate > scenarioInterval.toDate)
                occupationInterval.toDate = scenarioInterval.toDate;
            ResourceOccupationNeed resourceOccupationNeed
                = new ResourceOccupationNeed(nbAllocatedStations, nbAdditionalStations, occupationInterval);
            return resourceOccupationNeed;
        }

        private TimeInterval getOccupationByFlightAndMinutesFromArrivalToDeparture(LiegeTools.FlightInfoHolder flight, double minutesFromArrivalToDeparture)
        {
            DateTime occupationStartDate = occupationStartDate = flight.flightDate.AddMinutes(-minutesFromArrivalToDeparture);
            DateTime occupationEndDate = occupationEndDate = flight.flightDate;

            TimeInterval occupationInterval = new TimeInterval(occupationStartDate, occupationEndDate);

            if (occupationInterval.fromDate < scenarioInterval.fromDate)
                occupationInterval.fromDate = scenarioInterval.fromDate;
            if (occupationInterval.toDate > scenarioInterval.toDate)
                occupationInterval.toDate = scenarioInterval.toDate;

            return occupationInterval;
        }

        private TimeInterval getOccupationByFlightAndOCT(Flight flight)
        {
            ExceptionTable octExceptionTable = null;
            string oct_opening_row_text = "";
            string oct_closing_row_text = "";

            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                octExceptionTable = parkingOpeningClosingTimesExceptionTable;
                oct_opening_row_text = parking_oct_opening_row_text;
                oct_closing_row_text = parking_oct_closing_row_text;
            }
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                octExceptionTable = bgOpeningClosingTimesExceptionTable;
                oct_opening_row_text = bg_oct_opening_row_text;
                oct_closing_row_text = bg_oct_closing_row_text;
            }
            if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                octExceptionTable = ciOpeningClosingTimesExceptionTable;
                oct_opening_row_text = ci_oct_opening_row_text;
                oct_closing_row_text = ci_oct_closing_row_text;
            }

            if (octExceptionTable == null || oct_opening_row_text == "" || oct_closing_row_text == "")
                return null;

            TimeInterval occupationInterval 
                = getOccupationIntervalByFlightAndOCTExceptionSettings(flight, octExceptionTable, 
                                                                        oct_opening_row_text, oct_closing_row_text);
            return occupationInterval;
        }
                        
        private TimeInterval getRoomOccupationForBoardingGatesAllocation(Flight flight, ExceptionTable checkInOCTexTable,
            string ci_oct_opening_row_text, string ci_oct_closing_row_text)
        {
            if (flight == null || checkInOCTexTable == null || ci_oct_opening_row_text == null || ci_oct_closing_row_text == null)
                return null;
            TimeInterval roomOccupationInterval
                 = getOccupationIntervalByFlightAndOCTExceptionSettings(flight, checkInOCTexTable,
                                                                         ci_oct_opening_row_text, ci_oct_closing_row_text);            
            return roomOccupationInterval;
        }

        private TimeInterval getPierOccupationForBoardingGatesAllocation(Flight flight,
            ExceptionTable boardingGateOCTexTable, string bg_oct_opening_row_text, string bg_oct_closing_row_text)
        {
            if (flight == null || boardingGateOCTexTable == null || bg_oct_opening_row_text == null || bg_oct_closing_row_text == null)
                return null;
            TimeInterval pierOccupationInterval
                 = getOccupationIntervalByFlightAndOCTExceptionSettings(flight, boardingGateOCTexTable,
                                                                         bg_oct_opening_row_text, bg_oct_closing_row_text);
            return pierOccupationInterval;
        }

        private TimeInterval getOccupationIntervalByFlightAndOCTExceptionSettings(Flight flight, ExceptionTable octExceptionTable,
            string oct_opening_row_text, string oct_closing_row_text)
        {
            TimeInterval occupationInterval = null;
            if (flight == null || octExceptionTable == null || oct_opening_row_text == "" || oct_closing_row_text == "")
                return occupationInterval;

            DateTime flightCompleteDate = flight.date.AddMinutes(flight.time.TotalMinutes);
            DateTime occupationStartDate = DateTime.MinValue;
            DateTime occupationEndDate = DateTime.MinValue;

            int openingTimeInMinutes = -1;
            int closingTimeInMinutes = -1;

            string prefix = ARRIVAL_PREFIX;
            if (isDepartureAllocation)
                prefix = DEPARTURE_PREFIX;
            Dictionary<String, String> octDictionary
                = octExceptionTable.GetInformationsColumns(0, prefix + flight.id.ToString(),
                                                                            flight.airlineCode, flight.flightCategory);
            if (octDictionary != null && octDictionary.ContainsKey(oct_opening_row_text) && octDictionary.ContainsKey(oct_closing_row_text)
                && Int32.TryParse(octDictionary[oct_opening_row_text], out openingTimeInMinutes)
                && Int32.TryParse(octDictionary[oct_closing_row_text], out closingTimeInMinutes))
            {
                if (isDepartureAllocation)
                {
                    occupationStartDate = flightCompleteDate.AddMinutes(-openingTimeInMinutes);

                    if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                        occupationEndDate = flightCompleteDate.AddMinutes(-closingTimeInMinutes);
                    else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                        occupationEndDate = flightCompleteDate;//.AddMinutes(-1);//not including the STD: [ci opening, STD)
                    else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
                        occupationEndDate = flightCompleteDate.AddMinutes(-closingTimeInMinutes);
                }
                else
                {
                    occupationStartDate = flightCompleteDate.AddMinutes(openingTimeInMinutes);
                    occupationEndDate = flightCompleteDate.AddMinutes(closingTimeInMinutes);
                }
            }
            occupationInterval = new TimeInterval(occupationStartDate, occupationEndDate);

            if (occupationInterval.fromDate < scenarioInterval.fromDate)
                occupationInterval.fromDate = scenarioInterval.fromDate;
            if (occupationInterval.toDate > scenarioInterval.toDate)
                occupationInterval.toDate = scenarioInterval.toDate;

            if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                int nbNeededStations = 0;
                if (octDictionary != null && octDictionary.ContainsKey(GlobalNames.sOCT_CI_Line_Allocated)
                && Int32.TryParse(octDictionary[GlobalNames.sOCT_CI_Line_Allocated], out nbNeededStations))
                {
                    flight.nbNeededResources = nbNeededStations;
                }
            }
            return occupationInterval;
        }

        private int getBoardingTimeInSecondsByFlightNbSeats(int nbSeats, int secondsPerPaxBoardingSpeed)
        {
            int bufferTimeAfterBoardingInSeconds = 5 * 60;
            int boardingTimeInSeconds = nbSeats * secondsPerPaxBoardingSpeed + bufferTimeAfterBoardingInSeconds;
            return boardingTimeInSeconds;
        }

        public void allocateFixedFlightsFromFlightPlan(List<Flight> flights, List<Resource> availableResources)
        {
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE
                || allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                Dictionary<int, Resource> resourcesDictionary = new Dictionary<int, Resource>();
                foreach (Resource resource in availableResources)
                {
                    if (!resourcesDictionary.ContainsKey(resource.codeId))
                        resourcesDictionary.Add(resource.codeId, resource);
                }

                foreach (Flight flight in flights)
                {
                    if (flight.fpAsBasisResourceToAllocateStartIndex > 0)
                    {
                        if (resourcesDictionary.ContainsKey(flight.fpAsBasisResourceToAllocateStartIndex))
                        {
                            Resource resourceIndicatedInFP = resourcesDictionary[flight.fpAsBasisResourceToAllocateStartIndex];
                            bool flightNeedsTwoResources = false;
                            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                                flightNeedsTwoResources = flight.needsTwoParkingStands();
                            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                                flightNeedsTwoResources = flight.needsTwoBoardingRooms(lowerLargeAircraftLimit);

                            if (flightNeedsTwoResources)
                            {
                                if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                                {
                                    if (contactAreaResourceCodes.Contains(resourceIndicatedInFP.code))
                                        allocateTwoFixedPositionsForFlightFromFlightPlan(resourceIndicatedInFP, availableResources, flight);
                                    else
                                        allocateSingleFixedPositionForFlightFromFlightPlan(resourceIndicatedInFP, flight);
                                }
                                else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                                {
                                    allocateTwoFixedPositionsForFlightFromFlightPlan(resourceIndicatedInFP, availableResources, flight);
                                }
                            }
                            else
                            {
                                allocateSingleFixedPositionForFlightFromFlightPlan(resourceIndicatedInFP, flight);
                            }
                        }
                        else
                        {
                            //flight has a valid allocation (>0) in the Flight Plan but the resource nb couldn't be found in thr available resources
                            //=> will not be able to allocate it here or with the algorithm                        
                            flight.withResourceFromFPnotFoundInAvailable = true;
                        }
                    }
                }
            }
            else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                allocateFixedFlightsFromFlightPlanForCheckIn(flights, availableResources);
            }
        }

        public void allocateFixedFlightsFromFlightPlanForCheckIn(List<Flight> flights, List<Resource> availableResources)
        {
            Dictionary<int, Resource> resourcesDictionary = new Dictionary<int, Resource>();
            foreach (Resource resource in availableResources)
            {
                if (!resourcesDictionary.ContainsKey(resource.codeId))
                    resourcesDictionary.Add(resource.codeId, resource);
            }

            foreach (Flight flight in flights)
            {
                if (flight.fpAsBasisResourceToAllocateStartIndex > 0
                    && flight.fpAsBasisResourceToAllocateEndIndex > 0
                    && flight.fpAsBasisResourceToAllocateStartIndex <= flight.fpAsBasisResourceToAllocateEndIndex)
                {
                    List<Resource> resourcesIndicatedInFP = new List<Resource>();
                    bool resourcesExist = true;
                    for (int resourceId = flight.fpAsBasisResourceToAllocateStartIndex; resourceId <= flight.fpAsBasisResourceToAllocateEndIndex; resourceId++)
                    {
                        if (!resourcesDictionary.ContainsKey(resourceId))
                        {
                            resourcesExist = false;
                            break;
                        }
                        else 
                        {
                            resourcesIndicatedInFP.Add(resourcesDictionary[resourceId]);
                        }
                    }
                    if (resourcesExist)
                    {
                        allocateListOfFixedPositionsForFlightFromFlightPlan(resourcesIndicatedInFP, flight);
                    }
                    else
                    {
                        //flight has a valid allocation (>0) in the Flight Plan but the resource nb couldn't be found in thr available resources
                        //=> will not be able to allocate it here or with the algorithm                        
                        flight.withResourceFromFPnotFoundInAvailable = true;
                    }
                }
            }
        }

        private void allocateTwoFixedPositionsForFlightFromFlightPlan(Resource resourceIndicatedInFP,
            List<Resource> availableResources, Flight flight)
        {
            Resource pairResource = null;
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                if (resourceIndicatedInFP.code == P27_PARKING_STAND_CODE)
                {
                    pairResource = getResourceByCode(availableResources, P26_PARKING_STAND_CODE);
                }
                else if (resourceIndicatedInFP.code == P26_PARKING_STAND_CODE)
                {
                    pairResource = getResourceByCode(availableResources, P27_PARKING_STAND_CODE);
                }
                else if (resourceIndicatedInFP.code == P24_PARKING_STAND_CODE)
                {
                    pairResource = getResourceByCode(availableResources, P25_PARKING_STAND_CODE);
                }
                else if (resourceIndicatedInFP.code == P25_PARKING_STAND_CODE)
                {
                    pairResource = getResourceByCode(availableResources, P24_PARKING_STAND_CODE);
                }
            }
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                if (resourceIndicatedInFP.code == BG_1_BOARDING_GATE_CODE)
                {
                    pairResource = getResourceByCode(availableResources, BG_2_BOARDING_GATE_CODE);
                }
                else if (resourceIndicatedInFP.code == BG_2_BOARDING_GATE_CODE)
                {
                    pairResource = getResourceByCode(availableResources, BG_1_BOARDING_GATE_CODE);
                }
                else if (resourceIndicatedInFP.code == BG_3_BOARDING_GATE_CODE)
                {
                    pairResource = getResourceByCode(availableResources, BG_4_BOARDING_GATE_CODE);
                }
                else if (resourceIndicatedInFP.code == BG_4_BOARDING_GATE_CODE)
                {
                    pairResource = getResourceByCode(availableResources, BG_3_BOARDING_GATE_CODE);
                }
            }

            if (pairResource != null)
            {
                if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                {
                    //check if alloc is possible
                    if (resourceIndicatedInFP.hasAvailableInterval(flight.occupiedInterval)
                        && pairResource.hasAvailableInterval(flight.occupiedInterval))
                    { 
                        resourceIndicatedInFP.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenFlights);
                        flight.allocatedResources.Add(resourceIndicatedInFP);

                        pairResource.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenFlights);
                        flight.allocatedResources.Add(pairResource);

                        flight.allocatedFromFlightPlan = true;
                        
                    }
                    else
                    {
                        //flight did not have space in the resource => could not be allocated from Flight Plan                    
                        flight.noSpaceOnFPIndicatedResource = true;
                    }
                }
                else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                {
                    //check if alloc is possible
                    if (resourceIndicatedInFP.hasAvailableInterval(flight.occupiedInterval)
                        && pairResource.hasAvailableInterval(flight.occupiedInterval))
                    {
                        if (resourceIndicatedInFP.pier.hasAvailableInterval(flight.pierOccupiedInterval)
                            && pairResource.pier.hasAvailableInterval(flight.pierOccupiedInterval))
                        {
                            resourceIndicatedInFP.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenFlights);
                            resourceIndicatedInFP.pier.allocateFlightToResource(flight, flight.pierOccupiedInterval, delayBetweenFlights);
                            flight.allocatedResources.Add(resourceIndicatedInFP);

                            pairResource.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenFlights);
                            pairResource.pier.allocateFlightToResource(flight, flight.pierOccupiedInterval, delayBetweenFlights);
                            flight.allocatedResources.Add(pairResource);

                            flight.allocatedFromFlightPlan = true;
                        }
                        else
                        {
                            flight.noSpaceOnPierOfFPIndicatedResource = true;
                        }
                    }
                    else
                    {
                        //flight did not have space in the resource => could not be allocated from Flight Plan                    
                        flight.noSpaceOnFPIndicatedResource = true;
                    }
                }
                
            }
            else
            {
                flight.notAllowedToUseTheResourceFromFP = true;
            }
        }

        private void allocateSingleFixedPositionForFlightFromFlightPlan(Resource resourceIndicatedInFP, Flight flight)
        {
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                //check if alloc is possible
                if (resourceIndicatedInFP.hasAvailableInterval(flight.occupiedInterval))
                {
                    resourceIndicatedInFP.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenFlights);
                    flight.allocatedResources.Add(resourceIndicatedInFP);
                    flight.allocatedFromFlightPlan = true;
                }
                else
                {
                    //flight did not have space in the resource => could not be allocated from Flight Plan                                
                    flight.noSpaceOnFPIndicatedResource = true;
                }
            }
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                //check if alloc is possible
                if (resourceIndicatedInFP.hasAvailableInterval(flight.occupiedInterval))
                {
                    if (resourceIndicatedInFP.pier.hasAvailableInterval(flight.pierOccupiedInterval))
                    {
                        resourceIndicatedInFP.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenFlights);
                        resourceIndicatedInFP.pier.allocateFlightToResource(flight, flight.pierOccupiedInterval, delayBetweenFlights);
                        flight.allocatedResources.Add(resourceIndicatedInFP);
                        flight.allocatedFromFlightPlan = true;
                    }
                    else
                        flight.noSpaceOnPierOfFPIndicatedResource = true;
                }
                else
                {
                    //flight did not have space in the resource => could not be allocated from Flight Plan                                
                    flight.noSpaceOnFPIndicatedResource = true;
                }
            }
        }

        private void allocateListOfFixedPositionsForFlightFromFlightPlan(List<Resource> resourcesIndicatedInFP, Flight flight)
        {
            //check if alloc is possible on all resources
            bool resourcesHaveAvailableInterval = true;
            foreach (Resource resourceIndicatedInFP in resourcesIndicatedInFP)
            {
                if (!resourceIndicatedInFP.hasAvailableInterval(flight.occupiedInterval))
                {
                    resourcesHaveAvailableInterval = false;
                    break;
                }
            }
            if (resourcesHaveAvailableInterval)
            {
                foreach (Resource resourceIndicatedInFP in resourcesIndicatedInFP)
                {
                    resourceIndicatedInFP.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenFlights);
                    flight.allocatedResources.Add(resourceIndicatedInFP);
                }
                flight.allocatedFromFlightPlan = true;
            }
            else
            {
                //flight did not have space in the resource => could not be allocated from Flight Plan                                
                flight.noSpaceOnFPIndicatedResource = true;
            }
        }

        public void allocate(List<Flight> flights, List<Resource> availableResources)
        {
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                allocateParkingStands(flights, availableResources);
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                allocateBoardingGates(flights, availableResources);
            else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
                allocateCheckInStations(flights, availableResources);
        }
        
        private void allocateCheckInStations(List<Flight> flights, List<Resource> availableResources)
        {
            if (flights == null || flights.Count == 0
                || availableResources == null || availableResources.Count == 0)
            {
                return;
            }
            List<Flight> unallocatedFlights = new List<Flight>();
            List<Flight> allocatedByOverlapping = new List<Flight>();
            foreach (Flight flight in flights)
            {
                if (useFPasBasis
                    && (flight.allocatedFromFlightPlan
                        || flight.noSpaceOnFPIndicatedResource
                        || flight.withResourceFromFPnotFoundInAvailable
                        || flight.notAllowedToUseTheResourceFromFP))
                {
                    continue;
                }
                if (allocatedByOverlapping.Contains(flight))
                    continue;
                if (tryToAllocateCheckInStations(flight, availableResources))
                {                    
                    flight.allocatedWithAlgorithm = true;
                    if (flight.canBeOverlappedAtCheckIn())
                    {
                        List<Flight> overlappingFlights = LiegeTools.getOverlappingFlights(flight, flights);                        
                        foreach (Flight overlappingFlight in overlappingFlights)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("Flight " + flight.id + " is overlapped by flight " + overlappingFlight.id);
                            if (tryToAllocateCheckInForOverlappingFlight(overlappingFlight, flight, flight.allocatedResources, availableResources))
                            {
                                allocatedByOverlapping.Add(overlappingFlight);
                                overlappingFlight.allocatedWithAlgorithm = true;
                            }
                        }
                    }
                }
                else
                    unallocatedFlights.Add(flight);
            }
        }

        private void allocateBoardingGates(List<Flight> flights, List<Resource> availableResources)
        {
            if (flights == null || flights.Count == 0
                || availableResources == null || availableResources.Count == 0)
            {
                return;
            }
            List<Flight> unallocatedFlightsWithoutRoomOverlapping = new List<Flight>();
            bool allowBoardingRoomOverlapping = false;
            foreach (Flight flight in flights)
            {
                if (useFPasBasis)
                {
                    if (flight.allocatedFromFlightPlan
                        || (flight.noSpaceOnFPIndicatedResource && allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                        || flight.noSpaceOnPierOfFPIndicatedResource
                        || flight.withResourceFromFPnotFoundInAvailable
                        || flight.notAllowedToUseTheResourceFromFP)
                    {
                        continue;
                    }
                }

                List<string> possibleResourceCodes
                    = getResourceCodesByFlightAndAllocationType(flight, allocationType);

                List<Resource> possibleResources
                    = getPossibleResourcesListByResourceCodes(possibleResourceCodes, availableResources);

                if (tryToAllocateBoardingGate(flight, possibleResources, allowBoardingRoomOverlapping))
                {
                    flight.allocatedWithAlgorithm = true;
                    continue;
                }
                else
                {
                    unallocatedFlightsWithoutRoomOverlapping.Add(flight);
                }
            }
            if (unallocatedFlightsWithoutRoomOverlapping.Count > 0)
            {
                allowBoardingRoomOverlapping = true;
                foreach (Flight unallocatedFlight in unallocatedFlightsWithoutRoomOverlapping)
                {
                    List<string> possibleResourceCodes
                    = getResourceCodesByFlightAndAllocationType(unallocatedFlight, allocationType);

                    List<Resource> possibleResources
                        = getPossibleResourcesListByResourceCodes(possibleResourceCodes, availableResources);
                                        
                    if (tryToAllocateBoardingGate(unallocatedFlight, possibleResources, allowBoardingRoomOverlapping))
                    {
                        unallocatedFlight.allocatedWithAlgorithm = true;
                        unallocatedFlight.allocatedWithAlgorithmByAllowingBoardingRoomOverlapping = true;
                    }
                }
            }
        }

        private void allocateParkingStands(List<Flight> flights, List<Resource> availableResources)
        {
            if (flights == null || flights.Count == 0 || parkingOpeningClosingTimesExceptionTable == null
                || availableResources == null || availableResources.Count == 0)
            {
                return;
            }

            getContactAreaResources(availableResources);

            List<Flight> unallocatedWithSchengenConstraintFlights = new List<Flight>();
            foreach (Flight flight in flights)
            {
                if (useFPasBasis)
                {
                    if (flight.allocatedFromFlightPlan
                        || flight.noSpaceOnFPIndicatedResource
                        || flight.withResourceFromFPnotFoundInAvailable
                        || flight.notAllowedToUseTheResourceFromFP)
                    {
                        continue;
                    }
                }

                List<string> possibleResourceCodes
                    = getResourceCodesByFlightAndAllocationType(flight, allocationType);

                List<Resource> possibleResources
                    = getPossibleResourcesListByResourceCodes(possibleResourceCodes, availableResources);

                bool canBreakSchengenConstraint = false;
                if (tryToAllocateParking(flight, possibleResources, canBreakSchengenConstraint))
                {
                    flight.allocatedWithAlgorithm = true;
                    continue;
                }
                else
                {
                    unallocatedWithSchengenConstraintFlights.Add(flight);
                }
            }
            if (unallocatedWithSchengenConstraintFlights.Count > 0)
            {
                foreach (Flight unallocatedFlight in unallocatedWithSchengenConstraintFlights)
                {
                    List<string> possibleResourceCodes
                    = getResourceCodesByFlightAndAllocationType(unallocatedFlight, allocationType);

                    List<Resource> possibleResources
                        = getPossibleResourcesListByResourceCodes(possibleResourceCodes, availableResources);

                    bool canBreakSchengenConstraint = true;
                    if (tryToAllocateParking(unallocatedFlight, possibleResources, canBreakSchengenConstraint))
                    {
                        unallocatedFlight.allocatedWithAlgorithm = true;
                        unallocatedFlight.allocatedWithAlgorithmByBreakingSchengenConstraint = true;
                    }
                }
            }
        }

        private void getContactAreaResources(List<Resource> availableResources)
        {
            if (availableResources != null)
            {
                foreach (Resource availableResource in availableResources)
                {
                    if (contactAreaResourceCodes.Contains(availableResource.code))
                        contactAreaResources.Add(availableResource);
                }
            }
        }
        
        private bool tryToAllocateParking(Flight flight, List<Resource> possibleResources,
            bool canBreakSchengenRestriction)
        {
            if (!canBreakSchengenRestriction)
            {
                possibleResources = restrictPossibleResourcesBySchengenConstraint(flight, possibleResources);
            }
            foreach (Resource possibleResource in possibleResources)
            {                
                if (flight.needsTwoParkingStands())
                {
                    List<Resource> allowedResourcesForPair = getAllowedResourcesByPairResource(possibleResource, possibleResources);
                    foreach (Resource allowedResourceForPair in allowedResourcesForPair)
                    {
                        if (tryToAllocateFlightToTwoResources(possibleResource, allowedResourceForPair, flight, possibleResources))                        
                            return true;
                    }
                }
                else
                {
                    if (tryToAllocateFlightToResource(possibleResource, flight))
                        return true;
                }                
            }
            return false;
        }

        private List<Resource> getAllowedResourcesByPairResource(Resource pairResource,
            List<Resource> possibleResources)
        {
            List<Resource> allowedResourcesForPair = new List<Resource>();
            List<string> resourcePool = new List<string>();
            if (contactAreaResourceCodes.Contains(pairResource.code))
                resourcePool = contactAreaResourceCodes;
            else if (remoteResourceCodes.Contains(pairResource.code))
                resourcePool = contactAreaResourceCodes;
            else if (backUpResourceCodes.Contains(pairResource.code))
                resourcePool = backUpResourceCodes;

            int pairResourceIndexInPool = resourcePool.IndexOf(pairResource.code);
            if (pairResourceIndexInPool != -1)
            {
                int previousResourceIndex = pairResourceIndexInPool - 1;
                int nextResourceIndex = pairResourceIndexInPool + 1;
                if (previousResourceIndex >= 0 && previousResourceIndex < resourcePool.Count)
                {                    
                    Resource previousAllowedResource = getResourceByCodeIndex(previousResourceIndex, resourcePool, possibleResources);
                    if (previousAllowedResource != null)
                        allowedResourcesForPair.Add(previousAllowedResource);
                }
                if (nextResourceIndex >= 0 && nextResourceIndex < resourcePool.Count)
                {
                    Resource nextAllowedResource = getResourceByCodeIndex(nextResourceIndex, resourcePool, possibleResources);
                    if (nextAllowedResource != null)
                        allowedResourcesForPair.Add(nextAllowedResource);
                }                    
            }
            return allowedResourcesForPair;
        }

        private Resource getResourceByCodeIndex(int resourceCodeIndex, List<string> resourceCodesPool,
            List<Resource> possibleResources)
        {
            string allowedResourceCode = resourceCodesPool[resourceCodeIndex];
            Resource allowedResource = getResourceByCode(possibleResources, allowedResourceCode);
            return allowedResource;
        }

        private bool tryToAllocateBoardingGate(Flight flight, List<Resource> possibleResources, bool allowBoardingRoomOverlapping)
        {            
            possibleResources = restrictPossibleResourcesBySchengenConstraint(flight, possibleResources);
           
            foreach (Resource possibleResource in possibleResources)
            {
                if (flight.needsTwoBoardingRooms(lowerLargeAircraftLimit))
                {
                    if (possibleResource.code == BG_1_BOARDING_GATE_CODE)
                    {
                        Resource pairResource = getResourceByCode(possibleResources, BG_2_BOARDING_GATE_CODE);
                        if (pairResource != null 
                            && tryToAllocateFlightToTwoResources(possibleResource, pairResource, flight,
                                                                possibleResources, allowBoardingRoomOverlapping))
                        {
                            return true;
                        }
                    }
                    else if (possibleResource.code == BG_2_BOARDING_GATE_CODE)
                    {
                        Resource pairResource = getResourceByCode(possibleResources, BG_1_BOARDING_GATE_CODE);
                        if (pairResource != null
                            && tryToAllocateFlightToTwoResources(possibleResource, pairResource, flight,
                                                                possibleResources, allowBoardingRoomOverlapping))
                        {
                            return true;
                        }
                    }
                    else if (possibleResource.code == BG_3_BOARDING_GATE_CODE)
                    {
                        Resource pairResource = getResourceByCode(possibleResources, BG_4_BOARDING_GATE_CODE);
                        if (pairResource != null
                            && tryToAllocateFlightToTwoResources(possibleResource, pairResource, flight,
                                                                possibleResources, allowBoardingRoomOverlapping))
                        {
                            return true;
                        }
                    }
                    else if (possibleResource.code == BG_4_BOARDING_GATE_CODE)
                    {
                        Resource pairResource = getResourceByCode(possibleResources, BG_3_BOARDING_GATE_CODE);
                        if (pairResource != null
                            && tryToAllocateFlightToTwoResources(possibleResource, pairResource, flight,
                                                                possibleResources, allowBoardingRoomOverlapping))
                        {
                            return true;
                        }
                    }                    
                }
                else
                {
                    if (tryToAllocateFlightToResource(possibleResource, flight, allowBoardingRoomOverlapping))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// A flight can have x resources allocated at the begining of the occupation period.
        /// At partial opening y resources out of the x openend will close,
        /// leaving opened x - y resources opened until the end of the occupation period.
        /// </summary>
        /// <param name="flight">flight being allocated</param>
        /// <param name="possibleResources">all resources</param>
        /// <returns>true if allocation succeeded</returns>
        private bool tryToAllocateCheckInStations(Flight flight, List<Resource> possibleResources)
        {
            if (flight.entireAllocationOccupationNeed == null)
                return false;

            List<List<Resource>> groupsOfResourcesAllocatedForEntirePeriod = new List<List<Resource>>();
            for (int i = 0; i < possibleResources.Count; i++)
            {
                List<Resource> resourcesAllocatedForEntirePeriod
                    = getAvailableConsecutiveResources(i, flight.entireAllocationOccupationNeed.nbStations, possibleResources,
                                                       flight.entireAllocationOccupationNeed.occupationInterval);
                if (resourcesAllocatedForEntirePeriod.Count > 0)
                {
                    if (flight.partialAllocationOccupationNeed == null)  //no partial check-in opening
                    {
                        //allocate all resources from usableResources
                        allocateFlightToResources(resourcesAllocatedForEntirePeriod, flight, flight.entireAllocationOccupationNeed.occupationInterval);
                        return true;                        
                    }
                    else  //check-in partial opening
                    {
                        groupsOfResourcesAllocatedForEntirePeriod.Add(resourcesAllocatedForEntirePeriod);
                    }
                }
            }
            if (groupsOfResourcesAllocatedForEntirePeriod.Count > 0)
            {
                foreach (List<Resource> resourcesAllocatedForEntirePeriod in groupsOfResourcesAllocatedForEntirePeriod)
                {
                    Resource lastResourceAllocatedForEntirePeriod = resourcesAllocatedForEntirePeriod[resourcesAllocatedForEntirePeriod.Count - 1];                    
                    if (flight.partialAllocationOccupationNeed == null)
                        return false;

                    List<Resource> resourcesAllocatedForPartialPeriod
                        = getAvailableConsecutiveResources(lastResourceAllocatedForEntirePeriod.id, flight.partialAllocationOccupationNeed.nbStations, possibleResources,
                                                       flight.partialAllocationOccupationNeed.occupationInterval);
                    if (resourcesAllocatedForPartialPeriod.Count > 0)
                    {
                        //allocate all resources needed for the entire period and the resources needed only until partial opening
                        allocateFlightToResources(resourcesAllocatedForEntirePeriod, flight, flight.entireAllocationOccupationNeed.occupationInterval);
                        allocateFlightToResources(resourcesAllocatedForPartialPeriod, flight, flight.partialAllocationOccupationNeed.occupationInterval);
                        return true;                        
                    }
                }
            }
            return false;
        }

        private bool tryToAllocateCheckInForOverlappingFlight(Flight overlappingFlight, Flight overlappedFlight,
            List<Resource> overlappedFlightAllocatedResources, List<Resource> allResources)
        {
            overlappedFlightAllocatedResources = overlappedFlightAllocatedResources.OrderBy(r => r.id).ToList();
            Resource overlappedFlightLastAllocatedResource 
                = overlappedFlightAllocatedResources[overlappedFlightAllocatedResources.Count - 1];
            TimeInterval overlappingInterval = overlappedFlight.entireAllocationOccupationNeed.occupationInterval
                .getIntersectionTimeInterval(overlappingFlight.entireAllocationOccupationNeed.occupationInterval);
            List<Resource> resourcesAllocatedForOverlappingPeriod
                = getAvailableConsecutiveResources(overlappedFlightLastAllocatedResource.id, 
                        overlappedFlight.entireAllocationOccupationNeed.nbAdditionalStations, allResources, overlappingInterval);
            if (resourcesAllocatedForOverlappingPeriod.Count > 0)
            {
                allocateFlightToResources(overlappedFlightAllocatedResources, overlappingFlight, 
                                            overlappingFlight.entireAllocationOccupationNeed.occupationInterval);
                allocateFlightToResources(resourcesAllocatedForOverlappingPeriod, overlappingFlight, overlappingInterval);
                overlappedFlight.overlappingFlights.Add(overlappingFlight);
                return true;
            }
            return false;
        }

        private List<Resource> getAvailableConsecutiveResources(int startingResourceIndex, int nbOfResourcesNeeded,
            List<Resource> resources, TimeInterval occupationInterval)
        {
            List<Resource> usableResources = new List<Resource>();
            int endResourceIndex = startingResourceIndex + nbOfResourcesNeeded - 1;
            if (endResourceIndex >= resources.Count)
                return usableResources;            
            for (int i = startingResourceIndex; i <= endResourceIndex; i++)
            {
                Resource resource = resources[i];
                if (resource.hasAvailableInterval(occupationInterval))
                    usableResources.Add(resource);
                else
                {                    
                    usableResources.Clear();
                    return usableResources;
                }
            }
            return usableResources;
        }

        private List<Resource> restrictPossibleResourcesBySchengenConstraint(Flight flight,
            List<Resource> possibleResources)
        {
            List<Resource> resourcesRestrictedBySchengenConstraint = new List<Resource>();

            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                resourcesRestrictedBySchengenConstraint = contactAreaResources;
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                resourcesRestrictedBySchengenConstraint = possibleResources;

            List<string> allowedResourceCodesBySchengenRestriction
                = getAllowedResourcesBySchengenRestriction(resourcesRestrictedBySchengenConstraint, flight);
            List<Resource> schengenRestrictedPossibleResources = new List<Resource>();
                                       
            foreach (Resource possibleResource in possibleResources)
            {
                if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                {
                    if (contactAreaResourceCodes.Contains(possibleResource.code))
                    {
                        if (allowedResourceCodesBySchengenRestriction.Contains(possibleResource.code))
                            schengenRestrictedPossibleResources.Add(possibleResource);
                    }
                    else
                    {
                        schengenRestrictedPossibleResources.Add(possibleResource);
                    }
                }
                else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                {
                    if (allowedResourceCodesBySchengenRestriction.Contains(possibleResource.code))
                        schengenRestrictedPossibleResources.Add(possibleResource);
                }
            }
            return schengenRestrictedPossibleResources;
        }

        private List<string> getAllowedResourcesBySchengenRestriction(List<Resource> resourcesRestrictedBySchengenConstraint, 
            Flight flight)
        {
            List<string> allowedResourceCodesBySchengenRestriction = new List<string>();
            //The resource list must be ordered when checking the frontier (order direction depends on the alloc type) - see specs
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                resourcesRestrictedBySchengenConstraint = resourcesRestrictedBySchengenConstraint.OrderBy(res => res.code).ToList();
            else
                resourcesRestrictedBySchengenConstraint = resourcesRestrictedBySchengenConstraint.OrderByDescending(res => res.code).ToList();
                
            if (flight.isSchengen())
            {
                bool reachedNonSchengenContactResource = false;
                foreach (Resource possibleResource in resourcesRestrictedBySchengenConstraint)
                {
                    if (!reachedNonSchengenContactResource)
                    {
                        if (possibleResource.isOccupied(flight.occupiedInterval))
                        {
                            if (possibleResource.isCurrentlyOccupiedOnlyBySchengen(flight.occupiedInterval))
                            {
                                if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                                {
                                    if (possibleResource.code == BG_1_BOARDING_GATE_CODE
                                        || possibleResource.code == BG_2_BOARDING_GATE_CODE)
                                    {
                                        if (isPairResourceSameSchengenType(possibleResource.code, flight))
                                        {
                                            allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                        }
                                    }
                                    else
                                    {
                                        allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                    }
                                }
                                else if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                                {
                                    allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                }
                            }
                            else if (possibleResource.isCurrentlyOccupiedOnlyByNonSchengen(flight.occupiedInterval))
                            {
                                //if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                                //{
                                //    if (possibleResource.code == BG_1_BOARDING_GATE_CODE
                                //        || possibleResource.code == BG_2_BOARDING_GATE_CODE)
                                //    {
                                //        if (isPairResourceSameSchengenType(possibleResource.code, flight))
                                //        {
                                //            allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                //        }
                                //    }
                                //    else
                                //    {
                                //        allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                //    }
                                //}
                                //else if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                                //{
                                //    allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                //}
                                reachedNonSchengenContactResource = true;
                            }
                        }
                        else
                        {
                            if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                            {
                                if (possibleResource.code == BG_1_BOARDING_GATE_CODE
                                        || possibleResource.code == BG_2_BOARDING_GATE_CODE)
                                {
                                    if (isPairResourceSameSchengenType(possibleResource.code, flight))
                                    {
                                        allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                    }
                                }
                                else
                                {
                                    allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                }
                            }
                            else if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                            {
                                allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                            }
                        }
                    }                    
                }
            }
            else
            {
                bool reachedSchengenContactResource = false;
                for (int i = resourcesRestrictedBySchengenConstraint.Count - 1; i >= 0; i--)
                {
                    Resource possibleResource = resourcesRestrictedBySchengenConstraint[i];
                    if (!reachedSchengenContactResource)
                    {
                        if (possibleResource.isOccupied(flight.occupiedInterval))
                        {
                            if (possibleResource.isCurrentlyOccupiedOnlyByNonSchengen(flight.occupiedInterval))
                            {
                                if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                                {
                                    if (possibleResource.code == BG_1_BOARDING_GATE_CODE
                                        || possibleResource.code == BG_2_BOARDING_GATE_CODE)
                                    {
                                        if (isPairResourceSameSchengenType(possibleResource.code, flight))
                                        {
                                            allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                        }
                                    }
                                    else
                                    {
                                        allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                    }
                                }
                                else if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                                {
                                    allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                }
                            }
                            else if (possibleResource.isCurrentlyOccupiedOnlyBySchengen(flight.occupiedInterval))
                            {
                                //if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                                //{
                                //    if (possibleResource.code == BG_1_BOARDING_GATE_CODE
                                //        || possibleResource.code == BG_2_BOARDING_GATE_CODE)
                                //    {
                                //        if (isPairResourceSameSchengenType(possibleResource.code, flight))
                                //        {
                                //            allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                //        }
                                //    }
                                //    else
                                //    {
                                //        allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                //    }
                                //}
                                //else if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                                //{
                                //    allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                //}
                                reachedSchengenContactResource = true;
                            }
                        }
                        else
                        {
                            if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                            {
                                if (possibleResource.code == BG_1_BOARDING_GATE_CODE
                                        || possibleResource.code == BG_2_BOARDING_GATE_CODE)
                                {
                                    if (isPairResourceSameSchengenType(possibleResource.code, flight))
                                    {
                                        allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                    }
                                }
                                else
                                {
                                    allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                                }
                            }
                            else if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                            {
                                allowedResourceCodesBySchengenRestriction.Add(possibleResource.code);
                            }
                        }
                    }
                }
            }
            return allowedResourceCodesBySchengenRestriction;
        }

        private bool isPairResourceSameSchengenType(string currentResourceCode, Flight flight)
        {
            string pairResourceCode = "";
            if (currentResourceCode == BG_1_BOARDING_GATE_CODE)
                pairResourceCode = BG_2_BOARDING_GATE_CODE;
            else if (currentResourceCode == BG_2_BOARDING_GATE_CODE)
                pairResourceCode = BG_1_BOARDING_GATE_CODE;
                        
            if (initialAvailableResources != null)
            {
                Resource pair = getResourceByCode(initialAvailableResources, pairResourceCode);
                if (pair != null)
                {
                    if (pair.isOccupied(flight.occupiedInterval))
                    {
                        if (pair.isCurrentlyOccupiedOnlyBySchengen(flight.occupiedInterval) && !flight.isSchengen())
                            return false;
                        else if (pair.isCurrentlyOccupiedOnlyByNonSchengen(flight.occupiedInterval) && flight.isSchengen())
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }

        private bool tryToAllocateFlightToTwoResources(Resource possibleResource, Resource pairResource,
             Flight flight, List<Resource> possibleResources)
        {            
            if (possibleResource.hasAvailableInterval(flight.occupiedInterval)
                && pairResource.hasAvailableInterval(flight.occupiedInterval))
            {
                if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                {
                    if (possibleResource.pier.hasAvailableInterval(flight.pierOccupiedInterval)
                        && pairResource.pier.hasAvailableInterval(flight.pierOccupiedInterval))
                    {
                        allocateFlightToResource(possibleResource, flight);
                        allocateFlightToResource(pairResource, flight);
                        return true;
                    }
                }
                else
                {
                    allocateFlightToResource(possibleResource, flight);
                    allocateFlightToResource(pairResource, flight);
                    return true;
                }
                
            }            
            return false;
        }
        private bool tryToAllocateFlightToTwoResources(Resource possibleResource, Resource pairResource,
             Flight flight, List<Resource> possibleResources, bool allowBoardingRoomOverlapping)
        {
            if (allowBoardingRoomOverlapping 
                || (possibleResource.hasAvailableInterval(flight.occupiedInterval)
                    && pairResource.hasAvailableInterval(flight.occupiedInterval)))
            {
                if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                {
                    if (possibleResource.pier.hasAvailableInterval(flight.pierOccupiedInterval)
                        && pairResource.pier.hasAvailableInterval(flight.pierOccupiedInterval))
                    {
                        allocateFlightToResource(possibleResource, flight);
                        allocateFlightToResource(pairResource, flight);
                        return true;
                    }
                    else
                    {
                        flight.noSpaceOnPier = true;
                    }
                }
                else
                {
                    allocateFlightToResource(possibleResource, flight);
                    allocateFlightToResource(pairResource, flight);
                    return true;
                }

            }
            return false;
        }

        private bool tryToAllocateFlightToResource(Resource possibleResource, Flight flight)
        {            
            if (possibleResource.hasAvailableInterval(flight.occupiedInterval))
            {
                if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                {
                    if (possibleResource.pier.hasAvailableInterval(flight.pierOccupiedInterval))
                    {
                        allocateFlightToResource(possibleResource, flight); 
                        return true;
                    }
                }
                else
                {
                    allocateFlightToResource(possibleResource, flight);
                    return true;
                }                
            }            
            return false;
        }
        private bool tryToAllocateFlightToResource(Resource possibleResource, Flight flight, bool allowBoardingRoomOverlapping)
        {
            if (allowBoardingRoomOverlapping 
                || possibleResource.hasAvailableInterval(flight.occupiedInterval))
            {
                if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                {
                    if (possibleResource.pier.hasAvailableInterval(flight.pierOccupiedInterval))
                    {
                        allocateFlightToResource(possibleResource, flight);
                        return true;
                    }
                    else
                    {
                        flight.noSpaceOnPier = true;
                    }
                }
                else
                {
                    allocateFlightToResource(possibleResource, flight);
                    return true;
                }
            }
            return false;
        }

        private void allocateFlightToResource(Resource possibleResource, Flight flight)
        {
            possibleResource.allocateFlightToResource(flight, flight.occupiedInterval, delayBetweenFlights);
            if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                possibleResource.pier.allocateFlightToResource(flight, flight.pierOccupiedInterval, delayBetweenFlights);
            flight.allocatedResources.Add(possibleResource);
        }

        private void allocateFlightToResources(List<Resource> usableResources, Flight flight, TimeInterval occupationInterval)
        {
            foreach (Resource usableResource in usableResources)
            {
                usableResource.allocateFlightToResource(flight, occupationInterval, delayBetweenFlights);
                flight.allocatedResources.Add(usableResource);
            }
        }

        private List<string> getResourceCodesByFlightAndAllocationType(Flight flight, string allocationType)
        {
            List<string> resourceCodes = new List<string>();
            if (flightCategoriesWithResourcePrioritiesDictionary != null
                && flightCategoriesWithResourcePrioritiesDictionary.ContainsKey(flight.flightCategory))
            {
                //if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                //{
                //    if (flight.needsTwoParkingStands())
                //    {
                //        resourceCodes.Add(P27_PARKING_STAND_CODE);// + "|" + P26_PARKING_STAND_CODE);
                //        resourceCodes.Add(P26_PARKING_STAND_CODE);
                //        resourceCodes.Add(P25_PARKING_STAND_CODE);// + "|" + P24_PARKING_STAND_CODE);
                //        resourceCodes.Add(P24_PARKING_STAND_CODE);                        
                //    }
                //}
                //bool isBigFlight = false;
                //if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                //    isBigFlight = flight.needsTwoParkingStands();
                //else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                //    isBigFlight = flight.needsTwoBoardingRooms(lowerLargeAircraftLimit);//this doesnt affect the Boarding Gates obtained; only for parking 
                resourceCodes.AddRange(getResourceCodesListByFlightCategory(flight.flightCategory));//, isBigFlight));
            }
            else
            {
                OverallTools.ExternFunctions.PrintLogFile("Resources for flight with the Id " + flight.id +" could not have been determined");                
            }
            return resourceCodes;
        }

        private List<string> getResourceCodesListByFlightCategory(string flightCategory)//, bool isBigFlight)
        {
            List<string> resourceCodes = new List<string>();
            if (flightCategoriesWithResourcePrioritiesDictionary.ContainsKey(flightCategory))
            {
                List<ResourcePriority> resourcePriorities = flightCategoriesWithResourcePrioritiesDictionary[flightCategory];
                foreach (ResourcePriority resourcePriority in resourcePriorities)
                {
                    //if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                    //{
                    //    if (isBigFlight)
                    //    {
                    //        if (contactAreaResourceCodes.Contains(resourcePriority.resourceName))
                    //            continue;
                    //    }
                    //}
                    resourceCodes.Add(resourcePriority.resourceName);
                }
            }
            return resourceCodes;
        }

        private List<Resource> getPossibleResourcesListByResourceCodes(List<string> resourceCodes, List<Resource> availableResources)
        {
            List<Resource> possibleResources = new List<Resource>();
            if (resourceCodes != null)
            {
                for (int i = 0; i < resourceCodes.Count; i++)
                {
                    string resourceCode = resourceCodes[i];
                    foreach (Resource availableResource in availableResources)
                    {
                        if (availableResource.code == resourceCode)
                            possibleResources.Add(availableResource);
                    }
                }
            }
            return possibleResources;
        }

        private Resource getResourceByCode(List<Resource> resources, string code)
        {            
            foreach (Resource res in resources)
            {
                if (res.code == code)
                    return res;
            }
            return null;
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
            
    }
}
