using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using SIMCORE_TOOL.com.crispico.BHS_Analysis;

namespace SIMCORE_TOOL.Classes
{
    #region ParamScenario
    /// <summary>
    /// Classe contenant tous les paramétrages pour un scénario. Il contient l'ensemble des 
    /// noms des tables à utiliser et également des booléens et des listes des tables utilisées
    /// pour une simulation, ou pour une analyse.
    /// </summary>
    public class ParamScenario
    {
        internal const string BHS_GENERATE_LOCAL_IST_ATTRIBUTE_NAME = "BHSGenerateLocalIST";   // >> Task #13955 Pax2Sim -BHS trace loading issue
        internal const string BHS_GENERATE_GROUP_IST_ATTRIBUTE_NAME = "BHSGenerateGroupIST";   // >> Task #14280 Bag Trace Loading time too long
        internal const string BHS_GENERATE_MUP_SEGREGATION_ATTRIBUTE_NAME = "BHSGenerateMUPSegregation";   // >> Task #14280 Bag Trace Loading time too long
        
        internal const string EXPORT_AUTOMOD_MAIN_DATA_ATTRIBUTE_NAME = "ExportAutomodMainData";

        internal const string CLEAR_AUTOMOD_MAIN_DATA_ATTRIBUTE_NAME = "ClearAutomodMainData";
        internal const string CLEAR_AUTOMOD_USER_DATA_ATTRIBUTE_NAME = "ClearAutomodUserData";

        internal const double LAST_MODIFICATION_P2S_VERSION_OLDER_THAN_6_231 = -1;
        internal const double P2S_VERSION_EXPORT_AUTOMOD_MAIN_DATA = 6.231D;

        // >> Task #17690 PAX2SIM - Flight Plan Parameters table
        internal const string XML_ATTRIBUTE_NAME_FP_PARAMETERS_TABLE = "FPParametersTableXMLAttribute";
        // << Task #17690 PAX2SIM - Flight Plan Parameters table
        
        // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
        private string _authorName;
        private string _scenarioDescription;
        private DateTime _creationDate;
        private DateTime _lastModifiedDate;

        private double _lastModificationP2SVersion;

        internal string AUTHOR_NAME_XML_TAG = "AuthorName";
        internal string SCENARIO_DESCRIPTION_XML_TAG = "ScenarioDescription";
        internal string CREATION_DATE_XML_TAG = "CreationDate";
        internal string LAST_MODIFICATION_DATE_XML_TAG = "LastModificationDate";

        internal string LAST_MODIFICATION_P2S_VERSION_XML_TAG = "LastModificationP2SVersion";

        // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update
        internal string GENERATE_PAX_PLAN_FOR_STATIC_XML_TAG = "GeneratePaxPlanForStatic";

        // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17
        private string _bagTracePath;
        internal string BAG_TRACE_PATH_XML_TAG = "BagTracePathTag";
        // << Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17 

        /// <summary>
        /// Nom du scénario qui est représenté par ce \ref ParamScenario.
        /// </summary>
        private String sName;

        /// <summary>
        /// Booléen indiquant si l'analyse statique sur les vols départs doit être effectuée ou non.
        /// </summary>
        private bool bDeparture;
        /// <summary>
        /// Booléen indiquant si l'analyse statique sur les vols en arrivée doit être effectuée ou non.
        /// </summary>
        private bool bArrival;

        private bool bGeneratePaxPlanForStaticAnalysis;

        /// <summary>
        /// Booléen indiquant si le Paxplan doit être généré ou non.
        /// </summary>
        private bool bPaxplan;

        /// <summary>
        /// Booléen indiquant si la simulation passager doit être effectuée ou non.
        /// </summary>
        private bool bPaxSimulation;
        /// <summary>
        /// Booléen indiquant si la simulation bagage doit être effectuée ou non.
        /// </summary>
        private bool bBHSSimulation;
        /// <summary>
        /// Booléen indiquant si la simulation trolley doit être effectuée ou non.
        /// </summary>
        private bool bTMSSimulation;

        /// <summary>
        /// Booléen indiquant si la simulation Bagage doit utilisé un BagPlan ou non. 
        /// </summary>
        private bool bBagPlan;

        /// <summary>
        /// The generation of the desk IST for the Bag trace is optional.
        /// </summary>
        private bool _bhsGenerateLocalIST;   // >> Task #13955 Pax2Sim -BHS trace loading issue

        private bool _bhsGenerateGroupIST;  // >> Task #14280 Bag Trace Loading time too long

        private bool _bhsGenerateMUPSegregation;  // >> Task #14280 Bag Trace Loading time too long

        private bool _exportAutomodMainData;

        private bool _clearAutomodMainData;

        private bool _clearAutomodUserData;

        /// <summary>
        /// Boolean that indicates if the scenario is launched in a reclaim allocation mode. linked to \ref iTerminalReclaimAllocation
        /// </summary>
        private bool bDynamicReclaimAllocation;

        /// <summary>
        /// Entier représentant le terminal qui est sujet à l'allocation. Liè à \ref bDynamicReclaimAllocation
        /// </summary>
        private Int32 iTerminalReclaimAllocation;



        /// <summary>
        /// Booléen indiquant si l'application se trouve être utilisée comme un SIMREPORTER. Dans ce cas la,
        /// toutes les informations relatives aux passagers resteront NULL et seul \ref SimReporterInputData
        /// contiendra des informations.
        /// </summary>
        private bool bSimAnalyserSimulation;

        /// <summary>
        /// Booléen indiquant si la simulation a été lancée en mode sauvegarde de la trace ou non. Si oui, alors 
        /// </summary>
        private bool bTraceSaveMode;
        private bool bHasBeenSimulated;

        /// <summary>
        /// Liste des tables pour lesquelles il ne faut pas prendre en compte les exceptions.
        /// </summary>
        private List<String> lRejectEx;
        /// <summary>
        /// booleen qui specifie si il faut prendre en compte les tables d'exception pour 
        /// la table Make up segregation
        /// </summary>
        private bool bUseSegregationEx = true;

        #region Les différentes informations stockées dans cette classe.

        #region Informations Générales (Dates...)
        private DateTime dtDateDebut;
        private DateTime dtDateFin;
        private Double dSamplingStep;
        private Double dAnalysisRange;
        private bool bUseStatisticsStep;
        private Double dStatisticsStep;
        private String sStatisticsStepMode;
        #endregion

        #region Pax2sim Informations
        #region Departure peak
        private String sFPD;
        private String sDepartureLoadFactors;
        private String sCI_ShowUpTable;
        private String sOCT_CI_Table;
        private String sOCT_MakeUp;
        // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
        private String _ebsInputRateTableName;
        private String _ebsOutputRateTableName;
        // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
        #endregion
        #region Arrival peak
        private String sFPA;
        private String sArrivalLoadFactors;
        private String sICT_Table;
        #endregion
        #region PaxPlan
        private String sTransfTerminalDistribution;
        private String sTransfFligthCategoryDistribution;
        private bool isTransferDistributionDeterministic;

        private int iSeed;
        private int mffFileSeed;    // >> Bug #14900 MFF file not created
        private bool bUseUserProvidedSimulationEngineSeed;
        private bool bUseSeed;
        private bool bTransferArrivalGeneration;
        private bool missedDepartureTransferBasedOnCheckInShowUp;
        private bool bFillTransfer;
        private bool fillDepartureTransferBasedOnCheckInShowUp;
        private bool bGenerateAllWarmUp;
        private bool bGenerateFlightsAtEnd;
        private bool bFillQueue;
        private bool bProcessSchedule;
        private String sProcessSchedule;
        // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
        private String _stationGlobalFillingType;
        // >> Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation

        #endregion

        #region ParamAutomod

        private String sOCT_BC;
        private String sOCT_BG;
        private String _OCT_BagDropTableName;    // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
        // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
        private String sArrivalBaggageLoadingRateTableName;
        private String sArrivalBaggageLoadingDelayTableName;
        // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
        private string _reclaimBagsLimitDistributionTableName;  // >> Task #8958 Reclaim Synchronisation mode Gantt
        private String sProcessTimes;
        private String sSecurity;
        private String sTransfer;
        private String sPassport;
        // << Task #7570 new Desk and extra information for Pax -Phase I B
        private String sUserProcess;
        // >> Task #7570 new Desk and extra information for Pax -Phase I B
        private String sSaturation;
        private String sItinerary;
        private String sOneof;
        private String sOpening_CITable;
        private String sCapaQueues;
        private String sGroupQueues;
        private String sAnimatedQueues;
        // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
        private String processCapacities;
        // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
        // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        private Dictionary<String, String> _userAttributesTablesDictionary = new Dictionary<String, String>();
        // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        #endregion

        #region BHS informations
        private String sTerminal;
        private String sGeneral;
        private String sArrivalInfeedGroups;
        private String sCIGroups;
        private String sTransferInfeedGroups;

        private String sCICollectors;
        private String sCIRouting;
        private String sHBS3Routing;
        private String sTransferRouting;
        private String sFlowSplit;
        private String sProcess;
        private String sArrivalContainers;
        private String sArrivalMeanFlows;
        private String sCheckInMeanFlows;
        private String sTransferMeanFlows;

        private bool bUseMakeUpSegregation;
        #endregion

        #region BagPlan information
        private String sBagPlanScenario;
        #endregion

        
#if(PAXINOUTUTILISATION)
        private String sPaxIn;
        private String sPaxOut;
#endif
        private String sAircraftType;
        private String sAirline;



        private String sNbBags;
        private String sNbVisitors;
        private String sNbTrolley;

        private String sFlightCategories;
        private String sFlightSubcategories;    // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation

        private String sAircraftLinksTable;

        // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
        private bool bUseDefinedNbPax;
        private String sNumberOfPassengers;

        private bool bUseDefinedNbBags;
        private String sNumberOfBaggages;
        // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

        private String sUsaStandardParamTableName;        // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table

        private string _fpParametersTableName;   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
        #endregion

        #region ParamSimulation
        private double dWarmUp;
        private String sModelName;
        private bool bDisplayModel;
        private bool bRegeneratePaxplan;
        private bool bExportUserData;
        private bool bReclaimUS;
        #endregion


        private ParamUserData pudUserData;

        #region SimAnalyser Informations
        /// <summary>
        /// The name (with relative path) of the trace that should be analysed at end of simulation.
        /// </summary>
        private String sTraceName;
        /// <summary>
        /// The list of the element that can be found in the trace.
        /// </summary>
        private List<SIMCORE_TOOL.OverallTools.TraceTools.TraceContent> ltcResult;

        private int iIgnoredColumns;

        /// <summary>
        /// Parameters that contains the input tables to export.
        /// </summary>
        private ParamUserData pudInputData;
        #endregion


        #region 26/03/2012 - SGE - Parking Mulhouse
        private bool bUseExistingPRKPlan;
        private String sInitialCarStock;
        private String sShortStayTable;
        private String sLongStayTable;

        #endregion //26/03/2012 - SGE - Parking Mulhouse
        #endregion

        #region Les accesseurs aux différents éléments de la classe.        
        // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
        internal string authorName
        {
            get { return _authorName; }
            set { _authorName = value; }
        }

        internal string scenarioDescription
        {
            get { return _scenarioDescription; }
            set { _scenarioDescription = value; }
        }

        internal DateTime creationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        internal DateTime lastUpdateDate
        {
            get { return _lastModifiedDate; }
            set { _lastModifiedDate = value; }
        }
        // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

        internal double lastModificationP2SVersion
        {
            get { return _lastModificationP2SVersion; }
            set { _lastModificationP2SVersion = value; }
        }

        // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17
        internal string bagTracePath
        {
            get { return _bagTracePath; }
            set { _bagTracePath = value; }
        }
        // << Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17

        internal String Name
        {
            get
            {
                return sName;
            }
            set
            {
                sName = value;
            }
        }
        internal bool DeparturePeak
        {
            get { return bDeparture; }
            set { bDeparture = value; }
        }
        internal bool ArrivalPeak
        {
            get { return bArrival; }
            set { bArrival = value; }
        }
        internal bool generatePaxPlanForStaticAnalysis
        {
            get { return bGeneratePaxPlanForStaticAnalysis; }
            set { bGeneratePaxPlanForStaticAnalysis = value; }
        }
        
        internal bool PaxSimulation
        {
            get { return bPaxSimulation; }
            set { bPaxSimulation = value; }
        }
        internal bool BHSSimulation
        {
            get { return bBHSSimulation; }
            set { bBHSSimulation = value; }
        }

        internal bool BagPlan
        {
            get
            {
                return bBagPlan;
            }
            set
            {
                bBagPlan = value;
            }
        }
        
        internal bool bhsGenerateLocalIST   // >> Task #13955 Pax2Sim -BHS trace loading issue
        {
            get
            {
                return _bhsGenerateLocalIST;
            }
            set
            {
                _bhsGenerateLocalIST = value;
            }
        }

        internal bool bhsGenerateGroupIST   // >> Task #14280 Bag Trace Loading time too long
        {
            get
            {
                return _bhsGenerateGroupIST;
            }
            set
            {
                _bhsGenerateGroupIST = value;
            }
        }

        internal bool bhsGenerateMUPSegregation   // >> Task #14280 Bag Trace Loading time too long
        {
            get
            {
                return _bhsGenerateMUPSegregation;
            }
            set
            {
                _bhsGenerateMUPSegregation = value;
            }
        }
                
        internal bool exportAutomodMainData
        {
            get { return _exportAutomodMainData; }
            set { _exportAutomodMainData = value; }
        }

        internal bool clearAutomodMainData
        {
            get { return _clearAutomodMainData; }
            set { _clearAutomodMainData = value; }
        }

        internal bool clearAutomodUserData
        {
            get { return _clearAutomodUserData; }
            set { _clearAutomodUserData = value; }
        }

        internal bool DynamicReclaimAllocation
        {
            get
            {
                return bDynamicReclaimAllocation;
            }
            set
            {
                bDynamicReclaimAllocation = value;
            }
        }
        internal Int32 TerminalReclaimAllocation
        {
            get
            {
                return iTerminalReclaimAllocation;
            }
            set
            {
                iTerminalReclaimAllocation = value;
            }
        }

        internal bool TMSSimulation
        {
            get { return bTMSSimulation; }
            set { bTMSSimulation = value; }
        }
        internal bool PaxPlan
        {
            get { return bPaxplan; }
            set { bPaxplan = value; }
        }
        internal bool SaveTraceMode
        {
            get
            {
                return bTraceSaveMode;
            }
            set
            {
                bTraceSaveMode = value;
            }
        }
        internal bool HasBeenSimulated
        {
            get
            {
                return bHasBeenSimulated;
            }
            set
            {
                bHasBeenSimulated = value;
            }
        }
        internal bool SimAnalyserSimulation
        {
            get
            {
                return bSimAnalyserSimulation;
            }
            set
            {
                bSimAnalyserSimulation = value;
            }
        }
        /// <summary>
        /// Fonction pour tester si les exceptin doivent être prises en compte pour 
        /// une table donnée.
        /// </summary>
        /// <param name="TableName">Nom de la table</param>
        /// <returns>Booleen définissant si il faut prendre en compte les tables d'exceptions</returns>
        internal bool UseException (String TableName)
        {
            if (lRejectEx == null)
                return true;
            return !lRejectEx.Contains(TableName);
        }
        /// <summary>
        /// Fonction pour definir si il faut au non utiliser les exceptions pour une table donnée.
        /// Vraie pour toute les tables par default.
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="use"></param>
        internal void UseException (String TableName, bool use)
        {
            if (TableName == null || TableName == "")
                return;
            if (use)
            {
                if (lRejectEx == null)
                    return;
                if (lRejectEx.Contains(TableName))
                    lRejectEx.Remove(TableName);
            }
            else
            {
                if (lRejectEx == null)
                    lRejectEx = new List<String>();
                if (!lRejectEx.Contains(TableName))
                    lRejectEx.Add(TableName);
            }
        }
        /// <summary>
        /// Obtient ou definie s'il faut prendre en compte les exceptions
        /// pour la table Make up segregation.
        /// </summary>
        internal bool UseExSegregation
        {
            get { return bUseSegregationEx;  }
            set { bUseSegregationEx = value; }
        }
        internal DateTime DateDebut
        {
            get
            {
                return dtDateDebut;
            }
            set
            {
                dtDateDebut = value;
            }
        }
        internal DateTime DateFin
        {
            get
            {
                return dtDateFin;
            }
            set
            {
                dtDateFin = value;
            }
        }
        internal Double SamplingStep
        {
            get
            {
                return dSamplingStep;
            }
            set
            {
                dSamplingStep = value;
            }
        }
        internal Double AnalysisRange
        {
            get
            {
                return dAnalysisRange;
            }
            set
            {
                dAnalysisRange = value;
            }
        }
        internal bool UseStatisticsStep
        {
            get
            {
                return bUseStatisticsStep;
            }
            set
            {
                bUseStatisticsStep = value;
            }
        }
        internal Double StatisticsStep
        {
            get
            {
                return dStatisticsStep;
            }
            set
            {
                dStatisticsStep = value;
            }
        }
        internal String StatisticsStepMode
        {
            get
            {
                return sStatisticsStepMode;
            }
            set
            {
                sStatisticsStepMode = value;
            }
        }



        internal String FPD
        {
            get
            {
                return sFPD;
            }
            set
            {
                sFPD = value;
            }
        }
        internal String DepartureLoadFactors
        {
            get
            {
                return sDepartureLoadFactors;
            }
            set
            {
                sDepartureLoadFactors = value;
            }
        }
        internal String FPA
        {
            get
            {
                return sFPA;
            }
            set
            {
                sFPA = value;
            }
        }
        internal String ArrivalLoadFactors
        {
            get
            {
                return sArrivalLoadFactors;
            }
            set
            {
                sArrivalLoadFactors = value;
            }
        }

        internal String AircraftType
        {
            get
            {
                return sAircraftType;
            }
            set
            {
                sAircraftType = value;
            }
        }


#if(PAXINOUTUTILISATION)
        internal String PaxIn
        {
            get
            {
                return sPaxIn;
            }
            set
            {
                sPaxIn = value;
            }
        }
        internal String PaxOut
        {
            get
            {
                return sPaxOut;
            }
            set {
                sPaxOut = value;
            }
        }
#endif
        internal String Airline
        {
            get
            {
                return sAirline;
            }
            set
            {
                sAirline = value;
            }
        }
        internal String OCT_CI_Table
        {
            get
            {
                return sOCT_CI_Table;
            }
            set
            {
                sOCT_CI_Table = value;
            }
        }
        internal String ICT_Table
        {
            get
            {
                return sICT_Table;
            }
            set
            {
                sICT_Table = value;
            }
        }
        internal String OCT_MakeUp
        {
            get
            {
                return sOCT_MakeUp;
            }
            set
            {
                sOCT_MakeUp = value;
            }
        }
        internal String CI_ShowUpTable
        {
            get
            {
                return sCI_ShowUpTable;
            }
            set
            {
                sCI_ShowUpTable = value;
            }
        }
        internal String NbBags
        {
            get
            {
                return sNbBags;
            }
            set
            {
                sNbBags = value;
            }
        }
        internal String NbVisitors
        {
            get
            {
                return sNbVisitors;
            }
            set
            {
                sNbVisitors = value;
            }
        }
        internal String NbTrolley
        {
            get
            {
                return sNbTrolley;
            }
            set
            {
                sNbTrolley = value;
            }
        }
        internal String FlightCategories
        {
            get
            {
                return sFlightCategories;
            }
            set
            {
                sFlightCategories = value;
            }
        }

        // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
        internal String FlightSubcategories
        {
            get { return sFlightSubcategories; }
            set { sFlightSubcategories = value; }
        }
        // >> Task #9504 Pax2Sim - flight subCategories - peak flow segregation

        internal String AircraftLinksTable
        {
            get
            {
                return sAircraftLinksTable;
            }
            set
            {
                sAircraftLinksTable = value;
            }
        }

        // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
        internal bool UseDefinedNbPax
        {
            get { return bUseDefinedNbPax; }
            set { bUseDefinedNbPax = value; }
        }

        internal String NumberOfPassengers
        {
            get { return sNumberOfPassengers; }
            set { sNumberOfPassengers = value; }
        }

        internal bool UseDefinedNbBags
        {
            get { return bUseDefinedNbBags; }
            set { bUseDefinedNbBags = value; }
        }

        internal String NumberOfBaggages
        {
            get { return sNumberOfBaggages; }
            set { sNumberOfBaggages = value; }
        }
        // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

        // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
        internal String UsaStandardParamTableName
        {
            get { return sUsaStandardParamTableName; }
            set { sUsaStandardParamTableName = value; }
        }
        // << Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table

        internal string FPParametersTableName// >> Task #17690 PAX2SIM - Flight Plan Parameters table
        {
            get
            {
                return _fpParametersTableName;
            }
            set
            {
                _fpParametersTableName = value;
            }
        }

        internal String TransfTerminalDistribution
        {
            get
            {
                return sTransfTerminalDistribution;
            }
            set
            {
                sTransfTerminalDistribution = value;
            }
        }

        internal String TransfFligthCategoryDistribution
        {
            get
            {
                return sTransfFligthCategoryDistribution;
            }
            set
            {
                sTransfFligthCategoryDistribution = value;
            }
        }
        internal bool IsTransferDistributionDeterministic
        {
            get
            {
                return isTransferDistributionDeterministic;
            }
            set
            {
                isTransferDistributionDeterministic = value;
            }
        }
        internal int Seed
        {
            get
            {
                return iSeed;
            }
            set
            {
                iSeed = value;
            }
        }
        internal bool useUserProvidedSimulationEngineSeed
        {
            get { return bUseUserProvidedSimulationEngineSeed; }
            set { bUseUserProvidedSimulationEngineSeed = value; }
        }
        internal int simulationEngineSeed    // >> Bug #14900 MFF file not created
        {
            get
            {
                return mffFileSeed;
            }
            set
            {
                mffFileSeed = value;
            }
        }
        internal bool UseSeed
        {
            get
            {
                return bUseSeed;
            }
            set
            {
                bUseSeed = value;
            }
        }
        internal bool TransferArrivalGeneration
        {
            get
            {
                return bTransferArrivalGeneration;
            }
            set
            {
                bTransferArrivalGeneration = value;
            }
        }
        internal bool MissedDepartureTransferBasedOnCheckInShowUp
        {
            get
            {
                return missedDepartureTransferBasedOnCheckInShowUp;
            }
            set
            {
                missedDepartureTransferBasedOnCheckInShowUp = value;
            }
        }
        internal bool FillTransfer
        {
            get
            {
                return bFillTransfer;
            }
            set
            {
                bFillTransfer = value;
            }
        }
        internal bool FillDepartureTransferBasedOnCheckInShowUp
        {
            get
            {
                return fillDepartureTransferBasedOnCheckInShowUp;
            }
            set
            {
                fillDepartureTransferBasedOnCheckInShowUp = value;
            }
        }
        internal bool GenerateAll
        {
            get
            {
                return bGenerateAllWarmUp;
            }
            set
            {
                bGenerateAllWarmUp = value;
            }
        }
        internal bool GenerateFlightsAtEnd
        {
            get
            {
                return bGenerateFlightsAtEnd;
            }
            set
            {
                bGenerateFlightsAtEnd = value;
            }
        }

        internal bool ReclaimUS
        {
            get
            {
                return bReclaimUS;
            }
            set
            {
                bReclaimUS = value;
            }
        }


        internal bool FillQueue
        {
            get
            {
                return bFillQueue;
            }
            set
            {
                bFillQueue = value;
            }
        }

        // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
        internal String StationGlobalFillingType
        {
            get { return _stationGlobalFillingType; }
            set { _stationGlobalFillingType = value; }
        }
        // >> Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation

        internal Double WarmUp
        {
            get
            {
                return dWarmUp;
            }
            set
            {
                dWarmUp = value;
            }
        }
        internal String ModelName
        {
            get
            {
                return sModelName;
            }
            set
            {
                sModelName = value;
            }
        }
        internal bool DisplayModel
        {
            get
            {
                return bDisplayModel;
            }
            set
            {
                bDisplayModel = value;
            }
        }
        internal bool RegeneratePaxplan
        {
            get
            {
                return bRegeneratePaxplan;
            }
            set
            {
                bRegeneratePaxplan = value;
            }
        }
        internal bool ExportUserData
        {
            get
            {
                return bExportUserData;
            }
            set
            {
                bExportUserData = value;
            }
        }
        internal String OCT_BC
        {
            get
            {
                return sOCT_BC;
            }
            set
            {
                sOCT_BC = value;
            }
        }
        internal String OCT_BG
        {
            get
            {
                return sOCT_BG;
            }
            set
            {
                sOCT_BG = value;
            }
        }
        
        // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
        internal String OCT_BagDropTableName
        {
            get { return _OCT_BagDropTableName; }
            set { _OCT_BagDropTableName = value; }
        }
        // >> Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
        
        // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
        internal String ebsInputRateTableName
        {
            get { return _ebsInputRateTableName; }
            set { _ebsInputRateTableName = value; }
        }

        internal String ebsOutputRateTableName
        {
            get { return _ebsOutputRateTableName; }
            set { _ebsOutputRateTableName = value; }
        }
        // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

        // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
        internal String arrivalBaggageLoadingRateTableName
        {
            get { return sArrivalBaggageLoadingRateTableName; }
            set { sArrivalBaggageLoadingRateTableName = value; }
        }

        internal String arrivalBaggageLoadingDelayTableName
        {
            get { return sArrivalBaggageLoadingDelayTableName; }
            set { sArrivalBaggageLoadingDelayTableName = value; }
        }
        // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay

        internal string reclaimBagsLimitDistributionTableName   // >> Task #8958 Reclaim Synchronisation mode Gantt
        {
            get { return _reclaimBagsLimitDistributionTableName; }
            set { _reclaimBagsLimitDistributionTableName = value; }
        }

        internal String ProcessTimes
        {
            get
            {
                return sProcessTimes;
            }
            set
            {
                sProcessTimes = value;
            }
        }
        internal String Security
        {
            get
            {
                return sSecurity;
            }
            set
            {
                sSecurity = value;
            }
        }
        internal String Transfer
        {
            get
            {
                return sTransfer;
            }
            set
            {
                sTransfer = value;
            }
        }
        internal String Passport
        {
            get
            {
                return sPassport;
            }
            set
            {
                sPassport = value;
            }
        }
        // << Task #7570 new Desk and extra information for Pax -Phase I B
        internal String UserProcess
        {
            get
            {
                return sUserProcess;
            }
            set
            {
                sUserProcess = value;
            }
        }
        // >> Task #7570 new Desk and extra information for Pax -Phase I B
        internal String Saturation
        {
            get
            {
                return sSaturation;
            }
            set
            {
                sSaturation = value;
            }
        }
        internal String Itinerary
        {
            get
            {
                return sItinerary;
            }
            set
            {
                sItinerary = value;
            }
        }

        internal bool UseProcessSchedule
        {
            get
            {
                return bProcessSchedule;
            }
            set
            {
                bProcessSchedule = value;
            }
        }
        internal String ProcessSchedule
        {
            get
            {
                return sProcessSchedule;
            }
            set
            {
                sProcessSchedule = value;
            }
        }

        internal String Oneof
        {
            get
            {
                return sOneof;
            }
            set
            {
                sOneof = value;
            }
        }
        internal String Opening_CITable
        {
            get
            {
                return sOpening_CITable;
            }
            set
            {
                sOpening_CITable = value;
            }
        }

        internal String CapaQueues
        {
            get
            {
                return sCapaQueues;
            }
            set
            {
                sCapaQueues = value;
            }
        }
        internal String AnimatedQueues
        {
            get
            {
                return sAnimatedQueues;
            }
            set
            {
                sAnimatedQueues = value;
            }
        }

        // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
        internal String ProcessCapacities
        {
            get { return processCapacities; }
            set { processCapacities = value; }
        }
        // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection

        // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        internal Dictionary<String, String> userAttributesTablesDictionary
        {
            get { return _userAttributesTablesDictionary; }
            set { _userAttributesTablesDictionary = value; }
        }
        // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab

        internal String GroupQueues
        {
            get
            {
                return sGroupQueues;
            }
            set
            {
                sGroupQueues = value;
            }
        }
        internal bool UsePaxPlan
        {
            get
            {
                return bPaxplan;
            }
            set
            {
                bPaxplan = value;
            }
        }
        internal int iTerminal
        {
            get
            {
                int iTerminal = 0;
                String sTerminal_ = Terminal.Replace("Terminal", "");
                if (!Int32.TryParse(sTerminal_, out iTerminal))
                    iTerminal = 1;
                return iTerminal;
            }
        }
        internal String Terminal
        {
            get
            {
                return sTerminal;
            }
            set
            {
                sTerminal = value;
            }
        }
        internal String General
        {
            get
            {
                return sGeneral;
            }
            set
            {
                sGeneral = value;
            }
        }

        internal String ArrivalInfeedGroups
        {
            get
            {
                return sArrivalInfeedGroups;
            }
            set
            {
                sArrivalInfeedGroups = value;
            }
        }
        internal String CIGroups
        {
            get
            {
                return sCIGroups;
            }
            set
            {
                sCIGroups = value;
            }
        }
        internal String TransferInfeedGroups
        {
            get
            {
                return sTransferInfeedGroups;
            }
            set
            {
                sTransferInfeedGroups = value;
            }
        }
        internal String CICollectors
        {
            get
            {
                return sCICollectors;
            }
            set
            {
                sCICollectors = value;
            }
        }
        internal String CIRouting
        {
            get
            {
                return sCIRouting;
            }
            set
            {
                sCIRouting = value;
            }
        }
        internal String HBS3Routing
        {
            get
            {
                return sHBS3Routing;
            }
            set
            {
                sHBS3Routing = value;
            }
        }
        internal String TransferRouting
        {
            get
            {
                return sTransferRouting;
            }
            set
            {
                sTransferRouting = value;
            }
        }
        internal String FlowSplit
        {
            get
            {
                return sFlowSplit;
            }
            set
            {
                sFlowSplit = value;
            }
        }
        internal String Process
        {
            get
            {
                return sProcess;
            }
            set
            {
                sProcess = value;
            }
        }
        internal String ArrivalContainers
        {
            get
            {
                return sArrivalContainers;
            }
            set
            {
                sArrivalContainers = value;
            }
        }
        internal String ArrivalMeanFlows
        {
            get
            {
                return sArrivalMeanFlows;
            }
            set
            {
                sArrivalMeanFlows = value;
            }
        }
        internal String CheckInMeanFlows
        {
            get
            {
                return sCheckInMeanFlows;
            }
            set
            {
                sCheckInMeanFlows = value;
            }
        }
        internal String TransferMeanFlows
        {
            get
            {
                return sTransferMeanFlows;
            }
            set
            {
                sTransferMeanFlows = value;
            }
        }

        internal bool UseMakeUpSegregation
        {
            get
            {
                return bUseMakeUpSegregation;
            }
            set
            {
                bUseMakeUpSegregation = value;
            }
        }


        #region BagPlan
        internal String BagPlanScenarioName
        {
            get
            {
                return sBagPlanScenario;
            }

            set
            {
                sBagPlanScenario = value;
            }
        }
        #endregion

        #region For the SimReporter Purpose
        internal String SimReporterTraceName
        {
            get
            {
                return sTraceName;
            }
            set
            {
                sTraceName = value;
            }
        }

        internal List<SIMCORE_TOOL.OverallTools.TraceTools.TraceContent> SimReporterTraceParameters
        {
            get
            {
                return ltcResult;
            }
            set
            {
                ltcResult = value;
            }
        }
        internal ParamUserData SimReporterInputData
        {
            get
            {
                return pudInputData;
            }
            set
            {
                pudInputData = value;
            }
        }
        internal int SimReporterIgnoredColumns
        {
            get
            {
                return iIgnoredColumns;
            }
            set
            {
                iIgnoredColumns = value;
            }
        }
        #endregion

        internal ParamUserData UserData
        {
            get
            {
                return pudUserData;
            }
            set
            {
                pudUserData = value;
            }
        }

        
        #region 26/03/2012 - SGE - Parking Mulhouse

        internal bool UseExistingPRKPlan
        {
            get
            {
                return bUseExistingPRKPlan;
            }
            set
            {
                bUseExistingPRKPlan = value;
            }
        }
        internal String InitialCarStock
        {
            get
            {
                return sInitialCarStock;
            }
            set
            {
                sInitialCarStock = value;
            }
        }
        internal String ShortStayTable
        {
            get
            {
                return sShortStayTable;
            }
            set
            {
                sShortStayTable = value;
            }
        }
        internal String LongStayTable
        {
            get
            {
                return sLongStayTable;
            }
            set
            {
                sLongStayTable = value;
            }
        }

        #endregion //26/03/2012 - SGE - Parking Mulhouse
        #endregion

        // >> Bug #13367 Liege allocation
        internal bool isLiegeAllocation;
        // << Bug #13367 Liege allocation

        internal List<AnalysisResultFilter> analysisResultsFilters = new List<AnalysisResultFilter>();   // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
        internal List<AnalysisResultFilter> analysisResultsFiltersSplittedByFlow = new List<AnalysisResultFilter>();

        internal List<string> deletedCustomAnalysisResultsFilters = new List<string>();

        internal List<string> flowTypes = new List<string>();   // >> Task #15683 PAX2SIM - Result Filters - Split by Flow Type

        internal List<double> percentilesLevels = new List<double>();  // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec

        internal bool isTraceAnalysisScenario = false;  // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues

        public void Initialize(String sName_)
        {

            sName = sName_;

            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            authorName = "";
            scenarioDescription = "";
            creationDate = DateTime.Now;
            lastUpdateDate = DateTime.Now;
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

            bagTracePath = "";

            bDeparture = false;
            bArrival = false;
            bGeneratePaxPlanForStaticAnalysis = false;

            bPaxplan = false;            

            bPaxSimulation = false;
            bBHSSimulation = false;
            bBagPlan = false;
            bDynamicReclaimAllocation = false;
            iTerminalReclaimAllocation = 0;
            bTMSSimulation = false;
            bSimAnalyserSimulation = false;

            dtDateDebut = DateTime.Now;
            dtDateFin = DateTime.Now; ;
            dSamplingStep = 5;
            dAnalysisRange = 15;
            bUseStatisticsStep = false;
            sStatisticsStepMode = "Maximum";
            dStatisticsStep = 15;

            sFPD = "";
            sDepartureLoadFactors = "";
            sCI_ShowUpTable = "";
            sOCT_CI_Table = "";
            sOCT_MakeUp = "";

            sFPA = "";
            sArrivalLoadFactors = "";
            sICT_Table = "";



            sTransfTerminalDistribution = "";
            sTransfFligthCategoryDistribution = "";

            iSeed = 0;
            mffFileSeed = 1;
            bUseSeed = false;
            bTransferArrivalGeneration = false;
            bFillTransfer = false;
            bGenerateAllWarmUp = false;
            bGenerateFlightsAtEnd = false;
            bFillQueue = false;
            _stationGlobalFillingType = "";// << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation



            dWarmUp = 0;
            sModelName = "";
            bDisplayModel = false;
            bRegeneratePaxplan = true;
            bExportUserData = true;

            ReclaimUS = false;

            sAircraftType = "";
            sAirline = "";


            sNbBags = "";
            sNbVisitors = "";
            sNbTrolley = "";

            sFlightCategories = "";
            sFlightSubcategories = "";  // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation

            sAircraftLinksTable = "";

            // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)            
            sNumberOfPassengers = "";            
            sNumberOfBaggages = "";            
            // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

            sUsaStandardParamTableName = "";    // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table

            _fpParametersTableName = "";

            sOCT_BC = "";
            sOCT_BG = "";
            _OCT_BagDropTableName = ""; // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
            // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            _ebsInputRateTableName = "";
            _ebsOutputRateTableName = "";
            // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            sArrivalBaggageLoadingRateTableName = "";
            sArrivalBaggageLoadingDelayTableName = "";
            // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            _reclaimBagsLimitDistributionTableName = "";
            sProcessTimes = "";
            sSecurity = "";
            // << Task #7570 new Desk and extra information for Pax -Phase I B            
            sUserProcess = "";
            // >> Task #7570 new Desk and extra information for Pax -Phase I B
            sTransfer = "";
            sPassport = "";
            sSaturation = "";
            sItinerary = "";
            sOneof = "";
            sOpening_CITable = "";
            sCapaQueues = "";
            sGroupQueues = "";
            // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            processCapacities = "";
            // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            sAnimatedQueues = "";
            bProcessSchedule = false;
            sProcessSchedule = "";

            sTerminal = "";
            sGeneral = "";
            sArrivalInfeedGroups = "";
            sCIGroups = "";
            sTransferInfeedGroups = "";

            sCICollectors = "";
            sCIRouting = "";
            sHBS3Routing = "";
            sTransferRouting = "";
            sFlowSplit = "";
            sProcess = "";
            sArrivalContainers = "";
            sArrivalMeanFlows = "";
            sCheckInMeanFlows = "";
            sTransferMeanFlows = "";
            bUseMakeUpSegregation = false;


            sBagPlanScenario = "";

            pudUserData = null;
            bTraceSaveMode = false;

            bHasBeenSimulated = false;

            sTraceName = "";
            ltcResult = null;
            iIgnoredColumns = 1;
            #region 26/03/2012 - SGE - Parking Mulhouse
            bUseExistingPRKPlan = false;
            sInitialCarStock = ""; ;
            sShortStayTable = ""; ;
            sLongStayTable = ""; ;

            #endregion //26/03/2012 - SGE - Parking Mulhouse

            analysisResultsFilters = new List<AnalysisResultFilter>();        
        }

        public ParamScenario(String sName_)
        {
            Initialize(sName_);
        }
        public static ParamScenario FromOldParameter(ParamAnalysis paOldAnalysis)
        {
            if (paOldAnalysis != null)
                return null;
            ParamScenario psNewScenario = new ParamScenario(paOldAnalysis.Name);
            FromOldParameter(psNewScenario, paOldAnalysis);
            return psNewScenario;
        }
        public static void FromOldParameter(ParamScenario psNewScenario, ParamAnalysis paOldAnalysis)
        {

            if (paOldAnalysis == null)
                return;
            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            psNewScenario.authorName = "";
            psNewScenario.scenarioDescription = "";
            psNewScenario.creationDate = DateTime.Now;
            psNewScenario.lastUpdateDate = DateTime.Now;
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            psNewScenario.bagTracePath = "";
            psNewScenario.dtDateDebut = paOldAnalysis.DateDebut;
            psNewScenario.dtDateFin = paOldAnalysis.DateFin;
            psNewScenario.pudUserData = paOldAnalysis.UserData;
            #region PeakDeparture
            if (paOldAnalysis.PeakDeparture != null)
            {
                psNewScenario.DeparturePeak = true;
                psNewScenario.dAnalysisRange = paOldAnalysis.PeakDeparture.AnalysisRange;
                psNewScenario.dSamplingStep = paOldAnalysis.PeakDeparture.StepAnalysis;

                psNewScenario.sFPD = paOldAnalysis.PeakDeparture.FPD;
                psNewScenario.sDepartureLoadFactors = paOldAnalysis.PeakDeparture.LoadFactors;
                psNewScenario.sCI_ShowUpTable = paOldAnalysis.PeakDeparture.CI_ShowUpTable;
                psNewScenario.sAircraftType = paOldAnalysis.PeakDeparture.AircraftType;
                psNewScenario.sFlightCategories = paOldAnalysis.PeakDeparture.FlightCategories;
                psNewScenario.sNbBags = paOldAnalysis.PeakDeparture.NbBags;
                psNewScenario.sNbVisitors = paOldAnalysis.PeakDeparture.NbVisitors;
                psNewScenario.sNbTrolley = paOldAnalysis.PeakDeparture.NbTrolley;
                psNewScenario.sOCT_CI_Table = paOldAnalysis.PeakDeparture.OCT_CI_Table;
                psNewScenario.sOCT_MakeUp = paOldAnalysis.PeakDeparture.OCT_MakeUp;
            }
            #endregion
            #region PeakArrival
            if (paOldAnalysis.PeakArrival != null)
            {
                psNewScenario.ArrivalPeak = true;
                psNewScenario.dAnalysisRange = paOldAnalysis.PeakArrival.AnalysisRange;
                psNewScenario.dSamplingStep = paOldAnalysis.PeakArrival.StepAnalysis;

                psNewScenario.sFPA = paOldAnalysis.PeakArrival.FPA;
                psNewScenario.sArrivalLoadFactors = paOldAnalysis.PeakArrival.LoadFactors;
                psNewScenario.sICT_Table = paOldAnalysis.PeakArrival.ICT_Table;
                psNewScenario.sAircraftType = paOldAnalysis.PeakArrival.AircraftType;
                psNewScenario.sFlightCategories = paOldAnalysis.PeakArrival.FlightCategories;
                psNewScenario.sNbBags = paOldAnalysis.PeakArrival.NbBags;
                psNewScenario.sNbVisitors = paOldAnalysis.PeakArrival.NbVisitors;
                psNewScenario.sNbTrolley = paOldAnalysis.PeakArrival.NbTrolley;
            }
            #endregion
            #region Simulation
            if (paOldAnalysis.Simulation != null)
            {
                psNewScenario.PaxSimulation = true;
                psNewScenario.dAnalysisRange = paOldAnalysis.Simulation.AnalysisRange;
                psNewScenario.dSamplingStep = paOldAnalysis.Simulation.AnalysisStep;

                psNewScenario.bDisplayModel = paOldAnalysis.Simulation.DisplayModel;
                psNewScenario.bExportUserData = paOldAnalysis.Simulation.ExportUserData;
                psNewScenario.sModelName = paOldAnalysis.Simulation.ModelName;
                psNewScenario.bRegeneratePaxplan = paOldAnalysis.Simulation.RegeneratePaxplan;
                psNewScenario.dWarmUp = paOldAnalysis.Simulation.WarmUp;
                psNewScenario.bReclaimUS = false;
            }
            #endregion
            #region PaxPlan
            if (paOldAnalysis.PaxPlan != null)
            {
                psNewScenario.PaxPlan = true;
                psNewScenario.sFPD = paOldAnalysis.PaxPlan.FPD;
                psNewScenario.sDepartureLoadFactors = paOldAnalysis.PaxPlan.DLoadFactors;
                psNewScenario.sCI_ShowUpTable = paOldAnalysis.PaxPlan.CI_ShowUpTable;

                psNewScenario.sFPA = paOldAnalysis.PaxPlan.FPA;
                psNewScenario.sArrivalLoadFactors = paOldAnalysis.PaxPlan.ALoadFactors;
                psNewScenario.sICT_Table = paOldAnalysis.PaxPlan.ICT_Table;
                psNewScenario.sAircraftType = paOldAnalysis.PaxPlan.AircraftType;
                psNewScenario.sFlightCategories = paOldAnalysis.PaxPlan.FlightCategories;
                psNewScenario.sNbBags = paOldAnalysis.PaxPlan.NbBags;
                psNewScenario.sNbVisitors = paOldAnalysis.PaxPlan.NbVisitors;
                psNewScenario.sNbTrolley = paOldAnalysis.PaxPlan.NbTrolley;

                psNewScenario.bFillQueue = paOldAnalysis.PaxPlan.FillQueue;
                psNewScenario.bFillTransfer = paOldAnalysis.PaxPlan.FillTransfer;
                psNewScenario.bGenerateAllWarmUp = paOldAnalysis.PaxPlan.GenerateAll;
                psNewScenario.bGenerateFlightsAtEnd = paOldAnalysis.PaxPlan.GenerateFlightsAtEnd;
                psNewScenario.iSeed = paOldAnalysis.PaxPlan.Seed;
                psNewScenario.bTransferArrivalGeneration = paOldAnalysis.PaxPlan.TransferArrivalGeneration;
                psNewScenario.sTransfTerminalDistribution = paOldAnalysis.PaxPlan.TransfTerminalDistribution;
                psNewScenario.isTransferDistributionDeterministic = false;
                psNewScenario.sTransfFligthCategoryDistribution = "";

                psNewScenario.bUseSeed = paOldAnalysis.PaxPlan.UseSeed;
            }
            #endregion
            #region Automod
            if (paOldAnalysis.Automod != null)
            {
                psNewScenario.sOCT_CI_Table = paOldAnalysis.Automod.OCT_CI_Table;
                psNewScenario.sOCT_BC = paOldAnalysis.Automod.OCT_BC;
                psNewScenario.sOCT_BG = paOldAnalysis.Automod.OCT_BG;
                psNewScenario.sCapaQueues = paOldAnalysis.Automod.CapaQueues;
                psNewScenario.sItinerary = paOldAnalysis.Automod.Itinerary;
                psNewScenario.sOneof = paOldAnalysis.Automod.Oneof;
                psNewScenario.sOpening_CITable = paOldAnalysis.Automod.Opening_CITable;
                psNewScenario.sPassport = paOldAnalysis.Automod.Passport;
                // << Task #7570 new Desk and extra information for Pax -Phase I B
                psNewScenario.sUserProcess = paOldAnalysis.Automod.UserProcess;
                // >> Task #7570 new Desk and extra information for Pax -Phase I B
                psNewScenario.sSaturation = "";
                psNewScenario.sProcessTimes = paOldAnalysis.Automod.ProcessTimes;
                psNewScenario.sSecurity = paOldAnalysis.Automod.Security;
                psNewScenario.sTransfer = paOldAnalysis.Automod.Transfer;
            }
            #endregion
            #region BHS
            if (paOldAnalysis.BHS != null)
            {
                psNewScenario.BHSSimulation = true;
                psNewScenario.sArrivalContainers = paOldAnalysis.BHS.ArrivalContainers;
                psNewScenario.sArrivalInfeedGroups = paOldAnalysis.BHS.ArrivalInfeedGroups;
                psNewScenario.sArrivalMeanFlows = paOldAnalysis.BHS.ArrivalMeanFlows;
                psNewScenario.sCheckInMeanFlows = paOldAnalysis.BHS.CheckInMeanFlows;
                psNewScenario.sCICollectors = paOldAnalysis.BHS.CICollectors;
                psNewScenario.sCIGroups = paOldAnalysis.BHS.CIGroups;
                psNewScenario.sCIRouting = paOldAnalysis.BHS.CIRouting;
                psNewScenario.sFlowSplit = paOldAnalysis.BHS.FlowSplit;
                psNewScenario.sGeneral = paOldAnalysis.BHS.General;
                psNewScenario.sProcess = paOldAnalysis.BHS.Process;
                psNewScenario.sTerminal = paOldAnalysis.BHS.Terminal;
                psNewScenario.sTransferInfeedGroups = paOldAnalysis.BHS.TransferInfeedGroups;
                psNewScenario.sTransferMeanFlows = paOldAnalysis.BHS.TransferMeanFlows;
                psNewScenario.sTransferRouting = paOldAnalysis.BHS.TransferRouting;
                psNewScenario.bPaxplan = paOldAnalysis.BHS.UsePaxPlan;
            }
            #endregion

            #region 26/03/2012 - SGE - Parking Mulhouse
            psNewScenario.bUseExistingPRKPlan = false;
            psNewScenario.sInitialCarStock = "";
            psNewScenario.sShortStayTable = "";
            psNewScenario.sLongStayTable = "";

            #endregion //26/03/2012 - SGE - Parking Mulhouse
        }
        public const string XML_ATTRIBUTE_NAME_IS_TRANSFER_DISTRIBUTION_DETERMINISTIC = "IS_TRANSFER_DISTRIBUTION_DETERMINISTIC";
        public const string XML_ATTRIBUTE_NAME_FILL_DEPARTURE_TRANSFER_BASED_ON_CI = "FILL_DEPARTURE_TRANSFER_BASED_ON_CI";
        public const string XML_ATTRIBUTE_NAME_MISSED_DEPARTURE_TRANSFER_BASED_ON_CHECK_IN_SHOW_UP = "MISSED_DEPARTURE_TRANSFER_BASED_ON_CHECK_IN_SHOW_UP";
        public ParamScenario(XmlNode Parametres, VersionManager sVersion)
        {
            if (sVersion <= new VersionManager(1, 17) && (!sVersion.isVersion(0, 0)))
            {
                ParamAnalysis paOldAnalysis = new ParamAnalysis(Parametres, sVersion);
                FromOldParameter(this, paOldAnalysis);
                return;
            }
#if(PAXINOUTUTILISATION)
            //if (sVersion >= new VersionManager(1, 53))
            {
                ///For version greather than 1.53, we have to read the information for parameters for 
                ///PAX In & Out points.

                if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "PaxIn"))
                    sPaxIn = Parametres["PaxIn"].FirstChild.Value;
                if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "PaxOut"))
                    sPaxOut = Parametres["PaxOut"].FirstChild.Value;
            }
#endif
            if (!OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "Name"))
                return;
            String sName_ = Parametres.Attributes["Name"].Value;
            Initialize(sName_);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GlobalNames.IS_LIEGE_ALLOCATION_PARAM_SCENARIO_ATTRIBUTE_NAME))   // >> Bug #13367 Liege allocation
                Boolean.TryParse(Parametres.Attributes[GlobalNames.IS_LIEGE_ALLOCATION_PARAM_SCENARIO_ATTRIBUTE_NAME].Value, out isLiegeAllocation);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GlobalNames.IS_TRACE_ANALYSIS_SCENARIO_PARAM_SCENARIO_ATTRIBUTE_NAME))    // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues
                Boolean.TryParse(Parametres.Attributes[GlobalNames.IS_TRACE_ANALYSIS_SCENARIO_PARAM_SCENARIO_ATTRIBUTE_NAME].Value, out isTraceAnalysisScenario);
            
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GlobalNames.PaxPlanName))
                Boolean.TryParse(Parametres.Attributes[GlobalNames.PaxPlanName].Value, out bPaxplan);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "DeparturePeak"))
                Boolean.TryParse(Parametres.Attributes["DeparturePeak"].Value, out bDeparture);

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "SimAnalyser"))
                bSimAnalyserSimulation = true;

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "ArrivalPeak"))
                Boolean.TryParse(Parametres.Attributes["ArrivalPeak"].Value, out bArrival);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GENERATE_PAX_PLAN_FOR_STATIC_XML_TAG))
                Boolean.TryParse(Parametres.Attributes[GENERATE_PAX_PLAN_FOR_STATIC_XML_TAG].Value, out bGeneratePaxPlanForStaticAnalysis);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "PaxSimulation"))
                Boolean.TryParse(Parametres.Attributes["PaxSimulation"].Value, out bPaxSimulation);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "BHSSimulation"))
                Boolean.TryParse(Parametres.Attributes["BHSSimulation"].Value, out bBHSSimulation);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "TMSSimulation"))
                Boolean.TryParse(Parametres.Attributes["TMSSimulation"].Value, out bTMSSimulation);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "TraceSaveMode"))
                Boolean.TryParse(Parametres.Attributes["TraceSaveMode"].Value, out bTraceSaveMode);

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "SamplingStep"))
                Double.TryParse(Parametres["SamplingStep"].FirstChild.Value, out dSamplingStep);
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AnalysisRange"))
                Double.TryParse(Parametres["AnalysisRange"].FirstChild.Value, out dAnalysisRange);
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "UseStatisticsStep"))
            {
                bUseStatisticsStep = true;
                if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "StatisticsStep"))
                {
                    sStatisticsStepMode = OverallTools.FonctionUtiles.getValueChild(Parametres, "StatisticsStepMode");
                    Double.TryParse(Parametres["StatisticsStep"].FirstChild.Value, out dStatisticsStep);
                }
            }            

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Start"))
                DateTime.TryParse(Parametres["Start"].FirstChild.Value, out dtDateDebut);
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "End"))
                DateTime.TryParse(Parametres["End"].FirstChild.Value, out dtDateFin);

            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, AUTHOR_NAME_XML_TAG))
                _authorName = Parametres[AUTHOR_NAME_XML_TAG].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, SCENARIO_DESCRIPTION_XML_TAG))
                _scenarioDescription = Parametres[SCENARIO_DESCRIPTION_XML_TAG].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, CREATION_DATE_XML_TAG))
                DateTime.TryParse(Parametres[CREATION_DATE_XML_TAG].FirstChild.Value, out _creationDate);
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, LAST_MODIFICATION_DATE_XML_TAG))
                DateTime.TryParse(Parametres[LAST_MODIFICATION_DATE_XML_TAG].FirstChild.Value, out _lastModifiedDate);
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, LAST_MODIFICATION_P2S_VERSION_XML_TAG))
                Double.TryParse(Parametres[LAST_MODIFICATION_P2S_VERSION_XML_TAG].FirstChild.Value, out _lastModificationP2SVersion);
            else
                _lastModificationP2SVersion = LAST_MODIFICATION_P2S_VERSION_OLDER_THAN_6_231;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, BAG_TRACE_PATH_XML_TAG))
                _bagTracePath = Parametres[BAG_TRACE_PATH_XML_TAG].FirstChild.Value;
                         
                #region partie BHS
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Terminal"))
                sTerminal = Parametres["Terminal"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "General"))
                sGeneral = Parametres["General"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CIGroups"))
                sCIGroups = Parametres["CIGroups"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CICollectors"))
                sCICollectors = Parametres["CICollectors"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CIRouting"))
                sCIRouting = Parametres["CIRouting"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "HBS3Routing"))
                sHBS3Routing = Parametres["HBS3Routing"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferRouting"))
                sTransferRouting = Parametres["TransferRouting"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FlowSplit"))
                sFlowSplit = Parametres["FlowSplit"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Process"))
                sProcess = Parametres["Process"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ArrivalInfeedGroups") &&
                (Parametres["ArrivalInfeedGroups"].FirstChild != null))
                sArrivalInfeedGroups = Parametres["ArrivalInfeedGroups"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferInfeedGroups") &&
                (Parametres["TransferInfeedGroups"].FirstChild != null))
                sTransferInfeedGroups = Parametres["TransferInfeedGroups"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ArrivalMeanFlows"))
                sArrivalMeanFlows = Parametres["ArrivalMeanFlows"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CheckInMeanFlows"))
                sCheckInMeanFlows = Parametres["CheckInMeanFlows"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferMeanFlows"))
                sTransferMeanFlows = Parametres["TransferMeanFlows"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_MakeUp"))
                sOCT_MakeUp = Parametres["OCT_MakeUp"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ArrivalContainers") && (Parametres["ArrivalContainers"].FirstChild != null))
                sArrivalContainers = Parametres["ArrivalContainers"].FirstChild.Value;
            #endregion

            #region Partie Automod
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_CI"))
                sOCT_CI_Table = Parametres["OCT_CI"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_BC"))
                sOCT_BC = Parametres["OCT_BC"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_BG"))
                sOCT_BG = Parametres["OCT_BG"].FirstChild.Value;

            // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "arrivalBaggageLoadingRateTableName"))
                sArrivalBaggageLoadingRateTableName = Parametres["arrivalBaggageLoadingRateTableName"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "arrivalBaggageLoadingDelayTableName"))
                sArrivalBaggageLoadingDelayTableName = Parametres["arrivalBaggageLoadingDelayTableName"].FirstChild.Value;
            // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "reclaimBagsLimitDistributionTableName"))
                _reclaimBagsLimitDistributionTableName = Parametres["reclaimBagsLimitDistributionTableName"].FirstChild.Value;

            // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.EBS_INPUT_RATE_XML_CHILD_NODE_NAME))
                _ebsInputRateTableName = Parametres[GlobalNames.EBS_INPUT_RATE_XML_CHILD_NODE_NAME].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.EBS_OUTPUT_RATE_XML_CHILD_NODE_NAME))
                _ebsOutputRateTableName = Parametres[GlobalNames.EBS_OUTPUT_RATE_XML_CHILD_NODE_NAME].FirstChild.Value;
            // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

            // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.OCT_BAGDROP_XML_CHILD_NODE_NAME))
                _OCT_BagDropTableName = Parametres[GlobalNames.OCT_BAGDROP_XML_CHILD_NODE_NAME].FirstChild.Value;
            // >> Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ProcessTimes"))
                sProcessTimes = Parametres["ProcessTimes"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Security"))
                sSecurity = Parametres["Security"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Transfer"))
                sTransfer = Parametres["Transfer"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Passport"))
                sPassport = Parametres["Passport"].FirstChild.Value;
            // << Task #7570 new Desk and extra information for Pax -Phase I B
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "UserProcess"))
                sUserProcess = Parametres["UserProcess"].FirstChild.Value;
            // >> Task #7570 new Desk and extra information for Pax -Phase I B

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Saturation"))
                sSaturation = Parametres["Saturation"].FirstChild.Value;
            
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Itinerary"))
                sItinerary = Parametres["Itinerary"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Oneof"))
                sOneof = Parametres["Oneof"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CapaQueues"))
                sCapaQueues = Parametres["CapaQueues"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "GroupQueues"))
                sGroupQueues = Parametres["GroupQueues"].FirstChild.Value;
            // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.PROCESS_CAPACITIES_SCENARIO_PARAM_NAME))
                processCapacities = Parametres[GlobalNames.PROCESS_CAPACITIES_SCENARIO_PARAM_NAME].FirstChild.Value;
            // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection

            sProcessSchedule = OverallTools.FonctionUtiles.getValueChild(Parametres, "ProcessSchedule");
            if (sProcessSchedule != "")
                bProcessSchedule = true;

            sAnimatedQueues = OverallTools.FonctionUtiles.getValueChild(Parametres, "AnimatedQueues");


            sOpening_CITable = OverallTools.FonctionUtiles.getValueChild(Parametres, "Opening_CI");
            sAirline = OverallTools.FonctionUtiles.getValueChild(Parametres, "Airline");


            #endregion

            #region PaxPlan

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FPA"))
                sFPA = Parametres["FPA"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FPD"))
                sFPD = Parametres["FPD"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AircraftType"))
                sAircraftType = Parametres["AircraftType"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FlightCategories"))
                sFlightCategories = Parametres["FlightCategories"].FirstChild.Value;
            // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation            
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.flightSubcategories_XML_childNodeName))
                sFlightSubcategories = Parametres[GlobalNames.flightSubcategories_XML_childNodeName].FirstChild.Value;
            // >> Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ALoadFactors"))
                sArrivalLoadFactors = Parametres["ALoadFactors"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "DLoadFactors"))
                sDepartureLoadFactors = Parametres["DLoadFactors"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ICT"))
                sICT_Table = Parametres["ICT"].FirstChild.Value;



            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CI_ShowUp"))
                sCI_ShowUpTable = Parametres["CI_ShowUp"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbBags"))
                sNbBags = Parametres["NbBags"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbTrolley"))
                sNbTrolley = Parametres["NbTrolley"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbVisitors"))
                sNbVisitors = Parametres["NbVisitors"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AircraftLinksTable"))
                sAircraftLinksTable = Parametres["AircraftLinksTable"].FirstChild.Value;

            // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.NB_OF_PAX_XML_ATTRIBUTE_NAME))
                sNumberOfPassengers = Parametres[GlobalNames.NB_OF_PAX_XML_ATTRIBUTE_NAME].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.NB_OF_BAGS_XML_ATTRIBUTE_NAME))
                sNumberOfBaggages = Parametres[GlobalNames.NB_OF_BAGS_XML_ATTRIBUTE_NAME].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GlobalNames.USE_DEFINED_NB_PAX_XML_ATTRIBUTE_NAME))
                Boolean.TryParse(Parametres.Attributes[GlobalNames.USE_DEFINED_NB_PAX_XML_ATTRIBUTE_NAME].Value, out bUseDefinedNbPax);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GlobalNames.USE_DEFINED_NB_BAGS_XML_ATTRIBUTE_NAME))
                Boolean.TryParse(Parametres.Attributes[GlobalNames.USE_DEFINED_NB_BAGS_XML_ATTRIBUTE_NAME].Value, out bUseDefinedNbBags);
            // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, XML_ATTRIBUTE_NAME_FP_PARAMETERS_TABLE))
                _fpParametersTableName = Parametres[XML_ATTRIBUTE_NAME_FP_PARAMETERS_TABLE].FirstChild.Value;

            // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.USA_STANDARD_PARAMS_XML_ATTRIBUTE_NAME))
                sUsaStandardParamTableName = Parametres[GlobalNames.USA_STANDARD_PARAMS_XML_ATTRIBUTE_NAME].FirstChild.Value;
            // << Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferTerminalDistribution"))
                sTransfTerminalDistribution = Parametres["TransferTerminalDistribution"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferFligthCategoryDistribution"))
                sTransfFligthCategoryDistribution = Parametres["TransferFligthCategoryDistribution"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, XML_ATTRIBUTE_NAME_IS_TRANSFER_DISTRIBUTION_DETERMINISTIC))
                Boolean.TryParse(Parametres[XML_ATTRIBUTE_NAME_IS_TRANSFER_DISTRIBUTION_DETERMINISTIC].FirstChild.Value, out isTransferDistributionDeterministic);


            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "TransferArrivalGeneration"))
                Boolean.TryParse(Parametres.Attributes["TransferArrivalGeneration"].Value, out bTransferArrivalGeneration);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, XML_ATTRIBUTE_NAME_MISSED_DEPARTURE_TRANSFER_BASED_ON_CHECK_IN_SHOW_UP))
                Boolean.TryParse(Parametres.Attributes[XML_ATTRIBUTE_NAME_MISSED_DEPARTURE_TRANSFER_BASED_ON_CHECK_IN_SHOW_UP].Value, out missedDepartureTransferBasedOnCheckInShowUp);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "FillTransfer"))
                Boolean.TryParse(Parametres.Attributes["FillTransfer"].Value, out bFillTransfer);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, XML_ATTRIBUTE_NAME_FILL_DEPARTURE_TRANSFER_BASED_ON_CI))
                Boolean.TryParse(Parametres.Attributes[XML_ATTRIBUTE_NAME_FILL_DEPARTURE_TRANSFER_BASED_ON_CI].Value, out fillDepartureTransferBasedOnCheckInShowUp);
            
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "FillQueues"))
                Boolean.TryParse(Parametres.Attributes["FillQueues"].Value, out bFillQueue);

            // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GlobalNames.STATION_GLOBAL_FILLING_TYPE_XML_ATTRIBUTE_NAME))
                _stationGlobalFillingType = Parametres.Attributes[GlobalNames.STATION_GLOBAL_FILLING_TYPE_XML_ATTRIBUTE_NAME].Value;
            // >> Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "UseSeed"))
                Boolean.TryParse(Parametres.Attributes["UseSeed"].Value, out bUseSeed);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GlobalNames.USE_SIMULATION_ENGINE_SEED_XML_ATTRIBUTE_NAME))
                Boolean.TryParse(Parametres.Attributes[GlobalNames.USE_SIMULATION_ENGINE_SEED_XML_ATTRIBUTE_NAME].Value, out bUseUserProvidedSimulationEngineSeed);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "Seed"))
                Int32.TryParse(Parametres.Attributes["Seed"].Value, out iSeed);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, GlobalNames.MFF_FILE_SEED_XML_ATTRIBUTE_NAME))  // >> Bug #14900 MFF file not created
                Int32.TryParse(Parametres.Attributes[GlobalNames.MFF_FILE_SEED_XML_ATTRIBUTE_NAME].Value, out mffFileSeed);            
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "GenerateAll"))
                Boolean.TryParse(Parametres.Attributes["GenerateAll"].Value, out bGenerateAllWarmUp);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "GenerateFlightsAtEnd"))
                Boolean.TryParse(Parametres.Attributes["GenerateFlightsAtEnd"].Value, out bGenerateFlightsAtEnd);
            #endregion

            #region 26/03/2012 - SGE - Parking Mulhouse

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "UseExistingPRKPlan"))
                Boolean.TryParse(Parametres.Attributes["UseExistingPRKPlan"].Value, out bUseExistingPRKPlan);
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "InitialCarStock"))
                sInitialCarStock = Parametres["InitialCarStock"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ShortStayTable"))
                sShortStayTable = Parametres["ShortStayTable"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "LongStayTable"))
                sLongStayTable = Parametres["LongStayTable"].FirstChild.Value;

            #endregion //26/03/2012 - SGE - Parking Mulhouse


            #region BagPlan

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "BagPlan"))
                Boolean.TryParse(Parametres.Attributes["BagPlan"].Value, out bBagPlan);
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "BagPlanScenario"))
                sBagPlanScenario = Parametres["BagPlanScenario"].FirstChild.Value;
            #endregion

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, BHS_GENERATE_LOCAL_IST_ATTRIBUTE_NAME)) // >> Task #13955 Pax2Sim -BHS trace loading issue
                Boolean.TryParse(Parametres.Attributes[BHS_GENERATE_LOCAL_IST_ATTRIBUTE_NAME].Value, out _bhsGenerateLocalIST);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, BHS_GENERATE_GROUP_IST_ATTRIBUTE_NAME)) // >> Task #14280 Bag Trace Loading time too long
                Boolean.TryParse(Parametres.Attributes[BHS_GENERATE_GROUP_IST_ATTRIBUTE_NAME].Value, out _bhsGenerateGroupIST);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, BHS_GENERATE_MUP_SEGREGATION_ATTRIBUTE_NAME)) // >> Task #14280 Bag Trace Loading time too long
                Boolean.TryParse(Parametres.Attributes[BHS_GENERATE_MUP_SEGREGATION_ATTRIBUTE_NAME].Value, out _bhsGenerateMUPSegregation);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, EXPORT_AUTOMOD_MAIN_DATA_ATTRIBUTE_NAME))
                Boolean.TryParse(Parametres.Attributes[EXPORT_AUTOMOD_MAIN_DATA_ATTRIBUTE_NAME].Value, out _exportAutomodMainData);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, CLEAR_AUTOMOD_MAIN_DATA_ATTRIBUTE_NAME))
                Boolean.TryParse(Parametres.Attributes[CLEAR_AUTOMOD_MAIN_DATA_ATTRIBUTE_NAME].Value, out _clearAutomodMainData);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, CLEAR_AUTOMOD_USER_DATA_ATTRIBUTE_NAME))
                Boolean.TryParse(Parametres.Attributes[CLEAR_AUTOMOD_USER_DATA_ATTRIBUTE_NAME].Value, out _clearAutomodUserData);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "DynamicReclaimAllocation"))
                Boolean.TryParse(Parametres.Attributes["DynamicReclaimAllocation"].Value, out bDynamicReclaimAllocation);
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "TerminalReclaimAllocation"))
                Int32.TryParse(Parametres.Attributes["TerminalReclaimAllocation"].Value, out iTerminalReclaimAllocation);
            

            #region Partie simulation

            sModelName = OverallTools.FonctionUtiles.getValueChild(Parametres, "ModelName");
            if ((sModelName != "") && (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres["ModelName"], "DisplayModel")))
                Boolean.TryParse(Parametres["ModelName"].Attributes["DisplayModel"].Value.ToString(), out bDisplayModel);

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "WarmUp"))
            {
                Double.TryParse(Parametres["WarmUp"].FirstChild.Value.ToString(), out  dWarmUp);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "RegeneratePaxplan"))
            {
                Boolean.TryParse(Parametres["RegeneratePaxplan"].FirstChild.Value.ToString(), out  bRegeneratePaxplan);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ExportUserData"))
            {
                Boolean.TryParse(Parametres["ExportUserData"].FirstChild.Value.ToString(), out  bExportUserData);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ReclaimUS"))
            {
                Boolean.TryParse(Parametres["ReclaimUS"].FirstChild.Value.ToString(), out  bReclaimUS);
            }

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "UseMakeUpSegregation"))
            {
                Boolean.TryParse(Parametres["UseMakeUpSegregation"].FirstChild.Value.ToString(), out  bUseMakeUpSegregation);
            }

            #endregion

            #region Partie pour SymAnalyser
            if (bSimAnalyserSimulation)
            {
                XmlNode xnSimAnalyser = Parametres["SimAnalyser"];
                if (OverallTools.FonctionUtiles.hasNamedChild(xnSimAnalyser, "TraceName"))
                    sTraceName = xnSimAnalyser["TraceName"].FirstChild.Value;
                if (OverallTools.FonctionUtiles.hasNamedChild(xnSimAnalyser, "TraceParameter"))
                {
                    if (OverallTools.FonctionUtiles.hasNamedAttribute(xnSimAnalyser["TraceParameter"], "IgnoredColumns"))
                        Int32.TryParse(xnSimAnalyser["TraceParameter"].Attributes["IgnoredColumns"].Value, out iIgnoredColumns);
                    else
                        iIgnoredColumns = 1;
                    ltcResult = new List<OverallTools.TraceTools.TraceContent>();
                    foreach (XmlNode xnChild in xnSimAnalyser["TraceParameter"].ChildNodes)
                    {
                        ltcResult.Add(new OverallTools.TraceTools.TraceContent(xnChild));
                    }
                }

                if (OverallTools.FonctionUtiles.hasNamedChild(xnSimAnalyser, "UserData"))
                    pudInputData = new ParamUserData(xnSimAnalyser["UserData"], sVersion);
            }
            #endregion

            // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.USER_ATTRIBUTES_XML_ELEMENT_NAME))
            {
                loadUserAttributesParamsFromXMLNode(Parametres[GlobalNames.USER_ATTRIBUTES_XML_ELEMENT_NAME]);
            }
            // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, XML_ELEMENT_NAME_ANALYSIS_RESULTS_FILTERS))
                analysisResultsFilters = loadAnalysisResultsFilters(Parametres[XML_ELEMENT_NAME_ANALYSIS_RESULTS_FILTERS]);

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, XML_ELEMENT_NAME_DELETED_CUSTOM_ANALYSIS_RESULTS_FILTERS))
                deletedCustomAnalysisResultsFilters = loadDeletedCustomAnalysisResultsFilterNames(Parametres[XML_ELEMENT_NAME_DELETED_CUSTOM_ANALYSIS_RESULTS_FILTERS]);

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, XML_ELEMENT_NAME_FLOW_TYPES))
                flowTypes = loadFlowTypes(Parametres[XML_ELEMENT_NAME_FLOW_TYPES]);

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "UserData"))
                pudUserData = new ParamUserData(Parametres["UserData"], sVersion);

            // utilisation des tables d'exception
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "RejectEx"))
                foreach (XmlElement table in Parametres["RejectEx"].ChildNodes)
                    UseException(table.GetAttribute("name"), false);

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, XML_ELEMENT_NAME_DISTRIBUTION_LEVELS))
                percentilesLevels = loadDistributionLevels(Parametres[XML_ELEMENT_NAME_DISTRIBUTION_LEVELS]);
        }

        private List<double> loadDistributionLevels(XmlNode distributionLevelsNode)
        {
            List<double> distributionLevels = new List<double>();

            double level1 = -1;
            double level2 = -1;
            double level3 = -1;

            if (OverallTools.FonctionUtiles.hasNamedAttribute(distributionLevelsNode, XML_ELEMENT_NAME_DISTRIBUTION_LEVEL1))
                Double.TryParse(distributionLevelsNode.Attributes[XML_ELEMENT_NAME_DISTRIBUTION_LEVEL1].Value, out level1);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(distributionLevelsNode, XML_ELEMENT_NAME_DISTRIBUTION_LEVEL2))
                Double.TryParse(distributionLevelsNode.Attributes[XML_ELEMENT_NAME_DISTRIBUTION_LEVEL2].Value, out level2);

            if (OverallTools.FonctionUtiles.hasNamedAttribute(distributionLevelsNode, XML_ELEMENT_NAME_DISTRIBUTION_LEVEL3))
                Double.TryParse(distributionLevelsNode.Attributes[XML_ELEMENT_NAME_DISTRIBUTION_LEVEL3].Value, out level3);

            if (level1 != -1 && level2 != -1 && level3 != -1)
            {
                distributionLevels.Add(level1);
                distributionLevels.Add(level2);
                distributionLevels.Add(level3);
            }
            return distributionLevels;
        }

        public XmlNode getParams(XmlDocument projet)
        {
            if (projet == null)
                return null;
            XmlElement result = projet.CreateElement("ParamScenario");
            result.SetAttribute("Name", sName);                       
            result.SetAttribute(GlobalNames.IS_LIEGE_ALLOCATION_PARAM_SCENARIO_ATTRIBUTE_NAME, isLiegeAllocation.ToString());  // << Bug #13367 Liege allocation
            result.SetAttribute(GlobalNames.IS_TRACE_ANALYSIS_SCENARIO_PARAM_SCENARIO_ATTRIBUTE_NAME, isTraceAnalysisScenario.ToString());  // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues
            result.SetAttribute(GlobalNames.PaxPlanName, bPaxplan.ToString());
            result.SetAttribute("DeparturePeak", bDeparture.ToString());
            result.SetAttribute("ArrivalPeak", bArrival.ToString());
            result.SetAttribute(GENERATE_PAX_PLAN_FOR_STATIC_XML_TAG, bGeneratePaxPlanForStaticAnalysis.ToString());
            result.SetAttribute("PaxSimulation", bPaxSimulation.ToString());
            result.SetAttribute("BHSSimulation", bBHSSimulation.ToString());
            result.SetAttribute("TMSSimulation", bTMSSimulation.ToString());
            if (bTraceSaveMode)
                result.SetAttribute("TraceSaveMode", bTraceSaveMode.ToString());

            result.SetAttribute("BagPlan", bBagPlan.ToString());
            result.SetAttribute(BHS_GENERATE_LOCAL_IST_ATTRIBUTE_NAME, _bhsGenerateLocalIST.ToString());    // >> Task #13955 Pax2Sim -BHS trace loading issue
            result.SetAttribute(BHS_GENERATE_GROUP_IST_ATTRIBUTE_NAME, _bhsGenerateGroupIST.ToString());    // >> Task #14280 Bag Trace Loading time too long
            result.SetAttribute(BHS_GENERATE_MUP_SEGREGATION_ATTRIBUTE_NAME, _bhsGenerateMUPSegregation.ToString());    // >> Task #14280 Bag Trace Loading time too long
            result.SetAttribute(EXPORT_AUTOMOD_MAIN_DATA_ATTRIBUTE_NAME, _exportAutomodMainData.ToString());

            result.SetAttribute(CLEAR_AUTOMOD_MAIN_DATA_ATTRIBUTE_NAME, _clearAutomodMainData.ToString());
            result.SetAttribute(CLEAR_AUTOMOD_USER_DATA_ATTRIBUTE_NAME, _clearAutomodUserData.ToString());

            result.SetAttribute("DynamicReclaimAllocation", bDynamicReclaimAllocation.ToString());
            result.SetAttribute("TerminalReclaimAllocation", iTerminalReclaimAllocation.ToString());

            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, AUTHOR_NAME_XML_TAG, authorName);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, SCENARIO_DESCRIPTION_XML_TAG, scenarioDescription);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, CREATION_DATE_XML_TAG, creationDate.ToString());
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, LAST_MODIFICATION_DATE_XML_TAG, lastUpdateDate.ToString());
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

            OverallTools.FonctionUtiles.PrintValueChild(result, projet, LAST_MODIFICATION_P2S_VERSION_XML_TAG, _lastModificationP2SVersion.ToString());

            OverallTools.FonctionUtiles.PrintValueChild(result, projet, BAG_TRACE_PATH_XML_TAG, bagTracePath);

            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "BagPlanScenario", sBagPlanScenario);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Start", dtDateDebut.ToString());
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "End", dtDateFin.ToString());
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "SamplingStep", dSamplingStep.ToString());
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "AnalysisRange", dAnalysisRange.ToString());
            if (bUseStatisticsStep)
            {
                OverallTools.FonctionUtiles.PrintValueChild(result, projet, "UseStatisticsStep", "True");
                OverallTools.FonctionUtiles.PrintValueChild(result, projet, "StatisticsStep", dStatisticsStep.ToString());
                OverallTools.FonctionUtiles.PrintValueChild(result, projet, "StatisticsStepMode", sStatisticsStepMode);
            }

            if (sModelName != null)
            {
                XmlElement xnSon = OverallTools.FonctionUtiles.CreateElement(projet, "ModelName", sModelName);
                xnSon.SetAttribute("DisplayModel", bDisplayModel.ToString());
                result.AppendChild(xnSon);
            }
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "WarmUp", dWarmUp.ToString());
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "RegeneratePaxplan", bRegeneratePaxplan.ToString());
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ExportUserData", bExportUserData.ToString());
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ReclaimUS", bReclaimUS.ToString());
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "UseMakeUpSegregation", bUseMakeUpSegregation.ToString());

            saveDistributionLevels(percentilesLevels, projet, result);

            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "FPD", sFPD);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "DLoadFactors", sDepartureLoadFactors);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "CI_ShowUp", sCI_ShowUpTable);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "OCT_CI", sOCT_CI_Table);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "OCT_MakeUp", sOCT_MakeUp);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "FPA", sFPA);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ALoadFactors", sArrivalLoadFactors);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ICT", sICT_Table);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "TransferTerminalDistribution", sTransfTerminalDistribution);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "TransferFligthCategoryDistribution", sTransfFligthCategoryDistribution);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, XML_ATTRIBUTE_NAME_IS_TRANSFER_DISTRIBUTION_DETERMINISTIC, isTransferDistributionDeterministic.ToString());

            #region 26/03/2012 - SGE - Parking Mulhouse
            result.SetAttribute("UseExistingPRKPlan", bUseExistingPRKPlan.ToString());
            
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "InitialCarStock", sInitialCarStock);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ShortStayTable", sShortStayTable);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "LongStayTable", sLongStayTable);

            #endregion //26/03/2012 - SGE - Parking Mulhouse


            result.SetAttribute("Seed", iSeed.ToString());
            result.SetAttribute(GlobalNames.MFF_FILE_SEED_XML_ATTRIBUTE_NAME, mffFileSeed.ToString());  // >> Bug #14900 MFF file not created
            result.SetAttribute("UseSeed", bUseSeed.ToString());
            result.SetAttribute(GlobalNames.USE_SIMULATION_ENGINE_SEED_XML_ATTRIBUTE_NAME, bUseUserProvidedSimulationEngineSeed.ToString());
            result.SetAttribute("TransferArrivalGeneration", bTransferArrivalGeneration.ToString());
            result.SetAttribute(XML_ATTRIBUTE_NAME_MISSED_DEPARTURE_TRANSFER_BASED_ON_CHECK_IN_SHOW_UP, missedDepartureTransferBasedOnCheckInShowUp.ToString());
            result.SetAttribute("FillTransfer", bFillTransfer.ToString());
            result.SetAttribute(XML_ATTRIBUTE_NAME_FILL_DEPARTURE_TRANSFER_BASED_ON_CI, fillDepartureTransferBasedOnCheckInShowUp.ToString());
            result.SetAttribute("GenerateAll", bGenerateAllWarmUp.ToString());
            result.SetAttribute("GenerateFlightsAtEnd", bGenerateFlightsAtEnd.ToString());
            result.SetAttribute("FillQueues", bFillQueue.ToString());
            result.SetAttribute(GlobalNames.STATION_GLOBAL_FILLING_TYPE_XML_ATTRIBUTE_NAME, _stationGlobalFillingType);  // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "AircraftType", sAircraftType);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Airline", sAirline);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "NbBags", sNbBags);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "NbVisitors", sNbVisitors);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "NbTrolley", sNbTrolley);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "FlightCategories", sFlightCategories);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, GlobalNames.flightSubcategories_XML_childNodeName, sFlightSubcategories);   // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "AircraftLinksTable", sAircraftLinksTable);

            // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            OverallTools.FonctionUtiles
                .PrintValueChild(result, projet, GlobalNames.NB_OF_PAX_XML_ATTRIBUTE_NAME, NumberOfPassengers);
            OverallTools.FonctionUtiles
                .PrintValueChild(result, projet, GlobalNames.NB_OF_BAGS_XML_ATTRIBUTE_NAME, NumberOfBaggages);
            
            result.SetAttribute(GlobalNames.USE_DEFINED_NB_PAX_XML_ATTRIBUTE_NAME, bUseDefinedNbPax.ToString());
            result.SetAttribute(GlobalNames.USE_DEFINED_NB_BAGS_XML_ATTRIBUTE_NAME, bUseDefinedNbBags.ToString());
            // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

            OverallTools.FonctionUtiles
                .PrintValueChild(result, projet, GlobalNames.USA_STANDARD_PARAMS_XML_ATTRIBUTE_NAME, UsaStandardParamTableName);    // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table

            OverallTools.FonctionUtiles.PrintValueChild(result, projet, XML_ATTRIBUTE_NAME_FP_PARAMETERS_TABLE, _fpParametersTableName);
            
            // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            OverallTools.FonctionUtiles.PrintValueChild(result, projet,
                "arrivalBaggageLoadingRateTableName", sArrivalBaggageLoadingRateTableName);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, 
                "arrivalBaggageLoadingDelayTableName", sArrivalBaggageLoadingDelayTableName);
            // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            
            OverallTools.FonctionUtiles.PrintValueChild(result, projet,
                "reclaimBagsLimitDistributionTableName", _reclaimBagsLimitDistributionTableName);
            
            // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            OverallTools.FonctionUtiles.PrintValueChild(result, projet,
                GlobalNames.EBS_INPUT_RATE_XML_CHILD_NODE_NAME, _ebsInputRateTableName);

            OverallTools.FonctionUtiles.PrintValueChild(result, projet,
                GlobalNames.EBS_OUTPUT_RATE_XML_CHILD_NODE_NAME, _ebsOutputRateTableName);
            // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

            // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
            OverallTools.FonctionUtiles.PrintValueChild(result, projet,
                GlobalNames.OCT_BAGDROP_XML_CHILD_NODE_NAME, _OCT_BagDropTableName);
            // >> Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop

            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "OCT_BC", sOCT_BC);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "OCT_BG", sOCT_BG);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ProcessTimes", sProcessTimes);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Security", sSecurity);
            // << Task #7570 new Desk and extra information for Pax -Phase I B
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "UserProcess", sUserProcess);
            // >> Task #7570 new Desk and extra information for Pax -Phase I B
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Transfer", sTransfer);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Passport", sPassport);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Saturation", sSaturation);
            
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Itinerary", sItinerary);

            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Oneof", sOneof);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Opening_CI", sOpening_CITable);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "CapaQueues", sCapaQueues);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "GroupQueues", sGroupQueues);
            // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            OverallTools.FonctionUtiles
                .PrintValueChild(result, projet, GlobalNames.PROCESS_CAPACITIES_SCENARIO_PARAM_NAME, processCapacities);
            // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "AnimatedQueues", sAnimatedQueues);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ProcessSchedule", sProcessSchedule);

            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Terminal", sTerminal);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "General", sGeneral);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ArrivalInfeedGroups", sArrivalInfeedGroups);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "CIGroups", sCIGroups);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "HBS3Routing", sHBS3Routing);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "TransferInfeedGroups", sTransferInfeedGroups);


            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "CICollectors", sCICollectors);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "CIRouting", sCIRouting);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "TransferRouting", sTransferRouting);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "FlowSplit", sFlowSplit);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "Process", sProcess);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ArrivalContainers", sArrivalContainers);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "ArrivalMeanFlows", sArrivalMeanFlows);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "CheckInMeanFlows", sCheckInMeanFlows);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "TransferMeanFlows", sTransferMeanFlows);

#if(PAXINOUTUTILISATION)
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "PaxIn", sPaxIn);
            OverallTools.FonctionUtiles.PrintValueChild(result, projet, "PaxOut", sPaxOut);
#endif
            // utilisation des tables d'exception
            if (lRejectEx != null)
            {
                XmlNode xnRejectEx = projet.CreateElement("RejectEx");
                foreach (String tableName in lRejectEx)
                {
                    XmlElement xnRejectExTable = projet.CreateElement("Table");
                    xnRejectExTable.SetAttribute("name", tableName);
                    xnRejectEx.AppendChild(xnRejectExTable);
                }
                result.AppendChild(xnRejectEx);
            }
            if(!bUseSegregationEx)
                OverallTools.FonctionUtiles.PrintValueChild(result, projet, "UseSegregationEx", true.ToString());

            if (bSimAnalyserSimulation)
            {
                XmlNode xnSimAnalyser = projet.CreateElement("SimAnalyser");
                result.AppendChild(xnSimAnalyser);
                OverallTools.FonctionUtiles.PrintValueChild(xnSimAnalyser, projet, "TraceName", sTraceName);
                if ((ltcResult != null) && (ltcResult.Count > 0))
                {
                    XmlElement xnTraceParameter = projet.CreateElement("TraceParameter");
                    xnTraceParameter.SetAttribute("IgnoredColumns", iIgnoredColumns.ToString());
                    xnSimAnalyser.AppendChild(xnTraceParameter);
                    foreach (OverallTools.TraceTools.TraceContent tcContent in ltcResult)
                    {
                        xnTraceParameter.AppendChild(tcContent.ToXml(projet));
                    }
                }
                if (pudInputData != null)
                {
                    xnSimAnalyser.AppendChild(pudInputData.getParams(projet));
                }
            }
            // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            if (userAttributesTablesDictionary != null && userAttributesTablesDictionary.Count > 0)
            {
                XmlNode userAttributesXmlNode = getUserAttributesParams(projet);
                if (userAttributesXmlNode != null)
                    result.AppendChild(userAttributesXmlNode);
            }
            // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            if (pudUserData != null)
            {
                result.AppendChild(pudUserData.getParams(projet));
            }
            saveAnalysisResultsFilters(analysisResultsFilters, projet, result);
            saveDeletedCustomAnalysisResultsFilterNames(deletedCustomAnalysisResultsFilters, projet, result);
            saveFlowTypes(flowTypes, projet, result);
            return result;
        }

        const string XML_ELEMENT_NAME_DISTRIBUTION_LEVELS = "DISTRIBUTION_LEVELS";
        const string XML_ELEMENT_NAME_DISTRIBUTION_LEVEL1 = "LEVEL1";
        const string XML_ELEMENT_NAME_DISTRIBUTION_LEVEL2 = "LEVEL2";
        const string XML_ELEMENT_NAME_DISTRIBUTION_LEVEL3 = "LEVEL3";
        private void saveDistributionLevels(List<double> distributionLevels, XmlDocument project, XmlElement paramScenarioElement)
        {
            if (distributionLevels != null && distributionLevels.Count == 3)
            {
                XmlElement levelsRootElement = project.CreateElement(XML_ELEMENT_NAME_DISTRIBUTION_LEVELS);
                
                levelsRootElement.SetAttribute(XML_ELEMENT_NAME_DISTRIBUTION_LEVEL1, distributionLevels[0].ToString());
                levelsRootElement.SetAttribute(XML_ELEMENT_NAME_DISTRIBUTION_LEVEL2, distributionLevels[1].ToString());
                levelsRootElement.SetAttribute(XML_ELEMENT_NAME_DISTRIBUTION_LEVEL3, distributionLevels[2].ToString());
                
                paramScenarioElement.AppendChild(levelsRootElement);
            }
        }

        // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        /// <summary>
        /// Creates the UserAttributes tag: that will contain each user attribute used for the simulation.
        /// Each user Attribute will correspond to an element with the attribute -> the user attribute's name
        /// and the value -> the user attribute's table used
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private XmlNode getUserAttributesParams(XmlDocument project)
        {
            if (project == null)
                return null;

            XmlElement result = project.CreateElement(GlobalNames.USER_ATTRIBUTES_XML_ELEMENT_NAME);

            foreach (String userAttribute in userAttributesTablesDictionary.Keys)
            {
                String userAttributeUsedTableName = userAttributesTablesDictionary[userAttribute].ToString();
                
                String xmlElementName = userAttribute;
                xmlElementName = xmlElementName.Replace(" ", "_");
                
                XmlElement xmlElement = OverallTools.FonctionUtiles
                    .CreateElement(project, xmlElementName, userAttributeUsedTableName);
                xmlElement.SetAttribute(GlobalNames.USER_ATTRIBUTE_XML_ELEMENT_ATTRIBUTE_NAME, userAttribute);

                result.AppendChild(xmlElement);
            }
            return result;
        }

        private void loadUserAttributesParamsFromXMLNode(XmlNode userAttributesNode)
        {
            userAttributesTablesDictionary = new Dictionary<String, String>();

            foreach (XmlNode childNode in userAttributesNode.ChildNodes)
            {
                String userAttributeName = "";
                String userAttributTableName = "";

                if (OverallTools.FonctionUtiles
                    .hasNamedAttribute(childNode, GlobalNames.USER_ATTRIBUTE_XML_ELEMENT_ATTRIBUTE_NAME))
                {
                    userAttributeName = childNode.Attributes[GlobalNames.USER_ATTRIBUTE_XML_ELEMENT_ATTRIBUTE_NAME].Value;
                }
                userAttributTableName = childNode.FirstChild.Value.ToString();

                if (!userAttributesTablesDictionary.ContainsKey(userAttributeName))
                    userAttributesTablesDictionary.Add(userAttributeName, userAttributTableName);
            }
        }
        // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab

        // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes        
        public const string XML_ELEMENT_NAME_ANALYSIS_RESULTS_FILTERS = "AnalysisResultsFilters";
        public const string XML_ELEMENT_NAME_RESULT_FILTER_PREFIX = "ResultFilter";
        public const string XML_ATTRIBUTE_NAME_FILTER_NAME = "filterName";
        public const string XML_ATTRIBUTE_NAME_FROM_STATION = "fromStationCode";
        public const string XML_ATTRIBUTE_NAME_FROM_STATION_TIME_TYPE = "fromStationTimeType";
        public const string XML_ATTRIBUTE_NAME_TO_STATION = "toStationCode";
        public const string XML_ATTRIBUTE_NAME_TO_STATION_TIME_TYPE = "toStationTimeType";
        public const string XML_ATTRIBUTE_NAME_GENERATE = "generate";
        public const string XML_ATTRIBUTE_NAME_WITH_RECIRC = "withRecirculation";
        public const string XML_ATTRIBUTE_NAME_WITH_FROM_SEGR = "withFromSegregation";
        public const string XML_ATTRIBUTE_NAME_WITH_TO_SEGR = "withToSegregation";
        public const string XML_ATTRIBUTE_NAME_EXCLUDE_EBS = "excludeEBSTime";
        public const string XML_ATTRIBUTE_NAME_GENERATE_IST = "generateIST";
        private void saveAnalysisResultsFilters(List<AnalysisResultFilter> analysisResultsFilters,
            XmlDocument project, XmlElement paramScenarioElement)
        {
            if (analysisResultsFilters != null)
            {
                XmlElement resultsFiltersRootElement = project.CreateElement(XML_ELEMENT_NAME_ANALYSIS_RESULTS_FILTERS);
                for (int i = 0; i < analysisResultsFilters.Count; i++)
                {
                    AnalysisResultFilter resultFilter = analysisResultsFilters[i];
                    XmlElement resultFilterElement = project.CreateElement(resultFilter.filterName + "RF" + i);
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_FILTER_NAME, resultFilter.filterName);
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_FROM_STATION, resultFilter.fromStationCode);
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_FROM_STATION_TIME_TYPE, resultFilter.fromStationTimeType);
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_TO_STATION, resultFilter.toStationCode);
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_TO_STATION_TIME_TYPE, resultFilter.toStationTimeType);
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_WITH_RECIRC, resultFilter.withRecirculation.ToString());
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_WITH_FROM_SEGR, resultFilter.withFromSegregation.ToString());
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_WITH_TO_SEGR, resultFilter.withToSegregation.ToString());
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_EXCLUDE_EBS, resultFilter.excludeEBSStorageTime.ToString());
                    resultFilterElement.SetAttribute(XML_ATTRIBUTE_NAME_GENERATE_IST, resultFilter.generateIST.ToString());
                    resultsFiltersRootElement.AppendChild(resultFilterElement);
                }
                paramScenarioElement.AppendChild(resultsFiltersRootElement);
            }
        }
        private List<AnalysisResultFilter> loadAnalysisResultsFilters(XmlElement resultsFiltersRootElement)
        {
            List<AnalysisResultFilter> analysisResultsFilters = new List<AnalysisResultFilter>();
            if (resultsFiltersRootElement == null)
                return analysisResultsFilters;
            foreach (XmlElement resultFilterElement in resultsFiltersRootElement.ChildNodes)
            {
                bool dataRetrieved = true;
                string name = "";
                string fromStationCode = "";
                string fromStationTimeType = "";
                string toStationCode = "";
                string toStationTimeType = "";
                bool withRecirc = true;
                bool withFromSegr = true;
                bool withToSegr = true;
                bool excludeEBS = true;
                bool generateIST = true;
                if (OverallTools.FonctionUtiles
                    .hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_FILTER_NAME))
                {
                    name = resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_FILTER_NAME].Value;
                }
                else
                    dataRetrieved = false;
                if (OverallTools.FonctionUtiles
                    .hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_FROM_STATION))
                {
                    fromStationCode = resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_FROM_STATION].Value;
                }
                else
                    dataRetrieved = false;
                if (OverallTools.FonctionUtiles
                    .hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_FROM_STATION_TIME_TYPE))
                {
                    fromStationTimeType = resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_FROM_STATION_TIME_TYPE].Value;
                }
                else
                    dataRetrieved = false;
                if (OverallTools.FonctionUtiles
                    .hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_TO_STATION))
                {
                    toStationCode = resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_TO_STATION].Value;
                }
                else
                    dataRetrieved = false;
                if (OverallTools.FonctionUtiles
                    .hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_TO_STATION_TIME_TYPE))
                {
                    toStationTimeType = resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_TO_STATION_TIME_TYPE].Value;
                }
                else
                    dataRetrieved = false;
                if (!OverallTools.FonctionUtiles.hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_WITH_RECIRC)
                    || !Boolean.TryParse(resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_WITH_RECIRC].Value, out withRecirc))                
                    dataRetrieved = false;
                if (!OverallTools.FonctionUtiles.hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_WITH_FROM_SEGR)
                    || !Boolean.TryParse(resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_WITH_FROM_SEGR].Value, out withFromSegr))                
                    dataRetrieved = false;
                if (!OverallTools.FonctionUtiles.hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_WITH_TO_SEGR)
                    || !Boolean.TryParse(resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_WITH_TO_SEGR].Value, out withToSegr))
                    dataRetrieved = false;
                if (!OverallTools.FonctionUtiles.hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_EXCLUDE_EBS)
                    || !Boolean.TryParse(resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_EXCLUDE_EBS].Value, out excludeEBS))                
                    dataRetrieved = false;
                if (!OverallTools.FonctionUtiles.hasNamedAttribute(resultFilterElement, XML_ATTRIBUTE_NAME_GENERATE_IST)
                    || !Boolean.TryParse(resultFilterElement.Attributes[XML_ATTRIBUTE_NAME_GENERATE_IST].Value, out generateIST))
                {
                    generateIST = false;
                    //dataRetrieved = false;    compatibility with previous versions that didn't have the attribute
                }
                if (dataRetrieved)
                {
                    AnalysisResultFilter resultFilter 
                        = new AnalysisResultFilter(name, fromStationCode, fromStationTimeType, toStationCode, toStationTimeType,
                            withRecirc, withFromSegr, withToSegr, excludeEBS, generateIST);
                    analysisResultsFilters.Add(resultFilter);
                }
            }
            return analysisResultsFilters;
        }
        
        public const string XML_ELEMENT_NAME_DELETED_CUSTOM_ANALYSIS_RESULTS_FILTERS = "DeletedCustomAnalysisResultsFilters";

        private void saveDeletedCustomAnalysisResultsFilterNames(List<string> deletedFilterNames,
            XmlDocument project, XmlElement paramScenarioElement)
        {
            if (deletedFilterNames != null)
            {
                XmlElement namesListRootElement = project.CreateElement(XML_ELEMENT_NAME_DELETED_CUSTOM_ANALYSIS_RESULTS_FILTERS);

                for (int i = 0; i < deletedFilterNames.Count; i++)
                {
                    string filterName = deletedFilterNames[i];
                    XmlElement filterNameElement = project.CreateElement(filterName + "DRF" + i);
                    filterNameElement.SetAttribute(XML_ATTRIBUTE_NAME_FILTER_NAME, filterName);
                    namesListRootElement.AppendChild(filterNameElement);
                }
                paramScenarioElement.AppendChild(namesListRootElement);
            }
        }
                
        private List<string> loadDeletedCustomAnalysisResultsFilterNames(XmlElement deletedFiltersListRootElement)
        {
            List<string> deletedFiltersNames = new List<string>();
            if (deletedFiltersListRootElement == null)
                return deletedFiltersNames;
            foreach (XmlElement deletedFilterElement in deletedFiltersListRootElement.ChildNodes)
            {
                if (OverallTools.FonctionUtiles
                    .hasNamedAttribute(deletedFilterElement, XML_ATTRIBUTE_NAME_FILTER_NAME))
                {
                    string name = deletedFilterElement.Attributes[XML_ATTRIBUTE_NAME_FILTER_NAME].Value;
                    deletedFiltersNames.Add(name);
                }
            }
            return deletedFiltersNames;
        }
        // << Task #15088 Pax2Sim - BHS Analysis - Times and Indexes

        public const string XML_ELEMENT_NAME_FLOW_TYPES = "FlowTypesCodes";
        public const string XML_ATTRIBUTE_NAME_FLOW_TYPE = "FlowTypeName";
        private void saveFlowTypes(List<string> flowTypes, XmlDocument project, XmlElement paramScenarioElement)
        {
            if (flowTypes != null)
            {
                XmlElement namesListRootElement = project.CreateElement(XML_ELEMENT_NAME_FLOW_TYPES);

                for (int i = 0; i < flowTypes.Count; i++)
                {
                    string flowType = flowTypes[i];
                    XmlElement flowTypeElement = project.CreateElement(flowType);
                    flowTypeElement.SetAttribute(XML_ATTRIBUTE_NAME_FLOW_TYPE, flowType);
                    namesListRootElement.AppendChild(flowTypeElement);
                }
                paramScenarioElement.AppendChild(namesListRootElement);
            }
        }

        private List<string> loadFlowTypes(XmlElement flowTypesListRootElement)
        {
            List<string> flowTypes = new List<string>();
            if (flowTypesListRootElement == null)
                return flowTypes;
            foreach (XmlElement flowTypeElement in flowTypesListRootElement.ChildNodes)
            {
                if (OverallTools.FonctionUtiles
                    .hasNamedAttribute(flowTypeElement, XML_ATTRIBUTE_NAME_FLOW_TYPE))
                {
                    string name = flowTypeElement.Attributes[XML_ATTRIBUTE_NAME_FLOW_TYPE].Value;
                    flowTypes.Add(name);
                }
            }
            return flowTypes;
        }

        #region Fonctions pour mettre et supprimer les paramètrages.
        public void SetDeparture()
        {
            bDeparture = true;
        }
        public void EraseDeparture()
        {
            bDeparture = false;
        }
        public void SetArrival()
        {
            bArrival = true;
        }
        public void EraseArrival()
        {
            bArrival = false;
        }
        public void SetPaxPlan()
        {
            bPaxplan = true;
        }
        public void ErasePaxPlan()
        {
            bPaxplan = false;
        }

        public void SetPaxSimulation()
        {
            bPaxplan = bPaxSimulation;
        }
        public void ErasePaxSimulation()
        {
            bPaxplan = bPaxSimulation;
        }

        public void SetBHSSimulation()
        {
            bPaxplan = bBHSSimulation;
        }
        public void EraseBHSSimulation()
        {
            bPaxplan = bBHSSimulation;
        }

        public void SetTMSSimulation()
        {
            bPaxplan = bTMSSimulation;
        }
        public void EraseTMSSimulation()
        {
            bPaxplan = bTMSSimulation;
        }
        #endregion

        internal List<string> getReview(bool bPrintPDF)
        {
            List<string> lsResult = new List<string>();
            //ArrayList alResult = new ArrayList();
            /*Le nom et les statistics communes à tous les scénarios.*/
            lsResult.Add("Name" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sName);
            lsResult.Add(" ");

            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            /*
            lsResult.Add("Author Name" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + authorName);
            lsResult.Add("Scenario Description" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + scenarioDescription);
            lsResult.Add("Creation Date" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + creationDate);
            lsResult.Add("Last Modification Date" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + lastUpdateDate);
            lsResult.Add(" ");
             */ 
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

            lsResult.Add("Date" + " : " + dtDateDebut.ToString() + "   " + dtDateFin.ToString());
            if (bSimAnalyserSimulation)
                lsResult.Add("Sampling step" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + dSamplingStep.ToString());
            else
                lsResult.Add("Sampling step" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + dSamplingStep.ToString() + PrintIndexation(bPrintPDF, false, false, 2) + "Analysis range" + " : " + dAnalysisRange.ToString());
            if (this.UseStatisticsStep)
                lsResult.Add("Statistic step" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + dStatisticsStep.ToString() + PrintIndexation(bPrintPDF, false, false, 2) + "Collected value" + " : " + this.StatisticsStepMode);
            lsResult.Add(" ");


            if (!bSimAnalyserSimulation)
            {
                /*Les principales analises réalisées.*/
                lsResult.Add("Peak flow analysis");
                lsResult.Add(PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(bDeparture, "Departure") + PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(bArrival, "Arrival") + PrintIndexation(bPrintPDF, false, false, 2) + PrintBoolean(bPaxplan || bGeneratePaxPlanForStaticAnalysis, GlobalNames.PaxPlanName));
                lsResult.Add("Capacity analysis");
                lsResult.Add(PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(this.bPaxSimulation, "Passenger simulation"));
                /*if ((epPerimetre == PAX2SIM.EnumPerimetre.BHS)
                    || (epPerimetre == PAX2SIM.EnumPerimetre.HUB)
                    || (epPerimetre == PAX2SIM.EnumPerimetre.TMS))
                {*/
                if (bBHSSimulation)
                    lsResult.Add(PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(bBHSSimulation, "Baggage handling system"));
                /*if ((epPerimetre == PAX2SIM.EnumPerimetre.HUB)
                    || (epPerimetre == PAX2SIM.EnumPerimetre.TMS))*/
                if (bTMSSimulation)
                    lsResult.Add(PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(bTMSSimulation, "Trolley management system"));
            }
            if (bDeparture || bPaxplan || bGeneratePaxPlanForStaticAnalysis)
            {
                lsResult.Add(" ");
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 1) + "Departure tables");
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Departure flight plan" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sFPD);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Departure load factors" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sDepartureLoadFactors);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Check-In show Up profile" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sCI_ShowUpTable);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Open/Close time for Check-In" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sOCT_CI_Table);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Open/Close time for Make-Up" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sOCT_MakeUp);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Open/Close time for Boarding gate" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sOCT_BG);

                // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Open/Close time for Baggage Drop : "
                    + PrintIndexation(bPrintPDF, false, false, 1) + _OCT_BagDropTableName);
                // >> Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop

                // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "EBS Input Rate table : " 
                    + PrintIndexation(bPrintPDF, false, false, 1) + _ebsInputRateTableName);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "EBS Output Rate table : "
                    + PrintIndexation(bPrintPDF, false, false, 1) + _ebsOutputRateTableName);
                // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

                #region 26/03/2012 - SGE - Parking Mulhouse
                if (sInitialCarStock != "")
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, true, true, 1) + "Car park informations");
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Initial occupation of car parks." + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sInitialCarStock);
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Distribution for short stay cars" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sShortStayTable);
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Distribution for long stay cars" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sLongStayTable);
                    
                        lsResult.Add(PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(bUseExistingPRKPlan, "Use existing PRKPlan for parking statistics."));
                }

                #endregion //26/03/2012 - SGE - Parking Mulhouse
            }
            if (bArrival || bPaxplan || bGeneratePaxPlanForStaticAnalysis)
            {
                lsResult.Add(" ");
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 1) + "Arrival tables");
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Arrival flight plan" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sFPA);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Arrival load factors" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sArrivalLoadFactors);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Interconnecting times" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sICT_Table);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Open/Close time for Baggage claim" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sOCT_BC);
                // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Arrival Baggage Loading Rate :" 
                    + PrintIndexation(bPrintPDF, false, false, 1) + sArrivalBaggageLoadingRateTableName);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Arrival Baggage Loading Delay :"
                    + PrintIndexation(bPrintPDF, false, false, 1) + sArrivalBaggageLoadingDelayTableName);
                // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay                
            }
            if (bArrival || bPaxplan || bDeparture || bGeneratePaxPlanForStaticAnalysis)
            {
                lsResult.Add(" ");
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 1) + "General tables");
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Flight categories" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sFlightCategories);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Flight subcategories" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sFlightSubcategories);  // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Aircraft types" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sAircraftType);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Airline codes" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sAirline);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Number of bags" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sNbBags);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Number of visitors" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sNbVisitors);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Number of trolleys" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sNbTrolley);
                if ((bArrival && bDeparture) || bPaxplan || bGeneratePaxPlanForStaticAnalysis)
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Aircraft Links Table :" + PrintIndexation(bPrintPDF, false, false, 1) + sAircraftLinksTable);
                }
                // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Use defined number of passengers : "
                    + PrintIndexation(bPrintPDF, false, false, 1) + bUseDefinedNbPax.ToString());
                if (bUseDefinedNbPax)
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Number of passengers : "
                        + PrintIndexation(bPrintPDF, false, false, 1) + NumberOfPassengers);
                }

                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Use defined number of bags : "
                    + PrintIndexation(bPrintPDF, false, false, 1) + bUseDefinedNbBags.ToString());
                if (bUseDefinedNbBags)
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Number of bags : "
                        + PrintIndexation(bPrintPDF, false, false, 1) + NumberOfBaggages);
                }
                // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

                if (_fpParametersTableName != "")
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Flight Plan Parameters table : "
                            + PrintIndexation(bPrintPDF, false, false, 1) + _fpParametersTableName);
                }
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "USA Standard Parameters : "
                        + PrintIndexation(bPrintPDF, false, false, 1) + UsaStandardParamTableName);          // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Reclaim Bags Limit Distribution : "
                        + PrintIndexation(bPrintPDF, false, false, 1) + _reclaimBagsLimitDistributionTableName);
            }

            if (bPaxplan)
            {
                lsResult.Add(" ");
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 1) + GlobalNames.PaxPlanName);
                if (bUseSeed)                
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Random seed " + iSeed.ToString());
                if (bUseUserProvidedSimulationEngineSeed)
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "MFF File Random seed " + mffFileSeed.ToString());// >> Bug #14900 MFF file not created
                
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Passengers out of range");
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "At starting time" + PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(bGenerateAllWarmUp, "Generate all at start up") + PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(!bGenerateAllWarmUp, "Ignore all at start up"));
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "At ending time" + PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(bGenerateFlightsAtEnd, "Generate all flights at end") + PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(!bGenerateFlightsAtEnd, "Ignore flights"));
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Transfer preferences");
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Generate transfer passenger from " + PrintBoolean(!bTransferArrivalGeneration, "Departure") + PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(bTransferArrivalGeneration, "Arrival"));

                string missedTransferBasedOn = "Missed Departure Transfer Based on ";
                if (missedDepartureTransferBasedOnCheckInShowUp)
                    missedTransferBasedOn += "Check-In Show-Up";
                else
                    missedTransferBasedOn += "ICT Show-Up";
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(true, missedTransferBasedOn));
                
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(bFillTransfer, "Fill transfer for all flights"));
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(fillDepartureTransferBasedOnCheckInShowUp, "Fill departure transfer based on the Check-In Show-Up"));
                bool bTransferTerminalDistribution = ((sTransfTerminalDistribution != "") && (sTransfTerminalDistribution != null));
                String sTmp = PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(bTransferTerminalDistribution, "Transfer terminal distribution");
                if (bTransferTerminalDistribution)
                    sTmp += " :" + PrintIndexation(bPrintPDF, false, false, 1) + sTransfTerminalDistribution;
                lsResult.Add(sTmp);

                bool bTransferFlightCategorie = ((sTransfFligthCategoryDistribution != "") && (sTransfFligthCategoryDistribution != null));
                sTmp = PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(bTransferFlightCategorie, "Transfer flight categories distribution");
                if (bTransferFlightCategorie)
                    sTmp += " :" + PrintIndexation(bPrintPDF, false, false, 1) + sTransfFligthCategoryDistribution;
                if (isTransferDistributionDeterministic)
                    sTmp += " :" + PrintIndexation(bPrintPDF, false, false, 1) + "Deterministic Transfer Distribution";
                lsResult.Add(sTmp);
            }
            if (bPaxSimulation)
            {
                lsResult.Add(" ");
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 1) + "Pax Simulation");
                lsResult.Add(" ");
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 2) + PrintIndexation(bPrintPDF, false, false, 1) + "Allocation & Plannings");
                bool bOpeningCI = ((sOpening_CITable != "") && (sOpening_CITable != null));
                String sTmp = PrintIndexation(bPrintPDF, false, true, 3) + PrintBoolean(bOpeningCI, "Use check-in opening");
                if (bOpeningCI)
                    sTmp += " :" + PrintIndexation(bPrintPDF, false, false, 1) + sOpening_CITable;
                lsResult.Add(sTmp);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Security allocation" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sSecurity);
                // << Task #7570 new Desk and extra information for Pax -Phase I B                
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "UserProcess allocation" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sUserProcess);
                // >> Task #7570 new Desk and extra information for Pax -Phase I B
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Transfer allocation" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sTransfer);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Passport allocation" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sPassport);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Saturation parameters" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sSaturation);
                


                lsResult.Add(" ");
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 2) + "Airport Process");
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + PrintBoolean(this.UseProcessSchedule, "Use process schedule table") + "\t\t" + PrintBoolean(!UseProcessSchedule, "Use regular tables"));
                if (this.UseProcessSchedule)
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Process Schedule table :" + PrintIndexation(bPrintPDF, false, false, 1) + ProcessSchedule);
                }
                else
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Process times" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sProcessTimes);
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Itinerary" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sItinerary);
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Capacity of group queues" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sGroupQueues);
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Capacity of queues" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sCapaQueues);
                    // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3)
                        + "Processing capacities :" + PrintIndexation(bPrintPDF, false, false, 1) + processCapacities);
                    // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
                }
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 2) + "Queues");
                // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation                
                //lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + PrintBoolean(!bFillQueue, "Use all opened desks") + PrintIndexation(bPrintPDF, false, false, 2) + PrintBoolean(!bFillQueue, "Fill the entire queue before using the next desk"));
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Station global filling type :" + PrintIndexation(bPrintPDF, false, false, 1) + _stationGlobalFillingType);
                // >> Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation

                if ((AnimatedQueues != null) && (AnimatedQueues != ""))
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + PrintBoolean(true, "Animated queues : " + sAnimatedQueues));
                }

                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 2) + "Probability" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sOneof);
            }/*
                else if (bBHSSimulation || bTMSSimulation)
                {
                    lsResult.Add(" ");
                    lsResult.Add(PrintIndetation(bPrintPDF,true,true,1) + "Simulation");
                }*/
            if (bBHSSimulation)
            {
                lsResult.Add(" ");
                lsResult.Add(PrintIndexation(bPrintPDF, true, true, 1) + "Baggage handling system");
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(bPaxplan, "PaxPlan flow") + PrintIndexation(bPrintPDF, false, false, 1) + PrintBoolean(!bPaxplan, "Mean flow"));
                if (BagPlan)
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(BagPlan, "BagPlan From scenario : ") + PrintIndexation(bPrintPDF, false, false, 1) + BagPlanScenarioName);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + sTerminal);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "General BHS information" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sGeneral);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Arrival infeed groups" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sArrivalInfeedGroups);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Check-In groups" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sCIGroups);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Transfer infeed groups" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sTransferInfeedGroups);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Check-In collectors" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sCICollectors);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Check-In routing" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sCIRouting);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "HBS3 routing" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sHBS3Routing);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Transfer routing" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sTransferRouting);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Flow split" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sFlowSplit);
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Process" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sProcess);
                if (bPaxplan)
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Arrival containers" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sArrivalContainers);
                else
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Arrival infeed mean flows" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sArrivalMeanFlows);
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Check-In mean flows" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sCheckInMeanFlows);
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Transfer infeed mean flows" + " :" + PrintIndexation(bPrintPDF, false, false, 1) + sTransferMeanFlows);
                }
            }
            if ((bPaxSimulation) || (bBHSSimulation) || (bTMSSimulation))
            {
                // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
                if (userAttributesTablesDictionary != null && userAttributesTablesDictionary.Count > 0)
                {
                    List<String> userAttributesList = getUserAttributesReview(bPrintPDF);
                    if (userAttributesList != null)
                        lsResult.AddRange(userAttributesList);
                }
                // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
                if ((pudUserData != null) && (bExportUserData))
                {
                    List<string> alUserData = pudUserData.getReview(bPrintPDF);
                    if (alUserData != null)
                        lsResult.AddRange(alUserData);
                }
                if (bRegeneratePaxplan)
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(bRegeneratePaxplan, "Regenerate PaxPlan"));
                }
                if ((sModelName != null) && (sModelName != "") && (sModelName != "Default"))
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, true, true, 2) + "Model : ");
                    if (bDisplayModel)
                        lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + PrintBoolean(bDisplayModel, "Display"));
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Name : " + sModelName);
                }
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Warm up " + dWarmUp + " hours");
                lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + PrintBoolean(bTraceSaveMode, "Copy output tables in project folder when saving."));
            }
            if (bSimAnalyserSimulation)
            {

                if (bUseSeed)                
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "Random seed " + iSeed.ToString());
                if (bUseUserProvidedSimulationEngineSeed)
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 2) + "MFF File Random seed " + mffFileSeed.ToString());   // >> Bug #14900 MFF file not created
                
                lsResult.Add(" ");
                if ((SimReporterTraceName != null) && (SimReporterTraceName != ""))
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, true, true, 2) + "Trace analysis : ");

                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Trace name :" + SimReporterTraceName);
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Warm up " + dWarmUp + " hours");
                }
                if (pudInputData != null)
                {
                    List<string> lsTmp = pudInputData.getReview(bPrintPDF);
                    if ((lsTmp != null) && (lsTmp.Count != 0))
                        lsResult.AddRange(lsTmp);
                }
                if ((sModelName != "") && (sModelName != "Default"))
                {
                    lsResult.Add(PrintIndexation(bPrintPDF, true, true, 2) + "Model : ");
                    if (bDisplayModel)
                        lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + PrintBoolean(bDisplayModel, "Display"));
                    lsResult.Add(PrintIndexation(bPrintPDF, false, true, 3) + "Name : " + sModelName);
                }

            }
            return lsResult;
        }
        internal static String PrintBoolean(bool bValue, String sText)
        {
            String sResult = "[";
            if (bValue)
                sResult += "X";
            else
                sResult += " ";
            sResult += "] " + sText;
            return sResult;
        }
        internal static string PrintIndexation(bool bPrintPDF, bool bPrintTick, bool bBeginOfLine, int iNumber)
        {
            string sResult = "";
            if (!bPrintPDF)
            {
                for (int i = 0; i < iNumber; i++)
                    sResult += "\t";
            }
            else
            {
                if (bBeginOfLine)
                {
                    sResult += iNumber.ToString();
                }
                else
                {
                    for (int i = 0; i < iNumber; i++)
                        sResult += "    ";
                }
                if (bPrintTick)
                    sResult += ((char)149).ToString();
            }
            return sResult;
        }

        internal void Dispose()
        {
            if (pudUserData != null)
                pudUserData.Dispose();
            pudUserData = null;
            if (ltcResult != null)
            {
                for (int i = 0; i < ltcResult.Count; )
                {
                    ltcResult[0].Dispose();
                    ltcResult.RemoveAt(0);
                }
                ltcResult.Clear();
                ltcResult = null;
            }

            if (pudInputData != null)
                pudInputData.Dispose();
            pudInputData = null;
        }

        // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab        
        internal List<String> getUserAttributesReview(bool bPrintPDF)
        {
            List<String> userAttributesList = new List<String>();
            userAttributesList.Add(ParamScenario.PrintIndexation(bPrintPDF, true, true, 1) + "User Attributes");
            foreach (String userAttribute in userAttributesTablesDictionary.Keys)
            {
                userAttributesList.Add(ParamScenario
                    .PrintIndexation(bPrintPDF, false, true, 2) + userAttribute + " : " 
                                        + userAttributesTablesDictionary[userAttribute].ToString());
            }
            return userAttributesList;
        }
        // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
    }
    #endregion

    #region public class ParamUserData

    /// <summary>
    /// Classe utilisée pour le paramétrage de la génération des tables UserData qui doivent être exportées pour la simulation.
    /// </summary>
    public class ParamUserData
    {
        /// <summary>
        /// Valeur par défaut placée dans la variable \ref sDataType. Dans le cas où cette classe est utilisée pour
        /// stockée des informations autres que les UserData, alors \ref sDataType prendra la valeur des données 
        /// qu'il représente.
        /// </summary>
        internal const string CONSTUserData = "User Data";
        /// <summary>
        /// Type de données stockées dans le \ref ParamUserData. Par défaut la valeur est initialisée à \ref CONSTUserData. 
        /// Cependant si \ref ParamUserData est utilisé pour représenter d'autres données, alors cette variable prendra le
        /// type qu'il représente (un nom permettant d'identifier ce type).
        /// </summary>
        internal String sDataType;
        /// <summary>
        /// Dictionnaire des paramétrage représenté par ce \ref ParamUserData. Il s'agit d'un ensemble de clef, valeur ayant les propriétés
        /// suivantes : - Clef : Nom du UserData qui doit être exporté.  - Valeur : nom de la table qui doit être exportée pour ce UserData.
        /// </summary>
        private Dictionary<String, String> _htUserData;
        /// <summary>
        /// Dictionnaire des paramétrage représenté par ce \ref ParamUserData. Il s'agit d'un ensemble de clef, valeur ayant les propriétés
        /// suivantes : - Clef : Nom du UserData qui doit être exporté.  - Valeur : nom de la table qui doit être exportée pour ce UserData.
        /// 
        /// </summary>
        public Dictionary<String, String> UserData
        {
            get
            {
                return _htUserData;
            }
            set
            {
                _htUserData = value;
            }
        }
        /// <summary>
        /// Constructeur de \ref ParamUserData qui prend en argument un dictionnaire qui sera ensuite copié dans \ref _htUserData.
        /// </summary>
        /// <param name="htUserData">Paramétrage du UserData</param>
        public ParamUserData(Dictionary<String, String> htUserData)
        {
            sDataType = CONSTUserData;
            _htUserData = htUserData;
        }

        /// <summary>
        /// Constructeur de \ref ParamUserData qui prend en argument le type de données représenté par ce \ref ParamUserData et 
        /// un dictionnaire qui sera ensuite copié dans \ref _htUserData.
        /// </summary>
        /// <param name="DataType">Description du type de données qui sera stocké dans ce UserData</param>
        /// <param name="htUserData">Paramétrage du UserData</param>
        public ParamUserData(String DataType, Dictionary<String, String> htUserData)
        {
            sDataType = DataType;
            _htUserData = htUserData;
        }

        /// <summary>
        /// Constructeur de \ref ParamUserData qui, à partir d'un noeud XML, charge les informations sur ce UserData
        /// </summary>
        /// <param name="Parametres">Noeud XML représentant un \ref ParamUserData</param>
        /// <param name="sVersion">Version dans laquelle le \ref ParamUserData avait été enregistré.</param>
        public ParamUserData(XmlNode Parametres, VersionManager sVersion)
        {
            ///Initialisation des variables de la classe.
            _htUserData = new Dictionary<String, String>();
            sDataType = CONSTUserData;
            ///Si un attribut "DataType" existe, alors \ref sDataType est initialisé avec le contenu de cet attribut.
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "DataType"))
                sDataType = Parametres.Attributes["DataType"].Value;
            ///Pour chaque noeud enfant, il faut ajouter une entrée dans \ref _htUserData.
            foreach (XmlNode xnChild in Parametres.ChildNodes)
            {
                String sName = xnChild.Name;
                if (OverallTools.FonctionUtiles.hasNamedAttribute(xnChild, "UserName"))
                    sName = xnChild.Attributes["UserName"].Value;
                String sValue = xnChild.FirstChild.Value.ToString();
                if (!_htUserData.ContainsKey(sName))
                    _htUserData.Add(sName, sValue);
            }
        }

        /// <summary>
        /// Fonction qui permet de mettre à jour la clef \ref UserData à la valeur \ref Value. Si la clef n'existait pas dans le UserData,
        /// alors cette clef est ajoutée.
        /// </summary>
        /// <param name="UserData">Clef à mettre à jour</param>
        /// <param name="Value">Valeur à mettre dans la clef</param>
        public void setUserData(String UserData, String Value)
        {
            if (_htUserData == null)
                _htUserData = new Dictionary<String, String>();
            if (_htUserData.ContainsKey(UserData))
                _htUserData[UserData] = Value;
            else
                _htUserData.Add(UserData, Value);
        }

        /// <summary>
        /// Fonction qui permet de récupérer le paramétrage représenté par ce \ref ParamUserData pour l'objet passé en paramètre.
        /// </summary>
        /// <param name="UserData">Nom recherché dans le UserData.</param>
        /// <returns>Valeur associée au nom passé en paramètre. NULL si aucune valeur correpondant à la clef n'est présent dans ce \ref ParamUserData.</returns>
        public String getUserData(String UserData)
        {
            if (_htUserData == null)
                return null;
            if (_htUserData.ContainsKey(UserData))
                return (String)_htUserData[UserData];
            return null;
        }

        /// <summary>
        /// Fonction qui permet de sauvegarder le \ref ParamUserData sous forme XML.
        /// </summary>
        /// <param name="projet">Projet auquel tous les noeuds Xml doivent être rattachés.</param>
        /// <returns>Noeud xml représentant ce \ref ParamUserData, ou NULL si un problème est survenue.</returns>
        public XmlNode getParams(XmlDocument projet)
        {
            ///Création et exportation de la totalité des informations présentes dans ce \ref ParamUserData.
            if (projet == null)
                return null;
            if (_htUserData == null)
                return null;
            XmlElement result = projet.CreateElement("UserData");
            if (sDataType != CONSTUserData)
            {
                result.SetAttribute("DataType", sDataType);
            }
            foreach (String sKey in _htUserData.Keys)
            {
                string sName = sKey;
                sName = sName.Replace(" ", "_");
                sName = sName.Replace(".", "_");
                XmlElement xnTmp = OverallTools.FonctionUtiles.CreateElement(projet, sName, (String)_htUserData[sKey]);
                xnTmp.SetAttribute("UserName", sKey);
                result.AppendChild(xnTmp);
            }
            return result;
        }

        /// <summary>
        /// Fonction surchargée pour l'utilisation dans l'exportation PDF.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Fonction permettant de généré un aperç du paramétrage sous forme de texte ou sous forme de PDF.
        /// </summary>
        /// <param name="bPrintPDF">Booléen indiquant si l'exportation est destinée </param>
        /// <returns>Liste de lignes qui représente le résumé pour ce \ref ParamUserData</returns>
        internal List<string> getReview(bool bPrintPDF)
        {
            if (_htUserData.Count == 0)
                return null;
            List<string> alUserDataResult = new List<string>();
            alUserDataResult.Add(ParamScenario.PrintIndexation(bPrintPDF, true, true, 1) + sDataType);
            foreach (String sKey in _htUserData.Keys)
            {
                alUserDataResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + sKey + " : " + _htUserData[sKey].ToString());
            }
            return alUserDataResult;
        }

        /// <summary>
        /// Fonction qui permet de rendre la mémoire allouée pour ce \ref ParamUserData.
        /// </summary>
        internal void Dispose()
        {
            _htUserData.Clear();
            _htUserData = null;
        }
    }
    #endregion

    #region OldClasses
    #region public class ParamDeparturePeak
    /// <summary>
    /// \Deprecated
    /// Ancienne classe utilisée pour le paramétrage de l'analyse statique de peak départ.
    /// </summary>
    public class ParamDeparturePeak
    {

        #region Les différentes informations stockées dans cette classe.
        private DateTime dtDateDebut;
        private DateTime dtDateFin;
        private String sFPD;
        private String sLoadFactors;
        private String sAircraftType;
        private String sOCT_CI_Table;
        private String sOCT_MakeUp;
        private String sCI_ShowUpTable;
        private String sNbBags;
        private String sNbVisitors;
        private String sNbTrolley;
        private String sFlightCategories;
        private Double dStepAnalysis;
        private Double dAnalysisRange;
        private bool bSlidingHour;
        #endregion
        #region Les accesseurs aux différents éléments de la classe.
        public DateTime DateDebut
        {
            get
            {
                return dtDateDebut;
            }
            set
            {
                dtDateDebut = value;
            }
        }
        public DateTime DateFin
        {
            get
            {
                return dtDateFin;
            }
            set
            {
                dtDateFin = value;
            }
        }
        public String FPD
        {
            get
            {
                return sFPD;
            }
            set
            {
                sFPD = value;
            }
        }
        public String LoadFactors
        {
            get
            {
                return sLoadFactors;
            }
            set
            {
                sLoadFactors = value;
            }
        }

        public String AircraftType
        {
            get
            {
                return sAircraftType;
            }
            set
            {
                sAircraftType = value;
            }
        }
        public String OCT_CI_Table
        {
            get
            {
                return sOCT_CI_Table;
            }
            set
            {
                sOCT_CI_Table = value;
            }
        }
        public String OCT_MakeUp
        {
            get
            {
                return sOCT_MakeUp;
            }
            set
            {
                sOCT_MakeUp = value;
            }
        }
        public String CI_ShowUpTable
        {
            get
            {
                return sCI_ShowUpTable;
            }
            set
            {
                sCI_ShowUpTable = value;
            }
        }
        public String NbBags
        {
            get
            {
                return sNbBags;
            }
            set
            {
                sNbBags = value;
            }
        }
        public String NbVisitors
        {
            get
            {
                return sNbVisitors;
            }
            set
            {
                sNbVisitors = value;
            }
        }
        public String NbTrolley
        {
            get
            {
                return sNbTrolley;
            }
            set
            {
                sNbTrolley = value;
            }
        }
        public String FlightCategories
        {
            get
            {
                return sFlightCategories;
            }
            set
            {
                sFlightCategories = value;
            }
        }
        public Double StepAnalysis
        {
            get
            {
                return dStepAnalysis;
            }
            set
            {
                dStepAnalysis = value;
            }
        }
        public Double AnalysisRange
        {
            get
            {
                return dAnalysisRange;
            }
            set
            {
                dAnalysisRange = value;
            }
        }

        public bool SlidingHour
        {
            get
            {
                return bSlidingHour;
            }
            set
            {
                bSlidingHour = value;
            }
        }
        #endregion
        public ParamDeparturePeak(DateTime dtDateDebut_,
         DateTime dtDateFin_,
         String sFPD_,
         String sLoadFactors_,
         String sAircraftType_,
         String sOCT_CI_Table_,
         String sOCT_MakeUp_,
         String sCI_ShowUpTable_,
         String sNbBags_,
         String sNbVisitors_,
         String sNbTrolley_,
         String sFlightCategories_,
         Double dStepAnalysis_,
         Double dAnalysisRange_,
         bool bSlidingHour_)
        {
            dtDateDebut = dtDateDebut_;
            dtDateFin = dtDateFin_;
            sFPD = sFPD_;
            sLoadFactors = sLoadFactors_;
            sAircraftType = sAircraftType_;
            sOCT_CI_Table = sOCT_CI_Table_;
            sOCT_MakeUp = sOCT_MakeUp_;
            sCI_ShowUpTable = sCI_ShowUpTable_;
            sNbBags = sNbBags_;
            sNbVisitors = sNbVisitors_;
            sNbTrolley = sNbTrolley_;
            sFlightCategories = sFlightCategories_;
            dStepAnalysis = dStepAnalysis_;
            dAnalysisRange = dAnalysisRange_;
            bSlidingHour = bSlidingHour_;
        }

        public ParamDeparturePeak(XmlNode Parametres, VersionManager sVersion)
        {
            if ((!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Start")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "End")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FPD")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AircraftType")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FlightCategories")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "LoadFactors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.CI_ShowUpTableName)) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_CI_Table")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbBags")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbVisitors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbVisitors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbTrolley")))
            {
                return;
            }
            DateTime.TryParse(Parametres["Start"].FirstChild.Value, out dtDateDebut);
            DateTime.TryParse(Parametres["End"].FirstChild.Value, out dtDateFin);
            sFPD = Parametres["FPD"].FirstChild.Value;
            sAircraftType = Parametres["AircraftType"].FirstChild.Value;
            sFlightCategories = Parametres["FlightCategories"].FirstChild.Value;
            sLoadFactors = Parametres["LoadFactors"].FirstChild.Value;
            sCI_ShowUpTable = Parametres[GlobalNames.CI_ShowUpTableName].FirstChild.Value;
            sOCT_CI_Table = Parametres["OCT_CI_Table"].FirstChild.Value;
            sNbBags = Parametres["NbBags"].FirstChild.Value;
            sNbTrolley = Parametres["NbTrolley"].FirstChild.Value;
            sNbVisitors = Parametres["NbVisitors"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_MakeUp"))
                sOCT_MakeUp = Parametres["OCT_MakeUp"].FirstChild.Value;
            else
                sOCT_MakeUp = "OCT_MakeUp";
            dStepAnalysis = 5;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "StepAnalysis"))
                Double.TryParse(Parametres["StepAnalysis"].FirstChild.Value, out dStepAnalysis);
            dAnalysisRange = 15;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AnalysisRange"))
                Double.TryParse(Parametres["AnalysisRange"].FirstChild.Value, out dAnalysisRange);

            bSlidingHour = true;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "SlidingHour"))
                Boolean.TryParse(Parametres["SlidingHour"].FirstChild.Value, out bSlidingHour);
        }
        public XmlNode getParams(XmlDocument projet)
        {
            XmlNode Result = projet.CreateElement("ParamDeparturePeak");
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Start", dtDateDebut.ToString()));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "End", dtDateFin.ToString()));

            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FPD", sFPD));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "AircraftType", sAircraftType));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FlightCategories", sFlightCategories));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "LoadFactors", sLoadFactors));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, GlobalNames.CI_ShowUpTableName, sCI_ShowUpTable));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "OCT_CI_Table", sOCT_CI_Table));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "OCT_MakeUp", sOCT_MakeUp));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbBags", sNbBags));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbVisitors", sNbVisitors));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbTrolley", sNbTrolley));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "StepAnalysis", dStepAnalysis.ToString()));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "AnalysisRange", dAnalysisRange.ToString()));
            //Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "SlidingHour", bSlidingHour.ToString()));
            return Result;
        }
    }
    #endregion
    #region public class ParamArrivalPeak
    /// <summary>
    /// \Deprecated
    /// Ancienne classe utilisée pour le paramétrage de l'analyse statique de peak arrivée.
    /// </summary>
    public class ParamArrivalPeak
    {

        #region Les différentes informations stockées dans cette classe.
        private DateTime dtDateDebut;
        private DateTime dtDateFin;
        private String sFPA;
        private String sLoadFactors;
        private String sAircraftType;
        private String sICT_Table;
        private String sNbBags;
        private String sNbVisitors;
        private String sNbTrolley;
        private String sFlightCategories;
        private Double dStepAnalysis;
        private Double dAnalysisRange;
        private bool bSlidingHour;
        #endregion
        #region Les accesseurs aux différents éléments de la classe.
        public DateTime DateDebut
        {
            get
            {
                return dtDateDebut;
            }
            set
            {
                dtDateDebut = value;
            }
        }
        public DateTime DateFin
        {
            get
            {
                return dtDateFin;
            }
            set
            {
                dtDateFin = value;
            }
        }
        public String FPA
        {
            get
            {
                return sFPA;
            }
            set
            {
                sFPA = value;
            }
        }
        public String LoadFactors
        {
            get
            {
                return sLoadFactors;
            }
            set
            {
                sLoadFactors = value;
            }
        }

        public String AircraftType
        {
            get
            {
                return sAircraftType;
            }
            set
            {
                sAircraftType = value;
            }
        }
        public String ICT_Table
        {
            get
            {
                return sICT_Table;
            }
            set
            {
                sICT_Table = value;
            }
        }
        public String NbBags
        {
            get
            {
                return sNbBags;
            }
            set
            {
                sNbBags = value;
            }
        }
        public String NbVisitors
        {
            get
            {
                return sNbVisitors;
            }
            set
            {
                sNbVisitors = value;
            }
        }
        public String NbTrolley
        {
            get
            {
                return sNbTrolley;
            }
            set
            {
                sNbTrolley = value;
            }
        }
        public String FlightCategories
        {
            get
            {
                return sFlightCategories;
            }
            set
            {
                sFlightCategories = value;
            }
        }
        public Double StepAnalysis
        {
            get
            {
                return dStepAnalysis;
            }
            set
            {
                dStepAnalysis = value;
            }
        }
        public Double AnalysisRange
        {
            get
            {
                return dAnalysisRange;
            }
            set
            {
                dAnalysisRange = value;
            }
        }
        public bool SlidingHour
        {
            get
            {
                return bSlidingHour;
            }
            set
            {
                bSlidingHour = value;
            }
        }
        #endregion
        public ParamArrivalPeak(DateTime dtDateDebut_,
         DateTime dtDateFin_,
         String sFPA_,
         String sLoadFactors_,
         String sAircraftType_,
         String sICT_Table_,
         String sNbBags_,
         String sNbVisitors_,
         String sNbTrolley_,
         String sFlightCategories_,
         Double dStepAnalysis_,
         Double dAnalysisRange_,
         bool bSlidingHour_)
        {
            dtDateDebut = dtDateDebut_;
            dtDateFin = dtDateFin_;
            sFPA = sFPA_;
            sLoadFactors = sLoadFactors_;
            sAircraftType = sAircraftType_;
            sICT_Table = sICT_Table_;
            sNbBags = sNbBags_;
            sNbVisitors = sNbVisitors_;
            sNbTrolley = sNbTrolley_;
            sFlightCategories = sFlightCategories_;
            dStepAnalysis = dStepAnalysis_;
            dAnalysisRange = dAnalysisRange_;
            bSlidingHour = bSlidingHour_;
        }

        public ParamArrivalPeak(XmlNode Parametres, VersionManager sVersion)
        {
            if ((!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Start")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "End")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FPA")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AircraftType")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FlightCategories")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "LoadFactors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ICT_Table")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbBags")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbVisitors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbVisitors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbTrolley")))
            {
                return;
            }
            DateTime.TryParse(Parametres["Start"].FirstChild.Value, out dtDateDebut);
            DateTime.TryParse(Parametres["End"].FirstChild.Value, out dtDateFin);
            sFPA = Parametres["FPA"].FirstChild.Value;
            sAircraftType = Parametres["AircraftType"].FirstChild.Value;
            sFlightCategories = Parametres["FlightCategories"].FirstChild.Value;
            sLoadFactors = Parametres["LoadFactors"].FirstChild.Value;
            sICT_Table = Parametres["ICT_Table"].FirstChild.Value;
            sNbBags = Parametres["NbBags"].FirstChild.Value;
            sNbTrolley = Parametres["NbTrolley"].FirstChild.Value;
            sNbVisitors = Parametres["NbVisitors"].FirstChild.Value;
            dStepAnalysis = 5.0;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "StepAnalysis"))
                Double.TryParse(Parametres["StepAnalysis"].FirstChild.Value, out dStepAnalysis);
            dAnalysisRange = 15;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AnalysisRange"))
                Double.TryParse(Parametres["AnalysisRange"].FirstChild.Value, out dAnalysisRange);
            bSlidingHour = true;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "SlidingHour"))
                Boolean.TryParse(Parametres["SlidingHour"].FirstChild.Value, out bSlidingHour);

        }
        public XmlNode getParams(XmlDocument projet)
        {
            XmlNode Result = projet.CreateElement("ParamArrivalPeak");
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Start", dtDateDebut.ToString()));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "End", dtDateFin.ToString()));

            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FPA", sFPA));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "AircraftType", sAircraftType));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FlightCategories", sFlightCategories));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "LoadFactors", sLoadFactors));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "ICT_Table", sICT_Table));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbBags", sNbBags));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbVisitors", sNbVisitors));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbTrolley", sNbTrolley));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "StepAnalysis", dStepAnalysis.ToString()));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "AnalysisRange", dAnalysisRange.ToString()));
            //                Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "SlidingHour", bSlidingHour.ToString()));
            return Result;
        }
    }
    #endregion
    #region public class ParamPaxPlan
    /// <summary>
    /// \Deprecated
    /// Ancienne classe utilisée pour le paramétrage de la génération du PaxPlan.
    /// </summary>
    public class ParamPaxPlan
    {

        #region Les différentes informations stockées dans cette classe.
        private DateTime dtDateDebut;
        private DateTime dtDateFin;

        public String sFPD;
        private String sFPA;

        private String sAircraftType;
        private String sFlightCategories;

        private String sDLoadFactors;
        private String sALoadFactors;

        private String sICT_Table;
        private String sCI_ShowUpTable;

        private String sNbBags;
        private String sNbVisitors;
        private String sNbTrolley;

        private String sTransfTerminalDistribution;

        private int iSeed;
        private bool bUseSeed;
        private bool bTransferArrivalGeneration;
        private bool bFillTransfer;
        private bool bGenerateAllWarmUp;
        private bool bGenerateFlightsAtEnd;
        private bool bFillQueue;
        private bool bSlidingHour;
        #endregion

        #region Les accesseurs aux différents éléments de la classe.
        public DateTime DateDebut
        {
            get
            {
                return dtDateDebut;
            }
            set
            {
                dtDateDebut = value;
            }
        }
        public DateTime DateFin
        {
            get
            {
                return dtDateFin;
            }
            set
            {
                dtDateFin = value;
            }
        }

        public String FPD
        {
            get
            {
                return sFPD;
            }
            set
            {
                sFPD = value;
            }
        }
        public String FPA
        {
            get
            {
                return sFPA;
            }
            set
            {
                sFPA = value;
            }
        }


        public String AircraftType
        {
            get
            {
                return sAircraftType;
            }
            set
            {
                sAircraftType = value;
            }
        }
        public String FlightCategories
        {
            get
            {
                return sFlightCategories;
            }
            set
            {
                sFlightCategories = value;
            }
        }

        public String DLoadFactors
        {
            get
            {
                return sDLoadFactors;
            }
            set
            {
                sDLoadFactors = value;
            }
        }
        public String ALoadFactors
        {
            get
            {
                return sALoadFactors;
            }
            set
            {
                sALoadFactors = value;
            }
        }

        public String ICT_Table
        {
            get
            {
                return sICT_Table;
            }
            set
            {
                sICT_Table = value;
            }
        }
        public String CI_ShowUpTable
        {
            get
            {
                return sCI_ShowUpTable;
            }
            set
            {
                sCI_ShowUpTable = value;
            }
        }

        public String NbBags
        {
            get
            {
                return sNbBags;
            }
            set
            {
                sNbBags = value;
            }
        }
        public String NbVisitors
        {
            get
            {
                return sNbVisitors;
            }
            set
            {
                sNbVisitors = value;
            }
        }
        public String NbTrolley
        {
            get
            {
                return sNbTrolley;
            }
            set
            {
                sNbTrolley = value;
            }
        }

        public String TransfTerminalDistribution
        {
            get
            {
                return sTransfTerminalDistribution;
            }
            set
            {
                sTransfTerminalDistribution = value;
            }
        }

        public int Seed
        {
            get
            {
                return iSeed;
            }
            set
            {
                iSeed = value;
            }
        }
        public bool UseSeed
        {
            get
            {
                return bUseSeed;
            }
            set
            {
                bUseSeed = value;
            }
        }
        public bool TransferArrivalGeneration
        {
            get
            {
                return bTransferArrivalGeneration;
            }
            set
            {
                bTransferArrivalGeneration = value;
            }
        }
        public bool FillTransfer
        {
            get
            {
                return bFillTransfer;
            }
            set
            {
                bFillTransfer = value;
            }
        }
        public bool GenerateAll
        {
            get
            {
                return bGenerateAllWarmUp;
            }
            set
            {
                bGenerateAllWarmUp = value;
            }
        }
        public bool GenerateFlightsAtEnd
        {
            get
            {
                return bGenerateFlightsAtEnd;
            }
            set
            {
                bGenerateFlightsAtEnd = value;
            }
        }
        public bool SlidingHour
        {
            get
            {
                return bSlidingHour;
            }
        }


        public bool FillQueue
        {
            get
            {
                return bFillQueue;
            }
            set
            {
                bFillQueue = value;
            }
        }
        #endregion
        public ParamPaxPlan(DateTime DateDebut_,
            DateTime DateFin_,
            String FPD_,
            String FPA_,
            String sTransfTerminalDistribution_,
            String AircraftType_,
            String FlightCategories_,
            String DLoadFactors_,
            String ALoadFactors_,
            String ICT_Table_,
            String CI_ShowUpTable_,
            String NbBags_,
            String NbVisitors_,
            String NbTrolley_,
            int Seed_,
            bool UseSeed_,
            bool GenerateAll_,
            bool FillQueue_,
            bool bGenerateFlightsAtEnd_,
            bool bTransferArrivalGeneration_,
            bool bFillTransfer_,
            bool bSlidingHour_)
        {
            dtDateDebut = DateDebut_;
            dtDateFin = DateFin_;
            sFPD = FPD_;
            sFPA = FPA_;

            sAircraftType = AircraftType_;
            sFlightCategories = FlightCategories_;

            sDLoadFactors = DLoadFactors_;
            sALoadFactors = ALoadFactors_;

            sICT_Table = ICT_Table_;
            sCI_ShowUpTable = CI_ShowUpTable_;

            sNbBags = NbBags_;
            sNbVisitors = NbVisitors_;
            sNbTrolley = NbTrolley_;
            iSeed = Seed_;
            bUseSeed = UseSeed_;

            bTransferArrivalGeneration = bTransferArrivalGeneration_;
            bFillTransfer = bFillTransfer_;

            bGenerateAllWarmUp = GenerateAll_;
            bFillQueue = FillQueue_;
            bSlidingHour = bSlidingHour_;
            bGenerateFlightsAtEnd = bGenerateFlightsAtEnd_;
            sTransfTerminalDistribution = sTransfTerminalDistribution_;
        }
        public ParamPaxPlan(XmlNode Parametres, VersionManager sVersion)
        {
            if ((!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Start")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "End")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FPA")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FPD")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AircraftType")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FlightCategories")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "DLoadFactors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ALoadFactors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ICT_Table")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, GlobalNames.CI_ShowUpTableName)) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbBags")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Seed_Generate")) ||
                (!OverallTools.FonctionUtiles.hasNamedAttribute(Parametres["Seed_Generate"], "UseSeed")) ||
                (!OverallTools.FonctionUtiles.hasNamedAttribute(Parametres["Seed_Generate"], "Seed")) ||
                (!OverallTools.FonctionUtiles.hasNamedAttribute(Parametres["Seed_Generate"], "GenerateAll")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbTrolley")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FillQueues")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "NbVisitors")))
            {
                return;
            }
            DateTime.TryParse(Parametres["Start"].FirstChild.Value, out dtDateDebut);
            DateTime.TryParse(Parametres["End"].FirstChild.Value, out dtDateFin);
            sFPA = Parametres["FPA"].FirstChild.Value;
            sFPD = Parametres["FPD"].FirstChild.Value;
            sAircraftType = Parametres["AircraftType"].FirstChild.Value;
            sFlightCategories = Parametres["FlightCategories"].FirstChild.Value;
            sALoadFactors = Parametres["ALoadFactors"].FirstChild.Value;
            sDLoadFactors = Parametres["DLoadFactors"].FirstChild.Value;
            sICT_Table = Parametres["ICT_Table"].FirstChild.Value;
            sCI_ShowUpTable = Parametres[GlobalNames.CI_ShowUpTableName].FirstChild.Value;
            sNbBags = Parametres["NbBags"].FirstChild.Value;
            sNbTrolley = Parametres["NbTrolley"].FirstChild.Value;
            sNbVisitors = Parametres["NbVisitors"].FirstChild.Value;
            sTransfTerminalDistribution = "";
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferTerminalDistribution"))
                sTransfTerminalDistribution = Parametres["TransferTerminalDistribution"].FirstChild.Value;

            bTransferArrivalGeneration = true;
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "TransferArrivalGeneration"))
                Boolean.TryParse(Parametres.Attributes["TransferArrivalGeneration"].Value, out bTransferArrivalGeneration);
            FillTransfer = false;
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "FillTransfer"))
                Boolean.TryParse(Parametres.Attributes["FillTransfer"].Value, out bFillTransfer);
            bSlidingHour = true;
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "SlidingHour"))
                Boolean.TryParse(Parametres.Attributes["SlidingHour"].Value, out bSlidingHour);
            sNbVisitors = Parametres["NbVisitors"].FirstChild.Value;

            Boolean.TryParse(Parametres["FillQueues"].Attributes["Value"].Value, out bFillQueue);
            Boolean.TryParse(Parametres["Seed_Generate"].Attributes["UseSeed"].Value, out bUseSeed);
            Int32.TryParse(Parametres["Seed_Generate"].Attributes["Seed"].Value, out iSeed);
            Boolean.TryParse(Parametres["Seed_Generate"].Attributes["GenerateAll"].Value, out bGenerateAllWarmUp);
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "GenerateFlightsAtEnd"))
                Boolean.TryParse(Parametres["GenerateFlightsAtEnd"].Value, out bGenerateFlightsAtEnd);
            else
                bGenerateFlightsAtEnd = true;



        }
        public XmlNode getParams(XmlDocument projet)
        {
            XmlNode Result = projet.CreateElement("ParamPaxPlan");
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Start", dtDateDebut.ToString()));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "End", dtDateFin.ToString()));

            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FPD", sFPD));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FPA", sFPA));
            if ((sTransfTerminalDistribution != null) && (sTransfTerminalDistribution != ""))
                Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "TransferTerminalDistribution", sTransfTerminalDistribution));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "AircraftType", sAircraftType));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FlightCategories", sFlightCategories));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "DLoadFactors", sDLoadFactors));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "ALoadFactors", sALoadFactors));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "ICT_Table", sICT_Table));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, GlobalNames.CI_ShowUpTableName, sCI_ShowUpTable));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbBags", sNbBags));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbVisitors", sNbVisitors));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "NbTrolley", sNbTrolley));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "GenerateFlightsAtEnd", bGenerateFlightsAtEnd.ToString()));
            ((XmlElement)Result).SetAttribute("TransferArrivalGeneration", bTransferArrivalGeneration.ToString());
            ((XmlElement)Result).SetAttribute("FillTransfer", bFillTransfer.ToString());
            if (!bSlidingHour)
            {
                ((XmlElement)Result).SetAttribute("SlidingHour", bSlidingHour.ToString());
            }



            XmlElement tmp = projet.CreateElement("Seed_Generate");
            tmp.SetAttribute("UseSeed", bUseSeed.ToString());
            tmp.SetAttribute("Seed", iSeed.ToString());

            tmp.SetAttribute("GenerateAll", bGenerateAllWarmUp.ToString());
            Result.AppendChild(tmp);

            tmp = projet.CreateElement("FillQueues");
            tmp.SetAttribute("Value", bFillQueue.ToString());
            Result.AppendChild(tmp);

            return Result;
        }
    }
    #endregion
    #region public class ParamAutomod

    /// <summary>
    /// \Deprecated
    /// Ancienne classe utilisée pour le paramétrage de la génération des tables à exporter vers Automod pour la simulation Passager.
    /// </summary>
    public class ParamAutomod
    {

        #region Les différentes informations stockées dans cette classe.
        private String sOCT_CI_Table;
        private String sOCT_BC;
        private String sOCT_BG;
        private String sProcessTimes;
        private String sSecurity;
        // << Task #7570 new Desk and extra information for Pax -Phase I B
        private String sUserProcess;
        // >> Task #7570 new Desk and extra information for Pax -Phase I B
        private String sTransfer;
        private String sPassport;
        private String sItinerary;
        private String sOneof;
        private String sOpening_CITable;
        private String sCapaQueues;
        #endregion

        #region Les accesseurs aux différents éléments de la classe.
        public String OCT_CI_Table
        {
            get
            {
                return sOCT_CI_Table;
            }
            set
            {
                sOCT_CI_Table = value;
            }
        }
        public String OCT_BC
        {
            get
            {
                return sOCT_BC;
            }
            set
            {
                sOCT_BC = value;
            }
        }
        public String OCT_BG
        {
            get
            {
                return sOCT_BG;
            }
            set
            {
                sOCT_BG = value;
            }
        }

        public String ProcessTimes
        {
            get
            {
                return sProcessTimes;
            }
            set
            {
                sProcessTimes = value;
            }
        }
        public String Security
        {
            get
            {
                return sSecurity;
            }
            set
            {
                sSecurity = value;
            }
        }
        // << Task #7570 new Desk and extra information for Pax -Phase I B
        public String UserProcess
        {
            get
            {
                return sUserProcess;
            }
            set
            {
                sUserProcess = value;
            }
        }
        // >> Task #7570 new Desk and extra information for Pax -Phase I B
        public String Transfer
        {
            get
            {
                return sTransfer;
            }
            set
            {
                sTransfer = value;
            }
        }
        public String Passport
        {
            get
            {
                return sPassport;
            }
            set
            {
                sPassport = value;
            }
        }
        public String Itinerary
        {
            get
            {
                return sItinerary;
            }
            set
            {
                sItinerary = value;
            }
        }
        public String Oneof
        {
            get
            {
                return sOneof;
            }
            set
            {
                sOneof = value;
            }
        }
        public String Opening_CITable
        {
            get
            {
                return sOpening_CITable;
            }
            set
            {
                sOpening_CITable = value;
            }
        }

        public String CapaQueues
        {
            get
            {
                return sCapaQueues;
            }
            set
            {
                sCapaQueues = value;
            }
        }
        #endregion

        public ParamAutomod(String OCT_CI_Table_,
            String OCT_BC_,
            String OCT_BG_,
            String ProcessTimes_,
            String Security_,
            String Transfer_,
            String Passport_,
            String Itinerary_,
            String sOneof_,
            String Opening_CITable_,
            String CapaQueues_)
        {
            sOCT_CI_Table = OCT_CI_Table_;
            sOCT_BC = OCT_BC_;
            sOCT_BG = OCT_BG_;
            sProcessTimes = ProcessTimes_;
            sSecurity = Security_;
            sTransfer = Transfer_;
            sPassport = Passport_;
            sItinerary = Itinerary_;
            sOneof = sOneof_;
            sOpening_CITable = Opening_CITable_;
            //sFromTo = FromTo_;
            sCapaQueues = CapaQueues_;
        }
        public ParamAutomod(XmlNode Parametres, VersionManager sVersion)
        {
            if ((!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_CI")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_Baggage_Claim")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_Boarding_Gate")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ProcessTimes")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Security")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Transfer")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Passport")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Itinerary")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CapaQueues")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Oneof")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Opening_CI")))
            {
                return;
            }

            sOCT_CI_Table = Parametres["OCT_CI"].FirstChild.Value;
            sOCT_BC = Parametres["OCT_Baggage_Claim"].FirstChild.Value;
            sOCT_BG = Parametres["OCT_Boarding_Gate"].FirstChild.Value;
            sProcessTimes = Parametres["ProcessTimes"].FirstChild.Value;
            sSecurity = Parametres["Security"].FirstChild.Value;
            // << Task #7570 new Desk and extra information for Pax -Phase I B
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "UserPorcess"))
                sUserProcess = Parametres["UserProcess"].FirstChild.Value;
            // >> Task #7570 new Desk and extra information for Pax -Phase I B
            sTransfer = Parametres["Transfer"].FirstChild.Value;
            sPassport = Parametres["Passport"].FirstChild.Value;
            sItinerary = Parametres["Itinerary"].FirstChild.Value;
            sOneof = Parametres["Oneof"].FirstChild.Value;
            if (Parametres["Opening_CI"].FirstChild == null)
            {
                sOpening_CITable = "";
            }
            else
            {
                sOpening_CITable = Parametres["Opening_CI"].FirstChild.Value;
            }

            sCapaQueues = Parametres["CapaQueues"].FirstChild.Value;
        }
        public XmlNode getParams(XmlDocument projet)
        {
            XmlNode Result = projet.CreateElement("ParamAutomod");
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "OCT_CI", sOCT_CI_Table));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "OCT_Baggage_Claim", sOCT_BC));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "OCT_Boarding_Gate", sOCT_BG));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "ProcessTimes", sProcessTimes));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Security", sSecurity));
            // << Task #7570 new Desk and extra information for Pax -Phase I B
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "UserProcess", sUserProcess));
            // >> Task #7570 new Desk and extra information for Pax -Phase I B
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Transfer", sTransfer));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Passport", sPassport));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Itinerary", sItinerary));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Oneof", sOneof));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Opening_CI", sOpening_CITable));

            //Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FromTo", sFromTo));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "CapaQueues", sCapaQueues));
            return Result;
        }
    }
    #endregion
    #region public class ParamBHS
    /// <summary>
    /// \Deprecated
    /// Ancienne classe utilisée pour le paramétrage de la génération des tables à exporter vers Automod pour la simulation BHS.
    /// </summary>
    public class ParamBHS
    {

        #region Les différentes informations stockées dans cette classe.
        private bool bPaxPlan;
        private String sTerminal;
        private String sGeneral;
        private String sArrivalInfeedGroups;
        private String sCIGroups;
        private String sTransferInfeedGroups;

        private String sCICollectors;
        private String sCIRouting;
        private String sTransferRouting;
        private String sOCT_MakeUp;
        private String sFlowSplit;
        private String sProcess;
        private String sArrivalContainers;
        private String sArrivalMeanFlows;
        private String sCheckInMeanFlows;
        private String sTransferMeanFlows;

        #endregion
        #region Les accesseurs aux différents éléments de la classe.
        public bool UsePaxPlan
        {
            get
            {
                return bPaxPlan;
            }
            set
            {
                bPaxPlan = value;
            }
        }
        public int iTerminal
        {
            get
            {
                int iTerminal = 0;
                String sTerminal_ = Terminal.Replace("Terminal", "");
                if (!Int32.TryParse(sTerminal_, out iTerminal))
                    iTerminal = 1;
                return iTerminal;
            }
        }
        public String Terminal
        {
            get
            {
                return sTerminal;
            }
            set
            {
                sTerminal = value;
            }
        }
        public String General
        {
            get
            {
                return sGeneral;
            }
            set
            {
                sGeneral = value;
            }
        }

        public String ArrivalInfeedGroups
        {
            get
            {
                return sArrivalInfeedGroups;
            }
            set
            {
                sArrivalInfeedGroups = value;
            }
        }
        public String CIGroups
        {
            get
            {
                return sCIGroups;
            }
            set
            {
                sCIGroups = value;
            }
        }
        public String TransferInfeedGroups
        {
            get
            {
                return sTransferInfeedGroups;
            }
            set
            {
                sTransferInfeedGroups = value;
            }
        }
        public String CICollectors
        {
            get
            {
                return sCICollectors;
            }
            set
            {
                sCICollectors = value;
            }
        }
        public String CIRouting
        {
            get
            {
                return sCIRouting;
            }
            set
            {
                sCIRouting = value;
            }
        }
        public String TransferRouting
        {
            get
            {
                return sTransferRouting;
            }
            set
            {
                sTransferRouting = value;
            }
        }
        public String OCT_MakeUp
        {
            get
            {
                return sOCT_MakeUp;
            }
            set
            {
                sOCT_MakeUp = value;
            }
        }
        public String FlowSplit
        {
            get
            {
                return sFlowSplit;
            }
            set
            {
                sFlowSplit = value;
            }
        }
        public String Process
        {
            get
            {
                return sProcess;
            }
            set
            {
                sProcess = value;
            }
        }
        public String ArrivalContainers
        {
            get
            {
                return sArrivalContainers;
            }
            set
            {
                sArrivalContainers = value;
            }
        }
        public String ArrivalMeanFlows
        {
            get
            {
                return sArrivalMeanFlows;
            }
            set
            {
                sArrivalMeanFlows = value;
            }
        }
        public String CheckInMeanFlows
        {
            get
            {
                return sCheckInMeanFlows;
            }
            set
            {
                sCheckInMeanFlows = value;
            }
        }
        public String TransferMeanFlows
        {
            get
            {
                return sTransferMeanFlows;
            }
            set
            {
                sTransferMeanFlows = value;
            }
        }

        #endregion
        public ParamBHS(String sTerminal_,
                        String sGeneral_,
                        String sArrivalInfeedGroups_,
                        String sCIGroups_,
                        String sTransferInfeedGroups_,
                        String sCICollectors_,
                        String sCIRouting_,
                        String sTransferRouting_,
                        String sOCT_MakeUp_,
                        String sFlowSplit_,
                        String sProcess_,
                        String sArrivalMeanFlows_,
                        String sCheckInMeanFlows_,
                        String sTransferMeanFlows_,
                        String sArrivalContainers_)
        {
            bPaxPlan = (sCheckInMeanFlows_ == "");
            sTerminal = sTerminal_;
            sGeneral = sGeneral_;
            sArrivalInfeedGroups = sArrivalInfeedGroups_;
            sCIGroups = sCIGroups_;
            sTransferInfeedGroups = sTransferInfeedGroups_;
            sCICollectors = sCICollectors_;
            sCIRouting = sCIRouting_;
            sTransferRouting = sTransferRouting_;
            sOCT_MakeUp = sOCT_MakeUp_;
            sFlowSplit = sFlowSplit_;
            sProcess = sProcess_;
            sArrivalMeanFlows = sArrivalMeanFlows_;
            sCheckInMeanFlows = sCheckInMeanFlows_;
            sTransferMeanFlows = sTransferMeanFlows_;
            sArrivalContainers = sArrivalContainers_;
        }

        public ParamBHS(XmlNode Parametres, VersionManager sVersion)
        {
            if ((!OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "UsePaxPlan")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Terminal")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "General")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CIGroup")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CICollectors")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CIRouting")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferRouting")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "FlowSplit")) ||
                (!OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Process")))
            {
                return;
            }
            Boolean.TryParse(Parametres.Attributes["UsePaxPlan"].Value, out bPaxPlan);
            sTerminal = Parametres["Terminal"].FirstChild.Value;
            sGeneral = Parametres["General"].FirstChild.Value;
            sCIGroups = Parametres["CIGroup"].FirstChild.Value;
            sCICollectors = Parametres["CICollectors"].FirstChild.Value;
            sCIRouting = Parametres["CIRouting"].FirstChild.Value;
            sTransferRouting = Parametres["TransferRouting"].FirstChild.Value;
            sFlowSplit = Parametres["FlowSplit"].FirstChild.Value;
            sProcess = Parametres["Process"].FirstChild.Value;
            sArrivalMeanFlows = "";
            sCheckInMeanFlows = "";
            sTransferMeanFlows = "";
            sArrivalInfeedGroups = "";
            sTransferInfeedGroups = "";
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ArrivalInfeedGroups") &&
                (Parametres["ArrivalInfeedGroups"].FirstChild != null))
                sArrivalInfeedGroups = Parametres["ArrivalInfeedGroups"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferInfeedGroups") &&
                (Parametres["TransferInfeedGroups"].FirstChild != null))
                sTransferInfeedGroups = Parametres["TransferInfeedGroups"].FirstChild.Value;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ArrivalMeanFlows"))
                sArrivalMeanFlows = Parametres["ArrivalMeanFlows"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "CheckInMeanFlows"))
                sCheckInMeanFlows = Parametres["CheckInMeanFlows"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "TransferMeanFlows"))
                sTransferMeanFlows = Parametres["TransferMeanFlows"].FirstChild.Value;

            sOCT_MakeUp = "";
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "OCT_MakeUp"))
                sOCT_MakeUp = Parametres["OCT_MakeUp"].FirstChild.Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ArrivalContainers") && (Parametres["ArrivalContainers"].FirstChild != null))
                sArrivalContainers = Parametres["ArrivalContainers"].FirstChild.Value;
            else
                sArrivalContainers = "";
        }
        public XmlNode getParams(XmlDocument projet)
        {
            XmlElement Result = projet.CreateElement("ParamBHS");
            Result.SetAttribute("UsePaxPlan", bPaxPlan.ToString());

            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Terminal", sTerminal));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "General", sGeneral));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "CIGroup", sCIGroups));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "CICollectors", sCICollectors));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "CIRouting", sCIRouting));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "TransferRouting", sTransferRouting));
            if (sOCT_MakeUp != "")
                Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "OCT_MakeUp", sOCT_MakeUp));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "FlowSplit", sFlowSplit));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Process", sProcess));
            if (sArrivalContainers != "")
                Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "ArrivalContainers", sArrivalContainers));

            if (sArrivalMeanFlows != "")
                Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "ArrivalMeanFlows", sArrivalMeanFlows));
            if (sCheckInMeanFlows != "")
                Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "CheckInMeanFlows", sCheckInMeanFlows));
            if (sTransferMeanFlows != "")
                Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "TransferMeanFlows", sTransferMeanFlows));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "ArrivalInfeedGroups", sArrivalInfeedGroups));
            Result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "TransferInfeedGroups", sTransferInfeedGroups));
            return Result;
        }
    }
    #endregion
    #region public class ParamSimulation

    /// <summary>
    /// \Deprecated
    /// Ancienne classe utilisée pour le paramétrage de la simulation.
    /// </summary>
    public class ParamSimulation
    {
        private double dAnalysisStep;
        private double dAnalysisRange;
        private double dWarmUp;
        private String sModelName;
        private bool bDisplayModel;
        private bool bRegeneratePaxplan;
        private bool bExportUserData;

        #region Les accesseurs aux différents éléments de la classe.
        public Double AnalysisStep
        {
            get
            {
                return dAnalysisStep;
            }
            set
            {
                dAnalysisStep = value;
            }
        }

        public Double AnalysisRange
        {
            get
            {
                return dAnalysisRange;
            }
            set
            {
                dAnalysisRange = value;
            }
        }
        public Double WarmUp
        {
            get
            {
                return dWarmUp;
            }
            set
            {
                dWarmUp = value;
            }
        }
        public String ModelName
        {
            get
            {
                return sModelName;
            }
            set
            {
                sModelName = value;
            }
        }
        public bool DisplayModel
        {
            get
            {
                return bDisplayModel;
            }
            set
            {
                bDisplayModel = value;
            }
        }
        public bool RegeneratePaxplan
        {
            get
            {
                return bRegeneratePaxplan;
            }
            set
            {
                bRegeneratePaxplan = value;
            }
        }
        public bool ExportUserData
        {
            get
            {
                return bExportUserData;
            }
            set
            {
                bExportUserData = value;
            }
        }
        #endregion

        public ParamSimulation(String sModelName_,
            Double dAnalysisStep_,
            Double dAnalysisRange_,
            Double dWarmUp_,
            bool bDisplayModel_,
            bool bRegeneratePaxplan_,
            bool bExportUserData_)
        {
            sModelName = sModelName_;
            dAnalysisStep = dAnalysisStep_;
            dAnalysisRange = dAnalysisRange_;
            dWarmUp = dWarmUp_;
            bDisplayModel = bDisplayModel_;
            bRegeneratePaxplan = bRegeneratePaxplan_;
            bExportUserData = bExportUserData_;
        }
        public ParamSimulation(XmlNode Parametres, VersionManager sVersion)
        {
            sModelName = null;
            bDisplayModel = false;
            dAnalysisStep = 5;
            dAnalysisRange = 15;
            dWarmUp = 0.0;
            bRegeneratePaxplan = true;
            bExportUserData = true;

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AnalysisRange"))
                Double.TryParse(Parametres["AnalysisRange"].FirstChild.Value, out dAnalysisRange);

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ModelName"))
            {
                sModelName = Parametres["ModelName"].FirstChild.Value.ToString();
                bDisplayModel = true;
                if (OverallTools.FonctionUtiles.hasNamedAttribute(Parametres["ModelName"], "DisplayModel"))
                {
                    Boolean.TryParse(Parametres["ModelName"].Attributes["DisplayModel"].Value.ToString(), out bDisplayModel);
                }
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "AnalysisStep"))
            {
                Double.TryParse(Parametres["AnalysisStep"].FirstChild.Value.ToString(), out  dAnalysisStep);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "WarmUp"))
            {
                Double.TryParse(Parametres["WarmUp"].FirstChild.Value.ToString(), out  dWarmUp);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "RegeneratePaxplan"))
            {
                Boolean.TryParse(Parametres["RegeneratePaxplan"].FirstChild.Value.ToString(), out  bRegeneratePaxplan);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ExportUserData"))
            {
                Boolean.TryParse(Parametres["ExportUserData"].FirstChild.Value.ToString(), out  bExportUserData);
            }
        }
        public XmlNode getParams(XmlDocument projet)
        {
            if (projet == null)
                return null;
            XmlElement result = projet.CreateElement("Simulation");
            if (sModelName != null)
            {
                XmlElement xnSon = OverallTools.FonctionUtiles.CreateElement(projet, "ModeleName", sModelName);
                xnSon.SetAttribute("DisplayModel", bDisplayModel.ToString());
                result.AppendChild(xnSon);
            }
            if (dWarmUp != 0)
            {
                result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "WarmUp", dWarmUp.ToString()));
            }
            if (dAnalysisStep != 5)
            {
                result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "AnalysisStep", dAnalysisStep.ToString()));
            }

            if (dAnalysisRange != 15)
            {
                result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "AnalysisRange", dAnalysisRange.ToString()));
            }
            if (bRegeneratePaxplan)
            {
                result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "RegeneratePaxplan", bRegeneratePaxplan.ToString()));
            }
            if (bExportUserData)
            {
                result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "ExportUserData", bExportUserData.ToString()));
            }
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    #endregion
    #region public class ParamAnalysis
    /// <summary>
    /// \Deprecated
    /// Ancienne classe qui contenait les paramétrage pour un scénario. Cette classe, et la plupart de celles qu'elle
    /// utilise ont été remplacée par la classe \ref ParamScenario.
    /// </summary>
    public class ParamAnalysis
    {
        private ParamDeparturePeak pdpPeakDeparture;
        private ParamArrivalPeak papPeakArrival;
        private ParamPaxPlan pppPaxPlan;
        private ParamAutomod paAutomode;
        private ParamBHS pbBHS;
        private ParamSimulation psSimulation;
        private ParamUserData pudUserData;
        private DateTime dtDateDebut;
        private DateTime dtDateFin;

        private String sName;

        #region Les accesseurs aux différents éléments de la classe.
        public ParamDeparturePeak PeakDeparture
        {
            get
            {
                return pdpPeakDeparture;
            }
            set
            {
                pdpPeakDeparture = value;
            }
        }
        public ParamArrivalPeak PeakArrival
        {
            get
            {
                return papPeakArrival;
            }
            set
            {
                papPeakArrival = value;
            }
        }
        public ParamPaxPlan PaxPlan
        {
            get
            {
                return pppPaxPlan;
            }
            set
            {
                pppPaxPlan = value;
            }
        }
        public String Name
        {
            get
            {
                return sName;
            }
            set
            {
                sName = value;
            }
        }
        public ParamAutomod Automod
        {
            get
            {
                return paAutomode;
            }
            set
            {
                paAutomode = value;
            }
        }
        public ParamBHS BHS
        {
            get
            {
                return pbBHS;
            }
            set
            {
                pbBHS = value;
            }
        }
        public ParamSimulation Simulation
        {
            get
            {
                return psSimulation;
            }
            set
            {
                psSimulation = value;
            }
        }
        public ParamUserData UserData
        {
            get
            {
                return pudUserData;
            }
            set
            {
                pudUserData = value;
            }
        }

        public DateTime DateDebut
        {
            get
            {
                return dtDateDebut;
            }
            set
            {
                dtDateDebut = value;
            }
        }
        public DateTime DateFin
        {
            get
            {
                return dtDateFin;
            }
            set
            {
                dtDateFin = value;
            }
        }
        #endregion

        public ParamAnalysis(String Name, DateTime dtDateDebut_,
            DateTime dtDateFin_, ParamDeparturePeak PeakDeparture,
            ParamArrivalPeak PeakArrival, ParamPaxPlan PaxPlan, ParamAutomod Automod_, ParamBHS BHS_, ParamSimulation psSimulation_, ParamUserData pudUserData_)
        {
            dtDateDebut = dtDateDebut_;
            dtDateFin = dtDateFin_;
            pdpPeakDeparture = PeakDeparture;
            papPeakArrival = PeakArrival;
            pppPaxPlan = PaxPlan;
            paAutomode = Automod_;
            psSimulation = psSimulation_;
            pudUserData = pudUserData_;
            pbBHS = BHS_;
            sName = Name;
        }
        public ParamAnalysis(XmlNode Parametres, VersionManager sVersion)
        {
            pdpPeakDeparture = null;
            papPeakArrival = null;
            pppPaxPlan = null;
            paAutomode = null;
            pudUserData = null;
            sName = null;
            pbBHS = null;
            if (!OverallTools.FonctionUtiles.hasNamedAttribute(Parametres, "Name"))
            {
                return;
            }
            sName = Parametres.Attributes["Name"].Value;
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ParamDeparturePeak"))
            {
                pdpPeakDeparture = new ParamDeparturePeak(Parametres["ParamDeparturePeak"], sVersion);
                dtDateDebut = pdpPeakDeparture.DateDebut;
                dtDateFin = pdpPeakDeparture.DateFin;
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ParamArrivalPeak"))
            {
                papPeakArrival = new ParamArrivalPeak(Parametres["ParamArrivalPeak"], sVersion);
                dtDateDebut = papPeakArrival.DateDebut;
                dtDateFin = papPeakArrival.DateFin;
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ParamPaxPlan"))
            {
                pppPaxPlan = new ParamPaxPlan(Parametres["ParamPaxPlan"], sVersion);
                dtDateDebut = pppPaxPlan.DateDebut;
                dtDateFin = pppPaxPlan.DateFin;
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ParamAutomod"))
            {
                paAutomode = new ParamAutomod(Parametres["ParamAutomod"], sVersion);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "ParamBHS"))
            {
                pbBHS = new ParamBHS(Parametres["ParamBHS"], sVersion);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Simulation"))
            {
                psSimulation = new ParamSimulation(Parametres["Simulation"], sVersion);
            }

            if (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "UserData"))
            {
                pudUserData = new ParamUserData(Parametres["UserData"], sVersion);
            }

            if ((OverallTools.FonctionUtiles.hasNamedChild(Parametres, "Start")) &&
                (OverallTools.FonctionUtiles.hasNamedChild(Parametres, "End")))
            {
                DateTime.TryParse(Parametres["Start"].FirstChild.Value, out dtDateDebut);
                DateTime.TryParse(Parametres["End"].FirstChild.Value, out dtDateFin);
            }
        }
        public XmlNode getParams(XmlDocument projet)
        {
            if (projet == null)
                return null;
            XmlElement result = projet.CreateElement("Config");
            result.SetAttribute("Name", sName);
            result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Start", dtDateDebut.ToString()));
            result.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "End", dtDateFin.ToString()));
            if (pdpPeakDeparture != null)
            {
                result.AppendChild(pdpPeakDeparture.getParams(projet));
            }
            if (papPeakArrival != null)
            {
                result.AppendChild(papPeakArrival.getParams(projet));
            }
            if (pppPaxPlan != null)
            {
                result.AppendChild(pppPaxPlan.getParams(projet));
            }
            if (paAutomode != null)
            {
                result.AppendChild(paAutomode.getParams(projet));
            }
            if (pbBHS != null)
            {
                result.AppendChild(pbBHS.getParams(projet));
            }
            if (psSimulation != null)
            {
                result.AppendChild(psSimulation.getParams(projet));
            }

            if (pudUserData != null)
            {
                result.AppendChild(pudUserData.getParams(projet));
            }
            return result;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    #endregion
    #endregion
}
