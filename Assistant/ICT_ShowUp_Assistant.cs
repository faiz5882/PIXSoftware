using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class ICT_ShowUp_Assistant : Form
    {

        #region Les différents constructeurs et fonctions d'initialisation de la classe.
        DataTable ICT_ShowUp_table;
        Label[] lbls_Begin;
        Label[] lbls_End;
        TextBox[] mtb_Values;
        bool bAModifieValeurs;
        bool bAModifieStructure;
        const int startXFirst = 20;
        const int startXSecond = 106;
        const int startXThird = 202;
        const int startY = 5;
        const int IncrementY = 20;
        String ancienneSelection;

        bool bParking_;

        bool bAModifieTable;
        public bool AModifieTable
        {
            get
            {
                return bAModifieTable;
            }
        }


        private void Initialize(DataTable table_)
        {
            InitializeComponent();

            OverallTools.FonctionUtiles.MajBackground(this);
            ICT_ShowUp_table = table_;

            this.Text = "Modify " + ICT_ShowUp_table.TableName;
            lbls_Begin = new Label[0];
            lbls_End = new Label[0];
            mtb_Values = new TextBox[0];

            if (ICT_ShowUp_table.Columns.Count != 1)
            {
                foreach (DataColumn colonne in ICT_ShowUp_table.Columns)
                {
                    if ((colonne.ColumnName != "Begin") && (colonne.ColumnName != "End"))
                    {
                        cb_FC.Items.Add(colonne.ColumnName);
                    }
                }
            }
            ancienneSelection = "";
            bAModifieTable = true;
            bAModifieValeurs = true;
            bAModifieStructure = true;
            if (ICT_ShowUp_table.Rows.Count == 0)
            {
                tb_Pas.Text = "60";
                ChangeNumberOfRows(tb_Pas, null);   // >> Bug #10066 Pax2Sim - Airline exception editor
                tb_Start.Text = "0";
                ChangeNumberOfRows(tb_Start, null); // >> Bug #10066 Pax2Sim - Airline exception editor
                tb_Nb.Text = "1";
                ChangeNumberOfRows(tb_Nb, null);    // >> Bug #10066 Pax2Sim - Airline exception editor
            }
            else
            {
                tb_Pas.Text = ((int)(FonctionsType.getInt(ICT_ShowUp_table.Rows[0].ItemArray[1]) - FonctionsType.getInt(ICT_ShowUp_table.Rows[0].ItemArray[0]))).ToString();
                ChangeNumberOfRows(tb_Pas, null);   // >> Bug #10066 Pax2Sim - Airline exception editor
                tb_Start.Text = ICT_ShowUp_table.Rows[0].ItemArray[0].ToString();
                ChangeNumberOfRows(tb_Start, null); // >> Bug #10066 Pax2Sim - Airline exception editor
                tb_Nb.Text = ICT_ShowUp_table.Rows.Count.ToString();
                ChangeNumberOfRows(tb_Nb, null);    // >> Bug #10066 Pax2Sim - Airline exception editor
                bAModifieStructure = false;
            }
            bAModifieTable = false;
            bAModifieValeurs = false;
        }

        public ICT_ShowUp_Assistant(DataTable table)
        {
            Initialize(table);
            cb_FC.SelectedIndex = 0;
        }
        public ICT_ShowUp_Assistant(DataTable table, bool bParking)
        {
            Initialize(table);
            // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason            
            if (cb_FC.Items.Count > 0)
                cb_FC.SelectedIndex = 0;
            else
            {
                MessageBox.Show(this, "There is no Pax In Group object that contains \"parking\" in its description. Please check the Airport structure!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Abort;
                return;
            }
            // << Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason
            bParking_ = bParking;
            if (bParking_)
            {
                label2.Text = "Step (h)";
                label3.Text = "Start (h)";
            }

        }

        public ICT_ShowUp_Assistant(DataTable table, bool bParking, String FC)
        {
            Initialize(table);
            cb_FC.SelectedItem = FC;
            bParking_ = bParking;
            if (bParking_)
            {
                label2.Text = "Step (h)";
                label3.Text = "Start (h)";
            }
        }
        #endregion

        #region Fonction pour valider le contenu d'un masqued text box de type pourcentage.
        private void Poucentage_Validating(object sender, CancelEventArgs e)
        {
            if (bParking_ && cb_FC.SelectedIndex == 0)
            {
                bAModifieValeurs = true;
                return;
            }
            Double dValue = ParsePourcentage(((TextBox)sender).Text);
            if ((dValue > 100) || (dValue < 0))
            {
                ((TextBox)sender).BackColor = Color.Red;
                //MessageBox.Show("You must select a value between 0% and 100%", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            else
                if (((TextBox)sender).BackColor == Color.Red)
                ((TextBox)sender).BackColor = Color.White;

            bAModifieValeurs = true;
        }
        private double ParsePourcentage(String pourcentage)
        {
            Double dvalue;
            if (!Double.TryParse(pourcentage, out dvalue))
            {
                return -1;
            }
            return dvalue;
        }
        #endregion

        #region Fonction pour l'ajout de nouvelles lignes.
        private void ChangeNumberOfRows(object sender, EventArgs e)
        {
            bAModifieStructure = true;
            if ((tb_Pas.Text == "") || (tb_Start.Text == "") || (tb_Nb.Text == ""))
                return;
            if (tb_Pas.Text.Contains(" "))
            {
                MessageBox.Show("Please enter only numerical value for the step value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int iPas;
            int iDebut;
            int iNb;
            if (!Int32.TryParse(tb_Pas.Text, out iPas))
                return;
            if (!Int32.TryParse(tb_Start.Text, out iDebut))
                return;
            if (!Int32.TryParse(tb_Nb.Text, out iNb))
                return;
            if ((iPas == 0) || (iNb == 0))
            {
                return;
            }
            if (((TextBox)sender).Name == tb_Nb.Name)
            {
                this.SuspendLayout();
                //Il faut ajouter ou supprimer des labels et maskedtextbox.
                if (iNb > lbls_Begin.Length)
                {
                    //Il faut en ajouter.
                    Label[] tmp1 = new Label[iNb];
                    Label[] tmp2 = new Label[iNb];
                    TextBox[] tmp3 = new TextBox[iNb];
                    for (int i = 0; i < iNb; i++)
                    {
                        if (i < lbls_Begin.Length)
                        {
                            tmp1[i] = lbls_Begin[i];
                            tmp2[i] = lbls_End[i];
                            tmp3[i] = mtb_Values[i];
                        }
                        else
                        {
                            tmp1[i] = createLabel(i, true);
                            tmp2[i] = createLabel(i, false);
                            tmp3[i] = createTextBox(i);
                        }
                    }
                    lbls_Begin = tmp1;
                    lbls_End = tmp2;
                    mtb_Values = tmp3;

                }
                else
                {
                    //Il faut en supprimer.
                    Label[] tmp1 = new Label[iNb];
                    Label[] tmp2 = new Label[iNb];
                    TextBox[] tmp3 = new TextBox[iNb];
                    for (int i = 0; i < lbls_Begin.Length; i++)
                    {
                        if (i < iNb)
                        {
                            tmp1[i] = lbls_Begin[i];
                            tmp2[i] = lbls_End[i];
                            tmp3[i] = mtb_Values[i];
                        }
                        else
                        {
                            lbls_Begin[i].Dispose();
                            lbls_End[i].Dispose();
                            mtb_Values[i].Dispose();
                        }
                    }
                    lbls_Begin = tmp1;
                    lbls_End = tmp2;
                    mtb_Values = tmp3;
                }
                this.ResumeLayout();
            }

            for (int i = 0; i < lbls_Begin.Length; i++)
            {
                lbls_Begin[i].Text = ((int)(iDebut + (i * iPas))).ToString();
                lbls_End[i].Text = ((int)(iDebut + (i * iPas) + iPas)).ToString();
            }
            CheckSum(mtb_Values[0], null);
        }

        private Label createLabel(int iIndex, bool firstPosition)
        {
            int x = startXFirst;
            if (!firstPosition)
            {
                x = startXSecond;
            }
            int y = startY + (iIndex * IncrementY);
            Label result = new Label();
            result.BackColor = Color.Transparent;
            result.ForeColor = Color.Black;
            result.Location = new Point(x, y);
            result.Parent = p_Content;
            result.Size = new Size(35, 13);
            return result;
        }
        private TextBox createTextBox(int iIndex)
        {
            int x = startXThird;
            int y = startY + (iIndex * IncrementY) - 3;
            TextBox result = new TextBox();
            result.BackColor = Color.White;
            result.ForeColor = Color.Black;
            result.Location = new Point(x, y);
            result.Parent = p_Content;
            result.Size = new Size(54, 20);
            //result.Mask = "999.99%";
            result.TabIndex = iIndex + 5;
            result.Text = "0";
            result.TextAlign = HorizontalAlignment.Right;
            result.TextChanged += new EventHandler(CheckSum);
            result.Validating += new CancelEventHandler(Poucentage_Validating);
            return result;
        }

        private void CheckSum(object sender, EventArgs e)
        {
            bAModifieValeurs = true;
            double value = 0;
            foreach (TextBox mtb in mtb_Values)
            {
                Double dTmp = ParsePourcentage(mtb.Text);
                if (dTmp != -1)
                    value += dTmp;
                mtb.BackColor = Color.White;
            }
            if ((value > 100) && ((!bParking_) || (cb_FC.SelectedIndex != 0)))
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.White;
            }

            if ((value == 100) || (bParking_ && (cb_FC.SelectedIndex == 0)))
            {
                foreach (TextBox mtb in mtb_Values)
                {
                    mtb.BackColor = Color.LightGreen;
                }
            }
            else if ((value > 100) && ((!bParking_) || (cb_FC.SelectedIndex != 0)))
            {
                foreach (TextBox mtb in mtb_Values)
                {
                    mtb.BackColor = Color.Red;
                }
            }



            lbl_Sum.Text = "Sum (Must be 100): " +value.ToString();
        }
        #endregion

        #region Fonctions pour la gestion des différentes intéractions avec la liste déroulante des FC
        private void cb_FC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ancienneSelection == cb_FC.Text)
                return;
            BT_OK_Click(null, null);
            if (ancienneSelection == cb_FC.Text)
                return;
            if (!ICT_ShowUp_table.Columns.Contains(cb_FC.Text))
            {
                MessageBox.Show("Impossible to find the selected flight category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int iIndexLigne = ICT_ShowUp_table.Columns.IndexOf(cb_FC.Text);

            for (int i = 0; i < ICT_ShowUp_table.Rows.Count; i++)
            {
                mtb_Values[i].Text = ConvertToPercent(ICT_ShowUp_table.Rows[i].ItemArray[iIndexLigne].ToString());
            }
            ancienneSelection = cb_FC.Text;
            bAModifieValeurs = false;
        }
        private String ConvertToPercent(String sValue)
        {
            Double Value;
            if (!Double.TryParse(sValue, out Value))
            {
                return "0";
            }
            return Value.ToString();
        }
        #endregion

        #region Fonction pour l'enregistrement des informations dans la table.
        private void BT_OK_Click(object sender, EventArgs e)
        {
            if ((ancienneSelection != "") && (bAModifieValeurs))
            {
                if (mtb_Values[0].BackColor != Color.LightGreen)
                {
                    MessageBox.Show("The sum must be 100. Please modify your data.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cb_FC.Text = ancienneSelection;
                    DialogResult = DialogResult.None;
                    return;
                }
                if ((ancienneSelection != cb_FC.Text) || (DialogResult == DialogResult.OK))
                {
                    DialogResult drSave;
                    if (bParking_)
                        drSave = MessageBox.Show("Do you want to save the changes to table ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    else
                        drSave = MessageBox.Show("Do you want to save the changes to the Flight category table ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        cb_FC.Text = ancienneSelection;
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        if (!SaveFlightCategorie(ancienneSelection))
                            DialogResult = DialogResult.None;
                    }
                }
            }
        }

        private bool SaveFlightCategorie(string ancienneSelection)
        {
            Int32 iValueMax;
            if (!Int32.TryParse(lbls_End[lbls_End.Length - 1].Text, out iValueMax))
            {
                return false;
            }
            if ((PAX2SIM.bTrialVersion) && (GestionDonneesHUB2SIM.CI_ShowUpMax < iValueMax))
            {
                MessageBox.Show("Unable to save. The maximum time for the range for " + ICT_ShowUp_table.TableName + " can't be greater than " + GestionDonneesHUB2SIM.CI_ShowUpMax.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (bAModifieStructure)
            {
                DataTable ancienneTable = ICT_ShowUp_table.Copy();
                ICT_ShowUp_table.Clear();
                for (int i = 0; i < mtb_Values.Length; i++)
                {
                    DataRow newLigne = ICT_ShowUp_table.NewRow();
                    newLigne[0] = FonctionsType.getInt(lbls_Begin[i].Text);
                    newLigne[1] = FonctionsType.getInt(lbls_End[i].Text);
                    for (int j = 2; j < ICT_ShowUp_table.Columns.Count; j++)
                    {
                        if (ancienneTable.Rows.Count > i)
                        {
                            newLigne[j] = ancienneTable.Rows[i].ItemArray[j];
                        }
                        else
                        {
                            newLigne[j] = 0;
                        }
                    }
                    ICT_ShowUp_table.Rows.Add(newLigne);
                }
                bAModifieStructure = false;
            }
            int iIndexColonne = ICT_ShowUp_table.Columns.IndexOf(ancienneSelection);
            for (int i = 0; i < mtb_Values.Length; i++)
            {
                ICT_ShowUp_table.Rows[i].BeginEdit();
                ICT_ShowUp_table.Rows[i][iIndexColonne] = Math.Round(ParsePourcentage(mtb_Values[i].Text) /*/ 100*/, 4);
            }
            ICT_ShowUp_table.AcceptChanges();
            return true;
        }
        #endregion

        private void AdjustRowsClick(object sender, EventArgs e)
        {
            ChangeNumberOfRows(tb_Nb, null);    // >> Bug #10066 Pax2Sim - Airline exception editor
        }

    }
}