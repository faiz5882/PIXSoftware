using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_ColumnPosition : Form
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

        /// <summary>
        ///  Liste des colonnes affichées n'incluant pas les colonnes de time
        /// </summary>
        private ArrayList ColumnlistNames;
        /// <summary>
        /// Liste de toutes les colonnes
        /// </summary>
        private ArrayList ColumnNames;
        private ArrayList OriginColumn;
        private ArrayList laRepresentation;
        private ArrayList lAxeDeRepresentation;
        private ArrayList lesCouleursTour;
        private ArrayList lesCouleursRemplissage;
        private ArrayList lesAccumulations;
        private ArrayList lesPositions;
        private ArrayList lesShowValues;        
        private ArrayList MaxCandleValue;
        private ArrayList MidCandleValue;
        private ArrayList MinCandleValue;

        #region Setters and getters
        public ArrayList ColumnNames_
        {
            get
            {
                return ColumnNames;
            }
            set
            {
                ColumnNames = value;
            }
        }
        public ArrayList OriginColumn_
        {
            get
            {
                return OriginColumn;
            }
            set
            {
                OriginColumn = value;
            }
        }
        public ArrayList laRepresentation_
        {
            get
            {
                return laRepresentation;
            }
            set
            {
                laRepresentation = value;
            }
        }
        public ArrayList lAxeDeRepresentation_
        {
            get
            {
                return lAxeDeRepresentation;
            }
            set
            {
                lAxeDeRepresentation = value;
            }
        }
        public ArrayList lesCouleursTour_
        {
            get
            {
                return lesCouleursTour;
            }
            set
            {
                lesCouleursTour = value;
            }
        }
        public ArrayList lesCouleursRemplissage_
        {
            get
            {
                return lesCouleursRemplissage;
            }
            set
            {
                lesCouleursRemplissage = value;
            }
        }
        public ArrayList lesAccumulations_
        {
            get
            {
                return lesAccumulations;
            }
            set
            {
                lesAccumulations = value;
            }
        }
        public ArrayList lesPositions_
        {
            get
            {
                return lesPositions;
            }
            set
            {
                lesPositions = value;
            }
        }
        public ArrayList lesShowValues_
        {
            get
            {
                return lesShowValues;
            }
            set
            {
                lesShowValues = value;
            }
        }
        public ArrayList MaxCandleValue_
        {
            get
            {
                return MaxCandleValue;
            }
            set
            {
                MaxCandleValue = value;
            }
        }
        public ArrayList MidCandleValue_
        {
            get
            {
                return MidCandleValue;
            }
            set
            {
                MidCandleValue = value;
            }
        }
        public ArrayList MinCandleValue_
        {
            get
            {
                return MinCandleValue;
            }
            set
            {
                MinCandleValue = value;
            }
        }
        #endregion


        /// <summary>
        /// Constructeur de la classe, on créé une nouvelle liste
        /// </summary>
        public SIM_ColumnPosition()
        {
            InitializeComponent();
            ColumnlistNames = new ArrayList();
        }

        /// <summary>
        /// Methode permettant l'initialisation de la fenetre, toutes les listes permettant de récupérer les caracteristiques des courbes
        /// </summary>
        /// <param name="ColumnNames_"> Liste contenant les noms de colonne</param>
        /// <param name="OriginColumn_">Liste contenant les noms d'origine des colonnes</param>
        /// <param name="laRepresentation_">Liste contentant les type de courbe</param>
        /// <param name="lAxeDeRepresentation_">Liste contentant les axes d'affichage des colonnes</param>
        /// <param name="lesCouleursTour_">Liste contentant les couleurs de contour des colonnes</param>
        /// <param name="lesCouleursRemplissage_">Liste contentant les couleurs de remplissage des colonnes</param>
        /// <param name="lesAccumulations_">Liste contenant des booleens indiquant si l'accumulation des colonnes est affichée </param>
        /// <param name="lesPositions_">Liste contentant les positions des colonnes</param>
        /// <param name="lesShowValues_">Liste contentant si les valeurs des différentes valeurs des colonnes sont affichées</param>
        /// <param name="MaxCandleValue_">Liste contentant les valeurs max des candle si les colonnes sont de types candle</param>
        /// <param name="MidCandleValue_">Liste contentant les valeurs mid des candle si les colonnes sont de types candl</param>
        /// <param name="MinCandleValue_">Liste contentant les valeurs min des candle si les colonnes sont de types candl</param>
        internal void initializeWindow(ArrayList ColumnNames_, 
            ArrayList OriginColumn_, 
            ArrayList laRepresentation_, 
            ArrayList lAxeDeRepresentation_, 
            ArrayList lesCouleursTour_,
            ArrayList lesCouleursRemplissage_, 
            ArrayList lesAccumulations_, 
            ArrayList lesPositions_, 
            ArrayList lesShowValues_,            
            ArrayList MaxCandleValue_, 
            ArrayList MidCandleValue_, 
            ArrayList MinCandleValue_)
        {
            clb_ColumnPosition.Items.Clear();
            ColumnlistNames.Clear();

            ColumnNames = (ArrayList)ColumnNames_.Clone();
            OriginColumn = (ArrayList)OriginColumn_.Clone();
            laRepresentation = (ArrayList)laRepresentation_.Clone();
            lAxeDeRepresentation = (ArrayList)lAxeDeRepresentation_.Clone();
            lesCouleursTour = (ArrayList)lesCouleursTour_.Clone();
            lesCouleursRemplissage = (ArrayList)lesCouleursRemplissage_.Clone();
            lesAccumulations = (ArrayList)lesAccumulations_.Clone();
            lesPositions = (ArrayList)lesPositions_.Clone();
            lesShowValues = (ArrayList)lesShowValues_.Clone();            
            MaxCandleValue = (ArrayList)MaxCandleValue_.Clone();
            MidCandleValue = (ArrayList)MidCandleValue_.Clone();
            MinCandleValue = (ArrayList)MinCandleValue_.Clone();

            for (int i = 0; i < ColumnNames.Count; i++)
            {
                if (lAxeDeRepresentation[i].ToString() == "X")
                    continue;
                clb_ColumnPosition.Items.Add(new ListeItem(ColumnNames[i].ToString() + "   ( " + lAxeDeRepresentation[i].ToString() + " )"));
                ColumnlistNames.Add(ColumnNames[i].ToString());
            }
            OverallTools.FonctionUtiles.MajBackground(this);
        }


        /// <summary>
        /// Monter une colonne dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Raise_Click(object sender, EventArgs e)
        {
            if (clb_ColumnPosition.SelectedIndex == -1)
                return;

            ///On récupère l'index de la colonne
            int index = indexColumn(ColumnlistNames[clb_ColumnPosition.SelectedIndex].ToString());
            if (index == 0)
                return;
            object tmpItem;

            ///Si la colonne selectionnée est de type Candle et que la colonne du dessus n'est pas de ce type, alors on affiche un message de restriction
            if (laRepresentation[index].ToString() == "Candle" && laRepresentation[index-1].ToString() != "Candle")
            {
                MessageBox.Show("Unable to change candles position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ///Sinon on déplace la colonne
            if (clb_ColumnPosition.SelectedItem != null && clb_ColumnPosition.SelectedIndex > 0)
            {
                tmpItem = ColumnlistNames[clb_ColumnPosition.SelectedIndex];
                ColumnlistNames[clb_ColumnPosition.SelectedIndex] = ColumnlistNames[clb_ColumnPosition.SelectedIndex - 1];
                ColumnlistNames[clb_ColumnPosition.SelectedIndex - 1] = tmpItem;

                tmpItem = ColumnNames[index];
                ColumnNames[index] = ColumnNames[index - 1];
                ColumnNames[index - 1] = tmpItem;

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
                                
                tmpItem = lesPositions[index];
                lesPositions[index] = lesPositions[index - 1];
                lesPositions[index - 1] = tmpItem;

                tmpItem = MaxCandleValue[index];
                MaxCandleValue[index] = MaxCandleValue[index - 1];
                MaxCandleValue[index - 1] = tmpItem;

                tmpItem = MidCandleValue[index];
                MidCandleValue[index] = MidCandleValue[index - 1];
                MidCandleValue[index - 1] = tmpItem;

                tmpItem = MinCandleValue[index];
                MinCandleValue[index] = MinCandleValue[index - 1];
                MinCandleValue[index - 1] = tmpItem;


                tmpItem = clb_ColumnPosition.SelectedItem;
                clb_ColumnPosition.Items[clb_ColumnPosition.SelectedIndex] = clb_ColumnPosition.Items[clb_ColumnPosition.SelectedIndex - 1];
                clb_ColumnPosition.Items[clb_ColumnPosition.SelectedIndex - 1] = tmpItem;
                clb_ColumnPosition.SelectedItem = clb_ColumnPosition.Items[clb_ColumnPosition.SelectedIndex - 1];
                
            }

        }

        /// <summary>
        /// Abaisser une colonne dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Lower_Click(object sender, EventArgs e)
        {
            if (clb_ColumnPosition.SelectedIndex == -1)
                return;

            int index = indexColumn(ColumnlistNames[clb_ColumnPosition.SelectedIndex].ToString());
            object tmpItem;
            if (ColumnlistNames.Count == 1)
                return;
            if (index >= ColumnlistNames.Count-1)
                return;
            ///Si la colonne selectionnée n'est pas de type Candle et que la colonne du dessous est pas de type candle, alors on prévient qu'il est impossible de changer de position
            if (laRepresentation[index + 1].ToString() == "Candle" && laRepresentation[index].ToString() != "Candle")
            {
                MessageBox.Show("Unable to change candles position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);             
                return;
            }

            if (clb_ColumnPosition.SelectedItem != null && clb_ColumnPosition.Items.Count > clb_ColumnPosition.SelectedIndex + 1)
            {
                tmpItem = ColumnlistNames[clb_ColumnPosition.SelectedIndex + 1];
                ColumnlistNames[clb_ColumnPosition.SelectedIndex + 1] = ColumnlistNames[clb_ColumnPosition.SelectedIndex];
                ColumnlistNames[clb_ColumnPosition.SelectedIndex] = tmpItem;

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

                tmpItem = clb_ColumnPosition.Items[clb_ColumnPosition.SelectedIndex + 1];
                clb_ColumnPosition.Items[clb_ColumnPosition.SelectedIndex + 1] = clb_ColumnPosition.SelectedItem;
                clb_ColumnPosition.Items[clb_ColumnPosition.SelectedIndex] = tmpItem;
                clb_ColumnPosition.SelectedItem = clb_ColumnPosition.Items[clb_ColumnPosition.SelectedIndex + 1];

            }
        }

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
        #endregion

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
