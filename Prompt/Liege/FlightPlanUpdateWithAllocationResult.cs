using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class FlightPlanUpdateWithAllocationResult
    {
        internal const int BIG_FLIGHTS_DUMMY_FP_ID_START = 10000;
        internal const int BIG_FLIGHTS_DUMMY_FP_DEFAULT_DESK_NB = 1;
        internal const string BIG_FLIGHTS_DUMMY_FP_DEFAULT_FLIGHT_CATEGORY = "Empty";
        internal const string BIG_FLIGHTS_DUMMY_FP_DEFAULT_FLIGHT_CATEGORY_SUFFIX = "_Dupl";

        private string allocationType;
        private List<Flight> departureFlightsFromAllocationResult = new List<Flight>();        
        private GestionDonneesHUB2SIM donnees;

        internal List<string> defaultFlightCategoriesAdded = new List<string>();

        //internal bool hasBigFlights = false;

        public FlightPlanUpdateWithAllocationResult(string _allocationType,
            List<Flight> _departureFlightsFromAllocationResult,            
            GestionDonneesHUB2SIM _donnees)
        {
            allocationType = _allocationType;
            departureFlightsFromAllocationResult = _departureFlightsFromAllocationResult;            
            donnees = _donnees;
        }

        public void updateFlightPlansWihtAllocationResults()
        {
            DataTable baseInputDataFPD = getBaseInputDataDepartureFlightPlan();
            DataTable baseInputDataFPA = getBaseInputDataArrivalFlightPlan();

            if (baseInputDataFPA != null 
                && baseInputDataFPD != null)
            {
                updateFPWithAllocation(baseInputDataFPA, baseInputDataFPD);
            }
        }
                
        private DataTable getBaseInputDataDepartureFlightPlan()
        {
            DataTable inputDataFPD = null;
            if (donnees != null)
            {
                inputDataFPD = donnees.getTable("Input", GlobalNames.FPDTableName);
            }
            return inputDataFPD;
        }

        private DataTable getBaseInputDataArrivalFlightPlan()
        {
            DataTable inputDataFPA = null;
            if (donnees != null)
            {
                inputDataFPA = donnees.getTable("Input", GlobalNames.FPATableName);
            }
            return inputDataFPA;
        }

        private void updateFPWithAllocation(DataTable arrivalFlightPlan,
            DataTable departureFlightPlan)
        {
            if (arrivalFlightPlan != null && departureFlightPlan != null)
            {                
                departureFlightPlan.Rows.Clear();
                if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE))
                    arrivalFlightPlan.Rows.Clear();

                foreach (Flight flight in departureFlightsFromAllocationResult)
                {
                    addFlightToFP(flight.linkedFlight, flight, departureFlightPlan, false);
                    if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE)
                        && flight.linkedFlight != null)
                    {
                        addFlightToFP(flight.linkedFlight, flight, arrivalFlightPlan, true);
                    }
                }

                if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE))
                {
                    arrivalFlightPlan.AcceptChanges();
                }
                departureFlightPlan.AcceptChanges();
            }
        }

        private void addFlightToFP(Flight arrivalFlight, Flight departureFlight, DataTable flightPlan, bool isArrival)
        {
            if (isArrival && arrivalFlight == null)
                return;

            if (departureFlight != null
                && flightPlan != null)
            {
                Flight flight = null;
                if (isArrival)
                    flight = arrivalFlight;
                else
                    flight = departureFlight;

                #region column indexes
                List<int> indexes = new List<int>();
                #region flight information columns
                int idColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
                indexes.Add(idColumnIndex);
                int flightDateColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                indexes.Add(flightDateColumnIndex);
                int flightTimeColumnIndex = -1;
                if (isArrival)
                    flightTimeColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
                else
                    flightTimeColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
                indexes.Add(flightTimeColumnIndex);

                int airlineCodeColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                indexes.Add(airlineCodeColumnIndex);
                int flightNumberColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                indexes.Add(flightNumberColumnIndex);
                int airportCodeColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                indexes.Add(airportCodeColumnIndex);
                int flightCategoryColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                indexes.Add(flightCategoryColumnIndex);
                int aircraftTypeColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
                indexes.Add(aircraftTypeColumnIndex);
                int nbSeatsColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats);
                indexes.Add(nbSeatsColumnIndex);

                int noBSMColumnIndex = -1;
                int cbpColumnIndex = -1;
                int tsaColumnIndex = -1;

                if (isArrival)
                {
                    noBSMColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
                    indexes.Add(noBSMColumnIndex);
                    cbpColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
                    indexes.Add(cbpColumnIndex);
                }
                else
                {
                    tsaColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
                    indexes.Add(tsaColumnIndex);
                }
                #endregion

                #region desk allocation columns
                int gateTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
                indexes.Add(gateTerminalColumnIndex);
                int parkingTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
                indexes.Add(parkingTerminalColumnIndex);
                int parkingStandColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);
                indexes.Add(parkingStandColumnIndex);
                int runwayColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay);
                indexes.Add(runwayColumnIndex);

                #region initialization
                int arrivalGateColumnIndex = -1;
                int reclaimTerminalColumnIndex = -1;
                int reclaimDeskColumnIndex = -1;
                int infeedTerminalColumnIndex = -1;
                int arrivalInfeedStartColumnIndex = -1;
                int arrivalInfeedEndColumnIndex = -1;
                int transferInfeedColumnIndex = -1;

                int checkInTerminalColumnIndex = -1;
                int ecoCIStartColumnIndex = -1;
                int ecoCIEndColumnIndex = -1;
                int fbCIStartColumnIndex = -1;
                int fbCIEndColumnIndex = -1;
                int ecoBagDropStartColumnIndex = -1;
                int ecoBagDropEndColumnIndex = -1;
                int fbBagDropStartColumnIndex = -1;
                int fbBagDropEndColumnIndex = -1;

                int boardingGateColumnIndex = -1;

                int mupTerminalColumnIndex = -1;
                int ecoMupStartColumnIndex = -1;
                int ecoMupEndColumnIndex = -1;
                int fbMupStartColumnIndex = -1;
                int fbMupEndColumnIndex = -1;
                #endregion

                if (isArrival)
                {
                    #region arrival specific resources
                    arrivalGateColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate);
                    indexes.Add(arrivalGateColumnIndex);
                    reclaimTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim);
                    indexes.Add(reclaimTerminalColumnIndex);
                    reclaimDeskColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
                    indexes.Add(reclaimDeskColumnIndex);
                    infeedTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
                    indexes.Add(infeedTerminalColumnIndex);
                    arrivalInfeedStartColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject);
                    indexes.Add(arrivalInfeedStartColumnIndex);
                    arrivalInfeedEndColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject);
                    indexes.Add(arrivalInfeedEndColumnIndex);
                    transferInfeedColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);
                    indexes.Add(transferInfeedColumnIndex);
                    #endregion
                }
                else
                {
                    #region departure specific resources
                    checkInTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
                    indexes.Add(checkInTerminalColumnIndex);
                    ecoCIStartColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
                    indexes.Add(ecoCIStartColumnIndex);
                    ecoCIEndColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
                    indexes.Add(ecoCIEndColumnIndex);
                    fbCIStartColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
                    indexes.Add(fbCIStartColumnIndex);
                    fbCIEndColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);
                    indexes.Add(fbCIEndColumnIndex);
                    ecoBagDropStartColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
                    indexes.Add(ecoBagDropStartColumnIndex);
                    ecoBagDropEndColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
                    indexes.Add(ecoBagDropEndColumnIndex);
                    fbBagDropStartColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
                    indexes.Add(fbBagDropStartColumnIndex);
                    fbBagDropEndColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);
                    indexes.Add(fbBagDropEndColumnIndex);

                    boardingGateColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);
                    indexes.Add(boardingGateColumnIndex);

                    mupTerminalColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup);
                    indexes.Add(mupTerminalColumnIndex);
                    ecoMupStartColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
                    indexes.Add(ecoMupStartColumnIndex);
                    ecoMupEndColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
                    indexes.Add(ecoMupEndColumnIndex);
                    fbMupStartColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
                    indexes.Add(fbMupStartColumnIndex);
                    fbMupEndColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);
                    indexes.Add(fbMupEndColumnIndex);
                    #endregion
                }
                #endregion

                #region user_x columns
                int user1ColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                indexes.Add(user1ColumnIndex);
                int user2ColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                indexes.Add(user2ColumnIndex);
                int user3ColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                indexes.Add(user3ColumnIndex);
                int user4ColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                indexes.Add(user4ColumnIndex);
                int user5ColumnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
                indexes.Add(user5ColumnIndex);
                #endregion

                #endregion

                if (areIndexesValid(indexes))
                {
                    DataRow newRow = flightPlan.NewRow();
                    DataRow dupplicatedRow = null;

                    int firstResourceId = -1;
                    int lastResourceId = -1;
                    getAllocatedResourcesCodeId(departureFlight, 
                        out firstResourceId, out lastResourceId);

                    //if ((allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE) || allocationType.Equals(Allocation.BOARDING_GATE_ALLOCATION_TYPE))
                    //    && firstResourceId != lastResourceId)
                    //{
                    //    dupplicatedRow = flightPlan.NewRow();
                    //}                    

                    if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
                    {
                        if (firstResourceId != lastResourceId
                            || flight.dummyFlightBoardingGateNb != 0)
                        {
                            dupplicatedRow = flightPlan.NewRow();
                        }
                    }
                    else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
                    {
                        if (firstResourceId != lastResourceId
                            || flight.dummyFlightParkingStandNb != 0)
                        {
                            dupplicatedRow = flightPlan.NewRow();
                        }
                    }
                    else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
                    {
                        if (flight.dummyFlightParkingStandNb != 0 
                            ||flight.dummyFlightBoardingGateNb != 0)
                        {
                            dupplicatedRow = flightPlan.NewRow();
                        }
                    }

                    #region flight information
                    newRow[idColumnIndex] = flight.id;
                    newRow[flightDateColumnIndex] = flight.date;
                    newRow[flightTimeColumnIndex] = flight.time;
                    newRow[airlineCodeColumnIndex] = flight.airlineCode;
                    newRow[flightNumberColumnIndex] = flight.flightNumber;
                    newRow[airportCodeColumnIndex] = flight.airportCode;
                    newRow[flightCategoryColumnIndex] = flight.flightCategory;
                    newRow[aircraftTypeColumnIndex] = flight.aircraftType;
                    newRow[nbSeatsColumnIndex] = flight.nbSeats;
                    if (isArrival)
                    {
                        newRow[noBSMColumnIndex] = flight.noBSM;
                        newRow[cbpColumnIndex] = flight.cbp;
                    }
                    else
                    {
                        newRow[tsaColumnIndex] = flight.tsa;
                    }
                    #endregion

                    #region user_x
                    newRow[user1ColumnIndex] = flight.user1;
                    newRow[user2ColumnIndex] = flight.user2;
                    newRow[user3ColumnIndex] = flight.user3;
                    newRow[user4ColumnIndex] = flight.user4;
                    newRow[user5ColumnIndex] = flight.user5;
                    #endregion

                    if (dupplicatedRow != null)
                    {
                        dupplicatedRow.ItemArray = newRow.ItemArray;
                        dupplicatedRow[idColumnIndex] = BIG_FLIGHTS_DUMMY_FP_ID_START + flight.id;
                        
                        string dupplFlightCategory = flight.flightCategory + BIG_FLIGHTS_DUMMY_FP_DEFAULT_FLIGHT_CATEGORY_SUFFIX;;
                        dupplicatedRow[flightCategoryColumnIndex] = dupplFlightCategory;
                        if (!defaultFlightCategoriesAdded.Contains(dupplFlightCategory))
                            defaultFlightCategoriesAdded.Add(dupplFlightCategory);

                        dupplicatedRow[gateTerminalColumnIndex] = flight.flightPlanGateTerminalNb;
                        dupplicatedRow[parkingTerminalColumnIndex] = flight.flightPlanParkingTerminalNb;
                        dupplicatedRow[runwayColumnIndex] = flight.flightPlanRunwayNb;
                                               
                        if (isArrival)
                        {
                            dupplicatedRow[arrivalGateColumnIndex] = flight.flightPlanArrivalGateNb;
                            dupplicatedRow[reclaimTerminalColumnIndex] = flight.flightPlanReclaimTerminalNb;
                            dupplicatedRow[reclaimDeskColumnIndex] = flight.flightPlanReclaimDeskNb;
                            dupplicatedRow[infeedTerminalColumnIndex] = flight.flightPlanInfeedTerminalNb;
                            dupplicatedRow[arrivalInfeedStartColumnIndex] = flight.flightPlanArrivalInfeedStartDeskNb;
                            dupplicatedRow[arrivalInfeedEndColumnIndex] = flight.flightPlanArrivalInfeedEndDeskNb;
                            dupplicatedRow[transferInfeedColumnIndex] = flight.flightPlanTransferInfeedNb;
                            
                            dupplicatedRow[parkingStandColumnIndex] = lastResourceId;
                            
                        }
                        else
                        {
                            dupplicatedRow[checkInTerminalColumnIndex] = flight.flightPlanCheckInTerminalNb;

                            dupplicatedRow[ecoBagDropStartColumnIndex] = flight.flightPlanEcoBagDropStartDeskNb;
                            dupplicatedRow[ecoBagDropEndColumnIndex] = flight.flightPlanEcoBagDropEndDeskNb;
                            dupplicatedRow[fbBagDropStartColumnIndex] = flight.flightPlanFBBagDropStartDeskNb;
                            dupplicatedRow[fbBagDropEndColumnIndex] = flight.flightPlanFBBagDropEndDeskNb;

                            dupplicatedRow[mupTerminalColumnIndex] = flight.flightPlanMupTerminalNb;
                            dupplicatedRow[ecoMupStartColumnIndex] = flight.flightPlanEcoMupStartDeskNb;
                            dupplicatedRow[ecoMupEndColumnIndex] = flight.flightPlanEcoMupEndDeskNb;
                            dupplicatedRow[fbMupStartColumnIndex] = flight.flightPlanFBMupStartDeskNb;
                            dupplicatedRow[fbMupEndColumnIndex] = flight.flightPlanFBMupEndDeskNb;

                            if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE))
                            {
                                dupplicatedRow[parkingStandColumnIndex] = lastResourceId;

                                if (flight.dummyFlightBoardingGateNb != 0)
                                    dupplicatedRow[boardingGateColumnIndex] = flight.dummyFlightBoardingGateNb;//dummy flight saved boarding gate
                                else
                                    dupplicatedRow[boardingGateColumnIndex] = flight.flightPlanBoardingGateNb;//dummy flight saved boarding gate
                                dupplicatedRow[ecoCIStartColumnIndex] = flight.flightPlanEcoCheckInStartDeskNb;
                                dupplicatedRow[ecoCIEndColumnIndex] = flight.flightPlanEcoCheckInEndDeskNb;
                                dupplicatedRow[fbCIStartColumnIndex] = flight.flightPlanFBCheckInStartDeskNb;
                                dupplicatedRow[fbCIEndColumnIndex] = flight.flightPlanFBCheckInEndDeskNb;
                            }                            
                            else if (allocationType.Equals(Allocation.BOARDING_GATE_ALLOCATION_TYPE))
                            {
                                dupplicatedRow[boardingGateColumnIndex] = lastResourceId;

                                if (flight.dummyFlightParkingStandNb != 0)
                                    dupplicatedRow[parkingStandColumnIndex] = flight.dummyFlightParkingStandNb;//dummy flight saved parking stand
                                else
                                    dupplicatedRow[parkingStandColumnIndex] = flight.flightPlanParkingStandNb;//dummy flight saved parking stand
                                dupplicatedRow[ecoCIStartColumnIndex] = flight.flightPlanEcoCheckInStartDeskNb;
                                dupplicatedRow[ecoCIEndColumnIndex] = flight.flightPlanEcoCheckInEndDeskNb;
                                dupplicatedRow[fbCIStartColumnIndex] = flight.flightPlanFBCheckInStartDeskNb;
                                dupplicatedRow[fbCIEndColumnIndex] = flight.flightPlanFBCheckInEndDeskNb;
                            }
                            else if (allocationType.Equals(Allocation.CHECK_IN_ALLOCATION_TYPE))
                            {                                
                                if (flight.dummyFlightParkingStandNb != 0)
                                    dupplicatedRow[parkingStandColumnIndex] = flight.dummyFlightParkingStandNb;//dummy flight saved parking stand
                                else
                                    dupplicatedRow[parkingStandColumnIndex] = flight.flightPlanParkingStandNb;//dummy flight saved parking stand
                                
                                if (flight.dummyFlightBoardingGateNb != 0)
                                    dupplicatedRow[boardingGateColumnIndex] = flight.dummyFlightBoardingGateNb;//dummy flight saved boarding gate
                                else
                                    dupplicatedRow[boardingGateColumnIndex] = flight.flightPlanBoardingGateNb;//dummy flight saved boarding gate

                                dupplicatedRow[ecoCIStartColumnIndex] = firstResourceId;
                                dupplicatedRow[ecoCIEndColumnIndex] = lastResourceId;
                                dupplicatedRow[fbCIStartColumnIndex] = firstResourceId;
                                dupplicatedRow[fbCIEndColumnIndex] = lastResourceId;
                            }                         
                        }
                    }

                    #region resource allocation (other than Parking, BoardingGate or CheckIn)
                    newRow[runwayColumnIndex] = flight.flightPlanRunwayNb;

                    if (isArrival)
                    {
                        newRow[arrivalGateColumnIndex] = flight.flightPlanArrivalGateNb;
                        newRow[reclaimTerminalColumnIndex] = flight.flightPlanReclaimTerminalNb;
                        newRow[reclaimDeskColumnIndex] = flight.flightPlanReclaimDeskNb;
                        newRow[infeedTerminalColumnIndex] = flight.flightPlanInfeedTerminalNb;
                        newRow[arrivalInfeedStartColumnIndex] = flight.flightPlanArrivalInfeedStartDeskNb;
                        newRow[arrivalInfeedEndColumnIndex] = flight.flightPlanArrivalInfeedEndDeskNb;
                        newRow[transferInfeedColumnIndex] = flight.flightPlanTransferInfeedNb;
                    }
                    else
                    {
                        newRow[ecoBagDropStartColumnIndex] = flight.flightPlanEcoBagDropStartDeskNb;
                        newRow[ecoBagDropEndColumnIndex] = flight.flightPlanEcoBagDropEndDeskNb;
                        newRow[fbBagDropStartColumnIndex] = flight.flightPlanFBBagDropStartDeskNb;
                        newRow[fbBagDropEndColumnIndex] = flight.flightPlanFBBagDropEndDeskNb;

                        newRow[mupTerminalColumnIndex] = flight.flightPlanMupTerminalNb;
                        newRow[ecoMupStartColumnIndex] = flight.flightPlanEcoMupStartDeskNb;
                        newRow[ecoMupEndColumnIndex] = flight.flightPlanEcoMupEndDeskNb;
                        newRow[fbMupStartColumnIndex] = flight.flightPlanFBMupStartDeskNb;
                        newRow[fbMupEndColumnIndex] = flight.flightPlanFBMupEndDeskNb;
                    }
                    #endregion
                    
                    #region resource allocation for Parking, BoardingGate and CheckIn
                    newRow[parkingTerminalColumnIndex] = flight.flightPlanParkingTerminalNb;
                    newRow[gateTerminalColumnIndex] = flight.flightPlanGateTerminalNb;
                    if (!isArrival)
                    {
                        newRow[checkInTerminalColumnIndex] = flight.flightPlanCheckInTerminalNb;
                    }

                    if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE))
                    {                        
                        newRow[parkingStandColumnIndex] = firstResourceId;                                                
                        if (!isArrival)
                        {
                            newRow[ecoCIStartColumnIndex] = flight.flightPlanEcoCheckInStartDeskNb;
                            newRow[ecoCIEndColumnIndex] = flight.flightPlanEcoCheckInEndDeskNb;
                            newRow[fbCIStartColumnIndex] = flight.flightPlanFBCheckInStartDeskNb;
                            newRow[fbCIEndColumnIndex] = flight.flightPlanFBCheckInEndDeskNb;

                            newRow[boardingGateColumnIndex] = flight.flightPlanBoardingGateNb;
                        }
                    }
                    else if (allocationType.Equals(Allocation.BOARDING_GATE_ALLOCATION_TYPE))
                    {                        
                        if (!isArrival)
                        {                            
                            newRow[boardingGateColumnIndex] = firstResourceId;
                            newRow[ecoCIStartColumnIndex] = flight.flightPlanEcoCheckInStartDeskNb;
                            newRow[ecoCIEndColumnIndex] = flight.flightPlanEcoCheckInEndDeskNb;
                            newRow[fbCIStartColumnIndex] = flight.flightPlanFBCheckInStartDeskNb;
                            newRow[fbCIEndColumnIndex] = flight.flightPlanFBCheckInEndDeskNb;
                        }
                        newRow[parkingStandColumnIndex] = flight.flightPlanParkingStandNb;
                    }
                    else if (allocationType.Equals(Allocation.CHECK_IN_ALLOCATION_TYPE))
                    {
                        if (!isArrival)
                        {                            
                            newRow[ecoCIStartColumnIndex] = firstResourceId;
                            newRow[ecoCIEndColumnIndex] = lastResourceId;
                            newRow[fbCIStartColumnIndex] = firstResourceId;
                            newRow[fbCIEndColumnIndex] = lastResourceId;
                            newRow[boardingGateColumnIndex] = flight.flightPlanBoardingGateNb;
                        }
                        newRow[parkingStandColumnIndex] = flight.flightPlanParkingStandNb;
                    }
                    #endregion
                    
                    flightPlan.Rows.Add(newRow);
                    if (dupplicatedRow != null)
                        flightPlan.Rows.Add(dupplicatedRow);
                }
            }
        }

        private void getAllocatedResourcesCodeId(Flight allocatedFlight, 
            out int firstResourceId, out int lastResourceId)
        {
            firstResourceId = -1;
            lastResourceId = -1;

            if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                LiegeTools.getFirstAndLastOccupiedStationNb(allocatedFlight.allocatedResources, 
                    out firstResourceId, out lastResourceId);
            }
            else
            {
                if (allocatedFlight.allocatedResources.Count == 1)
                {
                    Resource resource = allocatedFlight.allocatedResources[0];
                    firstResourceId = resource.codeId;
                    lastResourceId = firstResourceId;
                }
                else if (allocatedFlight.allocatedResources.Count == 2)
                {
                    Resource firstResource = allocatedFlight.allocatedResources[0];
                    Resource lastResource = allocatedFlight.allocatedResources[1];

                    firstResourceId = firstResource.codeId;
                    lastResourceId = lastResource.codeId;
                }
            }
        }

        public DataTable updateFlightCategoryTableWithDefaultFlightCategories(DataTable currentFlightCategoryTable,
            List<string> defaultFlightCategoriesAdded)
        {
            if (currentFlightCategoryTable != null
                && currentFlightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC) != -1
                && currentFlightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_Description) != -1)
            {
                int flightCategoryColumnIndex = currentFlightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC);
                int flightCategoryDescriptionColumnIndex = currentFlightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_Description);

                foreach (string defaultFC in defaultFlightCategoriesAdded)
                {
                    if (!flightCategoryTableContainsFlightCategory(currentFlightCategoryTable, defaultFC))
                    {
                        DataRow newRow = currentFlightCategoryTable.NewRow();
                        newRow[flightCategoryColumnIndex] = defaultFC;
                        newRow[flightCategoryDescriptionColumnIndex] = "Dupplicated Flight Default Flight Category";
                        currentFlightCategoryTable.Rows.Add(newRow);
                        currentFlightCategoryTable.AcceptChanges();
                    }
                }
            }
            return currentFlightCategoryTable;
        }

        private bool flightCategoryTableContainsFlightCategory(DataTable flightCategoryTable, string flightCategory)
        {
            if (flightCategoryTable != null 
                && flightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC) != -1)
            {
                int flightCategoryColumnIndex = flightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC);
                foreach (DataRow row in flightCategoryTable.Rows)
                {
                    if (row[flightCategoryColumnIndex] != null)
                    {
                        string currentFlightCategory = row[flightCategoryColumnIndex].ToString();
                        if (currentFlightCategory != null
                            && currentFlightCategory.Equals(flightCategory))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        internal static bool areIndexesValid(List<int> indexes)
        {
            foreach (int index in indexes)
            {
                if (index == -1)
                    return false;
            }
            return true;
        }
    }
}
