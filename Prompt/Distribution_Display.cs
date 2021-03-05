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
    public partial class Distribution_Display : Form
    {
        ArrayList ListDistrib;
        public Distribution_Display(ArrayList List)
        {
            InitializeComponent();
            ListDistrib = List;
            OverallTools.FonctionUtiles.MajBackground(this);
            foreach (Distribution_Assistant.Distribution_Class distrib in List)
            {
                if (distrib.DistributionName == "Normal")
                {
                    lbl_N_P.Text = distrib.Parameters;
                    lbl_N_R.Text = distrib.Rank;
                    rb_Normal.Enabled = true;
                    lbl_N_P.Enabled = true;
                    lbl_N_R.Enabled = true;
                }
                else if (distrib.DistributionName == "Exponential")
                {
                    lbl_E_P.Text = distrib.Parameters;
                    lbl_E_R.Text = distrib.Rank;
                    rb_Exponential.Enabled = true;
                    lbl_E_P.Enabled = true;
                    lbl_E_R.Enabled = true;
                }
                else if (distrib.DistributionName == "Gamma")
                {
                    lbl_G_P.Text = distrib.Parameters;
                    lbl_G_R.Text = distrib.Rank;
                    rb_Gamma.Enabled = true;
                    lbl_G_P.Enabled = true;
                    lbl_G_R.Enabled = true;
                }
                else if (distrib.DistributionName == "LogNormal")
                {
                    lbl_L_P.Text = distrib.Parameters;
                    lbl_L_R.Text = distrib.Rank;
                    rb_LogNormal.Enabled = true;
                    lbl_L_P.Enabled = true;
                    lbl_L_R.Enabled = true;
                }
                else if (distrib.DistributionName == "Weibull")
                {
                    lbl_W_P.Text = distrib.Parameters;
                    lbl_W_R.Text = distrib.Rank;
                    rb_Weibull.Enabled = true;
                    lbl_W_P.Enabled = true;
                    lbl_W_R.Enabled = true;
                }
            }
        }


        public String GetSelectedDistribution()
        {
            if(rb_Normal.Checked)
                return rb_Normal.Text;
            if (rb_Exponential.Checked)
                return rb_Exponential.Text;
            if (rb_Gamma.Checked)
                return rb_Gamma.Text;
            if (rb_LogNormal.Checked)
                return rb_LogNormal.Text;
            if (rb_Weibull.Checked)
                return rb_Weibull.Text;
            return null;
        }
        private void rb_Normal_CheckedChanged(object sender, EventArgs e)
        {
            btn_Ok.Enabled = true;
        }
    }
}