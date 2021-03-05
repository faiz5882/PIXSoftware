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
    public partial class FP_ItemSelection : Form
    {
        private DataTable itemsTable;

        private String itemType;

        private int itemCodeColumnIndex = -1;

        private int itemDescriptionColumnIndex = -1;

        private String oldItemCode = "";

        private String selectedItemCode_;

        public String selectedItemCode
        {
            get { return selectedItemCode_; }
        }        

        public FP_ItemSelection(DataTable _itemsTable, String _itemType, String _oldItemCode)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            itemsTable = _itemsTable;
            itemType = _itemType;
            oldItemCode = _oldItemCode;

            if (itemsTable != null)
            {
                this.Text = itemType + " codes search";
                //the description column has the same name for airline/aircraft/airport tables
                itemDescriptionColumnIndex = itemsTable.Columns.IndexOf(GlobalNames.sFPAirline_Description);
                if (itemType.Equals(GlobalNames.FP_ITEM_TYPE_AIRLINE))
                {
                    itemCodeColumnIndex = itemsTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);
                }
                else if (itemType.Equals(GlobalNames.FP_ITEM_TYPE_AIRCRAFT))
                {
                    itemCodeColumnIndex = itemsTable.Columns.IndexOf(GlobalNames.sFPAircraft_AircraftTypes);
                }
                else if (itemType.Equals(GlobalNames.FP_ITEM_TYPE_AIRPORT))
                {
                    itemCodeColumnIndex = itemsTable.Columns.IndexOf(GlobalNames.sFPAirport_AirportCode);
                }

                if (itemCodeColumnIndex != -1 && itemDescriptionColumnIndex != -1)
                {
                    setUpAllItemsListBox();
                }

                if (oldItemCode != null && oldItemCode != "")
                {
                    setUpSelectedItemListBox();
                }
            }
        }

        private void setUpAllItemsListBox()
        {
            if (itemsTable != null && itemCodeColumnIndex != -1 && itemDescriptionColumnIndex != -1)
            {
                allItemsListBox.Items.Clear();

                foreach (DataRow row in itemsTable.Rows)
                {
                    String itemCodeAndDescription = row[itemCodeColumnIndex].ToString() 
                                                        + " (" + row[itemDescriptionColumnIndex].ToString() + ") ";
                    allItemsListBox.Items.Add(itemCodeAndDescription);
                }
            }
        }

        private void setUpSelectedItemListBox()
        {
            selectedItemsListBox.Items.Clear();

            if (oldItemCode != null && oldItemCode != "")
            {
                foreach (String item in allItemsListBox.Items)
                {
                    String itemCode = "";
                    int firstDelimiterIndex = item.IndexOf("(");
                    if (firstDelimiterIndex != -1)                    
                        itemCode = item.Substring(0, firstDelimiterIndex).Trim();
                    if (itemCode != "" && itemCode.Equals(oldItemCode.Trim()))
                    {
                        selectedItemsListBox.Items.Add(item);
                        break;
                    }
                }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            String searchedItem = searchTextBox.Text;
            if (searchedItem != null && searchedItem != "")
            {
                selectItemsBySearchedString(searchedItem, searchByCodeRadioBtn.Checked,
                                                searchByDescrRadioBtn.Checked);
            }
        }

        private void selectItemsBySearchedString(String searchedItem, bool searchByCode, bool searchByDescription)
        {
            selectedItemsListBox.Items.Clear();

            if (searchedItem != null && searchedItem != "")
            {
                List<String> itemsFoundInAvailableList = new List<String>();
                foreach (Object obj in allItemsListBox.Items)
                {
                    String currentItem = obj.ToString().Trim();
                    String currentItemParsed = "";
                    int firstDelimiterIndex = currentItem.IndexOf("(");
                    int lastDelimiterIndex = currentItem.LastIndexOf(")");

                    if (firstDelimiterIndex != -1 && lastDelimiterIndex != -1)
                    {
                        if (searchByCode)
                            currentItemParsed = currentItem.Substring(0, firstDelimiterIndex).Trim();
                        else if (searchByDescription)
                            currentItemParsed = currentItem.Substring(firstDelimiterIndex + 1, lastDelimiterIndex - (firstDelimiterIndex + 1));
                        
                        if (currentItemParsed != null && currentItemParsed != "")
                        {
                            if (currentItemParsed.Contains(searchedItem)
                                && !itemsFoundInAvailableList.Contains(currentItem))
                            {
                                itemsFoundInAvailableList.Add(currentItem);
                            }
                        }
                    }
                }
                if (itemsFoundInAvailableList.Count > 0)
                {
                    foreach (String item in itemsFoundInAvailableList)
                        selectedItemsListBox.Items.Add(item);
                }
                else
                {                    
                    MessageBox.Show("There are no items containing the text " + searchedItem + " in their name.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid search parameter.");
            }
        }

        private static char[] allowedCharsList = { '\b', '-', '_', ' ' };

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) 
                || Array.IndexOf(allowedCharsList, e.KeyChar) > -1)                
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        
        private void addItemToSelectionBtn_Click(object sender, EventArgs e)
        {
            if (allItemsListBox.SelectedItem != null)
            {
                String selectedItem = allItemsListBox.SelectedItem.ToString();
                if (selectedItem != null && selectedItem != "")
                {
                    selectedItemsListBox.Items.Clear();
                    selectedItemsListBox.Items.Add(selectedItem);
                }
            }
        }

        private void removeItemFromSelectionBtn_Click(object sender, EventArgs e)
        {
            List<String> itemsTobBeRemoved = new List<String>();
            foreach (String selectedItem in selectedItemsListBox.SelectedItems)
            {
                itemsTobBeRemoved.Add(selectedItem);
            }
            foreach (String itemToBeRemoved in itemsTobBeRemoved)
            {
                if (selectedItemsListBox.Items.Contains(itemToBeRemoved))
                    selectedItemsListBox.Items.Remove(itemToBeRemoved);
            }
        }

        private void clearSelectionListBtn_Click(object sender, EventArgs e)
        {
            selectedItemsListBox.Items.Clear();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            String errorMessage = "";

            if (selectedItemsListBox.Items.Count == 0
                || (selectedItemsListBox.Items.Count > 1 && selectedItemsListBox.SelectedItems.Count == 0))
            {
                errorMessage = "Please select one item.";
            }
            else if (selectedItemsListBox.Items.Count > 1 && selectedItemsListBox.SelectedItems.Count > 1)
            {
                errorMessage = "Please select only one item.";
            }

            if (errorMessage != "")
            {
                MessageBox.Show(errorMessage, "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.None;
            }
            else
            {

                String selectedItem = null;
                if (selectedItemsListBox.SelectedItem != null)
                    selectedItem = selectedItemsListBox.SelectedItem.ToString();

                if (selectedItem == null && selectedItemsListBox.Items.Count == 1)
                    selectedItem = selectedItemsListBox.Items[0].ToString();

                if (selectedItem != null && selectedItem != "")
                {
                    int firstDelimiterIndex = selectedItem.IndexOf("(");
                    if (firstDelimiterIndex != -1)
                    {
                        String selectedItemCode = selectedItem.Substring(0, firstDelimiterIndex).Trim();
                        if (selectedItemCode != null && selectedItemCode != "")
                            selectedItemCode_ = selectedItemCode;
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }


    }
}
