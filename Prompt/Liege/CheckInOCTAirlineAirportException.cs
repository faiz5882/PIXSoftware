using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class CheckInOCTAirlineAirportException
    {
        internal const string UNIQUE_IDENTIFIER_DELIMITER = "_";

        internal string airlineCode { get; set; }
        internal string airportCode { get; set; }
        internal int openingTimeInMinutes { get; set; }
        internal int closingTimeInMinutes { get; set; }
        internal int partialOpeningTimeInMintues { get; set; }
        internal int nbAllocatedStations { get; set; }
        internal int nbOpenedStationsAtPartial { get; set; }
        internal int nbAdditionalStations { get; set; }

        public CheckInOCTAirlineAirportException(string _airlineCode, string _airportCode,
            int _openingTime, int _closingTime, int _partialOpeningTime, int _nbAllocated,
            int _nbOpenedAtPartial, int _nbAdditional)
        {
            airlineCode = _airlineCode;
            airportCode = _airportCode;
            openingTimeInMinutes = _openingTime;
            closingTimeInMinutes = _closingTime;
            partialOpeningTimeInMintues = _partialOpeningTime;
            nbAllocatedStations = _nbAllocated;
            nbOpenedStationsAtPartial = _nbOpenedAtPartial;
            nbAdditionalStations = _nbAdditional;
        }

        internal string getUniqueIdentifier()
        {
            return airlineCode + UNIQUE_IDENTIFIER_DELIMITER + airportCode;
        }
    }
}
