using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL
{
    public partial class SIM_Edit_Project_Name : Form
    {
        private string nomProjet;
        bool bPrecedent = false;
        bool bLoad;
        public SIM_Edit_Project_Name(String nomProjet_, bool bUseAlpha)
        {
            InitializeComponent();
            bLoad = true;
            txt_projectName.BackColor = Color.White;
            OverallTools.FonctionUtiles.MajBackground(this);
            txt_projectName.Text = nomProjet_;
            nomProjet = nomProjet_;
            rb_UseDescription.Checked = bUseAlpha;
            rb_UseIndexes.Checked = !bUseAlpha;
            bLoad = false;
            gb_FlightPlanAlloccation.Visible = !PAX2SIM.bReporter;
        }
        public String getNomProjet()
        {
            return txt_projectName.Text;
        }
        /// <summary>
        /// Returns a boolean that indicates if the project will use the description ( Alpha) or not
        /// </summary>
        /// <returns></returns>
        public Boolean getUseDescription()
        {
            return rb_UseDescription.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_projectName.Text.Length == 0)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Please choose a project name.", "Error", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                txt_projectName.Focus();
            }
        }

        private void txt_projectName_TextChanged(object sender, EventArgs e)
        {
            String name = txt_projectName.Text;
            char c = 'a';
            char c2 = 'A';
            char c3 = '0';
            for(int i=0; i< 26;i++){
                name  =name.Replace(c, ' ');
                name = name.Replace(c2, ' ');
                if(i<10)
                    name = name.Replace(c3, ' ');
                c++;
                c2++;
                c3++;
            }
            name = name.Replace('_', ' ');
            name = name.Replace('-', ' ');
            name = name.Replace('(', ' ');
            name = name.Replace(')', ' ');            
            name = name.Replace(" ", "");
            button1.Enabled = !(name.Length > 0);
            if (!button1.Enabled)
            {
                if(!bPrecedent)
                    MessageBox.Show("Please use Alphanumerical characters for the Project name","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                bPrecedent = true;
            }
            else
            {
                 bPrecedent = false;
            }
        }

        private void rb_UseDescription_CheckedChanged(object sender, EventArgs e)
        {
            if (bLoad)
                return;
            if (!rb_UseDescription.Checked)
                return;
            DialogResult drResult = MessageBox.Show("Warning: The use of the airport objects descriptions needs you to fill correctly description fields using the appropriate prefix (see help for details).Do you want to continue ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (drResult == DialogResult.No)
            {
                rb_UseIndexes.Checked = true;
            }
        }
    }
}