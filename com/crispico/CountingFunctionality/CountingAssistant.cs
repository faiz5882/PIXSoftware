using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.com.crispico.CountingFunctionality
{
    // >> Task #10359 Pax2Sim - Counting functionality
    public partial class CountingAssistant : Form
    {
        private String scenarioName;

        private String processObservedName;

        private DataTable istTable;

        private GestionDonneesHUB2SIM donnees;

        public DataTable resultTable
        {
            get { return _resultTable; }
        }

        private DataTable _resultTable;
                
        public DataTable resultedISTTable
        {
            get { return _resultedISTTable; }
        }

        private DataTable _resultedISTTable;

        private string[] attributeTypesList = new string[] {CountingTools.COUNTING_ATTRIBUTE_TYPE_DATETIME, CountingTools.COUNTING_ATTRIBUTE_TYPE_NUMERICAL,
                                                            CountingTools.COUNTING_ATTRIBUTE_TYPE_TEXT, CountingTools.COUNTING_ATTRIBUTE_TYPE_BOOLEAN};

        private string[] timeAttributesList = new string[] { };

        private string[] numericalAttributesList = new string[] { };

        private string[] textAttributesList = new string[] { };

        private string[] comparisonTypesList = new string[] {GlobalNames.COMPARISON_GREATER_THAN, GlobalNames.COMPARISON_GREATER_OR_EQUAL_THAN,
                                                             GlobalNames.COMPARISON_EQUAL, GlobalNames.COMPARISON_LESS_OR_EQUAL_THAN,
                                                             GlobalNames.COMPARISON_LESS_THAN};

        private string[] textComparisonTypesList = new string[] { GlobalNames.COMPARISON_EQUAL, GlobalNames.COMPARISON_CONTAINS };

        private string[] booleanValuesList = new string[] { GlobalNames.BOOLEAN_TRUE_VALUE, GlobalNames.BOOLEAN_FALSE_VALUE };

        public CountingAssistant(String pScenarioName, DataTable pIstTable, GestionDonneesHUB2SIM pDonnees)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            scenarioName = pScenarioName;
            donnees = pDonnees;

            if (pIstTable != null)
            {
                istTable = pIstTable.Copy();
                getProcessObservedNameFromIstTableName(istTable.TableName);

                setUpAttributeTypesComboBox();
            }
        }

        private void getProcessObservedNameFromIstTableName(String istTableName)
        {
            if (istTable != null)
            {
                processObservedName = istTableName
                    .Substring(0, istTableName.IndexOf(GlobalNames.IST_TABLE_SUFFIX)).Trim();
            }
        }



        #region ComboBoxes management
        private void setUpAttributeTypesComboBox()
        {
            if (attributeTypeComboBox != null)
            {
                attributeTypeComboBox.Items.Clear();
                attributeTypeComboBox.Items.AddRange(attributeTypesList);
                if (attributeTypeComboBox.Items.Count > 0)
                    attributeTypeComboBox.SelectedIndex = 0;
            }
        }


        private void attributeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (attributeComboBox != null)
            {
                attributeComboBox.Items.Clear();

                if (attributeTypeComboBox != null && attributeTypeComboBox.SelectedItem != null)
                {
                    String selectedAttributeType = attributeTypeComboBox.SelectedItem.ToString();
                    List<String> attributesList = getAttributesListByTypeFromISTTable(selectedAttributeType, istTable);
                    if (attributesList != null && attributesList.ToArray() != null)
                        attributeComboBox.Items.AddRange(attributesList.ToArray());

                    if (attributeComboBox.Items.Count > 0)
                        attributeComboBox.SelectedIndex = 0;

                    //enable/disable the radio buttons(interval only for numeric or date)
                    if (selectedAttributeType.Equals(CountingTools.COUNTING_ATTRIBUTE_TYPE_TEXT))                        
                    {
                        intervalRadioButton.Enabled = false;
                        valueRadioButton.Checked = true;

                        comparisonTypeComboBox.Enabled = true;
                        comparisonTypeComboBox.Items.Clear();
                        comparisonTypeComboBox.Items.AddRange(textComparisonTypesList);
                        if (comparisonTypeComboBox.Items.Count > 0)
                            comparisonTypeComboBox.SelectedIndex = 0;                        

                        referenceValueTextBox.Show();
                        booleanReferenceValueComboBox.Hide();
                        referenceValueDateTimePicker.Hide();

                        referenceValueTextBox.Clear();
                    }
                    else if (selectedAttributeType.Equals(CountingTools.COUNTING_ATTRIBUTE_TYPE_BOOLEAN))
                    {
                        intervalRadioButton.Enabled = false;
                        valueRadioButton.Checked = true;

                        comparisonTypeComboBox.Items.Clear();
                        comparisonTypeComboBox.Items.Add(GlobalNames.COMPARISON_EQUAL);
                        if (comparisonTypeComboBox.Items.Count > 0)
                            comparisonTypeComboBox.SelectedIndex = 0;
                        comparisonTypeComboBox.Enabled = false;

                        referenceValueTextBox.Hide();
                        referenceValueDateTimePicker.Hide();
                        booleanReferenceValueComboBox.Show();
                        booleanReferenceValueComboBox.Items.Clear();
                        booleanReferenceValueComboBox.Items.AddRange(booleanValuesList);
                        if (booleanReferenceValueComboBox.Items.Count > 0)
                            booleanReferenceValueComboBox.SelectedIndex = 0;
                    }
                    else if (selectedAttributeType.Equals(CountingTools.COUNTING_ATTRIBUTE_TYPE_DATETIME))
                    {
                        intervalRadioButton.Enabled = true;

                        referenceValueDateTimePicker.Show();
                        referenceValueTextBox.Hide();
                        booleanReferenceValueComboBox.Hide();
                        
                        firstValueTextBox.Hide();
                        lastValueTextBox.Hide();
                        firstIintervalValueDateTimePicker.Show();
                        lastIntervalValueDateTimePicker.Show();
                        
                        comparisonTypeComboBox.Enabled = true;
                        comparisonTypeComboBox.Items.Clear();
                        comparisonTypeComboBox.Items.AddRange(comparisonTypesList);
                        if (comparisonTypeComboBox.Items.Count > 0)
                            comparisonTypeComboBox.SelectedIndex = 0;                                                
                    }
                    else if (selectedAttributeType.Equals(CountingTools.COUNTING_ATTRIBUTE_TYPE_NUMERICAL))
                    {
                        intervalRadioButton.Enabled = true;

                        referenceValueTextBox.Show();
                        booleanReferenceValueComboBox.Hide();
                        referenceValueDateTimePicker.Hide();
                        
                        firstValueTextBox.Show();
                        lastValueTextBox.Show();
                        firstIintervalValueDateTimePicker.Hide();
                        lastIntervalValueDateTimePicker.Hide();

                        firstValueTextBox.Clear();
                        lastValueTextBox.Clear();
                        
                        comparisonTypeComboBox.Enabled = true;
                        comparisonTypeComboBox.Items.Clear();
                        comparisonTypeComboBox.Items.AddRange(comparisonTypesList);
                        if (comparisonTypeComboBox.Items.Count > 0)
                            comparisonTypeComboBox.SelectedIndex = 0;

                        referenceValueTextBox.Clear();
                    }
                }
            }
        }

        private List<String> getAttributesListByTypeFromISTTable(String attributeType, DataTable pIstTable)
        {
            List<String> attributesList = new List<String>();

            if (istTable != null && istTable.Columns != null)
            {
                foreach (DataColumn column in istTable.Columns)
                {
                    Type columnType = column.DataType;
                    String columnName = column.ColumnName;

                    if (columnType != null)
                    {
                        switch (attributeType)
                        {
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_DATETIME:
                                {
                                    if (columnType.Equals(typeof(DateTime)))
                                    {
                                        attributesList.Add(columnName);
                                    }
                                    break;
                                }
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_NUMERICAL:
                                {
                                    if (columnType.Equals(typeof(Int16)) || columnType.Equals(typeof(UInt16))
                                        || columnType.Equals(typeof(Int32)) || columnType.Equals(typeof(UInt32))
                                        || columnType.Equals(typeof(Int64)) || columnType.Equals(typeof(UInt64))
                                        || columnType.Equals(typeof(Single)) || columnType.Equals(typeof(Double))
                                        || columnType.Equals(typeof(Decimal)))
                                    {
                                        attributesList.Add(columnName);
                                    }
                                    break;
                                }
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_TEXT:
                                {
                                    if (columnType.Equals(typeof(String)))
                                    {
                                        attributesList.Add(columnName);
                                    }
                                    break;
                                }
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_BOOLEAN:
                                {
                                    if (columnType.Equals(typeof(Boolean)))
                                    {
                                        attributesList.Add(columnName);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }

            return attributesList;
        }
        #endregion

        private void valueRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (valueRadioButton.Checked)
            {                
                referencePanel.Show();                
                intervalPanel.Hide();             
            }
            else
            {
                referencePanel.Hide();
                intervalPanel.Show();
            }
            setDateTimePickersUsingISTTable();
        }

        private void intervalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (intervalRadioButton.Checked)
            {
                intervalPanel.Show();
                referencePanel.Hide();
            }
            else
            {
                intervalPanel.Hide();
                referencePanel.Show();
            }
            setDateTimePickersUsingISTTable();
        }

        private static char[] allowedCharsList = { '\b', '.', '-' };
        
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (attributeTypeComboBox != null)
            {
                String selectedAttributeType = attributeTypeComboBox.SelectedItem.ToString();

                if (selectedAttributeType != null)
                {
                    char[] allowedList = null;
                    if (selectedAttributeType.Equals(CountingTools.COUNTING_ATTRIBUTE_TYPE_NUMERICAL))
                        allowedList = allowedCharsList;                                     

                    if (allowedList != null)
                    {
                        if (Char.IsDigit(e.KeyChar)
                            || Array.IndexOf(allowedList, e.KeyChar) > -1)
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            String selectedAttributeType = "";
            String selectedAttribute = "";
            bool ok = true;
            String errorMessage = "";

            if (verifySelectedAttributeData(out selectedAttributeType, out selectedAttribute))
            {
                int istColumnIndex = istTable.Columns.IndexOf(selectedAttribute);

                if (istColumnIndex != -1)
                {
                    if (valueRadioButton.Checked)
                    {
                        String comparisonType = "";
                        if (comparisonTypeComboBox != null && comparisonTypeComboBox.SelectedItem != null)
                            comparisonType = comparisonTypeComboBox.SelectedItem.ToString();

                        switch (selectedAttributeType)
                        {
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_DATETIME:
                                {
                                    DateTime referenceDate = referenceValueDateTimePicker.Value;
                                    ArrayList rowIndexesList = new ArrayList();

                                    //referenceDate = new DateTime(referenceDate.Year, referenceDate.Month,
                                    //                               referenceDate.Day, referenceDate.Hour, referenceDate.Minute, 0);

                                    if (CountingTools.countByDateValue(istTable, istColumnIndex, referenceDate, comparisonType, out rowIndexesList))
                                    {
                                        //call to create result table and the ist filtered table
                                        if (!updateCountResultTable(selectedAttribute, comparisonType, referenceDate.ToString(), rowIndexesList.Count)
                                            || !createCountedItemsTable(rowIndexesList))
                                        {
                                            ok = false;
                                            errorMessage = "There was an error while creating the count result table for "
                                                                + processObservedName + " (" + selectedAttribute + ").";
                                        }
                                    }
                                    break;
                                }
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_NUMERICAL:
                                {
                                    Double referenceValue = 0;
                                    if (Double.TryParse(referenceValueTextBox.Text, out referenceValue))
                                    {
                                        ArrayList rowIndexesList = new ArrayList();
                                        if (CountingTools.countByNumericalValue(istTable, istColumnIndex, referenceValue, comparisonType, out rowIndexesList))
                                        {
                                            //call to create result table and the ist filtered table
                                            if (!updateCountResultTable(selectedAttribute, comparisonType, referenceValue.ToString(), rowIndexesList.Count)
                                                || !createCountedItemsTable(rowIndexesList))
                                            {
                                                ok = false;
                                                errorMessage = "There was an error while creating the count result table for " 
                                                                    + processObservedName + " (" + selectedAttribute + ").";
                                            }
                                        }
                                    }
                                    break;
                                }
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_TEXT:
                                {
                                    String referenceText = referenceValueTextBox.Text;
                                    ArrayList rowIndexesList = new ArrayList();
                                    if (CountingTools.countByStringValue(istTable, istColumnIndex, referenceText, comparisonType, out rowIndexesList))
                                    {
                                        //call to create result table and the ist filtered table
                                        if (!updateCountResultTable(selectedAttribute, comparisonType, referenceText, rowIndexesList.Count)
                                            || !createCountedItemsTable(rowIndexesList))
                                        {
                                            ok = false;
                                            errorMessage = "There was an error while creating the count result table for "
                                                                + processObservedName + " (" + selectedAttribute + ").";
                                        }
                                    }
                                    break;
                                }
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_BOOLEAN:
                                {
                                    bool referenceBoolean = true;
                                    String booleanReferenceString = "";
                                    if (booleanReferenceValueComboBox.SelectedItem != null)
                                        booleanReferenceString = booleanReferenceValueComboBox.SelectedItem.ToString();

                                    if (Boolean.TryParse(booleanReferenceString, out referenceBoolean))
                                    {
                                        ArrayList rowIndexesList = new ArrayList();
                                        if (CountingTools.countByBooleanValue(istTable, istColumnIndex, referenceBoolean, out rowIndexesList))
                                        {
                                            //call to create result table and the ist filtered table
                                            if (!updateCountResultTable(selectedAttribute, comparisonType, referenceBoolean.ToString(), rowIndexesList.Count)
                                                || !createCountedItemsTable(rowIndexesList))
                                            {
                                                ok = false;
                                                errorMessage = "There was an error while creating the count result table for "
                                                                    + processObservedName + " (" + selectedAttribute + ").";
                                            }
                                        }
                                    }
                                    break;
                                }
                            default: break;
                        }
                    }
                    else if (intervalRadioButton.Checked)
                    {
                        switch (selectedAttributeType)
                        {
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_DATETIME:
                                {
                                    DateTime firstDate = firstIintervalValueDateTimePicker.Value;
                                    DateTime lastDate = lastIntervalValueDateTimePicker.Value;

                                    //firstDate = new DateTime(firstDate.Year, firstDate.Month, firstDate.Day, firstDate.Hour, firstDate.Minute, 0);
                                    //lastDate = new DateTime(lastDate.Year, lastDate.Month, lastDate.Day, lastDate.Hour, lastDate.Minute, 0);

                                    if (firstDate != null && lastDate != null)
                                    {
                                        ArrayList rowIndexesList = new ArrayList();
                                        if (CountingTools.countByDateInterval(istTable, istColumnIndex, includingFirstValueCheckBox.Checked, firstDate,
                                                                                includingLastValueCheckBox.Checked, lastDate, out rowIndexesList))
                                        {
                                            String referenceInterval = getRepresentationForReferenceInterval(includingFirstValueCheckBox.Checked, firstDate.ToString(),
                                                                                                                includingLastValueCheckBox.Checked, lastDate.ToString());
                                            String comparisonType = getRepresentationForComparisonTypeForInterval(includingFirstValueCheckBox.Checked,
                                                                                                                    includingLastValueCheckBox.Checked);
                                            //call to create result table and the ist filtered table
                                            if (!updateCountResultTable(selectedAttribute, comparisonType, referenceInterval, rowIndexesList.Count)
                                                || !createCountedItemsTable(rowIndexesList))
                                            {
                                                ok = false;
                                                errorMessage = "There was an error while creating the count result table for "
                                                                    + processObservedName + " (" + selectedAttribute + ").";
                                            }
                                        }
                                    }
                                    break;
                                }
                            case CountingTools.COUNTING_ATTRIBUTE_TYPE_NUMERICAL:
                                {
                                    Double firstIntervalValue = 0;
                                    Double lastIntervalValue = 0;
                                    if (Double.TryParse(firstValueTextBox.Text, out firstIntervalValue)
                                        && Double.TryParse(lastValueTextBox.Text, out lastIntervalValue))
                                    {
                                        ArrayList rowIndexesList = new ArrayList();
                                        if (CountingTools.countByNumericalInterval(istTable, istColumnIndex, includingFirstValueCheckBox.Checked, firstIntervalValue,
                                                                                    includingLastValueCheckBox.Checked, lastIntervalValue, out rowIndexesList))
                                        {
                                            String referenceInterval = getRepresentationForReferenceInterval(includingFirstValueCheckBox.Checked, firstIntervalValue.ToString(),
                                                                                                                includingLastValueCheckBox.Checked, lastIntervalValue.ToString());
                                            String comparisonType = getRepresentationForComparisonTypeForInterval(includingFirstValueCheckBox.Checked, 
                                                                                                                    includingLastValueCheckBox.Checked);

                                            //call to create result table and the ist filtered table
                                            if (!updateCountResultTable(selectedAttribute, comparisonType, referenceInterval, rowIndexesList.Count)
                                                || !createCountedItemsTable(rowIndexesList))
                                            {
                                                ok = false;
                                                errorMessage = "There was an error while creating the count result table for "
                                                                    + processObservedName + " (" + selectedAttribute + ").";
                                            }
                                        }
                                    }
                                    break;
                                }
                            default: break;
                        }
                    }
                }
            }
        }

        private String getRepresentationForReferenceInterval(bool includingFirst, String firstValue, bool includingLast, String lastValue)
        {
            String referenceInterval = "";
            if (includingFirst)
                referenceInterval += "[";
            else
                referenceInterval += "(";
            referenceInterval += firstValue;

            referenceInterval += " , ";

            referenceInterval += lastValue;

            if (includingLast)
                referenceInterval += "]";
            else
                referenceInterval += ")";

            return referenceInterval;
        }

        private String getRepresentationForComparisonTypeForInterval(bool includingFirst, bool includingLast)
        {
            String comparisonType = "";
            if (includingFirst)
                comparisonType += "Including";
            else
                comparisonType += "Excluding";

            comparisonType += " / ";

            if (includingLast)
                comparisonType += "Including";
            else
                comparisonType += "Excluding";

            return comparisonType;
        }

        private bool verifySelectedAttributeData(out String selectedAttributeType, out String selectedAttribute)
        {
            bool ok = true;
            selectedAttributeType = "";
            selectedAttribute = "";

            if (attributeTypeComboBox != null && attributeTypeComboBox.SelectedItem != null)
                selectedAttributeType = attributeTypeComboBox.SelectedItem.ToString();
            else
                ok = false;

            if (attributeComboBox != null && attributeComboBox.SelectedItem != null)
                selectedAttribute = attributeComboBox.SelectedItem.ToString();
            else
                ok = false;

            return ok;
        }

        private bool updateCountResultTable(String countedAttributeName, String comparisonType, String referenceValue, int totalItems)
        {

            if (scenarioName == null || processObservedName == null || countedAttributeName == null
                || comparisonType == null || referenceValue == null || totalItems < 0)
            {
                return false;
            }

            String resultTableName = processObservedName + GlobalNames.COUNT_RESULT_TABLE_SUFIX;
            _resultTable = donnees.getTable(scenarioName, resultTableName);

            if (_resultTable == null)
            {
                if (processObservedName != null)
                {
                    _resultTable = createCountResultTable(processObservedName);
                    return addRowToCountResultTable(_resultTable, countedAttributeName, comparisonType, referenceValue, totalItems);
                }
            }
            else
            {
                return addRowToCountResultTable(_resultTable, countedAttributeName, comparisonType, referenceValue, totalItems);
            }            
            return true;
        }

        private DataTable createCountResultTable(String processObservedName)
        {   
            String resultTableName = processObservedName + GlobalNames.COUNT_RESULT_TABLE_SUFIX;

            DataTable   resultTable = new DataTable(resultTableName);
            resultTable.Columns.Add(GlobalNames.COUNT_RESULT_ID_COLUMN_NAME, typeof(int));
            resultTable.Columns.Add(GlobalNames.COUNT_RESULT_SCENARIO_NAME_COLUMN_NAME, typeof(String));
            resultTable.Columns.Add(GlobalNames.COUNT_RESULT_PROCESS_OBSERVED_COLUMN_NAME, typeof(String));
            resultTable.Columns.Add(GlobalNames.COUNT_RESULT_ATTRIBUTE_COLUMN_NAME, typeof(String));
            resultTable.Columns.Add(GlobalNames.COUNT_RESULT_COMPARISON_TYPE_COLUMN_NAME, typeof(String));
            resultTable.Columns.Add(GlobalNames.COUNT_RESULT_REFERENCE_VALUE_COLUMN_NAME, typeof(String));
            resultTable.Columns.Add(GlobalNames.COUNT_RESULT_TOTAL_ITEMS_COLUMN_NAME, typeof(int));
            
            return resultTable;
        }

        private bool addRowToCountResultTable(DataTable pResultTable, String countedAttributeName, String comparisonType, String referenceValue, int totalItems)
        {
            if (pResultTable == null)
                return false;

            int idColumnIndex = pResultTable.Columns.IndexOf(GlobalNames.COUNT_RESULT_ID_COLUMN_NAME);
            int scenarioNameColumnIndex = pResultTable.Columns.IndexOf(GlobalNames.COUNT_RESULT_SCENARIO_NAME_COLUMN_NAME);
            int processObservedColumnIndex = pResultTable.Columns.IndexOf(GlobalNames.COUNT_RESULT_PROCESS_OBSERVED_COLUMN_NAME);
            int attributeColumnIndex = pResultTable.Columns.IndexOf(GlobalNames.COUNT_RESULT_ATTRIBUTE_COLUMN_NAME);
            int comparisonTypeColumnIndex = pResultTable.Columns.IndexOf(GlobalNames.COUNT_RESULT_COMPARISON_TYPE_COLUMN_NAME);
            int referenceValueColumnIndex = pResultTable.Columns.IndexOf(GlobalNames.COUNT_RESULT_REFERENCE_VALUE_COLUMN_NAME);
            int totalItemsColumnIndex = pResultTable.Columns.IndexOf(GlobalNames.COUNT_RESULT_TOTAL_ITEMS_COLUMN_NAME);

            if (idColumnIndex == -1 || scenarioNameColumnIndex == -1 || processObservedColumnIndex == -1 || attributeColumnIndex == -1 
                || comparisonTypeColumnIndex == -1 || referenceValueColumnIndex == -1 || totalItemsColumnIndex == -1)
            {
                return false;
            }

            int newResultId = 1;
            if (pResultTable.Rows.Count > 0)
            {
                int lastResultId = -1;
                if (!Int32.TryParse(pResultTable.Rows[pResultTable.Rows.Count - 1][idColumnIndex].ToString(), out lastResultId))
                    return false;
                newResultId = lastResultId + 1;
            }

            DataRow resultRow = pResultTable.NewRow();
            resultRow[idColumnIndex] = newResultId;
            resultRow[scenarioNameColumnIndex] = scenarioName;
            resultRow[processObservedColumnIndex] = processObservedName;
            resultRow[attributeColumnIndex] = countedAttributeName;
            resultRow[comparisonTypeColumnIndex] = comparisonType;
            resultRow[referenceValueColumnIndex] = referenceValue;
            resultRow[totalItemsColumnIndex] = totalItems;

            try
            {
                pResultTable.Rows.Add(resultRow);
                pResultTable.AcceptChanges();                
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile("Error while creating the Counting result table " + pResultTable.TableName + " : "
                    + e.Message);
                return false;
            }
            return true;
        }

        private bool createCountedItemsTable(ArrayList neededRowIndexesList)
        {
            if (_resultTable == null)
                return false;

            int idColumnIndex = _resultTable.Columns.IndexOf(GlobalNames.COUNT_RESULT_ID_COLUMN_NAME);

            int newResultId = -1;
            if (idColumnIndex != -1 && _resultTable.Rows.Count > 0)
            {  
                if (!Int32.TryParse(_resultTable.Rows[_resultTable.Rows.Count - 1][idColumnIndex].ToString(), out newResultId))
                    return false;                
            }
            if (newResultId == -1)
                return false;

            _resultedISTTable = istTable.Clone();
            _resultedISTTable.TableName = istTable.TableName + GlobalNames.COUNTED_ITEMS_TABLE_SUFIX + " " + newResultId;
            
            try
            {
                for (int i = 0; i < neededRowIndexesList.Count; i++)
                {
                    int neededRowIndex = (int)neededRowIndexesList[i];
                    if (neededRowIndex < istTable.Rows.Count)
                        _resultedISTTable.ImportRow(istTable.Rows[neededRowIndex]);
                }
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile("Error while creating the Counting result table: "
                    + e.Message);
                return false;
            }
            _resultedISTTable.AcceptChanges();
            return true;
        }

        private void attributeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDateTimePickersUsingISTTable();
        }

        private void setDateTimePickersUsingISTTable()
        {
            if (attributeTypeComboBox != null && attributeTypeComboBox.SelectedItem != null)
            {
                String selectedAttributeType = attributeTypeComboBox.SelectedItem.ToString();
                if (selectedAttributeType.Equals(CountingTools.COUNTING_ATTRIBUTE_TYPE_DATETIME))
                {
                    if (attributeComboBox.SelectedItem != null && istTable != null)
                    {
                        String selectedAttribute = attributeComboBox.SelectedItem.ToString();
                        int istColumnIndex = istTable.Columns.IndexOf(selectedAttribute);
                        if (istColumnIndex != -1)
                        {
                            DateTime maxDate = OverallTools.DataFunctions.getMaxDate(istTable, istColumnIndex);
                            DateTime minDate = OverallTools.DataFunctions.getMinDate(istTable, istColumnIndex);

                            minDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, minDate.Hour, minDate.Minute, 0);

                            if (intervalRadioButton.Checked)
                            {
                                firstIintervalValueDateTimePicker.Value = minDate;
                                lastIntervalValueDateTimePicker.Value = maxDate;
                            }
                            else
                            {
                                referenceValueDateTimePicker.Value = minDate;
                            }
                        }
                    }
                }
            }
        }

    }
}
