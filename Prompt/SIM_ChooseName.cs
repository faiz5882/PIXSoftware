using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_ChooseName : Form
    {
        private string nomProjet;
        bool bPrecedent = false;
        public SIM_ChooseName(String Prompt,String Text_,String nomProjet_)
        {
            InitializeComponent();
            txt_projectName.BackColor = Color.White;
            OverallTools.FonctionUtiles.MajBackground(this);
            lbl_pn.Text = Text_;
            if (lbl_pn.Width + lbl_pn.Left > this.Width)
                this.Width = lbl_pn.Width + lbl_pn.Left*2;
            this.Text = Prompt;
            txt_projectName.Text = nomProjet_;
            nomProjet = nomProjet_;
        }
        public String getName()
        {
            return txt_projectName.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_projectName.Text.Length == 0)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Please fill the blanks.");
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
    }
}