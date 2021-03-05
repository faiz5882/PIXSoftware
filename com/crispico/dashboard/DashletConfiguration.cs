using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.dashboard
{
    // >> Task #10701 Pax2Sim - Pax analysis - saving the Dashboard configuration
    class DashletConfiguration
    {
        public String name { get; set; }

        public bool isVisible { get; set; }

        public int positionIndex { get; set; }

        public DashletConfiguration()
        {
            name = "";
            isVisible = false;
            positionIndex = -1;
        }
    
    }
}
