using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;


namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Assistant_Creation_Filtres : Form
    {
        #region Les différentes variables de la classe.

        private const String sNewExpression = "New expression";

        private ArrayList listOperationType; //Les différentes opérations sur les champs

        private ArrayList lesFormules; // Les différentes formules pour calculer la colonne spécifiée.

        private ArrayList listConditions; //Les conditions à respecter

        private ArrayList listDisplayed;//Les informations concernant les column à afficher.

        private ArrayList lesNomsDesColonnes;

        private DataTable laTable; //La table d'origine afin de pouvoir accéder aux noms des colonnes


        private bool copyTable;

        private String dataset;

        private GestionDonneesHUB2SIM lesDonnees; // Pour pouvoir accéder, lors de la création d 'un nouveau filtre,
        //aux noms des tables déjà présentes dans la base de données.

        private int CurrentColumn;

        private bool TreatColumn = true;

        private bool lastFocused = true; //true ==> Expression, false  ==> Condition
        #endregion

        #region les différents constructeurs de la classe.
        private void InitialiserFenetre(Filter filtre, DataTable table_, String dataset_, GestionDonneesHUB2SIM lesDonnees_)
        {
            InitializeComponent();
            txt_ColumnName.BackColor = Color.White;
            txt_Condition.BackColor = Color.White;
            txt_FilterName.BackColor = Color.White;
            txt_formule.BackColor = Color.White;
            laTable = table_;
            dataset = dataset_;
            lesDonnees = lesDonnees_;
            txt_FilterName.Text = "";
            CurrentColumn = -1;
            //Définition du fond d'écran
            OverallTools.FonctionUtiles.MajBackground(this);


            foreach (DataColumn colonne in table_.Columns)
            {
                cb_TableColumns.Items.Add(colonne.ColumnName);
            }
            foreach (String operateur in Filter.ListOperateurs)
            {
                cb_Functions.Items.Add(operateur);
            }
            foreach (String operation in Filter.ListOperation)
            {
                cb_OperationType.Items.Add(operation);
            }
            cb_CopyDatas.Enabled = !PAX2SIM.bTrialVersion;

            if (filtre != null)
            {
                cb_CopyDatas.Checked = filtre.copyTable && cb_CopyDatas.Enabled;
                copyTable = cb_CopyDatas.Checked;
                lesFormules = (ArrayList)filtre.Formules.Clone();
                listConditions = (ArrayList)filtre.Conditions.Clone();
                listOperationType = (ArrayList)filtre.OperationType.Clone();
                listDisplayed = (ArrayList)filtre.Display.Clone();
                lesNomsDesColonnes = (ArrayList)filtre.ColumnsNames.Clone();
                for (int i = 0; i < lesNomsDesColonnes.Count; i++)
                {
                    clb_ColumnsToShow.Items.Add(lesNomsDesColonnes[i]);
                }
                txt_FilterName.Text = filtre.Name;
            }
            else
            {
                //Initialisation des arraylist.
                lesFormules = new ArrayList();
                listConditions = new ArrayList();
                listOperationType = new ArrayList();
                listDisplayed = new ArrayList();
                lesNomsDesColonnes = new ArrayList();
                copyTable = false;
                // Initialisation de la liste des noms de colonnes.
                for (int i = 0; i < laTable.Columns.Count; i++)
                {
                    lesNomsDesColonnes.Add(laTable.Columns[i].ColumnName);
                    clb_ColumnsToShow.Items.Add(laTable.Columns[i].ColumnName);
                    lesFormules.Add("[" + laTable.Columns[i].ColumnName + "]");
                    listConditions.Add("");
                    listDisplayed.Add(true);
                    listOperationType.Add(Filter.ListOperation[0]);
                }
            }
            if (txt_FilterName.Text == "")
            {
                txt_FilterName.Text = laTable.TableName;
            }
            else
            { // On bloque l'édition du nom du filtre si il était renseigné, (soit parce que c'est un filtre non
              // nul, auquel cas on ne peut pas changer le nom du filtre. Soit parce que l'on est dans la création
              // d'un mode de visualisation et on ne peut pas non plus changer le nom.
                txt_FilterName.Enabled = false;
                lbl_FilterName.Enabled = false;
            }

            mettreAJourBoutonsActifs();
            ActiveDesactiveColonneGroupe(false);
            VerifContenuListe();
            cb_CopyDatas.Enabled = (dataset != null) && (dataset != "");
        }
        internal SIM_Assistant_Creation_Filtres(Filter filtre, DataTable table_, String dataset_, GestionDonneesHUB2SIM lesDonnees_)
        {
            InitialiserFenetre(filtre, table_,dataset_,lesDonnees_);
            if (filtre == null)
            {
                //On change le nom du filtre.
                txt_FilterName.Text += "_";
                int i = 1;
                while (lesDonnees.tableEstPresente(dataset, txt_FilterName.Text + i.ToString()))
                {
                    i++;
                }
                txt_FilterName.Text += i.ToString();
            }
        }

        private void mettreAJourBoutonsActifs()
        {
            btn_Remove.Enabled = (clb_ColumnsToShow.SelectedIndex != -1);
            btn_RemoveAll.Enabled = (clb_ColumnsToShow.Items.Count != 0);
        }
        #endregion

        #region Gestion des différents boutons de la fenêtre.
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (clb_ColumnsToShow.Items.Count == 0)
            {
                MessageBox.Show("You must select at least one column");
                DialogResult = DialogResult.None;
            }
            else if (CurrentColumn != -1)
            {
                if (!valideDonnees())
                {
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            int i;
            for (i = 0; i < listDisplayed.Count; i++)
            {
                if ((bool)listDisplayed[i])
                    break;
            }
            if (i == listDisplayed.Count)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("You must display at least one column");
                return;
            }
            if (copyTable)
            {
                if (MessageBox.Show("Are you sure that you want to apply this filter ?\n\nData will be updated. Old data will be lost.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            if ((txt_FilterName.Enabled) && (lesDonnees.tableEstPresente(dataset, txt_FilterName.Text)))
            {
                MessageBox.Show("The filter name is already in use.");
                DialogResult = DialogResult.None;
                txt_FilterName.Focus();
                return;
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (CurrentColumn == -1)
                return;
            lesFormules.RemoveAt(CurrentColumn);
            listConditions.RemoveAt(CurrentColumn);
            listOperationType.RemoveAt(CurrentColumn);
            listDisplayed.RemoveAt(CurrentColumn);
            lesNomsDesColonnes.RemoveAt(CurrentColumn);
            CurrentColumn = -1;
            SupprimerElementCourant(ref clb_ColumnsToShow);
            if(CurrentColumn == -1)
                gb_Column.Enabled = false;
            VerifContenuListe();
            mettreAJourBoutonsActifs();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if ((CurrentColumn != -1) && (!valideDonnees()))
                return;
            String nouvelleColonne = "Column_";
            int i = 1;
            while (lesNomsDesColonnes.IndexOf( nouvelleColonne + i.ToString()) != -1)
            {
                i++;
            }
            lesFormules.Add(sNewExpression);
            listOperationType.Add(Filter.ListOperation[0]);
            listConditions.Add("");
            listDisplayed.Add(true);
            clb_ColumnsToShow.Items.Add(nouvelleColonne + i.ToString());
            lesNomsDesColonnes.Add(nouvelleColonne + i.ToString());
            clb_ColumnsToShow.SelectedIndex = clb_ColumnsToShow.Items.Count - 1;
            txt_ColumnName.Focus();
            VerifContenuListe();
        }

        private void btn_RemoveAll_Click(object sender, EventArgs e)
        {
            clb_ColumnsToShow.Items.Clear();
            lesNomsDesColonnes.Clear();
            lesFormules.Clear();
            listOperationType.Clear();
            listConditions.Clear();
            listDisplayed.Clear();
            gb_Column.Enabled = false;
            CurrentColumn = -1;
            mettreAJourBoutonsActifs();
        }
        #endregion

        #region les différentes fonctions qui s'occupent de gérer les listes de noms de colonne.

        private void VerifContenuListe()
        {

            for (int i = 0; i < lesNomsDesColonnes.Count; i++)
            {
                if ((lesFormules[i].ToString() != ("[" + lesNomsDesColonnes[i].ToString() + "]")) ||
                (listConditions[i].ToString() != "") ||
                (!(bool)listDisplayed[i]) ||
                (listOperationType[i].ToString() != Filter.ListOperation[0]))
                {
                    //clb_ColumnsToShow.Items[i] = lesNomsDesColonnes[i].ToString() + "*";
                }
            }
        }

        private void LST_ListeSeries_KeyDown(object sender, KeyEventArgs e)
        {
            //Fonction qui détecte les touches préssées lorsque les listes ont le focus.
            if (e.KeyCode == Keys.Delete)
            {
                btn_Remove_Click(sender, e);
            }
        }

        private void Listes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!TreatColumn)
                return;
            if ((clb_ColumnsToShow.SelectedIndex == CurrentColumn))
                return;
            if ((CurrentColumn == -1) || (valideDonnees()))
            {
                mettreAJourContenuColonne();
                mettreAJourBoutonsActifs();
            }
            VerifContenuListe();
        }
        #endregion

        #region Les différentes fonctions pour les vérifications sur le formulaire.

        private bool valideDonnees()
        {
            String tmp;
            tmp = txt_ColumnName.Text;
            listOperationType[CurrentColumn] = cb_OperationType.Text;
            listConditions[CurrentColumn] = txt_Condition.Text;
            lesFormules[CurrentColumn] = txt_formule.Text;
            listDisplayed[CurrentColumn] = cb_Display.Checked;
            Filter.ListeErreurs.Clear();
            rtb_Error.Text = "";
            ArrayList alErrors = null;
            if (!Filter.estFormuleValide(txt_formule.Text,txt_Condition.Text, laTable))
            {
                alErrors = Filter.ListeErreurs;
                foreach (string sError in alErrors)
                {
                    rtb_Error.Text += sError + "\n";
                }
                //La formule n'est pas valide, on affiche un message d'erreur
                MessageBox.Show("The expression is not valid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                clb_ColumnsToShow.Items[CurrentColumn] = tmp;
                lesNomsDesColonnes[CurrentColumn] = tmp;
                clb_ColumnsToShow.SelectedIndex = CurrentColumn;
                txt_formule.Focus();
                return false;
            }
            alErrors = Filter.ListeErreurs;
            if (alErrors.Count > 0)
            {
                foreach (string sError in alErrors)
                {
                    rtb_Error.Text += sError + "\n";
                }
            }
            //On vérifie que le nouveau nom pour la colonne n'est pas déjà présent dans la liste.
            for (int i = 0; i < lesNomsDesColonnes.Count; i++)
            {
                if (i != CurrentColumn)
                {
                    if (lesNomsDesColonnes[i].ToString() == txt_ColumnName.Text)
                    {
                        clb_ColumnsToShow.SelectedIndex = CurrentColumn;
                        MessageBox.Show("The column name is in use, please choose another one.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        txt_ColumnName.Focus();
                        clb_ColumnsToShow.Items[CurrentColumn] = tmp;
                        lesNomsDesColonnes[CurrentColumn] = tmp;
                        return false;
                    }
                }
            }
            clb_ColumnsToShow.Items[CurrentColumn] = tmp;
            lesNomsDesColonnes[CurrentColumn] = tmp;
            return true;
        }

        private void mettreAJourContenuColonne()
        {
            CurrentColumn = clb_ColumnsToShow.SelectedIndex;
            gb_Column.Text = "Column";
            if (CurrentColumn == -1)
            {
                ActiveDesactiveColonneGroupe(false);
            }
            else
            {
                ActiveDesactiveColonneGroupe(true);
                gb_Column.Text += " " + lesNomsDesColonnes[CurrentColumn].ToString();
                txt_ColumnName.Text = lesNomsDesColonnes[CurrentColumn].ToString();
                txt_formule.Text = lesFormules[CurrentColumn].ToString();
                txt_Condition.Text = listConditions[CurrentColumn].ToString();
                cb_Display.Checked = (bool)listDisplayed[CurrentColumn];
                cb_OperationType.Text = listOperationType[CurrentColumn].ToString();
            }
        }

        private void ActiveDesactiveColonneGroupe(bool state)
        {
            gb_Column.Enabled = state;
        }

        private void LST_ListeSeries_MouseDown(object sender, MouseEventArgs e)
        {
            int index = clb_ColumnsToShow.IndexFromPoint(e.Location);
            if (index != -1)
            {
                clb_ColumnsToShow.SelectedIndex = index;
            }
        }
        #endregion

        #region Fonction pour la gestion de la zone de texte.
        private void txtFilterName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(((e.KeyChar >= 'a') && (e.KeyChar <= 'z')) ||
                ((e.KeyChar >= 'A') && (e.KeyChar <= 'Z')) ||
                ((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (
                (e.KeyChar == '_') || (e.KeyChar == ' ') || (e.KeyChar == '\b') || (e.KeyChar == '.'))))
            {
                //Si le caractère tapé n'est pas dans l'alphabet ou n'est pas un chiffre.
                e.KeyChar = '\0';
            }
        }
        #endregion

        #region Fonction pour récupérer le filtre créer dans la fenêtre.
        internal Filter getFilter()
        {/*
            ArrayList listColonnes = new ArrayList();
            for (int i = 0; i < clb_ColumnsToShow.Items.Count; i++)
            {
                listColonnes.Add(lesNomsDesColonnes[i].ToString());
            }*/
            return new Filter(txt_FilterName.Text, laTable.TableName, lesNomsDesColonnes, lesFormules, listDisplayed, listConditions, listOperationType, cb_CopyDatas.Checked,true);
//            return new Filter(listColonnes, lesFormules,listDisplayed, txt_FilterName.Text, laTable.TableName, cb_CopyDatas.Checked);
        }

        #endregion

        #region AutresFonction utilisées dans la fenetre.
        private void cb_TableColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_formule_Click(sender, e);
            if (lastFocused)
                //txt_formule.Text += "[" + ((ComboBox)sender).Text + "]";
                AddLetter(txt_formule, "[" + ((ComboBox)sender).Text + "]");
            else
                //txt_Condition.Text += "[" + ((ComboBox)sender).Text + "]";
                AddLetter(txt_Condition, "[" + ((ComboBox)sender).Text + "]");
        }

        private void cb_Functions_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_formule_Click(sender, e);
            if (lastFocused)
                //txt_formule.Text += ((ComboBox)sender).Text;
                AddLetter(txt_formule, ((ComboBox)sender).Text);
            else
                //txt_Condition.Text += ((ComboBox)sender).Text;
                AddLetter(txt_Condition, ((ComboBox)sender).Text);
            //txt_formule.Text +=((ComboBox)sender).Text;
        }

        private void SupprimerElementCourant(ref ListBox liste)
        {
            //Fonction qui se charge de supprimer dans une liste, tout en gardant un élément sélectionné dans
            // cette liste.
            int iIndex = liste.SelectedIndex;
            liste.Items.RemoveAt(iIndex);
            if (iIndex > liste.Items.Count - 1)
            {
                iIndex--;
            }
            if (liste.Items.Count != 0)
                liste.SelectedIndex = iIndex;

        }

        //Fonction pour supprimer le "new Expression" de la zone de la formule.
        private void txt_formule_Click(object sender, EventArgs e)
        {
            if (txt_formule.Text == sNewExpression)
            {
                txt_formule.Text = "";
            }
        }
        #endregion

        private void txt_ColumnName_Leave(object sender, EventArgs e)
        {
            String tmp = txt_ColumnName.Text;
            //On vérifie que le nouveau nom pour la colonne n'est pas déjà présent dans la liste.
            for (int i = 0; i < lesNomsDesColonnes.Count; i++)
            {
                if (i != CurrentColumn)
                {
                    if (lesNomsDesColonnes[i].ToString() == txt_ColumnName.Text)
                    {
                        clb_ColumnsToShow.SelectedIndex = CurrentColumn;
                        MessageBox.Show("The column name is in use, please choose another one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txt_ColumnName.Focus();
                        return;
                    }
                }
            }
            TreatColumn = false;
            clb_ColumnsToShow.Items[CurrentColumn] = tmp;

            TreatColumn = true;
            lesNomsDesColonnes[CurrentColumn] = tmp;
        }

        private void txt_formule_Enter(object sender, EventArgs e)
        {
            lastFocused = (((TextBox)sender) == txt_formule);
        }

        private void rtb_Error_TextChanged(object sender, EventArgs e)
        {
            int height = this.Size.Height;

            if (rtb_Error.Text == "")
                this.Size = new Size(449, height);
            else
                this.Size = new Size(763, height);
        }

        private void AddLetter(TextBox txt_Value, String cLettre)
        {

            if (txt_Value.SelectionLength != 0)
            {
                String sValue = txt_Value.Text.Substring(0, txt_Value.SelectionStart);
                int iSelection = txt_Value.SelectionStart;
                sValue += cLettre + txt_Value.Text.Substring(txt_Value.SelectionStart + txt_Value.SelectionLength);

                txt_Value.Text = sValue;
                txt_Value.SelectionStart = iSelection + cLettre.Length;
                txt_Value.SelectionLength = 0;
            }
            else if (txt_Value.SelectionStart != -1)
            {
                int iSelection = txt_Value.SelectionStart;
                txt_Value.Text = txt_Value.Text.Insert(txt_Value.SelectionStart, cLettre);
                txt_Value.SelectionStart = iSelection + cLettre.Length;
                txt_Value.SelectionLength = 0;
            }
            else
            {
                txt_Value.Text += cLettre;
            }
            txt_Value.Focus();
        }
    }
}