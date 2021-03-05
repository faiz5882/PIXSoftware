using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class ResourceOccupationNeed
    {
        internal int nbStations { get; set; }
        internal int nbAdditionalStations { get; set; }
        internal TimeInterval occupationInterval { get; set; }

        public ResourceOccupationNeed(int _nbStations, int _nbAdditionalStations,
            TimeInterval _occupationInterval)
        {
            nbStations = _nbStations;
            nbAdditionalStations = _nbAdditionalStations;
            occupationInterval = _occupationInterval;
        }
    }
}
