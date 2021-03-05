using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class FlightLink
    {
        internal LiegeTools.FlightInfoHolder arrivalFlight { get; set; }
        internal LiegeTools.FlightInfoHolder departureFlight { get; set; }
        internal double minutesFromArrivalToDeparture { get; set; }

        public FlightLink(LiegeTools.FlightInfoHolder _arrivalFlight,
            LiegeTools.FlightInfoHolder _departureFlight, double _minBetweenArrAndDep)
        {
            arrivalFlight = _arrivalFlight;
            departureFlight = _departureFlight;
            minutesFromArrivalToDeparture = _minBetweenArrAndDep;
        }
    }
}
