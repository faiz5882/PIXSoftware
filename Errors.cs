using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL
{
    public partial class Errors : Form
    {
        public Errors(ArrayList lesErreurs)
        {
            InitializeComponent();
            setErrors(lesErreurs);
        }
        public void setErrors(ArrayList lesErreurs)
        {
            richTextBox1.Text="";
            String Messages = "";
            if (lesErreurs == null)
            {
                richTextBox1.Text = Messages;
                this.Refresh();
                return;
            }
            try
            {
                int iMin = Math.Min(2000, lesErreurs.Count); // >> Task #12393 Pax2Sim - File conversion for Athens 100 previous min value

                if (iMin != lesErreurs.Count)
                    Messages += "Warning: There are " + lesErreurs.Count + " messages from which the first " + iMin + " are shown in this list.\n"; // >> Task #12393 Pax2Sim - File conversion for Athens  //"...\n";

                for (int i = 0; i < iMin; i++)
                {
                    Messages += lesErreurs[i] + "\n";
                }                
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02012: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            richTextBox1.Text = Messages;
            this.Refresh();
        }

        private void Errors_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}