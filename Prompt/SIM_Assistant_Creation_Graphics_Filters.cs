using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Nevron.Editors;
using Nevron.GraphicsCore;
using Nevron.Chart;
using SIMCORE_TOOL.com.crispico.charts;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Assistant_Creation_Graphics_Filters : Form
    {
        #region La classe ListeItem utilisée pour actualiser l'affichage de la liste.
        private class ListeItem
        {
            private Object Name__;
            private bool Selected__;
            public bool Selected
            {
                get
                {
                    return Selected__;
                }
                set
                {
                    Selected__ = value;
                }
            }
            public Object Name
            {
                get
                {
                    return Name__;
                }
                set
                {
                    Name__ = value;
                }
            }
            public ListeItem(Object Name_)
            {
                Name__ = Name_;
                Selected__ = false;
            }
            public ListeItem(Object Name_, bool Selected_)
            {
                Name__ = Name_;
                Selected__ = Selected_;
            }
            public override string ToString()
            {
                if (Selected)
                    return Name.ToString() + "*";
                return Name.ToString();
            }
        }
        #endregion

        #region Les différentes variables de la fenêtre.

        private ArrayList ColumnNames; //Les noms des colonnes à afficher.

        private ArrayList OriginColumn; //Les noms des colonnes à afficher.

        private ArrayList laRepresentation; // Le type de représentation qui doit être utilisé dans le graphique.

        private ArrayList lAxeDeRepresentation; // L'axe sur lequel devra être lié la courbe.

        private ArrayList lesCouleursTour; //Les couleurs par défault des courbes

        private ArrayList lesCouleursRemplissage; //Les couleurs par défault des courbes

        private ArrayList lesAccumulations; //Contient un tableau de booléen indiquant si il y a une accumulation.

        private ArrayList lesPositions; //Position du graphique dans la zone.

        private List<Boolean> lesPositionsXScrollbar; //Position du graphique dans la zone.

        private ArrayList lesShowValues; //Position du graphique dans la zone.
                
        private List<double> listSetPointValue;    // list containing the values of Setpoints

        private List<double> listSetPointValue2;    // list containing the values of Setpoints2

        private List<DateTime> listSetPointBDateTime;    // list containing the values of Setpoints

        private List<DateTime> listSetPointEDateTime;    // list containing the values of Setpoints2

        private List<GraphicFilter.Notes> ListAnnotation; // list containing the annotations

        private List<List<GraphicFilter.Axe>> AxisList; // list containing the Axis scale

        private ArrayList listSetPointStrokeColor;   // list containing the colors of Setpoints or Setpoint Area when Setpoint2 is checked

        private ArrayList listSetPointFillColor;   // list containing the Stroke colors of Setpoints2

        private List<Boolean> listSetPointIsArea;    // list indicating whether a Setpoint is an area or not

        private List<String> listSetPointAxis;    // list indicating whether a Setpoint is an area or not

        private List<Boolean> listSetPointIsActivated;    // list indicating whether a Setpoint is an area or not

        private DataTable laTable; //La table d'origine afin de pouvoir accéder aux noms des colonnes

        private int CurrentColumn;

        private GestionDonneesHUB2SIM DonneesEnCours;

        private TreeNodeCollection NodesArbre;

        private String SelectedColumn;

        private bool bUserCheck; /// Booleen permettant d'éviter l'appel de fonctions lors de changement d'etat de variables ou de champs

        private String sXTitle;
        private String sYTitle;
        private String sY2Title;

        private SIM_Setpoint setpointWindow;  // Class for the oppening of the setpoint window

        private SIM_ColumnPosition ColumnPosition;

        private ImageList imgListTreeView_;

        //Candle variables
        private ArrayList CandleColumnsList; /// Liste contenant les noms des trois courbes servant a faire le candleList
        /// Valeur maximum, minimim et moyenne du Candle calculée grace aux données de la meme colonne
        private ArrayList MaxCandleValue;    
        private ArrayList MidCandleValue;    
        private ArrayList MinCandleValue;

        private List<float> lesPositionsBarWidth;
        private List<float> lesPositionsGapPercent;
        private List<float> lesPositionsBarWidthPercent;

        private List<SetPoint> setPoints = new List<SetPoint>();    // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
        #endregion

        #region les différents constructeurs de la classe.
        private void InitialiserFenetre(GestionDonneesHUB2SIM DonneesEnCours_, GraphicFilter filtre, DataTable table_, ImageList imgListTreeView
            , String scenarioName)  // << Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName
        {
            InitializeComponent();

            imgListTreeView_ = imgListTreeView;
            DonneesEnCours = DonneesEnCours_;
            bUserCheck = false;
            txt_FilterName.BackColor = Color.White;
            txt_Title.BackColor = Color.White;
            laTable = table_;
            txt_FilterName.Text = "";
            txt_Title.Text = "";

            setpointWindow = new Prompt.SIM_Setpoint();

            CurrentColumn = -1;
            //Définition du fond d'écran
            OverallTools.FonctionUtiles.MajBackground(this);
            // << Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName            
            if (scenarioName != null && scenarioName.Length > 0 && filtre == null && scenarioName != "Input")
                txt_Title.Text = scenarioName + " - ";             
            // >> Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName
            if (laTable != null)
            {
                // Initialisation de la liste des noms de colonnes.
                for (int i = 0; i < laTable.Columns.Count; i++)
                {
                    clb_ColumnsToShow.Items.Add(new ListeItem(laTable.Columns[i].ColumnName));
                }

                txt_FilterName.Text = laTable.TableName;
                btn_DeleteAllColumns.Enabled = false;
            }
            else
            {
                btn_HideAll.Enabled = false;
                btn_DisplayAll.Enabled = false;
            }


            if (filtre != null)
            {
                if (filtre.getListColumnsNames() != null)
                    ColumnNames = (ArrayList)filtre.getListColumnsNames().Clone();
                else
                    ColumnNames = new ArrayList();

                if (filtre.getlistColumnsOrigin() != null)
                    OriginColumn = (ArrayList)filtre.getlistColumnsOrigin().Clone();
                else
                    OriginColumn = new ArrayList();

                if (filtre.getListVizualisation() != null)
                    laRepresentation = (ArrayList)filtre.getListVizualisation().Clone();
                else
                    laRepresentation = new ArrayList();

                if (filtre.getListAxeRepresentation() != null)
                    lAxeDeRepresentation = (ArrayList)filtre.getListAxeRepresentation().Clone();
                else
                    lAxeDeRepresentation = new ArrayList();

                if (filtre.getListStrokeCouleurs() != null)
                    lesCouleursTour = (ArrayList)filtre.getListStrokeCouleurs().Clone();
                else
                    lesCouleursTour = new ArrayList();

                if (filtre.getListFillCouleurs() != null)
                    lesCouleursRemplissage = (ArrayList)filtre.getListFillCouleurs().Clone();
                else
                    lesCouleursRemplissage = new ArrayList();

                if (filtre.getListAccumulation() != null)
                    lesAccumulations = (ArrayList)filtre.getListAccumulation().Clone();
                else
                    lesAccumulations = new ArrayList();

                if (filtre.getListPositions() != null)
                    lesPositions = (ArrayList)filtre.getListPositions().Clone();
                else
                    lesPositions = new ArrayList();

                if (filtre.getListXScrollbar() != null)
                    lesPositionsXScrollbar = new List<Boolean>(filtre.getListXScrollbar());
                else
                    lesPositionsXScrollbar = new List<Boolean>();

                if (filtre.getListBarWidth() != null)
                    lesPositionsBarWidth = new List<float>(filtre.getListBarWidth());
                else
                {
                    lesPositionsBarWidth = new List<float>();
                    lesPositionsBarWidth.Add(0.0f);
                    lesPositionsBarWidth.Add(0.0f);
                    lesPositionsBarWidth.Add(0.0f);
                    lesPositionsBarWidth.Add(0.0f);
                }
                if (filtre.getListGapPercent() != null)
                    lesPositionsGapPercent = new List<float>(filtre.getListGapPercent());
                else
                {
                    lesPositionsGapPercent = new List<float>();
                    lesPositionsGapPercent.Add(0.0f);
                    lesPositionsGapPercent.Add(0.0f);
                    lesPositionsGapPercent.Add(0.0f);
                    lesPositionsGapPercent.Add(0.0f);
                }
                if (filtre.getListBarWidthPercent() != null)
                    lesPositionsBarWidthPercent = new List<float>(filtre.getListBarWidthPercent());
                else
                {
                    lesPositionsBarWidthPercent = new List<float>();
                    lesPositionsBarWidthPercent.Add(0.0f);
                    lesPositionsBarWidthPercent.Add(0.0f);
                    lesPositionsBarWidthPercent.Add(0.0f);
                    lesPositionsBarWidthPercent.Add(0.0f);
                }

                
                if (filtre.getListShowValues() != null)
                    lesShowValues = (ArrayList)filtre.getListShowValues().Clone();
                else
                    lesShowValues = new ArrayList();
                                
                if (filtre.getListIsArea() != null)
                    listSetPointIsArea = new List<Boolean>(filtre.getListIsArea());
                else
                    listSetPointIsArea = new List<Boolean>();

                if (filtre.getListIsActivated() != null)
                    listSetPointIsActivated = new List<Boolean>(filtre.getListIsActivated());
                else
                    listSetPointIsActivated = new List<Boolean>();

                if (filtre.getListSetPointAxis() != null)
                    listSetPointAxis = new List<String>(filtre.getListSetPointAxis());
                else
                    listSetPointAxis = new List<String>();

                if (filtre.getListSetPointStrokeColor() != null)
                    listSetPointStrokeColor = (ArrayList)filtre.getListSetPointStrokeColor().Clone();
                else
                    listSetPointStrokeColor = new ArrayList();

                if (filtre.getListSetPointFillColor() != null)
                    listSetPointFillColor = (ArrayList)filtre.getListSetPointFillColor().Clone();
                else
                    listSetPointFillColor = new ArrayList();

                if (filtre.getListSetPointValue() != null)
                    listSetPointValue = new List<double>(filtre.getListSetPointValue());
                else
                    listSetPointValue = new List<double>();

                if (filtre.getListSetPointValue2() != null)
                    listSetPointValue2 = new List<double>(filtre.getListSetPointValue2());
                else
                    listSetPointValue2 = new List<double>();

                if (filtre.getListSetPointBDateTime() != null)
                    listSetPointBDateTime = new List<DateTime>(filtre.getListSetPointBDateTime());
                else
                    listSetPointBDateTime = new List<DateTime>();

                if (filtre.getListSetPointEDateTime() != null)
                    listSetPointEDateTime = new List<DateTime>(filtre.getListSetPointEDateTime());
                else
                    listSetPointEDateTime = new List<DateTime>();

                if (filtre.setPoints != null)
                    setPoints = new List<SetPoint>(filtre.setPoints);
                else
                    setPoints = new List<SetPoint>();

                if (filtre.getListAnnotation() != null)
                    ListAnnotation = new List<GraphicFilter.Notes>(filtre.getListAnnotation());
                else
                    ListAnnotation = new List<GraphicFilter.Notes>();

                if (filtre.getAxisList() != null)
                    AxisList = new List<List<GraphicFilter.Axe>>(filtre.getAxisList());

                int setpointActivated = 0;

                if (filtre.getCandleColumnsList() != null)
                    CandleColumnsList = (ArrayList)filtre.getCandleColumnsList().Clone();
                else
                    CandleColumnsList = new ArrayList();

                if (filtre.getMidCandleValue() != null)
                    MidCandleValue = (ArrayList)filtre.getMidCandleValue().Clone();
                else
                    MidCandleValue = new ArrayList();

                if (filtre.getMinCandleValue() != null)
                    MinCandleValue = (ArrayList)filtre.getMinCandleValue().Clone();
                else
                    MinCandleValue = new ArrayList();

                if (filtre.getMaxCandleValue() != null)
                    MaxCandleValue = (ArrayList)filtre.getMaxCandleValue().Clone();
                else
                    MaxCandleValue = new ArrayList();

                //If there is at least one activated setpoint, set the Setpoint Icon to checked state
                for (int j = 0; j < listSetPointValue.Count; j++)
                    if (listSetPointIsActivated[j])
                    {
                        changeBtn_SetpointFont(new Font("Microsoft Sans Serif", 8, FontStyle.Bold));
                        setpointActivated = 1;
                    }
                ///Si aucun setpoint n'est activé alors on laisse l'état normal de l'icone
                if (setpointActivated == 0)
                    changeBtn_SetpointFont(new Font("Microsoft Sans Serif", 8, FontStyle.Regular));



                CB_InvertAbscisse.Checked = filtre.InvertAbscisse;
                CheckInvertAbscisse();


                sXTitle = filtre.XTitle;
                sYTitle = filtre.YTitle;
                sY2Title = filtre.Y2Title;

                cbUseScenarioNameInTitle.Checked = filtre.useScenarioNameInTitle;   // << Task #9624 Pax2Sim - Charts - checkBox for scenario name                

                // << Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName
                if (scenarioName != null && scenarioName.Length > 0 && scenarioName != "Input"
                    && filtre.Title != null && !filtre.Title.Contains(scenarioName)
                    && filtre.useScenarioNameInTitle)   // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
                {
                    txt_Title.Text = scenarioName + " - " + filtre.Title;
                }
                else
                    txt_Title.Text = filtre.Title;
                // >> Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName
                                
                txt_FilterName.Text = filtre.Name;
                if (DonneesEnCours == null)
                {
                    for (int i = 0; i < clb_ColumnsToShow.Items.Count; i++)
                    {
                        if (indexColumn(clb_ColumnsToShow.Items[i].ToString()) != -1)
                        {
                            ((ListeItem)clb_ColumnsToShow.Items[i]).Selected = true;
                        }
                    }
                }
                else
                {
                    cb_DisplayColumn.Enabled = false;
                    for (int i = 0; i < OriginColumn.Count; i++)
                    {
                        clb_ColumnsToShow.Items.Add(new ListeItem(OriginColumn[i], true));
                    }
                }
            }
            else
            {
                //Initialisation des arraylist.
                laRepresentation = new ArrayList();
                OriginColumn = new ArrayList();
                lAxeDeRepresentation = new ArrayList();
                lesShowValues = new ArrayList();
                lesCouleursTour = new ArrayList();
                lesCouleursRemplissage = new ArrayList();
                ColumnNames = new ArrayList();
                lesAccumulations = new ArrayList();
                lesPositions = new ArrayList();
                lesPositionsXScrollbar = new List<Boolean>();
                lesShowValues = new ArrayList();

                setPoints = new List<SetPoint>();
                listSetPointStrokeColor = new ArrayList();
                listSetPointFillColor = new ArrayList();
                listSetPointValue = new List<double>();
                listSetPointValue2 = new List<double>();
                listSetPointBDateTime = new List<DateTime>();
                listSetPointEDateTime = new List<DateTime>();
                listSetPointIsArea = new List<Boolean>();
                listSetPointAxis = new List<string>();
                listSetPointIsActivated = new List<Boolean>();
                ListAnnotation = new List<GraphicFilter.Notes>();
                CandleColumnsList = new ArrayList();
                MaxCandleValue = new ArrayList();
                MidCandleValue = new ArrayList();
                MinCandleValue = new ArrayList();
                AxisList = new List<List<GraphicFilter.Axe>>();


                sXTitle = "";
                sYTitle = "";
                sY2Title = "";

                lesPositionsBarWidth = new List<float>();
                lesPositionsBarWidth.Add(0.0f);
                lesPositionsBarWidth.Add(0.0f);
                lesPositionsBarWidth.Add(0.0f);
                lesPositionsBarWidth.Add(0.0f);

                lesPositionsGapPercent = new List<float>();
                lesPositionsGapPercent.Add(0.0f);
                lesPositionsGapPercent.Add(0.0f);
                lesPositionsGapPercent.Add(0.0f);
                lesPositionsGapPercent.Add(0.0f);

                lesPositionsBarWidthPercent = new List<float>();
                lesPositionsBarWidthPercent.Add(0.0f);
                lesPositionsBarWidthPercent.Add(0.0f);
                lesPositionsBarWidthPercent.Add(0.0f);
                lesPositionsBarWidthPercent.Add(0.0f);
            }
            if ((filtre == null) || (filtre.getAxisList() == null))
            {
                ///On initialise ici 4 listes de 3 axes avec des valeurs par défaut
                AxisList = new List<List<SIMCORE_TOOL.GraphicFilter.Axe>>();
                for (int i = 0; i < 4; i++)
                {
                    List<SIMCORE_TOOL.GraphicFilter.Axe> axis = new List<SIMCORE_TOOL.GraphicFilter.Axe>();
                    for (int j = 0; j < 3; j++)
                    {
                        SIMCORE_TOOL.GraphicFilter.Axe axe = new SIMCORE_TOOL.GraphicFilter.Axe();
                        axe.BeginDTValue = new DateTime(1, 1, 1);
                        axe.BeginNValue = -1;
                        axe.IsDateTime = false;
                        axe.LengthDTValue = -1;
                        axe.LengthNValue = -1;
                        axis.Add(axe);
                    }
                    AxisList.Add(axis);
                }
            }

            ///On initialise la liste de scrollbar, 4 Xscrollbar par graphique car 4 positions
            if (lesPositionsXScrollbar.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    lesPositionsXScrollbar.Add(false);
                }
            }
            //bar width per chart (we have 4 chart positions)
            if (lesPositionsBarWidth.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    lesPositionsBarWidth.Add(0.0f);
                }
            }
            if (lesPositionsGapPercent.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    lesPositionsGapPercent.Add(0.0f);
                }
            }
            if (lesPositionsBarWidthPercent.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    lesPositionsBarWidthPercent.Add(0.0f);
                }
            }

            txt_FilterName.Enabled = false;
            lbl_FilterName.Enabled = false;
            UpdateDisplay();
            bUserCheck = true;

            if (DonneesEnCours != null)
            {
                //Il faut mettre en place tout le fonctionnement pour l'ajout de filtre sur plusieurs tables.

                clb_ColumnsToShow.Size = new Size(clb_ColumnsToShow.Size.Width, clb_ColumnsToShow.Size.Height - 26);
                btn_Add_Column.Visible = true;
                btn_Edit.Visible = true;
                btn_DeleteColumn.Visible = true;
                if (filtre == null)
                {
                    txt_FilterName.Enabled = true;
                    lbl_FilterName.Enabled = true;
                    lbl_FilterName.Text = "Name";
                }
            }

            ///Check si le chart est custom ou non. Si ou on ne garde que les axes Y et Y2, sinon on propose X,Y et Y2
            if (DonneesEnCours != null)
            {
                cb_Axis.Items.Clear();
                cb_Axis.Items.AddRange(new String[] {"Y","Y2"});
            }
            else
            {
                cb_Axis.Items.Clear();
                cb_Axis.Items.AddRange(new String[] { "X", "Y", "Y2" });
            }


            ColumnPosition = new SIM_ColumnPosition();

        }

        internal SIM_Assistant_Creation_Graphics_Filters(GraphicFilter filtre, DataTable table_, ImageList imgListTreeView
            , String scenarioName)  // << Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName
        {
            // << Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName            
            InitialiserFenetre(null, filtre, table_, imgListTreeView, scenarioName);
            // >> Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName
        }

        internal SIM_Assistant_Creation_Graphics_Filters(GestionDonneesHUB2SIM DonneesEnCours_, TreeNodeCollection NodesArbre_, GraphicFilter filtre, ImageList imgListTreeView)
        {
            // << Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName            
            InitialiserFenetre(DonneesEnCours_, filtre, null, imgListTreeView, "");
            // >> Task #8319 Pax2Sim - Pax2Sim - Charts - Title format: ScenarioName - ChartName
            NodesArbre = NodesArbre_;
        }
        #endregion

        #region Fonction pour récupérer le filtre créer dans la fenêtre.
        internal GraphicFilter getFilter()
        {
            GraphicFilter gf = new GraphicFilter(ColumnNames, OriginColumn, laRepresentation, lAxeDeRepresentation, lesCouleursTour, lesCouleursRemplissage,
                lesAccumulations, listSetPointAxis, listSetPointIsArea, listSetPointStrokeColor, listSetPointFillColor, listSetPointValue, listSetPointValue2,
                listSetPointBDateTime, listSetPointEDateTime, listSetPointIsActivated, ListAnnotation, CandleColumnsList, MaxCandleValue, MidCandleValue, MinCandleValue,
                AxisList, lesPositions, lesPositionsXScrollbar, lesShowValues, txt_FilterName.Text, txt_Title.Text, CB_InvertAbscisse.Checked,
                sXTitle, sYTitle, sY2Title, cbUseScenarioNameInTitle.Checked, lesPositionsBarWidth, lesPositionsGapPercent, lesPositionsBarWidthPercent, setPoints);  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name            
            return gf;
        }
        #endregion

        #region Gestion des différents boutons de la fenêtre.
        private void btn_Ok_Click(object sender, EventArgs e)
        {

            int SelectedItemIndex;

            if (((ListeItem)clb_ColumnsToShow.SelectedItem) == null)
                SelectedItemIndex = listSetPointValue.Count - 1;
            else
                SelectedItemIndex = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());


            ///Si aucune colonne n'est présente dans la liste clb_ColumnsToShow on ne peut rien afficher
            if ((ColumnNames.Count == 0) || ((ColumnNames.Count == 1) && (lAxeDeRepresentation[0].ToString() == "X")))
            {
                MessageBox.Show("Select one column.");
                DialogResult = DialogResult.None;
                return;
            }
            ///Si le nom du filtre existe déja
            if ((DonneesEnCours != null) && (txt_FilterName.Enabled))
            {
                GraphicFilter gf = DonneesEnCours.GetGeneralGraphicFilter(txt_FilterName.Text);
                if(gf== null)
                    gf = DonneesEnCours.GetGraphicFilter(txt_FilterName.Text);
                if (gf != null)
                {
                    MessageBox.Show("The filter name already exist, please change the name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            ///Si aucun nom n'a été passé
            if (txt_FilterName.Text.CompareTo("") == 0)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Please indicate a name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string illegalChar = "";
            if (filterNameHasIllegalChars(txt_FilterName.Text, out illegalChar))
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("The name should not contain the character \"" + illegalChar + "\"." 
                    + Environment.NewLine + " Please change the name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private bool filterNameHasIllegalChars(string filterName, out string illegalChar)
        {
            illegalChar = "";
            foreach (string definedIllegalChar in GlobalNames.ILLEGAL_CHARACTERS_FOR_FILTER_NAME)
            {
                if (filterName.Contains(definedIllegalChar))
                {
                    illegalChar = definedIllegalChar;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Les différentes fonctions qui s'occupent de gérer les listes de noms de colonne.

        /// <summary>
        /// S'occupe de gérer l'affichage des informations concernant la colonne sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clb_ColumnsToShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            if (!bUserCheck)
                return;
            bUserCheck = false;
            CurrentColumn = clb_ColumnsToShow.SelectedIndex;

            gb_ChartType.Text = "Column";
            if (CurrentColumn == -1)
            {
                gb_ChartType.Enabled = false;
                bUserCheck = true;
            }
            else
            {
                gb_ChartType.Enabled = true;
                ///Si la colonne selectionnée est affichée
                if (((ListeItem)clb_ColumnsToShow.SelectedItem).Selected)
                {
                    cb_DisplayColumn.Checked = true;
                    index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                    if (index != -1)
                    {
                        ///On affiche les données correspondantes à la colonne selectionnée
                        DisplaySettings(index);
                        ///On affiche son nom dans la partie titre du groupe Box et on active la partie settings
                        SelectedColumn = ((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString();
                        gb_ChartType.Enabled = true;
                        gb_ChartType.Text += " " + SelectedColumn;

                        int isCandle = 0;   /// Let us know if the selected column is included among the 3 candle columns
                        ///Si une candle list est utilisé et que la colonne selectionné fait partie des 3 colonnes de candle.
                        for (int i = 0; i < CandleColumnsList.Count; i++)
                        {
                            ///On regarde si la colonne selectionnée est contenue dans la candlelist
                            if (ColumnNames[index].ToString().CompareTo(CandleColumnsList[i].ToString()) == 0)
                            {
                                isCandle = 1;
                                ///Si la colonne n'est pas la colonne de définition alors grise la partie settings
                                if (i > 0)
                                {
                                    gb_ChartType.Enabled = false;
                                    cb_ChartType.SelectedItem = cb_ChartType.Items[8];
                                }
                                ///Si la colonne est la colonne de définition alors on active la partie settings, on affiche la GroupeBox candleInformations    
                                else
                                {
                                    gb_ChartType.Enabled = true;
                                    cb_ChartType.SelectedItem = cb_ChartType.Items[8];
                                    gb_Candle.Enabled = true;
                                }
 
                                ///Appeler la fonction de sélection du chart type pour changer les listes contenues dans CandleInformation pourqu'elles contiennent les noms des colonnes disponible
                                setCandleListInformations();

                                ///On affiche dans les listes colonnes selectionnées comme contenues dans les candle lists
                                cb_MaxValue.Text = CandleColumnsList[0].ToString();
                                cb_MiddleValue.Text = CandleColumnsList[1].ToString();
                                cb_MinValue.Text = CandleColumnsList[2].ToString();

                                ///On sort apres le traitement
                                i = CandleColumnsList.Count;
                            }
                        }
                        //If the selected column is not a candle Column,isCandle = 0
                        if (isCandle == 0)
                        {
                            ///Si le type de la courbe est une Candle
                            if (laRepresentation[index].ToString().CompareTo("Candle") == 0)
                            {
                                ///On desactive automatiquement la possibilité d'utiliser une scrollbar
                                chb_Scrollbar.Checked = false;

                                ///Appeler la fonction de sélection du chart type pour changer les listes contenues dans CandleInformation et qu'elles contiennent les différents types de Max Min et Midlle Point voulut
                                setCandleInformations();
                            }
                        }
                    }
                }
                else
                {
                    cb_DisplayColumn.Checked = false;
                }
                UpdateDisplay();
                bUserCheck = true;
            }
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

        #region La gestion des options du menu pour la liste des noms de colonne.
        /// <summary>
        /// Changement d'état de l'affichage d'une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_DisplayColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;

            bUserCheck = false;
            gb_Candle.Enabled = false;
            ///On créé une liste d'item temporaire récupérant la colonne selectionnée
            ListeItem tmp = (ListeItem)clb_ColumnsToShow.Items[clb_ColumnsToShow.SelectedIndex];
            ///On change l'état de la colonne suivant si on désire l'afficher ou la cacher 
            tmp.Selected = cb_DisplayColumn.Checked;
            clb_ColumnsToShow.Items[clb_ColumnsToShow.SelectedIndex] = tmp;
            ///On affiche la colonne
            if (cb_DisplayColumn.Checked)
            {
                ///On récupère l'index de la colonne dans les listes grace à son nom dans la liste clb_ColumnsToShow
                int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                if (index != -1)
                {
                    bUserCheck = true;
                    return;
                }
                ///On l'ajoute dans la liste des noms de colonnes disponibles
                ColumnNames.Add(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                if (((ListeItem)clb_ColumnsToShow.SelectedItem).Name.GetType() == typeof(ColumnInformation))
                {
                    ///On l'ajoute également dans la liste des colonnes origine
                    OriginColumn.Add(((ListeItem)clb_ColumnsToShow.SelectedItem).Name);
                }
                else
                {
                    OriginColumn.Add("");
                }
                ///On récupère la position dans le graphique et on l'ajoute dans la liste lesPositions
                int Position = indexOfCartesianChart();
                if (Position == -1)
                {
                    lesPositions.Add(0);
                    DeleteAllChart(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString(), 0);
                }
                else
                    lesPositions.Add(Position);
                ///Par défaut on n'affiche pas les valeurs de la courbe sur le graphique
                lesShowValues.Add(false);
                
                ///Type ligne
                laRepresentation.Add("Line");
                ///Axe Y
                lAxeDeRepresentation.Add("Y");
                ///On n'utilise donc pas le type Candle donc on ne met rien dans les listes
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
                ///Pas d'accumulation
                lesAccumulations.Add(Color.Transparent);
                ///On récupère la couleur de la courbe avec getUnusedColor qui va choisir une couleur non utilisée
                int iColor = OverallTools.FonctionUtiles.getUnusedColor(lesCouleursRemplissage);
                lesCouleursTour.Add(new NStrokeStyle(OverallTools.FonctionUtiles.getColorStroke(iColor)));
                lesCouleursRemplissage.Add(new NColorFillStyle(OverallTools.FonctionUtiles.getColor(iColor)));

                if (ColumnNames.Count > 0)
                    DisplaySettings(ColumnNames.Count - 1);

                index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                if (index == -1)
                    return;

                moveColonneOrder(index);
                
            }
            /// On cache la colonne
            else
            {
                int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                if (index != -1)
                {
                    ///Si la colonne a cacher faisait parti d'une CandleColumnsList
                    if (CandleColumnsList.Contains(ColumnNames[index].ToString()))
                    {
                        int i, j, k;
                        ///On supprime toutes les colonnes qui etaient presentes dans la candleList
                        for (k = 0; k < CandleColumnsList.Count; k++)//(k = 1; k < CandleColumnsList.Count; k++)    // Task #16728 - C#42.4
                        {
                            for (i = 0; i < ColumnNames.Count; i++)
                                if (ColumnNames[i].ToString() == CandleColumnsList[k].ToString())
                                    break;
                            if (i == ColumnNames.Count)
                                i--;

                            tmp = null;
                            ///on recupere la colonne enlevée
                            for (j = 0; j < clb_ColumnsToShow.Items.Count; j++)
                            {
                                tmp = (ListeItem)clb_ColumnsToShow.Items[j];
                                if (tmp.Name.ToString() == ColumnNames[i].ToString())
                                    break;
                            }
                            ///Si la colonne faisait partie de la candle List mais qu'elle a été changée, on remet cette colonne en ligne
                            if (laRepresentation[i].ToString() == "Candle list")
                            {
                                tmp.Selected = false;
                                clb_ColumnsToShow.Items[j] = tmp;
                                deleteChart(i);
                            }
                        }
                        ///On supprime les colonnes qui étaient dans la CandleColumnList
                        for (i = 0; i < CandleColumnsList.Count; i++)
                        {
                            if (CandleColumnsList[i].ToString().CompareTo(clb_ColumnsToShow.SelectedItem.ToString()) == 0)
                                CandleColumnsList.Clear();
                        }
                    }
                    else    // Task #16728 - C#42.4 (added the else for the call to deleteChart because the Candle list chart deletion is handled in the if branch)
                    {
                        deleteChart(index);
                    }
                    CheckInvertAbscisse();
                }
            }
            UpdateDisplay();
            bUserCheck = true;
        }

        /// <summary>
        /// Mettre a jour l'affichage des differents composants de la fenetre suivant les données
        /// </summary>
        private void UpdateDisplay()
        {
            ///Vrai si la colonne est sélectionée
            bool bDisplayed = (cb_DisplayColumn.Checked);
            ///Vrai si la colonne n'est pas a afficher sur l'axe X
            bool bEnabled = !(cb_Axis.Text == "X");
            ///Vrai si le chart est de type candle ou candle list
            bool bCandle = cb_ChartType.Text == "Candle" || cb_ChartType.Text == "Candle list";

            lbl_Axis.Enabled = bDisplayed;
            cb_Axis.Enabled = bDisplayed;

            ///On affiche le groupeBox candle informations uniquement si le chart est de type Candle ou Candle List
            gb_Candle.Enabled = bDisplayed && bEnabled && bCandle;
            label1.Enabled = cb_ChartType.Enabled = bDisplayed && bEnabled;

            //ScrollBar Checkbox is automaticly disabled if we use a Candle
            chb_Scrollbar.Enabled = bDisplayed && bEnabled && !(cb_ChartType.Text == "Candle");

            cb_Position.Enabled = bDisplayed && bEnabled;
            lbl_Position.Enabled = bDisplayed && bEnabled;

            lbl_StrokeColor.Enabled = bDisplayed && bEnabled && (cb_ChartType.Text != "Pie") && !bCandle;
            pb_StrokeColor.Enabled = bDisplayed && bEnabled && (cb_ChartType.Text != "Pie") && !bCandle;

            bool bFillColor = ((cb_ChartType.Text == "Bar")
                                         || (cb_ChartType.Text == "Area")
                                         || (cb_ChartType.Text == "Area stacked")
                                         || (cb_ChartType.Text == "Bar stacked"));
            lbl_FillColor.Enabled = bDisplayed && bEnabled && bFillColor && !bCandle ;
            pb_FillColor.Enabled = bDisplayed && bEnabled && (bFillColor) && !bCandle;
            cb_ShowValues.Enabled = bDisplayed && bEnabled && (bFillColor);
            barWidthGroupBox.Enabled = bDisplayed && bEnabled && (bFillColor);//barWidthPercentageTextBox.Enabled = bDisplayed && bEnabled && (bFillColor);

            cb_AddCumul.Enabled = bDisplayed && bEnabled && (cb_ChartType.Text != "Pie") && !bCandle;
            pb_LineAccumulationColor.Enabled = cb_AddCumul.Enabled && cb_AddCumul.Checked && !bCandle;
        }
        #endregion

        #region Les fonctions qui se chargent d'effectuer des changements lors de changement d'états de certains objets
        private void AxeBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;
            if (cb_Axis.Text == "X")
            {
                for (int i = 0; i < lAxeDeRepresentation.Count; i++)
                {
                    if (lAxeDeRepresentation[i].ToString() == "X")
                    {
                        //Il y avait déjà un axe des abscisses, on le supprime.
                        ListeItem tmp = null;
                        int j;
                        for (j = 0; j < clb_ColumnsToShow.Items.Count; j++)
                        {
                            tmp = (ListeItem)clb_ColumnsToShow.Items[j];
                            if (tmp.Name.ToString() == ColumnNames[i].ToString())
                                break;
                        }
                        if (j != clb_ColumnsToShow.Items.Count)
                        {
                            bUserCheck = false;
                            tmp.Selected = false;
                            clb_ColumnsToShow.Items[j] = tmp;
                            bUserCheck = true;
                        }
                        deleteChart(i);
                        break;
                    }
                }
            }

            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
                return;
            lAxeDeRepresentation[index] = cb_Axis.Text;
            CheckInvertAbscisse();
            UpdateDisplay();
        }

        /// <summary>
        /// Appelée à chaque changement de chart type, permet d'adapter les informations liées au choix de type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_ChartType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!bUserCheck)
                return;
            bUserCheck = false;

            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
                return;

            ///Mettre à jour la liste des types de chart en remplacant avec le type choisit
            laRepresentation[index] = cb_ChartType.Text;

            ///Si le chart est de type Pie alors on desactive par defaut toutes les autres colonnes
            if (!CheckPositionsAttribution())
            {
                DeleteAllChart(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString(), (int)lesPositions[index]);
            }

            if (!laRepresentation.Contains("Pie"))
            {
                ///Si le type choisit est de type candle ou candle list
                if (cb_ChartType.SelectedItem.ToString().CompareTo("Candle") == 0 || cb_ChartType.SelectedItem.ToString().CompareTo("Candle list") == 0)
                {

                    ///If we click candle chart type
                    if (cb_ChartType.SelectedItem.ToString().CompareTo("Candle") == 0)
                    {
                        int positionToCompare = (int)lesPositions[index];

                        Boolean positionfound = false;
                        //Boolean candleTmp = false;
                        ///Si le type choisit est un Candle alors l'afficher dans une autre position car l'axe des candle n'est pas compatible avec les axes datesTime...
                        for (int i = 0; i < laRepresentation.Count; i++)                        
                        {
                            /// Si le chart récupéré est de type Candle et que ce n'est pas le nouveau Candle que l'on souhaite créer
                            if (laRepresentation[i].ToString() == "Candle" && i != index)
                            {
                                ///On définit la position du nouveau Candle ajouté et on sort directement de la boucle
                                positionToCompare = (int)lesPositions[i];
                                positionfound = true;
                                i = laRepresentation.Count;                               
                            }
                        }
                        ///Si aucun Candle n'est déjà existant, on va chercher une position libre sur les 4 disponibles
                        if(positionfound == false)
                        {
                            ///On part de la position courante
                            positionToCompare = (int)lesPositions[index];
                            ///nbpassage sert à savoir si on a étudié tous les emplacements
                            int nbpassage = 0;

                            for (int i = 0; i < lesPositions.Count; i++)
                            {
                                if ((int)lesPositions[i] == positionToCompare && i != index && ColumnNames[i].ToString() != "Time" && laRepresentation[i].ToString() != "Candle")
                                {
                                    ///Si le chart position topLeft(0) est utilisée alors aller en en Bottom Left(2), sinon en Top Right(1) sinon Bottom Right(3)
                                    if (positionToCompare == 0)
                                        positionToCompare = 2;
                                    else if (positionToCompare == 2)
                                        positionToCompare = 1;
                                    else if (positionToCompare == 1)
                                        positionToCompare = 3;
                                    else if (positionToCompare == 3)
                                        positionToCompare = 0;
                                    ///Si tous les positions sont occupées alors ne pas afficher
                                    if (nbpassage == 4)
                                    {
                                        MessageBox.Show("Unable to add a candle, no position available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        ///On redéfinit le type par défault comme Line
                                        laRepresentation[index] = cb_ChartType.SelectedItem = "Line";
                                        bUserCheck = true;
                                        return;
                                    }
                                    i = 0;
                                    nbpassage++;
                                }
                            }
                        }

                        ///On emmene la colonne à la fin de la liste car les candles doivent toujours être en dernière position!
                        BringToEndList(index);

                        /// On récupère la nouvelle position
                        index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                        if (index == -1)
                            return;

                        ///Mettre à jour la liste des positions en ajoutant la nouvelle position de la courbe.
                        lesPositions[index] = cb_Position.SelectedIndex = positionToCompare;

                        ///Afficher l'etat de la scrollbar
                        chb_Scrollbar.Checked = lesPositionsXScrollbar[cb_Position.SelectedIndex];

                        barWidthTextBox.Text = lesPositionsBarWidth[cb_Position.SelectedIndex].ToString();
                        gapPercentTextBox.Text = lesPositionsGapPercent[cb_Position.SelectedIndex].ToString();
                        barWidthPercentageTextBox.Text = lesPositionsBarWidthPercent[cb_Position.SelectedIndex].ToString();

                        ///If the selected column was a candlelist and we changed it for another type, clear the CandleColumnsList
                        if (CandleColumnsList.Count != 0)
                            if (clb_ColumnsToShow.SelectedItem.ToString().Contains(CandleColumnsList[0].ToString()) == true)
                            {
                                for (int i = 0; i < ColumnNames.Count; i++)
                                {
                                    int indexCandle = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                                    // Si la courbe contenue dans candleColumnsList n'est pas la courbe déselectionnée alors on reaffecte le type de courbe en ligne
                                    if (CandleColumnsList.Contains(ColumnNames[i].ToString()) && ColumnNames[i].ToString() != CandleColumnsList[0].ToString())
                                        laRepresentation[i] = "Line";
                                }
                                CandleColumnsList.Clear();
                            }
                        setCandleInformations();
                    }
                    ///If we click candle list chart type
                    else
                    {
                        Boolean newCandleList = false;
                        for (int i = 0; i < CandleColumnsList.Count; i++)
                        {
                            newCandleList = true;
                            if (((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString() == CandleColumnsList[i].ToString())
                            {
                                newCandleList = false;
                                i = CandleColumnsList.Count;
                            }
                        }
                        ///Si le type de chart choisit est candleList mais qu'une candle list existe déja, on empèche la création d'une nouvelles.
                        if (newCandleList)
                        {
                            MessageBox.Show("A candle List is already defined", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            ///On remet alors le type du chart a ligne
                            laRepresentation[index] = cb_ChartType.Text = "line";
                        }
                        else
                        {
                            int positionToCompare = (int)lesPositions[index];

                            for (int i = 0; i < lesPositions.Count; i++)
                            {
                                ///On regarde si aucun Candle n'est présent à la position selectionnée
                                if ((int)lesPositions[i] == positionToCompare && i != index && ColumnNames[i].ToString() != "Time" && laRepresentation[i].ToString() == "Candle")
                                {
                                    if (positionToCompare == 0)
                                        positionToCompare = 2;
                                    else if (positionToCompare == 2)
                                        positionToCompare = 1;
                                    else if (positionToCompare == 1)
                                        positionToCompare = 3;
                                    else if (positionToCompare == 3)
                                        positionToCompare = 0;
                                    i = 0;
                                }
                            }
                            ///On fait en sorte que rien ne soit positionné après les candles si candle il y a
                            moveColonneOrder(index);

                            index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                            if (index == -1)
                                return;

                            ///Mettre à jour la liste des positions en ajoutant la nouvelle position de la courbe.
                            lesPositions[index] = cb_Position.SelectedIndex = positionToCompare;

                            ///On active la partie Candle settings et on affiche les valeurs par défault des candle list
                            setCandleListInformations();
                        }
                    }
                }
                else
                {
                    if (CandleColumnsList.Contains(ColumnNames[index].ToString()))
                    {
                        int i, j, k;
                        ///On supprime toutes les colonnes qui etaient presentes dans la candleList
                        for (k = 1; k < CandleColumnsList.Count; k++)
                        {
                            for (i = 0; i < ColumnNames.Count; i++)
                                if (ColumnNames[i].ToString() == CandleColumnsList[k].ToString())
                                    break;

                            ListeItem tmp = null;
                            ///on recupere la colonne enlevée
                            for (j = 0; j < clb_ColumnsToShow.Items.Count; j++)
                            {
                                tmp = (ListeItem)clb_ColumnsToShow.Items[j];
                                if (tmp.Name.ToString() == ColumnNames[i].ToString())
                                    break;
                            }
                            ///Si la colonne faisait partie de la candle List mais qu'elle a été changée, on remet cette colonne en ligne
                            if (laRepresentation[i].ToString() == "Candle list")
                            {
                                if (DonneesEnCours == null)
                                {
                                    tmp.Selected = false;
                                    clb_ColumnsToShow.Items[j] = tmp;
                                    deleteChart(i);
                                }
                                else
                                    laRepresentation[i] = "Line";
                            }
                        }
                        //If the selected column was a candlelist and we changed it for another type, clear the CandleColumnsList
                        if (CandleColumnsList.Count != 0)
                            if (clb_ColumnsToShow.SelectedItem.ToString().Contains(CandleColumnsList[0].ToString()) == true)
                                CandleColumnsList.Clear();
                    }

                    index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                    if (index == -1)
                        return;
                    int positionToCompare = (int)lesPositions[index];

                    for (int i = 0; i < lesPositions.Count; i++)
                    {
                        if ((int)lesPositions[i] == positionToCompare && i != index && ColumnNames[i].ToString() != "Time" && laRepresentation[i].ToString() == "Candle")
                        {
                            if (positionToCompare == 0)
                                positionToCompare = 2;
                            else if (positionToCompare == 2)
                                positionToCompare = 1;
                            else if (positionToCompare == 1)
                                positionToCompare = 3;
                            else if (positionToCompare == 3)
                                positionToCompare = 0;
                            i = 0;
                        }
                        
                    }

                    index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
                    if (index == -1)
                        return;

                    ///Mettre à jour la liste des positions en ajoutant la nouvelle position de la courbe.
                    lesPositions[index] = cb_Position.SelectedIndex = positionToCompare;
                    //cb_Position.SelectedIndex = (int)lesPositions[index];

                    ///Ne rien mettre dans les Max, Middle et Min list
                    cb_MaxValue.SelectedItem = MaxCandleValue[index] = "";
                    cb_MiddleValue.SelectedItem = MidCandleValue[index] = "";
                    cb_MinValue.SelectedItem = MinCandleValue[index] = "";

                    ///On fait en sorte que rien ne soit positionné après les candles si candle il y a
                    moveColonneOrder(index);
                }
            }
            UpdateDisplay();
            DisplaySettings(index);
            bUserCheck = true;
        }

        /// <summary>
        /// Afficher les changements correspondants au choix de position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
                return;

            ///Si la colonne selectionnée est de type candle on bride le changement de position
            if (laRepresentation[index].ToString() == "Candle")
            {
                MessageBox.Show("Unable to change the candle position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);             
                cb_ChartType_SelectedIndexChanged(sender, e);
                return;
            }
            /// Sinon on regarde si on souhaite déplacer un non Candle sur un candle ce qui est incompatible
            else
            {
                for (int i = 0; i < lesPositions.Count; i++)
                {
                    ///Si c'est le cas, on fournit un message d'avertissement à l'utilisateur
                    if (lesPositions[i].ToString() == cb_Position.SelectedIndex.ToString() && laRepresentation[i].ToString() == "Candle")
                    {
                        MessageBox.Show("Unable to change position, a candle chart is already there.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);             
                        cb_ChartType_SelectedIndexChanged(sender, e);
                        return;
                    }
                }
            }

            /// Si tout s'est bien déroulé, on définit la nouvelle position
            lesPositions[index] = cb_Position.SelectedIndex;

            barWidthTextBox.Text = lesPositionsBarWidth[(int)lesPositions[index]].ToString();
            gapPercentTextBox.Text = lesPositionsGapPercent[(int)lesPositions[index]].ToString();
            barWidthPercentageTextBox.Text = lesPositionsBarWidthPercent[(int)lesPositions[index]].ToString();

            ///Afficher l'etat de la scrollbar suivant le type de position choisit
            chb_Scrollbar.Checked = lesPositionsXScrollbar[(int)lesPositions[index]];
            if (!CheckPositionsAttribution())
            {
                DeleteAllChart(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString(), (int)lesPositions[index]);
            }
            UpdateDisplay();
        }
        #endregion

        #region Les fonctions pour choisir les couleurs des courbes.
        private void ColorChoose_Click(object sender, EventArgs e)
        {
            if (clb_ColumnsToShow.SelectedItem == null)
                return;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
                return;
            NStrokeStyle style = (NStrokeStyle)lesCouleursTour[index];
            if (NStrokeStyleTypeEditor.Edit(style, out style))
            {
                lesCouleursTour[index] = style;
                
                pb_StrokeColor.Image = ((NStrokeStyle)lesCouleursTour[index]).CreatePreview(new NSize(33, 13), (NStrokeStyle)lesCouleursTour[index]);
                pb_FillColor.Image = ((NFillStyle)lesCouleursRemplissage[index]).CreatePreview(new NSize(33, 13), (NStrokeStyle)lesCouleursTour[index]);
            }
        }
        /// <summary>
        /// Permet de changer la couleur de remplissage d'une courbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorChooseFillBtn_Click(object sender, EventArgs e)
        {
            if (clb_ColumnsToShow.SelectedItem == null)
                return;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
                return;
            NFillStyle style = (NFillStyle)lesCouleursRemplissage[index];
            ///On ouvre l'éditeur de couleur avec la couleur courante
            if (NFillStyleTypeEditor.Edit(style, out style))
            {
                ///On définit la nouvelle couleur et on change l'image
                lesCouleursRemplissage[index] = style;
                pb_FillColor.Image = ((NFillStyle)lesCouleursRemplissage[index]).CreatePreview(new NSize(33, 13), (NStrokeStyle)lesCouleursTour[index]);
            }
        }

        /// <summary>
        /// permet de changer la couleur de la ligne d'accumulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pb_LineAccumulationColor_Click(object sender, EventArgs e)
        {
            if (clb_ColumnsToShow.SelectedItem == null)
                return;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
                return;
            NStrokeStyle style = new NStrokeStyle((Color)lesAccumulations[index]);
            ///On ouvre l'éditeur de couleur avec la couleur courante
            if (NStrokeStyleTypeEditor.Edit(style, out style))
            {
                ///On définit la nouvelle couleur et on change l'image
                lesAccumulations[index] = style.Color;
                pb_LineAccumulationColor.Image = style.CreatePreview(new NSize(33, 13), style);
            }
        }
        #endregion

        #region Fonction qui s'occupe d'activer ou de désactivé le group box suivant la sélection

        /// <summary>
        /// Met à jour l'affichage des données dans la fenetre d'assistant graphique
        /// </summary>
        /// <param name="index"></param>
        private void DisplaySettings(int index)
        {
            if (bUserCheck)
                return;
            cb_Axis.Text = lAxeDeRepresentation[index].ToString();
            cb_ChartType.Text = laRepresentation[index].ToString();
            cb_Position.SelectedIndex = (int)lesPositions[index];
            pb_StrokeColor.Image = ((NStrokeStyle)lesCouleursTour[index]).CreatePreview(new NSize(33, 13), (NStrokeStyle)lesCouleursTour[index]);
            pb_FillColor.Image = ((NFillStyle)lesCouleursRemplissage[index]).CreatePreview(new NSize(33, 13), (NStrokeStyle)lesCouleursTour[index]);
            cb_ShowValues.Checked = (Boolean)lesShowValues[index];
                                                            
            cb_AddCumul.Checked = ((Color)lesAccumulations[index]) != Color.Transparent;
            chb_Scrollbar.Checked = lesPositionsXScrollbar[cb_Position.SelectedIndex];

            if (cb_Position.SelectedIndex < lesPositionsBarWidth.Count)
                barWidthTextBox.Text = lesPositionsBarWidth[cb_Position.SelectedIndex].ToString();
            if (cb_Position.SelectedIndex < lesPositionsGapPercent.Count)
                gapPercentTextBox.Text = lesPositionsGapPercent[cb_Position.SelectedIndex].ToString();
            if (cb_Position.SelectedIndex < lesPositionsBarWidthPercent.Count)
                barWidthPercentageTextBox.Text = lesPositionsBarWidthPercent[cb_Position.SelectedIndex].ToString();

            if (cb_AddCumul.Checked)
            {
                NStrokeStyle style = new NStrokeStyle((Color)lesAccumulations[index]);
                pb_LineAccumulationColor.Image = style.CreatePreview(new NSize(33, 13), style);
            }
        }

        #endregion


        #region Fonction qui recherche l'index du nom de la colonne dans les données en mémoires
        private int indexColumn(String ColumnName)
        {
            int i = 0;
            foreach (String noms in ColumnNames)
            {
                if (ColumnName.ToString() == noms.ToString())
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        private int indexOfCartesianChart()
        {
            int[] Used = new int[4] { -1, -1, -1, -1 };
            int nbPie = 0;
            int nbCartesian = 0;
            int i;
            for (i = 0; i < lesPositions.Count; i++)
            {
                if (lAxeDeRepresentation[i].ToString() != "X")
                {
                    if (laRepresentation[i].ToString() != "Pie")
                    {
                        Used[(int)lesPositions[i]] = 0;
                        nbCartesian++;
                        //return (int)lesPositions[i];
                    }
                    else
                    {
                        Used[(int)lesPositions[i]] = 1;
                        nbPie++;
                    }
                }
            }
            if (nbPie >= 4)
                return -1;
            for (i = 0; i < 4; i++)
            {
                if (Used[i] == 0)
                    return i;
                if ((nbCartesian == 0) && (Used[i] == -1))
                    return i;

            }
            return nbPie;
        }
        #endregion


        private bool CheckPositionsAttribution()
        {
            int[] attribution = new int[4] { -1, -1, -1, -1 };
            int Position;
            int i;
            for (i = 0; i < lesPositions.Count; i++)
            {
                if (lAxeDeRepresentation[i].ToString() != "X")
                {
                    Position = (int)lesPositions[i];
                    if (Position == -1)
                        continue;
                    if (attribution[Position] != -1)
                    {
                        if (attribution[Position] == 1)
                        {
                            return false;
                        }
                        if (laRepresentation[i].ToString() == "Pie")
                        {
                            return false;
                        }
                    }
                    else if (laRepresentation[i].ToString() == "Pie")
                    {
                        attribution[Position] = 1;
                    }
                    else
                    {
                        attribution[Position] = 0;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Supprime toutes les colonnes contenues dans la liste clb_ColumnsToShow
        /// </summary>
        /// <param name="chartToHold"></param>
        /// <param name="iPosition"></param>
        private void DeleteAllChart(String chartToHold, int iPosition)
        {
            for (int i = 0; i < lesPositions.Count; i++)
            {
                if (((int)lesPositions[i]) == iPosition)
                {
                    if (lAxeDeRepresentation[i].ToString() == "X")
                        continue;
                    if (ColumnNames[i].ToString() != chartToHold)
                    {
                        int j;
                        ListeItem tmp = null;
                        for (j = 0; j < clb_ColumnsToShow.Items.Count; j++)
                        {
                            tmp = (ListeItem)clb_ColumnsToShow.Items[j];
                            if (tmp.Name.ToString() == ColumnNames[i].ToString())
                                break;
                        }
                        if (j != clb_ColumnsToShow.Items.Count)
                        {
                            bUserCheck = false;
                            tmp.Selected = false;
                            clb_ColumnsToShow.Items[j] = tmp;
                            bUserCheck = true;
                        }
                        deleteChart(i);
                        i--;
                    }
                }
            }
            CheckInvertAbscisse();
        }

        /// <summary>
        /// Supprime toutes les informations de la colonne
        /// </summary>
        /// <param name="iIndex"></param>
        private void deleteChart(int iIndex)
        {
            if (iIndex >= ColumnNames.Count)
                return;
            ColumnNames.RemoveAt(iIndex);
            OriginColumn.RemoveAt(iIndex);
            laRepresentation.RemoveAt(iIndex);
            lAxeDeRepresentation.RemoveAt(iIndex);
            MaxCandleValue.RemoveAt(iIndex);
            MidCandleValue.RemoveAt(iIndex);
            MinCandleValue.RemoveAt(iIndex);
            lesAccumulations.RemoveAt(iIndex);
            lesCouleursTour.RemoveAt(iIndex);
            lesCouleursRemplissage.RemoveAt(iIndex);
            lesShowValues.RemoveAt(iIndex);            
            lesPositions.RemoveAt(iIndex);
        }

        /// <summary>
        /// Ajoute l'option d'accumulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_AddCumul_CheckedChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;
            pb_LineAccumulationColor.Enabled = cb_AddCumul.Checked;

            if (!cb_DisplayColumn.Checked)
                return;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());

            if (index == -1)
                return;
            ///On enleve l'accumulation
            if (!cb_AddCumul.Checked)
            {
                lesAccumulations[index] = Color.Transparent;
            }
            ///On affiche l'accumulation
            else
            {
                if (((Color)lesAccumulations[index]) == Color.Transparent)
                {
                    lesAccumulations[index] = ((NStrokeStyle)lesCouleursTour[index]).Color;
                }
                ///On définit le nouveau style pour la courbe d'accumulation déterminée grace à la couleur de la liste lesCouleursTour
                NStrokeStyle style = new NStrokeStyle((Color)lesAccumulations[index]);
                pb_LineAccumulationColor.Image = style.CreatePreview(new NSize(33, 13), style);
            }
        }

        /// <summary>
        /// On ajoute une colonne dans la liste clb_ColumnsToShow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Column_Click(object sender, EventArgs e)
        {
            SIM_Graphic_Association ColumnSelect = new SIM_Graphic_Association(DonneesEnCours, NodesArbre, imgListTreeView_);
            ///On ouvre le fenetre SIM_Graphic_Association qui va nous permettre de choisir la colonne à ajouter
            if (ColumnSelect.ShowDialog() == DialogResult.OK)
            {
                ColumnInformation column = ColumnSelect.Column;
                for (int i = 0; i < clb_ColumnsToShow.Items.Count; i++)
                {
                    if (((ListeItem)clb_ColumnsToShow.Items[i]).Name.ToString() == column.ToString())
                    {
                        clb_ColumnsToShow.SelectedIndex = i;
                        return;
                    }
                }
                ListeItem tmp = new ListeItem(column, true);
                clb_ColumnsToShow.Items.Add(tmp);
                clb_ColumnsToShow.SelectedIndex = clb_ColumnsToShow.Items.Count - 1;
                cb_DisplayColumn.Checked = false;
                cb_DisplayColumn.Checked = true;
                cb_DisplayColumn.Enabled = false;
                UpdateDisplay();
            }
        }

        /// <summary>
        /// On supprime une colonne et on l'enleve de la liste clb_ColumnsToShow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DeleteColumn_Click(object sender, EventArgs e)
        {
            if (clb_ColumnsToShow.SelectedIndex == -1)
                return;

            for (int i = 0; i < ListAnnotation.Count; i++)
            {
                if (ListAnnotation[i].objectId == ((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString())
                    this.ListAnnotation.RemoveAt(i);
            }
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index != -1)
            {
                deleteChart(index);
                clb_ColumnsToShow.Items.RemoveAt(clb_ColumnsToShow.SelectedIndex);
            }


        }

        /// <summary>
        /// On edite une colonne ce qui nous permet de la remplacer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (clb_ColumnsToShow.SelectedIndex == -1)
                return;
            SIM_Graphic_Association ColumnSelect = new SIM_Graphic_Association(DonneesEnCours, NodesArbre, (ColumnInformation)((ListeItem)clb_ColumnsToShow.Items[clb_ColumnsToShow.SelectedIndex]).Name, imgListTreeView_);
            if (ColumnSelect.ShowDialog() == DialogResult.OK)
            {
                ColumnInformation column = ColumnSelect.Column;
                ((ListeItem)clb_ColumnsToShow.Items[clb_ColumnsToShow.SelectedIndex]).Name = column;
                clb_ColumnsToShow.Items[clb_ColumnsToShow.SelectedIndex] = clb_ColumnsToShow.Items[clb_ColumnsToShow.SelectedIndex];
                ColumnNames[clb_ColumnsToShow.SelectedIndex] = ((ListeItem)clb_ColumnsToShow.Items[clb_ColumnsToShow.SelectedIndex]).Name.ToString();
                OriginColumn[clb_ColumnsToShow.SelectedIndex] = ((ListeItem)clb_ColumnsToShow.SelectedItem).Name;
                UpdateDisplay();
            }
        }

        /// <summary>
        /// On active ou desactive fonction d'affichage des valeurs de courbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_ShowValues_CheckedChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;
            if (!cb_DisplayColumn.Checked)
                return;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());

            if (index == -1)
                return;
            ///On peut afficher les valeurs pour les courbes de type Bar, Area stacked ou Bar stacked
            bool bFillColor = ((cb_ChartType.Text == "Bar")
                               || (cb_ChartType.Text == "Area stacked")
                               || (cb_ChartType.Text == "Bar stacked"));

            lesShowValues[index] = cb_ShowValues.Checked;
        }

        /// <summary>
        /// On inverse les abscisses
        /// </summary>
        private void CheckInvertAbscisse()
        {
            bool AncienneValue = CB_InvertAbscisse.Checked;
            CB_InvertAbscisse.Enabled = false;
            CB_InvertAbscisse.Checked = false;
            if (this.laTable == null)
                return;
            if (!lAxeDeRepresentation.Contains("X"))
                return;
            int iIndexX = lAxeDeRepresentation.IndexOf("X");
            if (iIndexX >= ColumnNames.Count)
                return;
            String ColumnName = ColumnNames[iIndexX].ToString();
            if (!laTable.Columns.Contains(ColumnName))
                return;
            Type dataType = laTable.Columns[ColumnName].DataType;
            if ((dataType == typeof(int)) ||
                (dataType == typeof(Int32)) ||
                (dataType == typeof(Int16)) ||
                (dataType == typeof(Int64)) ||
                (dataType == typeof(Double)) ||
                (dataType == typeof(double)))
            {
                CB_InvertAbscisse.Enabled = true;
                CB_InvertAbscisse.Checked = AncienneValue;
            }
        }

        /// <summary>
        /// Permet d'afficher la fenetre de choix du nom des axes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Axes_Click(object sender, EventArgs e)
        {
            ///On affiche la fenetre SIM_Axes_Assistant  avec les noms des axes X, Y et Y2 existant
            SIM_Axes_Assistant Assistant = new SIM_Axes_Assistant(sXTitle, sYTitle, sY2Title);
            if (Assistant.ShowDialog() != DialogResult.OK)
                return;

            sXTitle = Assistant.X;
            sYTitle = Assistant.Y;
            sY2Title = Assistant.Y2;
        }

        /// <summary>
        /// Ajout d'une colonne
        /// </summary>
        /// <param name="column"></param>
        public void Add_Column_Click(ColumnInformation column)
        {
            for (int i = 0; i < clb_ColumnsToShow.Items.Count; i++)
            {
                if (((ListeItem)clb_ColumnsToShow.Items[i]).Name.ToString() == column.ToString())
                {
                    clb_ColumnsToShow.SelectedIndex = i;
                    return;
                }
            }
            ListeItem tmp = new ListeItem(column, true);
            clb_ColumnsToShow.Items.Add(tmp);
            clb_ColumnsToShow.SelectedIndex = clb_ColumnsToShow.Items.Count - 1;
            cb_DisplayColumn.Checked = false;
            cb_DisplayColumn.Checked = true;
            cb_DisplayColumn.Enabled = false;
            UpdateDisplay();
        }

        /// <summary>
        /// Lors du double clique sur un item de clb_ColumnsToShow_DoubleClick on edit la colonne cliquée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clb_ColumnsToShow_DoubleClick(object sender, EventArgs e)
        {
            if (btn_Edit.Visible)
            {
                if (clb_ColumnsToShow.SelectedIndex != -1)
                {
                    btn_Edit_Click(sender, e);
                }
            }
        }

        // Put the txt_FilterName field to enable if we are building a new chart
        internal void setTxt_FilterNameEnable()
        {
            txt_FilterName.Enabled = true;
        }

        /// <summary>
        /// appelé lors du click sur le Setpoint Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SetPointEdit_Click(object sender, EventArgs e)
        {
            int setpointActivated = 0;

            setpointWindow = new Prompt.SIM_Setpoint();
            ///On met à jour les listes dans la fenetre d'edition des setpoints
            setpointWindow.Value = this.listSetPointValue;
            setpointWindow.Value2 = this.listSetPointValue2;
            setpointWindow.beginDateTime = this.listSetPointBDateTime;
            setpointWindow.endDateTime = this.listSetPointEDateTime;
            setpointWindow.listSetPointStrokeColor = this.listSetPointStrokeColor;
            setpointWindow.listSetPointFillColor = this.listSetPointFillColor;
            setpointWindow.isArea = this.listSetPointIsArea;
            setpointWindow.axis = this.listSetPointAxis;
            setpointWindow.isActivated = this.listSetPointIsActivated;

            ///Mettre à jour l'affichage dans la fenetre d'edition.
            setpointWindow.DisplaySetpointList();

            // Retrieves the Abscissa type, useful for the setpoint use
            int iIndex = lAxeDeRepresentation.IndexOf("X");
            if (iIndex >= 0)
            {
                String type;
                DataTable dtTAble = testTypeColonne(out type);
                ///L'axe des X est de type DateTime on doit changer le type d'edition du setpoint lorsque celui ci est affiché en X
                if (type == "DateTime")
                {
                    if (ColumnNames != null && iIndex < ColumnNames.Count
                        && ColumnNames[iIndex] != null)
                    {
                        int dateTimeColumnIndex = dtTAble.Columns.IndexOf(ColumnNames[iIndex].ToString());
                        if (dateTimeColumnIndex != -1)  // >> Bug #13387 Default Analysis Charts X axis time range C#14
                        {
                            setpointWindow.DateTimeAbscissa = true;
                            setpointWindow.SetAbscissaDateLimits(OverallTools.DataFunctions
                                .getMinDate(dtTAble, dateTimeColumnIndex), OverallTools.DataFunctions.getMaxDate(dtTAble, dateTimeColumnIndex));
                        }
                        else
                        {
                            OverallTools.ExternFunctions.PrintLogFile("Chart Editor SetPoint option: Error while setting the time axis limits."
                                + Environment.NewLine + "The column \"" + ColumnNames[iIndex] + "\" couldn't be found in the table \"" + dtTAble.TableName + "\".");
                        }
                    }
                }
                else
                    setpointWindow.DateTimeAbscissa = false;
            }
            else
            {
                // >> Bug #15147 Charts Setpoints only work for first series chart location (frame) C#15
                DateTime minDate = DateTime.MaxValue;
                DateTime maxDate = DateTime.MinValue;
                if (PAX2SIM.allAbscissaColumnsAreDateTime(OriginColumn, DonneesEnCours, out minDate, out maxDate)
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
            ///On charge la fenetre de gestion des septpoints
            if (setpointWindow.ShowDialog() == DialogResult.OK)
            {
                ///On met a jour les listes de setpoint suivant les changements qui ont été faits
                this.listSetPointValue = setpointWindow.Value;
                this.listSetPointValue2 = setpointWindow.Value2;
                this.listSetPointBDateTime = setpointWindow.beginDateTime;
                this.listSetPointEDateTime = setpointWindow.endDateTime;
                this.listSetPointStrokeColor = setpointWindow.listSetPointStrokeColor;
                this.listSetPointFillColor = setpointWindow.listSetPointFillColor;
                this.listSetPointIsArea = setpointWindow.isArea;
                this.listSetPointAxis = setpointWindow.axis;
                this.setPoints = setpointWindow.setPoints;

                //If there is at least one activated setpoint, set the Setpoint Icon to checked state
                for (int j = 0; j < listSetPointValue.Count; j++)
                    if (listSetPointIsActivated[j])
                    {
                        changeBtn_SetpointFont(new Font("Microsoft Sans Serif", 8, FontStyle.Bold));
                        setpointActivated = 1;
                    }
                if (setpointActivated == 0)
                    changeBtn_SetpointFont(new Font("Microsoft Sans Serif", 8, FontStyle.Regular));
            }

        }


        internal DataTable testTypeColonne(out String type)
        {
            int iIndex = lAxeDeRepresentation.IndexOf("X");

            DataTable dtTAble;
            String sColumnName = "";
            if (DonneesEnCours != null)
            {
                int indexXclb_ColumnsToShow = 0;

                for (int i = 0; i < clb_ColumnsToShow.Items.Count; i++)
                    if (clb_ColumnsToShow.Items[i].ToString() == ColumnNames[iIndex].ToString())
                        indexXclb_ColumnsToShow = i;

                SIM_Graphic_Association ColumnSelect = new SIM_Graphic_Association(DonneesEnCours, NodesArbre, (ColumnInformation)((ListeItem)clb_ColumnsToShow.Items[indexXclb_ColumnsToShow]).Name, imgListTreeView_);
                ColumnInformation ciColumn = ColumnSelect.Column;
                if((ciColumn.ExceptionName!=null) && (ciColumn.ExceptionName != ""))
                    dtTAble = DonneesEnCours.getExceptionTable
                        (ciColumn.DataSet, ciColumn.TableName, ciColumn.ExceptionName);
                else
                    dtTAble = DonneesEnCours.getTable(ciColumn.DataSet, ciColumn.TableName);

                sColumnName = ciColumn.ColumnName;
            }
            else
            {
                dtTAble = laTable;
                sColumnName = ColumnNames[iIndex].ToString();
            }
            type = dtTAble.Columns[sColumnName].DataType.Name.ToString();

            return dtTAble;
        }


        //Usefull to change the font of the setpoint Button when setpoints are available
        internal void changeBtn_SetpointFont(Font font)
        {
            this.Btn_Setpoint.Font = font;
        }

        /// <summary>
        /// On supprime toutes les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DeleteAllColumns_Click(object sender, EventArgs e)
        {
            int nbcolumn = ColumnNames.Count;
            for (int i = 0; i < nbcolumn; i++)
            {
                deleteChart(0);
                clb_ColumnsToShow.Items.RemoveAt(0);
            }
        }

        //Display all the columns in the table
        private void btn_DisplayAll_Click(object sender, EventArgs e)
        {
            int nbcolumn = clb_ColumnsToShow.Items.Count;
            for (int i = 0; i < nbcolumn; i++)
            {
                clb_ColumnsToShow.SelectedIndex = i;
                clb_ColumnsToShow.SelectedItem = clb_ColumnsToShow.Items[i];
                //This one will call the method "cb_DisplayColumn_CheckedChanged" with the correct selectedIndex and selectedItem values
                cb_DisplayColumn.Checked = true;
                if( i == 0)
                    cb_Axis.SelectedIndex = 0;
            }
        }

        //Hide all the columns in the table
        private void btn_HideAll_Click(object sender, EventArgs e)
        {
            int nbcolumn = clb_ColumnsToShow.Items.Count;
            for (int i = 0; i < nbcolumn; i++)
            {
                clb_ColumnsToShow.SelectedIndex = i;
                clb_ColumnsToShow.SelectedItem = clb_ColumnsToShow.Items[i];
                cb_DisplayColumn.Checked = false;

            }

        }

        /// <summary>
        /// Change le type de valeur moyenne choisit pour le candle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_MiddleValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;
            bUserCheck = false;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
            {
                bUserCheck = true;
                return;
            }
            ///Si nous sommes en mode Candle list alors ajouter dans la candle list la colonne choisit en enlevant l'etoile présente en fin de nom.
            if (cb_ChartType.Text == "Candle list")
            {
                /// Permet de vérifier et changer l'état de l'ancienne colonne 
                Boolean StillCandleColumn = false;
                int i,j;
                for (i = 0; i < CandleColumnsList.Count; i++)
                {
                    if(i == 1) i++;
                    /// on regarde si la colonne changée est une des deux autres Candle colonnes,
                    /// si oui on ne fait rien, sinon on déselectionne cette colonne
                    if(CandleColumnsList[1].ToString().CompareTo(CandleColumnsList[i].ToString()) == 0)
                        StillCandleColumn = true;
                }
                for (i = 0; i < ColumnNames.Count; i++)
                    if (ColumnNames[i].ToString() == CandleColumnsList[1].ToString())
                        break;

                ListeItem tmp = null;
                ///on recupere la colonne enlevée
                for (j = 0; j < clb_ColumnsToShow.Items.Count; j++)
                {
                    tmp = (ListeItem)clb_ColumnsToShow.Items[j];
                    if (tmp.Name.ToString() == ColumnNames[i].ToString())
                        break;
                }
                ///Si la colonne faisait partie de la candle List mais qu'elle a été changée, on remet cette colonne en ligne
                if (laRepresentation[i].ToString().CompareTo("Candle list") == 0 && !StillCandleColumn)
                {
                    ///Si le graph est n'est pas un Graph général alors on change sa représentation en Line sinon on déselectionne 
                    if (DonneesEnCours == null)
                    {
                        tmp.Selected = false;
                        clb_ColumnsToShow.Items[j] = tmp;
                        deleteChart(i);
                    }
                    else
                        laRepresentation[i] = "line";
                }

                ////////////////////////////////////////////////////////////////////////////////////////


                ///Ajouter ou remplacer la colonne choisit dans la liste
                for (i = 0; i < clb_ColumnsToShow.Items.Count; i++)
                {
                    tmp = (ListeItem)clb_ColumnsToShow.Items[i];
                    if (tmp.Name.ToString() == cb_MiddleValue.Text.ToString())
                        break;
                }

                ///On récupère l'indice de colonne à ajouter dans la liste
                for (j = 0; j < ColumnNames.Count; j++)
                    if (ColumnNames[j].ToString() == tmp.Name.ToString())
                        break;

                if (i != clb_ColumnsToShow.Items.Count )
                {
                    if (DonneesEnCours == null)
                    {
                        ///Si la colonne n'est pas selectionnée, on la selectionne
                        if(!tmp.Selected)
                        {

                            ColumnNames.Add(tmp.Name.ToString());                        
                            if (tmp.Name.GetType() == typeof(ColumnInformation))
                            {
                                OriginColumn.Add(((ListeItem)tmp).Name);
                            }
                            else
                            {
                                OriginColumn.Add("");
                            }
                            int Position = indexOfCartesianChart();
                            if (Position == -1)
                            {
                                lesPositions.Add(0);
                                DeleteAllChart(tmp.Name.ToString(), 0);
                            }
                            else
                                lesPositions.Add(Position);
                            lesShowValues.Add(false);
                            
                            ///On change le type de courbe en Candle list
                            laRepresentation.Add("Candle list");
                            lAxeDeRepresentation.Add("Y");
                            MaxCandleValue.Add("");
                            MidCandleValue.Add("");
                            MinCandleValue.Add("");
                            lesAccumulations.Add(Color.Transparent);
                            int iColor = OverallTools.FonctionUtiles.getUnusedColor(lesCouleursRemplissage);
                            lesCouleursTour.Add(new NStrokeStyle(OverallTools.FonctionUtiles.getColorStroke(iColor)));
                            lesCouleursRemplissage.Add(new NColorFillStyle(OverallTools.FonctionUtiles.getColor(iColor)));
                            tmp.Selected = true;
                            clb_ColumnsToShow.Items[i] = tmp;
                        }
                        else
                            laRepresentation[j] = "Candle list";
                    }
                    else
                        laRepresentation[j] = "Candle list";
                }

                if (CandleColumnsList.Count > 1)
                    CandleColumnsList[1] = tmp.Name.ToString();
                else
                    CandleColumnsList.Add(tmp.Name.ToString());

                for (i = 0; i < MidCandleValue.Count; i++)
                {
                    if (MidCandleValue[i].ToString().CompareTo(CandleColumnsList[1].ToString()) == 0)
                    {
                        MaxCandleValue[i] = "";
                        MidCandleValue[i] = "";
                        MinCandleValue[i] = "";
                    }
                }
                

                int indexSelectedCandle = indexColumn(CandleColumnsList[1].ToString());
                if (indexSelectedCandle == -1)
                {
                    bUserCheck = true;
                    return;
                }
                lesPositions[indexSelectedCandle] = lesPositions[index];

                ///On fait en sorte que rien ne soit positionné après les candles si candle il y a
                moveColonneOrder(indexSelectedCandle);

            }
            else
            {
                ///définir la valeur choisit à la MidCandlelist
                MidCandleValue[index] = cb_MiddleValue.Text;
            }
            bUserCheck = true;

        }

        /// <summary>
        /// Change le type de valeur min choisit pour le candle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_MinValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;
            bUserCheck = false;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
            {
                bUserCheck = true;
                return;
            }
            if (cb_ChartType.Text == "Candle list")
            {
                /// Permet de vérifier et changer l'état de l'ancienne colonne 
                Boolean StillCandleColumn = false;
                int i, j;
                for (i = 0; i < CandleColumnsList.Count; i++)
                {
                    if (i == 2) break;
                    /// on regarde si la colonne changée est une des deux autres Candle colonnes,
                    /// si oui on ne fait rien, sinon on déselectionne cette colonne
                    if (CandleColumnsList[2].ToString().CompareTo(CandleColumnsList[i].ToString()) == 0)
                        StillCandleColumn = true;
                }
                for (i = 0; i < ColumnNames.Count; i++)
                    if (ColumnNames[i].ToString() == CandleColumnsList[2].ToString())
                        break;

                ListeItem tmp = null;
                ///on recupere la colonne enlevée
                for (j = 0; j < clb_ColumnsToShow.Items.Count; j++)
                {
                    tmp = (ListeItem)clb_ColumnsToShow.Items[j];
                    if (tmp.Name.ToString() == ColumnNames[i].ToString())
                        break;
                }
                ///Si la colonne faisait partie de la candle List mais qu'elle a été changée, on remet cette colonne en ligne
                if (laRepresentation[i].ToString().CompareTo("Candle list") == 0 && !StillCandleColumn)
                {
                    ///Si le graph est n'est pas un Graph général alors on change sa représentation en Line sinon on déselectionne 
                    if (DonneesEnCours == null)
                    {
                        tmp.Selected = false;
                        clb_ColumnsToShow.Items[j] = tmp;
                        deleteChart(i);
                    }
                    else
                        laRepresentation[i] = "line";
                }

                ////////////////////////////////////////////////////////////////////////////////////////


                ///Ajouter ou remplacer la colonne choisit dans la liste
                for (i = 0; i < clb_ColumnsToShow.Items.Count; i++)
                {
                    tmp = (ListeItem)clb_ColumnsToShow.Items[i];
                    if (tmp.Name.ToString() == cb_MinValue.Text.ToString())
                        break;
                }
                ///On récupère l'indice de colonne à ajouter dans la liste
                for (j = 0; j < ColumnNames.Count; j++)
                    if (ColumnNames[j].ToString() == tmp.Name.ToString())
                        break;

                if (i != clb_ColumnsToShow.Items.Count)
                {
                    ///Si le graph est n'est pas un Graph général alors on change sa représentation en CandleList sinon on le selectionne
                    if (DonneesEnCours == null)
                    {
                        ///Si la colonne n'est pas selectionnée, on la selectionne
                        if (!tmp.Selected)
                        {
                            ColumnNames.Add(tmp.Name.ToString());
                            if (tmp.Name.GetType() == typeof(ColumnInformation))
                            {
                                OriginColumn.Add(((ListeItem)tmp).Name);
                            }
                            else
                            {
                                OriginColumn.Add("");
                            }
                            int Position = indexOfCartesianChart();
                            if (Position == -1)
                            {
                                lesPositions.Add(0);
                                DeleteAllChart(tmp.Name.ToString(), 0);
                            }
                            else
                                lesPositions.Add(Position);
                            lesShowValues.Add(false);
                            
                            laRepresentation.Add("Candle list");
                            lAxeDeRepresentation.Add("Y");
                            MaxCandleValue.Add("");
                            MidCandleValue.Add("");
                            MinCandleValue.Add("");
                            lesAccumulations.Add(Color.Transparent);
                            int iColor = OverallTools.FonctionUtiles.getUnusedColor(lesCouleursRemplissage);
                            lesCouleursTour.Add(new NStrokeStyle(OverallTools.FonctionUtiles.getColorStroke(iColor)));
                            lesCouleursRemplissage.Add(new NColorFillStyle(OverallTools.FonctionUtiles.getColor(iColor)));
                            tmp.Selected = true;
                            clb_ColumnsToShow.Items[i] = tmp;
                        }
                        else
                            laRepresentation[j] = "Candle list";
                    }
                    else
                        laRepresentation[j] = "Candle list";
                }
                

                if (CandleColumnsList.Count > 2)
                    CandleColumnsList[2] = tmp.Name.ToString();
                else
                    CandleColumnsList.Add(tmp.Name.ToString());

                for (i = 0; i < MinCandleValue.Count; i++)
                {
                    if (MinCandleValue[i].ToString().CompareTo(CandleColumnsList[2].ToString()) == 0)
                    {
                        MaxCandleValue[i] = "";
                        MidCandleValue[i] = "";
                        MinCandleValue[i] = "";
                    }
                }

                ///On définit comme position la meme position que la colonne avec laquelle la candlelist a été créé.
                int indexSelectedCandle = indexColumn(CandleColumnsList[2].ToString());
                if (indexSelectedCandle == -1)
                {
                    bUserCheck = true;
                    return;
                }
                lesPositions[indexSelectedCandle] = lesPositions[index];

                ///On fait en sorte que rien ne soit positionné après les candles si candle il y a
                moveColonneOrder(indexSelectedCandle);

            }
            else
            {
                MinCandleValue[index] = cb_MinValue.Text;
            }
            bUserCheck = true;
        }

        private void AdjustWidthComboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        /// <summary>
        /// Change le type de valeur Max choisit pour le candle
        /// Cette fonction ne peut etre appellée que lorque la courbe est en type Candles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_MaxValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;
            bUserCheck = false;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
            {
                bUserCheck = true;
                return;
            }
            ///Ajoute le type max dans la liste
            if (MaxCandleValue.Count > 0)
                MaxCandleValue[index] = cb_MaxValue.Text;
            else
                MaxCandleValue.Add(cb_MaxValue.Text);

            bUserCheck = true;
        }

        /// <summary>
        /// Active ou désactive la scrollbar pour la position de chart choisit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chb_Scrollbar_CheckedChanged(object sender, EventArgs e)
        {
            if( chb_Scrollbar.Checked)
                lesPositionsXScrollbar[cb_Position.SelectedIndex] = true;
            else
                lesPositionsXScrollbar[cb_Position.SelectedIndex] = false;
        }

        /// <summary>
        /// Permet de modifier l'ordre d'affichage des courbes sur le graphique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ColumnOrder_Click(object sender, EventArgs e)
        {
            ///On initialise la fenetre avec les parametres necessaires
            ColumnPosition.initializeWindow(ColumnNames, OriginColumn, laRepresentation, lAxeDeRepresentation, lesCouleursTour,
            lesCouleursRemplissage, lesAccumulations, lesPositions, lesShowValues, MaxCandleValue, MidCandleValue, MinCandleValue);

            ///On affiche la fenetre d'assistant
            if (ColumnPosition.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < ColumnNames.Count; i++)
                {
                    ///On affecte les changements aux listes
                    ColumnNames[i] = ColumnPosition.ColumnNames_[i];
                    this.OriginColumn[i] = ColumnPosition.OriginColumn_[i];
                    this.laRepresentation[i] = ColumnPosition.laRepresentation_[i];
                    this.lAxeDeRepresentation[i] = ColumnPosition.lAxeDeRepresentation_[i];
                    this.lesCouleursTour[i] = ColumnPosition.lesCouleursTour_[i];
                    this.lesCouleursRemplissage[i] = ColumnPosition.lesCouleursRemplissage_[i];
                    this.lesAccumulations[i] = ColumnPosition.lesAccumulations_[i];
                    this.lesPositions[i] = ColumnPosition.lesPositions_[i];
                    this.lesShowValues[i] = ColumnPosition.lesShowValues_[i];                    
                    this.MaxCandleValue[i] = ColumnPosition.MaxCandleValue_[i];
                    this.MidCandleValue[i] = ColumnPosition.MidCandleValue_[i];
                    this.MinCandleValue[i] = ColumnPosition.MinCandleValue_[i];
                }
            }
        }

        /// <summary>
        /// Met à jour la partie candle settings pour les candles
        /// </summary>
        private void setCandleInformations()
        {

            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());
            if (index == -1)
                return;

            ///Activer les candle Informations listes et afficher les types contenus dans les listes Max, Min et Middle
            cb_MaxValue.Enabled = true;
            CandleL3.Text = "Max Value";
            CandleL1.Text = "Middle Value";
            CandleL2.Text = "Min Value";

            cb_MaxValue.Items.Clear();
            cb_MiddleValue.Items.Clear();
            cb_MinValue.Items.Clear();

            cb_MinValue.Items.Add("Min");
            cb_MinValue.Items.Add("Min - zero");
            cb_MiddleValue.Items.Add("50%");
            cb_MiddleValue.Items.Add("75%");
            cb_MiddleValue.Items.Add("95%");
            cb_MiddleValue.Items.Add("99%");
            cb_MaxValue.Items.Add("Max");
            cb_MaxValue.Items.Add("99%");

            ///Si le type n'a pas été encore définit, définir un type automatique à savoir Max pour la valeur Max, 95% pour la valeur moyenne et Min pour la valeur Min
            if (MaxCandleValue[index].ToString() != "")
                cb_MaxValue.SelectedItem = MaxCandleValue[index];
            else
                cb_MaxValue.SelectedItem = MaxCandleValue[index] = "Max";
            if (MidCandleValue[index].ToString() != "")
                cb_MiddleValue.SelectedItem = MidCandleValue[index];
            else
                cb_MiddleValue.SelectedItem = MidCandleValue[index] = "95%";
            if (MinCandleValue[index].ToString() != "")
                cb_MinValue.SelectedItem = MinCandleValue[index];
            else
                cb_MinValue.SelectedItem = MinCandleValue[index] = "Min";
        }

        /// <summary>
        /// Met a jour la partie candle settings pour les candle list
        /// </summary>
        private void setCandleListInformations()
        {
            ///Activer les comboBox Second et Third Column et desactiver la First Column.Afficher les types contenus dans les listes Max, Min et Middle
            cb_MaxValue.Enabled = false;
            CandleL3.Text = "First Column";
            CandleL1.Text = "Second Column";
            CandleL2.Text = "Third Column";

            cb_MaxValue.Items.Clear();
            cb_MiddleValue.Items.Clear();
            cb_MinValue.Items.Clear();

            ///Ajouter dans les listes toutes les colonnes contenues dans clb_ColumnsToShow
            for (int i = 0; i < clb_ColumnsToShow.Items.Count; i++)
            {
                cb_MiddleValue.Items.Add(((ListeItem)clb_ColumnsToShow.Items[i]).Name.ToString());
                cb_MinValue.Items.Add(((ListeItem)clb_ColumnsToShow.Items[i]).Name.ToString());
                cb_MaxValue.Items.Add(((ListeItem)clb_ColumnsToShow.Items[i]).Name.ToString());
            }
            ///Si Il n'y a pas deja de candle list de definit
            if (CandleColumnsList.Count == 0)
            {
                //lesPositions[clb_ColumnsToShow.SelectedIndex] = 0;
                ///Afficher la colonne selectionnée comme etant d'origine dans les trois listes
                cb_MaxValue.SelectedItem = cb_MaxValue.Items[clb_ColumnsToShow.SelectedIndex];
                cb_MiddleValue.SelectedItem = cb_MiddleValue.Items[clb_ColumnsToShow.SelectedIndex];
                cb_MinValue.SelectedItem = cb_MinValue.Items[clb_ColumnsToShow.SelectedIndex];
                ///Ajouter la colonne dans la candle list en enlevant l'etoile à la fin du nom

                CandleColumnsList.Add(cb_MaxValue.Text);
                CandleColumnsList.Add(cb_MaxValue.Text);
                CandleColumnsList.Add(cb_MaxValue.Text);
            }
        }

        /// <summary>
        ///  Fonction permettant d'amener une colonne en tout à la fin de la liste, utile pour les candles.
        /// </summary>
        /// <param name="index"></param>
        private void BringToEndList(int index)
        {

            object tmpItem;

            /// Tant que l'index de la colonne à changer n'est pas à la fin de liste
            while(index < lesPositions.Count-1)
            {
                ///On inverse toutes ses caractéristiques avec celles de la colonne à la position suivante
                tmpItem = ColumnNames[index + 1];
                ColumnNames[index + 1] = ColumnNames[index];
                ColumnNames[index] = tmpItem;

                tmpItem = OriginColumn[index + 1];
                OriginColumn[index + 1] = OriginColumn[index];
                OriginColumn[index] = tmpItem;

                tmpItem = laRepresentation[index + 1];
                laRepresentation[index + 1] = laRepresentation[index];
                laRepresentation[index] = tmpItem;

                tmpItem = lAxeDeRepresentation[index + 1];
                lAxeDeRepresentation[index + 1] = lAxeDeRepresentation[index];
                lAxeDeRepresentation[index] = tmpItem;

                tmpItem = lesCouleursTour[index + 1];
                lesCouleursTour[index + 1] = lesCouleursTour[index];
                lesCouleursTour[index] = tmpItem;

                tmpItem = lesCouleursRemplissage[index + 1];
                lesCouleursRemplissage[index + 1] = lesCouleursRemplissage[index];
                lesCouleursRemplissage[index] = tmpItem;

                tmpItem = lesAccumulations[index + 1];
                lesAccumulations[index + 1] = lesAccumulations[index];
                lesAccumulations[index] = tmpItem;

                tmpItem = lesPositions[index + 1];
                lesPositions[index + 1] = lesPositions[index];
                lesPositions[index] = tmpItem;

                tmpItem = lesShowValues[index + 1];
                lesShowValues[index + 1] = lesShowValues[index];
                lesShowValues[index] = tmpItem;
                                                
                tmpItem = MaxCandleValue[index + 1];
                MaxCandleValue[index + 1] = MaxCandleValue[index];
                MaxCandleValue[index] = tmpItem;

                tmpItem = MidCandleValue[index + 1];
                MidCandleValue[index + 1] = MidCandleValue[index];
                MidCandleValue[index] = tmpItem;

                tmpItem = MinCandleValue[index + 1];
                MinCandleValue[index + 1] = MinCandleValue[index];
                MinCandleValue[index] = tmpItem;

                index++;
            }
        }

        /// <summary>
        /// Permet de changer l'ordre dans les listes de la colonne dans le cas ou un candle est transformé en un autre type par exemple
        /// car tous les candles doivent être positionnés à la fin de la liste.
        /// </summary>
        /// <param name="index"></param>
        private void moveColonneOrder(int index)
        {
            if (index == 0)
                return;

            object tmpItem;

            ///On part par comparer la colonne positionnée à l'index -1
            for (int i = index-1; i >= 0; i--)
            {
                ///On recherche la première colonne n'etant pas un Candle
                if (laRepresentation[i].ToString() != "Candle")
                {
                    /// On remonte la colonne à changer jusque la position du dessus
                    while (index > i+1)
                    {
                        tmpItem = ColumnNames[index];
                        ColumnNames[index] = ColumnNames[index - 1];
                        ColumnNames[index - 1] = tmpItem;

                        tmpItem = lesPositions[index];
                        lesPositions[index] = lesPositions[index - 1];
                        lesPositions[index - 1] = tmpItem;

                        tmpItem = OriginColumn[index];
                        OriginColumn[index] = OriginColumn[index - 1];
                        OriginColumn[index - 1] = tmpItem;

                        tmpItem = laRepresentation[index];
                        laRepresentation[index] = laRepresentation[index - 1];
                        laRepresentation[index - 1] = tmpItem;

                        tmpItem = lAxeDeRepresentation[index];
                        lAxeDeRepresentation[index] = lAxeDeRepresentation[index - 1];
                        lAxeDeRepresentation[index - 1] = tmpItem;

                        tmpItem = lesCouleursTour[index];
                        lesCouleursTour[index] = lesCouleursTour[index - 1];
                        lesCouleursTour[index - 1] = tmpItem;

                        tmpItem = lesCouleursRemplissage[index];
                        lesCouleursRemplissage[index] = lesCouleursRemplissage[index - 1];
                        lesCouleursRemplissage[index - 1] = tmpItem;

                        tmpItem = lesAccumulations[index];
                        lesAccumulations[index] = lesAccumulations[index - 1];
                        lesAccumulations[index - 1] = tmpItem;

                        tmpItem = lesShowValues[index];
                        lesShowValues[index] = lesShowValues[index - 1];
                        lesShowValues[index - 1] = tmpItem;
                                                
                        tmpItem = MaxCandleValue[index];
                        MaxCandleValue[index] = MaxCandleValue[index - 1];
                        MaxCandleValue[index - 1] = tmpItem;

                        tmpItem = MidCandleValue[index];
                        MidCandleValue[index] = MidCandleValue[index - 1];
                        MidCandleValue[index - 1] = tmpItem;

                        tmpItem = MinCandleValue[index];
                        MinCandleValue[index] = MinCandleValue[index - 1];
                        MinCandleValue[index - 1] = tmpItem;

                        index--;
                    }
                    return;
                }
            }
        }

        private void barWidthPercentageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!bUserCheck)
                return;
            if (!cb_DisplayColumn.Checked)
                return;
            int index = indexColumn(((ListeItem)clb_ColumnsToShow.SelectedItem).Name.ToString());

            if (index == -1)
                return;
            double barWidthPerc = 0;
            Double.TryParse(barWidthPercentageTextBox.Text, out barWidthPerc);
            
        }

        private void barWidthTextBox_Leave(object sender, EventArgs e)
        {
            if (cb_Position.SelectedIndex < lesPositionsBarWidth.Count)
            {
                float barWidth = 0.0f;
                if (!float.TryParse(barWidthTextBox.Text, out barWidth))
                    barWidth = 0.0f;
                if (cb_Position.SelectedIndex >= 0 && cb_Position.SelectedIndex < lesPositionsBarWidth.Count)
                    lesPositionsBarWidth[cb_Position.SelectedIndex] = barWidth;
            }
        }

        private void gapPercentTextBox_Leave(object sender, EventArgs e)
        {
            if (cb_Position.SelectedIndex < lesPositionsGapPercent.Count)
            {
                float perc = 0.0f;
                if (!float.TryParse(gapPercentTextBox.Text, out perc))
                    perc = 0.0f;
                if (cb_Position.SelectedIndex >= 0 && cb_Position.SelectedIndex < lesPositionsGapPercent.Count)
                    lesPositionsGapPercent[cb_Position.SelectedIndex] = perc;
            }
        }

        private void barWidthPercentageTextBox_Leave(object sender, EventArgs e)
        {
            if (cb_Position.SelectedIndex < lesPositionsBarWidthPercent.Count)
            {
                float perc = 0.0f;
                if (!float.TryParse(barWidthPercentageTextBox.Text, out perc))
                    perc = 0.0f;
                if (cb_Position.SelectedIndex >= 0 && cb_Position.SelectedIndex < lesPositionsBarWidthPercent.Count)
                    lesPositionsBarWidthPercent[cb_Position.SelectedIndex] = perc;
            }
        }
        
    }
}