using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.com.crispico.general_allocation.constants;

namespace SIMCORE_TOOL.com.crispico.general_allocation.parameter
{
    class FlightGroupingRuleCondition
    {
        internal GeneralAllocationConstants.P2S_ATTRIBUTES p2sAttribute = GeneralAllocationConstants.P2S_ATTRIBUTES.DEFAULT;
        internal GeneralAllocationConstants.OPERATORS conditionOperator = GeneralAllocationConstants.OPERATORS.DEFAULT;
        internal List<string> inputValues = new List<string>();

        internal FlightGroupingRuleCondition(GeneralAllocationConstants.P2S_ATTRIBUTES pP2sAttribute,
            GeneralAllocationConstants.OPERATORS pConditionOperator, List<string> pInputValues)
        {
            p2sAttribute = pP2sAttribute;
            conditionOperator = pConditionOperator;
            inputValues = pInputValues;
        }

        public override string ToString()
        {
            string inputs = "";
            for (int i = 0; i < inputValues.Count; i++)
            {
                if (i < inputValues.Count - 1)
                {
                    inputs += inputValues[i] + ", ";
                }
                else
                {
                    inputs += inputValues[i];
                }
            }
            return p2sAttribute + " | " + conditionOperator + " | " + inputs;
        }
    }
}
