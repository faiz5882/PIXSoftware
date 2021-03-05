using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SIMCORE_TOOL.com.crispico.gantt;

namespace SIMCORE_TOOL.Classes
{
    internal class TreeViewTag
    {
        #region Definition of the images index for the treeview.
        public const int IndexDirectory = 0;
        const int AirportIndexImages = 1;

        public const int IndexAirportImage = AirportIndexImages + 0;
        public const int IndexTerminalImage = AirportIndexImages + 1;
        public const int IndexLevelImage = AirportIndexImages + 2;
        public const int IndexCheckInImage = AirportIndexImages + 3;
        public const int IndexPassportImage = AirportIndexImages + 4;
        public const int IndexSecurityImage = AirportIndexImages + 5;
        public const int IndexArrivalImage = AirportIndexImages + 6;
        public const int IndexBaggageImage = AirportIndexImages + 7;
        public const int IndexTransferImage = AirportIndexImages + 8;
        public const int IndexShuttleImage = AirportIndexImages + 9;
        public const int IndexBoardingImage = AirportIndexImages + 10;
        public const int IndexInImage = AirportIndexImages + 11;
        public const int IndexOutImage = AirportIndexImages + 12;
        public const int IndexShoppingImage = AirportIndexImages + 13;

        //BHS
        public const int IndexBHSImage = AirportIndexImages + 14;
        public const int IndexCI_CollectorImage = AirportIndexImages + 15;
        public const int IndexTransferInfeedImage = AirportIndexImages + 16;
        public const int IndexHBS1Image = AirportIndexImages + 17;
        public const int IndexHBS3Image = AirportIndexImages + 18;
        public const int IndexHBS5Image = AirportIndexImages + 19;
        public const int IndexMESImage = AirportIndexImages + 20;
        public const int IndexEBSImage = AirportIndexImages + 21;
        public const int IndexMakeUpImage = AirportIndexImages + 22;
        public const int IndexHBSCustomImage = AirportIndexImages + 23;
        public const int IndexArrivalInfeedImage = AirportIndexImages + 24;


        public const int IndexAircraftParkingImage = IndexArrivalInfeedImage + 1;
        public const int IndexRunwayImage = IndexArrivalInfeedImage + 2;



        public const int ChartImageIndex = IndexRunwayImage + 1;
        public const int ParagraphImageIndex = IndexRunwayImage + 2;
        public const int BlockedParagraphImageIndex = IndexRunwayImage + 3;



        public const int TableImageIndex = BlockedParagraphImageIndex + 1;
        public const int TableExImageIndex = TableImageIndex + 1;
        public const int TableNoteImageIndex = TableImageIndex + 2;
        public const int TableExNoteImageIndex = TableImageIndex + 3;

        public const int SelectedTableImageIndex = TableImageIndex + 4;
        public const int SelectedTableExImageIndex = TableImageIndex + 5;
        public const int SelectedTableNoteImageIndex = TableImageIndex + 6;
        public const int SelectedTableExNoteImageIndex = TableImageIndex + 7;

        public const int FilterImageIndex = SelectedTableExNoteImageIndex + 1;
        public const int FilterExImageIndex = FilterImageIndex + 1;
        public const int FilterNoteImageIndex = FilterImageIndex + 2;
        public const int FilterExNoteImageIndex = FilterImageIndex + 3;

        public const int SelectedFilterImageIndex = FilterImageIndex + 4;
        public const int SelectedFilterExImageIndex = FilterImageIndex + 5;
        public const int SelectedFilterNoteImageIndex = FilterImageIndex + 6;
        public const int SelectedFilterExNoteImageIndex = FilterImageIndex + 7;

        public const int FilterBloquedImageIndex = SelectedFilterExNoteImageIndex + 1;
        public const int FilterBloquedExImageIndex = FilterBloquedImageIndex + 1;
        public const int FilterBloquedNoteImageIndex = FilterBloquedImageIndex + 2;
        public const int FilterBloquedExNoteImageIndex = FilterBloquedImageIndex + 3;

        public const int SelectedFilterBloquedImageIndex = FilterBloquedImageIndex + 4;
        public const int SelectedFilterBloquedExImageIndex = FilterBloquedImageIndex + 5;
        public const int SelectedFilterBloquedNoteImageIndex = FilterBloquedImageIndex + 6;
        public const int SelectedFilterBloquedExNoteImageIndex = FilterBloquedImageIndex + 7;
        public static Int32[] liNormalTables = { TableImageIndex, FilterImageIndex, FilterBloquedImageIndex };
        public static Int32[] liSelectedTables = { SelectedTableImageIndex, SelectedFilterImageIndex, SelectedFilterBloquedImageIndex };


        const int ResultNodeIndexImages = SelectedFilterBloquedExNoteImageIndex + 1;
        public const int ResultNodeIndexAirportImage = ResultNodeIndexImages + 0;
        public const int ResultNodeIndexTerminalImage = ResultNodeIndexImages + 1;
        public const int ResultNodeIndexLevelImage = ResultNodeIndexImages + 2;
        public const int ResultNodeIndexCheckInImage = ResultNodeIndexImages + 3;
        public const int ResultNodeIndexPassportImage = ResultNodeIndexImages + 4;
        public const int ResultNodeIndexSecurityImage = ResultNodeIndexImages + 5;
        public const int ResultNodeIndexArrivalImage = ResultNodeIndexImages + 6;
        public const int ResultNodeIndexBaggageImage = ResultNodeIndexImages + 7;
        public const int ResultNodeIndexTransferImage = ResultNodeIndexImages + 8;
        public const int ResultNodeIndexShuttleImage = ResultNodeIndexImages + 9;
        public const int ResultNodeIndexBoardingImage = ResultNodeIndexImages + 10;
        public const int ResultNodeIndexInImage = ResultNodeIndexImages + 11;
        public const int ResultNodeIndexOutImage = ResultNodeIndexImages + 12;
        public const int ResultNodeIndexShoppingImage = ResultNodeIndexImages + 13;
        //BHS
        public const int ResultNodeIndexBHSImage = ResultNodeIndexImages + 11;
        public const int ResultNodeIndexCI_CollectorImage = ResultNodeIndexImages + 12;
        public const int ResultNodeIndexTransferInfeedImage = ResultNodeIndexImages + 13;
        public const int ResultNodeIndexHBS1Image = ResultNodeIndexImages + 14;
        public const int ResultNodeIndexHBS3Image = ResultNodeIndexImages + 15;
        public const int ResultNodeIndexHBS5Image = ResultNodeIndexImages + 16;
        public const int ResultNodeIndexMESImage = ResultNodeIndexImages + 17;
        public const int ResultNodeIndexEBSImage = ResultNodeIndexImages + 18;
        public const int ResultNodeIndexMakeUp5Image = ResultNodeIndexImages + 19;
        public const int ResultNodeIndexHBSCustomImage = ResultNodeIndexImages + 20;
        public const int ResultNodeIndexArrivalInfeedImage = ResultNodeIndexImages + 21;

        #endregion

        #region La gestion du type représenté par le tag.
        /// <summary>
        /// Enumération des différents type que peut prendre le tag du treeview.
        /// </summary>
        public enum TreeViewTypeTag
        {
            /// <summary>
            /// Le type représentant la structure de l'aéroport.
            /// </summary>
            AirportNode,
            /// <summary>
            /// Le type représentant une table.
            /// </summary>
            TableNode,
            /// <summary>
            /// Le type représentant le filtre.
            /// </summary>
            FilterNode,
            /// <summary>
            /// Le type représentant les graphiques.
            /// </summary>
            ChartNode,
            /// <summary>
            /// Le type représentant la structure de résultat qui n'est pas un dossier.
            /// </summary>
            ResultNode,
            /// <summary>
            /// Le type représentant un dossier.
            /// </summary>
            DirectoryNode,
            /// <summary>
            /// Text au format HTML.
            /// </summary>
            ParagraphNode,
            /// <summary>
            /// Table d'exception concernant les catégories de vol.
            /// </summary>
            ExceptionTable,
            /// <summary>
            /// Table d'exception concernant les compagnies aériennes.
            /// </summary>
            ExceptionFilter
        };

        TreeViewTypeTag classType;

        public TreeViewTypeTag TypeClass
        {
            get
            {
                return classType;
            }
        }
        public Boolean isAirportNode
        {
            get
            {
                return classType == TreeViewTypeTag.AirportNode;
            }
        }

        public Boolean isTableNode
        {
            get
            {
                return classType == TreeViewTypeTag.TableNode;
            }
        }

        public Boolean isFilterNode
        {
            get
            {
                return classType == TreeViewTypeTag.FilterNode;
            }
        }

        public Boolean isChartNode
        {
            get
            {
                return classType == TreeViewTypeTag.ChartNode;
            }
        }

        public Boolean isResultNode
        {
            get
            {
                return classType == TreeViewTypeTag.ResultNode;
            }
        }
        public Boolean isDirectoryNode
        {
            get
            {
                return classType == TreeViewTypeTag.DirectoryNode;
            }
        }
        public Boolean isParagraphNode
        {
            get
            {
                return classType == TreeViewTypeTag.ParagraphNode;
            }
        }

        internal bool IsExceptionNode
        {
            get
            {
                if (isUnleashed)
                    return IsUnleashedEx;
                return (classType == TreeViewTypeTag.ExceptionTable) ||
                    (classType == TreeViewTypeTag.ExceptionFilter);
                /*foreach (int iValue in liNormalTables)
                {
                    if (iImageIndex == iValue)
                        return false;
                    if (iImageIndex == iValue + 1)
                        return true;
                    if (iImageIndex == iValue + 2)
                        return false;
                    if (iImageIndex == iValue + 3)
                        return true;
                }
                return false;*/
            }
            set
            {
                if (isUnleashed)
                {
                    IsUnleashedEx = value;
                    return;
                }
                if (IsExceptionNode)
                    return;
                foreach (int iValue in liNormalTables)
                {
                    if ((iImageIndex == iValue) || (iImageIndex == iValue + 2))
                    {
                        iImageIndex += 1;
                        iSelectedImageIndex += 1;
                        return;
                    }
                }
                if (isFilterNode)
                    classType = TreeViewTypeTag.ExceptionFilter;
                else if (isTableNode)
                    classType = TreeViewTypeTag.ExceptionTable;
            }
        }

        #endregion

        #region La gestion du nom du scénario
        private String sScenarioName;
        public String ScenarioName
        {
            get
            {
                if ((isTableNode) || (isFilterNode) || (isResultNode) || (IsExceptionNode))
                {
                    return sScenarioName;
                }
                if (isUnleashed)
                    return sScenarioName;
                return null;
            }
            set
            {
                if ((isTableNode) || (isFilterNode) || (isResultNode) || (IsExceptionNode))
                    sScenarioName = value;
                if (isUnleashed)
                    sScenarioName = value; ;
            }
        }
        // Enleve le blockage: Scenario name uniquement sur TableNode, FilterNode et ResultNode
        private bool IsUnleashedEx = false;
        private bool isUnleashed = false;
        public bool IsUnleashed
        {
            set {
                IsUnleashedEx = IsExceptionNode;
                isUnleashed = value; }
            get { return isUnleashed; }
        }
        #endregion

        #region La gestion du nom de l'objet associé au tag.
        private String sName;
        public String Name
        {
            get
            {
                return sName;
            }
            set
            {
                sName = value;
            }
        }
        #endregion

        private String sExceptionName;
        public String ExceptionName
        {
            get
            {
                return sExceptionName;
            }
            set
            {
                sExceptionName = value;
            }
        }

        #region La gestion des objets de type AirportNode
        bool bGroup;
        Int32 iIndex;
        String sType;

        #region Les parties utilisées pour l'Itinerary view
        Point pLocation;
        bool bVisible;
        Point pBHSLocation;
        bool bBHSVisible;
        /// <summary>
        /// Permet d'obtenir ou de fixer la position du groupe dans l'itinerary view.
        /// </summary>
        public Point Location
        {
            get
            {
                if (isAirportNode)
                    return pLocation;
                return Point.Empty;
            }
            set
            {
                if (isAirportNode)
                    pLocation = value;
            }
        }
        public Boolean Visible
        {
            get
            {
                if (isAirportNode)
                    return bVisible;
                return false;
            }
            set
            {
                if (isAirportNode)
                    bVisible = value;
            }
        }
        public Point BHSLocation
        {
            get
            {
                if (isAirportNode)
                    return pBHSLocation;
                return Point.Empty;
            }
            set
            {
                if (isAirportNode)
                    pBHSLocation = value;
            }
        }
        public Boolean BHSVisible
        {
            get
            {
                if (isAirportNode)
                    return bBHSVisible;
                return false;
            }
            set
            {
                if (isAirportNode)
                    bBHSVisible = value;
            }
        }
        #endregion

        #region Accesseurs normaux
        public Int32 Index
        {
            get
            {
                if (isAirportNode)
                    return iIndex;
                return -1;
            }
            set
            {
                iIndex = value;
            }
        }
        public Boolean isTerminal
        {
            get
            {
                if (!isAirportNode)
                    return false;
                return (sType == PAX2SIM.sTerminalName);
            }
        }
        public Boolean isLevel
        {
            get
            {
                if (!isAirportNode)
                    return false;
                return (sType == PAX2SIM.sLevelName);
            }
        }

        public Boolean isBHS
        {
            get
            {
                if (!isAirportNode)
                    return false;
                return (sType == PAX2SIM.sBHSName);
            }
        }
        /*public Boolean isDesk
        {
            get
            {
                if (!isAirportNode)
                    return false;
                return false;
            }
        }*/
        public Boolean isGroup
        {
            get
            {
                if (!isAirportNode)
                    return false;
                return bGroup;
            }
        }

        public String AirportObjectType
        {
            get
            {
                if (isAirportNode)
                    return sType;
                return null;
            }
            set
            {
                if (isAirportNode)
                    sType = value;
            }
        }
        #endregion
        #endregion

        #region Gestion des index des images pour l'objet
        private Int32 iImageIndex;
        private Int32 iSelectedImageIndex;
        public Int32 ImageIndex
        {
            get
            {
                return iImageIndex;
            }
            set
            {
                iImageIndex = value;
            }
        }
        public Int32 SelectedImageIndex
        {
            get
            {
                return iSelectedImageIndex;
            }
            set
            {
                iSelectedImageIndex = value;
            }
        }
        internal bool HasNote
        {
            get
            {
                foreach (int iValue in liNormalTables)
                {
                    if (iImageIndex == iValue)
                        return false;
                    if (iImageIndex == iValue + 2)
                        return true;
                    if (iImageIndex == iValue + 3)
                        return true;
                }
                return false;
            }
            set
            {
                if (HasNote == value)
                    return;
                if (value) // image avec note
                {
                    foreach (int iValue in liNormalTables)
                    {
                        if ((iImageIndex == iValue) || (iImageIndex == iValue + 1))
                        {
                            iImageIndex += 2;
                            iSelectedImageIndex += 2;
                            return;
                        }
                    }
                }
                else // image sans note
                {
                    foreach (int iValue in liNormalTables)
                    {
                        if ((iImageIndex == iValue + 2) || (iImageIndex == iValue + 3))
                        {
                            iImageIndex -= 2;
                            iSelectedImageIndex -= 2;
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region Gestion des Filtres
        private bool bCopyTable;
        public bool isCopyTable
        {
            set
            {
                bCopyTable = value;
            }
            get
            {
                return bCopyTable;
            }
        }

        public bool isBloqued
        {
            get
            {
                return ((ImageIndex == FilterBloquedImageIndex) ||
                    (ImageIndex == FilterBloquedExImageIndex) ||
                    (ImageIndex == FilterBloquedNoteImageIndex) ||
                    (ImageIndex == FilterBloquedExNoteImageIndex));
            }
        }

        public void UpdateFilterImage(bool Bloqued, bool CopyFilter)
        {
            bool tmpNote = HasNote;
            if (CopyFilter)
            {
                ImageIndex = TableImageIndex;
                SelectedImageIndex = SelectedTableImageIndex;
                return;
            }
            if (Bloqued)
            {
                /*
                if (ImageIndex == FilterBloquedNoteImageIndex ||
                    ImageIndex == FilterBloquedExNoteImageIndex ||
                foreach (int iValue in liNormalTables)
                {
                    if (iImageIndex == iValue)
                        return false;
                    if (iImageIndex == iValue + 2)
                        return true;
                    if (iImageIndex == iValue + 3)
                        return true;
                }
                return false;*/
                ImageIndex = FilterBloquedImageIndex;
                SelectedImageIndex = SelectedFilterBloquedImageIndex;
                
            }
            else
            {
                ImageIndex = FilterImageIndex;
                SelectedImageIndex = SelectedFilterImageIndex;
            } 
            HasNote = tmpNote;
        }

        #endregion

        #region Gestion des ResultNode
        internal bool IsResultNodeForDesk
        {
            get
            {
                if (bGroup)
                    return false;
                return (ImageIndex == ResultNodeIndexCheckInImage) ||
                    (ImageIndex == ResultNodeIndexPassportImage) ||
                    (ImageIndex == ResultNodeIndexSecurityImage) ||
                    (ImageIndex == ResultNodeIndexArrivalImage) ||
                    (ImageIndex == ResultNodeIndexBaggageImage) ||
                    (ImageIndex == ResultNodeIndexTransferImage) ||
                    (ImageIndex == ResultNodeIndexShuttleImage) ||
                    (ImageIndex == ResultNodeIndexBoardingImage)||
                    (ImageIndex == ResultNodeIndexInImage) ||
                    (ImageIndex == ResultNodeIndexOutImage)||
                    (ImageIndex == ResultNodeIndexShoppingImage) ;
            }
        }
        #endregion

        public int reportTreeIndex;  // >> Task #13384 Report Tree-view        

        #region Fonctions pour les constructeurs des différents objets

        #region Constructeurs des AiportNode
        public static TreeViewTag getAirportNode(String Type, Int32 index, String Name)
        {
            return getAirportNode(Type, index, Name, new Point(0, 0), false);
        }
        public static TreeViewTag getAirportNode(String Type, Int32 index, String Name, Point Location, bool Visible)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.AirportNode;
            Result.Name = Name;
            Result.Index = index;
            Result.AirportObjectType = Type;
            Result.Location = Location;
            Result.Visible = Visible;
            int iType = OverallTools.DataFunctions.AnalyzeName(Type);
            if (iType == -1)
            {
                if (Type.IndexOf(PAX2SIM.sTerminalName) != -1)
                {
                    Result.ImageIndex = IndexTerminalImage;
                }
                else if (Type.IndexOf("Pax Level") != -1)//Version 1.15 (.pax) et V3.0.0.1 (pax2sim)
                {
                    Result.ImageIndex = IndexLevelImage;
                }
                else
                {
                    //Il s'agit d'un objet du BHS, il faut donc que l'on voit ce qu'il en est.
                    switch (Type)
                    {
                        case PAX2SIM.sBHSName:
                            Result.ImageIndex = IndexBHSImage;
                            break;
                        case "Runways":
                        case "Runway":
                            Result.ImageIndex = IndexRunwayImage;
                            break;
                        case "Aircraft Parking Stands":
                        case "Aircraft Parking Stand":
                            Result.ImageIndex = IndexAircraftParkingImage;
                            break;
                        case "Check In Collector Group":
                        case "Check In Collector":
                            Result.ImageIndex = IndexCI_CollectorImage;
                            break;
                        case GestionDonneesHUB2SIM.BHS_String_TransferInfeedGroup:
                        case "Transfer Infeed":
                            Result.ImageIndex = IndexTransferInfeedImage;
                            break;
                        case "HBS Lev1 Group":
                        case "HBS Lev1":
                            Result.ImageIndex = IndexHBS1Image;
                            break;
                        case "HBS Lev3 Group":
                        case "HBS Lev3":
                            Result.ImageIndex = IndexHBS3Image;
                            break;
                        case "HBS Lev5 Group":
                        case "HBS Lev5":
                            Result.ImageIndex = IndexHBS5Image;
                            break;
                        case "EBS Group":
                        case "EBS":
                            Result.ImageIndex = IndexEBSImage;
                            break;
                        case "Make-Up Group":
                        case "Make-Up":
                            Result.ImageIndex = IndexMakeUpImage;
                            break;
                        case "HBS Custom Group":
                        case "HBS Custom":
                            Result.ImageIndex = IndexHBSCustomImage;
                            break;
                        case "MES":
                        case "MES Group":
                            Result.ImageIndex = IndexMESImage;
                            break;
                        case "Arrival Infeed":
                        case GestionDonneesHUB2SIM.BHS_String_ArrivalInfeedGroup:
                            Result.ImageIndex = IndexArrivalInfeedImage;
                            break;
                        default:
                            Result.ImageIndex = IndexAirportImage;
                            break;
                    }



                }
            }
            else
            {
                /*if ((iType > GestionDonneesHUB2SIM.BoardingGateGroup) && (iType< GestionDonneesHUB2SIM.ModelIn))
                {
                    iType -= GestionDonneesHUB2SIM.BoardingGateGroup;
                }
                else if (iType > GestionDonneesHUB2SIM.ModelOut)
                {
                    iType -= 2;
                }*/
                switch (iType)
                {
                    case GestionDonneesHUB2SIM.CheckInGroup:
                    case GestionDonneesHUB2SIM.CheckIn:
                        Result.ImageIndex = IndexCheckInImage;
                        break;
                    case GestionDonneesHUB2SIM.PassportCheckGroup:
                    case GestionDonneesHUB2SIM.PassportCheck:
                        Result.ImageIndex = IndexPassportImage;
                        break;
                    case GestionDonneesHUB2SIM.SecurityCheckGroup:
                    case GestionDonneesHUB2SIM.SecurityCheck:
                        Result.ImageIndex = IndexSecurityImage;
                        break;
                    case GestionDonneesHUB2SIM.ArrivalGateGroup:
                    case GestionDonneesHUB2SIM.ArrivalGate:
                        Result.ImageIndex = IndexArrivalImage;
                        break;
                    case GestionDonneesHUB2SIM.BaggageClaimGroup:
                    case GestionDonneesHUB2SIM.BaggageClaim:
                        Result.ImageIndex = IndexBaggageImage;
                        break;
                    case GestionDonneesHUB2SIM.TransferGroup:
                    case GestionDonneesHUB2SIM.Transfer:
                        Result.ImageIndex = IndexTransferImage;
                        break;
                    case GestionDonneesHUB2SIM.ShuttleGroup:
                    case GestionDonneesHUB2SIM.Shuttle:
                        Result.ImageIndex = IndexShuttleImage;
                        break;
                    case GestionDonneesHUB2SIM.BoardingGateGroup:
                    case GestionDonneesHUB2SIM.BoardingGate:
                        Result.ImageIndex = IndexBoardingImage;
                        break;
                    case GestionDonneesHUB2SIM.ModelInGroup:
                    case GestionDonneesHUB2SIM.ModelIn:
                        Result.ImageIndex = IndexInImage;
                        break;
                    case GestionDonneesHUB2SIM.ModelOutGroup:
                    case GestionDonneesHUB2SIM.ModelOut:
                        Result.ImageIndex = IndexOutImage;
                        break;
                    case GestionDonneesHUB2SIM.ShoppingArea:
                    case GestionDonneesHUB2SIM.ShoppingAreaGroup:
                        Result.ImageIndex = IndexShoppingImage;
                        break;
                    default:
                        Result.ImageIndex = IndexAirportImage;
                        break;
                }
            }
            Result.SelectedImageIndex = Result.ImageIndex;
            Result.bGroup = Type.EndsWith(" Group");
            return Result;
        }
        public static TreeViewTag getAirportNode(String Type, Int32 index, String Name, Point Location, bool Visible, Point BHSLocation, bool BHSVisible)
        {
            TreeViewTag Result = getAirportNode(Type, index, Name, new Point(0, 0), false);
            if (Result != null)
            {
                Result.BHSLocation = BHSLocation;
                Result.BHSVisible = BHSVisible;
            }
            return Result;
        }
        #endregion

        #region Constructeur pour les tables
        public static TreeViewTag getTableNode(String ScenarioName, String Name)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.TableNode;
            Result.ScenarioName = ScenarioName;
            Result.Name = Name;
            Result.ImageIndex = TableImageIndex;
            Result.SelectedImageIndex = SelectedTableImageIndex;
            return Result;
        }

        public static TreeViewTag getExceptionTableNode(String ScenarioName, String TableName, String sTypeException)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.ExceptionTable;
            Result.ScenarioName = ScenarioName;
            Result.Name = TableName;
            Result.ImageIndex = TableExImageIndex;
            Result.SelectedImageIndex = SelectedTableExImageIndex;
            Result.sExceptionName = sTypeException;
            return Result;
        }
        #endregion

        #region Constructeur pour les noeuds de résultats (ceux qui ont les icones de l'aéroport).
        public static TreeViewTag getResultNode(String ScenarioName, String Name)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.ResultNode;
            Result.ScenarioName = ScenarioName;
            Result.Name = Name;
            int[] tiGroupType = OverallTools.DataFunctions.AnalyzeGroupName(Name);
            if (tiGroupType == null)
            {
                Result.ImageIndex = ResultNodeIndexAirportImage;
            }
            else if (tiGroupType[1] == 0)
            {
                Result.ImageIndex = ResultNodeIndexTerminalImage;
            }
            else if (tiGroupType[2] == 0)
            {
                Result.ImageIndex = ResultNodeIndexLevelImage;
            }
            else
            {
                /*if (tiGroupType[2] > GestionDonneesHUB2SIM.BoardingGateGroup)
                    tiGroupType[2] -= GestionDonneesHUB2SIM.BoardingGateGroup;
                else
                    Result.bGroup = true;
                */
                switch (tiGroupType[2])
                {
                    case GestionDonneesHUB2SIM.CheckInGroup:
                    case GestionDonneesHUB2SIM.PassportCheckGroup:
                    case GestionDonneesHUB2SIM.SecurityCheckGroup:
                    case GestionDonneesHUB2SIM.ArrivalGateGroup:
                    case GestionDonneesHUB2SIM.BaggageClaimGroup:
                    case GestionDonneesHUB2SIM.TransferGroup:
                    case GestionDonneesHUB2SIM.ShuttleGroup:
                    case GestionDonneesHUB2SIM.BoardingGateGroup:
                    case GestionDonneesHUB2SIM.ModelInGroup:
                    case GestionDonneesHUB2SIM.ModelOutGroup:
                        Result.bGroup = true;
                        break;
                    default:
                        break;
                }
                switch (tiGroupType[2])
                {
                    case GestionDonneesHUB2SIM.CheckInGroup:
                    case GestionDonneesHUB2SIM.CheckIn:
                        Result.ImageIndex = ResultNodeIndexCheckInImage;
                        break;
                    case GestionDonneesHUB2SIM.PassportCheckGroup:
                    case GestionDonneesHUB2SIM.PassportCheck:
                        Result.ImageIndex = ResultNodeIndexPassportImage;
                        break;
                    case GestionDonneesHUB2SIM.SecurityCheckGroup:
                    case GestionDonneesHUB2SIM.SecurityCheck:
                        Result.ImageIndex = ResultNodeIndexSecurityImage;
                        break;
                    case GestionDonneesHUB2SIM.ArrivalGateGroup:
                    case GestionDonneesHUB2SIM.ArrivalGate:
                        Result.ImageIndex = ResultNodeIndexArrivalImage;
                        break;
                    case GestionDonneesHUB2SIM.BaggageClaimGroup:
                    case GestionDonneesHUB2SIM.BaggageClaim:
                        Result.ImageIndex = ResultNodeIndexBaggageImage;
                        break;
                    case GestionDonneesHUB2SIM.TransferGroup:
                    case GestionDonneesHUB2SIM.Transfer:
                        Result.ImageIndex = ResultNodeIndexTransferImage;
                        break;
                    case GestionDonneesHUB2SIM.ShuttleGroup:
                    case GestionDonneesHUB2SIM.Shuttle:
                        Result.ImageIndex = ResultNodeIndexShuttleImage;
                        break;
                    case GestionDonneesHUB2SIM.BoardingGateGroup:
                    case GestionDonneesHUB2SIM.BoardingGate:
                        Result.ImageIndex = ResultNodeIndexBoardingImage;
                        break;
                    case GestionDonneesHUB2SIM.ModelInGroup:
                    case GestionDonneesHUB2SIM.ModelIn:
                        Result.ImageIndex = ResultNodeIndexInImage;
                        break;
                    case GestionDonneesHUB2SIM.ModelOutGroup:
                    case GestionDonneesHUB2SIM.ModelOut:
                        Result.ImageIndex = ResultNodeIndexOutImage;
                        break;
                    case GestionDonneesHUB2SIM.ShoppingArea:
                    case GestionDonneesHUB2SIM.ShoppingAreaGroup:
                        Result.ImageIndex = ResultNodeIndexShoppingImage;
                        break;
                }
            }
            Result.SelectedImageIndex = Result.ImageIndex;
            return Result;
        }
        #endregion

        #region Constructeurs pour les filtres.
        public static TreeViewTag getFilterNode(String ScenarioName, String Name)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.FilterNode;
            Result.ScenarioName = ScenarioName;
            Result.Name = Name;
            Result.ImageIndex = TableImageIndex;
            Result.SelectedImageIndex = SelectedTableImageIndex;
            Result.bCopyTable = true;
            return Result;
        }
        public static TreeViewTag getFilterNode(String ScenarioName, String Name, bool Bloqued)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.FilterNode;
            Result.ScenarioName = ScenarioName;
            Result.Name = Name;
            Result.UpdateFilterImage(Bloqued, false);
            return Result;
        }

        public static TreeViewTag getFilterNode(String ScenarioName, String Name, bool Bloqued, bool copy)
        {
            if (copy)
                return getFilterNode(ScenarioName, Name);
            return getFilterNode(ScenarioName, Name, Bloqued);
        }

        public static TreeViewTag getExceptionFilterNode(String ScenarioName, String TableName, String sTypeException, bool Bloqued, bool copy)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.ExceptionFilter;
            Result.ScenarioName = ScenarioName;
            Result.Name = TableName;
            Result.sExceptionName = sTypeException;
            if (Bloqued)
            {
                Result.ImageIndex = FilterBloquedExImageIndex;
                Result.SelectedImageIndex = SelectedFilterBloquedExImageIndex;
            }
            else if (copy)
            {
                Result.ImageIndex = TableExImageIndex;
                Result.SelectedImageIndex = SelectedTableExImageIndex;
            }
            else
            {
                Result.ImageIndex = FilterExImageIndex;
                Result.SelectedImageIndex = SelectedFilterExImageIndex;
            }
            return Result;
        }
        #endregion

        #region Constructeurs pour les Charts
        public static TreeViewTag getChartNode(String Name)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.ChartNode;
            Result.ScenarioName = "";
            Result.Name = Name;
            Result.ImageIndex = ChartImageIndex;
            Result.SelectedImageIndex = ChartImageIndex;
            return Result;
        }
        #endregion

        #region Constructeurs pour les dossiers
        public static TreeViewTag getDirectoryNode(String Name)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.DirectoryNode;
            Result.Name = Name;
            Result.ImageIndex = IndexDirectory;
            Result.SelectedImageIndex = IndexDirectory;
            return Result;
        }
        #endregion

        #region Constructeurs pour les paragraphes
        public static TreeViewTag getParagraphNode(String Name)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.ParagraphNode;
            Result.Name = Name;
            Result.ImageIndex = ParagraphImageIndex;
            Result.SelectedImageIndex = ParagraphImageIndex;
            return Result;
        }
        public static TreeViewTag getBlockedParagraphNode(String sName)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.ParagraphNode;
            Result.Name = sName;
            Result.ImageIndex = BlockedParagraphImageIndex;
            Result.SelectedImageIndex = BlockedParagraphImageIndex;
            return Result;
        }
        //>>GanttNote for Report
        // creates a Tag for the Node holding the Gantt Note
        // it will use the ParagraphNode type
        public static TreeViewTag getGanttParagraphNode(String sName)
        {
            TreeViewTag Result = new TreeViewTag();
            Result.classType = TreeViewTypeTag.ParagraphNode;
            Result.Name = sName;
            Result.ImageIndex = Model.GANTT_NOTE_IMAGE_INDEX;
            Result.SelectedImageIndex = Model.GANTT_NOTE_IMAGE_INDEX;
            return Result;
        }
        // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
        public static TreeViewTag getDashboardParagraphNode(String name)
        {
            TreeViewTag result = new TreeViewTag();
            result.classType = TreeViewTypeTag.ParagraphNode;
            result.Name = name;
            result.ImageIndex = GlobalNames.DASHBOARD_NOTE_IMAGE_INDEX;
            result.SelectedImageIndex = GlobalNames.DASHBOARD_NOTE_IMAGE_INDEX;
            return result;
        }
        // << Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
        #endregion

        #endregion

        //AirportNode, TableNode, FilterNode, ChartNode, ResultNode, DirectoryNode
        public TreeViewTag()
        {
            classType = TreeViewTypeTag.DirectoryNode;
            sScenarioName = "";
            sName = "";
            iIndex = 0;
            sType = "";
            pLocation = new Point(0, 0);
            bVisible = false;
            iImageIndex = 0;
            iSelectedImageIndex = 0;
            bCopyTable = false;
        }

        public TreeViewTag Clone()
        {
            TreeViewTag tvtResult = new TreeViewTag();

            tvtResult.classType = classType;
            tvtResult.IsUnleashedEx = IsUnleashedEx;
            tvtResult.IsUnleashed = isUnleashed; // need to be donne before change the scenario name
            tvtResult.sScenarioName = sScenarioName;
            tvtResult.sName = sName;
            tvtResult.iIndex = iIndex;
            tvtResult.sType = sType;
            tvtResult.pLocation = pLocation;
            tvtResult.bVisible = bVisible;
            tvtResult.pBHSLocation = pBHSLocation;
            tvtResult.bBHSVisible = bBHSVisible;
            tvtResult.iImageIndex = iImageIndex;
            tvtResult.reportTreeIndex = reportTreeIndex;
            tvtResult.iSelectedImageIndex = iSelectedImageIndex;
            tvtResult.ExceptionName = ExceptionName;
            if (IsExceptionNode && (isParagraphNode ||isChartNode) && IsUnleashed) // cas particulier d'une note sur une table d'exception dans l'editeur de rapport
                tvtResult.IsExceptionNode = true;
            return tvtResult;
        }

        public override string ToString()
        {
            if (isAirportNode)
            {
                if (iIndex == 0)
                    return sType + " ( " + sName + " )";
                return sType + " " + iIndex + " ( " + sName + " )";
            }
            return base.ToString();
        }

        internal void Dispose()
        {
            sExceptionName = null;
            sName = null;
            sScenarioName = null;
            sType = null;
        }
    }
}
