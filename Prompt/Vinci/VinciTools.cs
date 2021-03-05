using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIMCORE_TOOL.Classes;
using System.IO;

namespace SIMCORE_TOOL.Prompt.Vinci
{
    class VinciTools
    {
        #region Departure/Arrival Flight Statistics
        internal const string ARRIVAL_STATS_TABLE_NAME = "Arrival_Statistics";
        internal const string DEPARTURE_STATS_TABLE_NAME = "Departure_Statistics";

        internal const string CI_PROCESSED_PAX_DEPARTURE_STATS_COLUMN_NAME = "CI Processed Pax";
        internal const string CI_MISSED_PAX_DEPARTURE_STATS_COLUMN_NAME = "CI Missed Pax";
        internal const string CI_STOPPED_PAX_DEPARTURE_STATS_COLUMN_NAME = "CI Stopped Pax";
        internal const string BG_PROCESSED_PAX_DEPARTURE_STATS_COLUMN_NAME = "BG Processed Pax";
        internal const string BG_MISSED_PAX_DEPARTURE_STATS_COLUMN_NAME = "BG Missed Pax";
        internal const string BG_STOPPED_PAX_DEPARTURE_STATS_COLUMN_NAME = "BG Stopped Pax";
        internal const string SUM_OF_MISSED_STATS_COLUMN_NAME = "Sum of Missed";
        internal const string PERC_OF_MISSED_STATS_COLUMN_NAME = "% of Missed";
        internal const string SUM_OF_STOPPED_STATS_COLUMN_NAME = "Sum of Stopped";
        internal const string PERC_OF_STOPPED_STATS_COLUMN_NAME = "% of Stopped";
        internal const string IST_TO_BG_MIN_VALUE_DEPARTURE_STATS_COLUMN_NAME = "IST to Boarding Gate Min Value (min)";
        internal const string IST_TO_BG_AVG_VALUE_DEPARTURE_STATS_COLUMN_NAME = "IST to Boarding Gate Avg Value (min)";
        internal const string IST_TO_BG_MAX_VALUE_DEPARTURE_STATS_COLUMN_NAME = "IST to Boarding Gate Max Value (min)";
        internal const string PERC_VALUE_STATS_COLUMN_NAME_SUFFIX = " % Percentile (min)";

        internal const string ARR_GATE_PROCESSED_PAX_STATS_COLUMN_NAME = "Arrival Gate Processed Pax";
        internal const string ARR_GATE_MISSED_PAX_STATS_COLUMN_NAME = "Arrival Gate Missed Pax";
        internal const string ARR_GATE_STOPPED_PAX_STATS_COLUMN_NAME = "Arrival Gate Stopped Pax";
        internal const string BAG_CLAIM_PROCESSED_PAX_STATS_COLUMN_NAME = "Bagage Claim Processed Pax";
        internal const string BAG_CLAIM_MISSED_PAX_STATS_COLUMN_NAME = "Bagage Claim Missed Pax";
        internal const string BAG_CLAIM_STOPPED_PAX_STATS_COLUMN_NAME = "Bagage Claim Stopped Pax";
        internal const string IST_TO_BAG_CLAIM_MIN_VALUE_STATS_COLUMN_NAME = "IST to Bagage Claim Min Value (min)";
        internal const string IST_TO_BAG_CLAIM_AVG_VALUE_STATS_COLUMN_NAME = "IST to Bagage Claim Avg Value (min)";
        internal const string IST_TO_BAG_CLAIM_MAX_VALUE_STATS_COLUMN_NAME = "IST to Bagage Claim Max Value (min)";
                
        public static Dictionary<int, FlightPlanPax> getDepartureFlightPlanPaxDictionary(DataTable fpdPax)
        {
            int flightIdColumnIndex = fpdPax.Columns.IndexOf(FlightPlanPax.FLIGHT_ID_PAX_COLUMN_NAME);
            int expectedPaxColumnIndex = fpdPax.Columns.IndexOf(FlightPlanPax.EXPECTED_PAX_PAX_COLUMN_NAME);
            int generatedPaxColumnIndex = fpdPax.Columns.IndexOf(FlightPlanPax.GENERATED_PAX_PAX_COLUMN_NAME);
            int transferringPaxColumnIndex = fpdPax.Columns.IndexOf(FlightPlanPax.TRANSFERRING_PAX_PAX_COLUMN_NAME);

            int terminatingPaxColumnIndex = fpdPax.Columns.IndexOf(FlightPlanPax.TERMINATING_PAX_PAX_COLUMN_NAME);
            int originatingPaxColumnIndex = fpdPax.Columns.IndexOf(FlightPlanPax.ORIGINATING_PAX_PAX_COLUMN_NAME);
            int selfCIPaxColumnIndex = fpdPax.Columns.IndexOf(FlightPlanPax.SELF_CI_PAX_FPD_PAX_COLUMN_NAME);

            Dictionary<int, FlightPlanPax> flightPlanPaxDictionary = new Dictionary<int, FlightPlanPax>();
            
            if (flightIdColumnIndex != -1 && expectedPaxColumnIndex != -1
                && generatedPaxColumnIndex != -1 && transferringPaxColumnIndex != -1)
            {
                foreach (DataRow row in fpdPax.Rows)
                {
                    FlightPlanPax flightPaxPlan = new FlightPlanPax();
                    int flightId = -1;
                    double expectedPax = -1;
                    double generatedPax = -1;
                    double transferringPax = -1;

                    if (row[flightIdColumnIndex] != null 
                        && Int32.TryParse(row[flightIdColumnIndex].ToString(), out flightId))
                    {
                        flightPaxPlan.flightId = flightId;
                    }
                    if (row[expectedPaxColumnIndex] != null 
                        && Double.TryParse(row[expectedPaxColumnIndex].ToString(), out expectedPax))
                    {
                        flightPaxPlan.expectedPax = expectedPax;
                    }
                    if (row[generatedPaxColumnIndex] != null 
                        && Double.TryParse(row[generatedPaxColumnIndex].ToString(), out generatedPax))
                    {
                        flightPaxPlan.generatedPax = generatedPax;
                    }
                    if (row[transferringPaxColumnIndex] != null 
                        && Double.TryParse(row[transferringPaxColumnIndex].ToString(), out transferringPax))
                    {
                        flightPaxPlan.transferringPax = transferringPax;
                    }
                    if (terminatingPaxColumnIndex != -1)
                    {
                        double terminatingPax = -1;
                        if (row[terminatingPaxColumnIndex] != null
                        && Double.TryParse(row[terminatingPaxColumnIndex].ToString(), out terminatingPax))
                        {
                            flightPaxPlan.terminatingPax = terminatingPax;
                        }
                    }
                    if (originatingPaxColumnIndex != -1 && selfCIPaxColumnIndex != -1)
                    {
                        double originatingPax = -1;
                        double selfCIPax = -1;
                        if (row[originatingPaxColumnIndex] != null
                        && Double.TryParse(row[originatingPaxColumnIndex].ToString(), out originatingPax))
                        {
                            flightPaxPlan.originatingPax = originatingPax;
                        }
                        if (row[selfCIPaxColumnIndex] != null
                        && Double.TryParse(row[selfCIPaxColumnIndex].ToString(), out selfCIPax))
                        {
                            flightPaxPlan.selfCheckInPax = selfCIPax;
                        }
                    }
                    if (!flightPlanPaxDictionary.ContainsKey(flightPaxPlan.flightId))
                    {
                        flightPlanPaxDictionary.Add(flightPaxPlan.flightId, flightPaxPlan);
                    }
                }
            }
            return flightPlanPaxDictionary;
        }

        public static Dictionary<int, FlightPlanPax> getArrivalFlightPlanPaxDictionary(DataTable fpaPax)
        {
            int flightIdColumnIndex = fpaPax.Columns.IndexOf(FlightPlanPax.FLIGHT_ID_PAX_COLUMN_NAME);
            int expectedPaxColumnIndex = fpaPax.Columns.IndexOf(FlightPlanPax.EXPECTED_PAX_PAX_COLUMN_NAME);
            int generatedPaxColumnIndex = fpaPax.Columns.IndexOf(FlightPlanPax.GENERATED_PAX_PAX_COLUMN_NAME);
            int transferringPaxColumnIndex = fpaPax.Columns.IndexOf(FlightPlanPax.TRANSFERRING_PAX_PAX_COLUMN_NAME);
            int terminatingPaxColumnIndex = fpaPax.Columns.IndexOf(FlightPlanPax.TERMINATING_PAX_PAX_COLUMN_NAME);            

            Dictionary<int, FlightPlanPax> flightPlanPaxDictionary = new Dictionary<int, FlightPlanPax>();

            if (flightIdColumnIndex != -1 && expectedPaxColumnIndex != -1 && terminatingPaxColumnIndex != -1
                && generatedPaxColumnIndex != -1 && transferringPaxColumnIndex != -1)
            {
                foreach (DataRow row in fpaPax.Rows)
                {
                    FlightPlanPax flightPaxPlan = new FlightPlanPax();
                    int flightId = -1;
                    double expectedPax = -1;
                    double generatedPax = -1;
                    double transferringPax = -1;
                    double terminatingPax = -1;

                    if (row[flightIdColumnIndex] != null
                        && Int32.TryParse(row[flightIdColumnIndex].ToString(), out flightId))
                    {
                        flightPaxPlan.flightId = flightId;
                    }
                    if (row[expectedPaxColumnIndex] != null
                        && Double.TryParse(row[expectedPaxColumnIndex].ToString(), out expectedPax))
                    {
                        flightPaxPlan.expectedPax = expectedPax;
                    }
                    if (row[generatedPaxColumnIndex] != null
                        && Double.TryParse(row[generatedPaxColumnIndex].ToString(), out generatedPax))
                    {
                        flightPaxPlan.generatedPax = generatedPax;
                    }
                    if (row[transferringPaxColumnIndex] != null
                        && Double.TryParse(row[transferringPaxColumnIndex].ToString(), out transferringPax))
                    {
                        flightPaxPlan.transferringPax = transferringPax;
                    }                                           
                    if (row[terminatingPaxColumnIndex] != null
                        && Double.TryParse(row[terminatingPaxColumnIndex].ToString(), out terminatingPax))
                    {
                        flightPaxPlan.terminatingPax = terminatingPax;
                    }

                    if (!flightPlanPaxDictionary.ContainsKey(flightPaxPlan.flightId))
                    {
                        flightPlanPaxDictionary.Add(flightPaxPlan.flightId, flightPaxPlan);
                    }
                }
            }
            return flightPlanPaxDictionary;
        }

        public static Dictionary<int, FlightResourcesStats> getArrivalFlightResourcesStatsDictionary(
            Dictionary<int, SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.ObjectifsComparaison> arrivalStatsDictionary,
            double[] levels)
        {
            Dictionary<int, FlightResourcesStats> flightResourcesDictionary = new Dictionary<int, FlightResourcesStats>();
            foreach (int flightId in arrivalStatsDictionary.Keys)
            {
                if (arrivalStatsDictionary[flightId].alPax == null)
                    continue;

                FlightResourcesStats flightResourcesStats = getArrivalGateAndBaggageClaimStats(arrivalStatsDictionary[flightId].alPax);

                List<double> minutesFromEntryToBGList = getMinutesFromArrivalGateToBaggageClaimListByFlightPaxList(arrivalStatsDictionary[flightId].alPax);
                getTimeStatsForArrGateToBagClaim(flightResourcesStats, minutesFromEntryToBGList, levels);

                if (!flightResourcesDictionary.ContainsKey(flightId))
                {
                    flightResourcesDictionary.Add(flightId, flightResourcesStats);
                }
            }
            return flightResourcesDictionary;
        }

        internal static FlightResourcesStats getArrivalGateAndBaggageClaimStats(List<SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct> alPax)
        {
            FlightResourcesStats flightResourcesStats = new FlightResourcesStats();

            if (alPax == null)
                return flightResourcesStats;

            int agPaxStopped = 0;
            int agPaxProcessed = 0;
            int agPaxMissed = 0;

            int bcPaxStopped = 0;
            int bcPaxProcessed = 0;
            int bcPaxMissed = 0;

            int sumOfMissed = 0;
            int sumOfStopped = 0;
            foreach (SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct prsTmp in alPax)
            {
                if (prsTmp.ArrivalGateGroup != null)
                {
                    if (prsTmp.Stopped && prsTmp.isArrivalGateTheLastGroupOrStationEntered)
                        agPaxStopped++;
                    else if (prsTmp.HadMissed && prsTmp.isArrivalGateTheLastGroupOrStationEntered)
                        agPaxMissed++;
                    else
                        agPaxProcessed++;
                }
                if (prsTmp.BaggageClaimGroup != null)
                {
                    if (prsTmp.Stopped && prsTmp.isBaggageClaimTheLastGroupOrStationEntered)
                        bcPaxStopped++;
                    else if (prsTmp.HadMissed && prsTmp.isBaggageClaimTheLastGroupOrStationEntered)
                        bcPaxMissed++;
                    else
                        bcPaxProcessed++;
                }
            }
            flightResourcesStats.nbPaxProcessedAtArrivalGate = agPaxProcessed;
            flightResourcesStats.nbPaxStoppedAtArrivalGate = agPaxStopped;
            flightResourcesStats.nbPaxMissedAtArrivalGate = agPaxMissed;

            flightResourcesStats.nbPaxProcessedAtReclaim = bcPaxProcessed;
            flightResourcesStats.nbPaxMissedAtReclaim = bcPaxMissed;
            flightResourcesStats.nbPaxStoppedAtReclaim = bcPaxStopped;

            flightResourcesStats.sumOfMissed = agPaxMissed + bcPaxMissed;
            flightResourcesStats.sumOfStopped = agPaxStopped + bcPaxStopped;

            return flightResourcesStats;
        }

        internal static List<double> getMinutesFromArrivalGateToBaggageClaimListByFlightPaxList(List<SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct> alPax)
        {
            List<double> minutesFromArrGateToBagClaimList = new List<double>();

            if (alPax == null)
                return minutesFromArrGateToBagClaimList;

            foreach (SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct pax in alPax)
            {
                if (paxHasReachedBaggageClaim(pax.alPaxTravel))
                {
                    double minutes = getNbMinutesFromArrGateToBaggClaimByPaxTravel(pax.alPaxTravel);
                    minutesFromArrGateToBagClaimList.Add(minutes);
                }
            }
            return minutesFromArrGateToBagClaimList;
        }

        private static bool paxHasReachedBaggageClaim(List<SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct.paxTravelData> paxTravel)
        {
            if (paxTravel == null || paxTravel.Count == 0)
                return false;
            foreach (SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct.paxTravelData travelData in paxTravel)
            {
                if (travelData.Desk.Contains("Baggage Claim")
                    && !travelData.Desk.Contains("Group"))
                {
                    return true;
                }
            }
            return false;
        }

        private static double getNbMinutesFromArrGateToBaggClaimByPaxTravel(List<OverallTools.PaxTraceAnalysis.PaxResultsStruct.paxTravelData> list)
        {
            double minutesDifference = 0;
            if (list == null || list.Count == 0)
                return minutesDifference;
            double arrivalTimeAtArrGate = 0;
            double arrivalTimeAtBaggClaim = 0;
            for (int i = 0; i < list.Count; i++)
            {
                SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct.paxTravelData travelData = list[i];                
                if (travelData.Desk.Contains("Arrival Gate")
                    && !travelData.Desk.Contains("Group"))
                {
                    arrivalTimeAtArrGate = travelData.ArrivalTime;
                }
                if (travelData.Desk.Contains("Baggage Claim")
                    && !travelData.Desk.Contains("Group"))
                {
                    arrivalTimeAtBaggClaim = travelData.ArrivalTime;
                }

            }
            minutesDifference = arrivalTimeAtBaggClaim - arrivalTimeAtArrGate;
            return minutesDifference;
        }

        private static void getTimeStatsForArrGateToBagClaim(FlightResourcesStats flightResourcesStats,
            List<double> minutesFromArrGateToBagClaimList, double[] levels)
        {
            if (flightResourcesStats == null || minutesFromArrGateToBagClaimList == null
                || minutesFromArrGateToBagClaimList.Count == 0 || levels == null || levels.Length != 3)
            {
                return;
            }

            double min = double.MaxValue;
            double avg = 0;
            double max = double.MinValue;

            double sum = 0;
            for (int i = 0; i < minutesFromArrGateToBagClaimList.Count; i++)
            {
                sum += (double)minutesFromArrGateToBagClaimList[i];
                if ((double)minutesFromArrGateToBagClaimList[i] > max)
                    max = (double)minutesFromArrGateToBagClaimList[i];
                if ((double)minutesFromArrGateToBagClaimList[i] < min)
                    min = (double)minutesFromArrGateToBagClaimList[i];
            }

            if (minutesFromArrGateToBagClaimList.Count > 0)
                avg = sum / minutesFromArrGateToBagClaimList.Count;

            flightResourcesStats.minutesFromArrGateToBagClaimMinValue = min;
            flightResourcesStats.minutesFromArrGateToBagClaimAvgValue = avg;
            flightResourcesStats.minutesFromArrGateToBagClaimMaxValue = max;

            flightResourcesStats.minutesFromArrGateToBagClaimLevel1Value = getMaxTimeForListOfValuesByDistributionLevel(minutesFromArrGateToBagClaimList, levels[0]);
            flightResourcesStats.minutesFromArrGateToBagClaimLevel2Value = getMaxTimeForListOfValuesByDistributionLevel(minutesFromArrGateToBagClaimList, levels[1]);
            flightResourcesStats.minutesFromArrGateToBagClaimLevel3Value = getMaxTimeForListOfValuesByDistributionLevel(minutesFromArrGateToBagClaimList, levels[2]);

            flightResourcesStats.minutesFromArrGateToBagClaim = new List<double>();
            flightResourcesStats.minutesFromArrGateToBagClaim.AddRange(minutesFromArrGateToBagClaimList);
        }

        public static Dictionary<int, FlightResourcesStats> getDepartureFlightResourcesStatsDictionary(
            Dictionary<int, SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.ObjectifsComparaison> locResults,
            double[] levels)
        {
            Dictionary<int, FlightResourcesStats> flightResourcesDictionary = new Dictionary<int, FlightResourcesStats>();
            foreach (int flightId in locResults.Keys)
            {
                if (locResults[flightId].alPax == null)
                    continue;

                FlightResourcesStats flightResourcesStats = getCheckInAndBoardingGateStats(locResults[flightId].alPax);
                List<double> minutesFromEntryToBGList = getMinutesFromEntryToBGListByFlightPaxList(locResults[flightId].alPax);
                getTimeStatsForEntryToBG(flightResourcesStats, minutesFromEntryToBGList, levels);

                if (!flightResourcesDictionary.ContainsKey(flightId))
                {
                    flightResourcesDictionary.Add(flightId, flightResourcesStats);
                }
            }
            return flightResourcesDictionary;
        }
                
        internal static FlightResourcesStats getCheckInAndBoardingGateStats(List<SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct> alPax)
        {
            FlightResourcesStats flightResourcesStats = new FlightResourcesStats();
         
            if (alPax == null)
                return flightResourcesStats;

            int ciPaxStopped = 0;
            int ciPaxProcessed = 0;
            int ciPaxMissed = 0;

            int bgPaxStopped = 0;
            int bgPaxProcessed = 0;
            int bgPaxMissed = 0;

            int sumOfMissed = 0;
            int sumOfStopped = 0;
            foreach (SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct prsTmp in alPax)
            {   
                if ((prsTmp.CheckInGroup != null) && (!prsTmp.isSelfServiceCI))
                {
                    if ((prsTmp.Stopped) && (prsTmp.isCheckinLastTheGroupOrStationEntered))                    
                        ciPaxStopped++;                    
                    else if ((prsTmp.HadMissed) && (prsTmp.isCheckinLastTheGroupOrStationEntered))
                        ciPaxMissed++;
                    else                    
                        ciPaxProcessed++;                                       
                }
                if (prsTmp.BoardingGateGroup != null)
                {  
                    if ((prsTmp.Stopped) && (prsTmp.isBoardingGateTheLastGroupOrStationEntered))                    
                        bgPaxStopped++;                    
                    else if ((prsTmp.HadMissed) && (prsTmp.isBoardingGateTheLastGroupOrStationEntered))                    
                        bgPaxMissed++;                    
                    else                    
                        bgPaxProcessed++;
                }
            }
            flightResourcesStats.nbPaxProcessedAtCheckIn = ciPaxProcessed;
            flightResourcesStats.nbPaxStoppedAtCheckIn = ciPaxStopped;
            flightResourcesStats.nbPaxMissedAtCheckIn = ciPaxMissed;

            flightResourcesStats.nbPaxProcessedAtBoardingGate = ciPaxProcessed;
            flightResourcesStats.nbPaxMissedAtBoardingGate = bgPaxMissed;
            flightResourcesStats.nbPaxStoppedAtBoardingGate = bgPaxStopped;

            flightResourcesStats.sumOfMissed = ciPaxMissed + bgPaxMissed;
            flightResourcesStats.sumOfStopped = ciPaxStopped + bgPaxStopped;

            return flightResourcesStats;
        }

        internal static List<double> getMinutesFromEntryToBGListByFlightPaxList(List<SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct> alPax)
        {
            List<double> minutesFromEntryToBGList = new List<double>();

            if (alPax == null)
                return minutesFromEntryToBGList;

            foreach (SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct pax in alPax)
            {
                if (paxHasReachedBG(pax.alPaxTravel))
                {
                    // >> Departure_Statistics Passengers_IST Total time to BG
                    //double minutesFromEntryToBG = getNbMinutesFromEntryToBGByPaxTravel(pax.alPaxTravel);
                    //minutesFromEntryToBGList.Add(minutesFromEntryToBG);
                    minutesFromEntryToBGList.Add(pax.TotalTransferTime);
                    // << Departure_Statistics Passengers_IST Total time to BG
                }
            }

            return minutesFromEntryToBGList;
        }
        
        private static bool paxHasReachedBG(List<SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct.paxTravelData> paxTravel)
        {
            if (paxTravel == null || paxTravel.Count == 0)
                return false;
            foreach (SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct.paxTravelData travelData in paxTravel)
            {
                if (travelData.Desk.Contains("Boarding Gate")
                    && !travelData.Desk.Contains("Group"))
                {
                    return true;
                }
            }
            return false;
        }

        private static double getNbMinutesFromEntryToBGByPaxTravel(List<OverallTools.PaxTraceAnalysis.PaxResultsStruct.paxTravelData> list)
        {
            double minutesFromEntryToBG = 0;
            if (list == null || list.Count == 0)
                return minutesFromEntryToBG;
            double arrivalTimeAtEntry = 0;
            double arrivalTimeAtBG = 0;
            for (int i = 0; i < list.Count; i++)
            {
                SIMCORE_TOOL.OverallTools.PaxTraceAnalysis.PaxResultsStruct.paxTravelData travelData = list[i];
                if (i == 0)
                    arrivalTimeAtEntry = travelData.ArrivalTime;
                if (travelData.Desk.Contains("Boarding Gate")
                    && !travelData.Desk.Contains("Group"))
                {
                    arrivalTimeAtBG = travelData.ArrivalTime;
                }

            }
            minutesFromEntryToBG = arrivalTimeAtBG - arrivalTimeAtEntry;
            return minutesFromEntryToBG;
        }

        private static void getTimeStatsForEntryToBG(FlightResourcesStats flightResourcesStats, 
            List<double> minutesFromEntryToBGList, double[] levels)
        {
            if (flightResourcesStats == null || minutesFromEntryToBGList == null
                || minutesFromEntryToBGList.Count == 0 || levels == null || levels.Length != 3)
            {
                return;
            }

            double min = double.MaxValue;
            double avg = 0;
            double max = double.MinValue;

            double sum = 0;
            for (int i = 0; i < minutesFromEntryToBGList.Count; i++)
            {
                sum += (double)minutesFromEntryToBGList[i];
                if ((double)minutesFromEntryToBGList[i] > max)
                    max = (double)minutesFromEntryToBGList[i];
                if ((double)minutesFromEntryToBGList[i] < min)
                    min = (double)minutesFromEntryToBGList[i];
            }
            
            if (minutesFromEntryToBGList.Count > 0)
                avg = sum / minutesFromEntryToBGList.Count;

            flightResourcesStats.minutesFromEntryToBgMinValue = min;
            flightResourcesStats.minutesFromEntryToBgAvgValue = avg;
            flightResourcesStats.minutesFromEntryToBgMaxValue = max;

            flightResourcesStats.minutesFromEntryToBgLevel1Value = getMaxTimeForListOfValuesByDistributionLevel(minutesFromEntryToBGList, levels[0]);
            flightResourcesStats.minutesFromEntryToBgLevel2Value = getMaxTimeForListOfValuesByDistributionLevel(minutesFromEntryToBGList, levels[1]);
            flightResourcesStats.minutesFromEntryToBgLevel3Value = getMaxTimeForListOfValuesByDistributionLevel(minutesFromEntryToBGList, levels[2]);
            
            flightResourcesStats.minutesFromEntryToBg = new List<double>();
            flightResourcesStats.minutesFromEntryToBg.AddRange(minutesFromEntryToBGList);
        }

        public static void getStatsForListOfValuesByDistributionLevel(List<double> valuesList, double distributionLevel,
            out double minTimeLevel, out double avgTimeLevel, out double maxTimeLevel)
        {
            List<Double> timesTruncatedListLevel = OverallTools.ResultFunctions.getTruncatedListByPercent(valuesList, distributionLevel);
            timesTruncatedListLevel.Sort();

            if (timesTruncatedListLevel == null
                || timesTruncatedListLevel.Count == 0)
            {
                minTimeLevel = 0;
                avgTimeLevel = 0;
                maxTimeLevel = 0;

                return;
            }

            minTimeLevel = timesTruncatedListLevel[0];
            maxTimeLevel = timesTruncatedListLevel[timesTruncatedListLevel.Count - 1];

            double sum = 0;
            for (int i = 0; i < timesTruncatedListLevel.Count; i++)
            {
                sum += (double)timesTruncatedListLevel[i];
            }
            avgTimeLevel = 0;
            if (timesTruncatedListLevel.Count > 0)
                avgTimeLevel = sum / timesTruncatedListLevel.Count;
        }

        public static double getMaxTimeForListOfValuesByDistributionLevel(List<double> valuesList, double distributionLevel)            
        {
            double maxTimeLevel = 0;
            List<Double> timesTruncatedListLevel = OverallTools.ResultFunctions.getTruncatedListByPercent(valuesList, distributionLevel);            
            if (timesTruncatedListLevel == null
                || timesTruncatedListLevel.Count == 0)
            {
                return maxTimeLevel;
            }
            timesTruncatedListLevel.Sort();
            maxTimeLevel = timesTruncatedListLevel[timesTruncatedListLevel.Count - 1];
            return maxTimeLevel;
        }
        
        #endregion

        #region Desk Plan Information
        internal const string DESK_PLAN_INFORMATION_TABLE_NAME_SUFFIX = "_DeskPlanInformation";
        #endregion

        #region Scenario Owner Information
        internal const string SCENARIO_INFORMATION_TABLE_NAME = " Owner Information";
        internal const string AUTHOR_COLUMN_NAME = "Author Name";
        internal const string DESCRIPTION_COLUMN_NAME = "Scenario Description";
        internal const string CREATION_DATE_COLUMN_NAME = "Creation Date";
        internal const string LAST_UPDATE_DATE_COLUMN_NAME = "Last Update Date";
        internal const string LAST_MODIFICATION_P2S_VERSION_COLUMN_NAME = "Last Update PAX2SIM Version";
        internal const string PAX_TRACE_COLUMN_NAME = "Pax Trace";
        internal const string BAG_TRACE_COLUMN_NAME = "Bag Trace";

        internal static DataTable getScenarioInformationTable(ParamScenario scenarioParameters)
        {
            DataTable scenarioInformationTable = new DataTable(SCENARIO_INFORMATION_TABLE_NAME);
            if (scenarioParameters != null)
            {

                int authorColumnIndex = scenarioInformationTable.Columns.Count;
                scenarioInformationTable.Columns.Add(AUTHOR_COLUMN_NAME, typeof(String));
                int descriptionColumnIndex = scenarioInformationTable.Columns.Count;
                scenarioInformationTable.Columns.Add(DESCRIPTION_COLUMN_NAME, typeof(String));
                
                int creationDateColumnIndex = scenarioInformationTable.Columns.Count;
                scenarioInformationTable.Columns.Add(CREATION_DATE_COLUMN_NAME, typeof(String));
                int lastUpdateDateColumnIndex = scenarioInformationTable.Columns.Count;
                scenarioInformationTable.Columns.Add(LAST_UPDATE_DATE_COLUMN_NAME, typeof(String));
                
                int lastModificationP2SVersionColumnIndex = scenarioInformationTable.Columns.Count;
                scenarioInformationTable.Columns.Add(LAST_MODIFICATION_P2S_VERSION_COLUMN_NAME, typeof(Double));

                int bagTraceColumnIndex = scenarioInformationTable.Columns.Count;
                scenarioInformationTable.Columns.Add(BAG_TRACE_COLUMN_NAME, typeof(String));
                
                DataRow row = scenarioInformationTable.NewRow();
                row[authorColumnIndex] = scenarioParameters.authorName;
                row[descriptionColumnIndex] = scenarioParameters.scenarioDescription;
                row[creationDateColumnIndex] = scenarioParameters.creationDate.ToString("dd/MM/yyyy HH:mm:ss");
                row[lastUpdateDateColumnIndex] = scenarioParameters.lastUpdateDate.ToString("dd/MM/yyyy HH:mm:ss");
                row[lastModificationP2SVersionColumnIndex] = scenarioParameters.lastModificationP2SVersion;

                if (File.Exists(scenarioParameters.bagTracePath))
                {
                    string name = scenarioParameters.bagTracePath.Substring(scenarioParameters.bagTracePath.LastIndexOf("\\") + 1);
                    DateTime creation = File.GetCreationTime(scenarioParameters.bagTracePath);
                    row[bagTraceColumnIndex] = name + "_" + creation.ToString("dd/MM/yyyy HH:mm:ss");
                }

                scenarioInformationTable.Rows.Add(row);
                scenarioInformationTable.AcceptChanges();
            }
            return scenarioInformationTable;
        }
        #endregion
    }
}
