using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.com.crispico.inputs.flight_group_rules;

namespace SIMCORE_TOOL.com.crispico.inputs.deterministic_transfer_distribution
{
    public class RuleAndDeterministicValues
    {
        public FlightGroupRulesParameter rule { get; set; }
        public int nbTransferBags { get; set; }

    }
}
