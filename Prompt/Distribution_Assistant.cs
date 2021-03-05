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
    public partial class Distribution_Assistant : Form
    {
        private bool bValeursOntChangees;
        private GestionDonneesHUB2SIM gdh_Donnees_;
        private DataTable analysedTable;

        private void Initialize()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            txt_FirstParam.BackColor = Color.White;
            txt_SecondParam.BackColor = Color.White;
            txt_ThirdParam.BackColor = Color.White;
            cb_DistributionType.SelectedIndex = 0;
            analysedTable = null;
            bValeursOntChangees = false;
        }


        public Distribution_Assistant(GestionDonneesHUB2SIM gdh_Donnees)
        {
            Initialize();
            gdh_Donnees_ = gdh_Donnees;
        }

        private void cb_DistributionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool secondParam, thirdParam;
            OverallTools.FonctionUtiles.NbParams(cb_DistributionType.Text, out secondParam, out thirdParam);
            txt_SecondParam.Enabled = secondParam;
            txt_ThirdParam.Enabled = thirdParam;
            bValeursOntChangees = true;
        }

        private void btn_CalcDistribution_Click(object sender, EventArgs e)
        {
            gb_CalcDistribution.Visible = !gb_CalcDistribution.Visible;
            if (gb_CalcDistribution.Visible)
            {
                btn_Ok.Top = 370;
                rb_existingTable.Checked = false;
                rb_FromFile.Checked = false;
            }
            else
            {
                btn_Ok.Top = gb_CalcDistribution.Top;
            }
            this.Height = btn_Ok.Top + (this.Height - btn_Cancel.Top);
            btn_Cancel.Top = btn_Ok.Top;
        }

        private void cb_DistributionName_TextChanged(object sender, EventArgs e)
        {
            if (bValeursOntChangees)
            {
                saveChanges();
            }
        }

        private void saveChanges()
        {
            if (MessageBox.Show("The informations have change, do you want to save them to the distribution ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //Sauvegarde des changements
            }
            bValeursOntChangees = false;
        }

        private void Distribution_Assistant_Shown(object sender, EventArgs e)
        {
            btn_CalcDistribution_Click(null, null);
        }

        private void txt_FirstParam_EnabledChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Enabled)
            {
                ((TextBox)sender).BackColor = Color.White;
            }
            else
            {
                ((TextBox)sender).Text = "0";
                ((TextBox)sender).BackColor = Color.FromArgb(236, 233, 216);
            }
        }

        private void rb_existingTable_CheckedChanged(object sender, EventArgs e)
        {
            bool bValue = rb_existingTable.Checked;
            cb_Scenario.Enabled = bValue;
            cb_Tables.Enabled = bValue;
            lbl_Table.Enabled = bValue;
            if (bValue)
            {
                rb_FromFile.Checked = false;
                List<String> ScenarioNames = gdh_Donnees_.getScenarioNames();
                String oldValue = cb_Scenario.Text;
                cb_Scenario.Text = "";
                cb_Scenario.Items.Clear();
                cb_Scenario.Items.Add("Input");
                foreach (String name in ScenarioNames)
                {
                    cb_Scenario.Items.Add(name);
                }
                if (cb_Scenario.Items.Contains(oldValue))
                {
                    cb_Scenario.Text = oldValue;
                }
            }
            cb_Tables_SelectedIndexChanged(null, null);
        }

        private void rb_FromFile_CheckedChanged(object sender, EventArgs e)
        {

            bool bValue = rb_FromFile.Checked;
            lbl_NameFile.Enabled = bValue;
            btn_Open.Enabled = bValue;
            if (bValue)
            {
                rb_existingTable.Checked = false;
                bool bEnabled = (lbl_NameFile.Text != "");
                String oldValue = cb_Column.Text;
                cb_Column.Items.Clear();
                cb_Column.Text = "";
                if (bEnabled)
                {
                    if (!System.IO.File.Exists(lbl_NameFile.Text))
                    {
                        lbl_NameFile.Text = "";
                        bEnabled = false;
                        analysedTable = null;
                    }
                    else
                    {
                        analysedTable = new DataTable();
                        OverallTools.FonctionUtiles.LectureFichier(analysedTable, lbl_NameFile.Text, "\t",null);
                        MAJ_ColumnName(analysedTable, false);
                    }
                }
                lbl_Column.Enabled = bEnabled;
                cb_Column.Enabled = bEnabled;
                if (cb_Column.Items.Contains(oldValue))
                    cb_Column.SelectedItem = oldValue;
            }
        }

        private void txt_FirstParam_TextChanged(object sender, EventArgs e)
        {
            bValeursOntChangees = true;
        }

        private void cb_Column_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Analyse.Enabled = (cb_Column.Text != "");
        }

        private void cb_Scenario_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            String oldValue = cb_Tables.Text;
            cb_Tables.Text = "";
            cb_Tables.Items.Clear();
            if (cb_Scenario.Text != "")
            {
                String[] Names = gdh_Donnees_.getTableNames(cb_Scenario.Text);
                if (Names != null)
                {
                    foreach (String name in Names)
                    {
                        cb_Tables.Items.Add(name);
                    }
                }
            }
            if (cb_Tables.Items.Contains(oldValue))
            {
                cb_Tables.SelectedItem = oldValue;
            }*/
        }

        private void cb_Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!rb_existingTable.Checked)
                return;
            bool bEnabled = (cb_Tables.Text != "");

            String oldValue = cb_Column.Text;
            lbl_Column.Enabled = bEnabled;
            cb_Column.Enabled = bEnabled;
            cb_Column.Text = "";
            cb_Column.Items.Clear();
            if (bEnabled)
            {
                analysedTable = gdh_Donnees_.getTable(cb_Scenario.Text, cb_Tables.Text);
                MAJ_ColumnName(analysedTable, true);
                if (cb_Column.Items.Contains(oldValue))
                {
                    cb_Column.SelectedItem = oldValue;
                }
            }
        }
        private void MAJ_ColumnName(DataTable table, bool verifType)
        {
            if (table == null)
                return;
            foreach (DataColumn colonne in table.Columns)
            {
                if ((((colonne.DataType == typeof(Double)) ||
                    (colonne.DataType == typeof(double)) ||
                    (colonne.DataType == typeof(Int16)) ||
                    (colonne.DataType == typeof(int)) ||
                    (colonne.DataType == typeof(Int32)) ||
                    (colonne.DataType == typeof(Int64))) && verifType) || (!verifType))
                {
                    cb_Column.Items.Add(colonne.ColumnName);
                }
            }
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            ofd_OpenFile.FileName = lbl_NameFile.Text;
            if (ofd_OpenFile.ShowDialog() != DialogResult.Cancel)
            {
                lbl_NameFile.Text = ofd_OpenFile.FileName;
                rb_FromFile_CheckedChanged(null, null);
            }
        }

        public class Distribution_Class
        {
            private String DistributionName_;
            private Double FirstParameter_;
            private Double SecondParameter_;
            private Double FirstRank_;
            private Double SecondRank_;

            #region Accesseurs
            public String DistributionName
            {
                get
                {
                    return DistributionName_;
                }
            }

            public Double FirstParameter
            {
                get
                {
                    return FirstParameter_;
                }
            }
            public Double SecondParameter
            {
                get
                {
                    return SecondParameter_;
                }
            }
            public Double FirstRank
            {
                get
                {
                    return FirstRank_;
                }
            }
            public Double SecondRank
            {
                get
                {
                    return SecondRank_;
                }
            }
            #endregion

            public String Parameters
            {
                get
                {
                    
                    if (SecondParameter_ != 0)
                    {
                        return "( " + Math.Round(FirstParameter_, 2).ToString() + " ; " + Math.Round(SecondParameter_, 2).ToString() + " ) ";
                    }
                    else
                    {
                        return "( " + Math.Round(FirstParameter_, 2).ToString() + " ) ";
                    }
                }
            }

            public String Rank
            {
                get
                {
                    if (SecondParameter_ != 0)
                    {
                        return "( " + Math.Round(FirstRank, 2).ToString() + " ; " + Math.Round(SecondRank, 2).ToString() + " )";
                    }
                    else
                    {
                        return "( " + Math.Round(FirstRank, 2).ToString() + " )";
                    }
                }
            }
            public Distribution_Class(String DistributionName__, Double FirstParameter__, Double SecondParameter__,Double FirstRank__,Double SecondRank__)
            {
                DistributionName_ = DistributionName__;

                FirstParameter_ = FirstParameter__;
                SecondParameter_ = SecondParameter__;
                FirstRank_ = FirstRank__;
                SecondRank_ = SecondRank__;
            }
            public override string ToString()
            {
                if(SecondParameter_ != 0){
                    return DistributionName_ + "   ( " + Math.Round(FirstParameter_, 2).ToString() + " ; " + Math.Round(SecondParameter_, 2).ToString() + " )      sd : ( " + Math.Round(FirstRank, 2).ToString() + " ; " + Math.Round(SecondRank, 2).ToString() + " )";
                }
                else
                {
                    return DistributionName_ + "    ( " + Math.Round(FirstParameter_,2).ToString() + " )       sd : ( "+Math.Round(FirstRank,2).ToString() + " )";
                }
            }
        }

        private void btn_Analyse_Click(object sender, EventArgs e)
        {
            /*
            valid = new bool[distribution.Length];
            int i;
            for (i = 0; i < distribution.Length; i++)
            {
                valid[i] = true;
            }
            STATCONNECTORSRVLib.StatConnectorClass Connection=null;
            try
            {
                Connection = new STATCONNECTORSRVLib.StatConnectorClass();
                Connection.Init("R");
                Connection.EvaluateNoReturn("library(MASS)");
                String values = generateString(analysedTable, cb_Column.Text);
                if (values == null)
                    return;
                Connection.EvaluateNoReturn("distrib<-c(" + values + ")");
            }
            catch (Exception)
            {
                MessageBox.Show("Some errors appear during the connection to R (tool used to calcs the fits).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OverallTools.ExternFunctions.PrintLogFile("Except020: " + this.GetType().ToString() + " throw an exception: " + e.Message);
            }
            ArrayList Distrib = new ArrayList(); ;
            for (i = 0; i < distribution.Length; i++)
            {
                try
                {
                    Double[] Parameters = new Double[2] {0,0};
                    Double[] Ranks = new Double[2] {0,0};
                    Connection.EvaluateNoReturn("resultat<-fitdistr(distrib,\"" + distribution[i] + "\")");
                    String aff = distribution[i] + "(";
                    for (int j = 1; j <= distributionParameters[i]; j++)
                    {
                        if(!Double.TryParse(Connection.Evaluate("resultat$estimate["+j+"]").ToString(),out Parameters[j-1] ))
                        {
                            Parameters[j-1] = 0;
                        }

                        if (!Double.TryParse(Connection.Evaluate("resultat$sd[" + j + "]").ToString(), out Ranks[j-1]))
                        {
                            Ranks[j-1] = 0;
                        }
                    }
                    Distrib.Add( new Distribution_Class(distribution[i], Parameters[0], Parameters[1], Ranks[0], Ranks[1]));
            //        MessageBox.Show(aff.Substring(0,aff.Length - 4)+")");
                }
                catch (Exception)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Except020: " + this.GetType().ToString() + " throw an exception: " + e.Message);
                    valid[i] = false;
                }
            }

            Distribution_Display display = new Distribution_Display(Distrib);
            if (display.ShowDialog() == DialogResult.OK)
            {
                String result = display.GetSelectedDistribution();
                if (result != null)
                {
                    foreach (Distribution_Class dis in Distrib)
                    {
                        if (dis.DistributionName == result)
                        {
                            cb_DistributionType.SelectedIndex = cb_DistributionType.Items.IndexOf(result);
                            txt_FirstParam.Text = Math.Round(dis.FirstParameter,5).ToString();
                            txt_SecondParam.Text = Math.Round(dis.SecondParameter, 5).ToString();
                            txt_ThirdParam.Text = "";
                        }
                    }
                }

            }
            Connection.Close();*/
        }
        private String generateString(DataTable table, String columnName)
        {
            if (!table.Columns.Contains(columnName))
                return null;
            if ((table.Columns[columnName].DataType == typeof(DateTime)) ||
               (table.Columns[columnName].DataType == typeof(TimeSpan)))
                return null;
            if ((table.Columns[columnName].DataType == typeof(String)) ||
               (table.Columns[columnName].DataType == typeof(string)))
            {
                double dValue;
                foreach (DataRow ligne in table.Rows)
                {
                    if (!Double.TryParse(ligne[columnName].ToString(), out dValue))
                    {
                        return null;
                    }
                }
            }
            else if (!((table.Columns[columnName].DataType == typeof(Double)) ||
                    (table.Columns[columnName].DataType == typeof(double))||
                    (table.Columns[columnName].DataType == typeof(int)) ||
                    (table.Columns[columnName].DataType == typeof(Int16)) ||
                    (table.Columns[columnName].DataType == typeof(Int32)) ||
                    (table.Columns[columnName].DataType == typeof(Int64))))
            {
                //Il s'agit d'un type non pris en charge.
                return null;
            }
            String resultat = "";
            foreach (DataRow ligne in table.Rows)
            {
                resultat += ligne[columnName].ToString() + ",";
            }
            return resultat.Substring(0, resultat.Length - 1);
        }

        public String Distribution
        {
            get
            {
                return cb_DistributionType.Text;
            }
        }
        public Double Param1
        {
            get
            {
                Double tmp;
                if (!Double.TryParse(txt_FirstParam.Text, out tmp))
                {
                    return 0;
                }
                return tmp;
            }
        }
        public Double Param2
        {
            get
            {
                Double tmp;
                if (!Double.TryParse(txt_SecondParam.Text, out tmp))
                {
                    return 0;
                }
                return tmp;
            }
        }
        public Double Param3
        {
            get
            {
                Double tmp;
                if (!Double.TryParse(txt_ThirdParam.Text, out tmp))
                {
                    return 0;
                }
                return tmp;
            }
        }
    }
}