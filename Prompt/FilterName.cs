using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class FilterName :Form
    {
        private GestionDonneesHUB2SIM gestionDonnees;
        private String filterName;
        private String dataset;
        private bool bPrecedent = false;

        public FilterName(String dataset_, String filterName_, GestionDonneesHUB2SIM gestionDonnees_)
        {
            InitializeComponent();
            txt_Name.BackColor = Color.White;
            dataset = dataset_;
            OverallTools.FonctionUtiles.MajBackground(this);
            filterName = filterName_;
            txt_Name.Text = filterName;
            gestionDonnees = gestionDonnees_;
        }
        public String getName()
        {
            return filterName;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            if (gestionDonnees.tableEstPresente(dataset, txt_Name.Text))
            {
                MessageBox.Show("The filter name is already in use.");
                DialogResult = DialogResult.None;
                txt_Name.Focus();
                return;
            }
            filterName = txt_Name.Text;
        }

        private void txt_Name_TextChanged(object sender, EventArgs e)
        {

            String name = txt_Name.Text;
            char c = 'a';
            char c2 = 'A';
            char c3 = '0';
            for (int i = 0; i < 26; i++)
            {
                name = name.Replace(c, ' ');
                name = name.Replace(c2, ' ');
                if (i < 10)
                    name = name.Replace(c3, ' ');
                c++;
                c2++;
                c3++;
            }
            name = name.Replace('_', ' ');
            name = name.Replace('-', ' ');
            name = name.Replace('(', ' ');
            name = name.Replace(')', ' ');
            name = name.Replace('^', ' ');  // >> Task #16728 PAX2SIM Improvements (Recurring) C#11
            name = name.Replace('#', ' ');  // >> Task #16728 PAX2SIM Improvements (Recurring) C#20 (# allowed in the filter name)
            name = name.Replace('.', ' ');  // >> Task #1954
            
            name = name.Replace(" ", "");            
            btn_Ok.Enabled = !(name.Length > 0);
            if (!btn_Ok.Enabled)
            {
                if (!bPrecedent)
                    MessageBox.Show("Please use Alphanumerical characters for the filter name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bPrecedent = true;
            }
            else
            {
                bPrecedent = false;
            }
        }
    }
}