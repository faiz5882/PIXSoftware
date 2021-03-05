using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.Classes
{
    #region Les différentes classes pour la gestion de l'affichage dans le datagridview
    #region class VisualisationMode
    /// <summary>
    /// Cette classe est utilisée pour gérer le mode de visualisation de la table chargée 
    /// dans PAX2SIM. Cela permet de mettre en forme individuellement les cellules, ainsi 
    /// que de déterminer la façon dont les tables peuvent être modifiées.
    /// </summary>
    public class VisualisationMode
    {
        #region Les différentes variables de la classe avec leurs accesseurs
        #region public bool ShowRowHeader
        /// <summary>
        /// Booléen qui indique si l'entète de ligne doit être affiché ou non. Si ce booléen
        /// est vrai, alors le dataGridView affichera l'entète. Sinon celui ci sera masqué.
        /// </summary>
        private bool ShowRowHeader_;
        /// <summary>
        /// Booléen qui indique si l'entète de ligne doit être affiché ou non. Si ce booléen
        /// est vrai, alors le dataGridView affichera l'entète. Sinon celui ci sera masqué.
        /// </summary>
        public bool ShowRowHeader
        {
            get
            {
                return ShowRowHeader_;
            }
            set
            {
                ShowRowHeader_ = value;
            }
        }
        #endregion
        #region public bool Modifiable
        /// <summary>
        /// Booléen qui indique si le contenu de la table peut être modifié ou non.
        /// </summary>
        private bool Modifiable_;
        /// <summary>
        /// Booléen qui indique si le contenu de la table peut être modifié ou non.
        /// </summary>
        public bool Modifiable
        {
            get
            {
                return Modifiable_;
            }
            set
            {
                Modifiable_ = value;
            }
        }
        #endregion
        #region public bool AllowAddLine
        /// <summary>
        /// Booléen qui indique s'il est possible d'ajouter une ligne à la table.
        /// </summary>
        private bool AllowAddLine_;
        /// <summary>
        /// Booléen qui indique s'il est possible d'ajouter une ligne à la table.
        /// </summary>
        public bool AllowAddLine
        {
            get
            {
                return AllowAddLine_;
            }
            set
            {
                AllowAddLine_ = value;
            }
        }
        #endregion
        #region public bool FirstColumnInHeader
        /// <summary>
        /// Booléen qui indique si le contenu de la première colonne doit être affiché 
        /// dans l'entète de ligne ou non. Si Vrai, alors la première ligne n'apparaîtra
        /// pas du tout. Son contenu sera simplement visible dans l'entète de ligne. Le
        /// contenu sera alors non modifiable.
        /// </summary>
        private bool FirstColumnInHeader_;
        /// <summary>
        /// Booléen qui indique si le contenu de la première colonne doit être affiché 
        /// dans l'entète de ligne ou non. Si Vrai, alors la première ligne n'apparaîtra
        /// pas du tout. Son contenu sera simplement visible dans l'entète de ligne. Le
        /// contenu sera alors non modifiable.
        /// </summary>
        public bool FirstColumnInHeader
        {
            get
            {
                return FirstColumnInHeader_;
            }
            set
            {
                FirstColumnInHeader_ = value;
            }
        }
        #endregion
        #region public bool Sortable
        /// <summary>
        /// Booléen qui indique si la table peut être trier ou non. Si le tri est activé,
        /// alors cela veut dire que l'utilisateur sera capable de cliquer sur l'entete 
        /// de colonne pour retrier suivant une colonne particulière.
        /// </summary>
        private bool Sortable_;
        /// <summary>
        /// Booléen qui indique si la table peut être trier ou non. Si le tri est activé,
        /// alors cela veut dire que l'utilisateur sera capable de cliquer sur l'entete 
        /// de colonne pour retrier suivant une colonne particulière.
        /// </summary>
        public bool Sortable
        {
            get
            {
                return Sortable_;
            }
            set
            {
                Sortable_ = value;
            }
        }
        #endregion
        #region public Color BackgroundDefaultColor
        /// <summary>
        /// Cette couleur correspond à la couleur de fond par défaut. Cette couleur sera 
        /// utilisé lorsque la cellule n'aura aucune couleur assignée. (La classe 
        /// ConditionnalFormat permetant de changer la couleur d'une cellule en fonction
        /// de son contenu ou de sa position.
        /// </summary>
        private Color BackgroundDefaultColor_;
        /// <summary>
        /// Cette couleur correspond à la couleur de fond par défaut. Cette couleur sera 
        /// utilisé lorsque la cellule n'aura aucune couleur assignée. (La classe 
        /// ConditionnalFormat permetant de changer la couleur d'une cellule en fonction
        /// de son contenu ou de sa position.
        /// </summary>
        public Color BackgroundDefaultColor
        {
            get
            {
                return BackgroundDefaultColor_;
            }
            set
            {
                BackgroundDefaultColor_ = value;
            }
        }
        #endregion
        #region public Color BackgroundSelectedColor
        /// <summary>
        /// Cette couleur correspond à la couleur de fond par défaut (lorsque la 
        /// cellule est sélectionnée). Cette couleur sera 
        /// utilisé lorsque la cellule n'aura aucune couleur assignée. (La classe 
        /// ConditionnalFormat permetant de changer la couleur d'une cellule en fonction
        /// de son contenu ou de sa position.
        /// </summary>
        private Color BackgroundSelectedColor_;
        /// <summary>
        /// Cette couleur correspond à la couleur de fond par défaut (lorsque la 
        /// cellule est sélectionnée). Cette couleur sera 
        /// utilisé lorsque la cellule n'aura aucune couleur assignée. (La classe 
        /// ConditionnalFormat permetant de changer la couleur d'une cellule en fonction
        /// de son contenu ou de sa position.
        /// </summary>
        public Color BackgroundSelectedColor
        {
            get
            {
                return BackgroundSelectedColor_;
            }
            set
            {
                BackgroundSelectedColor_ = value;
            }
        }
        #endregion
        #region public Color FontDefaultColor
        /// <summary>
        /// Cette couleur correspond à la couleur de police par défaut. Cette couleur sera 
        /// utilisé lorsque la cellule n'aura aucune couleur assignée. (La classe 
        /// ConditionnalFormat permetant de changer la couleur d'une cellule en fonction
        /// de son contenu ou de sa position.
        /// </summary>
        private Color FontDefaultColor_;
        /// <summary>
        /// Cette couleur correspond à la couleur de police par défaut. Cette couleur sera 
        /// utilisé lorsque la cellule n'aura aucune couleur assignée. (La classe 
        /// ConditionnalFormat permetant de changer la couleur d'une cellule en fonction
        /// de son contenu ou de sa position.
        /// </summary>
        public Color FontDefaultColor
        {
            get
            {
                return FontDefaultColor_;
            }
            set
            {
                FontDefaultColor_ = value;
            }
        }
        #endregion
        #region public Color FontSelectedColor
        /// <summary>
        /// Cette couleur correspond à la couleur de police par défaut (lorsque la 
        /// cellule est sélectionnée). Cette couleur sera 
        /// utilisé lorsque la cellule n'aura aucune couleur assignée. (La classe 
        /// ConditionnalFormat permetant de changer la couleur d'une cellule en fonction
        /// de son contenu ou de sa position.
        /// </summary>
        private Color FontSelectedColor_;
        /// <summary>
        /// Cette couleur correspond à la couleur de police par défaut (lorsque la 
        /// cellule est sélectionnée). Cette couleur sera 
        /// utilisé lorsque la cellule n'aura aucune couleur assignée. (La classe 
        /// ConditionnalFormat permetant de changer la couleur d'une cellule en fonction
        /// de son contenu ou de sa position.
        /// </summary>
        public Color FontSelectedColor
        {
            get
            {
                return FontSelectedColor_;
            }
            set
            {
                FontSelectedColor_ = value;
            }
        }
        #endregion
        /// <summary>
        /// Cette énumération représente les différents façons dont les données de la table
        /// sont sélectionnée. (Par ligne, par colonne, ou par cellule).
        /// </summary>
        public enum SelectionModeEnum
        {
            /// <summary>
            /// Le contenu de la table sera sélectionné ligne par ligne.
            /// </summary>
            Row,
            /// <summary>
            /// Le contenu de la table sera sélectionné colonne par colonne.
            /// </summary>
            Column,
            /// <summary>
            /// Le contenu de la table sera sélectionné cellule par cellule.
            /// </summary>
            Cell
        };
        #region public SelectionModeEnum SelectionMode
        /// <summary>
        /// Cette variable contient la façon dont le contenu de la table doit être sélectionné.
        /// </summary>
        private SelectionModeEnum SelectionMode_;
        /// <summary>
        /// Cette variable contient la façon dont le contenu de la table doit être sélectionné.
        /// </summary>
        public SelectionModeEnum SelectionMode
        {
            get
            {
                return SelectionMode_;
            }
            set
            {
                SelectionMode_ = value;
            }
        }
        #endregion
        #region public bool AllowEditRow
        private bool bAllowEditRow;
        public bool AllowEditRow
        {
            get
            {
                return bAllowEditRow;
            }
            set
            {
                bAllowEditRow = value;
            }
        }
        #endregion
        #region public bool AllowEditColumn
        private bool bAllowEditColumn;
        public bool AllowEditColumn
        {
            get
            {
                return bAllowEditColumn;
            }
            set
            {
                bAllowEditColumn = value;
            }
        }
        #endregion
        public enum EditModeEnum { None, Row, Column, Cell };
        #region public EditModeEnum EditMode
        private EditModeEnum EditMode_;
        public EditModeEnum EditMode
        {
            get
            {
                return EditMode_;
            }
            set
            {
                EditMode_ = value;
            }
        }
        #endregion
        #region public int[] ColumnBlocked
        private int[] ColumnBlocked_;
        public int[] ColumnBlocked
        {
            get
            {
                return ColumnBlocked_;
            }
            set
            {
                ColumnBlocked_ = value;
            }
        }
        #endregion
        #region public int[] ColumnFrozen
        private int[] ColumnFrozen_;
        public int[] ColumnFrozen
        {
            get
            {
                return ColumnFrozen_;
            }
            set
            {
                ColumnFrozen_ = value;
            }
        }
        #endregion
        #region public int[] ColumnWidth
        private int[] ColumnWidth_;
        public int[] ColumnWidth
        {
            get
            {
                return ColumnWidth_;
            }
            set
            {
                ColumnWidth_ = value;
            }
        }

        #endregion
        #region public bool[] allowSortColumn
        private bool[] allowSortColumn_;
        public bool[] allowSortColumn
        {
            get
            {
                return allowSortColumn_;
            }
            set
            {
                allowSortColumn_ = value;
            }
        }
        #endregion
        #region public int[] PrimaryKey
        private int[] PrimaryKey_;
        public int[] PrimaryKey
        {
            get
            {
                return PrimaryKey_;
            }
            set
            {
                PrimaryKey_ = value;
            }
        }
        #endregion
        #region public int[][] ColumnsFormated;
        private int[][] ColumnsFormated_;
        public int[][] ColumnsFormated
        {
            get
            {
                return ColumnsFormated_;
            }
            set
            {
                ColumnsFormated_ = value;
            }
        }
        #endregion
        #region public ConditionnalFormat[] ConditionnalFormat;
        private ConditionnalFormat[] ConditionnalFormat_;
        internal ConditionnalFormat[] ConditionnalFormatClass
        {
            get
            {
                return ConditionnalFormat_;
            }
            set
            {
                ConditionnalFormat_ = value;
            }
        }
        internal ConditionnalFormat getConditionnalFormat(int iNumColumn)
        {
            if (ConditionnalFormat_ == null)
                return null;
            if (ConditionnalFormat_.Length == 0)
                return null;
            if (ColumnsFormated == null)
                return ConditionnalFormat_[0];
            if (ColumnsFormated.Length == 0)
                return ConditionnalFormat_[0];
            for (int i = 0; i < ConditionnalFormat_.Length; i++)
            {
                if (ColumnsFormated[i].Length == 0)
                    continue;
                for (int j = 0; j < ColumnsFormated[i].Length; j++)
                {
                    if (ColumnsFormated[i][j] == iNumColumn)
                        return ConditionnalFormat_[i];
                }
            }
            return ConditionnalFormat_[0];
        }
        #endregion
        #region public interface ConditionnalFormat
        internal class Format
        {
            internal Format(Color cColor_)
            {
                cColor = cColor_;
            }
            internal Color cColor;/*
            internal String sPrefixe;
            internal String sSuffixe;*/
        }
        public interface ConditionnalFormat
        {
            /*Format getFormat(Object value);
            Format getFormat(int indexColumn, int indexRow);*/
            Color getColor(Object value);
            Color getColor(int indexColumn, int indexRow);
            /*String getPrefixe(Object value);
            String getPrefixe(int indexColumn, int indexRow);
            String getSuffixe(Object value);
            String getSufffixe(int indexColumn, int indexRow);*/
            ConditionnalFormat Clone();
        }
        #endregion
        #endregion

        #region Fonctions pour l'initialisation de la classe.
        private void Initialize()
        {
            ShowRowHeader_ = false;
            Modifiable_ = false;
            AllowAddLine_ = false;
            FirstColumnInHeader_ = false;
            Sortable_ = true;
            bAllowEditRow = false;
            bAllowEditColumn = false;
            BackgroundDefaultColor_ = Color.White;
            BackgroundSelectedColor_ = Color.Blue;
            FontDefaultColor_ = Color.Black;
            FontSelectedColor_ = Color.White;
            SelectionMode_ = SelectionModeEnum.Row;
            EditMode_ = EditModeEnum.None;
            ColumnBlocked_ = null;
            PrimaryKey_ = null;
            ColumnFrozen_ = null;
            ColumnWidth_ = null;
            allowSortColumn_ = null;
            ConditionnalFormat_ = null;
            ColumnsFormated_ = null;
        }
        private void Initialize(bool Modifiable__,
                                bool AllowAddRow__,
                                bool Sortable__,
                                int[] ColumnBlocked__,
                                int[] PrimaryKey__)
        {
            Initialize();
            Modifiable_ = Modifiable__;
            AllowAddLine_ = AllowAddRow__;
            Sortable_ = Sortable__;
            ColumnBlocked_ = ColumnBlocked__;
            PrimaryKey_ = PrimaryKey__;
        }
        private void Initialize(bool Modifiable__,
                         bool AllowAddRow__,
                         bool Sortable__,
                         int[] ColumnBlocked__,
                         int[] PrimaryKey__,
                         bool ShowRowHeader__,
                         bool FirstColumnInHeader__,
                         Color BackgroundDefaultColor__,
                         Color BackgroundSelectedColor__,
                         Color FontDefaultColor__,
                         Color FontSelectedColor__,
                         SelectionModeEnum SelectionMode__,
                         EditModeEnum EditMode__,
                         int[] ColumnFrozen__,
                         int[] ColumnWidth__,
                         bool[] AllowSortColumn__,
                         int[][] ColumnsFormated__,
                         ConditionnalFormat[] ConditionnalFormatClass__)
        {
            Initialize(Modifiable__, AllowAddRow__, Sortable__, ColumnBlocked__, PrimaryKey__);

            ShowRowHeader_ = ShowRowHeader__;
            FirstColumnInHeader_ = FirstColumnInHeader__;
            BackgroundDefaultColor_ = BackgroundDefaultColor__;
            BackgroundSelectedColor_ = BackgroundSelectedColor__;
            FontDefaultColor_ = FontDefaultColor__;
            FontSelectedColor_ = FontSelectedColor__;
            SelectionMode_ = SelectionMode__;
            EditMode_ = EditMode__;
            ColumnFrozen_ = ColumnFrozen__;
            ColumnWidth_ = ColumnWidth__;
            allowSortColumn_ = AllowSortColumn__;
            ColumnsFormated_ = ColumnsFormated__;
            ConditionnalFormat_ = ConditionnalFormatClass__;
        }
        #endregion

        #region Les constructeurs de la classe.
        public VisualisationMode()
        {
            Initialize();
        }

        public VisualisationMode(bool Modifiable__,
                                 bool AllowAddRow__,
                                 bool Sortable__,
                                 int[] ColumnBlocked__,
                                 int[] PrimaryKey__)
        {

            Initialize(Modifiable__, AllowAddRow__, Sortable__, ColumnBlocked__, PrimaryKey__);
        }

        internal VisualisationMode(bool Modifiable__,
                                 bool AllowAddRow__,
                                 bool Sortable__,
                                 int[] ColumnBlocked__,
                                 int[] PrimaryKey__,
                                 bool ShowRowHeader__,
                                 bool FirstColumnInHeader__,
                                 Color BackgroundDefaultColor__,
                                 Color BackgroundSelectedColor__,
                                 Color FontDefaultColor__,
                                 Color FontSelectedColor__,
                                 SelectionModeEnum SelectionMode__,
                                 EditModeEnum EditMode__,
                                 int[] ColumnFrozen__,
                                 int[] ColumnWidth__,
                                 bool[] AllowSortColumn__,
                                 int[][] ColumnsFormated__,
                                 ConditionnalFormat[] ConditionnalFormatClass__)
        {
            Initialize(Modifiable__, AllowAddRow__, Sortable__, ColumnBlocked__, PrimaryKey__,
                    ShowRowHeader__, FirstColumnInHeader__, BackgroundDefaultColor__,
                    BackgroundSelectedColor__, FontDefaultColor__, FontSelectedColor__,
                    SelectionMode__, EditMode__, ColumnFrozen__, ColumnWidth__, AllowSortColumn__,
                    ColumnsFormated__, ConditionnalFormatClass__);
        }

        public VisualisationMode(VisualisationMode AncienMode)
        {
            int[] ColumnBlocked__ = null;
            int[] ColumnFrozen__ = null;
            int[] ColumnWidth__ = null;
            int[] PrimaryKey__ = null;
            bool[] AllowSortColumn__ = null;
            int[][] ColumnsFormated__ = null;
            ConditionnalFormat[] ConditionnalFormatClass__ = null;
            int i;
            if (AncienMode.ColumnBlocked != null)
            {
                ColumnBlocked__ = new int[AncienMode.ColumnBlocked.Length];
                for (i = 0; i < AncienMode.ColumnBlocked.Length; i++)
                {
                    ColumnBlocked__[i] = AncienMode.ColumnBlocked[i];
                }
            }

            if (AncienMode.ColumnFrozen != null)
            {
                ColumnFrozen__ = new int[AncienMode.ColumnFrozen.Length];// { AncienMode.ColumnFrozen };
                for (i = 0; i < AncienMode.ColumnFrozen.Length; i++)
                {
                    ColumnFrozen__[i] = AncienMode.ColumnFrozen[i];
                }
            }

            if (AncienMode.ColumnWidth != null)
            {
                ColumnWidth__ = new int[AncienMode.ColumnWidth.Length];// { AncienMode.ColumnWidth };
                for (i = 0; i < AncienMode.ColumnWidth.Length; i++)
                {
                    ColumnWidth__[i] = AncienMode.ColumnWidth[i];
                }
            }
            if (AncienMode.PrimaryKey != null)
            {
                PrimaryKey__ = new int[AncienMode.PrimaryKey.Length];
                for (i = 0; i < AncienMode.PrimaryKey.Length; i++)
                {
                    PrimaryKey__[i] = AncienMode.PrimaryKey[i];
                }
            }
            if (AncienMode.ConditionnalFormatClass != null)
            {
                ConditionnalFormatClass__ = new ConditionnalFormat[AncienMode.ConditionnalFormatClass.Length];
                ColumnsFormated__ = new int[AncienMode.ConditionnalFormatClass.Length][];
                for (i = 0; i < AncienMode.ConditionnalFormatClass.Length; i++)
                {
                    ConditionnalFormatClass__[i] = AncienMode.ConditionnalFormatClass[i].Clone();
                    if (AncienMode.ColumnsFormated_ != null)
                    {
                        ColumnsFormated__[i] = new int[AncienMode.ColumnsFormated_[i].Length];
                        for (int j = 0; j < AncienMode.ColumnsFormated_[i].Length; j++)
                        {
                            ColumnsFormated__[i][j] = AncienMode.ColumnsFormated_[i][j];
                        }
                    }
                    else
                    {
                        ColumnsFormated__ = null;
                    }
                }
            }
            Initialize(AncienMode.Modifiable, AncienMode.AllowAddLine, AncienMode.Sortable, ColumnBlocked__,
                PrimaryKey__, AncienMode.ShowRowHeader, AncienMode.FirstColumnInHeader,
                AncienMode.BackgroundDefaultColor, AncienMode.BackgroundSelectedColor, AncienMode.FontDefaultColor,
                AncienMode.FontSelectedColor, AncienMode.SelectionMode, AncienMode.EditMode, ColumnFrozen__,
                ColumnWidth__, AllowSortColumn__, ColumnsFormated__, ConditionnalFormatClass__);
            bAllowEditColumn = AncienMode.AllowEditColumn;
            bAllowEditRow = AncienMode.AllowEditRow;
        }
        #endregion

        #region La fonction pour déterminer le format conditionnel.
        internal Color getVisualisationMode(int indexColumn, Object value)
        {
            ConditionnalFormat format = getConditionnalFormat(indexColumn);

            if (format == null)
            {
                return BackgroundDefaultColor_;
            }
            return format.getColor(value);
        }
        internal Color getVisualisationMode(int indexColumn, int indexRow)
        {
            ConditionnalFormat format = getConditionnalFormat(indexColumn);
            if (format == null)
            {
                return BackgroundDefaultColor_;
            }
            return format.getColor(indexColumn, indexRow);
        }
        public String getErrorText(int indexColumn, int indexRow)
        {
            ConditionnalFormat format = getConditionnalFormat(indexColumn);
            if ((format == null) || (format.GetType() != typeof(ConditionnalFormatErrors)))
                return "";
            return ((ConditionnalFormatErrors)format).getError(indexColumn, indexRow);
        }
        public String getPrefixe(int indexColumn, int indexRow)
        {
            ConditionnalFormat format = getConditionnalFormat(indexColumn);
            if ((format == null) || (format.GetType() != typeof(ConditionnalFormatErrors)))
                return "";
            return ((ConditionnalFormatErrors)format).getPrefixe(indexColumn, indexRow);
        }
        public String getSuffixe(int indexColumn, int indexRow)
        {
            ConditionnalFormat format = getConditionnalFormat(indexColumn);
            if ((format == null) || (format.GetType() != typeof(ConditionnalFormatErrors)))
                return "";
            return ((ConditionnalFormatErrors)format).getSuffixe(indexColumn, indexRow);
        }
        #endregion

        public VisualisationMode Clone()
        {
            return new VisualisationMode(this);
        }

        private static VisualisationMode vmLockMode;
        public static VisualisationMode LockMode
        {
            get
            {
                if (vmLockMode == null)
                    vmLockMode = new VisualisationMode(false, false, true, null, null);
                return vmLockMode;
            }
        }
        private static VisualisationMode vmModifiableMode;
        public static VisualisationMode ModifiableMode
        {
            get
            {
                if (vmModifiableMode == null)
                    vmModifiableMode = new VisualisationMode(true, false, false, null, null);
                return vmModifiableMode;
            }
        }

    }
    #endregion

    #region La classe ConditionnalFormatCharacter qui est l'implémentation de l'interface ConditionnalFormat
    /// <summary>
    /// Classe de mise en forme qui se base sur l'occurence d'un caractère.
    /// </summary>
    class ConditionnalFormatCharacter : VisualisationMode.ConditionnalFormat
    {/*
        static List<ConditionnalFormatCharacter> lDefinedEntities = null;
        private static void AddEntities(ConditionnalFormatCharacter cfcError)
        {
            if (lDefinedEntities == null)
                lDefinedEntities = new List<ConditionnalFormatCharacter>();
            lDefinedEntities.Add(cfcError);
        }*/

        private bool bAnalyseContent_;
        #region public char Character
        private char Character_;
        public char Character
        {
            get
            {
                return Character_;
            }
            set
            {
                Character_ = value;
            }
        }
        #endregion

        #region public Color DefaultColor
        private Color DefaultColor_;
        public Color DefaultColor
        {
            get
            {
                return DefaultColor_;
            }
            set
            {
                DefaultColor_ = value;
            }
        }
        #endregion

        #region Les constructeurs de la classe.

        public ConditionnalFormatCharacter(char Character__, bool bAnalyseContent)
        {
            //AddEntities(this);
            Character_ = Character__;
            bAnalyseContent_ = bAnalyseContent;
            DefaultColor_ = Color.White;
            lesConditions_ = null;
        }
        public ConditionnalFormatCharacter(char Character__)
        {
            //AddEntities(this);
            Character_ = Character__;
            bAnalyseContent_ = false;
            DefaultColor_ = Color.White;
            lesConditions_ = null;
        }
        private ConditionnalFormatCharacter(ConditionnalFormatCharacter AncienConditionnalFormat)
        {
            //AddEntities(this);
            Character_ = AncienConditionnalFormat.Character;
            DefaultColor_ = AncienConditionnalFormat.DefaultColor;
            lesConditions_ = null;
            bAnalyseContent_ = AncienConditionnalFormat.bAnalyseContent_;
            if (AncienConditionnalFormat.lesConditions_ != null)
            {
                lesConditions_ = new List<Condition>();
                foreach (Condition cond in AncienConditionnalFormat.lesConditions_)
                {
                    lesConditions_.Add(new Condition(cond));
                }
            }
        }
        #endregion

        #region Les fonctions et les variables pour stocker les couleurs de visualisation.
        private List<Condition> lesConditions_;
        class Condition
        {
            public int Value;
            public Color NewColor;
            public Condition(int Value_, Color NewColor_)
            {
                Value = Value_;
                NewColor = NewColor_;
            }
            public Condition(Condition oldValue)
            {
                Value = oldValue.Value;
                NewColor = oldValue.NewColor;
            }
        }
        class ConditionComparer : IComparer<Condition>
        {
            public int Compare(Object x, Object y)
            {
                return ((Condition)x).Value - ((Condition)y).Value;
            }
            public int Compare(Condition x, Condition y)
            {
                return x.Value - y.Value;
            }
        }
        public void setCondition(int Value, Color NewColor)
        {
            if (lesConditions_ == null)
                lesConditions_ = new List<Condition>();
            lesConditions_.Add(new Condition(Value, NewColor));
            lesConditions_.Sort(new ConditionComparer());
        }
        #endregion

        #region Les trois fonctons membres de l'interface.

        public int nombreCaractere(String chaine)
        {
            String[] tsChaines = chaine.Split(Character_);
            if (tsChaines.Length <= 1)
                return 1;
            tsChaines[0] = tsChaines[0].Remove(0, 1);
            tsChaines[tsChaines.Length - 1] = tsChaines[tsChaines.Length - 1].Remove(tsChaines[tsChaines.Length - 1].Length - 1, 1);
            for (int i = 0; i < tsChaines.Length; i++)
            {
                if (!tsChaines[i].Contains("("))
                    continue;
                tsChaines[i] = tsChaines[i].Remove(tsChaines[i].IndexOf("("));
            }
            ArrayList alDifferent = new ArrayList();
            foreach (String sTmp in tsChaines)
            {
                if (!alDifferent.Contains(sTmp))
                    alDifferent.Add(sTmp);
            }
            return alDifferent.Count;
        }
        /*internal VisualisationMode.Format getFormat(Object value)
        {
            String sValue = value.ToString();
            if (sValue == "")
                return new VisualisationMode.Format(DefaultColor_);
            int nbCaracter = 0;
            if (bAnalyseContent_)
            {
                nbCaracter = nombreCaractere(sValue);
            }
            else if ((value.GetType() == typeof(int)) ||
                    (value.GetType() == typeof(Int16)) ||
                    (value.GetType() == typeof(Int32)) ||
                    (value.GetType()) == typeof(Int64))
            {
                nbCaracter = (int)value;
            }
            else if ((value.GetType() == typeof(double)) ||
                  (value.GetType() == typeof(Double)))
            {
                nbCaracter = (int)((Double)value);
            }
            else
            {
                nbCaracter = OverallTools.FonctionUtiles.nombreCaractere(sValue, Character_);
            }
            for (int i = 0; i < lesConditions_.Count; i++)
            {

                if (nbCaracter <= ((Condition)lesConditions_[i]).Value)
                {
                    if ((nbCaracter != ((Condition)lesConditions_[i]).Value) && (i > 0))
                    {
                        return new VisualisationMode.Format(((Condition)lesConditions_[i - 1]).NewColor);
                    }
                    else
                    {
                        return new VisualisationMode.Format(((Condition)lesConditions_[i]).NewColor);
                    }
                }
            }
            if (lesConditions_.Count == 0)
                return new VisualisationMode.Format(DefaultColor_);
            return new VisualisationMode.Format(((Condition)lesConditions_[lesConditions_.Count - 1]).NewColor);
        }*/
        public Color getColor(Object value)
        {
            String sValue = value.ToString();
            if (sValue == "")
                return DefaultColor_;
            int nbCaracter = 0;
            if (bAnalyseContent_)
            {
                nbCaracter = nombreCaractere(sValue);
            }
            else if ((value.GetType() == typeof(int)) ||
                    (value.GetType() == typeof(Int16)) ||
                    (value.GetType() == typeof(Int32)) ||
                    (value.GetType()) == typeof(Int64))
            {
                nbCaracter = (int)value;
            }
            else if ((value.GetType() == typeof(double)) ||
                  (value.GetType() == typeof(Double)))
            {
                nbCaracter = (int)((Double)value);
            }
            else
            {
                nbCaracter = OverallTools.FonctionUtiles.nombreCaractere(sValue, Character_);
            }
            // >> Task #12808 Pax2Sim - allocation Liege
            if (value.Equals(AllocationOutput.RESOURCE_CLOSED_IN_INTERVAL_MARKER))
            {
                return Color.Red;
            }
            // << Task #12808 Pax2Sim - allocation Liege
            for (int i = 0; i < lesConditions_.Count; i++)
            {

                if (nbCaracter <= ((Condition)lesConditions_[i]).Value)
                {
                    if ((nbCaracter != ((Condition)lesConditions_[i]).Value) && (i > 0))
                    {
                        return ((Condition)lesConditions_[i - 1]).NewColor;
                    }
                    else
                    {
                        return ((Condition)lesConditions_[i]).NewColor;
                    }
                }
            }
            if (lesConditions_.Count == 0)
                return DefaultColor_;
            return ((Condition)lesConditions_[lesConditions_.Count - 1]).NewColor;
        }

        /*public VisualisationMode.Format getFormat(int indexColumn, int indexRow)
        {
            return new VisualisationMode.Format(DefaultColor_);
        }*/
        public Color getColor(int indexColumn, int indexRow)
        {
            return DefaultColor_;
        }

        public VisualisationMode.ConditionnalFormat Clone()
        {
            return new ConditionnalFormatCharacter(this);
        }
        #endregion
    }
    #endregion

    #region La classe ConditionnalFormatErrors qui est l'implémentation de l'interface ConditionnalFormat
    /// <summary>
    /// Classe de mise en forme qui se base sur l'occurence d'un caractère.
    /// </summary>
    internal class ConditionnalFormatErrors : VisualisationMode.ConditionnalFormat
    {
        #region public Color DefaultColor
        private Color DefaultColor_;
        public Color DefaultColor
        {
            get
            {
                return DefaultColor_;
            }
            set
            {
                DefaultColor_ = value;
            }
        }
        #endregion

        #region Les constructeurs de la classe.
        public ConditionnalFormatErrors()
        {
            DefaultColor_ = Color.White;
            didicfInformations = null;
            /*htWarnings = null;
            htErrors = null;
            htSuffixe=null;
            htPrefixe = null;*/
        }
        private ConditionnalFormatErrors(ConditionnalFormatErrors AncienConditionnalFormat)
        {
            DefaultColor_ = AncienConditionnalFormat.DefaultColor;
            if (AncienConditionnalFormat.didicfInformations == null)
                return;
            didicfInformations = new Dictionary<int, Dictionary<int, CellFormat>>();
            foreach (int i in AncienConditionnalFormat.didicfInformations.Keys)
            {
                didicfInformations.Add(i, new Dictionary<int, CellFormat>());

                foreach (int j in AncienConditionnalFormat.didicfInformations[i].Keys)
                {
                    didicfInformations[i].Add(j, new CellFormat(AncienConditionnalFormat.didicfInformations[i][j]));
                }
            }
            /*
            htWarnings = null;
            htErrors = null;
            if (AncienConditionnalFormat.htWarnings != null)
            {
                htWarnings = new Dictionary<string,string>();
                foreach (String key in AncienConditionnalFormat.htWarnings.Keys)
                {
                    htWarnings.Add(key, AncienConditionnalFormat.htWarnings[key]);
                }
            }
            if (AncienConditionnalFormat.htErrors != null)
            {
                htErrors = new Hashtable();
                foreach (String key in AncienConditionnalFormat.htErrors.Keys)
                {
                    htErrors.Add(key, AncienConditionnalFormat.htErrors[key]);
                }
            }*/
        }
        #endregion

        #region Les fonctions et les variables pour stocker les couleurs de visualisation.

        private class CellFormat
        {
            internal int iColumn;
            internal int iRow;
            internal bool bColorDefined;
            internal Color cColor;
            internal String sMessage;
            internal String sSuffixe;
            internal String sPrefixe;

            internal CellFormat(int Column, int Row, Color Color)
            {
                iColumn = Column;
                iRow = Row;
                sMessage = "";
                cColor = Color;
                bColorDefined = true;
            }
            internal CellFormat(int Column, int Row, String Message, Color Color)
            {
                iColumn = Column;
                iRow = Row;
                bColorDefined = true;
                cColor = Color;
                sMessage = Message;
            }
            internal CellFormat(int Column, int Row, String Message)
            {
                iColumn = Column;
                iRow = Row;
                bColorDefined = false;
                sMessage = Message;
            }
            internal CellFormat(int Column, int Row, String Prefixe, String Suffixe)
            {
                iColumn = Column;
                iRow = Row;
                bColorDefined = false;
                sMessage = "";
                sSuffixe = Suffixe;
                sPrefixe = Prefixe;
            }

            public CellFormat(CellFormat cellFormat)
            {
                iColumn = cellFormat.iColumn;
                iRow = cellFormat.iRow;
                bColorDefined = cellFormat.bColorDefined;
                cColor = cellFormat.cColor;
                sMessage = cellFormat.sMessage;
                sSuffixe = cellFormat.sSuffixe;
                sPrefixe = cellFormat.sPrefixe;
            }
        }

        private Dictionary<int, Dictionary<int, CellFormat>> didicfInformations;
        [Obsolete ("Use didicfInformations instead")]
        private Dictionary<String,String> htWarnings;
        [Obsolete ("Use didicfInformations instead")]
        private Hashtable htErrors;
        [Obsolete ("Use didicfInformations instead")]
        private Dictionary<String, String> htSuffixe;
        [Obsolete ("Use didicfInformations instead")]
        private Dictionary<String, String> htPrefixe;

        public void setCondition(int iColumn, int iRow, Color cColor)
        {
            if (didicfInformations == null)
                didicfInformations = new Dictionary<int, Dictionary<int, CellFormat>>();

            if (!didicfInformations.ContainsKey(iColumn))
                didicfInformations.Add(iColumn, new Dictionary<int, CellFormat>());

            if (!didicfInformations[iColumn].ContainsKey(iRow))
                didicfInformations[iColumn].Add(iRow, new CellFormat(iColumn, iRow, cColor));
            else
                didicfInformations[iColumn][iRow].cColor = cColor;
            /*
            if (htWarnings == null)
                htWarnings = new Dictionary<String, String>();
            if (htErrors == null)
                htErrors = new Hashtable();
            String sKey = iColumn.ToString() + "_" + iRow.ToString();
            if (htErrors.ContainsKey(sKey))
            {
                htErrors[sKey] = cColor;
                return;
            }
            htErrors.Add(sKey, cColor);*/
        }

        public void setExceptionDiffCondition(int iColumn, int iRow, string message)    // >> Bug #14377 Tables Data check improvement
        {

            if (didicfInformations == null)
                didicfInformations = new Dictionary<int, Dictionary<int, CellFormat>>();

            if (!didicfInformations.ContainsKey(iColumn))
                didicfInformations.Add(iColumn, new Dictionary<int, CellFormat>());
            Color cColor = Color.Cyan;

            if (!didicfInformations[iColumn].ContainsKey(iRow))
                didicfInformations[iColumn].Add(iRow, new CellFormat(iColumn, iRow, cColor));
            else
                didicfInformations[iColumn][iRow].cColor = cColor;

            didicfInformations[iColumn][iRow].sMessage = message;
        }

        public void setCondition(int iColumn, int iRow, String sError, String sWarning)
        {

            if (didicfInformations == null)
                didicfInformations = new Dictionary<int, Dictionary<int, CellFormat>>();

            if (!didicfInformations.ContainsKey(iColumn))
                didicfInformations.Add(iColumn, new Dictionary<int, CellFormat>());
            Color cColor = Color.Red;
            if ((sWarning != null) && (sWarning != ""))
            {
                cColor = Color.Yellow;
                sError = sWarning;
            }

            if (!didicfInformations[iColumn].ContainsKey(iRow))
                didicfInformations[iColumn].Add(iRow, new CellFormat(iColumn, iRow, cColor));
            else
                didicfInformations[iColumn][iRow].cColor = cColor;

            didicfInformations[iColumn][iRow].sMessage = sError;


            /*if (htWarnings == null)
                htWarnings = new Dictionary<String, String>();
            if (htErrors == null)
                htErrors = new Hashtable();
            if ((sError == null) || (sError == ""))
            {
                if (htWarnings.ContainsKey(iColumn.ToString() + "_" + iRow.ToString()))
                    htWarnings[iColumn.ToString() + "_" + iRow.ToString()] = sWarning;
                else
                    htWarnings.Add(iColumn.ToString() + "_" + iRow.ToString(), sWarning);
            }
            else
            {
                if (htErrors.ContainsKey(iColumn.ToString() + "_" + iRow.ToString()))
                    htErrors[iColumn.ToString() + "_" + iRow.ToString()] = sError;
                else
                    htErrors.Add(iColumn.ToString() + "_" + iRow.ToString(), sError);
            }*/
        }
        public void setCondition(int iColumn, int iRow, String sError)
        {
            setCondition(iColumn, iRow, sError, "");
        }
        public void setFormat(int iColumn, int iRow, String sPrefixe, String sSuffixe)
        {

            if (didicfInformations == null)
                didicfInformations = new Dictionary<int, Dictionary<int, CellFormat>>();

            if (!didicfInformations.ContainsKey(iColumn))
                didicfInformations.Add(iColumn, new Dictionary<int, CellFormat>());

            if (!didicfInformations[iColumn].ContainsKey(iRow))
                didicfInformations[iColumn].Add(iRow, new CellFormat(iColumn, iRow, sPrefixe, sSuffixe));
            else
            {
                didicfInformations[iColumn][iRow].sPrefixe = sPrefixe;
                didicfInformations[iColumn][iRow].sSuffixe = sSuffixe;
            }



            /*

            if (htPrefixe == null)
                htPrefixe = new Dictionary<String, String>();
            if (htSuffixe == null)
                htSuffixe = new Dictionary<String, String>();
            if ((sPrefixe != null) && (sPrefixe != ""))
            {
                if (htPrefixe.ContainsKey(iColumn.ToString() + "_" + iRow.ToString()))
                    htPrefixe[iColumn.ToString() + "_" + iRow.ToString()] = sPrefixe;
                else
                    htPrefixe.Add(iColumn.ToString() + "_" + iRow.ToString(), sPrefixe);
            }
            if ((sSuffixe != null) && (sSuffixe != ""))
            {
                if (htSuffixe.ContainsKey(iColumn.ToString() + "_" + iRow.ToString()))
                    htSuffixe[iColumn.ToString() + "_" + iRow.ToString()] = sSuffixe;
                else
                    htSuffixe.Add(iColumn.ToString() + "_" + iRow.ToString(), sSuffixe);
            }*/
        }

        public int Errors
        {
            get
            {
                /*
                if (htErrors == null)
                    return 0;
                return htErrors.Count;*/
                if (didicfInformations == null)
                    return 0;
                int errNum = 0;
                foreach (int i in didicfInformations.Keys)
                {

                    foreach (int j in didicfInformations[i].Keys)
                    {
                        if (didicfInformations[i][j].cColor == Color.Red)
                            errNum++;
                    }
                }
                return errNum;
            }
        }
        public int Warnings
        {
            get
            {
                if (didicfInformations == null)
                    return 0;
                int errNum = 0;
                foreach (int i in didicfInformations.Keys)
                {

                    foreach (int j in didicfInformations[i].Keys)
                    {
                        if (didicfInformations[i][j].cColor == Color.Yellow)//Color.Orange) // >> Bug #14888 Pax Capacity launcher blocking Simulation for dummy reason
                            errNum++;
                    }
                }
                return errNum;
                /*if (htWarnings == null)
                    return 0;
                return htWarnings.Count;*/
            }
        }

        public int exceptionDiffWarning
        {
            get
            {
                int warningsNb = 0;
                if (didicfInformations == null)
                    return warningsNb;                
                foreach (int i in didicfInformations.Keys)
                {
                    foreach (int j in didicfInformations[i].Keys)
                    {
                        if (didicfInformations[i][j].cColor == Color.Cyan)
                            warningsNb++;
                    }
                }
                return warningsNb;
            }
        }
        #endregion

        #region Les trois fonctons membres de l'interface.

        public Color getColor(Object value)
        {
            return DefaultColor_;
        }

        /*public VisualisationMode.Format getFormat(Object value)
        {
            return new VisualisationMode.Format(DefaultColor_);
        }*/
        /*public VisualisationMode.Format getFormat(int indexColumn, int indexRow)
        {
            if (htErrors == null)
                return new VisualisationMode.Format(DefaultColor_);
            String sKey = indexColumn.ToString() + "_" + indexRow.ToString();
            if (htErrors.Contains(sKey))
            {
                if (htErrors[sKey].GetType() == typeof(Color))
                    return new VisualisationMode.Format((Color)htErrors[sKey]);
                return new VisualisationMode.Format(Color.Red);

            }
            if (htWarnings.Contains(sKey))
            {
                return new VisualisationMode.Format(Color.Yellow);
            }
            return new VisualisationMode.Format(DefaultColor_);
        }*/
        
        public Color getColor(int indexColumn, int indexRow)
        {
            if (didicfInformations == null)
                return DefaultColor_;
            if (!didicfInformations.ContainsKey(indexColumn))
                return DefaultColor_;
            if (!didicfInformations[indexColumn].ContainsKey(indexRow))
                return DefaultColor_;
            if (!didicfInformations[indexColumn][indexRow].bColorDefined)
                return DefaultColor_;
            return didicfInformations[indexColumn][indexRow].cColor;
            /*
            if (htErrors == null)
                return DefaultColor_;
            String sKey = indexColumn.ToString() + "_" + indexRow.ToString();
            if (htErrors.Contains(sKey))
            {
                if (htErrors[sKey].GetType() == typeof(Color))
                    return (Color)htErrors[sKey];
                return Color.Red;

            }
            if (htWarnings.ContainsKey(sKey))
            {
                return Color.Yellow;
            }
            return DefaultColor_;*/
        }

        public String getError(int indexColumn, int indexRow)
        {
            if (didicfInformations == null)
                return "";
            if (!didicfInformations.ContainsKey(indexColumn))
                return "";
            if (!didicfInformations[indexColumn].ContainsKey(indexRow))
                return "";
            return didicfInformations[indexColumn][indexRow].sMessage;
            /*
            if (htErrors == null)
                return "";
            String sKey = indexColumn.ToString() + "_" + indexRow.ToString();
            if (htErrors.Contains(sKey))
            {
                if (htErrors[sKey].GetType() == typeof(Color))
                    return "";
                return (String)htErrors[sKey];
            }
            if (htWarnings.ContainsKey(sKey))
            {
                return (String)htWarnings[sKey];
            }
            return "";*/
        }

        public VisualisationMode.ConditionnalFormat Clone()
        {
            return new ConditionnalFormatErrors(this);
        }
        #endregion

        internal string getSuffixe(int indexColumn, int indexRow)
        {
            if (didicfInformations == null)
                return "";
            if (!didicfInformations.ContainsKey(indexColumn))
                return "";
            if (!didicfInformations[indexColumn].ContainsKey(indexRow))
                return "";
            return didicfInformations[indexColumn][indexRow].sSuffixe;

            /*
            if (htSuffixe == null)
                return "";
            String sKey = indexColumn.ToString() + "_" + indexRow.ToString();
            if (htSuffixe.Contains(sKey))
            {
                return htSuffixe[sKey].ToString();
            }
            return "";*/
        }

        internal string getPrefixe(int indexColumn, int indexRow)
        {
            if (didicfInformations == null)
                return "";
            if (!didicfInformations.ContainsKey(indexColumn))
                return "";
            if (!didicfInformations[indexColumn].ContainsKey(indexRow))
                return "";
            return didicfInformations[indexColumn][indexRow].sPrefixe;

            /*
            if (htPrefixe == null)
                return "";
            String sKey = indexColumn.ToString() + "_" + indexRow.ToString();
            if (htPrefixe.Contains(sKey))
            {
                return htPrefixe[sKey].ToString();
            }
            return "";*/
        }

        internal void RemoveColumn(int iIndex)
        {
            if (didicfInformations == null)
                return;
            if (didicfInformations.ContainsKey(iIndex))
                didicfInformations.Remove(iIndex);
            List<Int32> liIndexToMove = new List<int>();
            foreach (int iKey in didicfInformations.Keys)
            {
                if (iKey < iIndex)
                    continue;
                liIndexToMove.Add(iKey);
            }
            liIndexToMove.Sort();
            foreach(int iKey in liIndexToMove)
            {

                didicfInformations.Add(iKey - 1, new Dictionary<int, CellFormat>());
                foreach (int j in didicfInformations[iKey].Keys)
                {
                    didicfInformations[iKey][j].iColumn--;
                    didicfInformations[iKey - 1].Add(j, didicfInformations[iKey][j]);
                }
                didicfInformations.Remove(iKey);
            }

        }
        internal void RemoveColumn(int iIndex, int iNumberColumn)
        {
            for (int i = 0; i < iNumberColumn; i++)
            {
                RemoveColumn(i);
            }
        }
    }
    #endregion

    #region La classe ConditionnalFormatValue qui est l'implémentation de l'interface ConditionnalFormat
    /// <summary>
    /// Classe de mise en forme qui se base sur l'occurence d'un caractère.
    /// </summary>
    class ConditionnalFormatValue : VisualisationMode.ConditionnalFormat
    {

        //static List<ConditionnalFormatValue> lDefinedEntities = null;
        //private static void AddEntities(ConditionnalFormatValue cfcError)
        //{
        //    if (lDefinedEntities == null)
        //        lDefinedEntities = new List<ConditionnalFormatValue>();
        //    lDefinedEntities.Add(cfcError);
        //}
        #region public Color DefaultColor
        private Color DefaultColor_;
        public Color DefaultColor
        {
            get
            {
                return DefaultColor_;
            }
            set
            {
                DefaultColor_ = value;
            }
        }
        #endregion

        #region Les constructeurs de la classe.
        public ConditionnalFormatValue()
        {
            //AddEntities(this);
            DefaultColor_ = Color.White;
            lesConditions_ = null;
        }
        private ConditionnalFormatValue(ConditionnalFormatValue AncienConditionnalFormat)
        {
            //AddEntities(this);
            DefaultColor_ = AncienConditionnalFormat.DefaultColor;
            lesConditions_ = null;
            if (AncienConditionnalFormat.lesConditions_ != null)
            {
                lesConditions_ = new ArrayList();
                foreach (Condition cond in AncienConditionnalFormat.lesConditions_)
                {
                    lesConditions_.Add(new Condition(cond));
                }
            }
        }
        #endregion

        #region Les fonctions et les variables pour stocker les couleurs de visualisation.
        private ArrayList lesConditions_;
        class Condition
        {
            public Double Value;
            public Color NewColor;
            public Condition(Double Value_, Color NewColor_)
            {
                Value = Value_;
                NewColor = NewColor_;
            }
            public Condition(Condition oldValue)
            {
                Value = oldValue.Value;
                NewColor = oldValue.NewColor;
            }
        }
        class ConditionComparer : IComparer
        {
            public int Compare(Object x, Object y)
            {
                return (int)(((Condition)x).Value - ((Condition)y).Value);
            }
        }
        public void setCondition(Double Value, Color NewColor)
        {
            if (lesConditions_ == null)
                lesConditions_ = new ArrayList();
            lesConditions_.Add(new Condition(Value, NewColor));
            lesConditions_.Sort(new ConditionComparer());
        }
        #endregion

        #region Les trois fonctons membres de l'interface.

        public Color getColor(Object value)
        {
            Double dValue;
            if ((value.GetType() == typeof(int)) ||
                (value.GetType() == typeof(Int16)) ||
                (value.GetType() == typeof(Int32)) ||
                (value.GetType()) == typeof(Int64))
            {
                dValue = (int)value;
            }
            else if ((value.GetType() == typeof(double)) ||
                    (value.GetType() == typeof(Double)))
            {
                dValue = (int)((Double)value);
            }
            else
            {
                return DefaultColor_;
            }
            if (lesConditions_.Count == 0)
                return DefaultColor_;
            for (int i = 0; i < lesConditions_.Count; i++)
            {

                if (dValue <= ((Condition)lesConditions_[i]).Value)
                {
                    if ((dValue != ((Condition)lesConditions_[i]).Value) && (i > 0))
                    {
                        return ((Condition)lesConditions_[i - 1]).NewColor;
                    }
                    else
                    {
                        return ((Condition)lesConditions_[i]).NewColor;
                    }
                }
            }
            return ((Condition)lesConditions_[lesConditions_.Count - 1]).NewColor;
        }

        public Color getColor(int indexColumn, int indexRow)
        {
            return DefaultColor_;
        }

        public VisualisationMode.ConditionnalFormat Clone()
        {
            return new ConditionnalFormatValue(this);
        }
        #endregion
    }
    #endregion

    #region La classe ConditionnalFormatLine qui est l'implémentation de l'interface ConditionnalFormat
    /// <summary>
    /// Classe de mise en forme d'une ligne entière.
    /// </summary>
    public class ConditionnalFormatLine : VisualisationMode.ConditionnalFormat
    {
        //static List<ConditionnalFormatLine> lDefinedEntities = null;
        //private static void AddEntities(ConditionnalFormatLine cfcError)
        //{
        //    if (lDefinedEntities == null)
        //        lDefinedEntities = new List<ConditionnalFormatLine>();
        //    lDefinedEntities.Add(cfcError);
        //}
        private int iRowNumber;
        private Color cFirstColor;
        private Color cSecondColor;
        #region public Color DefaultColor
        private Color DefaultColor_;
        public Color DefaultColor
        {
            get
            {
                return DefaultColor_;
            }
            set
            {
                DefaultColor_ = value;
            }
        }
        #endregion

        #region Les constructeurs de la classe.
        public ConditionnalFormatLine()
        {
            //AddEntities(this);
            DefaultColor_ = Color.White;
            lesConditions_ = null;
            iRowNumber = 0;
            cFirstColor = Color.White;
            cSecondColor = Color.White;
        }

        public ConditionnalFormatLine(int RowNumber, Color FirstColor, Color SecondColor)
        {
            //AddEntities(this);
            DefaultColor_ = Color.White;
            lesConditions_ = null;
            iRowNumber = RowNumber;
            cFirstColor = FirstColor;
            cSecondColor = SecondColor;
        }
        private ConditionnalFormatLine(ConditionnalFormatLine AncienConditionnalFormat)
        {
            //AddEntities(this);
            DefaultColor_ = AncienConditionnalFormat.DefaultColor;
            lesConditions_ = null;
            if (AncienConditionnalFormat.lesConditions_ != null)
            {
                lesConditions_ = new Hashtable();
                foreach (int key in AncienConditionnalFormat.lesConditions_.Keys)
                {
                    lesConditions_.Add(key, AncienConditionnalFormat.lesConditions_[key]);
                }
            }
            iRowNumber = AncienConditionnalFormat.iRowNumber;
            cFirstColor = AncienConditionnalFormat.cFirstColor;
            cSecondColor = AncienConditionnalFormat.cSecondColor;
        }
        #endregion

        #region Les fonctions et les variables pour stocker les couleurs de visualisation.
        private Hashtable lesConditions_;

        public void setCondition(int iRow, Color Couleur)
        {
            if (lesConditions_ == null)
                lesConditions_ = new Hashtable();
            if (lesConditions_.ContainsKey(iRow))
                lesConditions_[iRow] = Couleur;
            else
                lesConditions_.Add(iRow, Couleur);
        }
        public int countConditions()
        {
            if (lesConditions_ == null)
                return 0;
            return lesConditions_.Count;
        }
        #endregion

        #region Les trois fonctons membres de l'interface.

        public Color getColor(Object value)
        {
            return DefaultColor_;
        }

        public Color getColor(int indexColumn, int indexRow)
        {
            if ((lesConditions_ != null) && (lesConditions_.Contains(indexRow)))
            {
                return (Color)lesConditions_[indexRow];
            }
            else if (iRowNumber == 0)
            {
                return DefaultColor_;
            }
            int iNumber = 0;
            if (lesConditions_ != null)
                iNumber = lesConditions_.Count;

            if (((indexRow - iNumber) / iRowNumber) % 2 == 0)
                return cFirstColor;
            return cSecondColor;
        }
        public String getError(int indexColumn, int indexRow)
        {
            return OverallTools.FonctionUtiles.toHexa(getColor(indexColumn, indexRow));
        }

        public VisualisationMode.ConditionnalFormat Clone()
        {
            return new ConditionnalFormatLine(this);
        }
        #endregion
    }
    #endregion
    #endregion
}
