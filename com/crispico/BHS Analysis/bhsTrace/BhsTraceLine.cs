using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis.bhsTrace
{
    class BhsTraceLine
    {
        public static string NO_END_OF_LINE_KEYWORD = "";
        private static string DELIMITER = " |#| ";

        public BhsItem bhsItem { get; set; }
        public List<BhsTraceLineEntry> entries = new List<BhsTraceLineEntry>();
        public string endOfLineKeyword { get; set; }

        public BhsTraceLine(BhsItem bhsItem, string endOfLineKeyword)
        {
            this.bhsItem = bhsItem;
            this.endOfLineKeyword = endOfLineKeyword;
        }

        public override string ToString()
        {
            string traceLineEntriesToString = "";
            foreach (BhsTraceLineEntry entry in entries)
            {
                traceLineEntriesToString += entry.ToString();
            }
            return "BhsItem: " + bhsItem.ToString() + DELIMITER + traceLineEntriesToString + DELIMITER + "Keyword: " + endOfLineKeyword;
        }
    }
}
