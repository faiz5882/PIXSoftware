using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nevron.GraphicsCore;

namespace SIMCORE_TOOL.com.crispico.generalUse
{
    class GraphicFilterTools
    {
        // move to OverallTools ?
        public static T getEnumerationValueByName<T>(Array enumValues, T defaultValue, string name)
        {
            T result = defaultValue;
            if (enumValues == null || name == null)
            {
                return result;
            }
            Dictionary<string, T> enumValuesDictionary = new Dictionary<string, T>();
            foreach (T value in enumValues)
            {
                enumValuesDictionary.Add(value.ToString(), value);
            }
            if (enumValuesDictionary.ContainsKey(name))
            {
                result = enumValuesDictionary[name];
            }
            return result;
        }

    }
}
