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
    public partial class UserAttributesEditor : Form
    {
        DataTable userAttributesTable;
        GestionDonneesHUB2SIM doneesEnCours;
        DataTable oldUserAttributesTable;
        List<string> userAttributesToDelete = new List<string>();
        List<string> unmodifiableUserAttributeList = new List<string>();

        public UserAttributesEditor(DataTable userAttributesTable, GestionDonneesHUB2SIM doneesEnCours)
        {            
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            this.userAttributesTable = userAttributesTable;
            this.doneesEnCours = doneesEnCours;
            this.oldUserAttributesTable = doneesEnCours.getTable("Input", GlobalNames.sUserAttributesTableName).Copy();
            addAttributesFromTableToList();
            this.userAttributesToDelete = new List<string>();
            this.unmodifiableUserAttributeList.Add(GlobalNames.flightSubcategoriesTableName);   // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation            
            this.unmodifiableUserAttributeList.AddRange(GlobalNames.PAX_GROUP_USER_ATTRIBUTES_LIST);    // >> Task #10764 Pax2Sim - new User attributes for Groups            
            this.unmodifiableUserAttributeList.Add(GlobalNames.userAttributesReclaimLogTableName);
        }

        public UserAttributesEditor(DataTable userAttributesTable, String attribute, GestionDonneesHUB2SIM doneesEnCours)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            this.userAttributesTable = userAttributesTable;
            this.doneesEnCours = doneesEnCours;
            this.oldUserAttributesTable = doneesEnCours.getTable("Input", GlobalNames.sUserAttributesTableName).Copy();
            addAttributesFromTableToList();
            this.attributeInput.Text = attribute;            
            removeAttributeForReplacement(attribute);
            this.userAttributesToDelete = new List<string>();
        }

        public void addAttributesFromTableToList()
        {
            int indexColumnColumnName = this.userAttributesTable.Columns.IndexOf(GlobalNames.sUserAttributes_ColumnName);
            if (this.userAttributesTable.Rows.Count > 0)
            {
                foreach (DataRow dr in this.userAttributesTable.Rows)
                {
                    String attribute = dr[indexColumnColumnName].ToString();
                    this.userAttributesList.Items.Add(attribute);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String newAttribute = this.attributeInput.Text;
            if (newAttribute.Length > 0)
            {
                if (existsInTable(newAttribute))
                {
                    MessageBox.Show("The attribute is already in the User Attributes table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (existsInList(newAttribute))
                {
                    MessageBox.Show("The attribute is already in the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.userAttributesList.Items.Add(newAttribute);
                /*
                DataRow row = this.userAttributesTable.NewRow();
                row[GlobalNames.sUserAttributes_ColumnName] = newAttribute;
                this.userAttributesTable.Rows.Add(row);*/
                this.attributeInput.Clear();
            }
        }

        private bool existsInTable(String newAttribute)
        {
            DataRow foundRow = this.userAttributesTable.Rows.Find(newAttribute);
            if (foundRow != null)
                return true;
            return false;
        }
        private bool existsInList(String newAttribute)
        {
            if (userAttributesList.Items.Contains(newAttribute))
                return true;
            return false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedItem = userAttributesList.SelectedItem;
            if (selectedItem != null)
            {
                this.userAttributesList.Items.Remove(selectedItem);
                this.userAttributesToDelete.Add(selectedItem.ToString());
                this.attributeInput.Clear();
            }
            /*
            if (userAttributesList.SelectedItem != null)
            {
                var selectedItem = userAttributesList.SelectedItem;
                String attribute = selectedItem.ToString();
                DataRow rowToDelete = this.userAttributesTable.Rows.Find(attribute);
                if (rowToDelete != null)
                {
                    this.userAttributesTable.Rows.Remove(rowToDelete);
                    this.userAttributesList.Items.Remove(selectedItem);
                }
            }
             */ 
        }

        private void removeAttributeForReplacement(String attribute)
        {
            DataRow rowToDelete = this.userAttributesTable.Rows.Find(attribute);
            if (rowToDelete != null)
            {
                this.userAttributesTable.Rows.Remove(rowToDelete);
                if (existsInList(attribute))
                {
                    this.userAttributesList.Items.Remove(attribute);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {    
            // update the user Attributes table with the new values inserted in the list
            foreach (String userAttribute in userAttributesList.Items)
            {
                if (!existsInTable(userAttribute))
                {
                    DataRow dr = this.userAttributesTable.NewRow();
                    dr[GlobalNames.sUserAttributes_ColumnName] = userAttribute;
                    this.userAttributesTable.Rows.Add(dr);
                }
            }
            foreach (String removedUserAttribute in userAttributesToDelete)
            {
                if (existsInTable(removedUserAttribute))
                {
                    DataRow rowToDelete = this.userAttributesTable.Rows.Find(removedUserAttribute);
                    if (rowToDelete != null)                    
                        this.userAttributesTable.Rows.Remove(rowToDelete);
                }                 
            }
            // create new tables / delete unused tables
            doneesEnCours.updateUserAttributes(userAttributesTable, oldUserAttributesTable);
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.userAttributesToDelete.Clear();
             DialogResult =  DialogResult.Cancel;
        }

        private void userAttributesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userAttributesList.SelectedItem != null)
            {
                // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                String selectedUserAttributeName = this.userAttributesList.SelectedItem.ToString();

                if (GlobalNames.nonUserAttributesExceptionsList.Contains(selectedUserAttributeName)
                    || unmodifiableUserAttributeList.Contains(selectedUserAttributeName))   // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation                
                {
                    allowUseOfModifyDeleteButtons(false);
                }
                else
                {
                    allowUseOfModifyDeleteButtons(true);
                }

                this.attributeInput.Text = selectedUserAttributeName;   //this.attributeInput.Text = this.userAttributesList.SelectedItem.ToString();
                // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant                
            }
        }

        // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
        private void allowUseOfModifyDeleteButtons(bool enabled)
        {
            btnModify.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }
        // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant

        private void btnModify_Click(object sender, EventArgs e)
        {
            String modifiedUserAttribute = attributeInput.Text;
            if (existsInTable(modifiedUserAttribute))
            {
                MessageBox.Show("This name is already used by an attribute from the User Attributes table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (existsInList(modifiedUserAttribute))
            {
                MessageBox.Show("This name is already used by an attribute from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedItem = userAttributesList.SelectedItem;
            if (selectedItem != null)
            {
                String oldUserAttribute = selectedItem.ToString();
                this.userAttributesList.Items.Remove(selectedItem);
                this.userAttributesToDelete.Add(oldUserAttribute);
                this.userAttributesList.Items.Add(modifiedUserAttribute);
                this.attributeInput.Clear();
            }
        }

    }
}
