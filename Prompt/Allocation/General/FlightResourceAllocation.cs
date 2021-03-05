using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.Prompt.Allocation.General
{
    class FlightResourceAllocation
    {
        internal Flight allocatedFlight { get; set; }
        internal Resource resource { get; set; }
        internal TimeInterval occupationInterval { get; set; }

        public FlightResourceAllocation(Flight _allocatedFlight, 
            Resource _resource, TimeInterval _occupationInterval)
        {
            allocatedFlight = _allocatedFlight;
            resource = _resource;
            occupationInterval = _occupationInterval;
        }

        public override string ToString()
        {
            if (allocatedFlight == null || resource == null || occupationInterval == null)
                return "";
            return allocatedFlight.ToString() + " " + resource.ToString() + " " + occupationInterval.ToString();
        }
    }
}
