using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class ScenarioName :Form
    {
        private GestionDonneesHUB2SIM gestionDonnees;
        public ScenarioName(String dataset_, GestionDonneesHUB2SIM gestionDonnees_)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            txt_Name.BackColor = Color.White;
            txt_Name.Text = dataset_;
            gestionDonnees = gestionDonnees_;
        }
        public String getName()
        {
            return txt_Name.Text;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (gestionDonnees.ScenarioExist(txt_Name.Text))
            {
                MessageBox.Show("The Scenario name is already in use.");
                DialogResult = DialogResult.None;
                txt_Name.Focus();
                return;
            }
        }
    }
}