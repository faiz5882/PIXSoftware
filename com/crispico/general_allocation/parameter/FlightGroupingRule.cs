using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.general_allocation.parameter
{
    internal class FlightGroupingRule
    {
        internal string name { get; set; }
        internal string description { get; set; }
        internal List<FlightGroupingRuleCondition> ruleConditions = new List<FlightGroupingRuleCondition>();

        internal FlightGroupingRule(string pName, string pDescription, List<FlightGroupingRuleCondition> pConditions)
        {
            name = pName;
            description = pDescription;
            ruleConditions = pConditions;
        }

        public override string ToString()
        {
            string conditions = "";
            for (int i = 0; i < ruleConditions.Count; i++)
            {
                if (i < ruleConditions.Count - 1)
                {
                    conditions += ruleConditions[i] + " || ";
                }
                else
                {
                    conditions += ruleConditions[i];
                }
            }
            return name + ", " + description + ", " + conditions;
        }
    }
}
