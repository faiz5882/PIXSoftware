using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class FlightAllocation
    {
        internal Flight allocatedFlight { get; set; }
        internal Resource resource { get; set; }
        internal TimeInterval occupationInterval { get; set; }

        public FlightAllocation(Flight _allocatedFlight, 
            Resource _resource, TimeInterval _occupationInterval)
        {
            allocatedFlight = _allocatedFlight;
            resource = _resource;
            occupationInterval = _occupationInterval;
        }
    }
}
