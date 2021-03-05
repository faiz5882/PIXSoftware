using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.com.crispico.FlightPlansUtils;

namespace SIMCORE_TOOL.com.crispico.inputs.deterministic_transfer_distribution
{
    public class DeterministicTransferDistributionParameter
    {
        public string flightPlanTableName { get; set; }

        public FlightAttribute flight { get; set; }
        
        public List<RuleAndDeterministicValues> deterministicValues = new List<RuleAndDeterministicValues>();

    }
}
