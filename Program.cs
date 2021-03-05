using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nevron;

namespace SIMCORE_TOOL
{
    static class Program
    {

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            NLicense license = new NLicense("72026f86-3268-0115-fb0d-650200881bbd");
            //NLicense license = new NLicense("0f0f1497-1250-0ace-a4b3-336ad9671948");
            NLicenseManager.Instance.SetLicense(license);
            NLicenseManager.Instance.LockLicense = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //On prends l'heure de lancement de l'application.
            long h = DateTime.Now.Ticks;

            //On lance le spashscreen.
            Splash sp = new Splash();
            sp.Show();

            // << Task #8279 Pax2Sim - Protection Key - key details function
            try
            {
                OverallTools.ExternFunctions.PrintLogFile(GlobalNames.NEW_APPLICATION_INSTANCE);    // << Task #8604 Pax2Sim - Log File - accessible while running the application                
                SIMCORE_TOOL.Classes.AskMode.logPax2SimAssemblyVersion();
                SIMCORE_TOOL.Classes.AskMode.logProtectionKeyDetails();                
            }
            catch (Exception e)
            {
                String sErrorMessage = "A problem appears when trying to read the protection key details.";
                OverallTools.ExternFunctions.PrintLogFile("Exception: (" + sErrorMessage + "). Exception Details: " + e.Message);
            }
            // >> Task #8279 Pax2Sim - Protection Key - key details function

            //MessageBox.Show("01 - First CheckDongle");
            if (!PAX2SIM.CheckDongle())
            {
                sp.Close();
                return;/**/
            }
            //MessageBox.Show("01.5 - First CheckDongle");
            //On charge la fenêtre principale du projet.

            PAX2SIM program = null;
            try
            {
                program = new PAX2SIM(args);
            }
            catch (Exception e)
            {
                if (sp != null)
                {
                    sp.Close();
                }
                string error = "";
                error += "Source: " + e.Source + Environment.NewLine;
                error += "Error message: " + e.Message + Environment.NewLine;
                error += "StackTrace: " + e.StackTrace + Environment.NewLine;                
                MessageBox.Show(error);
                return;
            }
            //On calcul ensuite le temps qu'il nous a fallu pour charger la fenêtre principale du
            //logiciel. On fait en sorte que le splashscreen reste afficher au moins 3s.
            //Si le chargement de la fenêtre principal était plus long que 3s alors le logiciel se lance.
            TimeSpan p = new TimeSpan(DateTime.Now.Ticks - h);
            int dureeSplash = 3000 - (int)p.TotalMilliseconds;
            if (dureeSplash > 0)
                System.Threading.Thread.Sleep(dureeSplash);
            //On ferme le splashscreen et on lance l'application.
            sp.Close();
            //MessageBox.Show("03 - Second CheckKey");
            // >> Task #12391 Pax2Sim - Network use - Singapore University
            //when using the network UniKey.dll do not check the 2nd time for the key info. This will cause a memory problem.
            // The check was done just before this - to be investigated why it is done again here.(CheckKey() is included in CheckDongle())            
            if (PAX2SIM.singaporeMode)
            {
                Application.Run(program);
                program.Dispose();
                program.Close();
            }
            else
            {
                if (PAX2SIM.CheckKey())
                {
                    Application.Run(program);
                    program.Dispose();
                    program.Close();
                }
            }
            // >> Task #12391 Pax2Sim - Network use - Singapore University
        }
    }
}