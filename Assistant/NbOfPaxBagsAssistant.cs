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
    public partial class NbOfPaxBagsAssistant : Form
    {
        DataTable nbOfItemsTable;
        int originatingDepartureColumnIndex = -1;
        int transferringDepartureColumnIndex = -1;
        int terminatingArrivalColumnIndex = -1;
        int transferringArrivalColumnIndex = -1;

        public NbOfPaxBagsAssistant(DataTable pNbOfItemsTable, String objectType)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            nbOfItemsTable = pNbOfItemsTable;

            setTitleAndLabels(objectType);

            setNbOfItemsDataFromSelectedTable(objectType);

        }

        private void setTitleAndLabels(String objectType)
        {
            if (objectType.Equals(GlobalNames.PAX_TYPE_IDENTIFIER))
            {
                this.Text = "Edit Number of Passengers Per Flight";
                
                originatingDepartureLabel.Text = "Originating PAX / Flight";
                transferringDepartureLabel.Text = "Transferring PAX / Flight";

                terminatingArrivalLabel.Text = "Terminating PAX / Flight";
                transferringArrivalLabel.Text = "Transferring PAX / Flight";
            }
            else if (objectType.Equals(GlobalNames.BAG_TYPE_IDENTIFIER))
            {
                this.Text = "Edit Number of Bags Per Flight";

                originatingDepartureLabel.Text = "Originating Bags / Flight";
                transferringDepartureLabel.Text = "Transferring Bags / Flight";

                terminatingArrivalLabel.Text = "Terminating Bags / Flight";
                transferringArrivalLabel.Text = "Transferring Bags / Flight";
            }
        }

        private void setNbOfItemsDataFromSelectedTable(String objectType)
        {
            if (nbOfItemsTable != null && nbOfItemsTable.Rows.Count == 1)
            {
                if (objectType.Equals(GlobalNames.PAX_TYPE_IDENTIFIER))
                {
                    originatingDepartureColumnIndex = nbOfItemsTable.Columns.IndexOf(GlobalNames.originatingPaxDepartureColumnName);
                    transferringDepartureColumnIndex = nbOfItemsTable.Columns.IndexOf(GlobalNames.transferringPaxDepartureColumnName);

                    terminatingArrivalColumnIndex = nbOfItemsTable.Columns.IndexOf(GlobalNames.terminatingPaxArrivalColumnName);
                    transferringArrivalColumnIndex = nbOfItemsTable.Columns.IndexOf(GlobalNames.transferringPaxArrivalColumnName);
                }
                else if (objectType.Equals(GlobalNames.BAG_TYPE_IDENTIFIER))
                {
                    originatingDepartureColumnIndex = nbOfItemsTable.Columns.IndexOf(GlobalNames.originatingBagDepartureColumnName);
                    transferringDepartureColumnIndex = nbOfItemsTable.Columns.IndexOf(GlobalNames.transferringBagDepartureColumnName);

                    terminatingArrivalColumnIndex = nbOfItemsTable.Columns.IndexOf(GlobalNames.terminatingBagArrivalColumnName);
                    transferringArrivalColumnIndex = nbOfItemsTable.Columns.IndexOf(GlobalNames.transferringBagArrivalColumnName);
                }

                DataRow row = nbOfItemsTable.Rows[0];
                if (originatingDepartureColumnIndex != -1 && transferringDepartureColumnIndex != -1
                    && terminatingArrivalColumnIndex != -1 && transferringArrivalColumnIndex != -1)
                {
                    originatingDepartureTextBox.Text = row[originatingDepartureColumnIndex].ToString();
                    transferringDepartureTextBox.Text = row[transferringDepartureColumnIndex].ToString();

                    terminatingArrivalTextBox.Text = row[terminatingArrivalColumnIndex].ToString();
                    transferringArrivalTextBox.Text = row[transferringArrivalColumnIndex].ToString();
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (nbOfItemsTable != null)
            {
                if (DialogResult == DialogResult.OK)
                {
                    DialogResult drSave = MessageBox
                        .Show("Do you want to save the changes ? ", "Information"
                        , MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        if (e != null)
                            DialogResult = DialogResult.None;
                    } 
                    else if (drSave == DialogResult.Yes)
                    {
                        if (!saveTableChanges())
                            DialogResult = DialogResult.None;
                    }
                }
            }
        }

        private bool saveTableChanges()
        {
            if (nbOfItemsTable != null && nbOfItemsTable.Rows.Count > 0)
            {
                Double originatingDepartureNb = 0;
                Double transferringDepartureNb = 0;
                Double terminatingArrivalNb = 0;
                Double transferringArrivalNb = 0;

                if (Double.TryParse(originatingDepartureTextBox.Text, out originatingDepartureNb)
                    && Double.TryParse(transferringDepartureTextBox.Text, out transferringDepartureNb)
                    && Double.TryParse(terminatingArrivalTextBox.Text, out terminatingArrivalNb)
                    && Double.TryParse(transferringArrivalTextBox.Text, out transferringArrivalNb))
                {
                    if (originatingDepartureColumnIndex != -1 && transferringDepartureColumnIndex != -1
                        && terminatingArrivalColumnIndex != -1 && transferringArrivalColumnIndex != -1)
                    {
                        nbOfItemsTable.Rows[0][originatingDepartureColumnIndex] = originatingDepartureNb;
                        nbOfItemsTable.Rows[0][transferringDepartureColumnIndex] = transferringDepartureNb;
                        nbOfItemsTable.Rows[0][terminatingArrivalColumnIndex] = terminatingArrivalNb;
                        nbOfItemsTable.Rows[0][transferringArrivalColumnIndex] = transferringArrivalNb;
                    }
                }
                else
                {
                    MessageBox.Show("Please use only numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (sender != null && (TextBox)sender != null) {
                String value = ((TextBox)sender).Text;
                Double numberValue = 0;
                if (!Double.TryParse(value, out numberValue))
                    MessageBox.Show("Please use only numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}
