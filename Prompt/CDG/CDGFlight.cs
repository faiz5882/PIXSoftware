using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.CDG
{
    class CDGFlight
    {
        internal int id { get; set; }
        internal string flightNb { get; set; }
        internal DateTime flightDate { get; set; }
        internal string flightNbWithSuffix { get; set; }
        internal int etd { get; set; }
        internal int terminal {get;set;}
        
        internal string realFlightCategory { get; set; }
        internal string user1_columnJ { get; set; }
        internal int user2_NumberInSimulation
        {
            get
            {
                int nbInSimulation = -1;
                if (user1_columnJ != null)
                    nbInSimulation = getNumberInSimulation(user1_columnJ.Trim());
                return nbInSimulation;
            }
        }

        public CDGFlight(int _id, string _flightNb, int _etd, string flNbSuffix,
            DateTime date, string _realFlightCategory, string _user1ColumnJ)
        {
            id = _id;
            flightDate = date.AddMinutes(_etd);
            flightNb = _flightNb;
            etd = _etd;            
            flightNbWithSuffix = flightNb + flNbSuffix;
            if (flNbSuffix == "_S3")
                terminal = 3;
            else if (flNbSuffix == "_S4")
                terminal = 4;
            realFlightCategory = _realFlightCategory;
            user1_columnJ = _user1ColumnJ;
        }

        public override string ToString()
        {
            return flightNbWithSuffix + " " + etd;
        }


        private int getNumberInSimulation(string user1_columnJ)
        {
            if (user1_columnJ == "C401")
                return 41;
            else if (user1_columnJ == "C402")
                return 42;
            else if (user1_columnJ == "C403")
                return 43;
            else if (user1_columnJ == "C404")
                return 44;
            else if (user1_columnJ == "C405")
                return 45;
            else if (user1_columnJ == "C406")
                return 46;
            else if (user1_columnJ == "C407")
                return 47;
            else if (user1_columnJ == "C408")
                return 48;
            else if (user1_columnJ == "C409")
                return 49;
            else if (user1_columnJ == "C410")
                return 50;
            else if (user1_columnJ == "J300")
                return 1;
            else if (user1_columnJ == "C301")
                return 2;
            else if (user1_columnJ == "J302")
                return 3;
            else if (user1_columnJ == "J310")
                return 4;
            else if (user1_columnJ == "C311")
                return 5;
            else if (user1_columnJ == "J312")
                return 6;
            else if (user1_columnJ == "J320")
                return 7;
            else if (user1_columnJ == "C321")
                return 8;
            else if (user1_columnJ == "J322")
                return 9;
            else if (user1_columnJ == "J330")
                return 10;
            else if (user1_columnJ == "C331")
                return 11;
            else if (user1_columnJ == "J332")
                return 12;
            else if (user1_columnJ == "C341")
                return 13;
            else if (user1_columnJ == "C351")
                return 14;
            else if (user1_columnJ == "J342")
                return 15;
            else if (user1_columnJ == "J350")
                return 16;
            return -1;
        }

    }
}
