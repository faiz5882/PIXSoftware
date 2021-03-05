using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.Assistant
{
    public partial class SIM_Flight_Parameters : Form
    {
        List<String> visibleList;

        internal List<String> VisibleList
        {
            get
            {
                return visibleList;
            }
            set
            {
                visibleList = value;
            }
        } 

        public SIM_Flight_Parameters(String ParameterType, String[] State, List<String> ListParameters)
        {
            int i, j; 
            initialize();
            bCancelChanged = false;
            visibleList = new List<String>();

            if (State != null)  // >> Task #10764 Pax2Sim - new User attributes for Groups
            {
                for (i = 0; i < State.Length; i++)
                    lst_Visible.Items.Add(State[i]);
            }

            for (i = 0; i < ListParameters.Count; i++)
            {
                if ((State == null) || (State.Length == 0))
                {
                    lst_Available.Items.Add(ListParameters[i]);
                    continue;
                }


                for (j = 0; j < State.Length; j++)
                {
                    if (State[j] != ListParameters[i].ToString())
                    {
                        if (!lst_Available.Items.Contains(ListParameters[i]))   // >> Task #10764 Pax2Sim - new User attributes for Groups
                            lst_Available.Items.Add(ListParameters[i]);
                    }
                }
            }

            for (i = 0; i < State.Length; i++)
            {
                if (lst_Available.Items.Contains(State[i]))
                    lst_Available.Items.RemoveAt(lst_Available.Items.IndexOf(State[i]));
            }
        }

        private void initialize()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }
     

        /// <summary>
        /// Fonction pour récuperer une valeur dans la colonne "outFPcolumn" à la première 
        /// ligne où la colonne "inFPColumn" à la valeur "value".
        /// </summary>
        /// <param name="dtFPDTable"></param>
        /// <param name="dtFPATable"></param>
        /// <param name="inFPColumn"></param>
        /// <param name="outFPcolumn"></param>
        /// <returns></returns>
        private static object GetValueInFor(DataTable dtFPDTable, DataTable dtFPATable, String inFPColumn, String outFPcolumn, object value)
        {
            object objFound = null;
            for (int i = 0; i<2; i++)
            {
                DataTable dtTable = i==0 ? dtFPDTable : dtFPATable;

                // retrouver le numero de la colonne correspondant à "inFPColumn"
                int iInFPColumn = -1;
                foreach (DataColumn dcColumn in dtTable.Columns)
                    if (dcColumn.ColumnName == inFPColumn)
                        iInFPColumn = dtTable.Columns.IndexOf(dcColumn);
                if(iInFPColumn == -1)
                    break;

                // retrouver l'index de la ligne en question
                int index = OverallTools.DataFunctions.indexLigne(dtTable, iInFPColumn, value.ToString());
                if (index == -1)
                    break;

                // puis aller chercher la valeur de la colonne desiré
                objFound = dtTable.Rows[index][outFPcolumn];
            }
            return objFound;
        }

        #region Mouvement des Elements
        private void bt_right_Click(object sender, EventArgs e)
        {
            MoveItemTo(lst_Available, lst_Visible);
        }

        private void bt_left_Click(object sender, EventArgs e)
        {
            if (lst_Visible.SelectedIndex != -1)
                MoveItemTo( lst_Visible,lst_Available);
        }

        private static void MoveItemTo(ListBox lstSource, ListBox lstDestination)
        {
            lstDestination.Sorted = true;
            if (lstSource.SelectedItems.Count == 0)
                return;
            int i;
            System.Collections.ArrayList lstSelected = new System.Collections.ArrayList();
            if (lstDestination.Items.Contains(""))
                lstDestination.Items.RemoveAt(0);
            for (i = 0; i < lstSource.SelectedItems.Count; i++)
            {
                lstDestination.Items.Add(lstSource.SelectedItems[i]);
                lstSelected.Add(lstSource.SelectedItems[i]);
            }
            int iIndex = lstSource.Items.IndexOf(lstSelected[0]);
            foreach(Object sKey in lstSelected)
            {
                lstSource.Items.Remove(sKey);
            }
            if(lstSource.Items.Count>0)
            {
                if(lstSource.Items.Count<= iIndex)
                    lstSource.SelectedIndex = lstSource.Items.Count -1;
                else
                    lstSource.SelectedIndex = iIndex;
            }
        }
        #endregion

        private static void InitializeAvailableInformations(ListBox lstToFill, 
            ListBox lstSecondList,
            DataTable dtOriginTable, 
            bool bColumnExceptions,
            String sPrefixe)
        {
            lstToFill.Sorted = true;
            if (dtOriginTable == null)
                return;
            if (bColumnExceptions)
            {
                foreach (DataColumn dcColumn in dtOriginTable.Columns)
                {
                    if (OverallTools.FonctionUtiles.estPresentDansListe(dcColumn.ColumnName, GestionDonneesHUB2SIM.ListeEnteteDiv))
                        continue;
                    if ((lstSecondList != null) && (lstSecondList.Items.Contains(sPrefixe + dcColumn.ColumnName)))
                        continue;
                    lstToFill.Items.Add(sPrefixe + dcColumn.ColumnName);
                }
            }
            else
            {
                foreach (DataRow dcRow in dtOriginTable.Rows)
                {
                    if ((lstSecondList != null) && (lstSecondList.Items.Contains(sPrefixe + dcRow[0].ToString())))
                        continue;
                    if (!lstToFill.Items.Contains(sPrefixe + dcRow[0].ToString()))
                        lstToFill.Items.Add(sPrefixe + dcRow[0].ToString());
                }
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lst_Visible.Items.Count; i++)
                visibleList.Add(lst_Visible.Items[i].ToString());
        }

        bool bCancelChanged;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (bCancelChanged)
                return;
            bCancelChanged = true;
            if (checkBox1.CheckState == CheckState.Indeterminate)
                return;
            if (lst_Visible.SelectedIndices.Count == 0)
                return;
            List<int> liIndexes= new List<int>();
            for (int i = 0; i < lst_Visible.SelectedIndices.Count; i++)
            {
                liIndexes.Add(lst_Visible.SelectedIndices[i]);
            }
            
            
            int j;

            ArrayList lstSelected = new ArrayList();
            for (int i = 0; i < lst_Visible.SelectedItems.Count; i++)
                lstSelected.Add(lst_Visible.SelectedItems[i]);
            for (int i = 0; i < lstSelected.Count; i++)
            {
                for (j = 0; j < lst_Visible.Items.Count; j++)
                {
                    if (lst_Visible.Items[j] == lstSelected[i])
                    {
                        if ((checkBox1.Checked) && (!lst_Visible.Items[j].ToString().StartsWith("!")))
                        {
                            lst_Visible.Items[j] = "!" + lst_Visible.Items[j];
                        }else if((!checkBox1.Checked)&& (lst_Visible.Items[j].ToString().StartsWith("!")))
                            lst_Visible.Items[j] = lst_Visible.Items[j].ToString().Remove(0, 1);
                    }
                }
            }
            foreach (int k in liIndexes)
                lst_Visible.SetSelected(k, true);
            bCancelChanged = false;
        }

        private void lst_Visible_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCancelChanged)
                return;
            bCancelChanged = true;
            bool bFindNotBanned = false;
            bool bFindBanned = false;
            for (int i = 0; i < lst_Visible.SelectedItems.Count; i++)
            {
                if (lst_Visible.SelectedItems[i].ToString().StartsWith("!"))
                    bFindBanned = true;
                else
                    bFindNotBanned = true;
                if (bFindBanned && bFindNotBanned)
                    break;
            }
            if (bFindBanned && bFindNotBanned)
                checkBox1.CheckState = CheckState.Indeterminate;
            else
                checkBox1.Checked = bFindBanned;
            bCancelChanged = false;
        }
    }
}
