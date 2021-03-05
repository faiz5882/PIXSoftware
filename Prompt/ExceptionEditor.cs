using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.Prompt
{
    public partial class ExceptionEditor : Form
    {
        DataTable dtTable_;
        DataTable dtMotherTable_;
        DataTable dtFPA_;
        DataTable dtFPD_;
        DataTable dtAirlines_;// >> Task #10079 Pax2Sim - Exception Editor Search functionality
        bool bColumnExceptions_;
        String sAddedColumn;
        String sExceptionType_;
        DataManagement.ExceptionTable.ExceptionTableParameters etpParameter_;
        GestionDonneesHUB2SIM donnees_;// >> Task #10079 Pax2Sim - Exception Editor Search functionality

        private void initialize()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        internal ExceptionEditor(DataTable dtTable,
            String sExceptionType,
            DataTable dtFPDTable,
            DataTable dtFPATable,
            DataTable dtFlightCategorie,
            DataTable dtAirline,
            DataTable dtParentTable,
            DataManagement.ExceptionTable.ExceptionTableParameters etpParameter,
            bool bColumnExceptions,
            bool bDepartureOnly,
            bool bArrivalOnly,
            GestionDonneesHUB2SIM donnees)  // >> Task #10079 Pax2Sim - Exception Editor Search functionality
        {
            initialize();
            dtTable_ = dtTable;
            dtFPA_ = dtFPATable;
            dtFPD_ = dtFPDTable;
            dtAirlines_ = dtAirline;    // >> Task #10079 Pax2Sim - Exception Editor Search functionality
            etpParameter_ = etpParameter;
            dtMotherTable_ = dtParentTable;
            bColumnExceptions_ = bColumnExceptions;
            sExceptionType_ = sExceptionType;
            sAddedColumn = "";
            donnees_ = donnees;

            // << Task #9486 Pax2Sim - Exception Editor details            
            if (sExceptionType.EndsWith(GlobalNames.Flight))
                initializeVisibleListForFlightException(lst_Visible, dtTable_, bColumnExceptions, dtFPATable, dtFPDTable);
            else
                InitializeAvailableInformations(lst_Visible, null, dtTable_, bColumnExceptions, "");
            //InitializeAvailableInformations(lst_Visible, null, dtTable_, bColumnExceptions, "");
            // >> Task #9486 Pax2Sim - Exception Editor details

            if (sExceptionType.StartsWith(GlobalNames.FirstAndBusiness))
            {
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                {
                    sAddedColumn = GlobalNames.Flight;
                    if ((bArrivalOnly) || (bArrivalOnly == bDepartureOnly))
                    {
                        //InitializeAvailableInformations(lst_Available, lst_Visible, dtFPATable, false, "A_");
                        InitializeAvailableInformationsForFlightExceptions(lst_Available, lst_Visible, dtFPATable, "A_"); // << Task #9486 Pax2Sim - Exception Editor details
                    }
                    if ((bDepartureOnly) || (bArrivalOnly == bDepartureOnly))
                    {
                        //InitializeAvailableInformations(lst_Available, lst_Visible, dtFPDTable, false, "D_");
                        InitializeAvailableInformationsForFlightExceptions(lst_Available, lst_Visible, dtFPDTable, "D_"); // << Task #9486 Pax2Sim - Exception Editor details                        
                    }
                }
                else if (sExceptionType.EndsWith(GlobalNames.Airline))
                {
                    sAddedColumn = GlobalNames.Airline;
                    // >> Task #10066 Pax2Sim - Airline exception editor
                    DataTable airlinesInUseTable = createAirlinesInUseTable(dtAirline, dtFPA_, dtFPD_);
                    InitializeAvailableInformations(lst_Available, lst_Visible, airlinesInUseTable, false, "");    //InitializeAvailableInformations(lst_Available, lst_Visible, dtAirline, false, "");                    
                    // << Task #10066 Pax2Sim - Airline exception editor                                        
                }
                else if (sExceptionType.EndsWith(GlobalNames.FlightCategory))
                {
                    sAddedColumn = GlobalNames.FlightCategory;
                    InitializeAvailableInformations(lst_Available, lst_Visible, dtFlightCategorie, false, "");
                }
                else
                {
                    InitializeAvailableInformations(lst_Available, lst_Visible, dtParentTable, bColumnExceptions, "");
                }
            }
            else
            {
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                {
                    sAddedColumn = GlobalNames.Flight;
                    if ((bArrivalOnly) || (bArrivalOnly == bDepartureOnly))
                    {
                        //InitializeAvailableInformations(lst_Available, lst_Visible, dtFPATable, false, "A_");
                        InitializeAvailableInformationsForFlightExceptions(lst_Available, lst_Visible, dtFPATable, "A_");     // << Task #9486 Pax2Sim - Exception Editor details
                    }
                    if ((bDepartureOnly) || (bArrivalOnly == bDepartureOnly))
                    {
                        //InitializeAvailableInformations(lst_Available, lst_Visible, dtFPDTable, false, "D_");
                        InitializeAvailableInformationsForFlightExceptions(lst_Available, lst_Visible, dtFPDTable, "D_");   // << Task #9486 Pax2Sim - Exception Editor details
                    }
                }
                else if (sExceptionType.EndsWith(GlobalNames.Airline))
                {
                    sAddedColumn = GlobalNames.Airline;
                    // >> Task #10066 Pax2Sim - Airline exception editor
                    DataTable airlinesInUseTable = createAirlinesInUseTable(dtAirline, dtFPA_, dtFPD_);
                    InitializeAvailableInformations(lst_Available, lst_Visible, airlinesInUseTable, false, "");    //InitializeAvailableInformations(lst_Available, lst_Visible, dtAirline, false, "");                    
                    // << Task #10066 Pax2Sim - Airline exception editor                    
                }
                else if (sExceptionType.EndsWith(GlobalNames.FlightCategory))
                {
                    sAddedColumn = GlobalNames.FlightCategory;
                    InitializeAvailableInformations(lst_Available, lst_Visible, dtFlightCategorie, false, "");
                }
            }

            // >> Task #10079 Pax2Sim - Exception Editor Search functionality
            if (sExceptionType.EndsWith(GlobalNames.Flight))
            {
                ToolTip searchButtonToolTip = new ToolTip();
                searchButtonToolTip.SetToolTip(btnSearch, "The search for a flight is made using the flight number.");
                searchButtonToolTip.AutomaticDelay = 500;
            }

            ToolTip multipleSearchCheckBoxToolTip = new ToolTip();
            multipleSearchCheckBoxToolTip.SetToolTip(cbMultipleSearch, "For a multiple input please separate the values using the comma.");
            multipleSearchCheckBoxToolTip.AutomaticDelay = 500;

            if (!sExceptionType.EndsWith(GlobalNames.Airline))
            {
                cbLoadAllAirlineCodes.Visible = false;
            }
            // << Task #10079 Pax2Sim - Exception Editor Search functionality
        }

        // >> Task #10066 Pax2Sim - Airline exception editor
        /// <summary>
        /// Using the Airline codes table we create a new table with the same structure but
        /// only with the codes for the airlines that can be found in the flight plans.
        /// </summary>
        /// <param name="airlineCodes">Airline Codes table from Input data.</param>
        /// <param name="FPA"></param>
        /// <param name="FPD"></param>
        /// <returns></returns>
        private DataTable createAirlinesInUseTable(DataTable airlineCodes, DataTable FPA, DataTable FPD)
        {
            DataTable airlinesInUseTable = airlineCodes.Clone();
           
            List<String> alreadyAddedAirlinesList = new List<String>();
            NormalTable ntFPA = donnees_.GetTable("Input", FPA.TableName);
            NormalTable ntFPD = donnees_.GetTable("Input", FPD.TableName);

            List<DataTable> filtersForFPA = getFiltersForTable(ntFPA, donnees_);
            List<DataTable> filtersForFPD = getFiltersForTable(ntFPD, donnees_);

            fillAirlinesInUseTable(FPA, airlinesInUseTable, alreadyAddedAirlinesList, airlineCodes);
            foreach (DataTable fpaFilter in filtersForFPA)
                fillAirlinesInUseTable(fpaFilter, airlinesInUseTable, alreadyAddedAirlinesList, airlineCodes);

            fillAirlinesInUseTable(FPD, airlinesInUseTable, alreadyAddedAirlinesList, airlineCodes);
            foreach (DataTable fpdFilter in filtersForFPD)
                fillAirlinesInUseTable(fpdFilter, airlinesInUseTable, alreadyAddedAirlinesList, airlineCodes);

            return airlinesInUseTable;
        }

        private static List<DataTable> getFiltersForTable(DataManagement.NormalTable parentTable, GestionDonneesHUB2SIM donnees)
        {
            List<DataTable> filtersList = new List<DataTable>();
            List<String> filtersNamesList = parentTable.GetAllFilters();

            if (filtersNamesList != null && filtersNamesList.Count > 0)
            {
                foreach (String filterName in filtersNamesList)
                {
                    DataTable filterTable = donnees.getTable("Input", filterName);
                    if (filterTable != null && !filtersList.Contains(filterTable))
                        filtersList.Add(filterTable);
                }
            }

            return filtersList;
        }

        private static void fillAirlinesInUseTable(DataTable sourceFlightPlan, DataTable airlinesInUseTable, List<String> alreadyAddedAirlinesList, DataTable airlineCodes)
        {
            if (sourceFlightPlan != null && airlinesInUseTable != null)
            {
                sourceFlightPlan.AcceptChanges();
                int indexFPD_A_AirlineColumn = sourceFlightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                int indexAirlinesInUse_AirlineCodeColumn = airlinesInUseTable.Columns.IndexOf(GlobalNames.sFPAirline_AirlineCode);

                if (indexFPD_A_AirlineColumn != -1 && indexAirlinesInUse_AirlineCodeColumn != -1)
                {

                    // << Task T-20 ValueCoders - Airline Exception editor improvement
                    //Fetching the value of description by running two loops for two different table
                    foreach (DataRow dr in sourceFlightPlan.Rows)
                    {
                        String airline = dr[indexFPD_A_AirlineColumn].ToString();

                        foreach (DataRow item in airlineCodes.Rows)
                        {
                            string airlineValue = item[0].ToString();
                            if (airlineValue == airline)
                            {
                                string airlineDescriptionValue = item[1].ToString();

                                if (!alreadyAddedAirlinesList.Contains(airline))
                                {
                                    alreadyAddedAirlinesList.Add(airline);
                                    DataRow airlinesInUseRow = airlinesInUseTable.NewRow();
                                    airlinesInUseRow[indexAirlinesInUse_AirlineCodeColumn] = airline;
                                    airlinesInUseRow[1] = airlineDescriptionValue;
                                    airlinesInUseTable.Rows.Add(airlinesInUseRow);
                                }
                                break;
                            }
                        }
                    }
                    airlinesInUseTable.AcceptChanges();
                }
                else
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while updating the Airline codes in the Exception editor."
                        + " The Airline codes column is missing from the Flight plan and/or the Airline Codes table(s).");
                }
            }
            else
            {
                OverallTools.ExternFunctions.PrintLogFile("Error while updating the Airline codes in the Exception editor."
                    + " The Flight plan or Airline codes table doesn't exist.");
            }
        }
        // << Task #10066 Pax2Sim - Airline exception editor

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
                        if (columnHasException(dcColumn.ColumnName, lst_Visible))
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
                        // << Task #9486 Pax2Sim - Exception Editor details
                        String exceptedEntityValue = lst_Visible.Items[i].ToString();
                        if (exceptedEntityValue.IndexOf(GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER) != -1)
                        {
                            exceptedEntityValue = exceptedEntityValue
                                .Substring(0, exceptedEntityValue.IndexOf(GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER)).Trim();
                        }
                        // >> Task #9486 Pax2Sim - Exception Editor details

                        if (dtTable_.Columns.Contains(exceptedEntityValue)) //if (dtTable_.Columns.Contains(lst_Visible.Items[i].ToString()))   // << Task #9486 Pax2Sim - Exception Editor details
                            continue;
                        dtTable_.Columns.Add(exceptedEntityValue, typeof(Double));  //dtTable_.Columns.Add(lst_Visible.Items[i].ToString(), typeof(Double));    // << Task #9486 Pax2Sim - Exception Editor details

                        // recherche des valeurs par defaut (recherche de correspondance avec les tables FPA FPD et la table parente)
                        bool filled = EnterDefaultValue(dtTable_, dtTable_.Columns.Count - 1, dtMotherTable_, dtFPA_, dtFPD_, sExceptionType_, bColumnExceptions_);
                        if (!filled)
                        {
                            if (ldDefaultValues == null || ldDefaultValues.Count == 0)
                                for (int j = 0; j < dtMotherTable_.Rows.Count; j++)
                                {
                                    // >> Bug #10066 Pax2Sim - Airline exception editor
                                    if (j == 0)
                                        dtTable_.Rows[j][exceptedEntityValue] = 100; //dtMotherTable_.Rows[j][1];  //dtTable_.Rows[j][lst_Visible.Items[i].ToString()] = dtMotherTable_.Rows[j][1];    // << Task #9486 Pax2Sim - Exception Editor details
                                    else
                                        dtTable_.Rows[j][exceptedEntityValue] = 0;
                                    // << Bug #10066 Pax2Sim - Airline exception editor
                                }
                            else
                                for (int j = 0; j < ldDefaultValues.Count; j++)
                                    dtTable_.Rows[j][exceptedEntityValue] = ldDefaultValues[j]; //dtTable_.Rows[j][lst_Visible.Items[i].ToString()] = ldDefaultValues[j];   // << Task #9486 Pax2Sim - Exception Editor details
                        }

                    }
                    OverallTools.DataFunctions.SortColumns(dtTable_, iNumber);
                }
                else
                {
                    List<String> lsAlreadyPresent = new List<string>();
                    for (int i = 0; i < dtTable_.Rows.Count; i++)
                    {
                        // >> Bug #19986 PAX2SIM - Exception editor
                        /*
                                                if (!lst_Visible.Items.Contains(dtTable_.Rows[i][0].ToString()))
                                                {
                                                    dtTable_.Rows.RemoveAt(i);
                                                    i--;
                                                    continue;
                                                }
                        */
                        if (!columnHasException(dtTable_.Rows[i][0].ToString(), lst_Visible))
                        {
                            dtTable_.Rows.RemoveAt(i);
                            i--;
                            continue;
                        }
                        // << Bug #19986 PAX2SIM - Exception editor

                        lsAlreadyPresent.Add(dtTable_.Rows[i][0].ToString());
                    }

                    int iOffset = dtTable_.Columns.Count - dtMotherTable_.Columns.Count;
                    for (int i = 0; i < lst_Visible.Items.Count; i++)
                    {
                        // << Task #9486 Pax2Sim - Exception Editor details
                        String exceptedEntityValue = lst_Visible.Items[i].ToString();
                        if (exceptedEntityValue.IndexOf(GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER) != -1)
                        {
                            exceptedEntityValue = exceptedEntityValue
                                .Substring(0, exceptedEntityValue.IndexOf(GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER)).Trim();
                        }
                        // >> Task #9486 Pax2Sim - Exception Editor details
                        if (lsAlreadyPresent.Contains(exceptedEntityValue))//if (lsAlreadyPresent.Contains(lst_Visible.Items[i].ToString()))    // << Task #9486 Pax2Sim - Exception Editor details
                            continue;
                        int indexMere = OverallTools.DataFunctions.indexLigne(dtMotherTable_, 0, exceptedEntityValue);//int indexMere = OverallTools.DataFunctions.indexLigne(dtMotherTable_, 0, lst_Visible.Items[i].ToString()); // << Task #9486 Pax2Sim - Exception Editor details
                        if (indexMere == -1) break;
                        DataRow drMother = dtMotherTable_.Rows[indexMere];
                        DataRow drRow = dtTable_.NewRow();
                        if (iOffset == 1)
                            drRow[0] = exceptedEntityValue;//drRow[0] = lst_Visible.Items[i].ToString();    // << Task #9486 Pax2Sim - Exception Editor details
                        for (int j = 0; j < dtMotherTable_.Columns.Count; j++)
                        {
                            drRow[j + iOffset] = drMother[j];
                        }
                        dtTable_.Rows.Add(drRow);
                    }
                }
                return dtTable_;
            }
        }

        public static bool columnHasException(string columnName, ListBox selectedColumnDescriptions)
        {
            if (columnName == null)
                return false;
            for (int i = 0; i < selectedColumnDescriptions.Items.Count; i++)
            {
                string selectedColumnDescription = selectedColumnDescriptions.Items[i].ToString();
                string selectedColumnName = selectedColumnDescription;
                if (selectedColumnDescription.IndexOf(GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER) != -1)
                {
                    selectedColumnName = selectedColumnDescription
                        .Substring(0, selectedColumnDescription.IndexOf(GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER)).Trim();
                }
                if (columnName.Equals(selectedColumnName))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Fonction pour récuperer une valeur dans la colonne "outFPcolumn" à la première 
        /// ligne où la colonne "inFPColumn" à la valeur "value".
        /// </summary>
        /// <param name="dtFPDTable"></param>
        /// <param name="dtFPATable"></param>
        /// <param name="inFPColumn"></param>
        /// <param name="outFPcolumn"></param>
        /// <returns></returns>
        private static object GetValueInFor(DataTable dtFPDTable, DataTable dtFPATable, String inFPColumn, String outFPcolumn, object value)
        {
            object objFound = null;
            for (int i = 0; i < 2; i++)
            {
                DataTable dtTable = i == 0 ? dtFPDTable : dtFPATable;

                // retrouver le numero de la colonne correspondant à "inFPColumn"
                int iInFPColumn = -1;
                foreach (DataColumn dcColumn in dtTable.Columns)
                    if (dcColumn.ColumnName == inFPColumn)
                        iInFPColumn = dtTable.Columns.IndexOf(dcColumn);
                if (iInFPColumn == -1)
                    break;

                // retrouver l'index de la ligne en question
                int index = OverallTools.DataFunctions.indexLigne(dtTable, iInFPColumn, value.ToString());
                if (index == -1)
                {
                    // >> Bug #10066 Pax2Sim - Airline exception editor
                    continue;
                    //break;
                    // << Bug #10066 Pax2Sim - Airline exception editor
                }

                // puis aller chercher la valeur de la colonne desiré
                objFound = dtTable.Rows[index][outFPcolumn];
            }
            return objFound;
        }

        /// <summary>
        /// Fonction pour remplir la table d'exception générés avec des valeurs par defaut.
        /// les valeurs par defaut sont récupérés depuis la table parente.
        /// </summary>
        /// <param name="dtTable">Table à completer</param>
        /// <param name="EmptyEntry">Numero de la ligne (ou colonne) qui doit être complétée.</param>
        /// <param name="dtParent">Table parente</param>
        /// <param name="dtFPDTable">Table Flight Plan Departure</param>
        /// <param name="dtFPATable">Table Flight Plan Arrival</param>
        /// <param name="sExceptionType">Type d'exception</param>
        /// <param name="bColumnExceptions">Pour précisé s'il s'agit d'une table en colonne</param>
        /// <returns>Booleen indiquant si la table à peu être initialisé</returns>
        private bool EnterDefaultValue(
            DataTable dtTable,
            int EmptyEntry,
            DataTable dtParent,
            DataTable dtFPDTable,
            DataTable dtFPATable,
            String sExceptionType,
            bool bColumnExceptions)
        {

            if (bColumnExceptions)
            {
                if (dtTable.Columns.Count - 1 < EmptyEntry) return false;
            }
            else
                return false; // on n'initialise pas pour lignes

            object valeur; // nom de l'entrée ou il faut recuperer les valeurs par defaut dans la table parente

            // cas ou on rempli avec les valeurs de la table parente du même nom que l'entrée
            if (sExceptionType.Equals(GlobalNames.FirstAndBusiness))
            {
                if (bColumnExceptions)
                    valeur = dtTable.Columns[EmptyEntry].ColumnName;
                else
                    valeur = dtTable.Rows[EmptyEntry][0];
                if (valeur == null || valeur.ToString() == "")
                    return false;
            }
            // cas ou il faut aller chercher un une valeur dans les tables FPA et FPD
            else
            {
                // recherche du nom de la colonne ou cherché
                String inFPColumn = "";
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                    inFPColumn = GlobalNames.sFPD_A_Column_ID;
                else if (sExceptionType.EndsWith(GlobalNames.Airline))
                    inFPColumn = GlobalNames.sFPD_A_Column_AirlineCode;
                else if (sExceptionType.EndsWith(GlobalNames.FlightCategory))
                    inFPColumn = GlobalNames.sFPD_A_Column_FlightCategory;

                // recherche du nom de la colonne pour la valeur de retour
                String outFPcolumn = "";
                if (bColumnExceptions)
                    outFPcolumn = GlobalNames.sFPD_A_Column_FlightCategory;
                else
                    return false;

                // Aller chercher (dans la colonne inFPColumn) la ligne ou cette entrée apparaît pour la premiere fois dans les table FPA ou FPD
                String search;
                if (bColumnExceptions)
                    search = dtTable.Columns[EmptyEntry].ColumnName;
                else
                    search = dtTable.Rows[EmptyEntry][0].ToString();
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                    search = search.Substring(2);
                valeur = GetValueInFor(dtFPDTable, dtFPATable, inFPColumn, outFPcolumn, search);

                if (valeur == null)
                    return false;
            }

            // Recuperer dans la table parente les informations pour l'entrée correspondante
            object[] defaultElt;
            int index = 0;
            if (bColumnExceptions)
            {
                if (!dtParent.Columns.Contains(valeur.ToString()))
                    return false;
                defaultElt = new object[dtTable.Rows.Count];
                for (int i = 0; i < defaultElt.Length; i++)
                {
                    if (i < dtParent.Rows.Count)
                        defaultElt[i] = dtParent.Rows[i][valeur.ToString()];
                    else
                        defaultElt[i] = "0";
                }
            }
            else
            {
                defaultElt = new object[dtTable.Columns.Count - 1];
                // recherche de la ligne correspondant à value
                index = OverallTools.DataFunctions.indexLigne(dtTable, 0, valeur.ToString());
                for (int i = 0; i < defaultElt.Length; i++)
                    defaultElt[0] = dtParent.Rows[index][i + 1];
            }

            // Remplir la ligne non renseignée
            if (bColumnExceptions)
            {
                for (int i = 0; i < defaultElt.Length; i++)
                    if (i < dtTable.Rows.Count)
                        dtTable.Rows[i][EmptyEntry] = defaultElt[i];
            }
            else
            {
                for (int i = 0; i < defaultElt.Length; i++)
                    if (i + 1 < dtTable.Columns.Count)
                        dtTable.Rows[index][i + 1] = defaultElt[0];
            }

            return true;
        }

        #region Mouvement des Elements
        /// <summary>
        /// Fonction pour déplacer un element de la liste des élements disponnible 
        /// à la liste des elements d'exception.
        /// </summary>
        private void bt_right_Click(object sender, EventArgs e)
        {
            MoveItemTo(lst_Available, lst_Visible);
            lst_Available.ClearSelected();  // >> Task #10079 Pax2Sim - Exception Editor Search functionality
        }

        /// <summary>
        /// Fonction pour déplacer un element de la liste des élements d'exception 
        /// à la liste des elements disponnible.
        /// </summary>
        private void bt_left_Click(object sender, EventArgs e)
        {
            MoveItemTo(lst_Visible, lst_Available);
        }

        /// <summary>
        /// Fonction pour deplacer l'élements selectionné d'une liste à une autre.
        /// (Supprime l'element de la liste d'origine)
        /// </summary>
        /// <param name="lstSource">Liste d'origine de l'element</param>
        /// <param name="lstDestination">Liste de destination de l'élement</param>
        private static void MoveItemTo(ListBox lstSource, ListBox lstDestination)
        {
            lstDestination.Sorted = true;
            if (lstSource.SelectedItems.Count == 0)
                return;
            int i;
            System.Collections.ArrayList lstSelected = new System.Collections.ArrayList();
            for (i = 0; i < lstSource.SelectedItems.Count; i++)
            {
                lstDestination.Items.Add(lstSource.SelectedItems[i]);
                lstSelected.Add(lstSource.SelectedItems[i]);
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

        /// <summary>
        /// Fonction pour mettre à jour la liste avec les elements déjà dans la table d'exception.
        /// </summary>
        /// <param name="lstToFill"></param>
        /// <param name="lstSecondList"></param>
        /// <param name="dtOriginTable"></param>
        /// <param name="bColumnExceptions"></param>
        /// <param name="sPrefixe"></param>
        private static void InitializeAvailableInformations(ListBox lstToFill,
            ListBox lstSecondList,
            DataTable dtOriginTable,
            bool bColumnExceptions,
            String sPrefixe)
        {
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
                foreach (DataRow dcRow in dtOriginTable.Rows)
                {
                    if ((lstSecondList != null) && (lstSecondList.Items.Contains(sPrefixe + dcRow[0].ToString())))
                        continue;
                    //if (!lstToFill.Items.Contains(sPrefixe + dcRow[0].ToString()))
                    string menuItemsValue = sPrefixe + dcRow[0].ToString() + " (" + dcRow[1].ToString() + " )";  // << Task T-20 ValueCoders - Airline Exception editor improvement
                    if (!lstToFill.Items.Contains(menuItemsValue))
                    {
                        lstToFill.Items.Add(menuItemsValue);
                    }
                }
            }
        }
        // << Task #9486 Pax2Sim - Exception Editor details
        private static void initializeVisibleListForFlightException(ListBox visibleList, DataTable dtTable,
            bool bColumnExceptions, DataTable dtFPA, DataTable dtFPD)
        {
            visibleList.Sorted = true;
            if (dtTable == null || dtFPA == null || dtFPD == null)
            {
                return;
            }

            int indexColumnArrivalFlightNb = dtFPA.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnDepartureFlightNb = dtFPD.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnArrivalFlightId = dtFPA.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int indexColumnDepartureFlightId = dtFPD.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);

            int indexColumnAirline = dtFPA.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int indexColumnAirport = dtFPA.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            int indexColumnAircraftType = dtFPA.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);

            if (indexColumnArrivalFlightNb == -1 || indexColumnArrivalFlightId == -1 || indexColumnDepartureFlightNb == -1 || indexColumnDepartureFlightId == -1
                || indexColumnAirline == -1 || indexColumnAirport == -1 || indexColumnAircraftType == -1)
            {
                return;
            }

            if (bColumnExceptions)
            {
                foreach (DataColumn dcColumn in dtTable.Columns)
                {
                    String flightIdWithPrefix = dcColumn.ColumnName;
                    bool isArrival = flightIdWithPrefix.Contains("A_");
                    String flightId = "";
                    String flightNumber = "";
                    string airline = string.Empty;
                    string airport = string.Empty;
                    string aircraftType = string.Empty;

                    if (OverallTools.FonctionUtiles.estPresentDansListe(flightIdWithPrefix, GestionDonneesHUB2SIM.ListeEnteteDiv))
                        continue;

                    if (isArrival)
                    {
                        flightId = flightIdWithPrefix.Substring(flightIdWithPrefix.IndexOf("A_") + 2);
                        flightNumber = getFlightNumberByFlightId(flightId, dtFPA, indexColumnArrivalFlightId, indexColumnArrivalFlightNb);
                        airline = getFlightAttributeByFlightId(flightId, dtFPA, indexColumnArrivalFlightId, indexColumnAirline);
                        airport = getFlightAttributeByFlightId(flightId, dtFPA, indexColumnArrivalFlightId, indexColumnAirport);
                        aircraftType = getFlightAttributeByFlightId(flightId, dtFPA, indexColumnArrivalFlightId, indexColumnAircraftType);
                    }
                    else
                    {
                        flightId = flightIdWithPrefix.Substring(flightIdWithPrefix.IndexOf("D_") + 2);
                        flightNumber = getFlightNumberByFlightId(flightId, dtFPD, indexColumnDepartureFlightId, indexColumnDepartureFlightNb);
                        airline = getFlightAttributeByFlightId(flightId, dtFPD, indexColumnArrivalFlightId, indexColumnAirline);
                        airport = getFlightAttributeByFlightId(flightId, dtFPD, indexColumnArrivalFlightId, indexColumnAirport);
                        aircraftType = getFlightAttributeByFlightId(flightId, dtFPD, indexColumnArrivalFlightId, indexColumnAircraftType);
                    }

                    visibleList.Items.Add(flightIdWithPrefix + " " + GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER
                                            + airline + ", " + flightNumber + ", " + airport + ", " + aircraftType + GlobalNames.EXCEPTION_EDITOR_DETAILS_FINAL_DELIMITER);
                }
            }
            else
            {
                foreach (DataRow drRow in dtTable.Rows)
                {
                    if (!visibleList.Items.Contains(drRow[0].ToString()))
                    {
                        String flightIdWithPrefix = drRow[0].ToString();
                        bool isArrival = flightIdWithPrefix.Contains("A_");
                        String flightId = "";
                        String flightNumber = "";
                        string airline = string.Empty;
                        string airport = string.Empty;
                        string aircraftType = string.Empty;

                        if (isArrival)
                        {
                            flightId = flightIdWithPrefix.Substring(flightIdWithPrefix.IndexOf("A_") + 1);
                            flightNumber = getFlightNumberByFlightId(flightId, dtFPA, indexColumnArrivalFlightId, indexColumnArrivalFlightNb);
                            airline = getFlightAttributeByFlightId(flightId, dtFPA, indexColumnArrivalFlightId, indexColumnAirline);
                            airport = getFlightAttributeByFlightId(flightId, dtFPA, indexColumnArrivalFlightId, indexColumnAirport);
                            aircraftType = getFlightAttributeByFlightId(flightId, dtFPA, indexColumnArrivalFlightId, indexColumnAircraftType);
                        }
                        else
                        {
                            flightId = flightIdWithPrefix.Substring(flightIdWithPrefix.IndexOf("D_") + 1);
                            flightNumber = getFlightNumberByFlightId(flightId, dtFPD, indexColumnDepartureFlightId, indexColumnDepartureFlightNb);
                            airline = getFlightAttributeByFlightId(flightId, dtFPD, indexColumnArrivalFlightId, indexColumnAirline);
                            airport = getFlightAttributeByFlightId(flightId, dtFPD, indexColumnArrivalFlightId, indexColumnAirport);
                            aircraftType = getFlightAttributeByFlightId(flightId, dtFPD, indexColumnArrivalFlightId, indexColumnAircraftType);
                        }

                        visibleList.Items.Add(flightIdWithPrefix + " " + GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER
                                            + airline + ", " + flightNumber + ", " + airport + ", " + aircraftType + GlobalNames.EXCEPTION_EDITOR_DETAILS_FINAL_DELIMITER);
                    }
                }
            }
        }
        private static String getFlightNumberByFlightId(String flightId, DataTable flightPlan, int indexColumnFlightId, int indexColumnFlightNb)
        {
            String flightNumber = "";

            foreach (DataRow dr in flightPlan.Rows)
            {
                String currentFlightId = dr[indexColumnFlightId].ToString();
                if (currentFlightId.Trim().Equals(flightId.Trim()))
                {
                    flightNumber = dr[indexColumnFlightNb].ToString();
                    break;
                }
            }

            return flightNumber;
        }

        private static String getFlightAttributeByFlightId(string flightId, DataTable flightPlan, int indexColumnFlightId, int indexColumnFlightAttribute)
        {
            String flightAttribute = "";
            foreach (DataRow dr in flightPlan.Rows)
            {
                String currentFlightId = dr[indexColumnFlightId].ToString();
                if (currentFlightId.Trim().Equals(flightId.Trim()))
                {
                    flightAttribute = dr[indexColumnFlightAttribute].ToString();
                    break;
                }
            }
            return flightAttribute;
        }

        private static void InitializeAvailableInformationsForFlightExceptions(ListBox lstToFill, ListBox lstSecondList,
                                                                               DataTable dtOriginTable, String sPrefixe)
        {
            lstToFill.Sorted = true;
            if (dtOriginTable == null)
                return;

            int indexColumnFlightNb = dtOriginTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int indexColumnAirline = dtOriginTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int indexColumnAirport = dtOriginTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            int indexColumnAircraftType = dtOriginTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
            if (indexColumnFlightNb == -1 || indexColumnAirline == -1 || indexColumnAirport == -1 || indexColumnAircraftType == -1)
                return;

            foreach (DataRow row in dtOriginTable.Rows)
            {
                String flightDescription = sPrefixe + row[0].ToString()
                    + " " + GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER + row[indexColumnAirline] + ", " + row[indexColumnFlightNb] + ", "
                    + row[indexColumnAirport] + ", " + row[indexColumnAircraftType] + GlobalNames.EXCEPTION_EDITOR_DETAILS_FINAL_DELIMITER;

                if ((lstSecondList != null) && (lstSecondList.Items.Contains(flightDescription)))
                    continue;
                if (!lstToFill.Items.Contains(flightDescription))
                    lstToFill.Items.Add(flightDescription);
            }

        }
        // >> Task #9486 Pax2Sim - Exception Editor details

        // >> Task #10079 Pax2Sim - Exception Editor Search functionality        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbMultipleSearch.Checked)
                selectMultipleItemsByListOfSearchedItems();
            else
                selectItemsBySearchedString();
        }

        private void selectItemsBySearchedString()
        {
            lst_Available.ClearSelected();

            String searchItem = tbSearch.Text.Trim();

            if (searchItem != null && searchItem != "")
            {
                List<String> itemsFoundInAvailableList = new List<String>();

                foreach (Object obj in lst_Available.Items)
                {
                    String currentItem = obj.ToString().Trim();
                    String currentItemParsed = "";

                    if (currentItem.Contains("A_") || currentItem.Contains("D_"))
                    {
                        String currentFlightNb = currentItem.Substring(currentItem.IndexOf(GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER) + 2);
                        currentItemParsed = currentFlightNb.Substring(0, currentFlightNb.Length - GlobalNames.EXCEPTION_EDITOR_DETAILS_FINAL_DELIMITER.Length);
                    }
                    else
                    {
                        currentItemParsed = currentItem;
                    }

                    if (currentItemParsed.Contains(searchItem)
                        && !itemsFoundInAvailableList.Contains(currentItem))
                    {
                        itemsFoundInAvailableList.Add(currentItem);
                    }
                }

                if (itemsFoundInAvailableList.Count > 0)
                {
                    foreach (String item in itemsFoundInAvailableList)
                    {
                        int index = lst_Available.FindString(item, -1);
                        if (index != -1)
                        {
                            lst_Available.SetSelected(index, true);
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("The item " + tbSearch.Text + " was not found in the list.");
                    MessageBox.Show("There are no items containing the text " + tbSearch.Text + " in their description.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid search parameter.");
            }
        }

        private void selectMultipleItemsByListOfSearchedItems()
        {
            lst_Available.ClearSelected();

            String inputData = tbSearch.Text.Trim();
            string[] searchItemsList = inputData.Split(GlobalNames.EXCEPTION_EDITOR_SEARCH_FILTER_DELIMITER);

            if (!inputData.Contains(GlobalNames.EXCEPTION_EDITOR_SEARCH_FILTER_DELIMITER.ToString()))
            {
                MessageBox.Show("Please check the input data. The items must be separated by comma.");
                return;
            }

            if (searchItemsList != null && searchItemsList.Length > 0)
            {
                bool haveMissingItem = false;
                String searchedItemsNotFoundString = "";

                for (int i = 0; i < searchItemsList.Length; i++)
                {
                    String inputedItem = searchItemsList[i].Trim();
                    List<String> itemsFoundInAvailableList = new List<String>();

                    foreach (Object obj in lst_Available.Items)
                    {
                        String currentItem = obj.ToString().Trim();
                        String currentItemParsed = "";

                        if (currentItem.Contains("A_") || currentItem.Contains("D_"))
                        {
                            String currentFlightNb = currentItem.Substring(currentItem.IndexOf(GlobalNames.EXCEPTION_EDITOR_DETAILS_INITIAL_DELIMITER) + 2);
                            currentItemParsed = currentFlightNb.Substring(0, currentFlightNb.Length - GlobalNames.EXCEPTION_EDITOR_DETAILS_FINAL_DELIMITER.Length);
                        }
                        else
                        {
                            currentItemParsed = currentItem;
                        }

                        if (currentItemParsed.Equals(inputedItem)
                            && !itemsFoundInAvailableList.Contains(currentItem))
                        {
                            itemsFoundInAvailableList.Add(currentItem);
                        }
                    }

                    if (itemsFoundInAvailableList.Count > 0)
                    {
                        foreach (String item in itemsFoundInAvailableList)
                        {
                            int index = lst_Available.FindString(item, -1);
                            if (index != -1)
                            {
                                lst_Available.SetSelected(index, true);
                            }
                        }
                    }
                    else
                    {
                        if (inputedItem != "")
                        {
                            searchedItemsNotFoundString += " " + inputedItem;
                            haveMissingItem = true;
                        }
                    }
                }

                if (haveMissingItem)
                {
                    MessageBox.Show("The list doesn't contain any of the items:" + searchedItemsNotFoundString + ".");
                }
            }
        }

        private void btnClearSelectedItems_Click(object sender, EventArgs e)
        {
            lst_Available.ClearSelected();
        }

        private void cbMultipleSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbMultipleSearch.Checked)
                tbSearch.Text = "";
        }

        private static char[] allowedCharsList = { '\b', '-', '_', ',' };

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar)
                || Array.IndexOf(allowedCharsList, e.KeyChar) > -1)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cbLoadAllAirlineCodes_CheckedChanged(object sender, EventArgs e)
        {
            lst_Available.Items.Clear();

            if (cbLoadAllAirlineCodes.Checked)
            {
                InitializeAvailableInformations(lst_Available, lst_Visible, dtAirlines_, false, "");
            }
            else
            {
                DataTable airlinesInUseTable = createAirlinesInUseTable(dtAirlines_, dtFPA_, dtFPD_);
                InitializeAvailableInformations(lst_Available, lst_Visible, airlinesInUseTable, false, "");
            }
        }
        // << Task #10079 Pax2Sim - Exception Editor Search functionality
    }
}
