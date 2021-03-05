using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Drawing;
using Nevron.Chart;
using Nevron.Chart.WinForm;
using Nevron;
using System.Windows.Forms;
using Nevron.GraphicsCore;
using SIMCORE_TOOL.com.crispico.gantt;
using SIMCORE_TOOL.DataManagement;
using SIMCORE_TOOL.Prompt.Liege;
using SIMCORE_TOOL.com.crispico.generalUse;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.com.crispico.BHS_Analysis;
using SIMCORE_TOOL.com.crispico.charts;
using System.Xml;
using SIMCORE_TOOL.com.crispico.EBS;
using System.Drawing.Drawing2D;

namespace SIMCORE_TOOL
{

    #region La classe ColumnInformation
    /// <summary>
    /// Cette classe est utilisée pour les graphiques généraux d'un projet. Elle permet de trouver
    /// la table à laquelle la colonne fait référence, en stockant la manière d'accéder à cette
    /// colonne et en stockant le nom qu'il faut lui attribuer sur le graphique (fixer par 
    /// l'utilisateur).
    /// </summary>
    public class ColumnInformation
    {
        /// <summary>
        /// Le dataset d'où provient la donnée à afficher. "Input" ou alors le nom d'un scénario existant.
        /// </summary>
        private String DataSet_;
        /// <summary>
        /// Le nom de la table d'où provient la colonne.
        /// </summary>
        private String TableName_;

        /// <summary>
        /// Le nom de la table d'exception depuis laquelle la colonne provient.
        /// </summary>
        private String ExceptionName_;
        internal String ExceptionName { get { return ExceptionName_; } set { ExceptionName_ = value; } }
        /// <summary>
        /// Le nom de la colonne dans la table \ref TableName_ qui représente la donnée à afficher.
        /// </summary>
        private String ColumnName_;
        /// <summary>
        /// Le nom de la colonne dans la table \ref TableName_ qui représente l'abscisse. Par exemple
        /// si l'utilisateur souhaite afficher l'utilisation d'un poste par rapport au temps, il
        /// mettra en abscisse la colonne représentant le temps...
        /// </summary>
        private String AbscissaColumnName_;
        /// <summary>
        /// Le nom qu'il faudra affiché sur le graphique pour cette colonne.
        /// </summary>
        private String DisplayedName_;
        /// <summary>
        /// Le dataset d'où provient la donnée à afficher. "Input" ou alors le nom d'un scénario existant.
        /// </summary>
        public String DataSet
        {
            get
            {
                return DataSet_;
            }
            set // >> Task #13384 Report Tree-view
            {
                DataSet_ = value;
            }
        }
        /// <summary>
        /// Le nom de la table d'où provient la colonne.
        /// </summary>
        public String TableName
        {
            get
            {
                return TableName_;
            }
            set
            {
                TableName_ = value; // >> Task #10375 Pax2Sim - Chart - copy chart definition does not exist at Global Charts & User Graphic level
            }
        }
        /// <summary>
        /// Le nom de la colonne dans la table \ref TableName_ qui représente la donnée à afficher.
        /// </summary>
        public String ColumnName
        {
            get{
                return ColumnName_;
            }
        }
        /// <summary>
        /// Le nom de la colonne dans la table \ref TableName_ qui représente l'abscisse. Par exemple
        /// si l'utilisateur souhaite afficher l'utilisation d'un poste par rapport au temps, il
        /// mettra en abscisse la colonne représentant le temps...
        /// </summary>
        public String AbscissaColumnName
        {
            get
            {
                return AbscissaColumnName_;
            }
        }
        /// <summary>
        /// Accesseur pour savoir si la table possède une information pour l'axe des abscisses.
        /// </summary>
        public bool HadAbscissaColumn
        {
            get
            {
                return (AbscissaColumnName_ != "")&&(AbscissaColumnName_ != null);
            }
        }
        /// <summary>
        /// Le nom qu'il faudra affiché sur le graphique
        /// </summary>
        public String DisplayedName
        {
            get
            {
                return DisplayedName_;
            }
            set // >> Task #13384 Report Tree-view
            {
                DisplayedName_ = value;
            }
        }

        public ColumnInformation(String DataSet__, String TableName__, String ColumnName__, String DisplayedName__)
        {
            DataSet_ = DataSet__;
            TableName_ = TableName__;
            ExceptionName = null;
            ColumnName_ = ColumnName__;
            AbscissaColumnName_ = "";
            DisplayedName_ = DisplayedName__;
        }
        public ColumnInformation(String DataSet__, String TableName__, String ColumnName__,String AbscissaColumnName__, String DisplayedName__)
        {
            DataSet_ = DataSet__;
            TableName_ = TableName__;
            ExceptionName = null;
            ColumnName_ = ColumnName__;
            AbscissaColumnName_ = AbscissaColumnName__;
            DisplayedName_ = DisplayedName__;
        }
        public ColumnInformation(String DataSet__, String TableName__,String ExceptionName_,  String ColumnName__, String AbscissaColumnName__, String DisplayedName__)
        {
            DataSet_ = DataSet__;
            TableName_ = TableName__;
            ExceptionName = ExceptionName_;
            ColumnName_ = ColumnName__;
            AbscissaColumnName_ = AbscissaColumnName__;
            DisplayedName_ = DisplayedName__;
        }
        public String FullName()
        {
            if ((ExceptionName != null) && (ExceptionName != ""))
                return DataSet_ + "." + TableName_ + "."+ExceptionName+"." + ColumnName_;
            return DataSet_ + "." + TableName_ + "." + ColumnName_;

        }
        public override string ToString()
        {
            return DisplayedName;
        }

        public ColumnInformation clone()    // >> Task #13384 Report Tree-view
        {
            string dataSetClone = "";
            if (DataSet != null && DataSet.Clone() != null)
                dataSetClone = DataSet.Clone().ToString();

            string tableNameClone = "";
            if (TableName != null && TableName.Clone() != null)
                tableNameClone = TableName.Clone().ToString();

            string exceptionNameClone = "";
            if (ExceptionName != null && ExceptionName.Clone() != null)
                exceptionNameClone = ExceptionName.Clone().ToString();

            string columnNameClone = "";
            if (ColumnName != null && ColumnName.Clone() != null)
                columnNameClone = ColumnName.Clone().ToString();
            
            string abscissaColumnNameClone = "";
            if (AbscissaColumnName != null && AbscissaColumnName.Clone() != null)
                abscissaColumnNameClone = AbscissaColumnName.Clone().ToString();

            string displayednameClone = "";
            if (DisplayedName != null && DisplayedName.Clone() != null)
                displayednameClone = DisplayedName.Clone().ToString();

            return new ColumnInformation(dataSetClone, tableNameClone, exceptionNameClone, columnNameClone, abscissaColumnNameClone, displayednameClone);
        }
    }
    #endregion

#if(DEBUG)
    public
#else
    internal
#endif 
        class GraphicFilter
    {
        #region Les différentes informations de la classe
        /// <summary>
        /// Liste contenant le noms des colonnes selectionnées
        /// </summary>
        private ArrayList listColumnsNames;
        /// <summary>
        /// Les noms des colonnes où se trouve la donnée. Si graphique associé qu'à une seule table, alors cette liste contiendra une chaine de 
        /// caractère avec le nom de la colonne à afficher. Sinon Cette liste contiendra un \ref ColumnInformation permettant de connaître la
        /// colonne à afficher pour les graphiques généraux.
        /// </summary>
        private ArrayList listColumnsOrigin;
        /// <summary>
        /// Liste contenant les types des charts ( Area, line, candle...)
        /// </summary>
        private ArrayList listVisualisation;
        /// <summary>
        /// Liste contenant les axes de représentation des colonnes (X, Y, Y2)
        /// </summary>
        private ArrayList listAxeRepresentation;
        /// <summary>
        /// Liste contenant les couleurs de contour des differents types de chart.
        /// </summary>
        private ArrayList listStrokeCouleurs;
        /// <summary>
        /// Liste contenant les couleurs de remplissage des differents types de chart.
        /// </summary>
        private ArrayList listFillCouleurs;
        /// <summary>
        /// Liste contenant les couleurs des accumulation. Couleur transparente si la colonne n'utilise pas d'accumulations.
        /// </summary>
        private ArrayList listAccumulation;
        /// <summary>
        /// Liste contenant les positions des charts (Top left, Bottom left, Top right, Bottom right)
        /// </summary>
        private ArrayList listPositions;
        /// <summary>
        /// liste de booleens précisant si les différentes positions sont affichées avec scrollbar ou pas
        /// </summary>
        private List<Boolean> lesPositionsXScrollbar;
        /// <summary>
        /// Liste de booleen permettant d'indiquer si on affiche ou pas la valeur max.
        /// </summary>
        private ArrayList listShowValues;
                
        /// <summary>
        /// Liste contenant les couleurs de contour des setpoints utilisés.Cette couleur est précisée lorsqu'on utilise un setpoint de type Zone
        /// </summary>
        private ArrayList listSetPointStrokeColor;
        /// <summary>
        /// Liste contenant les couleurs de remplissage des setpoints utilisés.
        /// </summary>
        private ArrayList listSetPointFillColor;
        /// <summary>
        /// Liste indiquant si les setpoints sont de type ligne ou zone
        /// </summary>
        private List<Boolean> listSetPointIsArea;
        /// <summary>
        /// Liste Contenant les valeurs des setpoints
        /// </summary>
        private List<double> listSetPointValue;
        /// <summary>
        /// Liste Contenant les deuxiemes valeurs de setpoint lorsque en mode zone
        /// </summary>
        private List<double> listSetPointValue2;
        /// <summary>
        /// Liste Contenant La date de debut du setpoint lorsque celui ci est en mode zone
        /// </summary>
        private List<DateTime> listSetPointBDateTime;
        /// <summary>
        /// Liste Contenant La date de fin du setpoint lorsque celui ci est en mode zone
        /// </summary>
        private List<DateTime> listSetPointEDateTime;
        /// <summary>
        /// Axe utilisé par le setpoint
        /// </summary>
        private List<String> listSetPointAxis;
        /// <summary>
        /// Liste indiquant si les setpoints sont Activés ou desactivés
        /// </summary>
        private List<Boolean> listSetPointIsActivated;
        /// <summary>
        /// Liste contenant les annotations du graphique
        /// </summary>
        private List<Notes> ListAnnotation;
        /// <summary>
        /// Utile pour les candles listes car il permet d'ajouter les candles à la suite des autres car Le candle est sauvegardé apres la premiere définition
        /// </summary>
        private NErrorBarSeries m_cCandle;

        //Candle Variables
        private ArrayList CandleColumnsList;
        private ArrayList MaxCandleValue;
        private ArrayList MidCandleValue;
        private ArrayList MinCandleValue;

        /// <summary>
        /// List contenant les données concernant les 3 axes X, Y, Y2 des 4 differentes positions existante dans un graphique
        /// </summary>
        private List<List<Axe>> AxisList;

        private bool bInvertAbscisse;

        /// <summary>
        /// Titre de l'abscisse X
        /// </summary>
        private String sXTitle;
        /// <summary>
        /// Titre de l'abscisse Y
        /// </summary>
        private String sYTitle;
        /// <summary>
        /// Titre de l'abscisse Y2
        /// </summary>
        private String sY2Title;

        public String Title;
        public String Name;
        private bool useScenarioNameInTitle_;// << Task #9624 Pax2Sim - Charts - checkBox for scenario name        

        private List<float> lesPositionsBarWidth;   //keep naming consistency wiht lesPositionsXScrollbar which has a similar behaviour
        private List<float> lesPositionsGapPercent;
        private List<float> lesPositionsBarWidthPercent;

        public List<SetPoint> setPoints = new List<SetPoint>(); // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
        #endregion

        #region Les différentes classes permettant la gestion des notes, axes et Evenements
        /// <summary>
        /// Classe contenant la description d'une annotation
        /// </summary>
        public class Notes
        {
            /// <summary>
            ///  Annotation Text
            /// </summary>
            string Text;
            /// <summary>
            /// Exact point where the annotation is bound
            /// </summary>
            int DataPointIndex;
            /// <summary>
            ///  id of the object bound to the annotation
            /// </summary>
            String ObjectId;
            /// <summary>
            /// Id of the annotation
            /// </summary>
            String AnnotationId;

            #region getters and setters
            internal String text
            {
                get
                {
                    return Text;
                }
                set
                {
                    Text = value;
                }
            }
            internal int dataPointIndex
            {
                get
                {
                    return DataPointIndex;
                }
                set
                {
                    DataPointIndex = value;
                }
            }
            internal String objectId
            {
                get
                {
                    return ObjectId;
                }
                set
                {
                    ObjectId = value;
                }
            }
            internal String annotationId
            {
                get
                {
                    return AnnotationId;
                }
                set
                {
                    AnnotationId = value;
                }
            }
            #endregion

            // >> Task #13384 Report Tree-view
            public Notes clone()
            {
                Notes clone = new Notes();
                if (text != null && text.Clone() != null)
                    clone.text = this.text.Clone().ToString();
                clone.dataPointIndex = this.dataPointIndex;
                if (objectId != null && objectId.Clone() != null)
                    clone.objectId = this.objectId.Clone().ToString();
                if (annotationId != null && annotationId.Clone() != null)
                    clone.annotationId = this.annotationId.Clone().ToString();
                return clone;
            }
            // << Task #13384 Report Tree-view
        }

        /// <summary>
        /// Classe contenant les données de debut et taille d'axe suivant le fait que l'axe est de type Date ou Numerique
        /// </summary>
        public class Axe
        {
            /// <summary>
            ///  Axe de type Date ou non
            /// </summary>
            Boolean isDateTime;
            DateTime beginDTValue;  /// Date de debut si l'axe est de type date, sinon sera définit comme DateTime(1,1,1)
            int lengthDTValue;      /// Taille de l'axe s'il est de type date, sinon sera définit comme DateTime(1,1,1)
            double beginNValue;     /// Valeur de début de l'axe s'il est de type numérique, sinon sera égal à -1
            double lengthNValue;    /// taille de l'axe s'il est de type numérique, sinon sera égal à -1

            #region getters and setters
            internal Boolean IsDateTime
            {
                get
                {
                    return isDateTime;
                }
                set
                {
                    isDateTime = value;
                }
            }
            internal DateTime BeginDTValue
            {
                get
                {
                    return beginDTValue;
                }
                set
                {
                    beginDTValue = value;
                }
            }
            internal int LengthDTValue
            {
                get
                {
                    return lengthDTValue;
                }
                set
                {
                    lengthDTValue = value;
                }
            }
            internal double BeginNValue
            {
                get
                {
                    return beginNValue;
                }
                set
                {
                    beginNValue = value;
                }
            }

            internal double LengthNValue
            {
                get
                {
                    return lengthNValue;
                }
                set
                {
                    lengthNValue = value;
                }
            }
            #endregion

            // >> Task #13384 Report Tree-view
            public Axe clone()
            {
                Axe clone = new Axe();
                clone.isDateTime = this.isDateTime;
                clone.beginDTValue = this.beginDTValue;
                clone.lengthDTValue = this.lengthDTValue;
                clone.beginNValue = this.beginNValue;
                clone.lengthNValue = this.lengthNValue;
                return clone;
            }
            // << Task #13384 Report Tree-view
        }

        /// <summary>
        /// Classe permettant la gestion de la scrollbar
        /// </summary>
        public class MyEventArgs : EventArgs
        {
            /// <summary>
            /// Graphique auquel la scrollbar est attaché
            /// </summary>
            public NChartControl GraphicArea;
            /// <summary>
            /// Type de valeur scrollé (DateTime, Numeriques)
            /// </summary>
            public String scaleType;
            /// <summary>
            /// Evenement appelé à chaque fois que l'utilisateur bouge la scrollbar d'un graphique
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            internal void ScrollbarValueChanged(object sender, System.EventArgs e)
            {
                NChart Graphique = GraphicArea.Charts[0];
                if (Graphique == null)
                    return;

                ///La scrollbar est utilisée avec l'axe des X
                NAxis axis = Graphique.Axis(StandardAxis.PrimaryX);
                ///Si le type de donnée scrollé est un DateTime
                if(scaleType == "DateTimeScale")
                {
                   NDateTimeAxisPagingView DateTimepagingView = axis.PagingView as NDateTimeAxisPagingView;
                   if (DateTimepagingView != null)
                   {
                        ///Mettre à jour l'affichage de graphique en récupérant la position de la scrollbar
                        DateTimepagingView.Begin = DateTime.FromOADate(Graphique.Axis(StandardAxis.PrimaryX).ScrollBar.BeginValue);
                   }
                }
                ///Si le type de donnée scrollé est de type numeric
                else if (scaleType == "LinearScale")
                {
                    NNumericAxisPagingView NumericpagingView = axis.PagingView as NNumericAxisPagingView;
                    if (NumericpagingView != null)
                    {
                        NumericpagingView.Begin = Graphique.Axis(StandardAxis.PrimaryX).ScrollBar.BeginValue;
                    }
                }


                GraphicArea.Refresh();
            }


        }
        #endregion

        #region Les fonctions qui permettent de créer les graphiques de manière automatique.
        /********************************************/
        /********************************************/
        /********************************************/
        /********************************************/

        #region Default GraphicFilter for "Input" tables
        // >> Task #15867 Transfer Distribution Tables/Charts improvement
        static List<string> columnsRejectedForYAxis = new List<string>(new string[] { GlobalNames.sColumnFrom, GlobalNames.sColumnTo });
        static List<string> defaultChartTableNames = new List<string>(new string[] { GlobalNames.Transfer_FlightCategoryDitributionTableName, 
                                                                                        GlobalNames.Transfer_TerminalDitributionTableName });
        public static GraphicFilter getDefaultGraphicFilterForInputTables(DataTable table)
        {
            GraphicFilter graphicFilter = null;
            if (table == null)
                return graphicFilter;

            if (!defaultChartTableNames.Contains(table.TableName))
                return graphicFilter;

            ArrayList ColumnsNames = new ArrayList();
            ColumnsNames.Add(table.Columns[0].ColumnName);
            ArrayList ColumnsOrigin = new ArrayList();
            ColumnsOrigin.Add("");
            
            ArrayList Visualisation = new ArrayList();
            Visualisation.Add("Line");

            ArrayList AxeRepresentation = new ArrayList();
            AxeRepresentation.Add("X");

            ArrayList StrokeCouleurs = new ArrayList();
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));

            ArrayList FillCouleurs = new ArrayList();
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));

            ArrayList Accumulation = new ArrayList();
            Accumulation.Add(Color.Transparent);

            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();
            List<bool> SetPointIsActivated = new List<bool>();

            List<Notes> ListAnnotation = new List<Notes>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();

            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            ArrayList Position = new ArrayList();
            Position.Add(0);

            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            ArrayList ShowValues = new ArrayList();
            ShowValues.Add(false);

            for (int i = 1; i < table.Columns.Count; i++)
            {
                string columnName = table.Columns[i].ColumnName;
                if (columnsRejectedForYAxis.Contains(columnName))
                    continue;
                
                ColumnsNames.Add(columnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Bar stacked");
                AxeRepresentation.Add("Y");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(i)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(i)));

                Position.Add(0);
                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);

                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");                
            }
            string xAxisTitle = GlobalNames.sColumnFrom + " > " + GlobalNames.sColumnTo;
            string yAxisiTitle = "%";
            string y2AxisTitle = "";
            graphicFilter = new GraphicFilter(ColumnsNames, ColumnsOrigin, Visualisation, AxeRepresentation, StrokeCouleurs, FillCouleurs, Accumulation,
                SetpointAxis, SetpointIsArea, SetpointStrokeColor, SetpointFillColor, SetpointValue, SetpointValue2, SetpointBDateTime, SetpointEDateTime, SetPointIsActivated,
                ListAnnotation, CandleColumnsList, MaxCandleValue, MidCandleValue, MinCandleValue, AxisList, Position, PositionsXScrollbar, ShowValues,
                table.TableName, table.TableName, false, xAxisTitle, yAxisiTitle, y2AxisTitle, false, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent,
                new List<SetPoint>());
            return graphicFilter;
        }
        // << Task #15867 Transfer Distribution Tables/Charts improvement
        #endregion

        #region Fonctions appelées pour la partie Statique de l'analyse
        public static GraphicFilter getStaticGraphicDistribution(DataTable dtTable)
        {
            ArrayList ColumnsNames = new ArrayList();
            ColumnsNames.Add(dtTable.Columns[0].ColumnName);
            ArrayList ColumnsOrigin = new ArrayList();
            ColumnsOrigin.Add("");
            ///On définit par defaut des parametres tel que le type de courbe en ligne sur l'axe X
            ArrayList Visualisation = new ArrayList();
            Visualisation.Add("Line");

            ArrayList AxeRepresentation = new ArrayList();
            AxeRepresentation.Add("X");

            ArrayList StrokeCouleurs = new ArrayList();

            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));

            ArrayList FillCouleurs = new ArrayList();

            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));

            ArrayList Accumulation = new ArrayList();
            Accumulation.Add(Color.Transparent);


            List<String> SetpointAxis = new List<String>();

            ArrayList SetpointStrokeColor = new ArrayList();

            ArrayList SetpointFillColor = new ArrayList();

            List<bool> SetpointIsArea = new List<bool>();

            List<double> SetpointValue = new List<double>();

            List<double> SetpointValue2 = new List<double>(); 

            List<DateTime> SetpointBDateTime = new List<DateTime>();

            List<DateTime> SetpointEDateTime = new List<DateTime>();
            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();

            ///La liste AxisList contient quatres listes de trois axes
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    ///Un axe contient une valeur de debut et de fin pour les axes de type Date time,
                    ///et debut et fin pour les axes de type numéric, et un booleen indiquant si on utilise le type DateTime
                    ///Par défaut, on met les variables à -1
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            ArrayList Position = new ArrayList();
            Position.Add(0);
            
            ///La liste PositionsXScrollbar contient quatres booleens par défaut à false.
            ///Ils permettent d'indiquer si la scrollbar est activée suivant les quatres positions de chart
            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            ArrayList ShowValues = new ArrayList();
            ShowValues.Add(false);
            
            
            bool bFlightCategory = dtTable.TableName.Contains("FlightCategor");

            for (int i = 1; i < dtTable.Columns.Count; i++)
            {
                if ((dtTable.Columns[i].ColumnName.Contains("Distrib"))||((dtTable.Columns[i].ColumnName.StartsWith("%_")) &&
                    (!dtTable.Columns[i].ColumnName.StartsWith("%_Cumul"))))
                {
                    ColumnsNames.Add(dtTable.Columns[i].ColumnName);
                    ColumnsOrigin.Add("");
                    Visualisation.Add("Bar");
                    AxeRepresentation.Add("Y");
                    StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(i)));
                    FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(i)));

                    Position.Add(0);
                    if (bFlightCategory)
                        Accumulation.Add(Color.Transparent);
                    else
                        Accumulation.Add(OverallTools.FonctionUtiles.getColor(i));
                    ShowValues.Add(true);
                    
                    MaxCandleValue.Add("");
                    MidCandleValue.Add("");
                    MinCandleValue.Add("");
                }
            }
            // << Task #9109 Pax2Sim - Static Analysis - Distribution charts start value
            if (bFlightCategory)
            {
                if (AxisList.Count > 0 && AxisList[0].Count > 0
                    && AxisList[0][0] != null)
                {
                    AxisList[0][0].BeginNValue = -1;
                    AxisList[0][0].LengthNValue = dtTable.Rows.Count;
                }
            }
            // >> Task #9109 Pax2Sim - Static Analysis - Distribution charts start value
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated ,
                                    ListAnnotation ,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, dtTable.TableName, false, 
                                    ColumnsNames[0].ToString(),
                                    "%",
                                    "",
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }
        // << Task #9171 Pax2Sim - Static Analysis - EBS algorithm - Throughputs

        public static GraphicFilter getStaticEBS(DataTable dtTable)
        {
            int indexColumnTime = dtTable.Columns.IndexOf(GlobalNames.FPD_EBS_TIME_COLUMN_NAME);
            int indexColumnConstrained = dtTable.Columns.IndexOf(GlobalNames.FPD_EBS_CONSTRAINED_COLUMN_NAME);
            int indexColumnUnconstrained = dtTable.Columns.IndexOf(GlobalNames.FPD_EBS_UNCONSTRAINED_OCCUPATION_COLUMN_NAME);

            int topLeftChartPosition = 0;
            int BottomLeftChartPosition = 2;

            ArrayList ColumnsNames = new ArrayList();
            if (indexColumnTime != -1)
                ColumnsNames.Add(dtTable.Columns[indexColumnTime].ColumnName);
            ArrayList ColumnsOrigin = new ArrayList();
            ColumnsOrigin.Add("");
            ///On définit par defaut des parametres tel que le type de courbe en ligne sur l'axe X
            ArrayList Visualisation = new ArrayList();
            Visualisation.Add("Line");
            ArrayList AxeRepresentation = new ArrayList();
            AxeRepresentation.Add("X");
            ArrayList StrokeCouleurs = new ArrayList();
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            ArrayList FillCouleurs = new ArrayList();
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            ArrayList Accumulation = new ArrayList();
            Accumulation.Add(Color.Transparent);

            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();
            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();
            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();

            ///La liste AxisList contient quatres listes de trois axes
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    ///Un axe contient une valeur de debut et de fin pour les axes de type Date time,
                    ///et debut et fin pour les axes de type numéric, et un booleen indiquant si on utilise le type DateTime
                    ///Par défaut, on met les variables à -1
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            ArrayList Position = new ArrayList();
            Position.Add(0);

            ///La liste PositionsXScrollbar contient quatres booleens par défaut à false.
            ///Ils permettent d'indiquer si la scrollbar est activée suivant les quatres positions de chart
            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            
            ArrayList ShowValues = new ArrayList();
            ShowValues.Add(false);

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            //Throughput Chart - bottom left
            for (int columnIndex = 0; columnIndex < dtTable.Columns.Count; columnIndex++)
            {
                String columnName = dtTable.Columns[columnIndex].ColumnName;
                if (GlobalNames.ebsThroughputChartColumnNamesList.Contains(columnName))
                {
                    ColumnsNames.Add(columnName);
                    ColumnsOrigin.Add("");
                    Visualisation.Add("Line");

                    if (columnName.Contains(GlobalNames.THROUGHPUT_COLUMN_IDENTIFIER)
                        || columnName.Contains(GlobalNames.THROUGHPUT_COLUMN_SHORT_IDENTIFIER))
                    {
                        AxeRepresentation.Add("Y2");
                    }
                    else
                        AxeRepresentation.Add("Y");

                    Position.Add(topLeftChartPosition);

                    StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));
                    FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));
                    Accumulation.Add(Color.Transparent);
                    ShowValues.Add(false);

                    MaxCandleValue.Add("");
                    MidCandleValue.Add("");
                    MinCandleValue.Add("");
                }
            }

            if (indexColumnConstrained != -1)
            {
                ColumnsNames.Add(dtTable.Columns[indexColumnConstrained].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                AxeRepresentation.Add("Y");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(indexColumnConstrained)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(indexColumnConstrained)));

                Position.Add(BottomLeftChartPosition);

                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);

                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            if (indexColumnUnconstrained != -1)
            {
                ColumnsNames.Add(dtTable.Columns[indexColumnUnconstrained].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Area");
                AxeRepresentation.Add("Y");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.Silver));//OverallTools.FonctionUtiles.getColor(2)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(Color.Silver));//OverallTools.FonctionUtiles.getColor(2)));

                Position.Add(BottomLeftChartPosition);

                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);

                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            
            
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, dtTable.TableName, false,
                                    ColumnsNames[0].ToString(),
                                    "Nb Bags",
                                    "Flow per Sliding Hour",
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }

        public static GraphicFilter getEbsIssuesChart(DataTable dtTable) // >> Task #9171 Pax2Sim - Static Analysis - EBS algorithm - Throughputs C#5
        {
            int indexColumnFlightTime = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_FLIGHT_COMPLETE_DATE_COLUMN_NAME);

            int indexColumnInputOverflowTime = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_INPUT_OVERFLOW_TIME_COLUMN_NAME);
            int indexColumnInputNbEcoBags = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_NB_INPUT_OVERFLOW_ECO_BAGS_COLUMN_NAME);
            int indexColumnInputNbFbBags = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_NB_INPUT_OVERFLOW_FB_BAGS_COLUMN_NAME);
            int indexColumnInputNbTotalBags = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_NB_INPUT_OVERFLOW_TOTAL_BAGS_COLUMN_NAME);

            int indexColumnOutputOverflowTime = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_OUTPUT_OVERFLOW_TIME_COLUMN_NAME);
            int indexColumnOutputNbEcoBags = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_NB_OUTPUT_OVERFLOW_ECO_BAGS_COLUMN_NAME);
            int indexColumnOutputNbFbBags = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_NB_OUTPUT_OVERFLOW_FB_BAGS_COLUMN_NAME);
            int indexColumnOutputNbTotalBags = dtTable.Columns.IndexOf(EbsLogger.EBS_ISSUES_NB_OUTPUT_OVERFLOW_TOTAL_BAGS_COLUMN_NAME);
            
            int topLeftChartPosition = 0;
            int BottomLeftChartPosition = 2;

            ArrayList ColumnsNames = new ArrayList();
            if (indexColumnFlightTime != -1)
                ColumnsNames.Add(dtTable.Columns[indexColumnFlightTime].ColumnName);
            ArrayList ColumnsOrigin = new ArrayList();
            ColumnsOrigin.Add("");
            ///On définit par defaut des parametres tel que le type de courbe en ligne sur l'axe X
            ArrayList Visualisation = new ArrayList();
            Visualisation.Add("Line");
            ArrayList AxeRepresentation = new ArrayList();
            AxeRepresentation.Add("X");
            ArrayList StrokeCouleurs = new ArrayList();
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            ArrayList FillCouleurs = new ArrayList();
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            ArrayList Accumulation = new ArrayList();
            Accumulation.Add(Color.Transparent);

            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();
            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();
            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();

            ///La liste AxisList contient quatres listes de trois axes
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    ///Un axe contient une valeur de debut et de fin pour les axes de type Date time,
                    ///et debut et fin pour les axes de type numéric, et un booleen indiquant si on utilise le type DateTime
                    ///Par défaut, on met les variables à -1
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            ArrayList Position = new ArrayList();
            Position.Add(0);

            ///La liste PositionsXScrollbar contient quatres booleens par défaut à false.
            ///Ils permettent d'indiquer si la scrollbar est activée suivant les quatres positions de chart
            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);

            ArrayList ShowValues = new ArrayList();
            ShowValues.Add(false);

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            for (int columnIndex = 0; columnIndex < dtTable.Columns.Count; columnIndex++)
            {

                if (columnIndex != indexColumnInputNbEcoBags && columnIndex != indexColumnInputNbFbBags && columnIndex != indexColumnInputNbTotalBags)
                {
                    continue;
                }
                ColumnsNames.Add(dtTable.Columns[columnIndex].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                AxeRepresentation.Add("Y");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));

                Position.Add(topLeftChartPosition);

                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);

                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            for (int columnIndex = 0; columnIndex < dtTable.Columns.Count; columnIndex++)
            {

                if (columnIndex != indexColumnOutputNbEcoBags && columnIndex != indexColumnOutputNbFbBags && columnIndex != indexColumnOutputNbTotalBags)
                {
                    continue;
                }
                ColumnsNames.Add(dtTable.Columns[columnIndex].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                AxeRepresentation.Add("Y");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));

                Position.Add(BottomLeftChartPosition);

                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);

                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }           
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, dtTable.TableName, false,
                                    ColumnsNames[0].ToString(),
                                    "Nb Bags",
                                    "",
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }

        // >> Task #9171 Pax2Sim - Static Analysis - EBS algorithm - Throughputs
        public static GraphicFilter getStaticGraphicFilter(DataTable dtTable, bool bSlidingHour)
        {            
            if(dtTable.Columns.Count<2)
                return null;

            if (dtTable.TableName.Contains("Distribution"))
                return getStaticGraphicDistribution(dtTable);

            if (dtTable.TableName.Contains("EBS")
                && !dtTable.TableName.Equals(GlobalNames.EBS_STATS_TABLE_NAME)  // >> Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
                && !dtTable.TableName.Equals(GlobalNames.EBS_INPUT_RATE_GENERIC_NAME_FOR_SCENARIOS)     // >> Task #10346 Pax2Sim - EBS review
                && !dtTable.TableName.Equals(GlobalNames.EBS_OUTPUT_RATE_GENERIC_NAME_FOR_SCENARIOS))   // << Task #10346 Pax2Sim - EBS review                   
            {
                if (dtTable.TableName.Contains("Issues"))   // >> Task #9171 Pax2Sim - Static Analysis - EBS algorithm - Throughputs C#5
                {
                    return getEbsIssuesChart(dtTable);
                }
                else
                {
                    if (!dtTable.TableName.Contains("Summary"))
                    {
                        return getStaticEBS(dtTable);
                    }
                }
            }

            // >> Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
            if (dtTable.TableName == GlobalNames.usaStandard_NumberOfETD_tableName ||
                dtTable.TableName == GlobalNames.usaStandard_NumberOfETD_short_tableName || // >> Task #10346 Pax2Sim - EBS review
                dtTable.TableName == GlobalNames.usaStandard_SortationRate_tableName)
            {
                return getStaticGraphicFilterForUSAStandardTables(dtTable);
            }
            // << Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory

            if ((!dtTable.TableName.Contains("Flow")) &&
                (!dtTable.TableName.Contains("AircraftMovements")) &&
                (!dtTable.TableName.Contains("Smoothed")) &&
                (!dtTable.TableName.Contains("Rolling")) &&
                (!dtTable.TableName.Contains("Instantaneous")) &&
                (!dtTable.TableName.Contains("flow")))
                return null;

            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<bool> PositionsXScrollbar = new List<bool>();
            ArrayList ShowValues = new ArrayList();

            List<float> PositionsBarWidth = new List<float>();
            
            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();
            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();
            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            //Abscisses
            ColumnsNames.Add(dtTable.Columns[0].ColumnName);
            ColumnsOrigin.Add("");
            Visualisation.Add("Line");
            AxeRepresentation.Add("X");
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            Accumulation.Add(Color.Transparent);

            Position.Add(0);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            ShowValues.Add(false);

            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            int iTmp = 1;
            String sLegendY2 = "Flow per Hour";//"Flow per sliding Hour";   // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
            String sLegendY = "";
            if ((dtTable.TableName.Contains("_Terminal")) ||
                (dtTable.TableName.Contains("_Airline")) ||
                (dtTable.TableName.Contains("_FC")))
            {
                sLegendY = "";
            }
            else
            {
                iTmp = 2;
                //2nd colonne
                ColumnsNames.Add(dtTable.Columns[1].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Bar");
                AxeRepresentation.Add("Y");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.FromArgb(194, 139, 255)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(Color.FromArgb(194, 139, 255)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            bool bY2 = false;
            for (int i = iTmp; i < dtTable.Columns.Count; i++)
            {
                if (dtTable.Columns[i].DataType == typeof(String))
                    continue;
                ColumnsNames.Add(dtTable.Columns[i].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                if ((i == 1) && !(dtTable.Columns[i].ColumnName.Contains("/ h")))
                    sLegendY2 = dtTable.Columns[i].ColumnName;
                if (dtTable.Columns[i].ColumnName.Contains("/ h") || dtTable.Columns[i].ColumnName.Contains("Throughput"))
                {
                    bY2 = true;
                    AxeRepresentation.Add("Y2");
                }
                else
                    AxeRepresentation.Add("Y");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(i)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(i)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            if (dtTable.TableName.Contains("AircraftMovements"))
            {
                sLegendY = "Nb Flights";
            }
            else if (!bY2)
            {
                sLegendY = "Flow per "/*sliding */+"Hour";
            }
            else
            {

                if (GlobalNames.staticAnalysisPaxTableNamesList.Contains(dtTable.TableName))   // >> Task #9108 Pax2Sim - Static Analysis - FPD+FPA split table
                {
                    sLegendY = "Nb Pax";
                }
                else if (dtTable.TableName.Contains("BHS"))  // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                    sLegendY = "Nb of Bags";
                else
                {
                    sLegendY = "Nb Pax/ Bags/ Trolleys";
                }
            }
            if (!bY2)
                sLegendY2 = "";
            return new GraphicFilter(ColumnsNames, 
                                    ColumnsOrigin, 
                                    Visualisation, 
                                    AxeRepresentation, 
                                    StrokeCouleurs, 
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,                                    
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, "  "+dtTable.TableName+"  ", false, 
                                    "Time",
                                    sLegendY,
                                    sLegendY2,
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }


        // >> Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
        public static List<String> USAStandardColumnNamesIncludedInChart = new List<String>() { GlobalNames.numberOfETD_ETD_columnName,
            GlobalNames.numberOfETD_ETDScreenLIT_columnName, GlobalNames.numberOfETD_ETDOOG_columnName, GlobalNames.numberOfETD_ETDforDOM_columnName,
            GlobalNames.numberOfETD_ETDSegPercDOMINT_columnName, GlobalNames.numberOfETD_RequirementsForOverSize_columnName,
            GlobalNames.sortationRateTable_SortationRate_columnName};
        
        public static GraphicFilter getStaticGraphicFilterForUSAStandardTables(DataTable dtTable)
        {
            if (dtTable.Columns.Count < 2)
                return null;
           
            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<bool> PositionsXScrollbar = new List<bool>();
            ArrayList ShowValues = new ArrayList();

            List<float> PositionsBarWidth = new List<float>();

            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();
            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();
            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();
            
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            //Abscisses
            ColumnsNames.Add(dtTable.Columns[0].ColumnName);
            ColumnsOrigin.Add("");
            Visualisation.Add("Line");
            AxeRepresentation.Add("X");
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            Accumulation.Add(Color.Transparent);

            Position.Add(0);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            ShowValues.Add(false);

            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            String sLegendY = "EDS Requirements";
            String sLegendY2 = "";

            int colorIndex = 0;
            for (int i = 1; i < dtTable.Columns.Count; i++)
            {
                
                if (dtTable.Columns[i].DataType == typeof(String))
                    continue;

                if (!USAStandardColumnNamesIncludedInChart.Contains(dtTable.Columns[i].ColumnName))
                    continue;
                colorIndex++;
                ColumnsNames.Add(dtTable.Columns[i].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                AxeRepresentation.Add("Y");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(colorIndex)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(colorIndex)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, "  " + dtTable.TableName + "  ", false,
                                    "Time",
                                    sLegendY,
                                    sLegendY2,
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());
        }
        // << Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory

        // << Task #9092 Pax2Sim - Static Analysis - Throughput Charts
        public static GraphicFilter getStaticGraphicFilterWithThroughputOnYAndNumberOnY2(DataTable dtTable, bool bSlidingHour)
        {
            if (dtTable.Columns.Count < 2)
                return null;
            if (dtTable.TableName.Contains("Distribution"))
                return getStaticGraphicDistribution(dtTable);
            if (dtTable.TableName.Contains("EBS"))
                return getStaticEBS(dtTable);
            if ((!dtTable.TableName.Contains("Flow")) &&
                (!dtTable.TableName.Contains("AircraftMovements")) &&
                (!dtTable.TableName.Contains("Smoothed")) &&
                (!dtTable.TableName.Contains("Rolling")) &&
                (!dtTable.TableName.Contains("Instantaneous")) &&
                (!dtTable.TableName.Contains("flow")))
                return null;
            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<bool> PositionsXScrollbar = new List<bool>();
            ArrayList ShowValues = new ArrayList();

            List<float> PositionsBarWidth = new List<float>();
            
            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();
            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();
            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            //Abscisses
            ColumnsNames.Add(dtTable.Columns[0].ColumnName);
            ColumnsOrigin.Add("");
            Visualisation.Add("Line");
            AxeRepresentation.Add("X");
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            Accumulation.Add(Color.Transparent);

            Position.Add(0);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            ShowValues.Add(false);

            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            int iTmp = 1;
            String sLegendY = "Flow per Hour";//"Flow per sliding Hour";   // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
            String sLegendY2 = "";
            if ((dtTable.TableName.Contains("_Terminal")) ||
                (dtTable.TableName.Contains("_Airline")) ||
                (dtTable.TableName.Contains("_FC"))
                || dtTable.TableName == GlobalNames.fpaInBoundSmoothedTableName        // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                || dtTable.TableName == GlobalNames.fpaInBoundInstantaneousTableName
                || dtTable.TableName == GlobalNames.fpdOutBoundSmoothedTableName
                || dtTable.TableName == GlobalNames.fpdOutBoundInstantaneousTableName
                || dtTable.TableName == GlobalNames.fpdFpaInOutBoundSmoothedTableName
                || dtTable.TableName == GlobalNames.fpdFpaInOutBoundInstantaneousTableName
                || dtTable.TableName == GlobalNames.FPD_FPA_PAXBoardingRoom_BHSMakeUpSmoothedTableName
                || dtTable.TableName == GlobalNames.FPD_FPA_PAXBoardingRoom_BHSMakeUpInstantaneousTableName
                || dtTable.TableName == GlobalNames.FPDCheckInShowUpSmoothedTableName
                || dtTable.TableName == GlobalNames.FPDCheckInShowUpInstantaneousTableName)    // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
            {
                sLegendY2 = "";
            }
            else
            {
                iTmp = 2;
                //2nd colonne
                ColumnsNames.Add(dtTable.Columns[1].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Bar");
                AxeRepresentation.Add("Y2");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.FromArgb(194, 139, 255)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(Color.FromArgb(194, 139, 255)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            bool bY2 = true;
            for (int i = iTmp; i < dtTable.Columns.Count; i++)
            {
                if (dtTable.Columns[i].DataType == typeof(String))
                    continue;
                ColumnsNames.Add(dtTable.Columns[i].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                if ((i == 1) && !(dtTable.Columns[i].ColumnName.Contains("/ h")))
                    sLegendY2 = dtTable.Columns[i].ColumnName;
                if (dtTable.Columns[i].ColumnName.Contains("/ h") || dtTable.Columns[i].ColumnName.Contains("Throughput"))
                {
                    bY2 = false;
                    AxeRepresentation.Add("Y");
                }
                else
                {                    
                    AxeRepresentation.Add("Y2");
                }
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(i)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(i)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            if (dtTable.TableName.Contains("AircraftMovements"))
            {
                sLegendY2 = "Nb Flights";
            }
            else if (!bY2)
            {
                sLegendY = "Flow per "/*sliding */+ "Hour";
            }
            else
            {
                if (dtTable.TableName.Contains("BHS"))  // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                    sLegendY2 = "Nb of Bags";
                else
                    sLegendY2 = "Nb Pax/ Bags/ Trolleys";
            }
            /*
            if (!bY2)
            {
                sLegendY2 = "";
            }*/

            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, "  " + dtTable.TableName + "  ", false,
                                    "Time",
                                    sLegendY,
                                    sLegendY2,
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }
        // >> Task #9092 Pax2Sim - Static Analysis - Throughput Charts

        #endregion

        #region Graphic Filters for Allocation
        // >> Bug #13367 Liege allocation
        public static GraphicFilter getGraphicFilterForTextGantt(DataTable dtTable, string allocationType)
        {
            if (dtTable.Columns.Count < 2)
                return null;

            int neededColumnIndex = dtTable.Columns.IndexOf(AllocationOutput.ALLOCATION_TEXT_GANT_NEEDED_COLUMN_NAME);
            int occupiedColumnIndex = dtTable.Columns.IndexOf(AllocationOutput.ALLOCATION_TEXT_GANT_OCCUPIED_COLUMN_NAME);
            int nbFlightsColumnIndex = dtTable.Columns.IndexOf(AllocationOutput.ALLOCATION_TEXT_GANT_NBFLIGHTS_COLUMN_NAME);

            if (neededColumnIndex == -1 || occupiedColumnIndex == -1 || nbFlightsColumnIndex == -1)
                return null;

            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<bool> PositionsXScrollbar = new List<bool>();
            ArrayList ShowValues = new ArrayList();

            List<float> PositionsBarWidth = new List<float>();

            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();
            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();
            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            //Abscisses
            ColumnsNames.Add(dtTable.Columns[0].ColumnName);
            ColumnsOrigin.Add("");
            Visualisation.Add("Line");
            AxeRepresentation.Add("X");
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            Accumulation.Add(Color.Transparent);

            Position.Add(0);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            ShowValues.Add(false);

            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            Color neededColumnColor = OverallTools.FonctionUtiles.getColor(1);  //Color.DeepSkyBlue;
            ColumnsNames.Add(dtTable.Columns[neededColumnIndex].ColumnName);
            ColumnsOrigin.Add("");
            Visualisation.Add("Line");
            AxeRepresentation.Add("Y");
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(neededColumnColor));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(neededColumnColor));
            Accumulation.Add(Color.Transparent);
            Position.Add(0);
            ShowValues.Add(false);
            
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            Color occupiedColumnColor = OverallTools.FonctionUtiles.getColor(2);    //Color.DarkBlue;
            ColumnsNames.Add(dtTable.Columns[occupiedColumnIndex].ColumnName);
            ColumnsOrigin.Add("");
            Visualisation.Add("Area");
            AxeRepresentation.Add("Y");
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(occupiedColumnColor));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(occupiedColumnColor));
            Accumulation.Add(Color.Transparent);
            Position.Add(0);
            ShowValues.Add(false);
            
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");
            /*
            ColumnsNames.Add(dtTable.Columns[nbFlightsColumnIndex].ColumnName);
            ColumnsOrigin.Add("");
            Visualisation.Add("Line");
            AxeRepresentation.Add("Y");
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.OrangeRed));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(Color.OrangeRed));
            Accumulation.Add(Color.Transparent);
            Position.Add(0);
            ShowValues.Add(false);
            */
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");
            
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, allocationType, false,
                                    "Time",
                                    "",//sLegendY,
                                    "",//sLegendY2,
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }
        // << Bug #13367 Liege allocation
        #endregion

        #region La fonction pour le mode de visualisation par défaut des tables
        public static GraphicFilter getGraphicFilter(DataTable table)
        {
            if (table.Columns.Count == 0)
                return null;
            bool bUtilization = table.TableName.Contains("Utilization");
            bool bIST = table.TableName.EndsWith("IST") || table.TableName.EndsWith("IST_FromTo");
            ///Booleen permetant de savoir si la table est une table d'occupation
            bool bOccupation = table.TableName.EndsWith("Occupation") || table.TableName.EndsWith(AllocationOutput.OCCUPATION_ISSUES_TABLE_NAME)
                || table.TableName.Contains("Occupation_From");
            // << Task #7868 Scenario - Utilization chart default columns used
            bool bUtilizationForGroups = (table.TableName.Contains("Group") && table.TableName.Contains("Utilization"));
            // >> Task #7868 Scenario - Utilization chart default columns used


            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            ArrayList ShowValues = new ArrayList();

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();

            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            Type type = table.Columns[0].DataType;
            int iColumn = 0;
            if ((type == typeof(DateTime)) || (type == typeof(int)) ||
                (type == typeof(Int32)) || (type == typeof(Int16)) ||
                (type == typeof(Int64)) || (type == typeof(Double)) ||
                (type == typeof(double)))
            {
                // Il y a une colonne en abscisse.
                ColumnsNames.Add(table.Columns[0].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                AxeRepresentation.Add("X");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                iColumn++;
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
                        
            bool bY2 = false;
            for (; iColumn < table.Columns.Count; iColumn++)
            {
                String ColumnName = table.Columns[iColumn].ColumnName;                
                if (bOccupation && ColumnName == "Slot-End Occupation")
                    continue;

                // << Task #7868 Scenario - Utilization chart default columns used
                if (bUtilizationForGroups)
                {
                    if (Array.IndexOf(Model.UTILIZATION_CHART_COLUMNS, ColumnName) == -1)
                        continue;
                }
                // >> Task #7868 Scenario - Utilization chart default columns used

                if ((bIST) && (!ColumnName.Contains("TotalTime"))
                     && (!ColumnName.Contains("Total time"))
                     && (!ColumnName.Contains("Total Time"))
                    && (!ColumnName.Contains(GlobalNames.BHS_IST_TABLE_OCCUPATION_TIME_COLUMN_NAME)))   // >> Task #13422 Keywords improvement
                    continue;
                type = table.Columns[iColumn].DataType;
                if (!((type == typeof(Int32)) || (type == typeof(Int16)) ||
                (type == typeof(Int64)) || (type == typeof(Double)) ||
                (type == typeof(double))))
                    continue;
                if (bUtilization && (ColumnName == "Total"))
                    continue;
                ColumnsNames.Add(ColumnName);
                ColumnsOrigin.Add("");
                if (bIST)
                {
                    Visualisation.Add("Point");
                    AxeRepresentation.Add("Y");
                    Position.Add(0);

                }
                else if (bUtilization && ColumnName == GlobalNames.GROUP_UTILIZATION_DESK_NEED_COLUMN_NAME)
                {
                    // << Task #7911 Scenario - Utilization chart default columns used II 
                    Visualisation.Add("Bar");   // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                    AxeRepresentation.Add("Y2");
                    bY2 = true;
                    //Visualisation.Add(Model.chartLineVisualisationType);
                    //AxeRepresentation.Add(Model.chartAxeRepresentationY);                    
                    // >> Task #7911 Scenario - Utilization chart default columns used II 
                    Position.Add(0);
                }
                else if (bUtilization && ColumnName == GlobalNames.GROUP_UTILIZATION_AVERAGE_COLUMN_NAME)   // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                {
                    Visualisation.Add("Line");
                    AxeRepresentation.Add("Y");
                    Position.Add(0);
                }
                ///Si la table contient une colonne Average Occupation, on ajoute cette colonne ainsi que Min Occupation et Max Occupation
                ///à la candle list à la position bottom left
                else if (bOccupation && (ColumnName == "Average Occupation")) 
                {
                    CandleColumnsList.Add("Average Occupation");
                    CandleColumnsList.Add("Min Occupation");
                    CandleColumnsList.Add("Max Occupation");

                    AxeRepresentation.Add("Y");
                    Visualisation.Add("Candle list");
                    Position.Add(2);
                }
                /// Si la colonne est Min Occupation ou Max Occupation, on met comme type de courbe candle list en bottom left
                else if (bOccupation && (ColumnName == "Min Occupation" || ColumnName == "Max Occupation"))
                {
                    AxeRepresentation.Add("Y");
                    Visualisation.Add("Candle list");
                    Position.Add(2);
                }
                else
                {
                    Visualisation.Add("Line");
                    if (ColumnName.Contains("/ h") || ColumnName.Contains("Throughput"))
                    {
                        bY2 = true;
                        AxeRepresentation.Add("Y2");
                    }
                    else
                        AxeRepresentation.Add("Y");
                    Position.Add(0);
                }
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(iColumn)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(iColumn)));
                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }

            if (bOccupation && table.Columns.Contains("Slot-End Occupation"))
            {
                int columnIndex = table.Columns.IndexOf("Slot-End Occupation");
                type = table.Columns["Slot-End Occupation"].DataType;
                ColumnsNames.Add("Slot-End Occupation");
                ColumnsOrigin.Add("");

                AxeRepresentation.Add("Y");
                Visualisation.Add("Line");
                Position.Add(2);

                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));
                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }

            String sLegendY1 = "";
            String sLegendY2 = "";
            if ((bY2) && (!bUtilization))
                sLegendY2 = "Flow per sliding Hour";
            else if (bY2)
                sLegendY2 = "Nb. Stations";
            if (bIST)
                sLegendY1 = "mn";
            else if (bUtilization)
            {
                // << Task #7911 Scenario - Utilization chart default columns used II                
                sLegendY1 = "%";
                //sLegendY1 = Model.chartAxisNameNbOfStations;
                // >> Task #7911 Scenario - Utilization chart default columns used II
            }
            /// Si table Occupation, le titre de l'axe Y est Nb Pax
            else if (bOccupation)
                sLegendY1 = "Nb. per Slot";
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,                                    
                                    SetPointIsActivated,
                                    ListAnnotation ,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    table.TableName,
                                    table.TableName,
                                    false,
                                    "Time",
                                    sLegendY1,
                                    sLegendY2,
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());
        }

        private static GraphicFilter getLocalISTGraphicFilter(DataTable table)  // >> Task #10877 Pax2Sim - BHS analysis - local IST tables for all the objects
        {
            if (table.Columns.Count == 0)
                return null;
            
            bool bIST = table.TableName.EndsWith("IST");            
            int occupationTimeColumnIndex = table.Columns.IndexOf(GlobalNames.BHS_IST_TABLE_OCCUPATION_TIME_COLUMN_NAME);
            int arrivalTimeAtStationColumnIndex = table.Columns.IndexOf(GlobalNames.BHS_IST_TABLE_ARRIVING_TIME_COLUMN_NAME);

            if (occupationTimeColumnIndex == -1 || arrivalTimeAtStationColumnIndex == -1)
                return null;

            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            ArrayList ShowValues = new ArrayList();

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();

            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();

            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            Type type = table.Columns[occupationTimeColumnIndex].DataType;
            int iColumn = 0;
            if ((type == typeof(DateTime)) || (type == typeof(int)) ||
                (type == typeof(Int32)) || (type == typeof(Int16)) ||
                (type == typeof(Int64)) || (type == typeof(Double)) ||
                (type == typeof(double)))
            {
                // Il y a une colonne en abscisse.
                ColumnsNames.Add(table.Columns[arrivalTimeAtStationColumnIndex].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                AxeRepresentation.Add("X");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                iColumn = iColumn + 3;
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
                       
            String ColumnName = table.Columns[occupationTimeColumnIndex].ColumnName;                
            ColumnsNames.Add(ColumnName);
            ColumnsOrigin.Add("");
            
            Visualisation.Add("Point");
            AxeRepresentation.Add("Y");
            Position.Add(0);
            
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(iColumn)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(iColumn)));
            Accumulation.Add(Color.Transparent);
            ShowValues.Add(false);
            
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");
                                    
            String sLegendY1 = "mn";
            String sLegendY2 = "";            
            
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    table.TableName,
                                    table.TableName,
                                    false,
                                    "Time",
                                    sLegendY1,
                                    sLegendY2,
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());
        }
        #endregion

        #region La fonction pour le mode de visualisation par défaut des tables de répartition
        public static GraphicFilter getGraphicFilterRepartition(DataTable table)
        {
            if (table.Columns.Count == 0)
                return null;

            if (table.TableName.StartsWith(GlobalNames.PAX_DISTRIBUTION_TABLE_PREFIX) 
                && table.TableName.EndsWith(GlobalNames.PAX_SUMMARY_DISTRIBUTION_TABLE_SUFFIX))
            {
                return null;
            }

            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            ArrayList ShowValues = new ArrayList();            
            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();

            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            Type type = table.Columns[0].DataType;
            int iColumn = 0;
            if ((type == typeof(DateTime)) || (type == typeof(int)) ||
                (type == typeof(Int32)) || (type == typeof(Int16)) ||
                (type == typeof(Int64)) || (type == typeof(Double)) ||
                (type == typeof(double)) || (type == typeof(string)) || (type == typeof(String)))
            {
                // Il y a une colonne en abscisse.

                ColumnsNames.Add(table.Columns[0].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                AxeRepresentation.Add("X");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                iColumn++;
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }
            string y2Title = "";
            for (; iColumn < table.Columns.Count; iColumn++)
            {
                // << Task #8283 Pax2Sim - BHS mode - statistics for transportaition time per period of time                
                if (!table.TableName.Contains("Timeline") 
                    && !table.TableName.Contains("Dwell Area"))     // << Task #8589 Pax2Sim - Waiting Time Per Period of Time for PAX
                {
                    if (((!table.Columns[iColumn].ColumnName.StartsWith("%")) &&
                        (!table.Columns[iColumn].ColumnName.Contains("(%)"))) ||
                        (table.Columns[iColumn].ColumnName.Contains("Cumul")))
                        continue;
                }
                else
                {
                    // << Task #8618 Pax2Sim - Dynamic Analysis - Pax - histograns and timeline tables for Dwell Area
                    if (table.TableName.Contains("Pax_Time_Distrib"))
                    {
                        if ((table.Columns[iColumn].ColumnName.Contains("Min")
                            || table.Columns[iColumn].ColumnName.Contains("Max")
                            || table.Columns[iColumn].ColumnName.Contains("Average"))
                            && (table.Columns[iColumn].ColumnName.Contains("Walking Time")
                                || table.Columns[iColumn].ColumnName.Contains("Waiting Time")
                                || table.Columns[iColumn].ColumnName.Contains("Process Time")
                                || table.Columns[iColumn].ColumnName.Contains("Time To Boarding Gate")))
                        {
                            continue;
                        }
                        if (table.Columns[iColumn].ColumnName.Contains("%"))
                            continue;
                        if (table.Columns[iColumn].ColumnName.Equals("Time")) //|| table.Columns[iColumn].ColumnName.Equals("Nb Pax"))
                            continue;
                    // >> Task #8618 Pax2Sim - Dynamic Analysis - Pax - histograns and timeline tables for Dwell Area
                    // << Task #8321 Pax2Sim - BHS mode - Charts for Timeline statistic table                    
                    }
                    else if (!table.Columns[iColumn].ColumnName.Contains("Average") && !table.Columns[iColumn].ColumnName.Contains("Min")
                        && !table.Columns[iColumn].ColumnName.Contains("Max") && !table.Columns[iColumn].ColumnName.Contains("Nb Bags")    // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                        && !table.Columns[iColumn].ColumnName.Contains("Nb Pax"))
                    {
                        continue;
                    }
                    // >> Task #8321 Pax2Sim - BHS mode - Charts for Timeline statistic table
                }
                // >> Task #8283 Pax2Sim - BHS mode - statistics for transportaition time per period of time

                type = table.Columns[iColumn].DataType;
                if (!((type == typeof(Int32)) || (type == typeof(Int16)) ||
                (type == typeof(Int64)) || (type == typeof(Double)) ||
                (type == typeof(double))))
                    continue;
                ColumnsNames.Add(table.Columns[iColumn].ColumnName);
                ColumnsOrigin.Add("");

                // << Task #8283 Pax2Sim - BHS mode - statistics for transportaition time per period of time 
                // << Task #8618 Pax2Sim - Dynamic Analysis - Pax - histograns and timeline tables for Dwell Area
                if (table.TableName.Contains("Pax_Time_Distrib") && table.TableName.Contains("Timeline"))
                {
                    if (table.Columns[iColumn].ColumnName.Contains("Average") 
                        && table.Columns[iColumn].ColumnName.Contains("Total Time"))
                    {
                        CandleColumnsList.Add("Average Total Time");
                        CandleColumnsList.Add("Min Total Time");
                        CandleColumnsList.Add("Max Total Time");

                        Visualisation.Add("Candle list");
                    }
                    else if (table.Columns[iColumn].ColumnName.Contains("Nb Pax"))
                    {
                        Visualisation.Add("Bar");
                    }
                    else if ((table.Columns[iColumn].ColumnName.Contains("Min") || table.Columns[iColumn].ColumnName.Contains("Max"))
                        && (table.Columns[iColumn].ColumnName.Contains("Total Time")))
                    {
                        Visualisation.Add("Candle list");
                    }
                    // >> Task #8618 Pax2Sim - Dynamic Analysis - Pax - histograns and timeline tables for Dwell Area
                } 
                else if (table.TableName.Contains("Timeline"))
                {
                    // << Task #8321 Pax2Sim - BHS mode - Charts for Timeline statistic table
                    if (table.Columns[iColumn].ColumnName.Contains("Average"))
                    {
                        CandleColumnsList.Add("Average Travel Time");
                        CandleColumnsList.Add("Min Travel Time");
                        CandleColumnsList.Add("Max Travel Time");
                                                
                        Visualisation.Add("Candle list");                        
                    }
                    else if (table.Columns[iColumn].ColumnName.Contains("Min") || table.Columns[iColumn].ColumnName.Contains("Max"))
                    {                        
                        Visualisation.Add("Candle list");
                    }
                    else if (table.Columns[iColumn].ColumnName.Contains("Nb Bags"))
                    {
                        Visualisation.Add("Bar");
                    }
                    // >> Task #8321 Pax2Sim - BHS mode - Charts for Timeline statistic table
                }
                else if (table.TableName.Contains("Dwell Area"))  // << Task #8589 Pax2Sim - Waiting Time Per Period of Time for PAX
                {
                    if (table.Columns[iColumn].ColumnName.Contains("Average"))
                    {
                        CandleColumnsList.Add(GlobalNames.BHS_DWELL_AREA_AVG_COLUMN_NAME);
                        CandleColumnsList.Add(GlobalNames.BHS_DWELL_AREA_MIN_COLUMN_NAME);
                        CandleColumnsList.Add(GlobalNames.BHS_DWELL_AREA_MAX_COLUMN_NAME);

                        Visualisation.Add("Candle list");
                    }
                    else if (table.Columns[iColumn].ColumnName.Contains("Min") || table.Columns[iColumn].ColumnName.Contains("Max"))
                    {
                        Visualisation.Add("Candle list");
                    }
                    else if (table.Columns[iColumn].ColumnName.Contains("Nb Bags") || table.Columns[iColumn].ColumnName.Contains("Nb Pax"))
                    {
                        Visualisation.Add("Bar");
                    }
                    // >> Task #8589 Pax2Sim - Waiting Time Per Period of Time for PAX
                } else
                    Visualisation.Add("Bar");
                // >> Task #8283 Pax2Sim - BHS mode - statistics for transportaition time per period of time

                if ((table.TableName.Contains("Dwell Area") || table.TableName.Contains("Timeline"))
                    && table.Columns[iColumn].ColumnName.Contains("Nb Bags"))  // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                {
                    AxeRepresentation.Add("Y2");
                    y2Title = table.Columns[iColumn].ColumnName;
                    ShowValues.Add(false);
                }
                else if ((table.TableName.Contains("Dwell Area") || (table.TableName.Contains("Pax_Time_Distrib") && table.TableName.Contains("Timeline"))) 
                        && table.Columns[iColumn].ColumnName.Contains("Nb Pax"))  // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                {
                    AxeRepresentation.Add("Y2");
                    y2Title = "Nb Pax";
                    ShowValues.Add(false);
                }
                else
                {
                    AxeRepresentation.Add("Y");
                    ShowValues.Add(true);
                }
                

                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(iColumn)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(iColumn)));
                
                Color cTmp = OverallTools.FonctionUtiles.getColor(iColumn);
                int iG = FonctionsType.getInt(cTmp.G);
                if (iG > 175)
                    iG -= 50;
                else
                    iG += 50;
                if (table.Columns[iColumn].ColumnName == "Nb Bags" || table.Columns[iColumn].ColumnName == "Nb Pax")
                {
                    cTmp = Color.Transparent;
                }
                else
                {
                    cTmp = Color.FromArgb(cTmp.A, cTmp.R, iG, cTmp.B);                    
                }
                Accumulation.Add(cTmp);
                
                Position.Add(0);
                //ShowValues.Add(true);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }

            // << Task #8283 Pax2Sim - BHS mode - statistics for transportaition time per period of time            
            String xTitle = "";
            String yTitle = "%";            
            // << Task #8618 Pax2Sim - Dynamic Analysis - Pax - histograns and timeline tables for Dwell Area
            if (table.TableName.Contains("Pax_Time_Distrib") && table.TableName.Contains("Timeline"))
            {
                xTitle = "Time";
                yTitle = "Average Total Time";
                // >> Task #8618 Pax2Sim - Dynamic Analysis - Pax - histograns and timeline tables for Dwell Area
            } 
            else if (table.TableName.Contains("Timeline"))
            {
                xTitle = "Time";
                yTitle = "Time (mn)"; // Task #16728 C#42
            }
            else if (table.TableName.Contains("Dwell Area"))      // << Task #8589 Pax2Sim - Waiting Time Per Period of Time for PAX
            {
                xTitle = "Time";
                yTitle = "Time (mn)"; // Task #16728 C#42    // "Average Dwell Area";
            }                                                       // >> Task #8589 Pax2Sim - Waiting Time Per Period of Time for PAX
            // >> Task #8283 Pax2Sim - BHS mode - statistics for transportaition time per period of time

            return new GraphicFilter(ColumnsNames, 
                ColumnsOrigin, 
                Visualisation, 
                AxeRepresentation, 
                StrokeCouleurs, 
                FillCouleurs,
                Accumulation, 
                SetpointAxis,
                SetpointIsArea,
                SetpointStrokeColor,
                SetpointFillColor,
                SetpointValue,
                SetpointValue2,
                SetpointBDateTime,
                SetpointEDateTime,                
                SetPointIsActivated,
                ListAnnotation,
                CandleColumnsList,
                MaxCandleValue,
                MidCandleValue,
                MinCandleValue,
                AxisList,
                Position, 
                PositionsXScrollbar,
                ShowValues, 
                table.TableName,  
                table.TableName, 
                false,
                xTitle,     // << Task #8283 Pax2Sim - BHS mode - statistics for transportaition time per period of time                
                yTitle, 
                y2Title,
                true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }

        #endregion

        #region Les fonctions pour la gestion des visualisation des tables résultats de la partie BHS
        public static GraphicFilter getBHSOccupationGraphicFilter(DataTable dtTable)
        {
            if (isLocalISTTable(dtTable))
                return getLocalISTGraphicFilter(dtTable);
            return getBHSGraphicFilter(dtTable);
        }

        public static GraphicFilter getBHSGraphicFilter(DataTable table)
        {
            if (table.Columns.Count == 0)
                return null;
            bool bUtilization = table.TableName.Contains("Utilization");
            bool bUtilizationForGroups = table.TableName.Contains("Utilization") && table.TableName.Contains("_0_");
            bool bIST = table.TableName.EndsWith("IST");
            bool bISTFromTo = table.TableName.EndsWith("IST_FromTo");
            ///Booleen permetant de savoir si la table est une table d'occupation
            bool bOccupation = table.TableName.EndsWith("Occupation") || table.TableName.EndsWith(AllocationOutput.OCCUPATION_ISSUES_TABLE_NAME)
                || table.TableName.Contains("Occupation_From");

            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            ArrayList ShowValues = new ArrayList();

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            List<String> SetpointAxis = new List<String>();
            ArrayList SetpointStrokeColor = new ArrayList();
            ArrayList SetpointFillColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();

            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            Type type = table.Columns[0].DataType;
            int iColumn = 0;
            if ((type == typeof(DateTime)) || (type == typeof(int)) ||
                (type == typeof(Int32)) || (type == typeof(Int16)) ||
                (type == typeof(Int64)) || (type == typeof(Double)) ||
                (type == typeof(double)))
            {
                // Il y a une colonne en abscisse.
                ColumnsNames.Add(table.Columns[0].ColumnName);
                ColumnsOrigin.Add("");
                Visualisation.Add("Line");
                AxeRepresentation.Add("X");
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
                Accumulation.Add(Color.Transparent);
                Position.Add(0);
                iColumn++;
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }

            bool bY2 = false;
            for (; iColumn < table.Columns.Count; iColumn++)
            {
                String ColumnName = table.Columns[iColumn].ColumnName;
                if (bOccupation && ColumnName == "Slot-End Occupation")
                    continue;
                if (bUtilization) // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                {
                    if (bUtilizationForGroups && Array.IndexOf(Model.BHS_UTILIZATION_FOR_GROUPS_CHART_COLUMNS, ColumnName) == -1)
                    {
                        continue;
                    }
                    else if (!bUtilizationForGroups && Array.IndexOf(Model.BHS_UTILIZATION_FOR_STATIONS_CHART_COLUMNS, ColumnName) == -1)
                    {
                        continue;
                    }
                }
                if (bIST)
                {
                    if (!ColumnName.Equals(GlobalNames.TOTAL_TIME_FROM_QUEUE_TO_EXIT_COLUMN_NAME)
                        && !ColumnName.Equals(GlobalNames.TOTAL_TIME_FROM_CI_TO_EXIT_COLUMN_NAME)
                        && !ColumnName.Equals(GlobalNames.TOTAL_TIME_FROM_CI_COLL_TO_EXIT_COLUMN_NAME)
                        && !ColumnName.Equals(GlobalNames.TOTAL_TIME_FROM_TRANSFER_COLL_TO_EXIT_COLUMN_NAME))
                    {
                        continue;
                    }
                }
                else if (bISTFromTo)
                {
                    if (!ColumnName.Equals(GlobalNames.TOTAL_TIME_FROM_QUEUE_TO_EXIT_COLUMN_NAME)
                        && !ColumnName.Equals(GlobalNames.TOTAL_TIME_FROM_CI_TO_EXIT_COLUMN_NAME)
                        && !ColumnName.Equals(GlobalNames.TOTAL_TIME_FROM_CI_COLL_TO_EXIT_COLUMN_NAME)
                        && !ColumnName.Equals(GlobalNames.TOTAL_TIME_FROM_TRANSFER_COLL_TO_EXIT_COLUMN_NAME)
                        && !ColumnName.Contains("min"))
                    {
                        continue;
                    }
                }


                type = table.Columns[iColumn].DataType;
                if (!((type == typeof(Int32)) || (type == typeof(Int16)) ||
                (type == typeof(Int64)) || (type == typeof(Double)) ||
                (type == typeof(double))))
                    continue;
                if (bUtilization && (ColumnName == "Total"))
                    continue;
                ColumnsNames.Add(ColumnName);
                ColumnsOrigin.Add("");
                if (bIST || bISTFromTo)
                {
                    Visualisation.Add("Point");
                    AxeRepresentation.Add("Y");
                    Position.Add(0);

                }
                else if (bUtilization && (ColumnName == GlobalNames.BHS_UTILIZATION_STATION_NEED_COLUMN_NAME))
                {
                    // << Task #7911 Scenario - Utilization chart default columns used II 
                    Visualisation.Add("Bar");   // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                    AxeRepresentation.Add("Y2");
                    bY2 = true;
                    //Visualisation.Add(Model.chartLineVisualisationType);
                    //AxeRepresentation.Add(Model.chartAxeRepresentationY);
                    // >> Task #7911 Scenario - Utilization chart default columns used II 
                    Position.Add(0);
                }
                else if (bUtilization && (ColumnName == GlobalNames.BHS_UTILIZATION_AVERAGE_COLUMN_NAME))   // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                {
                    Visualisation.Add("Line");
                    AxeRepresentation.Add("Y");
                    Position.Add(0);
                }
                ///Si la table contient une colonne Average Occupation, on ajoute cette colonne ainsi que Min Occupation et Max Occupation
                ///à la candle list à la position bottom left
                else if (bOccupation && (ColumnName == "Average Occupation"))
                {
                    CandleColumnsList.Add("Average Occupation");
                    CandleColumnsList.Add("Min Occupation");
                    CandleColumnsList.Add("Max Occupation");

                    AxeRepresentation.Add("Y");
                    Visualisation.Add("Candle list");
                    Position.Add(2);
                }
                /// Si la colonne est Min Occupation ou Max Occupation, on met comme type de courbe candle list en bottom left
                else if (bOccupation && (ColumnName == "Min Occupation" || ColumnName == "Max Occupation"))
                {
                    AxeRepresentation.Add("Y");
                    Visualisation.Add("Candle list");
                    Position.Add(2);
                }
                else
                {
                    Visualisation.Add("Line");
                    if (ColumnName.Contains("/ h") || ColumnName.Contains("Throughput"))
                    {
                        bY2 = true;
                        AxeRepresentation.Add("Y2");
                    }
                    else
                        AxeRepresentation.Add("Y");
                    Position.Add(0);
                }
                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(iColumn)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(iColumn)));
                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }

            if (bOccupation && table.Columns.Contains("Slot-End Occupation"))
            {
                int columnIndex = table.Columns.IndexOf("Slot-End Occupation");
                type = table.Columns["Slot-End Occupation"].DataType;
                ColumnsNames.Add("Slot-End Occupation");
                ColumnsOrigin.Add("");

                AxeRepresentation.Add("Y");
                Visualisation.Add("Line");
                Position.Add(2);

                StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));
                FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(columnIndex)));
                Accumulation.Add(Color.Transparent);
                ShowValues.Add(false);
                
                MaxCandleValue.Add("");
                MidCandleValue.Add("");
                MinCandleValue.Add("");
            }

            String sLegendY1 = "";
            String sLegendY2 = "";
            if ((bY2) && (!bUtilization))
                sLegendY2 = "Flow per sliding Hour";
            else if (bY2)
                sLegendY2 = "Nb. Stations";
            if (bIST || bISTFromTo)
                sLegendY1 = "mn";
            else if (bUtilization)
            {
                // << Task #7911 Scenario - Utilization chart default columns used II                
                sLegendY1 = "%";    // >> Task #18306 PAX2SIM - BHS - Sorter occupation
                //sLegendY1 = Model.chartAxisNameNbOfStations;
                // >> Task #7911 Scenario - Utilization chart default columns used II
            }
            /// Si table Occupation, le titre de l'axe Y est Nb Pax
            else if (bOccupation)
                sLegendY1 = "Nb. per Slot";
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation,
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    table.TableName,
                                    table.TableName,
                                    false,
                                    "Time",
                                    sLegendY1,
                                    sLegendY2,
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());
        }
                
        private static bool isLocalISTTable(DataTable table)
        {
            if (table == null)
                return false;
            if (table.TableName.EndsWith("IST")
                && table.Columns.IndexOf(GlobalNames.BHS_IST_TABLE_BAG_ID_COLUMN_NAME) != -1
                && table.Columns.IndexOf(GlobalNames.BHS_IST_TABLE_PAX_ID_COLUMN_NAME) != -1
                && table.Columns.IndexOf(GlobalNames.BHS_IST_TABLE_ARRIVING_TIME_COLUMN_NAME) != -1
                && table.Columns.IndexOf(GlobalNames.BHS_IST_TABLE_OCCUPATION_TIME_COLUMN_NAME) != -1)
            {
                return true;
            }
            return false;
        }

        public static GraphicFilter getBHSRemainingDistribution(DataTable dtTable, String sColumnName)
        {
            GraphicFilter gfGraphic = GetDistributionGraphicFilter(dtTable,sColumnName);
            gfGraphic.XTitle = "Remaining minutes";
            return gfGraphic;
        }
        public static GraphicFilter getBHSDistributionGraphicFilter(DataTable dtTable)
        {
            if (dtTable.Columns.Count < 2)
                return null;
            ArrayList ColumnsNames = new ArrayList();
            ColumnsNames.Add(dtTable.Columns[0].ColumnName);
            ColumnsNames.Add(dtTable.Columns[2].ColumnName);
            ArrayList ColumnsOrigin = new ArrayList();
            ColumnsOrigin.Add("");
            ColumnsOrigin.Add("");
            ArrayList Visualisation = new ArrayList();
            Visualisation.Add("Line");
            Visualisation.Add("Bar");

            ArrayList AxeRepresentation = new ArrayList();
            AxeRepresentation.Add("X");
            AxeRepresentation.Add("Y");

            ArrayList StrokeCouleurs = new ArrayList();

            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.FromArgb(194, 139, 255)));

            ArrayList FillCouleurs = new ArrayList();

            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(Color.FromArgb(194, 180, 255)));

            ArrayList SetpointStrokeColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            ArrayList SetpointFillColor = new ArrayList();
            List<String> SetpointAxis = new List<String>();

            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();

            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>> AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");


            ArrayList Accumulation = new ArrayList();
            Accumulation.Add(Color.Transparent);
            Accumulation.Add(Color.FromArgb(194, 180, 255));
            ArrayList Position = new ArrayList();
            Position.Add(0);
            Position.Add(0);

            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);

            ArrayList ShowValues = new ArrayList();
            ShowValues.Add(false);
            ShowValues.Add(false);

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            
            int iIndexCumul = dtTable.Columns.IndexOf("Cumul");
            if (iIndexCumul != -1)
            {
                if (iIndexCumul + 1 != dtTable.Columns.Count)
                {
                    for (int i = iIndexCumul + 1; i < dtTable.Columns.Count; i++)
                    {
                        ColumnsNames.Add(dtTable.Columns[i].ColumnName);
                        ColumnsOrigin.Add("");
                        Visualisation.Add("Line");
                        AxeRepresentation.Add("Y2");
                        StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(i + 1)));
                        FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(i + 1)));
                        Accumulation.Add(Color.Transparent);
                        Position.Add(0);
                        ShowValues.Add(false);
                        
                        MaxCandleValue.Add("");
                        MidCandleValue.Add("");
                        MinCandleValue.Add("");
                    }
                }
            }
            String XTitle = "Cycle Time (" + ColumnsNames[0].ToString() + ")";
            bool bInvertAbscissa = false;
            if (dtTable.TableName.Contains("RemainingTime"))
            {
                XTitle = ColumnsNames[0].ToString();
                bInvertAbscissa = true;
            }
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation, 
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,                                    
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, dtTable.TableName, bInvertAbscissa, 
                                    XTitle,
                                    "% Bags",
                                    "Cumul. %",
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }
        #endregion

        public static GraphicFilter GetDistributionGraphicFilter(DataTable dtTable, String sColumnName)
        {
            ArrayList ColumnsNames = new ArrayList();
            ColumnsNames.Add(dtTable.Columns[0].ColumnName);
            ColumnsNames.Add(sColumnName);
            ArrayList ColumnsOrigin = new ArrayList();
            ColumnsOrigin.Add("");
            ColumnsOrigin.Add("");
            ArrayList Visualisation = new ArrayList();
            Visualisation.Add("Line");
            Visualisation.Add("Bar");

            ArrayList AxeRepresentation = new ArrayList();
            AxeRepresentation.Add("X");
            AxeRepresentation.Add("Y");

            ArrayList StrokeCouleurs = new ArrayList();

            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.FromArgb(194, 139, 255)));

            ArrayList FillCouleurs = new ArrayList();

            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(Color.FromArgb(194, 180, 255)));

          
            ArrayList Accumulation = new ArrayList();
            Accumulation.Add(Color.Transparent);
            Accumulation.Add(Color.FromArgb(210, 210, 255));
            ArrayList Position = new ArrayList();
            Position.Add(0);
            Position.Add(0);

            List<bool> PositionsXScrollbar = new List<bool>();
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);
            PositionsXScrollbar.Add(false);

            ArrayList ShowValues = new ArrayList();
            ShowValues.Add(false);
            ShowValues.Add(false);

            List<float> PositionsBarWidth = new List<float>();
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);
            PositionsBarWidth.Add(0.0f);

            List<float> PositionsGapPercent = new List<float>();
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);
            PositionsGapPercent.Add(0.0f);

            List<float> PositionsBarWidthPercent = new List<float>();
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);
            PositionsBarWidthPercent.Add(0.0f);

            ArrayList SetpointStrokeColor = new ArrayList();
            List<bool> SetpointIsArea = new List<bool>();
            ArrayList SetpointFillColor = new ArrayList();
            List<String> SetpointAxis = new List<String>();

            List<double> SetpointValue = new List<double>();
            List<double> SetpointValue2 = new List<double>();
            List<DateTime> SetpointBDateTime = new List<DateTime>();
            List<DateTime> SetpointEDateTime = new List<DateTime>();

            List<bool> SetPointIsActivated = new List<bool>();
            List<Notes> ListAnnotation = new List<Notes>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<Axe>>  AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");
            MaxCandleValue.Add("");
            MidCandleValue.Add("");
            MinCandleValue.Add("");

            int iIndexCumul = dtTable.Columns.IndexOf("Cumul");
            if (iIndexCumul != -1)
            {
                if (iIndexCumul + 1 != dtTable.Columns.Count)
                {
                    for (int i = iIndexCumul + 1; i < dtTable.Columns.Count; i++)
                    {
                        ColumnsNames.Add(dtTable.Columns[i].ColumnName);
                        ColumnsOrigin.Add("");
                        Visualisation.Add("Line");
                        AxeRepresentation.Add("Y2");
                        StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColorStroke(i + 1)));
                        FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(i + 1)));
                        Accumulation.Add(Color.Transparent);
                        Position.Add(0);
                        ShowValues.Add(false);
                        
                        MaxCandleValue.Add("");
                        MidCandleValue.Add("");
                        MinCandleValue.Add("");
                    }
                }
            }
            return new GraphicFilter(ColumnsNames,
                                    ColumnsOrigin,
                                    Visualisation,
                                    AxeRepresentation,
                                    StrokeCouleurs,
                                    FillCouleurs,
                                    Accumulation, 
                                    SetpointAxis,
                                    SetpointIsArea,
                                    SetpointStrokeColor,
                                    SetpointFillColor,
                                    SetpointValue,
                                    SetpointValue2,
                                    SetpointBDateTime,
                                    SetpointEDateTime,
                                    SetPointIsActivated,
                                    ListAnnotation,
                                    CandleColumnsList,
                                    MaxCandleValue,
                                    MidCandleValue,
                                    MinCandleValue,
                                    AxisList,
                                    Position,
                                    PositionsXScrollbar,
                                    ShowValues,
                                    dtTable.TableName, dtTable.TableName, false, 
                                    "Cycle Time (" + ColumnsNames[0].ToString() + ")",
                                    "% " + sColumnName.Substring(5),
                                    "",
                                    true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        }
        /********************************************/
        /********************************************/
        /********************************************/
        /********************************************/

        #endregion

        #region Les fonctions pour la gestion de la légende.
        public static void EraseLegend(NLegend m_Legend)
        {
            if(m_Legend != null)
                m_Legend.Data.Items.Clear();
        }
        public void CreateLegend(NLegend m_Legend)
        {
            //boolean that inform whether a column is a candle column, in which case we don't display in the legend
            Boolean isFirstCandleCulumn = false;
            Boolean isCandleCulumn = false; 
            m_Legend.Data.Items.Clear();
            for(int i=0;i<listColumnsNames.Count;i++)
            {
                ///On regarde s'il y a des candlesList existante c'est a dire Trois colonnes servant à faire un candle
                ///auquel cas on ne les affiches pas dans la légende
                if (CandleColumnsList.Count != 0)
                    for(int j =0; j < CandleColumnsList.Count; j++)
                        ///Si le nom contenu dans la liste listColumnsNames est le meme que celui récupéré de CandleColumnsList, on récupère l'emplacement dans la liste
                        if (listColumnsNames[i].ToString() == CandleColumnsList[j].ToString())
                            ///On cherche à récupérer le premier élement contenu dans CandleColumnsList
                            if (j == 0)
                            {
                                isFirstCandleCulumn = true;
                                if (listColumnsNames[i].ToString() == CandleColumnsList[1].ToString() ||
                                    listColumnsNames[i].ToString() == CandleColumnsList[2].ToString())
                                    break;
                            }
                            /// Sinon on n'affiche pas l'élément dans la légende
                            else
                                isCandleCulumn = true;
                ///Si l'élément est un simple candle, c'est à dire une colonne servant à faire un candle
                ///ou que c'est la deuxieme ou troisieme colonne d'une candleList alors on ne l'affiche pas dans la légende
                if (listVisualisation[i].ToString().CompareTo("Candle") != 0 && isCandleCulumn == false)
                {
                    ///On n'affiche dans la légende que les courbes qui sont affichées sur l'axe Y ou Y2
                    if (listAxeRepresentation[i].ToString() != "X")
                    {
                        NLegendItemCellData legendDataItem = new NLegendItemCellData();
                        ///Le nom affiché dans la légende sera composé du nom de la courbe et de l'axe sur lequel il est utilisé 
                        legendDataItem.Text = listColumnsNames[i].ToString() + " (" + listAxeRepresentation[i].ToString() + ")";
                        m_Legend.Data.Items.Add(legendDataItem);
                        ///Si c'est le premier élément d'une candleList alors on l'ajoute dans la légende avec comme image un carré en diagonale de couleur bleu
                        if (isFirstCandleCulumn)
                        {
                            legendDataItem.MarkShape = LegendMarkShape.Diamond;
                            legendDataItem.MarkBorderStyle = new NStrokeStyle(Color.Blue);
                            legendDataItem.MarkFillStyle = (NFillStyle)(new NColorFillStyle(Color.Blue));
                        }
                        ///sinon on l'affiche simplement avec la couleur correspondante utilisée
                        else
                        {
                            legendDataItem.MarkBorderStyle = (NStrokeStyle)listStrokeCouleurs[i];
                            if ((listVisualisation[i].ToString() != "Bar") &&
                                (listVisualisation[i].ToString() != "Bar stacked") &&
                                (listVisualisation[i].ToString() != "Area") &&
                                (listVisualisation[i].ToString() != "Area stacked"))
                                legendDataItem.MarkFillStyle = new NColorFillStyle(((NStrokeStyle)listStrokeCouleurs[i]).Color);
                            else
                                legendDataItem.MarkFillStyle = (NFillStyle)listFillCouleurs[i];
                        }
                    }
                }
                isFirstCandleCulumn = false;
                isCandleCulumn = false;
            }
            m_Legend.BringToFront();
        }
        #endregion

        #region Function that create a SetPoint Line
        public void CreateSetPoint(NChartControl GraphiqueZone, NChart chart)
        {

            NStandardScaleConfigurator standardScale;
            NScaleSectionStyle m_FirstHorizontalSection;
            NTextStyle labelStyle;
            DateTime dtime = new DateTime(1, 1, 1);

            for (int i = 0; i < listSetPointValue.Count; i++)
            {
                //If the setpoint is activated, we display it, otherwise not
                if (listSetPointIsActivated[i])
                {


                    m_FirstHorizontalSection = new NScaleSectionStyle();
                    m_FirstHorizontalSection.SetShowAtWall(ChartWallType.Back, true);

                    ///Si le Setpoint est a afficher en X
                    if (listSetPointAxis[i].ToString() == "X")
                    {
                        // If the setpoint is an area, draw the area between the two lines with the stroke color and fill color
                        if (listSetPointIsArea[i])
                        {
                            ///On récupère l'axe des X
                            standardScale = (NStandardScaleConfigurator)chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator;
                            m_FirstHorizontalSection.SetShowAtWall(ChartWallType.Floor, true);

                            ///Si la valeur contenue dans listSetPointBDateTime est différent de la valeur par défaut( DateTime(1, 1, 1) ) alors cela 
                            ///signifie que l'affichage en X se fait avec des dates et on utilise alors les valeurs de debut listSetPointBDateTime et fin listSetPointEDateTime
                            if (listSetPointBDateTime[i] != dtime)
                                m_FirstHorizontalSection.Range = new NRange1DD(listSetPointBDateTime[i].ToOADate(), listSetPointEDateTime[i].ToOADate());
                            else
                                m_FirstHorizontalSection.Range = new NRange1DD(listSetPointValue[i], listSetPointValue2[i]);
                            ///On definit le style du Setpoint à savoir sa couleur de remplissage et de contour le la couleur des ticks d'abscisse lorsqu'ils sont dans une zone de setpoint
                            m_FirstHorizontalSection.MajorTickStrokeStyle = (NStrokeStyle)listSetPointStrokeColor[i];
                            m_FirstHorizontalSection.MinorGridStrokeStyle = new NStrokeStyle(2, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);
                            m_FirstHorizontalSection.MajorGridStrokeStyle = (NStrokeStyle)listSetPointStrokeColor[i];
                            m_FirstHorizontalSection.RangeFillStyle = new NColorFillStyle(Color.FromArgb(125, ((NStrokeStyle)listSetPointFillColor[i]).Color));

                            ///Style des labels d'abscisse
                            labelStyle = new NTextStyle();
                            labelStyle.FillStyle = new NGradientFillStyle(((NStrokeStyle)listSetPointStrokeColor[i]).Color, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);
                            labelStyle.FontStyle.Style = FontStyle.Bold;
                            m_FirstHorizontalSection.LabelTextStyle = labelStyle;

                            standardScale.Sections.Add(m_FirstHorizontalSection);
                        }
                        ///Le setpoint est un setpoint en Ligne
                        else
                        {
                            ///Déclaration d'une ligne constante
                            NAxisConstLine cl = chart.Axis(StandardAxis.PrimaryX).ConstLines.Add();
                            ///Epaisseur de la ligne et couleur
                            cl.StrokeStyle = new NStrokeStyle(2, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);

                            ///Si le setpoint est a affiché sur un axe en DateTime, on récupère la valeur contenue dans la liste listSetPointBDateTime
                            if (listSetPointBDateTime[i] != dtime)
                                cl.Value = listSetPointBDateTime[i].ToOADate();
                            else
                                cl.Value = listSetPointValue[i];
                        }
                    }
                    ///Si le Setpoint est a afficher en Y
                    else if (listSetPointAxis[i].ToString() == "Y2")
                    {
                        if (listSetPointIsArea[i])
                        {
                            standardScale = (NStandardScaleConfigurator)chart.Axis(StandardAxis.SecondaryY).ScaleConfigurator;
                            m_FirstHorizontalSection.SetShowAtWall(ChartWallType.Right, true);

                            m_FirstHorizontalSection.Range = new NRange1DD(listSetPointValue[i], listSetPointValue2[i]);
                            m_FirstHorizontalSection.MajorTickStrokeStyle = (NStrokeStyle)listSetPointStrokeColor[i];
                            m_FirstHorizontalSection.MinorGridStrokeStyle = new NStrokeStyle(2, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);
                            m_FirstHorizontalSection.MajorGridStrokeStyle = (NStrokeStyle)listSetPointStrokeColor[i];
                            m_FirstHorizontalSection.RangeFillStyle = new NColorFillStyle(Color.FromArgb(125, ((NStrokeStyle)listSetPointFillColor[i]).Color));

                            labelStyle = new NTextStyle();
                            labelStyle.FillStyle = new NGradientFillStyle(((NStrokeStyle)listSetPointStrokeColor[i]).Color, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);
                            labelStyle.FontStyle.Style = FontStyle.Bold;
                            m_FirstHorizontalSection.LabelTextStyle = labelStyle;

                            standardScale.Sections.Add(m_FirstHorizontalSection);
                        }
                        else
                        {
                            NAxisConstLine cl = chart.Axis(StandardAxis.SecondaryY).ConstLines.Add();
                            cl.StrokeStyle = new NStrokeStyle(2, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);
                            cl.Value = listSetPointValue[i];
                        }
                    }
                    ///Si le Setpoint est a afficher en Y2
                    else
                    {
                        if (listSetPointIsArea[i])
                        {
                            standardScale = (NStandardScaleConfigurator)chart.Axis(StandardAxis.PrimaryY).ScaleConfigurator;
                            m_FirstHorizontalSection.SetShowAtWall(ChartWallType.Left, true);

                            m_FirstHorizontalSection.Range = new NRange1DD(listSetPointValue[i], listSetPointValue2[i]);
                            m_FirstHorizontalSection.MajorTickStrokeStyle = (NStrokeStyle)listSetPointStrokeColor[i];
                            m_FirstHorizontalSection.MinorGridStrokeStyle = new NStrokeStyle(2, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);
                            m_FirstHorizontalSection.MajorGridStrokeStyle = (NStrokeStyle)listSetPointStrokeColor[i];
                            m_FirstHorizontalSection.RangeFillStyle = new NColorFillStyle(Color.FromArgb(125, ((NStrokeStyle)listSetPointFillColor[i]).Color));

                            labelStyle = new NTextStyle();
                            labelStyle.FillStyle = new NGradientFillStyle(((NStrokeStyle)listSetPointStrokeColor[i]).Color, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);
                            labelStyle.FontStyle.Style = FontStyle.Bold;
                            m_FirstHorizontalSection.LabelTextStyle = labelStyle;

                            standardScale.Sections.Add(m_FirstHorizontalSection);
                        }
                        else
                        {
                            NAxisConstLine cl = chart.Axis(StandardAxis.PrimaryY).ConstLines.Add();
                            cl.StrokeStyle = new NStrokeStyle(2, ((NStrokeStyle)listSetPointStrokeColor[i]).Color);
                            cl.Value = listSetPointValue[i];
                        }
                    }
                }
            }
        }
                
        public void CreateSetPoint(NChartControl GraphiqueZone, NChart chart, int chartPositionIndex)   // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
        {
            NStandardScaleConfigurator standardScale;
            NScaleSectionStyle m_FirstHorizontalSection;
            NTextStyle labelStyle;
            DateTime dtime = new DateTime(1, 1, 1);

            foreach (SetPoint setPoint in setPoints)
            {
                if ((int)setPoint.chartPosition != chartPositionIndex)
                    continue;
                if (!setPoint.isActivated)
                    continue;

                m_FirstHorizontalSection = new NScaleSectionStyle();
                m_FirstHorizontalSection.SetShowAtWall(ChartWallType.Back, true);

                if (setPoint.axis == "X")
                {
                    if (setPoint.isArea)
                    {
                        // >> Bug #15147 Charts Setpoints only work for first series chart location (frame) C#12                        
                        NAxis axis = chart.Axis(StandardAxis.PrimaryX);
                        NAxisStripe axisStripe = new NAxisStripe();
                        axis.Stripes.Add(axisStripe);

                        axisStripe.StrokeStyle = new NStrokeStyle(2, (setPoint.strokeColor).Color);
                        axisStripe.FillStyle = new NColorFillStyle(setPoint.fillColor.Color);
                        axisStripe.SetShowAtWall(ChartWallType.Back, true);

                        if (setPoint.dateStartValue != dtime)
                        {                            
                            axisStripe.SetShowAtWall(ChartWallType.Right, true);
                            axisStripe.From = setPoint.dateStartValue.ToOADate();
                            axisStripe.To = setPoint.dateEndValue.ToOADate();
                        }
                        else
                        {
                            axisStripe.SetShowAtWall(ChartWallType.Floor, true);
                            axisStripe.From = setPoint.numericStartValue;
                            axisStripe.To = setPoint.numericEndValue;
                        }
                        // >> Bug #15147 Charts Setpoints only work for first series chart location (frame) C#12
                    }
                    ///Le setpoint est un setpoint en Ligne
                    else
                    {
                        ///Déclaration d'une ligne constante
                        NAxisConstLine cl = chart.Axis(StandardAxis.PrimaryX).ConstLines.Add();
                        ///Epaisseur de la ligne et couleur
                        cl.StrokeStyle = new NStrokeStyle(2, (setPoint.strokeColor).Color);

                        ///Si le setpoint est a affiché sur un axe en DateTime, on récupère la valeur contenue dans la liste listSetPointBDateTime
                        if (setPoint.dateStartValue != dtime)
                            cl.Value = setPoint.dateStartValue.ToOADate();
                        else
                            cl.Value = setPoint.numericStartValue;
                    }
                }
                else if (setPoint.axis == "Y2")
                {
                    if (setPoint.isArea)
                    {
                        standardScale = (NStandardScaleConfigurator)chart.Axis(StandardAxis.SecondaryY).ScaleConfigurator;
                        m_FirstHorizontalSection.SetShowAtWall(ChartWallType.Right, true);

                        m_FirstHorizontalSection.Range = new NRange1DD(setPoint.numericStartValue, setPoint.numericEndValue);
                        m_FirstHorizontalSection.MajorTickStrokeStyle = setPoint.strokeColor;
                        m_FirstHorizontalSection.MinorGridStrokeStyle = new NStrokeStyle(2, ((setPoint.strokeColor).Color));
                        m_FirstHorizontalSection.MajorGridStrokeStyle = setPoint.strokeColor;
                        m_FirstHorizontalSection.RangeFillStyle = new NColorFillStyle(Color.FromArgb(125, (setPoint.fillColor).Color));

                        labelStyle = new NTextStyle();
                        labelStyle.FillStyle = new NGradientFillStyle((setPoint.strokeColor).Color, (setPoint.strokeColor).Color);
                        labelStyle.FontStyle.Style = FontStyle.Bold;
                        m_FirstHorizontalSection.LabelTextStyle = labelStyle;

                        standardScale.Sections.Add(m_FirstHorizontalSection);
                    }
                    else
                    {
                        NAxisConstLine cl = chart.Axis(StandardAxis.SecondaryY).ConstLines.Add();
                        cl.StrokeStyle = new NStrokeStyle(2, (setPoint.strokeColor).Color);
                        cl.Value = setPoint.numericStartValue;
                    }
                }
                else
                {
                    if (setPoint.isArea)
                    {
                        standardScale = (NStandardScaleConfigurator)chart.Axis(StandardAxis.PrimaryY).ScaleConfigurator;
                        m_FirstHorizontalSection.SetShowAtWall(ChartWallType.Left, true);

                        m_FirstHorizontalSection.Range = new NRange1DD(setPoint.numericStartValue, setPoint.numericEndValue);
                        m_FirstHorizontalSection.MajorTickStrokeStyle = setPoint.strokeColor;
                        m_FirstHorizontalSection.MinorGridStrokeStyle = new NStrokeStyle(2, (setPoint.strokeColor).Color);
                        m_FirstHorizontalSection.MajorGridStrokeStyle = setPoint.strokeColor;
                        m_FirstHorizontalSection.RangeFillStyle = new NColorFillStyle(Color.FromArgb(125, (setPoint.fillColor).Color));

                        labelStyle = new NTextStyle();
                        labelStyle.FillStyle = new NGradientFillStyle((setPoint.strokeColor).Color, (setPoint.strokeColor).Color);
                        labelStyle.FontStyle.Style = FontStyle.Bold;
                        m_FirstHorizontalSection.LabelTextStyle = labelStyle;

                        standardScale.Sections.Add(m_FirstHorizontalSection);
                    }
                    else
                    {
                        NAxisConstLine cl = chart.Axis(StandardAxis.PrimaryY).ConstLines.Add();
                        cl.StrokeStyle = new NStrokeStyle(2, (setPoint.strokeColor).Color);
                        cl.Value = setPoint.numericStartValue;
                    }
                }
            }
        }
        #endregion


        #region Les constructeurs de la classe.
        public GraphicFilter()  // >> Task #17969 PAX2SIM - Reports improvements
        {
        }

        private void initilizeFilter()
        {
            listColumnsNames = new ArrayList();
            listColumnsOrigin = new ArrayList();
            listVisualisation = new ArrayList();
            listAxeRepresentation = new ArrayList();
            listStrokeCouleurs = new ArrayList();
            listFillCouleurs = new ArrayList();
            listAccumulation = new ArrayList();
            listPositions = new ArrayList();
            lesPositionsXScrollbar = new List<bool>();
            listShowValues = new ArrayList();

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

            listSetPointAxis = new List<string>();
            listSetPointStrokeColor = new ArrayList();
            listSetPointFillColor = new ArrayList();
            listSetPointIsArea = new List<Boolean>();
            listSetPointValue = new List<double>();
            listSetPointValue2 = new List<double>();
            listSetPointBDateTime = new List<DateTime>();
            listSetPointEDateTime = new List<DateTime>(); 
            listSetPointIsActivated = new List<Boolean>();
            ListAnnotation = new List<Notes>();
            AxisList = new List<List<Axe>>();

            CandleColumnsList = new ArrayList();
            MidCandleValue = new ArrayList();
            MinCandleValue = new ArrayList();
            MaxCandleValue = new ArrayList();
            AxisList = new List<List<Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<Axe> axis = new List<Axe>();
                for (int j = 0; j < 3; j++)
                {
                    Axe axe = new Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            Name = "";
            Title = "";
            bInvertAbscisse = false;

            sXTitle = "";
            sYTitle = "";
            sY2Title = "";
            useScenarioNameInTitle_ = false;    // << Task #9624 Pax2Sim - Charts - checkBox for scenario name            
            
        }
        public GraphicFilter(ArrayList listColumnsNames_, 
            ArrayList listColumnOrigin_, 
            ArrayList listVizualisation_, 
            ArrayList ListAxeRepresentation_, 
            ArrayList listStrokeCouleurs_, 
            ArrayList listFillCouleurs_,
            ArrayList listAccumulation_, 
            List<String> listSetPointAxis_,
            List<Boolean> listSetPointIsArea_,
            ArrayList listSetPointStrokeColor_,
            ArrayList listSetPointFillColor_,
            List<double> listSetPointValue_,
            List<double> listSetPointValue2_,
            List<DateTime> listSetPointBDateTime_,
            List<DateTime> listSetPointEDateTime_,
            List<Boolean> listSetPointIsActivated_,
            List<Notes> ListAnnotation_,
            ArrayList CandleColumnsList_,
            ArrayList MaxCandleValue_,
            ArrayList MidCandleValue_,
            ArrayList MinCandleValue_,
            List<List<Axe>> AxisList_,
            ArrayList listPositions_,
            List<Boolean> lesPositionsXScrollbar_,
            ArrayList listShowValues_,            
            String Name_, 
            String Title_, 
            Boolean InvertAbscisse_, 
            String XTitle_, 
            String YTitle_,
            String Y2Title_,
            bool pUseScenarioNameInTitle,   // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
            //float barWidth_, float gapPercent_, float barWidthPercent_)
            List<float> lesPositionBarWidth_,
            List<float> lesPositionGapPercent_,
            List<float> lesPositionBarWidthPercent_,
            List<SetPoint> setPoints_)
        {
            listColumnsNames = listColumnsNames_;
            listColumnsOrigin = listColumnOrigin_;
            listVisualisation = listVizualisation_;
            listAxeRepresentation = ListAxeRepresentation_;
            listStrokeCouleurs = listStrokeCouleurs_;
            listFillCouleurs = listFillCouleurs_;
            listAccumulation = listAccumulation_;

            listSetPointStrokeColor = listSetPointStrokeColor_;
            listSetPointFillColor = listSetPointFillColor_;
            listSetPointIsArea = listSetPointIsArea_;
            listSetPointValue = listSetPointValue_;
            listSetPointValue2 = listSetPointValue2_;
            listSetPointBDateTime = listSetPointBDateTime_;
            listSetPointEDateTime = listSetPointEDateTime_; 
            ListAnnotation = ListAnnotation_;
            listSetPointAxis = listSetPointAxis_;
            listSetPointIsActivated = listSetPointIsActivated_;
            CandleColumnsList = CandleColumnsList_;
            MaxCandleValue = MaxCandleValue_;
            MidCandleValue = MidCandleValue_;
            MinCandleValue = MinCandleValue_;
            AxisList = AxisList_;

            listPositions = listPositions_;
            lesPositionsXScrollbar = lesPositionsXScrollbar_;
            listShowValues = listShowValues_;            
            Name = Name_;
            Title = Title_;
            bInvertAbscisse = InvertAbscisse_;
            sXTitle = XTitle_;
            sYTitle = YTitle_;
            sY2Title = Y2Title_;
            useScenarioNameInTitle_ = pUseScenarioNameInTitle;  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name

            lesPositionsBarWidth = lesPositionBarWidth_;
            lesPositionsGapPercent = lesPositionGapPercent_;
            lesPositionsBarWidthPercent = lesPositionBarWidthPercent_;

            setPoints = setPoints_;
        }

        /// <summary>
        /// Récupère les informations sauvegardées
        /// </summary>
        /// <param name="Noeud"></param>
        public GraphicFilter(System.Xml.XmlNode Noeud)
        {

            initilizeFilter();
            ///Récupère le nom du filtre
            if (!OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "Name"))
                return;
            Name = Noeud.Attributes["Name"].Value;

            if (Name.Contains("Input") || Name.Contains("User Data"))
            {
                return;
            }

            ///Récupère le titre du filtre
            if (!OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "Title"))
            {
                Title = "";
            }
            else
            {
                Title = Noeud.Attributes["Title"].Value;
            }
            ///Si l'option d'inversion d'abscisse est activée alors un attribut InvertAbscisse doit etre présent
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "InvertAbscisse"))
            {
                if (!Boolean.TryParse(Noeud.Attributes["InvertAbscisse"].Value, out bInvertAbscisse))
                    InvertAbscisse = false;
            }            

            ///Récupère le nom de l'abscisse X
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "X"))
                XTitle = Noeud.Attributes["X"].Value;
            ///Récupère le nom de l'abscisse Y
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "Y"))
                YTitle = Noeud.Attributes["Y"].Value;
            ///Récupère le nom de l'abscisse Y2
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "Y2"))
                Y2Title = Noeud.Attributes["Y2"].Value;

            // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
            if (OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, GlobalNames.USE_SCENARIO_NAME_IN_TITLE_XML_NODE_NAME))
            {
                if (!Boolean.TryParse(Noeud.Attributes[GlobalNames.USE_SCENARIO_NAME_IN_TITLE_XML_NODE_NAME].Value, out useScenarioNameInTitle_))
                    useScenarioNameInTitle_ = false;
            }
            // >> Task #9624 Pax2Sim - Charts - checkBox for scenario name
                                    
            if (!OverallTools.FonctionUtiles.hasNamedChild(Noeud, "Columns"))
                return;

            #region For the list of the 3 Candles.
            System.Xml.XmlElement candleColumns = (System.Xml.XmlElement)Noeud["Candles"];

            ///Si une liste de candle est définie pour ce filtre alors candleColumns est différent de null
            if (candleColumns != null)
            {
                ///Récupère tous les éléments de la CandleColumnsList et les met dans candleColumn
                foreach (System.Xml.XmlElement candleColumn in candleColumns.ChildNodes)
                {                    
                    if (OverallTools.FonctionUtiles.hasNamedAttribute(candleColumn, "CandleColumnsList"))
                    {
                        ///Ajoute la valeur contenue dans candleColumn
                        CandleColumnsList.Add(candleColumn.Attributes["CandleColumnsList"].Value);
                    }
                    else
                        CandleColumnsList.Add("");
                }
            }
            #endregion

            #region For the different columns of the Graphic.
            System.Xml.XmlElement colonnes = (System.Xml.XmlElement)Noeud["Columns"];
            int couleur;

            Double dValue;
            ///Pour chaque colonne du graphique, on récurère le nom du scénario (DataSet),le nom de la table (Table), le nom de la colonne (Column),
            ///le nom de l'abscisse (Abscissa),  ainsi que toutes les colonnes et les informations concernant la table (ColumnOrigin)
            foreach (System.Xml.XmlElement colonne in colonnes.ChildNodes)
            {
                if (OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "DataSet"))
                {
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "Table"))
                        continue;
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "Column"))
                        return;

                    String DataSet = colonne.Attributes["DataSet"].Value;
                    String Table = colonne.Attributes["Table"].Value;
                    String Column = colonne.Attributes["Column"].Value;
                    String sExceptionName = null;
                    if(OverallTools.FonctionUtiles.hasNamedAttribute(colonne,"ExceptionName"))
                        sExceptionName = colonne.Attributes["ExceptionName"].Value;
                    String Abscissa = "";
                    if (OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "Abscissa"))
                        Abscissa = colonne.Attributes["Abscissa"].Value;
                    ColumnInformation ColumnOrigin = new ColumnInformation(DataSet, Table,sExceptionName, Column,Abscissa, colonne.Attributes["Name"].Value);
                    listColumnsOrigin.Add(ColumnOrigin);
                }
                else
                {
                    listColumnsOrigin.Add("");
                }
                ///Détermine si l'option d'accumulation est utilisée, si oui un attribut Accumulation est présent
                if (OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "Accumulation"))
                {
                    ///On récupère ainsi la couleur de l'accumulation
                    Int32.TryParse(colonne.Attributes["Accumulation"].Value, out couleur);
                    listAccumulation.Add(Color.FromArgb(couleur));
                }
                ///Sinon on définit la courbe d'accumulation comme transparente pour pouvoir savoir plus tard que cela signifie que cette option n'est pas utilisée
                else
                {
                    listAccumulation.Add(Color.Transparent);
                }
                ///Détermine si l'option d'affichage des valeurs est utilisée, si oui un attribut ShowValues est présent
                if (OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "ShowValues"))
                {
                    listShowValues.Add(true);
                }
                else
                {
                    listShowValues.Add(false);
                }               
                ///Détermine la position de la coube sur le Graphique : 0 = Top Left, 1 = Top Right, 2 = Bottom Left, 3 = Bottom Right
                if (OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "Position"))
                {
                    listPositions.Add(FonctionsType.getInt(colonne.Attributes["Position"].Value));
                }
                else
                {
                    listPositions.Add(1);
                }
                ///Nom de la courbe
                listColumnsNames.Add(colonne.Attributes["Name"].Value);
                ///Le type de courbe, Line, bar...
                listVisualisation.Add(colonne.Attributes["Visualisation"].Value);
                ///L'axe sur lequel il est affiché
                listAxeRepresentation.Add(colonne.Attributes["Axe"].Value);
                ///Si la courbe est un candle alors on récupère le type de valeur maximum voulue, cad 99% ou 95% ...
                if (OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "MaxCandleValue"))
                {
                    MaxCandleValue.Add(colonne.Attributes["MaxCandleValue"].Value);
                }
                else
                    MaxCandleValue.Add("");
                ///Si la courbe est un candle alors on récupère le type de valeur moyenne voulue, cad 95% ou 75% ou Mean...
                if (OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "MidCandleValue"))
                {
                    MidCandleValue.Add(colonne.Attributes["MidCandleValue"].Value);
                }
                else
                    MidCandleValue.Add("");
                ///Si la courbe est un candle alors on récupère le type de valeur minimum voulue, cad min ou Min - zero...
                if (OverallTools.FonctionUtiles.hasNamedAttribute(colonne, "MinCandleValue"))
                {
                    MinCandleValue.Add(colonne.Attributes["MinCandleValue"].Value);
                }
                else
                    MinCandleValue.Add("");

                ///Déterminer la couleur de contour de la courbe
                Int32.TryParse(colonne.Attributes["StrokeColor"].Value, out couleur);

                // >> Task #16728 PAX2SIM Improvements (Recurring) C#34 7.2
                //listStrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.FromArgb(couleur)));                                
                NStrokeStyle strokeStyle = new Nevron.GraphicsCore.NStrokeStyle(Color.FromArgb(couleur));
                
                float floatParseResult = 0;
                NMeasurementUnit measurementUnit = null;
                if (colonne.HasAttribute("StrokeWidthValue") && colonne.Attributes["StrokeWidthValue"].Value != null
                    && colonne.HasAttribute("StrokeWidthMeasurementUnit") && colonne.Attributes["StrokeWidthMeasurementUnit"].Value != null)
                {
                    string measurementUnitName = colonne.Attributes["StrokeWidthMeasurementUnit"].Value;
                    measurementUnit = NMeasurementSystemManager.DefaultMeasurementSystemManager.GetMeasurementUnitFromName(ref measurementUnitName);
                    if (measurementUnit != null && float.TryParse(colonne.Attributes["StrokeWidthValue"].Value, out floatParseResult))
                    {
                        strokeStyle.Width = new NLength(floatParseResult, measurementUnit);
                    }
                }
                if (colonne.HasAttribute("StrokeLinePattern") && colonne.Attributes["StrokeLinePattern"].Value != null)
                {
                    strokeStyle.Pattern = GraphicFilterTools
                        .getEnumerationValueByName<LinePattern>(Enum.GetValues(typeof(LinePattern)), LinePattern.Solid, colonne.Attributes["StrokeLinePattern"].Value);
                }
                int intParseResult = 0;
                if (colonne.HasAttribute("StrokeCustomLinePatternValue") && colonne.Attributes["StrokeCustomLinePatternValue"].Value != null
                    && Int32.TryParse(colonne.Attributes["StrokeCustomLinePatternValue"].Value, out intParseResult))
                {
                    strokeStyle.CustomPattern = intParseResult;
                }
                if (colonne.HasAttribute("StrokeFactor") && colonne.Attributes["StrokeFactor"].Value != null
                    && Int32.TryParse(colonne.Attributes["StrokeFactor"].Value, out intParseResult))
                {
                    strokeStyle.Factor = intParseResult;
                }
                if (colonne.HasAttribute("StrokeDashCap") && colonne.Attributes["StrokeDashCap"].Value != null)
                {
                    strokeStyle.DashCap = GraphicFilterTools
                        .getEnumerationValueByName<DashCap>(Enum.GetValues(typeof(DashCap)), DashCap.Flat, colonne.Attributes["StrokeDashCap"].Value);
                }
                if (colonne.HasAttribute("StrokeLineJoin") && colonne.Attributes["StrokeLineJoin"].Value != null)
                {
                    strokeStyle.LineJoin = GraphicFilterTools
                        .getEnumerationValueByName<LineJoin>(Enum.GetValues(typeof(LineJoin)), LineJoin.Miter, colonne.Attributes["StrokeLineJoin"].Value);
                }
                listStrokeCouleurs.Add(strokeStyle);                
                // << Task #16728 PAX2SIM Improvements (Recurring) C#34 7.2

                ///Déterminer la couleur de remplissage de la courbe
                Int32.TryParse(colonne.Attributes["FillColor"].Value, out couleur);
                listFillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(Color.FromArgb(couleur)));
            }
            #endregion

            #region For the charts Scrollbar
            System.Xml.XmlElement Scrollbars = (System.Xml.XmlElement)Noeud["Scrollbars"];
            ///Si l'axe des abscisses est affiché avec une scrollbar
            if (Scrollbars != null)
            {
                foreach (System.Xml.XmlElement Scrollbar in Scrollbars.ChildNodes)
                {
                    ///Si la scrollbar est activée on l'ajoute
                    bool bScrollBar = OverallTools.FonctionUtiles.hasNamedAttribute(Scrollbar, "Activated");
                    lesPositionsXScrollbar.Add(bScrollBar);
                }
            }
            #endregion

            #region For the charts BarWidth
            lesPositionsBarWidth.Clear();
            System.Xml.XmlElement BarWidths = (System.Xml.XmlElement)Noeud["BarWidths"];
            if (BarWidths != null)
            {
                foreach (System.Xml.XmlElement BarWidth in BarWidths.ChildNodes)
                {
                    float width = 0.0f;
                    if (OverallTools.FonctionUtiles.hasNamedAttribute(BarWidth, "BarWidthValue"))
                    {
                        if (!float.TryParse(BarWidth.Attributes["BarWidthValue"].Value, out width))
                            width = 0.0f;
                    }
                    lesPositionsBarWidth.Add(width);
                }
            }
            #endregion

            #region For the charts GapPercent
            lesPositionsGapPercent.Clear();
            System.Xml.XmlElement GapPercents = (System.Xml.XmlElement)Noeud["GapPercents"];
            if (GapPercents != null)
            {
                foreach (System.Xml.XmlElement GapPercent in GapPercents.ChildNodes)
                {
                    float perc = 0.0f;
                    if (OverallTools.FonctionUtiles.hasNamedAttribute(GapPercent, "GapPercentValue"))
                    {
                        if (!float.TryParse(GapPercent.Attributes["GapPercentValue"].Value, out perc))
                            perc = 0.0f;
                    }
                    lesPositionsGapPercent.Add(perc);
                }
            }
            #endregion

            #region For the charts BarWidthPercent
            lesPositionsBarWidthPercent.Clear();
            System.Xml.XmlElement BarWidthPercents = (System.Xml.XmlElement)Noeud["BarWidthPercents"];
            if (BarWidthPercents != null)
            {
                foreach (System.Xml.XmlElement BarWidthPercent in BarWidthPercents.ChildNodes)
                {
                    float perc = 0.0f;
                    if (OverallTools.FonctionUtiles.hasNamedAttribute(BarWidthPercent, "BarWidthPercentValue"))
                    {
                        if (!float.TryParse(BarWidthPercent.Attributes["BarWidthPercentValue"].Value, out perc))
                            perc = 0.0f;
                    }
                    lesPositionsBarWidthPercent.Add(perc);
                }
            }
            #endregion

            #region For the Set Point of the graphic
            System.Xml.XmlElement SetPoints = (System.Xml.XmlElement)Noeud["SetPoints"];
            DateTime dtTmp ;
            ///S'il existe un ou plusieurs setpoints de définit
            if (SetPoints != null)
            {
                ///On récupère chaque setpoint ainsi que ses caractéristiques
                foreach (System.Xml.XmlElement SetPoint in SetPoints.ChildNodes)
                {
                    ///L'axe sur lequel il est affiché
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "Axis"))
                        continue;
                    ///Sa couleur de contour
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "Stroke"))
                        continue;
                    ///Sa valeur de début S'il utilise un axe de type date
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "BeginDateTime"))
                        continue;
                    ///Sa valeur de fin S'il utilise un axe de type date
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "EndDateTime"))
                        continue;
                    ///Sa valeur s'il utilise un axe de type numérique
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "Value"))
                        continue;
                    ///Couleur de contour
                    if (!Int32.TryParse(SetPoint.Attributes["Stroke"].Value.ToString(), out couleur))
                        continue;
                    ///Booleen indiquant si c'est un setpoint de type zone
                    bool bArea = OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "Area");
                    ///Booleen indiquant si le setpoint est activé ou non
                    bool bActivated = OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "Activated");
                    listSetPointIsActivated.Add(bActivated);

                    listSetPointAxis.Add(SetPoint.Attributes["Axis"].Value.ToString());
                    listSetPointIsArea.Add(bArea);
                    listSetPointStrokeColor.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.FromArgb(couleur)));
                    if (!Double.TryParse(SetPoint.Attributes["Value"].Value.ToString(), out dValue))
                        listSetPointValue.Add(0);
                    else
                        listSetPointValue.Add(dValue);

                    if (!DateTime.TryParse(SetPoint.Attributes["BeginDateTime"].Value.ToString(), out dtTmp))
                        listSetPointBDateTime.Add(new DateTime());
                    else
                        listSetPointBDateTime.Add(dtTmp);

                    if (!DateTime.TryParse(SetPoint.Attributes["EndDateTime"].Value.ToString(), out dtTmp))
                        listSetPointEDateTime.Add(new DateTime());
                    else
                        listSetPointEDateTime.Add(dtTmp);
                    ///Si le setpoint est de type zone
                    if (bArea)
                    {
                        ///On récupère la couleur de remplissage
                        if ((!OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "Fill")) ||
                            (!Int32.TryParse(SetPoint.Attributes["Fill"].Value.ToString(), out couleur)))
                        {
                            listSetPointFillColor.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.Transparent));
                            listSetPointValue2.Add(0);
                            continue;
                        }
                        ///On récupère la valeur de fin de zone
                        listSetPointFillColor.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.FromArgb(couleur)));

                        if ((!OverallTools.FonctionUtiles.hasNamedAttribute(SetPoint, "Value2")) ||
                            (!Double.TryParse(SetPoint.Attributes["Value2"].Value.ToString(), out dValue)))
                        {
                            listSetPointValue2.Add(0);
                            continue;
                        }
                        listSetPointValue2.Add(dValue);
                    }
                    ///Si c'est un setpoint de type ligne on met la couleur transparente en couleur de remplissage
                    else
                    {
                        listSetPointFillColor.Add(new Nevron.GraphicsCore.NStrokeStyle(Color.Transparent));

                        listSetPointValue2.Add(0);
                    }
                }
            }
            #endregion

            // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
            #region Set point New
            XmlElement setPointsElement = (XmlElement)Noeud["SetPointsNew"];
            if (setPointsElement != null)
            {
                setPoints = new List<SetPoint>();
                int strokeColor = -1;
                int fillColor = -1;
                double numericValue = -1;
                DateTime dateValue;

                foreach (XmlElement setPointElement in setPointsElement.ChildNodes)
                {
                    SetPoint setPoint = new SetPoint();
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "Axis"))
                        continue;
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "ChartPosition"))
                        continue;
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "Stroke"))
                        continue;
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "BeginDateTime"))
                        continue;
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "EndDateTime"))
                        continue;
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "Value"))
                        continue;
                    if (!Int32.TryParse(setPointElement.Attributes["Stroke"].Value.ToString(), out strokeColor))
                        continue;

                    setPoint.isArea = OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "Area");
                    setPoint.isActivated = OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "Activated");


                    setPoint.chartPosition = (ChartUtils.CHART_POSITIONS)Enum
                        .Parse(typeof(ChartUtils.CHART_POSITIONS), setPointElement.Attributes["ChartPosition"].Value.ToString());

                    setPoint.axis = setPointElement.Attributes["Axis"].Value.ToString();
                    setPoint.strokeColor = new NStrokeStyle(Color.FromArgb(strokeColor));

                    if (!Double.TryParse(setPointElement.Attributes["Value"].Value.ToString(), out numericValue))
                        setPoint.numericStartValue = 0;
                    else
                        setPoint.numericStartValue = numericValue;

                    if (!DateTime.TryParse(setPointElement.Attributes["BeginDateTime"].Value.ToString(), out dateValue))
                        setPoint.dateStartValue = new DateTime();
                    else
                        setPoint.dateStartValue = dateValue;

                    if (!DateTime.TryParse(setPointElement.Attributes["EndDateTime"].Value.ToString(), out dateValue))
                        setPoint.dateEndValue = new DateTime();
                    else
                        setPoint.dateEndValue = dateValue;

                    if (setPoint.isArea)
                    {
                        if ((!OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "Fill")) ||
                            (!Int32.TryParse(setPointElement.Attributes["Fill"].Value.ToString(), out fillColor)))
                        {
                            setPoint.fillColor = new NStrokeStyle(Color.Transparent);
                            setPoint.numericEndValue = 0;
                            continue;
                        }
                        setPoint.fillColor = new NStrokeStyle(Color.FromArgb(fillColor));

                        if ((!OverallTools.FonctionUtiles.hasNamedAttribute(setPointElement, "Value2")) ||
                            (!Double.TryParse(setPointElement.Attributes["Value2"].Value.ToString(), out numericValue)))
                        {
                            setPoint.numericEndValue = 0;
                            continue;
                        }
                        setPoint.numericEndValue = numericValue;
                    }
                    else
                    {
                        setPoint.fillColor = new NStrokeStyle(Color.Transparent);
                        setPoint.numericEndValue = 0;
                    }
                    setPoints.Add(setPoint);
                }
            }
            else
            {
                List<ChartUtils.CHART_POSITIONS> chartPositions = new List<ChartUtils.CHART_POSITIONS>();
                if (listSetPointValue != null && listSetPointValue.Count > 0)
                {
                    for (int i = 0; i < listSetPointValue.Count; i++)
                        chartPositions.Add(ChartUtils.CHART_POSITIONS.TopLeft);
                    setPoints = ChartUtils.retrieveSetPoints(listSetPointValue, listSetPointValue2, listSetPointBDateTime, listSetPointEDateTime,
                        listSetPointIsArea, listSetPointIsActivated, listSetPointAxis, chartPositions, listSetPointStrokeColor, listSetPointFillColor);
                }
            }
            #endregion
            // << Bug #15147 Charts Setpoints only work for first series chart location (frame)

            #region For the Annotations
            System.Xml.XmlElement Annotations = (System.Xml.XmlElement)Noeud["Annotations"];
            if (Annotations != null)
            {
                int dataPointIndex;
                foreach (System.Xml.XmlElement Annotation in Annotations.ChildNodes)
                {
                    ///On créé une note
                    Notes note = new Notes();
                    ///Texte qui apparaitra dans la bulle d'annotation
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(Annotation, "Text"))
                        continue;
                    ///Le numéro d'index du point de courbe
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(Annotation, "DataPointIndex"))
                        continue;
                    ///L'id de l'annotation
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(Annotation, "AnnotationId"))
                        continue;
                    ///l'id de l'objet auquel l'annotation sera attachée
                    if (!OverallTools.FonctionUtiles.hasNamedAttribute(Annotation, "ObjectId"))
                        continue;

                    if (OverallTools.FonctionUtiles.hasNamedAttribute(Annotation, "Text"))
                    {
                        note.text = Annotation.Attributes["Text"].Value;
                    }
                    if (!int.TryParse(Annotation.Attributes["DataPointIndex"].Value.ToString(), out dataPointIndex))
                        note.dataPointIndex = 0;
                    else
                        note.dataPointIndex = dataPointIndex;
                    if (OverallTools.FonctionUtiles.hasNamedAttribute(Annotation, "AnnotationId"))
                    {
                        note.annotationId = Annotation.Attributes["AnnotationId"].Value;
                    }
                    note.objectId = Annotation.Attributes["ObjectId"].Value;
                    ListAnnotation.Add(note);
                }
            }
            #endregion


            #region For the Axes
            System.Xml.XmlElement Axes = (System.Xml.XmlElement)Noeud["Axes"];
            if (Axes != null)
            {
                ///Chaque graphique contient quatres listes de trois axes 
                AxisList = new List<List<Axe>>();

                foreach (System.Xml.XmlElement AxisPosition in Axes.ChildNodes)
                {
                    List<Axe> AxisPositionList = new List<Axe>();

                    DateTime dateTime;
                    int tmpIVal;
                    double tmpDVal;
                    foreach (System.Xml.XmlElement Axis in AxisPosition.ChildNodes)
                    {
                        Axe axis = new Axe();

                        ///Si l'attribut IsDateTime est présent cela signifie que l'abscisse est de type date
                        if (OverallTools.FonctionUtiles.hasNamedAttribute(Axis, "IsDateTime"))
                        {
                            axis.IsDateTime = true;
                            ///On récupère ainsi la valeur de début
                            if (!DateTime.TryParse(Axis.Attributes["beginDTValue"].Value.ToString(), out dateTime))
                                axis.BeginDTValue = new DateTime();
                            else
                                axis.BeginDTValue = dateTime;
                            ///On récupère la taille en heure de l'axe                            
                            if (!int.TryParse(Axis.Attributes["lengthDTValue"].Value.ToString(), out tmpIVal))
                                axis.LengthDTValue = -1;
                            else
                                axis.LengthDTValue = tmpIVal;
                            axis.BeginNValue = -1;
                            axis.LengthNValue = -1;
                        }

                         ///Si l'attribut IsDateTime n'est pas présent , on récupère la valeur numérique du début d'axe
                        else if (OverallTools.FonctionUtiles.hasNamedAttribute(Axis, "beginNValue"))
                        {
                            ///On met des valeurs par défaut pour IsDateTime, BeginDTValue et LengthDTValue pour indiquer que l'axe n'est pas de type DateTime
                            axis.IsDateTime = false;
                            axis.BeginDTValue = new DateTime(1, 1, 1);
                            axis.LengthDTValue = -1;
                            if (!double.TryParse(Axis.Attributes["beginNValue"].Value.ToString(), out tmpDVal))
                                axis.BeginNValue = -1;
                            else
                                axis.BeginNValue = tmpDVal;
                            if (!double.TryParse(Axis.Attributes["lengthNValue"].Value.ToString(), out tmpDVal))
                                axis.LengthNValue = -1;
                            else
                                axis.LengthNValue = tmpDVal;
                        }
                        else
                        {
                            axis.IsDateTime = false;
                            axis.BeginDTValue = new DateTime(1, 1, 1);
                            axis.LengthDTValue = -1;
                            axis.BeginNValue = -1;
                            axis.LengthNValue = -1;

                        }
                        AxisPositionList.Add(axis);
                    }
                    AxisList.Add(AxisPositionList);
                }
            }
            #endregion

        }
        #endregion

        // >> Task #13384 Report Tree-view
        public GraphicFilter clone()
        {
            ArrayList listColumnsNamesClone = new ArrayList(listColumnsNames);

            ArrayList listColumnOriginClone = new ArrayList();
            foreach (object o in listColumnsOrigin)
            {
                if (o != null && o is ColumnInformation)
                {
                    ColumnInformation ci = (ColumnInformation)o;
                    listColumnOriginClone.Add(ci.clone());
                }
            }
                                    
            ArrayList listVizualisationClone = new ArrayList(listVisualisation);
            ArrayList ListAxeRepresentationClone = new ArrayList(listAxeRepresentation);
            ArrayList listStrokeCouleursClone = new ArrayList(listStrokeCouleurs);
            ArrayList listFillCouleursClone = new ArrayList(listFillCouleurs);
            ArrayList listAccumulationClone = new ArrayList(listAccumulation);
            ArrayList listSetPointStrokeColorClone = new ArrayList(listSetPointStrokeColor);
            ArrayList listSetPointFillColorClone = new ArrayList(listSetPointFillColor);
            ArrayList CandleColumnsListClone = new ArrayList(CandleColumnsList);
            ArrayList MaxCandleValueClone = new ArrayList(MaxCandleValue);
            ArrayList MidCandleValueClone = new ArrayList(MidCandleValue);
            ArrayList MinCandleValueClone = new ArrayList(MinCandleValue);
            ArrayList listShowValuesClone = new ArrayList(listShowValues);
            ArrayList listPositionsClone = new ArrayList(listPositions);

            List<SetPoint> setPointClones = new List<SetPoint>();
            foreach (SetPoint setPoint in setPoints)            
                setPointClones.Add(setPoint.clone());            

            List<double> listSetPointValueClone = new List<double>(listSetPointValue);
            List<double> listSetPointValue2Clone = new List<double>(listSetPointValue2);            
            List<Boolean> listSetPointIsAreaClone = new List<bool>(listSetPointIsArea);
            List<Boolean> listSetPointIsActivatedClone = new List<bool>(listSetPointIsActivated);
            List<Boolean> lesPositionsXScrollbarClone = new List<bool>(lesPositionsXScrollbar);

            List<float> lesPositionsBarWidthClone = new List<float>(lesPositionsBarWidth);
            List<float> lesPositionsGapPercentClone = new List<float>(lesPositionsGapPercent);
            List<float> lesPositionsBarWidthPercentClone = new List<float>(lesPositionsBarWidthPercent);

            List<String> listSetPointAxisClone = new List<string>(listSetPointAxis);
            foreach (string setPointAxis in listSetPointAxis)
            {
                if (setPointAxis != null && setPointAxis.Clone() != null)
                    listSetPointAxisClone.Add(setPointAxis.Clone().ToString());
            }
            
            List<DateTime> listSetPointBDateTimeClone = new List<DateTime>();
            foreach (DateTime date in listSetPointBDateTime)
            {
                DateTime clonedDate = date;
                listSetPointBDateTimeClone.Add(clonedDate);
            }
            List<DateTime> listSetPointEDateTimeClone = new List<DateTime>();
            foreach (DateTime date in listSetPointEDateTime)
            {
                DateTime clonedDate = date;
                listSetPointEDateTimeClone.Add(clonedDate);
            }

            List<List<Axe>> AxisListClone = new List<List<Axe>>();
            foreach (List<Axe> innerAxeList in AxisList)
            {
                List<Axe> innerAxeListClone = new List<Axe>();
                foreach (Axe axe in innerAxeList)
                {
                    innerAxeListClone.Add(axe.clone());
                }
                AxisListClone.Add(innerAxeListClone);
            }

            List<Notes> ListAnnotationClone = new List<Notes>();
            foreach (Notes notes in ListAnnotation)
            {
                ListAnnotationClone.Add(notes.clone());
            }            
           
            return new GraphicFilter(listColumnsNamesClone, listColumnOriginClone, listVizualisationClone, ListAxeRepresentationClone, listStrokeCouleursClone, listFillCouleursClone, listAccumulationClone,
                listSetPointAxisClone, listSetPointIsAreaClone, listSetPointStrokeColorClone, listSetPointFillColorClone, listSetPointValueClone, listSetPointValue2Clone,
                listSetPointBDateTimeClone, listSetPointEDateTimeClone, listSetPointIsActivatedClone, ListAnnotationClone, CandleColumnsListClone,
                MaxCandleValueClone, MidCandleValueClone, MinCandleValueClone, AxisListClone, listPositionsClone, lesPositionsXScrollbarClone, listShowValuesClone,
                Name, Title, InvertAbscisse, XTitle, YTitle, Y2Title, useScenarioNameInTitle, lesPositionsBarWidthClone, lesPositionsGapPercentClone,
                lesPositionsBarWidthPercentClone, setPointClones);
            
        }
        // << Task #13384 Report Tree-view

        #region La liste des fonctions accesseurs pour la classe.
        public ArrayList getlistColumnsOrigin()
        {
            return listColumnsOrigin;
        }
        public ArrayList getListColumnsNames()
        {
            return listColumnsNames;
        }
        public ArrayList getListVizualisation()
        {
            return listVisualisation;
        }
        public ArrayList getListAccumulation()
        {
            return listAccumulation;
        }
        public ArrayList getListPositions()
        {
            return listPositions;
        }

        public List<Boolean> getListXScrollbar()
        {
            return lesPositionsXScrollbar;
        }

        public List<float> getListBarWidth()
        {
            return lesPositionsBarWidth;
        }

        public List<float> getListGapPercent()
        {
            return lesPositionsGapPercent;
        }

        public List<float> getListBarWidthPercent()
        {
            return lesPositionsBarWidthPercent;
        }

        public ArrayList getListAxeRepresentation()
        {
            return listAxeRepresentation;
        }
        public String getName()
        {
            return Name;
        }
        public ArrayList getListFillCouleurs()
        {
            return listFillCouleurs;
        }
        public ArrayList getListStrokeCouleurs()
        {
            return listStrokeCouleurs;
        }

        public Boolean InvertAbscisse
        {
            get
            {
                return bInvertAbscisse;
            }
            set
            {
                bInvertAbscisse = value;
            }
        }

        public String XTitle
        {
            get
            {
                return sXTitle;
            }
            set
            {
                sXTitle = value;
            }
        }
        public String YTitle
        {
            get
            {
                return sYTitle;
            }
            set
            {
                sYTitle = value;
            }
        }
        public String Y2Title
        {
            get
            {
                return sY2Title;
            }
            set
            {
                sY2Title = value;
            }
        }
        // << Task #9624 Pax2Sim - Charts - checkBox for scenario name
        public Boolean useScenarioNameInTitle
        {
            get { return useScenarioNameInTitle_; }
            set { useScenarioNameInTitle_ = value; }
        }
        // >> Task #9624 Pax2Sim - Charts - checkBox for scenario name
                
        internal ArrayList getListShowValues()
        {
            return listShowValues;
        }

        /// <summary>
        /// On récupère La couleur de remplissage d'une courbe à l'index iIndex
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public Nevron.GraphicsCore.NFillStyle getListFillCouleurs(int iIndex)
        {
            if ((listFillCouleurs == null) || (iIndex <0)) return new Nevron.GraphicsCore.NColorFillStyle(Color.Blue);
            if (iIndex >= listFillCouleurs.Count) return new Nevron.GraphicsCore.NColorFillStyle(Color.Blue);
            return (Nevron.GraphicsCore.NFillStyle)listFillCouleurs[iIndex];
        }
        /// <summary>
        /// On récupère La couleur de contour d'une courbe à l'index iIndex
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public Nevron.GraphicsCore.NStrokeStyle getListStrokeCouleurs(int iIndex)
        {
            if ((listStrokeCouleurs == null) || (iIndex < 0)) return new Nevron.GraphicsCore.NStrokeStyle(Color.Black);
            if (iIndex >= listStrokeCouleurs.Count) return new Nevron.GraphicsCore.NStrokeStyle(Color.Black);
            return (Nevron.GraphicsCore.NStrokeStyle)listStrokeCouleurs[iIndex];
        }

        public ArrayList getCandleColumnsList()
        {
            return CandleColumnsList;
        }

        public ArrayList getMaxCandleValue()
        {
            return MaxCandleValue;
        }
        public ArrayList getMidCandleValue()
        {
            return MidCandleValue;
        }
        public ArrayList getMinCandleValue()
        {
            return MinCandleValue;
        }

        public void setlistColumnsOrigin(ArrayList listColumnsOrigin_)
        {
            listColumnsOrigin = listColumnsOrigin_;
        }

        public void setlistAxeRepresentation(ArrayList listAxeRepresentation_)
        {
            listAxeRepresentation = listAxeRepresentation_;
        }
        public void setListColumnsNames(ArrayList listColumnsNames_)
        {
            listColumnsNames = listColumnsNames_;
        }
        public void setListVizualisation(ArrayList listVizualisation_)
        {
            listVisualisation = listVizualisation_;
        }
        public void setName(String Name_)
        {
            Name = Name_;
        }
        public void setListStrokeCouleurs(ArrayList listStrokeCouleurs_)
        {
            listStrokeCouleurs = listStrokeCouleurs_;
        }

        public void setListXScrollbar(List<Boolean> lesPositionsXScrollbar_)
        {
            lesPositionsXScrollbar = lesPositionsXScrollbar_;
        }

        public void setListBarWidth(List<float> lesPositionsBarWidth_)
        {
            lesPositionsBarWidth = lesPositionsBarWidth_;
        }

        public void setListGapPercent(List<float> lesPositionsGapPercent_)
        {
            lesPositionsGapPercent = lesPositionsGapPercent_;
        }

        public void setListBarWidthPercent(List<float> lesPositionsBarWidthPercent_)
        {
            lesPositionsBarWidthPercent = lesPositionsBarWidthPercent_;
        }

        public void setListFillCouleurs(ArrayList listFillCouleurs_)
        {
            listFillCouleurs = listFillCouleurs_;
        }

        public void setListSetPointAxis(List<String> lList)
        {
            listSetPointAxis = lList;
        }

        public void setListSetPointStrokeCouleurs(ArrayList listStrokeCouleurs_)
        {
            listSetPointStrokeColor = listStrokeCouleurs_;
        }

        public void setListSetPointFillColor(ArrayList lList)
        {
            listSetPointFillColor = lList;
        }

        public void setListIsArea(List<Boolean> lList)
        {
            listSetPointIsArea = lList;
        }

        public void setListSetPointValue(List<double> lList)
        {
            listSetPointValue = lList;
        }

        public void setListSetPointValue2(List<double> lList)
        {
            listSetPointValue2 = lList;
        }

        public void setListIsActivated(List<Boolean> lList)
        {
            listSetPointIsActivated = lList;
        }

        public void setListSetPointBDateTime(List<DateTime> lList)
        {
            listSetPointBDateTime = lList;
        }

        public void setListSetPointEDateTime(List<DateTime> lList)
        {
            listSetPointEDateTime = lList;
        }


        public void setListAnnotation(List<Notes> lAnnotation)
        {
            ListAnnotation = lAnnotation;
        }


        public void setCandleColumnsList(ArrayList aList)
        {
            CandleColumnsList = aList;
        }

        public void setMaxCandleValue(ArrayList aList)
        {
            MaxCandleValue = aList;
        }

        public void setMidCandleValue(ArrayList aList)
        {
            MidCandleValue = aList;
        }
        public void setMinCandleValue(ArrayList aList)
        {
            MinCandleValue = aList;
        }

        public void setAxisList(List<List<Axe>> axisList)
        {
            AxisList = axisList;
        }


        public ArrayList getListSetPointStrokeColor()
        {
            return listSetPointStrokeColor;
        }

        public ArrayList getListSetPointFillColor()
        {
            return listSetPointFillColor;
        }

        public List<double> getListSetPointValue()

        {
            return listSetPointValue;
        }

        public List<double> getListSetPointValue2()
        {
            return listSetPointValue2;
        }

        public List<DateTime> getListSetPointBDateTime()
        {
            return listSetPointBDateTime;
        }

        public List<DateTime> getListSetPointEDateTime()
        {
            return listSetPointEDateTime;
        }
                
        public List<Boolean> getListIsArea()
        {
            return listSetPointIsArea;
        }


        public List<String> getListSetPointAxis()
        {
            return listSetPointAxis;
        }

        public List<Boolean> getListIsActivated()
        {
            return listSetPointIsActivated;
        }

        public List<Notes> getListAnnotation()
        {
            return ListAnnotation;
        }

        public List<List<Axe>> getAxisList()
        {
            return AxisList;
        }

        public String getModeVisualisation(String nomColonne)
        {
            int iIndex = listColumnsNames.IndexOf(nomColonne);
            if ( iIndex == -1)
            {
                return null;
            }
            return listVisualisation[iIndex].ToString();
        }
        #endregion

        #region Les différentes fonctions utiles pour un filtre.
        /// <summary>
        /// On créé un arbre Xml dans le fichier xml pour sauvegarder les données
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public System.Xml.XmlNode creerArbreXml(System.Xml.XmlDocument document)
        {
            ///Créé un élément GraphicFilter
            System.Xml.XmlElement filtre = document.CreateElement("GraphicFilter");
            ///Passe le nom du Graphique
            filtre.SetAttribute("Name", Name);
            ///Passe le titre du Graphique
            filtre.SetAttribute("Title", Title);
            ///passe le titre de l'axe X
            if(XTitle!="")
                filtre.SetAttribute("X", XTitle);
            ///passe le titre de l'axe Y
            if (YTitle != "")
                filtre.SetAttribute("Y", YTitle);
            ///passe le titre de l'axe Y2
            if (Y2Title != "")
                filtre.SetAttribute("Y2", Y2Title);
            ///Passe l'option d'inversement d'abscisse
            if(InvertAbscisse)
                filtre.SetAttribute("InvertAbscisse", true.ToString());
            
            filtre.SetAttribute(GlobalNames.USE_SCENARIO_NAME_IN_TITLE_XML_NODE_NAME, useScenarioNameInTitle_.ToString());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name

            #region Saving the candles if exist
            System.Xml.XmlElement Candles = document.CreateElement("Candles");
            System.Xml.XmlElement Candle;
            ///Si une candleList existe on créé trois éléments Candle
            for (int i = 0; i < CandleColumnsList.Count; i++)
            {
                Candle = document.CreateElement("Candle");
                ///pour chaque élément constituant la candlelist on passe sa valeur
                Candle.SetAttribute("CandleColumnsList", CandleColumnsList[i].ToString());

                Candles.AppendChild(Candle);
            }
            filtre.AppendChild(Candles);
            #endregion


            System.Xml.XmlElement Colonnes = document.CreateElement("Columns");
            System.Xml.XmlElement Colonne;
            ///On créé des éléments colonnes avec toutes ses informations correspondantes
            for (int i = 0; i < listColumnsNames.Count; i++)
            {
                Colonne = document.CreateElement("Column");
                if (((Color)listAccumulation[i])!=Color.Transparent)
                {
                    Colonne.SetAttribute("Accumulation", ((Color)listAccumulation[i]).ToArgb().ToString());
                }
                if (listColumnsOrigin[i].ToString() != "")
                {
                    ColumnInformation Information = (ColumnInformation )listColumnsOrigin[i];
                    Colonne.SetAttribute("DataSet", Information.DataSet);
                    Colonne.SetAttribute("Table", Information.TableName);
                    Colonne.SetAttribute("Column", Information.ColumnName);
                    Colonne.SetAttribute("Abscissa", Information.AbscissaColumnName);
                    if ((Information.ExceptionName != null) && (Information.ExceptionName != ""))
                        Colonne.SetAttribute("ExceptionName", Information.ExceptionName);
                }
                if((Boolean)listShowValues[i])
                    Colonne.SetAttribute("ShowValues","true");
               
                Colonne.SetAttribute("Position", listPositions[i].ToString());
                Colonne.SetAttribute("Name",listColumnsNames[i].ToString());
                Colonne.SetAttribute("Visualisation",listVisualisation[i].ToString());
                Colonne.SetAttribute("Axe",listAxeRepresentation[i].ToString());

                if (MaxCandleValue[i].ToString() != "")
                {
                    Colonne.SetAttribute("MaxCandleValue", MaxCandleValue[i].ToString());
                    Colonne.SetAttribute("MidCandleValue", MidCandleValue[i].ToString());
                    Colonne.SetAttribute("MinCandleValue", MinCandleValue[i].ToString());
                }
                Colonne.SetAttribute("StrokeColor", ((Nevron.GraphicsCore.NStrokeStyle)listStrokeCouleurs[i]).Color.ToArgb().ToString());
                NStrokeStyle strokeStyle = (NStrokeStyle)listStrokeCouleurs[i];
                // >> Task #16728 PAX2SIM Improvements (Recurring) C#34 7.2
                Colonne.SetAttribute("StrokeWidthValue", strokeStyle.Width.Value.ToString());
                Colonne.SetAttribute("StrokeWidthMeasurementUnit", strokeStyle.Width.MeasurementUnit.Name);

                Colonne.SetAttribute("StrokeLinePattern", strokeStyle.Pattern.ToString());
                Colonne.SetAttribute("StrokeCustomLinePatternValue", strokeStyle.CustomPattern.ToString());

                Colonne.SetAttribute("StrokeFactor", strokeStyle.Factor.ToString());

                Colonne.SetAttribute("StrokeDashCap", strokeStyle.DashCap.ToString());
                Colonne.SetAttribute("StrokeLineJoin", strokeStyle.LineJoin.ToString());
                // << Task #16728 PAX2SIM Improvements (Recurring) C#34 7.2
                Colonne.SetAttribute("FillColor", ((Nevron.GraphicsCore.NFillStyle)listFillCouleurs[i]).GetPrimaryColor().ToColor().ToArgb().ToString());
                
                Colonnes.AppendChild(Colonne);
            }
            filtre.AppendChild(Colonnes);

            System.Xml.XmlElement Scrollbars = document.CreateElement("Scrollbars");
            System.Xml.XmlElement Scrollbar;
            ///On sauvegarde les scrollbars s'il en existe
            for (int i = 0; i < lesPositionsXScrollbar.Count; i++)
            {
                Scrollbar = document.CreateElement("Scrollbar");
                if (lesPositionsXScrollbar[i])
                    Scrollbar.SetAttribute("Activated", lesPositionsXScrollbar[i].ToString());

                Scrollbars.AppendChild(Scrollbar);
            }
            filtre.AppendChild(Scrollbars);
            
            //bar widths for each chart position
            System.Xml.XmlElement BarWidths = document.CreateElement("BarWidths");                        
            for (int i = 0; i < lesPositionsBarWidth.Count; i++)
            {
                System.Xml.XmlElement BarWidth = document.CreateElement("BarWidth");
                BarWidth.SetAttribute("BarWidthValue", lesPositionsBarWidth[i].ToString());
                BarWidths.AppendChild(BarWidth);
            }
            filtre.AppendChild(BarWidths);

            System.Xml.XmlElement GapPercents = document.CreateElement("GapPercents");
            for (int i = 0; i < lesPositionsGapPercent.Count; i++)
            {
                System.Xml.XmlElement GapPercent = document.CreateElement("GapPercent");
                GapPercent.SetAttribute("GapPercentValue", lesPositionsGapPercent[i].ToString());
                GapPercents.AppendChild(GapPercent);
            }
            filtre.AppendChild(GapPercents);

            System.Xml.XmlElement BarWidthPercents = document.CreateElement("BarWidthPercents");
            for (int i = 0; i < lesPositionsBarWidthPercent.Count; i++)
            {
                System.Xml.XmlElement BarWidthPercent = document.CreateElement("BarWidthPercent");
                BarWidthPercent.SetAttribute("BarWidthPercentValue", lesPositionsBarWidthPercent[i].ToString());
                BarWidthPercents.AppendChild(BarWidthPercent);
            }
            filtre.AppendChild(BarWidthPercents);

            ///On sauvegarde les setpoints
            if (listSetPointValue.Count > 0)
            {
                System.Xml.XmlElement SetPoints = document.CreateElement("SetPoints");
                System.Xml.XmlElement SetPoint;
                for (int i = 0; i < listSetPointValue.Count; i++)
                {
                    SetPoint = document.CreateElement("SetPoint");
                    SetPoint.SetAttribute("Axis", listSetPointAxis[i].ToString());
                    SetPoint.SetAttribute("Stroke", ((NStrokeStyle)listSetPointStrokeColor[i]).Color.ToArgb().ToString());
                    SetPoint.SetAttribute("Value", listSetPointValue[i].ToString());
                    SetPoint.SetAttribute("BeginDateTime", listSetPointBDateTime[i].ToString());
                    SetPoint.SetAttribute("EndDateTime", listSetPointEDateTime[i].ToString());
                    if (listSetPointIsArea[i])
                    {
                        SetPoint.SetAttribute("Area", listSetPointIsArea[i].ToString());
                        SetPoint.SetAttribute("Fill", ((NStrokeStyle)listSetPointFillColor[i]).Color.ToArgb().ToString());
                        SetPoint.SetAttribute("Value2", listSetPointValue2[i].ToString());
                    }
                    ///Si le setpoint est activé, on ecrit l'attribut Activated sinon rien
                    if (listSetPointIsActivated[i])
                        SetPoint.SetAttribute("Activated", listSetPointIsActivated[i].ToString());

                    SetPoints.AppendChild(SetPoint);
                }
                filtre.AppendChild(SetPoints);
            }
            // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
            if (setPoints.Count > 0)
            {
                XmlElement setPointsElement = document.CreateElement("SetPointsNew");
                filtre.AppendChild(setPointsElement);
                foreach (SetPoint setPoint in setPoints)                
                {
                    XmlElement setPointElement = document.CreateElement("SetPointNew");
                    setPointsElement.AppendChild(setPointElement);

                    setPointElement.SetAttribute("ChartPosition", setPoint.chartPosition.ToString());
                    setPointElement.SetAttribute("Axis", setPoint.axis);
                    setPointElement.SetAttribute("Stroke", ((NStrokeStyle)setPoint.strokeColor).Color.ToArgb().ToString());
                    setPointElement.SetAttribute("Value", setPoint.numericStartValue.ToString());
                    setPointElement.SetAttribute("BeginDateTime", setPoint.dateStartValue.ToString());
                    setPointElement.SetAttribute("EndDateTime", setPoint.dateEndValue.ToString());
                    if (setPoint.isArea)
                    {
                        setPointElement.SetAttribute("Area", setPoint.isArea.ToString());
                        setPointElement.SetAttribute("Fill", ((NStrokeStyle)setPoint.fillColor).Color.ToArgb().ToString());
                        setPointElement.SetAttribute("Value2", setPoint.numericEndValue.ToString());
                    }
                    ///Si le setpoint est activé, on ecrit l'attribut Activated sinon rien
                    if (setPoint.isActivated)
                        setPointElement.SetAttribute("Activated", setPoint.isActivated.ToString());
                }
            }
            // << Bug #15147 Charts Setpoints only work for first series chart location (frame)

            ///On sauvegarde les annotations 
            if (ListAnnotation.Count > 0)
            {
                System.Xml.XmlElement Annotations = document.CreateElement("Annotations");
                System.Xml.XmlElement Annotation;
                for (int i = 0; i < ListAnnotation.Count; i++)
                {
                    Annotation = document.CreateElement("Annotation");
                    Annotation.SetAttribute("Text", ListAnnotation[i].text);
                    Annotation.SetAttribute("ObjectId", ListAnnotation[i].objectId);
                    Annotation.SetAttribute("AnnotationId", ListAnnotation[i].annotationId.ToString());
                    Annotation.SetAttribute("DataPointIndex", ListAnnotation[i].dataPointIndex.ToString());
                    Annotations.AppendChild(Annotation);
                }
                filtre.AppendChild(Annotations);
            }

            ///On sauvegarde la définition des axes
            System.Xml.XmlElement Axes = document.CreateElement("Axes");
            for (int i = 0; i < 4; i++)
            {
                System.Xml.XmlElement AxisPosition = document.CreateElement("AxisPosition");
                System.Xml.XmlElement Axis;
                for (int j = 0; j < 3; j++)
                {
                    Axis = document.CreateElement("Axis");
                    if (AxisList.Count != 0 )
                    {
                        if (AxisList[i][j].IsDateTime)
                        {
                            Axis.SetAttribute("IsDateTime", "true");
                            Axis.SetAttribute("beginDTValue", AxisList[i][j].BeginDTValue.ToString());
                            Axis.SetAttribute("lengthDTValue", AxisList[i][j].LengthDTValue.ToString());
                        }
                        else if (AxisList[i][j].LengthNValue != -1 && AxisList[i][j].LengthNValue != 0)
                        {
                            Axis.SetAttribute("beginNValue", AxisList[i][j].BeginNValue.ToString());
                            Axis.SetAttribute("lengthNValue", AxisList[i][j].LengthNValue.ToString());
                        }
                    }
                    AxisPosition.AppendChild(Axis);
                }
                Axes.AppendChild(AxisPosition);
            }
            filtre.AppendChild(Axes);

            return filtre;
        }
        public override string ToString()
        {
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            return creerArbreXml(document).OuterXml;
        }
        public int indexColumn(String name)
        {
            int i = 0;
            foreach (String Noms in listColumnsNames)
            {
                if (Noms == name)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        #endregion

        /// <summary>
        /// Fonction qui retourne un ScaleConfigurator à partir d'un temps entre la fin et le début de l'axe
        /// </summary>
        /// <param name="Fenetre"></param>
        /// <returns></returns>
        private NDateTimeScaleConfigurator setXScale(TimeSpan Fenetre)
        {
            //Dans le cas où les abscisses sont représentées par une date.
            NDateTimeScaleConfigurator DateTimeScale = new NDateTimeScaleConfigurator();

            DateTimeScale.LabelStyle.Angle = new NScaleLabelAngle(ScaleLabelAngleMode.View, 90);
            DateTimeScale.LabelStyle.ContentAlignment = ContentAlignment.MiddleLeft;

            // >> Bug #13387 Default Analysis Charts X axis time range
            DateTimeScale.AutoDateTimeUnits = new Nevron.NDateTimeUnit[] { NDateTimeUnit.Hour };
            /// Si le nombre d'heures entre la début et la fin de l'axe est inferieur à 2 alors l'échelle de l'axe sera affiché en minutes
            //if (Fenetre.TotalHours < 2)
            //{
            //    DateTimeScale.AutoDateTimeUnits = new Nevron.NDateTimeUnit[] { NDateTimeUnit.Minute };
            //}
            ///// Sinon si le nombre d'heures entre la début et la fin de l'axe est inferieur à 48 alors l'échelle de l'axe sera affiché en heures
            //else if (Fenetre.TotalHours < 48)
            //{
            //    DateTimeScale.AutoDateTimeUnits = new Nevron.NDateTimeUnit[] { NDateTimeUnit.Hour };
            //}
            ///// Sinon on l'échelle de l'axe sera affiché en heures
            //else
            //{
            //    DateTimeScale.AutoDateTimeUnits = new Nevron.NDateTimeUnit[] { NDateTimeUnit.Day };
            //}
            // << Bug #13387 Default Analysis Charts X axis time range
            DateTimeScale.EnableUnitSensitiveFormatting = true;
            DateTimeScale.AutoMinorTicks = true;
            DateTimeScale.MinorTickCount = 1;
            
            return DateTimeScale;
        }

        /// <summary>
        /// Utilisé pour renvoyer un DateTimeSpan à partir d'un TimeSpan
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private NDateTimeSpan setDateTimeSpan(TimeSpan timeSpan)
        {
            //Dans le cas où les abscisses sont représentées par une date.
            NDateTimeSpan DateTimeSpan = new NDateTimeSpan();

            ///Arrondis du nombre d'heure à la valeur superieure ex 23:55h => 24h
            double totalHours = Math.Ceiling((double)timeSpan.TotalHours);
            
            // >> Bug #13387 Default Analysis Charts X axis time range
            DateTimeSpan = new NDateTimeSpan((long)totalHours, NDateTimeUnit.Hour);
            //if (totalHours < 2)
            //    DateTimeSpan = new NDateTimeSpan((long)totalHours, NDateTimeUnit.Minute);
            //else if (totalHours < 48)
            //    DateTimeSpan = new NDateTimeSpan((long)totalHours, NDateTimeUnit.Hour);
            //else
            //    DateTimeSpan = new NDateTimeSpan((long)totalHours, NDateTimeUnit.Day);
            // << Bug #13387 Default Analysis Charts X axis time range

            return DateTimeSpan;
        }

        #region Fonctions pour la vérification des graphiques (généraux et autres)
        /// <summary>
        /// Fonction qui permet de vérifier que le graphique général ne contient pas de définition pour des colonnes qui 
        /// n'existent plus dans les tables. Cette fonction accepte un \ref GestionDonneesHUB2SIM car elle s'applique sur
        /// les graphiques de type général et doit donc vérifier que les tables auxquelles les colonnes se refèrent 
        /// existent toujours.
        /// </summary>
        /// <param name="gdhData">Le projet dans lequel recherché les tables pour les colonnes du graphique.</param>
        /// <returns>Booléen indiquant si le graphique est valid ou non.</returns>
        private bool CommitGraphic(GestionDonneesHUB2SIM gdhData)
        {
            if (gdhData == null)
                return false;
            String sColumnName;
            DataTable dtTable = gdhData.getTable(this.Name);
            if(dtTable!=null)
                return CommitGraphic(dtTable);
            for (int i = 0; i < listPositions.Count; i++)
            {
                dtTable = getGraphicTable(gdhData, i, out sColumnName);
                if (dtTable != null)
                {

                    if (CommitColumn(dtTable, sColumnName))
                        continue;
                }
                listColumnsNames.RemoveAt(i);
                listColumnsOrigin.RemoveAt(i);
                listVisualisation.RemoveAt(i);
                listAxeRepresentation.RemoveAt(i);
                listStrokeCouleurs.RemoveAt(i);
                listFillCouleurs.RemoveAt(i);
                listAccumulation.RemoveAt(i);
                listPositions.RemoveAt(i);
                listShowValues.RemoveAt(i);                
                i--;
            }
            return true;
        }

        /// <summary>
        /// Fonction qui permet de vérifier que le graphique général ne contient pas de définition pour des colonnes qui 
        /// n'existent plus dans la table
        /// </summary>
        /// <param name="dtTable">La table à laquelle le graphique est associée.</param>
        /// <returns>Booléen indiquant si le graphique est valid ou non.</returns>
        private bool CommitGraphic(DataTable dtTable)
        {
            if ((dtTable == null) || (dtTable.Columns.Count == 0))
                return false;
            for (int i = 0; i < listPositions.Count; i++)
            {
                if (CommitColumn(dtTable, listColumnsNames[i].ToString()))
                    continue;
                listColumnsNames.RemoveAt(i);
                listColumnsOrigin.RemoveAt(i);
                listVisualisation.RemoveAt(i);
                listAxeRepresentation.RemoveAt(i);
                listStrokeCouleurs.RemoveAt(i);
                listFillCouleurs.RemoveAt(i);
                listAccumulation.RemoveAt(i);
                listPositions.RemoveAt(i);

                listShowValues.RemoveAt(i);
                
                i--;
            }
            return true;
        }
        /// <summary>
        /// Permet de valider une colonne par rapport à la table d'où elle provient.
        /// </summary>
        /// <param name="dtTable"></param>
        /// <param name="sColumnName"></param>
        /// <returns></returns>
        private static bool CommitColumn(DataTable dtTable, String sColumnName)
        {
            if ((dtTable == null) || (dtTable.Columns.Count == 0))
                return false;
            return dtTable.Columns.Contains(sColumnName);
        }
        #endregion

        #region La fonction qui se charge de créer un sous graphique.

        #region La fonction qui se charge d'afficher le graphique pour l'index sélectionné.
        /// <summary>
        /// Fonction qui dessine les graphiques pour les types de graphiques qui ont leur définition avec des données
        /// provenant de plusieurs tables.
        /// </summary>
        /// <param name="ZoneGraphique"></param>
        /// <param name="donneeEnCours"></param>
        /// <param name="m_legend"></param>
        /// <returns></returns>
        public NChart[] CreateGraphicZone(NChartControl ZoneGraphique, GestionDonneesHUB2SIM donneeEnCours, NLegend m_legend, out DataTable dtGraphic)
        {
            dtGraphic = null;
            if (listColumnsOrigin.Count <= 0)
                return null;

            // << Task #8862 Pax2sim - Chart - confirm loading Scenario for Chart
            if (donneeEnCours == null)
                return null;
            
            List<string> allScenarios = donneeEnCours.getScenarioNames();
            List<string> neededScenarios = new List<string>();
            
            for (int i = 0; i < listAxeRepresentation.Count; i++)
            {
                if (listColumnsOrigin[i] == null || listColumnsOrigin[i].ToString() == ""
                    || listColumnsOrigin[i].GetType() != typeof(ColumnInformation))
                    continue;

                ColumnInformation informations = (ColumnInformation)listColumnsOrigin[i];
                if (allScenarios.Contains(informations.DataSet))
                {
                    bool isFilterTable = donneeEnCours.isFilterTable(informations.DataSet, informations.TableName);
                    bool isLoaded = false;
                    bool isCalculatedFromTrace = false;

                    if (isFilterTable)
                    {
                        Filter filterTable = donneeEnCours.getFilter(informations.DataSet, informations.TableName);
                        String parentTableName = filterTable.MotherTableName;

                        while (donneeEnCours.isFilterTable(informations.DataSet, parentTableName))
                        {
                            filterTable = donneeEnCours.getFilter(informations.DataSet, parentTableName);
                            if (filterTable == null)
                                break;
                            parentTableName = filterTable.MotherTableName;
                        }
                        if (parentTableName != null)
                        {
                            isLoaded = donneeEnCours.tabelIsLoaded(informations.DataSet, parentTableName);
                            if (!isLoaded)
                            {
                                isCalculatedFromTrace = donneeEnCours
                                         .tabelIsCalculatedFromTrace(informations.DataSet, parentTableName);
                                if (isCalculatedFromTrace && !neededScenarios.Contains(informations.DataSet))
                                    neededScenarios.Add(informations.DataSet);
                            }
                        }                        
                    }
                    else
                    {
                        isLoaded = donneeEnCours.tabelIsLoaded(informations.DataSet, informations.TableName);
                        if (!isLoaded)
                        {
                            isCalculatedFromTrace = donneeEnCours
                                     .tabelIsCalculatedFromTrace(informations.DataSet, informations.TableName);
                            if (isCalculatedFromTrace && !neededScenarios.Contains(informations.DataSet))
                                    neededScenarios.Add(informations.DataSet);
                        }
                    }
                }
            }
                        
            for (int iIndexGraphique = 0; iIndexGraphique < listPositions.Count; iIndexGraphique++)
            {
                if (listColumnsOrigin[iIndexGraphique] == null)
                    continue;
                if (listColumnsOrigin[iIndexGraphique].ToString() == "")
                    continue;
                if (listColumnsOrigin[iIndexGraphique].GetType() != typeof(ColumnInformation))
                    continue;
                ColumnInformation informations = (ColumnInformation)listColumnsOrigin[iIndexGraphique];
                if (allScenarios.Contains(informations.DataSet))
                {
                    bool isFilterTable = donneeEnCours.isFilterTable(informations.DataSet, informations.TableName);
                    bool isLoaded = false;
                    bool isCalculatedFromTrace = false;

                    if (isFilterTable)
                    {
                        Filter filterTable = donneeEnCours.getFilter(informations.DataSet, informations.TableName);
                        if (filterTable != null)
                        {
                            String parentTableName = filterTable.MotherTableName;
                            while (donneeEnCours.isFilterTable(informations.DataSet, parentTableName))
                            {
                                filterTable = donneeEnCours.getFilter(informations.DataSet, parentTableName);
                                if (filterTable == null)
                                    break;
                                parentTableName = filterTable.MotherTableName;
                            }

                            if (parentTableName != null)
                            {
                                isLoaded = donneeEnCours.tabelIsLoaded(informations.DataSet, parentTableName);
                                if (!isLoaded)
                                {
                                    isCalculatedFromTrace = donneeEnCours
                                             .tabelIsCalculatedFromTrace(informations.DataSet, parentTableName);
                                    if (isCalculatedFromTrace && !neededScenarios.Contains(informations.DataSet))
                                        neededScenarios.Add(informations.DataSet);
                                }
                            }
                        }
                    }
                    else
                    {
                        isLoaded = donneeEnCours.tabelIsLoaded(informations.DataSet, informations.TableName);
                        if (!isLoaded)
                        {
                            isCalculatedFromTrace = donneeEnCours
                                     .tabelIsCalculatedFromTrace(informations.DataSet, informations.TableName);
                            if (isCalculatedFromTrace && !neededScenarios.Contains(informations.DataSet))
                                    neededScenarios.Add(informations.DataSet);
                        }
                    }
                }                
            }
            

            int nbOfScenariosToBeLoaded = neededScenarios.Count;

            if (nbOfScenariosToBeLoaded > 0)
            {
                String warningMessage = "";
                if (nbOfScenariosToBeLoaded == 1)
                {
                    string scenarioName = Environment.NewLine + "  * " + neededScenarios[0];
                    warningMessage = "This is a graphic based on one scenario:"
                        + scenarioName
                        + Environment.NewLine
                        + "The loading procedure may take a few minutes. Do you want to continue?";
                }
                else if (nbOfScenariosToBeLoaded > 1)
                {
                    string scenarioNames = "";
                    foreach (string name in neededScenarios)
                    {
                        scenarioNames += Environment.NewLine + "  * " + name;
                    }
                    warningMessage = "This is a graphic based on " + nbOfScenariosToBeLoaded + " scenarios that are not loaded: "
                        + scenarioNames
                        + Environment.NewLine
                        + "The loading procedure may take a few minutes. Do you want to continue?";
                }

                ParamScenario scenarioParams = null;
                List<AnalysisResultFilter> resultFilters = null;
                List<string> flowTypes = null;
                bool isBHSSimulation = false;
                if (neededScenarios.Count == 1)
                {                    
                    scenarioParams = donneeEnCours.GetScenario(neededScenarios[0]);
                    if (scenarioParams != null)
                    {
                        resultFilters = scenarioParams.analysisResultsFilters;
                        flowTypes = scenarioParams.flowTypes;
                        isBHSSimulation = scenarioParams.BHSSimulation;

                        if (scenarioParams.analysisResultsFilters != null && scenarioParams.analysisResultsFilters.Count > 0)
                        {
                            if (scenarioParams.flowTypes == null || scenarioParams.flowTypes.Count == 0)
                            {
                                scenarioParams.flowTypes.Clear();
                                scenarioParams.flowTypes.Add(AnalysisResultFilter.DEPARTING_FLOW_TYPE_VISUAL_NAME);
                                //scenarioParams.flowTypes.Add(AnalysisResultFilter.ARRIVING_FLOW_TYPE_VISUAL_NAME);
                                scenarioParams.flowTypes.Add(AnalysisResultFilter.ORIGINATING_FLOW_TYPE_VISUAL_NAME);
                                //scenarioParams.flowTypes.Add(AnalysisResultFilter.TERMINATING_FLOW_TYPE_VISUAL_NAME);
                                scenarioParams.flowTypes.Add(AnalysisResultFilter.TRANSFERRING_FLOW_TYPE_VISUAL_NAME);
                            }
                        }
                    }
                }

                BHSCustomMessageBox customMessageBox = new BHSCustomMessageBox(warningMessage, isBHSSimulation, resultFilters, flowTypes, donneeEnCours,
                    BHSCustomMessageBox.generateParametersForPopup(scenarioParams));
                DialogResult dr = customMessageBox.ShowDialog();
                if (dr == DialogResult.No || dr == DialogResult.Cancel)
                {
                    PAX2SIM.loadingScenarioToShowObject = false;
                    return null;
                }
                PAX2SIM.loadingScenarioToShowObject = true;
                
                PAX2SIM.generateLocalISTForBHS = customMessageBox.generateLocalIST;
                PAX2SIM.generateGroupISTForBHS = customMessageBox.generateGroupIST; // >> Task #14280 Bag Trace Loading time too long
                PAX2SIM.generateMUPSegregationForBHS = customMessageBox.generateMUPSegregation; // >> Task #14280 Bag Trace Loading time too long
                PAX2SIM.copyOutputTables = customMessageBox.copyOutputTables;
                
                if (scenarioParams != null && customMessageBox.resultFilters != null)
                {
                    scenarioParams.analysisResultsFilters.Clear();
                    scenarioParams.analysisResultsFilters.AddRange(customMessageBox.resultFilters);
                }
                if (scenarioParams != null && customMessageBox.flowTypes != null)
                {
                    scenarioParams.flowTypes.Clear();
                    scenarioParams.flowTypes.AddRange(customMessageBox.flowTypes);
                }
                // << Task #13955 Pax2Sim -BHS trace loading issue
            }
            // >> Task #8862 Pax2sim - Chart - confirm loading Scenario for Chart

            this.CommitGraphic(donneeEnCours);
            PAX2SIM.loadingScenarioToShowObject = false;    // >> Task #13955 Pax2Sim -BHS trace loading issue
            int nbGraphique = getNumberOfChart(listPositions);
            int nbGraphiqueOnTop = getNumberOfChartOnTop(listPositions);
            NChart[] Result = new NChart[4] {null,null,null,null};
            dtGraphic = GenerateTable(donneeEnCours);
            for (int i = 0; i < listAxeRepresentation.Count; i++)
            {
                if (listAxeRepresentation[i].ToString() != "X")
                {
                    int iNbBar = getNumberGraphicBar((int)listPositions[i]);

                    if (Result[(int)listPositions[i]] == null)
                    {
                        ///A chaque fois que l'on ajoute une nouvelle position sur le graphique on met la variable globale m_cCandle
                        ///à null pour ne plus que les candles soient ajoutés à la suite
                        m_cCandle = null;
                        Result[(int)listPositions[i]] = CreateGraphicZone(ZoneGraphique, donneeEnCours, i, iNbBar);
                        
                        if (Result[(int)listPositions[i]] == null)
                            continue;
                        if (listVisualisation[i].ToString() == "Pie")
                        {
                            ZoneGraphique.Charts.Add(Result[(int)listPositions[i]]);
                        }
                        Point pPosition;
                        Point pSize;
                        getPosition((int)listPositions[i], nbGraphique, nbGraphiqueOnTop,Title!="", out pPosition, out pSize);
                        Result[(int)listPositions[i]].Location = new NPointL(new NLength(pPosition.X, NRelativeUnit.ParentPercentage),
                                                           new NLength(pPosition.Y, NRelativeUnit.ParentPercentage));
                        Result[(int)listPositions[i]].Size = new NSizeL(new NLength(pSize.X, NRelativeUnit.ParentPercentage),
                                                                   new NLength(pSize.Y, NRelativeUnit.ParentPercentage));
                        Result[(int)listPositions[i]].BoundsMode = BoundsMode.Stretch;
                        Result[(int)listPositions[i]].Tag = (int)listPositions[i]; 
                    }
                    else
                    {
                        if (listColumnsOrigin[i].ToString() == "")
                            return null;
                        ColumnInformation informations = (ColumnInformation) listColumnsOrigin[i];
                        DataTable laTable = null;
                        if ((informations.ExceptionName != null) && (informations.ExceptionName != ""))
                            laTable = donneeEnCours.getExceptionTable
                                (informations.DataSet, informations.TableName, informations.ExceptionName);
                        else
                            laTable = donneeEnCours.getTable(informations.DataSet, informations.TableName);


                        // donneeEnCours.getTable(informations.DataSet, informations.TableName);
                        if (laTable == null)
                            return null;
                        CreateGraphicZone(ZoneGraphique, Result[(int) listPositions[i]], laTable, i, iNbBar);
                    }
                }
            }
            return Result;
        }

        /// <summary>
        /// Fonction qui s'occupe de dessiner le graphique associé à une seule table.
        /// </summary>
        /// <param name="ZoneGraphique"></param>
        /// <param name="laTable"></param>
        /// <param name="m_legend"></param>
        /// <returns></returns>
        public NChart[] CreateGraphicZone(NChartControl ZoneGraphique,DataTable laTable, NLegend m_legend)
        {
            if (laTable == null)
                return null;
            this.CommitGraphic(laTable);

            int nbGraphique = getNumberOfChart(listPositions);
            
            int nbGraphiqueOnTop = getNumberOfChartOnTop(listPositions);
            NChart[] Result = new NChart[4] {null,null,null,null};

            DataTable sortedTable = laTable;            
             
            int iIndexX = listAxeRepresentation.IndexOf("X");
            if (iIndexX != -1)
            {
                if (InvertAbscisse)
                {
                    int iIndexXTable = sortedTable.Columns.IndexOf(listColumnsNames[iIndexX].ToString());
                    if (iIndexXTable != -1)
                    {
                        sortedTable = laTable.Copy();
                        foreach (DataRow row in sortedTable.Rows)
                        {
                            if ((sortedTable.Columns[iIndexXTable].DataType == typeof(Double)) ||
                                (sortedTable.Columns[iIndexXTable].DataType == typeof(double)))
                                row[iIndexXTable] = -(Double)row[iIndexXTable];
                            else
                                row[iIndexXTable] = -(int)row[iIndexXTable];
                        }
                    }
                }
                if((sortedTable.Columns.Count!=0)&&((sortedTable.Columns[listColumnsNames[iIndexX].ToString()].DataType != typeof(String))&&
                    (sortedTable.Columns[listColumnsNames[iIndexX].ToString()].DataType != typeof(string))))
                    sortedTable = OverallTools.DataFunctions.sortTable(sortedTable, listColumnsNames[iIndexX].ToString());
            }
            ///Dans un premier temps il faut dessiner les graphiques qui sont de la forme Area.
            for (int i = 0; i < listAxeRepresentation.Count; i++)
            {
                if (listAxeRepresentation[i].ToString() == "X")
                    continue;
                int iNbCourbeBar = getNumberGraphicBar((int)listPositions[i]);
                if (((String)listVisualisation[i]) != "Area")
                    continue;
                if (Result[(int)listPositions[i]] != null)
                {
                    CreateGraphicZone(ZoneGraphique, Result[(int)listPositions[i]], sortedTable, i, iNbCourbeBar);
                    continue;
                }
                Result[(int)listPositions[i]] = CreateGraphicZone(ZoneGraphique, sortedTable, i, iNbCourbeBar);
                if (Result[(int)listPositions[i]] == null)
                    continue;
                Result[(int)listPositions[i]].DisplayOnLegend = m_legend;
                Point pPosition;
                Point pSize;
                getPosition((int)listPositions[i], nbGraphique, nbGraphiqueOnTop, Title != "", out pPosition, out pSize);
                Result[(int)listPositions[i]].Location = new NPointL(new NLength(pPosition.X, NRelativeUnit.ParentPercentage),
                                                   new NLength(pPosition.Y, NRelativeUnit.ParentPercentage));
                Result[(int)listPositions[i]].Size = new NSizeL(new NLength(pSize.X, NRelativeUnit.ParentPercentage),
                                                           new NLength(pSize.Y, NRelativeUnit.ParentPercentage));
                Result[(int)listPositions[i]].BoundsMode = BoundsMode.Stretch;
            }

            ///Tous les graphiques qui ne sont pas du type Area.
            for (int i = 0; i < listAxeRepresentation.Count; i++)
            {
                if (listAxeRepresentation[i].ToString() == "X")
                    continue;
                if (((String)listVisualisation[i]) == "Area")
                    continue;
                int iNbCourbeBar = getNumberGraphicBar((int)listPositions[i]);
                if (Result[(int)listPositions[i]] == null)
                {
                    m_cCandle = null;
                    Result[(int)listPositions[i]] = CreateGraphicZone(ZoneGraphique, sortedTable, i, iNbCourbeBar);
                    
                    if (Result[(int)listPositions[i]] == null)
                        continue;

                    if (listVisualisation[i].ToString() != "Pie")
                    {
                        Result[(int)listPositions[i]].DisplayOnLegend = m_legend;
                        if(m_legend!=null)
                        m_legend.Refresh();
                    }
                    else
                    {
                        ZoneGraphique.Charts.Add(Result[(int)listPositions[i]]);
                    }
                    Point pPosition;
                    Point pSize;
                    getPosition((int)listPositions[i], nbGraphique, nbGraphiqueOnTop, Title != "", out pPosition, out pSize);
                    Result[(int)listPositions[i]].Location = new NPointL(new NLength(pPosition.X, NRelativeUnit.ParentPercentage),
                                                       new NLength(pPosition.Y, NRelativeUnit.ParentPercentage));
                    Result[(int)listPositions[i]].Size = new NSizeL(new NLength(pSize.X, NRelativeUnit.ParentPercentage),
                                                               new NLength(pSize.Y, NRelativeUnit.ParentPercentage));
                    Result[(int)listPositions[i]].BoundsMode = BoundsMode.Stretch;
                    //Ajout pour pouvoir savoir quel Nchart est positionné a quel endroit dans l'utilisation de la classe Visu par exemple.
                    Result[(int)listPositions[i]].Tag = (int)listPositions[i];
                }
                else
                {
                    CreateGraphicZone(ZoneGraphique, Result[(int)listPositions[i]], sortedTable, i, iNbCourbeBar);
                } 
            }
            return Result;
        }

        /// <summary>
        /// Function which generate the table linked to a custom graphic.
        /// </summary>
        /// <param name="donneeEnCours">DataBase that contains all the table to be used to generate the table.</param>
        /// <returns>Return null or the table associated for that graphic.</returns>
        public DataTable GenerateTable(GestionDonneesHUB2SIM donneeEnCours)
        {
            DataTable dtGraphic = new DataTable(this.Name);
            bool bHasAbscissa = false;
            bool bHasIndexedValues = false;
            int iIndexLineForAbscissa = -1;
            DataTable dtTmp;
            Double dNumberOfRow = -1;

            #region
            /*In a first time, we tried to figure out if the graphic has a defined column for the abscissa.
              We tried to see also if the number of rows contained in all the tables are the same.*/
            for (int i = 0; i < listAxeRepresentation.Count; i++)
            {
                if (listColumnsOrigin[i].ToString() == "")
                    return null;
                ColumnInformation informations = (ColumnInformation)listColumnsOrigin[i];
                if((informations.ExceptionName!=null) && (informations.ExceptionName != ""))
                    dtTmp = donneeEnCours.getExceptionTable
                        (informations.DataSet, informations.TableName, informations.ExceptionName);
                else
                    dtTmp = donneeEnCours.getTable(informations.DataSet, informations.TableName);
                if (dtTmp == null)
                    return null;
                if (!dtTmp.Columns.Contains(informations.ColumnName))
                    return null;
                
                if(dNumberOfRow ==-1)
                {
                    //If the number of row looked for is -1 (we still haven't initialized the number of row)
                    dNumberOfRow = dtTmp.Rows.Count;
                }
                else if ((dtTmp.Rows.Count != dNumberOfRow))
                {
                    //The number of row is different for the different tables used to make that table.
                    //So we can't continue making that table.
                    return null;
                }


                if (listAxeRepresentation[i].ToString() == "X")
                {
                    //There is one column designed for the abscissa.
                    iIndexLineForAbscissa = i;
                    bHasAbscissa = true;
                    bHasIndexedValues = false;
                    //We had to the table the current column.
                    dtGraphic.Columns.Add(informations.DisplayedName, dtTmp.Columns[informations.ColumnName].DataType);
                    //We get the index of the new created column and store it to use it later.
                    int iIndexColumn = dtTmp.Columns.IndexOf(informations.ColumnName);

                    //For every line of the current table, we had the content of the abscissa column to the result table.
                    for (int j = 0; j < dNumberOfRow; j++)
                    {
                        dtGraphic.Rows.Add(new Object[1] { dtTmp.Rows[j][iIndexColumn] });
                    }
                }
                else if((!bHasIndexedValues) && (!bHasAbscissa))
                {
                    //If there is no column designed for the abscissa, and that a column contain an information for the Abscissa
                    //Then we store that information for later.
                    bHasIndexedValues = bHasIndexedValues || informations.HadAbscissaColumn;
                    iIndexLineForAbscissa = i;
                }
            }
            if (!bHasAbscissa && bHasIndexedValues)
            {
                //If there is no column designed for the abscissa, but a column that have informations about it.
                //The we full the table with informations about abscissa.
                ColumnInformation informations = (ColumnInformation) listColumnsOrigin[iIndexLineForAbscissa];

                if ((informations.ExceptionName != null) && (informations.ExceptionName != ""))
                    dtTmp = donneeEnCours.getExceptionTable
                        (informations.DataSet, informations.TableName, informations.ExceptionName);
                else
                    dtTmp = donneeEnCours.getTable(informations.DataSet, informations.TableName);
                dtGraphic.Columns.Add(informations.AbscissaColumnName,
                                      dtTmp.Columns[informations.AbscissaColumnName].DataType);
                int iIndexColumn = dtTmp.Columns.IndexOf(informations.AbscissaColumnName);
                for (int j = 0; j < dNumberOfRow; j++)
                {
                    dtGraphic.Rows.Add(new Object[1] {dtTmp.Rows[j][iIndexColumn]});
                }
            }

            #endregion

            bHasAbscissa = bHasAbscissa || bHasIndexedValues;
            for (int i = 0; i < listAxeRepresentation.Count; i++)
            {
                if (listColumnsOrigin[i].ToString() == "")
                    return null;
                ColumnInformation informations = (ColumnInformation)listColumnsOrigin[i];
                if ((informations.ExceptionName != null) && (informations.ExceptionName != ""))
                    dtTmp = donneeEnCours.getExceptionTable
                        (informations.DataSet, informations.TableName, informations.ExceptionName);
                else
                dtTmp = donneeEnCours.getTable(informations.DataSet, informations.TableName);
                //If the current column is the abscissa information, the we continue (column already added)
                if (listAxeRepresentation[i].ToString() == "X")
                    continue;
                int iIndexX = -1;
                int iIndexY = -1;
                if (informations.HadAbscissaColumn)
                    iIndexX = dtTmp.Columns.IndexOf(informations.AbscissaColumnName);
                int iIndexnewColumn = dtGraphic.Columns.Count;
                iIndexY = dtTmp.Columns.IndexOf(informations.ColumnName);
                if(dtGraphic.Columns.Contains(informations.DisplayedName))
                    return null;
                dtGraphic.Columns.Add(informations.DisplayedName, dtTmp.Columns[iIndexY].DataType);
                for (int j = 0; j < dNumberOfRow; j++)
                {
                    if ((informations.HadAbscissaColumn) && (iIndexX != -1))
                    {
                        try
                        {
                            //The abscissa column contains information different than the abscissa of that column.
                            //So to make sure to do not give wrong information, we retur null.
                            if (dtGraphic.Rows[j][0].ToString() != dtTmp.Rows[j][iIndexX].ToString())
                                return null;
                        }
                        catch (Exception exc)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("Except02048: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                            return null;
                        }
                    }
                    if (dtGraphic.Rows.Count <=j)
                    {
                        dtGraphic.Rows.Add(new Object[1] { dtTmp.Rows[j][iIndexY] });
                    }
                    else
                    {
                        dtGraphic.Rows[j][iIndexnewColumn] = dtTmp.Rows[j][iIndexY];
                    }
                }
            }
            if (dtGraphic.Columns.Count == 0)
                return null;

            if (dtGraphic.Rows.Count == 0)
                return null;
            return dtGraphic;
        }

        #region Fonctions qui permettent de récupérer la table et le nom de la colonne qui doivent être affichées dans le graphique.
        private DataTable getGraphicTable(GestionDonneesHUB2SIM donneeEnCours, int iIndexGraphique, out String sColumnName)
        {
            sColumnName = "";
            if (listColumnsOrigin[iIndexGraphique] == null)
                return null;
            if (listColumnsOrigin[iIndexGraphique].ToString() == "")
                return null;
            if (listColumnsOrigin[iIndexGraphique].GetType() != typeof(ColumnInformation))
                return null;
            ColumnInformation informations = (ColumnInformation)listColumnsOrigin[iIndexGraphique];
            sColumnName = informations.ColumnName;
            return getGraphicTable(donneeEnCours, informations);
        }


        private static DataTable getGraphicTable(GestionDonneesHUB2SIM donneeEnCours, ColumnInformation ciGraphic)
        {
            if ((ciGraphic.ExceptionName != null) && (ciGraphic.ExceptionName != ""))
                return donneeEnCours.getExceptionTable(ciGraphic.DataSet, ciGraphic.TableName, ciGraphic.ExceptionName);
            else
                return donneeEnCours.getTable(ciGraphic.DataSet, ciGraphic.TableName);
        }

        #endregion

        /// <summary>
        /// Fonction qui dessine un des graphiques pour le filtre graphique sélectionné.
        /// </summary>
        /// <param name="ZoneGraphique"></param>
        /// <param name="donneeEnCours"></param>
        /// <param name="iIndexGraphique"></param>
        /// <returns></returns>
        private NChart CreateGraphicZone(NChartControl ZoneGraphique, GestionDonneesHUB2SIM donneeEnCours, int iIndexGraphique, int iNbBar)
        {
            if (listColumnsOrigin[iIndexGraphique].ToString() == "")
                return null;
            ColumnInformation informations = (ColumnInformation)listColumnsOrigin[iIndexGraphique];
            DataTable laTable = null;
            if ((informations.ExceptionName != null) && (informations.ExceptionName != ""))
                laTable = donneeEnCours.getExceptionTable
                    (informations.DataSet, informations.TableName, informations.ExceptionName);
            else
                laTable = donneeEnCours.getTable(informations.DataSet, informations.TableName);
            if (laTable == null)
                return null;
            int iIndexColumn = laTable.Columns.IndexOf(informations.ColumnName);
            if (iIndexColumn == -1)
                return null;
            return CreateGraphicZone(ZoneGraphique, laTable, iIndexGraphique, iNbBar);
        }

        /// <summary>
        /// Récupère le nombre de colonne qui doivent être affichées sous forme d'histogrammes pour position passée en paramètre (Top left / top Right...)
        /// Cette information est importante pour connaître la largeur que doivent prendre ces histogrammes.
        /// </summary>
        /// <param name="iPosition"></param>
        /// <returns></returns>
        private int getNumberGraphicBar(int iPosition)
        {
            int Result = 0;
            for (int i = 0; i < listAxeRepresentation.Count; i++)
            {
                if (listAxeRepresentation[i].ToString() == "X")
                    continue;
                if((int)listPositions[i] != iPosition)
                    continue;
                if ((listVisualisation[i].ToString() == "Bar")||
                    (listVisualisation[i].ToString() == "Bar stacked"))
                    Result++;
            }
            return Result;
        }
        private void CreateGraphicZone(NChartControl ZoneGraphique, NChart Graphique, DataTable laTable, int iIndexGraphique, int iNbBar)
        {
            Boolean iscandleList = false;
            if (Graphique.GetType() == typeof(NPieChart))
                return;
            int iIndexColonne, iIndexColonneX;
            if (!VerifData(laTable, iIndexGraphique, out iIndexColonne, out iIndexColonneX))
            {
                return;
            }

            String typeVisu = listVisualisation[iIndexGraphique].ToString();
            ArrayList altimes = new ArrayList();


            if (typeVisu == "Pie")
                return;

            bool bDouble, bDate, bString;
            DateTime dtFirstValue = DateTime.MinValue;
            DateTime dtLastValue = DateTime.MinValue;
            double nFirstValue, nLastValue;

            CheckDisplayedData(laTable, iIndexColonneX, out bDouble, out bDate, out bString, out dtFirstValue, out dtLastValue, out nFirstValue, out nLastValue);
            bool bAccumulation = ((Color)listAccumulation[iIndexGraphique]) != Color.Transparent;

            //if (dtFirstValue != DateTime.MinValue && dtLastValue != DateTime.MinValue)
                //dtLastValue = dtLastValue.AddMinutes(120);   // >> Bug #13387 Default Analysis Charts X axis time range

            // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
            if (setPoints.Count > 0)
                CreateSetPoint(ZoneGraphique, Graphique, (int)listPositions[iIndexGraphique]);
            // << Bug #15147 Charts Setpoints only work for first series chart location (frame)

            ///If there's columns to display as candle
            if (CandleColumnsList.Count != 0)
            {
                for (int i = 0; i < CandleColumnsList.Count; i++)
                {
                    ///if the current column is included among the three columns composing the candles
                    if (CandleColumnsList[i].ToString().Contains(listColumnsNames[iIndexGraphique].ToString()) == true)
                    {
                        iscandleList = true;
                        ///if the current column is the first column, carry out the treatment, otherwise, do nothing
                        if (i == 0)
                        {
                            if (ZoneGraphique.Charts.IndexOf(Graphique) == -1)  // Task #16728 C#42
                            {
                                ZoneGraphique.Charts.Add(Graphique);
                            }

                            double middle, max, min;
                            for (int j = 0; j < laTable.Rows.Count; j++)
                            {
                                String indexCC1 = "";
                                String indexCC2 = "";
                                String indexCC3 = "";
                                ///On fait autant de candle qu'il y a de valeurs en X 
                                for (int il = 0; il < listColumnsOrigin.Count; il++)
                                {
                                    ///Si listColumnsOrigine ne contient rien alors on utilise listColumnsNames
                                    if (listColumnsOrigin[il].ToString() != "")
                                    {
                                        ColumnInformation cI = (ColumnInformation)listColumnsOrigin[il];
                                        ///Récupérer les index de colonne des candleColonnelist
                                        if (cI.DisplayedName.CompareTo(CandleColumnsList[0].ToString()) == 0)
                                            indexCC1 = cI.ColumnName;
                                        if (cI.DisplayedName.CompareTo(CandleColumnsList[1].ToString()) == 0)
                                            indexCC2 = cI.ColumnName;
                                        if (cI.DisplayedName.CompareTo(CandleColumnsList[2].ToString()) == 0)
                                            indexCC3 = cI.ColumnName;
                                    }
                                    else
                                    {
                                        String cN = listColumnsNames[il].ToString();
                                        if (cN.CompareTo(CandleColumnsList[0].ToString()) == 0)
                                            indexCC1 = cN;
                                        if (cN.CompareTo(CandleColumnsList[1].ToString()) == 0)
                                            indexCC2 = cN;
                                        if (cN.CompareTo(CandleColumnsList[2].ToString()) == 0)
                                            indexCC3 = cN;
                                    }
                                }

                                ///On ajoute les trois valeurs des trois colonnes qui serviront à construire le candle
                                altimes.Add(laTable.Rows[j][indexCC1]);
                                altimes.Add(laTable.Rows[j][indexCC2]);
                                altimes.Add(laTable.Rows[j][indexCC3]);

                                ///On tri la liste pour pouvoir récupérer la valeur max moyennee et min
                                altimes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());

                                double.TryParse(altimes[0].ToString(),out min);
                                double.TryParse(altimes[1].ToString(), out middle);
                                double.TryParse(altimes[2].ToString(), out max);

                                CreateGraphicCandle(ZoneGraphique, Graphique, laTable, j, iIndexColonneX, iIndexGraphique,
                                bDouble, bDate, bString, dtFirstValue, dtLastValue, max, min, middle,iscandleList);
                                
                                Graphique.Wall(ChartWallType.Back).FillStyle.SetTransparencyPercent(100);
                                Graphique.Tag = "NCartesianChart";
                                altimes.Clear();
                            }
                            
                        }return;
                        
                    }
                }
            }
            if (!bDouble && !bDate && !bString)
            {
                return;
            }
            if (typeVisu == "Line")
            {
                CreateGraphicLine(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,
                    bDouble, bDate, bString, dtFirstValue, dtLastValue, bAccumulation, false);
            }
            else if ((typeVisu == "Area")
                  || (typeVisu == "Area stacked"))
            {
                CreateGraphicArea(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,
                    bDouble, bDate, bString, dtFirstValue, dtLastValue, bAccumulation, false, (typeVisu == "Area stacked"));
            }
            else if ((typeVisu == "Bar") ||
                     (typeVisu == "Bar stacked"))
            {
                CreateGraphicBar(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique, iNbBar,
                    bDouble, bDate, bString, dtFirstValue, dtLastValue, bAccumulation, typeVisu == "Bar stacked");
            }
            else if (typeVisu == "Point")
            {
                CreateGraphicCloudPoints(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,
                    bDouble, bDate, bString, dtFirstValue, dtLastValue, bAccumulation, false);
            }
            else if (typeVisu == "Candle")
            {
                ///On ajoute toute les valeurs de la colonne sélectionnée comme candle
                for (int i = 0; i < laTable.Rows.Count; i++)
                    altimes.Add(laTable.Rows[i][iIndexColonne]);
                ///On tri ses valeurs
                altimes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                ///On récupère le type de valeur max moyenne et min voulu ( Max, 99%, 95%, 75%, Mean, Min-0, Min)
                String MxValueSt = MaxCandleValue[iIndexGraphique].ToString();
                String MdValueSt = MidCandleValue[iIndexGraphique].ToString();
                String MnValueSt = MinCandleValue[iIndexGraphique].ToString();
                int indexValue;
                double dFirstLevel, min, max;
                
                ///Si la valeur Min est Min alors on envoie directement la valeur minimum ajoutée dans la liste
                if (MnValueSt.CompareTo("Min") == 0)
                    double.TryParse(altimes[OverallTools.ResultFunctions.getLowLevelValue(altimes, 0)].ToString(),out min);
                ///Sinon on appelle la fonction getLowLevelValue qui va calculer la valeur à partir de la liste
                else
                {
                    ///getLowLevelValue retourne l'indice de tableau ou la valeur calculée est positionnée
                    indexValue = OverallTools.ResultFunctions.getLowLevelValue(altimes, 1);
                    double.TryParse(altimes[indexValue].ToString(), out min);
                    ArrayList tmp = new ArrayList();
                    ///On recopie et supprime la liste altimes dans une liste temporaire pour pouvoir établir la nouvelle liste
                    for (int i = 0; i < altimes.Count; i++)
                        tmp.Add(altimes[i]);
                    altimes.Clear();
                    ///On ajoute les valeurs à partir de la valeur minimum calculée
                    for (int i = indexValue; i < tmp.Count; i++)
                        altimes.Add(tmp[i]);
                }
                ///Si la valeur Max est Max alors on envoie directement la valeur maximum ajoutée dans la liste
                if (MxValueSt.CompareTo("Max") == 0)
                    double.TryParse(altimes[OverallTools.ResultFunctions.getHighLevelValue(altimes, 100)].ToString(), out max);
                ///Sinon on appelle la fonction getHighLevelValue qui va calculer la valeur high à partir de la liste calculée avec altimes
                else
                {
                    indexValue = OverallTools.ResultFunctions.getHighLevelValue(altimes, 99);
                    double.TryParse(altimes[indexValue].ToString(), out max);
                    ArrayList tmp = new ArrayList();
                    for (int i = 0; i < altimes.Count; i++)
                        tmp.Add(altimes[i]);
                    altimes.Clear();
                    for (int i = 0; i < indexValue; i++)
                        altimes.Add(tmp[i]);
                }
                
                if (MdValueSt.CompareTo("99%") == 0)
                    dFirstLevel = OverallTools.ResultFunctions.getLevelValue(altimes, 99);
                else if (MdValueSt.CompareTo("95%") == 0)
                    dFirstLevel = OverallTools.ResultFunctions.getLevelValue(altimes, 95);
                else if (MdValueSt.CompareTo("75%") == 0)
                    dFirstLevel = OverallTools.ResultFunctions.getLevelValue(altimes, 75);
                else
                    dFirstLevel = OverallTools.ResultFunctions.getLevelValue(altimes, 50);

                CreateGraphicCandle(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,
                    bDouble, false, bString, dtFirstValue, dtLastValue, max, min, dFirstLevel,false);
            }
        }

        //Called when new chart position
        private NChart CreateGraphicZone(NChartControl ZoneGraphique, DataTable laTable, int iIndexGraphique, int iNbBar)
        {
            int iIndexColonne,iIndexColonneX;
            Boolean isCandleList = false;

            if (!VerifData(laTable, iIndexGraphique, out iIndexColonne, out iIndexColonneX))
            {
                return null;
            }
            if (laTable.Rows.Count == 0)
                return null;
            //dtResult
            String typeVisu = listVisualisation[iIndexGraphique].ToString();
            if (typeVisu != "Pie")
            {
                bool bDouble, bDate, bString;
                double nFirstValue, nLastValue;
                ArrayList altimes = new ArrayList();
                DateTime dtFirstValue = DateTime.MinValue;
                DateTime dtLastValue = DateTime.MinValue;
                CheckDisplayedData(laTable, iIndexColonneX, out bDouble, out bDate, out bString, out dtFirstValue, out dtLastValue, out nFirstValue, out nLastValue);
                bool bAccumulation = ((Color)listAccumulation[iIndexGraphique]) != Color.Transparent;
                if (!bString && !bDouble && !bDate)
                {
                    return null;
                }
                //if (dtFirstValue != DateTime.MinValue && dtLastValue != DateTime.MinValue)
                    //dtLastValue = dtLastValue.AddMinutes(120);   // >> Bug #13387 Default Analysis Charts X axis time range

                /*if (dtFirstValue != DateTime.MinValue && dtLastValue != DateTime.MinValue)
                {
                    dtFirstValue = new DateTime(dtFirstValue.Year, dtFirstValue.Month, dtFirstValue.Day, dtFirstValue.Hour, 0, 0);
                    dtLastValue = new DateTime(dtLastValue.Year, dtLastValue.Month, dtLastValue.Day, dtLastValue.Hour + 1, 0, 0);
                }*/

                List<int> res = new List<int>();

                NCartesianChart Graphique = new NCartesianChart();

                if (typeVisu != "Candle")
                {
                    ///Si l'axe est de type Date
                    if (bDate)
                    {
                        NDateTimeAxisPagingView dateTimePagingView;
                        DateTime viewBeginDate = DateTime.MinValue;
                        DateTime viewEndDate = DateTime.MinValue;
                        /// Si l'axe a été changé par l'intermédiaire de du gestionnaire d'axe alors on définit l'axe avec les données existante dans la liste AxisList
                        if (AxisList[(int)listPositions[iIndexGraphique]][0].BeginDTValue != new DateTime(1, 1, 1))
                        {
                            dateTimePagingView = new NDateTimeAxisPagingView(AxisList[(int)listPositions[iIndexGraphique]][0].BeginDTValue, new NDateTimeSpan(AxisList[(int)listPositions[iIndexGraphique]][0].LengthDTValue, NDateTimeUnit.Hour));
                            // >> Bug #13387 Default Analysis Charts X axis time range C#14                            
                            viewBeginDate = AxisList[(int)listPositions[iIndexGraphique]][0].BeginDTValue;
                            viewEndDate = viewBeginDate.AddHours(AxisList[(int)listPositions[iIndexGraphique]][0].LengthDTValue);
                            // << Bug #13387 Default Analysis Charts X axis time range C#14
                        }
                        else
                        {
                            //dateTimePagingView = new NDateTimeAxisPagingView(((DateTime)laTable.Rows[0][iIndexColonneX]), setDateTimeSpan(dtLastValue.Subtract(dtFirstValue)));
                            // >> Bug #13387 Default Analysis Charts X axis time range C#14
                            double totalHours = Math.Ceiling((double)(dtLastValue.Subtract(dtFirstValue)).TotalHours);
                            viewBeginDate = ((DateTime)laTable.Rows[0][iIndexColonneX]);
                            viewEndDate = viewBeginDate.AddHours(totalHours);

                            /*viewBeginDate = viewBeginDate.AddMinutes(-15);
                            if (dtLastValue.Subtract(dtFirstValue).Minutes >= 45)
                            {
                                viewEndDate = viewEndDate.AddMinutes(15);
                            }*/
                            dateTimePagingView = new NDateTimeAxisPagingView(viewBeginDate, setDateTimeSpan(viewEndDate.Subtract(viewBeginDate)));
                            // << Bug #13387 Default Analysis Charts X axis time range C#14
                        }
                        dateTimePagingView.Enabled = true;
                        Graphique.Axis(StandardAxis.PrimaryX).PagingView = dateTimePagingView;
                        Graphique.Axis(StandardAxis.PrimaryX).View = new NRangeAxisView(new NRange1DD(viewBeginDate.ToOADate(), viewEndDate.ToOADate()));   // >> Bug #13387 Default Analysis Charts X axis time range C#14
                    }
                    else
                    {
                        ///Ici on doit déterminer quelle est la dernière valeur
                        double lastvalue = laTable.Rows.Count;
                        NNumericAxisPagingView numericPagingView;
                        // << Task #7570 new Desk and extra information for Pax -Phase I B                        
                        NRangeAxisView axisView;
                        // >> Task #7570 new Desk and extra information for Pax -Phase I B
                        ///Si le champ LengthNValue de la liste contenant l'abscisse est différent de -1, cela signifie que l'échelle de l'abscisse a déja été changé
                        ///donc on doit utiliser les données contenues dans cette liste pour définir l'axe.
                        if (AxisList[(int)listPositions[iIndexGraphique]][0].LengthNValue != -1)
                        {
                            numericPagingView = new NNumericAxisPagingView(new NRange1DD(AxisList[(int)listPositions[iIndexGraphique]][0].BeginNValue - 0.5, AxisList[(int)listPositions[iIndexGraphique]][0].LengthNValue));
                            // << Task #7570 new Desk and extra information for Pax -Phase I B 
                            axisView = new NRangeAxisView(new NRange1DD(AxisList[(int)listPositions[iIndexGraphique]][0].BeginNValue - 0.5, AxisList[(int)listPositions[iIndexGraphique]][0].LengthNValue));
                            // >> Task #7570 new Desk and extra information for Pax -Phase I B
                        }
                        ///Sinon on définit l'axe par le nombre de valeurs à afficher
                        else
                        {
                            if (iIndexColonneX == -1)
                            {
                                numericPagingView = new NNumericAxisPagingView(new NRange1DD(-0.5, lastvalue));
                                // << Task #7570 new Desk and extra information for Pax -Phase I B
                                axisView = new NRangeAxisView(new NRange1DD(-0.5, lastvalue));
                                // >> Task #7570 new Desk and extra information for Pax -Phase I B
                            }
                            else
                            {  
                                numericPagingView = new NNumericAxisPagingView(new NRange1DD(nFirstValue - 0.5, nLastValue));
                                // << Task #7570 new Desk and extra information for Pax -Phase I B
                                axisView = new NRangeAxisView(new NRange1DD(nFirstValue - 0.5, nLastValue));
                                // >> Task #7570 new Desk and extra information for Pax -Phase I B                                
                            }
                        }
                        Graphique.Axis(StandardAxis.PrimaryX).PagingView = numericPagingView;
                        // << Task #7570 new Desk and extra information for Pax -Phase I B
                        Graphique.Axis(StandardAxis.PrimaryX).View = axisView;
                        // >> Task #7570 new Desk and extra information for Pax -Phase I B
                        ///On met le tag de l'axe à XNuméricValue pour que lorsque l'on utilisera l'assistant de gestion d'axe on sache que c'est l'axe X utilise des valeurs numériques
                        Graphique.Axis(StandardAxis.PrimaryX).Tag = "XNumericValue";
                        Graphique.Axis(StandardAxis.SecondaryX).Tag = "None";
                        if (bString)
                        {
                            Graphique.Axis(StandardAxis.SecondaryX).PagingView = numericPagingView;
                            // << Task #7570 new Desk and extra information for Pax -Phase I B
                            Graphique.Axis(StandardAxis.SecondaryX).View = axisView;
                            // >> Task #7570 new Desk and extra information for Pax -Phase I B
                            ///On met le tag de l'axe à XNuméricValue pour que lorsque l'on utilisera l'assistant de gestion d'axe on sache que c'est l'axe X utilise des valeurs numériques
                            Graphique.Axis(StandardAxis.SecondaryX).Tag = "String";
                        }
                    }

                    /// Si l'axe Y a été changé par l'intermédiaire de du gestionnaire d'axe alors on définit l'axe avec les données existante dans la liste AxisList
                    if (AxisList[(int)listPositions[iIndexGraphique]][1].LengthNValue != -1)
                    {
                        NNumericAxisPagingView numericPagingView = new NNumericAxisPagingView(new NRange1DD(AxisList[(int)listPositions[iIndexGraphique]][1].BeginNValue, AxisList[(int)listPositions[iIndexGraphique]][1].LengthNValue));
                        Graphique.Axis(StandardAxis.PrimaryY).PagingView = numericPagingView;
                        // << Task #7570 new Desk and extra information for Pax -Phase I B
                        NRangeAxisView axisView = new NRangeAxisView(new NRange1DD(AxisList[(int)listPositions[iIndexGraphique]][1].BeginNValue, AxisList[(int)listPositions[iIndexGraphique]][1].LengthNValue));
                        Graphique.Axis(StandardAxis.PrimaryY).View = axisView;
                        // >> Task #7570 new Desk and extra information for Pax -Phase I B
                        Graphique.Axis(StandardAxis.PrimaryY).Tag = "YNumericValue";

                    }
                    /// Si l'axe Y2 a été changé par l'intermédiaire de du gestionnaire d'axe alors on définit l'axe avec les données existante dans la liste AxisList
                    if (AxisList[(int)listPositions[iIndexGraphique]][2].LengthNValue != -1)
                    {
                        NNumericAxisPagingView numericPagingView = new NNumericAxisPagingView(new NRange1DD(AxisList[(int)listPositions[iIndexGraphique]][2].BeginNValue, AxisList[(int)listPositions[iIndexGraphique]][2].LengthNValue));
                        Graphique.Axis(StandardAxis.SecondaryY).PagingView = numericPagingView;
                        // << Task #7570 new Desk and extra information for Pax -Phase I B
                        NRangeAxisView axisView = new NRangeAxisView(new NRange1DD(AxisList[(int)listPositions[iIndexGraphique]][2].BeginNValue, AxisList[(int)listPositions[iIndexGraphique]][2].LengthNValue));
                        Graphique.Axis(StandardAxis.SecondaryY).View = axisView;
                        // >> Task #7570 new Desk and extra information for Pax -Phase I B
                        Graphique.Axis(StandardAxis.SecondaryY).Tag = "Y2NumericValue";
                    }
                }
                //If the Scrollbar is activated for the chart position
                if (lesPositionsXScrollbar.Count != 0)
                {
                    if (lesPositionsXScrollbar[(int)listPositions[iIndexGraphique]] == true)
                    {
                        MyEventArgs evt;
                        ///Axe de type double donc on peut mettre une DateTime scrollbar 
                        if (bDate)
                        {
                            ///On affiche le bouton reset a coté de la scrollbar qui nous permettra de retourner en mode normal
                            Graphique.Axis(StandardAxis.PrimaryX).ScrollBar.ResetButton.Visible = true;
                            NDateTimeAxisPagingView dateTimePagingView;
                            if (AxisList[(int)listPositions[iIndexGraphique]][0].BeginDTValue != new DateTime(1, 1, 1))
                            {
                                dateTimePagingView = new NDateTimeAxisPagingView(AxisList[(int)listPositions[iIndexGraphique]][0].BeginDTValue, new NDateTimeSpan((long)AxisList[(int)listPositions[iIndexGraphique]][0].LengthDTValue, NDateTimeUnit.Hour));
                            }
                            else
                                dateTimePagingView = new NDateTimeAxisPagingView(((DateTime)laTable.Rows[0][iIndexColonneX]), setDateTimeSpan(dtLastValue.Subtract(dtFirstValue)));
                            dateTimePagingView.Enabled = true;
                            ///Si Aucun axe n'a été définit précédemment donc on définit l'axe ici.
                            if (AxisList[(int)listPositions[iIndexGraphique]][0].BeginDTValue == new DateTime(1, 1, 1))
                                Graphique.Axis(StandardAxis.PrimaryX).PagingView = dateTimePagingView;

                            ///On précise à la fonction qui sera appelée à chaque changement de valeur de la scrollbar que l'axe est de type DateTime
                            evt = new MyEventArgs();
                            evt.GraphicArea = ZoneGraphique;
                            evt.scaleType = "DateTimeScale";
                            Graphique.Axis(StandardAxis.PrimaryX).ScrollBar.Visible = true;

                            //This function will be called every time we move the scrollbar
                            Graphique.Axis(StandardAxis.PrimaryX).Scale.RulerRangeChanged += evt.ScrollbarValueChanged; //ScrollBar.BeginValueChanged += evt.ScrollbarValueChanged;
                            ZoneGraphique.Controller.Tools.Add(new NAxisScrollTool());
                        }
                        ///Axe de type double donc on peut mettre une linear scrollbar 
                        else
                        {
                            Graphique.Axis(StandardAxis.PrimaryX).ScrollBar.ResetButton.Visible = true;
                            ///Si Aucun axe n'a été définit précédemment donc on définit l'axe ici.
                            //NNumericAxisPagingView numericPagingView;
                            //if(AxisList[(int)listPositions[iIndexGraphique]][0].LengthNValue == -1 || AxisList[(int)listPositions[iIndexGraphique]][0].LengthNValue == 0)
                            //    NNumericAxisPagingView numericPagingView = new NNumericAxisPagingView(new NRange1DD(AxisList[(int)listPositions[iIndexGraphique]][0].BeginNValue, AxisList[(int)listPositions[iIndexGraphique]][0].LengthNValue));
                            //if (AxisList[(int)listPositions[iIndexGraphique]][0].LengthNValue == -1)
                            //   Graphique.Axis(StandardAxis.PrimaryX).PagingView = numericPagingView;

                            evt = new MyEventArgs();
                            evt.GraphicArea = ZoneGraphique;
                            evt.scaleType = "LinearScale";
                            Graphique.Axis(StandardAxis.PrimaryX).ScrollBar.Visible = true;

                            //This function will be called every time we move the scrollbar
                            Graphique.Axis(StandardAxis.PrimaryX).Scale.RulerRangeChanged += evt.ScrollbarValueChanged; //ScrollBar.BeginValueChanged += evt.ScrollbarValueChanged;
                            ZoneGraphique.Controller.Tools.Add(new NAxisScrollTool());
                        }
                    }
                }
                // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
                if (setPoints.Count > 0)
                    CreateSetPoint(ZoneGraphique, Graphique, (int)listPositions[iIndexGraphique]);
                // << Bug #15147 Charts Setpoints only work for first series chart location (frame)

                //If there's columns to display as candle
                if (CandleColumnsList.Count != 0)
                {
                    for (int i = 0; i < CandleColumnsList.Count; i++)
                    {
                        //if the current column is included among the three columns composing the candles
                        if (CandleColumnsList[i].ToString().Contains(listColumnsNames[iIndexGraphique].ToString()) == true)
                        {
                            isCandleList = true;

                            //if the current column is the first column, carry out the treatment, otherwise, do nothing
                            if (i == 0)
                            {
                                double middle, max, min;
                                ZoneGraphique.Charts.Add(Graphique);

                                for (int j = 0; j < laTable.Rows.Count; j++)
                                {            
                                    PlaceXvalues(Graphique, laTable, iIndexColonneX);

                                    String indexCC1 = "";
                                    String indexCC2 = "";
                                    String indexCC3 = "";
                                    for (int il = 0; il < listColumnsOrigin.Count; il++)
                                    {
                                        if (listColumnsOrigin[il].ToString() != "")
                                        {
                                            ColumnInformation cI = (ColumnInformation)listColumnsOrigin[il];
                                            if (cI.DisplayedName.CompareTo(CandleColumnsList[0].ToString()) == 0)
                                                indexCC1 = cI.ColumnName;
                                            if (cI.DisplayedName.CompareTo(CandleColumnsList[1].ToString()) == 0)
                                                indexCC2 = cI.ColumnName;
                                            if (cI.DisplayedName.CompareTo(CandleColumnsList[2].ToString()) == 0)
                                                indexCC3 = cI.ColumnName;
                                        }
                                        else
                                        {
                                            String cN = listColumnsNames[il].ToString();
                                            if (cN.CompareTo(CandleColumnsList[0].ToString()) == 0)
                                                indexCC1 = cN;
                                            if (cN.CompareTo(CandleColumnsList[1].ToString()) == 0)
                                                indexCC2 = cN;
                                            if (cN.CompareTo(CandleColumnsList[2].ToString()) == 0)
                                                indexCC3 = cN;
                                        }
                                    }
                                    if (indexCC1 == "")
                                        indexCC1 = CandleColumnsList[0].ToString();
                                    if(indexCC2 == "")
                                        indexCC2 = CandleColumnsList[1].ToString();
                                    if(indexCC3 == "")
                                        indexCC3 = CandleColumnsList[2].ToString();
                                    altimes.Add(laTable.Rows[j][indexCC1]);
                                    altimes.Add(laTable.Rows[j][indexCC2]);
                                    altimes.Add(laTable.Rows[j][indexCC3]);

                                    altimes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());

                                    double.TryParse(altimes[0].ToString(), out min);
                                    double.TryParse(altimes[1].ToString(), out middle);
                                    double.TryParse(altimes[2].ToString(), out max);
                                    
                                    CreateGraphicCandle(ZoneGraphique, Graphique, laTable, j, iIndexColonneX, iIndexGraphique,
                                    bDouble, bDate, bString, dtFirstValue, dtLastValue,max, min, middle,isCandleList);
                                    
                                    Graphique.Wall(ChartWallType.Back).FillStyle.SetTransparencyPercent(100);
                                    Graphique.Tag = "NCartesianChart";
                                    altimes.Clear();
                                }
                                return Graphique;
                            }
                            return null;
                        }
                    }
                }

                if (typeVisu == "Line")
                {
                    /*if (bString)//Les lignes ne sont pas dispo lorsque l'axe X contient des chaines de caratères.
                        return null;*/

                    PlaceXvalues(Graphique, laTable, iIndexColonneX);
                    ZoneGraphique.Charts.Add(Graphique);
                    CreateGraphicLine(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,
                        bDouble, bDate, bString, dtFirstValue, dtLastValue, bAccumulation, false);
                    if ((((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text == "") && (bAccumulation))
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = "Cumul.%";
                    }
                    Graphique.Wall(ChartWallType.Back).FillStyle.SetTransparencyPercent(100);
                    Graphique.Tag = "NCartesianChart";
                    return Graphique;
                }
                else if(( typeVisu == "Area")||
                    ( typeVisu == "Area stacked"))
                {


                    PlaceXvalues(Graphique, laTable, iIndexColonneX);
                    ZoneGraphique.Charts.Add(Graphique);
                    CreateGraphicArea(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,
                        bDouble, bDate, bString, dtFirstValue, dtLastValue, bAccumulation, false, (typeVisu == "Area stacked"));
                    if ((((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text == "") && (bAccumulation))
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = "Cumul.%";
                    }
                    Graphique.Wall(ChartWallType.Back).FillStyle.SetTransparencyPercent(100);
                    Graphique.Tag = "NCartesianChart";
                    return Graphique;
                }
                else if ((typeVisu == "Bar")||
                    (typeVisu == "Bar stacked"))
                {

                    PlaceXvalues(Graphique, laTable, iIndexColonneX);
                    //Graphique.BoundsMode = BoundsMode.Stretch;
                    ZoneGraphique.Charts.Add(Graphique);
                    CreateGraphicBar(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,iNbBar,
                        bDouble, bDate, bString, dtFirstValue, dtLastValue, bAccumulation, typeVisu == "Bar stacked");
                    if ((((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text == "") && (bAccumulation))
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = "Cumul.%";
                    }
                    Graphique.Wall(ChartWallType.Back).FillStyle.SetTransparencyPercent(100);
                    Graphique.Tag = "NCartesianChart";
                    return Graphique;
                }
                else if (typeVisu == "Point")
                {
                    /*if (bString)//Les lignes ne sont pas dispo lorsque l'axe X contient des chaines de caratères.
                        return null;*/

                    PlaceXvalues(Graphique, laTable, iIndexColonneX);
                    ZoneGraphique.Charts.Add(Graphique);
                    CreateGraphicCloudPoints(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,
                        bDouble, bDate, bString, dtFirstValue, dtLastValue, bAccumulation, false);
                    if ((((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text == "") && (bAccumulation))
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = "Cumul.%";
                    }
                    Graphique.Wall(ChartWallType.Back).FillStyle.SetTransparencyPercent(100);
                    Graphique.Tag = "NCartesianChart";
                    return Graphique;
                }
                else if (typeVisu == "Candle")
                {
                    /*if (bString)//Les lignes ne sont pas dispo lorsque l'axe X contient des chaines de caratères.
                        return null;*/

                    PlaceXvalues(Graphique, laTable, iIndexColonneX);
                    ZoneGraphique.Charts.Add(Graphique);

                    for (int i = 0; i < laTable.Rows.Count; i++)
                        altimes.Add(laTable.Rows[i][iIndexColonne]);
                    altimes.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                    
                    ///On récupère les trois types de min moyenne et max utilisés pour faire le candle
                    String MxValueSt = MaxCandleValue[iIndexGraphique].ToString();
                    String MdValueSt = MidCandleValue[iIndexGraphique].ToString();
                    String MnValueSt = MinCandleValue[iIndexGraphique].ToString();
                    int indexValue;
                    double dFirstLevel, min,max;

                    ///Si pour minValue on on prend la valeur minimum alors on a juste a appeler la fonction getLowLevelValue avec l'enssemble des valeurs de cette colonne (altimes)
                    ///et on précise que l'on désire récupérer la valeur la plus basse (0) d'ou getLowLevelValue(altimes, 0). Enfin on place le resultat dans la variable min
                    if (MnValueSt.CompareTo("Min") == 0)
                        double.TryParse(altimes[OverallTools.ResultFunctions.getLowLevelValue(altimes, 0)].ToString(), out min);
                    ///Sinon cela signifie que l'on souhaite utiliser la valeur minimum - zero
                    else
                    {
                        ///On envoie  à getLowLevelValue l'enssemble des valeurs et 1 et on récupére l'index à laquelle la valeur min - zero est positionnée dans la liste altimes
                        indexValue = OverallTools.ResultFunctions.getLowLevelValue(altimes, 1);
                        ///On récupère la valeur min dans altimes à l'index indexValue
                        double.TryParse(altimes[indexValue].ToString(), out min);
                        ArrayList tmp = new ArrayList();
                        ///On ajoute tous les éléments de Altimes dans une liste intermidiaire
                        for (int i = 0; i < altimes.Count; i++)
                            tmp.Add(altimes[i]);
                        ///On éfface la liste altimes pour pouvoir écrire dedans tous les éléments à partir de la valeur minimum récupérée
                        altimes.Clear();
                        for (int i = indexValue; i < tmp.Count; i++)
                            altimes.Add(tmp[i]);
                        ///On a maintenant un tableau de la taille de altimes moins la valeur min - zero
                    }

                    ///On effectue la meme chose pour la valeur maximum
                    if (MxValueSt.CompareTo("Max") == 0)
                        double.TryParse(altimes[OverallTools.ResultFunctions.getHighLevelValue(altimes, 100)].ToString(), out max);
                    else
                    {
                        indexValue = OverallTools.ResultFunctions.getHighLevelValue(altimes, 99);
                        double.TryParse(altimes[indexValue].ToString(), out max);
                        ArrayList tmp = new ArrayList();
                        for (int i = 0; i < altimes.Count; i++)
                            tmp.Add(altimes[i]);
                        altimes.Clear();
                        for (int i = 0; i < indexValue; i++)
                            altimes.Add(tmp[i]);
                    }
                    ///Idem pour la valeur moyenne, si c'est 99% on envoit 99, 75% => 75...
                    if (MdValueSt.CompareTo("99%") == 0)
                        dFirstLevel = OverallTools.ResultFunctions.getLevelValue(altimes, 99);
                    else if (MdValueSt.CompareTo("95%") == 0)
                        dFirstLevel = OverallTools.ResultFunctions.getLevelValue(altimes, 95);
                    else if (MdValueSt.CompareTo("75%") == 0)
                        dFirstLevel = OverallTools.ResultFunctions.getLevelValue(altimes, 75);
                    else
                        dFirstLevel = OverallTools.ResultFunctions.getLevelValue(altimes, 50);

                    CreateGraphicCandle(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique,
                        bDouble, false, bString, dtFirstValue, dtLastValue, max, min, dFirstLevel,false);
                    
                    Graphique.Wall(ChartWallType.Back).FillStyle.SetTransparencyPercent(100);
                    Graphique.Tag = "NCartesianChart";
                    return Graphique;
                }
            }
            else
            {
                return CreateGraphicPie(laTable, iIndexColonne, iIndexColonneX/*, iIndexGraphique*/);
            }
            return null;
        }

        private bool VerifData(DataTable laTable, int iIndexGraphique,
                                out int iIndexColonne, out int iIndexColonneX)
        {
            iIndexColonne = -1;
            iIndexColonneX = -1;
            //Si l'index du graphique n'est pas valide, alors on renvoie un booléen indiquant que la 
            //valeur n'est pas valide.
            if (iIndexGraphique == -1)
                return false;
            if (iIndexGraphique >= listColumnsNames.Count)
                return false;

            //Si le contenu de la liste listColumnsOrigin est différent de "", ça veut dire que l'on traite un
            //graphique général.
            if (listColumnsOrigin[iIndexGraphique].ToString() != "")
            {
                iIndexColonne = laTable.Columns.IndexOf(((ColumnInformation)listColumnsOrigin[iIndexGraphique]).ColumnName);
            }
            else
            {
                iIndexColonne = laTable.Columns.IndexOf(listColumnsNames[iIndexGraphique].ToString());
            }
            if (iIndexColonne == -1)
                return false;
            iIndexColonneX = listAxeRepresentation.IndexOf("X");

            if ((listColumnsOrigin[iIndexGraphique].ToString() != "") &&
                (listColumnsOrigin[iIndexGraphique].GetType() == typeof(ColumnInformation)))
            {
                ColumnInformation informations = (ColumnInformation)listColumnsOrigin[iIndexGraphique];

                if (informations.HadAbscissaColumn)
                {
                    iIndexColonneX = laTable.Columns.IndexOf(informations.AbscissaColumnName);
                }
            }
            else if (iIndexColonneX != -1)
            {
                /*if (listColumnsOrigin[iIndexGraphique].ToString() != "")
                {
                    if (laTable.TableName == ((ColumnInformation)listColumnsOrigin[iIndexGraphique]).TableName)
                    {
                        iIndexColonneX = laTable.Columns.IndexOf(listColumnsNames[iIndexGraphique].ToString());
                        if (iIndexColonne == iIndexColonneX)
                            return false;
                    }
                    else
                    {
                        iIndexColonneX = -1;
                    }
                }
                else
                {*/
                    iIndexColonneX = laTable.Columns.IndexOf(listColumnsNames[iIndexColonneX].ToString());
                    if (iIndexColonne == iIndexColonneX)
                        return false;
                //}
            }
            return true;
        }
        #endregion

        #region Fonction pour vérifier le type de données contenue dans la colonne pour l'axe des abscisses
        internal void CheckDisplayedData(DataTable laTable, int iIndexColonneX, out bool bDouble, out bool bDate, out bool bString, out DateTime dtFirstValue, out DateTime dtLastValue, out double nFirstValue, out double nLastValue)
        {
            dtFirstValue = DateTime.MaxValue;
            dtLastValue = DateTime.MinValue;
            nFirstValue = double.MaxValue;
            nLastValue = double.MinValue;
            bDate = false;
            bDouble = false;
            bString = false;
            if (iIndexColonneX == -1)
            {
                nFirstValue = 0;
                nLastValue = 1;
                bDouble = true;
                return;
            }
            ///Si les données de la colonne sont du type DateTime
            if(laTable.Columns[iIndexColonneX].DataType == typeof(DateTime))
            {
                DateTime tmp;
                foreach (DataRow ligne in laTable.Rows)
                {
                    ///On calcul la première et dernière valeur de la liste
                    if (ligne.ItemArray[iIndexColonneX] == null || !DateTime.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out tmp)) // >> Bug - IST chart
                        continue;
                    //tmp = (DateTime)ligne.ItemArray[iIndexColonneX];
                    if (tmp < dtFirstValue)
                        dtFirstValue = tmp;
                    if (tmp > dtLastValue)
                        dtLastValue = tmp;
                }
                bDate = true;
            }
            /// Sinon Si de type String, le premier element est à la valeur 0 et la dernière au nombre d'éléments contenus dans le tableau
            else if ((laTable.Columns[iIndexColonneX].DataType == typeof(String)) ||
                (laTable.Columns[iIndexColonneX].DataType == typeof(string)))
            {
                nFirstValue = 0;
                nLastValue = laTable.Rows.Count;
                bString = true;
            }
            ///Si de type int ou double on calcul de la meme manière que avec les DateTime
            else if ((laTable.Columns[iIndexColonneX].DataType == typeof(int)) ||
                    (laTable.Columns[iIndexColonneX].DataType == typeof(Int16)) ||
                    (laTable.Columns[iIndexColonneX].DataType == typeof(Int32)) ||
                    (laTable.Columns[iIndexColonneX].DataType == typeof(Int64)) ||
                    (laTable.Columns[iIndexColonneX].DataType == typeof(double)) ||
                    (laTable.Columns[iIndexColonneX].DataType == typeof(Double)))
            {
                double tmp;
                foreach (DataRow ligne in laTable.Rows)
                {
                    double.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out tmp);
                    if (tmp < nFirstValue)
                        nFirstValue = tmp;
                    if (tmp > nLastValue)
                        nLastValue = tmp;
                }
                bDouble = true;
            }
        }
        #endregion

        /// <summary>
        /// Configure l'abscisse C avec les données de la table et de la colonne iIndexColonneX
        /// </summary>
        /// <param name="Graphique"></param>
        /// <param name="laTable"></param>
        /// <param name="iIndexColonneX"></param>
        private void PlaceXvalues(NChart Graphique, DataTable laTable,int iIndexColonneX)
        {
            if (iIndexColonneX == -1)
                return;
            if ((laTable.Columns[iIndexColonneX].DataType == typeof(Double))||
                (laTable.Columns[iIndexColonneX].DataType == typeof(double))||
                (laTable.Columns[iIndexColonneX].DataType == typeof(int))||
                (laTable.Columns[iIndexColonneX].DataType == typeof(Int32)) ||
                (laTable.Columns[iIndexColonneX].DataType == typeof(Int16)) ||
                (laTable.Columns[iIndexColonneX].DataType == typeof(Int64)) ||
                (laTable.Columns[iIndexColonneX].DataType == typeof(DateTime)))
                return;
            NStandardScaleConfigurator scaleConfiguratorX = null;
            scaleConfiguratorX = (NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator;
            scaleConfiguratorX.MajorTickMode = MajorTickMode.AutoMaxCount;
            scaleConfiguratorX.AutoLabels = false;

            foreach (DataRow ligne in laTable.Rows)
            {
                scaleConfiguratorX.Labels.Add(ligne.ItemArray[iIndexColonneX].ToString());
            }
        }




        #region Fonction pour l'ajout sur un Cartesian chart d'un nouveau graphique en mode Line.
        private void CreateGraphicLine(NChartControl ZoneGraphique, NChart Graphique, DataTable laTable,
            int iIndexColonne, int iIndexColonneX, int iIndexGraphique,
            bool bDouble, bool bDate, bool bString, DateTime firstValue, DateTime lastValue,
            bool bAccumulation, bool bAccumuler)
        {
            if ((bAccumulation) && (!bAccumuler))
            {
                CreateGraphicLine(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique, bDouble, bDate, bString, firstValue, lastValue, bAccumulation, true);
            }
            //La visualisation souhaitée est sur un graphique cartesian
            NLineSeries Serie;
            Serie = (NLineSeries)Graphique.Series.Add(SeriesType.Line);            
            Serie.DataLabelStyle.Visible = false;
            Graphique.Series.RemoveAt(Graphique.Series.Count - 1);
            Graphique.Series.Insert(0, Serie);
            // if (!bDouble)
            if (bString)
            {
                Serie.DisplayOnAxis(StandardAxis.SecondaryX, true);
                Serie.DisplayOnAxis(StandardAxis.PrimaryX, false);
            }
            Serie.UseXValues = true;
            if (ZoneGraphique != null)
            {
                ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator).Title.Text = XTitle;
                ///Si on cherche à créer une ligne sur l'axe Y2 ou une accumulation
                if ((listAxeRepresentation[iIndexGraphique].ToString() == "Y2") || bAccumuler)
                {
                    //On ajoute cette courbe sur l'axe Y2
                    Serie.DisplayOnAxis(StandardAxis.PrimaryY, false);
                    Serie.DisplayOnAxis(StandardAxis.SecondaryY, true);
                    Graphique.Axis(StandardAxis.SecondaryY).Visible = true;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Color = Color.Gray;
                    ///Si l'axe Y2 ne possède pas de titre et que l'on veut faire une accumulation
                    if ((Y2Title == "") && (bAccumuler))
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = "Cumul.%";
                    }else{
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = Y2Title;
                    }
                    //((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Pattern = LinePattern.DashDotDot;
                }
                else
                {
                    ///Sinon on ajoute sur l'axe Y
                    Serie.DisplayOnAxis(StandardAxis.PrimaryY, true);
                    Serie.DisplayOnAxis(StandardAxis.SecondaryY, false);
                    //((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Color = Color.Black;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryY).ScaleConfigurator).Title.Text = YTitle;
                }
            }
            ///On définit le style la courbe d'accumulation
            if (bAccumuler)
            {
                Serie.BorderStyle = new NStrokeStyle((Color)listAccumulation[iIndexGraphique]);
                Serie.BorderStyle.Width = new NLength(1.5f);
                Serie.FillStyle = new NColorFillStyle((Color)listAccumulation[iIndexGraphique]);
            }
            /// On définit le style de la courbe si ce n'est pas une accumulation
            else
            {
                Serie.BorderStyle = getListStrokeCouleurs(iIndexGraphique);
                Serie.BorderStyle.Width = new NLength(1.5f);
                Serie.FillStyle = getListFillCouleurs(iIndexGraphique);
            }
            //NStandardScaleConfigurator scaleConfiguratorX = null;
            if (bDate)
            {
                Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator = setXScale(lastValue.Subtract(firstValue));
            }
            //On ne veut pas afficher les valeurs pour chaque point.
            Serie.DataLabelStyle.Visible = false;
            Serie.Name = listColumnsNames[iIndexGraphique].ToString();
            if (bAccumuler)
            {
                Serie.Name = Serie.Name + "_Accumulation";
            }
            Double j = -1;
            Double dAccumulation = 0;
            Double xValue;
            DateTime dt = DateTime.MinValue;
            ///On ajoute ici les points sur le graphique
            foreach (DataRow ligne in laTable.Rows)
            {
                if ((iIndexColonneX != -1) && (!bString))
                {
                    if (bDouble)
                    {
                        Double.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out j);
                    }
                    else if ((bDate) && (DateTime.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out dt)))
                    {
                        Serie.XValues.Add(dt);
                    }
                }
                else
                {
                    j++;
                }
                if (Double.TryParse(ligne.ItemArray[iIndexColonne].ToString(), out xValue))
                {
                    if (bAccumuler)
                    {
                        dAccumulation += xValue;
                        xValue = Math.Round(dAccumulation, 6);
                        if (Math.Round(xValue) == 100)
                            xValue = 100;
                    }
                    if ((!bDouble) && (!bString))
                    {
                        ((NLineSeries)Serie).Values.Add(xValue);
                    }
                    else
                    {
                        Serie.AddDataPoint(new NDataPoint(j, xValue));
                    }
                }
            }
            /*if (bDate)    // >> Bug #13387 Default Analysis Charts X axis time range C#14
            {
                Serie.XValues.Add(lastValue.AddMilliseconds(1));
                if ((!bDouble) && (!bString))
                {
                    ((NLineSeries)Serie).Values.Add(0);
                }
                else
                {
                    Serie.AddDataPoint(new NDataPoint(j + 1, 0));
                }
                Serie.XValues.Add(lastValue.AddMinutes(15));//Serie.XValues.Add(lastValue.AddMinutes(15));
                if ((!bDouble) && (!bString))
                {
                    ((NLineSeries)Serie).Values.Add(0);
                }
                else
                {
                    Serie.AddDataPoint(new NDataPoint(j + 2, 0));
                }
            }*/
        }
        #endregion

        #region Fonction pour l'ajout sur un Cartesian chart d'un nouveau graphique en mode Area.
        private void CreateGraphicArea(NChartControl ZoneGraphique, NChart Graphique, DataTable laTable,
            int iIndexColonne, int iIndexColonneX, int iIndexGraphique,
            bool bDouble, bool bDate, bool bString, DateTime firstValue, DateTime lastValue,
            bool bAccumulation, bool bAccumuler, bool bStacked)
        {
            if ((bAccumulation) && (!bAccumuler))
            {
                CreateGraphicLine(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique, bDouble, bDate, bString, firstValue, lastValue, bAccumulation, true);
            }
            //La visualisation souhaitée est sur un graphique cartesian
            NAreaSeries Serie;
            Serie = (NAreaSeries)Graphique.Series.Add(SeriesType.Area);
            //Serie.DataLabelStyle.Visible = false;
            if (bStacked)
                Serie.MultiAreaMode = MultiAreaMode.Stacked;

            /*16/08/2010 : ShowValue for the area type
             if (!(Boolean)listShowValues[iIndexGraphique])
            {
                Serie.DataLabelStyle.Visible = false;
            }
             */
            if ((Boolean)listShowValues[iIndexGraphique])
            {
                Serie.DataLabelStyle.Format = "<value>";
            }
            Serie.DataLabelStyle.Visible = (Boolean)listShowValues[iIndexGraphique];
            //}
            Graphique.Series.RemoveAt(Graphique.Series.Count - 1);
            Graphique.Series.Insert(0, Serie);
            // if (!bDouble)
            if (bString)
            {
                Serie.DisplayOnAxis(StandardAxis.SecondaryX, true);
                Serie.DisplayOnAxis(StandardAxis.PrimaryX, false);
            }
            Serie.UseXValues = true;
            if (ZoneGraphique != null)
            {
                ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator).Title.Text = XTitle;
                if ((listAxeRepresentation[iIndexGraphique].ToString() == "Y2") || bAccumuler)
                {
                    Serie.DisplayOnAxis(StandardAxis.PrimaryY, false);
                    Serie.DisplayOnAxis(StandardAxis.SecondaryY, true);
                    Graphique.Axis(StandardAxis.SecondaryY).Visible = true;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Color = Color.Gray;
                    if ((Y2Title == "") && (bAccumuler))
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = "Cumul.%";
                    }
                    else
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = Y2Title;
                    }
                    //((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Pattern = LinePattern.DashDotDot;
                }
                else
                {
                    Serie.DisplayOnAxis(StandardAxis.PrimaryY, true);
                    Serie.DisplayOnAxis(StandardAxis.SecondaryY, false);
                    //((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Color = Color.Black;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryY).ScaleConfigurator).Title.Text = YTitle;
                }
            }
            if (bAccumuler)
            {
                Serie.BorderStyle = new NStrokeStyle((Color)listAccumulation[iIndexGraphique]);
                Serie.BorderStyle.Width = new NLength(1.5f);
                Serie.FillStyle = new NColorFillStyle((Color)listAccumulation[iIndexGraphique]);
            }
            else
            {
                Serie.BorderStyle = getListStrokeCouleurs(iIndexGraphique);
                Serie.BorderStyle.Width = new NLength(1.5f);
                Serie.FillStyle = getListFillCouleurs(iIndexGraphique);
            }
            //NStandardScaleConfigurator scaleConfiguratorX = null;
            if (bDate)
            {
                Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator = setXScale(lastValue.Subtract(firstValue));
                /*//Dans le cas où les abscisses sont représentées par une date.
                NDateTimeScaleConfigurator DateTimeScale = new NDateTimeScaleConfigurator();

                DateTimeScale.LabelStyle.Angle = new NScaleLabelAngle(ScaleLabelAngleMode.UseCustomAngle, 90);
                DateTimeScale.LabelStyle.ContentAlignment = ContentAlignment.MiddleLeft;

                TimeSpan Fenetre = lastValue.Subtract(firstValue);
                if (Fenetre.TotalHours < 48)
                {
                    DateTimeScale.AutoDateTimeUnits = new Nevron.NDateTimeUnit[] { NDateTimeUnit.Hour };
                }
                else
                {
                    DateTimeScale.AutoDateTimeUnits = new Nevron.NDateTimeUnit[] { NDateTimeUnit.Day };
                }
                DateTimeScale.EnableUnitSensitiveFormatting = true;
                DateTimeScale.AutoMinorTicks = true;
                DateTimeScale.MinorTickCount = 1;

                Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator = DateTimeScale;*/
            }
            //On ne veut pas afficher les valeurs pour chaque point.
            /*16/08/2010 : ShowValue for the area type*/
            //Serie.DataLabelStyle.Visible = false;
            Serie.Name = listColumnsNames[iIndexGraphique].ToString();
            if (bAccumuler)
            {
                Serie.Name = Serie.Name + "_Accumulation";
            }
            Double j = -1;
            Double dAccumulation = 0;
            Double xValue;
            DateTime dt = DateTime.MinValue;
            foreach (DataRow ligne in laTable.Rows)
            {
                if ((iIndexColonneX != -1) && (!bString))
                {
                    if (bDouble)
                    {
                        Double.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out j);
                    }
                    else if ((bDate) && (DateTime.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out dt)))
                    {
                        Serie.XValues.Add(dt);
                    }
                }
                else
                {
                    j++;
                }
                if (Double.TryParse(ligne.ItemArray[iIndexColonne].ToString(), out xValue))
                {
                    if (bAccumuler)
                    {
                        dAccumulation += xValue;
                        xValue = Math.Round(dAccumulation, 6);
                        if (Math.Round(xValue) == 100)
                            xValue = 100;
                    }
                    if ((!bDouble) && (!bString))
                    {
                        ((NAreaSeries)Serie).Values.Add(xValue);
                    }
                    else
                    {
                        Serie.AddDataPoint(new NDataPoint(j, xValue));
                    }
                }
            }
            /*if (bDate)    // >> Bug #13387 Default Analysis Charts X axis time range C#14
            {
                Serie.XValues.Add(lastValue.AddMilliseconds(1));
                if (!bDouble)
                {
                    Serie.Values.Add(0);
                }
                else
                {
                    Serie.AddDataPoint(new NDataPoint(j + 1, 0));
                }
                Serie.XValues.Add(lastValue.AddMinutes(15));//Serie.XValues.Add(lastValue.AddMinutes(15));
                if (!bDouble)
                {
                    Serie.Values.Add(0);
                }
                else
                {
                    Serie.AddDataPoint(new NDataPoint(j + 2, 0));
                }
            }*/
        }
        #endregion

        #region Fonction pour l'ajout sur un Cartesian chart d'un nouveau graphique en mode Point.
        private void CreateGraphicCloudPoints(NChartControl ZoneGraphique, NChart Graphique, DataTable laTable,
            int iIndexColonne, int iIndexColonneX, int iIndexGraphique,
            bool bDouble, bool bDate, bool bString, DateTime firstValue, DateTime lastValue,
            bool bAccumulation, bool bAccumuler)
        {
            if ((bAccumulation) && (!bAccumuler))
            {
                CreateGraphicLine(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique, bDouble, bDate, bString, firstValue, lastValue, bAccumulation, true);
            }
            //La visualisation souhaitée est sur un graphique cartesian
            NPointSeries m_Point;
            m_Point = (NPointSeries)Graphique.Series.Add(SeriesType.Point);
            m_Point.DataLabelStyle.Visible = false;
            m_Point.PointShape = PointShape.Sphere;
            m_Point.Size = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            Graphique.Series.RemoveAt(Graphique.Series.Count - 1);
            Graphique.Series.Insert(0, m_Point);
            // if (!bDouble)
            if (bString)
            {
                m_Point.DisplayOnAxis(StandardAxis.SecondaryX, true);
                m_Point.DisplayOnAxis(StandardAxis.PrimaryX, false);
            }
            m_Point.UseXValues = true;
            if (ZoneGraphique != null)
            {
                ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator).Title.Text = XTitle;
                if ((listAxeRepresentation[iIndexGraphique].ToString() == "Y2") || bAccumuler)
                {
                    m_Point.DisplayOnAxis(StandardAxis.PrimaryY, false);
                    m_Point.DisplayOnAxis(StandardAxis.SecondaryY, true);
                    Graphique.Axis(StandardAxis.SecondaryY).Visible = true;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Color = Color.Gray;
                    if ((Y2Title == "") && (bAccumuler))
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = "Cumul.%";
                    }
                    else
                    {
                        ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = Y2Title;
                    }
                    //((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Pattern = LinePattern.DashDotDot;
                }
                else
                {
                    m_Point.DisplayOnAxis(StandardAxis.PrimaryY, true);
                    m_Point.DisplayOnAxis(StandardAxis.SecondaryY, false);
                    //((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Color = Color.Black;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryY).ScaleConfigurator).Title.Text = YTitle;
                }
            }
            if (bAccumuler)
            {
                m_Point.BorderStyle = new NStrokeStyle((Color)listAccumulation[iIndexGraphique]);
                m_Point.BorderStyle.Width = new NLength(1.5f);
                m_Point.FillStyle = new NColorFillStyle((Color)listAccumulation[iIndexGraphique]);
            }
            else
            {
                m_Point.BorderStyle = getListStrokeCouleurs(iIndexGraphique);
                m_Point.BorderStyle.Width = new NLength(1.5f);
                m_Point.FillStyle = new NColorFillStyle(getListStrokeCouleurs(iIndexGraphique).Color);
            }
            //NStandardScaleConfigurator scaleConfiguratorX = null;
            if (bDate)
            {
                Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator = setXScale(lastValue.Subtract(firstValue));
            }
            //On ne veut pas afficher les valeurs pour chaque point.
            m_Point.DataLabelStyle.Visible = false;
            m_Point.Name = listColumnsNames[iIndexGraphique].ToString();
            if (bAccumuler)
            {
                m_Point.Name = m_Point.Name + "_Accumulation";
            }
            Double j = -1;
            Double dAccumulation = 0;
            Double xValue;
            DateTime dt = DateTime.MinValue;
            foreach (DataRow ligne in laTable.Rows)
            {
                bool xValueToBeAdded = false;
                if ((iIndexColonneX != -1) && (!bString))
                {
                    if (bDouble)
                    {
                        Double.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out j);
                    }
                    else if ((bDate) && (DateTime.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out dt)))
                    {
//                        m_Point.XValues.Add(dt);
                        xValueToBeAdded = true;
                    }/*
                    else
                    {
                        scaleConfiguratorX.Labels.Add(ligne.ItemArray[iIndexColonneX].ToString());
                    }*/
                }
                else
                {
                    j++;
                }
                bool yValueToBeAdded = false;
                if (Double.TryParse(ligne.ItemArray[iIndexColonne].ToString(), out xValue))
                {

                    if (bAccumuler)
                    {
                        dAccumulation += xValue;
                        xValue = Math.Round(dAccumulation, 6);
                        if (Math.Round(xValue) == 100)
                            xValue = 100;
                    }
                    if ((!bDouble) && (!bString))
                    {
                        yValueToBeAdded = true;
//                        ((NPointSeries)m_Point).Values.Add(xValue);
                    }
                    else
                    {
                        m_Point.AddDataPoint(new NDataPoint(j, xValue));
                    }
                }
                if (xValueToBeAdded && yValueToBeAdded) // >> Bug - IST chart
                {
                    m_Point.XValues.Add(dt);
                    ((NPointSeries)m_Point).Values.Add(xValue);
                }
            }
            /*if (bDate)    // >> Bug #13387 Default Analysis Charts X axis time range C#14
            {
                m_Point.XValues.Add(lastValue.AddMilliseconds(1));
                if (!bDouble)
                {
                    m_Point.Values.Add(0);
                }
                else
                {
                    m_Point.AddDataPoint(new NDataPoint(j + 1, 0));
                }
                m_Point.XValues.Add(lastValue.AddMinutes(15));//m_Point.XValues.Add(lastValue.AddMinutes(15));
                if (!bDouble)
                {
                    m_Point.Values.Add(0);
                }
                else
                {
                    m_Point.AddDataPoint(new NDataPoint(j + 2, 0));
                }
            }*/
        }
                #endregion

        #region Fonction pour l'ajout sur un Cartesian chart d'un nouveau graphique en mode Candle.
        /// <summary>
        /// Fonction permettant la créaction de candles simples ou candleLists
        /// </summary>
        /// <param name="ZoneGraphique">Zone du graphique</param>
        /// <param name="Graphique">Graphique sur lequel on va ajouter le candle</param>
        /// <param name="laTable">table contenant les données</param>
        /// <param name="iIndexColonne">index de colonne</param>
        /// <param name="iIndexColonneX"></param>
        /// <param name="iIndexGraphique">index du candle à afficher</param>
        /// <param name="bDouble">Vrai si l'axe sur lequel on affiche le candle est de type double</param>
        /// <param name="bDate">Vrai si l'axe sur lequel on affiche le candle est de type date</param>
        /// <param name="bString">Vrai si l'axe sur lequel on affiche le candle est de type string</param>
        /// <param name="firstValue">premiere valeur en X</param>
        /// <param name="lastValue">Derniere valeur en X</param>
        /// <param name="MaxValue">Valeur Max du candle</param>
        /// <param name="MinValue">Valeur Min du candle</param>
        /// <param name="MiddleValue">Valeur Middle du candle</param>
        /// <param name="isCandleList">Variable booleene précisant si le candle à créer fait parti d'une candle list</param>
        private void CreateGraphicCandle(NChartControl ZoneGraphique, NChart Graphique, DataTable laTable,
            int iIndexColonne, int iIndexColonneX, int iIndexGraphique,
            bool bDouble, bool bDate, bool bString, DateTime firstValue, DateTime lastValue,
            double MaxValue, double MinValue, double MiddleValue, Boolean isCandleList)
        {
            int lastCandleColumn = 0;   // 0 if there's no Candle curve to display after the current one, 1 otherwise
            DateTime dt;

            NErrorBarSeries m_Candle;

            ///Si l'axe des abscisses est de type date
            if (bDate)
            {
                ///On ajoute Le candle
                m_Candle = (NErrorBarSeries)Graphique.Series.Add(SeriesType.ErrorBar);
                //On fait correspondre à un candle un point en X.
                m_Candle.UseXValues = true;
                Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator = setXScale(lastValue.Subtract(firstValue));
                ///On ajoute la donnée en x qui correspondra aux données du candle que l'on va ajouter plus bas
                if (DateTime.TryParse(laTable.Rows[iIndexColonne][iIndexColonneX].ToString(), out dt))
                {
                    m_Candle.XValues.Add(dt);
                }
                Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator = setXScale(lastValue.Subtract(firstValue));
            }
            else
            {
                ///Si on est à une nouvelle position, alors on créé un nouveau candle
                if (m_cCandle == null)
                    m_Candle = (NErrorBarSeries)Graphique.Series.Add(SeriesType.ErrorBar);
                else m_Candle = m_cCandle;
                m_Candle.UseXValues = false;
                ///Si le candle n'est pas une candle list, alors on créé un deuxieme axe x pour afficher la legende des candles affichés
                if (!isCandleList)
                {
                    NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryX).ScaleConfigurator;
                    ///On n'utilise pas de labels automatique
                    scaleConfiguratorX.AutoLabels = false;

                    ///On ajoute en dessous du candle créé son nom
                    if(listColumnsNames[iIndexGraphique].ToString().Length > 23)
                        scaleConfiguratorX.Labels.Add(listColumnsNames[iIndexGraphique].ToString().Remove(10) + "..." + listColumnsNames[iIndexGraphique].ToString().Substring(listColumnsNames[iIndexGraphique].ToString().Length - 10, 10));
                    else
                        scaleConfiguratorX.Labels.Add(listColumnsNames[iIndexGraphique].ToString());

                    //Add blank label after the current one added if there's no candle curve to display anymore
                    for (int i = iIndexGraphique + 1; i < listVisualisation.Count; i++)
                        ///Si il y a parmis les autres courbes restantes à afficher un autre candle
                        if (listVisualisation[i].ToString().CompareTo("Candle") == 0)
                            lastCandleColumn = 1;
                    ///Si il n'y a plus de candle à afficher apres celui que l'on créé alors on n'affiche plus de labels en dessous de l'axe
                    if (lastCandleColumn == 0)
                        for (int i = iIndexGraphique; i < scaleConfiguratorX.MinTickDistance.Value; i++)
                            scaleConfiguratorX.Labels.Add("");

                    scaleConfiguratorX.LabelStyle.Angle = new NScaleLabelAngle(ScaleLabelAngleMode.View, 45);
                    ///On créé un titre pour le deuxieme abscisse X ajouté
                    NScaleTitleStyle AxisTitle = new NScaleTitleStyle();
                    AxisTitle.Text = "Candle Axis";
                    ///Couleur de l'abscisse rouge
                    AxisTitle.TextStyle.FillStyle = new NColorFillStyle(Color.Red);
                    ///On affiche le titre au milieu de l'axe.
                    AxisTitle.RulerAlignment = HorzAlign.Center;
                    scaleConfiguratorX.Title = AxisTitle;
                    ///On donne la couleur rouge pour les ticks superieurs et inferieurs de l'axe
                    scaleConfiguratorX.OuterMajorTickStyle.LineStyle.Color = Color.Red;
                    scaleConfiguratorX.InnerMajorTickStyle.LineStyle.Color = Color.Red;

                    scaleConfiguratorX.RulerStyle.BorderStyle.Color = Color.Red;
                    
                    ///On affiche l'axe secondaire
                    Graphique.Axis(StandardAxis.SecondaryX).Visible = true;
                    Graphique.Axis(StandardAxis.SecondaryX).Anchor = new NDockAxisAnchor(AxisDockZone.FrontBottom);
                }
            }
            // Candle Definition////////////////////////////////////////////////////
            //On n'affiche pas la valeur des candles
            m_Candle.DataLabelStyle.Visible = false;
            m_Candle.MarkerStyle.Visible = true;
            m_Candle.MarkerStyle.AutoDepth = false;
            ///On définit ici la largeur et hauteur du candle
            m_Candle.MarkerStyle.Width = new NLength(1.2f, NRelativeUnit.ParentPercentage);
            m_Candle.MarkerStyle.Height = new NLength(1.2f, NRelativeUnit.ParentPercentage);
            m_Candle.MarkerStyle.Depth = new NLength(1.2f, NRelativeUnit.ParentPercentage);

            ///On définit les trois valeurs du candle à savoir la valeur Min, Max, et Middle
            ((NErrorBarSeries)m_Candle).Values.Add(MiddleValue);
            m_Candle.LowerErrorsY.Add(MiddleValue - MinValue);
            m_Candle.UpperErrorsY.Add(MaxValue - MiddleValue);
            ///Si le candle est affiché avec des valeurs de type date alors on l'affiche sur le premier axe X sinon le deuxieme
            if (bDate)
                m_Candle.DisplayOnAxis(StandardAxis.PrimaryX, true);
            else m_Candle.DisplayOnAxis(StandardAxis.SecondaryX, true);
            /////////////////////////////////////////////////////////////////////////

            if (ZoneGraphique != null)
            {
                ///Si le candle doit etre affiché sur L'axe Y2
                if (listAxeRepresentation[iIndexGraphique].ToString() == "Y2")
                {
                    m_Candle.DisplayOnAxis(StandardAxis.PrimaryY, false);
                    m_Candle.DisplayOnAxis(StandardAxis.SecondaryY, true);
                    Graphique.Axis(StandardAxis.SecondaryY).Visible = true;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Color = Color.Gray;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = Y2Title;
                }
                ///Sinon si on l'affiche sur l'axe Y
                else
                {
                    m_Candle.DisplayOnAxis(StandardAxis.PrimaryY, true);
                    m_Candle.DisplayOnAxis(StandardAxis.SecondaryY, false);
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryY).ScaleConfigurator).Title.Text = YTitle;
                }
            }
            m_cCandle = m_Candle;
        }
        #endregion




        #region Fonction pour l'ajout sur un Cartesian chart d'un nouveau graphique en mode histogramme.

        private void CreateGraphicBar(NChartControl ZoneGraphique,
                                      NChart Graphique,
                                      DataTable laTable,
                                      int iIndexColonne,
                                      int iIndexColonneX,
                                      int iIndexGraphique,
                                      int iNbBar,
                                      bool bDouble,
                                      bool bDate,
                                      bool bString,
                                      DateTime firstValue,
                                      DateTime lastValue,
                                      bool bAccumulation,
                                      bool bStacked)
        {

            if (bAccumulation )
            {
                CreateGraphicLine(ZoneGraphique, Graphique, laTable, iIndexColonne, iIndexColonneX, iIndexGraphique, bDouble, bDate, bString, firstValue, lastValue, bAccumulation, true);
            }
            //La visualisation souhaitée est sur un graphique cartesian
            NBarSeries Serie;
            Serie = (NBarSeries)Graphique.Series.Add(SeriesType.Bar);
            if (bStacked)
                Serie.MultiBarMode = MultiBarMode.Stacked;
            else
                Serie.MultiBarMode = MultiBarMode.Clustered;

            if (!(Boolean)listShowValues[iIndexGraphique])
            {
                Serie.DataLabelStyle.Visible = false;
            }
            
            if ((!bString) && (iIndexColonneX != -1))
            {
                float barWidth = 0.0f;
                if (iIndexGraphique < listPositions.Count && (int)listPositions[iIndexGraphique] < lesPositionsBarWidth.Count)
                    barWidth = lesPositionsBarWidth[(int)listPositions[iIndexGraphique]];
                Serie.UseXValues = true;
                if ((iNbBar != 0) && (laTable.Rows.Count != 0))
                {
                    if (barWidth > 0)
                        Serie.BarWidth = new NLength(barWidth, NRelativeUnit.ParentPercentage);
                    else
                    {
                        Serie.BarWidth = new NLength(100.0f / (float)(laTable.Rows.Count * (iNbBar)), NRelativeUnit.ParentPercentage);
                        if (iIndexGraphique < listPositions.Count && (int)listPositions[iIndexGraphique] < lesPositionsBarWidth.Count)
                            lesPositionsBarWidth[(int)listPositions[iIndexGraphique]] = (float)Math.Round(100.0f / (float)(laTable.Rows.Count * (iNbBar)), 2);
                    }
                }

                float gapPerc = 0.0f;
                if (iIndexGraphique < listPositions.Count && (int)listPositions[iIndexGraphique] < lesPositionsGapPercent.Count)
                    gapPerc = lesPositionsGapPercent[(int)listPositions[iIndexGraphique]];
                if (gapPerc > 0)
                    Serie.GapPercent = gapPerc;
                else
                {
                    Serie.GapPercent = 20;
                    if (iIndexGraphique < listPositions.Count && (int)listPositions[iIndexGraphique] < lesPositionsGapPercent.Count)
                        lesPositionsGapPercent[(int)listPositions[iIndexGraphique]] = 20.0f;
                }
            }
            else
            {
                float barWidthPerc = 0.0f;
                if (iIndexGraphique < listPositions.Count && (int)listPositions[iIndexGraphique] < lesPositionsBarWidthPercent.Count)
                    barWidthPerc = lesPositionsBarWidthPercent[(int)listPositions[iIndexGraphique]];
                if (barWidthPerc > 0)
                    Serie.WidthPercent = barWidthPerc;
                else
                {
                    Serie.WidthPercent = 90.0f;
                    if (iIndexGraphique < listPositions.Count && (int)listPositions[iIndexGraphique] < lesPositionsBarWidthPercent.Count)
                        lesPositionsBarWidthPercent[(int)listPositions[iIndexGraphique]] = 90.0f;
                }
            }


            ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator).Title.Text = XTitle;
            if (ZoneGraphique != null)
            {
                if (listAxeRepresentation[iIndexGraphique].ToString() == "Y2")
                {
                    Serie.DisplayOnAxis(StandardAxis.PrimaryY, false);
                    Serie.DisplayOnAxis(StandardAxis.SecondaryY, true);
                    Graphique.Axis(StandardAxis.SecondaryY).Visible = true;
                    //((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Pattern = LinePattern.DashDotDot;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).MajorGridStyle.LineStyle.Color = Color.Gray;
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.SecondaryY).ScaleConfigurator).Title.Text = Y2Title;
                }
                else
                {
                    Serie.DisplayOnAxis(StandardAxis.PrimaryY, true);
                    Serie.DisplayOnAxis(StandardAxis.SecondaryY, false);
                    ((NStandardScaleConfigurator)Graphique.Axis(StandardAxis.PrimaryY).ScaleConfigurator).Title.Text = YTitle;
                }
            }
            Serie.BorderStyle = getListStrokeCouleurs(iIndexGraphique);
            Serie.FillStyle = getListFillCouleurs(iIndexGraphique);

            if (bDate)
            {
                Graphique.Axis(StandardAxis.PrimaryX).ScaleConfigurator = setXScale(lastValue.Subtract(firstValue));
            }
            //On ne veut pas afficher les valeurs pour chaque point.
            Serie.Name = listColumnsNames[iIndexGraphique].ToString();
            Double j = 0;
            Double dMinValueJ = 0;
            Double xValue;
            DateTime dt = DateTime.MinValue;
            bool dateAddedOnX = false;
            foreach (DataRow ligne in laTable.Rows)
            {
                if (iIndexColonneX != -1)
                {
                    if (bDouble)
                    {
                        Double.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out j);
                    }
                    else if ((bDate) && (DateTime.TryParse(ligne.ItemArray[iIndexColonneX].ToString(), out dt)))
                    {
                        Serie.XValues.Add(dt);
                        dateAddedOnX = true;
                    }/*
                    else
                    {
                        //Serie.XValues.Add();
                        scaleConfiguratorX.Labels.Add(ligne.ItemArray[iIndexColonneX].ToString());
                    }*/
                }
                else
                {
                    j++;
                }
                if (j < dMinValueJ)
                    dMinValueJ = j;
                if (Double.TryParse(ligne.ItemArray[iIndexColonne].ToString(), out xValue))
                {
                    if (!bDouble)
                    {
                        Serie.Values.Add(xValue);
                    }
                    else
                    {
                        Serie.AddDataPoint(new NDataPoint(j, xValue));
                    }
                }
            }
            //Correction d'un bug décallant l'affichage de l'axe des abscisses lorsque les valeurs sont négatives.
            if (dMinValueJ < 0)
            {
                Serie.AddDataPoint(new NDataPoint(dMinValueJ-1, 0));
            }

            /*if (bDate)    // >> Bug #13387 Default Analysis Charts X axis time range C#14
            {
                Serie.XValues.Add(lastValue.AddMilliseconds(1));
                if (!bDouble)
                {
                    Serie.Values.Add(0);
                }
                else
                {
                    Serie.AddDataPoint(new NDataPoint(j + 1, 0));
                }
                Serie.XValues.Add(lastValue.AddMinutes(15));//Serie.XValues.Add(lastValue.AddMinutes(15));
                if (!bDouble)
                {
                    Serie.Values.Add(0);
                }
                else
                {
                    Serie.AddDataPoint(new NDataPoint(j + 2, 0));
                }
            }*/
        }
        #endregion

        #region Pour la création du Pie Chart
        private NChart CreateGraphicPie(DataTable laTable, int iIndexColonne, int iIndexColonneX/*, int iIndexGraphique*/)
        {
            NChart chart = new NPieChart();

            chart.Tag = "NPieChart";

            NPieSeries m_Pie = (NPieSeries)chart.Series.Add(SeriesType.Pie);
            m_Pie.Values.Clear();
            foreach (DataRow ligne in laTable.Rows)
            {
                if (iIndexColonneX == -1)
                {
                    m_Pie.AddDataPoint(new NDataPoint(FonctionsType.getDouble(ligne.ItemArray[iIndexColonne], typeof(String))));
                }
                else
                {
                    m_Pie.AddDataPoint(new NDataPoint(FonctionsType.getDouble(ligne.ItemArray[iIndexColonne], typeof(String)), ligne.ItemArray[iIndexColonneX].ToString()));
                }
                m_Pie.Detachments.Add(0);
            }
            for (int y = 0; y < m_Pie.Values.Count; y++)
            {
                m_Pie.FillStyles[y] = new NColorFillStyle(PAX2SIM.sequenceCouleurs[y % PAX2SIM.sequenceCouleurs.Length]);
            }
            return chart;
        }
        #endregion

        #region Fonction pour savoir le nombre de graphique qu'il va y avoir.
        /// <summary>
        /// Fonction qui renvoit le nombre de graphique d'afficher dans la zone de 0 à 4.
        /// </summary>
        /// <param name="listPositions">La liste des positions</param>
        /// <returns>Retourne le nombre de graphique différents affichés dans la zone de graphique.</returns>
        private static int getNumberOfChart(ArrayList listPositions)
        {
            bool[] find = new bool[4] { false, false, false, false };
            int Number = 0;
            foreach (int value in listPositions)
            {
                if ((value >= 0) && (value < 4) && (!find[value]))
                {
                    find[value] = true;
                    Number++;
                }
            }
            return Number;
        }

        /// <summary>
        /// Fonction qui renvoit le nombre de graphique afficher dans la partie haute de la zone graphique (0 à 2)
        /// </summary>
        /// <param name="listPositions">La liste des positions</param>
        /// <returns>Retourne le nombre de graphique différents affichés dans la partie haute de la zone de graphique.</returns>
        private static int getNumberOfChartOnTop(ArrayList listPositions)
        {
            bool[] find = new bool[4] { false, false, false, false };
            int Number = 0;
            foreach (int value in listPositions)
            {
                if ((value >= 0) && (value < 2) && (!find[value]))
                {
                    find[value] = true;
                    Number++;
                }
            }
            return Number;
        }

        /// <summary>
        /// Fonction qui se charge de définir la position du graphique dans la zone de graphique. Elle se base sur 
        /// le nombre de graphique affiché et sur le nombre de graphique affichés dans la partie haute de la zone.
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="NumberOfGraphic"></param>
        /// <param name="NumberOfGraphiqueOnTop"></param>
        /// <param name="pPosition"></param>
        /// <param name="pTaille"></param>
        private static void getPosition(int Position, int NumberOfGraphic, int NumberOfGraphiqueOnTop, bool bTitle, out Point pPosition, out Point pTaille)
        {
            int iPositionH = 5;
            int iPositionHalfH = 55;
            int iHauteur = 90;
            int iHauterHalf = 45;
            if (bTitle)
            {
                iPositionH = 10;
                iHauteur = 85;
                iHauterHalf = 42;
                iPositionHalfH = 58;
            }

            switch (NumberOfGraphic)
            {
                case 2:
                    if ((NumberOfGraphiqueOnTop == 0) || (NumberOfGraphiqueOnTop == 2))
                    {
                        if ((Position % 2) == 0)
                        {
                            pPosition = new Point(5, iPositionH);
                        }
                        else
                        {
                            pPosition = new Point(53, iPositionH);
                        }
                        pTaille = new Point(42, iHauteur);
                        return;
                    }
                    else
                    {
                        if (Position < 2)
                        {
                            pPosition = new Point(5, iPositionH);
                        }
                        else
                        {
                            pPosition = new Point(5, iPositionHalfH);
                        }
                        pTaille = new Point(90, iHauterHalf);
                        return;
                    }
                case 3:
                    if (NumberOfGraphiqueOnTop == 1)
                    {
                        if (Position < 2)
                        {
                            pPosition = new Point(5, iPositionH);
                            pTaille = new Point(90, iHauterHalf);
                            return;
                        }
                        else
                        {
                            if (Position == 2)
                            {
                                pPosition = new Point(5, iPositionHalfH);
                            }
                            else
                            {
                                pPosition = new Point(53, iPositionHalfH);
                            }
                            pTaille = new Point(42, iHauterHalf);
                            return;
                        }
                    }
                    else
                    {
                        if (Position < 2)
                        {
                            if (Position == 0)
                            {
                                pPosition = new Point(5, iPositionH);
                            }
                            else
                            {
                                pPosition = new Point(53, iPositionH);
                            }

                            pTaille = new Point(42, iHauterHalf);
                            return;
                        }
                        else
                        {
                            pPosition = new Point(5, iPositionHalfH);
                            pTaille = new Point(90, iHauterHalf);
                            return;
                        }
                    }
                case 4:
                    pTaille = new Point(42, iHauterHalf);
                    switch (Position)
                    {
                        case 0: pPosition = new Point(5, iPositionH);
                            return;
                        case 1: pPosition = new Point(53, iPositionH);
                            return;
                        case 2: pPosition = new Point(5, iPositionHalfH);
                            return;
                        case 3: pPosition = new Point(53, iPositionHalfH);
                            return;
                        default: pPosition = new Point(5, iPositionH);
                            pTaille = new Point(90, iHauteur);
                            return;
                    }
                default:
                    pPosition = new Point(5, iPositionH);
                    pTaille = new Point(90, iHauteur);
                    return;
            }
        }

        #endregion

        #endregion

        #region Fonction mettre en place les annotations

        //Add a new annotation
        internal void SetAnnotation(NChartControl Graphique, NHitTestResult hitTestResult)
        {
            NRoundedRectangularCallout m_RoundedRectangularCallout;
            SIMCORE_TOOL.Prompt.SIM_Annotation annot = new SIMCORE_TOOL.Prompt.SIM_Annotation("");
            Notes note = new Notes();

            m_RoundedRectangularCallout = new NRoundedRectangularCallout();
            m_RoundedRectangularCallout.ArrowLength = new NLength(10, NRelativeUnit.ParentPercentage);  //  Arrow lenght
            m_RoundedRectangularCallout.FillStyle = new NGradientFillStyle(Color.FromArgb(125, Color.White), Color.FromArgb(125, Color.LightGreen));
            m_RoundedRectangularCallout.UseAutomaticSize = true;
            m_RoundedRectangularCallout.Orientation = 25;   // Arrow orientation

            switch (hitTestResult.ChartElement)
            {
                //Click on the graphic but not on a curve
                case ChartElement.ControlBackground:
                    break;
                //Click on a series graphic curve
                case ChartElement.DataPoint:
                    if (hitTestResult.Series == null)
                        return;
                    m_RoundedRectangularCallout.Anchor = new NDataPointAnchor(hitTestResult.Series, hitTestResult.DataPointIndex, ContentAlignment.MiddleCenter, StringAlignment.Center);
                    if (annot.ShowDialog() == DialogResult.OK)
                    {
                        m_RoundedRectangularCallout.Text = annot.Value;
                        note.objectId = hitTestResult.Series.Name;
                        note.text = annot.Value;
                        note.dataPointIndex = hitTestResult.DataPointIndex;
                        Graphique.Panels.Add(m_RoundedRectangularCallout);
                        ListAnnotation.Add(note);
                        //Set the tag of the annotation that will be usefull for comparison
                        m_RoundedRectangularCallout.Tag = m_RoundedRectangularCallout.Id.ToString();
                        note.annotationId = m_RoundedRectangularCallout.Id.ToString();
                    }
                    break;
            }
        }

        /// <summary>
        /// Affiche les annotations présentes dans le graphique
        /// </summary>
        /// <param name="Graphique">Graphique sur lequel nous travaillons</param>
        internal void DisplayAnnotation(NChartControl Graphique)
        {
            // check if there are objects that hold Annotations in the Root Panel and remove them
            if (Graphique.Panels.Count > 0)
            {
                ArrayList panelsToBeRemoved = new ArrayList();
                for (int k = 0; k < Graphique.Panels.Count; k++)
                {
                    if (Graphique.Panels[k].GetType().IsInstanceOfType(new NRoundedRectangularCallout()))
                    {
                        NRoundedRectangularCallout annotationObject = (NRoundedRectangularCallout)Graphique.Panels[k];
                        panelsToBeRemoved.Add(annotationObject);
                    }
                }
                for (int j = 0; j < panelsToBeRemoved.Count; j++)
                    Graphique.Panels.Remove(panelsToBeRemoved[j]);
            }

            NRoundedRectangularCallout m_RoundedRectangularCallout;
            // recreate the objects for the Annotations found in this Graphic's list of annotations
            for (int i = 0; i < ListAnnotation.Count; i++)
            {

                m_RoundedRectangularCallout = new NRoundedRectangularCallout();
                m_RoundedRectangularCallout.ArrowLength = new NLength(10, NRelativeUnit.ParentPercentage);
                m_RoundedRectangularCallout.FillStyle = new NGradientFillStyle(Color.FromArgb(125, Color.White), Color.FromArgb(125, Color.LightGreen));
                m_RoundedRectangularCallout.UseAutomaticSize = true;
                m_RoundedRectangularCallout.Orientation = 25;

                for (int j = 0; j < Graphique.Charts[0].Series.Count; j++)
                {
                    //Add the annotation with its correct fields
                    if (Graphique.Charts[0].Series[j].Name == ListAnnotation[i].objectId)
                        m_RoundedRectangularCallout.Anchor = new NDataPointAnchor((NSeriesBase)Graphique.Charts[0].Series[j], ListAnnotation[i].dataPointIndex, ContentAlignment.MiddleCenter, StringAlignment.Center);
                }

                m_RoundedRectangularCallout.Text = ListAnnotation[i].text;
                Graphique.Panels.Add(m_RoundedRectangularCallout);
                //The tag would help us to identify the annotations when deleting
                if (ListAnnotation[i].annotationId == null || ListAnnotation[i].annotationId == "-1")
                    m_RoundedRectangularCallout.Tag = ListAnnotation[i].annotationId = m_RoundedRectangularCallout.Id.ToString();
                else
                    m_RoundedRectangularCallout.Tag = ListAnnotation[i].annotationId;

            }
        }

        /// <summary>
        /// Cette fonction nous permet de supprimer une annotation précédemment ajoutée
        /// </summary>
        /// <param name="Graphique">Graphique sur lequel nous travaillons</param>
        /// <param name="hitTestResult">parametres de clique</param>
        internal void SuppressAnnotation(NChartControl Graphique, NHitTestResult hitTestResult)
        {
            ///regarder le type d'objet sur lequel on à cliqué
            switch (hitTestResult.ChartElement)
            {
                ///Si on à cliqué sur une annotation
                case ChartElement.Annotation:
                    //compare the object id passed in parameter to all the annotations
                    for (int i = 0; i < ListAnnotation.Count; i++)
                    {
                        String id = ((NRoundedRectangularCallout)hitTestResult.Object).Tag.ToString();
                        //If the annotation id correspond to the object passed in parameter, delete it
                        if (id == ListAnnotation[i].annotationId)
                        {   // remove the Annotation from the Graphic's list of annotations 
                            ListAnnotation.RemoveAt(i);
                            // remove the object that holds the Annotation from the Root Panel
                            NRoundedRectangularCallout suppressedAnnotation = (NRoundedRectangularCallout)hitTestResult.Object;
                            NPanel parentPanel = suppressedAnnotation.ParentPanel;
                            parentPanel.Document.RootPanel.Panels.Remove(suppressedAnnotation);
                        }
                        Graphique.Update();
                    }
                    break;
                default:
                    break;
            }

        }
        #endregion

    }
}