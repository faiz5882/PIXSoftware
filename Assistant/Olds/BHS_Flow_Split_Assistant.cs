using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class BHS_Flow_Split_Assistant : Form
    {
        #region Les différentes variables de la classe.
        private DataTable dtTable;
        private bool bAEteModifie;
        private String sAncienneValeur;
        #endregion

        #region Constructeurs et fonctions pour initialiser la classe.
        private void Initialize(DataTable dtTable_)
        {
            InitializeComponent();
            setDelhi();
            OverallTools.FonctionUtiles.MajBackground(this);
            dtTable = dtTable_;
            bAEteModifie = false;

            this.Text = "Modify " + dtTable.TableName;

            if (dtTable.Columns.Count != 1)
            {
                foreach (DataColumn colonne in dtTable.Columns)
                {
                    if (colonne.ColumnName != "Flows")
                    {
                        cb_FC.Items.Add(colonne.ColumnName);
                    }
                }
            }
            sAncienneValeur = "";
            if (cb_FC.Items.Count != 0)
                cb_FC.SelectedIndex = 0;
        }
        public BHS_Flow_Split_Assistant(DataTable dtTable_)
        {
            Initialize(dtTable_);
        }
        public BHS_Flow_Split_Assistant(DataTable dtTable_, String FC)
        {
            Initialize(dtTable_);
            cb_FC.Text = FC;
        }
        #endregion

        #region Fonction pour le cas ou l'index dans la liste est changée
        private void cb_FC_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if ((cb_FC.Text == "")||(dtTable.Rows.Count!=8))
            {
                gb_HBS.Enabled = false;
                gb_Interlink.Enabled = false;
                gb_MES.Enabled = false;
                return;
            }*/
            gb_HBS.Enabled = true;
            gb_Interlink.Enabled = true;
            gb_MES.Enabled = true;
            if (sAncienneValeur == cb_FC.Text)
                return;
            if(sAncienneValeur != "")
                SaveFlightCategorie(sAncienneValeur);
            if (sAncienneValeur == cb_FC.Text)
                return;
            if (!dtTable.Columns.Contains(cb_FC.Text))
            {
                MessageBox.Show("Unable to find the selected flight category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int iIndexColumn = dtTable.Columns.IndexOf(cb_FC.Text);
            OverallTools.FormFunctions.FillControl(this, dtTable, 0, iIndexColumn);
            bAEteModifie = false;
        }
        #endregion

        #region Fonction lorsque le bouton ok est cliqué
        private void BT_OK_Click(object sender, EventArgs e)
        {
            //Bouton qui permet l'enregistrement des données entrées dans le formulaire.
            if ((bAEteModifie))
            {
                /*if ((sAncienneValeur != cb_FC.Text)
                    || (DialogResult == DialogResult.OK))
                {*/
                DialogResult drSave = MessageBox.Show("Do you want to save the changes for the flight category ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        cb_FC.Text = sAncienneValeur;
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        if (!SaveFlightCategorie(cb_FC.Text))
                        {
                            DialogResult = DialogResult.None;
                        }
                    }
                //}
            }
        }
        #endregion

        #region Fonction pour l'enregistrement de la categorie de vol courante.
        private bool SaveFlightCategorie(String ancienneSelection)
        {
            if (!OverallTools.FormFunctions.VerifData(this))
            {
                if (cb_FC.Text != ancienneSelection)
                    cb_FC.Text = ancienneSelection;
                MessageBox.Show("The data you are trying to insert are not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            int iIndexColonne = dtTable.Columns.IndexOf(ancienneSelection);
            if (iIndexColonne == -1)
            {
                return false;
            }
            OverallTools.FormFunctions.SaveControl(this, dtTable, 0, iIndexColonne);
            /*Double dValue;
            Double.TryParse(txt_Level2.Text, out dValue);
            dtTable.Rows[0][iIndexColonne] = dValue;
            Double.TryParse(txt_Level3.Text, out dValue);
            dtTable.Rows[1][iIndexColonne] = dValue;
            Double.TryParse(txt_Level4.Text, out dValue);
            dtTable.Rows[2][iIndexColonne] = dValue;
            Double.TryParse(txt_Level5.Text, out dValue);
            dtTable.Rows[3][iIndexColonne] = dValue;
            Double.TryParse(txt_MES_Ori.Text, out dValue);
            dtTable.Rows[4][iIndexColonne] = dValue;
            Double.TryParse(txt_MES_tran.Text, out dValue);
            dtTable.Rows[5][iIndexColonne] = dValue;
            Double.TryParse(txt_IntLnk_Transf.Text, out dValue);
            dtTable.Rows[6][iIndexColonne] = dValue;
            Double.TryParse(txt_IntLnk_Orig.Text, out dValue);
            dtTable.Rows[7][iIndexColonne] = dValue;*/
            dtTable.AcceptChanges();
            return true;
        }
        #endregion

        #region Fonctons pour vérifier la validité des informations entrées dans le formulaire.

        private void UpdateMeans()
        {
            bool bWrong = (txt_Level2.BackColor != Color.White);
            Double dValue =0;
            if (bWrong)
                txt_Level2_Mean.Text = "";
            else
            {
                dValue = Convert.ToDouble(txt_Level2.Text);
                txt_Level2_Mean.Text = Math.Round(dValue, 2).ToString();
            }

            bWrong = bWrong || (txt_Level3.BackColor != Color.White);
            if (bWrong)
                txt_Level3_Mean.Text = "";
            else
            {
                dValue =(dValue *  Convert.ToDouble(txt_Level3.Text))/100.0;
                txt_Level3_Mean.Text = Math.Round(dValue, 2).ToString();
            }

            bWrong = bWrong || (txt_Level4.BackColor != Color.White);
            if (bWrong)
                txt_Level4_Mean.Text = "";
            else
            {
                dValue = (dValue * Convert.ToDouble(txt_Level4.Text)) / 100.0;
                txt_Level4_Mean.Text = Math.Round(dValue, 2).ToString();
            }

            bWrong = bWrong || (txt_Level5.BackColor != Color.White);
            if (bWrong)
                txt_Level5_Mean.Text = "";
            else
            {
                dValue = (dValue * Convert.ToDouble(txt_Level5.Text)) / 100.0;
                txt_Level5_Mean.Text = Math.Round(dValue, 2).ToString();
            }
        }
        private void textBoxTextChanged(object sender, EventArgs e)
        {
            String sValue = ((TextBox)sender).Text;
            Double dValue;
            bAEteModifie = true;
            if (sValue == "")
            {
                ((TextBox)sender).Text = "0";
            }
            else if (!Double.TryParse(sValue, out dValue))
            {
                ((TextBox)sender).BackColor = Color.Red;
                tt_Tooltips.SetToolTip((Control)sender, "The value must be a valid number.");
                return;
            }
            else if ((dValue > 100) || (dValue < 0))
            {
                ((TextBox)sender).BackColor = Color.Red;
                tt_Tooltips.SetToolTip((Control)sender, "The value must be between 0 and 100.");
                return;
            }
            ((TextBox)sender).BackColor = Color.White;
            UpdateMeans();
        }
        #endregion

        private void setDelhi()
        {
            if (!PAX2SIM.bDelhi)
                return;
            lbl_Level3.Text = "HBS2 to HBS3 auto.(%)";
            lbl_Level4.Text = "HBS3 auto. to HBS3 manu.(%)";
            lbl_Level5.Text = "HBS3 manu. to HBS4 (%)";

            txt_Level2.Tag = "% HBS1 to HBS2";
            txt_Level3.Tag = "% HBS2 to HBS3 auto.";
            txt_Level4.Tag = "% HBS3 auto. to HBS3 manu.";
            txt_Level5.Tag = "% HBS3 manu. to HBS4";

            lbl_DMRC.Visible = true;
            txt_DMRC.Visible = true;
            txt_DMRC.Tag = "% MES (DMRC Bags)";
            this.Size = new Size(this.Size.Width, this.Size.Height + 25);
            gb_MES.Size = new Size(gb_MES.Size.Width, gb_MES.Size.Height+25);
            gb_Interlink.Location = new Point(gb_Interlink.Location.X, gb_Interlink.Location.Y + 25);
            BT_OK.Location = new Point(BT_OK.Location.X, BT_OK.Location.Y + 25);
            BT_CANCEL.Location = new Point(BT_CANCEL.Location.X, BT_CANCEL.Location.Y + 25);
        }
    }
}