using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.EBS
{
    class EbsSummaryKpi
    {
        public int id { get; set; }
        public string name { get; set; }

        public double total { get; set; }
        public string totalUnit { get; set; }

        public double min { get; set; }
        public double avg { get; set; }
        public double max { get; set; }
        public string unit { get; set; }

        public EbsSummaryKpi(int id, string name, double total, string totalUnit,
            double min, double avg, double max, string unit)
        {
            this.id = id;
            this.name = name;
            this.total = total;
            this.totalUnit = totalUnit;
            this.min = min;
            this.avg = avg;
            this.max = max;
            this.unit = unit;
        }
    }
}
