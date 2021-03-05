using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace SIMCORE_TOOL.Assistant
{
    public partial class BHS_Arrival_Containers_Assistant : Form
    {
        #region Les différentes variables de la classe.
        private DataTable dtTable;
        private bool bAEteModifie;
        private String sAncienneValeur;
        private ResourceManager manager;
        #endregion

        #region Constructeurs et fonctions pour initialiser la classe.
        private void Initialize(DataTable dtTable_)
        {
            manager = new ResourceManager("SIMCORE_TOOL.Assistant.BHS_Arrival_Containers_Assistant", Assembly.GetExecutingAssembly());

            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            dtTable = dtTable_;
            bAEteModifie = false;

            this.Text = manager.GetString("S_MainTitle") + dtTable.TableName;

            if (dtTable.Columns.Count != 1)
            {
                foreach (DataColumn colonne in dtTable.Columns)
                {
                    if (colonne.ColumnName != "Select a category")
                    {
                        cb_FC.Items.Add(colonne.ColumnName);
                    }
                }
            }
            lbl_First.Text = dtTable.Rows[0][0].ToString();
            lbl_Second.Text = dtTable.Rows[1][0].ToString();
            lbl_Third.Text = dtTable.Rows[2][0].ToString();
            sAncienneValeur = "";
            if (cb_FC.Items.Count != 0)
                cb_FC.SelectedIndex = 0;
        }
        public BHS_Arrival_Containers_Assistant(DataTable dtTable_)
        {
            Initialize(dtTable_);
        }
        public BHS_Arrival_Containers_Assistant(DataTable dtTable_, String FC)
        {
            Initialize(dtTable_);
            cb_FC.Text = FC;
        }
        #endregion

        #region Fonction pour le cas ou l'index dans la liste est changée
        private void cb_FC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_FC.Text == "")
            {
                gb_Parameters.Enabled = false;
                return;
            }
            gb_Parameters.Enabled = true;
            if (sAncienneValeur == cb_FC.Text)
                return;
            BT_OK_Click(null, null);
            if (sAncienneValeur == cb_FC.Text)
                return;
            if (!dtTable.Columns.Contains(cb_FC.Text))
            {
                MessageBox.Show("Unable to find the selected flight category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sAncienneValeur = cb_FC.Text;

            int iIndexColonne = dtTable.Columns.IndexOf(cb_FC.Text);
            int iIndexLigne = OverallTools.DataFunctions.indexLigne(dtTable, 0, lbl_First.Text);
            if ((iIndexLigne == -1) || (iIndexColonne == -1))
            {
                txt_FirstValue.Text = "0";
                txt_SecondValue.Text = "0";
                return;
            }
            txt_FirstValue.Text = dtTable.Rows[iIndexLigne][iIndexColonne].ToString();
            iIndexLigne = OverallTools.DataFunctions.indexLigne(dtTable, 0, lbl_Second.Text);
            if (iIndexColonne == -1)
            {
                txt_SecondValue.Text = "0";
                return;
            }
            txt_SecondValue.Text = dtTable.Rows[iIndexLigne][iIndexColonne].ToString();

            if(dtTable.Rows[iIndexLigne][iIndexColonne].ToString()=="1")
                cb_Laterals.SelectedIndex = 0;
            else
                cb_Laterals.SelectedIndex = 1;
            bAEteModifie = false;
        }
        #endregion

        #region Fonction lorsque le bouton ok est cliqué
        private void BT_OK_Click(object sender, EventArgs e)
        {
            //Bouton qui permet l'enregistrement des données entrées dans le formulaire.
            if ((sAncienneValeur != "") && (bAEteModifie))
            {
                if ((txt_FirstValue.Text == "") || (txt_SecondValue.Text == "") )
                {
                    MessageBox.Show("Please enter valid values for the openning and closing times", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (txt_FirstValue.Text == "")
                        txt_FirstValue.Focus();
                    else if (txt_SecondValue.Text == "")
                        txt_SecondValue.Focus();
                    DialogResult = DialogResult.None;
                    return;
                }
                if ((sAncienneValeur != cb_FC.Text)
                    || (DialogResult == DialogResult.OK))
                {
                    DialogResult drSave = MessageBox.Show("Do you want to save the changes for the flight category ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        cb_FC.Text = sAncienneValeur;
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        if (!SaveFlightCategorie(sAncienneValeur))
                        {
                            MessageBox.Show("The entered data are not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            DialogResult = DialogResult.None;
                        }
                    }
                }
            }
        }
        #endregion

        #region Fonction pour l'enregistrement de la categorie de vol courante.
        private bool SaveFlightCategorie(String ancienneSelection)
        {
            if (!VerifDatas())
            {
                return false;
            }
            int iIndexColonne = dtTable.Columns.IndexOf(ancienneSelection);
            if (iIndexColonne == -1)
            {
                return false;
            }
            int iIndexLigne = OverallTools.DataFunctions.indexLigne(dtTable, 0, lbl_First.Text);
            if (iIndexLigne == -1)
            {
                return false;
            }
            int iIndexSecondLigne = OverallTools.DataFunctions.indexLigne(dtTable, 0, lbl_Second.Text);
            if (iIndexSecondLigne == -1)
            {
                return false;
            }
            int iValue ;
            Int32.TryParse(txt_FirstValue.Text, out iValue);
            dtTable.Rows[iIndexLigne][iIndexColonne] = iValue;
            Int32.TryParse(txt_SecondValue.Text, out iValue);
            dtTable.Rows[iIndexSecondLigne][iIndexColonne] = iValue;
            if (cb_Laterals.SelectedIndex == 0)
            {
                dtTable.Rows[2][iIndexColonne] = 1;
            }
            else
            {
                dtTable.Rows[2][iIndexColonne] = 2;
            }
            dtTable.AcceptChanges();
            return true;
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
            if (!Int32.TryParse(((TextBox)sender).Text, out iMinutes) || (iMinutes<0))
            {
                MessageBox.Show("Please enter valid values", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        #endregion

        #region Fonctons pour vérifier la validité des informations entrées dans le formulaire.
        private bool VerifDatas()
        {
            int iTmp;
            Int32.TryParse(txt_FirstValue.Text, out iTmp);
            if (iTmp < 0)
                return false;

            Int32.TryParse(txt_SecondValue.Text, out iTmp);
            if (iTmp <= 0)
                return false;
            return true;
        }
        #endregion

        private void cb_Laterals_SelectedIndexChanged(object sender, EventArgs e)
        {
            bAEteModifie = true;
        }
    }
}