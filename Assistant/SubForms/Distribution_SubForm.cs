using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant.SubForms
{
    public partial class Distribution_SubForm : Form
    {
        private bool bValeursOntChangees;
        private bool bSetValues;
        private GestionDonneesHUB2SIM gdh_Datas_;
        private DataTable dtOneofDistribution_;
        public static String AddNewDistribution = "Add new...";

        private void Initialize()
        {
            InitializeComponent();
            txt_FirstParam.BackColor = Color.White;
            txt_SecondParam.BackColor = Color.FromArgb(236, 233, 216);
            txt_ThirdParam.BackColor = Color.FromArgb(236, 233, 216);
            cb_OneOf.SelectedIndex = -1;
            cb_DistributionType.SelectedIndex = -1;
            cb_DistributionType.SelectedIndex = 0;
            bValeursOntChangees = false;
            bSetValues = false;
        }
        public Distribution_SubForm(GestionDonneesHUB2SIM gdh_Datas__,DataTable dtOneofDistribution, String distribution, Double fValue, Double sValue, Double tValue)
        {
            Initialize();
            dtOneofDistribution_ = dtOneofDistribution;
            setValue(distribution, fValue, sValue, tValue);
            gdh_Datas_ = gdh_Datas__;
        }
        public void setValue(String distribution, Double fValue, Double sValue, Double tValue)
        {
            if (!cb_DistributionType.Items.Contains(distribution))
            {
                bSetValues = true;
                cb_DistributionType.SelectedIndex = cb_DistributionType.Items.Count - 1;
                cb_OneOf_Enter(null, null);
                cb_OneOf.Text = distribution;
                txt_FirstParam.Text = "0";
                txt_SecondParam.Text = "0";
                txt_ThirdParam.Text = "0";
                bSetValues = false;
                bValeursOntChangees = false;
                return;
            }
            bSetValues = true;
            cb_DistributionType.Text = distribution;
            txt_FirstParam.Text = fValue.ToString();
            txt_SecondParam.Text = sValue.ToString();
            txt_ThirdParam.Text = tValue.ToString();
            bSetValues = false;
            bValeursOntChangees = false;
        }
        public void HideLabel()
        {
            lbl_Moving.Visible = false;
        }

        private void cb_DistributionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool secondParam, thirdParam;
            toolTip1.SetToolTip(cb_DistributionType, cb_DistributionType.Text);
            if (cb_DistributionType.Text == "Probability profiles")
            {
                txt_FirstParam.Visible = false;
                txt_SecondParam.Visible = false;
                txt_ThirdParam.Visible = false;
                cb_OneOf.Visible = true;
            }
            else
            {
                txt_FirstParam.Visible = true;
                txt_SecondParam.Visible = true;
                txt_ThirdParam.Visible = true;
                OverallTools.FonctionUtiles.NbParams(cb_DistributionType.Text, out secondParam, out thirdParam);
                txt_SecondParam.Enabled = secondParam;
                txt_ThirdParam.Enabled = thirdParam;
                bValeursOntChangees = true;
                cb_OneOf.Visible = false;
            }
            if (bSetValues)
                return;
            if (this.Parent != null)
            {
                if (this.Parent.GetType() == typeof(Panel))
                {
                    this.Parent.Refresh();
                }
            }
        }

        private void txt_FirstParam_EnabledChanged(object sender, EventArgs e)
        {
            /*if (!((TextBox)sender).Visible)
                return;*/
            if (!((TextBox)sender).Parent.Enabled )
                return;
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

        private void txt_FirstParam_TextChanged(object sender, EventArgs e)
        {
            if (!((TextBox)sender).Visible)
                return;
            if (bSetValues)
                return;
            bValeursOntChangees = true;
            TextBox tmp = ((TextBox)sender);
            Double value;
            if (!Double.TryParse(tmp.Text, out value))
            {
                MessageBox.Show("The parameters must be numerical values", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (this.Parent != null)
            {
                if (this.Parent.GetType() == typeof(Panel))
                {
                    this.Parent.Refresh();
                }
            }
        }
        public bool estValide()
        {
            if (cb_DistributionType.Text == "")
                return false;
            Double fValue, sValue, tValue;
            if ((!Double.TryParse(txt_FirstParam.Text, out fValue)) ||
                (!Double.TryParse(txt_SecondParam.Text, out sValue)) ||
                (!Double.TryParse(txt_ThirdParam.Text, out tValue)))
            {
                return false;
            }
            String error;

            // << Task #8758 Pax2Sim - Editor for Groups
            if (cb_DistributionType.Text == GlobalNames.PROBABILITY_PROFILES_DISTRIBUTION)
            {
                if (cb_OneOf.Text == "")
                {
                    error = "Please select a probability profile from the list.";
                    MessageBox.Show(error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            // >> Task #8758 Pax2Sim - Editor for Groups

            if (!OverallTools.FonctionUtiles.checkParams(cb_DistributionType.Text, fValue, sValue, tValue, out error))
            {
                MessageBox.Show(error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;

        }
        #region Les getteurs pour récupérer les informations contenues dans cette fenêtre
        public String getDistribution()
        {
            if (cb_DistributionType.Text == "Probability profiles")
                return cb_OneOf.Text;
            return cb_DistributionType.Text;
        }
        public Double getFirstParam()
        {
            if (cb_DistributionType.Text == "Probability profiles")
                return 0;
            Double value;
            if (!Double.TryParse(txt_FirstParam.Text, out value))
            {
                return -1;
            }
            return value;
        }
        public Double getSecondParam()
        {
            if (cb_DistributionType.Text == "Probability profiles")
                return 0;
            Double value;
            if (!Double.TryParse(txt_SecondParam.Text, out value))
            {
                return -1;
            }
            return value;
        }
        public Double getThirdParam()
        {
            if (cb_DistributionType.Text == "Probability profiles")
                return 0;
            Double value;
            if (!Double.TryParse(txt_ThirdParam.Text, out value))
            {
                return -1;
            }
            return value;
        }

        public bool getValeursOntChangees()
        {
            return bValeursOntChangees;
        }
        #endregion

        private void btn_Determine_Click(object sender, EventArgs e)
        {
            Prompt.Distribution_Assistant assistant = new SIMCORE_TOOL.Prompt.Distribution_Assistant(gdh_Datas_);
            if (assistant.ShowDialog() == DialogResult.OK)
            {
                this.cb_DistributionType.SelectedItem = assistant.Distribution;
                this.txt_FirstParam.Text = assistant.Param1.ToString();
                this.txt_SecondParam.Text = assistant.Param2.ToString();
                this.txt_ThirdParam.Text = assistant.Param3.ToString();
            }
        }

        private void cb_OneOf_Enter(object sender, EventArgs e)
        {
            Int32 iSelected = cb_OneOf.SelectedIndex;
            cb_OneOf.Items.Clear();
            cb_OneOf.Items.Add(AddNewDistribution);
            if (dtOneofDistribution_ != null)
            {

                foreach (DataColumn Name in dtOneofDistribution_.Columns)
                {
                    if (Name.ColumnName.LastIndexOf("Value") == (Name.ColumnName.Length - 5))
                        continue;
                    if (Name.ColumnName.LastIndexOf("Frequency") == -1)
                        continue;
                    String DistributionName = Name.ColumnName.Substring(0, Name.ColumnName.LastIndexOf("Frequency") - 1);
                    if (!cb_OneOf.Items.Contains(DistributionName))
                    {
                        cb_OneOf.Items.Add(DistributionName);
                    }
                }
                /*
                foreach (DataRow ligne in dtOneofDistribution_.Rows)
                {
                    if (!cb_OneOf.Items.Contains(ligne[0].ToString()))
                    {
                        cb_OneOf.Items.Add(ligne[0].ToString());
                    }
                }*/
            }
            cb_OneOf.SelectedIndex = iSelected;
        }

        private void cb_OneOf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_OneOf.SelectedIndex == -1)
                return;
            bValeursOntChangees = true;
            if (cb_OneOf.Text == AddNewDistribution)
            {
                Oneof_Assistant Assistant = new Oneof_Assistant(dtOneofDistribution_);
                if (Assistant.ShowDialog() != DialogResult.OK)
                {
                    cb_OneOf.SelectedIndex = -1;
                    return;
                }
                cb_OneOf.SelectedIndex = -1;
                cb_OneOf_Enter(null, null);
                cb_OneOf.Text = Assistant.SelectedDistribution;
            }
        }
    }
}