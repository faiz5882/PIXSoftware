using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nevron.Chart;
using Nevron.Chart.WinForm;
using System.Collections;
using Nevron;
using System.Runtime.InteropServices;
using Nevron.GraphicsCore;
using SIMCORE_TOOL.Prompt;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL
{
    /// <summary>
    /// classe permettant la gestion des axes des charts.Un graphique comporte 4 positions de charts avec 3 axes par chart : X, Y, Y2
    /// </summary>
    internal partial class Visu : Form
    {
        PAX2SIM ProjectWindow=null;
        GraphicFilter gf;
        NChartControl Graphique;
        /// <summary>
        /// Délégué permettant d'appeler la méthode afficherGraphique de la classe PAX2SIM
        /// </summary>
        SIMCORE_TOOL.PAX2SIM.RefreshGraphDelegate gPD;
        private SIM_ColumnPosition ColumnPosition;
        private SIM_Setpoint setpointWindow;  // Class for the oppening of the setpoint window
        bool buserCheck = true;

        private List<List<GraphicFilter.Axe>> initialAxisList = null;

        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MINIMIZE = 0xf020;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern int EnableMenuItem(IntPtr hMenu, int wIDEnableItem, int wEnable);

        /// <summary>
        /// Constructeur de la classe. 
        /// </summary>
        /// <param name="projectWindow_">Instance de la classe Pax2Sim, permet de faire des opérations sur certaines variables de cette classe</param>
        internal Visu(PAX2SIM projectWindow_)
        {
            InitializeComponent();
            Owner = projectWindow_;
            ProjectWindow = projectWindow_;
            OverallTools.FonctionUtiles.MajBackground(this);
            setpointWindow = new Prompt.SIM_Setpoint();
            ColumnPosition = new SIM_ColumnPosition();
            ///On attache le textbox de nom d'abscisse à la fonction checkKeys pour que des que l'utilisateur appuie sur la touche entrée, le nom soit affecté
            tb_AxisName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckKeys);

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.tb_AxisName, "Press Enter to valid");

        }

        /// <summary>
        /// Permet d'initialiser les variables de la classe à chaque fois que l'on ouvre cette fenetre
        /// ou que l'utilisateur a fait une action nécéssitant l'actualisation du graphique
        /// </summary>
        /// <param name="gf">filtre courant</param>
        /// <param name="Graphique">graphique courant</param>
        /// <param name="gPD">Délégué permettant un appel de la fonction d'actualisation du graphique</param>
        internal void initializeGraphicFilter(GraphicFilter gf, NChartControl Graphique, SIMCORE_TOOL.PAX2SIM.RefreshGraphDelegate gPD)
        {
            if (!buserCheck)
                return;
            buserCheck = false;
            this.gf = gf;
            this.Graphique = Graphique;
            this.gPD = gPD;

            if (initialAxisList == null)
            {
                initialAxisList = new List<List<GraphicFilter.Axe>>();
                List<List<GraphicFilter.Axe>> allChartPositionsAxesLists = gf.getAxisList();
                foreach (List<GraphicFilter.Axe> chartPositionAxesList in allChartPositionsAxesLists)
                {
                    List<GraphicFilter.Axe> chartPositionAxesListClone = new List<GraphicFilter.Axe>();
                    foreach (GraphicFilter.Axe axe in chartPositionAxesList)
                        chartPositionAxesListClone.Add(axe.clone());
                    initialAxisList.Add(chartPositionAxesListClone);
                }
            }
            cb_ChartPosition.SelectedIndex = 0;
            cb_ChartAxis.SelectedIndex = 0;
            tb_AxisName.Text = gf.XTitle;
            buserCheck = true;
            UpdateDisplay();
            //gPD();
        }

        private bool hasBarVisualization(GraphicFilter graphicFilter, int currentChartPosition)
        {
            if (graphicFilter == null || currentChartPosition < 0 || currentChartPosition >= 4)
                return false;
            if (graphicFilter.getListVizualisation().Count == 0 || graphicFilter.getListPositions().Count == 0)
                return false;

            List<int> currentVisualizationsIndex = new List<int>();
            for (int i = 0; i < graphicFilter.getListPositions().Count; i++)
            {
                int pos = -1;
                if (graphicFilter.getListPositions()[i] != null && int.TryParse(graphicFilter.getListPositions()[i].ToString(), out pos))
                {
                    if (pos != -1 && currentChartPosition == pos && !currentVisualizationsIndex.Contains(i))
                        currentVisualizationsIndex.Add(i);
                }
            }
            foreach (int visualizationIndex in currentVisualizationsIndex)
            {
                if (visualizationIndex < graphicFilter.getListVizualisation().Count
                    && graphicFilter.getListVizualisation()[visualizationIndex] != null
                    && (graphicFilter.getListVizualisation()[visualizationIndex].ToString() == "Bar"
                        || graphicFilter.getListVizualisation()[visualizationIndex].ToString() == "Bar stacked"))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Actions effectuées lors de la fermeture de la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Visu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(gPD!=null)
                gPD();
            ProjectWindow.HideVisu(true);
        }

        /// <summary>
        /// Changement de position de chart pour l'édition des axes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_ChartPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            barSettingsGroupBox.Enabled = hasBarVisualization(gf, cb_ChartPosition.SelectedIndex);
            if (!buserCheck)
                return;
            buserCheck = false;
            ///Test si la position selectionnée est disponible, sinon on ne change pas l'affichage    
            NChart m_chart = Graphique.Charts[0];
            int ChartTagPosition = 0;
            ///Récupération du bon chart en fonction de la position choisit
            for (int i = 0; i < Graphique.Charts.Count; i++)
            {
                m_chart = Graphique.Charts[i];
                ///On essaye de convertit Le tag qui contient la position du chart en int. 
                if(int.TryParse(m_chart.Tag.ToString() , out ChartTagPosition))
                    ///On compare la position du chart avec le tag du chart récupéré. 
                    if (ChartTagPosition == cb_ChartPosition.SelectedIndex)
                    {
                        UpdateDisplay();
                        i = Graphique.Charts.Count;
                    }
            }
            ///Si aucun graph n'est disponible à la position demandée alors on ne change rien et on remet la position d'origine
            if (ChartTagPosition != cb_ChartPosition.SelectedIndex)
                cb_ChartPosition.SelectedIndex = 0;
            UpdateDisplay();            
            buserCheck = true;
        }

        /// <summary>
        /// Appellée lors du changement d'axe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_ChartAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// On affiche le nom de l'axe dans la textbox
            switch(cb_ChartAxis.SelectedIndex)
            {
                case 0: tb_AxisName.Text = gf.XTitle; break;

                case 1: tb_AxisName.Text = gf.YTitle; break;

                case 2: tb_AxisName.Text = gf.Y2Title; break;
            }
            UpdateDisplay();
        }

        /// <summary>
        /// Mise a jour de l'axe
        /// </summary>
        private void UpdateAxes()
		{
            NChart m_chart = Graphique.Charts[0];
            NAxis axis = RetrieveAxis();
            if (axis == null)
            {
                groupBox1.Enabled = false;
                label2.Enabled = false;
                cb_ChartAxis.Enabled = false;
                return;
            }
            else
            {
                groupBox1.Enabled = true;
                label2.Enabled = true;
                cb_ChartAxis.Enabled = true;
            }

            ///Si le champ de debut d'axe est visible alors ça veut dire que les données affichées sur cet axe sont de type numérique
            if (nUD_Beginvalue.Visible)
            {
                
                ///On définit un nouvel axe avec les parametres correspondants de debut et fin d'axe
                NNumericAxisPagingView NumericPagingView = new NNumericAxisPagingView(new NRange1DD((double)nUD_Beginvalue.Value, (double)nUD_AxisLength.Value));
                axis.PagingView = NumericPagingView;
                if ((cb_ChartAxis.SelectedIndex == 0) && (m_chart.Axis(StandardAxis.SecondaryX)).Tag.ToString() == "String")
                    m_chart.Axis(StandardAxis.SecondaryX).PagingView = NumericPagingView;

                // << Task #7570 new Desk and extra information for Pax -Phase I B                
                NRangeAxisView axisView = new NRangeAxisView(new NRange1DD((double)nUD_Beginvalue.Value, (double)nUD_AxisLength.Value));
                axis.View = axisView;
                if ((cb_ChartAxis.SelectedIndex == 0) && (m_chart.Axis(StandardAxis.SecondaryX)).Tag.ToString() == "String")
                    m_chart.Axis(StandardAxis.SecondaryX).View = axisView;
                // >> Task #7570 new Desk and extra information for Pax -Phase I B
                                                
                ///On récupère la liste contenant la définition des axes
                List<List<GraphicFilter.Axe>> axe = gf.getAxisList();
                ///On change les données correspondantes à l'axe courant en édition
                axe[cb_ChartPosition.SelectedIndex][cb_ChartAxis.SelectedIndex].BeginNValue = (int)nUD_Beginvalue.Value ;
                axe[cb_ChartPosition.SelectedIndex][cb_ChartAxis.SelectedIndex].LengthNValue = (int)nUD_AxisLength.Value;
                gf.setAxisList(axe);
                Graphique.Refresh();
            }
            ///sinon ca veut dire que les données de l'axe sont de type DateTime
            else
            {
                NDateTimeAxisPagingView DateTimepagingView = axis.PagingView as NDateTimeAxisPagingView;
                if (DateTimepagingView != null)
                {
                    List<List<GraphicFilter.Axe>> axe = gf.getAxisList();
                    axe[cb_ChartPosition.SelectedIndex][cb_ChartAxis.SelectedIndex].BeginDTValue = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, dateTimePicker1.Value.Hour, dateTimePicker1.Value.Minute,0);
                    if ((int)nUD_AxisLength.Value == 0)
                    {
                        axe[cb_ChartPosition.SelectedIndex][cb_ChartAxis.SelectedIndex].LengthDTValue = (int)DateTimepagingView.Length.Units;
                    }
                    else
                    {
                        axe[cb_ChartPosition.SelectedIndex][cb_ChartAxis.SelectedIndex].LengthDTValue = (int)nUD_AxisLength.Value;
                    }
                    axe[cb_ChartPosition.SelectedIndex][cb_ChartAxis.SelectedIndex].IsDateTime = true;
                    gf.setAxisList(axe);
                    DateTimepagingView.Begin = dateTimePicker1.Value;
                    DateTimepagingView.Length = new NDateTimeSpan((int)nUD_AxisLength.Value, NDateTimeUnit.Hour);
                }
            }
            Graphique.Refresh();
		}
        
        /// <summary>
        /// On met a jour l'affichage des données correspondant à l'axe choisit dans la fenetre
        /// </summary>
        internal void UpdateDisplay()
        {
            if (Graphique == null || Graphique.Charts.Count == 0)
                return;
            ///On récupère l'axe
            NAxis axis = RetrieveAxis();
            if (axis == null)
            {
                groupBox1.Enabled = false;
                label2.Enabled = false;
                cb_ChartAxis.Enabled = false;
                return;
            }
            else
            {
                groupBox1.Enabled = true;
                label2.Enabled = true;
                cb_ChartAxis.Enabled = true;
            }
            ///On récupère le type de l'axe
            Type PVtype= axis.PagingView.GetType();

            ///Si type DateTime on affiche le dateTimePicker et on cache la numérique comboBox
            if (PVtype == typeof(NDateTimeAxisPagingView))
            {
                nUD_Beginvalue.Visible = false;
                dateTimePicker1.Visible = true;
                NDateTimeAxisPagingView DateTimepagingView = axis.PagingView as NDateTimeAxisPagingView;
                
                if (DateTimepagingView != null)
                {
                    ///On affiche la valeur de debut d'axe
                    dateTimePicker1.Value = DateTimepagingView.Begin;

                    label4.Text = "Axis range (in hours):";
                    ///On affiche la longueur de l'axe
                    nUD_AxisLength.Value = DateTimepagingView.Length.Units;
                    
                    if (DateTimepagingView.Length.Units == 0)
                        nUD_AxisLength.Value = 2;
                    ///Si la liste des scrollbar de contient rien, ca veut dire que aucun scrollbar n'existe
                    if (gf.getListXScrollbar().Count == 0)
                        cb_Scrolbar.Checked = false;
                    ///Sinon regarder s'il y a une scrollbar à la position donnée
                    else
                    {
                        buserCheck = false;
                        cb_Scrolbar.Checked = gf.getListXScrollbar()[cb_ChartPosition.SelectedIndex];
                        buserCheck = true;
                    }

                    if (gf.getListBarWidth().Count == 0)
                        barWidthTextBox.Text = "0";
                    else
                    {
                        buserCheck = false;                        
                        barWidthTextBox.Text = gf.getListBarWidth()[cb_ChartPosition.SelectedIndex].ToString();
                        buserCheck = true;
                    }
                    if (gf.getListGapPercent().Count == 0)
                        gapPercentTextBox.Text = "0";
                    else
                    {
                        buserCheck = false;
                        gapPercentTextBox.Text = gf.getListGapPercent()[cb_ChartPosition.SelectedIndex].ToString();
                        buserCheck = true;
                    }
                    if (gf.getListBarWidthPercent().Count == 0)
                        barWidthPercentageTextBox.Text = "0";
                    else
                    {
                        buserCheck = false;
                        barWidthPercentageTextBox.Text = gf.getListBarWidthPercent()[cb_ChartPosition.SelectedIndex].ToString();
                        buserCheck = true;
                    }
                }
            }
            /// données de l'axe de type numerique
            else if(PVtype == typeof(NNumericAxisPagingView))
            {
                ///on cache le dateTimePicker et on affiche la numérique comboBox
                nUD_Beginvalue.Visible = true;
                dateTimePicker1.Visible = false;
                label4.Text = "Axis end :";
                ///Si l'axe est l'axe X on n'utilise les paging view pour récupérer les valeurs d'axe
                if ((axis.Tag != null) && (axis.Tag.ToString() == "XNumericValue"))
                {
                    NNumericAxisPagingView NumericpagingView = axis.PagingView as NNumericAxisPagingView;
                    if (NumericpagingView != null)
                    {
                        nUD_Beginvalue.Value = (int)NumericpagingView.Begin;
                        nUD_AxisLength.Value = (int)NumericpagingView.Length + nUD_Beginvalue.Value;                 
                    }
                
                }
                ///Sinon on utilise le pageRange
                else
                {
                    //int beginvalue = (int)Math.Floor(axis.PageRange.Begin);
                    //int endvalue = (int)Math.Ceiling(axis.PageRange.End);
                                        
                    nUD_Beginvalue.Value = nUD_Beginvalue.Minimum;
                    nUD_AxisLength.Value = nUD_AxisLength.Maximum;
                    
                    //nUD_Beginvalue.Value = beginvalue;
                    //nUD_AxisLength.Value = endvalue;

                    nUD_Beginvalue.Value = (int)Math.Floor(axis.PageRange.Begin);
                    nUD_AxisLength.Value = (int)Math.Ceiling(axis.PageRange.End);
                }
                //gf.getListVizualisation();
                if (gf.getListXScrollbar().Count == 0)
                    cb_Scrolbar.Checked = false;
                else
                {
                    buserCheck = false;
                    cb_Scrolbar.Checked = gf.getListXScrollbar()[cb_ChartPosition.SelectedIndex];
                    buserCheck = true;
                }
                if (gf.getListBarWidth().Count == 0)
                    barWidthTextBox.Text = "0";
                else
                {
                    buserCheck = false;
                    barWidthTextBox.Text = gf.getListBarWidth()[cb_ChartPosition.SelectedIndex].ToString();
                    buserCheck = true;
                }
                if (gf.getListGapPercent().Count == 0)
                    gapPercentTextBox.Text = "0";
                else
                {
                    buserCheck = false;
                    gapPercentTextBox.Text = gf.getListGapPercent()[cb_ChartPosition.SelectedIndex].ToString();
                    buserCheck = true;
                }
                if (gf.getListBarWidthPercent().Count == 0)
                    barWidthPercentageTextBox.Text = "0";
                else
                {
                    buserCheck = false;
                    barWidthPercentageTextBox.Text = gf.getListBarWidthPercent()[cb_ChartPosition.SelectedIndex].ToString();
                    buserCheck = true;
                }
            }
        }

        /// <summary>
        /// Changement de la longueur de l'axe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nUD_AxisLength_ValueChanged(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            buserCheck = false;
            NChart m_chart = Graphique.Charts[0];
            int ChartTagPosition;
            bool retrievedAxe = false;
            ///On récupère tous les charts suivant les positions et on compare avec la position voulue
            for (int i = 0; i < Graphique.Charts.Count; i++)
            {
                m_chart = Graphique.Charts[i];
                if (int.TryParse(m_chart.Tag.ToString(), out ChartTagPosition))
                    if (ChartTagPosition == cb_ChartPosition.SelectedIndex)
                    {
                        retrievedAxe = true;
                        i = Graphique.Charts.Count;
                    }
            }
            if (retrievedAxe == false) m_chart = Graphique.Charts[0]; 
            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)m_chart.Axis(StandardAxis.SecondaryX).ScaleConfigurator;
            ///Si le chart est un candle alors on ne doit pas pouvoir modifier son axe
            if (scaleConfiguratorX.Title.Text == "Candle Axis")
            {
                UpdateDisplay();
                return;
            }
            if (!nUD_Beginvalue.Visible && nUD_AxisLength.Value <= 0)
                nUD_AxisLength.Value = 1;
            else if (nUD_Beginvalue.Visible == false)
                UpdateAxes();
            ///Si la longueur de l'axe est superieur à la valeur de debut 
            ///alors c'est ok sinon on fixe la valeur à celle de la valeur de debut +1
            else if (nUD_AxisLength.Value > nUD_Beginvalue.Value)
                UpdateAxes();
            else
                nUD_AxisLength.Value = nUD_Beginvalue.Value + 1;
            buserCheck = true;
        }

        /// <summary>
        /// changement de la valeur de debut de l'axe.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nUD_Beginvalue_ValueChanged(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            buserCheck = false;
            NChart m_chart = Graphique.Charts[0];
            int ChartTagPosition;
            bool retrievedAxe = false;
            ///On récupère tous les charts suivant les positions et on compare avec la position voulue
            for (int i = 0; i < Graphique.Charts.Count; i++)
            {
                m_chart = Graphique.Charts[i];
                if (int.TryParse(m_chart.Tag.ToString(), out ChartTagPosition))
                    if (ChartTagPosition == cb_ChartPosition.SelectedIndex)
                    {
                        retrievedAxe = true;
                        i = Graphique.Charts.Count;
                    }
            }
            if (retrievedAxe == false) m_chart = Graphique.Charts[0];
            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)m_chart.Axis(StandardAxis.SecondaryX).ScaleConfigurator;
            ///Si le chart est un candle alors on ne doit pas pouvoir modifier son axe
            if (scaleConfiguratorX.Title.Text == "Candle Axis")
            {
                UpdateDisplay();
                return;
            }
            ///Si la valeur de debut  est superieur à la longueur de l'axe 
            ///alors on fixe la valeur à celle de la longueur de l'axe -1
            if(nUD_Beginvalue.Value > nUD_AxisLength.Value)
                nUD_Beginvalue.Value = nUD_AxisLength.Value - 1;
            UpdateAxes();

            buserCheck = true;
        }

        /// <summary>
        /// changement d'etat de la scrollbar d'abscisse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Scrolbar_CheckedChanged(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            buserCheck = false;
            NAxis axis = RetrieveAxis();
            ///On récupère la liste X scrollbar pour pouvoir y ajouter ou enlever une scrollbar
            List<Boolean> tmpList = gf.getListXScrollbar();
            ///Si la liste est vide alors on ajoute 4 booleens a faux
            if (tmpList.Count == 0)                
                for (int i = 0; i < 4; i++)
                    tmpList.Add(false);
            ///On affecte l'état de la scrollbar à la liste
            tmpList[cb_ChartPosition.SelectedIndex] = cb_Scrolbar.Checked;
            gf.setListXScrollbar(tmpList);
            //setAxisLimits();           
            gPD();
            ///on actualise l'affichage
            UpdateDisplay();

        }

        /// <summary>
        /// changement de la date de départ d'un axe en dateTime 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            UpdateAxes();
        }
        
        /// <summary>
        /// appelée à chaque affichage de la fenetre, on cache le bouton minimize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Visu_Load(object sender, EventArgs e)
        {
            MinimizeBox = false;
            EnableMenuItem(GetSystemMenu(this.Handle, false), SC_MINIMIZE, WM_SYSCOMMAND);
        }

        /// <summary>
        /// Fonction permettant de récupérer l'axe en cours d'edition
        /// </summary>
        /// <returns></returns>
        private NAxis RetrieveAxis()
        {
            NAxis axis;
            NChart m_chart = Graphique.Charts[0];
            int ChartTagPosition;
            bool retrievedAxe = false;
            ///On récupère tous les charts suivant les positions et on compare avec la position voulue
            for (int i = 0; i < Graphique.Charts.Count; i++)
            {
                m_chart = Graphique.Charts[i];
                if (int.TryParse(m_chart.Tag.ToString(), out ChartTagPosition))
                    if (ChartTagPosition == cb_ChartPosition.SelectedIndex)
                    {
                        retrievedAxe = true;
                        i = Graphique.Charts.Count;
                    }
            }
            if (retrievedAxe == false)
                m_chart = Graphique.Charts[0];         
            if (m_chart == null)
                return null;
            ///Ensuite on récupère l'axe correspondant au choix de l'utilisateur
            if (cb_ChartAxis.SelectedIndex == 0)
                axis = m_chart.Axis(StandardAxis.PrimaryX);
            else if (cb_ChartAxis.SelectedIndex == 1)
                axis = m_chart.Axis(StandardAxis.PrimaryY);
            else
                axis = m_chart.Axis(StandardAxis.SecondaryY);

            return axis;

        }

        /// <summary>
        /// Lancement de la fenetre de gestion des setpoints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Setpoint_Click(object sender, EventArgs e)
        {
            setpointWindow = new Prompt.SIM_Setpoint();
            ///On met à jour les listes dans la fenetre d'edition des setpoints
            setpointWindow.Value = gf.getListSetPointValue();
            setpointWindow.Value2 = gf.getListSetPointValue2();
            setpointWindow.beginDateTime = gf.getListSetPointBDateTime();
            setpointWindow.endDateTime = gf.getListSetPointEDateTime();
            setpointWindow.listSetPointStrokeColor = gf.getListSetPointStrokeColor();
            setpointWindow.listSetPointFillColor = gf.getListSetPointFillColor();
            setpointWindow.isArea = gf.getListIsArea();
            setpointWindow.axis = gf.getListSetPointAxis();
            setpointWindow.isActivated = gf.getListIsActivated();
            setpointWindow.setPoints = gf.setPoints;    // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
                        
            // Retrieves the Abscissa type, useful for the setpoint use
            int xAxisIndex = gf.getListAxeRepresentation().IndexOf("X");//gf.getListVizualisation().IndexOf("X");
            /*if (iIndex >= 0)  // >> Bug #15147 Charts Setpoints only work for first series chart location (frame) C#12
            {
                DataTable dtTAble;
                String sColumnName = "";
                if (ProjectWindow.DonneesEnCours != null && gf.getlistColumnsOrigin()[iIndex].GetType() == typeof(ColumnInformation))
                {
                    ColumnInformation ciColumn = (ColumnInformation)gf.getlistColumnsOrigin()[iIndex];//getListColumnsNames()[iIndex];
                    if ((ciColumn.ExceptionName != null) && (ciColumn.ExceptionName != ""))
                        dtTAble = ProjectWindow.DonneesEnCours.getExceptionTable(ciColumn.DataSet, ciColumn.TableName,
                                                                                 ciColumn.ExceptionName);
                    else
                        dtTAble = ProjectWindow.DonneesEnCours.getTable(ciColumn.DataSet, ProjectWindow.CurrentNode.Name);
                    sColumnName = ciColumn.ColumnName;
                }
                else
                    return;
                if (dtTAble == null)
                    return;
                 
                
                //else
                //{
                //    dtTAble = (DataTable)ProjectWindow.dataGridView1.DataSource;
                //    sColumnName = gf.getListColumnsNames()[iIndex].ToString();
                //}
                Type tt = dtTAble.Columns[sColumnName].DataType;
                if (tt.Name.ToString() == "DateTime")
                {
                    setpointWindow.DateTimeAbscissa = true;

                    setpointWindow.SetAbscissaDateLimits(OverallTools.DataFunctions.getMinDate(dtTAble, 0), OverallTools.DataFunctions.getMaxDate(dtTAble, 0));
                }
                else
                    setpointWindow.DateTimeAbscissa = false;
            }*/

            // >> Bug #15147 Charts Setpoints only work for first series chart location (frame) C#12
            if (xAxisIndex >= 0)
            {
                TreeViewTag currentNodeTag = ProjectWindow.CheckCurrentNode();
                if (currentNodeTag != null && !currentNodeTag.isDirectoryNode && !currentNodeTag.isResultNode && !currentNodeTag.isAirportNode)
                {
                    DataTable chartTable = null;
                    if (currentNodeTag.IsExceptionNode)
                    {
                        chartTable = ProjectWindow.DonneesEnCours.getExceptionTable(currentNodeTag.ScenarioName, currentNodeTag.Name, currentNodeTag.ExceptionName);
                    }
                    else
                    {
                        chartTable = ProjectWindow.DonneesEnCours.getTable(currentNodeTag.ScenarioName, currentNodeTag.Name);
                    }
                    if (chartTable != null && xAxisIndex < gf.getListColumnsNames().Count && gf.getListColumnsNames()[xAxisIndex].GetType() == typeof(String))
                    {
                        String xAxisColumnName = (String)gf.getListColumnsNames()[xAxisIndex];
                        if (chartTable.Columns[xAxisColumnName] != null)
                        {
                            if (chartTable.Columns[xAxisColumnName].DataType == typeof(DateTime))
                            {
                                int xAxisColumnIndex = chartTable.Columns.IndexOf(xAxisColumnName);
                                if (xAxisColumnIndex != -1) // >> Bug #13387 Default Analysis Charts X axis time range C#14
                                {
                                    setpointWindow.DateTimeAbscissa = true;
                                    setpointWindow.SetAbscissaDateLimits(OverallTools.DataFunctions.getMinDate(chartTable, xAxisColumnIndex),
                                        OverallTools.DataFunctions.getMaxDate(chartTable, xAxisColumnIndex));
                                }
                                else
                                {
                                    OverallTools.ExternFunctions.PrintLogFile("SetPoint assistant: Error while setting the time axis limits."
                                         + Environment.NewLine + "The column \"" + xAxisColumnName + "\" couldn't be found in the table \"" + chartTable.TableName + "\".");
                                }
                            }
                            else
                            {
                                setpointWindow.DateTimeAbscissa = false;
                            }
                        }
                    }
                }
            }
            else
            {
                // >> Bug #15147 Charts Setpoints only work for first series chart location (frame) C#15
                DateTime minDate = DateTime.MaxValue;
                DateTime maxDate = DateTime.MinValue;
                if (PAX2SIM.allAbscissaColumnsAreDateTime(gf.getlistColumnsOrigin(), ProjectWindow.DonneesEnCours, out minDate, out maxDate)
                    && minDate != DateTime.MaxValue && maxDate != DateTime.MinValue)
                {
                    setpointWindow.DateTimeAbscissa = true;
                    setpointWindow.SetAbscissaDateLimits(minDate, maxDate);
                }
                else
                {
                    setpointWindow.DateTimeAbscissa = false;
                }
                // << Bug #15147 Charts Setpoints only work for first series chart location (frame) C#15
            }
            // << Bug #15147 Charts Setpoints only work for first series chart location (frame) C#12

            ///Mettre à jour l'affichage dans la fenetre d'edition.
            setpointWindow.DisplaySetpointList();

            if (setpointWindow.ShowDialog() == DialogResult.OK)
            {

                gf.setListSetPointValue(setpointWindow.Value);
                gf.setListSetPointValue2(setpointWindow.Value2);
                gf.setListSetPointBDateTime(setpointWindow.beginDateTime);
                gf.setListSetPointEDateTime(setpointWindow.endDateTime);
                gf.setListSetPointStrokeCouleurs(setpointWindow.listSetPointStrokeColor);
                gf.setListSetPointFillColor(setpointWindow.listSetPointFillColor);
                gf.setListIsArea(setpointWindow.isArea);
                gf.setListSetPointAxis(setpointWindow.axis);
                gf.setListIsActivated(setpointWindow.isActivated);
                gf.setPoints = setpointWindow.setPoints;    // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
                buserCheck = false;
                gPD();
                buserCheck = true;
            }
            
        }

        /// <summary>
        /// Lancement de la fenetre de gestion de la position des courbes sur le graphique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ColumnOrder_Click(object sender, EventArgs e)
        {
            ColumnPosition.initializeWindow(gf.getListColumnsNames(),gf.getlistColumnsOrigin(), gf.getListVizualisation(),gf.getListAxeRepresentation(),gf.getListStrokeCouleurs(),
            gf.getListFillCouleurs(), gf.getListAccumulation(), gf.getListPositions(), gf.getListShowValues(), gf.getMaxCandleValue(), gf.getMidCandleValue(),gf.getMinCandleValue());

            ColumnPosition.TopMost = true;
            if (ColumnPosition.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < gf.getListColumnsNames().Count; i++)
                {
                    gf.getListColumnsNames()[i] = ColumnPosition.ColumnNames_[i];
                    gf.getlistColumnsOrigin()[i] = ColumnPosition.OriginColumn_[i];
                    gf.getListVizualisation()[i] = ColumnPosition.laRepresentation_[i];
                    gf.getListAxeRepresentation()[i] = ColumnPosition.lAxeDeRepresentation_[i];
                    gf.getListStrokeCouleurs()[i] = ColumnPosition.lesCouleursTour_[i];
                    gf.getListFillCouleurs()[i] = ColumnPosition.lesCouleursRemplissage_[i];
                    gf.getListAccumulation()[i] = ColumnPosition.lesAccumulations_[i];
                    gf.getListPositions()[i] = ColumnPosition.lesPositions_[i];
                    gf.getMaxCandleValue()[i] = ColumnPosition.MaxCandleValue_[i];
                    gf.getMinCandleValue()[i] = ColumnPosition.MinCandleValue_[i];
                    gf.getMidCandleValue()[i] = ColumnPosition.MidCandleValue_[i];
                }
                buserCheck = false;
                gPD();
                buserCheck = true;
            }
        }

        /// <summary>
        /// Changement du nom de l'axe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_AxisName_TextChanged(object sender, EventArgs e)
        {
            ///On affecte le nouveau nom de l'axe en fonction de l'axe en cours d'edition
            switch(cb_ChartAxis.SelectedIndex)
            {              
                case 0: gf.XTitle = tb_AxisName.Text; break;

                case 1: gf.YTitle = tb_AxisName.Text; break;

                case 2: gf.Y2Title = tb_AxisName.Text; break;
            }
            //UpdateGraphique();
        }

        /// <summary>
        /// Fonction appellée lors de l'appuie sur une touche dans la TextBox correspondant à l'affichage du nom de l'axe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ///Le code 13 correspond à la touche Entrée
            if (e.KeyChar == (char)13)
            {
                UpdateGraphique();
            }
        }
        private void UpdateGraphique()
        {
            buserCheck = false;
            gPD();
            buserCheck = true;
        }

        /// <summary>
        /// Fonction permettant l'affectation des limites des champs de gestion d'axe.
        /// </summary>
        private void setAxisLimits()
        {
            NAxis axis = RetrieveAxis();
            if (axis.Name.Contains("PrimaryX"))
            {
                Type PVtype= axis.PagingView.GetType();

                if (cb_Scrolbar.Checked)
                {
                    if (PVtype == typeof(NNumericAxisPagingView))
                    {

                        NNumericAxisPagingView NumericpagingView = axis.PagingView as NNumericAxisPagingView;
                        nUD_Beginvalue.Minimum = nUD_AxisLength.Minimum = (uint)NumericpagingView.Begin;
                        nUD_AxisLength.Maximum = nUD_Beginvalue.Maximum = (uint)NumericpagingView.Length;
                    }
                    else
                    {
                        NDateTimeAxisPagingView NDateTimepagingView = axis.PagingView as NDateTimeAxisPagingView;
                        nUD_AxisLength.Maximum = nUD_Beginvalue.Maximum = (uint)NDateTimepagingView.Length.Units;
                    }
                }
                else
                {
                    nUD_Beginvalue.Minimum = nUD_AxisLength.Minimum = -99999999;
                    nUD_AxisLength.Maximum = nUD_Beginvalue.Maximum = 99999999;
                }
            }
            else
            {
                nUD_Beginvalue.Minimum = nUD_AxisLength.Minimum = -99999999;
                nUD_AxisLength.Maximum = nUD_Beginvalue.Maximum = 99999999;
            }
        }

        private void barWidthTextBox_Leave(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            buserCheck = false;
            NAxis axis = RetrieveAxis();
            List<float> tmpList = gf.getListBarWidth();
            if (tmpList.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                    tmpList.Add(0.0f);
            }
            float width = 0.0f;
            if (!float.TryParse(barWidthTextBox.Text, out width))
                width = 0.0f;
            tmpList[cb_ChartPosition.SelectedIndex] = width;
            gf.setListBarWidth(tmpList);
            gPD();
            ///on actualise l'affichage
            UpdateDisplay();
        }

        private void gapPercentTextBox_Leave(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            buserCheck = false;
            NAxis axis = RetrieveAxis();
            List<float> tmpList = gf.getListGapPercent();
            if (tmpList.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                    tmpList.Add(0.0f);
            }
            float perc = 0.0f;
            if (!float.TryParse(gapPercentTextBox.Text, out perc))
                perc = 0.0f;
            tmpList[cb_ChartPosition.SelectedIndex] = perc;
            gf.setListGapPercent(tmpList);
            gPD();
            ///on actualise l'affichage
            UpdateDisplay();

        }

        private void barWidthPercentageTextBox_Leave(object sender, EventArgs e)
        {
            if (!buserCheck)
                return;
            buserCheck = false;
            NAxis axis = RetrieveAxis();
            List<float> tmpList = gf.getListBarWidthPercent();
            if (tmpList.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                    tmpList.Add(0.0f);
            }
            float perc = 0.0f;
            if (!float.TryParse(barWidthPercentageTextBox.Text, out perc))
                perc = 0.0f;
            tmpList[cb_ChartPosition.SelectedIndex] = perc;
            gf.setListBarWidthPercent(tmpList);
            gPD();
            ///on actualise l'affichage
            UpdateDisplay();
        }

        private void resetAxisButton_Click(object sender, EventArgs e)
        {
            List<List<GraphicFilter.Axe>> allChartsAxesLists = gf.getAxisList();

            GraphicFilter.Axe initialAxe = initialAxisList[cb_ChartPosition.SelectedIndex][cb_ChartAxis.SelectedIndex];
            
            allChartsAxesLists[cb_ChartPosition.SelectedIndex][cb_ChartAxis.SelectedIndex] = initialAxe.clone();

            gPD();
            ///on actualise l'affichage
            UpdateDisplay();
        }


    }
}