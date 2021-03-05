using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class ResourcePriority
    {
        internal string resourceName { get; set; }
        internal int priority { get; set; }

        public ResourcePriority(string _resourceCode, int _priority)
        {
            resourceName = _resourceCode;
            priority = _priority;
        }
    }
}
