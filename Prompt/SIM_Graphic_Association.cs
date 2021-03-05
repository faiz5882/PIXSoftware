using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Graphic_Association : Form
    {
        private GestionDonneesHUB2SIM DonneesEnCours;
        TreeNodeCollection NodesArbre;
        private String LastName;
        ColumnInformation SelectedTable_;
        Boolean tableNameMode;

        public SIM_Graphic_Association(GestionDonneesHUB2SIM DonneesEnCours_, TreeNodeCollection NodesArbre_, ImageList imgList)
        {
            InitializeComponent();
            treeView1.ImageList = imgList;
            OverallTools.FonctionUtiles.MajBackground(this);
            DonneesEnCours = DonneesEnCours_;
            NodesArbre = NodesArbre_;
            cb_Scenario.Items.Add("Input");
            SelectedTable_ = null;
            tableNameMode = false;
            foreach (String Name in DonneesEnCours.getScenarioNames())
            {
                cb_Scenario.Items.Add(Name);
            }
            cb_Scenario.SelectedIndex = 0;
        }

        public SIM_Graphic_Association(GestionDonneesHUB2SIM DonneesEnCours_, TreeNodeCollection NodesArbre_, ColumnInformation SelectedTable, ImageList imgList)
        {
            InitializeComponent();
            treeView1.ImageList = imgList;
            SelectedTable_ = SelectedTable;
            OverallTools.FonctionUtiles.MajBackground(this);
            DonneesEnCours = DonneesEnCours_;
            NodesArbre = NodesArbre_;
            cb_Scenario.Items.Add("Input");
            foreach (String Name in DonneesEnCours.getScenarioNames())
            {
                cb_Scenario.Items.Add(Name);
            }
            cb_Scenario.SelectedIndex = cb_Scenario.Items.IndexOf(SelectedTable.DataSet);
            if (cb_Scenario.SelectedIndex == -1)
            {
                cb_Scenario.SelectedIndex = 0;
            }
            else
            {
                TreeNode Find = null;
                for (int i = 0; (i < treeView1.Nodes.Count) && (Find == null); i++)
                {
                    Find = OverallTools.TreeViewFunctions.RechercherNom(SelectedTable.TableName, treeView1.Nodes[i]);
                }
                if ((SelectedTable.ExceptionName != null) && (SelectedTable.ExceptionName != "") && (Find != null))
                {
                    Find = OverallTools.TreeViewFunctions.RechercherNom(SelectedTable.ExceptionName, Find);
                }
                if (Find == null)
                {
                    treeView1.SelectedNode = null;
                }
                else
                {

                    treeView1.SelectedNode = Find;
                    Find.Expand();
                }
            }
        }

        public ColumnInformation Column
        {
            get
            {
                if (treeView1.SelectedNode == null)
                    return null;
                TreeViewTag tvt=null;
                if (treeView1.SelectedNode.Tag != null)
                {
                    tvt = treeView1.SelectedNode.Tag as TreeViewTag;
                    if(tvt.IsExceptionNode)
                    {
                        return new ColumnInformation(cb_Scenario.Text, tvt.Name,
                                                     tvt.ExceptionName,
                                                     cb_Column.Text, cb_Abscissa.Text, txt_DisplayedName.Text);
                    }
                }
                return new ColumnInformation(cb_Scenario.Text, treeView1.SelectedNode.Name, cb_Column.Text, cb_Abscissa.Text, txt_DisplayedName.Text);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (!tableNameMode)
            {
                if ((cb_Scenario.Text == "") || (treeView1.SelectedNode == null) || (treeView1.SelectedNode.ImageIndex == 0) || (cb_Column.Text == "") || (txt_DisplayedName.Text == ""))
                {
                    MessageBox.Show("Please select a valid column", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }
        }

        private void cb_Scenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Scenario.Text == "")
            {
                treeView1.Nodes.Clear();
                return;
            }

            //Partie pour le treeView.
            if (cb_Scenario.Text == "Input")
            {
                OverallTools.TreeViewFunctions.copyChilds(treeView1, NodesArbre[1], false,true,false);
            }
            else
            {
                TreeNode Scenario = OverallTools.TreeViewFunctions.RechercherNomEnfants(cb_Scenario.Text, NodesArbre[2]);
                if (Scenario == null)
                    return;
                OverallTools.TreeViewFunctions.copyChilds(treeView1, Scenario, false,true,false);
            }
            treeView1.SelectedNode = treeView1.Nodes[0];

        }

        private void cb_Column_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!tableNameMode)
            {
                if ((txt_DisplayedName.Text == "") || ((txt_DisplayedName.Text == null)) || (txt_DisplayedName.Text == LastName))
                {
                    txt_DisplayedName.Text = this.Column.FullName();
                    LastName = txt_DisplayedName.Text;
                }
            }
            else
            {
                if ((txt_DisplayedName.Text == "") || ((txt_DisplayedName.Text == null)) || (txt_DisplayedName.Text == LastName))
                {
                    txt_DisplayedName.Text = this.Column.TableName + "." +this.Column.ColumnName;
                    LastName = txt_DisplayedName.Text;
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e == null)
                return;
            cb_Column.Items.Clear();
            cb_Abscissa.Items.Clear();
            cb_Column.Text = "";
            cb_Abscissa.Text = "";
            txt_DisplayedName.Text = "";
            if ((e.Node == null) || (e.Node.ImageIndex == 0))
            {
                return;
            }
            TreeViewTag tvt = null;
            DataTable table = DonneesEnCours.getTable(cb_Scenario.Text, e.Node.Name);
            if(e.Node.Tag != null)
            {
                tvt = e.Node.Tag as TreeViewTag;
                if(tvt.IsExceptionNode)
                    table = DonneesEnCours.getExceptionTable(cb_Scenario.Text, tvt.Name, tvt.ExceptionName);
            }
            if (table == null)
                return;
            cb_Abscissa.Items.Add("");
            foreach (DataColumn Colonne in table.Columns)
            {
                cb_Column.Items.Add(Colonne.ColumnName);
                cb_Abscissa.Items.Add(Colonne.ColumnName);
            }
            if (cb_Column.Items.Count > 1)
            {
                cb_Column.SelectedIndex = 1;
                cb_Abscissa.SelectedIndex = 1;
            }
            else
            {
                cb_Column.SelectedIndex = 0;
            }
            if (SelectedTable_ != null)
            {
                int i = cb_Column.Items.IndexOf(SelectedTable_.ColumnName);
                if (i != -1)
                    cb_Column.SelectedIndex = i;

                if (SelectedTable_.AbscissaColumnName != "")
                {
                    i = cb_Abscissa.Items.IndexOf(SelectedTable_.AbscissaColumnName);
                    if (i != -1)
                        cb_Abscissa.SelectedIndex = i;
                }
                txt_DisplayedName.Text = SelectedTable_.DisplayedName;
                SelectedTable_ = null;
                return;
            }
        }
        internal void changeLabel1()
        {
            label1.Text = "Table name displayed";
            txt_DisplayedName.Text = "";
            cb_Scenario.Enabled = false;
        }

        internal string getDisplayedName()
        {
            return txt_DisplayedName.Text;
        }

        internal void setTableNameMode()
        {
            tableNameMode = true;
        }

        internal ColumnInformation getSelectedTable()
        {
            return SelectedTable_;
        }

    }
}