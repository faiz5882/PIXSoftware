using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis.bhsTrace
{
    class BhsStation
    {
        private const string DELIMITER = " | ";

        public string name { get; set; }
        public string type { get; set; }
        public int terminalIndex { get; set; }
        public int groupIndex { get; set; }
        public int stationIndex { get; set; }
        public List<AccessingItem> accessingItems = new List<AccessingItem>();

        public BhsStation(string name, string type, int terminalIndex, int groupIndex, int stationIndex)
        {    
            this.name = name;
            this.type = type;
            this.terminalIndex = terminalIndex;
            this.groupIndex = groupIndex;
            this.stationIndex = stationIndex;
        }

        public override string ToString()
        {
            return "Type: " + type + DELIMITER + "T: " + terminalIndex + DELIMITER + "G: " + groupIndex 
                + DELIMITER + "S: " + stationIndex + DELIMITER;
        }

    }
}
