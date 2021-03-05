using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Prompt.CDG
{
    public partial class FPDImportForCDGAssistant : Form
    {
        GestionDonneesHUB2SIM donnees;
        DataManagerInput dataManagerInput;

        internal NormalTable fpTable
        {
            get
            {
                addTableFromComboBoxToUserData(FPcomboBox);
                NormalTable userDataFP = donnees.GetFormatedUserDataTable(FPcomboBox.Text);

                if (userDataFP == null)
                {
                    removeComboBoxTableFromUserData(FPcomboBox);
                    return null;
                }
                return userDataFP;
            }
        }

        internal String fpFileName
        {
            get
            {
                return FPcomboBox.Text;
            }
        }

        internal NormalTable s3InterfaceTable
        {
            get
            {
                addTableFromComboBoxToUserData(S3InterfaceFileComboBox);
                NormalTable userDataS3Interface = donnees.GetFormatedUserDataTable(S3InterfaceFileComboBox.Text);

                if (userDataS3Interface == null)
                {
                    removeComboBoxTableFromUserData(S3InterfaceFileComboBox);
                    return null;
                }
                return userDataS3Interface;
            }
        }

        internal String s3InterfaceFileName
        {
            get
            {
                return S3InterfaceFileComboBox.Text;
            }
        }

        internal NormalTable s4ScenarioTable
        {
            get
            {
                addTableFromComboBoxToUserData(S4ScenarioFileComboBox);
                NormalTable userDataS4Scenario = donnees.GetFormatedUserDataTable(S4ScenarioFileComboBox.Text);

                if (userDataS4Scenario == null)
                {
                    removeComboBoxTableFromUserData(S4ScenarioFileComboBox);
                    return null;
                }
                return userDataS4Scenario;
            }
        }

        internal String s4ScenarioFileName
        {
            get
            {
                return S4ScenarioFileComboBox.Text;
            }
        }

        public FPDImportForCDGAssistant(GestionDonneesHUB2SIM _donnees)
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

            openS3InterfaceFileDialogButton.Tag = S3InterfaceFileComboBox;
            openS4ScenarioFileDialogButton.Tag = S4ScenarioFileComboBox;
            openflightPlanButton.Tag = FPcomboBox;

            updateComboBoxesWithUserDataFileNames();
        }
        
        private void updateComboBoxesWithUserDataFileNames()
        {
            Dictionary<String, List<String>> userDataDirectoriesAndFilesDictionary = donnees.getUserData();
            updateComboBox(S3InterfaceFileComboBox, userDataDirectoriesAndFilesDictionary);
            updateComboBox(S4ScenarioFileComboBox, userDataDirectoriesAndFilesDictionary);
            updateComboBox(FPcomboBox, userDataDirectoriesAndFilesDictionary);
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
                openFileDialog1.FileName = comboBox.Text;
            }
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            comboBox.Text = openFileDialog1.FileName;
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

        private void FPDImportForCDGAssistant_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.OK
                && DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if ((S3InterfaceFileComboBox.Text == "") || (s3InterfaceTable == null) || fpTable == null)
            {
                MessageBox.Show("The \"Flight plan\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                S3InterfaceFileComboBox.Focus();
                DialogResult = DialogResult.None;
                return;
            }
            if ((S4ScenarioFileComboBox.Text == "") || (s4ScenarioTable == null))
            {
                MessageBox.Show("The \"Flight plan\" table could not be loaded. Please check the Log.txt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                S4ScenarioFileComboBox.Focus();
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult warningDialogResult
                = MessageBox.Show("The current Departure Flight Plan table will be overwritten. Are you sure you want to continue?",
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
