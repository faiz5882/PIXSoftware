using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class OCT_AssistantMakeUp : Form
    {
        #region Les diff�rentes variables de la classe.
        private DataTable OCTTable;
        private bool bAEteModifie;
        private bool bDeparture;
        private bool bParking;
        private bool bDelivery;
        private String sAncienneValeur;
        #endregion

        #region Constructeurs et fonctions pour initialiser la classe.
        private void Initialize(DataTable OCTTable_)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            OCTTable = OCTTable_;
            bAEteModifie = false;
            bDeparture = false;
            bParking = false;

            this.Text = "Modify " + OCTTable.TableName;

            txt_FirstValue.BackColor = Color.White;
            txt_SecondValue.BackColor = Color.White;
            txt_EBS_Delivery.BackColor = Color.White;

            if (OCTTable.Columns.Count != 1)
            {
                foreach (DataColumn colonne in OCTTable.Columns)
                {
                    if (colonne.ColumnName != "Select a category")
                    {
                        cb_FC.Items.Add(colonne.ColumnName);
                    }
                }
            }
            lbl_First.Text = OCTTable.Rows[0][0].ToString();
            lbl_Second.Text = OCTTable.Rows[1][0].ToString();
            if (lbl_First.Text.IndexOf("Closing") != -1)
            {
                String tmp = lbl_First.Text;
                lbl_First.Text = lbl_Second.Text;
                lbl_Second.Text = tmp;
            }
            txt_FirstValue.Tag = lbl_First.Text;
            txt_SecondValue.Tag = lbl_Second.Text;

            if (cb_FC.Items.Count != 0)
                cb_FC.SelectedIndex = 0;
        }
        public OCT_AssistantMakeUp(DataTable OCTTable_)
        {
            Initialize(OCTTable_);
        }
        public OCT_AssistantMakeUp(DataTable OCTTable_, String FC)
        {
            Initialize(OCTTable_);
            cb_FC.Text = FC;
        }
        #endregion

        #region Fonction pour le cas ou l'index dans la liste est chang�e
        private void cb_FC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_FC.Text == "")
            {
                gb_Parameters.Enabled = false;
                gb_Delivery.Enabled = false;
                gb_AllocatedMakeUp.Enabled = false;
                gb_Segregations.Enabled = false;
                gb_Containers.Enabled = false;
                return;
            }
            gb_Parameters.Enabled = true;
            gb_Delivery.Enabled = true;
            gb_AllocatedMakeUp.Enabled = true;
            gb_Segregations.Enabled = true;
            gb_Containers.Enabled = true;

            if (sAncienneValeur == cb_FC.Text)
                return;
            BT_OK_Click(null, null);
            if (sAncienneValeur == cb_FC.Text)
                return;
            if (!OCTTable.Columns.Contains(cb_FC.Text))
            {
                MessageBox.Show("Unable to find the selected flight category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sAncienneValeur = cb_FC.Text;

            int iIndexColonne = OCTTable.Columns.IndexOf(cb_FC.Text);

            OverallTools.FormFunctions.FillControl(this, OCTTable, 0, iIndexColonne);
            bAEteModifie = false;
        }
        #endregion

        #region Fonction lorsque le bouton ok est cliqu�
        private void BT_OK_Click(object sender, EventArgs e)
        {
            if((sAncienneValeur == "")||(sAncienneValeur == null))
                return;
            if (!this.Visible)
                return;
            if (!VerifDatas())
            {
                DialogResult = DialogResult.None;
                cb_FC.Text = sAncienneValeur;
                return;
            }
            //Bouton qui permet l'enregistrement des donn�es entr�es dans le formulaire.
            if ((sAncienneValeur != "") && (bAEteModifie))
            {
                if ((sAncienneValeur != cb_FC.Text) || (DialogResult == DialogResult.OK))
                {
                    DialogResult drSave = MessageBox.Show("Do you want to save the changes for the flight category ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        cb_FC.Text = sAncienneValeur;
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        SaveFlightCategorie(sAncienneValeur);
                    }
                }
            }
        }
        #endregion

        #region Fonction pour l'enregistrement de la categorie de vol courante.
        private void SaveFlightCategorie(String ancienneSelection)
        {
            if (!VerifDatas())
            {
                cb_FC.Text = ancienneSelection;
                return;
            }
            int iIndexColonne = OCTTable.Columns.IndexOf(ancienneSelection);
            if (iIndexColonne == -1)
            {
                return;
            }
            OverallTools.FormFunctions.SaveControl(this, OCTTable, 0, iIndexColonne);/*
            int iIndexLigne = OverallTools.DataFunctions.indexLigne(OCTTable, 0, lbl_First.Text);
            if (iIndexLigne == -1)
            {
                return;
            }
            int iIndexSecondLigne = OverallTools.DataFunctions.indexLigne(OCTTable, 0, lbl_Second.Text);
            if (iIndexSecondLigne == -1)
            {
                return;
            }
            int iValue ;
            Int32.TryParse(txt_FirstValue.Text, out iValue);
            OCTTable.Rows[iIndexLigne].BeginEdit();
            OCTTable.Rows[iIndexLigne][iIndexColonne] = iValue;
            Int32.TryParse(txt_SecondValue.Text, out iValue);
            OCTTable.Rows[iIndexSecondLigne].BeginEdit();
            OCTTable.Rows[iIndexSecondLigne][iIndexColonne] = iValue;
            if (bDelivery)
            {
                iIndexSecondLigne = OverallTools.DataFunctions.indexLigne(OCTTable, 0, "EBS delivery time (Min before STD)");
                Int32.TryParse(txt_EBS_Delivery.Text, out iValue);
                OCTTable.Rows[iIndexLigne].BeginEdit();
                OCTTable.Rows[iIndexSecondLigne][iIndexColonne] = iValue;
            }*/
        }
        #endregion

        #region Fonction pour g�rer les zones de textes
        private void txt_FirstValue_TextChanged(object sender, EventArgs e)
        {
            bAEteModifie = true;
            if (cb_FC.Text == "")
                return;
            if(((TextBox)sender).Text=="")
                return;
            int iMinutes;
            if (!Int32.TryParse(((TextBox)sender).Text, out iMinutes))
            {
                MessageBox.Show("Please enter valid values", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void txt_FirstValue_Leave(object sender, EventArgs e)
        {
            if (!VerifDatas())
            {
                /*if((!txt_FirstValue.Focused) && (!txt_SecondValue.Focused))
                    ((TextBox)sender).Focus();*/
            }
        }
        #endregion

        #region Fonctons pour v�rifier la validit� des informations entr�es dans le formulaire.
        private bool VerifDatas()
        {
            int iFirstMinutes;
            if ((!Int32.TryParse(txt_FirstValue.Text, out iFirstMinutes)) || (iFirstMinutes < 0))
            {
                MessageBox.Show("The value for the \"" + lbl_First.Text + "\" must be a number (>=0).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            int iSecondMinutes;
            if ((!Int32.TryParse(txt_SecondValue.Text, out iSecondMinutes)) || (iSecondMinutes < 0))
            {
                MessageBox.Show("The value for the \"" + lbl_Second.Text + "\" must be a number (>=0).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (bParking)
            {
                return true;
            }
            int iEbsTime;
            if ((!Int32.TryParse(txt_EBS_Delivery.Text, out iEbsTime)) || (iEbsTime <= 0))
            {
                MessageBox.Show("The value for the \"EBS delivery time\" must be a number (>0).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (iEbsTime > iFirstMinutes)
            {
                MessageBox.Show("The value for the \"EBS delivery time\" must be lower than the value for the opening time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (iEbsTime < iSecondMinutes)
            {
                MessageBox.Show("The value for the \"EBS delivery time\" must be greater than the value for the closing time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if ((iFirstMinutes <= iSecondMinutes))
            {
                MessageBox.Show("The opening time must be greather than the closing time", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            int iAllocatedMakeUp;

            if ((!Int32.TryParse(txt_Allocated.Text, out iAllocatedMakeUp)) || (iAllocatedMakeUp <= 0))
            {
                MessageBox.Show("The value for the number of allocated Make-Up must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if ((!Int32.TryParse(txt_Segregation.Text, out iAllocatedMakeUp)) || (iAllocatedMakeUp <= 0))
            {
                MessageBox.Show("The value for the number of segregation must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if ((!Int32.TryParse(txt_ContainerSize.Text, out iAllocatedMakeUp)) || (iAllocatedMakeUp <= 0))
            {
                MessageBox.Show("The value for the container size must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if ((!Int32.TryParse(txt_ContainerDeadTime.Text, out iAllocatedMakeUp)) || (iAllocatedMakeUp < 0))
            {
                MessageBox.Show("The value for the container dead time must not be negatif.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if ((!Int32.TryParse(txt_ContainerPerLateral.Text, out iAllocatedMakeUp)) || (iAllocatedMakeUp <= 0))
            {
                MessageBox.Show("The value for the number of container per lateral must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
        #endregion
    }
}