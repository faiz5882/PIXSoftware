using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.dashboard
{
    /// <summary>
    /// Holds the information for the KPI used in the Summary tables.
    /// Will be used to send the KPI data from C# to Flex.    
    /// </summary>
    public class SummaryKPI
    {
        public String name { get; set; }

        /// <summary>
        /// Real name is assigned only when the name field contains a preffix or suffix.
        /// Ex.: if name = HBS_1_0_Occupation => realName = Occupation
        /// Useful in setting the KPI type.
        /// </summary>
        public String realName { get; set; }

        /// <summary>
        /// In the global statistics we get rhe child stations' statistics.
        /// This will keep the name of the child station to which the KPI belongs.
        /// For the general statistics this field is empty string ("").
        /// </summary>
        public String childStationName { get; set; }

        public String type { get; set; }

        public String description { get; set; }

        public Double value { get; set; }

        public String valueUnitName { get; set; }

        public Double minValue { get; set; }

        public Double avgValue { get; set; }

        public Double maxValue { get; set; }

        public Double level1MinValue { get; set; }

        public Double level1AvgValue { get; set; }

        public Double level1MaxValue { get; set; }

        public Double level2MinValue { get; set; }

        public Double level2AvgValue { get; set; }

        public Double level2MaxValue { get; set; }

        public Double level3MinValue { get; set; }

        public Double level3AvgValue { get; set; }

        public Double level3MaxValue { get; set; }

        public String unitName { get; set; }

        public bool isBHSkpi { get; set; }

        public bool isVisibleByDefault { get; set; }

        public bool isBHSTimesDirectoryKpi { get; set; }

        public bool showByDefault { get; set; } // >> Task #9171 Pax2Sim - Static Analysis - EBS algorithm - Throughputs C#5

        public SummaryKPI()
        {
            name = "";
            realName = "";
            childStationName = "";
            description = "";
            value = -1;
            valueUnitName = "";
            minValue = -1;
            avgValue = -1;
            maxValue = -1;
            level1MinValue = -1;
            level1AvgValue = -1;
            level1MaxValue = -1;
            level2MinValue = -1;
            level2AvgValue = -1;
            level2MaxValue = -1;
            level3MinValue = -1;
            level3AvgValue = -1;
            level3MaxValue = -1;
            unitName = "";
            isBHSkpi = false;
            isBHSTimesDirectoryKpi = false;
            isVisibleByDefault = false;
        }
        private const string SEPARATOR = " | ";
        public override string ToString()
        {
            return "Name: " + name + SEPARATOR + "Total: " + value + valueUnitName + SEPARATOR + "Min: " + minValue + SEPARATOR + "Avg: " + avgValue
                + SEPARATOR + "Max: " + maxValue + SEPARATOR + "Perc1: " + level1MaxValue + SEPARATOR + "Perc2: " + level2MaxValue
                + SEPARATOR + "Perc3: " + level3MaxValue + SEPARATOR + "Unit: " + unitName;
        }
    }
}
