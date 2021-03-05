using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SIMCORE_TOOL.Interface
{
    public partial class Help : Form
    {
        // Lancer une page spéciale dans la partie droite : 
        //                webBrowser1.Navigate("http://www.google.fr", "right");
        
        static String sWebSiteRootDirectoryName = "NetHelp";


        private bool bInternHelp;
        private PAX2SIM p2s_Window;
        static String sCurrentURL;
        private bool bHelp;

        internal String URL
        {
            get
            {
                if (webBrowser1.Url == null)
                    return null;
                return webBrowser1.Url.AbsolutePath;
            }
            set
            {
                this.Navigate(value);
            }
        }
        internal String ShortURL
        {
            get
            {
                String GlobalURL = webBrowser1.Url.AbsolutePath;
                if (!GlobalURL.Contains(sWebSiteRootDirectoryName))
                    return GlobalURL;
                return GlobalURL.Substring(GlobalURL.IndexOf(sWebSiteRootDirectoryName) + sWebSiteRootDirectoryName.Length);
            }
        }
        internal Help(PAX2SIM p2s_Window_)
        {
            InitializeComponent();
            p2s_Window = p2s_Window_;
            ShowInIntern();
            sCurrentURL = "";
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            /*webBrowser1.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
            webBrowser1.FileDownload += new EventHandler(webBrowser1_FileDownload);*/
            bHelp = true;
        }


        /// <summary>
        /// Open it as a html note viewer in a separate window.
        /// </summary>
        internal Help(String html)
        {
            InitializeComponent();
            ShowInWindow();
            sCurrentURL = "";
            webBrowser1.AllowNavigation = false;
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.DocumentText = html;
            ts_Help.Visible = false;
            bHelp = false;
        }

        internal Help()
        {
            InitializeComponent();
            ShowInWindow();
            sCurrentURL = "";
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            bHelp = false;
        }

        internal void Update(String html)
        {
            webBrowser1.DocumentText = html;
            webBrowser1.Invalidate();
        }


        /// <summary>
        /// Constructeur pour ouvrir un fichier (html, pdf, ...).
        /// La fenetre est lancé en mode détaché et sans toolbar.
        /// </summary>
        /// <param name="path">Chemin du fichier à ouvrir</param>
        /// <returns></returns>
        internal static Help OpenFile(String path)
        {
            Help helpwin = new Help();
            helpwin.HideToolBar();
            helpwin.Navigation = path;
            helpwin.Text = "Preview";
            return helpwin;
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            sCurrentURL = e.Url.AbsolutePath;
        }

        private void ShowHideButtons(Boolean bInternMode)
        {
            bInternHelp = bInternMode;
            tsb_HelpInInternWindow.Visible = !bInternMode;
            tsb_HelpInExternWindow.Visible = bInternMode;
        }

        internal void ShowInWindow()
        {
            Point pScreenPosition = this.PointToScreen(new Point(0, 0));

            this.Hide();
            this.Dock = DockStyle.None;
            this.Parent = null;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.TopLevel = true;
            /*this.Location = CenterToScreen(
                new Point(pScreenPosition.X - this.Width-10, pScreenPosition.Y);*/
            if(this.Width<500)
                this.Width = this.Width * 2; 
            CenterToScreen();
            this.Show();
            ShowHideButtons(false);
            if (p2s_Window != null)
                p2s_Window.help_HideInternHelp();
        }

        internal void ShowInIntern()
        {
            this.Hide();
            if (this.Width > 500)
                this.Width = 500;
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Parent = p2s_Window.Help_GetPanel();
            this.Dock = DockStyle.Fill;
            this.Show();
            ShowHideButtons(true);
            if (p2s_Window != null)
                p2s_Window.help_ShowInternHelp();
        }

        internal void ShowHelp()
        {
            if (!bInternHelp)
            {
                ShowInWindow();
            }
            else
            {
                ShowInIntern();
            }
        }

        internal void HideHelp()
        {
            if (!bInternHelp)
            {
                this.Hide();
            }
            if (p2s_Window != null)
            {
                p2s_Window.help_HideInternHelp();
                p2s_Window.HideWindow();
            }
        }

        internal void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            try
            {
                webBrowser1.Navigate(address);
            }
            catch (System.UriFormatException ex)
            {
                Console.WriteLine(ex);
                OverallTools.ExternFunctions.PrintLogFile("Except02049: " + this.GetType().ToString() + " throw an exception: " + ex.Message);
                return;
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae);
                OverallTools.ExternFunctions.PrintLogFile("Except02050: " + this.GetType().ToString() + " throw an exception: " + ae.Message);
            }
        }

        internal String Navigation
        {
            get
            {
                return URL;
            }
            set
            {
                Navigate(value);
            }
        }

        #region Gestion de la toolbar principale
        private void tsb_HideHelp_Click(object sender, EventArgs e)
        {
            HideHelp();
        }

        private void tsb_HelpInExternWindow_Click(object sender, EventArgs e)
        {
            ShowInWindow();
        }

        private void tsb_HelpInInternWindow_Click(object sender, EventArgs e)
        {
            ShowInIntern();
        }

        private void tsb_SeeHelpOnBrowser_Click(object sender, EventArgs e)
        {
            HideHelp();
            try
            {

                Process.Start(URL.Replace('/', '\\'));
            }
            catch (Exception ae)
            {
                Console.WriteLine(ae);
                OverallTools.ExternFunctions.PrintLogFile("Except02051: " + this.GetType().ToString() + " throw an exception: " + ae.Message);
            }
        }

        private void tsb_HelpOnline_Click(object sender, EventArgs e)
        {
            HideHelp();
        }

        internal void HideToolBar()
        {
            ts_Help.Hide();
        }
        #endregion

        private void Help_FormClosing(object sender, FormClosingEventArgs e)
        {
            // La fenetre d'aide ne doit pas etre fermée
            if (bHelp)
            {
                e.Cancel = true;
                HideHelp();
            }
            // Mais les autres (prévisualisation report ou note) oui.
            else
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void tsb_SendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string strMailTo = "mailto:support@pax2sim.com?subject=Support&body=Pax2sim informations :%0A" + OverallTools.AssemblyActions.AssemblyVersion + "%0A" + OverallTools.AssemblyActions.AssemblyCopyright + "%0A%0A%0A" + "Content :%0A%0A"; ;
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.RedirectStandardOutput = false;
                myProcess.StartInfo.FileName = strMailTo;
                myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                myProcess.Start();
            }
            catch (Exception except)
            {
                Console.WriteLine(except);
                OverallTools.ExternFunctions.PrintLogFile("Except02052: " + this.GetType().ToString() + " throw an exception: " + except.Message);
            }
        }

        private void Help_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                HideHelp();
        }
    }
}