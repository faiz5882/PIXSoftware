using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Prompt.Allocation.General;

namespace SIMCORE_TOOL.com.crispico.inputs.flight_group_rules
{
    public partial class FlightGroupRulesEditor : Form
    {
        public const string FLIGHT_GROUP_RULES_TABLE_TECHNICAL_NAME = "Flight_Group_Rules";
        public const string FLIGHT_GROUP_RULES_TABLE_DISPLAYED_NAME = "Flight Group Rules";

        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_RULE_NAME = "Rule Name";
        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_FLIGHT_CATEGORIES = "Flight Categories";
        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_AIRLINES = "Airlines";
        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_AIRPORTS = "Airports";
        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_1 = "User 1";
        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_2 = "User 2";
        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_3 = "User 3";
        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_4 = "User 4";
        public const string FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_5 = "User 5";

        private const string ITEM_TYPE_FLIGHT_CATEGORIES = "Flight Categories";
        private const string ITEM_TYPE_AIRLINES = "Airlines";
        private const string ITEM_TYPE_AIRPORTS = "Airports";
        private const string ITEM_TYPE_USER1 = "User1";
        private const string ITEM_TYPE_USER2 = "User2";
        private const string ITEM_TYPE_USER3 = "User3";
        private const string ITEM_TYPE_USER4 = "User4";
        private const string ITEM_TYPE_USER5 = "User5";

        private readonly List<string> itemTypes = new List<string>(new string[] { ITEM_TYPE_FLIGHT_CATEGORIES, ITEM_TYPE_AIRLINES, ITEM_TYPE_AIRPORTS, 
            ITEM_TYPE_USER1, ITEM_TYPE_USER2, ITEM_TYPE_USER3, ITEM_TYPE_USER4, ITEM_TYPE_USER5});

        public static List<FlightGroupRulesParameter> getParametersFromGivenTable(DataTable flightGroupRulesTable)
        {
            List<FlightGroupRulesParameter> result = new List<FlightGroupRulesParameter>();
            if (flightGroupRulesTable == null)
                return result;
            foreach (DataRow row in flightGroupRulesTable.Rows)
                result.Add(getParameterFromTableRow(row));
            return result;
        }

        private DataTable flightGroupRulesTable;
        private int editedRowIndex = -1;

        private Dictionary<string, ListBox> selectedItemsListBoxesByItemType = new Dictionary<string, ListBox>();
        
        /// <summary>
        /// K = item type name (Flight categories, Airlines, ...)
        /// V = list of selected items
        /// </summary>
        private Dictionary<string, List<string>> selectedItemsByItemType = new Dictionary<string, List<string>>();
        
        /// <summary>
        /// K = item type name (Flight categories, Airlines, ...)
        /// V = list of available items
        /// </summary>
        private Dictionary<string, List<string>> availableItemsByItemType = new Dictionary<string, List<string>>();

        private List<string> ruleNamesInUse = new List<string>();

        public FlightGroupRulesEditor(DataTable selectedFlightGroupRulesTable, List<DataTable> fpTables)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            this.flightGroupRulesTable = selectedFlightGroupRulesTable;
            addRuleNamesInUse(flightGroupRulesTable);

            addFlightCategoriesGroupBox(rootGroupBox);
            setAvailableItemsByItemType(fpTables);
            initializeSelectedItemsByItemTypeWithEmptyLists();
            
        }

        public FlightGroupRulesEditor(DataTable flightGroupRulesTable, List<DataTable> fpTables, int editedRowIndex)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            this.flightGroupRulesTable = flightGroupRulesTable;
            addRuleNamesInUse(flightGroupRulesTable);

            this.editedRowIndex = editedRowIndex;

            addFlightCategoriesGroupBox(rootGroupBox);
            setAvailableItemsByItemType(fpTables);
            initializeSelectedItemsByItemTypeWithEmptyLists();

            updateInterfaceByEditedRow(flightGroupRulesTable, editedRowIndex);
        }

        private void addRuleNamesInUse(DataTable flightGroupRulesTable)
        {
            if (flightGroupRulesTable == null || flightGroupRulesTable.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_RULE_NAME) == -1)
                return;
            int columnIndexRuleName = flightGroupRulesTable.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_RULE_NAME);
            foreach (DataRow row in flightGroupRulesTable.Rows)
            {
                if (row[columnIndexRuleName] == null)
                    continue;
                string ruleName = row[columnIndexRuleName].ToString();
                if (!ruleNamesInUse.Contains(ruleName))
                    ruleNamesInUse.Add(ruleName);
            }
        }

        private void addFlightCategoriesGroupBox(Control parent)
        {
            // First row
            int locationX = 5;
            int locationY = 8;
            GroupBox flightCategoriesGb = addGroupBox(ITEM_TYPE_FLIGHT_CATEGORIES, locationX, locationY, rootGroupBox);

            locationX = flightCategoriesGb.Location.X + flightCategoriesGb.Width + 10;
            locationY = flightCategoriesGb.Location.Y;
            GroupBox airlinesGb = addGroupBox(ITEM_TYPE_AIRLINES, locationX, locationY, rootGroupBox);

            locationX = airlinesGb.Location.X + airlinesGb.Width + 10;
            locationY = airlinesGb.Location.Y;
            GroupBox airportsGb = addGroupBox(ITEM_TYPE_AIRPORTS, locationX, locationY, rootGroupBox);

            locationX = airportsGb.Location.X + airportsGb.Width + 10;
            locationY = airportsGb.Location.Y;
            GroupBox user1Gb = addGroupBox(ITEM_TYPE_USER1, locationX, locationY, rootGroupBox);

            // Second row
            locationX = flightCategoriesGb.Location.X;
            locationY = flightCategoriesGb.Location.Y + flightCategoriesGb.Height + 10;
            GroupBox user2Gb = addGroupBox(ITEM_TYPE_USER2, locationX, locationY, rootGroupBox);

            locationX = airlinesGb.Location.X;
            locationY = airlinesGb.Location.Y + airlinesGb.Height + 10;
            GroupBox user3Gb = addGroupBox(ITEM_TYPE_USER3, locationX, locationY, rootGroupBox);

            locationX = airportsGb.Location.X;
            locationY = airportsGb.Location.Y + airportsGb.Height + 10;
            GroupBox user4Gb = addGroupBox(ITEM_TYPE_USER4, locationX, locationY, rootGroupBox);

            locationX = user1Gb.Location.X;
            locationY = user1Gb.Location.Y + user1Gb.Height + 10;
            GroupBox user5Gb = addGroupBox(ITEM_TYPE_USER5, locationX, locationY, rootGroupBox);
        }

        private GroupBox addGroupBox(string itemType, int locationX, int locationY, Control parent)
        {
            GroupBox itemTypeGb = new GroupBox();
            
            parent.Controls.Add(itemTypeGb);
            itemTypeGb.Location = new Point(locationX, locationY);

            itemTypeGb.Text = itemType;
            itemTypeGb.Tag = itemType;
            itemTypeGb.Width = 145;
            itemTypeGb.Height = 170;

            Label selectedLbl = new Label();
            selectedLbl.Text = "Selected ";
            itemTypeGb.Controls.Add(selectedLbl);
            selectedLbl.Location = new Point(35, 17);

            ListBox selectedListBox = new ListBox();
            itemTypeGb.Controls.Add(selectedListBox);
            selectedListBox.Location = new Point(15, 40);
            selectedListBox.Size = new Size(120, 95);
            selectedListBox.SelectionMode = SelectionMode.None;
            selectedItemsListBoxesByItemType.Add(itemType, selectedListBox);

            Button modifyBtn = new Button();
            itemTypeGb.Controls.Add(modifyBtn);
            modifyBtn.Text = "Modify";
            modifyBtn.Size = new Size(75, 25);
            modifyBtn.Location = new Point(35, 140);
            modifyBtn.Click += new EventHandler(this.modifyBtn_Click);

            return itemTypeGb;
        }

        private void initializeSelectedItemsByItemTypeWithEmptyLists()
        {
            selectedItemsByItemType = new Dictionary<string, List<string>>();
            foreach (string itemType in itemTypes)
                selectedItemsByItemType.Add(itemType, new List<string>());
        }

        private void updateInterfaceByEditedRow(DataTable flightGroupRulesTable, int editedRowIndex)
        {
            if (flightGroupRulesTable == null || editedRowIndex < 0 || editedRowIndex >= flightGroupRulesTable.Rows.Count)
                return;
            DataRow editedRow = flightGroupRulesTable.Rows[editedRowIndex];
            FlightGroupRulesParameter editedParam = getParameterFromTableRow(editedRow);
            ruleNameTextBox.Text = editedParam.ruleName;
            foreach (string itemType in itemTypes)
                setListBoxByEditedParamItemType(itemType, editedParam, availableItemsByItemType);
        }

        private void setListBoxByEditedParamItemType(string itemType, FlightGroupRulesParameter editedParam, Dictionary<string, List<string>> availableItemsByItemType)
        {
            if (editedParam == null)
                return;
            if (!selectedItemsListBoxesByItemType.ContainsKey(itemType) || !availableItemsByItemType.ContainsKey(itemType) || !selectedItemsByItemType.ContainsKey(itemType))
                return;
            
            ListBox listBox = selectedItemsListBoxesByItemType[itemType];
            List<string> availableItems = availableItemsByItemType[itemType];
            List<string> selectedItems = selectedItemsByItemType[itemType];

            List<string> itemsFromParam = new List<string>();
            switch (itemType)
            {
                case ITEM_TYPE_FLIGHT_CATEGORIES:
                    {
                        itemsFromParam = editedParam.flightCategories;
                        break;
                    }
                case ITEM_TYPE_AIRLINES:
                    {
                        itemsFromParam = editedParam.airlines;
                        break;
                    }
                case ITEM_TYPE_AIRPORTS:
                    {
                        itemsFromParam = editedParam.airports;
                        break;
                    }
                case ITEM_TYPE_USER1:
                    {
                        itemsFromParam = editedParam.user1Contents;
                        break;
                    }
                case ITEM_TYPE_USER2:
                    {
                        itemsFromParam = editedParam.user2Contents;
                        break;
                    }
                case ITEM_TYPE_USER3:
                    {
                        itemsFromParam = editedParam.user3Contents;
                        break;
                    }
                case ITEM_TYPE_USER4:
                    {
                        itemsFromParam = editedParam.user4Contents;
                        break;
                    }
                case ITEM_TYPE_USER5:
                    {
                        itemsFromParam = editedParam.user5Contents;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            List<string> itemsToAdd = new List<string>();
            foreach (string paramItem in itemsFromParam)
            {
                if (availableItems.Contains(paramItem))
                    itemsToAdd.Add(paramItem);
            }
            foreach (string itemToAdd in itemsToAdd)
            {
                listBox.Items.Add(itemToAdd);
                selectedItems.Add(itemToAdd);
            }
        }

        


        private void ruleNameTextBox_Leave(object sender, EventArgs e)
        {
            if (editedRowIndex != -1)
                return;
            if (ruleNamesInUse.Contains(ruleNameTextBox.Text))
            {
                MessageBox.Show("The name \"" + ruleNameTextBox.Text + "\" is currently used by another rule."
                    + Environment.NewLine + "Please select another name.", "Rule name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ruleNameTextBox.Focus();
            }
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            if (sender == null || sender.GetType() != typeof(Button))
                return;
            Button modifyBtn = (Button)sender;
            if (modifyBtn.Parent == null || modifyBtn.Parent.GetType() != typeof(GroupBox))
                return;
            GroupBox parentGroupBox = (GroupBox)modifyBtn.Parent;
            if (parentGroupBox.Tag == null)
                return;
            string itemType = parentGroupBox.Tag.ToString();
            if (!availableItemsByItemType.ContainsKey(itemType) || !selectedItemsByItemType.ContainsKey(itemType) || !selectedItemsListBoxesByItemType.ContainsKey(itemType))
                return;
            
            List<string> availableItems = availableItemsByItemType[itemType];
            List<string> selectedItems = selectedItemsByItemType[itemType];

            MultiSelectionPopUp multiSelector = new MultiSelectionPopUp(availableItems, selectedItems);
            if (multiSelector.ShowDialog() == DialogResult.OK)
            {
                selectedItems.Clear();
                selectedItems.AddRange(multiSelector.selectedItems);
                
                ListBox itemTypeListBox = selectedItemsListBoxesByItemType[itemType];
                itemTypeListBox.Items.Clear();
                foreach (string selectedItem in multiSelector.selectedItems)
                    itemTypeListBox.Items.Add(selectedItem);
            }
        }        

        private void setAvailableItemsByItemType(List<DataTable> flightPlans)
        {
            availableItemsByItemType.Clear();
            foreach (string itemType in itemTypes)
                availableItemsByItemType.Add(itemType, getItemsFromFlightPlans(itemType, flightPlans));
        }

        private List<string> getItemsFromFlightPlans(string itemType, List<DataTable> flightPlans)
        {
            List<string> items = new List<string>();
            foreach (DataTable flightPlan in flightPlans)
            {
                int fpColumnIndex = getFlightPlanColumnIndexByItemType(itemType, flightPlan);
                if (fpColumnIndex == -1)
                    return items;
                foreach (DataRow row in flightPlan.Rows)
                    if (row[fpColumnIndex] != null && !items.Contains(row[fpColumnIndex].ToString()))
                        items.Add(row[fpColumnIndex].ToString());
            }
            return items;
        }

        private int getFlightPlanColumnIndexByItemType(string itemType, DataTable flightPlan)
        {
            int index = -1;
            if (flightPlan == null)
                return index;
            switch (itemType)
            {
                case ITEM_TYPE_FLIGHT_CATEGORIES:
                    index = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                    break;
                case ITEM_TYPE_AIRLINES:
                    index = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                    break;
                case ITEM_TYPE_AIRPORTS:
                    index = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                    break;
                case ITEM_TYPE_USER1:
                    index = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                    break;
                case ITEM_TYPE_USER2:
                    index = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                    break;
                case ITEM_TYPE_USER3:
                    index = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                    break;
                case ITEM_TYPE_USER4:
                    index = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                    break;
                case ITEM_TYPE_USER5:
                    index = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
                    break;
            }
            return index;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            FlightGroupRulesParameter param = getParameterFromForm();
            if (editedRowIndex != -1)
                updateFlightGroupRulesTable(param, editedRowIndex, flightGroupRulesTable);
            else
                addToFlightGroupRulesTable(param, flightGroupRulesTable);
        }

        private void updateFlightGroupRulesTable(FlightGroupRulesParameter param, int editedRowIndex, DataTable flightGroupRulesTable)
        {
            if (param == null || flightGroupRulesTable == null)
                return;
            if (editedRowIndex < 0 || editedRowIndex >= flightGroupRulesTable.Rows.Count)
                return;
            DataRow row = flightGroupRulesTable.Rows[editedRowIndex];
            updateFlightGroupRulesTableRow(param, row);
        }

        private void addToFlightGroupRulesTable(FlightGroupRulesParameter param, DataTable flightGroupRulesTable)
        {
            if (param == null || flightGroupRulesTable == null)
                return;
            DataRow newRow = flightGroupRulesTable.NewRow();
            updateFlightGroupRulesTableRow(param, newRow);
            flightGroupRulesTable.Rows.Add(newRow);
        }

        private void updateFlightGroupRulesTableRow(FlightGroupRulesParameter param, DataRow row)
        {
            if (row == null || param == null)
                return;

            int columnIndexRuleName = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_RULE_NAME);
            int columnIndexFlightCategories = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_FLIGHT_CATEGORIES);
            int columnIndexAirlines = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_AIRLINES);
            int columnIndexAirports = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_AIRPORTS);
            int columnIndexUser1 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_1);
            int columnIndexUser2 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_2);
            int columnIndexUser3 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_3);
            int columnIndexUser4 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_4);
            int columnIndexUser5 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_5);

            if (columnIndexRuleName == -1 || columnIndexFlightCategories == -1 || columnIndexAirlines == -1 || columnIndexAirports == -1
                || columnIndexUser1 == -1 || columnIndexUser2 == -1 || columnIndexUser3 == -1 || columnIndexUser4 == -1 || columnIndexUser5 == -1)
            {
                return;
            }
            row[columnIndexRuleName] = param.ruleName;
            row[columnIndexFlightCategories] = getItemsStringFromParameterItems(param.flightCategories);
            row[columnIndexAirlines] = getItemsStringFromParameterItems(param.airlines);
            row[columnIndexAirports] = getItemsStringFromParameterItems(param.airports);

            row[columnIndexUser1] = getItemsStringFromParameterItems(param.user1Contents);
            row[columnIndexUser2] = getItemsStringFromParameterItems(param.user2Contents);
            row[columnIndexUser3] = getItemsStringFromParameterItems(param.user3Contents);
            row[columnIndexUser4] = getItemsStringFromParameterItems(param.user4Contents);
            row[columnIndexUser5] = getItemsStringFromParameterItems(param.user5Contents);
        }
        public const string ITEMS_SEPARATOR = ",";
        private string getItemsStringFromParameterItems(List<string> paramItems)
        {
            string items = string.Empty;
            for (int i = 0; i < paramItems.Count; i++)
            {
                items += paramItems[i];
                if (i < paramItems.Count - 1)
                    items += ITEMS_SEPARATOR + " ";
            }
            return items;
        }

        private FlightGroupRulesParameter getParameterFromForm()
        {
            FlightGroupRulesParameter param = new FlightGroupRulesParameter();
            param.ruleName = ruleNameTextBox.Text;
            if (selectedItemsByItemType.ContainsKey(ITEM_TYPE_FLIGHT_CATEGORIES))
                param.flightCategories.AddRange(selectedItemsByItemType[ITEM_TYPE_FLIGHT_CATEGORIES]);
            if (selectedItemsByItemType.ContainsKey(ITEM_TYPE_AIRLINES))
                param.airlines.AddRange(selectedItemsByItemType[ITEM_TYPE_AIRLINES]);
            if (selectedItemsByItemType.ContainsKey(ITEM_TYPE_AIRPORTS))
                param.airports.AddRange(selectedItemsByItemType[ITEM_TYPE_AIRPORTS]);

            if (selectedItemsByItemType.ContainsKey(ITEM_TYPE_USER1))
                param.user1Contents.AddRange(selectedItemsByItemType[ITEM_TYPE_USER1]);
            if (selectedItemsByItemType.ContainsKey(ITEM_TYPE_USER2))
                param.user2Contents.AddRange(selectedItemsByItemType[ITEM_TYPE_USER2]);
            if (selectedItemsByItemType.ContainsKey(ITEM_TYPE_USER3))
                param.user3Contents.AddRange(selectedItemsByItemType[ITEM_TYPE_USER3]);
            if (selectedItemsByItemType.ContainsKey(ITEM_TYPE_USER4))
                param.user4Contents.AddRange(selectedItemsByItemType[ITEM_TYPE_USER4]);
            if (selectedItemsByItemType.ContainsKey(ITEM_TYPE_USER5))
                param.user5Contents.AddRange(selectedItemsByItemType[ITEM_TYPE_USER5]);

            return param;
        }

        public static FlightGroupRulesParameter getParameterFromTableRow(DataRow row)
        {
            if (row == null)
                return null;

            int columnIndexRuleName = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_RULE_NAME);
            int columnIndexFlightCategories = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_FLIGHT_CATEGORIES);
            int columnIndexAirlines = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_AIRLINES);
            int columnIndexAirports = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_AIRPORTS);
            int columnIndexUser1 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_1);
            int columnIndexUser2 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_2);
            int columnIndexUser3 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_3);
            int columnIndexUser4 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_4);
            int columnIndexUser5 = row.Table.Columns.IndexOf(FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_5);

            if (columnIndexRuleName == -1 || columnIndexFlightCategories == -1 || columnIndexAirlines == -1 || columnIndexAirports == -1
                || columnIndexUser1 == -1 || columnIndexUser2 == -1 || columnIndexUser3 == -1 || columnIndexUser4 == -1 || columnIndexUser5 == -1)
            {
                return null;
            }

            FlightGroupRulesParameter param = new FlightGroupRulesParameter();
            if (row[columnIndexRuleName] != null)
                param.ruleName = row[columnIndexRuleName].ToString();
            if (row[columnIndexFlightCategories] != null)
                param.flightCategories = getItemsListFromTableCellContent(row[columnIndexFlightCategories].ToString());
            if (row[columnIndexAirlines] != null)
                param.airlines = getItemsListFromTableCellContent(row[columnIndexAirlines].ToString());
            if (row[columnIndexAirports] != null)
                param.airports = getItemsListFromTableCellContent(row[columnIndexAirports].ToString());
            if (row[columnIndexUser1] != null)
                param.user1Contents = getItemsListFromTableCellContent(row[columnIndexUser1].ToString());
            if (row[columnIndexUser2] != null)
                param.user2Contents = getItemsListFromTableCellContent(row[columnIndexUser2].ToString());
            if (row[columnIndexUser3] != null)
                param.user3Contents = getItemsListFromTableCellContent(row[columnIndexUser3].ToString());
            if (row[columnIndexUser4] != null)
                param.user4Contents = getItemsListFromTableCellContent(row[columnIndexUser4].ToString());
            if (row[columnIndexUser5] != null)
                param.user5Contents = getItemsListFromTableCellContent(row[columnIndexUser5].ToString());
            return param;
        }

        private static List<string> getItemsListFromTableCellContent(string cellContent)
        {
            List<string> items = new List<string>();
            if (cellContent == null || cellContent == string.Empty)
                return items;
            string[] splitCellContent = cellContent.Split(new string[] { ITEMS_SEPARATOR }, StringSplitOptions.None);
            foreach (string content in splitCellContent)
                items.Add(content.Trim());
            return items;
        }

        
    }
}
