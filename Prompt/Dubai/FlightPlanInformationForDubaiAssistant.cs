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
    public partial class FlightPlanInformationForDubaiAssistant : Form
    {
        GestionDonneesHUB2SIM donnees;
        DataManagerInput dataManagerInput;

        internal NormalTable filteredFlightPlan
        {
            get
            {
                addTableFromComboBoxToUserData(filteredFlightPlanComboBox);
                NormalTable userDataFilteredFlightPlan = donnees.GetFormatedUserDataTable(filteredFlightPlanComboBox.Text);
                /*
                if (userDataFlightPlan == null
                    || !DubaiTools.areColumnHeadersFromFileValid(userDataFlightPlan.Table, DubaiTools.FLIGHT_PLAN_FILE_COLUMNS))
                {
                    removeComboBoxTableFromUserData(flightPlanComboBox);
                    return null;
                }*/
                return userDataFilteredFlightPlan;
            }
        }
        internal String filteredFlightPlanFileName
        {
            get
            {
                return filteredFlightPlanComboBox.Text;
            }
        }
        internal NormalTable partialAllocationFile
        {
            get
            {
                addTableFromComboBoxToUserData(partialAllocationComboBox);
                NormalTable userDataPartialAllocation = donnees.GetFormatedUserDataTable(partialAllocationComboBox.Text);
                /*
                if (userDataFlightPlan == null
                    || !DubaiTools.areColumnHeadersFromFileValid(userDataFlightPlan.Table, DubaiTools.FLIGHT_PLAN_FILE_COLUMNS))
                {
                    removeComboBoxTableFromUserData(flightPlanComboBox);
                    return null;
                }*/
                return userDataPartialAllocation;
            }
        }
        internal String partialAllocationFileName
        {
            get
            {
                return partialAllocationComboBox.Text;
            }
        }
        internal NormalTable cplexAllocationFile
        {
            get
            {
                addTableFromComboBoxToUserData(cplexAllocationFileComboBox);
                NormalTable userDataCplexAllocation = donnees.GetFormatedUserDataTable(cplexAllocationFileComboBox.Text);
                /*
                if (userDataFlightPlan == null
                    || !DubaiTools.areColumnHeadersFromFileValid(userDataFlightPlan.Table, DubaiTools.FLIGHT_PLAN_FILE_COLUMNS))
                {
                    removeComboBoxTableFromUserData(flightPlanComboBox);
                    return null;
                }*/
                return userDataCplexAllocation;
            }
        }
        internal String cplexAllocationFileName
        {
            get
            {
                return cplexAllocationFileComboBox.Text;
            }
        }

        public FlightPlanInformationForDubaiAssistant(GestionDonneesHUB2SIM _donnees)
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

        private void setComboBoxesAsTagsForCorrespondingOpenDialogButtons()
        {
            openFilteredFlightPlanFileDialog.Tag = filteredFlightPlanComboBox;
            openPartialAllocationFileDialog.Tag = partialAllocationComboBox;
            openCplexAllocationFileDialog.Tag = cplexAllocationFileComboBox;
        }
        private void updateComboBoxesWithUserDataFileNames()
        {
            Dictionary<String, List<String>> userDataDirectoriesAndFilesDictionary = donnees.getUserData();
            updateComboBox(filteredFlightPlanComboBox, userDataDirectoriesAndFilesDictionary);
            updateComboBox(partialAllocationComboBox, userDataDirectoriesAndFilesDictionary);
            updateComboBox(cplexAllocationFileComboBox, userDataDirectoriesAndFilesDictionary);
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

        private void FlightPlanInformationAssistant_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.OK
                && DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if ((filteredFlightPlanComboBox.Text == "") || (filteredFlightPlan == null))
            {
                MessageBox.Show("The \"Filtered Flight plan\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                filteredFlightPlanComboBox.Focus();
                DialogResult = DialogResult.None;
                return;
            }
            if ((partialAllocationComboBox.Text == "") || (partialAllocationFile == null))
            {
                MessageBox.Show("The \"Partial Allocation file\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                partialAllocationComboBox.Focus();
                DialogResult = DialogResult.None;
                return;
            }
            if ((cplexAllocationFileComboBox.Text == "") || (cplexAllocationFile == null))
            {
                MessageBox.Show("The \"CPLEX Allocation file\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cplexAllocationFileComboBox.Focus();
                DialogResult = DialogResult.None;
                return;
            }
        }
    }
}
