using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.EBS
{
    class EbsIssue
    {
        const string SEPARATOR = " | ";
              
        internal enum BAG_TYPE { ECO, FB};
        internal enum EBS_PROCESS_TYPE { INPUT, OUTPUT };

        public DateTime overflowTime { get; set; }
        public string relatedTableName { get; set; }
        public EbsFlight flight { get; set; }
        public double nbOverflowBags { get; set; }
        public BAG_TYPE bagType { get; set; }
        public EBS_PROCESS_TYPE processType { get; set; }

        public EbsIssue(DateTime overflowTime, string tableName, EbsFlight flight, double nbOverflowBags, BAG_TYPE bagType, EBS_PROCESS_TYPE processType)
        {
            this.overflowTime = overflowTime;
            this.relatedTableName = tableName;
            this.flight = flight;
            this.nbOverflowBags = nbOverflowBags;
            this.bagType = bagType;
            this.processType = processType;
        }

        public override string ToString()
        {
            return "Overflow time: " + overflowTime.ToString(EbsLogger.completeDateFormat) + SEPARATOR + "Table: " + relatedTableName + SEPARATOR + "FlightId: " + flight.id + SEPARATOR 
                + "OvfBags: " + nbOverflowBags + SEPARATOR + "BagType: " + bagType + SEPARATOR + "ProcessType: " + processType;
        }
    }
}
