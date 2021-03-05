using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.DataManagement;
using System.Collections;

namespace SIMCORE_TOOL.Prompt.Athens
{
    public partial class FlightPlanImportAssistantForAIA : Form
    {
        GestionDonneesHUB2SIM donnees;        
        DataManagerInput dataManagerInput;

        internal NormalTable flightPlan
        {
            get
            {
                addTableFromComboBoxToUserData(flightPlanComboBox);
                NormalTable userDataFlightPlan = donnees.GetFormatedUserDataTableForFlightPlan(flightPlanComboBox.Text);
                
                if (userDataFlightPlan == null
                    || !AthensTools.areColumnHeadersFromFileValid(userDataFlightPlan.Table, AthensTools.flightPlanFileColumns))
                {
                    removeComboBoxTableFromUserData(flightPlanComboBox);                    
                    return null;
                }
                flightPlanTable = userDataFlightPlan.Table;
                return userDataFlightPlan;
            }
        }
        internal DataTable flightPlanTable;
       
        internal String flightPlanFileName
        {
            get
            {
                return flightPlanComboBox.Text;
            }
        }

        internal NormalTable makeUpAllocation
        {
            get
            {
                addTableFromComboBoxToUserData(makeUpAllocationComboBox);
                NormalTable userDataMakeUpAllocation = donnees.GetFormatedUserDataTable(makeUpAllocationComboBox.Text);

                if (userDataMakeUpAllocation == null
                    || !AthensTools.areColumnHeadersFromFileValid(userDataMakeUpAllocation.Table,
                                        AthensTools.DeskAllocation.MakeUpAllocation.MAKE_UP_ALLOCATION_COLUMN_NAMES_LIST))
                {
                    removeComboBoxTableFromUserData(makeUpAllocationComboBox);
                    return null;
                }
                return userDataMakeUpAllocation;
            }
        }
        internal String makeUpALlocationFileName
        {
            get
            {
                return makeUpAllocationComboBox.Text;
            }
        }

        #region checkIn, reclaim allocation
        /*
        internal NormalTable checkInAllocation
        {
            get
            {
                addTableFromComboBoxToUserData(checkInAllocationComboBox);
                NormalTable userDataCheckInAllocation = donnees.GetFormatedUserDataTable(checkInAllocationComboBox.Text);

                if (userDataCheckInAllocation == null
                    || !AthensTools.areColumnHeadersFromFileValid(userDataCheckInAllocation.Table, 
                                        AthensTools.DeskAllocation.CheckInAllocation.CHECK_IN_ALLOCATION_COLUMN_NAMES_LIST))
                {
                    removeComboBoxTableFromUserData(checkInAllocationComboBox);
                    return null;
                }
                return userDataCheckInAllocation;
            }
        }
        internal String checkInAllocationFileName
        {
            get
            {
                return checkInAllocationComboBox.Text;
            }
        }
        
        internal NormalTable reclaimAllocation
        {
            get
            {
                addTableFromComboBoxToUserData(reclaimAllocationComboBox);
                NormalTable userDataReclaimAllocation = donnees.GetFormatedUserDataTable(reclaimAllocationComboBox.Text);

                if (userDataReclaimAllocation == null
                    || !AthensTools.areColumnHeadersFromFileValid(userDataReclaimAllocation.Table, AthensTools.reclaimAllocationFileColumns))
                {
                    removeComboBoxTableFromUserData(reclaimAllocationComboBox);
                    return null;
                }
                return userDataReclaimAllocation;
            }
        }
        internal String reclaimAllocationFileName
        {
            get
            {
                return reclaimAllocationComboBox.Text;
            }
        }
*/
        #endregion

        internal NormalTable localAirports
        {
            get
            {
                addTableFromComboBoxToUserData(localAirportsComboBox);
                NormalTable userDataLocalAirports = donnees.GetFormatedUserDataTable(localAirportsComboBox.Text);
                ArrayList errorList = new ArrayList();
                
                if (userDataLocalAirports == null
                    || !AthensTools.areColumnHeadersFromFileValid(userDataLocalAirports.Table, AthensTools.localAirportsFileColumns))
                {
                    removeComboBoxTableFromUserData(localAirportsComboBox);                    
                    return null;
                }
                return userDataLocalAirports;
            }
        }
        internal String localAirportsFileName
        {
            get
            {
                return localAirportsComboBox.Text;
            }
        }

        internal DataTable airlinesDatabaseTable;

        internal DateTime startDate
        {
            get
            {
                return startDatePicker.Value;
            }
        }
        internal DateTime endDate
        {
            get
            {
                return endDatePicker.Value;
            }
        }
        
        public FlightPlanImportAssistantForAIA(GestionDonneesHUB2SIM _donnees)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            if (_donnees != null)
            {
                this.donnees = _donnees;
            }
            else
            {
                MessageBox.Show("Error while accessing the internal project data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.Abort;
                return;
            }
            
            dataManagerInput = (DataManagerInput)donnees.GetDataManager("Input");
            if (dataManagerInput == null)
            {
                MessageBox.Show("Error while accessing the \"Input data\" directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.Abort;
                return;
            }
                 
            //The Airlines database file must be present in the User data directory
            //? to decide how to use(already imported in user data or add from the assistant) the airlineDatabase file
            if (donnees.GetFormatedUserDataTable(AthensTools.AIRLINE_DATABASE_TEXT_FILE_NAME) == null)
            {
                MessageBox.Show("The " + AthensTools.AIRLINE_DATABASE_TEXT_FILE_NAME
                    + " file is not loaded. Please load into the User Data directory the " + AthensTools.AIRLINE_DATABASE_TEXT_FILE_NAME + " .");
                DialogResult = DialogResult.Abort;
                return;
            }
            else
            {
                airlinesDatabaseTable = donnees.GetFormatedUserDataTable(AthensTools.AIRLINE_DATABASE_TEXT_FILE_NAME).Table;
            }

            setComboBoxesAsTagsForCorrespondingOpenDialogButtons();
            updateComboBoxesWithUserDataFileNames();
            updateDatePickersWithFlgihtPlanDate();
        }

        private void addTableFromComboBoxToUserData(ComboBox comboBox)
        {
            if (comboBox == null || comboBox.Tag == null)
                return;            

            String fileNameToLoad = comboBox.Text;

            if (donnees.IsUserData(fileNameToLoad))
                return;

            String userDataSubDirectoryForFile = comboBox.Tag.ToString();
            String fileNameWithExtension = System.IO.Path.GetFileName(fileNameToLoad);

            donnees.AddUserData(userDataSubDirectoryForFile, fileNameToLoad);            
            comboBox.Text = fileNameWithExtension;
            updateComboBox(comboBox, donnees.getUserData());
        }
        private void removeComboBoxTableFromUserData(ComboBox comboBox)
        {
            if (comboBox == null || comboBox.Tag == null || comboBox.Text == "")
                return;

            String fileNameToRemove = comboBox.Text;

            if (donnees.IsUserData(fileNameToRemove))
            {
                String userDataSubDirectoryForFile = comboBox.Tag.ToString();

                dataManagerInput.removeUserData(userDataSubDirectoryForFile, fileNameToRemove);
                updateComboBox(comboBox, donnees.getUserData());
            }
        }

        private void setComboBoxesAsTagsForCorrespondingOpenDialogButtons()
        {
            openFlightPlanFileDialogButton.Tag = flightPlanComboBox;
            //openCheckInAllocationFileDialogButton.Tag = checkInAllocationComboBox;
            openMakeUpAllocationFileDialogButton.Tag = makeUpAllocationComboBox;
            //openReclaimAllocationFileDialogButton.Tag = reclaimAllocationComboBox;
            openLocalAirportsFileDialogButton.Tag = localAirportsComboBox;
        }

        /// <summary>
        /// When a file is saved in the User Data directory a subdirectory with the name of the file is created and the file added there.
        /// Here we check if we have in User data a directory with the same name as the standard name for the file loaded with the combobox.(ex: FlightPlan.txt)
        /// If we find such directory we load all the files that it contains in the combobox's list.
        /// </summary>
        private void updateComboBoxesWithUserDataFileNames()
        {
            Dictionary<String, List<String>> userDataDirectoriesAndFilesDictionary = donnees.getUserData();
            updateComboBox(flightPlanComboBox, userDataDirectoriesAndFilesDictionary);
            //updateComboBox(checkInAllocationComboBox, userDataDirectoriesAndFilesDictionary);
            updateComboBox(makeUpAllocationComboBox, userDataDirectoriesAndFilesDictionary);
            //updateComboBox(reclaimAllocationComboBox, userDataDirectoriesAndFilesDictionary);
            updateComboBox(localAirportsComboBox, userDataDirectoriesAndFilesDictionary);
        }
        private void updateComboBox(ComboBox comboBox, Dictionary<String, List<String>> userDataStructure)
        {
            if (comboBox == null || comboBox.Tag == null
                || userDataStructure == null)
            {
                return;
            }
            String selectedTable = comboBox.Text;

            comboBox.Items.Clear();
            comboBox.Text = "";
                        
            String comboBoxStandardFileName = comboBox.Tag.ToString();

            if (userDataHasSubdirectory(userDataStructure, comboBoxStandardFileName))
            {
                foreach (String value in userDataStructure[comboBoxStandardFileName])
                {
                    comboBox.Items.Add(value);
                }
                comboBox.Text = selectedTable;
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

        private void openFileDialog_Click(object sender, EventArgs e)
        {
            if (!isSentByButton(sender) 
                || !senderButtonHasComboBoxTag((Button)sender))
            {
                return;
            }
            
            ComboBox comboBox = (ComboBox)((Button)sender).Tag;
            if (comboBox.Text != "")
            {
                openFileDialogElement.FileName = comboBox.Text;
            }
            if (openFileDialogElement.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            comboBox.Text = openFileDialogElement.FileName;
            if (comboBox == flightPlanComboBox)
            {            
                updateDatePickersWithFlgihtPlanDate();
                MessageBox.Show("Please check the data extraction period.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);            
            }
        }

        private bool isSentByButton(object sender)
        {
            if (sender != null && sender.GetType() == typeof(Button))
            {
                return true;
            }
            return false;
        }
        private bool senderButtonHasComboBoxTag(Button button)
        {
            if (button != null && button.Tag != null
                && button.Tag.GetType() == typeof(ComboBox))
            {
                return true;
            }
            return false;
        }

        private void FlightPlanImportAssistantForAIA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.OK
                && DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        bool beginSendingTheInfo = false;
        private void okButton_Click(object sender, EventArgs e)
        {
            beginSendingTheInfo = true;

            if (flightPlanComboBox.Text == "")
            {
                MessageBox.Show("Please select the \"Flight plan\" file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                flightPlanComboBox.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }
            else if (flightPlan == null)
            {
                MessageBox.Show("The \"Flight plan\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                flightPlanComboBox.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }
            /*
            if ((checkInAllocationComboBox.Text == "") || (checkInAllocation == null))
            {
                MessageBox.Show("The \"CheckIn Allocation\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                checkInAllocationComboBox.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }*/
            if (makeUpAllocationComboBox.Text == "")
            {
                MessageBox.Show("Please select the \"MakeUp Allocation\" file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                makeUpAllocationComboBox.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }
            else if (makeUpAllocation == null)
            {
                MessageBox.Show("The \"MakeUp Allocation\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                makeUpAllocationComboBox.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }
            /*
            if ((reclaimAllocationComboBox.Text == "") || (reclaimAllocation == null))
            {
                MessageBox.Show("The \"Reclaim Allocation\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                reclaimAllocationComboBox.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }*/
            if (localAirportsComboBox.Text == "")
            {
                MessageBox.Show("Please select the \"Local Airports\" file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                localAirportsComboBox.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }
            else if (localAirports == null)
            {
                MessageBox.Show("The \"Local Airports\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                localAirportsComboBox.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }
            if (startDatePicker.Value > endDatePicker.Value)
            {
                MessageBox.Show("Please specify valid dates", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                startDatePicker.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }
            DialogResult warningDialogResult
                = MessageBox.Show("The current Flight Plans will be overwritten. Are you sure you want to continue?",
                                    "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (warningDialogResult == DialogResult.No)
            {
                DialogResult = DialogResult.Abort;
            }
            else if (warningDialogResult == DialogResult.Cancel)
            {
                DialogResult = DialogResult.None;
                return;
            }
        }

        private void beginDatePicker_ValueChanged(object sender, EventArgs e)
        {
            endDatePicker.MinDate = startDatePicker.Value;
        }
                
        private void flightPlanComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!beginSendingTheInfo)
            {
                updateDatePickersWithFlgihtPlanDate();
            }
        }

        private void updateDatePickersWithFlgihtPlanDate()
        {
            DataTable arrivalFP = donnees.getTable("Input", GlobalNames.FPATableName);
            DataTable departureFP = donnees.getTable("Input", GlobalNames.FPDTableName);

            DateTime minFPA = new DateTime();
            DateTime maxFPA = new DateTime();
            DateTime minFPD = new DateTime();
            DateTime maxFPD = new DateTime();

            bool aquiredFromFPA = false;
            bool aquiredFromFPD = false;

            if (arrivalFP != null)
            {
                aquiredFromFPA = getMinMaxDateFromFlightPlan(arrivalFP, out minFPA, out maxFPA);
            }
            if (departureFP != null)
            {
                aquiredFromFPD = getMinMaxDateFromFlightPlan(departureFP, out minFPD, out maxFPD);
            }

            if (!aquiredFromFPA && !aquiredFromFPD)
            {
                OverallTools.ExternFunctions.PrintLogFile("Athens Data Integration module: "
                    + "Could not aquire the minimum and maximum dates from the departure and arrival Flight Plans.");
                return;
            }

            if (aquiredFromFPA && aquiredFromFPD)
            {
                if (minFPA > minFPD)
                {
                    minFPA = minFPD;
                }
                if (maxFPA < maxFPD)
                {
                    maxFPA = maxFPD;
                }
            }
            else if (aquiredFromFPD)
            {
                minFPA = minFPD;
                maxFPA = maxFPD;
            }

            minFPD = minFPA.Date.AddDays(-1);
            maxFPD = maxFPA.Date.AddDays(2);
            
            startDatePicker.MinDate = DateTimePicker.MinimumDateTime;
            startDatePicker.MaxDate = DateTimePicker.MaximumDateTime;

            endDatePicker.MinDate = DateTimePicker.MinimumDateTime;
            endDatePicker.MaxDate = DateTimePicker.MaximumDateTime;

            if (startDatePicker.Value < minFPD
                || startDatePicker.Value > maxFPD)
            {
                startDatePicker.Value = minFPA.Date;
            }
            if (endDatePicker.Value < minFPD
                || endDatePicker.Value > maxFPD)
            {
                endDatePicker.Value = maxFPA.Date.AddHours(23).AddMinutes(59);
            }
            if (endDatePicker.Value < startDatePicker.Value)
            {
                endDatePicker.Value = maxFPD;
            }

            //startDatePicker.MinDate = minFPD;
            //startDatePicker.MaxDate = maxFPD;

            //endDatePicker.MinDate = minFPD;
            //endDatePicker.MaxDate = maxFPD;
        }
        private bool getMinMaxDateFromFlightPlan(DataTable flightPlan, 
            out DateTime minDate, out DateTime maxDate)
        {
            if (flightPlan == null)
            {
                minDate = DateTime.MinValue;
                maxDate = DateTime.MinValue;
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
    }
}