using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.inputs.flight_group_rules
{
    public class FlightGroupRulesParameter
    {
        public string ruleName { get; set; }

        public List<string> flightCategories = new List<string>();
        public List<string> airlines = new List<string>();
        public List<string> airports = new List<string>();
        public List<string> user1Contents = new List<string>();
        public List<string> user2Contents = new List<string>();
        public List<string> user3Contents = new List<string>();
        public List<string> user4Contents = new List<string>();
        public List<string> user5Contents = new List<string>();

    }
}
