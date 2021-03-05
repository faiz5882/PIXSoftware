using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.EBS
{
    class EbsFlightStatistics
    {
        const string SEPARATOR = " | ";

        public string relatedTableName { get; set; }
        public EbsFlight flight { get; set; }
        
        public DateTime inputOverflowTime { get; set; }
        public DateTime outputOverflowTime { get; set; }

        public double inputOverflowNbEcoBags { get; set; }
        public double inputOverflowNbFbBags { get; set; }
        public double inputOverflowNbTotalBags
        {
            get
            {
                return Math.Round(inputOverflowNbEcoBags + inputOverflowNbFbBags, 1);
            }
        }
                
        public double outputOverflowNbEcoBags { get; set; }
        public double outputOverflowNbFbBags { get; set; }
        public double outputOverflowNbTotalBags
        {
            get
            {
                return Math.Round(outputOverflowNbEcoBags + outputOverflowNbFbBags, 1);
            }
        }

        public double totalNbOverflowBags
        {
            get
            {
                return Math.Round(inputOverflowNbTotalBags + outputOverflowNbTotalBags, 1);
            }
        }

        public EbsFlightStatistics()
        {
        }

        public EbsFlightStatistics(string relatedTableName, EbsFlight flight, DateTime inputOverflowTime, DateTime outputOverflowTime,
            double inputOverflowNbEcoBags, double inputOverflowNbFbBags, double outputOverflowNbEcoBags, double outputOverflowNbFbBags)
        {
            this.relatedTableName = relatedTableName;
            this.flight = flight;

            this.inputOverflowTime = inputOverflowTime;
            this.outputOverflowTime = outputOverflowTime;

            this.inputOverflowNbEcoBags = inputOverflowNbEcoBags;
            this.inputOverflowNbFbBags = inputOverflowNbFbBags;

            this.outputOverflowNbEcoBags = outputOverflowNbEcoBags;
            this.outputOverflowNbFbBags = outputOverflowNbFbBags;
        }

        public override string ToString()
        {
            return "Table: " + relatedTableName + SEPARATOR + "FlightId: " + flight.id + SEPARATOR + "In.Ovf.Time: " + inputOverflowTime + SEPARATOR + "Out.Ovf.Time: " + outputOverflowTime
                + SEPARATOR + "In.Ovf.Eco: " + inputOverflowNbEcoBags + SEPARATOR + "In.Ovf.Fb: " + inputOverflowNbFbBags + SEPARATOR + "In.Ovf.Total: " + inputOverflowNbTotalBags
                + SEPARATOR + "Out.Ovf.Eco: " + outputOverflowNbEcoBags + SEPARATOR + "Out.Ovf.Fb: " + outputOverflowNbFbBags + SEPARATOR + "Out.Ovf.Total: " + outputOverflowNbTotalBags;
        }
    }
}
