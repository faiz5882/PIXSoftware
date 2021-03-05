using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis.bhsTrace
{
    class BhsTraceLineEntry
    {
        private const string DELIMITER = " | ";

        public int traceLinePosition { get; set; }
        public BhsStation station { get; set; }
        public double arrivingMinute { get; set; }
        public double leavingMinute { get; set; }

        public BhsTraceLineEntry(int traceLinePosition, BhsStation station, double arrivalMinute, double leavingMinute)
        {
            this.traceLinePosition = traceLinePosition;
            this.station = station;
            this.arrivingMinute = arrivalMinute;
            this.leavingMinute = leavingMinute;
        }

        public override string ToString()
        {
            return "Pos: " + traceLinePosition + DELIMITER + station.ToString() + DELIMITER + "In: " + arrivingMinute + DELIMITER + "Out: " + leavingMinute + DELIMITER;
        }
    }
}
