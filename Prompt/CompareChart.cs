using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SIMCORE_TOOL.Prompt;
using Nevron.Chart.WinForm;

namespace SIMCORE_TOOL.Prompt
{
    public partial class CompareChart : Form
    {

        GestionDonneesHUB2SIM DonneesEnCours_;
        TreeNodeCollection treenode;
        ImageList imgList;
        ColumnInformation SelectedTable;
        List<String> ScenarioNames;
        ArrayList alScenarioNames;
        ToolStripMenuItem sender_;
        SIM_Assistant_Creation_Graphics_Filters Sacgf;
        Prompt.SIM_Graphic_Association sga;
        GraphicFilter gf;
        //If we are using the comparison with the whole chart mode, set its value to true 
        Boolean wholeChartMode;
        DataTable dtableTest;
        // String containing Table and scenario name when the user added a table that is not contained in a scenario
        ArrayList TableError;
        ArrayList ScenarioError;

        internal SIM_Graphic_Association getSga()
        {
            return sga;
        }

        internal SIM_Assistant_Creation_Graphics_Filters getSacgf()
        {
            return Sacgf;
        }

        internal GraphicFilter getGf()
        {
            return gf;
        }
        public CompareChart(GestionDonneesHUB2SIM DonneesEnCours, TreeView treeview, ImageList imageList1,ToolStripMenuItem sender)
        {
            DonneesEnCours_ = DonneesEnCours;
            imgList = imageList1;
            treenode = treeview.Nodes;
            sender_ = sender;
            wholeChartMode = false;
            OverallTools.FonctionUtiles.MajBackground(this);
            InitializeComponent();
            ScenarioNames = DonneesEnCours.getScenariosReadyToSimulate();
            alScenarioNames = new ArrayList(ScenarioNames);
            alScenarioNames.Sort(new OverallTools.FonctionUtiles.ColumnsComparer()); 
            TableError = new ArrayList();
            ScenarioError = new ArrayList();

            foreach (String Name in alScenarioNames)
            {
                listBox1.Items.Add(Name);
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count <= 1)
            {
                MessageBox.Show("Select a least two scenarios", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedTable = new ColumnInformation(listBox2.Items[0].ToString(), sender_.Tag.ToString(), sender_.Tag.ToString(), sender_.Tag.ToString());
            sga = new Prompt.SIM_Graphic_Association(DonneesEnCours_, treenode, SelectedTable, imgList);

            gf = DonneesEnCours_.GetGeneralGraphicFilter(sender_.Tag.ToString());
            Sacgf = new SIM_Assistant_Creation_Graphics_Filters(DonneesEnCours_, treenode, gf, imgList);

            sga.changeLabel1();
            sga.setTableNameMode();



            if (sga.ShowDialog() == DialogResult.OK)
            {
                SelectedTable = sga.Column;
                // The user checks Compare Chart >> Chart

                if (!wholeChartMode)
                {
                    for (int i = 0; i < listBox2.Items.Count; i++)
                    {
                        dtableTest = DonneesEnCours_.getTable(listBox2.Items[i].ToString(), SelectedTable.TableName);
                        if (dtableTest != null)
                        {

                            ColumnInformation column = new ColumnInformation(listBox2.Items[i].ToString(), sga.Column.TableName, sga.Column.ColumnName, sga.Column.AbscissaColumnName, listBox2.Items[i].ToString() + sga.getDisplayedName() + sga.Column.ColumnName);
                            Sacgf.Add_Column_Click(column);
                        }
                        //If the table is not include in the scenario, add them both in the appropriate arraylist
                        else
                        {
                            TableError.Add(SelectedTable.TableName);
                            ScenarioError.Add(listBox2.Items[i].ToString());
                        }
                    }
                    // If the arraylists are not null, display an error message indicating that one or more tables have not been included
                    if (TableError.Count != 0)
                    {
                        String sScenario = "";
                        for (int j = 0; j < ScenarioError.Count; j++)
                        {
                            if (sScenario.Length > 0)
                                sScenario += ", ";
                            sScenario += ScenarioError[j];
                        }
                        DialogResult dr = MessageBox.Show(TableError[0] + " is not available in scenario : " + sScenario + ",\n Continue?", "warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dr == DialogResult.No)
                        {
                            this.DialogResult = DialogResult.None;
                            return;
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            this.DialogResult = DialogResult.Cancel;
                            return;
                        }
                    }
                }
                else
                {
                    // The user checks Compare Chart >> Whole table
                    /*                    SelectedTable = sga.Column;

                                        DataTable table = DonneesEnCours_.getTable(alScenarioNames[0].ToString(), SelectedTable.TableName);
                                        DataColumnCollection dccColum = table.Columns;*/
                    for (int i = 0; i < listBox2.Items.Count; i++)
                    {
                        DataTable table = DonneesEnCours_.getTable(listBox2.Items[i].ToString(), SelectedTable.TableName);

                        if (table != null)
                        {
                            DataColumnCollection dccColum = table.Columns;

                            for (int j = 0; j < dccColum.Count; j++)
                            {
                                if (dccColum[j].ToString().CompareTo(sga.Column.AbscissaColumnName.ToString()) != 0)
                                {
                                    SelectedTable = new ColumnInformation(
                                               listBox2.Items[i].ToString(),
                                               sga.Column.TableName,
                                               dccColum[j].ToString(),
                                               sga.Column.AbscissaColumnName,
                                               listBox2.Items[i].ToString() +
                                               sga.getDisplayedName() +
                                               dccColum[j].ToString());

                                    Sacgf.Add_Column_Click(SelectedTable);
                                }
                            }
                        }                        //If the table is not include in the scenario, add them both in the appropriate arraylist
                        else
                        {
                            TableError.Add(SelectedTable.TableName);
                            ScenarioError.Add(listBox2.Items[i].ToString());
                        }
                    }
                    // If the arraylists are not null, display an error message indicating that one or more tables have not been included
                    if (TableError.Count != 0)
                    {
                        String sScenario = "";
                        for (int j = 0; j < ScenarioError.Count; j++)
                        {
                            if (sScenario.Length > 0)
                                sScenario += ", ";
                            sScenario += ScenarioError[j];
                        }
                        DialogResult dr = MessageBox.Show(TableError[0] + " is not available in scenario : " + sScenario + ",\n Continue?", "warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dr == DialogResult.No)
                        {
                            ScenarioError.Clear();
                            this.DialogResult = DialogResult.None;
                            return;
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            ScenarioError.Clear();
                            this.DialogResult = DialogResult.Cancel;
                            return;
                        }
                    }
                }
                if (Sacgf.ShowDialog() == DialogResult.OK)
                {
                    gf = Sacgf.getFilter();
                    if (gf == null)
                        this.Close();
                    DialogResult = DialogResult.OK;
                }
            }
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                listBox1.Items.Add(listBox2.SelectedItem);
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        internal void setWholeChartMode()
        {
            wholeChartMode = true;
        }
        private void RaiseButton_Click(object sender, EventArgs e)
        {
            object tmpItem;
            if (listBox2.SelectedItem != null && listBox2.SelectedIndex > 0)
            {
                tmpItem = listBox2.SelectedItem;
                listBox2.Items[listBox2.SelectedIndex] = listBox2.Items[listBox2.SelectedIndex - 1];
                listBox2.Items[listBox2.SelectedIndex - 1] = tmpItem;
                listBox2.SelectedItem = listBox2.Items[listBox2.SelectedIndex - 1];
            }
        }

        private void LowerButton_Click(object sender, EventArgs e)
        {
            object tmpItem;
            if (listBox2.SelectedItem != null && listBox2.Items.Count > listBox2.SelectedIndex + 1)
            {
                tmpItem = listBox2.Items[listBox2.SelectedIndex + 1];
                listBox2.Items[listBox2.SelectedIndex + 1] = listBox2.SelectedItem;
                listBox2.Items[listBox2.SelectedIndex] = tmpItem;
                listBox2.SelectedItem = listBox2.Items[listBox2.SelectedIndex + 1];
            }
        }
    }
}
