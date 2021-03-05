using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace SIMCORE_TOOL.Prompt.Dubai
{
    class DubaiTools
    {
        //Flight Plan
        internal static String flightPlanFileArrivalOrDepartureColumnName = "A/D";
        internal static String flightPlanFileFlightNbColumnName = "Flight No";
        internal static String flightPlanFileAirlineCodeColumnName = "Carrier";
        internal static String flightPlanFileAirportCodeColumnName = "O/D";
        internal static String flightPlanFileDateColumnName = "Date";
        internal static String flightPlanFileTimeColumnName = "STA/STD";
        internal static String flightPlanFileFlightCategoryColumnName = "Flight Type";
        internal static String flightPlanFileLinkedDepFlightColumnName = "Linked Flight No.";
        internal static String flightPlanFileAircraftTypeColumnName = "Aircraft";
        internal static String flightPlanFileNbSeatsColumnName = "Seats";
        internal static String flightPlanFileParkingTerminalColumnName = "Terminal";
        
        internal static List<String> FLIGHT_PLAN_FILE_COLUMNS
            = new List<String>(new string[] 
            { 
                flightPlanFileArrivalOrDepartureColumnName, flightPlanFileFlightNbColumnName,
                flightPlanFileAirlineCodeColumnName, flightPlanFileAirportCodeColumnName, flightPlanFileDateColumnName,
                flightPlanFileTimeColumnName, flightPlanFileFlightCategoryColumnName, flightPlanFileLinkedDepFlightColumnName,
                flightPlanFileAircraftTypeColumnName, flightPlanFileNbSeatsColumnName, flightPlanFileParkingTerminalColumnName
            });

        internal static string PARKING_ZONE_CHARLIE = "CHARLIE";
        internal static string PARKING_ZONE_GOLF = "GOLF";
        internal static string PARKING_ZONE_ECHO = "ECHO";

        internal static string PARKING_ALLOCATION_CODE_F = "F";
        internal static string PARKING_ALLOCATION_CODE_E = "E";
        internal static string PARKING_ALLOCATION_CODE_C = "C";
        internal static List<String> PARKING_ALLOCATION_CODES_LIST
            = new List<String>(new string[] { PARKING_ALLOCATION_CODE_F, PARKING_ALLOCATION_CODE_E, PARKING_ALLOCATION_CODE_C });

        internal static string PARKING_CONCOURSE_CA = "CA";
        internal static string PARKING_CONCOURSE_CB = "CB";
        internal static string PARKING_CONCOURSE_CC = "CC";
        internal static string PARKING_CONCOURSE_CD = "CD";

        internal static string PARKING_LOCATION_CODE_CA = "CA";
        internal static string PARKING_LOCATION_CODE_CB = "CB";
        internal static string PARKING_LOCATION_CODE_CC = "CC";
        internal static string PARKING_LOCATION_CODE_CD = "CD";
        internal static string PARKING_LOCATION_CODE_ECHO = "Echo";
        internal static string PARKING_LOCATION_CODE_REMOTE_CHARLIE = "RemoteCharlie";
        internal static string PARKING_LOCATION_CODE_REMOTE_GOLF = "RemoteGolf";
        internal static string PARKING_LOCATION_CODE_REMOTE_ECHO = "RemoteEcho";

        public static Boolean areColumnHeadersFromFileValid(DataTable table, List<String> columnNames)
        {
            if (table == null)
                return false;
            if (columnNames == null || columnNames.Count == 0)
                return true;

            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = column.ColumnName.Trim();
            }

            foreach (String columnName in columnNames)
            {
                if (!table.Columns.Contains(columnName))
                {
                    OverallTools.ExternFunctions
                        .PrintLogFile("Please check the table " + table.TableName + ". It must contain the column " + columnName + ".");
                    return false;
                }
            }
            return true;
        }
       
        internal class DubaiFlight
        {
            private const String KEY_DELIMITER = "[-]";

            public int succesfullRowId { get; set; }
            public int flightId { get; set; }
            public DateTime flightDate { get; set; }
            public TimeSpan flightTime { get; set; }
            public String airlineCode { get; set; }
            public String flightNumber { get; set; }
            public String airportCode { get; set; }
            public String flightCategory { get; set; }
            public String aircraftType { get; set; }
            public int nbSeats { get; set; }
            public int parkingTerminalNb { get; set; }            
            
            public String linkedFlightNb { get; set; }
            public double downTime { get; set; }
            public bool hasDefaultDownTime { get; set; }
            public String arrivalOrDepartureTag { get; set; }
            public String terminalName { get; set; }

            public String parkingStandDistributionCode { get; set; }    // >> Task #12843 Pax2Sim - Dubai - allocate using Cplex

            /// <summary>
            /// key = flightNumber + delimiter + flightDate + delimiter + flightTime
            /// </summary>
            public String dictionaryKey
            {
                get
                {
                    return flightNumber + KEY_DELIMITER
                        + flightDate + KEY_DELIMITER + flightTime;
                }
            }
        }
        
        internal class FlightPlanConversion
        {            
            internal Dictionary<String, DubaiFlight> arrivalFlightsDictionary = new Dictionary<String, DubaiFlight>();
            internal Dictionary<String, DubaiFlight> departureFlightsDictionary = new Dictionary<String, DubaiFlight>();

            internal List<String> importedAirlineCodes = new List<String>();
            internal List<String> importedFlightCategories = new List<String>();
            internal List<String> importedAircraftTypes = new List<String>();

            DataTable rawFlightPlan;            
            
            internal ArrayList errorList = new ArrayList();
            internal ArrayList infoList = new ArrayList();

            public FlightPlanConversion(DataTable _rawFlightPlan)
            {
                arrivalFlightsDictionary.Clear();
                departureFlightsDictionary.Clear();

                importedAirlineCodes.Clear();
                importedFlightCategories.Clear();
                importedAircraftTypes.Clear();

                this.rawFlightPlan = _rawFlightPlan;                
            }

            public void convert()
            {
                if (rawFlightPlan == null || rawFlightPlan.Rows.Count == 0)
                {
                    errorList.Add("The imported flight plan doesn't have any data.");
                    return;
                }
                if (!DubaiTools.areColumnHeadersFromFileValid(rawFlightPlan, DubaiTools.FLIGHT_PLAN_FILE_COLUMNS))
                {
                    errorList.Add("The imported flight plan doesn't have valid columns. Please check the Log.txt file for details.");
                    return;
                }
                generateFlights();                
            }

            private const String ARRIVAL_FLIGHT_TAG = "A";
            private const String DEPARTURE_FLIGHT_TAG = "D";

            private void generateFlights()
            {
                int arrOrDepColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileArrivalOrDepartureColumnName);
                                
                int flightNbColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileFlightNbColumnName);
                int airlineCodeColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileAirlineCodeColumnName);

                int flightDateColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileDateColumnName);
                int flightTimeColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileTimeColumnName);
                
                int airportCodeColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileAirportCodeColumnName);
                int flightCategoryColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileFlightCategoryColumnName);
                int aircraftTypeColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileAircraftTypeColumnName);
                int nbSeatsColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileNbSeatsColumnName);

                int linkedFlightNbColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileLinkedDepFlightColumnName);
                int terminalColumnIndex = rawFlightPlan.Columns.IndexOf(DubaiTools.flightPlanFileParkingTerminalColumnName);
                
                int rowIndex = 2;//row number 1 from the text file contains the column names
                int ignoredFlightInfoIndex = 1;
                                
                foreach (DataRow rawFlightPlanRow in rawFlightPlan.Rows)
                {
                    DubaiFlight flight = new DubaiFlight();
                    flight.succesfullRowId = rowIndex;

                    bool arrival = true;
                    String arrivalOrDeparture = "";
                    if (rawFlightPlanRow[arrOrDepColumnIndex] != null)
                    {
                        arrivalOrDeparture = rawFlightPlanRow[arrOrDepColumnIndex].ToString().Trim();
                        flight.arrivalOrDepartureTag = arrivalOrDeparture;

                        if (!arrivalOrDeparture.Equals(ARRIVAL_FLIGHT_TAG))
                        {
                            if (arrivalOrDeparture.Equals(DEPARTURE_FLIGHT_TAG))
                            {
                                arrival = false;                                
                            }
                            else
                            {
                                //invalid tag
                                infoList.Add("Flight plan file info " + ignoredFlightInfoIndex + ". " + "The flight from the line "
                                    + rowIndex + " doesn't have a valid Arrival or Departure indicator (A or D). It will be ignored.");
                                ignoredFlightInfoIndex++;
                                rowIndex++;
                                continue;
                            }
                        }
                    }

                    if (rawFlightPlanRow[flightNbColumnIndex] != null)
                    {
                        flight.flightNumber = rawFlightPlanRow[flightNbColumnIndex].ToString().Trim();
                    }
                    if (flight.flightNumber == null || flight.flightNumber == "")
                    {
                        infoList.Add("Flight plan file info " + ignoredFlightInfoIndex + ". "
                            + "The flight from the line " + rowIndex + " doesn't have a flight number. It will be ignored.");
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }

                    if (rawFlightPlanRow[airlineCodeColumnIndex] != null)
                    {
                        flight.airlineCode = rawFlightPlanRow[airlineCodeColumnIndex].ToString().Trim();
                        if (!importedAirlineCodes.Contains(flight.airlineCode))
                            importedAirlineCodes.Add(flight.airlineCode);
                    }

                    DateTime flightDate = DateTime.MinValue;
                    if (rawFlightPlanRow[flightDateColumnIndex] != null)
                        flightDate = FonctionsType.getDate(rawFlightPlanRow[flightDateColumnIndex]);
                    if (flightDate == DateTime.MinValue)
                    {
                        infoList.Add("Flight plan file info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The flight " + flight.flightNumber
                            + " doesn't have a valid value in the column Date from the Flight Plan text file. It will be ignored.");
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    else
                    {
                        flight.flightDate = flightDate;
                    }

                    TimeSpan flightTime = TimeSpan.MinValue;
                    if (rawFlightPlanRow[flightTimeColumnIndex] != null)
                        flightTime = FonctionsType.getTime(rawFlightPlanRow[flightTimeColumnIndex]);
                    if (flightTime == TimeSpan.MinValue)
                    {
                        infoList.Add("Flight plan file info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The flight " + flight.flightNumber
                            + " doesn't have a valid value in the column STA/STD (flight time) from the Flight Plan text file. It will be ignored.");
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    else
                    {
                        flight.flightTime = flightTime;
                    }

                    if (rawFlightPlanRow[flightCategoryColumnIndex] != null)
                    {
                        flight.flightCategory = rawFlightPlanRow[flightCategoryColumnIndex].ToString().Trim();
                        if (!importedFlightCategories.Contains(flight.flightCategory))
                            importedFlightCategories.Add(flight.flightCategory);
                    }

                    if (rawFlightPlanRow[airportCodeColumnIndex] != null)
                    {
                        flight.airportCode = rawFlightPlanRow[airportCodeColumnIndex].ToString().Trim();
                    }

                    if (rawFlightPlanRow[aircraftTypeColumnIndex] != null)
                    {
                        flight.aircraftType = rawFlightPlanRow[aircraftTypeColumnIndex].ToString().Trim();

                        if (!importedAircraftTypes.Contains(flight.aircraftType))
                            importedAircraftTypes.Add(flight.aircraftType);
                    }

                    String terminal = "";
                    if (rawFlightPlanRow[terminalColumnIndex] != null)
                    {
                        terminal = rawFlightPlanRow[terminalColumnIndex].ToString().Trim();
                    }
                    if (terminal == null || terminal.Equals(""))
                    {
                        infoList.Add("Flight plan file info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The flight " + flight.flightNumber
                            + " doesn't have a valid value in the column Terminal (parking terminal) from the Flight Plan text file. It will be ignored.");
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    flight.parkingTerminalNb = getTerminalNbFromAlphanumerical(terminal);
                    flight.terminalName = terminal;
                    
                    int nbSeats = -1;
                    if (rawFlightPlanRow[nbSeatsColumnIndex] != null)
                    {
                        Int32.TryParse(rawFlightPlanRow[nbSeatsColumnIndex].ToString(), out nbSeats);
                    }
                    if (nbSeats < 0)
                    {
                        flight.nbSeats = 0;
                        infoList.Add("Flight plan file Line " + rowIndex + ": Number of seats not indicated in the Flight plan file for the flight "
                            + flight.flightNumber + ". The flight will be taken into account.");
                    }
                    else
                    {
                        flight.nbSeats = nbSeats;
                    }

                    String linkedFlightNb = "";
                    if (rawFlightPlanRow[linkedFlightNbColumnIndex] != null)
                    {
                        linkedFlightNb = rawFlightPlanRow[linkedFlightNbColumnIndex].ToString().Trim();
                    }
                    if (linkedFlightNb == null || linkedFlightNb == "")
                    {
                        infoList.Add("Flight plan file info Line " + rowIndex + ": The flight " + flight.flightNumber
                            + " doesn't have a Linked Flight. The flight will be taken into account.");
                    }
                    else
                    {
                        flight.linkedFlightNb = linkedFlightNb;
                    }

                    // >> Task #12843 Pax2Sim - Dubai - allocate using Cplex                    
                    //Parking Stand distribution code
                    if (!arrival && flight.aircraftType != null)
                    {
                        flight.parkingStandDistributionCode 
                            = getParkingStandCodeByAircraftTypeAndTerminal(flight.aircraftType, flight.parkingTerminalNb);

                        if (flight.parkingStandDistributionCode == null
                            && flight.parkingStandDistributionCode == "")
                        {
                            infoList.Add("Flight plan file info Line " + rowIndex
                                + ": Could not obtain the parking stand distribution code for the flight "
                                + flight.flightNumber + ". The flight will be taken into account.");
                        }
                    }
                    // << Task #12843 Pax2Sim - Dubai - allocate using Cplex

                    if (arrival)
                        arrivalFlightsDictionary.Add(flight.dictionaryKey, flight);
                    else
                        departureFlightsDictionary.Add(flight.dictionaryKey, flight);
                    rowIndex++;
                }
            }
            
            #region Terminal calculation
            private const String TERMINAL_1_I = "1I";
            private const int TERMINAL_1I_NB = 1;
            private const String TERMINAL_2_I = "2I";
            private const int TERMINAL_2I_NB = 2;
            private const String TERMINAL_3I = "3I";
            private const int TERMINAL_3I_NB = 3;
            private const String TERMINAL_ENG = "ENG";
            private const int TERMINAL_ENG_NB = 4;

            private int getTerminalNbFromAlphanumerical(String terminalName)
            {
                if (terminalName.Equals(TERMINAL_1_I))
                    return TERMINAL_1I_NB;
                else if (terminalName.Equals(TERMINAL_2_I))
                    return TERMINAL_2I_NB;
                else if (terminalName.Equals(TERMINAL_3I))
                    return TERMINAL_3I_NB;
                else if (terminalName.Equals(TERMINAL_ENG))
                    return TERMINAL_ENG_NB;
                return 0;
            }
            #endregion

            internal const int DEFAULT_DOWN_TIME = 60;
            internal const string DEFAULT_DOWN_TIME_TAG = "Default Down Time";
            public void computeFlightDownTime()
            {                
                if (arrivalFlightsDictionary != null && arrivalFlightsDictionary.Count > 0
                    && departureFlightsDictionary != null && departureFlightsDictionary.Count > 0)
                {                    
                    foreach (KeyValuePair<String, DubaiFlight> pair in arrivalFlightsDictionary)
                    {                        
                        DubaiFlight arrivalFlight = pair.Value;
                        String linkedFlightNb = arrivalFlight.linkedFlightNb;
                        List<DubaiFlight> departureFlights = departureFlightsDictionary.Values.ToList<DubaiFlight>();

                        List<DubaiFlight> linkedFlights = getLinkedFlightsFromDictionary(linkedFlightNb, departureFlights);
                        DateTime flightCompleteDate = arrivalFlight.flightDate.AddMinutes(arrivalFlight.flightTime.TotalMinutes);

                        double downTime = getDownTimeInMinutesBetweenArrivalAndNextDeparture(flightCompleteDate, linkedFlights);
                        if (downTime != Double.MaxValue)
                        {
                            arrivalFlight.downTime = downTime;
                        }
                        else
                        {
                            arrivalFlight.downTime = DEFAULT_DOWN_TIME;
                            arrivalFlight.hasDefaultDownTime = true;
                            //arrivalFlight.arrivalOrDepartureTag = DEFAULT_DOWN_TIME_TAG;
                        }
                    }
                    foreach (KeyValuePair<String, DubaiFlight> pair in departureFlightsDictionary)
                    {                        
                        DubaiFlight departureFlight = pair.Value;
                        String linkedFlightNb = departureFlight.linkedFlightNb;
                        List<DubaiFlight> arrivalFlights = arrivalFlightsDictionary.Values.ToList<DubaiFlight>();

                        List<DubaiFlight> linkedFlights = getLinkedFlightsFromDictionary(linkedFlightNb, arrivalFlights);
                        DateTime flightCompleteDate = departureFlight.flightDate.AddMinutes(departureFlight.flightTime.TotalMinutes);

                        double downTime = getDownTimeInMinutesBetweenDepartureAndPreviousArrival(flightCompleteDate, linkedFlights);
                        if (downTime != Double.MaxValue)
                        {
                            departureFlight.downTime = downTime;
                        }
                        else
                        {
                            departureFlight.downTime = DEFAULT_DOWN_TIME;
                            departureFlight.hasDefaultDownTime = true;
                            //departureFlight.arrivalOrDepartureTag = DEFAULT_DOWN_TIME_TAG;
                        }
                    }
                }            
            }

            private List<DubaiFlight> getLinkedFlightsFromDictionary(String linkedFlightNb, List<DubaiFlight> flightsList)
            {
                List<DubaiFlight> linkedFlights = new List<DubaiFlight>();

                if (linkedFlightNb != null && linkedFlightNb != ""
                    && flightsList != null && flightsList.Count > 0)
                {
                    foreach (DubaiFlight flight in flightsList)
                    {
                        if (flight.flightNumber != null && flight.flightNumber.Equals(linkedFlightNb))
                        {
                            linkedFlights.Add(flight);
                        }
                    }
                }
                return linkedFlights;
            }

            private double getDownTimeInMinutesBetweenArrivalAndNextDeparture(DateTime flightCompleteDate, List<DubaiFlight> linkedFlights)
            {
                double downTime = Double.MaxValue;
                if (flightCompleteDate != null && linkedFlights != null && linkedFlights.Count > 0)
                {                    
                    foreach (DubaiFlight linkFlight in linkedFlights)
                    {
                        DateTime linkFlightCompleteDate = linkFlight.flightDate.AddMinutes(linkFlight.flightTime.TotalMinutes);
                        if (flightCompleteDate < linkFlightCompleteDate)
                        {
                            double minutesDifference = (linkFlightCompleteDate - flightCompleteDate).TotalMinutes;
                            if (minutesDifference < downTime)
                                downTime = minutesDifference;
                        }
                    }
                }
                return downTime;
            }

            private double getDownTimeInMinutesBetweenDepartureAndPreviousArrival(DateTime flightCompleteDate, List<DubaiFlight> linkedFlights)
            {
                double downTime = Double.MaxValue;
                if (flightCompleteDate != null && linkedFlights != null && linkedFlights.Count > 0)
                {
                    foreach (DubaiFlight linkFlight in linkedFlights)
                    {
                        DateTime linkFlightCompleteDate = linkFlight.flightDate.AddMinutes(linkFlight.flightTime.TotalMinutes);
                        if (flightCompleteDate > linkFlightCompleteDate)
                        {
                            double minutesDifference = (flightCompleteDate - linkFlightCompleteDate).TotalMinutes;
                            if (minutesDifference < downTime)
                                downTime = minutesDifference;
                        }
                    }
                }
                return downTime;
            }

            // >> Task #12843 Pax2Sim - Dubai - allocate using Cplex
            internal static string ZONE_F_1_CODE = "F_1";
            internal static string ZONE_F_2_CODE = "F_2";
            internal static string ZONE_F_3_CODE = "F_3";

            internal static string ZONE_E_1_CODE = "E_1";
            internal static string ZONE_E_2_CODE = "E_2";
            internal static string ZONE_E_3_CODE = "E_3";

            internal static string ZONE_C_1_CODE = "C_1";
            internal static string ZONE_C_2_CODE = "C_2";
            internal static string ZONE_C_3_CODE = "C_3";

            internal static string ZONE_F_1_REM_CODE = "FR_1";
            internal static string ZONE_F_2_REM_CODE = "FR_2";
            internal static string ZONE_F_3_REM_CODE = "FR_3";

            internal static string ZONE_E_1_REM_CODE = "ER_1";
            internal static string ZONE_E_2_REM_CODE = "ER_2";
            internal static string ZONE_E_3_REM_CODE = "ER_3";

            internal static string ZONE_C_1_REM_CODE = "CR_1";
            internal static string ZONE_C_2_REM_CODE = "CR_2";
            internal static string ZONE_C_3_REM_CODE = "CR_3";
                        
            internal static string ZONE_BIN_CODE = "BIN";
            
            List<string> zoneFAircraftTypeFirstChars = new List<string>(new string[] { "38" });
            List<string> zoneEAircraftTypeFirstChars = new List<string>(new string[] { "77", "74", "35", "34" });

            private string getParkingStandCodeByAircraftTypeAndTerminal(String aircraftType, int flightTerminalNb)
            {
                string parkingStandDistributionCode = "";

                if (aircraftType != null && aircraftType.Length >= 2)
                {
                    string firstTwoCharsFromAircraftCode = aircraftType.Substring(0, 2);
                    if (zoneFAircraftTypeFirstChars.Contains(firstTwoCharsFromAircraftCode))
                    {
                        if (flightTerminalNb == 1)
                        {
                            parkingStandDistributionCode = ZONE_F_1_CODE;
                        }
                        else if (flightTerminalNb == 2)
                        {
                            parkingStandDistributionCode = ZONE_F_2_CODE;
                        }
                        else if (flightTerminalNb == 3)
                        {
                            parkingStandDistributionCode = ZONE_F_3_CODE;
                        }
                    }
                    else if (zoneEAircraftTypeFirstChars.Contains(firstTwoCharsFromAircraftCode))
                    {
                        if (flightTerminalNb == 1)
                        {
                            parkingStandDistributionCode = ZONE_E_1_CODE;
                        }
                        else if (flightTerminalNb == 2)
                        {
                            parkingStandDistributionCode = ZONE_E_2_CODE;
                        }
                        else if (flightTerminalNb == 3)
                        {
                            parkingStandDistributionCode = ZONE_E_3_CODE;
                        }
                    }
                    else
                    {
                        if (flightTerminalNb == 1)
                        {
                            parkingStandDistributionCode = ZONE_C_1_CODE;
                        }
                        else if (flightTerminalNb == 2)
                        {
                            parkingStandDistributionCode = ZONE_C_2_CODE;
                        }
                        else if (flightTerminalNb == 3)
                        {
                            parkingStandDistributionCode = ZONE_C_3_CODE;
                        }
                    }
                }
                return parkingStandDistributionCode;
            }
            // << Task #12843 Pax2Sim - Dubai - allocate using Cplex
        }

        internal class FlightPlanUpdate
        {
            private DataTable currentArrivalFlightPlan;
            private DataTable currentDepartureFlightPlan;
            private Dictionary<String, DubaiFlight> arrivalFlightsDictionary = new Dictionary<String, DubaiFlight>();
            private Dictionary<String, DubaiFlight> departureFlightsDictionary = new Dictionary<String, DubaiFlight>();

            public FlightPlanUpdate(Dictionary<String, DubaiFlight> arrivalFlightsDictionary_,
                Dictionary<String, DubaiFlight> departureFlightsDictionary_, DataTable currentArrivalFlightPlan_,
                DataTable currentDepartureFlightPlan_)
            {
                this.arrivalFlightsDictionary = arrivalFlightsDictionary_;
                this.departureFlightsDictionary = departureFlightsDictionary_;
                this.currentArrivalFlightPlan = currentArrivalFlightPlan_;
                this.currentDepartureFlightPlan = currentDepartureFlightPlan_;
            }
            
            public DataTable generateArrivalFlightPlanWithImportedData()
            {
                DataTable flightPlanWithImportedData = null;
                if (currentArrivalFlightPlan != null && arrivalFlightsDictionary.Count > 0)
                {
                    flightPlanWithImportedData = currentArrivalFlightPlan.Clone();

                    int rowNb = 1;
                    foreach (KeyValuePair<String, DubaiFlight> arrivalFlightPair in arrivalFlightsDictionary)
                    {
                        DataRow newFPRow = flightPlanWithImportedData.NewRow();
                        DubaiFlight flight = arrivalFlightPair.Value;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_ID] = rowNb;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_DATE] = flight.flightDate;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_STA) != -1)
                            newFPRow[GlobalNames.sFPA_Column_STA] = flight.flightTime;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_AirlineCode] = flight.airlineCode;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_FlightN] = flight.flightNumber;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_AirportCode] = flight.airportCode;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_FlightCategory] = flight.flightCategory;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_AircraftType] = flight.aircraftType;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM) != -1)
                            newFPRow[GlobalNames.sFPA_Column_NoBSM] = false;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_CBP) != -1)
                            newFPRow[GlobalNames.sFPA_Column_CBP] = false;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_NbSeats] = flight.nbSeats;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_TerminalGate] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate) != -1)
                            newFPRow[GlobalNames.sFPA_Column_ArrivalGate] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim) != -1)
                            newFPRow[GlobalNames.sFPA_Column_TerminalReclaim] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_ReclaimObject] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_TerminalInfeedObject] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_StartArrivalInfeedObject] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_EndArrivalInfeedObject] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_TransferInfeedObject] = 0;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_TerminalParking] = flight.parkingTerminalNb;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_Parking] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_RunWay] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1) != -1)
                        {
                            String defaultDownTimeIndicator = "";
                            if (flight.hasDefaultDownTime)
                            {
                                defaultDownTimeIndicator = " (None after arrival date)";
                            }
                            newFPRow[GlobalNames.sFPD_A_Column_User1] = "A: " + flight.flightNumber + " - " + "D: " + flight.linkedFlightNb + defaultDownTimeIndicator;
                        }
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User2] = flight.downTime;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User3] = flight.arrivalOrDepartureTag + ", " + "Terminal: " + flight.terminalName;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User4] = flight.terminalName;

                        flightPlanWithImportedData.Rows.Add(newFPRow);
                        rowNb++;
                    }

                    flightPlanWithImportedData.AcceptChanges();
                }
                return flightPlanWithImportedData;
            }

            public DataTable generateDepartureFlightPlanWithImportedData()
            {
                DataTable flightPlanWithImportedData = null;
                if (currentDepartureFlightPlan != null && departureFlightsDictionary.Count > 0)
                {
                    flightPlanWithImportedData = currentDepartureFlightPlan.Clone();

                    int rowNb = 1;
                    foreach (KeyValuePair<String, DubaiFlight> departureFlightPair in departureFlightsDictionary)
                    {
                        DataRow newFPRow = flightPlanWithImportedData.NewRow();
                        DubaiFlight flight = departureFlightPair.Value;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_ID] = rowNb;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_DATE] = flight.flightDate;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_STD) != -1)
                            newFPRow[GlobalNames.sFPD_Column_STD] = flight.flightTime;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_AirlineCode] = flight.airlineCode;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_FlightN] = flight.flightNumber;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_AirportCode] = flight.airportCode;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_FlightCategory] = flight.flightCategory;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_AircraftType] = flight.aircraftType;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TSA) != -1)
                            newFPRow[GlobalNames.sFPD_Column_TSA] = false;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_NbSeats] = flight.nbSeats;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI) != -1)
                            newFPRow[GlobalNames.sFPD_Column_TerminalCI] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_CI_Start] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_CI_End] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_FB_CI_Start] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_FB_CI_End] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_Drop_Start] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_Drop_End] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_FB_Drop_Start] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_FB_Drop_End] = 0;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_TerminalGate] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate) != -1)
                            newFPRow[GlobalNames.sFPD_Column_BoardingGate] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup) != -1)
                            newFPRow[GlobalNames.sFPD_Column_TerminalMup] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_Mup_Start] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_Mup_End] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_First_Mup_Start] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_First_Mup_End] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_TerminalParking] = flight.parkingTerminalNb;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_Parking] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_RunWay] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1) != -1)
                        {
                            String defaultDownTimeIndicator = "";
                            if (flight.hasDefaultDownTime)
                            {
                                defaultDownTimeIndicator = " (None before departure date) ";
                            }
                            newFPRow[GlobalNames.sFPD_A_Column_User1] = "A: " + flight.linkedFlightNb + defaultDownTimeIndicator + " - " + "D: " + flight.flightNumber;
                        }
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User2] = flight.downTime;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User3] = flight.arrivalOrDepartureTag + ", " + "Terminal: " + flight.terminalName;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User4] = flight.terminalName;
                        // >> Task #12843 Pax2Sim - Dubai - allocate using Cplex
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User5] = flight.parkingStandDistributionCode;                    
                        // << Task #12843 Pax2Sim - Dubai - allocate using Cplex

                        flightPlanWithImportedData.Rows.Add(newFPRow);
                        rowNb++;
                    }
                    flightPlanWithImportedData.AcceptChanges();
                }
                return flightPlanWithImportedData;
            }
        
        }

        public static DataTable createFlightCategoryTableWithImportedData(DataTable flightCategoryTable, List<String> importedFlightCategories)
        {
            if (flightCategoryTable == null || importedFlightCategories == null
                || importedFlightCategories.Count == 0)
            {
                return flightCategoryTable;
            }
            DataTable newFlightCategoryTable = flightCategoryTable.Copy();
            List<String> currentFlightCategories = getValuesFromTableByColumnName(flightCategoryTable, GlobalNames.sFPFlightCategory_FC);
            int flightCategoryColumnIndex = flightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC);

            if (currentFlightCategories.Count > 0 &&
                 flightCategoryColumnIndex != -1)
            {
                foreach (String importedFlightCategory in importedFlightCategories)
                {
                    if (!currentFlightCategories.Contains(importedFlightCategory))
                    {
                        DataRow newRow = newFlightCategoryTable.NewRow();
                        newRow[flightCategoryColumnIndex] = importedFlightCategory;
                        newFlightCategoryTable.Rows.Add(newRow);
                    }
                }
                newFlightCategoryTable.AcceptChanges();
            }
            return newFlightCategoryTable;
        }

        private static List<String> getValuesFromTableByColumnName(DataTable givenTable, String columnName)
        {
            List<String> valuesList = new List<String>();
            if (givenTable == null || givenTable.Rows.Count == 0
                || columnName == null)
            {
                return valuesList;
            }

            int valueColumnIndex = givenTable.Columns.IndexOf(columnName);

            if (valueColumnIndex != -1)
            {
                foreach (DataRow row in givenTable.Rows)
                {
                    if (row[valueColumnIndex] != null
                        && row[valueColumnIndex].ToString() != null)
                    {
                        valuesList.Add(row[valueColumnIndex].ToString().Trim());
                    }
                }
            }
            return valuesList;
        }

        internal class FlightPlanInformation
        {
            DateTime scenarioStartDate = DateTime.MinValue;

            DataTable filteredFlightPlan = null;
            DataTable partialAllocation = null;
            DataTable cplexAllocation = null;

            ArrayList fpiMessages = new ArrayList();

            Dictionary<string, FilteredFlight> flightsFromFilteredFlightPlan = new Dictionary<string, FilteredFlight>();
            Dictionary<string, AllocatedFlight> flightsFromPartialAllocationTable = new Dictionary<string, AllocatedFlight>();
            Dictionary<string, AllocatedFlight> flightsFromCplexAllocationTable = new Dictionary<string, AllocatedFlight>();
            
            internal const string FLIGHT_ID_CPLEX_COLUMN_NAME = "FlightId";
            internal const string FLIGHT_NB_CPLEX_COLUMN_NAME = "FlightNumber";
            internal const string FLIGHT_DATE_CPLEX_COLUMN_NAME = " FlightDate";
            internal const string FLIGHT_TERMINAL_NB_CPLEX_COLUMN_NAME = "FlightTerminalNb";
            internal const string FLIGHT_ALLOC_TYPE_CPLEX_COLUMN_NAME = "FlightAllocationType";
            internal const string FLIGHT_ALLOC_TYPE_LIST_CPLEX_COLUMN_NAME = " FlightAllocationTypeList";
            internal const string DESK_CPLEX_COLUMN_NAME = " DeskName";
            internal const string OCC_START_CPLEX_COLUMN_NAME = "OccupationStartMinute";
            internal const string OCC_END_CPLEX_COLUMN_NAME = "OccupationEndMinute";
            internal const string OCC_DURATION_CPLEX_COLUMN_NAME = "OccupationTotalMinutes";

            internal const string DELIMITER = "[+]";

            public FlightPlanInformation(DataTable _filteredFlightPlan, 
                DataTable _cplexAllocation, DataTable _partialAllocation, ArrayList _fpiMessages, DateTime _fromDate)
            {
                scenarioStartDate = _fromDate;
                filteredFlightPlan = _filteredFlightPlan;
                partialAllocation = _partialAllocation;
                cplexAllocation = _cplexAllocation;
                fpiMessages = _fpiMessages;
            }

            public DataTable generateFlightPlanInformationTable()
            {
                DataTable flightPlanInformationTable = createFlightPlanInformationTableStructure();

                getFlightsFromFilteredFlightPlan();
                getFlightsFromPartialAllocationTable();
                getFlightsFromCplexAllocationTable();

                fillFlightPlanInformationTable(flightPlanInformationTable);

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

                //flightPlanInformationTable.Columns.Add(resourceType + " First Desk", typeof(int));
                //flightPlanInformationTable.Columns.Add(resourceType + " Last Desk", typeof(int));
                flightPlanInformationTable.Columns.Add("Parking First Desk", typeof(int));
                flightPlanInformationTable.Columns.Add("Parking Last Desk", typeof(int));
                flightPlanInformationTable.Columns.Add(GlobalNames.FPI_Column_NbOfResourcesUsed, typeof(int));
                //flightPlanInformationTable.Columns.Add(resourceType + " Opening Time", typeof(DateTime));
                //flightPlanInformationTable.Columns.Add(resourceType + " Closing Time", typeof(DateTime));
                //flightPlanInformationTable.Columns.Add(observedTerminal + " Nb", typeof(int));
                flightPlanInformationTable.Columns.Add("Parking Opening Time", typeof(DateTime));
                flightPlanInformationTable.Columns.Add("Parking Closing Time", typeof(DateTime));
                flightPlanInformationTable.Columns.Add("Parking Terminal Nb", typeof(int));
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


                return flightPlanInformationTable;
            }

            private void getFlightsFromFilteredFlightPlan()
            {                
                flightsFromFilteredFlightPlan.Clear();
                if (filteredFlightPlan != null && filteredFlightPlan.Rows.Count > 0)
                {
                    #region Flight Plan Indexes
                    List<int> flightPlanColumnIndexes = new List<int>();
                    int idColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                    flightPlanColumnIndexes.Add(idColumnIndex);
                    int dateColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                    flightPlanColumnIndexes.Add(dateColumnIndex);
                    int stdColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
                    flightPlanColumnIndexes.Add(stdColumnIndex);
                    int airlineCodeColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                    flightPlanColumnIndexes.Add(airlineCodeColumnIndex);
                    int flightNbColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                    flightPlanColumnIndexes.Add(flightNbColumnIndex);
                    int airportCodeColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                    flightPlanColumnIndexes.Add(airportCodeColumnIndex);
                    int flightCategoryColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                    flightPlanColumnIndexes.Add(flightCategoryColumnIndex);
                    int aircraftTypeColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                    flightPlanColumnIndexes.Add(aircraftTypeColumnIndex);
                    int tsaColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
                    flightPlanColumnIndexes.Add(tsaColumnIndex);
                    int nbSeatsColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
                    flightPlanColumnIndexes.Add(nbSeatsColumnIndex);

                    int user1ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                    flightPlanColumnIndexes.Add(user1ColumnIndex);
                    int user2ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                    flightPlanColumnIndexes.Add(user2ColumnIndex);
                    int user3ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                    flightPlanColumnIndexes.Add(user3ColumnIndex);
                    int user4ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                    flightPlanColumnIndexes.Add(user4ColumnIndex);
                    int user5ColumnIndex = filteredFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
                    flightPlanColumnIndexes.Add(user5ColumnIndex);
                    #endregion

                    if (!areValidColumnIndexes(flightPlanColumnIndexes))
                        return;

                    int flightId = -1;
                    DateTime flightDate = DateTime.MinValue;
                    TimeSpan flightSTD = TimeSpan.MinValue;
                    bool tsa = false;
                    int nbSeats = -1;

                    foreach (DataRow filteredFProw in filteredFlightPlan.Rows)
                    {
                        FilteredFlight filteredFlight = new FilteredFlight();
                        if (filteredFProw[idColumnIndex] != null)
                        {
                            if (Int32.TryParse(filteredFProw[idColumnIndex].ToString(), out flightId))
                                filteredFlight.id = flightId;
                        }
                        if (filteredFProw[dateColumnIndex] != null)
                        {
                            if (DateTime.TryParse(filteredFProw[dateColumnIndex].ToString(), out flightDate))
                                filteredFlight.flightDate = flightDate;
                        }
                        if (filteredFProw[stdColumnIndex] != null)
                        {
                            if (TimeSpan.TryParse(filteredFProw[stdColumnIndex].ToString(), out flightSTD))
                                filteredFlight.flightTime = flightSTD;
                        }
                        if (filteredFProw[airlineCodeColumnIndex] != null)
                        {
                            filteredFlight.airlineCode = filteredFProw[airlineCodeColumnIndex].ToString();                            
                        }
                        if (filteredFProw[flightNbColumnIndex] != null)
                        {
                            filteredFlight.flightNumber = filteredFProw[flightNbColumnIndex].ToString();
                        }
                        if (filteredFProw[airportCodeColumnIndex] != null)
                        {
                            filteredFlight.airportCode = filteredFProw[airportCodeColumnIndex].ToString();
                        }
                        if (filteredFProw[flightCategoryColumnIndex] != null)
                        {
                            filteredFlight.flightCategory = filteredFProw[flightCategoryColumnIndex].ToString();
                        }
                        if (filteredFProw[aircraftTypeColumnIndex] != null)
                        {
                            filteredFlight.aircraftType = filteredFProw[aircraftTypeColumnIndex].ToString();
                        }
                        if (filteredFProw[tsaColumnIndex] != null)
                        {
                            if (Boolean.TryParse(filteredFProw[tsaColumnIndex].ToString(), out tsa))
                                filteredFlight.tsa = tsa;
                        }
                        if (filteredFProw[nbSeatsColumnIndex] != null)
                        {
                            if (Int32.TryParse(filteredFProw[nbSeatsColumnIndex].ToString(), out nbSeats))
                                filteredFlight.nbSeats = nbSeats;
                        }
                        if (filteredFProw[user1ColumnIndex] != null)
                        {
                            filteredFlight.user1 = filteredFProw[user1ColumnIndex].ToString();
                        }
                        if (filteredFProw[user1ColumnIndex] != null)
                        {
                            filteredFlight.user2 = filteredFProw[user2ColumnIndex].ToString();
                        }
                        if (filteredFProw[user1ColumnIndex] != null)
                        {
                            filteredFlight.user3 = filteredFProw[user3ColumnIndex].ToString();
                        }
                        if (filteredFProw[user1ColumnIndex] != null)
                        {
                            filteredFlight.user4 = filteredFProw[user4ColumnIndex].ToString();
                        }
                        if (filteredFProw[user1ColumnIndex] != null)
                        {
                            filteredFlight.user5 = filteredFProw[user5ColumnIndex].ToString();
                        }
                        if (!flightsFromFilteredFlightPlan.ContainsKey(filteredFlight.getUniqueKeyIdAndFlNb()))
                        {
                            flightsFromFilteredFlightPlan.Add(filteredFlight.getUniqueKeyIdAndFlNb(), filteredFlight);
                        }
                        else
                        {
                            fpiMessages.Add("The flight with the Flight Plan Id: " 
                                + filteredFlight.id + " was found more than once in the Filtered Flight Plan file.");
                        }
                    }
                }
            }

            private void getFlightsFromPartialAllocationTable()
            {
                flightsFromPartialAllocationTable.Clear();
                if (partialAllocation != null && partialAllocation.Rows.Count > 0)
                {
                    #region Partial Allocation Indexes
                    List<int> partialAllocationColumnIndexes = new List<int>();
                    int idColumnIndex = partialAllocation.Columns.IndexOf(FLIGHT_ID_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(idColumnIndex);

                    int flightNbColumnIndex = partialAllocation.Columns.IndexOf(FLIGHT_NB_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(flightNbColumnIndex);

                    int dateColumnIndex = partialAllocation.Columns.IndexOf(FLIGHT_DATE_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(dateColumnIndex);

                    int terminalNbCodeColumnIndex = partialAllocation.Columns.IndexOf(FLIGHT_TERMINAL_NB_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(terminalNbCodeColumnIndex);

                    int allocTypeColumnIndex = partialAllocation.Columns.IndexOf(FLIGHT_ALLOC_TYPE_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(allocTypeColumnIndex);

                    int allocTypeListColumnIndex = partialAllocation.Columns.IndexOf(FLIGHT_ALLOC_TYPE_LIST_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(allocTypeListColumnIndex);

                    int deskNameColumnIndex = partialAllocation.Columns.IndexOf(DESK_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(deskNameColumnIndex);

                    int occStartColumnIndex = partialAllocation.Columns.IndexOf(OCC_START_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(occStartColumnIndex);

                    int occEndColumnIndex = partialAllocation.Columns.IndexOf(OCC_END_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(occEndColumnIndex);

                    int occDurationColumnIndex = partialAllocation.Columns.IndexOf(OCC_DURATION_CPLEX_COLUMN_NAME);
                    partialAllocationColumnIndexes.Add(occDurationColumnIndex);
                    #endregion

                    if (!areValidColumnIndexes(partialAllocationColumnIndexes))
                        return;

                    int flightId = -1;
                    int terminalNb = -1;
                    int occupationMinute = -1;

                    foreach (DataRow partialAllocRow in partialAllocation.Rows)
                    {
                        AllocatedFlight allocatedPartialFlight = new AllocatedFlight();
                        if (partialAllocRow[idColumnIndex] != null)
                        {
                            if (Int32.TryParse(partialAllocRow[idColumnIndex].ToString(), out flightId))
                                allocatedPartialFlight.id = flightId;
                        }
                        if (partialAllocRow[dateColumnIndex] != null)
                        {
                            allocatedPartialFlight.flightCompleteDate = partialAllocRow[dateColumnIndex].ToString();
                        }
                        if (partialAllocRow[flightNbColumnIndex] != null)
                        {
                            allocatedPartialFlight.flightNumber = partialAllocRow[flightNbColumnIndex].ToString();
                        }
                        if (partialAllocRow[terminalNbCodeColumnIndex] != null)
                        {
                            if (Int32.TryParse(partialAllocRow[terminalNbCodeColumnIndex].ToString(), out terminalNb))
                                allocatedPartialFlight.terminalNb = terminalNb;
                        }
                        if (partialAllocRow[allocTypeColumnIndex] != null)
                        {
                            allocatedPartialFlight.flightAllocationType = partialAllocRow[allocTypeColumnIndex].ToString();
                        }
                        if (partialAllocRow[allocTypeListColumnIndex] != null)
                        {
                            allocatedPartialFlight.flightAllocationTypesPriorityList = partialAllocRow[allocTypeListColumnIndex].ToString();
                        }
                        if (partialAllocRow[deskNameColumnIndex] != null)
                        {
                            allocatedPartialFlight.resourceName = partialAllocRow[deskNameColumnIndex].ToString();
                        }
                        if (partialAllocRow[occStartColumnIndex] != null)
                        {
                            if (Int32.TryParse(partialAllocRow[occStartColumnIndex].ToString(), out occupationMinute))
                                allocatedPartialFlight.occupationStartMinute = occupationMinute;
                        }
                        if (partialAllocRow[occEndColumnIndex] != null)
                        {
                            if (Int32.TryParse(partialAllocRow[occEndColumnIndex].ToString(), out occupationMinute))
                                allocatedPartialFlight.occupationEndMinute = occupationMinute;
                        }
                        if (partialAllocRow[occDurationColumnIndex] != null)
                        {
                            if (Int32.TryParse(partialAllocRow[occDurationColumnIndex].ToString(), out occupationMinute))
                                allocatedPartialFlight.occupationDurationInMinutes = occupationMinute;
                        }
                        if (!flightsFromPartialAllocationTable.ContainsKey(allocatedPartialFlight.getUniqueKeyIdAndFlNb()))
                        {
                            flightsFromPartialAllocationTable.Add(allocatedPartialFlight.getUniqueKeyIdAndFlNb(), allocatedPartialFlight);
                        }
                        else
                        {
                            fpiMessages.Add("The flight with the Flight Plan Id: "
                                + allocatedPartialFlight.id + " was found more than once in the Partial Allocation file.");
                        }
                    }
                }                
            }

            private void getFlightsFromCplexAllocationTable()            
            {
                flightsFromCplexAllocationTable.Clear();
                if (cplexAllocation != null && cplexAllocation.Rows.Count > 0)
                {
                    #region Cplex Allocation Indexes
                    List<int> cplexAllocationColumnIndexes = new List<int>();
                    int idColumnIndex = cplexAllocation.Columns.IndexOf(FLIGHT_ID_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(idColumnIndex);

                    int flightNbColumnIndex = cplexAllocation.Columns.IndexOf(FLIGHT_NB_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(flightNbColumnIndex);

                    int dateColumnIndex = cplexAllocation.Columns.IndexOf(FLIGHT_DATE_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(dateColumnIndex);

                    int terminalNbCodeColumnIndex = cplexAllocation.Columns.IndexOf(FLIGHT_TERMINAL_NB_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(terminalNbCodeColumnIndex);

                    int allocTypeColumnIndex = cplexAllocation.Columns.IndexOf(FLIGHT_ALLOC_TYPE_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(allocTypeColumnIndex);

                    int allocTypeListColumnIndex = cplexAllocation.Columns.IndexOf(FLIGHT_ALLOC_TYPE_LIST_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(allocTypeListColumnIndex);

                    int deskNameColumnIndex = cplexAllocation.Columns.IndexOf(DESK_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(deskNameColumnIndex);

                    int occStartColumnIndex = cplexAllocation.Columns.IndexOf(OCC_START_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(occStartColumnIndex);

                    int occEndColumnIndex = cplexAllocation.Columns.IndexOf(OCC_END_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(occEndColumnIndex);

                    int occDurationColumnIndex = cplexAllocation.Columns.IndexOf(OCC_DURATION_CPLEX_COLUMN_NAME);
                    cplexAllocationColumnIndexes.Add(occDurationColumnIndex);
                    #endregion

                    if (!areValidColumnIndexes(cplexAllocationColumnIndexes))
                        return;

                    int flightId = -1;
                    int terminalNb = -1;
                    int occupationMinute = -1;

                    foreach (DataRow cplexAllocRow in cplexAllocation.Rows)
                    {
                        AllocatedFlight allocatedFlight = new AllocatedFlight();
                        if (cplexAllocRow[idColumnIndex] != null)
                        {
                            if (Int32.TryParse(cplexAllocRow[idColumnIndex].ToString(), out flightId))
                                allocatedFlight.id = flightId;
                        }
                        if (cplexAllocRow[dateColumnIndex] != null)
                        {                            
                            allocatedFlight.flightCompleteDate = cplexAllocRow[dateColumnIndex].ToString();
                        }
                        if (cplexAllocRow[flightNbColumnIndex] != null)
                        {
                            allocatedFlight.flightNumber = cplexAllocRow[flightNbColumnIndex].ToString();
                        }
                        if (cplexAllocRow[terminalNbCodeColumnIndex] != null)
                        {
                            if (Int32.TryParse(cplexAllocRow[terminalNbCodeColumnIndex].ToString(), out terminalNb))
                                allocatedFlight.terminalNb = terminalNb;
                        }
                        if (cplexAllocRow[allocTypeColumnIndex] != null)
                        {
                            allocatedFlight.flightAllocationType = cplexAllocRow[allocTypeColumnIndex].ToString();
                        }
                        if (cplexAllocRow[allocTypeListColumnIndex] != null)
                        {
                            allocatedFlight.flightAllocationTypesPriorityList = cplexAllocRow[allocTypeListColumnIndex].ToString();
                        }
                        if (cplexAllocRow[deskNameColumnIndex] != null)
                        {
                            allocatedFlight.resourceName = cplexAllocRow[deskNameColumnIndex].ToString();
                        }
                        if (cplexAllocRow[occStartColumnIndex] != null)
                        {
                            if (Int32.TryParse(cplexAllocRow[occStartColumnIndex].ToString(), out occupationMinute))
                                allocatedFlight.occupationStartMinute = occupationMinute;                            
                        }
                        if (cplexAllocRow[occEndColumnIndex] != null)
                        {
                            if (Int32.TryParse(cplexAllocRow[occEndColumnIndex].ToString(), out occupationMinute))
                                allocatedFlight.occupationEndMinute = occupationMinute;
                        }
                        if (cplexAllocRow[occDurationColumnIndex] != null)
                        {
                            if (Int32.TryParse(cplexAllocRow[occDurationColumnIndex].ToString(), out occupationMinute))
                                allocatedFlight.occupationDurationInMinutes = occupationMinute;
                        }
                        if (!flightsFromCplexAllocationTable.ContainsKey(allocatedFlight.getUniqueKeyIdAndFlNb()))
                        {
                            flightsFromCplexAllocationTable.Add(allocatedFlight.getUniqueKeyIdAndFlNb(), allocatedFlight);
                        }
                        else
                        {
                            fpiMessages.Add("The flight with the Flight Plan Id: "
                                + allocatedFlight.id + " was found more than once in the Cplex Allocation file.");
                        }
                    }
                }

            }

            private void fillFlightPlanInformationTable(DataTable fpiTable)
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

                int firstDeskColumnIndex = fpiTable.Columns.IndexOf("Parking First Desk");
                fpiIndexes.Add(firstDeskColumnIndex);

                int lastDeskColumnIndex = fpiTable.Columns.IndexOf("Parking Last Desk");
                fpiIndexes.Add(lastDeskColumnIndex);

                int nbResUsedColumnIndex = fpiTable.Columns.IndexOf(GlobalNames.FPI_Column_NbOfResourcesUsed);
                fpiIndexes.Add(nbResUsedColumnIndex);

                int openingTimeColumnIndex = fpiTable.Columns.IndexOf("Parking Opening Time");
                fpiIndexes.Add(openingTimeColumnIndex);

                int closingTimeColumnIndex = fpiTable.Columns.IndexOf("Parking Closing Time");
                fpiIndexes.Add(closingTimeColumnIndex);

                int nbColumnIndex = fpiTable.Columns.IndexOf("Parking Terminal Nb");
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

                foreach (KeyValuePair<string, AllocatedFlight> allocatedFlightPair in flightsFromCplexAllocationTable)
                {
                    if (flightsFromFilteredFlightPlan.ContainsKey(allocatedFlightPair.Key))
                    {
                        FilteredFlight filteredFlight = flightsFromFilteredFlightPlan[allocatedFlightPair.Key];
                        AllocatedFlight allocatedFlight = allocatedFlightPair.Value;

                        if (filteredFlight != null && allocatedFlight != null)
                        {
                            DataRow newFpiRow = fpiTable.NewRow();
                            newFpiRow[flightIdColumnIndex] = allocatedFlight.id;
                            newFpiRow[flightDateColumnIndex] = filteredFlight.flightDate;
                            newFpiRow[flightSTDColumnIndex] = filteredFlight.flightTime;
                            newFpiRow[airlineCodeColumnIndex] = filteredFlight.airlineCode;
                            newFpiRow[flightNbColumnIndex] = filteredFlight.flightNumber;
                            newFpiRow[airportCodeColumnIndex] = filteredFlight.airportCode;
                            newFpiRow[flightCategoryColumnIndex] = filteredFlight.flightCategory;
                            newFpiRow[aircraftTypeColumnIndex] = filteredFlight.aircraftType;
                            newFpiRow[tsaColumnIndex] = filteredFlight.tsa;
                            newFpiRow[nbSeatsColumnIndex] = filteredFlight.nbSeats;
                            //newFpiRow[nbPaxColumnIndex] = 0;
                            //newFpiRow[containerColumnIndex] = 0;
                            int deskNb = -1;
                            if (allocatedFlight.resourceName.Length > 3)
                            {
                                string deskNbAsString = allocatedFlight.resourceName.Trim().Substring(1, 3);
                                Int32.TryParse(deskNbAsString, out deskNb);
                                newFpiRow[firstDeskColumnIndex] = deskNb;
                                newFpiRow[lastDeskColumnIndex] = deskNb;
                            }
                            newFpiRow[nbResUsedColumnIndex] = 1;

                            double realOccupationEnd = allocatedFlight.occupationEndMinute - 60;//dead time previously added

                            newFpiRow[openingTimeColumnIndex] = scenarioStartDate.AddMinutes(allocatedFlight.occupationStartMinute);
                            newFpiRow[closingTimeColumnIndex] = scenarioStartDate.AddMinutes(realOccupationEnd);
                            newFpiRow[nbColumnIndex] = allocatedFlight.terminalNb;

                            newFpiRow[user1ColumnIndex] = filteredFlight.user1;
                            newFpiRow[user2ColumnIndex] = filteredFlight.user2;
                            newFpiRow[user3ColumnIndex] = filteredFlight.user3;
                            newFpiRow[user4ColumnIndex] = allocatedFlight.resourceName; //filteredFlight.user4;
                            newFpiRow[user5ColumnIndex] = filteredFlight.user5;

                            newFpiRow[allocTypeColumnIndex] = "Parking";
                            newFpiRow[resourceTypeNameColumnIndex] = "Parking";

                            newFpiRow[calculationTypeColumnIndex] = "OCT table based";
                            newFpiRow[octTableUsedColumnIndex] = allocatedFlight.flightAllocationTypesPriorityList;


                            fpiTable.Rows.Add(newFpiRow);
                        }
                    }
                }
                foreach (KeyValuePair<string, AllocatedFlight> allocatedPartialFlightPair in flightsFromPartialAllocationTable)
                {
                    string simpleFlightNb = "";
                    string filteredSTDFlightKey = "";
                    if (allocatedPartialFlightPair.Value != null && allocatedPartialFlightPair.Value.flightNumber.IndexOf("_") != -1)
                    {
                        simpleFlightNb = allocatedPartialFlightPair.Value.flightNumber.Substring(0, allocatedPartialFlightPair.Value.flightNumber.IndexOf("_"));
                        filteredSTDFlightKey = allocatedPartialFlightPair.Value.id + DELIMITER + simpleFlightNb + "_Part3(STD)";
                    }

                    if (flightsFromFilteredFlightPlan.ContainsKey(filteredSTDFlightKey))
                    {
                        FilteredFlight filteredFlight = flightsFromFilteredFlightPlan[filteredSTDFlightKey];
                        AllocatedFlight allocatedPartialFlight = allocatedPartialFlightPair.Value;

                        if (filteredFlight != null && allocatedPartialFlight != null)
                        {
                            DataRow newFpiRow = fpiTable.NewRow();
                            DateTime allocatedPartialFlightCompleteDate = DateTime.MinValue;
                            DateTime.TryParse(allocatedPartialFlight.flightCompleteDate, out allocatedPartialFlightCompleteDate);
                            newFpiRow[flightIdColumnIndex] = allocatedPartialFlight.id;
                            newFpiRow[flightDateColumnIndex] = allocatedPartialFlightCompleteDate.Date;
                            newFpiRow[flightSTDColumnIndex] = allocatedPartialFlightCompleteDate.TimeOfDay;
                            newFpiRow[airlineCodeColumnIndex] = filteredFlight.airlineCode;
                            newFpiRow[flightNbColumnIndex] = allocatedPartialFlight.flightNumber;
                            newFpiRow[airportCodeColumnIndex] = filteredFlight.airportCode;
                            newFpiRow[flightCategoryColumnIndex] = filteredFlight.flightCategory;
                            newFpiRow[aircraftTypeColumnIndex] = filteredFlight.aircraftType;
                            newFpiRow[tsaColumnIndex] = filteredFlight.tsa;
                            newFpiRow[nbSeatsColumnIndex] = filteredFlight.nbSeats;
                            
                            int deskNb = -1;
                            if (allocatedPartialFlight.resourceName.Length > 3)
                            {
                                string deskNbAsString = allocatedPartialFlight.resourceName.Trim().Substring(1, 3);
                                Int32.TryParse(deskNbAsString, out deskNb);
                                newFpiRow[firstDeskColumnIndex] = deskNb;
                                newFpiRow[lastDeskColumnIndex] = deskNb;
                            }
                            newFpiRow[nbResUsedColumnIndex] = 1;

                            double realOccupationEnd = allocatedPartialFlight.occupationEndMinute; //no dead time for Part2(BIN2) flights

                            newFpiRow[openingTimeColumnIndex] = scenarioStartDate.AddMinutes(allocatedPartialFlight.occupationStartMinute);
                            newFpiRow[closingTimeColumnIndex] = scenarioStartDate.AddMinutes(realOccupationEnd);
                            newFpiRow[nbColumnIndex] = allocatedPartialFlight.terminalNb;

                            newFpiRow[user1ColumnIndex] = filteredFlight.user1;
                            newFpiRow[user2ColumnIndex] = allocatedPartialFlight.occupationDurationInMinutes.ToString();
                            newFpiRow[user3ColumnIndex] = filteredFlight.user3;
                            newFpiRow[user4ColumnIndex] = allocatedPartialFlight.resourceName; //filteredFlight.user4;
                            newFpiRow[user5ColumnIndex] = filteredFlight.user5;

                            newFpiRow[allocTypeColumnIndex] = "Parking";
                            newFpiRow[resourceTypeNameColumnIndex] = "Parking";

                            newFpiRow[calculationTypeColumnIndex] = "OCT table based";
                            newFpiRow[octTableUsedColumnIndex] = allocatedPartialFlight.flightAllocationTypesPriorityList;


                            fpiTable.Rows.Add(newFpiRow);
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
            
            internal class FilteredFlight
            {                
                public int id { get; set; }
                public DateTime flightDate { get; set; }
                public TimeSpan flightTime { get; set; }
                public String airlineCode { get; set; }                
                public String flightNumber { get; set; }                
                public String airportCode { get; set; }
                public String flightCategory { get; set; }
                public String aircraftType { get; set; }                
                public Boolean tsa { get; set; }
                public Int32 nbSeats { get; set; }
                public String user1 { get; set; }
                public String user2 { get; set; }
                public String user3 { get; set; }
                public String user4 { get; set; }
                public String user5 { get; set; }

                public string getUniqueKeyIdAndFlNb()
                {
                    return id + DELIMITER + flightNumber;
                }
            }
            internal class AllocatedFlight
            {
                public int id { get; set; }
                public String flightNumber { get; set; }
                public String flightCompleteDate { get; set; }
                public int terminalNb { get; set; }
                public String flightAllocationType { get; set; }
                public String flightAllocationTypesPriorityList { get; set; }
                public String resourceName { get; set; }
                public int occupationStartMinute { get; set; }
                public int occupationEndMinute { get; set; }
                public int occupationDurationInMinutes { get; set; }

                public string getUniqueKeyIdAndFlNb()
                {
                    return id + DELIMITER + flightNumber;
                }
            }
        }

        internal class FlightPlanTextAllocation
        {
            ArrayList fpTextAllocationMessages = new ArrayList();
            DataTable flightPlanInformationTable = null;

            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;
            int step;

            List<string> resourceNameList = new List<string>();
            Dictionary<string, FPIFlight> flightsFromFlightPlanInformationTable = new Dictionary<string, FPIFlight>();

            internal const string FLIGHTPLAN_TEXT_ALLOCATION_TABLENAME = "Flight Plan Text Allocation";

            public FlightPlanTextAllocation(DataTable _flightPlanInformationTable,
                DateTime _fromDate, DateTime _toDate, int _step, ArrayList _fptaMessages)
            {
                flightPlanInformationTable = _flightPlanInformationTable;

                fromDate = _fromDate;
                toDate = _toDate;
                step = _step;

                fpTextAllocationMessages = _fptaMessages;
            }

            public DataTable generateTextAllocationTable()
            {
                getFlightsFromFlightPlanInformationTable();

                DataTable flightPlanTextAllocationTable = createFlightPlanTextAllocationTableStructure();
                fillFlightPlanTextAllocationTable(flightPlanTextAllocationTable);

                return flightPlanTextAllocationTable;
            }

            private void getFlightsFromFlightPlanInformationTable()
            {
                flightsFromFlightPlanInformationTable.Clear();
                if (flightPlanInformationTable != null && flightPlanInformationTable.Rows.Count > 0)
                {
                    #region Flight Plan Information Indexes
                    List<int> fpiIndexes = new List<int>();
                    int flightIdColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                    fpiIndexes.Add(flightIdColumnIndex);

                    int flightDateColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                    fpiIndexes.Add(flightDateColumnIndex);

                    int flightSTDColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
                    fpiIndexes.Add(flightSTDColumnIndex);

                    int airlineCodeColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                    fpiIndexes.Add(airlineCodeColumnIndex);

                    int flightNbColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                    fpiIndexes.Add(flightNbColumnIndex);

                    int airportCodeColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                    fpiIndexes.Add(airportCodeColumnIndex);

                    int flightCategoryColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                    fpiIndexes.Add(flightCategoryColumnIndex);

                    int aircraftTypeColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                    fpiIndexes.Add(aircraftTypeColumnIndex);

                    int nbSeatsColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
                    fpiIndexes.Add(nbSeatsColumnIndex);

                    int firstDeskColumnIndex = flightPlanInformationTable.Columns.IndexOf("Parking First Desk");
                    fpiIndexes.Add(firstDeskColumnIndex);

                    int lastDeskColumnIndex = flightPlanInformationTable.Columns.IndexOf("Parking Last Desk");
                    fpiIndexes.Add(lastDeskColumnIndex);

                    int openingTimeColumnIndex = flightPlanInformationTable.Columns.IndexOf("Parking Opening Time");
                    fpiIndexes.Add(openingTimeColumnIndex);

                    int closingTimeColumnIndex = flightPlanInformationTable.Columns.IndexOf("Parking Closing Time");
                    fpiIndexes.Add(closingTimeColumnIndex);

                    int terminalNbColumnIndex = flightPlanInformationTable.Columns.IndexOf("Parking Terminal Nb");
                    fpiIndexes.Add(terminalNbColumnIndex);

                    int user1ColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                    fpiIndexes.Add(user1ColumnIndex);

                    int user2ColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                    fpiIndexes.Add(user2ColumnIndex);

                    int user3ColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                    fpiIndexes.Add(user3ColumnIndex);

                    int user4ColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                    fpiIndexes.Add(user4ColumnIndex);

                    int user5ColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
                    fpiIndexes.Add(user5ColumnIndex);

                    int allocTypeColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.FPI_Column_Allocation_Type);
                    fpiIndexes.Add(allocTypeColumnIndex);

                    int resourceTypeNameColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.FPI_Column_Resource_Type_Name);
                    fpiIndexes.Add(resourceTypeNameColumnIndex);

                    int octTableUsedColumnIndex = flightPlanInformationTable.Columns.IndexOf(GlobalNames.FPI_Column_OCTTableUsed);
                    fpiIndexes.Add(octTableUsedColumnIndex);
                    #endregion

                    if (!areValidColumnIndexes(fpiIndexes))
                        return;

                    int flightId = -1;
                    DateTime flightDate = DateTime.MinValue;
                    TimeSpan flightSTD = TimeSpan.MinValue;                    
                    int nbSeats = -1;

                    int firstDesk = -1;
                    int lastDesk = -1;
                    int terminalnb = -1;
                    DateTime openingTime = DateTime.MinValue;
                    DateTime closingTime = DateTime.MinValue;

                    foreach (DataRow fpiRow in flightPlanInformationTable.Rows)
                    {
                        FPIFlight fpiFlight = new FPIFlight();
                        if (fpiRow[flightIdColumnIndex] != null)
                        {
                            if (Int32.TryParse(fpiRow[flightIdColumnIndex].ToString(), out flightId))
                                fpiFlight.id = flightId;
                        }
                        if (fpiRow[flightDateColumnIndex] != null)
                        {
                            if (DateTime.TryParse(fpiRow[flightDateColumnIndex].ToString(), out flightDate))
                                fpiFlight.flightDate = flightDate;
                        }
                        if (fpiRow[flightSTDColumnIndex] != null)
                        {
                            if (TimeSpan.TryParse(fpiRow[flightSTDColumnIndex].ToString(), out flightSTD))
                                fpiFlight.flightTime = flightSTD;
                        }
                        if (fpiRow[airlineCodeColumnIndex] != null)
                        {
                            fpiFlight.airlineCode = fpiRow[airlineCodeColumnIndex].ToString();
                        }
                        if (fpiRow[flightNbColumnIndex] != null)
                        {
                            fpiFlight.flightNumber = fpiRow[flightNbColumnIndex].ToString();
                        }
                        if (fpiRow[airportCodeColumnIndex] != null)
                        {
                            fpiFlight.airportCode = fpiRow[airportCodeColumnIndex].ToString();
                        }
                        if (fpiRow[flightCategoryColumnIndex] != null)
                        {
                            fpiFlight.flightCategory = fpiRow[flightCategoryColumnIndex].ToString();
                        }
                        if (fpiRow[aircraftTypeColumnIndex] != null)
                        {
                            fpiFlight.aircraftType = fpiRow[aircraftTypeColumnIndex].ToString();
                        }
                        if (fpiRow[nbSeatsColumnIndex] != null)
                        {
                            if (Int32.TryParse(fpiRow[nbSeatsColumnIndex].ToString(), out nbSeats))
                                fpiFlight.nbSeats = nbSeats;
                        }

                        if (fpiRow[firstDeskColumnIndex] != null)
                        {
                            if (Int32.TryParse(fpiRow[firstDeskColumnIndex].ToString(), out firstDesk))
                                fpiFlight.firstDesk = firstDesk;
                        }
                        if (fpiRow[lastDeskColumnIndex] != null)
                        {
                            if (Int32.TryParse(fpiRow[lastDeskColumnIndex].ToString(), out lastDesk))
                                fpiFlight.lastDesk = lastDesk;
                        }
                        if (fpiRow[terminalNbColumnIndex] != null)
                        {
                            if (Int32.TryParse(fpiRow[terminalNbColumnIndex].ToString(), out terminalnb))
                                fpiFlight.terminalNb = terminalnb;
                        }

                        if (fpiRow[openingTimeColumnIndex] != null)
                        {
                            if (DateTime.TryParse(fpiRow[openingTimeColumnIndex].ToString(), out openingTime))
                                fpiFlight.openingTime = openingTime;
                        }
                        if (fpiRow[closingTimeColumnIndex] != null)
                        {
                            if (DateTime.TryParse(fpiRow[closingTimeColumnIndex].ToString(), out closingTime))
                                fpiFlight.closingTime = closingTime;
                        }

                        if (fpiRow[user1ColumnIndex] != null)
                        {
                            fpiFlight.flightLinkInfo = fpiRow[user1ColumnIndex].ToString();
                        }
                        if (fpiRow[user2ColumnIndex] != null)
                        {
                            fpiFlight.user2 = fpiRow[user2ColumnIndex].ToString();
                        }
                        if (fpiRow[user3ColumnIndex] != null)
                        {
                            fpiFlight.user3 = fpiRow[user3ColumnIndex].ToString();
                        }
                        if (fpiRow[user4ColumnIndex] != null)
                        {
                            fpiFlight.resourceName = fpiRow[user4ColumnIndex].ToString().Trim();
                            if (!resourceNameList.Contains(fpiFlight.resourceName))
                                resourceNameList.Add(fpiFlight.resourceName);
                        }
                        if (fpiRow[user5ColumnIndex] != null)
                        {
                            fpiFlight.flightAllocationType = fpiRow[user5ColumnIndex].ToString();
                        }
                        if (fpiRow[octTableUsedColumnIndex] != null)
                        {
                            fpiFlight.flightAllocationTypesPriorityList = fpiRow[octTableUsedColumnIndex].ToString();
                        }

                        if (!flightsFromFlightPlanInformationTable.ContainsKey(fpiFlight.getUniqueKeyIdAndFlNb()))
                        {
                            flightsFromFlightPlanInformationTable.Add(fpiFlight.getUniqueKeyIdAndFlNb(), fpiFlight);
                        }
                        else
                        {
                            fpTextAllocationMessages.Add("The flight with the Flight Plan Information Id: "
                                + fpiFlight.id + " and flight number: " + fpiFlight.flightNumber + " was found more than once in the Flight Plan Information file.");
                        }
                    }
                }
            }

            private DataTable createFlightPlanTextAllocationTableStructure()
            {
                DataTable flightPlanTextAllocationTable = new DataTable(FLIGHTPLAN_TEXT_ALLOCATION_TABLENAME);

                flightPlanTextAllocationTable.Columns.Add(GlobalNames.sColumnTime, typeof(DateTime));
                foreach (string resourceName in resourceNameList)
                {
                    flightPlanTextAllocationTable.Columns.Add(resourceName, typeof(String));
                }

                flightPlanTextAllocationTable.Rows.Clear();
                OverallTools.DataFunctions.initialiserLignes(flightPlanTextAllocationTable, fromDate, toDate, step);
                foreach (DataRow row in flightPlanTextAllocationTable.Rows)
                {
                    row.BeginEdit();
                    for (int i = 1; i < row.ItemArray.Length; i++)
                        row[i] = "";
                }

                return flightPlanTextAllocationTable;
            }

            private void fillFlightPlanTextAllocationTable(DataTable flightPlanTextAllocationTable)
            {
                foreach (KeyValuePair<string, FPIFlight> fpiFlightPair in flightsFromFlightPlanInformationTable)
                {
                    FPIFlight fpiFlight = fpiFlightPair.Value;
                    string flightTextInGantt = fpiFlight.id + "_" + fpiFlight.flightNumber;

                    string resourceName = fpiFlight.resourceName;
                    double fromDateOpeningTimeMinutesDiff = (fpiFlight.openingTime - fromDate).TotalMinutes;
                    double fromDateClosingTimeMinutesDiff = (fpiFlight.closingTime - fromDate).TotalMinutes;
                    int openingTimeIndex = (int)(fromDateOpeningTimeMinutesDiff / step);
                    int closingTimeIndex = (int)(fromDateClosingTimeMinutesDiff / step) - 1;

                    if (flightPlanTextAllocationTable.Columns.IndexOf(resourceName) != -1)
                    {
                        for (int i = openingTimeIndex; i <= closingTimeIndex; i++)
                        {
                            if (flightPlanTextAllocationTable.Rows[i][resourceName].ToString() == "")
                                flightPlanTextAllocationTable.Rows[i][resourceName] += flightTextInGantt;
                            else
                                flightPlanTextAllocationTable.Rows[i][resourceName] += ", " + flightTextInGantt;
                        }
                    }
                }
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
            
            internal class FPIFlight
            {
                internal const string DELIMITER = "[+]";

                public int id { get; set; }
                public DateTime flightDate { get; set; }
                public TimeSpan flightTime { get; set; }
                public String airlineCode { get; set; }
                public String flightNumber { get; set; }
                public String airportCode { get; set; }
                public String flightCategory { get; set; }
                public String aircraftType { get; set; }                
                public Int32 nbSeats { get; set; }

                //public String user1 { get; set; }
                public String user2 { get; set; }
                public String user3 { get; set; }
                //public String user4 { get; set; }
                //public String user5 { get; set; }

                public int firstDesk { get; set; }
                public int lastDesk { get; set; }
                public int terminalNb { get; set; }
                public DateTime openingTime { get; set; }
                public DateTime closingTime { get; set; }

                public string flightLinkInfo { get; set; }
                public String flightAllocationType { get; set; }
                public String flightAllocationTypesPriorityList { get; set; }
                public String resourceName { get; set; }

                public string getUniqueKeyIdAndFlNb()
                {
                    return id + DELIMITER + flightNumber;
                }

            }
        }
    }    
}
