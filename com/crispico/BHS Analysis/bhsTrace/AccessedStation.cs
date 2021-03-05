using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis.bhsTrace
{
    class AccessedStation
    {
        public BhsStation station { get; set; }
        public double arrivingMinute { get; set; }
        public double leavingMinute { get; set; }

        public AccessedStation(BhsStation station, double arrivingMinute, double leavingMinute)
        {
            this.station = station;
            this.arrivingMinute = arrivingMinute;
            this.leavingMinute = leavingMinute;
        }

        public override string ToString()
        {
            return station.ToString() + " | " + "In: " + arrivingMinute + " - Out: " + leavingMinute;
        }
    }
}
