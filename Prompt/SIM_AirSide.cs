using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_AirSide : Form
    {
        DataTable FPDTable_;
        DataTable FPATable_;

        delegate void EndOfSimulationCallBack();
        public SIM_AirSide(Int32 iNb_Parking, Int32 iNb_Pistes, DataTable FPDTable, DataTable FPATable)
        {
            InitializeComponent();
            FPDTable_ = FPDTable;
            FPATable_ = FPATable;
            OverallTools.FonctionUtiles.MajBackground(this);
            lbl_NB_Parking.Text = "Number of parking : " + iNb_Parking;
            lbl_NB_Pistes.Text = "Number of runway : " + iNb_Pistes;
        }
        public SIM_AirSide( DataTable FPDTable, DataTable FPATable)
        {
            InitializeComponent();
            FPDTable_ = FPDTable;
            FPATable_ = FPATable;

            /*On détermine le nombre de parking présents sur l'aéroport.*/
            Int32 iTmpFPD = OverallTools.DataFunctions.valeurMaximaleDansColonne(
                                    FPDTable,
                                    FPDTable.Columns.IndexOf("Parking"));
            Int32 iTmpFPA = OverallTools.DataFunctions.valeurMaximaleDansColonne(
                                    FPATable,
                                    FPATable.Columns.IndexOf("Parking"));
            if(iTmpFPD<iTmpFPA){
                iTmpFPD = iTmpFPA;
            }
            lbl_NB_Parking.Text = "Number of parking : " + iTmpFPD.ToString();


            /*On détermine le nombre de pistes présentes sur l'aéroport.*/
            iTmpFPD = OverallTools.DataFunctions.valeurMaximaleDansColonne(
                    FPDTable,
                    FPDTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay));
            iTmpFPA = OverallTools.DataFunctions.valeurMaximaleDansColonne(
                    FPATable,
                    FPATable.Columns.IndexOf(GlobalNames.sFPD_A_Column_RunWay));
            if (iTmpFPD < iTmpFPA)
            {
                iTmpFPD = iTmpFPA;
            }
            lbl_NB_Pistes.Text = "Number of runway : " + iTmpFPD.ToString();

            /*Mise à jour du fond d'écran.*/
            OverallTools.FonctionUtiles.MajBackground(this);
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {/*
            if (!OverallTools.FonctionUtiles.CheckCreateDirectory("C:\\Temp"))
            {
                return;
            }
            if (!OverallTools.FonctionUtiles.CheckCreateDirectory("C:\\Temp\\Model"))
            {
                return;
            }

            if (!OverallTools.FonctionUtiles.CheckCreateDirectory("C:\\Temp\\Model\\Airside.dir"))
            {
                return;
            }

            if (!OverallTools.FonctionUtiles.CheckCreateDirectory("C:\\Temp\\Model\\data"))
            {
                return;
            }
            if (!System.IO.File.Exists("C:\\Temp\\Model\\Airside.exe"))
            {
                System.IO.File.Copy(Application.StartupPath + "\\Ressources\\Airside.exe", "C:\\Temp\\Model\\Airside.exe");
                if (!System.IO.File.Exists("C:\\Temp\\Model\\Airside.exe"))
                {
                    return;
                }
            }
            if (!System.IO.File.Exists("C:\\Temp\\Model\\Airside.dir\\Airside.dll"))
            {
                System.IO.File.Copy(Application.StartupPath + "\\Ressources\\Airside.dll", "C:\\Temp\\Model\\Airside.dir\\Airside.dll");
                if (!System.IO.File.Exists("C:\\Temp\\Model\\Airside.dir\\Airside.dll"))
                {
                    return;
                }
            }
            if (!System.IO.File.Exists("C:\\Temp\\Model\\Airside.dir\\Airside.mod"))
            {
                System.IO.File.Copy(Application.StartupPath + "\\Ressources\\Airside.mod", "C:\\Temp\\Model\\Airside.dir\\Airside.mod");
                if (!System.IO.File.Exists("C:\\Temp\\Model\\Airside.dir\\Airside.mod"))
                {
                    return;
                }
            }
            String []Files = System.IO.Directory.GetFiles(Application.StartupPath + "\\Ressources\\");
            foreach (String file in Files)
            {
                if (file.EndsWith("sys"))
                {
                    String[] Path = file.Split('\\');
                    if (!System.IO.File.Exists("C:\\Temp\\Model\\Airside.dir\\" + Path[Path.Length-1]))
                    {
                        System.IO.File.Copy(file, "C:\\Temp\\Model\\Airside.dir\\" + Path[Path.Length - 1]);
                        if (!System.IO.File.Exists("C:\\Temp\\Model\\Airside.dir\\" + Path[Path.Length - 1]))
                        {
                            return;
                        }
                    }
                }
            }
            OverallTools.FonctionUtiles.EcritureFichier(FPDTable_, "C:\\Temp\\Model\\Data\\FPDTable.txt", "\t", true);
            OverallTools.FonctionUtiles.EcritureFichier(FPATable_, "C:\\Temp\\Model\\Data\\FPATable.txt", "\t", true);
            tSimulation = new Thread(new ThreadStart(lance));
            tSimulation.Start();
            DialogResult = DialogResult.None;
            btn_Cancel.Enabled = false;
            btn_ok.Enabled = false;
            */
        }
        private void EndOfSimulation()
        {
            if (this.InvokeRequired)
            {
                EndOfSimulationCallBack d = new EndOfSimulationCallBack(EndOfSimulation);
                this.Invoke(d, null);
            }
            else
            {
                btn_Cancel.Enabled = true;
                btn_ok.Enabled = true;
                DialogResult = DialogResult.OK;
            }
        }
    }
}