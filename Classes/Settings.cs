using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Classes
{
    /// <summary>
    /// Classe utilisée pour chargée les informations d'exécution de l'application. (le mode de visualisation, les fichiers
    /// déjà ouverts...)
    /// </summary>
    internal class Settings
    {
        #region les variables de la classe.
        private static int iNumberProject = 12;
        private List<String> alHistoric;
        private String SettingsPath;
        private static VersionManager Version = new VersionManager("1.0");
        private int iLastNumberOfFile;
        internal Color Couleur1;
        internal Color Couleur2;
        internal Int32 Angle;
        #endregion

        #region Les constructeurs et la fonction de sauvegarde du projet.
        internal Settings(String Path, Color Couleur1_, Color Couleur2_, Int32 Angle_)
        {
            Couleur1 = Couleur1_;
            Couleur2 = Couleur2_;
            Angle = Angle_;
            alHistoric = new List<String>();
            SettingsPath = Path;
            if (!System.IO.File.Exists(SettingsPath))
                return;
            XmlDocument projet = new XmlDocument();
            try
            {
                projet.Load(SettingsPath);
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.DeleteFile(SettingsPath);
                OverallTools.ExternFunctions.PrintLogFile("Err00199 : The settings file has a wrong format : " + e.Message);
                return;
            }
            VersionManager sVersion = new VersionManager("1.0");
            if ((!(projet.HasChildNodes)) || (!(OverallTools.FonctionUtiles.hasNamedChild(projet, "Settings"))))
                return;
            if ((OverallTools.FonctionUtiles.hasNamedAttribute(projet["Settings"], "Version")))
                sVersion = new VersionManager(projet["Settings"].Attributes["Version"].Value.ToString());
            if (sVersion > Version)
                return;
            if (OverallTools.FonctionUtiles.hasNamedChild(projet["Settings"], "BackGround"))
            {
                XmlElement xeBackGround = projet["Settings"]["BackGround"];
                if ((OverallTools.FonctionUtiles.hasNamedAttribute(xeBackGround, "Color1")))
                {
                    if (!Int32.TryParse(xeBackGround.Attributes["Color1"].Value, out Angle))
                        Couleur1 = Color.SkyBlue;
                    else
                        Couleur1 = Color.FromArgb(Angle);
                }
                if ((OverallTools.FonctionUtiles.hasNamedAttribute(xeBackGround, "Color2")))
                {
                    if (!Int32.TryParse(xeBackGround.Attributes["Color2"].Value, out Angle))
                        Couleur2 = Color.White;
                    else
                        Couleur2 = Color.FromArgb(Angle);
                }
                Angle = 90;
                if ((OverallTools.FonctionUtiles.hasNamedAttribute(xeBackGround, "Angle")))
                {
                    Int32.TryParse(xeBackGround.Attributes["Angle"].Value, out Angle);
                }
            }
            if ((OverallTools.FonctionUtiles.hasNamedChild(projet["Settings"], "Historic")) && (projet["Settings"]["Historic"].HasChildNodes))
            {
                foreach (XmlElement Element in projet["Settings"]["Historic"].ChildNodes)
                {
                    if (Element.Name == "Project")
                    {
                        if (!Element.HasChildNodes)
                            continue;
                        if (File.Exists(Element.FirstChild.Value.ToString()))
                            alHistoric.Add(Element.FirstChild.Value.ToString());
                    }
                }
            }
            iLastNumberOfFile = 0;
            projet = null;
        }
        internal void SaveSettings()
        {
            XmlDocument projet = new XmlDocument();
            projet.AppendChild(projet.CreateElement("Settings"));
            projet["Settings"].SetAttribute("Version", Version.ToString());
            if ((PAX2SIM.Couleur1 != Color.SkyBlue) ||
                (PAX2SIM.Couleur1 != Color.White) ||
                (PAX2SIM.Angle != 90))
            {
                projet["Settings"].AppendChild(projet.CreateElement("BackGround"));
                if (PAX2SIM.Couleur1 != Color.SkyBlue)
                    projet["Settings"]["BackGround"].SetAttribute("Color1", PAX2SIM.Couleur1.ToArgb().ToString());
                if (PAX2SIM.Couleur2 != Color.White)
                    projet["Settings"]["BackGround"].SetAttribute("Color2", PAX2SIM.Couleur2.ToArgb().ToString());
                if (PAX2SIM.Angle != 90)
                    projet["Settings"]["BackGround"].SetAttribute("Angle", PAX2SIM.Angle.ToString());
            }
            projet["Settings"].AppendChild(projet.CreateElement("Historic"));
            foreach (String Value in alHistoric)
            {
                XmlElement elem = projet.CreateElement("Project");
                projet["Settings"]["Historic"].AppendChild(elem);
                elem.AppendChild(projet.CreateTextNode(Value));
            }
            try
            {
                projet.Save(SettingsPath);
            }
            catch (Exception exc)
            {
                String sErrorMessage = "Unable to save the settings file for current " + OverallTools.AssemblyActions.AssemblyTitle + " user. ";
                OverallTools.ExternFunctions.PrintLogFile(DateTime.Now.ToString() + " : " + sErrorMessage + " : " + exc.Message);
                MessageBox.Show(sErrorMessage + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OverallTools.ExternFunctions.PrintLogFile("Except02009: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
        }
        #endregion

        #region Partie destinée à la gestion de l'historique.
        public void AddHistoricProject(String ProjectPath)
        {
            if (alHistoric.Contains(ProjectPath))
                alHistoric.Remove(ProjectPath);
            if (!alHistoric.Contains(ProjectPath))
            {
                alHistoric.Insert(0, ProjectPath);
                if (alHistoric.Count > iNumberProject)
                    alHistoric.RemoveAt(iNumberProject);
            }
        }
        public void UpdateHistoric(ToolStripMenuItem tsmi_Menu, int iIndex, EventHandler Eventhandler)
        {
            if (alHistoric.Count == 0)
                return;
            if (iLastNumberOfFile == 0)
                tsmi_Menu.DropDownItems.Insert(iIndex, new ToolStripSeparator());
            int iNumberProject_ = alHistoric.Count;
            for (int i = 0; i < iLastNumberOfFile; i++)
            {
                tsmi_Menu.DropDownItems.RemoveAt(iIndex);
            }
            for (int i = 0; i < iNumberProject_; i++)
            {
                String Name = System.IO.Path.GetFileNameWithoutExtension(alHistoric[i].ToString());
                tsmi_Menu.DropDownItems.Insert(iIndex + i, new ToolStripMenuItem(Name));
                tsmi_Menu.DropDownItems[iIndex + i].Click += Eventhandler;
                tsmi_Menu.DropDownItems[iIndex + i].Tag = alHistoric[i].ToString();
                tsmi_Menu.DropDownItems[iIndex + i].ToolTipText = alHistoric[i].ToString();

            }
            iLastNumberOfFile = iNumberProject_;
        }
        #endregion

    }
}
