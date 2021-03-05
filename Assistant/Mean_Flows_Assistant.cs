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
    public partial class Mean_Flows_Assistant : Form
    {
        DataTable AllocationTable;
        GroupAllocation[] lesAllocations;

        #region Fonctions pour l'initialisation et la construction de la classe
        private void Initialize(DataTable AllocationTable_)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            this.Text = "Change the data on " + AllocationTable_.TableName;
            AllocationTable = AllocationTable_;
            txt_Number.BackColor = Color.White;

            lst_Groups.BackColor = Color.White;
            if (AllocationTable.Rows.Count != 0)
            {
                dtp_Start.Value = (DateTime)AllocationTable.Rows[0][0];
                dtp_End.Value = (DateTime)AllocationTable.Rows[AllocationTable.Rows.Count - 1][0];
                if (AllocationTable.Rows.Count > 1)
                {
                    txt_Step.Text = OverallTools.DataFunctions.MinuteDifference((DateTime)AllocationTable.Rows[0][0], (DateTime)AllocationTable.Rows[1][0]).ToString();
                }
            }
            else
            {
                dtp_Start.Value =DateTime.Now;
                dtp_Start.Value = dtp_Start.Value.AddSeconds(-dtp_Start.Value.Second);
                dtp_Start.Value = dtp_Start.Value.AddMinutes(-dtp_Start.Value.Minute);
                dtp_End.Value = dtp_Start.Value.AddDays(1);
            }

            foreach (DataColumn colonne in AllocationTable_.Columns)
            {
                if (colonne.ColumnName != "Time")
                {
                    lst_Groups.Items.Add(colonne.ColumnName);
                }
            }
            ActiveDesactiveControl(false);
            foreach (DataRow ligne in AllocationTable.Rows)
            {
                cb_Begin.Items.Add(ligne["Time"].ToString());
            }
            DataTable table = new DataTable();
            table.Columns.Add("Begin", typeof(DateTime));
            table.Columns.Add("End", typeof(DateTime));
            table.Columns.Add("Flow", typeof(Double));


            dataGridView1.DataSource = table;

            InitializeAllocation(); //InitializeAllocation(OverallTools.DataFunctions.convertToAllocTable(AllocationTable, false, false, typeof(Int32)));
        }

        private void InitializeAllocation_(DataTable laTable)
        {
            if (AllocationTable.Columns.Count == 0)
                return;
            lesAllocations = new GroupAllocation[AllocationTable.Columns.Count-1];

            int i;
            for (i= 0; i < lesAllocations.Length; i++)
            {
                lesAllocations[i] = new GroupAllocation(AllocationTable.Columns[1+i].ColumnName);
            }

            if (AllocationTable.Rows.Count == 0)
                return;

            DateTime dtEndTime;
            DateTime dtBeginTime;
            if (!DateTime.TryParse(cb_Begin.Items[cb_Begin.Items.Count - 1].ToString(), out dtEndTime))
            {
                return;
            }
            if (!DateTime.TryParse(cb_Begin.Items[0].ToString(), out dtBeginTime))
            {
                return;
            }
            for (i= 0; i < lesAllocations.Length; i++)
            {
                lesAllocations[i].AddAllocation(dtEndTime, dtBeginTime, 0);
            }
            foreach (DataRow ligne in laTable.Rows)
            {
                int iIndexColonne = AllocationTable.Columns.IndexOf(ligne[0].ToString());
                    lesAllocations[iIndexColonne - 1].AddAllocation((DateTime)ligne[1], (int)ligne[2]);
            }
            
            for (i = 0; i < lesAllocations.Length; i++)
            {
                    lesAllocations[i].AddAllocation(dtEndTime, 0);
            }
        }
        
        // >> Task #17261 PAX2SIM - BHS mode - Mean Flows Assistant
        private void InitializeAllocation()
        {
            if (AllocationTable.Columns.Count == 0)
                return;
            lesAllocations = new GroupAllocation[AllocationTable.Columns.Count - 1];

            int i;
            for (i = 0; i < lesAllocations.Length; i++)
            {
                lesAllocations[i] = new GroupAllocation(AllocationTable.Columns[1 + i].ColumnName);
            }

            if (AllocationTable.Rows.Count == 0)
                return;

            DateTime dtEndTime;
            DateTime dtBeginTime;
            if (!DateTime.TryParse(cb_Begin.Items[cb_Begin.Items.Count - 1].ToString(), out dtEndTime))
            {
                return;
            }
            if (!DateTime.TryParse(cb_Begin.Items[0].ToString(), out dtBeginTime))
            {
                return;
            }
            for (i = 0; i < lesAllocations.Length; i++)
            {
                lesAllocations[i].AddAllocation(dtEndTime, dtBeginTime, 0);
            }

            int timeColumnIndex = AllocationTable.Columns.IndexOf("Time");
            if (timeColumnIndex != -1)
            {
                foreach (GroupAllocation ga in lesAllocations)
                {
                    int valueColumnIndex = AllocationTable.Columns.IndexOf(ga.GroupName);
                    if (valueColumnIndex == -1)
                    {
                        continue;
                    }
                    double previousValue = -1;
                    double currentValue = -1;
                    DateTime intervalStartDate = DateTime.MinValue;                    
                    DateTime currentDate = DateTime.MinValue;
                    for (int j = 0; j < AllocationTable.Rows.Count; j++)
                    {
                        DataRow row = AllocationTable.Rows[j];                        
                        if (row[valueColumnIndex] != null && double.TryParse(row[valueColumnIndex].ToString(), out currentValue)
                            && row[timeColumnIndex] != null && DateTime.TryParse(row[timeColumnIndex].ToString(), out currentDate))
                        {
                            if (j == 0)
                            {
                                previousValue = currentValue;                                
                                intervalStartDate = currentDate;                                
                                continue;
                            }
                            if (currentValue != previousValue || j == AllocationTable.Rows.Count - 1)
                            {
                                ga.AddAllocation(intervalStartDate, currentDate, previousValue);
                                intervalStartDate = currentDate;
                            }
                            previousValue = currentValue;                            
                        }
                    }
                }
            }
        }
        // << Task #17261 PAX2SIM - BHS mode - Mean Flows Assistant
        
        public Mean_Flows_Assistant(DataTable AllocationTable_, String sColumn)
        {
            Initialize(AllocationTable_);
            lst_Groups.SelectedItem = sColumn;
        }
        public Mean_Flows_Assistant(DataTable AllocationTable_)
        {
            Initialize(AllocationTable_);
        }
        private void ActiveDesactiveControl(bool value)
        {
            lbl_Alloc.Enabled = value;
            lbl_Begin.Enabled = value;
            lbl_End.Enabled = value;
            txt_Number.Enabled = value;
            cb_End.Enabled = value;
            cb_Begin.Enabled = value;
        }
        #endregion

        #region la class GroupAllocation
        class GroupAllocation
        {
            public String GroupName;
            ArrayList StockageListe;
            AllocationStructure AncienneValeur;

            #region Structure de stockage
            class AllocationStructure
            {
                public DateTime dt_Debut;
                public DateTime dt_End;
                public Double d_Openned;
                public AllocationStructure(DateTime dt_Debut_, DateTime dt_End_, Double d_Openned_)
                {
                    dt_Debut = dt_Debut_;
                    dt_End = dt_End_;
                    d_Openned = d_Openned_;
                }
                public bool isIdentique(DateTime dt_Debut_, DateTime dt_End_, Double d_Openned_)
                {
                    return (dt_Debut == dt_Debut_) && (dt_End == dt_End_) && (d_Openned == d_Openned_);
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
            public void AddAllocation(DateTime dtBegin, Double dOpenned)
            {
                if (AncienneValeur != null)
                {
                    AddAllocation(new AllocationStructure(AncienneValeur.dt_Debut, dtBegin, AncienneValeur.d_Openned));
                }
                AncienneValeur = new AllocationStructure(dtBegin, DateTime.Now, dOpenned);
            }

            public void AddAllocation(DateTime dtBegin, DateTime dtEnd, Double dOpenned)
            {
                    AddAllocation(new AllocationStructure(dtBegin, dtEnd, dOpenned));
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
            private void AddAllocation(AllocationStructure structure)
            {

                //ArrayList contenant les différentes structures qui doivent être modifiées
                ArrayList al_dtARemodele = new ArrayList();
                AllocationStructure alloc;
                for (int i=0; i< StockageListe.Count;i++)
                { 
                    alloc= (AllocationStructure)StockageListe[i];

                    if (((alloc.dt_Debut < structure.dt_Debut) && (alloc.dt_End > structure.dt_Debut)) ||
                       ((alloc.dt_End > structure.dt_End) && (alloc.dt_Debut <= structure.dt_End))||
                       ((alloc.dt_Debut>=structure.dt_Debut)&&(alloc.dt_End<=structure.dt_End)))
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
                    return;
                }

                while (al_dtARemodele.Count != 0)
                {
                    int iIndex = indexDateMinimum(al_dtARemodele);
                    alloc = (AllocationStructure)al_dtARemodele[iIndex];
                    al_dtARemodele.RemoveAt(iIndex);
                    if ((structure.dt_Debut <= alloc.dt_Debut) && (structure.dt_End >= alloc.dt_End))
                    {
                        continue;
                    }
                    else if((structure.dt_Debut > alloc.dt_Debut) && (structure.dt_End < alloc.dt_End))
                    {
                        //il faut ajouter Deux nouvelles allocations.
                        StockageListe.Add(new AllocationStructure(alloc.dt_Debut, structure.dt_Debut, alloc.d_Openned));
                        StockageListe.Add(new AllocationStructure(structure.dt_End, alloc.dt_End, alloc.d_Openned));
                    }else if (structure.dt_Debut > alloc.dt_Debut)
                    {
                        //le début de cette nouvelle ligne doit être modifié.
                        alloc.dt_End = structure.dt_Debut;
                        StockageListe.Add(alloc);
                    }
                    else if (structure.dt_End < alloc.dt_End)
                    {
                        alloc.dt_Debut = structure.dt_End;
                        StockageListe.Add(alloc);
                    }
                }
                sortTable();
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
            #endregion

            #region Fonction pour mettre à jour l'affichage du datagridview
            public void fillTable(DataTable table)
            {
                foreach (AllocationStructure alloc in StockageListe)
                {
                    table.Rows.Add(new Object[] { alloc.dt_Debut, alloc.dt_End, alloc.d_Openned });
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

            public Object getAllocation(DateTime Creneau, bool PassportAllocation)
            {
                AllocationStructure Aiguille = null;
                foreach (AllocationStructure alloc in StockageListe)
                {
                    if ((alloc.dt_Debut <= Creneau) && (alloc.dt_End >Creneau) )
                    {
                        Aiguille = alloc;
                        break;
                    }
                }
                if (Aiguille == null)
                    return 0;
                return Aiguille.d_Openned;
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
            //dataGridView1.Rows.Clear();
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            fillTable();

        }
        private void cb_Begin_SelectedIndexChanged(object sender, EventArgs e)
        {
            String ancienneValeur = cb_End.Text;
            cb_End.Items.Clear();
            for (int i = cb_Begin.SelectedIndex+1; i < cb_Begin.Items.Count; i++)
            {
                cb_End.Items.Add(cb_Begin.Items[i]);
            }
            if (cb_End.Items.Contains(ancienneValeur))
            {
                cb_End.Text = ancienneValeur;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (lst_Groups.SelectedIndex == -1)
                return;
            if (((cb_Begin.Text != "") && (cb_End.Text != "") && (txt_Number.Text != "")))
            {
                if (cb_Begin.Text == cb_End.Text)
                {
                    MessageBox.Show("Please choose another interval for the times.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Double dValue;
                if (!Double.TryParse(txt_Number.Text, out dValue))
                {
                    MessageBox.Show("Please enter a valid number for the flow.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                lesAllocations[lst_Groups.SelectedIndex].AddAllocation(
                    FonctionsType.getDate(cb_Begin.Text, typeof(String)),
                    FonctionsType.getDate(cb_End.Text, typeof(String)),
                    dValue);
                fillTable();
                cb_Begin.Text = cb_End.Text;
                txt_Number.Text = "";
                txt_Number.Focus();
            }
            else
            {
                MessageBox.Show("Please fill all the blank before add a new line", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void fillTable()
        {
            /*if (AllocationTable.Rows.Count == 0)
                return;*/
            //dataGridView1.Rows.Clear();
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            lesAllocations[lst_Groups.SelectedIndex].fillTable((DataTable)dataGridView1.DataSource);
        }
        #endregion

        #region Gestion des boutons de suppression de lignes
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if(lst_Groups.SelectedIndex ==-1)
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
                cb_Begin.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                cb_End.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_Number.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Would you like to save your change ?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            switch (res)
            {
                case DialogResult.Yes:
                    AllocationTable.Rows.Clear();
                    DateTime dt_i = dtp_Start.Value;
                    Int32 StepValue;
                    if (!Int32.TryParse(txt_Step.Text, out StepValue))
                    {
                        MessageBox.Show("Wrong values for the step value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    while (dt_i <= dtp_End.Value)
                    {
                        DataRow ligne = AllocationTable.NewRow();
                        ligne[0] = dt_i;
                        for (int i = 0; i < lesAllocations.Length; i++)
                        {
                            ligne[i + 1] = lesAllocations[i].getAllocation(dt_i, false);
                        }
                        AllocationTable.Rows.Add(ligne);
                        dt_i = dt_i.AddMinutes(StepValue);
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

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            if ((dtp_Start.Focused) || (dtp_End.Focused) || (txt_Step.Focused))
                return;
            if (dtp_Start.Value > dtp_End.Value)
            {
                MessageBox.Show("Wrong values for the dates", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Int32 StepValue;
            if ((!Int32.TryParse(txt_Step.Text, out StepValue)) || (StepValue<=0))
            {
                MessageBox.Show("Wrong values for the step value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Int32 minuteDifference = (Int32) OverallTools.DataFunctions.MinuteDifference(dtp_Start.Value, dtp_End.Value);
            dtp_End.Value = dtp_End.Value.AddMinutes(-minuteDifference % StepValue);
            foreach (GroupAllocation group in lesAllocations)
            {
                group.AddAllocation(dtp_Start.Value, dtp_End.Value);
            }
            cb_Begin.Items.Clear();
            cb_End.Items.Clear();
            DateTime dt_i = dtp_Start.Value.AddSeconds(-dtp_Start.Value.Second);
            while (dt_i <= dtp_End.Value)
            {
                cb_Begin.Items.Add(dt_i.ToString());
                cb_End.Items.Add(dt_i.ToString());
                dt_i = dt_i.AddMinutes(StepValue);
            }
        }

        private void txt_Number_TextChanged(object sender, EventArgs e)
        {
            
            Double dFlow;
            if ((!Double.TryParse(txt_Number.Text, out dFlow)))
            {
                txt_Flow_S.Text = "0";
                return;
            }
            txt_Flow_S.Text = ((Double)Math.Round(dFlow / 3600, 2)).ToString();
        }
    }
}