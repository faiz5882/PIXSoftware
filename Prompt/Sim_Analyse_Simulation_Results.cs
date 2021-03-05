using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SIMCORE_TOOL.com.crispico.BHS_Analysis;
using SIMCORE_TOOL.com.crispico.generalUse;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.Prompt
{
    public partial class Sim_Analyse_Simulation_Results : Form
    {
        private GestionDonneesHUB2SIM Donnees;
        PAX2SIM.EnumPerimetre epPerimetre;

        public String ScenarioName
        {
            get
            {
                return cb_Scenario.Text;
            }
        }
        public String BagFileName
        {
            get
            {
                return tb_File.Text;
            }
        }
        public String PaxFileName
        {
            get
            {
                return tb_PaxTrace.Text;
            }
        }
        public Double WarmUp
        {
            get
            {
                Double dTmp;
                if (!Double.TryParse(txt_SIM_WarmUp.Text, out dTmp))
                    return -1;
                if (dTmp < 0)
                    return -1;
                return dTmp;
                    
            }
        }
        public Double Step
        {
            get
            {
                Double dTmp;
                if (!Double.TryParse(cb_SIM_AnalyseStep.Text, out dTmp))
                    return -1;
                if (dTmp < 0)
                    return -1;
                return dTmp;

            }
        }
        // << Task #9611 Pax2Sim - BHS - Analyse BHS Results Assistant - Analysis Range option
        public Double AnalysisRange
        {
            get
            {
                Double dTmp;
                if (!Double.TryParse(analysisRangeComboBox.Text, out dTmp))
                    return -1;
                if (dTmp < 0)
                    return -1;
                return dTmp;

            }
        }
        // >> Task #9611 Pax2Sim - BHS - Analyse BHS Results Assistant - Analysis Range option
        public DateTime BeginDate
        {
            get
            {
                return dtp_BeginTime.Value;
            }
        }
        public DateTime EndDate
        {
            get
            {
                return dtp_EndTime.Value;
            }
        }

        public List<AnalysisResultFilter> resultFilters = new List<AnalysisResultFilter>(); // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
        public List<string> flowTypes = new List<string>();

        public List<double> getDistributionLevels() // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
        {
            List<double> distributionLevels = new List<double>();
            
            double percentile1 = 0;
            double percentile2 = 0;
            double percentile3 = 0;

            if (Double.TryParse(firstLevelTextBox.Text, out percentile1)
                && Double.TryParse(secondLevelTextBox.Text, out percentile2)
                && Double.TryParse(thirdLevelTextBox.Text, out percentile3))
            {
                distributionLevels.Add(percentile1);
                distributionLevels.Add(percentile2);
                distributionLevels.Add(percentile3);
            }            
            return distributionLevels;
        }

        public Classes.ParamScenario AnalysisParameters
        {
            get
            {
                Classes.ParamScenario newScenario = new SIMCORE_TOOL.Classes.ParamScenario(ScenarioName);
                newScenario.DateDebut = BeginDate;
                newScenario.DateFin = EndDate;
                newScenario.SamplingStep = Step;    // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17                
                newScenario.AnalysisRange = AnalysisRange;    // >> Task #15556 Pax2Sim - Scenario Properties Assistant issues - C#17
                newScenario.BHSSimulation = tb_File.Text != "";
                newScenario.PaxSimulation = tb_PaxTrace.Text != "";
                newScenario.bhsGenerateLocalIST = generateLocalISTCheckBox.Checked; // >> Task #13955 Pax2Sim -BHS trace loading issue
                newScenario.bhsGenerateGroupIST = generateGroupISTCheckBox.Checked; // >> Task #14280 Bag Trace Loading time too long
                newScenario.bhsGenerateMUPSegregation = generateMUPSegregationCheckBox.Checked; // >> Task #14280 Bag Trace Loading time too long
                newScenario.SaveTraceMode = !copyOutputTablesCheckBox.Checked;  // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
                if (resultFilters != null)
                    newScenario.analysisResultsFilters.AddRange(resultFilters.ToArray());
                if (flowTypes != null)
                    newScenario.flowTypes.AddRange(flowTypes.ToArray());
                newScenario.bagTracePath = BagFileName;
                newScenario.percentilesLevels = getDistributionLevels();

                string assemblyVersion = OverallTools.AssemblyActions.AssemblyVersion;
                if (assemblyVersion.Contains("."))
                {
                    string[] splittedVersion = assemblyVersion.Split('.');
                    if (splittedVersion.Length > 1)
                    {
                        string partialVersion = splittedVersion[0] + "." + splittedVersion[1];
                        double version = -1;
                        Double.TryParse(partialVersion, out version);
                        newScenario.lastModificationP2SVersion = version;
                    }
                }

                return newScenario;
            }
        }

        private void Initialize(GestionDonneesHUB2SIM Donnees_, PAX2SIM.EnumPerimetre epPerimetre_)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            Donnees = Donnees_;
            epPerimetre = epPerimetre_;
            if (epPerimetre == PAX2SIM.EnumPerimetre.PAX)
            {
                gb_BagtraceFile.Visible = false;
                txt_SIM_WarmUp.Enabled = false;
            }
            else if ((epPerimetre == PAX2SIM.EnumPerimetre.BHS) && (PAX2SIM.bBHS_MeanFlows))
            {
                gb_Pax.Visible = false;
            }
            else
            {
                
                this.Size = new Size(this.Size.Width, this.Size.Height + 60);
                this.MinimumSize = this.Size;
                gb_BagtraceFile.Location = new Point(gb_BagtraceFile.Location.X, gb_BagtraceFile.Location.Y + 60);
            }
            if(Donnees.getScenarioPAXBHS() != null)
                foreach (String name in Donnees.getScenarioPAXBHS())
                {
                    cb_Scenario.Items.Add(name);
                }
            cb_SIM_AnalyseStep.SelectedIndex = 8;
            if (analysisRangeComboBox.Items.Count > 0)  // >> Task #18306 PAX2SIM - BHS - Sorter occupation
            {
                analysisRangeComboBox.SelectedIndex = analysisRangeComboBox.Items.Count - 1;
            }
            dtp_BeginTime.Value = DateTime.Now;
            this.dtp_BeginTime.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
            this.dtp_EndTime.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
            dtp_BeginTime.Value = dtp_BeginTime.Value.AddSeconds(-dtp_BeginTime.Value.Second);
            dtp_BeginTime.Value = dtp_BeginTime.Value.AddMinutes(-dtp_BeginTime.Value.Minute);
            dtp_EndTime.Value = dtp_BeginTime.Value.AddDays(1);

            //editResultFiltersButton.Visible = false;    // V6.225 without _Times

            // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            if (Donnees.Levels != null && Donnees.Levels.Length == 3)
            {
                firstLevelTextBox.Text = Donnees.Levels[0].ToString();
                secondLevelTextBox.Text = Donnees.Levels[1].ToString();
                thirdLevelTextBox.Text = Donnees.Levels[2].ToString();
            }
            // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
        }
        internal Sim_Analyse_Simulation_Results(GestionDonneesHUB2SIM Donnees_, PAX2SIM.EnumPerimetre epPerimetre_)
        {
            Initialize(Donnees_, epPerimetre_);
        }
        internal Sim_Analyse_Simulation_Results(GestionDonneesHUB2SIM Donnees_, PAX2SIM.EnumPerimetre epPerimetre_, String sScenario)
        {
            Initialize(Donnees_, epPerimetre_);
            cb_Scenario.Text = sScenario;
        }

        private void Sim_Analyse_Simulation_Results_Shown(object sender, EventArgs e)
        {
            if (cb_Scenario.Text == "")
            {
                int i = 1;
                List<String> alList = Donnees.getScenarioNames();


                while (alList.Contains(SIM_Scenarios_Assistant.DefaultScenarioName + i.ToString()))
                {
                    i++;
                }
                cb_Scenario.Text = SIM_Scenarios_Assistant.DefaultScenarioName + i.ToString();
            }
        }

        private void tb_File_TextChanged(object sender, EventArgs e)
        {
            TextBox tt = (TextBox)sender;
            tt_Tooltip.SetToolTip(tt, tt.Text);
            if ((tt.Text == "") || (System.IO.File.Exists(tt.Text)))
                tt.BackColor = Color.White;
            else
                tt.BackColor = Color.LightSalmon;
            if ((!gb_Pax.Visible) || (tb_PaxTrace.Text == ""))
            {
                dtp_EndTime.Enabled = true;//dtp_EndTime.Enabled = false;// << Task #9611 Pax2Sim - BHS - Analyse BHS Results Assistant - Analysis Range option
                lbl_EndTime.Enabled = true;//lbl_EndTime.Enabled = false;// << Task #9611 Pax2Sim - BHS - Analyse BHS Results Assistant - Analysis Range option
                dtp_EndTime.Value = ObtainEndingTime();
            }
            else
            {
                dtp_EndTime.Enabled = true;
                lbl_EndTime.Enabled = true;
            }
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            ofd_Open.Title = "Open BagTrace result";
            ofd_Open.Filter = "Result file (*.txt)|*.txt|All files (*.*)|*.*";
            ofd_Open.FileName = "BagTrace.txt";
            if (ofd_Open.ShowDialog() != DialogResult.OK)
                return;
            tb_File.Text = ofd_Open.FileName;
        }
        private void btn_OpenPax_Click(object sender, EventArgs e)
        {
            ofd_Open.Title = "Open PaxTrace result";
            ofd_Open.Filter = "Result file (*.txt)|*.txt|All files (*.*)|*.*";
            ofd_Open.FileName = "PaxTrace.txt";
            if (ofd_Open.ShowDialog() != DialogResult.OK)
                return;
            tb_PaxTrace.Text = ofd_Open.FileName;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if ((tb_File.BackColor != Color.White) && (tb_File.Text != ""))
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("The BagTrace file does not exist, please check the informations.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((tb_PaxTrace.BackColor != Color.White) && (tb_PaxTrace.Text != ""))
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("The PaxTrace file does not exist, please check the informations.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((tb_PaxTrace.Text == "") && (tb_File.Text == ""))
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Please select a file to continue the analysis.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (WarmUp ==-1)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("The Warm Up value has to be .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
            if (flowTypes.Count == 0)
            {
                DialogResult dr = MessageBox.Show("You did not activate any Flow Types." + Environment.NewLine + " Are you sure you want to continue?",
                    "Flow Types", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                    DialogResult = DialogResult.None;
            }
            if (resultFilters.Count == 0)
            {
                DialogResult dr = MessageBox.Show("You did not activate any Result Filters." + Environment.NewLine + " Are you sure you want to continue?",
                    "Result Filters", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                    DialogResult = DialogResult.None;
            }
        }

        private void txt_SIM_WarmUp_TextChanged(object sender, EventArgs e)
        {
            if (WarmUp == -1)
                txt_SIM_WarmUp.BackColor = Color.LightSalmon;
            else
                txt_SIM_WarmUp.BackColor = Color.White;
            dtp_EndTime.Value = ObtainEndingTime();
        }

        private void cb_Scenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Scenario.SelectedIndex != -1)
            {
                Classes.ParamScenario paTmp = Donnees.GetScenario(cb_Scenario.Text);
                if (paTmp != null)
                {
                    dtp_BeginTime.Value = paTmp.DateDebut;
                    if (paTmp.BHSSimulation)
                    {
                        cb_SIM_AnalyseStep.Text = paTmp.SamplingStep.ToString();
                        analysisRangeComboBox.Text = paTmp.AnalysisRange.ToString();    // << Task #9611 Pax2Sim - BHS - Analyse BHS Results Assistant - Analysis Range option
                        txt_SIM_WarmUp.Text = paTmp.WarmUp.ToString();
                        generateLocalISTCheckBox.Checked = paTmp.bhsGenerateLocalIST;   // >> Task #13955 Pax2Sim -BHS trace loading issue
                        generateGroupISTCheckBox.Checked = paTmp.bhsGenerateGroupIST;   // >> Task #14280 Bag Trace Loading time too long
                        generateMUPSegregationCheckBox.Checked = paTmp.bhsGenerateMUPSegregation;   // >> Task #14280 Bag Trace Loading time too long
                        copyOutputTablesCheckBox.Checked = !paTmp.SaveTraceMode;    // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
                        String sModelName = paTmp.ModelName;
                        if ((gb_BagtraceFile.Visible) && (sModelName != null) && (sModelName.Length > 0) && (sModelName.IndexOf("\\") != -1))
                        {
                            String Directory = sModelName.Substring(0, sModelName.LastIndexOf('\\')) + "\\";
                            tb_File.Text = Directory + "Outputs\\BagTrace.txt";
                            ObtainEndingTime();
                        }
                        if ((gb_Pax.Visible) && (sModelName != null) && (sModelName.Length > 0) && (sModelName.IndexOf("\\") != -1))
                        {
                            String Directory = sModelName.Substring(0, sModelName.LastIndexOf('\\')) + "\\";
                            tb_PaxTrace.Text = Directory + "Outputs\\PaxTrace.txt";
                        }
                        if (paTmp.analysisResultsFilters != null)
                        {
                            resultFilters.Clear();
                            resultFilters.AddRange(paTmp.analysisResultsFilters.ToArray());
                        }
                        if (paTmp.flowTypes != null)
                        {
                            flowTypes.Clear();
                            flowTypes.AddRange(paTmp.flowTypes.ToArray());

                            if (paTmp.analysisResultsFilters != null && paTmp.analysisResultsFilters.Count > 0)
                            {
                                if (paTmp.flowTypes == null || paTmp.flowTypes.Count == 0)
                                {
                                    flowTypes.Clear();
                                    flowTypes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
                                    //flowTypes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
                                    flowTypes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
                                    //flowTypes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
                                    flowTypes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
                                }
                            }
                        }
                        if (paTmp.deletedCustomAnalysisResultsFilters.Count > 0) // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
                        {
                            string warningMessage = "The following custom filters were deleted and are not linked anymore to this scenario:";
                            foreach (string filterName in paTmp.deletedCustomAnalysisResultsFilters)
                            {
                                warningMessage += Environment.NewLine + " * " + filterName;
                            }
                            MessageBox.Show(warningMessage, "Deleted Result Filters", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            paTmp.deletedCustomAnalysisResultsFilters.Clear();
                        }
                    }
                    // >> Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
                    if (paTmp.percentilesLevels.Count == 3)
                    {
                        firstLevelTextBox.Text = paTmp.percentilesLevels[0].ToString();
                        secondLevelTextBox.Text = paTmp.percentilesLevels[1].ToString();
                        thirdLevelTextBox.Text = paTmp.percentilesLevels[2].ToString();
                    }
                    else if (Donnees.Levels != null && Donnees.Levels.Length == 3)
                    {
                        firstLevelTextBox.Text = Donnees.Levels[0].ToString();
                        secondLevelTextBox.Text = Donnees.Levels[1].ToString();
                        thirdLevelTextBox.Text = Donnees.Levels[2].ToString();
                    }
                    // << Task #9936 Pax2Sim - project properties saved specifically for each scenario + Times stats Spec
                    return;
                }
                else
                {
                    resultFilters.Clear();
                    flowTypes.Clear();
                    if (Donnees.Levels != null && Donnees.Levels.Length == 3)
                    {
                        firstLevelTextBox.Text = Donnees.Levels[0].ToString();
                        secondLevelTextBox.Text = Donnees.Levels[1].ToString();
                        thirdLevelTextBox.Text = Donnees.Levels[2].ToString();
                    }
                }
            }
            dtp_BeginTime.Value = DateTime.Now;
            dtp_BeginTime.Value = dtp_BeginTime.Value.AddSeconds(-dtp_BeginTime.Value.Second);
            dtp_BeginTime.Value = dtp_BeginTime.Value.AddMinutes(-dtp_BeginTime.Value.Minute);

        }

        private void cb_Scenario_TextChanged(object sender, EventArgs e)
        {
            if (cb_Scenario.Items.Contains(cb_Scenario.Text) && cb_Scenario.SelectedIndex == -1)
            {
                cb_Scenario.SelectedIndex = cb_Scenario.Items.IndexOf(cb_Scenario.Text);
            }
        }

        private DateTime ObtainEndingTime()
        {
            Double dTimeStep = Step;
            Double LastObjectTimeLeaving;
            Double dWarmUp = WarmUp;
            DateTime dtBeginDate = dtp_BeginTime.Value;
            if ((!System.IO.File.Exists(BagFileName))|| (dWarmUp == -1)|| (dTimeStep==-1))
                return dtBeginDate.AddHours(1);
            System.IO.StreamReader srFile;
            try
            {
                srFile = new System.IO.StreamReader(BagFileName);
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02062: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                return dtBeginDate.AddHours(1);
            }
            String sLine = srFile.ReadLine();
            ArrayList alList = new ArrayList();
            while(srFile.Peek()!=-1)
            {
                sLine = srFile.ReadLine();
                if (sLine != null)  // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                {
                    sLine = sLine.TrimEnd();
                }
                alList.Add(sLine);
            }
            srFile.Close();
            if (alList.Count == 0)
                return dtBeginDate.AddHours(1);
            if (sLine.Length == 0)
                return dtBeginDate.AddHours(1);
            while (sLine.EndsWith(OverallTools.BagTraceAnalysis.StoppedKeyWord))
            {
                if (alList.Count == 0)
                    return dtBeginDate.AddHours(1);
                sLine = (String)alList[alList.Count - 1];
                alList.RemoveAt(alList.Count - 1);
            }
            String[] tsLine = sLine.Split(new char[] { '\t' });
            if (tsLine.Length <= 1)
                return dtBeginDate.AddHours(1);
            if (!Double.TryParse(tsLine[tsLine.Length - 1], out LastObjectTimeLeaving))
            {
                return dtBeginDate.AddHours(1);
            }
            if (LastObjectTimeLeaving == -1)
            {
                if (!Double.TryParse(tsLine[tsLine.Length - 2], out LastObjectTimeLeaving))
                {
                    return dtBeginDate.AddHours(1);
                }
            }
            //dtBeginDate = dtBeginDate.AddMinutes(LastObjectTimeLeaving);
            //Double dDiff = OverallTools.DatasFunctions.MinuteDifference(dtp_BeginTime.Value,dtBeginDate);
            Double dDiff = ((int)LastObjectTimeLeaving) / ((int)dTimeStep);
            dtBeginDate = dtp_BeginTime.Value.AddMinutes((dDiff + 1) * dTimeStep);
            if (dtp_BeginTime.Value.AddHours(1) > dtBeginDate)
                return dtp_BeginTime.Value.AddHours(1);
            return dtBeginDate;
        }

        private void dtp_BeginTime_ValueChanged(object sender, EventArgs e)
        {
            if (!dtp_EndTime.Enabled)
            {
                dtp_EndTime.Value = ObtainEndingTime();
            }
        }

        private void cb_SIM_AnalyseStep_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (dtp_BeginTime.Enabled)
            {
                dtp_EndTime.Value = ObtainEndingTime();
            }

            // << Task #9611 Pax2Sim - BHS - Analyse BHS Results Assistant - Analysis Range option
            //set up the analysis range values
            int iMax = 60;
            int iValue;

            analysisRangeComboBox.Items.Clear();
            analysisRangeComboBox.Text = "";

            if (!Int32.TryParse(cb_SIM_AnalyseStep.Text, out iValue))
            {
                return;
            }
            for (int i = iValue; i <= iMax; i++)
            {
                if (((i % iValue) == 0) && ((iMax % i) == 0))
                {
                    analysisRangeComboBox.Items.Add(i.ToString());                    
                }
            }
            if (analysisRangeComboBox.Items.Count > 0)  // >> Task #18306 PAX2SIM - BHS - Sorter occupation
            {
                analysisRangeComboBox.SelectedIndex = analysisRangeComboBox.Items.Count - 1;
            }
            // >> Task #9611 Pax2Sim - BHS - Analyse BHS Results Assistant - Analysis Range option
        }

        bool newScenarioModified = false;
        private void editResultFiltersButton_Click(object sender, EventArgs e)  // >> Task #15088 Pax2Sim - BHS Analysis - Times and Indexes
        {
            if (resultFilters != null && flowTypes != null && Donnees != null)
            {
                ParamScenario scenario = Donnees.GetScenario(ScenarioName);
                if (scenario == null && !newScenarioModified)
                {
                    flowTypes = new List<string>();
                    flowTypes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
                    //flowTypes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
                    flowTypes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
                    //flowTypes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
                    flowTypes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
                }
                EditResultFilters resultFiltersEditor = new EditResultFilters(resultFilters, flowTypes, Donnees);
                if (resultFiltersEditor.ShowDialog() == DialogResult.OK)
                {
                    newScenarioModified = true;

                    resultFilters.Clear();
                    resultFilters.AddRange(resultFiltersEditor.resultFilters);

                    flowTypes.Clear();
                    flowTypes.AddRange(resultFiltersEditor.flowTypes);
                }
            }
        }

        private void levelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormUtils.restrictNumbersWithDecimalsOnTextBox(sender, e);
        }

        private void levelTextBox_TextChanged(object sender, EventArgs e)
        {
            restrictMaxValueOnTextBox(sender, 100);
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

        private const string FIRST_PERCENTILE_TAG = "First";
        private const string SECOND_PERCENTILE_TAG = "Second";
        private const string THIRD_PERCENTILE_TAG = "Third";
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
    }
}