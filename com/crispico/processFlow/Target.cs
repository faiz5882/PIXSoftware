using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.processFlow
{
    public class Target
    {
        public String processName { get; set; }

        public String targetAchieved { get; set; }

        public Target()
        {
        }

        public Target(String pProcessName, String pTargetAchieved)
        {
            processName = pProcessName;
            targetAchieved = pTargetAchieved;
        }
    }
}
