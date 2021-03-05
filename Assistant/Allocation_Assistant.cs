using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace SIMCORE_TOOL.Assistant
{
    public partial class Allocation_Assistant : Form
    {
        DataTable AllocationTable;
        bool PassportAllocation;
        bool bSecurityAllocation;
        GroupAllocation[] lesAllocations;

        //ResourceManager
        private ResourceManager manager;
        //Tableau contenu tous les messages à mettre dans les MessageBox
        //Extrait depuis le fichier de ressource associé à la fenêtre
        string[] messages;

        #region Fonctions pour l'initialisation et la construction de la classe
        private void Initialize(DataTable AllocationTable_, bool PassportAllocation_, bool bSecurityAllocation_)
        {
            //Creation du resourceManager
            manager = new ResourceManager("SIMCORE_TOOL.Assistant.Allocation_Assistant", Assembly.GetExecutingAssembly());
            InitializeComponent();
            bSecurityAllocation = bSecurityAllocation_;
            //////////////////////////////
            //Initialisation des messages
            //////////////////////////////
            string[] messagesInfo = { manager.GetString("message1"), 
                                  manager.GetString("message2"),
                                  manager.GetString("message3"),
                                  manager.GetString("message4"),
                                  manager.GetString("message5")};
            messages = messagesInfo ;

            OverallTools.FonctionUtiles.MajBackground(this);
            //this.Text = "Change the data on " + AllocationTable_.TableName;
            this.Text = manager.GetString("message6") + AllocationTable_.TableName;
            AllocationTable = AllocationTable_;
            PassportAllocation = PassportAllocation_;
            txt_Number.BackColor = Color.White;

            lst_Groups.BackColor = Color.White;
            cb_PassportType.Enabled = (PassportAllocation ||bSecurityAllocation);
            lbl_Passport.Enabled = (PassportAllocation ||bSecurityAllocation);
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

                dtp_Start.Value = OverallTools.DataFunctions.EraseSeconds(DateTime.Now);
                dtp_End.Value = OverallTools.DataFunctions.EraseSeconds(DateTime.Now);
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
            if (bSecurityAllocation)
                lbl_Passport.Text = "Class";
            else
                lbl_Passport.Text = "PassportType";
            table.Columns.Add(lbl_Passport.Text, typeof(String));
            table.Columns.Add("Number of desks", typeof(int));


            dataGridView1.DataSource = table;
            if (bSecurityAllocation_)
            {
                this.cb_PassportType.Items.Clear();
                this.cb_PassportType.Items.AddRange(new object[] {
            "F & B",
            "Eco.",
            "Both"});
            }

            InitializeAllocation(OverallTools.DataFunctions.convertToAllocTable(AllocationTable, PassportAllocation_,bSecurityAllocation, typeof(Int32)));
        }

        private void InitializeAllocation(DataTable laTable)
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
                lesAllocations[i].AddAllocation(dtEndTime, dtBeginTime, 0, 0);
            }
            foreach (DataRow ligne in laTable.Rows)
            {
                int iIndexColonne = AllocationTable.Columns.IndexOf(ligne[0].ToString());
                if ((PassportAllocation)||(bSecurityAllocation))
                {
                    lesAllocations[iIndexColonne - 1].AddAllocation((DateTime)ligne[1], (int)ligne[2],(int )ligne[3]);
                }
                else
                {
                    lesAllocations[iIndexColonne - 1].AddAllocation((DateTime)ligne[1], (int)ligne[2]);
                }
            }
            
            for (i = 0; i < lesAllocations.Length; i++)
            {
                if ((PassportAllocation) || (bSecurityAllocation))
                {
                    lesAllocations[i].AddAllocation(dtEndTime, 0, 0);
                }
                else
                {
                    lesAllocations[i].AddAllocation(dtEndTime, 0);
                }
            }
        }

        public Allocation_Assistant(DataTable AllocationTable_, bool PassportAllocation_, bool bSecurityAllocation_, String FC)
        {
            Initialize(AllocationTable_, PassportAllocation_, bSecurityAllocation_);
            lst_Groups.SelectedItem = FC;
        }
        public Allocation_Assistant(DataTable AllocationTable_, String FC)
        {
            Initialize(AllocationTable_, false,false);
            lst_Groups.SelectedItem = FC;
        }

        public Allocation_Assistant(DataTable AllocationTable_, bool PassportAllocation_, bool bSecurityAllocation_)
        {
            Initialize(AllocationTable_, PassportAllocation_,bSecurityAllocation_);
        }
        public Allocation_Assistant(DataTable AllocationTable_)
        {
            Initialize(AllocationTable_, false,false);
        }
        private void ActiveDesactiveControl(bool value)
        {
            lbl_Alloc.Enabled = value;
            lbl_Begin.Enabled = value;
            lbl_End.Enabled = value;
            txt_Number.Enabled = value;
            cb_End.Enabled = value;
            cb_Begin.Enabled = value;
            cb_PassportType.Enabled = value && (PassportAllocation ||bSecurityAllocation);
            lbl_Passport.Enabled = value && (PassportAllocation||bSecurityAllocation);
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
                public int i_PassportType;
                public int i_Openned;
                public AllocationStructure(DateTime dt_Debut_, DateTime dt_End_, int i_PassportType_, int i_Openned_)
                {
                    dt_Debut = dt_Debut_;
                    dt_End = dt_End_;
                    i_PassportType = i_PassportType_;
                    i_Openned = i_Openned_;
                }
                public AllocationStructure(DateTime dt_Debut_, DateTime dt_End_, int i_Openned_)
                {
                    dt_Debut = dt_Debut_;
                    dt_End = dt_End_;
                    i_PassportType = -1;
                    i_Openned = i_Openned_;
                }
                public bool isIdentique(DateTime dt_Debut_, DateTime dt_End_, int i_PassportType_, int i_Openned_)
                {
                    return (dt_Debut == dt_Debut_) && (dt_End == dt_End_) && (i_PassportType == i_PassportType_) && (i_Openned == i_Openned_);
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
            public void AddAllocation(DateTime dtBegin, int iOpenned)
            {
                if (AncienneValeur != null)
                {
                    AddAllocation(new AllocationStructure(AncienneValeur.dt_Debut, dtBegin, AncienneValeur.i_Openned));
                }
                AncienneValeur = new AllocationStructure(dtBegin, DateTime.Now, iOpenned);
            }
            public void AddAllocation(DateTime dtBegin, int iPassportType, int iOpenned)
            {
                if (AncienneValeur != null)
                {
                    AddAllocation(new AllocationStructure(AncienneValeur.dt_Debut, dtBegin, AncienneValeur.i_PassportType, AncienneValeur.i_Openned));
                }
                AncienneValeur = new AllocationStructure(dtBegin, DateTime.Now, iPassportType, iOpenned);
            }

            public void AddAllocation(DateTime dtBegin, DateTime dtEnd, int iOpenned)
            {
                    AddAllocation(new AllocationStructure(dtBegin, dtEnd, iOpenned));
            }

            public void AddAllocation(DateTime dtBegin, DateTime dtEnd, int iPassportType, int iOpenned)
            {
                    AddAllocation(new AllocationStructure(dtBegin, dtEnd, iPassportType, iOpenned));
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
                        StockageListe.Add(new AllocationStructure(alloc.dt_Debut, structure.dt_Debut, alloc.i_PassportType, alloc.i_Openned));
                        StockageListe.Add(new AllocationStructure(structure.dt_End, alloc.dt_End, alloc.i_PassportType, alloc.i_Openned));
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
            public void fillTable(DataTable table, bool Passport)
            {
                foreach (AllocationStructure alloc in StockageListe)
                {
                    String sPassportType = "";
                    switch (alloc.i_PassportType)
                    {
                        case 1:
                            if (Passport)
                                sPassportType = "Local";
                            else
                                sPassportType = "F & B";
                            break;
                        case 2:
                            if (Passport)
                                sPassportType = "Not Local";
                            else
                                sPassportType = "Eco.";
                            break;
                        case 3:
                            sPassportType = "Both";
                            break;
                        default:
                            sPassportType = "";
                            break;
                    }
                    table.Rows.Add(new Object[] { alloc.dt_Debut, alloc.dt_End, sPassportType, alloc.i_Openned });
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
                if (PassportAllocation)
                {
                    if (Aiguille == null)
                        return "3,0";
                    if (Aiguille.i_PassportType == 0)
                        Aiguille.i_PassportType = 3;
                    return Aiguille.i_PassportType.ToString() + "," + Aiguille.i_Openned.ToString();
                }
                if (Aiguille == null)
                    return 0;
                return Aiguille.i_Openned;
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
            if (((cb_Begin.Text != "") && (cb_End.Text != "") && (txt_Number.Text != "")) && (((cb_PassportType.Text != "") && (cb_PassportType.Enabled)) || (!cb_PassportType.Enabled)))
            {
                if (cb_Begin.Text == cb_End.Text)
                {
                    MessageBox.Show(messages[2], "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //MessageBox.Show("Please choose another interval for the times.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if ((PassportAllocation) || (bSecurityAllocation))
                {
                    FonctionsType.getDate(cb_Begin.Text, typeof(String));
                    lesAllocations[lst_Groups.SelectedIndex].AddAllocation(
                        FonctionsType.getDate(cb_Begin.Text, typeof(String)), 
                        FonctionsType.getDate(cb_End.Text, typeof(String)),
                        cb_PassportType.SelectedIndex + 1, 
                        FonctionsType.getInt(txt_Number.Text, typeof(String))); 
                }
                else
                {
                    lesAllocations[lst_Groups.SelectedIndex].AddAllocation(
                        FonctionsType.getDate(cb_Begin.Text, typeof(String)), 
                        FonctionsType.getDate(cb_End.Text, typeof(String)), 
                        FonctionsType.getInt(txt_Number.Text, typeof(String))); 
                }
                fillTable();
                cb_Begin.Text = cb_End.Text;
                txt_Number.Text = "";
                txt_Number.Focus();
            }
            else
            {
                MessageBox.Show(messages[3], "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("Please fill all the blank before add a new line", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void fillTable()
        {
            /*if (AllocationTable.Rows.Count == 0)
                return;*/
            //dataGridView1.Rows.Clear();
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            lesAllocations[lst_Groups.SelectedIndex].fillTable((DataTable)dataGridView1.DataSource,PassportAllocation);
            dataGridView1.Columns[2].Visible = (PassportAllocation||bSecurityAllocation);
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
                if ((PassportAllocation) || (bSecurityAllocation))
                {
                    cb_PassportType.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                }
                txt_Number.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(messages[4], "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //DialogResult res = MessageBox.Show("Would you like to save your change ?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            switch (res)
            {
                case DialogResult.Yes:
                    AllocationTable.Rows.Clear();
                    DateTime dt_i = dtp_Start.Value;
                    Int32 StepValue;
                    if (!Int32.TryParse(txt_Step.Text, out StepValue))
                    {
                        MessageBox.Show(messages[0], "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //MessageBox.Show("Wrong values for the step value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    while (dt_i <= dtp_End.Value)
                    {
                        DataRow ligne = AllocationTable.NewRow();
                        ligne[0] = dt_i;
                        for (int i = 0; i < lesAllocations.Length; i++)
                        {
                            ligne[i + 1] = lesAllocations[i].getAllocation(dt_i, PassportAllocation||bSecurityAllocation);
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
                MessageBox.Show(messages[1], "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("Wrong values for the dates", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Int32 StepValue;
            if ((!Int32.TryParse(txt_Step.Text, out StepValue)) || (StepValue<=0))
            {
                MessageBox.Show(messages[0], "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("Wrong values for the step value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            while (dt_i <= dtp_End.Value.AddMinutes(1))
            {
                cb_Begin.Items.Add(dt_i.ToString());
                cb_End.Items.Add(dt_i.ToString());
                dt_i = dt_i.AddMinutes(StepValue);
            }
        }

        private void lbl_EndTime_Click(object sender, EventArgs e)
        {

        }

        private void Allocation_Assistant_Load(object sender, EventArgs e)
        {

        }

    }
}