using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SIMCORE_TOOL.com.crispico.charts;

namespace SIMCORE_TOOL.Assistant
{
    public partial class Oneof_Assistant : Form
    {
        private DataTable OneofSpecification_;
        private DataTable OneofSpecification_TMP;
        GraphicFilter gfGraphic = null;
        private int iAncienneIndex;
        private bool bAEteModifie;
        public String SelectedDistribution;
        private void Initialize(DataTable OneofSpecification)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            OneofSpecification_ = OneofSpecification;
            if ((OneofSpecification_.Columns.Count % 2) != 0)
            {
                OneofSpecification_.Columns.Clear();
            }
            else
            {
                foreach (DataColumn Name in OneofSpecification_.Columns)
                {
                    if (Name.ColumnName.LastIndexOf("Value") == (Name.ColumnName.Length - 5))
                        continue;
                    if (Name.ColumnName.LastIndexOf("Frequency") == -1)
                        continue;
                    String DistributionName = Name.ColumnName.Substring(0, Name.ColumnName.LastIndexOf("Frequency") - 1);
                    CB_DistribName.Items.Add(DistributionName);
                }
            }
            OneofSpecification_TMP = OneofSpecification.Copy();
            bAEteModifie = false;
            iAncienneIndex = -1;
            SelectedDistribution = "";

            ArrayList ColumnsNames = new ArrayList();
            ArrayList ColumnsOrigin = new ArrayList();
            ArrayList Visualisation = new ArrayList();
            ArrayList AxeRepresentation = new ArrayList();
            ArrayList StrokeCouleurs = new ArrayList();
            ArrayList FillCouleurs = new ArrayList();
            ArrayList Accumulation = new ArrayList();
            ArrayList Position = new ArrayList();
            List<Boolean> PositionScrollbar = new List<Boolean>();
            ArrayList ShowValues = new ArrayList();

            List<float> PositionsBarWidth = new List<float>();
            List<float> PositionsGapPercent = new List<float>();
            List<float> PositionsBarWidthPercent = new List<float>();
            
            ArrayList listSetPointStrokeColor = new ArrayList();
            ArrayList listSetPointFillColor = new ArrayList();
            List<Boolean> listSetPointIsArea = new List<bool>();
            List<Boolean> listSetPointIsActivated = new List<bool>();
            List<double> listSetPointValue = new List<double>();
            List<double> listSetPointValue2 = new List<double>();
            List<DateTime> listSetPointBDateTime = new List<DateTime>();
            List<DateTime> listSetPointEDateTime = new List<DateTime>();
            List<SIMCORE_TOOL.GraphicFilter.Notes> ListAnnotation = new List<SIMCORE_TOOL.GraphicFilter.Notes>();
            List<String> listSetPointAxis = new List<String>();

            ArrayList CandleColumnsList = new ArrayList();
            ArrayList MaxCandleValue = new ArrayList();
            ArrayList MidCandleValue = new ArrayList();
            ArrayList MinCandleValue = new ArrayList();
            List<List<SIMCORE_TOOL.GraphicFilter.Axe>> AxisList ;

            ///On initialise ici 4 listes de 3 axes avec des valeurs par défaut
            AxisList = new List<List<SIMCORE_TOOL.GraphicFilter.Axe>>();
            for (int i = 0; i < 4; i++)
            {
                List<SIMCORE_TOOL.GraphicFilter.Axe> axis = new List<SIMCORE_TOOL.GraphicFilter.Axe>();
                for (int j = 0; j < 3; j++)
                {
                    SIMCORE_TOOL.GraphicFilter.Axe axe = new SIMCORE_TOOL.GraphicFilter.Axe();
                    axe.BeginDTValue = new DateTime(1, 1, 1);
                    axe.BeginNValue = -1;
                    axe.IsDateTime = false;
                    axe.LengthDTValue = -1;
                    axe.LengthNValue = -1;
                    axis.Add(axe);
                }
                AxisList.Add(axis);
            }

            ColumnsNames.Add("Value");
            ColumnsNames.Add("Frequency");
            ColumnsOrigin.Add("");
            ColumnsOrigin.Add("");
            Visualisation.Add("Line");
            Visualisation.Add("Bar");
            AxeRepresentation.Add("X");
            AxeRepresentation.Add("Y");
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            StrokeCouleurs.Add(new Nevron.GraphicsCore.NStrokeStyle(OverallTools.FonctionUtiles.getColor(0)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            FillCouleurs.Add(new Nevron.GraphicsCore.NColorFillStyle(OverallTools.FonctionUtiles.getColor(0)));
            Accumulation.Add(Color.Transparent);
            Accumulation.Add(OverallTools.FonctionUtiles.getColorStroke(0));
           

            Position.Add(0);
            Position.Add(0);
            ShowValues.Add(false);
            ShowValues.Add(false);
            
            
            gfGraphic = new GraphicFilter(ColumnsNames, 
                ColumnsOrigin, 
                Visualisation, 
                AxeRepresentation, 
                StrokeCouleurs, 
                FillCouleurs, 
                Accumulation,
                listSetPointAxis,
                listSetPointIsArea,
                listSetPointStrokeColor,
                listSetPointFillColor,
                listSetPointValue,
                listSetPointValue2,
                listSetPointBDateTime ,
                listSetPointEDateTime, 
                listSetPointIsActivated,
                ListAnnotation,
                CandleColumnsList,
                MaxCandleValue,
                MidCandleValue,
                MinCandleValue,
                AxisList,
                Position,
                PositionScrollbar,
                ShowValues, 
                "One of", 
                "", 
                false, 
                "", 
                "", 
                "",
                true, PositionsBarWidth, PositionsGapPercent, PositionsBarWidthPercent, new List<SetPoint>());  // << Task #9624 Pax2Sim - Charts - checkBox for scenario name

        }
        public Oneof_Assistant(DataTable OneofSpecification)
        {
            Initialize(OneofSpecification);
        }

        private void CB_DistribName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iAncienneIndex == CB_DistribName.SelectedIndex)
                return;
            int iIndexFrequency, iIndexValue;
            if ((iAncienneIndex != -1)&&(bAEteModifie))
            {
                int i;
                DataTable tmp = (DataTable)DGV_Table.DataSource;
                if (!OneofSpecification_TMP.Columns.Contains(CB_DistribName.Items[iAncienneIndex].ToString() + " Frequency"))
                {
                    OneofSpecification_TMP.Columns.Add(CB_DistribName.Items[iAncienneIndex].ToString() + " Frequency", typeof(Double));
                    OneofSpecification_TMP.Columns.Add(CB_DistribName.Items[iAncienneIndex].ToString() + " Value", typeof(Double));
                }
                if (tmp.Rows.Count > OneofSpecification_TMP.Rows.Count)
                {
                    for (i = OneofSpecification_TMP.Rows.Count; i < tmp.Rows.Count; i++)
                    {
                        DataRow newRow = OneofSpecification_TMP.NewRow();
                        for (int j = 0; j < OneofSpecification_TMP.Columns.Count; j++)
                        {
                            newRow[j] = 0;
                        }
                        OneofSpecification_TMP.Rows.Add(newRow);
                    }
                }
                iIndexFrequency = OneofSpecification_TMP.Columns.IndexOf(CB_DistribName.Items[iAncienneIndex].ToString() + " Frequency");
                iIndexValue = OneofSpecification_TMP.Columns.IndexOf(CB_DistribName.Items[iAncienneIndex].ToString() + " Value");
                if ((iIndexFrequency == -1) || (iIndexValue == -1))
                    return;

                for (i = 0; i < tmp.Rows.Count; i++)
                {
                    OneofSpecification_TMP.Rows[i][iIndexFrequency] = (Double)tmp.Rows[i][0];
                    OneofSpecification_TMP.Rows[i][iIndexValue] = (Double)tmp.Rows[i][1];
                }
                for (i = tmp.Rows.Count; i < OneofSpecification_TMP.Rows.Count; i++)
                {
                    OneofSpecification_TMP.Rows[i][iIndexFrequency] = 0;
                    OneofSpecification_TMP.Rows[i][iIndexValue] = 0;
                }
            }
            bool bSelected = (CB_DistribName.SelectedIndex != -1);
            BTN_AddFrequency.Enabled = bSelected;
            //BTN_Delete.Enabled = bSelected;
            BTN_DeleteAll.Enabled = bSelected;
            DGV_Table.Enabled = bSelected;
            DGV_Table.DataSource = null;
            bAEteModifie = false;
            iAncienneIndex = CB_DistribName.SelectedIndex;
            if (!bSelected)
                return;
            DataTable NewTable = new DataTable();
            NewTable.Columns.Add("Frequency",typeof(Double));
            NewTable.Columns.Add("Value", typeof(Double));
            DGV_Table.DataSource = NewTable;
             iIndexFrequency = OneofSpecification_TMP.Columns.IndexOf(CB_DistribName.Text + " Frequency");
             iIndexValue = OneofSpecification_TMP.Columns.IndexOf(CB_DistribName.Text + " Value");
            if ((iIndexFrequency != -1) && (iIndexValue != -1))
            {
                foreach (DataRow ligne in OneofSpecification_TMP.Rows)
                {
                    if (((Double)ligne[iIndexFrequency]) == 0)
                        break;
                    DataRow newLine = NewTable.NewRow();
                    newLine[0] = ligne[iIndexFrequency];
                    newLine[1] = ligne[iIndexValue];
                    NewTable.Rows.Add(newLine);
                }
            }
            DGV_Table.DataSource = OverallTools.DataFunctions.sortTable(NewTable, "Value");
            UpdateGraphic();
            BTN_DeleteAll.Enabled = (DGV_Table.Rows.Count != 0);
        }

        private void DGV_Table_SelectionChanged(object sender, EventArgs e)
        {
            BTN_Delete.Enabled = (DGV_Table.SelectedRows.Count != 0);
            BTN_Edit.Enabled = (DGV_Table.SelectedRows.Count != 0);
            BTN_DeleteAll.Enabled = (DGV_Table.Rows.Count != 0);
        }

        private void BTN_AddDistrib_Click(object sender, EventArgs e)
        {
            Prompt.SIM_ChooseName Assistant = new SIMCORE_TOOL.Prompt.SIM_ChooseName("Distribution name", "Choose a distribution name", "");
            if (Assistant.ShowDialog() == DialogResult.OK)
            {
                if ((OverallTools.FonctionUtiles.estPresentDansListe(Assistant.getName(), GestionDonneesHUB2SIM.DistributionsNames)) ||
                    (SIMCORE_TOOL.Assistant.SubForms.Distribution_SubForm.AddNewDistribution == Assistant.getName()))
                {
                    MessageBox.Show("This name can't be used to design a distribution name. Please choose another one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!CB_DistribName.Items.Contains(Assistant.getName()))
                {
                    CB_DistribName.Items.Add(Assistant.getName());
                }
                CB_DistribName.SelectedIndex = CB_DistribName.Items.IndexOf(Assistant.getName());
            }
        }

        private void BTN_AddFrequency_Click(object sender, EventArgs e)
        {
            bAEteModifie = true;
            Oneof_AddFrequency_Assistant Assistant = new Oneof_AddFrequency_Assistant();
            if (Assistant.ShowDialog() == DialogResult.OK)
            {
                DataTable tmp = ((DataTable)DGV_Table.DataSource);
                int i;
                for (i = 0; i < tmp.Rows.Count; i++)
                {
                    if (((Double)tmp.Rows[i][1]) == Assistant.Value_)
                    {
                        break;
                    }
                }
                if (i < tmp.Rows.Count)
                {
                    tmp.Rows[i][0] = ((Double)tmp.Rows[i][0]) + Assistant.Frequency_;
                }
                else
                {
                    tmp.Rows.Add(new object[] { Assistant.Frequency_, Assistant.Value_ });
                }
            }
            UpdateGraphic();
        }

        private void BTN_Edit_Click(object sender, EventArgs e)
        {
            bAEteModifie = true;
            if (DGV_Table.SelectedRows.Count == 0)
            {
                BTN_Edit.Enabled = false;
                return;
            }
            DataTable tmp = (DataTable)DGV_Table.DataSource;
            Double Frequency = (Double)DGV_Table.SelectedRows[0].Cells[0].Value;
            Double Value = (Double)DGV_Table.SelectedRows[0].Cells[1].Value;
            Oneof_AddFrequency_Assistant Assistant = new Oneof_AddFrequency_Assistant(Frequency,Value);
            if (Assistant.ShowDialog() == DialogResult.OK)
            {
                int i;
                for (i = 0; i < tmp.Rows.Count; i++)
                {
                    if (((Double)tmp.Rows[i][1]) == Value)
                        break;
                }
                if (i == tmp.Rows.Count)
                    return;
                if (Value == Assistant.Value_)
                {
                    tmp.Rows[i][0] = Assistant.Frequency_;
                }
                else
                {
                    int iCurrent = i;
                    for (i = 0; i < tmp.Rows.Count; i++)
                    {
                        if ((((Double)tmp.Rows[i][1]) == Assistant.Value_)&&(iCurrent != i))
                            break;
                    }

                    if (i != tmp.Rows.Count)
                    {
                        tmp.Rows[i][0] = ((Double)tmp.Rows[i][0]) + Assistant.Frequency_;
                        tmp.Rows.RemoveAt(iCurrent);
                    }
                    else
                    {
                        tmp.Rows[iCurrent][0] = Assistant.Frequency_;
                        tmp.Rows[iCurrent][1] = Assistant.Value_;
                    }
                }
            }
            UpdateGraphic();
        }

        private void BTN_Delete_Click(object sender, EventArgs e)
        {
            bAEteModifie = true;
            DataTable tmp = (DataTable)DGV_Table.DataSource;
            ArrayList alDeleteValues = new ArrayList();
            foreach (DataGridViewRow ligne in DGV_Table.SelectedRows)
            {
                alDeleteValues.Add((Double)ligne.Cells[1].Value);
            }

            for (int i = 0; i < tmp.Rows.Count; i++)
            {
                if(alDeleteValues.Contains((Double)tmp.Rows[i][1]))
                {
                    tmp.Rows.RemoveAt(i);
                    i--;
                }
            }
            UpdateGraphic();
        }

        private void BTN_DeleteAll_Click(object sender, EventArgs e)
        {
            bAEteModifie = true;
            ((DataTable)DGV_Table.DataSource).Rows.Clear();
            UpdateGraphic();
        }

        private void BTN_Ok_Click(object sender, EventArgs e)
        {
            SelectedDistribution = CB_DistribName.Text;
            CB_DistribName.SelectedIndex = -1;
            OneofSpecification_.Rows.Clear();
            OneofSpecification_.Columns.Clear();

            foreach (DataColumn column in OneofSpecification_TMP.Columns)
            {
                OneofSpecification_.Columns.Add(column.ColumnName, column.DataType);
            }
            foreach (DataRow ligne in OneofSpecification_TMP.Rows)
            {
                OneofSpecification_.Rows.Add(ligne.ItemArray);
            }
        }

        private void UpdateGraphic()
        {
            nccGraphique.Charts.Clear();
            nccGraphique.Refresh();
            if ((DGV_Table.DataSource == null) || (DGV_Table.DataSource.GetType() != typeof(DataTable)))
                return;

            DataTable dtSource = (DataTable)DGV_Table.DataSource;
            if (dtSource.Rows.Count == 0)
                return;
            if (gfGraphic == null)
                return;

            nccGraphique.Charts.Clear();
            gfGraphic.CreateGraphicZone(nccGraphique, dtSource, null);
            nccGraphique.Refresh();
        }
    }
}