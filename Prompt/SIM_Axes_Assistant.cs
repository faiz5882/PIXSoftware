using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Axes_Assistant : Form
    {
        public SIM_Axes_Assistant(String xValue, String yValue, String y2Value)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            txt_X.Text = xValue;
            txt_Y.Text = yValue;
            txt_Y2.Text = y2Value;
        }
        public String X
        {
            get
            {
                return txt_X.Text;
            }
        }
        public String Y
        {
            get
            {
                return txt_Y.Text;
            }
        }
        public String Y2
        {
            get
            {
                return txt_Y2.Text;
            }
        }
    }
}