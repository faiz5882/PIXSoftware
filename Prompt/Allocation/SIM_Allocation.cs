using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.DataManagement;
using SIMCORE_TOOL.Prompt.Allocation.General;
using SIMCORE_TOOL.com.crispico.FlightPlansUtils;

namespace SIMCORE_TOOL.Prompt
{
    /// <summary>
    /// Classe permettant l'allocation de postes d'arrivé et départ tel que Make-Up, Check-In...
    /// </summary>
    public partial class SIM_Allocation : Form
    {

        #region Variables
        /// <summary>
        /// Variable permettant d'acceder aux données de la classe GestionDonneesHUB2SIM
        /// </summary>
        GestionDonneesHUB2SIM Donnees;
        Classes.GenerateAllocationTool gatAllocation;

        DataTable[] dtResults;
        VisualisationMode[] vmResults;

        DataTable fpTable;
        DataManagement.NormalTable AllocationTable;
        List<GenerateAllocationTool.SegregationForOverlap> lgatOverlapInformations = null;
        GenerateAllocationTool.SegregationForOverlap sfoCurrentOverlap = null;
        String sNumberBagsTableName;
        String sArrivalLoadFactorTableName;
        Boolean bUseExceptionNumberBagsTableName;
        Boolean bUseExceptionArrivalLoadFactorTableName;
        int iNumberOfReclaimWithDoubleInfeed;
        Double dInfeedSpeed;

        bool buserCheck = true;

        private List<string> dbFlightCategories = new List<string>();   // >> Task #16728 PAX2SIM Improvements (Recurring) C#7
        #endregion

        #region Accessors
        public DataTable[] Results
        {
            get
            {
                return dtResults;
            }
        }
        public VisualisationMode[] VisualisationModeResults
        {
            get
            {
                return vmResults;
            }
        }
        public String TablesNames
        {
            get
            {
                return txt_AllocationName.Text;
            }
        }

        internal Classes.GenerateAllocationTool getGatAllocation()
        {
            return gatAllocation;
        }
        internal String allocatedType()
        {
            return cb_AllocType.SelectedItem.ToString();
        }
        #endregion

        #region Constructors
        private void Initialize(GestionDonneesHUB2SIM gdhDonnees, Classes.GenerateAllocationTool gatTmp)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            Donnees = gdhDonnees;

            DataTable dtFlightCategories = gdhDonnees.getTable("Input",GlobalNames.FP_FlightCategoriesTableName);
            if (dtFlightCategories != null)
            {
                foreach (DataRow drRow in dtFlightCategories.Rows)
                {                    
                    dbFlightCategories.Add(FonctionsType.getString(drRow[0]));
                }
            }
            if (gatTmp != null)
            {
                txt_AllocationName.Text = gatTmp.NomTable;
                /// Si le nom du poste est Baggage-Claim ou Transfer-Infeed alors l'allocation concerne un poste de départ
                if (gatTmp.sNomColonneAllocation == "Baggage-Claim" || gatTmp.sNomColonneAllocation == "Transfer-Infeed")
                    rb_Arrival.Checked = true;
                else
                    rb_Departure.Checked = true;

                cb_UseInfeed.Checked = (gatTmp.dtNbBagage != null);
                if (cb_UseInfeed.Checked)
                {
                    sNumberBagsTableName = gatTmp.dtNbBagage.Name;
                    sArrivalLoadFactorTableName = gatTmp.dtLoadFactors.Name;
                    iNumberOfReclaimWithDoubleInfeed = gatTmp.iNumberOfDoubleInfeed;
                    dInfeedSpeed = gatTmp.dInfeedSpeed;
                    bUseExceptionArrivalLoadFactorTableName = gatTmp.UseLoadFactorExceptions;
                    bUseExceptionNumberBagsTableName = gatTmp.UseNbBaggageExceptions;
                }


                ///On définit les différents champs par rapport à ce qui a été sauvegardé
                cb_AllocType.SelectedItem = gatTmp.sNomColonneAllocation.ToString();
                cb_FlightPlan.SelectedItem = gatTmp.dtFPTable.ToString();
                cb_AirlineCT.SelectedItem = gatTmp.dtAirlineTable.ToString();
                cb_AircraftCT.SelectedItem = gatTmp.dtAircraftTable.ToString();
                cb_AircraftCodeEx.Checked = gatTmp.UseAircraftTableExceptions;
                // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                if (gatTmp.dtLoadFactors != null)
                {
                    cb_LoadFactorsTableName.SelectedItem = gatTmp.dtLoadFactors.ToString();
                    cb_LoadFactorsExc.Checked = gatTmp.UseLoadFactorExceptions;
                }
                if (gatTmp.dtNbBagage != null)
                {
                    cb_NbBagsTableName.SelectedItem = gatTmp.dtNbBagage.ToString();
                    cb_NbBagsExc.Checked = gatTmp.UseNbBaggageExceptions;
                }
                fpParametersCheckBox.Checked = (gatTmp.flightPlanParametersTable != null);
                if (gatTmp.flightPlanParametersTable != null && fpParametersComboBox.Items.Contains(gatTmp.flightPlanParametersTable.ToString()))   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
                {
                    fpParametersComboBox.SelectedItem = gatTmp.flightPlanParametersTable.ToString();                    
                }
                cb_calculateBasedOnContainerSize.Checked = gatTmp.calculateBasedOnContainerSize;
                cb_UseInfeed.Checked = gatTmp.useInfeedLimitation;
                // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                cb_RulesAlloc.SelectedItem = gatTmp.dtOCTTable.ToString();
                cb_RulesAllocationEx.Checked = gatTmp.UseOCTTableExceptions;

                cb_TerminalInfo.SelectedItem = gatTmp.ObservedTerminal.ToString();
                cb_TerminalObserved.SelectedItem = gatTmp.Terminal.ToString();
                dtp_Begin.Value = gatTmp.dtBegin;
                dtp_End.Value = gatTmp.dtEnd;
                txt_TimeStep.Text = gatTmp.dStep.ToString();
                txt_Separation.Text = gatTmp.dTimeBetweenFlights.ToString();

                cb_AllocateFlightPlan.Checked = gatTmp.AllocateFlightPlan;
                cb_ColorFC.Checked = gatTmp.ColorByFC;
                cb_ColorHandlers.Checked = gatTmp.ColorByHandler;
                cb_AirlineColor.Checked = gatTmp.ColorByAirline;
                cb_Algorithme.Checked = gatTmp.FCAllocation;
                cb_LooseULD.Checked = gatTmp.SegregateLooseULD;
                cb_FPAsBasis.Checked = gatTmp.UseFPContent;


                cb_GroundHandlersSegregation.Checked = gatTmp.SegregateGroundHandlers;
                // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
                cbContinousSegregationNumerotation.Checked = gatTmp.ContinousSegregationNumerotation;
                // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
                // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                firstIndexTextBox.Text = gatTmp.FirstIndexForAllocation.ToString();
                // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource

                lgatOverlapInformations = gatTmp.OverlapParameters;
                if (lgatOverlapInformations != null)
                {
                    foreach (GenerateAllocationTool.SegregationForOverlap sfoTmp in lgatOverlapInformations)
                    {
                        listBox1.Items.Add(sfoTmp.Name);                        
                    }
                }

                // >> Task #12741 Pax2Sim - Dubai - flight exceptions
                if (PAX2SIM.dubaiMode)
                {
                    parkingDynamicOpeningTimesGroupBox.Visible = true;
                    useDynamicOpeningTimes.Visible = true;
                    dynamicOpeningFPColumnsLabel.Visible = true;
                    dynamicOpeningTimesColumnName.Visible = true;
                }
                else
                {
                    parkingDynamicOpeningTimesGroupBox.Visible = false;
                    useDynamicOpeningTimes.Visible = false;
                    dynamicOpeningFPColumnsLabel.Visible = false;
                    dynamicOpeningTimesColumnName.Visible = false;
                }

                useDynamicOpeningTimes.Enabled = (gatTmp.sNomColonneAllocation == "Parking");
                useDynamicOpeningTimes.Checked = gatTmp.useDynamicOpeningTimes;

                dynamicOpeningTimesColumnName.Enabled = (useDynamicOpeningTimes.Enabled && useDynamicOpeningTimes.Checked);
                dynamicOpeningFPColumnsLabel.Enabled = (useDynamicOpeningTimes.Enabled && useDynamicOpeningTimes.Checked);
                if (dynamicOpeningTimesColumnName.Enabled)
                {
                    dynamicOpeningTimesColumnName.Text = gatTmp.openingTimeFlightPlanColumnName;
                }                            
                // << Task #12741 Pax2Sim - Dubai - flight exceptions                
            }
            else
            {
                ///Au départ on met tous les champs enable sauf le champs de choix du titre et les radio button d'arrivé ou départ
                cb_FlightPlan.Enabled = cb_AirlineCT.Enabled
                = cb_AircraftCT.Enabled = cb_AirlineColor.Enabled
                = cb_TerminalInfo.Enabled = cb_RulesAlloc.Enabled
                = dtp_Begin.Enabled = dtp_End.Enabled
                = txt_TimeStep.Enabled = txt_Separation.Enabled
                = gb_StationOverlap.Enabled = cb_TerminalObserved.Enabled
                = cb_FPAsBasis.Enabled = cb_AllocateFlightPlan.Enabled
                = gb_Colors.Enabled = gb_Segregation.Enabled
                = cb_NbBagsTableName.Enabled = cb_LoadFactorsTableName.Enabled = cb_calculateBasedOnContainerSize.Enabled    // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                = fpParametersCheckBox.Enabled = fpParametersComboBox.Enabled   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
                = false;
                cb_RulesAllocationEx.Enabled = cb_RulesAlloc.Enabled;
                cb_AircraftCodeEx.Enabled = cb_AircraftCT.Enabled;
                // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                cb_LoadFactorsExc.Enabled = cb_LoadFactorsTableName.Enabled;
                cb_NbBagsExc.Enabled = cb_NbBagsTableName.Enabled;
                // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                lgatOverlapInformations = new List<GenerateAllocationTool.SegregationForOverlap>();
                
                // >> Task #12741 Pax2Sim - Dubai - flight exceptions
                if (PAX2SIM.dubaiMode)
                {
                    parkingDynamicOpeningTimesGroupBox.Visible = true;
                    useDynamicOpeningTimes.Visible = true;
                    dynamicOpeningFPColumnsLabel.Visible = true;
                    dynamicOpeningTimesColumnName.Visible = true;
                }
                else
                {
                    parkingDynamicOpeningTimesGroupBox.Visible = false;
                    useDynamicOpeningTimes.Visible = false;
                    dynamicOpeningFPColumnsLabel.Visible = false;
                    dynamicOpeningTimesColumnName.Visible = false;
                }

                useDynamicOpeningTimes.Checked = false;
                useDynamicOpeningTimes.Enabled = false;

                dynamicOpeningTimesColumnName.Enabled = false;
                dynamicOpeningFPColumnsLabel.Enabled = false;
                // << Task #12741 Pax2Sim - Dubai - flight exceptions
            }
            dtResults = null;
            UpdateFields();
        }

        /// <summary>
        /// Constructeur principal de la classe
        /// </summary>
        /// <param name="gdhDonnees"></param>
        public SIM_Allocation(GestionDonneesHUB2SIM gdhDonnees)
        {
            Initialize(gdhDonnees, null);
        }

        /// <summary>
        /// Constructeur permettant de réouvrir une allocation déja existante pour pouvoir l'éditer
        /// </summary>
        /// <param name="gdhDonnees"></param>
        /// <param name="gatTmp"></param>
        public SIM_Allocation(GestionDonneesHUB2SIM gdhDonnees, Classes.GenerateAllocationTool gatTmp)
        {
            Initialize(gdhDonnees, gatTmp);
        }

        #endregion

        #region Update for the combo boxes (exception of the flight plans).
        /// <summary>
        /// Met à jours les champs Airline et Aircraft types
        /// </summary>
        private void UpdateFields()
        {
            String sValue = cb_AirlineCT.Text;
            List<String> values ;
            values = Donnees.getValidTables("Input", GlobalNames.FP_AirlineCodesTableName);
            cb_AirlineCT.Items.Clear(); 
            cb_AirlineCT.Items.AddRange(values.ToArray());
            if (cb_AirlineCT.Items.Contains(sValue))
                cb_AirlineCT.SelectedItem = sValue;
            else
            {
                if (cb_AirlineCT.Items.Count > 0)   // PAX key flag disabled
                {
                    cb_AirlineCT.SelectedIndex = 0;
                }
            }

            sValue = cb_AircraftCT.Text;
            values = Donnees.getValidTables("Input", GlobalNames.FP_AircraftTypesTableName);
            cb_AircraftCT.Items.Clear(); 
            cb_AircraftCT.Items.AddRange(values.ToArray());
            if (cb_AircraftCT.Items.Contains(sValue))
                cb_AircraftCT.SelectedItem = sValue;
            else
            {
                if (cb_AircraftCT.Items.Count > 0)  // PAX key flag disabled
                {
                    cb_AircraftCT.SelectedIndex = 0;
                }
            }
            // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            sValue = cb_NbBagsTableName.Text;
            values = Donnees.getValidTables("Input", GlobalNames.NbBagsTableName);

            cb_NbBagsTableName.Items.Clear();
            cb_NbBagsTableName.Items.AddRange(values.ToArray());

            if (cb_NbBagsTableName.Items.Contains(sValue))
                cb_NbBagsTableName.SelectedItem = sValue;
            else if (cb_NbBagsTableName.Items.Count > 0)    // PAX key flag disabled
            {
                cb_NbBagsTableName.SelectedIndex = 0;
            }
            // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            if (fpParametersCheckBox.Checked)
            {
                sValue = fpParametersComboBox.Text;
                values = Donnees.getValidTables("Input", FPParametersTableConstants.TABLE_TECHNICAL_NAME);

                fpParametersComboBox.Items.Clear();
                fpParametersComboBox.Items.AddRange(values.ToArray());
                if (fpParametersComboBox.Items.Contains(sValue))
                {
                    fpParametersComboBox.SelectedItem = sValue;
                }
                else if (fpParametersComboBox.Items.Count > 0)
                {
                    fpParametersComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                fpParametersComboBox.Items.Clear();
            }
            // << Task #17690 PAX2SIM - Flight Plan Parameters table
        }
        #endregion

        #region Departure / Arrival Checked
        /// <summary>
        /// Appelée lors du clique sur le radio button de départ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_Departure_CheckedChanged(object sender, EventArgs e)
        {
            ///On efface la liste déja existante et on ajoute les postes correspondants
            cb_AllocType.Items.Clear();
            cb_AllocType.Items.Add("Make-Up");
            cb_AllocType.Items.Add("Check-In");
            cb_AllocType.Items.Add("Boarding-Gate");
            cb_AllocType.Items.Add("Bag-Drop");
            cb_AllocType.Items.Add("Parking"); // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
            ///On selectionne d'origine le poste Make-Up
            cb_AllocType.SelectedIndex = 0;
            ///On affiche maintenant les champs contenus dans la fenetre
            cb_FlightPlan.Enabled = cb_AirlineCT.Enabled = true;
            cb_AircraftCT.Enabled = cb_AirlineColor.Enabled = true;
            cb_RulesAlloc.Enabled = cb_TerminalInfo.Enabled = true;
            cb_RulesAllocationEx.Enabled = cb_RulesAlloc.Enabled;
            cb_TerminalObserved.Enabled = true;
            cb_AircraftCodeEx.Enabled = true;
            dtp_Begin.Enabled = dtp_End.Enabled = true;
            txt_TimeStep.Enabled = txt_Separation.Enabled = true;
            cb_FPAsBasis.Enabled = cb_AllocateFlightPlan.Enabled = true;
            gb_Colors.Enabled = gb_Segregation.Enabled = true;
            gb_StationOverlap.Enabled = true;
            // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            cb_LoadFactorsTableName.Enabled = cb_LoadFactorsExc.Enabled = true;
            cb_NbBagsTableName.Enabled = cb_NbBagsExc.Enabled = true;            
            // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size

            fpParametersComboBox.Enabled = fpParametersCheckBox.Checked;    // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            fpParametersCheckBox.Enabled = true;

            // >> Task #12741 Pax2Sim - Dubai - flight exceptions                
            useDynamicOpeningTimes.Enabled = false;
            useDynamicOpeningTimes.Checked = false;

            dynamicOpeningTimesColumnName.Enabled = false;
            dynamicOpeningFPColumnsLabel.Enabled = false;
            // << Task #12741 Pax2Sim - Dubai - flight exceptions   
        }

        private void rb_Arrival_CheckedChanged(object sender, EventArgs e)
        {
            ///On efface la liste déja existante et on ajoute les postes correspondants
            cb_AllocType.Items.Clear();
            cb_AllocType.Items.Add("Transfer-Infeed");
            cb_AllocType.Items.Add("Baggage-Claim");
            ///On selectionne d'origine le poste Make-Up
            cb_AllocType.SelectedIndex = 0;
            ///On affiche maintenant les champs contenus dans la fenetre
            cb_FlightPlan.Enabled = cb_AirlineCT.Enabled = true;
            cb_AircraftCT.Enabled = cb_AirlineColor.Enabled = true;
            cb_TerminalInfo.Enabled = cb_TerminalObserved.Enabled = true;
            dtp_Begin.Enabled = dtp_End.Enabled = true;
            txt_TimeStep.Enabled = txt_Separation.Enabled = true;
            cb_FPAsBasis.Enabled = cb_AllocateFlightPlan.Enabled = true;
            gb_Colors.Enabled = gb_Segregation.Enabled = true;

            gb_StationOverlap.Enabled = (cb_AllocType.SelectedItem.ToString() != "Boarding-Gate");
            cb_AircraftCodeEx.Enabled = true;
            cb_RulesAlloc.Enabled = true;
            cb_RulesAllocationEx.Enabled = cb_RulesAlloc.Enabled;

            // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            cb_LoadFactorsTableName.Enabled = cb_LoadFactorsExc.Enabled = true;
            cb_NbBagsTableName.Enabled = cb_NbBagsExc.Enabled = true;            
            // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            fpParametersComboBox.Enabled = fpParametersCheckBox.Checked;    // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            fpParametersCheckBox.Enabled = true;

            // >> Task #12741 Pax2Sim - Dubai - flight exceptions                
            useDynamicOpeningTimes.Enabled = false;
            useDynamicOpeningTimes.Checked = false;

            dynamicOpeningTimesColumnName.Enabled = false;
            dynamicOpeningFPColumnsLabel.Enabled = false;
            // << Task #12741 Pax2Sim - Dubai - flight exceptions  
        }
        #endregion

        #region Change of the type of desk allocated
        /// <summary>
        /// Changement du type d'allocation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_AllocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size            
            List<String> values;
            List<String> loadFactorTableNamesList;
            ///Si l'allocation concerne un poste de départ alors on prend la table FPDTableName sinon FPATableName
            if (rb_Departure.Checked)
            {
                values = Donnees.getValidTables("Input", GlobalNames.FPDTableName);
                loadFactorTableNamesList = Donnees.getValidTables("Input", GlobalNames.FPD_LoadFactorsTableName);
            }
            else
            {
                values = Donnees.getValidTables("Input", GlobalNames.FPATableName);
                loadFactorTableNamesList = Donnees.getValidTables("Input", GlobalNames.FPA_LoadFactorsTableName);
            }
            // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size

            ///On supprime la liste cb_FlightPlan
            cb_FlightPlan.Items.Clear();
            ///On ajoute ensuite le plan de vol correspondant dans la liste
            cb_FlightPlan.Items.AddRange(values.ToArray());
            if (cb_FlightPlan.Items.Count > 0)
                cb_FlightPlan.SelectedIndex = 0;

            // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            cb_LoadFactorsTableName.Items.Clear();
            cb_LoadFactorsTableName.Items.AddRange(loadFactorTableNamesList.ToArray());
            if (cb_LoadFactorsTableName.Items.Count > 0)
                cb_LoadFactorsTableName.SelectedIndex = 0;
                        
            cb_LoadFactorsTableName.Enabled = cb_LoadFactorsExc.Enabled = true;
            cb_NbBagsTableName.Enabled = cb_NbBagsExc.Enabled = true;
            fpParametersComboBox.Enabled = fpParametersCheckBox.Checked;    // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            fpParametersCheckBox.Enabled = true;

            if (cb_AllocType.SelectedItem.ToString() == "Make-Up")
                cb_calculateBasedOnContainerSize.Enabled = true;
            else
            {
                cb_calculateBasedOnContainerSize.Enabled = false;
                cb_calculateBasedOnContainerSize.Checked = false;
            }
            // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size

            cb_FlightPlan.Enabled = cb_AirlineCT.Enabled = true;
            cb_AircraftCT.Enabled = cb_AirlineColor.Enabled = true;
            cb_AircraftCodeEx.Enabled = true;
            cb_TerminalInfo.Enabled = cb_TerminalObserved.Enabled = true;
            dtp_Begin.Enabled = dtp_End.Enabled = true;
            txt_TimeStep.Enabled = txt_Separation.Enabled = true;            
            cb_FPAsBasis.Enabled = cb_AllocateFlightPlan.Enabled = true;
            gb_Colors.Enabled = gb_Segregation.Enabled = true;
            cb_RulesAlloc.Items.Clear();
            cb_RulesAlloc.Enabled = true;
            cb_RulesAllocationEx.Enabled = cb_RulesAlloc.Enabled;

            cb_UseInfeed.Enabled = false;
            ///On récupère les noms de des tables correspondant à la selection et on ajoute dans dans la liste RulesAllocation 
            if (cb_AllocType.SelectedItem.ToString() == "Make-Up")                
                values = Donnees.getValidTables("Input", GlobalNames.OCT_MakeUpTableName);
            else if (cb_AllocType.SelectedItem.ToString() == "Check-In")
                values = Donnees.getValidTables("Input", GlobalNames.OCT_CITableName);
            else if (cb_AllocType.SelectedItem.ToString() == "Boarding-Gate")
                values = Donnees.getValidTables("Input", GlobalNames.OCT_BoardGateTableName);
            else if (cb_AllocType.SelectedItem.ToString() == "Bag-Drop")
                values = Donnees.getValidTables("Input", GlobalNames.OCT_CITableName);
            else if (cb_AllocType.SelectedItem.ToString() == "Parking") // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
                values = Donnees.getValidTables("Input", GlobalNames.OCT_ParkingTableName);
            /// Si l'item choisit est Transfer-Infeed alors on n'affiche pas la Rules Allocation
            else if (cb_AllocType.SelectedItem.ToString() == "Transfer-Infeed")
            {
                //cb_RulesAlloc.Enabled = false;
                values = Donnees.getValidTables("Input", GlobalNames.TransferInfeedAllocationRulesTableName);
            }
            else if (cb_AllocType.SelectedItem.ToString() == "Baggage-Claim")
            {
                values = Donnees.getValidTables("Input", GlobalNames.OCT_BaggageClaimTableName);
                cb_UseInfeed.Enabled = true;
            }
            if (!cb_UseInfeed.Enabled)
                cb_UseInfeed.Checked = false;

            ///Ajout des noms dans la liste 
            cb_RulesAlloc.Items.AddRange(values.ToArray());
            if (cb_RulesAlloc.Items.Count > 0)
                cb_RulesAlloc.SelectedIndex = 0;

            ///Si le poste choisit est Make-Up ou Boarding-Gate, on cache le groupe Box StationOverlap
            gb_StationOverlap.Enabled = (cb_AllocType.SelectedItem.ToString() != "Boarding-Gate");
            /*if (
                gb_StationOverlap.Enabled = false;
            else
                gb_StationOverlap.Enabled = true;*/

            ///Si le poste choisit est un poste d'arrivé ou Bording-Gate on cache empeche l'utilisation de Allocation Biggest Flight first
            if (rb_Arrival.Checked == true || cb_AllocType.SelectedItem.ToString() == "Boarding-Gate")
            {
                cb_Algorithme.Checked = false;
                cb_Algorithme.Enabled = false;
            }
            else
                cb_Algorithme.Enabled = true;

            UpdateFields();
            ///On met deux valeurs par défaut pour les champs  Time Step et delay between two flights
            txt_TimeStep.Text = Donnees.AllocationStep.ToString(); //"5";    // >> Bug #13367 Liege allocation
            txt_Separation.Text = "10";

            // >> Task #12741 Pax2Sim - Dubai - flight exceptions                
            useDynamicOpeningTimes.Enabled = (cb_AllocType.SelectedItem.ToString() == "Parking");
            useDynamicOpeningTimes.Checked = false;

            dynamicOpeningTimesColumnName.Enabled = (useDynamicOpeningTimes.Enabled && useDynamicOpeningTimes.Checked);
            dynamicOpeningFPColumnsLabel.Enabled = (useDynamicOpeningTimes.Enabled && useDynamicOpeningTimes.Checked);
            // << Task #12741 Pax2Sim - Dubai - flight exceptions  
        }
        #endregion

        #region Configuration for the infeed limitation
        private void cb_UseInfeed_CheckedChanged(object sender, EventArgs e)
        {
            gb_Segregation.Enabled = !cb_UseInfeed.Checked;
            btn_UseInfeed.Enabled = cb_UseInfeed.Checked;
            if (cb_UseInfeed.Checked)
            {
                cb_Algorithme.Checked = false;
                cb_LooseULD.Checked = false;
                cb_GroundHandlersSegregation.Checked = false;
                if ((sNumberBagsTableName == null) || (sNumberBagsTableName == ""))
                    sNumberBagsTableName = GlobalNames.NbBagsTableName;
                if ((sArrivalLoadFactorTableName == null) || (sArrivalLoadFactorTableName == ""))
                    sArrivalLoadFactorTableName = GlobalNames.FPA_LoadFactorsTableName;
                if (iNumberOfReclaimWithDoubleInfeed < 0)
                    iNumberOfReclaimWithDoubleInfeed = 0;
                if (dInfeedSpeed < 0)
                    dInfeedSpeed = 0.0;
                bUseExceptionArrivalLoadFactorTableName = true;
                bUseExceptionNumberBagsTableName = true;
            }
        }

        private void btn_UseInfeed_Click(object sender, EventArgs e)
        {
            Allocation.SIM_InfeedParameters sipInfeed = new Allocation.SIM_InfeedParameters(iNumberOfReclaimWithDoubleInfeed,
                                                                                            dInfeedSpeed);
            if (sipInfeed.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            
            iNumberOfReclaimWithDoubleInfeed = sipInfeed.DoubleInfeed;
            dInfeedSpeed = sipInfeed.InfeedSpeed;            
        }
        #endregion

        #region Lors d'un changement de plan de vol
        /// <summary>
        /// Changement du flight plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_FlightPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            fpTable = Donnees.getTable("Input", cb_FlightPlan.Text);
            ChangeDate(dtp_Begin,dtp_End);
            ChangeTerminalColumns(fpTable, cb_TerminalInfo);
        }

        private static void ChangeTerminalColumns(DataTable dtTable, ComboBox cbDest)
        {
            String sOldValue = cbDest.Text;
            cbDest.Items.Clear();
            cbDest.Text = "";
            for (int i = 0; i < dtTable.Columns.Count; i++)
            {
                if (dtTable.Columns[i].ColumnName.Contains("Terminal"))
                    cbDest.Items.Add(dtTable.Columns[i].ColumnName);
            }
            if (cbDest.Items.Contains(sOldValue))
                cbDest.Text = sOldValue;
            else
                if (cbDest.Items.Count > 0)
                    cbDest.SelectedIndex = 0;
        }

        private static void ChangeTerminalIndex(DataTable dtTable, String sColumn, ComboBox cbDest)
        {
            if (!dtTable.Columns.Contains(sColumn))
                return;
            String sOldValue = cbDest.Text;
            cbDest.Items.Clear();
            cbDest.Text = "";
            ArrayList alList = new ArrayList();
            foreach (DataRow drRow in dtTable.Rows)
            {
                String sValue = drRow[sColumn].ToString();
                if (alList.Contains(sValue))
                    continue;
                alList.Add(sValue);
            }
            alList.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
            cbDest.Items.AddRange(alList.ToArray());
            if (cbDest.Items.Contains(sOldValue))
                cbDest.Text = sOldValue;
            else
                if (cbDest.Items.Count > 0)
                    cbDest.SelectedIndex = 0;
        }

        private void ChangeDate(DateTimePicker dateBegin, DateTimePicker dateEnd)
        {
            if (fpTable == null)
            {
                return;
            }
            DateTime minFPD = new DateTime(),
                     maxFPD = new DateTime();
            bool bFPD = false;
            bFPD = findMinMaxValues(out minFPD, out maxFPD);
            if (!bFPD)
                return;

            dateBegin.MaxDate = DateTimePicker.MaximumDateTime;
            dateBegin.MinDate = DateTimePicker.MinimumDateTime;
            dateEnd.MinDate = DateTimePicker.MinimumDateTime;
            dateEnd.MaxDate = DateTimePicker.MaximumDateTime;

            dateBegin.Value = minFPD.Date;
            dateEnd.Value = maxFPD.Date.AddHours(23).AddMinutes(59);

            dateBegin.MaxDate = maxFPD.Date.AddDays(2);
            dateBegin.MinDate = minFPD.Date.AddDays(-1);
            dateEnd.MinDate = dateBegin.MinDate;
            dateEnd.MaxDate = dateBegin.MaxDate;
        }

        private bool findMinMaxValues(out DateTime min, out DateTime max)
        {

            if (fpTable == null)
            {
                min = DateTimePicker.MinimumDateTime;
                max = DateTimePicker.MinimumDateTime;
                return false;
            }
            max = OverallTools.DataFunctions.valeurMaximaleDansColonne(fpTable, 1, 2);
            min = OverallTools.DataFunctions.valeurMinimaleDansColonne(fpTable, 1, 2);
            if ((max == DateTime.MinValue) || (min == DateTime.MinValue))
                return false;
            return true;
        }
        #endregion

        #region Change of the rules for allocation
        /// <summary>
        /// Changement de rules allocation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_RulesAlloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllocationTable = Donnees.GetTable("Input", cb_RulesAlloc.Text);
        }
        #endregion

        #region Lors du changement du terminal observé
        private void cb_TerminalInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            fpTable = Donnees.getTable("Input", cb_FlightPlan.Text);
            ChangeTerminalIndex(fpTable, cb_TerminalInfo.Text, cb_TerminalObserved);
            updateDynamicOpeningTimesCombobox();    // >> Task #12741 Pax2Sim - Dubai - flight exceptions
        }
        #endregion

        #region Overlap Fonctions
        /// <summary>
        /// Bouton permettant l'ajout d'une station dans la liste des station overlap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add1_Click(object sender, EventArgs e)
        {
            bool bAlreadyExist = false;

            if (textBox2.Text == "")
                MessageBox.Show("Please write a name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                if (lgatOverlapInformations != null)
                {
                    foreach (GenerateAllocationTool.SegregationForOverlap sfoTmp in lgatOverlapInformations)
                    {
                        if (sfoTmp.Name == textBox2.Text)
                        {
                            bAlreadyExist = true;
                            break;
                        }
                    }
                }
                if (bAlreadyExist)
                    MessageBox.Show("Name already used", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ///Si un nom est fournit et qu'il est correct alors on l'ajoute dans la liste
                else
                {
                    if (lgatOverlapInformations == null)
                    {
                        lgatOverlapInformations = new List<GenerateAllocationTool.SegregationForOverlap>();
                    }
                    sfoCurrentOverlap = new GenerateAllocationTool.SegregationForOverlap(textBox2.Text, new List<String>(), 1);
                    lgatOverlapInformations.Add(sfoCurrentOverlap);
                    listBox1.Items.Add(textBox2.Text);
                    buserCheck = false;
                    cb_OverlapNum.SelectedIndex = 0;
                    buserCheck = true;
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
            }
            textBox2.Text = "";
        }

        /// <summary>
        /// Suppression d'une station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            buserCheck = false;
            int index = getIndexByName(listBox1.SelectedItem.ToString());
            if (index == -1)
            {
                buserCheck = true;
                return;
            }
            if (lgatOverlapInformations != null)
            {
                for (int i = 0; i < lgatOverlapInformations.Count; i++)
                {
                    if (lgatOverlapInformations[i].Name == listBox1.SelectedItem.ToString())
                    {                        
                        lgatOverlapInformations.RemoveAt(i);
                        break;
                    }
                }
            }
            if (listBox1.SelectedIndex != -1)
                listBox1.Items.RemoveAt(index);

            selectedFlightCategoriesList.Items.Clear();
            sfoCurrentOverlap = null;
            buserCheck = true;
        }
 
        /// <summary>
        /// Changement de selection de la liste des postes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            sfoCurrentOverlap = null;
            selectedFlightCategoriesList.Items.Clear();
            if (listBox1.SelectedIndex == -1)
                return;


            buserCheck = false;
            int i, j, index;
            index = getIndexByName(listBox1.SelectedItem.ToString());
            if (index == -1)
            {
                buserCheck = true;
                return;
            }



            sfoCurrentOverlap = lgatOverlapInformations[index];
            ///Si des catégories de vol sont déja utilisés pour le poste sélectionné alors on l'affiche
            if (sfoCurrentOverlap.FlightCategories != null)
            {
                for (i = 0; i < sfoCurrentOverlap.FlightCategories.Count; i++)
                {
                    selectedFlightCategoriesList.Items.Add(sfoCurrentOverlap.FlightCategories[i]);
                }
            }
            // >> Bug #11489 Pax2Sim - Allocation (static) - Allocation assistant - overlap selected index
            ///On affiche le nombre de vol possible par poste
            //cb_OverlapNum.SelectedIndex = sfoCurrentOverlap.MaxOverlap - 1;
            String selectedOverlap = "";
            if( sfoCurrentOverlap != null)
            {
                selectedOverlap = sfoCurrentOverlap.MaxOverlap.ToString();
            }
            if (cb_OverlapNum.Items.Contains(selectedOverlap))
            {
                cb_OverlapNum.SelectedItem = selectedOverlap;
            }
            // << Bug #11489 Pax2Sim - Allocation (static) - Allocation assistant - overlap selected index            
            buserCheck = true;
        }

        private void cb_OverlapNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            if (sfoCurrentOverlap == null)
                return;
            sfoCurrentOverlap.MaxOverlap = FonctionsType.getInt(cb_OverlapNum.SelectedItem);
        }

        private int getIndexByName(String Name)
        {
            if (lgatOverlapInformations == null)
                return -1;
            for (int i = 0; i < lgatOverlapInformations.Count; i++)
            {

                if (lgatOverlapInformations[i].Name == Name)
                    return i;
            }
            return -1;
        }
        #endregion

        #region Check Box modifications
        private void cb_FPAsBasis_CheckedChanged(object sender, EventArgs e)
        {
            // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            cb_GroundHandlersSegregation.Enabled = !cb_FPAsBasis.Checked;

            if (cb_GroundHandlersSegregation.Checked && cb_FPAsBasis.Checked)
            {
                cb_GroundHandlersSegregation.Checked = false;
            }

            cb_LooseULD.Enabled = !cb_FPAsBasis.Checked;
            if (cb_LooseULD.Checked && cb_FPAsBasis.Checked)
            {
                cb_LooseULD.Checked = false;
            }

            cbContinousSegregationNumerotation.Enabled = !cb_FPAsBasis.Checked;
            if (cbContinousSegregationNumerotation.Checked && cb_FPAsBasis.Checked)
                cbContinousSegregationNumerotation.Checked = false;

            /*
            cb_GroundHandlersSegregation.Enabled = !cb_FPAsBasis.Checked;
            if (cb_GroundHandlersSegregation.Checked && cb_FPAsBasis.Checked)
                cb_GroundHandlersSegregation.Checked = false;
            cb_LooseULD.Enabled = !cb_FPAsBasis.Checked;
            if (cb_LooseULD.Checked && cb_FPAsBasis.Checked)
                cb_LooseULD.Checked = false;
             */
            // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            
            // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
            firstIndexTextBox.Enabled = !cb_FPAsBasis.Checked;
            firstIndexLabel.Enabled = !cb_FPAsBasis.Checked;
            if (!firstIndexTextBox.Enabled)
                firstIndexTextBox.Text = "";
            // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
        }

        private void cb_LooseULD_CheckedChanged(object sender, EventArgs e)
        {
            // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
            if ((cb_GroundHandlersSegregation.Checked || cb_LooseULD.Checked))
            {
                cb_FPAsBasis.Enabled = false;
            }
            else
                cb_FPAsBasis.Enabled = true;

            if (cb_FPAsBasis.Checked && !cb_FPAsBasis.Enabled)
                cb_FPAsBasis.Checked = false;

            /*
            bool bEnabled = cb_GroundHandlersSegregation.Checked || cb_LooseULD.Checked;
            cb_FPAsBasis.Enabled = !bEnabled;
            if (cb_FPAsBasis.Checked && bEnabled)
                cb_FPAsBasis.Checked = false;
             */
            // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering            
        }
        #endregion

        #region Button Ok

        // >> Task #13366 Error message correction
        private bool isScenarioDateLengthAcceptable()
        {
            DateTime delayedStartDate = dtp_Begin.Value.AddDays(7);
            if (delayedStartDate < dtp_End.Value)
            {
                DialogResult res = MessageBox.Show("The simulation date range is greater than 7 days. The calculation could take some time. Do you want to continue?",
                                                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }
        // << Task #13366 Error message correction

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            ///Si aucun nom n'est fournit
            if (txt_AllocationName.Text.Length == 0)
            {
                MessageBox.Show("Please specify a title for the new scenario", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            List<String> lsTmp = Donnees.getScenarioAllocation();
            List<String> lsUsedNames= Donnees.getScenarioNames();
            if (((lsTmp==null)||(!lsTmp.Contains(txt_AllocationName.Text))) && (lsUsedNames.Contains(txt_AllocationName.Text)))
            {
                MessageBox.Show("The name for your scenario is already used. Please specify a new one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            /// Si aucun poste n'est fournit
            if (cb_AllocType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a allocation station", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            /// Si aucun plan de vol n'est fournit
            if (cb_FlightPlan.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a flight plan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            /// Si aucune table Airline n'est fournit
            if (cb_AirlineCT.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Airline table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            /// Si aucune table Aircraft n'est fournit
            if (cb_AircraftCT.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Aircraft table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            if (cb_LoadFactorsTableName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Load Factors table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (cb_NbBagsTableName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Number of Baggage table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            if (fpParametersCheckBox.Checked && fpParametersComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a \"" + FPParametersTableConstants.TABLE_DISPLAY_NAME + "\" table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            // >> Task #13366 Error message correction
            if (!isScenarioDateLengthAcceptable())
            {
                DialogResult = DialogResult.None;
                return;
            }
            // << Task #13366 Error message correction

            DataManagement.NormalTable dtAircraft = Donnees.GetTable("Input", cb_AircraftCT.Text);
            DataTable dtAirline = Donnees.getTable("Input", cb_AirlineCT.Text);

            // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            DataManagement.NormalTable dtLoadFactorsTable = Donnees.GetTable("Input", cb_LoadFactorsTableName.Text);
            DataManagement.NormalTable dtNbBagsTable = Donnees.GetTable("Input", cb_NbBagsTableName.Text);
            // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
            DataTable fpParametersTable = Donnees.getTable("Input", fpParametersComboBox.Text); // >> Task #17690 PAX2SIM - Flight Plan Parameters table

            String sTerminal = cb_TerminalObserved.Text;
            if (Donnees.UseAlphaNumericForFlightInfo)
            {
                System.Xml.XmlNode xnTerminal = GestionDonneesHUB2SIM.getTerminalByDescription(Donnees.getRacine(), sTerminal);
                if (xnTerminal == null)
                    sTerminal = "";
                else
                    sTerminal = xnTerminal.Attributes["Index"].Value;
            }
            int iTerminal;
            if ((cb_TerminalObserved.SelectedIndex == -1)
                || (((!Int32.TryParse(sTerminal, out iTerminal)) || (iTerminal == 0)))
                )
            {
                MessageBox.Show("Please select a valid terminal.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            Double dTimeStep;
            if ((txt_TimeStep.Text.Length == 0) || (!Double.TryParse(txt_TimeStep.Text, out dTimeStep)) || (dTimeStep <= 0))
            {
                MessageBox.Show("Please specify a valid step", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            Double dTimeSeparation;
            if ((txt_Separation.Text.Length == 0) || (!Double.TryParse(txt_Separation.Text, out dTimeSeparation)) || (dTimeSeparation < 0))
            {
                MessageBox.Show("Please specify a valid separation between 2 fligts", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            List<String> alErrors = new List<String>();
            try
            {
                if (Donnees.UseAlphaNumericForFlightInfo)
                {
                    if (rb_Departure.Checked)
                        GestionDonneesHUB2SIM.ConvertFPDInformations(fpTable, Donnees.getRacine(), false);
                    else
                        GestionDonneesHUB2SIM.ConvertFPAInformations(fpTable, Donnees.getRacine(), false);
                }

                ///Si le poste d'allocation est un make up
                if (cb_AllocType.SelectedItem.ToString() == "Make-Up")
                {
                    #region Make Up
                    /// On initialise la variable gatAllocation avec les parametres fournit
                    gatAllocation
                        = new Classes.GenerateAllocationTool(txt_AllocationName.Text,
                            Classes.GenerateAllocationTool.TypeAllocation.MakeUpAllocation,
                            "Make-Up",
                            fpTable,
                            AllocationTable,
                            dtAirline,
                            dtAircraft,
                            dtp_Begin.Value,
                            dtp_End.Value,
                            dTimeStep,
                            dTimeSeparation);


                    gatAllocation.sOpeningOCTDesk = GlobalNames.sOCT_MakeUpOpening;
                    gatAllocation.sClosingOCTDesk = GlobalNames.sOCT_MakeUpClosing;
                    gatAllocation.sNumberOCTDesk = GlobalNames.sOCT_MakeUpAllocatedMup;
                    gatAllocation.sPartialClosingOCTDesk = GlobalNames.sOCT_MakeUpPartialOpening;

                    gatAllocation.sPartialNumberOCTDesk = GlobalNames.sOCT_MakeUpPartialAllocatedMup;


                    gatAllocation.sColumnTerminalResult = GlobalNames.sFPD_Column_TerminalMup;
                    gatAllocation.sColumnFirstColumnResult = GlobalNames.sFPD_Column_Eco_Mup_Start;
                    gatAllocation.sColumnLastColumnResult = GlobalNames.sFPD_Column_Eco_Mup_End;

                    gatAllocation.sColumnFirstColumnResultFirst = GlobalNames.sFPD_Column_First_Mup_Start;
                    gatAllocation.sColumnLastColumnResultFirst = GlobalNames.sFPD_Column_First_Mup_End;

                    // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                    
                    gatAllocation.sNBContainerPerCycle = GlobalNames.sOCT_MakeUpNumberContainerLateral;
                    gatAllocation.sDeadTimeBetweenCycle = GlobalNames.sOCT_MakeUpDeadTime;
                    gatAllocation.sContainerSize = GlobalNames.sOCT_MakeUpContainerSize;

                    gatAllocation.calculateBasedOnContainerSize = cb_calculateBasedOnContainerSize.Checked;
                    // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                    #endregion
                }
                else if (cb_AllocType.SelectedItem.ToString() == "Parking")// >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
                {
                    #region Parking 
                    /// On initialise la variable gatAllocation avec les parametres fournit
                    gatAllocation
                        = new Classes.GenerateAllocationTool(txt_AllocationName.Text,
                            Classes.GenerateAllocationTool.TypeAllocation.Parking,
                            "Parking",
                            fpTable,
                            AllocationTable,
                            dtAirline,
                            dtAircraft,
                            dtp_Begin.Value,
                            dtp_End.Value,
                            dTimeStep,
                            dTimeSeparation);


                    gatAllocation.sOpeningOCTDesk = GlobalNames.sOCT_ParkingOpening;
                    gatAllocation.sClosingOCTDesk = GlobalNames.sOCT_ParkingClosing;
                    
                    gatAllocation.sColumnTerminalResult = GlobalNames.sFPD_A_Column_TerminalParking;
                    gatAllocation.sColumnFirstColumnResult = GlobalNames.sFPD_A_Column_Parking;
                    gatAllocation.sColumnLastColumnResult = GlobalNames.sFPD_A_Column_Parking;

                    // >> Task #12741 Pax2Sim - Dubai - flight exceptions
                    gatAllocation.useDynamicOpeningTimes = useDynamicOpeningTimes.Checked;                    
                    gatAllocation.openingTimeFlightPlanColumnName = dynamicOpeningTimesColumnName.Text;
                    // << Task #12741 Pax2Sim - Dubai - flight exceptions
                    #endregion
                }
                else if (cb_AllocType.SelectedItem.ToString() == "Check-In")
                {
                    #region Check-In
                    gatAllocation
                       = new Classes.GenerateAllocationTool(txt_AllocationName.Text,
                           Classes.GenerateAllocationTool.TypeAllocation.CheckInAllocation,
                           "Check-In",
                           fpTable,
                           AllocationTable,
                           dtAirline,
                           dtAircraft,
                           dtp_Begin.Value,
                           dtp_End.Value,
                           dTimeStep,
                           dTimeSeparation);

                    gatAllocation.sOpeningOCTDesk = GlobalNames.sOCT_CI_Line_Opening;
                    gatAllocation.sClosingOCTDesk = GlobalNames.sOCT_CI_Line_Closing;
                    gatAllocation.sNumberOCTDesk = GlobalNames.sOCT_CI_Line_Allocated;

                    gatAllocation.sColumnTerminalResult = GlobalNames.sFPD_Column_TerminalCI;
                    gatAllocation.sColumnFirstColumnResult = GlobalNames.sFPD_Column_Eco_CI_Start;
                    gatAllocation.sColumnLastColumnResult = GlobalNames.sFPD_Column_Eco_CI_End;

                    gatAllocation.sColumnFirstColumnResultFirst = GlobalNames.sFPD_Column_FB_CI_Start;
                    gatAllocation.sColumnLastColumnResultFirst = GlobalNames.sFPD_Column_FB_CI_End;
                    
                    #endregion
                }
                else if (cb_AllocType.SelectedItem.ToString() == "Boarding-Gate")
                {
                    #region Boarding Gate
                    gatAllocation
                       = new Classes.GenerateAllocationTool(txt_AllocationName.Text,
                           Classes.GenerateAllocationTool.TypeAllocation.BoardingGateAllocation,
                           "Boarding-Gate",
                           fpTable,
                           AllocationTable,
                           dtAirline,
                           dtAircraft,
                           dtp_Begin.Value,
                           dtp_End.Value,
                           dTimeStep,
                           dTimeSeparation);

                    gatAllocation.sOpeningOCTDesk = GlobalNames.sOCT_Board_Line_Opening;
                    gatAllocation.sClosingOCTDesk = GlobalNames.sOCT_Board_Line_Closing;

                    gatAllocation.sColumnTerminalResult = GlobalNames.sFPD_A_Column_TerminalGate;
                    gatAllocation.sColumnFirstColumnResult = GlobalNames.sFPD_Column_BoardingGate;
                    gatAllocation.sColumnLastColumnResult = GlobalNames.sFPD_Column_BoardingGate;
                    #endregion
                }
                else if (cb_AllocType.SelectedItem.ToString() == "Bag-Drop")
                {
                    #region Bag-Drop
                    gatAllocation
                        = new Classes.GenerateAllocationTool(txt_AllocationName.Text,
                            Classes.GenerateAllocationTool.TypeAllocation.CheckInAllocation,
                            "Bag-Drop",
                            fpTable,
                            AllocationTable,
                            dtAirline,
                            dtAircraft,
                            dtp_Begin.Value,
                            dtp_End.Value,
                            dTimeStep,
                            dTimeSeparation);

                    gatAllocation.sOpeningOCTDesk = GlobalNames.sOCT_CI_Line_Opening;
                    gatAllocation.sClosingOCTDesk = GlobalNames.sOCT_CI_Line_Closing;


                    gatAllocation.sColumnTerminalResult = GlobalNames.sFPD_Column_TerminalCI;
                    gatAllocation.sColumnFirstColumnResult = GlobalNames.sFPD_Column_Eco_Drop_Start;
                    gatAllocation.sColumnLastColumnResult = GlobalNames.sFPD_Column_Eco_Drop_End;

                    gatAllocation.sColumnFirstColumnResultFirst = GlobalNames.sFPD_Column_FB_Drop_Start;
                    gatAllocation.sColumnLastColumnResultFirst = GlobalNames.sFPD_Column_FB_Drop_End;
                    #endregion
                }
                else if (cb_AllocType.SelectedItem.ToString() == "Transfer-Infeed")
                {
                    #region Transfer Infeed
                    gatAllocation
                        = new Classes.GenerateAllocationTool(txt_AllocationName.Text,
                            Classes.GenerateAllocationTool.TypeAllocation.TransferInfeedAllocation,
                            "Transfer-Infeed",
                            fpTable,
                            AllocationTable,
                            dtAirline,
                            dtAircraft,
                            dtp_Begin.Value,
                            dtp_End.Value,
                            dTimeStep,
                            dTimeSeparation);

                    gatAllocation.sOpeningOCTDesk = GlobalNames.sOCT_TransferInfeedOpening;
                    gatAllocation.sClosingOCTDesk = GlobalNames.sOCT_TransferInfeedClosing;
                    gatAllocation.sNumberOCTDesk = GlobalNames.sOCT_TransferInfeedAllocatedInfeed;

                    gatAllocation.sColumnTerminalResult = GlobalNames.sFPA_Column_TerminalInfeedObject;
                    gatAllocation.sColumnFirstColumnResult = GlobalNames.sFPA_Column_TransferInfeedObject;
                    gatAllocation.sColumnLastColumnResult = GlobalNames.sFPA_Column_TransferInfeedObject;
                    #endregion
                }
                else// Baggage claim
                {
                    #region Baggage Claim
                    gatAllocation
                    = new Classes.GenerateAllocationTool(txt_AllocationName.Text,
                        Classes.GenerateAllocationTool.TypeAllocation.ReclaimAllocation,
                        "Baggage-Claim",
                        fpTable,
                        AllocationTable,
                        dtAirline,
                        dtAircraft,
                        dtp_Begin.Value,
                        dtp_End.Value,
                        dTimeStep,
                        dTimeSeparation);
                    if (cb_UseInfeed.Checked)   // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                    {       
                        gatAllocation.dInfeedSpeed = dInfeedSpeed;
                        gatAllocation.iNumberOfDoubleInfeed = iNumberOfReclaimWithDoubleInfeed;

                        gatAllocation.sLoadFactor = GlobalNames.sLFD_A_Line_Full;
                        gatAllocation.sContainerSize = GlobalNames.sOCT_Baggage_Line_ContainerSize;
                        gatAllocation.sNBContainerPerCycle = GlobalNames.sOCT_Baggage_Line_NumberProcessedContainer ;
                        gatAllocation.sDeadTimeBetweenCycle = GlobalNames.sOCT_Baggage_Line_DeadTime;
                    }

                    gatAllocation.sOpeningOCTDesk = GlobalNames.sOCT_Baggage_Line_Opening;
                    gatAllocation.sClosingOCTDesk = GlobalNames.sOCT_Baggage_Line_Closing;

                    gatAllocation.sColumnTerminalResult = GlobalNames.sFPA_Column_TerminalReclaim;
                    gatAllocation.sColumnFirstColumnResult = GlobalNames.sFPA_Column_ReclaimObject;
                    gatAllocation.sColumnLastColumnResult = GlobalNames.sFPA_Column_ReclaimObject;
                    // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size                                       
                    gatAllocation.useInfeedLimitation = cb_UseInfeed.Checked;
                    // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                    #endregion
                }
                if (rb_Departure.Checked)
                {
                    gatAllocation.sColumnDate = GlobalNames.sFPD_A_Column_DATE;
                    gatAllocation.sColumnTime = GlobalNames.sFPD_Column_STD;
                }
                else
                {
                    gatAllocation.sColumnDate = GlobalNames.sFPD_A_Column_DATE;
                    gatAllocation.sColumnTime = GlobalNames.sFPA_Column_STA;
                }
                gatAllocation.ShowFlightNumber = true;

                gatAllocation.UseAircraftTableExceptions = cb_AircraftCodeEx.Checked;
                gatAllocation.UseOCTTableExceptions = cb_RulesAllocationEx.Checked;
                gatAllocation.OverlapParameters = lgatOverlapInformations;

                ///nombre de vols par terminal 
                gatAllocation.Terminal = iTerminal;
                gatAllocation.AllocateFlightPlan = cb_AllocateFlightPlan.Checked;
                gatAllocation.ColorByFC = cb_ColorFC.Checked;
                gatAllocation.ColorByHandler = cb_ColorHandlers.Checked;
                gatAllocation.ColorByAirline = cb_AirlineColor.Checked;
                gatAllocation.FCAllocation = cb_Algorithme.Checked;
                gatAllocation.ObservedTerminal = cb_TerminalInfo.Text;
                gatAllocation.SegregateLooseULD = cb_LooseULD.Checked;
                gatAllocation.UseFPContent = cb_FPAsBasis.Checked;
                gatAllocation.Errors = alErrors;


                gatAllocation.sColumnID = GlobalNames.sFPD_A_Column_ID;
                gatAllocation.sColumnFLIGHTN = GlobalNames.sFPD_A_Column_FlightN;

                gatAllocation.sColumnFlightCategory = GlobalNames.sFPD_A_Column_FlightCategory;
                gatAllocation.sColumnAircraft = GlobalNames.sFPD_A_Column_AircraftType;
                gatAllocation.sColumnAirline = GlobalNames.sFPD_A_Column_AirlineCode;

                gatAllocation.sColumnAircraftAircraft = GlobalNames.sFPAircraft_AircraftTypes;
                gatAllocation.sColumnAircraftULDLoose = GlobalNames.sTableColumn_ULDLoose;
                gatAllocation.sColumnAirlineAirline = GlobalNames.sFPAirline_AirlineCode;
                gatAllocation.sColumnAirlineGroundHandlers = GlobalNames.sFPAirline_GroundHandlers;

                gatAllocation.SegregateGroundHandlers = cb_GroundHandlersSegregation.Checked;
                // << Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
                gatAllocation.ContinousSegregationNumerotation = cbContinousSegregationNumerotation.Checked;
                // >> Task #8907 Pax2Sim - Allocation algorithm - continuous numbering
                // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource
                int firstIndexForAllocation = 1;
                if (Int32.TryParse(firstIndexTextBox.Text, out firstIndexForAllocation))
                    gatAllocation.FirstIndexForAllocation = firstIndexForAllocation;
                // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource

                gatAllocation.IsFPAFlight = rb_Arrival.Checked;

                // << Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                gatAllocation.dtLoadFactors = (DataManagement.ExceptionTable)dtLoadFactorsTable;
                gatAllocation.UseLoadFactorExceptions = cb_LoadFactorsExc.Checked;

                gatAllocation.dtNbBagage = (DataManagement.ExceptionTable)dtNbBagsTable;
                gatAllocation.UseNbBaggageExceptions = cb_NbBagsExc.Checked;                                
                // >> Task #9440 Pax2Sim - Allocation - Calculate dynamically the nb of positions based on container size
                gatAllocation.flightPlanParametersTable = fpParametersTable;

                if (gatAllocation.AllocateDesks())
                {
                    dtResults = gatAllocation.TableResultat;
                    vmResults = gatAllocation.AllocationVisualisation;
                }
                if (Donnees.UseAlphaNumericForFlightInfo)
                    GestionDonneesHUB2SIM.ConvertFPDInformations(fpTable, Donnees.getRacine(), true);
            }
            catch (Exception excep)
            {
                dtResults = null;
                alErrors.Clear();
                alErrors.Add("Err00708 : Some errors appear during the execution : " + excep.Message);
                OverallTools.ExternFunctions.PrintLogFile("Err00708: (Some errors appear during the execution) " + this.GetType().ToString() + " throw an exception: " + excep.Message);

                if (Donnees.UseAlphaNumericForFlightInfo)
                    GestionDonneesHUB2SIM.ConvertFPDInformations(fpTable, Donnees.getRacine(), true);
            }
            if (dtResults == null)
            {
                String Error = "";
                if (alErrors.Count > 0)
                    Error = alErrors[0].ToString();

                MessageBox.Show("An error occurred during the analysis of the flight plan table " + Error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (alErrors.Count > 0)
            {
                if (MessageBox.Show("Errors happend during the allocations. These errors would be accessible in the table \"" + gatAllocation.TableErrors.TableName + "\". Do you want to continue ?", "Errors", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    Errors err = new Errors(new ArrayList(alErrors.ToArray()));
                    err.Show();//err.ShowDialog();  // >> Task #10347 Pax2Sim - Errors window - modeless
                }
            }
        }
        #endregion

        // << Task #8915 Pax2Sim - Allocation algorithm - First index for Resource        
        private void firstIndexTextBox_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = (e.KeyChar != (char)Keys.Back);
            }
        }
        // >> Task #8915 Pax2Sim - Allocation algorithm - First index for Resource

        // >> Task #12741 Pax2Sim - Dubai - flight exceptions        
        private void useDynamicOpeningTimes_CheckedChanged(object sender, EventArgs e)
        {
            if (useDynamicOpeningTimes.Checked)
            {
                dynamicOpeningFPColumnsLabel.Enabled = true;
                dynamicOpeningTimesColumnName.Enabled = true;
                updateDynamicOpeningTimesCombobox();
            }
            else
            {
                dynamicOpeningTimesColumnName.Items.Clear();
                dynamicOpeningTimesColumnName.Enabled = false;
                dynamicOpeningFPColumnsLabel.Enabled = false;
            }
        }
        private void updateDynamicOpeningTimesCombobox()
        {
            if (fpTable != null)
            {
                dynamicOpeningTimesColumnName.Items.Clear();
                List<string> possibleColumnNames
                    = getPossibleOpeningTimeColumnsFromFlightPlan(fpTable);
                dynamicOpeningTimesColumnName
                    .Items.AddRange(possibleColumnNames.ToArray());
            }
        }
        private List<string> getPossibleOpeningTimeColumnsFromFlightPlan(DataTable flightPlan)
        {
            List<string> possibleOpeningTimeColumnNames = new List<string>();
            if (flightPlan != null)
            {
                int columnIndex = -1;
                
                columnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                if (columnIndex != -1)
                    possibleOpeningTimeColumnNames.Add(GlobalNames.sFPD_A_Column_User1);

                columnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                if (columnIndex != -1)
                    possibleOpeningTimeColumnNames.Add(GlobalNames.sFPD_A_Column_User2);

                columnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                if (columnIndex != -1)
                    possibleOpeningTimeColumnNames.Add(GlobalNames.sFPD_A_Column_User3);

                columnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                if (columnIndex != -1)
                    possibleOpeningTimeColumnNames.Add(GlobalNames.sFPD_A_Column_User4);

                columnIndex = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
                if (columnIndex != -1)
                    possibleOpeningTimeColumnNames.Add(GlobalNames.sFPD_A_Column_User5);

                // if we have a filter from a flight plan we might have extra added columns
                if (flightPlan.Columns.Count - 1 > columnIndex)
                {
                    for (int i = columnIndex + 1; i <= flightPlan.Columns.Count - 1; i++)
                    {
                        DataColumn column = flightPlan.Columns[i];
                        possibleOpeningTimeColumnNames.Add(column.ColumnName);
                    }
                }
            }
            return possibleOpeningTimeColumnNames;
        }
        // << Task #12741 Pax2Sim - Dubai - flight exceptions

        // >> Task #16728 PAX2SIM Improvements (Recurring) C#7
        private void selectFlightCategoriesButton_Click(object sender, EventArgs e)
        {
            List<string> selectedFlightCategories = getSelectedItemsAsStringList(selectedFlightCategoriesList);
            MultiSelectionPopUp multiSelector = new MultiSelectionPopUp(dbFlightCategories, selectedFlightCategories);
            if (multiSelector.ShowDialog() == DialogResult.OK)
            {
                if (sfoCurrentOverlap == null)
                    return;
                if (sfoCurrentOverlap.FlightCategories == null)
                    sfoCurrentOverlap.FlightCategories = new List<string>();

                sfoCurrentOverlap.FlightCategories.Clear();
                selectedFlightCategoriesList.Items.Clear();
                foreach (string flightCategory in multiSelector.selectedItems)
                {
                    //update overlap flight categories
                    sfoCurrentOverlap.FlightCategories.Add(flightCategory);
                    //update selected flight categories list
                    selectedFlightCategoriesList.Items.Add(flightCategory);
                }
            }
        }

        private List<string> getSelectedItemsAsStringList(ListBox selectedItemList)
        {
            List<string> items = new List<string>();
            if (selectedItemList == null)
            {
                return items;
            }
            foreach (object selectedItem in selectedItemList.Items)
            {
                if (selectedItem != null && selectedItem.ToString() != null)
                {
                    items.Add(selectedItem.ToString());
                }
            }
            return items;
        }

        private void fpParametersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            fpParametersComboBox.Enabled = fpParametersCheckBox.Checked;
            if (fpParametersCheckBox.Checked)
            {                
                List<string> values = Donnees.getValidTables("Input", FPParametersTableConstants.TABLE_TECHNICAL_NAME);

                fpParametersComboBox.Items.Clear();
                fpParametersComboBox.Items.AddRange(values.ToArray());
                if (fpParametersComboBox.Items.Count > 0)
                {
                    fpParametersComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                fpParametersComboBox.Text = "";
                fpParametersComboBox.Items.Clear();
            }   
        }
        // << Task #16728 PAX2SIM Improvements (Recurring) C#7
    }
}
