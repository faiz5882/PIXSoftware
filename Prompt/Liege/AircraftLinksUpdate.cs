using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class AircraftLinksUpdate
    {
        private DataTable currentAircraftLinks;        
        private Dictionary<String, LiegeTools.FlightInfoHolder> arrivalFlightsDictionary = new Dictionary<String, LiegeTools.FlightInfoHolder>();
        private Dictionary<String, LiegeTools.FlightInfoHolder> departureFlightsDictionary = new Dictionary<String, LiegeTools.FlightInfoHolder>();

        private List<FlightLink> flightLinks = new List<FlightLink>();
        internal List<LiegeTools.FlightInfoHolder> departureFlightsWithAlreadyUsedLink = new List<LiegeTools.FlightInfoHolder>();
        internal List<LiegeTools.ImportDataInformation> infoList = new List<LiegeTools.ImportDataInformation>();

        public AircraftLinksUpdate(DataTable _currentAircraftLinks, Dictionary<String, LiegeTools.FlightInfoHolder> _arrivalFlightsDictionary,
            Dictionary<String, LiegeTools.FlightInfoHolder> _departureFlightsDictionary)
        {
            currentAircraftLinks = _currentAircraftLinks;
            arrivalFlightsDictionary = _arrivalFlightsDictionary;
            departureFlightsDictionary = _departureFlightsDictionary;
        }

        public void updateAircraftLinksTable()
        {
            if (currentAircraftLinks != null && arrivalFlightsDictionary != null
                && departureFlightsDictionary != null)
            {
                flightLinks.Clear();
                foreach (KeyValuePair<string, LiegeTools.FlightInfoHolder> pair in departureFlightsDictionary)
                {
                    LiegeTools.FlightInfoHolder departureFlight = pair.Value;
                    if (departureFlight != null && departureFlight.linkFlightNb != null && departureFlight.linkFlightDate != null
                        && departureFlight.linkFlightNb != "")
                    {
                        LiegeTools.FlightInfoHolder linkedArrivalFlight 
                            = getLinkedFlightByFlightNbAndFlightDate(departureFlight.linkFlightNb,
                                                departureFlight.linkFlightDate, arrivalFlightsDictionary);
                        if (linkedArrivalFlight != null)
                        {
                            DateTime completeArrivalDate = linkedArrivalFlight.flightDate.Add(linkedArrivalFlight.flightTime);
                            DateTime completeDepartureDate = departureFlight.flightDate.Add(departureFlight.flightTime);
                            double minBetweenArrAndDep = completeDepartureDate.Subtract(completeArrivalDate).TotalMinutes;
                            FlightLink flightLink = new FlightLink(linkedArrivalFlight, departureFlight, minBetweenArrAndDep);
                            flightLinks.Add(flightLink);
                        }
                        else
                        {
                            LiegeTools.ImportDataInformation information = new LiegeTools.ImportDataInformation();
                            information.tableName = GlobalNames.FPDTableName;
                            information.flightIdInFP = departureFlight.id;
                            information.fileName = FlightPlanConversion.flightPlanFileName;
                            information.lineNbFromFile = departureFlight.textFileLineNb;
                            DateTime departureFlightCompleteDate = departureFlight.flightDate.Add(departureFlight.flightTime);
                            information.message = "The flight " + departureFlight.flightNumber + " from "
                                + departureFlightCompleteDate + " doesn't have a valid link ("
                                + departureFlight.linkFlightNb + " " + departureFlight.linkFlightDate
                                + "). It will use the Opening/Closing Times for Parking Stand occupation.";
                            infoList.Add(information);
                        }
                    }
                }
                if (flightLinks.Count > 0)
                {
                    fillAircraftLinksTable(currentAircraftLinks, flightLinks);
                    if (departureFlightsWithAlreadyUsedLink.Count > 0)
                    {
                        foreach (LiegeTools.FlightInfoHolder flightWithAlreadyUsedLink in departureFlightsWithAlreadyUsedLink)
                        {
                            LiegeTools.ImportDataInformation information = new LiegeTools.ImportDataInformation();
                            information.tableName = GlobalNames.FPDTableName;
                            information.flightIdInFP = flightWithAlreadyUsedLink.id;
                            information.fileName = FlightPlanConversion.flightPlanFileName;
                            information.lineNbFromFile = flightWithAlreadyUsedLink.textFileLineNb;
                            DateTime flightWithAlreadyUsedLinkFlightCompleteDate = flightWithAlreadyUsedLink.flightDate.Add(flightWithAlreadyUsedLink.flightTime);
                            information.message = "The flight " + flightWithAlreadyUsedLink.flightNumber + " from "
                                + flightWithAlreadyUsedLinkFlightCompleteDate + " tries to use an already assigned link ("
                                + flightWithAlreadyUsedLink.linkFlightNb + " " + flightWithAlreadyUsedLink.linkFlightDate
                                + "). It will use the Opening/Closing Times for Parking Stand occupation.";
                            infoList.Add(information);
                        }
                    }
                }
            }
        }

        private LiegeTools.FlightInfoHolder getLinkedFlightByFlightNbAndFlightDate(string linkFlightNb, DateTime linkFlightDate,
            Dictionary<String, LiegeTools.FlightInfoHolder> possibleLinkflightsDictionary)
        {
            LiegeTools.FlightInfoHolder linkedFlight = null;
            foreach (KeyValuePair<string, LiegeTools.FlightInfoHolder> pair in possibleLinkflightsDictionary)
            {
                LiegeTools.FlightInfoHolder possibleLinkFlight = pair.Value;
                if (possibleLinkFlight.flightNumber == linkFlightNb
                    && possibleLinkFlight.flightDate == linkFlightDate)
                {
                    linkedFlight = possibleLinkFlight;
                    break;
                }
            }
            return linkedFlight;
        }

        Dictionary<int, LiegeTools.FlightInfoHolder> departureFlightsLinkedDictionary = new Dictionary<int, LiegeTools.FlightInfoHolder>();        
        private void fillAircraftLinksTable(DataTable aircraftLinksTable, List<FlightLink> flightLinks)
        {
            if (aircraftLinksTable != null)
            {
                int arrivalFlightIdColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_FPAID);
                int arrivalFlightDateColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_STA);
                int arrivalFlightNumberColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_FPAName);
                int departureFlightIdColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_FPDID);
                int departureFlightDateColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_STD);
                int departureFlightNumberColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_FPDName);
                int rotationDurationColumnIndex = aircraftLinksTable.Columns.IndexOf(GlobalNames.sFPLinks_Column_RotationDuration);

                if (arrivalFlightIdColumnIndex != -1 && arrivalFlightDateColumnIndex != -1 && arrivalFlightNumberColumnIndex != -1
                    && departureFlightIdColumnIndex != -1 && departureFlightDateColumnIndex != -1 && departureFlightNumberColumnIndex != -1
                    && rotationDurationColumnIndex != -1)
                {
                    currentAircraftLinks.Clear();
                    foreach (FlightLink flightLink in flightLinks)
                    {
                        DataRow row = currentAircraftLinks.NewRow();
                        row[arrivalFlightIdColumnIndex] = flightLink.arrivalFlight.id;
                        DateTime completeArrivalFlightDate = flightLink.arrivalFlight.flightDate.Add(flightLink.arrivalFlight.flightTime);
                        row[arrivalFlightDateColumnIndex] = completeArrivalFlightDate;
                        row[arrivalFlightNumberColumnIndex] = flightLink.arrivalFlight.flightNumber;
                        row[departureFlightIdColumnIndex] = flightLink.departureFlight.id;
                        DateTime completeDepartureFlightDate = flightLink.departureFlight.flightDate.Add(flightLink.departureFlight.flightTime);
                        row[departureFlightDateColumnIndex] = completeDepartureFlightDate;
                        row[departureFlightNumberColumnIndex] = flightLink.departureFlight.flightNumber;
                        row[rotationDurationColumnIndex] = flightLink.minutesFromArrivalToDeparture;

                        if (departureFlightsLinkedDictionary.ContainsKey(flightLink.arrivalFlight.id))
                        {
                            departureFlightsWithAlreadyUsedLink.Add(flightLink.departureFlight);
                        }
                        else
                        {
                            departureFlightsLinkedDictionary.Add(flightLink.arrivalFlight.id, flightLink.departureFlight);
                            currentAircraftLinks.Rows.Add(row);
                        }
                    }
                    currentAircraftLinks.AcceptChanges();
                }
            }
        }
    }
}
