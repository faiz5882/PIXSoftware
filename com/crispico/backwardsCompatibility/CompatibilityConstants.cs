using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.backwardsCompatibility
{
    class CompatibilityConstants
    {
        #region version 1.55

        #region Transfer Flight Category Distribution

        #region old definition
        public static string[] transferFlightCategoryDistributionColumnNames_1_54 = new string[] { GlobalNames.sColumnTo };
        public static Type[] transferFlightCategoryDistributionColumnDataTypes_1_54 = new Type[] { typeof(String) };
        public static int[] transferFlightCategoryDistributionPrimaryKey_1_54 = new int[] { 0 };
        #endregion

        public static string[] transferFlightCategoryDistributionLinks_1_54_1_55 = new string[] { GlobalNames.sColumnTo };

        #region new definition
        public static String[] transferFlightCategoryDistributionColumnNames_1_55 = { GlobalNames.sColumnFrom };
        public static Type[] transferFlightCategoryDistributionColumnDataTypes_1_55 = new Type[] { typeof(String) };
        public static int[] transferFlightCategoryDistributionPrimaryKey_1_55 = new int[] { 0 };
        public static Object[] transferFlightCategoryDistributionDefaultValues_1_55 = { 0 };
        #endregion

        #endregion

        #region Transfer Terminal Distribution

        #region old definition
        public static string[] transferTerminalDistributionColumnNames_1_54 = new string[] { GlobalNames.sColumnTo };
        public static Type[] transferTerminalDistributionColumnDataTypes_1_54 = new Type[] { typeof(String) };
        public static int[] transferTerminalDistributionPrimaryKey_1_54 = new int[] { 0 };
        #endregion

        public static string[] transferTerminalDistributionLinks_1_54_1_55 = new string[] { GlobalNames.sColumnTo };

        #region new definition
        public static String[] transferTerminalDistributionColumnNames_1_55 = { GlobalNames.sColumnFrom };
        public static Type[] transferTerminalDistributionColumnDataTypes_1_55 = new Type[] { typeof(String) };
        public static int[] transferTerminalDistributionPrimaryKey_1_55 = new int[] { 0 };
        public static Object[] transferTerminalDistributionDefaultValues_1_55 = { 0 };
        #endregion

        #endregion

        #endregion
    }
}
