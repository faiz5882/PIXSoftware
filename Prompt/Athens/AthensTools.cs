using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Prompt.Athens
{
    static class AthensTools
    {
        internal const int AIRPORT_IATA_CODE_LENGTH = 3;
        internal const int AIRPORT_ICAO_CODE_LENGTH = 4;

        internal const String FLIGHT_CATEGORY_DOM_REGULAR = "DOM_J";
        internal const String FLIGHT_CATEGORY_DOM_CHARTER = "DOM_C";
        internal const String FLIGHT_CATEGORY_INTRA_SCHENGEN_REGULAR = "IS_J";
        internal const String FLIGHT_CATEGORY_INTRA_SCHENGEN_CHARTER = "IS_C";
        internal const String FLIGHT_CATEGORY_EXTRA_SCHENGEN_REGULAR = "ES_J";
        internal const String FLIGHT_CATEGORY_EXTRA_SCHENGEN_CHARTER = "ES_C";
               
        private const String SCHENGEN_INFO_INTRA_SCHENGEN = "D";
        private const String SCHENGEN_INFO_EXTRA_SCHENGEN = "I";
        private const String FLIGHT_TYPE_SCHEDULED = "J";
        private const String FLIGHT_TYPE_CHARTER = "C";

        internal const String AIRLINE_DATABASE_TEXT_FILE_NAME = "Airlines_Database.csv";

        //Flight Plan
        internal static String flightPlanFileArrivalFlightNumberColumnName = "Arrival";
        internal static String flightPlanFileArrSchengenInfoColumnName = "A_ID";
        internal static String flightPlanFileArrDateColumnName = "A_Date";
        internal static String flightPlanFileArrSTAColumnName = "A_STA";
        internal static String flightPlanFileArrDayOfWeekColumnName = "A_A";
        internal static String flightPlanFileArrFlightTypeColumnName = "A_Na";
        internal static String flightPlanFileArrOriginAirportCodeColumnName = "A_ORG";
        internal static String flightPlanFileArrDepartureTimeFromOrgColumnName = "A_STD ORG";
        internal static String flightPlanFileArrStopoverColumnName = "A_VIA";
        internal static String flightPlanFileArrParkingStandColumnName = "A_POS";        
        internal static String flightPlanFileArrReclaimBeltColumnName = "A_Belt";
        internal static String flightPlanFileAircraftTypeColumnName = "A_Type";
        internal static String flightPlanFileDepartureFlightNumberColumnName = "Departure";
        internal static String flightPlanFileDepSchengenInfoColumnName = "D_ID";
        internal static String flightPlanFileDepDateColumnName = "D_Date";
        internal static String flightPlanFileDepSTDColumnName = "D_STD";
        internal static String flightPlanFileDepDayOfWeekColumnName = "D_A";
        internal static String flightPlanFileDepFlightTypeColumnName = "D_Na";
        internal static String flightPlanFileDepStopoverColumnName = "D_VIA";
        internal static String flightPlanFileDepDestinationAirportCodeColumnName = "D_DES";
        internal static String flightPlanFileDepParkingStandColumnName = "D_POS";
        internal static String flightPlanFileDepDepartureGateColumnName = "D_Gate1";
        internal static String flightPlanFileDepCheckInColumnName = "D_Cki";

        internal static List<String> flightPlanFileColumns
            = new List<String>(new string[] 
            { 
                flightPlanFileArrivalFlightNumberColumnName, flightPlanFileArrSchengenInfoColumnName,
                flightPlanFileArrDateColumnName, flightPlanFileArrSTAColumnName, flightPlanFileArrDayOfWeekColumnName, flightPlanFileArrFlightTypeColumnName,
                flightPlanFileArrOriginAirportCodeColumnName, flightPlanFileArrDepartureTimeFromOrgColumnName, flightPlanFileArrStopoverColumnName,
                flightPlanFileArrParkingStandColumnName, flightPlanFileArrReclaimBeltColumnName, flightPlanFileAircraftTypeColumnName, 
                flightPlanFileDepartureFlightNumberColumnName, flightPlanFileDepSchengenInfoColumnName, flightPlanFileDepDateColumnName, 
                flightPlanFileDepSTDColumnName, flightPlanFileDepDayOfWeekColumnName,flightPlanFileDepFlightTypeColumnName, flightPlanFileDepStopoverColumnName,
                flightPlanFileDepDestinationAirportCodeColumnName,flightPlanFileDepParkingStandColumnName,  flightPlanFileDepDepartureGateColumnName,
                flightPlanFileDepCheckInColumnName
            });

        //Reclaim Allocation
        internal static String reclaimAllocationFileFlightNumberColumnName = "Flight";
        //internal static String reclaimAllocationFileOriginAirportColumnName = "From"; not used for now
        internal static String reclaimAllocationFileScheduleSTAColumnName = "Schedule";
        internal static String reclaimAllocationFileBelt1ColumnName = "Belt1";

        internal static List<String> reclaimAllocationFileColumns
            = new List<String>(new string[]
            {
                reclaimAllocationFileFlightNumberColumnName,
                reclaimAllocationFileScheduleSTAColumnName,
                reclaimAllocationFileBelt1ColumnName
            });

        //Local Airports
        internal static String localAirportsFileIATAColumnName = "IATA";
        internal static String localAirportsFileICAOColumnName = "ICAO";
        internal static String localAirportsFileAirportNameColumnName = "AIRPORT NAME";

        internal static List<String> localAirportsFileColumns
            = new List<String>(new string[] { localAirportsFileIATAColumnName, localAirportsFileICAOColumnName, localAirportsFileAirportNameColumnName });

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
       
        internal static List<AirportCode> localAirportCodes = new List<AirportCode>();
        internal static List<String> localAirportIATACodes = new List<String>();
        internal static List<String> localAirportICAOCodes = new List<String>();

        internal class AirportCode
        {
            internal String airportName;
            internal String airportIATACode;
            internal String airportICAOCode;

            public AirportCode(String name, String iata, String icao)
            {
                airportName = name;
                airportIATACode = iata;
                airportICAOCode = icao;
            }
        }

        public static void setUpLocalAirportCodesFromImportedTable(DataTable localAirportsTable, String localAirportsFileName,
            out ArrayList errorList, out List<ImportDataInformation> importDataInformationList)
        {
            errorList = new ArrayList();
            importDataInformationList = new List<ImportDataInformation>();

            localAirportCodes.Clear();
            localAirportIATACodes.Clear();
            localAirportICAOCodes.Clear();
            
            if (localAirportsTable != null && localAirportsTable.Rows.Count > 0)
            {
                if (!areColumnHeadersFromFileValid(localAirportsTable, localAirportsFileColumns))
                {
                    errorList.Add("The Local Airports table has wrong column names. Please check the Log.txt file.");
                    return;
                }
                int localAirportsFileIATACodeColumnIndex = localAirportsTable.Columns.IndexOf(localAirportsFileIATAColumnName);
                int localAirportsFileICAOCodeColumnIndex = localAirportsTable.Columns.IndexOf(localAirportsFileICAOColumnName);
                int localAirportsFileAirportNameColumnIndex = localAirportsTable.Columns.IndexOf(localAirportsFileAirportNameColumnName);

                if (localAirportsFileIATACodeColumnIndex != -1 && localAirportsFileICAOCodeColumnIndex != -1
                    && localAirportsFileAirportNameColumnIndex != -1)
                {
                    int infoIndex = 1;
                    //informationList.Add("\n" + "Parsing the Local Airports text file." + "\n");
                    
                    foreach (DataRow row in localAirportsTable.Rows)
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = localAirportsTable.TableName;
                        information.fileName = localAirportsFileName;

                        String iataCode = row[localAirportsFileIATACodeColumnIndex].ToString().Trim();
                        String icaoCode = row[localAirportsFileICAOCodeColumnIndex].ToString().Trim();
                        String airportName = row[localAirportsFileAirportNameColumnIndex].ToString().Trim();

                        if (iataCode.Length != AIRPORT_IATA_CODE_LENGTH)
                        {
                            information.lineNbFromFile = infoIndex;
                            information.message = "An Airport IATA code must have only " + AIRPORT_IATA_CODE_LENGTH + " characters. For "
                                + airportName + " found: " + iataCode + "." + " The line will be ignored.";
                            importDataInformationList.Add(information);
                            /*
                            informationList.Add("Local airports file info " + infoIndex + ". " 
                                + "IATA code must have only " + IATA_CODE_LENGTH + " characters. For " 
                                + airportName + " found: " + iataCode + "." + " The line will be ignored.");
                             */ 
                            infoIndex++;
                            continue;
                        }
                        if (icaoCode.Length != AIRPORT_ICAO_CODE_LENGTH)
                        {
                            information.lineNbFromFile = infoIndex;
                            information.message = "An Airport ICAO code must have only " + AIRPORT_ICAO_CODE_LENGTH + " characters. For "
                                + airportName + " found: " + icaoCode + "." + " The line will be ignored.";
                            importDataInformationList.Add(information);
                            /*
                            informationList.Add("Local airports file info " + infoIndex + ". " 
                                + "ICAO code must have only " + ICAO_CODE_LENGTH + " characters. For "
                                + airportName + " found: " + icaoCode + "." + " The line will be ignored.");
                             */ 
                            infoIndex++;
                            continue;
                        }

                        AirportCode localAirport = new AirportCode(airportName, iataCode, icaoCode);
                        localAirportCodes.Add(localAirport);
                                                
                        if (!localAirportIATACodes.Contains(iataCode))
                        {
                            localAirportIATACodes.Add(iataCode);                            
                        }
                        if (!localAirportICAOCodes.Contains(icaoCode))
                        {
                            localAirportICAOCodes.Add(icaoCode);
                        }
                    }
                }
            }            
        }

        internal const int AIRLINE_IATA_CODE_LENGTH = 2;
        internal const int AIRLINE_ICAO_CODE_LENGTH = 3;

        internal static String airlineDatabaseFileIATAColumnName = "TWO_LETTER_CODE";
        internal static String airlineDatabaseFileICAOColumnName = "THREE_LETTER_CODE";
        internal static String airlineDatabaseFileAirlineNameColumnName = "AIRLINE";

        internal static List<String> airlineDatabaseFileColumns
            = new List<String>(new string[] { airlineDatabaseFileIATAColumnName, airlineDatabaseFileICAOColumnName, airlineDatabaseFileAirlineNameColumnName });

        public static void setUpAirlineCodesFromAirlineDatabaseTable(DataTable airlineDatabaseTable,
            out Dictionary<String, String> airlineIATAKeyICAOValueDictionary, out Dictionary<String, String> airlineICAOKeyIATAValueDictionary,
            out ArrayList errorList, out List<ImportDataInformation> importDataInformationList)
        {
            airlineIATAKeyICAOValueDictionary = new Dictionary<string, string>();
            airlineICAOKeyIATAValueDictionary = new Dictionary<string, string>();

            errorList = new ArrayList();
            importDataInformationList = new List<ImportDataInformation>();

            if (airlineDatabaseTable != null && airlineDatabaseTable.Rows.Count > 0)
            {
                if (!areColumnHeadersFromFileValid(airlineDatabaseTable, airlineDatabaseFileColumns))
                {
                    errorList.Add("The Airline database table has wrong column names. Please check the Log.txt file.");
                    return;
                }
                int airlineIATACodeColumnIndex = airlineDatabaseTable.Columns.IndexOf(airlineDatabaseFileIATAColumnName);
                int airlineICAOCodeColumnIndex = airlineDatabaseTable.Columns.IndexOf(airlineDatabaseFileICAOColumnName);
                int airlineNameColumnIndex = airlineDatabaseTable.Columns.IndexOf(airlineDatabaseFileAirlineNameColumnName);

                if (airlineIATACodeColumnIndex != -1 && airlineICAOCodeColumnIndex != -1
                    && airlineNameColumnIndex != -1)
                {
                    int infoIndex = 1;

                    foreach (DataRow row in airlineDatabaseTable.Rows)
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = airlineDatabaseTable.TableName;
                        information.fileName = airlineDatabaseTable.TableName;

                        String iataCode = row[airlineIATACodeColumnIndex].ToString().Trim();
                        String icaoCode = row[airlineICAOCodeColumnIndex].ToString().Trim();
                        String airlineName = row[airlineNameColumnIndex].ToString().Trim();

                        if (iataCode.Length != AIRLINE_IATA_CODE_LENGTH)
                        {
                            information.lineNbFromFile = infoIndex;
                            information.message = "An Airline IATA code must have only " + AIRLINE_IATA_CODE_LENGTH + " characters. For "
                                + airlineName + " found: " + iataCode + "." + " The line will be ignored.";
                            importDataInformationList.Add(information);

                            infoIndex++;
                            continue;
                        }
                        if (icaoCode.Length != AIRLINE_ICAO_CODE_LENGTH)
                        {
                            information.lineNbFromFile = infoIndex;
                            information.message = "An Airline ICAO code must have only " + AIRLINE_ICAO_CODE_LENGTH + " characters. For "
                                + airlineName + " found: " + icaoCode + "." + " The line will be ignored.";
                            importDataInformationList.Add(information);

                            infoIndex++;
                            continue;
                        }

                        if (!airlineIATAKeyICAOValueDictionary.ContainsKey(iataCode))
                        {
                            airlineIATAKeyICAOValueDictionary.Add(iataCode, icaoCode);
                        }
                        if (!airlineICAOKeyIATAValueDictionary.ContainsKey(icaoCode))
                        {
                            airlineICAOKeyIATAValueDictionary.Add(icaoCode, iataCode);
                        }
                    }
                }
            }
        }

        internal class Flight
        {
            private const String KEY_DELIMITER = "[-]";

            public int succesfullRowId { get; set; }
            public DateTime flightDate { get; set; }
            public TimeSpan flightTime { get; set; }
            public String airlineIATACode { get; set; }
            public String airlineICAOCode { get; set; }
            public String flightNumber { get; set; }
            public String flightNumberWithoutAirline { get; set; }
            public String airportCode { get; set; }
            public String flightCategory { get; set; }
            public String aircraftType { get; set; }
            public Boolean noBSM { get; set; }
            public Boolean cbp { get; set; }
            public Boolean tsa { get; set; }
            public int checkInDeskStart { get; set; }
            public int checkInDeskEnd { get; set; }
            public int makeUpDeskStart { get; set; }
            public int makeUpDeskEnd { get; set; }
            public int reclaimDeskStart { get; set; }
            public int reclaimDeskEnd { get; set; }
            public String user1 { get; set; }
            public String user2 { get; set; }
            public String user3 { get; set; }
            public String user4 { get; set; }
            public String user5 { get; set; }
            /// <summary>
            /// Messages about the parsing process.
            /// </summary>
            public String infoMessage { get; set; }

            /// <summary>
            /// key = airlineIATACode + flightNumberWithoutAirline + delimiter + flightDate + delimiter + flightTime
            /// </summary>
            public String dictionaryIATAKey
            {
                get
                {
                    return airlineIATACode + flightNumberWithoutAirline + KEY_DELIMITER 
                        + flightDate + KEY_DELIMITER + flightTime;
                }
            }

            /// <summary>
            /// key = airlineIATACode + flightNumberWithoutAirline + delimiter + flightDate + delimiter + flightTime
            /// </summary>
            public String dictionaryICAOKey
            {
                get
                {
                    return airlineICAOCode + flightNumberWithoutAirline + KEY_DELIMITER
                        + flightDate + KEY_DELIMITER + flightTime;
                }
            }
        }

        internal class FlightPlanConversion
        {            
            List<String> allowedFlightTypes
                = new List<String>(new string[] { FLIGHT_TYPE_SCHEDULED, FLIGHT_TYPE_CHARTER });

            internal Dictionary<String, Flight> arrivalFlightsDictionary = new Dictionary<String, Flight>();
            internal Dictionary<String, Flight> departureFlightsDictionary = new Dictionary<String, Flight>();

            internal List<String> importedAirlineCodes = new List<String>();
            internal List<String> importedFlightCategories = new List<String>();
            internal List<String> importedAircraftTypes = new List<String>();

            DataTable rawFlightPlan;
            String flightPlanFileName;
            List<String> localAirportIATACodes = new List<String>();

            Dictionary<string, string> airlineIATAKeyICAOValueDictionary = new Dictionary<string, string>();
            Dictionary<string, string> airlineICAOKeyIATAValueDictionary = new Dictionary<string, string>();
            
            DateTime startDate;
            DateTime endDate;

            internal ArrayList errorList = new ArrayList();
            internal List<ImportDataInformation> infoList = new List<ImportDataInformation>();

            public FlightPlanConversion(DataTable _rawFlightPlan, String _fileName, List<String> _localAirportIATACodes,
                Dictionary<string, string> _airlineIATAKeyICAOValueDictionary, Dictionary<string, string> _airlineICAOKeyIATAValueDictionary,
                DateTime _startDate, DateTime _endDate)
            {
                arrivalFlightsDictionary.Clear();
                departureFlightsDictionary.Clear();

                importedAirlineCodes.Clear();
                importedFlightCategories.Clear();
                importedAircraftTypes.Clear();

                this.rawFlightPlan = _rawFlightPlan;
                this.flightPlanFileName = _fileName;
                this.localAirportIATACodes = _localAirportIATACodes;

                this.airlineIATAKeyICAOValueDictionary = _airlineIATAKeyICAOValueDictionary;
                this.airlineICAOKeyIATAValueDictionary = _airlineICAOKeyIATAValueDictionary;

                this.startDate = _startDate;
                this.endDate = _endDate;
            }

            public void convert()
            {
                if (rawFlightPlan == null || rawFlightPlan.Rows.Count == 0)
                {
                    errorList.Add("The imported flight plan doesn't have any data.");
                    return;
                }
                if (!AthensTools.areColumnHeadersFromFileValid(rawFlightPlan, AthensTools.flightPlanFileColumns))
                {
                    errorList.Add("The imported flight plan doesn't have valid columns. Please check the Log.txt file for details.");
                    return;
                }
                                
                List<ImportDataInformation> informationList = generateArrivalFlights();
                if (informationList.Count > 0)
                    infoList.AddRange(informationList);
                informationList = generateDepartureFlights();
                if (informationList.Count > 0)
                    infoList.AddRange(informationList);                
            }
            private List<ImportDataInformation> generateArrivalFlights()
            {
                List<ImportDataInformation> informationList = new List<ImportDataInformation>();
                
                int arrFlightNumberColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrivalFlightNumberColumnName);
                int arrSchengenInfoColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrSchengenInfoColumnName);
                int arrFlightDateColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrDateColumnName);
                int arrFlightTimeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrSTAColumnName);
                int arrFlightTypeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrFlightTypeColumnName);                
                int arrOriginAirportCodeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrOriginAirportCodeColumnName);
                int arrReclaimBeltColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrReclaimBeltColumnName);
                int arrAircraftTypeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileAircraftTypeColumnName);
                //user 1..5 info columns
                int arrDayOfWeekColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrDayOfWeekColumnName);
                int arrDepartureTimeFormOriginAirportColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrDepartureTimeFromOrgColumnName);
                int arrStopoverAirportCodeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrStopoverColumnName);
                int arrParkingStandColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileArrParkingStandColumnName);
                
                int rowIndex = 2;//row number 1 from the text file contains the column names
                int ignoredFlightInfoIndex = 1;

                //infoList.Add("\n" + "Parsing the Flight Plan text file for arrival flights." + "\n");

                foreach (DataRow rawFlightPlanRow in rawFlightPlan.Rows)
                {
                    ImportDataInformation information = new ImportDataInformation();
                    information.tableName = GlobalNames.FPATableName;
                    information.fileName = flightPlanFileName;

                    Flight flight = new Flight();

                    flight.succesfullRowId = rowIndex;

                    String arrFlightNumber = "";
                    String arrFlightType = "";

                    if (rawFlightPlanRow[arrFlightNumberColumnIndex] != null)
                        arrFlightNumber = rawFlightPlanRow[arrFlightNumberColumnIndex].ToString().Trim();
                    if (rawFlightPlanRow[arrFlightTypeColumnIndex] != null)                    
                        arrFlightType = rawFlightPlanRow[arrFlightTypeColumnIndex].ToString().Trim();

                    if (arrFlightNumber == null || arrFlightNumber == "")
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The arrival flight doesn't have a flight number. It will be ignored.";
                        infoList.Add(information);

                        //informationList.Add("Flight plan file (arrival) info " + ignoredFlightInfoIndex + ". " + "The arrival flight from the line " + rowIndex + " doesn't have a flight number. It will be ignored.");
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }

                    if (!allowedFlightTypes.Contains(arrFlightType))
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The arrival flight " + arrFlightNumber + " is not Scheduled or Charter. It will be ignored.";
                        infoList.Add(information);
                        
                        //informationList.Add("Flight plan file (arrival) info " + ignoredFlightInfoIndex + ". " + "The arrival flight " + arrFlightNumber + " from the line " + rowIndex + " is not Scheduled or Charter. It will be ignored.");
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    arrFlightNumber = removeAllWhiteSpacesFromString(arrFlightNumber);
                    flight.flightNumber = arrFlightNumber;

                    DateTime flightDate = DateTime.MinValue;                    
                    if (rawFlightPlanRow[arrFlightDateColumnIndex] != null)
                        flightDate = FonctionsType.getDate(rawFlightPlanRow[arrFlightDateColumnIndex]);
                    if (flightDate == DateTime.MinValue)
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column Date from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*
                        informationList.Add("Flight plan file (arrival) info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column Date from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    else
                    {                        
                        flight.flightDate = flightDate;                        
                    }

                    TimeSpan flightTime = TimeSpan.MinValue;
                    if (rawFlightPlanRow[arrFlightTimeColumnIndex] != null)
                        flightTime = FonctionsType.getTime(rawFlightPlanRow[arrFlightTimeColumnIndex]);
                    if (flightTime == TimeSpan.MinValue)
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column STA (flight time) from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (arrival) info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column STA (flight time) from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    else
                    {
                        flight.flightTime = flightTime;
                    }

                    DateTime completeFlightDate = flight.flightDate.Add(flight.flightTime);
                    if (completeFlightDate < startDate || completeFlightDate > endDate)
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The arrival flight " + arrFlightNumber
                            + " has a flight date (" + completeFlightDate + ") that is not inside the interval for loading the data. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (arrival) info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The arrival flight " + arrFlightNumber
                            + " has a flight date (" + completeFlightDate + ") that is not inside the interval for loading the data. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                                        
                    string airlineIATACode = "";
                    string airlineICAOCode = "";
                    string flightNumberWithoutAirlineCode = "";

                    getAirlineCodeFromFlightNumber(arrFlightNumber, out airlineIATACode,
                                                   out airlineICAOCode, out flightNumberWithoutAirlineCode);
                    if (airlineIATACode == "" && airlineICAOCode == "")
                    {
                        ImportDataInformation airlineInformation = new ImportDataInformation();
                        airlineInformation.tableName = GlobalNames.FPATableName;
                        airlineInformation.fileName = flightPlanFileName;
                        airlineInformation.lineNbFromFile = rowIndex;
                        airlineInformation.message = "Could not find an airline IATA or ICAO code for the arrival flight " + arrFlightNumber
                            + ". It will be ignored.";
                        infoList.Add(airlineInformation);

                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    else
                    {
                        if (airlineIATACode == "" && airlineICAOCode != "")
                        {
                            ImportDataInformation airlineInformation = new ImportDataInformation();
                            airlineInformation.tableName = GlobalNames.FPATableName;
                            airlineInformation.fileName = flightPlanFileName;
                            airlineInformation.lineNbFromFile = rowIndex;
                            airlineInformation.message = "Could only find the airline ICAO code for the arrival flight " + arrFlightNumber
                                + ". The flight will be taken into account.";
                            infoList.Add(airlineInformation);

                            flight.airlineICAOCode = airlineICAOCode;
                            flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                        }
                        else if (airlineIATACode != "" && airlineICAOCode == "")
                        {
                            ImportDataInformation airlineInformation = new ImportDataInformation();
                            airlineInformation.tableName = GlobalNames.FPATableName;
                            airlineInformation.fileName = flightPlanFileName;
                            airlineInformation.lineNbFromFile = rowIndex;
                            airlineInformation.message = "Could only find the airline IATA code for the arrival flight " + arrFlightNumber
                                + ". The flight will be taken into account.";
                            infoList.Add(airlineInformation);

                            flight.airlineIATACode = airlineIATACode;
                            flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                        }
                        else
                        {
                            flight.airlineIATACode = airlineIATACode;
                            flight.airlineICAOCode = airlineICAOCode;
                            flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                        }
                    }
                    if (!importedAirlineCodes.Contains(flight.airlineIATACode))
                        importedAirlineCodes.Add(flight.airlineIATACode);
                                        
                    String schengenInfo = "";
                    if (rawFlightPlanRow[arrSchengenInfoColumnIndex] != null)
                        schengenInfo = rawFlightPlanRow[arrSchengenInfoColumnIndex].ToString().Trim();
                    if (schengenInfo == null || schengenInfo == "")
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column ID (schengen - extra-schengen info) from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (arrival) info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The arrival flight " + arrFlightNumber 
                            + " doesn't have a valid value in the column ID (schengen - extra-schengen info) from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }

                    String originAirportCode = "";
                    if (rawFlightPlanRow[arrOriginAirportCodeColumnIndex] != null)
                        originAirportCode = rawFlightPlanRow[arrOriginAirportCodeColumnIndex].ToString().Trim();
                    if (originAirportCode == null || originAirportCode == "")
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column ORG (origin airport code) from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (arrival) info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column ORG (origin airport code) from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    flight.airportCode = originAirportCode;

                    flight.flightCategory = getFlightCategoryBySchengenInfoFlightTypeAndAirportCode(schengenInfo, 
                        arrFlightType, originAirportCode, this.localAirportIATACodes);

                    if (!importedFlightCategories.Contains(flight.flightCategory))
                        importedFlightCategories.Add(flight.flightCategory);

                    String aircraftType = "";
                    if (rawFlightPlanRow[arrAircraftTypeColumnIndex] != null)
                        aircraftType = rawFlightPlanRow[arrAircraftTypeColumnIndex].ToString().Trim();
                    if (aircraftType == null || aircraftType == "")
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column Type (Aircraft Type) from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (arrival) info " + ignoredFlightInfoIndex + ". " + "Line " + rowIndex + ": The arrival flight " + arrFlightNumber
                            + " doesn't have a valid value in the column Type (Aircraft Type) from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    flight.aircraftType = aircraftType;
                    if (!importedAircraftTypes.Contains(flight.aircraftType))
                        importedAircraftTypes.Add(flight.aircraftType);

                    int reclaimBeltNb = 0;
                    if (rawFlightPlanRow[arrReclaimBeltColumnIndex] != null)                        
                    {
                        Int32.TryParse(rawFlightPlanRow[arrReclaimBeltColumnIndex].ToString(), out reclaimBeltNb);
                    }
                    if (reclaimBeltNb <= 0)
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "Reclaim Belt allocation not possible from the Flight plan file for the arrival flight "
                            + arrFlightNumber + ". The flight will be taken into account.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (arrival) Line " + rowIndex + ": Reclaim Belt allocation not possible from the Flight plan file for the arrival flight " 
                            + arrFlightNumber + ". The flight will be considered and an allocation will be tried from the Reclaim Allocation file.");*/
                    }
                    flight.reclaimDeskStart = reclaimBeltNb;
                    flight.reclaimDeskEnd = reclaimBeltNb;

                    if (rawFlightPlanRow[arrDayOfWeekColumnIndex] != null)
                    {
                        int dayNumber = -1;
                        if (Int32.TryParse(rawFlightPlanRow[arrDayOfWeekColumnIndex].ToString().Trim(), out dayNumber)
                            && dayNumber >= 0 && dayNumber <= 6)
                        {

                            String dayOfWeek = Enum.GetName(typeof(DayOfWeek), dayNumber) + " (Day " + dayNumber + ")";
                            flight.user1 = dayOfWeek;
                        }
                    }
                    if (rawFlightPlanRow[arrDepartureTimeFormOriginAirportColumnIndex] != null)
                    {
                        flight.user2 = "STD_ORG: " + rawFlightPlanRow[arrDepartureTimeFormOriginAirportColumnIndex].ToString().Trim();
                    }
                    if (rawFlightPlanRow[arrStopoverAirportCodeColumnIndex] != null)
                    {
                        flight.user3 = "VIA: " + rawFlightPlanRow[arrStopoverAirportCodeColumnIndex].ToString().Trim();
                    }
                    if (rawFlightPlanRow[arrParkingStandColumnIndex] != null)
                    {
                        flight.user4 = "POS: " + rawFlightPlanRow[arrParkingStandColumnIndex].ToString().Trim();
                    }
                    
                    if (flight.airlineIATACode != "")
                    {
                        arrivalFlightsDictionary.Add(flight.dictionaryIATAKey, flight);
                    }
                    else if (flight.airlineICAOCode != "")
                    {
                        arrivalFlightsDictionary.Add(flight.dictionaryICAOKey, flight);
                    }

                    rowIndex++;                
                }
                return informationList;
            }

            private List<ImportDataInformation> generateDepartureFlights()
            {
                List<ImportDataInformation> informationList = new List<ImportDataInformation>();

                int depFlightNumberColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepartureFlightNumberColumnName);
                int depSchengenInfoColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepSchengenInfoColumnName);
                int depFlightDateColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepDateColumnName);
                int depFlightTimeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepSTDColumnName);
                int depFlightTypeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepFlightTypeColumnName);
                int depDestinationAirportCodeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepDestinationAirportCodeColumnName);
                //aircraft type appears only once per line in the arrival side
                int aircraftTypeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileAircraftTypeColumnName);
                int depCheckInAllocationColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepCheckInColumnName);
                
                //user 1..5 info columns
                int depDayOfWeekColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepDayOfWeekColumnName);
                int depDepartureGate1ColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepDepartureGateColumnName);
                int depStopoverAirportCodeColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepStopoverColumnName);
                int depParkingStandColumnIndex = rawFlightPlan.Columns.IndexOf(AthensTools.flightPlanFileDepParkingStandColumnName);

                int rowIndex = 2;//row number 1 from the text file contains the column names
                int ignoredFlightInfoIndex = 1;

                //infoList.Add("\n" + "Parsing the Flight Plan text file for departure flights." + "\n");

                foreach (DataRow rawFlightPlanRow in rawFlightPlan.Rows)
                {
                    Flight flight = new Flight();

                    flight.succesfullRowId = rowIndex;

                    String depFlightNumber = "";
                    String depFlightType = "";

                    if (rawFlightPlanRow[depFlightNumberColumnIndex] != null)
                        depFlightNumber = rawFlightPlanRow[depFlightNumberColumnIndex].ToString().Trim();
                    if (rawFlightPlanRow[depFlightTypeColumnIndex] != null)
                        depFlightType = rawFlightPlanRow[depFlightTypeColumnIndex].ToString().Trim();

                    if (depFlightNumber == null || depFlightNumber == "")
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "The departure flight doesn't have a flight number. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) info " + ignoredFlightInfoIndex + ". " 
                            + "The departure flight from the line " + rowIndex + " doesn't have a flight number. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }

                    if (!allowedFlightTypes.Contains(depFlightType))
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "The departure flight " + depFlightNumber + " is not Scheduled or Charter. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) info " + ignoredFlightInfoIndex + ". " 
                            + "The departure flight " + depFlightNumber + " from the line " + rowIndex + " is not Scheduled or Charter. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    depFlightNumber = removeAllWhiteSpacesFromString(depFlightNumber);
                    flight.flightNumber = depFlightNumber;

                    DateTime flightDate = DateTime.MinValue;
                    if (rawFlightPlanRow[depFlightDateColumnIndex] != null)
                        flightDate = FonctionsType.getDate(rawFlightPlanRow[depFlightDateColumnIndex]);
                    if (flightDate == DateTime.MinValue)
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column Date from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) info " + ignoredFlightInfoIndex + ". " 
                            + "Line " + rowIndex + ": The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column Date from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    else
                    {
                        flight.flightDate = flightDate;
                    }

                    TimeSpan flightTime = TimeSpan.MinValue;
                    if (rawFlightPlanRow[depFlightTimeColumnIndex] != null)
                        flightTime = FonctionsType.getTime(rawFlightPlanRow[depFlightTimeColumnIndex]);
                    if (flightTime == TimeSpan.MinValue)
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column STD (flight time) from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) info " + ignoredFlightInfoIndex + ". "
                            + "Line " + rowIndex + ": The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column STD (flight time) from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    else
                    {
                        flight.flightTime = flightTime;
                    }

                    DateTime completeFlightDate = flight.flightDate.Add(flight.flightTime);
                    if (completeFlightDate < startDate || completeFlightDate > endDate)
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "The departure flight " + depFlightNumber + " has a flight date ("
                            + completeFlightDate + ") that is not inside the interval for loading the data. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) info " + ignoredFlightInfoIndex + ". " 
                            + "Line " + rowIndex + ": The departure flight " + depFlightNumber + " has a flight date (" 
                            + completeFlightDate + ") that is not inside the interval for loading the data. It will be ignored.");*/

                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }

                    string airlineIATACode = "";
                    string airlineICAOCode = "";
                    string flightNumberWithoutAirlineCode = "";

                    getAirlineCodeFromFlightNumber(depFlightNumber, out airlineIATACode, 
                                                   out airlineICAOCode, out flightNumberWithoutAirlineCode);
                    if (airlineIATACode == "" && airlineICAOCode == "")
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "Could not find an airline IATA or ICAO code for the departure flight " + depFlightNumber
                            + ". It will be ignored.";
                        infoList.Add(information);

                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    else
                    {
                        if (airlineIATACode == "" && airlineICAOCode != "")
                        {
                            ImportDataInformation information = new ImportDataInformation();
                            information.tableName = GlobalNames.FPDTableName;
                            information.fileName = flightPlanFileName;
                            information.lineNbFromFile = rowIndex;
                            information.message = "Could only find the airline ICAO code for the departure flight " + depFlightNumber
                                + ". The flight will be taken into account.";
                            infoList.Add(information);

                            flight.airlineICAOCode = airlineICAOCode;
                            flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                        }
                        else if (airlineIATACode != "" && airlineICAOCode == "")
                        {
                            ImportDataInformation information = new ImportDataInformation();
                            information.tableName = GlobalNames.FPDTableName;
                            information.fileName = flightPlanFileName;
                            information.lineNbFromFile = rowIndex;
                            information.message = "Could only find the airline IATA code for the departure flight " + depFlightNumber
                                + ". The flight will be taken into account.";
                            infoList.Add(information);

                            flight.airlineIATACode = airlineIATACode;
                            flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                        }
                        else
                        {
                            flight.airlineIATACode = airlineIATACode;
                            flight.airlineICAOCode = airlineICAOCode;
                            flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                        }
                    }

                    if (!importedAirlineCodes.Contains(flight.airlineIATACode))
                        importedAirlineCodes.Add(flight.airlineIATACode);

                    String schengenInfo = "";
                    if (rawFlightPlanRow[depSchengenInfoColumnIndex] != null)
                        schengenInfo = rawFlightPlanRow[depSchengenInfoColumnIndex].ToString().Trim();
                    if (schengenInfo == null || schengenInfo == "")
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column ID (schengen - extra-schengen info) from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) info " + ignoredFlightInfoIndex + ". " 
                            + "Line " + rowIndex + ": The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column ID (schengen - extra-schengen info) from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }

                    String destinationAirportCode = "";
                    if (rawFlightPlanRow[depDestinationAirportCodeColumnIndex] != null)
                        destinationAirportCode = rawFlightPlanRow[depDestinationAirportCodeColumnIndex].ToString().Trim();
                    if (destinationAirportCode == null || destinationAirportCode == "")
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column DES (destination airport code) from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) info " + ignoredFlightInfoIndex + ". " 
                            + "Line " + rowIndex + ": The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column DES (destination airport code) from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    flight.airportCode = destinationAirportCode;

                    flight.flightCategory = getFlightCategoryBySchengenInfoFlightTypeAndAirportCode(schengenInfo,
                        depFlightType, destinationAirportCode, this.localAirportIATACodes);

                    if (!importedFlightCategories.Contains(flight.flightCategory))
                        importedFlightCategories.Add(flight.flightCategory);

                    String aircraftType = "";
                    if (rawFlightPlanRow[aircraftTypeColumnIndex] != null)
                        aircraftType = rawFlightPlanRow[aircraftTypeColumnIndex].ToString().Trim();
                    if (aircraftType == null || aircraftType == "")
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column Type (Aircraft Type) from the Flight Plan text file. It will be ignored.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) info " + ignoredFlightInfoIndex + ". "
                            + "Line " + rowIndex + ": The departure flight " + depFlightNumber
                            + " doesn't have a valid value in the column Type (Aircraft Type) from the Flight Plan text file. It will be ignored.");*/
                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                    flight.aircraftType = aircraftType;
                    if (!importedAircraftTypes.Contains(flight.aircraftType))
                        importedAircraftTypes.Add(flight.aircraftType);

                    int checkInDeskStart = 0;
                    int checkInDeskEnd = 0;
                    if (rawFlightPlanRow[depCheckInAllocationColumnIndex] != null)
                    {
                        String checkInAllocationInfo = rawFlightPlanRow[depCheckInAllocationColumnIndex].ToString().Trim();
                        getCheckInAllocationFromFlightPlanValue(checkInAllocationInfo, out checkInDeskStart, out checkInDeskEnd);
                    }
                    if (checkInDeskStart <= 0 || checkInDeskEnd <= 0)
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = GlobalNames.FPDTableName;
                        information.fileName = flightPlanFileName;
                        information.lineNbFromFile = rowIndex;
                        information.message = "Check-In allocation not possible from the Flight plan file for the departure flight "
                            + depFlightNumber + ". The flight will be taken into account.";
                        infoList.Add(information);

                        /*informationList.Add("Flight plan file (departure) Line " + rowIndex 
                            + ": Check-In allocation not possible from the Flight plan file for the departure flight "
                            + depFlightNumber + ". The flight will be considered and an allocation will be tried from the Check-In Allocation file.");*/
                    }
                    flight.checkInDeskStart = checkInDeskStart;
                    flight.checkInDeskEnd = checkInDeskEnd;

                    //user1..5
                    if (rawFlightPlanRow[depDayOfWeekColumnIndex] != null)
                    {
                        int dayNumber = -1;
                        if (Int32.TryParse(rawFlightPlanRow[depDayOfWeekColumnIndex].ToString().Trim(), out dayNumber)
                            && dayNumber >= 0 && dayNumber <= 6)
                        {

                            String dayOfWeek = Enum.GetName(typeof(DayOfWeek), dayNumber) + " (Day " + dayNumber + ")";
                            flight.user1 = dayOfWeek;
                        }
                    }
                    if (rawFlightPlanRow[depDepartureGate1ColumnIndex] != null)
                    {
                        flight.user3 = "Gate1: " + rawFlightPlanRow[depDepartureGate1ColumnIndex].ToString().Trim();
                    }
                    if (rawFlightPlanRow[depStopoverAirportCodeColumnIndex] != null)
                    {
                        flight.user2 = "VIA: " + rawFlightPlanRow[depStopoverAirportCodeColumnIndex].ToString().Trim();
                    }
                    if (rawFlightPlanRow[depParkingStandColumnIndex] != null)
                    {
                        flight.user4 = "POS: " + rawFlightPlanRow[depParkingStandColumnIndex].ToString().Trim();
                    }

                    if (flight.airlineIATACode != "")
                    {
                        departureFlightsDictionary.Add(flight.dictionaryIATAKey, flight);
                    }
                    else if (flight.airlineICAOCode != "")
                    {
                        departureFlightsDictionary.Add(flight.dictionaryICAOKey, flight);
                    }

                    rowIndex++;
                }
                return informationList;                
            }

            private const char RANGE_SPLITTER = '-';
            private const char SINGLE_SPLITTER = '/';
            internal static void getCheckInAllocationFromFlightPlanValue(String checkInAllocationValue,
                out int firstDesk, out int lastDesk)
            {
                firstDesk = 0;
                lastDesk = 0;

                List<int> checkInDeskNumbers = new List<int>();
                checkInAllocationValue.Replace(" ", String.Empty);

                if (checkInAllocationValue.Contains(SINGLE_SPLITTER))
                {
                    string[] checkInStrings = checkInAllocationValue.Split(SINGLE_SPLITTER);
                    foreach (string checkInString in checkInStrings)
                    {
                        int checkInNb = 0;
                        if (Int32.TryParse(checkInString, out checkInNb))
                            checkInDeskNumbers.Add(checkInNb);
                    }                    
                    firstDesk = checkInDeskNumbers.Min();
                    lastDesk = checkInDeskNumbers.Max();
                }
                else if (checkInAllocationValue.Contains(RANGE_SPLITTER))
                {
                    string[] checkInStrings = checkInAllocationValue.Split(RANGE_SPLITTER);
                    if (checkInStrings.Length == 2)
                    {
                        Int32.TryParse(checkInStrings[0], out firstDesk);
                        Int32.TryParse(checkInStrings[1], out lastDesk);
                    }
                }
                else
                {
                    if (Int32.TryParse(checkInAllocationValue, out firstDesk))
                    {
                        lastDesk = firstDesk;
                    }
                }
            }

            private void getAirlineCodeFromFlightNumber(String flightNumber, 
                out string airlineIATACode, out string airlineICAOCode, out string flightNumberWithoutAirlineCode)
            {
                airlineIATACode = "";
                airlineICAOCode = "";
                flightNumberWithoutAirlineCode = "";

                if (flightNumber != null && flightNumber != "")
                {
                    if (flightNumber.Length <= Math.Min(AIRLINE_IATA_CODE_LENGTH, AIRLINE_ICAO_CODE_LENGTH))
                    {
                        return;
                    }

                    flightNumber.Trim();
                    flightNumber.Replace(" ", String.Empty);

                    String extractedAirlineICAOCode = flightNumber.Substring(0, AIRLINE_ICAO_CODE_LENGTH);
                    String extractedAirlineIATACode = flightNumber.Substring(0, AIRLINE_IATA_CODE_LENGTH);
                    
                    if (airlineICAOKeyIATAValueDictionary.ContainsKey(extractedAirlineICAOCode))
                    {
                        airlineICAOCode = extractedAirlineICAOCode;
                        airlineIATACode = airlineICAOKeyIATAValueDictionary[extractedAirlineICAOCode];
                        flightNumberWithoutAirlineCode = flightNumber.Substring(AIRLINE_ICAO_CODE_LENGTH);
                    }
                    else if (airlineIATAKeyICAOValueDictionary.ContainsKey(extractedAirlineIATACode))
                    {
                        airlineIATACode = extractedAirlineIATACode;
                        airlineICAOCode = airlineIATAKeyICAOValueDictionary[extractedAirlineIATACode];
                        flightNumberWithoutAirlineCode = flightNumber.Substring(AIRLINE_IATA_CODE_LENGTH);
                    }                    
                }                
            }
                        
            private String getFlightCategoryBySchengenInfoFlightTypeAndAirportCode(String schengenInfo, 
                String flightType, String airportCode, List<String> localAirportIATACodes)
            {
                String flightCategory = "";
                if (schengenInfo.Equals(SCHENGEN_INFO_EXTRA_SCHENGEN))
                {
                    if (flightType.Equals(FLIGHT_TYPE_SCHEDULED))
                        flightCategory = FLIGHT_CATEGORY_EXTRA_SCHENGEN_REGULAR;
                    else if (flightType.Equals(FLIGHT_TYPE_CHARTER))
                        flightCategory = FLIGHT_CATEGORY_EXTRA_SCHENGEN_CHARTER;
                }
                else if (schengenInfo.Equals(SCHENGEN_INFO_INTRA_SCHENGEN))
                {
                    if (localAirportIATACodes.Contains(airportCode))
                    {
                        if (flightType.Equals(FLIGHT_TYPE_SCHEDULED))
                            flightCategory = FLIGHT_CATEGORY_DOM_REGULAR;
                        else if (flightType.Equals(FLIGHT_TYPE_CHARTER))
                            flightCategory = FLIGHT_CATEGORY_DOM_CHARTER;
                    }
                    else
                    {
                        if (flightType.Equals(FLIGHT_TYPE_SCHEDULED))
                            flightCategory = FLIGHT_CATEGORY_INTRA_SCHENGEN_REGULAR;
                        else if (flightType.Equals(FLIGHT_TYPE_CHARTER))
                            flightCategory = FLIGHT_CATEGORY_INTRA_SCHENGEN_CHARTER;
                    }
                }
                return flightCategory;
            }

            private String removeAllWhiteSpacesFromString(String givenString)
            {
                if (givenString != null && givenString != "")
                {                    
                    givenString = givenString.Replace(" ", string.Empty);
                }
                return givenString;
            }
        }

        internal class FlightPlanUpdate
        {
            internal const string IMPORTED_FPA_TABLE_NAME = "Imported Arrival Flight Plan";
            internal const string IMPORTED_FPD_TABLE_NAME = "Imported Departure Flight Plan";

            private DataTable currentArrivalFlightPlan;
            private DataTable currentDepartureFlightPlan;
            private Dictionary<String, Flight> arrivalFlightsDictionary = new Dictionary<String, Flight>();
            private Dictionary<String, Flight> departureFlightsDictionary = new Dictionary<String, Flight>();

            public FlightPlanUpdate(Dictionary<String, Flight> arrivalFlightsDictionary_,
                Dictionary<String, Flight> departureFlightsDictionary_, DataTable currentArrivalFlightPlan_,
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
                    flightPlanWithImportedData.TableName = IMPORTED_FPA_TABLE_NAME;

                    int rowNb = 1;
                    foreach (KeyValuePair<String, Flight> arrivalFlightPair in arrivalFlightsDictionary)
                    {
                        DataRow newFPRow = flightPlanWithImportedData.NewRow();
                        Flight flight = arrivalFlightPair.Value;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_ID] = rowNb;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_DATE] = flight.flightDate;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_STA) != -1)
                            newFPRow[GlobalNames.sFPA_Column_STA] = flight.flightTime;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_AirlineCode] = flight.airlineIATACode;
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
                            newFPRow[GlobalNames.sFPD_A_Column_NbSeats] = 0;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_TerminalGate] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate) != -1)
                            newFPRow[GlobalNames.sFPA_Column_ArrivalGate] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim) != -1)
                            newFPRow[GlobalNames.sFPA_Column_TerminalReclaim] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_ReclaimObject] = flight.reclaimDeskStart;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_TerminalInfeedObject] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_StartArrivalInfeedObject] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_EndArrivalInfeedObject] = 0;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject) != -1)
                            newFPRow[GlobalNames.sFPA_Column_TransferInfeedObject] = 0;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_TerminalParking] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_Parking] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_RunWay] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User1] = flight.user1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User2] = flight.user2;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User3] = flight.user3;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User4] = flight.user4;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User5] = flight.user5;

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
                    flightPlanWithImportedData.TableName = IMPORTED_FPD_TABLE_NAME;

                    int rowNb = 1;
                    foreach (KeyValuePair<String, Flight> departureFlightPair in departureFlightsDictionary)
                    {
                        DataRow newFPRow = flightPlanWithImportedData.NewRow();
                        Flight flight = departureFlightPair.Value;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_ID] = rowNb;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_DATE] = flight.flightDate;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_STD) != -1)
                            newFPRow[GlobalNames.sFPD_Column_STD] = flight.flightTime;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_AirlineCode] = flight.airlineIATACode;
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
                            newFPRow[GlobalNames.sFPD_A_Column_NbSeats] = 0;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI) != -1)
                            newFPRow[GlobalNames.sFPD_Column_TerminalCI] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_CI_Start] = flight.checkInDeskStart;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_CI_End] = flight.checkInDeskEnd;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_FB_CI_Start] = flight.checkInDeskStart;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_FB_CI_End] = flight.checkInDeskEnd;

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
                            newFPRow[GlobalNames.sFPD_Column_Eco_Mup_Start] = flight.makeUpDeskStart;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_Eco_Mup_End] = flight.makeUpDeskEnd;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start) != -1)
                            newFPRow[GlobalNames.sFPD_Column_First_Mup_Start] = flight.makeUpDeskStart;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End) != -1)
                            newFPRow[GlobalNames.sFPD_Column_First_Mup_End] = flight.makeUpDeskEnd;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_TerminalParking] = 1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_Parking] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_RunWay] = 1;

                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User1] = flight.user1;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User2] = flight.user2;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User3] = flight.user3;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User4] = flight.user4;
                        if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5) != -1)
                            newFPRow[GlobalNames.sFPD_A_Column_User5] = flight.user5;

                        flightPlanWithImportedData.Rows.Add(newFPRow);
                        rowNb++;
                    }
                    flightPlanWithImportedData.AcceptChanges();
                }
                return flightPlanWithImportedData;
            }
        }

        internal class DeskAllocation
        {
            internal class ReclaimAllocation
            {
                DataTable rawReclaimAllocation;                
                internal Dictionary<String, Flight> arrivalFlightsDictionary = new Dictionary<String, Flight>();
                                
                internal ArrayList infoList = new ArrayList();

                public ReclaimAllocation(DataTable rawReclaimAllocation_, Dictionary<String, Flight> arrivalFlightsDictionary_)
                {
                    this.rawReclaimAllocation = rawReclaimAllocation_;
                    this.arrivalFlightsDictionary = arrivalFlightsDictionary_;
                }

                public Dictionary<String, Flight> convertAndUpdateAllocation()
                {
                    if (rawReclaimAllocation == null || rawReclaimAllocation.Rows.Count == 0)
                    {
                        infoList.Add("The imported reclaim allocation doesn't have any data."
                            + " Could not allocate the reclaim desk from the Reclaim Allocation file.");
                        return arrivalFlightsDictionary;
                    }
                    if (!AthensTools.areColumnHeadersFromFileValid(rawReclaimAllocation, AthensTools.reclaimAllocationFileColumns))
                    {
                        infoList.Add("The imported reclaim allocation doesn't have valid columns." 
                            + " Could not allocate a reclaim desk from the Reclaim Allocation file. Please check the Log.txt file for details.");
                        return arrivalFlightsDictionary;
                    }

                    int flightNumberColumnIndex = rawReclaimAllocation.Columns.IndexOf(AthensTools.reclaimAllocationFileFlightNumberColumnName);
                    int flightSTAColumnIndex = rawReclaimAllocation.Columns.IndexOf(AthensTools.reclaimAllocationFileScheduleSTAColumnName);
                    int reclaimDeskNbColumnIndex = rawReclaimAllocation.Columns.IndexOf(AthensTools.reclaimAllocationFileBelt1ColumnName);

                    Dictionary<String, Flight> reclaimAllocationFlightsDictionary = new Dictionary<String, Flight>();
                    int rowIndex = 2;
                    int ignoredFlightInfoIndex = 1;

                    infoList.Add("\n" + "Parsing the Reclaim Allocation text file." + "\n");

                    foreach (DataRow rawReclaimRow in rawReclaimAllocation.Rows)
                    {
                        Flight flight = new Flight();
                        String flightNb = "";
                        DateTime sta = DateTime.MinValue;
                        int reclaimDeskNb = -1;

                        flight.succesfullRowId = rowIndex;

                        if (rawReclaimRow[flightNumberColumnIndex] != null)
                            flightNb = rawReclaimRow[flightNumberColumnIndex].ToString().Trim();

                        if (flightNb == null || flightNb == "")
                        {
                            infoList.Add("Reclaim allocation file info " + ignoredFlightInfoIndex + ". "
                                + "The flight from the line " + rowIndex + " doesn't have a flight number."
                                + " Could not allocate a reclaim desk from the Reclaim Allocation file.");
                            rowIndex++;
                            ignoredFlightInfoIndex++;
                            continue;
                        }
                        else
                        {
                            flightNb = flightNb.Replace(" ", String.Empty);
                        }
                        flight.flightNumber = flightNb;

                        if (rawReclaimRow[flightSTAColumnIndex] != null)
                        {
                            sta = FonctionsType.getDate(rawReclaimRow[flightSTAColumnIndex]);

                            if (sta == DateTime.MinValue)
                            {
                                infoList.Add("Reclaim allocation file info " + ignoredFlightInfoIndex + ". "
                                    + "Line " + rowIndex + ": The flight " + flightNb
                                    + " doesn't have a valid value in the column Schedule (STA) from the Reclaim Allocation text file."
                                    + " Could not allocate a reclaim desk from the Reclaim Allocation file.");
                                rowIndex++;
                                ignoredFlightInfoIndex++;
                                continue;
                            }
                        }
                        flight.flightDate = sta.Date;
                        flight.flightTime = sta.TimeOfDay;

                        if (rawReclaimRow[reclaimDeskNbColumnIndex] != null)
                        {
                            String deskNbAsString = rawReclaimRow[reclaimDeskNbColumnIndex].ToString().Trim();
                            if (!Int32.TryParse(deskNbAsString, out reclaimDeskNb))
                            {
                                infoList.Add("Reclaim allocation file info " + ignoredFlightInfoIndex + ". "
                                    + "Line " + rowIndex + ": The flight " + flightNb + " doesn't have a reclaim desk number."
                                    + " Could not allocate a reclaim desk from the Reclaim Allocation file.");

                                rowIndex++;
                                ignoredFlightInfoIndex++;
                                continue;
                            }
                            else
                            {
                                flight.reclaimDeskStart = reclaimDeskNb;
                                flight.reclaimDeskEnd = reclaimDeskNb;

                                if (reclaimAllocationFlightsDictionary.ContainsKey(flight.dictionaryIATAKey))
                                {
                                    Flight alreadyAllocatedFlight = reclaimAllocationFlightsDictionary[flight.dictionaryIATAKey];
                                    if (alreadyAllocatedFlight.reclaimDeskStart > flight.reclaimDeskStart)
                                        alreadyAllocatedFlight.reclaimDeskStart = flight.reclaimDeskStart;
                                    if (alreadyAllocatedFlight.reclaimDeskEnd < flight.reclaimDeskEnd)
                                        alreadyAllocatedFlight.reclaimDeskEnd = flight.reclaimDeskEnd;
                                    reclaimAllocationFlightsDictionary[flight.dictionaryIATAKey] = alreadyAllocatedFlight;
                                }
                                else
                                {
                                    reclaimAllocationFlightsDictionary.Add(flight.dictionaryIATAKey, flight);
                                }
                            }
                        }                                                
                        rowIndex++;
                    }
                    
                    infoList.Add("\n" + "Updating the reclaim allocation using the information frmo the Reclaim Allocation text file." + "\n");                    

                    foreach (KeyValuePair<String, Flight> arrivalFlightPair in arrivalFlightsDictionary)
                    {                        
                        Flight arrivalFlight = arrivalFlightPair.Value;

                        if (arrivalFlight.reclaimDeskStart < 0)
                        {
                            if (reclaimAllocationFlightsDictionary.ContainsKey(arrivalFlight.dictionaryIATAKey))
                            {
                                Flight reclaimAllocationFlight = reclaimAllocationFlightsDictionary[arrivalFlight.dictionaryIATAKey];
                                arrivalFlight.reclaimDeskStart = reclaimAllocationFlight.reclaimDeskStart;
                                arrivalFlight.reclaimDeskEnd = reclaimAllocationFlight.reclaimDeskEnd;

                                infoList.Add("Reclaim allocation file - allocation update: Line " + reclaimAllocationFlight.succesfullRowId + ": The Reclaim allocation for the flight "
                                        + reclaimAllocationFlight.flightNumber + " was done using the Reclaim Allocation file.");
                            }
                            else
                            {
                                infoList.Add("Reclaim allocation file - allocation update: Could not do the Reclaim allocation for the flight "
                                    + arrivalFlight.flightNumber + " (arriving at " + String.Format("DD//MM//YYYY", arrivalFlight.flightDate) + " " + arrivalFlight.flightTime
                                    + ") using the provided Reclaim Allocation file.");
                            }
                        }
                    }
                    return arrivalFlightsDictionary;
                }
            }

            internal class CheckInAllocation
            {
                internal const String CHECK_IN_ALLOCATION_FILE_FLIGHT_NB_COLUMN_NAME = "Flight";
                internal const String CHECK_IN_ALLOCATION_FILE_STD_COLUMN_NAME = "STD";
                internal const String CHECK_IN_ALLOCATION_FILE_CHECK_IN_DESKS_RANGE = "Counter  from - to";

                public static List<String> CHECK_IN_ALLOCATION_COLUMN_NAMES_LIST 
                    = new List<String>(new string[] { CHECK_IN_ALLOCATION_FILE_FLIGHT_NB_COLUMN_NAME, 
                        CHECK_IN_ALLOCATION_FILE_STD_COLUMN_NAME, CHECK_IN_ALLOCATION_FILE_CHECK_IN_DESKS_RANGE });

                DataTable rawCheckInAllocation;
                internal Dictionary<String, Flight> departureFlightDictionary = new Dictionary<String, Flight>();

                internal ArrayList infoList = new ArrayList();

                public CheckInAllocation(DataTable _rawCheckInAllocation, Dictionary<String, Flight> _departureFlightDictionary)
                {
                    this.rawCheckInAllocation = _rawCheckInAllocation;
                    this.departureFlightDictionary = _departureFlightDictionary;
                }

                public Dictionary<String, Flight> convertAndUpdateAllocation()
                {
                    if (rawCheckInAllocation == null || rawCheckInAllocation.Rows.Count == 0)
                    {
                        infoList.Add("The imported CheckIn Allocation doesn't have any data."
                            + " Could not allocate the check-in desks from the CheckIn Allocation file.");
                        return departureFlightDictionary;
                    }
                    if (!AthensTools.areColumnHeadersFromFileValid(rawCheckInAllocation, CHECK_IN_ALLOCATION_COLUMN_NAMES_LIST))
                    {
                        infoList.Add("The imported CheckIn Allocation doesn't have valid columns."
                            + " Could not allocate check-in desks from the CheckIn Allocation file. Please check the Log.txt file for details.");
                        return departureFlightDictionary;
                    }

                    int flightNumberColumnIndex = rawCheckInAllocation.Columns.IndexOf(CHECK_IN_ALLOCATION_FILE_FLIGHT_NB_COLUMN_NAME);
                    int flightSTDColumnIndex = rawCheckInAllocation.Columns.IndexOf(CHECK_IN_ALLOCATION_FILE_STD_COLUMN_NAME);
                    int checkInDesksRangeColumnIndex = rawCheckInAllocation.Columns.IndexOf(CHECK_IN_ALLOCATION_FILE_CHECK_IN_DESKS_RANGE);

                    Dictionary<String, Flight> checkInAllocationFlightsDictionary = new Dictionary<String, Flight>();
                    int rowIndex = 2;
                    int ignoredFlightInfoIndex = 1;

                    infoList.Add("\n" + "Parsing the CheckIn Allocation text file." + "\n");

                    foreach (DataRow rawCheckInRow in rawCheckInAllocation.Rows)
                    {
                        Flight flight = new Flight();
                        String flightNb = "";
                        DateTime std = DateTime.MinValue;
                        int checkInFirstDeskNb = -1;
                        int checkInLastDeskNb = -1;

                        flight.succesfullRowId = rowIndex;

                        if (rawCheckInRow[flightNumberColumnIndex] != null)
                            flightNb = rawCheckInRow[flightNumberColumnIndex].ToString().Trim();

                        if (flightNb == null || flightNb == "")
                        {
                            infoList.Add("CheckIn allocation file info " + ignoredFlightInfoIndex + ". "
                                + "The flight from the line " + rowIndex + " doesn't have a flight number."
                                + " Could not allocate a check-in desks from the CheckIn Allocation file.");
                            rowIndex++;
                            ignoredFlightInfoIndex++;
                            continue;
                        }
                        else
                        {
                            flightNb = flightNb.Replace(" ", String.Empty);
                        }
                        flight.flightNumber = flightNb;

                        if (rawCheckInRow[flightSTDColumnIndex] != null)
                        {
                            std = FonctionsType.getDate(rawCheckInRow[flightSTDColumnIndex]);

                            if (std == DateTime.MinValue)
                            {
                                infoList.Add("CheckIn allocation file info " + ignoredFlightInfoIndex + ". "
                                    + "Line " + rowIndex + ": The flight " + flightNb
                                    + " doesn't have a valid value in the column STD from the CheckIn Allocation text file."
                                    + " Could not allocate a check-in desk from the CheckIn Allocation file.");
                                rowIndex++;
                                ignoredFlightInfoIndex++;
                                continue;
                            }
                        }
                        flight.flightDate = std.Date;
                        flight.flightTime = std.TimeOfDay;

                        if (rawCheckInRow[checkInDesksRangeColumnIndex] != null)
                        {
                            String checkInAllocationInfo = rawCheckInRow[checkInDesksRangeColumnIndex].ToString().Trim();
                            FlightPlanConversion.getCheckInAllocationFromFlightPlanValue(checkInAllocationInfo, out checkInFirstDeskNb, out checkInLastDeskNb);

                            if (checkInFirstDeskNb < 0 || checkInLastDeskNb < 0)
                            {
                                infoList.Add("CheckIn allocation file info " + ignoredFlightInfoIndex + ". "
                                    + "Line " + rowIndex + ": The flight " + flightNb + " doesn't have a valid check-in desk allocation."
                                    + " Could not allocate the check-in desks from the CheckIn Allocation file.");
                                rowIndex++;
                                ignoredFlightInfoIndex++;
                                continue;
                            }
                            else
                            {
                                flight.checkInDeskStart = checkInFirstDeskNb;
                                flight.checkInDeskEnd = checkInLastDeskNb;

                                if (checkInAllocationFlightsDictionary.ContainsKey(flight.dictionaryIATAKey))
                                {
                                    Flight alreadyAllocatedFlight = checkInAllocationFlightsDictionary[flight.dictionaryIATAKey];
                                    if (alreadyAllocatedFlight.checkInDeskStart > flight.checkInDeskStart)
                                        alreadyAllocatedFlight.checkInDeskStart = flight.checkInDeskStart;
                                    if (alreadyAllocatedFlight.checkInDeskEnd < flight.checkInDeskEnd)
                                        alreadyAllocatedFlight.checkInDeskEnd = flight.checkInDeskEnd;
                                    checkInAllocationFlightsDictionary[flight.dictionaryIATAKey] = alreadyAllocatedFlight;
                                }
                                else
                                {
                                    checkInAllocationFlightsDictionary.Add(flight.dictionaryIATAKey, flight);
                                }
                            }                            
                        }                        
                        rowIndex++;
                    }
                    infoList.Add("\n" + "Updating the reclaim allocation using the information frmo the Reclaim Allocation text file." + "\n");

                    foreach (KeyValuePair<String, Flight> departureFlightPair in departureFlightDictionary)
                    {
                        Flight departureFlight = departureFlightPair.Value;

                        if (departureFlight.checkInDeskStart < 0 || departureFlight.checkInDeskEnd < 0)
                        {
                            if (checkInAllocationFlightsDictionary.ContainsKey(departureFlight.dictionaryIATAKey))
                            {
                                Flight checkInAllocationFlight = checkInAllocationFlightsDictionary[departureFlight.dictionaryIATAKey];
                                departureFlight.checkInDeskStart = checkInAllocationFlight.checkInDeskStart;
                                departureFlight.checkInDeskEnd = checkInAllocationFlight.checkInDeskEnd;

                                infoList.Add("CheckIn allocation file - allocation update: Line " + checkInAllocationFlight.succesfullRowId + ": The Check-In allocation for the flight "
                                    + checkInAllocationFlight.flightNumber + " was done using the CheckIn Allocation file.");

                            }
                            else
                            {
                                infoList.Add("CheckIn allocation file - allocation update: Could not do the CheckIn allocation for the flight "
                                    + departureFlight.flightNumber + " departing at (" + String.Format("DD//MM//YYYY", departureFlight.flightDate) + " " + departureFlight.flightTime
                                    + ") using the provided CheckIn Allocation file.");
                            }
                        }
                    }
                    return departureFlightDictionary;
                }
            }

            internal class MakeUpAllocation
            {
                internal const String MAKE_UP_ALLOCATION_FILE_FLIGHT_NB_COLUMN_NAME = "Flight Number";
                internal const String MAKE_UP_ALLOCATION_FILE_STD_COLUMN_NAME = "STD";
                internal const String MAKE_UP_ALLOCATION_FILE_DESK_NB_COLUMN_NAME = "Chute";

                internal const String MAKE_UP_ALLOCATION_FILE_HANDLER_COLUMN_NAME = "Handler";
                internal const String MAKE_UP_ALLOCATION_FILE_SHORT_NAME_COLUMN_NAME = "Short Name";
                internal const String MAKE_UP_ALLOCATION_FILE_HALL_COLUMN_NAME = "Hall";

                public static List<String> MAKE_UP_ALLOCATION_COLUMN_NAMES_LIST
                    = new List<String>(new string[] { MAKE_UP_ALLOCATION_FILE_FLIGHT_NB_COLUMN_NAME, 
                        MAKE_UP_ALLOCATION_FILE_STD_COLUMN_NAME, MAKE_UP_ALLOCATION_FILE_DESK_NB_COLUMN_NAME,
                        MAKE_UP_ALLOCATION_FILE_HANDLER_COLUMN_NAME, MAKE_UP_ALLOCATION_FILE_SHORT_NAME_COLUMN_NAME,
                        MAKE_UP_ALLOCATION_FILE_HALL_COLUMN_NAME});

                DataTable rawMakeUpAllocationTable;
                String makeUpAllocationFilename;
                internal Dictionary<String, Flight> departureFlightDictionary = new Dictionary<String, Flight>();

                Dictionary<string, string> airlineIATAKeyICAOValueDictionary;
                Dictionary<string, string> airlineICAOKeyIATAValueDictionary;

                internal List<ImportDataInformation> infoList = new List<ImportDataInformation>();

                public MakeUpAllocation(DataTable _rawMakeUpAllocationTable,
                    Dictionary<string, string> _airlineIATAKeyICAOValueDictionary, Dictionary<string, string> _airlineICAOKeyIATAValueDictionary,
                    Dictionary<String, Flight> _departureFlightDictionary, String _fileName)
                {
                    this.rawMakeUpAllocationTable = _rawMakeUpAllocationTable;
                    this.makeUpAllocationFilename = _fileName;
                    this.departureFlightDictionary = _departureFlightDictionary;
                    this.airlineIATAKeyICAOValueDictionary = _airlineIATAKeyICAOValueDictionary;
                    this.airlineICAOKeyIATAValueDictionary = _airlineICAOKeyIATAValueDictionary;
                }

                public Dictionary<String, Flight> convertAndUpdateAllocation()
                {
                    if (rawMakeUpAllocationTable == null || rawMakeUpAllocationTable.Rows.Count == 0)
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = "MakeUp Allocation";
                        information.fileName = makeUpAllocationFilename;
                        information.lineNbFromFile = 0;
                        information.message = "The imported MakeUp Allocation doesn't have any data."
                            + " Could not allocate the make-ups from the MakeUp Allocation file.";
                        infoList.Add(information);

                        /*infoList.Add("The imported MakeUp Allocation doesn't have any data."
                            + " Could not allocate the make-ups from the MakeUp Allocation file.");*/
                        return departureFlightDictionary;
                    }
                    if (!AthensTools.areColumnHeadersFromFileValid(rawMakeUpAllocationTable, MAKE_UP_ALLOCATION_COLUMN_NAMES_LIST))
                    {
                        ImportDataInformation information = new ImportDataInformation();
                        information.tableName = "MakeUp Allocation";
                        information.fileName = makeUpAllocationFilename;
                        information.lineNbFromFile = 0;
                        information.message = "The imported MakeUp Allocation doesn't have valid columns."
                            + " Could not allocate make-ups from the MakeUp Allocation file. Please check the Log.txt file for details.";
                        infoList.Add(information);

                        /*infoList.Add("The imported MakeUp Allocation doesn't have valid columns."
                            + " Could not allocate make-ups from the MakeUp Allocation file. Please check the Log.txt file for details.");*/
                        return departureFlightDictionary;
                    }

                    int flightNumberColumnIndex = rawMakeUpAllocationTable.Columns.IndexOf(MAKE_UP_ALLOCATION_FILE_FLIGHT_NB_COLUMN_NAME);
                    int flightSTDColumnIndex = rawMakeUpAllocationTable.Columns.IndexOf(MAKE_UP_ALLOCATION_FILE_STD_COLUMN_NAME);
                    int makeUpNbColumnIndex = rawMakeUpAllocationTable.Columns.IndexOf(MAKE_UP_ALLOCATION_FILE_DESK_NB_COLUMN_NAME);

                    int handlerColumnIndex = rawMakeUpAllocationTable.Columns.IndexOf(MAKE_UP_ALLOCATION_FILE_HANDLER_COLUMN_NAME);
                    int shortNameSTDColumnIndex = rawMakeUpAllocationTable.Columns.IndexOf(MAKE_UP_ALLOCATION_FILE_SHORT_NAME_COLUMN_NAME);                    
                    int hallColumnIndex = rawMakeUpAllocationTable.Columns.IndexOf(MAKE_UP_ALLOCATION_FILE_HALL_COLUMN_NAME);

                    Dictionary<String, Flight> makeUpAllocationFlightsDictionary = new Dictionary<String, Flight>();
                    int rowIndex = 2;
                    int ignoredFlightInfoIndex = 1;

                    //infoList.Add("\n" + "Parsing the MakeUp Allocation text file." + "\n");

                    foreach (DataRow rawMakeUpRow in rawMakeUpAllocationTable.Rows)
                    {
                        // ignore the lines that are headers (they repeat from time to time in the text file)
                        if (rawMakeUpRow[flightNumberColumnIndex] != null)
                        {
                            String firstValueFromRow = rawMakeUpRow[flightNumberColumnIndex].ToString();
                            if (firstValueFromRow == "" || firstValueFromRow == MAKE_UP_ALLOCATION_FILE_FLIGHT_NB_COLUMN_NAME)
                            {
                                rowIndex++;                                
                                continue;
                            }
                        }

                        Flight flight = new Flight();
                        String flightNb = "";
                        DateTime std = DateTime.MinValue;
                        int makeUpNb = -1;                        

                        flight.succesfullRowId = rowIndex;

                        if (rawMakeUpRow[flightNumberColumnIndex] != null)
                            flightNb = rawMakeUpRow[flightNumberColumnIndex].ToString().Trim();

                        if (flightNb == null || flightNb == "")
                        {
                            ImportDataInformation information = new ImportDataInformation();
                            information.tableName = "MakeUp Allocation";
                            information.fileName = makeUpAllocationFilename;
                            information.lineNbFromFile = rowIndex;
                            information.message = "The flight doesn't have a flight number."
                                + " Could not allocate a make-ups from the MakeUp Allocation file.";
                            infoList.Add(information);

                            /*infoList.Add("MakeUp allocation file info " + ignoredFlightInfoIndex + ". "
                                + "The flight from the line " + rowIndex + " doesn't have a flight number."
                                + " Could not allocate a make-ups from the MakeUp Allocation file.");*/
                            rowIndex++;
                            ignoredFlightInfoIndex++;
                            continue;
                        }
                        else
                        {
                            flightNb = flightNb.Replace(" ", String.Empty);

                            string airlineIATACode = "";
                            string airlineICAOCode = "";
                            string flightNumberWithoutAirlineCode = "";

                            getAirlineCodeFromFlightNumber(flightNb, out airlineIATACode,
                                                           out airlineICAOCode, out flightNumberWithoutAirlineCode);
                            if (airlineIATACode == "" && airlineICAOCode == "")
                            {
                                ImportDataInformation information = new ImportDataInformation();
                                information.tableName = "MakeUp Allocation";
                                information.fileName = makeUpAllocationFilename;
                                information.lineNbFromFile = rowIndex;
                                information.message = "Could not allocate a make-ups from the MakeUp Allocation file for the flight " + flightNb + ".";                                    
                                infoList.Add(information);

                                rowIndex++;
                                ignoredFlightInfoIndex++;
                                continue;
                            }
                            else
                            {
                                if (airlineIATACode == "" && airlineICAOCode != "")
                                {
                                    ImportDataInformation information = new ImportDataInformation();
                                    information.tableName = "MakeUp Allocation";
                                    information.fileName = makeUpAllocationFilename;
                                    information.lineNbFromFile = rowIndex;
                                    information.message = "Could only find the airline ICAO code for the departure flight " + flightNb + ".";
                                    infoList.Add(information);

                                    flight.airlineICAOCode = airlineICAOCode;
                                    flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                                }
                                else if (airlineIATACode != "" && airlineICAOCode == "")
                                {
                                    ImportDataInformation information = new ImportDataInformation();
                                    information.tableName = "MakeUp Allocation";
                                    information.fileName = makeUpAllocationFilename;
                                    information.lineNbFromFile = rowIndex;
                                    information.message = "Could only find the airline IATA code for the departure flight " + flightNb + ".";                                        
                                    infoList.Add(information);

                                    flight.airlineIATACode = airlineIATACode;
                                    flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                                }
                                else
                                {
                                    flight.airlineIATACode = airlineIATACode;
                                    flight.airlineICAOCode = airlineICAOCode;
                                    flight.flightNumberWithoutAirline = flightNumberWithoutAirlineCode;
                                }
                            }

                        }
                        flight.flightNumber = flightNb;

                        if (rawMakeUpRow[flightSTDColumnIndex] != null)
                        {
                            std = FonctionsType.getDate(rawMakeUpRow[flightSTDColumnIndex]);

                            if (std == DateTime.MinValue)
                            {
                                ImportDataInformation information = new ImportDataInformation();
                                information.tableName = "MakeUp Allocation";
                                information.fileName = makeUpAllocationFilename;
                                information.lineNbFromFile = rowIndex;
                                information.message = "The flight " + flightNb
                                    + " doesn't have a valid value in the column STD from the MakeUp Allocation text file."
                                    + " Could not allocate make-up from the MakeUp Allocation file.";
                                infoList.Add(information);

                                /*infoList.Add("MakeUp allocation file info " + ignoredFlightInfoIndex + ". "
                                    + "Line " + rowIndex + ": The flight " + flightNb
                                    + " doesn't have a valid value in the column STD from the MakeUp Allocation text file."
                                    + " Could not allocate make-up from the MakeUp Allocation file.");*/
                                rowIndex++;
                                ignoredFlightInfoIndex++;
                                continue;
                            }
                        }
                        flight.flightDate = std.Date;
                        flight.flightTime = std.TimeOfDay;

                        if (rawMakeUpRow[makeUpNbColumnIndex] != null)
                        {
                            String makeUpAllocationInfo = rawMakeUpRow[makeUpNbColumnIndex].ToString().Trim();

                            if (Int32.TryParse(makeUpAllocationInfo, out makeUpNb))
                            {
                                flight.makeUpDeskStart = makeUpNb;
                                flight.makeUpDeskEnd = makeUpNb;

                                if (makeUpAllocationFlightsDictionary.ContainsKey(flight.dictionaryIATAKey))
                                {
                                    Flight partialAllocatedFlight = makeUpAllocationFlightsDictionary[flight.dictionaryIATAKey];
                                    if (partialAllocatedFlight.makeUpDeskStart > flight.makeUpDeskStart)
                                        partialAllocatedFlight.makeUpDeskStart = flight.makeUpDeskStart;
                                    if (partialAllocatedFlight.makeUpDeskEnd < flight.makeUpDeskEnd)
                                        partialAllocatedFlight.makeUpDeskEnd = flight.makeUpDeskEnd;
                                    makeUpAllocationFlightsDictionary[flight.dictionaryIATAKey] = partialAllocatedFlight;
                                }
                                else
                                {
                                    makeUpAllocationFlightsDictionary.Add(flight.dictionaryIATAKey, flight);
                                }
                            }
                            else
                            {
                                ImportDataInformation information = new ImportDataInformation();
                                information.tableName = "MakeUp Allocation";
                                information.fileName = makeUpAllocationFilename;
                                information.lineNbFromFile = rowIndex;
                                information.message = "The flight " + flightNb + " doesn't have a valid make-up allocation."
                                    + " Could not allocate the make-ups from the MakeUp Allocation file.";
                                infoList.Add(information);

                                /*infoList.Add("MakeUp allocation file info " + ignoredFlightInfoIndex + ". "
                                    + "Line " + rowIndex + ": The flight " + flightNb + " doesn't have a valid make-up allocation."
                                    + " Could not allocate the make-ups from the MakeUp Allocation file.");*/
                                rowIndex++;
                                ignoredFlightInfoIndex++;
                                continue;
                            }
                        }

                        if (rawMakeUpRow[handlerColumnIndex] != null)
                        {
                            flight.user5 = "Handler: " + rawMakeUpRow[handlerColumnIndex].ToString().Trim();
                        }
                        if (rawMakeUpRow[shortNameSTDColumnIndex] != null)
                        {
                            flight.user5 += " Short Name: " + rawMakeUpRow[shortNameSTDColumnIndex].ToString().Trim();
                        }
                        if (rawMakeUpRow[hallColumnIndex] != null)
                        {
                            flight.user5 += " Hall: " + rawMakeUpRow[hallColumnIndex].ToString().Trim();
                        }
                        rowIndex++;
                    }

                    //infoList.Add("\n" + "Updating the reclaim allocation using the information frmo the Reclaim Allocation text file." + "\n");

                    foreach (KeyValuePair<String, Flight> departureFlightPair in departureFlightDictionary)
                    {
                        Flight departureFlight = departureFlightPair.Value;

                        if (makeUpAllocationFlightsDictionary.ContainsKey(departureFlight.dictionaryIATAKey))
                        {
                            Flight makeUpAllocationFlight = makeUpAllocationFlightsDictionary[departureFlight.dictionaryIATAKey];

                            if (departureFlight.makeUpDeskStart <= 0 || departureFlight.makeUpDeskEnd <= 0)
                            {

                                departureFlight.makeUpDeskStart = makeUpAllocationFlight.makeUpDeskStart;
                                departureFlight.makeUpDeskEnd = makeUpAllocationFlight.makeUpDeskEnd;

                                ImportDataInformation information = new ImportDataInformation();
                                information.tableName = "MakeUp Allocation";
                                information.fileName = makeUpAllocationFilename;
                                information.lineNbFromFile = makeUpAllocationFlight.succesfullRowId;
                                information.message = "The MakeUp allocation for the flight "
                                    + makeUpAllocationFlight.flightNumber + " was done using the MakeUp Allocation file.";
                                infoList.Add(information);

                                /*infoList.Add("MakeUp allocation file - allocation update: Line " + makeUpAllocationFlight.id + ": The MakeUp allocation for the flight "
                                    + makeUpAllocationFlight.flightNumber + " was done using the MakeUp Allocation file.");*/
                            }
                            //departureFlight.user3 = makeUpAllocationFlight.user3;
                            //departureFlight.user4 = makeUpAllocationFlight.user4;
                            departureFlight.user5 = makeUpAllocationFlight.user5;
                        }
                        else if (makeUpAllocationFlightsDictionary.ContainsKey(departureFlight.dictionaryICAOKey))
                        {
                            Flight makeUpAllocationFlight = makeUpAllocationFlightsDictionary[departureFlight.dictionaryICAOKey];

                            if (departureFlight.makeUpDeskStart <= 0 || departureFlight.makeUpDeskEnd <= 0)
                            {

                                departureFlight.makeUpDeskStart = makeUpAllocationFlight.makeUpDeskStart;
                                departureFlight.makeUpDeskEnd = makeUpAllocationFlight.makeUpDeskEnd;

                                ImportDataInformation information = new ImportDataInformation();
                                information.tableName = "MakeUp Allocation";
                                information.fileName = makeUpAllocationFilename;
                                information.lineNbFromFile = makeUpAllocationFlight.succesfullRowId;
                                information.message = "The MakeUp allocation for the flight "
                                    + makeUpAllocationFlight.flightNumber + " was done using the MakeUp Allocation file.";
                                infoList.Add(information);

                                /*infoList.Add("MakeUp allocation file - allocation update: Line " + makeUpAllocationFlight.id + ": The MakeUp allocation for the flight "
                                    + makeUpAllocationFlight.flightNumber + " was done using the MakeUp Allocation file.");*/
                            }
                            //departureFlight.user3 = makeUpAllocationFlight.user3;
                            //departureFlight.user4 = makeUpAllocationFlight.user4;
                            departureFlight.user5 = makeUpAllocationFlight.user5;
                        }
                        else
                        {
                            if (departureFlight.makeUpDeskStart < 0 || departureFlight.makeUpDeskEnd < 0)
                            {
                                ImportDataInformation information = new ImportDataInformation();
                                information.tableName = "MakeUp Allocation";
                                information.fileName = makeUpAllocationFilename;
                                information.lineNbFromFile = 0;
                                information.message = "Could not do the MakeUp allocation for the flight "
                                    + departureFlight.flightNumber + " departing at (" + String.Format("DD//MM//YYYY", departureFlight.flightDate) + " " + departureFlight.flightTime
                                    + ") using the provided MakeUp Allocation file.";
                                infoList.Add(information);

                                /*infoList.Add("MakeUp allocation file - allocation update: Could not do the MakeUp allocation for the flight "
                                    + departureFlight.flightNumber + " departing at (" + String.Format("DD//MM//YYYY", departureFlight.flightDate) + " " + departureFlight.flightTime
                                    + ") using the provided MakeUp Allocation file.");*/
                            }
                        }                        
                    }
                    return departureFlightDictionary;
                }

                private void getAirlineCodeFromFlightNumber(String flightNumber,
                out string airlineIATACode, out string airlineICAOCode, out string flightNumberWithoutAirlineCode)
                {
                    airlineIATACode = "";
                    airlineICAOCode = "";
                    flightNumberWithoutAirlineCode = "";

                    if (flightNumber != null && flightNumber != "")
                    {
                        if (flightNumber.Length <= Math.Min(AIRLINE_IATA_CODE_LENGTH, AIRLINE_ICAO_CODE_LENGTH))
                        {
                            return;
                        }

                        flightNumber = flightNumber.Trim();
                        flightNumber = flightNumber.Replace(" ", String.Empty);

                        airlineIATACode = flightNumber.Substring(0, AIRLINE_IATA_CODE_LENGTH);
                        airlineICAOCode = flightNumber.Substring(0, AIRLINE_ICAO_CODE_LENGTH);

                        if (airlineICAOKeyIATAValueDictionary.ContainsKey(airlineICAOCode))
                        {
                            airlineIATACode = airlineICAOKeyIATAValueDictionary[airlineICAOCode];
                            flightNumberWithoutAirlineCode = flightNumber.Substring(AIRLINE_ICAO_CODE_LENGTH);
                        }
                        else if (airlineIATAKeyICAOValueDictionary.ContainsKey(airlineIATACode))
                        {
                            airlineICAOCode = airlineIATAKeyICAOValueDictionary[airlineIATACode];
                            flightNumberWithoutAirlineCode = flightNumber.Substring(AIRLINE_IATA_CODE_LENGTH);
                        }
                    }
                }
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
            List<String> currentFlightCategories = getValueFromTableByColumnName(flightCategoryTable, GlobalNames.sFPFlightCategory_FC);
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
                
        public static DataTable createAirlineCodesTableWithImportedData(DataTable airlineCodesTable, List<String> importedAirlineCodes)
        {
            if (airlineCodesTable == null || importedAirlineCodes == null
                || importedAirlineCodes.Count == 0)
            {
                return airlineCodesTable;
            }
            DataTable newAirlineCodesTable = airlineCodesTable.Copy();
            List<String> currentAirlineCodes = getValueFromTableByColumnName(airlineCodesTable, GlobalNames.sFPAirline_AirlineCode);
            int airlineCodeColumnIndex = airlineCodesTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);

            if (currentAirlineCodes.Count > 0 &&
                 airlineCodeColumnIndex != -1)
            {
                foreach (String importedAirline in importedAirlineCodes)
                {
                    if (!currentAirlineCodes.Contains(importedAirline))
                    {
                        DataRow newRow = newAirlineCodesTable.NewRow();
                        newRow[airlineCodeColumnIndex] = importedAirline;
                        newAirlineCodesTable.Rows.Add(newRow);
                    }
                }
                newAirlineCodesTable.AcceptChanges();
            }
            return newAirlineCodesTable;
        }

        const int NB_SEATS_DEFAULT_VALUE = 0;        
        public static DataTable createAircraftTypesTableWithImportedData(DataTable aircraftTypesTable, List<String> importedAircraftTypes)
        {
            if (aircraftTypesTable == null || importedAircraftTypes == null
                || importedAircraftTypes.Count == 0)
            {
                return aircraftTypesTable;
            }
            DataTable newAircraftTable = aircraftTypesTable.Copy();
            List<String> currentAircraftTypes = getValueFromTableByColumnName(aircraftTypesTable, GlobalNames.sFPAircraft_AircraftTypes);
            int aircraftTypeCodeColumnIndex = aircraftTypesTable.Columns.IndexOf(GlobalNames.sFPAircraft_AircraftTypes);
            int nbSeatsColumnIndex = aircraftTypesTable.Columns.IndexOf(GlobalNames.sFPAircraft_NumberSeats);
            int uldLooseColumnIndex = aircraftTypesTable.Columns.IndexOf(GlobalNames.sTableColumn_ULDLoose);

            if (currentAircraftTypes.Count > 0 && aircraftTypeCodeColumnIndex != -1
                && nbSeatsColumnIndex != -1 && uldLooseColumnIndex != -1)
            {
                foreach (String importedAircraftType in importedAircraftTypes)
                {
                    if (!currentAircraftTypes.Contains(importedAircraftType))
                    {
                        DataRow newRow = newAircraftTable.NewRow();
                        newRow[aircraftTypeCodeColumnIndex] = importedAircraftType;
                        newRow[nbSeatsColumnIndex] = NB_SEATS_DEFAULT_VALUE;
                        newRow[uldLooseColumnIndex] = GlobalNames.sTableContent_ULD;
                        newAircraftTable.Rows.Add(newRow);
                    }
                }
            }
            return newAircraftTable;
        }
        
        private static List<String> getValueFromTableByColumnName(DataTable givenTable, String columnName)
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

        public static List<ImportDataInformation> getInformationAboutNewAircraftTypes(DataTable aircraftTypesTable, List<String> importedAircraftTypes)
        {
            List<ImportDataInformation> informationList = new List<ImportDataInformation>();

            if (aircraftTypesTable == null || importedAircraftTypes == null
                || importedAircraftTypes.Count == 0)
            {
                return informationList;
            }            
            List<String> currentAircraftTypes 
                = getValueFromTableByColumnName(aircraftTypesTable, GlobalNames.sFPAircraft_AircraftTypes);
            
            if (currentAircraftTypes.Count > 0)
            {
                foreach (String importedAircraftType in importedAircraftTypes)
                {
                    if (!currentAircraftTypes.Contains(importedAircraftType))
                    {
                        ImportDataInformation info = new ImportDataInformation();
                        info.tableName = GlobalNames.FP_AircraftTypesTableName;                        
                        info.message = "The aircraft type " + importedAircraftType + " was not found in the current Aircraft Types table.";
                        informationList.Add(info);
                    }
                }
            }
            return informationList;
        }

        internal class ImportDataInformation
        {
            internal String tableName;
            internal String fileName;
            internal Int32 lineNbFromFile;
            internal String message;
        }

        internal const String INFORMATION_TABLE_NAME = "Import Data Information";
        private const String IMPORT_DATA_INFO_TABLE_NAME_COLUMN_NAME = "Table Name";
        private const String IMPORT_DATA_INFO_FILE_NAME_COLUMN_NAME = "File Name";
        private const String IMPORT_DATA_INFO_LINE_NB_IN_FILE_COLUMN_NAME = "File Line Number";
        private const String IMPORT_DATA_INFO_INFORMATION_COLUMN_NAME = "Information";
        public static DataTable createInformationTable(List<ImportDataInformation> informationList)
        {
            DataTable informationTable = new DataTable(INFORMATION_TABLE_NAME);
            
            int tableNameColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_TABLE_NAME_COLUMN_NAME, typeof(String));
            
            int fileNameColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_FILE_NAME_COLUMN_NAME, typeof(String));

            int lineNbColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_LINE_NB_IN_FILE_COLUMN_NAME, typeof(String));

            int informationColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_INFORMATION_COLUMN_NAME, typeof(String));

            foreach (ImportDataInformation importInfo in informationList)
            {
                DataRow row = informationTable.NewRow();

                row[tableNameColumnIndex] = importInfo.tableName;
                row[fileNameColumnIndex] = importInfo.fileName;
                
                String lineNbAsText = "";
                if (importInfo.lineNbFromFile > 0)
                {
                    lineNbAsText = importInfo.lineNbFromFile.ToString(); ;
                }
                row[lineNbColumnIndex] = lineNbAsText;

                row[informationColumnIndex] = importInfo.message;

                informationTable.Rows.Add(row);
            }
            informationTable.AcceptChanges();

            return informationTable;
        }
    }
}
