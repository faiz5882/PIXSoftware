using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt.Liege
{
    public partial class AllocationAssistant : Form
    {
        internal const string SORT_TYPE_BY_OCCUPATION_START = "STD - Resource Opening Time";
        internal const string SORT_TYPE_BY_STD = "STD";
        internal const string SORT_TYPE_BY_BODY_CATEGORY = "Body Category";
        internal const string SORT_TYPE_BY_NB_SEATS = "Number of Seats";

        internal const int DEFAULT_ANALYSIS_RANGE_IN_MINUTES = 60;

        internal const int DEFAULT_DELAY_BETWEEN_CONSECUTIVE_FLIGHTS_MINUTES = 5;
        internal const int DEFAULT_DELAY_BETWEEN_CONSECUTIVE_BOARDINGS_MINUTES = 5;
        internal const int DEFAULT_BOARDING_ENTERING_SPEED_PAX_PER_MINUTES = 15;
        internal const int DEFAULT_BOARDING_EXITING_SPEED_PAX_PER_MINUTES = 15;
        internal const int DEFAULT_NB_SEATS_LOWER_LIMIT = 200;

        internal const int DEFAULT_DOWN_TIME_MAX_ACCEPTED_VALUE = 420;
        internal const int DEFAULT_DOWN_TIME_AFTER_STA = 120;
        internal const int DEFAULT_DOWN_TIME_BEFORE_STD = 120;

        GestionDonneesHUB2SIM donnees;

        DataTable departureFlightPlan;
        
        internal DataTable fpdTable
        {
            get
            {
                return departureFlightPlan;
            }
        }

        internal string parkingOCTTableName
        {
            get
            {
                return parkingOCTComboBox.Text;
            }
        }
        internal bool parkingUseOCTexceptions
        {
            get
            {
                return parkingOCTUseExceptionsCheckBox.Checked;
            }
        }

        internal string bgOCTTableName
        {
            get
            {
                return bgOCTcomboBox.Text;
            }
        }
        internal bool bgUseOCTexceptions
        {
            get
            {
                return bgOCTUseExceptionsCheckBox.Checked;
            }
        }

        internal string ciOCTTableName
        {
            get
            {
                return ciOCTcomboBox.Text;
            }
        }
        internal bool ciUseOCTexceptions
        {
            get
            {
                return ciOCTUseExceptionsCheckBox.Checked;
            }
        }

        internal string ciOCTAirlineAirportTableName
        {
            get
            {
                return ciOCTAirportAirlineComboBox.Text;
            }
        }

        internal string ciShowUpTableName
        {
            get
            {
                return ciShowUpComboBox.Text;
            }
        }
        internal bool ciShowUpUseExceptions
        {
            get
            {
                return ciShowUpUseExceptionsCheckBox.Checked;
            }
        }

        internal bool ignoreCheckInShowUp
        {
            get
            {
                return ignoreCheckInShowUpCheckBox.Checked;
            }
        }

        internal string aircraftTypesTableName
        {
            get
            {
                return aircraftTypesComboBox.Text;
            }
        }
        internal bool useAircraftTypesExceptions
        {
            get
            {
                return aircraftTypesUseExceptionCheckBox.Checked;
            }
        }

        internal string aircraftLinksTableName
        {
            get
            {
                return aircraftLinksComboBox.Text;
            }
        }
        internal bool disableAircraftLinks
        {
            get
            {
                return disableAircraftLinksCheckBox.Checked;
            }
        }

        internal string loadingFactorsTableName
        {
            get
            {
                return loadingFactorsComboBox.Text;
            }
        }
        internal bool useLoadingFactorsExceptions
        {
            get
            {
                return loadingFactorsUseExceptionsCheckBox.Checked;
            }
        }
        internal bool disableLoadingFactors
        {
            get
            {
                return disableLoadingFactorsCheckBox.Checked;
            }
        }

        internal string parkingPrioritiesTableName
        {
            get
            {
                return parkingPrioritiesComboBox.Text;
            }
        }

        internal string boardingGatesPrioritiesTableName
        {
            get
            {
                return boardingGatesPrioritiesComboBox.Text;
            }
        }

        internal string scenarioName
        {
            get
            {
                return scenarioNameTextBox.Text;
            }
        }

        internal DateTime fromDate
        {
            get
            {                
                return fromDatePicker.Value;
            }
        }

        internal DateTime toDate
        {
            get
            {
                return toDatePicker.Value;
            }
        }

        internal const int ALL_TERMINALS = -1;
        internal const int T1_T3 = 13;
        internal int terminalNb
        {
            get
            {
                int terminalNb = -1;
                string selectedTerminalNb = terminalComboBox.SelectedItem.ToString();
                switch (selectedTerminalNb)
                {
                    case "All":
                        terminalNb = ALL_TERMINALS;
                        break;
                    case "T1&T3":
                        terminalNb = T1_T3;
                        break;
                    case "T1":
                        terminalNb = 1;
                        break;
                    case "T2":
                        terminalNb = 2;
                        break;
                    case "T3":
                        terminalNb = 3;
                        break;
                    default: 
                        break;
                }
                return terminalNb;
            }
        }

        internal double timeStepInMinutes
        {
            get
            {
                double timeStep = -1;
                Double.TryParse(timeStepTextBox.Text, out timeStep);
                return timeStep;
            }
        }

        internal double analysisRangeInMinutes
        {
            get
            {
                double analysisRange = -1;
                Double.TryParse(analysisRangeTextBox.Text, out analysisRange);
                return analysisRange;
            }
        }

        internal int delayBetweenConsecutiveFlightsInMinutes
        {
            get
            {
                int delay = -1;
                Int32.TryParse(delayBetweenFlightsTextBox.Text, out delay);
                return delay;
            }
        }

        internal bool useFPasBasis
        {
            get
            {
                return useFPasBasisCheckBox.Checked;
            }
        }

        internal int downTimeMaxAcceptedValue
        {
            get
            {
                int value = -1;
                Int32.TryParse(downTimeMaxAcceptedValueTextBox.Text, out value);
                return value;
            }
        }

        internal int downTimeAfterSTA
        {
            get
            {
                int value = -1;
                Int32.TryParse(downTimeAfterSTATextBox.Text, out value);
                return value;
            }
        }

        internal int downTimeBeforeSTD
        {
            get
            {
                int value = -1;
                Int32.TryParse(downTimeBeforeSTDTextBox.Text, out value);
                return value;
            }
        }

        internal bool updateFPwithAllocation
        {
            get
            {
                return updateFPWithAllocationCheckBox.Checked;
            }
        }

        internal int boardingEnteringSpeedPaxPerMinute
        {
            get
            {
                int boardingSpeed = -1;
                Int32.TryParse(boardingEnteringSpeedTextBox.Text, out boardingSpeed);
                return boardingSpeed;
            }
        }

        internal int boardingExitingSpeedPaxPerMinute
        {
            get
            {
                int boardingSpeed = -1;
                Int32.TryParse(boardingExitingSpeedTextBox.Text, out boardingSpeed);
                return boardingSpeed;
            }
        }

        internal int lowerNsSeatsLimitForLargeAircrafts
        {
            get
            {
                int limit = -1;
                Int32.TryParse(limitForLargeAircraftsTextBox.Text, out limit);
                return limit;
            }
        }

        internal String allocationType
        {
            get
            {
                if (checkInAllocationTypeRadioButton.Checked)
                    return Allocation.CHECK_IN_ALLOCATION_TYPE;
                else if (boardingGateAllocationTypeRadioButton.Checked)
                    return Allocation.BOARDING_GATE_ALLOCATION_TYPE;
                else if (parkingAllocationTypeRadioButton.Checked)
                    return Allocation.PARKING_ALLOCATION_TYPE;
                return "";
            }
        }

        internal String resourceType
        {
            get
            {
                if (checkInAllocationTypeRadioButton.Checked)
                    return Allocation.CHECK_IN_RESOURCE_TYPE;
                else if (boardingGateAllocationTypeRadioButton.Checked)
                    return Allocation.BOARDING_GATE_RESOURCE_TYPE;
                else if (parkingAllocationTypeRadioButton.Checked)
                    return Allocation.PARKING_RESOURCE_TYPE;
                return "";
            }
        }

        internal String flightPlanInformationCompatibleAllocationType
        {
            get
            {
                if (checkInAllocationTypeRadioButton.Checked)
                    return Allocation.CHECK_IN_FPI_ALLOC_TYPE;
                else if (boardingGateAllocationTypeRadioButton.Checked)
                    return Allocation.BOARDING_GATE_FPI_ALLOC_TYPE;
                else if (parkingAllocationTypeRadioButton.Checked)
                    return Allocation.PARKING_FPI_ALLOC_TYPE;
                return "";
            }
        }
                
        internal string mainSortType
        {
            get
            {
                return mainSortTypeComboBox.SelectedItem.ToString();
            }
        }
        internal string secondarySortType
        {
            get
            {
                if (secondarySortTypecomboBox.Enabled && secondarySortTypecomboBox.SelectedItem != null)
                    return secondarySortTypecomboBox.SelectedItem.ToString();
                return "";
            }
        }

        internal bool calculateOccupation
        {
            get
            {
                return calculateOccupationCheckBox.Checked;
            }
        }

        private AllocationParameters initialParameters;

        public AllocationAssistant(GestionDonneesHUB2SIM _donnees)
        {
            setUpAssistant(_donnees);
        }

        internal AllocationAssistant(GestionDonneesHUB2SIM _donnees, AllocationParameters parameters)  // >> Task #13367 Liege allocation C#109
        {
            setUpAssistant(_donnees);
            updateAssistantWithParameters(parameters);
            initialParameters = parameters;
        }

        private void updateAssistantWithParameters(AllocationParameters parameters)
        {
            #region Parameters tab
            scenarioNameTextBox.Text = parameters.name;
            if (parameters.type == Allocation.PARKING_ALLOCATION_TYPE)
            {
                parkingAllocationTypeRadioButton.Checked = true;
            }
            else if (parameters.type == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                boardingGateAllocationTypeRadioButton.Checked = true;
            }
            else if (parameters.type == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                checkInAllocationTypeRadioButton.Checked = true;
            }
            if (departureFlightPlanComboBox.Items.Contains(parameters.fpdTableName))
            {
                departureFlightPlanComboBox.SelectedItem = parameters.fpdTableName;
            }
            updateDatePicker(fromDatePicker, parameters.start);
            updateDatePicker(toDatePicker, parameters.end);
            if (parameters.terminal != null && terminalComboBox.Items.Contains(parameters.terminal))
            {
                terminalComboBox.SelectedItem = parameters.terminal;
            }

            if (mainSortTypeComboBox.Items.Contains(parameters.mainSortingType))
            {
                mainSortTypeComboBox.SelectedItem = parameters.mainSortingType;
            }
            if (parameters.secondarySortingType != null && secondarySortTypecomboBox.Items.Contains(parameters.secondarySortingType))
            {
                secondarySortTypecomboBox.SelectedItem = parameters.secondarySortingType;
            }
            timeStepTextBox.Text = parameters.timeStep.ToString();
            analysisRangeTextBox.Text = parameters.analysisRange.ToString();
            delayBetweenFlightsTextBox.Text = parameters.delayBetweenConsecutiveFlights.ToString();

            useFPasBasisCheckBox.Checked = parameters.useFpAsBasis;
            updateFPWithAllocationCheckBox.Checked = parameters.updateFpWithResults;

            if (PAX2SIM.dubaiMode)
            {
                downTimeMaxAcceptedValueTextBox.Text = parameters.maximumAcceptedDownTime.ToString();
                downTimeAfterSTATextBox.Text = parameters.downTimeAfterSta.ToString();
                downTimeBeforeSTDTextBox.Text = parameters.downTimeBeforeStd.ToString();
            }
            boardingEnteringSpeedTextBox.Text = parameters.boardingEnteringSpeed.ToString();
            boardingExitingSpeedTextBox.Text = parameters.boardingExitingSpeed.ToString();
            limitForLargeAircraftsTextBox.Text = parameters.lowerLimitForLargeAircraft.ToString();
            calculateOccupationCheckBox.Checked = parameters.calculateOccupation;
            #endregion

            #region Input Data tab
            if (parameters.aircraftTypesTableName != null)
            {
                if (aircraftTypesComboBox.Items.Contains(parameters.aircraftTypesTableName))
                {
                    aircraftTypesComboBox.SelectedItem = parameters.aircraftTypesTableName;                    
                }
                aircraftTypesUseExceptionCheckBox.Checked = parameters.aircraftTypesUseExceptions;
            }
            if (parameters.aircraftLinksTableName != null)
            {
                if (aircraftLinksComboBox.Items.Contains(parameters.aircraftLinksTableName))
                {
                    aircraftLinksComboBox.SelectedItem = parameters.aircraftLinksTableName;                    
                }
                disableAircraftLinksCheckBox.Checked = parameters.aircraftLinksDisable;
            }
            if (parameters.loadingFactorsTableName != null)
            {
                if (loadingFactorsComboBox.Items.Contains(parameters.loadingFactorsTableName))
                {
                    loadingFactorsComboBox.SelectedItem = parameters.loadingFactorsTableName;
                }
                loadingFactorsUseExceptionsCheckBox.Checked = parameters.loadingFactorsUseExceptions;
                disableLoadingFactorsCheckBox.Checked = parameters.loadingFactorsDisable;
            }
            if (parameters.ciShowUpTableName != null)
            {
                if (ciShowUpComboBox.Items.Contains(parameters.ciShowUpTableName))
                {
                    ciShowUpComboBox.SelectedItem = parameters.ciShowUpTableName;
                }
                ciShowUpUseExceptionsCheckBox.Checked = parameters.ciShowUpUseExceptions;
                ignoreCheckInShowUpCheckBox.Checked = parameters.ciShowUpIgnore;
            }
            if (parameters.prkOctTableName != null)
            {
                if (parkingOCTComboBox.Items.Contains(parameters.prkOctTableName))
                {
                    parkingOCTComboBox.SelectedItem = parameters.prkOctTableName;
                }
                parkingOCTUseExceptionsCheckBox.Checked = parameters.prkOctUseExceptions;
            }
            if (parameters.bgOctTableName != null)
            {
                if (bgOCTcomboBox.Items.Contains(parameters.bgOctTableName))
                {
                    bgOCTcomboBox.SelectedItem = parameters.bgOctTableName;
                }
                bgOCTUseExceptionsCheckBox.Checked = parameters.bgOctUseExceptions;
            }            
            if (parameters.ciOctTableName != null)
            {
                if (ciOCTcomboBox.Items.Contains(parameters.ciOctTableName))
                {
                    ciOCTcomboBox.SelectedItem = parameters.ciOctTableName;
                }
                ciOCTUseExceptionsCheckBox.Checked = parameters.ciOctUseExceptions;
            }
            if (parameters.ciOctAirlineAirportExceptionTableName != null)
            {
                if (ciOCTAirportAirlineComboBox.Items.Contains(parameters.ciOctAirlineAirportExceptionTableName))
                {
                    ciOCTAirportAirlineComboBox.SelectedItem = parameters.ciOctAirlineAirportExceptionTableName;
                }
            }
            if (parameters.parkingPrioritiesTableName != null
                && parkingPrioritiesComboBox.Items.Contains(parameters.parkingPrioritiesTableName))
            {
                parkingPrioritiesComboBox.SelectedItem = parameters.parkingPrioritiesTableName;
            }
            if (parameters.boardingGatesPrioritiesTableName != null
                && boardingGatesPrioritiesComboBox.Items.Contains(parameters.boardingGatesPrioritiesTableName))
            {
                boardingGatesPrioritiesComboBox.SelectedItem = parameters.boardingGatesPrioritiesTableName;
            }
            #endregion
        }

        private void updateDatePicker(DateTimePicker datePicker, DateTime dateTime)
        {   
            datePicker.MinDate = dateTime.AddDays(-1);
            datePicker.MaxDate = dateTime.AddDays(1);
            datePicker.Value = dateTime;
        }

        private void setUpAssistant(GestionDonneesHUB2SIM _donnees)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            donnees = _donnees;
            if (PAX2SIM.dubaiMode)
            {
                #region Dubai
                terminalLabel.Visible = true;
                terminalComboBox.Visible = true;

                terminalComboBox.Items.Add("All");
                terminalComboBox.Items.Add("T1&T3");
                terminalComboBox.Items.Add("T1");
                terminalComboBox.Items.Add("T2");
                terminalComboBox.Items.Add("T3");
                terminalComboBox.SelectedIndex = 0;

                boardingGateAllocationTypeRadioButton.Visible = false;
                checkInAllocationTypeRadioButton.Visible = false;
                boardingGatesGroupBox.Visible = false;
                allocationTabControl.TabPages.Remove(inputDataTabPage);
                useFPasBasisCheckBox.Visible = false;
                updateFPWithAllocationCheckBox.Visible = false;

                splitFlightsGroupBox.Visible = true;
                //Point location = new Point(6, 91);
                //downTimeMaxAcceptedValueLabel.Visible = true;
                //downTimeMaxAcceptedValueLabel.Location = location;

                //location = new Point(241, 91);                
                //downTimeMaxAcceptedValueTextBox.Location = location;
                downTimeMaxAcceptedValueTextBox.Text = DEFAULT_DOWN_TIME_MAX_ACCEPTED_VALUE.ToString();
                //downTimeMaxAcceptedValueTextBox.Visible = true;

                //location = new Point(6, 116);
                //downTimeAfterSTALabel.Visible = true;
                //downTimeAfterSTALabel.Location = location;

                //location = new Point(241, 117);
                //downTimeAfterSTATextBox.Location = location;
                downTimeAfterSTATextBox.Text = DEFAULT_DOWN_TIME_AFTER_STA.ToString();
                //downTimeAfterSTATextBox.Visible = true;

                //location = new Point(6, 140);
                //downTimeBeforeSTDLabel.Visible = true;
                //downTimeBeforeSTDLabel.Location = location;

                //location = new Point(241, 143);
                //downTimeBeforeSTDTextBox.Location = location;
                downTimeBeforeSTDTextBox.Text = DEFAULT_DOWN_TIME_BEFORE_STD.ToString();
                //downTimeBeforeSTDTextBox.Visible = true;
                #endregion
            }
            setUpComboBoxes();
            enableAllocationSpecificInput();
            setDefaultTimeStepValue();
            setDefaultAnalysisRangeValue();
            setDefaultDelayBetweenConsecutiveFlights();

            //setDefaultDelayBetweenConsecutiveBoardings();
            setDefaultBoardingSpeed();
            setDefaultNbSeatsLowerLimit();
            setUpSortingTypes();
        }

        private void setUpSortingTypes()
        {
            mainSortTypeComboBox.Items.Clear();
            secondarySortTypecomboBox.Items.Clear();
            if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)
            {
                if (PAX2SIM.liegeMode)
                    mainSortTypeComboBox.Items.Add(SORT_TYPE_BY_BODY_CATEGORY);
                mainSortTypeComboBox.Items.Add(SORT_TYPE_BY_OCCUPATION_START);
                mainSortTypeComboBox.Items.Add(SORT_TYPE_BY_STD);
                mainSortTypeComboBox.SelectedIndex = 0;

                secondarySortTypecomboBox.Enabled = true;
                if (PAX2SIM.liegeMode)
                    secondarySortTypecomboBox.Items.Add(SORT_TYPE_BY_BODY_CATEGORY);
                secondarySortTypecomboBox.Items.Add(SORT_TYPE_BY_OCCUPATION_START);
                secondarySortTypecomboBox.Items.Add(SORT_TYPE_BY_STD);
                secondarySortTypecomboBox.SelectedIndex = 1;
            }
            else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                mainSortTypeComboBox.Items.Add(SORT_TYPE_BY_NB_SEATS);
                mainSortTypeComboBox.Items.Add(SORT_TYPE_BY_OCCUPATION_START);
                mainSortTypeComboBox.Items.Add(SORT_TYPE_BY_STD);
                mainSortTypeComboBox.SelectedIndex = 0;

                secondarySortTypecomboBox.Enabled = true;
                secondarySortTypecomboBox.Items.Add(SORT_TYPE_BY_NB_SEATS);
                secondarySortTypecomboBox.Items.Add(SORT_TYPE_BY_OCCUPATION_START);
                secondarySortTypecomboBox.Items.Add(SORT_TYPE_BY_STD);
                secondarySortTypecomboBox.SelectedIndex = 1;
            }
            else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {                
                mainSortTypeComboBox.Items.Add(SORT_TYPE_BY_OCCUPATION_START);
                mainSortTypeComboBox.Items.Add(SORT_TYPE_BY_STD);
                mainSortTypeComboBox.SelectedIndex = 0;
                secondarySortTypecomboBox.Enabled = false;
            }
        }

        private void setUpComboBoxes()
        {
            setUpComboBox(departureFlightPlanComboBox, GlobalNames.FPDTableName);
            setUpComboBox(aircraftTypesComboBox, GlobalNames.FP_AircraftTypesTableName);
            if (!disableLoadingFactorsCheckBox.Checked)
                setUpComboBox(loadingFactorsComboBox, GlobalNames.FPD_LoadFactorsTableName);
            if (allocationType.Equals(Allocation.PARKING_ALLOCATION_TYPE))
            {
                setUpComboBox(parkingOCTComboBox, GlobalNames.OCT_ParkingTableName);
                setUpComboBox(aircraftLinksComboBox, GlobalNames.FPLinksTableName);
                setUpComboBox(parkingPrioritiesComboBox, GlobalNames.parkingPrioritiesTableName);

                bgOCTcomboBox.Items.Clear();
                ciOCTcomboBox.Items.Clear();
                ciOCTAirportAirlineComboBox.Items.Clear();
                ciShowUpComboBox.Items.Clear();
                boardingGatesPrioritiesComboBox.Items.Clear();
                loadingFactorsComboBox.Items.Clear();
                boardingEnteringSpeedTextBox.Text = "";
                boardingExitingSpeedTextBox.Text = "";
                //delayBetweenBoardingsTextBox.Text = "";
                limitForLargeAircraftsTextBox.Text = "";
            }
            else if (allocationType.Equals(Allocation.BOARDING_GATE_ALLOCATION_TYPE))
            {
                
                setUpComboBox(bgOCTcomboBox, GlobalNames.OCT_BoardGateTableName);
                setUpComboBox(ciOCTcomboBox, GlobalNames.OCT_CITableName);
                setUpComboBox(loadingFactorsComboBox, GlobalNames.FPD_LoadFactorsTableName);
                setUpCheckInShowUpData();
                setUpComboBox(boardingGatesPrioritiesComboBox, GlobalNames.boardingGatesPrioritiesTableName);
                
                parkingOCTComboBox.Items.Clear();
                aircraftLinksComboBox.Items.Clear();
                parkingPrioritiesComboBox.Items.Clear();
                ciOCTAirportAirlineComboBox.Items.Clear();
            }
            else if (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)
            {
                boardingEnteringSpeedTextBox.Text = "";
                boardingExitingSpeedTextBox.Text = "";
                //delayBetweenBoardingsTextBox.Text = "";
                limitForLargeAircraftsTextBox.Text = "";
                
                aircraftLinksComboBox.Items.Clear();
                loadingFactorsComboBox.Items.Clear();
                bgOCTcomboBox.Items.Clear();
                parkingOCTComboBox.Items.Clear();
                ciShowUpComboBox.Items.Clear();
                boardingGatesPrioritiesComboBox.Items.Clear();
                parkingPrioritiesComboBox.Items.Clear();

                setUpComboBox(ciOCTcomboBox, GlobalNames.OCT_CITableName);
                setUpUserDataBasedComboBox(ciOCTAirportAirlineComboBox, donnees.getUserData());
            }
        }

        private void setUpComboBox(ComboBox comboBox, string mainTableName)
        {
            if (comboBox != null && mainTableName != null)
            {
                List<String> tableNames = donnees.getValidTables("Input", mainTableName);
                if (tableNames != null)
                {
                    comboBox.Items.Clear();
                    comboBox.Items.AddRange(tableNames.ToArray());
                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedIndex = 0;
                }
            }
        }

        private void setUpUserDataBasedComboBox(ComboBox comboBox, Dictionary<String, List<String>> userDataStructure)
        {
            if (comboBox == null || comboBox.Tag == null
                || userDataStructure == null)
            {
                return;
            }            
            comboBox.Items.Clear();            
            String comboBoxStandardFileName = comboBox.Tag.ToString();

            if (userDataHasSubdirectory(userDataStructure, comboBoxStandardFileName))
            {
                foreach (String value in userDataStructure[comboBoxStandardFileName])                
                    comboBox.Items.Add(value);
                if (comboBox.Items.Count > 0)
                    comboBox.SelectedIndex = 0;
            }
        }

        private bool userDataHasSubdirectory(Dictionary<String, List<String>> userDataStructure, String subDirectoryName)
        {
            if (subDirectoryName == null)
                return false;
            if (!userDataStructure.ContainsKey(subDirectoryName))
                return false;
            if (userDataStructure[subDirectoryName] == null)
                return false;
            return true;
        }

        private void enableAllocationSpecificInput()
        {
            parkingOCTLabel.Enabled = (allocationType == Allocation.PARKING_ALLOCATION_TYPE);
            parkingOCTComboBox.Enabled = (allocationType == Allocation.PARKING_ALLOCATION_TYPE);
            parkingOCTUseExceptionsCheckBox.Enabled = (allocationType == Allocation.PARKING_ALLOCATION_TYPE);

            aircraftLinksLabel.Enabled = (allocationType == Allocation.PARKING_ALLOCATION_TYPE);
            aircraftLinksComboBox.Enabled = (allocationType == Allocation.PARKING_ALLOCATION_TYPE);
            disableAircraftLinksCheckBox.Enabled = (allocationType == Allocation.PARKING_ALLOCATION_TYPE);

            parkingPrioritiesLabel.Enabled = (allocationType == Allocation.PARKING_ALLOCATION_TYPE);
            parkingPrioritiesComboBox.Enabled = (allocationType == Allocation.PARKING_ALLOCATION_TYPE);

            boardingGatesGroupBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);            

            boardingEnteringSpeedLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            boardingEnteringSpeedTextBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            boardingExitingSpeedLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            boardingExitingSpeedTextBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);

            //delayBetweenBoardingsLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            //delayBetweenBoardingsTextBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);

            lowerLimitLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            limitForLargeAircraftsTextBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);

            bgOCTLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            bgOCTcomboBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            bgOCTUseExceptionsCheckBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);

            loadingFactorsLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE && !disableLoadingFactors);
            loadingFactorsComboBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE && !disableLoadingFactors);
            loadingFactorsUseExceptionsCheckBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE && !disableLoadingFactors);
            disableLoadingFactorsCheckBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);

            boardingGatesPrioritiesLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            boardingGatesPrioritiesComboBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);

            ciOCTLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE || allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE);
            ciOCTcomboBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE || allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE);
            ciOCTUseExceptionsCheckBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE || allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE);
            
            ciOCTAirlineAirportLabel.Enabled = (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE);
            ciOCTAirportAirlineComboBox.Enabled = (allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE);

            checkInShowUpGroupBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            ignoreCheckInShowUpCheckBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            ciShowUpLabel.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            ciShowUpComboBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
            ciShowUpUseExceptionsCheckBox.Enabled = (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE);
        }

        private void setDefaultTimeStepValue()
        {
            if (donnees != null && donnees.AllocationStep > 0)
            {
                timeStepTextBox.Text = donnees.AllocationStep.ToString();
            }
        }

        private void setDefaultAnalysisRangeValue()
        {
            analysisRangeTextBox.Text = DEFAULT_ANALYSIS_RANGE_IN_MINUTES.ToString();
        }

        private void setDefaultDelayBetweenConsecutiveFlights()
        {
            delayBetweenFlightsTextBox.Text = DEFAULT_DELAY_BETWEEN_CONSECUTIVE_FLIGHTS_MINUTES.ToString();
        }
        
        private void setDefaultDelayBetweenConsecutiveBoardings()
        {
            //delayBetweenBoardingsTextBox.Text = DEFAULT_DELAY_BETWEEN_CONSECUTIVE_BOARDINGS_MINUTES.ToString();
        }
        private void setDefaultBoardingSpeed()
        {
            boardingEnteringSpeedTextBox.Text = DEFAULT_BOARDING_ENTERING_SPEED_PAX_PER_MINUTES.ToString();
            boardingExitingSpeedTextBox.Text = DEFAULT_BOARDING_EXITING_SPEED_PAX_PER_MINUTES.ToString();
        }
        private void setDefaultNbSeatsLowerLimit()
        {
            limitForLargeAircraftsTextBox.Text = DEFAULT_NB_SEATS_LOWER_LIMIT.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (scenarioName == null || scenarioName == "")
            {
                MessageBox.Show("Please specify a scenario name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                scenarioNameTextBox.Focus();
                DialogResult = DialogResult.None;
                return;
            }
            List<String> scenarioNames = donnees.getScenarioNames();
            if (scenarioNames.Contains(scenarioName)
                && (initialParameters == null || initialParameters.name != scenarioName))
            {
                MessageBox.Show("The name for your scenario is already used. Please specify a new one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (!checkInAllocationTypeRadioButton.Checked && !boardingGateAllocationTypeRadioButton.Checked 
                && !parkingAllocationTypeRadioButton.Checked)
            {
                MessageBox.Show("Please specify an allocation type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
                return;
            }
            if (fromDatePicker.Value > toDatePicker.Value)
            {
                MessageBox.Show("Please specify valid dates", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fromDatePicker.Focus();
                DialogResult = DialogResult.None;
                return;
            }
            if (updateFPwithAllocation)
            {
                string warningMessage = "";
                if (allocationType == Allocation.PARKING_ALLOCATION_TYPE)                
                    warningMessage = "The current root Departure and Arrival Flight Plan tables will be overwritten. Are you sure you want to continue?";                
                else if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE || allocationType == Allocation.CHECK_IN_ALLOCATION_TYPE)                
                    warningMessage = "The current root Departure Flight Plan table will be overwritten. Are you sure you want to continue?";
                
                DialogResult warningDialogResult
                = MessageBox.Show(warningMessage,
                                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (warningDialogResult == DialogResult.No)
                {
                    DialogResult = DialogResult.Abort;
                    return;
                }
            }
        }

        private void departureFlightPlanComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            departureFlightPlan = donnees.getTable("Input", departureFlightPlanComboBox.Text);
            setUpTimePickers();
        }

        private void setUpTimePickers()
        {
            DateTime minDate = new DateTime();
            DateTime maxDate = new DateTime();

            if (getMinMaxDateFromFlightPlan(departureFlightPlan,
                                            out minDate, out maxDate))
            {
                if (PAX2SIM.dubaiMode)
                {
                    //flights with a large down time get splited and some may not be included in the FP period
                    //Flights with down time greater than 7h get splitted into STA + 2h and STD - 2h.
                    minDate = minDate.AddDays(-1);
                    maxDate = maxDate.AddDays(1);
                }

                fromDatePicker.MinDate = DateTimePicker.MinimumDateTime;
                fromDatePicker.MaxDate = DateTimePicker.MaximumDateTime;
                
                fromDatePicker.MinDate = minDate.AddDays(-1);
                fromDatePicker.MaxDate = maxDate;

                toDatePicker.MinDate = DateTimePicker.MinimumDateTime;
                toDatePicker.MaxDate = DateTimePicker.MaximumDateTime;

                toDatePicker.MinDate = minDate;
                toDatePicker.MaxDate = maxDate.AddDays(1);

                fromDatePicker.Value = minDate.Date;
                toDatePicker.Value = maxDate;
            }

        }

        private bool getMinMaxDateFromFlightPlan(DataTable flightPlan,
            out DateTime minDate, out DateTime maxDate)
        {
            if (flightPlan == null)
            {
                minDate = DateTime.MinValue;
                maxDate = DateTime.MaxValue;
                return false;
            }
            minDate = OverallTools.DataFunctions.valeurMinimaleDansColonne(flightPlan, 1, 2);
            maxDate = OverallTools.DataFunctions.valeurMaximaleDansColonne(flightPlan, 1, 2);

            if (maxDate == DateTime.MinValue
                || minDate == DateTime.MinValue)
            {
                return false;
            }
            return true;
        }

        private void allocationTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            setUpComboBoxes();
            enableAllocationSpecificInput();

            if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE)
            {
                setDefaultDelayBetweenConsecutiveBoardings();
                setDefaultBoardingSpeed();
                setDefaultNbSeatsLowerLimit();
            }
            setUpSortingTypes();
        }

        private void delayBetweenFlightsTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.delayBetweenFlightsTextBox);
        }      

        private void timeStepTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.timeStepTextBox);
        }

        private void restrictOnlyDigitsInTextBox(TextBox textBox)
        {
            bool enteredLetter = false;
            Queue<char> text = new Queue<char>();
            foreach (var ch in textBox.Text)
            {
                if (char.IsDigit(ch))
                {
                    text.Enqueue(ch);
                }
                else
                {
                    enteredLetter = true;
                }
            }
            if (enteredLetter)
            {
                StringBuilder sb = new StringBuilder();
                while (text.Count > 0)
                {
                    sb.Append(text.Dequeue());
                }

                textBox.Text = sb.ToString();
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void scenarioNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((scenarioNameTextBox.Text.Contains("/")) || (scenarioNameTextBox.Text.Contains("\\")))
            {
                String scenarioName = scenarioNameTextBox.Text;

                scenarioName = scenarioName.Replace("/", "_");
                scenarioName = scenarioName.Replace("\\", "_");
                scenarioNameTextBox.Text = scenarioName;
            }
        }

        private void boardingEnteringSpeedTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.boardingEnteringSpeedTextBox);
        }

        private void delayBetweenBoardingsTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void limitForLargeAircraftsTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.limitForLargeAircraftsTextBox);
        }

        private void analysisRangeTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.analysisRangeTextBox);
        }

        private void boardingExitingSpeedTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.boardingExitingSpeedTextBox);
        }

        private void ignoreCheckInShowUpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setUpCheckInShowUpData();       
        }

        private void setUpCheckInShowUpData()
        {
            bool ignoreCheckInShowUp = ignoreCheckInShowUpCheckBox.Checked;
            ciShowUpLabel.Enabled = !ignoreCheckInShowUp;
            ciShowUpComboBox.Enabled = !ignoreCheckInShowUp;
            ciShowUpUseExceptionsCheckBox.Enabled = !ignoreCheckInShowUp;

            string previousSelection = null;
            if (ciShowUpComboBox.SelectedItem != null)
            {
                previousSelection = ciShowUpComboBox.SelectedItem.ToString();
            }
            ciShowUpComboBox.Items.Clear();
            if (allocationType == Allocation.BOARDING_GATE_ALLOCATION_TYPE
                && !ignoreCheckInShowUp)
            {
                setUpComboBox(ciShowUpComboBox, GlobalNames.CI_ShowUpTableName);
                if (previousSelection != null && ciShowUpComboBox.Items.Contains(previousSelection))
                {
                    ciShowUpComboBox.SelectedItem = previousSelection;
                }
            }
        }

        private void allocationTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            setUpCheckInShowUpData();
        }

        private void disableAircraftLinksCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (disableAircraftLinksCheckBox.Checked)
            {
                aircraftLinksLabel.Enabled = false;
                aircraftLinksComboBox.Enabled = false;
                aircraftLinksComboBox.Items.Clear();
            }
            else
            {
                aircraftLinksLabel.Enabled = true;
                aircraftLinksComboBox.Enabled = true;
                setUpComboBox(aircraftLinksComboBox, GlobalNames.FPLinksTableName);
            }
        }

        private void disableLoadingFactorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (disableLoadingFactorsCheckBox.Checked)
            {
                loadingFactorsLabel.Enabled = false;
                loadingFactorsComboBox.Enabled = false;
                loadingFactorsComboBox.Items.Clear();
                loadingFactorsUseExceptionsCheckBox.Enabled = false;
            }
            else
            {
                loadingFactorsLabel.Enabled = true;
                loadingFactorsComboBox.Enabled = true;                
                loadingFactorsUseExceptionsCheckBox.Enabled = true;
                setUpComboBox(loadingFactorsComboBox, GlobalNames.FPD_LoadFactorsTableName);
            }
        }

        private void downTimeMaxAcceptedValueTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.downTimeMaxAcceptedValueTextBox);
        }

        private void downTimeAfterSTATextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.downTimeAfterSTATextBox);
        }

        private void downTimeBeforeSTDTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictOnlyDigitsInTextBox(this.downTimeBeforeSTDTextBox);
        }

    
    }
}
