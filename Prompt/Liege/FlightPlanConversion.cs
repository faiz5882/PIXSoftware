using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class FlightPlanConversion
    {
        internal Dictionary<String, LiegeTools.FlightInfoHolder> arrivalFlightsDictionary = new Dictionary<String, LiegeTools.FlightInfoHolder>();
        internal Dictionary<String, LiegeTools.FlightInfoHolder> departureFlightsDictionary = new Dictionary<String, LiegeTools.FlightInfoHolder>();

        internal List<String> importedAirlineCodes = new List<String>();
        internal List<String> importedFlightCategories = new List<String>();
        internal Dictionary<string, AircraftType> allAircraftTypesDictionary = new Dictionary<string, AircraftType>();

        List<AircraftType> currentAircraftTypesList = new List<AircraftType>();

        DataTable rawFlightPlan;
        internal static string flightPlanFileName;
        
        DateTime startDate;
        DateTime endDate;

        internal ArrayList errorList = new ArrayList();
        internal List<LiegeTools.ImportDataInformation> infoList = new List<LiegeTools.ImportDataInformation>();

        public FlightPlanConversion(DataTable _rawFlightPlan, List<AircraftType> _currentAircraftTypesList, String _fileName,
            DateTime _startDate, DateTime _endDate)
        {
            arrivalFlightsDictionary.Clear();
            departureFlightsDictionary.Clear();

            importedAirlineCodes.Clear();
            importedFlightCategories.Clear();

            currentAircraftTypesList = _currentAircraftTypesList;

            this.rawFlightPlan = _rawFlightPlan;
            flightPlanFileName = _fileName;
            
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
            if (!LiegeTools.areColumnHeadersFromFileValid(rawFlightPlan, LiegeTools.flightPlanFileColumnNames))
            {
                errorList.Add("The imported flight plan doesn't have valid columns. Please check the Log.txt file for details.");
                return;
            }

            List<LiegeTools.ImportDataInformation> informationList = generateFlights();
            if (informationList.Count > 0)
                infoList.AddRange(informationList);
            updateAircraftTypesTable(currentAircraftTypesList, allAircraftTypesDictionary);
        }

        private List<LiegeTools.ImportDataInformation> generateFlights()
        {
            List<LiegeTools.ImportDataInformation> informationList = new List<LiegeTools.ImportDataInformation>();
                        
            int flightDateColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_FLIGHT_DATE_COLUMN_NAME);
            int flightTimeColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_FLIGHT_TIME_COLUMN_NAME);
            int flightNumberColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_FLIGHT_NUMBER_COLUMN_NAME);
            int airlineCodeColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_AIRLINE_CODE_COLUMN_NAME);
            int aircraftTypeColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_AIRCRAFT_TYPE_COLUMN_NAME);
            int airportIATACodeColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_AIRPORT_IATA_CODE_COLUMN_NAME);
                                    
            //user1..5
            int schengenOrNonSchengenColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_SCHENGEN_OR_NONSCHENGEN_COLUMN_NAME);
            int ueeOrNonUeeColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_UEE_OR_NONUEE_COLUMN_NAME);
            
            int arrivalOrDepartureColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_ARRIVAL_OR_DEPARTURE_COLUMN_NAME);
            int dayOfWeekColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_DAY_OF_WEEK_COLUMN_NAME);
            int dayNbInWeekColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_DAY_NB_IN_WEEK_COLUMN_NAME);
                                    
            int aircraftCapacityClassColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_AIRCRAFT_CAPACITY_CLASS_COLUMN_NAME);

            int airportNameColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_AIRPORT_NAME_COLUMN_NAME);
            int viaClassColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_VIA_COLUMN_NAME);

            int handlerAgentColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_HANDLER_AGENT_COLUMN_NAME);
            int obsColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_OBS_COLUMN_NAME);

            int aircraftBodyCategoryColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_BODY_CATEGORY_COLUMN_NAME);
            int linkFlightNbColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_LINK_FLIGHT_NB_COLUMN_NAME);
            int linkFlightDateColumnIndex = rawFlightPlan.Columns.IndexOf(LiegeTools.FLIGHT_PLAN_FILE_LINK_FLIGHT_DATE_COLUMN_NAME);

            int rowIndex = 2;//row number 1 from the text file contains the column names
            int ignoredFlightInfoIndex = 1;

            int arrivalFlightId = 1;
            int departureFlightId = 1;

            foreach (DataRow rawFlightPlanRow in rawFlightPlan.Rows)
            {
                LiegeTools.ImportDataInformation information = new LiegeTools.ImportDataInformation();                
                information.fileName = flightPlanFileName;

                LiegeTools.FlightInfoHolder flight = new LiegeTools.FlightInfoHolder();

                flight.textFileLineNb = rowIndex;
                string arrivalOrDeparture = "";
                if (rawFlightPlanRow[arrivalOrDepartureColumnIndex] != null)
                    arrivalOrDeparture = rawFlightPlanRow[arrivalOrDepartureColumnIndex].ToString().Trim();
                if (arrivalOrDeparture == null || arrivalOrDeparture == "")
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight "
                        + " doesn't have an Arrival or Departure information in th Flight Plan text file (column "
                        + LiegeTools.FLIGHT_PLAN_FILE_ARRIVAL_OR_DEPARTURE_COLUMN_NAME + " ). It will be ignored.";
                    infoList.Add(information);

                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }
                else
                {
                    if (arrivalOrDeparture == LiegeTools.ARRIVAL_TAG_IN_FLIGHT_PLAN_FILE
                        || arrivalOrDeparture == LiegeTools.DEPARTURE_TAG_IN_FLIGHT_PLAN_FILE)
                    {
                        flight.arrivalOrDepartureTag = arrivalOrDeparture;
                    }
                    else
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The flight "
                            + " doesn't have a valid (A or D) Arrival or Departure information in th Flight Plan text file (column "
                            + LiegeTools.FLIGHT_PLAN_FILE_ARRIVAL_OR_DEPARTURE_COLUMN_NAME + " ). It will be ignored.";
                        infoList.Add(information);

                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                }
                if (flight.arrivalOrDepartureTag == LiegeTools.ARRIVAL_TAG_IN_FLIGHT_PLAN_FILE)
                    information.tableName = GlobalNames.FPATableName;
                else if (flight.arrivalOrDepartureTag == LiegeTools.DEPARTURE_TAG_IN_FLIGHT_PLAN_FILE)
                    information.tableName = GlobalNames.FPDTableName;
                            
                if (rawFlightPlanRow[flightNumberColumnIndex] != null)
                    flight.flightNumber = rawFlightPlanRow[flightNumberColumnIndex].ToString().Trim();
                if (flight.flightNumber == null || flight.flightNumber == "")
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight "
                        + " doesn't have a valid Flight Number in the Flight Plan text file. It will be ignored.";
                    infoList.Add(information);

                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }

                DateTime flightDate = DateTime.MinValue;
                if (rawFlightPlanRow[flightDateColumnIndex] != null)
                    flightDate = FonctionsType.getDate(rawFlightPlanRow[flightDateColumnIndex]);
                if (flightDate == DateTime.MinValue)
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight " + flight.flightNumber
                        + " doesn't have a valid value in the column " + LiegeTools.FLIGHT_PLAN_FILE_FLIGHT_DATE_COLUMN_NAME + " from the Flight Plan text file. It will be ignored.";
                    infoList.Add(information);
                                       
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
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight " + flight.flightNumber
                        + " doesn't have a valid value in the column " + LiegeTools.FLIGHT_PLAN_FILE_FLIGHT_TIME_COLUMN_NAME + " from the Flight Plan text file. It will be ignored.";
                    infoList.Add(information);
                                        
                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }
                else
                {
                    flight.flightTime = flightTime;
                }
                DateTime flightCompleteDate = flight.flightDate.Add(flight.flightTime);

                if (flight.arrivalOrDepartureTag == LiegeTools.DEPARTURE_TAG_IN_FLIGHT_PLAN_FILE)
                {
                    DateTime completeFlightDate = flight.flightDate.Add(flight.flightTime);
                    if (completeFlightDate < startDate || completeFlightDate > endDate)
                    {
                        //information.lineNbFromFile = rowIndex;
                        //information.message = "The flight " + flight.flightNumber
                        //    + " has a flight date (" + completeFlightDate + ") that is not inside the interval for loading the data. It will be ignored.";
                        //infoList.Add(information);

                        rowIndex++;
                        ignoredFlightInfoIndex++;
                        continue;
                    }
                }

                if (rawFlightPlanRow[airlineCodeColumnIndex] != null)
                    flight.airlineCode = rawFlightPlanRow[airlineCodeColumnIndex].ToString().Trim();
                if (flight.airlineCode == null || flight.airlineCode == "")
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate 
                        + " doesn't have a valid Airline Code in the Flight Plan text file. It will be ignored.";
                    infoList.Add(information);

                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }
                else
                {
                    if (!importedAirlineCodes.Contains(flight.airlineCode))
                        importedAirlineCodes.Add(flight.airlineCode);
                }

                if (rawFlightPlanRow[aircraftBodyCategoryColumnIndex] != null)
                    flight.aircraftBodyCategory = rawFlightPlanRow[aircraftBodyCategoryColumnIndex].ToString().Trim();
                if (flight.aircraftBodyCategory == null || flight.aircraftBodyCategory == ""
                    || !AircraftType.AIRCRAFT_BODY_CATEGORIES_LIST.Contains(flight.aircraftBodyCategory))
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                        + " doesn't have a valid Body Category in the Flight Plan text file. It will be ignored.";
                    infoList.Add(information);

                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }

                if (rawFlightPlanRow[aircraftTypeColumnIndex] != null)
                    flight.aircraftType = rawFlightPlanRow[aircraftTypeColumnIndex].ToString().Trim();
                if (flight.aircraftType == null || flight.aircraftType == "")
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                        + " doesn't have a valid Aircraft Type in the Flight Plan text file. It will be ignored.";
                    infoList.Add(information);

                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }                

                if (rawFlightPlanRow[airportIATACodeColumnIndex] != null)
                    flight.airportCode = rawFlightPlanRow[airportIATACodeColumnIndex].ToString().Trim();
                if (flight.airportCode == null || flight.airportCode == "")
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                        + " doesn't have a valid Airport IATA Code in the Flight Plan text file. It will be ignored.";
                    infoList.Add(information);

                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }

                if (rawFlightPlanRow[schengenOrNonSchengenColumnIndex] != null)
                    flight.schengenInfo = rawFlightPlanRow[schengenOrNonSchengenColumnIndex].ToString().Trim();
                if (flight.schengenInfo == null || flight.schengenInfo == "")
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                        + " doesn't have a valid Schengen information in th Flight Plan text file (column " 
                        + LiegeTools.FLIGHT_PLAN_FILE_SCHENGEN_OR_NONSCHENGEN_COLUMN_NAME + " ). It will be ignored.";
                    infoList.Add(information);

                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }

                if (rawFlightPlanRow[ueeOrNonUeeColumnIndex] != null)
                    flight.ueeInfo = rawFlightPlanRow[ueeOrNonUeeColumnIndex].ToString().Trim();
                if (flight.ueeInfo == null || flight.ueeInfo == "")
                {
                    information.lineNbFromFile = rowIndex;
                    information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                        + " doesn't have a valid UEE information in th Flight Plan text file (column "
                        + LiegeTools.FLIGHT_PLAN_FILE_UEE_OR_NONUEE_COLUMN_NAME + " ). It will be ignored.";
                    infoList.Add(information);

                    rowIndex++;
                    ignoredFlightInfoIndex++;
                    continue;
                }

                if (flight.arrivalOrDepartureTag == LiegeTools.ARRIVAL_TAG_IN_FLIGHT_PLAN_FILE)
                {
                    flight.id = arrivalFlightId;
                    arrivalFlightId++;
                }
                else if (flight.arrivalOrDepartureTag == LiegeTools.DEPARTURE_TAG_IN_FLIGHT_PLAN_FILE)
                {
                    flight.id = departureFlightId;
                    departureFlightId++;
                }
                information.flightIdInFP = flight.id;

                flight.flightCategory 
                    = getFlightCategoryBySchengenInfoAndUEEInfo(flight.schengenInfo, flight.ueeInfo);

                if (rawFlightPlanRow[dayOfWeekColumnIndex] != null)
                {
                    flight.dayOfWeek = rawFlightPlanRow[dayOfWeekColumnIndex].ToString().Trim();
                }
                int dayNbInWeek = -1;
                if (rawFlightPlanRow[dayNbInWeekColumnIndex] != null
                    && Int32.TryParse(rawFlightPlanRow[dayNbInWeekColumnIndex].ToString().Trim(), out dayNbInWeek))
                {
                    flight.dayNbInWeek = dayNbInWeek;
                }
                if (rawFlightPlanRow[aircraftCapacityClassColumnIndex] != null)
                {
                    flight.aircraftCapacityClass = rawFlightPlanRow[aircraftCapacityClassColumnIndex].ToString().Trim();
                }
                if (rawFlightPlanRow[airportNameColumnIndex] != null)
                {
                    flight.airportName = rawFlightPlanRow[airportNameColumnIndex].ToString().Trim();
                }
                if (rawFlightPlanRow[viaClassColumnIndex] != null)
                {
                    flight.via = rawFlightPlanRow[viaClassColumnIndex].ToString().Trim();
                }
                if (rawFlightPlanRow[handlerAgentColumnIndex] != null)
                {
                    flight.handlerAgent = rawFlightPlanRow[handlerAgentColumnIndex].ToString().Trim();
                }
                if (rawFlightPlanRow[obsColumnIndex] != null)
                {
                    flight.obs = rawFlightPlanRow[obsColumnIndex].ToString().Trim();
                }
                                
                if (rawFlightPlanRow[linkFlightNbColumnIndex] != null)
                    flight.linkFlightNb = rawFlightPlanRow[linkFlightNbColumnIndex].ToString().Trim();

                string linkedFlightDateAsString = "";
                if (rawFlightPlanRow[linkFlightDateColumnIndex] != null)
                {
                    linkedFlightDateAsString = rawFlightPlanRow[linkFlightDateColumnIndex].ToString();
                    flight.linkFlightDate = FonctionsType.getDate(rawFlightPlanRow[linkFlightDateColumnIndex]);
                }

                
                if (flight.linkFlightNb == null || flight.linkFlightNb == ""
                    || flight.linkFlightDate == DateTime.MinValue)
                {
                    if (linkedFlightDateAsString != null && linkedFlightDateAsString != "")
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                            + " has an invalid linked flight date (" + linkedFlightDateAsString + "). It will use the Opening/Closing Times for Parking Stand occupation.";
                        infoList.Add(information);
                    }
                    else
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                            + " doesn't have a linked flight. It will use the Opening/Closing Times for Parking Stand occupation.";
                        infoList.Add(information);
                    }
                }

                if (flight.aircraftType != null)
                {
                    AircraftType existingAircraftType
                        = LiegeTools.getExistingAircraftTypeByNewAircraftTypeName(flight.aircraftType,
                                                                                    currentAircraftTypesList, flight.aircraftBodyCategory);
                    AircraftType newAircraftType
                        = LiegeTools.getAircraftTypeInfoByTypeName(flight.aircraftType, flight.aircraftCapacityClass, flight.aircraftBodyCategory);

                    if (existingAircraftType == null)
                    {
                        if (newAircraftType != null)
                        {
                            if (!allAircraftTypesDictionary.ContainsKey(newAircraftType.aircraftTypeName))
                                allAircraftTypesDictionary.Add(newAircraftType.aircraftTypeName, newAircraftType);
                            flight.aircraftType = newAircraftType.aircraftTypeName;
                        }
                    }
                    else
                    {
                        if (newAircraftType != null)
                        {
                            if (existingAircraftType.nbSeats != newAircraftType.nbSeats)
                            {
                                newAircraftType.aircraftTypeName += "_" + newAircraftType.nbSeats;
                                if (!allAircraftTypesDictionary.ContainsKey(newAircraftType.aircraftTypeName))
                                    allAircraftTypesDictionary.Add(newAircraftType.aircraftTypeName, newAircraftType);
                                //if (!allAircraftTypesDictionary.ContainsKey(existingAircraftType.aircraftTypeName))
                                //allAircraftTypesDictionary.Add(existingAircraftType.aircraftTypeName, existingAircraftType);
                                flight.aircraftType = newAircraftType.aircraftTypeName;
                            }
                        }
                        if (!allAircraftTypesDictionary.ContainsKey(existingAircraftType.aircraftTypeName))
                            allAircraftTypesDictionary.Add(existingAircraftType.aircraftTypeName, existingAircraftType);
                    }
                }

                if (arrivalOrDeparture == LiegeTools.ARRIVAL_TAG_IN_FLIGHT_PLAN_FILE)
                {
                    if (!arrivalFlightsDictionary.ContainsKey(flight.dictionaryKey))
                    {
                        arrivalFlightsDictionary.Add(flight.dictionaryKey, flight);
                    }
                    else
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                            + " is duplicated.It will be considered only once (the first appearance). The rest of the duplicates will be ignored.";
                        infoList.Add(information);                                                
                        ignoredFlightInfoIndex++;

                        //OverallTools.ExternFunctions.PrintLogFile("The flight " +flight.dictionaryKey + " is duplicated.");
                    }
                }
                else if (arrivalOrDeparture == LiegeTools.DEPARTURE_TAG_IN_FLIGHT_PLAN_FILE)
                {
                    if (!departureFlightsDictionary.ContainsKey(flight.dictionaryKey))
                    {
                        departureFlightsDictionary.Add(flight.dictionaryKey, flight);
                    }
                    else
                    {
                        information.lineNbFromFile = rowIndex;
                        information.message = "The flight " + flight.flightNumber + " from " + flightCompleteDate
                            + " is duplicated.It will be considered only once (the first appearance). The rest of the duplicates will be ignored.";
                        infoList.Add(information);
                        ignoredFlightInfoIndex++;

                        //OverallTools.ExternFunctions.PrintLogFile("The flight " + flight.dictionaryKey + " is duplicated.");
                    }
                }

                rowIndex++;
            }
            return informationList;
        }

        private string getFlightCategoryBySchengenInfoAndUEEInfo(string schengenInfo, string ueeInfo)
        {
            string flightCategory = "";
            if (schengenInfo != null && ueeInfo != null)
            {
                flightCategory = schengenInfo + "_" + ueeInfo;
            }
            return flightCategory;
        }

        private void updateAircraftTypesTable(List<AircraftType> initialAircraftTypes, Dictionary<string, AircraftType> allImportAircraftTypes)
        {
            if (initialAircraftTypes != null && allImportAircraftTypes != null)
            {
                foreach (AircraftType initialAircraftType in initialAircraftTypes)
                {
                    if (!allImportAircraftTypes.ContainsKey(initialAircraftType.aircraftTypeName))
                        allImportAircraftTypes.Add(initialAircraftType.aircraftTypeName, initialAircraftType);
                }
            }
        }

    }
}
