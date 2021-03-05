using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.CDG
{
    class BaggageTriage
    {
        internal int nbTotalBags { get; set; }
        internal int nbDirectBags { get; set; }
        internal int nbEBSBags { get; set; }
        internal int startMinute { get; set; }
        internal int endMinute { get; set; }

        public BaggageTriage(int _nbTotalBags,
            int _nbDirectBags, int _nbEBSBags,
            int _startMinute, int timeSlotLengthMinutes)
        {
            nbTotalBags = _nbTotalBags;
            nbDirectBags = _nbDirectBags;
            nbEBSBags = _nbEBSBags;
            startMinute = _startMinute;
            endMinute = _startMinute + timeSlotLengthMinutes;
        }

        public override string ToString()
        {
            return startMinute + " - " + endMinute + " || Total: " + nbTotalBags + ", Direct: " + nbDirectBags + ", EBS: " + nbEBSBags;
        }
    }
}
