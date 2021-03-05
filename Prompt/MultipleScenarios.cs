using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace SIMCORE_TOOL.Prompt
{
    internal partial class MultipleScenarios : Form
    {
        // classe qui permet d'afficher dans la liste 
        internal class ScenarioStatus
        {
            private String sScenarioName_;
            private bool bSimulate_;
            private String sProgress_;
            private TimeSpan tsChrono_;
            Hashtable htBagsResults;
            Hashtable _htPKGResults;
            internal List<String> lsBHSNames;
            CheckState csDisplayModel;
            SIMCORE_TOOL.Prompt.TraceAnalyser.TraceAnalysis htReporterResults;
            OverallTools.PaxTraceAnalysis AnalysePaxTraceClass_;

            private void InitializeScenarioStatus(String sScenarioName,
                                             bool bSimulate,
                                             String sProgress)
            {
                sScenarioName_ = sScenarioName;
                bSimulate_ = bSimulate;
                sProgress_ = sProgress;
                tsChrono_ = new TimeSpan(0, 0, 0);
                csDisplayModel = CheckState.Indeterminate;
            }
            public ScenarioStatus(String sScenarioName)
            {
                InitializeScenarioStatus(sScenarioName, false, "");
            }
            public ScenarioStatus(String sScenarioName, Boolean bSimulate)
            {
                InitializeScenarioStatus(sScenarioName, bSimulate, "");
            }
            public String ScenarioName
            {
                get
                {
                    return sScenarioName_;
                }
            }
            public Boolean Simulate
            {
                get
                {
                    return bSimulate_;
                }
                set
                {
                    bSimulate_ = value;
                }
            }
            public bool Succeed
            {
                get
                {
                    return sProgress_ == "Succeed";
                }
            }
            public String Progress
            {
                get
                {
                    return sProgress_;
                }
                set
                {
                    sProgress_ = value;
                }
            }
            public TimeSpan Chrono
            {
                get
                {
                    return tsChrono_;
                }
                set
                {
                    tsChrono_ = value;
                }
            }
            public Hashtable BagsResults
            {
                get
                {
                    return htBagsResults;
                }
                set
                {
                    htBagsResults = value;
                }
            }

            internal Hashtable htPKGResults
            {
                get
                {
                    return _htPKGResults;
                }
                set
                {
                    _htPKGResults = value;
                }
            }
            internal CheckState DisplayModel
            {
                get
                {
                    return csDisplayModel;
                }
                set 
                {
                    csDisplayModel = value;
                }
            }

            public SIMCORE_TOOL.Prompt.TraceAnalyser.TraceAnalysis ReporterResults
            {
                get
                {
                    return htReporterResults;
                }
                set
                {
                    htReporterResults = value;
                }
            }
            public OverallTools.PaxTraceAnalysis AnalysePaxTraceClass
            {
                set
                {
                    AnalysePaxTraceClass_ = value;
                }
                get
                {
                    return AnalysePaxTraceClass_;
                }
            }
            public override string ToString()
            {
                String sReturn = "[";
                if (bSimulate_)
                    sReturn += "X";
                else
                    sReturn += " ";
                sReturn += "]\t" + sScenarioName_ + "\t" + sProgress_;
                if (tsChrono_.TotalSeconds > 0)
                    sReturn += "\t" + tsChrono_.ToString();
                return sReturn;
            }
        }

        GestionDonneesHUB2SIM gestionDonnees_;
        PAX2SIM.EnumPerimetre epPerimetre_;
        ArrayList alSimulatedScenarios;
        public ArrayList SimulatedScenarios
        {
            get
            {
                return alSimulatedScenarios;
            }
        }
        TreeNode tnAirport_;
        bool bIsOrHadBeenSimulated;

        internal MultipleScenarios(GestionDonneesHUB2SIM gestionDonnees, PAX2SIM.EnumPerimetre epPerimetre, TreeNode tnAirport)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            gestionDonnees_ = gestionDonnees;
            epPerimetre_ = epPerimetre;
            tnAirport_ = tnAirport;
            if(gestionDonnees_.getScenarioPAXBHS() != null)
            foreach (String name in gestionDonnees_.getScenarioPAXBHS())
            {
                lst_Scenarios.Items.Add(new ScenarioStatus(name));
            }
            lst_Scenarios.Refresh();
            bIsOrHadBeenSimulated = false;
        }

        //Pour protéger le chgangement d'état du checkbox.
        bool bProtect = false;
        private void lst_Scenarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bIsOrHadBeenSimulated)
                return;
            if (lst_Scenarios.SelectedItems.Count == 0)
            {
                cb_Simulate.Enabled = false;
                gb_Scenario.Enabled = false;
                return;
            }

            cb_Simulate.Enabled = true;
            bProtect = true;
            bool bEnabled = false;
            bool bDisabled = false;
            bool bDisplayEnabled = false;
            bool bDisplayDisabled = false;
            for (int i = 0; i < lst_Scenarios.SelectedItems.Count; i++)
            {
                if (lst_Scenarios.SelectedItems[i].GetType() != typeof(ScenarioStatus))
                    continue;
                ScenarioStatus ssTmp = (ScenarioStatus)lst_Scenarios.SelectedItems[i];
                if (ssTmp.Simulate && (!bEnabled))
                    bEnabled = true;
                if (!ssTmp.Simulate && (!bDisabled))
                    bDisabled = true;
                if(ssTmp.DisplayModel== CheckState.Indeterminate)
                {
                    bDisplayEnabled = true;
                    bDisplayDisabled = true;
                }
                else
                {
                    if((ssTmp.DisplayModel== CheckState.Checked)&&(!bDisplayEnabled))
                    bDisplayEnabled = true;
                    if((ssTmp.DisplayModel== CheckState.Unchecked)&&(!bDisplayDisabled))
                    bDisplayEnabled = true;
                }
            }
            if (bEnabled && bDisabled)
                cb_Simulate.CheckState = CheckState.Indeterminate;
            else if (bEnabled)
                cb_Simulate.CheckState = CheckState.Checked;
            else if (bDisabled)
                cb_Simulate.CheckState = CheckState.Unchecked;



            if (cb_Simulate.CheckState == CheckState.Unchecked)
                gb_Scenario.Enabled = false;
            else
            {
                gb_Scenario.Enabled = true;
                if (bDisplayEnabled && bDisplayDisabled)
                    cb_DisplayModel.CheckState = CheckState.Indeterminate;
                else if (bDisplayEnabled)
                    cb_DisplayModel.CheckState = CheckState.Checked;
                else if (bDisplayDisabled)
                    cb_DisplayModel.CheckState = CheckState.Unchecked;
            }
            bProtect = false;
        }

        private void cb_Simulate_CheckedChanged(object sender, EventArgs e)
        {
            if (bProtect)
                return;
            for (int i = 0; i < lst_Scenarios.SelectedItems.Count; i++)
            {
                if (lst_Scenarios.SelectedItems[i].GetType() != typeof(ScenarioStatus))
                    continue;
                ScenarioStatus ssTmp = (ScenarioStatus)lst_Scenarios.SelectedItems[i];
                ssTmp.Simulate = cb_Simulate.Checked;
//                ssTmp.DisplayModel = 
            }
            refreshList();
        }
        private void cb_DisplayModel_CheckedChanged(object sender, EventArgs e)
        {
            if (bProtect)
                return;
            for (int i = 0; i < lst_Scenarios.SelectedItems.Count; i++)
            {
                if (lst_Scenarios.SelectedItems[i].GetType() != typeof(ScenarioStatus))
                    continue;
                ScenarioStatus ssTmp = (ScenarioStatus)lst_Scenarios.SelectedItems[i];
                ssTmp.Simulate = cb_Simulate.Checked;
                ssTmp.DisplayModel = cb_DisplayModel.CheckState;
            }
            refreshList();
        }
        private void refreshList()
        {
            ArrayList alSelected = new ArrayList();
            if (lst_Scenarios.SelectedIndices.Count > 0)
                alSelected.AddRange(lst_Scenarios.SelectedIndices);

            ArrayList alList = new ArrayList();
            alList.AddRange(lst_Scenarios.Items);
            lst_Scenarios.Items.Clear();
            foreach (Object obj in alList)
                lst_Scenarios.Items.Add(obj);
            if (alSelected.Count > 0)
                foreach (int iIndex in alSelected)
                    lst_Scenarios.SetSelected(iIndex, true);

        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            ofd_Open.Title = "Locate Simulation model";
            ofd_Open.Filter = "Model (*.exe)|*.exe|All files (*.*)|*.*";
            ofd_Open.FileName = "Hub2sim.exe";
            if (ofd_Open.ShowDialog() != DialogResult.OK)
                return;
            tb_File.Text = ofd_Open.FileName;
        }

        private void btn_LocateFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Locate Simulation folder";
            if (tb_File.Text != "")
                folderBrowserDialog1.SelectedPath = tb_File.Text;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            txt_Folder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (bIsOrHadBeenSimulated)
                return;
            DialogResult = DialogResult.None;
            if (tb_File.Text == "")
            {
                MessageBox.Show("Select a model", "Warning", MessageBoxButtons.OK);
                return;
            }
            if (txt_Folder.Text == "")
            {
                MessageBox.Show("Select a destination folder", "Warning", MessageBoxButtons.OK);
                return;
            }
            alSimulatedScenarios = new ArrayList();
            for (int i = 0; i < lst_Scenarios.Items.Count; i++)
            {
                if (lst_Scenarios.Items[i].GetType() != typeof(ScenarioStatus))
                    continue;
                ScenarioStatus ssTmp = (ScenarioStatus)lst_Scenarios.Items[i];
                if (ssTmp.Simulate)
                    alSimulatedScenarios.Add(ssTmp);
            }
            if (alSimulatedScenarios.Count == 0)
            {
                MessageBox.Show("Select at least one scenario to simulate!", "Warning", MessageBoxButtons.OK);
                return;
            }
            cb_DisplayModel_General.Enabled = false;
            gb_Scenario.Enabled = false;
            btn_Cancel.Enabled = false;
            cb_Simulate.Enabled = false;
            gb_BagtraceFile.Enabled = false;
            groupBox1.Enabled = false;
            bIsOrHadBeenSimulated = true;
            lst_Scenarios.SelectedIndex = -1;
            SIMCORE_TOOL.Prompt.SIM_Chargement cht = null;
            if (!PAX2SIM.bDebug)
            {
                cht = new SIMCORE_TOOL.Prompt.SIM_Chargement(this.Location, this.Size);
                Thread tSimulation = new Thread(new ParameterizedThreadStart(ShowChargement));
                tSimulation.Start(cht);
                Thread.Sleep(100);
            }
            foreach (ScenarioStatus ssTmp in alSimulatedScenarios)
            {
                sStatus = "";
                htBagsResults = null;
                htPKGResults = null;
                AnalysePaxTraceClass_ = null;
                DateTime dtStart = DateTime.Now;
                try
                {
                    Classes.ParamScenario psScenario = gestionDonnees_.GetScenario(ssTmp.ScenarioName);
                    bool bDisplayModel = cb_DisplayModel_General.CheckState == CheckState.Checked;
                    if(cb_DisplayModel_General.CheckState == CheckState.Indeterminate)
                    {
                        bDisplayModel = (ssTmp.DisplayModel == CheckState.Checked);
                        if(ssTmp.DisplayModel == CheckState.Indeterminate)
                            bDisplayModel = psScenario.DisplayModel;
                    }
                    SimulationLaunched(psScenario, bDisplayModel,cht);
                    psScenario.HasBeenSimulated = true;
                    gestionDonnees_.ReplaceScenario(ssTmp.ScenarioName, psScenario);
                }
                catch (Exception exc)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Except02059: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                    sStatus = "Erreur";
                }
                ssTmp.Progress = sStatus;
                DateTime dtEnd = DateTime.Now;
                ssTmp.Chrono = dtEnd - dtStart;
                ssTmp.htPKGResults = htPKGResults;
                ssTmp.BagsResults = htBagsResults;
                ssTmp.lsBHSNames = lsBHSNames;
                ssTmp.ReporterResults = htReporterResults;
                ssTmp.AnalysePaxTraceClass = AnalysePaxTraceClass_;
            }


            if (cht != null)
                cht.KillWindow();
            refreshList();
        }

        private void ShowChargement(Object cht)
        {
            ((Prompt.SIM_Chargement)cht).ShowDialog();
            return;
        }
        String sStatus;
        Hashtable htBagsResults;
        Hashtable htPKGResults;
        List<String> lsBHSNames;
        SIMCORE_TOOL.Prompt.TraceAnalyser.TraceAnalysis htReporterResults;
        OverallTools.PaxTraceAnalysis AnalysePaxTraceClass_;

        private void SimulationLaunched(Classes.ParamScenario psScenario,bool bDisplayModel, SIMCORE_TOOL.Prompt.SIM_Chargement sfForm)
        {
            GestionDonneesHUB2SIM.EraseWarningsErrorsProject();
            /*Copie du model par défaut dans le dossier réservé à cet usage.*/
            String sOriginDirectory = tb_File.Text;

            String sDestinationDirectory = txt_Folder.Text + "\\" + psScenario.Name + "\\";
            String sModelName = sOriginDirectory.Substring(sOriginDirectory.LastIndexOf("\\") + 1);
            String sModelShortName = sModelName.Substring(0, sModelName.LastIndexOf("."));


            sOriginDirectory = sOriginDirectory.Remove(sOriginDirectory.LastIndexOf("\\"))+"\\";

            if (System.IO.Directory.Exists(sDestinationDirectory))
            {
                sStatus = "Directory already exist";
                return;
            }
            //OverallTools.ExternFunctions.CopyDirectory(sOriginDirectory, sDestinationDirectory, sfForm);
            //String sModelPath = sOriginDirectory + sModelName;
            /*if (sDestinationDirectory.Length == 0)
            {
                sStatus = ("Some errors happened during the access of the model.");
                return;
            }*/
            String sDataDirectory = sOriginDirectory + "Data\\";
            String sOutputsDirectory = sOriginDirectory + "Outputs\\";
            String sBG_StatsCounterFile = sOriginDirectory + "hub2sim.pax2sim.bg_statscounter";

            String sMessageFile = tb_File.Text.Remove(tb_File.Text.Length - 3) + "message";
            if ((!OverallTools.ExternFunctions.CheckCreateDirectory(sDataDirectory)) ||
               (!OverallTools.ExternFunctions.CheckCreateDirectory(sOutputsDirectory)))
            {
                sStatus = ("Unable to create the directories for the simulation files.");
                return;
            }
            htPKGResults = null;
            if (psScenario.PaxSimulation)
            {
                htPKGResults = gestionDonnees_.Maj_Scenario(psScenario.Name, null, null, psScenario, null, sfForm, null, null, null, null);
            }
            if (sfForm != null)
                sfForm.ChargementFichier("");
            bool bGenerate = false;
            try
            {
                List<DataTable> automodTablesList = new List<DataTable>();  // >> Task #13361 FP AutoMod Data tables V3
                bGenerate = gestionDonnees_.SaveSimulationFiles(psScenario.Name, sOriginDirectory, psScenario.PaxSimulation, psScenario.BHSSimulation, sfForm, out automodTablesList);

            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except020: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                bGenerate = false;
            }
            try
            {
                if (System.IO.File.Exists(sMessageFile))
                    System.IO.File.Delete(sMessageFile);
            }
            catch (Exception exc) 
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02060: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }

            if (!bGenerate)
            {
                String sError = GestionDonneesHUB2SIM.getLastError();
                if (sError == "")
                    sError = "Some errors happened during the saving of the tables";
                sStatus = sError;
                return;
            }
            if (sfForm != null)
                sfForm.ChargementFichier("");

            //Lancement de la simulation.
            Process proc;
            if (!bDisplayModel)
            {
                proc = Process.Start("amrun", sModelName + " -Mwn");
            }
            else
            {
                proc = Process.Start("amrun", sModelName);
            }
            proc.WaitForExit();
            if (!System.IO.File.Exists(sMessageFile))
            {
                sStatus = "Problem during the simulation (The Message file does not exist, maybe the simulation didn't end properly.)";
                return;
            }
            try
            {
                System.IO.StreamReader srMessageFile = new System.IO.StreamReader(sMessageFile);
                if (srMessageFile == null)
                {
                    sStatus = "The model had a problem. Unable to open Automod model.";
                    return;
                }
                bool bKeyNotFound = false;
                bool bCannotContinue = false;
                while (srMessageFile.Peek() != -1)
                {
                    String sLine = srMessageFile.ReadLine();
                    bKeyNotFound = bKeyNotFound || sLine.Contains("Key not found, model terminated.");
                    bCannotContinue = bCannotContinue || sLine.Contains("Simulation terminating");
                }
                srMessageFile.Close();
                if (bKeyNotFound)
                {
                    sStatus = "The model had a problem. Unable to find the hardware key for the execution of the model.";

                    return;
                }
            }
            catch (Exception exc)
            {
                sStatus = "The model had a problem. Unable to open Automod model.";
                OverallTools.ExternFunctions.PrintLogFile("Except02061: (The model had a problem. Unable to open Automod model.) " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                return;
            } 
            Double dEndingTime = OverallTools.DataFunctions.MinuteDifference(psScenario.DateDebut, psScenario.DateFin);

            // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            double percentile1 = -1;
            double percentile2 = -1;
            double percentile3 = -1;
            if (psScenario.percentilesLevels == null || psScenario.percentilesLevels.Count == 0)
            {
                percentile1 = gestionDonnees_.Levels[0];
                percentile2 = gestionDonnees_.Levels[1];
                percentile3 = gestionDonnees_.Levels[2];
            }
            else if (psScenario.percentilesLevels.Count == 3)
            {
                percentile1 = psScenario.percentilesLevels[0];
                percentile2 = psScenario.percentilesLevels[1];
                percentile3 = psScenario.percentilesLevels[2];
            }
            // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec

            if (psScenario.BHSSimulation)
            {
                OverallTools.BagTraceAnalysis btaBagAnalysis = new OverallTools.BagTraceAnalysis(psScenario.Name, sOutputsDirectory + "BagTrace.txt", psScenario.WarmUp * 60, percentile1, percentile2, percentile3, psScenario);
                if (!btaBagAnalysis.OpenBagTrace(dEndingTime, psScenario.SaveTraceMode, sfForm))
                {
                    sStatus = ("The bagtrace has a wrong format. Unable to analyze it.");
                    return;
                }
                XmlNode airportStrucutre = gestionDonnees_.getRacine(); // >> Task #13659 IST MakeUp segregation
                htBagsResults = btaBagAnalysis.GenerateResults(psScenario.DateDebut, psScenario.DateFin,
                    psScenario.SamplingStep, 1, 31, sfForm, psScenario.AnalysisRange, psScenario.bhsGenerateLocalIST,
                    psScenario.bhsGenerateGroupIST, psScenario.bhsGenerateMUPSegregation, airportStrucutre,
                    psScenario.analysisResultsFilters, psScenario.analysisResultsFiltersSplittedByFlow, psScenario.flowTypes, psScenario.percentilesLevels);  // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation   // >> Task #13955 Pax2Sim -BHS trace loading issue    // >> Task #14280 Bag Trace Loading time too long
                lsBHSNames = btaBagAnalysis.Names;
            }
            
            if (psScenario.PaxSimulation)
            {
                AnalysePaxTraceClass_ = OverallTools.PaxTraceAnalysis.AnalysePaxTrace(psScenario.Name,
                    sOutputsDirectory, sBG_StatsCounterFile, gestionDonnees_, psScenario.SamplingStep,
                    psScenario.WarmUp * 60, psScenario.SaveTraceMode, false);

                if (AnalysePaxTraceClass_ == null)
                {
                    sStatus = ("The paxtrace has a wrong format.");
                    return;
                }

                AnalysePaxTraceClass_.GenerateAllTable(tnAirport_, gestionDonnees_, psScenario.Name, psScenario.SamplingStep, psScenario.percentilesLevels); // >> Task #10484 Pax2Sim - Pax analysis - Summary with distribution levels percent
            }
            if (psScenario.SimAnalyserSimulation)
            {
                htReporterResults = new SIMCORE_TOOL.Prompt.TraceAnalyser.TraceAnalysis(psScenario.Name, sOriginDirectory + psScenario.SimReporterTraceName, sOriginDirectory, psScenario.SimReporterTraceParameters, psScenario.SimReporterIgnoredColumns, psScenario.WarmUp * 60, percentile1, percentile2, percentile3);
                if (psScenario.SimReporterTraceName != "")
                {
                    if (!htReporterResults.OpenTrace(dEndingTime, psScenario.SaveTraceMode, sfForm))
                    {
                        sStatus ="The trace has a wrong format. Unable to analyze it.";
                        return;
                    }
                }
                htReporterResults.GenerateResults(psScenario.DateDebut, psScenario.DateFin, psScenario.SamplingStep,
                    1, 31, sfForm, psScenario.AnalysisRange);   // << Task #8775 Pax2Sim - Occupation stat - Throughput calculation
            }
            OverallTools.ExternFunctions.CopyDirectory(sOriginDirectory + "Data", sDestinationDirectory + "Data", sfForm);
            OverallTools.ExternFunctions.CopyDirectory(sOriginDirectory + "Outputs", sDestinationDirectory + "Outputs", sfForm);
            OverallTools.ExternFunctions.CopyFile(sDestinationDirectory + sModelShortName + ".message", sMessageFile, "", null, null, sfForm);
            OverallTools.ExternFunctions.CopyFile(sDestinationDirectory + sModelShortName + ".report", sOriginDirectory + sModelShortName + ".report", "", null, null, sfForm);
            
            sStatus = "Succeed";
        }
    }
}