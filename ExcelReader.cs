using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Collections;

namespace SIMCORE_TOOL
{
    class ExcelReader
    {
        // Application :
        private _Application m_Application;
        // Identifiant du processus :
        private int m_ProcessId;
        // Classeur :
        private _Workbook m_Workbook;
        // Feuille :
        private _Worksheet m_Worksheet;

        /*** FEUILLE ***/
        // Nombre total de colonnes :
        private int m_ColumnCount;
        // Index courant de colonne :
        private int m_ColumnIndex;
        // Fin du flux :
        private bool m_EndOfStream;
        // Nombre total de lignes :
        private int m_RowCount;
        // Index courant de ligne :
        private int m_RowIndex;
        // Valeur de cellule ou de ligne :
        private string m_Value;

        public bool EndOfStream
        {
            get { return m_EndOfStream; }
        }

        public ExcelReader()
        {
            this.m_ProcessId = -1;
            this.m_EndOfStream = false;
            this.m_RowIndex = -1;
            this.m_RowCount = -1;
            this.m_ColumnIndex = -1;
            this.m_ColumnCount = -1;
        }

        //Fermeture du fichier
        public void Close()
        {
            this.m_Worksheet = null;
            this.m_Workbook.Close(false, Type.Missing, Type.Missing);
            this.m_Workbook = null;
            this.m_Application.DisplayAlerts = true;
            this.m_Application.Quit();
            this.m_Application = null;
            Process.GetProcessById(this.m_ProcessId).Kill();
        }

        //Ouverture du fichier
        public void Open(string FilePath)
        {
            this.Open(FilePath, null);
        }

        //Ouverture du fichier avec le chemin et le nom passés en param.
        public void Open(string FilePath, string WorksheetName)
        {
            // Ouverture d' Excel :
            Process[] ExcelProcessesBefore = Process.GetProcessesByName("EXCEL");
            this.m_Application = new ApplicationClass();
            Process[] ExcelProcessesAfter = Process.GetProcessesByName("EXCEL");
            this.m_ProcessId = this.GetProcessId(ExcelProcessesBefore, ExcelProcessesAfter);
            this.m_Application.DisplayAlerts = false;

            // Ouverture du classeur :
            this.m_Workbook = this.m_Application.Workbooks.Open(FilePath, Type.Missing, Type.Missing, Type.Missing,
                                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                Type.Missing, Type.Missing, Type.Missing);

            // Sélectionner la feuille :
            if (string.IsNullOrEmpty(WorksheetName))
            {
                // Sélectionner la feuille active :
                this.m_Worksheet = (_Worksheet)this.m_Workbook.ActiveSheet;
            }
            else
            {
                // Sélectionner la feuille donnée :
                this.m_Worksheet = this.GetWorksheet(WorksheetName);
            }

            // Mettre à jour le nombre de lignes et colonnes :
            this.SetWorksheetAttributes();
        }

        //Lorsque la feuille est donnée, le lecteur est placé à son niveau. 
        //Le nombre de colonnes et de lignes renseignées est dénombré 
        //et le lecteur est placé à la première ligne et la première cellule de la feuille. 
        //Les alertes déclenchées par Excel sont désactivées (DisplayAlerts = false).
        public int Peek()
        {
            if (!this.m_EndOfStream)
            {
                if (this.m_ColumnIndex + 1 <= this.m_ColumnCount)
                {
                    return ((this.m_ColumnCount * (this.m_RowIndex - 1)) + this.m_ColumnIndex);
                }
                else
                {
                    if (this.m_RowIndex + 1 <= this.m_RowCount)
                    {
                        return ((this.m_ColumnCount * (this.m_RowIndex - 1)) + this.m_ColumnIndex);
                    }
                    else
                    {
                        this.m_EndOfStream = true;
                        return -1;
                    }
                }
            }
            else
            {
                return -1;
            }
        }

        public string Read()
        {
            if (!this.m_EndOfStream)
            {
                this.m_Value = string.Empty;

                // Récupérer le contenu de la cellule :
                this.m_Value = (((Range)this.m_Worksheet.Cells[this.m_RowIndex, this.m_ColumnIndex]).Value2 != null ? ((Range)this.m_Worksheet.Cells[this.m_RowIndex, this.m_ColumnIndex]).Value2.ToString() : string.Empty);

                // Avancer le lecteur :
                if (this.m_ColumnIndex == this.m_ColumnCount)
                {
                    if (this.m_RowIndex < this.m_RowCount)
                    {
                        this.m_RowIndex++;
                        this.m_ColumnIndex = 1;
                    }
                    else
                    {
                        this.m_EndOfStream = true;
                    }
                }
                else
                {
                    this.m_ColumnIndex++;
                }

                return this.m_Value;
            }
            else
            {
                throw new EndOfStreamException();
            }
        }

        public string ReadLine()
        {
            if (!this.m_EndOfStream)
            {
                this.m_Value = string.Empty;

                // Récupérer toute la ligne :
                for (int i = 1; i <= this.m_ColumnCount; i++)
                {
                    this.m_Value += (((Range)this.m_Worksheet.Cells[this.m_RowIndex, i]).Value2 != null ? ((Range)this.m_Worksheet.Cells[this.m_RowIndex, i]).Value2.ToString() + ";" : ";");
                }

                // Avancer le lecteur :
                if (this.m_RowIndex < this.m_RowCount)
                {
                    this.m_RowIndex++;
                    this.m_ColumnIndex = 1;
                }
                else
                {
                    this.m_EndOfStream = true;
                }

                return this.m_Value.Substring(0, this.m_Value.Length - 1);
            }
            else
            {
                throw new EndOfStreamException();
            }
        }

        private int GetColumnCount(int FirstColumnIndex, int LastColumnIndex, int MiddleColumnIndex)
        {
            if ((Range)this.m_Worksheet.Cells[1, MiddleColumnIndex] != null &&
                ((Range)this.m_Worksheet.Cells[1, MiddleColumnIndex]).Value2 != null)
            {
                if ((Range)this.m_Worksheet.Cells[1, MiddleColumnIndex + 1] != null &&
                    ((Range)this.m_Worksheet.Cells[1, MiddleColumnIndex + 1]).Value2 == null)
                {
                    // La ligne suivante n'est pas renseignée, le résultat est :
                    return MiddleColumnIndex;
                }
                else
                {
                    // La ligne suivante est renseignée, rechercher dans l'intervalle supérieur :
                    return GetColumnCount(MiddleColumnIndex, LastColumnIndex, (int)Math.Ceiling((double)(MiddleColumnIndex + ((LastColumnIndex - MiddleColumnIndex) / 2))));
                }
            }
            else
            {
                // La ligne n'est pas renseignée, rechercher dans l'intervalle inférieur :
                return GetColumnCount(FirstColumnIndex, MiddleColumnIndex, (int)Math.Ceiling((double)(FirstColumnIndex + ((MiddleColumnIndex - FirstColumnIndex) / 2))));
            }
        }

        private int GetProcessId(Process[] ExcelProcessesBefore, Process[] ExcelProcessesAfter)
        {
            bool IsMyProcess = false;
            int Result = -1;

            // Si mon processus est le seul à être instancié
            // inutile de parcourir le tableau, il n'y a qu'une seule instance :
            if (ExcelProcessesBefore.Length == 0 && ExcelProcessesAfter.Length == 1)
            {
                Result = ExcelProcessesAfter[0].Id;
            }
            else
            {
                // Parcours des processus après instanciation de l'objet :
                foreach (Process ProcessAfter in ExcelProcessesAfter)
                {
                    // Parcours des processus avant instanciation de l'objet :
                    IsMyProcess = true;
                    foreach (Process ProcessBefore in ExcelProcessesBefore)
                    {
                        // Si je le retrouve, ce n'est pas celui que je cherche :
                        if (ProcessAfter.Id == ProcessBefore.Id)
                        {
                            IsMyProcess = false;
                        }
                    }

                    // J'ai retrouvé mon processus :
                    if (IsMyProcess)
                    {
                        Result = ProcessAfter.Id;
                    }
                }
            }

            return Result;
        }

        private int GetRowCount(int FirstRowIndex, int LastRowIndex, int MiddleRowIndex)
        {
            if ((Range)this.m_Worksheet.Cells[MiddleRowIndex, 1] != null &&
                ((Range)this.m_Worksheet.Cells[MiddleRowIndex, 1]).Value2 != null)
            {
                if ((Range)this.m_Worksheet.Cells[MiddleRowIndex + 1, 1] != null &&
                    ((Range)this.m_Worksheet.Cells[MiddleRowIndex + 1, 1]).Value2==null &&
                    ((Range)this.m_Worksheet.Cells[MiddleRowIndex + 2, 1]).Value2 == null)
                {
                    // La ligne suivante n'est pas renseignée, le résultat est :
                    return MiddleRowIndex;
                }
                else
                {
                    // La ligne suivante est renseignée, rechercher dans l'intervalle supérieur :
                    return GetRowCount((MiddleRowIndex), LastRowIndex, (int)Math.Ceiling((double)((MiddleRowIndex) + ((LastRowIndex - MiddleRowIndex) / 2))));
                }
            }
            else
            {
                // La ligne n'est pas renseignée, rechercher dans l'intervalle inférieur :
                return GetRowCount(FirstRowIndex, MiddleRowIndex, (int)Math.Ceiling((double)(FirstRowIndex + ((MiddleRowIndex - FirstRowIndex) / 2))));
            }
        }


        private _Worksheet GetWorksheet(string WorksheetName)
        {
            foreach (_Worksheet Worksheet in this.m_Workbook.Worksheets)
            {
                if (Worksheet.Name == WorksheetName)
                {
                    this.SetWorksheetAttributes();
                    return Worksheet;
                }
            }
            return (_Worksheet)this.m_Workbook.ActiveSheet;
        }

        private void SetWorksheetAttributes()
        {
            //Le nombre de lignes(non vides) exactement contenues dans le fichier excel
            //int nbLignes=Message.collectStrings().Count;

            // Les attributs spécifiques à la feuille sont :
            // - Le nombre de lignes :
            //this.m_RowCount = Message.getRowCount(m_Worksheet, nbLignes);
            this.m_RowCount = this.GetRowCount(1, this.m_Worksheet.Rows.Count, (int)Math.Ceiling((double)((this.m_Worksheet.Rows.Count - 1) / 2)));
            this.m_RowIndex = 1;
            // - Le nombre de colonnes :
            this.m_ColumnCount = this.GetColumnCount(1, this.m_Worksheet.Columns.Count, (int)Math.Ceiling((double)((this.m_Worksheet.Columns.Count - 1) / 2)));
            this.m_ColumnIndex = 1;
        }
       
    }
}
