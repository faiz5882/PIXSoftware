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
    public partial class Nb_Bags_Visitors_Assistant : Form
    {

        #region Les différents constructeurs et fonctions d'initialisation de la classe.
        //Pour la gestion des fichiers resssource
        private ResourceManager manager;
        //Pour les messages à afficher dans les MessageBox
        private string[] messages;

        DataTable ICT_ShowUp_table;
        Label[] lbls_Nb;
        TextBox[] mtb_Values;
        bool bAModifieValeurs;
        bool bAModifieStructure;
        int iStartIndex;
        const int startXFirst = 65;
        const int startXThird = 202;
        const int startY = 10;
        const int IncrementY = 20;
        String ancienneSelection;

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
            /////////////////////////////
            //Creation du resourceManager
            ////////////////////////////
            manager = new ResourceManager("SIMCORE_TOOL.Assistant.Nb_Bags_Visitors_Assistant", Assembly.GetExecutingAssembly());
            //////////////////////////////
            //Initialisation des messages
            /////////////////////////////
            messages = new string[3];
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = manager.GetString("message" + (i + 1));
            }

            InitializeComponent();

            OverallTools.FonctionUtiles.MajBackground(this);
            ICT_ShowUp_table = table_;

            iStartIndex = 0;
            this.Text = "Modify " + ICT_ShowUp_table.TableName;
            lbls_Nb = new Label[0];
            mtb_Values = new TextBox[0];

            tb_Nb.BackColor = Color.White;

            if (ICT_ShowUp_table.Columns.Count != 1)
            {
                foreach (DataColumn colonne in ICT_ShowUp_table.Columns)
                {
                    if ((colonne.ColumnName != "NbBags")
                        && (colonne.ColumnName != "NbVisitors")
                        && (colonne.ColumnName != "Segregation"))
                    {
                        cb_FC.Items.Add(colonne.ColumnName);
                    }
                }
            }
            ancienneSelection = "";
            bAModifieTable = true;
            bAModifieValeurs = true;
            bAModifieStructure = true;

            iStartIndex = (int)ICT_ShowUp_table.Rows[0][0];
            int iMax = 0;
            foreach (DataRow row in ICT_ShowUp_table.Rows)
            {
                if ((int)row[0] > iMax)
                    iMax = (int)row[0];
            }
            tb_Nb.Text = iMax.ToString();
            ChangeNumberOfRows(tb_Nb, null);
            bAModifieTable = false;
            bAModifieValeurs = false;
            bAModifieStructure = false;
        }

        public Nb_Bags_Visitors_Assistant(DataTable table)
        {
            Initialize(table);
            cb_FC.SelectedIndex = 0;
        }

        public Nb_Bags_Visitors_Assistant(DataTable table, String FC)
        {
            Initialize(table);
            cb_FC.SelectedItem = FC;
        }
        #endregion

        #region Fonction pour valider le contenu d'un masqued text box de type pourcentage.
        private void Poucentage_Validating(object sender, CancelEventArgs e)
        {
            Double dValue = ParsePourcentage(((TextBox)sender).Text);
            if ((dValue > 100) || (dValue < 0))
            {
                ((TextBox)sender).BackColor = Color.Red;
                //MessageBox.Show("You must select a value between 0% and 100%", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            else
            {
                if (((TextBox)sender).BackColor == Color.Red)
                    ((TextBox)sender).BackColor = Color.White;
            }
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
            int iNb = 0;
            if (tb_Nb.Text == "")
            {
                tb_Nb.Text = iStartIndex.ToString();
                return;
            }
            if (!Int32.TryParse(tb_Nb.Text, out iNb))
            {
                tb_Nb.Text = iStartIndex.ToString();
                return;
            }
            if (iNb < iStartIndex)
            {
                tb_Nb.Text = iStartIndex.ToString();
                return;
            }
            if (iNb > 50)
            {
                tb_Nb.Text = "50";
                return;
            }
            iNb++;
            iNb -= iStartIndex;
            if (((TextBox)sender).Name == tb_Nb.Name)
            {
                this.SuspendLayout();
                //Il faut ajouter ou supprimer des labels et maskedtextbox.
                if (iNb > lbls_Nb.Length)
                {
                    if (lbls_Nb != null && lbls_Nb.Length > 0)
                        pContent.ScrollControlIntoView(lbls_Nb[0]);
                    //Il faut en ajouter.
                    Label[] tmp1 = new Label[iNb];
                    TextBox[] tmp3 = new TextBox[iNb];
                    for (int i = 0; i < iNb; i++)
                    {
                        if (i < lbls_Nb.Length)
                        {
                            tmp1[i] = lbls_Nb[i];
                            tmp3[i] = mtb_Values[i];
                        }
                        else
                        {
                            tmp1[i] = createLabel(i);
                            tmp3[i] = createTextBox(i);
                        }
                    }
                    lbls_Nb = tmp1;
                    mtb_Values = tmp3;

                }
                else
                {
                    //Il faut en supprimer.
                    Label[] tmp1 = new Label[iNb];
                    TextBox[] tmp3 = new TextBox[iNb];
                    for (int i = 0; i < lbls_Nb.Length; i++)
                    {
                        if (i < iNb)
                        {
                            tmp1[i] = lbls_Nb[i];
                            tmp3[i] = mtb_Values[i];
                        }
                        else
                        {
                            lbls_Nb[i].Dispose();
                            mtb_Values[i].Dispose();
                        }
                    }
                    lbls_Nb = tmp1;
                    mtb_Values = tmp3;
                }
                //lbl_Sum.Location = new Point(startXThird - 100, startY + (lbls_Nb.Length * IncrementY));
                //lbl_Mean.Location = new Point(startXThird - 100, startY + (lbls_Nb.Length * IncrementY)+15);
                //this.Height = startY + (IncrementY * mtb_Values.Length) + 123;
                //BT_CANCEL.Location = new Point(BT_CANCEL.Location.X, this.Height - 103);
                //BT_OK.Location = new Point(BT_OK.Location.X, this.Height - 103);
                this.ResumeLayout();
            }

            for (int i = 0; i < lbls_Nb.Length; i++)
            {
                lbls_Nb[i].Text = (i + iStartIndex).ToString();
            }
            CheckSum(mtb_Values[0], null);
        }
        private Label createLabel(int iIndex)
        {

            int x = startXFirst;
            int y = startY + (iIndex * IncrementY);
            Label result = new Label();
            result.BackColor = Color.Transparent;
            result.ForeColor = Color.Black;
            result.Location = new Point(x, y);
            result.Parent = pContent;
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
            result.Parent = pContent;
            result.Size = new Size(54, 20);
            result.TabIndex = iIndex + 5;
            result.Text = "0";
            result.TextChanged += new EventHandler(CheckSum);
            result.Validating += new CancelEventHandler(Poucentage_Validating);
            return result;
        }
        private void CheckSum(object sender, EventArgs e)
        {
            bAModifieValeurs = true;
            double value = 0;
            Double mean = 0;
            Double dTmp;
            for (int i = 0; i < mtb_Values.Length; i++)
            {
                dTmp = ParsePourcentage(mtb_Values[i].Text);
                if (dTmp != -1)
                {
                    value += dTmp;
                    mean += dTmp * i;
                    mtb_Values[i].BackColor = Color.White;
                }
                else
                {

                    mtb_Values[i].BackColor = Color.Red;
                }
            }
            if (mtb_Values.Length > 0)
                mean = mean / (100);
            if (value > 100)
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.White;
            }
            if (value == 100)
            {
                foreach (TextBox mtb in mtb_Values)
                {
                    mtb.BackColor = Color.LightGreen;
                }
            }
            else if(value > 100)
            {
                foreach (TextBox mtb in mtb_Values)
                {
                    mtb.BackColor = Color.Red;
                }
            }

            lbl_Sum.Text = manager.GetString("lbl_Sum.Text") + ": " + Math.Round(value, 2).ToString();
            lbl_Mean.Text = manager.GetString("lbl_Mean.Text") + ": " + Math.Round(mean, 2).ToString();
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
                MessageBox.Show(messages[0], "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("The sum must be 100. Please modify your data.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);// << Task #7949 Capacity Analysis - IST tables modification
                    //cb_FC.Text = ancienneSelection;
                    if (e != null)
                        DialogResult = DialogResult.None;
                    return;
                }
                if ((ancienneSelection != cb_FC.Text) || (DialogResult == DialogResult.OK))
                {
                    DialogResult drSave = MessageBox.Show("Do you want to save the changes to table ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);// << Task #7949 Capacity Analysis - IST tables modification
                    if (drSave == DialogResult.Cancel)
                    {
                        cb_FC.Text = ancienneSelection;
                        if (e != null)
                            DialogResult = DialogResult.None;
                    }
                    else if (drSave == DialogResult.No)
                    {
                        if (e != null)
                            DialogResult = DialogResult.None;
                        tb_Nb.Text = (ICT_ShowUp_table.Rows.Count - 1).ToString();
                        ChangeNumberOfRows(tb_Nb, null);
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        SaveFlightCategorie(ancienneSelection);
                    }
                }
            }
        }

        private void SaveFlightCategorie(string ancienneSelection)
        {
            if (bAModifieStructure)
            {
                DataTable ancienneTable = ICT_ShowUp_table.Copy();
                ICT_ShowUp_table.Clear();
                for (int i = 0; i < mtb_Values.Length; i++)
                {
                    DataRow newLigne = ICT_ShowUp_table.NewRow();
                    newLigne[0] = FonctionsType.getInt(lbls_Nb[i].Text);
                    for (int j = 1; j < ICT_ShowUp_table.Columns.Count; j++)
                    {
                        if (ancienneTable.Rows.Count > i)
                            newLigne[j] = ancienneTable.Rows[i].ItemArray[j];
                        else
                            newLigne[j] = 0;
                    }
                    ICT_ShowUp_table.Rows.Add(newLigne);
                }
                bAModifieStructure = false;
            }
            int iIndexColonne = ICT_ShowUp_table.Columns.IndexOf(ancienneSelection);
            for (int i = 0; i < mtb_Values.Length; i++)
            {
                ICT_ShowUp_table.Rows[i].BeginEdit();
                ICT_ShowUp_table.Rows[i][iIndexColonne] = Math.Round(ParsePourcentage(mtb_Values[i].Text) /*/ 100*/, 2);
            }
            ICT_ShowUp_table.AcceptChanges();

        }
        #endregion

    }
}