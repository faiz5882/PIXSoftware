using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SIMCORE_TOOL.Prompt.CDG
{
    class Converter
    {
        public static Dictionary<string, List<BaggageTriage>> getBaggageTriageDictionary(DataTable sourceTable)
        {
            Dictionary<string, List<BaggageTriage>> baggageTriageDictionary = new Dictionary<string, List<BaggageTriage>>();
            if (sourceTable != null && sourceTable.Columns.Count == 5)
            {
                int rowNb = 1;

                int timeColumnIndex = 0;
                int flightNbColumnIndex = 1;
                int totalBagsNbColumnIndex = 2;
                int directNbBagsColumnIndex = 3;
                int ebsNbBagsColumnIndex = 4;

                foreach (DataRow row in sourceTable.Rows)
                {
                    string flightNb = "";
                    if (row[flightNbColumnIndex] != null)
                    {
                        flightNb = row[flightNbColumnIndex].ToString();

                        int startMinute = -1;
                        if (row[timeColumnIndex] != null)
                            Int32.TryParse(row[timeColumnIndex].ToString(), out startMinute);

                        int totalNbBags = 0;
                        if (row[totalBagsNbColumnIndex] != null)
                            Int32.TryParse(row[totalBagsNbColumnIndex].ToString(), out totalNbBags);
                        int directNbBags = 0;
                        if (row[directNbBagsColumnIndex] != null)
                            Int32.TryParse(row[directNbBagsColumnIndex].ToString(), out directNbBags);
                        int ebsNbBags = 0;
                        if (row[ebsNbBagsColumnIndex] != null)
                            Int32.TryParse(row[ebsNbBagsColumnIndex].ToString(), out ebsNbBags);

                        if (startMinute != -1)
                        {
                            BaggageTriage bagTriage = new BaggageTriage(totalNbBags, directNbBags, ebsNbBags, startMinute, 10);
                            if (baggageTriageDictionary.ContainsKey(flightNb))
                            {

                                List<BaggageTriage> baggageTriageList = baggageTriageDictionary[flightNb];
                                baggageTriageList.Add(bagTriage);
                            }
                            else
                            {
                                List<BaggageTriage> baggageTriageList = new List<BaggageTriage>();
                                baggageTriageList.Add(bagTriage);
                                baggageTriageDictionary.Add(flightNb, baggageTriageList);
                            }
                        }
                        else
                            OverallTools.ExternFunctions.PrintLogFile("CDG Converter error on row nb " + rowNb + " : Start minute could not be determined.");
                    }
                    else
                    {
                        
                        OverallTools.ExternFunctions.PrintLogFile("CDG Converter error on row nb " + rowNb + " : Flight Number could not be determined.");
                    }
                    rowNb++;
                }
            }
            return baggageTriageDictionary;
        }

        public static List<CDGFlight> getFlights(DataTable fp)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, now.Day);
            
            List<CDGFlight> flights = new List<CDGFlight>();
            string s3Sufix = "_S3";
            string s4Sufix = "_S4";
            int flightNbColumnIndex = 0;
            int etdColumnIndex = 2;
            int realFlightCategoryColumnIndex = 3;
            int user1_columnJ_columnIndex = 8;
            int id = 1;

            
            foreach (DataRow row in fp.Rows)
            {
                string flightNb = "";
                int etd = -1;
                flightNb = row[flightNbColumnIndex].ToString();
                Int32.TryParse(row[etdColumnIndex].ToString(), out etd);
                string realFlightCategory = row[realFlightCategoryColumnIndex].ToString();
                string user1ColumnJ = row[user1_columnJ_columnIndex].ToString();                

                CDGFlight flight = null;
                if (flightNb.StartsWith("AF4"))
                    flight = new CDGFlight(id, flightNb, etd, s4Sufix, date, realFlightCategory, user1ColumnJ);
                else
                    flight = new CDGFlight(id, flightNb, etd, s3Sufix, date, realFlightCategory, user1ColumnJ);
                flights.Add(flight);
                id++;
            }
            return flights;
        }

        public static DataTable generateDepartureFlightPlanWithImportedData(DataTable currentDepartureFlightPlan,
            List<CDGFlight> departureFlights, Dictionary<string, List<BaggageTriage>> s3InterfaceDictionary,
            Dictionary<string, List<BaggageTriage>> s4ScenarioDictionary)
        {
            DataTable flightPlanWithImportedData = null;
            if (currentDepartureFlightPlan != null && departureFlights.Count > 0)
            {
                flightPlanWithImportedData = currentDepartureFlightPlan.Clone();

                int rowNb = 1;
                foreach (CDGFlight flight in departureFlights)
                {
                    List<BaggageTriage> bagTriList = null;
                    if (s3InterfaceDictionary.ContainsKey(flight.flightNb))
                        bagTriList = s3InterfaceDictionary[flight.flightNb];
                    else if (s4ScenarioDictionary.ContainsKey(flight.flightNb))
                        bagTriList = s4ScenarioDictionary[flight.flightNb];
                    int nbEbsBags = 0;
                    if (bagTriList != null)
                    {
                        foreach (BaggageTriage bt in bagTriList)
                        {
                            if (bt.nbEBSBags > 0)                            
                                nbEbsBags += bt.nbEBSBags;                            
                        }
                    }

                    DataRow newFPRow = flightPlanWithImportedData.NewRow();
                    
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_ID] = flight.id;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_DATE] = flight.flightDate.Date;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_STD) != -1)
                        newFPRow[GlobalNames.sFPD_Column_STD] = flight.flightDate.TimeOfDay;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AirlineCode] = "";
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_FlightN] = flight.flightNbWithSuffix;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AirportCode] = "";
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_FlightCategory] = flight.realFlightCategory; //flight.flightNbWithSuffix;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AircraftType] = "100";
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TSA) != -1)
                        newFPRow[GlobalNames.sFPD_Column_TSA] = false;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_NbSeats] = 0;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI) != -1)
                        newFPRow[GlobalNames.sFPD_Column_TerminalCI] = flight.terminal;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_CI_Start] = 1;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_CI_End] = 1;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_FB_CI_Start] = 1;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_FB_CI_End] = 1;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_Drop_Start] = 0;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_Drop_End] = 0;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_FB_Drop_Start] = 0;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_FB_Drop_End] = 0;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_TerminalGate] = 1;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate) != -1)
                        newFPRow[GlobalNames.sFPD_Column_BoardingGate] = 1;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup) != -1)
                        newFPRow[GlobalNames.sFPD_Column_TerminalMup] = flight.terminal;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_Mup_Start] = 1;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_Mup_End] = 1;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_First_Mup_Start] = 1;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_First_Mup_End] = 1;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_TerminalParking] = 1;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_Parking] = 1;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_RunWay] = 1;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1) != -1)                    
                        newFPRow[GlobalNames.sFPD_A_Column_User1] = flight.user1_columnJ;                    
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_User2] = flight.user2_NumberInSimulation;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_User3] = nbEbsBags;
                    flightPlanWithImportedData.Rows.Add(newFPRow);
                    rowNb++;
                }
                flightPlanWithImportedData.AcceptChanges();
            }
            return flightPlanWithImportedData;
        }


        internal static DataTable getOCT(List<CDGFlight> flights, 
            Dictionary<string, List<BaggageTriage>> s3InterfaceDictionary,
            Dictionary<string, List<BaggageTriage>> s4ScenarioDictionary, 
            DataTable mupOCT)
        {
            foreach (CDGFlight flight in flights)
            {
                List<BaggageTriage> bagTriList = null;
                if (s3InterfaceDictionary.ContainsKey(flight.flightNb))
                    bagTriList = s3InterfaceDictionary[flight.flightNb];
                else if (s4ScenarioDictionary.ContainsKey(flight.flightNb))
                    bagTriList = s4ScenarioDictionary[flight.flightNb];
                if (bagTriList != null)
                {
                    int minTime = Int32.MaxValue;
                    int maxTime = Int32.MinValue;
                    foreach (BaggageTriage bt in bagTriList)
                    {
                        if (bt.nbEBSBags > 0)
                        {
                            if (bt.startMinute < minTime)
                                minTime = bt.startMinute;
                            if (bt.endMinute > maxTime)
                                maxTime = bt.endMinute;
                        }
                    }
                    if (minTime != Int32.MaxValue && maxTime != Int32.MinValue)
                    {
                        if (flight.realFlightCategory == "MC"
                            || flight.realFlightCategory == "RG")
                        {
                            maxTime += 30;
                        }
                        else if (flight.realFlightCategory == "LC")
                        {
                            maxTime += 60;
                        }
                        if (maxTime > flight.etd)
                            maxTime = flight.etd;

                        int octOpening = flight.etd - minTime;
                        int octClosing = flight.etd - maxTime;

                        string colName = "D_" + flight.id;
                        DataColumn col = new DataColumn(colName, typeof(double));
                        mupOCT.Columns.Add(col);
                        int latColNb = mupOCT.Columns.Count - 1;
                        mupOCT.Rows[0][latColNb] = octOpening;
                        mupOCT.Rows[1][latColNb] = octClosing;
                        mupOCT.Rows[4][latColNb] = octOpening;

                        mupOCT.Rows[2][latColNb] = 90;
                        mupOCT.Rows[3][latColNb] = 1;
                        mupOCT.Rows[5][latColNb] = 1;
                        mupOCT.Rows[6][latColNb] = 3;
                        mupOCT.Rows[7][latColNb] = 40;
                        mupOCT.Rows[8][latColNb] = 60;
                        mupOCT.Rows[9][latColNb] = 3;
                        mupOCT.Rows[10][latColNb] = 15;
                    }
                }
            }
            mupOCT.AcceptChanges();
            return mupOCT;
        }
    }
}
