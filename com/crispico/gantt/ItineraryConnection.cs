using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.gantt
{
    /// <summary>
    /// This class is used to create Connection
    /// objects based on the data received from Flex.
    /// These objects will help update the Itinerary Table.
    /// </summary>
    public class ItineraryConnection
    {
        //the Key as seen in the ArrowParam class
        public String sourceName { get; set; }

        //the Goal as seen in the ArrowParam class
        public String targetName { get; set; }

        public String weight { get; set; }

        public String distributionType { get; set; }

        public String firstParam { get; set; }

        public String secondParam { get; set; }

        public String thirdParam { get; set; }

        //bending points
        public List<Bendpoint> bendpoints { get; set; }

        public ItineraryConnection()
        {
            sourceName = "";
            targetName = "";
            weight = "";
            distributionType = "";
            firstParam = "";
            secondParam = "";
            thirdParam = "";
            bendpoints = new List<Bendpoint>();
        }

    }
}
