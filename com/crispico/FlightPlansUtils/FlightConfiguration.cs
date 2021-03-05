using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.FlightPlansUtils
{
    class FlightConfiguration
    {
        internal FlightAttribute flightAttribute { get; set; }
        internal FlightParameter flightParameter { get; set; }

        public FlightConfiguration()
        {
        }

        public FlightConfiguration(FlightAttribute pFlightAttribute, FlightParameter pFlightParameter)
        {
            flightAttribute = pFlightAttribute;
            flightParameter = pFlightParameter;
        }
               
    }
}
