using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Diagnostics;

namespace SIMCORE_TOOL
{
    partial class AboutPax2SIM : Form
    {
        //Pour la gestion des fichiers resssource
        private ResourceManager manager;
        //Pour les messages à afficher dans les MessageBox
        private string[] messages;
        public AboutPax2SIM()
        {
            /////////////////////////////
            //Creation du resourceManager
            ////////////////////////////
            manager = new ResourceManager("SIMCORE_TOOL.AboutPax2SIM", Assembly.GetExecutingAssembly());
            //////////////////////////////
            //Initialisation des messages
            /////////////////////////////
            messages = new string[4];
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = manager.GetString("message" + (i + 1));
            }
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            //  Initialisez AboutBox pour afficher les informations sur le produit à partir des informations de l'assembly.
            //  Modifiez les paramètres des informations de l'assembly pour votre application via :
            //  - Projet->Propriétés->Application->Informations de l'assembly
            //  - AssemblyInfo.cs
            this.Text = String.Format("About {0}", OverallTools.AssemblyActions.AssemblyTitle);
            this.labelProductName.Text = OverallTools.AssemblyActions.AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", OverallTools.AssemblyActions.AssemblyVersion);

            this.labelCopyright.Text = OverallTools.AssemblyActions.AssemblyCopyright;
            this.labelCompanyName.Text = OverallTools.AssemblyActions.AssemblyCompany;
            this.textBoxDescription.Text = OverallTools.AssemblyActions.AssemblyDescription + "\r\n" + "Informations : \tinfo@pax2sim.com\r\nSupport : \t\tsupport@pax2sim.com";
            
            if (PAX2SIM.bTrialVersion)
            {
                this.textBoxDescription.Text += "\r\n" + messages[3] + "\t";
                if (PAX2SIM.iValidDaysTrialVersion > 0)
                    this.textBoxDescription.Text += PAX2SIM.iValidDaysTrialVersion.ToString() + " " + messages[2];
                else
                    this.textBoxDescription.Text += messages[1];
            }

        }



        private void tsmi_SendMail_Click(object sender, EventArgs e)
        {
            try
            {
                string strMailTo = "mailto:support@pax2sim.com?subject=Support&body=Pax2sim informations :%0A" + labelVersion.Text + "%0A" + labelCopyright.Text + "%0A%0A%0A" + "Content :%0A%0A"; ;
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.RedirectStandardOutput = false;
                myProcess.StartInfo.FileName = strMailTo;
                myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                myProcess.Start();
            }
            catch (Exception exe)
            {
                MessageBox.Show("Failure while sending email."); //messages[0]);
                OverallTools.ExternFunctions.PrintLogFile("Except02000: " + this.GetType().ToString() + " throw an exception while sending email: " + exe.Message);
            }
        }

        private void tmsi_CopyLink_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("www.pax2sim.com");
        }

        private void tmsi_CopySupport_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("support@pax2sim.com");
        }

        private void textBoxDescription_Click(object sender, EventArgs e)
        {
            textBoxDescription.ContextMenuStrip.Show(MousePosition);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.pax2sim.com/");
        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.hubperformance.com");
        }

    }
}
