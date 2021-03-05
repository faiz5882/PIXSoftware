using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class ExceptionEditorLine : Form
    {
        DataTable dtTable_;
        DataTable dtMotherTable_;
        bool bColumnExceptions_;
        String sAddedColumn;
        DataManagement.ExceptionTable.ExceptionTableParameters etpParameter_;
        ComboBox cbList = null;
        Label lblCategory = null;
        Dictionary<String, List<String>> SelectedElements = new Dictionary<String, List<String>>();
        List<String> lstElements = new List<string>();

        internal ExceptionEditorLine(DataTable dtTable,
            String sExceptionType,
            DataTable dtFPDTable,
            DataTable dtFPATable,
            DataTable dtFlightCategorie,
            DataTable dtAirline,
            DataTable dtParentTable,
            DataManagement.ExceptionTable.ExceptionTableParameters etpParameter,
            bool bColumnExceptions,
            bool bDepartureOnly,
            bool bArrivalOnly)
        {
            initialize();
            dtTable_ = dtTable;
            etpParameter_ = etpParameter;
            dtMotherTable_ = dtParentTable;
            bColumnExceptions_ = bColumnExceptions;
            sAddedColumn = "";
            InitializeAvailableInformations(lst_Visible, null, cbList, null, null, bColumnExceptions, "");

            AddComboBox(sExceptionType);

            if (sExceptionType.StartsWith(GlobalNames.FirstAndBusiness))
            {
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                {
                    sAddedColumn = GlobalNames.Flight;
                    if ((bArrivalOnly) || (bArrivalOnly == bDepartureOnly))
                        InitializeAvailableInformations(
                            lst_Available, lst_Visible, cbList,
                            dtFPATable, dtParentTable, false, "A_");
                    if ((bDepartureOnly) || (bArrivalOnly == bDepartureOnly))
                        InitializeAvailableInformations(
                            lst_Available, lst_Visible, cbList,
                            dtFPDTable, dtParentTable, false, "D_");
                }
                else if (sExceptionType.EndsWith(GlobalNames.Airline))
                {
                    sAddedColumn = GlobalNames.Airline;
                    InitializeAvailableInformations(
                        lst_Available, lst_Visible, cbList,
                        dtAirline, dtParentTable, false, "");
                }
                else if (sExceptionType.EndsWith(GlobalNames.FlightCategory))
                {
                    sAddedColumn = GlobalNames.FlightCategory;
                    InitializeAvailableInformations(
                        lst_Available, lst_Visible, cbList,
                        dtFlightCategorie, dtParentTable, false, "");
                }
                else
                {
                    InitializeAvailableInformations(
                        lst_Available, lst_Visible, cbList,
                        dtParentTable, dtParentTable, bColumnExceptions, "");
                }
            }
            else
            {
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                {
                    sAddedColumn = GlobalNames.Flight;
                    if ((bArrivalOnly) || (bArrivalOnly == bDepartureOnly))
                        InitializeAvailableInformations(
                            lst_Available, lst_Visible, cbList,
                            dtFPATable, dtParentTable, false, "A_");
                    if ((bDepartureOnly) || (bArrivalOnly == bDepartureOnly))
                        InitializeAvailableInformations(
                            lst_Available, lst_Visible, cbList,
                            dtFPDTable, dtParentTable, false, "D_");
                }
                else if (sExceptionType.EndsWith(GlobalNames.Airline))
                {
                    sAddedColumn = GlobalNames.Airline;
                    InitializeAvailableInformations(
                        lst_Available, lst_Visible, cbList,
                        dtAirline, dtParentTable, false, "");
                }
                else if (sExceptionType.EndsWith(GlobalNames.FlightCategory))
                {
                    sAddedColumn = GlobalNames.FlightCategory;
                    InitializeAvailableInformations(
                        lst_Available, lst_Visible, cbList,
                        dtFlightCategorie, dtParentTable, false, "");
                }
            }
        }

        private void initialize()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        /// <summary>
        /// Accesseur qui ce charge de contruire la table d'exception
        /// </summary>
        internal DataTable Table
        {
            get
            {
                if (dtTable_ == null)
                {
                    dtTable_ = new DataTable(dtMotherTable_.TableName);

                    if (bColumnExceptions_)
                    {
                        dtTable_ = dtMotherTable_.Copy();
                    }
                    else
                    {
                        if (sAddedColumn != "")
                            dtTable_.Columns.Add(sAddedColumn, typeof(String));

                        foreach (DataColumn dcColumn in dtMotherTable_.Columns)
                        {
                            dtTable_.Columns.Add(dcColumn.ColumnName, dcColumn.DataType);
                        }
                        if (dtTable_.Columns.Count > dtMotherTable_.Columns.Count)
                            dtTable_.PrimaryKey = new DataColumn[] { dtTable_.Columns[0], dtTable_.Columns[1] };
                        else
                            dtTable_.PrimaryKey = new DataColumn[] { dtTable_.Columns[0] };
                    }
                }
                if (bColumnExceptions_)
                {
                    int iNumber = 0;
                    List<String> lsColumnsToRemove = new List<string>();
                    List<Double> ldDefaultValues = new List<double>();

                    foreach (DataColumn dcColumn in dtTable_.Columns)
                    {
                        if (OverallTools.FonctionUtiles.estPresentDansListe(dcColumn.ColumnName, GestionDonneesHUB2SIM.ListeEnteteDiv))
                        {
                            iNumber++;
                            continue;
                        }
                        // >> Bug #19986 PAX2SIM - Exception editor
                        //                        if (lst_Visible.Items.Contains(dcColumn.ColumnName))
                        //                            continue;
                        if (ExceptionEditor.columnHasException(dcColumn.ColumnName, lst_Visible))
                            continue;
                        // << Bug #19986 PAX2SIM - Exception editor

                        if (ldDefaultValues.Count == 0)
                        {
                            for (int i = 0; i < dtTable_.Rows.Count; i++)
                            {
                                String sValue = dtTable_.Rows[i][dcColumn].ToString();
                                Double dValue;
                                if (!Double.TryParse(sValue, out dValue))
                                    continue;
                                ldDefaultValues.Add(dValue);
                            }
                        }
                        lsColumnsToRemove.Add(dcColumn.ColumnName);
                    }
                    foreach (String sColumnName in lsColumnsToRemove)
                        dtTable_.Columns.Remove(sColumnName);
                    for (int i = 0; i < lst_Visible.Items.Count; i++)
                    {
                        if (dtTable_.Columns.Contains(lst_Visible.Items[i].ToString()))
                            continue;
                        dtTable_.Columns.Add(lst_Visible.Items[i].ToString(), typeof(Double));
                        if (ldDefaultValues.Count != 0)
                        {
                            for (int j = 0; j < ldDefaultValues.Count; j++)
                            {
                                dtTable_.Rows[j][lst_Visible.Items[i].ToString()] = ldDefaultValues[j];
                            }
                        }
                    }
                    OverallTools.DataFunctions.SortColumns(dtTable_, iNumber);
                }
                else
                {
                    List<String> lsAlreadyPresent = new List<string>();
                    for (int i = 0; i < dtTable_.Rows.Count; i++)
                    {
                        bool present = true;
                        List<String> lstElt = null;
                        if (SelectedElements.ContainsKey(dtTable_.Rows[i][0].ToString()))
                        {
                            lstElt = SelectedElements[dtTable_.Rows[i][0].ToString()];
                            if (!(lstElt == null || lstElt.Contains(dtTable_.Rows[i][1].ToString())))
                                present = false;
                        }
                        else
                            present = false;
                        if (!present)
                        {
                            dtTable_.Rows.RemoveAt(i);
                            i--;
                            continue;
                        }
                        lsAlreadyPresent.Add(dtTable_.Rows[i][0].ToString() + dtTable_.Rows[i][1].ToString());
                    }
                    int iOffset = dtTable_.Columns.Count - dtMotherTable_.Columns.Count;


                    foreach (String exKey in SelectedElements.Keys)
                    {
                        List<String> lstItems = SelectedElements[exKey];
                        foreach (String item in lstItems)
                        {
                            if (lsAlreadyPresent.Contains(exKey + item))
                                continue;
                            foreach (DataRow drMother in dtMotherTable_.Rows)
                            {
                                if (item == drMother[0].ToString())
                                {
                                    DataRow drRow = dtTable_.NewRow();
                                    if (iOffset == 1)
                                    {
                                        if (exKey.Contains(" ("))
                                        {
                                            String exKeyWithoutDescription = exKey.Substring(0, exKey.IndexOf(" ("));
                                            drRow[0] = exKeyWithoutDescription;
                                        }
                                        else
                                        {
                                            drRow[0] = exKey;
                                        }
                                    }
                                    for (int j = 0; j < dtMotherTable_.Columns.Count; j++)
                                    {
                                        drRow[j + iOffset] = drMother[j];
                                    }
                                    dtTable_.Rows.Add(drRow);
                                }
                            }
                            if (iOffset == 0)
                                break;
                        }
                    }
                }
                return dtTable_;
            }
        }

        #region Initialisation du contenu
        private void InitializeAvailableInformations(ListBox lstToFill,
            ListBox lstSecondList,
            ComboBox cbExElts,
            DataTable dtOriginTable,
            DataTable dtRefeTable,
            bool bColumnExceptions,
            String sPrefixe)
        {
            String str = "";
            lstToFill.Sorted = true;
            if (dtOriginTable == null)
                return;

            if (bColumnExceptions)
            {
                foreach (DataColumn dcColumn in dtOriginTable.Columns)
                {
                    if (OverallTools.FonctionUtiles.estPresentDansListe(dcColumn.ColumnName, GestionDonneesHUB2SIM.ListeEnteteDiv))
                        continue;
                    if ((lstSecondList != null) && (lstSecondList.Items.Contains(sPrefixe + dcColumn.ColumnName)))
                        continue;
                    lstToFill.Items.Add(sPrefixe + dcColumn.ColumnName);
                }
            }
            else
            {
                // Mise à jour du ComboBox
                foreach (DataRow dcRow in dtOriginTable.Rows)
                {
                    String description = String.Empty;
                    if (dtOriginTable.Columns.Count >= 8)
                    {
                        if (dcRow[3] != null)
                            description += " (" + dcRow[3].ToString() + ", ";
                        else
                            description += " (";
                        if (dcRow[4] != null)
                            description += dcRow[4].ToString() + ", ";
                        if (dcRow[5] != null)
                            description += dcRow[5].ToString() + ", ";
                        if (dcRow[7] != null)
                            description += dcRow[7].ToString() + ")";
                        else
                            description += ")";
                    }
                    // >> Task T-20 Pax2Sim - Airline Exception editor improvement
                    if (dtOriginTable.Columns.Count == 3)
                    {
                        if (dcRow[1] != null && dcRow[1].ToString() != "")
                            description += " (" + dcRow[1].ToString();
                        if (dcRow[2] != null && dcRow[2].ToString() != "")
                            description += ", " + dcRow[2].ToString() + " )";
                        else
                            description += dcRow[2].ToString() + " )";
                    }
                    str = sPrefixe + dcRow[0].ToString() + description;
                    cbExElts.Items.Add(str);
                }

                // mise à jour de la liste des elements qui peuvent être ajouté
                // >> Task T-20 ValueCoders - Airline Exception editor improvement
                foreach (DataRow drRow in dtRefeTable.Rows)
                {
                    String flightDescription = "";
                    if (drRow[1] != null && drRow[1].ToString() != "")
                    {
                        flightDescription = drRow[1].ToString();
                        String listDescription = drRow[0].ToString() + " (" + flightDescription + ")";
                        lstElements.Add(listDescription);
                    }
                    else
                    {
                        lstElements.Add(drRow[0].ToString());
                    }
                }

                // mise à jour de la liste des élements ajouté pour chaque exception
                SelectedElements.Clear();
                if (dtTable_ != null)
                    foreach (DataRow drRow in dtTable_.Rows)
                    {
                        String exKey = drRow[0].ToString();
                        if (!SelectedElements.ContainsKey(exKey))
                            SelectedElements.Add(exKey, new List<string>());
                        SelectedElements[exKey].Add(drRow[1].ToString());
                    }

            }

            // selection du premier item du ComboBox
            if (cbList != null && cbList.Items.Count > 0)
                cbList.SelectedIndex = 0;
        }
        #endregion

        #region ComboBox
        /// <summary>
        /// Fonction pour ajouter le ComboBox de choix du choix de la cas d'exception.
        /// </summary>
        /// <param name="ComboBoxLabel"></param>
        private void AddComboBox(String ComboBoxLabel)
        {
            // création du ComboBox et du label
            cbList = new ComboBox();
            cbList.Parent = this;
            cbList.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            cbList.SelectedIndexChanged += new EventHandler(ComboBox_IndexChange);
            cbList.DropDownStyle = ComboBoxStyle.DropDownList;
            cbList.Size = new Size(300, 21);
            lblCategory = new Label();
            lblCategory.Text = ComboBoxLabel;
            lblCategory.Size = lblCategory.GetPreferredSize(new Size(0, 0));
            lblCategory.Parent = this;
            lblCategory.BackColor = System.Drawing.Color.Transparent;
            lblCategory.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);

            // Positionnement du ComboBox et du label
            lblCategory.Location = new Point(
                lst_Available.Location.X,
                lst_Available.Location.Y + 4);
            cbList.Location = new Point(
                lst_Available.Location.X + 10 + lblCategory.Size.Width,
                lst_Available.Location.Y);

            // décalage des autres élements
            int deplacement = 30;
            lst_Available.Location = new Point(
                lst_Available.Location.X,
                lst_Available.Location.Y + deplacement);
            lst_Available.Size = new Size(
                lst_Available.Size.Width,
                lst_Available.Size.Height - deplacement);
            lst_Visible.Location = new Point(
                lst_Visible.Location.X,
                lst_Available.Location.Y);
            lst_Visible.Size = new Size(
                lst_Visible.Size.Width,
                lst_Visible.Size.Height - deplacement);
        }

        /// <summary>
        /// Fonction pour actualiser la liste des elements selectionné pour
        /// une exception.
        /// </summary>
        private void ComboBox_IndexChange(object sender, EventArgs e)
        {
            if (sender != cbList)
                return;

            // Init
            lst_Available.Items.Clear();
            lst_Visible.Items.Clear();

            /// On charge brutalement la liste des elements dans la liste des elements disponible
            lst_Available.Items.AddRange(lstElements.ToArray());

            /// Chercher si l'element du combobox existe dans le dico
            String selectedElementKey = cbList.SelectedItem.ToString();
            if (selectedElementKey.Contains(" ("))
                selectedElementKey = selectedElementKey.Substring(0, selectedElementKey.IndexOf(" ("));
            if (SelectedElements.ContainsKey(selectedElementKey))
            {
                /// __ si oui on les ajoutes dans la liste de droite et on les supprimes de la liste des elements disponibles
                List<String> lstSelected = SelectedElements[selectedElementKey];
                foreach (String str in lstSelected)
                {
                    if (lst_Available.Items.Contains(str))
                    {
                        lst_Available.Items.Remove(str);
                        lst_Visible.Items.Add(str);
                    }
                }
            }
        }
        #endregion

        #region Mouvement des Elements
        /// <summary>
        /// Fonction pour déplacer un element de la liste des élements disponnible 
        /// à la liste des elements d'exception.
        /// </summary>
        private void bt_right_Click(object sender, EventArgs e)
        {
            MoveItemTo(lst_Available, lst_Visible, true);
        }

        /// <summary>
        /// Fonction pour déplacer un element de la liste des élements d'exception 
        /// à la liste des elements disponnible.
        /// </summary>
        private void bt_left_Click(object sender, EventArgs e)
        {
            MoveItemTo(lst_Visible, lst_Available, false);
        }

        /// <summary>
        /// Déplacement d'un élement d'une des liste à l'autre.
        /// </summary>
        /// <param name="lstSource">Liste de provenance</param>
        /// <param name="lstDestination">Liste de destination</param>
        /// <param name="add">Indique si l'élement doit être ajouté ou supprimé de
        /// la liste \ref SelectedElements</param>
        private void MoveItemTo(ListBox lstSource, ListBox lstDestination, bool add)
        {
            lstDestination.Sorted = true;
            if (lstSource.SelectedItems.Count == 0)
                return;
            int i;
            System.Collections.ArrayList lstSelected = new System.Collections.ArrayList();
            for (i = 0; i < lstSource.SelectedItems.Count; i++)
            {
                if (bColumnExceptions_)
                {
                    lstDestination.Items.Add(lstSource.SelectedItems[i]);
                    lstSelected.Add(lstSource.SelectedItems[i]);
                }
                else
                {
                    Object sKey = lstSource.SelectedItems[i];
                    // mise à jour des elements affichés
                    lstDestination.Items.Add(sKey);
                    lstSelected.Add(sKey);
                    // mise à jour du dictionnaire des selections
                    String comboBoxKey = cbList.SelectedItem.ToString();
                    if (comboBoxKey.Contains(" ("))
                        comboBoxKey = comboBoxKey.Substring(0, comboBoxKey.IndexOf(" ("));
                    if (add)
                    {
                        if (!SelectedElements.ContainsKey(comboBoxKey))
                            SelectedElements.Add(comboBoxKey, new List<String>());
                        SelectedElements[comboBoxKey].Add(sKey.ToString());
                    }
                    else
                    {
                        SelectedElements[comboBoxKey].Remove(sKey.ToString());
                        if (SelectedElements[comboBoxKey].Count < 1)
                            SelectedElements.Remove(comboBoxKey);
                    }
                }
            }
            int iIndex = lstSource.Items.IndexOf(lstSelected[0]);
            foreach (Object sKey in lstSelected)
            {
                lstSource.Items.Remove(sKey);
            }
            if (lstSource.Items.Count > 0)
            {
                if (lstSource.Items.Count <= iIndex)
                    lstSource.SelectedIndex = lstSource.Items.Count - 1;
                else
                    lstSource.SelectedIndex = iIndex;
            }
        }
        #endregion
    }
}

