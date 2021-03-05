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
    public partial class Schedule_Assistant : Form
    {
        DataTable AllocationTable;
        GestionDonneesHUB2SIM gdhData_;
        String[] tsTablesNames_;
        GroupAllocation[] lesAllocations;

        #region Fonctions pour l'initialisation et la construction de la classe
        private void Initialize(DataTable AllocationTable_, String[] tsTablesNames, GestionDonneesHUB2SIM gdhData)
        {
            InitializeComponent();
            this.dtp_End.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
            this.dtp_Start.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
            OverallTools.FonctionUtiles.MajBackground(this);
            this.Text = "Change the data on " + AllocationTable_.TableName;
            AllocationTable = AllocationTable_;
            gdhData_ = gdhData;
            tsTablesNames_ = tsTablesNames;

            lst_Groups.BackColor = Color.White;

            for (int i = 0; i < AllocationTable_.Columns.Count;i++ )
            {
                if (tsTablesNames[i] != "")
                {
                    lst_Groups.Items.Add(AllocationTable_.Columns[i].ColumnName);
                }
            }
            ActiveDesactiveControl(false);
            DataTable table = new DataTable();
            table.Columns.Add("Begin", typeof(DateTime));
            table.Columns.Add("End", typeof(DateTime));
            table.Columns.Add("Used Table", typeof(String));

            dtp_Start.Value = OverallTools.DataFunctions.EraseSeconds(DateTime.Now);
            dtp_End.Value = dtp_Start.Value.AddMinutes(30);

            dataGridView1.DataSource = table;

            InitializeAllocation(AllocationTable);
        }

        private void InitializeAllocation(DataTable laTable)
        {
            if (laTable == null)
                return;
            if (laTable.Columns.Count == 0)
                return;
            int iIndexBegin = laTable.Columns.IndexOf("Begin");
            int iIndexEnd = laTable.Columns.IndexOf("End");
            if (iIndexBegin == -1)
                return;
            if (iIndexEnd == -1)
                return;
            lesAllocations = new GroupAllocation[lst_Groups.Items.Count];

            int i;
            for (i = 2; i < laTable.Columns.Count; i++)
            {
                lesAllocations[i-2] = new GroupAllocation(laTable.Columns[i].ColumnName);
            }

            if (laTable.Rows.Count == 0)
                return;
            /*
            for (i = 0; i < lesAllocations.Length; i++)
            {
                lesAllocations[i].AddAllocation(dtp_Start.Value, dtp_Start.Value, tsTablesNames_[i+2]);
            }
            DateTime dtEndTime = dtp_Start.Value;*/
            foreach (DataRow ligne in laTable.Rows)
            {
                for (i = 2; i < laTable.Columns.Count; i++)
                {
                    lesAllocations[i - 2].AddAllocation((DateTime)ligne[0], (DateTime)ligne[1], (String)ligne[i]);
                }
            }
        }

        public Schedule_Assistant(DataTable AllocationTable_,String[]tsTablesNames, GestionDonneesHUB2SIM gdhData)
        {
            Initialize(AllocationTable_,tsTablesNames, gdhData);
        }
        private void ActiveDesactiveControl(bool value)
        {
            lbl_Begin.Enabled = value;
            lbl_End.Enabled = value;
            dtp_End.Enabled = value;
            dtp_Start.Enabled = value;
            btn_Add.Enabled = value;
        }
        #endregion

        #region la class GroupAllocation
        class GroupAllocation
        {
            String GroupName;
            ArrayList StockageListe;
            AllocationStructure AncienneValeur;

            #region Structure de stockage
            class AllocationStructure
            {
                public DateTime dt_Debut;
                public DateTime dt_End;
                public String sTableName;
                public AllocationStructure(DateTime dt_Debut_, DateTime dt_End_, String  sTableName_)
                {
                    dt_Debut = dt_Debut_;
                    dt_End = dt_End_;
                    sTableName = sTableName_;
                }
                public bool isIdentique(DateTime dt_Debut_, DateTime dt_End_, String sTableName_)
                {
                    return (dt_Debut == dt_Debut_) && (dt_End == dt_End_) &&  (sTableName == sTableName_);
                }
            }
            #endregion

            public GroupAllocation(String GroupName_)
            {
                GroupName = GroupName_;
                StockageListe = new ArrayList();
                AncienneValeur = null;
            }

            #region Fonctions pour l'ajout de nouvelles allocation
            public int AddAllocation(DateTime dtBegin, String sTableName)
            {
                int i = 0;
                if (AncienneValeur != null)
                {
                    i = AddAllocation(new AllocationStructure(AncienneValeur.dt_Debut, dtBegin, AncienneValeur.sTableName));
                }
                AncienneValeur = new AllocationStructure(dtBegin, DateTime.Now,  sTableName);
                return i;
            }

            public int AddAllocation(DateTime dtBegin, DateTime dtEnd, String sTableName)
            {
                if (dtBegin == dtEnd)
                    return 0;
                return AddAllocation(new AllocationStructure(dtBegin, dtEnd, sTableName));
            }
            public void AddAllocation(DateTime dtBegin, DateTime dtEnd)
            {
                //Il faut vérifier le contenu de la table et supprimer tout ce qui n'est pas dans le moule.
                for (int i = 0; i < StockageListe.Count; i++)
                {
                    AllocationStructure alloc = (AllocationStructure)StockageListe[i];
                    if ((alloc.dt_End <= dtBegin) || (alloc.dt_Debut >= dtEnd))
                    {
                        //Il faut supprimer l'entrée.
                        StockageListe.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        if (alloc.dt_Debut < dtBegin)
                        {
                            alloc.dt_Debut = dtBegin;
                        }
                        else if (alloc.dt_End > dtEnd)
                        {
                            alloc.dt_End = dtEnd;
                        }
                    }
                }
            }
            #endregion

            #region Fonction pour ajouter la nouvelle allocation
            private int AddAllocation(AllocationStructure structure)
            {

                //ArrayList contenant les différentes structures qui doivent être modifiées
                ArrayList al_dtARemodele = new ArrayList();
                AllocationStructure alloc;
                for (int i = 0; i < StockageListe.Count; i++)
                {
                    alloc = (AllocationStructure)StockageListe[i];

                    if (alloc.sTableName == structure.sTableName)
                    {
                        //The new allocation is the same than the old one.
                        if ((alloc.dt_Debut >= structure.dt_Debut) && (alloc.dt_End <= structure.dt_End))
                        {
                            //If the new allocation includes totally the old one. No need to save the old allocation.
                            //The following line is used to save the old allocation for the case there is more 
                            //things to do.
                            //al_dtARemodele.Add(StockageListe[i]);
                            StockageListe.RemoveAt(i);
                            i--;
                        }
                        else if ((alloc.dt_Debut < structure.dt_Debut) && (alloc.dt_End > structure.dt_End))
                        {
                            //The new allocation is included in an old one that had exactly the same allocation.
                            //We then exit this fonction.
                            return i;
                        }
                        else if ((alloc.dt_Debut < structure.dt_Debut) && (alloc.dt_End <= structure.dt_End)
                                && (alloc.dt_End >= structure.dt_Debut))
                        {
                            //The old allocation was starting before the new allocation, but was finishing before the 
                            //new allocation. We then change the start date for the new allocation.
                            structure.dt_Debut = alloc.dt_Debut;
                            StockageListe.RemoveAt(i);
                            i--;
                        }
                        else if ((alloc.dt_Debut >= structure.dt_Debut) && (alloc.dt_End > structure.dt_End)
                            && (alloc.dt_Debut <= structure.dt_End))
                        {
                            //The old allocation was ending after the new allocation, but was starting after the 
                            //new allocation. We then change the end date for the new allocation.
                            structure.dt_End = alloc.dt_End;
                            StockageListe.RemoveAt(i);
                            i--;
                        }
                    }
                    else if (((alloc.dt_Debut < structure.dt_Debut) && (alloc.dt_End > structure.dt_Debut)) ||
                           ((alloc.dt_End > structure.dt_End) && (alloc.dt_Debut <= structure.dt_End)) ||
                           ((alloc.dt_Debut >= structure.dt_Debut) && (alloc.dt_End <= structure.dt_End)))
                    {
                        al_dtARemodele.Add(StockageListe[i]);

                        StockageListe.RemoveAt(i);
                        i--;
                    }
                }
                StockageListe.Add(structure);
                if (al_dtARemodele.Count == 0)
                {
                    sortTable();
                    return StockageListe.IndexOf(structure);
                }

                while (al_dtARemodele.Count != 0)
                {
                    int iIndex = indexDateMinimum(al_dtARemodele);
                    alloc = (AllocationStructure)al_dtARemodele[iIndex];
                    al_dtARemodele.RemoveAt(iIndex);
                    if ((structure.dt_Debut <= alloc.dt_Debut) && (structure.dt_End >= alloc.dt_End))
                    {
                        //The new allocation redefine this old allocation. So we ignore the old allocation.
                        continue;
                    }
                    else if ((structure.dt_Debut > alloc.dt_Debut) && (structure.dt_End < alloc.dt_End))
                    {
                        //The new allocation is in the middle of an old one. So we have to split the old one in two parts.
                        StockageListe.Add(new AllocationStructure(alloc.dt_Debut, structure.dt_Debut, alloc.sTableName));
                        StockageListe.Add(new AllocationStructure(structure.dt_End, alloc.dt_End, alloc.sTableName));
                    }
                    else if (structure.dt_Debut > alloc.dt_Debut)
                    {
                        //The new allocation starts in the middle of an old one and finish after the old allocation.
                        //So we just need to change the end of the old allocation to the value of the begining of the new one.

                        alloc.dt_End = structure.dt_Debut;
                        StockageListe.Add(alloc);
                    }
                    else if (structure.dt_End < alloc.dt_End)
                    {
                        //The end of the new allocation is in the middle of the old allocation. We change the begining of the
                        //old allocation to the end of the new allocation.
                        alloc.dt_Debut = structure.dt_End;
                        StockageListe.Add(alloc);
                    }
                }
                sortTable();
                return StockageListe.IndexOf(structure);
            }
            private int indexDateMinimum(ArrayList alListe)
            {
                DateTime min = ((AllocationStructure)alListe[0]).dt_Debut;
                int iIndex = 0;
                for (int i = 1; i < alListe.Count; i++)
                {
                    if (min > ((AllocationStructure)alListe[i]).dt_Debut)
                    {
                        min = ((AllocationStructure)alListe[i]).dt_Debut;
                        iIndex = i;
                    }
                }
                return iIndex;
            }
            /// <summary>
            /// This function will return the next time that there would be a change
            /// in the planning. It is based on the parameter which indicates the 
            /// time actual.
            /// </summary>
            /// <param name="dtTime"></param>
            /// <returns></returns>
            internal DateTime getNextChange(DateTime dtTime)
            {
                if (StockageListe.Count == 0)
                    return DateTime.MinValue;
                DateTime min = ((AllocationStructure)StockageListe[0]).dt_Debut;
                if (dtTime < min)
                    return min;

                /*SGE : 23.08.2010 : Remplacement de l'index de début de boucle par 0 au lieu de 1.*/
                for (int i = 0; i < StockageListe.Count; i++)
                {
                    if ((dtTime >= ((AllocationStructure)StockageListe[i]).dt_Debut) &&
                        (dtTime < ((AllocationStructure)StockageListe[i]).dt_End))
                    {
                        return ((AllocationStructure)StockageListe[i]).dt_End;
                    }
                    else if ((dtTime < ((AllocationStructure)StockageListe[i]).dt_Debut))
                    {
                        return ((AllocationStructure)StockageListe[i]).dt_Debut;
                    }
                }
                return DateTime.MinValue;
            }
            internal DateTime getStartChange()
            {
                if (StockageListe.Count == 0)
                    return DateTime.MinValue;
                return ((AllocationStructure)StockageListe[0]).dt_Debut;
            }
            #endregion

            #region Fonction pour mettre à jour l'affichage du datagridview
            public void fillTable(DataTable table)
            {
                foreach (AllocationStructure alloc in StockageListe)
                {
                    table.Rows.Add(new Object[] { alloc.dt_Debut, alloc.dt_End,alloc.sTableName });
                }
            }
            #endregion

            #region Gestion du tri dela liste

            private void sortTable()
            {
                StockageListe.Sort(new AllocationStructureComparer());
            }
            class AllocationStructureComparer : IComparer
            {
                public AllocationStructureComparer()
                {
                }
                public int Compare(object x, object y)
                {
                    if (!(x is AllocationStructure) || !(y is AllocationStructure))
                    {
                        return 0;
                    }
                    DateTime first = ((AllocationStructure)x).dt_Debut;
                    DateTime second = ((AllocationStructure)y).dt_Debut;

                    return first.CompareTo(second);
                }
            }
            #endregion

            #region Gestion de la suppresion des données
            public void deleteAt(DateTime dt_Begin)
            {
                for (int i = 0; i < StockageListe.Count; i++)
                {
                    if (((AllocationStructure)StockageListe[i]).dt_Debut == dt_Begin)
                    {
                        StockageListe.RemoveAt(i);
                        break;
                    }
                }
            }
            public void deleteAll()
            {
                StockageListe.Clear();
            }
            #endregion

            public Object getAllocation(DateTime Creneau)
            {
                AllocationStructure Aiguille = null;
                foreach (AllocationStructure alloc in StockageListe)
                {
                    if ((alloc.dt_Debut <= Creneau) && (alloc.dt_End > Creneau))
                    {
                        Aiguille = alloc;
                        break;
                    }
                }
                if (Aiguille == null)
                    return null;
                return Aiguille.sTableName;
            }
        }
        #endregion

        #region Gestion des composants pour l'ajout de nouveaux objets
        private void lst_Groups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_Groups.SelectedIndex == -1)
            {
                ActiveDesactiveControl(false);
                return;
            }
            ActiveDesactiveControl(true);
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            cb_TableName.Items.Clear();
            if (lst_Groups.SelectedIndex < tsTablesNames_.Length-2)
            {
                List<String> tsTables = gdhData_.getValidTables("Input", tsTablesNames_[lst_Groups.SelectedIndex+2]);
                cb_TableName.Items.AddRange(tsTables.ToArray());
            }
            fillTable();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (lst_Groups.SelectedIndex == -1)
                return;
            if (cb_TableName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int iSelected = lesAllocations[lst_Groups.SelectedIndex].AddAllocation(dtp_Start.Value, dtp_End.Value, cb_TableName.Text);
            DateTime dtOldValue = dtp_End.Value;
            fillTable();
            dtp_Start.Value = dtOldValue;
            dtp_Start_ValueChanged(null, null);
        }
        private void fillTable()
        {
            DataTable dtSource = ((DataTable)dataGridView1.DataSource);
            dataGridView1.DataSource = null;
            dtSource.Rows.Clear();
            lesAllocations[lst_Groups.SelectedIndex].fillTable(dtSource);
            dataGridView1.DataSource = dtSource;
        }
        #endregion

        #region Gestion des boutons de suppression de lignes
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (lst_Groups.SelectedIndex == -1)
                return;
            ArrayList RowToDelete = new ArrayList();
            foreach (DataGridViewRow ligne in dataGridView1.SelectedRows)
            {
                RowToDelete.Add(ligne.Cells["Begin"].Value);
            }
            while (RowToDelete.Count != 0)
            {
                lesAllocations[lst_Groups.SelectedIndex].deleteAt((DateTime)RowToDelete[0]);
                RowToDelete.RemoveAt(0);
            }
            fillTable();
        }

        private void btn_DeleteAll_Click(object sender, EventArgs e)
        {
            if (lst_Groups.SelectedIndex == -1)
                return;

            lesAllocations[lst_Groups.SelectedIndex].deleteAll();
            fillTable();
        }
        #endregion

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                dtp_Start.Value = (DateTime)dataGridView1.SelectedRows[0].Cells[0].Value;
                dtp_End.MinDate = (DateTime)dataGridView1.SelectedRows[0].Cells[1].Value;
                dtp_End.Value = dtp_End.MinDate;
            }
        }

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_End.Value <= dtp_Start.Value)
                dtp_End.Value = dtp_Start.Value.AddHours(1);
            dtp_End.MinDate = dtp_Start.Value.AddMinutes(1);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Would you like to save your change ?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            switch (res)
            {
                case DialogResult.Yes:
                    AllocationTable.Rows.Clear();
                    DateTime dt_i, dt_Tmp;
                    bool bEnd = false;
                    dt_i = DateTime.MaxValue;
                    for (int i = 0; i < lesAllocations.Length; i++)
                    {
                        dt_Tmp = lesAllocations[i].getStartChange();
                        if ((dt_Tmp < dt_i) && (dt_Tmp!=DateTime.MinValue))
                            dt_i = dt_Tmp;
                    }
                    if (dt_i != DateTime.MaxValue)
                    {
                        String[] tsOldValues = new string[lesAllocations.Length];
                        
                            for (int i = 0; i < lesAllocations.Length; i++)
                            {
                                tsOldValues[i] = tsTablesNames_[i+2];
                            }
                        while (!bEnd)
                        {
                            DataRow ligne = AllocationTable.NewRow();
                            ligne[0] = dt_i; 
                            DateTime dt_Tmp2 = DateTime.MaxValue;
                            for (int i = 0; i < lesAllocations.Length; i++)
                            {
                                dt_Tmp = lesAllocations[i].getNextChange(dt_i);
                                if ((dt_Tmp < dt_Tmp2) && (dt_Tmp != DateTime.MinValue))
                                    dt_Tmp2 = dt_Tmp;
                            }
                            if (dt_Tmp2 == DateTime.MaxValue)
                                break;
                            ligne[1] = dt_Tmp2;
                            for (int i = 0; i < lesAllocations.Length; i++)
                            {
                                String sTmp = (String)lesAllocations[i].getAllocation(dt_i);
                                if (sTmp == null)
                                {
                                    sTmp = tsOldValues[i ];
                                }
                                else
                                {
                                    tsOldValues[i] = sTmp;
                                }
                                ligne[i + 2] = sTmp;
                            }
                            AllocationTable.Rows.Add(ligne);
                            
                            dt_i = dt_Tmp2;
                        }
                    }
                    AllocationTable.AcceptChanges();
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    DialogResult = DialogResult.None;
                    break;
            }
        }
    }
}