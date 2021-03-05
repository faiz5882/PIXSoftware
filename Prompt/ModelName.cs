using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class ModelName :Form
    {
        private String ModelName_;
        public ModelName(String ModelName__)
        {
            InitializeComponent();
            txt_Name.BackColor = Color.White;
            OverallTools.FonctionUtiles.MajBackground(this);
            ModelName_ = ModelName__;
            txt_Name.Text = ModelName_;
        }
        public String getName()
        {
            return txt_Name.Text;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text.IndexOf(' ') != -1)
            {
                MessageBox.Show("Please use a model name without space.");
                DialogResult = DialogResult.None;
                txt_Name.Focus();
                return;
            }
            String[] sDirectories = System.IO.Directory.GetDirectories(Application.StartupPath + "\\Ressources\\", "", System.IO.SearchOption.TopDirectoryOnly);
            
            foreach (String Repertoire in sDirectories)
            {
                if (txt_Name.Text == Repertoire)
                {
                    MessageBox.Show("The model name is already defined.");
                    DialogResult = DialogResult.None;
                    txt_Name.Focus();
                    return;
                }
            }
        }
    }
}