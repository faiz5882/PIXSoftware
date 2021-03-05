using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Prompt.Liege
{
    public partial class FlightPlanImportAssistant : Form
    {
        GestionDonneesHUB2SIM donnees;
        DataManagerInput dataManagerInput;

        internal NormalTable flightPlan
        {
            get
            {
                addTableFromComboBoxToUserData(flightPlanComboBox);
                NormalTable userDataFlightPlan = donnees.GetFormatedUserDataTable(flightPlanComboBox.Text);

                if (userDataFlightPlan == null
                    || !LiegeTools.areColumnHeadersFromFileValid(userDataFlightPlan.Table, LiegeTools.flightPlanFileColumnNames))
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

        public FlightPlanImportAssistant(GestionDonneesHUB2SIM _donnees)
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

        private void updateDatePickersWithFlgihtPlanDate()
        {
            DataTable arrivalFP = donnees.getTable("Input", GlobalNames.FPATableName);
            DataTable departureFP = donnees.getTable("Input", GlobalNames.FPDTableName);

            DateTime minFPA = DateTime.MaxValue;
            DateTime maxFPA = DateTime.MinValue;
            DateTime minFPD = DateTime.MaxValue;
            DateTime maxFPD = DateTime.MinValue;

            bool aquiredFromFPA = false;
            bool aquiredFromFPD = false;

            if (arrivalFP != null && arrivalFP.Rows.Count > 0)
            {
                aquiredFromFPA = getMinMaxDateFromFlightPlan(arrivalFP, out minFPA, out maxFPA);
            }
            if (departureFP != null && departureFP.Rows.Count > 0)
            {
                aquiredFromFPD = getMinMaxDateFromFlightPlan(departureFP, out minFPD, out maxFPD);
            }

            if (!aquiredFromFPA && !aquiredFromFPD)
            {
                OverallTools.ExternFunctions.PrintLogFile("Liege Data Integration module: "
                    + "Could not aquire the minimum and maximum dates from the departure and arrival Flight Plans.");
                DateTime now = DateTime.Now;
                now = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                startDatePicker.Value = now;
                endDatePicker.Value = now;
                return;
            }

            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;
            if (aquiredFromFPA && aquiredFromFPD)
            {
                if (minFPA < minFPD)                
                    minDate = minFPA;                
                else
                    minDate = minFPD;
                if (maxFPA > maxFPD)
                    maxDate = maxFPA;                
                else
                    maxDate = maxFPA;
            }
            else if (aquiredFromFPD)
            {
                minDate = minFPD;
                maxDate = maxFPD;
            }
            else if (aquiredFromFPA)
            {
                minDate = minFPA;
                maxDate = maxFPA;
            }

            minDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 0, 0, 0);            
            maxDate = maxDate.Date.AddDays(1);
            maxDate = new DateTime(maxDate.Year, maxDate.Month, maxDate.Day, 0, 0, 0);            

            startDatePicker.MinDate = DateTimePicker.MinimumDateTime;
            startDatePicker.MaxDate = DateTimePicker.MaximumDateTime;

            endDatePicker.MinDate = DateTimePicker.MinimumDateTime;
            endDatePicker.MaxDate = DateTimePicker.MaximumDateTime;

            startDatePicker.Value = minDate;
            endDatePicker.Value = maxDate;                        
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
            if (startDatePicker.Value > endDatePicker.Value)
            {
                MessageBox.Show("Please specify valid dates", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                startDatePicker.Focus();
                DialogResult = DialogResult.None;
                beginSendingTheInfo = false;
                return;
            }
            DialogResult warningDialogResult
                = MessageBox.Show("The current Flight Plans, Aircraft Types and Aircraft links tables will be overwritten. Are you sure you want to continue?",
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
                
        private void FlightPlanImportAssistant_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.OK
                && DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
    }
}
