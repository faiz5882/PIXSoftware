using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nevron.GraphicsCore;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Annotation : Form
    {
        public SIM_Annotation()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            Value = "";
        }

        public SIM_Annotation(string sValue)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            Value = sValue;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (Value == "")
            {
                DialogResult dr = MessageBox.Show("No Text provided, continue?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                {
                    this.DialogResult = DialogResult.None;
                }
            }
        }

        internal String Value
        {
            get
            {
                return richTextBox1.Text;
            }
            set
            {
                richTextBox1.Text = value;
            }
        }
    }
}
