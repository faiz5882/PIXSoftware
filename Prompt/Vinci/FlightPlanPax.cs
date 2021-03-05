using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Vinci
{
    class FlightPlanPax
    {
        internal const string FLIGHT_ID_PAX_COLUMN_NAME = "Flight ID";
        internal const string EXPECTED_PAX_PAX_COLUMN_NAME = "Expected Pax";
        internal const string GENERATED_PAX_PAX_COLUMN_NAME = "Generated Pax";
        internal const string TERMINATING_PAX_PAX_COLUMN_NAME = "Terminating Pax";
        internal const string TRANSFERRING_PAX_PAX_COLUMN_NAME = "Transferred Pax";
        internal const string ORIGINATING_PAX_PAX_COLUMN_NAME = "Originating Pax";
        internal const string SELF_CI_PAX_FPD_PAX_COLUMN_NAME = "Self Check In Pax";
        
        public int flightId { get; set; }
        public double expectedPax { get; set; }
        public double generatedPax { get; set; }
        public double terminatingPax { get; set; }
        public double transferringPax { get; set; }
        public double originatingPax { get; set; }
        public double selfCheckInPax { get; set; }
    }
}
