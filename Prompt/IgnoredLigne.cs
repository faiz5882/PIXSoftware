using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class IgnoredLigne : Form
    {
        public IgnoredLigne(DataTable table, String caption)
        {
            InitializeComponent();
            this.Text = caption;
            dgw_Table.DataSource = table;
        }
        public void UpdateContent(DataTable table, String caption)
        {
            this.Text = caption;
            dgw_Table.DataSource = table;
        }

        private void IgnoredLigne_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}