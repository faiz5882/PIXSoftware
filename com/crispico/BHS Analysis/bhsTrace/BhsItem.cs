using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis.bhsTrace
{
    class BhsItem
    {
        private const string DELIMITER = " |  ";

        public enum STATUS_TYPE
        {
            OK, STOPPED, MISSED, LOST
        }

        public int bagId { get; set; }
        public int paxId { get; set; }
        public int flightId { get; set; }
        public double minutesUntilFlightDeparture { get; set; }
        public int recirculationNb { get; set; }
        public int interlink { get; set; }
        public STATUS_TYPE status { get; set; } //stopped - missed - lost -> attribute of the item
        public List<AccessedStation> accessedStations = new List<AccessedStation>();

        public BhsItem(int bagId, int paxId, int flightId, double minutesUntilFlightDeparture,
            int recirculationNb, int interlink, STATUS_TYPE status)
        {
            this.bagId = bagId;
            this.paxId = paxId;
            this.flightId = flightId;
            this.minutesUntilFlightDeparture = minutesUntilFlightDeparture;
            this.recirculationNb = recirculationNb;
            this.interlink = interlink;
            this.status = status;
        }

        public override string ToString()
        {
            return "BagId: " + bagId + DELIMITER + "PaxId: " + paxId + DELIMITER + "FlightId: " + flightId + DELIMITER + "ST: " + minutesUntilFlightDeparture + DELIMITER
                + "Recirc: " + recirculationNb + DELIMITER + "InterLink: " + interlink + DELIMITER + "Status: " + status.ToString() + DELIMITER;
        }
    }
}
