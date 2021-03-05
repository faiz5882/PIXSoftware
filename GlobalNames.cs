#define PARKINGMULHOUSE
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIMCORE_TOOL.Assistant;

namespace SIMCORE_TOOL
{
    class GlobalNames
    {
        internal abstract class ConvertClass
        {
            internal abstract DataTable Convert(DataTable dtOldTable);
        }

        #region Valeurs pouvant être présentes dans la tables Alloc Passport/ Security / Transfert
        internal static String sAllocation_NotApplicable = "N/A";
        internal static String sAllocation_Local = "Local";
        internal static String sAllocation_NotLocal = "Not Local";
        internal static String sAllocation_All = "All";
        internal static String sAllocation_Eco = "Eco";
        internal static String sAllocation_FB = "F&B";
        #endregion

        #region Parametres pour les parkings.
        internal const String sParkingGeneralName = "PKG_General";
        internal const String sParkingGeneralFullName = "Parking General Parameters";
        internal const String sParkingModaleDistribName = "PKG_Modal Distribution";
        internal const String sParkingModaleDistribFullName = "Modal Distribution";
        internal const String sParkingOccupationTimeName = "PKG_Occupation";
        internal const String sParkingOccupationTimeFullName = "PKG Occupation Time Distribution";
        internal const String sParkingDitributionTimeName = "PKG_DistributionTime";
        internal const String sParkingDitributionTimeFullName = "Complementary traffic at Peak Hour & PKG usage rates";


        internal static String sParkingColumnTotal = "Total";

        internal static String sParkingGeneralLine1 = "Passengers traffic";
        internal static String sParkingGeneralLine2 = "Information 1";
        internal static String sParkingGeneralLine3 = "Information 2";
        internal static String sParkingGeneralLine4 = "Information 3";
        internal static String sParkingGeneralLine5 = "Departure passengers";
        internal static String sParkingGeneralLine6 = "Origin passengers";
        internal static String sParkingGeneralLine7 = "Passengers 30th PH (A+D)";
        internal static String sParkingGeneralLine8 = "Passengers 30th PH (O+D)";
        internal static String sParkingGeneralLine9 = "Passengers 30th PH (Total)";
        internal static String sParkingGeneralLine10 = "Distribution of peak hour (30th PH)";
        internal static String sParkingGeneralLine11 = "Distribution of peak hour (30th PH) PAX A + D";
        internal static String sParkingGeneralLine12 = "Distribution of peak hour (30th PH) %";
        //internal static String sParkingGeneralLine13 = "Complementary traffic during rush times. PAX A+D";
        internal static String sParkingGeneralLine16 = "Number of Pax per Vehicle";
        internal static String sParkingGeneralLine17 = "Estimated number of vehicles";
        internal static String sParkingGeneralLine18 = "Distribution access Parking (except incidents)";
        internal static String sParkingGeneralLine19 = "Space";
        internal static String sParkingGeneralLine20 = "Absolute max occupation";
        internal static String sParkingGeneralLine21 = "30th PH occupation";


        internal static String sParkingDistribRushTimeLine1Begin = "Complementary traffic during peak hour (";
        internal static String sParkingDistribRushTimeLine1End = " PH)";
        internal static String sParkingDistribRushTimeLine2Begin = "Distribution of passengers during peak hour (";
        internal static String sParkingDistribRushTimeLine2End = " PH)";

        internal static String sParkingModalDistribLine1 = "Small vehicles";
        internal static String sParkingModalDistribLine2 = "Public transport (Buses)";
        internal static String sParkingModalDistribLine3 = "Transfer services";
        internal static String sParkingModalDistribLine4 = "Taxi";

        internal static String sParkingOccupationTimeLine1 = "<1h";
        internal static String sParkingOccupationTimeLine2 = "1h-2h";
        internal static String sParkingOccupationTimeLine3 = "2h-4h";
        internal static String sParkingOccupationTimeLine4 = "4h-6h";
        internal static String sParkingOccupationTimeLine5 = "6h-12h";
        internal static String sParkingOccupationTimeLine6 = "12h-24h";
        internal static String sParkingOccupationTimeLine7 = "24h-36h";
        internal static String sParkingOccupationTimeLine8 = "36h-48h";
        internal static String sParkingOccupationTimeLine9 = "48h-60h";
        internal static String sParkingOccupationTimeLine10 = "60h-72h";
        internal static String sParkingOccupationTimeLine11 = "72h-120h";
        internal static String sParkingOccupationTimeLine12 = "120h-168h";
        internal static String sParkingOccupationTimeLine13 = "168h-336h";
        internal static String sParkingOccupationTimeLine14 = ">336h";

        #endregion

        #region Préfixes pour les tables BHS

        public static String sBHS_PrefixeLong = "BHS_Terminal_";
        public static String sBHS_Prefixe = "BHS_T";
        public static String BHS_Prefixe_Name = "Terminal ";
        #endregion

        #region Current Names for the passenger tables
        internal static String FPDTableName = "FPDTable"; //Ok
        internal static String FPATableName = "FPATable"; //Ok
        internal const String FPLinksTableName = "FP_LinksTable";
        internal static String FP_AircraftTypesTableName = "FP_AircraftTypesTable"; //Ok
        internal static String FP_AirlineCodesTableName = "FP_AirlineCodesTable"; //Ok
        internal static String FP_AirportCodesTableName = "FP_AirportCodesTable"; //Ok
        internal static String FP_FlightCategoriesTableName = "FP_FlightCategoriesTable"; //Ok
        internal static String FPD_LoadFactorsTableName = "FPD_LoadFactorsTable"; //Ok
        internal static String FPA_LoadFactorsTableName = "FPA_LoadFactorsTable"; //Ok

        internal const String Transfer_ICTTableName = "Transfer_ICTTable"; //Ok
        internal const String Transfer_TerminalDitributionTableName = "Transfer_TerminalDitributionTable"; //Ok

        internal const String Transfer_FlightCategoryDitributionTableName = "Transfer_FlightCategoryDitributionTable"; //Ok
        internal static String CI_ShowUpTableName = "CI_ShowUpTable"; //Ok
        internal static String NbBagsTableName = "NbBagsTable"; //Ok
        internal static String NbVisitorsTableName = "NbVisitorsTable"; //Ok
        internal static String NbTrolleyTableName = "NbTrolleyTable"; //Ok

        internal static String Parking_InitialStateTableName = "Parking_InitialState"; //Ok
        internal const String Parking_ShortStayTableName = "Parking_ShortStay"; //Ok
        internal const String Parking_LongStayTableName = "Parking_LongStay"; //Ok


        internal static String OCT_CITableName = "OCT_CITable";
        internal static String Alloc_CITableName = "Alloc_CITable";
        internal static String Opening_CITableName = "Opening_CITable";
        //baggDrop
        internal static String OCT_BaggDropTableName = "OCT_BaggageDropTable";
        internal static String OCT_BoardGateTableName = "OCT_BoardGateTable";
        internal static String Alloc_BoardGateTableName = "Alloc_BoardGateTable";
        internal const String boardingGatesPrioritiesTableName = "BoardingGates_Priorities";   // >> Bug #13367 Liege allocation
        //arrival gate OCT
        internal static String OCT_ArrivalGateTableName = "OCT_ArrivalGateTable";
        internal static String OCT_BaggageClaimTableName = "OCT_BaggageClaimTable";
        internal static String Alloc_BaggageClaimTableName = "Alloc_BaggageClaimTable";

        internal static String OCT_ParkingTableName = "OCT_ParkingTable";
        internal static String Alloc_ParkingTableName = "Alloc_ParkingTable";
        internal const String parkingPrioritiesTableName = "Parking_Priorities";   // >> Bug #13367 Liege allocation
        //runway oct
        internal static String OCT_RunwayTableName = "OCT_RunwayTable";
        internal static String Alloc_PassportCheckTableName = "Alloc_PassportCheckTable";
        internal static String Alloc_SecurityCheckTableName = "Alloc_SecurityCheckTable";
        // << Task #7570 new Desk and extra information for Pax -Phase I B
        internal static String Alloc_UserProcessCheckTableName = "Alloc_UserProcessCheckTable";
        // >> Task #7570 new Desk and extra information for Pax -Phase I B
        internal static String Alloc_TransferDeskTableName = "Alloc_TransferDeskTable";

        // >> Task #13880 Various UI improvements and fixes        
        internal static List<String> planningTableNamesList = new List<String>(new String[] {Alloc_PassportCheckTableName,
            Alloc_SecurityCheckTableName, Alloc_UserProcessCheckTableName, Alloc_TransferDeskTableName });
        // << Task #13880 Various UI improvements and fixes

        internal static String Alloc_SaturationTableName = "Alloc_Saturation";

        internal static String OCT_MakeUpTableName = "OCT_MakeUp";
        internal static String SegregationName = "Segregation";
        //arrival infeed OCT //arrInf
        internal static String OCT_ArrivalInfeedTableName = "OCT_ArrivalInfeedTable";
        internal static String TransferInfeedAllocationRulesTableName = "TransferInfeedAllocationRulesTable";
        internal static String Baggage_Claim_ConstraintName = "Baggage_Claim_Constraint";

        internal static String Capa_QueuesTableName = "Capa_QueuesTable";
        internal static String Group_QueuesName = "Group_Queues";
        internal static String Animated_QueuesName = "Animated_Queues";
        internal static String Times_ProcessTableName = "Times_ProcessTable";
        internal static String ItineraryTableName = "ItineraryTable";

        internal const String ProcessScheduleName = "ProcessSchedule";
        internal static String ParkingTableName = "ParkingTable";
        internal static String FromToShuttleTableName = "FromToShuttleTable";
        internal static String OneofSpecificationTableName = "OneofSpecificationTable";

        internal const String ProcessDistributionPaxInName = "DistributionPaxIn";
        internal const String ProcessDistributionPaxInFullName = "Distribution Pax In";
        internal const String ProcessDistributionPaxOutName = "DistributionPaxOut";
        internal const String ProcessDistributionPaxOutFullName = "Distribution Pax Out";

        //<< Task #7405 - new Desk and extra information for Pax
        internal const String sUserAttributesTableName = "User Attributes";
        internal const String sUserAttributes_ColumnName = "Column Name";
        internal const String sUserAttributes_DirectoryName = "User Attributes Tables";
        internal const String sUserAttributes_DistributionTableColumnName_Value = "Value";

        //>> Task #7405 - new Desk and extra information for Pax
        // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
        internal const String fpaInBoundSmoothedTableName = "FPA_InBoundRolling";
        internal const String fpaInBoundInstantaneousTableName = "FPA_InBoundInstantaneous";

        internal const String fpdOutBoundSmoothedTableName = "FPD_OutBoundRolling";
        internal const String fpdOutBoundInstantaneousTableName = "FPD_OutBoundInstantaneous";

        internal const String FPDCheckInShowUpSmoothedTableName = "FPD_CheckInShowUpRolling";
        internal const String FPDCheckInShowUpInstantaneousTableName = "FPD_CheckInShowUpInstantaneous";

        internal const String fpdFpaInOutBoundSmoothedTableName = "FPDFPA_InOutBoundRolling";
        internal const String fpdFpaInOutBoundInstantaneousTableName = "FPDFPA_InOutBoundInstantaneous";

        internal const String FPD_FPA_PAXBoardingRoom_BHSMakeUpSmoothedTableName = "PAX Boarding room & BHS Make-Up Rolling flows";
        internal const String FPD_FPA_PAXBoardingRoom_BHSMakeUpInstantaneousTableName = "PAX Boarding room & BHS Make-Up Instantaneous flows";

        internal const String fpaPeakStatsBHSTableName = "FPA_Peak_Stats_BHS";

        internal const String paxGateSmoothedBHSTableName = "FPA_InBoundRolling_BHS";
        internal const String paxGateInstantaneousBHSTableName = "FPA_InBoundInstantaneous_BHS";

        // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
        internal const String FPA_TransferICTSmoothedTableName = "FPA_TransferICTRolling";
        internal const String FPA_TransferICTInstantaneousTableName = "FPA_TransferICTInstantaneous";
        // >> Task #9504 Pax2Sim - flight subCategories - peak flow segregation

        internal const String sUserAttributesBaggLoadingRateTableName = "Arrival Baggage Loading Rate";
        internal const String sUserAttributesBaggLoadingRateValueColumnName = "Arrival Loading Rate (Bags / min)";

        internal const String sUserAttributesBaggLoadingDelayTableName = "Arrival Baggage Loading Delay";
        internal const String sUserAttributesBaggLoadingDelayValueColumnName = "Arrival Loading Delay (min)";

        // >> Task #8958 Reclaim Synchronisation mode Gantt
        internal const string reclaimSyncDirectoryName = "ReclaimSync";
        internal const string userAttributesReclaimLogTableName = "ReclaimSync_LastBag";
        internal const string userAttributesReclaimLogValueColumnName = "Limit of Bags left on the Belt";
        internal static List<String> defaultReclaimLogValuesList = new List<String>(new String[] { "-15", "-30" });
        // << Task #8958 Reclaim Synchronisation mode Gantt

        // << Task #9260 Pax2Sim - Static Analysis - EBS algorithm - EBS per Terminal
        internal const String ebsRatesTableTerminalUnknownColumnName = "T0";
        internal const String ebsDirectoryName = "EBS";
        internal const String TERMINAL_ABBREVIATION = "T";
        // >> Task #9260 Pax2Sim - Static Analysis - EBS algorithm - EBS per Terminal

        // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
        internal const String oldUserAttributesEBSInputRateTableName = "EBS Input Rate";
        internal const String userAttributesEBSInputRateTableName = "EBS Input Rates";
        internal const String userAttributesEBSInputRateValueColumnName = "EBS Input Rate (Bags / min)";

        internal const String oldUserAttributesEBSOutputRateTableName = "EBS Output Rate";
        internal const String userAttributesEBSOutputRateTableName = "EBS Output Rates";
        internal const String userAttributesEBSOutputRateValueColumnName = "EBS Output Rate (Bags / min)";

        internal static List<String> oldEbsRateTableNamesList = new List<String>(new String[] {oldUserAttributesEBSInputRateTableName,
            oldUserAttributesEBSOutputRateTableName});

        internal static List<String> ebsRateTableNamesList = new List<String>(new String[] {userAttributesEBSInputRateTableName,
            userAttributesEBSOutputRateTableName});

        internal const String OCT_BAGDROP_XML_CHILD_NODE_NAME = "octBagDropTableName"; // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
        internal const String EBS_INPUT_RATE_XML_CHILD_NODE_NAME = "ebsInputRateTableName";
        internal const String EBS_OUTPUT_RATE_XML_CHILD_NODE_NAME = "ebsOutputRateTableName";
        // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

        // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
        internal const String numberOfPassengersTableName = "Number of Passengers";
        internal const String originatingPaxDepartureColumnName = "Originating PAX (Departure) / Flight";
        internal const String transferringPaxDepartureColumnName = "Transferring PAX (Departure) / Flight";
        internal const String terminatingPaxArrivalColumnName = "Terminating PAX (Arrival) / Flight";
        internal const String transferringPaxArrivalColumnName = "Transferring PAX (Arrival) / Flight";

        internal const String numberOfBaggagesTableName = "Number of Bags";
        internal const String originatingBagDepartureColumnName = "Originating Bag (Departure) / Flight";
        internal const String transferringBagDepartureColumnName = "Transferring Bag (Departure) / Flight";
        internal const String terminatingBagArrivalColumnName = "Terminating Bag (Arrival) / Flight";
        internal const String transferringBagArrivalColumnName = "Transferring Bag (Arrival) / Flight";

        internal const String PAX_TYPE_IDENTIFIER = "PAX";
        internal const String BAG_TYPE_IDENTIFIER = "BAG";

        internal const String USE_DEFINED_NB_PAX_XML_ATTRIBUTE_NAME = "useDefinedNbPax";
        internal const String USE_DEFINED_NB_BAGS_XML_ATTRIBUTE_NAME = "useDefinedNbBags";

        internal const String NB_OF_PAX_XML_ATTRIBUTE_NAME = "numberOfPassengers";
        internal const String NB_OF_BAGS_XML_ATTRIBUTE_NAME = "numberOfBaggages";
        // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

        // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
        internal const String oldLoadingRateParameterTableName = "Baggage Loading Rate";
        internal const String oldLoadingDelayParameterTableName = "Baggage Loading Delay";
        internal static List<String> oldUserAttributeTableNames = new List<String>(new String[] {oldLoadingRateParameterTableName,
            oldLoadingDelayParameterTableName});
        // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
        internal static List<String> nonUserAttributesExceptionsList = new List<String>(
            new String[] { sUserAttributesBaggLoadingRateTableName, sUserAttributesBaggLoadingDelayTableName,
                userAttributesReclaimLogTableName,
                userAttributesEBSInputRateTableName, userAttributesEBSOutputRateTableName    // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
                , numberOfPassengersTableName, numberOfBaggagesTableName, // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                USA_STANDARD_PARAMETERS_TABLE_NAME});   // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
        // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant

        // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
        internal static String flightSubcategoriesTableName = "Flight Subcategories";
        internal static String flightSubcategoryColumnName = "Flight Subcategory";
        internal static String flightSubcategories_XML_childNodeName = "FlightSubcategories";

        internal static String defaultFlightSubcateg_DOM_USA = "DOM_USA";
        internal static String defaultFlightSubcateg_INT = "INT";
        internal static String defaultFlightSubcateg_CAN = "CAN";

        internal static List<String> defaultFlightSubcategoriesList = new List<String>();   // >> Task #10069 Pax2Sim - no BNP development
             //= new List<String>(new String[] { defaultFlightSubcateg_DOM_USA, defaultFlightSubcateg_INT, defaultFlightSubcateg_CAN });


        internal static String FPD_table_preffix = "FPD_";
        internal static String FPA_table_preffix = "FPA_";

        internal static String flight_subcategory_table_suffix = "_FSC";

        internal static String FPD_flightInformationTableName 
            = FPD_table_preffix + "Flight subcategory Information" + flight_subcategory_table_suffix;
        internal static String FPA_flightInformationTableName
            = FPA_table_preffix + "Flight subcategory Information" + flight_subcategory_table_suffix;

        internal static String flightInformationFlightIdColumnName = "Flight Id";
        internal static String flightInformationFlightNumberColumnName = "Flight Number";
        internal static String flightInformationFlightCategoryColumnName = "Flight Category";
        internal static String flightInformationFlightSubcategoryColumnName = "Flight Subcategory";
        // >> Task #9504 Pax2Sim - flight subCategories - peak flow segregation

        // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        internal static String USER_ATTRIBUTES_XML_ELEMENT_NAME = "UserAttributes";
        internal static String USER_ATTRIBUTE_XML_ELEMENT_ATTRIBUTE_NAME = "UserAttributeName";
        // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        
        // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        internal const String capaProcessTableName = "CapaProcess";
        internal const String capaProcessTableFullName = "Pax Process (Capacities)";
        internal const String capaProcessTableProcessCapacityColumnName = "Process Capacity";
        // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)

        // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
        internal static String PROCESS_CAPACITIES_SCENARIO_PARAM_NAME = "ProcessCapacities";
        // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection

        // << Task #9171 Pax2Sim - Static Analysis - EBS algorithm - Throughputs
        # region Static Analysis - FPD - EBS table columns
        internal static String FPD_EBS_ROLLING_TABLE_NAME = "FPD_EBSRolling";
        internal static String FPD_EBS_TIME_COLUMN_NAME = "Time";
        internal static String FPD_EBS_INPUT_DEMAND_COLUMN_NAME = "Input Demand";
        internal static String FPD_EBS_INPUT_OVERFLOW_COLUMN_NAME = "Input Overflow";
        internal static String FPD_EBS_INPUT_COLUMN_NAME = "Input Bags";
        internal static String FPD_EBS_THROUGHPUT_INPUT_COLUMN_NAME = "Input Bags / h";

        internal static String FPD_EBS_OUTPUT_DEMAND_COLUMN_NAME = "Output Demand";
        internal static String FPD_EBS_OUTPUT_BACKLOG_COLUMN_NAME = "Output Backlog";
        internal static String FPD_EBS_OUTPUT_COLUMN_NAME = "Output Bags";
        internal static String FPD_EBS_THROUGHPUT_OUTPUT_COLUMN_NAME = "Output Bags / h";

        internal static String FPD_EBS_CONSTRAINED_COLUMN_NAME = "Constrained Occupation";  //"Total";  // >> Task #10346 Pax2Sim - EBS review
        internal static String FPD_EBS_UNCONSTRAINED_OCCUPATION_COLUMN_NAME = "Unconstrained Occupation";
        
        internal static String FPD_EBS_UNKNOWN_TERMINAL_COLUMN_NAME = "Unknow Terminal";

        internal static List<String> ebsThroughputChartColumnNamesList = new List<String>(new String[] {FPD_EBS_INPUT_COLUMN_NAME,
            FPD_EBS_THROUGHPUT_INPUT_COLUMN_NAME, FPD_EBS_OUTPUT_COLUMN_NAME, FPD_EBS_THROUGHPUT_OUTPUT_COLUMN_NAME });
        
        internal static String THROUGHPUT_COLUMN_IDENTIFIER = "Throughput";
        internal static String THROUGHPUT_COLUMN_SHORT_IDENTIFIER = "/ h";

        #endregion
        // >> Task #9171 Pax2Sim - Static Analysis - EBS algorithm - Throughputs
        #endregion

        #region Current Names for the Baggage tables
        #region General
        internal static String sBHS_Capa_Queues = "BHS_Capa_Queues";
        internal static String sBHS_Group_Queues = "BHS_Group_Queues";
        internal static String sBHS_Itinerary = "BHS_Itinerary";
        #endregion
        #region Indexée

        internal static String sBHS_General = "General";
        internal static String sBHS_ArrivalInfeed_Groups = "ArrivalInfeed_Groups";
        internal static String sBHS_CI_Groups = "CI_Groups";
        internal static String sBHS_TransferInfeed_Groups = "TransferInfeed_Groups";
        internal static String sBHS_CI_Collectors = "CI_Collectors";
        internal static String sBHS_CI_Routing = "CI_Routing";
        internal static String sBHS_HBS3_Routing = "HBS3_Routing";
        internal static String sBHS_Transfer_Routing = "Transfer_Routing";
        internal static String sBHS_Flow_Split = "Flow_Split";
        internal static String sBHS_Process = "Process";
        internal static String sBHS_Alloc_MakeUp = "Alloc_MakeUp";
        internal static String sBHS_Arrival_Containers = "Arrival_Containers";

        internal static String sBHS_Mean_Flows_Arrival_Infeed = "Mean_Flows_Arrival_Infeed";
        internal static String sBHS_Mean_Flows_Check_In = "Mean_Flows_Check_In";
        internal static String sBHS_Mean_Flows_Transfer_Infeed = "Mean_Flows_Transfer_Infeed";
        #endregion
        #endregion

        #region Old names for the passenger tables
        internal static String OCT_MakeUpTableName_1_16 = "AllocationRulesTable";
        #endregion


        internal static String Pax_GenTransferLog = "Pax_GenTransferLog";
        internal static String PaxPlanName = "PaxPlan";
        internal static String PrkPlanName = "PrkPlan";
        internal static String BagPlanName = "BagPlan";
        // << Sodexi Task#7129 Bagplan Update
        internal static String BagPlan2Name = "BagPlan_2";

        
        #region BagPlan2 Columns
        internal static String sBagPlan2_Column_Time = "Time(mn)";
        internal static String sBagPlan2_Column_FirstBag = "FirstBag";
        internal static String sBagPlan2_Column_BagId = "ID BAG";
        internal static String sBagPlan2_Column_PaxId = "ID PAX";
        internal static String sBagPlan2_Column_FPAId = "FPA_ID";
        internal static String sBagPlan2_Column_FPAClass = "FPA_Class";
        internal static String sBagPlan2_Column_PaxAtReclaim = "PaxAtReclaim";
        internal static String sBagPlan2_Column_FPDId = "FPD_ID";
        internal static String sBagPlan2_Column_FPDClass = "FPD_Class";
        internal static String sBagPlan2_Column_STD = "STD";
        internal static String sBagPlan2_Column_NbBags = "NbBags";
        internal static String sBagPlan2_Column_OOGBag = "OOG_Bag";
        internal static String sBagPlan2_Column_Segregation = "Segregation";
        internal static String sBagPlan2_Column_PassportLocal = "PassportLocal";
        internal static String sBagPlan2_Column_Transfer = "Transfer";
        internal static String sBagPlan2_Column_ArrivalTerminal = "Term.Arr";
        internal static String sBagPlan2_Column_ArrivalGateNb = "#Arr.Gate";
        internal static String sBagPlan2_Column_CheckInTerminal = "Term.CI";
        internal static String sBagPlan2_Column_CheckInNb = "#CI";
        internal static String sBagPlan2_Column_Weight = "Weight";
        internal static String sBagPlan2_Column_Length = "Length";
        internal static String sBagPlan2_Column_Width = "Width";
        internal static String sBagPlan2_Column_Height = "Height";
        internal static String sBagPlan2_Column_Volume = "Volume";
        internal static String sBagPlan2_Column_LocalOrigin = "Orig_Local";
        internal static String sBagPlan2_Column_LocalDestination = "Dest_Local";
        // << Sodexi Task#7495 Modification of Sodexi Project
        internal static String sBagPlan2_Column_CustomStatus = "Customs status";
        // >> Sodexi Task#7495 Modification of Sodexi Project
        internal static String sBagPlan2_Column_User1 = "User 1";
        internal static String sBagPlan2_Column_User2 = "User 2";
        internal static String sBagPlan2_Column_User3 = "User 3";
        internal static String sBagPlan2_Column_User4 = "User 4";
        internal static String sBagPlan2_Column_User5 = "User 5";
        #endregion

        #region BagPlanInformation Columns
        internal static String sBagPlanInformation_ID_BAG = "ID_BAG";
        internal static String sBagPlanInformation_ID_PAX = "ID_PAX";
        internal static String sBagPlanInformation_ArrivalFlight = "Arrival Flight";
        internal static String sBagPlanInformation_DepartureFlight = "Departure Flight";
        internal static String sBagPlanInformation_LTA = "LTA";
        internal static String sBagPlanInformation_Antenne = "Antenne ou sortie";
        internal static String sBagPlanInformation_Weight = "Weight";
        internal static String sBagPlanInformation_Length = "Length";
        internal static String sBagPlanInformation_Width = "Width";
        internal static String sBagPlanInformation_Height = "Height";
        internal static String sBagPlanInformation_Volume = "Volume";
        internal static String sBagPlanInformation_OriginLocal = "Orig_Local";
        internal static String sBagPlanInformation_DestinationLocal = "Dest_Local";
        // << Sodexi Task#7495 Modification of Sodexi Project
        internal static String sBagPlanInformation_CustomStatus = "Customs status";
        // >> Sodexi Task#7495 Modification of Sodexi Project
        internal static String sBagPlanInformation_User1 = "User 1";
        internal static String sBagPlanInformation_User2 = "User 2";
        internal static String sBagPlanInformation_User3 = "User 3";
        internal static String sBagPlanInformation_User4 = "User 4";
        internal static String sBagPlanInformation_User5 = "User 5";
        #endregion
        // >> Sodexi Task#7129 Bagplan Update
        internal static String ProcessName = "Process";
        internal static String Airline = "Airline";
        internal static String FirstAndBusiness = "First and Business";
        internal static String Flight = "Flight";
        internal static String FlightCategory = "Flight Category";

        public static String sBHS_MakeUpObject = "Make-Up";

        #region Columns FPD
        internal static String sFPD_Column_Mup_Start = "Make-Up start";
        internal static String sFPD_Column_Mup_End = "Make-Up end";
        #region Old

        #endregion
        internal static String sFPD_Column_STD = "STD";//-
        internal static String sFPD_Column_TSA = "TSA";//-
        internal static String sFPD_Column_TerminalCI = "CI Terminal";//-
        internal static String sFPD_Column_Eco_CI_Start = "Eco. Class CI start";//-
        internal static String sFPD_Column_Eco_CI_End = "Eco. Class CI end";//-
        internal static String sFPD_Column_FB_CI_Start = "F&B Class CI start";//-
        internal static String sFPD_Column_FB_CI_End = "F&B Class CI end";//-

        internal static String sFPD_Column_Eco_Drop_Start = "Eco. Bag Drop start";//-
        internal static String sFPD_Column_Eco_Drop_End = "Eco. Bag Drop end";//-
        internal static String sFPD_Column_FB_Drop_Start = "F&B Bag Drop start";//-
        internal static String sFPD_Column_FB_Drop_End = "F&B Bag Drop end";//-

        internal static String sFPD_Column_BoardingGate = "Boarding Gate";//-

        internal static String sFPD_Column_TerminalMup = "Make-Up Terminal";//-

        internal static String sFPD_Column_Eco_Mup_Start = "Eco. Make-Up start";//-
        internal static String sFPD_Column_Eco_Mup_End = "Eco. Make-Up end";//-
        internal static String sFPD_Column_First_Mup_Start = "F&B Make-Up start";//-
        internal static String sFPD_Column_First_Mup_End = "F&B Make-Up end";//-

        #endregion

        #region Common Columns FPD and FPA
        internal static String sFPD_A_Column_ID = "ID";//-
        internal static String sFPD_A_Column_DATE = "DATE";//-
        internal static String sFPD_A_Column_AirlineCode = "AIRLINE CODE";//-
        internal static String sFPD_A_Column_FlightN = "FLIGHT N";//-
        internal static String sFPD_A_Column_AirportCode = "AIRPORT CODE";//-
        internal static String sFPD_A_Column_FlightCategory = "FLIGHT CATEGORY";//-
        internal static String sFPD_A_Column_AircraftType = "AIRCRAFT TYPE";//-
        internal static String sFPD_A_Column_NbSeats = "NB Seats"; //-

        internal static String sFPD_A_Column_TerminalGate = "Gate Terminal"; //-
        internal static String sFPD_A_Column_TerminalParking = "Parking Terminal";//-
        internal static String sFPD_A_Column_Parking = "Parking Stand";//-
        internal static String sFPD_A_Column_RunWay = "RunWay";//-
        internal static String sFPD_A_Column_User1 = "User 1";//-
        internal static String sFPD_A_Column_User2 = "User 2";//-
        internal static String sFPD_A_Column_User3 = "User 3";//-
        internal static String sFPD_A_Column_User4 = "User 4";//-
        internal static String sFPD_A_Column_User5 = "User 5";//-    

        internal static string sFPD_A_Column_ResourceType_Value = "Resource Type";
        #endregion

        #region Columns FPA
        internal static String sFPA_Column_STA = "STA";//-
        internal static String sFPA_Column_NoBSM = "No BSM";//-
        internal static String sFPA_Column_CBP = "CBP";//-
        internal static String sFPA_Column_ArrivalGate = "Arrival Gate";//-
        internal static String sFPA_Column_TerminalReclaim = "Reclaim Terminal";//-
        internal static String sFPA_Column_ReclaimObject = "Reclaim Belt";//-
        internal static String sFPA_Column_TransferInfeedObject = "Transfer Infeed";//-
        internal static String sFPA_Column_TerminalInfeedObject = "Infeeds Terminal";//-
        internal static String sFPA_Column_StartArrivalInfeedObject = "Arrival Infeed start";//-
        internal static String sFPA_Column_EndArrivalInfeedObject = "Arrival Infeed end";//-
        #endregion

        #region Columns FPLinksTable
        internal static String sFPLinks_Column_FPAID = "FPA ID";//-
        internal static String sFPLinks_Column_FPDID = "FPD ID";//-
        internal static String sFPLinks_Column_STA = "STA";//-
        internal static String sFPLinks_Column_STD = "STD";//-
        internal static String sFPLinks_Column_FPAName = "FPA No";//-
        internal static String sFPLinks_Column_FPDName = "FPD No";//-
        internal static String sFPLinks_Column_RotationDuration = "Rotation Duration (mn)";//-
        #endregion

        #region Columns FP_AircraftTypesTable
        internal static String sFPAircraft_AircraftTypes = "AIRCRAFT TYPES";
        internal static String sFPAircraft_Description = "Description";
        internal static String sFPAircraft_Wake = "Wake";
        internal static String sFPAircraft_Body= "Body";

        internal static String sFPAircraft_NumberSeats = "Number of seats";
        internal static String sTableColumn_ULDLoose = "ULD / Loose";
        internal static String sTableContent_ULD = "ULD";
        internal static String sTableContent_Loose = "Loose";
        #endregion

        #region Columns FP_AirlineCodesTable
        internal static String sFPAirline_AirlineCode = "AIRLINE CODES";
        internal static String sFPAirline_Description = "Description";
        internal static String sFPAirline_GroundHandlers = "Ground Handlers";
        #endregion

        #region Columns FP_AirportCodesTable
        internal static String sFPAirport_AirportCode = "AIRPORT CODES";
        internal static String sFPAirport_Description = sFPAirline_Description;
        #endregion

        #region Columns FP_FlightCategoriesTable
        internal static String sFPFlightCategory_FC = "FLIGHT CATEGORIES";
        internal static String sFPFlightCategory_Description = sFPAirline_Description;
        #endregion

        #region Common Columns
        internal static String sColumnSelectACategory = "Select a category";
        internal static String sColumnBegin= "Begin";
        internal static String sColumnEnd = "End";
        internal static String sColumnTo = "To";
        internal static String sColumnNbBags = "NbBags";
        internal static String sColumnNbVisitors = "NbVisitors";
        internal static String sColumnNbTrolley = "NbTrolley";
        internal static String sColumnTime = "Time";
        internal static String sColumnSegregation = "Segregation";
        internal static String sColumnFromTo = "From/To";
        internal static string sColumnFrom = "From";
        #endregion

        #region Colums Saturation Parameters
        internal static String sSaturation_ApplyRules = "Apply local saturation rules"; // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
        internal static String sSaturation_Filling= "Filling Queue";
        internal static String sSaturation_Opened = "% Opened";
        internal static String sSaturation_Accumulation = "Accumulation";
        internal static String sSaturation_ReactionTime = "Reaction Time (mn)";
        #endregion

        internal const string MFF_FILE_SEED_XML_ATTRIBUTE_NAME = "MFFSeed"; // >> Bug #14900 MFF file not created
        internal const string USE_SIMULATION_ENGINE_SEED_XML_ATTRIBUTE_NAME = "UseSimulationEngineSeed"; // >> Bug #14900 MFF file not created

        // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
        #region Station Filling Queues
        internal const String FIRST_STATION_FILLING_TYPE = "First";
        internal const String SATURATE_STATION_FILLING_TYPE = "Saturate";
        internal const String RANDOM_STATION_FILLING_TYPE = "Random";
        internal static List<String> ALLOWED_FILLIG_TYPES_LIST = new List<String>(new String[] { FIRST_STATION_FILLING_TYPE,
            SATURATE_STATION_FILLING_TYPE, RANDOM_STATION_FILLING_TYPE});

        internal const String STATION_GLOBAL_FILLING_TYPE_XML_ATTRIBUTE_NAME = "StationGlobalFillingType";

        internal const String saturationParametersTableName = "Alloc_Saturation";
        #endregion
        // >> Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation

        #region Columns ProcessQueuesTable
        internal static String sCapaQueue_Element = "Element";
        internal static String sCapaQueue_QueueCapacity = "Queue Capacity";
        #endregion

        #region Columns Animated Queues
        internal static String sAnimatedQueue_Name = "Name";
        internal static String sAnimatedQueue_Value = "Value";
        #endregion

        #region Columns Times processTable
        internal static String sProcessTable_Items = "Items";
        internal static String sProcessTable_Distrib_1 = "Distrib_1";
        internal static String sProcessTable_Param1_1 = "Param1_1";
        internal static String sProcessTable_Param2_1 = "Param2_1";
        internal static String sProcessTable_Param3_1 = "Param3_1";
        internal static String sProcessTable_Distrib_2 = "Distrib_2";
        internal static String sProcessTable_Param1_2 = "Param1_2";
        internal static String sProcessTable_Param2_2 = "Param2_2";
        internal static String sProcessTable_Param3_2 = "Param3_2";
        // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        internal static String sProcessTable_WaitingTimeReference = "Waiting Time Reference";
        internal static String sProcessTable_Distrib_3 = "Distrib_3";
        internal static String sProcessTable_Param1_3 = "Param1_3";
        internal static String sProcessTable_Param2_3 = "Param2_3";
        internal static String sProcessTable_Param3_3 = "Param3_3";
        //ProcessingsTimes_V2.txt file - column names
        internal static String TERMINAL_COLUMN_NAME = "Terminal";
        internal static String GROUP_TYPE_COLUMN_NAME = "GroupType";
        internal static String GROUP_INDEX_COLUMN_NAME = "GroupIndex";
        internal static String DISTRIBUTION_INDEX_COLUMN_NAME = "IndexDistrib";
        internal static String WAIT_BASE_COLUMN_NAME = "WaitBase";
        internal static String DISTRIBUTION_COLUMN_NAME = "Distrib";
        internal static String FIRST_PARAMETER_COLUMN_NAME = "Param1";
        internal static String SECOND_PARAMETER_COLUMN_NAME = "Param2";
        internal static String THIRD_PARAMETER_COLUMN_NAME = "Param3";
        // text files for Automod
        internal static String PROCESSING_TIMES_TEXT_FILE_NAME = "ProcessingsTimes";
        internal static String CAPA_PROCESS_TEXT_FILE_NAME = "CapaProcess";
        internal static String SCHEDULE_CAPA_PROCESS_FILE_NAME = "ScheduleCapaProcess";
        internal static String CAPA_PROCESS_VERSION_COLUMN_NAME = "CapaProcess Version";
        // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        #endregion

        // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        internal static String STA = "STA";
        internal static String STD = "STD";
        internal static String CONSTANT_DISTRIBUTION = "Constant";
        // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)

        // << Task #8758 Pax2Sim - Editor for Groups
        internal static String PROBABILITY_PROFILES_DISTRIBUTION = "Probability profiles";
        // >> Task #8758 Pax2Sim - Editor for Groups

        #region Columns Times processTable
        internal static String sItineraryTable_Group = "Group";
        internal static String sItineraryTable_NextGroup = "NextGroup";
        internal static String sItineraryTable_Weight = "Weight";
        internal static String sItineraryTable_Distribution = "Distribution";
        internal static String sItineraryTable_Param1 = "Param1";
        internal static String sItineraryTable_Param2 = "Param2";
        internal static String sItineraryTable_Param3 = "Param3";

        internal static String sItineraryTable_Succeed = "Succeed";
        #endregion

        #region Columns ProcessSchedule
        internal static String sTableProcessScheduleLong = "Process Schedule";
        internal static String sTableProcessScheduleItinerary = "Itinerary Version";
        internal static String sTableProcessScheduleProcess = "Process Version";
        internal static String sTableProcessScheduleGroupQueues = "Group Queues Version";
        internal static String sTableProcessScheduleProcessQueues = "Process Queues Version";
        // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        internal static String sTableProcessScheduleProcessCapacity = "Process Capacity Version";        
        // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        #endregion

        #region Columns Parking
        internal static String sParkingTable_Parking = "Parking";
        internal static String sParkingTable_Shuttle= "Shuttle";
        #endregion

        #region Columns Car park
        internal static string sInitialStateColumn_PaxInOut = "PAX In/Out Locations data";
        internal static string sInitialStateColumn_StartUpValue = "Start-up value";

        #endregion

        #region Lines Load Factors
        internal const String sLFD_A_Line_Full = "% Full";//-
        internal const String sLFD_A_Line_C = "% C";//- First class
        internal const String sLFD_A_Line_Y = "% Y";//- Second class
        internal const String sLFD_A_Line_Local = "% Local";//-
        internal const String sLFD_A_Line_NotLocal = "% Not Local";//-
        internal const String sLFD_A_Line_Transferring = "% Transferring";//-
        internal const String sLFD_A_Line_ReCheck = "% Transfer Re-Check";//-
        internal const String sLFD_A_Line_TransferDesk = "% Transfer Desk";//-
        internal const String sLFD_A_Line_OOGTransf = "% Transfer Bags Out Of Gauge (OOG)";
        #region //SGE-26/03/2012-Begin
#if(PARKINGMULHOUSE)
        internal const String sLFD_A_Line_NbPaxPerCar = "Number of passengers per car";    // AndreiZaharia / Mulhouse project
#endif
        #endregion //SGE-26/03/2012-End
        #endregion

        #region Lines Departure Load Factors
        internal const String sLFD_Line_SelfCI = "% Self Service Check In";//-
        internal const String sLFD_Line_Originating = "% Originating";//-
        internal const String sLFD_Line_OOGOrig = "% Originating Bags Out Of Gauge (OOG)";
        #endregion

        #region Lines Arrival Load Factors
        internal const String sLFA_Line_Terminating = "% Terminating";//-
        internal const String sLFA_Line_OOGTerm = "% Terminating Bags Out Of Gauge (OOG)";
        #endregion

        #region Lignes OCT_CI
        internal static String sOCT_CI_Line_Opening = "CI Opening Time (Min before STD)";
        internal static String sOCT_CI_Line_Closing = "CI Closing Time (Min before STD)";
        internal static String sOCT_CI_Line_Allocated = "Number of allocated CI";
        // >> Bug #13367 Liege allocation
        internal static String sOCT_CI_Line_PartialOpeningTime = "CI Partial Opening Time (Min before STD)";
        internal static String sOCT_CI_Line_NbStationsOpenedAtPartial = "Number of Opened CI at Partial Opening";
        internal static String sOCT_CI_Line_NbAdditionalStationsForOverlappingFlights = "Number of Additional CI for overlapping flights";
        // << Bug #13367 Liege allocation
        #endregion

        #region Lignes OCT_Boarding gate
        internal static String sOCT_Board_Line_Opening = "Boarding Gate Opening Time (Min before STD)";
        internal static String sOCT_Board_Line_Closing = "Boarding Gate Closing Time (Min before STD)";
        #endregion

        #region Rows OCT_Arrival gate
        internal static String sOCT_Arrival_Gate_Line_Opening = "Arrival Gate Opening Time (Min after STA)";
        internal static String sOCT_Arrival_Gate_Line_Closing = "Arrival Gate Closing Time (Min after STA)";
        #endregion

        #region Lignes OCT_Baggage claim
        internal static String sOCT_Baggage_Line_Opening = "Baggage Claim Opening Time (Min after STA)";
        internal static String sOCT_Baggage_Line_Closing = "Baggage Claim Closing Time (Min after STA)";
        internal static String sOCT_Baggage_Line_ContainerSize = "Container size (baggage)";
        internal static String sOCT_Baggage_Line_NumberProcessedContainer = "Number of processed container per cycle";
        internal static String sOCT_Baggage_Line_DeadTime = "Dead time between two cycles (minutes)";

        #endregion

        #region Lignes OCT_Parking
        internal static String sOCT_ParkingOpening ="Parking Opening Time (Min after STA or before STD)";
        internal static String sOCT_ParkingClosing  ="Parking Closing Time (Min after STA or before STD)" ;
        #endregion

        // >> Bug #13367 Liege allocation
        #region Parking Priorities Rows
        internal static string parkingPrioritiesRowP20 = "Parking_20";
        internal static string parkingPrioritiesRowP21 = "Parking_21";
        internal static string parkingPrioritiesRowP22 = "Parking_22";
        internal static string parkingPrioritiesRowP23 = "Parking_23";
        internal static string parkingPrioritiesRowP24 = "Parking_24";
        internal static string parkingPrioritiesRowP25 = "Parking_25";
        internal static string parkingPrioritiesRowP26 = "Parking_26";
        internal static string parkingPrioritiesRowP27 = "Parking_27";
        internal static string parkingPrioritiesRowP28 = "Parking_28";

        internal static string parkingPrioritiesRowP41 = "Parking_41";
        internal static string parkingPrioritiesRowP42 = "Parking_42";
        internal static string parkingPrioritiesRowP43 = "Parking_43";
        #endregion

        #region Boarding Gates Priorities Rows
        internal static string boardingGatesPrioritiesRowBG1 = "BoardingGate_1";
        internal static string boardingGatesPrioritiesRowBG2 = "BoardingGate_2";
        internal static string boardingGatesPrioritiesRowBG3 = "BoardingGate_3";
        internal static string boardingGatesPrioritiesRowBG4 = "BoardingGate_4";
        #endregion
        // << Bug #13367 Liege allocation

        #region Lignes OCT_MakeUp
        internal static String sOCT_MakeUpOpening ="Make-Up Opening Time (Min before STD)";
        internal static String sOCT_MakeUpClosing  ="Make-Up Closing Time (Min before STD)" ;
        internal static String sOCT_MakeUpEBS_Delivery = "EBS delivery time (Min before STD)";
        internal static String sOCT_MakeUpAllocatedMup  ="Number of allocated Make-Up per flight" ;
        internal static String sOCT_MakeUpPartialOpening ="Make-Up Partial Opening Time (Min before STD)";
        internal static String sOCT_MakeUpPartialAllocatedMup  = "Number of Opened Make-Up at Partial Opening/Closing" ;
        
        internal static String sOCT_MakeUpSegregationNumber ="Segregation number";
        internal static String sOCT_MakeUpContainerSize ="Container size"  ;
        internal static String sOCT_MakeUpDeadTime ="DeadTime (Time to change the container) (s)";
        internal static String sOCT_MakeUpNumberContainerLateral ="Number of container per Lateral"  ;
        #endregion

        #region Rows OCT_Arrival Infeed
        internal static String sOCT_Arrival_Infeed_Line_Opening = "Arrival Infeed Opening Time (Min after STA)";
        internal static String sOCT_Arrival_Infeed_Line_Closing = "Arrival Infeed Closing Time (Min after STA)";
        #endregion

        #region Lignes OCT_TransferInfeed
        internal static String sOCT_TransferInfeedOpening ="Tranfer Opening Time (Min after STA)";
        internal static String sOCT_TransferInfeedClosing  = "Transfer Closing Time (Min after STA)";
        internal static String sOCT_TransferInfeedAllocatedInfeed = "Number of allocated Transfer Infeed per flight";
        #endregion 

        #region Rows OCT_BaggDrop
        internal static String sOCT_BagDropOpening = "Baggage Drop Opening Time (Min before STD)";
        internal static String sOCT_BagDropClosing = "Baggage Drop Closing Time (Min before STD)";
        #endregion

        #region Rows OCT_Runway
        internal static String sOCT_RunwayOpening = "Runway Opening Time (Min after STA or before STD)";
        internal static String sOCT_RunwayClosing = "Runway Closing Time (Min after STA or before STD)";
        #endregion

        #region Lignes BaggageClaimConstraint
        internal static String sBaggageClaimConstraintInfeedNumber ="Infeed number";
        internal static String sBaggageClaimConstraintInfeedSpeed ="Infeed speed (sec/bags)";
        internal static String sBaggageClaimConstraintLimitBags ="Limit Bag Acceptance (Number of bags)";
        internal static String sBaggageClaimConstraintMinNumberBags ="Number of Bag Min";
        internal static String sBaggageClaimConstraintMaxNumberBag ="Number of Bag Max"; 
        internal static String sBaggageClaimConstraintExcludedCategories ="Excluded categories";
        internal static String sBaggageClaimConstraintExcludedGroundHandler ="Excluded Ground Handler";
        internal static String sBaggageClaimConstraintExcludedAirline ="Excluded Airline Code";
        internal static String sBaggageClaimConstraintExcludedFlight = "Excluded Flight";
        internal static String sBaggageClaimConstraintExcludedContainerType = "Excluded Container type";
        #endregion                                      

        #region Lines et Columns BHS General
        internal static String sBHSGeneralTable_Data= "Data";
        internal static String sBHSGeneralTable_Value = "Value";

        internal static String sBHSGeneralTable_NumberArrivalInfeed = "Number of Arrival Infeeds";
        internal static String sBHSGeneralTable_LastArrivalInfeed = "Last index of Arrival Infeeds";
        internal static String sBHSGeneralTable_NumberCI = "Number of CI desks";
        internal static String sBHSGeneralTable_LastCI = "Last index of CI desks";
        internal static String sBHSGeneralTable_NumberTransferInfeed = "Number of Transfer Infeeds";
        internal static String sBHSGeneralTable_LastTransferInfeed = "Last index of Transfer Infeeds";
        internal static String sBHSGeneralTable_NumberHBS1 = "Number of HBS Lev.1 machines";
        internal static String sBHSGeneralTable_LastHBS1 = "Last index of HBS Lev.1 machines";
        internal static String sBHSGeneralTable_NumberHBS3 = "Number of HBS Lev.3 machines";
        internal static String sBHSGeneralTable_LastHBS3 = "Last index of HBS Lev.3 machines";
        internal static String sBHSGeneralTable_NumberME = "Number of ME Station";
        internal static String sBHSGeneralTable_LastME = "Last index of ME Station";
        internal static String sBHSGeneralTable_NumberMUP = "Number of Make-Up positions";
        internal static String sBHSGeneralTable_LastMUP = "Last index of Make-Up positions";
        internal static String sBHSGeneralTable_NumberCIColl = "Number of CI Collectors";
        internal static String sBHSGeneralTable_LastCIColl = "Last index of CI Collectors";
        internal static String sBHSGeneralTable_WindowColl = "Window reservation Collectors ? (0/1)";
        internal static String sBHSGeneralTable_WindowSize = "Window size (m)";

        #endregion
        
        #region  Columns BHS_ArrivalInfeed_Group
        internal static String sBHS_ArrivalInfeed_Group_ArrGroup = "Arrival Infeed Group";
        internal static String sBHS_ArrivalInfeed_Group_ArrStart = "Arrival Infeed Start";
        internal static String sBHS_ArrivalInfeed_Group_ArrEnd = "Arrival Infeed End";

        #endregion

        #region Columns BHS_CI_Group
        internal static String sBHS_CI_Groups_CIGroup = "CI Group";
        internal static String sBHS_CI_Groups_CIStart = "CI Start";
        internal static String sBHS_CI_Groups_CIEnd = "CI End";

        #endregion

        #region  Columns BHS_TransferInfeed_Group
        internal static String sBHS_TransferInfeed_Group_ArrGroup = "Transfer Infeed Group";
        internal static String sBHS_TransferInfeed_Group_ArrStart = "Transfer Infeed Start";
        internal static String sBHS_TransferInfeed_Group_ArrEnd = "Transfer Infeed End";

        #endregion

        #region  Columns sBHS_CI_Collectors
        internal static String sBHS_CI_Collectors_Collector = "Collector";
        internal static String sBHS_CI_Collectors_CI_Desk_Start = "CI_Desk_Start";
        internal static String sBHS_CI_Collectors_CI_Desk_End = "CI_Desk_End";
        #endregion

        #region  Columns sBHS_CI_Routing
        internal static String sBHS_CI_RoutingCI_Group = "CI_Group";
        internal static String sBHS_CI_RoutingHBS1_Start = "HBS1_Start";
        internal static String sBHS_CI_RoutingHBS1_End = "HBS1_End";
        internal static String sBHS_CI_RoutingHBS3_Start = "HBS3_Start";
        internal static String sBHS_CI_RoutingHBS3_End = "HBS3_End";
        internal static String sBHS_CI_RoutingMES_Start = "MES_Start";
        internal static String sBHS_CI_RoutingMES_End = "MES_End";
        internal static String sBHS_CI_RoutingMakeUp_Start = "MakeUp_Start";
        internal static String sBHS_CI_RoutingMakeUp_End = "MakeUp_End";
        #endregion

        #region  Columns sBHS_HBS3_Routing
        internal static String sBHS_HBS3_Routing_HBS3 = "HBS3";
        internal static String sBHS_HBS3_RoutingMES_Start = "MES_Start";
        internal static String sBHS_HBS3_RoutingMES_End = "MES_End";
        internal static String sBHS_HBS3_RoutingHBS1_Start = "MakeUp_Start";
        internal static String sBHS_HBS3_RoutingHBS1_End = "MakeUp_End";
        #endregion

        #region  Columns sBHS_Transfer_Routing
        internal static String sBHS_Transfer_RoutingInfeed_Group = "Trans_Infeed";
        internal static String sBHS_Transfer_RoutingHBS1_Start = "HBS1_Start";
        internal static String sBHS_Transfer_RoutingHBS1_End = "HBS1_End";
        internal static String sBHS_Transfer_RoutingHBS3_Start = "HBS3_Start";
        internal static String sBHS_Transfer_RoutingHBS3_End = "HBS3_End";
        internal static String sBHS_Transfer_RoutingMES_Start = "MES_Start";
        internal static String sBHS_Transfer_RoutingMES_End = "MES_End";
        internal static String sBHS_Transfer_RoutingMakeUp_Start = "MakeUp_Start";
        internal static String sBHS_Transfer_RoutingMakeUp_End = "MakeUp_End";
        #endregion

        #region  Lines et Columns sBHS_Flow_Split
        internal static String sBHS_Flow_Split_Flows = "Flows";
        internal static String sBHS_Flow_Split_ToHBS2 = "% to HBS Level 2";
        internal static String sBHS_Flow_Split_ToHBS3 = "% to HBS Level 3";
        internal static String sBHS_Flow_Split_ToHBS4 = "% to HBS Level 4";
        internal static String sBHS_Flow_Split_ToHBS5 = "% to HBS Level 5";
        internal static String sBHS_Flow_Split_ToMESOri = "% MES (Originating Bags)";
        internal static String sBHS_Flow_Split_ToMESTrans = "% MES (Transfer Bags)";
        internal static String sBHS_Flow_Split_InterlinkTrans = "% Interlink (Transfer Bags)-optional";
        internal static String sBHS_Flow_Split_InterlinkOrig = "% Interlink (Originating Bags)-optional";
        #endregion

        #region  Lines et Columns sBHS_Process
        internal static String sBHS_Process_ProcessData = "BHS Process data";
        internal static String sBHS_Process_Value = "Value";


        internal static String sBHS_Process_CIProcess = "Check-In Bag process time (Mean Flows mode) (sec)";
        internal static String sBHS_Process_MESShortRate = "MES Short Process rate (%)";
        internal static String sBHS_Process_MESShortProcess = "MES Short process time (sec)";
        internal static String sBHS_Process_MESLongProcess = "MES Long process time (sec)";

        internal static String sBHS_Process_HBS1Spac = "HBS Lev.1 spacing (m)";
        internal static String sBHS_Process_HBS1Velocity = "HBS Lev.1 Velocity (m/s)";

        internal static String sBHS_Process_HBS2Min = "HBS Lev.2 process time Min (sec)";
        internal static String sBHS_Process_HBS2Mode = "HBS Lev.2 process time Mode (sec)";
        internal static String sBHS_Process_HBS2Max = "HBS Lev.2 process time Max (sec)";
        internal static String sBHS_Process_HBS2Timeout = "HBS Lev.2 TimeOut (sec)";
        internal static String sBHS_Process_HBS2Operators = "HBS Lev.2 Operators";

        internal static String sBHS_Process_HBS3Min = "HBS Lev.3 process time Min (sec)";
        internal static String sBHS_Process_HBS3Mode = "HBS Lev.3 process time Mode (sec)";
        internal static String sBHS_Process_HBS3Max = "HBS Lev.3 process time Max (sec)";
        internal static String sBHS_Process_HBS3Spac  = "HBS Lev.3 spacing (m)";
        internal static String sBHS_Process_HBS3Velocity = "HBS Lev.3 Velocity (m/s)";

        internal static String sBHS_Process_HBS4Min = "HBS Lev.4 process time Min (sec)";
        internal static String sBHS_Process_HBS4Mode = "HBS Lev.4 process time Mode (sec)";
        internal static String sBHS_Process_HBS4Max = "HBS Lev.4 process time Max (sec)";
        internal static String sBHS_Process_HBS4Timeout = "HBS Lev.4 TimeOut (sec)";
        internal static String sBHS_Process_HBS4Operators = "HBS Lev.4 Operators";
        internal static String sBHS_Process_HBS4HoldInside = "HBS Lev.4 Hold inside ? (0/1)";

        internal static String sBHS_Process_HBS5Min = "HBS Lev.5 process time Min (sec)";
        internal static String sBHS_Process_HBS5Mode = "HBS Lev.5 process time Mode (sec)";
        internal static String sBHS_Process_HBS5Max = "HBS Lev.5 process time Max (sec)";

        internal static String sBHS_Process_MupPickUp = "Make-Up PickUp throughput (bag/h)";
        internal static String sBHS_Process_Transf = "Transfer Unload throughput (bag/h)";
        internal static String sBHS_Process_Arrival = "Arrival Unload throughput (bag/h)";
        internal static String sBHS_Process_NumberTray = "Number of Tray Sorters";
        internal static String sBHS_Process_SorterVelocity = "Sorter velocity (m/s)";
        internal static String sBHS_Process_SorterTrayLength = "Sorter tray length (m)";
        internal static String sBHS_Process_SorterFillingLimit = "Sorter filling limit (%)";
        internal static String sBHS_Process_SorterRecirLimit = "Sorter recirculation limit (nb laps)";
        internal static String sBHS_Process_SorterTiltInterval = "Sorter Tilt interval (trays)";
        internal static String sBHS_Process_SorterInductInterval = "Sorter Induction interval (trays)";
        internal static String sBHS_Process_EBSOperating = "EBS operating ? (0/1)";
        #endregion
        
        #region  Lines pour sBHS_Arrival_Containers
        internal static String sBHS_Arrival_Containers_FirstContainer = "First Container delay (minutes)";
        internal static String sBHS_Arrival_Containers_ContainerSize = "Container size";
        internal static String sBHS_Arrival_Containers_NumberLaterals = "Number of laterals";
        #endregion

        #region Columns PaxPlan
        public static String sPaxPlan_PaxID = "PAX_ID";
        public static String sPaxPlan_CreationTime = "CreationTime";
        public static String sPaxPlan_FPAID = "FPA_ID";
        public static String sPaxPlan_FPAClass = "FPA_Class";
        public static String sPaxPlan_FPDID = "FPD_ID";
        public static String sPaxPlan_FPDClass = "FPD_Class";
        public static String sPaxPlan_FPDSTD = "FPD_STD";
        public static String sPaxPlan_SelfCI = "Self CheckIn";
        public static String sPaxPlan_Visitors = "NbVisitors";
        public static String sPaxPlan_BagsIG = "NbBags IG";
        public static String sPaxPlan_BagsOOG = "NbBags OOG";
        public static String sPaxPlan_Trolleys = "NBTrolleys";
        public static String sPaxPlan_Segregation = "Segregation";
        public static String sPaxPlan_Local = "PassportLocal";
        public static String sPaxPlan_Transfer = "Transfer";
        public static String sPaxPlan_TerminalInOut = "TerminalInOut";
        public static String sPaxPlan_InOut = "InOut";
        // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        public static String sPaxPlan_DelayTime = "Delay Time (min)";
        // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        

        #region //SGE-26/03/2012-Begin
#if(PARKINGMULHOUSE)
        public static String sPaxPlan_Car = "Car";
#endif
        #endregion //SGE-26/03/2012-End
        #endregion

        #region extra columns Sodexi - FPD Statistics Extended
        public static String FPDStatisticsExtendedName = "FPD Statistics Extended";
        public static String FPDStatisticsExtended_ItemTotal = "Item Total";
        public static String FPDStatisticsExtended_WeightTotal = "Weight Total";
        public static String FPDStatisticsExtended_VolumeTotal = "Volume Total";
        #endregion

        #region FlightPlanInformation table name
        internal static String FPI_TableName = "Flight Plan Information";
        internal static String FPI_Column_Allocation_Type = "Allocation Type";
        internal static String FPI_Column_Resource_Type_Name = "Resource Type";
        // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
        internal static String FPI_Column_Container_Size = "Container size";
        internal static String FPI_Column_Nb_Passengers = "Number of Passengers";
        internal static String FPI_Column_Nb_Bags = "Number of Bags";
        internal static String FPI_Column_CalculationType = "Calcultation type for nb of positions";
        internal static String FPI_Column_OCTTableUsed = "OCT table used";
        internal static String FPI_Column_NbOfResourcesUsed = "Nb of Desks used";   // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column

        public static string FPI_FIRST_DESK_COLUMN_SUFFIX = " First Desk";
        public static string FPI_LAST_DESK_COLUMN_SUFFIX = " Last Desk";
        public static string FPI_OPENING_TIME_COLUMN_SUFFIX = " Opening Time";
        public static string FPI_CLOSING_TIME_COLUMN_SUFFIX = " Closing Time";
        public static string FPI_TERMINAL_NB_COLUMN_SUFFIX = " Terminal Nb";

        //calculation types for number of positions(stations)        
        internal static String OCT_table_based_calc_type = "OCT table based";
        internal static String container_size_based_calc_type = "Container size based";
        internal static String FP_and_ContainerSize_based_calc_type = "FP as basis and Container size";
        internal static String FP_and_OCT_table_based_calc_type = "FP as basis and OCT table";
        // >> Task #9465 Pax2Sim - Allocation - Add FPI Columns
        #endregion

        #region Static Allocation table names
        internal static String StaticAlloc_DepartureParking_TableName = "Alloc_DepartureParkingTable";
        internal static String StaticAlloc_ArrivalParking_TableName = "Alloc_ArrivalParkingTable";
        #endregion

        #region Dynamic Allocation Types (for FPInformation table)
        internal static String MakeUpDynamicAllocation = "Make-Up";
        internal static String CheckInDynamicAllocation = "Check-In";
        internal static String BoardingGateDynamicAllocation = "Boarding-Gate";
        internal static String BagDropDynamicAllocation = "Bag-Drop";
        internal static String TransferInfeedDynamicAllocation = "Transfer-Infeed";
        internal static String BaggageClaimDynamicAllocation = "Baggage-Claim";
        #endregion

        // << Task #7530 Analyze Bag Trace module - Recirculation statistics        
        #region BagTrace Analysis
        // Keys for the HashTable that contains the max nb of recirculations 
        // for Terminating / Departing / Originating and Transfering Baggs
        internal static String maxNbOfRecirculationsDep = "maxNbOfRecirculationsDep";
        internal static String maxNbOfRecirculationsOrig = "maxNbOfRecirculationsOrig";
        internal static String maxNbOfRecirculationsTransf = "maxNbOfRecirculationsTransf";
        internal static String maxNbOfRecirculationsTerm = "maxNbOfRecirculationsTerm";        
        #endregion
        // >> Task #7530 Analyze Bag Trace module - Recirculation statistics

        // << Task #8069 Pax Capacity Analysis - statistics missing from the Pax_Time_Distrib tables
        #region Pax Capacity Analysis - Dwell Times tables column
        internal static String TotalTimeToBoardingGateDistributionColumnName = "TotalTimeToBoardingGateDistribution";
        #endregion
        // >> Task #8069 Pax Capacity Analysis - statistics missing from the Pax_Time_Distrib tables

        // << Task #8246 Pax2Sim - Dynamic Analysis - Bag Trace analysis - split the total time into smaller intervals
        /// <summary>
        /// number of time intervals in which the initial time interval is split for the RemainingTime tables from the Dynamic Analysis
        /// </summary>
        public static int nbTimeIntervalsForRemainingTimeTable = 41;
        // >> Task #8246 Pax2Sim - Dynamic Analysis - Bag Trace analysis - split the total time into smaller intervals

        // << Task #8589 Pax2Sim - Waiting Time Per Period of Time for PAX
        public static String DWELL_TIME_TABLE_NAME_SUFIX = "_Dwell Area";
        // >> Task #8589 Pax2Sim - Waiting Time Per Period of Time for PAX

        // << Task #8604 Pax2Sim - Log File - accessible while running the application
        public static String NEW_APPLICATION_INSTANCE = "Application Startup";
        // >> Task #8604 Pax2Sim - Log File - accessible while running the application

        // << Task #8618 Pax2Sim - Dynamic Analysis - Pax - histograns and timeline tables for Dwell Area
        public static String GLOBAL_PAX_DISTRIBUTION_TIMELINE_TABLE_NAME = "Pax_Time_Distrib_Global_Timeline";        
        public static String DEPARTURE_PAX_DISTRIBUTION_TIMELINE_TABLE_NAME = "Pax_Time_Distrib_Departure_Timeline";
        public static String TRANSFER_PAX_DISTRIBUTION_TIMELINE_TABLE_NAME = "Pax_Time_Distrib_Transfer_Timeline";
        public static String ORIGINATING_PAX_DISTRIBUTION_TIMELINE_TABLE_NAME = "Pax_Time_Distrib_Originating_Timeline";
        public static String TERMINATING_PAX_DISTRIBUTION_TIMELINE_TABLE_NAME = "Pax_Time_Distrib_Terminating_Timeline";
        public static String LOST_PAX_DISTRIBUTION_TIMELINE_TABLE_NAME = "Pax_Time_Distrib_Lost_Timeline";
        public static String MISSED_PAX_DISTRIBUTION_TIMELINE_TABLE_NAME = "Pax_Time_Distrib_Missed_Timeline";
        public static String STOPPED_PAX_DISTRIBUTION_TIMELINE_TABLE_NAME = "Pax_Time_Distrib_Stopped_Timeline";

        public static String PAX_DISTRIBUTION_TABLE_PREFIX = "Pax_Time_Distrib_";
        public static String PAX_SUMMARY_DISTRIBUTION_TABLE_SUFFIX = "_Reports";

        public static String GLOBAL_PAX_TYPE = "Global";
        public static String ARRIVAL_PAX_TYPE = "Arrival";
        public static String DEPARTURE_PAX_TYPE = "Departure";
        public static String TRANSFER_PAX_TYPE = "Transfer";
        public static String ORIGINATING_PAX_TYPE = "Originating";
        public static String TERMINATING_PAX_TYPE = "Terminating";
        public static String LOST_PAX_TYPE = "Lost";
        public static String MISSED_PAX_TYPE = "Missed";
        public static String STOPPED_PAX_TYPE = "Stopped";

        public const String SUMMARY_KPI_NAME_TOTAL_WALKING_TIME = "Total Walking Time";
        public const String SUMMARY_KPI_NAME_TOTAL_WAITING_TIME = "Total Waiting Time";
        public const String SUMMARY_KPI_NAME_TOTAL_PROCESS_TIME_PAX = "Total Process Time";
        public const String SUMMARY_KPI_NAME_TOTAL_TIME_TO_BG = "Total Time To Boarding Gate";

        public static List<String> SUMMARY_PAX_DISTRIBUTION_TABLE_KPI_NAMES
            = new List<String>(new String[] { SUMMARY_KPI_NAME_TOTAL_TIME, SUMMARY_KPI_NAME_TOTAL_WALKING_TIME, SUMMARY_KPI_NAME_TOTAL_WAITING_TIME, 
                                                SUMMARY_KPI_NAME_TOTAL_PROCESS_TIME_PAX, SUMMARY_KPI_NAME_TOTAL_TIME_TO_BG });
        // >> Task #8618 Pax2Sim - Dynamic Analysis - Pax - histograns and timeline tables for Dwell Area

        // << Task #8793 Pax2Sim - Statistics - _IST table modification
        #region _IST tables - extra columns
        internal static String IST_Column_EntryTime = "Entry Time";
        internal static String IST_Column_ExitTime = "Exit Time";
        #endregion
        // >> Task #8793 Pax2Sim - Statistics - _IST table modification

        // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
        #region Peak Flows: FPD Airline segregation table names
        internal static String fpdOutboundSmoothedPAXOriginating_Airline = "FPD_OutBoundRolling_PAX_Originating_Airline";
        internal static String fpdOutboundInstantaneousPAXOriginating_Airline = "FPD_OutBoundInstantaneous_PAX_Originating_Airline";

        internal static String fpdOutboundSmoothedPAXTransferring_Airline = "FPD_OutBoundRolling_PAX_Transfering_Airline";
        internal static String fpdOutboundInstantaneousPAXTransferring_Airline = "FPD_OutBoundInstantaneous_PAX_Transfering_Airline";

        internal static String fpdOutboundSmoothedBagOriginating_Airline = "FPD_OutBoundRolling_Bag_Originating_Airline";
        internal static String fpdOutboundInstantaneousBagOriginating_Airline = "FPD_OutBoundInstantaneous_Bag_Originating_Airline";

        internal static String fpdOutboundSmoothedBagTransferring_Airline = "FPD_OutBoundRolling_Bag_Transfering_Airline";
        internal static String fpdOutboundInstantaneousBagTransferring_Airline = "FPD_OutBoundInstantaneous_Bag_Transfering_Airline";
        #endregion

        #region Peak Flows: FPA Airline segregation table names
        internal static String fpaInboundSmoothedPAXTerminating_Airline = "FPA_InBoundRolling_PAX_Terminating_Airline";
        internal static String fpaInboundInstantaneousPAXTerminating_Airline = "FPA_InBoundInstantaneous_PAX_Terminating_Airline";

        internal static String fpaInboundSmoothedPAXTransferring_Airline = "FPA_InBoundRolling_PAX_Transfering_Airline";
        internal static String fpaInboundInstantaneousPAXTransferring_Airline = "FPA_InBoundInstantaneous_PAX_Transfering_Airline";

        internal static String fpaInboundSmoothedBagTerminating_Airline = "FPA_InBoundRolling_Bag_Terminating_Airline";
        internal static String fpaInboundInstantaneousBagTerminating_Airline = "FPA_InBoundInstantaneous_Bag_Terminating_Airline";

        internal static String fpaInboundSmoothedBagTransferring_Airline = "FPA_InBoundRolling_Bag_Transfering_Airline";
        internal static String fpaInboundInstantaneousBagTransferring_Airline = "FPA_InBoundInstantaneous_Bag_Transfering_Airline";
        #endregion

        #region Peak Flows: FPD Terminal segregation table names
        internal static String fpdOutboundSmoothedPAXOriginating_Terminal = "FPD_OutBoundRolling_PAX_Originating_Terminal";
        internal static String fpdOutboundInstantaneousPAXOriginating_Terminal = "FPD_OutBoundInstantaneous_PAX_Originating_Terminal";

        internal static String fpdOutboundSmoothedPAXTransferring_Terminal = "FPD_OutBoundRolling_PAX_Transfering_Terminal";
        internal static String fpdOutboundInstantaneousPAXTransferring_Terminal = "FPD_OutBoundInstantaneous_PAX_Transfering_Terminal";

        internal static String fpdOutboundSmoothedBagOriginating_Terminal = "FPD_OutBoundRolling_Bag_Originating_Terminal";
        internal static String fpdOutboundInstantaneousBagOriginating_Terminal = "FPD_OutBoundInstantaneous_Bag_Originating_Terminal";

        internal static String fpdOutboundSmoothedBagTransferring_Terminal = "FPD_OutBoundRolling_Bag_Transfering_Terminal";
        internal static String fpdOutboundInstantaneousBagTransferring_Terminal = "FPD_OutBoundInstantaneous_Bag_Transfering_Terminal";
        #endregion

        #region Peak Flows: FPA Termianl segregation table names
        internal static String fpaInboundSmoothedPAXTerminating_Terminal = "FPA_InBoundRolling_PAX_Terminating_Terminal";
        internal static String fpaInboundInstantaneousPAXTerminating_Terminal = "FPA_InBoundInstantaneous_PAX_Terminating_Terminal";

        internal static String fpaInboundSmoothedPAXTransferring_Terminal = "FPA_InBoundRolling_PAX_Transfering_Terminal";
        internal static String fpaInboundInstantaneousPAXTransferring_Terminal = "FPA_InBoundInstantaneous_PAX_Transfering_Terminal";

        internal static String fpaInboundSmoothedBagTerminating_Terminal = "FPA_InBoundRolling_Bag_Terminating_Terminal";
        internal static String fpaInboundInstantaneousBagTerminating_Terminal = "FPA_InBoundInstantaneous_Bag_Terminating_Terminal";

        internal static String fpaInboundSmoothedBagTransferring_Terminal = "FPA_InBoundRolling_Bag_Transfering_Terminal";
        internal static String fpaInboundInstantaneousBagTransferring_Terminal = "FPA_InBoundInstantaneous_Bag_Transfering_Terminal";
        #endregion

        #region Peak Flows: FPA Flight Category segregation table names
        internal static String fpaInboundSmoothedPAXTotal_FC = "FPA_InBoundRolling_PAX_Total_FC";
        internal static String fpaInboundInstantaneousPAXTotal_FC = "FPA_InBoundInstantaneous_PAX_Total_FC";

        internal static String fpaInboundSmoothedPAXTerminating_FC = "FPA_InBoundRolling_PAX_Terminating_FC";
        internal static String fpaInboundInstantaneousPAXTerminating_FC = "FPA_InBoundInstantaneous_PAX_Terminating_FC";

        internal static String fpaInboundSmoothedPAXTransfering_FC = "FPA_InBoundRolling_PAX_Transferring_FC";
        internal static String fpaInboundInstantaneousPAXTransfering_FC = "FPA_InBoundInstantaneous_PAX_Transferring_FC";

        internal static String fpaInboundSmoothedBagTotal_FC = "FPA_InBoundRolling_Bag_Total_FC";
        internal static String fpaInboundInstantaneousBagTotal_FC = "FPA_InBoundInstantaneous_Bag_Total_FC";

        internal static String fpaInboundSmoothedBagTerminating_FC = "FPA_InBoundRolling_Bag_Terminating_FC";
        internal static String fpaInboundInstantaneousBagTerminating_FC = "FPA_InBoundInstantaneous_Bag_Terminating_FC";

        internal static String fpaInboundSmoothedBagTransfering_FC = "FPA_InBoundRolling_Bag_Transferring_FC";
        internal static String fpaInboundInstantaneousBagTransfering_FC = "FPA_InBoundInstantaneous_Bag_Transferring_FC";
        #endregion

        #region Peak Flows: FPD Flight Category segregation table names
        internal static String fpdOutboundSmoothedPAXTotal_FC = "FPD_OutBoundRolling_PAX_Total_FC";
        internal static String fpdOutBoundInstantaneousPAXTotal_FC = "FPD_OutBoundInstantaneous_PAX_Total_FC";

        internal static String fpdOutBoundSmoothedPAXOriginating_FC = "FPD_OutBoundRolling_PAX_Originating_FC";
        internal static String fpdOutBoundInstantaneousPAXOriginating_FC = "FPD_OutBoundInstantaneous_PAX_Originating_FC";

        internal static String fpdOutBoundSmoothedPAXTransfering_FC = "FPD_OutBoundRolling_PAX_Transfering_FC";
        internal static String fpdOutBoundInstantaneousPAXTransfering_FC = "FPD_OutBoundInstantaneous_PAX_Transfering_FC";

        internal static String fpdOutboundSmoothedBagTotal_FC = "FPD_OutBoundRolling_Bag_Total_FC";
        internal static String fpdOutBoundInstantaneousBagTotal_FC = "FPD_OutBoundInstantaneous_Bag_Total_FC";

        internal static String fpdOutBoundSmoothedBagOriginating_FC = "FPD_OutBoundRolling_Bag_Originating_FC";
        internal static String fpdOutBoundInstantaneousBagOriginating_FC = "FPD_OutBoundInstantaneous_Bag_Originating_FC";

        internal static String fpdOutBoundSmoothedBagTransfering_FC = "FPD_OutBoundRolling_Bag_Transfering_FC";
        internal static String fpdOutBoundInstantaneousBagTransfering_FC = "FPD_OutBoundInstantaneous_Bag_Transfering_FC";
        #endregion

        #region Peak Flows: FPA Flight SubCategory segregation table names
        internal static String fpaInboundSmoothedPAXTotal_FSC = "FPA_InBoundRolling_PAX_Total_FSC";
        internal static String fpaInboundInstantaneousPAXTotal_FSC = "FPA_InBoundInstantaneous_PAX_Total_FSC";

        internal static String fpaInboundSmoothedPAXTerminating_FSC = "FPA_InBoundRolling_PAX_Terminating_FSC";
        internal static String fpaInboundInstantaneousPAXTerminating_FSC = "FPA_InBoundInstantaneous_PAX_Terminating_FSC";

        internal static String fpaInboundSmoothedPAXTransfering_FSC = "FPA_InBoundRolling_PAX_Transferring_FSC";
        internal static String fpaInboundInstantaneousPAXTransfering_FSC = "FPA_InBoundInstantaneous_PAX_Transferring_FSC";

        internal static String fpaInboundSmoothedBagTotal_FSC = "FPA_InBoundRolling_Bag_Total_FSC";
        internal static String fpaInboundInstantaneousBagTotal_FSC = "FPA_InBoundInstantaneous_Bag_Total_FSC";

        internal static String fpaInboundSmoothedBagTerminating_FSC = "FPA_InBoundRolling_Bag_Terminating_FSC";
        internal static String fpaInboundInstantaneousBagTerminating_FSC = "FPA_InBoundInstantaneous_Bag_Terminating_FSC";

        internal static String fpaInboundSmoothedBagTransfering_FSC = "FPA_InBoundRolling_Bag_Transferring_FSC";
        internal static String fpaInboundInstantaneousBagTransfering_FSC = "FPA_InBoundInstantaneous_Bag_Transferring_FSC";
        #endregion

        #region Peak Flows: FPD Flight SubCategory segregation table names
        internal static String fpdOutboundSmoothedPAXTotal_FSC = "FPD_OutBoundRolling_PAX_Total_FSC";
        internal static String fpdOutBoundInstantaneousPAXTotal_FSC = "FPD_OutBoundInstantaneous_PAX_Total_FSC";

        internal static String fpdOutBoundSmoothedPAXOriginating_FSC = "FPD_OutBoundRolling_PAX_Originating_FSC";
        internal static String fpdOutBoundInstantaneousPAXOriginating_FSC = "FPD_OutBoundInstantaneous_PAX_Originating_FSC";

        internal static String fpdOutBoundSmoothedPAXTransfering_FSC = "FPD_OutBoundRolling_PAX_Transfering_FSC";
        internal static String fpdOutBoundInstantaneousPAXTransfering_FSC = "FPD_OutBoundInstantaneous_PAX_Transfering_FSC";

        internal static String fpdOutboundSmoothedBagTotal_FSC = "FPD_OutBoundRolling_Bag_Total_FSC";
        internal static String fpdOutBoundInstantaneousBagTotal_FSC = "FPD_OutBoundInstantaneous_Bag_Total_FSC";

        internal static String fpdOutBoundSmoothedBagOriginating_FSC = "FPD_OutBoundRolling_Bag_Originating_FSC";
        internal static String fpdOutBoundInstantaneousBagOriginating_FSC = "FPD_OutBoundInstantaneous_Bag_Originating_FSC";

        internal static String fpdOutBoundSmoothedBagTransfering_FSC = "FPD_OutBoundRolling_Bag_Transfering_FSC";
        internal static String fpdOutBoundInstantaneousBagTransfering_FSC = "FPD_OutBoundInstantaneous_Bag_Transfering_FSC";

        internal static string FPD_TRANSFER_ICT_ROLLING = "FPD_TransferICTRolling";
        internal static string FPD_TRANSFER_ICT_INSTANTANEOUS = "FPD_TransferICTInstantaneous";
        #endregion
        // >> Task #9504 Pax2Sim - flight subCategories - peak flow segregation

        // << Task #9115 Pax2Sim - Static Analysis - FPD+FPA Departure Entrance
        internal static String fpaFpdDepartureEntranceSmoothedTableName = "Departure Entrance Rolling";
        internal static String fpaFpdDepartureEntranceInstantaneousTableName = "Departure Entrance Instantaneous";

        internal static String fpaFpdDepartureEntranceBHSSmoothedTableName = "Departure Entrance Rolling_BHS";
        internal static String fpaFpdDepartureEntranceBHSInstantaneousTableName = "Departure Entrance Instantaneous_BHS";
        // >> Task #9115 Pax2Sim - Static Analysis - FPD+FPA Departure Entrance

        // >> Task #9108 Pax2Sim - Static Analysis - FPD+FPA split table        
        #region Static Analysis
        internal static String paxBoardingRoomSmoothedFlowsTableName = "PAX Boarding room Rolling flows";
        internal static String makeUpSmoothedFlowsBHSTableName = "Make-Up Rolling flows_BHS";

        internal static String paxBoardingRoomInstantaneousFlowsTableName = "PAX Boarding room Instantaneous flows";
        internal static String makeUpInstantaneousFlowsBHSTableName = "Make-Up Instantaneous flows_BHS";

        internal static List<String> staticAnalysisPaxTableNamesList = 
            new List<String>(new String[] { paxBoardingRoomSmoothedFlowsTableName, paxBoardingRoomInstantaneousFlowsTableName,
            fpaFpdDepartureEntranceSmoothedTableName, fpaFpdDepartureEntranceInstantaneousTableName});
        #endregion
        // << Task #9108 Pax2Sim - Static Analysis - FPD+FPA split table

        // << Task #9238 Pax2Sim - Validations - Validate flight plan Parking
        internal const String AIRCRAFT_PARKING_STANDS_OBJECT_NAME = "Aircraft Parking Stands";
        internal const String AIRCRAFT_PARKING_STAND_OBJECT_NAME = "Aircraft Parking Stand";
        // >> Task #9238 Pax2Sim - Validations - Validate flight plan Parking

        // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
        internal const String FPD_TABLE_2_OPENING_CI_FIRST_COLUMN_NAME = "OpeningCIFirst";
        internal const String FPD_TABLE_2_CLOSING_CI_FIRST_COLUMN_NAME = "ClosingCIFirst";

        internal const String FPD_TABLE_2_OPENING_BAGDROP_ECO_COLUMN_NAME = "OpeningBD";
        internal const String FPD_TABLE_2_CLOSING_BAGDROP_ECO_COLUMN_NAME = "ClosingBD";

        internal const String FPD_TABLE_2_OPENING_BAGDROP_FIRST_COLUMN_NAME = "OpeningBDFirst";
        internal const String FPD_TABLE_2_CLOSING_BAGDROP_FIRST_COLUMN_NAME = "ClosingBDFirst";
        // >> Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop

        // << Task #9582 Pax2Sim - BagTrace - ICS_Toploader and ICS_Unloader
        #region BHS analysis - DepartingIST table - column names
        internal const String DEPARTING_READER_TIMESTAMP_COLUMN_NAME = "Departing Reader Timestamp";
        internal const String ICS_TOPLOADER_COLUMN_NAME = "ICS Toploader";
        internal const String ICS_UNLOADER_COLUMN_NAME = "ICS Unloader";
        internal const String LAST_CHUTE_COLUMN_NAME = "Last Chute";
        internal const String ARRIVING_TIME_AT_CI_COLUMN_NAME = "Time At Check-In"; // >> #13391 IST Tables standardization - IST - CI times columns
        internal const String ARRIVING_TIME_AT_DEP_READER_TIMESTAMP_COLUMN_NAME = "Time At Dep-Reader-TimeStamp";
        internal const String ARRIVING_TIME_AT_ICS_TOPLOADER_COLUMN_NAME = "Time At ICS Toploader";
        internal const String ARRIVING_TIME_AT_ICS_UNLOADER_COLUMN_NAME = "Time At ICS Unloader";
        internal const String ARRIVING_TIME_AT_MAKE_UP_COLUMN_NAME = "Time At Make-Up";
        // >> Task #7949 Capacity Analysis - IST tables modification C#4
        internal const string FIRST_EBS_ENTRY_TIME_COLUMN_NAME = "First EBS Entry Time";
        internal const string LAST_EBS_EXIT_TIME_COLUMN_NAME = "Last EBS Exit Time";
        internal const string EBS_TOTAL_DURATION = "EBS Total Duration";
        // >> Task #7949 Capacity Analysis - IST tables modification C#4
        
        internal const String TOTAL_TIME_FROM_CIQUEUE_TO_DEPREADERTIMESTAMP_COLUMN_NAME = "Total Time From Entrance Queue To Dep-Reader-TimeStamp";//"Total Time From CI-Queue To Dep-Reader-TimeStamp";
        internal const String TOTAL_TIME_FROM_CI_TO_DEPREADERTIMESTAMP_COLUMN_NAME = "Total Time From Check-In To Dep-Reader-TimeStamp";
        internal const String TOTAL_TIME_FROM_CICOLL_TO_DEPREADERTIMESTAMP_COLUMN_NAME = "Total Time From Entrance Collector To Dep-Reader-TimeStamp";//"Total Time From CI-Collector To Dep-Reader-TimeStamp";
        internal const String TOTAL_TIME_FROM_READER_TO_LASTCHUTE_COLUMN_NAME = "Total Time From Dep-Reader-TimeStamp To Last Chute";
        internal const String TOTAL_TIME_FROM_READER_TO_EXIT_COLUMN_NAME = "Total Time From Dep-Reader-TimeStamp To Exit";
        internal const String TOTAL_TIME_FROM_LASTCHUTE_TO_MAKEUP_COLUMN_NAME = "Total Time From Last Chute To Make-Up";
        internal const String TOTAL_TIME_FROM_LASTCHUTE_TO_EXIT_COLUMN_NAME = "Total Time From Last Chute To Exit";

        internal const String TOTAL_TIME_FROM_CI_TO_PRESORTATION_COLUMN_NAME = "Total Time From Check-In To First Pre-Sortation";
        internal const String TOTAL_TIME_FROM_CI_TO_LASTCHUTE_COLUMN_NAME = "Total Time From Check-In To Last Chute";
        internal const String TOTAL_TIME_FROM_CI_TO_EXIT_COLUMN_NAME = "Total Time From Check-In To Exit";

        internal const String TOTAL_TIME_FROM_QUEUE_TO_EXIT_COLUMN_NAME = "Total Time From Entrance Queue To Exit";
        internal const String TOTAL_TIME_FROM_CI_COLL_TO_EXIT_COLUMN_NAME = "Total Time From CheckIn Collector To Exit";
        internal const String TOTAL_TIME_FROM_TRANSFER_COLL_TO_EXIT_COLUMN_NAME = "Total Time From Transfer Collector To Exit";
        #endregion
        // >> Task #9582 Pax2Sim - BagTrace - ICS_Toploader and ICS_Unloader

        // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        internal const String USE_SCENARIO_NAME_IN_TITLE_XML_NODE_NAME = "UseScenarioNameInTitle";
        // >> Task #9624 Pax2Sim - Charts - checkBox for scenario name

        // >> Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
        internal const String usaStandard_directoryName = "USA Standard";
        internal const String usaStandard_inputData_subdirectoryName = "Input Data";
        internal const String usaStandard_EDSRequiremenst_subdirectoryName = "EDS Requirements";
        internal const String usaStandard_EBSRequiremenst_subdirectoryName = "EBS Requirements";

        internal const String usaStandard_NumberOfETD_tableName = "Number of ETD";
        internal const String usaStandard_SortationRate_tableName = "Sortation Rate";
        internal const String usaStandard_EBSRequirements_tableName = "EBS Requirements";

        internal const String numberOfETD_SurgeFactor_columnName = "Surge Factor";
        internal const String numberOfETD_SurgeFactorReal_columnName = "Surge Factor Real";
        internal const String numberOfETD_BagsPerMin_columnName = "Bags per min";
        internal const String numberOfETD_SFRealPerMin_columnName = "BagwSF Real per min";
        internal const String numberOfETD_OSRate_columnName = "TSA OS Rate";
        internal const String numberOfETD_OOGRate_columnName = "TSA OOG Rate";
        internal const String numberOfETD_OSOOGRate_columnName = "TSA OS plus TSA OOG Rate";
        internal const String numberOfETD_ScreenRate_columnName = "Screen Rate";
        internal const String numberOfETD_EDSRequirements_columnName = "EDS Requirements";
        internal const String numberOfETD_NumberOfEDS_columnName = "Number of EDS";
        internal const String numberOfETD_NumberOfOSR_columnName = "Number of OSR";
        internal const String numberOfETD_Level1Alarmed_columnName = "Level 1 Alarmed";
        internal const String numberOfETD_Level2Alarmed_columnName = "Level 2 Alarmed";
        internal const String numberOfETD_DomesticContribution_columnName = "Domestic contribution";
        internal const String numberOfETD_ETD_columnName = "ETD";
        internal const String numberOfETD_ETDScreenLIT_columnName = "ETD ScreenLIT";
        internal const String numberOfETD_ETDOOG_columnName = "ETD OOG";
        internal const String numberOfETD_ETDforDOM_columnName = "ETD for DOM";
        internal const String numberOfETD_ETDSegPercDOMINT_columnName = "ETD Segregation Percent DOM_INT";
        internal const String numberOfETD_RequirementsForOverSize_columnName = "Number of ETD for OS";
        internal const String sortationRateTable_SortationRate_columnName = "Sortation Rate";

        //EDS requirements calculation constants (will be changed to parameters in the future)
        internal const Double OOG_PERCENT = 0.04;
        internal const Double OS_PERCENT = 0.02;
        internal const Double EDS_SCREENING_RATE_PER_MINUTE = 8.42;
        internal const Double EDS_SCREENING_RATE_PER_HOUR = 505;
        internal const Double FALSE_ALARM_EDS_DOM = 0.2;
        internal const Double FALSE_ALARM_EDS_INT = 0.2;
        internal const Double OSR_PROCESSING_RATE_BPH = 180;
        internal const Double CLEAR_RATE = 0.5;
        internal const Double ETD_SCREENING_RATE_DOM_WITH_IMAGE = 18.85;
        internal const Double ETD_SCREENING_RATE_DOM_NO_IMAGE = 26.67;
        internal const Double ETD_SCREENING_RATE_INT_WITH_IMAGE = 13.9;
        internal const Double ETD_SCREENING_RATE_INT_NO_IMAGE = 19.78;
        internal const Double RLIT_1 = 0.01;//rate of lost in tracking for oversize? = rate of lost in tracking (any bag type)
        internal const Double RLIT_2 = 0.04;//rate of lost in tracking for OOG? RLIT_2 is actually OOG Percent
        internal const Double ETD_SCREENING_RATE_OS_BAGS_BPH = 15.19;        
                
        //statistic tables
        internal const String EDS_STATS_TABLE_NAME = "EDS Statistics";
        internal const String EBS_STATS_TABLE_NAME = "EBS Statistics";
        // << Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory

        // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
        internal const String USA_STANDARD_PARAMETERS_TABLE_NAME = "USA Standard Parameters";
        internal const String USA_STANDARD_PARAMS_XML_ATTRIBUTE_NAME = "usaStandardParametersXML";

        internal const String USA_STANDARD_OOG_PERCENT_PARAM_NAME = "OOG Percent";
        internal const String USA_STANDARD_OS_PERCENT_PARAM_NAME = "OS Percent";
        internal const String USA_STANDARD_EDS_SCREENING_RATE_PER_MINUTE_PARAM_NAME = "EDS Screening rate per minute";
        internal const String USA_STANDARD_EDS_SCREENING_RATE_PER_HOUR_PARAM_NAME = "EDS Screening rate per hour";
        internal const String USA_STANDARD_FALSE_ALARM_EDS_DOM_PARAM_NAME = "False Alarm EDS DOM";
        internal const String USA_STANDARD_FALSE_ALARM_EDS_INT_PARAM_NAME = "False Alarm EDS INT";
        internal const String USA_STANDARD_OSR_PROCESSING_RATE_BPH_PARAM_NAME = "OSR Processing rate per hour";
        internal const String USA_STANDARD_CLEAR_RATE_PARAM_NAME = "Clear rate";
        internal const String USA_STANDARD_ETD_SCREENING_RATE_DOM_WITH_IMAGE_PARAM_NAME = "ETD Screening rate DOM with image";
        internal const String USA_STANDARD_ETD_SCREENING_RATE_DOM_NO_IMAGE_PARAM_NAME = "ETD Screening rate DOM no image";
        internal const String USA_STANDARD_ETD_SCREENING_RATE_INT_WITH_IMAGE_PARAM_NAME = "ETD Screening rate INT with image";
        internal const String USA_STANDARD_ETD_SCREENING_RATE_INT_NO_IMAGE_PARAM_NAME = "ETD Screening rate INT no image";
        internal const String USA_STANDARD_RATE_LOST_TRACK_OS_PARAM_NAME = "Rate of lost in tracking"; //"Rate of lost in tracking for oversize";
        internal const String USA_STANDARD_RATE_LOST_TRACK_OOG_PARAM_NAME = "Rate of lost in tracking for out-of-gauge";
        internal const String USA_STANDARD_ETD_SCREENING_RATE_OS_BAGS_BPH_PARAM_NAME = "ETD Screening rate per hour for OS";
        // << Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table

        // >> Task #10035 Pax2Sim - BNP development - Data Input tables
        internal const String USA_STANDARD_PARAMETERS_TABLE_GENERIC_NAME_FOR_SCENARIOS = "Standard Parameters";
        internal const String FLIGHT_PLAN_TABLE_GENERIC_NAME_FOR_SCENARIO = "Flight Plan Info";
        internal const String LOAD_FACTOR_COLUMN_NAME = "Load Factor";
        // << Task #10035 Pax2Sim - BNP development - Data Input tables

        // >> Task #10346 Pax2Sim - EBS review
        internal const String USA_PARAMETERS_GENERIC_TABLE_DATA_COLUMN_NAME = "Parameter Name";
        internal const String USA_PARAMETERS_GENERIC_TABLE_VALUE_COLUMN_NAME = "Parameter Value";

        internal const String DEPARTURE_LOAD_FACTORS_GENERIC_NAME_FOR_SCENARIOS = "Load Factors";
        //internal const String AIRCRAFT_TYPE_GENERIC_NAME_FOR_SCENARIOS = "Aircraft Types";
        internal const String CI_SHOWUP_PROFILE_GENERIC_NAME_FOR_SCENARIOS = "CheckIn ShowUp Profile";
        internal const String OCT_CI_GENERIC_NAME_FOR_SCENARIOS = "OCT for CheckIn";
        internal const String NB_BAGS_GENERIC_NAME_FOR_SCENARIOS = "Number of Bags";
        //internal const String TRANSFER_ICT_GENERIC_NAME_FOR_SCENARIOS = "Transfer ICT";
        //internal const String FLIGHT_CATEGORY_GENERIC_NAME_FOR_SCENARIOS = "Flight categories";
        internal const String OCT_MUP_GENERIC_NAME_FOR_SCENARIOS = "OCT for MakeUp";
        internal const String EBS_INPUT_RATE_GENERIC_NAME_FOR_SCENARIOS = "EBS - Input Rates";
        internal const String EBS_OUTPUT_RATE_GENERIC_NAME_FOR_SCENARIOS = "EBS - Output Rates";

        internal const String usaStandard_NumberOfETD_short_tableName = "Number of ETD short";
        // << Task #10346 Pax2Sim - EBS review

        // >> Task #10010 Pax2Sim - Filters - Ceiling function
        internal const String CEILING_FUNCTION_IDENTIFIER = "roundceiling";
        // << Task #10010 Pax2Sim - Filters - Ceiling function
        
        // >> Task #15717 PAX2SIM - Create Filter Assistant and Datagrid - new Features
        public const string DATETIME_TO_DHMS = "DateTimeToDHMS";
        public const string DATETIME_TO_DHMS_TO_LOWER = "datetimetodhms";

        public const string ROUND = "Round";
        public const string ROUND_TO_LOWER = "round";
        // << Task #15717 PAX2SIM - Create Filter Assistant and Datagrid - new Features

        // << Task #9486 Pax2Sim - Exception Editor details
        internal const String EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER = "( ";
        internal const String EXCEPTION_EDITOR_DETAILS_FINAL_DELIMITER = " )";
        // >> Task #9486 Pax2Sim - Exception Editor details

        // >> Task #10079 Pax2Sim - Exception Editor Search functionality
        internal const char EXCEPTION_EDITOR_SEARCH_FILTER_DELIMITER = ',';
        // << Task #10079 Pax2Sim - Exception Editor Search functionality

        // >> Task #10156 Pax2Sim - Statistic dev - Target
        internal const String AIRPORT_REPORTS_NODE_NAME = "Airport Reports";
        internal const String PAX_CAPACITY_ANALYSIS_NODE_NAME = "Pax Capacity Analysis";
        internal const String TARGET_DIRECTORY_NAME = "Target";
        internal const String TARGET_TABLE_NAME_SUFIX = "Target";

        internal const String TARGET_ACHIEVED_POSITIVE = "Y";
        internal const String TARGET_ACHIEVED_NEGATIVE = "N";

        internal const String target_scenarioName_columnName = "Statistic Name";
        internal const String target_processObserved_columnName = "Process Observed";
        internal const String target_statisticType_columnName = "Statistic Type";
        internal const String target_statisticAttribute_columnName = "Statistic Attribute";
        internal const String target_attributeDegree_columnName = "Attribute Degree";
        internal const String target_comparisonType_columnName = "Comparison Type";
        internal const String target_targetValue_columnName = "Target Value";
        internal const String target_valueObserved_columnName = "Value Observed";
        internal const String target_targetAchived_columnName = "Target Achieved";
        internal const String target_difference_columnName = "Difference";
        internal const String target_percentSuccess_columnName = "% Success";

        internal const String STATISTIC_TYPE_OVERVIEW = "Overview"; // >> Task #11217 Pax2Sim - Target - add missing Attribute degree stage2
        internal const String STATISTIC_TYPE_OCCUPATION = "Occupation";
        internal const String STATISTIC_TYPE_REMAINING_TIME = "Remaining";
        internal const String STATISTIC_TYPE_TIME = "Time";
        internal const String STATISTIC_TYPE_UTILIZATION = "Utilization";
        internal const String STATISTIC_TYPE_DWELL_AREA = "Dwell Area";

        internal const String REMAINING_TIME_STAT_TABLE_NB_PAX_COLUMN_NAME = "Nb Pax";
        internal const String TIME_STAT_TABLE_MINUTES_COLUMN_NAME = "Minutes";

        internal const String IST_TOTAL_TIME_COLUMN_NAME = "TotalTime (min)";
        internal const String IST_PROCESS_TIME_COLUMN_NAME = "Process (min)";
        internal const String IST_WAITING_GROUP_TIME_COLUMN_NAME = "WaitingGroup (min)";
        internal const String IST_WAITING_DESK_TIME_COLUMN_NAME = "WaitingDesk (min)";
        internal const String IST_REMAINING_TIME_COLUMN_NAME = "Remaining Time (min)";
        
        internal const String TIME_STATS_TOTAL_TIME = "TotalTime";
        internal const String TIME_STATS_DELAY_TIME = "DelayTime";
        internal const String TIME_STATS_PROCESS_TIME = "Process";
        internal const String TIME_STATS_WAITING_GROUP_TIME = "WaitingGroup";
        internal const String TIME_STATS_WAITING_DESK_TIME = "WaitingDesk";
        // << Task #10156 Pax2Sim - Statistic dev - Target

        // >> Task #10175 Pax2Sim - Flight Plan table editor
        internal const String FP_ITEM_TYPE_AIRLINE = "Airline";
        internal const String FP_ITEM_TYPE_AIRCRAFT = "Aircraft";
        internal const String FP_ITEM_TYPE_AIRPORT = "Airport";
        // << Task #10175 Pax2Sim - Flight Plan table editor

        // >> Task #10254 Pax2Sim - View statistics option
        internal const String VIEW_STATS_DIRECTORY_NAME = "View Statistics";
        internal const String VIEW_STATS_TABLE_SUFFIX = " View Stats";
        // << Task #10254 Pax2Sim - View statistics option

        // >> Task #10282 Process Flow - Set target option
        internal const String AIRPORT_REPORTS_PROCESS_NAME = "Airport";
        internal const String ANALYSIS_NODE_NAME = "Analysis";
        internal const String IST_TABLE_SUFFIX = "_IST";
        internal const String ATTRIBUTE_DEGREE_MAX = "Maximum";
        internal const String ATTRIBUTE_DEGREE_MIN = "Minimum";
        internal const String ATTRIBUTE_DEGREE_AVG = "Average"; // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
        internal const String ATTRIBUTE_DEGREE_TOTAL = "Total"; // >> Task #11217 Pax2Sim - Target - add missing Attribute degree stage2
        // << Task #10282 Process Flow - Set target option

        // >> Task #10359 Pax2Sim - Counting functionality
        public const String COMPARISON_LESS_THAN = "<";
        public const String COMPARISON_LESS_OR_EQUAL_THAN = "<=";
        public const String COMPARISON_EQUAL = "=";
        public const String COMPARISON_GREATER_OR_EQUAL_THAN = ">=";
        public const String COMPARISON_GREATER_THAN = ">";
        public const String COMPARISON_CONTAINS = "Contains";

        public const String BOOLEAN_TRUE_VALUE = "True";
        public const String BOOLEAN_FALSE_VALUE = "False";

        public const String COUNT_RESULT_TABLE_SUFIX = " Count Result";
        public const String COUNT_RESULT_ID_COLUMN_NAME = "Id";
        public const String COUNT_RESULT_SCENARIO_NAME_COLUMN_NAME = "Scenario Name";
        public const String COUNT_RESULT_PROCESS_OBSERVED_COLUMN_NAME = "Process Observed";
        public const String COUNT_RESULT_ATTRIBUTE_COLUMN_NAME = "Counted Attribute";
        public const String COUNT_RESULT_COMPARISON_TYPE_COLUMN_NAME = "Comparison Type";
        public const String COUNT_RESULT_REFERENCE_VALUE_COLUMN_NAME = "Reference Value";
        public const String COUNT_RESULT_TOTAL_ITEMS_COLUMN_NAME = "Nb of Items";

        public const String COUNTED_ITEMS_TABLE_SUFIX = " Counted Items";

        public const String COUNT_DIRECTORY_NAME = "Count";
        // << Task #10359 Pax2Sim - Counting functionality

        // >> Task #13206 Pax2Sim - Dynamic Analysis - Arrival Flight Statistics
        public const string FPD_PAX_TABLE_NAME = "FPD_Pax";
        public const string FPA_PAX_TABLE_NAME = "FPA_Pax";
        // << Task #13206 Pax2Sim - Dynamic Analysis - Arrival Flight Statistics

        // >> Task #10440 Pax2Sim - Pax analysis - missing KPIs part 1
        public const String SUMMARY_TABLE_DISTRIBUTION_LEVEL_COLUMN_SUFIX = " % Percentile";

        public const String SUMMARY_PASSENGER_UNIT_NAME = "Passenger(s)";
        public const String SUMMARY_TIME_MINUTES_UNIT_NAME = "Minute(s)";
        public const String SUMMARY_DESK_UNIT_NAME = "Desk(s)";
        public const String SUMMARY_PERCENT_UNIT_NAME = "%";
        public const String SUMMARY_PAX_PER_HOUR_UNIT_NAME = "Passenger(s)/hour";

        public const String GROUP_UTILIZATION_TOTAL_UTILIZATION_PERCENT_COLUMN_NAME = "Total";
        public const String GROUP_UTILIZATION_AVERAGE_COLUMN_NAME = "Average";
        public const String GROUP_UTILIZATION_DESK_NEED_COLUMN_NAME = "Desks Need";        
        public const String DESK_UTILIZATION_UTILIZATION_PERCENT_COLUMN_NAME = "% Utilization";

        public const String OCCUPATION_TABLE_PAX_IN_COLUMN_NAME = "Nb Pax (Input)";
        public const String OCCUPATION_TABLE_PAX_OUT_COLUMN_NAME = "Nb Pax (Output)";

        public const String OCCUPATION_TABLE_THROUGHPUT_IN_COLUMN_NAME = "Throughput (Input)";
        public const String OCCUPATION_TABLE_THROUGHPUT_OUT_COLUMN_NAME = "Throughput (Output)";

        // >> Task #13762 Pax2Sim - Dynamic Analysis - Occupation table        
        public const String OCCUPATION_TABLE_THROUGHPUT_INSTANT_IN_COLUMN_NAME = "Throughput Instant (Input)";
        public const String OCCUPATION_TABLE_THROUGHPUT_INSTANT_OUT_COLUMN_NAME = "Throughput Instant (Output)";
        // << Task #13762 Pax2Sim - Dynamic Analysis - Occupation table

        public const String MIN_THROUGHPUT_IN_SUMMARY_NAME = "MinThroughputInput";
        public const String AVG_THROUGHPUT_IN_SUMMARY_NAME = "AvgThroughputInput";
        public const String MAX_THROUGHPUT_IN_SUMMARY_NAME = "MaxThroughputInput";

        public const String MIN_THROUGHPUT_OUT_SUMMARY_NAME = "MinThroughputOutput";
        public const String AVG_THROUGHPUT_OUT_SUMMARY_NAME = "AvgThroughputOutput";
        public const String MAX_THROUGHPUT_OUT_SUMMARY_NAME = "MaxThroughputOutput";

        public const String MIN_REMAINING_TIME_SUMMARY_NAME = "MinRemainingTime";
        public const String AVG_REMAINING_TIME_SUMMARY_NAME = "AvgRemainingTime";
        public const String MAX_REMAINING_TIME_SUMMARY_NAME = "MaxRemainingTime";
        
        public const String MIN_UTILIZATION_PERCENT_SUMMARY_NAME = "MinUtilizationPercent";
        public const String AVG_UTILIZATION_PERCENT_SUMMARY_NAME = "AvgUtilizationPercent";
        public const String MAX_UTILIZATION_PERCENT_SUMMARY_NAME = "MaxUtilizationPercent";

        public const String MIN_UTILIZATION_DESK_NEED_SUMMARY_NAME = "MinDeskNeed";
        public const String AVG_UTILIZATION_DESK_NEED_SUMMARY_NAME = "AvgDeskNeed";
        public const String MAX_UTILIZATION_DESK_NEED_SUMMARY_NAME = "MaxDeskNeed";
        // << Task #10440 Pax2Sim - Pax analysis - missing KPIs part 1

        // >> Task #10484 Pax2Sim - Pax analysis - Summary with distribution levels percent
        public const String BAGGAGE_CLAIM_GROUP_TYPE_IDENTIFIER = "Baggage Claim Group";
        public const String CHECK_IN_GROUP_TYPE_IDENTIFIER = "Check In Group";
        public const String BOARDING_GATE_GROUP_TYPE_IDENTIFIER = "Boarding Gate Group";

        public const String PASSPORT_GROUP_TYPE_IDENTIFIER = "Passport Check Group";
        public const String SECURITY_GROUP_TYPE_IDENTIFIER = "Security Check Group";
        public const String USER_PROCESS_GROUP_TYPE_IDENTIFIER = "User Process Group";
        public const String TRANSFER_GROUP_TYPE_IDENTIFIER = "Transfer Group";

        public const String DWELL_AREA_MIN_COLUMN_NAME = "Min Dwell Area (mn)";
        public const String DWELL_AREA_AVG_COLUMN_NAME = "Average Dwell Area (mn)";
        public const String DWELL_AREA_MAX_COLUMN_NAME = "Max Dwell Area (mn)";
        public const String DWELL_AREA_NB_PAX_COLUMN_NAME = "Nb Pax";

        public const String OCCUPATION_TABLE_MIN_OCCUPATION_COLUMN_NAME = "Min Occupation";
        public const String OCCUPATION_TABLE_AVG_OCCUPATION_COLUMN_NAME = "Average Occupation";
        public const String OCCUPATION_TABLE_MAX_OCCUPATION_COLUMN_NAME = "Max Occupation";

        public const String AIRPORT_RESOURCE_TYPE_CODE = "Airport";
        public const String TERMINAL_RESOURCE_TYPE_CODE = "Terminal";
        public const String LEVEL_RESOURCE_TYPE_CODE = "Level";
        public const String GROUP_RESOURCE_TYPE_CODE = "Group";
        public const String DESK_RESOURCE_TYPE_CODE = "Desk";

        // >> Task #14902 Improve Dynamic Occupation tables (PAX & BHS)
        public const String BHS_STATS_TRANSP_TABLE_SUFFIX = "_Dashboard_V2";
        public const String SUMMARY_TABLE_TRANSP_KPI_CATEGORY_COLUMN_NAME = "KPI Category";
        public const String SUMMARY_TABLE_TRANSP_KPI_STAT_COLUMN_NAME = "KPI Stat";
        public const String SUMMARY_TABLE_TRANSP_VALUE_UNIT_COLUMN_NAME = "Value Unit";
        public const String SUMMARY_TABLE_TRANSP_STATION_VALUE_UNIT_COLUMN_NAME = "Station Value Unit";
        public const String SUMMARY_TABLE_TRANSP_VALUE_COLUMN_NAME = "Value";

        public const String SUMMARY_TABLE_TRANSP_TOTAL_VALUE_KPI_STAT_NAME = "Total Value";
        public const String SUMMARY_TABLE_TRANSP_MIN_VALUE_KPI_STAT_NAME = "Min Value";
        public const String SUMMARY_TABLE_TRANSP_AVG_VALUE_KPI_STAT_NAME = "Avg Value";
        public const String SUMMARY_TABLE_TRANSP_MAX_VALUE_KPI_STAT_NAME = "Max Value";
        public const String SUMMARY_TABLE_TRANSP_DISTRIBUTION_LEVEL_KPI_STAT_SUFIX = " % Percentile";

        public static List<String> SUMMARY_TABLE_TRANSP_KPI_STAT_NAMES
            = new List<String>(new String[] { SUMMARY_TABLE_TRANSP_TOTAL_VALUE_KPI_STAT_NAME, SUMMARY_TABLE_TRANSP_MIN_VALUE_KPI_STAT_NAME, 
                                                SUMMARY_TABLE_TRANSP_AVG_VALUE_KPI_STAT_NAME, SUMMARY_TABLE_TRANSP_MAX_VALUE_KPI_STAT_NAME });
        // << Task #14902 Improve Dynamic Occupation tables (PAX & BHS)

        #region new summary table

        #region column names
        public const String SUMMARY_TABLE_KPI_ID_COLUMN_NAME = "KPI Id";
        public const String SUMMARY_TABLE_DATA_COLUMN_NAME = "Data";
        public const String SUMMARY_TABLE_VALUE_COLUMN_NAME = "Total Value";
        public const String SUMMARY_TABLE_MIN_VALUE_COLUMN_NAME = "Min Value";
        public const String SUMMARY_TABLE_AVG_VALUE_COLUMN_NAME = "Avg Value";
        public const String SUMMARY_TABLE_MAX_VALUE_COLUMN_NAME = "Max Value";
        public const String SUMMARY_TABLE_MIN_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME = "Min Value for ";
        public const String SUMMARY_TABLE_AVG_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME = "Avg Value for ";
        public const String SUMMARY_TABLE_MAX_VALUE_FOR_LEVEL_COLUMN_PARTIAL_NAME = "Max Value for ";
        public const String SUMMARY_TABLE_UNIT_COLUMN_NAME = "Unit";
        #endregion

        #region KPI codes
        public const String SUMMARY_KPI_NAME_TOTAL_PAX_NB = "Number of Passengers";
        public const String SUMMARY_KPI_NAME_TOTAL_PROCESSED_PAX_NB = "Number of Processed Passengers";
        public const String SUMMARY_KPI_NAME_TRANSFER_PAX_NB = "Number of Transfer Passengers";
        public const String SUMMARY_KPI_NAME_MISSED_PAX_NB = "Number of Missed Passengers";
        public const String SUMMARY_KPI_NAME_STOPPED_PAX_NB = "Number of Stopped Passengers";
        public const String SUMMARY_KPI_NAME_LOST_PAX_NB = "Number of Lost Passengers";

        public const String SUMMARY_KPI_NAME_AREA_OCCUPATION = "Area Occupation";
        public const String SUMMARY_KPI_NAME_TOTAL_TIME = "Total Time";
        public const String SUMMARY_KPI_NAME_WAITING = "Waiting Time";
        public const String SUMMARY_KPI_NAME_DELAY_TIME = "Delay Time";
        public const String SUMMARY_KPI_NAME_WAITING_DESK = "Waiting Desk Time";
        public const String SUMMARY_KPI_NAME_WAITING_GROUP = "Waiting Group Time";
        public const String SUMMARY_KPI_NAME_PROCESS = "Process Time";
        public const String SUMMARY_KPI_NAME_DWELL_AREA = "Dwell Area Time";
        public const String SUMMARY_KPI_NAME_REMAINING_TIME = "Remaining Time";
        public const String SUMMARY_KPI_NAME_QUEUE_OCCUPATION = "Queue Occupation";
        public const String SUMMARY_KPI_NAME_THROUGHPUT_NB_PAX_IN = "Input Passengers";
        public const String SUMMARY_KPI_NAME_THROUGHPUT_NB_PAX_OUT = "Output Passengers";
        public const String SUMMARY_KPI_NAME_THROUGHPUT_INPUT = "Throughput Input";
        public const String SUMMARY_KPI_NAME_THROUGHPUT_INSTANT_INPUT = "Throughput Instant Input"; // >> Task #13762 Pax2Sim - Dynamic Analysis - Occupation table
        public const String SUMMARY_KPI_NAME_THROUGHPUT_OUTPUT = "Throughput Output";
        public const String SUMMARY_KPI_NAME_THROUGHPUT_INSTANT_OUTPUT = "Throughput Instant Output";   // >> Task #13762 Pax2Sim - Dynamic Analysis - Occupation table
        public const String SUMMARY_KPI_NAME_UTILIZATION_PERCENT = "Desk Utilization Percent";
        public const String SUMMARY_KPI_NAME_DESK_NEED = "Desk Need";

        public const String SUMMARY_KPI_NAME_TOTAL_OPENING_TIME = "Desk Total Opening Time";
        public const String SUMMARY_KPI_NAME_TOTAL_PROCESS_TIME = "Desk Total Process Time";
        public const String SUMMARY_KPI_NAME_RATE = "Rate";
        #endregion

        public const String SUMMARY_TABLE_NAME_PREFIX = " ";
        public const String SUMMARY_TABLE_NAME_SUFFIX = "_Summary";
        #endregion
        // << Task #10484 Pax2Sim - Pax analysis - Summary with distribution levels percent

        // >> Task #10553 Pax2Sim - Pax analysis - Summary - Dashboard
        public const String DASHBOARD_SWF_FILE_NAME = "ComponentSample.swf";
        // << Task #10553 Pax2Sim - Pax analysis - Summary - Dashboard

        // >> Task #10615 Pax2Sim - Pax analysis - Summary - small changes
        public const String SUMMARY_KPI_TYPE_TEXT = "Text KPI";

        public const String SUMMARY_KPI_TYPE_MIN_MAX_AVG_TOTAL_TEXT = "MinMaxAvgTotalText KPI";
                
        /// <summary>
        /// Chart KPIs will always have the min/avg/max values. Some will also have totalValue or percentile values
        /// Percentile values are max values for different percents of the total data.(the best x% cases)
        /// </summary>
        public const String SUMMARY_KPI_TYPE_CHART_WITH_TOTAL_VALUE_WITH_PERCENTILE = "Chart With Percentile KPI";
        public const String SUMMARY_KPI_TYPE_CHART_NO_TOTAL_VALUE_NO_PERCENTILE = "Chart Without Total Value Without Percentile KPI";
        public const String SUMMARY_KPI_TYPE_CHART_WITH_TOTAL_VALUE_NO_PERCENTILE = "Chart With Total Value Without Percentile KPI";
        // << Task #10615 Pax2Sim - Pax analysis - Summary - small changes

        // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
        public const String DASHBOARD_NOTE_XML_ELEMENT_NAME = "DashboardNote";
        public const String DASHBOARD_NODE_PREFIX = "DashboardImage";
        public const int DASHBOARD_NOTE_IMAGE_INDEX = 71; // nb 71 in the list of images, imageList, from PAS2SIM.cs[Designer]

        public const String SCENARIO_DASHBOARD_MAP_KEY_DELIMITER = "|+|";
        public const String SCENARIO_OUTPUT_DIRECTORY_NAME = "Output";
        public const String DASHBOARD_CONFIG_TEXT_FILE_NAME = "DashboardConfig.txt";
        public const String SEPARATOR_FOR_DASHBOARD_CONFIG_TEXT_FILE = ";";
        // << Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports

        // >> Task #10764 Pax2Sim - new User attributes for Groups
        public const String CHECK_IN_GROUP_TYPE = "Check In Group";
        public const String TRANSFER_GROUP_TYPE = "Transfer Group";
        public const String RECLAIM_GROUP_TYPE = "Baggage Claim Group";
        public const String BOARDING_GATE_GROUP_TYPE = "Boarding Gate Group";

        public const String PAX_CI_GROUP_USER_ATTRIBUTE = "PAX_CI_Group";
        public const String PAX_TRANSFER_GROUP_USER_ATTRIBUTE = "PAX_Transfer_Group";
        public const String PAX_RECLAIM_GROUP_USER_ATTRIBUTE = "PAX_Reclaim_Group";
        public const String PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE = "PAX_BoardingGate_Group";

        public static List<String> PAX_GROUP_USER_ATTRIBUTES_LIST
            = new List<String>(new String[] { PAX_CI_GROUP_USER_ATTRIBUTE, PAX_RECLAIM_GROUP_USER_ATTRIBUTE, 
                                                PAX_TRANSFER_GROUP_USER_ATTRIBUTE, PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE });
        // << Task #10764 Pax2Sim - new User attributes for Groups

        // >> Task #10720 Pax2Sim - Static analysis - redesign Peak flow statistics
        public const String PEAK_FLOWS_FPA_STATISTICS_TABLE_NAME = "FPA_Peak_Stats";
        public const String PEAK_FLOWS_FPD_STATISTICS_TABLE_NAME = "FPD_Peak_Stats";
        public const String PEAK_FLOWS_FPDFPA_STATISTICS_TABLE_NAME = "FPDFPA_PeakStats";
        
        public static List<String> PEAK_FLOWS_STATISTICS_TABLE_NAMES_LIST
            = new List<String>(new String[] { PEAK_FLOWS_FPA_STATISTICS_TABLE_NAME, fpaPeakStatsBHSTableName,
                                                PEAK_FLOWS_FPD_STATISTICS_TABLE_NAME, PEAK_FLOWS_FPDFPA_STATISTICS_TABLE_NAME});
        // << Task #10720 Pax2Sim - Static analysis - redesign Peak flow statistics

        // >> Task #10926 Pax2Sim - BHS analysis - Filter period problem
        public static String BAG_ID_IST_TABLE_COLUMN_NAME = "BAG ID";
        // << Task #10926 Pax2Sim - BHS analysis - Filter period problem
        public static string FLIGHT_ID_IST_TABLE_COLUMN_NAME = "Flight ID";
        public static string CHECK_IN_IST_TABLE_COLUMN_NAME = "CheckIn";
        public static string TRANSFER_INFEED_QUEUE_IST_TABLE_COLUMN_NAME = "Transfer Infeed Queue";
        public static string TRANSFER_INFEED_DESK_IST_TABLE_COLUMN_NAME = "Transfer Infeed Desk";
        public static string MAKE_UP_IST_TABLE_COLUMN_NAME = "MakeUp";
        public static string STOPPED_IST_TABLE_COLUMN_NAME = "Stopped";
        public static string PBC_TYPE_IST_TABLE_COLUMN_NAME = "PBC Type";

        // >> Task #11030 Pax2Sim - BHS analysis - HBS extended statistics
        public static String BHS_STATISTICS_TABLE_PERCENT_OF_GLOBAL_COLUMN_NAME = "% of Global";
        public static String BHS_STATISTICS_TABLE_TYPE_COLUMN_NAME = "Type";
        public static String BHS_STATISTICS_TABLE_NB_BAGS_COLUMN_NAME = "Nb Bags";
        public static String BHS_STATISTICS_GLOBAL_RESULTS_TAG_NAME = "_Global";
        // << Task #11030 Pax2Sim - BHS analysis - HBS extended statistics

        // >> Task #10985 Pax2Sim - BHS dynamic analysis - adapt statistics tables for the Dashboard
        public const String BHS_STATS_TABLE_SUFFIX = "_Dashboard";
        public const String BHS_STATS_TABLE_TOTAL_VALUE_UNIT_COLUMN_NAME = "Total Value Unit";

        #region BHS KPIs Codes
        public const String BHS_KPI_NAME_NB_BAGS = "Number of Bags";
        public const String BHS_KPI_NAME_NB_STOPPED_BAGS = "Number of Stopped Bags";
        public const String BHS_KPI_NAME_NB_STOPPED_BAGS_AFTER_STATION = "Number of Stopped Bags after Next Station";

        public const String BHS_KPI_NAME_PROCESS_TIME = "Process Time";
        public const String BHS_KPI_NAME_DWELL_AREA_TIME = "Dwell Area Time";
        public const String BHS_KPI_NAME_TRAVEL_TIME_TO_NEXT_STATION = "Travel Time to Next Station";

        public const String BHS_KPI_NAME_OCCUPATION = "Occupation";
        public const String BHS_KPI_NAME_INPUT_BAGS = "Input Bags";
        public const String BHS_KPI_NAME_OUTPUT_BAGS = "Output Bags";
        public const String BHS_KPI_NAME_THROUGHPUT_INPUT = "Throughput Input";
        public const String BHS_KPI_NAME_THROUGHPUT_OUTPUT = "Throughput Output";
        // >> Task #14902 Improve Dynamic Occupation tables (PAX & BHS) C#9 Instant Throughput KPIs
        public const String BHS_KPI_NAME_THROUGHPUT_INSTANT_INPUT = "Throughput Instant Input";
        public const String BHS_KPI_NAME_THROUGHPUT_INSTANT_OUTPUT = "Throughput Instant Output";
        // << Task #14902 Improve Dynamic Occupation tables (PAX & BHS) C#9 Instant Throughput KPIs

        public const String BHS_KPI_NAME_STATION_UTILIZATION_PERCENT = "Station Utilization Percent";
        public const String BHS_KPI_NAME_STATION_NEED = "Station Need";

        // >> Task #17222 PAX2SIM - FromTo Times statistics - global summaries
        public const String BHS_KPI_NAME_QUEUE_EXIT_OCCUPATION = "Occupation (Queue_Exit)";
        public const String BHS_KPI_NAME_QUEUE_EXIT_INPUT_BAGS = "Input Bags (Queue_Exit)";
        public const String BHS_KPI_NAME_QUEUE_EXIT_OUTPUT_BAGS = "Output Bags (Queue_Exit)";
        public const String BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INPUT = "Throughput Input (Queue_Exit)";
        public const String BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INSTANT_INPUT = "Throughput Instant Input (Queue_Exit)";
        public const String BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_OUTPUT = "Throughput Output (Queue_Exit)";
        public const String BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INSTANT_OUTPUT = "Throughput Instant Output (Queue_Exit)";
        
        public const String BHS_KPI_NAME_COLLECTOR_EXIT_OCCUPATION = "Occupation (Collector_Exit)";
        public const String BHS_KPI_NAME_COLLECTOR_EXIT_INPUT_BAGS = "Input Bags (Collector_Exit)";
        public const String BHS_KPI_NAME_COLLECTOR_EXIT_OUTPUT_BAGS = "Output Bags (Collector_Exit)";
        public const String BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INPUT = "Throughput Input (Collector_Exit)";
        public const String BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INSTANT_INPUT = "Throughput Instant Input (Collector_Exit)";
        public const String BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_OUTPUT = "Throughput Output (Collector_Exit)";
        public const String BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INSTANT_OUTPUT = "Throughput Instant Output (Collector_Exit)";

        public static Dictionary<String, String> QUEUE_EXIT_OCCUPATION_KPI_NAMES_MAP
            = new Dictionary<String, String> { 
            { BHS_KPI_NAME_OCCUPATION, BHS_KPI_NAME_QUEUE_EXIT_OCCUPATION },
            { BHS_KPI_NAME_INPUT_BAGS, BHS_KPI_NAME_QUEUE_EXIT_INPUT_BAGS },
            { BHS_KPI_NAME_OUTPUT_BAGS, BHS_KPI_NAME_QUEUE_EXIT_OUTPUT_BAGS },
            { BHS_KPI_NAME_THROUGHPUT_INPUT, BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INPUT },
            { BHS_KPI_NAME_THROUGHPUT_INSTANT_INPUT, BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INSTANT_INPUT },
            { BHS_KPI_NAME_THROUGHPUT_OUTPUT, BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_OUTPUT },
            { BHS_KPI_NAME_THROUGHPUT_INSTANT_OUTPUT, BHS_KPI_NAME_QUEUE_EXIT_THROUGHPUT_INSTANT_OUTPUT }
            };
        public static Dictionary<String, String> COLLECTOR_EXIT_OCCUPATION_KPI_NAMES_MAP
            = new Dictionary<String, String> { 
            { BHS_KPI_NAME_OCCUPATION, BHS_KPI_NAME_COLLECTOR_EXIT_OCCUPATION },
            { BHS_KPI_NAME_INPUT_BAGS, BHS_KPI_NAME_COLLECTOR_EXIT_INPUT_BAGS },
            { BHS_KPI_NAME_OUTPUT_BAGS, BHS_KPI_NAME_COLLECTOR_EXIT_OUTPUT_BAGS },
            { BHS_KPI_NAME_THROUGHPUT_INPUT, BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INPUT },
            { BHS_KPI_NAME_THROUGHPUT_INSTANT_INPUT, BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INSTANT_INPUT },
            { BHS_KPI_NAME_THROUGHPUT_OUTPUT, BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_OUTPUT },
            { BHS_KPI_NAME_THROUGHPUT_INSTANT_OUTPUT, BHS_KPI_NAME_COLLECTOR_EXIT_THROUGHPUT_INSTANT_OUTPUT }
            };

        public const string BHS_SUMMARY_TABLE_NAME_SUFFIX = "_Overall_Summary";

        public static List<String> BHS_SUMMARY_VISIBLE_BY_DEFAULT_KPIS
            = new List<String>(new String[] { BHS_KPI_NAME_QUEUE_EXIT_OCCUPATION, BHS_KPI_NAME_COLLECTOR_EXIT_OCCUPATION, BHS_KPI_NAME_QUEUE_EXIT_INPUT_BAGS, 
                                                BHS_KPI_NAME_QUEUE_EXIT_OUTPUT_BAGS, BHS_KPI_NAME_COLLECTOR_EXIT_INPUT_BAGS, BHS_KPI_NAME_COLLECTOR_EXIT_OUTPUT_BAGS });
        // << Task #17222 PAX2SIM - FromTo Times statistics - global summaries

        public static List<String> BHS_KPIS_NAMES_LIST 
            = new List<String>(new String[] {BHS_KPI_NAME_NB_BAGS, BHS_KPI_NAME_NB_STOPPED_BAGS, BHS_KPI_NAME_NB_STOPPED_BAGS_AFTER_STATION,
                                             BHS_KPI_NAME_PROCESS_TIME, BHS_KPI_NAME_DWELL_AREA_TIME, BHS_KPI_NAME_TRAVEL_TIME_TO_NEXT_STATION,
                                             BHS_KPI_NAME_OCCUPATION, BHS_KPI_NAME_INPUT_BAGS, BHS_KPI_NAME_OUTPUT_BAGS, 
                                             BHS_KPI_NAME_THROUGHPUT_INPUT, BHS_KPI_NAME_THROUGHPUT_INSTANT_INPUT,  // >> Task #14902 Improve Dynamic Occupation tables (PAX & BHS) C#9 Instant Throughput KPIs
                                             BHS_KPI_NAME_THROUGHPUT_OUTPUT, BHS_KPI_NAME_THROUGHPUT_INSTANT_OUTPUT,
                                             BHS_KPI_NAME_STATION_UTILIZATION_PERCENT, BHS_KPI_NAME_STATION_NEED});
        #endregion

        #region BHS Statistics Unit Codes
        public const String BHS_STATS_UNIT_NAME_BAG = "Bag(s)";
        public const String BHS_STATS_UNIT_NAME_CARS = "Car(s)";
        public const String BHS_STATS_UNIT_NAME_HOURS = "Hour(s)";
        public const String BHS_STATS_UNIT_NAME_MINUTES = "Minute(s)";
        public const String BHS_STATS_UNIT_NAME_SECONDS = "Second(s)";
        public const String BHS_STATS_UNIT_NAME_BAGS_PER_HOUR = "Bag(s) / hour";
        public const String BHS_STATS_UNIT_NAME_PERCENTAGE = "%";
        public const String BHS_STATS_UNIT_NAME_STATION = "Station(s)";
        #endregion
        
        public const String BHS_OCCUPATION_TABLE_MIN_OCC_COLUMN_NAME = "Min Occupation";
        public const String BHS_OCCUPATION_TABLE_AVG_OCC_COLUMN_NAME = "Average Occupation";
        public const String BHS_OCCUPATION_TABLE_MAX_OCC_COLUMN_NAME = "Max Occupation";

        public const String BHS_OCCUPATION_TABLE_NB_BAGS_IN_COLUMN_NAME = "Nb Bags (Input)";
        public const String BHS_OCCUPATION_TABLE_NB_BAGS_OUT_COLUMN_NAME = "Nb Bags (Output)";

        public const String BHS_OCCUPATION_TABLE_THROUGHPUT_IN_COLUMN_NAME = "Throughput (Input)";
        public const String BHS_OCCUPATION_TABLE_THROUGHPUT_OUT_COLUMN_NAME = "Throughput (Output)";

        public const String BHS_DWELL_AREA_MIN_COLUMN_NAME = "Min Dwell Area (mn)";
        public const String BHS_DWELL_AREA_AVG_COLUMN_NAME = "Average Dwell Area (mn)";
        public const String BHS_DWELL_AREA_MAX_COLUMN_NAME = "Max Dwell Area (mn)";
        public const String BHS_DWELL_AREA_NB_PAX_COLUMN_NAME = "Nb Bags";

        public const String BHS_UTILIZATION_PERCENT_COLUMN_NAME = "% Occupation";
        public const String BHS_UTILIZATION_AVERAGE_COLUMN_NAME = "Average";
        public const String BHS_UTILIZATION_STATION_NEED_COLUMN_NAME = "Stations Need";
        // << Task #10985 Pax2Sim - BHS dynamic analysis - adapt statistics tables for the Dashboard

        // >> Task #11212 Pax2Sim - Target - add missing Attribute degree        
        /// <summary>
        /// K: the name of the statistic attribute from the Target editor combobox
        /// V: the name of the summary table kpi (the name that appears in the summary table).
        /// </summary>
        public static Dictionary<String, String> targetStatisticAttributeSummaryTableStatisticNameMap       // >> Task #11212 Pax2Sim - Target - add missing Attribute degree
            = new Dictionary<String, String> { { STATISTIC_ATTRIBUTE_OVERVIEW_NB_OF_PAX, SUMMARY_KPI_NAME_TOTAL_PAX_NB },
                                               { STATISTIC_ATTRIBUTE_OVERVIEW_NB_TRANSFER_PAX, SUMMARY_KPI_NAME_TRANSFER_PAX_NB },
                                               { STATISTIC_ATTRIBUTE_OVERVIEW_NB_MISSED_PAX, SUMMARY_KPI_NAME_MISSED_PAX_NB },
                                               { STATISTIC_ATTRIBUTE_OVERVIEW_NB_STOPPED_PAX, SUMMARY_KPI_NAME_STOPPED_PAX_NB },
                                               { STATISTIC_ATTRIBUTE_OVERVIEW_NB_LOST_PAX, SUMMARY_KPI_NAME_LOST_PAX_NB },
                                               { STATISTIC_ATTRIBUTE_OVERVIEW_NB_PROCESSED_PAX, SUMMARY_KPI_NAME_TOTAL_PROCESSED_PAX_NB },
                                               { STATISTIC_ATTRIBUTE_REMAINING_TIME, SUMMARY_KPI_NAME_REMAINING_TIME },
                                               { TIME_STATS_TOTAL_TIME, SUMMARY_KPI_NAME_TOTAL_TIME }, 
                                               { TIME_STATS_DELAY_TIME , SUMMARY_KPI_NAME_DELAY_TIME},
                                               {TIME_STATS_PROCESS_TIME, SUMMARY_KPI_NAME_PROCESS},
                                               {TIME_STATS_WAITING_GROUP_TIME, SUMMARY_KPI_NAME_WAITING_GROUP},
                                               {TIME_STATS_WAITING_DESK_TIME, SUMMARY_KPI_NAME_WAITING_DESK},
                                               {STATISTIC_ATTRIBUTE_DWELL_AREA, SUMMARY_KPI_NAME_DWELL_AREA},
                                               {STATISTIC_ATTRIBUTE_QUEUE_OCCUPATION, SUMMARY_KPI_NAME_QUEUE_OCCUPATION},
                                               {STATISTIC_ATTRIBUTE_AREA_OCCUPATION, SUMMARY_KPI_NAME_AREA_OCCUPATION},
                                               {STATISTIC_ATTRIBUTE_OCCUPATION_INPUT, SUMMARY_KPI_NAME_THROUGHPUT_NB_PAX_IN},
                                               {STATISTIC_ATTRIBUTE_OCCUPATION_OUTPUT, SUMMARY_KPI_NAME_THROUGHPUT_NB_PAX_OUT},
                                               {STATISTIC_ATTRIBUTE_THROUGHPUT_INPUT, SUMMARY_KPI_NAME_THROUGHPUT_INPUT},
                                               {STATISTIC_ATTRIBUTE_THROUGHPUT_OUTPUT, SUMMARY_KPI_NAME_THROUGHPUT_OUTPUT},
                                               {STATISTIC_ATTRIBUTE_UTILIZATION_DESK_NEED, SUMMARY_KPI_NAME_DESK_NEED},
                                               {STATISTIC_ATTRIBUTE_UTILIZATION_PERCENT, SUMMARY_KPI_NAME_UTILIZATION_PERCENT}};
        // << Task #11212 Pax2Sim - Target - add missing Attribute degree

        // >> Task #11217 Pax2Sim - Target - add missing Attribute degree stage2
        public const String STATISTIC_ATTRIBUTE_OVERVIEW_NB_OF_PAX = "Nb of Pax";
        public const String STATISTIC_ATTRIBUTE_OVERVIEW_NB_TRANSFER_PAX = "Nb of Transfer Pax";
        public const String STATISTIC_ATTRIBUTE_OVERVIEW_NB_MISSED_PAX = "Nb of Missed Pax";
        public const String STATISTIC_ATTRIBUTE_OVERVIEW_NB_STOPPED_PAX = "Nb of Stopped Pax";
        public const String STATISTIC_ATTRIBUTE_OVERVIEW_NB_LOST_PAX = "Nb of Lost Pax";
        public const String STATISTIC_ATTRIBUTE_OVERVIEW_NB_PROCESSED_PAX = "Nb of Processed Pax";

        public const String STATISTIC_ATTRIBUTE_QUEUE_OCCUPATION = "Queue Occupation";
        public const String STATISTIC_ATTRIBUTE_AREA_OCCUPATION = "Area Occupation";
        public const String STATISTIC_ATTRIBUTE_OCCUPATION_INPUT = "Input Passengers";
        public const String STATISTIC_ATTRIBUTE_OCCUPATION_OUTPUT = "Output Passengers";
        public const String STATISTIC_ATTRIBUTE_THROUGHPUT_INPUT = "Throughput Input";
        public const String STATISTIC_ATTRIBUTE_THROUGHPUT_OUTPUT = "Throughput Output";

        public const String STATISTIC_ATTRIBUTE_DWELL_AREA = "Dwell Area";

        public const String STATISTIC_ATTRIBUTE_REMAINING_TIME = "RemainingTime";

        public const String STATISTIC_ATTRIBUTE_UTILIZATION_DESK_NEED = "Desks Need";
        public const String STATISTIC_ATTRIBUTE_UTILIZATION_PERCENT = "% Utilization";
        // << Task #11217 Pax2Sim - Target - add missing Attribute degree stage2

        // >> Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats
        public const String SUMMARY_TABLE_PERCENT_OF_GLOBAL_COLUMN_NAME = "% of Global";
        // << Task #11603 Pax2Sim - BHS analysis - Dashboard for Times stats

        // >> Task #13361 FP AutoMod Data tables V3        
        public const String SCENARIO_INPUT_DATA_DIRECTORY_NAME = "Scenario_Input_Data";

        public const String SCENARIO_INPUT_AUTOMOD_DATA_DIRECTORY_NAME = "Automod_Data";        
        public const String SCENARIO_FLIGHT_PLANS_DIRECTORY_NAME = "Flight_Plans";
        public const String SCENARIO_ALLOCATION_PLANNING_DIRECTORY_NAME = "Allocation_Planning";
        public const String SCENARIO_PASSENGER_DATA_DIRECTORY_NAME = "Passenger_Data";
        public const String SCENARIO_AIRPORT_PROCESS_DIRECTORY_NAME = "Airport_Process_Data";
        public const String SCENARIO_AIRPORT_AREA_CAPACITY_DIRECTORY_NAME = "Airport_Area_Capacity";
        public const String SCENARIO_AIRPORT_USER_ATTRIBUTES_DIRECTORY_NAME = "User_Attributes";
        public const String SCENARIO_AIRPORT_USER_DATA_DIRECTORY_NAME = "User_Data";
        public const String SCENARIO_BHS_DIRECTORY_NAME = "BHS_Data";
        public const String SCENARIO_EXCEPTION_TABLES_DIRECTORY_NAME = "_Exceptions";
                
        public const String SCENARIO_RESULT_FILTERS_TABLE_NAME = " ResultFilters";
        public const string SCENARIO_SETTINGS_TABLE_NAME = " Settings";

        public const String SCENARIO_AIRLINE_EXCEPTION_TABLE_SUFIX = "_AirlineExc";
        public const String SCENARIO_AIRLINE_FB_EXCEPTION_TABLE_SUFIX = "_AirlineFBExc";
        public const String SCENARIO_FLIGHT_EXCEPTION_TABLE_SUFIX = "_FlightExc";
        public const String SCENARIO_FLIGHT_FB_EXCEPTION_TABLE_SUFIX = "_FlightFBExc";
        public const String SCENARIO_FLIGHT_CATEG_EXCEPTION_TABLE_SUFIX = "_FlightCategoryExc";
        public const String SCENARIO_FLIGHT_CATEG_FB_EXCEPTION_TABLE_SUFIX = "_FlightCategoryFBExc";
        public const String SCENARIO_FB_EXCEPTION_TABLE_SUFIX = "_FBExc";

        public const String SCENARIO_TABLE_EXCEPTION_TAG = "_||EXC||";

        public static List<String> SCENARIO_INPUT_AUTOMOD_TABLE_NAMES_LIST 
            = new List<string>(new string[] { GestionDonneesHUB2SIM.FPA_TABLE_3_FOR_AUTOMOD_PAX2SIM_TABLE_NAME,
                                                GestionDonneesHUB2SIM.FPD_TABLE_3_FOR_AUTOMOD_PAX2SIM_TABLE_NAME });
        // << Task #13361 FP AutoMod Data tables V3

        // >> Task #13368 Project/Properties update
        public const int PROJECT_DEFAULT_ALLOCATION_STEP = 5;

        public const double PROJECT_DEFAULT_DISTRIBUTION_LEVEL_1 = 75;
        public const double PROJECT_DEFAULT_DISTRIBUTION_LEVEL_2 = 95;
        public const double PROJECT_DEFAULT_DISTRIBUTION_LEVEL_3 = 98;
        // << Task #13368 Project/Properties update

        internal const string NETWORK_VERSION_SUFFIX = ".NLS";        // >> Task #12391 Pax2Sim - Network use - Singapore University
        
        internal const string IS_LIEGE_ALLOCATION_PARAM_SCENARIO_ATTRIBUTE_NAME = "isLiegeAllocation";  // >> Bug #13367 Liege allocation

        internal const string IS_TRACE_ANALYSIS_SCENARIO_PARAM_SCENARIO_ATTRIBUTE_NAME = "isTraceAnalysisScenario"; // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues

        // >> Task #13422 Keywords improvement
        internal const string BHS_USER_DIRECTORY_NAME = "User";
        internal const string BHS_USER_TIMESTAMP_DIRECTORY_NAME = "User TimeStamp";
        internal const string BHS_USER_QUEUE_DIRECTORY_NAME = "User Queue";
        internal const string BHS_USER_CONV_DIRECTORY_NAME = "User Conveyor";
        internal const string BHS_USER_SECU_DIRECTORY_NAME = "User Security";
        internal const string BHS_USER_PASS_DIRECTORY_NAME = "User Passport";
        internal const string BHS_USER_VEH_DIRECTORY_NAME = "User Vehicle";
        internal const string BHS_USER_WORKCENTER_DIRECTORY_NAME = "User WorkCenter";
        internal const string BHS_USER_GATE_DIRECTORY_NAME = "User Gate";

        internal static List<string> BHS_USER_SUB_DIRECTORIES_NAMES
            = new List<string>(new string[] { BHS_USER_TIMESTAMP_DIRECTORY_NAME, BHS_USER_QUEUE_DIRECTORY_NAME, BHS_USER_CONV_DIRECTORY_NAME,
                                                BHS_USER_SECU_DIRECTORY_NAME, BHS_USER_PASS_DIRECTORY_NAME, BHS_USER_VEH_DIRECTORY_NAME, 
                                                BHS_USER_WORKCENTER_DIRECTORY_NAME, BHS_USER_GATE_DIRECTORY_NAME});

        internal const int BHS_OBJECTS_ON_3_INDEXES_LENGTH = 4; //BHS object: objectType_terminalNb_groupNb_stationNb

        internal const string BHS_USER_TERMINALS_SUB_DIRECTORY_NAME = "Terminals";
        internal const string BHS_USER_GROUPS_SUB_DIRECTORY_NAME = "Groups";
        internal const string BHS_USER_STATIONS_SUB_DIRECTORY_NAME = "Stations";

        internal const string BHS_IST_TABLE_BAG_ID_COLUMN_NAME = "Bag Id";
        internal const string BHS_IST_TABLE_PAX_ID_COLUMN_NAME = "Pax Id";
        internal const string BHS_IST_TABLE_STOPPED_COLUMN_NAME = "Stopped";
        internal const string BHS_IST_TABLE_ARRIVAL_OR_DEPARTURE_COLUMN_NAME = "Arrival or Departure";
        internal const string BHS_IST_TABLE_FLIGHT_ID_COLUMN_NAME = "Flight Id";
        internal const string BHS_IST_TABLE_FLIGHT_TIME_COLUMN_NAME = "Flight Time";
        

        internal const string BHS_IST_TABLE_STATION_TYPE_COLUMN_NAME = "Station Type";
        internal const string BHS_IST_TABLE_TERMINAL_NB_COLUMN_NAME = "Terminal Nb";
        internal const string BHS_IST_TABLE_GROUP_NB_COLUMN_NAME = "Group Nb";
        internal const string BHS_IST_TABLE_STATION_NB_COLUMN_NAME = "Station Nb";

        internal const string BHS_IST_TABLE_ARRIVING_TIME_COLUMN_NAME = "Arriving Time at Station";
        internal const string BHS_IST_TABLE_LEAVING_TIME_COLUMN_NAME = "Leaving Time from the Station";
        internal const string BHS_IST_TABLE_OCCUPATION_TIME_COLUMN_NAME = "Occupation Time (Minutes)";
        internal const string BHS_IST_TABLE_TRAVEL_TIME_TO_NEXT_COLUMN_NAME = "Travel Time to Next Station (Minutes)";

        #region Terminating columns
        internal const string BHS_IST_TABLE_TIME_AT_QUEUE_COLUMN_NAME = "Time At Queue";
        internal const string BHS_IST_TABLE_TIME_AT_INFEED_COLUMN_NAME = "Time At Infeed";
        internal const string BHS_IST_TABLE_TOTAL_TIME_FROM_QUEUE_COLUMN_NAME = "Total Time from Queue";
        internal const string BHS_IST_TABLE_TOTAL_TIME_FROM_INFEED_COLUMN_NAME = "Total Time from Infeed";
        internal const string BHS_IST_TABLE_INFEED_NB_COLUMN_NAME = "Infeed Nb";
        internal const string BHS_IST_TABLE_RECLAIM_NB_COLUMN_NAME = "Reclaim Nb";
        internal const string BHS_IST_TABLE_TERM_HBS_NB_COLUMN_NAME = "HBS Nb (Terminating)";
        internal const string BHS_IST_TABLE_CUSTOM_COLUMN_NAME = "Custom";        
        #endregion

        #region Departing columns
        internal const string BHS_IST_TABLE_TIME_AT_CI_QUEUE_COLUMN_NAME = "Time At CI-Queue";
        internal const string BHS_IST_TABLE_TIME_AT_CI_COLLECTOR_COLUMN_NAME = "Time At CI Collector";
        internal const string BHS_IST_TABLE_TIME_AT_DEP_READER_TS_COLUMN_NAME = "Time At CI Dep-Reader-TimeStamp";
        internal const string BHS_IST_TABLE_ARRIVING_TIME_AT_FIRST_PRESORTATION_COLUMN_NAME = "Arriving Time At First Pre-Sortation";
        internal const string BHS_IST_TABLE_LEAVING_TIME_FROM_LAST_PRESORTATION_COLUMN_NAME = "Leaving Time from Last Pre-Sortation";
        internal const string BHS_IST_TABLE_TIME_AT_ICS_TOPLOADER_COLUMN_NAME = "Time At ICS Toploader";
        internal const string BHS_IST_TABLE_TIME_AT_ICS_UNLOADER_COLUMN_NAME = "Time At ICS Unloader";
        internal const string BHS_IST_TABLE_TIME_AT_LAST_CHUTE_COLUMN_NAME = "Time At Last Chute";
        internal const string BHS_IST_TABLE_TIME_AT_MUP_COLUMN_NAME = "Time At Make-up";
        internal const string BHS_IST_TABLE_TIME_FROM_CIQ_TO_PRESORT_COLUMN_NAME = "Time from CI-Queue to First Pre-Sortation";
        internal const string BHS_IST_TABLE_TIME_FROM_CICOLL_TO_PRESORT_COLUMN_NAME = "Time from CI-Collector to First Pre-Sortation";
        internal const string BHS_IST_TABLE_TIME_FROM_CIQ_TO_DEP_READER_COLUMN_NAME = "Time from CI-Queue to Dep-Reader-TimeStamp";
        internal const string BHS_IST_TABLE_TIME_FROM_CIQ_TO_LAST_CHUTE_COLUMN_NAME = "Time from CI-Queue to Last Chute";
        internal const string BHS_IST_TABLE_TIME_FROM_CICOLL_TO_DEP_READER_COLUMN_NAME = "Time from CI-Collector to Dep-Reader-TimeStamp";
        internal const string BHS_IST_TABLE_TIME_FROM_CICOLL_TO_LAST_CHUTE_COLUMN_NAME = "Time from CI-Collector to Last Chute";
        internal const string BHS_IST_TABLE_TIME_FROM_DEP_READER_TO_LAST_CHUTE_COLUMN_NAME = "Time from Dep-Reader-TimeStamp to Last Chute";
        internal const string BHS_IST_TABLE_TIME_FROM_LAST_CHUTE_TO_MUP_COLUMN_NAME = "Time from Last Chute to Make-up";
        internal const string BHS_IST_TABLE_TIME_FROM_CIQ_TO_EXIT_COLUMN_NAME = "Time from CI-Queue to Exit";
        internal const string BHS_IST_TABLE_TIME_FROM_CICOLL_TO_EXIT_COLUMN_NAME = "Time from CI-Collector to Exit";
        internal const string BHS_IST_TABLE_TIME_FROM_DEPREAD_TO_EXIT_COLUMN_NAME = "Time from Dep-Reader-TimeStamp to Exit";
        internal const string BHS_IST_TABLE_TIME_FROM_LAST_CHUTE_TO_EXIT_COLUMN_NAME = "Time from Last Chute to Exit";

        internal const string BHS_IST_TABLE_CIQ_NB_COLUMN_NAME = "CheckIn Queue Nb";
        internal const string BHS_IST_TABLE_CI_NB_COLUMN_NAME = "CheckIn Nb";
        internal const string BHS_IST_TABLE_CICOLL_NB_COLUMN_NAME = "CheckIn Collector Nb";
        internal const string BHS_IST_TABLE_DEPREAD_NB_COLUMN_NAME = "Dep-Reader-TimeStamp Nb";
        internal const string BHS_IST_TABLE_ICS_TOPLOADER_NB_COLUMN_NAME = "ICS Topleader Nb";
        internal const string BHS_IST_TABLE_ICS_UNLOADER_NB_COLUMN_NAME = "ICS Unloader Nb";
        internal const string BHS_IST_TABLE_MUP_NB_COLUMN_NAME = "Make-up Nb";
        internal const string BHS_IST_TABLE_LAST_CHUTE_NB_COLUMN_NAME = "Last Chute";
        internal const string BHS_IST_TABLE_HBS1_NB_COLUMN_NAME = "HBS1 Nb";
        internal const string BHS_IST_TABLE_HBS2_NB_COLUMN_NAME = "HBS2 Nb";
        internal const string BHS_IST_TABLE_HBS3_NB_COLUMN_NAME = "HBS3 Nb";
        internal const string BHS_IST_TABLE_HBS4_NB_COLUMN_NAME = "HBS4 Nb";
        internal const string BHS_IST_TABLE_HBS5_NB_COLUMN_NAME = "HBS5 Nb";
        internal const string BHS_IST_TABLE_INTERLINK_NB_COLUMN_NAME = "IntLink Nb";
        internal const string BHS_IST_TABLE_MES_NB_COLUMN_NAME = "MES Nb";
        internal const string BHS_IST_TABLE_EBS_NB_COLUMN_NAME = "EBS Nb";
        internal const string BHS_IST_TABLE_TRANSFER_INFEED_NB_COLUMN_NAME = "Transfer Infeed Nb";
        internal const string BHS_IST_TABLE_TRANSFER_INFEED_QUEUE_NB_COLUMN_NAME = "Transfer Infeed Queue Nb";
        internal const string BHS_IST_TABLE_PBC_TYPE_COLUMN_NAME = "PBC Type";
        #endregion

        #region Common (Term + Dep) columns
        internal const string BHS_IST_TABLE_TIME_AT_EXIT_COLUMN_NAME = "Time At Exit";
        internal const string BHS_IST_TABLE_BEFORE_WARMUP_COLUMN_NAME = "Before WarmUp";
        internal const string BHS_IST_TABLE_RECIRCULATION_COLUMN_NAME = "Recirculation";                
        #endregion

        internal const string BHS_TRACE_ARRIVAL_TAG = "Arrival";
        internal const string BHS_TRACE_DEPARTURE_TAG = "Departure";
        // << Task #13422 Keywords improvement

        // >> Task #13390 Targets for BHS Analysis output tables        
        public const string BAG_HANDLING_SYSTEM_DIRECTORY_NAME = "Baggage Handling System";
        // << Task #13390 Targets for BHS Analysis output tables

        public const string BHS_IST_MAKEUP_GROUP_COLUMN_NAME = "MakeUp Group";  // >> Task #13659 IST MakeUp segregation
        public const string BHS_IST_MAKEUP_GROUP_S3_TAG = "S3";  // >> Task #13659 IST MakeUp segregation
        public const string BHS_IST_MAKEUP_GROUP_S4_TAG = "S4";  // >> Task #13659 IST MakeUp segregation

        public const string OCT_MAKE_UP_LATE_TIME_COLUMN_NAME = "Make-Up Late Time (Min before STD)";// New Mup OCT parameter

        public const string REPORTS_MAIN_NODE_NAME = "Reports";  // >> Task #13384 Report Tree-view

        public const string DOCUMENTS_MAIN_NODE_NAME = "Documents"; // >> Task #16578 PAX2SIM - Documents - new node in main tree-view
        
        public const string DOCUMENTS_XML_NODE_NAME = "Documents";
        public const string DOCUMENT_XML_NODE_NAME = "Document";
        public const string DOCUMENT_XML_NODE_NAME_ATTRIBUTE = "Name";
        
        public const string DOCUMENTS_DIRECTORY_NAME = "Documents";

        // >> Report generate/preview error - Illegal char in path
        public const string SPECIAL_CHAR_SLASH = "/";
        public const string SPECIAL_CHAR_BACK_SLASH = "\\";
        public const string SPECIAL_CHAR_LESS_THAN = "<";
        public const string SPECIAL_CHAR_GREATER_THAN = ">";
        public const string SPECIAL_CHAR_PIPE = "|";
        public const string SPECIAL_CHAR_COLON = ":";
        public const string SPECIAL_CHAR_QUESTION_MARK = "?";
        public const string SPECIAL_CHAR_ASTERISK = "*";
        public const string SPECIAL_CHAR_QUOTATION_MARK = "\"";
        public const string SPECIAL_CHAR_UNDERSCORE = "_";

        internal static List<string> ILLEGAL_CHARACTERS_IN_FILE_PATH
            = new List<string>(new string[] { SPECIAL_CHAR_SLASH, SPECIAL_CHAR_LESS_THAN,
                                                SPECIAL_CHAR_GREATER_THAN, SPECIAL_CHAR_PIPE, 
                                                SPECIAL_CHAR_QUESTION_MARK, SPECIAL_CHAR_ASTERISK, SPECIAL_CHAR_QUOTATION_MARK});

        internal static List<string> ILLEGAL_CHARACTERS_FOR_FILTER_NAME
            = new List<string>(new string[] { SPECIAL_CHAR_SLASH, SPECIAL_CHAR_BACK_SLASH, SPECIAL_CHAR_LESS_THAN, 
                                                SPECIAL_CHAR_GREATER_THAN, SPECIAL_CHAR_PIPE, SPECIAL_CHAR_COLON,
                                                SPECIAL_CHAR_QUESTION_MARK, SPECIAL_CHAR_ASTERISK, SPECIAL_CHAR_QUOTATION_MARK});

        internal const string RECLAIM_LOG_FILE_NAME = "ReclaimLog"; // >> Task #8958 Reclaim Synchronisation mode Gantt

        #region Version 1.15
        #region FPDTable 1.15
        #region OldTable
        public static String[] listeEnteteFPDTable_1_15 = new String[]{ sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        "NB PAX",
                                                        "Terminal CI", 
                                                        sFPD_Column_Eco_CI_Start, 
                                                        sFPD_Column_Eco_CI_End, 
                                                        "First Class CI start", 
                                                        "First Class CI end", 
                                                        "Terminal Gate", 
                                                        sFPD_Column_BoardingGate ,

                                                        "Terminal Make-Up", 
                                                        "First Make-Up",
                                                        "Last Make-Up" ,
                                                        "Terminal Parking",

                                                        "Parking",
                                                        sFPD_A_Column_RunWay};
        public static Type[] listeTypeEnteteFPDTable_1_15 = new Type[]{ System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.DateTime"), 
                                                          System.Type.GetType("System.TimeSpan"), 
                                                          System.Type.GetType("System.String"), 
                                                          System.Type.GetType("System.String") ,
                                                          System.Type.GetType("System.String") ,
                                                          System.Type.GetType("System.String") ,
                                                          System.Type.GetType("System.String") ,
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32")};
        public static int[] listePrimaryKeyFPDTable_1_15 = new int[] { 0 };
        #endregion

        #region New FPDTable

        public static String[] listeEntete_LinksFPDTable_1_15 = new String[]{ sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        "",
                                                        "NB PAX",
                                                        "Terminal CI", 
                                                        sFPD_Column_Eco_CI_Start, 
                                                        sFPD_Column_Eco_CI_End, 
                                                        "First Class CI start", 
                                                        "First Class CI end", 
                                                        "",
                                                        "",
                                                        "Terminal Gate", 
                                                        sFPD_Column_BoardingGate ,

                                                        "Terminal Make-Up", 
                                                        "First Make-Up",
                                                        "Last Make-Up" ,
                                                        "Terminal Parking",

                                                        "Parking",
                                                        sFPD_A_Column_RunWay};
        public static String[] ListeEntete_NewFPDTable_1_15 = { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        "NB PAX",
                                                        "Terminal CI", 
                                                        sFPD_Column_Eco_CI_Start, 
                                                        sFPD_Column_Eco_CI_End, 
                                                        "First Class CI start", 
                                                        "First Class CI end", 
                                                        "Start Bag Drop",
                                                        "End Bag Drop",
                                                        "Terminal Gate", 
                                                        sFPD_Column_BoardingGate ,

                                                        "Terminal Make-Up", 
                                                        "First Make-Up",
                                                        "Last Make-Up" ,
                                                        "Terminal Parking",

                                                        "Parking",
                                                        sFPD_A_Column_RunWay};

        public static Type[] ListeTypeEntete_NewFPDTable_1_15 = { System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.DateTime"), 
                                                          System.Type.GetType("System.TimeSpan"), 
                                                          typeof(String), 
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          System.Type.GetType("System.Boolean"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32")};
        public static int[] ListePrimaryKey_NewFPDTable_1_15 = { 0 };
        public static Object[] ListeDefault_NewFPDTable_1_15 = { null, null, null, "", "", "", "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        #endregion
        #endregion

        #region FPATable 1.15

        public static String[] listeEnteteFPATable_1_15 = new String[] { sFPD_A_Column_ID, sFPD_A_Column_DATE, sFPA_Column_STA, sFPD_A_Column_AirlineCode, sFPD_A_Column_FlightN, sFPD_A_Column_AirportCode, sFPD_A_Column_FlightCategory, sFPD_A_Column_AircraftType, "NB PAX", "Terminal Gate", sFPA_Column_ArrivalGate, "Terminal Reclaim", sFPA_Column_ReclaimObject, "Terminal Transfer", sFPA_Column_TransferInfeedObject, "Terminal Parking", "Parking", sFPD_A_Column_RunWay };
        public static Type[] listeTypeEnteteFPATable_1_15 = new Type[]{ System.Type.GetType("System.Int32"), System.Type.GetType("System.DateTime"), System.Type.GetType("System.TimeSpan"), System.Type.GetType("System.String"), 
                                                          System.Type.GetType("System.String") ,System.Type.GetType("System.String") ,System.Type.GetType("System.String") ,System.Type.GetType("System.String") ,
                                                          System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"),System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32")};
        public static int[] listePrimaryKeyFPATable_1_15 = new int[] { 0 };


        #region FPATable
        public static String[] ListeEntete_LinksFPATable_1_15 = { sFPD_A_Column_ID, sFPD_A_Column_DATE, sFPA_Column_STA, sFPD_A_Column_AirlineCode, sFPD_A_Column_FlightN, sFPD_A_Column_AirportCode, sFPD_A_Column_FlightCategory, sFPD_A_Column_AircraftType, "", "NB PAX", "Terminal Gate", sFPA_Column_ArrivalGate, "Terminal Reclaim", sFPA_Column_ReclaimObject, "Terminal Transfer", sFPA_Column_TransferInfeedObject, "Terminal Parking", "Parking", sFPD_A_Column_RunWay };
        public static String[] ListeEntete_NewFPATable_1_15 = { sFPD_A_Column_ID, sFPD_A_Column_DATE, sFPA_Column_STA, sFPD_A_Column_AirlineCode, sFPD_A_Column_FlightN, sFPD_A_Column_AirportCode, sFPD_A_Column_FlightCategory, sFPD_A_Column_AircraftType, sFPA_Column_NoBSM, "NB PAX", "Terminal Gate", sFPA_Column_ArrivalGate, "Terminal Reclaim", sFPA_Column_ReclaimObject, "Terminal Transfer", sFPA_Column_TransferInfeedObject, "Terminal Parking", "Parking", sFPD_A_Column_RunWay };
        public static Type[] ListeTypeEntete_NewFPATable_1_15 = { System.Type.GetType("System.Int32"), System.Type.GetType("System.DateTime"), System.Type.GetType("System.TimeSpan"), typeof(String), 
                                                          typeof(String) ,typeof(String) ,typeof(String) ,typeof(String) ,
                                                          System.Type.GetType("System.Boolean"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32")/*, System.Type.GetType("System.Int32"),System.Type.GetType("System.Int32")*/};
        public static int[] ListePrimaryKey_NewFPATable_1_15 = { 0 };
        public static Object[] ListeDefault_NewFPATable_1_15 = { null, null, null, "", "", "", "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        #endregion
        #endregion
        #endregion

        #region Version 1.16
        #region FP_AirlineCodesTable
        #region old Version
        internal static String[] ListeEntete_FP_AirlineCodesTable_1_16 ={ sFPAirline_AirlineCode, sFPAirline_Description };
        internal static Type[] ListeTypeEntete_FP_AirlineCodesTable_1_16 = { System.Type.GetType("System.String"), System.Type.GetType("System.String") };
        internal static int[] ListePrimaryKey_FP_AirlineCodesTable_1_16 = { 0 };
        #endregion
        #region new Version
        internal static String[] ListeEntete_LinksFP_AirlineCodesTable_1_16 ={ sFPAirline_AirlineCode, sFPAirline_Description, "" };
        internal static String[] ListeEntete_NewFP_AirlineCodesTable_1_16 ={ sFPAirline_AirlineCode, sFPAirline_Description, sFPAirline_GroundHandlers };
        internal static Type[] ListeTypeEntete_NewFP_AirlineCodesTable_1_16 = { typeof(String), typeof(String), typeof(String) };
        internal static int[] ListePrimaryKey_NewFP_AirlineCodesTable_1_16 = { 0 };
        #endregion
        #endregion

        #region ListeLignesAllocationRulesTable
        internal static string[] ListeOldLignesAllocationRulesTable_1_16 = { "Make Up Opening Time (Min before STD)",
                                                                       "Make Up Closing Time (Min before STD)", 
                                                                       "Number of allocated Make-Up per flight"};
        internal static string[] ListeLignesAllocationRulesTable_1_16 = { "Make Up Opening Time (Min before STD)",
                                                                       "Make Up Closing Time (Min before STD)", 
                                                                       "EBS delivery time (Min before STD)",
                                                                       "Number of allocated Make-Up per flight",
                                                                       "Segregation number",
                                                                       "Container size",
                                                                       "DeadTime (Time to change the container) (s)",
                                                                       "Number of container per Lateral"};

        internal static string[] ListeLinksAllocationRulesTable_1_16 = { "Make Up Opening Time (Min before STD)",
                                                                       "Make Up Closing Time (Min before STD)", 
                                                                       "",
                                                                       "Number of allocated Make-Up per flight",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        ""};
        internal static Object[] Default_AllocationRulesTable_1_16 = { 180.0, 30.0, 5.0, 3.0, 40.0, 60.0, 3.0 };
        #endregion

        #region LignesProcess_BHS
        #region New
        internal static string[] Liste_LignesProcess_BHS_1_16 ={"Check-In Bag process time (mean flows mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "Transfer Infeed throughput (bag/h)",
                                    "Arrival Infeed throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)"
        };
        internal static string[] ListeLinksLignesProcess_BHS_1_16 ={ "Check-In Bag process time (Mean flow mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "",
                                    "",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "",
                                    "",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "Transfer Infeed throughput (bag/h)",
                                    "Arrival Infeed throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)"
        };
        #endregion
        #region Old
        internal static string[] Liste_Old_LignesProcess_BHS_1_16 ={ "Check-In Bag process time (Mean flow mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "Transfer Infeed throughput (bag/h)",
                                    "Arrival Infeed throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)"
        };
        #endregion
        #endregion
        #endregion

        #region Version 1.17
        #region FPDTable
        //Old -> Version 1.15
        #region New
        internal static Object[] ListeDefault_FPDTable_1_17 = { null, null, null, "", "", "", "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal static String[] ListeEntete_LinksFPDTable_1_17 = { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        "NB PAX",
                                                        "Terminal CI", 
                                                        sFPD_Column_Eco_CI_Start, 
                                                        sFPD_Column_Eco_CI_End, 
                                                        "First Class CI start", 
                                                        "First Class CI end", 
                                                        "Start Bag Drop",
                                                        "End Bag Drop",
                                                        "Terminal Gate", 
                                                        sFPD_Column_BoardingGate ,

                                                        "Terminal Make-Up", 
                                                        "First Make-Up",
                                                        "Last Make-Up" ,
                                                        "Terminal Parking",

                                                        "Parking",
                                                        sFPD_A_Column_RunWay};
        internal static String[] ListeEntete_NewFPDTable_1_17 = { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                        "Terminal CI", 
                                                        sFPD_Column_Eco_CI_Start, 
                                                        sFPD_Column_Eco_CI_End, 
                                                        "First Class CI start", 
                                                        "First Class CI end", 
                                                        "Start Bag Drop",
                                                        "End Bag Drop",
                                                        "Terminal Gate", 
                                                        sFPD_Column_BoardingGate ,

                                                        "Terminal Make-Up", 
                                                        "First Make-Up",
                                                        "Last Make-Up" ,
                                                        "Terminal Parking",

                                                        "Parking",
                                                        sFPD_A_Column_RunWay};

        internal static Type[] ListeTypeEntete_NewFPDTable_1_17 = { System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.DateTime"), 
                                                          System.Type.GetType("System.TimeSpan"), 
                                                          typeof(String), 
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          System.Type.GetType("System.Boolean"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32")};
        internal static int[] ListePrimaryKey_NewFPDTable_1_17 = { 0 };
        #endregion
        #endregion

        #region FPATable

        //Old -> Version 1.15
        #region New
        internal static String[] ListeEntete_LinksFPATable_1_17 = { sFPD_A_Column_ID, sFPD_A_Column_DATE, sFPA_Column_STA, sFPD_A_Column_AirlineCode, sFPD_A_Column_FlightN, sFPD_A_Column_AirportCode, sFPD_A_Column_FlightCategory, sFPD_A_Column_AircraftType, sFPA_Column_NoBSM, "NB PAX", "Terminal Gate", sFPA_Column_ArrivalGate, "Terminal Reclaim", sFPA_Column_ReclaimObject, "Terminal Transfer", sFPA_Column_TransferInfeedObject, "Terminal Parking", "Parking", sFPD_A_Column_RunWay };
        internal static String[] ListeEntete_NewFPATable_1_17 = { sFPD_A_Column_ID, sFPD_A_Column_DATE, sFPA_Column_STA, sFPD_A_Column_AirlineCode, sFPD_A_Column_FlightN, sFPD_A_Column_AirportCode, sFPD_A_Column_FlightCategory, sFPD_A_Column_AircraftType, sFPA_Column_NoBSM, sFPD_A_Column_NbSeats, "Terminal Gate", sFPA_Column_ArrivalGate, "Terminal Reclaim", sFPA_Column_ReclaimObject, "Terminal Transfer", sFPA_Column_TransferInfeedObject, "Terminal Parking", "Parking", sFPD_A_Column_RunWay };
        internal static Type[] ListeTypeEntete_NewFPATable_1_17 = { System.Type.GetType("System.Int32"), System.Type.GetType("System.DateTime"), System.Type.GetType("System.TimeSpan"), typeof(String), 
                                                          typeof(String) ,typeof(String) ,typeof(String) ,typeof(String) ,
                                                          System.Type.GetType("System.Boolean"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32")/*, System.Type.GetType("System.Int32"),System.Type.GetType("System.Int32")*/};
        internal static int[] ListePrimaryKey_NewFPATable_1_17 = { 0 };
        #endregion
        #endregion
        #endregion

        #region Version 1.18
        #region FP_AircraftTypesTable
        #region Old
        public static String[] ListeEntete_FP_AircraftTypesTable_1_18 ={ sFPAircraft_AircraftTypes, sFPAircraft_NumberSeats };
        public static Type[] ListeTypeEntete_FP_AircraftTypesTable_1_18 = { typeof(String), System.Type.GetType("System.Int32") };
        public static int[] ListePrimaryKey_FP_AircraftTypesTable_1_18 = { 0 };
        #endregion
        #region New
        public static String[] ListeEntete_LinksFP_AircraftTypesTable_1_18 = { sFPAircraft_AircraftTypes, 
                                                        sFPAircraft_NumberSeats, 
                                                        ""};
        public static String[] ListeEntete_NewFP_AircraftTypesTable_1_18 = {sFPAircraft_AircraftTypes, 
                                                        sFPAircraft_NumberSeats, 
                                                        sTableColumn_ULDLoose };

        public static Type[] ListeTypeEntete_NewFP_AircraftTypesTable_1_18 = { typeof(String), System.Type.GetType("System.Int32"), typeof(String) };
        public static int[] ListePrimaryKey_NewFP_AircraftTypesTable_1_18 = { 0 };
        public static Object[] ListeDefault_NewFP_AircraftTypesTable_1_18 = { "", 0, sTableContent_ULD };
        #endregion
        #endregion
        #endregion

        #region Version 1.20
        #region ListeLignesProcess_BHS
        //Old: V1.16
        #region New
        internal static string[] ListeLignesProcess_BHS_New_1_20 ={ "Check-In Bag process time (mean flows mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "Transfer Infeed throughput (bag/h)",
                                    "Arrival Infeed throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)",
                                    "Sorter Induction interval (trays)",
                                    "Sorter Tilt interval (trays)"
        };
        internal static Object[] ListeValuesProcess_BHS_1_20 = new Object[]{0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,0.0,0.0};
        internal static string[] ListeLinksLignesProcess_BHS_1_20 ={"Check-In Bag process time (mean flows mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "Transfer Infeed throughput (bag/h)",
                                    "Arrival Infeed throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)",
                                    "",
                                    ""
        };
        #endregion
        #region New Delhi
        internal static string[] ListeLignesProcess_BHS_New_Delhi_1_20 = new String[]{"Check-In Bag process time (mean flows mode) (sec)",
                                    "HBS Lev.2 tracking loss rate (%)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 Automatic process time Min (sec)",
                                    "HBS Lev.3 Automatic process time Mode (sec)",
                                    "HBS Lev.3 Automatic process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.3 Manual process time Min (sec)",
                                    "HBS Lev.3 Manual process time Mode (sec)",
                                    "HBS Lev.3 Manual process time Max (sec)",
                                    "HBS Lev.3 Operators",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "Transfer Infeed 1 operator throughput (bag/h)",
                                    "Transfer Infeed 2 operator throughput (bag/h)",
                                    "DMRC Infeed operator throughput (bag/h)",
                                    "Arrival Infeed operator throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)",
                                    "Sorter Induction interval (trays)",
                                    "Sorter Tilt interval (trays)"};
        internal static Object[] ListeValuesProcess_BHS_Delhi_1_20 = new Object[]{0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0};
        #endregion
        #region Old Delhi
        internal static string[] ListeLinksLignesProcess_BHS_Delhi_1_20 = new String[]{"Check-In Bag process time (mean flows mode) (sec)",
                                    "HBS Lev.2 tracking loss rate (%)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 Automatic process time Min (sec)",
                                    "HBS Lev.3 Automatic process time Mode (sec)",
                                    "HBS Lev.3 Automatic process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.3 Manual process time Min (sec)",
                                    "HBS Lev.3 Manual process time Mode (sec)",
                                    "HBS Lev.3 Manual process time Max (sec)",
                                    "HBS Lev.3 Operators",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "Transfer Infeed 1 operator throughput (bag/h)",
                                    "Transfer Infeed 2 operator throughput (bag/h)",
                                    "DMRC Infeed operator throughput (bag/h)",
                                    "Arrival Infeed operator throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)",
                                    "",
                                    ""};
        internal static string[] ListeLignesProcess_BHS_Old_Delhi_1_20 = new String[]{"Check-In Bag process time (mean flows mode) (sec)",
                                    "HBS Lev.2 tracking loss rate (%)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 Automatic process time Min (sec)",
                                    "HBS Lev.3 Automatic process time Mode (sec)",
                                    "HBS Lev.3 Automatic process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.3 Manual process time Min (sec)",
                                    "HBS Lev.3 Manual process time Mode (sec)",
                                    "HBS Lev.3 Manual process time Max (sec)",
                                    "HBS Lev.3 Operators",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "Transfer Infeed 1 operator throughput (bag/h)",
                                    "Transfer Infeed 2 operator throughput (bag/h)",
                                    "DMRC Infeed operator throughput (bag/h)",
                                    "Arrival Infeed operator throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)"};
        #endregion
        #endregion
        #endregion

        #region Version 1.21
        #region ListeLignesOCT_BaggageClaim
        #region New
        internal static string[] ListeLignesOCT_BaggageClaim_New_1_21 = { "Baggage Claim Opening Time (Min after STA)", "Baggage Claim Closing Time (Min after STA)", "Container size (baggage)", "Number of processed container per cycle", "Dead time between two cycles (minutes)" };
        internal static Object[] ListeValuesOCT_BaggageClaim_1_21 = new Object[] { 0.0, 60.0, 40.0, 2.0, 0.0 };
        #endregion
        #region Old
        internal static string[] ListeLignesOCT_BaggageClaim_1_21 = { "Baggage Claim Opening Time (Min after STA)", "Baggage Claim Closing Time (Min after STA)" };
        internal static string[] ListeLinksLignesOCT_BaggageClaim_1_21 = { "Baggage Claim Opening Time (Min after STA)", "Baggage Claim Closing Time (Min after STA)", "", "", "" };
        #endregion
        #endregion
        #endregion

        #region Version 1.22
        #region LignesProcess
        #region New
        internal static string[] ListeLignesProcess_BHS_New_1_22 ={ "Check-In Bag process time (mean flows mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 TimeOut (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "HBS Lev.4 TimeOut (sec)",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "Transfer Infeed throughput (bag/h)",
                                    "Arrival Infeed throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)",
                                    "Sorter Induction interval (trays)",
                                    "Sorter Tilt interval (trays)"
        };
        internal static Object[] ListeValuesProcess_BHS_1_22 = new Object[]{0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0};
        #endregion
        #region Old
        internal static string[] ListeLignesProcess_BHS_1_22 ={"Check-In Bag process time (mean flows mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "Transfer Infeed throughput (bag/h)",
                                    "Arrival Infeed throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)",
                                    "Sorter Induction interval (trays)",
                                    "Sorter Tilt interval (trays)"
        };
        #endregion
        #endregion
        #region FPDTable
        //Old : 1.17
        #region New
        internal static String[] listeEntete_LinksFPDTable_1_22 = new String[]{  sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                        "Terminal CI", 
                                                        sFPD_Column_Eco_CI_Start, 
                                                        sFPD_Column_Eco_CI_End, 
                                                        "First Class CI start", 
                                                        "First Class CI end", 
                                                        "Start Bag Drop",
                                                        "End Bag Drop",
                                                        "",
                                                        "",
                                                        "Terminal Gate", 
                                                        sFPD_Column_BoardingGate ,

                                                        "Terminal Make-Up", 
                                                        "First Make-Up",
                                                        "Last Make-Up" ,
                                                        "Terminal Parking",

                                                        "Parking",
                                                        sFPD_A_Column_RunWay};
        internal static String[] ListeEntete_NewFPDTable_1_22 = { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                        "Terminal CI", 
                                                        sFPD_Column_Eco_CI_Start, 
                                                        sFPD_Column_Eco_CI_End, 
                                                        "First Class CI start", 
                                                        "First Class CI end", 
                                                        sFPD_Column_Eco_Drop_Start,
                                                        sFPD_Column_Eco_Drop_End,
                                                        sFPD_Column_FB_Drop_Start,
                                                        sFPD_Column_FB_Drop_End,
                                                        "Terminal Gate", 
                                                        sFPD_Column_BoardingGate ,

                                                        "Terminal Make-Up", 
                                                        "First Make-Up",
                                                        "Last Make-Up" ,
                                                        "Terminal Parking",

                                                        "Parking",
                                                        sFPD_A_Column_RunWay};

        internal static Type[] ListeTypeEntete_NewFPDTable_1_22 = { System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.DateTime"), 
                                                          System.Type.GetType("System.TimeSpan"), 
                                                          typeof(String), 
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          System.Type.GetType("System.Boolean"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32")};
        internal static int[] ListePrimaryKey_NewFPDTable_1_22 = { 0 };
        internal static Object[] ListeDefault_NewFPDTable_1_22 = { null, null, null, "", "", "", "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        #endregion
        #endregion
        #endregion

        #region Version 1.23
        #region Animated_QueuesTable
        #region Old
        public static String[] ListeEntete_Old_Animated_QueuesTable_1_23 = { "Name" };
        public static Type[] ListeTypeEntete_Old_Animated_QueuesTable_1_23 = { typeof(String) };
        public static int[] ListePrimaryKey_Old_Animated_QueuesTable_1_23 = { 0 };
        #endregion
        #region New
        public static String[] listeEntete_LinksAnimated_QueuesTable_1_23 = new String[] { "Name", "" };
        public static String[] ListeEntete_New_Animated_QueuesTable_1_23 = { "Name", "Value" };
        public static Type[] ListeTypeEntete_New_Animated_QueuesTable_1_23 = { typeof(String), typeof(Int32) };
        public static int[] ListePrimaryKey_New_Animated_QueuesTable_1_23 = { 0 };
        public static Object[] ListeDefault_NewFPDTable_1_23 = { "", 0 };
        #endregion
        #endregion
        #endregion

        #region Version 1.24
        #region FPDTable
        //Old : 1.22
        #region New

        internal static String[] listeEntete_LinksFPDTable_1_24 = new String[] { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                        "Terminal CI", 
                                                        sFPD_Column_Eco_CI_Start, 
                                                        sFPD_Column_Eco_CI_End, 
                                                        "First Class CI start", 
                                                        "First Class CI end", 
            
                                                        sFPD_Column_Eco_Drop_Start,
                                                        sFPD_Column_Eco_Drop_End,
                                                        "First Bag Drop start",
                                                        "First Bag Drop end",
                                                        "Terminal Gate", 
                                                        sFPD_Column_BoardingGate ,

                                                        "Terminal Make-Up", 
                                                        "First Make-Up",
                                                        "Last Make-Up" ,
                                                        "Terminal Parking",

                                                        "Parking",
                                                        sFPD_A_Column_RunWay,
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        ""};

        internal static String[] ListeEntete_New_FPDTable_1_24 = { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                         sFPD_Column_TerminalCI,
                                                         sFPD_Column_Eco_CI_Start,
                                                         sFPD_Column_Eco_CI_End,
                                                         sFPD_Column_FB_CI_Start,
                                                         sFPD_Column_FB_CI_End,
                                                         sFPD_Column_Eco_Drop_Start,
                                                         sFPD_Column_Eco_Drop_End,
                                                         sFPD_Column_FB_Drop_Start,
                                                         sFPD_Column_FB_Drop_End,
                                                         sFPD_A_Column_TerminalGate,
                                                         sFPD_Column_BoardingGate,
                                                         sFPD_Column_TerminalMup,
                                                         sFPD_Column_Mup_Start,
                                                         sFPD_Column_Mup_End,
                                                         sFPD_A_Column_TerminalParking,
                                                         sFPD_A_Column_Parking,
                                                         sFPD_A_Column_RunWay,
                                                        sFPD_A_Column_User1,
                                                        sFPD_A_Column_User2,
                                                        sFPD_A_Column_User3,
                                                        sFPD_A_Column_User4,
                                                        sFPD_A_Column_User5};

        internal static Type[] ListeTypeEntete_New_FPDTable_1_24 = { System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.DateTime"), 
                                                          System.Type.GetType("System.TimeSpan"), 
                                                          typeof(String), 
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          typeof(String) ,
                                                          System.Type.GetType("System.Boolean"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"), 
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          System.Type.GetType("System.Int32"),
                                                          typeof(String),
                                                        typeof(String),
                                                        typeof(String),
                                                        typeof(String),
                                                        typeof(String)};
        internal static int[] ListePrimaryKey_New_FPDTable_1_24 = { 0 };
        internal static Object[] ListeDefault_New_FPDTable_1_24 = { null, null, null, "", "", "", "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "" };



        #endregion
        #endregion

        #region FPATable
        //Old : 1.17
        #region New FPATable

        public static String[] listeEntete_LinksFPATable_1_24 = new String[] { sFPD_A_Column_ID, 
                sFPD_A_Column_DATE, 
                sFPA_Column_STA, 
                sFPD_A_Column_AirlineCode, 
                sFPD_A_Column_FlightN, 
                sFPD_A_Column_AirportCode, 
                sFPD_A_Column_FlightCategory, 
                sFPD_A_Column_AircraftType,
                sFPA_Column_NoBSM, 
                "",
                sFPD_A_Column_NbSeats, 
                "Terminal Gate", 
                sFPA_Column_ArrivalGate, 
                "Terminal Reclaim", 
                sFPA_Column_ReclaimObject, 
                "Terminal Transfer",
                "",
                "",
                sFPA_Column_TransferInfeedObject, 
                "Terminal Parking", 
                "Parking", 
                sFPD_A_Column_RunWay,
                "",
                "",
                "",
                "",
                ""};

        public static String[] ListeEntete_New_FPATable_1_24 = { sFPD_A_Column_ID, 
                sFPD_A_Column_DATE, 
                sFPA_Column_STA, 
                sFPD_A_Column_AirlineCode, 
                sFPD_A_Column_FlightN, 
                sFPD_A_Column_AirportCode, 
                sFPD_A_Column_FlightCategory, 
                sFPD_A_Column_AircraftType,
                sFPA_Column_NoBSM, 
                sFPA_Column_CBP,
                sFPD_A_Column_NbSeats, 
                sFPD_A_Column_TerminalGate, 
                sFPA_Column_ArrivalGate, 
                sFPA_Column_TerminalReclaim, 
                sFPA_Column_ReclaimObject,
                sFPA_Column_TerminalInfeedObject,
                sFPA_Column_StartArrivalInfeedObject,
                sFPA_Column_EndArrivalInfeedObject,
                sFPA_Column_TransferInfeedObject, 
                sFPD_A_Column_TerminalParking, 
                sFPD_A_Column_Parking, 
                sFPD_A_Column_RunWay ,
                sFPD_A_Column_User1,
                sFPD_A_Column_User2,
                sFPD_A_Column_User3,
                sFPD_A_Column_User4,
                sFPD_A_Column_User5};
        public static Type[] ListeTypeEntete_New_FPATable_1_24 = { typeof(Int32), 
                                                            typeof(DateTime), 
                                                            typeof(TimeSpan), 
                                                            typeof(String), 
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(Boolean),
                                                            typeof(Boolean),
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(Int32),
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(Int32), 
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String)};
        public static int[] ListePrimaryKey_New_FPATable_1_24 = { 0 };
        public static Object[] ListeDefault_New_FPATable_1_24 = { null, null, null, "", "", "", "", "", false, false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "" };
        #endregion
        #endregion

        #region BagPlan
        #region Old
        public static String[] ListeEntete_Old_BagPlan_1_24 = { "Time(mn)", 
                                                       "ID_PAX", 
                                                       "FPA_ID", 
                                                       "FPA_Class", 
                                                       "FPD_ID", 
                                                       "FPD_Class", 
                                                       "STD", 
                                                       "NbBags",
                                                       "Segregation", 
                                                       "PassportLocal", 
                                                       "Transfer",
                                                       "Term.Arr", 
                                                       "#Arr.Gate", 
                                                       "Term.CI" , 
                                                       "#CI" };

        public static Type[] ListeTypeEntete_Old_BagPlan_1_24 = { typeof(Double), 
                                                       typeof(Int32), 
                                                       typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32)};
        public static int[] ListePrimaryKey_Old_BagPlan_1_24 = { };

        #endregion
        #region New
        public static String[] listeEntete_LinksBagPlan_1_24 = new String[] {"Time(mn)", 
                                                       "ID_PAX", 
                                                       "FPA_ID", 
                                                       "FPA_Class", 
                                                       "",
                                                       "FPD_ID", 
                                                       "FPD_Class", 
                                                       "STD", 
                                                       "NbBags",
                                                       "Segregation", 
                                                       "PassportLocal", 
                                                       "Transfer",
                                                       "Term.Arr", 
                                                       "#Arr.Gate", 
                                                       "Term.CI" , 
                                                       "#CI"};
        public static String[] ListeEntete_New_BagPlan_1_24 = { "Time(mn)", 
                                                       "ID_PAX", 
                                                       "FPA_ID", 
                                                       "FPA_Class", 
                                                       "PaxAtReclaim",
                                                       "FPD_ID", 
                                                       "FPD_Class", 
                                                       "STD", 
                                                       "NbBags",
                                                       "Segregation", 
                                                       "PassportLocal", 
                                                       "Transfer",
                                                       "Term.Arr", 
                                                       "#Arr.Gate", 
                                                       "Term.CI" , 
                                                       "#CI" };

        public static Type[] ListeTypeEntete_New_BagPlan_1_24 = { typeof(Double), 
                                                       typeof(Int32), 
                                                       typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                       typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32)};
        public static int[] ListePrimaryKey_New_BagPlan_1_24 = { };
        public static Object[] ListeDefault_New_BagPlan_1_24 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


        #endregion
        #endregion

        #region Baggage_Claim_Constraint
        internal static string[] ListeLignesBaggage_Claim_Constraint_New_1_24 = { "Infeed number", 
                                            "Infeed speed (sec/bags)", 
                                            "Limit Bag Acceptance (Number of bags)", 
                                            "Number of Bag Min", 
                                            "Number of Bag Max", 
                                            "Excluded categories", 
                                            "Excluded Ground Handler", 
                                            "Excluded Airline Code", 
                                            "Excluded Flight", 
                                            "Excluded Container type" };
        internal static string[] ListeLignesBaggage_Claim_Constraint_1_24 = { "Infeed number", 
                                            "Infeed speed (sec/bags)", 
                                            "Limit Bag Acceptance (Number of bags)", 
                                            "Number of Bag Min", 
                                            "Number of Bag Max", 
                                            "Excluded categories", 
                                            "Excluded Ground Handler", 
                                            "Excluded Airline Code", 
                                            "Excluded Flight", 
                                            "Exclude Container type" };
        internal static Object[] ListeValuesBaggage_Claim_Constraint_1_24 = new Object[]{"",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    ""};
        #endregion
        #endregion

        #region Version 1.25
        #region OCT_MakeUp (old AllocationRulesTable)
        //Old : 1.16 (AllocationRulesTable)
        #region New
        internal static string[] ListeLignesAllocationRulesTable_1_25 = { "Make-Up Opening Time (Min before STD)",
                                                                       "Make-Up Closing Time (Min before STD)", 
                                                                       "EBS delivery time (Min before STD)",
                                                                       "Number of allocated Make-Up per flight",
                                                                       "Make-Up Partial Opening/Closing Time (Min before STD)",
                                                                       "Number of Opened Make-Up at Partial Opening/Closing",
                                                                       "Segregation number",
                                                                       "Container size",
                                                                       "DeadTime (Time to change the container) (s)",
                                                                       "Number of container per Lateral"};

        internal static string[] ListeLinksAllocationRulesTable_1_25 = {"Make Up Opening Time (Min before STD)",
                                                                       "Make Up Closing Time (Min before STD)", 
                                                                       "EBS delivery time (Min before STD)",
                                                                       "Number of allocated Make-Up per flight",
                                                                       "",
                                                                       "",
                                                                       "Segregation number",
                                                                       "Container size",
                                                                       "DeadTime (Time to change the container) (s)",
                                                                       "Number of container per Lateral"};
        internal static Object[] Default_AllocationRulesTable_1_25 = { 180.0, 30.0, 5.0, 180.0, 5.0, 3.0, 40.0, 60.0, 3.0 };
        #endregion
        #endregion

        #region ListeLignesFPD_LF
        #region New
        internal static string[] ListeLignesFPD_LF_New_1_25 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local,
            sLFD_A_Line_NotLocal,
            sLFD_Line_SelfCI, 
            sLFD_Line_Originating, 
            sLFD_A_Line_Transferring,
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk ,
            sLFD_A_Line_OOGTransf,
            sLFD_Line_OOGOrig};
        internal static Object[] ListeDefaultFPD_LF_1_25 = { 90.0, 10.0, 90.0, 50.0, 50.0, 50.0, 80.0, 20.0, 10.0, 0.0, 0.0, 0.0 };
        #endregion
        #region Old
        internal static string[] ListeLinksLignesFPD_LF_1_25 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local,
            sLFD_A_Line_NotLocal,
            sLFD_Line_SelfCI, 
            sLFD_Line_Originating, 
            sLFD_A_Line_Transferring,
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk,
            "",
            ""};
        internal static string[] ListeLignesFPD_LF_1_25 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local,
            sLFD_A_Line_NotLocal,
            sLFD_Line_SelfCI, 
            sLFD_Line_Originating, 
            sLFD_A_Line_Transferring,
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk };
        #endregion
        #endregion

        #region ListeLignesFPA_LF
        #region New
        internal static string[] ListeLignesFPA_LF_New_1_25 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local, 
            sLFD_A_Line_NotLocal, 
            sLFA_Line_Terminating, 
            sLFD_A_Line_Transferring, 
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk,
            sLFD_A_Line_OOGTransf,
            sLFA_Line_OOGTerm};
        internal static Object[] ListeDefaultFPA_LF_1_25 = { 90.0, 10.0, 90.0, 50.0, 50.0, 90.0, 10.0, 50.0, 0.0, 0.0, 0.0 };
        #endregion
        #region Old
        internal static string[] ListeLignesFPA_LF_1_25 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local, 
            sLFD_A_Line_NotLocal, 
            sLFA_Line_Terminating, 
            sLFD_A_Line_Transferring, 
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk };
        internal static string[] ListeLinksLignesFPA_LF_1_25 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local, 
            sLFD_A_Line_NotLocal, 
            sLFA_Line_Terminating, 
            sLFD_A_Line_Transferring, 
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk,
            "",
            ""};
        #endregion
        #endregion

        #region FPDTable
        //Old : 1.24
        #region New

        internal static String[] listeEntete_LinksFPDTable_1_25 = new String[] { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                         sFPD_Column_TerminalCI,
                                                         sFPD_Column_Eco_CI_Start,
                                                         sFPD_Column_Eco_CI_End,
                                                         sFPD_Column_FB_CI_Start,
                                                         sFPD_Column_FB_CI_End,
                                                         sFPD_Column_Eco_Drop_Start,
                                                         sFPD_Column_Eco_Drop_End,
                                                         sFPD_Column_FB_Drop_Start,
                                                         sFPD_Column_FB_Drop_End,
                                                         sFPD_A_Column_TerminalGate,
                                                         sFPD_Column_BoardingGate,
                                                         sFPD_Column_TerminalMup,
                                                         sFPD_Column_Mup_Start,
                                                         sFPD_Column_Mup_End,
                                                         sFPD_A_Column_TerminalParking,
                                                         sFPD_A_Column_Parking,
                                                        sFPD_A_Column_RunWay,
                                                        sFPD_A_Column_User1,
                                                        sFPD_A_Column_User2,
                                                        sFPD_A_Column_User3,
                                                        sFPD_A_Column_User4,
                                                        sFPD_A_Column_User5};

        internal static String[] ListeEntete_New_FPDTable_1_25 = { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                         sFPD_Column_TerminalCI,
                                                         sFPD_Column_Eco_CI_Start,
                                                         sFPD_Column_Eco_CI_End,
                                                         sFPD_Column_FB_CI_Start,
                                                         sFPD_Column_FB_CI_End,
                                                         sFPD_Column_Eco_Drop_Start,
                                                         sFPD_Column_Eco_Drop_End,
                                                         sFPD_Column_FB_Drop_Start,
                                                         sFPD_Column_FB_Drop_End,
                                                         sFPD_A_Column_TerminalGate,
                                                         sFPD_Column_BoardingGate,
                                                         sFPD_Column_TerminalMup,
                                                         sFPD_Column_Mup_Start,
                                                         sFPD_Column_Mup_End,
                                                         sFPD_A_Column_TerminalParking,
                                                         sFPD_A_Column_Parking,
                                                        sFPD_A_Column_RunWay,
                                                        sFPD_A_Column_User1,
                                                        sFPD_A_Column_User2,
                                                        sFPD_A_Column_User3,
                                                        sFPD_A_Column_User4,
                                                        sFPD_A_Column_User5};

        internal static Type[] ListeTypeEntete_New_FPDTable_1_25 = { System.Type.GetType("System.Int32"), 
                                                            System.Type.GetType("System.DateTime"), 
                                                            System.Type.GetType("System.TimeSpan"), 
                                                            typeof(String), 
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            System.Type.GetType("System.Boolean"), 
                                                            System.Type.GetType("System.Int32"), 
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String)};
        internal static int[] ListePrimaryKey_New_FPDTable_1_25 = { 0 };
        internal static Object[] ListeDefault_New_FPDTable_1_25 = { null, null, null, "", "", "", "", "", false, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };



        #endregion
        #endregion

        #region FPATable
        //Old : 1.24
        #region New FPATable

        internal static String[] listeEntete_LinksFPATable_1_25 = new String[] { sFPD_A_Column_ID, 
                sFPD_A_Column_DATE, 
                sFPA_Column_STA, 
                sFPD_A_Column_AirlineCode, 
                sFPD_A_Column_FlightN, 
                sFPD_A_Column_AirportCode, 
                sFPD_A_Column_FlightCategory, 
                sFPD_A_Column_AircraftType,
                sFPA_Column_NoBSM, 
                sFPA_Column_CBP,
                sFPD_A_Column_NbSeats, 
                sFPD_A_Column_TerminalGate, 
                sFPA_Column_ArrivalGate, 
                sFPA_Column_TerminalReclaim, 
                sFPA_Column_ReclaimObject,
                sFPA_Column_TerminalInfeedObject,
                sFPA_Column_StartArrivalInfeedObject,
                sFPA_Column_EndArrivalInfeedObject,
                sFPA_Column_TransferInfeedObject, 
                sFPD_A_Column_TerminalParking, 
                sFPD_A_Column_Parking, 
                sFPD_A_Column_RunWay ,
                sFPD_A_Column_User1,
                sFPD_A_Column_User2,
                sFPD_A_Column_User3,
                sFPD_A_Column_User4,
                sFPD_A_Column_User5};

        internal static String[] ListeEntete_New_FPATable_1_25 = { sFPD_A_Column_ID, 
                sFPD_A_Column_DATE, 
                sFPA_Column_STA, 
                sFPD_A_Column_AirlineCode, 
                sFPD_A_Column_FlightN, 
                sFPD_A_Column_AirportCode, 
                sFPD_A_Column_FlightCategory, 
                sFPD_A_Column_AircraftType,
                sFPA_Column_NoBSM, 
                sFPA_Column_CBP,
                sFPD_A_Column_NbSeats, 
                sFPD_A_Column_TerminalGate, 
                sFPA_Column_ArrivalGate, 
                sFPA_Column_TerminalReclaim, 
                sFPA_Column_ReclaimObject,
                sFPA_Column_TerminalInfeedObject,
                sFPA_Column_StartArrivalInfeedObject,
                sFPA_Column_EndArrivalInfeedObject,
                sFPA_Column_TransferInfeedObject, 
                sFPD_A_Column_TerminalParking, 
                sFPD_A_Column_Parking, 
                sFPD_A_Column_RunWay ,
                sFPD_A_Column_User1,
                sFPD_A_Column_User2,
                sFPD_A_Column_User3,
                sFPD_A_Column_User4,
                sFPD_A_Column_User5};
        internal static Type[] ListeTypeEntete_New_FPATable_1_25 = { typeof(Int32), 
                                                            typeof(DateTime), 
                                                            typeof(TimeSpan), 
                                                            typeof(String), 
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(Boolean),
                                                            typeof(Boolean),
                                                            typeof(Int32), 
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String)};
        internal static int[] ListePrimaryKey_New_FPATable_1_25 = { 0 };
        internal static Object[] ListeDefault_New_FPATable_1_25 = { null, null, null, "", "", "", "", "", false, false, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        #endregion
        #endregion

        #region Pax_GenTransferLog
        #region Old
        internal static String[] ListeEntete_PaxGenTransferLog_Old_1_25 = { "PAX_ID", "FPA_ID", "FPA_Time", "PAX_Class", "FDP_Data" };
        internal static Type[] ListeTypeEntete_PaxGenTransferLog_Old_1_25 = { System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"),
                                                                System.Type.GetType("System.Int32"),System.Type.GetType("System.Int32"),
                                                                typeof(String)};
        #endregion
        #region New
        internal static String[] ListeEntete_PaxGenTransferLog_New_1_25 = { "PAX_ID", "FPA_ID", "FPA_Time", "PAX_Class", "Information" };
        internal static Type[] ListeTypeEntete_PaxGenTransferLog_New_1_25 = { System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32"),
                                                                System.Type.GetType("System.Int32"),System.Type.GetType("System.Int32"),
                                                                typeof(String)};
        internal static String[] ListeLinks_PaxGenTransferLog_New_1_25 = { "PAX_ID", "FPA_ID", "FPA_Time", "PAX_Class", "FDP_Data" };
        #endregion
        #endregion

        #region ListeEntete_NbTrolleyTable
        #region Old
        internal static String[] ListeEntete_NbTrolleyTable_Old_1_25 ={ "NbBaggage", "NbTrolley" };
        internal static Type[] ListeTypeEntete_NbTrolleyTable_Old_1_25 = { System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32") };
        internal static int[] ListePrimaryKey_NbTrolleyTable_Old_1_25 = { 0, 1 };
        #endregion
        #region New
        internal static String[] ListeLinksEntete_NbTrolleyTable_1_25 ={ "NbBaggage", "NbTrolley" };
        internal static String[] ListeEntete_NbTrolleyTable_New_1_25 ={ "NbBags", "NbTrolley" };
        internal static Type[] ListeTypeEntete_NbTrolleyTable_New_1_25 = { System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32") };
        internal static int[] ListePrimaryKey_NbTrolleyTable_New_1_25 = { 0, 1 };

        #endregion
        #endregion

        #region ListeLignesTransferInfeedAllocationRulesTable
        internal static string[] ListeLignesTransferInfeedAllocationRulesTable_Old_1_25 = { "Tranfer Opening Time (Min after STA)", "Transfer Closing Time (Min after STD)", "Number of allocated Transfer Infeed per flight" };

        internal static string[] ListeLinksTransferInfeedAllocationRulesTable_1_25 = { "Tranfer Opening Time (Min after STA)", "Transfer Closing Time (Min after STD)", "Number of allocated Transfer Infeed per flight" };

        internal static string[] ListeLignesTransferInfeedAllocationRulesTable_New_1_25 = { "Tranfer Opening Time (Min after STA)", "Transfer Closing Time (Min after STA)", "Number of allocated Transfer Infeed per flight" };

        internal static Object[] Default_TransferInfeedAllocationRulesTable_1_25 = { 10.0, 30.0, 2.0 };
        #endregion

        #region BagPlan
        #region New
        public static String[] listeEntete_LinksBagPlan_1_25 = new String[] {"Time(mn)", 
                                                       "ID_PAX", 
                                                       "FPA_ID", 
                                                       "FPA_Class", 
                                                       "PaxAtReclaim",
                                                       "FPD_ID", 
                                                       "FPD_Class", 
                                                       "STD", 
                                                       "",
                                                       "Segregation", 
                                                       "PassportLocal", 
                                                       "Transfer",
                                                       "Term.Arr", 
                                                       "#Arr.Gate", 
                                                       "Term.CI" , 
                                                       "#CI"};
        public static String[] ListeEntete_New_BagPlan_1_25 = { "Time(mn)", 
                                                       "ID_PAX", 
                                                       "FPA_ID", 
                                                       "FPA_Class", 
                                                       "PaxAtReclaim",
                                                       "FPD_ID", 
                                                       "FPD_Class", 
                                                       "STD", 
                                                       "OOG",
                                                       "Segregation", 
                                                       "PassportLocal", 
                                                       "Transfer",
                                                       "Term.Arr", 
                                                       "#Arr.Gate", 
                                                       "Term.CI" , 
                                                       "#CI" };

        public static Type[] ListeTypeEntete_New_BagPlan_1_25 = { typeof(Double), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32), 
                                                        typeof(Int32)};
        public static int[] ListePrimaryKey_New_BagPlan_1_25 = { };
        public static Object[] ListeDefault_New_BagPlan_1_25 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


        #endregion
        #endregion

        #region PaxPlan
        #region Old

        public static String[] ListeEntete_Old_PaxPlan_1_25 = { "PAX_ID", 
                                                       "CreationTime", 
                                                       "FPA_ID", 
                                                       "FPA_Class", 
                                                       "FPD_ID", 
                                                       "FPD_Class", 
                                                       "FPD_STD", 
                                                       "Self CheckIn",
                                                       "NbVisitors", 
                                                       "NbBags", 
                                                       "NBTrolleys",
                                                       "Segregation", 
                                                       "PassportLocal", 
                                                       "Transfer" };


        public static Type[] ListeTypeEntete_Old_PaxPlan_1_25 = { typeof(Int32), 
                                                                  typeof(Double), 
                                                                  typeof(Int32), 
                                                                  typeof(Int32), 
                                                                  typeof(Int32),
                                                                  typeof(Int32),
                                                                  typeof(Int32), 
                                                                  typeof(Int32), 
                                                                  typeof(Int32),
                                                                  typeof(Int32), 
                                                                  typeof(Int32),
                                                                  typeof(Int32),
                                                                  typeof(Int32),
                                                                  typeof(Int32)};


        #endregion
        #region New
        public static String[] ListeEntete_LinksPaxPlan_1_25 = { "PAX_ID", 
                                                       "CreationTime", 
                                                       "FPA_ID", 
                                                       "FPA_Class", 
                                                       "FPD_ID", 
                                                       "FPD_Class", 
                                                       "FPD_STD", 
                                                       "Self CheckIn",
                                                       "NbVisitors", 
                                                       "NbBags", 
                                                       "", 
                                                       "NBTrolleys",
                                                       "Segregation", 
                                                       "PassportLocal", 
                                                       "Transfer" };
        public static String[] ListeEntete_New_PaxPlan_1_25 = {
                                                        sPaxPlan_PaxID,
                                                        sPaxPlan_CreationTime,
                                                        sPaxPlan_FPAID,
                                                        sPaxPlan_FPAClass,
                                                        sPaxPlan_FPDID,
                                                        sPaxPlan_FPDClass,
                                                        sPaxPlan_FPDSTD,
                                                        sPaxPlan_SelfCI,
                                                        sPaxPlan_Visitors ,
                                                        "NbBags",
                                                        sPaxPlan_BagsOOG ,
                                                        sPaxPlan_Trolleys,
                                                        sPaxPlan_Segregation ,
                                                        sPaxPlan_Local ,
                                                        sPaxPlan_Transfer};
        
        
        public static Type[] ListeTypeEntete_New_PaxPlan_1_25 = { typeof(Int32), 
                                                                  typeof(Double), 
                                                                  typeof(Int32), 
                                                                  typeof(Int32), 
                                                                  typeof(Int32),
                                                                  typeof(Int32),
                                                                  typeof(Int32), 
                                                                  typeof(Int32), 
                                                                  typeof(Int32), 
                                                                  typeof(Int32),
                                                                  typeof(Int32), 
                                                                  typeof(Int32),
                                                                  typeof(Int32),
                                                                  typeof(Int32),
                                                                  typeof(Int32)};
        public static int[] ListePrimaryKey_New_PaxPlan_1_25 = { };
        public static Object[] ListeDefault_New_PaxPlan_1_25 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        #endregion
        #endregion
        #endregion

        #region Version 1.26
        #region FPDTable
        //Old : 1.24
        #region New

        internal static String[] listeEntete_LinksFPDTable_1_26 = new String[] { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                         sFPD_Column_TerminalCI,
                                                         sFPD_Column_Eco_CI_Start,
                                                         sFPD_Column_Eco_CI_End,
                                                         sFPD_Column_FB_CI_Start,
                                                         sFPD_Column_FB_CI_End,
                                                         sFPD_Column_Eco_Drop_Start,
                                                         sFPD_Column_Eco_Drop_End,
                                                         sFPD_Column_FB_Drop_Start,
                                                         sFPD_Column_FB_Drop_End,
                                                         sFPD_A_Column_TerminalGate,
                                                         sFPD_Column_BoardingGate,
                                                         sFPD_Column_TerminalMup,
                                                         sFPD_Column_Mup_Start,
                                                         sFPD_Column_Mup_End,
                                                         "",
                                                         "",
                                                         sFPD_A_Column_TerminalParking,
                                                         sFPD_A_Column_Parking,
                                                        sFPD_A_Column_RunWay,
                                                        sFPD_A_Column_User1,
                                                        sFPD_A_Column_User2,
                                                        sFPD_A_Column_User3,
                                                        sFPD_A_Column_User4,
                                                        sFPD_A_Column_User5};

        internal static String[] ListeEntete_New_FPDTable_1_26 = { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                         sFPD_Column_TerminalCI,
                                                         sFPD_Column_Eco_CI_Start,
                                                         sFPD_Column_Eco_CI_End,
                                                         sFPD_Column_FB_CI_Start,
                                                         sFPD_Column_FB_CI_End,
                                                         sFPD_Column_Eco_Drop_Start,
                                                         sFPD_Column_Eco_Drop_End,
                                                         sFPD_Column_FB_Drop_Start,
                                                         sFPD_Column_FB_Drop_End,
                                                         sFPD_A_Column_TerminalGate,
                                                         sFPD_Column_BoardingGate,
                                                         sFPD_Column_TerminalMup,
                                                        sFPD_Column_Eco_Mup_Start,
                                                        sFPD_Column_Eco_Mup_End,
                                                        sFPD_Column_First_Mup_Start,
                                                        sFPD_Column_First_Mup_End,
                                                         sFPD_A_Column_TerminalParking,
                                                         sFPD_A_Column_Parking,
                                                        sFPD_A_Column_RunWay,
                                                        sFPD_A_Column_User1,
                                                        sFPD_A_Column_User2,
                                                        sFPD_A_Column_User3,
                                                        sFPD_A_Column_User4,
                                                        sFPD_A_Column_User5};

        internal static Type[] ListeTypeEntete_New_FPDTable_1_26 = { System.Type.GetType("System.Int32"), 
                                                            System.Type.GetType("System.DateTime"), 
                                                            System.Type.GetType("System.TimeSpan"), 
                                                            typeof(String), 
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            System.Type.GetType("System.Boolean"), 
                                                            System.Type.GetType("System.Int32"), 
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String)};
        internal static int[] ListePrimaryKey_New_FPDTable_1_26 = { 0 };
        internal static Object[] ListeDefault_New_FPDTable_1_26 = { null, null, null, "", "", "", "", "", false, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "0", "0", "", "", "", "", "", "", "", "" };



        #endregion
        #endregion
        #endregion

        #region Version 1.28

        #region FP_AircraftTypesTable
        #region New
        public static String[] ListeEntete_Old_FP_AircraftTypesTable_1_28 = { sFPAircraft_AircraftTypes, 
                                                        sFPAircraft_NumberSeats, 
                                                        sTableColumn_ULDLoose};
        public static Type[] ListeTypeEntete_Old_FP_AircraftTypesTable_1_28 = { typeof(String), System.Type.GetType("System.Int32"), typeof(String) };
        public static String[] ListeEntete_LinksFP_AircraftTypesTable_1_28 = { sFPAircraft_AircraftTypes, 
                                                        "",
                                                        "",
                                                        "",
                                                        sFPAircraft_NumberSeats, 
                                                        sTableColumn_ULDLoose};
        public static String[] ListeEntete_NewFP_AircraftTypesTable_1_28 = {sFPAircraft_AircraftTypes, 
                                                        sFPAircraft_Description,
                                                        sFPAircraft_Wake,
                                                        sFPAircraft_Body,
                                                        sFPAircraft_NumberSeats, 
                                                        sTableColumn_ULDLoose };

        public static Type[] ListeTypeEntete_NewFP_AircraftTypesTable_1_28 = { typeof(String), typeof(String), typeof(String), typeof(String), System.Type.GetType("System.Int32"), typeof(String) };
        public static int[] ListePrimaryKey_NewFP_AircraftTypesTable_1_28 = { 0 };
        public static Object[] ListeDefault_FP_AircraftTypesTable_1_28 = { "","","","", 0, sTableContent_ULD };
        #endregion
        #endregion

        #region Process_BHS
        #region LignesProcess_BHS
        #region New
        internal static string[] Liste_LignesProcess_BHS_1_28 ={"Check-In Bag process time (Mean Flows mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 TimeOut (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "HBS Lev.4 TimeOut (sec)",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "Make-Up PickUp throughput (bag/h)",
                                    "Transfer Unload throughput (bag/h)",
                                    "Arrival Unload throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (%)",
                                    "Sorter recirculation limit (nb laps)",
                                    "Sorter Tilt interval (trays)",
                                    "Sorter Induction interval (trays)",
                                    "EBS operating ? (0/1)"
        };
        internal static string[] ListeLinksLignesProcess_BHS_1_28 ={ "Check-In Bag process time (mean flows mode) (sec)",
                                    "MES Short Process rate (%)",
                                    "MES Short process time (sec)",
                                    "MES Long process time (sec)",
                                    "HBS Lev.1 spacing (m)",
                                    "HBS Lev.1 Velocity (m/s)",
                                    "HBS Lev.2 process time Min (sec)",
                                    "HBS Lev.2 process time Mode (sec)",
                                    "HBS Lev.2 process time Max (sec)",
                                    "HBS Lev.2 TimeOut (sec)",
                                    "HBS Lev.2 Operators",
                                    "HBS Lev.3 process time Min (sec)",
                                    "HBS Lev.3 process time Mode (sec)",
                                    "HBS Lev.3 process time Max (sec)",
                                    "HBS Lev.3 spacing (m)",
                                    "HBS Lev.3 Velocity (m/s)",
                                    "HBS Lev.4 process time Min (sec)",
                                    "HBS Lev.4 process time Mode (sec)",
                                    "HBS Lev.4 process time Max (sec)",
                                    "HBS Lev.4 TimeOut (sec)",
                                    "HBS Lev.4 Operators",
                                    "HBS Lev.4 Hold inside ? (0/1)",
                                    "HBS Lev.5 process time Min (sec)",
                                    "HBS Lev.5 process time Mode (sec)",
                                    "HBS Lev.5 process time Max (sec)",
                                    "",
                                    "Transfer Infeed throughput (bag/h)",
                                    "Arrival Infeed throughput (bag/h)",
                                    "Number of Tray Sorters",
                                    "Sorter velocity (m/s)",
                                    "Sorter tray length (m)",
                                    "Sorter filling limit (nb bags)",
                                    "Sorter recirculation limit (nb laps)",
                                    "Sorter Tilt interval (trays)",
                                    "Sorter Induction interval (trays)",
                                    ""
        };
        #endregion
        #endregion
        #region Columns

        public static String[] ListeEntete_Old_BHS_Process_1_28 = { "Input datas", "Value" };
        public static Type[] ListeTypeEntete_Old_BHS_Process_1_28 = { typeof(String), typeof(Double) };
        public static int[] ListePrimaryKey_Old_BHS_Process_1_28 = { 0 };

        public static String[] ListeEntete_New_BHS_Process_1_28 = { "BHS Process data", "Value" };
        public static Type[] ListeTypeEntete_New_BHS_Process_1_28 = { typeof(String), typeof(Double) };
        public static int[] ListePrimaryKey_New_BHS_Process_1_28 = { 0 };
        public static String[] ListeEntete_Links_BHS_Process_1_28 = { "Input datas", "Value" };
        #endregion
        #endregion

        #region FPLinksTables
        #region New Lines
        private static string[] ListeColumnsFPLinksTable_1_28 = 
        {
            sFPLinks_Column_FPAID ,
            sFPLinks_Column_STA ,
            sFPLinks_Column_FPAName ,
            sFPLinks_Column_FPDID ,
            sFPLinks_Column_STD ,
            sFPLinks_Column_FPDName,
            sFPLinks_Column_RotationDuration
        };
        private static Type[] ListeTypeFPLinksTable_1_28 = 
        {
            typeof(int) ,
            typeof(DateTime),
            typeof(String),
            typeof(String) ,
            typeof(String),
            typeof(String),
            typeof(String)
        };
        private static Int32[] ListePrimaryKeyFPLinksTable_1_28 = 
        {
            0
        };
        #endregion
        #endregion

        #region SecurityAllocation table

        internal class ConvertAllocationSecurity_1_28 : ConvertClass
        {
            /// <summary>
            /// Constructor for this class.
            /// </summary>
            internal ConvertAllocationSecurity_1_28()
            {
            }
            /// <summary>
            /// This function will convert The old version of the security allocation table into it's new format.
            /// </summary>
            /// <param name="dtOldTable">The old table that needs to be converted</param>
            /// <returns>The converted table</returns>
            internal override DataTable Convert(DataTable dtOldTable)
            {
                if (dtOldTable == null)
                    return null;
                int i;
                DataTable dtNewTable = new DataTable(dtOldTable.TableName);
                for (i = 0; i < dtOldTable.Columns.Count; i++)
                {
                    if (i == 0)
                        dtNewTable.Columns.Add(dtOldTable.Columns[i].ColumnName, dtOldTable.Columns[i].DataType);
                    else
                        dtNewTable.Columns.Add(dtOldTable.Columns[i].ColumnName, typeof(String));
                }
                foreach (DataRow drRow in dtOldTable.Rows)
                {
                    DataRow drNewRow = dtNewTable.NewRow();
                    drNewRow[0] = drRow[0];
                    for (i = 1; i < dtOldTable.Columns.Count; i++)
                    {
                        drNewRow[i] = "3," + drRow[i].ToString();
                    }
                    dtNewTable.Rows.Add(drNewRow);
                }
                return dtNewTable;

            }
        }
        #endregion

        #endregion

        #region Version 1.52

        #region SecurityAllocation /TransfertAllocation/ PassPort allocation table

        internal class ConvertAllocationSecurity_1_52 : ConvertClass
        {
            /// <summary>
            /// Constructor for this class.
            /// </summary>
            internal ConvertAllocationSecurity_1_52()
            {
            }
            /// <summary>
            /// This function will convert The old version of the security/ Transfer/ passport allocation table into it's new format.
            /// </summary>
            /// <param name="dtOldTable">The old table that needs to be converted</param>
            /// <returns>The converted table</returns>
            internal override DataTable Convert(DataTable dtOldTable)
            {
                if (dtOldTable == null)
                    return null;
                int i;
                DataTable dtNewTable = new DataTable(dtOldTable.TableName);
                for (i = 0; i < dtOldTable.Columns.Count; i++)
                {
                    if (i == 0)
                        dtNewTable.Columns.Add(dtOldTable.Columns[i].ColumnName, dtOldTable.Columns[i].DataType);
                    else
                        dtNewTable.Columns.Add(dtOldTable.Columns[i].ColumnName, typeof(String));
                }
                bool bPassport = dtOldTable.TableName == Alloc_PassportCheckTableName;
                String sAll = ";" + sAllocation_NotApplicable + ";" +
                                sAllocation_All +";" +
                                sAllocation_All +";" +
                                sAllocation_All +";" +
                                sAllocation_All +";" +
                                sAllocation_All;
                String sAllFC = ";" + sAllocation_All +";" +
                                sAllocation_All +";" +
                                sAllocation_All;
                foreach (DataRow drRow in dtOldTable.Rows)
                {
                    DataRow drNewRow = dtNewTable.NewRow();
                    drNewRow[0] = drRow[0];
                    for (i = 1; i < dtOldTable.Columns.Count; i++)
                    {
                        String sValue = drRow[i].ToString();
                        if ((sValue == null) || (sValue.Length==0))
                            drNewRow[i] = "0" + sAll;
                        else
                        {
                            String[] Tmp = sValue.Split(',');
                            if (Tmp.Length == 1)
                            {
                                if (Tmp[0].Length == 0)
                                    drNewRow[i] = "0" + sAll;
                                else
                                    drNewRow[i] = Tmp[0] + sAll;
                            }
                            else
                            {
                                if (Tmp[0] == "1")
                                {
                                    Tmp[0] = sAllocation_NotApplicable + ";";
                                    if (bPassport)
                                        Tmp[0] = Tmp[0] + sAllocation_Local + ";" + sAllocation_All;
                                    else
                                        Tmp[0] = Tmp[0] + sAllocation_All + ";" + sAllocation_FB;


                                }
                                else if (Tmp[0] == "2")
                                {
                                    Tmp[0] = sAllocation_NotApplicable + ";";
                                    if (bPassport)
                                        Tmp[0] = Tmp[0] + sAllocation_NotLocal + ";" + sAllocation_All;
                                    else
                                        Tmp[0] = Tmp[0] + sAllocation_All + ";" + sAllocation_Eco;
                                }
                                else
                                {
                                    Tmp[0] = sAllocation_NotApplicable + ";" +
                                        sAllocation_All + ";" +
                                        sAllocation_All;
                                }
                                drNewRow[i] = Tmp[1] + ";" + Tmp[0] + sAllFC;
                            }
                        }
                    }
                    dtNewTable.Rows.Add(drNewRow);
                }
                return dtNewTable;

            }

        }
        #endregion

        #region OCT_CIShow
        #region New
        internal static string[] ListeOldLignesOCT_CITable_1_52 = { 
                                                                            sOCT_CI_Line_Opening ,
                                                                            sOCT_CI_Line_Closing 
                                                                        
                                                                        
                                                                        };
        internal static string[] ListeLignesOCT_CITable_1_52 = { 
                                                                            sOCT_CI_Line_Opening ,
                                                                            sOCT_CI_Line_Closing ,
                                                                            sOCT_CI_Line_Allocated
                                                                        
                                                                        
                                                                        };

        internal static string[] ListeLinksOCT_CITable_1_52 = {
                                                                            sOCT_CI_Line_Opening ,
                                                                            sOCT_CI_Line_Closing ,
                                                                            ""};
        internal static Object[] Default_OCT_CITable_1_52 = { 120.0, 30.0, 1 };
        #endregion
        #endregion

        #region ListeLignesFPD_LF
        #region New
        internal static string[] ListeLignesFPD_LF_New_1_52 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local,
            sLFD_A_Line_NotLocal,
            sLFD_Line_SelfCI, 
            sLFD_Line_Originating, 
            sLFD_A_Line_Transferring,
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk ,
            sLFD_A_Line_OOGTransf,
            sLFD_Line_OOGOrig,
            sLFD_A_Line_NbPaxPerCar};
        internal static Object[] ListeDefaultFPD_LF_1_52 = { 90.0, 10.0, 90.0, 50.0, 50.0, 50.0, 80.0, 20.0, 10.0, 0.0, 0.0, 0.0,1.0 };
        #endregion
        #region Old
        internal static string[] ListeLinksLignesFPD_LF_1_52 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local,
            sLFD_A_Line_NotLocal,
            sLFD_Line_SelfCI, 
            sLFD_Line_Originating, 
            sLFD_A_Line_Transferring,
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk,
            sLFD_A_Line_OOGTransf,
            sLFD_Line_OOGOrig,
            ""};
        
        #endregion
        #endregion

        #region ListeLignesFPA_LF
        #region New
        internal static string[] ListeLignesFPA_LF_New_1_52 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local, 
            sLFD_A_Line_NotLocal, 
            sLFA_Line_Terminating, 
            sLFD_A_Line_Transferring, 
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk,
            sLFD_A_Line_OOGTransf,
            sLFA_Line_OOGTerm,
            sLFD_A_Line_NbPaxPerCar};
        internal static Object[] ListeDefaultFPA_LF_1_52 = { 90.0, 10.0, 90.0, 50.0, 50.0, 90.0, 10.0, 50.0, 0.0, 0.0, 0.0,1.0 };
        #endregion
        #region Old
        internal static string[] ListeLinksLignesFPA_LF_1_52 ={ sLFD_A_Line_Full, 
            sLFD_A_Line_C, 
            sLFD_A_Line_Y, 
            sLFD_A_Line_Local, 
            sLFD_A_Line_NotLocal, 
            sLFA_Line_Terminating, 
            sLFD_A_Line_Transferring, 
            sLFD_A_Line_ReCheck, 
            sLFD_A_Line_TransferDesk,
            sLFD_A_Line_OOGTransf,
            sLFA_Line_OOGTerm,
            ""};
        #endregion
        #endregion

        // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
        #region Process Scheduel
        
        #region old Process Schedule
        public static String[] listeEnteteProcessScheduleTable_1_52 
                                = new String[]{ sColumnBegin,
                                                sColumnEnd,
                                                sTableProcessScheduleItinerary,
                                                sTableProcessScheduleProcess,
                                                sTableProcessScheduleGroupQueues,
                                                sTableProcessScheduleProcessQueues};

        public static Type[] listeTypeEnteteProcessScheduleTable_1_52
                                = new Type[]{ System.Type.GetType("System.DateTime"),
                                              System.Type.GetType("System.DateTime"),
                                              System.Type.GetType("System.String"),
                                              System.Type.GetType("System.String"),
                                              System.Type.GetType("System.String"),
                                              System.Type.GetType("System.String") };

        public static int[] listePrimaryKeyProcessScheduleTable_1_52 = new int[] { 0 };
        #endregion

        #region new ProcessSchedule
        public static String[] listeEntete_LinksProcessScheduleTable_1_53
                                = new String[]{ sColumnBegin,
                                                sColumnEnd,
                                                sTableProcessScheduleItinerary,
                                                sTableProcessScheduleProcess,
                                                sTableProcessScheduleGroupQueues,
                                                sTableProcessScheduleProcessQueues,
                                                ""};

        public static String[] ListeEntete_NewProcessScheduleTable_1_53 
                                    = { sColumnBegin,
                                        sColumnEnd,
                                        sTableProcessScheduleItinerary,
                                        sTableProcessScheduleProcess,
                                        sTableProcessScheduleGroupQueues,
                                        sTableProcessScheduleProcessQueues,
                                        sTableProcessScheduleProcessCapacity};

        public static Type[] ListeTypeEntete_NewProcessScheduleTable_1_53
                                = { System.Type.GetType("System.DateTime"),
                                    System.Type.GetType("System.DateTime"),
                                    System.Type.GetType("System.String"),
                                    System.Type.GetType("System.String"),
                                    System.Type.GetType("System.String"),
                                    System.Type.GetType("System.String"),
                                   System.Type.GetType("System.String")};

        public static int[] ListePrimaryKey_NewProcessScheduleTable_1_53 = { 0 };

        public static Object[] ListeDefault_NewProcessScheduleTable_1_53 = {null, null, GlobalNames.ItineraryTableName,
                    GlobalNames.Times_ProcessTableName, GlobalNames.Group_QueuesName, GlobalNames.Capa_QueuesTableName,
                    GlobalNames.capaProcessTableName};
        #endregion

        #endregion                
        // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)        
                
        #endregion

        #region version 1.53
        #region Pax Process Times
        #region old Pax Process Times
        public static String[] listeEntetePaxProcessTimesTable_1_53
                                = new String[]{ sProcessTable_Items,
                                                sProcessTable_Distrib_1,
                                                sProcessTable_Param1_1,
                                                sProcessTable_Param2_1,
                                                sProcessTable_Param3_1,
                                                sProcessTable_Distrib_2,
                                                sProcessTable_Param1_2,
                                                sProcessTable_Param2_2,
                                                sProcessTable_Param3_2};

        public static Type[] listeTypeEntetePaxProcessTimesTable_1_53
                                = new Type[]{ System.Type.GetType("System.String"),
                                              System.Type.GetType("System.String"),
                                              System.Type.GetType("System.Double"),
                                              System.Type.GetType("System.Double"),
                                              System.Type.GetType("System.Double"),
                                              System.Type.GetType("System.String"),
                                              System.Type.GetType("System.Double"),
                                              System.Type.GetType("System.Double"),
                                              System.Type.GetType("System.Double")};

        public static int[] listePrimaryKeyPaxProcessTimesTable_1_53 = new int[] { 0 };
        #endregion

        #region new Pax Process Times
        public static String[] listeEntete_LinksPaxProcessTimesTable_1_54
                                = new String[]{ sProcessTable_Items,
                                                sProcessTable_Distrib_1,
                                                sProcessTable_Param1_1,
                                                sProcessTable_Param2_1,
                                                sProcessTable_Param3_1,
                                                sProcessTable_Distrib_2,
                                                sProcessTable_Param1_2,
                                                sProcessTable_Param2_2,
                                                sProcessTable_Param3_2,
                                                "",
                                                "",
                                                "",
                                                "",
                                                ""};

        public static String[] ListeEntete_NewPaxProcessTimesTable_1_54
                                            = { sProcessTable_Items,
                                                sProcessTable_Distrib_1,
                                                sProcessTable_Param1_1,
                                                sProcessTable_Param2_1,
                                                sProcessTable_Param3_1,
                                                sProcessTable_Distrib_2,
                                                sProcessTable_Param1_2,
                                                sProcessTable_Param2_2,
                                                sProcessTable_Param3_2,
                                                sProcessTable_WaitingTimeReference,
                                                sProcessTable_Distrib_3,
                                                sProcessTable_Param1_3,
                                                sProcessTable_Param2_3,
                                                sProcessTable_Param3_3};

        public static Type[] ListeTypeEntete_NewPaxProcessTimesTable_1_54
                                        = { System.Type.GetType("System.String"),
                                            System.Type.GetType("System.String"),
                                            System.Type.GetType("System.Double"),
                                            System.Type.GetType("System.Double"),
                                            System.Type.GetType("System.Double"),
                                            System.Type.GetType("System.String"),
                                            System.Type.GetType("System.Double"),
                                            System.Type.GetType("System.Double"),
                                            System.Type.GetType("System.Double"),
                                            System.Type.GetType("System.String"),
                                            System.Type.GetType("System.String"),
                                            System.Type.GetType("System.Double"),
                                            System.Type.GetType("System.Double"),
                                            System.Type.GetType("System.Double")};

        public static int[] ListePrimaryKey_NewPaxProcessTimesTable_1_54 = { 0 };

        public static Object[] ListeDefault_NewPaxProcessTimesTable_1_54 = {"", "Constant", 0, 0, 0, "", 0, 0, 0,
                                                                           "", "Constant", 0, 0, 0};

        #endregion
        #endregion
        #endregion

        #region version 1.54
        // >> Bug #13367 Liege allocation
        internal static string[] ListeOldLignesOCT_CITable_1_53 = { 
                                                                            sOCT_CI_Line_Opening ,
                                                                            sOCT_CI_Line_Closing, 
                                                                            sOCT_CI_Line_Allocated
                                                                        
                                                                        };
        internal static string[] ListeLignesOCT_CITable_1_54 = { 
                                                                            sOCT_CI_Line_Opening ,
                                                                            sOCT_CI_Line_Closing ,
                                                                            sOCT_CI_Line_PartialOpeningTime,
                                                                            sOCT_CI_Line_Allocated,
                                                                            sOCT_CI_Line_NbStationsOpenedAtPartial,
                                                                            sOCT_CI_Line_NbAdditionalStationsForOverlappingFlights
                                                                        };

        internal static string[] ListeLinksOCT_CITable_1_54 = {
                                                                            sOCT_CI_Line_Opening ,
                                                                            sOCT_CI_Line_Closing ,
                                                                            "",
                                                                            sOCT_CI_Line_Allocated,
                                                                            "",
                                                                            ""
                                                                        };
        internal static Object[] Default_OCT_CITable_1_54 = { 120.0, 30.0, 120, 1, 1, -1 };
        // << Bug #13367 Liege allocation
        #endregion

        /*
        // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
        #region version 1.54
        // old FPD v 1.26
        #region new FPD
        internal static String[] listeEntete_LinksFPDTable_1_54 = new String[] { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                         sFPD_Column_TerminalCI,
                                                         sFPD_Column_Eco_CI_Start,
                                                         sFPD_Column_Eco_CI_End,
                                                         sFPD_Column_FB_CI_Start,
                                                         sFPD_Column_FB_CI_End,
                                                         sFPD_Column_Eco_Drop_Start,
                                                         sFPD_Column_Eco_Drop_End,
                                                         sFPD_Column_FB_Drop_Start,
                                                         sFPD_Column_FB_Drop_End,
                                                         sFPD_A_Column_TerminalGate,
                                                         sFPD_Column_BoardingGate,
                                                         sFPD_Column_TerminalMup,
                                                         sFPD_Column_Mup_Start,
                                                         sFPD_Column_Mup_End,
                                                         sFPD_Column_First_Mup_Start,
                                                        sFPD_Column_First_Mup_End,
                                                         sFPD_A_Column_TerminalParking,
                                                         sFPD_A_Column_Parking,
                                                         "",
                                                        sFPD_A_Column_RunWay,
                                                        sFPD_A_Column_User1,
                                                        sFPD_A_Column_User2,
                                                        sFPD_A_Column_User3,
                                                        sFPD_A_Column_User4,
                                                        sFPD_A_Column_User5};

        internal static String[] ListeEntete_New_FPDTable_1_54 = { sFPD_A_Column_ID, 
                                                        sFPD_A_Column_DATE, 
                                                        sFPD_Column_STD, 
                                                        sFPD_A_Column_AirlineCode, 
                                                        sFPD_A_Column_FlightN, 
                                                        sFPD_A_Column_AirportCode, 
                                                        sFPD_A_Column_FlightCategory, 
                                                        sFPD_A_Column_AircraftType, 
                                                        sFPD_Column_TSA,
                                                        sFPD_A_Column_NbSeats,
                                                         sFPD_Column_TerminalCI,
                                                         sFPD_Column_Eco_CI_Start,
                                                         sFPD_Column_Eco_CI_End,
                                                         sFPD_Column_FB_CI_Start,
                                                         sFPD_Column_FB_CI_End,
                                                         sFPD_Column_Eco_Drop_Start,
                                                         sFPD_Column_Eco_Drop_End,
                                                         sFPD_Column_FB_Drop_Start,
                                                         sFPD_Column_FB_Drop_End,
                                                         sFPD_A_Column_TerminalGate,
                                                         sFPD_Column_BoardingGate,
                                                         sFPD_Column_TerminalMup,
                                                        sFPD_Column_Eco_Mup_Start,
                                                        sFPD_Column_Eco_Mup_End,
                                                        sFPD_Column_First_Mup_Start,
                                                        sFPD_Column_First_Mup_End,
                                                         sFPD_A_Column_TerminalParking,
                                                         sFPD_A_Column_Parking,
                                                         sFPD_A_Column_Parking + "2",
                                                        sFPD_A_Column_RunWay,
                                                        sFPD_A_Column_User1,
                                                        sFPD_A_Column_User2,
                                                        sFPD_A_Column_User3,
                                                        sFPD_A_Column_User4,
                                                        sFPD_A_Column_User5};

        internal static Type[] ListeTypeEntete_New_FPDTable_1_54 = { System.Type.GetType("System.Int32"), 
                                                            System.Type.GetType("System.DateTime"), 
                                                            System.Type.GetType("System.TimeSpan"), 
                                                            typeof(String), 
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            typeof(String) ,
                                                            System.Type.GetType("System.Boolean"), 
                                                            System.Type.GetType("System.Int32"), 
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String),
                                                            typeof(String)};
        internal static int[] ListePrimaryKey_New_FPDTable_1_54 = { 0 };
        internal static Object[] ListeDefault_New_FPDTable_1_54 = { null, null, null, "", "", "", "", "", false, 
                                                                      0, "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                                                                      "0", "0", "", "", "", "", "", "", "", "", "" };

        #endregion
        #endregion        
        // << Task #11326 Pax2Sim - Allocation - add Parking allocation option
*/
        #region Current Version
        /// <summary>
        /// The current version for the project. This should be corelated with the third parameter from
        /// the Assembly Information(Project -> Properties)
        /// </summary>
        internal const String currentProjectVersion = "1.55";   // >> Task #15867 Transfer Distribution Tables/Charts improvement
//        internal const String currentProjectVersion = "1.54";
        //internal const String currentProjectVersion = "1.55";// >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
        #endregion
    }
}
    