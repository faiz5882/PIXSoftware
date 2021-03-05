using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class OCT_Assistant : Form
    {
        #region Les différentes variables de la classe.
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
            if (lbl_First.Text.IndexOf("before") != -1)
            {
                if (lbl_First.Text.IndexOf("after") != -1)
                {
                    bParking = true;
                }
                else
                {
                    bDeparture = true;
                }
            }
            sAncienneValeur = "";
            bDelivery = false;
            if (OCTTable.TableName.EndsWith("_OCT_MakeUp"))
            {
                bDelivery = true;
                gb_Delivery.Visible = true;
                Int32 iDiff = this.Size.Height - (gb_Parameters.Height+gb_Parameters.Location.Y);
                Int32 iDiffButton = this.Size.Height - (BT_OK.Location.Y);
                this.Size= new Size(this.Width ,gb_Delivery.Height + gb_Delivery.Location.Y+iDiff);

                BT_OK.Location = new Point(BT_OK.Location.X, this.Size.Height - iDiffButton);
                BT_CANCEL.Location = new Point(BT_CANCEL.Location.X, this.Size.Height - iDiffButton);
            }
            if (cb_FC.Items.Count != 0)
                cb_FC.SelectedIndex = 0;
        }
        public OCT_Assistant(DataTable OCTTable_)
        {
            Initialize(OCTTable_);
        }
        public OCT_Assistant(DataTable OCTTable_, String FC)
        {
            Initialize(OCTTable_);
            cb_FC.Text = FC;
        }
        #endregion

        #region Fonction pour le cas ou l'index dans la liste est changée
        private void cb_FC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_FC.Text == "")
            {
                gb_Parameters.Enabled = false;
                gb_Delivery.Enabled = false;
                return;
            }
            gb_Parameters.Enabled = true;
            gb_Delivery.Enabled = true;
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
            int iIndexLigne = OverallTools.DataFunctions.indexLigne(OCTTable, 0, lbl_First.Text);
            if ((iIndexLigne == -1) || (iIndexColonne == -1))
            {
                txt_FirstValue.Text = "0";
                txt_SecondValue.Text = "0";
                txt_EBS_Delivery.Text = "0";
                return;
            }
            txt_FirstValue.Text = OCTTable.Rows[iIndexLigne][iIndexColonne].ToString();
            iIndexLigne = OverallTools.DataFunctions.indexLigne(OCTTable, 0, lbl_Second.Text);
            if (iIndexColonne == -1)
            {
                txt_SecondValue.Text = "0";
                return;
            }
            txt_SecondValue.Text = OCTTable.Rows[iIndexLigne][iIndexColonne].ToString();
            if (bDelivery)
            {
                iIndexLigne = OverallTools.DataFunctions.indexLigne(OCTTable, 0, "EBS delivery time (Min before STD)");
                if (iIndexColonne == -1)
                {
                    txt_EBS_Delivery.Text = "0";
                    return;
                }
                txt_EBS_Delivery.Text = OCTTable.Rows[iIndexLigne][iIndexColonne].ToString();

            }
            bAEteModifie = false;
        }
        #endregion

        #region Fonction lorsque le bouton ok est cliqué
        private void BT_OK_Click(object sender, EventArgs e)
        {
            //Bouton qui permet l'enregistrement des données entrées dans le formulaire.
            if ((sAncienneValeur != "") && (bAEteModifie))
            {
                if ((txt_FirstValue.Text == "") || (txt_SecondValue.Text == "") || ((txt_EBS_Delivery.Text == "")&&bDelivery))
                {
                    MessageBox.Show("Please enter valid values for the openning and closing times", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    /*if (txt_FirstValue.Text == "")
                        txt_FirstValue.Focus();
                    else if (txt_SecondValue.Text == "")
                        txt_SecondValue.Focus();
                    else
                        txt_EBS_Delivery.Focus();*/
                    DialogResult = DialogResult.None;
                    return;
                }
                if ((sAncienneValeur != cb_FC.Text) || (DialogResult == DialogResult.OK))
                {
                    DialogResult drSave = MessageBox.Show("Do you want to save the changes for the flight category ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        cb_FC.Text = sAncienneValeur;
                        DialogResult = DialogResult.None;
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
                return;
            }
            int iIndexColonne = OCTTable.Columns.IndexOf(ancienneSelection);
            if (iIndexColonne == -1)
            {
                return;
            }
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
            }
        }
        #endregion

        #region Fonction pour gérer les zones de textes
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

        #region Fonctons pour vérifier la validité des informations entrées dans le formulaire.
        private bool VerifDatas()
        {
            int iFirstMinutes;
            if ((!Int32.TryParse(txt_FirstValue.Text, out iFirstMinutes))||(iFirstMinutes<0))
            {
                MessageBox.Show("The value for the \""+lbl_First.Text+"\" must be a number (>=0).","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;
            }
            int iSecondMinutes;
            if ((!Int32.TryParse(txt_SecondValue.Text, out iSecondMinutes))||(iSecondMinutes<0))
            {
                MessageBox.Show("The value for the \""+lbl_Second.Text+"\" must be a number (>=0).","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;
            }
            if (bParking)
            {
                return true;
            }
            if (bDelivery)
            {
                int iEbsTime;
                if ((!Int32.TryParse(txt_EBS_Delivery.Text, out iEbsTime)) || (iSecondMinutes < 0))
                {
                    MessageBox.Show("The value for the \"EBS delivery time\" must be a number (>=0).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if(iEbsTime<iFirstMinutes)
                {
                    MessageBox.Show("The value for the \"EBS delivery time\" must be greater than the value for the opening time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

            }
            if ((bDeparture) && (iFirstMinutes <= iSecondMinutes)) 
            {
                MessageBox.Show("The opening time must be greather than the closing time","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;
            }
            else if ((!bDeparture) && (iFirstMinutes >= iSecondMinutes))
            {
                MessageBox.Show("The opening time must be smaller than the closing time", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
        #endregion
    }
}