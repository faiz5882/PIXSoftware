using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt.Liege
{
    public partial class PrioritiesAssistant : Form
    {
        private const int UNUSED_RESOURCE_PRIORITY = -1;
        DataTable prioritiesTable;
        Dictionary<string, List<PriorityResource>> flightCategoriesWithResourcesPrioritiesList 
            = new Dictionary<string, List<PriorityResource>>();
                
        private string previousSelectedFlightCategory = "";
        private bool currentFlightCategoryPrioritiesListNeedsUpdate = false;
        private bool madeAnyChanges = false;

        public PrioritiesAssistant(DataTable _prioritiesTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            prioritiesTable = _prioritiesTable;

            if (prioritiesTable == null)
            {
                MessageBox.Show(this, "The Priorities table does not exist or could not be loaded. Please check the table again.", 
                                    null, MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            flightCategoriesWithResourcesPrioritiesList 
                = getResourcesWithInitialPrioritiesGroupedByFlightCategory();
            fillFlightCategoryComboBox(flightCategoriesWithResourcesPrioritiesList);
            
            if (flightCategoryComboBox.SelectedItem != null)
            {
                refreshAvailableResourcesList(flightCategoryComboBox.SelectedItem.ToString(),
                    flightCategoriesWithResourcesPrioritiesList);                
                refreshPrioritiesResourcesList(flightCategoryComboBox.SelectedItem.ToString(),
                    flightCategoriesWithResourcesPrioritiesList);

                previousSelectedFlightCategory = flightCategoryComboBox.SelectedItem.ToString();
            }
        }

        private void fillFlightCategoryComboBox(Dictionary<string, List<PriorityResource>> flightCategoriesWithResourcesPrioritiesList)
        {
            flightCategoryComboBox.Items.Clear();
            foreach (string flightCategory in flightCategoriesWithResourcesPrioritiesList.Keys)
            {
                flightCategoryComboBox.Items.Add(flightCategory);
            }
            if (flightCategoryComboBox.Items.Count > 0)
                flightCategoryComboBox.SelectedIndex = 0;
        }

        private void refreshAvailableResourcesList(string selectedFlightCategory,
            Dictionary<string, List<PriorityResource>> flightCategoriesWithResourcesPrioritiesList)
        {
            availableResourcesListBox.Items.Clear();
            if (flightCategoriesWithResourcesPrioritiesList.ContainsKey(selectedFlightCategory))
            {
                List<PriorityResource> priorityResources = flightCategoriesWithResourcesPrioritiesList[selectedFlightCategory];
                foreach (PriorityResource priorityResource in priorityResources)
                {
                    if (priorityResource.priority == UNUSED_RESOURCE_PRIORITY)
                    {
                        availableResourcesListBox.Items.Add(priorityResource.name);
                    }
                }
            }
        }

        private void refreshPrioritiesResourcesList(string selectedFlightCategory,
            Dictionary<string, List<PriorityResource>> flightCategoriesWithResourcesPrioritiesList)
        {
            if (selectedFlightCategory != null)
            {
                prioritiesListBox.Items.Clear();                
                if (flightCategoriesWithResourcesPrioritiesList.ContainsKey(selectedFlightCategory))
                {
                    List<PriorityResource> resourcePriorities = flightCategoriesWithResourcesPrioritiesList[selectedFlightCategory];
                    foreach (PriorityResource resourcePriority in resourcePriorities)
                    {
                        if (resourcePriority.priority >= 0)
                            prioritiesListBox.Items.Add(resourcePriority.name);
                    }
                }
            }
        }

        private Dictionary<string, List<PriorityResource>> getResourcesWithInitialPrioritiesGroupedByFlightCategory()
        {
            Dictionary<string, List<PriorityResource>> flightCategoriesWithResourcesPrioritiesList = new Dictionary<string, List<PriorityResource>>();
            if (prioritiesTable != null)
            {
                for (int i = 0; i < prioritiesTable.Columns.Count; i++)
                {
                    DataColumn column = prioritiesTable.Columns[i];
                    if (column.ColumnName == GlobalNames.sColumnSelectACategory)
                        continue;
                    string flightCategory = column.ColumnName;
                    List<PriorityResource> resourcesWithPriorities = getResourcePrioritiesListByFlightCategory(prioritiesTable, i);
                    if (!flightCategoriesWithResourcesPrioritiesList.ContainsKey(flightCategory))
                        flightCategoriesWithResourcesPrioritiesList.Add(flightCategory, resourcesWithPriorities);
                }
            }
            return flightCategoriesWithResourcesPrioritiesList;
        }

        private List<PriorityResource> getResourcePrioritiesListByFlightCategory(DataTable prioritiesTable,
            int flightCategoryColumnIndex)
        {
            List<PriorityResource> resourcePriorities = new List<PriorityResource>();
            if (prioritiesTable != null && flightCategoryColumnIndex < prioritiesTable.Columns.Count)
            {
                int resourceNameColumnIndex = prioritiesTable.Columns.IndexOf(GlobalNames.sColumnSelectACategory);
                if (resourceNameColumnIndex == -1)
                    return resourcePriorities;

                foreach (DataRow row in prioritiesTable.Rows)
                {
                    string resourceName = row[resourceNameColumnIndex].ToString();
                    int priority = UNUSED_RESOURCE_PRIORITY;
                    if (Int32.TryParse(row[flightCategoryColumnIndex].ToString(), out priority))
                    {
                        PriorityResource resourcePriority = new PriorityResource(resourceName, priority);
                        resourcePriorities.Add(resourcePriority);
                    }
                }
            }
            try
            {
                resourcePriorities = resourcePriorities.OrderBy(res => res.priority).ToList();
            }
            catch (ArgumentNullException argNullExc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Error while trying to obtain data from the " + prioritiesTable.TableName + " table. "
                                                            + argNullExc.Message);
            }
            return resourcePriorities;
        }


        private void flightCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateFlightCategoriesDictionary(previousSelectedFlightCategory, flightCategoriesWithResourcesPrioritiesList);
            if (flightCategoryComboBox.SelectedItem != null)
            {
                refreshAvailableResourcesList(flightCategoryComboBox.SelectedItem.ToString(),
                    flightCategoriesWithResourcesPrioritiesList);
                refreshPrioritiesResourcesList(flightCategoryComboBox.SelectedItem.ToString(), 
                    flightCategoriesWithResourcesPrioritiesList);
                previousSelectedFlightCategory = flightCategoryComboBox.SelectedItem.ToString();
            }            
        }

        private void updateFlightCategoriesDictionary(string previousSelectedFlightCategory, 
            Dictionary<string, List<PriorityResource>> flightCategoriesWithResourcesPrioritiesList)
        {
            if (currentFlightCategoryPrioritiesListNeedsUpdate
                && flightCategoriesWithResourcesPrioritiesList.ContainsKey(previousSelectedFlightCategory))
            {
                List<PriorityResource> previousPriorityResources = flightCategoriesWithResourcesPrioritiesList[previousSelectedFlightCategory];
                List<PriorityResource> priorityResources = getPriorityResourcesListFromListBox();
                flightCategoriesWithResourcesPrioritiesList[previousSelectedFlightCategory] = priorityResources;
                addUnusedResources(previousPriorityResources, priorityResources);

                currentFlightCategoryPrioritiesListNeedsUpdate = false;
                madeAnyChanges = true;
            }
        }

        private void addUnusedResources(List<PriorityResource> previousPriorityResources, List<PriorityResource> priorityResources)
        {
            List<string> usedResourceNames = new List<string>();
            foreach (PriorityResource priorityResource in priorityResources)
            {
                usedResourceNames.Add(priorityResource.name);
            }

            foreach (PriorityResource previousPriorityResource in previousPriorityResources)
            {
                if (!usedResourceNames.Contains(previousPriorityResource.name))
                {
                    PriorityResource unusedResource = new PriorityResource(previousPriorityResource.name, UNUSED_RESOURCE_PRIORITY);
                    priorityResources.Add(unusedResource);
                }
            }
        }

        private List<PriorityResource> getPriorityResourcesListFromListBox()
        {
            List<PriorityResource> priorityResources = new List<PriorityResource>();
            for (int i = 0; i < prioritiesListBox.Items.Count; i++)
            {
                PriorityResource priorityResource = new PriorityResource(prioritiesListBox.Items[i].ToString(), i + 1);
                priorityResources.Add(priorityResource);
            }
            return priorityResources;
        }

        private void moveRightButton_Click(object sender, EventArgs e)
        {
            int selectedAvailableResourceIndex = getSeletctedIndexFromListBox(availableResourcesListBox);
            string selectedAvailableResourceName = getSelectedResourceNameFromListBox(availableResourcesListBox);

            deleteResourceFromListBoxByIndex(selectedAvailableResourceIndex, availableResourcesListBox);
            addResourceToListBoxByName(selectedAvailableResourceName, prioritiesListBox);

            selectTheNextItemFromRemainingAfterRemovingFromList(selectedAvailableResourceIndex, availableResourcesListBox);
            currentFlightCategoryPrioritiesListNeedsUpdate = true;
        }

        private void moveLeftButton_Click(object sender, EventArgs e)
        {
            int selectedPriorityResourceIndex = getSeletctedIndexFromListBox(prioritiesListBox);
            string selectedPriorityResourceName = getSelectedResourceNameFromListBox(prioritiesListBox);

            deleteResourceFromListBoxByIndex(selectedPriorityResourceIndex, prioritiesListBox);
            addResourceToListBoxByName(selectedPriorityResourceName, availableResourcesListBox);

            selectTheNextItemFromRemainingAfterRemovingFromList(selectedPriorityResourceIndex, prioritiesListBox);
            availableResourcesListBox.Sorted = true;

            currentFlightCategoryPrioritiesListNeedsUpdate = true;
        }

        private void selectTheNextItemFromRemainingAfterRemovingFromList(int removedResourceOriginListIndex, ListBox originListBox)
        {
            if (originListBox.Items.Count > 0)
            {
                if (removedResourceOriginListIndex >= originListBox.Items.Count)
                    originListBox.SelectedIndex = originListBox.Items.Count - 1;
                else
                    originListBox.SelectedIndex = removedResourceOriginListIndex;
            }
        }

        private int getSeletctedIndexFromListBox(ListBox listBox)
        {
            int selectedIndex = -1;
            if (listBox.SelectedIndex >= 0)
                return selectedIndex = listBox.SelectedIndex;
            return selectedIndex;
        }

        private string getSelectedResourceNameFromListBox(ListBox listBox)
        {
            string name = null;
            if (listBox.SelectedItem != null)
                name = listBox.SelectedItem.ToString();
            return name;
        }

        private void deleteResourceFromListBoxByIndex(int selectedResourceIndex, ListBox listBoxToDeleteFrom)
        {
            if (selectedResourceIndex >= 0 && listBoxToDeleteFrom.Items.Count > selectedResourceIndex)
                listBoxToDeleteFrom.Items.RemoveAt(selectedResourceIndex);
        }

        private void addResourceToListBoxByName(string resourceName, ListBox listBoxToAddTo)
        {
            if (resourceName != null && !listBoxToAddTo.Items.Contains(resourceName))
                listBoxToAddTo.Items.Add(resourceName);
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            int selectedResourceIndex = getSeletctedIndexFromListBox(prioritiesListBox);
            string selectedResourceName = getSelectedResourceNameFromListBox(prioritiesListBox);

            if (selectedResourceIndex > 0)
            {
                modifySelectedResourcePositionInListBox(selectedResourceName, selectedResourceIndex, 
                                                            selectedResourceIndex - 1, prioritiesListBox);
                currentFlightCategoryPrioritiesListNeedsUpdate = true;
            }
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            int selectedResourceIndex = getSeletctedIndexFromListBox(prioritiesListBox);
            string selectedResourceName = getSelectedResourceNameFromListBox(prioritiesListBox);

            if (selectedResourceIndex >= 0 && selectedResourceIndex < prioritiesListBox.Items.Count - 1)
            {
                modifySelectedResourcePositionInListBox(selectedResourceName, selectedResourceIndex,
                                                            selectedResourceIndex + 1, prioritiesListBox);
                currentFlightCategoryPrioritiesListNeedsUpdate = true;
            }
        }

        private void modifySelectedResourcePositionInListBox(string selectedResourceName, int selectedResourceIndex,
            int newIndex, ListBox listBox)
        {
            if (newIndex >=0 && selectedResourceIndex >= 0 && selectedResourceIndex < listBox.Items.Count)
            {
                deleteResourceFromListBoxByIndex(selectedResourceIndex, listBox);
                if (newIndex > listBox.Items.Count)
                    newIndex = listBox.Items.Count;
                listBox.Items.Insert(newIndex, selectedResourceName);                    
                listBox.SelectedIndex = newIndex;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            updateFlightCategoriesDictionary(flightCategoryComboBox.SelectedItem.ToString(), flightCategoriesWithResourcesPrioritiesList);
            if (madeAnyChanges)
            {
                DialogResult result = MessageBox.Show("Do you want to save the changes made?",
                    "Save " + prioritiesTable.TableName + " changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    saveChangesToPrioritiesTable(prioritiesTable, flightCategoriesWithResourcesPrioritiesList);
                }
                else if (result == DialogResult.Cancel)
                    DialogResult = DialogResult.None;                
            }
            else
            {
                DialogResult result = MessageBox.Show("No changes have been made to the " + prioritiesTable.TableName + " table." 
                                                        + Environment.NewLine + "Do you want to exit?", prioritiesTable.TableName + " table editor",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    DialogResult = DialogResult.None;
            }
        }

        private void saveChangesToPrioritiesTable(DataTable prioritiesTable, 
            Dictionary<string, List<PriorityResource>> flightCategoriesWithResourcesPrioritiesList)
        {
            int resourceNameColumnIndex = prioritiesTable.Columns.IndexOf(GlobalNames.sColumnSelectACategory);
            if (resourceNameColumnIndex != -1)
            {
                for (int i = 0; i < prioritiesTable.Columns.Count; i++)
                {
                    DataColumn column = prioritiesTable.Columns[i];
                    if (column.ColumnName == GlobalNames.sColumnSelectACategory)
                        continue;
                    string flightCategory = column.ColumnName;
                    if (flightCategoriesWithResourcesPrioritiesList.ContainsKey(flightCategory))
                    {
                        List<PriorityResource> priorityResources = flightCategoriesWithResourcesPrioritiesList[flightCategory];
                        foreach (DataRow row in prioritiesTable.Rows)
                        {
                            string resourceName = row[resourceNameColumnIndex].ToString();
                            int priority = getPriorityByResourceName(resourceName, priorityResources);
                            row[i] = priority;
                        }
                    }
                }
            }
            prioritiesTable.AcceptChanges();
        }

        private int getPriorityByResourceName(string resourceName, List<PriorityResource> priorityResources)
        {
            if (resourceName != null && priorityResources != null)
            {
                foreach (PriorityResource priorityResource in priorityResources)
                {
                    if (priorityResource.name == resourceName)
                        return priorityResource.priority;
                }
            }
            return UNUSED_RESOURCE_PRIORITY;
        }

        private class PriorityResource
        {
            public string name { get; set; }
            public int priority { get; set; }

            public PriorityResource(string _name, int _priority)
            {
                name = _name;
                priority = _priority;
            }
        }
    }
}
