using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class OCT_Assistant_CheckIn : Form
    {
        private DataTable openingClosingTimesTable;
        private bool wasModified = false;
        private string previousSelectedFlightCategory = "";

        private int openingTimeInMinutes
        {
            get
            {
                int nbMinutes = Int32.MinValue;
                if (Int32.TryParse(openingTimeTextBox.Text, out nbMinutes))
                    return nbMinutes;
                return nbMinutes;
            }
        }
        
        private int closingTimeInMinutes
        {
            get
            {
                int nbMinutes = Int32.MinValue;
                if (Int32.TryParse(closingTimeTextBox.Text, out nbMinutes))
                    return nbMinutes;
                return nbMinutes;
            }
        }

        private int partialOpeningTimeInMinutes
        {
            get
            {
                int nbMinutes = Int32.MinValue;
                if (Int32.TryParse(partialOpeningTimeTextBox.Text, out nbMinutes))
                    return nbMinutes;
                return nbMinutes;
            }
        }

        private int nbAllocatedCheckIns
        {
            get
            {
                int nbStations = Int32.MinValue;
                if (Int32.TryParse(nbAllocatedCheckInsTextBox.Text, out nbStations))
                    return nbStations;
                return nbStations;
            }
        }

        private int nbOpenedCheckInsAtPartialOpening
        {
            get
            {
                int nbStations = Int32.MinValue;
                if (Int32.TryParse(nbOpenedCheckInsAtPartialOpeningTextBox.Text, out nbStations))
                    return nbStations;
                return nbStations;
            }
        }

        private int nbAdditionalCheckInsForOverlappingFlights
        {
            get
            {
                int nbStations = Int32.MinValue;
                if (Int32.TryParse(nbAdditionalCheckInsForOverlappingFlightsTextBox.Text, out nbStations))
                    return nbStations;
                return nbStations;
            }
        }

        public OCT_Assistant_CheckIn(DataTable _openingClosingTimesTable)
        {
            initializeDesign();
            openingClosingTimesTable = _openingClosingTimesTable;
            setUpFlightCategoriesComboBox(openingClosingTimesTable, "");
            updateFormFieldsFromOpeningClosingTimesTable(openingClosingTimesTable, flightCategoriesComboBox.Text);
        }

        public OCT_Assistant_CheckIn(DataTable _openingClosingTimesTable,
            string selectedFlightCategory)
        {
            initializeDesign();
            openingClosingTimesTable = _openingClosingTimesTable;
            setUpFlightCategoriesComboBox(openingClosingTimesTable, selectedFlightCategory);
            updateFormFieldsFromOpeningClosingTimesTable(openingClosingTimesTable, flightCategoriesComboBox.Text);
        }

        private void initializeDesign()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        private void setUpFlightCategoriesComboBox(DataTable openingClosingTimesTable,
            string selectedFlightCategory)
        {
            if (openingClosingTimesTable != null
                && openingClosingTimesTable.Columns.Count != 1)
            {
                foreach (DataColumn colonne in openingClosingTimesTable.Columns)
                {
                    if (colonne.ColumnName != GlobalNames.sColumnSelectACategory)
                    {
                        flightCategoriesComboBox.Items.Add(colonne.ColumnName);
                    }
                }
                if (flightCategoriesComboBox.Items.Contains(selectedFlightCategory))
                    flightCategoriesComboBox.Text = selectedFlightCategory;
                else if (flightCategoriesComboBox.Items.Count > 0)
                    flightCategoriesComboBox.SelectedIndex = 0;
            }        
        }

        private void updateFormFieldsFromOpeningClosingTimesTable(DataTable openingClosingTimesTable,
            string selectedFlightCategory)
        {
            if (openingClosingTimesTable == null)
                return;

            if (!openingClosingTimesTable.Columns.Contains(selectedFlightCategory))
            {
                MessageBox.Show("Unable to find the selected flight category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int flightCategoryColumnIndex = openingClosingTimesTable.Columns.IndexOf(selectedFlightCategory);            
            int openingTimeColumnIndex 
                = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_Opening);
            int closingTimeColumnIndex 
                = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_Closing);
            int partialOpeningTimeColumnIndex 
                = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_PartialOpeningTime);
            int nbAllocatedCheckInsColumnIndex 
                = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_Allocated);
            int nbCheckInsOpenedAtPartialOpeningColumnIndex
                = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_NbStationsOpenedAtPartial);
            int nbAdditionalStationsForOverlappingColumnIndex
                = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_NbAdditionalStationsForOverlappingFlights);

            if (flightCategoryColumnIndex == -1 || openingTimeColumnIndex == -1
                || closingTimeColumnIndex == -1 || partialOpeningTimeColumnIndex == -1
                || nbAllocatedCheckInsColumnIndex == -1 || nbCheckInsOpenedAtPartialOpeningColumnIndex == -1
                || nbAdditionalStationsForOverlappingColumnIndex == -1)
            {
                return;
            }
            openingTimeTextBox.Text = openingClosingTimesTable.Rows[openingTimeColumnIndex][flightCategoryColumnIndex].ToString();
            closingTimeTextBox.Text = openingClosingTimesTable.Rows[closingTimeColumnIndex][flightCategoryColumnIndex].ToString();
            partialOpeningTimeTextBox.Text = openingClosingTimesTable.Rows[partialOpeningTimeColumnIndex][flightCategoryColumnIndex].ToString();

            nbAllocatedCheckInsTextBox.Text 
                = openingClosingTimesTable.Rows[nbAllocatedCheckInsColumnIndex][flightCategoryColumnIndex].ToString();
            nbOpenedCheckInsAtPartialOpeningTextBox.Text 
                = openingClosingTimesTable.Rows[nbCheckInsOpenedAtPartialOpeningColumnIndex][flightCategoryColumnIndex].ToString();
            nbAdditionalCheckInsForOverlappingFlightsTextBox.Text 
                = openingClosingTimesTable.Rows[nbAdditionalStationsForOverlappingColumnIndex][flightCategoryColumnIndex].ToString();            
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            saveIfMadeChanges();
        }

        private void saveIfMadeChanges()
        {
            if ((previousSelectedFlightCategory != "") && (wasModified))
            {
                if (openingTimeTextBox.Text == "" || closingTimeTextBox.Text == "" || partialOpeningTimeTextBox.Text == "")
                {
                    MessageBox.Show("Please enter valid values for the openning/closing times", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.None;
                    return;
                }
                if ((previousSelectedFlightCategory != flightCategoriesComboBox.Text) || (DialogResult == DialogResult.OK))
                {
                    DialogResult drSave = MessageBox.Show("Do you want to save the changes for the flight category " + "\"" + flightCategoriesComboBox.Text + "\"" + " ? ",
                                            "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        flightCategoriesComboBox.Text = previousSelectedFlightCategory;
                        DialogResult = DialogResult.None;
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        if (!checkDataOnForm())                        
                            DialogResult = DialogResult.None; 
                        else
                            saveFlightCategoryData(openingClosingTimesTable, previousSelectedFlightCategory);
                    }
                }
            }
        }

        private void saveFlightCategoryData(DataTable openingClosingTimesTable, String previousSelectedFlightCategpry)
        {
            int flightCategoryColumnIndex = openingClosingTimesTable.Columns.IndexOf(previousSelectedFlightCategpry);

            int openingTimeColumnIndex = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_Opening);
            int closingTimeColumnIndex = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_Closing);
            int partialOpeningTimeColumnIndex = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_PartialOpeningTime);

            int nbAllocatedCheckInsColumnIndex = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_Allocated);
            int nbCheckInsOpenedAtPartialOpeningColumnIndex 
                = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_NbStationsOpenedAtPartial);
            int nbAdditionalStationsForOverlappingColumnIndex 
                = OverallTools.DataFunctions.indexLigne(openingClosingTimesTable, 0, GlobalNames.sOCT_CI_Line_NbAdditionalStationsForOverlappingFlights);

            if (flightCategoryColumnIndex == -1 || openingTimeColumnIndex == -1 
                || closingTimeColumnIndex == -1 || partialOpeningTimeColumnIndex == -1
                || nbAllocatedCheckInsColumnIndex == -1 || nbCheckInsOpenedAtPartialOpeningColumnIndex == -1 
                || nbAdditionalStationsForOverlappingColumnIndex == -1)
            {
                return;
            }
                        
            openingClosingTimesTable.Rows[openingTimeColumnIndex].BeginEdit();
            openingClosingTimesTable.Rows[openingTimeColumnIndex][flightCategoryColumnIndex] = openingTimeInMinutes;
            openingClosingTimesTable.Rows[closingTimeColumnIndex].BeginEdit();
            openingClosingTimesTable.Rows[closingTimeColumnIndex][flightCategoryColumnIndex] = closingTimeInMinutes;
            openingClosingTimesTable.Rows[partialOpeningTimeColumnIndex].BeginEdit();
            openingClosingTimesTable.Rows[partialOpeningTimeColumnIndex][flightCategoryColumnIndex] = partialOpeningTimeInMinutes;

            openingClosingTimesTable.Rows[nbAllocatedCheckInsColumnIndex].BeginEdit();
            openingClosingTimesTable.Rows[nbAllocatedCheckInsColumnIndex][flightCategoryColumnIndex] = nbAllocatedCheckIns;
            openingClosingTimesTable.Rows[nbCheckInsOpenedAtPartialOpeningColumnIndex].BeginEdit();
            openingClosingTimesTable.Rows[nbCheckInsOpenedAtPartialOpeningColumnIndex][flightCategoryColumnIndex] = nbOpenedCheckInsAtPartialOpening;
            openingClosingTimesTable.Rows[nbAdditionalStationsForOverlappingColumnIndex].BeginEdit();
            openingClosingTimesTable.Rows[nbAdditionalStationsForOverlappingColumnIndex][flightCategoryColumnIndex] = nbAdditionalCheckInsForOverlappingFlights;            
        }

        private bool checkDataOnForm()
        {            
            if (openingTimeInMinutes < 0)
            {
                MessageBox.Show("The value for the Opening Time must be a number (>=0).",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }            
            if (closingTimeInMinutes < 0)
            {
                MessageBox.Show("The value for the Closing Time must be a number (>=0).",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }            
            if (partialOpeningTimeInMinutes < 0)
            {
                MessageBox.Show("The value for the Partial Opening Time must be a number (>=0).",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }   
            if (openingTimeInMinutes <= closingTimeInMinutes)
            {
                MessageBox.Show("The Opening Time must be greater than the Closing Time", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (openingTimeInMinutes < partialOpeningTimeInMinutes)
            {
                MessageBox.Show("The Opening Time must be greater than the Partial Opening Time", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (partialOpeningTimeInMinutes <= closingTimeInMinutes)
            {
                MessageBox.Show("The Partial Opening Time must be greater than the Closing Time",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (nbAllocatedCheckIns < 0)
            {
                MessageBox.Show("The value for the number of allocated check-in stations must be a number (>=0).",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (nbOpenedCheckInsAtPartialOpening < 0)
            {
                MessageBox.Show("The value for the number of opened check-in stations at the partial opening time must be a number (>=0).",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (nbAdditionalCheckInsForOverlappingFlights < -1)
            {
                MessageBox.Show("The value for the number of additional check-in stations for overlapping flights must be a positive number (>=0) or -1 if this option is not used.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void flightCategoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flightCategoriesComboBox.Text == ""
                || previousSelectedFlightCategory == flightCategoriesComboBox.Text)
            {
                return;
            }
            saveIfMadeChanges();
            if (!openingClosingTimesTable.Columns.Contains(flightCategoriesComboBox.Text))
            {
                MessageBox.Show("Unable to find the selected flight category", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            previousSelectedFlightCategory = flightCategoriesComboBox.Text;
            updateFormFieldsFromOpeningClosingTimesTable(openingClosingTimesTable, flightCategoriesComboBox.Text);
            wasModified = false;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            wasModified = true;
            if (flightCategoriesComboBox.Text == ""
                || ((TextBox)sender).Text == "")
            {
                return;
            }            
            int value;
            if (!Int32.TryParse(((TextBox)sender).Text, out value))
            {
                MessageBox.Show("Please enter valid values", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            colorPartialTextboxesByPartialAndNormalValues();
        }

        private void colorPartialTextboxesByPartialAndNormalValues()
        {
            if (openingTimeInMinutes == partialOpeningTimeInMinutes
                && nbAllocatedCheckIns == nbOpenedCheckInsAtPartialOpening)
            {
                partialOpeningTimeTextBox.BackColor = Color.LightGray;
                nbOpenedCheckInsAtPartialOpeningTextBox.BackColor = Color.LightGray;
            }
            else if (openingTimeInMinutes != partialOpeningTimeInMinutes
                && nbAllocatedCheckIns != nbOpenedCheckInsAtPartialOpening)
            {
                partialOpeningTimeTextBox.BackColor = Color.Yellow;
                nbOpenedCheckInsAtPartialOpeningTextBox.BackColor = Color.Yellow;
            }
            else if (openingTimeInMinutes == partialOpeningTimeInMinutes
                && nbAllocatedCheckIns != nbOpenedCheckInsAtPartialOpening)
            {
                partialOpeningTimeTextBox.BackColor = Color.Red;
                nbOpenedCheckInsAtPartialOpeningTextBox.BackColor = Color.Red;
            }
            else
            {
                partialOpeningTimeTextBox.BackColor = Color.White;
                nbOpenedCheckInsAtPartialOpeningTextBox.BackColor = Color.White;
            }
        }
    }
}
