using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.gantt
{

    /// <summary>
    /// The ItineraryData object is used to transfer the data needed from P2S to Flex
    /// The data can be: Group objects, Scenario objects or Chart objects.
    /// The data transfered represents only the data needed for the Flex part
    /// (ex for the Group object - the name and description)
    /// </summary>
    public class ItineraryData
    {
        public String name { get; set; }

        public String description { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public bool onItinerary { get; set; }

        // can be Group object, Scenario object, Chart object
        public String itineraryDataType { get; set; }

        // The process time as string - type and params
        public String paxProcessTimes { get; set; }

        public String paxProcessTimeType { get; set; }

        public String paxProcessTimeFirstParam { get; set; }

        public String paxProcessTimeSecondParam { get; set; }

        public String paxProcessTimeThirdParam { get; set; }

        //<< The process time as string - type and params
        public String bagProcessTimes { get; set; }
        public String bagProcessTimeType { get; set; }
        public String bagProcessTimeFirstParam { get; set; }
        public String bagProcessTimeSecondParam { get; set; }
        public String bagProcessTimeThirdParam { get; set; }
        public String capacity { get; set; }
        //>>

        //<<The background color data for Background Groups // for Groups - stores fake data
        public String firstGradientColor { get; set; }
        public String firstGradientAlpha { get; set; }
        public String secondGradientColor { get; set; }
        public String secondGradientAlpha { get; set; }
        public String borderColor { get; set; }
        public String gradientAngle { get; set; }
        //>>

        // << Task #8789 Pax2Sim - ProcessFlow - update Group details
        public String processingCapacity { get; set; }
        public String delayTimeDistributionReferenceTime { get; set; }
        public String delayTimeDistribution { get; set; }
        public String delayTimeDistributionFirstParam { get; set; }
        public String delayTimeDistributionSecondParam { get; set; }
        public String delayTimeDistributionThirdParam { get; set; }
        // >> Task #8789 Pax2Sim - ProcessFlow - update Group details

        public ItineraryData()
        {
            name = "";
            description = "";
            itineraryDataType = "";
            x = Model.INITIAL_X_AXIS_VALUE;
            y = Model.INITIAL_Y_AXIS_VALUE;
            height = Model.STANDARD_HEIGHT;
            width = Model.STANDARD_WIDTH;
            onItinerary = false;
            paxProcessTimes = "";
            bagProcessTimes = "";
            capacity = "";
            // << Task #8789 Pax2Sim - ProcessFlow - update Group details
            processingCapacity = "";
            delayTimeDistributionReferenceTime = "";
            delayTimeDistribution = "";
            delayTimeDistributionFirstParam = "";
            delayTimeDistributionSecondParam = "";
            delayTimeDistributionThirdParam = "";
            // >> Task #8789 Pax2Sim - ProcessFlow - update Group details
        }

        public ItineraryData clone()
        {
            ItineraryData itineraryDataClone = new ItineraryData();
            itineraryDataClone.name = this.name;
            itineraryDataClone.description = this.description;
            itineraryDataClone.itineraryDataType = this.itineraryDataType;
            itineraryDataClone.x = this.x;
            itineraryDataClone.y = this.y;
            itineraryDataClone.height = this.height;
            itineraryDataClone.width = this.width;
            itineraryDataClone.onItinerary = this.onItinerary;
            itineraryDataClone.paxProcessTimes = this.paxProcessTimes;
            itineraryDataClone.bagProcessTimes = this.bagProcessTimes;
            itineraryDataClone.capacity = this.capacity;
            itineraryDataClone.firstGradientColor = this.firstGradientColor;
            itineraryDataClone.firstGradientAlpha = this.firstGradientAlpha;
            itineraryDataClone.secondGradientColor = this.secondGradientColor;
            itineraryDataClone.secondGradientAlpha = this.secondGradientAlpha;
            itineraryDataClone.borderColor = this.borderColor;
            itineraryDataClone.gradientAngle = this.gradientAngle;
            // << Task #8789 Pax2Sim - ProcessFlow - update Group details
            itineraryDataClone.processingCapacity = this.processingCapacity;
            itineraryDataClone.delayTimeDistribution = this.delayTimeDistribution;
            itineraryDataClone.delayTimeDistributionReferenceTime = this.delayTimeDistributionReferenceTime;
            itineraryDataClone.delayTimeDistributionFirstParam = this.delayTimeDistributionFirstParam;
            itineraryDataClone.delayTimeDistributionSecondParam = this.delayTimeDistributionSecondParam;
            itineraryDataClone.delayTimeDistributionThirdParam = this.delayTimeDistributionThirdParam;
            // >> Task #8789 Pax2Sim - ProcessFlow - update Group details

            return itineraryDataClone;
        }
    }
}
