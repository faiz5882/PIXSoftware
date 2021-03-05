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
    public partial class Transfert_Distri_Assistant : Form
    {

        #region Les différents constructeurs et fonctions d'initialisation de la classe.
        //Pour la gestion des fichiers resssource
        private ResourceManager manager;
        //Pour les messages à afficher dans les MessageBox
        private string[] messages;

        Label[] lbls_Nb;
        TextBox[] mtb_Values;
        bool bAModifieValeurs;
        bool bAModifieStructure;
        const int startXFirst = 65;
        const int startXThird = 202;
        const int startY = 10;
        const int IncrementY = 20;
        String ancienneSelection;
        DataTable table;

        String tableEltType;


        /// <summary>
        /// Nombre de ligne dans la table a modifier, donc dans la liste de textBox
        /// </summary>
        private int iRowNumber;

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
            // manager = new ResourceManager("SIMCORE_TOOL.Assistant.Nb_Bags_Visitors_Assistant", Assembly.GetExecutingAssembly());
            manager = new ResourceManager("SIMCORE_TOOL.Assistant.Transfert_Distri_Assistant", Assembly.GetExecutingAssembly());
            //////////////////////////////
            //Initialisation des messages
            /////////////////////////////
            messages = new string[4];
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = manager.GetString("message" + i);
            }
            //message0 = "Impossible to find the selected flight category";
            //message1 = "The sum must be 100. Please modify your data.";
            //message2 = "Do you want to save the changes to the Flight category ";
            //message3 = " table ?";

            InitializeComponent();

            // set texts
            if (table_.TableName == GlobalNames.Transfer_FlightCategoryDitributionTableName)
            {
                tableEltType = "flight category";
            }
            else if (table_.TableName == GlobalNames.Transfer_TerminalDitributionTableName)
            {
                tableEltType = "terminal";
            }
            label1.Text = "From " + tableEltType + ":";
            label6.Text = "To " + tableEltType + ":";

            OverallTools.FonctionUtiles.MajBackground(this);
            table = table_;

            this.Text = "Modify " + table.TableName;
            lbls_Nb = new Label[0];
            mtb_Values = new TextBox[0];

            ancienneSelection = "";
            bAModifieTable = true;
            bAModifieValeurs = true;
            bAModifieStructure = true;

            SetRows();

            bAModifieTable = false;
            bAModifieValeurs = false;
            bAModifieStructure = false;
        }

        public Transfert_Distri_Assistant(DataTable table)
        {
            Initialize(table);
            cbMainComboBox.SelectedIndex = 0;
        }

        public Transfert_Distri_Assistant(DataTable table, String SelectedItem)
        {
            Initialize(table);
            cbMainComboBox.SelectedItem = SelectedItem;
        }
        #endregion

        #region Fonction pour l'initialization des lignes.
        private void SetRows()
        {
            bAModifieStructure = true;

            // Initialise la combo box avec la liste des entetes de colonne
            // ainsi que les labels de la liste
            iRowNumber = table.Rows.Count;
            if (iRowNumber > 0)
            {
                lbls_Nb = new Label[iRowNumber];
                for (int i = 0; i < iRowNumber; i++)
                {
                    String name = table.Rows[i][0].ToString();
                    cbMainComboBox.Items.Add(name);
                    lbls_Nb[i] = createLabel(i);
                    lbls_Nb[i].AutoSize = true;
                    lbls_Nb[i].Text = name;
                    lbls_Nb[i].Refresh();
                }
            }
            else
                return;

            // Initialise les ComboBox avec les pourcentages de la table
            mtb_Values = new TextBox[iRowNumber];
            for (int i = 0; i < iRowNumber; i++)
            {
                mtb_Values[i] = createTextBox(i);
                mtb_Values[i].Anchor = AnchorStyles.Left;
                mtb_Values[i].Anchor = AnchorStyles.Bottom;
            }
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

        #region Fonction pour la vérification de la somme
        private void CheckSum(object sender, EventArgs e)
        {
            bAModifieValeurs = true;
            double value = 0;
            //Double mean = 0;
            Double dTmp;
            for (int i = 0; i < mtb_Values.Length; i++)
            {
                dTmp = ParsePourcentage(mtb_Values[i].Text);
                if (dTmp != -1)
                {
                    value += dTmp;
                    //mean += dTmp * i;
                    mtb_Values[i].BackColor = Color.White;
                }
                else
                {

                    mtb_Values[i].BackColor = Color.Red;
                }
            }
            //if (mtb_Values.Length > 0)
                //mean = mean / (100);
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
            else if (value > 100)
            {
                foreach (TextBox mtb in mtb_Values)
                {
                    mtb.BackColor = Color.Red;
                }
            }

            lbl_Sum.Text = manager.GetString("lbl_Sum.Text") + Math.Round(value, 2).ToString();
        }
        #endregion

        #region Fonctions pour la gestion des différentes intéractions avec la liste déroulante des FC
        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ancienneSelection == cbMainComboBox.Text)
                return;
            BT_OK_Click(null, null);
            if (ancienneSelection == cbMainComboBox.Text)
                return;
            String msg = messages[0] + tableEltType;
            if (!table.Columns.Contains(cbMainComboBox.Text))
            {
                MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int iIndexLigne = OverallTools.DataFunctions.indexLigne(table, 0, cbMainComboBox.Text);
            if (iIndexLigne == -1)
            {
                MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = 0; i < table.Columns.Count-1; i++)
            {
                mtb_Values[i].Text = ConvertToPercent(table.Rows[iIndexLigne].ItemArray[i+1].ToString());
            }
            ancienneSelection = cbMainComboBox.Text;
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
                    MessageBox.Show("The sum must be 100.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbMainComboBox.Text = ancienneSelection;
                    if (e != null)
                        DialogResult = DialogResult.None;
                    return;
                }
                if ((ancienneSelection != cbMainComboBox.Text) || (DialogResult == DialogResult.OK))
                {
                    //String msg = messages[2] + DataManagement.DataManagerInput.ConvertToFullName(table.TableName) + messages[3];
                    String msg = "Do you want to save the changes for " + DataManagement.DataManagerInput.ConvertToFullName(table.TableName) + " ?";
                    DialogResult drSave = MessageBox.Show(msg, "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        cbMainComboBox.Text = ancienneSelection;
                        if (e != null)
                            DialogResult = DialogResult.None;
                    }
                    else if (drSave == DialogResult.No)
                    {
                        if (e != null)
                            DialogResult = DialogResult.None;
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        SaveTransfertTable(ancienneSelection);
                    }
                }
            }
        }

        private void SaveTransfertTable(string ancienneSelection)
        {
            if (bAModifieStructure)
            {
                DataTable ancienneTable = table.Copy();
                table.Clear();
                for (int i = 0; i < mtb_Values.Length; i++)
                {
                    DataRow newLigne = table.NewRow();
                    newLigne[0] = FonctionsType.getInt(lbls_Nb[i].Text);
                    for (int j = 1; j < table.Columns.Count; j++)
                    {
                        if (ancienneTable.Rows.Count > i)
                            newLigne[j] = ancienneTable.Rows[i].ItemArray[j];
                        else
                            newLigne[j] = 0;
                    }
                    table.Rows.Add(newLigne);
                }
                bAModifieStructure = false;
            }
            int iIndexLine = OverallTools.DataFunctions.indexLigne(table, 0, ancienneSelection);
            //for (int i = 0; i < table.Rows.Count; i++) //table.Columns.IndexOf(ancienneSelection);
            //    if (table.Rows[i].ItemArray[0].ToString() == ancienneSelection)
            //        iIndexLine = i;
            if (iIndexLine == -1)
            {
                MessageBox.Show(messages[0], "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < mtb_Values.Length; i++)
            {
                table.Rows[iIndexLine].BeginEdit();
                table.Rows[iIndexLine][i+1] = Math.Round(ParsePourcentage(mtb_Values[i].Text) /*/ 100*/, 2);
            }
            table.AcceptChanges();

        }
        #endregion
    }
}