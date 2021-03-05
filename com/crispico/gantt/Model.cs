using System;
using System.Collections.Generic;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.gantt
{
    class Model
    {
        public const String fbExceptionIndicator = "FB";
        public const int DOWN = 0;
        public const int NORMAL = 1;
        public const int UP = 2;
        //Flight categories
        public const String EXTRASHENGEN = "ES";
        public static String EXTRASHENGEN_COLOR = "" + (OverallTools.FonctionUtiles.getColor(1).ToArgb()); //green-yellow
        public const String INTRASHENGEN = "IS";
        public static String INTRASHENGEN_COLOR = "" + (OverallTools.FonctionUtiles.getColor(2).ToArgb());//yellow
        public const String DOMESTIC = "DOM";
        public static String DOMESTIC_COLOR = "" + (OverallTools.FonctionUtiles.getColor(3).ToArgb());  //orange 

        public const String INT = "INT";
        public static String INT_COLOR = "" + (OverallTools.FonctionUtiles.getColor(1).ToArgb());  //orange 

        public const String FR = "FR";
        public static String FR_COLOR = "" + (OverallTools.FonctionUtiles.getColor(1).ToArgb()); //green-yellow
        public const String JAF = "JAF";
        public static String JAF_COLOR = "" + (OverallTools.FonctionUtiles.getColor(2).ToArgb());//yellow
        public const String W6 = "W6";
        public static String W6_COLOR = "" + (OverallTools.FonctionUtiles.getColor(3).ToArgb());  //orange 
        public const String JFU = "JFU";
        public static String JFU_COLOR = "" + (OverallTools.FonctionUtiles.getColor(4).ToArgb());  
        public const String FIN = "FIN";
        public static String FIN_COLOR = "" + (OverallTools.FonctionUtiles.getColor(5).ToArgb());
        //Class type
        public const String ECO = "Eco ";
        public const String FB = "FB ";
        public const String EcoFB = "Eco FB";
        //Gantt type
        public const String RESOURCESGANTT = "ResourceGantt";
        public const String RESOURCESGANTTFILE = "ResourcesGanttFile.swf";
        public const String FLIGHTGANTT = "FlightGantt";
        public const String FLIGHTGANTTFILE = "FlightGanttFile.swf";
        public const String RESOURCE_GANTT_FOR_ALLOCATION = "ResourceGanttForAllocation";
        public const String RESOURCE_GANTT_FOR_ALLOCATION_FILE = "ResourceGanttForAllocationFile.swf";
        //Gantt Title
        public const String DEFAULT_FPA_TITLE = "Default Arrival Flight Plans";
        public const String DEFAULT_FPD_TITLE = "Default Departure Flight Plans";
        public const String BAGGAGE_CLAIM_TITLE = "Baggage Claim Gantt";
        public const String TRANSFER_INFEED_TITLE = "Transfer Infeed Gantt";
        public const String ARRIVAL_INFEED_TITLE = "Arrival Infeed Gantt";
        public const String ARRIVAL_GATE_TITLE = "Arrival Gate Gantt";
        public const String CHECKIN_TITLE = "Check-In Gantt";
        public const String BAGGAGE_DROP_TITLE = "Baggage Drop Gantt";
        public const String MAKEUP_TITLE = "Make-Up Gantt";
        public const String BOARDING_GATE_TITLE = "Boarding Gate Gantt";
        public const String PARKING_TITLE = "Parking Gantt";
        public const String PARKING_ARRIVAL_STATIC_ALLOC_TITLE = "Arrival Parking Gantt";
        public const String PARKING_DEPARTURE_STATIC_ALLOC_TITLE = "Departure Parking Gantt";
        public const String RUNWAY_TITLE = "Runway Gantt";
        public const String FLIGHT_PLAN_INFO_TITLE = "Flight Plan Information Gantt";
        
        // global resource types: range/non-range
        public const String RANGED_RESOURCES = "ranged";
        public const String NON_RANGED_RESOURCES = "nonRanged";

        // flight color criteria - by flight category or by airline code
        public const String COLOR_CRITERIA_FLIGHT_CATEGORY = "colorByFlightCategory";
        public const String COLOR_CRITERIA_AIRLINE_CODE = "colorByAirlineCode";
        public const String COLOR_CRITERIA_GROUND_HANDLER_CODE = "colorByGroundHandlerCode";

        // Standart Nevron Toolbar: Context Description for functionality
        // used to suppress commands from toolbar
        public const String SHOW_CHART_EDITOR = "Context: Show Chart Editor";
        public const String PAGE_SETUP = "Context: Page Setup";
        public const String SHOW_CHART_WIZARD = "Context: Show Chart Wizard";
        public const String APPLY_STYLE_SHEET = "Context: Apply Style Sheet";

        //image index for Gantt Note attached to a Table
        public const int GANTT_NOTE_IMAGE_INDEX = 70;
        //Gantt params for exporting as image
        public const int IMAGE_TYPE_PNG = 1;
        public const int IMAGE_TYPE_JPG = 0;
        public const String IMAGE_TYPE_PNG_EXTENTION = ".png";
        public const String IMAGE_TYPE_JPG_EXTENTION = ".jpg";
        // Gantt prefix for the Gantt Paragraph Node
        // << Task #6386 Itinerary process        
        //public const String GANTT_PREFIX = "Gantt";
        public const String GANTT_PREFIX = "Image";
        // >> Task #6386 Itinerary process

        //resource type names according to Dynamic Allocation for Flight Plan Information table
        public const String MAKE_UP_RESOURCE_TYPE_NAME = "Make-Up";
        public const String CHECK_IN_RESOURCE_TYPE_NAME = "Check-In";
        public const String BOARDING_GATE_RESOURCE_TYPE_NAME = "Boarding-Gate";
        public const String BAGG_DROP_RESOURCE_TYPE_NAME = "Bag-Drop";
        public const String TRANSFER_INFEED_RESOURCE_TYPE_NAME = "Transfer-Infeed";
        public const String BAGGAGE_CLAIM_RESOURCE_TYPE_NAME = "Baggage-Claim";

        // Group types        
        public const String MAKE_UP_GROUP = "Make-Up Group";
        public const String ARRIVAL_INFEED_GROUP = "Arrival Infeed Group";
        public const String ARRIVAL_GATE_GROUP = "Arrival Gate Group";
        public const String BAGG_CLAIM_GROUP = "Baggage Claim Group";
        public const String TRANSFER_INFEED_GROUP = "Transfer Infeed Group";
        public const String BOARDING_GATE_GROUP = "Boarding Gate Group";

        //Itinerary Group Names
        public const String ARRIVAL_GATE_GROUP_NAME = "Arrival Gate";
        public const String BOARDING_GATE_GROUP_NAME = "Boarding Gate";
        public const String BAGGAGE_CLAIM_GROUP_NAME = "Baggage Claim";
        public const String CHECK_IN_GROUP_NAME = "Check In";

        public const String PASSPORT_CHECK_GROUP_NAME = "Passport Check";
        public const String SECURITY_CHECK_GROUP_NAME = "Security Check";
        public const String TRANSFER_GROUP_NAME = "Transfer";
        public const String USER_PROCESS_GROUP_NAME = "User Process";

        // << Task #7868 Scenario - Utilization chart default columns used
        public static string[] UTILIZATION_CHART_COLUMNS = { "Start", GlobalNames.GROUP_UTILIZATION_AVERAGE_COLUMN_NAME, 
                                                               GlobalNames.GROUP_UTILIZATION_DESK_NEED_COLUMN_NAME };// >> Task #18306 PAX2SIM - BHS - Sorter occupation
        // >> Task #7868 Scenario - Utilization chart default columns used
        
        // >> Task #18306 PAX2SIM - BHS - Sorter occupation
        public static string[] BHS_UTILIZATION_FOR_STATIONS_CHART_COLUMNS = { "Start", GlobalNames.BHS_UTILIZATION_PERCENT_COLUMN_NAME };
        public static string[] BHS_UTILIZATION_FOR_GROUPS_CHART_COLUMNS = { "Start", GlobalNames.BHS_UTILIZATION_AVERAGE_COLUMN_NAME, 
                                                               GlobalNames.BHS_UTILIZATION_STATION_NEED_COLUMN_NAME };
        // << Task #18306 PAX2SIM - BHS - Sorter occupation
        public enum ResourceTypes
        {
            //arrival flight resources
            ArrivalInfeed = 1,//"Arrival Infeed",
            TransferInfeed = 2,//"Transfer Infeed",
            BaggageClaim = 3,//"Baggage Claim",
            ArrivalGate = 4,//"Arrival Gate",
            //departure flight resources
            EcoCheck_In = 5,//"Eco. Check-In",
            FBCheck_In = 6,//"F&B Check-In",
            EcoBagDrop = 7,//"Eco. Baggage Drop",
            FBBagDrop = 8,//"F&B Baggage Drop",
            EcoMakeUp = 9,//"Eco. MakeUp",
            FBMakeUp = 10,//"F&B MakeUp",
            BoardingGate = 11,//"Boarding Gate",
            //arrival and departure flight resources            
            ParkingStand = 12,//"Parking Stand",
            Runway = 13,//"Runway"
        };

        // << Task #6386 Itinerary process        
        // swf file for Process Flow (the new itinerary)
        public const String PROCESS_FLOW_FILE = "ProcessFlow.swf";
        // itinerary data types
        public const String GROUP_OBJECT_TYPE = "Group";
        public const String SCENARIO_OBJECT_TYPE = "Scenario";
        public const String CHART_OBJECT_TYPE = "Chart";
        // itinerary group object standard dimensions and coordinates
        public const int STANDARD_WIDTH = 59;
        public const int STANDARD_HEIGHT = 47;
        public const int INITIAL_X_AXIS_VALUE = 20;
        public const int INITIAL_Y_AXIS_VALUE = 20;
        
        //Type of information added to the Group - Process Times / Capacity
        public const String PAX_PROCESS_TIME_DATA = "PaxProcessTime";
        public const String BAG_PROCESS_TIME_DATA = "BagProcessTime";
        public const String CAPACITY_DATA = "Capacity";
        // << Task #8789 Pax2Sim - ProcessFlow - update Group details
        public const String DELAY_TIME_DISTRIBUTION_DATA = "DelayTimeDistribution";
        // >> Task #8789 Pax2Sim - ProcessFlow - update Group details
        
        //Background Image File Name (name of the file the image will be stored on the disk)
        public const String BACKGROUND_IMAGE_FILE_NAME = "BackgroundImage";
        public const String BACKGROUND_IMAGE_DIRECTORY_NAME = "BackgroundImage";

        //ItineraryGroups text file name Prefix and Extention 
        // => file name = itineraryGroupsTextFilePrefix + selected table/filter name + itineraryGroupsTextFileExtention
        public const String itineraryGroupsTextFilePrefix = "ItineraryGroups_";
        public const String itineraryGroupsTextFileExtention = ".txt";
        public const String itineraryConnectionsBendPointsTextFilePrefix = "ItineraryConnectionsBendpoints_";
        public const String itineraryConnectionsBendPointsFileExtention = ".txt";
        // >> Task #6386 Itinerary process

        // << Task #8789 Pax2Sim - ProcessFlow - update Group details        
        public const String itineraryParamsTextFilePrefix = "ItineraryParameters_";
        public const String itineraryParamsTextFileExtension = ".txt";
        // >> Task #8789 Pax2Sim - ProcessFlow - update Group details

        // << Task #7911 Scenario - Utilization chart default columns used II
        public const String chartLineVisualisationType = "Line";
        public const String chartAxeRepresentationY = "Y";
        public const String chartAxisNameNbOfStations = "Nb. Stations";
        // >> Task #7911 Scenario - Utilization chart default columns used II

        // << Task #8302 Pax2Sim - Scenario Parameters - Intermediate distribution levels
        public const String distributionLevelsTextFilePrefix = "distributionLevels_";
        public const String distributionLevelsTextFileExtention = ".txt";
        // >> Task #8302 Pax2Sim - Scenario Parameters - Intermediate distribution levels

        // << Task #8320 Pax2Sim - Scenario - reload at right click menu action
        public const String paxTraceFilename = "PaxTrace.txt";
        public const String bagTraceFilename = "BagTrace.txt";
        // >> Task #8320 Pax2Sim - Scenario - reload at right click menu action
    }
}
