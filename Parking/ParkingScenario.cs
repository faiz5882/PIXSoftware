using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Parking
{
    public partial class ParkingScenario : Form
    {
        private GestionDonneesHUB2SIM gdhData;

        #region Class permettant de connaître les tables utilisées à l'origine pour le scénario.
        internal class ParkingInformation
        {
            internal String sParkingGeneralTable;
            internal String sParkingOccupationTable;
            internal String sParkingDistributionTable;
            internal String sParkingModadDistributionTable;
        }
        #endregion

        #region Accesseurs
        internal String ScenarioName
        {
            get
            {
                return cb_Scenario.Text;
            }
        }
        internal String GeneralTable
        {
            get
            {
                return  DataManagement.DataManagerInput.ConvertToShortName(cb_GeneralTable.Text);
            }
        }

        internal String OccupationTable
        {
            get
            {
                return DataManagement.DataManagerInput.ConvertToShortName(cb_Occupation.Text);
            }
        }

        internal String DistributionTable
        {
            get
            {
                return DataManagement.DataManagerInput.ConvertToShortName(cb_Distribution.Text);
            }
        }

        internal String ModadDistributionTable
        {
            get
            {
                return DataManagement.DataManagerInput.ConvertToShortName(cb_ModalDistribution.Text);
            }
        }


        internal bool UseGeneralTable
        {
            get
            {
                return chk_GeneralTable.Checked;
            }
        }

        internal bool UseOccupationTable
        {
            get
            {
                return chk_Occupation.Checked;
            }
        }

        internal bool UseDistributionTable
        {
            get
            {
                return chk_Distribution.Checked;
            }
        }

        internal bool UseModadDistributionTable
        {
            get
            {
                return chk_ModalDistribution.Checked;
            }
        }
        #endregion

        public ParkingScenario(GestionDonneesHUB2SIM gdhData_, String sScenarioName)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            gdhData = gdhData_;
            cb_GeneralTable.Tag = GlobalNames.sParkingGeneralName;
            cb_Occupation.Tag = GlobalNames.sParkingOccupationTimeName;
            cb_Distribution.Tag = GlobalNames.sParkingDitributionTimeName;
            cb_ModalDistribution.Tag = GlobalNames.sParkingModaleDistribName;

            UpdateComboBox(cb_GeneralTable, true);
            UpdateComboBox(cb_ModalDistribution, true);
            UpdateComboBox(cb_Distribution, true);
            UpdateComboBox(cb_Occupation, true);
            if (gdhData.getScenarioParking() != null)   // << ParkingScenario Bug when opening Scenario Properties
                cb_Scenario.Items.AddRange(gdhData.getScenarioParking().ToArray());


            if ((sScenarioName == "") || (sScenarioName == null))
            {
                int i = 1;
                String sScenario = "Scenario ";
                while (gdhData_.GetDataManager(sScenario + i.ToString()) != null)
                    i++;
                cb_Scenario.Text = sScenario + i.ToString();
            }
            else
            {
                cb_Scenario.Text = sScenarioName;
            }
        }

        private void cb_Scenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Scenario.Items.Contains(cb_Scenario.Text))
            {
                DataManagement.DataManager pp = gdhData.GetDataManager(cb_Scenario.Text);
                if (!typeof(DataManagement.DataManagerParking).IsInstanceOfType(pp))
                {
                    MessageBox.Show("The scenario " + cb_Scenario.Text + " is not a valid parking scenario. Please select another scenario");
                    return;
                }
                setScenario((DataManagement.DataManagerParking)pp);
            }
            else
            {
                chk_ModalDistribution.Enabled = false;
                chk_Distribution.Enabled = false;
                chk_GeneralTable.Enabled = false;
                chk_Occupation.Enabled = false;
                chk_ModalDistribution.Checked = true;
                chk_Distribution.Checked = true;
                chk_GeneralTable.Checked = true;
                chk_Occupation.Checked = true;
            }
        }

        private void setScenario(DataManagement.DataManagerParking pp)
        {
            cb_Distribution.Text = pp.DistributionTableName;
            cb_GeneralTable.Text = pp.GeneralTableName;
            cb_ModalDistribution.Text = pp.ModalDistributionTableName;
            cb_Occupation.Text = pp.OccupationTableName;

            chk_ModalDistribution.Enabled = true;
            chk_Distribution.Enabled = true;
            chk_GeneralTable.Enabled = true;
            chk_Occupation.Enabled = true;
            chk_ModalDistribution.Checked = false;
            chk_Distribution.Checked = false;
            chk_GeneralTable.Checked = false;
            chk_Occupation.Checked = false;
        }
        internal ParkingInformation getScenario()
        {
            ParkingInformation piInformation = new ParkingInformation();
            piInformation.sParkingDistributionTable = cb_Distribution.Text;
            piInformation.sParkingGeneralTable=cb_GeneralTable.Text ;
            piInformation.sParkingModadDistributionTable=cb_ModalDistribution.Text;
            piInformation.sParkingOccupationTable=cb_Occupation.Text;
            return piInformation;
        }
        private void cb_Scenario_TextChanged(object sender, EventArgs e)
        {
            if ((cb_Scenario.Text.Contains("/")) || (cb_Scenario.Text.Contains("\\")))
            {
                String sScenario = cb_Scenario.Text;

                sScenario = sScenario.Replace("/", "_");
                sScenario = sScenario.Replace("\\", "_");
                cb_Scenario.Text = sScenario;
                cb_Scenario.SelectionStart = cb_Scenario.Text.Length;
                return;
            }
            cb_Scenario_SelectedIndexChanged(sender, e);
        }

        private void UpdateComboBox(ComboBox cbTmp, bool bEnabled)
        {
            if (cbTmp == null)
                return;
            String sOldValue = cbTmp.Text;
            cbTmp.Items.Clear();

            if (!bEnabled)
            {
                cbTmp.Text = "";
                return;
            }
            List<String> values = gdhData.getValidTables("Input", (String)cbTmp.Tag);
            foreach (String value in values)
            {
                OverallTools.FonctionUtiles.AddSortedItem(cbTmp, DataManagement.DataManagerInput.ConvertToFullName(value));
            }
            cbTmp.Text = sOldValue;
            if (((cbTmp.Text == null) || (cbTmp.Text.Length == 0)) && (cbTmp.Items.Count != 0))
            {
                int iDefault = cbTmp.Items.IndexOf(DataManagement.DataManagerInput.ConvertToFullName(cbTmp.Tag.ToString()));
                if (iDefault == -1)
                    iDefault = 0;
                cbTmp.SelectedIndex = iDefault;
            }
        }

        private void chk_GeneralTable_CheckedChanged(object sender, EventArgs e)
        {
            cb_GeneralTable.Enabled = chk_GeneralTable.Checked;
        }

        private void chk_Distribution_CheckedChanged(object sender, EventArgs e)
        {
            cb_Distribution.Enabled = chk_Distribution.Checked;
        }

        private void chk_ModalDistribution_CheckedChanged(object sender, EventArgs e)
        {
            cb_ModalDistribution.Enabled = chk_ModalDistribution.Checked;
        }

        private void chk_Occupation_CheckedChanged(object sender, EventArgs e)
        {
            cb_Occupation.Enabled = chk_Occupation.Checked;
        }

    }
}

