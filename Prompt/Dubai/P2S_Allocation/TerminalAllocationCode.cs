using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Dubai.P2S_Allocation
{
    class TerminalAllocationCode
    {
        internal int terminalNb { get; set; }
        internal string allocationCode { get; set; }
        internal List<string> parkingLocationCodes = new List<string>();

        public TerminalAllocationCode(int _terminalNb, string _allocationCode,
            List<string> _parkingLocationCodes)
        {
            terminalNb = _terminalNb;
            allocationCode = _allocationCode;
            parkingLocationCodes = _parkingLocationCodes;
        }

        public override string ToString()
        {
            return terminalNb + "_" + allocationCode;
        }
    }
}
