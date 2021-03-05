using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIMCORE_TOOL.Classes;
using System.IO;
using System.Xml;
using SIMCORE_TOOL.DataManagement;
using System.Windows.Forms;
using SIMCORE_TOOL.Classes;
using System.Collections;
using System.Globalization;
using SIMCORE_TOOL.com.crispico.processFlow;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.com.crispico.gantt
{
    class GanttServices
    {
        // Stores the intervals in which the Check-ins are opened.
        // The data provider is the Check-In (Opening) table
        // The key is the Check-in number.
        public static Dictionary<int, Task> allocationOpeningCI = new Dictionary<int, Task>();
        internal static PAX2SIM pax2simInst;
        public static List<Task> departureFlights = new List<Task>();
        public static bool createTask = true;
        //public static String exceptionType = "";

        /**
        * Load the departure flights from the given table
        * which can be the FPD or one of its filters
        */
        internal static List<Task> getDepartureFlightsForGantt(DataTable fpdInfo, PAX2SIM pax2sim)
        {
            //>Task T-21(ValueCoders):-Resetting the color radio button to initial states.

            DataTable convertedFPDInfo = new DataTable();
            pax2simInst = pax2sim;
            departureFlights.Clear();

            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
            {
                convertedFPDInfo = GestionDonneesHUB2SIM.ConvertFPDInformations(fpdInfo, pax2sim.DonneesEnCours.getRacine(), false, false);
            }

            #region Determine column indexes for FPD
            //FPD
            int indexColumnDepartureFlightID = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int indexColumnDepartureFlightN = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnDepartureFlightAirline = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int indexColumnDepartureFlightAirport = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            int indexColumnDepartureFlightDate = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            int indexColumnDepartureFlightTime = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
            int indexColumnDepartureFlightType = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
            int indexColumnDepartureFlightCategory = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int indexColumnEcoCheckInStart = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
            int indexColumnEcoCheckInEnd = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
            int indexColumnFBCheckInStart = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
            int indexColumnFBCheckInEnd = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);
            int indexColumnEcoBagDropStart = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
            int indexColumnEcoBagDropEnd = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
            int indexColumnFBBagDropStart = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
            int indexColumnFBBagDropEnd = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);
            int indexColumnEcoMakeUpStart = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
            int indexColumnEcoMakeUpEnd = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
            int indexColumnFBMakeUpStart = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
            int indexColumnFBMakeUpEnd = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);
            int indexColumnBoardingGate = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);
            int indexColumnDepartureParkingStand = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);
            int indexColumnDepartureRunWay = fpdInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay);
            #endregion

            #region Departure flights

            bool isArrival = false;

            // used for the alphaNumerical mode to store
            // the alphaNumerical value coresponding to the numerical value
            String taskName = "";
            String alphaNumericalFirst = "";
            String alphaNumericalLast = "";

            // retrive data from Check-In (Opening) table
            // for the Check-in resources (only for departure)
            // the information is stored in the allocationOpeningCI dictionary
            getOpeningIntervalsForCheckIns();

            //Load all data about each departure flight
            //from the FPD into the dictionary.
            foreach (DataRow drRow in fpdInfo.Rows)
            {
                Task depFlight = new Task();
                DataManagement.NormalTable octTable;
                int first;
                int last;
                String lineOpening = "";
                String lineClosing = "";
                DateTime globalStart = DateTime.MaxValue;
                DateTime globalEnd = DateTime.MinValue;
                DateTime finalSubTaskStart = new DateTime();
                DateTime finalSubTaskEnd = new DateTime();

                //Eco. Check-In, FB Check-In

                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_CITableName);
                lineOpening = GlobalNames.sOCT_CI_Line_Opening;
                lineClosing = GlobalNames.sOCT_CI_Line_Closing;

                //Eco. Check-In
                Task ecoCheckIn = new Task();
                //first = FonctionsType.getInt(drRow[indexColumnEcoCheckInStart]);
                //last = FonctionsType.getInt(drRow[indexColumnEcoCheckInEnd]);
                alphaNumericalFirst = "";
                alphaNumericalLast = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    alphaNumericalFirst = drRow[indexColumnEcoCheckInStart].ToString();
                    alphaNumericalLast = drRow[indexColumnEcoCheckInEnd].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnEcoCheckInStart]).ToString(), out first);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnEcoCheckInEnd]).ToString(), out last);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnEcoCheckInStart]);
                    last = FonctionsType.getInt(drRow[indexColumnEcoCheckInEnd]);
                }
                ecoCheckIn = createTaskWithChildren(Model.ResourceTypes.EcoCheck_In, isArrival,
                                                    first, last, drRow, octTable, lineOpening, lineClosing,
                                                    out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo,
                                                    alphaNumericalFirst, alphaNumericalLast);
                if (finalSubTaskStart != DateTime.MaxValue && finalSubTaskEnd != DateTime.MinValue)
                {
                    if (globalStart > finalSubTaskStart)
                        globalStart = finalSubTaskStart;
                    if (globalEnd < finalSubTaskEnd)
                        globalEnd = finalSubTaskEnd;
                }
                if (ecoCheckIn.segments.Count != 0 &&
                    ecoCheckIn.segments[0].start < ecoCheckIn.segments[0].end)
                    depFlight.subTasks.Add(ecoCheckIn);

                //FB Check-In
                Task fbCheckIn = new Task();
                //first = FonctionsType.getInt(drRow[indexColumnFBCheckInStart]);
                //last = FonctionsType.getInt(drRow[indexColumnFBCheckInEnd]);
                alphaNumericalFirst = "";
                alphaNumericalLast = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    alphaNumericalFirst = drRow[indexColumnFBCheckInStart].ToString();
                    alphaNumericalLast = drRow[indexColumnFBCheckInEnd].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnFBCheckInStart]).ToString(), out first);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnFBCheckInEnd]).ToString(), out last);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnFBCheckInStart]);
                    last = FonctionsType.getInt(drRow[indexColumnFBCheckInEnd]);
                }
                fbCheckIn = createTaskWithChildren(Model.ResourceTypes.FBCheck_In, isArrival,
                                                   first, last, drRow, octTable, lineOpening, lineClosing,
                                                   out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo,
                                                   alphaNumericalFirst, alphaNumericalLast);
                if (finalSubTaskStart != DateTime.MaxValue && finalSubTaskEnd != DateTime.MinValue)
                {
                    if (globalStart > finalSubTaskStart)
                        globalStart = finalSubTaskStart;
                    if (globalEnd < finalSubTaskEnd)
                        globalEnd = finalSubTaskEnd;
                }
                // if (createTask)
                //   depFlight.subTasks.Add(ecoCheckIn);
                if (!depFlight.subTasks.Contains(ecoCheckIn))
                {
                    if (fbCheckIn.segments.Count != 0 &&
                        fbCheckIn.segments[0].start < fbCheckIn.segments[0].end)
                    {
                        fbCheckIn.segments[0].color = "0x00FF00";
                        depFlight.subTasks.Add(fbCheckIn);
                    }
                }
                else
                {
                    depFlight.subTasks.Remove(ecoCheckIn);

                    if (fbCheckIn.subTasks.Count > 0)
                        foreach (Task t in fbCheckIn.subTasks)
                            ecoCheckIn.subTasks.Add(t);

                    if (ecoCheckIn.segments[0].start > finalSubTaskStart)
                        ecoCheckIn.segments[0].start = finalSubTaskStart;
                    if (ecoCheckIn.segments[0].end < finalSubTaskEnd)
                        ecoCheckIn.segments[0].end = finalSubTaskEnd;
                    ecoCheckIn.segments[0].color = "0x008000";
                    ecoCheckIn.segments[0].middleText = fbCheckIn.segments[0].middleText;
                    depFlight.subTasks.Add(ecoCheckIn);
                }

                //Eco BagDrop and FB BagDrop resources
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_BaggDropTableName);
                lineOpening = GlobalNames.sOCT_BagDropOpening;
                lineClosing = GlobalNames.sOCT_BagDropClosing;

                //Eco BagDrop
                Task ecoBagDrop = new Task();
                //first = FonctionsType.getInt(drRow[indexColumnEcoBagDropStart]);
                //last = FonctionsType.getInt(drRow[indexColumnEcoBagDropEnd]);
                alphaNumericalFirst = "";
                alphaNumericalLast = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    alphaNumericalFirst = drRow[indexColumnEcoBagDropStart].ToString();
                    alphaNumericalLast = drRow[indexColumnEcoBagDropEnd].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnEcoBagDropStart]).ToString(), out first);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnEcoBagDropEnd]).ToString(), out last);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnEcoBagDropStart]);
                    last = FonctionsType.getInt(drRow[indexColumnEcoBagDropEnd]);
                }
                ecoBagDrop = createTaskWithChildren(Model.ResourceTypes.EcoBagDrop, isArrival,
                                                    first, last, drRow, octTable, lineOpening, lineClosing,
                                                    out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo,
                                                    alphaNumericalFirst, alphaNumericalLast);
                if (finalSubTaskStart != DateTime.MaxValue && finalSubTaskEnd != DateTime.MinValue)
                {
                    if (globalStart > finalSubTaskStart)
                        globalStart = finalSubTaskStart;
                    if (globalEnd < finalSubTaskEnd)
                        globalEnd = finalSubTaskEnd;
                }
                if (ecoBagDrop.segments.Count != 0 &&
                    ecoBagDrop.segments[0].start < ecoBagDrop.segments[0].end)
                    depFlight.subTasks.Add(ecoBagDrop);

                //FB BagDrop
                Task fbBagDrop = new Task();
                //first = FonctionsType.getInt(drRow[indexColumnFBBagDropStart]);
                //last = FonctionsType.getInt(drRow[indexColumnFBBagDropEnd]);
                alphaNumericalFirst = "";
                alphaNumericalLast = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    alphaNumericalFirst = drRow[indexColumnFBBagDropStart].ToString();
                    alphaNumericalLast = drRow[indexColumnFBBagDropEnd].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnFBBagDropStart]).ToString(), out first);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnFBBagDropEnd]).ToString(), out last);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnFBBagDropStart]);
                    last = FonctionsType.getInt(drRow[indexColumnFBBagDropEnd]);
                }
                fbBagDrop = createTaskWithChildren(Model.ResourceTypes.FBBagDrop, isArrival,
                                                   first, last, drRow, octTable, lineOpening, lineClosing,
                                                   out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo,
                                                   alphaNumericalFirst, alphaNumericalLast);
                if (finalSubTaskStart != DateTime.MaxValue && finalSubTaskEnd != DateTime.MinValue)
                {
                    if (globalStart > finalSubTaskStart)
                        globalStart = finalSubTaskStart;
                    if (globalEnd < finalSubTaskEnd)
                        globalEnd = finalSubTaskEnd;
                }
                //if (createTask)
                //  depFlight.subTasks.Add(fbBagDrop);
                if (!depFlight.subTasks.Contains(ecoBagDrop))
                {
                    if (fbBagDrop.segments.Count != 0 &&
                        fbBagDrop.segments[0].start < fbBagDrop.segments[0].end)
                    {
                        fbBagDrop.segments[0].color = "0x603311";
                        depFlight.subTasks.Add(fbBagDrop);
                    }
                }
                else
                {
                    depFlight.subTasks.Remove(ecoBagDrop);

                    if (fbBagDrop.subTasks.Count > 0)
                        foreach (Task t in fbBagDrop.subTasks)
                            ecoBagDrop.subTasks.Add(t);

                    if (ecoBagDrop.segments[0].start > finalSubTaskStart)
                        ecoBagDrop.segments[0].start = finalSubTaskStart;
                    if (ecoBagDrop.segments[0].end < finalSubTaskEnd)
                        ecoBagDrop.segments[0].end = finalSubTaskEnd;
                    ecoBagDrop.segments[0].color = "0x608341";
                    ecoBagDrop.segments[0].middleText = fbBagDrop.segments[0].middleText;
                    depFlight.subTasks.Add(ecoBagDrop);
                }


                //Eco&FB MakeUp
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_MakeUpTableName);
                lineOpening = GlobalNames.sOCT_MakeUpOpening;
                lineClosing = GlobalNames.sOCT_MakeUpClosing;

                Task ecoMakeUp = new Task();
                //first = FonctionsType.getInt(drRow[indexColumnEcoMakeUpStart]);
                //last = FonctionsType.getInt(drRow[indexColumnEcoMakeUpEnd]);
                alphaNumericalFirst = "";
                alphaNumericalLast = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    alphaNumericalFirst = drRow[indexColumnEcoMakeUpStart].ToString();
                    alphaNumericalLast = drRow[indexColumnEcoMakeUpEnd].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnEcoMakeUpStart]).ToString(), out first);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnEcoMakeUpEnd]).ToString(), out last);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnEcoMakeUpStart]);
                    last = FonctionsType.getInt(drRow[indexColumnEcoMakeUpEnd]);
                }
                ecoMakeUp = createTaskWithChildren(Model.ResourceTypes.EcoMakeUp, isArrival,
                                                   first, last, drRow, octTable, lineOpening, lineClosing,
                                                   out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo,
                                                   alphaNumericalFirst, alphaNumericalLast);
                if (finalSubTaskStart != DateTime.MinValue && finalSubTaskEnd != DateTime.MaxValue)
                {
                    if (globalStart > finalSubTaskStart)
                        globalStart = finalSubTaskStart;
                    if (globalEnd < finalSubTaskEnd)
                        globalEnd = finalSubTaskEnd;
                }
                if (ecoMakeUp.segments.Count != 0 &&
                    ecoMakeUp.segments[0].start < ecoMakeUp.segments[0].end)
                    depFlight.subTasks.Add(ecoMakeUp);

                Task fbMakeUp = new Task();
                //first = FonctionsType.getInt(drRow[indexColumnFBMakeUpStart]);
                //last = FonctionsType.getInt(drRow[indexColumnFBMakeUpEnd]);
                alphaNumericalFirst = "";
                alphaNumericalLast = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    alphaNumericalFirst = drRow[indexColumnFBMakeUpStart].ToString();
                    alphaNumericalLast = drRow[indexColumnFBMakeUpEnd].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnFBMakeUpStart]).ToString(), out first);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnFBMakeUpEnd]).ToString(), out last);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnFBMakeUpStart]);
                    last = FonctionsType.getInt(drRow[indexColumnFBMakeUpEnd]);
                }
                fbMakeUp = createTaskWithChildren(Model.ResourceTypes.FBMakeUp, isArrival,
                                                  first, last, drRow, octTable, lineOpening, lineClosing,
                                                  out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo,
                                                  alphaNumericalFirst, alphaNumericalLast);
                if (finalSubTaskStart != DateTime.MaxValue && finalSubTaskEnd != DateTime.MinValue)
                {
                    if (globalStart > finalSubTaskStart)
                        globalStart = finalSubTaskStart;
                    if (globalEnd < finalSubTaskEnd)
                        globalEnd = finalSubTaskEnd;
                }
                // if (createTask)
                //   depFlight.subTasks.Add(fbMakeUp);
                if (!depFlight.subTasks.Contains(ecoMakeUp))
                {
                    if (fbMakeUp.segments.Count != 0 &&
                        fbMakeUp.segments[0].start < fbMakeUp.segments[0].end)
                    {
                        fbMakeUp.segments[0].color = "0x5D478B";
                        depFlight.subTasks.Add(fbMakeUp);
                    }
                }
                else
                {
                    depFlight.subTasks.Remove(ecoMakeUp);

                    if (fbMakeUp.subTasks.Count > 0)
                        foreach (Task t in fbMakeUp.subTasks)
                            ecoMakeUp.subTasks.Add(t);

                    if (ecoMakeUp.segments[0].start > finalSubTaskStart)
                        ecoMakeUp.segments[0].start = finalSubTaskStart;
                    if (ecoMakeUp.segments[0].end < finalSubTaskEnd)
                        ecoMakeUp.segments[0].end = finalSubTaskEnd;
                    ecoMakeUp.segments[0].color = "0x551A8B";
                    ecoMakeUp.segments[0].middleText = fbMakeUp.segments[0].middleText;
                    depFlight.subTasks.Add(ecoMakeUp);
                }


                //Boarding Gate resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_BoardGateTableName);
                lineOpening = GlobalNames.sOCT_Board_Line_Opening;
                lineClosing = GlobalNames.sOCT_Board_Line_Closing;
                //first = FonctionsType.getInt(drRow[indexColumnBoardingGate]);
                taskName = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    taskName = drRow[indexColumnBoardingGate].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnBoardingGate]).ToString(), out first);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnBoardingGate]);
                }
                Task boardingGate = createBasicTask(drRow, Model.ResourceTypes.BoardingGate, octTable,
                                                            lineOpening, lineClosing, isArrival, first,
                                                            out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo, taskName);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (boardingGate.segments.Count != 0 &&
                    boardingGate.segments[0].start < boardingGate.segments[0].end)
                {
                    boardingGate.segments[0].color = "0xD9D919";
                    depFlight.subTasks.Add(boardingGate);
                }

                //Parking Stand resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_ParkingTableName);
                lineOpening = GlobalNames.sOCT_ParkingOpening;
                lineClosing = GlobalNames.sOCT_ParkingClosing;
                //first = FonctionsType.getInt(drRow[indexColumnDepartureParkingStand]);
                taskName = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    taskName = drRow[indexColumnDepartureParkingStand].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnDepartureParkingStand]).ToString(), out first);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnDepartureParkingStand]);
                }
                Task parkingStand = createBasicTask(drRow, Model.ResourceTypes.ParkingStand, octTable,
                                                    lineOpening, lineClosing, isArrival, first,
                                                    out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo, taskName);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (parkingStand.segments.Count != 0 &&
                    parkingStand.segments[0].start < parkingStand.segments[0].end)
                {
                    parkingStand.segments[0].color = "0xFFFF00";
                    depFlight.subTasks.Add(parkingStand);
                }
                //Runway resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_RunwayTableName);
                lineOpening = GlobalNames.sOCT_RunwayOpening;
                lineClosing = GlobalNames.sOCT_RunwayClosing;
                //first = FonctionsType.getInt(drRow[indexColumnDepartureRunWay]);
                taskName = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPDInfo.Rows.Count > 0)
                {
                    taskName = drRow[indexColumnDepartureRunWay].ToString();
                    int rowIndex = fpdInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnDepartureFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPDInfo.Rows[rowIndex][indexColumnDepartureRunWay]).ToString(), out first);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnDepartureRunWay]);
                }
                Task runway = createBasicTask(drRow, Model.ResourceTypes.Runway, octTable,
                                              lineOpening, lineClosing, isArrival, first,
                                              out finalSubTaskStart, out finalSubTaskEnd, depFlight, fpdInfo, taskName);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (runway.segments.Count != 0 &&
                    runway.segments[0].start < runway.segments[0].end)
                {
                    runway.segments[0].color = "0xE2DDB5";
                    depFlight.subTasks.Add(runway);
                }

                //finalize the main task
                String flightName = drRow[indexColumnDepartureFlightID].ToString() + " / "
                                  + drRow[indexColumnDepartureFlightAirline].ToString() + " / "
                                  + drRow[indexColumnDepartureFlightN].ToString();
                depFlight.name = flightName;

                depFlight.arrivalDeparture = "D";

                Segment depFlightSegment = new Segment();
                depFlightSegment.start = globalStart;
                depFlightSegment.end = globalEnd;
                String flightDate = drRow[indexColumnDepartureFlightDate].ToString();
                flightDate.Trim();
                if (flightDate.Length >= 10)
                    flightDate = flightDate.Substring(0, 10);
                String flightTime = drRow[indexColumnDepartureFlightTime].ToString();
                flightTime.Trim();
                String middleTextForFlight = drRow[indexColumnDepartureFlightAirline] + " / "
                                           + drRow[indexColumnDepartureFlightN] + " / "
                                           + drRow[indexColumnDepartureFlightAirport] + " / "
                                           + drRow[indexColumnDepartureFlightCategory] + " / "
                                           + "STD " + flightDate + " " + flightTime + " / "
                                           + drRow[indexColumnDepartureFlightType];
                String flightId = drRow[indexColumnDepartureFlightID].ToString();
                depFlightSegment.middleText = middleTextForFlight;
                depFlightSegment.leftText = flightId;

                #region set the color for the flight according to the Flight Category
                int flightCategoryRowNb = 0;
                DataTable flightCategoryTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_FlightCategoriesTableName);
                if (pax2sim.colorByFlightCategory)
                {
                    String flightCategory = drRow[indexColumnDepartureFlightCategory].ToString();
                    int indexColumnFlightCategoryFC = flightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC);

                    foreach (DataRow flightCategoryTableRow in flightCategoryTable.Rows)
                    {
                        if (flightCategoryTableRow[indexColumnFlightCategoryFC].ToString().Equals(flightCategory))
                            break;
                        flightCategoryRowNb++;
                    }
                    // the flight category from FP has been found in the Flight Categories table
                    int colorIndex = flightCategoryRowNb + 1;
                    if (flightCategoryRowNb < flightCategoryTable.Rows.Count)
                        depFlightSegment.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());
                }
                else if (pax2sim.colorByAirlineCode)
                {
                    ArrayList airlineCodes = new ArrayList();
                    DataTable colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                    int indexColumnColorCriteria = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                    if (colorCriteriaTable != null)
                    {
                        foreach (DataRow line in colorCriteriaTable.Rows)
                        {
                            if (!airlineCodes.Contains(line[indexColumnColorCriteria].ToString()))
                                airlineCodes.Add(line[indexColumnColorCriteria].ToString());
                        }
                        airlineCodes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                    }
                    string airline = "";
                    if (drRow[indexColumnDepartureFlightAirline] != null)
                        airline = drRow[indexColumnDepartureFlightAirline].ToString();
                    depFlightSegment.color = "" + (OverallTools.FonctionUtiles.getColor(airlineCodes.IndexOf(airline)).ToArgb());
                }
                else if (pax2sim.colorByGroundHandlerCode)
                {
                    ArrayList groundHandlingCompanies = new ArrayList();
                    DataTable colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                    int iIndexAirlineCodes_ColumnFlightAirlineGroundHandler = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_GroundHandlers);
                    int iIndexAirlineCodes_ColumnFlightAirline = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                    if (colorCriteriaTable != null)
                    {
                        foreach (DataRow line in colorCriteriaTable.Rows)
                        {
                            if (!groundHandlingCompanies.Contains(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString()))
                                groundHandlingCompanies.Add(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString());
                        }
                        groundHandlingCompanies.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());

                        string airline = "";
                        if (drRow[indexColumnDepartureFlightAirline] != null)
                        {
                            airline = drRow[indexColumnDepartureFlightAirline].ToString();
                            string flightAirlineGroundHandler = OverallTools.DataFunctions
                                .getValue(colorCriteriaTable, airline, iIndexAirlineCodes_ColumnFlightAirline, iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                            int colorIndex = 0;
                            if (flightAirlineGroundHandler != null && flightAirlineGroundHandler != "" && groundHandlingCompanies.Count > 0)
                                colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                            depFlightSegment.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());
                        }
                    }
                }
                #endregion

                //set the flight segment form
                depFlightSegment.drawComposed = true;

                depFlight.segments.Add(depFlightSegment);

                // add the flight only if the its flight category has been found in the Flight categories table
                if (flightCategoryRowNb < flightCategoryTable.Rows.Count)
                    departureFlights.Add(depFlight);
            }
            #endregion

            return departureFlights;
        }

        /**
        * Load the arrival flights from the given table
        * which can be the FPA or one of its filters
        */
        internal static List<Task> getArrivalFlightsForGantt(DataTable fpaInfo, PAX2SIM pax2sim)
        {
            DataTable convertedFPAInfo = new DataTable();
            pax2simInst = pax2sim;

            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
            {
                convertedFPAInfo = GestionDonneesHUB2SIM.ConvertFPAInformations(fpaInfo, pax2sim.DonneesEnCours.getRacine(), false, false);
            }

            #region Determine column indexes for FPA
            //FPA
            int indexColumnArrivalFlightID = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int indexColumnArrivalFlightN = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnArrivalFlightAirline = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int indexColumnArrivalFlightAirport = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            int indexColumnArrivalFlightAircraftType = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
            int indexColumnArrivalFlightDate = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            int indexColumnArrivalFlightTime = fpaInfo.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
            int indexColumnArrivalFlightCategory = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int indexColumnArrivalBaggageClaim = fpaInfo.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
            int indexColumnArrivalInfeedStart = fpaInfo.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject);
            int indexColumnArrivalInfeedEnd = fpaInfo.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject);
            int indexColumnArrivalTransferInfeed = fpaInfo.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);
            int indexColumnArrivalGate = fpaInfo.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate);
            int indexColumnArrivalParkingStand = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);
            int indexColumnArrivalRunWay = fpaInfo.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay);

            #endregion

            #region Arrival flights
            List<Task> arrivalFlights = new List<Task>();
            bool isArrival = true;
            // used for the alphaNumerical mode to store
            // the alphaNumerical value coresponding to the numerical value
            String taskName = "";
            String alphaNumericalFirst = "";
            String alphaNumericalLast = "";

            //Load all data about each arrival flight
            //from the FPA into a list.            
            foreach (DataRow drRow in fpaInfo.Rows)
            {
                Task arrFlight = new Task();
                DataManagement.NormalTable octTable;
                int first;
                int last;
                String lineOpening = "";
                String lineClosing = "";
                DateTime globalStart = DateTime.MaxValue;
                DateTime globalEnd = DateTime.MinValue;
                DateTime finalSubTaskStart = new DateTime();
                DateTime finalSubTaskEnd = new DateTime();

                //Baggage Claim resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_BaggageClaimTableName);
                lineOpening = GlobalNames.sOCT_Baggage_Line_Opening;
                lineClosing = GlobalNames.sOCT_Baggage_Line_Closing;
                //first = FonctionsType.getInt(drRow[indexColumnArrivalBaggageClaim]);
                taskName = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPAInfo.Rows.Count > 0)
                {
                    taskName = drRow[indexColumnArrivalBaggageClaim].ToString();
                    int rowIndex = fpaInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnArrivalFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPAInfo.Rows[rowIndex][indexColumnArrivalBaggageClaim]).ToString(), out first);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnArrivalBaggageClaim]);
                }
                Task baggageClaim = createBasicTask(drRow, Model.ResourceTypes.BaggageClaim, octTable,
                                                    lineOpening, lineClosing, isArrival, first,
                                                    out finalSubTaskStart, out finalSubTaskEnd, arrFlight, fpaInfo, taskName);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (baggageClaim.segments.Count != 0 &&
                    baggageClaim.segments[0].start < baggageClaim.segments[0].end && baggageClaim.taskNumber > 0)
                {
                    arrFlight.subTasks.Add(baggageClaim);
                }
                //arrFlight.subTasks.Add(baggageClaim);

                //Arrival Infeed resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_ArrivalInfeedTableName);
                lineOpening = GlobalNames.sOCT_Arrival_Infeed_Line_Opening;
                lineClosing = GlobalNames.sOCT_Arrival_Infeed_Line_Closing;
                //first = FonctionsType.getInt(drRow[indexColumnArrivalInfeedStart]);
                //last = FonctionsType.getInt(drRow[indexColumnArrivalInfeedEnd]);
                Task arrInfeed = new Task();
                alphaNumericalFirst = "";
                alphaNumericalLast = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPAInfo.Rows.Count > 0)
                {
                    alphaNumericalFirst = drRow[indexColumnArrivalInfeedStart].ToString();
                    alphaNumericalLast = drRow[indexColumnArrivalInfeedEnd].ToString();
                    int rowIndex = fpaInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnArrivalFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPAInfo.Rows[rowIndex][indexColumnArrivalInfeedStart]).ToString(), out first);
                    int.TryParse((convertedFPAInfo.Rows[rowIndex][indexColumnArrivalInfeedEnd]).ToString(), out last);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnArrivalInfeedStart]);
                    last = FonctionsType.getInt(drRow[indexColumnArrivalInfeedEnd]);
                }
                arrInfeed = createTaskWithChildren(Model.ResourceTypes.ArrivalInfeed, isArrival,
                                                   first, last, drRow, octTable, lineOpening, lineClosing,
                                                   out finalSubTaskStart, out finalSubTaskEnd, arrFlight, fpaInfo,
                                                   alphaNumericalFirst, alphaNumericalLast);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (arrInfeed.segments.Count != 0 &&
                    arrInfeed.segments[0].start < arrInfeed.segments[0].end)
                {
                    arrInfeed.segments[0].color = "0x00FF00";
                    arrFlight.subTasks.Add(arrInfeed);
                }
                //arrFlight.subTasks.Add(arrInfeed);

                //Transfer Infeed resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.TransferInfeedAllocationRulesTableName);
                lineOpening = GlobalNames.sOCT_TransferInfeedOpening;
                lineClosing = GlobalNames.sOCT_TransferInfeedClosing;
                //first = FonctionsType.getInt(drRow[indexColumnArrivalTransferInfeed]);
                taskName = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPAInfo.Rows.Count > 0)
                {
                    taskName = drRow[indexColumnArrivalTransferInfeed].ToString();
                    int rowIndex = fpaInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnArrivalFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPAInfo.Rows[rowIndex][indexColumnArrivalTransferInfeed]).ToString(), out first);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnArrivalTransferInfeed]);
                }
                Task transferInfeed = createBasicTask(drRow, Model.ResourceTypes.TransferInfeed, octTable,
                                                      lineOpening, lineClosing, isArrival, first,
                                                      out finalSubTaskStart, out finalSubTaskEnd, arrFlight, fpaInfo, taskName);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (transferInfeed.segments.Count != 0 &&
                    transferInfeed.segments[0].start < transferInfeed.segments[0].end && transferInfeed.taskNumber > 0)
                {
                    arrFlight.subTasks.Add(transferInfeed);
                }
                //arrFlight.subTasks.Add(transferInfeed);

                //Arrival Gate resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_ArrivalGateTableName);
                lineOpening = GlobalNames.sOCT_Arrival_Gate_Line_Opening;
                lineClosing = GlobalNames.sOCT_Arrival_Gate_Line_Closing;
                //first = FonctionsType.getInt(drRow[indexColumnArrivalGate]);
                taskName = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPAInfo.Rows.Count > 0)
                {
                    taskName = drRow[indexColumnArrivalGate].ToString();
                    int rowIndex = fpaInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnArrivalFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPAInfo.Rows[rowIndex][indexColumnArrivalGate]).ToString(), out first);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnArrivalGate]);
                }
                Task arrGate = createBasicTask(drRow, Model.ResourceTypes.ArrivalGate, octTable,
                                                      lineOpening, lineClosing, isArrival, first,
                                                      out finalSubTaskStart, out finalSubTaskEnd, arrFlight, fpaInfo, taskName);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (arrGate.segments.Count != 0 &&
                    arrGate.segments[0].start < arrGate.segments[0].end)
                {
                    arrGate.segments[0].color = "0xD9D919";
                    arrFlight.subTasks.Add(arrGate);
                }
                //arrFlight.subTasks.Add(arrGate);

                //Parking Stand resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_ParkingTableName);
                lineOpening = GlobalNames.sOCT_ParkingOpening;
                lineClosing = GlobalNames.sOCT_ParkingClosing;
                //first = FonctionsType.getInt(drRow[indexColumnArrivalParkingStand]);
                taskName = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPAInfo.Rows.Count > 0)
                {
                    taskName = drRow[indexColumnArrivalParkingStand].ToString();
                    int rowIndex = fpaInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnArrivalFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPAInfo.Rows[rowIndex][indexColumnArrivalParkingStand]).ToString(), out first);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnArrivalParkingStand]);
                }
                Task parkingStand = createBasicTask(drRow, Model.ResourceTypes.ParkingStand, octTable,
                                                    lineOpening, lineClosing, isArrival, first,
                                                    out finalSubTaskStart, out finalSubTaskEnd, arrFlight, fpaInfo, taskName);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (parkingStand.segments.Count != 0 &&
                    parkingStand.segments[0].start < parkingStand.segments[0].end)
                {
                    parkingStand.segments[0].color = "0xFFFF00";
                    arrFlight.subTasks.Add(parkingStand);
                }
                //arrFlight.subTasks.Add(parkingStand);

                //Runway resource
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_RunwayTableName);
                lineOpening = GlobalNames.sOCT_RunwayOpening;
                lineClosing = GlobalNames.sOCT_RunwayClosing;
                //first = FonctionsType.getInt(drRow[indexColumnArrivalRunWay]);
                taskName = "";
                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && convertedFPAInfo.Rows.Count > 0)
                {
                    taskName = drRow[indexColumnArrivalRunWay].ToString();
                    int rowIndex = fpaInfo.Rows.IndexOf(drRow);
                    //int.TryParse(drRow[indexColumnArrivalFlightID].ToString(), out rowIndex);
                    int.TryParse((convertedFPAInfo.Rows[rowIndex][indexColumnArrivalRunWay]).ToString(), out first);
                }
                else
                {
                    first = FonctionsType.getInt(drRow[indexColumnArrivalRunWay]);
                }
                Task runway = createBasicTask(drRow, Model.ResourceTypes.Runway, octTable,
                                              lineOpening, lineClosing, isArrival, first,
                                              out finalSubTaskStart, out finalSubTaskEnd, arrFlight, fpaInfo, taskName);
                if (globalStart > finalSubTaskStart)
                    globalStart = finalSubTaskStart;
                if (globalEnd < finalSubTaskEnd)
                    globalEnd = finalSubTaskEnd;
                if (runway.segments.Count != 0 &&
                    runway.segments[0].start < runway.segments[0].end)
                {
                    runway.segments[0].color = "0xE2DDB5";
                    arrFlight.subTasks.Add(runway);
                }
                //arrFlight.subTasks.Add(runway);

                #region finalize the main task
                // Flight name = flight ID + Airline code + flight number
                String flightName = drRow[indexColumnArrivalFlightID].ToString() + " / "
                                  + drRow[indexColumnArrivalFlightAirline].ToString() + " / "
                                  + drRow[indexColumnArrivalFlightN].ToString();
                arrFlight.name = flightName;
                arrFlight.arrivalDeparture = "A";

                Segment arrFlightSegment = new Segment();
                String flightDate = drRow[indexColumnArrivalFlightDate].ToString();
                flightDate.Trim();
                if (flightDate.Length >= 10)
                    flightDate = flightDate.Substring(0, 10);
                String flightTime = drRow[indexColumnArrivalFlightTime].ToString();
                flightTime.Trim();
                String middleTextForFlight = drRow[indexColumnArrivalFlightAirline] + " / "
                                           + drRow[indexColumnArrivalFlightN] + " / "
                                           + drRow[indexColumnArrivalFlightAirport] + " / "
                                           + drRow[indexColumnArrivalFlightCategory] + " / "
                                           + "STA " + flightDate + " " + flightTime + " / "
                                           + drRow[indexColumnArrivalFlightAircraftType];
                String flightId = drRow[indexColumnArrivalFlightID].ToString();
                arrFlightSegment.middleText = middleTextForFlight;
                arrFlightSegment.leftText = flightId;

                arrFlightSegment.start = globalStart;
                arrFlightSegment.end = globalEnd;

                #region set the color for the flight according to the Flight Category
                int flightCategoryRowNb = 0;
                DataTable flightCategoryTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_FlightCategoriesTableName);
                if (pax2sim.colorByFlightCategory)
                {
                    String flightCategory = drRow[indexColumnArrivalFlightCategory].ToString();
                    int indexColumnFlightCategoryFC = flightCategoryTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC);

                    foreach (DataRow flightCategoryTableRow in flightCategoryTable.Rows)
                    {
                        if (flightCategoryTableRow[indexColumnFlightCategoryFC].ToString().Equals(flightCategory))
                            break;
                        flightCategoryRowNb++;
                    }
                    // the flight category from FP has been found in the Flight Categories table
                    int colorIndex = flightCategoryRowNb + 1;
                    if (flightCategoryRowNb < flightCategoryTable.Rows.Count)
                        arrFlightSegment.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());
                }
                else if (pax2sim.colorByAirlineCode)
                {
                    ArrayList airlineCodes = new ArrayList();
                    DataTable colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                    int indexColumnColorCriteria = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                    if (colorCriteriaTable != null)
                    {
                        foreach (DataRow line in colorCriteriaTable.Rows)
                        {
                            if (!airlineCodes.Contains(line[indexColumnColorCriteria].ToString()))
                                airlineCodes.Add(line[indexColumnColorCriteria].ToString());
                        }
                        airlineCodes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                    }
                    string airline = "";
                    if (drRow[indexColumnArrivalFlightAirline] != null)
                        airline = drRow[indexColumnArrivalFlightAirline].ToString();
                    arrFlightSegment.color = "" + (OverallTools.FonctionUtiles.getColor(airlineCodes.IndexOf(airline)).ToArgb());
                }
                else if (pax2sim.colorByGroundHandlerCode)
                {
                    ArrayList groundHandlingCompanies = new ArrayList();
                    DataTable colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                    int iIndexAirlineCodes_ColumnFlightAirlineGroundHandler = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_GroundHandlers);
                    int iIndexAirlineCodes_ColumnFlightAirline = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                    if (colorCriteriaTable != null)
                    {
                        foreach (DataRow line in colorCriteriaTable.Rows)
                        {
                            if (!groundHandlingCompanies.Contains(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString()))
                                groundHandlingCompanies.Add(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString());
                        }
                        groundHandlingCompanies.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());

                        string airline = "";
                        if (drRow[indexColumnArrivalFlightAirline] != null)
                        {
                            airline = drRow[indexColumnArrivalFlightAirline].ToString();
                            string flightAirlineGroundHandler = OverallTools.DataFunctions
                                .getValue(colorCriteriaTable, airline, iIndexAirlineCodes_ColumnFlightAirline, iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                            int colorIndex = 0;
                            if (flightAirlineGroundHandler != null && flightAirlineGroundHandler != "" && groundHandlingCompanies.Count > 0)
                                colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                            arrFlightSegment.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());
                        }
                    }
                }
                #endregion

                arrFlight.segments.Add(arrFlightSegment);

                //set the flight segment form
                arrFlightSegment.drawComposed = true;
                #endregion

                // add the flight only if the its flight category has been found in the Flight categories table
                if (flightCategoryRowNb < flightCategoryTable.Rows.Count)
                    arrivalFlights.Add(arrFlight);
            }

            #endregion

            return arrivalFlights;
        }

        private static Task createBasicTask(DataRow drFlight, Model.ResourceTypes resourceType, DataManagement.NormalTable OCTTable,
                                     String lineOpening, String lineClosing, bool isArrival, int taskNumber,
                                     out DateTime start, out DateTime end, Task currentFlight, DataTable flightPlan)
        {
            return createBasicTask(drFlight, resourceType, OCTTable, lineOpening, lineClosing, isArrival, taskNumber,
                                     out start, out end, currentFlight, flightPlan, "");
        }

        /**
         * Creates a basic task. This type of task
         * doesn't have any children => its task list is empty.        * 
         */
        private static Task createBasicTask(DataRow drFlight, Model.ResourceTypes resourceType, DataManagement.NormalTable OCTTable,
                                     String lineOpening, String lineClosing, bool isArrival, int taskNumber,
                                     out DateTime start, out DateTime end, Task currentFlight, DataTable flightPlan, String taskName)
        {
            #region terminal information
            String reclaimTerminal = "";
            String infeedsTerminal = "";
            String checkInTerminal = "";
            String makeUpTerminal = "";
            String gateTerminal = "";
            String parkingTerminal = "";

            if (isArrival)
            {
                int indexColumnReclaimTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim);
                int indexColumnInfeedTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);

                reclaimTerminal = drFlight[indexColumnReclaimTerminal].ToString();
                infeedsTerminal = drFlight[indexColumnInfeedTerminal].ToString();
            }
            else
            {
                int indexColumnCheckInTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
                int indexColumnMakeUpTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup);

                checkInTerminal = drFlight[indexColumnCheckInTerminal].ToString();
                makeUpTerminal = drFlight[indexColumnMakeUpTerminal].ToString();
            }
            int indexColumnGateTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int indexColumnParkingTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);

            gateTerminal = drFlight[indexColumnGateTerminal].ToString();
            parkingTerminal = drFlight[indexColumnParkingTerminal].ToString();

            #endregion

            String flightName = FonctionsType.getString(drFlight[4]);
            Task basicTask = new Task();
            basicTask.taskNumber = taskNumber;
            if (taskName.Length > 0)
            {
                basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName;
            }
            else
            {
                basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber;
            }

            Segment basicTaskSegment = new Segment();
            #region task color, middle text and  left text
            if (resourceType == Model.ResourceTypes.EcoCheck_In)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                   + "T " + checkInTerminal;

                    basicTaskSegment.middleText = "Economy " + taskName + " / "
                                                + "T " + checkInTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                   + "T " + checkInTerminal;

                    basicTaskSegment.middleText = "Economy " + taskNumber + " / "
                                                + "T " + checkInTerminal;
                }
                basicTaskSegment.color = "0x008000";
                //basicTaskSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.FBCheck_In)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                      + "T " + checkInTerminal;
                    basicTaskSegment.middleText = "First&Business " + taskName + " / "
                                                + "T " + checkInTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                    + "T " + checkInTerminal;
                    basicTaskSegment.middleText = "First&Business " + taskNumber + " / "
                                                + "T " + checkInTerminal;
                }
                basicTaskSegment.color = "0x00FF00";
                //basicTaskSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.EcoBagDrop)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                  + "T " + checkInTerminal;
                    basicTaskSegment.middleText = "Economy " + taskName + " / "
                                                + "T " + checkInTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                + "T " + checkInTerminal;
                    basicTaskSegment.middleText = "Economy " + taskNumber + " / "
                                                + "T " + checkInTerminal;
                }
                basicTaskSegment.color = "0x608341";
                //basicTaskSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.FBBagDrop)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                    + "T " + checkInTerminal;
                    basicTaskSegment.middleText = "First&Business " + taskName + " / "
                                                + "T " + checkInTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                + "T " + checkInTerminal;
                    basicTaskSegment.middleText = "First&Business " + taskNumber + " / "
                                                + "T " + checkInTerminal;
                }
                basicTaskSegment.color = "0x603311";
                // basicTaskSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.EcoMakeUp)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                     + "T " + makeUpTerminal;
                    basicTaskSegment.middleText = "Economy " + taskName + " / "
                                                + "T " + makeUpTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                     + "T " + makeUpTerminal;
                    basicTaskSegment.middleText = "Economy " + taskNumber + " / "
                                                + "T " + makeUpTerminal;
                }
                basicTaskSegment.color = "0x5D478B";
                // basicTaskSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.FBMakeUp)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                    + "T " + makeUpTerminal;
                    basicTaskSegment.middleText = "First&Business " + taskName + " / "
                                                + "T " + makeUpTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                    + "T " + makeUpTerminal;
                    basicTaskSegment.middleText = "First&Business " + taskNumber + " / "
                                                + "T " + makeUpTerminal;
                }
                basicTaskSegment.color = "0x551A8B";
                // basicTaskSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.BoardingGate)
            {
                basicTaskSegment.color = "0xD9D919";
                basicTaskSegment.leftText = flightName;
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                    + "T " + gateTerminal;

                    basicTaskSegment.middleText = "Boarding gate  " + taskName + " / "
                                                                + "T " + gateTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                    + "T " + gateTerminal;

                    basicTaskSegment.middleText = "Boarding gate  " + taskNumber + " / "
                                                                + "T " + gateTerminal;
                }
            }
            if (resourceType == Model.ResourceTypes.ArrivalGate)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                    + "T " + gateTerminal;
                    basicTaskSegment.middleText = "Arrival gate  " + taskName + " / "
                                                                + "T " + gateTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                    + "T " + gateTerminal;
                    basicTaskSegment.middleText = "Arrival gate  " + taskNumber + " / "
                                                                + "T " + gateTerminal;
                }
                basicTaskSegment.leftText = flightName;
                basicTaskSegment.color = "0xD9D919";
            }
            if (resourceType == Model.ResourceTypes.ParkingStand)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                    + "T " + parkingTerminal;
                    basicTaskSegment.middleText = "Parking stand  " + taskName + " / "
                                                + "T " + parkingTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                    + "T " + parkingTerminal;
                    basicTaskSegment.middleText = "Parking stand  " + taskNumber + " / "
                                                + "T " + parkingTerminal;
                }
                basicTaskSegment.leftText = flightName;
                basicTaskSegment.color = "0xFFFF00";
            }
            if (resourceType == Model.ResourceTypes.Runway)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                    basicTaskSegment.middleText = "Runway  " + taskName + ".";
                else
                    basicTaskSegment.middleText = "Runway  " + taskNumber + ".";
                basicTaskSegment.leftText = flightName;
                basicTaskSegment.color = "0xE2DDB5";
            }
            if (resourceType == Model.ResourceTypes.BaggageClaim)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                    + "T " + reclaimTerminal;
                    basicTaskSegment.middleText = "Baggage claim" + taskName + " / "
                                                + "T " + reclaimTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                    + "T " + reclaimTerminal;
                    basicTaskSegment.middleText = "Baggage claim" + taskNumber + " / "
                                                + "T " + reclaimTerminal;
                }
                basicTaskSegment.leftText = flightName;
                basicTaskSegment.color = "0x008000";
            }
            if (resourceType == Model.ResourceTypes.ArrivalInfeed)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                    + "T " + infeedsTerminal;
                    basicTaskSegment.middleText = "Arrival Infeed" + taskName + " / "
                                                + "T " + infeedsTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                    + "T " + infeedsTerminal;
                    basicTaskSegment.middleText = "Arrival Infeed" + taskNumber + " / "
                                                + "T " + infeedsTerminal;
                }
                basicTaskSegment.color = "0x00FF00";
                //basicTaskSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.TransferInfeed)
            {
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskName + " / "
                                    + "T " + infeedsTerminal;
                    basicTaskSegment.middleText = "Transfer Infeed" + taskName + " / "
                                                + "T " + infeedsTerminal;
                }
                else
                {
                    basicTask.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType) + " " + taskNumber + " / "
                                    + "T " + infeedsTerminal;
                    basicTaskSegment.middleText = "Transfer Infeed" + taskNumber + " / "
                                                + "T " + infeedsTerminal;
                }
                basicTaskSegment.leftText = flightName;
                basicTaskSegment.color = "0x608341";
            }

            #endregion

            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();

            DateTime tmp = new DateTime();
            String FC = drFlight[GlobalNames.sFPD_A_Column_FlightCategory].ToString();
            String ID = drFlight[GlobalNames.sFPD_A_Column_ID].ToString();
            String Airline = drFlight[GlobalNames.sFPD_A_Column_AirlineCode].ToString();

            String sPrefixe = "";
            if (isArrival)
            {
                sPrefixe = "A_";
                tmp = OverallTools.DataFunctions.toDateTime(drFlight[GlobalNames.sFPD_A_Column_DATE], drFlight[GlobalNames.sFPA_Column_STA]);
            }
            else
            {
                sPrefixe = "D_";
                tmp = OverallTools.DataFunctions.toDateTime(drFlight[GlobalNames.sFPD_A_Column_DATE], drFlight[GlobalNames.sFPD_Column_STD]);
            }

            // if the resource type starts with FB then we must check if
            // there is a First&Bussines exception table
            // i = 1 => we search for a First&Bussines exception table
            int i = 0;
            if (resourceType.ToString().StartsWith(Model.fbExceptionIndicator))
                i = 1;
            Dictionary<String, String> dssOCTimes = ((DataManagement.ExceptionTable)OCTTable).GetInformationsColumns(i, sPrefixe + ID, Airline, FC);
            //basicTaskSegment.leftText += exceptionType;

            int oct_val1, oct_val2;

            // if the opening/closing times can't be determined the procedure stops and returns default values
            if (dssOCTimes == null)
            { //add error message
                oct_val1 = 0;
                oct_val2 = 0;
                start = DateTime.MaxValue;
                end = DateTime.MinValue;
                return basicTask;
            }

            Int32.TryParse(dssOCTimes[lineOpening], out oct_val1);
            Int32.TryParse(dssOCTimes[lineClosing], out oct_val2);
            if (isArrival)
            {
                startTime = tmp.AddMinutes(oct_val1);
                endTime = tmp.AddMinutes(oct_val2);
            }
            else
            {
                startTime = tmp.AddMinutes(-oct_val1);
                endTime = tmp.AddMinutes(-oct_val2);
            }
            if (startTime > endTime)
            {
                tmp = endTime;
                endTime = startTime;
                startTime = tmp;
            }

            #region oct oci tables
            //adaug in lista segmentele obtinute colorate corespunzator(grey - inactiv / normal - activ)
            // If the resource is based on Check-in then we check the data from Check-In (Opening)
            //stored in a dictionary -> key = the check-in number, value = the check-in with 
            //a list of segments representing the periods when the desk is opened.
            if ((resourceType == Model.ResourceTypes.EcoCheck_In) || (resourceType == Model.ResourceTypes.FBCheck_In)
                || (resourceType == Model.ResourceTypes.EcoBagDrop) || (resourceType == Model.ResourceTypes.FBBagDrop))
            {
                DateTime auxStartTime = startTime;
                DateTime auxEndTime = endTime;
                // we modify the schedule only for the check-ins
                // changed in the Check-In (Opening) table
                if (allocationOpeningCI.ContainsKey(taskNumber))
                {
                    Task taskFromOCI = new Task();

                    bool gotTask = allocationOpeningCI.TryGetValue(taskNumber, out taskFromOCI);
                    if (gotTask)
                    {
                        foreach (Segment s in taskFromOCI.segments)
                        {
                            if (auxStartTime < s.start)
                            {
                                if (auxEndTime <= s.start) //1 outside the opening CI time
                                {
                                    Segment period = new Segment();
                                    period.start = auxStartTime;
                                    period.end = auxEndTime;
                                    period.color = "0x000000";
                                    period.middleText = basicTaskSegment.middleText;
                                    if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                        period.position = Model.DOWN;
                                    if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                        period.position = Model.UP;
                                    basicTask.segments.Add(period);
                                    break;
                                }
                                else
                                {
                                    if (auxEndTime > s.end)//3 the OCI period is included in the OCT period
                                    {
                                        Segment period = new Segment(); // the period outside OCI period => grey color
                                        period.start = auxStartTime;
                                        period.end = s.start;
                                        period.color = "0x000000";
                                        period.middleText = basicTaskSegment.middleText;
                                        if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            period.position = Model.DOWN;
                                        if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            period.position = Model.UP;
                                        basicTask.segments.Add(period);

                                        Segment periodN = new Segment(); // the period included in the OCI period => normal color
                                        periodN.start = s.start;
                                        periodN.end = s.end;
                                        periodN.color = basicTaskSegment.color;
                                        periodN.middleText = basicTaskSegment.middleText;
                                        if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            periodN.position = Model.DOWN;
                                        if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            periodN.position = Model.UP;
                                        basicTask.segments.Add(periodN);

                                        auxStartTime = s.end; // continue the search for the rest of the OCT period

                                        //If the OCI period is the last one in the list
                                        //the remaining OCT period is outside the scheduel
                                        //=> grey color
                                        int segmentIndex = taskFromOCI.segments.IndexOf(s);
                                        if (segmentIndex == (taskFromOCI.segments.Count - 1))
                                        {
                                            Segment finalPeriod = new Segment();
                                            finalPeriod.start = auxStartTime;
                                            finalPeriod.end = auxEndTime;
                                            finalPeriod.color = "0x000000";
                                            finalPeriod.middleText = basicTaskSegment.middleText;
                                            if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                                finalPeriod.position = Model.DOWN;
                                            if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                                finalPeriod.position = Model.UP;
                                            basicTask.segments.Add(finalPeriod);
                                        }
                                    }
                                    else//2 the OCT period has 2 segments - one outside OCI period and one completly included
                                    {
                                        Segment period = new Segment(); // the OCT period outside the OCI period => grey color
                                        period.start = auxStartTime;
                                        period.end = s.start;
                                        period.color = "0x000000";
                                        period.middleText = basicTaskSegment.middleText;
                                        if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            period.position = Model.DOWN;
                                        if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            period.position = Model.UP;
                                        basicTask.segments.Add(period);
                                        //start = startTime;

                                        Segment periodN = new Segment(); // the OCT period included in the OCI period
                                        periodN.start = s.start;
                                        periodN.end = auxEndTime;
                                        periodN.color = basicTaskSegment.color;
                                        periodN.middleText = basicTaskSegment.middleText;
                                        if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            periodN.position = Model.DOWN;
                                        if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            periodN.position = Model.UP;
                                        basicTask.segments.Add(periodN);
                                        //end = endTime;
                                        //return basicTask;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (auxStartTime >= s.end) //4
                                {
                                    //search the next segment because the OCT period is 
                                    //outside the OCI period in the right part
                                    //(where other OCI periods can be found)
                                    //If the OCT period is outside the last OCI period
                                    // => grey color
                                    int segmentIndex = taskFromOCI.segments.IndexOf(s);
                                    if (segmentIndex == (taskFromOCI.segments.Count - 1))
                                    {
                                        Segment period = new Segment();
                                        period.start = auxStartTime;
                                        period.end = auxEndTime;
                                        period.color = "0x000000";
                                        period.middleText = basicTaskSegment.middleText;
                                        if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            period.position = Model.DOWN;
                                        if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            period.position = Model.UP;
                                        basicTask.segments.Add(period);
                                    }
                                }
                                else
                                {
                                    if (auxEndTime <= s.end) //5 the OCT period is included in the OCI period
                                    {
                                        Segment period = new Segment(); // normal segment
                                        period.start = auxStartTime;
                                        period.end = auxEndTime;
                                        period.color = basicTaskSegment.color;
                                        period.middleText = basicTaskSegment.middleText;
                                        if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            period.position = Model.DOWN;
                                        if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            period.position = Model.UP;
                                        basicTask.segments.Add(period);
                                        break;
                                    }
                                    else//6 the OCT period is partialy included in the OCI period and has a part outside in the right side
                                    {
                                        Segment periodN = new Segment(); // normal segment
                                        periodN.start = auxStartTime;
                                        periodN.end = s.end;
                                        periodN.color = basicTaskSegment.color;
                                        periodN.middleText = basicTaskSegment.middleText;
                                        if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            periodN.position = Model.DOWN;
                                        if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                            periodN.position = Model.UP;
                                        basicTask.segments.Add(periodN);

                                        // continue the search in the right side of the OCI period
                                        //where there might be other OCI periods
                                        auxStartTime = s.end;

                                        //If the OCI period is the last one in the list
                                        //the remaining OCT period is outside the scheduel
                                        //=> grey color
                                        int segmentIndex = taskFromOCI.segments.IndexOf(s);
                                        if (segmentIndex == (taskFromOCI.segments.Count - 1))
                                        {
                                            Segment period = new Segment();
                                            period.start = auxStartTime;
                                            period.end = auxEndTime;
                                            period.color = "0x000000";
                                            period.middleText = basicTaskSegment.middleText;
                                            if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                                period.position = Model.DOWN;
                                            if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                                                period.position = Model.UP;
                                            basicTask.segments.Add(period);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    basicTaskSegment.start = startTime;
                    basicTaskSegment.end = endTime;
                    if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                        basicTaskSegment.position = Model.UP;
                    if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.EcoMakeUp)
                        basicTaskSegment.position = Model.DOWN;
                    basicTask.segments.Add(basicTaskSegment);
                }

                if (resourceType == Model.ResourceTypes.FBCheck_In && currentFlight.subTasks != null)
                {//check the depFlight task for eco res
                    for (int j = 0; j < currentFlight.subTasks.Count; j++)
                    {
                        foreach (Task t in currentFlight.subTasks[j].subTasks)
                        {
                            if (t.taskNumber == taskNumber && t.name.Contains("Check_In"))
                            {
                                //basicTaskSegment.position = Model.UP;
                                //basicTaskSegment.start = basicTask.segments[0].start;
                                //basicTaskSegment.end = basicTask.segments[0].end;
                                foreach (Segment s in basicTask.segments)
                                {
                                    s.position = Model.UP;
                                    //s.leftText = exceptionType;
                                    //exceptionType = "";
                                    t.segments.Add(s);

                                }
                                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                                    t.name = "Check_In " + taskName + " / T " + checkInTerminal;
                                else
                                    t.name = "Check_In " + t.taskNumber + " / T " + checkInTerminal;
                                //t.segments.Add(basicTaskSegment);
                                start = startTime;
                                end = endTime;
                                return t;
                            }
                        }
                    }
                }
                if (resourceType == Model.ResourceTypes.FBBagDrop && currentFlight.subTasks != null)
                {//check the depFlight task for eco res
                    for (int j = 0; j < currentFlight.subTasks.Count; j++)
                    {
                        foreach (Task t in currentFlight.subTasks[j].subTasks)
                        {
                            if (t.taskNumber == taskNumber && t.name.Contains("BagDrop"))
                            {
                                foreach (Segment s in basicTask.segments)
                                {
                                    s.position = Model.UP;
                                    t.segments.Add(s);
                                }
                                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                                    t.name = "BagDrop " + taskName + " / T " + checkInTerminal;
                                else
                                    t.name = "BagDrop " + t.taskNumber + " / T " + checkInTerminal;
                                start = startTime;
                                end = endTime;
                                return t;
                            }
                        }
                    }
                }

                start = startTime;
                end = endTime;
                return basicTask;
            }
            #endregion
            if (resourceType == Model.ResourceTypes.FBMakeUp && currentFlight.subTasks != null)
            {//check the depFlight task for eco res
                for (int j = 0; j < currentFlight.subTasks.Count; j++)
                {
                    foreach (Task t in currentFlight.subTasks[j].subTasks)
                    {
                        if (t.taskNumber == taskNumber && t.name.Contains("MakeUp"))
                        {
                            Segment fbOverEcoMakeUp = new Segment();
                            fbOverEcoMakeUp.start = t.segments[0].start;
                            fbOverEcoMakeUp.end = t.segments[0].end;
                            fbOverEcoMakeUp.position = Model.UP;
                            fbOverEcoMakeUp.color = basicTaskSegment.color;
                            fbOverEcoMakeUp.middleText = basicTaskSegment.middleText;
                            t.segments.Add(fbOverEcoMakeUp);
                            if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && taskName.Length > 0)
                                t.name = "MakeUp " + taskName + " / T " + makeUpTerminal;
                            else
                                t.name = "MakeUp " + taskNumber + " / T " + makeUpTerminal;
                            start = startTime;
                            end = endTime;
                            return t;
                        }
                    }
                }
            }
            if (resourceType == Model.ResourceTypes.EcoMakeUp)
                basicTaskSegment.position = Model.DOWN;
            if (resourceType == Model.ResourceTypes.FBMakeUp)
                basicTaskSegment.position = Model.UP;


            basicTaskSegment.start = startTime;
            basicTaskSegment.end = endTime;
            /*
            if (resourceType == Model.ResourceTypes.FBCheck_In)
            {//check the depFlight task for eco res
                foreach (Task t in currentFlight.subTasks[0].subTasks)
                {
                    if (t.taskNumber == taskNumber)
                    {
                        basicTaskSegment.position = Model.UP;
                        t.segments.Add(basicTaskSegment);
                        start = basicTaskSegment.start;
                        end = basicTaskSegment.end;
                        return t;
                    }
                }

            }
            */
            basicTask.segments.Add(basicTaskSegment);
            //test
            /*
            basicTaskSegment.position = Model.DOWN;
            basicTaskSegment.leftText = "Some info";
            basicTaskSegment.middleText = "Basic segment";
            basicTaskSegment.color = 0xFF0033;
            basicTask.segments.Add(basicTaskSegment);
            */
            /*
            Segment testSegment = new Segment();
            testSegment.start = startTime;
            testSegment.end = endTime.AddMinutes(-5);
            testSegment.position = Model.UP;
            testSegment.leftText = "Flight info";
            testSegment.middleText = "Test segment";
            testSegment.color = 0xFFFF00;
            basicTask.segments.Add(testSegment);
            */

            start = startTime;
            end = endTime;

            return basicTask;
        }
        internal static int firstEco = 999;
        internal static int lastEco = -1;
        private static Task createTaskWithChildren(Model.ResourceTypes resourceType, bool isArrival,
                                                   int first, int last, DataRow drRow, DataManagement.NormalTable octTable,
                                                   String lineOpening, String lineClosing, out DateTime start, out DateTime end,
                                                   Task currentFlight, DataTable flightPlan)
        {
            return createTaskWithChildren(resourceType, isArrival, first, last, drRow, octTable,
                                                   lineOpening, lineClosing, out start, out end,
                                                   currentFlight, flightPlan, "", "");
        }

        /**
         * Creates a task with children tasks. This type of task
         * will always have a parent task and a list of tasks.
         */
        private static Task createTaskWithChildren(Model.ResourceTypes resourceType, bool isArrival,
                                                   int first, int last, DataRow drRow, DataManagement.NormalTable octTable,
                                                   String lineOpening, String lineClosing, out DateTime start, out DateTime end,
                                                   Task currentFlight, DataTable flightPlan, String alpnaNumericalFirst,
                                                   String alpnaNumericalLast)
        {
            Task taskWithChildren = new Task();
            Segment taskWithChildrenSegment = new Segment();

            #region terminal information
            String infeedsTerminal = "";
            String checkInTerminal = "";
            String makeUpTerminal = "";
            int checkInTerminalNb = 0;
            int makeUpTerminalNb = 0;
            int InfeedsTerminalNb = 0;
            int indexColumnInfeedTerminal = 0;
            int indexColumnCheckInTerminal = 0;
            int indexColumnMakeUpTerminal = 0;


            if (isArrival)
            {
                indexColumnInfeedTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
                infeedsTerminal = drRow[indexColumnInfeedTerminal].ToString();
                int.TryParse(infeedsTerminal, out InfeedsTerminalNb);
            }
            else
            {
                indexColumnCheckInTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
                indexColumnMakeUpTerminal = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup);

                checkInTerminal = drRow[indexColumnCheckInTerminal].ToString();
                makeUpTerminal = drRow[indexColumnMakeUpTerminal].ToString();

                int.TryParse(checkInTerminal, out checkInTerminalNb);
                int.TryParse(makeUpTerminal, out makeUpTerminalNb);
            }
            #endregion

            String flightName = FonctionsType.getString(drRow[4]);
            taskWithChildren.name = Enum.GetName(typeof(Model.ResourceTypes), resourceType);

            #region task name, middle text and left text
            // resourceGroup is used to get the 
            // alphanumerical description of the resource
            String resourceGroup = "";
            int realFirst = 0;
            int realLast = 0;
            if (resourceType == Model.ResourceTypes.EcoCheck_In)
            {
                resourceGroup = PAX2SIM.sCheckInGroup;

                taskWithChildren.name = "Check_In" + " / T " + checkInTerminal;
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && alpnaNumericalFirst.Length > 0
                    && alpnaNumericalLast.Length > 0)
                {
                    /*
                    String alpnaNumericalFirst = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, first,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnCheckInTerminal, resourceGroup);
                    String alpnaNumericalLast = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, last,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnCheckInTerminal, resourceGroup);
                     */
                    taskWithChildrenSegment.middleText = "Check In from " + alpnaNumericalFirst + " to "
                                                        + alpnaNumericalLast + " / " + "T " + checkInTerminal;
                }
                else
                {
                    taskWithChildrenSegment.middleText = "Check In from " + first + " to " + last + " / "
                                                       + "T " + checkInTerminal;
                }
                firstEco = first;
                lastEco = last;
                taskWithChildrenSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.FBCheck_In)
            {
                resourceGroup = PAX2SIM.sCheckInGroup;

                taskWithChildren.name = "Check_In" + " / T " + checkInTerminal;
                if (first < firstEco)
                    realFirst = first;
                else
                    realFirst = firstEco;
                if (last > lastEco)
                    realLast = last;
                else
                    realLast = lastEco;
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && alpnaNumericalFirst.Length > 0
                    && alpnaNumericalLast.Length > 0)
                {
                    /*
                    String alpnaNumericalFirst = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, realFirst,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnCheckInTerminal, resourceGroup);
                    String alpnaNumericalLast = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, realLast,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnCheckInTerminal, resourceGroup);
                     */
                    taskWithChildrenSegment.middleText = "Check In from " + alpnaNumericalFirst + " to "
                                                        + alpnaNumericalLast + " / " + "T " + checkInTerminal;
                }
                else
                {
                    taskWithChildrenSegment.middleText = "Check In from " + realFirst + " to " + realLast + " / "
                                                   + "T " + checkInTerminal;
                }
                firstEco = 999;
                lastEco = -1;
                taskWithChildrenSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.EcoBagDrop)
            {
                resourceGroup = PAX2SIM.sCheckInGroup;

                taskWithChildren.name = "BagDrop" + " / T " + checkInTerminal;
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && alpnaNumericalFirst.Length > 0
                    && alpnaNumericalLast.Length > 0)
                {
                    /*
                    String alpnaNumericalFirst = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, first,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnCheckInTerminal, resourceGroup);
                    String alpnaNumericalLast = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, last,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnCheckInTerminal, resourceGroup);
                     */
                    taskWithChildrenSegment.middleText = "Check In from " + alpnaNumericalFirst + " to "
                                                        + alpnaNumericalLast + " / " + "T " + checkInTerminal;
                }
                else
                {
                    taskWithChildrenSegment.middleText = "Check In from " + first + " to " + last + " / "
                                                   + "T " + checkInTerminal;
                }
                firstEco = first;
                lastEco = last;
                taskWithChildrenSegment.leftText = flightName;


            }
            if (resourceType == Model.ResourceTypes.FBBagDrop)
            {
                resourceGroup = PAX2SIM.sCheckInGroup;

                taskWithChildren.name = "BagDrop" + " / T " + checkInTerminal;
                if (first < firstEco)
                    realFirst = first;
                else
                    realFirst = firstEco;
                if (last > lastEco)
                    realLast = last;
                else
                    realLast = lastEco;
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && alpnaNumericalFirst.Length > 0
                    && alpnaNumericalLast.Length > 0)
                {
                    /*
                    String alpnaNumericalFirst = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, realFirst,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnCheckInTerminal, resourceGroup);
                    String alpnaNumericalLast = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, realLast,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnCheckInTerminal, resourceGroup);
                     */
                    taskWithChildrenSegment.middleText = "Check In from " + alpnaNumericalFirst + " to "
                                                        + alpnaNumericalLast + " / " + "T " + checkInTerminal;
                }
                else
                {
                    taskWithChildrenSegment.middleText = "Check In from " + realFirst + " to " + realLast + " / "
                                                       + "T " + checkInTerminal;
                }
                firstEco = 999;
                lastEco = -1;
                taskWithChildrenSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.EcoMakeUp)
            {
                resourceGroup = Model.MAKE_UP_GROUP;

                taskWithChildren.name = "MakeUp" + " / T " + makeUpTerminal;
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && alpnaNumericalFirst.Length > 0
                    && alpnaNumericalLast.Length > 0)
                {
                    /*
                    String alpnaNumericalFirst = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, first,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnMakeUpTerminal, resourceGroup);
                    String alpnaNumericalLast = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, last,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnMakeUpTerminal, resourceGroup);
                     */
                    taskWithChildrenSegment.middleText = "Make up from " + alpnaNumericalFirst + " to "
                                                        + alpnaNumericalLast + " / " + "T " + makeUpTerminal;
                }
                else
                {
                    taskWithChildrenSegment.middleText = "Make up from " + first + " to " + last + " / "
                                                       + "T " + makeUpTerminal;
                }
                firstEco = first;
                lastEco = last;
                taskWithChildrenSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.FBMakeUp)
            {
                resourceGroup = Model.MAKE_UP_GROUP;

                taskWithChildren.name = "MakeUp" + " / T " + makeUpTerminal;
                if (first < firstEco)
                    realFirst = first;
                else
                    realFirst = firstEco;
                if (last > lastEco)
                    realLast = last;
                else
                    realLast = lastEco;
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && alpnaNumericalFirst.Length > 0
                    && alpnaNumericalLast.Length > 0)
                {
                    /*
                    String alpnaNumericalFirst = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, realFirst,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnMakeUpTerminal, resourceGroup);
                    String alpnaNumericalLast = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, realLast,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnMakeUpTerminal, resourceGroup);
                     */
                    taskWithChildrenSegment.middleText = "Make up from " + alpnaNumericalFirst + " to "
                                                        + alpnaNumericalLast + " / " + "T " + makeUpTerminal;
                }
                else
                {
                    taskWithChildrenSegment.middleText = "Make up from " + realFirst + " to " + realLast + " / "
                                                       + "T " + makeUpTerminal;
                }
                firstEco = 999;
                lastEco = -1;
                taskWithChildrenSegment.leftText = flightName;
            }
            if (resourceType == Model.ResourceTypes.ArrivalInfeed)
            {
                resourceGroup = Model.ARRIVAL_INFEED_GROUP;

                taskWithChildren.name = "ArrivalInfeed" + " / T " + infeedsTerminal;
                if (pax2simInst.DonneesEnCours.UseAlphaNumericForFlightInfo && alpnaNumericalFirst.Length > 0
                    && alpnaNumericalLast.Length > 0)
                {
                    /*
                    String alpnaNumericalFirst = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, first,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnInfeedTerminal, resourceGroup);
                    String alpnaNumericalLast = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, last,
                            pax2simInst.DonneesEnCours.getRacine(), indexColumnInfeedTerminal, resourceGroup);
                     */
                    taskWithChildrenSegment.middleText = "Infeed from " + alpnaNumericalFirst + " to "
                                                        + alpnaNumericalLast + " / " + "T " + infeedsTerminal;
                }
                else
                {
                    taskWithChildrenSegment.middleText = "Infeed from " + first + " to " + last + " / "
                                   + "T " + infeedsTerminal;
                }
                taskWithChildrenSegment.leftText = flightName;
            }

            #endregion

            DateTime subtaskStart = new DateTime();
            DateTime subtaskEnd = new DateTime();
            DateTime finalSubTaskStart = DateTime.MaxValue;
            DateTime finalSubTaskEnd = DateTime.MinValue;
            bool addTask = true;
            createTask = true;

            if (first <= last && first != 0 && last != 0)
            {
                for (int i = first; i <= last; i++)
                {
                    addTask = true;
                    createTask = true;

                    #region taskName from element description
                    String taskName = "";
                    if (resourceType == Model.ResourceTypes.EcoCheck_In || resourceType == Model.ResourceTypes.FBCheck_In
                        || resourceType == Model.ResourceTypes.EcoBagDrop || resourceType == Model.ResourceTypes.FBBagDrop)
                    {
                        taskName = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, i,
                            pax2simInst.DonneesEnCours.getRacine(), checkInTerminalNb, resourceGroup);
                    }
                    else if (resourceType == Model.ResourceTypes.EcoMakeUp || resourceType == Model.ResourceTypes.FBMakeUp)
                    {
                        taskName = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, i,
                            pax2simInst.DonneesEnCours.getRacine(), makeUpTerminalNb, resourceGroup);
                    }
                    else if (resourceType == Model.ResourceTypes.ArrivalInfeed)
                    {
                        taskName = getElementDescriptionByValueAndTypeAndTerminalNb(drRow, i,
                            pax2simInst.DonneesEnCours.getRacine(), InfeedsTerminalNb, resourceGroup);
                    }
                    #endregion

                    Task basicTaskWithChildren = createBasicTask(drRow, resourceType, octTable,
                                                                 lineOpening, lineClosing, isArrival, i,
                                                                 out subtaskStart, out subtaskEnd, currentFlight, flightPlan, taskName);
                    if (resourceType == Model.ResourceTypes.FBCheck_In && currentFlight.subTasks != null)
                    {
                        for (int j = 0; j < currentFlight.subTasks.Count; j++)
                        {
                            foreach (Task t in currentFlight.subTasks[j].subTasks)
                            {
                                if (t.taskNumber == i && t.name.Contains("Check_In"))
                                {
                                    addTask = false;
                                    createTask = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (resourceType == Model.ResourceTypes.FBBagDrop && currentFlight.subTasks != null)
                    {
                        for (int j = 0; j < currentFlight.subTasks.Count; j++)
                        {
                            foreach (Task t in currentFlight.subTasks[j].subTasks)
                            {
                                if (t.taskNumber == i && t.name.Contains("BagDrop"))
                                {
                                    addTask = false;
                                    createTask = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (resourceType == Model.ResourceTypes.FBMakeUp && currentFlight.subTasks != null)
                    {
                        for (int j = 0; j < currentFlight.subTasks.Count; j++)
                        {
                            foreach (Task t in currentFlight.subTasks[j].subTasks)
                            {
                                if (t.taskNumber == i && t.name.Contains("MakeUp"))
                                {
                                    addTask = false;
                                    createTask = false;
                                    break;
                                }
                            }
                        }
                    }


                    if (addTask)
                    {
                        taskWithChildren.subTasks.Add(basicTaskWithChildren);
                    }
                    if (finalSubTaskStart > subtaskStart)
                        finalSubTaskStart = subtaskStart;
                    if (finalSubTaskEnd < subtaskEnd)
                        finalSubTaskEnd = subtaskEnd;
                }
            }

            taskWithChildrenSegment.start = finalSubTaskStart;
            taskWithChildrenSegment.end = finalSubTaskEnd;
            taskWithChildren.segments.Add(taskWithChildrenSegment);

            start = finalSubTaskStart;
            end = finalSubTaskEnd;

            return taskWithChildren;
        }

        internal static String getElementDescriptionByValueAndTypeAndTerminalNb(DataRow drRow,
            int elementValue, XmlNode racine, int terminalNb, String sTypeElement)
        {
            String elementDescription = "";
            XmlNode xnGroup = null;
            xnGroup = GestionDonneesHUB2SIM.getGroup(racine, terminalNb, sTypeElement, elementValue);
            if (xnGroup == null)
                return "";
            for (int j = 0; j < xnGroup.ChildNodes.Count; j++)
            {
                if (xnGroup.ChildNodes[j].Attributes["Index"].Value.ToString() == elementValue.ToString())
                {
                    elementDescription = GestionDonneesHUB2SIM.extractDescription(xnGroup.ChildNodes[j].Attributes["Name"].Value);
                    return elementDescription;
                }
            }
            return elementDescription;
        }

        internal static String getParkingElementDescriptionByValueAndTerminalNb(DataRow drRow,
            int elementValue, XmlNode racine, int terminalNb, bool bUseAlpha)
        {
            XmlNode xnTerminal = null;
            if (bUseAlpha)
                xnTerminal = GestionDonneesHUB2SIM.getTerminal(racine, terminalNb);
            else
                xnTerminal = GestionDonneesHUB2SIM.getTerminalByDescription(racine, terminalNb.ToString());
            if (xnTerminal == null)
                return "";

            XmlNode xnParkings = null;
            foreach (XmlNode xnNode in xnTerminal.ChildNodes)
            {
                if (xnNode.Attributes["Type"].Value == "Aircraft Parking Stands")
                {
                    xnParkings = xnNode;
                    break;
                }
            }
            if (xnParkings == null)
                return "";

            for (int j = 0; j < xnParkings.ChildNodes.Count; j++)
            {
                String sDescription = GestionDonneesHUB2SIM.extractDescription(xnParkings.ChildNodes[j].Attributes["Name"].Value.ToString());
                if (!bUseAlpha && (elementValue.ToString() == sDescription.ToLower().Trim()))
                    return xnParkings.ChildNodes[j].Attributes["Index"].Value.ToString();
                else if ((bUseAlpha) && (elementValue.ToString() == xnParkings.ChildNodes[j].Attributes["Index"].Value))
                    return sDescription;
            }
            return "";
        }

        internal static String getRunwayElementDescriptionByValueAndTerminalNb(DataRow drRow,
            int elementValue, XmlNode racine, int terminalNb, bool bUseAlpha)
        {
            XmlNode xnGroup = GestionDonneesHUB2SIM.getRunways(racine);
            if (xnGroup == null)
                return "";
            for (int j = 0; j < xnGroup.ChildNodes.Count; j++)
            {
                String sDescription = GestionDonneesHUB2SIM.extractDescription(xnGroup.ChildNodes[j].Attributes["Name"].Value.ToString());
                if (!bUseAlpha && (elementValue.ToString() == sDescription.ToLower().Trim()))
                    return xnGroup.ChildNodes[j].Attributes["Index"].Value.ToString();
                else if ((bUseAlpha) && (elementValue.ToString() == xnGroup.ChildNodes[j].Attributes["Index"].Value))
                    return sDescription;
            }
            return "";
        }

        /**
         * Converts the given list of flights 
         * into an xml representation.
         */
        internal static String serializeForXml(List<Task> flightsList)
        {
            if (flightsList != null)
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(flightsList.GetType());
                using (StringWriter writer = new StringWriter())
                {
                    try
                    {
                        serializer.Serialize(writer, flightsList);
                        return writer.GetStringBuilder().ToString();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.InnerException.Message + "\n" + ex.InnerException.StackTrace);
                    }
                    return "";
                }
            }
            else
            {
                MessageBox.Show("No Data to display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }
        }

        /**
         * The function goes through the Check-In (Opening) table and retrievs for
         * every check-in the intervals of time in which the check-in is opened
         * The data is stored in a dictionary having the key = the number of the check-in
         * and the value = a task representing the check-in(with the opening intervals)
         */
        public static void getOpeningIntervalsForCheckIns()
        {
            allocationOpeningCI.Clear();
            DataTable ciAlloc = pax2simInst.DonneesEnCours.getTable("Input", GlobalNames.Opening_CITableName);
            if (ciAlloc.Rows.Count > 0)
            {
                int indexColumnTime = ciAlloc.Columns.IndexOf(GlobalNames.sColumnTime);
                DateTime startDate = new DateTime();
                DateTime startDateInterval = new DateTime();
                DateTime endDateInterval = new DateTime();
                int dateStep = 0;
                int stepsNb = 0;
                int checkInNb = 0;

                foreach (DataColumn dc in ciAlloc.Columns)
                {
                    Task checkIn = new Task();
                    checkIn.name = "Check-In " + (ciAlloc.Columns.IndexOf(dc)).ToString();

                    bool gotStartDate = false;
                    bool gotDateStep = false;
                    bool opened = false;

                    foreach (DataRow dr in ciAlloc.Rows)
                    {
                        if (!dc.ColumnName.Equals("Time"))
                        {
                            #region checkIn data
                            bool isOpen = FonctionsType.getBoolean(dr[dc]);
                            if (isOpen)
                            {
                                opened = true;
                                stepsNb++;
                            }
                            else
                            {
                                if (opened)
                                {
                                    opened = false;
                                    int currentRowIndex = ciAlloc.Rows.IndexOf(dr);
                                    int startRowIndex = currentRowIndex - stepsNb;
                                    startDateInterval = startDate.AddMinutes((startRowIndex) * dateStep);
                                    endDateInterval = startDateInterval.AddMinutes(stepsNb * dateStep);
                                    stepsNb = 0;

                                    Segment openInterval = new Segment();
                                    openInterval.start = startDateInterval;
                                    openInterval.end = endDateInterval;
                                    checkIn.segments.Add(openInterval);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region get start date and step from table
                            if (gotStartDate && !gotDateStep)
                            {
                                gotDateStep = true;
                                DateTime secondDate = new DateTime();
                                secondDate = FonctionsType.getDate(dr[dc]);
                                TimeSpan secondDateTime = FonctionsType.getTime(dr[dc].ToString());
                                secondDate.AddMinutes(secondDateTime.Minutes);
                                dateStep = (secondDate.AddMinutes(-startDate.Minute)).Minute;
                            }
                            if (!gotStartDate)
                            {
                                gotStartDate = true;
                                startDate = FonctionsType.getDate(dr[dc]);
                                TimeSpan startDateTime = FonctionsType.getTime(dr[dc].ToString());
                                startDate.AddMinutes(startDateTime.Minutes);
                            }
                            #endregion
                        }
                    }
                    if (!dc.ColumnName.Equals("Time") && (checkIn.segments.Count > 0))
                    {
                        checkInNb = ciAlloc.Columns.IndexOf(dc);
                        allocationOpeningCI.Add(checkInNb, checkIn);
                    }
                }
            }
        }

        /**
         * Function used to store for the current resource the number of desks on each terminal
         * Returns the dictionary and the first/last desk number for the Runway resource(which doesn't use terminals)
         */
        private static void getTerminalsAndDesksForResource(PAX2SIM pax2sim, String resourceType, out Dictionary<int, int[]> desksByTerminalNb,
                                                            out int firstDeskFromAirportStructure, out int lastDeskFromAirportStructure)
        {
            String[] desks = null;
            desksByTerminalNb = new Dictionary<int, int[]>();
            String[] terminalsFromAirportStructure = pax2sim.DonneesEnCours.getTerminal();
            firstDeskFromAirportStructure = 0;
            lastDeskFromAirportStructure = 0;

            if (terminalsFromAirportStructure != null)
            {
                foreach (String terminal in terminalsFromAirportStructure)
                {
                    int airportTerminal = 0;
                    firstDeskFromAirportStructure = 0;
                    lastDeskFromAirportStructure = 0;
                    if (!Int32.TryParse(terminal.Substring(0, 1), out airportTerminal))
                        MessageBox.Show("Error parsing the terminal " + terminal + ". Please contact support.");

                    #region get desks by resource type
                    if (resourceType.Equals(Model.ResourceTypes.BaggageClaim.ToString()))
                        desks = pax2sim.DonneesEnCours.getBaggageClaimBelt(terminal);
                    if (resourceType.Equals(Model.ResourceTypes.TransferInfeed.ToString()))
                        desks = pax2sim.DonneesEnCours.getTransferInfeed(terminal);
                    if (resourceType.Equals(Model.ResourceTypes.ArrivalInfeed.ToString()))
                        desks = pax2sim.DonneesEnCours.getArrivalInfeed(terminal);
                    if (resourceType.Equals(Model.ResourceTypes.ArrivalGate.ToString()))
                        desks = pax2sim.DonneesEnCours.getArrivalGate(terminal);
                    if (resourceType.Equals(Model.ResourceTypes.BoardingGate.ToString()))
                        desks = pax2sim.DonneesEnCours.getBoardingGate(terminal);
                    if (resourceType.Equals(Model.ResourceTypes.ParkingStand.ToString()))
                        desks = pax2sim.DonneesEnCours.getParking(terminal);
                    if (resourceType.Equals(Model.ResourceTypes.Runway.ToString()))
                        desks = pax2sim.DonneesEnCours.getRunWay();
                    if (resourceType.Equals(Model.ResourceTypes.EcoCheck_In.ToString()) || resourceType.Equals(Model.ResourceTypes.FBCheck_In.ToString())
                        || resourceType.Equals(Model.ResourceTypes.EcoBagDrop.ToString()) || resourceType.Equals(Model.ResourceTypes.FBBagDrop.ToString()))
                        desks = pax2sim.DonneesEnCours.getCheckIn(terminal);
                    if (resourceType.Equals(Model.ResourceTypes.EcoMakeUp.ToString()) || resourceType.Equals(Model.ResourceTypes.FBMakeUp.ToString()))
                        desks = pax2sim.DonneesEnCours.getMakeUp(terminal);
                    #endregion

                    if (desks != null && desks.Length > 0)
                    {
                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                        {
                            int start = desks[0].IndexOf("(") + 1;
                            int end = desks[0].IndexOf(":");
                            Int32.TryParse(desks[0].Substring(start, end - start), out firstDeskFromAirportStructure);

                            if (resourceType.Equals(Model.ResourceTypes.EcoCheck_In.ToString())
                                || resourceType.Equals(Model.ResourceTypes.FBCheck_In.ToString())
                                || resourceType.Equals(Model.ResourceTypes.EcoBagDrop.ToString())
                                || resourceType.Equals(Model.ResourceTypes.FBBagDrop.ToString()))
                            {
                                int i = 0;
                                while (i < desks.Length) //&& desks[i].Contains("Check"))
                                {
                                    start = desks[0].IndexOf("(") + 1;
                                    end = desks[i].IndexOf(":");
                                    Int32.TryParse(desks[i].Substring(start, end - start), out lastDeskFromAirportStructure);
                                    i++;
                                }
                            }
                            else
                            {
                                start = desks[desks.Length - 1].IndexOf("(") + 1;
                                end = desks[desks.Length - 1].IndexOf(":");
                                Int32.TryParse(desks[desks.Length - 1].Substring(start, end - start), out lastDeskFromAirportStructure);
                            }
                        }
                        else
                        {
                            int end = desks[0].IndexOf("(");
                            Int32.TryParse(desks[0].Substring(0, end), out firstDeskFromAirportStructure);

                            if (resourceType.Equals(Model.ResourceTypes.EcoCheck_In.ToString())
                                || resourceType.Equals(Model.ResourceTypes.FBCheck_In.ToString())
                                || resourceType.Equals(Model.ResourceTypes.EcoBagDrop.ToString())
                                || resourceType.Equals(Model.ResourceTypes.FBBagDrop.ToString()))
                            {
                                int i = 0;
                                while (i < desks.Length) //&& desks[i].Contains("Check"))
                                {
                                    end = desks[i].IndexOf("(");
                                    Int32.TryParse(desks[i].Substring(0, end), out lastDeskFromAirportStructure);
                                    i++;
                                }
                            }
                            else
                            {
                                end = desks[desks.Length - 1].IndexOf("(");
                                Int32.TryParse(desks[desks.Length - 1].Substring(0, end), out lastDeskFromAirportStructure);
                            }
                        }
                        desksByTerminalNb.Add(airportTerminal, new int[] { firstDeskFromAirportStructure, lastDeskFromAirportStructure });
                        //because Runway doesn't use terminals it has only one set of values for the first/last desk in the dictionary
                        //therefore in the dictionary we insert only one entry for a fake key(the first terminal is used as fake key)
                        if (resourceType.Equals(Model.ResourceTypes.Runway.ToString()))
                            break;
                    }
                }
            }
            else
            {
                //MessageBox.Show("Please add the Terminal object(s) to the Airport structure.",
                //    "Airport Structure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // << Bug #13367 Liege allocation
        }

        internal static List<Task> getFlightDataForResourceGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim, Model.ResourceTypes resourceType,
                                                                 int indexColumnTerminalForResource,
                                                                 int indexColumnResourceStart, int indexColumnResourceEnd,
                                                                 NormalTable octTable, String lineOpening, String lineClosing)
        {
            return getFlightDataForResourceGantt(dt, isArrival, pax2sim, resourceType, indexColumnTerminalForResource,
                                                 indexColumnResourceStart, indexColumnResourceEnd, octTable,
                                                 lineOpening, lineClosing, true, "Input");
        }

        internal static List<Task> getFlightDataForResourceGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim, Model.ResourceTypes resourceType,
                                                                 int indexColumnTerminalForResource, int indexColumnResourceStart, int indexColumnResourceEnd,
                                                                 NormalTable octTable, String lineOpening, String lineClosing, bool useExceptions,
                                                                 String scenarioName)
        {
            // converted table for Alphanumerical mode
            DataTable numericalTable = new DataTable();
            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
            {
                if (dt.TableName.Equals(GlobalNames.FPATableName))
                    numericalTable = GestionDonneesHUB2SIM.ConvertFPAInformations(dt, pax2sim.DonneesEnCours.getRacine(), false, false);
                else if (dt.TableName.Equals(GlobalNames.FPDTableName))
                    numericalTable = GestionDonneesHUB2SIM.ConvertFPDInformations(dt, pax2sim.DonneesEnCours.getRacine(), false, false);
            }
            List<Task> resourcesList = new List<Task>();
            ArrayList airlineCodes = new ArrayList();
            ArrayList flightCategories = new ArrayList();
            ArrayList groundHandlingCompanies = new ArrayList();
            // Flight details - nb, category, airline
            int indexColumnFlightId = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int indexColumnFlightNb = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnFlightCategory = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int indexColumnFlightAirline = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int iIndexAirlineCodes_ColumnFlightAirlineGroundHandler = -1;
            int iIndexAirlineCodes_ColumnFlightAirline = -1;
            // Flight Categories table - used to set the flight color according to the flight category
            // colorCriteriaTable - Flight Categories or Airline Codes
            DataTable colorCriteriaTable = new DataTable();
            int indexColumnColorCriteria = -1;
            String flightAirlineGroundHandler = "";
            if (pax2sim.colorByFlightCategory && !pax2sim.colorByAirlineCode && !pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_FlightCategoriesTableName);
                indexColumnColorCriteria = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!flightCategories.Contains(line[indexColumnColorCriteria].ToString()))
                            flightCategories.Add(line[indexColumnColorCriteria].ToString());
                    }
                    flightCategories.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }
            else if (!pax2sim.colorByFlightCategory && pax2sim.colorByAirlineCode && !pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                indexColumnColorCriteria = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!airlineCodes.Contains(line[indexColumnColorCriteria].ToString()))
                            airlineCodes.Add(line[indexColumnColorCriteria].ToString());
                    }
                    airlineCodes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }
            else if (!pax2sim.colorByFlightCategory && !pax2sim.colorByAirlineCode && pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                iIndexAirlineCodes_ColumnFlightAirlineGroundHandler = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_GroundHandlers);
                iIndexAirlineCodes_ColumnFlightAirline = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!groundHandlingCompanies.Contains(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString()))
                            groundHandlingCompanies.Add(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString());
                    }
                    groundHandlingCompanies.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }

            // create root task(Occupied resources) which will hold all the used resources
            Task occupiedResources = new Task();
            occupiedResources.name = "Occupied Resources";
            occupiedResources.taskNumber = -2;
            occupiedResources.taskTerminal = -1;
            occupiedResources.arrivalDeparture = "N";
            occupiedResources.type = "usedResources";
            occupiedResources.resourceType = "N";
            // create root task(Free resources) which will hold all the free resources
            Task freeResources = new Task();
            freeResources.name = "Free Resources";
            freeResources.taskNumber = -1;
            freeResources.taskTerminal = -1;
            freeResources.arrivalDeparture = "N";
            freeResources.type = "freeResources";
            freeResources.resourceType = "N";

            int terminals = 0;
            // if the resource doesn't use a terminal (ex.: Runway) 
            // the number of terminals will be set to 1 to allow the calculation once
            if (indexColumnTerminalForResource == -1)
                terminals = 1;
            else
                terminals = OverallTools.DataFunctions.valeurMaximaleDansColonne(dt, indexColumnTerminalForResource);

            #region get the first and last desk from the Airport structure according to the resource type

            String[] terminalsFromAirportStructure = pax2sim.DonneesEnCours.getTerminal();
            int firstDeskFromAirportStructure = 0;
            int lastDeskFromAirportStructure = 0;
            // Key: terminal number / value: vector holding the first and last desk
            // Each terminal has associated the first and last desk from the airport structure
            // for a specific resource
            Dictionary<int, int[]> desksByTerminalNb = new Dictionary<int, int[]>();
            getTerminalsAndDesksForResource(pax2sim, resourceType.ToString(), out desksByTerminalNb,
                                            out firstDeskFromAirportStructure, out lastDeskFromAirportStructure);
            #endregion

            int terminalNb = 0;
            int resourceStartDesk = 0;
            int resourceEndDesk = 0;
            String resourceStartDeskAN = "";
            String resourceEndDeskAN = "";
            int flightId = 0;
            String flightNb = "";
            String flightCategory = "";
            String flightAirline = "";

            DateTime resourceStartTime = new DateTime();
            DateTime resourceEndTime = new DateTime();

            //reset the variables used to hold the min 
            //and max desk number for the current resource
            pax2sim.minDeskNumber = -1;
            pax2sim.maxDeskNumber = -1;
            pax2sim.desksByTerminalNb.Clear();

            bool modified = false;

            if (terminals > 0)
            {
                //for every terminal go through the table and get the resources
                for (int i = 1; i <= terminals; i++)
                {
                    foreach (DataRow drRow in dt.Rows)
                    {
                        if (indexColumnTerminalForResource == -1)
                            terminalNb = 1;
                        else
                            terminalNb = FonctionsType.getInt(drRow[indexColumnTerminalForResource]);
                        flightId = FonctionsType.getInt(drRow[indexColumnFlightId]);
                        flightNb = drRow[indexColumnFlightNb].ToString();
                        flightCategory = drRow[indexColumnFlightCategory].ToString();
                        flightAirline = drRow[indexColumnFlightAirline].ToString();

                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && numericalTable.Rows.Count > 0)
                        {
                            int rowIndex = dt.Rows.IndexOf(drRow);
                            //int.TryParse(drRow[indexColumnFlightId].ToString(), out rowIndex);
                            int.TryParse((numericalTable.Rows[rowIndex][indexColumnResourceStart]).ToString(), out resourceStartDesk);
                            resourceStartDeskAN = drRow[indexColumnResourceStart].ToString();
                        }
                        else
                        {
                            resourceStartDesk = FonctionsType.getInt(drRow[indexColumnResourceStart]);
                        }


                        if (indexColumnResourceEnd == -1)
                            resourceEndDesk = resourceStartDesk;
                        else
                        {
                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && numericalTable.Rows.Count > 0)
                            {
                                int rowIndex = dt.Rows.IndexOf(drRow);
                                //int.TryParse(drRow[indexColumnFlightId].ToString(), out rowIndex);
                                int.TryParse((numericalTable.Rows[rowIndex][indexColumnResourceEnd]).ToString(), out resourceEndDesk);
                                resourceEndDeskAN = drRow[indexColumnResourceEnd].ToString();
                            }
                            else
                            {
                                resourceEndDesk = FonctionsType.getInt(drRow[indexColumnResourceEnd]);
                            }

                        }
                        if (resourceStartDesk == 0 || resourceEndDesk == 0)
                            break;

                        for (int k = resourceStartDesk; k <= resourceEndDesk; k++)
                        {
                            modified = false;
                            if (terminalNb == i)
                            {
                                if (resourcesList != null && resourcesList.Count > 0)
                                    foreach (Task res in resourcesList)
                                    {
                                        if (res.taskNumber == k && res.taskTerminal == terminalNb)
                                        {//the resource was created earlier so we just add the flight
                                            Segment resourceFlight = new Segment();
                                            resourceFlight.id = flightId;
                                            getStartEndDates(drRow, isArrival, resourceType, octTable, lineOpening, lineClosing,
                                                             out resourceStartTime, out resourceEndTime, useExceptions);
                                            resourceFlight.start = resourceStartTime;
                                            resourceFlight.end = resourceEndTime;
                                            // << Bug #7863 Flight Category missing from the Information panel - Resource Gantt                                            
                                            resourceFlight.middleText = flightId + " / " + flightAirline + " / " + flightNb + " / " + flightCategory;
                                            // >> Bug #7863 Flight Category missing from the Information panel - Resource Gantt

                                            #region flight category - color                                            

                                            //int criteriaRowNb = 0;
                                            int colorIndex = 0;
                                            /*
                                            if (indexColumnColorCriteria != -1)
                                            {
                                                foreach (DataRow criteriaTableRow in colorCriteriaTable.Rows)
                                                {
                                                    if (pax2sim.colorByFlightCategory)
                                                    {
                                                        if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightCategory))
                                                            break;
                                                    }
                                                    else if (pax2sim.colorByAirlineCode)
                                                    {
                                                        if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightAirline))
                                                            break;
                                                    }
                                                    criteriaRowNb++;
                                                }
                                                colorIndex = criteriaRowNb + 1;
                                            } 
                                                */
                                            if (pax2sim.colorByFlightCategory)
                                            {
                                                if ((flightCategory == null) || (flightCategory == "") || (flightCategories.Count == 0))
                                                    colorIndex = 0;
                                                else
                                                    colorIndex = flightCategories.IndexOf(flightCategory) + 1;
                                            }
                                            else if (pax2sim.colorByAirlineCode)
                                            {
                                                if ((flightAirline == null) || (flightAirline == "") || (airlineCodes.Count == 0))
                                                    colorIndex = 0;
                                                else
                                                    colorIndex = airlineCodes.IndexOf(flightAirline) + 1;
                                            }
                                            else if (pax2sim.colorByGroundHandlerCode)
                                            {
                                                // for GH coloring the colorIndex will be -1 and will be set in this block
                                                // this is because a list is used to load all the GH codes instead of the 
                                                // pax2sim table( in the table the GH can repead over several lines => it 
                                                // will not generated same color for a GH if we check the row nb)
                                                flightAirlineGroundHandler = OverallTools.DataFunctions
                                                        .getValue(colorCriteriaTable, flightAirline, iIndexAirlineCodes_ColumnFlightAirline,
                                                                  iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                                                if ((flightAirlineGroundHandler == null) || (flightAirlineGroundHandler == "")
                                                    || (groundHandlingCompanies.Count == 0))
                                                    colorIndex = 0;
                                                else
                                                    colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                                            }

                                            // the flight category from FP has been found in the Flight Categories table
                                            //if (criteriaRowNb < colorCriteriaTable.Rows.Count || pax2sim.colorByGroundHandlerCode)
                                            resourceFlight.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());

                                            #endregion

                                            if (isArrival)
                                                resourceFlight.arrivalDeparture = "A";
                                            else
                                                resourceFlight.arrivalDeparture = "D";
                                            // set the first and last desk occupied for range resources(Arrival Infeeds)
                                            // and the desk occupied for non-range resources
                                            // For Allocation scenarios all resources are considered ranged 
                                            // => use the FPI Gantt(the data used for the Allocation must remain the same)
                                            if (scenarioName.Equals("Input"))
                                            {
                                                if (res.resourceType == Model.RANGED_RESOURCES)
                                                {
                                                    resourceFlight.startDesk = resourceStartDesk;
                                                    resourceFlight.endDesk = resourceEndDesk;
                                                    if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                    {
                                                        resourceFlight.startDeskAN = resourceStartDeskAN;
                                                        resourceFlight.endDeskAN = resourceEndDeskAN;
                                                    }
                                                }
                                                else if (res.resourceType == Model.NON_RANGED_RESOURCES)
                                                {
                                                    resourceFlight.startDesk = resourceStartDesk;
                                                    if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                        resourceFlight.startDeskAN = resourceStartDeskAN;
                                                }

                                            }
                                            else
                                            {
                                                resourceFlight.startDesk = resourceStartDesk;
                                                resourceFlight.endDesk = resourceEndDesk;
                                                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                {
                                                    resourceFlight.startDeskAN = resourceStartDeskAN;
                                                    resourceFlight.endDeskAN = resourceEndDeskAN;
                                                }
                                            }
                                            // add the flight only if the its flight category has been found in the Flight categories table
                                            //if (criteriaRowNb < colorCriteriaTable.Rows.Count)
                                            if (resourceFlight.start < resourceFlight.end)
                                                res.segments.Add(resourceFlight);
                                            modified = true;
                                            break;
                                        }
                                    }
                                if (!modified)
                                {
                                    //the resource hasn't been created yet
                                    Task resource = new Task();
                                    resource.type = resourceType.ToString();
                                    // Runway resource doesn't have a terminal                                    
                                    String taskNameForAlphaNumericalMode = "";
                                    String resourceGroup = "";
                                    if (resourceType.Equals(Model.ResourceTypes.ArrivalGate))
                                        resourceGroup = Model.ARRIVAL_GATE_GROUP;
                                    if (resourceType.Equals(Model.ResourceTypes.BaggageClaim))
                                        resourceGroup = Model.BAGG_CLAIM_GROUP;
                                    if (resourceType.Equals(Model.ResourceTypes.ArrivalInfeed))
                                        resourceGroup = Model.ARRIVAL_INFEED_GROUP;
                                    if (resourceType.Equals(Model.ResourceTypes.TransferInfeed))
                                        resourceGroup = Model.TRANSFER_INFEED_GROUP;
                                    if (resourceType.Equals(Model.ResourceTypes.BoardingGate))
                                        resourceGroup = Model.BOARDING_GATE_GROUP;
                                    if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                    {
                                        if (resourceType == Model.ResourceTypes.ParkingStand)
                                        {
                                            taskNameForAlphaNumericalMode = getParkingElementDescriptionByValueAndTerminalNb(drRow,
                                                                                k, pax2sim.DonneesEnCours.getRacine(), terminalNb,
                                                                                pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo);
                                        }
                                        else if (resourceType == Model.ResourceTypes.Runway)
                                        {
                                            taskNameForAlphaNumericalMode = getRunwayElementDescriptionByValueAndTerminalNb(drRow,
                                                                                 k, pax2sim.DonneesEnCours.getRacine(), terminalNb,
                                                                                 pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo);
                                        }
                                        else
                                        {

                                            taskNameForAlphaNumericalMode = getElementDescriptionByValueAndTypeAndTerminalNb(drRow,
                                                                             k, pax2sim.DonneesEnCours.getRacine(), terminalNb,
                                                                             resourceGroup);
                                        }
                                        if (indexColumnTerminalForResource == -1)
                                            resource.name = resourceType + " " + taskNameForAlphaNumericalMode;
                                        else
                                            resource.name = resourceType + " " + taskNameForAlphaNumericalMode + " / T " + terminalNb;
                                    }
                                    else
                                    {
                                        if (indexColumnTerminalForResource == -1)
                                            resource.name = resourceType + " " + k;
                                        else
                                            resource.name = resourceType + " " + k + " / T " + terminalNb;
                                    }
                                    if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                        resource.alphaNumericalName = taskNameForAlphaNumericalMode;
                                    else
                                        resource.alphaNumericalName = "";
                                    resource.taskNumber = k;
                                    resource.taskTerminal = terminalNb;
                                    if (isArrival)
                                        resource.arrivalDeparture = "A";
                                    else
                                        resource.arrivalDeparture = "D";
                                    if (indexColumnResourceEnd == -1)
                                        resource.resourceType = Model.NON_RANGED_RESOURCES;
                                    else
                                        resource.resourceType = Model.RANGED_RESOURCES;

                                    Segment resourceFlight = new Segment();
                                    resourceFlight.id = flightId;
                                    getStartEndDates(drRow, isArrival, resourceType, octTable, lineOpening, lineClosing,
                                                     out resourceStartTime, out resourceEndTime, useExceptions);
                                    resourceFlight.start = resourceStartTime;
                                    resourceFlight.end = resourceEndTime;
                                    // << Bug #7863 Flight Category missing from the Information panel - Resource Gantt                                    
                                    resourceFlight.middleText = flightId + " / " + flightAirline + " / " + flightNb + " / " + flightCategory;
                                    // >> Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                    #region flight category - color

                                    //int criteriaRowNb = 0;
                                    int colorIndex = 0;
                                    /*
                                    if (indexColumnColorCriteria != -1)
                                    {
                                        foreach (DataRow criteriaTableRow in colorCriteriaTable.Rows)
                                        {
                                            if (pax2sim.colorByFlightCategory)
                                            {
                                                if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightCategory))
                                                    break;
                                            }
                                            else if (pax2sim.colorByAirlineCode)
                                            {
                                                if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightAirline))
                                                    break;
                                            }
                                            criteriaRowNb++;
                                        }
                                        colorIndex = criteriaRowNb + 1;
                                    }*/
                                    if (pax2sim.colorByFlightCategory)
                                    {
                                        if ((flightCategory == null) || (flightCategory == "") || (flightCategories.Count == 0))
                                            colorIndex = 0;
                                        else
                                            colorIndex = flightCategories.IndexOf(flightCategory) + 1;
                                    }
                                    else if (pax2sim.colorByAirlineCode)
                                    {
                                        if ((flightAirline == null) || (flightAirline == "") || (airlineCodes.Count == 0))
                                            colorIndex = 0;
                                        else
                                            colorIndex = airlineCodes.IndexOf(flightAirline) + 1;
                                    }
                                    else if (pax2sim.colorByGroundHandlerCode)
                                    {
                                        // for GH coloring the colorIndex will be -1 and will be set in this block
                                        // this is because a list is used to load all the GH codes instead of the 
                                        // pax2sim table( in the table the GH can repead over several lines => it 
                                        // will not generated same color for a GH if we check the row nb)
                                        flightAirlineGroundHandler = OverallTools.DataFunctions
                                                .getValue(colorCriteriaTable, flightAirline, iIndexAirlineCodes_ColumnFlightAirline,
                                                          iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                                        if ((flightAirlineGroundHandler == null) || (flightAirlineGroundHandler == "")
                                            || (groundHandlingCompanies.Count == 0))
                                            colorIndex = 0;
                                        else
                                            colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                                    }

                                    // the flight category from FP has been found in the Flight Categories table
                                    //if (criteriaRowNb < colorCriteriaTable.Rows.Count || pax2sim.colorByGroundHandlerCode)
                                    resourceFlight.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());

                                    #endregion
                                    if (isArrival)
                                        resourceFlight.arrivalDeparture = "A";
                                    else
                                        resourceFlight.arrivalDeparture = "D";
                                    // set the first and last desk occupied for range resources(Arrival Infeeds)
                                    // and the desk occupied for non-range resources
                                    // For Allocation scenarios all resources are considered ranged 
                                    // => use the FPI Gantt(the data used for the Allocation must remain the same)
                                    if (scenarioName.Equals("Input"))
                                    {
                                        if (resource.resourceType == Model.RANGED_RESOURCES)
                                        {
                                            resourceFlight.startDesk = resourceStartDesk;
                                            resourceFlight.endDesk = resourceEndDesk;
                                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                            {
                                                resourceFlight.startDeskAN = resourceStartDeskAN;
                                                resourceFlight.endDeskAN = resourceEndDeskAN;
                                            }
                                        }
                                        else if (resource.resourceType == Model.NON_RANGED_RESOURCES)
                                        {
                                            resourceFlight.startDesk = resourceStartDesk;
                                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                resourceFlight.startDeskAN = resourceStartDeskAN;
                                        }
                                    }
                                    else
                                    {
                                        resourceFlight.startDesk = resourceStartDesk;
                                        resourceFlight.endDesk = resourceEndDesk;
                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                        {
                                            resourceFlight.startDeskAN = resourceStartDeskAN;
                                            resourceFlight.endDeskAN = resourceEndDeskAN;
                                        }
                                    }
                                    // add the flight only if the its flight category has been found in the Flight categories table
                                    //if (criteriaRowNb < colorCriteriaTable.Rows.Count)
                                    if (resourceFlight.start < resourceFlight.end)
                                        resource.segments.Add(resourceFlight);
                                    if (resource.segments.Count > 0)
                                        resourcesList.Add(resource);
                                }
                            }

                        }
                    }
                }
            }

            resourcesList.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));
            foreach (Task res in resourcesList)
            {
                occupiedResources.subTasks.Add(res);
            }
            occupiedResources.subTasks.Sort(new Comparison<Task>((x, y) =>
            {
                int result = x.taskTerminal.CompareTo(y.taskTerminal);
                return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
            }));
            resourcesList.Clear();
            resourcesList.Add(occupiedResources);

            foreach (KeyValuePair<int, int[]> pair in desksByTerminalNb)
            {
                firstDeskFromAirportStructure = pair.Value[0];
                lastDeskFromAirportStructure = pair.Value[1];
                int airportTerminalNb = pair.Key;

                if (firstDeskFromAirportStructure > 0 && lastDeskFromAirportStructure > 0 && firstDeskFromAirportStructure <= lastDeskFromAirportStructure)
                {
                    for (int i = firstDeskFromAirportStructure; i <= lastDeskFromAirportStructure; i++)
                    {
                        bool isUsed = false;
                        foreach (Task usedRes in occupiedResources.subTasks)
                        {
                            if (usedRes.taskNumber == i && usedRes.taskTerminal == airportTerminalNb)
                            {
                                isUsed = true;
                                break;
                            }
                        }
                        if (!isUsed)
                        {
                            #region create free resource
                            Task freeResource = new Task();
                            freeResource.type = resourceType.ToString();

                            String taskNameForAlphaNumericalMode = "";
                            String resourceGroup = "";
                            if (resourceType.Equals(Model.ResourceTypes.ArrivalGate))
                                resourceGroup = Model.ARRIVAL_GATE_GROUP;
                            if (resourceType.Equals(Model.ResourceTypes.BaggageClaim))
                                resourceGroup = Model.BAGG_CLAIM_GROUP;
                            if (resourceType.Equals(Model.ResourceTypes.ArrivalInfeed))
                                resourceGroup = Model.ARRIVAL_INFEED_GROUP;
                            if (resourceType.Equals(Model.ResourceTypes.TransferInfeed))
                                resourceGroup = Model.TRANSFER_INFEED_GROUP;
                            if (resourceType.Equals(Model.ResourceTypes.BoardingGate))
                                resourceGroup = Model.BOARDING_GATE_GROUP;
                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                            {
                                if (resourceType == Model.ResourceTypes.ParkingStand)
                                {
                                    taskNameForAlphaNumericalMode = getParkingElementDescriptionByValueAndTerminalNb(null,
                                                                        i, pax2sim.DonneesEnCours.getRacine(), airportTerminalNb,
                                                                        pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo);
                                }
                                else if (resourceType == Model.ResourceTypes.Runway)
                                {
                                    taskNameForAlphaNumericalMode = getRunwayElementDescriptionByValueAndTerminalNb(null,
                                                                         i, pax2sim.DonneesEnCours.getRacine(), airportTerminalNb,
                                                                         pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo);
                                }
                                else
                                {
                                    taskNameForAlphaNumericalMode = getElementDescriptionByValueAndTypeAndTerminalNb(null,
                                                                     i, pax2sim.DonneesEnCours.getRacine(), airportTerminalNb,
                                                                     resourceGroup);
                                }
                                if (indexColumnTerminalForResource == -1)
                                    freeResource.name = resourceType + " " + taskNameForAlphaNumericalMode;
                                else
                                    freeResource.name = resourceType + " " + taskNameForAlphaNumericalMode + " / T " + airportTerminalNb;
                            }
                            else
                            {
                                if (indexColumnTerminalForResource == -1)
                                    freeResource.name = resourceType + " " + i;
                                else
                                    freeResource.name = resourceType + " " + i + " / T " + airportTerminalNb;
                            }
                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                freeResource.alphaNumericalName = taskNameForAlphaNumericalMode;
                            else
                                freeResource.alphaNumericalName = "";
                            freeResource.taskNumber = i;
                            freeResource.taskTerminal = airportTerminalNb;
                            if (isArrival)
                                freeResource.arrivalDeparture = "A";
                            else
                                freeResource.arrivalDeparture = "D";
                            if (indexColumnResourceEnd == -1)
                                freeResource.resourceType = Model.NON_RANGED_RESOURCES;
                            else
                                freeResource.resourceType = Model.RANGED_RESOURCES;
                            #endregion
                            // if there is a gap in the desk numbering from the airport structure
                            // the desks from the gap are not included(because they don't exist)
                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                            {
                                if (taskNameForAlphaNumericalMode.Length > 0)
                                    freeResources.subTasks.Add(freeResource);
                            }
                            else
                                freeResources.subTasks.Add(freeResource);
                        }
                    }
                }
            }
            freeResources.subTasks.Sort(new Comparison<Task>((x, y) =>
            {
                int result = x.taskTerminal.CompareTo(y.taskTerminal);
                return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
            }));

            resourcesList.Add(freeResources);
            //set the min/max desk number from the airport structure for each terminal
            //the dictionary used for all the resources except the Runway which needs 
            //only the first and last desk
            pax2sim.desksByTerminalNb = desksByTerminalNb;
            pax2sim.minDeskNumber = firstDeskFromAirportStructure;
            pax2sim.maxDeskNumber = lastDeskFromAirportStructure;

            resourcesList.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));

            return resourcesList;
        }
        //for FB+Eco resources
        internal static List<Task> getFlightDataForFBEcoResourceGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim, Model.ResourceTypes resourceType,
                                                                 int indexColumnTerminalForResource,
                                                                 int indexColumnEcoResourceStart, int indexColumnEcoResourceEnd,
                                                                 int indexColumnFBResourceStart, int indexColumnFBResourceEnd,
                                                                 NormalTable octTable, String lineOpening, String lineClosing)
        {
            return getFlightDataForFBEcoResourceGantt(dt, isArrival, pax2sim, resourceType, indexColumnTerminalForResource,
                                                      indexColumnEcoResourceStart, indexColumnEcoResourceEnd,
                                                      indexColumnFBResourceStart, indexColumnFBResourceEnd,
                                                      octTable, lineOpening, lineClosing, true, "Input");
        }

        internal static List<Task> getFlightDataForFBEcoResourceGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim, Model.ResourceTypes resourceType,
                                                                 int indexColumnTerminalForResource,
                                                                 int indexColumnEcoResourceStart, int indexColumnEcoResourceEnd,
                                                                 int indexColumnFBResourceStart, int indexColumnFBResourceEnd,
                                                                 NormalTable octTable, String lineOpening, String lineClosing,
                                                                 bool useExceptions, String scenarioName)
        {
            // converted table for Alphanumerical mode
            DataTable numericalTable = new DataTable();
            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
            {
                numericalTable = GestionDonneesHUB2SIM.ConvertFPDInformations(dt, pax2sim.DonneesEnCours.getRacine(), false, false);
            }
            List<Task> resourcesList = new List<Task>();
            ArrayList airlineCodes = new ArrayList();
            ArrayList flightCategories = new ArrayList();
            ArrayList groundHandlingCompanies = new ArrayList();
            // Flight details - nb, category, airline
            int indexColumnFlightId = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int indexColumnFlightNb = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnFlightCategory = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int indexColumnFlightAirline = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int iIndexAirlineCodes_ColumnFlightAirlineGroundHandler = -1;
            int iIndexAirlineCodes_ColumnFlightAirline = -1;
            // Flight Categories table - used to set the flight color according to the flight category
            // colorCriteriaTable - Flight Categories or Airline Codes
            DataTable colorCriteriaTable = new DataTable();
            int indexColumnColorCriteria = -1;
            String flightAirlineGroundHandler = "";
            if (pax2sim.colorByFlightCategory && !pax2sim.colorByAirlineCode && !pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_FlightCategoriesTableName);
                indexColumnColorCriteria = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!flightCategories.Contains(line[indexColumnColorCriteria].ToString()))
                            flightCategories.Add(line[indexColumnColorCriteria].ToString());
                    }
                    flightCategories.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }
            else if (!pax2sim.colorByFlightCategory && pax2sim.colorByAirlineCode && !pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                indexColumnColorCriteria = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!airlineCodes.Contains(line[indexColumnColorCriteria].ToString()))
                            airlineCodes.Add(line[indexColumnColorCriteria].ToString());
                    }
                    airlineCodes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }
            else if (!pax2sim.colorByFlightCategory && !pax2sim.colorByAirlineCode && pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                iIndexAirlineCodes_ColumnFlightAirlineGroundHandler = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_GroundHandlers);
                iIndexAirlineCodes_ColumnFlightAirline = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!groundHandlingCompanies.Contains(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString()))
                            groundHandlingCompanies.Add(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString());
                    }
                    groundHandlingCompanies.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }

            // create root task(Occupied resources) which will hold all the used resources
            Task occupiedResources = new Task();
            occupiedResources.name = "Occupied Resources";
            occupiedResources.taskNumber = -2;
            occupiedResources.taskTerminal = -1;
            occupiedResources.arrivalDeparture = "N";
            occupiedResources.type = "usedResources";
            occupiedResources.resourceType = "N";
            // create root task(Free resources) which will hold all the free resources
            Task freeResources = new Task();
            freeResources.name = "Free Resources";
            freeResources.taskNumber = -1;
            freeResources.taskTerminal = -1;
            freeResources.arrivalDeparture = "N";
            freeResources.type = "freeResources";
            freeResources.resourceType = "N";

            int terminals = OverallTools.DataFunctions.valeurMaximaleDansColonne(dt, indexColumnTerminalForResource);

            #region get the first and last desk according to the resource type
            String[] terminalsFromAirportStructure = pax2sim.DonneesEnCours.getTerminal();
            int firstDeskFromAirportStructure = 0;
            int lastDeskFromAirportStructure = 0;
            // Key: terminal number / value: vector holding the first and last desk
            // Each terminal has associated the first and last desk from the airport structure
            // for a specific resource
            Dictionary<int, int[]> desksByTerminalNb = new Dictionary<int, int[]>();
            getTerminalsAndDesksForResource(pax2sim, resourceType.ToString(), out desksByTerminalNb,
                                            out firstDeskFromAirportStructure, out lastDeskFromAirportStructure);
            #endregion

            int terminalNb = 0;
            int resourceEcoStartDesk = 0;
            int resourceEcoEndDesk = 0;
            int resourceFBStartDesk = 0;
            int resourceFBEndDesk = 0;
            String resourceEcoStartDeskAN = "";
            String resourceEcoEndDeskAN = "";
            String resourceFBStartDeskAN = "";
            String resourceFBEndDeskAN = "";
            int flightId = 0;
            String flightNb = "";
            String flightCategory = "";
            String flightAirline = "";

            DateTime resourceStartTime = new DateTime();
            DateTime resourceEndTime = new DateTime();

            //reset the variables used to hold the min 
            //and max desk number for the current resource
            pax2sim.minDeskNumber = -1;
            pax2sim.maxDeskNumber = -1;
            pax2sim.desksByTerminalNb.Clear();

            bool modified = false;

            if (terminals > 0)
            {
                //for every terminal go through the table and get the resources
                for (int i = 1; i <= terminals; i++)
                {
                    foreach (DataRow drRow in dt.Rows)
                    {
                        terminalNb = FonctionsType.getInt(drRow[indexColumnTerminalForResource]);
                        flightId = FonctionsType.getInt(drRow[indexColumnFlightId]);
                        flightNb = drRow[indexColumnFlightNb].ToString();
                        flightCategory = drRow[indexColumnFlightCategory].ToString();
                        flightAirline = drRow[indexColumnFlightAirline].ToString();

                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo && numericalTable.Rows.Count > 0)
                        {
                            int rowIndex = dt.Rows.IndexOf(drRow);
                            //int.TryParse(drRow[indexColumnFlightId].ToString(), out rowIndex);
                            int.TryParse((numericalTable.Rows[rowIndex][indexColumnEcoResourceStart]).ToString(), out resourceEcoStartDesk);
                            int.TryParse((numericalTable.Rows[rowIndex][indexColumnEcoResourceEnd]).ToString(), out resourceEcoEndDesk);
                            int.TryParse((numericalTable.Rows[rowIndex][indexColumnFBResourceStart]).ToString(), out resourceFBStartDesk);
                            int.TryParse((numericalTable.Rows[rowIndex][indexColumnFBResourceEnd]).ToString(), out resourceFBEndDesk);

                            resourceEcoStartDeskAN = drRow[indexColumnEcoResourceStart].ToString();
                            resourceEcoEndDeskAN = drRow[indexColumnEcoResourceEnd].ToString();
                            resourceFBStartDeskAN = drRow[indexColumnFBResourceStart].ToString();
                            resourceFBEndDeskAN = drRow[indexColumnFBResourceEnd].ToString();
                        }
                        else
                        {
                            resourceEcoStartDesk = FonctionsType.getInt(drRow[indexColumnEcoResourceStart]);
                            resourceEcoEndDesk = FonctionsType.getInt(drRow[indexColumnEcoResourceEnd]);
                            resourceFBStartDesk = FonctionsType.getInt(drRow[indexColumnFBResourceStart]);
                            resourceFBEndDesk = FonctionsType.getInt(drRow[indexColumnFBResourceEnd]);
                        }

                        if ((resourceEcoStartDesk == 0 || resourceEcoEndDesk == 0) && (resourceFBStartDesk == 0 || resourceFBEndDesk == 0))
                            continue;

                        if (resourceEcoEndDesk < resourceFBStartDesk || resourceEcoStartDesk > resourceFBEndDesk)
                        {// intervals don't intersect
                            int firstCIfirstInt = 0;
                            int lastCIfirstInt = 0;
                            int firstCIlastInt = 0;
                            int lastCIlastInt = 0;
                            if (resourceEcoEndDesk < resourceFBStartDesk)
                            {
                                firstCIfirstInt = resourceEcoStartDesk;
                                lastCIfirstInt = resourceEcoEndDesk;
                                firstCIlastInt = resourceFBStartDesk;
                                lastCIlastInt = resourceFBEndDesk;
                            }
                            if (resourceEcoStartDesk > resourceFBEndDesk)
                            {
                                firstCIfirstInt = resourceFBStartDesk;
                                lastCIfirstInt = resourceFBEndDesk;
                                firstCIlastInt = resourceEcoStartDesk;
                                lastCIlastInt = resourceEcoEndDesk;
                            }
                            int k = firstCIfirstInt;
                            //for (int k = resourceEcoStartDesk; k <= resourceEcoEndDesk; k++)
                            do
                            {
                                #region create task
                                if (k == 0)
                                    k = firstCIlastInt;
                                modified = false;
                                if (terminalNb == i)
                                {
                                    if (resourcesList != null && resourcesList.Count > 0)
                                        foreach (Task res in resourcesList)
                                        {
                                            if (res.taskNumber == k && res.taskTerminal == terminalNb)
                                            {//the resource was created earlier so we just add the flight
                                                Segment resourceFlight = new Segment();
                                                resourceFlight.id = flightId;
                                                getStartEndDates(drRow, isArrival, resourceType, octTable, lineOpening, lineClosing,
                                                                 out resourceStartTime, out resourceEndTime, useExceptions);
                                                resourceFlight.start = resourceStartTime;
                                                resourceFlight.end = resourceEndTime;
                                                String classType = "";
                                                if (k >= resourceEcoStartDesk && k <= resourceEcoEndDesk)
                                                    classType = Model.ECO;
                                                if (k >= resourceFBStartDesk && k <= resourceFBEndDesk)
                                                    classType += Model.FB;
                                                resourceFlight.classType = classType;
                                                // For Allocation scenarios all resources are considered ranged 
                                                // => use the FPI Gantt(the data used for the Allocation must remain the same)
                                                if (scenarioName.Equals("Input"))
                                                {
                                                    resourceFlight.startEco = resourceEcoStartDesk;
                                                    resourceFlight.endEco = resourceEcoEndDesk;
                                                    resourceFlight.startFB = resourceFBStartDesk;
                                                    resourceFlight.endFB = resourceFBEndDesk;
                                                    if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                    {
                                                        resourceFlight.startEcoAN = resourceEcoStartDeskAN;
                                                        resourceFlight.endEcoAN = resourceEcoEndDeskAN;
                                                        resourceFlight.startFBAN = resourceFBStartDeskAN;
                                                        resourceFlight.endFBAN = resourceFBEndDeskAN;
                                                    }
                                                }
                                                else
                                                {
                                                    if (resourceFlight.classType == Model.ECO)
                                                    {
                                                        resourceFlight.startDesk = resourceEcoStartDesk;
                                                        resourceFlight.endDesk = resourceEcoEndDesk;
                                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                        {
                                                            resourceFlight.startDeskAN = resourceEcoStartDeskAN;
                                                            resourceFlight.endDeskAN = resourceEcoEndDeskAN;
                                                        }
                                                    }
                                                    else if (resourceFlight.classType == Model.FB)
                                                    {
                                                        resourceFlight.startDesk = resourceFBStartDesk;
                                                        resourceFlight.endDesk = resourceFBEndDesk;
                                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                        {
                                                            resourceFlight.startDeskAN = resourceFBStartDeskAN;
                                                            resourceFlight.endDeskAN = resourceFBEndDeskAN;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        resourceFlight.startDesk = resourceEcoStartDesk;
                                                        resourceFlight.endDesk = resourceEcoEndDesk;
                                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                        {
                                                            resourceFlight.startDeskAN = resourceEcoStartDeskAN;
                                                            resourceFlight.endDeskAN = resourceEcoEndDeskAN;
                                                        }
                                                    }
                                                }
                                                // << Bug #7863 Flight Category missing from the Information panel - Resource Gantt                                                
                                                resourceFlight.middleText = flightId + " / " + flightAirline + " / " + flightNb + " / " + flightCategory + " / " + classType;
                                                // >> Bug #7863 Flight Category missing from the Information panel - Resource Gantt

                                                #region flight category - color

                                                //int criteriaRowNb = 0;
                                                int colorIndex = 0;
                                                /*
                                                if (indexColumnColorCriteria != -1)
                                                {
                                                    foreach (DataRow criteriaTableRow in colorCriteriaTable.Rows)
                                                    {
                                                        if (pax2sim.colorByFlightCategory)
                                                        {
                                                            if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightCategory))
                                                                break;
                                                        }
                                                        else if (pax2sim.colorByAirlineCode)
                                                        {
                                                            if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightAirline))
                                                                break;
                                                        }
                                                        criteriaRowNb++;
                                                    }
                                                    colorIndex = criteriaRowNb + 1;
                                                }*/
                                                if (pax2sim.colorByFlightCategory)
                                                {
                                                    if ((flightCategory == null) || (flightCategory == "") || (flightCategories.Count == 0))
                                                        colorIndex = 0;
                                                    else
                                                        colorIndex = flightCategories.IndexOf(flightCategory) + 1;
                                                }
                                                else if (pax2sim.colorByAirlineCode)
                                                {
                                                    if ((flightAirline == null) || (flightAirline == "") || (airlineCodes.Count == 0))
                                                        colorIndex = 0;
                                                    else
                                                        colorIndex = airlineCodes.IndexOf(flightAirline) + 1;
                                                }
                                                else if (pax2sim.colorByGroundHandlerCode)
                                                {
                                                    // for GH coloring the colorIndex will be -1 and will be set in this block
                                                    // this is because a list is used to load all the GH codes instead of the 
                                                    // pax2sim table( in the table the GH can repead over several lines => it 
                                                    // will not generated same color for a GH if we check the row nb)
                                                    flightAirlineGroundHandler = OverallTools.DataFunctions
                                                            .getValue(colorCriteriaTable, flightAirline, iIndexAirlineCodes_ColumnFlightAirline,
                                                                      iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                                                    if ((flightAirlineGroundHandler == null) || (flightAirlineGroundHandler == "")
                                                        || (groundHandlingCompanies.Count == 0))
                                                        colorIndex = 0;
                                                    else
                                                        colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                                                }

                                                // the flight category from FP has been found in the Flight Categories table
                                                //if (criteriaRowNb < colorCriteriaTable.Rows.Count || pax2sim.colorByGroundHandlerCode)
                                                resourceFlight.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());

                                                #endregion
                                                if (isArrival)
                                                    resourceFlight.arrivalDeparture = "A";
                                                else
                                                    resourceFlight.arrivalDeparture = "D";
                                                // add the flight only if the its flight category has been found in the Flight categories table
                                                //if (criteriaRowNb < colorCriteriaTable.Rows.Count)
                                                if (resourceFlight.start < resourceFlight.end)
                                                    res.segments.Add(resourceFlight);
                                                modified = true;
                                                break;
                                            }
                                        }
                                    if (!modified)
                                    {
                                        //the resource hasn't been created yet
                                        Task resource = new Task();
                                        resource.type = resourceType.ToString();
                                        String partialName = "";
                                        String taskNameForAlphaNumericalMode = "";
                                        String resourceGroup = "";
                                        if (resourceType.Equals(Model.ResourceTypes.FBCheck_In) || resourceType.Equals(Model.ResourceTypes.EcoCheck_In))
                                        {
                                            if (resourceType.Equals(Model.ResourceTypes.FBCheck_In))
                                                partialName = "Check In";
                                            resourceGroup = PAX2SIM.sCheckInGroup;
                                        }
                                        if (resourceType.Equals(Model.ResourceTypes.FBBagDrop) || resourceType.Equals(Model.ResourceTypes.EcoBagDrop))
                                        {
                                            if (resourceType.Equals(Model.ResourceTypes.FBBagDrop))
                                                partialName = "Baggage Drop";
                                            resourceGroup = PAX2SIM.sCheckInGroup;
                                        }
                                        if (resourceType.Equals(Model.ResourceTypes.FBMakeUp) || resourceType.Equals(Model.ResourceTypes.EcoMakeUp))
                                        {
                                            if (resourceType.Equals(Model.ResourceTypes.FBMakeUp))
                                                partialName = "MakeUp";
                                            resourceGroup = Model.MAKE_UP_GROUP;
                                        }
                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                        {
                                            taskNameForAlphaNumericalMode = getElementDescriptionByValueAndTypeAndTerminalNb(drRow,
                                                                                k, pax2sim.DonneesEnCours.getRacine(), terminalNb,
                                                                                resourceGroup);
                                            resource.name = partialName + " " + taskNameForAlphaNumericalMode + " / T " + terminalNb;//resourceType + " " + k + " / T " + terminalNb;
                                        }
                                        else
                                            resource.name = partialName + " " + k + " / T " + terminalNb;//resourceType + " " + k + " / T " + terminalNb;
                                        resource.taskNumber = k;
                                        resource.taskTerminal = terminalNb;
                                        if (isArrival)
                                            resource.arrivalDeparture = "A";
                                        else
                                            resource.arrivalDeparture = "D";
                                        resource.resourceType = Model.RANGED_RESOURCES;

                                        Segment resourceFlight = new Segment();
                                        resourceFlight.id = flightId;
                                        getStartEndDates(drRow, isArrival, resourceType, octTable, lineOpening, lineClosing,
                                                         out resourceStartTime, out resourceEndTime, useExceptions);
                                        resourceFlight.start = resourceStartTime;
                                        resourceFlight.end = resourceEndTime;
                                        String classType = "";
                                        if (k >= resourceEcoStartDesk && k <= resourceEcoEndDesk)
                                            classType = Model.ECO;
                                        if (k >= resourceFBStartDesk && k <= resourceFBEndDesk)
                                            classType += Model.FB;
                                        resourceFlight.classType = classType;
                                        // For Allocation scenarios all resources are considered ranged 
                                        // => use the FPI Gantt(the data used for the Allocation must remain the same)
                                        if (scenarioName.Equals("Input"))
                                        {
                                            resourceFlight.startEco = resourceEcoStartDesk;
                                            resourceFlight.endEco = resourceEcoEndDesk;
                                            resourceFlight.startFB = resourceFBStartDesk;
                                            resourceFlight.endFB = resourceFBEndDesk;
                                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                            {
                                                resourceFlight.startEcoAN = resourceEcoStartDeskAN;
                                                resourceFlight.endEcoAN = resourceEcoEndDeskAN;
                                                resourceFlight.startFBAN = resourceFBStartDeskAN;
                                                resourceFlight.endFBAN = resourceFBEndDeskAN;
                                            }
                                        }
                                        else
                                        {
                                            if (resourceFlight.classType == Model.ECO)
                                            {
                                                resourceFlight.startDesk = resourceEcoStartDesk;
                                                resourceFlight.endDesk = resourceEcoEndDesk;
                                                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                {
                                                    resourceFlight.startDeskAN = resourceEcoStartDeskAN;
                                                    resourceFlight.endDeskAN = resourceEcoEndDeskAN;
                                                }
                                            }
                                            else if (resourceFlight.classType == Model.FB)
                                            {
                                                resourceFlight.startDesk = resourceFBStartDesk;
                                                resourceFlight.endDesk = resourceFBEndDesk;
                                                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                {
                                                    resourceFlight.startDeskAN = resourceFBStartDeskAN;
                                                    resourceFlight.endDeskAN = resourceFBEndDeskAN;
                                                }
                                            }
                                            else
                                            {
                                                resourceFlight.startDesk = resourceEcoStartDesk;
                                                resourceFlight.endDesk = resourceEcoEndDesk;
                                                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                {
                                                    resourceFlight.startDeskAN = resourceEcoStartDeskAN;
                                                    resourceFlight.endDeskAN = resourceEcoEndDeskAN;
                                                }
                                            }
                                        }
                                        // << Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                        resourceFlight.middleText = flightId + " / " + flightAirline + " / " + flightNb + " / " + flightCategory + " / " + classType;
                                        // >> Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                        #region flight category - color

                                        //int criteriaRowNb = 0;
                                        int colorIndex = 0;
                                        /*
                                        if (indexColumnColorCriteria != -1)
                                        {
                                            foreach (DataRow criteriaTableRow in colorCriteriaTable.Rows)
                                            {
                                                if (pax2sim.colorByFlightCategory)
                                                {
                                                    if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightCategory))
                                                        break;
                                                }
                                                else if (pax2sim.colorByAirlineCode)
                                                {
                                                    if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightAirline))
                                                        break;
                                                }
                                                criteriaRowNb++;
                                            }
                                            colorIndex = criteriaRowNb + 1;
                                        }*/
                                        if (pax2sim.colorByFlightCategory)
                                        {
                                            if ((flightCategory == null) || (flightCategory == "") || (flightCategories.Count == 0))
                                                colorIndex = 0;
                                            else
                                                colorIndex = flightCategories.IndexOf(flightCategory) + 1;
                                        }
                                        else if (pax2sim.colorByAirlineCode)
                                        {
                                            if ((flightAirline == null) || (flightAirline == "") || (airlineCodes.Count == 0))
                                                colorIndex = 0;
                                            else
                                                colorIndex = airlineCodes.IndexOf(flightAirline) + 1;
                                        }
                                        else if (pax2sim.colorByGroundHandlerCode)
                                        {
                                            // for GH coloring the colorIndex will be -1 and will be set in this block
                                            // this is because a list is used to load all the GH codes instead of the 
                                            // pax2sim table( in the table the GH can repead over several lines => it 
                                            // will not generated same color for a GH if we check the row nb)
                                            flightAirlineGroundHandler = OverallTools.DataFunctions
                                                    .getValue(colorCriteriaTable, flightAirline, iIndexAirlineCodes_ColumnFlightAirline,
                                                              iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                                            if ((flightAirlineGroundHandler == null) || (flightAirlineGroundHandler == "")
                                                || (groundHandlingCompanies.Count == 0))
                                                colorIndex = 0;
                                            else
                                                colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                                        }
                                        // the flight category from FP has been found in the Flight Categories table
                                        //if (criteriaRowNb < colorCriteriaTable.Rows.Count || pax2sim.colorByGroundHandlerCode)
                                        resourceFlight.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());

                                        #endregion
                                        if (isArrival)
                                            resourceFlight.arrivalDeparture = "A";
                                        else
                                            resourceFlight.arrivalDeparture = "D";
                                        // add the flight only if the its flight category has been found in the Flight categories table
                                        //if (criteriaRowNb < colorCriteriaTable.Rows.Count)
                                        if (resourceFlight.start < resourceFlight.end)
                                            resource.segments.Add(resourceFlight);
                                        resourcesList.Add(resource);
                                    }
                                }
                                #endregion
                                k = k + 1;
                                if (k > lastCIfirstInt && k <= firstCIlastInt)
                                    k = firstCIlastInt;
                            } while (k <= lastCIlastInt);
                        }
                        else // intervals will intersect
                        {
                            int start = 0;
                            int end = 0;

                            if (resourceEcoStartDesk <= resourceFBStartDesk && resourceEcoEndDesk >= resourceFBStartDesk)
                            {
                                if (resourceEcoEndDesk <= resourceFBEndDesk)
                                {
                                    start = resourceEcoStartDesk;
                                    end = resourceFBEndDesk;
                                }
                                else
                                {
                                    start = resourceEcoStartDesk;
                                    end = resourceEcoEndDesk;
                                }
                            }
                            if (resourceEcoStartDesk > resourceFBStartDesk && resourceEcoStartDesk <= resourceFBEndDesk)
                            {
                                if (resourceEcoEndDesk <= resourceFBEndDesk)
                                {
                                    start = resourceFBStartDesk;
                                    end = resourceFBEndDesk;
                                }
                                else
                                {
                                    start = resourceFBStartDesk;
                                    end = resourceEcoEndDesk;
                                }
                            }

                            if (resourceEcoStartDesk <= resourceFBStartDesk && resourceEcoEndDesk <= resourceFBEndDesk)
                            {
                                start = resourceEcoStartDesk;
                                end = resourceFBEndDesk;
                            }

                            #region create task
                            for (int k = start; k <= end; k++)
                            {
                                modified = false;
                                if (terminalNb == i)
                                {
                                    if (resourcesList != null && resourcesList.Count > 0)
                                        foreach (Task res in resourcesList)
                                        {
                                            if (res.taskNumber == k && res.taskTerminal == terminalNb)
                                            {//the resource was created earlier so we just add the flight
                                                Segment resourceFlight = new Segment();
                                                resourceFlight.id = flightId;
                                                getStartEndDates(drRow, isArrival, resourceType, octTable, lineOpening, lineClosing,
                                                                 out resourceStartTime, out resourceEndTime, useExceptions);
                                                resourceFlight.start = resourceStartTime;
                                                resourceFlight.end = resourceEndTime;
                                                String classType = "";
                                                if (k >= resourceEcoStartDesk && k <= resourceEcoEndDesk)
                                                    classType = Model.ECO;
                                                if (k >= resourceFBStartDesk && k <= resourceFBEndDesk)
                                                    classType += Model.FB;
                                                resourceFlight.classType = classType;
                                                // For Allocation scenarios all resources are considered ranged 
                                                // => use the FPI Gantt(the data used for the Allocation must remain the same)
                                                if (scenarioName.Equals("Input"))
                                                {
                                                    resourceFlight.startEco = resourceEcoStartDesk;
                                                    resourceFlight.endEco = resourceEcoEndDesk;
                                                    resourceFlight.startFB = resourceFBStartDesk;
                                                    resourceFlight.endFB = resourceFBEndDesk;
                                                    if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                    {
                                                        resourceFlight.startEcoAN = resourceEcoStartDeskAN;
                                                        resourceFlight.endEcoAN = resourceEcoEndDeskAN;
                                                        resourceFlight.startFBAN = resourceFBStartDeskAN;
                                                        resourceFlight.endFBAN = resourceFBEndDeskAN;
                                                    }
                                                }
                                                else
                                                {
                                                    if (resourceFlight.classType == Model.ECO)
                                                    {
                                                        resourceFlight.startDesk = resourceEcoStartDesk;
                                                        resourceFlight.endDesk = resourceEcoEndDesk;
                                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                        {
                                                            resourceFlight.startDeskAN = resourceEcoStartDeskAN;
                                                            resourceFlight.endDeskAN = resourceEcoEndDeskAN;
                                                        }
                                                    }
                                                    else if (resourceFlight.classType == Model.FB)
                                                    {
                                                        resourceFlight.startDesk = resourceFBStartDesk;
                                                        resourceFlight.endDesk = resourceFBEndDesk;
                                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                        {
                                                            resourceFlight.startDeskAN = resourceFBStartDeskAN;
                                                            resourceFlight.endDeskAN = resourceFBEndDeskAN;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        resourceFlight.startDesk = resourceEcoStartDesk;
                                                        resourceFlight.endDesk = resourceEcoEndDesk;
                                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                        {
                                                            resourceFlight.startDeskAN = resourceEcoStartDeskAN;
                                                            resourceFlight.endDeskAN = resourceEcoEndDeskAN;
                                                        }
                                                    }
                                                }
                                                // << Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                                resourceFlight.middleText = flightId + " / " + flightAirline + " / " + flightNb + " / " + flightCategory + " / " + classType;
                                                // >> Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                                #region flight category - color

                                                //int criteriaRowNb = 0;
                                                int colorIndex = 0;
                                                /*
                                                if (indexColumnColorCriteria != -1)
                                                {
                                                    foreach (DataRow criteriaTableRow in colorCriteriaTable.Rows)
                                                    {
                                                        if (pax2sim.colorByFlightCategory)
                                                        {
                                                            if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightCategory))
                                                                break;
                                                        }
                                                        else if (pax2sim.colorByAirlineCode)
                                                        {
                                                            if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightAirline))
                                                                break;
                                                        }
                                                        criteriaRowNb++;
                                                    }
                                                    colorIndex = criteriaRowNb + 1;
                                                }*/
                                                if (pax2sim.colorByFlightCategory)
                                                {
                                                    if ((flightCategory == null) || (flightCategory == "") || (flightCategories.Count == 0))
                                                        colorIndex = 0;
                                                    else
                                                        colorIndex = flightCategories.IndexOf(flightCategory) + 1;
                                                }
                                                else if (pax2sim.colorByAirlineCode)
                                                {
                                                    if ((flightAirline == null) || (flightAirline == "") || (airlineCodes.Count == 0))
                                                        colorIndex = 0;
                                                    else
                                                        colorIndex = airlineCodes.IndexOf(flightAirline) + 1;
                                                }
                                                else if (pax2sim.colorByGroundHandlerCode)
                                                {
                                                    // for GH coloring the colorIndex will be -1 and will be set in this block
                                                    // this is because a list is used to load all the GH codes instead of the 
                                                    // pax2sim table( in the table the GH can repead over several lines => it 
                                                    // will not generated same color for a GH if we check the row nb)
                                                    flightAirlineGroundHandler = OverallTools.DataFunctions
                                                            .getValue(colorCriteriaTable, flightAirline, iIndexAirlineCodes_ColumnFlightAirline,
                                                                      iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                                                    if ((flightAirlineGroundHandler == null) || (flightAirlineGroundHandler == "")
                                                        || (groundHandlingCompanies.Count == 0))
                                                        colorIndex = 0;
                                                    else
                                                        colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                                                }
                                                // the flight category from FP has been found in the Flight Categories table
                                                //if (criteriaRowNb < colorCriteriaTable.Rows.Count || pax2sim.colorByGroundHandlerCode)
                                                resourceFlight.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());

                                                #endregion
                                                if (isArrival)
                                                    resourceFlight.arrivalDeparture = "A";
                                                else
                                                    resourceFlight.arrivalDeparture = "D";
                                                // add the flight only if the its flight category has been found in the Flight categories table
                                                //if (criteriaRowNb < colorCriteriaTable.Rows.Count)
                                                if (resourceFlight.start < resourceFlight.end)
                                                    res.segments.Add(resourceFlight);
                                                modified = true;
                                                break;
                                            }
                                        }
                                    if (!modified)
                                    {
                                        //the resource hasn't been created yet
                                        Task resource = new Task();
                                        resource.type = resourceType.ToString();
                                        //resource.name = "Check In" + " " + k + " / T " + terminalNb;//resourceType + " " + k + " / T " + terminalNb;
                                        String partialName = "";
                                        String taskNameForAlphaNumericalMode = "";
                                        String resourceGroup = "";
                                        if (resourceType.Equals(Model.ResourceTypes.FBCheck_In) || resourceType.Equals(Model.ResourceTypes.EcoCheck_In))
                                        {
                                            if (resourceType.Equals(Model.ResourceTypes.FBCheck_In))
                                                partialName = "Check In";
                                            resourceGroup = PAX2SIM.sCheckInGroup;
                                        }
                                        if (resourceType.Equals(Model.ResourceTypes.FBBagDrop) || resourceType.Equals(Model.ResourceTypes.EcoBagDrop))
                                        {
                                            if (resourceType.Equals(Model.ResourceTypes.FBBagDrop))
                                                partialName = "Baggage Drop";
                                            resourceGroup = PAX2SIM.sCheckInGroup;
                                        }
                                        if (resourceType.Equals(Model.ResourceTypes.FBMakeUp) || resourceType.Equals(Model.ResourceTypes.EcoMakeUp))
                                        {
                                            if (resourceType.Equals(Model.ResourceTypes.FBMakeUp))
                                                partialName = "MakeUp";
                                            resourceGroup = Model.MAKE_UP_GROUP;
                                        }
                                        if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                        {
                                            taskNameForAlphaNumericalMode = getElementDescriptionByValueAndTypeAndTerminalNb(drRow,
                                                                                k, pax2sim.DonneesEnCours.getRacine(), terminalNb,
                                                                                resourceGroup);
                                            resource.name = partialName + " " + taskNameForAlphaNumericalMode + " / T " + terminalNb;
                                        }
                                        else
                                            resource.name = partialName + " " + k + " / T " + terminalNb;//resourceType + " " + k + " / T " + terminalNb;                                        
                                        resource.taskNumber = k;
                                        resource.taskTerminal = terminalNb;
                                        if (isArrival)
                                            resource.arrivalDeparture = "A";
                                        else
                                            resource.arrivalDeparture = "D";
                                        resource.resourceType = Model.RANGED_RESOURCES;

                                        Segment resourceFlight = new Segment();
                                        resourceFlight.id = flightId;
                                        getStartEndDates(drRow, isArrival, resourceType, octTable, lineOpening, lineClosing,
                                                         out resourceStartTime, out resourceEndTime, useExceptions);
                                        resourceFlight.start = resourceStartTime;
                                        resourceFlight.end = resourceEndTime;
                                        String classType = "";
                                        if (k >= resourceEcoStartDesk && k <= resourceEcoEndDesk)
                                            classType = Model.ECO;
                                        if (k >= resourceFBStartDesk && k <= resourceFBEndDesk)
                                            classType += Model.FB;
                                        resourceFlight.classType = classType;
                                        // For Allocation scenarios all resources are considered ranged 
                                        // => use the FPI Gantt(the data used for the Allocation must remain the same)
                                        if (scenarioName.Equals("Input"))
                                        {
                                            resourceFlight.startEco = resourceEcoStartDesk;
                                            resourceFlight.endEco = resourceEcoEndDesk;
                                            resourceFlight.startFB = resourceFBStartDesk;
                                            resourceFlight.endFB = resourceFBEndDesk;
                                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                            {
                                                resourceFlight.startEcoAN = resourceEcoStartDeskAN;
                                                resourceFlight.endEcoAN = resourceEcoEndDeskAN;
                                                resourceFlight.startFBAN = resourceFBStartDeskAN;
                                                resourceFlight.endFBAN = resourceFBEndDeskAN;
                                            }
                                        }
                                        else
                                        {
                                            if (resourceFlight.classType == Model.ECO)
                                            {
                                                resourceFlight.startDesk = resourceEcoStartDesk;
                                                resourceFlight.endDesk = resourceEcoEndDesk;
                                                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                {
                                                    resourceFlight.startDeskAN = resourceEcoStartDeskAN;
                                                    resourceFlight.endDeskAN = resourceEcoEndDeskAN;
                                                }
                                            }
                                            else if (resourceFlight.classType == Model.FB)
                                            {
                                                resourceFlight.startDesk = resourceFBStartDesk;
                                                resourceFlight.endDesk = resourceFBEndDesk;
                                                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                {
                                                    resourceFlight.startDeskAN = resourceFBStartDeskAN;
                                                    resourceFlight.endDeskAN = resourceFBEndDeskAN;
                                                }
                                            }
                                            else
                                            {
                                                resourceFlight.startDesk = resourceEcoStartDesk;
                                                resourceFlight.endDesk = resourceEcoEndDesk;
                                                if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                                                {
                                                    resourceFlight.startDeskAN = resourceEcoStartDeskAN;
                                                    resourceFlight.endDeskAN = resourceEcoEndDeskAN;
                                                }
                                            }
                                        }
                                        // << Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                        resourceFlight.middleText = flightId + " / " + flightAirline + " / " + flightNb + " / " + flightCategory + " / " + classType;
                                        // >> Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                        #region flight category - color

                                        //int criteriaRowNb = 0;
                                        int colorIndex = 0;
                                        /*
                                        if (indexColumnColorCriteria != -1)
                                        {
                                            foreach (DataRow criteriaTableRow in colorCriteriaTable.Rows)
                                            {
                                                if (pax2sim.colorByFlightCategory)
                                                {
                                                    if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightCategory))
                                                        break;
                                                }
                                                else if (pax2sim.colorByAirlineCode)
                                                {
                                                    if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightAirline))
                                                        break;
                                                }
                                                criteriaRowNb++;
                                            }
                                            colorIndex = criteriaRowNb + 1;
                                        }*/
                                        if (pax2sim.colorByFlightCategory)
                                        {
                                            if ((flightCategory == null) || (flightCategory == "") || (flightCategories.Count == 0))
                                                colorIndex = 0;
                                            else
                                                colorIndex = flightCategories.IndexOf(flightCategory) + 1;
                                        }
                                        else if (pax2sim.colorByAirlineCode)
                                        {
                                            if ((flightAirline == null) || (flightAirline == "") || (airlineCodes.Count == 0))
                                                colorIndex = 0;
                                            else
                                                colorIndex = airlineCodes.IndexOf(flightAirline) + 1;
                                        }
                                        else if (pax2sim.colorByGroundHandlerCode)
                                        {
                                            // for GH coloring the colorIndex will be -1 and will be set in this block
                                            // this is because a list is used to load all the GH codes instead of the 
                                            // pax2sim table( in the table the GH can repead over several lines => it 
                                            // will not generated same color for a GH if we check the row nb)
                                            flightAirlineGroundHandler = OverallTools.DataFunctions
                                                    .getValue(colorCriteriaTable, flightAirline, iIndexAirlineCodes_ColumnFlightAirline,
                                                              iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                                            if ((flightAirlineGroundHandler == null) || (flightAirlineGroundHandler == "")
                                                || (groundHandlingCompanies.Count == 0))
                                                colorIndex = 0;
                                            else
                                                colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                                        }
                                        // the flight category from FP has been found in the Flight Categories table
                                        //if (criteriaRowNb < colorCriteriaTable.Rows.Count || pax2sim.colorByGroundHandlerCode)
                                        resourceFlight.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());

                                        #endregion
                                        if (isArrival)
                                            resourceFlight.arrivalDeparture = "A";
                                        else
                                            resourceFlight.arrivalDeparture = "D";
                                        // add the flight only if the its flight category has been found in the Flight categories table
                                        //if (criteriaRowNb < colorCriteriaTable.Rows.Count)
                                        if (resourceFlight.start < resourceFlight.end)
                                            resource.segments.Add(resourceFlight);
                                        if (resource.segments.Count > 0)
                                            resourcesList.Add(resource);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }

            resourcesList.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));
            foreach (Task res in resourcesList)
            {
                occupiedResources.subTasks.Add(res);
            }

            occupiedResources.subTasks.Sort(new Comparison<Task>((x, y) =>
            {
                int result = x.taskTerminal.CompareTo(y.taskTerminal);
                return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
            }));
            resourcesList.Clear();
            resourcesList.Add(occupiedResources);

            foreach (KeyValuePair<int, int[]> pair in desksByTerminalNb)
            {
                firstDeskFromAirportStructure = pair.Value[0];
                lastDeskFromAirportStructure = pair.Value[1];
                int airportTerminalNb = pair.Key;

                if (firstDeskFromAirportStructure > 0 && lastDeskFromAirportStructure > 0 && firstDeskFromAirportStructure <= lastDeskFromAirportStructure)
                {
                    for (int i = firstDeskFromAirportStructure; i <= lastDeskFromAirportStructure; i++)
                    {
                        bool isUsed = false;
                        foreach (Task usedRes in occupiedResources.subTasks)
                        {
                            if (usedRes.taskNumber == i && usedRes.taskTerminal == airportTerminalNb)
                            {
                                isUsed = true;
                                break;
                            }
                        }
                        if (!isUsed)
                        {
                            #region create free resource
                            Task freeResource = new Task();
                            freeResource.type = resourceType.ToString();
                            String partialName = "";
                            String taskNameForAlphaNumericalMode = "";
                            String resourceGroup = "";
                            if (resourceType.Equals(Model.ResourceTypes.FBCheck_In) || resourceType.Equals(Model.ResourceTypes.EcoCheck_In))
                            {
                                if (resourceType.Equals(Model.ResourceTypes.FBCheck_In))
                                    partialName = "Check In";
                                resourceGroup = PAX2SIM.sCheckInGroup;
                            }
                            if (resourceType.Equals(Model.ResourceTypes.FBBagDrop) || resourceType.Equals(Model.ResourceTypes.EcoBagDrop))
                            {
                                if (resourceType.Equals(Model.ResourceTypes.FBBagDrop))
                                    partialName = "Baggage Drop";
                                resourceGroup = PAX2SIM.sCheckInGroup;
                            }
                            if (resourceType.Equals(Model.ResourceTypes.FBMakeUp) || resourceType.Equals(Model.ResourceTypes.EcoMakeUp))
                            {
                                if (resourceType.Equals(Model.ResourceTypes.FBMakeUp))
                                    partialName = "MakeUp";
                                resourceGroup = Model.MAKE_UP_GROUP;
                            }
                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                            {
                                taskNameForAlphaNumericalMode = getElementDescriptionByValueAndTypeAndTerminalNb(null,
                                                                    i, pax2sim.DonneesEnCours.getRacine(), airportTerminalNb,
                                                                    resourceGroup);
                                freeResource.name = partialName + " " + taskNameForAlphaNumericalMode + " / T " + airportTerminalNb;
                            }
                            else
                                freeResource.name = partialName + " " + i + " / T " + airportTerminalNb;

                            freeResource.taskNumber = i;
                            freeResource.taskTerminal = airportTerminalNb;
                            if (isArrival)
                                freeResource.arrivalDeparture = "A";
                            else
                                freeResource.arrivalDeparture = "D";
                            freeResource.resourceType = Model.RANGED_RESOURCES;
                            #endregion
                            // if there is a gap in the desk numbering from the airport structure
                            // the desks from the gap are not included(because they don't exist)
                            if (pax2sim.DonneesEnCours.UseAlphaNumericForFlightInfo)
                            {
                                if (taskNameForAlphaNumericalMode.Length > 0)
                                    freeResources.subTasks.Add(freeResource);
                            }
                            else
                                freeResources.subTasks.Add(freeResource);
                        }
                    }
                }
            }
            freeResources.subTasks.Sort(new Comparison<Task>((x, y) =>
            {
                int result = x.taskTerminal.CompareTo(y.taskTerminal);
                return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
            }));

            resourcesList.Add(freeResources);
            //set the min/max desk number from the airport structure for each terminal
            //the dictionary used for all the resources except the Runway which needs 
            //only the first and last desk
            pax2sim.minDeskNumber = firstDeskFromAirportStructure;
            pax2sim.maxDeskNumber = lastDeskFromAirportStructure;
            pax2sim.desksByTerminalNb = desksByTerminalNb;

            resourcesList.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));
            return resourcesList;
        }

        internal static List<Task> getFlightDataForBaggageClaimGantt(DataTable dt, String octTableName, Boolean isArrival,
                                                                     PAX2SIM pax2sim, bool useExceptions, String scenarioName)
        {
            List<Task> baggageClaimResources = new List<Task>();
            //arrival table terminal column index
            int indexColumnReclaimBeltTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim);
            int indexColumnReclaimBelt = dt.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
            // OCT for Reclaim belts
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", octTableName);
            String lineOpening = GlobalNames.sOCT_Baggage_Line_Opening;
            String lineClosing = GlobalNames.sOCT_Baggage_Line_Closing;

            baggageClaimResources = getFlightDataForResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.BaggageClaim,
                                                                  indexColumnReclaimBeltTerminal, indexColumnReclaimBelt, -1,
                                                                  octTable, lineOpening, lineClosing, useExceptions, scenarioName);

            return baggageClaimResources;
        }

        internal static List<Task> getFlightDataForTransferInfeedGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim)
        {
            List<Task> transferInfeedResources = new List<Task>();
            //arrival table terminal column index
            int indexColumnTransferInfeedTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
            int indexColumnTransferInfeed = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);
            // OCT for transfer infeeds
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.TransferInfeedAllocationRulesTableName);
            String lineOpening = GlobalNames.sOCT_TransferInfeedOpening;
            String lineClosing = GlobalNames.sOCT_TransferInfeedClosing;

            transferInfeedResources = getFlightDataForResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.TransferInfeed,
                                                                  indexColumnTransferInfeedTerminal, indexColumnTransferInfeed, -1,
                                                                  octTable, lineOpening, lineClosing);

            transferInfeedResources.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));
            return transferInfeedResources;
        }

        internal static List<Task> getFlightDataForArrivalInfeedGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim)
        {
            List<Task> arrivalInfeedResources = new List<Task>();
            //arrival table terminal column index
            int indexColumnArrivalInfeedTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
            int indexColumnArrivalInfeedStart = dt.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject);
            int indexColumnArrivalInfeedEnd = dt.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject);
            // OCT for arrival infeeds
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_ArrivalInfeedTableName);
            String lineOpening = GlobalNames.sOCT_Arrival_Infeed_Line_Opening;
            String lineClosing = GlobalNames.sOCT_Arrival_Infeed_Line_Closing;

            arrivalInfeedResources = getFlightDataForResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.ArrivalInfeed,
                                                                  indexColumnArrivalInfeedTerminal, indexColumnArrivalInfeedStart, indexColumnArrivalInfeedEnd,
                                                                  octTable, lineOpening, lineClosing);

            arrivalInfeedResources.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));
            return arrivalInfeedResources;
        }

        internal static List<Task> getFlightDataForArrivalGateGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim)
        {
            List<Task> arrivalGateResources = new List<Task>();
            //arrival table terminal column index
            int indexColumnGateTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int indexColumnGate = dt.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate);
            // OCT for Arrival gates
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_ArrivalGateTableName);
            String lineOpening = GlobalNames.sOCT_Arrival_Gate_Line_Opening;
            String lineClosing = GlobalNames.sOCT_Arrival_Gate_Line_Closing;

            arrivalGateResources = getFlightDataForResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.ArrivalGate,
                                                                  indexColumnGateTerminal, indexColumnGate, -1,
                                                                  octTable, lineOpening, lineClosing);

            return arrivalGateResources;
        }

        internal static List<Task> getFlightDataForCheckInGantt(DataTable dt, String octTableName, Boolean isArrival,
                                                                PAX2SIM pax2sim, bool useExceptions, String scenarioName)
        {
            List<Task> ecoCheckInResources = new List<Task>();
            List<Task> completeCheckInResources = new List<Task>();
            //departure table terminal column index
            int indexColumnEcoCheckInStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
            int indexColumnEcoCheckInEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
            int indexColumnFBCheckInStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
            int indexColumnFBCheckInEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);
            int indexColumnCheckInTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
            // OCT for Boarding gates
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", octTableName);
            String lineOpening = GlobalNames.sOCT_CI_Line_Opening;
            String lineClosing = GlobalNames.sOCT_CI_Line_Closing;

            ecoCheckInResources = getFlightDataForFBEcoResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.FBCheck_In,
                                                                indexColumnCheckInTerminal, indexColumnEcoCheckInStart, indexColumnEcoCheckInEnd,
                                                                indexColumnFBCheckInStart, indexColumnFBCheckInEnd,
                                                                octTable, lineOpening, lineClosing, useExceptions, scenarioName);


            if (completeCheckInResources.Count == 0)
                return ecoCheckInResources;

            return completeCheckInResources;
        }

        internal static List<Task> getFlightDataForBaggageDropGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim)
        {
            List<Task> ecoBaggDropResources = new List<Task>();
            List<Task> completeBaggDropResources = new List<Task>();
            //departure table terminal column index
            int indexColumnEcoBagDropStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
            int indexColumnEcoBagDropEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
            int indexColumnFBBagDropStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
            int indexColumnFBBagDropEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);
            //bag drop and check-in are on the same terminal
            int indexColumnTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
            // OCT for Boarding gates
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_BaggDropTableName);
            String lineOpening = GlobalNames.sOCT_BagDropOpening;
            String lineClosing = GlobalNames.sOCT_BagDropClosing;

            ecoBaggDropResources = getFlightDataForFBEcoResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.FBBagDrop,
                                                                indexColumnTerminal, indexColumnEcoBagDropStart, indexColumnEcoBagDropEnd,
                                                                indexColumnFBBagDropStart, indexColumnFBBagDropEnd,
                                                                octTable, lineOpening, lineClosing);


            if (completeBaggDropResources.Count == 0)
                return ecoBaggDropResources;

            return completeBaggDropResources;
        }

        internal static List<Task> getFlightDataForMakeUpGantt(DataTable dt, String octTableName, Boolean isArrival, PAX2SIM pax2sim)
        {
            List<Task> ecoMakeUpResources = new List<Task>();
            List<Task> completeMakeUpResources = new List<Task>();
            //departure table terminal column index
            int indexColumnEcoMakeUpStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
            int indexColumnEcoMakeUpEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
            int indexColumnFBMakeUpStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
            int indexColumnFBMakeUpEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);
            int indexColumnMakeUpTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup);
            // OCT for Boarding gates
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", octTableName);
            String lineOpening = GlobalNames.sOCT_MakeUpOpening;
            String lineClosing = GlobalNames.sOCT_MakeUpClosing;

            ecoMakeUpResources = getFlightDataForFBEcoResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.FBMakeUp,
                                                                indexColumnMakeUpTerminal, indexColumnEcoMakeUpStart, indexColumnEcoMakeUpEnd,
                                                                indexColumnFBMakeUpStart, indexColumnFBMakeUpEnd,
                                                                octTable, lineOpening, lineClosing);


            if (completeMakeUpResources.Count == 0)
                return ecoMakeUpResources;

            return completeMakeUpResources;
        }

        internal static List<Task> getFlightDataForBoardGateGantt(DataTable dt, String octTableName, Boolean isArrival,
                                                                  PAX2SIM pax2sim, bool useExceptions, String scenarioName)
        {
            List<Task> boardingGateResources = new List<Task>();
            //departure table terminal column index
            int indexColumnGateTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int indexColumnGate = dt.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);
            // OCT for Boarding gates
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", octTableName);
            String lineOpening = GlobalNames.sOCT_Board_Line_Opening;
            String lineClosing = GlobalNames.sOCT_Board_Line_Closing;

            boardingGateResources = getFlightDataForResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.BoardingGate,
                                                                  indexColumnGateTerminal, indexColumnGate, -1,
                                                                  octTable, lineOpening, lineClosing, useExceptions, scenarioName);

            return boardingGateResources;
        }

        internal static List<Task> getFlightDataForArrivalAndDepartureResource(PAX2SIM pax2sim, Model.ResourceTypes resourceType)
        {
            return getFlightDataForArrivalAndDepartureResource(pax2sim, resourceType, "Input");
        }

        internal static List<Task> getFlightDataForArrivalAndDepartureResource(PAX2SIM pax2sim, Model.ResourceTypes resourceType,
                                                                               String scenarioName)
        {
            // holds the resources and flights from the Departure Flight plans
            List<Task> departureFlightsResources = new List<Task>();
            // holds the resources and flights from the Arrival Flight plans
            List<Task> arrivalFlightsResources = new List<Task>();
            // holds the resources found only in the departure resource list or only in the arrival resource list            
            List<Task> departureArrivalFlightsResources = new List<Task>();
            // holds the resources that have been combined from the arrival and departure resource list            
            List<Task> combinedFlightsResources = new List<Task>();
            List<Task> completeResources = new List<Task>();

            DataTable dt = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FPDTableName);
            if (dt.Rows.Count > 0)
            {
                if (resourceType.Equals(Model.ResourceTypes.ParkingStand))
                {
                    if (!scenarioName.Equals("Input"))
                    {
                        ParamScenario ps = pax2sim.DonneesEnCours.GetScenario(scenarioName);
                        dt = pax2sim.DonneesEnCours.getTable("Input", ps.FPD);
                    }
                    departureFlightsResources = getPartialFlightDataForParkingGantt(dt, false, pax2sim, scenarioName);
                }
                if (resourceType.Equals(Model.ResourceTypes.Runway))
                    departureFlightsResources = getPartialFlightDataForRunwayGantt(dt, false, pax2sim);
            }

            dt = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FPATableName);
            if (dt.Rows.Count > 0)
            {
                if (resourceType.Equals(Model.ResourceTypes.ParkingStand))
                {
                    if (!scenarioName.Equals("Input"))
                    {
                        ParamScenario ps = pax2sim.DonneesEnCours.GetScenario(scenarioName);
                        dt = pax2sim.DonneesEnCours.getTable("Input", ps.FPA);
                    }
                    arrivalFlightsResources = getPartialFlightDataForParkingGantt(dt, true, pax2sim, scenarioName);
                }
                if (resourceType.Equals(Model.ResourceTypes.Runway))
                    arrivalFlightsResources = getPartialFlightDataForRunwayGantt(dt, true, pax2sim);
            }
            // return empty list if the flight plan tables are empty
            if (departureFlightsResources.Count == 0 || arrivalFlightsResources.Count == 0)
                return departureFlightsResources;
            // if there are only departure or arrival parking resources
            if (departureFlightsResources.Count == 0 && arrivalFlightsResources[0].subTasks.Count > 0)
            {
                arrivalFlightsResources[0].subTasks.Sort(new Comparison<Task>((x, y) =>
                {
                    int result = x.taskTerminal.CompareTo(y.taskTerminal);
                    return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
                }));
                return arrivalFlightsResources;
            }
            if (arrivalFlightsResources.Count == 0 && departureFlightsResources[0].subTasks.Count > 0)
            {
                departureFlightsResources[0].subTasks.Sort(new Comparison<Task>((x, y) =>
                {
                    int result = x.taskTerminal.CompareTo(y.taskTerminal);
                    return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
                }));
                return departureFlightsResources;
            }
            #region root tasks(occupied + free) added to the completeResources, combined and departureArrivalFlightsResources lists
            // create root task(Occupied resources) which will hold all the used resources
            Task occupiedResources = new Task();
            occupiedResources.name = "Occupied Resources";
            occupiedResources.taskNumber = -2;
            occupiedResources.taskTerminal = -1;
            occupiedResources.arrivalDeparture = "N";
            occupiedResources.type = "usedResources";
            occupiedResources.resourceType = "N";
            // create root task(Free resources) which will hold all the free resources
            Task freeResources = new Task();
            freeResources.name = "Free Resources";
            freeResources.taskNumber = -1;
            freeResources.taskTerminal = -1;
            freeResources.arrivalDeparture = "N";
            freeResources.type = "freeResources";
            freeResources.resourceType = "N";
            completeResources.Add(occupiedResources);
            completeResources.Add(freeResources);

            Task occupiedResourcesDepArr = occupiedResources.clone();
            Task freeResourcesDepArr = freeResources.clone();
            departureArrivalFlightsResources.Add(occupiedResourcesDepArr);
            departureArrivalFlightsResources.Add(freeResourcesDepArr);

            Task occupiedResourcesComb = occupiedResources.clone();
            Task freeResourcesComb = freeResources.clone();
            combinedFlightsResources.Add(occupiedResourcesComb);
            combinedFlightsResources.Add(freeResourcesComb);
            #endregion

            int index = 0;
            for (int i = 0; i < arrivalFlightsResources[0].subTasks.Count; i++)
            {
                for (int j = 0; j < departureFlightsResources[0].subTasks.Count; j++)
                {
                    if (arrivalFlightsResources[0].subTasks[i].name.Equals(departureFlightsResources[0].subTasks[j].name))
                    {
                        // add to a list the resources that have been combined
                        // so that theese resources will not be added to the final list
                        if (!combinedFlightsResources[0].subTasks.Contains(arrivalFlightsResources[0].subTasks[i]))
                            combinedFlightsResources[0].subTasks.Add(arrivalFlightsResources[0].subTasks[i]);
                        if (!combinedFlightsResources[0].subTasks.Contains(departureFlightsResources[0].subTasks[j]))
                            combinedFlightsResources[0].subTasks.Add(departureFlightsResources[0].subTasks[j]);

                        completeResources[0].subTasks.Add(arrivalFlightsResources[0].subTasks[i]);
                        index = completeResources[0].subTasks.IndexOf(arrivalFlightsResources[0].subTasks[i]);
                        foreach (Segment s in departureFlightsResources[0].subTasks[j].segments)
                            completeResources[0].subTasks[index].segments.Add(s);
                    }
                    else
                    {
                        if (!departureArrivalFlightsResources[0].subTasks.Contains(arrivalFlightsResources[0].subTasks[i]))
                            departureArrivalFlightsResources[0].subTasks.Add(arrivalFlightsResources[0].subTasks[i]);
                        if (!departureArrivalFlightsResources[0].subTasks.Contains(departureFlightsResources[0].subTasks[j]))
                            departureArrivalFlightsResources[0].subTasks.Add(departureFlightsResources[0].subTasks[j]);
                    }
                }
            }
            // filter out the resources that have been combined
            if (combinedFlightsResources[0].subTasks.Count > 0)
                foreach (Task t in combinedFlightsResources[0].subTasks)
                {
                    if (departureArrivalFlightsResources[0].subTasks.Contains(t))
                        departureArrivalFlightsResources[0].subTasks.Remove(t);
                }


            if (departureArrivalFlightsResources[0].subTasks.Count > 0)
                foreach (Task t in departureArrivalFlightsResources[0].subTasks)
                {
                    if (!completeResources[0].subTasks.Contains(t))
                        completeResources[0].subTasks.Add(t);
                }


            if (completeResources[0].subTasks.Count == 0)
            {
                departureFlightsResources[0].subTasks.Sort(new Comparison<Task>((x, y) =>
                {
                    int result = x.taskTerminal.CompareTo(y.taskTerminal);
                    return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
                }));
                departureFlightsResources[0].subTasks.Sort((x, y) => x.taskTerminal.CompareTo(y.taskTerminal));
                return departureFlightsResources;
            }

            completeResources[0].subTasks.Sort(new Comparison<Task>((x, y) =>
            {
                int result = x.taskTerminal.CompareTo(y.taskTerminal);
                return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
            }));

            // add those free resources found in both arrival and departure flight resources
            if (departureFlightsResources.Count > 1 && arrivalFlightsResources.Count > 1)
                foreach (Task depRes in departureFlightsResources[1].subTasks)
                    foreach (Task arrRes in arrivalFlightsResources[1].subTasks)
                        if (depRes.taskNumber == arrRes.taskNumber)
                            if (!completeResources[1].subTasks.Contains(arrRes))
                                completeResources[1].subTasks.Add(arrRes);

            return completeResources;
        }

        internal static List<Task> getPartialFlightDataForParkingGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim, String scenarioName)
        {
            List<Task> parkingResources = new List<Task>();
            //arrival/departure table terminal column index
            int indexColumnParkingTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
            int indexColumnParking = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);
            // OCT for Parking stands
            String octTableName = "";
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_ParkingTableName);
            /* No parking OCT table selection in the scenario assistant yet!
            if (scenarioName.Equals("Input"))
                octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_ParkingTableName);
            else
            {
                ParamScenario ps = pax2sim.DonneesEnCours.GetScenario(scenarioName);
                if (ps != null)
                {                    
                }
            }
            */
            String lineOpening = GlobalNames.sOCT_ParkingOpening;
            String lineClosing = GlobalNames.sOCT_ParkingClosing;

            parkingResources = getFlightDataForResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.ParkingStand,
                                                                  indexColumnParkingTerminal, indexColumnParking, -1,
                                                                  octTable, lineOpening, lineClosing, true, scenarioName);

            return parkingResources;
        }

        internal static List<Task> getFlightDataForParkingGantt(PAX2SIM pax2sim, String scenarioName)
        {
            List<Task> finalParkingResources = new List<Task>();
            finalParkingResources = getFlightDataForArrivalAndDepartureResource(pax2sim, Model.ResourceTypes.ParkingStand, scenarioName);

            return finalParkingResources;
        }

        internal static List<Task> getPartialFlightDataForRunwayGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim)
        {
            List<Task> runwayResources = new List<Task>();
            //arrival/departure table column index (the Runway doesn't have a terminal)
            int indexColumnRunway = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay);
            // OCT for Runway
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_RunwayTableName);
            String lineOpening = GlobalNames.sOCT_RunwayOpening;
            String lineClosing = GlobalNames.sOCT_RunwayClosing;

            runwayResources = getFlightDataForResourceGantt(dt, isArrival, pax2sim, Model.ResourceTypes.Runway,
                                                                  -1, indexColumnRunway, -1,
                                                                  octTable, lineOpening, lineClosing);

            return runwayResources;
        }

        internal static List<Task> getFlightDataForRunwayGantt(PAX2SIM pax2sim)
        {
            List<Task> finalRunwayResources = new List<Task>();
            finalRunwayResources = getFlightDataForArrivalAndDepartureResource(pax2sim, Model.ResourceTypes.Runway);

            return finalRunwayResources;
        }

        internal static List<Task> getFlightDataForArrivalParkingGantt(PAX2SIM pax2sim, String scenarioName)
        {
            List<Task> arrivalFlightsResources = new List<Task>();
            DataTable dt = new DataTable();

            if (!scenarioName.Equals("Input"))
            {
                ParamScenario ps = pax2sim.DonneesEnCours.GetScenario(scenarioName);
                if (ps != null)
                    dt = pax2sim.DonneesEnCours.getTable("Input", ps.FPA);
            }
            arrivalFlightsResources = getPartialFlightDataForParkingGantt(dt, true, pax2sim, scenarioName);

            return arrivalFlightsResources;
        }

        internal static List<Task> getFlightDataForDepartureParkingGantt(PAX2SIM pax2sim, String scenarioName)
        {
            List<Task> departureFlightsResources = new List<Task>();
            DataTable dt = new DataTable();

            if (!scenarioName.Equals("Input"))
            {
                ParamScenario ps = pax2sim.DonneesEnCours.GetScenario(scenarioName);
                if (ps != null)
                    dt = pax2sim.DonneesEnCours.getTable("Input", ps.FPD);
            }
            departureFlightsResources = getPartialFlightDataForParkingGantt(dt, false, pax2sim, scenarioName);

            return departureFlightsResources;
        }

        #region Functions used to retrieve data for the Flight Plan Information(Allocation) Gantt
        // These function are used to get the data from the Flight Plan Information table generated
        // when an allocation is made. Gantt type = resource gantt.
        internal static List<Task> getFlightDataForFPIResourceGantt(DataTable dt, PAX2SIM pax2sim)
        {
            List<Task> resourceList = new List<Task>();
            if (dt != null)
            {
                int indexColumnAllocationType = dt.Columns.IndexOf(GlobalNames.FPI_Column_Allocation_Type);
                int indexColumnResourceTypeName = dt.Columns.IndexOf(GlobalNames.FPI_Column_Resource_Type_Name);

                Model.ResourceTypes resourceType = Model.ResourceTypes.Runway;
                String resourceTypeName = "";
                String firstDeskColumnName = "";
                String lastDeskColumnName = "";
                String openingTimeColumnName = "";
                String closingTimeColumnName = "";
                String allocationType = "";
                bool isArrival = false;
                if (dt.Rows.Count > 0)
                {
                    allocationType = dt.Rows[0][indexColumnAllocationType].ToString();
                    resourceTypeName = dt.Rows[0][indexColumnResourceTypeName].ToString();
                }

                #region Resource Type

                //ValueCoders
                //int indexResourceTypeValueNew = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_ResourceType_Value); // >> Bug T-27 (ValueCoder)
                //string newresourceTypeValue = Convert.ToString(dt.Rows[indexResourceTypeValueNew]);  // >> Bug T-27 (ValueCoder)

                if (allocationType == GenerateAllocationTool.TypeAllocation.ArrivalGateAllocation.ToString())
                    resourceType = Model.ResourceTypes.ArrivalGate;

                if (allocationType == GenerateAllocationTool.TypeAllocation.BoardingGateAllocation.ToString())
                    resourceType = Model.ResourceTypes.BoardingGate;

                if (allocationType == GenerateAllocationTool.TypeAllocation.CheckInAllocation.ToString())
                {
                    resourceType = Model.ResourceTypes.FBCheck_In;
                    string valuecoders = "FBCheck_In";
                }

                if (allocationType == GenerateAllocationTool.TypeAllocation.MakeUpAllocation.ToString())
                    resourceType = Model.ResourceTypes.FBMakeUp;

                if (allocationType == GenerateAllocationTool.TypeAllocation.ReclaimAllocation.ToString())
                    resourceType = Model.ResourceTypes.BaggageClaim;

                if (allocationType == GenerateAllocationTool.TypeAllocation.TransferInfeedAllocation.ToString())
                    resourceType = Model.ResourceTypes.TransferInfeed;

                // >> Task #12668 Pax2Sim - Dubai ADPi - Flight Plan Conversion                
                if (allocationType == GenerateAllocationTool.TypeAllocation.Parking.ToString())
                    resourceType = Model.ResourceTypes.ParkingStand;
                // << Task #12668 Pax2Sim - Dubai ADPi - Flight Plan Conversion
                #endregion

                if (resourceType == Model.ResourceTypes.ArrivalGate || resourceType == Model.ResourceTypes.TransferInfeed
                    || resourceType == Model.ResourceTypes.BaggageClaim)
                    isArrival = true;
                else if (resourceType == Model.ResourceTypes.BoardingGate || resourceType == Model.ResourceTypes.FBCheck_In
                            || resourceType == Model.ResourceTypes.FBMakeUp)
                    isArrival = false;

                //FPI table column indexes
                //String resourceTypeName = allocationType.ToString().Substring(0, allocationType.ToString().IndexOf("Allocation"));

                firstDeskColumnName = resourceTypeName + " First Desk";
                lastDeskColumnName = resourceTypeName + " Last Desk";
                openingTimeColumnName = resourceTypeName + " Opening Time";
                closingTimeColumnName = resourceTypeName + " Closing Time";

                int indexColumnTerminal = -1;
                if (isArrival)
                    indexColumnTerminal = 19;   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column
                else
                    indexColumnTerminal = 18;   // << Task #9465 Pax2Sim - Allocation - Add FPI Columns // >> Task #10355 Pax2Sim - Allocation - Flight Plan Information new column
                int indexColumnFirstDesk = dt.Columns.IndexOf(firstDeskColumnName);
                int indexColumnLastDesk = dt.Columns.IndexOf(lastDeskColumnName);
                int indexColumnOpeningTime = dt.Columns.IndexOf(openingTimeColumnName);
                int indexColumnClosingTime = dt.Columns.IndexOf(closingTimeColumnName);

                if (indexColumnTerminal != -1 && indexColumnFirstDesk != -1 && indexColumnLastDesk != -1
                    && indexColumnOpeningTime != -1 && indexColumnClosingTime != -1)
                    resourceList = createResourceListForFPIResourceGantt(dt, isArrival, pax2sim, resourceType, indexColumnTerminal,
                                                                         indexColumnFirstDesk, indexColumnLastDesk,
                                                                         indexColumnOpeningTime, indexColumnClosingTime);
            }
            return resourceList;
        }

        internal static List<Task> createResourceListForFPIResourceGantt(DataTable dt, Boolean isArrival, PAX2SIM pax2sim,
                                                                 Model.ResourceTypes resourceType, int indexColumnTerminalForResource,
                                                                 int indexColumnResourceStart, int indexColumnResourceEnd,
                                                                 int indexColumnOpeningTime, int indexColumnClosingTime)
        {
            List<Task> resourcesList = new List<Task>();
            ArrayList airlineCodes = new ArrayList();
            ArrayList flightCategories = new ArrayList();
            ArrayList groundHandlingCompanies = new ArrayList();
            // Flight details - nb, category, airline
            int indexColumnFlightId = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int indexColumnFlightNb = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnFlightCategory = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int indexColumnFlightAirline = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int indexColumnNbSeats = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_NbSeats); // >> Bug #13367 Liege allocation
            int iIndexAirlineCodes_ColumnFlightAirlineGroundHandler = -1;
            int iIndexAirlineCodes_ColumnFlightAirline = -1;

            int indexResourceTypeValue = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_ResourceType_Value); // >> Bug T-27 (ValueCoder)

            // Flight Categories table - used to set the flight color according to the flight category
            // colorCriteriaTable - Flight Categories or Airline Codes
            DataTable colorCriteriaTable = new DataTable();
            int indexColumnColorCriteria = -1;
            String flightAirlineGroundHandler = "";
            if (pax2sim.colorByFlightCategory && !pax2sim.colorByAirlineCode && !pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_FlightCategoriesTableName);
                indexColumnColorCriteria = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPFlightCategory_FC);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!flightCategories.Contains(line[indexColumnColorCriteria].ToString()))
                            flightCategories.Add(line[indexColumnColorCriteria].ToString());
                    }
                    flightCategories.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }
            else if (!pax2sim.colorByFlightCategory && pax2sim.colorByAirlineCode && !pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                indexColumnColorCriteria = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!airlineCodes.Contains(line[indexColumnColorCriteria].ToString()))
                            airlineCodes.Add(line[indexColumnColorCriteria].ToString());
                    }
                    airlineCodes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }
            else if (!pax2sim.colorByFlightCategory && !pax2sim.colorByAirlineCode && pax2sim.colorByGroundHandlerCode)
            {
                colorCriteriaTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                iIndexAirlineCodes_ColumnFlightAirlineGroundHandler = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_GroundHandlers);
                iIndexAirlineCodes_ColumnFlightAirline = colorCriteriaTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                if (colorCriteriaTable != null)
                {
                    foreach (DataRow line in colorCriteriaTable.Rows)
                    {
                        if (!groundHandlingCompanies.Contains(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString()))
                            groundHandlingCompanies.Add(line[iIndexAirlineCodes_ColumnFlightAirlineGroundHandler].ToString());
                    }
                    groundHandlingCompanies.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                }
            }

            // create root task(Occupied resources) which will hold all the used resources
            Task occupiedResources = new Task();
            occupiedResources.name = "Occupied Resources";
            occupiedResources.taskNumber = -2;
            occupiedResources.taskTerminal = -1;
            occupiedResources.arrivalDeparture = "N";
            occupiedResources.type = "usedResources";
            occupiedResources.resourceType = "N";
            // create root task(Free resources) which will hold all the free resources
            Task freeResources = new Task();
            freeResources.name = "Free Resources";
            freeResources.taskNumber = -1;
            freeResources.taskTerminal = -1;
            freeResources.arrivalDeparture = "N";
            freeResources.type = "freeResources";
            freeResources.resourceType = "N";

            int terminals = 0;
            // if the resource doesn't use a terminal (ex.: Runway) 
            // the number of terminals will be set to 1 to allow the calculation once
            if (indexColumnTerminalForResource == -1)
                terminals = 1;
            else
                terminals = OverallTools.DataFunctions.valeurMaximaleDansColonne(dt, indexColumnTerminalForResource);

            #region get the first and last desk from the Airport structure according to the resource type

            String[] terminalsFromAirportStructure = pax2sim.DonneesEnCours.getTerminal();
            int firstDeskFromAirportStructure = 0;
            int lastDeskFromAirportStructure = 0;
            // Key: terminal number / value: vector holding the first and last desk
            // Each terminal has associated the first and last desk from the airport structure
            // for a specific resource
            Dictionary<int, int[]> desksByTerminalNb = new Dictionary<int, int[]>();
            getTerminalsAndDesksForResource(pax2sim, resourceType.ToString(), out desksByTerminalNb,
                                            out firstDeskFromAirportStructure, out lastDeskFromAirportStructure);
            // >> Bug #13367 Liege allocation
            if (PAX2SIM.liegeMode)
            {
                desksByTerminalNb = new Dictionary<int, int[]>();
                if (resourceType == Model.ResourceTypes.ParkingStand)
                {
                    firstDeskFromAirportStructure = Allocation.DUMMY_UNALLOCATED_RESOURCE_CODE_NB;//Allocation.MIN_PARKING_STAND_CODE_NB;
                    lastDeskFromAirportStructure = Allocation.MAX_BACKUP_PARKING_STAND_CODE_NB;
                }
                else if (resourceType == Model.ResourceTypes.BoardingGate)
                {
                    firstDeskFromAirportStructure = Allocation.DUMMY_UNALLOCATED_RESOURCE_CODE_NB;//Allocation.MIN_BOARDING_GATE_CODE_NB;
                    lastDeskFromAirportStructure = Allocation.MAX_BOARDING_GATE_CODE_NB;
                }
                desksByTerminalNb.Add(1, new int[] { firstDeskFromAirportStructure, lastDeskFromAirportStructure });
            }
            // << Bug #13367 Liege allocation
            #endregion

            int terminalNb = 0;
            int resourceStartDesk = 0;
            int resourceEndDesk = 0;
            int flightId = 0;
            String flightNb = "";
            String flightCategory = "";
            String flightAirline = "";
            int nbSeats = 0;    // >> Bug #13367 Liege allocation

            string resourceTypeValue = "";  // >> Bug T-27 (ValueCoder)

            DateTime resourceStartTime = new DateTime();
            DateTime resourceEndTime = new DateTime();

            //reset the variables used to hold the min 
            //and max desk number for the current resource
            pax2sim.minDeskNumber = -1;
            pax2sim.maxDeskNumber = -1;
            pax2sim.desksByTerminalNb.Clear();

            bool modified = false;

            if (terminals > 0)
            {
                //for every terminal go through the table and get the resources
                for (int i = 1; i <= terminals; i++)
                {
                    foreach (DataRow drRow in dt.Rows)
                    {
                        if (indexColumnTerminalForResource == -1)
                            terminalNb = 1;
                        else
                            terminalNb = FonctionsType.getInt(drRow[indexColumnTerminalForResource]);
                        flightId = FonctionsType.getInt(drRow[indexColumnFlightId]);
                        flightNb = drRow[indexColumnFlightNb].ToString();
                        flightCategory = drRow[indexColumnFlightCategory].ToString();
                        flightAirline = drRow[indexColumnFlightAirline].ToString();
                        nbSeats = FonctionsType.getInt(drRow[indexColumnNbSeats]);  // >> Bug #13367 Liege allocation

                        resourceTypeValue = Convert.ToString(drRow[indexResourceTypeValue]);  // >> Bug T-27 (ValueCoder)

                        resourceStartDesk = FonctionsType.getInt(drRow[indexColumnResourceStart]);
                        if (indexColumnResourceEnd == -1)
                            resourceEndDesk = resourceStartDesk;
                        else
                            resourceEndDesk = FonctionsType.getInt(drRow[indexColumnResourceEnd]);
                        //if (resourceStartDesk == 0 || resourceEndDesk == 0)   // >> Bug #13367 Liege allocation
                        //    break;
                        if (resourceStartDesk < 0 || resourceEndDesk < 0)
                            break;

                        for (int k = resourceStartDesk; k <= resourceEndDesk; k++)
                        {
                            modified = false;
                            if (terminalNb == i)
                            {
                                if (resourcesList != null && resourcesList.Count > 0)
                                    foreach (Task res in resourcesList)
                                    {
                                        if (res.taskNumber == k && res.taskTerminal == terminalNb)
                                        {//the resource was created earlier so we just add the flight
                                            Segment resourceFlight = new Segment();
                                            resourceFlight.id = flightId;
                                            resourceStartTime = DateTime.Parse(drRow[indexColumnOpeningTime].ToString());
                                            resourceEndTime = DateTime.Parse(drRow[indexColumnClosingTime].ToString());
                                            resourceFlight.start = resourceStartTime;
                                            resourceFlight.end = resourceEndTime;
                                            // << Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                            resourceFlight.middleText = flightId + " / " + flightAirline + " / " + flightNb + " / " + flightCategory + " / " + nbSeats + " pax";    // >> Bug #13367 Liege allocation
                                            // >> Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                            #region flight category - color

                                            //int criteriaRowNb = 0;
                                            int colorIndex = 0;
                                            /*
                                            if (indexColumnColorCriteria != -1)
                                            {
                                                foreach (DataRow criteriaTableRow in colorCriteriaTable.Rows)
                                                {
                                                    if (pax2sim.colorByFlightCategory)
                                                    {
                                                        if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightCategory))
                                                            break;
                                                    }
                                                    else if (pax2sim.colorByAirlineCode)
                                                    {
                                                        if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightAirline))
                                                            break;
                                                    }
                                                    criteriaRowNb++;
                                                }
                                                colorIndex = criteriaRowNb + 1;
                                            }*/
                                            if (pax2sim.colorByFlightCategory)
                                            {
                                                if ((flightCategory == null) || (flightCategory == "") || (flightCategories.Count == 0))
                                                    colorIndex = 0;
                                                else
                                                    colorIndex = flightCategories.IndexOf(flightCategory) + 1;
                                            }
                                            else if (pax2sim.colorByAirlineCode)
                                            {
                                                if ((flightAirline == null) || (flightAirline == "") || (airlineCodes.Count == 0))
                                                    colorIndex = 0;
                                                else
                                                    colorIndex = airlineCodes.IndexOf(flightAirline) + 1;
                                            }
                                            else if (pax2sim.colorByGroundHandlerCode)
                                            {
                                                // for GH coloring the colorIndex will be -1 and will be set in this block
                                                // this is because a list is used to load all the GH codes instead of the 
                                                // pax2sim table( in the table the GH can repead over several lines => it 
                                                // will not generated same color for a GH if we check the row nb)
                                                flightAirlineGroundHandler = OverallTools.DataFunctions
                                                        .getValue(colorCriteriaTable, flightAirline, iIndexAirlineCodes_ColumnFlightAirline,
                                                                  iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                                                if ((flightAirlineGroundHandler == null) || (flightAirlineGroundHandler == "")
                                                    || (groundHandlingCompanies.Count == 0))
                                                    colorIndex = 0;
                                                else
                                                    colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                                            }

                                            // the flight category from FP has been found in the Flight Categories table
                                            // if (criteriaRowNb < colorCriteriaTable.Rows.Count || pax2sim.colorByGroundHandlerCode)
                                            resourceFlight.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());

                                            #endregion

                                            if (isArrival)
                                                resourceFlight.arrivalDeparture = "A";
                                            else
                                                resourceFlight.arrivalDeparture = "D";
                                            // set the first and last desk occupied for range resources(Arrival Infeeds)
                                            // and the desk occupied for non-range resources
                                            resourceFlight.startDesk = resourceStartDesk;
                                            resourceFlight.endDesk = resourceEndDesk;
                                            /*
                                            if (res.resourceType == Model.RANGED_RESOURCES)
                                            {
                                                resourceFlight.startDesk = resourceStartDesk;
                                                resourceFlight.endDesk = resourceEndDesk;
                                            }
                                            else if (res.resourceType == Model.NON_RANGED_RESOURCES)
                                            {
                                                resourceFlight.startDesk = resourceStartDesk;
                                                resourceFlight.endDesk = resourceEndDesk;
                                            }
                                            */
                                            // add the flight only if the its flight category has been found in the Flight categories table
                                            //if (criteriaRowNb < colorCriteriaTable.Rows.Count)
                                            res.segments.Add(resourceFlight);
                                            modified = true;
                                            break;
                                        }
                                    }
                                if (!modified)
                                {
                                    //the resource hasn't been created yet
                                    Task resource = new Task();
                                    // >> Bug T-27 (ValueCoder):-To Solve the bug of child resource
                                    //resource.type = resourceType.ToString();
                                    resource.type = resourceTypeValue.ToString();

                                    // Runway resource doesn't have a terminal
                                    //if (indexColumnTerminalForResource == -1)
                                    //    resource.name = resourceType + " " + k;
                                    //else
                                    //    resource.name = resourceType + " " + k + " / T " + terminalNb;
                                    //resource.name = resourceTypeValue + " " + k + " / T " + terminalNb;

                                    if (indexColumnTerminalForResource == -1)
                                        resource.name = resourceTypeValue + " " + k;
                                    else
                                        resource.name = resourceTypeValue + " " + k + " / T " + terminalNb;


                                    resource.taskNumber = k;
                                    resource.taskTerminal = terminalNb;
                                    if (isArrival)
                                        resource.arrivalDeparture = "A";
                                    else
                                        resource.arrivalDeparture = "D";
                                    //set the range/non-range resource type
                                    //if (indexColumnResourceEnd == -1)
                                    if (resourceType == Model.ResourceTypes.ArrivalGate || resourceType == Model.ResourceTypes.BoardingGate
                                        || resourceType == Model.ResourceTypes.BaggageClaim || resourceType == Model.ResourceTypes.TransferInfeed)
                                        resource.resourceType = Model.NON_RANGED_RESOURCES;
                                    else if (resourceType == Model.ResourceTypes.FBCheck_In || resourceType == Model.ResourceTypes.FBMakeUp)
                                        resource.resourceType = Model.RANGED_RESOURCES;

                                    Segment resourceFlight = new Segment();
                                    resourceFlight.id = flightId;
                                    resourceStartTime = DateTime.Parse(drRow[indexColumnOpeningTime].ToString());
                                    resourceEndTime = DateTime.Parse(drRow[indexColumnClosingTime].ToString());
                                    resourceFlight.start = resourceStartTime;
                                    resourceFlight.end = resourceEndTime;
                                    // << Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                    resourceFlight.middleText = flightId + " / " + flightAirline + " / " + flightNb + " / " + flightCategory + " / " + nbSeats + " pax";    // >> Bug #13367 Liege allocation
                                    // >> Bug #7863 Flight Category missing from the Information panel - Resource Gantt
                                    #region flight category - color

                                    //int criteriaRowNb = 0;
                                    int colorIndex = 0;
                                    /*
                                    if (indexColumnColorCriteria != -1)
                                    {
                                        foreach (DataRow criteriaTableRow in colorCriteriaTable.Rows)
                                        {
                                            if (pax2sim.colorByFlightCategory)
                                            {
                                                if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightCategory))
                                                    break;
                                            }
                                            else if (pax2sim.colorByAirlineCode)
                                            {
                                                if (criteriaTableRow[indexColumnColorCriteria].ToString().Equals(flightAirline))
                                                    break;
                                            }
                                            criteriaRowNb++;
                                        }
                                        colorIndex = criteriaRowNb + 1;
                                    }
                                    */
                                    if (pax2sim.colorByFlightCategory)
                                    {
                                        if ((flightCategory == null) || (flightCategory == "") || (flightCategories.Count == 0))
                                            colorIndex = 0;
                                        else
                                            colorIndex = flightCategories.IndexOf(flightCategory) + 1;
                                    }
                                    else if (pax2sim.colorByAirlineCode)
                                    {
                                        if ((flightAirline == null) || (flightAirline == "") || (airlineCodes.Count == 0))
                                            colorIndex = 0;
                                        else
                                            colorIndex = airlineCodes.IndexOf(flightAirline) + 1;
                                    }
                                    else if (pax2sim.colorByGroundHandlerCode)
                                    {
                                        // for GH coloring the colorIndex will be -1 and will be set in this block
                                        // this is because a list is used to load all the GH codes instead of the 
                                        // pax2sim table( in the table the GH can repead over several lines => it 
                                        // will not generated same color for a GH if we check the row nb)
                                        flightAirlineGroundHandler = OverallTools.DataFunctions
                                                .getValue(colorCriteriaTable, flightAirline, iIndexAirlineCodes_ColumnFlightAirline,
                                                          iIndexAirlineCodes_ColumnFlightAirlineGroundHandler);
                                        if ((flightAirlineGroundHandler == null) || (flightAirlineGroundHandler == "")
                                            || (groundHandlingCompanies.Count == 0))
                                            colorIndex = 0;
                                        else
                                            colorIndex = groundHandlingCompanies.IndexOf(flightAirlineGroundHandler) + 1;
                                    }

                                    // the flight category from FP has been found in the Flight Categories table
                                    //if (criteriaRowNb < colorCriteriaTable.Rows.Count || pax2sim.colorByGroundHandlerCode)
                                    resourceFlight.color = "" + (OverallTools.FonctionUtiles.getColor(colorIndex).ToArgb());

                                    #endregion
                                    if (isArrival)
                                        resourceFlight.arrivalDeparture = "A";
                                    else
                                        resourceFlight.arrivalDeparture = "D";
                                    // set the first and last desk occupied for range resources(Arrival Infeeds)
                                    // and the desk occupied for non-range resources
                                    resourceFlight.startDesk = resourceStartDesk;
                                    resourceFlight.endDesk = resourceEndDesk;
                                    /*
                                    if (resource.resourceType == Model.RANGED_RESOURCES)
                                    {
                                        resourceFlight.startDesk = resourceStartDesk;
                                        resourceFlight.endDesk = resourceEndDesk;
                                    }
                                    else if (resource.resourceType == Model.NON_RANGED_RESOURCES)
                                        resourceFlight.startDesk = resourceStartDesk;
                                    */
                                    // add the flight only if the its flight category has been found in the Flight categories table
                                    //if (criteriaRowNb < colorCriteriaTable.Rows.Count)
                                    resource.segments.Add(resourceFlight);
                                    if (resource.segments.Count > 0)
                                        resourcesList.Add(resource);
                                }
                            }

                        }
                    }
                }
            }

            resourcesList.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));
            foreach (Task res in resourcesList)
            {
                occupiedResources.subTasks.Add(res);
            }
            occupiedResources.subTasks.Sort(new Comparison<Task>((x, y) =>
            {
                int result = x.taskTerminal.CompareTo(y.taskTerminal);
                return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
            }));
            resourcesList.Clear();
            resourcesList.Add(occupiedResources);

            // >> Task #12808 Pax2Sim - allocation Liege
            if (PAX2SIM.liegeMode)
            {

                int nb = 1;
                if (desksByTerminalNb.ContainsKey(nb))
                {
                    int[] desks = desksByTerminalNb[nb];
                    if (desks.Length == 2)
                    {
                        int firstDeskNb = desks[0];
                        int lastDeskNb = desks[1];
                        if (resourceType == Model.ResourceTypes.ParkingStand)
                        {
                            if (firstDeskNb > Allocation.MIN_PARKING_STAND_CODE_NB)
                                firstDeskNb = Allocation.MIN_PARKING_STAND_CODE_NB;
                            if (lastDeskNb < Allocation.MAX_BACKUP_PARKING_STAND_CODE_NB)
                                lastDeskNb = Allocation.MAX_BACKUP_PARKING_STAND_CODE_NB;
                        }
                        else if (resourceType == Model.ResourceTypes.BoardingGate)
                        {
                            if (firstDeskNb > Allocation.MIN_BOARDING_GATE_CODE_NB)
                                firstDeskNb = Allocation.MIN_BOARDING_GATE_CODE_NB;
                            if (lastDeskNb < Allocation.MAX_BOARDING_GATE_CODE_NB)
                                lastDeskNb = Allocation.MAX_BOARDING_GATE_CODE_NB;
                        }
                        else if (resourceType == Model.ResourceTypes.FBCheck_In)
                        {
                            if (firstDeskNb > Allocation.MIN_CHECK_IN_CODE_NB)
                                firstDeskNb = Allocation.MIN_CHECK_IN_CODE_NB;
                            if (lastDeskNb < Allocation.MAX_CHECK_IN_CODE_NB)
                                lastDeskNb = Allocation.MAX_CHECK_IN_CODE_NB;
                        }
                        int[] desksForLiege = new int[] { firstDeskNb, lastDeskNb };
                        desksByTerminalNb.Remove(nb);
                        desksByTerminalNb.Add(nb, desksForLiege);
                    }
                }

            }
            // << Task #12808 Pax2Sim - allocation Liege

            foreach (KeyValuePair<int, int[]> pair in desksByTerminalNb)
            {
                firstDeskFromAirportStructure = pair.Value[0];
                lastDeskFromAirportStructure = pair.Value[1];
                int airportTerminalNb = pair.Key;

                if (firstDeskFromAirportStructure >= 0 && lastDeskFromAirportStructure >= 0 && firstDeskFromAirportStructure <= lastDeskFromAirportStructure)
                {
                    for (int i = firstDeskFromAirportStructure; i <= lastDeskFromAirportStructure; i++)
                    {
                        // >> Task #12808 Pax2Sim - allocation Liege
                        if (PAX2SIM.liegeMode)
                        {
                            if (resourceType == Model.ResourceTypes.ParkingStand)
                            {
                                if (i < Allocation.MIN_PARKING_STAND_CODE_NB
                                    || (i > Allocation.MAX_PARKING_STAND_CODE_NB && i < Allocation.MIN_BACKUP_PARKING_STAND_CODE_NB)
                                    || i > Allocation.MAX_BACKUP_PARKING_STAND_CODE_NB)
                                {
                                    if (i != Allocation.DUMMY_UNALLOCATED_RESOURCE_CODE_NB)
                                    {
                                        continue;
                                    }
                                }
                            }
                            else if (resourceType == Model.ResourceTypes.BoardingGate)
                            {
                                if (i < Allocation.MIN_BOARDING_GATE_CODE_NB
                                    || i > Allocation.MAX_BOARDING_GATE_CODE_NB)
                                {
                                    if (i != Allocation.DUMMY_UNALLOCATED_RESOURCE_CODE_NB)
                                    {
                                        continue;
                                    }
                                }
                            }
                            else if (resourceType == Model.ResourceTypes.FBCheck_In)
                            {
                                if (i < Allocation.MIN_CHECK_IN_CODE_NB
                                    || i > Allocation.MAX_CHECK_IN_CODE_NB)
                                {
                                    if (i != Allocation.DUMMY_UNALLOCATED_RESOURCE_CODE_NB)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        // << Task #12808 Pax2Sim - allocation Liege

                        bool isUsed = false;
                        foreach (Task usedRes in occupiedResources.subTasks)
                        {
                            if (usedRes.taskNumber == i && usedRes.taskTerminal == airportTerminalNb)
                            {
                                isUsed = true;
                                break;
                            }
                        }
                        if (!isUsed)
                        {
                            #region create free resource
                            Task freeResource = new Task();
                            freeResource.type = resourceType.ToString();
                            if (indexColumnTerminalForResource == -1)
                                freeResource.name = resourceType + " " + i;
                            else
                                freeResource.name = resourceType + " " + i + " / T " + airportTerminalNb;

                            freeResource.taskNumber = i;
                            freeResource.taskTerminal = airportTerminalNb;
                            if (isArrival)
                                freeResource.arrivalDeparture = "A";
                            else
                                freeResource.arrivalDeparture = "D";
                            if (indexColumnResourceEnd == -1)
                                freeResource.resourceType = Model.NON_RANGED_RESOURCES;
                            else
                                freeResource.resourceType = Model.RANGED_RESOURCES;
                            #endregion
                            freeResources.subTasks.Add(freeResource);
                        }
                    }
                }
            }
            freeResources.subTasks.Sort(new Comparison<Task>((x, y) =>
            {
                int result = x.taskTerminal.CompareTo(y.taskTerminal);
                return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
            }));
            //don't display free resources in the Allocation Gantt. Only for Liege
            if (PAX2SIM.liegeMode)
            {
                resourcesList.Add(freeResources);
            }
            //set the min/max desk number from the airport structure for each terminal
            //the dictionary used for all the resources except the Runway which needs 
            //only the first and last desk
            pax2sim.desksByTerminalNb = desksByTerminalNb;
            pax2sim.minDeskNumber = firstDeskFromAirportStructure;
            pax2sim.maxDeskNumber = lastDeskFromAirportStructure;

            resourcesList.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));

            return resourcesList;
        }
        #endregion

        #region old functions getFlightData...
        internal static List<Task> getFlightDataForParkingGantt_(PAX2SIM pax2sim)
        {
            List<Task> departureParkingFlightsResources = new List<Task>();
            List<Task> arrivalParkingFlightsResources = new List<Task>();
            List<Task> completeParkingResources = new List<Task>();
            List<Task> departureArrivalParkingFlightsResources = new List<Task>();
            List<Task> combinedParkingFlightsResources = new List<Task>();

            DataTable dt = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FPDTableName);
            if (dt.Rows.Count > 0)
                departureParkingFlightsResources = getPartialFlightDataForParkingGantt(dt, false, pax2sim, "");

            dt = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FPATableName);
            if (dt.Rows.Count > 0)
                arrivalParkingFlightsResources = getPartialFlightDataForParkingGantt(dt, true, pax2sim, "");

            // if there are only departure or arrival parking resources
            if (departureParkingFlightsResources.Count == 0 && arrivalParkingFlightsResources.Count > 0)
            {
                arrivalParkingFlightsResources.Sort(new Comparison<Task>((x, y) =>
                {
                    int result = x.taskTerminal.CompareTo(y.taskTerminal);
                    return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
                }));
                return arrivalParkingFlightsResources;
            }
            if (arrivalParkingFlightsResources.Count == 0 && departureParkingFlightsResources.Count > 0)
            {
                departureParkingFlightsResources.Sort(new Comparison<Task>((x, y) =>
                {
                    int result = x.taskTerminal.CompareTo(y.taskTerminal);
                    return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
                }));
                return departureParkingFlightsResources;
            }

            int index = 0;
            for (int i = 0; i < arrivalParkingFlightsResources.Count; i++)
            {
                for (int j = 0; j < departureParkingFlightsResources.Count; j++)
                {
                    if (arrivalParkingFlightsResources[i].name.Equals(departureParkingFlightsResources[j].name))
                    {
                        completeParkingResources.Add(arrivalParkingFlightsResources[i]);
                        index = completeParkingResources.IndexOf(arrivalParkingFlightsResources[i]);
                        foreach (Segment s in departureParkingFlightsResources[j].segments)
                            completeParkingResources[index].segments.Add(s);
                        // add to a list the resources that have been combined
                        // so that theese resources will not be added to the final list
                        if (!combinedParkingFlightsResources.Contains(arrivalParkingFlightsResources[i]))
                            combinedParkingFlightsResources.Add(arrivalParkingFlightsResources[i]);
                        if (!combinedParkingFlightsResources.Contains(departureParkingFlightsResources[j]))
                            combinedParkingFlightsResources.Add(departureParkingFlightsResources[j]);
                    }
                    else
                    {
                        if (!departureArrivalParkingFlightsResources.Contains(arrivalParkingFlightsResources[i]))
                            departureArrivalParkingFlightsResources.Add(arrivalParkingFlightsResources[i]);
                        if (!departureArrivalParkingFlightsResources.Contains(departureParkingFlightsResources[j]))
                            departureArrivalParkingFlightsResources.Add(departureParkingFlightsResources[j]);
                    }
                }
            }
            // filter out the resources that have been combined
            if (combinedParkingFlightsResources.Count > 0)
                foreach (Task t in combinedParkingFlightsResources)
                {
                    if (departureArrivalParkingFlightsResources.Contains(t))
                        departureArrivalParkingFlightsResources.Remove(t);
                }


            if (departureArrivalParkingFlightsResources.Count > 0)
                foreach (Task t in departureArrivalParkingFlightsResources)
                {
                    if (!completeParkingResources.Contains(t))
                        completeParkingResources.Add(t);
                }


            if (completeParkingResources.Count == 0)
            {
                departureParkingFlightsResources.Sort(new Comparison<Task>((x, y) =>
                {
                    int result = x.taskTerminal.CompareTo(y.taskTerminal);
                    return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
                }));
                departureParkingFlightsResources.Sort((x, y) => x.taskTerminal.CompareTo(y.taskTerminal));
                return departureParkingFlightsResources;
            }

            completeParkingResources.Sort(new Comparison<Task>((x, y) =>
            {
                int result = x.taskTerminal.CompareTo(y.taskTerminal);
                return (result != 0) ? result : x.taskNumber.CompareTo(y.taskNumber);
            }));
            return completeParkingResources;
        }

        internal static List<Task> getFlightDataForRunwayGantt_(PAX2SIM pax2sim)
        {
            List<Task> departureRunwayFlightsResources = new List<Task>();
            List<Task> arrivalRunwayFlightsResources = new List<Task>();
            List<Task> completeRunwayResources = new List<Task>();
            List<Task> departureOnlyRunwayFlightsResources = new List<Task>();
            List<Task> combinedParkingFlightsResources = new List<Task>();


            DataTable dt = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FPDTableName);

            if (dt.Rows.Count > 0)
                departureRunwayFlightsResources = getPartialFlightDataForRunwayGantt(dt, false, pax2sim);
            dt = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.FPATableName);
            if (dt.Rows.Count > 0)
                completeRunwayResources = getPartialFlightDataForRunwayGantt(dt, true, pax2sim);

            for (int i = 0; i < completeRunwayResources.Count; i++)
            {
                for (int j = 0; j < departureRunwayFlightsResources.Count; j++)
                {
                    if (completeRunwayResources[i].name.Equals(departureRunwayFlightsResources[j].name))
                    {
                        foreach (Segment s in departureRunwayFlightsResources[j].segments)
                            completeRunwayResources[i].segments.Add(s);

                    }
                    else
                    {
                        if (!departureOnlyRunwayFlightsResources.Contains(departureRunwayFlightsResources[j]))
                            departureOnlyRunwayFlightsResources.Add(departureRunwayFlightsResources[j]);
                    }
                }
            }
            if (departureOnlyRunwayFlightsResources.Count > 0)
                foreach (Task t in departureOnlyRunwayFlightsResources)
                    completeRunwayResources.Add(t);

            if (completeRunwayResources.Count == 0)
                return departureRunwayFlightsResources;

            return completeRunwayResources;
        }

        // not used 
        internal static List<Task> getFlightDataForBaggageClaimGant_old(DataTable dt, Boolean isArrival, PAX2SIM pax2sim)
        {
            List<Task> baggageClaimResources = new List<Task>();
            // Flight details - nb, category, airline
            int indexColumnFlightNb = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnFlightCategory = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int indexColumnFlightAirline = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            //arrival table terminal column index
            int indexColumnReclaimBeltTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim);
            int indexColumnReclaimBelt = dt.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
            // OCT for Reclaim belts
            NormalTable octTable = pax2sim.DonneesEnCours.GetTable("Input", GlobalNames.OCT_BaggageClaimTableName);
            String lineOpening = GlobalNames.sOCT_Baggage_Line_Opening;
            String lineClosing = GlobalNames.sOCT_Baggage_Line_Closing;

            int terminals = 0;
            int terminalNb = 0;
            int reclaimBeltNb = 0;
            String flightNb = "";
            String flightCategory = "";
            String flightAirline = "";

            DateTime reclaimBeltStart = new DateTime();
            DateTime reclaimBeltEnd = new DateTime();

            terminals = OverallTools.DataFunctions.valeurMaximaleDansColonne(dt, indexColumnReclaimBeltTerminal);

            bool modified = false;

            if (terminals > 0)
            {
                //for every terminal go through the table and get the resources
                for (int i = 1; i <= terminals; i++)
                {
                    foreach (DataRow drRow in dt.Rows)
                    {
                        terminalNb = FonctionsType.getInt(drRow[indexColumnReclaimBeltTerminal]);
                        reclaimBeltNb = FonctionsType.getInt(drRow[indexColumnReclaimBelt]);
                        flightNb = drRow[indexColumnFlightNb].ToString();
                        flightCategory = drRow[indexColumnFlightCategory].ToString();
                        flightAirline = drRow[indexColumnFlightAirline].ToString();

                        if (reclaimBeltNb == 0)
                            continue;
                        modified = false;
                        if (terminalNb == i)
                        {
                            if (baggageClaimResources != null && baggageClaimResources.Count > 0)
                                foreach (Task rb in baggageClaimResources)
                                {
                                    if (rb.taskNumber == reclaimBeltNb && rb.taskTerminal == terminalNb)
                                    {//the resource was created earlier so we just add the flight
                                        Segment reclaimBeltFlight = new Segment();
                                        getStartEndDates(drRow, isArrival, Model.ResourceTypes.BaggageClaim, octTable, lineOpening, lineClosing,
                                                         out reclaimBeltStart, out reclaimBeltEnd);
                                        reclaimBeltFlight.start = reclaimBeltStart;
                                        reclaimBeltFlight.end = reclaimBeltEnd;
                                        reclaimBeltFlight.middleText = flightAirline + " / " + flightNb;
                                        switch (flightCategory)
                                        {
                                            case Model.DOMESTIC:
                                                reclaimBeltFlight.color = Model.DOMESTIC_COLOR;
                                                break;
                                            case Model.INTRASHENGEN:
                                                reclaimBeltFlight.color = Model.INTRASHENGEN_COLOR;
                                                break;
                                            case Model.EXTRASHENGEN:
                                                reclaimBeltFlight.color = Model.EXTRASHENGEN_COLOR;
                                                break;
                                            default:
                                                break;
                                        }
                                        rb.segments.Add(reclaimBeltFlight);
                                        modified = true;
                                        break;
                                    }
                                }
                            if (!modified)
                            {
                                //the resource hasn't been created yet
                                Task reclaimBelt = new Task();
                                reclaimBelt.name = Model.ResourceTypes.BaggageClaim + " " + reclaimBeltNb + " / T " + terminalNb;
                                reclaimBelt.taskNumber = reclaimBeltNb;
                                reclaimBelt.taskTerminal = terminalNb;

                                Segment reclaimBeltFlight = new Segment();
                                getStartEndDates(drRow, isArrival, Model.ResourceTypes.BaggageClaim, octTable, lineOpening, lineClosing,
                                                 out reclaimBeltStart, out reclaimBeltEnd);
                                reclaimBeltFlight.start = reclaimBeltStart;
                                reclaimBeltFlight.end = reclaimBeltEnd;
                                reclaimBeltFlight.middleText = flightAirline + " / " + flightNb;
                                switch (flightCategory)
                                {
                                    case Model.DOMESTIC:
                                        reclaimBeltFlight.color = Model.DOMESTIC_COLOR;
                                        break;
                                    case Model.INTRASHENGEN:
                                        reclaimBeltFlight.color = Model.INTRASHENGEN_COLOR;
                                        break;
                                    case Model.EXTRASHENGEN:
                                        reclaimBeltFlight.color = Model.EXTRASHENGEN_COLOR;
                                        break;
                                    default:
                                        break;
                                }
                                reclaimBelt.segments.Add(reclaimBeltFlight);

                                baggageClaimResources.Add(reclaimBelt);
                            }
                        }
                    }

                }
            }
            baggageClaimResources.Sort((x, y) => x.taskNumber.CompareTo(y.taskNumber));
            return baggageClaimResources;
        }
        #endregion

        /*
         * Determine the start time andend time for the resource
         * based on the appropriate Opening/Closing Time table.
         */
        private static void getStartEndDates(DataRow drFlight, Boolean isArrival, Model.ResourceTypes resourceType,
                                             NormalTable OCTTable, String lineOpening, String lineClosing,
                                             out DateTime start, out DateTime end)
        {
            getStartEndDates(drFlight, isArrival, resourceType, OCTTable, lineOpening, lineClosing,
                             out start, out end, true);
        }
        private static void getStartEndDates(DataRow drFlight, Boolean isArrival, Model.ResourceTypes resourceType,
                                             NormalTable OCTTable, String lineOpening, String lineClosing,
                                             out DateTime start, out DateTime end, bool useExceptions)
        {
            int oct_val1, oct_val2;
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();

            DateTime tmp = new DateTime();
            String FC = drFlight[GlobalNames.sFPD_A_Column_FlightCategory].ToString();
            String ID = drFlight[GlobalNames.sFPD_A_Column_ID].ToString();
            String Airline = drFlight[GlobalNames.sFPD_A_Column_AirlineCode].ToString();

            String sPrefixe = "";
            if (isArrival)
            {
                sPrefixe = "A_";
                tmp = OverallTools.DataFunctions.toDateTime(drFlight[GlobalNames.sFPD_A_Column_DATE], drFlight[GlobalNames.sFPA_Column_STA]);
            }
            else
            {
                sPrefixe = "D_";
                tmp = OverallTools.DataFunctions.toDateTime(drFlight[GlobalNames.sFPD_A_Column_DATE], drFlight[GlobalNames.sFPD_Column_STD]);
            }

            // if the resource type starts with FB then we must check if
            // there is a First&Bussines exception table
            // i = 1 => we search for a First&Bussines exception table
            int i = 0;
            if (resourceType.ToString().StartsWith(Model.fbExceptionIndicator))
                i = 1;
            Dictionary<String, String> dssOCTimes = new Dictionary<String, String>();
            // according to the useException parameter the OC times are calculated
            // from the exceptions linked to the OC table or  from the Normal OC table            
            if (useExceptions)
                dssOCTimes = ((DataManagement.ExceptionTable)OCTTable).GetInformationsColumns(i, sPrefixe + ID, Airline, FC);
            else
            {
                String primaryKeyColumn = "";
                int indexPrimaryKeyColumn = -1;
                int indexColumnFC = -1;

                if (OCTTable.Table.PrimaryKey.Length == 1)
                    primaryKeyColumn = OCTTable.Table.PrimaryKey[0].ColumnName;
                indexPrimaryKeyColumn = OCTTable.Table.Columns.IndexOf(primaryKeyColumn);
                indexColumnFC = OCTTable.Table.Columns.IndexOf(FC);

                if (indexPrimaryKeyColumn == -1 || indexColumnFC == -1)
                {
                    oct_val1 = 0;
                    oct_val2 = 0;
                    start = DateTime.MaxValue;
                    end = DateTime.MinValue;
                    return;
                }
                foreach (DataRow drRow in OCTTable.Table.Rows)
                    dssOCTimes.Add(drRow[indexPrimaryKeyColumn].ToString(), drRow[indexColumnFC].ToString());
            }
            //basicTaskSegment.leftText += exceptionType;

            // if the opening/closing times can't be determined the procedure stops and returns default values
            if (dssOCTimes == null)
            { //add error message
                oct_val1 = 0;
                oct_val2 = 0;
                start = DateTime.MaxValue;
                end = DateTime.MinValue;
                return;
            }

            Int32.TryParse(dssOCTimes[lineOpening], out oct_val1);
            Int32.TryParse(dssOCTimes[lineClosing], out oct_val2);
            if (isArrival)
            {
                startTime = tmp.AddMinutes(oct_val1);
                endTime = tmp.AddMinutes(oct_val2);
            }
            else
            {
                startTime = tmp.AddMinutes(-oct_val1);
                endTime = tmp.AddMinutes(-oct_val2);
            }
            if (startTime > endTime)
            {
                tmp = endTime;
                endTime = startTime;
                startTime = tmp;
            }
            start = startTime;
            end = endTime;
        }

        public static void updateFPTable(DataTable dt, String flightId, String resourceNb, String resourceType, int terminalNb, PAX2SIM p2s)
        {
            #region get indexes for columns            
            int indexColumnBaggageClaimNb = dt.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
            int indexColumnBaggageClaimTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim);

            int indexColumnInfeedTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
            int indexColumnTransferInfeedNb = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);

            int indexColumnArrivalGateTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int indexColumnArrivalGateNb = dt.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate);

            int indexColumnBoardingGateTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int indexColumnBoardingGateNb = dt.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);

            int indexColumnParkingTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
            int indexColumnParkingStandNb = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);

            int indexColumnRunwayNb = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay);
            #endregion

            DataRow[] rowToUpdate = dt.Select(GlobalNames.sFPD_A_Column_ID + " = " + flightId);
            if (resourceType.Equals(Model.ResourceTypes.BaggageClaim.ToString())
                && indexColumnBaggageClaimNb != -1 && indexColumnBaggageClaimTerminal != -1)
            {
                rowToUpdate[0][indexColumnBaggageClaimNb] = resourceNb;
                rowToUpdate[0][indexColumnBaggageClaimTerminal] = terminalNb;
            }
            // Arrival infeed and Transfer infeed use the same terminal
            if (resourceType.Equals(Model.ResourceTypes.TransferInfeed.ToString())
                && indexColumnTransferInfeedNb != -1 && indexColumnInfeedTerminal != -1)
            {
                rowToUpdate[0][indexColumnTransferInfeedNb] = resourceNb;
                rowToUpdate[0][indexColumnInfeedTerminal] = terminalNb;
            }
            if (resourceType.Equals(Model.ResourceTypes.ArrivalGate.ToString())
                && indexColumnArrivalGateNb != -1 && indexColumnArrivalGateTerminal != -1)
            {
                rowToUpdate[0][indexColumnArrivalGateNb] = resourceNb;
                rowToUpdate[0][indexColumnArrivalGateTerminal] = terminalNb;
            }
            if (resourceType.Equals(Model.ResourceTypes.BoardingGate.ToString())
                && indexColumnBoardingGateNb != -1 && indexColumnBoardingGateTerminal != -1)
            {
                rowToUpdate[0][indexColumnBoardingGateNb] = resourceNb;
                rowToUpdate[0][indexColumnBoardingGateTerminal] = terminalNb;
            }
            if (resourceType.Equals(Model.ResourceTypes.ParkingStand.ToString())
                && indexColumnParkingStandNb != -1 && indexColumnParkingTerminal != -1)
            {
                rowToUpdate[0][indexColumnParkingStandNb] = resourceNb;
                rowToUpdate[0][indexColumnParkingTerminal] = terminalNb;
            }
            if (resourceType.Equals(Model.ResourceTypes.Runway.ToString()) && indexColumnRunwayNb != -1)
                rowToUpdate[0][indexColumnRunwayNb] = resourceNb;

            dt.AcceptChanges();
            // the tabled must be flagged as modified 
            // to trigger the refresh on the old text Gantt
            TreeViewTag NodeTag = p2s.CheckCurrentNode();
            String scenarioName = NodeTag.ScenarioName;
            p2s.DonneesEnCours.aEteModifiee(scenarioName, dt.TableName);
        }

        public static void updateFPTable(DataTable dt, String flightId, String resourceNb, String resourceType, String terminalNb, PAX2SIM p2s)
        {
            #region get indexes for columns
            int indexColumnBaggageClaimNb = dt.Columns.IndexOf(GlobalNames.sFPA_Column_ReclaimObject);
            int indexColumnBaggageClaimTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalReclaim);

            int indexColumnInfeedTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
            int indexColumnTransferInfeedNb = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TransferInfeedObject);

            int indexColumnArrivalGateTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int indexColumnArrivalGateNb = dt.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate);

            int indexColumnBoardingGateTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int indexColumnBoardingGateNb = dt.Columns.IndexOf(GlobalNames.sFPD_Column_BoardingGate);

            int indexColumnParkingTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalParking);
            int indexColumnParkingStandNb = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_Parking);

            int indexColumnRunwayNb = dt.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay);
            #endregion

            DataRow[] rowToUpdate = dt.Select(GlobalNames.sFPD_A_Column_ID + " = " + flightId);
            if (resourceType.Equals(Model.ResourceTypes.BaggageClaim.ToString())
                && indexColumnBaggageClaimNb != -1 && indexColumnBaggageClaimTerminal != -1)
            {
                rowToUpdate[0][indexColumnBaggageClaimNb] = resourceNb;
                rowToUpdate[0][indexColumnBaggageClaimTerminal] = terminalNb;
            }
            // Arrival infeed and Transfer infeed use the same terminal
            if (resourceType.Equals(Model.ResourceTypes.TransferInfeed.ToString())
                && indexColumnTransferInfeedNb != -1 && indexColumnInfeedTerminal != -1)
            {
                rowToUpdate[0][indexColumnTransferInfeedNb] = resourceNb;
                rowToUpdate[0][indexColumnInfeedTerminal] = terminalNb;
            }
            if (resourceType.Equals(Model.ResourceTypes.ArrivalGate.ToString())
                && indexColumnArrivalGateNb != -1 && indexColumnArrivalGateTerminal != -1)
            {
                rowToUpdate[0][indexColumnArrivalGateNb] = resourceNb;
                rowToUpdate[0][indexColumnArrivalGateTerminal] = terminalNb;
            }
            if (resourceType.Equals(Model.ResourceTypes.BoardingGate.ToString())
                && indexColumnBoardingGateNb != -1 && indexColumnBoardingGateTerminal != -1)
            {
                rowToUpdate[0][indexColumnBoardingGateNb] = resourceNb;
                rowToUpdate[0][indexColumnBoardingGateTerminal] = terminalNb;
            }
            if (resourceType.Equals(Model.ResourceTypes.ParkingStand.ToString())
                && indexColumnParkingStandNb != -1 && indexColumnParkingTerminal != -1)
            {
                rowToUpdate[0][indexColumnParkingStandNb] = resourceNb;
                rowToUpdate[0][indexColumnParkingTerminal] = terminalNb;
            }
            if (resourceType.Equals(Model.ResourceTypes.Runway.ToString()) && indexColumnRunwayNb != -1)
                rowToUpdate[0][indexColumnRunwayNb] = resourceNb;

            dt.AcceptChanges();
            // the tabled must be flagged as modified 
            // to trigger the refresh on the old text Gantt
            TreeViewTag NodeTag = p2s.CheckCurrentNode();
            String scenarioName = NodeTag.ScenarioName;
            p2s.DonneesEnCours.aEteModifiee(scenarioName, dt.TableName);
        }

        public static void updateFPTableForRangeResources(DataTable dt, String flightId, String resourceFirstDeskNb,
            String resourceLastDeskNb, String resourceClassType, String resourceType, String resourceClassToUpdate,
            int terminalNb, PAX2SIM p2s)
        {
            #region get indexes for columns
            int indexColumnCheckInTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
            int indexColumnEcoCheckInStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
            int indexColumnEcoCheckInEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
            int indexColumnFBCheckInStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
            int indexColumnFBCheckInEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);

            int indexColumnEcoBagDropStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
            int indexColumnEcoBagDropEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
            int indexColumnFBBagDropStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
            int indexColumnFBBagDropEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);

            int indexColumnMakeUpTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup);
            int indexColumnEcoMakeUpStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
            int indexColumnEcoMakeUpEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
            int indexColumnFBMakeUpStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
            int indexColumnFBMakeUpEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);

            int indexColumnInfeedTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
            int indexColumnArrivalInfeedStart = dt.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject);
            int indexColumnArrivalInfeedEnd = dt.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject);

            #endregion

            DataRow[] rowToUpdate = dt.Select(GlobalNames.sFPD_A_Column_ID + " = " + flightId);

            #region Check In
            if (resourceType.Equals(Model.ResourceTypes.FBCheck_In.ToString()) && indexColumnCheckInTerminal != -1)
            {
                if (resourceClassToUpdate.Equals(Model.ECO.ToString().Trim()) && indexColumnEcoCheckInStart != -1 && indexColumnEcoCheckInEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoCheckInStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoCheckInEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.FB.ToString().Trim()) && indexColumnFBCheckInStart != -1 && indexColumnFBCheckInEnd != -1)
                {
                    rowToUpdate[0][indexColumnFBCheckInStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBCheckInEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.EcoFB.ToString()) && indexColumnEcoCheckInStart != -1 && indexColumnEcoCheckInEnd != -1
                    && indexColumnFBCheckInStart != -1 && indexColumnFBCheckInEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoCheckInStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoCheckInEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnFBCheckInStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBCheckInEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
            }
            #endregion

            #region Bagg drop
            //Bag drop and Check In use the same terminal
            if (resourceType.Equals(Model.ResourceTypes.FBBagDrop.ToString()) && indexColumnCheckInTerminal != -1)
            {
                if (resourceClassToUpdate.Equals(Model.ECO.ToString().Trim()) && indexColumnEcoBagDropStart != -1 && indexColumnEcoBagDropEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoBagDropStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoBagDropEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.FB.ToString().Trim()) && indexColumnFBBagDropStart != -1 && indexColumnFBBagDropEnd != -1)
                {
                    rowToUpdate[0][indexColumnFBBagDropStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBBagDropEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.EcoFB.ToString()) && indexColumnEcoBagDropStart != -1 && indexColumnEcoBagDropEnd != -1
                    && indexColumnFBBagDropStart != -1 && indexColumnFBBagDropEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoBagDropStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoBagDropEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnFBBagDropStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBBagDropEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
            }
            #endregion

            #region MakeUo
            if (resourceType.Equals(Model.ResourceTypes.FBMakeUp.ToString()) && indexColumnMakeUpTerminal != -1)
            {
                if (resourceClassToUpdate.Equals(Model.ECO.ToString().Trim()) && indexColumnEcoMakeUpStart != -1 && indexColumnEcoMakeUpEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoMakeUpStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoMakeUpEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnMakeUpTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.FB.ToString().Trim()) && indexColumnFBMakeUpStart != -1 && indexColumnFBMakeUpEnd != -1)
                {
                    rowToUpdate[0][indexColumnFBMakeUpStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBMakeUpEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnMakeUpTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.EcoFB.ToString()) && indexColumnEcoMakeUpStart != -1 && indexColumnEcoMakeUpEnd != -1
                    && indexColumnFBMakeUpStart != -1 && indexColumnFBMakeUpEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoMakeUpStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoMakeUpEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnFBMakeUpStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBMakeUpEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnMakeUpTerminal] = terminalNb;
                }
            }
            #endregion

            #region Arrival Infeeds (range but not Eco/FB)
            // Arrival infeed and Transfer infeed use the same terminal
            if (resourceType.Equals(Model.ResourceTypes.ArrivalInfeed.ToString()) && indexColumnInfeedTerminal != -1)
            {
                rowToUpdate[0][indexColumnArrivalInfeedStart] = resourceFirstDeskNb;
                rowToUpdate[0][indexColumnArrivalInfeedEnd] = resourceLastDeskNb;
                rowToUpdate[0][indexColumnInfeedTerminal] = terminalNb;
            }
            #endregion

            dt.AcceptChanges();
            // the tabled must be flagged as modified 
            // to trigger the refresh on the old text Gantt
            TreeViewTag NodeTag = p2s.CheckCurrentNode();
            String scenarioName = NodeTag.ScenarioName;
            p2s.DonneesEnCours.aEteModifiee(scenarioName, dt.TableName);
        }

        public static void updateFPTableForRangeResources(DataTable dt, String flightId, String resourceFirstDeskNb,
            String resourceLastDeskNb, String resourceClassType, String resourceType, String resourceClassToUpdate,
            String terminalNb, PAX2SIM p2s)
        {
            #region get indexes for columns
            int indexColumnCheckInTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
            int indexColumnEcoCheckInStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
            int indexColumnEcoCheckInEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_End);
            int indexColumnFBCheckInStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_Start);
            int indexColumnFBCheckInEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_CI_End);

            int indexColumnEcoBagDropStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_Start);
            int indexColumnEcoBagDropEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Drop_End);
            int indexColumnFBBagDropStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_Start);
            int indexColumnFBBagDropEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_FB_Drop_End);

            int indexColumnMakeUpTerminal = dt.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalMup);
            int indexColumnEcoMakeUpStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_Start);
            int indexColumnEcoMakeUpEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_Mup_End);
            int indexColumnFBMakeUpStart = dt.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_Start);
            int indexColumnFBMakeUpEnd = dt.Columns.IndexOf(GlobalNames.sFPD_Column_First_Mup_End);

            int indexColumnInfeedTerminal = dt.Columns.IndexOf(GlobalNames.sFPA_Column_TerminalInfeedObject);
            int indexColumnArrivalInfeedStart = dt.Columns.IndexOf(GlobalNames.sFPA_Column_StartArrivalInfeedObject);
            int indexColumnArrivalInfeedEnd = dt.Columns.IndexOf(GlobalNames.sFPA_Column_EndArrivalInfeedObject);

            #endregion

            DataRow[] rowToUpdate = dt.Select(GlobalNames.sFPD_A_Column_ID + " = " + flightId);

            #region Check In
            if (resourceType.Equals(Model.ResourceTypes.FBCheck_In.ToString()) && indexColumnCheckInTerminal != -1)
            {
                if (resourceClassToUpdate.Equals(Model.ECO.ToString().Trim()) && indexColumnEcoCheckInStart != -1 && indexColumnEcoCheckInEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoCheckInStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoCheckInEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.FB.ToString().Trim()) && indexColumnFBCheckInStart != -1 && indexColumnFBCheckInEnd != -1)
                {
                    rowToUpdate[0][indexColumnFBCheckInStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBCheckInEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.EcoFB.ToString()) && indexColumnEcoCheckInStart != -1 && indexColumnEcoCheckInEnd != -1
                    && indexColumnFBCheckInStart != -1 && indexColumnFBCheckInEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoCheckInStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoCheckInEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnFBCheckInStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBCheckInEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
            }
            #endregion

            #region Bagg drop
            //Bag drop and Check In use the same terminal
            if (resourceType.Equals(Model.ResourceTypes.FBBagDrop.ToString()) && indexColumnCheckInTerminal != -1)
            {
                if (resourceClassToUpdate.Equals(Model.ECO.ToString().Trim()) && indexColumnEcoBagDropStart != -1 && indexColumnEcoBagDropEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoBagDropStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoBagDropEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.FB.ToString().Trim()) && indexColumnFBBagDropStart != -1 && indexColumnFBBagDropEnd != -1)
                {
                    rowToUpdate[0][indexColumnFBBagDropStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBBagDropEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.EcoFB.ToString()) && indexColumnEcoBagDropStart != -1 && indexColumnEcoBagDropEnd != -1
                    && indexColumnFBBagDropStart != -1 && indexColumnFBBagDropEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoBagDropStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoBagDropEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnFBBagDropStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBBagDropEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnCheckInTerminal] = terminalNb;
                }
            }
            #endregion

            #region MakeUo
            if (resourceType.Equals(Model.ResourceTypes.FBMakeUp.ToString()) && indexColumnMakeUpTerminal != -1)
            {
                if (resourceClassToUpdate.Equals(Model.ECO.ToString().Trim()) && indexColumnEcoMakeUpStart != -1 && indexColumnEcoMakeUpEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoMakeUpStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoMakeUpEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnMakeUpTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.FB.ToString().Trim()) && indexColumnFBMakeUpStart != -1 && indexColumnFBMakeUpEnd != -1)
                {
                    rowToUpdate[0][indexColumnFBMakeUpStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBMakeUpEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnMakeUpTerminal] = terminalNb;
                }
                if (resourceClassToUpdate.Equals(Model.EcoFB.ToString()) && indexColumnEcoMakeUpStart != -1 && indexColumnEcoMakeUpEnd != -1
                    && indexColumnFBMakeUpStart != -1 && indexColumnFBMakeUpEnd != -1)
                {
                    rowToUpdate[0][indexColumnEcoMakeUpStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnEcoMakeUpEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnFBMakeUpStart] = resourceFirstDeskNb;
                    rowToUpdate[0][indexColumnFBMakeUpEnd] = resourceLastDeskNb;
                    rowToUpdate[0][indexColumnMakeUpTerminal] = terminalNb;
                }
            }
            #endregion

            #region Arrival Infeeds (range but not Eco/FB)
            // Arrival infeed and Transfer infeed use the same terminal
            if (resourceType.Equals(Model.ResourceTypes.ArrivalInfeed.ToString()) && indexColumnInfeedTerminal != -1)
            {
                rowToUpdate[0][indexColumnArrivalInfeedStart] = resourceFirstDeskNb;
                rowToUpdate[0][indexColumnArrivalInfeedEnd] = resourceLastDeskNb;
                rowToUpdate[0][indexColumnInfeedTerminal] = terminalNb;
            }
            #endregion

            dt.AcceptChanges();
            // the tabled must be flagged as modified 
            // to trigger the refresh on the old text Gantt
            TreeViewTag NodeTag = p2s.CheckCurrentNode();
            String scenarioName = NodeTag.ScenarioName;
            p2s.DonneesEnCours.aEteModifiee(scenarioName, dt.TableName);
        }

        // >> Task #12808 Pax2Sim - allocation Liege
        public static void updateFPITable(DataTable dt, String flightId, String firstResourceNb, String lastResourceNb,
            String firstResourceNbBeforeSaveAsString, String lastResourceNbBeforeSaveAsString, String resourceType, String terminalNb, PAX2SIM p2s)
        {
            #region get indexes for columns
            int indexColumnFirstDeskNb = -1;
            int indexColumnLastDeskNb = -1;
            int indexColumnNbDesksUsed = dt.Columns.IndexOf(GlobalNames.FPI_Column_NbOfResourcesUsed);
            int indexColumnTerminalNb = -1;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn column = dt.Columns[i];
                if (column.ColumnName.Contains("First Desk"))
                {
                    indexColumnFirstDeskNb = i;
                }
                else if (column.ColumnName.Contains("Last Desk"))
                {
                    indexColumnLastDeskNb = i;
                }
                else if (column.ColumnName.Contains("Terminal Nb"))
                {
                    indexColumnTerminalNb = i;
                }
            }

            if (indexColumnFirstDeskNb == -1 || indexColumnLastDeskNb == -1
                || indexColumnNbDesksUsed == -1 || indexColumnTerminalNb == -1)
            {
                return;
            }
            #endregion

            DataRow[] rowsToUpdate = dt.Select(GlobalNames.sFPD_A_Column_ID + " = " + flightId);
            DataRow rowToUpdate = null;
            if (rowsToUpdate.Length == 1)
                rowToUpdate = rowsToUpdate[0];
            else if (rowsToUpdate.Length > 1)
            {
                int firstDesk = -1;
                int lastDesk = -1;
                int firstDeskBeforeSave = -1;
                int lastDeskBeforeSave = -1;
                if (Int32.TryParse(firstResourceNbBeforeSaveAsString, out firstDeskBeforeSave)
                    && Int32.TryParse(lastResourceNbBeforeSaveAsString, out lastDeskBeforeSave))
                {
                    foreach (DataRow row in rowsToUpdate)
                    {
                        if (row[indexColumnFirstDeskNb] != null && row[indexColumnLastDeskNb] != null
                            && Int32.TryParse(row[indexColumnFirstDeskNb].ToString(), out firstDesk)
                            && Int32.TryParse(row[indexColumnLastDeskNb].ToString(), out lastDesk)
                            && firstDesk == firstDeskBeforeSave && lastDesk == lastDeskBeforeSave)
                        {
                            rowToUpdate = row;
                            break;
                        }
                    }
                }
            }

            if (rowToUpdate != null)
            {
                if (indexColumnFirstDeskNb != -1)
                    rowToUpdate[indexColumnFirstDeskNb] = firstResourceNb;
                if (indexColumnLastDeskNb != -1)
                {
                    if (lastResourceNb != null && lastResourceNb != "")
                        rowToUpdate[indexColumnLastDeskNb] = lastResourceNb;
                    else
                        rowToUpdate[indexColumnLastDeskNb] = firstResourceNb;
                }
                if (indexColumnNbDesksUsed != -1)
                {
                    int fromDesk = -1;
                    int toDesk = -1;
                    if (Int32.TryParse(firstResourceNb, out fromDesk)
                        && Int32.TryParse(lastResourceNb, out toDesk))
                    {
                        rowToUpdate[indexColumnNbDesksUsed] = toDesk - fromDesk + 1;
                    }
                    else
                        rowToUpdate[indexColumnNbDesksUsed] = 1;
                }
                if (indexColumnTerminalNb != -1)
                    rowToUpdate[indexColumnTerminalNb] = terminalNb;

                dt.AcceptChanges();
            }
            /*
            // the tabled must be flagged as modified 
            // to trigger the refresh on the old text Gantt
            TreeViewTag NodeTag = p2s.CheckCurrentNode();
            String scenarioName = NodeTag.ScenarioName;
            p2s.DonneesEnCours.aEteModifiee(scenarioName, dt.TableName);
             */
        }
        // << Task #12808 Pax2Sim - allocation Liege

        // << Task #6386 Itinerary process
        public static List<ItineraryData> getItineraryDataByType(PAX2SIM pax2sim, String itineraryDataType)
        {
            List<ItineraryData> itineraryDataList = new List<ItineraryData>();

            switch (itineraryDataType)
            {
                case Model.GROUP_OBJECT_TYPE:
                    {
                        itineraryDataList = getGroupObjectListForItinerary(pax2sim);
                    }
                    break;
                case Model.SCENARIO_OBJECT_TYPE:
                    {
                        itineraryDataList = getScenarioListForItinerary(pax2sim);
                    }
                    break;
                case Model.CHART_OBJECT_TYPE:
                    {
                        itineraryDataList = getChartListForItinerary(pax2sim);
                    }
                    break;
                default:
                    break;
            }

            return itineraryDataList;
        }

        private static List<ItineraryData> getGroupObjectListForItinerary(PAX2SIM pax2sim)
        {
            List<ItineraryData> groupObjectListForItinerary = new List<ItineraryData>();
            ArrayList groupNames = pax2sim.DonneesEnCours.getAllGroups();
            ArrayList groupDescriptions = pax2sim.DonneesEnCours.getAllGroupsDescriptions();
            //The information about ProcessTimes and Capacity for each Group are retrieved into this dictionaries
            Dictionary<String, String> allGroupsPaxProcessTimesDictionary = ProcessFlowServices.getAllGroupsPaxProcessTimes(pax2sim);
            Dictionary<String, String> allGroupsBagProcessTimesDictionary = ProcessFlowServices.getAllGroupsBaggProcessTimes(pax2sim);
            Dictionary<String, String> allGroupsCapacitiesDictionary = ProcessFlowServices.getAllGroupsCapacities(pax2sim);
            // << Task #8789 Pax2Sim - ProcessFlow - update Group details
            //Information about Groups' Waiting time before queueing distribution
            Dictionary<String, String> allGroupsDelayTimeDistributionDictionary = ProcessFlowServices
                .getAllGroupsDelayTimeDistributions(pax2sim);
            // >> Task #8789 Pax2Sim - ProcessFlow - update Group details

            // if all the descriptions exist => we send also the descriptions - to be checked
            if (groupNames.Count == groupDescriptions.Count)
            {
                for (int i = 0; i < groupNames.Count; i++)
                {
                    ItineraryData groupForItinerary = new ItineraryData();
                    groupForItinerary.name = groupNames[i].ToString();
                    groupForItinerary.description = groupDescriptions[i].ToString();
                    groupForItinerary.itineraryDataType = Model.GROUP_OBJECT_TYPE;
                    //adding the information about process times and capacities
                    addProcessTimeAndCapacityInformationToGroup(groupForItinerary,
                        allGroupsPaxProcessTimesDictionary, Model.PAX_PROCESS_TIME_DATA);
                    addProcessTimeAndCapacityInformationToGroup(groupForItinerary,
                        allGroupsBagProcessTimesDictionary, Model.BAG_PROCESS_TIME_DATA);
                    addProcessTimeAndCapacityInformationToGroup(groupForItinerary,
                        allGroupsCapacitiesDictionary, Model.CAPACITY_DATA);
                    // << Task #8789 Pax2Sim - ProcessFlow - update Group details
                    addProcessTimeAndCapacityInformationToGroup(groupForItinerary,
                        allGroupsDelayTimeDistributionDictionary, Model.DELAY_TIME_DISTRIBUTION_DATA);
                    // >> Task #8789 Pax2Sim - ProcessFlow - update Group details
                    groupObjectListForItinerary.Add(groupForItinerary);
                }
            }
            else
            {
                for (int i = 0; i < groupNames.Count; i++)
                {
                    ItineraryData groupForItinerary = new ItineraryData();
                    groupForItinerary.name = groupNames[i].ToString();
                    groupForItinerary.itineraryDataType = Model.GROUP_OBJECT_TYPE;
                    //adding the information about process times and capacities
                    addProcessTimeAndCapacityInformationToGroup(groupForItinerary,
                        allGroupsPaxProcessTimesDictionary, Model.PAX_PROCESS_TIME_DATA);
                    addProcessTimeAndCapacityInformationToGroup(groupForItinerary,
                        allGroupsBagProcessTimesDictionary, Model.BAG_PROCESS_TIME_DATA);
                    addProcessTimeAndCapacityInformationToGroup(groupForItinerary,
                        allGroupsCapacitiesDictionary, Model.CAPACITY_DATA);
                    // << Task #8789 Pax2Sim - ProcessFlow - update Group details
                    addProcessTimeAndCapacityInformationToGroup(groupForItinerary,
                        allGroupsDelayTimeDistributionDictionary, Model.DELAY_TIME_DISTRIBUTION_DATA);
                    // >> Task #8789 Pax2Sim - ProcessFlow - update Group details
                    groupObjectListForItinerary.Add(groupForItinerary);
                }
            }
            return groupObjectListForItinerary;
        }

        private static void addProcessTimeAndCapacityInformationToGroup(ItineraryData group,
            Dictionary<String, String> dataSource, String dataAdded)
        {
            String groupInformation = "";
            if (dataSource.TryGetValue(group.name, out groupInformation))
            {
                if (dataAdded == Model.CAPACITY_DATA)
                    group.capacity = groupInformation;
                else if (dataAdded == Model.PAX_PROCESS_TIME_DATA)
                    group.paxProcessTimes = groupInformation;
                else if (dataAdded == Model.BAG_PROCESS_TIME_DATA)
                    group.bagProcessTimes = groupInformation;
                else if (dataAdded == Model.DELAY_TIME_DISTRIBUTION_DATA)   // << Task #8789 Pax2Sim - ProcessFlow - update Group details
                    group.delayTimeDistribution = groupInformation;
            }
        }

        private static List<ItineraryData> getScenarioListForItinerary(PAX2SIM pax2sim)
        {
            List<ItineraryData> scenarioListForItinerary = new List<ItineraryData>();
            List<String> scenarioNamesList = pax2sim.DonneesEnCours.getScenariosReadyToSimulate();

            for (int i = 0; i < scenarioNamesList.Count; i++)
            {
                ItineraryData scenarioForItinerary = new ItineraryData();
                scenarioForItinerary.name = scenarioNamesList[i];
                scenarioForItinerary.itineraryDataType = Model.SCENARIO_OBJECT_TYPE;
                scenarioListForItinerary.Add(scenarioForItinerary);
            }
            return scenarioListForItinerary;
        }

        private static List<ItineraryData> getChartListForItinerary(PAX2SIM pax2sim)
        {
            List<ItineraryData> chartListForItinerary = new List<ItineraryData>();
            List<String> chartNamesList = pax2sim.DonneesEnCours.GetGraphicFilter();

            for (int i = 0; i < chartNamesList.Count; i++)
            {
                ItineraryData chartForItinerary = new ItineraryData();
                chartForItinerary.name = chartNamesList[i];
                chartForItinerary.itineraryDataType = Model.CHART_OBJECT_TYPE;
                chartListForItinerary.Add(chartForItinerary);
            }
            return chartListForItinerary;
        }

        /**
         * Converts the given list of itinerary objects 
         * into an xml representation.
         */
        internal static String serializeItineraryObjectsForXml(List<ItineraryData> itineraryList)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(itineraryList.GetType());
            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    serializer.Serialize(writer, itineraryList);
                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
                return "";
            }
        }

        internal static String serializeItineraryConnectionsForXml(List<ItineraryConnection> itineraryList)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(itineraryList.GetType());
            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    serializer.Serialize(writer, itineraryList);
                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
                return "";
            }
        }
        // >> Task #6386 Itinerary process

        // << Task #8819 Pax2Sim - ProcessFlow - save Details parameters
        internal static String serializeShowDetailsParametersForXml(List<ProcessFlowParameters> parametersList)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer
                    = new System.Xml.Serialization.XmlSerializer(parametersList.GetType());
                using (StringWriter writer = new StringWriter())
                {
                    try
                    {
                        serializer.Serialize(writer, parametersList);
                        return writer.GetStringBuilder().ToString();
                    }
                    catch (Exception ex)
                    {
                        writer.Close();
                        writer.Dispose();
                        System.Windows.Forms.MessageBox
                            .Show(ex.Message + "\n" + ex.StackTrace);
                    }
                    return "";
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox
                               .Show(e.Message + "\n" + e.StackTrace);
                return "";

            }
        }
        // >> Task #8819 Pax2Sim - ProcessFlow - save Details parameters

        // >> Task #10282 Process Flow - Set target option
        internal static String serializeTargetAchievedListForXml(List<Target> targetAchievedList)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer
                    = new System.Xml.Serialization.XmlSerializer(targetAchievedList.GetType());
                using (StringWriter writer = new StringWriter())
                {
                    try
                    {
                        serializer.Serialize(writer, targetAchievedList);
                        return writer.GetStringBuilder().ToString();
                    }
                    catch (Exception ex)
                    {
                        writer.Close();
                        writer.Dispose();
                        System.Windows.Forms.MessageBox
                            .Show(ex.Message + "\n" + ex.StackTrace);
                    }
                    return "";
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox
                               .Show(e.Message + "\n" + e.StackTrace);
                return "";
            }
        }
        // << Task #10282 Process Flow - Set target option
    }
}
