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
    public partial class SetTargetEditor : Form
    {
        private DataTable targetTable;

        private GestionDonneesHUB2SIM donnees;

        private string[] comparisonTypesList = new string[] { "<", "<=", "=", ">=", ">" };

        private int scenarioNameColumnIndex = -1;
        private int processObservedColumnIndex = -1;
        private int statisticTypeColumnIndex = -1;
        private int attributeDegreeColumnIndex = -1;
        private int statisticAttributeColumnIndex = -1;
        private int comparisonTypeColumnIndex = -1;
        private int targetValueColumnIndex = -1;
        private int valueObservedColumnIndex = -1;
        private int targetAchievedColumnIndex = -1;
        private int differenceColumnIndex = -1;
        private int percSuccessColumnIndex = -1;

        private int selectedRowIndex = -1;

        private bool modified = false;

        public SetTargetEditor(DataTable pTargetTable, GestionDonneesHUB2SIM pDonnees)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
                        
            donnees = pDonnees;
            targetTable = pTargetTable;
            setTargetTableColumnIndexes();
            setComparisonTypeComboBox();
            modified = false;
            setTargetRowComboBox();
        }

        private void setTargetTableColumnIndexes()
        {
            if (targetTable != null)
            {
                scenarioNameColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_scenarioName_columnName);
                processObservedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_processObserved_columnName);
                statisticTypeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_statisticType_columnName);
                attributeDegreeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_attributeDegree_columnName);
                statisticAttributeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_statisticAttribute_columnName);
                comparisonTypeColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_comparisonType_columnName);
                targetValueColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_targetValue_columnName);
                valueObservedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_valueObserved_columnName);
                targetAchievedColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_targetAchived_columnName);
                differenceColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_difference_columnName);
                percSuccessColumnIndex = targetTable.Columns.IndexOf(GlobalNames.target_percentSuccess_columnName);
            }
        }

        private bool areTargetTableIndexesConsistent()
        {
            if (scenarioNameColumnIndex != -1 && processObservedColumnIndex != -1 && statisticTypeColumnIndex != -1
                && attributeDegreeColumnIndex != -1 && statisticAttributeColumnIndex != -1 && comparisonTypeColumnIndex != -1
                && targetValueColumnIndex != -1 && valueObservedColumnIndex != -1 && targetAchievedColumnIndex != -1
                && differenceColumnIndex != -1 && percSuccessColumnIndex != -1)
            {
                return true;
            }
            return false;
        }

        private void setTargetRowComboBox()
        {
            if (targetTable != null && targetTable.Rows != null)
            {
                targetRowComboBox.Items.Clear();

                for (int i = 1; i <= targetTable.Rows.Count; i++)
                    targetRowComboBox.Items.Add(i);

                if (targetRowComboBox.Items.Count > 0)
                    targetRowComboBox.SelectedIndex = 0;
            }
        }

        private void setComparisonTypeComboBox()
        {
            comparisonTypeComboBox.Items.Clear();
            comparisonTypeComboBox.Items.AddRange(comparisonTypesList);
        }

        #region event handlers
        private void targetRowComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modified)
            {
                DialogResult res = MessageBox.Show("Would you like to save the changes made?", 
                                        "Target Details", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    double difference = 0;
                    double percSuccess = 0;

                    if (Double.TryParse(differenceTextBox.Text, out difference)
                        && Double.TryParse(percSuccessTextBox.Text, out percSuccess))
                    {
                        updateTargetTableResults(targetAchievedTextBox.Text, difference, percSuccess);
                    }
                }
            }
            
            int rowNb = -1;
            if (Int32.TryParse(targetRowComboBox.SelectedItem.ToString(), out rowNb))
            {
                int rowIndex = rowNb - 1;
                DataRow row = targetTable.Rows[rowIndex];
                scenarioNameTextBox.Text = row[scenarioNameColumnIndex].ToString();
                processObservedTextBox.Text = row[processObservedColumnIndex].ToString();
                statisticTypeTextBox.Text = row[statisticTypeColumnIndex].ToString();
                attributeDegreeTextBox.Text = row[attributeDegreeColumnIndex].ToString();
                statisticAttributeTextBox.Text = row[statisticAttributeColumnIndex].ToString();
                if (comparisonTypeComboBox.Items.Contains(row[comparisonTypeColumnIndex].ToString()))
                    comparisonTypeComboBox.SelectedItem = row[comparisonTypeColumnIndex].ToString();
                targetValueTextBox.Text = row[targetValueColumnIndex].ToString();
                valueObservedTextBox.Text = row[valueObservedColumnIndex].ToString();
                targetAchievedTextBox.Text = row[targetAchievedColumnIndex].ToString();
                differenceTextBox.Text = row[differenceColumnIndex].ToString();
                percSuccessTextBox.Text = row[percSuccessColumnIndex].ToString();

                selectedRowIndex = rowIndex;
            }
            modified = false;
        }

        private static char[] allowedCharsList = { '\b', '.' };
        private void targetValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)
                || Array.IndexOf(allowedCharsList, e.KeyChar) > -1)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void targetValueTextBox_Leave(object sender, EventArgs e)
        {
            modified = true;

            String targetAchieved = "";
            double difference = 0;
            double percSuccess = 0;
            recalculateTargetTableResults(out targetAchieved, out difference, out percSuccess);
            updateTargetResultsFields(targetAchieved, difference, percSuccess);
        }

        private void comparisonTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modified = true;

            String targetAchieved = "";
            double difference = 0;
            double percSuccess = 0;
            recalculateTargetTableResults(out targetAchieved, out difference, out percSuccess);
            updateTargetResultsFields(targetAchieved, difference, percSuccess);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (modified)
            {
                DialogResult res = MessageBox.Show("Would you like to save the changes made?",
                                        "Target Details", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    double difference = 0;
                    double percSuccess = 0;

                    if (Double.TryParse(differenceTextBox.Text, out difference)
                        && Double.TryParse(percSuccessTextBox.Text, out percSuccess))
                    {
                        updateTargetTableResults(targetAchievedTextBox.Text, difference, percSuccess);
                    }
                }
            }
        }

        #endregion

        private void recalculateTargetTableResults(out String calcTargetAchieved,
            out double calcDifference, out double calcPercSuccess)
        {
            calcTargetAchieved = "";
            calcDifference = 0;
            calcPercSuccess = 0;

            String processObservedName = processObservedTextBox.Text;
            String statisticType = statisticTypeTextBox.Text;
            String statisticAttribute = statisticAttributeTextBox.Text;
            String attributeDegree = attributeDegreeTextBox.Text;
            String comparisonType = comparisonTypeComboBox.SelectedItem.ToString();
            double valueObserved = Double.MinValue;

            if (Double.TryParse(valueObservedTextBox.Text, out valueObserved))
            {
                double targetValue = 0;
                if (Double.TryParse(targetValueTextBox.Text, out targetValue))
                {
                    if (comparisonType.Contains(">"))
                    {
                        calcPercSuccess = (valueObserved / targetValue) * 100;
                        if (!comparisonType.Contains("=") && valueObserved == targetValue)
                            calcPercSuccess--;
                    }
                    else if (comparisonType.Contains("<"))
                    {
                        calcPercSuccess = (targetValue / valueObserved) * 100;
                        if (!comparisonType.Contains("=") && valueObserved == targetValue)
                            calcPercSuccess--;
                    }
                    else if (comparisonType.Equals("="))
                    {
                        calcPercSuccess = (Math.Min(targetValue, valueObserved) / Math.Max(targetValue, valueObserved)) * 100;
                        if (targetValue == valueObserved)
                            calcPercSuccess = 100;
                    }
                    calcPercSuccess = Math.Round(calcPercSuccess, 2);

                    calcDifference = Math.Round(Math.Abs(targetValue - valueObserved), 2);

                    if (isTargetAchieved(targetValue, valueObserved, comparisonType))
                    {
                        calcTargetAchieved = GlobalNames.TARGET_ACHIEVED_POSITIVE;
                    }
                    else
                    {
                        calcTargetAchieved = GlobalNames.TARGET_ACHIEVED_NEGATIVE;
                        calcDifference = calcDifference * (-1);
                    }
                }
            }
        }

        private bool isTargetAchieved(double targetValue, double valueObserved, String comparisonType)
        {
            bool targetAchieved = false;

            switch (comparisonType)
            {
                case "<":
                    if (valueObserved < targetValue)
                        targetAchieved = true;
                    break;
                case "<=":
                    if (valueObserved <= targetValue)
                        targetAchieved = true;
                    break;
                case "=":
                    if (valueObserved == targetValue)
                        targetAchieved = true;
                    break;
                case ">=":
                    if (valueObserved >= targetValue)
                        targetAchieved = true;
                    break;
                case ">":
                    if (valueObserved > targetValue)
                        targetAchieved = true;
                    break;
            }
            return targetAchieved;

        }

        private void updateTargetResultsFields(String targetArchieved, double difference, double percSuccess)
        {
            targetAchievedTextBox.Text = targetArchieved;
            differenceTextBox.Text = difference.ToString();
            percSuccessTextBox.Text = percSuccess.ToString();
        }

        private void updateTargetTableResults(String targetAchieved, double difference, double percSuccess)
        {
            DataRow row = targetTable.Rows[selectedRowIndex];

            row[targetValueColumnIndex] = targetValueTextBox.Text;
            row[comparisonTypeColumnIndex] = comparisonTypeComboBox.SelectedItem.ToString();

            row[targetAchievedColumnIndex] = targetAchieved;
            row[differenceColumnIndex] = difference;
            row[percSuccessColumnIndex] = percSuccess;

            targetTable.AcceptChanges();
        }

        private void removeTargetRowBtn_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = targetRowComboBox.SelectedIndex;

            if (selectedRowIndex != -1)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to remove this row ?",
                                                "Remove target", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    targetTable.Rows.RemoveAt(selectedRowIndex);
                    targetTable.AcceptChanges();

                    setTargetRowComboBox();
                }

                if (targetRowComboBox.Items.Count == 0)
                {
                    MessageBox.Show("The target table doesn't have any data. The editor will close.",
                        "Target Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    this.Close();
                }
            }
        }
        
    }
}