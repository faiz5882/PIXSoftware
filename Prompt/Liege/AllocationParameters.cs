using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class AllocationParameters
    {
        internal string name { get; set; }
        internal string type { get; set; }

        internal DateTime start { get; set; }
        internal DateTime end { get; set; }

        internal string mainSortingType { get; set; }
        internal string secondarySortingType { get; set; }

        internal string terminal { get; set; }

        internal double timeStep { get; set; }
        internal double analysisRange { get; set; }

        internal int delayBetweenConsecutiveFlights { get; set; }

        internal bool useFpAsBasis { get; set; }
        internal bool updateFpWithResults { get; set; }

        internal double boardingEnteringSpeed { get; set; }
        internal double boardingExitingSpeed { get; set; }
        internal double lowerLimitForLargeAircraft { get; set; }

        internal bool calculateOccupation { get; set; }

        internal double maximumAcceptedDownTime { get; set; }
        internal double downTimeAfterSta { get; set; }
        internal double downTimeBeforeStd { get; set; }

        internal string fpdTableName { get; set; }

        internal string prkOctTableName { get; set; }
        internal bool prkOctUseExceptions { get; set; }

        internal string bgOctTableName { get; set; }
        internal bool bgOctUseExceptions { get; set; }

        internal string ciOctTableName { get; set; }
        internal bool ciOctUseExceptions { get; set; }

        internal string ciOctAirlineAirportExceptionTableName { get; set; }

        internal string ciShowUpTableName { get; set; }
        internal bool ciShowUpUseExceptions { get; set; }
        internal bool ciShowUpIgnore { get; set; }

        internal string aircraftTypesTableName { get; set; }
        internal bool aircraftTypesUseExceptions { get; set; }

        internal string loadingFactorsTableName { get; set; }
        internal bool loadingFactorsUseExceptions { get; set; }
        internal bool loadingFactorsDisable { get; set; }

        internal string aircraftLinksTableName { get; set; }
        internal bool aircraftLinksDisable { get; set; }

        internal string parkingPrioritiesTableName { get; set; }
        internal string boardingGatesPrioritiesTableName { get; set; }

    }
}
