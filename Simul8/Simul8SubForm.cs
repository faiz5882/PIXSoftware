using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SIMCORE_TOOL.Simul8
{
    public partial class Simul8SubForm : Form
    {

        #region Variables de la classe.
        SIMUL8.S8Simulation S8Application_;
        String sSubWindowName_;

        bool bLinkMode;

        DateTime dtDebut;
        int iCurrentZoom;
        Image iCurrentImage;
        Cursor cOldCursor;
        Cursor cCreateLinksCursor;
        String sCurrentObjectToDrop;
        //The current project Name
        String ProjectName;

        ToolStripComboBox cb_Zoom;
        ToolStripLabel ctrlLabel;

        const int Simul8Width = 4096;
        const int Simul8Height = 3072;

        String sModelPath = "";

        #endregion

        #region Delegate used to notify the parent of action that are happening.
        /// <summary>
        /// The delegate called when the run is finished.
        /// </summary>
        internal delegate void EndRun();
        EndRun EndRunFunction;

        /// <summary>
        /// The delegate called when an object in Simul8 is double clicked.
        /// </summary>
        /// <param name="sName">Name of the object</param>
        internal delegate void Object_DoubleClick(String sName);
        Object_DoubleClick Object_DoubleClickFunction;

        /// <summary>
        /// Delegate that will notify the fact that a component had been dropped in Simul8
        /// </summary>
        /// <param name="sName">Name of the object</param>
        internal delegate void Object_Dropped(String sName);
        Object_Dropped Object_DroppedFunction;

        /// <summary>
        /// Delegate that will notify the fact that the drop of a component had been cancelled.
        /// </summary>
        /// <param name="sName">Name of the object</param>
        internal delegate void Object_CancelDropped(String sName);
        Object_CancelDropped Object_CancelDroppedFunction;

        /// <summary>
        /// Delegate that will permit to find the first desk index for the given group.
        /// </summary>
        /// <param name="sGroupeName"></param>
        /// <returns></returns>
        internal delegate int GetFirstStationForGroup(int iTerminal, int iTypeGroupe, int iIndexGroupe);
        GetFirstStationForGroup GetFirstStationForGroupFunction;

        /// <summary>
        /// Function that will permit to find the last desk index for the given group.
        /// </summary>
        /// <param name="sGroupeName"></param>
        /// <returns></returns>
        internal delegate int GetLastStationForGroup(int iTerminal, int iTypeGroupe, int iIndexGroupe);
        GetLastStationForGroup GetLastStationForGroupFunction;

        /// <summary>
        /// Function called to know what group the current station belongs to.
        /// </summary>
        /// <param name="sStationName">The name of the station</param>
        /// <returns>The group.</returns>
        internal delegate String getGroupNameForStation(String sStationName);
        getGroupNameForStation getGroupNameForStationFunction;
        #endregion

        #region Classes

        internal class Simul8Object
        {
            internal String sGroupeName;
            internal String sDescription;
            internal String sDisplayedName;
            internal Image iSourceImage;
            internal Point pLocation;
            internal Simul8Object(String sGroupeName_,
                            String sDescription_,
                            String sDisplayedName_,
                            Image iSourceImage_,
                            Point pLocation_)
            {
                sGroupeName = sGroupeName_;
                sDescription = sDescription_;
                sDisplayedName = sDisplayedName_;
                iSourceImage = iSourceImage_;
                pLocation = pLocation_;
            }
        }
        #endregion

        #region Constantes

        static String sGenerator = "Pax Generator";
        static String sGeneratorName = "P2S__Pax_Generator.OS8";



        static String sCheckInOS8 = "P2S_Pax1_CheckIn.OS8";
        static String sPassportOS8 = "P2S_Pax2_Passport.OS8";
        static String sSecurityOS8 = "P2S_Pax3_Security.OS8";
        static String sArrivalGateOS8 = "P2S_Pax4_ArrivalGate.OS8";
        static String sReclaimOS8 = "P2S_Pax5_Reclaim.OS8";
        static String sTransferDeskOS8 = "P2S_Pax6_TransferDesk.OS8";
        static String sBoardingGateOS8 = "P2S_Pax8_BoardingGate.OS8";


        static String sPaxInOutOS8 = "P2S_Pax_InOut.OS8";
        static String sParkingStandOS8 = "P2S_ParkingStand.OS8";
        static String sGenericOS8 = "P2S_Generic.OS8";





        /// <summary>
        /// The tempory image name for the Simul8 exchange
        /// </summary>
        static String sS8Image = "S8Icone.bmp";
        #endregion

        #region Accesseurs
        /// <summary>
        /// Get or set the sub window name that is shown in the current SubForm.
        /// </summary>
        private String SubWindowName
        {
            get
            {
                return sSubWindowName_;
            }
            set
            {
                sSubWindowName_ = value;
            }
        }
        /// <summary>
        /// Get the Simul8 application instance that is linked to the current subWindow.
        /// </summary>
        private SIMUL8.S8Simulation S8Application
        {
            get
            {
                if (S8Application_ == null)
                {
                    S8Application_ = new SIMUL8.S8Simulation();
                    sSubWindowName_ = "";
                    IntializeSimul8Application();
                    DropGenerator();
                    }
                return S8Application_;
            }
        }

        /// <summary>
        /// Get or set the simulation speed in the Simul8 model.
        /// </summary>
        internal int SimulationSpeed
        {
            get
            {
                return S8Application.SimSpeed;
            }
            set
            {
                S8Application.SimSpeed = value;
                p_Simul8.Invalidate();
                RefreshSimul8();
            }
        }

        /// <summary>
        /// Get or set the zoom in the Simul8 model.
        /// </summary>
        internal int Zoom
        {
            get
            {
                return S8Application.ZoomPercent;
            }
            set
            {
                //We set the zoom to the Simul8 window.
                S8Application.ZoomPercent = value;
                iCurrentZoom = value;
                RecalculateScrollBar();
                p_Simul8.Invalidate();
                RefreshSimul8();
            }
        }

        internal void RecalculateScrollBar()
        {
            iCurrentZoom = S8Application.ZoomPercent;
            //The values for the scroll inside Simul8
            int ivalue, i2Value;

            //Set the active window in Simul8 to the current one
            S8Application.SetActiveSimulationWindow(SubWindowName);

            //Get the scroll position in Simul8
            S8Application.GetScrollPosition(out ivalue, out i2Value);

            if (ivalue > hScrollBar1.Maximum)
            {
                hScrollBar1.Maximum = (Simul8Height * iCurrentZoom / 100);
                hScrollBar1.Value = ivalue;
            }
            else
            {
                hScrollBar1.Value = ivalue;
                hScrollBar1.Maximum = (Simul8Height * iCurrentZoom / 100);
            }
            if (i2Value > vScrollBar1.Maximum)
            {
                vScrollBar1.Maximum = (Simul8Width * iCurrentZoom / 100);
                vScrollBar1.Value = i2Value;
            }
            else
            {
                vScrollBar1.Value = i2Value;
                vScrollBar1.Maximum = (Simul8Width * iCurrentZoom / 100);
            }
        }

        /// <summary>
        /// The different state that the Simul8 model can be in.
        /// </summary>
        internal enum SimulationStat { Running, Stopped, Reseted};

        /// <summary>
        /// Get the state of the simulation.
        /// </summary>
        internal SimulationStat SimulationState
        {
            get
            {
                if (S8Application.SIMUL8Mode == 1)
                    return SimulationStat.Reseted;
                if (S8Application.SIMUL8Mode == 2)
                    return SimulationStat.Running;
                return SimulationStat.Stopped;
            }
        }

        /// <summary>
        /// Stop the simulation.
        /// </summary>
        internal void StopSimulation()
        {
            S8Application.StopSim();
        }

        /// <summary>
        /// Reset the simulation with the random stream given
        /// </summary>
        /// <param name="RandomNumberStream">Random stream to use to reset the simulation.</param>
        internal void ResetSimulation(int RandomNumberStream)
        {
            S8Application.ResetSim(RandomNumberStream);
            callSetVariable(S8Application, "VTX_ModelPath", "\"" + ModelPath + "\"");
            
            p_Simul8.Invalidate();
            RefreshSimul8();
        }

        /// <summary>
        /// Run the simulation for the given amount of time
        /// </summary>
        /// <param name="EndTime">Amount of time for the simulation (by default given in seconds.</param>
        internal void RunSimulation(Double EndTime)
        {
            //We start the simulation.
            S8Application.RunSim(EndTime);
        }

        /// <summary>
        /// Function to open a project in the Simul8 panel.
        /// </summary>
        /// <param name="ProjectName">Name and path of the Simul8 project. (Can be "" to open a blank project)</param>
        /// <returns>Return a boolean that indicates if Simul8 had opened the project</returns>
        internal bool OpenSimul8Project(String ProjectName)
        {
            //We check that the project file exist, Or that it is a blank project.
            if (((ProjectName != "") && (ProjectName != null)) &&(!System.IO.File.Exists(ProjectName)))
                return false;

            //We try to open the project. (To be sure that if there is a problem, the software will not be affected).
            try
            {
                //We close first the old project.
                S8Application.Close();

                //If the project name is not null, we try to open the Simul8 project.
                if((ProjectName!="") && (ProjectName != null))
                    S8Application.Open(ProjectName);

                //We set the handle where Simul8 must open the new project.
                S8Application.set_WindowHandle(SubWindowName, (int)p_Simul8.Handle);

                //DropGenerator();
                //We force Simul8 to paint his window.
                p_Simul8.Invalidate();
                RefreshSimul8();
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile(DateTime.Now.ToString() + " : Unable to open a Simul8 project : " + e.Message);
                //Unable to open the project.
                return false;
            }
            return true;
        }

        /// <summary>
        /// Allow the use to show of hide the links between the objects.
        /// </summary>
        internal bool ShowLinks
        {
            get
            {
                return S8Application.RouteArrowsVisible;
            }
            set
            {
                S8Application.RouteArrowsVisible = value;
                p_Simul8.Invalidate();
                RefreshSimul8();

            }
        }

        internal bool LinkMode
        {
            get
            {
                return bLinkMode;
            }
            set
            {
                DropComponent("", null);
                bLinkMode = value;
                if (value)
                {
                    if (cCreateLinksCursor == null)
                    {
                        Image iImg = global::SIMCORE_TOOL.Properties.Resources.CreateLink3;
                        cCreateLinksCursor = OverallTools.FonctionUtiles.CreateCursor(iImg, iImg.Width, 0);
                    }
                    p_Simul8.Cursor = cCreateLinksCursor;
                }
                else
                    p_Simul8.Cursor = cOldCursor;
            }
        }
        /// <summary>
        /// Function that permits to refresh the display of the Simul8 Area (For Windows 7 users )
        /// There were a problem with the invalidate function, so that function had been written to
        /// refresh the display. It only send the mouse down and up signals to Simul8.
        /// </summary>
        internal void RefreshSimul8()
        {
            if (S8Application == null)
                return;
            //The fact to send the signal of mouse down and mouse up make Simul8 to
            //redraw it window. So as on Windows 7 the refresh is not working. That solution 
            //had been adopted.
            S8Application.simleftmousedown(false,
                                false,
                                false,
                                0,
                                0,
                                SubWindowName);
            S8Application.simleftmouseup(false,
                                false,
                                false,
                                0,
                                0,
                                SubWindowName);
        }

        /// <summary>
        /// Permits to put a handler for the zoom on a ComboBox. Every time the zoom will change in
        /// that window, the ComboBox will receive the good value too.
        /// </summary>
        internal ToolStripComboBox ZoomHandler
        {
            set
            {
                cb_Zoom = value;
            }
        }

        /// <summary>
        /// Delegate that allows the window to call a special function when the run is ended.
        /// </summary>
        internal EndRun EndRunDelegate
        {
            set
            {
                EndRunFunction = value;
            }
        }

        /// <summary>
        /// Delegate that allows the window to call a special function when a double click occurs on an object.
        /// </summary>
        internal Object_DoubleClick Object_DoubleClickDelegate
        {
            set
            {
                Object_DoubleClickFunction = value;
            }
        }

        /// <summary>
        /// Delegate that will notify the fact that a component had been dropped in Simul8
        /// </summary>
        internal Object_Dropped Object_DroppedDelegate
        {
            set
            {
                Object_DroppedFunction = value;
            }
        }
        
        /// <summary>
        /// Delegate that will notify the fact that the drop of a component had been cancelled.
        /// </summary>
        internal Object_CancelDropped Object_CancelDroppedDelegate
        {
            set
            {
                Object_CancelDroppedFunction = value;
            }
        }

        /// <summary>
        /// Function called to get the first station index in the groupe.
        /// </summary>
        internal GetFirstStationForGroup GetFirstStationForGroupDelegate
        {
            set
            {
                GetFirstStationForGroupFunction = value;
            }
        }

        /// <summary>
        /// Function called to get the last station index in the groupe.
        /// </summary>
        internal GetLastStationForGroup GetLastStationForGroupDelegate
        {
            set
            {
                GetLastStationForGroupFunction = value;
            }
        }

        /// <summary>
        /// Set the delegate that will permits to find what group the station belongs to.
        /// </summary>
        internal getGroupNameForStation getGroupNameForStationDelegate
        {
            set
            {
                getGroupNameForStationFunction = value;
            }
        }

        internal Int32 RandomSeed
        {
            set
            {
                S8Application.ExecVL("Set Current Random Number Stream Set    " + value.ToString());
            }
        }

        internal String ModelPath
        {
            get
            {
                if (sModelPath == "")
                    sModelPath = OverallTools.ExternFunctions.getTempDirectoryForPax2sim();
                return sModelPath;
            }
            set
            {
                sModelPath = value;
            }
        }

        #region Functions and elements to manage the clock of the simulation
        /// <summary>
        /// Permits to put a handler for the clock on a Control. That will update the time in 
        /// the control, every step.
        /// </summary>
        internal ToolStripLabel ClockHandler
        {
            set
            {
                ctrlLabel = value;
            }
        }

        internal float SimulationTimeF
        {
            get
            {
                return S8Application.SimulationTime;
            }
        }

        /// <summary>
        /// Get the simulation time in a String format with dates and time.
        /// </summary>
        internal String SimulationTime
        {
            get
            {
                if (S8Application == null)
                    return dtDebut.ToString();
                float fTime = ConvertTimeInMinutes( S8Application.SimulationTime,TimeUnit);
                return dtDebut.AddMinutes(fTime).ToString();
            }
        }

        /// <summary>
        /// Gets and Sets the value for the start date for the simulation. By default it's 1/1/2000 at 0:00
        /// </summary>
        internal DateTime StartDate
        {
            get
            {
                return dtDebut;
            }
            set
            {
                dtDebut = value;
            }
        }


        internal enum TimeUnits { Seconds, Minutes, Hours, Days };

        /// <summary>
        /// Function that permits to convert a given value (in a certain time Unit), into a number
        /// of minutes. This permits the user to easily convert the Simul8 simulation time into
        /// a number of minutes since the begining of the simulation.
        /// </summary>
        /// <param name="Value">Value in TimeUnit</param>
        /// <param name="timeUnit">TimeUnit of the given value.</param>
        /// <returns>Converted number of minutes for the value.</returns>
        internal static float ConvertTimeInMinutes(float Value, TimeUnits timeUnit)
        {
            float fConverter = 1;
            switch (timeUnit)
            {
                case Simul8SubForm.TimeUnits.Days:
                        fConverter = 1440.0f;
                    break;
                case Simul8SubForm.TimeUnits.Minutes:
                    break;
                case Simul8SubForm.TimeUnits.Hours:
                        fConverter = 60.0f;
                    break;
                case Simul8SubForm.TimeUnits.Seconds:
                        fConverter = 1.0f / 60.0f;
                    break;
                default:
                    break;
            }
            return fConverter * Value;
        }
        /// <summary>
        /// Function that permits to convert a given value (in minutes), into a value in the
        /// TimeUnit given. This permits the user to easily convert the Simul8 simulation time into
        /// a number of minutes since the begining of the simulation.
        /// </summary>
        /// <param name="Value">Value in minutes</param>
        /// <param name="timeUnit">TimeUnit expected for the result.</param>
        /// <returns>Converted value in the TimeUnit.</returns>
        internal static float ConvertMinutesInTime(float Value, TimeUnits timeUnit)
        {
            float fConverter = 1;
            switch (timeUnit)
            {
                case Simul8SubForm.TimeUnits.Days:
                        fConverter = 1.0f / 1440.0f;
                    break;
                case Simul8SubForm.TimeUnits.Minutes:
                    break;
                case Simul8SubForm.TimeUnits.Hours:
                        fConverter = 1.0f / 60.0f;
                    break;
                case Simul8SubForm.TimeUnits.Seconds:
                        fConverter = 60.0f;
                    break;
                default:
                    break;
            }
            return fConverter * Value;
        }
        internal TimeUnits TimeUnit
        {
            get
            {
                int iValue = S8Application.ClockDisplayTimeUnits;
                switch (iValue)
                {
                    case 0:
                        return TimeUnits.Seconds;
                    case 1:
                        return TimeUnits.Minutes;
                    case 2:
                        return TimeUnits.Hours;
                    case 3:
                        return TimeUnits.Days;
                    default:
                        return TimeUnits.Minutes;
                }
            }
            /*set
            {
                switch (value) 
                { 
                    case TimeUnits.Seconds:
                        S8Application.ClockDisplayTimeUnits = 0;
                        break;
                    case TimeUnits.Minutes:
                        S8Application.ClockDisplayTimeUnits = 1;
                        break;
                    case TimeUnits.Hours:
                        S8Application.ClockDisplayTimeUnits = 2;
                        break;
                    case TimeUnits.Days:
                        S8Application.ClockDisplayTimeUnits = 3;
                        break;
                    default:
                        S8Application.ClockDisplayTimeUnits = 1;
                        break;

                }
            }*/
        }

        internal enum TimeFormats { SimpleUnit, TimeOnly, Percent, TimeAndDay_Day, TimeAndDay_DayWeek, TimeAndDay_Date };
        internal TimeFormats TimeFormat
        {
            get
            {
                int iValue = S8Application.ClockDisplayFormat;

                switch (iValue)
                {
                    case 0:
                        return TimeFormats.SimpleUnit;
                    case 1:
                        return TimeFormats.TimeOnly;
                    case 20:
                        return TimeFormats.Percent;
                    case 2:
                        return TimeFormats.TimeAndDay_Day;
                    case 3:
                        return TimeFormats.TimeAndDay_DayWeek;
                    case 8:
                        return TimeFormats.TimeAndDay_Date;
                    default:
                        return TimeFormats.TimeAndDay_DayWeek;
                }
            }/*
            set
            {
                switch (value)
                {
                    case TimeFormats.SimpleUnit:
                        S8Application.ClockDisplayFormat =0;
                        break;
                    case TimeFormats.TimeOnly:
                        S8Application.ClockDisplayFormat =1;
                        break;
                    case TimeFormats.Percent:
                        S8Application.ClockDisplayFormat =20;
                        break;
                    case TimeFormats.TimeAndDay_Day:
                        S8Application.ClockDisplayFormat =2;
                        break;
                    case TimeFormats.TimeAndDay_DayWeek:
                        S8Application.ClockDisplayFormat =3;
                        break;
                    case TimeFormats.TimeAndDay_Date:
                        S8Application.ClockDisplayFormat =8;
                        break;
                    default:
                        S8Application.ClockDisplayFormat =3;
                        break;
                }
            }*/
        }

        internal Boolean DisplayDaysOfWeek
        {
            get
            {
                return S8Application.ClockDisplayDaysOfWeek;
            }/*
            set
            {
                S8Application.ClockDisplayDaysOfWeek = value;
            }*/
        }

        internal int DaysPerWeek
        {
            get
            {
                return S8Application.ClockDaysPerWeek;
            }/*
            set
            {
                S8Application.ClockDaysPerWeek = value;
            }*/
        }


        internal DateTime StartDateModele
        {
            get
            {
                return S8Application.ClockStartDate;
            }/*
            set
            {
                S8Application.ClockStartDate = value;
            }*/
        }

        internal int DayStartTime
        {
            get
            {
                return S8Application.ClockDayStartTime;
            }/*
            set
            {
                S8Application.ClockDayStartTime = value;
            }*/
        }

        /// <summary>
        /// Get the Day duration value of the Simul8 model currently opened. That information 
        /// indicates how many hours and minutes are worked in the simulation. 
        /// </summary>
        internal int DayDuration
        {
            get
            {
                return S8Application.ClockDayDuration;
            }/*
            set
            {
                S8Application.ClockDayDuration = value;
            }*/
        }

        /// <summary>
        /// Get or Set the Warm up period of the Simul8 model currently opened. The value is an amount of
        /// Time Unit of the model (TimeUnit). After the WarmUp period, the ResultsCollectionPeriod will 
        /// starts. At that moment all the statistics indicators will be reseted. In all cases the 
        /// simulation will then last (WarmUp + ResultsCollectionPeriod ) amount of time.
        /// </summary>
        internal float WarmUpPeriod
        {
            get
            {
                return S8Application.WarmUpTime;
            }
            set
            {
                S8Application.WarmUpTime = value;
            }
        }

        /// <summary>
        /// Get or Set the collection period of the Simul8 model currently opened. The value is an amount of
        /// Time Unit of the model (TimeUnit). This amound of time is the period that would last after the 
        /// WarmUpPeriod. The simulation will then last (WarmUp + ResultsCollectionPeriod ) amount of time.
        /// </summary>
        internal float ResultsCollectionPeriod
        {
            get
            {
                return S8Application.ResultsCollectionPeriod;
            }
            set
            {
                S8Application.ResultsCollectionPeriod = value;
            }
        }
        #endregion
        #endregion

        #region Constructors & fonctions d'initialisation
        public Simul8SubForm(SIMUL8.S8Simulation S8Application__, String sSubWindowName, String sProjectName)
        {
            InitializeComponent();
            cb_Zoom = null;
            ClockHandler = null;
            S8Application_ = S8Application__;
            SubWindowName = sSubWindowName;
            dtDebut = new DateTime(2000, 1, 1, 0, 0, 0);
            t_Update.Start();
            IntializeSimul8Application();
            cOldCursor = p_Simul8.Cursor;
            cCreateLinksCursor = null;
            sCurrentObjectToDrop = "";
            DropGenerator();
            iCurrentImage = null;
            bLinkMode = false;
            ProjectName = sProjectName;
        }

        /// <summary>
        /// Function that initialize the Simul8 Subform and the communications between the subForm and Simul8.
        /// </summary>
        private void IntializeSimul8Application()
        {
            //Link the work area of simul8 to the Panel where it has to update itself
            S8Application.set_WindowHandle(SubWindowName, (int)p_Simul8.Handle);

            p_Simul8.Focus();

            //Make sure that all the children windows will use the panel as the parent window.
            //Then these children windows will draw themselves in the area.
            //S8Application.DialogParent = (int)p_Simul8.Handle;
            S8Application.set_WindowParent("",(int)p_Simul8.Handle);

            //Add the trigger for the simulation.
            S8Application.S8SimulationComponent2ComponentLink += new SIMUL8.IS8SimulationEvents_S8SimulationComponent2ComponentLinkEventHandler(S8Application__S8SimulationComponent2ComponentLink);
            S8Application.S8SimulationComponent2ComponentUnlink += new SIMUL8.IS8SimulationEvents_S8SimulationComponent2ComponentUnlinkEventHandler(S8Application__S8SimulationComponent2ComponentUnlink);

            S8Application.S8SimulationComponentCreate += new SIMUL8.IS8SimulationEvents_S8SimulationComponentCreateEventHandler(S8Application__S8SimulationComponentCreate);
            S8Application.S8SimulationComponentDelete += new SIMUL8.IS8SimulationEvents_S8SimulationComponentDeleteEventHandler(S8Application__S8SimulationComponentDelete);

            S8Application.S8SimulationOpened += new SIMUL8.IS8SimulationEvents_S8SimulationOpenedEventHandler(S8Application__S8SimulationOpened);
            S8Application.S8SimulationReadyToClose += new SIMUL8.IS8SimulationEvents_S8SimulationReadyToCloseEventHandler(S8Application__S8SimulationReadyToClose);
            S8Application.S8SimulationCustomEvent += new SIMUL8.IS8SimulationEvents_S8SimulationCustomEventEventHandler(S8Application__S8SimulationCustomEvent);
            S8Application.S8SimulationEndRun += new SIMUL8.IS8SimulationEvents_S8SimulationEndRunEventHandler(S8Application_S8SimulationEndRun);

            S8Application.S8SimulationUserMessage += new SIMUL8.IS8SimulationEvents_S8SimulationUserMessageEventHandler(S8Application_S8SimulationUserMessage);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            S8Application.DisplayUserMessages = false;
        }
        #endregion

        #region Event raised by Simul8

        /// <summary>
        /// Function that is launched when a Simul8 message has to appear in the Screen.
        /// </summary>
        /// <param name="Answer">The answer that the user will send to Simul8</param>
        /// <param name="TextMsg">The Error message displayed</param>
        /// <param name="ValidAnswers">The validanswers that can be given.</param>
        void S8Application_S8SimulationUserMessage(out int Answer, ref string TextMsg, int ValidAnswers)
        {
            //Function called to test the UserMessage interception.
            //S8Application.ExecVL("File to Sheet    \"C:\\Data/Settings.TXT\" ,  VSS_Settings[1,1]");

            //Buttons available in the Simul8 message bow.
            byte bOk = 0x01;
            byte bCancel = 0x02;
            byte bAbort = 0x04;
            byte bRetry = 0x08;
            byte bIgnore = 0x10;
            byte bYes = 0x20;
            byte bNo = 0x40;

            //Values to send back to simul8, to indicate what action to make
            int iOk = 1;
            int iCancel = 2;
            int iAbort = 3;
            int iRetry = 4;
            int iIgnore = 5;
            int iYes = 6;
            int iNo = 7;

            //We convert the ValidAnswers into a Byte value, to compare it easily with the answers possible.
            Byte bValidAnswers = (Byte)ValidAnswers;

            //We initialize the Possibilities that will be logged in the log file.
            String Possibilities = "";
            //We initialize the answer to 0.
            Answer = 0;

            #region We select the right answer to send back to Simul8.
            //If the cancel Button is available, then we select it.
            if ((bCancel & bValidAnswers) == bCancel)
            {
                //We add to Possibilities the Cancel value.
                Possibilities += "Cancel";
                //We choose that answer.
                if (Answer == 0)
                    Answer = iCancel;
            }

            //If the Ignore Button is available, then we select it.
            if ((bIgnore & bValidAnswers) == bIgnore)
            {
                //We add a coma to separate the different values.
                if (Possibilities.Length > 0)
                    Possibilities += ", ";
                //We add to Possibilities the Ignore value.
                Possibilities += "Ignore";
                //We choose that answer.
                if (Answer == 0)
                    Answer = iIgnore;
            }

            //If the No Button is available, then we select it.
            if ((bNo & bValidAnswers) == bNo)
            {
                //We add a coma to separate the different values.
                if (Possibilities.Length > 0)
                    Possibilities += ", ";
                //We add to Possibilities the No value.
                Possibilities += "No";
                //We choose that answer.
                if (Answer == 0)
                    Answer = iNo;
            }

            //If the Abort Button is available, then we select it.
            if ((bAbort & bValidAnswers) == bAbort)
            {
                //We add a coma to separate the different values.
                if (Possibilities.Length > 0)
                    Possibilities += ", ";
                //We add to Possibilities the Abort value.
                Possibilities += "Abort";
                //We choose that answer.
                if (Answer == 0)
                    Answer = iAbort;
            }

            //If the Ok Button is available, then we select it.
            if ((bOk & bValidAnswers) == bOk)
            {
                //We add a coma to separate the different values.
                if (Possibilities.Length > 0)
                    Possibilities += ", ";
                //We add to Possibilities the Ok value.
                Possibilities += "Ok";
                //We choose that answer.
                if (Answer == 0)
                    Answer = iOk;
            }

            //If the Yes Button is available, then we select it.
            if ((bYes & bValidAnswers) == bYes)
            {
                //We add a coma to separate the different values.
                if (Possibilities.Length > 0)
                    Possibilities += ", ";
                //We add to Possibilities the Yes value.
                Possibilities += "Yes";
                //We choose that answer.
                if (Answer == 0)
                    Answer = iYes;
            }

            //If the Retry Button is available, then we select it.
            if ((bRetry & bValidAnswers) == bRetry)
            {
                //We add a coma to separate the different values.
                if (Possibilities.Length > 0)
                    Possibilities += ", ";
                //We add to Possibilities the Retry value.
                Possibilities += "Retry";
                //We choose that answer.
                if (Answer == 0)
                    Answer = iRetry;
            }
            #endregion

            OverallTools.ExternFunctions.PrintLogFile(DateTime.Now.ToString() + " : Simul8 Message : " + TextMsg + " ( x" + Possibilities + " )");
        }
        #endregion

        #region Closing of the form (or of the current project)
        /// <summary>
        /// Function called when the form is closing. The function will permits to 
        /// close properly Simul8.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Simul8SubForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If the Simul8 application is still opened. We close the current project.
            if (S8Application_ != null)
                S8Application_.Close();

            //We then set the Object to null, that will free the memory.
            S8Application_ = null;
        }

        /// <summary>
        /// Close the current simulation. That will automatically open a blank one.
        /// </summary>
        internal void CloseProject()
        {
            S8Application.Close();

            //We set the handle where Simul8 must open the new project.
            S8Application.set_WindowHandle(SubWindowName, (int)p_Simul8.Handle);

            //We force Simul8 to paint his window.
            p_Simul8.Invalidate();

            //We drop the Pax Generator on the Simul8 project. (The generator has to be there except 
            //for the custom models.
            DropGenerator();

            //We force the refresh of the simul8 window.
            RefreshSimul8();
        }
        #endregion

        #region Fonction to fix the custom menu opened when click on a specific object
        /// <summary>
        /// Function that will update the right menu of the objects with the scenarios names.
        /// </summary>
        /// <param name="lsScenarios_">List of the available scenarios</param>
        /// <param name="ehContextMenuScenarioClick">Event to be sent when the user select a scenario.</param>
        /*internal void setScenarioMenu(List<String> lsScenarios_, EventHandler ehContextMenuScenarioClick)
        {
            cms_ContextMenuObject.Items.Clear();
            foreach (String sName in lsScenarios_)
            {
                cms_ContextMenuObject.Items.Add(sName, null, ehContextMenuScenarioClick);
            }
        }*/
        internal void setScenarioMenu(List<String> lsScenarios_, List<String> lsGraphicsNames, EventHandler ehContextMenuScenarioClick)
        {
            ToolStripMenuItem tsScenario;
            ToolStripItem tsScenario2;
            ToolStripMenuItem tsScenario3;
            ToolStripItem tsGlobalChart;
            ToolStripMenuItem tsmiGlobalChart;
            System.Collections.ArrayList alScenarios = new System.Collections.ArrayList(lsScenarios_);
            alScenarios.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());

            if ((cms_ContextMenuObject.Items.Count > 2) && (cms_ContextMenuObject.Items[2].GetType() == typeof(ToolStripMenuItem)))
            {
                tsScenario = (ToolStripMenuItem)cms_ContextMenuObject.Items[2];
                tsScenario.DropDownItems.Clear();
                //alScenarioNames.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                //tsScenario.DropDown.Opening += new CancelEventHandler(cmsPicture_Opening);
                foreach (String Name in alScenarios)
                {
                    tsScenario.DropDownItems.Add(Name);
                    tsScenario.DropDownItems[tsScenario.DropDownItems.Count - 1].Click += ehContextMenuScenarioClick;
                }
            }
            if ((cms_ContextMenuObject.Items.Count > 3) && (cms_ContextMenuObject.Items[3].GetType() == typeof(ToolStripMenuItem)))
            {
             
                tsScenario = (ToolStripMenuItem)cms_ContextMenuObject.Items[3];
                tsScenario.DropDownItems.Clear();
                //tsScenario.DropDown.Opening += new CancelEventHandler(cmsPicture_Opening);
                System.Collections.ArrayList alGraphics= new System.Collections.ArrayList(lsGraphicsNames);
                alGraphics.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                foreach (String Name in alScenarios)
                {
                    tsScenario2 = tsScenario.DropDownItems.Add(Name);
                    tsScenario3 = (ToolStripMenuItem)tsScenario2;
                    tsGlobalChart = tsScenario3.DropDownItems.Add("Global Chart");
                    tsmiGlobalChart = (ToolStripMenuItem)tsGlobalChart;

                    //tsmiGlobalChart.DropDown.Opening += new CancelEventHandler(cmsPicture_Opening);

                    tsmiGlobalChart.DropDownItems.Add("New");
                    tsmiGlobalChart.DropDownItems[tsmiGlobalChart.DropDownItems.Count - 1].Click += ehContextMenuScenarioClick;

                    tsmiGlobalChart.DropDownItems.Add(new ToolStripSeparator());

                    foreach (String ChartName in alGraphics)
                    {
                        tsmiGlobalChart.DropDownItems.Add(ChartName);
                        tsmiGlobalChart.DropDownItems[tsmiGlobalChart.DropDownItems.Count - 1].Click += ehContextMenuScenarioClick;
                    }
                }
            }
            if ((cms_ContextMenuObject.Items.Count > 4) && (cms_ContextMenuObject.Items[4].GetType() == typeof(ToolStripMenuItem)))
            {
                String[] alName = { "Chart", "Whole Table" };
                tsScenario = (ToolStripMenuItem)cms_ContextMenuObject.Items[4];
                tsScenario.DropDownItems.Clear();
                //tsScenario.DropDown.Opening += new CancelEventHandler(cmsPicture_Opening);
                foreach (String Name in alName)
                {
                    tsScenario.DropDownItems.Add(Name);
                    tsScenario.DropDownItems[tsScenario.DropDownItems.Count - 1].Click += ehContextMenuScenarioClick;
                }
            }
        }

        /*/// <summary>
        /// Function that will initialize the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsPicture_Opening(object sender, CancelEventArgs e)
        {

            if (sender.GetType() != typeof(ToolStripDropDownMenu))
                return;
            ToolStripDropDownMenu tsmiTmp = (ToolStripDropDownMenu)sender;
            //Fonction pour supprimer l'objet sélectionner par l'utilisateur.
            if ((Parent != null) && (Parent.Tag.GetType() == typeof(Ressource_Assistant)))
            {
                for (int i = 0; i < tsmiTmp.Items.Count; i++)
                {
                    tsmiTmp.Items[i].Tag = ((Ressource_Assistant)Parent.Tag).getSelect();
                }
            }
        }*/
        #endregion

        #region The triggers called by Simul8 when specific action are made by the user.
        void S8Application__S8SimulationCustomEvent(ref string TextInfo)
        {
            /*throw new Exception("The method or operation is not implemented.");*/
        }

        void S8Application__S8SimulationReadyToClose()
        {
            /*throw new Exception("The method or operation is not implemented.");*/
        }

        void S8Application__S8SimulationOpened()
        {
            /*throw new Exception("The method or operation is not implemented.");*/
        }

        void S8Application__S8SimulationComponentDelete(ref string ComponentName)
        {
            /*throw new Exception("The method or operation is not implemented.");*/
        }

        void S8Application__S8SimulationComponentCreate(ref string ComponentName)
        {
            /*throw new Exception("The method or operation is not implemented.");*/
        }

        void S8Application__S8SimulationComponent2ComponentUnlink(ref string ComponentNames)
        {
            /*throw new Exception("The method or operation is not implemented.");*/
        }

        void S8Application__S8SimulationComponent2ComponentLink(ref string ComponentNames)
        {
            /*throw new Exception("The method or operation is not implemented.");*/
            if (ComponentNames.Contains(sGenerator))
            {
                String []sLink = ComponentNames.Split('/');
                RemoveLink(S8Application, sLink[0], sLink[1]);
            }
        }

        void S8Application_S8SimulationEndRun()
        {
            if(EndRunFunction!=null)
                EndRunFunction.Invoke();
        }
        #endregion

        #region Event that occurs on the panel (Clicks, Resizing...).
        private void p_Simul8_Paint(object sender, PaintEventArgs e)
        {
            //We protect the call of the PaintWindow function because we made the assumption that
            //Processors with double core are responsible of exception for the COM communication.
            //
            try
            {
                //RefreshSimul8();
                //return;
                //Call of the Paint even in Simul8.
                S8Application.PaintWindow((int)p_Simul8.Handle);
            }
            catch (Exception except)
            {
                OverallTools.ExternFunctions.PrintLogFile(DateTime.Now.ToString() + " : The paint event on Simul8 has raised an error : " + except.Message);
                //We make the thread sleep in order for the COM protocole to finish it's communication before
                //a new try for painting the window.
                Thread.Sleep(10);
            }
        }

        private void p_Simul8_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //If the user is about to drop a component, he would not be able to do anything else.
            if (sCurrentObjectToDrop != "")
                return;
            //Locate the down action in the Simul8 window
            Point sLocation = getMouseLocationInSimul8(e.Location);

            //We are trying to access an object at the location that had been double clicked.
            SIMUL8.S8SimObject s8obj = getSelectedAtLocation(S8Application, SubWindowName, sLocation);

            if(s8obj == null)
                return;

            if(Object_DoubleClickFunction != null)
                Object_DoubleClickFunction.Invoke(s8obj.Name);            
        }

        private void p_Simul8_MouseClick(object sender, MouseEventArgs e)
        {
            //Locate the down action in the Simul8 window
            Point sLocation = getMouseLocationInSimul8(e.Location);

            //We are trying to access an object at the location that had been clicked.
            SIMUL8.S8SimObject s8obj = getSelectedAtLocation(S8Application, SubWindowName, sLocation);

            //We get the number of objects selected in Simul8
            int iNumberSelected = getNumberSelected(S8Application, SubWindowName);

            if (e.Button == MouseButtons.Right)
            {
                //We verify that an object had been clicked.
                if (s8obj==null)
                    return;
                if (iNumberSelected > 1)
                {
                    //------------------------
                    //Todo : Here add the right code for the case of a right click.
                    //------------------------
                }
                else
                {
                    if (cms_ContextMenuObject.Items.Count == 0)
                        return;
                    String sName = s8obj.Name;
                    foreach (ToolStripItem tsmi_Tmp in cms_ContextMenuObject.Items)
                    {
                        if(tsmi_Tmp.GetType() == typeof(ToolStripMenuItem))
                            foreach (ToolStripMenuItem tsmi_Tmp2 in ((ToolStripMenuItem)tsmi_Tmp).DropDownItems)
                            tsmi_Tmp2.Tag = sName;
                    }
                    cms_ContextMenuObject.Show(MousePosition);
                }
            }
            else
            {
                //------------------------
                //Todo : Here add the right code for the case of a left click.
                //------------------------
                if (sCurrentObjectToDrop != "")
                {
                    //We change the cursor to the normal one.
                    p_Simul8.Cursor = cOldCursor;

                    //We drop the component on the screen.
                    if (!DropComponent(sCurrentObjectToDrop, sLocation, iCurrentImage))
                    {

                        //We call the function that will specify to the controlPanel that the component had been placed.
                        if (Object_DroppedFunction != null)
                            Object_DroppedFunction("");
                    }
                    else
                        if (Object_DroppedFunction != null)
                            Object_DroppedFunction(sCurrentObjectToDrop);
                    sCurrentObjectToDrop = "";
                }
            }
        }

        private void p_Simul8_Resize(object sender, EventArgs e)
        {
            S8Application_.SetActiveSimulationWindow(SubWindowName);
            S8Application_.SetWindowSize(p_Simul8.Width, p_Simul8.Height);
        }
        #endregion

        #region The different Events that are sent to Simul8 (MouseUp/MouseDown/MouseMove/ScrollAction.
        private void p_Simul8_MouseUp(object sender, MouseEventArgs e)
        {
            //If the user is about to drop a component, he would not be able to do anything else.
            if (sCurrentObjectToDrop != "")
                return;
            //If Simul8 is running, then the user can't move the objects.
            if (this.SimulationState == SimulationStat.Running)
                return;
            //If the control key is pressed, then we don't send any message to Simul8.
            /*if (Control.ModifierKeys == Keys.Control)
                return;*/
            //Locate the mouse in the Simul8 window
            Point pLocation = getMouseLocationInSimul8(e.Location);


            //We prevent the user to make links between the pax generator and other objets.
            SIMUL8.S8SimObject obj = getSelectedAtLocation(S8Application, SubWindowName, pLocation);
            if ((obj != null) && ((LinkMode)||(Control.ModifierKeys == Keys.Shift)) && (obj.Name == sGenerator))
                return;

            //Send the mouseup event to simul8.
            S8Application.simleftmouseup(
                                    (Control.ModifierKeys == Keys.Shift) || LinkMode,
                /*Control.ModifierKeys == Keys.Control*/false, 
                              Control.ModifierKeys == Keys.Alt, 
                              pLocation.X, 
                              pLocation.Y, 
                              SubWindowName);
        }

        private void p_Simul8_MouseDown(object sender, MouseEventArgs e)
        {
            //If the user is about to drop a component, he would not be able to do anything else.
            if (sCurrentObjectToDrop != "")
                return;
            //If Simul8 is running, then the user can't move the objects.
            if (this.SimulationState == SimulationStat.Running)
                return;

            //Locate the mouse in the Simul8 window
            Point pLocation = getMouseLocationInSimul8(e.Location);

            //We prevent the user to make links between the pax generator and other objets.
            SIMUL8.S8SimObject obj = getSelectedAtLocation(S8Application, SubWindowName, pLocation);
            if ((obj != null) && ((LinkMode) || (Control.ModifierKeys == Keys.Shift)) && (obj.Name == sGenerator))
                return;

            //Send the mouseDown event to simul8.
            S8Application.simleftmousedown(
                                    (Control.ModifierKeys == Keys.Shift) || LinkMode,
                /*Control.ModifierKeys == Keys.Control*/false, 
                                Control.ModifierKeys == Keys.Alt, 
                                pLocation.X, 
                                pLocation.Y,
                                SubWindowName);
        }

        private void p_Simul8_MouseMove(object sender, MouseEventArgs e)
        {
            //If the user is about to drop a component, he would not be able to do anything else.
            if (sCurrentObjectToDrop != "")
                return;
            //If Simul8 is running, then the user can't move the objects.
            if (this.SimulationState == SimulationStat.Running)
                return;
            //If the control key is pressed, then we don't send any message to Simul8.
            /*if (Control.ModifierKeys == Keys.Control)
                return;*/
            //Locate the mouse in the Simul8 window
            Point pLocation = getMouseLocationInSimul8(e.Location);

            //Send the mouseMove event to simul8.
            S8Application.simmousemove(e.Button == MouseButtons.Left, 
                                    e.Button == MouseButtons.Middle, 
                                    e.Button == MouseButtons.Right,
                                    (Control.ModifierKeys == Keys.Shift) ||LinkMode,
                /*Control.ModifierKeys == Keys.Control*/false, 
                                    Control.ModifierKeys == Keys.Alt, 
                                    pLocation.X, 
                                    pLocation.Y, 
                                    SubWindowName);

        }

        /// <summary>
        /// This function permits to scroll the Simul8 sub window. It checks first that
        /// the scroll location in Simul8 is different than the scroll location in 
        /// PAX2SIM.
        /// </summary>
        /// <param name="sender">The sender of the scroll action</param>
        /// <param name="e">The arguments of the scroll action (old values).</param>
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //The values for the scroll inside Simul8
            int ivalue, i2Value;

            //Set the active window in Simul8 to the current one
            S8Application.SetActiveSimulationWindow(SubWindowName);

            //Get the scroll position in Simul8
            S8Application.GetScrollPosition(out ivalue, out i2Value);

            //If the scroll position in Simul8 are the same than the current Scroll bars, then we dont do nothing.
            if ((ivalue != hScrollBar1.Value) || (i2Value != vScrollBar1.Value))
            {
                //Set the scroll location to Simul8.
                S8Application.SetScrollPosition(hScrollBar1.Value, vScrollBar1.Value);

                //Force the paint of the Simul8 area.
                p_Simul8.Invalidate();
                RefreshSimul8();
            }
        }
        #endregion

        #region Part that is making the links and giving the path between the different objects
        /// <summary>
        /// Function that permits to add a link between 2 objets in Simul8
        /// </summary>
        /// <param name="S8Application">The Simul8 application.</param>
        /// <param name="sFirst">Name of the originating object</param>
        /// <param name="sSecond">Name of the Destination object</param>
        /// <param name="fTravelTime">Constant time between the objects</param>
        /// <returns>Return a boolean that indicate if it succed to add the link</returns>
        internal static bool AddLink(SIMUL8.S8Simulation S8Application, String sFirst, String sSecond, float fTravelTime)
        {
            //Get the originating Object
            SIMUL8.S8SimObject sObj = objectExist(S8Application,sFirst);
            //Get the destination Object
            SIMUL8.S8SimObject sObj2 = objectExist(S8Application, sSecond);

            //If one of the 2 objects are null, then there were a problem.
            if ((sObj == null) || (sObj2 == null))
                return false;

            //Add the Links between the two objects
            S8Application.LinkSimObjects(sObj, sObj2, fTravelTime);
            return true;
        }

        /// <summary>
        /// Function that permits to remove a link between 2 objets in Simul8
        /// </summary>
        /// <param name="S8Application">The Simul8 application.</param>
        /// <param name="sFirst">Name of the originating object</param>
        /// <param name="sSecond">Name of the Destination object</param>
        /// <returns>Return a boolean that indicate if it succed to remove the link</returns>
        internal static bool RemoveLink(SIMUL8.S8Simulation S8Application, String sFirst, String sSecond)
        {
            //We get the list of the links from the first group
            List<String> lsList = getChildObjectNames(S8Application, sFirst);
            //If the second Object is not reachable. We return false
            if ((lsList  == null) || (!lsList.Contains(sSecond)))
                return false;

            //Get the originating Object
            SIMUL8.S8SimObject sObj = objectExist(S8Application, sFirst);
            //Get the destination Object
            SIMUL8.S8SimObject sObj2 = objectExist(S8Application, sSecond);

            //If one of the 2 objects are null, then there were a problem.
            if ((sObj == null) || (sObj2 == null))
                return false;

            //Remove the links between the two objects.
            S8Application.UnLinkSimObjects(sObj, sObj2);
            return true;
        }

        /// <summary>
        /// Function that returns the names of all the object child of the current node.
        /// </summary>
        /// <param name="S8Application">The Simul8 application.</param>
        /// <param name="sCurrentObject">The current object</param>
        /// <returns></returns>
        internal static List<String> getChildObjectNames(SIMUL8.S8Simulation S8Application, String sCurrentObject)
        {
            //We select the origin name from Simul8
            SIMUL8.S8SimObject s8Object = objectExist(S8Application, sCurrentObject);

            //If the object does not exist in Simul8, we return null
            if (s8Object == null)
                return null;

            //We create the list for the results
            List<String> lsResult = new List<string>();

            //We catch the number of routes out that exist for that object
            int iNumber = s8Object.CountOutRoutes;
            
            //We are going throught the different links available for that desk.
            for (int i = 0; i < iNumber; i++)
            {
                //We take the child at the index i
                SIMUL8.S8SimObject s8Object2 = ConvertObject(s8Object.get_GetRouteOutObject(i));
                //If the object does not exist, or there is no link anymore. We continue.
                if (s8Object2 == null)
                    continue;

                //If the object wasn't added yet to the list we had it (normally not possible)
                if(!lsResult.Contains(s8Object2.Name))
                    lsResult.Add(s8Object2.Name);
            }
            return lsResult;
        }


        /// <summary>
        /// Function that returns the names of all the object child of the current node.
        /// </summary>
        /// <param name="S8Application">The Simul8 application.</param>
        /// <param name="sCurrentObject">The current object</param>
        /// <returns></returns>
        internal static List<String> getParentObjectNames(SIMUL8.S8Simulation S8Application, String sCurrentObject)
        {
            //We select the origin name from Simul8
            SIMUL8.S8SimObject s8Object = objectExist(S8Application, sCurrentObject);

            //If the object does not exist in Simul8, we return null
            if (s8Object == null)
                return null;

            //We create the list for the results
            List<String> lsResult = new List<string>();

            //We catch the number of routes out that exist for that object
            int iNumber = s8Object.CountInRoutes;

            //We are going throught the different links available for that desk.
            for (int i = 0; i < iNumber; i++)
            {
                //We take the child at the index i
                SIMUL8.S8SimObject s8Object2 = ConvertObject(s8Object.get_GetRouteInObject(i));
                //If the object does not exist, or there is no link anymore. We continue.
                if (s8Object2 == null)
                    continue;

                //If the object wasn't added yet to the list we had it (normally not possible)
                if (!lsResult.Contains(s8Object2.Name))
                    lsResult.Add(s8Object2.Name);
            }
            return lsResult;
        }
        #endregion

        #region Function to get the objects from Simul8

        /// <summary>
        /// Function that returns the number of selected objects in Simul8
        /// </summary>
        /// <param name="S8Application">The Simul8 application.</param>
        /// <param name="SubWindowName">The sub window name where the selected objets are</param>
        /// <returns>The number of selected objects in Simul8</returns>
        internal static int getNumberSelected(SIMUL8.S8Simulation S8Application, String SubWindowName)
        {
            //We get the number of objects that are selected in the Simul8 application.
            return S8Application.get_CountSelectedSimObjects(SubWindowName);
        }

        /// <summary>
        /// Function that returns the objects that are selected in Simul8.
        /// </summary>
        /// <param name="S8Application">The Simul8 application.</param>
        /// <param name="SubWindowName">The sub window name where the selected objets are</param>
        /// <returns>Returns a table with all the selected objects.</returns>
        internal static List<SIMUL8.S8SimObject> getSelected(SIMUL8.S8Simulation S8Application, String SubWindowName)
        {
            //We get the number of objects that are selected in the Simul8 application.
            int iSelected = S8Application.get_CountSelectedSimObjects(SubWindowName);

            //Create a table that would contain the selected
            List<SIMUL8.S8SimObject> s8Selected = new List<SIMUL8.S8SimObject>();

            //If there is no selected object, then we return an empty table.
            if (iSelected == 0)
                return s8Selected;

            //We then fill the table with the selected objects
            for (int i = 0; i < iSelected; i++)
            {
                SIMUL8.S8SimObject sObjTmp = ConvertObject(S8Application.GetSelectedSimObject(SubWindowName, i));

                if (sObjTmp == null)
                    continue;

                //We get the selected objects one by one.
                s8Selected.Add(sObjTmp);
            }
            return s8Selected;
        }

        /// <summary>
        /// Function that returns the objects that are selected in Simul8.
        /// </summary>
        /// <param name="S8Application">The Simul8 application.</param>
        /// <param name="SubWindowName">The sub window name where the selected objets are</param>
        /// <returns>Returns a table with all the selected objects.</returns>
        internal static SIMUL8.S8SimObject getSelectedAtLocation(SIMUL8.S8Simulation S8Application, String SubWindowName, Point pLocation)
        {
            //We are trying to access an object at the location that had been double clicked.
            Object obj = S8Application.FindSimObjectAtLocation(SubWindowName, pLocation.X, pLocation.Y);

            //We convert the object into a Simul8 object
            return ConvertObject(obj);
        }

        /// <summary>
        /// Function that returns the Simul8 object of the given name. If the simul8 object does not
        /// exist, then it returns null.
        /// </summary>
        /// <param name="S8Application">The Simul8 application.</param>
        /// <param name="sName">The name of the object that we are looking for.</param>
        /// <returns>Null if the object does not exist, A Simul8 object, if the object exist.</returns>
        internal static SIMUL8.S8SimObject objectExist(SIMUL8.S8Simulation S8Application, String sName)
        {
            //We try to get the object with the given name
            Object obj = S8Application.get_SimObject(sName);

            //We convert the object into a Simul8 object
            return ConvertObject(obj);
        }

        /// <summary>
        /// Function that convert an object into a Simul8 object
        /// </summary>
        /// <param name="sObjectToConvert">The object that needs to be converted</param>
        /// <returns>Null if the object is null, A Simul8 object, if the object was not null</returns>
        internal static SIMUL8.S8SimObject ConvertObject(object sObjectToConvert)
        {
            //If the object is null we return null.
            if (sObjectToConvert == null)
                return null;
            //We verify the object exist.
            if ((sObjectToConvert.GetType() == typeof(String)) || (sObjectToConvert.ToString() == "(None)"))
                return null;

            //We convert the object into a Simul8 object
            return (SIMUL8.S8SimObject)sObjectToConvert;
        }
        #endregion

        #region Function to convert the mouse location in the Simul8 Window
        /// <summary>
        /// Return the location of the mouse in the Simul8 window.
        /// </summary>
        /// <param name="pMouseLocation">The current location that we need to convert</param>
        /// <returns>The converted location in Simul8 window</returns>
        internal Point getMouseLocationInSimul8(Point pMouseLocation)
        {
            //Create a new Point initialized with the mouse coordinate.
            Point sLocation = new Point(pMouseLocation.X, pMouseLocation.Y);

            //Apply a transformation on the coordinate and on the scroll values to be sure that 
            //the position will have the zoom transforation applied.
            sLocation.X = (int)(UnZoom(sLocation.X, iCurrentZoom) + UnZoom(hScrollBar1.Value, iCurrentZoom));
            sLocation.Y = (int)(UnZoom(sLocation.Y, iCurrentZoom) + UnZoom(vScrollBar1.Value, iCurrentZoom));

            //Return the calculated value.
            return sLocation;
        }

        /// <summary>
        /// Return the location of the mouse in the Simul8 window.
        /// </summary>
        /// <returns>The converted location in Simul8 window</returns>
        internal Point getMouseLocationInSimul8()
        {
            //Convert the position of the mouse in coordinate in the panel. (The panel is representing the
            //Simul8 Window so the converted value would be the position in the Simul8 Window.
            Point sLocation = p_Simul8.PointToClient(MousePosition);
            //Call the function that would translate the coordinate in the Simul8 panel values. That would 
            //translate the coordinate throught the space to match the offset of the Scrollbars.
            return getMouseLocationInSimul8(sLocation);
        }
        #endregion

        #region Function to manage the zoom in the Simul8 Window
        /// <summary>
        /// Function that permits to apply a reverse zoom on a value.  ( Value / Zoom ) * 100.
        /// </summary>
        /// <param name="iValue">The value to convert</param>
        /// <param name="iZoom">The zoom applied.</param>
        /// <returns>The new value without value.</returns>
        internal static Double UnZoom(int iValue, int iZoom)
        {
            return (((Double)iValue) / ((Double)iZoom)) * 100;
        }
        #endregion

        #region Function to manage the TIMER
        /// <summary>
        /// The function that is called by the timer every ticks. That function permits to update
        /// the current Zoom and the current time of the simulation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t_Update_Tick(object sender, EventArgs e)
        {
            /**
            * For the brave souls who get this far: You are the chosen ones,
            * the valiant knights of programming who toil away, without rest,
            * fixing our most awful code. To you, true saviors, kings of men,
            * I say this: never gonna give you up, never gonna let you down,
            * never gonna run around and desert you. Never gonna make you cry,
            * never gonna say goodbye. Never gonna tell a lie and hurt you.
            */
            //Current zoom of Simul8.
            int iZoom = Zoom;

            //We compare the current zoom stored with the current zoom in Simul8.
            if (iCurrentZoom != iZoom)
            {
                //If the are different, then we set the current Zoom to the Simul8 value.
                //To recalculate the scrollbars and to paint the Simul8 window.
                Zoom = iZoom;
            }

            //if the handler for the zoom is defined and its value is different than the current zoom.
            //Then we set the Handler to the current Zoom value.
            if ((cb_Zoom != null) && (cb_Zoom.Text != iZoom.ToString()) && (iCurrentZoom != iZoom))
                cb_Zoom.Text = iZoom.ToString();

            //If the handler for the time exist, then we update the current simulation time.
            if(ctrlLabel!=null)
                ctrlLabel.Text = SimulationTime;
        }
        #endregion

        #region Function used to drop or remove the components

        /// <summary>
        /// Function that allow the user to add a component to Simul8.
        /// </summary>
        /// <param name="S8Application">Simul8 application where to apply the changes.</param>
        /// <param name="SubWindowName">The name of the subwindow in Simul8.</param>
        /// <param name="sPath">Path of the Component</param>
        /// <param name="sName">Name of the component</param>
        /// <param name="Location">Location of the component.</param>
        /// <param name="sVisualLogicCommandLine">Command line that has to be called once the object created.</param>
        /// <returns>Return a boolean that indicates if the add function had finish without problems.</returns>
        internal bool AddComponent( String sPath,
            String sName,
            Point Location,
            Image iImage,
            bool bUseCpStructureFunction)
        {
            //We check that the object does not exist yet in Simul8
            if (objectExist(S8Application, sName) != null)
                return false;

            //We create the new object
            SIMUL8.S8SimObject iobj = ConvertObject(S8Application.CreateComponentObject(sPath));
            if (iobj == null)
                return false;

            //We set the image to the object
            if(iImage != null)
                SetImage(S8Application, iImage, iobj.Name, sName);
            //If the Command line is not null.
            if (bUseCpStructureFunction)
            {
                int iFirstGroup = 1;
                int iLastGroup = 1;
                int[] iTmp = OverallTools.DataFunctions.AnalyzeGroupName(sName);
                if ((iTmp != null) && (iTmp.Length == 4) && (iTmp[2]!=0))
                {
                    if (GetFirstStationForGroupFunction != null)
                        iFirstGroup = GetFirstStationForGroupFunction(iTmp[0], iTmp[2], iTmp[3]);
                    if (GetLastStationForGroupFunction != null)
                        iLastGroup = GetLastStationForGroupFunction(iTmp[0], iTmp[2], iTmp[3]);
                }
                if ((iFirstGroup == -1) || (iLastGroup == -1))
                {
                    DeleteGroup(iobj.Name);
                    EraseImage(S8Application, sName);
                    return false;
                }

                String sDisplayName = "\"#" + (iLastGroup - iFirstGroup + 1).ToString() + " (" + iFirstGroup.ToString() + "-" + iLastGroup.ToString() + ")\"";
                if (iTmp[2] == 0)
                    sDisplayName = sName;
                //We execute the P2S_SR_CpStructure. That function will initialize the component.
                callP2S_SR_CpStructure(S8Application, iFirstGroup, iLastGroup - iFirstGroup + 1, iobj.Name, sName, sDisplayName);

                if ((iTmp[2] == GestionDonneesHUB2SIM.PassportCheckGroup) || 
                    (iTmp[2] == GestionDonneesHUB2SIM.SecurityCheckGroup) || 
                    (iTmp[2] == GestionDonneesHUB2SIM.TransferGroup))
                {
                    //We have to initialize the load that will manage the allocation of the groups that needs allocation
                    //
                    callVisualLogicFunction(S8Application, "CALL Global_P2S_SR_Init_Scheduler ,  " + iTmp[0].ToString() + " ,  " + iTmp[2].ToString() + " ,  " + iTmp[3].ToString() + " ,  " + sName, null);                    
                }
            }

            //We change the name of the new component.
            if (iobj.Name != sName)
                iobj.Name = sName;

            //We change the location of the new component.
            iobj.SetLocation(SubWindowName, Location.X, Location.Y);

            //The object had been added to the Simul8 project without problems.
            return true;
        }

        internal static void callVisualLogicFunction(SIMUL8.S8Simulation S8Application, String sCommand, String[] Parameters)
        {
            String sSeparator = " ,  ";
            String sCommandLine = sCommand;
            if (Parameters != null)
            {
                foreach (String sParameter in Parameters)
                {
                    sCommandLine += sSeparator + sParameter;
                }
            }
            S8Application.ExecVL(sCommandLine);
        }
        internal static void callP2S_SR_CpStructure(SIMUL8.S8Simulation S8Application,
                int iFirstStation,
                int iStationNumber,
                String sOldObjectName,
                String sNewObjectName,
                String sDisplayName)
        {
            callVisualLogicFunction(S8Application, "CALL Global_P2S_SR_CpStructure", new String[]{
                iFirstStation.ToString(),
                iStationNumber.ToString() ,
                "\""+sOldObjectName+"\"",
                "\""+sNewObjectName+"\"",sDisplayName
            });
        }
        internal static void callSetVariable(SIMUL8.S8Simulation S8Application,
                                             String sVariable,
                                             String sValue)
        {
            callVisualLogicFunction(S8Application, "SET " + sVariable + "  =  " + sValue, null);
        }

        /// <summary>
        /// This function will update the queues capacity of the station and the groupes
        /// </summary>
        /// <param name="dsiValues">The list of group and station capacity.</param>
        /// <param name="bPriorityQueues">A boolean that indicates if the simulation has to be in Priority mode or not. (fill queue or not)</param>
        internal void UpdateStationQueueCapacity(Dictionary<String, Int32> dsiValues, bool bPriorityQueues)
        {
            //If the list is null, then we do nothing.
            if (dsiValues == null)
                return;
            int[] iTmp;
            String sCommandLine;
            //For each key contained in the list.
            foreach(String skey in dsiValues.Keys)
            {
                //We analyse the key.
                iTmp = OverallTools.DataFunctions.AnalyzeGroupName(skey);

                //If the analyse didn't work well, we continue to the next station or group.
                if ((iTmp == null)||(iTmp.Length != 4))
                    continue;

                //If the key is of type group. We have to send the group update function.
                if (iTmp[2] <= GestionDonneesHUB2SIM.BoardingGateGroup)
                {
                    //We set the priority behaviour.
                    sCommandLine = "SET " + skey + ".CustomProperty[\"CpTX_Regul_StationQueuingMode\"] = ";
                    if (bPriorityQueues) /*Filling queue mode.*/
                        sCommandLine += "\"Priority\"";
                    else
                        sCommandLine += "\"ShortestQueue\"";
                    S8Application.ExecVL(sCommandLine);

                    //The queue to initialize belong to a group.
                    sCommandLine = "SET " + skey + ":Q_Grp.Max size  =  " + dsiValues[skey].ToString();
                }
                else
                {
                    //The queue to initialize belong to a station
                    //If the function that we are using to know to what group the station belong
                    //is null, then we stop analysing this station and we continue the list.
                    if (getGroupNameForStationFunction == null)
                        continue;

                    //We get the group for that station.
                    String sGroupName = getGroupNameForStationFunction(skey);

                    //If the function returns a null value, then we continue the list.
                    if (sGroupName == null)
                        continue;

                    //We set the Simul8 Visual logic command line to execute.
                    sCommandLine = "SET " + sGroupName + ":Q " + iTmp[3].ToString() + ".Max size  =  " + dsiValues[skey].ToString();
                    S8Application.ExecVL(sCommandLine);
                    sCommandLine = "Set Group Limit    \"" + sGroupName + ":Grp" + iTmp[3].ToString() + "\" ,  " + dsiValues[skey].ToString();
                }

                // We execute the Simul8 command line.
                S8Application.ExecVL(sCommandLine);
            }
        }


        /// <summary>
        /// Function that remove from the Simul8 window the element with the name sName
        /// </summary>
        /// <param name="S8Application">Simul8 application from which we wants to delete an object.</param>
        /// <param name="sName">Name of the group that has to be removed.</param>
        internal static bool DeleteGroup(SIMUL8.S8Simulation S8Application, String sName)
        {
            //If the current Simul8 application is not initialized, we do nothing.
            if (S8Application == null)
                return false;

            //We select the group that has to be deleted.
            SIMUL8.S8SimObject s8Group = objectExist(S8Application, sName);

            //We check that the object exists in the current Simul8 project.
            if (s8Group == null)
                return false;

            //We unselect the current selected objects.
            S8Application.UnSelectObjects();

            //We select the object we want to delete.
            s8Group.Select();

            //we finally delete the selected object (which is only the group we wanted to delete).
            S8Application.DeleteSelectedObjects();

            //Erase the image that was linked to that object.
            EraseImage(S8Application, sName);
            return true;
        }

        /// <summary>
        /// Function that remove from the Simul8 window the element with the name sName
        /// </summary>
        /// <param name="sName">Name of the group that has to be removed.</param>
        internal void DeleteGroup(String sName)
        {
            DeleteGroup(S8Application, sName);
        }

        private void DropGenerator()
        {
            //We check that the component exist or not in the simulation model.
            if (objectExist(S8Application, sGenerator) != null)
                return;
            //We determine the path for the Generator Object.
            String sPath = OverallTools.ExternFunctions.getTempDirectoryForPax2sim() + sGeneratorName;

            //If the object does not exist, then we export the resource to the directory.
            if (!System.IO.File.Exists(sPath))
            {
                if ((!PAX2SIM.bDebug) || (!OverallTools.ExternFunctions.CopyFile(sPath, OverallTools.ExternFunctions.getPax2simDirectory() + "Ressources\\" + sGeneratorName, "", null, null, null)))
                    OverallTools.ExternFunctions.copyResourceFile(sGeneratorName, sPath);
                if (!System.IO.File.Exists(sPath))
                    return;
            }

            //We add the component to the simulation.
            AddComponent(sPath, sGenerator, new Point(30, 30), null, false);
            callSetVariable(S8Application, "VTX_ModelPath", "\"" + ModelPath + "\"");
        }

        /// <summary>
        /// Function that will extract from the Pax2Sim Resources the component that the user
        /// wants to drop. It would also gave a boolean that indicates if there's a need to
        /// call the CpStructure function in Simul8. (To initialize the component).
        /// </summary>
        /// <param name="sName">Name of the group that will be placed</param>
        /// <param name="bCallCpStructureFunction">Boolean that indicates if the structure function has to be called</param>
        /// <returns>Path of the component.</returns>
        private static String DropComponent(String sName,out bool bCallCpStructureFunction)
        {
            bCallCpStructureFunction = false;
            int[] iTerminal = OverallTools.DataFunctions.AnalyzeGroupName(sName);
            if (iTerminal == null)
                return null;

            //We determine the path for the Generator Object.
            String sPath = OverallTools.ExternFunctions.getTempDirectoryForPax2sim();
            String sObject = "";
            if (iTerminal.Length == 4)
            {
                bCallCpStructureFunction = true;
                switch (iTerminal[2])
                {
                    case 0:
                        sObject = sPaxInOutOS8;
                        //bCallCpStructureFunction = false;
                        break;
                    case 1:
                        sObject = sCheckInOS8;
                        break;
                    case 2:
                        sObject = sPassportOS8;
                        break;
                    case 3:
                        sObject = sSecurityOS8;
                        break;
                    case 4:
                        sObject = sArrivalGateOS8;
                        break;
                    case 5:
                        sObject = sReclaimOS8;
                        break;
                    case 6:
                        sObject = sTransferDeskOS8;
                        break;
                    case 8:
                        sObject = sBoardingGateOS8;
                        break;
                    default:
                        return null;
                }
                sPath += sObject;
            }

            if (sObject == "")
                return null;
            //If the object does not exist, then we export the resource to the directory.
            if (!System.IO.File.Exists(sPath))
            {
                if ((!PAX2SIM.bDebug) || (!OverallTools.ExternFunctions.CopyFile(sPath, OverallTools.ExternFunctions.getPax2simDirectory() +"Ressources\\"+ sObject, "", null, null, null)))
                        OverallTools.ExternFunctions.copyResourceFile(sGeneratorName, sPath);

                OverallTools.ExternFunctions.copyResourceFile(sObject, sPath);
                if (!System.IO.File.Exists(sPath))
                    return null;
            }
            return sPath;
        }


        private bool DropComponent(String sObjectToDrop,
                                 Point pLocation,
                                 Image iImage)
        {
            bool bCallCpStructureFunction;
            String sCurrentComponentLocation = DropComponent(sObjectToDrop, out bCallCpStructureFunction);
            if (sCurrentComponentLocation == null)
                return false;
            if (AddComponent(sCurrentComponentLocation, sObjectToDrop, pLocation,iImage, bCallCpStructureFunction))
            {
                return true;
            }
            return false;
        }
        


        internal void InitializeModel(List<Simul8Object> lsObjects, List<Assistant.Ressource_Assistant.ArrowParam>lsLinks)
        {
            if (lsObjects == null)
                return;
            if (lsLinks == null)
                return;
            foreach(Simul8Object s8Object in lsObjects)
            {
                s8Object.iSourceImage = OverallTools.FonctionUtiles.CreateSimul8Image(s8Object.iSourceImage, s8Object.sGroupeName);
                DropComponent(s8Object.sGroupeName, s8Object.pLocation, s8Object.iSourceImage);
            }

            foreach (Assistant.Ressource_Assistant.ArrowParam Link in lsLinks)
            {
                AddLink(S8Application, Link.Origin, Link.Goal, (float)Link.Param1);
            }
        }
        #endregion

        /// <summary>
        /// Set the Current application into the dropComponent mode. Allow the user to click in 
        /// the Simul8 window to drop the selected component.
        /// </summary>
        /// <param name="sGroupName">Name of the group that has to be dropped.</param>
        /// <param name="iImage">Image to be shown on the Simul8 window.</param>
        internal void DropComponent(String sGroupName, Image iImage)
        {
            //If the component does not have a name, we Initialize back the cursor.
            if (sGroupName == "")
            {
                p_Simul8.Cursor = cOldCursor;
                sCurrentObjectToDrop = "";
                return;
            }
            sCurrentObjectToDrop = sGroupName;
            iCurrentImage = OverallTools.FonctionUtiles.CreateSimul8Image(iImage, sGroupName);
            //We convert the given image into a cursor   
            p_Simul8.Cursor = OverallTools.FonctionUtiles.CreateCursor(iCurrentImage, 0, 0);
        }

        #region Manage the images
        /// <summary>
        /// Function that will save the image in a tempory file that would be used directly in
        /// Simul8
        /// </summary>
        /// <param name="img">The image to save</param>
        /// <returns></returns>
        private static Boolean SaveImageToSimul8(Image img)
        {
            try
            {
                img.Save(OverallTools.ExternFunctions.getTempDirectoryForPax2sim() + sS8Image, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile(DateTime.Now.ToString() + " : Unable to save the image for Simul8 : " + e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Function that will 
        /// </summary>
        /// <param name="iImg"></param>
        /// <param name="iTerminal"></param>
        /// <param name="iLevel"></param>
        /// <param name="iGroup"></param>
        private static void SetImage(SIMUL8.S8Simulation S8Application,
                              Image iImg, 
                              String sObjet,
                              String sImage)
        {
            if(!SaveImageToSimul8(iImg))
                return;
            String sImagePath = OverallTools.ExternFunctions.getTempDirectoryForPax2sim() + sS8Image;
            String sCode = "Load Image    \"" + sImagePath + "\" ,  \"" + sImage + "_IMG\"";
            S8Application.ExecVL(sCode);
            S8Application.ExecVL("Set Object Image    \"" + sObjet + "\" ,  \"" + sImage + "_IMG\"");
        }

        private static void EraseImage(SIMUL8.S8Simulation S8Application, String sGoupName)
        {
            S8Application.ExecVL("Erase Image    \"" + sGoupName + "_IMG\"");
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            S8Application.Save("C:/"+ProjectName+".S8");
            //List<String > lsTmp = getChildObjectNames(S8Application, "T1L1_Check In Group 1");
            //SIMUL8.S8SimObject

            //S8Application.CreateVisualLogicSection("VL SECTION: Seb Logic ,  LOCALDATA: LcNB:[NUMBER]\t"
            //    + "  Clear Sheet    VSS_Temp[1,1]");
            //S8Application.Save("c:\\test.s8");

        }
    }
}