using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.dashboard
{
    // >> Task #10701 Pax2Sim - Pax analysis - saving the Dashboard configuration
    class DashboardConfiguration
    {
        public String stationName { get; set; }

        public String preferenceInfoName { get; set; }

        public String preferences { get; set; }

        public int numberOfRows { get; set; }

        public int numberOfColumns { get; set; }

        public List<DashletConfiguration> dashletConfigurationsList { get; set; }

        public DashboardConfiguration()
        {
            stationName = "";
            preferenceInfoName = "";
            preferences = "";
            numberOfRows = 3;
            numberOfColumns = 3;
            dashletConfigurationsList = new List<DashletConfiguration>();
        }
    }
}
