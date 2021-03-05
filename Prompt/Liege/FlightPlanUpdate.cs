using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class FlightPlanUpdate
    {
        internal const string IMPORTED_FPA_TABLE_NAME = "Imported Arrival Flight Plan";
        internal const string IMPORTED_FPD_TABLE_NAME = "Imported Departure Flight Plan";

        internal const int DEFAULT_RESOURCE_INDEX = 0;

        private DataTable currentArrivalFlightPlan;
        private DataTable currentDepartureFlightPlan;
        private Dictionary<String, LiegeTools.FlightInfoHolder> arrivalFlightsDictionary = new Dictionary<String, LiegeTools.FlightInfoHolder>();
        private Dictionary<String, LiegeTools.FlightInfoHolder> departureFlightsDictionary = new Dictionary<String, LiegeTools.FlightInfoHolder>();

        public FlightPlanUpdate(Dictionary<String, LiegeTools.FlightInfoHolder> arrivalFlightsDictionary_,
            Dictionary<String, LiegeTools.FlightInfoHolder> departureFlightsDictionary_, DataTable currentArrivalFlightPlan_,
            DataTable currentDepartureFlightPlan_)
        {
            this.arrivalFlightsDictionary = arrivalFlightsDictionary_;
            this.departureFlightsDictionary = departureFlightsDictionary_;
            this.currentArrivalFlightPlan = currentArrivalFlightPlan_;
            this.currentDepartureFlightPlan = currentDepartureFlightPlan_;
        }

        public DataTable generateArrivalFlightPlanWithImportedData()
        {
            DataTable flightPlanWithImportedData = null;
            if (currentArrivalFlightPlan != null && arrivalFlightsDictionary.Count > 0)
            {
                flightPlanWithImportedData = currentArrivalFlightPlan.Clone();
                flightPlanWithImportedData.TableName = IMPORTED_FPA_TABLE_NAME;

                int rowNb = 1;
                foreach (KeyValuePair<String, LiegeTools.FlightInfoHolder> arrivalFlightPair in arrivalFlightsDictionary)
                {
                    DataRow newFPRow = flightPlanWithImportedData.NewRow();
                    LiegeTools.FlightInfoHolder flight = arrivalFlightPair.Value;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID) != -1)
                    {
                        newFPRow[GlobalNames.sFPD_A_Column_ID] = rowNb;
                        flight.id = rowNb;
                    }
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_DATE] = flight.flightDate;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_STA) != -1)
                        newFPRow[GlobalNames.sFPA_Column_STA] = flight.flightTime;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AirlineCode] = flight.airlineCode;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_FlightN] = flight.flightNumber;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AirportCode] = flight.airportCode;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_FlightCategory] = flight.flightCategory;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AircraftType] = flight.aircraftType;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM) != -1)
                        newFPRow[GlobalNames.sFPA_Column_NoBSM] = false;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_CBP) != -1)
                        newFPRow[GlobalNames.sFPA_Column_CBP] = false;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_NbSeats] = 0;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_TerminalGate] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate) != -1)
                        newFPRow[GlobalNames.sFPA_Column_ArrivalGate] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim) != -1)
                        newFPRow[GlobalNames.sFPA_Column_TerminalReclaim] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject) != -1)
                        newFPRow[GlobalNames.sFPA_Column_ReclaimObject] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject) != -1)
                        newFPRow[GlobalNames.sFPA_Column_TerminalInfeedObject] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject) != -1)
                        newFPRow[GlobalNames.sFPA_Column_StartArrivalInfeedObject] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject) != -1)
                        newFPRow[GlobalNames.sFPA_Column_EndArrivalInfeedObject] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject) != -1)
                        newFPRow[GlobalNames.sFPA_Column_TransferInfeedObject] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_TerminalParking] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_Parking] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_RunWay] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_User1] = flight.schengenInfo + ", " + flight.ueeInfo;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2) != -1)
                    {
                        newFPRow[GlobalNames.sFPD_A_Column_User2] = flight.arrivalOrDepartureTag + ", " 
                            + flight.dayOfWeek + ", Day number in week: " + flight.dayNbInWeek;
                    }
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3) != -1)
                    {
                        newFPRow[GlobalNames.sFPD_A_Column_User3] = "Capacity Class: " + flight.aircraftCapacityClass 
                                                                        + ", Body Category: " + flight.aircraftBodyCategory;
                    }
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4) != -1)
                    {
                        newFPRow[GlobalNames.sFPD_A_Column_User4] = "Airport Name: " + flight.airportName 
                                                                  + ", VIA: " + flight.via 
                                                                  + " Handler: " + flight.handlerAgent
                                                                  + ", OBS: " + flight.obs;
                    }
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5) != -1)
                    {
                        if (flight.linkFlightNb != null && flight.linkFlightDate != DateTime.MinValue)
                        {
                            newFPRow[GlobalNames.sFPD_A_Column_User5] = "Link Flight Nb: " + flight.linkFlightNb
                                                                        + ", Link Flight Date: " + flight.linkFlightDate.Date;
                        }
                    }
                    
                    flightPlanWithImportedData.Rows.Add(newFPRow);
                    rowNb++;
                }

                flightPlanWithImportedData.AcceptChanges();
            }
            return flightPlanWithImportedData;
        }

        public DataTable generateDepartureFlightPlanWithImportedData()
        {
            DataTable flightPlanWithImportedData = null;
            if (currentDepartureFlightPlan != null && departureFlightsDictionary.Count > 0)
            {
                flightPlanWithImportedData = currentDepartureFlightPlan.Clone();
                flightPlanWithImportedData.TableName = IMPORTED_FPD_TABLE_NAME;
                                
                foreach (KeyValuePair<String, LiegeTools.FlightInfoHolder> departureFlightPair in departureFlightsDictionary)
                {
                    DataRow newFPRow = flightPlanWithImportedData.NewRow();
                    LiegeTools.FlightInfoHolder flight = departureFlightPair.Value;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_ID] = flight.id;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_DATE] = flight.flightDate;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_STD) != -1)
                        newFPRow[GlobalNames.sFPD_Column_STD] = flight.flightTime;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AirlineCode] = flight.airlineCode;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_FlightN] = flight.flightNumber;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AirportCode] = flight.airportCode;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_FlightCategory] = flight.flightCategory;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_AircraftType] = flight.aircraftType;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TSA) != -1)
                        newFPRow[GlobalNames.sFPD_Column_TSA] = false;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_NbSeats] = 0;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI) != -1)
                        newFPRow[GlobalNames.sFPD_Column_TerminalCI] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_CI_Start] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_CI_End] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_FB_CI_Start] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_FB_CI_End] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_Drop_Start] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_Drop_End] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_FB_Drop_Start] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_FB_Drop_End] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_TerminalGate] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate) != -1)
                        newFPRow[GlobalNames.sFPD_Column_BoardingGate] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup) != -1)
                        newFPRow[GlobalNames.sFPD_Column_TerminalMup] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_Mup_Start] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_Eco_Mup_End] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start) != -1)
                        newFPRow[GlobalNames.sFPD_Column_First_Mup_Start] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End) != -1)
                        newFPRow[GlobalNames.sFPD_Column_First_Mup_End] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_TerminalParking] = DEFAULT_RESOURCE_INDEX;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_Parking] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_RunWay] = DEFAULT_RESOURCE_INDEX;

                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1) != -1)
                        newFPRow[GlobalNames.sFPD_A_Column_User1] = flight.schengenInfo + ", " + flight.ueeInfo;
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2) != -1)
                    {
                        newFPRow[GlobalNames.sFPD_A_Column_User2] = flight.arrivalOrDepartureTag + ", "
                            + flight.dayOfWeek + ", Day number in week: " + flight.dayNbInWeek;
                    }
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3) != -1)
                    {
                        newFPRow[GlobalNames.sFPD_A_Column_User3] = "Capacity Class: " + flight.aircraftCapacityClass
                                                                        + ", Body Category: " + flight.aircraftBodyCategory;
                    }
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4) != -1)
                    {
                        newFPRow[GlobalNames.sFPD_A_Column_User4] = "Airport Name: " + flight.airportName
                                                                  + ", VIA: " + flight.via
                                                                  + " Handler: " + flight.handlerAgent
                                                                  + ", OBS: " + flight.obs;
                    }
                    if (flightPlanWithImportedData.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5) != -1)
                    {
                        if (flight.linkFlightNb != null && flight.linkFlightDate != DateTime.MinValue)
                        {                            
                            newFPRow[GlobalNames.sFPD_A_Column_User5] = "Link Flight Nb: " + flight.linkFlightNb
                                                                        + ", Link Flight Date: " + flight.linkFlightDate.Date;
                        }
                    }

                    flightPlanWithImportedData.Rows.Add(newFPRow);                    
                }
                flightPlanWithImportedData.AcceptChanges();
            }
            return flightPlanWithImportedData;
        }
    }
}
