using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Allocation.General
{
    class Utils
    {
        public static bool areValidColumnIndexes(List<int> columnIndexes)
        {
            foreach (int index in columnIndexes)
            {
                if (index == -1)
                    return false;
            }
            return true;
        }
    }
}
