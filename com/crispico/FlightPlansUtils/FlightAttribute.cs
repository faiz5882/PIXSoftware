using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.FlightPlansUtils
{
    public class FlightAttribute
    {
        public enum ARR_OR_DEP_FLIGHT_TAG { NONE, A, D };

        internal ARR_OR_DEP_FLIGHT_TAG arrOrDep { get; set; }
        internal int flightId { get; set; }

        internal DateTime flightDate { get; set; }
        internal TimeSpan flightTime { get; set; }
        internal string airlineCode { get; set; }
        internal string flightNb { get; set; }
        internal string airportCode { get; set; }
        internal string flightCategory { get; set; }
        internal string aircraftType { get; set; }

        internal bool tsa { get; set; }
        internal bool cbp { get; set; }
        internal bool noBsm { get; set; }

        internal string user1 { get; set; }
        internal string user2 { get; set; }
        internal string user3 { get; set; }
        internal string user4 { get; set; }
        internal string user5 { get; set; }

        public FlightAttribute()
        {
            flightDate = DateTime.MinValue;
            flightTime = TimeSpan.MinValue;
        }

        public FlightAttribute(ARR_OR_DEP_FLIGHT_TAG pArrOrDep, int pFlightId, DateTime pFlightDate, TimeSpan pFlightTime, string pAirlineCode, string pFlightNb, string pAirportCode, string pFlightCategory,
            string pAircraftType, bool pTsa, bool pCbp, bool pNoBsm, string pUser1, string pUser2, string pUser3, string pUser4, string pUser5)
        {
            flightDate = pFlightDate;
            flightTime = pFlightTime;

            airlineCode = pAirlineCode;
            flightNb = pFlightNb;
            airportCode = pAirportCode;
            flightCategory = pFlightCategory;
            aircraftType = pAircraftType;

            tsa = pTsa;
            cbp = pCbp;
            noBsm = pNoBsm;

            user1 = pUser1;
            user2 = pUser2;
            user3 = pUser3;
            user4 = pUser4;
            user5 = pUser5;
        }
    }
}
