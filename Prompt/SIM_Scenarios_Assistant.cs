#define NEWUSERDATA
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.com.crispico.BHS_Analysis;
using Microsoft.Win32;
using System.IO;
using PresentationControls;
using SIMCORE_TOOL.com.crispico.generalUse;
using SIMCORE_TOOL.com.crispico.FlightPlansUtils;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Scenarios_Assistant : Form, SIM_LoadingForm
    {
        #region Variables
        /// <summary>
        /// The data manager information. Use to access to any needed information about the current project.
        /// </summary>
        GestionDonneesHUB2SIM Donnees;

        /// <summary>
        /// The current \ref ParamScenario that represent the current simulation / analysis that the use want to do.
        /// </summary>
        ParamScenario SourceAnalysis;

        // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update        
        DateTime scenarioCreationDate;
        DateTime scenarioLastUpdateDate;
        // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

        /// <summary>
        /// The different TabPages that can be shown in the form.
        /// </summary>
        private TabPage[] ttpOrderTabs;

        /// <summary>
        /// The airport structure.
        /// </summary>
        TreeNode tnAirportNode_;

        /// <summary>
        /// The error form shown in case of errors on the input tables.
        /// </summary>
        Errors ErrForm;

        /// <summary>
        /// The current perimeter of PAX2SIM : PAX / BHS ...
        /// </summary>
        PAX2SIM.EnumPerimetre epPerimetre_;

        /// <summary>
        /// The default scenario name.
        /// </summary>
        public static String DefaultScenarioName = "Scenario_";

        /// <summary>
        /// A boolean indicating if the user is currently in a bhs mean flow mode (limitation in some options)
        /// </summary>
        private bool bBHSMeanFlow;
        /// <summary>
        /// A boolean indicating if the user is currently in a static mode (limitation in some options)
        /// </summary>
        private bool bStatic_;
        /// <summary>
        /// A boolean indicating if the user is currently in a dynamic mode (limitation in some options)
        /// </summary>
        private bool bDynamic_;
        /// <summary>
        /// A boolean indicating if the BHS process is enable. that part is currently not available to users. Because the
        /// BHS itinerary is not valid.
        /// </summary>
        private bool bBHSProcessEnabled;

        private bool isTraceAnalysisScenario;   // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues

        private string bagTracePath = "";   // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17

        // >> Task #13361 FP AutoMod Data tables V3 
        private List<DataTable> _automodTablesList = new List<DataTable>();
        internal List<DataTable> automodTablesList
        {
            get { return _automodTablesList; }
            set { _automodTablesList = value; }
        }
        // << Task #13361 FP AutoMod Data tables V3

        private List<AnalysisResultFilter> temporaryCustomResultsFilters = new List<AnalysisResultFilter>();
        private List<AnalysisResultFilter> temporaryLinkedResultsFilters = new List<AnalysisResultFilter>();

        private List<string> flowTypes = new List<string>();    // >> Task #15683 PAX2SIM - Result Filters - Split by Flow Type                
        #endregion

        private ParamScenario currentParameters;

        #region Fonction pour initialiser le formulaire
        internal void initialiseForm(GestionDonneesHUB2SIM Donnees_,
                                   PAX2SIM.EnumPerimetre epPerimeters,
                                   TreeNode AirportNode,
                                   bool bStatic,
                                   bool bDynamic,
                                   Errors ErrForm_,
                                   ParamScenario scenarioParams)    // >> Task #16728 PAX2SIM Improvements (Recurring) C#1
        {
            InitializeComponent();

            ///We set the default value for the dynamic reclaim allocation to No (to ensure there would be no problem).
            cb_SIM_DynReclaimTerminal.Text = "No";


            #region Gestion des tags
            ///We update the tags of all the differents combobox of the form to the actual TableName to which they are linked.
            this.cb_D_FPD.Tag = GlobalNames.FPDTableName;
            this.cb_FPATable.Tag = GlobalNames.FPATableName;
            this.cb_D_AircraftType.Tag = GlobalNames.FP_AircraftTypesTableName;
            this.cb_St_Airline.Tag = GlobalNames.FP_AirlineCodesTableName;
            this.cb_D_FC.Tag = GlobalNames.FP_FlightCategoriesTableName;
            this.cb_D_LFD.Tag = GlobalNames.FPD_LoadFactorsTableName;
            this.cb_A_LoadFactors.Tag = GlobalNames.FPA_LoadFactorsTableName;
            this.cb_A_InterConnectingTimes.Tag = GlobalNames.Transfer_ICTTableName;
            // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            this.cb_A_BaggageLoadingRate.Tag = GlobalNames.sUserAttributesBaggLoadingRateTableName;
            this.cb_A_BaggageLoadingDelay.Tag = GlobalNames.sUserAttributesBaggLoadingDelayTableName;
            // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            this.reclaimSyncComboBox.Tag = GlobalNames.userAttributesReclaimLogTableName;
            this.cb_TransTerm.Tag = GlobalNames.Transfer_TerminalDitributionTableName;
            this.cb_PP_TransferFCTable.Tag = GlobalNames.Transfer_FlightCategoryDitributionTableName;
            this.cb_D_ShowUp.Tag = GlobalNames.CI_ShowUpTableName;
            this.cb_D_NbBags.Tag = GlobalNames.NbBagsTableName;
            this.cb_D_NbVisitors.Tag = GlobalNames.NbVisitorsTableName;
            this.cb_D_NbTrolley.Tag = GlobalNames.NbTrolleyTableName;
            this.cb_D_AircraftLinks.Tag = GlobalNames.FPLinksTableName;
            this.cb_PP_PaxIn.Tag = GlobalNames.ProcessDistributionPaxInName;
            this.cb_PP_PaxOut.Tag = GlobalNames.ProcessDistributionPaxOutName;
            cb_PF_ProcessSchedule.Tag = GlobalNames.ProcessScheduleName;
            // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            cb_d_EBSInputRate.Tag = GlobalNames.userAttributesEBSInputRateTableName;
            cb_d_EBSOutputRate.Tag = GlobalNames.userAttributesEBSOutputRateTableName;
            // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            cb_D_OCT_BagDrop.Tag = GlobalNames.OCT_BaggDropTableName;   // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop

            #region 26/03/2012 - SGE - Parking Mulhouse
            this.cb_PP_DwellTime.Tag = GlobalNames.Parking_LongStayTableName;
            this.cb_PP_DwellTimeVis.Tag = GlobalNames.Parking_ShortStayTableName;
            this.cb_PP_InitialOccupation.Tag = GlobalNames.Parking_InitialStateTableName;
            #endregion //26/03/2012 - SGE - Parking Mulhouse

            flightSubcategoriesComboBox.Tag = GlobalNames.flightSubcategoriesTableName;     // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            nbOfPaxComboBox.Tag = GlobalNames.numberOfPassengersTableName;
            nbOfBagsComboBox.Tag = GlobalNames.numberOfBaggagesTableName;
            // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            fpParametersTableComboBox.Tag = FPParametersTableConstants.TABLE_TECHNICAL_NAME;    // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            usaStandardParamsComboBox.Tag = GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME; // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            #endregion

#if(DEBUG)
            ///We enable the Bag2sim process if the model in resources is compatible.
            bBHSProcessEnabled = System.IO.File.Exists(Application.StartupPath + "\\Ressources\\hub2sim\\" + "bag2sim.mod");
#else
            bBHSProcessEnabled = false;
#endif            
            cb_SamplingStep.Text = "15";
            cb_AnalysisRange.Text = "60";
            cb_StatisticStep.Text = "15";
            cb_StStepPolitic.SelectedIndex = cb_StStepPolitic.Items.Count - 1;
            // >> Task #13761 Pax2Sim - Static Analysis - Instant and Smoothed calculations
            cb_StatisticStep.Visible = false;
            statisticStepLabel.Visible = false;

            cb_StStepPolitic.Visible = false;
            lbl_Per.Visible = false;
            // << Task #13761 Pax2Sim - Static Analysis - Instant and Smoothed calculations

            mb_PP_UseDefinedSeed.BackColor = Color.White;
            //mffFileSeedMaskedTextBox.BackColor = Color.White;   // >> Bug #14900 MFF file not created
            OverallTools.FonctionUtiles.MajBackground(this);

            epPerimetre_ = epPerimeters;
            tnAirportNode_ = AirportNode;
            Donnees = Donnees_;
            bStatic_ = bStatic;
            bDynamic_ = bDynamic;

            #region for the tabs (we remove all of them exception the general one).
            ttpOrderTabs = new TabPage[tc_General.TabPages.Count];
            for (int i = 0; i < tc_General.TabPages.Count; i++)
            {
                ttpOrderTabs[i] = tc_General.TabPages[i];
            }
            tc_General.TabPages.Remove(tp_SIM_Start);
            tc_General.TabPages.Remove(tp_BHS);
            tc_General.TabPages.Remove(tp_resultsFilters);
            tc_General.TabPages.Remove(tp_UserData);
            tc_General.TabPages.Remove(tp_Static);
            tc_General.TabPages.Remove(tp_PaxPlan);
            tc_General.TabPages.Remove(tp_Dynamic);
            #endregion

            ///We set the \ref SourceAnalysis to null
            SourceAnalysis = null;
            ErrForm = ErrForm_;

            #region We get all the scenario names that are valid PAX/BHS scenarios.
            foreach (String name in Donnees.getScenarioNames())
            {
                if (PAX2SIM.bTrialVersion && cb_Scenario.Items.Count >= GestionDonneesHUB2SIM.NBScenarios)
                    continue;
                if (Donnees.GetScenario(name) == null)
                    continue;
                if ((!((Donnees.GetScenario(name).BHSSimulation) && epPerimetre_ == PAX2SIM.EnumPerimetre.PAX))
                    || (epPerimetre_ == PAX2SIM.EnumPerimetre.BHS) || (epPerimetre_ == PAX2SIM.EnumPerimetre.TMS))
                    cb_Scenario.Items.Add(name);
            }
            #endregion
            #region Trial version limitation
            if ((PAX2SIM.bTrialVersion && (cb_Scenario.Items.Count >= GestionDonneesHUB2SIM.NBScenarios)) || PAX2SIM.bRuntime)
                cb_Scenario.DropDownStyle = ComboBoxStyle.DropDownList;
            #endregion

            #region For BHS / TMS parameters
            if ((epPerimetre_ == PAX2SIM.EnumPerimetre.BHS) || (epPerimetre_ == PAX2SIM.EnumPerimetre.TMS))
            {
                List<Int32> tiTerminaux = Donnees.getTerminalBHS();
                if ((tiTerminaux != null) && (tiTerminaux.Count != 0))
                {
                    foreach (int iTerminal in tiTerminaux)
                    {
                        cb_BHS_Terminal.Items.Add(GlobalNames.BHS_Prefixe_Name + iTerminal.ToString());
                    }
                }
                setMeanFlowsMode();
            }
            #endregion

            dtp_BeginTime.Value = DateTime.Now.Date;
            dtp_EndTime.Value = DateTime.Now.Date.AddDays(1);
            UpdateVisible(bStatic && PAX2SIM.bStatic, bDynamic && PAX2SIM.bDynamic);
            // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            setUpUserAttributesTab();
            // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            InitializeUserData(scenarioParams);
            setDelhiMode();
            setRuntimeMode();
#if(PAXINOUTUTILISATION)
            gb_PaxInOut.Visible = true;
#endif

            #region 26/03/2012 - SGE - Parking Mulhouse
            gb_ParkingAnalysis.Visible = PAX2SIM.bPKG;
            #endregion 26/03/2012 - SGE - Parking Mulhouse
            if (isTraceAnalysisScenario)
                setUpAssistantForATraceAnalysisScenario();
            else
            {
                // >> Bug #15556 Pax2Sim - Scenario Properties Assistant issues C#23
                cb_D_CalcDeparturePeak.Enabled = true;
                cb_CalcArrivalPeak.Enabled = true;
                cbGeneratePaxPlanForStatic.Enabled = true;
                // << Bug #15556 Pax2Sim - Scenario Properties Assistant issues C#23
                cb_BHS_SimulateBHS.Enabled = true;
            }
            // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            if (Donnees.Levels != null && Donnees.Levels.Length == 3)
            {
                firstLevelTextBox.Text = Donnees.Levels[0].ToString();
                secondLevelTextBox.Text = Donnees.Levels[1].ToString();
                thirdLevelTextBox.Text = Donnees.Levels[2].ToString();
            }
            // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            cbGeneratePaxPlanForStatic.Visible = !PAX2SIM.bBHS_MeanFlows && PAX2SIM.bStatic;    // PAX key flag disabled            
        }
        #endregion

        #region les Constructeurs de la classe.
        /// <summary>
        /// Constructure de la classe pour l'édition des propriétés d'un scénario.
        /// Initialisation complète du formulaire.
        /// </summary>
        /// <param name="Donnees_"></param>
        /// <param name="Parameters"></param>
        internal SIM_Scenarios_Assistant(GestionDonneesHUB2SIM Donnees_,
                                       ParamScenario Parameters,
                                       PAX2SIM.EnumPerimetre epPerimetre,
                                       TreeNode AirportNode,
                                       Errors ErrForm_)
        {
            if (Parameters != null)
            {
                isTraceAnalysisScenario = Parameters.isTraceAnalysisScenario;
                bagTracePath = Parameters.bagTracePath;
                currentParameters = Parameters;
            }
            initialiseForm(Donnees_, epPerimetre, AirportNode, true, true, ErrForm_, Parameters);
            if ((Parameters != null) && (cb_Scenario.Items.Contains(Parameters.Name)))
            {
                cb_Scenario.Text = Parameters.Name;
                // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update                
                scenarioCreationDate = Parameters.creationDate;
                scenarioLastUpdateDate = Parameters.lastUpdateDate;
                // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update                
            }
            temporaryCustomResultsFilters = new List<AnalysisResultFilter>();
            if (Parameters != null)
            {
                temporaryLinkedResultsFilters.Clear();
                temporaryLinkedResultsFilters.AddRange(Parameters.analysisResultsFilters);

                flowTypes.Clear();
                flowTypes.AddRange(Parameters.flowTypes);

                if (Parameters.analysisResultsFilters != null && Parameters.analysisResultsFilters.Count > 0)
                {
                    if (Parameters.flowTypes == null || Parameters.flowTypes.Count == 0)
                    {
                        flowTypes.Clear();
                        flowTypes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
                        //flowTypes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
                        flowTypes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
                        //flowTypes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
                        flowTypes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
                        updateSelectedFlowsLabel(flowTypes);
                    }
                }

                string paxPlanFilePath = Donnees.getNomDuChemin() + "\\" + "Output" + "\\" + Parameters.Name + "\\" + "PaxPlanTable.txt";
                if (File.Exists(paxPlanFilePath))
                {
                    cb_SIM_Regenerate_Data.Enabled = dynamicSimulationUsingPaxPlan();
                    //cb_SIM_Regenerate_Data.Checked = dynamicSimulationUsingPaxPlan();
                }
                else
                {
                    cb_SIM_Regenerate_Data.Enabled = false;
                    cb_SIM_Regenerate_Data.Checked = true;
                }
            }
        }
        /// <summary>
        /// Constructeur de la classe générique.
        /// </summary>
        /// <param name="Donnees_"></param>
        /// <param name="bStatic"></param>
        internal SIM_Scenarios_Assistant(GestionDonneesHUB2SIM Donnees_,
                                       PAX2SIM.EnumPerimetre epPerimetre,
                                       TreeNode AirportNode,
                                       Errors ErrForm_)
        {
            initialiseForm(Donnees_, epPerimetre, AirportNode, false, true, ErrForm_, null);
            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            //DateTime now = DateTime.Now;
            scenarioCreationDate = DateTime.MinValue;
            scenarioLastUpdateDate = DateTime.MinValue;
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

            temporaryCustomResultsFilters = new List<AnalysisResultFilter>();
            temporaryLinkedResultsFilters = new List<AnalysisResultFilter>();

            flowTypes.Clear();
            flowTypes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
            //flowTypes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
            flowTypes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
            //flowTypes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
            flowTypes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
        }
        #endregion

        #region Fonction pour récupérer les paramêtres pour les analyses

        public String getNomScenario()
        {
            return cb_Scenario.Text;
        }
        public ParamScenario getAnalysis()
        {
            ParamScenario param = new ParamScenario(getNomScenario());

            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            param.authorName = authorNameTextBox.Text;
            param.scenarioDescription = scenarioDescriptionTextBox.Text;
            param.creationDate = scenarioCreationDate;
            param.lastUpdateDate = scenarioLastUpdateDate;
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

            string assemblyVersion = OverallTools.AssemblyActions.AssemblyVersion;
            if (assemblyVersion.Contains("."))
            {
                string[] splittedVersion = assemblyVersion.Split('.');
                if (splittedVersion.Length > 1)
                {
                    string partialVersion = splittedVersion[0] + "." + splittedVersion[1];
                    double version = -1;
                    Double.TryParse(partialVersion, out version);
                    param.lastModificationP2SVersion = version;
                }
            }
            param.DateDebut = dtp_BeginTime.Value;
            param.DateFin = dtp_EndTime.Value;
            Double dTmp;
            Double.TryParse(cb_SamplingStep.Text, out dTmp);
            param.SamplingStep = dTmp;
            Double.TryParse(cb_AnalysisRange.Text, out dTmp);
            param.AnalysisRange = dTmp;
            Double.TryParse(cb_StatisticStep.Text, out dTmp);
            param.StatisticsStep = dTmp;
            param.StatisticsStepMode = cb_StStepPolitic.Text;
            if ((cb_SIM_DynReclaimTerminal.Text != "No") && (cb_SIM_DynReclaimTerminal.Text != ""))
            {
                param.DynamicReclaimAllocation = true;
                param.TerminalReclaimAllocation = FonctionsType.getInt(cb_SIM_DynReclaimTerminal.Text);
                if (param.TerminalReclaimAllocation == 0)
                    param.DynamicReclaimAllocation = false;
            }
            else
            {
                param.DynamicReclaimAllocation = false;
                param.TerminalReclaimAllocation = 0;
            }

            param.DeparturePeak = cb_D_CalcDeparturePeak.Checked;
            param.ArrivalPeak = cb_CalcArrivalPeak.Checked;
            param.generatePaxPlanForStaticAnalysis = cbGeneratePaxPlanForStatic.Checked;
            param.PaxPlan = cb_PP_GeneratePaxPlan.Checked;
            param.BHSSimulation = cb_BHS_SimulateBHS.Checked;
            param.TMSSimulation = cb_Gen_Tms.Checked;
            param.BagPlan = rb_BHS_BagPlan.Checked;
            if ((param.BagPlan) || (param.DeparturePeak) || (param.PaxPlan) || param.generatePaxPlanForStaticAnalysis)
            {
                param.FPD = ConvertToName(cb_D_FPD.Text);
                param.OCT_CI_Table = ConvertToName(cb_D_OCT_CI.Text);
                param.OCT_BG = ConvertToName(cb_D_BoardingGate.Text);
                param.OCT_MakeUp = ConvertToName(cb_D_OCT_MakeUp.Text);
                param.OCT_BagDropTableName = ConvertToName(cb_D_OCT_BagDrop.Text);  // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop

                if (!param.BagPlan)
                {
                    param.DepartureLoadFactors = ConvertToName(cb_D_LFD.Text);
                    param.CI_ShowUpTable = ConvertToName(cb_D_ShowUp.Text);
                }
                param.ICT_Table = ConvertToName(cb_A_InterConnectingTimes.Text);    // << Task #9117 Pax2Sim - Static Analysis - FPD_EBS calculation - transfering pax missing
                // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
                param.ebsInputRateTableName = ConvertToName(cb_d_EBSInputRate.Text);
                param.ebsOutputRateTableName = ConvertToName(cb_d_EBSOutputRate.Text);
                // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

            }
            if ((param.BagPlan) || (param.ArrivalPeak) || (param.PaxPlan) || param.generatePaxPlanForStaticAnalysis)
            {
                param.FPA = ConvertToName(cb_FPATable.Text);
                param.OCT_BC = ConvertToName(cb_A_BaggageClaim.Text);
                if (!param.BagPlan)
                {
                    param.ArrivalLoadFactors = ConvertToName(cb_A_LoadFactors.Text);
                    param.ICT_Table = ConvertToName(cb_A_InterConnectingTimes.Text);
                }
                // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
                param.arrivalBaggageLoadingRateTableName = ConvertToName(cb_A_BaggageLoadingRate.Text);
                param.arrivalBaggageLoadingDelayTableName = ConvertToName(cb_A_BaggageLoadingDelay.Text);
                // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            }
            if ((param.BagPlan) || (param.DeparturePeak) || (param.ArrivalPeak) || (param.PaxPlan) || param.generatePaxPlanForStaticAnalysis)
            {
                param.Airline = ConvertToName(cb_St_Airline.Text);
                param.AircraftType = ConvertToName(cb_D_AircraftType.Text); // >> Task #13361 FP AutoMod Data tables V3
            }
            if ((param.DeparturePeak) || (param.ArrivalPeak) || (param.PaxPlan) || param.generatePaxPlanForStaticAnalysis)
            {
                param.FlightCategories = ConvertToName(cb_D_FC.Text);
                param.FlightSubcategories = ConvertToName(flightSubcategoriesComboBox.Text);    // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
                param.AircraftType = ConvertToName(cb_D_AircraftType.Text);
                param.NbBags = ConvertToName(cb_D_NbBags.Text);
                param.NbTrolley = ConvertToName(cb_D_NbTrolley.Text);
                param.NbVisitors = ConvertToName(cb_D_NbVisitors.Text);
                if ((param.DeparturePeak && param.ArrivalPeak) || (param.PaxPlan) || param.generatePaxPlanForStaticAnalysis)
                    param.AircraftLinksTable = ConvertToName(cb_D_AircraftLinks.Text);
                // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                param.UseDefinedNbPax = useNbOfPaxCheckBox.Checked;
                param.NumberOfPassengers = ConvertToName(nbOfPaxComboBox.Text);

                param.UseDefinedNbBags = useNbOfBagsCheckBox.Checked;
                param.NumberOfBaggages = ConvertToName(nbOfBagsComboBox.Text);
                if (useFPParametersTableCheckBox.Checked)   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
                {
                    param.FPParametersTableName = ConvertToName(fpParametersTableComboBox.Text);
                }
                // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                param.UsaStandardParamTableName = ConvertToName(usaStandardParamsComboBox.Text);    // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
                param.reclaimBagsLimitDistributionTableName = ConvertToName(reclaimSyncComboBox.Text);
            }
            param.IsTransferDistributionDeterministic = cb_PP_TransferDeterministicDistribution.Checked;
            if (param.PaxPlan || param.generatePaxPlanForStaticAnalysis)
            {
                if (cb_PP_TransferDistribution.Checked)
                    param.TransfTerminalDistribution = ConvertToName(cb_TransTerm.Text);
                else
                    param.TransfTerminalDistribution = "";

                if (cb_PP_FlightCategoryDistr.Checked)
                    param.TransfFligthCategoryDistribution = ConvertToName(cb_PP_TransferFCTable.Text);
                else
                    param.TransfFligthCategoryDistribution = "";

                param.UseSeed = cb_PP_UseDefinedSeed.Checked;
                param.Seed = 1;
                int iSeed;
                Int32.TryParse(mb_PP_UseDefinedSeed.Text, out iSeed);

                if (param.UseSeed)
                    param.Seed = iSeed;

                param.GenerateAll = rb_PP_GenerateAll.Checked;
                param.GenerateFlightsAtEnd = rb_PP_GenerateFlightsAtEnd.Checked;
                param.TransferArrivalGeneration = rb_PP_Arriving.Checked;
                param.MissedDepartureTransferBasedOnCheckInShowUp = missedDepartureTransferBasedOnCIRadioButton.Checked;
                param.FillTransfer = cb_PP_FillTransfer.Checked;
                param.FillDepartureTransferBasedOnCheckInShowUp = fillBasedOnCheckInRadioButton.Checked;
                param.FillQueue = rb_Filling.Checked;

                // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
                if (rb_Normal.Checked)
                    param.StationGlobalFillingType = GlobalNames.FIRST_STATION_FILLING_TYPE;
                else if (rb_Filling.Checked)
                    param.StationGlobalFillingType = GlobalNames.SATURATE_STATION_FILLING_TYPE;
                else if (rb_RandomFilling.Checked)
                    param.StationGlobalFillingType = GlobalNames.RANDOM_STATION_FILLING_TYPE;
                // >> Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation

                param.Opening_CITable = "";
                if (cb_PP_Opening.Checked)
                    param.Opening_CITable = ConvertToName(cb_PP_Opening_CI.Text);
                param.PaxSimulation = true;

                if (rb_PF_ProcessSchedule.Checked)
                {
                    param.UseProcessSchedule = true;
                    param.ProcessSchedule = ConvertToName(cb_PF_ProcessSchedule.Text);
                    param.ProcessTimes = "";
                    param.Itinerary = "";
                    param.CapaQueues = "";
                    param.GroupQueues = "";
                }
                else
                {
                    param.UseProcessSchedule = false;
                    param.ProcessSchedule = "";
                    param.ProcessTimes = ConvertToName(cb_PP_Alloc_Process.Text);
                    param.Itinerary = ConvertToName(cb_PP_Alloc_Itinerary.Text);
                    param.CapaQueues = ConvertToName(cb_PP_Alloc_CapaQueues.Text);
                    param.GroupQueues = ConvertToName(cb_PP_GroupQueues.Text);
                    // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
                    param.ProcessCapacities = ConvertToName(processCapaComboBox.Text);
                    // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
                }
                if (chk_Animated.Checked)
                {
                    param.AnimatedQueues = ConvertToName(cb_PF_AnimatedFlow.Text);
                }
                param.Security = ConvertToName(cb_PP_Alloc_Security.Text);
                param.Transfer = ConvertToName(cb_PP_Alloc_Transfer.Text);
                param.Passport = ConvertToName(cb_PP_Alloc_Passport.Text);
                // << Task #7570 new Desk and extra information for Pax -Phase I B
                param.UserProcess = ConvertToName(cb_PP_Alloc_UserProcess.Text);
                // >> Task #7570 new Desk and extra information for Pax -Phase I B
                param.Saturation = ConvertToName(cb_PF_Saturation.Text);
                param.Oneof = ConvertToName(cb_PP_Oneof.Text);


                #region 26/03/2012 - SGE - Parking Mulhouse
                if (chk_PP_Parking.Checked)
                {
                    param.InitialCarStock = ConvertToName(cb_PP_InitialOccupation.Text);
                    param.UseExistingPRKPlan = rb_PP_ExistingPrkPlan.Checked;
                    param.LongStayTable = ConvertToName(cb_PP_DwellTime.Text);
                    param.ShortStayTable = ConvertToName(cb_PP_DwellTimeVis.Text);
                }
                #endregion //26/03/2012 - SGE - Parking Mulhouse
            }
            if (param.BHSSimulation)
            {
                param.UsePaxPlan = rb_BHS_UsePaxPlan.Checked;
                param.PaxSimulation = rb_BHS_UsePaxPlan.Checked;
                param.Terminal = cb_BHS_Terminal.Text;
                param.General = cb_BHS_General.Text;
                param.CICollectors = cb_BHS_CI_Collectors.Text;
                param.CIRouting = cb_BHS_CI_Routing.Text;
                param.HBS3Routing = cb_BHS_HBS3_Routing.Text;
                param.TransferRouting = cb_BHS_TransferRouting.Text;
                param.ArrivalInfeedGroups = cb_BHS_ArrivalInfeedGroups.Text;
                param.CIGroups = cb_BHS_CI_Group.Text;
                param.TransferInfeedGroups = cb_BHS_TransferInfeedGroups.Text;
                param.FlowSplit = cb_BHS_FlowSplit.Text;
                param.Process = cb_BHS_Process.Text;
                param.ArrivalContainers = cb_BHS_Arrival_Containers.Text;
                param.ArrivalMeanFlows = cb_BHS_Arrival_MeanFlowTable.Text;
                param.CheckInMeanFlows = cb_BHS_CheckIn_MeanFlowTable.Text;
                param.TransferMeanFlows = cb_BHS_Transfer_MeanFlowTable.Text;
                if (param.BagPlan)
                    param.BagPlanScenarioName = cb_BHS_BagPlan.Text;
                /*
                if (cb_SIM_MakeUpSegregation.Visible && (!cb_CustomizedModel.Checked))
                {
                    param.UseMakeUpSegregation = cb_SIM_MakeUpSegregation.Checked;
                    param.UseExSegregation = cb_ex_MakeUpSegregation.Checked;
                }
                else
                {
                    param.UseMakeUpSegregation = false;
                    param.UseExSegregation = true;
                }*/
                param.UseMakeUpSegregation = cb_SIM_MakeUpSegregation.Checked;
                param.UseExSegregation = cb_ex_MakeUpSegregation.Checked;

                param.bhsGenerateLocalIST = generateLocalISTCheckBox.Checked;   // >> Task #13955 Pax2Sim -BHS trace loading issue
                param.bhsGenerateGroupIST = generateGroupISTcheckBox.Checked;   // >> Task #14280 Bag Trace Loading time too long
                param.bhsGenerateMUPSegregation = generateMUPSegregationCheckBox.Checked;   // >> Task #14280 Bag Trace Loading time too long

                if (tc_General.TabPages.Contains(tp_resultsFilters))
                {
                    param.analysisResultsFilters.Clear();
                    param.analysisResultsFilters.AddRange(temporaryLinkedResultsFilters);
                    if (paGlobalParams != null)
                        param.analysisResultsFiltersSplittedByFlow.AddRange(paGlobalParams.analysisResultsFiltersSplittedByFlow);

                    param.flowTypes.Clear();
                    param.flowTypes.AddRange(flowTypes);
                }
            }
            param.UseMakeUpSegregation = cb_SIM_MakeUpSegregation.Checked;
            param.UseExSegregation = cb_ex_MakeUpSegregation.Checked;
            // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            param.userAttributesTablesDictionary = getSelectedUserAttributesDictionary();
            // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab

            param.UserData = getParamUserData();
            Double dWarmup;
            if (!Double.TryParse(txt_SIM_WarmUp.Text, out dWarmup))
            {
                dWarmup = 0;
            }
            param.WarmUp = dWarmup;
            param.SaveTraceMode = !cb_SaveTraceMode.Checked;
            param.ModelName = lbl_SIM_ModelName.Text;
            param.DisplayModel = cb_SIM_DisplayModel.Checked;
            param.RegeneratePaxplan = cb_SIM_Regenerate_Data.Checked;

            param.ExportUserData = cb_SIM_ExportUserData.Checked;
            param.clearAutomodUserData = clearAutomodUserDataCheckBox.Checked;

            param.exportAutomodMainData = exportAutomodMainDataCheckBox.Checked;
            param.clearAutomodMainData = clearAutomodMainDataCheckBox.Checked;

            param.useUserProvidedSimulationEngineSeed = useSimulationEngineSeedCheckBox.Checked;
            param.simulationEngineSeed = 1;
            if (useSimulationEngineSeedCheckBox.Checked)  // >> Bug #14900 MFF file not created - C#6
            {
                int simulationEngineSeed = 1;
                Int32.TryParse(simulationEngineSeedMaskedTextBox1.Text, out simulationEngineSeed);
                param.simulationEngineSeed = simulationEngineSeed;
            }

            // Utilisation des tables d'exceptions
            param.UseException(
                 ConvertToName(cb_D_LFD.Text),
                 cb_ex_DLoadFactors.Checked);
            param.UseException(
                ConvertToName(cb_D_ShowUp.Text),
                cb_ex_CI_ShowUp.Checked);
            param.UseException(
                ConvertToName(cb_D_OCT_CI.Text),
                cb_ex_OCT_CI.Checked);
            param.UseException(
                ConvertToName(cb_D_OCT_MakeUp.Text),
                cb_ex_OCT_MakeUp.Checked);
            param.UseException(
                ConvertToName(cb_D_BoardingGate.Text),
                cb_ex_OCT_BoardGate.Checked);
            //To-check
            param.UseException(
                ConvertToName(cb_A_LoadFactors.Text),
                cb_ex_ALoadFactors.Checked);
            param.UseException(
                ConvertToName(cb_A_InterConnectingTimes.Text),
                cb_ex_Transfer_ICT.Checked);
            param.UseException(
                ConvertToName(cb_A_BaggageClaim.Text),
                cb_ex_OCT_BaggageClaim.Checked);
            // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            param.UseException(ConvertToName(cb_A_BaggageLoadingRate.Text), cb_ex_BaggageLoadingRate.Checked);
            param.UseException(ConvertToName(cb_A_BaggageLoadingDelay.Text), cb_ex_BaggageLoadingDelay.Checked);
            // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay

            param.UseException(ConvertToName(cb_D_OCT_BagDrop.Text), cb_ex_OCT_BagDrop.Checked);  // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop

            // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            param.UseException(ConvertToName(cb_d_EBSInputRate.Text), cb_ex_EBSInputRate.Checked);
            param.UseException(ConvertToName(cb_d_EBSOutputRate.Text), cb_ex_EBSOutputRate.Checked);
            // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            param.UseException(
                ConvertToName(cb_D_AircraftType.Text),
                cb_ex_Aircraft.Checked);
            param.UseException(
                ConvertToName(cb_D_NbBags.Text),
                cb_ex_NbBags.Checked);
            param.UseException(
                ConvertToName(cb_D_NbVisitors.Text),
                cb_ex_NbVisitors.Checked);
            param.UseException(
                ConvertToName(cb_D_NbTrolley.Text),
                cb_ex_NbTrolley.Checked);
            param.UseException(
                ConvertToName(cb_PP_Alloc_Process.Text),
                cb_ex_TimesProcess.Checked);
#if(PAXINOUTUTILISATION)
            if (param.PaxPlan || param.generatePaxPlanForStaticAnalysis)
            {
                param.UseException(
                    ConvertToName(cb_PP_PaxIn.Text),
                    cb_ex_PaxIn.Checked);
                param.UseException(
                    ConvertToName(cb_PP_PaxOut.Text),
                    cb_ex_PaxOut.Checked);
                param.PaxIn = ConvertToName(cb_PP_PaxIn.Text);
                param.PaxOut = ConvertToName(cb_PP_PaxOut.Text);
            }
#endif

            #region 26/03/2012 - SGE - Parking Mulhouse
            if (chk_PP_Parking.Checked)
            {
                param.UseException(
                    ConvertToName(cb_PP_DwellTime.Text),
                    cb_PP_Ex_DwellTime.Checked);

                param.UseException(
                    ConvertToName(cb_PP_DwellTimeVis.Text),
                    cb_PP_Ex_DwellTimeVis.Checked);
            }
            #endregion //26/03/2012 - SGE - Parking Mulhouse
            param.UseExSegregation = cb_ex_MakeUpSegregation.Checked;

            //param.isTraceAnalysisScenario = isTraceAnalysisScenario;    // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#13
            param.isTraceAnalysisScenario = traceAnalysisCheckBox.Checked;
            //if (isTraceAnalysisScenario)  // >> Task #16728 PAX2SIM Improvements (Recurring) C#17
            param.bagTracePath = bagTracePath;  // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17

            // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            param.percentilesLevels = new List<double>();
            double percentile1 = 0;
            double percentile2 = 0;
            double percentile3 = 0;
            if (Double.TryParse(firstLevelTextBox.Text, out percentile1)
                && Double.TryParse(secondLevelTextBox.Text, out percentile2)
                && Double.TryParse(thirdLevelTextBox.Text, out percentile3))
            {
                param.percentilesLevels.Add(percentile1);
                param.percentilesLevels.Add(percentile2);
                param.percentilesLevels.Add(percentile3);
            }
            // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec

            return param;
        }

        internal ParamScenario Analysis
        {
            get
            {
                if (SourceAnalysis == null)
                    SourceAnalysis = getAnalysis();
                return SourceAnalysis;
            }
        }
        #endregion

        // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        #region User Attributes management
        private void setUpUserAttributesTab()
        {
            List<String> userAttributesNamesList = getUserAttributesNamesList();
            p_UserAttributes.Controls.Clear();

            if (userAttributesNamesList.Count > 0)
            {
                int tabIndex = 0;
                int xPosition = 30;
                int yPosition = 30;
                int spaceBetweenComboBoxes = 25;
                foreach (String userAttributeName in userAttributesNamesList)
                {
                    createLabelAndComboBox(userAttributeName, xPosition, yPosition, tabIndex);
                    tabIndex++;
                    yPosition += spaceBetweenComboBoxes;
                }
            }
        }

        private void createLabelAndComboBox(String userAttributeName, int xPosition, int yPosition, int tabIndex)
        {
            System.Windows.Forms.Label userAttributeLabel = new System.Windows.Forms.Label();
            System.Windows.Forms.ComboBox userAttributeComboBox = new System.Windows.Forms.ComboBox();

            userAttributeLabel.Name = userAttributeName + "L";
            userAttributeLabel.Text = userAttributeName;
            userAttributeLabel.Size = new System.Drawing.Size(216, 13);
            userAttributeLabel.Location = new System.Drawing.Point(xPosition, yPosition);
            userAttributeLabel.AutoSize = true;
            userAttributeLabel.TabIndex = tabIndex;

            userAttributeComboBox.Name = userAttributeName + "CB";
            userAttributeComboBox.Size = new System.Drawing.Size(250, 20);
            userAttributeComboBox.Location = new System.Drawing.Point(xPosition + 220, yPosition - 2);
            userAttributeComboBox.TabIndex = tabIndex + 1;
            userAttributeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            userAttributeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            userAttributeComboBox.Tag = userAttributeName;

            p_UserAttributes.Controls.Add(userAttributeLabel);
            p_UserAttributes.Controls.Add(userAttributeComboBox);

            UpdateComboBox(userAttributeComboBox, true);
        }

        private Dictionary<String, String> getSelectedUserAttributesDictionary()
        {
            Dictionary<String, String> selectedUserAttributesDictionary = new Dictionary<String, String>();

            if (p_UserAttributes.Controls != null && p_UserAttributes.Controls.Count > 0)
            {
                bool flightSubcategAdded = false;   // >> Task #10764 Pax2Sim - new User attributes for Groups

                for (int i = 0; i < p_UserAttributes.Controls.Count; i++)
                {
                    if (p_UserAttributes.Controls[i] is System.Windows.Forms.ComboBox)
                    {
                        System.Windows.Forms.ComboBox userAttributeComboBox
                            = (System.Windows.Forms.ComboBox)p_UserAttributes.Controls[i];

                        if (userAttributeComboBox.SelectedItem != null)
                        {
                            String selectedUserAttributeTableName = userAttributeComboBox.SelectedItem.ToString();
                            selectedUserAttributesDictionary.Add(userAttributeComboBox.Tag.ToString(), selectedUserAttributeTableName);

                            if (userAttributeComboBox.Tag.ToString().Equals("Flight Subcategories"))    // >> Task #10764 Pax2Sim - new User attributes for Groups
                            {
                                flightSubcategAdded = true;
                            }
                        }
                    }
                    // >> Task #10764 Pax2Sim - new User attributes for Groups
                    if (flightSubcategAdded)
                    {
                        if (!selectedUserAttributesDictionary.ContainsKey(GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE))
                            selectedUserAttributesDictionary.Add(GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE, GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE);
                        if (!selectedUserAttributesDictionary.ContainsKey(GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE))
                            selectedUserAttributesDictionary.Add(GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE, GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE);
                        if (!selectedUserAttributesDictionary.ContainsKey(GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE))
                            selectedUserAttributesDictionary.Add(GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE, GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE);
                        if (!selectedUserAttributesDictionary.ContainsKey(GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE))
                            selectedUserAttributesDictionary.Add(GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE, GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE);
                    }
                    // << Task #10764 Pax2Sim - new User attributes for Groups
                }
                // >> Task #10764 Pax2Sim - new User attributes for Groups
                if (!flightSubcategAdded)
                {
                    if (!selectedUserAttributesDictionary.ContainsKey(GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE))
                        selectedUserAttributesDictionary.Add(GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE, GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE);
                    if (!selectedUserAttributesDictionary.ContainsKey(GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE))
                        selectedUserAttributesDictionary.Add(GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE, GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE);
                    if (!selectedUserAttributesDictionary.ContainsKey(GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE))
                        selectedUserAttributesDictionary.Add(GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE, GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE);
                    if (!selectedUserAttributesDictionary.ContainsKey(GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE))
                        selectedUserAttributesDictionary.Add(GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE, GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE);
                }
                // << Task #10764 Pax2Sim - new User attributes for Groups
            }
            return selectedUserAttributesDictionary;
        }

        private void setParamUserAttributes(Dictionary<String, String> userAttributesDictionary)
        {
            if (p_UserAttributes != null && p_UserAttributes.Controls != null
                && p_UserAttributes.Controls.Count > 0)
            {
                if (userAttributesDictionary != null && userAttributesDictionary.Count > 0)
                {
                    for (int i = 0; i < p_UserAttributes.Controls.Count; i++)
                    {
                        if (p_UserAttributes.Controls[i] is System.Windows.Forms.ComboBox)
                        {
                            System.Windows.Forms.ComboBox userAttributeComboBox
                                = (System.Windows.Forms.ComboBox)p_UserAttributes.Controls[i];
                            if (userAttributesDictionary.ContainsKey(userAttributeComboBox.Tag.ToString()))
                            {
                                String userAttributeTableName = "";
                                if (userAttributesDictionary
                                    .TryGetValue(userAttributeComboBox.Tag.ToString(), out userAttributeTableName))
                                {
                                    int userAttributeIndex = userAttributeComboBox.Items.IndexOf(userAttributeTableName);
                                    if (userAttributeIndex != -1)
                                        userAttributeComboBox.SelectedIndex = userAttributeIndex;
                                    else if (userAttributeComboBox.Items.Count > 0) // PAX key flag disabled
                                    {
                                        userAttributeComboBox.SelectedIndex = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
        // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab

        #region Partie pour la Gestion des USER_DATA

        internal class UserDataFiles
        {
            public String sFileName;
            public List<String> alFileChilds;

            public String[] FileChilds
            {
                get
                {
                    if (alFileChilds == null)
                        return null;
                    String[] tsResults = new String[alFileChilds.Count];
                    for (int i = 0; i < alFileChilds.Count; i++)
                    {
                        tsResults[i] = (String)alFileChilds[i];
                    }
                    return tsResults;
                }
            }
            public UserDataFiles(String sFileName_, List<String> alFileChilds_)
            {
                sFileName = sFileName_;
                alFileChilds = alFileChilds_;
            }
            public class UserDataFilesComparer : IComparer<UserDataFiles>
            {
                public int Compare(UserDataFiles x, UserDataFiles y)
                {
                    return OverallTools.FonctionUtiles.CompareString(x.ToString(), y.ToString(), true);
                }
            }
            public override string ToString()
            {
                return sFileName;
            }
        }

        private ParamUserData getParamUserData()
        {
            if (lbl_UD_Name == null)
                return null;
            if (lbl_UD_Name.Length == 0)
                return null;
            if (userDataCheckBoxes == null || userDataCheckBoxes.Length == 0) // >> Task #16728 PAX2SIM Improvements (Recurring) C#1
                return null;
            ParamUserData pudResult = new ParamUserData(new Dictionary<String, String>());
            for (int i = 0; i < lbl_UD_Name.Length; i++)
            {
                if (i < userDataCheckBoxes.Length && userDataCheckBoxes[i].Checked)
                    pudResult.setUserData(lbl_UD_Name[i].Text, cb_UD_Selected[i].Text);
            }
            return pudResult;
        }

        private CheckBox selectAllCheckBox = null;
        private void InitializeUserData(ParamScenario scenarioParams)
        {
            List<UserDataFiles> alUserData = new List<UserDataFiles>();
#if(!NEWUSERDATA)
            XmlNode xnUserData = Donnees.getUserData();
            foreach (XmlNode xnChild in xnUserData.ChildNodes)
            {
                List<String> alFiles = null;
                if (xnChild.Attributes.Count > 0)
                {
                    alFiles = new List<String>();
                    foreach (XmlAttribute xnAttribute in xnChild.Attributes)
                    {
                        alFiles.Add(xnAttribute.Value);
                    }
                    alFiles.Sort();
                    alUserData.Add(new UserDataFiles(xnChild.FirstChild.Value, alFiles));
                }
            }
            if (xnUserData.Attributes.Count > 0)
            {
                foreach (XmlAttribute xnAttribute in xnUserData.Attributes)
                {
                    alUserData.Add(new UserDataFiles(xnAttribute.Value, null));
                }
            }
#else
            Dictionary<String, List<String>> xnUserData = Donnees.getUserData();
            foreach (String sKey in xnUserData.Keys)
            {
                alUserData.Add(new UserDataFiles(sKey, xnUserData[sKey]));
            }
#endif
            alUserData.Sort(new UserDataFiles.UserDataFilesComparer());
            lbl_UD_Name = new Label[alUserData.Count];
            cb_UD_Selected = new ComboBox[alUserData.Count];
            userDataCheckBoxes = new CheckBox[alUserData.Count];

            if (alUserData.Count > 0)
            {
                selectAllCheckBox = new CheckBox();
                p_UserData.Controls.Add(selectAllCheckBox);
                selectAllCheckBox.Text = "Select / Deselect All";
                selectAllCheckBox.AutoSize = true;
                selectAllCheckBox.Location = new Point(650, 5);
                selectAllCheckBox.CheckedChanged += new EventHandler(selectAllCheckBox_CheckedChanged);
            }
            for (int i = 0; i < alUserData.Count; i++)  // >> Task #16728 PAX2SIM Improvements (Recurring) C#1
            {
                string selectedUserDataFileName = null;
                if (scenarioParams != null && scenarioParams.UserData != null && scenarioParams.UserData.UserData != null)
                {
                    if (scenarioParams.UserData.UserData.ContainsKey(alUserData[i].sFileName))
                        selectedUserDataFileName = scenarioParams.UserData.UserData[alUserData[i].sFileName];
                }
                initializeUserDataTab(i, alUserData[i].sFileName, alUserData[i].FileChilds, selectedUserDataFileName);
            }
            if (selectAllCheckBox != null)
            {
                bool allUserDataCheckboxesChecked = true;
                foreach (CheckBox cb in userDataCheckBoxes)
                {
                    if (!cb.Checked)
                    {
                        allUserDataCheckboxesChecked = false;
                        break;
                    }
                }
                selectAllCheckBox.CheckedChanged -= new EventHandler(selectAllCheckBox_CheckedChanged);
                selectAllCheckBox.Checked = allUserDataCheckboxesChecked;
                selectAllCheckBox.CheckedChanged += new EventHandler(selectAllCheckBox_CheckedChanged);
            }

            /*if (alUserData.Count > 0)
            {
                cb_SIM_ExportUserData.Visible = true;// (epPerimetre_ != PAX2SIM.EnumPerimetre.PAX);
                clearAutomodUserDataCheckBox.Visible = cb_SIM_ExportUserData.Visible;
            }*/
            cb_SIM_ExportUserData.Enabled = (alUserData.Count > 0);
        }

        private void selectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckBox cb in userDataCheckBoxes)
            {
                cb.CheckedChanged -= new EventHandler(userDataCheckBox_CheckedChanged);
                cb.Checked = selectAllCheckBox.Checked;
                cb.CheckedChanged += new EventHandler(userDataCheckBox_CheckedChanged);
            }
        }

        private void setParamUserData(ParamUserData pudParams)
        {
            if (pudParams == null)
                return;

            if (lbl_UD_Name == null)
                return;
            if (lbl_UD_Name.Length == 0)
                return;
            if (userDataCheckBoxes == null || userDataCheckBoxes.Length == 0)   // >> Task #16728 PAX2SIM Improvements (Recurring) C#1
                return;
            for (int i = 0; i < lbl_UD_Name.Length; i++)
            {
                String sSelected = pudParams.getUserData(lbl_UD_Name[i].Text);
                int iSelected = -1;
                if (sSelected != null)
                {
                    iSelected = cb_UD_Selected[i].Items.IndexOf(sSelected);
                    if (i < userDataCheckBoxes.Length)
                        userDataCheckBoxes[i].Checked = true;
                }
                if (iSelected == -1)
                    iSelected = 0;
                cb_UD_Selected[i].SelectedIndex = iSelected;
            }
        }
        private System.Windows.Forms.Label[] lbl_UD_Name = null;
        private System.Windows.Forms.ComboBox[] cb_UD_Selected = null;
        private CheckBox[] userDataCheckBoxes = null;   // >> Task #16728 PAX2SIM Improvements (Recurring) C#1

        private void initializeUserDataTab(int iIndexUserData, String sLabel, String[] cbContent, String sSelected)
        {
            lbl_UD_Name[iIndexUserData] = new System.Windows.Forms.Label();
            cb_UD_Selected[iIndexUserData] = new System.Windows.Forms.ComboBox();
            userDataCheckBoxes[iIndexUserData] = new CheckBox();

            // 
            // lbl_UD_Name
            // 
            lbl_UD_Name[iIndexUserData].AutoSize = true;
            lbl_UD_Name[iIndexUserData].Location = new System.Drawing.Point(30, iIndexUserData * 25 + 30);
            lbl_UD_Name[iIndexUserData].Name = "lbl_UD_Name_" + iIndexUserData.ToString();
            lbl_UD_Name[iIndexUserData].Size = new System.Drawing.Size(216, 13);
            lbl_UD_Name[iIndexUserData].TabIndex = 0;
            lbl_UD_Name[iIndexUserData].Text = sLabel;

            // 
            // cb_UD_Selected
            //            
            cb_UD_Selected[iIndexUserData].Location = new System.Drawing.Point(350, iIndexUserData * 25 + 28);//.Point(220, iIndexUserData * 25 + 28);
            cb_UD_Selected[iIndexUserData].Name = "cb_UD_Selected_" + iIndexUserData.ToString();
            cb_UD_Selected[iIndexUserData].Size = new System.Drawing.Size(250, 20);
            cb_UD_Selected[iIndexUserData].TabIndex = iIndexUserData;
            cb_UD_Selected[iIndexUserData].DropDownStyle = ComboBoxStyle.DropDownList;
            cb_UD_Selected[iIndexUserData].Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            if (cbContent == null)
            {
                cb_UD_Selected[iIndexUserData].Items.Add(sLabel);
                cb_UD_Selected[iIndexUserData].SelectedIndex = 0;
            }
            else
            {
                for (int i = 0; i < cbContent.Length; i++)
                {
                    cb_UD_Selected[iIndexUserData].Items.Add(cbContent[i]);
                }
                int iSelected = 0;
                if (sSelected != null)
                {
                    iSelected = cb_UD_Selected[iIndexUserData].Items.IndexOf(sSelected);
                    if (iSelected == -1)
                        iSelected = 0;
                }
                cb_UD_Selected[iIndexUserData].SelectedIndex = iSelected;

            }
            //Check-box
            userDataCheckBoxes[iIndexUserData].Name = "userDataCheckBox_" + iIndexUserData.ToString();
            userDataCheckBoxes[iIndexUserData].Location = new Point(650, iIndexUserData * 25 + 28);
            userDataCheckBoxes[iIndexUserData].Text = "Use";
            userDataCheckBoxes[iIndexUserData].Checked = (sSelected != null);
            userDataCheckBoxes[iIndexUserData].CheckedChanged += new EventHandler(userDataCheckBox_CheckedChanged);

            p_UserData.Controls.Add(lbl_UD_Name[iIndexUserData]);
            p_UserData.Controls.Add(cb_UD_Selected[iIndexUserData]);
            p_UserData.Controls.Add(userDataCheckBoxes[iIndexUserData]);
        }

        private void userDataCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null || sender.GetType() != typeof(CheckBox))
                return;
            CheckBox senderCheckBox = (CheckBox)sender;
            if (!senderCheckBox.Checked)
            {
                selectAllCheckBox.CheckedChanged -= new EventHandler(selectAllCheckBox_CheckedChanged);
                selectAllCheckBox.Checked = false;
                selectAllCheckBox.CheckedChanged += new EventHandler(selectAllCheckBox_CheckedChanged);
            }
            else
            {
                bool allOtherUserDataCheckBoxesAreChecked = true;
                foreach (CheckBox cb in userDataCheckBoxes)
                {
                    if (cb != senderCheckBox && !cb.Checked)
                    {
                        allOtherUserDataCheckBoxesAreChecked = false;
                        break;
                    }
                }
                selectAllCheckBox.CheckedChanged -= new EventHandler(selectAllCheckBox_CheckedChanged);
                selectAllCheckBox.Checked = allOtherUserDataCheckBoxesAreChecked;
                selectAllCheckBox.CheckedChanged += new EventHandler(selectAllCheckBox_CheckedChanged);
            }
        }
        #endregion

        #region Pour mettre à jour les objets actifs.
        private void UpdateEnabled()
        {
            bool bDeparture = cb_D_CalcDeparturePeak.Checked;
            bool bArrival = cb_CalcArrivalPeak.Checked;
            bool paxPlanGeneration = cbGeneratePaxPlanForStatic.Checked;
            bool bPaxPlan = cb_PP_GeneratePaxPlan.Checked;
            bool bBagPlan = rb_BHS_BagPlan.Checked && cb_BHS_SimulateBHS.Checked;
            bool bTrialVersion = PAX2SIM.bTrialVersion;
            bool bAnimatedQueues = PAX2SIM.bAnimatedQueues;

            //btn_PeakFlows.Enabled = (bDeparture || bArrival || bPaxPlan) && cb_Scenario.Enabled;
            //Task T-50 >>ValueCoders
            //if (bDeparture && bArrival && bPaxPlan && cb_Scenario.Enabled)
            if ((bDeparture || bArrival || paxPlanGeneration) && cb_Scenario.Enabled)
            {
                btn_PeakFlows.Enabled = true;
            }
            else
            {
                btn_PeakFlows.Enabled = false;
            }

            btn_CapacityAnalysis.Enabled = (bPaxPlan || cb_BHS_SimulateBHS.Checked || cb_Gen_Tms.Checked) && tc_General.TabPages.Contains(tp_SIM_Start);

            cb_D_FPD.Enabled = bDeparture || bPaxPlan || paxPlanGeneration;
            cb_D_LFD.Enabled = bDeparture || bPaxPlan || paxPlanGeneration;
            cb_ex_DLoadFactors.Enabled = cb_D_LFD.Enabled;
            cb_D_ShowUp.Enabled = bDeparture || bPaxPlan || paxPlanGeneration;
            cb_ex_CI_ShowUp.Enabled = cb_D_ShowUp.Enabled;
            cb_D_OCT_CI.Enabled = bDeparture || bPaxPlan || paxPlanGeneration;
            cb_ex_OCT_CI.Enabled = cb_D_OCT_CI.Enabled;
            cb_D_OCT_MakeUp.Enabled = (bDeparture || bPaxPlan || bBagPlan || paxPlanGeneration) && (!bTrialVersion);
            cb_ex_OCT_MakeUp.Enabled = cb_D_OCT_MakeUp.Enabled;
            cb_D_BoardingGate.Enabled = bDeparture || bPaxPlan || paxPlanGeneration;
            cb_ex_OCT_BoardGate.Enabled = cb_D_BoardingGate.Enabled;
            // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
            cb_D_OCT_BagDrop.Enabled = bDeparture || bPaxPlan || paxPlanGeneration;
            cb_ex_OCT_BagDrop.Enabled = cb_D_OCT_BagDrop.Enabled;
            // >> Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
            // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            cb_d_EBSInputRate.Enabled = bDeparture || bPaxPlan || paxPlanGeneration;
            cb_ex_EBSInputRate.Enabled = cb_d_EBSInputRate.Enabled;

            cb_d_EBSOutputRate.Enabled = bDeparture || bPaxPlan || paxPlanGeneration;
            cb_ex_EBSOutputRate.Enabled = cb_d_EBSOutputRate.Enabled;
            // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

            cb_A_InterConnectingTimes.Enabled = bArrival || bPaxPlan || bDeparture || paxPlanGeneration; // << Task #9117 Pax2Sim - Static Analysis - FPD_EBS calculation - transfering pax missing
            cb_ex_Transfer_ICT.Enabled = cb_A_InterConnectingTimes.Enabled;
            cb_A_LoadFactors.Enabled = bArrival || bPaxPlan || paxPlanGeneration;
            //To-check
            cb_ex_ALoadFactors.Enabled = cb_A_LoadFactors.Enabled;
            cb_FPATable.Enabled = bArrival || bPaxPlan || paxPlanGeneration;
            cb_A_BaggageClaim.Enabled = bArrival || bPaxPlan || paxPlanGeneration;
            cb_ex_OCT_BaggageClaim.Enabled = cb_A_BaggageClaim.Enabled;

            // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            cb_A_BaggageLoadingRate.Enabled = bArrival || bPaxPlan || paxPlanGeneration;
            cb_ex_BaggageLoadingRate.Enabled = cb_A_BaggageLoadingRate.Enabled;
            cb_A_BaggageLoadingDelay.Enabled = bArrival || bPaxPlan || paxPlanGeneration;
            cb_ex_BaggageLoadingDelay.Enabled = cb_A_BaggageLoadingDelay.Enabled;
            // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay

            cb_D_FC.Enabled = bArrival || bPaxPlan || bDeparture || paxPlanGeneration;
            flightSubcategoriesComboBox.Enabled = bArrival || bPaxPlan || bDeparture || paxPlanGeneration;   // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            cb_D_AircraftType.Enabled = bArrival || bPaxPlan || bDeparture || paxPlanGeneration;
            cb_ex_Aircraft.Enabled = cb_D_AircraftType.Enabled;
            cb_St_Airline.Enabled = bArrival || bPaxPlan || bDeparture || paxPlanGeneration;
            cb_D_NbBags.Enabled = bArrival || bPaxPlan || bDeparture || paxPlanGeneration;
            cb_ex_NbBags.Enabled = cb_D_NbBags.Enabled;
            cb_D_NbTrolley.Enabled = bArrival || bPaxPlan || bDeparture || paxPlanGeneration;
            cb_ex_NbTrolley.Enabled = cb_D_NbTrolley.Enabled;
            cb_D_NbVisitors.Enabled = bArrival || bPaxPlan || bDeparture || paxPlanGeneration;
            cb_ex_NbVisitors.Enabled = cb_D_NbVisitors.Enabled;

            cb_D_AircraftLinks.Enabled = (bArrival && bDeparture) || bPaxPlan || paxPlanGeneration;

            gb_PP_Seed.Enabled = !bTrialVersion;
            gb_PP_TransferPreferences.Enabled = !bTrialVersion;
            gb_PP_OutOfRange.Enabled = !bTrialVersion;
            cb_PP_Opening.Enabled = !bTrialVersion;
            if (!cb_PP_Opening.Enabled)
                cb_PP_Opening.Checked = false;
            rb_PF_ProcessSchedule.Enabled = !bTrialVersion/* && PAX2SIM.bJNK*/;

            cb_PF_AnimatedFlow.Enabled = bAnimatedQueues && chk_Animated.Checked;
            chk_Animated.Enabled = bAnimatedQueues;

            cb_PF_AnimatedFlow.Visible = bAnimatedQueues;
            chk_Animated.Visible = bAnimatedQueues;
            UpdateComboBox(cb_PF_AnimatedFlow, cb_PF_AnimatedFlow.Enabled);

            UpdateComboBox(cb_D_FPD, cb_D_FPD.Enabled || bBagPlan);
            UpdateComboBox(cb_D_LFD, cb_D_LFD.Enabled);
            UpdateComboBox(cb_D_ShowUp, cb_D_ShowUp.Enabled);
            UpdateComboBox(cb_D_OCT_CI, cb_D_OCT_CI.Enabled || bBagPlan);
            UpdateComboBox(cb_D_OCT_MakeUp, cb_D_OCT_MakeUp.Enabled || bTrialVersion);
            UpdateComboBox(cb_D_BoardingGate, cb_D_BoardingGate.Enabled || bBagPlan);
            UpdateComboBox(cb_A_InterConnectingTimes, cb_A_InterConnectingTimes.Enabled);
            UpdateComboBox(cb_A_BaggageClaim, cb_A_BaggageClaim.Enabled || bBagPlan);

            UpdateComboBox(cb_D_OCT_BagDrop, cb_D_OCT_BagDrop.Enabled || bBagPlan); // >> Task #13361 FP AutoMod Data tables V3 // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop            

            // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            UpdateComboBox(cb_d_EBSInputRate, cb_d_EBSInputRate.Enabled);
            UpdateComboBox(cb_d_EBSOutputRate, cb_d_EBSOutputRate.Enabled);
            // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

            // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            UpdateComboBox(cb_A_BaggageLoadingRate, cb_A_BaggageLoadingRate.Enabled);
            UpdateComboBox(cb_A_BaggageLoadingDelay, cb_A_BaggageLoadingDelay.Enabled);
            // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            UpdateComboBox(reclaimSyncComboBox, reclaimSyncComboBox.Enabled);

            UpdateComboBox(cb_A_LoadFactors, cb_A_LoadFactors.Enabled);
            UpdateComboBox(cb_FPATable, cb_FPATable.Enabled || bBagPlan);
            UpdateComboBox(cb_D_FC, cb_D_FC.Enabled);
            UpdateComboBox(flightSubcategoriesComboBox, flightSubcategoriesComboBox.Enabled);   // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            UpdateComboBox(cb_D_AircraftType, cb_D_AircraftType.Enabled || bBagPlan);   // >> Task #13361 FP AutoMod Data tables V3
            UpdateComboBox(cb_D_NbBags, cb_D_NbBags.Enabled);
            UpdateComboBox(cb_D_NbTrolley, cb_D_NbTrolley.Enabled);
            UpdateComboBox(cb_D_NbVisitors, cb_D_NbVisitors.Enabled);

            UpdateComboBox(cb_D_AircraftLinks, cb_D_AircraftLinks.Enabled);
            UpdateComboBox(cb_St_Airline, cb_St_Airline.Enabled || bBagPlan);

            UpdateComboBox(cb_PP_Alloc_CapaQueues, cb_PP_Alloc_CapaQueues.Enabled);
            UpdateComboBox(cb_PP_GroupQueues, cb_PP_GroupQueues.Enabled);
            // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            UpdateComboBox(processCapaComboBox, processCapaComboBox.Enabled);
            // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            UpdateComboBox(cb_PP_Alloc_Itinerary, cb_PP_Alloc_Itinerary.Enabled);
            //UpdateComboBox(cb_PP_Alloc_OCT_BC, cb_PP_Alloc_OCT_BC.Enabled);
            //UpdateComboBox(cb_PP_Alloc_OCT_BG, cb_PP_Alloc_OCT_BG.Enabled);
            UpdateComboBox(cb_PP_Alloc_Passport, cb_PP_Alloc_Passport.Enabled);
            UpdateComboBox(cb_PP_Alloc_Process, cb_PP_Alloc_Process.Enabled);
            cb_ex_TimesProcess.Enabled = cb_PP_Alloc_Process.Enabled;
            UpdateComboBox(cb_PP_Alloc_Security, cb_PP_Alloc_Security.Enabled);
            // << Task #7570 new Desk and extra information for Pax -Phase I B
            UpdateComboBox(cb_PP_Alloc_UserProcess, cb_PP_Alloc_UserProcess.Enabled);
            // >> Task #7570 new Desk and extra information for Pax -Phase I B
            UpdateComboBox(cb_PP_Alloc_Transfer, cb_PP_Alloc_Transfer.Enabled);
            UpdateComboBox(cb_PF_Saturation, cb_PF_Saturation.Enabled);
            UpdateComboBox(cb_PP_Oneof, cb_PP_Oneof.Enabled);
            UpdateComboBox(cb_PP_Opening_CI, cb_PP_Opening_CI.Enabled);
            UpdateComboBox(usaStandardParamsComboBox, usaStandardParamsComboBox.Enabled);   // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
#if(PAXINOUTUTILISATION)
            UpdateComboBox(cb_PP_PaxIn, cb_PP_PaxIn.Enabled);
            UpdateComboBox(cb_PP_PaxOut, cb_PP_PaxOut.Enabled);
#endif

            //cb_ex_MakeUpSegregation.Enabled = cb_SIM_MakeUpSegregation.Checked;
            // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
            if (cb_BHS_SimulateBHS.Checked)
            {
                comboboxResultsProfile.Items.Clear();
                comboboxResultsProfile.Items.Add(BASIC_FILTER_PROFILE);
                comboboxResultsProfile.Items.Add(ADVANCED_FILTER_PROFILE);
                comboboxResultsProfile.Items.Add(CUSTOM_FILTER_PROFILE);
                comboboxResultsProfile.SelectedIndex = 0;

                flowTypeComboBox.Items.Clear();
                flowTypeComboBox.Items.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
                flowTypeComboBox.Items.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
                flowTypeComboBox.Items.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
                flowTypeComboBox.SelectedIndex = 0;

                updateSelectedFlowsLabel(flowTypes);
            }
            if (SourceAnalysis != null)
            {
                refreshAnalysisResultsFiltersTab(SourceAnalysis.analysisResultsFilters);

                if (SourceAnalysis.analysisResultsFilters != null && SourceAnalysis.analysisResultsFilters.Count > 0)
                {
                    if (SourceAnalysis.flowTypes == null || SourceAnalysis.flowTypes.Count == 0)
                    {
                        SourceAnalysis.flowTypes.Clear();
                        SourceAnalysis.flowTypes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
                        //SourceAnalysis.flowTypes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
                        SourceAnalysis.flowTypes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
                        //SourceAnalysis.flowTypes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
                        SourceAnalysis.flowTypes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
                    }
                }
                updateSelectedFlowsLabel(SourceAnalysis.flowTypes);
            }
            Dictionary<String, List<String>> userDataDictionary = Donnees.getUserData();
            if (userDataDictionary == null || userDataDictionary.Count == 0)
            {
                cb_SIM_ExportUserData.Checked = false;
                clearAutomodUserDataCheckBox.Checked = true;
            }
            // << Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
        }
        #endregion

        #region Pour les onglets visibles.
        private static int getIndex(TabPage[] Pages, TabPage tpLookedPage)
        {
            if (Pages == null)
                return -1;
            if (tpLookedPage == null)
                return -1;
            for (int i = 0; i < Pages.Length; i++)
            {
                if (Pages[i] == tpLookedPage)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// Fonction qui se charge de mettre à jour les onglets qui doivent être affichés.
        /// </summary>
        /// <param name="bStatic"></param>
        /// <param name="bDynamic"></param>
        private void UpdateVisible(bool bStatic, bool bDynamic)
        {
            if (ttpOrderTabs == null)
                return;
            bool[] bVisible = new bool[ttpOrderTabs.Length];
            int i;
            for (i = 0; i < bVisible.Length; i++)
                bVisible[i] = true;
            int iIndex;
            //On récupère le mode à utiliser
            bool bBHSMode = (((epPerimetre_ == PAX2SIM.EnumPerimetre.BHS) || (epPerimetre_ == PAX2SIM.EnumPerimetre.TMS)) && (bDynamic)) && PAX2SIM.bBHS;
            bool bTMSMode = (((epPerimetre_ == PAX2SIM.EnumPerimetre.BHS) || (epPerimetre_ == PAX2SIM.EnumPerimetre.TMS)) && (bDynamic)) && PAX2SIM.bTMS;

            //cb_SIM_ExportUserData.Visible = !(PAX2SIM.EnumPerimetre.PAX == epPerimetre_);
            cb_BHS_SimulateBHS.Visible = bBHSMode;
            cb_Gen_Tms.Visible = bTMSMode;
            if (!bBHSMode)
                cb_BHS_SimulateBHS.Checked = false;
            if (!bTMSMode)
                cb_Gen_Tms.Checked = false;

            bool bBHSActivate = bBHSMode && (cb_BHS_SimulateBHS.Checked);
            bool bTMSActivate = bTMSMode && (cb_Gen_Tms.Checked);
            //cb_SIM_MakeUpSegregation.Visible = false;
            cb_ex_MakeUpSegregation.Visible = cb_SIM_MakeUpSegregation.Visible;

            if ((PAX2SIM.bBHS_BagPlan) && (!PAX2SIM.bBHS_PaxPlan))
            {
                //On doit afficher l'option bagPlan. On doit donc déplacer l'ensemble de la fenêtre BHS.
                if (!rb_BHS_BagPlan.Visible)
                {
                    rb_BHS_BagPlan.Visible = true;
                    lbl_BHS_BagPlanScenario.Visible = true;
                    cb_BHS_BagPlan.Visible = true;
                    cb_BHS_Terminal.Visible = true;///
                    gb_BHSFlows.Size = new Size(485, 91);
                    lbl_BHS_Terminal.Location = new Point(28, 118);
                    cb_BHS_Terminal.Location = new Point(113, 118);
                    gb_BHS_Information.Location = new Point(10, 145);
                    rb_BHS_UseMeanFlow.Location = new Point(18, 58);
                }
            }

            if ((bBHSActivate) || (bTMSActivate))
            {
                #region Gestion des cas où le mean flows est fixé sur la clef
                if (!PAX2SIM.bBHS_MeanFlows)
                {
                    //Si la clef n'est pas bloquée en mean flow.
                    if (PAX2SIM.bBHS_PaxPlan)
                    {
                        //PaxPlan obligatoire.
                        rb_BHS_UseMeanFlow.Enabled = false;
                        rb_BHS_UsePaxPlan.Checked = true;

                    }
                    else if (bTMSActivate)
                    {
                        //Si le tms est activés l'option mean flows n'est pas disponible.
                        rb_BHS_UseMeanFlow.Enabled = false;
                        rb_BHS_UsePaxPlan.Checked = true;
                        rb_BHS_BagPlan.Enabled = false;
                    }
                    else
                    {
                        //Si le TMS n'est pas activé, alors le mean flows est activable.
                        rb_BHS_UseMeanFlow.Enabled = true;
                        rb_BHS_BagPlan.Enabled = true;
                    }
                }
                else
                {
                    //L'option Mean flows est activée, il faut donc bloqué l'application dans ce mode.
                    rb_BHS_UseMeanFlow.Checked = true;
                    gb_BHSFlows.Enabled = false;
                }
                #endregion


                if ((rb_BHS_UseMeanFlow.Checked) && (bBHSActivate))
                {
                    cb_PP_GeneratePaxPlan.Checked = false;
                    cb_PP_GeneratePaxPlan.Enabled = false;
                    cb_D_CalcDeparturePeak.Checked = false;
                    cb_D_CalcDeparturePeak.Enabled = false;
                    cb_CalcArrivalPeak.Checked = false;
                    cb_CalcArrivalPeak.Enabled = false;
                    cbGeneratePaxPlanForStatic.Checked = false; // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#8
                    cbGeneratePaxPlanForStatic.Enabled = false;
                    gb_Dates.Enabled = true;
                    // >> Task #10263 Pax2Sim - BHS Analysis - SS, AR enabled for selected bagplan
                    dtp_BeginTime.Enabled = true;
                    lbl_BeginTime.Enabled = true;
                    dtp_EndTime.Enabled = true;
                    lbl_EndTime.Enabled = true;
                    // << Task #10263 Pax2Sim - BHS Analysis - SS, AR enabled for selected bagplan
                    cb_Gen_Tms.Enabled = false;
                    cb_Gen_Tms.Checked = false;
                }
                else if ((rb_BHS_BagPlan.Checked) && (bBHSActivate))
                {
                    cb_Gen_Tms.Checked = false;
                    cb_Gen_Tms.Enabled = false;
                    cb_PP_GeneratePaxPlan.Checked = false;
                    cb_PP_GeneratePaxPlan.Enabled = false;
                    cb_D_CalcDeparturePeak.Checked = false;
                    cb_D_CalcDeparturePeak.Enabled = false;
                    cb_CalcArrivalPeak.Checked = false;
                    cb_CalcArrivalPeak.Enabled = false;
                    cbGeneratePaxPlanForStatic.Checked = false; // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#8
                    cbGeneratePaxPlanForStatic.Enabled = false;
                    // >> Task #10263 Pax2Sim - BHS Analysis - SS, AR enabled for selected bagplan
                    //gb_Dates.Enabled = false;
                    gb_Dates.Enabled = true;
                    dtp_BeginTime.Enabled = false;
                    lbl_BeginTime.Enabled = false;
                    dtp_EndTime.Enabled = false;
                    lbl_EndTime.Enabled = false;
                    // << Task #10263 Pax2Sim - BHS Analysis - SS, AR enabled for selected bagplan
                }
                else if ((rb_BHS_UsePaxPlan.Checked) && ((bBHSActivate) || bTMSActivate))
                {
                    cb_PP_GeneratePaxPlan.Checked = true;
                    cb_PP_GeneratePaxPlan.Enabled = false;

                    cb_D_CalcDeparturePeak.Enabled = true;
                    cb_CalcArrivalPeak.Enabled = true;

                    if (currentParameters != null)
                        cbGeneratePaxPlanForStatic.Checked = currentParameters.generatePaxPlanForStaticAnalysis;
                    else
                        cbGeneratePaxPlanForStatic.Checked = true;  // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#8
                    cbGeneratePaxPlanForStatic.Enabled = true;
                    cb_Gen_Tms.Enabled = true;
                    gb_Dates.Enabled = true;
                    // >> Task #10263 Pax2Sim - BHS Analysis - SS, AR enabled for selected bagplan
                    dtp_BeginTime.Enabled = true;
                    lbl_BeginTime.Enabled = true;
                    dtp_EndTime.Enabled = true;
                    lbl_EndTime.Enabled = true;
                    // << Task #10263 Pax2Sim - BHS Analysis - SS, AR enabled for selected bagplan
                }
            }
            else
            {
                //cb_SIM_MakeUpSegregation.Visible = false;
                cb_ex_MakeUpSegregation.Visible = cb_SIM_MakeUpSegregation.Visible;
                cb_PP_GeneratePaxPlan.Enabled = true;
                cb_D_CalcDeparturePeak.Enabled = true;
                cb_CalcArrivalPeak.Enabled = true;
                cb_Gen_Tms.Enabled = true;
            }
            bool bDataActivate = ((cb_D_CalcDeparturePeak.Checked)
                                    || (cb_CalcArrivalPeak.Checked)
                                    || (cb_PP_GeneratePaxPlan.Checked)
                                    || (rb_BHS_BagPlan.Checked && bBHSActivate));
            bool bPaxPlanActivate = (cb_PP_GeneratePaxPlan.Checked);
            bool bPaxFlowActivate = (cb_PP_GeneratePaxPlan.Checked) && bDynamic;
            bool bUserActivate = (getParamUserData() != null) && (cb_PP_GeneratePaxPlan.Checked || cb_BHS_SimulateBHS.Checked);  //bDynamic && (getParamUserData() != null);    // >> Task #13880 Various UI improvements and fixes
            bool bSimulationActivate = (bDynamic) && ((cb_PP_GeneratePaxPlan.Checked) || (cb_BHS_SimulateBHS.Checked));
            // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            bool isUserAttributesTabVisible = bDynamic && (getUserAttributesNamesList().Count > 0)
                                                && (cb_D_CalcDeparturePeak.Checked || cb_CalcArrivalPeak.Checked
                                                    || cb_PP_GeneratePaxPlan.Checked || (rb_BHS_BagPlan.Checked && bBHSActivate));
            // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab

            iIndex = getIndex(ttpOrderTabs, tp_Static);
            if (iIndex != -1)
                bVisible[iIndex] = bDataActivate;
            iIndex = getIndex(ttpOrderTabs, tp_PaxPlan);
            if (iIndex != -1)
                bVisible[iIndex] = bPaxPlanActivate;
            iIndex = getIndex(ttpOrderTabs, tp_Dynamic);
            if (iIndex != -1)
                bVisible[iIndex] = bPaxFlowActivate;
            iIndex = getIndex(ttpOrderTabs, tp_BHS);
            if (iIndex != -1)
                bVisible[iIndex] = bBHSActivate;
            iIndex = getIndex(ttpOrderTabs, tp_resultsFilters);
            if (iIndex != -1)
                bVisible[iIndex] = bBHSActivate; //false;    // V6.225 without _Times
            // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            iIndex = getIndex(ttpOrderTabs, tp_UserAttributes);
            if (iIndex != -1)
                bVisible[iIndex] = isUserAttributesTabVisible;
            // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            iIndex = getIndex(ttpOrderTabs, tp_UserData);
            if (iIndex != -1)
                bVisible[iIndex] = bUserActivate;
            iIndex = getIndex(ttpOrderTabs, tp_SIM_Start);
            if (iIndex != -1)
                bVisible[iIndex] = bSimulationActivate;
            iIndex = 0;
            for (i = 0; i < bVisible.Length; i++)
            {
                if (tc_General.TabPages.Contains(ttpOrderTabs[i]))
                {
                    if (!bVisible[i])
                        tc_General.TabPages.Remove(ttpOrderTabs[i]);
                    else
                        iIndex++;
                }
                else
                {
                    if (bVisible[i])
                    {
                        if (iIndex >= tc_General.TabPages.Count)
                        {
                            tc_General.TabPages.Add(ttpOrderTabs[i]);
                        }
                        else
                        {
                            tc_General.TabPages.Insert(iIndex, ttpOrderTabs[i]);
                        }
                        iIndex++;
                    }
                }
            }
            // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            if (useNbOfPaxCheckBox.Checked)
            {
                nbOfPaxComboBox.Enabled = true;
                nbOfPaxLbl.Enabled = true;
            }
            else
            {
                nbOfPaxComboBox.Text = "";
                nbOfPaxComboBox.Items.Clear();
                nbOfPaxComboBox.Enabled = false;
                nbOfPaxLbl.Enabled = false;
            }

            if (useNbOfBagsCheckBox.Checked)
            {
                nbOfBagsComboBox.Enabled = true;
                nbOfBagsLbl.Enabled = true;
            }
            else
            {
                nbOfBagsComboBox.Text = "";
                nbOfBagsComboBox.Items.Clear();
                nbOfBagsComboBox.Enabled = false;
                nbOfBagsLbl.Enabled = false;
            }
            // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

            if (useFPParametersTableCheckBox.Checked)   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            {
                fpParametersTableComboBox.Enabled = true;
            }
            else
            {
                fpParametersTableComboBox.Text = "";
                fpParametersTableComboBox.Items.Clear();
                fpParametersTableComboBox.Enabled = false;
            }
            updateStaticPaxPlanTabVisibility(); // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#5            
            cbGeneratePaxPlanForStatic.Visible = !PAX2SIM.bBHS_MeanFlows && PAX2SIM.bStatic;    // PAX key flag disabled
        }

        // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        private List<String> getUserAttributesNamesList()
        {
            List<String> userAttributesNamesList = new List<String>();

            DataTable userAttributesTable = Donnees.getTable("Input", GlobalNames.sUserAttributesTableName);
            if (userAttributesTable != null && userAttributesTable.Rows != null
                && userAttributesTable.Rows.Count > 0)
            {
                int userAttributeNameColumnIndex = -1;
                if (userAttributesTable.Columns != null)
                    userAttributeNameColumnIndex = userAttributesTable.Columns.IndexOf(GlobalNames.sUserAttributes_ColumnName);

                if (userAttributeNameColumnIndex != -1)
                {
                    foreach (DataRow dr in userAttributesTable.Rows)
                    {
                        String userAttributeName = dr[userAttributeNameColumnIndex].ToString();

                        // >> Task #10764 Pax2Sim - new User attributes for Groups
                        if (GlobalNames.PAX_GROUP_USER_ATTRIBUTES_LIST.Contains(userAttributeName))
                        {
                            //we don't add a user attribute table for the Scenario's User attributes tab for the Pax_x_Group attributes
                            //as these are only used for the Planning and PaxPlan tables and receive values programmatically
                            continue;
                        }
                        // << Task #10764 Pax2Sim - new User attributes for Groups

                        if (!GlobalNames.nonUserAttributesExceptionsList.Contains(userAttributeName))
                            userAttributesNamesList.Add(userAttributeName);
                    }
                }
            }
            return userAttributesNamesList;
        }
        // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
        #endregion

        #region Gestion des dates du formulaire.
        private void cb_FPD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sFPAFullName = cb_FPATable.Text;
            if (sFPAFullName == "")
                sFPAFullName = cb_FPATable.Text;

            string sFPDFullName = cb_D_FPD.Text;
            if (sFPDFullName == "")
                sFPDFullName = cb_D_FPD.Text;
            if (!isTraceAnalysisScenario)   // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17
                ChangeDate(ConvertToName(sFPAFullName), ConvertToName(sFPDFullName), dtp_BeginTime, dtp_EndTime);
        }

        private void dtp_BeginTime_ValueChanged(object sender, EventArgs e)
        {
            dtp_EndTime.MinDate = dtp_BeginTime.Value;
        }
        private void cb_SamplingStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iMax = 60;
            int iValue;

            cb_AnalysisRange.Items.Clear();
            cb_StatisticStep.Items.Clear();
            cb_StatisticStep.Text = "";
            cb_AnalysisRange.Text = "";
            if (!Int32.TryParse(cb_SamplingStep.Text, out iValue))
            {
                return;
            }
            for (int i = iValue; i <= iMax; i++)
            {
                if (((i % iValue) == 0) && ((iMax % i) == 0))
                {
                    cb_AnalysisRange.Items.Add(i.ToString());
                    cb_StatisticStep.Items.Add(i.ToString());
                }
            }
            if (cb_AnalysisRange.Items.Count > 0)  // >> Task #18306 PAX2SIM - BHS - Sorter occupation
            {
                cb_AnalysisRange.SelectedIndex = cb_AnalysisRange.Items.Count - 1;
            }
            cb_StatisticStep.SelectedIndex = 0;
        }

        #region Fonctions pour le choix des dates min et max.
        private void ChangeDate(String FPA, String FPD, DateTimePicker dateBegin, DateTimePicker dateEnd)
        {
            if ((((FPA == "") || (FPA == null)) && ((FPD == "") || (FPD == null))))
            {
                return;
            }
            DateTime minFPA = new DateTime(),
                    minFPD = new DateTime(),
                    maxFPA = new DateTime(),
                    maxFPD = new DateTime();
            bool bFPA = false;
            bool bFPD = false;
            if ((FPA != "") && (FPA != null))
            {
                bFPA = findMinMaxValues(FPA, out minFPA, out maxFPA);
                /*if (!findMinMaxValues(FPA, out minFPA, out maxFPA))
                {
                    return;
                }*/
            }
            if ((FPD != "") && (FPD != null))
            {
                bFPD = findMinMaxValues(FPD, out minFPD, out maxFPD);
                /*                if (!findMinMaxValues(FPD, out minFPD, out maxFPD))
                                {
                                    return;
                                }*/
            }
            if ((!bFPA) && (!bFPD))
                return;
            if ((FPA != "") && (FPD != "") && bFPA && bFPD)
            {
                if (minFPA > minFPD)
                {
                    minFPA = minFPD;
                }
                if (maxFPA < maxFPD)
                {
                    maxFPA = maxFPD;
                }
            }
            else if ((FPD != "") && bFPD)
            {
                minFPA = minFPD;
                maxFPA = maxFPD;
            }

            minFPD = minFPA.Date.AddDays(-1);
            maxFPD = maxFPA.Date.AddDays(2);

            dateBegin.MaxDate = DateTimePicker.MaximumDateTime;
            dateBegin.MinDate = DateTimePicker.MinimumDateTime;
            dateEnd.MinDate = DateTimePicker.MinimumDateTime;
            dateEnd.MaxDate = DateTimePicker.MaximumDateTime;

            // << Task #9252 Pax2Sim - Scenario Properties - Begin / End date setting
            if ((dateEnd.Value < minFPD) ||
                    (dateEnd.Value > maxFPD))
            {
                DateTime date = maxFPA.Date.AddHours(23).AddMinutes(59);
                dateEnd.Value = date;// maxFPA.Date.AddHours(23).AddMinutes(59);
            }

            if ((dateBegin.Value < minFPD) ||
                (dateBegin.Value > maxFPD))
                dateBegin.Value = minFPA.Date;

            /*            
                        if ((dateBegin.Value < minFPD) ||
                            (dateBegin.Value > maxFPD))
                            dateBegin.Value = minFPA.Date;

                        if ((dateEnd.Value < minFPD) ||
                                (dateEnd.Value > maxFPD))
                        {
                            DateTime date = maxFPA.Date.AddHours(23).AddMinutes(59);
                            dateEnd.Value = date;// maxFPA.Date.AddHours(23).AddMinutes(59);
                        }
            */
            // >> Task #9252 Pax2Sim - Scenario Properties - Begin / End date setting

            if (dateEnd.Value < dateBegin.Value)
                dateEnd.Value = maxFPD;
            dateBegin.MaxDate = maxFPD;
            dateBegin.MinDate = minFPD;
            dateEnd.MinDate = minFPD;
            dateEnd.MaxDate = maxFPD;
        }
        private bool findMinMaxValues(String Name, out DateTime min, out DateTime max)
        {
            DataTable fpTable = Donnees.getTable("Input", Name);
            if (fpTable == null)
            {
                min = DateTime.MinValue;
                max = DateTime.MinValue;
                return false;
            }
            max = OverallTools.DataFunctions.valeurMaximaleDansColonne(fpTable, 1, 2);
            min = OverallTools.DataFunctions.valeurMinimaleDansColonne(fpTable, 1, 2);
            if ((max == DateTime.MinValue) || (min == DateTime.MinValue))
                return false;
            return true;
        }
        #endregion
        #endregion

        #region Les fonctions pour la gestion du premier onglet : Le calcul des pics de passagers en partance.
        private bool VerifDates()
        {
            if (dtp_BeginTime.Value >= dtp_EndTime.Value)   // << Task #9221 Pax2Sim - Static Analysis - FPD_FPA Airport Statistics - CI/Transfer bags columns
            {
                MessageBox.Show("The dates are not valids. \"Begin\" date must be before \"End\" date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtp_EndTime.Focus();
                return false;
            }
            return true;
        }
        private bool verifCalcPeakDeparture()
        {
            if (!VerifDates())
                return false;
            bool bFilled = (cb_D_FPD.Text == "");
            bFilled = bFilled || (flightSubcategoriesComboBox.Text == "");  // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            bFilled = bFilled || (cb_D_FC.Text == "");
            bFilled = bFilled || (cb_D_AircraftType.Text == "");
            bFilled = bFilled || (cb_D_OCT_MakeUp.Text == "");
            bFilled = bFilled || (cb_D_BoardingGate.Text == "");
            bFilled = bFilled || (cb_D_LFD.Text == "");
            bFilled = bFilled || (cb_D_ShowUp.Text == "");
            bFilled = bFilled || (cb_D_NbBags.Text == "");
            bFilled = bFilled || (cb_D_NbVisitors.Text == "");
            bFilled = bFilled || (cb_D_NbTrolley.Text == "");
            bFilled = bFilled || (cb_D_OCT_CI.Text == "");
            bFilled = bFilled || (cb_St_Airline.Text == "");
            if (cb_CalcArrivalPeak.Checked)
            {
                bFilled = bFilled || (cb_D_AircraftLinks.Text == "");
                bFilled = bFilled || (cb_FPATable.Text == "");
            }
            bFilled = bFilled || (cb_D_OCT_BagDrop.Text == "");// << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
            bFilled = bFilled || (cb_A_InterConnectingTimes.Text == "");    // << Task #9117 Pax2Sim - Static Analysis - FPD_EBS calculation - transfering pax missing
            // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            //bFilled = bFilled || (cb_d_EBSInputRate.Text == "");  // >> Task #10615 Pax2Sim - Pax analysis - Summary - small changes
            //bFilled = bFilled || (cb_d_EBSOutputRate.Text == ""); // >> Task #10615 Pax2Sim - Pax analysis - Summary - small changes
            // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

            // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            if (useNbOfPaxCheckBox.Checked)
                bFilled = bFilled || (nbOfPaxComboBox.Text == "");
            if (useNbOfBagsCheckBox.Checked)
                bFilled = bFilled || (nbOfBagsComboBox.Text == "");
            // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            if (useFPParametersTableCheckBox.Checked)   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            {
                bFilled = bFilled || (fpParametersTableComboBox.Text == "");
            }
            if (bFilled)
            {
                MessageBox.Show("You must select a table for each field.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tp_Static.Select();
                return false;
            }
            List<String> lsTables = new List<string>();
            lsTables.Add(ConvertToName(cb_D_FPD.Text));
            lsTables.Add(ConvertToName(cb_D_FC.Text));
            lsTables.Add(ConvertToName(cb_D_AircraftType.Text));
            lsTables.Add(ConvertToName(cb_D_OCT_MakeUp.Text));
            lsTables.Add(ConvertToName(cb_D_BoardingGate.Text));
            lsTables.Add(ConvertToName(cb_D_LFD.Text));
            lsTables.Add(ConvertToName(cb_D_ShowUp.Text));
            lsTables.Add(ConvertToName(cb_A_InterConnectingTimes.Text));    // << Task #9117 Pax2Sim - Static Analysis - FPD_EBS calculation - transfering pax missing
            lsTables.Add(ConvertToName(cb_D_NbBags.Text));
            lsTables.Add(ConvertToName(cb_D_NbVisitors.Text));
            lsTables.Add(ConvertToName(cb_D_NbTrolley.Text));
            lsTables.Add(ConvertToName(cb_D_OCT_CI.Text));
#if(PAXINOUTUTILISATION)
            lsTables.Add(ConvertToName(cb_PP_PaxIn.Text));
            lsTables.Add(ConvertToName(cb_PP_PaxOut.Text));
#endif
            //Dictionary<String, DataTable> htTables = new Dictionary<String, DataTable>();
            //htTables.Add(cb_D_FPD.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_FPD.Text)));
            //htTables.Add(cb_D_FC.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_FC.Text)));
            //htTables.Add(cb_D_AircraftType.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_AircraftType.Text)));
            //htTables.Add(cb_D_OCT_MakeUp.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_OCT_MakeUp.Text)));
            //htTables.Add(cb_D_BoardingGate.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_BoardingGate.Text)));
            //htTables.Add(cb_D_LFD.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_LFD.Text)));
            //htTables.Add(cb_D_ShowUp.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_ShowUp.Text)));
            //htTables.Add(cb_D_NbBags.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbBags.Text)));
            //htTables.Add(cb_D_NbVisitors.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbVisitors.Text)));
            //htTables.Add(cb_D_NbTrolley.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbTrolley.Text)));
            //htTables.Add(cb_D_OCT_CI.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_OCT_CI.Text)));

            if (cb_CalcArrivalPeak.Checked)
            {
                lsTables.Add(ConvertToName(cb_FPATable.Text));
                lsTables.Add(ConvertToName(cb_D_AircraftLinks.Text));
                //We have to check the content of the Aircraft link table. So we have also to check the content of the FPA table.
                //htTables.Add(cb_FPATable.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_FPATable.Text)));
                //htTables.Add(cb_D_AircraftLinks.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_AircraftLinks.Text)));
            }
            //Ici vérifier la validité de toutes les informations entre toutes les tables : FC inconnue ou Avion non valide.
            ArrayList ErrorList = Donnees.CheckTable(lsTables, true, false);
            if (ErrorList != null)
            {
                ErrorList.Insert(0, "Error in the definition for the calc of Peak Departure");
                SetErrors(ErrorList);
            }
            return (ErrorList == null);
        }
        #endregion

        #region Fonctions pour la gestion de l'onglet Arrival
        private bool verifCalcPeakArrival()
        {

            if (!VerifDates())
                return false;
            bool bFilled = (cb_FPATable.Text == "");
            bFilled = bFilled || (cb_D_FC.Text == "");
            bFilled = bFilled || (flightSubcategoriesComboBox.Text == "");  // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            bFilled = bFilled || (cb_D_AircraftType.Text == "");
            bFilled = bFilled || (cb_A_InterConnectingTimes.Text == "");
            bFilled = bFilled || (cb_A_LoadFactors.Text == "");
            bFilled = bFilled || (cb_D_NbBags.Text == "");
            bFilled = bFilled || (cb_D_NbVisitors.Text == "");
            bFilled = bFilled || (cb_D_NbTrolley.Text == "");
            bFilled = bFilled || (cb_St_Airline.Text == "");
            bFilled = bFilled || (cb_A_BaggageClaim.Text == "");
            // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            //bFilled = bFilled || (cb_A_BaggageLoadingRate.Text == "");   // >> Task #10615 Pax2Sim - Pax analysis - Summary - small changes
            //bFilled = bFilled || (cb_A_BaggageLoadingDelay.Text == "");  // >> Task #10615 Pax2Sim - Pax analysis - Summary - small changes
            // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay

            // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            if (useNbOfPaxCheckBox.Checked)
                bFilled = bFilled || (nbOfPaxComboBox.Text == "");
            if (useNbOfBagsCheckBox.Checked)
                bFilled = bFilled || (nbOfBagsComboBox.Text == "");
            // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            if (useFPParametersTableCheckBox.Checked)   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            {
                bFilled = bFilled || (fpParametersTableComboBox.Text == "");
            }
            if (bFilled)
            {
                MessageBox.Show("You must select a table for each field.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tp_Static.Select();
                return false;
            }
            List<String> lsTables = new List<string>();
            lsTables.Add(ConvertToName(cb_FPATable.Text));
            lsTables.Add(ConvertToName(cb_D_FC.Text));
            lsTables.Add(ConvertToName(cb_D_AircraftType.Text));
            lsTables.Add(ConvertToName(cb_A_LoadFactors.Text));
            lsTables.Add(ConvertToName(cb_A_InterConnectingTimes.Text));
            lsTables.Add(ConvertToName(cb_D_NbBags.Text));
            lsTables.Add(ConvertToName(cb_D_NbVisitors.Text));
            lsTables.Add(ConvertToName(cb_D_NbTrolley.Text));
            //Dictionary<String, DataTable> htTables = new Dictionary<String, DataTable>();
            //htTables.Add(cb_FPATable.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_FPATable.Text)));
            //htTables.Add(cb_D_FC.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_FC.Text)));
            //htTables.Add(cb_D_AircraftType.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_AircraftType.Text)));
            //htTables.Add(cb_A_LoadFactors.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_A_LoadFactors.Text)));
            //htTables.Add(cb_A_InterConnectingTimes.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_A_InterConnectingTimes.Text)));
            //htTables.Add(cb_D_NbBags.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbBags.Text)));
            //htTables.Add(cb_D_NbVisitors.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbVisitors.Text)));
            //htTables.Add(cb_D_NbTrolley.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbTrolley.Text)));
            //Ici vérifier la validité de toutes les informations entre toutes les tables : FC inconnue ou Avion non valide.
            ArrayList ErrorList = Donnees.CheckTable(lsTables, true, false);


            if (ErrorList != null)
            {
                ErrorList.Insert(0, "Error in the definition for the calc of Peak Arrival");
                SetErrors(ErrorList);
            }
            return (ErrorList == null);
        }
        #endregion

        #region Fonction pour la gestion de l'onglet PaxPlan
        private void cb_UseDefinedSeed_CheckedChanged(object sender, EventArgs e)
        {
            mb_PP_UseDefinedSeed.Enabled = cb_PP_UseDefinedSeed.Checked;
            if (!mb_PP_UseDefinedSeed.Enabled)
                mb_PP_UseDefinedSeed.Text = "";
            else
                mb_PP_UseDefinedSeed.Text = "1";
            //mffFileSeedMaskedTextBox.Enabled = cb_PP_UseDefinedSeed.Checked;    // >> Bug #14900 MFF file not created
            //if (!mffFileSeedMaskedTextBox.Enabled)
            //mffFileSeedMaskedTextBox.Text = "1";
        }

        private void cb_UseSameSeed_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_DefineSeeds_Click(object sender, EventArgs e)
        {

        }


        private bool verifCalcPaxPlan()
        {
            if (!VerifDates())
                return false;
            bool bFilled = (cb_D_FPD.Text == "");
            bFilled = bFilled || (cb_FPATable.Text == "");
            bFilled = bFilled || (cb_D_FC.Text == "");
            bFilled = bFilled || (flightSubcategoriesComboBox.Text == "");  // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            bFilled = bFilled || (cb_D_AircraftType.Text == "");
            bFilled = bFilled || (cb_D_OCT_MakeUp.Text == "");
            bFilled = bFilled || (cb_D_BoardingGate.Text == "");
            bFilled = bFilled || (cb_D_LFD.Text == "");
            bFilled = bFilled || (cb_A_LoadFactors.Text == "");
            bFilled = bFilled || (cb_A_InterConnectingTimes.Text == "");
            bFilled = bFilled || (cb_D_ShowUp.Text == "");
            bFilled = bFilled || (cb_D_NbBags.Text == "");
            bFilled = bFilled || (cb_D_NbVisitors.Text == "");
            bFilled = bFilled || (cb_D_NbTrolley.Text == "");
            bFilled = bFilled || (cb_D_AircraftLinks.Text == "");
            bFilled = bFilled || (cb_D_OCT_CI.Text == "");
            bFilled = bFilled || (cb_St_Airline.Text == "");
            bFilled = bFilled || (cb_A_BaggageClaim.Text == "");

            bool itineraryTableCheckFailed = false; // >> Bug #14377 Tables Data check improvement
            if (rb_PF_Regular.Checked)
            {
                bFilled = bFilled || (cb_PP_Alloc_Process.Text == "");
                bFilled = bFilled || (cb_PP_Alloc_Itinerary.Text == "");
                bFilled = bFilled || (cb_PP_Alloc_CapaQueues.Text == "");
                bFilled = bFilled || (cb_PP_GroupQueues.Text == "");
                bFilled = bFilled || (processCapaComboBox.Text == "");  // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection

                if (cb_PP_Alloc_Itinerary.Text != "")
                {
                    DataTable itineraryTable = Donnees.getTable("Input", ConvertToName(cb_PP_Alloc_Itinerary.Text));
                    if (itineraryTable == null || itineraryTable.Rows.Count == 0)
                        itineraryTableCheckFailed = true;
                }
            }
            else
            {
                bFilled = bFilled || (cb_PF_ProcessSchedule.Text == "");
            }
            bFilled = bFilled || (cb_PP_Oneof.Text == "");
            bFilled = bFilled || (cb_D_OCT_CI.Text == "");
            //bFilled = bFilled || (cb_PP_Alloc_OCT_BG.Text == "");
            //bFilled = bFilled || (cb_PP_Alloc_OCT_BC.Text == "");
            bFilled = bFilled || (cb_PP_Alloc_Passport.Text == "");
            bFilled = bFilled || (cb_PP_Alloc_Security.Text == "");
            // << Task #7570 new Desk and extra information for Pax -Phase I B
            bFilled = bFilled || (cb_PP_Alloc_UserProcess.Text == "");
            // >> Task #7570 new Desk and extra information for Pax -Phase I B
            bFilled = bFilled || (cb_PP_Alloc_Transfer.Text == "");
            bFilled = bFilled || (cb_PF_Saturation.Text == "");
            bFilled = bFilled || (mb_PP_UseDefinedSeed.Text == "");
            //bFilled = bFilled || (mffFileSeedMaskedTextBox.Text == "");// >> Bug #14900 MFF file not created
            bFilled = bFilled || (simulationEngineSeedMaskedTextBox1.Text == "");// >> Bug #14900 MFF file not created - C#6

            // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            if (useNbOfPaxCheckBox.Checked)
                bFilled = bFilled || (nbOfPaxComboBox.Text == "");
            if (useNbOfBagsCheckBox.Checked)
                bFilled = bFilled || (nbOfBagsComboBox.Text == "");
            // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            if (useFPParametersTableCheckBox.Checked)   // >> Task #17690 PAX2SIM - Flight Plan Parameters table
            {
                bFilled = bFilled || (fpParametersTableComboBox.Text == "");
            }
            if (bFilled)
            {
                MessageBox.Show("You must select a table for each field.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tp_Dynamic.Select();
                return false;
            }
            if (itineraryTableCheckFailed)
            {
                MessageBox.Show("The paths between the airport's functions must be defined." + Environment.NewLine + "Please check the Process Flow table.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tp_Dynamic.Select();
                return false;
            }

            ArrayList ErrorList;
            List<String> lsTables = new List<string>();
            lsTables.Add(ConvertToName(cb_D_FPD.Text));

            lsTables.Add(ConvertToName(cb_FPATable.Text));
            lsTables.Add(ConvertToName(cb_D_FC.Text));
            lsTables.Add(ConvertToName(cb_D_AircraftType.Text));
            lsTables.Add(ConvertToName(cb_D_LFD.Text));
            lsTables.Add(ConvertToName(cb_A_LoadFactors.Text));
            lsTables.Add(ConvertToName(cb_A_InterConnectingTimes.Text));
            lsTables.Add(ConvertToName(cb_D_ShowUp.Text));

            lsTables.Add(ConvertToName(cb_D_NbBags.Text));
            lsTables.Add(ConvertToName(cb_D_NbVisitors.Text));
            lsTables.Add(ConvertToName(cb_D_NbTrolley.Text));
            lsTables.Add(ConvertToName(cb_D_AircraftLinks.Text));
            //Dictionary<String, DataTable> htTables = new Dictionary<String, DataTable>();
            //htTables.Add(cb_D_FPD.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_FPD.Text)));

            //htTables.Add(cb_FPATable.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_FPATable.Text)));
            //htTables.Add(cb_D_FC.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_FC.Text)));
            //htTables.Add(cb_D_AircraftType.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_AircraftType.Text)));
            //htTables.Add(cb_D_LFD.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_LFD.Text)));
            //htTables.Add(cb_A_LoadFactors.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_A_LoadFactors.Text)));
            //htTables.Add(cb_A_InterConnectingTimes.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_A_InterConnectingTimes.Text)));
            //htTables.Add(cb_D_ShowUp.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_ShowUp.Text)));

            //htTables.Add(cb_D_NbBags.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbBags.Text)));
            //htTables.Add(cb_D_NbVisitors.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbVisitors.Text)));
            //htTables.Add(cb_D_NbTrolley.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_NbTrolley.Text)));
            //htTables.Add(cb_D_AircraftLinks.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_AircraftLinks.Text)));
            if (rb_PF_ProcessSchedule.Checked)
            {
            }
            else
            {
                lsTables.Add(ConvertToName(cb_PP_Alloc_Process.Text));
                lsTables.Add(ConvertToName(cb_PP_Alloc_Itinerary.Text));
                lsTables.Add(ConvertToName(cb_PP_Alloc_CapaQueues.Text));
                //htTables.Add(cb_PP_Alloc_Process.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_PP_Alloc_Process.Text)));
                //htTables.Add(cb_PP_Alloc_Itinerary.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_PP_Alloc_Itinerary.Text)));
                //htTables.Add(cb_PP_Alloc_CapaQueues.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_PP_Alloc_CapaQueues.Text)));
            }
            lsTables.Add(ConvertToName(cb_PP_Oneof.Text));
            lsTables.Add(ConvertToName(cb_D_OCT_CI.Text));
            lsTables.Add(ConvertToName(cb_D_BoardingGate.Text));
            lsTables.Add(ConvertToName(cb_A_BaggageClaim.Text));

            //htTables.Add(cb_PP_Oneof.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_PP_Oneof.Text)));
            //htTables.Add(cb_D_OCT_CI.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_OCT_CI.Text)));
            //htTables.Add(cb_D_BoardingGate.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_D_BoardingGate.Text)));
            //htTables.Add(cb_A_BaggageClaim.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_A_BaggageClaim.Text)));


            lsTables.Add(ConvertToName(cb_PP_Alloc_Passport.Text));
            lsTables.Add(ConvertToName(cb_PP_Alloc_Security.Text));
            // << Task #7570 new Desk and extra information for Pax -Phase I B
            lsTables.Add(ConvertToName(cb_PP_Alloc_UserProcess.Text));
            // >> Task #7570 new Desk and extra information for Pax -Phase I B
            lsTables.Add(ConvertToName(cb_PP_Alloc_Transfer.Text));
            lsTables.Add(ConvertToName(cb_PF_Saturation.Text));
            //<< Task #7405 - new Desk and extra information for Pax            
            lsTables.Add(GlobalNames.sUserAttributesTableName);
            //>> Task #7405 - new Desk and extra information for Pax
            //htTables.Add(cb_PP_Alloc_Passport.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_PP_Alloc_Passport.Text)));
            //htTables.Add(cb_PP_Alloc_Security.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_PP_Alloc_Security.Text)));
            //htTables.Add(cb_PP_Alloc_Transfer.Tag.ToString(), Donnees.getTable("Input", ConvertToName(cb_PP_Alloc_Transfer.Text)));

            //Ici vérifier la validité de toutes les informations entre toutes les tables : FC inconnue ou Avion non valide.
            ErrorList = Donnees.CheckTable(lsTables, cb_PP_GeneratePaxPlan.Checked, cb_BHS_SimulateBHS.Checked);
            if (ErrorList != null)
            {
                ErrorList.Insert(0, "Error in the definition for the calc of Pax plan");
                SetErrors(ErrorList);
            }
            //Ici vérifier la validité de toutes les informations entre toutes les tables : FC inconnue ou Avion non valide.
            return (ErrorList == null);
        }


        private void cb_TransferDistribution_CheckedChanged(object sender, EventArgs e)
        {
            cb_TransTerm.Enabled = cb_PP_TransferDistribution.Checked;
            UpdateComboBox(cb_TransTerm, cb_TransTerm.Enabled);
            cb_PP_FlightCategoryDistr.Enabled = !cb_PP_TransferDistribution.Checked;
            cb_PP_TransferDeterministicDistribution.Enabled = !cb_PP_TransferDistribution.Checked && rb_PP_Arriving.Checked;
        }

        private void cb_PP_FlightCategoryDistr_CheckedChanged(object sender, EventArgs e)
        {
            cb_PP_TransferFCTable.Enabled = cb_PP_FlightCategoryDistr.Checked;
            UpdateComboBox(cb_PP_TransferFCTable, cb_PP_TransferFCTable.Enabled);
            cb_PP_TransferDistribution.Enabled = !cb_PP_FlightCategoryDistr.Checked;
            cb_PP_TransferDeterministicDistribution.Enabled = !cb_PP_FlightCategoryDistr.Checked && rb_PP_Arriving.Checked;
        }

        private void cb_PP_TransferDeterministicDistribution_CheckedChanged(object sender, EventArgs e)
        {
            cb_PP_TransferDistribution.Enabled = !cb_PP_TransferDeterministicDistribution.Checked;
            cb_PP_FlightCategoryDistr.Enabled = !cb_PP_TransferDeterministicDistribution.Checked;
        }

        private void rb_PF_Regular_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_PF_ProcessSchedule.Checked && !rb_PF_ProcessSchedule.Enabled)
            {
                rb_PF_Regular.Checked = true;
                return;
            }
            bool bRegular = rb_PF_Regular.Checked;
            cb_PP_Alloc_CapaQueues.Enabled = bRegular;
            cb_PP_Alloc_Process.Enabled = bRegular;
            cb_PP_GroupQueues.Enabled = bRegular;
            cb_PP_Alloc_Itinerary.Enabled = bRegular;
            // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            processCapaComboBox.Enabled = bRegular;
            processCapaLabel.Enabled = bRegular;
            // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            label10.Enabled = bRegular;
            label56.Enabled = bRegular;
            label51.Enabled = bRegular;
            label53.Enabled = bRegular;


            cb_PF_ProcessSchedule.Enabled = !bRegular;

            UpdateComboBox(cb_PF_ProcessSchedule, !bRegular);
            UpdateComboBox(cb_PP_Alloc_CapaQueues, bRegular);
            UpdateComboBox(cb_PP_Alloc_Process, bRegular);
            UpdateComboBox(cb_PP_GroupQueues, bRegular);
            UpdateComboBox(cb_PP_Alloc_Itinerary, bRegular);
            // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
            UpdateComboBox(processCapaComboBox, bRegular);
            // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
        }

        private void chk_Animated_CheckedChanged(object sender, EventArgs e)
        {
            // >> Bug #13273 Pax2Sim - Scenario Properties - Animated Queues
            // The TableLayoutPanel control has to be visible on the screen when you run your verification code. 
            //If it's behind an inactive TabPage, then it isn't on the screen and the control will report the Visible property as false,
            //regardless if the property is set to true in the designer.
            //if (!cb_PF_AnimatedFlow.Visible)  
            //  return;
            if (PAX2SIM.bAnimatedQueues)
            {
                cb_PF_AnimatedFlow.Enabled = chk_Animated.Checked;
                UpdateComboBox(cb_PF_AnimatedFlow, chk_Animated.Checked);
            }
        }

        #endregion

        #region Specificites AIA pour les User data
        private bool VerifInputForAIA()
        {
            ParamUserData pudTmp = getParamUserData();
            if (pudTmp == null)
                return false;
            if (pudTmp.getUserData("Input.txt") == null)
                return false;
            DataTable dtTmp = Donnees.getTable("Input", pudTmp.getUserData("Input.txt"));
            return Prompt.SIM_ParametersIhm.DataVerification(dtTmp);
        }
        #endregion


        #region Gestion de l'option Bagplan présente dans l'onglet BHS

        private bool verifCalcBagPlan() // VerifCalcBagPlan() function issue
        {
            bool bFilled = (cb_D_FPD.SelectedItem == null || cb_D_FPD.SelectedItem.ToString() == "");
            bFilled = bFilled || (cb_FPATable.SelectedItem == null || cb_FPATable.SelectedItem.ToString() == "");
            bFilled = bFilled || (cb_D_OCT_CI.SelectedItem == null || cb_D_OCT_CI.SelectedItem.ToString() == "");
            bFilled = bFilled || (cb_A_BaggageClaim.SelectedItem == null || cb_A_BaggageClaim.SelectedItem.ToString() == "");
            bFilled = bFilled || (cb_D_BoardingGate.SelectedItem == null || cb_D_BoardingGate.SelectedItem.ToString() == "");
            bFilled = bFilled || (cb_D_OCT_MakeUp.SelectedItem == null || cb_D_OCT_MakeUp.SelectedItem.ToString() == "");

            bFilled = bFilled || (cb_BHS_BagPlan.SelectedItem == null || cb_BHS_BagPlan.SelectedItem.ToString() == "");

            if (bFilled)
            {
                MessageBox.Show("There are some information missing : Please select a valid scenario for BagPlan definition.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tp_Dynamic.Select();
                return false;
            }
            return true;
        }

        private void cb_BHS_BagPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!rb_BHS_BagPlan.Checked)
                return;
            if ((sender == null) || (sender.GetType() != typeof(ComboBox)))
                return;
            ComboBox cbBox = (ComboBox)sender;
            if (cbBox.Text == "")
                return;
            ParamScenario psOldScenario = Donnees.GetScenario(cbBox.Text);
            if ((psOldScenario.FPD != "") && (cb_D_FPD.Items.IndexOf(psOldScenario.FPD) != -1))
            {
                cb_D_FPD.SelectedIndex = cb_D_FPD.Items.IndexOf(psOldScenario.FPD);
            }
            else
            {
                cb_D_FPD.Text = "";
            }
            if ((psOldScenario.FPA != "") && (cb_FPATable.Items.IndexOf(psOldScenario.FPA) != -1))
            {
                cb_FPATable.SelectedIndex = cb_FPATable.Items.IndexOf(psOldScenario.FPA);
            }
            else
            {
                cb_FPATable.Text = "";
            }

            if ((psOldScenario.OCT_BC != "") && (cb_A_BaggageClaim.Items.IndexOf(psOldScenario.OCT_BC) != -1))
            {
                cb_A_BaggageClaim.SelectedIndex = cb_A_BaggageClaim.Items.IndexOf(psOldScenario.OCT_BC);
            }
            else
            {
                cb_A_BaggageClaim.Text = "";
            }

            if ((psOldScenario.OCT_BG != "") && (cb_D_BoardingGate.Items.IndexOf(psOldScenario.OCT_BG) != -1))
            {
                cb_D_BoardingGate.SelectedIndex = cb_D_BoardingGate.Items.IndexOf(psOldScenario.OCT_BG);
            }
            else
            {
                cb_D_BoardingGate.Text = "";
            }
            if ((psOldScenario.OCT_CI_Table != "") && (cb_D_OCT_CI.Items.IndexOf(psOldScenario.OCT_CI_Table) != -1))
            {
                cb_D_OCT_CI.SelectedIndex = cb_D_OCT_CI.Items.IndexOf(psOldScenario.OCT_CI_Table);
            }
            else
            {
                cb_D_OCT_CI.Text = "";
            }
            if ((psOldScenario.OCT_MakeUp != "") && (cb_D_OCT_MakeUp.Items.IndexOf(psOldScenario.OCT_MakeUp) != -1))
            {
                cb_D_OCT_MakeUp.SelectedIndex = cb_D_OCT_MakeUp.Items.IndexOf(psOldScenario.OCT_MakeUp);
            }
            else
            {
                cb_D_OCT_MakeUp.Text = "";
            }
            if ((psOldScenario.Airline != "") && (cb_St_Airline.Items.IndexOf(ConvertToFullName(psOldScenario.Airline)) != -1))
            {
                cb_St_Airline.SelectedIndex = cb_St_Airline.Items.IndexOf(ConvertToFullName(psOldScenario.Airline));
            }
            else
            {
                cb_St_Airline.Text = ConvertToFullName(GlobalNames.FP_AirlineCodesTableName);
            }
            // >> Task #13361 FP AutoMod Data tables V3
            if ((psOldScenario.AircraftType != "") && (cb_D_AircraftType.Items.IndexOf(ConvertToFullName(psOldScenario.AircraftType)) != -1))
            {
                cb_D_AircraftType.SelectedIndex = cb_D_AircraftType.Items.IndexOf(ConvertToFullName(psOldScenario.AircraftType));
            }
            else
            {
                cb_D_AircraftType.Text = ConvertToFullName(GlobalNames.FP_AircraftTypesTableName);
            }
            if ((psOldScenario.OCT_BagDropTableName != "") && (cb_D_OCT_BagDrop.Items.IndexOf(psOldScenario.OCT_BagDropTableName) != -1))
            {
                cb_D_OCT_BagDrop.SelectedIndex = cb_D_OCT_BagDrop.Items.IndexOf(psOldScenario.OCT_BagDropTableName);
            }
            else
            {
                cb_D_OCT_BagDrop.Text = ConvertToFullName(GlobalNames.OCT_BaggDropTableName);
            }
            // << Task #13361 FP AutoMod Data tables V3
            try
            {
                dtp_BeginTime.Value = psOldScenario.DateDebut;
                dtp_EndTime.Value = psOldScenario.DateFin;
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02067: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            cb_SamplingStep.SelectedIndex = OverallTools.FormFunctions.FindIndexDansCombo(cb_SamplingStep, psOldScenario.SamplingStep);
            if (cb_SamplingStep.SelectedIndex == -1)
                cb_SamplingStep.SelectedIndex = 0;
            cb_AnalysisRange.SelectedIndex = OverallTools.FormFunctions.FindIndexDansCombo(cb_AnalysisRange, psOldScenario.AnalysisRange);
            if (cb_AnalysisRange.SelectedIndex == -1)
                cb_AnalysisRange.SelectedIndex = 0;
            cb_StatisticStep.SelectedIndex = OverallTools.FormFunctions.FindIndexDansCombo(cb_StatisticStep, psOldScenario.StatisticsStep);
            if (cb_StatisticStep.SelectedIndex == -1)
                cb_StatisticStep.SelectedIndex = 0;
            cb_StStepPolitic.SelectedIndex = cb_StStepPolitic.Items.IndexOf(psOldScenario.StatisticsStepMode);
        }
        #endregion


        #region Fonctions pour la gestion de l'onglets BHS
        private bool verifBHSData()
        {
            if (!VerifDates())
                return false;
            bool bFilled = ((cb_BHS_Arrival_MeanFlowTable.Text == "") && (rb_BHS_UseMeanFlow.Checked));
            bFilled = bFilled || ((cb_BHS_Transfer_MeanFlowTable.Text == "") && (rb_BHS_UseMeanFlow.Checked));
            bFilled = bFilled || ((cb_BHS_CheckIn_MeanFlowTable.Text == "") && (rb_BHS_UseMeanFlow.Checked));
            bFilled = bFilled || ((cb_BHS_Arrival_Containers.Text == "") && (rb_BHS_UsePaxPlan.Checked));
            bFilled = bFilled || (cb_BHS_ArrivalInfeedGroups.Text == "");
            bFilled = bFilled || (cb_BHS_CI_Collectors.Text == "");
            bFilled = bFilled || (cb_BHS_CI_Group.Text == "");
            bFilled = bFilled || (cb_BHS_CI_Routing.Text == "");
            bFilled = bFilled || ((cb_BHS_FlowSplit.Text == "") && (rb_BHS_UsePaxPlan.Checked));
            bFilled = bFilled || (cb_BHS_General.Text == "");
            bFilled = bFilled || (cb_BHS_Process.Text == "");
            bFilled = bFilled || (cb_BHS_TransferInfeedGroups.Text == "");
            bFilled = bFilled || (cb_BHS_TransferRouting.Text == "");
            bFilled = bFilled || (cb_BHS_HBS3_Routing.Text == "");

            if (bFilled)
            {
                MessageBox.Show("You must select a table for each field.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tp_BHS.Select();
                return false;
            }
            if (rb_BHS_UsePaxPlan.Checked)
            {
                if (!verifCalcPaxPlan())
                    return false;
            }
            if (rb_BHS_BagPlan.Checked)
            {
                if (!verifCalcBagPlan())
                    return false;
            }
            if (PAX2SIM.bAIA)
            {
                if (!VerifInputForAIA())
                {
                    MessageBox.Show("The user data for the simulation are not valid.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Functions pour the tab Simulation

        private void btn_SIM_OpenModel_Click(object sender, EventArgs e)
        {
            if (PAX2SIM.bSimul8)
                ofd_OpenFileDialog.Filter = "Custom model (*.exe;*.S8)|*.exe;*.S8";
            if (PAX2SIM.bExperior)
                ofd_OpenFileDialog.Filter = "Custom model (*.exe;*.Experior)|*.exe;*.Experior";
            if (lbl_SIM_ModelName.Text != "Default")
                ofd_OpenFileDialog.FileName = lbl_SIM_ModelName.Text;
            if (ofd_OpenFileDialog.ShowDialog() != DialogResult.OK)
            {
                lbl_SIM_ModelName.Text = "Default";
                return;
            }
            lbl_SIM_ModelName.Text = ofd_OpenFileDialog.FileName;
            tt_Tooltip.SetToolTip(lbl_SIM_ModelName, lbl_SIM_ModelName.Text);
            tc_General_SelectedIndexChanged(null, null);
        }

        private void cb_SIM_Regenerate_Data_CheckedChanged(object sender, EventArgs e)
        {
            if (dynamicSimulationUsingPaxPlan())
            {
                if (!cb_SIM_Regenerate_Data.Checked)
                    if (MessageBox.Show("Are you sure that you want to use the existing paxplan ? There might be \nincoherent data between the paxplan and the flight plan.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                        cb_SIM_Regenerate_Data.Checked = true;
            }
        }

        private void cb_CustomizedModel_CheckedChanged(object sender, EventArgs e)
        {
            bool bChanged = !(gb_SIM_Model.Enabled == cb_CustomizedModel.Checked);
            gb_SIM_Model.Enabled = cb_CustomizedModel.Checked;

            //cb_SIM_MakeUpSegregation.Enabled = !cb_CustomizedModel.Checked;
            //if (cb_CustomizedModel.Checked)
            //    cb_SIM_MakeUpSegregation.Checked = false;
            if (!cb_CustomizedModel.Checked)
                cb_SIM_DisplayModel.Checked = false;
            if (!cb_CustomizedModel.Checked)
                lbl_SIM_ModelName.Text = "Default";
            if (bChanged)
                tc_General_SelectedIndexChanged(null, null);
        }

        private void cb_CustomizedModel_EnabledChanged(object sender, EventArgs e)
        {/*
            if (!cb_CustomizedModel.Enabled)
                gb_SIM_Model.Enabled = false;
            else
                gb_SIM_Model.Enabled = cb_CustomizedModel.Checked;*/
        }

        private void cb_SIM_MakeUpSegregation_CheckedChanged(object sender, EventArgs e)
        {
            cb_ex_MakeUpSegregation.Enabled = cb_SIM_MakeUpSegregation.Checked;
            if (!cb_SIM_MakeUpSegregation.Checked)
                cb_ex_MakeUpSegregation.Checked = false;
            if (!cb_SIM_MakeUpSegregation.Checked)
                return;
            if (cb_SIM_MakeUpSegregation.Enabled)
            {
                String Tooltip = tt_Tooltip.GetToolTip(cb_SIM_MakeUpSegregation);
                if ((Tooltip != null) && (Tooltip.Length != 0))
                    MessageBox.Show(Tooltip, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Validation des informations contenues dans la fenêtre
        private bool DataIsValid()
        {

            if (cb_Scenario.Text == "")
            {
                MessageBox.Show("Please select a Scenario Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cb_Scenario.Focus();
                return false;
            }
            if (!VerifDates())
                return false;

            if (cb_BHS_SimulateBHS.Checked && !cb_BHS_SimulateBHS.Enabled) // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues
                return true;

            if (cb_D_CalcDeparturePeak.Checked
                && !verifCalcPeakDeparture())
            {
                //Vérification des informations données dans le premier onglet.
                return false;
            }

            if ((cb_CalcArrivalPeak.Checked) && (!verifCalcPeakArrival()))
            {
                //Vérification des informations données dans le premier onglet.
                return false;
            }

            if ((cb_BHS_SimulateBHS.Checked) && (!verifBHSData()))
            {
                return false;
            }
            if (cb_PP_GeneratePaxPlan.Checked)
            {
                //Vérification des informations données dans le premier onglet.
                if (!verifCalcPaxPlan())
                {
                    return false;
                }
            }
            if ((!cb_D_CalcDeparturePeak.Checked) &&
                (!cb_CalcArrivalPeak.Checked) &&
                (!cbGeneratePaxPlanForStatic.Checked) &&
                (!cb_PP_GeneratePaxPlan.Checked) &&
                ((!cb_BHS_SimulateBHS.Checked)))
            {
                MessageBox.Show("Please select at least one analysis", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            double percentile1 = -1;
            double percentile2 = -1;
            double percentile3 = -1;
            if (Double.TryParse(firstLevelTextBox.Text, out percentile1)
                && Double.TryParse(secondLevelTextBox.Text, out percentile2)
                && Double.TryParse(thirdLevelTextBox.Text, out percentile3))
            {
                if (percentile1 == percentile2 || percentile2 == percentile3 || percentile1 == percentile3)
                {
                    MessageBox.Show("The percentile values must differ from one another.", "Percentiles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            return true;
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (!DataIsValid())
            {
                DialogResult = DialogResult.None;
                return;
            }
            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update            
            DateTime now = DateTime.Now;
            if (scenarioCreationDate == DateTime.MinValue && scenarioLastUpdateDate == DateTime.MinValue)
            {
                scenarioCreationDate = now;
                scenarioLastUpdateDate = now;
            }
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            SourceAnalysis = getAnalysis();
        }
        #endregion

        #region Fonction pour l'activation et la désactivation des contrôles

        private void UpdateComboBox(ComboBox cbTmp, bool bEnabled)
        {
            if (cbTmp == null)
                return;
            String sOldValue = cbTmp.Text;
            cbTmp.Items.Clear();

            if (!bEnabled)
            {
                cbTmp.Text = "";
                return;
            }
            List<String> values = Donnees.getValidTables("Input", (String)cbTmp.Tag);
            foreach (String value in values)
            {
                OverallTools.FonctionUtiles.AddSortedItem(cbTmp, ConvertToFullName(value));
            }
            cbTmp.Text = sOldValue;
            if (((cbTmp.Text == null) || (cbTmp.Text.Length == 0)) && (cbTmp.Items.Count != 0))
            {
                int iDefault = cbTmp.Items.IndexOf(ConvertToFullName(cbTmp.Tag.ToString()));
                if (iDefault == -1)
                    iDefault = 0;
                cbTmp.SelectedIndex = iDefault;
            }
        }

        #endregion


        #region Gestion des chargement des scénarios
        private void setScenario(ParamScenario parametres)
        {

            if (parametres == null)
                return;
            /*if (SourceAnalysis.Name == parametres.Name)
                return;*/
            //On sauvegarde l'ancien système de paramétrage afin de pouvoir 
            //faire un mix dans le cas où l'utilisateur n'entre que des informations
            //sur un type d'analyses. : Statique ou dynamique ou BHS.
            SourceAnalysis = parametres;
            if (cb_Scenario.Text != parametres.Name)
            {
                cb_Scenario.Text = parametres.Name;
            }
            temporaryLinkedResultsFilters.Clear();
            temporaryLinkedResultsFilters.AddRange(SourceAnalysis.analysisResultsFilters);  // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes

            setScenarioInformation(parametres); // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update

            flowTypes.Clear();
            flowTypes.AddRange(SourceAnalysis.flowTypes);

            if (SourceAnalysis.analysisResultsFilters != null && SourceAnalysis.analysisResultsFilters.Count > 0)
            {
                if (SourceAnalysis.flowTypes == null || SourceAnalysis.flowTypes.Count == 0)
                {
                    flowTypes.Clear();
                    flowTypes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
                    //flowTypes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
                    flowTypes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
                    //flowTypes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
                    flowTypes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
                }
            }

            setDeparturePeak(parametres);
        }

        // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
        //When changing the scenarios in the ScenarioProperties assistant: we load a scenario to modify it => set the last update date to now
        private void setScenarioInformation(ParamScenario param)
        {
            if (param != null)
            {
                if (authorNameTextBox != null)
                    authorNameTextBox.Text = param.authorName;
                if (scenarioDescriptionTextBox != null)
                    scenarioDescriptionTextBox.Text = param.scenarioDescription;
                scenarioCreationDate = param.creationDate;
                scenarioLastUpdateDate = param.lastUpdateDate;
            }
        }
        // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

        private void setDeparturePeak(ParamScenario param)
        {
            /*We remove here the update fonction for the checkBoxes, to be sure that the updates will not be done before it actually been changed. Then
             We gonna call the function to update the tabs (will make sure that the time to update the informations will not be too long.*/
            cb_D_CalcDeparturePeak.CheckedChanged -= new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);
            cb_CalcArrivalPeak.CheckedChanged -= new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);
            cb_BHS_SimulateBHS.CheckedChanged -= new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);
            cb_PP_GeneratePaxPlan.CheckedChanged -= new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);
            cb_Gen_Tms.CheckedChanged -= new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);

            cb_D_CalcDeparturePeak.Checked = false;
            cb_CalcArrivalPeak.Checked = false;
            cb_BHS_SimulateBHS.Checked = false;
            cb_PP_GeneratePaxPlan.Checked = false;
            cb_Gen_Tms.Checked = false;
            if (param == null)
            {
                return;
            }

            if (param.DynamicReclaimAllocation)
            {
                cb_SIM_DynReclaimTerminal.Items.Add(param.TerminalReclaimAllocation.ToString());
                cb_SIM_DynReclaimTerminal.Text = param.TerminalReclaimAllocation.ToString();
            }
            else
            {
                cb_SIM_DynReclaimTerminal.Text = "No";
            }



            cb_Gen_Tms.Checked = param.TMSSimulation;
            cb_BHS_SimulateBHS.Checked = param.BHSSimulation;
            if (!cb_BHS_SimulateBHS.Checked)
                cb_PP_GeneratePaxPlan.Checked = param.PaxPlan;
            cb_D_CalcDeparturePeak.Checked = param.DeparturePeak;
            cb_CalcArrivalPeak.Checked = param.ArrivalPeak;
            cbGeneratePaxPlanForStatic.Checked = param.generatePaxPlanForStaticAnalysis;

            cb_D_CalcDeparturePeak.CheckedChanged += new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);
            cb_CalcArrivalPeak.CheckedChanged += new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);
            cb_BHS_SimulateBHS.CheckedChanged += new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);
            cb_PP_GeneratePaxPlan.CheckedChanged += new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);
            cb_Gen_Tms.CheckedChanged += new System.EventHandler(this.cb_D_CalcDeparturePeak_CheckedChanged);

            if (param.BHSSimulation)
            {
                // >> Bug #13579 BHS Analysis Assistant
                rb_BHS_UseMeanFlow.CheckedChanged -= new System.EventHandler(this.rb_BHS_UsePaxPlan_CheckedChanged);
                cb_BHS_CheckIn_MeanFlowTable.SelectedIndexChanged -= new System.EventHandler(this.cb_BHS_CheckIn_MeanFlowTable_SelectedIndexChanged);
                cb_BHS_Arrival_MeanFlowTable.SelectedIndexChanged -= new System.EventHandler(this.cb_BHS_CheckIn_MeanFlowTable_SelectedIndexChanged);
                cb_BHS_Transfer_MeanFlowTable.SelectedIndexChanged -= new System.EventHandler(this.cb_BHS_CheckIn_MeanFlowTable_SelectedIndexChanged);

                if (param.ArrivalMeanFlows != "" && param.CheckInMeanFlows != "" && param.TransferMeanFlows != "")
                {
                    DateTime minDate = DateTime.MaxValue;
                    DateTime maxDate = DateTime.MinValue;

                    List<string> meanFlowTableNames
                        = new List<string>(new string[] { param.CheckInMeanFlows, param.TransferMeanFlows, param.ArrivalMeanFlows });
                    getMinMaxDatesFromMeanFlowTables(meanFlowTableNames, out minDate, out maxDate);

                    if (minDate != DateTime.MaxValue && maxDate != DateTime.MinValue)
                    {
                        bool changed = false;
                        if (param.DateDebut > minDate)
                        {
                            // >> Task #16728 PAX2SIM Improvements (Recurring) C#13                            
                            dtp_BeginTime.MinDate = DateTimePicker.MinimumDateTime;
                            dtp_BeginTime.MaxDate = DateTimePicker.MaximumDateTime;
                            // << Task #16728 PAX2SIM Improvements (Recurring) C#13

                            dtp_BeginTime.MinDate = minDate.AddDays(-1);    // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason
                            dtp_BeginTime.Value = minDate;
                            changed = true;
                        }
                        if (param.DateFin < maxDate)
                        {
                            // >> Task #16728 PAX2SIM Improvements (Recurring) C#13                            
                            dtp_EndTime.MinDate = DateTimePicker.MinimumDateTime;
                            dtp_EndTime.MaxDate = DateTimePicker.MaximumDateTime;
                            // << Task #16728 PAX2SIM Improvements (Recurring) C#13

                            changed = true;
                            dtp_EndTime.MaxDate = maxDate.AddDays(1);   // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason
                            dtp_EndTime.Value = maxDate;
                        }
                        if (changed)
                        {
                            MessageBox.Show("The time range was automatically updated because the Flight Plan data did not comply with the saved time range.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                rb_BHS_UseMeanFlow.CheckedChanged += new System.EventHandler(this.rb_BHS_UsePaxPlan_CheckedChanged);
                cb_BHS_CheckIn_MeanFlowTable.SelectedIndexChanged += new System.EventHandler(this.cb_BHS_CheckIn_MeanFlowTable_SelectedIndexChanged);
                cb_BHS_Arrival_MeanFlowTable.SelectedIndexChanged += new System.EventHandler(this.cb_BHS_CheckIn_MeanFlowTable_SelectedIndexChanged);
                cb_BHS_Transfer_MeanFlowTable.SelectedIndexChanged += new System.EventHandler(this.cb_BHS_CheckIn_MeanFlowTable_SelectedIndexChanged);
                // << Bug #13579 BHS Analysis Assistant
                generateLocalISTCheckBox.Checked = param.bhsGenerateLocalIST;
                generateGroupISTcheckBox.Checked = param.bhsGenerateGroupIST;   // >> Task #14280 Bag Trace Loading time too long
                generateMUPSegregationCheckBox.Checked = param.bhsGenerateMUPSegregation;   // >> Task #14280 Bag Trace Loading time too long                
            }

            if (param.lastModificationP2SVersion < ParamScenario.P2S_VERSION_EXPORT_AUTOMOD_MAIN_DATA)
                exportAutomodMainDataCheckBox.Checked = true;
            else
                exportAutomodMainDataCheckBox.Checked = param.exportAutomodMainData;
            clearAutomodMainDataCheckBox.Checked = param.clearAutomodMainData;

            Dictionary<String, List<String>> userDataDictionary = Donnees.getUserData();
            if (userDataDictionary == null || userDataDictionary.Count == 0)
            {
                cb_SIM_ExportUserData.Checked = false;
                clearAutomodUserDataCheckBox.Checked = true;
            }
            else
            {
                cb_SIM_ExportUserData.Checked = param.ExportUserData;
                clearAutomodUserDataCheckBox.Checked = param.clearAutomodUserData;
            }
            cb_D_CalcDeparturePeak_CheckedChanged(null, null);
            if (cb_BHS_SimulateBHS.Checked)
            {
                /*if (bBHSPaxPlan || bBHSMeanFlow)
                {
                    rb_BHS_UseMeanFlow.Enabled = false;
                    rb_BHS_UsePaxPlan.Enabled = false;
                }*/
                rb_BHS_BagPlan.Checked = param.BagPlan;
                rb_BHS_UsePaxPlan.Checked = param.UsePaxPlan;
                rb_BHS_UseMeanFlow.Checked = !(rb_BHS_BagPlan.Checked || rb_BHS_UsePaxPlan.Checked);

                // >> Issue - Fixed Some BHS table references are wrongly saved/retrieved
                //cb_BHS_Terminal.Text = ConvertToFullName(param.Terminal);
                //cb_BHS_General.Text = ConvertToFullName(param.General);
                //cb_BHS_ArrivalInfeedGroups.Text = ConvertToFullName(param.ArrivalInfeedGroups);
                //cb_BHS_CI_Group.Text = ConvertToFullName(param.CIGroups);
                //cb_BHS_TransferInfeedGroups.Text = ConvertToFullName(param.TransferInfeedGroups);
                //cb_BHS_CI_Collectors.Text = param.CICollectors;//ConvertToFullName(param.CICollectors);   // >> Bug #13579 BHS Analysis Assistant
                //cb_BHS_HBS3_Routing.Text = ConvertToFullName(param.HBS3Routing);
                //cb_BHS_CI_Routing.Text = ConvertToFullName(param.CIRouting);
                //cb_BHS_TransferRouting.Text = ConvertToFullName(param.TransferRouting);
                //cb_BHS_FlowSplit.Text = ConvertToFullName(param.FlowSplit);
                //cb_BHS_Process.Text = ConvertToFullName(param.Process);
                //cb_BHS_Arrival_Containers.Text = ConvertToFullName(param.ArrivalContainers);
                //cb_BHS_Arrival_MeanFlowTable.Text = ConvertToFullName(param.ArrivalMeanFlows);
                //cb_BHS_CheckIn_MeanFlowTable.Text = param.CheckInMeanFlows;//ConvertToFullName(param.CheckInMeanFlows); // >> Bug #13579 BHS Analysis Assistant
                //cb_BHS_Transfer_MeanFlowTable.Text = ConvertToFullName(param.TransferMeanFlows);

                cb_BHS_Terminal.SelectedIndex = cb_BHS_Terminal.Items.IndexOf(param.Terminal);
                cb_BHS_General.SelectedIndex = cb_BHS_General.Items.IndexOf(param.General);
                cb_BHS_ArrivalInfeedGroups.SelectedIndex = cb_BHS_ArrivalInfeedGroups.Items.IndexOf(param.ArrivalInfeedGroups);
                cb_BHS_CI_Group.SelectedIndex = cb_BHS_CI_Group.Items.IndexOf(param.CIGroups);
                cb_BHS_TransferInfeedGroups.SelectedIndex = cb_BHS_TransferInfeedGroups.Items.IndexOf(param.TransferInfeedGroups);
                cb_BHS_CI_Collectors.SelectedIndex = cb_BHS_CI_Collectors.Items.IndexOf(param.CICollectors);
                cb_BHS_HBS3_Routing.SelectedIndex = cb_BHS_HBS3_Routing.Items.IndexOf(param.HBS3Routing);
                cb_BHS_CI_Routing.SelectedIndex = cb_BHS_CI_Routing.Items.IndexOf(param.CIRouting);
                cb_BHS_TransferRouting.SelectedIndex = cb_BHS_TransferRouting.Items.IndexOf(param.TransferRouting);
                cb_BHS_FlowSplit.SelectedIndex = cb_BHS_FlowSplit.Items.IndexOf(param.FlowSplit);
                cb_BHS_Process.SelectedIndex = cb_BHS_Process.Items.IndexOf(param.Process);
                cb_BHS_Arrival_Containers.SelectedIndex = cb_BHS_Arrival_Containers.Items.IndexOf(param.ArrivalContainers);
                cb_BHS_Arrival_MeanFlowTable.SelectedIndex = cb_BHS_Arrival_MeanFlowTable.Items.IndexOf(param.ArrivalMeanFlows);
                cb_BHS_CheckIn_MeanFlowTable.SelectedIndex = cb_BHS_CheckIn_MeanFlowTable.Items.IndexOf(param.CheckInMeanFlows);
                cb_BHS_Transfer_MeanFlowTable.SelectedIndex = cb_BHS_Transfer_MeanFlowTable.Items.IndexOf(param.TransferMeanFlows);
                // >> Issue - Fixed Some BHS table references are wrongly saved/retrieved

                if (param.BagPlan)
                {
                    if (cb_BHS_BagPlan.Items.Count != 0)
                    {
                        int iIndex = cb_BHS_BagPlan.Items.IndexOf(param.BagPlanScenarioName);
                        if (iIndex != -1)
                        {
                            if (cb_BHS_BagPlan.SelectedIndex == iIndex)
                                cb_BHS_BagPlan.SelectedIndex = -1;
                            cb_BHS_BagPlan.SelectedIndex = iIndex;
                        }
                        else
                        {
                            cb_BHS_BagPlan.Text = "";
                        }

                        iIndex = cb_D_OCT_MakeUp.Items.IndexOf(ConvertToFullName(param.OCT_MakeUp));
                        if (iIndex != -1)
                        {
                            if (cb_D_OCT_MakeUp.SelectedIndex == iIndex)
                                cb_D_OCT_MakeUp.SelectedIndex = -1;
                            cb_D_OCT_MakeUp.SelectedIndex = iIndex;
                        }
                        cb_ex_OCT_MakeUp.Checked = param.UseException(param.OCT_MakeUp);
                    }
                }
                if (tc_General.TabPages.Contains(tp_resultsFilters))
                {
                    refreshAnalysisResultsFiltersTab(param.analysisResultsFilters);  // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
                    updateSelectedFlowsLabel(flowTypes);
                }
            }
            if ((cb_D_CalcDeparturePeak.Checked) || (cbGeneratePaxPlanForStatic.Checked) || (cb_D_CalcDeparturePeak.Checked) || (cb_PP_GeneratePaxPlan.Checked))
            {
                cb_D_FPD.Text = ConvertToFullName(param.FPD);
                cb_D_LFD.Text = ConvertToFullName(param.DepartureLoadFactors);
                cb_ex_DLoadFactors.Checked = param.UseException(param.DepartureLoadFactors);
                cb_D_ShowUp.Text = ConvertToFullName(param.CI_ShowUpTable);
                cb_ex_CI_ShowUp.Checked = param.UseException(param.CI_ShowUpTable);
                // << Task #9117 Pax2Sim - Static Analysis - FPD_EBS calculation - transfering pax missing                
                cb_A_InterConnectingTimes.Text = ConvertToFullName(param.ICT_Table);
                cb_ex_Transfer_ICT.Checked = param.UseException(param.ICT_Table);
                // >> Task #9117 Pax2Sim - Static Analysis - FPD_EBS calculation - transfering pax missing
                cb_D_OCT_CI.Text = ConvertToFullName(param.OCT_CI_Table);
                cb_ex_OCT_CI.Checked = param.UseException(param.OCT_CI_Table);
                cb_D_OCT_MakeUp.Text = ConvertToFullName(param.OCT_MakeUp);
                cb_ex_OCT_MakeUp.Checked = param.UseException(param.OCT_MakeUp);
                cb_D_BoardingGate.Text = ConvertToFullName(param.OCT_BG);
                cb_ex_OCT_BoardGate.Checked = param.UseException(param.OCT_BG);
                // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
                cb_D_OCT_BagDrop.Text = ConvertToFullName(param.OCT_BagDropTableName);
                cb_ex_OCT_BagDrop.Checked = param.UseException(param.OCT_BagDropTableName);
                // >> Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
                // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
                cb_d_EBSInputRate.Text = ConvertToFullName(param.ebsInputRateTableName);
                cb_ex_EBSInputRate.Checked = param.UseException(param.ebsInputRateTableName);

                cb_d_EBSOutputRate.Text = ConvertToFullName(param.ebsOutputRateTableName);
                cb_ex_EBSOutputRate.Checked = param.UseException(param.ebsOutputRateTableName);
                // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

            }
            if ((cb_D_CalcDeparturePeak.Checked) || (cb_CalcArrivalPeak.Checked) || (cb_PP_GeneratePaxPlan.Checked) || (cbGeneratePaxPlanForStatic.Checked))
            {
                cb_FPATable.Text = ConvertToFullName(param.FPA);
                cb_A_LoadFactors.Text = ConvertToFullName(param.ArrivalLoadFactors);
                //To-check
                cb_ex_ALoadFactors.Checked = param.UseException(param.ArrivalLoadFactors);
                cb_A_InterConnectingTimes.Text = ConvertToFullName(param.ICT_Table);
                cb_ex_Transfer_ICT.Checked = param.UseException(param.ICT_Table);
                cb_A_BaggageClaim.Text = ConvertToFullName(param.OCT_BC);
                cb_ex_OCT_BaggageClaim.Checked = param.UseException(param.OCT_BC);
                // << Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
                cb_A_BaggageLoadingRate.Text = ConvertToFullName(param.arrivalBaggageLoadingRateTableName);
                cb_ex_BaggageLoadingRate.Checked = param.UseException(param.arrivalBaggageLoadingRateTableName);
                cb_A_BaggageLoadingDelay.Text = ConvertToFullName(param.arrivalBaggageLoadingDelayTableName);
                cb_ex_BaggageLoadingDelay.Checked = param.UseException(param.arrivalBaggageLoadingDelayTableName);
                // >> Task #9121 Pax2Sim - Scenario Properties params - Loading Rate/Delay
            }
            if ((cb_CalcArrivalPeak.Checked) || (cb_PP_GeneratePaxPlan.Checked) || (cb_D_CalcDeparturePeak.Checked))
            {
                cb_D_FC.Text = ConvertToFullName(param.FlightCategories);
                flightSubcategoriesComboBox.Text = ConvertToFullName(param.FlightSubcategories);    // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
                cb_D_AircraftType.Text = ConvertToFullName(param.AircraftType);
                cb_ex_Aircraft.Checked = param.UseException(param.AircraftType);
                cb_St_Airline.Text = ConvertToFullName(param.Airline);
                cb_D_NbBags.Text = ConvertToFullName(param.NbBags);
                cb_ex_NbBags.Checked = param.UseException(param.NbBags);
                cb_D_NbVisitors.Text = ConvertToFullName(param.NbVisitors);
                cb_ex_NbVisitors.Checked = param.UseException(param.NbVisitors);
                cb_D_NbTrolley.Text = ConvertToFullName(param.NbTrolley);
                cb_ex_NbTrolley.Checked = param.UseException(param.NbTrolley);
                if ((cb_CalcArrivalPeak.Checked && cb_D_CalcDeparturePeak.Checked) || (cb_PP_GeneratePaxPlan.Checked))
                    cb_D_AircraftLinks.Text = ConvertToFullName(param.AircraftLinksTable);
#if(PAXINOUTUTILISATION)
                if (param.PaxIn != null)
                {
                    cb_ex_PaxIn.Checked = param.UseException(param.PaxIn);
                    cb_PP_PaxIn.Text = ConvertToFullName(param.PaxIn);
                }
                if (param.PaxOut != null)
                {
                    cb_ex_PaxOut.Checked = param.UseException(param.PaxOut);
                    cb_PP_PaxOut.Text = ConvertToFullName(param.PaxOut);
                }
                gb_PaxInOut.Visible = true;
#endif

                #region 26/03/2012 - SGE - Parking Mulhouse
                if (param.InitialCarStock != "")
                {
                    chk_PP_Parking.Checked = true;
                    cb_PP_InitialOccupation.Text = ConvertToFullName(param.InitialCarStock);
                    if (param.UseExistingPRKPlan)
                        rb_PP_ExistingPrkPlan.Checked = param.UseExistingPRKPlan;
                    else
                        rb_PP_GeneratePRKPLAN.Checked = true;

                    cb_PP_DwellTime.Text = ConvertToFullName(param.LongStayTable);
                    cb_PP_DwellTimeVis.Text = ConvertToFullName(param.ShortStayTable);
                    cb_PP_Ex_DwellTime.Checked = param.UseException(param.LongStayTable);
                    cb_PP_Ex_DwellTimeVis.Checked = param.UseException(param.ShortStayTable);
                }
                else
                {
                    chk_PP_Parking.Checked = false;
                }
                #endregion //26/03/2012 - SGE - Parking Mulhouse

                // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                #region Defined Number of Passengers / Baggages
                useNbOfPaxCheckBox.Checked = param.UseDefinedNbPax;
                nbOfPaxComboBox.Text = ConvertToFullName(param.NumberOfPassengers);

                useNbOfBagsCheckBox.Checked = param.UseDefinedNbBags;
                nbOfBagsComboBox.Text = ConvertToFullName(param.NumberOfBaggages);
                #endregion
                // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                if (param.FPParametersTableName != "") // >> Task #17690 PAX2SIM - Flight Plan Parameters table
                {
                    useFPParametersTableCheckBox.Checked = true;
                    fpParametersTableComboBox.Text = ConvertToFullName(param.FPParametersTableName);
                }

                reclaimSyncComboBox.Text = ConvertToFullName(param.reclaimBagsLimitDistributionTableName);
                usaStandardParamsComboBox.Text = ConvertToFullName(param.UsaStandardParamTableName);    // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            }
            //cb_PP_TransferDeterministicDistribution.CheckedChanged -= new EventHandler(cb_PP_TransferDeterministicDistribution_CheckedChanged);
            cb_PP_TransferDeterministicDistribution.Checked = param.IsTransferDistributionDeterministic;
            //cb_PP_TransferDeterministicDistribution.CheckedChanged += new EventHandler(cb_PP_TransferDeterministicDistribution_CheckedChanged);
            if ((cb_PP_GeneratePaxPlan.Checked) || (cbGeneratePaxPlanForStatic.Checked))
            {
                if ((param.TransfTerminalDistribution != "") && (param.TransfTerminalDistribution != null))
                {
                    cb_PP_TransferDistribution.Checked = true;
                    cb_TransTerm.Text = ConvertToFullName(param.TransfTerminalDistribution);
                }
                else
                {
                    cb_PP_TransferDistribution.Checked = false;
                }

                if ((param.TransfFligthCategoryDistribution != "") && (param.TransfFligthCategoryDistribution != null))
                {
                    cb_PP_FlightCategoryDistr.Checked = true;
                    cb_PP_TransferFCTable.Text = ConvertToFullName(param.TransfFligthCategoryDistribution);
                }
                else
                {
                    cb_PP_FlightCategoryDistr.Checked = false;
                }


                cb_PP_UseDefinedSeed.Checked = param.UseSeed;
                if (!param.UseSeed)
                {
                    mb_PP_UseDefinedSeed.Text = "1";
                    //mffFileSeedMaskedTextBox.Text = "0";    // >> Bug #14900 MFF file not created
                }
                else
                {
                    mb_PP_UseDefinedSeed.Text = param.Seed.ToString();
                    ///mffFileSeedMaskedTextBox.Text = param.MFFFileSeed.ToString();
                }
                rb_PP_IgnoreAll.Checked = !param.GenerateAll;
                rb_PP_GenerateAll.Checked = param.GenerateAll;

                // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
                //rb_Filling.Checked = param.FillQueue;
                //rb_Normal.Checked = !param.FillQueue;

                if (param.StationGlobalFillingType.Equals(GlobalNames.FIRST_STATION_FILLING_TYPE))
                {
                    rb_Normal.Checked = true;
                    rb_Filling.Checked = false;
                    rb_RandomFilling.Checked = false;
                }
                else if (param.StationGlobalFillingType.Equals(GlobalNames.SATURATE_STATION_FILLING_TYPE))
                {
                    rb_Normal.Checked = false;
                    rb_Filling.Checked = true;
                    rb_RandomFilling.Checked = false;
                }
                else if (param.StationGlobalFillingType.Equals(GlobalNames.RANDOM_STATION_FILLING_TYPE))
                {
                    rb_Normal.Checked = false;
                    rb_Filling.Checked = false;
                    rb_RandomFilling.Checked = true;
                }
                // >> Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation

                rb_PP_Arriving.Checked = param.TransferArrivalGeneration;
                rb_PP_Departing.Checked = !param.TransferArrivalGeneration;
                rb_PP_GenerateFlightsAtEnd.Checked = param.GenerateFlightsAtEnd;
                rb_PP_IgnoreFlights.Checked = !param.GenerateFlightsAtEnd;

                if (!param.TransferArrivalGeneration)
                {
                    missedDepartureTransferBasedOnICTRadioButton.Checked = !param.MissedDepartureTransferBasedOnCheckInShowUp;
                    missedDepartureTransferBasedOnCIRadioButton.Checked = param.MissedDepartureTransferBasedOnCheckInShowUp;
                }
                if (param.FillTransfer)
                {
                    fillBasedOnCheckInRadioButton.Checked = param.FillDepartureTransferBasedOnCheckInShowUp;
                    fillBasedOnICTRadioButton.Checked = !param.FillDepartureTransferBasedOnCheckInShowUp;
                }
                cb_PP_FillTransfer.Checked = param.FillTransfer;

                if ((param.Opening_CITable != "") && (param.Opening_CITable != null))
                {
                    cb_PP_Opening.Checked = true;
                    cb_PP_Opening_CI.Text = ConvertToFullName(param.Opening_CITable);
                }
                else
                {
                    cb_PP_Opening.Checked = false;
                }
                cb_D_OCT_CI.Text = ConvertToFullName(param.OCT_CI_Table);
                //cb_PP_Alloc_OCT_BC.Text = ConvertToFullName(param.OCT_BC);
                //cb_PP_Alloc_OCT_BG.Text = ConvertToFullName(param.OCT_BG);
                if ((param.UseProcessSchedule) && (rb_PF_ProcessSchedule.Enabled))
                {
                    rb_PF_ProcessSchedule.Checked = true;
                    cb_PF_ProcessSchedule.Text = ConvertToFullName(param.ProcessSchedule);
                    cb_PP_Alloc_Process.Text = "";
                    cb_PP_Alloc_Itinerary.Text = "";
                    cb_PP_Alloc_CapaQueues.Text = "";
                    cb_PP_GroupQueues.Text = "";
                    // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
                    processCapaComboBox.Text = "";
                    // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
                }
                else
                {
                    rb_PF_Regular.Checked = true;
                    cb_PF_ProcessSchedule.Text = "";
                    cb_PP_Alloc_Process.Text = ConvertToFullName(param.ProcessTimes);
                    cb_ex_TimesProcess.Checked = param.UseException(param.ProcessTimes);
                    cb_PP_Alloc_Itinerary.Text = ConvertToFullName(param.Itinerary);
                    cb_PP_Alloc_CapaQueues.Text = ConvertToFullName(param.CapaQueues);
                    cb_PP_GroupQueues.Text = ConvertToFullName(param.GroupQueues);
                    // << Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
                    processCapaComboBox.Text = ConvertToFullName(param.ProcessCapacities);
                    // >> Task #8757 Pax2Sim - Scenario Properties - Capa Process table selection
                }
                if (chk_Animated.Enabled && (param.AnimatedQueues != "") && (param.AnimatedQueues != null))
                {
                    chk_Animated.Checked = true;
                    cb_PF_AnimatedFlow.Text = ConvertToFullName(param.AnimatedQueues);
                }
                else
                {
                    chk_Animated.Checked = false;
                }
                cb_PP_Alloc_Security.Text = ConvertToFullName(param.Security);
                cb_PP_Alloc_Transfer.Text = ConvertToFullName(param.Transfer);
                cb_PP_Alloc_Passport.Text = ConvertToFullName(param.Passport);
                // << Task #7570 new Desk and extra information for Pax -Phase I B
                cb_PP_Alloc_UserProcess.Text = ConvertToFullName(param.UserProcess);
                // >> Task #7570 new Desk and extra information for Pax -Phase I B
                cb_PF_Saturation.Text = ConvertToFullName(param.Saturation);
                cb_PP_Oneof.Text = ConvertToFullName(param.Oneof);

                // >> Task T-49 ValueCoders - Fetching the value of "Flight Plans Parameters table" checkbox and dropdown when we only select Generate Pax Plan Checkbox
                if (param.FPParametersTableName != "") 
                {
                    useFPParametersTableCheckBox.Checked = true;
                    fpParametersTableComboBox.Text = ConvertToFullName(param.FPParametersTableName);
                }
            }

            useSimulationEngineSeedCheckBox.Checked = param.useUserProvidedSimulationEngineSeed;
            if (!param.useUserProvidedSimulationEngineSeed)
                simulationEngineSeedMaskedTextBox1.Text = "1";
            else
                simulationEngineSeedMaskedTextBox1.Text = param.simulationEngineSeed.ToString();

            //On met le modèle Automod par défaut.
            lbl_SIM_ModelName.Text = "Default";
            cb_CustomizedModel.Checked = false;
            cb_SIM_DisplayModel.Checked = false;

            if ((param.BHSSimulation || param.TMSSimulation) && (!bBHSProcessEnabled))
            {
                //Si le modèle Process BHS n'est pas disponible, alors la sélection du
                //modèle est forcée.
                cb_CustomizedModel.Checked = true;
                cb_CustomizedModel.Enabled = false;
                //cb_SIM_MakeUpSegregation.Visible = false;
                cb_ex_MakeUpSegregation.Visible = cb_SIM_MakeUpSegregation.Visible;
                //cb_SIM_MakeUpSegregation.Checked = false;
                if (param.UseMakeUpSegregation)
                    cb_SIM_MakeUpSegregation.Checked = true;
                cb_ex_MakeUpSegregation.Checked = param.UseExSegregation && param.UseMakeUpSegregation;
                cb_ex_MakeUpSegregation.Enabled = param.UseMakeUpSegregation;
            }
            else if ((param.BHSSimulation) && (bBHSProcessEnabled))
            {
                cb_SIM_MakeUpSegregation.Visible = true;
                cb_ex_MakeUpSegregation.Visible = true;
                if (param.UseMakeUpSegregation)
                    cb_SIM_MakeUpSegregation.Checked = true;
                cb_ex_MakeUpSegregation.Checked = param.UseExSegregation && param.UseMakeUpSegregation;
                cb_ex_MakeUpSegregation.Enabled = param.UseMakeUpSegregation;
            }
            else
            {
                if (param.UseMakeUpSegregation)
                    cb_SIM_MakeUpSegregation.Checked = true;
                //cb_SIM_MakeUpSegregation.Visible = false;
                cb_ex_MakeUpSegregation.Visible = cb_SIM_MakeUpSegregation.Visible;
            }
            if ((param.BHSSimulation || param.TMSSimulation || PAX2SIM.bAnimatedQueues) &&
                (param.ModelName != "") &&
                (param.ModelName != null) &&
                (param.ModelName != "Default"))
            {
                //Le modèle est renseigné, on doit donc mettre le modèle sélectionné.                
                lbl_SIM_ModelName.Text = param.ModelName;
                cb_CustomizedModel.Checked = true;
                btn_SIM_Start.Enabled = IsAbleToSimulate(); // PAX key flag disabled
                cb_SIM_DisplayModel.Checked = param.DisplayModel;
            }
            else if ((!PAX2SIM.bAnimatedQueues) && (!(param.BHSSimulation || param.TMSSimulation)))
            {
                //Les queues ne sont pas animées (option indisponible) donc dans tous les cas
                cb_CustomizedModel.Enabled = false;
                cb_CustomizedModel.Checked = false;
            }
            cb_SaveTraceMode.CheckedChanged -= new EventHandler(cb_SaveTraceMode_CheckedChanged);
            cb_SaveTraceMode.Checked = !param.SaveTraceMode;
            cb_SaveTraceMode.CheckedChanged += new EventHandler(cb_SaveTraceMode_CheckedChanged);
            txt_SIM_WarmUp.Text = param.WarmUp.ToString();
            cb_SIM_Regenerate_Data.Checked = param.RegeneratePaxplan;

            setParamUserData(param.UserData);

            // << Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab
            setParamUserAttributes(param.userAttributesTablesDictionary);
            // >> Task #9163 Pax2Sim - Scenario Properties - User Attributes tables Tab

            if (!param.BagPlan)
            {
                if (!param.isTraceAnalysisScenario)  // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17
                {
                    //En mode BagPlan, les dates sont attribuées automatiquement à partir de l'autre scénario.
                    //Aucun besoin donc de les modifiées.
                    ChangeDate(param.FPA, param.FPD, dtp_BeginTime, dtp_EndTime);
                    if (((param.DateDebut > dtp_EndTime.MaxDate) || (param.DateDebut < dtp_EndTime.MinDate)) ||
                        ((param.DateDebut > dtp_EndTime.MaxDate) || (param.DateDebut < dtp_EndTime.MinDate)))
                    {
                        MessageBox.Show("The time range was automatically updated because the Flight Plan data did not comply with the saved time range.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //dtp_BeginTime.Value = param.DateDebut;
                        //dtp_EndTime.Value = param.DateFin;
                    }
                    else
                    {
                        dtp_BeginTime.Value = param.DateDebut;
                        dtp_EndTime.Value = param.DateFin;
                    }
                }
                else
                {
                    dtp_BeginTime.MinDate = DateTimePicker.MinimumDateTime;
                    dtp_BeginTime.MaxDate = DateTimePicker.MaximumDateTime;

                    dtp_EndTime.MinDate = DateTimePicker.MinimumDateTime;
                    dtp_EndTime.MaxDate = DateTimePicker.MaximumDateTime;

                    dtp_BeginTime.Value = param.DateDebut;
                    dtp_EndTime.Value = param.DateFin;
                }

                // << Task #9123 Pax2Sim - Scenario Properties - Parameters Saving issue
                cb_SamplingStep.Text = param.SamplingStep.ToString();
                cb_AnalysisRange.Text = param.AnalysisRange.ToString();
                cb_StatisticStep.Text = param.StatisticsStep.ToString();
                //cb_AnalysisRange.Text = param.AnalysisRange.ToString();
                //cb_SamplingStep.Text = param.SamplingStep.ToString();
                //cb_StatisticStep.Text = param.StatisticsStep.ToString();
                // >> Task #9123 Pax2Sim - Scenario Properties - Parameters Saving issue                

                cb_StStepPolitic.Text = param.StatisticsStepMode;
            }
            //gb_ParkingAnalysis            
            // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            if (param.percentilesLevels.Count == 3)
            {
                firstLevelTextBox.Text = param.percentilesLevels[0].ToString();
                secondLevelTextBox.Text = param.percentilesLevels[1].ToString();
                thirdLevelTextBox.Text = param.percentilesLevels[2].ToString();
            }
            else if (Donnees.Levels != null && Donnees.Levels.Length == 3)
            {
                firstLevelTextBox.Text = Donnees.Levels[0].ToString();
                secondLevelTextBox.Text = Donnees.Levels[1].ToString();
                thirdLevelTextBox.Text = Donnees.Levels[2].ToString();
            }
            // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
        }

        private void setUpAssistantForATraceAnalysisScenario()// >> Task #15556 Pax2Sim - Scenario Properties Assistant issues
        {
            //return;     // V6.225 without _Times                        
            // >> Bug #15556 Pax2Sim - Scenario Properties Assistant issues C#23            
            cb_D_CalcDeparturePeak.Checked = false;
            cb_D_CalcDeparturePeak.Enabled = false;

            cb_CalcArrivalPeak.Checked = false;
            cb_CalcArrivalPeak.Enabled = false;

            cbGeneratePaxPlanForStatic.Checked = false;
            cbGeneratePaxPlanForStatic.Enabled = false;
            // << Bug #15556 Pax2Sim - Scenario Properties Assistant issues C#23

            cb_BHS_SimulateBHS.Enabled = false;

            btn_PeakFlows.Enabled = false;
            btn_CapacityAnalysis.Enabled = false;

            bool[] bVisible = new bool[ttpOrderTabs.Length];
            int generalTabIndex = getIndex(ttpOrderTabs, tp_General);
            int ownerInfoTabIndex = getIndex(ttpOrderTabs, scenarioInfoTab);    // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#13
            int resultFiltersTabIndex = getIndex(ttpOrderTabs, tp_resultsFilters);
            for (int i = 0; i < bVisible.Length; i++)
            {
                if (i == generalTabIndex || i == ownerInfoTabIndex || i == resultFiltersTabIndex)
                    bVisible[i] = true;
                else
                    bVisible[i] = false;
            }
            addOrRemoveTabPagesByVisibilityList(bVisible);
            cbGeneratePaxPlanForStatic.Visible = !PAX2SIM.bBHS_MeanFlows && PAX2SIM.bStatic;    // PAX key flag disabled
        }

        private void addOrRemoveTabPagesByVisibilityList(bool[] bVisible)
        {
            int iIndex = 0;
            for (int i = 0; i < bVisible.Length; i++)
            {
                if (tc_General.TabPages.Contains(ttpOrderTabs[i]))
                {
                    if (!bVisible[i])
                        tc_General.TabPages.Remove(ttpOrderTabs[i]);
                    else
                        iIndex++;
                }
                else
                {
                    if (bVisible[i])
                    {
                        if (iIndex >= tc_General.TabPages.Count)
                            tc_General.TabPages.Add(ttpOrderTabs[i]);
                        else
                            tc_General.TabPages.Insert(iIndex, ttpOrderTabs[i]);
                        iIndex++;
                    }
                }
            }
        }

        private void cb_Scenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Scenario.Items.Contains(cb_Scenario.Text))
            {
                if ((SourceAnalysis != null) && (SourceAnalysis.Name == cb_Scenario.Text))
                    return;
                ParamScenario pp = Donnees.GetScenario(cb_Scenario.Text);
                isTraceAnalysisScenario = pp.isTraceAnalysisScenario;
                traceAnalysisCheckBox.Checked = pp.isTraceAnalysisScenario;
                setScenario(pp);

                string paxPlanFilePath = Donnees.getNomDuChemin() + "\\" + "Output" + "\\" + pp.Name + "\\" + "PaxPlanTable.txt";
                if (File.Exists(paxPlanFilePath))
                {
                    cb_SIM_Regenerate_Data.Enabled = dynamicSimulationUsingPaxPlan();
                    //cb_SIM_Regenerate_Data.Checked = dynamicSimulationUsingPaxPlan();                    
                }
                else
                {
                    cb_SIM_Regenerate_Data.Enabled = false;
                    cb_SIM_Regenerate_Data.Checked = true;
                }

                if (pp.deletedCustomAnalysisResultsFilters.Count > 0) // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
                {
                    string warningMessage = "The following custom filters were deleted and are not linked anymore to this scenario:";
                    foreach (string filterName in pp.deletedCustomAnalysisResultsFilters)
                    {
                        warningMessage += Environment.NewLine + " * " + filterName;
                    }
                    MessageBox.Show(warningMessage, "Deleted Result Filters", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pp.deletedCustomAnalysisResultsFilters.Clear();
                }
            }
            else
            {
                SourceAnalysis = null;
                cb_SIM_Regenerate_Data.Enabled = false;
                cb_SIM_Regenerate_Data.Checked = true;
            }
        }
        private void cb_Scenario_TextChanged(object sender, EventArgs e)
        {
            if ((cb_Scenario.Text.Contains("/")) || (cb_Scenario.Text.Contains("\\")))
            {
                String sScenario = cb_Scenario.Text;

                sScenario = sScenario.Replace("/", "_");
                sScenario = sScenario.Replace("\\", "_");
                cb_Scenario.Text = sScenario;
                cb_Scenario.SelectionStart = cb_Scenario.Text.Length;
                return;
            }
            cb_Scenario_SelectedIndexChanged(sender, e);
            if (SourceAnalysis == null && isTraceAnalysisScenario)
            {
                isTraceAnalysisScenario = false;
                traceAnalysisCheckBox.Checked = isTraceAnalysisScenario;
                rb_BHS_UsePaxPlan.Checked = true;
                cb_D_CalcDeparturePeak_CheckedChanged(null, null);
            }
        }
        #endregion

        #region Fonction statiques pour la conversion des noms des tables.
        private String ConvertToName(String FullName)
        {
            return DataManagement.DataManagerInput.ConvertToShortName(FullName);
            //int iIndex = OverallTools.FonctionUtiles.indexDansListe(FullName, Donnees.ListeNomsCompletsTables);
            //if ((iIndex < 0) || (iIndex >= Donnees.ListeNomTable.Length))
            //    return FullName;
            //return Donnees.ListeNomTable[iIndex];
        }
        private String ConvertToFullName(String Name)
        {
            return DataManagement.DataManagerInput.ConvertToFullName(Name);
            //int iIndex = OverallTools.FonctionUtiles.indexDansListe(Name, Donnees.ListeNomTable);
            //if ((iIndex < 0) || (iIndex >= Donnees.ListeNomsCompletsTables.Length))
            //    return Name;
            //return Donnees.ListeNomsCompletsTables[iIndex];
        }
        #endregion

        #region Fonction pour la gestion de la fenêtre des erreurs.
        private void SetErrors(ArrayList ErrorList)
        {
            if (ErrForm == null)
            {
                ErrForm = new Errors(ErrorList);
            }
            else
            {
                ErrForm.setErrors(ErrorList);
            }
            ErrForm.Show();
        }
        #endregion

        #region Fonction pour mettre un nom par défaut au lancement de la fenêtre.
        private void SIM_Assistant_Scenarios_Shown(object sender, EventArgs e)
        {
            if (cb_Scenario.Text == "")
            {
                if (cb_Scenario.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    if (cb_Scenario.Items.Count > 0)
                    {
                        cb_Scenario.SelectedIndex = 0;
                    }
                }
                else
                {
                    int i = 1;
                    List<String> alList = Donnees.getScenarioNames();


                    while (alList.Contains(DefaultScenarioName + i.ToString()))
                    {
                        i++;
                    }
                    cb_Scenario.Text = DefaultScenarioName + i.ToString();
                }
            }
            cb_D_CalcDeparturePeak_CheckedChanged(null, null);
        }
        #endregion

        #region Fonctions pour la gestion de openning table
        private void cb_PP_Opening_CheckedChanged(object sender, EventArgs e)
        {
            cb_PP_Opening_CI.Enabled = cb_PP_Opening.Checked;
            UpdateComboBox(cb_PP_Opening_CI, cb_PP_Opening_CI.Enabled);
        }
        #endregion

        #region Pour la partie BHS
        private void rb_BHS_UsePaxPlan_CheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked)
                return;
            //applyChanges(rb_BHS_UsePaxPlan.Checked, tp_PaxPlan);
            if (rb_BHS_UsePaxPlan.Checked)
            {
                cb_PP_GeneratePaxPlan.Checked = true;
            }
            else
            {
                cb_PP_GeneratePaxPlan.Checked = false;
            }
            //Gestion des informations pour le bagplan.
            lbl_BHS_BagPlanScenario.Enabled = rb_BHS_BagPlan.Checked;
            cb_BHS_BagPlan.Enabled = rb_BHS_BagPlan.Checked;
            if (rb_BHS_BagPlan.Checked)
            {
                String sSelected = cb_BHS_BagPlan.Text;
                cb_BHS_BagPlan.Items.Clear();
                cb_BHS_BagPlan.Text = "";
                cb_BHS_BagPlan.Items.AddRange(Donnees.getScenariosReadyToSimulateWithBagPlan().ToArray());
                if (cb_BHS_BagPlan.Items.Contains(sSelected))
                    cb_BHS_BagPlan.SelectedIndex = cb_BHS_BagPlan.Items.IndexOf(sSelected);
                /*else
                    if (cb_BHS_BagPlan.Items.Count > 0)
                        cb_BHS_BagPlan.SelectedIndex = 0;*/
            }

            cb_PP_GeneratePaxPlan.Enabled = false;
            /*La gestion des mean flows. Activation des mean flows suivant la sélection faite.*/
            lbl_BHS_Arrival_MeanFlows.Enabled = rb_BHS_UseMeanFlow.Checked;
            cb_BHS_Arrival_MeanFlowTable.Enabled = rb_BHS_UseMeanFlow.Checked;
            lbl_BHS_CheckIn_MeanFlows.Enabled = rb_BHS_UseMeanFlow.Checked;
            cb_BHS_CheckIn_MeanFlowTable.Enabled = rb_BHS_UseMeanFlow.Checked;
            lbl_BHS_Transfer_MeanFlows.Enabled = rb_BHS_UseMeanFlow.Checked;
            cb_BHS_Transfer_MeanFlowTable.Enabled = rb_BHS_UseMeanFlow.Checked;
            if (!rb_BHS_UseMeanFlow.Checked)
            {
                cb_BHS_Arrival_MeanFlowTable.Text = "";
                cb_BHS_CheckIn_MeanFlowTable.Text = "";
                cb_BHS_Transfer_MeanFlowTable.Text = "";
            }

            /*Gestion de l'utilisation de la table des containers */
            lbl_BHS_ArrivalContainers.Enabled = rb_BHS_UsePaxPlan.Checked || rb_BHS_BagPlan.Checked;
            cb_BHS_Arrival_Containers.Enabled = rb_BHS_UsePaxPlan.Checked || rb_BHS_BagPlan.Checked;
            if (!rb_BHS_UsePaxPlan.Checked)
            {
                cb_BHS_Arrival_Containers.Text = "";
            }
            cb_D_CalcDeparturePeak_CheckedChanged(null, null);

            // >> Bug #13579 BHS Analysis Assistant
            resetBeginEndDateUsingMeanFlows();  // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason
            // << Bug #13579 BHS Analysis Assistant
        }

        // >> Bug #13579 BHS Analysis Assistant
        private void getMinMaxDatesFromMeanFlowTables(List<string> meanFlowTableNames,
            out DateTime minGlobalDate, out DateTime maxGlobalDate)
        {
            minGlobalDate = DateTime.MaxValue;
            maxGlobalDate = DateTime.MinValue;

            foreach (string tableName in meanFlowTableNames)
            {
                DataTable meanFlowTable = Donnees.getTable("Input", tableName);
                if (meanFlowTable != null)
                {
                    DateTime minDate = OverallTools.DataFunctions.getMinDate(meanFlowTable, 0);
                    DateTime maxDate = OverallTools.DataFunctions.getMaxDate(meanFlowTable, 0);
                    if (minDate != DateTime.MinValue && minGlobalDate > minDate)
                    {
                        minGlobalDate = minDate;
                    }
                    if (maxDate != DateTime.MinValue && maxGlobalDate < maxDate)
                    {
                        maxGlobalDate = maxDate;
                    }
                }
            }
        }
        // << Bug #13579 BHS Analysis Assistant

        private void UpdateBHSComboBox(ComboBox cb_BHS, int iIndex, bool bEnabled)
        {
            cb_BHS.Items.Clear();
            if ((!cb_BHS.Enabled) && (!bEnabled))
                return;
            if ((cb_BHS.Tag == null) || (cb_BHS.Tag.GetType() != typeof(String)) || (cb_BHS.Tag.GetType() != typeof(string)))
                return;
            //String Table = GestionDonneesHUB2SIM.BHS_Prefixe + iIndex.ToString() +"_"+ cb_BHS.Tag.ToString();
            List<String> values = Donnees.getValidTables("Input", iIndex, cb_BHS.Tag.ToString());
            foreach (String value in values)
            {
                OverallTools.FonctionUtiles.AddSortedItem(cb_BHS, value);
            }
            if (cb_BHS.Items.Count > 0)
                cb_BHS.SelectedIndex = 0;
            else
                cb_BHS.Text = "";
        }

        private void cb_BHS_Terminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iIndex = -1;
            if (cb_BHS_Terminal.Text.Length > 0)
            {
                //On détermine l'index du Terminal
                String sIndex = cb_BHS_Terminal.Text.Substring(GlobalNames.BHS_Prefixe_Name.Length);

                if (!Int32.TryParse(sIndex, out iIndex))
                    iIndex = -1;
            }
            if ((cb_BHS_Terminal.SelectedIndex == -1) || (iIndex == -1))
            {
                return;
            }
            UpdateBHSComboBox(cb_BHS_General, iIndex, true);//!PAX2SIM.bRuntime);
            UpdateBHSComboBox(cb_BHS_ArrivalInfeedGroups, iIndex, true);//!PAX2SIM.bRuntime);
            UpdateBHSComboBox(cb_BHS_CI_Group, iIndex, true);//!PAX2SIM.bRuntime);
            UpdateBHSComboBox(cb_BHS_HBS3_Routing, iIndex, true);//!PAX2SIM.bRuntime);
            UpdateBHSComboBox(cb_BHS_TransferInfeedGroups, iIndex, true);//!PAX2SIM.bRuntime);
            UpdateBHSComboBox(cb_BHS_CI_Collectors, iIndex, true);//!PAX2SIM.bRuntime);
            UpdateBHSComboBox(cb_BHS_CI_Routing, iIndex, true);//!PAX2SIM.bRuntime);
            UpdateBHSComboBox(cb_BHS_TransferRouting, iIndex, true);//!PAX2SIM.bRuntime);

            UpdateBHSComboBox(cb_BHS_FlowSplit, iIndex, false);
            UpdateBHSComboBox(cb_BHS_Process, iIndex, false);
            UpdateBHSComboBox(cb_BHS_Arrival_Containers, iIndex, !rb_BHS_UseMeanFlow.Checked);

            UpdateBHSComboBox(cb_BHS_Arrival_MeanFlowTable, iIndex, rb_BHS_UseMeanFlow.Checked);
            UpdateBHSComboBox(cb_BHS_CheckIn_MeanFlowTable, iIndex, rb_BHS_UseMeanFlow.Checked);
            UpdateBHSComboBox(cb_BHS_Transfer_MeanFlowTable, iIndex, rb_BHS_UseMeanFlow.Checked);
        }
        private void cb_BHS_MeanFlowTable_EnabledChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (!cb.Enabled)
            {
                cb.Text = "";
                cb.Items.Clear();
                return;
            }
            int iIndex = -1;
            if (cb_BHS_Terminal.Text.Length > 0)
            {
                //On détermine l'index du Terminal
                String sIndex = cb_BHS_Terminal.Text.Substring(GlobalNames.BHS_Prefixe_Name.Length);

                if (!Int32.TryParse(sIndex, out iIndex))
                    iIndex = -1;
            }
            if ((cb_BHS_Terminal.SelectedIndex == -1) || (iIndex == -1))
            {
                return;
            }
            UpdateBHSComboBox(cb, iIndex, false);
        }
        #endregion




        #region Partie pour la simulation

        #region Les variables utilisées pour la simulation.

        private String sDataDirectory;
        private String sOutputsDirectory;


        private bool bSimulatePax;
        private bool bSimulateBag;
        private bool bSimul8Model;
        private bool bExperiorModel;
        private bool bCancel = false;
        private bool IsModifiable = true;
        private bool bSimulationSucceed;
        private bool bSimulationLaunched;
        private bool bUseAutomod;
        public bool skipAnalysisMode // >> Task #13880 Various UI improvements and fixes
        {
            get
            {
                return simulationModeCheckBox.Checked;
            }
        }

        Thread tSimulation;
        private Hashtable htBagsResults;
        Hashtable htPKGResults;
        List<String> lsBHSUsedNames;
        private static Process proc;
        private ParamScenario paGlobalParams;
        OverallTools.PaxTraceAnalysis AnalysePaxTraceClass_;
        public Hashtable getBagsResults()
        {
            return htBagsResults;
        }
        public Hashtable getPKGResults()
        {
            return htPKGResults;
        }
        public List<String> BHSUsedNames
        {
            get
            {
                return lsBHSUsedNames;
            }
        }
        public OverallTools.PaxTraceAnalysis getPaxResults()
        {
            return AnalysePaxTraceClass_;
        }
        #endregion

        #region Les délégués qui permettent de communiqué avec le thread.
        /*
        delegate void AnalysePaxTraceCallBack();*/
        delegate void SaveResultsCallBack(Hashtable htResults, List<String> Names);
        private void SaveResults(Hashtable htResults, List<String> Names)
        {
            if (this.InvokeRequired)
            {
                SaveResultsCallBack d = new SaveResultsCallBack(SaveResults);
                this.Invoke(d, new object[] { htResults, Names });
            }
            else
            {
                if (Names == null)
                {
                    htPKGResults = htResults;
                }
                else
                {
                    htBagsResults = htResults;
                    lsBHSUsedNames = Names;
                }
            }
        }


        delegate void DisplayErreurCallBack(String text);
        private void DisplayErreur(String text)
        {
            if (this.InvokeRequired)
            {
                DisplayErreurCallBack d = new DisplayErreurCallBack(DisplayErreur);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                MessageBox.Show(text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        delegate void EndOfSimulationCallBack(String sMessage);
        private void EndOfSimulation(String sMessage)
        {
            if (this.InvokeRequired)
            {
                EndOfSimulationCallBack d = new EndOfSimulationCallBack(EndOfSimulation);
                this.Invoke(d, new Object[] { sMessage });
            }
            else
            {
                Donnees.ResetUseException();
                if (sMessage == "Simul8")
                {
                    DialogResult = DialogResult.Ignore;
                    return;
                }
                if (sMessage != null)
                {
                    if (!bSimulationSucceed)
                        MessageBox.Show(sMessage, "Simulation ended", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(sMessage, "Simulation ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                bSimulationLaunched = false;
                //pb_SIM_Current.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
                btn_Ok.Enabled = true;
                btn_Cancel.Enabled = true;//IsModifiable;   // >> Task #15418 BHS Scenario Assistant

                btn_CapacityAnalysis.Enabled = true;
                //cb_SIM_AnalyseStep.Enabled = true;
                btn_SIM_Start.Enabled = true;
                lbl_SIM_Current.Text = "";
                btn_SIM_CancelSimulation.Enabled = false;
                //cb_SIM_MakeUpSegregation.Enabled = (!cb_CustomizedModel.Checked) && cb_SIM_MakeUpSegregation.Visible;

                lbl_SIM_DynReclaim.Enabled = true;
                cb_SIM_DynReclaimTerminal.Enabled = true;
                cb_CustomizedModel.Enabled = ((!cb_BHS_SimulateBHS.Checked) && cb_PP_GeneratePaxPlan.Checked && (!cb_Gen_Tms.Checked) && PAX2SIM.bAnimatedQueues) ||
                                             ((cb_BHS_SimulateBHS.Checked) && (bBHSProcessEnabled) && ((!rb_BHS_UseMeanFlow.Checked) && (!rb_BHS_BagPlan.Checked)));
                /*cb_CustomizedModel.Enabled = ((!cb_BHS_SimulateBHS.Checked) && cb_PP_GeneratePaxPlan.Checked && (!cb_Gen_Tms.Checked) && PAX2SIM.bAnimatedQueues) ||
                                             ((cb_BHS_SimulateBHS.Checked) && bBHSProcessEnabled);*/
                /*cb_CustomizedModel.Enabled = ((!cb_BHS_SimulateBHS.Checked) && cb_PP_GeneratePaxPlan.Checked && PAX2SIM.bAnimatedQueues) ||
                                             ((cb_BHS_SimulateBHS.Checked || cb_Gen_Tms.Checked) && bBHSProcessEnabled);*/
                /*b_CustomizedModel.Enabled = (paGlobalParams.PaxSimulation && PAX2SIM.bAnimatedQueues)||
                                             ((cb_BHS_SimulateBHS.Checked || cb_Gen_Tms.Checked) && bBHSProcessEnabled);*/
                gb_SIM_Model.Enabled = cb_CustomizedModel.Checked;
                //cb_CustomizedModel.Enabled = (cb_BHS_SimulateBHS.Checked || cb_Gen_Tms.Checked || PAX2SIM.bAnimatedQueues);

                txt_SIM_WarmUp.Enabled = true;
                // << Task #8569 Pax2Sim - Scenario Properties - Regenerate PaxPlan                
                cb_SIM_Regenerate_Data.Enabled = dynamicSimulationUsingPaxPlan();//true;//PAX2SIM.bJNK;
                cb_SIM_Regenerate_Data.Checked = dynamicSimulationUsingPaxPlan();
                // >> Task #8569 Pax2Sim - Scenario Properties - Regenerate PaxPlan
            }
        }
        delegate void SetValueCallback(int Value, String sMessage);
        private void SetValue(int value, String sMessage)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.pb_SIM_TotalOperations.InvokeRequired)
            {

                SetValueCallback d = new SetValueCallback(SetValue);
                this.Invoke(d, new object[] { value, sMessage });
            }
            else
            {
                lbl_SIM_Current.Text = sMessage;
                this.pb_SIM_TotalOperations.Value += value;
            }
        }
        delegate void SimulationSucceedCallback();
        private void SimulationSucceed()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.pb_SIM_TotalOperations.InvokeRequired)
            {

                SimulationSucceedCallback d = new SimulationSucceedCallback(SimulationSucceed);
                this.Invoke(d, null);
            }
            else
            {
                bSimulationSucceed = true;
            }
        }

        delegate Boolean MessageCallBack(String sMessage);
        private Boolean Message(String sMessage)
        {
            if (this.InvokeRequired)
            {
                MessageCallBack d = new MessageCallBack(Message);
                return (Boolean)this.Invoke(d, new Object[] { sMessage });
            }
            else
            {
                return (MessageBox.Show(sMessage, "Simulation ended", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK);
            }
        }
        #endregion

        #region Fonction pour savoir si un Scenario est pret à être simulé.
        private bool IsAbleToSimulate()
        {
            //OverallTools.ExternFunctions.PrintLogFile("File path: " + lbl_SIM_ModelName.Text);
            if (!cb_CustomizedModel.Checked)
                return true;
            if (!System.IO.File.Exists(lbl_SIM_ModelName.Text))
            {
                //OverallTools.ExternFunctions.PrintLogFile("File not found...");
                return false;
            }
            return true;
        }
        #endregion

        #region Gestion des clics sur les boutons du formulaire de simulation.
        // >> CheckIn ShowUp default table warning
        private bool checkInShowUpUsedHasDefaultFormat(string checkInShowUpTableName)
        {
            DataTable ciShowUp = Donnees.getTable("Input", checkInShowUpTableName);

            if (ciShowUp == null || ciShowUp.Rows == null)
                return false;

            if (ciShowUp.Rows.Count == 1)
                return true;
            return false;
        }
        // << CheckIn ShowUp default table warning

        private void btn_SIM_CancelSimulation_Click(object sender, EventArgs e)
        {
            btn_SIM_CancelSimulation.Enabled = false;
            bCancel = true;
            if (bUseAutomod)
            {
                proc.Kill();
                tSimulation.Abort();
                EndOfSimulation(null);
                bUseAutomod = false;
            }
        }
        private void btn_SIM_Start_Click(object sender, EventArgs e)
        {
            if (!isScenarioDateLengthAcceptable())  // >> Task #13366 Error message correction
                return;

            ParamScenario psSimulation = getAnalysis();

            if (SourceAnalysis != null)
                SourceAnalysis = psSimulation;

            if (psSimulation.ModelName.Length == 0)
                return;

            // >> Task #15087 Pax2Sim - BHS Analysis - Collector - C#8
            if (tc_General.TabPages.Contains(tp_resultsFilters))
            {
                if (psSimulation.analysisResultsFilters.Count == 0)
                {
                    DialogResult dr = MessageBox.Show("You did not activate any Result Filters." + Environment.NewLine + " Are you sure you want to continue?",
                        "Result Filters", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                        return;
                }
                if (psSimulation.flowTypes.Count == 0)
                {
                    DialogResult dr = MessageBox.Show("You did not activate any Flow Types." + Environment.NewLine + " Are you sure you want to continue?",
                        "Flow Types", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                        return;
                }
            }
            // << Task #15087 Pax2Sim - BHS Analysis - Collector - C#8

            // >> CheckIn ShowUp default table warning
            if (psSimulation.CI_ShowUpTable != null && psSimulation.CI_ShowUpTable != ""
                && checkInShowUpUsedHasDefaultFormat(psSimulation.CI_ShowUpTable))
            {
                DialogResult dr = MessageBox.Show("You are using a single line CheckIn ShowUp profile." + Environment.NewLine + " Are you sure you want to continue?",
                    "CheckIn ShowUp Profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                    return;
            }
            // << CheckIn ShowUp default table warning

            if (tc_General.TabPages.Contains(tp_SIM_Start))
            {
                Dictionary<string, List<string>> userDataDictionary = Donnees.getUserData();
                string warningMessage = "";
                if (psSimulation.clearAutomodMainData && !psSimulation.exportAutomodMainData)
                    warningMessage += "You are about to clear the simulation main data directory without exporting any data." + Environment.NewLine;
                if (userDataDictionary != null && userDataDictionary.Count > 0 && psSimulation.clearAutomodUserData && !psSimulation.ExportUserData)
                    warningMessage += "You are about to clear the simulation UserData directory without exporting any UserData." + Environment.NewLine;
                if (warningMessage != "")
                    warningMessage += "Are you sure you want to continue?";

                if (warningMessage != "")
                {
                    DialogResult dr = MessageBox.Show(warningMessage, "Simulation data Export", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                        return;
                }
            }

            bSimulationLaunched = true;

            #region Mise à jour de l'affichage.
            btn_CapacityAnalysis.Enabled = false;
            btn_PeakFlows.Enabled = false;
            btn_Ok.Enabled = false;
            btn_Cancel.Enabled = false;
            this.ControlBox = false;
            lbl_SIM_DynReclaim.Enabled = false;
            cb_SIM_DynReclaimTerminal.Enabled = false;

            btn_SIM_CancelSimulation.Enabled = true;

            lbl_SIM_Current.Text = "Export data for simulation";
            //cb_SIM_MakeUpSegregation.Enabled = false;
            txt_SIM_WarmUp.Enabled = false;
            btn_SIM_Start.Enabled = false;
            cb_CustomizedModel.Enabled = false;
            gb_SIM_Model.Enabled = false;
            cb_SIM_Regenerate_Data.Enabled = false;
            pb_SIM_TotalOperations.Value = 0;
            bCancel = false;
            htBagsResults = null;
            htPKGResults = null;
            cb_Scenario.Enabled = false;
            #endregion

            AnalysePaxTraceClass_ = null;
            htBagsResults = null;
            htPKGResults = null;
            IsModifiable = false;
            bSimulationSucceed = false;
            bUseAutomod = false;
            #region Les parametres pour la simulation.

            paGlobalParams = getAnalysis();
            paGlobalParams.HasBeenSimulated = true;
            SourceAnalysis = paGlobalParams;
            bSimulatePax = cb_PP_GeneratePaxPlan.Checked;

            bSimulateBag = cb_BHS_SimulateBHS.Checked;

            if (bSimulateBag)
            {
                bSimulatePax = paGlobalParams.UsePaxPlan;
            }
            #endregion

            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            DateTime now = DateTime.Now;
            if (scenarioCreationDate == DateTime.MinValue && scenarioLastUpdateDate == DateTime.MinValue)
            {
                scenarioCreationDate = now;
                scenarioLastUpdateDate = now;
            }
            else
            {
                scenarioLastUpdateDate = now;
            }
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update

            if (!Donnees.ScenarioExist(paGlobalParams.Name))
            {
                TreeNode tnTmp = Donnees.AddScenario(paGlobalParams.Name, null, null, null, null, null, paGlobalParams, null);
                if (tnTmp == null)
                {
                    EndOfSimulation("Unable to create a new Scenario");
                    return;
                }
            }
            else
            {
                Donnees.ReplaceScenario(paGlobalParams.Name, paGlobalParams);
            }
            bSimul8Model = lbl_SIM_ModelName.Text.EndsWith(".S8");
            bExperiorModel = lbl_SIM_ModelName.Text.EndsWith(".Experior");
            if (PAX2SIM.bDebug)
            {
                SimulationLaunched();
                return;
            }
            tSimulation = new Thread(new ThreadStart(SimulationLaunched));
            tSimulation.Start();
        }

        #endregion

        #region La partie qui lance la simulation
        private void SimulationLaunched()
        {
            #region Le pas utilisé pour faire avancé la barre de progression.
            int iPas = 33;
            if (bSimulatePax && (bSimulateBag || paGlobalParams.DynamicReclaimAllocation))
            {
                iPas = 25;
            }
            #endregion

            PAX2SIM.CheckDecimalSeparator();
            Donnees.SetUseException(SourceAnalysis);

            bool bDisplayGraphic = paGlobalParams.DisplayModel;
            String Directory = "";
            String sNomModel = paGlobalParams.ModelName;
            String sMessageFile = "";
            String sMFFFileCompletePath = "";
            string mffFileDirectory = "";
            String sBG_StatsCounterFile = "";

            if ((sNomModel == "Default") || (sNomModel == null) || (sNomModel == ""))
            {
                String sRoot = System.IO.Path.GetPathRoot(Application.StartupPath);
                Directory = "Default";
                sNomModel = sRoot + "Temp\\Model\\" + GestionDonneesHUB2SIM.sHub2simModel + ".exe";
                sDataDirectory = sRoot + "Temp\\Model\\Data\\";
                sOutputsDirectory = sRoot + "Temp\\Model\\Outputs\\";
                sMessageFile = sRoot + "Temp\\Model\\" + GestionDonneesHUB2SIM.sHub2simModel + ".message";

                mffFileDirectory = sRoot + "Temp\\Model\\" + GestionDonneesHUB2SIM.sHub2simModel + ".dir\\";
                sMFFFileCompletePath = mffFileDirectory + GestionDonneesHUB2SIM.sHub2simModel + ".mff";

                sBG_StatsCounterFile = sRoot + "Temp\\Model\\" + GestionDonneesHUB2SIM.sHub2simModel + ".pax2sim.bg_statscounter";
            }
            else
            {
                if (bExperiorModel)
                {
                    Directory = OverallTools.ExternFunctions.getUserDirectoryForPax2sim();

                }
                else
                {
                    Directory = sNomModel.Substring(0, sNomModel.LastIndexOf('\\')) + "\\";
                }
                if (Directory.Length == 0)
                {
                    DisplayErreur("Some errors happened during the access of the model.");
                    EndOfSimulation("Unable to locate the model.");
                    return;
                }
                sDataDirectory = Directory + "Data\\";
                sOutputsDirectory = Directory + "Outputs\\";
                sBG_StatsCounterFile = Directory + "hub2sim.pax2sim.bg_statscounter";
                sMessageFile = sNomModel.Remove(sNomModel.Length - 3) + "message";

                string modelNameWithExtension = sNomModel.Substring(sNomModel.LastIndexOf("\\") + 1);
                string modelNameOnly = modelNameWithExtension.Remove(modelNameWithExtension.Length - 4);
                string pathWithoutFile = sNomModel.Substring(0, sNomModel.LastIndexOf("\\") + 1);
                mffFileDirectory = pathWithoutFile + modelNameOnly + ".dir\\";//GestionDonneesHUB2SIM.sHub2simModel + ".dir\\"; mff file's directory should be named following the exe model file
                sMFFFileCompletePath = mffFileDirectory + modelNameOnly + ".mff";
            }
            // >> Bug #14900 MFF file not created
            if ((!OverallTools.ExternFunctions.CheckCreateDirectory(sDataDirectory)) ||
                   (!OverallTools.ExternFunctions.CheckCreateDirectory(sOutputsDirectory)))
            {
                DisplayErreur("Some errors happened during the access of the model.");
                EndOfSimulation("Unable to create the directories for the simulation files.");
                return;
            }

            int simulationEngineSeed = SourceAnalysis.simulationEngineSeed;
            if (!SourceAnalysis.useUserProvidedSimulationEngineSeed)
                simulationEngineSeed = 1;
            if (SourceAnalysis.useUserProvidedSimulationEngineSeed)
                OverallTools.AutomodFiles.GenerateMFF(sMFFFileCompletePath, mffFileDirectory, simulationEngineSeed);
            // >> Bug #14900 MFF file not created

            if ((bSimulatePax) || (paGlobalParams.BagPlan))
            {
                Hashtable htPKG = Donnees.Maj_Scenario(paGlobalParams.Name, null, null, paGlobalParams, null, this, null, null, null, null);
                SaveResults(htPKG, null);
            }
            this.ChargementFichier("");
            bool bGenerate = false;
            String sError = "";
            try
            {/*
                if (paGlobalParams.ExportUserData)
                {
                    Donnees.SaveUserDataForScenario(sDataDirectory + "User\\");
                }*/
                bGenerate = Donnees.SaveSimulationFiles(paGlobalParams.Name, Directory, bSimulatePax, bSimulateBag, this,
                    out _automodTablesList);    // >> Task #13361 FP AutoMod Data tables V3
            }
            catch (Exception Excep)
            {
                bGenerate = false;
                sError = "Err00328 : An exception appeared during the generation of simulation data files : " + Excep.Message;
                OverallTools.ExternFunctions.PrintLogFile("Err00328: " + this.GetType().ToString() + " throw an exception during the generating of simulation data files: " + Excep.Message);
            }
            try
            {
                if (System.IO.File.Exists(sMessageFile))
                    System.IO.File.Delete(sMessageFile);
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02068: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }

            if (!bGenerate)
            {
                if (sError == "")
                    sError = GestionDonneesHUB2SIM.getLastError();
                if (sError == "")
                    sError = "Some errors happened during the saving of the tables";
                EndOfSimulation(sError);
                return;
            }
            this.ChargementFichier("");
            SetValue(iPas, "Simulating the airport");

            // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
            String scenarioName = "";
            if (paGlobalParams != null)
                scenarioName = paGlobalParams.Name;
            OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "Simulating the airport");
            // >> Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps

            if (bCancel)
            {
                EndOfSimulation(null);
                return;
            }
            if (bSimul8Model)
            {
                //We are simulating the airport using Simul8. We have now to exit the scenario assistant, and to
                //show the Simul8 model in the right window.
                //The main question is to know if we would come back here to analyse the results.
                EndOfSimulation("Simul8");
                return;
            }
            bUseAutomod = true;
            //<< Task #7405 - new Desk and extra information for Pax            
            try
            {
                if (bExperiorModel)
                {
                    proc = Process.Start(sNomModel);
                }
                else
                {
                    string amrunArg = sNomModel;
                    if (paGlobalParams.DisplayModel)
                    {
                        if (paGlobalParams.useUserProvidedSimulationEngineSeed)
                            amrunArg += " -M";
                        else
                            amrunArg += ""; // for clarity - display mode and NO MFF file => call amrun only with the model name
                    }
                    else
                    {
                        if (paGlobalParams.useUserProvidedSimulationEngineSeed)
                            amrunArg += " -Mwn";
                        else
                            amrunArg += " -wn";
                    }
                    proc = Process.Start("amrun", amrunArg);
                }
                proc.WaitForExit();
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile("Exception while trying to start the simulation. " + e.Message);
            }
            //>> Task #7405 - new Desk and extra information for Pax
            bUseAutomod = false;
            if (bCancel)
            {
                EndOfSimulation(null);
                return;
            }
            if ((!System.IO.File.Exists(sMessageFile)) && (!bExperiorModel))
            {
                EndOfSimulation("The model had a problem. Unable to open Automod model.Please check the compatibility of the Automod model with the installed version of Automod.");
                return;
            }
            if (!bExperiorModel)
            {
                try
                {
                    System.IO.StreamReader srMessageFile = new System.IO.StreamReader(sMessageFile);
                    if (srMessageFile == null)
                    {
                        EndOfSimulation("The model had a problem. Unable to open Automod model.");
                        return;
                    }
                    bool bKeyNotFound = false;
                    bool bCannotContinue = false;
                    bool bFileNotFound = false;
                    bool bKeyInvalid = false;
                    bool bError = false;
                    String sFileNotFound = "";
                    while (srMessageFile.Peek() != -1)
                    {
                        String sLine = srMessageFile.ReadLine();
                        bKeyNotFound = bKeyNotFound || sLine.Contains("Key not found, model terminated.");
                        bCannotContinue = bCannotContinue || sLine.Contains("Simulation terminating");
                        bKeyInvalid = bKeyInvalid || sLine.Contains("Key not found");
                        bFileNotFound = bFileNotFound || sLine.Contains("File not found:");
                        bError = bError || sLine.StartsWith("!! ERROR:");
                        if (bFileNotFound || bKeyInvalid)
                        {
                            sFileNotFound = sLine;
                        }
                        if (bFileNotFound || bKeyNotFound || bError || bKeyInvalid)
                            break;
                    }
                    srMessageFile.Close();
                    if (bKeyNotFound)
                    {
                        EndOfSimulation("The model had a problem. Unable to find the hardware key for the execution of the model.");
                        return;
                    }
                    if (bFileNotFound)
                    {
                        EndOfSimulation("The model had a problem : " + sFileNotFound);
                        return;
                    }
                    if (bKeyInvalid)
                    {
                        EndOfSimulation("The model had a problem : " + sFileNotFound);
                        return;
                    }
                    if (!skipAnalysisMode)  // >> Task #13880 Various UI improvements and fixes
                    {
                        if ((bError || (!bCannotContinue))
                            && (!Message("The model run did not finish properly. The user may have stopped the simulation before the end. Do you want to analyze partial results?")))   // >> Task #13366 Error message correction
                        {
                            EndOfSimulation(null);
                            return;
                        }
                    }
                }
                catch (Exception exc)
                {
                    EndOfSimulation("The model had a problem. Unable to open Automod model.");
                    OverallTools.ExternFunctions.PrintLogFile("Except02069: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                    return;
                }
            }
            // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps            
            OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "Airport simulation finished.");
            // >> Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps

            // >> Task #13880 Various UI improvements and fixes
            if (skipAnalysisMode)
            {
                EndOfSimulation("Skip Analysis Mode: The results will not be processed");
                btn_Cancel.Enabled = true;
                return;
            }
            // << Task #13880 Various UI improvements and fixes

            if (bSimulateBag || paGlobalParams.DynamicReclaimAllocation)
            {
                // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
                OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "Analyzing baggage results");
                // >> Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps

                SetValue(iPas, "Analyzing baggage results");
                //////////////////  // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
                string bagTraceFileName = sOutputsDirectory + "BagTrace.txt";
                //string bagTraceFileName = "D:\\data\\PAX2SIM\\tasks\\#15 088 Times and Indexes\\BagTrace.txt";

                paGlobalParams.bagTracePath = bagTraceFileName; // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17
                bagTracePath = bagTraceFileName;// >> Task #16728 PAX2SIM Improvements (Recurring) C#17
                /////////////////

                // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
                if (paGlobalParams.percentilesLevels.Count == 0)
                {
                    paGlobalParams.percentilesLevels.Add(Donnees.Levels[0]);
                    paGlobalParams.percentilesLevels.Add(Donnees.Levels[1]);
                    paGlobalParams.percentilesLevels.Add(Donnees.Levels[2]);
                }
                OverallTools.BagTraceAnalysis btaBagAnalysis = new OverallTools.BagTraceAnalysis(paGlobalParams.Name, bagTraceFileName, paGlobalParams.WarmUp * 60, paGlobalParams.percentilesLevels[0], paGlobalParams.percentilesLevels[1], paGlobalParams.percentilesLevels[2], paGlobalParams);
                //OverallTools.BagTraceAnalysis btaBagAnalysis = new OverallTools.BagTraceAnalysis(paGlobalParams.Name, bagTraceFileName, paGlobalParams.WarmUp * 60, Donnees.Levels[0], Donnees.Levels[1], Donnees.Levels[2]);
                // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec

                Double dEndingTime = OverallTools.DataFunctions.MinuteDifference(paGlobalParams.DateDebut, paGlobalParams.DateFin);
                OverallTools.ExternFunctions.PrintLogFile(paGlobalParams.Name + " : " + "Analyzing baggage results: Open Bag Trace Start");
                if (!btaBagAnalysis.OpenBagTrace(dEndingTime, paGlobalParams.SaveTraceMode, this))
                {
                    // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
                    OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "The bagtrace has a wrong format. Unable to analyze it.");
                    // >> Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
                    bagTracePath = "";      // >> Task #16728 PAX2SIM Improvements (Recurring) C#17
                    EndOfSimulation("The bagtrace has a wrong format. Unable to analyze it.");
                    return;
                }
                OverallTools.ExternFunctions.PrintLogFile(paGlobalParams.Name + " : " + "Analyzing baggage results: Open Bag Trace End");
                XmlNode airportStructure = Donnees.getRacine(); // >> Task #13659 IST MakeUp segregation
                OverallTools.ExternFunctions.PrintLogFile(paGlobalParams.Name + " : " + "Analyzing baggage results: GenerateResults Start");
                Hashtable htRes = btaBagAnalysis.GenerateResults(paGlobalParams.DateDebut, paGlobalParams.DateFin,
                    paGlobalParams.SamplingStep, 1, 31, this, paGlobalParams.AnalysisRange, paGlobalParams.bhsGenerateLocalIST,    // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation   // >> Task #13955 Pax2Sim -BHS trace loading issue
                    paGlobalParams.bhsGenerateGroupIST, paGlobalParams.bhsGenerateMUPSegregation,
                    airportStructure, paGlobalParams.analysisResultsFilters,
                    paGlobalParams.analysisResultsFiltersSplittedByFlow, paGlobalParams.flowTypes, paGlobalParams.percentilesLevels);    // >> Task #14280 Bag Trace Loading time too long
                ChargementFichier("");
                OverallTools.ExternFunctions.PrintLogFile(paGlobalParams.Name + " : " + "Analyzing baggage results: GenerateResults End");
                OverallTools.ExternFunctions.PrintLogFile(paGlobalParams.Name + " : " + "Analyzing baggage results: SaveResults Start");
                SaveResults(htRes, btaBagAnalysis.Names);
                OverallTools.ExternFunctions.PrintLogFile(paGlobalParams.Name + " : " + "Analyzing baggage results: SaveResults End");
                OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "Baggage results analysis finished");  // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps                
            }

            if (bSimulatePax)
            {
                // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
                OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "Analyzing passengers results");
                // >> Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps

                SetValue(iPas, "Analyzing passengers results");

                string outputDirectoryPath = sOutputsDirectory;
                if (PAX2SIM.bDebug)
                {
                    string outputsTestDir = "D:\\data\\PAX2SIM\\projects\\_Debug projects\\pax trace issue project\\P2S_ZRH\\Output\\PaxBag_Plan";//"D:\\data\\PAX2SIM\\tasks\\#15 088 Times and Indexes\\Hong Kong_SC01_AM02_lightPaxCapaTest\\Output\\";  //test only
                    outputDirectoryPath = outputsTestDir;
                }
                AnalysePaxTraceClass_ = OverallTools.PaxTraceAnalysis.AnalysePaxTrace(paGlobalParams.Name,
                    outputDirectoryPath, //outputsTestDir,//sOutputsDirectory,//outputsTestDir, // >> Pax trace analysis - Sum and BagPlan_2.txt
                    sBG_StatsCounterFile, Donnees, paGlobalParams.SamplingStep,
                    paGlobalParams.WarmUp * 60, paGlobalParams.SaveTraceMode, false);

                if (AnalysePaxTraceClass_ == null)
                {
                    // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
                    OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "The paxtrace has a wrong format.");
                    // >> Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
                    EndOfSimulation("The paxtrace has a wrong format.");
                    return;
                }

                AnalysePaxTraceClass_.GenerateAllTable(tnAirportNode_, Donnees, paGlobalParams.Name, paGlobalParams.SamplingStep, paGlobalParams.percentilesLevels); // >> Task #10484 Pax2Sim - Pax analysis - Summary with distribution levels percent

                // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
                OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "Passengers results analysis finished");
                // >> Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
            }

            // >> Task #8958 Reclaim Synchronisation mode Gantt
            String tempDirectory = OverallTools.ExternFunctions.getTempDirectoryForPax2sim();
            string originFullPath = sOutputsDirectory + GlobalNames.RECLAIM_LOG_FILE_NAME + ".txt";
            string destinationFullPath = tempDirectory + paGlobalParams.Name + "_" + GlobalNames.RECLAIM_LOG_FILE_NAME + ".tmp";

            OverallTools.ExternFunctions.PrintLogFile("Reclaim Log: source path (outputs dir): " + originFullPath);
            OverallTools.ExternFunctions.PrintLogFile("Reclaim Log: destination path (temp dir): " + destinationFullPath);

            if (tempDirectory != "" && System.IO.File.Exists(originFullPath))
            {
                if (OverallTools.ExternFunctions.CheckCreateDirectory(tempDirectory))
                    OverallTools.ExternFunctions.CopyFile(destinationFullPath, originFullPath, "", null, null, null);
            }
            // << Task #8958 Reclaim Synchronisation mode Gantt

            ChargementFichier(" ");
            SetValue(iPas, "");
            SimulationSucceed();
            EndOfSimulation("Simulation ended with success.");

            // << Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
            OverallTools.ExternFunctions.PrintLogFile(scenarioName + " : " + "Simulation ended with success.");
            // >> Task #9251 Pax2Sim - Simulation - print in the Log the simulation steps
        }
        #endregion





        #endregion

        #region partie de l'implémentation de SIM_LoadingForm
        #region Les délégués pour les fonctions utilisées.
        delegate void ChargementCallBack(String Fichier);
        delegate void KillWindowCallBack();
        delegate void SetNumberFileCallBack(int Nombre);
        delegate void ResetCallBack(int iValue);
        #endregion

        public void KillWindow()
        {
            //Non usité dans cette partie du projet.
            return;
        }
        public void Reset()
        {
            //Non usité dans cette partie du projet.
            return;
        }

        public void Reset(int iValue)
        {
            if (this.InvokeRequired)
            {
                ResetCallBack chgCB = new ResetCallBack(Reset);
                this.Invoke(chgCB, iValue);
            }
            else
            {
                if (pb_SIM_Current.Maximum < iValue)
                {
                    pb_SIM_Current.Value = pb_SIM_Current.Maximum;
                }
                else
                {
                    pb_SIM_Current.Value = iValue;
                }
            }
        }

        public void ChargementFichier(String Fichier)
        {
            if (this.InvokeRequired)
            {
                ChargementCallBack chgCB = new ChargementCallBack(ChargementFichier);
                this.Invoke(chgCB, Fichier);
            }
            else
            {
                if (Fichier != "")
                {
                    pb_SIM_Current.Style = ProgressBarStyle.Blocks;
                    if (pb_SIM_Current.Value < pb_SIM_Current.Maximum)
                        pb_SIM_Current.Value += 1;
                }
                else
                {
                    pb_SIM_Current.Style = ProgressBarStyle.Marquee;
                }
                lbl_CurrentOperation.Text = Fichier;
            }
        }

        public void setFileNumber(int iNombre)
        {
            if (this.InvokeRequired)
            {
                SetNumberFileCallBack snfCB = new SetNumberFileCallBack(setFileNumber);
                this.Invoke(snfCB, iNombre);
            }
            else
            {
                pb_SIM_Current.Maximum = iNombre;
            }
        }

        public int getFileNumber()
        {
            return pb_SIM_Current.Value;
        }

        public Boolean setChenillard
        {
            set
            {
                if (value)
                {
                    pb_SIM_Current.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    pb_SIM_Current.Style = ProgressBarStyle.Blocks;
                }
            }
        }
        #endregion
        public void setDelhiMode()
        {
            if (!PAX2SIM.bDelhi)
                return;
            cb_BHS_Arrival_MeanFlowTable.Enabled = false;
            cb_BHS_Transfer_MeanFlowTable.Enabled = false;
            cb_BHS_CheckIn_MeanFlowTable.Enabled = false;
        }
        public void setRuntimeMode()
        {
            if (!PAX2SIM.bRuntime)
                return;
            if ((epPerimetre_ == PAX2SIM.EnumPerimetre.BHS) || (epPerimetre_ == PAX2SIM.EnumPerimetre.TMS))
            {
                cb_BHS_General.Enabled = false;
                cb_BHS_CI_Collectors.Enabled = false;
                cb_BHS_ArrivalInfeedGroups.Enabled = false;
                cb_BHS_HBS3_Routing.Enabled = false;
                cb_BHS_CI_Group.Enabled = false;
                cb_BHS_TransferInfeedGroups.Enabled = false;
                cb_BHS_CI_Routing.Enabled = false;
                cb_BHS_TransferRouting.Enabled = false;
                cb_BHS_Arrival_Containers.EnabledChanged -= new EventHandler(cb_BHS_MeanFlowTable_EnabledChanged);
                cb_BHS_Arrival_MeanFlowTable.EnabledChanged -= new EventHandler(cb_BHS_MeanFlowTable_EnabledChanged);
                cb_BHS_CheckIn_MeanFlowTable.EnabledChanged -= new EventHandler(cb_BHS_MeanFlowTable_EnabledChanged);
                cb_BHS_Transfer_MeanFlowTable.EnabledChanged -= new EventHandler(cb_BHS_MeanFlowTable_EnabledChanged);
            }
        }
        public void setMeanFlowsMode()
        {
            if (!PAX2SIM.bBHS_MeanFlows)
                return;
            if (epPerimetre_ == PAX2SIM.EnumPerimetre.BHS)
            {
                bBHSMeanFlow = PAX2SIM.bBHS_MeanFlows;
                bool bBHSPaxPlan = PAX2SIM.bBHS_PaxPlan;
                if (bBHSMeanFlow)
                {
                    cb_Gen_Tms.Enabled = bBHSPaxPlan;
                    cb_PP_GeneratePaxPlan.Visible = false;
                    cb_D_CalcDeparturePeak.Visible = false;
                    cb_CalcArrivalPeak.Visible = false;
                    gb_BHSFlows.Enabled = false;
                }
                if (bBHSPaxPlan)
                {
                    gb_BHSFlows.Enabled = false;
                }
            }
        }
        private void cb_D_CalcDeparturePeak_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_PP_GeneratePaxPlan.Checked)  // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#8
            {
                if (currentParameters != null)
                    cbGeneratePaxPlanForStatic.Checked = currentParameters.generatePaxPlanForStaticAnalysis;
                else
                    cbGeneratePaxPlanForStatic.Checked = true;
                cbGeneratePaxPlanForStatic.Enabled = false;
            }
            else
                cbGeneratePaxPlanForStatic.Enabled = true;

            UpdateVisible(bStatic_ && PAX2SIM.bStatic, bDynamic_ && PAX2SIM.bDynamic);
            UpdateEnabled();
            if (isTraceAnalysisScenario)
                setUpAssistantForATraceAnalysisScenario();
            traceAnalysisCheckBox.Visible = cb_PP_GeneratePaxPlan.Checked || cb_BHS_SimulateBHS.Checked;
            if (!cb_PP_GeneratePaxPlan.Checked && !cb_BHS_SimulateBHS.Checked)
                traceAnalysisCheckBox.Checked = false;
        }

        private bool bChangeTabForUpdate = false;
        private void tc_General_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bChangeTabForUpdate)
                return;
            if (tc_General.SelectedTab != tp_SIM_Start)
            {
                bool bEnabled = ((!bSimulationLaunched) && (!PAX2SIM.bRuntime));
                if (PAX2SIM.bRuntime)
                {
                    bEnabled = (!bSimulationLaunched) && ((tc_General.SelectedTab == tp_BHS) || (tc_General.SelectedTab == tp_UserData));
                    if (tc_General.SelectedTab == tp_BHS)
                    {
                        gb_BHSFlows.Enabled = false;
                        cb_BHS_Terminal.Enabled = false;
                    }
                }
                foreach (Control ctr in tc_General.SelectedTab.Controls)
                {
                    ctr.Enabled = bEnabled;
                }
                resetBeginEndDateUsingMeanFlows();  // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason                
            }
            else
            {
                bChangeTabForUpdate = true;
                //On va parcourir rapidement l'onglet data pour s'affranchir du problème de mise à jour des données.
                if (tc_General.TabPages.Contains(tp_Static))
                {
                    tc_General.SelectedTab = tp_Static;
                }
                if (tc_General.TabPages.Contains(tp_BHS))
                {
                    tc_General.SelectedTab = tp_BHS;
                }
                if (tc_General.SelectedTab != tp_SIM_Start)
                {
                    tc_General.SelectedTab = tp_SIM_Start;
                }
                ParamUserData pudTmp = getParamUserData();
                List<Int32> liListe = ReclaimAllocation.getAvailableTerminalForDynnamicAllocation(pudTmp);
                lbl_SIM_DynReclaim.Visible = (liListe != null);
                cb_SIM_DynReclaimTerminal.Visible = (liListe != null);

                if (liListe != null)
                {
                    String sText = cb_SIM_DynReclaimTerminal.Text;
                    if (sText == "")
                    {
                        if ((paGlobalParams != null) && (paGlobalParams.DynamicReclaimAllocation) && (paGlobalParams.TerminalReclaimAllocation != 0))
                        {
                            sText = paGlobalParams.TerminalReclaimAllocation.ToString();
                        }
                        else
                        {
                            sText = "No";
                        }
                    }
                    cb_SIM_DynReclaimTerminal.Items.Clear();
                    cb_SIM_DynReclaimTerminal.Items.Add("No");
                    foreach (Int32 i in liListe)
                        cb_SIM_DynReclaimTerminal.Items.Add(i.ToString());

                    if (cb_SIM_DynReclaimTerminal.Items.Contains(sText))
                        cb_SIM_DynReclaimTerminal.Text = sText;

                }
                else
                {
                    cb_SIM_DynReclaimTerminal.Items.Clear();
                    cb_SIM_DynReclaimTerminal.Items.Add("No");
                    cb_SIM_DynReclaimTerminal.Text = "No";
                }
                bChangeTabForUpdate = false;
            }
            if (bSimulationLaunched)
                return;
            if (tc_General.SelectedTab != tp_SIM_Start)
                return;


            //bool bEnable = ((!cb_BHS_SimulateBHS.Checked) && cb_PP_GeneratePaxPlan.Checked && (!cb_Gen_Tms.Checked) && (PAX2SIM.bAnimatedQueues || PAX2SIM.bExperior));
            //cb_CustomizedModel.Enabled =bEnable;
            cb_CustomizedModel.Enabled = (!cb_BHS_SimulateBHS.Checked && cb_PP_GeneratePaxPlan.Checked && !cb_Gen_Tms.Checked
                                            && (PAX2SIM.bAnimatedQueues || PAX2SIM.bExperior))
                                         || (cb_BHS_SimulateBHS.Checked && bBHSProcessEnabled && (!rb_BHS_UseMeanFlow.Checked && !rb_BHS_BagPlan.Checked));
            if (!cb_CustomizedModel.Enabled)
            {
                //cb_SIM_MakeUpSegregation.Checked = false;
                //cb_SIM_MakeUpSegregation.Visible = false;
                cb_ex_MakeUpSegregation.Visible = cb_SIM_MakeUpSegregation.Visible;

                if (((cb_BHS_SimulateBHS.Checked) && (!bBHSProcessEnabled || (rb_BHS_BagPlan.Checked) || (rb_BHS_UseMeanFlow.Checked))) ||
                    (cb_Gen_Tms.Checked && (!cb_BHS_SimulateBHS.Checked)))
                    cb_CustomizedModel.Checked = true;
                else if (((!cb_BHS_SimulateBHS.Checked) && cb_PP_GeneratePaxPlan.Checked) && (!PAX2SIM.bAnimatedQueues))
                    cb_CustomizedModel.Checked = false;
            }
            else if (cb_BHS_SimulateBHS.Checked)
            {
                cb_SIM_MakeUpSegregation.Visible = true;
                cb_ex_MakeUpSegregation.Visible = true;
                //cb_SIM_MakeUpSegregation.Enabled = !cb_CustomizedModel.Checked;                
            }
            else
            {
                //cb_SIM_MakeUpSegregation.Visible = false;
                cb_ex_MakeUpSegregation.Visible = cb_SIM_MakeUpSegregation.Visible;
                //cb_SIM_MakeUpSegregation.Checked = false;                
            }
            gb_SIM_Model.Enabled = cb_CustomizedModel.Checked;
            //cb_CustomizedModel.Enabled = cb_BHS_SimulateBHS.Checked || cb_Gen_Tms.Checked ||PAX2SIM.bAnimatedQueues;
            cb_CustomizedModel_CheckedChanged(null, null);
            //cb_SIM_ExportUserData.Enabled = !PAX2SIM.bRuntime;
            //clearAutomodUserDataCheckBox.Enabled = cb_SIM_ExportUserData.Enabled;

            btn_SIM_Start.Enabled = ((cb_BHS_SimulateBHS.Checked && verifBHSData())
                  || ((!cb_BHS_SimulateBHS.Checked) && (cb_PP_GeneratePaxPlan.Checked) && (verifCalcPaxPlan())));
            btn_SIM_CancelSimulation.Enabled = false;
            gb_SIM_AnalysisSettings.Enabled = cb_BHS_SimulateBHS.Checked || cb_Gen_Tms.Checked;

            exportAutomodMainDataCheckBox.Visible = cb_BHS_SimulateBHS.Checked || cb_Gen_Tms.Checked || cb_PP_GeneratePaxPlan.Checked;

            clearAutomodMainDataCheckBox.Visible = exportAutomodMainDataCheckBox.Visible;
            if (!clearAutomodMainDataCheckBox.Visible)
                clearAutomodMainDataCheckBox.Checked = false;

            clearAutomodUserDataCheckBox.Visible = cb_SIM_ExportUserData.Visible;
            if (!clearAutomodUserDataCheckBox.Visible)
                clearAutomodUserDataCheckBox.Checked = false;

            // << Task #8569 Pax2Sim - Scenario Properties - Regenerate PaxPlan
            ParamScenario scenarioParameters = Donnees.GetScenario(cb_Scenario.Text);
            string paxPlanFilePath = "";
            if (scenarioParameters != null)
                paxPlanFilePath = Donnees.getNomDuChemin() + "\\" + "Output" + "\\" + scenarioParameters.Name + "\\" + "PaxPlanTable.txt";

            if (File.Exists(paxPlanFilePath))
            {
                cb_SIM_Regenerate_Data.Enabled = dynamicSimulationUsingPaxPlan();
                //cb_SIM_Regenerate_Data.Checked = dynamicSimulationUsingPaxPlan();
            }
            else
            {
                cb_SIM_Regenerate_Data.Enabled = false;
                cb_SIM_Regenerate_Data.Checked = true;
            }
            // >> Task #8569 Pax2Sim - Scenario Properties - Regenerate PaxPlan
            if (btn_SIM_Start.Enabled)
                btn_SIM_Start.Enabled = IsAbleToSimulate();

        }
        // >> Task #15418 BHS Scenario Assistant
        private bool dynamicSimulationUsingPaxPlan()
        {
            return cb_PP_GeneratePaxPlan.Checked || (cb_BHS_SimulateBHS.Checked && rb_BHS_UsePaxPlan.Checked);
        }
        // << Task #15418 BHS Scenario Assistant

        private void btn_CapacityAnalysis_Click(object sender, EventArgs e)
        {
            if (!tc_General.TabPages.Contains(tp_SIM_Start))
            {
                btn_CapacityAnalysis.Enabled = false;
                return;
            }
            if (tc_General.SelectedTab == tp_SIM_Start)
            {
                MessageBox.Show("Please use the \"Start Simulation\" button to start the simulation", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            tc_General.SelectedTab = tp_SIM_Start;
        }

        private void btn_PeakFlows_Click(object sender, EventArgs e)
        {
            if (!DataIsValid())
            {
                DialogResult = DialogResult.None;
                return;
            }

            if (!isScenarioDateLengthAcceptable())  // >> Task #13366 Error message correction
            {
                DialogResult = DialogResult.None;
                return;
            }
            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update            
            DateTime now = DateTime.Now;
            if (scenarioCreationDate == DateTime.MinValue && scenarioLastUpdateDate == DateTime.MinValue)
            {
                scenarioCreationDate = now;
                scenarioLastUpdateDate = now;
            }
            else
            {
                scenarioLastUpdateDate = now;
            }
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            SourceAnalysis = getAnalysis();

            // >> CheckIn ShowUp default table warning
            if (SourceAnalysis.CI_ShowUpTable != null && SourceAnalysis.CI_ShowUpTable != ""
                && checkInShowUpUsedHasDefaultFormat(SourceAnalysis.CI_ShowUpTable))
            {
                DialogResult dr = MessageBox.Show("You are using a single line CheckIn ShowUp profile." + Environment.NewLine + " Do you want to continue?",
                    "CheckIn ShowUp Profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            // << CheckIn ShowUp default table warning
        }

        // >> Task #13366 Error message correction
        private bool isScenarioDateLengthAcceptable()
        {
            DateTime delayedStartDate = dtp_BeginTime.Value.AddDays(7);
            if (delayedStartDate < dtp_EndTime.Value)
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

        // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues        
        private void btn_Help_Click(object sender, EventArgs e)
        {
            string url = Application.StartupPath + @"\NetHelp\Documents\scenariolauncher.htm";
            string browserPath = GetStandardBrowserPath();
            if (string.IsNullOrEmpty(browserPath))
                MessageBox.Show("No default browser found!");
            else
            {
                try
                {
                    Process.Start(browserPath, url);
                }
                catch (FileNotFoundException fnfEx)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Exception while loading the Help file: Could not find the file at the given path :" + url + ". "
                        + this.GetType().ToString() + " throw an exception: " + fnfEx.Message);
                    return;
                }
                catch (Exception ex)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Exception while loading the Help file: "
                        + this.GetType().ToString() + " throw an exception: " + ex.Message);
                    return;
                }
            }
        }
        private static string GetStandardBrowserPath()
        {
            string browserPath = string.Empty;
            RegistryKey browserKey = null;

            try
            {
                //Read default browser path from Win XP registry key
                browserKey = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                //If browser path wasn't found, try Win Vista (and newer) registry key
                if (browserKey == null)
                {
                    browserKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http", false); ;
                }

                //If browser path was found, clean it
                if (browserKey != null)
                {
                    //Remove quotation marks
                    browserPath = (browserKey.GetValue(null) as string).ToLower().Replace("\"", "");

                    //Cut off optional parameters
                    if (!browserPath.EndsWith("exe"))
                    {
                        browserPath = browserPath.Substring(0, browserPath.LastIndexOf(".exe") + 4);
                    }

                    //Close registry key
                    browserKey.Close();
                }
            }
            catch
            {
                //Return empty string, if no path was found
                return string.Empty;
            }
            //Return default browsers path
            return browserPath;
        }
        // << Task #15556 Pax2Sim - Scenario Properties Assistant issues

        #region 26/03/2012 - SGE - Parking Mulhouse
        private void chk_PP_Parking_CheckedChanged(object sender, EventArgs e)
        {
            rb_PP_ExistingPrkPlan.Enabled = chk_PP_Parking.Checked;
            if (!chk_PP_Parking.Checked)
                rb_PP_ExistingPrkPlan.Checked = true;
            else
                rb_PP_GeneratePRKPLAN.Checked = true;

            rb_PP_GeneratePRKPLAN.Enabled = chk_PP_Parking.Checked;
            lbl_PP_InitialOccupation.Enabled = chk_PP_Parking.Checked;
            cb_PP_InitialOccupation.Enabled = chk_PP_Parking.Checked;

            lbl_PP_DwellingTime.Enabled = chk_PP_Parking.Checked;
            cb_PP_DwellTime.Enabled = chk_PP_Parking.Checked;
            cb_PP_Ex_DwellTime.Enabled = chk_PP_Parking.Checked;

            lbl_PP_DwellTimeVis.Enabled = chk_PP_Parking.Checked;
            cb_PP_DwellTimeVis.Enabled = chk_PP_Parking.Checked;
            cb_PP_Ex_DwellTimeVis.Enabled = chk_PP_Parking.Checked;

            if (!chk_PP_Parking.Checked)
            {
                cb_PP_DwellTime.Text = "";
                cb_PP_DwellTime.Items.Clear();
                cb_PP_Ex_DwellTime.Checked = true;

                cb_PP_DwellTimeVis.Text = "";
                cb_PP_DwellTimeVis.Items.Clear();
                cb_PP_Ex_DwellTimeVis.Checked = true;
            }
            else
            {
                UpdateComboBox(cb_PP_DwellTime, true);
                UpdateComboBox(cb_PP_DwellTimeVis, true);
            }


            if (!chk_PP_Parking.Checked)
            {
                cb_PP_InitialOccupation.Text = "";
                cb_PP_InitialOccupation.Items.Clear();
            }
            else
            {
                UpdateComboBox(cb_PP_InitialOccupation, true);
            }
        }
        #endregion 26/03/2012 - SGE - Parking Mulhouse


        // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)        
        private void useNbOfPaxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useNbOfPaxCheckBox.Checked)
            {
                nbOfPaxComboBox.Enabled = true;
                nbOfPaxLbl.Enabled = true;
                UpdateComboBox(nbOfPaxComboBox, true);
            }
            else
            {
                nbOfPaxComboBox.Text = "";
                nbOfPaxComboBox.Items.Clear();
                nbOfPaxComboBox.Enabled = false;
                nbOfPaxLbl.Enabled = false;
            }
        }

        private void useNbOfBagsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useNbOfBagsCheckBox.Checked)
            {
                nbOfBagsComboBox.Enabled = true;
                nbOfBagsLbl.Enabled = true;
                UpdateComboBox(nbOfBagsComboBox, true);
            }
            else
            {
                nbOfBagsComboBox.Text = "";
                nbOfBagsComboBox.Items.Clear();
                nbOfBagsComboBox.Enabled = false;
                nbOfBagsLbl.Enabled = false;
            }
        }

        private void usaStandardParamLbl_Click(object sender, EventArgs e)
        {

        }

        private void gb_PP_Seed_Enter(object sender, EventArgs e)
        {

        }

        private void cb_ex_PaxOut_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mb_PP_UseDefinedSeed_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)

        // >> Bug #13579 BHS Analysis Assistant        
        private void cb_BHS_CheckIn_MeanFlowTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void resetBeginEndDateUsingMeanFlows()
        {
            if (rb_BHS_UseMeanFlow.Checked)
            {
                if (cb_BHS_Arrival_MeanFlowTable.Text != "" && cb_BHS_CheckIn_MeanFlowTable.Text != "" && cb_BHS_Transfer_MeanFlowTable.Text != "")
                {
                    DateTime minDate = DateTime.MaxValue;
                    DateTime maxDate = DateTime.MinValue;

                    List<string> meanFlowTableNames
                        = new List<string>(new string[] { cb_BHS_Arrival_MeanFlowTable.Text, cb_BHS_CheckIn_MeanFlowTable.Text, cb_BHS_Transfer_MeanFlowTable.Text });
                    getMinMaxDatesFromMeanFlowTables(meanFlowTableNames, out minDate, out maxDate);

                    if (minDate != DateTime.MaxValue && maxDate != DateTime.MinValue)
                    {
                        dtp_BeginTime.MinDate = DateTimePicker.MinimumDateTime;
                        dtp_BeginTime.MaxDate = DateTimePicker.MaximumDateTime;
                        dtp_EndTime.MinDate = DateTimePicker.MinimumDateTime;
                        dtp_EndTime.MaxDate = DateTimePicker.MaximumDateTime;

                        dtp_BeginTime.Value = minDate;
                        dtp_BeginTime.MinDate = minDate.AddDays(-1);    // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason

                        dtp_EndTime.Value = maxDate;
                        dtp_EndTime.MaxDate = maxDate.AddDays(1);       // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason                        
                    }
                }
            }
        }

        private void txt_SIM_WarmUp_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cb_SaveTraceMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_SaveTraceMode.Checked)
            {
                MessageBox.Show("Warning: If the Scenario is not loaded its tables will not be exported when saving the Project.",
                    "Copy output tables", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cb_BHS_Transfer_MeanFlowTable_SelectedIndexChanged(object sender, EventArgs e) // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason
        {
            resetBeginEndDateUsingMeanFlows();
        }
        // << Bug #13579 BHS Analysis Assistant

        // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
        public const string BASIC_FILTER_PROFILE = "Basic";
        public const string ADVANCED_FILTER_PROFILE = "Advanced";
        public const string CUSTOM_FILTER_PROFILE = "Custom";
        private void comboboxResultsProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateShownResultProfiles();
        }

        private void flowTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateShownResultProfiles();
        }

        private void updateShownResultProfiles()    // >> Task #15683 PAX2SIM - Result Filters - Split by Flow Type
        {
            if (comboboxResultsProfile.SelectedItem == null)
            //|| flowTypeComboBox.SelectedItem == null)
            {
                return;
            }

            string selectedProfile = comboboxResultsProfile.SelectedItem.ToString();
            //string selectedFlowTypeVisualName = flowTypeComboBox.SelectedItem.ToString();
            profilesPanel.Controls.Clear();

            List<AnalysisResultFilter> shownResultFilters = new List<AnalysisResultFilter>();
            if (selectedProfile == BASIC_FILTER_PROFILE)
            {
                shownResultFilters = filterListOfResultFiltersByFlowTypeVisualNameList(Donnees.globalBasicAnalysisResultFilterList, flowTypes);
                addResultFiltersToPanel(shownResultFilters, profilesPanel);
            }
            else if (selectedProfile == ADVANCED_FILTER_PROFILE)
            {
                shownResultFilters = filterListOfResultFiltersByFlowTypeVisualNameList(Donnees.globalAdvancedAnalysisResultFilterList, flowTypes);
                addResultFiltersToPanel(shownResultFilters, profilesPanel);
            }
            else if (selectedProfile == CUSTOM_FILTER_PROFILE)
            {
                shownResultFilters = filterListOfResultFiltersByFlowTypeVisualNameList(Donnees.globalCustomAnalysisResultFilterList, flowTypes);
                addResultFiltersToPanel(shownResultFilters, profilesPanel);
            }
            refreshAnalysisResultsFiltersTab(temporaryLinkedResultsFilters);
            activateAllCheckBox.Checked = areAllActivateCheckBoxesChecked();
        }

        private List<AnalysisResultFilter> filterListOfResultFiltersByFlowTypeVisualNameList(List<AnalysisResultFilter> resultFilters, List<string> flowTypes)
        {
            List<AnalysisResultFilter> result = new List<AnalysisResultFilter>();
            if (resultFilters == null)
                return result;

            foreach (AnalysisResultFilter rf in resultFilters)
            {
                bool useResultFilter = false;
                foreach (string flowType in flowTypes)
                {
                    if (flowType == AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME
                        && (rf.belongsToTechnicalFlowType(AnalysisResultFilter.ORIGINATING_TECHNICAL_FLOW_TYPE) || rf.belongsToTechnicalFlowType(AnalysisResultFilter.TRANSFERRING_TECHNICAL_FLOW_TYPE)))
                    {
                        useResultFilter = true;
                        break;
                    }
                    else if (flowType == AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME && rf.belongsToTechnicalFlowType(AnalysisResultFilter.ORIGINATING_TECHNICAL_FLOW_TYPE))
                    {
                        useResultFilter = true;
                        break;
                    }
                    else if (flowType == AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME && rf.belongsToTechnicalFlowType(AnalysisResultFilter.TRANSFERRING_TECHNICAL_FLOW_TYPE))
                    {
                        useResultFilter = true;
                        break;
                    }
                }
                if (useResultFilter)
                    result.Add(rf);
            }
            return result;
        }

        private void addResultFiltersToPanel(List<AnalysisResultFilter> analysisResultFilterList, Panel profilesPanel)
        {
            int groupBoxXLocation = 60;
            int groupBoxYLocation = 0;
            int groupBoxChildLabelYLocation = 9;
            Size groupBoxSize = new Size(groupBoxReference.Width, 25);  //(600, 26);

            for (int i = 0; i < analysisResultFilterList.Count; i++)
            {
                AnalysisResultFilter resultFilter = analysisResultFilterList[i];
                bool isLinkedToScenario = (paGlobalParams != null && resultFilter.hasSameConfigurationAsOneFromList(paGlobalParams.analysisResultsFilters));

                string groupBoxUniqueId = i.ToString();
                addGroupBoxByResultFitler(profilesPanel, resultFilter, groupBoxSize, groupBoxXLocation,
                    groupBoxYLocation, groupBoxChildLabelYLocation, groupBoxUniqueId, null);
                addCheckBoxToControl(profilesPanel, groupBoxUniqueId, activateReferenceLabel.Location.X + 16, groupBoxYLocation + 9,
                    isLinkedToScenario, true, new EventHandler(linkToScenarioCheckBox_Click));
                groupBoxYLocation += groupBoxSize.Height + 4;
            }
        }

        public const string nameLabelTag = "nameLabel";
        public const string fromLabelTag = "fromLabel";
        public const string fromTimeTypeLabelTag = "fromTimeTypeLabel";
        public const string toLabelTag = "toLabel";
        public const string toTimeTypeLabelTag = "toTimeTypeLabel";
        public const string generateCheckBoxTag = "generate";
        public const string withFromSegCheckBoxTag = "withFromSeg";
        public const string withToSegCheckBoxTag = "withToSeg";
        public const string withRecircCheckBoxTag = "withRecirc";
        public const string excludeEbsCheckBoxTag = "excludeEbs";
        public const string generateISTCheckBoxTag = "generateIST";
        public void addGroupBoxByResultFitler(Panel parent, AnalysisResultFilter resultFilter,
            Size groupBoxSize, int groupBoxXLocation, int groupBoxYLocation, int groupBoxChildLabelYLocation,
            string groupBoxTag, EventHandler checkBoxOnClickEventHandler)
        {
            GroupBox gb = new GroupBox();
            gb.Tag = groupBoxTag;
            gb.Text = "";
            gb.Size = groupBoxSize;
            gb.Location = new Point(groupBoxXLocation, groupBoxYLocation);

            bool enableCheckBox = false;

            addLabelToGroupBox(gb, nameLabelTag, resultFilter.filterName, nameLabelReference.Location.X, groupBoxChildLabelYLocation,
                fromLabelReference.Location.X - nameLabelReference.Location.X, 13);
            addLabelToGroupBox(gb, fromLabelTag, resultFilter.fromStationCode, fromLabelReference.Location.X, groupBoxChildLabelYLocation, 100, 13);
            addLabelToGroupBox(gb, fromTimeTypeLabelTag, resultFilter.fromStationTimeType, fromTimeTypeLabelReference.Location.X, groupBoxChildLabelYLocation, 30, 13);
            addLabelToGroupBox(gb, toLabelTag, resultFilter.toStationCode, toLabelReference.Location.X, groupBoxChildLabelYLocation, 100, 13);
            addLabelToGroupBox(gb, toTimeTypeLabelTag, resultFilter.toStationTimeType, toTimeTypeLabelReference.Location.X, groupBoxChildLabelYLocation, 30, 13);

            addCheckBoxToControl(gb, withFromSegCheckBoxTag, fromSegrLabelReference.Location.X + 8, groupBoxChildLabelYLocation, resultFilter.withFromSegregation,
                enableCheckBox, checkBoxOnClickEventHandler);
            addCheckBoxToControl(gb, withToSegCheckBoxTag, toSegrLabelReference.Location.X + 3, groupBoxChildLabelYLocation, resultFilter.withToSegregation,
                enableCheckBox, checkBoxOnClickEventHandler);
            addCheckBoxToControl(gb, withRecircCheckBoxTag, recircLabelReference.Location.X + 27, groupBoxChildLabelYLocation, resultFilter.withRecirculation,
                enableCheckBox, checkBoxOnClickEventHandler);
            addCheckBoxToControl(gb, excludeEbsCheckBoxTag, excludeEBSLabelReference.Location.X + 27, groupBoxChildLabelYLocation, resultFilter.excludeEBSStorageTime,
                enableCheckBox, checkBoxOnClickEventHandler);
            addCheckBoxToControl(gb, generateISTCheckBoxTag, generateISTLabelReference.Location.X + 27, groupBoxChildLabelYLocation, resultFilter.generateIST,
                enableCheckBox, checkBoxOnClickEventHandler);
            parent.Controls.Add(gb);
        }
        public static void addLabelToGroupBox(GroupBox gb, string labelTagPrefix, string labelText, int locationX, int locationY, int width, int height)
        {
            Label label = new Label();
            label.Tag = labelTagPrefix;
            gb.Controls.Add(label);
            label.Text = labelText;
            label.Size = new Size(width, height);   //(100, 13);
            label.Location = new Point(locationX, locationY);
        }
        public static void addCheckBoxToControl(Control control, string tag, int locationX, int locationY, bool isChecked, bool isEnabled,
            EventHandler onClickEventHandler)
        {
            CheckBox checkBox = new CheckBox();
            control.Controls.Add(checkBox);
            checkBox.Text = "";
            checkBox.Size = new Size(15, 14);
            checkBox.Tag = tag;
            checkBox.Checked = isChecked;
            checkBox.Enabled = isEnabled;
            checkBox.Location = new Point(locationX, locationY);
            if (onClickEventHandler != null)
                checkBox.CheckedChanged += onClickEventHandler; //Click += onClickEventHandler;
        }

        public void linkToScenarioCheckBox_Click(object sender, EventArgs e)
        {
            if (sender != null && sender is CheckBox)
            {
                CheckBox currentCheckBox = (CheckBox)sender;
                string correspondingGroupBoxUniqueId = "";
                if (currentCheckBox.Tag != null)
                    correspondingGroupBoxUniqueId = currentCheckBox.Tag.ToString();
                if (correspondingGroupBoxUniqueId == "" || correspondingGroupBoxUniqueId == null || currentCheckBox.Parent == null)
                    return;
                AnalysisResultFilter correspondingResultFilter = getResultFilterByGroupBoxUniqueId(currentCheckBox.Parent, correspondingGroupBoxUniqueId);
                if (correspondingResultFilter != null)
                {
                    if (currentCheckBox.Checked && !correspondingResultFilter.hasSameConfigurationAndNameAsOneFromList(temporaryLinkedResultsFilters))
                        temporaryLinkedResultsFilters.Add(correspondingResultFilter);
                    if (!currentCheckBox.Checked && correspondingResultFilter.hasSameConfigurationAndNameAsOneFromList(temporaryLinkedResultsFilters))
                        correspondingResultFilter.removeFromGivenListByNameAndConfiguration(temporaryLinkedResultsFilters);
                }
                if (!currentCheckBox.Checked)
                    activateAllCheckBox.Checked = false;
                else if (areAllActivateCheckBoxesChecked())
                    activateAllCheckBox.Checked = true;
            }
        }

        public AnalysisResultFilter getResultFilterByGroupBoxUniqueId(Control parent, string groupBoxUniqueId)
        {
            AnalysisResultFilter correspondingResultFilter = null;
            foreach (Control profilesPanelChild in parent.Controls)
            {
                if (profilesPanelChild is GroupBox && ((GroupBox)profilesPanelChild).Tag != null
                    && ((GroupBox)profilesPanelChild).Tag.ToString() == groupBoxUniqueId)
                {
                    GroupBox parentGroupBox = (GroupBox)profilesPanelChild;
                    correspondingResultFilter = getResultFilterByGroupBox(parentGroupBox);
                    break;
                }
            }
            return correspondingResultFilter;
        }

        public static AnalysisResultFilter getResultFilterByGroupBox(GroupBox groupBox)
        {
            string name = "";
            string fromStationCode = "";
            string fromTimeType = "";
            string toStationCode = "";
            string toTimeType = "";
            bool withFromSegr = false;
            bool withToSegr = false;
            bool withRecirc = false;
            bool excludeEBS = false;
            bool generateIST = false;
            foreach (Control child in groupBox.Controls)
            {
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == nameLabelTag)
                    name = ((Label)child).Text;
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == fromLabelTag)
                    fromStationCode = ((Label)child).Text;
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == fromTimeTypeLabelTag)
                    fromTimeType = ((Label)child).Text;
                else if (child is ComboBox && ((ComboBox)child).Tag != null && ((ComboBox)child).Tag.ToString() == fromTimeTypeLabelTag
                    && ((ComboBox)child).SelectedItem != null)
                {
                    fromTimeType = ((ComboBox)child).SelectedItem.ToString();
                }
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == toLabelTag)
                    toStationCode = ((Label)child).Text;
                if (child is Label && ((Label)child).Tag != null && ((Label)child).Tag.ToString() == toTimeTypeLabelTag)
                    toTimeType = ((Label)child).Text;
                else if (child is ComboBox && ((ComboBox)child).Tag != null && ((ComboBox)child).Tag.ToString() == toTimeTypeLabelTag
                    && ((ComboBox)child).SelectedItem != null)
                {
                    toTimeType = ((ComboBox)child).SelectedItem.ToString();
                }
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == withFromSegCheckBoxTag)
                    withFromSegr = ((CheckBox)child).Checked;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == withToSegCheckBoxTag)
                    withToSegr = ((CheckBox)child).Checked;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == withRecircCheckBoxTag)
                    withRecirc = ((CheckBox)child).Checked;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == excludeEbsCheckBoxTag)
                    excludeEBS = ((CheckBox)child).Checked;
                if (child is CheckBox && ((CheckBox)child).Tag != null && ((CheckBox)child).Tag.ToString() == generateISTCheckBoxTag)
                    generateIST = ((CheckBox)child).Checked;
            }
            AnalysisResultFilter correspondingResultFilter = new AnalysisResultFilter(name, fromStationCode, fromTimeType, toStationCode, toTimeType,
                withRecirc, withFromSegr, withToSegr, excludeEBS, generateIST);
            return correspondingResultFilter;
        }

        private CheckBox getCheckBoxFromPanelByGroupBoxUniqueId(Control panel, string groupBoxUniqueId)
        {
            CheckBox checkBox = null;
            if (panel == null || panel.Controls == null)
                return checkBox;
            foreach (Control child in panel.Controls)
            {
                if (child is CheckBox && ((CheckBox)child).Tag != null
                    && ((CheckBox)child).Tag.ToString() == groupBoxUniqueId)
                {
                    return (CheckBox)child;
                }
            }
            return checkBox;
        }

        private void refreshAnalysisResultsFiltersTab(List<AnalysisResultFilter> analysisResultsFilters)
        {
            foreach (Control profilesPanelChild in profilesPanel.Controls)
            {
                if (profilesPanelChild is GroupBox && ((GroupBox)profilesPanelChild).Tag != null)
                {
                    GroupBox groupBox = (GroupBox)profilesPanelChild;
                    AnalysisResultFilter resultFilterOnPanel = getResultFilterByGroupBox(groupBox);
                    CheckBox checkBox = getCheckBoxFromPanelByGroupBoxUniqueId(profilesPanel, groupBox.Tag.ToString());
                    if (checkBox != null)
                        checkBox.Checked = resultFilterOnPanel.hasSameConfigurationAndNameAsOneFromList(analysisResultsFilters);
                }
            }
        }

        private void editCustomProfilesButton_Click(object sender, EventArgs e)
        {

            CustomResultProfileAssistant profileAssistant = new CustomResultProfileAssistant(Donnees, Donnees.globalCustomAnalysisResultFilterList,
                Donnees.globalBasicAnalysisResultFilterList, Donnees.globalAdvancedAnalysisResultFilterList, temporaryLinkedResultsFilters);
            DialogResult drResult = profileAssistant.ShowDialog();
            if (drResult != DialogResult.OK && drResult != DialogResult.Yes)
                return;
            if (drResult == DialogResult.OK)
                comboboxResultsProfile_SelectedIndexChanged(null, null);
        }
        // << Task #15088 Pax2Sim - BHS Analysis - Times and Indexes

        private void cbGeneratePaxPlanForStatic_CheckedChanged(object sender, EventArgs e)  // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#5
        {
            updateStaticPaxPlanTabVisibility();
            UpdateEnabled();
        }

        private void updateStaticPaxPlanTabVisibility()
        {
            if (cb_PP_GeneratePaxPlan.Checked || cb_BHS_SimulateBHS.Checked || cb_Gen_Tms.Checked)
                return;
            List<TabPage> visibleTabPages = new List<TabPage>();
            if (cbGeneratePaxPlanForStatic.Checked)
            {
                visibleTabPages.Add(tp_General);
                visibleTabPages.Add(scenarioInfoTab);
                visibleTabPages.Add(tp_Static);
                visibleTabPages.Add(tp_PaxPlan);
                visibleTabPages.Add(tp_UserAttributes);
            }
            else
            {
                visibleTabPages.Add(tp_General);
                visibleTabPages.Add(scenarioInfoTab);
                if (cb_CalcArrivalPeak.Checked || cb_D_CalcDeparturePeak.Checked)
                {
                    visibleTabPages.Add(tp_Static);
                    visibleTabPages.Add(tp_UserAttributes);
                }
            }
            showOnlyGivenTabs(visibleTabPages);
        }

        private void showOnlyGivenTabs(List<TabPage> visibleTabPages)
        {
            if (visibleTabPages == null)
                return;

            bool[] bVisible = new bool[ttpOrderTabs.Length];
            List<int> visibleTabIndexes = new List<int>();
            foreach (TabPage tp in visibleTabPages)
            {
                int tabIndex = getIndex(ttpOrderTabs, tp);
                if (tabIndex != -1)
                    visibleTabIndexes.Add(tabIndex);
            }
            for (int i = 0; i < bVisible.Length; i++)
                bVisible[i] = visibleTabIndexes.Contains(i);
            addOrRemoveTabPagesByVisibilityList(bVisible);
        }

        private void cbUseSimulationEngineSeed_CheckedChanged(object sender, EventArgs e)
        {
            simulationEngineSeedMaskedTextBox1.Enabled = useSimulationEngineSeedCheckBox.Checked;
            if (!simulationEngineSeedMaskedTextBox1.Enabled)
                simulationEngineSeedMaskedTextBox1.Text = "1";
        }

        private void activateAllCheckBox_Click(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            List<CheckBox> checkBoxes = getActivateCheckBoxes();
            foreach (CheckBox cb in checkBoxes)
                cb.Checked = activateAllCheckBox.Checked;
        }

        private List<CheckBox> getActivateCheckBoxes()
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();
            foreach (Control profilesPanelControl in profilesPanel.Controls)
            {
                if (profilesPanelControl is CheckBox && ((CheckBox)profilesPanelControl).Tag != null)
                {
                    CheckBox cb = (CheckBox)profilesPanelControl;
                    string tag = cb.Tag.ToString();
                    int tagAsInteger = -1;
                    if (Int32.TryParse(tag, out tagAsInteger))
                        checkBoxes.Add(cb);
                }
            }
            return checkBoxes;
        }

        private bool areAllActivateCheckBoxesChecked()
        {
            List<CheckBox> checkBoxes = getActivateCheckBoxes();
            foreach (CheckBox cb in checkBoxes)
                if (!cb.Checked)
                    return false;
            return true;
        }

        private void p_Sim_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simulationEngineSeedMaskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            int inputValue = -1;
            if (!Int32.TryParse(simulationEngineSeedMaskedTextBox1.Text, out inputValue)
                || inputValue == 0)
            {
                ToolTip tooltip = new ToolTip();
                tooltip.ToolTipTitle = "Invalid Data";
                tooltip.Show("The value 0 is not allowed. Please change the value.", simulationEngineSeedMaskedTextBox1, 3000);
                simulationEngineSeedMaskedTextBox1.Text = "1";
            }
        }

        private void mb_PP_UseDefinedSeed_TextChanged(object sender, EventArgs e)
        {
            if (!mb_PP_UseDefinedSeed.Enabled)
                return;
            int inputValue = -1;
            if (!Int32.TryParse(mb_PP_UseDefinedSeed.Text, out inputValue)
                || inputValue == 0)
            {
                ToolTip tooltip = new ToolTip();
                tooltip.ToolTipTitle = "Invalid Data";
                tooltip.Show("The value 0 is not allowed. Please change the value.", mb_PP_UseDefinedSeed, 3000);
                mb_PP_UseDefinedSeed.Text = "1";
            }
        }

        // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec        
        private void firstLevelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormUtils.restrictNumbersWithDecimalsOnTextBox(sender, e);
        }

        private void firstLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictMaxValueOnTextBox(sender, 100);
        }

        private void secondLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictMaxValueOnTextBox(sender, 100);
        }

        private void thirdLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictMaxValueOnTextBox(sender, 100);
        }

        private void secondLevelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormUtils.restrictNumbersWithDecimalsOnTextBox(sender, e);
        }

        private void thirdLevelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormUtils.restrictNumbersWithDecimalsOnTextBox(sender, e);
        }

        private void restrictNumbersOnTextBox(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void restrictMaxValueOnTextBox(object sender, int maxValue)
        {
            if (sender is TextBox)
            {
                string value = ((TextBox)sender).Text;
                double dValue = 0;
                if (Double.TryParse(value, out dValue) && dValue > maxValue)
                    ((TextBox)sender).Text = maxValue.ToString();
            }
        }

        private const string FIRST_PERCENTILE_TAG = "First";
        private const string SECOND_PERCENTILE_TAG = "Second";
        private const string THIRD_PERCENTILE_TAG = "Third";
        private void firstLevelTextBox_Leave(object sender, EventArgs e)
        {
            checkPercentileValues(sender, FIRST_PERCENTILE_TAG);
        }

        private void secondLevelTextBox_Leave(object sender, EventArgs e)
        {
            checkPercentileValues(sender, SECOND_PERCENTILE_TAG);
        }

        private void thirdLevelTextBox_Leave(object sender, EventArgs e)
        {
            checkPercentileValues(sender, THIRD_PERCENTILE_TAG);
        }

        private void checkPercentileValues(object sender, string senderTag)
        {
            double senderPercentileValue = -1;
            if ((sender is TextBox)
                && Double.TryParse(((TextBox)sender).Text, out senderPercentileValue))
            {
                if (!isPercentileDifferentFromOtherPercentiles(senderPercentileValue, senderTag))
                {
                    MessageBox.Show("The percentile values must differ from one another."
                        + Environment.NewLine + "Please modify the " + senderTag + " Percentile.",
                        "Percentiles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ((TextBox)sender).Focus();
                }
            }
        }

        private bool isPercentileDifferentFromOtherPercentiles(double senderPercentileValue, string senderTag)
        {
            double percentileValueA = -1;
            double percentileValueB = -1;

            if (senderTag == FIRST_PERCENTILE_TAG)
            {
                if (Double.TryParse(secondLevelTextBox.Text, out percentileValueA)
                    && Double.TryParse(thirdLevelTextBox.Text, out percentileValueB))
                {
                    return senderPercentileValue != percentileValueA && senderPercentileValue != percentileValueB;
                }
            }
            else if (senderTag == SECOND_PERCENTILE_TAG)
            {
                if (Double.TryParse(firstLevelTextBox.Text, out percentileValueA)
                    && Double.TryParse(thirdLevelTextBox.Text, out percentileValueB))
                {
                    return senderPercentileValue != percentileValueA && senderPercentileValue != percentileValueB;
                }
            }
            else if (senderTag == THIRD_PERCENTILE_TAG)
            {
                if (Double.TryParse(firstLevelTextBox.Text, out percentileValueA)
                    && Double.TryParse(secondLevelTextBox.Text, out percentileValueB))
                {
                    return senderPercentileValue != percentileValueA && senderPercentileValue != percentileValueB;
                }
            }
            return true;
        }
        // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec

        private void flowTypeButton_Click(object sender, EventArgs e)
        {
            string entityType = "Flow Type";
            List<string> entitiesCodes = new List<string>();
            entitiesCodes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
            //entitiesCodes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
            entitiesCodes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
            //entitiesCodes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
            entitiesCodes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);

            MultipleCheckBoxSelector flowsSelector = new MultipleCheckBoxSelector(entityType, entitiesCodes, flowTypes);
            if (flowsSelector.ShowDialog() == DialogResult.OK)
            {
                flowTypes.Clear();
                flowTypes.AddRange(flowsSelector.selectedEntityCodes);
                updateSelectedFlowsLabel(flowTypes);
                //filter the shown result filters by selected flows
                updateShownResultProfiles();
            }
        }

        private void updateSelectedFlowsLabel(List<string> selectedFlows)
        {
            selectedFlowLabel.Text = "Selected Flow Types: ";
            if (selectedFlows.Count == 0)
                selectedFlowLabel.Text += "None.";
            else
            {
                for (int i = 0; i < selectedFlows.Count; i++)
                {
                    if (i < selectedFlows.Count - 1)
                        selectedFlowLabel.Text += selectedFlows[i] + ", ";
                    else
                        selectedFlowLabel.Text += selectedFlows[i] + ".";
                }
            }
        }

        private void useFPParametersTableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useFPParametersTableCheckBox.Checked)
            {
                fpParametersTableComboBox.Enabled = true;
                UpdateComboBox(fpParametersTableComboBox, true);
            }
            else
            {
                fpParametersTableComboBox.Text = "";
                fpParametersTableComboBox.Items.Clear();
                fpParametersTableComboBox.Enabled = false;
            }
        }

        private void rb_PP_Departing_CheckedChanged(object sender, EventArgs e)
        {
            cb_PP_TransferDeterministicDistribution.Enabled = !rb_PP_Departing.Checked && !cb_PP_TransferDistribution.Checked && !cb_PP_FlightCategoryDistr.Checked;
            if (rb_PP_Departing.Checked && cb_PP_TransferDeterministicDistribution.Checked)
                cb_PP_TransferDeterministicDistribution.Checked = false;

            missedTransferShowUpSelectionGroupBox.Enabled = rb_PP_Departing.Checked;
            if (missedTransferShowUpSelectionGroupBox.Enabled)
            {
                if (!missedDepartureTransferBasedOnICTRadioButton.Checked && !missedDepartureTransferBasedOnCIRadioButton.Checked)
                    missedDepartureTransferBasedOnICTRadioButton.Checked = true;
            }
            else
            {
                missedDepartureTransferBasedOnICTRadioButton.Checked = false;
                missedDepartureTransferBasedOnCIRadioButton.Checked = false;
            }
        }

        private void rb_PP_Arriving_CheckedChanged(object sender, EventArgs e)
        {
            updateStateForFillTransferCustomizationGroupBox();
        }

        private void cb_PP_FillTransfer_CheckedChanged(object sender, EventArgs e)
        {
            updateStateForFillTransferCustomizationGroupBox();
        }

        private void updateStateForFillTransferCustomizationGroupBox()
        {
            fillTransferCustomizationGroupBox.Enabled = rb_PP_Arriving.Checked && cb_PP_FillTransfer.Checked;
            if (fillTransferCustomizationGroupBox.Enabled)
            {
                if (!fillBasedOnCheckInRadioButton.Checked && !fillBasedOnICTRadioButton.Checked)
                    fillBasedOnICTRadioButton.Checked = true;
            }
            else
            {
                fillBasedOnCheckInRadioButton.Checked = false;
                fillBasedOnICTRadioButton.Checked = false;
            }
        }

    }
}

