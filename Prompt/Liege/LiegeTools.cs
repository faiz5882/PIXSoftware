using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIMCORE_TOOL.Prompt.Dubai.P2S_Allocation;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class LiegeTools
    {
        internal const string LIEGE_ALLOCATION_TABLE_NAME = "Liege Allocation";

        internal const string ARRIVAL_TAG_IN_FLIGHT_PLAN_FILE = "A";
        internal const string DEPARTURE_TAG_IN_FLIGHT_PLAN_FILE = "D";
        //Flight Plan
        internal static String FLIGHT_PLAN_FILE_DAY_OF_WEEK_COLUMN_NAME = "JR";
        internal static String FLIGHT_PLAN_FILE_FLIGHT_DATE_COLUMN_NAME = "DATE";
        internal static String FLIGHT_PLAN_FILE_DAY_NB_IN_WEEK_COLUMN_NAME = "JR NB";
        internal static String FLIGHT_PLAN_FILE_ARRIVAL_OR_DEPARTURE_COLUMN_NAME = "A/D";
        internal static String FLIGHT_PLAN_FILE_FLIGHT_NUMBER_COLUMN_NAME = "FLT NR";
        internal static String FLIGHT_PLAN_FILE_AIRLINE_CODE_COLUMN_NAME = "COMPAGNIE";
        internal static String FLIGHT_PLAN_FILE_AIRCRAFT_TYPE_COLUMN_NAME = "ACFT";
        internal static String FLIGHT_PLAN_FILE_AIRCRAFT_CAPACITY_CLASS_COLUMN_NAME = "CF";
        internal static String FLIGHT_PLAN_FILE_AIRPORT_NAME_COLUMN_NAME = "AIRPORT";
        internal static String FLIGHT_PLAN_FILE_AIRPORT_IATA_CODE_COLUMN_NAME = "IATA";
        internal static String FLIGHT_PLAN_FILE_VIA_COLUMN_NAME = "VIA";
        internal static String FLIGHT_PLAN_FILE_OBS_COLUMN_NAME = "OBS";
        internal static String FLIGHT_PLAN_FILE_FLIGHT_TIME_COLUMN_NAME = "LT";
        internal static String FLIGHT_PLAN_FILE_SCHENGEN_OR_NONSCHENGEN_COLUMN_NAME = "SCH / NSCH";
        internal static String FLIGHT_PLAN_FILE_UEE_OR_NONUEE_COLUMN_NAME = "UEE / NUEE";
        internal static String FLIGHT_PLAN_FILE_HANDLER_AGENT_COLUMN_NAME = "H";
        internal static String FLIGHT_PLAN_FILE_BODY_CATEGORY_COLUMN_NAME = "BODY CAT";
        internal static String FLIGHT_PLAN_FILE_LINK_FLIGHT_NB_COLUMN_NAME = "LNK FLT NR";
        internal static String FLIGHT_PLAN_FILE_LINK_FLIGHT_DATE_COLUMN_NAME = "LNK DATE";        

        internal static List<String> flightPlanFileColumnNames
            = new List<String>(new string[] 
            { 
                FLIGHT_PLAN_FILE_DAY_OF_WEEK_COLUMN_NAME, FLIGHT_PLAN_FILE_FLIGHT_DATE_COLUMN_NAME,
                FLIGHT_PLAN_FILE_FLIGHT_DATE_COLUMN_NAME, FLIGHT_PLAN_FILE_ARRIVAL_OR_DEPARTURE_COLUMN_NAME,
                FLIGHT_PLAN_FILE_FLIGHT_NUMBER_COLUMN_NAME, FLIGHT_PLAN_FILE_AIRLINE_CODE_COLUMN_NAME,
                FLIGHT_PLAN_FILE_AIRCRAFT_TYPE_COLUMN_NAME, FLIGHT_PLAN_FILE_AIRCRAFT_CAPACITY_CLASS_COLUMN_NAME,
                FLIGHT_PLAN_FILE_AIRPORT_NAME_COLUMN_NAME, FLIGHT_PLAN_FILE_AIRPORT_IATA_CODE_COLUMN_NAME,
                FLIGHT_PLAN_FILE_VIA_COLUMN_NAME, FLIGHT_PLAN_FILE_OBS_COLUMN_NAME,
                FLIGHT_PLAN_FILE_FLIGHT_TIME_COLUMN_NAME,FLIGHT_PLAN_FILE_SCHENGEN_OR_NONSCHENGEN_COLUMN_NAME,
                FLIGHT_PLAN_FILE_UEE_OR_NONUEE_COLUMN_NAME, FLIGHT_PLAN_FILE_HANDLER_AGENT_COLUMN_NAME,
                FLIGHT_PLAN_FILE_BODY_CATEGORY_COLUMN_NAME, FLIGHT_PLAN_FILE_LINK_FLIGHT_NB_COLUMN_NAME,
                FLIGHT_PLAN_FILE_LINK_FLIGHT_DATE_COLUMN_NAME
            });

        internal static string OCT_CITABLE_EXCEPTION_AIRLINE_AIRPORT_TEXT_FILE_NAME = "OCT_CITable_Exception_AirlineAirport.txt";

        private const string AVIAPARTNER_HANDLER_TAG = "A";
        private const string AVIAPARTNER_HANDLER_NAME = "Aviapartner";
        private const string SWISSPORT_HANDLER_TAG = "S";
        private const string SWISSPORT_HANDLER_NAME = "Swissport";

        public static String getHandlerNameByHandlerTag(string handlerTag)
        {
            if (handlerTag == AVIAPARTNER_HANDLER_TAG)
                return AVIAPARTNER_HANDLER_NAME;
            else if (handlerTag == SWISSPORT_HANDLER_TAG)
                return SWISSPORT_HANDLER_NAME;
            return "";
        }
        
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

        internal class FlightInfoHolder
        {
            private const String KEY_DELIMITER = "[-]";

            public int textFileLineNb { get; set; }
            public int id { get; set; }
            public DateTime flightDate { get; set; }
            public TimeSpan flightTime { get; set; }
            public String airlineCode { get; set; }            
            public String flightNumber { get; set; }            
            public String airportCode { get; set; }
            public String flightCategory { get; set; }
            public String aircraftType { get; set; }
            public String dayOfWeek { get; set; }
            public int dayNbInWeek { get; set; }
            public String arrivalOrDepartureTag { get; set; }
            public String aircraftCapacityClass { get; set; }
            public String airportName { get; set; }
            public String via { get; set; }
            public String obs { get; set; }
            public String schengenInfo { get; set; }
            public String ueeInfo { get; set; }
            public String handlerAgent { get; set; }
            public String user1 { get; set; }
            public String user2 { get; set; }
            public String user3 { get; set; }
            public String user4 { get; set; }
            public String user5 { get; set; }
            public String aircraftBodyCategory { get; set; }
            public String linkFlightNb { get; set; }
            public DateTime linkFlightDate { get; set; }

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

        internal class ImportDataInformation
        {            
            internal String fileName;
            internal Int32 lineNbFromFile;
            internal String tableName;
            internal int flightIdInFP;
            internal String message;
        }

        internal const String INFORMATION_TABLE_NAME = "Import Data Information";        
        private const String IMPORT_DATA_INFO_FILE_NAME_COLUMN_NAME = "File Name";
        private const String IMPORT_DATA_INFO_LINE_NB_IN_FILE_COLUMN_NAME = "File Line Number";
        private const String IMPORT_DATA_INFO_TABLE_NAME_COLUMN_NAME = "Table Name";
        private const String IMPORT_DATA_INFO_FLIGHT_ID_IN_FP_COLUMN_NAME = "Flight Id";
        private const String IMPORT_DATA_INFO_INFORMATION_COLUMN_NAME = "Information";
        public static DataTable createInformationTable(List<ImportDataInformation> informationList)
        {
            DataTable informationTable = new DataTable(INFORMATION_TABLE_NAME);
                        
            int fileNameColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_FILE_NAME_COLUMN_NAME, typeof(String));

            int lineNbColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_LINE_NB_IN_FILE_COLUMN_NAME, typeof(int));

            int tableNameColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_TABLE_NAME_COLUMN_NAME, typeof(String));

            int flightIdInFPColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_FLIGHT_ID_IN_FP_COLUMN_NAME, typeof(int));

            int informationColumnIndex = informationTable.Columns.Count;
            informationTable.Columns.Add(IMPORT_DATA_INFO_INFORMATION_COLUMN_NAME, typeof(String));

            foreach (ImportDataInformation importInfo in informationList)
            {
                DataRow row = informationTable.NewRow();
                row[fileNameColumnIndex] = importInfo.fileName;
                row[lineNbColumnIndex] = importInfo.lineNbFromFile;
                row[tableNameColumnIndex] = importInfo.tableName;
                row[flightIdInFPColumnIndex] = importInfo.flightIdInFP;                
                row[informationColumnIndex] = importInfo.message;
                informationTable.Rows.Add(row);
            }
            informationTable.AcceptChanges();
            return informationTable;
        }


        internal static List<AircraftType> getAircraftTypesByAircraftTypeTable(DataTable currentAircraftTypeTable)
        {
            List<AircraftType> currentAircraftTypesList = new List<AircraftType>();
            if (currentAircraftTypeTable != null)
            {
                int aircraftTypeColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_AircraftTypes);
                int bodyColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Body);
                int nbSeatsColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_NumberSeats);
                int bagStoringModeColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sTableColumn_ULDLoose);

                int descriptionColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Description);
                int wakeColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Wake);

                if (aircraftTypeColumnIndex != -1 && bodyColumnIndex != -1
                    && nbSeatsColumnIndex != -1 && bagStoringModeColumnIndex != -1
                    && descriptionColumnIndex != -1 && wakeColumnIndex != -1)
                {
                    foreach (DataRow row in currentAircraftTypeTable.Rows)
                    {
                        AircraftType aircraftType = new AircraftType();
                        if (row[aircraftTypeColumnIndex] != null)
                            aircraftType.aircraftTypeName = row[aircraftTypeColumnIndex].ToString();
                        if (row[bodyColumnIndex] != null)
                            aircraftType.bodyCategory = row[bodyColumnIndex].ToString();
                        if (row[nbSeatsColumnIndex] != null)
                        {
                            int nbSeats = -1;
                            if (Int32.TryParse(row[nbSeatsColumnIndex].ToString(), out nbSeats))
                            {
                                aircraftType.nbSeats = nbSeats;
                            }
                        }
                        if (row[bagStoringModeColumnIndex] != null)
                            aircraftType.bagStoringMode = row[bagStoringModeColumnIndex].ToString();
                        if (row[descriptionColumnIndex] != null)
                            aircraftType.description = row[descriptionColumnIndex].ToString();
                        if (row[wakeColumnIndex] != null)
                            aircraftType.wake = row[wakeColumnIndex].ToString();

                        currentAircraftTypesList.Add(aircraftType);
                    }
                }
            }
            return currentAircraftTypesList;
        }

        internal static AircraftType getExistingAircraftTypeByNewAircraftTypeName(string aircraftTypeName,
            List<AircraftType> currentAircraftTypesList, string bodyCategory)
        {            
            if (currentAircraftTypesList != null)
            {
                foreach (AircraftType aircraftType in currentAircraftTypesList)
                {
                    if (aircraftType.aircraftTypeName == aircraftTypeName)
                    {
                        aircraftType.bodyCategory = bodyCategory;
                        return aircraftType;
                    }
                }
            }
            return null;
        }

        internal static AircraftType getAircraftTypeInfoByTypeName(string aircraftTypeName, string aircraftCapacityClass, string bodyCategory)
        {
            AircraftType aircraftType = null;
            if (aircraftCapacityClass == null)
                return aircraftType;

            char[] aircraftCapacityClassNameChars = aircraftCapacityClass.ToCharArray();
            int firstDigitIndex = -1;
            for (int i = 0; i < aircraftCapacityClassNameChars.Length; i++)
            {
                if (Char.IsDigit(aircraftCapacityClassNameChars[i]))
                {
                    firstDigitIndex = i;
                    break;
                }
            } if (firstDigitIndex != -1)
            {
                string aircraftTypeNbSeats = aircraftCapacityClass.Substring(firstDigitIndex);
                int nbSeats = -1;
                if (Int32.TryParse(aircraftTypeNbSeats, out nbSeats))
                {
                    aircraftType = new AircraftType();
                    aircraftType.aircraftTypeName = aircraftTypeName;
                    aircraftType.nbSeats = nbSeats;
                    aircraftType.bagStoringMode = AircraftType.BAG_STORING_MODE_ULD;
                    aircraftType.bodyCategory = bodyCategory;
                }
            }
            return aircraftType;
        }

        internal static DataTable updateAircraftTypesTable(DataTable currentAircraftTypeTable, Dictionary<string, AircraftType> dictionary)
        {
            if (currentAircraftTypeTable != null)
            {
                int aircraftTypeColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_AircraftTypes);
                int bodyColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Body);
                int nbSeatsColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_NumberSeats);
                int bagStoringModeColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sTableColumn_ULDLoose);

                int descriptionColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Description);
                int wakeColumnIndex = currentAircraftTypeTable.Columns.IndexOf(GlobalNames.sFPAircraft_Wake);

                if (aircraftTypeColumnIndex != -1 && bodyColumnIndex != -1
                    && nbSeatsColumnIndex != -1 && bagStoringModeColumnIndex != -1
                    && descriptionColumnIndex != -1 && wakeColumnIndex != -1)
                {
                    currentAircraftTypeTable.Rows.Clear(); 
                    foreach (KeyValuePair<string, AircraftType> pair in dictionary)
                    {
                        AircraftType aircraftType = pair.Value;
                        DataRow row = currentAircraftTypeTable.NewRow();
                        row[aircraftTypeColumnIndex] = aircraftType.aircraftTypeName;
                        row[bodyColumnIndex] = aircraftType.bodyCategory;
                        row[nbSeatsColumnIndex] = aircraftType.nbSeats;     
                        row[bagStoringModeColumnIndex] = aircraftType.bagStoringMode;              
                        row[descriptionColumnIndex] = aircraftType.description;
                        row[wakeColumnIndex] = aircraftType.wake;
                        currentAircraftTypeTable.Rows.Add(row);
                    }
                    currentAircraftTypeTable.AcceptChanges();
                }                
            }
            return currentAircraftTypeTable;
        }

        internal const string AIRLINE_AIRPORT_EXCEPTION_AIRLINE_ROW_NAME = "Airline";
        internal const string AIRLINE_AIRPORT_EXCEPTION_AIRPORT_ROW_NAME = "Airport";
        internal const string AIRLINE_AIRPORT_EXCEPTION_OPENING_ROW_NAME = "CI Opening Time (Min before STD)";
        internal const string AIRLINE_AIRPORT_EXCEPTION_CLOSING_ROW_NAME = "CI Closing Time (Min before STD)";
        internal const string AIRLINE_AIRPORT_EXCEPTION_PARTIAL_OPENING_ROW_NAME = "CI Partial Opening Time (Min before STD)";
        internal const string AIRLINE_AIRPORT_EXCEPTION_ALLOCATED_ROW_NAME = "Number of allocated CI";        
        internal const string AIRLINE_AIRPORT_EXCEPTION_ALLOCATED_PARTIAL_OPENING_ROW_NAME = "Number of Opened CI at Partial Opening";
        internal const string AIRLINE_AIRPORT_EXCEPTION_ADDITIONAL_ROW_NAME = "Number of Additional CI for overlapping flights";

        internal static List<string> AIRLINE_AIRPORT_EXCEPTION_AIRLINE_ROWS 
            = new List<string>(new string[] { AIRLINE_AIRPORT_EXCEPTION_AIRLINE_ROW_NAME, AIRLINE_AIRPORT_EXCEPTION_AIRPORT_ROW_NAME,
                                              AIRLINE_AIRPORT_EXCEPTION_OPENING_ROW_NAME, AIRLINE_AIRPORT_EXCEPTION_CLOSING_ROW_NAME,
                                              AIRLINE_AIRPORT_EXCEPTION_PARTIAL_OPENING_ROW_NAME, AIRLINE_AIRPORT_EXCEPTION_ALLOCATED_ROW_NAME,
                                              AIRLINE_AIRPORT_EXCEPTION_ALLOCATED_PARTIAL_OPENING_ROW_NAME, AIRLINE_AIRPORT_EXCEPTION_ADDITIONAL_ROW_NAME});

        internal static Dictionary<string, CheckInOCTAirlineAirportException> getCheckInOCTAirlineAirportExceptionDictionary(DataTable ciOCTAirlineAirportExcTable)
        {
            Dictionary<string, CheckInOCTAirlineAirportException> octAirlineAirportExceptionDictionary
                = new Dictionary<string, CheckInOCTAirlineAirportException>();

            string airlineCode = "";
            string airportCode = "";
            int openingTime = 0;
            int closingTime = 0;
            int partialOpeningTime = 0;
            int nbAllocated = 0;
            int nbAtPartialOpening = 0;
            int nbForOverlapping = 0;
            if (isValidAirlineAirportExceptionTable(ciOCTAirlineAirportExcTable))
            {
                for (int i = 1; i < ciOCTAirlineAirportExcTable.Columns.Count; i++)
                {
                    foreach (DataRow row in ciOCTAirlineAirportExcTable.Rows)
                    {
                        if (row[0] == null || row[i] == null)
                            continue;
                        string attributeName = row[0].ToString().Trim();
                        string attributeValueAsString = row[i].ToString().Trim();
                        switch (attributeName)
                        {
                            case AIRLINE_AIRPORT_EXCEPTION_AIRLINE_ROW_NAME:
                                airlineCode = attributeValueAsString;
                                break;
                            case AIRLINE_AIRPORT_EXCEPTION_AIRPORT_ROW_NAME:
                                airportCode = attributeValueAsString;
                                break;
                            case AIRLINE_AIRPORT_EXCEPTION_OPENING_ROW_NAME:
                                Int32.TryParse(attributeValueAsString, out openingTime);
                                break;
                            case AIRLINE_AIRPORT_EXCEPTION_CLOSING_ROW_NAME:
                                Int32.TryParse(attributeValueAsString, out closingTime);
                                break;
                            case AIRLINE_AIRPORT_EXCEPTION_PARTIAL_OPENING_ROW_NAME:
                                Int32.TryParse(attributeValueAsString, out partialOpeningTime);
                                break;
                            case AIRLINE_AIRPORT_EXCEPTION_ALLOCATED_ROW_NAME:
                                Int32.TryParse(attributeValueAsString, out nbAllocated);
                                break;
                            case AIRLINE_AIRPORT_EXCEPTION_ALLOCATED_PARTIAL_OPENING_ROW_NAME:
                                Int32.TryParse(attributeValueAsString, out nbAtPartialOpening);
                                break;
                            case AIRLINE_AIRPORT_EXCEPTION_ADDITIONAL_ROW_NAME:
                                Int32.TryParse(attributeValueAsString, out nbForOverlapping);
                                break;
                            default: 
                                break;
                        }
                    }
                    CheckInOCTAirlineAirportException ciOCTException 
                        = new CheckInOCTAirlineAirportException(airlineCode, airportCode, openingTime, closingTime, 
                            partialOpeningTime, nbAllocated, nbAtPartialOpening, nbForOverlapping);
                    if (!octAirlineAirportExceptionDictionary.ContainsKey(ciOCTException.getUniqueIdentifier()))
                        octAirlineAirportExceptionDictionary.Add(ciOCTException.getUniqueIdentifier(), ciOCTException);
                }
            }
            return octAirlineAirportExceptionDictionary;
        }
        internal static bool isValidAirlineAirportExceptionTable(DataTable ciOCTAirlineAirportExcTable)
        {
            if (ciOCTAirlineAirportExcTable != null && ciOCTAirlineAirportExcTable.Columns != null
                && ciOCTAirlineAirportExcTable.Columns.Count > 0 && ciOCTAirlineAirportExcTable.Rows != null
                && ciOCTAirlineAirportExcTable.Rows.Count > 0)
            {
                foreach (DataRow row in ciOCTAirlineAirportExcTable.Rows)
                {
                    if (row[0] == null 
                        || !AIRLINE_AIRPORT_EXCEPTION_AIRLINE_ROWS.Contains(row[0].ToString()))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        internal static List<Flight> getOverlappingFlights(Flight overlappedFlight, List<Flight> allFlights)
        {
            List<Flight> overlappingFlights = new List<Flight>();
            if (overlappedFlight == null)
                return overlappingFlights;

            foreach (Flight flight in allFlights)
            {
                if (overlappedFlight.id == flight.id)
                    continue;
                if (flight.entireAllocationOccupationNeed != null
                    && overlappedFlight.entireAllocationOccupationNeed != null)
                {
                    if (isFlightAllowedToOverlap(flight, overlappedFlight))                    
                        overlappingFlights.Add(flight);
                }
            }
            return overlappingFlights;
        }

        private static bool isFlightAllowedToOverlap(Flight flight, Flight overlappedFlight)
        {
            if (!flight.allocatedWithAlgorithm
                && overlappedFlight.airlineCode == flight.airlineCode
                && overlappedFlight.entireAllocationOccupationNeed.occupationInterval
                    .intersectsTimeInterval(flight.entireAllocationOccupationNeed.occupationInterval))
            {
                return true;
            }
            return false;
        }

        internal static List<Flight> getFlightsByAirlinesList(List<string> airlines, List<Flight> allFlights)
        {
            List<Flight> searchedFlights = new List<Flight>();
            foreach (Flight flight in allFlights)
            {
                if (!airlines.Contains(flight.airlineCode))
                    searchedFlights.Add(flight);
            }
            return searchedFlights;
        }

        internal static void getFirstAndLastOccupiedStationNb(List<Resource> allocatedResources,
            out int firstResourceNb, out int lastResourceNb)
        {
            firstResourceNb = 0;
            lastResourceNb = 0;
            if (allocatedResources != null
                && allocatedResources.Count > 0)
            {
                firstResourceNb = Int32.MaxValue;
                lastResourceNb = Int32.MinValue;
                foreach (Resource resource in allocatedResources)
                {
                    if (resource.codeId < firstResourceNb)
                        firstResourceNb = resource.codeId;
                    if (resource.codeId > lastResourceNb)
                        lastResourceNb = resource.codeId;
                }
            }
        }

        internal static TimeInterval getFirstOccupationIntervalByResourcesAndFlight(Flight flight,
            List<Resource> extraAllocatedResources)
        {            
            foreach (Resource resource in extraAllocatedResources)
            {
                foreach (FlightAllocation allocation in resource.flightAllocations)
                {
                    if (allocation.allocatedFlight.id == flight.id)
                        return allocation.occupationInterval;
                }
            }
            return null;
        }

        internal class AllocationParametersTools
        {
            internal static AllocationParameters getParametersFromParametersTable(DataTable parametersTable)
            {
                AllocationParameters parameters = new AllocationParameters();
                if (parametersTable == null || !parametersTable.Columns.Contains(AllocationOutput.ALLOCATION_PARAMETERS_TABLE_COLUM_NAME)
                    || !parametersTable.Columns.Contains(AllocationOutput.ALLOCATION_PARAMETERS_TABLE_COLUM_VALUE))
                {
                    return parameters;
                }
                int columnIndexName = parametersTable.Columns.IndexOf(AllocationOutput.ALLOCATION_PARAMETERS_TABLE_COLUM_NAME);
                int columnIndexValue = parametersTable.Columns.IndexOf(AllocationOutput.ALLOCATION_PARAMETERS_TABLE_COLUM_VALUE);
                
                string paramName = "";
                string paramValue = "";
                foreach (DataRow row in parametersTable.Rows)
                {
                    if (row[columnIndexName] != null)
                    {
                        paramName = row[columnIndexName].ToString();                        
                    }
                    if (row[columnIndexValue] != null)
                    {
                        paramValue = row[columnIndexValue].ToString();
                    }
                    updateParameters(paramName, paramValue, parameters);
                }
                return parameters;
            }

            private static void updateParameters(string paramName, string paramValue, AllocationParameters parameters)
            {
                if (parameters == null)
                {
                    return;
                }
                string dateFormater = "";
                if (PAX2SIM.liegeMode)
                {
                    dateFormater = AllocationOutput.ALLOCATION_DATE_FORMAT_LIEGE;
                }
                else if (PAX2SIM.dubaiMode)
                {
                    dateFormater = DubaiOutput.ALLOCATION_DATE_FORMAT_DUBAI;
                }
                switch (paramName)
                {
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_SCENARIO_NAME:
                        {
                            parameters.name = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_ALLOCATION_TYPE:
                        {
                            parameters.type = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_START_DATE:
                        {
                            parameters.start = DateTime.ParseExact(paramValue, dateFormater, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_END_DATE:
                        {
                            parameters.end = DateTime.ParseExact(paramValue, dateFormater, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_FLIGHT_PROCESSING_ORDER:
                        {
                            if (paramValue.Contains(AllocationOutput.ALLOCATION_PROCESSING_ORDER_SEPARATOR))
                            {
                                string[] sortingOrders = paramValue.Split(new string[] { AllocationOutput.ALLOCATION_PROCESSING_ORDER_SEPARATOR }, StringSplitOptions.None);
                                if (sortingOrders.Length == 2)
                                {
                                    parameters.mainSortingType = sortingOrders[0];
                                    parameters.secondarySortingType = sortingOrders[1];
                                }
                            }
                            else
                            {
                                parameters.mainSortingType = paramValue;
                            }                            
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_TIME_STEP:
                        {
                            double timeStep = -1;
                            if (double.TryParse(paramValue, out timeStep))
                            {
                                parameters.timeStep = timeStep;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_ANALYSIS_RANGE:
                        {
                            double analysisRange = -1;
                            if (double.TryParse(paramValue, out analysisRange))
                            {
                                parameters.analysisRange = analysisRange;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_DELAY_BETWEEN_CONS_FLIGHTS:
                        {
                            int delay = -1;
                            if (int.TryParse(paramValue, out delay))
                            {
                                parameters.delayBetweenConsecutiveFlights = delay;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_USE_FP_AS_BASIS:
                        {
                            bool useFp;
                            if (bool.TryParse(paramValue, out useFp))
                            {
                                parameters.useFpAsBasis = useFp;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_UPDATE_FP_WITH_RESULTS:
                        {
                            bool updateFp;
                            if (bool.TryParse(paramValue, out updateFp))
                            {
                                parameters.updateFpWithResults = updateFp;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_BOARDING_ENTERING_SPEED:
                        {
                            double enteringSpeed = -1;
                            if (double.TryParse(paramValue, out enteringSpeed))
                            {
                                parameters.boardingEnteringSpeed = enteringSpeed;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_BOARDING_EXITING_SPEED:
                        {
                            double exitingSpeed = -1;
                            if (double.TryParse(paramValue, out exitingSpeed))
                            {
                                parameters.boardingExitingSpeed = exitingSpeed;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_LOWER_LIMIT_LARGE_AIRCRAFT:
                        {
                            double limit = -1;
                            if (double.TryParse(paramValue, out limit))
                            {
                                parameters.lowerLimitForLargeAircraft = limit;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_CALCULATE_OCCUPATION:
                        {
                            bool calcOcc;
                            if (bool.TryParse(paramValue, out calcOcc))
                            {
                                parameters.calculateOccupation = calcOcc;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_FPD_TABLE:
                        {
                            parameters.fpdTableName = paramValue;                            
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_PRK_OCT:
                        {
                            parameters.prkOctTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_PRK_OCT_USE_EXC:
                        {
                            bool useExc;
                            if (bool.TryParse(paramValue, out useExc))
                            {
                                parameters.prkOctUseExceptions = useExc;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_BG_OCT:
                        {
                            parameters.bgOctTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_BG_OCT_USE_EXC:
                        {
                            bool useExc;
                            if (bool.TryParse(paramValue, out useExc))
                            {
                                parameters.bgOctUseExceptions = useExc;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_CI_OCT:
                        {
                            parameters.ciOctTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_CI_OCT_USE_EXC:
                        {
                            bool useExc;
                            if (bool.TryParse(paramValue, out useExc))
                            {
                                parameters.ciOctUseExceptions = useExc;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_CI_OCT_AIRLINE_AIRPORT:
                        {
                            parameters.ciOctAirlineAirportExceptionTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_CI_SHOWUP:
                        {
                            parameters.ciShowUpTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_CI_SHOWUP_USE_EXC:
                        {
                            bool useExc;
                            if (bool.TryParse(paramValue, out useExc))
                            {
                                parameters.ciShowUpUseExceptions = useExc;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_CI_SHOWUP_IGNORE:
                        {
                            bool ignore;
                            if (bool.TryParse(paramValue, out ignore))
                            {
                                parameters.ciShowUpIgnore = ignore;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_AIRCRAFT_TYPES:
                        {
                            parameters.aircraftTypesTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_AIRCRAFT_TYPES_USE_EXC:
                        {
                            bool useExc;
                            if (bool.TryParse(paramValue, out useExc))
                            {
                                parameters.aircraftTypesUseExceptions = useExc;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_LF:
                        {
                            parameters.loadingFactorsTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_LF_USE_EXC:
                        {
                            bool useExc;
                            if (bool.TryParse(paramValue, out useExc))
                            {
                                parameters.loadingFactorsUseExceptions = useExc;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_LF_DISABLE:
                        {
                            bool disable;
                            if (bool.TryParse(paramValue, out disable))
                            {
                                parameters.loadingFactorsDisable = disable;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_AIRCRAFT_LINKS:
                        {
                            parameters.aircraftLinksTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_AIRCRAFT_LINKS_DISABLE:
                        {
                            bool disable;
                            if (bool.TryParse(paramValue, out disable))
                            {
                                parameters.aircraftLinksDisable = disable;
                            }
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_PRK_PRIORITIES:
                        {
                            parameters.parkingPrioritiesTableName = paramValue;
                            break;
                        }
                    case AllocationOutput.ALLOCATION_PARAMETER_NAME_BG_PRIORITIES:
                        {
                            parameters.boardingGatesPrioritiesTableName = paramValue;
                            break;
                        }
                    case DubaiOutput.ALLOCATION_PARAMETER_NAME_TERMINAL_NB:
                        {
                            parameters.terminal = paramValue;                            
                            break;
                        }
                    case DubaiOutput.ALLOCATION_PARAMETER_NAME_MAX_ACCEPTED_DOWN_TIME:
                        {
                            double value = -1;
                            if (double.TryParse(paramValue, out value))
                            {
                                parameters.maximumAcceptedDownTime = value;
                            }
                            break;
                        }
                    case DubaiOutput.ALLOCATION_PARAMETER_NAME_DOWN_TIME_AFTER_STA:
                        {
                            double value = -1;
                            if (double.TryParse(paramValue, out value))
                            {
                                parameters.downTimeAfterSta = value;
                            }                            
                            break;
                        }
                    case DubaiOutput.ALLOCATION_PARAMETER_NAME_DOWN_TIME_BEFORE_STD:
                        {
                            double value = -1;
                            if (double.TryParse(paramValue, out value))
                            {
                                parameters.downTimeBeforeStd = value;
                            }                            
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

        }
    
    }
}
