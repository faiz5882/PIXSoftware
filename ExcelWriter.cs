using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Collections;

namespace SIMCORE_TOOL
{
    class ExcelWriter
    {
        /*** EXCEL ***/
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

        public bool EndOfStream
        {
            get { return m_EndOfStream; }
        }

        public ExcelWriter()
        {
            this.m_ProcessId = -1;
            this.m_EndOfStream = false;
            this.m_RowIndex = -1;
            this.m_RowCount = -1;
            this.m_ColumnIndex = -1;
            this.m_ColumnCount = -1;
        }

        public void Close()
        {
            this.m_Worksheet = null;
            this.m_Workbook.Save();
            this.m_Workbook.Close(false, Type.Missing, Type.Missing);
            this.m_Workbook = null;
            this.m_Application.DisplayAlerts = true;
            this.m_Application.Quit();
            this.m_Application = null;
            Process.GetProcessById(this.m_ProcessId).Kill();
        }
        public void NewLine()
        {
            if (!this.m_EndOfStream)
            {
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
            }
            else
            {
                throw new EndOfStreamException();
            }
        }

        public void Open(string FilePath)
        {
            this.Open(FilePath, null);
        }


        public void Open(string FilePath, string WorksheetName)
        {
            // Ouvrir Excel :
            Process[] ExcelProcessesBefore = Process.GetProcessesByName("EXCEL");
            this.m_Application = new ApplicationClass();
            Process[] ExcelProcessesAfter = Process.GetProcessesByName("EXCEL");
            this.m_ProcessId = this.GetProcessId(ExcelProcessesBefore, ExcelProcessesAfter);
            this.m_Application.DisplayAlerts = false;

            // Ouvrir le classeur :
            this.m_Workbook = this.m_Application.Workbooks.Add(Type.Missing);
            /*this.m_Workbook.SaveAs(FilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing,
                                   Type.Missing, Type.Missing, Type.Missing);*/

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

        public void Write(string Value)
        {
            if (!this.m_EndOfStream)
            {
                // Renseigner le contenu de la cellule :
                ((Range)this.m_Worksheet.Cells[this.m_RowIndex, this.m_ColumnIndex]).Value2 = Value;

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
            }
            else
            {
                throw new EndOfStreamException();
            }
        }

        public void WriteLine(ArrayList Values)
        {
            if (!this.m_EndOfStream)
            {
                // Renseigner les cellules :
                foreach (string str in Values)
                {
                    if (this.m_ColumnIndex <= this.m_ColumnCount)
                    {
                        ((Range)this.m_Worksheet.Cells[this.m_RowIndex, this.m_ColumnIndex]).Value2 = str;
                        if (this.m_ColumnIndex < this.m_ColumnCount)
                        {
                            this.m_ColumnIndex++;
                        }
                    }
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
            }
            else
            {
                throw new EndOfStreamException();
            }
        }

        private void SetWorksheetAttributes()
        {
            // Les attributs spécifiques à la feuille sont :
            // - Le nombre de lignes :
            this.m_RowCount = this.m_Worksheet.Rows.Count;
            this.m_RowIndex = 1;
            // - Le nombre de colonnes :
            this.m_ColumnCount = this.m_Worksheet.Columns.Count;
            this.m_ColumnIndex = 1;
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

    }
}
