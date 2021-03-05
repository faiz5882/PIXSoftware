using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Collections;
using System.Drawing;
using SIMCORE_TOOL.com.crispico.gantt;
using SIMCORE_TOOL.DataManagement;
using SIMCORE_TOOL.com.crispico.FlightPlansUtils;

namespace SIMCORE_TOOL.Classes
{
    public class GenerateAllocationTool
    {
        internal enum TypeAllocation
        {
            MakeUpAllocation,
            CheckInAllocation,
            ReclaimAllocation,
            TransferInfeedAllocation,
            BoardingGateAllocation,
            ArrivalGateAllocation,
            Parking // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
        };
        
        internal bool isLiegeAllocation;    // >> Bug #13367 Liege allocation
        
        // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
        internal List<TypeAllocation> departureAllocations = 
            new List<TypeAllocation> (new TypeAllocation[] { TypeAllocation.MakeUpAllocation, TypeAllocation.CheckInAllocation, 
                                                                TypeAllocation.BoardingGateAllocation, TypeAllocation.Parking });
        // << Task #11326 Pax2Sim - Allocation - add Parking allocation option



        private class SegregationAllocationInformation
        {
            #region Variables
            private String sStatisticName;

            private int iNumberExpectedFlights;
            private int iNumberAllocatedFlights;

            private int iMinimumNumberOfFlightPerSlot;
            private int iMaximumNumberOfFlightPerSlot;
            private int iAverageNumberOfFlightPerSlot;
            private int iNumberOfSlots;

            private int iMinimumNumberOfFlightPerSlotPerDesk;
            private int iMaximumNumberOfFlightPerSlotPerDesk;
            private int iAverageNumberOfFlightPerSlotPerDesk;

            private int iMinimumNumberOfNeededDesks;
            private int iMaximumNumberOfNeededDesks;
            private int iAverageNumberOfNeededDesks;


            private int iMinimumNumberOfOccupiedDesks;
            private int iMaximumNumberOfOccupiedDesks;
            private int iAverageNumberOfOccupiedDesks;
            private int iNumberOfDesks;
            #endregion

            #region Constructors
            internal SegregationAllocationInformation(String StatisticName, int NumberExpectedFlights,int NumberAllocatedFlights, int NumberOfDesks)
            {
                sStatisticName = StatisticName;
                iNumberExpectedFlights=NumberExpectedFlights;
                iNumberAllocatedFlights = NumberAllocatedFlights;
                ResetStatistics(NumberOfDesks);
            }
            #endregion

            #region Functions to add Statistics
            internal void AddStatsPerSlot(int iNumberOfFlights, List<int> liDesksOccupation, int iLastIndexOccupied)
            {
                #region For NumberOfFlightPerSlot
                if ((iMinimumNumberOfFlightPerSlot == -1) || (iNumberOfFlights < iMinimumNumberOfFlightPerSlot))
                    iMinimumNumberOfFlightPerSlot = iNumberOfFlights;
                if (iNumberOfFlights > iMaximumNumberOfFlightPerSlot)
                    iMaximumNumberOfFlightPerSlot = iNumberOfFlights;
                iAverageNumberOfFlightPerSlot += iNumberOfFlights;
                #endregion

                if ((liDesksOccupation != null) && (liDesksOccupation.Count > 0))
                {
                    #region for MinimumNumberOfFlightPerSlotPerDesk
                    liDesksOccupation.Sort();
                    if ((iMinimumNumberOfFlightPerSlotPerDesk == -1) || (liDesksOccupation[0] < iMinimumNumberOfFlightPerSlotPerDesk))
                        iMinimumNumberOfFlightPerSlotPerDesk = liDesksOccupation[0];

                    if (liDesksOccupation[liDesksOccupation.Count - 1] > iMaximumNumberOfFlightPerSlotPerDesk)
                        iMaximumNumberOfFlightPerSlotPerDesk = liDesksOccupation[liDesksOccupation.Count - 1];

                    foreach (int iValue in liDesksOccupation)
                        iAverageNumberOfFlightPerSlotPerDesk += iValue;
                    #endregion

                    #region for NumberOfOccupiedDesks
                    if ((iMinimumNumberOfOccupiedDesks == -1) || (liDesksOccupation.Count < iMinimumNumberOfOccupiedDesks))
                        iMinimumNumberOfOccupiedDesks = liDesksOccupation.Count;
                    if (liDesksOccupation.Count > iMaximumNumberOfOccupiedDesks)
                        iMaximumNumberOfOccupiedDesks = liDesksOccupation.Count;
                    iAverageNumberOfOccupiedDesks += liDesksOccupation.Count;
                    #endregion
                }
                #region For NumberOfNeededDesks
                if ((iMinimumNumberOfNeededDesks == -1) || (iLastIndexOccupied < iMinimumNumberOfNeededDesks))
                    iMinimumNumberOfNeededDesks = iNumberOfFlights;
                if (iLastIndexOccupied > iMaximumNumberOfNeededDesks)
                    iMaximumNumberOfNeededDesks = iLastIndexOccupied;
                iAverageNumberOfNeededDesks += iLastIndexOccupied;
                #endregion

                iNumberOfSlots++;
            }
            internal void ResetStatistics( int NumberOfDesks)
            {
                iMinimumNumberOfFlightPerSlot = -1;
                iMaximumNumberOfFlightPerSlot = 0;
                iAverageNumberOfFlightPerSlot = 0;
                iNumberOfSlots = 0;

                iMinimumNumberOfFlightPerSlotPerDesk = -1;
                iMaximumNumberOfFlightPerSlotPerDesk = 0;
                iAverageNumberOfFlightPerSlotPerDesk = 0;

                iMinimumNumberOfNeededDesks = -1;
                iMaximumNumberOfNeededDesks = 0;
                iAverageNumberOfNeededDesks = 0;


                iMinimumNumberOfOccupiedDesks = -1;
                iMaximumNumberOfOccupiedDesks = 0;
                iAverageNumberOfOccupiedDesks = 0;

                iNumberOfDesks = NumberOfDesks;

            }
            internal void AddFligts(int NumberExpectedFlights, int NumberAllocatedFlights)
            {
                iNumberExpectedFlights += NumberExpectedFlights;
                iNumberAllocatedFlights += NumberAllocatedFlights;
            }
            #endregion

            #region Function to get the statistics
            internal Object[] getRow()
            {
                Object[] oTmp = new Object[15];
                if (sStatisticName.EndsWith("_"))
                    oTmp[0] = sStatisticName.Substring(0, sStatisticName.Length - 1);
                else
                    oTmp[0] = sStatisticName;
                oTmp[1] = iNumberExpectedFlights;
                oTmp[2] = iNumberAllocatedFlights;

                #region FlightPerSlot
                if (iMinimumNumberOfFlightPerSlot == -1)
                {
                    oTmp[3] = 0;
                    oTmp[4] = 0;
                    oTmp[5] = 0;
                }
                else
                {
                    oTmp[3] = iMinimumNumberOfFlightPerSlot;
                    if (iNumberOfSlots != 0)
                        oTmp[4] = Math.Round((Double)iAverageNumberOfFlightPerSlot / (Double)iNumberOfSlots, 2);
                    else
                        oTmp[4] = 0;
                    oTmp[5] = iMaximumNumberOfFlightPerSlot;
                }
                #endregion

                #region FlightPerSlotPerDesk
                if (iMinimumNumberOfFlightPerSlotPerDesk == -1)
                {
                    oTmp[6] = 0;
                    oTmp[7] = 0;
                    oTmp[8] = 0;
                }
                else
                {
                    oTmp[6] = iMinimumNumberOfFlightPerSlotPerDesk;
                    if ((iNumberOfSlots != 0) && (iNumberOfDesks !=0))
                        oTmp[7] = Math.Round((Double)iAverageNumberOfFlightPerSlotPerDesk / (Double)(iNumberOfSlots*iNumberOfDesks), 2);
                    else
                        oTmp[7] = 0;
                    oTmp[8] = iMaximumNumberOfFlightPerSlotPerDesk;
                }
                #endregion

                #region NumberOfNeededDesks
                if (iMinimumNumberOfNeededDesks == -1)
                {
                    oTmp[9] = 0;
                    oTmp[10] = 0;
                    oTmp[11] = 0;
                }
                else
                {
                    oTmp[9] = iMinimumNumberOfNeededDesks;
                    if (iNumberOfSlots != 0)
                        oTmp[10] = Math.Round((Double)iAverageNumberOfNeededDesks / (Double)iNumberOfSlots, 2);
                    else
                        oTmp[10] = 0;
                    oTmp[11] = iMaximumNumberOfNeededDesks;
                }
                #endregion


                #region NumberOfOccupiedDesks
                if (iMinimumNumberOfOccupiedDesks == -1)
                {
                    oTmp[12] = 0;
                    oTmp[13] = 0;
                    oTmp[14] = 0;
                }
                else
                {
                    oTmp[12] = iMinimumNumberOfOccupiedDesks;
                    if (iNumberOfSlots != 0)
                        oTmp[13] = Math.Round((Double)iAverageNumberOfOccupiedDesks / (Double)iNumberOfSlots, 2);
                    else
                        oTmp[13] = 0;
                    oTmp[14] = iMaximumNumberOfOccupiedDesks;
                }
                #endregion
                return oTmp;
            }
            #endregion

            internal static DataTable GetTable()
            {
                DataTable dtTable = new DataTable("Statistics");
                dtTable.Columns.Add("Segregation", typeof(String));

                dtTable.Columns.Add("Expected flights", typeof(int));
                dtTable.Columns.Add("Allocated flights", typeof(int));

                dtTable.Columns.Add("Min. Nb of Flights Per slot", typeof(int));
                dtTable.Columns.Add("Aver. Nb of Flights Per slot", typeof(double));
                dtTable.Columns.Add("Max. Nb of Flights Per slot", typeof(int));

                dtTable.Columns.Add("Min. Nb of Flights per allocated desk per slot", typeof(int));
                dtTable.Columns.Add("Aver. Nb of Flights per desk per slot", typeof(Double));
                dtTable.Columns.Add("Max. Nb of Flights per allocated desk per slot", typeof(int));

                dtTable.Columns.Add("Min. Nb of needed desks", typeof(int));
                dtTable.Columns.Add("Aver. Nb of needed desks", typeof(Double));
                dtTable.Columns.Add("Max. Nb of needed desks", typeof(int));

                dtTable.Columns.Add("Min. Nb of occupied desks", typeof(int));
                dtTable.Columns.Add("Aver. Nb of occupied desks", typeof(Double));
                dtTable.Columns.Add("Max. Nb of occupied desks", typeof(int));
                return dtTable;
            }
        }

        // Scenario Allocation FlightPlan Information table for gantt
        private class AllocationFlightPlanInformation
        {
            #region variables
            private int flightId;
            private DateTime date;
            private TimeSpan sta_d;
            private String airlineCode;
            private String flightNb;
            private String airportCode;
            private String flightCategory;
            private String aircraftType;
            private Boolean noBSM;
            private Boolean cbp;
            private Boolean tsa;
            private int nbSeats;

            // << Task #9465 Pax2Sim - Allocation - Add FPI Columns            
            private double totalNbPassengers;
            private double totalNbBags;
            private double containerSize;

            private String calculationType;
            private String octTableDescription;
            // >> Task #9465 Pax2Sim - Allocation - Add FPI Columns

            private int firstDesk;
            private int lastDesk;
            private int desksUsed;  // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column
            private DateTime openingTime;
            private DateTime closingTime;
            private int terminalNb;
            private String groundHandler;

            private String user1;
            private String user2;
            private String user3;
            private String user4;
            private String user5;

            private String allocationType;
            private String resourceTypeName;
            #endregion

            internal AllocationFlightPlanInformation(int pFlightId, DateTime pDate, TimeSpan pSta, String pAirlineCode, String pFlightNb,
                                                     String pAirportCode, String pFlightCategory, String pAircraftType, Boolean pNoBSM,
                                                     Boolean pCBP, Boolean pTSA, int pNbSeats,
                                                     double pContainerSize, double pTotalNbPassengers, double pTotalNbBags,    // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                     int pFirstDesk, int pLastDesk,
                                                     DateTime pOpeningTime, DateTime pClosingTime, int pTerminalNb, String pGroundHandler,
                                                     String pUser1, String pUser2, String pUser3, String pUser4, String pUser5, 
                                                     String pAllocationType, String pResourceTypeName,
                                                     String pCalculationType, String pOCTTableDescription)// << Task #9465 Pax2Sim - Allocation - Add FPI Columns
            {
                flightId = pFlightId;
                date = pDate;
                sta_d = pSta;
                airlineCode = pAirlineCode;
                flightNb = pFlightNb;
                airportCode = pAirportCode;
                flightCategory = pFlightCategory;
                aircraftType = pAircraftType;
                noBSM = pNoBSM;
                cbp = pCBP;
                tsa = pTSA;
                nbSeats = pNbSeats;

                // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                containerSize = pContainerSize;
                totalNbPassengers = pTotalNbPassengers;
                totalNbBags = pTotalNbBags;

                calculationType = pCalculationType;
                octTableDescription = pOCTTableDescription;
                // >> Task #9465 Pax2Sim - Allocation - Add FPI Columns

                firstDesk = pFirstDesk;
                lastDesk = pLastDesk;
                desksUsed = pLastDesk - pFirstDesk + 1; // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column
                openingTime = pOpeningTime;
                closingTime = pClosingTime;
                terminalNb = pTerminalNb;
                groundHandler = pGroundHandler;

                user1 = pUser1;
                user2 = pUser2;
                user3 = pUser3;
                user4 = pUser4;
                user5 = pUser5;

                allocationType = pAllocationType;
                resourceTypeName = pResourceTypeName;
            }

            internal Object[] getRow(bool departure)
            {
                Object[] fpiTableRowInfo;
                if (departure)
                    fpiTableRowInfo = new Object[29];   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column
                else
                    fpiTableRowInfo = new Object[30];   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column

                fpiTableRowInfo[0] = flightId;
                fpiTableRowInfo[1] = date;
                fpiTableRowInfo[2] = sta_d;
                fpiTableRowInfo[3] = airlineCode;
                fpiTableRowInfo[4] = flightNb;
                fpiTableRowInfo[5] = airportCode;
                fpiTableRowInfo[6] = flightCategory;
                fpiTableRowInfo[7] = aircraftType;

                int index = 0;
                if (departure)
                {
                    fpiTableRowInfo[8] = tsa;
                    index = 9;
                }
                else
                {
                    fpiTableRowInfo[8] = noBSM;
                    fpiTableRowInfo[9] = cbp;
                    index = 10;
                }

                fpiTableRowInfo[index] = nbSeats;
                // << Task #9465 Pax2Sim - Allocation - Add FPI Columns                
                fpiTableRowInfo[index + 1] = totalNbPassengers;
                fpiTableRowInfo[index + 2] = totalNbBags;
                if (containerSize >= 0)
                    fpiTableRowInfo[index + 3] = containerSize.ToString();
                else
                    fpiTableRowInfo[index + 3] = "";
                // >> Task #9465 Pax2Sim - Allocation - Add FPI Columns

                fpiTableRowInfo[index + 4] = firstDesk;
                fpiTableRowInfo[index + 5] = lastDesk;
                fpiTableRowInfo[index + 6] = desksUsed; // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column
                fpiTableRowInfo[index + 7] = openingTime;
                fpiTableRowInfo[index + 8] = closingTime;
                fpiTableRowInfo[index + 9] = terminalNb;
                fpiTableRowInfo[index + 10] = groundHandler;

                fpiTableRowInfo[index + 11] = user1;
                fpiTableRowInfo[index + 12] = user2;
                fpiTableRowInfo[index + 13] = user3;
                fpiTableRowInfo[index + 14] = user4;
                fpiTableRowInfo[index + 15] = user5;

                fpiTableRowInfo[index + 16] = allocationType;
                fpiTableRowInfo[index + 17] = resourceTypeName;

                // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                fpiTableRowInfo[index + 18] = calculationType;
                fpiTableRowInfo[index + 19] = octTableDescription;
                // >> Task #9465 Pax2Sim - Allocation - Add FPI Columns

                return fpiTableRowInfo;
            }

            internal static DataTable getTable(String resourceType, bool departure, String observedTerminal)
            {
                DataTable dtTable = new DataTable(GlobalNames.FPI_TableName);
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_ID, typeof(int));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_DATE, typeof(DateTime));
                if (departure)
                    dtTable.Columns.Add(GlobalNames.sFPD_Column_STD, typeof(TimeSpan));
                else
                    dtTable.Columns.Add(GlobalNames.sFPA_Column_STA, typeof(TimeSpan));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_AirlineCode, typeof(String));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_FlightN, typeof(String));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_AirportCode, typeof(String));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_FlightCategory, typeof(String));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_AircraftType, typeof(String));
                if (departure)
                    dtTable.Columns.Add(GlobalNames.sFPD_Column_TSA, typeof(Boolean));
                else
                {
                    dtTable.Columns.Add(GlobalNames.sFPA_Column_NoBSM, typeof(Boolean));
                    dtTable.Columns.Add(GlobalNames.sFPA_Column_CBP, typeof(Boolean));
                }
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_NbSeats, typeof(int));
                // << Task #9465 Pax2Sim - Allocation - Add FPI Columns                
                dtTable.Columns.Add(GlobalNames.FPI_Column_Nb_Passengers, typeof(double));
                dtTable.Columns.Add(GlobalNames.FPI_Column_Nb_Bags, typeof(double));
                dtTable.Columns.Add(GlobalNames.FPI_Column_Container_Size, typeof(String));
                // >> Task #9465 Pax2Sim - Allocation - Add FPI Columns

                dtTable.Columns.Add(resourceType + " First Desk", typeof(int));
                dtTable.Columns.Add(resourceType + " Last Desk", typeof(int));
                dtTable.Columns.Add(GlobalNames.FPI_Column_NbOfResourcesUsed, typeof(int));    // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column
                dtTable.Columns.Add(resourceType + " Opening Time", typeof(DateTime));
                dtTable.Columns.Add(resourceType + " Closing Time", typeof(DateTime));
                dtTable.Columns.Add(observedTerminal + " Nb", typeof(int));
                dtTable.Columns.Add(GlobalNames.sFPAirline_GroundHandlers, typeof(String));

                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_User1, typeof(String));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_User2, typeof(String));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_User3, typeof(String));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_User4, typeof(String));
                dtTable.Columns.Add(GlobalNames.sFPD_A_Column_User5, typeof(String));

                dtTable.Columns.Add(GlobalNames.FPI_Column_Allocation_Type, typeof(String));
                dtTable.Columns.Add(GlobalNames.FPI_Column_Resource_Type_Name, typeof(String));

                // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                dtTable.Columns.Add(GlobalNames.FPI_Column_CalculationType, typeof(String));
                dtTable.Columns.Add(GlobalNames.FPI_Column_OCTTableUsed, typeof(String));
                // >> Task #9465 Pax2Sim - Allocation - Add FPI Columns

                return dtTable;
            }
        }
        

        #region Fonctions pour l'ouverture et la sauvegarde d'un GenerateAllocationTool.
        internal delegate DataTable GetTable(String NomTable);
        internal delegate DataManagement.ExceptionTable GetTableException(String NomTable);
        internal GenerateAllocationTool(XmlNode xnNode, GetTable dGetTable, GetTableException dGetTableException)
        {
            //OverallTools.FonctionUtiles.getValueChild()
            //OverallTools.FonctionUtiles.getValueChild(
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xnNode, "Name"))
            {
                sNomTable_ = xnNode.Attributes["Name"].Value;
            }
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xnNode, "TypeAllocation"))
            {
                int iValue;
                if (!Int32.TryParse(xnNode.Attributes["TypeAllocation"].Value, out iValue))
                    AllocationType = TypeAllocation.MakeUpAllocation;
                else
                    AllocationType = (TypeAllocation)iValue;
            }
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xnNode, GlobalNames.IS_LIEGE_ALLOCATION_PARAM_SCENARIO_ATTRIBUTE_NAME))   // >> Bug #13367 Liege allocation
                Boolean.TryParse(xnNode.Attributes[GlobalNames.IS_LIEGE_ALLOCATION_PARAM_SCENARIO_ATTRIBUTE_NAME].Value, out isLiegeAllocation);
            
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "iTerminal_") != "")
            {
                iTerminal_ = FonctionsType.getInt(OverallTools.FonctionUtiles.getValueChild(xnNode, "iTerminal_"));
            }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sNomColonneAllocation_") != "") { sNomColonneAllocation_ = OverallTools.FonctionUtiles.getValueChild(xnNode, "sNomColonneAllocation_"); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtFPTable_") != "") { dtFPTable_ = dGetTable(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtFPTable_")); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtOCTTable_") != "") 
            {
                UseOCTTableExceptions = xnNode["dtOCTTable_"].HasAttribute("UseException");
                dtOCTTable_ = dGetTableException(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtOCTTable_")); 
            }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtAirlineTable_") != "") { dtAirlineTable_ = dGetTable(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtAirlineTable_")); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtAircraftTable_") != "")
            {
                UseAircraftTableExceptions = xnNode["dtAircraftTable_"].HasAttribute("UseException");
                dtAircraftTable_ = dGetTableException(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtAircraftTable_"));
            }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtBegin_") != "") { DateTime.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtBegin_"), out dtBegin_); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtEnd_") != "") { DateTime.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtEnd_"), out dtEnd_); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dStep_") != "") { Double.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "dStep_"), out dStep_); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dTimeBetweenFlights_") != "") { Double.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "dTimeBetweenFlights_"), out dTimeBetweenFlights_); }

            // >> Task #16728 PAX2SIM Improvements (Recurring) C#27 New.1.            
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, XML_ATTRIBUTE_MUP_CALCULATE_USING_CONTAINER_SIZE) != "") 
            {
                Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, XML_ATTRIBUTE_MUP_CALCULATE_USING_CONTAINER_SIZE), out calculateBasedOnContainerSize); 
            }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, XML_ATTRIBUTE_BC_USE_INFEED_LIMITATION) != "") 
            {
                Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, XML_ATTRIBUTE_BC_USE_INFEED_LIMITATION), out useInfeedLimitation);
            }
            // << Task #16728 PAX2SIM Improvements (Recurring) C#27 New.1.

            /*if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sFlightCategoriesSH_") != "") { sFlightCategoriesSH_ = OverallTools.FonctionUtiles.getValueChild(xnNode, "sFlightCategoriesSH_"); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "iToleranceSH_") != "") { Int32.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "iToleranceSH_"), out iToleranceSH_); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "iToleranceLH_") != "") { Int32.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "iToleranceLH_"), out iToleranceLH_); }
            */
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bAllocateFlightPlan_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bAllocateFlightPlan_"), out bAllocateFlightPlan_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bColorByFC_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bColorByFC_"), out bColorByFC_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bColorByHandler_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bColorByHandler_"), out bColorByHandler_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bColorByAirline_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bColorByAirline_"), out bColorByAirline_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bSegregateLooseULD_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bSegregateLooseULD_"), out bSegregateLooseULD_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bFCAllocation_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bFCAllocation_"), out bFCAllocation_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bSegregateGroundHandlers_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bSegregateGroundHandlers_"), out bSegregateGroundHandlers_); }

            // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bContinousSegregationNumerotation") != "")
            {
                Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bContinousSegregationNumerotation"),
                    out bContinousSegregationNumerotation);
            }
            // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering

            // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "firstIndexForAllocation_") != "")
            {
                int value = 0;
                if (Int32.TryParse(OverallTools.FonctionUtiles
                                                    .getValueChild(xnNode, "firstIndexForAllocation_"), out value))
                {
                    firstIndexForAllocation_ = value;
                }
            }
            // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bUseFPContent_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bUseFPContent_"), out bUseFPContent_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bIsFPAFlight_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bIsFPAFlight_"), out bIsFPAFlight_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "bShowFlightNumber_") != "") { Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "bShowFlightNumber_"), out bShowFlightNumber_); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sObservedTerminal_") != "") { sObservedTerminal_ = OverallTools.FonctionUtiles.getValueChild(xnNode, "sObservedTerminal_"); }


            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnID") != "") { sColumnID = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnID"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnFLIGHTN") != "") { sColumnFLIGHTN = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnFLIGHTN"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnDate") != "") { sColumnDate = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnDate"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnTime") != "") { sColumnTime = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnTime"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnTerminalResult") != "") { sColumnTerminalResult = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnTerminalResult"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnFirstColumnResult") != "") { sColumnFirstColumnResult = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnFirstColumnResult"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnLastColumnResult") != "") { sColumnLastColumnResult = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnLastColumnResult"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnFirstColumnResultFirst") != "") { sColumnFirstColumnResultFirst = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnFirstColumnResultFirst"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnLastColumnResultFirst") != "") { sColumnLastColumnResultFirst = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnLastColumnResultFirst"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnFlightCategory") != "") { sColumnFlightCategory = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnFlightCategory"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAircraft") != "") { sColumnAircraft = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAircraft"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAirline") != "") { sColumnAirline = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAirline"); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAircraftAircraft") != "") { sColumnAircraftAircraft = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAircraftAircraft"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAircraftULDLoose") != "") { sColumnAircraftULDLoose = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAircraftULDLoose"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAirlineAirline") != "") { sColumnAirlineAirline = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAirlineAirline"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAirlineGroundHandlers") != "") { sColumnAirlineGroundHandlers = OverallTools.FonctionUtiles.getValueChild(xnNode, "sColumnAirlineGroundHandlers"); }



            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sOpeningOCTDesk") != "") { sOpeningOCTDesk = OverallTools.FonctionUtiles.getValueChild(xnNode, "sOpeningOCTDesk"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sClosingOCTDesk") != "") { sClosingOCTDesk = OverallTools.FonctionUtiles.getValueChild(xnNode, "sClosingOCTDesk"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sNumberOCTDesk") != "") { sNumberOCTDesk = OverallTools.FonctionUtiles.getValueChild(xnNode, "sNumberOCTDesk"); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sPartialClosingOCTDesk") != "") { sPartialClosingOCTDesk = OverallTools.FonctionUtiles.getValueChild(xnNode, "sPartialClosingOCTDesk"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sPartialNumberOCTDesk") != "") { sPartialNumberOCTDesk = OverallTools.FonctionUtiles.getValueChild(xnNode, "sPartialNumberOCTDesk"); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "iNumberOfDoubleInfeed") != "") { Int32.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "iNumberOfDoubleInfeed"), out iNumberOfDoubleInfeed); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dInfeedSpeed") != "") { Double.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "dInfeedSpeed"), out dInfeedSpeed); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtLoadFactors") != "")
            {
                UseLoadFactorExceptions = xnNode["dtLoadFactors"].HasAttribute("UseException");
                dtLoadFactors = dGetTableException(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtLoadFactors")); 
            }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtNbBagage") != "") {

                UseNbBaggageExceptions = xnNode["dtNbBagage"].HasAttribute("UseException"); 
                dtNbBagage = dGetTableException(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtNbBagage"));
            }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, XML_ATTRIBUTE_FP_PARAMETERS_TABLE) != "")
            {
                flightPlanParametersTable = dGetTable((OverallTools.FonctionUtiles.getValueChild(xnNode, XML_ATTRIBUTE_FP_PARAMETERS_TABLE)));
            }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "dtBagConstraint") != "") { dtBagConstraint = dGetTableException(OverallTools.FonctionUtiles.getValueChild(xnNode, "dtBagConstraint")); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sLoadFactor") != "") { sLoadFactor = OverallTools.FonctionUtiles.getValueChild(xnNode, "sLoadFactor"); }
            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sContainerSize") != "") { sContainerSize = OverallTools.FonctionUtiles.getValueChild(xnNode, "sContainerSize"); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sNBContainerPerCycle") != "") { sNBContainerPerCycle = OverallTools.FonctionUtiles.getValueChild(xnNode, "sNBContainerPerCycle"); }

            if (OverallTools.FonctionUtiles.getValueChild(xnNode, "sDeadTimeBetweenCycle") != "") { sDeadTimeBetweenCycle = OverallTools.FonctionUtiles.getValueChild(xnNode, "sDeadTimeBetweenCycle"); }
            if (OverallTools.FonctionUtiles.hasNamedChild(xnNode, "OverlapParameters") )
            {
                lsfoOverlapParameters = new List<SegregationForOverlap>();
                foreach (XmlElement xeChild in xnNode["OverlapParameters"].ChildNodes)
                {
                    lsfoOverlapParameters.Add(new SegregationForOverlap(xeChild, null));
                }
            }
            // >> Task #12741 Pax2Sim - Dubai - flight exceptions
            if (PAX2SIM.dubaiMode)
            {
                if (OverallTools.FonctionUtiles.getValueChild(xnNode, "useDynamicOpeningTimes") != "")
                {
                    Boolean.TryParse(OverallTools.FonctionUtiles.getValueChild(xnNode, "useDynamicOpeningTimes"), out useDynamicOpeningTimes);
                }
                if (OverallTools.FonctionUtiles.getValueChild(xnNode, "openingTimeFlightPlanColumnName") != "")
                {
                    sColumnAirline = OverallTools.FonctionUtiles.getValueChild(xnNode, "openingTimeFlightPlanColumnName");
                }
            }
            // << Task #12741 Pax2Sim - Dubai - flight exceptions
        }
        private const string XML_ATTRIBUTE_FP_PARAMETERS_TABLE = "FPParametersTable";
        private const string XML_ATTRIBUTE_MUP_CALCULATE_USING_CONTAINER_SIZE = "XMLCalculateUsingContainerSize";
        private const string XML_ATTRIBUTE_BC_USE_INFEED_LIMITATION = "XMLUseInfeedLimitation";
        internal XmlElement Save(XmlDocument xdDocument)
        {
            XmlElement xeResult = xdDocument.CreateElement("GenerateAllocationTool");
            xeResult.SetAttribute("Name", sNomTable_);
            xeResult.SetAttribute("TypeAllocation", ((Int32)AllocationType).ToString());
            xeResult.SetAttribute(GlobalNames.IS_LIEGE_ALLOCATION_PARAM_SCENARIO_ATTRIBUTE_NAME, isLiegeAllocation.ToString());  // << Bug #13367 Liege allocation
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sNomColonneAllocation_", sNomColonneAllocation_));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "iTerminal_", iTerminal_.ToString()));
            if(dtFPTable_ !=null)
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtFPTable_", dtFPTable_.TableName));
            if (dtOCTTable_ != null)
            {
                XmlElement xeOCTTable = OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtOCTTable_", dtOCTTable_.Name);
                if (UseOCTTableExceptions)
                    xeOCTTable.SetAttribute("UseException", true.ToString());
                xeResult.AppendChild(xeOCTTable);
            }
            if (dtAirlineTable_ != null)
                xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtAirlineTable_", dtAirlineTable_.TableName));
            if (dtAircraftTable_ != null)
            {
                XmlElement xeAircraft = OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtAircraftTable_", dtAircraftTable_.Name);
                if (UseAircraftTableExceptions)
                    xeAircraft.SetAttribute("UseException", true.ToString());
                xeResult.AppendChild(xeAircraft);
            }


            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtBegin_", dtBegin_.ToString()));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtEnd_", dtEnd_.ToString()));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "dStep_", dStep_.ToString()));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "dTimeBetweenFlights_", dTimeBetweenFlights_.ToString()));

            // >> Task #16728 PAX2SIM Improvements (Recurring) C#27 New.1.            
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, XML_ATTRIBUTE_MUP_CALCULATE_USING_CONTAINER_SIZE, calculateBasedOnContainerSize.ToString()));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, XML_ATTRIBUTE_BC_USE_INFEED_LIMITATION, useInfeedLimitation.ToString()));
            // << Task #16728 PAX2SIM Improvements (Recurring) C#27 New.1.

            /*xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sFlightCategoriesSH_", sFlightCategoriesSH_));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "iToleranceSH_", iToleranceSH_.ToString()));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "iToleranceLH_", iToleranceLH_.ToString()));
            */
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bAllocateFlightPlan_", bAllocateFlightPlan_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bColorByFC_", bColorByFC_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bColorByHandler_", bColorByHandler_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bColorByAirline_", bColorByAirline_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bSegregateLooseULD_", bSegregateLooseULD_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bFCAllocation_", bFCAllocation_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bSegregateGroundHandlers_", bSegregateGroundHandlers_.ToString()));

            // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bContinousSegregationNumerotation", bContinousSegregationNumerotation.ToString()));
            // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering

            // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "firstIndexForAllocation_", firstIndexForAllocation_.ToString()));
            // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bUseFPContent_", bUseFPContent_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bIsFPAFlight_", bIsFPAFlight_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "bShowFlightNumber_", bShowFlightNumber_.ToString()));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sObservedTerminal_", sObservedTerminal_));


            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnID", sColumnID));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnFLIGHTN", sColumnFLIGHTN));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnDate", sColumnDate));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnTime", sColumnTime));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnTerminalResult", sColumnTerminalResult));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnFirstColumnResult", sColumnFirstColumnResult));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnLastColumnResult", sColumnLastColumnResult));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnFirstColumnResultFirst", sColumnFirstColumnResultFirst));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnLastColumnResultFirst", sColumnLastColumnResultFirst));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnFlightCategory", sColumnFlightCategory));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnAircraft", sColumnAircraft));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnAirline", sColumnAirline));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnAircraftAircraft", sColumnAircraftAircraft));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnAircraftULDLoose", sColumnAircraftULDLoose));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnAirlineAirline", sColumnAirlineAirline));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sColumnAirlineGroundHandlers", sColumnAirlineGroundHandlers));



            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sOpeningOCTDesk", sOpeningOCTDesk));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sClosingOCTDesk", sClosingOCTDesk));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sNumberOCTDesk", sNumberOCTDesk));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sPartialClosingOCTDesk", sPartialClosingOCTDesk));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sPartialNumberOCTDesk", sPartialNumberOCTDesk));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "iNumberOfDoubleInfeed", iNumberOfDoubleInfeed.ToString()));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "dInfeedSpeed", dInfeedSpeed.ToString()));
            if (dtLoadFactors != null)
            {
                XmlElement xeLF = OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtLoadFactors", dtLoadFactors.Name);
                if (UseLoadFactorExceptions)
                    xeLF.SetAttribute("UseException", true.ToString());
                xeResult.AppendChild(xeLF);
            }
            if (dtNbBagage != null)
            {
                XmlElement xeBags = OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtNbBagage", dtNbBagage.Name);
                if (UseNbBaggageExceptions)
                    xeBags.SetAttribute("UseException", true.ToString());
                xeResult.AppendChild(xeBags);
            }
            if (flightPlanParametersTable != null)
            {
                XmlElement xmlElementFpParameters = OverallTools.FonctionUtiles.CreateElement(xdDocument, XML_ATTRIBUTE_FP_PARAMETERS_TABLE, flightPlanParametersTable.TableName);
                xeResult.AppendChild(xmlElementFpParameters);
            }
            if (dtBagConstraint != null)
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "dtBagConstraint", dtBagConstraint.Name));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sLoadFactor", sLoadFactor));
            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sContainerSize", sContainerSize));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sNBContainerPerCycle", sNBContainerPerCycle));

            xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "sDeadTimeBetweenCycle", sDeadTimeBetweenCycle));
            if (lsfoOverlapParameters != null)
            {
                XmlElement xeOverlap = xdDocument.CreateElement("OverlapParameters");
                foreach (SegregationForOverlap sfoTmp in lsfoOverlapParameters)
                {
                    xeOverlap.AppendChild(sfoTmp.Save(xdDocument));
                }
                xeResult.AppendChild(xeOverlap);
            }
            // >> Task #12741 Pax2Sim - Dubai - flight exceptions
            if (PAX2SIM.dubaiMode)
            {
                xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "useDynamicOpeningTimes", useDynamicOpeningTimes.ToString()));
                xeResult.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "openingTimeFlightPlanColumnName", openingTimeFlightPlanColumnName));
            }
            // << Task #12741 Pax2Sim - Dubai - flight exceptions
            return xeResult;
        }
        #endregion

        #region class OCTInformation
        private class OCTInformation
        {
            public Double Opening;
            public Double Closing;
            public int NombrePostes;
            public int iIndexColor;
            public double PartialClosing;
            public int PartialNumber;

            #region Partie spécifique pour les reclaim
            public Double dDeadTime;
            public Double iNbContainerPerCycle;
            public Double dContainerSize;
            #endregion

            public OCTInformation(Double Opening_, Double Closing_, int iIndexColor_)
            {
                Opening = Opening_;
                Closing = Closing_;
                iIndexColor = iIndexColor_;
                NombrePostes = 1;
                PartialClosing = Closing_;
                PartialNumber = 1;
            }
            public OCTInformation(Double Opening_, Double Closing_, int NombrePostes_, int iIndexColor_)
            {
                Opening = Opening_;
                Closing = Closing_;
                NombrePostes = NombrePostes_;
                iIndexColor = iIndexColor_;
                PartialClosing = Closing_;
                PartialNumber = NombrePostes_;
            }
            public OCTInformation(Double Opening_, Double Closing_, int NombrePostes_, Double PartialClosing_, int PartialNumber_, int iIndexColor_)
            {
                Opening = Opening_;
                Closing = Closing_;
                NombrePostes = NombrePostes_;
                iIndexColor = iIndexColor_;
                PartialClosing = PartialClosing_;
                PartialNumber = PartialNumber_;
                if (PartialClosing < Opening)
                {
                    Opening = PartialClosing_;
                    NombrePostes = PartialNumber_;
                    PartialClosing = Opening_;
                    PartialNumber = NombrePostes_;
                }
                if ((PartialClosing == Opening) ||
                   (PartialClosing == Closing))
                    PartialNumber = NombrePostes;
                if (PartialNumber == 0)
                {
                    PartialClosing = Closing;
                    PartialNumber = NombrePostes;
                }
                if (PartialClosing > Closing_)
                {
                    PartialClosing = Closing_;
                    Closing = PartialClosing_;

                }

            }
            public Double Difference
            {
                get
                {
                    return Math.Abs(Opening - Closing);
                }
            }
        }
        #endregion

        #region class BaggageConstraint (inutile pour le moment)
        private class BaggageConstraint
        {
            internal class Constraint
            {
                public Constraint()
                {
                    iInfeedNumber = 0;
                    dInfeedSpeed = 0.0;
                    iSize = 0;
                    iNumberMiniOfBag = 0;
                    iNumberMaxiOfBag = 0;
                    sEcludedCategories = "";
                    sEcludedGroundHandlers = "";
                    sEcludedAirlineCodes = "";
                    sEcludedFlight = "";
                    sEcludedContainerType = "";
                }
                public int iInfeedNumber;
                public Double dInfeedSpeed;
                public int iSize;
                public int iNumberMiniOfBag;
                public int iNumberMaxiOfBag;
                public String sEcludedCategories;
                public String sEcludedGroundHandlers;
                public String sEcludedAirlineCodes;
                public String sEcludedFlight;
                public String sEcludedContainerType;
            }
            Dictionary<String, Constraint> htConstraints_;

            internal static BaggageConstraint getConstraints(DataTable dtBaggageConstraint, List<String> alErrors)
            {
                if (dtBaggageConstraint == null)
                {
                    alErrors.Add("Err00040 : Baggage claim constraint empty. Please select another table or options.");
                    return null;
                }
                int iIndexInfeedNumber = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Infeed number");
                int iIndexInfeedSpeed = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Infeed speed (sec/bags)");
                int iIndexLimitBag = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Limit Bag Acceptance (Number of bags)");
                int iIndexBagMini = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Number of Bag Min");
                int iIndexBagMaxi = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Number of Bag Max");
                int iIndexExFC = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Excluded categories");
                int iIndexExGH = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Excluded Ground Handler");
                int iIndexExAirline = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Excluded Airline Code");
                int iIndexExFlight = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Excluded Flight");
                int iIndexExContainerType = OverallTools.DataFunctions.indexLigne(dtBaggageConstraint, 0, "Excluded Container type");
                if (iIndexInfeedNumber == -1)
                {
                    alErrors.Add("Err00039 : Baggage claim constraint unvalid. The line \"Infeed number\" is not present in the table.");
                    return null;
                }
                if (iIndexInfeedSpeed == -1)
                {
                    alErrors.Add("Err00038 : Baggage claim constraint unvalid. The line \"Infeed speed (sec/bags)\" is not present in the table.");
                    return null;
                }
                if (iIndexLimitBag == -1)
                {
                    alErrors.Add("Err00037 : Baggage claim constraint unvalid. The line \"Limit Bag Acceptance (Number of bags)\" is not present in the table.");
                    return null;
                }
                if (iIndexBagMini == -1)
                {
                    alErrors.Add("Err00036 : Baggage claim constraint unvalid. The line \"Number of Bag Min\" is not present in the table.");
                    return null;
                }
                if (iIndexBagMaxi == -1)
                {
                    alErrors.Add("Err00035 : Baggage claim constraint unvalid. The line \"Number of Bag Max\" is not present in the table.");
                    return null;
                }

                if (iIndexExFC == -1)
                {
                    alErrors.Add("Err00034 : Baggage claim constraint unvalid. The line \"Excluded categories\" is not present in the table.");
                    return null;
                }

                if (iIndexExGH == -1)
                {
                    alErrors.Add("Err00033 : Baggage claim constraint unvalid. The line \"Excluded Ground Handler\" is not present in the table.");
                    return null;
                }
                if (iIndexExAirline == -1)
                {
                    alErrors.Add("Err00032 : Baggage claim constraint unvalid. The line \"Excluded Airline Code\" is not present in the table.");
                    return null;
                }
                if (iIndexExFlight == -1)
                {
                    alErrors.Add("Err00031 : Baggage claim constraint unvalid. The line \"Excluded Flight\" is not present in the table.");
                    return null;
                }
                if (iIndexExContainerType == -1)
                {
                    alErrors.Add("Err00030 : Baggage claim constraint unvalid. The line \"Excluded Container type\" is not present in the table.");
                    return null;
                }
                Dictionary<String, Constraint> htConstraints = new Dictionary<String, Constraint>();
                for (int i = 1; i < dtBaggageConstraint.Columns.Count; i++)
                {
                    String sConstraintName = dtBaggageConstraint.Columns[i].ColumnName;
                    Constraint cConstraints = new Constraint();
                    if (Int32.TryParse((string)dtBaggageConstraint.Rows[iIndexInfeedNumber][i], out cConstraints.iInfeedNumber))
                    {
                        alErrors.Add("Err00029 : The value for the Infeed number is incorrect for the column \"" + sConstraintName + "\" in the table " + dtBaggageConstraint.TableName + ".");
                        htConstraints = null;
                        cConstraints = null;
                        return null;
                    }
                    if (Double.TryParse((string)dtBaggageConstraint.Rows[iIndexInfeedSpeed][i], out cConstraints.dInfeedSpeed))
                    {
                        alErrors.Add("Err00028 : The value for the Infeed speed is incorrect for the column \"" + sConstraintName + "\" in the table " + dtBaggageConstraint.TableName + ".");
                        htConstraints = null;
                        cConstraints = null;
                        return null;
                    }
                    if (Int32.TryParse((string)dtBaggageConstraint.Rows[iIndexLimitBag][i], out cConstraints.iSize))
                    {
                        alErrors.Add("Err00027 : The value for the Limit bags is incorrect for the column \"" + sConstraintName + "\" in the table " + dtBaggageConstraint.TableName + ".");
                        htConstraints = null;
                        cConstraints = null;
                        return null;
                    }
                    if ((string)dtBaggageConstraint.Rows[iIndexBagMini][i] != "")
                    {
                        if (Int32.TryParse((string)dtBaggageConstraint.Rows[iIndexBagMini][i], out cConstraints.iNumberMiniOfBag))
                        {
                            alErrors.Add("Err00026 : The value for the Bags mini is incorrect for the column \"" + sConstraintName + "\" in the table " + dtBaggageConstraint.TableName + ".");
                            htConstraints = null;
                            cConstraints = null;
                            return null;
                        }
                    }
                    if ((string)dtBaggageConstraint.Rows[iIndexBagMaxi][i] != "")
                    {
                        if (Int32.TryParse((string)dtBaggageConstraint.Rows[iIndexBagMaxi][i], out cConstraints.iNumberMaxiOfBag))
                        {
                            alErrors.Add("Err00025 : The value for the Bags maxi is incorrect for the column \"" + sConstraintName + "\" in the table " + dtBaggageConstraint.TableName + ".");
                            htConstraints = null;
                            cConstraints = null;
                            return null;
                        }
                    }
                    cConstraints.sEcludedCategories = dtBaggageConstraint.Rows[iIndexExFC][i].ToString();
                    cConstraints.sEcludedGroundHandlers = dtBaggageConstraint.Rows[iIndexExGH][i].ToString();
                    cConstraints.sEcludedAirlineCodes = dtBaggageConstraint.Rows[iIndexExAirline][i].ToString();
                    cConstraints.sEcludedFlight = dtBaggageConstraint.Rows[iIndexExFlight][i].ToString();
                    cConstraints.sEcludedContainerType = dtBaggageConstraint.Rows[iIndexExContainerType][i].ToString();
                    htConstraints.Add(sConstraintName, cConstraints);
                }
                return new BaggageConstraint(htConstraints);
            }
            private BaggageConstraint(Dictionary<String, Constraint> htConstraints)
            {
                htConstraints_ = htConstraints;
            }
            internal Constraint getConstraints(String sTerminal)
            {
                if (htConstraints_ == null)
                    return null;
                if (!htConstraints_.ContainsKey(sTerminal))
                    return null;
                return (Constraint)htConstraints_[sTerminal];
            }
        }
        #endregion

        #region Variables statiques pour les noms interne à la classe
        private static String sOpeningDeskColumn = "DateOpeningDesks";
        private static String sClosingDeskColumn = "DateClosingDesks";
        private static String sClosingPartialDeskColumn = "DatePartialClosing";


        private static String sGlobal = "Global";
        private static String sNeed = "Need";
        private static String sOccupied = "Occupied";
        private static String sFlightNumber = "Flight number";
        #endregion

        #region Variables Résultats
        /// <summary>
        /// Les tables résultats.
        /// </summary>
        private DataTable[] dtResults;
        /// <summary>
        /// Accesseurs vers les tables résultats.
        /// </summary>
        public DataTable[] TableResultat
        {
            get
            {
                return dtResults;
            }
        }

        /// <summary>
        /// The table that will contains the errors that happend while allocating the flight plan.
        /// </summary>
        private DataTable dtErrors;

        /// <summary>
        /// The table that will contains the errors that happend while allocating the flight plan.
        /// </summary>
        internal DataTable TableErrors
        {
            get
            {
                return dtErrors;
            }
        }


        private DataTable dtStatistiques;
        internal DataTable Statistiques
        {
            get
            {
                return dtStatistiques;
            }
        }

        // Scenario Allocation FlightPlan Information table for gantt
        private DataTable dtFlightPlanInformation;
        internal DataTable FlightPlanInformation
        {
            get
            {
                return dtFlightPlanInformation;
            }
        }

        /// <summary>
        /// Les structures contenant les couleurs pour les tables d'allocation.
        /// </summary>
        private VisualisationMode[] tvmAllocationVisualisation_;
        /// <summary>
        /// Les structures contenant les couleurs pour les tables d'allocation.
        /// </summary>
        public VisualisationMode[] AllocationVisualisation
        {
            get
            {
                return tvmAllocationVisualisation_;
            }
        }
        #endregion

        #region Input Variables for the class
        
        #region Paramètres généraux de la classe.
        #region AllocationType
        /// <summary>
        /// Le type d'allocation générée par cette instance de classe.
        /// </summary>
        private TypeAllocation tAllocationType;
        /// <summary>
        /// Le type d'allocation générée par cette instance de classe.
        /// </summary>
        internal TypeAllocation AllocationType
        {
            get
            {
                return tAllocationType;
            }
            set
            {
                tAllocationType = value;
            }
        }
        #endregion

        #region NomTable
        /// <summary>
        /// Le nom de la table d'allocation qui va être généré.
        /// </summary>
        private String sNomTable_;
        /// <summary>
        /// Le nom de la table d'allocation qui va être généré.
        /// </summary>
        internal String NomTable
        {
            set
            {
                sNomTable_ = value;
            }
            get
            {
                return sNomTable_;
            }
        }
        #endregion

        #region sNomColonneAllocation
        /// <summary>
        /// La souche du nom des colonnes qui vont être ajoutées à la table d'allocation.
        /// </summary>
        private String sNomColonneAllocation_;
        /// <summary>
        /// La souche du nom des colonnes qui vont être ajoutées à la table d'allocation.
        /// </summary>
        internal String sNomColonneAllocation
        {
            get
            {
                return sNomColonneAllocation_;
            }
        }
        #endregion

        #region dtFPTable
        /// <summary>
        /// La table plan de vol.
        /// </summary>
        private DataTable dtFPTable_;

        internal DataTable dtFPTable
        {
            get
            {
                return dtFPTable_;
            }
        }
        #endregion

        #region dtOCTTable
        /// <summary>
        /// La table OCT utilisé pour l'allocation.
        /// </summary>
        private DataManagement.ExceptionTable dtOCTTable_;

        internal DataManagement.ExceptionTable dtOCTTable
        {
            get
            {
                return dtOCTTable_;
            }
        }
        internal Boolean UseOCTTableExceptions;
        #endregion

        #region dtAirlineTable
        /// <summary>
        /// La table Airline utilisé pour l'allocation.
        /// </summary>
        private DataTable dtAirlineTable_;
        /// <summary>
        /// La table Airline utilisé pour l'allocation.
        /// </summary>
        internal DataTable dtAirlineTable
        {
            get
            {
                return dtAirlineTable_;
            }
        }
        #endregion

        #region dtAircraftTable
        /// <summary>
        /// La table Aircraft utilisé pour l'allocation.
        /// </summary>
        private DataManagement.ExceptionTable dtAircraftTable_;
        /// <summary>
        /// La table Aircraft utilisé pour l'allocation.
        /// </summary>
        internal DataManagement.ExceptionTable dtAircraftTable
        {
            get
            {
                return dtAircraftTable_;
            }
        }

        internal Boolean UseAircraftTableExceptions;
        #endregion

        #region dtBegin
        /// <summary>
        /// La date de début d'allocation.
        /// </summary>
        private DateTime dtBegin_;

        internal DateTime dtBegin
        {
            get
            {
                return dtBegin_;
            }
        }
        #endregion

        #region dtEnd
        /// <summary>
        /// La date de fin d'allocation.
        /// </summary>
        internal DateTime dtEnd_;

        internal DateTime dtEnd
        {
            get
            {
                return dtEnd_;
            }
        }
        #endregion

        #region dStep
        /// <summary>
        /// Le pas d'allocation utilisé.
        /// </summary>
        private Double dStep_;

        internal Double dStep
        {
            get
            {
                return dStep_;
            }
        }
        #endregion

        #region dTimeBetweenFlights
        /// <summary>
        /// Trou autorisé entre 2 vols.
        /// </summary>
        private Double dTimeBetweenFlights_;

        internal Double dTimeBetweenFlights
        {
            get
            {
                return dTimeBetweenFlights_;
            }
        }
        #endregion



        #region Terminal
        /// <summary>
        /// Le terminal observé.
        /// </summary>
        private int iTerminal_;
        /// <summary>
        /// Le terminal observé.
        /// </summary>
        public int Terminal
        {
            get
            {
                return iTerminal_;
            }
            set
            {
                iTerminal_ = value;
            }
        }
        #endregion

        #region AllocateFlightPlan
        /// <summary>
        /// Booleen pour savoir si l'on doit allouer le plan de vol avec les informations recueillies.
        /// </summary>
        private bool bAllocateFlightPlan_;
        /// <summary>
        /// Booleen pour savoir si l'on doit allouer le plan de vol avec les informations recueillies.
        /// </summary>
        public Boolean AllocateFlightPlan
        {
            get
            {
                return bAllocateFlightPlan_;
            }
            set
            {
                bAllocateFlightPlan_ = value;
            }
        }
        #endregion
        
        #region SegregateLooseULD
        /// <summary>
        /// Booleen pour savoir si il y a une ségrégation des vols par type de stockage des bagages.
        /// </summary>
        private bool bSegregateLooseULD_;
        /// <summary>
        /// Booleen pour savoir si il y a une ségrégation des vols par type de stockage des bagages.
        /// </summary>
        public Boolean SegregateLooseULD
        {
            get
            {
                return bSegregateLooseULD_;
            }
            set
            {
                bSegregateLooseULD_ = value;
            }
        }
        #endregion

        #region FCAllocation
        /// <summary>
        /// Booleen pour savoir si l'allocation se fait vol par vol ou alors catégorie par catégorie.
        /// </summary>
        private bool bFCAllocation_;
        /// <summary>
        /// Booleen pour savoir si l'allocation se fait vol par vol ou alors catégorie par catégorie.
        /// </summary>
        public Boolean FCAllocation
        {
            get
            {
                return bFCAllocation_;
            }
            set
            {
                bFCAllocation_ = value;
            }
        }
        #endregion

        #region SegregateGroundHandlers
        /// <summary>
        /// Booleen pour savoir si l'allocation suit les ground handler ou pas.
        /// </summary>
        private bool bSegregateGroundHandlers_;
        /// <summary>
        /// Booleen pour savoir si l'allocation suit les ground handler ou pas.
        /// </summary>
        public Boolean SegregateGroundHandlers
        {
            get
            {
                return bSegregateGroundHandlers_;
            }
            set
            {
                bSegregateGroundHandlers_ = value;
            }
        }

        // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
        /// <summary>
        /// Indicates if continous numbering is used for the Resources allocated.
        /// </summary>
        private bool bContinousSegregationNumerotation;
        /// <summary>
        /// Indicates if continous numbering is used for the Resources allocated.
        /// </summary>
        public Boolean ContinousSegregationNumerotation
        {
            get { return bContinousSegregationNumerotation; }
            set { bContinousSegregationNumerotation = value; }
        }
        // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering

        // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
        private int firstIndexForAllocation_;

        public Int32 FirstIndexForAllocation
        {
            get { return firstIndexForAllocation_; }
            set { firstIndexForAllocation_ = value; }
        }
        // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
        #endregion

        #region UseFPContent
        /// <summary>
        /// Booleen pour savoir si le contenu du plan de vol est à place dans la feuille d'allocation dans un premier temps.
        /// </summary>
        private bool bUseFPContent_;
        /// <summary>
        /// Booleen pour savoir si le contenu du plan de vol est à place dans la feuille d'allocation dans un premier temps.
        /// </summary>
        public Boolean UseFPContent
        {
            get
            {
                return bUseFPContent_;
            }
            set
            {
                bUseFPContent_ = value;
            }
        }
        #endregion

        #region IsFPAFlight
        /// <summary>
        /// Booleen pour savoir si l'allocation se fait après l'heure du vol ou avant.
        /// </summary>
        private bool bIsFPAFlight_;
        /// <summary>
        /// Booleen pour savoir si l'allocation se fait après l'heure du vol ou avant.
        /// </summary>
        public Boolean IsFPAFlight
        {
            get
            {
                return bIsFPAFlight_;
            }
            set
            {
                bIsFPAFlight_ = value;
            }
        }
        #endregion

        #region ShowFlightNumber
        /// <summary>
        /// Booleen pour savoir si la table d'allocation doit contenurla colonne Flight Number
        /// </summary>
        private bool bShowFlightNumber_;
        /// <summary>
        /// Booleen pour savoir si la table d'allocation doit contenurla colonne Flight Number
        /// </summary>
        public Boolean ShowFlightNumber
        {
            get
            {
                return bShowFlightNumber_;
            }
            set
            {
                bShowFlightNumber_ = value;
            }
        }
        #endregion

        // >> Task #12741 Pax2Sim - Dubai - flight exceptions
        internal bool useDynamicOpeningTimes;
        internal string openingTimeFlightPlanColumnName;
        // << Task #12741 Pax2Sim - Dubai - flight exceptions
        #endregion

        #region Overlap Informations
        internal class SegregationForOverlap
        {
            String sName;

            List<String> lsFlightCategories;

            int iMaxOverlap;

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

            public List<String> FlightCategories
            {
                get
                {
                    return lsFlightCategories;
                }
                set
                {
                    lsFlightCategories = value;
                }
            }

            public int MaxOverlap
            {
                get
                {
                    return iMaxOverlap;
                }
                set
                {
                    iMaxOverlap = value;
                }
            }

            internal SegregationForOverlap(String Name,List<String> FlightCategories,int MaxOverlap)
            { 
                sName=Name;
                lsFlightCategories=FlightCategories;
                if (MaxOverlap <= 0)
                    iMaxOverlap = 1;
                else
                    iMaxOverlap=MaxOverlap;
            }
            internal SegregationForOverlap(XmlElement Element, VersionManager Version)
            {
                sName = "";
                if (OverallTools.FonctionUtiles.hasNamedAttribute(Element, "Name"))
                {
                    sName = Element.Attributes["Name"].Value;
                }
                iMaxOverlap = 1;
                if (OverallTools.FonctionUtiles.hasNamedAttribute(Element, "MaxOverlap"))
                {
                    iMaxOverlap = FonctionsType.getInt(Element.Attributes["MaxOverlap"].Value);
                    if (iMaxOverlap <= 0)
                        iMaxOverlap = 1;
                }
                if (OverallTools.FonctionUtiles.hasNamedChild(Element, "FlightCategories"))
                {
                    lsFlightCategories = new List<string>();
                    foreach (XmlElement xeChild in Element["FlightCategories"].ChildNodes)
                    {
                        lsFlightCategories.Add(xeChild.FirstChild.Value);
                    }
                }
            }

            internal XmlElement Save(XmlDocument xdDocument)
            {
                XmlElement xeResult = xdDocument.CreateElement("SegregationForOverlap");
                xeResult.SetAttribute("Name", sName);
                xeResult.SetAttribute("MaxOverlap", iMaxOverlap.ToString());
                XmlElement xeFlightcategories = xdDocument.CreateElement("FlightCategories");
                if(lsFlightCategories != null)
                {
                    xeResult.AppendChild(xeFlightcategories);
                    foreach(String sValue in lsFlightCategories)
                    {
                        xeFlightcategories.AppendChild(OverallTools.FonctionUtiles.CreateElement(xdDocument, "FlightCategory", sValue));
                    }
                }
                return xeResult;
            }

            /// <summary>
            /// Method that permits to know if the flight categorie is currently present in the \ref lsFlightCategories
            /// </summary>
            /// <param name="FlightCategory"></param>
            /// <returns></returns>
            internal bool Contains(string FlightCategory)
            {
                if (lsFlightCategories == null)
                    return false;
                return lsFlightCategories.Contains(FlightCategory);
            }
        }

        /// <summary>
        /// Get or set the informations about the overlap parameters
        /// To be able to know what flight categorie can overlap each 
        /// other and for how many flights.
        /// </summary>
        List<SegregationForOverlap> lsfoOverlapParameters;

        /// <summary>
        /// Get or set the informations about the overlap parameters
        /// To be able to know what flight categorie can overlap each 
        /// other and for how many flights.
        /// </summary>
        internal List<SegregationForOverlap> OverlapParameters
        {
            get
            {
                return lsfoOverlapParameters;
            }
            set
            {
                lsfoOverlapParameters = value;
            }
        }

        /// <summary>
        /// La liste des catégories de vols qui représentent les SH (ou tout autre ségrégation.)
        /// </summary>
        //[Obsolete("Use OverlapParameters instead to be able to use the overlap informations.")]
        //private String sFlightCategoriesSH_;

        /// <summary>
        /// La liste des catégories de vols qui représentent les SH (ou tout autre ségrégation.)
        /// </summary>
        [Obsolete("Use OverlapParameters instead to be able to use the overlap informations.")]
        public String FlightCategoriesSH
        {
            get
            {
                return null;// sFlightCategoriesSH_;
            }/*
            set
            {
                sFlightCategoriesSH_ = value;
            }*/
        }
        /// <summary>
        /// Nombre max de vols par reclaim pour les vols SH.
        /// </summary>
        //[Obsolete("Use OverlapParameters instead to be able to use the overlap informations.")]
        //private int iToleranceSH_;
        /// <summary>
        /// Nombre max de vols par reclaim pour les vols SH.
        /// </summary>
        [Obsolete("Use OverlapParameters instead to be able to use the overlap informations.")]
        public int ToleranceSH
        {
            get
            {
                return 1;// iToleranceSH_;
            }/*
            set
            {
                iToleranceSH_ = value;
            }*/
        }
        /// <summary>
        /// Nombre max de vols par reclaim pour les vols non SH.
        /// </summary>
        //[Obsolete("Use OverlapParameters instead to be able to use the overlap informations.")]
        //private int iToleranceLH_;
        /// <summary>
        /// Nombre max de vols par reclaim pour les vols non SH.
        /// </summary>
        [Obsolete("Use OverlapParameters instead to be able to use the overlap informations.")]
        public int ToleranceLH
        {
            get
            {
                return 1;// iToleranceLH_;
            }/*
            set
            {
                iToleranceLH_ = value;
            }*/
        }
        #endregion

        #region Pour la gestion des couleurs.
        /// <summary>
        /// Couleur par flight catégorie.
        /// </summary>
        private bool bColorByFC_;
        /// <summary>
        /// Couleur par flight catégorie.
        /// </summary>
        public Boolean ColorByFC
        {
            get
            {
                return bColorByFC_;
            }
            set
            {
                bColorByFC_ = value;
            }
        }
        /// <summary>
        /// Couleur par Ground Handler.
        /// </summary>
        private bool bColorByHandler_;
        /// <summary>
        /// Couleur par Ground Handler.
        /// </summary>
        public Boolean ColorByHandler
        {
            get
            {
                return bColorByHandler_;
            }
            set
            {
                bColorByHandler_ = value;
            }
        }
        /// <summary>
        /// Couleur par Airline.
        /// </summary>
        private bool bColorByAirline_;
        /// <summary>
        /// Couleur par Airline.
        /// </summary>
        public Boolean ColorByAirline
        {
            get
            {
                return bColorByAirline_;
            }
            set
            {
                bColorByAirline_ = value;
            }
        }
        #endregion

        #region Les noms des colonnes à observer.

        /// <summary>
        /// Nom de la colonne utilisé pour déterminé le terminal observé.
        /// </summary>
        private String sObservedTerminal_;
        /// <summary>
        /// Nom de la colonne utilisé pour déterminé le terminal observé.
        /// </summary>
        public String ObservedTerminal
        {
            get
            {
                return sObservedTerminal_;
            }
            set
            {
                sObservedTerminal_ = value;
            }
        }
        /// <summary>
        /// Nom de la colonne utilisé pour déterminé le terminal observé.
        /// </summary>
        public String sColumnID;
        public String sColumnFLIGHTN;
        public String sColumnDate;
        public String sColumnTime;
        public String sColumnTerminalResult;
        public String sColumnFirstColumnResult;
        public String sColumnLastColumnResult;
        public String sColumnFirstColumnResultFirst;
        public String sColumnLastColumnResultFirst;
        public String sColumnFlightCategory;
        public String sColumnAircraft;
        public String sColumnAirline;

        public String sColumnAircraftAircraft;
        public String sColumnAircraftULDLoose;

        public String sColumnAirlineAirline;
        public String sColumnAirlineGroundHandlers;


        private Int32 iIndexID;
        private Int32 iIndexFLIGHTN;
        private Int32 iIndexDate;
        private Int32 iIndexTime;
        private Int32 iIndexTerminalResult;
        private Int32 iIndexFirstColumnResult;
        private Int32 iIndexLastColumnResult;
        private Int32 iIndexFirstColumnResultFirst;
        private Int32 iIndexLastColumnResultFirst;
        private Int32 iIndexFlightCategory;
        private Int32 iIndexAircraft;
        private Int32 iIndexAirline;

        private Int32 iIndexAircraftAircraft;
        private Int32 iIndexAircraftULDLoose;
        private Int32 iIndexAirlineAirline;
        private Int32 iIndexAirlineGroundHandlers;
        private Int32 iIndexTerminal;

        #endregion

        #region Gestion de la table OCT

        private Int32 iIndexOpeningOCTDesk;
        public String sOpeningOCTDesk;
        private Int32 iIndexClosingOCTDesk;
        public String sClosingOCTDesk;
        private Int32 iIndexNumberOCTDesk;
        public String sNumberOCTDesk;

        private Int32 iIndexPartialClosingOCTDesk;
        public String sPartialClosingOCTDesk;
        private Int32 iIndexPartialNumberOCTDesk;
        public String sPartialNumberOCTDesk;

        #endregion

        #region Partie ajoutée pour le paramétrage de reclaim depuis une différente optique.

        public int iNumberOfDoubleInfeed;
        public Double dInfeedSpeed;
        internal DataManagement.ExceptionTable dtLoadFactors;
        internal DataManagement.ExceptionTable dtNbBagage;
        internal Boolean UseLoadFactorExceptions;
        internal Boolean UseNbBaggageExceptions;
        // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
        internal Boolean calculateBasedOnContainerSize;
        internal Boolean useInfeedLimitation;
        // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
        internal DataTable flightPlanParametersTable;   // >> Task #17690 PAX2SIM - Flight Plan Parameters table

        private Int32 iIndexLoadFactor;
        public String sLoadFactor;
        private Int32 iIndexContainerSize;
        public String sContainerSize;

        private Int32 iIndexNBContainerPerCycle;
        public String sNBContainerPerCycle;

        private Int32 iIndexDeadTimeBetweenCycle;
        public String sDeadTimeBetweenCycle;

        #region For the constraint baggage use 
        internal DataManagement.ExceptionTable dtBagConstraint;

        #region Pour les allocations de reclaim

        /// <summary>
        /// Structure stockant les contraintes pour l'allocation des reclaim.
        /// </summary>
        private BaggageConstraint bcConstraints;
        #endregion
        #endregion
        #endregion

        #region System
        /// <summary>
        /// Fenêtre d'allocation qui permet de faire patienter l'utilisateur.
        /// </summary>
        private Prompt.SIM_LoadingForm cht_;
        /// <summary>
        /// Fenêtre d'allocation qui permet de faire patienter l'utilisateur.
        /// </summary>
        public Prompt.SIM_LoadingForm cht
        {
            set
            {
                cht_ = value;
            }
        }
        /// <summary>
        /// Liste utilisé pour les erreurs survenue durant l'allocation.
        /// </summary>
        private List<String> alErrors_;
        /// <summary>
        /// Liste utilisé pour les erreurs survenue durant l'allocation.
        /// </summary>
        public List<String> Errors
        {
            set
            {
                alErrors_ = value;
            }
        }
        #endregion
        #endregion

        #region Constructor
        internal GenerateAllocationTool(String sNomTable,
                                      TypeAllocation taAllocation,
                                      String sNomColonneAllocation,
                                      DataTable dtFPTable,
                                      DataManagement.NormalTable dtOCTTable,
                                      DataTable dtAirlineTable,
                                      DataManagement.NormalTable dtAircraftTable,
                                      DateTime dtBegin,
                                      DateTime dtEnd,
                                      Double dStep,
                                      Double dTimeBetweenFlights)
        {
            sNomTable_ = sNomTable;
            AllocationType = taAllocation;
            sNomColonneAllocation_ = sNomColonneAllocation;
            dtFPTable_ = dtFPTable;
            if (dtOCTTable is DataManagement.ExceptionTable)
                dtOCTTable_ = (DataManagement.ExceptionTable)dtOCTTable;
            dtAirlineTable_ = dtAirlineTable;
            if (dtAircraftTable is DataManagement.ExceptionTable)
                dtAircraftTable_ = (DataManagement.ExceptionTable)dtAircraftTable;
            dtBegin_ = dtBegin;
            dtEnd_ = dtEnd;
            dStep_ = dStep;
            dTimeBetweenFlights_ = dTimeBetweenFlights;
            tvmAllocationVisualisation_ = null;
            dtResults = null;
            bIsFPAFlight_ = false;
            bShowFlightNumber_ = false;
            dtNbBagage = null;
            dtLoadFactors = null;
            dtBagConstraint = null;
            lsfoOverlapParameters = null;
            UseNbBaggageExceptions = true;
            UseOCTTableExceptions = true;
            UseLoadFactorExceptions = true;
            UseAircraftTableExceptions = true;
            // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            calculateBasedOnContainerSize = false;
            useInfeedLimitation = false;
            // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            flightPlanParametersTable = null;
            /*
            sFlightCategoriesSH_ = "";
            iToleranceLH_ = 1;
            iToleranceSH_ = 1;*/
        }

        #endregion

        #region Function to check the different parameters.
        private Boolean CheckInputTable()
        {
            if ((dtFPTable_ == null) || (dtFPTable_.Rows.Count == 0))
            {
                alErrors_.Add("Err00650 : The flight plan table is empty. Cannot continue allocation.");
                return false;
            }
            if ((dtOCTTable_ == null) || (dtOCTTable_.Table.Rows.Count == 0) || (dtOCTTable_.Table.Columns.Count <= 1))
            {
                if(dtOCTTable_!=null)
                alErrors_.Add("Err00651 : The table \"" + dtOCTTable_.Name + "\" is empty. Cannot continue allocation.");
                else
                    alErrors_.Add("Err00651 : The table \"OCTTable\" is empty. Cannot continue allocation.");
                return false;
            }
            if ((dtAircraftTable_ == null) || (dtAircraftTable_.Table.Rows.Count == 0) || (dtAircraftTable_.Table.Columns.Count == 0))
            {
                if(dtAircraftTable_!=null)
                    alErrors_.Add("Err00652 : The table \"" + dtAircraftTable_.Name + "\" is empty. Cannot continue allocation.");
                else
                    alErrors_.Add("Err00652 : The table \"AircraftTable\" is empty. Cannot continue allocation.");
                return false;
            }
            if ((dtAirlineTable_ == null) || (dtAirlineTable_.Rows.Count == 0) || (dtAirlineTable_.Columns.Count == 0))
            {
                if (dtAirlineTable_ != null)
                alErrors_.Add("Err00653 : The table \"" + dtAirlineTable_.TableName + "\" is empty. Cannot continue allocation.");
                else
                    alErrors_.Add("Err00653 : The table \"AirlineTable\" is empty. Cannot continue allocation.");
                return false;
            }

            iIndexID = ColumnisValid(dtFPTable_, sColumnID, "Err00654", alErrors_);
            iIndexFLIGHTN = ColumnisValid(dtFPTable_, sColumnFLIGHTN, "Err00655", alErrors_);
            iIndexDate = ColumnisValid(dtFPTable_, sColumnDate, "Err00656", alErrors_);
            iIndexTime = ColumnisValid(dtFPTable_, sColumnTime, "Err00657", alErrors_);
            iIndexTerminalResult = ColumnisValid(dtFPTable_, sColumnTerminalResult, "Err00658", alErrors_);
            iIndexFirstColumnResult = ColumnisValid(dtFPTable_, sColumnFirstColumnResult, "Err00659", alErrors_);
            iIndexLastColumnResult = ColumnisValid(dtFPTable_, sColumnLastColumnResult, "Err00660", alErrors_);

            iIndexFirstColumnResultFirst = -1;
            if ((sColumnFirstColumnResultFirst != null) && (sColumnLastColumnResultFirst != null))
            {
                iIndexFirstColumnResultFirst = ColumnisValid(dtFPTable_, sColumnFirstColumnResultFirst, "Warn00682", alErrors_);
                iIndexLastColumnResultFirst = ColumnisValid(dtFPTable_, sColumnLastColumnResultFirst, "Warn00683", alErrors_);
            }
            if ((iIndexFirstColumnResultFirst == -1) ||
                (iIndexLastColumnResultFirst == -1))
            {
                iIndexFirstColumnResultFirst = -1;
                iIndexLastColumnResultFirst = -1;
            }
            iIndexFlightCategory = ColumnisValid(dtFPTable_, sColumnFlightCategory, "Err00661", alErrors_);
            iIndexAircraft = ColumnisValid(dtFPTable_, sColumnAircraft, "Err00662", alErrors_);
            iIndexAirline = ColumnisValid(dtFPTable_, sColumnAirline, "Err00663", alErrors_);
            iIndexTerminal = ColumnisValid(dtFPTable_, sObservedTerminal_, "Err00664", alErrors_);

            iIndexAircraftAircraft = ColumnisValid(dtAircraftTable_.Table, sColumnAircraftAircraft, "Err00665", alErrors_);
            iIndexAircraftULDLoose = ColumnisValid(dtAircraftTable_.Table, sColumnAircraftULDLoose, "Err00666", alErrors_);
            iIndexAirlineAirline = ColumnisValid(dtAirlineTable_, sColumnAirlineAirline, "Err00667", alErrors_);
            iIndexAirlineGroundHandlers = ColumnisValid(dtAirlineTable_, sColumnAirlineGroundHandlers, "Err00668", alErrors_);
            if ((iIndexID == -1) ||
                (iIndexFLIGHTN == -1) ||
                (iIndexDate == -1) ||
                (iIndexTime == -1) ||
                (iIndexTerminalResult == -1) ||
                (iIndexFirstColumnResult == -1) ||
                (iIndexLastColumnResult == -1) ||
                (iIndexFlightCategory == -1) ||
                (iIndexFlightCategory == -1) ||
                (iIndexAircraft == -1) ||
                (iIndexAirline == -1) ||
                (iIndexTerminal == -1) ||
                (iIndexAircraftAircraft == -1) ||
                (iIndexAircraftULDLoose == -1) ||
                (iIndexAirlineAirline == -1) ||
                (iIndexAirlineGroundHandlers == -1))
                return false;

            iIndexOpeningOCTDesk = RowisValid(dtOCTTable_.Table, sOpeningOCTDesk, "Err00670", alErrors_);
            iIndexClosingOCTDesk = RowisValid(dtOCTTable_.Table, sClosingOCTDesk, "Err00671", alErrors_);
            iIndexNumberOCTDesk = -1;
            iIndexPartialClosingOCTDesk = -1;
            iIndexPartialNumberOCTDesk = -1;

            iIndexNBContainerPerCycle = -1;
            iIndexDeadTimeBetweenCycle = -1;
            iIndexContainerSize = -1;
            iIndexLoadFactor = -1;

            if ((sNumberOCTDesk != null) && (sNumberOCTDesk != ""))
            {
                iIndexNumberOCTDesk = RowisValid(dtOCTTable_.Table, sNumberOCTDesk, "Err00672", alErrors_);
                if (iIndexNumberOCTDesk == -1)
                    return false;
                if ((sPartialClosingOCTDesk != null) && (sPartialNumberOCTDesk != null) && (sPartialClosingOCTDesk != "") && (sPartialNumberOCTDesk != ""))
                {
                    iIndexPartialClosingOCTDesk = RowisValid(dtOCTTable_.Table, sPartialClosingOCTDesk, "Err00673", alErrors_);
                    iIndexPartialNumberOCTDesk = RowisValid(dtOCTTable_.Table, sPartialNumberOCTDesk, "Err00674", alErrors_);
                    if ((iIndexNumberOCTDesk == -1) || (iIndexPartialNumberOCTDesk == -1))
                        return false;
                }
            }
            if ((sNBContainerPerCycle != null) && (sNBContainerPerCycle != ""))
            {

                iIndexNBContainerPerCycle = RowisValid(dtOCTTable_.Table, sNBContainerPerCycle, "Err00677", alErrors_);
                iIndexDeadTimeBetweenCycle = RowisValid(dtOCTTable_.Table, sDeadTimeBetweenCycle, "Err00678", alErrors_);
                iIndexContainerSize = RowisValid(dtOCTTable_.Table, sContainerSize, "Err00679", alErrors_);
                if ((iIndexContainerSize < 0) || (iIndexDeadTimeBetweenCycle < 0) || (iIndexContainerSize < 0))
                {
                    iIndexNBContainerPerCycle = -1;
                    iIndexDeadTimeBetweenCycle = -1;
                    iIndexContainerSize = -1;
                }
            }
            if ((dtLoadFactors != null) && (sLoadFactor != null) && (sLoadFactor != ""))
            {
                iIndexLoadFactor = RowisValid(dtLoadFactors.Table, sLoadFactor, "Err00680", alErrors_);
            }

            if (dtBagConstraint != null)
            {
                bcConstraints = BaggageConstraint.getConstraints(dtBagConstraint.Table, alErrors_);
                if (bcConstraints == null)
                    return false;
            }
            return true;
        }
        private static int ColumnisValid(DataTable dtTable, String SColumnName, String ErrorNumber, List<String> alErrors)
        {
            int iIndex = dtTable.Columns.IndexOf(SColumnName);
            if (iIndex == -1)
            {
                alErrors.Add(ErrorNumber + " : Unable to locate the column \"" + SColumnName + "\" in The table \"" + dtTable.TableName + "\". Cannot continue allocation.");
            }
            return iIndex;
        }
        private static int RowisValid(DataTable dtTable, String SRowName, String ErrorNumber, List<String> alErrors)
        {
            int iIndex = OverallTools.DataFunctions.indexLigne(dtTable, 0, SRowName);
            if (iIndex == -1)
            {
                alErrors.Add(ErrorNumber + " : Unable to locate the row\"" + SRowName + "\" in The table \"" + dtTable.TableName + "\". Cannot continue allocation.");
            }
            return iIndex;
        }
        #endregion

        #region The OCT informations
        private Dictionary<String, Dictionary<String, OCTInformation>> GenerateOCTHashtable(out int iMinNbMakeUp, out double dMinOpeningTime)
        {
            iMinNbMakeUp = -1;
            dMinOpeningTime = -1;
            Dictionary<String, Dictionary<String, OCTInformation>> dsdsoiResult = new Dictionary<string, Dictionary<string, OCTInformation>>();
            if (dtOCTTable_ != null)
            {
                Dictionary<String, OCTInformation> dsoiTmp = GenerateOCTHashtable(dtOCTTable_.Table, out iMinNbMakeUp, out dMinOpeningTime);
                if (dsoiTmp == null)
                    return null;
                dsdsoiResult.Add("Normal", dsoiTmp);
            }
            if (!UseOCTTableExceptions)
                return dsdsoiResult;
            int iTmp;
            Double dTmp;
            if (dtOCTTable_.ExceptionFlightFB != null)
            {
                Dictionary<String, OCTInformation> dsoiTmp = GenerateOCTHashtable(dtOCTTable_.ExceptionFlightFB.Table, out iTmp, out dTmp);
                if (dsoiTmp == null)
                    return null;
                dsdsoiResult.Add(GlobalNames.Flight + GlobalNames.FirstAndBusiness, dsoiTmp);
                if (iTmp <= 0)
                    iTmp = 1;
                iMinNbMakeUp = Math.Min(iMinNbMakeUp, iTmp);
                dMinOpeningTime = Math.Min(dMinOpeningTime, dTmp);
            }
            if (dtOCTTable_.ExceptionFlight != null)
            {
                Dictionary<String, OCTInformation> dsoiTmp = GenerateOCTHashtable(dtOCTTable_.ExceptionFlight.Table, out iTmp, out dTmp);
                if (dsoiTmp == null)
                    return null;
                dsdsoiResult.Add(GlobalNames.Flight, dsoiTmp);
                if (iTmp <= 0)
                    iTmp = 1;
                iMinNbMakeUp = Math.Min(iMinNbMakeUp, iTmp);
                dMinOpeningTime = Math.Min(dMinOpeningTime, dTmp);
            }
            if (dtOCTTable_.ExceptionAirlineFB != null)
            {
                Dictionary<String, OCTInformation> dsoiTmp = GenerateOCTHashtable(dtOCTTable_.ExceptionAirlineFB.Table, out iTmp, out dTmp);
                if (dsoiTmp == null)
                    return null;
                dsdsoiResult.Add(GlobalNames.Airline + GlobalNames.FirstAndBusiness, dsoiTmp);
                if (iTmp <= 0)
                    iTmp = 1;
                iMinNbMakeUp = Math.Min(iMinNbMakeUp, iTmp);
                dMinOpeningTime = Math.Min(dMinOpeningTime, dTmp);
            }
            if (dtOCTTable_.ExceptionAirline != null)
            {
                Dictionary<String, OCTInformation> dsoiTmp = GenerateOCTHashtable(dtOCTTable_.ExceptionAirline.Table, out iTmp, out dTmp);
                if (dsoiTmp == null)
                    return null;
                dsdsoiResult.Add(GlobalNames.Airline, dsoiTmp);
                if (iTmp <= 0)
                    iTmp = 1;
                iMinNbMakeUp = Math.Min(iMinNbMakeUp, iTmp);
                dMinOpeningTime = Math.Min(dMinOpeningTime, dTmp);
            }
            if (dtOCTTable_.ExceptionFB != null)
            {
                Dictionary<String, OCTInformation> dsoiTmp = GenerateOCTHashtable(dtOCTTable_.ExceptionFB.Table, out iTmp, out dTmp);
                if (dsoiTmp == null)
                    return null;
                dsdsoiResult.Add(GlobalNames.FirstAndBusiness, dsoiTmp);
                if (iTmp <= 0)
                    iTmp = 1;
                iMinNbMakeUp = Math.Min(iMinNbMakeUp, iTmp);
                dMinOpeningTime = Math.Min(dMinOpeningTime, dTmp);
            }
            return dsdsoiResult;
        }

        private Dictionary<String, OCTInformation> GenerateOCTHashtable(DataTable dtOCTTableTMP, out int iMinNbMakeUp, out double dMinOpeningTime)
        {
            iMinNbMakeUp = -1;
            dMinOpeningTime = -1;
            Dictionary<String, OCTInformation> htResult = new Dictionary<String, OCTInformation>();
            try
            {
                for (int i = 1; i < dtOCTTableTMP.Columns.Count; i++)
                {
                    OCTInformation newObject = null;
                    Double dOpening = (Double)dtOCTTableTMP.Rows[iIndexOpeningOCTDesk][i];
                    Double dClosing = (Double)dtOCTTableTMP.Rows[iIndexClosingOCTDesk][i];
                    Double dNBContainerPerCycle = -1;
                    Double dDeadTime = -1;
                    Double dContainerSize = -1;
                    if (iIndexNBContainerPerCycle != -1)
                    {
                        dNBContainerPerCycle = (Double)dtOCTTableTMP.Rows[iIndexNBContainerPerCycle][i];
                        dDeadTime = (Double)dtOCTTableTMP.Rows[iIndexDeadTimeBetweenCycle][i];
                        dContainerSize = (Double)dtOCTTableTMP.Rows[iIndexContainerSize][i];
                    }
                    if (!bIsFPAFlight_)
                    {
                        dOpening = -dOpening;
                        dClosing = -dClosing;
                    }
                    if (iIndexNumberOCTDesk != -1)
                    {
                        int dUsed = (Int32)((Double)dtOCTTableTMP.Rows[iIndexNumberOCTDesk][i]);
                        if (iIndexPartialClosingOCTDesk != -1)
                        {
                            Double dPartialClose = (Double)dtOCTTableTMP.Rows[iIndexPartialClosingOCTDesk][i];
                            if (!bIsFPAFlight_)
                                dPartialClose = -dPartialClose;
                            int dPartialUsed = (Int32)((Double)dtOCTTableTMP.Rows[iIndexPartialNumberOCTDesk][i]);
                            newObject = new OCTInformation(dOpening, dClosing, dUsed, dPartialClose, dPartialUsed, i);
                        }
                        else
                        {
                            newObject = new OCTInformation(dOpening, dClosing, dUsed, i);
                        }
                    }
                    else
                    {
                        newObject = new OCTInformation(dOpening, dClosing, i);
                    }
                    newObject.dContainerSize = dContainerSize;
                    newObject.dDeadTime = dDeadTime;
                    newObject.iNbContainerPerCycle = (int)dNBContainerPerCycle;
                    htResult.Add(dtOCTTableTMP.Columns[i].ColumnName, newObject);
                    if ((iMinNbMakeUp == -1) || (newObject.NombrePostes < iMinNbMakeUp))
                        iMinNbMakeUp = newObject.NombrePostes;
                    if ((newObject.PartialNumber < iMinNbMakeUp) && (newObject.PartialNumber != 0))
                        iMinNbMakeUp = newObject.PartialNumber;
                    if ((dMinOpeningTime == -1) || newObject.Difference < dMinOpeningTime)
                        dMinOpeningTime = newObject.Difference;
                }
            }
            catch (Exception exc)
            {
                alErrors_.Add("Err00675 : A problem appears : " + exc.Message);
                OverallTools.ExternFunctions.PrintLogFile("Err00675: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                return null;
            }
            return htResult;
        }
        private static OCTInformation getInformation(Dictionary<String, Dictionary<String, OCTInformation>> dsdsoiTmp, int iClasse, String sFlight, String sAirline, String sFlightCategory)
        {
            if ((iClasse == 1) && (dsdsoiTmp.ContainsKey(GlobalNames.Flight + GlobalNames.FirstAndBusiness)))
            {
                if (dsdsoiTmp[GlobalNames.Flight + GlobalNames.FirstAndBusiness].ContainsKey(sFlight))
                    return dsdsoiTmp[GlobalNames.Flight + GlobalNames.FirstAndBusiness][sFlight];
            }
            if (dsdsoiTmp.ContainsKey(GlobalNames.Flight))
            {
                if (dsdsoiTmp[GlobalNames.Flight].ContainsKey(sFlight))
                    return dsdsoiTmp[GlobalNames.Flight][sFlight];
            }
            if ((iClasse == 1) && (dsdsoiTmp.ContainsKey(GlobalNames.Airline + GlobalNames.FirstAndBusiness)))
            {
                if (dsdsoiTmp[GlobalNames.Airline + GlobalNames.FirstAndBusiness].ContainsKey(sAirline))
                    return dsdsoiTmp[GlobalNames.Airline + GlobalNames.FirstAndBusiness][sAirline];
            }
            if (dsdsoiTmp.ContainsKey(GlobalNames.Airline))
            {
                if (dsdsoiTmp[GlobalNames.Airline].ContainsKey(sAirline))
                    return dsdsoiTmp[GlobalNames.Airline][sAirline];
            }
            if ((iClasse == 1) && (dsdsoiTmp.ContainsKey(GlobalNames.FirstAndBusiness)))
            {
                if (dsdsoiTmp[GlobalNames.FirstAndBusiness].ContainsKey(sFlightCategory))
                    return dsdsoiTmp[GlobalNames.FirstAndBusiness][sFlightCategory];
            }
            if (dsdsoiTmp["Normal"].ContainsKey(sFlightCategory))
                return dsdsoiTmp["Normal"][sFlightCategory];
            return null;
        }
        #endregion

        #region To get the Loose ULD information (if needed).
        private Dictionary<String, Dictionary<String, Dictionary<String, String>>> GenerateLooseULD()
        {
            if (dtAircraftTable_ == null)
                return null;
            Dictionary<String, Dictionary<String, Dictionary<String, String>>> htLooseULD = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>();

            Dictionary<String, Dictionary<String, String>> dssTmp = GenerateLooseULD(dtAircraftTable_.Table, true);
            if (dssTmp == null)
                return null;
            htLooseULD.Add("Normal", dssTmp);
            if (!UseAircraftTableExceptions)
                return htLooseULD;
            if (dtAircraftTable_.ExceptionFlightFB != null)
            {
                dssTmp = GenerateLooseULD(dtAircraftTable_.ExceptionFlightFB.Table, false);
                if (dssTmp == null)
                    return null;
                htLooseULD.Add(GlobalNames.Flight + GlobalNames.FirstAndBusiness, dssTmp);
            }
            if (dtAircraftTable_.ExceptionFlight != null)
            {
                dssTmp = GenerateLooseULD(dtAircraftTable_.ExceptionFlight.Table, false);
                if (dssTmp == null)
                    return null;
                htLooseULD.Add(GlobalNames.Flight, dssTmp);
            }
            if (dtAircraftTable_.ExceptionAirlineFB != null)
            {
                dssTmp = GenerateLooseULD(dtAircraftTable_.ExceptionAirlineFB.Table, false);
                if (dssTmp == null)
                    return null;
                htLooseULD.Add(GlobalNames.Airline + GlobalNames.FirstAndBusiness, dssTmp);
            }
            if (dtAircraftTable_.ExceptionAirline != null)
            {
                dssTmp = GenerateLooseULD(dtAircraftTable_.ExceptionAirline.Table, false);
                if (dssTmp == null)
                    return null;
                htLooseULD.Add(GlobalNames.Airline, dssTmp);
            }
            if (dtAircraftTable_.ExceptionFB != null)
            {
                dssTmp = GenerateLooseULD(dtAircraftTable_.ExceptionFB.Table, true);
                if (dssTmp == null)
                    return null;
                htLooseULD.Add(GlobalNames.FirstAndBusiness, dssTmp);
            }
            if (dtAircraftTable_.ExceptionFC != null)
            {
                dssTmp = GenerateLooseULD(dtAircraftTable_.ExceptionFC.Table, false);
                if (dssTmp == null)
                    return null;
                htLooseULD.Add(GlobalNames.FlightCategory, dssTmp);
            }
            if (dtAircraftTable_.ExceptionFCFB != null)
            {
                dssTmp = GenerateLooseULD(dtAircraftTable_.ExceptionFCFB.Table, false);
                if (dssTmp == null)
                    return null;
                htLooseULD.Add(GlobalNames.FlightCategory + GlobalNames.FirstAndBusiness, dssTmp);
            }
            return htLooseULD;
        }
        private Dictionary<String, Dictionary<String, String>> GenerateLooseULD(DataTable dtTableTmp, bool bRootTable)
        {
            Dictionary<String, Dictionary<String, String>> dssAircraft = new Dictionary<String, Dictionary<String, String>>();
            foreach (DataRow row in dtTableTmp.Rows)
            {
                String sSubDivision = row[0].ToString();
                if (bRootTable)
                    sSubDivision = "Normal";
                if (!dssAircraft.ContainsKey(sSubDivision))
                    dssAircraft.Add(sSubDivision, new Dictionary<string, string>());
                dssAircraft[sSubDivision].Add(row[sColumnAircraftAircraft].ToString(), row[sColumnAircraftULDLoose].ToString());
            }
            return dssAircraft;
        }

        private static String getInformation(Dictionary<String, Dictionary<String, Dictionary<String, String>>> dsdssTmp, int iClasse, String sFlight, String sAirline, String sFlightCategory, String sAircraft)
        {
            if ((iClasse == 1) && (dsdssTmp.ContainsKey(GlobalNames.Flight + GlobalNames.FirstAndBusiness)))
            {
                if (dsdssTmp[GlobalNames.Flight + GlobalNames.FirstAndBusiness].ContainsKey(sFlight))
                    if (dsdssTmp[GlobalNames.Flight + GlobalNames.FirstAndBusiness][sFlight].ContainsKey(sAircraft))
                        return dsdssTmp[GlobalNames.Flight + GlobalNames.FirstAndBusiness][sFlight][sAircraft];
            }
            if (dsdssTmp.ContainsKey(GlobalNames.Flight))
            {
                if (dsdssTmp[GlobalNames.Flight].ContainsKey(sFlight))
                    if (dsdssTmp[GlobalNames.Flight][sFlight].ContainsKey(sAircraft))
                        return dsdssTmp[GlobalNames.Flight][sFlight][sAircraft];
            }
            if ((iClasse == 1) && (dsdssTmp.ContainsKey(GlobalNames.Airline + GlobalNames.FirstAndBusiness)))
            {
                if (dsdssTmp[GlobalNames.Airline + GlobalNames.FirstAndBusiness].ContainsKey(sAirline))
                    if (dsdssTmp[GlobalNames.Airline + GlobalNames.FirstAndBusiness][sAirline].ContainsKey(sAircraft))
                        return dsdssTmp[GlobalNames.Airline + GlobalNames.FirstAndBusiness][sAirline][sAircraft];
            }
            if (dsdssTmp.ContainsKey(GlobalNames.Airline))
            {
                if (dsdssTmp[GlobalNames.Airline].ContainsKey(sAirline))
                    if (dsdssTmp[GlobalNames.Airline][sAirline].ContainsKey(sAircraft))
                        return dsdssTmp[GlobalNames.Airline][sAirline][sAircraft];
            }
            if ((iClasse == 1) && (dsdssTmp.ContainsKey(GlobalNames.FlightCategory + GlobalNames.FirstAndBusiness)))
            {
                if (dsdssTmp[GlobalNames.FlightCategory + GlobalNames.FirstAndBusiness].ContainsKey(sFlightCategory))
                    if (dsdssTmp[GlobalNames.FlightCategory + GlobalNames.FirstAndBusiness][sFlightCategory].ContainsKey(sAircraft))
                        return dsdssTmp[GlobalNames.FlightCategory + GlobalNames.FirstAndBusiness][sFlightCategory][sAircraft];
            }
            if (dsdssTmp.ContainsKey(GlobalNames.Airline))
            {
                if (dsdssTmp[GlobalNames.FlightCategory].ContainsKey(sFlightCategory))
                    if (dsdssTmp[GlobalNames.FlightCategory][sFlightCategory].ContainsKey(sAircraft))
                        return dsdssTmp[GlobalNames.FlightCategory][sFlightCategory][sAircraft];
            }
            if ((iClasse == 1) && (dsdssTmp.ContainsKey(GlobalNames.FirstAndBusiness)))
            {
                if (dsdssTmp[GlobalNames.FirstAndBusiness].ContainsKey("Normal"))
                    if (dsdssTmp[GlobalNames.FirstAndBusiness]["Normal"].ContainsKey(sAircraft))
                        return dsdssTmp[GlobalNames.FirstAndBusiness]["Normal"][sAircraft];
            }
            if (dsdssTmp["Normal"]["Normal"].ContainsKey(sAircraft))
                return dsdssTmp["Normal"]["Normal"][sAircraft];
            return null;
        }

        #endregion

        #region Main function to make the allocation

        // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
        ArrayList airlineCodes = new ArrayList();
        ArrayList flightCategories = new ArrayList();
        ArrayList groundHandlers = new ArrayList();

        private class HelperFlight
        {
            public String flightId;
            public String flightNumber;
            public String flightCategory;
            public String airlineCode;
            public String aircraftType;

            public double totalNbPassengers;
            public double totalNbBaggages;

            public Color currentColor;
            public Color handlingColor = Color.White;
            public Color airlineColor = Color.White;
        }
        private class HelperFlightPlanColumnIndexes
        {
            public int indexColumnID {get; set;}
            public int indexColumnDate { get; set; }
            public int indexColumnSTA { get; set; }
            public int indexColumnSTD { get; set; }
            public int indexColumnAirlineCode { get; set; }
            public int indexColumnFlightNb { get; set; }
            public int indexColumnAirportCode { get; set; }
            public int indexColumnFlightCategory { get; set; }
            public int indexColumnAircraftType { get; set; }
            public int indexColumnNoBSM { get; set; }
            public int indexColumnCBP { get; set; }
            public int indexColumnTSA { get; set; }
            public int indexColumnNbSeats { get; set; }
            public int indexColumnUser1 { get; set; }
            public int indexColumnUser2 { get; set; }
            public int indexColumnUser3 { get; set; }
            public int indexColumnUser4 { get; set; }
            public int indexColumnUser5 { get; set; }

            public int indexColumnCheckInEcoStart { get; set; }
            public int indexColumnCheckInEcoEnd { get; set; }
            public int indexColumnCheckInFBStart { get; set; }
            public int indexColumnCheckInFBEnd { get; set; }

            public int indexColumnBagDropEcoStart { get; set; }
            public int indexColumnBagDropEcoEnd { get; set; }
            public int indexColumnBagDropFBStart { get; set; }
            public int indexColumnBagDropFBEnd { get; set; }

            public int indexColumnMakeUpEcoStart { get; set; }
            public int indexColumnMakeUpEcoEnd { get; set; }
            public int indexColumnMakeUpFBStart { get; set; }
            public int indexColumnMakeUpFBEnd { get; set; }

            public int indexColumnBoardingGate { get; set; }
            public int indexColumnBaggageClaim { get; set; }
            public int indexColumnTransferInfeed { get; set; }
            
            public HelperFlightPlanColumnIndexes(DataTable flightPlan, DataTable allocationFlightPlan)
            {
                if (flightPlan == null || allocationFlightPlan == null)
                {
                    return;
                }

                indexColumnID = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                indexColumnDate = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                indexColumnSTA = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
                indexColumnSTD = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
                indexColumnAirlineCode = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                indexColumnFlightNb = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                indexColumnAirportCode = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                indexColumnFlightCategory = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                indexColumnAircraftType = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                indexColumnNoBSM = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
                indexColumnCBP = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
                indexColumnTSA = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
                indexColumnNbSeats = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
                indexColumnUser1 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                indexColumnUser2 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                indexColumnUser3 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                indexColumnUser4 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                indexColumnUser5 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);

                indexColumnCheckInEcoStart = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
                indexColumnCheckInEcoEnd = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
                indexColumnCheckInFBStart = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
                indexColumnCheckInFBEnd = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);

                indexColumnBagDropEcoStart = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
                indexColumnBagDropEcoEnd = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
                indexColumnBagDropFBStart = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
                indexColumnBagDropFBEnd = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);

                indexColumnMakeUpEcoStart = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
                indexColumnMakeUpEcoEnd = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
                indexColumnMakeUpFBStart = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
                indexColumnMakeUpFBEnd = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);

                indexColumnBoardingGate = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);

                indexColumnBaggageClaim = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
                indexColumnTransferInfeed = allocationFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);
            }
            public bool hasValidIndexes()
            {
                int value = -1;
                Type type = GetType();
                System.Reflection.PropertyInfo[] pi = type.GetProperties();
                foreach (var property in pi)
                {
                    if (!Int32.TryParse(property.GetValue(this, null).ToString(), out value) 
                        || value == -1)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private class HelperFlightPlanValues
        {
            public int flightID { get; set; }
            public int flightDate { get; set; }
            public int flightSTA { get; set; }
            public int flightSTD { get; set; }
            public int flightAirlineCode { get; set; }
            public int flightNb { get; set; }
            public int flightAirportCode { get; set; }
            public int flightCategory { get; set; }
            public int flightAircraftType { get; set; }
            public int flightNoBSM { get; set; }
            public int flightCBP { get; set; }
            public int flightTSA { get; set; }
            public int flightNbSeats { get; set; }
            public int flightUser1 { get; set; }
            public int flightUser2 { get; set; }
            public int flightUser3 { get; set; }
            public int flightUser4 { get; set; }
            public int flightUser5 { get; set; }

            public int flightCheckInEcoStart { get; set; }
            public int flightCheckInEcoEnd { get; set; }
            public int flightCheckInFBStart { get; set; }
            public int flightCheckInFBEnd { get; set; }

            public int flightBagDropEcoStart { get; set; }
            public int flightBagDropEcoEnd { get; set; }
            public int flightBagDropFBStart { get; set; }
            public int flightBagDropFBEnd { get; set; }

            public int flightMakeUpEcoStart { get; set; }
            public int flightMakeUpEcoEnd { get; set; }
            public int flightMakeUpFBStart { get; set; }
            public int flightMakeUpFBEnd { get; set; }

            public int flightBoardingGate { get; set; }
            public int flightBaggageClaim { get; set; }
            public int flightTransferInfeed { get; set; }

            public HelperFlightPlanValues(DataRow flightPlanRow, HelperFlightPlanColumnIndexes helperFlightPlanIndexes)
            {

            }
        }
        String allocationColumnName;
        
        int nbOfVisualizations;
        int handlingColorIndex;
        int airlineColorIndex;

        Double timeBetweenFlights = -1;
        private int minNbOfResources = -1;
        private Double minOpeningTimeInMinutes = -1;
        private Dictionary<String, Dictionary<String, OCTInformation>> octDictionary;
        Dictionary<String, Dictionary<String, Dictionary<String, String>>> uldLooseDictionary;
        List<Dictionary<int, SegregationForOverlap>> timeSlotIndexAndSegregationForOverlapping;
        Dictionary<String, SegregationForOverlap> flightCategoryOverlappingDictionary;

        public Boolean allocateDesks()
        {
            if (!CheckInputTable())
            {
                return false;
            }            
            if (bColorByHandler_)
            {
                groundHandlers = getGroundHandlersFromAirlineTable(dtAirlineTable_);
            }
            nbOfVisualizations = getNbOfVisualizations();
            handlingColorIndex = getHandlingColorIndex();
            airlineColorIndex = getAirlineColorIndex();

            initializeVisualizationMode(nbOfVisualizations, handlingColorIndex, airlineColorIndex);
            
            dtResults[0] = createAndInitializeAllocationTable(sNomColonneAllocation_);
            timeBetweenFlights = getNbOfIntervalsBetweenTwoFlights();

            initializeOpeningClosingTimesHashtable();
            minNbOfResources = 1;
            if (octDictionary == null)
            {
                return false;
            }
            allocationColumnName = sNomColonneAllocation_ + "_";
            flightCategories = getFlightCategoriesFromOCTExcTable(dtOCTTable_);
            airlineCodes = getSortedAirlineCodesListFromFlightPlan(dtFPTable_, iIndexAirline);

            timeSlotIndexAndSegregationForOverlapping = initializeTimeSlotIndexAndSegregationForOverlapListOfDictionaries(lsfoOverlapParameters, dtResults[0]);
            flightCategoryOverlappingDictionary = getFlightCategoryOverlapDictionary(lsfoOverlapParameters, dtResults[0], flightCategories);

            bool isDepartureTypeAllocation = isDepartureAllocation(AllocationType);
            uldLooseDictionary = GenerateLooseULD();

            if (bUseFPContent_)
            {
                allocateDesksUsingFPContent();
            }

            return true;
        }

        private ArrayList getGroundHandlersFromAirlineTable(DataTable airlineTable)
        {
            ArrayList groundHandlers = new ArrayList();
            if (airlineTable == null)
            {
                alErrors_.Add("Warn00669 : The airline table for the allocation is empty, the calc for the handling agent will not be done.");
                return groundHandlers;
            }
            if (iIndexAirlineGroundHandlers == -1)
            {
                alErrors_.Add("Warn00669.1 : The airline table for the allocation does not have a ground handler column. The calculation for the handling agent will not be done.");
                return groundHandlers;
            }
            foreach (DataRow airline in airlineTable.Rows)
            {
                if (airline[iIndexAirlineGroundHandlers] != null)
                {
                    String airlineGroundHandler = airline[iIndexAirlineGroundHandlers].ToString();
                    if (!groundHandlers.Contains(airlineGroundHandler))
                    {
                        groundHandlers.Add(airlineGroundHandler);
                    }
                }
            }
            groundHandlers.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
            return groundHandlers;
        }

        private int getNbOfVisualizations()
        {
            int nbOfVisualizations = 0;
            if (bColorByHandler_)
            {
                nbOfVisualizations++;             
            }
            if (bColorByAirline_)
            {
                nbOfVisualizations++;             
            }            
            return nbOfVisualizations;
        }
        
        private int getHandlingColorIndex()
        {
            int handlingColorIndex = -1;
            int helperIndex = 0;
            if (bColorByFC_)
            {
                helperIndex++;                
            }            
            if (bColorByHandler_)
            {
                handlingColorIndex = helperIndex;
            }
            return handlingColorIndex;
        }
        
        private int getAirlineColorIndex()
        {
            int airlineColorIndex = -1;
            int helperIndex = 0;
            if (bColorByFC_)
            {
                helperIndex++;                
            }            
            if (bColorByHandler_)
            {                
                helperIndex++;                
            }
            if (bColorByAirline_)
            {
                airlineColorIndex = helperIndex;                   
            }
            return airlineColorIndex;
        }
        
        private void initializeVisualizationMode(int nbOfVisualizations, int handlingColorIndex, int airlineColorIndex)
        {
            if (nbOfVisualizations > 0)
            {
                tvmAllocationVisualisation_ = new VisualisationMode[nbOfVisualizations];
            }
            else
            {
                tvmAllocationVisualisation_ = new VisualisationMode[1];
                tvmAllocationVisualisation_[0] = new VisualisationMode(false, false, false, null, new int[] { 0 });
            }
            if (bColorByFC_)
            {
                tvmAllocationVisualisation_[0] = new VisualisationMode(false, false, false, null, new int[] { 0 });
                tvmAllocationVisualisation_[0].ShowRowHeader = true;
                tvmAllocationVisualisation_[0].FirstColumnInHeader = true;
                ConditionnalFormatErrors cfeErrors = new ConditionnalFormatErrors();
                tvmAllocationVisualisation_[0].ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfeErrors };
            }
            if (bColorByHandler_)
            {
                tvmAllocationVisualisation_[handlingColorIndex] = new VisualisationMode(false, false, false, null, new int[] { 0 });
                tvmAllocationVisualisation_[handlingColorIndex].ShowRowHeader = true;
                tvmAllocationVisualisation_[handlingColorIndex].FirstColumnInHeader = true;
                ConditionnalFormatErrors cfeErrors = new ConditionnalFormatErrors();
                tvmAllocationVisualisation_[handlingColorIndex].ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfeErrors };
            }
            if (bColorByAirline_)
            {
                tvmAllocationVisualisation_[airlineColorIndex] = new VisualisationMode(false, false, false, null, new int[] { 0 });
                tvmAllocationVisualisation_[airlineColorIndex].ShowRowHeader = true;
                tvmAllocationVisualisation_[airlineColorIndex].FirstColumnInHeader = true;
                ConditionnalFormatErrors cfeErrors = new ConditionnalFormatErrors();
                tvmAllocationVisualisation_[airlineColorIndex].ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfeErrors };
            }
        }

        private DataTable createAndInitializeAllocationTable(String allocationName)
        {
            String sColumnsName = allocationName + "_";
            DataTable allocationTable = new DataTable(/*sNomTable_*/"Allocation");
            allocationTable.Columns.Add("Time", typeof(DateTime));
            allocationTable.Columns.Add("Legend", typeof(String));
            OverallTools.DataFunctions.initialiserLignes(allocationTable, dtBegin_, dtEnd_, dStep_);
            //We add some globals columns that will be used to know the global occupation of the desks. (We add a dumb column to 
            //deal with the fact that the function AddOccupationDesksColumns will remove the lasts columns if there is no new
            //columns added to the table.
            AddOccupationDesksColumns(allocationTable, sGlobal + "_", ShowFlightNumber);
            allocationTable.Columns.Add("Dumb", typeof(String));
            AddOccupationDesksColumns(allocationTable, sColumnsName, ShowFlightNumber);
            allocationTable.Columns.Remove("Dumb");
            for (int i = 0; i < allocationTable.Rows.Count; i++)
            {
                allocationTable.Rows[i][1] = "";
            }
            return allocationTable;
        }

        private Double getNbOfIntervalsBetweenTwoFlights()
        {
            Double nbIntervalsBetweenTwoFlights = -1;
            if (dStep_ > 0)
            {
                nbIntervalsBetweenTwoFlights = Math.Ceiling(dTimeBetweenFlights_ / dStep_);
            }
            return nbIntervalsBetweenTwoFlights;
        }

        private void initializeOpeningClosingTimesHashtable()
        {
            octDictionary = GenerateOCTHashtable(out minNbOfResources, out minOpeningTimeInMinutes);
        }

        private ArrayList getFlightCategoriesFromOCTExcTable(ExceptionTable octExcTable)
        {
            ArrayList flightCategories = new ArrayList();
            if (octExcTable == null)
            {
                return flightCategories;
            }
            for (int i = 1; i < octExcTable.Table.Columns.Count; i++)
            {
                flightCategories.Add(octExcTable.Table.Columns[i].ColumnName);
            }
            return flightCategories;
        }

        private ArrayList getSortedAirlineCodesListFromFlightPlan(DataTable flightPlan, int airlineColumnIndex)
        {
            ArrayList airlineCodes = new ArrayList();
            if (flightPlan == null || airlineColumnIndex < 0)
            {
                return airlineCodes;
            }
            foreach (DataRow Line in flightPlan.Rows)
            {
                if (!airlineCodes.Contains(Line[airlineColumnIndex].ToString()))
                {
                    airlineCodes.Add(Line[airlineColumnIndex].ToString());
                }
            }
            airlineCodes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
            return airlineCodes;
        }

        private List<Dictionary<int, SegregationForOverlap>> initializeTimeSlotIndexAndSegregationForOverlapListOfDictionaries(
            List<SegregationForOverlap> segregationForOverlapList, DataTable allocationTable)
        {
            List<Dictionary<int, SegregationForOverlap>> timeSlotIndexAndSegregationForOverlappingDictionaries = null;
            if (allocationTable == null)
            {
                return timeSlotIndexAndSegregationForOverlappingDictionaries;
            }
            if (segregationForOverlapList != null && segregationForOverlapList.Count != 0
                && useOverlapping(segregationForOverlapList))
            {
                timeSlotIndexAndSegregationForOverlappingDictionaries = new List<Dictionary<int, SegregationForOverlap>>();
                for (int i = 0; i < allocationTable.Rows.Count; i++)
                {
                    timeSlotIndexAndSegregationForOverlappingDictionaries.Add(new Dictionary<int, SegregationForOverlap>());
                }
            }
            return timeSlotIndexAndSegregationForOverlappingDictionaries;
        }

        private Dictionary<String, SegregationForOverlap> getFlightCategoryOverlapDictionary(
            List<SegregationForOverlap> segregationForOverlapList, DataTable allocationTable, ArrayList flightCategories)
        {
            Dictionary<String, SegregationForOverlap> flightCategoryOverlappingDictionary = null;

            if (segregationForOverlapList != null && segregationForOverlapList.Count != 0
                && useOverlapping(segregationForOverlapList))
            {                    
                foreach (String flightCategory in flightCategories)
                {
                    bool bAdded = false;
                    foreach (SegregationForOverlap segregation in segregationForOverlapList)
                    {
                        if (segregation.Contains(flightCategory))
                        {
                            flightCategoryOverlappingDictionary.Add(flightCategory, segregation);
                            bAdded = true;
                            break;
                        }
                    }
                    if (!bAdded)
                    {
                        List<String> lsTmp = new List<string>();
                        lsTmp.Add(flightCategory);
                        SegregationForOverlap tmpSegregation = new SegregationForOverlap("", lsTmp, 1);
                        flightCategoryOverlappingDictionary.Add(flightCategory, tmpSegregation);
                    }
                }
            }            
            return flightCategoryOverlappingDictionary;
        }
        
        private bool useOverlapping(List<SegregationForOverlap> segregationForOverlapList)
        {
            foreach (SegregationForOverlap segregation in segregationForOverlapList)
            {
                if (segregation.MaxOverlap > 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool isDepartureAllocation(TypeAllocation allocationType)
        {
            return departureAllocations.Contains(allocationType);
        }

        private bool allocateDesksUsingFPContent()
        {
            bool isDeparture = isDepartureAllocation(AllocationType);
            DataTable sortedFlightPlan = SortFlightPlan(octDictionary, uldLooseDictionary,
                new ArrayList(), "", "", true, isDeparture);
            int expectedNbOfFlights = sortedFlightPlan.Rows.Count;
            int iOffsetDesks = dtResults[0].Columns.Count;
                        
            HelperFlight currentFlight;
            HelperFlightPlanColumnIndexes flightPlanColumnIndexes = new HelperFlightPlanColumnIndexes(dtFPTable_, sortedFlightPlan);

            int nbOfAllocatedFlights = 0;
            foreach (DataRow drRow in sortedFlightPlan.Rows)
            {
                currentFlight = new HelperFlight();

                String flightIdPrefix = "A_";
                if (isDeparture)
                {
                    flightIdPrefix = "D_";
                }
                currentFlight.flightId = flightIdPrefix + drRow[iIndexID];
                currentFlight.flightCategory = drRow[iIndexFlightCategory].ToString();
                currentFlight.airlineCode = drRow[iIndexAirline].ToString();
                currentFlight.aircraftType = drRow[iIndexAircraft].ToString();
                
                OCTInformation octEcoInformation = getInformation(octDictionary, 0, currentFlight.flightId, currentFlight.airlineCode, currentFlight.flightCategory);
                OCTInformation octFBInformation = getInformation(octDictionary, 1, currentFlight.flightId, currentFlight.airlineCode, currentFlight.flightCategory);
                if (octEcoInformation == null || octFBInformation == null)
                {
                    continue;
                }

                int firstEcoDeskIndex = getDeskIndexFromFlightPlanRow(drRow, iIndexFirstColumnResult);  //i
                int lastEcoDeskIndex = getDeskIndexFromFlightPlanRow(drRow, iIndexLastColumnResult);    //k
                int firstFBDeskIndex = getDeskIndexFromFlightPlanRow(drRow, iIndexFirstColumnResultFirst);  //m
                int lastFBDeskIndex = getDeskIndexFromFlightPlanRow(drRow, iIndexLastColumnResultFirst);    //n

                if (firstEcoDeskIndex <= 0 || lastEcoDeskIndex <= 0)
                {
                    continue;
                }
                if (firstFBDeskIndex > 0 && lastFBDeskIndex <= 0)
                {
                    continue;
                }

                firstEcoDeskIndex = adjustDeskIndexToSkipNonAllocationColumns(firstEcoDeskIndex, iOffsetDesks);
                lastEcoDeskIndex = adjustDeskIndexToSkipNonAllocationColumns(lastEcoDeskIndex, iOffsetDesks);

                if (firstFBDeskIndex > 0)
                {
                    firstFBDeskIndex = adjustDeskIndexToSkipNonAllocationColumns(firstFBDeskIndex, iOffsetDesks);
                    lastFBDeskIndex = adjustDeskIndexToSkipNonAllocationColumns(lastFBDeskIndex, iOffsetDesks);
                }

                currentFlight.currentColor = OverallTools.FonctionUtiles.getColor(octEcoInformation.iIndexColor);                
                if (bColorByAirline_)
                {
                    currentFlight.airlineColor = OverallTools.FonctionUtiles.getColor(airlineCodes.IndexOf(currentFlight.airlineCode));
                }
                if (bColorByHandler_)
                {
                    String handlingAgentCode = OverallTools.DataFunctions.getValue(dtAirlineTable_, currentFlight.airlineCode,
                                                                                    iIndexAirlineAirline, iIndexAirlineGroundHandlers);
                    if (handlingAgentCode == null || handlingAgentCode == "")
                    {
                        currentFlight.handlingColor = OverallTools.FonctionUtiles.getColor(0);
                    }
                    else
                    {
                        currentFlight.handlingColor = OverallTools.FonctionUtiles.getColor(groundHandlers.IndexOf(handlingAgentCode));
                    }
                }

                currentFlight.flightNumber = drRow[iIndexFLIGHTN].ToString().Trim();
                DateTime deskOpeningDate = (DateTime)drRow[sOpeningDeskColumn]; //dateDebutAlloc
                DateTime deskClosingDate = (DateTime)drRow[sClosingDeskColumn]; //dateFinAlloc
                double deskOpeningTimeInMinutes = OverallTools.DataFunctions.MinuteDifference(dtBegin_, deskOpeningDate); //dTimeDebutAlloc
                double deskClosingTimeInMinutes = OverallTools.DataFunctions.MinuteDifference(dtBegin_, deskClosingDate); //dTimeFinAlloc

                int firstAllocatedPosition = (int)(deskOpeningTimeInMinutes / dStep_);      //iPosDebutAlloc
                int lastAllocatedPosition = (int)(deskClosingTimeInMinutes / dStep_);       //iPosFinAlloc
                if ((Double)lastAllocatedPosition == deskClosingTimeInMinutes / dStep_)
                {
                    lastAllocatedPosition--;
                }
                if (lastAllocatedPosition < 0 || firstAllocatedPosition > dtResults[0].Rows.Count)
                {
                    continue;
                }
                int firstFreePosition = firstAllocatedPosition - (int)timeBetweenFlights;   //iPosDebutLibre
                int lastFreePosition = lastAllocatedPosition + (int)timeBetweenFlights;     //iPosFinLibre

                if (firstFreePosition < 0)
                {
                    firstFreePosition = 0;
                }
                if (firstAllocatedPosition < 0)
                {
                    firstAllocatedPosition = 0;
                }
                if (lastAllocatedPosition >= dtResults[0].Rows.Count)
                {
                    lastAllocatedPosition = dtResults[0].Rows.Count - 1;
                }
                if (lastFreePosition >= dtResults[0].Rows.Count)
                {
                    lastFreePosition = dtResults[0].Rows.Count - 1;
                }
                int lastAllocatedPositionForDoubleInfeed = 0;//iPosFinAllocDoubleInfeed

                currentFlight.totalNbPassengers = getNumberOfPassengers(drRow);
                currentFlight.totalNbBaggages = getNumberOfBaggages(currentFlight, drRow);

                if (useInfeedLimitation && iIndexContainerSize != -1)
                {
                    deskClosingDate = (DateTime)drRow[sOpeningDeskColumn];
                    deskClosingDate = adjustDeskClosingDateForInfeedUsage(currentFlight,deskClosingDate);
                    lastAllocatedPositionForDoubleInfeed 
                        = calculateLastAllocatedPositionForInfeedUsage(currentFlight, deskClosingDate, lastAllocatedPositionForDoubleInfeed);
                }
                
                #region (In the case of the reclaim allocation.)
                int lastFreePositionForDoubleInfeed = 0;//iPosFinLibreDoubleInfeed
                if (useInfeedLimitation && iIndexContainerSize != -1)
                {
                    lastFreePositionForDoubleInfeed = lastAllocatedPositionForDoubleInfeed + (int)timeBetweenFlights;
                    if (lastAllocatedPositionForDoubleInfeed >= dtResults[0].Rows.Count)
                        lastAllocatedPositionForDoubleInfeed = dtResults[0].Rows.Count - 1;
                    if (lastFreePositionForDoubleInfeed >= dtResults[0].Rows.Count)
                        lastFreePositionForDoubleInfeed = dtResults[0].Rows.Count - 1;
                }
                #endregion                  
                
                nbOfAllocatedFlights++; //iAllocatedFlight
                for (int h = firstAllocatedPosition; h <= lastAllocatedPosition; h++)
                {
                    while (dtResults[0].Columns.Count <= h)///Création des colonnes manquantes dans la table de résultat.
                    {
                        dtResults[0].Columns.Add(allocationColumnName + (dtResults[0].Columns.Count - iOffsetDesks + 1).ToString(), typeof(String));
                    }
                    #region (In the case of the reclaim allocation.)
                    int finalAllocatedPosition = lastAllocatedPositionForDoubleInfeed;
                    int finalFreePosition = lastFreePosition;
                    if (useInfeedLimitation && iIndexContainerSize != -1)
                    {
                        finalAllocatedPosition = lastAllocatedPositionForDoubleInfeed;
                        finalFreePosition = lastFreePositionForDoubleInfeed;
                    }
                    #endregion
                    for (int j = firstAllocatedPosition; j <= finalAllocatedPosition; j++)
                    {
                        if (useOverlapping(lsfoOverlapParameters))
                        {
                            if (!timeSlotIndexAndSegregationForOverlapping[j].ContainsKey(h))
                            {
                                timeSlotIndexAndSegregationForOverlapping[j].Add(h, flightCategoryOverlappingDictionary[currentFlight.flightCategory]);
                            }
                        }

                        String currentAllocatedCellText = dtResults[0].Rows[j][h].ToString();
                        if (currentAllocatedCellText.Length > 0)
                        {
                            currentAllocatedCellText += ", ";
                        }
                        currentAllocatedCellText += currentFlight.flightNumber;
                        dtResults[0].Rows[j][h] = currentAllocatedCellText;
                        if (bColorByFC_)
                            ((ConditionnalFormatErrors)tvmAllocationVisualisation_[0].ConditionnalFormatClass[0]).setCondition(h, j, currentFlight.currentColor);
                        if (bColorByHandler_)
                            ((ConditionnalFormatErrors)tvmAllocationVisualisation_[handlingColorIndex].ConditionnalFormatClass[0]).setCondition(h, j, currentFlight.handlingColor);
                        if (bColorByAirline_)
                            ((ConditionnalFormatErrors)tvmAllocationVisualisation_[airlineColorIndex].ConditionnalFormatClass[0]).setCondition(h, j, currentFlight.airlineColor);
                    }
                    if (h == lastEcoDeskIndex && firstFBDeskIndex != -1 && lastFBDeskIndex != lastEcoDeskIndex)
                    {
                        /*Allocation of the fisrt and business desks*/
                        h = firstFBDeskIndex - 1; //i = m - 1;
                        firstEcoDeskIndex = firstFBDeskIndex - 1;//i = m - 1;
                        lastEcoDeskIndex = lastFBDeskIndex;//k = n;
                    }
                }                    
                /*
                gatherDataForFlightPlanInformationTable();   

                #region We calculate the result of the already allocated flight.
                SegregationAllocationInformation saiTmp = new SegregationAllocationInformation(sNomColonneAllocation_, expectedNbOfFlights, nbOfAllocatedFlights, dtResults[0].Columns.Count - iOffsetDesks);
                lsaiInformation.Add(sNomColonneAllocation_, saiTmp);
                AnalyseTableOccupation(dtResults[0], iOffsetDesks, iIndexOccupied, iIndexNeeded, iIndexFlightNumber, saiTmp);
                #endregion
                 */ 
            }
            return true;
        }

        private double getNumberOfPassengers(DataRow flightPlanRow)
        {
            double nbOfPassengers = OverallTools.DataFunctions.numberOfPassengers(flightPlanRow, dtAircraftTable_, null, dtLoadFactors, alErrors_);
            if (iIndexFLIGHTN != -1 && flightPlanRow[iIndexFLIGHTN] != null)
            {
                if (nbOfPassengers == -1)
                {
                    alErrors_.Add("Err00681 : Flight ignored : \"" + flightPlanRow[iIndexFLIGHTN].ToString() + "\". The number of passenger for that flight is not valid.");
                }
            }
            return nbOfPassengers;
        }

        private double getNumberOfBaggages(HelperFlight flight, DataRow flightPlanRow)
        {
            double totalNumberOfBags = 0;

            if (dtAircraftTable_ != null
               && dtLoadFactors != null && dtNbBagage != null)
            {
                int iColumn;
                dtNbBagage.UseException = UseNbBaggageExceptions;
                DataTable dtTableBags = dtNbBagage.GetInformationsColumns(0, flight.flightId, flight.airlineCode, flight.flightCategory, out iColumn);

                if (dtTableBags != null)
                {
                    Double dMeanBaggage = OverallTools.StaticAnalysis.CalculCoefficientMultiplicateur(dtTableBags, iColumn);
                    if (dMeanBaggage != -1)
                    {
                        dtLoadFactors.UseException = UseLoadFactorExceptions;
                        dtAircraftTable_.UseException = UseAircraftTableExceptions;
                        totalNumberOfBags = Math.Round(flight.totalNbPassengers * dMeanBaggage, 2);
                    }
                }
            }
            return totalNumberOfBags;
        }

        private DateTime adjustDeskClosingDateForInfeedUsage(HelperFlight flight, DateTime deskClosingDate)
        {            
            if (useInfeedLimitation && iIndexContainerSize != -1)
            {
                OCTInformation octEcoInformation = getInformation(octDictionary, 0, flight.flightId, flight.airlineCode, flight.flightCategory);
                int nbOfContainers = (int)Math.Ceiling(flight.totalNbBaggages / octEcoInformation.dContainerSize);
                int nbOfCycles = (int)Math.Ceiling((Double)nbOfContainers / (double)octEcoInformation.iNbContainerPerCycle);
                Double waitingTimeForCycleInMinutes = (Double)(nbOfCycles - 1) * octEcoInformation.dDeadTime;
                                
                deskClosingDate = deskClosingDate.AddMinutes(waitingTimeForCycleInMinutes);
                DateTime dateFinAllocDoubleInfeed = deskClosingDate.AddMinutes(dInfeedSpeed * flight.totalNbBaggages * 2);
                deskClosingDate = deskClosingDate.AddMinutes(dInfeedSpeed * flight.totalNbBaggages);                
            }
            return deskClosingDate;
        }

        private int calculateLastAllocatedPositionForInfeedUsage(HelperFlight flight, DateTime deskClosingDate, int lastAllocatedPositionForDoubleInfeed)
        {
            if (useInfeedLimitation && iIndexContainerSize != -1)
            {
                DateTime dateFinAllocDoubleInfeed = deskClosingDate.AddMinutes(dInfeedSpeed * flight.totalNbBaggages * 2);                
                Double dTimeFinAllocDoubleInfeed = OverallTools.DataFunctions.MinuteDifference(dtBegin_, dateFinAllocDoubleInfeed);
                lastAllocatedPositionForDoubleInfeed = (int)(dTimeFinAllocDoubleInfeed / dStep_);
                if (((Double)lastAllocatedPositionForDoubleInfeed) == dTimeFinAllocDoubleInfeed / dStep_)
                {
                    lastAllocatedPositionForDoubleInfeed--;
                }
            }
            return lastAllocatedPositionForDoubleInfeed;
        }
                
        private int getDeskIndexFromFlightPlanRow(DataRow flightPlanRow, int columnIndex)
        {
            int deskIndex = -1;
            if (flightPlanRow != null && columnIndex != -1
                && Int32.TryParse(flightPlanRow[columnIndex].ToString(), out deskIndex))
            {
                return deskIndex;
            }
            return deskIndex;   
        }

        private int adjustDeskIndexToSkipNonAllocationColumns(int deskIndex, int offset)
        {
            return deskIndex + offset - 1;
        }
        /*
        private void gatherDataForFlightPlanInformationTable(DataTable flightPlan)
        {
             
                    // add the information about the allocated flight in the allocFPInfoDictionary dictionary
                    // the dictionary will be used to insert the informations into the Flight Plan Information tabel
                    
                    int indexColumnCheckInEcoStart = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
                    int indexColumnCheckInEcoEnd = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
                    int indexColumnCheckInFBStart = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
                    int indexColumnCheckInFBEnd = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);

                    int indexColumnBagDropEcoStart = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
                    int indexColumnBagDropEcoEnd = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
                    int indexColumnBagDropFBStart = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
                    int indexColumnBagDropFBEnd = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);

                    int indexColumnMakeUpEcoStart = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
                    int indexColumnMakeUpEcoEnd = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
                    int indexColumnMakeUpFBStart = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
                    int indexColumnMakeUpFBEnd = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);

                    int indexColumnBoardingGate = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);

                    int indexColumnBaggageClaim = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
                    int indexColumnTransferInfeed = sortedFlightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);
                    
                    int checkInEcoStartDesk = -1;
                    int checkInEcoEndDesk = -1;
                    int checkInFBStartDesk = -1;
                    int checkInFBEndDesk = -1;

                    int baggDropEcoStartDesk = -1;
                    int baggDropEcoEndDesk = -1;
                    int baggDropFBStartDesk = -1;
                    int baggDropFBEndDesk = -1;

                    int makeUpEcoStartDesk = -1;
                    int makeUpEcoEndDesk = -1;
                    int makeUpFBStartDesk = -1;
                    int makeUpFBEndDesk = -1;

                    int boardGateDesk = -1;

                    int baggClaimDesk = -1;
                    int transferInfeedDesk = -1;

                    if (indexColumnCheckInEcoStart != -1)
                        checkInEcoStartDesk = int.Parse(drRow[indexColumnCheckInEcoStart].ToString());
                    if (indexColumnCheckInEcoEnd != -1)
                        checkInEcoEndDesk = int.Parse(drRow[indexColumnCheckInEcoEnd].ToString());
                    if (indexColumnCheckInFBStart != -1)
                        checkInFBStartDesk = int.Parse(drRow[indexColumnCheckInFBStart].ToString());
                    if (indexColumnCheckInFBEnd != -1)
                        checkInFBEndDesk = int.Parse(drRow[indexColumnCheckInFBEnd].ToString());

                    if (indexColumnBagDropEcoStart != -1)
                        baggDropEcoStartDesk = int.Parse(drRow[indexColumnBagDropEcoStart].ToString());
                    if (indexColumnBagDropEcoEnd != -1)
                        baggDropEcoEndDesk = int.Parse(drRow[indexColumnBagDropEcoEnd].ToString());
                    if (indexColumnBagDropFBStart != -1)
                        baggDropFBStartDesk = int.Parse(drRow[indexColumnBagDropFBStart].ToString());
                    if (indexColumnBagDropFBEnd != -1)
                        baggDropFBEndDesk = int.Parse(drRow[indexColumnBagDropFBEnd].ToString());

                    if (indexColumnMakeUpEcoStart != -1)
                        makeUpEcoStartDesk = int.Parse(drRow[indexColumnMakeUpEcoStart].ToString());
                    if (indexColumnMakeUpEcoEnd != -1)
                        makeUpEcoEndDesk = int.Parse(drRow[indexColumnMakeUpEcoEnd].ToString());
                    if (indexColumnMakeUpFBStart != -1)
                        makeUpFBStartDesk = int.Parse(drRow[indexColumnMakeUpFBStart].ToString());
                    if (indexColumnMakeUpFBEnd != -1)
                        makeUpFBEndDesk = int.Parse(drRow[indexColumnMakeUpFBEnd].ToString());

                    if (indexColumnBoardingGate != -1)
                        boardGateDesk = int.Parse(drRow[indexColumnBoardingGate].ToString());

                    if (indexColumnBaggageClaim != -1)
                        baggClaimDesk = int.Parse(drRow[indexColumnBaggageClaim].ToString());
                    if (indexColumnTransferInfeed != -1)
                        transferInfeedDesk = int.Parse(drRow[indexColumnTransferInfeed].ToString());
                   

                   
                    int indexColumnID = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                    int indexColumnDate = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                    int indexColumnSTA = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
                    int indexColumnSTD = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
                    int indexColumnAirlineCode = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                    int indexColumnFlightNb = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                    int indexColumnAirportCode = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                    int indexColumnFlightCategory = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                    int indexColumnAircraftType = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                    int indexColumnNoBSM = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
                    int indexColumnCBP = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
                    int indexColumnTSA = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
                    int indexColumnNbSeats = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
                    int indexColumnUser1 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                    int indexColumnUser2 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                    int indexColumnUser3 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                    int indexColumnUser4 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                    int indexColumnUser5 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);

                    int flightId = -1;
                    DateTime date = new DateTime();
                    TimeSpan sta_d = new TimeSpan();
                    String airlineCode = "";
                    String flightNb = "";
                    String airportCode = "";
                    String flightCategory = "";
                    String aircraftType = "";
                    Boolean noBSM = false;
                    Boolean CBP = false;
                    Boolean TSA = false;
                    int nbSeats = -1;
                    String user1 = "";
                    String user2 = "";
                    String user3 = "";
                    String user4 = "";
                    String user5 = "";

                    if (indexColumnID != -1)
                        flightId = int.Parse(drRow[indexColumnID].ToString());
                    if (indexColumnDate != -1)
                        date = DateTime.Parse(drRow[indexColumnDate].ToString());
                    if (indexColumnSTA != -1)
                        sta_d = TimeSpan.Parse(drRow[indexColumnSTA].ToString());
                    else if (indexColumnSTD != -1)
                        sta_d = TimeSpan.Parse(drRow[indexColumnSTD].ToString());
                    if (indexColumnAirlineCode != -1)
                        airlineCode = drRow[indexColumnAirlineCode].ToString();
                    if (indexColumnFlightNb != -1)
                        flightNb = drRow[indexColumnFlightNb].ToString();
                    if (indexColumnAirportCode != -1)
                        airportCode = drRow[indexColumnAirportCode].ToString();
                    if (indexColumnFlightCategory != -1)
                        flightCategory = drRow[indexColumnFlightCategory].ToString();
                    if (indexColumnAircraftType != -1)
                        aircraftType = drRow[indexColumnAircraftType].ToString();
                    if (indexColumnNoBSM != -1)
                        noBSM = bool.Parse(drRow[indexColumnNoBSM].ToString());
                    if (indexColumnCBP != -1)
                        CBP = bool.Parse(drRow[indexColumnCBP].ToString());
                    if (indexColumnTSA != -1)
                        TSA = bool.Parse(drRow[indexColumnTSA].ToString());
                    if (indexColumnNbSeats != -1)
                        nbSeats = int.Parse(drRow[indexColumnNbSeats].ToString());
                    if (indexColumnUser1 != -1)
                        user1 = drRow[indexColumnUser1].ToString();
                    if (indexColumnUser1 != -1)
                        user2 = drRow[indexColumnUser2].ToString();
                    if (indexColumnUser1 != -1)
                        user3 = drRow[indexColumnUser3].ToString();
                    if (indexColumnUser1 != -1)
                        user4 = drRow[indexColumnUser4].ToString();
                    if (indexColumnUser1 != -1)
                        user5 = drRow[indexColumnUser5].ToString();
                   

                    String groundHandler = OverallTools.DataFunctions.getValue(dtAirlineTable_, sAirline, iIndexAirlineAirline, iIndexAirlineGroundHandlers);
                    int firstDeskFB = -1;
                    int lastDeskFB = -1;
                    int firstDeskEco = -1;
                    int lastDeskEco = -1;
                    
                  
                    if (sNomColonneAllocation.Equals(GlobalNames.MakeUpDynamicAllocation))
                    {
                        firstDeskFB = makeUpFBStartDesk;
                        lastDeskFB = makeUpFBEndDesk;
                        firstDeskEco = makeUpEcoStartDesk;
                        lastDeskEco = makeUpEcoEndDesk;
                    }
                    if (sNomColonneAllocation.Equals(GlobalNames.CheckInDynamicAllocation))
                    {
                        firstDeskFB = checkInFBStartDesk;
                        lastDeskFB = checkInFBEndDesk;
                        firstDeskEco = checkInEcoStartDesk;
                        lastDeskEco = checkInEcoEndDesk;
                    }
                    if (sNomColonneAllocation.Equals(GlobalNames.BagDropDynamicAllocation))
                    {
                        firstDeskFB = baggDropFBStartDesk;
                        lastDeskFB = baggDropFBEndDesk;
                        firstDeskEco = baggDropEcoStartDesk;
                        lastDeskEco = baggDropEcoEndDesk;
                    }
                    if (sNomColonneAllocation.Equals(GlobalNames.BoardingGateDynamicAllocation))                    
                        firstDeskFB = boardGateDesk;
                    if (sNomColonneAllocation.Equals(GlobalNames.BaggageClaimDynamicAllocation))
                        firstDeskFB = baggClaimDesk;
                    if (sNomColonneAllocation.Equals(GlobalNames.TransferInfeedDynamicAllocation))                    
                        firstDeskFB = transferInfeedDesk;
                        
                    
                    

                   
                    // use FPITableIndex as key for the allocatedFlightInfo dictionary                    
                    if (DateTime.Compare(dateDebutAlloc, dtBegin_) < 0)
                        dateDebutAlloc = dtBegin_;
                    
                    if (sNomColonneAllocation.Equals(GlobalNames.MakeUpDynamicAllocation) || sNomColonneAllocation.Equals(GlobalNames.CheckInDynamicAllocation)
                        || sNomColonneAllocation.Equals(GlobalNames.BagDropDynamicAllocation))
                    {
                        if (firstDeskEco > 0 && lastDeskEco > 0)
                        {
                            String flightNbEco = flightNb + " " + "Eco";
                            allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNbEco, airportCode,
                                                            flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                            oiMakeUpInformation.dContainerSize, dTotalNumberOfPassengers, dTotalNumberOfBags,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                            firstDeskEco, lastDeskEco,
                                                            dateDebutAlloc, dateFinAlloc, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                            AllocationType.ToString(), sNomColonneAllocation,
                                                            calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                            if (allocatedFlightInfo != null)
                            {
                                FPITableIndex++;
                                allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);
                            }
                        }
                        if (firstDeskFB > 0 && lastDeskFB > 0)
                        {
                            String flightNbFB = flightNb + " " + "FB";
                            allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNbFB, airportCode,
                                                            flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                            oiMakeUpInformation.dContainerSize, dTotalNumberOfPassengers, dTotalNumberOfBags,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                            firstDeskFB, lastDeskFB,
                                                            dateDebutAlloc, dateFinAlloc, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                            AllocationType.ToString(), sNomColonneAllocation,
                                                            calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                            if (allocatedFlightInfo != null)
                            {
                                FPITableIndex++;
                                allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);
                            }
                        }

                    }
                    else if (sNomColonneAllocation.Equals(GlobalNames.BoardingGateDynamicAllocation) || sNomColonneAllocation.Equals(GlobalNames.BaggageClaimDynamicAllocation)
                            || sNomColonneAllocation.Equals(GlobalNames.TransferInfeedDynamicAllocation))
                    {
                        allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNb, airportCode,
                                                        flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                        oiMakeUpInformation.dContainerSize, dTotalNumberOfPassengers, dTotalNumberOfBags,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                        firstDeskFB, firstDeskFB,
                                                        dateDebutAlloc, dateFinAlloc, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                        AllocationType.ToString(), sNomColonneAllocation,
                                                        calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                        if (allocatedFlightInfo != null)
                        {
                            FPITableIndex++;
                            allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);                            
                        }
                    }
                    
                    
                }               
        */
        // << Task #11326 Pax2Sim - Allocation - add Parking allocation option

        public Boolean AllocateDesks()
        {
            int i, j, k;
            if (!CheckInputTable())
                return false;
            String sColumnsName = sNomColonneAllocation_ + "_";
            #region Les couleurs par compagnies doivent être appliqués.
            ArrayList alHandlingCompanies = null;
            if (bColorByHandler_)
            {
                bColorByHandler_ = bColorByHandler_ && (dtAirlineTable_ != null);
                alHandlingCompanies = new ArrayList();

                if (!bColorByHandler_)
                {
                    alErrors_.Add("Warn00669 : The airline table for the allocation is empty, the calc for the handling agent will not be done.");
                }
                else
                {
                    foreach (DataRow line in dtAirlineTable_.Rows)
                    {
                        if (!alHandlingCompanies.Contains(line[iIndexAirlineGroundHandlers].ToString()))
                            alHandlingCompanies.Add(line[iIndexAirlineGroundHandlers].ToString());
                    }
                    alHandlingCompanies.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }
            #endregion

            #region Pour les modes de visualisation et les couleurs.

            Hashtable htColor = null;
            Hashtable htHandlingColor = null;
            Hashtable htAirlineColor = null;

            dtResults = new DataTable[1];
            int iNbVisualisation = 0;
            if (bColorByFC_)
            {
                iNbVisualisation++;
                htColor = new Hashtable();
            }
            int iIndexHandlingColor = -1;
            int iIndexAirlineColor = -1;
            if (bColorByHandler_)
            {
                iIndexHandlingColor = iNbVisualisation;
                iNbVisualisation++;
                htHandlingColor = new Hashtable();
            }
            if (bColorByAirline_)
            {
                iIndexAirlineColor = iNbVisualisation;
                iNbVisualisation++;
                htAirlineColor = new Hashtable();
            }
            if (iNbVisualisation > 0)
                tvmAllocationVisualisation_ = new VisualisationMode[iNbVisualisation];
            else
            {
                tvmAllocationVisualisation_ = new VisualisationMode[1];
                tvmAllocationVisualisation_[0] = new VisualisationMode(false, false, false, null, new int[] { 0 });
            }
            #endregion

            #region Initialisation de la table
            dtResults[0] = new DataTable(/*sNomTable_*/"Allocation");
            dtResults[0].Columns.Add("Time", typeof(DateTime));
            dtResults[0].Columns.Add("Legend", typeof(String));
            OverallTools.DataFunctions.initialiserLignes(dtResults[0], dtBegin_, dtEnd_, dStep_);
            //We add some globals columns that will be used to know the global occupation of the desks. (We add a dumb column to 
            //deal with the fact that the function AddOccupationDesksColumns will remove the lasts columns if there is no new
            //columns added to the table.
            AddOccupationDesksColumns(dtResults[0], sGlobal+"_", ShowFlightNumber);
            dtResults[0].Columns.Add("Dumb", typeof(String));
            AddOccupationDesksColumns(dtResults[0], sColumnsName, ShowFlightNumber);
            dtResults[0].Columns.Remove("Dumb");
            if (bColorByFC_)
            {
                tvmAllocationVisualisation_[0] = new VisualisationMode(false, false, false, null, new int[] { 0 });
                tvmAllocationVisualisation_[0].ShowRowHeader = true;
                tvmAllocationVisualisation_[0].FirstColumnInHeader = true;
                ConditionnalFormatErrors cfeErrors = new ConditionnalFormatErrors();
                tvmAllocationVisualisation_[0].ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfeErrors };
            }
            if (bColorByHandler_)
            {
                tvmAllocationVisualisation_[iIndexHandlingColor] = new VisualisationMode(false, false, false, null, new int[] { 0 });
                tvmAllocationVisualisation_[iIndexHandlingColor].ShowRowHeader = true;
                tvmAllocationVisualisation_[iIndexHandlingColor].FirstColumnInHeader = true;
                ConditionnalFormatErrors cfeErrors = new ConditionnalFormatErrors();
                tvmAllocationVisualisation_[iIndexHandlingColor].ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfeErrors };
            }
            if (bColorByAirline_)
            {
                tvmAllocationVisualisation_[iIndexAirlineColor] = new VisualisationMode(false, false, false, null, new int[] { 0 });
                tvmAllocationVisualisation_[iIndexAirlineColor].ShowRowHeader = true;
                tvmAllocationVisualisation_[iIndexAirlineColor].FirstColumnInHeader = true;
                ConditionnalFormatErrors cfeErrors = new ConditionnalFormatErrors();
                tvmAllocationVisualisation_[iIndexAirlineColor].ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfeErrors };
            }

            for (i = 0; i < dtResults[0].Rows.Count; i++)
            {
                dtResults[0].Rows[i][1] = "";
            }

            #endregion

            #region On détermine le nombre d'interval entre 2 vols.
            Double dTimeBetweenFligths = dTimeBetweenFlights_;
            if (dTimeBetweenFlights_ != 0)
            {
                /*On arrondi en nombre d'interval entre 2 vols.*/
                //dTimeBetweenFligths = number of time intervals between 2 flights = minutes between2flights/timeStep, rounded up(we need an integer)
                dTimeBetweenFligths = dTimeBetweenFlights_ / dStep_;
                if ((Double)dTimeBetweenFligths > (int)dTimeBetweenFligths)
                    dTimeBetweenFligths = ((int)dTimeBetweenFligths) + 1;
            }
            #endregion

            #region Gestion de la liste des OCT
            int iMinNbRessources = -1;
            Double dMinOpeningTime = -1;
            Dictionary<String, Dictionary<String, OCTInformation>> htOCTList = null;
            // >> Task #12741 Pax2Sim - Dubai - flight exceptions
            //create the NotmalTable for flight exceptions and add it to dtOCTTable_.ExceptionFlight
            if (PAX2SIM.dubaiMode)
            {
                NormalTable exceptionFlightBkp = dtOCTTable_.ExceptionFlight;
                if (useDynamicOpeningTimes && dtOCTTable_ != null)
                {
                    dtOCTTable_.ExceptionFlight = createOCTFlightExceptionTableUsingDownTime(dtOCTTable_.Table, dtFPTable_, openingTimeFlightPlanColumnName);
                }
                htOCTList = GenerateOCTHashtable(out iMinNbRessources, out dMinOpeningTime);
                dtOCTTable_.ExceptionFlight = exceptionFlightBkp;
            }
            // << Task #12741 Pax2Sim - Dubai - flight exceptions
            else
            {
                htOCTList = GenerateOCTHashtable(out iMinNbRessources, out dMinOpeningTime);
            }
            iMinNbRessources = 1;
            if (htOCTList == null)
                return false;
            ///Initialisation d'une liste contenant toutes les catégories de vol présentes dans la table OCT.
            ArrayList alFlightCategory = new ArrayList();
            for (i = 1; i < dtOCTTable_.Table.Columns.Count; i++)
                alFlightCategory.Add(dtOCTTable_.Table.Columns[i].ColumnName);
            #endregion

            #region Gestion de l'information Loose / ULD pour les aircrafts.
            Dictionary<String, Dictionary<String, Dictionary<String, String>>> htLooseULD = GenerateLooseULD();
            #endregion

            #region Gestion des couleurs pour les airlines.
            ArrayList alAirlineCode = null;
            if (bColorByAirline_)
            {
                alAirlineCode = new ArrayList();
                foreach (DataRow Line in dtFPTable_.Rows)
                {
                    if (!alAirlineCode.Contains(Line[iIndexAirline].ToString()))
                        alAirlineCode.Add(Line[iIndexAirline].ToString());
                }
                alAirlineCode.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
            }
            #endregion

            #region Managing the overlap informations.

            /*bool bUseFlightCategoryTolerance = false;
            ///List caractérisant chacune des lignes contenues dans la table qui sera renvoyée. Cela
            ///permet de connaitre les catégories de vols allouées par cellule afin de gérer les cas
            ///où on peut allouer plusieurs vols sur une ressources.
            List<Hashtable> alFlightCategorieByLine = null;
            ArrayList alSHFlightCategories = null;*/

            ///Boolean that indicates if the overlapping mode is on or not. That will allow more than one flight
            ///to be allocated on the same desk.
            bool bOverlappingConstraints = false;
            ///List of the current overlapping object that are in the table. That will allow to make sure that
            ///the overlap is doing well by checking ervery cell for value that could avoir the overlapping.
            List<Dictionary<int, SegregationForOverlap>> ldisOverlapping = null;
            ///Dictionnary that will link a specified flitgh category to its maching overlap parameters to be
            ///able to find the overlap information for a given flight category quickly.
            Dictionary<String, SegregationForOverlap> dssfoOverlappingParameters = null;

            if ((lsfoOverlapParameters != null)&&(lsfoOverlapParameters.Count != 0))
            {
                foreach (SegregationForOverlap sfoTmp in lsfoOverlapParameters)
                {
                    if (sfoTmp.MaxOverlap == 1)
                        continue;
                    bOverlappingConstraints = true;
                    break;
                }
                if (bOverlappingConstraints)
                {
                    ldisOverlapping = new List<Dictionary<int, SegregationForOverlap>>();
                    for (i = 0; i < dtResults[0].Rows.Count; i++)
                    {
                        ldisOverlapping.Add(new Dictionary<int, SegregationForOverlap>());
                    }
                    dssfoOverlappingParameters = new Dictionary<string, SegregationForOverlap>();
                    foreach (String sFlightCategory in alFlightCategory)
                    {
                        bool bAdded = false;
                        foreach (SegregationForOverlap sfoTmp in lsfoOverlapParameters)
                        {
                            if (sfoTmp.Contains(sFlightCategory))
                            {
                                dssfoOverlappingParameters.Add(sFlightCategory, sfoTmp);
                                bAdded = true;
                                break;
                            }
                        }
                        if (!bAdded)
                        {
                            List<String> lsTmp = new List<string>();
                            lsTmp.Add(sFlightCategory);
                            SegregationForOverlap sfoTmp2 = new SegregationForOverlap("", lsTmp, 1);
                            dssfoOverlappingParameters.Add(sFlightCategory,sfoTmp2);
                        }
                    }
                }
            }
            /*if ((iToleranceSH_ != 1) || (iToleranceLH_ != 1))
            {
                alFlightCategorieByLine = new List<Hashtable>();
                for (i = 0; i < dtResults[0].Rows.Count; i++)
                {
                    alFlightCategorieByLine.Add(new Hashtable());
                }
                bUseFlightCategoryTolerance = true;
                String[] tsSHFlightCategories = sFlightCategoriesSH_.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                alSHFlightCategories = new ArrayList();
                alSHFlightCategories.AddRange(tsSHFlightCategories);
            }*/
            #endregion

            #region All variables used for the allocation.
            OCTInformation oiMakeUpInformation = null;
            OCTInformation oiMakeUpInformationFirst = null;
            String sCurrentFC, sAirline, sAircraft, sFlightID;
            Color cCurrentColor, cHandlingColor, cAirlineColor;
            DateTime dateDebutAlloc, dateFinAlloc, DatePatialClosing;
            Double dTimeDebutAlloc, dTimeFinAlloc;
            int iPosDebutAlloc, iPosFinAlloc, iPosFinAllocDoubleInfeed;
            int iPosDebutLibre, iPosFinLibre, iPosFinLibreDoubleInfeed;
            String sValue;
            int iExpectedFlight = 0;
            int iAllocatedFlight = 0;
            int iIndexNeeded = 0;
            int iIndexOccupied = 2;
            int iIndexFlightNumber = -1;
            int iOffsetDesks = dtResults[0].Columns.Count;
            if (ShowFlightNumber)
            {
                iIndexNeeded = dtResults[0].Columns.Count - 2;
                iIndexOccupied = dtResults[0].Columns.Count - 3;
                iIndexFlightNumber = dtResults[0].Columns.Count - 1;
            }
            else
            {
                iIndexNeeded = dtResults[0].Columns.Count - 1;
                iIndexOccupied = dtResults[0].Columns.Count - 2;
            }
            
            Dictionary<String, SegregationAllocationInformation>lsaiInformation = new Dictionary<string,SegregationAllocationInformation>();

            bool bDeparture = (AllocationType == TypeAllocation.MakeUpAllocation) ||
                            (AllocationType == TypeAllocation.CheckInAllocation) ||
                            (AllocationType == TypeAllocation.BoardingGateAllocation)
                            || (AllocationType == TypeAllocation.Parking);    // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
            #endregion

            #region FlightPlan Information table for Gantt initialize variables
            // the index used for the Flight Plan Information table
            // this will be the key in the allocationFPI dictionary
            // values starting from 1(will be incremented before first use)
            int FPITableIndex = 0;
            // Scenario Allocation FlightPlan Information table for gantt
            Dictionary<String, AllocationFlightPlanInformation> allocFPInfoDictionary = new Dictionary<string, AllocationFlightPlanInformation>();
            AllocationFlightPlanInformation allocatedFlightInfo;
            #endregion

            // << Task #9465 Pax2Sim - Allocation - Add FPI Columns                    
            //set the calculation type and OCT table used
            string calculationType = "";

            if (UseFPContent && calculateBasedOnContainerSize)
                calculationType = GlobalNames.FP_and_ContainerSize_based_calc_type;  //fp+contS
            else if (!UseFPContent && calculateBasedOnContainerSize)
                calculationType = GlobalNames.container_size_based_calc_type;  //contS
            else if (UseFPContent)
                calculationType = GlobalNames.FP_and_OCT_table_based_calc_type;  //fp+oct
            else
                calculationType = GlobalNames.OCT_table_based_calc_type;  //oct

            string octTableDescription = "";

            if (dtOCTTable != null)
            {
                octTableDescription = dtOCTTable.Name;
                if (UseOCTTableExceptions)
                    octTableDescription += " (with exceptions)";
                
                // >> Task #12741 Pax2Sim - Dubai - flight exceptions
                if (PAX2SIM.dubaiMode)
                {
                    if (useDynamicOpeningTimes)
                        octTableDescription += " (with dynamic opening times)";
                }
                // << Task #12741 Pax2Sim - Dubai - flight exceptions
            }
            // >> Task #9465 Pax2Sim - Allocation - Add FPI Columns
            List<FlightConfiguration> flightConfigurations = new List<FlightConfiguration>();   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            if (flightPlanParametersTable != null)
            {
                flightConfigurations = Tools.FlightPlanParametersTools.getFlightConfigurationsListFromFPParametersTable(flightPlanParametersTable);
            }
            #region Allocation des postes qui sont déjà renseignés dans le plan de vol.
            if (bUseFPContent_)
            {                                
                DataTable dtFPAllocated = SortFlightPlan(htOCTList, htLooseULD, new ArrayList(), "", "", true,
                    bDeparture);    // << Bug #9454 Pax2Sim - Simcore version bug - Allocation - flight exception doesn't work
                iExpectedFlight = dtFPAllocated.Rows.Count;
                #region The allocation
                foreach (DataRow drRow in dtFPAllocated.Rows)
                {

                    sCurrentFC = drRow[iIndexFlightCategory].ToString();
                    sAirline = drRow[iIndexAirline].ToString();
                    sAircraft = drRow[iIndexAircraft].ToString();
                    int iIdentifiantVol = (int)drRow[iIndexID];
                    sFlightID = "A_";
                    if (bDeparture)
                        sFlightID = "D_";
                    sFlightID += iIdentifiantVol.ToString();
                    //if (!htOCTList.Contains(sCurrentFC))
                    //    continue;

                    oiMakeUpInformation = getInformation(htOCTList, 0, sFlightID, sAirline, sCurrentFC);
                    if (oiMakeUpInformation == null)
                        continue;
                    oiMakeUpInformationFirst = getInformation(htOCTList, 0, sFlightID, sAirline, sCurrentFC);
                    if (oiMakeUpInformationFirst == null)
                        continue;
                    cCurrentColor = OverallTools.FonctionUtiles.getColor(oiMakeUpInformation.iIndexColor);
                    if (!Int32.TryParse(drRow[iIndexFirstColumnResult].ToString(), out i))
                    {
                        continue;
                    }
                    if (!Int32.TryParse(drRow[iIndexLastColumnResult].ToString(), out k))
                    {
                        continue;
                    }
                    int m = -1, n = -1;
                    if ((iIndexFirstColumnResultFirst == -1) || (!Int32.TryParse(drRow[iIndexFirstColumnResultFirst].ToString(), out m)) || (m == 0))
                    {
                        m = -1;
                        //continue;
                    }
                    else
                    {
                        if ((iIndexLastColumnResultFirst == -1) || (!Int32.TryParse(drRow[iIndexLastColumnResultFirst].ToString(), out n)))
                        {
                            continue;
                        }
                    }
                    i = /*(int)drRow[iIndexFirstColumnResult]*/ i + iOffsetDesks - 1;
                    k = /*(int)drRow[iIndexLastColumnResult]*/ k + iOffsetDesks - 1;

                    if (m != -1)
                    {
                        m = m + iOffsetDesks - 1;
                        n = n + iOffsetDesks - 1;
                    }

                    cHandlingColor = Color.White;
                    cAirlineColor = Color.White;
                    if (bColorByAirline_)
                    {
                        cAirlineColor = OverallTools.FonctionUtiles.getColor(alAirlineCode.IndexOf(sAirline));
                    }

                    if (bColorByHandler_)
                    {
                        string sHandlingAgent = OverallTools.DataFunctions.getValue(dtAirlineTable_, sAirline, iIndexAirlineAirline, iIndexAirlineGroundHandlers);
                        if ((sHandlingAgent == null) || (sHandlingAgent == ""))
                            cHandlingColor = OverallTools.FonctionUtiles.getColor(0);
                        else
                            cHandlingColor = OverallTools.FonctionUtiles.getColor(alHandlingCompanies.IndexOf(sHandlingAgent));
                    }

                    String sId = drRow[iIndexFLIGHTN].ToString().Trim();
                    dateDebutAlloc = (DateTime)drRow[sOpeningDeskColumn];
                    dateFinAlloc = (DateTime)drRow[sClosingDeskColumn];



                    #region We determine the number of passengers for that flight (In the case of the reclaim allocation.)
                    iPosFinAllocDoubleInfeed = 0;

                    // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size

                    double dTotalNumberOfPassengers = 0;
                    double dTotalNumberOfBags = 0;

                    if ((dtAircraftTable_ != null)
                        && (dtLoadFactors != null) && (dtNbBagage != null))
                    {
                        int iColumn;
                        dtNbBagage.UseException = UseNbBaggageExceptions;
                        DataTable dtTableBags = dtNbBagage.GetInformationsColumns(0, sFlightID, sAirline, sCurrentFC, out iColumn);
                        //dtNbBagage.UseException = true;

                        if (dtTableBags != null)
                        {
                            Double dMeanBaggage = OverallTools.StaticAnalysis.CalculCoefficientMultiplicateur(dtTableBags, iColumn);
                            if (dMeanBaggage != -1)
                            {
                                dtLoadFactors.UseException = UseLoadFactorExceptions;
                                dtAircraftTable_.UseException = UseAircraftTableExceptions;
                                dTotalNumberOfPassengers = OverallTools.DataFunctions.numberOfPassengers(drRow, dtAircraftTable_, null, dtLoadFactors, alErrors_);
                                
                                if (dTotalNumberOfPassengers == -1)
                                {
                                    alErrors_.Add("Err00681 : Flight ignored : \"" + sId + "\". The number of passenger for that flight is not valid.");
                                    continue;
                                }
                                dTotalNumberOfBags = Math.Round(dTotalNumberOfPassengers * dMeanBaggage, 2);

                                if (useInfeedLimitation && iIndexContainerSize != -1)
                                {
                                    // >> Task #17690 PAX2SIM - Flight Plan Parameters table                                    
                                    FlightConfiguration flightConfiguration = null;
                                    FlightAttribute.ARR_OR_DEP_FLIGHT_TAG arrOrDep = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.A;
                                    if (bDeparture)
                                    {
                                        arrOrDep = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.D;
                                    }
                                    flightConfiguration = Tools.FlightPlanParametersTools
                                        .getFlightConfigurationByArrOrDepAndFlightId(arrOrDep, iIdentifiantVol, flightConfigurations);                                    
                                    if (flightConfiguration != null)
                                    {
                                        if (bDeparture)
                                        {
                                            dTotalNumberOfPassengers = flightConfiguration.flightParameter.nbOrigEcoPax + flightConfiguration.flightParameter.nbOrigFbPax
                                                + flightConfiguration.flightParameter.nbTransferEcoPax + flightConfiguration.flightParameter.nbTransferFbPax;
                                            dTotalNumberOfBags = flightConfiguration.flightParameter.nbOrigEcoBags + flightConfiguration.flightParameter.nbOrigFbBags
                                                + flightConfiguration.flightParameter.nbTransferEcoBags + flightConfiguration.flightParameter.nbTransferFbBags;
                                        }
                                        else
                                        {
                                            dTotalNumberOfPassengers = flightConfiguration.flightParameter.nbTransferEcoPax + flightConfiguration.flightParameter.nbTransferFbPax
                                                + flightConfiguration.flightParameter.nbTermEcoPax + flightConfiguration.flightParameter.nbTermFbPax;
                                            dTotalNumberOfBags = flightConfiguration.flightParameter.nbTransferEcoBags + flightConfiguration.flightParameter.nbTransferFbBags
                                                + flightConfiguration.flightParameter.nbTermEcoBags + flightConfiguration.flightParameter.nbTermFbBags;
                                        }                                        
                                    }
                                    // << Task #17690 PAX2SIM - Flight Plan Parameters table
                                    int iNumberOfContainer = (int)Math.Ceiling(dTotalNumberOfBags / oiMakeUpInformation.dContainerSize);
                                    int iNumberOfCycle = (int)Math.Ceiling((Double)iNumberOfContainer / (double)oiMakeUpInformation.iNbContainerPerCycle);
                                    Double dWaitingForCycle = (Double)(iNumberOfCycle - 1) * oiMakeUpInformation.dDeadTime;

                                    dateFinAlloc = (DateTime)drRow[sOpeningDeskColumn];
                                    dateFinAlloc = dateFinAlloc.AddMinutes(dWaitingForCycle);
                                    DateTime dateFinAllocDoubleInfeed = dateFinAlloc.AddMinutes(dInfeedSpeed * dTotalNumberOfBags * 2);
                                    dateFinAlloc = dateFinAlloc.AddMinutes(dInfeedSpeed * dTotalNumberOfBags);
                                    Double dTimeFinAllocDoubleInfeed = OverallTools.DataFunctions.MinuteDifference(dtBegin_, dateFinAllocDoubleInfeed);
                                    iPosFinAllocDoubleInfeed = (int)(dTimeFinAllocDoubleInfeed / dStep_);
                                    if (((Double)iPosFinAllocDoubleInfeed) == dTimeFinAllocDoubleInfeed / dStep_)
                                        iPosFinAllocDoubleInfeed--;
                                }
                            }
                        }
                    }

                    #region old code
                    /*
                    if ((iIndexContainerSize != -1) && (dtAircraftTable_ != null)
                        && (dtLoadFactors != null) && (dtNbBagage != null))
                    {
                        int iColumn;
                        dtNbBagage.UseException = UseNbBaggageExceptions;
                        DataTable dtTableBags = dtNbBagage.GetInformationsColumns(0, sFlightID, sAirline, sCurrentFC, out iColumn);
                        dtNbBagage.UseException = true;
                        if (dtTableBags != null)
                        {
                            Double dMeanBaggage = OverallTools.StaticAnalysis.CalculCoefficientMultiplicateur(dtTableBags, iColumn);
                            if (dMeanBaggage != -1)
                            {
                                dtLoadFactors.UseException = UseLoadFactorExceptions;
                                dtAircraftTable_.UseException = UseAircraftTableExceptions;
                                double dTotalNumberOfPassengers = OverallTools.DataFunctions.numberOfPassengers(drRow, dtAircraftTable_, null, dtLoadFactors, alErrors_);
                                dtLoadFactors.UseException = true;
                                dtAircraftTable_.UseException = true;
                                if (dTotalNumberOfPassengers == -1)
                                {
                                    alErrors_.Add("Err00681 : Flight ignored : \"" + sId + "\". The number of passenger for that flight is not valid.");
                                    continue;
                                }
                                Double dTotalNumberOfBags = dTotalNumberOfPassengers * dMeanBaggage;
                                int iNumberOfContainer = (int)Math.Ceiling(dTotalNumberOfBags / oiMakeUpInformation.dContainerSize);
                                int iNumberOfCycle = (int)Math.Ceiling((Double)iNumberOfContainer / (double)oiMakeUpInformation.iNbContainerPerCycle);
                                Double dWaitingForCycle = (Double)(iNumberOfCycle - 1) * oiMakeUpInformation.dDeadTime;

                                dateFinAlloc = (DateTime)drRow[sOpeningDeskColumn];
                                dateFinAlloc = dateFinAlloc.AddMinutes(dWaitingForCycle);
                                DateTime dateFinAllocDoubleInfeed = dateFinAlloc.AddMinutes(dInfeedSpeed * dTotalNumberOfBags * 2);
                                dateFinAlloc = dateFinAlloc.AddMinutes(dInfeedSpeed * dTotalNumberOfBags);
                                Double dTimeFinAllocDoubleInfeed = OverallTools.DataFunctions.MinuteDifference(dtBegin_, dateFinAllocDoubleInfeed);
                                iPosFinAllocDoubleInfeed = (int)(dTimeFinAllocDoubleInfeed / dStep_);
                                if (((Double)iPosFinAllocDoubleInfeed) == dTimeFinAllocDoubleInfeed / dStep_)
                                    iPosFinAllocDoubleInfeed--;
                            }
                        }
                    }*/
                    #endregion
                    
                    // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                    #endregion

                    dTimeDebutAlloc = OverallTools.DataFunctions.MinuteDifference(dtBegin_, dateDebutAlloc);
                    dTimeFinAlloc = OverallTools.DataFunctions.MinuteDifference(dtBegin_, dateFinAlloc);
                    iPosDebutAlloc = (int)(dTimeDebutAlloc / dStep_);
                    iPosFinAlloc = (int)(dTimeFinAlloc / dStep_);
                    if (((Double)iPosFinAlloc) == dTimeFinAlloc / dStep_)
                        iPosFinAlloc--;
                    if ((iPosFinAlloc < 0) || (iPosDebutAlloc > dtResults[0].Rows.Count))
                        continue;
                    iPosDebutLibre = iPosDebutAlloc - (int)dTimeBetweenFligths;
                    iPosFinLibre = iPosFinAlloc + (int)dTimeBetweenFligths;

                    if (iPosDebutLibre < 0)
                        iPosDebutLibre = 0;
                    if (iPosDebutAlloc < 0)
                        iPosDebutAlloc = 0;
                    if (iPosFinAlloc >= dtResults[0].Rows.Count)
                        iPosFinAlloc = dtResults[0].Rows.Count - 1;
                    if (iPosFinLibre >= dtResults[0].Rows.Count)
                        iPosFinLibre = dtResults[0].Rows.Count - 1;

                    #region (In the case of the reclaim allocation.)
                    iPosFinLibreDoubleInfeed = 0;
                    if (useInfeedLimitation && iIndexContainerSize != -1)
                    {
                        iPosFinLibreDoubleInfeed = iPosFinAllocDoubleInfeed + (int)dTimeBetweenFligths;
                        if (iPosFinAllocDoubleInfeed >= dtResults[0].Rows.Count)
                            iPosFinAllocDoubleInfeed = dtResults[0].Rows.Count - 1;
                        if (iPosFinLibreDoubleInfeed >= dtResults[0].Rows.Count)
                            iPosFinLibreDoubleInfeed = dtResults[0].Rows.Count - 1;
                    }
                    #endregion
                    iAllocatedFlight++;
                    for (; i <= k; i++)
                    {
                        ///Création des colonnes manquantes dans la table de résultat.
                        while (dtResults[0].Columns.Count <= i)
                            dtResults[0].Columns.Add(sColumnsName + (dtResults[0].Columns.Count - iOffsetDesks + 1).ToString(), typeof(String));

                        #region (In the case of the reclaim allocation.)
                        int iFinAllocation = iPosFinAlloc;
                        int iFinLibre = iPosFinLibre;
                        if (useInfeedLimitation && iIndexContainerSize != -1)
                        {
                            iFinAllocation = iPosFinAllocDoubleInfeed;
                            iFinLibre = iPosFinLibreDoubleInfeed;
                        }
                        #endregion
                        for (j = iPosDebutAlloc; j <= iFinAllocation; j++)
                        {
                            if (bOverlappingConstraints)
                            {
                                if (!ldisOverlapping[j].ContainsKey(i))
                                {
                                    ldisOverlapping[j].Add(i, dssfoOverlappingParameters[sCurrentFC]);
                                }
                            }
                            /*if (bUseFlightCategoryTolerance)
                            {
                                Hashtable htTmp = (Hashtable)alFlightCategorieByLine[j];
                                if (!htTmp.ContainsKey(i))
                                    htTmp.Add(i, sCurrentFC);
                            }*/
                            sValue = dtResults[0].Rows[j][i].ToString();
                            if (sValue.Length > 0)
                                sValue += ", ";
                            sValue += sId;
                            dtResults[0].Rows[j][i] = sValue;
                            if (bColorByFC_)
                                ((ConditionnalFormatErrors)tvmAllocationVisualisation_[0].ConditionnalFormatClass[0]).setCondition(i, j, cCurrentColor);
                            if (bColorByHandler_)
                                ((ConditionnalFormatErrors)tvmAllocationVisualisation_[iIndexHandlingColor].ConditionnalFormatClass[0]).setCondition(i, j, cHandlingColor);
                            if (bColorByAirline_)
                                ((ConditionnalFormatErrors)tvmAllocationVisualisation_[iIndexAirlineColor].ConditionnalFormatClass[0]).setCondition(i, j, cAirlineColor);
                        }
                        if ((i == k) && (m != -1) && (n != k))
                        {
                            /*Allocation of the fisrt and business desks*/
                            i = m - 1;
                            k = n;
                        }
                    }
                    //Add the flights from the FP that are already allocated in the FPInformation table
                    #region Scenario Allocation FlightPlan Information table for gantt
                    // add the information about the allocated flight in the allocFPInfoDictionary dictionary
                    // the dictionary will be used to insert the informations into the Flight Plan Information tabel
                    #region Variables for retrieving the data from the dtFPAllocated - the allocated flights
                    int indexColumnCheckInEcoStart = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
                    int indexColumnCheckInEcoEnd = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
                    int indexColumnCheckInFBStart = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
                    int indexColumnCheckInFBEnd = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);

                    int indexColumnBagDropEcoStart = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
                    int indexColumnBagDropEcoEnd = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
                    int indexColumnBagDropFBStart = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
                    int indexColumnBagDropFBEnd = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);

                    int indexColumnMakeUpEcoStart = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
                    int indexColumnMakeUpEcoEnd = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
                    int indexColumnMakeUpFBStart = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
                    int indexColumnMakeUpFBEnd = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);

                    int indexColumnBoardingGate = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);

                    int indexColumnBaggageClaim = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
                    int indexColumnTransferInfeed = dtFPAllocated.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);
                    
                    int checkInEcoStartDesk = -1;
                    int checkInEcoEndDesk = -1;
                    int checkInFBStartDesk = -1;
                    int checkInFBEndDesk = -1;

                    int baggDropEcoStartDesk = -1;
                    int baggDropEcoEndDesk = -1;
                    int baggDropFBStartDesk = -1;
                    int baggDropFBEndDesk = -1;

                    int makeUpEcoStartDesk = -1;
                    int makeUpEcoEndDesk = -1;
                    int makeUpFBStartDesk = -1;
                    int makeUpFBEndDesk = -1;

                    int boardGateDesk = -1;

                    int baggClaimDesk = -1;
                    int transferInfeedDesk = -1;

                    if (indexColumnCheckInEcoStart != -1)
                        int.TryParse(drRow[indexColumnCheckInEcoStart].ToString(), out checkInEcoStartDesk);    // >> Allocation bug - not needed check on other resource data from the FP
                    
                    if (indexColumnCheckInEcoEnd != -1)
                        int.TryParse(drRow[indexColumnCheckInEcoEnd].ToString(), out checkInEcoEndDesk);
                    if (indexColumnCheckInFBStart != -1)
                        int.TryParse(drRow[indexColumnCheckInFBStart].ToString(), out checkInFBStartDesk);
                    if (indexColumnCheckInFBEnd != -1)
                        int.TryParse(drRow[indexColumnCheckInFBEnd].ToString(), out checkInFBEndDesk);

                    if (indexColumnBagDropEcoStart != -1)
                        int.TryParse(drRow[indexColumnBagDropEcoStart].ToString(), out baggDropEcoStartDesk);
                    if (indexColumnBagDropEcoEnd != -1)
                        int.TryParse(drRow[indexColumnBagDropEcoEnd].ToString(), out baggDropEcoEndDesk);
                    if (indexColumnBagDropFBStart != -1)
                        int.TryParse(drRow[indexColumnBagDropFBStart].ToString(), out baggDropFBStartDesk);
                    if (indexColumnBagDropFBEnd != -1)
                        int.TryParse(drRow[indexColumnBagDropFBEnd].ToString(), out baggDropFBEndDesk);

                    if (indexColumnMakeUpEcoStart != -1)
                        int.TryParse(drRow[indexColumnMakeUpEcoStart].ToString(), out makeUpEcoStartDesk);
                    if (indexColumnMakeUpEcoEnd != -1)
                        int.TryParse(drRow[indexColumnMakeUpEcoEnd].ToString(), out makeUpEcoEndDesk);
                    if (indexColumnMakeUpFBStart != -1)
                        int.TryParse(drRow[indexColumnMakeUpFBStart].ToString(), out makeUpFBStartDesk);
                    if (indexColumnMakeUpFBEnd != -1)
                        int.TryParse(drRow[indexColumnMakeUpFBEnd].ToString(), out makeUpFBEndDesk);

                    if (indexColumnBoardingGate != -1)
                        int.TryParse(drRow[indexColumnBoardingGate].ToString(), out boardGateDesk);

                    if (indexColumnBaggageClaim != -1)
                        int.TryParse(drRow[indexColumnBaggageClaim].ToString(), out baggClaimDesk);
                    if (indexColumnTransferInfeed != -1)
                        int.TryParse(drRow[indexColumnTransferInfeed].ToString(), out transferInfeedDesk);
                    #endregion

                    #region Variables which hold the information from the Flight Plan
                    int indexColumnID = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                    int indexColumnDate = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                    int indexColumnSTA = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
                    int indexColumnSTD = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
                    int indexColumnAirlineCode = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                    int indexColumnFlightNb = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                    int indexColumnAirportCode = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                    int indexColumnFlightCategory = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                    int indexColumnAircraftType = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                    int indexColumnNoBSM = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
                    int indexColumnCBP = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
                    int indexColumnTSA = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
                    int indexColumnNbSeats = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
                    int indexColumnUser1 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                    int indexColumnUser2 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                    int indexColumnUser3 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                    int indexColumnUser4 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                    int indexColumnUser5 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);

                    int flightId = -1;
                    DateTime date = new DateTime();
                    TimeSpan sta_d = new TimeSpan();
                    String airlineCode = "";
                    String flightNb = "";
                    String airportCode = "";
                    String flightCategory = "";
                    String aircraftType = "";
                    Boolean noBSM = false;
                    Boolean CBP = false;
                    Boolean TSA = false;
                    int nbSeats = -1;
                    String user1 = "";
                    String user2 = "";
                    String user3 = "";
                    String user4 = "";
                    String user5 = "";

                    if (indexColumnID != -1)
                        flightId = int.Parse(drRow[indexColumnID].ToString());
                    if (indexColumnDate != -1)
                        date = DateTime.Parse(drRow[indexColumnDate].ToString());
                    if (indexColumnSTA != -1)
                        sta_d = TimeSpan.Parse(drRow[indexColumnSTA].ToString());
                    else if (indexColumnSTD != -1)
                        sta_d = TimeSpan.Parse(drRow[indexColumnSTD].ToString());
                    if (indexColumnAirlineCode != -1)
                        airlineCode = drRow[indexColumnAirlineCode].ToString();
                    if (indexColumnFlightNb != -1)
                        flightNb = drRow[indexColumnFlightNb].ToString();
                    if (indexColumnAirportCode != -1)
                        airportCode = drRow[indexColumnAirportCode].ToString();
                    if (indexColumnFlightCategory != -1)
                        flightCategory = drRow[indexColumnFlightCategory].ToString();
                    if (indexColumnAircraftType != -1)
                        aircraftType = drRow[indexColumnAircraftType].ToString();
                    if (indexColumnNoBSM != -1)
                        noBSM = bool.Parse(drRow[indexColumnNoBSM].ToString());
                    if (indexColumnCBP != -1)
                        CBP = bool.Parse(drRow[indexColumnCBP].ToString());
                    if (indexColumnTSA != -1)
                        TSA = bool.Parse(drRow[indexColumnTSA].ToString());
                    if (indexColumnNbSeats != -1)
                        nbSeats = int.Parse(drRow[indexColumnNbSeats].ToString());
                    if (indexColumnUser1 != -1)
                        user1 = drRow[indexColumnUser1].ToString();
                    if (indexColumnUser1 != -1)
                        user2 = drRow[indexColumnUser2].ToString();
                    if (indexColumnUser1 != -1)
                        user3 = drRow[indexColumnUser3].ToString();
                    if (indexColumnUser1 != -1)
                        user4 = drRow[indexColumnUser4].ToString();
                    if (indexColumnUser1 != -1)
                        user5 = drRow[indexColumnUser5].ToString();
                    #endregion

                    String groundHandler = OverallTools.DataFunctions.getValue(dtAirlineTable_, sAirline, iIndexAirlineAirline, iIndexAirlineGroundHandlers);
                    int firstDeskFB = -1;
                    int lastDeskFB = -1;
                    int firstDeskEco = -1;
                    int lastDeskEco = -1;
                    
                    #region set the first/last FB/Eco desks according to the allocation type
                    if (sNomColonneAllocation.Equals(GlobalNames.MakeUpDynamicAllocation))
                    {
                        firstDeskFB = makeUpFBStartDesk;
                        lastDeskFB = makeUpFBEndDesk;
                        firstDeskEco = makeUpEcoStartDesk;
                        lastDeskEco = makeUpEcoEndDesk;
                    }
                    if (sNomColonneAllocation.Equals(GlobalNames.CheckInDynamicAllocation))
                    {
                        firstDeskFB = checkInFBStartDesk;
                        lastDeskFB = checkInFBEndDesk;
                        firstDeskEco = checkInEcoStartDesk;
                        lastDeskEco = checkInEcoEndDesk;
                    }
                    if (sNomColonneAllocation.Equals(GlobalNames.BagDropDynamicAllocation))
                    {
                        firstDeskFB = baggDropFBStartDesk;
                        lastDeskFB = baggDropFBEndDesk;
                        firstDeskEco = baggDropEcoStartDesk;
                        lastDeskEco = baggDropEcoEndDesk;
                    }
                    if (sNomColonneAllocation.Equals(GlobalNames.BoardingGateDynamicAllocation))                    
                        firstDeskFB = boardGateDesk;
                    if (sNomColonneAllocation.Equals(GlobalNames.BaggageClaimDynamicAllocation))
                        firstDeskFB = baggClaimDesk;
                    if (sNomColonneAllocation.Equals(GlobalNames.TransferInfeedDynamicAllocation))                    
                        firstDeskFB = transferInfeedDesk;
                        
                    
                    #endregion

                   
                    // use FPITableIndex as key for the allocatedFlightInfo dictionary                    
                    if (DateTime.Compare(dateDebutAlloc, dtBegin_) < 0)
                        dateDebutAlloc = dtBegin_;
                    
                    if (sNomColonneAllocation.Equals(GlobalNames.MakeUpDynamicAllocation) || sNomColonneAllocation.Equals(GlobalNames.CheckInDynamicAllocation)
                        || sNomColonneAllocation.Equals(GlobalNames.BagDropDynamicAllocation))
                    {
                        if (firstDeskEco > 0 && lastDeskEco > 0)
                        {
                            String flightNbEco = flightNb + " " + "Eco";
                            allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNbEco, airportCode,
                                                            flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                            oiMakeUpInformation.dContainerSize, dTotalNumberOfPassengers, dTotalNumberOfBags,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                            firstDeskEco, lastDeskEco,
                                                            dateDebutAlloc, dateFinAlloc, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                            AllocationType.ToString(), sNomColonneAllocation,
                                                            calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                            if (allocatedFlightInfo != null)
                            {
                                FPITableIndex++;
                                allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);
                            }
                        }
                        if (firstDeskFB > 0 && lastDeskFB > 0)
                        {
                            String flightNbFB = flightNb + " " + "FB";
                            allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNbFB, airportCode,
                                                            flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                            oiMakeUpInformation.dContainerSize, dTotalNumberOfPassengers, dTotalNumberOfBags,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                            firstDeskFB, lastDeskFB,
                                                            dateDebutAlloc, dateFinAlloc, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                            AllocationType.ToString(), sNomColonneAllocation,
                                                            calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                            if (allocatedFlightInfo != null)
                            {
                                FPITableIndex++;
                                allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);
                            }
                        }

                    }
                    else if (sNomColonneAllocation.Equals(GlobalNames.BoardingGateDynamicAllocation) || sNomColonneAllocation.Equals(GlobalNames.BaggageClaimDynamicAllocation)
                            || sNomColonneAllocation.Equals(GlobalNames.TransferInfeedDynamicAllocation))
                    {
                        allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNb, airportCode,
                                                        flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                        oiMakeUpInformation.dContainerSize, dTotalNumberOfPassengers, dTotalNumberOfBags,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                        firstDeskFB, firstDeskFB,
                                                        dateDebutAlloc, dateFinAlloc, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                        AllocationType.ToString(), sNomColonneAllocation,
                                                        calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                        if (allocatedFlightInfo != null)
                        {
                            FPITableIndex++;
                            allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);                            
                        }
                    }
                    
                    #endregion
                }
                #endregion

                #region We calculate the result of the already allocated flight.
                SegregationAllocationInformation saiTmp = new SegregationAllocationInformation(sNomColonneAllocation_, iExpectedFlight, iAllocatedFlight, dtResults[0].Columns.Count - iOffsetDesks);
                lsaiInformation.Add(sNomColonneAllocation_, saiTmp);
                AnalyseTableOccupation(dtResults[0], iOffsetDesks, iIndexOccupied, iIndexNeeded, iIndexFlightNumber, saiTmp);
                #endregion
            }
            #endregion

            ///Transformation du nombre de minutes minimum pour une allocation en un nombre de lignes dans la tables d'allocation.
            dMinOpeningTime = dMinOpeningTime / dStep_;

            #region Création des ségrégations d'analyse.
            ArrayList alSegregation = new ArrayList();
            ArrayList alFlightCategorie = new ArrayList();
            #region Pour les catégories de vols.
            if (bFCAllocation_)
            {
                #region Classement des catégories de vols.
                //On détermine l'ordre des FC pour l'allocation (en fonction du nombre de MakeUp et de la période d'ouverture).
                ArrayList alProcessedFC = new ArrayList();

                while (alProcessedFC.Count != htOCTList["Normal"].Count)
                {
                    ArrayList alFCTmp = new ArrayList();
                    Double dAllocTime = -1;
                    int iNombrePostes = -1;
                    sCurrentFC = "";
                    foreach (String keys in htOCTList["Normal"].Keys)
                    {
                        if (alProcessedFC.Contains(keys))
                            continue;
                        if (htOCTList["Normal"][keys].NombrePostes >= iNombrePostes)
                        {
                            if ((htOCTList["Normal"][keys].NombrePostes > iNombrePostes) ||
                                (htOCTList["Normal"][keys].Difference > dAllocTime))
                            {
                                alFCTmp = new ArrayList();
                                alFCTmp.Add(keys);
                                iNombrePostes = htOCTList["Normal"][keys].NombrePostes;
                                dAllocTime = htOCTList["Normal"][keys].Difference;
                            }
                            else if (htOCTList["Normal"][keys].Difference == dAllocTime)
                            {
                                alFCTmp.Add(keys);
                            }
                        }
                    }
                    if (alFCTmp.Count == 0)
                    {
                        alErrors_.Add("Err00676 : The rules for the allocation are wrong.");
                        return false;
                    }
                    alProcessedFC.AddRange(alFCTmp);
                    alFlightCategorie.Add(alFCTmp);
                }
                #endregion
            }
            else
            {
                alFlightCategorie.Add(new ArrayList());
            }
            #endregion
            #region Pour les GRound Handlers
            ArrayList alGroundTmp = new ArrayList();

            if (bSegregateGroundHandlers_)
            {
                foreach (DataRow drRow in dtAirlineTable_.Rows)
                {
                    String sGroundTmp = drRow[iIndexAirlineGroundHandlers].ToString();
                    if ((sGroundTmp.Length != 0) && (!alGroundTmp.Contains(sGroundTmp)))
                        alGroundTmp.Add(sGroundTmp);
                }
            }
            if (alGroundTmp.Count == 0)
                alGroundTmp.Add("");
            #endregion
            #region Pour les Loose ULD.
            ArrayList alLooseULD = new ArrayList();
            if (bSegregateLooseULD_)
            {
                alLooseULD.Add(GlobalNames.sTableContent_ULD);
                alLooseULD.Add(GlobalNames.sTableContent_Loose);
            }
            else
            {
                alLooseULD.Add("");
            }
            #endregion
            foreach (String sLooseULD in alLooseULD)
            {
                foreach (String sGroundHandlerTmp in alGroundTmp)
                {
                    foreach (ArrayList alList in alFlightCategorie)
                    {
                        alSegregation.Add(CreateAnalyse(alList, sGroundHandlerTmp, sLooseULD));
                    }
                }
            }

            #endregion


            #region (In the case of the reclaim allocation.)
            int iLastDoubleInfeed = 0;
            #endregion

            #region Partie pour l'allocation à proprement parlé.
            // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            int currentResourceNumber = 0;
            int lastResourceNbFromPreviousSegregation = 0;
            // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
            int allocationOffset = 0;
            if (firstIndexForAllocation_ > 0)
                allocationOffset = firstIndexForAllocation_ - 1;
            // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource

            foreach (Object[] toObject in alSegregation)
            {
                if (!bUseFPContent_)
                {
                    SegregationAllocationInformation saiTmp = null;
                    if ((sColumnsName != sNomColonneAllocation_ + "_") ||((sColumnsName == sNomColonneAllocation_ + "_") && ((toObject[1].ToString() == "") || (toObject[2].ToString() == ""))))
                    {
                        /*if (iExpectedFlight != 0)
                        {*/
                            if (!lsaiInformation.ContainsKey(sColumnsName))
                                lsaiInformation.Add(sColumnsName, new SegregationAllocationInformation(sColumnsName, iExpectedFlight, iAllocatedFlight, dtResults[0].Columns.Count - iOffsetDesks));
                            else
                            {
                                if (dtResults[0].Columns.Count < iOffsetDesks)
                                    lsaiInformation[sColumnsName].ResetStatistics(0);
                                else
                                    lsaiInformation[sColumnsName].ResetStatistics(dtResults[0].Columns.Count - iOffsetDesks);
                                lsaiInformation[sColumnsName].AddFligts(iExpectedFlight, iAllocatedFlight);
                            }
                            iExpectedFlight = 0;
                            iAllocatedFlight = 0;
                            saiTmp = lsaiInformation[sColumnsName];
                        //}
                    }


                    sColumnsName = sNomColonneAllocation_ + "_" + toObject[1].ToString();
                    if (!sColumnsName.EndsWith("_"))
                        sColumnsName += "_";

                    sColumnsName += toObject[2].ToString();
                    if (!sColumnsName.EndsWith("_"))
                        sColumnsName += "_";
                    AnalyseTableOccupation(dtResults[0], iOffsetDesks, iIndexOccupied, iIndexNeeded, iIndexFlightNumber, saiTmp);
                    int iTmpNombreColonne = dtResults[0].Columns.Count;

                    AddOccupationDesksColumns(dtResults[0], sColumnsName, ShowFlightNumber);
                    if (iTmpNombreColonne != dtResults[0].Columns.Count)
                    {
                        //iOffsetDesks += 2;
                        //iIndexFirstMakeUpToWatch = dtResults[0].Columns.Count;

                        // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering                
                        lastResourceNbFromPreviousSegregation = currentResourceNumber;
                        // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering

                        iOffsetDesks = dtResults[0].Columns.Count;
                        if (bOverlappingConstraints)
                        {
                            for (i = 0; i < ldisOverlapping.Count; i++)
                                ldisOverlapping[i].Clear();
                        }
                        if (ShowFlightNumber)
                        {
                            iIndexFlightNumber = dtResults[0].Columns.Count - 1;
                            iIndexNeeded = dtResults[0].Columns.Count - 2;
                            iIndexOccupied = dtResults[0].Columns.Count - 3;
                        }
                        else
                        {
                            iIndexNeeded = dtResults[0].Columns.Count - 1;
                            iIndexOccupied = dtResults[0].Columns.Count - 2;
                        }
                    }

                    #region (In the case of the reclaim allocation.)
                    if (useInfeedLimitation && iIndexContainerSize != -1)
                    {
                        iLastDoubleInfeed = dtResults[0].Columns.Count + iNumberOfDoubleInfeed;
                    }
                    #endregion
                }

                DataTable dtFPTemporaire = SortFlightPlan(htOCTList, htLooseULD, (ArrayList)toObject[0], (String)toObject[1], (String)toObject[2], false,
                    bDeparture);      // << Bug #9454 Pax2Sim - Simcore version bug - Allocation - flight exception doesn't work
                if (dtFPTemporaire.Rows.Count == 0)
                    continue;
                iExpectedFlight += dtFPTemporaire.Rows.Count;

                foreach (DataRow drRow in dtFPTemporaire.Rows)
                {
                    sFlightID = drRow[iIndexID].ToString();
                    if (bDeparture)
                        sFlightID = "D_" + sFlightID;
                    else
                        sFlightID = "A_" + sFlightID;

                    sCurrentFC = drRow[iIndexFlightCategory].ToString();
                    sAircraft = drRow[iIndexAircraft].ToString();
                    sAirline = drRow[iIndexAirline].ToString();
                    oiMakeUpInformation = getInformation(htOCTList, 0, sFlightID, sAirline, sCurrentFC);
                    if (oiMakeUpInformation == null)
                    {
                        alErrors_.Add("Err00333: Flight ignored : \"" + sFlightID + "\". There is no information for the opening and closing in the table. (The flight might have wrong flight categories informations).");
                        continue;
                    }

                    cCurrentColor = OverallTools.FonctionUtiles.getColor(oiMakeUpInformation.iIndexColor);

                    cHandlingColor = Color.White;
                    cAirlineColor = Color.White;
                    if (bColorByAirline_)
                    {
                        cAirlineColor = OverallTools.FonctionUtiles.getColor(alAirlineCode.IndexOf(sAirline));
                    }

                    if (bColorByHandler_)
                    {
                        string sHandlingAgent = OverallTools.DataFunctions.getValue(dtAirlineTable_, sAirline, iIndexAirlineAirline, iIndexAirlineGroundHandlers);
                        if ((sHandlingAgent == null) || (sHandlingAgent == ""))
                            cHandlingColor = OverallTools.FonctionUtiles.getColor(0);
                        else
                            cHandlingColor = OverallTools.FonctionUtiles.getColor(alHandlingCompanies.IndexOf(sHandlingAgent));
                    }

                    int iIdentifiantVol = (int)drRow[iIndexID];
                    String sId = drRow[iIndexFLIGHTN].ToString().Trim();

                    dateDebutAlloc = (DateTime)drRow[sOpeningDeskColumn];
                    DatePatialClosing = (DateTime)drRow[sClosingPartialDeskColumn];
                    dateFinAlloc = (DateTime)drRow[sClosingDeskColumn];

                    #region We determine the number of passengers for that flight (In the case of the reclaim allocation.)
                    iPosFinAllocDoubleInfeed = 0;
                    if (useInfeedLimitation && (iIndexContainerSize != -1) &&
                        (dtNbBagage != null) && (dtAircraftTable_ != null)
                        && (dtLoadFactors != null))
                    {
                        int iColumn;
                        dtNbBagage.UseException = UseNbBaggageExceptions;
                        DataTable dtTableBags = dtNbBagage.GetInformationsColumns(0, sFlightID, sAirline, sCurrentFC, out iColumn);
                        dtNbBagage.UseException = true;
                        if (dtTableBags != null)
                        {
                            Double dMeanBaggage = OverallTools.StaticAnalysis.CalculCoefficientMultiplicateur(dtTableBags, iColumn);
                            if (dMeanBaggage != -1)
                            {
                                dtLoadFactors.UseException = UseLoadFactorExceptions;
                                dtAircraftTable_.UseException = UseAircraftTableExceptions;
                                double dTotalNumberOfPassengers = OverallTools.DataFunctions.numberOfPassengers(drRow, dtAircraftTable_, null, dtLoadFactors, alErrors_);
                                dtLoadFactors.UseException = true;
                                dtAircraftTable_.UseException = true;
                                if (dTotalNumberOfPassengers == -1)
                                {
                                    alErrors_.Add("Err00681 : Flight ignored : \"" + sId + "\". The number of passenger for that fligt is not valid.");
                                    continue;
                                }
                                Double dTotalNumberOfBags = dTotalNumberOfPassengers * dMeanBaggage;

                                // >> Task #17690 PAX2SIM - Flight Plan Parameters table                                    
                                FlightConfiguration flightConfiguration = null;
                                FlightAttribute.ARR_OR_DEP_FLIGHT_TAG arrOrDep = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.A;
                                if (bDeparture)
                                {
                                    arrOrDep = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.D;
                                }
                                flightConfiguration = Tools.FlightPlanParametersTools
                                    .getFlightConfigurationByArrOrDepAndFlightId(arrOrDep, iIdentifiantVol, flightConfigurations);                                
                                if (flightConfiguration != null)
                                {
                                    if (bDeparture)
                                    {
                                        dTotalNumberOfPassengers = flightConfiguration.flightParameter.nbOrigEcoPax + flightConfiguration.flightParameter.nbOrigFbPax
                                            + flightConfiguration.flightParameter.nbTransferEcoPax + flightConfiguration.flightParameter.nbTransferFbPax;
                                        dTotalNumberOfBags = flightConfiguration.flightParameter.nbOrigEcoBags + flightConfiguration.flightParameter.nbOrigFbBags
                                            + flightConfiguration.flightParameter.nbTransferEcoBags + flightConfiguration.flightParameter.nbTransferFbBags;                                            
                                    }
                                    else
                                    {
                                        dTotalNumberOfPassengers = flightConfiguration.flightParameter.nbTransferEcoPax + flightConfiguration.flightParameter.nbTransferFbPax
                                            + flightConfiguration.flightParameter.nbTermEcoPax + flightConfiguration.flightParameter.nbTermFbPax;
                                        dTotalNumberOfBags = flightConfiguration.flightParameter.nbTransferEcoBags + flightConfiguration.flightParameter.nbTransferFbBags
                                            + flightConfiguration.flightParameter.nbTermEcoBags + flightConfiguration.flightParameter.nbTermFbBags;
                                    }                                    
                                }
                                // << Task #17690 PAX2SIM - Flight Plan Parameters table

                                int iNumberOfContainer = (int)Math.Ceiling(dTotalNumberOfBags / oiMakeUpInformation.dContainerSize);
                                int iNumberOfCycle = (int)Math.Ceiling((Double)iNumberOfContainer / (double)oiMakeUpInformation.iNbContainerPerCycle);
                                Double dWaitingForCycle = (Double)(iNumberOfCycle - 1) * oiMakeUpInformation.dDeadTime;

                                dateFinAlloc = (DateTime)drRow[sOpeningDeskColumn];
                                dateFinAlloc = dateFinAlloc.AddMinutes(dWaitingForCycle);
                                DateTime dateFinAllocDoubleInfeed = dateFinAlloc.AddSeconds((dInfeedSpeed * dTotalNumberOfBags) / 2);
                                dateFinAlloc = dateFinAlloc.AddSeconds(dInfeedSpeed * dTotalNumberOfBags);
                                Double dTimeFinAllocDoubleInfeed = OverallTools.DataFunctions.MinuteDifference(dtBegin_, dateFinAllocDoubleInfeed);
                                iPosFinAllocDoubleInfeed = (int)(dTimeFinAllocDoubleInfeed / dStep_);
                                if (((Double)iPosFinAllocDoubleInfeed) == dTimeFinAllocDoubleInfeed / dStep_)
                                    iPosFinAllocDoubleInfeed--;                                
                            }
                        }
                    }
                    #endregion

                    dTimeDebutAlloc = OverallTools.DataFunctions.MinuteDifference(dtBegin_, dateDebutAlloc);
                    dTimeFinAlloc = OverallTools.DataFunctions.MinuteDifference(dtBegin_, dateFinAlloc);
                    iPosDebutAlloc = (int)(dTimeDebutAlloc / dStep_);
                    iPosFinAlloc = (int)(dTimeFinAlloc / dStep_);

                    Double dTimePartialEnd = OverallTools.DataFunctions.MinuteDifference(dtBegin_, DatePatialClosing);
                    int iPosPartialClosing = (int)(dTimePartialEnd / dStep_);
                    if (((Double)iPosFinAlloc) == dTimeFinAlloc / dStep_)
                        iPosFinAlloc--;
                    if ((iPosFinAlloc < 0) || (iPosDebutAlloc > dtResults[0].Rows.Count))
                    {
                        alErrors_.Add("Warn00334 : Flight ignored \"" + sId + "\". its opening area out of the dates provided. it would be ignored");
                        continue;
                    }
                    iPosDebutLibre = iPosDebutAlloc - (int)dTimeBetweenFligths;
                    iPosFinLibre = iPosFinAlloc + (int)dTimeBetweenFligths;

                    if (iPosDebutLibre < 0)
                        iPosDebutLibre = 0;
                    if (iPosDebutAlloc < 0)
                        iPosDebutAlloc = 0;
                    if (iPosPartialClosing < 0)
                        iPosPartialClosing = 0;
                    if (iPosPartialClosing > dtResults[0].Rows.Count)
                        iPosPartialClosing = dtResults[0].Rows.Count - 1;
                    if (iPosFinAlloc >= dtResults[0].Rows.Count)
                        iPosFinAlloc = dtResults[0].Rows.Count - 1;
                    if (iPosFinLibre >= dtResults[0].Rows.Count)
                        iPosFinLibre = dtResults[0].Rows.Count - 1;


                    #region (In the case of the reclaim allocation.)
                    iPosFinLibreDoubleInfeed = 0;
                    if (useInfeedLimitation && iIndexContainerSize != -1)
                    {
                        iPosFinLibreDoubleInfeed = iPosFinAllocDoubleInfeed + (int)dTimeBetweenFligths;
                        if (iPosFinAllocDoubleInfeed >= dtResults[0].Rows.Count)
                            iPosFinAllocDoubleInfeed = dtResults[0].Rows.Count - 1;
                        if (iPosFinLibreDoubleInfeed >= dtResults[0].Rows.Count)
                            iPosFinLibreDoubleInfeed = dtResults[0].Rows.Count - 1;
                    }
                    #endregion

                    int iCurrentTolerance = 1;
                    if (bOverlappingConstraints)
                    {
                        iCurrentTolerance = dssfoOverlappingParameters[sCurrentFC].MaxOverlap;
                    }
                    /*if (bUseFlightCategoryTolerance)
                    {
                        if (alSHFlightCategories.Contains(sCurrentFC))
                            iCurrentTolerance = iToleranceSH_;
                        else
                            iCurrentTolerance = iToleranceLH_;
                    }*/
                    Point[] pAreas = null;
                    //OverallTools.ExternFunctions.PrintLogFile(sId );

                    // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                    //if the use container size option is enabled we calculate the number of positions needed based on
                    //the container size of the flight: nbPos = round(flightSeats*loadingFactor*nbBagsPerPax / containerSize)
                    int nbOfAllocatedPositions = oiMakeUpInformation.NombrePostes;
                    int partialNbOfAllocatedPositions = oiMakeUpInformation.PartialNumber;

                    double dTotalNumberOfPax = 0;
                    double dTotalNumberOfBagages = 0;
                    
                    if (dtNbBagage != null && dtAircraftTable_ != null && dtLoadFactors != null)
                    {
                        int iColumn;
                        dtNbBagage.UseException = UseNbBaggageExceptions;
                        DataTable dtTableBags = dtNbBagage.GetInformationsColumns(0, sFlightID, sAirline, sCurrentFC, out iColumn);
                        //dtNbBagage.UseException = true;                        

                        if (dtTableBags != null)
                        {
                            Double dMeanBaggage = OverallTools.StaticAnalysis.CalculCoefficientMultiplicateur(dtTableBags, iColumn);
                            if (dMeanBaggage != -1)
                            {
                                dtLoadFactors.UseException = UseLoadFactorExceptions;
                                dtAircraftTable_.UseException = UseAircraftTableExceptions;
                                dTotalNumberOfPax = OverallTools.DataFunctions.numberOfPassengers(drRow, dtAircraftTable_, null, dtLoadFactors, alErrors_);
                                //dtLoadFactors.UseException = true;
                                //dtAircraftTable_.UseException = true;

                                if (dTotalNumberOfPax == -1)
                                {
                                    alErrors_.Add("Err00681 : Flight ignored : \"" + sId + "\". The number of passenger for that fligt is not valid.");
                                    continue;
                                }
                                dTotalNumberOfBagages = Math.Round(dTotalNumberOfPax * dMeanBaggage, 2);

                                // >> Task #17690 PAX2SIM - Flight Plan Parameters table                                    
                                FlightConfiguration flightConfiguration = null;
                                FlightAttribute.ARR_OR_DEP_FLIGHT_TAG arrOrDep = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.A;
                                if (bDeparture)
                                {
                                    arrOrDep = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.D;
                                }
                                flightConfiguration = Tools.FlightPlanParametersTools
                                    .getFlightConfigurationByArrOrDepAndFlightId(arrOrDep, iIdentifiantVol, flightConfigurations);                                
                                if (flightConfiguration != null)
                                {
                                    if (bDeparture)
                                    {
                                        dTotalNumberOfPax = flightConfiguration.flightParameter.nbOrigEcoPax + flightConfiguration.flightParameter.nbOrigFbPax
                                            + flightConfiguration.flightParameter.nbTransferEcoPax + flightConfiguration.flightParameter.nbTransferFbPax;
                                        dTotalNumberOfBagages = flightConfiguration.flightParameter.nbOrigEcoBags + flightConfiguration.flightParameter.nbOrigFbBags
                                            + flightConfiguration.flightParameter.nbTransferEcoBags + flightConfiguration.flightParameter.nbTransferFbBags;                                            
                                    }
                                    else
                                    {
                                        dTotalNumberOfPax = flightConfiguration.flightParameter.nbTransferEcoPax + flightConfiguration.flightParameter.nbTransferFbPax
                                            + flightConfiguration.flightParameter.nbTermEcoPax + flightConfiguration.flightParameter.nbTermFbPax;
                                        dTotalNumberOfBagages = flightConfiguration.flightParameter.nbTransferEcoBags + flightConfiguration.flightParameter.nbTransferFbBags
                                            + flightConfiguration.flightParameter.nbTermEcoBags + flightConfiguration.flightParameter.nbTermFbBags;
                                    }                                    
                                }
                                // << Task #17690 PAX2SIM - Flight Plan Parameters table

                                if (calculateBasedOnContainerSize)
                                {
                                    int nbContainers = (int)Math.Ceiling(dTotalNumberOfBagages / oiMakeUpInformation.dContainerSize);

                                    if (nbContainers > 0)
                                    {
                                        nbOfAllocatedPositions = nbContainers;
                                        partialNbOfAllocatedPositions = nbContainers;
                                    }
                                }
                            }
                        }
                    }
                    
                    // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size

                    try
                    {
                        pAreas = CheckEmptyArea(dtResults[0],
                                                            iOffsetDesks /*+ iIndexLastSegregation*/,
                                                            iPosDebutLibre,
                                                            iPosPartialClosing,
                                                            iPosFinLibre,
                                                            nbOfAllocatedPositions,//oiMakeUpInformation.NombrePostes,// << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                                                            partialNbOfAllocatedPositions,//oiMakeUpInformation.PartialNumber,// << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                                                            1,
                                                            iMinNbRessources,
                                                            ldisOverlapping,
                            /*alFlightCategorieByLine,*/
                                                            sCurrentFC,
                                                            iCurrentTolerance,
                                                            iLastDoubleInfeed,
                                                            iPosFinLibreDoubleInfeed);
                    }
                    catch (Exception e)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Exception while trying to find an area in the allocation tools" + e.ToString());
                        continue;
                    }

                    if (bAllocateFlightPlan_)
                    {
                        int iIndexLigne = OverallTools.DataFunctions.indexLigne(dtFPTable_, iIndexID, iIdentifiantVol.ToString());
                        if (iIndexLigne != -1)
                        {
                            dtFPTable_.Rows[iIndexLigne][iIndexTerminalResult] = iTerminal_.ToString();

                            // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering 
                            int firstDeskNb = 0;
                            int lastDeskNb = 0;
                            if (!bContinousSegregationNumerotation)
                            {
                                firstDeskNb = ((Int32)(pAreas[0].X - iOffsetDesks + 1)) + allocationOffset;    // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                                lastDeskNb = ((Int32)(pAreas[0].Y - iOffsetDesks + 1)) + allocationOffset;    // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                            }
                            else
                            {
                                firstDeskNb = ((Int32)(pAreas[0].X - iOffsetDesks
                                    + 1 + lastResourceNbFromPreviousSegregation)) + allocationOffset;    // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                                lastDeskNb = ((Int32)(pAreas[0].Y - iOffsetDesks
                                    + 1 + lastResourceNbFromPreviousSegregation)) + allocationOffset;    // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                            }

                            dtFPTable_.Rows[iIndexLigne][iIndexFirstColumnResult] = firstDeskNb.ToString();
                            dtFPTable_.Rows[iIndexLigne][iIndexLastColumnResult] = lastDeskNb.ToString();

                            //dtFPTable_.Rows[iIndexLigne][iIndexFirstColumnResult] = ((Int32)(pAreas[0].X - iOffsetDesks + 1)).ToString();
                            //dtFPTable_.Rows[iIndexLigne][iIndexLastColumnResult] = ((Int32)(pAreas[0].Y - iOffsetDesks + 1)).ToString();
                            // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
                            
                            
                            if (iIndexFirstColumnResultFirst != -1)
                            {
                                dtFPTable_.Rows[iIndexLigne][iIndexFirstColumnResultFirst] = "0";
                                dtFPTable_.Rows[iIndexLigne][iIndexLastColumnResultFirst] = "0";
                            }
                        }
                    }
                    iAllocatedFlight++;
                    for (k = 0; k < pAreas.Length; k++)
                    {
                        //If we are here, that means that we have find the good allocation.
                        //The first column is i and the last is i + iLastTested
                        for (i = pAreas[k].X; i <= pAreas[k].Y; i++)
                        {
                            while (dtResults[0].Columns.Count <= i)
                            {
                                String sNewName = sColumnsName;
                                #region (In the case of the reclaim allocation.)
                                if (dtResults[0].Columns.Count < iLastDoubleInfeed)
                                {
                                    sNewName += "DoubleInfeed_";
                                }
                                #endregion

                                // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
                                if (!bContinousSegregationNumerotation)
                                    dtResults[0].Columns.Add(sNewName
                                        + (dtResults[0].Columns.Count - iOffsetDesks + 1 + allocationOffset).ToString(),
                                        typeof(String));    // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                                else
                                {
                                    currentResourceNumber++;
                                    dtResults[0].Columns.Add(sNewName
                                        + (currentResourceNumber + allocationOffset).ToString(), typeof(String));   // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                                }
                                //dtResults[0].Columns.Add(sNewName + (dtResults[0].Columns.Count - iOffsetDesks + 1).ToString(), typeof(String));
                                // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
                                
                            }

                            j = iPosDebutAlloc;
                            iPosFinLibre = iPosFinAlloc;
                            #region (In the case of the reclaim allocation.)
                            if (i < iLastDoubleInfeed)
                            {
                                iPosFinLibre = iPosFinAllocDoubleInfeed;
                            }
                            #endregion
                            if ((k == 0) && (pAreas.Length > 1))
                            {
                                iPosFinLibre = iPosPartialClosing - 1;
                            }
                            else if (pAreas.Length > 1)
                                j = iPosPartialClosing;
                            for (; j <= iPosFinLibre; j++)
                            {
                                if (bOverlappingConstraints)
                                {
                                    if (!ldisOverlapping[j].ContainsKey(i))
                                    {
                                        ldisOverlapping[j].Add(i, dssfoOverlappingParameters[sCurrentFC]);
                                    }
                                }
                                /*if (bUseFlightCategoryTolerance)
                                {
                                    Hashtable htTmp = (Hashtable)alFlightCategorieByLine[j];
                                    if (!htTmp.ContainsKey(i))
                                        htTmp.Add(i, sCurrentFC);
                                }*/
                                sValue = dtResults[0].Rows[j][i].ToString();
                                if (sValue.Length > 0)
                                    sValue += ", ";
                                sValue += sId;
                                dtResults[0].Rows[j][i] = sValue;
                                if (bColorByFC_)
                                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[0].ConditionnalFormatClass[0]).setCondition(i, j, cCurrentColor);
                                if (bColorByHandler_)
                                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[iIndexHandlingColor].ConditionnalFormatClass[0]).setCondition(i, j, cHandlingColor);
                                if (bColorByAirline_)
                                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[iIndexAirlineColor].ConditionnalFormatClass[0]).setCondition(i, j, cAirlineColor);
                            }
                        }
                    }
                    #region Scenario Allocation FlightPlan Information table for gantt
                    // add the information about the allocated flight in the allocFPInfoDictionary dictionary
                    // the dictionary will be used to insert the informations into the Flight Plan Information tabel
                    #region Variables which hold the information from the Flight Plan
                    int indexColumnID = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                    int indexColumnDate = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                    int indexColumnSTA = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
                    int indexColumnSTD = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
                    int indexColumnAirlineCode = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                    int indexColumnFlightNb = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                    int indexColumnAirportCode = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                    int indexColumnFlightCategory = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                    int indexColumnAircraftType = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                    int indexColumnNoBSM = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
                    int indexColumnCBP = dtFPTable_.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
                    int indexColumnTSA = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
                    int indexColumnNbSeats = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
                    int indexColumnUser1 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                    int indexColumnUser2 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                    int indexColumnUser3 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                    int indexColumnUser4 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                    int indexColumnUser5 = dtFPTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);

                    int flightId = -1;
                    DateTime date = new DateTime();
                    TimeSpan sta_d = new TimeSpan();
                    String airlineCode = "";
                    String flightNb = "";
                    String airportCode = "";
                    String flightCategory = "";
                    String aircraftType = "";
                    Boolean noBSM = false;
                    Boolean CBP = false;
                    Boolean TSA = false;
                    int nbSeats = -1;
                    String user1 = "";
                    String user2 = "";
                    String user3 = "";
                    String user4 = "";
                    String user5 = "";

                    if (indexColumnID != -1)
                        flightId = int.Parse(drRow[indexColumnID].ToString());
                    if (indexColumnDate != -1)
                        date = DateTime.Parse(drRow[indexColumnDate].ToString());
                    if (indexColumnSTA != -1)
                        sta_d = TimeSpan.Parse(drRow[indexColumnSTA].ToString());
                    else if (indexColumnSTD != -1)
                        sta_d = TimeSpan.Parse(drRow[indexColumnSTD].ToString());
                    if (indexColumnAirlineCode != -1)
                        airlineCode = drRow[indexColumnAirlineCode].ToString();
                    if (indexColumnFlightNb != -1)
                        flightNb = drRow[indexColumnFlightNb].ToString();
                    if (indexColumnAirportCode != -1)
                        airportCode = drRow[indexColumnAirportCode].ToString();
                    if (indexColumnFlightCategory != -1)
                        flightCategory = drRow[indexColumnFlightCategory].ToString();
                    if (indexColumnAircraftType != -1)
                        aircraftType = drRow[indexColumnAircraftType].ToString();
                    if (indexColumnNoBSM != -1)
                        noBSM = bool.Parse(drRow[indexColumnNoBSM].ToString());
                    if (indexColumnCBP != -1)
                        CBP = bool.Parse(drRow[indexColumnCBP].ToString());
                    if (indexColumnTSA != -1)
                        TSA = bool.Parse(drRow[indexColumnTSA].ToString());
                    if (indexColumnNbSeats != -1)
                        nbSeats = int.Parse(drRow[indexColumnNbSeats].ToString());
                    if (indexColumnUser1 != -1)
                        user1 = drRow[indexColumnUser1].ToString();
                    if (indexColumnUser1 != -1)
                        user2 = drRow[indexColumnUser2].ToString();
                    if (indexColumnUser1 != -1)
                        user3 = drRow[indexColumnUser3].ToString();
                    if (indexColumnUser1 != -1)
                        user4 = drRow[indexColumnUser4].ToString();
                    if (indexColumnUser1 != -1)
                        user5 = drRow[indexColumnUser5].ToString();
                    #endregion
                                       
                    String groundHandler = OverallTools.DataFunctions.getValue(dtAirlineTable_, sAirline, iIndexAirlineAirline, iIndexAirlineGroundHandlers);

                    // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering 
                    int firstDesk = 0;
                    int lastDesk = 0;
                    if (!bContinousSegregationNumerotation)
                    {
                        firstDesk = ((Int32)(pAreas[0].X - iOffsetDesks + 1)) + allocationOffset;   // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                        lastDesk = ((Int32)(pAreas[0].Y - iOffsetDesks + 1)) + allocationOffset;   // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                    }
                    else
                    {
                        firstDesk = ((Int32)(pAreas[0].X - iOffsetDesks
                            + 1 + lastResourceNbFromPreviousSegregation)) + allocationOffset;   // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                        lastDesk = ((Int32)(pAreas[0].Y - iOffsetDesks
                            + 1 + lastResourceNbFromPreviousSegregation)) + allocationOffset;   // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                    }
                    //int firstDesk = ((Int32)(pAreas[0].X - iOffsetDesks + 1));
                    //int lastDesk = ((Int32)(pAreas[0].Y - iOffsetDesks + 1));
                    // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering

                    // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                    //int lastDeskAtClosing = firstDesk + oiMakeUpInformation.PartialNumber - 1;
                    int lastDeskAtClosing = firstDesk + partialNbOfAllocatedPositions - 1;
                    // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                                        
                    // used as key for the allocatedFlightInfo dictionary
                    FPITableIndex++;
                    if (DateTime.Compare(dateDebutAlloc, dtBegin_) < 0)
                        dateDebutAlloc = dtBegin_;
                    if (DateTime.Compare(dateDebutAlloc, DatePatialClosing) < 0 && DateTime.Compare(DatePatialClosing, dateFinAlloc) < 0
                        && lastDesk != lastDeskAtClosing)
                    {
                        allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNb, airportCode,
                                                        flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                        oiMakeUpInformation.dContainerSize, dTotalNumberOfPax, dTotalNumberOfBagages,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                        firstDesk, lastDesk,
                                                        dateDebutAlloc, DatePatialClosing, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                        AllocationType.ToString(), sNomColonneAllocation,
                                                        calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                        if (allocatedFlightInfo != null)
                            allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);
                        allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNb, airportCode,
                                                        flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                        oiMakeUpInformation.dContainerSize, dTotalNumberOfPax, dTotalNumberOfBagages,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                        firstDesk, lastDeskAtClosing,
                                                        DatePatialClosing, dateFinAlloc, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                        AllocationType.ToString(), sNomColonneAllocation,
                                                        calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                        FPITableIndex++;
                        if (allocatedFlightInfo != null)
                            allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);
                    }
                    else
                    {
                        // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                        if (useInfeedLimitation && iLastDoubleInfeed > 0)
                        {
                            if (firstDesk == lastDesk && firstDesk <= iNumberOfDoubleInfeed)
                            {
                                //we use a Doubled-Infeed Station => 2xSpeed => 1/2 time
                                TimeSpan timeDifference = dateFinAlloc.Subtract(dateDebutAlloc);
                                dateFinAlloc = dateDebutAlloc.AddSeconds(timeDifference.TotalSeconds / 2);
                            }
                        }
                        // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size

                        allocatedFlightInfo = new AllocationFlightPlanInformation(flightId, date, sta_d, airlineCode, flightNb, airportCode,
                                                        flightCategory, aircraftType, noBSM, CBP, TSA, nbSeats,
                                                        oiMakeUpInformation.dContainerSize, dTotalNumberOfPax, dTotalNumberOfBagages,   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                                                        firstDesk, lastDesk,
                                                        dateDebutAlloc, dateFinAlloc, Terminal, groundHandler, user1, user2, user3, user4, user5,
                                                        AllocationType.ToString(), sNomColonneAllocation,
                                                        calculationType, octTableDescription);  // << Task #9465 Pax2Sim - Allocation - Add FPI Columns
                        if (allocatedFlightInfo != null)
                            allocFPInfoDictionary.Add(FPITableIndex.ToString(), allocatedFlightInfo);
                    }
                    #endregion
                }
                //iIndexLastSegregation = dtResults[0].Columns.Count - iOffsetDesks;
            }
            #endregion

            #region Gestion des statistiques sur les colonnes.
            CheckLastColumns(dtResults[0], ShowFlightNumber);
            dtFPTable_.AcceptChanges();

            if (!lsaiInformation.ContainsKey(sColumnsName))
                lsaiInformation.Add(sColumnsName, new SegregationAllocationInformation(sColumnsName, iExpectedFlight, iAllocatedFlight, dtResults[0].Columns.Count - iOffsetDesks));
            else
            {
                if (dtResults[0].Columns.Count < iOffsetDesks)
                    lsaiInformation[sColumnsName].ResetStatistics(0);
                else
                    lsaiInformation[sColumnsName].ResetStatistics(dtResults[0].Columns.Count - iOffsetDesks);
                lsaiInformation[sColumnsName].AddFligts(iExpectedFlight, iAllocatedFlight);
            }
            AnalyseTableOccupation(dtResults[0], iOffsetDesks, iIndexOccupied, iIndexNeeded, iIndexFlightNumber, lsaiInformation[sColumnsName]);

            int iNumberColumnsRemoved = AnalyseTableOccupation(dtResults[0]);
            if ((iNumberColumnsRemoved != 0) && (iNbVisualisation > 0))
            {
                if (bColorByHandler_)
                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[iIndexHandlingColor].ConditionnalFormatClass[0]).RemoveColumn(2, iNumberColumnsRemoved);
                if (bColorByAirline_)
                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[iIndexAirlineColor].ConditionnalFormatClass[0]).RemoveColumn(2, iNumberColumnsRemoved);
                if (bColorByFC_)
                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[0].ConditionnalFormatClass[0]).RemoveColumn(2, iNumberColumnsRemoved);
            }

            #endregion

            DataTable dtTmp = dtResults[0];
            dtResults = new DataTable[iNbVisualisation];
            if (iNbVisualisation == 0)
                dtResults = new DataTable[1];
            dtResults[0] = dtTmp;
            /*Pour mettre la légende dans la seconde colonne de la table.*/
            dtResults[0].AcceptChanges();
            #region Génération des tables avec les couleurs et insertion des légendes.
            if (bColorByHandler_)
            {
                dtResults[iIndexHandlingColor] = dtResults[0].Copy();
                dtResults[iIndexHandlingColor].TableName = /*dtResults[iIndexHandlingColor].TableName + "_*/"Handling";
                for (k = 0; k < alHandlingCompanies.Count; k++)
                {
                    if (dtResults[iIndexHandlingColor].Rows.Count <= k) continue;
                    dtResults[iIndexHandlingColor].Rows[k][1] = alHandlingCompanies[k].ToString();
                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[iIndexHandlingColor].ConditionnalFormatClass[0]).setCondition(1, k, OverallTools.FonctionUtiles.getColor(k));
                }
                for (k = alHandlingCompanies.Count; k < dtResults[iIndexHandlingColor].Rows.Count; k++)
                    dtResults[iIndexHandlingColor].Rows[k][1] = "";
                dtResults[iIndexHandlingColor].AcceptChanges();
            }
            if (bColorByAirline_)
            {
                dtResults[iIndexAirlineColor] = dtResults[0].Copy();
                dtResults[iIndexAirlineColor].TableName = /*dtResults[iIndexAirlineColor].TableName + "_*/"Airline";
                for (k = 0; k < alAirlineCode.Count; k++)
                {
                    if (dtResults[iIndexAirlineColor].Rows.Count <= k) continue;
                    dtResults[iIndexAirlineColor].Rows[k][1] = alAirlineCode[k].ToString();
                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[iIndexAirlineColor].ConditionnalFormatClass[0]).setCondition(1, k, OverallTools.FonctionUtiles.getColor(k));
                }
                for (k = alAirlineCode.Count; k < dtResults[iIndexAirlineColor].Rows.Count; k++)
                    dtResults[iIndexAirlineColor].Rows[k][1] = "";
                dtResults[iIndexAirlineColor].AcceptChanges();
            }
            if (bColorByFC_)
            {
                dtResults[0].TableName = /*dtResults[0].TableName + "_*/"FlightCategory";
                for (k = 0; k < alFlightCategory.Count; k++)
                {
                    if (dtResults[0].Rows.Count <= k) continue;
                    dtResults[0].Rows[k][1] = alFlightCategory[k].ToString();
                    //On génère la couleur à k+1 car lors de la génération de la table oct il y avait un décallage de 1.
                    ((ConditionnalFormatErrors)tvmAllocationVisualisation_[0].ConditionnalFormatClass[0]).setCondition(1, k, OverallTools.FonctionUtiles.getColor(k + 1));
                }
                for (k = alFlightCategory.Count; k < dtResults[0].Rows.Count; k++)
                    dtResults[0].Rows[k][1] = "";
                dtResults[0].AcceptChanges();
            }
            #endregion
            if (iNbVisualisation == 0)
            {
                dtResults[0].Columns.Remove("Legend");
            }
            dtErrors = getErrors();
            dtStatistiques = SegregationAllocationInformation.GetTable();
            foreach (String sKey in lsaiInformation.Keys)
            {
                Object[] oTmp = lsaiInformation[sKey].getRow();
                if(oTmp!=null)
                dtStatistiques.Rows.Add(oTmp);
            }
            #region Insert the flight plan information in the AllocationFlightPlanInformation table
            dtFlightPlanInformation = AllocationFlightPlanInformation.getTable(sNomColonneAllocation, bDeparture, sObservedTerminal_);
            foreach (String key in allocFPInfoDictionary.Keys)
            {
                Object[] rowForFPITable = allocFPInfoDictionary[key].getRow(bDeparture);
                if (rowForFPITable != null)
                    dtFlightPlanInformation.Rows.Add(rowForFPITable);
            }            
            dtFlightPlanInformation.AcceptChanges();
            // sort the table by ID
            if (dtFlightPlanInformation.Rows.Count > 0)
            {
                DataView auxFlightPlanInformationView = dtFlightPlanInformation.DefaultView;
                auxFlightPlanInformationView.Sort = GlobalNames.sFPD_A_Column_ID;
                dtFlightPlanInformation = auxFlightPlanInformationView.ToTable();
            }

            #endregion

            return true;
        }

        // >> Task #12741 Pax2Sim - Dubai - flight exceptions
        private class FlightOCTInformation
        {
            internal string flightTag { get; set; }
            internal double openingTime { get; set; }
            internal double closingTime { get; set; }
        }
        private NormalTable createOCTFlightExceptionTableUsingDownTime(DataTable octTable, DataTable flightPlan, String openingTimesColumnName)
        {
            NormalTable flightExceptionNTable = new NormalTable(octTable.TableName, octTable);

            while (flightExceptionNTable.Table.Columns.Count > 1)
                flightExceptionNTable.Table.Columns.RemoveAt(flightExceptionNTable.Table.Columns.Count - 1);

            Dictionary<string, FlightOCTInformation> flightOCTInformationDictionary = new Dictionary<string, FlightOCTInformation>();
            String flightTagPrefix = "A_";
            if (isDepartureAllocation(AllocationType))
                flightTagPrefix = "D_";

            int idColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int downTimeColumnIndex = flightPlan.Columns.IndexOf(openingTimeFlightPlanColumnName);//(GlobalNames.sFPD_A_Column_User2);

            if (idColumnIndex != -1 && downTimeColumnIndex != -1)
            {
                foreach (DataRow row in flightPlan.Rows)
                {
                    if (row[idColumnIndex] != null && row[downTimeColumnIndex] != null)
                    {
                        double id = -1;
                        double downTimeValue = -1;
                        if (Double.TryParse(row[downTimeColumnIndex].ToString(), out downTimeValue)
                            && Double.TryParse(row[idColumnIndex].ToString(), out id)
                            && id > 0 && downTimeValue >= 0)
                        {                            
                            FlightOCTInformation flightOCTInformation = new FlightOCTInformation();
                            flightOCTInformation.flightTag = flightTagPrefix + id;
                            flightOCTInformation.openingTime = downTimeValue;
                            flightOCTInformation.closingTime = 0;
                            flightOCTInformationDictionary.Add(flightOCTInformation.flightTag, flightOCTInformation);
                        }
                    }
                }
                foreach (KeyValuePair<String, FlightOCTInformation> pair in flightOCTInformationDictionary)
                {
                    DataColumn col = new DataColumn(pair.Key, typeof(double));
                    flightExceptionNTable.Table.Columns.Add(col);                    
                }
                foreach (DataColumn col in flightExceptionNTable.Table.Columns)
                {
                    if (col.ColumnName != GlobalNames.sColumnSelectACategory)
                    {
                        if (flightOCTInformationDictionary.ContainsKey(col.ColumnName))
                        {
                            FlightOCTInformation flightOCT = flightOCTInformationDictionary[col.ColumnName];
                            flightExceptionNTable.Table.Rows[0][col] = flightOCT.openingTime;
                            flightExceptionNTable.Table.Rows[1][col] = flightOCT.closingTime;
                        }
                    }
                }
                flightExceptionNTable.Table.AcceptChanges();                
            }
            return flightExceptionNTable;
        }
        // << Task #12741 Pax2Sim - Dubai - flight exceptions
        #endregion

        internal DataTable getErrors()
        {
            if(alErrors_ == null)
                return null;
            if(alErrors_.Count == 0)
                return null;
            DataTable dtErrors = new DataTable("Generation errors");
            dtErrors.Columns.Add("Error",typeof(String));
            foreach(String sError in alErrors_)
            {
                dtErrors.Rows.Add(new Object[]{sError});
            }
            return dtErrors;
        }
        private static void AnalyseTableOccupation(DataTable dtAllocationTable, int iStartColumn, int iUsedColumn, int iNeededColumn, int iFlightNumberColumn, SegregationAllocationInformation  saiInformations)
        {
            if ((iStartColumn == -1) || (iUsedColumn == -1) || (iNeededColumn == -1))
                return;
            if (iFlightNumberColumn >= dtAllocationTable.Columns.Count)
                iFlightNumberColumn = -1;
            int iLastColumn = dtAllocationTable.Columns.Count;
            if ((iStartColumn >= iLastColumn) || (iUsedColumn >= iLastColumn) || (iNeededColumn >= iLastColumn))
                return;
            List<Int32> liNumberOfFlightPerDesk;
            foreach (DataRow drRow in dtAllocationTable.Rows)
            {
                int iNumber = 0;
                int iLastIndex = iStartColumn - 1;
                List<String> lsFlightName = new List<string>();
                liNumberOfFlightPerDesk = new List<int>();
                for (int i = iStartColumn; i < dtAllocationTable.Columns.Count; i++)
                {
                    if (drRow[i].ToString().Length > 0)
                    {
                        if (iFlightNumberColumn != -1)
                        {
                            String[] lsNames = drRow[i].ToString().Split(',');
                            foreach (String sName in lsNames)
                            {
                                if (sName != null)  // >> Bug #11488 Pax2Sim - Allocation (static) - Allocation table_Flight number
                                {
                                    if (!lsFlightName.Contains(sName.Trim()))
                                        lsFlightName.Add(sName.Trim());
                                }
                            }
                            liNumberOfFlightPerDesk.Add(lsNames.Length);

                        }
                        iNumber++;
                        iLastIndex = i;
                    }
                }
                if ((iFlightNumberColumn!=-1)&&(saiInformations != null))
                    saiInformations.AddStatsPerSlot(lsFlightName.Count, liNumberOfFlightPerDesk, iLastIndex - iStartColumn+1);
                if (iNumber == 0)
                {
                    iLastIndex = 0;
                }
                else
                {
                    iLastIndex = iLastIndex - iStartColumn + 1;
                }
                drRow[iUsedColumn] = iLastIndex;
                drRow[iNeededColumn] = iNumber;
                if (iFlightNumberColumn != -1)
                    drRow[iFlightNumberColumn] = lsFlightName.Count;
            }
            dtAllocationTable.AcceptChanges();
        }
        /// <summary>
        /// Function that will update (if they are existing) the columns for the global statistics of the allocation.
        /// </summary>
        /// <param name="dtAllocationTable">The table where the allocations had been done.</param>
        private static int AnalyseTableOccupation(DataTable dtAllocationTable)
        {
            
            int iRemoved = 0;
            #region We verify the available global columns
            int iIndexNeed = dtAllocationTable.Columns.IndexOf(sGlobal + "_" +sNeed);
            if (iIndexNeed != -1)
            {
                iRemoved++;
                dtAllocationTable.Columns.Remove(sGlobal + "_" + sNeed);
            }
            int iIndexOccupied = dtAllocationTable.Columns.IndexOf(sGlobal + "_" + sOccupied);
            int iIndexFlightNumber = dtAllocationTable.Columns.IndexOf(sGlobal + "_" + sFlightNumber);
            #endregion
            List<int> liOccupiedIndex = new List<int>();
            List<int> liFlightNumberIndex = new List<int>();

            #region We get all the indexes for the different objects
            for (int i = 0; i < dtAllocationTable.Columns.Count; i++)
            {
                if (dtAllocationTable.Columns[i].ColumnName.Contains(sOccupied))
                {
                    if (i != iIndexOccupied)
                        liOccupiedIndex.Add(i);
                }
                if (dtAllocationTable.Columns[i].ColumnName.Contains(sFlightNumber))
                {
                    if (i != iIndexFlightNumber)
                        liFlightNumberIndex.Add(i);
                }
            }
            #endregion

            #region We removed the columns if we need to do so.
            if ((iIndexOccupied == -1) || (liOccupiedIndex.Count <= 1))
            {
                if (iIndexOccupied != -1)
                {
                    iRemoved++;
                    dtAllocationTable.Columns.Remove(sGlobal + "_" + sOccupied);
                }
                if (iIndexFlightNumber != -1)
                {
                    iRemoved++;
                    dtAllocationTable.Columns.Remove(sGlobal + "_" + sFlightNumber);
                }
                return iRemoved;
            }
            #endregion
            foreach(DataRow drRow in dtAllocationTable.Rows)
            {
                int iOccupied = 0;

                foreach (int i in liOccupiedIndex)
                {
                    iOccupied += FonctionsType.getInt(drRow[i]);
                }
                drRow[iIndexOccupied] = iOccupied;
                if (iIndexFlightNumber != -1)
                {
                    int iFlightNumber = 0;
                    foreach (int i in liFlightNumberIndex)
                    {
                        iFlightNumber += FonctionsType.getInt(drRow[i]);
                    }
                    drRow[iIndexFlightNumber] = iFlightNumber;
                }
            }

            return iRemoved;
        }

        #region The generation of the table, and creation of Analysis information.
        private static Object[] CreateAnalyse(ArrayList alFlightCategories, String sGroundHandlers, String sULDLoose)
        {
            return new object[] { alFlightCategories, sGroundHandlers, sULDLoose };
        }

        /// <summary>
        /// Cette fonction va s'occuper d'ajouter les colonnes need et occupied (et Flight number si désirée)
        /// need : Nombre de ressources nécessaires (vides incluses)
        /// occupied : Nombre de ressources réellement occupées sur le créneau
        /// flight number : Nombre de vols alloués sur le créneau.
        /// </summary>
        /// <param name="dtAllocationTable"></param>
        /// <param name="sName"></param>
        /// <param name="bShowFlightNumber"></param>
        /// <returns></returns>
        private static bool AddOccupationDesksColumns(DataTable dtAllocationTable, String sName, Boolean bShowFlightNumber)
        {
            if (dtAllocationTable == null)
                return false;
            if (dtAllocationTable.Columns.Contains(sName + sNeed))
                return false;
            if (dtAllocationTable.Columns.Contains(sName + sOccupied))
                return false;
            if (bShowFlightNumber && dtAllocationTable.Columns.Contains(sName + sFlightNumber))
                return false;
            int iLastIndex = dtAllocationTable.Columns.Count - 1;
            CheckLastColumns(dtAllocationTable, bShowFlightNumber);
            
            dtAllocationTable.Columns.Add(sName + sNeed, typeof(Int32));
            dtAllocationTable.Columns.Add(sName + sOccupied, typeof(Int32));
            if (bShowFlightNumber)
                dtAllocationTable.Columns.Add(sName + sFlightNumber, typeof(Int32));
            foreach (DataRow drRow in dtAllocationTable.Rows)
            {
                drRow[sName + sNeed] = 0;
                drRow[sName + sOccupied] = 0;
                if (bShowFlightNumber)
                    drRow[sName + sFlightNumber] = 0;
            }
            return true;
        }

        private static void CheckLastColumns(DataTable dtAllocationTable, Boolean bShowFlightNumber)
        {
            if (dtAllocationTable == null)
                return;
            int iLastIndex = dtAllocationTable.Columns.Count - 1;

            if (bShowFlightNumber)
                iLastIndex--;
            if ((dtAllocationTable.Columns[iLastIndex].DataType == typeof(Int32)) &&
               (dtAllocationTable.Columns[iLastIndex].ColumnName.Contains(sOccupied)) &&
               (dtAllocationTable.Columns[iLastIndex - 1].DataType == typeof(Int32)) &&
               (dtAllocationTable.Columns[iLastIndex - 1].ColumnName.Contains(sNeed)) &&
               ((!bShowFlightNumber) || ((dtAllocationTable.Columns[iLastIndex + 1].DataType == typeof(Int32)) &&
               (dtAllocationTable.Columns[iLastIndex + 1].ColumnName.Contains(sFlightNumber)))))
            {
                if (bShowFlightNumber)
                    dtAllocationTable.Columns.RemoveAt(iLastIndex + 1);
                dtAllocationTable.Columns.RemoveAt(iLastIndex);
                dtAllocationTable.Columns.RemoveAt(iLastIndex - 1);
            }
        }
        #endregion

        #region Fonction to sort the flight plan
        /// <summary>
        /// Fonction qui permet de trier et d'épurer le plan de vol suivant le paramétrage voulu. \ref alFc
        /// </summary>
        /// <param name="htFlightCategorie"></param>
        /// <param name="htLooseTable"></param>
        /// <param name="alFc"></param>
        /// <param name="sGroundHandler"></param>
        /// <param name="sLooseULD"></param>
        /// <param name="bAllocatedFlight"></param>
        /// <returns></returns>
        private DataTable SortFlightPlan(Dictionary<String, Dictionary<String, OCTInformation>> htFlightCategorie,
                                          Dictionary<String, Dictionary<String, Dictionary<String, String>>> htLooseTable,
                                         ArrayList alFc,
                                         String sGroundHandler,
                                         String sLooseULD,
                                         bool bAllocatedFlight,
                                         bool isDeparture)  // << Bug #9454 Pax2Sim - Simcore version bug - Allocation - flight exception doesn't work
        {
            DataTable dtFPWithOpening = dtFPTable_.Copy();
            dtFPWithOpening.Columns.Add(sOpeningDeskColumn, typeof(DateTime));
            dtFPWithOpening.Columns.Add(sClosingDeskColumn, typeof(DateTime));
            dtFPWithOpening.Columns.Add(sClosingPartialDeskColumn, typeof(DateTime));
            OCTInformation oiInformations = null;
            DateTime dateDepart;
            for (int i = 0; i < dtFPWithOpening.Rows.Count; i++)
            {
                dateDepart = OverallTools.DataFunctions.toDateTime(dtFPWithOpening.Rows[i][iIndexDate], dtFPWithOpening.Rows[i][iIndexTime]);
                if ((dateDepart < dtBegin_) || (dateDepart > dtEnd_))
                {
                    dtFPWithOpening.Rows.RemoveAt(i);
                    i--;
                    continue;
                }
                Int32 iTmp;
                if ((Int32.TryParse(dtFPWithOpening.Rows[i][iIndexTerminal].ToString(), out iTmp)) && (iTmp != iTerminal_))
                {
                    dtFPWithOpening.Rows.RemoveAt(i);
                    i--;
                    continue;
                }
                String sFlight = dtFPWithOpening.Rows[i][iIndexID].ToString();
                String sAirline = dtFPWithOpening.Rows[i][iIndexAirline].ToString();
                String sFlightCategory = dtFPWithOpening.Rows[i][iIndexFlightCategory].ToString();

                // << Bug #9454 Pax2Sim - Simcore version bug - Allocation - flight exception doesn't work                
                if (isDeparture)
                    sFlight = "D_" + sFlight;
                else
                    sFlight = "A_" + sFlight;
                // >> Bug #9454 Pax2Sim - Simcore version bug - Allocation - flight exception doesn't work

                oiInformations = getInformation(htFlightCategorie, 0, sFlight, sAirline, sFlightCategory);
                if (((alFc.Count != 0) && (!alFc.Contains(sFlightCategory))) ||
                    (oiInformations == null))
                {
                    dtFPWithOpening.Rows.RemoveAt(i);
                    i--;
                    continue;
                }
                if ((bSegregateLooseULD_) && (sLooseULD != "") && (htLooseTable != null))
                {
                    String sAircraft = dtFPWithOpening.Rows[i][iIndexAircraft].ToString();
                    String sLooseULDType = GlobalNames.sTableContent_Loose;
                    String sTmp = getInformation(htLooseTable, 0, sFlight, sAirline, sFlightCategory, sAircraft);
                    if (sTmp != null)
                        sLooseULDType = sTmp;

                    if (sLooseULDType != sLooseULD)
                    {
                        dtFPWithOpening.Rows.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                if (sGroundHandler != "")
                {
                    string sHandlingAgent = OverallTools.DataFunctions.getValue(dtAirlineTable_, dtFPWithOpening.Rows[i][iIndexAirline].ToString(), iIndexAirlineAirline, iIndexAirlineGroundHandlers);
                    if (sHandlingAgent != sGroundHandler)
                    {
                        dtFPWithOpening.Rows.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                if (bUseFPContent_)
                {
                    int iTerminalTmp;
                    Int32.TryParse(dtFPWithOpening.Rows[i][iIndexTerminalResult].ToString(), out iTerminalTmp);
                    int iFirstDeskTmp;// = (Int32)dtFPWithOpening.Rows[i][iIndexFirstColumnResult];
                    Int32.TryParse(dtFPWithOpening.Rows[i][iIndexFirstColumnResult].ToString(), out iFirstDeskTmp);
                    int iLastDeskTmp;// = (Int32)dtFPWithOpening.Rows[i][iIndexLastColumnResult];
                    Int32.TryParse(dtFPWithOpening.Rows[i][iIndexLastColumnResult].ToString(), out iLastDeskTmp);
                    bool bAllocated = (iTerminalTmp == iTerminal_) && (iFirstDeskTmp != 0) && (iLastDeskTmp != 0) && (iFirstDeskTmp <= iLastDeskTmp);
                    if (bAllocated != bAllocatedFlight)
                    {
                        dtFPWithOpening.Rows.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                //oiInformations = (OCTInformation)htFlightCategorie[dtFPWithOpening.Rows[i][iIndexFlightCategory].ToString()];


                dtFPWithOpening.Rows[i][sOpeningDeskColumn] = dateDepart.AddMinutes(oiInformations.Opening);
                dtFPWithOpening.Rows[i][sClosingDeskColumn] = dateDepart.AddMinutes(oiInformations.Closing);
                dtFPWithOpening.Rows[i][sClosingPartialDeskColumn] = dateDepart.AddMinutes(oiInformations.PartialClosing);
            }
            dtFPWithOpening.AcceptChanges();
            DataView dgSort = new DataView(dtFPWithOpening, "", sOpeningDeskColumn, DataViewRowState.CurrentRows);

            return dgSort.ToTable();
        }
        #endregion

        #region Functions to find an area for the given flight
        private static Point[] CheckEmptyArea(DataTable dtAllocationTable,
                                            int iOffsetColumns,
                                            int iIndexLigneStart,
                                            int iIndexPartialClosing,
                                            int iIndexLigneEnd,
                                            int iAllocatedColumns,
                                            int iPartialAllocatedColumns,
                                            int iMinimumRows,
                                            int iMinimumColumns,
                                            List<Dictionary<int,SegregationForOverlap>> alCategoriesAllocated,
                                            /*List<Hashtable> alCategoriesAllocated,*/
                                            String sCurrentCategory,
                                            int iToleranceCategory,
                                            int iLastColumnDoubleInfeed,
                                            int iIndexLigneEndDoubleInfeed)
        {
            Point[] pAreas = null;
            if (iPartialAllocatedColumns == iAllocatedColumns)
            {
                //Il n'y a pas d'allocation partielle, on doit donc simplement trouvé un emplacement
                // assez grand pour l'objet passé en paramètre.
                Point pArea = CheckEmptyArea(dtAllocationTable, iOffsetColumns, iIndexLigneStart,
                    iIndexLigneEnd, iAllocatedColumns, false, iMinimumRows, iMinimumColumns,
                    alCategoriesAllocated, sCurrentCategory, iToleranceCategory,
                    iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);

                pAreas = new Point[] { pArea };
            }
            else
            {
                //Il y a une allocation partielle, il faut donc que l'on trouve 2 emplacements suffisament grands
                //qui puisse contenir le vol avec son allocation partielle.
                Point pArea = new Point(-1, -1);
                /*Modification SGE - 04/02/2011 => 
                if (iIndexPartialClosing > dtAllocationTable.Rows.Count)
                    iIndexPartialClosing = dtAllocationTable.Rows.Count;
                 */
                if (iIndexPartialClosing >= dtAllocationTable.Rows.Count)
                    iIndexPartialClosing = dtAllocationTable.Rows.Count - 1;
                if (iIndexPartialClosing > 1)
                    /*Modification SGE - 04/02/2011 => iIndexPartialClosing - 1 replaced by iIndexPartialClosing*/
                    pArea = CheckEmptyArea(dtAllocationTable, iOffsetColumns, iIndexLigneStart,
                        iIndexPartialClosing, iAllocatedColumns, false, iMinimumRows,
                        iMinimumColumns, alCategoriesAllocated, sCurrentCategory, iToleranceCategory,
                        iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);
                if (iIndexPartialClosing >= dtAllocationTable.Rows.Count)
                    pAreas = new Point[] { pArea };
                else
                {
                    Point pArea2 = new Point(-1, -1);
                    if (pArea.X == -1)
                    {
                        pArea2 = CheckEmptyArea(dtAllocationTable, iOffsetColumns, iIndexPartialClosing,
                            iIndexLigneEnd, iPartialAllocatedColumns, false, iMinimumRows, iMinimumColumns,
                            alCategoriesAllocated, sCurrentCategory, iToleranceCategory,
                            iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);
                        pAreas = new Point[] { pArea2 };
                    }
                    else
                    {
                        pArea2 = CheckEmptyArea(dtAllocationTable, pArea.X, iIndexPartialClosing,
                            iIndexLigneEnd, iPartialAllocatedColumns, false, iMinimumRows, iMinimumColumns,
                            alCategoriesAllocated, sCurrentCategory, iToleranceCategory,
                            iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);
                        if (pArea.X != pArea2.X)
                        {
                            pAreas = CheckEmptyArea(dtAllocationTable, pArea2.X,
                               iIndexLigneStart, iIndexPartialClosing, iIndexLigneEnd, iAllocatedColumns,
                               iPartialAllocatedColumns, iMinimumRows, iMinimumColumns,
                               alCategoriesAllocated, sCurrentCategory, iToleranceCategory,
                               iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);
                        }
                        else
                        {
                            pAreas = new Point[] { pArea, pArea2 };
                        }
                    }

                    /*if (pArea.X == -1)
                    {
                        pAreas = new Point[] { pArea2 };
                    }
                    else if (pArea.X != pArea2.X)
                    {
                        pAreas = CheckEmptyArea(dtAllocationTable, Math.Max(pArea.X, pArea2.X),
                            iIndexLigneStart, iIndexPartialClosing, iIndexLigneEnd, iAllocatedColumns,
                            iPartialAllocatedColumns, iMinimumRows, iMinimumColumns,
                            alCategoriesAllocated, sCurrentCategory, iToleranceCategory,
                            iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);
                    }
                    else
                    {
                        pAreas = new Point[] { pArea, pArea2 };
                    }*/
                }
            }
            return pAreas;
        }

        private static Point CheckEmptyArea(DataTable dtAllocationTable,
                                           int iOffsetColumns,
                                           int iIndexLigneStart,
                                           int iIndexLigneEnd,
                                           int iAllocatedColumns,
                                           bool bCheckAvailable,
                                           int iMinimumRows,
                                           int iMinimumColumns,
                                           List<Dictionary<int,SegregationForOverlap>> alCategoriesAllocated,
                                           /*List<Hashtable> alCategoriesAllocated,*/
                                           String sCurrentCategory,
                                           int iToleranceCatrgory,
                                           int iLastColumnDoubleInfeed,
                                           int iIndexLigneEndDoubleInfeed)
        {
            int i;
            //L'index de recherche se trouve en dehors de la table. Ce n'est pas alloué, on renvoie donc les coordonnées fictives
            //dans la table.
            if (iOffsetColumns >= dtAllocationTable.Columns.Count)
                return new Point(iOffsetColumns, iOffsetColumns + iAllocatedColumns - 1);

            int iIndexLigneEndTmp = iIndexLigneEnd;
            if (iOffsetColumns <= iLastColumnDoubleInfeed)
                iIndexLigneEndTmp = iIndexLigneEndDoubleInfeed;
            //On parcours la colonne courante de iMinimumRows en iMinimumRows.
            for (i = iIndexLigneStart; i <= (iIndexLigneEndTmp + iMinimumRows - 1); i += iMinimumRows)
            {
                if (i > iIndexLigneEndTmp)
                    i = iIndexLigneEndTmp;
                //L'une des cases de cette colonne n'est pas vide, on avance donc l'index de la colonne.
                if (dtAllocationTable.Rows[i][iOffsetColumns].ToString().Length > 0)
                {
                    int iNumber = dtAllocationTable.Rows[i][iOffsetColumns].ToString().Split(',').Length;
                    if (iNumber < iToleranceCatrgory)
                    {
                        if (alCategoriesAllocated[i].ContainsKey(iOffsetColumns) && alCategoriesAllocated[i][iOffsetColumns].Contains(sCurrentCategory))
                            continue;
                        /*Hashtable htTmp = (Hashtable)alCategoriesAllocated[i];
                        if ((htTmp.Contains(iOffsetColumns)) && (htTmp[iOffsetColumns].ToString() == sCurrentCategory))
                        {
                            continue;
                        }*/
                    }
                    if (bCheckAvailable)
                        return new Point(-1, iOffsetColumns);
                    return CheckEmptyArea(dtAllocationTable, iOffsetColumns + 1, iIndexLigneStart, iIndexLigneEnd,
                        iAllocatedColumns, false, iMinimumRows, iMinimumColumns, alCategoriesAllocated,
                        sCurrentCategory, iToleranceCatrgory,
                        iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);
                }
            }
            if (iAllocatedColumns == 1)
                return new Point(iOffsetColumns, iOffsetColumns);


            int iLeftToBeAllocated;
            int iNextColumn;
            if (iAllocatedColumns > iMinimumColumns)
            {
                iLeftToBeAllocated = iAllocatedColumns - iMinimumColumns;
                iNextColumn = iOffsetColumns + iMinimumColumns;

            }
            else
            {
                if (iAllocatedColumns == iMinimumColumns)
                {
                    iLeftToBeAllocated = 1;
                    iNextColumn = iOffsetColumns + iMinimumColumns - 1;
                }
                else
                {
                    iLeftToBeAllocated = 1;
                    iNextColumn = iOffsetColumns + iAllocatedColumns - 1;
                }
            }
            Point pResult = CheckEmptyArea(dtAllocationTable, iNextColumn, iIndexLigneStart, iIndexLigneEnd,
                iLeftToBeAllocated, true, iMinimumRows, iMinimumColumns, alCategoriesAllocated,
                sCurrentCategory, iToleranceCatrgory,
                iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);
            //Si le composant X du point est négatif, cela veut dire que la zone était invalide. On reprend donc depuis 
            //la colonne suivant l'index qui avait posé problème.
            if (pResult.X == -1)
            {
                if (bCheckAvailable)
                    return pResult;
                return CheckEmptyArea(dtAllocationTable,
                    iOffsetColumns + iMinimumColumns + 1,
                    iIndexLigneStart,
                    iIndexLigneEnd,
                    iAllocatedColumns,
                    false,
                    iMinimumRows, iMinimumColumns, alCategoriesAllocated, sCurrentCategory, iToleranceCatrgory,
                    iLastColumnDoubleInfeed, iIndexLigneEndDoubleInfeed);
            }
            return new Point(iOffsetColumns, pResult.Y);
        }

        #endregion

        #region The getReview function
        internal List<String> getReview(bool bPrintPDF)
        {
            List<string> lsResult = new List<string>();
            /*Le nom et les statistics communes à tous les scénarios.*/
            lsResult.Add("Name" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) +sNomTable_);
            lsResult.Add(" ");
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Type" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + AllocationType.ToString());

            lsResult.Add(" ");
            lsResult.Add("Tables");

            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Flight Plan" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dtFPTable.TableName);
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "OCT Table" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dtOCTTable.Name);
            if (UseOCTTableExceptions)
                lsResult[lsResult.Count - 1] += ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + "[X] Use Exception";

            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Airline table" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dtAirlineTable.TableName);

            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Aircraft table" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dtAircraftTable.Name);
            if (UseAircraftTableExceptions)
                lsResult[lsResult.Count - 1] += ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + "[X] Use Exception";
            if (dtLoadFactors != null)
            {
                lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Infeed limitation");
                lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 3) + "Number of reclaim with double infeed" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1)+iNumberOfDoubleInfeed.ToString());
                lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 3) + "Speed of infeed" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dInfeedSpeed.ToString() + " sec / Bag");
                lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 3) + "Load factor table" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dtLoadFactors.Name);
                if (UseLoadFactorExceptions)
                    lsResult[lsResult.Count - 1] += ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + "[X] Use Exception";
                lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 3) + "Number of baggage table" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dtNbBagage.Name);
                if (UseNbBaggageExceptions)
                    lsResult[lsResult.Count - 1] += ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + "[X] Use Exception";
            }
            if (flightPlanParametersTable != null)
            {
                lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 3) + FPParametersTableConstants.TABLE_DISPLAY_NAME + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + flightPlanParametersTable.TableName);
            }
            lsResult.Add(" ");
            lsResult.Add("Times specifications");
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "From" + " : " + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dtBegin.ToString() + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) +" To "+ ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dtEnd.ToString());
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Step" + " : " + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dStep.ToString());
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Time between two flights" + " : " + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + dTimeBetweenFlights.ToString());

            lsResult.Add(" ");
            lsResult.Add("Terminal informations");
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "In column \""+ObservedTerminal + "\" : " + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + Terminal.ToString());


            lsResult.Add(" ");
            lsResult.Add("Global parameters");
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(UseFPContent, "Use the flight plan information as basis"));
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(AllocateFlightPlan, "Allocate the flight plan"));
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(FCAllocation, "Allocate biggest flights first"));
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(SegregateLooseULD, "Segregate flight following information Loose / ULD in aircraft"));
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(SegregateGroundHandlers, "Segregate by ground handlers"));
            // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) 
                + ParamScenario.PrintBoolean(ContinousSegregationNumerotation, "Reset Segregation Numerotation"));
            // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2)
                + "First Index for Allocation : " + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1)
                + FirstIndexForAllocation.ToString());
            // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource

            lsResult.Add(" ");
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(ColorByFC, "Colors by flight category"));
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(ColorByHandler, "Colors by Ground handler"));
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(ColorByAirline, "Colors by Airline"));
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + ParamScenario.PrintBoolean(OverlapParameters != null, "Use overlap informations"));

            return lsResult;
        }
        #endregion
    }
}
