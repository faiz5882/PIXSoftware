using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis.bhsTrace
{
    class AccessingItem
    {
        public BhsItem item { get; set; }
        public double arrivingMinute { get; set; }
        public double leavingMinute { get; set; }

        public AccessingItem(BhsItem item, double arrivingMinute, double leavingMinute)
        {
            this.item = item;
            this.arrivingMinute = arrivingMinute;
            this.leavingMinute = leavingMinute;
        }

        public override string ToString()
        {
            return item.ToString() + " | " + "In: " + arrivingMinute + " - Out: " + leavingMinute;
        }
    }
}
