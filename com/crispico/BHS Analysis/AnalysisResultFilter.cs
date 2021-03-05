using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis
{
    public class AnalysisResultFilter
    {
        public const string STATION_TIME_TYPE_ARRIVING = "IN";
        public const string STATION_TIME_TYPE_LEAVING = "OUT";

        public const string DEPARTING_TECHNICAL_FLOW_TYPE = "Dep0";
        public const string TRANSFERRING_TECHNICAL_FLOW_TYPE = "Transf0";
        public const string ORIGINATING_TECHNICAL_FLOW_TYPE = "Orig0";
        public const string TERMINATING_TECHNICAL_FLOW_TYPE = "Term0";

        public const string ALL_FLOW_TYPE_VISUAL_NAME = "All";
        public const string DEPARTING_FLOW_TYPE_VISUAL_NAME = "Departing";
        public const string ORIGINATING_FLOW_TYPE_VISUAL_NAME = "Originating";
        public const string TRANSFERRING_FLOW_TYPE_VISUAL_NAME = "Transferring";
        public const string ARRIVING_FLOW_TYPE_VISUAL_NAME = "Arriving";
        public const string TERMINATING_FLOW_TYPE_VISUAL_NAME = "Terminating";
                
        public static readonly List<string> TRANSFERRING_STATION_CODE_LIST = new List<string>(new string[] { OverallTools.BagTraceAnalysis.FROMTO_ENTRY_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_QUEUE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_TRANSF_QUEUE_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_COLLECTOR_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_TRANSF_COLLECTOR_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_TRANSF_INFEED_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_DEP_READER_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_EBS_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_LAST_CHUTE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_PRESORTATION_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_MAKEUP_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_EXIT_STATION_CODE});

        public static readonly List<string> ORIGINATING_STATION_CODE_LIST = new List<string>(new string[] { OverallTools.BagTraceAnalysis.FROMTO_ENTRY_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_QUEUE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_CI_QUEUE_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_COLLECTOR_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_CI_COLLECTOR_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_CHECKIN_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_DEP_READER_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_EBS_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_LAST_CHUTE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_PRESORTATION_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_MAKEUP_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_EXIT_STATION_CODE});

        public readonly List<string> TERMINATING_STATION_CODE_LIST = new List<string>(new string[] { });

        public readonly List<string> ARRIVING_STATION_CODE_LIST = new List<string>(new string[] { });   //Arriving = Intersection of Terminating + Transferring (so we don't show redundant info in the tree view)

        public readonly List<string> DEPARTING_STATION_CODE_LIST = new List<string>(new string[] { OverallTools.BagTraceAnalysis.FROMTO_ENTRY_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_QUEUE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_COLLECTOR_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_CI_QUEUE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_CI_COLLECTOR_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_CHECKIN_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_TRANSF_QUEUE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_TRANSF_COLLECTOR_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_TRANSF_INFEED_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_DEP_READER_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_EBS_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_LAST_CHUTE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_PRESORTATION_STATION_CODE, 
                OverallTools.BagTraceAnalysis.FROMTO_MAKEUP_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_EXIT_STATION_CODE});     //Departing = Union: Originating + Transferring (after introducing flow types when generating only Departure we should have all the Orig+Transf station types.//*Intersection of Originating + Transferring (so we don't show redundant info in the tree view)*//

        public static readonly List<string> MULTIPLE_STATION_CODE_LIST = new List<string>(new string[] { OverallTools.BagTraceAnalysis.FROMTO_ENTRY_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_QUEUE_STATION_CODE, OverallTools.BagTraceAnalysis.FROMTO_COLLECTOR_STATION_CODE,
                OverallTools.BagTraceAnalysis.FROMTO_EXIT_STATION_CODE });

        internal string filterName { get; set; }
        internal string fromStationCode { get; set; }
        internal string toStationCode { get; set; }
        internal string fromStationTimeType { get; set; }
        internal string toStationTimeType { get; set; }
        internal bool withRecirculation { get; set; }
        internal bool withFromSegregation { get; set; }
        internal bool withToSegregation { get; set; }
        internal bool excludeEBSStorageTime { get; set; }
        internal bool generateIST { get; set; } // >> Task #15683 PAX2SIM - Result Filters - Split by Flow Type

        internal string flowType { get; set; }

        public AnalysisResultsDisplayParameters displayParameter { get; set; }
        public List<AnalysisResultFilter> fromEntryChildFilters = new List<AnalysisResultFilter>();
        public List<AnalysisResultFilter> toExitChildFilters = new List<AnalysisResultFilter>();
        public List<AnalysisResultFilter> fromEntryToExitChildFilters = new List<AnalysisResultFilter>();

        internal List<string> entryStationCodesList = new List<string>();
        internal List<string> exitStationCodesList = new List<string>();

        public AnalysisResultFilter(string _name, string _fromStationCode, string _fromStationTimeType, string _toStationCode,
            string _toStationTimeType, bool _withRecirculation, bool _withFromSegregation, bool _withToSegregation, bool _excludeEBS,
            bool _generateIST)
        {
            filterName = _name;
            fromStationCode = _fromStationCode;
            fromStationTimeType = _fromStationTimeType;
            toStationCode = _toStationCode;
            toStationTimeType = _toStationTimeType;
            withRecirculation = _withRecirculation;
            withFromSegregation = _withFromSegregation;
            withToSegregation = _withToSegregation;
            excludeEBSStorageTime = _excludeEBS;
            generateIST = _generateIST;
            flowType = "";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            AnalysisResultFilter arf = obj as AnalysisResultFilter;
            if ((Object)arf == null)
                return false;
            if (this.fromStationCode != arf.fromStationCode)
                return false;
            if (this.toStationCode != arf.toStationCode)
                return false;
            if (this.fromStationTimeType != arf.fromStationTimeType)
                return false;
            if (this.toStationTimeType != arf.toStationTimeType)
                return false;
            if (this.withFromSegregation != arf.withFromSegregation)
                return false;
            if (this.withToSegregation != arf.withToSegregation)
                return false;
            if (this.withRecirculation != arf.withRecirculation)
                return false;
            if (this.excludeEBSStorageTime != arf.excludeEBSStorageTime)
                return false;
            if (this.generateIST != arf.generateIST)
                return false;
            if (this.flowType != arf.flowType)
                return false;
            return true;            
        }

        public override int GetHashCode()
        {
            int hash = 11;
            hash = (hash * 7) + fromStationCode.GetHashCode();
            hash = (hash * 7) + toStationCode.GetHashCode();
            hash = (hash * 7) + fromStationTimeType.GetHashCode();
            hash = (hash * 7) + toStationTimeType.GetHashCode();
            hash = (hash * 7) + withFromSegregation.GetHashCode();
            hash = (hash * 7) + withToSegregation.GetHashCode();
            hash = (hash * 7) + withRecirculation.GetHashCode();
            hash = (hash * 7) + excludeEBSStorageTime.GetHashCode();
            hash = (hash * 7) + generateIST.GetHashCode();
            hash = (hash * 7) + flowType.GetHashCode();
            return hash;
        }

        public AnalysisResultFilter clone()
        {
            AnalysisResultFilter clone = new AnalysisResultFilter(this.filterName, this.fromStationCode, this.fromStationTimeType, this.toStationCode, 
                this.toStationTimeType, this.withRecirculation, this.withFromSegregation, this.withToSegregation, this.excludeEBSStorageTime, this.generateIST);
            return clone;
        }

        public void copyConfigurationFromGivenFilter(AnalysisResultFilter givenFilter)
        {
            this.fromStationCode = givenFilter.fromStationCode;
            this.fromStationTimeType = givenFilter.fromStationTimeType;
            this.toStationCode = givenFilter.toStationCode;
            this.toStationTimeType = givenFilter.toStationTimeType;
            this.withRecirculation = givenFilter.withRecirculation;
            this.withFromSegregation = givenFilter.withFromSegregation;
            this.withToSegregation = givenFilter.withToSegregation;
            this.excludeEBSStorageTime = givenFilter.excludeEBSStorageTime;
            this.generateIST = givenFilter.generateIST;
        }

        public bool hasSameConfigurationAsOneFromList(List<AnalysisResultFilter> givenResultsFilters)
        {
            if (givenResultsFilters == null)
                return false;
            foreach (AnalysisResultFilter resFil in givenResultsFilters)
            {
                if (this.Equals(resFil))
                    return true;
            }
            return false;
        }

        public bool hasSameConfigurationAndNameAsOneFromList(List<AnalysisResultFilter> givenResultsFilters)
        {
            if (givenResultsFilters == null)
                return false;
            foreach (AnalysisResultFilter resFil in givenResultsFilters)
            {
                if (this.filterName == resFil.filterName && this.Equals(resFil))
                    return true;
            }
            return false;
        }

        public void removeFromGivenListByNameAndConfiguration(List<AnalysisResultFilter> givenResultsFilters)
        {
            if (givenResultsFilters == null)
                return;
            int searchedIndex = -1;
            for (int i = 0; i < givenResultsFilters.Count; i++)
            {
                AnalysisResultFilter resFil = givenResultsFilters[i];
                if (this.filterName == resFil.filterName && this.Equals(resFil))
                {
                    searchedIndex = i;
                    break;
                }
            }
            if (searchedIndex != -1)
                givenResultsFilters.RemoveAt(searchedIndex);
        }

        public bool isCustomFilter()
        {
            return filterName != null && filterName.Length > 0;
        }

        public bool belongsToTechnicalFlowType(string flowType)
        {
            if (flowType == null)
                return false;
            if ((flowType == ORIGINATING_TECHNICAL_FLOW_TYPE && ORIGINATING_STATION_CODE_LIST.Contains(fromStationCode) && ORIGINATING_STATION_CODE_LIST.Contains(toStationCode))
                || (flowType == TRANSFERRING_TECHNICAL_FLOW_TYPE && TRANSFERRING_STATION_CODE_LIST.Contains(fromStationCode) && TRANSFERRING_STATION_CODE_LIST.Contains(toStationCode))
                || (flowType == DEPARTING_TECHNICAL_FLOW_TYPE && DEPARTING_STATION_CODE_LIST.Contains(fromStationCode) && DEPARTING_STATION_CODE_LIST.Contains(toStationCode)))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "Filter: " + flowType + " " + filterName + ":"
                + " From: " + fromStationCode + " " + fromStationTimeType
                + " To: " + toStationCode + " " + toStationTimeType
                + " withRecirc: " + withRecirculation.ToString() + " withFromSegr: " + withFromSegregation.ToString() + " withToSegr: " + withToSegregation.ToString()
                + " excludeEBS: " + excludeEBSStorageTime.ToString() + " generateIST: " + generateIST.ToString();
        }
    }
}
