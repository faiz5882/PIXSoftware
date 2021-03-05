using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.Prompt.Allocation.General
{
    public partial class MultiSelectionPopUp : Form
    {
        private List<string> availableItems = new List<string>();
        private List<string> initiallySelectedItems = new List<string>();

        public List<string> selectedItems
        {
            get
            {
                return retrieveAllFromSelectedItemsList();
            }
        }

        #region initialization
        public MultiSelectionPopUp(List<string> _availableItems, List<string> _initiallySelectedItems)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            availableItems = _availableItems;
            initiallySelectedItems = _initiallySelectedItems;
            
            initializeAvailableAndSelectedLists(availableItems, initiallySelectedItems);
        }

        private void initializeAvailableAndSelectedLists(List<string> availableItems, List<string> initiallySelectedItems)
        {
            if (availableItems == null || initiallySelectedItems == null)
                return;
            if (availableItems.Count == 0)
                return;

            foreach (string availableItem in availableItems)
            {
                if (!initiallySelectedItems.Contains(availableItem))
                    availableListBox.Items.Add(availableItem);
            }
            foreach (string selectedItem in initiallySelectedItems)
                selectedListBox.Items.Add(selectedItem);
        }
        #endregion

        #region Moving items between lists
        private void addToSelectionButton_Click(object sender, EventArgs e)
        {
            moveItemBetweenLists(availableListBox, selectedListBox);            
        }

        private void removeFromSelectionButton_Click(object sender, EventArgs e)
        {
            moveItemBetweenLists(selectedListBox, availableListBox);
        }

        //Imported from ExceptionEditor.cs
        private static void moveItemBetweenLists(ListBox fromList, ListBox toList)
        {
            toList.Sorted = true;
            if (fromList.SelectedItems.Count == 0)
                return;
            
            ArrayList lstSelected = new ArrayList();
            for (int i = 0; i < fromList.SelectedItems.Count; i++)
            {
                toList.Items.Add(fromList.SelectedItems[i]);
                lstSelected.Add(fromList.SelectedItems[i]);
            }
            int iIndex = fromList.Items.IndexOf(lstSelected[0]);
            foreach (Object sKey in lstSelected)
            {
                fromList.Items.Remove(sKey);
            }
            if (fromList.Items.Count > 0)
            {
                if (fromList.Items.Count <= iIndex)
                    fromList.SelectedIndex = fromList.Items.Count - 1;
                else
                    fromList.SelectedIndex = iIndex;
            }
        }

        private void addAllItemsButton_Click(object sender, EventArgs e)
        {
            if (availableListBox.Items.Count == 0)
                return;
            selectAllItems(availableListBox);
            moveItemBetweenLists(availableListBox, selectedListBox);
        }

        private void removeAllItemsButton_Click(object sender, EventArgs e)
        {
            if (selectedListBox.Items.Count == 0)
                return;
            selectAllItems(selectedListBox);
            moveItemBetweenLists(selectedListBox, availableListBox);
        }

        private void selectAllItems(ListBox list)
        {
            List<object> itemsToBeSelected = new List<object>();
            foreach (object item in list.Items)
            {
                itemsToBeSelected.Add(item);
            }
            list.ClearSelected();
            foreach (object item in itemsToBeSelected)
            {
                list.SelectedItems.Add(item);
            }
        }
        #endregion

        private void clearSelectionButton_Click(object sender, EventArgs e)
        {
            availableListBox.ClearSelected();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (availableItemsRadioButton.Checked)
            {
                searchListAndSelectItems(availableListBox, searchTextBox.Text);
            }
            else if (selectedItemsRadioButton.Checked)
            {
                searchListAndSelectItems(selectedListBox, searchTextBox.Text);
            }
        }

        private void searchListAndSelectItems(ListBox list, string searchedValue)
        {
            if (searchedValue == null)
            {
                return;
            }
            List<object> selectedItems = new List<object>();
            foreach (object item in list.Items)
            {
                if (item == null || item.ToString() == null)
                {
                    continue;
                }
                if (matchRadioButton.Checked && item.ToString().ToUpper() == searchedValue.ToUpper())
                {
                    selectedItems.Add(item);
                }
                else if (containRadioButton.Checked && item.ToString().ToUpper().Contains(searchedValue.ToUpper()))
                {
                    selectedItems.Add(item);
                }
            }
            list.SelectedItems.Clear();
            foreach (object item in selectedItems)
            {
                list.SelectedItems.Add(item);
            }
        }


        private List<string> retrieveAllFromSelectedItemsList()
        {
            List<string> results = new List<string>();
            foreach (object item in selectedListBox.Items)
            {
                if (item == null || item.ToString() == null)
                {
                    continue;
                }
                results.Add(item.ToString());
            }
            return results;
        }
    }
}
