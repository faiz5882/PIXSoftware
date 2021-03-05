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

namespace SIMCORE_TOOL.Prompt
{
    public partial class TraceAnalyser :Form, SIM_LoadingForm
    {
        OverallTools.FormFunctions.ManageUserData mud_InputData;
        GestionDonneesHUB2SIM gdh_Donnees;

        int iOldSelected;

        #region Constructeurs et fonctions pour initialiser la classe
        private void initialize()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            iOldSelected = -1;

            /*foreach (String name in Donnees.getScenarioNames())
            {
                if (PAX2SIM.bTrialVersion && cb_Scenario.Items.Count >= GestionDonneesHUB2SIM.NBScenarios)
                    continue;
                if ((!((Donnees.getScenario(name).BHSSimulation) && epPerimetre_ == PAX2SIM.EnumPerimetre.PAX))
                    || (epPerimetre_ == PAX2SIM.EnumPerimetre.BHS) || (epPerimetre_ == PAX2SIM.EnumPerimetre.TMS))
                    cb_Scenario.Items.Add(name);
            }*/
        }
        public TraceAnalyser(GestionDonneesHUB2SIM Donnees, String sScenarioName)
        {
            initialize();
            gdh_Donnees = Donnees;
            cb_SamplingStep.SelectedIndex = 4;

            #region La Gestion des tables en entrées
            List<String> lsNames = Donnees.getShowTable();
            if (lsNames.Count == 0)
            {
                tabControl1.TabPages.Remove(tp_InputData);
            }
            else
            {
                List<List<String>> llsTables = new List<List<string>>();
                foreach (String sName in lsNames)
                {
                    llsTables.Add(new List<String>(Donnees.getValidTables("Input", sName)));
                }
                mud_InputData = OverallTools.FormFunctions.ManageUserData.InitializeForm(false, p_InputData, new Point(15, 15), lsNames, llsTables);
            }
            #endregion

            #region La gestion des Scénarios sélectionnés

            cb_Scenario.Items.AddRange(Donnees.getScenarioNames().ToArray());
            if ((sScenarioName == "")||(sScenarioName == null))
            {
                if (cb_Scenario.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    cb_Scenario.SelectedIndex = 0;
                }
                else
                {
                    int i = 1;
                    List<String> alList = Donnees.getScenarioNames();


                    while (alList.Contains(Prompt.SIM_Scenarios_Assistant.DefaultScenarioName + i.ToString()))
                    {
                        i++;
                    }
                    cb_Scenario.Text = Prompt.SIM_Scenarios_Assistant.DefaultScenarioName + i.ToString();
                }
            }
            else
            {
                cb_Scenario.Text = sScenarioName;
            }
            #endregion
        }
        #endregion

        #region Buttons Ok/ Cancel/
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if(iOldSelected != -1)
                SaveCurrentSelected(iOldSelected);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Gestion du paramétrage de la trace.
        #region Buttons Load/ Save/ Add/ Delete/
        private void btn_Load_Click(object sender, EventArgs e)
        {
            ofd_Open.Filter = "Trace files (*.ta)|*.ta";
            if (ofd_Open.ShowDialog() != DialogResult.OK)
                return;
            XmlDocument xdProject = new XmlDocument();
            xdProject.Load(ofd_Open.FileName);
            LoadList(xdProject.FirstChild);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (sfd_SaveDialog.ShowDialog() != DialogResult.OK)
                return;
            XmlDocument xdProject = new XmlDocument();
            XmlNode Document = getList(xdProject);
            xdProject.AppendChild(Document);
            xdProject.Save(sfd_SaveDialog.FileName);
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            lst_Content.Items.Add(new SIMCORE_TOOL.OverallTools.TraceTools.TraceContent("Item", "Item", true, false, false));
            lst_Content.SelectedIndex = lst_Content.Items.Count-1;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (lst_Content.SelectedIndex == -1)
                return;
            lst_Content.Items.RemoveAt(lst_Content.SelectedIndex);
            lst_Content.SelectedIndex = -1;
        }
        #endregion

        #region To manage the list
        private void lst_Content_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_Content.SelectedIndex == iOldSelected)
                return;
            if (lst_Content.SelectedIndex == -1)
            {
                gb_Content.Visible = false;
                iOldSelected = -1;
                return;
            }
            if(iOldSelected != -1)
                SaveCurrentSelected(iOldSelected);
            gb_Content.Visible = true;
            SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp = (SIMCORE_TOOL.OverallTools.TraceTools.TraceContent)lst_Content.Items[lst_Content.SelectedIndex];
            txt_DisplayedName.Text = tcTmp.sDisplayedName;
            txt_TraceName.Text = tcTmp.sTraceName;
            cb_Indexed.Checked = tcTmp.bIndexed;
            cb_Queue.Checked = tcTmp.bQueue;
            rbTrackingPoint.Checked= !tcTmp.bWorkingPoint;
            rbWorkingPoint.Checked = tcTmp.bWorkingPoint;
            iOldSelected = lst_Content.SelectedIndex;
        }
        private void SaveCurrentSelected(int iSelected)
        {
            if (lst_Content.Items.Count <= iSelected)
                return;
            if (iSelected == -1)
                return;
            SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp = (SIMCORE_TOOL.OverallTools.TraceTools.TraceContent)lst_Content.Items[iSelected];
            tcTmp.sDisplayedName = txt_DisplayedName.Text;
            tcTmp.sTraceName = txt_TraceName.Text;

            tcTmp.bIndexed = cb_Indexed.Checked;
            tcTmp.bWorkingPoint = !rbTrackingPoint.Checked;
            tcTmp.bQueue = cb_Queue.Checked;
            lst_Content.Items[iSelected] = tcTmp;
        }
        #endregion

        #region To manage the group box shown on the TabPage for defining traces objects
        private void txt_DisplayedName_TextChanged(object sender, EventArgs e)
        {
            gb_Content.Text = txt_DisplayedName.Text;
        }

        private void txt_TraceName_Validating(object sender, CancelEventArgs e)
        {
            if (txt_TraceName.Text.Contains(" "))
            {
                MessageBox.Show("Err10049 : The trace name can't have space in items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }
        #endregion 
        #endregion

        #region The differents functions used to get the informations from the form
        internal List<SIMCORE_TOOL.OverallTools.TraceTools.TraceContent> getList()
        {
            List<SIMCORE_TOOL.OverallTools.TraceTools.TraceContent> ltcResult = new List<SIMCORE_TOOL.OverallTools.TraceTools.TraceContent>();
            foreach (SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp in lst_Content.Items)
            {
                ltcResult.Add(tcTmp);
            }
            return ltcResult;
        }
        internal int getIgnoredColumns()
        {
            return FonctionsType.getInt(nudIgnoredColumns.Value);
        }
        internal void LoadList(XmlNode xnValue)
        {
            lst_Content.SelectedIndex = -1;
            lst_Content.Items.Clear();
            if (xnValue == null)
                return;
            if (xnValue.Name != "ListTraceContent")
                return;
            int iValue = 1;
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xnValue, "IgnoredColumns"))
                Int32.TryParse(xnValue.Attributes["IgnoredColumns"].Value, out iValue);
            nudIgnoredColumns.Value = iValue;
            foreach (XmlNode xnNode in xnValue.ChildNodes)
            {
                SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp = new SIMCORE_TOOL.OverallTools.TraceTools.TraceContent(xnNode);
                lst_Content.Items.Add(tcTmp);
            }
        }
        internal void LoadList(List<OverallTools.TraceTools.TraceContent> ltcTrace, int iIgnoredColumns)
        {
            lst_Content.SelectedIndex = -1;
            lst_Content.Items.Clear();
            if (ltcTrace == null)
                return;
            nudIgnoredColumns.Value = iIgnoredColumns;
            lst_Content.Items.AddRange(ltcTrace.ToArray());
        }
        internal XmlNode getList(XmlDocument xdDocument)
        {
            if (xdDocument == null)
                return null;
            XmlElement result = xdDocument.CreateElement("ListTraceContent");
            result.SetAttribute("IgnoredColumns", getIgnoredColumns().ToString());
            foreach (SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcContent in lst_Content.Items)
            {
                result.AppendChild(tcContent.ToXml(xdDocument));
            }
            return result;
        }
        internal String getTraceName()
        {
            return txt_Trace.Text;
        }
        internal DateTime getDateBegin()
        {
            return dtp_BeginTime.Value;
        }
        internal DateTime getDateEnd()
        {
            return dtp_EndTime.Value;
        }
        internal int getSamplingStep()
        {
            int iResult;
            if (!Int32.TryParse(cb_SamplingStep.Text, out iResult))
                return -1;
            return iResult;
        }
        internal String getScenarioName()
        {
            return cb_Scenario.Text;
        }
        internal TraceAnalysis getAnalysisClass()
        {
            return btaBagAnalysis;
        }
        internal Hashtable getResults()
        {
            return htBagsResults;
        }

        #endregion




        private void btn_SIM_OpenModel_Click(object sender, EventArgs e)
        {
            ofd_Open.Filter = "Custom model (*.exe)|*.exe";
            if (lbl_SIM_ModelName.Text != "")
                ofd_Open.FileName = lbl_SIM_ModelName.Text;
            if (ofd_Open.ShowDialog() != DialogResult.OK)
            {
                lbl_SIM_ModelName.Text = "";
                btn_SIM_Start.Enabled = false;
                return;
            }
            lbl_SIM_ModelName.Text = ofd_Open.FileName;
            //We update the button view.
            tabControl1_SelectedIndexChanged(null, null);
        }

        private void cbTrace_CheckedChanged(object sender, EventArgs e)
        {
            gb_Trace.Enabled = cb_ReadTrace.Checked;
            p_Trace.Enabled = cb_ReadTrace.Checked;
        }

        private void cb_Scenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            SIMCORE_TOOL.Classes.ParamScenario psScenario = gdh_Donnees.GetScenario(cb_Scenario.Text);
            if (psScenario != null)
            {
                SetData(psScenario);
            }
        }
        private void SetData(SIMCORE_TOOL.Classes.ParamScenario psScenario)
        {
            cb_PP_UseDefinedSeed.Checked = false;
            cb_ReadTrace.Checked = false;
            lbl_SIM_ModelName.Text = "";
            mud_InputData.ReinitializeToDefault();
            LoadList((XmlNode)null);
            if (psScenario == null)
            {
                return;
            }
            dtp_BeginTime.Value = psScenario.DateDebut;
            dtp_EndTime.Value = psScenario.DateFin;
            cb_SamplingStep.Text = psScenario.SamplingStep.ToString();
            cb_PP_UseDefinedSeed.Checked = psScenario.UseSeed;
            mb_PP_UseDefinedSeed.Text = psScenario.Seed.ToString();
            cb_ReadTrace.Checked = (psScenario.SimReporterTraceName != "");
            txt_TraceName.Text = psScenario.SimReporterTraceName;
            txt_SIM_WarmUp.Text = psScenario.WarmUp.ToString();
            mud_InputData.setParamUserData(psScenario.SimReporterInputData);
            lbl_SIM_ModelName.Text = psScenario.ModelName;
            cb_SIM_DisplayModel.Checked = psScenario.DisplayModel;
            LoadList(psScenario.SimReporterTraceParameters, psScenario.SimReporterIgnoredColumns);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iOldSelected != -1)
                SaveCurrentSelected(iOldSelected);
           
            if (tabControl1.SelectedTab.Name == tp_Simulation.Name)
            {
                bool bDate = true;
                bool bModel=false;
                bool bSeed = false;
                bool bTrace = false;
                if (dtp_BeginTime.Value > dtp_EndTime.Value)
                {
                    MessageBox.Show("The dates you selected are not valid. Please enter valid dates before launching the analysis.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bDate = false;
                }
                if (bDate)
                {
                    bSeed = ((cb_PP_UseDefinedSeed.Checked) && (mb_PP_UseDefinedSeed.Text.Length > 0)) || (!cb_PP_UseDefinedSeed.Checked);
                    if (!bSeed)
                    {
                        MessageBox.Show("The seed needs to be defined. Please enter valid seed before launching the analysis.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        bTrace = ((cb_ReadTrace.Checked) && (txt_Trace.Text != "") && (lst_Content.Items.Count > 0)) || (!cb_ReadTrace.Checked);
                        List<string> lsPresent = new List<string>();
                        foreach (SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp in lst_Content.Items)
                        {
                            if (lsPresent.Contains(tcTmp.sTraceName))
                            {
                                MessageBox.Show("The Item \"" + tcTmp.sTraceName + "\" is defined 2 times. Please rename it and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                bTrace = false;
                                break;
                            }
                            lsPresent.Add(tcTmp.sTraceName);
                        }
                        if (!bTrace)
                        {
                            MessageBox.Show("The trace needs to be defined. Please define the parameters for the trace reading before launching the analysis.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            bModel = (lbl_SIM_ModelName.Text != "") && (System.IO.File.Exists(lbl_SIM_ModelName.Text));
                            if((!bModel) && (sender==null))
                                MessageBox.Show("The model you selected is not valid. Please select a valid model before launching the analysis.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                    }
                }

                
                btn_SIM_Start.Enabled = bModel && bSeed && bTrace && bDate;
                
            }
        }

        internal Classes.ParamScenario getAnalysis()
        {
            Double dTmp;
            int iTmp;
            Classes.ParamScenario psResult = new Classes.ParamScenario(cb_Scenario.Text);
            psResult.SimAnalyserSimulation = true;
            psResult.DateDebut = dtp_BeginTime.Value;
            psResult.DateFin = dtp_EndTime.Value;
            if (!Double.TryParse(cb_SamplingStep.Text, out dTmp))
                dTmp = 15;
            psResult.SamplingStep = dTmp;
            if (cb_PP_UseDefinedSeed.Checked)
            {
                if (!Int32.TryParse(mb_PP_UseDefinedSeed.Text, out iTmp))
                    iTmp = 0;
                psResult.Seed = iTmp;
                psResult.UseSeed = true;
            }
            if(cb_ReadTrace.Checked)
            {
                psResult.SimReporterTraceName = txt_Trace.Text;
                psResult.SimReporterTraceParameters = getList();
            }
            psResult.SimReporterInputData = mud_InputData.getParamUserData("Input Data");
            psResult.ModelName = lbl_SIM_ModelName.Text;
            psResult.DisplayModel = cb_SIM_DisplayModel.Checked;
            psResult.SimReporterIgnoredColumns = getIgnoredColumns();
            return psResult;
        }

        #region Gestion du dernier Onglet
        bool bSimulationSucceed=false;
        bool bCancel = false;
        SIMCORE_TOOL.Classes.ParamScenario SourceAnalysis;
        private static Process proc;
        Thread tSimulation;
        Hashtable htBagsResults=null;
        Prompt.TraceAnalyser.TraceAnalysis btaBagAnalysis;


        private void btn_SIM_Start_Click(object sender, EventArgs e)
        {

            SourceAnalysis =getAnalysis();

            
            if (SourceAnalysis.ModelName.Length == 0)
                return;
            bSimulationSucceed = false;
            htBagsResults = null;
            #region Mise à jour de l'affichage.
            p_InputData.Enabled = false;
            p_Trace.Enabled = false;
            p_Main.Enabled = false;
            btn_ok.Enabled = false;
            btn_Cancel.Enabled = false;
            this.ControlBox = false;

            btn_SIM_CancelSimulation.Enabled = true;

            lbl_SIM_Current.Text = "Export data for simulation";
            btn_SIM_Start.Enabled = false;
            gb_SIM_Model.Enabled = false;

            pb_SIM_TotalOperations.Value = 0;
            bCancel = false;
            cb_Scenario.Enabled = false;
            #endregion

            #region Les parametres pour la simulation.

            SourceAnalysis.HasBeenSimulated = true;
            #endregion

            if (!gdh_Donnees.ScenarioExist(SourceAnalysis.Name))
            {
                TreeNode tnTmp = gdh_Donnees.AddScenario(SourceAnalysis.Name,null, null, null, null, null, SourceAnalysis, null);
                if (tnTmp == null)
                {
                    return;
                }
            }
            else
            {
                gdh_Donnees.ReplaceScenario(SourceAnalysis.Name, SourceAnalysis);
            }
            SourceAnalysis.HasBeenSimulated = true;
            if (PAX2SIM.bDebug)
            {
                SimulationLaunched();
                return;
            }
            tSimulation = new Thread(new ThreadStart(SimulationLaunched));
            tSimulation.Start();
        }
        delegate void SaveResultsCallBack(Hashtable htResults);
        private void SaveResults(Hashtable htResults)
        {
            if (this.InvokeRequired)
            {
                SaveResultsCallBack d = new SaveResultsCallBack(SaveResults);
                this.Invoke(d, new object[] { htResults });
            }
            else
            {
                htBagsResults = htResults;
            }
        }
        private void btn_SIM_CancelSimulation_Click(object sender, EventArgs e)
        {
            btn_SIM_CancelSimulation.Enabled = false;
            bCancel = true;
            proc.Kill();
            tSimulation.Abort();
            EndOfSimulation(null);
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
                if (sMessage != null)
                {
                    if (!bSimulationSucceed)
                        MessageBox.Show(sMessage, "Simulation ended", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(sMessage, "Simulation ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //pb_SIM_Current.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
                btn_ok.Enabled = true;
                p_Main.Enabled = true;
                p_InputData.Enabled = true;
                gb_SIM_Model.Enabled = true;
                p_Trace.Enabled = cb_ReadTrace.Checked;


                btn_SIM_Start.Enabled = true;
                lbl_SIM_Current.Text = "";
                btn_SIM_CancelSimulation.Enabled = false;
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

        #region Partie pour la simulation


        #region La partie qui lance la simulation
        private void SimulationLaunched()
        {
            #region Le pas utilisé pour faire avancé la barre de progression.
            int iPas = 33;
            #endregion

            bool bDisplayGraphic = SourceAnalysis.DisplayModel;
            String Directory = "";
            String sNomModel = SourceAnalysis.ModelName;
            String sMessageFile = "";
            string sMFFFile = "";
            String sDataDirectory = "";

            Directory = sNomModel.Substring(0, sNomModel.LastIndexOf('\\')) + "\\";
            if (Directory.Length == 0)
            {
                DisplayErreur("Some errors happened during the access of the model.");
                EndOfSimulation("Unable to locate the model.");
                return;
            }
            sDataDirectory = Directory + "Data\\";
            sMessageFile = sNomModel.Remove(sNomModel.Length - 3) + "message";
            sMFFFile = sNomModel.Remove(sNomModel.Length - 3) + "mff";
            if (!OverallTools.ExternFunctions.CheckCreateDirectory(sDataDirectory))
            {
                DisplayErreur("Some errors happened during the access of the model.");
                EndOfSimulation("Unable to create the directories for the simulation files.");
                return;
            }
            if (SourceAnalysis.UseSeed)
            {
                OverallTools.AutomodFiles.GenerateMFF(sMFFFile, SourceAnalysis.Seed);
            }
            this.ChargementFichier("");
            bool bGenerate = false;
            String sError = "";
            try
            {
                List<DataTable> automodTablesList = new List<DataTable>();  // >> Task #13361 FP AutoMod Data tables V3
                bGenerate = gdh_Donnees.SaveSimulationFiles(SourceAnalysis.Name, Directory, false, false, this, out automodTablesList);
            }
            catch (Exception Excep)
            {
                bGenerate = false;
                sError = "Err00329 : An exception appeared during the generating of simulation data files : " + Excep.Message;
                OverallTools.ExternFunctions.PrintLogFile("Err00329: " + this.GetType().ToString() + " throw an exception during the generating of simulation data files: " + Excep.Message);
            }
            try
            {
                if (System.IO.File.Exists(sMessageFile))
                    System.IO.File.Delete(sMessageFile);
            }
            catch (Exception exc) 
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02070: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
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
            SetValue(iPas, "Simulation");
            if (bCancel)
            {
                EndOfSimulation(null);
                return;
            }
            //Lancement de la simulation.
            if (!SourceAnalysis.DisplayModel)
            {
                proc = Process.Start("amrun", sNomModel + " -Mwn");
            }
            else
            {
                proc = Process.Start("amrun", sNomModel);
            }
            proc.WaitForExit();
            if (bCancel)
            {
                EndOfSimulation(null);
                return;
            }
            if (!System.IO.File.Exists(sMessageFile))
            {
                EndOfSimulation("The model had a problem. Unable to open Automod model.Please check the compatibility of the Automod model with the installed version of Automod.");
                return;
            }
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
                if ((bError || (!bCannotContinue))
                    && (!Message("The model run did not finish properly. The user may have stopped the simulation before the end. Do you want to analyze partial results?")))    // >> Task #13366 Error message correction
                {
                    EndOfSimulation(null);
                    return;
                }
            }
            catch (Exception exc)
            {
                EndOfSimulation("The model had a problem. Unable to open Automod model.");
                OverallTools.ExternFunctions.PrintLogFile("Except02071: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                return;
            }

            // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            double percentile1 = -1;
            double percentile2 = -1;
            double percentile3 = -1;
            if (SourceAnalysis.percentilesLevels == null || SourceAnalysis.percentilesLevels.Count == 0)
            {
                percentile1 = gdh_Donnees.Levels[0];
                percentile2 = gdh_Donnees.Levels[0];
                percentile3 = gdh_Donnees.Levels[0];
            }
            else if (SourceAnalysis.percentilesLevels.Count == 3)
            {
                percentile1 = SourceAnalysis.percentilesLevels[0];
                percentile2 = SourceAnalysis.percentilesLevels[1];
                percentile3 = SourceAnalysis.percentilesLevels[2];
            }
            // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec

            SetValue(iPas, "Analyze baggage results");
            btaBagAnalysis = new TraceAnalysis(SourceAnalysis.Name, Directory + SourceAnalysis.SimReporterTraceName,Directory,SourceAnalysis.SimReporterTraceParameters,SourceAnalysis.SimReporterIgnoredColumns, SourceAnalysis.WarmUp * 60, percentile1, percentile2, percentile3);
            Double dEndingTime = OverallTools.DataFunctions.MinuteDifference(SourceAnalysis.DateDebut, SourceAnalysis.DateFin);
            if (SourceAnalysis.SimReporterTraceName != "")
            {
                if (!btaBagAnalysis.OpenTrace(dEndingTime, SourceAnalysis.SaveTraceMode, this))
                {
                    EndOfSimulation("The trace has a wrong format. Unable to analyze it.");
                    return;
                }
            }
            Hashtable htRes = btaBagAnalysis.GenerateResults(SourceAnalysis.DateDebut, SourceAnalysis.DateFin,
                SourceAnalysis.SamplingStep, 1, 31, this, SourceAnalysis.AnalysisRange);    // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
            ChargementFichier("");
            SaveResults(htRes);
            

            ChargementFichier(" ");
            SetValue(iPas, "");
            SimulationSucceed();
            EndOfSimulation("Simulation ended with success.");
        }
        #endregion
        #endregion
        #endregion

        #region Classe pour analyser une trace associée.

        public class TraceAnalysis
        {
            /// <summary>
            /// The list of the different items that can be found in the trace.
            /// </summary>
            List<SIMCORE_TOOL.OverallTools.TraceTools.TraceContent> ltcTracePoints;

            private SIMCORE_TOOL.OverallTools.TraceTools.TraceContent getTrace(String sTraceName)
            {

                foreach (SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp in ltcTracePoints)
                    if (tcTmp.sTraceName == sTraceName)
                    {
                        return tcTmp;
                    }
                return null;
            }

            internal SIMCORE_TOOL.OverallTools.TraceTools.TraceContent isValidDesk(String sName)
            {
                if (sName.Length == 0)
                    return null;
                String[] tsObject = sName.Split('_');
                if (tsObject.Length < 1)
                    return null;
                SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcPoint = getTrace(tsObject[0]);
                if (tcPoint == null)
                    return null;
                if (!tcPoint.bIndexed)
                    return tcPoint;
                int iTmp;
                if (!Int32.TryParse(tsObject[tsObject.Length - 1], out iTmp))
                {
                    return null;
                }
                return tcPoint;
            }

            internal Hashtable Results;
            /// <summary>
            /// These columns are the one in the begining of the file that represents data
            /// that will be ignored in a first place (ID, times ...)
            /// </summary>
            int iIgnoredColumns;

            public static String StoppedKeyWord = "STOPPED";

            #region Fonction statique qui génère l'arborescence visualisée dans le TreeView.

            public void GenerateDirectories(TreeNode tnRacine, ContextMenuStrip cmsDirectorysMenu)
            {
                if (tnRacine == null)
                    return;
                if (ltcTracePoints == null)
                    return;
                foreach (SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp in ltcTracePoints)
                {
                    TreeNode tnTmp = OverallTools.TreeViewFunctions.CreateDirectory(tcTmp.sTraceName, tcTmp.sDisplayedName, cmsDirectorysMenu);
                    tnRacine.Nodes.Add(tnTmp);
                
                }
            }
            #endregion

            #region Les constructeurs de la classe.
            /// <summary>
            /// Constructeur de la classe qui s'occupe d'analyser le contenu du bagtrace.
            /// </summary>
            /// <param name="FileName">Le nom du fichier bagtrace à analyser (avec son chemin complet)</param>
            /// <param name="WarmUp">Le Warmup appliqué sur cette simulation (en minutes)</param>
            internal TraceAnalysis(String ScenarioName,
                                    String FileName,
                                    String OutputDirectory,
                                    List<SIMCORE_TOOL.OverallTools.TraceTools.TraceContent> TracePoints,
                                    int IgnoredColumns,
                                    double WarmUp,
                                    Double FirstLevelPercent,
                                    Double SecondLevelPercent,
                                    Double ThirdLevelPercent)
            {
                sScenarioName = ScenarioName;
                sFileName = FileName;
                sOutputDirectory = OutputDirectory;
                ltcTracePoints = TracePoints;
                iIgnoredColumns = IgnoredColumns;
                dWarmUp = WarmUp;

                dFirstLevelPercent = FirstLevelPercent;
                dSecondLevelPercent = SecondLevelPercent;
                dThirdLevelPercent = ThirdLevelPercent;

                Initialize();
            }
            public void Initialize()
            {
                alErrorList = new ArrayList();
                htStation = new Hashtable();
                htTransferring = new Hashtable();
                Results = null;
            }
            #endregion

            #region Les différentes variables de la classe.

            private String sScenarioName;
            /// <summary>
            /// Le nom du fichier bagtrace.
            /// </summary>
            private String sFileName;

            /// <summary>
            /// The location for the outputDirectory
            /// </summary>
            private string sOutputDirectory;

            /// <summary>
            /// Le Warm up utilisée pour la simulation (à appliquer sur les résultats).
            /// </summary>
            private Double dWarmUp;

            /// <summary>
            /// La liste des erreurs qui ont pu apparaitre durant l'analyse du BagTrace
            /// </summary>
            ArrayList alErrorList;

            /// <summary>
            /// Hastable contenant les résultats de la simulation pour chaque station
            /// contient des StationResult.
            /// </summary>
            Hashtable htStation;

            /// <summary>
            /// Hastable contenant les résultats de la simulation pour chaque bagage en arrivée
            /// contient des TransferringResult.
            /// </summary>
            Hashtable htTransferring;


            private Double dFirstLevelPercent;
            private Double dSecondLevelPercent;
            private Double dThirdLevelPercent;

            #endregion

            #region Classes utilisées pour les résultats présents dans le bagTrace

            #region BaggageDesk
            private class BaggageDesk
            {
                private String sName;
                private Double dEnter;
                private Double dLeave;
                private Double dTravelTime;
                private bool bUniqueTime;

                #region Accesseurs
                public String DeskName
                {
                    get
                    {
                        return sName;
                    }
                }
                public Double ArrivingTime
                {
                    get
                    {
                        return dEnter;
                    }
                }
                public Double LeavingTime
                {
                    get
                    {
                        return dLeave;
                    }
                    set
                    {
                        if (!bUniqueTime)
                            dLeave = value;
                    }
                }
                public String DeskType
                {
                    get
                    {
                        if (sName.Length == 0)
                            return "";
                        return (sName.Split('_'))[0];
                    }
                }
                public Double TotalTime
                {
                    get
                    {
                        return (dLeave - dEnter);
                    }
                }
                public Double TravelTime
                {
                    get
                    {
                        return dTravelTime;
                    }
                    set
                    {
                        dTravelTime = value;
                    }
                }
                public bool UniqueTime
                {
                    get
                    {
                        return bUniqueTime;
                    }
                }
                #endregion

                #region Constructeurs
                public static BaggageDesk getBaggageDesk(String Name, String Enter, String Leave)
                {
                    if ((Name == null) || (Enter == null) || (Leave == null))
                        return null;
                    if ((Name.Length == 0) || (Enter.Length == 0) || (Leave.Length == 0))
                        return null;
                    Double dEnter_, dLeave_;
                    if (!Double.TryParse(Enter, out dEnter_))
                        return null;
                    if (!Double.TryParse(Leave, out dLeave_))
                        return null;
                    return new BaggageDesk(Name, dEnter_, dLeave_);
                }
                public static BaggageDesk getBaggageDesk(String Name, String Enter, Double Leave)
                {
                    if ((Name == null) || (Enter == null))
                        return null;
                    if ((Name.Length == 0) || (Enter.Length == 0))
                        return null;
                    Double dEnter_;
                    if (!Double.TryParse(Enter, out dEnter_))
                        return null;
                    return new BaggageDesk(Name, dEnter_, Leave);
                }
                private BaggageDesk(String Name, Double Enter, Double Leave)
                {
                    sName = Name;
                    dEnter = Enter;
                    dLeave = Leave;
                    if (dLeave == -1)
                    {
                        bUniqueTime = true;
                        dLeave = dEnter;
                    }
                }
                #endregion

            }
            #endregion

            #region BaggageResult
            private class BaggageResult
            {
                protected String[] tsInformations_;
                protected bool bStopped;
                protected ArrayList alBaggageWay;
                protected String sLine;

                #region Accesseurs

                public String OldLine
                {
                    get
                    {
                        return sLine;
                    }
                    set
                    {
                        sLine = value;
                    }
                }
                public bool hasLine
                {
                    get
                    {
                        return sLine.Length > 0;
                    }
                }
                public Double ArrivingTime
                {
                    get
                    {
                        return ((BaggageDesk)alBaggageWay[0]).ArrivingTime;
                    }
                }

                protected BaggageDesk GetResults(int iIndex)
                {
                    if (iIndex < 0)
                        return null;
                    if (iIndex >= alBaggageWay.Count)
                        return null;
                    return (BaggageDesk)alBaggageWay[iIndex];
                }

                public virtual Double LeavingTime
                {
                    get
                    {
                        if ((alBaggageWay == null) || (alBaggageWay.Count == 0))
                            return 0;
                        return ((BaggageDesk)alBaggageWay[alBaggageWay.Count - 1]).LeavingTime;
                    }
                }
                public virtual Double TotalTime
                {
                    get
                    {
                        if ((alBaggageWay == null) || (alBaggageWay.Count == 0))
                            return 0;
                        Double dArriving = ArrivingTime;
                        Double dLeaving = LeavingTime;
                        return dLeaving - dArriving;
                    }
                }
                public virtual Double TotalSimulationTime
                {
                    get
                    {
                        return TotalTime;
                    }
                }

                public int NumberStep
                {
                    get
                    {
                        if (alBaggageWay == null)
                            return 0;
                        return alBaggageWay.Count;
                    }
                }
                public bool Stopped
                {
                    get
                    {
                        return bStopped;
                    }
                    set
                    {
                        bStopped = value;
                    }
                }
                #endregion

                #region Constructeur
                /// <summary>
                /// Constructeur de la classe de résultat d'un bagage.
                /// </summary>
                /// <param name="IDBag">Identificant du bagage</param>
                /// <param name="IDFlight">Identifiant du vol.</param>
                /// <param name="STD">Heure de départ</param>
                /// <param name="Recirculation">Nombre de recirculation</param>
                /// <param name="InterLink">Information d'interlink</param>
                public BaggageResult(String []tsInformations)
                {
                    tsInformations_ = tsInformations;
                    alBaggageWay = new ArrayList();
                    bStopped = false;
                }
                #endregion

                #region public override void AddBaggageWay(BaggageDesk bdDesk)
                /// <summary>
                /// Fonction qui ajoute à la statistique du bagage les postes pas lesquels il est passé.
                /// </summary>
                /// <param name="bdDesk">L'information sur le poste.</param>
                public virtual void AddBaggageWay(BaggageDesk bdDesk)
                {
                    if ((alBaggageWay.Count > 0) &&
                        (((BaggageDesk)alBaggageWay[alBaggageWay.Count - 1]).DeskName == bdDesk.DeskName))
                    {
                        ((BaggageDesk)alBaggageWay[alBaggageWay.Count - 1]).LeavingTime = bdDesk.LeavingTime;
                    }
                    else
                    {
                        alBaggageWay.Add(bdDesk);
                    }
                }
                #endregion

            }

            class DepartingResult : BaggageResult
            {
                /// <summary>
                /// Index du point d'entree (CI ou TrIF)
                /// </summary>
                protected int iInfeedIndex;
                /// <summary>
                /// Index du point de sortie (MU)
                /// </summary>
                protected int iMUIndex;

                protected int iBuffer;
                /// <summary>
                /// Some simulation can send several times the same bagage in the EBS area.
                /// </summary>
                protected List<Int32> liEBSIndex;
                protected int iHBS1;
                protected int iHBS2;
                protected int iHBS3;
                protected int iHBS4;
                protected int iHBS5;
                protected int iMES;
                protected int iChute;
                protected int iInterlink;
                protected int iPBC;

                #region Accesseurs

                public bool HasInfeed
                {
                    get
                    {
                        return (iInfeedIndex != -1);
                    }
                }
                public bool HasHBS1
                {
                    get
                    {
                        return (iHBS1 != -1);
                    }
                }
                public bool HasHBS2
                {
                    get
                    {
                        return (iHBS2 != -1);
                    }
                }
                public bool HasHBS3
                {
                    get
                    {
                        return (iHBS3 != -1);
                    }
                }
                public bool HasHBS4
                {
                    get
                    {
                        return (iHBS4 != -1);
                    }
                }
                public bool HasHBS5
                {
                    get
                    {
                        return (iHBS5 != -1);
                    }
                }

                public bool HasMES
                {
                    get
                    {
                        return (iMES != -1);
                    }
                }
                public bool HasEBS
                {
                    get
                    {
                        return (liEBSIndex.Count != 0);
                    }
                }
                public bool HasInterLink
                {
                    get
                    {
                        return (iInterlink != -1);
                    }
                }
                public bool HasPBC
                {
                    get { return iPBC != -1; }
                }


                public Double EBSTime
                {
                    get
                    {
                        if (!HasEBS)
                            return 0;
                        Double dResult = 0;
                        foreach (int iEBSIndex in liEBSIndex)
                            dResult += GetResults(iEBSIndex).TotalTime;
                        return dResult;
                    }
                }

                public Double ArrivingInfeedTime
                {
                    get
                    {
                        if (iInfeedIndex != -1)
                            return GetResults(iInfeedIndex).LeavingTime;
                        return GetResults(0).ArrivingTime;
                    }
                }
                /// <summary>
                /// From the queue till the end without the ebs time.
                /// </summary>
                public override Double TotalTime
                {
                    get
                    {
                        return base.TotalTime - EBSTime;
                    }
                }
                /// <summary>
                /// From the infeed till the end without the ebs time.
                /// </summary>
                public override Double TotalSimulationTime
                {
                    get
                    {
                        if ((iInfeedIndex == -1) || ((iMUIndex == -1) && (!HasHBS5) && (!HasPBC)))
                            return 0;
                        //Double EBSTime = 0;

                        //if (HasEBS){
                        //    EBSTime = GetResults(iEBSIndex).TotalTime;
                        int iIndexSortie = iMUIndex;
                        if (HasHBS5)
                            iIndexSortie = iHBS5;
                        if (HasPBC)
                            iIndexSortie = iPBC;
                        return GetResults(iIndexSortie).LeavingTime - GetResults(iInfeedIndex).LeavingTime - EBSTime;
                    }
                }
                #endregion

                #region Constructeur
                public DepartingResult(String[] tsInformations)
                    : base(tsInformations)
                {
                    iMUIndex = -1;
                    iInfeedIndex = -1;
                    liEBSIndex = new List<int>();
                    iHBS1 = -1;
                    iHBS2 = -1;
                    iHBS3 = -1;
                    iHBS4 = -1;
                    iHBS5 = -1;
                    iMES = -1;
                    iChute = -1;
                    iInterlink = -1;
                    iPBC = -1;
                    iBuffer = -1;
                }
                #endregion

                #region public override void AddBaggageWay(BaggageDesk bdDesk)
                public override void AddBaggageWay(BaggageDesk bdDesk)
                {
                    base.AddBaggageWay(bdDesk);
                }
                #endregion

                public Double RemainingTime
                {
                    get
                    {
                            return -1;
                    }
                }

            }

            class TransferringResult : DepartingResult
            {

                #region Constructeurs
                public TransferringResult(String []tsInformations)
                    : base(tsInformations)
                {
                }
                #endregion

                #region public override void AddBaggageWay(BaggageDesk bdDesk)
                public override void AddBaggageWay(BaggageDesk bdDesk)
                {
                    base.AddBaggageWay(bdDesk);
                }
                #endregion
            }
            #endregion

            #region StationResult
            private class StationResult
            {
                private String sStationName;
                private String sStationType;
                private int iIndexStation;
                /// <summary>
                /// Booléen indiquand si le poste courant à une valeur de process ou non.
                /// </summary>
                private bool bTimeNull;

                private Double dMinThroughput;
                private Double dAverageThroughput;
                private Double dMaxThroughput;

                private Double dMinAverage;
                private Double dAverageAverage;
                private Double dMaxAverage;

                private Double dMinMax;
                private Double dAverageMax;
                private Double dMaxMax;

                private String sUnit;
                private Double dFirstLevel;
                private Double dSecondLevel;
                private Double dThirdLevel;

                private Double dFirstLevelPercent;
                private Double dSecondLevelPercent;
                private Double dThirdLevelPercent;

                private ArrayList alStationUse;
                private SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcLinkedDesk;

                public class Result
                {
                    private Double dArrivingTime;
                    private Double dLeavingTime;
                    private Double dTime;
                    private Double dTravelTime;
                    private bool bStopped;
                    public Result(Double ArrivingTime, Double LeavingTime)
                    {
                        dArrivingTime = ArrivingTime;
                        dLeavingTime = LeavingTime;
                        dTime = dLeavingTime - dArrivingTime;
                        dTravelTime = 0;
                        bStopped = false;
                    }
                    public Double OccupationTime
                    {
                        get
                        {
                            return dTime;
                        }
                    }
                    public Double ArrivingTime
                    {
                        get
                        {
                            return dArrivingTime;
                        }
                    }
                    public Double LeavingTime
                    {
                        get
                        {
                            return dLeavingTime;
                        }
                        set
                        {
                            dLeavingTime = value;
                            dTime = dLeavingTime - dArrivingTime;
                        }
                    }
                    public Double TravelTime
                    {
                        get
                        {
                            return dTravelTime;
                        }
                        set
                        {
                            dTravelTime = value;
                        }
                    }
                    public bool Stopped
                    {
                        get
                        {
                            return bStopped;
                        }
                        set
                        {
                            bStopped = value;
                        }
                    }
                }

                #region Accesseurs
                internal SIMCORE_TOOL.OverallTools.TraceTools.TraceContent TypeDesk
                {
                    get
                    {
                        return tcLinkedDesk;
                    }
                }
                public String StationType
                {
                    get
                    {
                        if (sStationType != "")
                            return sStationType;
                        if (sStationName.Length == 0)
                            return "";
                        String[] tsObject = sStationName.Split('_');
                        if(tcLinkedDesk.bIndexed)
                            if (!Int32.TryParse(tsObject[tsObject.Length - 1], out iIndexStation))
                            {
                                return "";
                            }
                        sStationType = tsObject[0];
                        return sStationType;
                    }
                }
                public String StationName
                {
                    get
                    {
                        return sStationName;
                    }
                }
                public int StationIndex
                {
                    get
                    {
                        if (StationType == "")
                            return -1;
                        return iIndexStation;
                    }
                }
                public bool TimeNull
                {
                    get
                    {
                        return bTimeNull;
                    }
                    set
                    {
                        bTimeNull = value;
                    }
                }
                #endregion

                #region Constructeur
                public StationResult(String StationName,
                                     SIMCORE_TOOL.OverallTools.TraceTools.TraceContent LinkedDesk,
                                     Double FirstLevelPercent,
                                     Double SecondLevelPercent,
                                     Double ThirdLevelPercent)
                {
                    sStationName = StationName;
                    alStationUse = new ArrayList();
                    tcLinkedDesk = LinkedDesk;
                    sStationType = "";
                    iIndexStation = -1;
                    dMinThroughput = -1;
                    dAverageThroughput = -1;
                    dMaxThroughput = -1;

                    dMinAverage = -1;
                    dAverageAverage = -1;
                    dMaxAverage = -1;

                    dMinMax = -1;
                    dAverageMax = -1;
                    dMaxMax = -1;

                    sUnit = "";
                    dFirstLevel = -1;
                    dSecondLevel = -1;
                    dThirdLevel = -1;

                    dFirstLevelPercent = FirstLevelPercent;
                    dSecondLevelPercent = SecondLevelPercent;
                    dThirdLevelPercent = ThirdLevelPercent;

                    bTimeNull = false;
                }
                #endregion

                #region AddStationResult -- functions
                public void AddStationResult(Result result)
                {
                    alStationUse.Add(result);
                }
                public void AddStationResult(Double ArrivingTime, Double LeavingTime)
                {
                    alStationUse.Add(new Result(ArrivingTime, LeavingTime));
                }
                #endregion

                public void AddRangeInformation(StationResult srInformation)
                {
                    foreach (Result rValue in srInformation.alStationUse)
                    {
                        AddStationResult(rValue);
                    }
                }

                public DataTable getStatistics(Double dWarmUp)
                {
                    DataTable dtResults = new DataTable(sStationName + "_Statistics");
                    dtResults.Columns.Add("Type", typeof(String));
                    dtResults.Columns.Add("Value", typeof(Double));
                    dtResults.Columns.Add("Unit", typeof(String));
                    Double[] tdResults = new double[9];
                    //Number of bags
                    //Stopped bags
                    //Stopped bags after desk.
                    //Min
                    //Average
                    //Max
                    //Travel min
                    //Travel Average
                    //Travel max
                    tdResults[0] = 0;
                    tdResults[1] = 0;
                    tdResults[2] = 0;
                    tdResults[3] = -1;
                    tdResults[4] = 0;
                    tdResults[5] = 0;
                    tdResults[6] = -1;
                    tdResults[7] = 0;
                    tdResults[8] = 0;
                    ArrayList alTravelTimes = new ArrayList();
                    for (int i = 0; i < alStationUse.Count; i++)
                    {
                        Result rResult = (Result)alStationUse[i];
                        if (rResult.ArrivingTime < dWarmUp)
                            continue;
                        if (rResult.Stopped)
                        {
                            tdResults[1]++;
                            continue;
                        }
                        tdResults[0]++;
                        Double dTime = rResult.OccupationTime;
                        if ((dTime < tdResults[3]) || (tdResults[3] == -1))
                            tdResults[3] = dTime;
                        if (dTime > tdResults[5])
                            tdResults[5] = dTime;
                        tdResults[4] += dTime;
                        if (rResult.TravelTime == -1)
                        {
                            tdResults[2]++;
                            continue;
                        }
                        dTime = rResult.TravelTime;
                        alTravelTimes.Add(dTime);
                        if ((dTime < tdResults[6]) || (tdResults[6] == -1))
                            tdResults[6] = dTime;
                        if (dTime > tdResults[8])
                            tdResults[8] = dTime;
                        tdResults[7] += dTime;
                    }
                    if (tdResults[0] != 0)
                    {
                        tdResults[4] = tdResults[4] / tdResults[0];
                        if (tdResults[8] > 0)
                            tdResults[7] = tdResults[7] / (tdResults[0] - tdResults[2]);

                    }
                    else
                    {
                        tdResults[3] = 0;
                        tdResults[6] = 0;
                    }

                    dtResults.Rows.Add(new Object[] { "Total number of bags", tdResults[0], "Bags" });
                    dtResults.Rows.Add(new Object[] { "Total number of stopped bags", Math.Round(tdResults[1], 2), "Bags" });
                    dtResults.Rows.Add(new Object[] { "Total number of stopped bags after the desk", Math.Round(tdResults[2], 2), "Bags" });
                    if (!TimeNull)
                    {
                        dtResults.Rows.Add(new Object[] { "Minimum processing time", Math.Round(tdResults[3], 2), "Minutes" });
                        dtResults.Rows.Add(new Object[] { "Average processing time", Math.Round(tdResults[4], 2), "Minutes" });
                        dtResults.Rows.Add(new Object[] { "Maximum processing time", Math.Round(tdResults[5], 2), "Minutes" });
                    }

                    if (dFirstLevel != -1)
                    {
                        dtResults.Rows.Add(new Object[] { "Processing time " + dFirstLevelPercent + " %", Math.Round(dFirstLevel, 2), sUnit });
                        dtResults.Rows.Add(new Object[] { "Processing time " + dSecondLevelPercent + " %", Math.Round(dSecondLevel, 2), sUnit });
                        dtResults.Rows.Add(new Object[] { "Processing time " + dThirdLevelPercent + " %", Math.Round(dThirdLevel, 2), sUnit });
                    }
                    if (tdResults[8] > 0)
                    {
                        dtResults.Rows.Add(new Object[] { "Minimum travel time", Math.Round(tdResults[6], 2), "Minutes" });
                        dtResults.Rows.Add(new Object[] { "Average travel time", Math.Round(tdResults[7], 2), "Minutes" });
                        dtResults.Rows.Add(new Object[] { "Maximum travel time", Math.Round(tdResults[8], 2), "Minutes" });
                        if (alTravelTimes.Count > 0)
                        {
                            alTravelTimes.Sort();
                            dtResults.Rows.Add(new Object[] { "Travel time " + dFirstLevelPercent + " %", Math.Round(OverallTools.ResultFunctions.getLevelValue(alTravelTimes, dFirstLevelPercent), 2), "Minutes" });
                            dtResults.Rows.Add(new Object[] { "Travel time " + dSecondLevelPercent + " %", Math.Round(OverallTools.ResultFunctions.getLevelValue(alTravelTimes, dSecondLevelPercent), 2), "Minutes" });
                            dtResults.Rows.Add(new Object[] { "Travel time " + dThirdLevelPercent + " %", Math.Round(OverallTools.ResultFunctions.getLevelValue(alTravelTimes, dThirdLevelPercent), 2), "Minutes" });
                        }
                    }
                    if (dAverageThroughput != -1)
                    {
                        dtResults.Rows.Add(new Object[] { "Min throughput", Math.Round(dMinThroughput, 2), "Bags/h" });
                        dtResults.Rows.Add(new Object[] { "Average throughput", Math.Round(dAverageThroughput, 2), "Bags/h" });
                        dtResults.Rows.Add(new Object[] { "Max throughput", Math.Round(dMaxThroughput, 2), "Bags/h" });
                    }
                    if (dAverageAverage != -1)
                    {
                        dtResults.Rows.Add(new Object[] { "Min Average", Math.Round(dMinAverage, 2), "Bags" });
                        dtResults.Rows.Add(new Object[] { "Average Average", Math.Round(dAverageAverage, 2), "Bags" });
                        dtResults.Rows.Add(new Object[] { "Max Average", Math.Round(dMaxAverage, 2), "Bags" });
                    }
                    if (dAverageMax != -1)
                    {
                        dtResults.Rows.Add(new Object[] { "Min Max", Math.Round(dMinMax, 2), "Bags" });
                        dtResults.Rows.Add(new Object[] { "Average Max", Math.Round(dAverageMax, 2), "Bags" });
                        dtResults.Rows.Add(new Object[] { "Max Max", Math.Round(dMaxMax, 2), "Bags" });
                    }
                    return dtResults;
                }

                public DataTable getDistribution(Double dStep, int iNbClasses, Double dWarmUp)
                {
                    return getDistribution(false, dStep, iNbClasses, dWarmUp);
                }

                public DataTable getDistribution(bool bTravelTimes, Double dStep, int iNbClasses, Double dWarmUp)
                {
                    dFirstLevel = -1;
                    dSecondLevel = -1;
                    dThirdLevel = -1;

                    sUnit = "Minutes";
                    Double dCoeff = 1;
                    /*if ((StationType == OriginatingTransferSorterInduction)
                        || (StationType == OriginatingTransferHBS1)
                        || (StationType == OriginatingTransferHBS2)
                        || (StationType == OriginatingTransferHBS3)
                        || (StationType == OriginatingTransferHBS4)
                        || (StationType == OriginatingTransferChute))
                    {*/
                    sUnit = "Seconds";
                    dCoeff = 60;
                    dStep = 1;
                    /*}

                    if (StationType == OriginatingTransferEBS)
                    {
                        sUnit = "Hours";
                        dCoeff = 1.0 / 60.0;
                        dStep = 1;
                        iNbClasses = 14;
                    }*/
                    DataTable dtResult = OverallTools.ResultFunctions.BuildClassTable(sStationName + "_Distribution", dStep, iNbClasses, 0, sUnit, new string[] { "Nb Bags" });
                    if (dtResult == null)
                        return null;
                    Double dValue;
                    int iNbBaggage = 0;
                    //Variable qui permet de déterminer les valeurs à x%
                    ArrayList alTimes = new ArrayList();
                    for (int i = 0; i < alStationUse.Count; i++)
                    {
                        Result rResult = (Result)alStationUse[i];
                        if (rResult.ArrivingTime < dWarmUp)
                            continue;
                        if (rResult.Stopped)
                            continue;
                        iNbBaggage++;
                        if (bTravelTimes)
                            dValue = rResult.TravelTime;
                        else
                            dValue = rResult.OccupationTime;
                        dValue = dValue * dCoeff;
                        alTimes.Add(dValue);
                        int iIndex = OverallTools.ResultFunctions.DetermineClass(dValue, dStep, iNbClasses);
                        dtResult.Rows[iIndex][1] = (int)dtResult.Rows[iIndex][1] + 1;
                    }
                    alTimes.Sort();
                    if (alTimes.Count > 0)
                    {
                        dFirstLevel = OverallTools.ResultFunctions.getLevelValue(alTimes, dFirstLevelPercent);
                        dSecondLevel = OverallTools.ResultFunctions.getLevelValue(alTimes, dSecondLevelPercent);
                        dThirdLevel = OverallTools.ResultFunctions.getLevelValue(alTimes, dThirdLevelPercent);
                    }

                    //On calcul les pourcentages et le cumul des pourcentages.
                    dtResult.Columns.Add("Distrib. (%)", typeof(Double));
                    dtResult.Columns.Add("Cumul (%)", typeof(Double));
                    Double cumul = 0;
                    foreach (DataRow drTheLine in dtResult.Rows)
                    {
                        if (iNbBaggage == 0)
                        {
                            drTheLine[2] = 0;
                            drTheLine[3] = 0;
                        }
                        else
                        {
                            drTheLine[2] = Math.Round(((float)((int)drTheLine[1]) / (float)(iNbBaggage)) * 100, 2);
                            cumul += (Double)drTheLine[2];
                            drTheLine[3] = cumul;
                        }
                    }
                    return dtResult;
                }
                public DataTable getOccupation(DateTime dtBegin, DateTime dtEnd, Double dStep, Double dWarmUp,
                    bool bSlidingHour, Double analysisRange)    // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                {
                    dMinThroughput = -1;
                    dAverageThroughput = -1;
                    dMaxThroughput = -1;

                    dMinAverage = -1;
                    dAverageAverage = -1;
                    dMaxAverage = -1;

                    dMinMax = -1;
                    dAverageMax = -1;
                    dMaxMax = -1;
                    if (dStep <= 0)
                        return null;
                    if (dtBegin > dtEnd)
                        return null;
                    DataTable dtOccupationTable = new DataTable(sStationName + "_Occupation");
                    dtOccupationTable.Columns.Add("Time", typeof(DateTime));
                    dtOccupationTable.Columns.Add("Nb bags", typeof(Int32));
                    dtOccupationTable.Columns.Add("Throughput", typeof(Int32));

                    OverallTools.DataFunctions.initialiserLignes(dtOccupationTable, dtBegin, dtEnd, dStep);

                    DateTime dtRealEnd = (DateTime)dtOccupationTable.Rows[dtOccupationTable.Rows.Count - 1][0];
                    dtRealEnd = dtRealEnd.AddMinutes(dStep);

                    foreach (DataRow Row in dtOccupationTable.Rows)
                    {
                        Row[1] = 0;
                        Row[2] = 0;
                    }
                    for (int i = 0; i < alStationUse.Count; i++)
                    {
                        Double dArrivingTime = ((Result)alStationUse[i]).ArrivingTime;
                        DateTime dtArrivingTime = dtBegin.AddMinutes(dArrivingTime);
                        if (dtArrivingTime > dtRealEnd)
                        {
                            //the value is out of the simulation Bound.
                            continue;
                        }
                        Double dIndex = dArrivingTime / dStep;
                        int iIndex = (int)dIndex;
                        if (iIndex < dtOccupationTable.Rows.Count)
                            dtOccupationTable.Rows[iIndex][1] = (int)dtOccupationTable.Rows[iIndex][1] + 1;
                    }

                    // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation                    
                    //OverallTools.ResultFunctions.AnalyzePeak_SlidingHour(dtOccupationTable, 1, 2, dStep, bSlidingHour);
                    OverallTools.ResultFunctions.calculateThroughputSliding(dtOccupationTable, 1, 2, dStep, analysisRange);
                    // >> Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                    UpdateThroughput(dtOccupationTable, dWarmUp);
                    return dtOccupationTable;
                }


                public DataTable getMinMaxQueueOccupation(DateTime dtBegin, DateTime dtEnd, Double dStep, Double dWarmUp,
                    Double analysisRange)   // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                {
                    ArrayList alTimes = new ArrayList();
                    foreach (Result rValues in alStationUse)
                    {
                        alTimes.Add(new PointF((float)rValues.ArrivingTime, (float)rValues.LeavingTime));
                    }
                    DataTable dtOccupation = CalcOccupationQueue(sStationName + "_Occupation", alTimes, dtBegin, dtEnd,
                        dStep, true, true, analysisRange);  // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                    UpdateThroughput(dtOccupation, dWarmUp);
                    return dtOccupation;

                    /* ArrayList alTimes = new ArrayList();
                     foreach (Result rValues in alStationUse)
                     {
                         //alTimes.Add(new PointF((float)rValues.ArrivingTime, (float)rValues.LeavingTime ));
 //                        Math.Floor(rValues.OccupationTime * 100) / 100;
 //                        (float)Math.Round(rValues.OccupationTime, 2))
                         if ((rValues.ArrivingTime<=163.13) && (rValues.LeavingTime>=163.05) )
                             alTimes.Add(new PointF((float)rValues.ArrivingTime, (float)rValues.LeavingTime));
                     }
                     String sMessage = "";
                     foreach (PointF tt in alTimes)
                         sMessage += tt.X.ToString() + "\t" + tt.Y.ToString() + "\n";
                     Clipboard.SetText(sMessage);


                     DataTable dtOccupation = CalcOccupationQueue(sStationName + "_Occupation", alTimes, dtBegin, dtEnd,1, true, true);
                     UpdateThroughput(dtOccupation, dWarmUp);
                     return dtOccupation;*/
                }

                private void UpdateThroughput(DataTable dtOccupationTable, Double dWarmUp)
                {
                    dMinThroughput = -1;
                    dAverageThroughput = -1;
                    dMaxThroughput = -1;

                    dMinAverage = -1;
                    dAverageAverage = -1;
                    dMaxAverage = -1;

                    dMinMax = -1;
                    dAverageMax = -1;
                    dMaxMax = -1;
                    Double[] dtResult = OverallTools.ResultFunctions.GetStatColumn(dtOccupationTable, "Throughput", dWarmUp);
                    if (dtResult == null)
                    {
                        dtResult = OverallTools.ResultFunctions.GetStatColumn(dtOccupationTable, "Throughput (Input)", dWarmUp);
                        if (dtResult == null)
                            return;
                    }
                    dMinThroughput = dtResult[0];
                    dAverageThroughput = dtResult[2];
                    dMaxThroughput = dtResult[1];

                    dtResult = OverallTools.ResultFunctions.GetStatColumn(dtOccupationTable, "Average", dWarmUp);
                    if (dtResult == null)
                    {
                        dtResult = OverallTools.ResultFunctions.GetStatColumn(dtOccupationTable, "Average Occupation", dWarmUp);
                        if (dtResult == null)
                            return;
                    }
                    dMinAverage = dtResult[0];
                    dAverageAverage = dtResult[2];
                    dMaxAverage = dtResult[1];
                    dtResult = OverallTools.ResultFunctions.GetStatColumn(dtOccupationTable, "Max", dWarmUp);
                    if (dtResult == null)
                    {
                        dtResult = OverallTools.ResultFunctions.GetStatColumn(dtOccupationTable, "Max Occupation", dWarmUp);
                        if (dtResult == null)
                            return;
                    }
                    dMinMax = dtResult[0];
                    dAverageMax = dtResult[2];
                    dMaxMax = dtResult[1];
                }

            }
            #endregion

            #endregion

            #region Les fonctions qui ouvrent le bagtrace et qui initialisent les informations de résultats.
            public Boolean OpenTrace(Double dEndingTime, bool bSaveTraceMode)
            {
                return OpenTrace(dEndingTime, bSaveTraceMode, null);
            }
            public Boolean OpenTrace(Double dEndingTime, bool bSaveTraceMode, Prompt.SIM_LoadingForm cht)
            {
                Initialize();
                #region Ouverture du fichier trace.
                if (!System.IO.File.Exists(sFileName))
                {
                    alErrorList.Add("Err10050 : A problem appear when trying to read the file result.");
                    return false;
                }
                System.IO.StreamReader monLecteur;
                try
                {
                    monLecteur = new System.IO.StreamReader(sFileName);
                }
                catch (Exception exc)
                {
                    alErrorList.Add("Err10051 : A problem appear when trying to read the file result.");
                    OverallTools.ExternFunctions.PrintLogFile("Err10051: " + this.GetType().ToString() + " throw an exception when trying to read the file result: " + exc.Message);
                    return false;
                }
                if (monLecteur == null)
                    return false;
                //On ignore la première ligne.
                String sTmp = monLecteur.ReadLine();
                #endregion

                #region les variables
                String[] sIgnoredColumns;

                String sUneLigne;
                String[] dsValeurs;
                int iIndex;
                #endregion

                #region On lit tout le contenu de la table.
                ArrayList alList = new ArrayList();
                if (cht != null)
                {
                    cht.setFileNumber(1);
                    cht.ChargementFichier("Reading " + sFileName);
                }

                while (monLecteur.Peek() != -1)
                {
                    sUneLigne = monLecteur.ReadLine();
                    alList.Add(sUneLigne);
                }
                if (cht != null)
                {
                    cht.Reset(0);
                    cht.setFileNumber(alList.Count / 1000);
                }
                monLecteur.Close();
                #endregion

                bool bIsStoppedInCurrentProcess = false;

                String ObservedLine;
                for (int iLine = 0; iLine < alList.Count; iLine++)
                {
                    if ((cht != null) && (iLine % 1000 == 0))
                    {
                        //cht.ChargementFichier("Line " + iLine.ToString());
                        cht.ChargementFichier("Scenario \"" + sScenarioName + "\": line " + iLine.ToString());  // >> Task #16728 PAX2SIM Improvements (Recurring) C#13
                    }
                    ObservedLine = (String)alList[iLine];
                    dsValeurs = ObservedLine.Split('\t');
                    sIgnoredColumns = new string[iIgnoredColumns];
                    #region Lecture des 5 (ou 6) premières colonnes (colonnes toujours présentes)
                    for (int iFirstColumns = 0; iFirstColumns < iIgnoredColumns; iFirstColumns++)
                    {
                        sIgnoredColumns[iFirstColumns] = dsValeurs[iFirstColumns];
                    }
                    #endregion

                    BaggageResult brBaggageResult = null;
                    iIndex = iIgnoredColumns;

                    #region Génération de la classe de stockage des résultats
                    if (dsValeurs.Length < iIndex)
                        continue;

                    String sPoste = dsValeurs[iIndex];
                    if (sPoste.Length == 0)
                        continue;
                    String sDeskType = sPoste.Split('_')[0];

                    brBaggageResult = new TransferringResult(sIgnoredColumns);
                    //SDE Débuggage 13/01/2010
                    //brBaggageResult.OldLine = ObservedLine;

                    #endregion
                    if (dsValeurs[dsValeurs.Length - 1] == StoppedKeyWord)
                        brBaggageResult.Stopped = true;
                    String sLastDesk = "";
                    StationResult.Result Res = null;
                    //Ensuite viennent une succession de temps indiquant les différentes étapes suivies par le baggage.
                    bool bBlank = false;
                    while (dsValeurs.Length >= iIndex + 3)
                    {
                        if (dsValeurs[iIndex] == "")
                        {
                            bBlank = true;
                            break;
                        }
                        SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp = isValidDesk(dsValeurs[iIndex]);
                        if (tcTmp==null)
                        {
                            alErrorList.Add("Err10052 : Unvalid Working point for the \"" + sIgnoredColumns[0] + "\" in line n°" + (iLine + 2).ToString() + ".");
                            break;
                        }
                        bIsStoppedInCurrentProcess = (dsValeurs[iIndex + 2] == StoppedKeyWord);
                        BaggageDesk desk;
                        if (bIsStoppedInCurrentProcess)
                            desk = BaggageDesk.getBaggageDesk(dsValeurs[iIndex], dsValeurs[iIndex + 1], dEndingTime);
                        else
                            desk = BaggageDesk.getBaggageDesk(dsValeurs[iIndex], dsValeurs[iIndex + 1], dsValeurs[iIndex + 2]);
                        if (desk == null)
                        {
                            alErrorList.Add("Err10053 : Unvalid format for the \"" + sIgnoredColumns[0] + "\" in line n°" + (iLine + 2).ToString() + ". The time for that station does not have a good format.");
                            break;
                        }
                        iIndex += 3;
                        brBaggageResult.AddBaggageWay(desk);

                        if (sLastDesk == desk.DeskName)
                        {
                            Res.LeavingTime = desk.LeavingTime;
                            if (bIsStoppedInCurrentProcess)
                                Res.Stopped = true;
                        }
                        else
                        {
                            if (!htStation.ContainsKey(desk.DeskName))
                            {
                                htStation.Add(desk.DeskName, new StationResult(desk.DeskName,tcTmp, dFirstLevelPercent, dSecondLevelPercent, dThirdLevelPercent));
                                if (desk.UniqueTime)
                                    ((StationResult)htStation[desk.DeskName]).TimeNull = true;
                            }
                            if (Res != null)
                            {
                                Res.TravelTime = desk.ArrivingTime - Res.LeavingTime;
                            }
                            Res = new StationResult.Result(desk.ArrivingTime, desk.LeavingTime);
                            ((StationResult)htStation[desk.DeskName]).AddStationResult(Res);
                            if (bIsStoppedInCurrentProcess)
                                Res.Stopped = true;
                            sLastDesk = desk.DeskName;
                        }
                    }
                    if ((brBaggageResult.Stopped) && (Res != null) && (!bIsStoppedInCurrentProcess))
                    {
                        Res.TravelTime = -1;
                    }
                    if ((dsValeurs.Length != iIndex) && (!bBlank))
                    {
                        if (dsValeurs[iIndex] != StoppedKeyWord)
                            continue;
                        //brBaggageResult.
                        //Il y a eu un break dans la boucle précédente, car les valeurs n'étaient pas correctes.
                        //Ou alors le nombre de colonnes pour les temps de passage n'est pas multiple de 3,
                        //et donc il y a un problème dans le BagTrace.
                        //   alErrorList.Add("Err10010 : The baggage with the id \"" + iIDBag.ToString() + "\" contains errors on its row.");
                    }
                    try
                    {
                        //SDE 22/01/2010
                        //brBaggageResult.OldLine = ObservedLine;
                        htTransferring.Add(sIgnoredColumns[0], brBaggageResult);
                    }
                    catch (Exception exc)
                    {
                        alErrorList.Add("Err10054 : Unable to insert the \"" + sIgnoredColumns[0] + "\". The trace has already been inserted.");
                        OverallTools.ExternFunctions.PrintLogFile("Err10054: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                        continue;
                    }
                }
                if (cht != null)
                    cht.Reset(0);
                //We copy the bagtrace in the temporary directory, to save it later with the project.
                if (bSaveTraceMode)
                {
                    String sTempDirectory = OverallTools.ExternFunctions.getTempDirectoryForPax2sim();
                    if (sTempDirectory != "")
                    {
                        if (OverallTools.ExternFunctions.CheckCreateDirectory(sTempDirectory))
                        {
                            OverallTools.ExternFunctions.CopyFile(sTempDirectory + sScenarioName + "_Trace.tmp", sFileName, "", null, null, cht);
                        }
                    }
                }
                return true;
            }
            #endregion

            #region Transferring part

            private void GenerateTransferringResults(Hashtable htResults)
            {
                AnalyseDeparting(htResults, "Transf", "Transferring_RemainingTime", htTransferring, 1, 31, dWarmUp, new Double[] { dFirstLevelPercent, dSecondLevelPercent, dThirdLevelPercent }, true, false);
                AnalyseDeparting(htResults, "Transf", null, htTransferring, 1, 31, dWarmUp, new Double[] { dFirstLevelPercent, dSecondLevelPercent, dThirdLevelPercent }, false, false);
            }

            #endregion

            #region Fonction pour le calcul de l'occupation globale par les bagages
            private void GenerateGlobalOccupationResults(Hashtable htResults, DateTime dtBeginDate, DateTime dtEndDate,
                Double dStep, Double analysisRange) // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
            {
                ArrayList alTransferring = new ArrayList();
                ArrayList alTransferringQueue = new ArrayList();
                float fEndSimulation = (float)Math.Round(OverallTools.DataFunctions.MinuteDifference(dtBeginDate, dtEndDate), 1);
                float fArrivingTime, fArrivingInfeedTime, fLeavingTime;

                foreach (TransferringResult orResult in htTransferring.Values)
                {
                    fArrivingTime = (float)orResult.ArrivingTime;
                    fArrivingInfeedTime = (float)orResult.ArrivingInfeedTime;
                    fLeavingTime = (float)orResult.LeavingTime;
                    if (orResult.Stopped)
                    {
                        alTransferringQueue.Add(new PointF(fArrivingTime, fEndSimulation));
                        if (orResult.HasInfeed)
                            alTransferring.Add(new PointF(fArrivingInfeedTime, fEndSimulation));
                    }
                    else
                    {
                        alTransferringQueue.Add(new PointF(fArrivingTime, fLeavingTime));
                        if (orResult.HasInfeed)
                            alTransferring.Add(new PointF(fArrivingInfeedTime, fLeavingTime));
                    }
                }

                DataTable dtTable;


                if (htTransferring.Count > 0)
                {
                    dtTable = OverallTools.ResultFunctions
                        .CalcQueueOccupationWithLeavingTimes("Transferring_From_Collector", alTransferring, dtBeginDate,
                        dtEndDate, dStep, "Nb Bags", true, true, analysisRange);    // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                    if (!htResults.ContainsKey(dtTable.TableName))
                        htResults.Add(dtTable.TableName, dtTable);
                    dtTable = OverallTools.ResultFunctions
                        .CalcQueueOccupationWithLeavingTimes("Transferring_From_Queue", alTransferringQueue, dtBeginDate,
                        dtEndDate, dStep, "Nb Bags", true, true, analysisRange);    // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                    if (!htResults.ContainsKey(dtTable.TableName))
                        htResults.Add(dtTable.TableName, dtTable);
                }
            }
            #endregion

            public Hashtable GenerateResults(DateTime dtBeginDate,
                                             DateTime dtEndDate,
                                             Double dStep,
                                             Double dStepDistribution,
                                             int iNbClasses,
                                             Prompt.SIM_LoadingForm cht,
                                             Double analysisRange)  // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
            {
                int iResult = htStation.Keys.Count + 5;
                if (cht != null)
                    cht.setFileNumber(iResult);
                Results = new Hashtable();

                OpenOutputFiles(Results);
                //if (cht != null)
                //    cht.ChargementFichier("Transferring distribution");
                //GenerateTransferringResults(htResults);

                if (cht != null)
                    cht.ChargementFichier("Generating global occupations");
                GenerateGlobalOccupationResults(Results, dtBeginDate, dtEndDate, dStep, analysisRange); // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation

                Hashtable htSorter = new Hashtable();
                Hashtable htHBS = new Hashtable();
                Hashtable htGroups = new Hashtable();

                Hashtable htConcatValues = new Hashtable();

                StationResult srTmp;
                DataTable dtTmp;
                SIMCORE_TOOL.OverallTools.TraceTools.TraceContent tcTmp;
                #region Parcours de tous les objets trouvé dans le bagtrace
                foreach (String key in htStation.Keys)
                {
                    if (cht != null)
                        cht.ChargementFichier(key);
                    srTmp = ((StationResult)htStation[key]);
                    dtTmp = null;
                    tcTmp = srTmp.TypeDesk;
                    if (tcTmp == null)
                        continue;
                    #region Dans le cas des autres postes
                    if (!htGroups.ContainsKey(srTmp.StationType))
                    {
                        htGroups.Add(srTmp.StationType, new StationResult(srTmp.StationType + "_0", tcTmp, dFirstLevelPercent, dSecondLevelPercent, dThirdLevelPercent));
                        ((StationResult)htGroups[srTmp.StationType]).TimeNull = srTmp.TimeNull;
                        iResult++;
                        if (cht != null)
                            cht.setFileNumber(iResult);
                    }
                    ((StationResult)htGroups[srTmp.StationType]).AddRangeInformation(srTmp);

                    #endregion

                    if (!tcTmp.bIndexed)
                        continue;

                    #region Partie pour la génération de l'occupation des files d'attente.
                    if (tcTmp.bQueue)
                    {
                        dtTmp = srTmp.getMinMaxQueueOccupation(dtBeginDate, dtEndDate, dStep, dWarmUp,
                            analysisRange); // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                    }
                    else
                    {
                        dtTmp = srTmp.getOccupation(dtBeginDate, dtEndDate, dStep, dWarmUp, true
                            , analysisRange);   // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                    }
                    if (dtTmp != null)
                    {
                        if (!htConcatValues.Contains(srTmp.StationType + "_Occupation"))
                            htConcatValues.Add(srTmp.StationType + "_Occupation", new DataTable(srTmp.StationType + "_Occupation"));
                        Results.Add(dtTmp.TableName, dtTmp);
                        OverallTools.DataFunctions.ConcateneTable(((DataTable)htConcatValues[srTmp.StationType + "_Occupation"]), dtTmp, new String[] {/*"Nb bags",*/ "Throughput" }, srTmp.StationName + "_");
                    }
                    #endregion

                    #region S'il y a un temps de traitement.
                    if (!srTmp.TimeNull)
                    {
                        dtTmp = srTmp.getDistribution(dStepDistribution, iNbClasses, dWarmUp);
                        Results.Add(dtTmp.TableName, dtTmp);
                        if (!htConcatValues.Contains(srTmp.StationType + "_Distribution"))
                            htConcatValues.Add(srTmp.StationType + "_Distribution", new DataTable(srTmp.StationType + "_Distribution"));
                        OverallTools.DataFunctions.ConcateneTable(((DataTable)htConcatValues[srTmp.StationType + "_Distribution"]), dtTmp, new String[] { "Distrib. (%)" }, srTmp.StationName + "_");
                    }
                    #endregion

                    #region Partie pour les statistiques des postes.
                    dtTmp = srTmp.getStatistics(dWarmUp);
                    Results.Add(dtTmp.TableName, dtTmp);

                    if (!htConcatValues.Contains(srTmp.StationType + "_Statistics"))
                        htConcatValues.Add(srTmp.StationType + "_Statistics", new DataTable(srTmp.StationType + "_Statistics"));
                    OverallTools.DataFunctions.ConcateneTable(((DataTable)htConcatValues[srTmp.StationType + "_Statistics"]), dtTmp, new String[] { "Value" }, srTmp.StationName + "_");
                    #endregion
                }
                #endregion

                #region Gestion des résultats pour les groupes de postes.
                foreach (String sKey in htGroups.Keys)
                {
                    if (cht != null)
                        cht.ChargementFichier(sKey);
                    srTmp = ((StationResult)htGroups[sKey]);
                    tcTmp = srTmp.TypeDesk;
                    if ((tcTmp.bQueue) || (!tcTmp.bIndexed))
                    {
                        dtTmp = srTmp.getMinMaxQueueOccupation(dtBeginDate, dtEndDate, dStep, dWarmUp,
                            analysisRange); // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                    }
                    else
                    {
                        dtTmp = srTmp.getOccupation(dtBeginDate, dtEndDate, dStep, dWarmUp, true,
                            analysisRange); // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
                    }
                    if (dtTmp != null)
                    {
                        if (htConcatValues.Contains(srTmp.StationType + "_Occupation"))
                        {
                            AddDesksStatistics(dtTmp, (DataTable)htConcatValues[srTmp.StationType + "_Occupation"], srTmp.StationType + "_", "_Throughput");
                        }
                        Results.Add(dtTmp.TableName, dtTmp);
                    }

                    if (!srTmp.TimeNull)
                    {
                        dtTmp = srTmp.getDistribution(dStepDistribution, iNbClasses, dWarmUp);
                        if (htConcatValues.Contains(srTmp.StationType + "_Distribution"))
                        {
                            AddDesksStatistics(dtTmp, (DataTable)htConcatValues[srTmp.StationType + "_Distribution"], srTmp.StationType + "_", "_Distrib. (%)");
                        }
                        Results.Add(dtTmp.TableName, dtTmp);
                    }

                    dtTmp = srTmp.getStatistics(dWarmUp);
                    Results.Add(dtTmp.TableName, dtTmp);
                    if (htConcatValues.Contains(srTmp.StationType + "_Statistics"))
                    {
                        AddDesksStatistics(dtTmp, (DataTable)htConcatValues[srTmp.StationType + "_Statistics"], srTmp.StationType + "_", "_Value");
                    }
                }
                #endregion
                return Results;
            }
            private static bool AddDesksStatistics(DataTable dtGeneral, DataTable dtDesks, String sPrefixe, String sSuffixe)
            {
                if (dtGeneral.Rows.Count != dtDesks.Rows.Count)
                    return false;
                for (int i = 1; i < 300; i++)
                {
                    String SColumnName = sPrefixe + i.ToString() + sSuffixe;
                    if (!dtDesks.Columns.Contains(SColumnName))
                        continue;
                    OverallTools.DataFunctions.ConcateneTable(dtGeneral, dtDesks, new String[] { SColumnName }, "");
                    dtDesks.Columns.Remove(SColumnName);
                    if (dtDesks.Columns.Count == 0)
                        break;
                }
                if (dtDesks.Columns.Count == 0)
                    return true;
                return false;
            }

            #region Fonctions statiques.

            public static DataTable CalcOccupationQueue(String sTableName, ArrayList alTimes, DateTime dtBegin,
                DateTime dtEnd, Double dStep, bool bUseSliddingHour, bool bOutputStats,
                Double analysisRange) // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
            {
                return OverallTools.ResultFunctions.CalcQueueOccupationWithLeavingTimes(sTableName, alTimes, dtBegin,
                    dtEnd, dStep, "Nb bags", bUseSliddingHour, bOutputStats, analysisRange);    // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
            }

            public static void AnalyseDeparting(Hashtable htResults,
                                                String sName,
                                                String sRemainingName,
                                                Hashtable htData,
                                                Double dStep,
                                                int iNbClasses,
                                                Double dWarmUp,
                                                Double[] Levels,
                                                bool bCollector,
                                                bool bHasEBS)
            {
                if (bCollector)
                    sName += "Coll";
                Double dDecallage = 20;
                DataTable dtStatistics = new DataTable(sName + "__Statistics");
                dtStatistics.Columns.Add("Type", typeof(String));
                dtStatistics.Columns.Add("Nb Bags", typeof(Int32));
                dtStatistics.Columns.Add("Min", typeof(Double));
                dtStatistics.Columns.Add("Average", typeof(Double));
                dtStatistics.Columns.Add("Max", typeof(Double));
                dtStatistics.Columns.Add(Levels[0].ToString() + " %", typeof(Double));
                dtStatistics.Columns.Add(Levels[1].ToString() + " %", typeof(Double));
                dtStatistics.Columns.Add(Levels[2].ToString() + " %", typeof(Double));

                ArrayList htGlobal = new ArrayList();
                ArrayList htStandardBags = new ArrayList();

                ArrayList htHBS3 = new ArrayList();
                ArrayList htMES = new ArrayList();
                ArrayList htHBS3_MES = new ArrayList();
                ArrayList htHBS3_WithoutMES = new ArrayList();
                ArrayList htInterLink = new ArrayList();
                ArrayList htEBS = new ArrayList();
                ArrayList htHBS5 = new ArrayList();
                //SDE Débuggage 13/01/2010
                //String sTmp = "";
                ArrayList htRemaining = new ArrayList();
                int iNb = 0;
                //SDE_Débugg 22/01/2010
                //ArrayList alTooLong = new ArrayList();
                foreach (string iResult in htData.Keys)
                {
                    DepartingResult drTmp = (DepartingResult)htData[iResult];
                    if (drTmp.Stopped)
                        continue;
                    //Seb ==> 19.01.2010 
                    //if (drTmp.ArrivingTime < dWarmUp)
                    if (drTmp.LeavingTime < dWarmUp)
                        continue;

                    //SDE_Débugg 22/01/2010
                    //if (drTmp.TotalSimulationTime > 60)
                    //    alTooLong.Add(drTmp.OldLine);

                    /*if (drTmp.STD != -1)
                        htRemaining.Add(drTmp.RemainingTime + dDecallage);*/
                    //else
                    //    htRemaining.Add(0.0);
                    Double dValue;
                    if (bCollector)
                    {
                        dValue = drTmp.TotalSimulationTime;
                    }
                    else
                    {
                        dValue = drTmp.TotalTime;
                    }
                    if (dValue <= 0)
                    {
                        iNb++;
                    }
                    htGlobal.Add(dValue);


                    if ((!drTmp.HasHBS3) && (!drTmp.HasMES) && (!drTmp.HasInterLink))
                    {
                        htStandardBags.Add(dValue);
                        //SDE Débuggage 13/01/2010
                        //sTmp += dValue + "\t\t" +drTmp.OldLine + "\r\n";
                    }

                    if (drTmp.HasHBS3)
                    {
                        htHBS3.Add(dValue);
                        if ((!drTmp.HasMES) /*&& (!drTmp.HasInterLink)*/)
                            htHBS3_WithoutMES.Add(dValue);
                    }

                    if (drTmp.HasMES)
                        htMES.Add(dValue);
                    if ((drTmp.HasMES) && (drTmp.HasHBS3))
                        htHBS3_MES.Add(dValue);

                    if (drTmp.HasInterLink)
                        htInterLink.Add(dValue);
                    if (drTmp.HasEBS)
                        htEBS.Add(dValue);

                    if (drTmp.HasHBS5)
                        htHBS5.Add(dValue);
                }
                /*String sValue = "";
                foreach (Double dValue in htStandardBags)
                    sValue += dValue.ToString() + "\r\n";
                Clipboard.SetText(sTmp);*/
                //Clipboard.SetText(sTmp);

                //SDE_Débugg 22/01/2010
                /*System.IO.StreamWriter swWriter = new StreamWriter("c:\\Terminating_" + sName + ".txt");
                foreach (String sLine in alTooLong)
                    swWriter.WriteLine(sLine);
                swWriter.Close();*/
                ///SDE_Débugg


                DataTable dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_Global", htGlobal, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);

                dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_Standard_Bags", htStandardBags, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);

                dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_HBS3", htHBS3, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);

                dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_MES", htMES, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);

                dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_HBS3_MES", htHBS3_MES, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);

                dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_HBS3_WithoutMES", htHBS3_WithoutMES, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);

                dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_Interlink", htInterLink, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);

                if (bHasEBS)
                {
                    dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_EBS", htEBS, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                    htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);
                }
                String sHBS5 = "HBS5";

                dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sName + "_" + sHBS5, htHBS5, dStep, iNbClasses, 0, "Minutes", new String[] { "Nb Bags" }, Levels, dtStatistics);
                htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);

                if ((bCollector) && (htRemaining.Count > 0))
                {
                    // << Task #8246 Pax2Sim - Dynamic Analysis - Bag Trace analysis - split the total time into smaller intervals
                    //dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sRemainingName, htRemaining, 5, 29, -dDecallage, "Minutes", new String[] { "Nb Bags" }, Levels, null);
                    dtOriginatingDistribution = OverallTools.ResultFunctions.GenerateDistribution(sRemainingName, htRemaining, 5, GlobalNames.nbTimeIntervalsForRemainingTimeTable, -dDecallage, "Minutes", new String[] { "Nb Bags" }, Levels, null);                    
                    // >> Task #8246 Pax2Sim - Dynamic Analysis - Bag Trace analysis - split the total time into smaller intervals                    
                    htResults.Add(dtOriginatingDistribution.TableName, dtOriginatingDistribution);
                }
                htResults.Add(dtStatistics.TableName, dtStatistics);
            }
            #endregion


            internal void OpenOutputFiles(Hashtable htResults)
            {
                DataTable dtResultat = null;
                for (int i = 0; i < GestionDonneesHUB2SIM.lsOutputTables.Count; i++)
                {
                    if (!System.IO.File.Exists(sOutputDirectory +"\\"+ GestionDonneesHUB2SIM.lsOutputTablesPath[i]))
                    {
                        alErrorList.Add("Err00212 : Unable to find the output file " + GestionDonneesHUB2SIM.lsOutputTablesPath[i] + ". File ignored.");
                        continue;
                    }
                    dtResultat = new DataTable(GestionDonneesHUB2SIM.lsOutputTables[i]);

                    OverallTools.FonctionUtiles.initialiserTable(dtResultat, GestionDonneesHUB2SIM.ltsOutputColumns[i],
                        GestionDonneesHUB2SIM.ltsOutputTypeColumns[i], GestionDonneesHUB2SIM.ltiOutputPrimaryKey[i]);
                    if (!OverallTools.FonctionUtiles.LectureFichier(dtResultat, sOutputDirectory +"\\"+ GestionDonneesHUB2SIM.lsOutputTablesPath[i], "\t", alErrorList, GestionDonneesHUB2SIM.lccot_OutputConversion[i]))
                    {
                        alErrorList.Add("Err00213 : Unable to load the output file " + GestionDonneesHUB2SIM.lsOutputTablesPath[i] + ". Wrong format. File ignored.");
                        continue;
                    }
                    htResults.Add(dtResultat.TableName, dtResultat);
                }
            }
        }
        #endregion

        private void cb_PP_UseDefinedSeed_CheckedChanged(object sender, EventArgs e)
        {
            mb_PP_UseDefinedSeed.Enabled = cb_PP_UseDefinedSeed.Checked;
        }






        #region Buttons OpenTrace
        /*private void btn_OpenTrace_Click(object sender, EventArgs e)
        {
            ofd_Open.Filter = "Trace files(*.dat)|*.dat|All files|*.*";
            if (ofd_Open.ShowDialog() != DialogResult.OK)
                return;
            txt_Trace.Text = ofd_Open.FileName;
        }*/
        #endregion
    }
}
