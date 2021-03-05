using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class AircraftType
    {        
        public const string AIRCTAFT_BODY_CATEGORY_A = "A";
        public const string AIRCTAFT_BODY_CATEGORY_B = "B";
        public const string AIRCTAFT_BODY_CATEGORY_C = "C";
        public const string AIRCTAFT_BODY_CATEGORY_D = "D";
        public const string AIRCTAFT_BODY_CATEGORY_E = "E";
        public const string AIRCTAFT_BODY_CATEGORY_F = "F";

        public static List<string> AIRCRAFT_BODY_CATEGORIES_LIST
            = new List<string>(new string[] { AIRCTAFT_BODY_CATEGORY_A, AIRCTAFT_BODY_CATEGORY_B, AIRCTAFT_BODY_CATEGORY_C,
                                                AIRCTAFT_BODY_CATEGORY_D, AIRCTAFT_BODY_CATEGORY_E, AIRCTAFT_BODY_CATEGORY_F});

        public static string BAG_STORING_MODE_ULD = GlobalNames.sTableContent_ULD;
        public static string BAG_STORING_MODE_LOOSE = GlobalNames.sTableContent_Loose;

        public static List<string> BAG_STORING_MODES_LIST
            = new List<string>(new string[] { BAG_STORING_MODE_ULD, BAG_STORING_MODE_LOOSE });

        internal string aircraftTypeName { get; set; }
        internal string bodyCategory { get; set; }
        internal int nbSeats { get; set; }
        internal string bagStoringMode { get; set; }
        internal string description { get; set; }
        internal string wake { get; set; }

    }
}
