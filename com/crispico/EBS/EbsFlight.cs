using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.EBS
{
    class EbsFlight
    {
        const string SEPARATOR = " | ";

        public string id { get; set; }
        public string number { get; set; }
        public string flightCategory { get; set; }
        public string airline { get; set; }
        public DateTime flightDate { get; set; }
        public TimeSpan flightTime { get; set; }

        public EbsFlight(string id, string nb, string category, string airline, DateTime date, TimeSpan time)
        {
            this.id = id;
            this.number = nb;
            this.flightCategory = category;
            this.airline = airline;
            this.flightDate = date;
            this.flightTime = time;
        }

        public override string ToString()
        {
            DateTime completeDate = flightDate.Add(flightTime);
            return "ID: " + id + SEPARATOR + "Date: " + completeDate + SEPARATOR + "Nb: " + number 
                + SEPARATOR + "CAT: " + flightCategory + SEPARATOR + "Airline: " + airline;
        }
    }
}
