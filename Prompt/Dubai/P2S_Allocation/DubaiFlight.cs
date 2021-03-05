using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.Prompt.Dubai.P2S_Allocation
{
    class DubaiFlight
    {        
        internal int id { get; set; }
        internal DateTime date { get; set; }
        internal TimeSpan time { get; set; }
        internal string airlineCode { get; set; }
        internal string flightNumber { get; set; }
        internal string airportCode { get; set; }
        internal string flightCategory { get; set; }
        internal string aircraftType { get; set; }
        internal int nbSeats { get; set; }

        internal int flightPlanEcoCheckInStartDeskNb { get; set; }
        internal int flightPlanEcoCheckInEndDeskNb { get; set; }
        internal int flightPlanBoardingGateNb { get; set; }
        internal int flightPlanParkingStandNb { get; set; }
                
        internal string user1 { get; set; }
        internal string user2 { get; set; }
        internal string user3 { get; set; }
        internal string user4 { get; set; }
        internal string user5 { get; set; }
        internal TimeInterval occupiedInterval { get; set; }
        internal List<DubaiResource> allocatedResources = new List<DubaiResource>();
        internal DubaiFlight linkedFlight { get; set; }

        internal string aircraftBodyCategory { get; set; }


    }
}
