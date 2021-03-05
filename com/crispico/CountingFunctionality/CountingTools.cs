using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.com.crispico.CountingFunctionality
{
    // >> Task #10359 Pax2Sim - Counting functionality
    class CountingTools
    {
        public const String COUNTING_ATTRIBUTE_TYPE_DATETIME = "Date attributes";
        public const String COUNTING_ATTRIBUTE_TYPE_NUMERICAL = "Numerical attributes";
        public const String COUNTING_ATTRIBUTE_TYPE_BOOLEAN = "True/False attributes";
        public const String COUNTING_ATTRIBUTE_TYPE_TEXT = "Text attributes";   

        /// <summary>
        /// The number of items is given by the length of the list(out parameter).
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="columnIndex"></param>
        /// <param name="providedValue"></param>
        /// <param name="rowIndexesList">The list with the row indexes that satisfy the requirement.</param>
        /// <returns></returns>
        public static bool countByStringValue(DataTable sourceTable, int columnIndex, String providedValue,
                                              String comparisonType, out ArrayList rowIndexesList)
        {
            rowIndexesList = new ArrayList();
            rowIndexesList.Clear();

            if (columnIndex == -1)
                return false;

            if (sourceTable != null && sourceTable.Columns[columnIndex] != null)
            {
                DataColumn investigatedColumn = sourceTable.Columns[columnIndex];
                if (investigatedColumn.DataType != typeof(String))
                    return false;
                
                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    DataRow row = sourceTable.Rows[i];
                    if (row != null)
                    {
                        String investigatedValue = row[investigatedColumn].ToString();
                        if (investigatedValue != null) 
                        {
                            if (comparisonType.Equals(GlobalNames.COMPARISON_EQUAL))
                            {
                                if (investigatedValue.Equals(providedValue))
                                    rowIndexesList.Add(i);
                            }
                            else if (comparisonType.Equals(GlobalNames.COMPARISON_CONTAINS))
                            {
                                if (investigatedValue.Contains(providedValue))
                                    rowIndexesList.Add(i);
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// The number of items is given by the length of the list(out parameter).
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="columnIndex"></param>
        /// <param name="providedValue"></param>
        /// <param name="comparisonType"></param>
        /// <param name="rowIndexesList"></param>
        /// <returns> Returns false if the table is null or if it doesn't have the column received
        /// or if all the values from the specified column can't be parsed as doubles.</returns>
        public static bool countByNumericalValue(DataTable sourceTable, int columnIndex, double providedValue,
            String comparisonType, out ArrayList rowIndexesList)
        {
            rowIndexesList = new ArrayList();
            rowIndexesList.Clear();

            if (columnIndex == -1)
                return false;

            if (sourceTable != null && sourceTable.Columns[columnIndex] != null)
            {
                bool allParsingFailed = true;
                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    double investigatedValue = 0;
                    DataRow row = sourceTable.Rows[i];

                    if (row != null
                        && Double.TryParse(row[columnIndex].ToString(), out investigatedValue))
                    {
                        allParsingFailed = false;

                        if (isComparisonConditionTrue(investigatedValue, providedValue, comparisonType))
                            rowIndexesList.Add(i);                        
                    }
                }
                if (allParsingFailed)
                    return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// The number of items is given by the length of the list(out parameter).
        /// Returns false if the table is null or if it doesn't have the column received
        /// or if all the values from the specified column can't be parsed as doubles.        
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="columnIndex"></param>
        /// <param name="firstIntervalValueComparisonType"></param>
        /// <param name="firstIntervalValue">Must be less than or equal with lastIntervalValue</param>
        /// <param name="lastIntervalValueComparisonType"></param>
        /// <param name="lastIntervalValue">Must be greater than or equal with first IntervalValue</param>
        /// <param name="useInterval"></param>
        /// <param name="rowIndexesList">The list with the row indexes that satisfy the requirement.</param>
        /// <returns></returns>
        public static bool countByNumericalInterval(DataTable sourceTable, int columnIndex, 
            bool includeFirstIntervalValue, double firstIntervalValue,
            bool includeLastIntervalValue, double lastIntervalValue,
            out ArrayList rowIndexesList)
        {
            rowIndexesList = new ArrayList();
            rowIndexesList.Clear();

            if (columnIndex == -1)
                return false;
            
            if (firstIntervalValue > lastIntervalValue)                
                return false;

            if (firstIntervalValue == lastIntervalValue
                && (!includeFirstIntervalValue || !includeLastIntervalValue))
            {
                return false;
            }

            String firstIntervalValueComparisonType = "";
            String lastIntervalValueComparisonType = "";

            if (includeFirstIntervalValue)
                firstIntervalValueComparisonType = GlobalNames.COMPARISON_GREATER_OR_EQUAL_THAN;
            else
                firstIntervalValueComparisonType = GlobalNames.COMPARISON_GREATER_THAN;

            if (includeLastIntervalValue)
                lastIntervalValueComparisonType = GlobalNames.COMPARISON_LESS_OR_EQUAL_THAN;
            else
                lastIntervalValueComparisonType = GlobalNames.COMPARISON_LESS_THAN;

            if (sourceTable != null && sourceTable.Columns[columnIndex] != null)
            {   
                bool allParsingFailed = true;
                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    double investigatedValue = 0;
                    DataRow row = sourceTable.Rows[i];

                    if (row != null
                        && Double.TryParse(row[columnIndex].ToString(), out investigatedValue))
                    {
                        allParsingFailed = false;
                        
                        if (isComparisonConditionTrue(investigatedValue, firstIntervalValue, firstIntervalValueComparisonType)
                            && isComparisonConditionTrue(investigatedValue, lastIntervalValue, lastIntervalValueComparisonType))
                        {
                            rowIndexesList.Add(i);
                        }
                    }
                }
                if (allParsingFailed)
                    return false;
                return true;
            }
            return false;
        }

        public static bool isComparisonConditionTrue(double leftSideValue, double rightSideValue, String comparisonType)
        {
            bool result = false;

            switch (comparisonType)
            {
                case GlobalNames.COMPARISON_LESS_THAN:
                    if (leftSideValue < rightSideValue)
                        result = true;
                    break;
                case GlobalNames.COMPARISON_LESS_OR_EQUAL_THAN:
                    if (leftSideValue <= rightSideValue)
                        result = true;
                    break;
                case GlobalNames.COMPARISON_EQUAL:
                    if (leftSideValue == rightSideValue)
                        result = true;
                    break;
                case GlobalNames.COMPARISON_GREATER_OR_EQUAL_THAN:
                    if (leftSideValue >= rightSideValue)
                        result = true;
                    break;
                case GlobalNames.COMPARISON_GREATER_THAN:
                    if (leftSideValue > rightSideValue)
                        result = true;
                    break;
            }
            return result;
        }

        public static bool countByBooleanValue(DataTable sourceTable, int columnIndex, bool providedValue,
                                                out ArrayList rowIndexesList)
        {
            rowIndexesList = new ArrayList();
            rowIndexesList.Clear();

            if (columnIndex == -1)
                return false;

            if (sourceTable != null && sourceTable.Columns[columnIndex] != null)
            {
                bool allParsingFailed = true;

                DataColumn investigatedColumn = sourceTable.Columns[columnIndex];
                if (investigatedColumn.DataType != typeof(Boolean))
                    return false;

                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    DataRow row = sourceTable.Rows[i];                    
                    bool investigatedValue = false;
                    if (row != null
                        && Boolean.TryParse(row[investigatedColumn].ToString(), out investigatedValue))
                    {
                        allParsingFailed = false;

                        if (investigatedValue == providedValue)
                            rowIndexesList.Add(i);
                    }                    
                }
                if (allParsingFailed)
                    return false;
                return true;
            }
            return false;
        }

        public static bool countByDateValue(DataTable sourceTable, int columnIndex, DateTime providedValue,
                                            String comparisonType, out ArrayList rowIndexesList)
        {
            rowIndexesList = new ArrayList();
            rowIndexesList.Clear();

            if (columnIndex == -1)
                return false;

            if (sourceTable != null && sourceTable.Columns[columnIndex] != null)
            {
                bool allParsingFailed = true;

                DataColumn investigatedColumn = sourceTable.Columns[columnIndex];
                if (investigatedColumn.DataType != typeof(DateTime))
                    return false;

                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    DataRow row = sourceTable.Rows[i];
                    DateTime investigatedValue = new DateTime();
                    if (row != null
                        && DateTime.TryParse(row[investigatedColumn].ToString(), out investigatedValue))
                    {
                        allParsingFailed = false;

                        if (isComparisonConditionTrue(investigatedValue.ToOADate(), providedValue.ToOADate(), comparisonType))
                            rowIndexesList.Add(i);  
                    }
                }
                if (allParsingFailed)
                    return false;
                return true;
            }
            return false;
        }

        public static bool countByDateInterval(DataTable sourceTable, int columnIndex,
            bool includeFirstIntervalValue, DateTime firstIntervalValue,
            bool includeLastIntervalValue, DateTime lastIntervalValue,
            out ArrayList rowIndexesList)
        {
            rowIndexesList = new ArrayList();
            rowIndexesList.Clear();

            if (columnIndex == -1)
                return false;

            if (firstIntervalValue > lastIntervalValue)
                return false;

            if (firstIntervalValue == lastIntervalValue
                && (!includeFirstIntervalValue || !includeLastIntervalValue))
            {
                return false;
            }

            String firstIntervalValueComparisonType = "";
            String lastIntervalValueComparisonType = "";

            if (includeFirstIntervalValue)
                firstIntervalValueComparisonType = GlobalNames.COMPARISON_GREATER_OR_EQUAL_THAN;
            else
                firstIntervalValueComparisonType = GlobalNames.COMPARISON_GREATER_THAN;

            if (includeLastIntervalValue)
                lastIntervalValueComparisonType = GlobalNames.COMPARISON_LESS_OR_EQUAL_THAN;
            else
                lastIntervalValueComparisonType = GlobalNames.COMPARISON_LESS_THAN;
            
            if (sourceTable != null && sourceTable.Columns[columnIndex] != null)
            {
                bool allParsingFailed = true;
                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    DateTime investigatedValue = new DateTime();
                    DataRow row = sourceTable.Rows[i];

                    if (row != null
                        && DateTime.TryParse(row[columnIndex].ToString(), out investigatedValue))
                    {
                        allParsingFailed = false;

                        if (isComparisonConditionTrue(investigatedValue.ToOADate(), firstIntervalValue.ToOADate(), firstIntervalValueComparisonType)
                            && isComparisonConditionTrue(investigatedValue.ToOADate(), lastIntervalValue.ToOADate(), lastIntervalValueComparisonType))
                        {
                            rowIndexesList.Add(i);
                        }
                    }
                }
                if (allParsingFailed)
                    return false;
                return true;
            }
            return false;
        }

        public static bool isCountingDirectoryChild(TreeNode node)
        {
            if (node.Parent != null)
            {
                TreeNode parent = node.Parent;
                TreeViewTag tvtParent = (TreeViewTag)(parent.Tag);
                if (tvtParent.isDirectoryNode && parent.Name.Equals(GlobalNames.COUNT_DIRECTORY_NAME))
                    return true;
            }
            //else
            return false;
            //return isCountingDirectoryChild(node.Parent);
        }

        public static bool isCountResultChildTable(TreeNode node)
        {
            if (node == null || node.Parent == null)
                return false;

            TreeNode parent = node.Parent;            
            String parentName = parent.Name;
            String nodeName = node.Name;

            if (parentName == null || nodeName == null)
                return false;

            if (parentName.EndsWith(GlobalNames.COUNT_RESULT_TABLE_SUFIX)
                && nodeName.Contains(GlobalNames.IST_TABLE_SUFFIX + GlobalNames.COUNTED_ITEMS_TABLE_SUFIX))
            {
                return true;
            }
            return false;
        }

    }
}
