using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt.Allocation
{
    public partial class SIM_InfeedParameters : Form
    {   
        internal int DoubleInfeed
        {
            get
            {
                return FonctionsType.getInt(txt_DoubleInfeed.Text);
            }
        }
        internal Double InfeedSpeed
        {
            get
            {
                return FonctionsType.getDouble(txt_InfeedSpeed.Text);
            }
        }

        public SIM_InfeedParameters(int iInfeedDouble, Double dSpeed)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);            

            txt_DoubleInfeed.Text = iInfeedDouble.ToString();
            txt_InfeedSpeed.Text = dSpeed.ToString();
        }


        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if ((txt_DoubleInfeed.Text == "") || (FonctionsType.getInt(txt_DoubleInfeed.Text) < 0))
            {
                MessageBox.Show("Please specify a valid number for the reclaim with 2 infeeds", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if ((txt_InfeedSpeed.Text == "") || (FonctionsType.getDouble(txt_InfeedSpeed.Text) <= 0))
            {
                MessageBox.Show("Please specify a valid number for speed of infeed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
        }
    }
}
