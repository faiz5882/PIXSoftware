using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Prompt.Dubai
{
    public partial class FlightPlanImportAssistantForDubai : Form
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
                    || !DubaiTools.areColumnHeadersFromFileValid(userDataFlightPlan.Table, DubaiTools.FLIGHT_PLAN_FILE_COLUMNS))
                {
                    removeComboBoxTableFromUserData(flightPlanComboBox);
                    return null;
                }
                return userDataFlightPlan;
            }
        }

        internal String flightPlanFileName
        {
            get
            {
                return flightPlanComboBox.Text;
            }
        }

        public FlightPlanImportAssistantForDubai(GestionDonneesHUB2SIM _donnees)
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

        private void FlightPlanImportAssistantForDubai_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.OK
                && DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {            
            if ((flightPlanComboBox.Text == "") || (flightPlan == null))
            {
                MessageBox.Show("The \"Flight plan\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                flightPlanComboBox.Focus();
                DialogResult = DialogResult.None;                
                return;
            }
            DialogResult warningDialogResult
                = MessageBox.Show("The current Flight Plans tables will be overwritten. Are you sure you want to continue?",
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
    }
}
