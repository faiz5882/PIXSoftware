using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace SIMCORE_TOOL.com.crispico.documentTreeNode
{
    class UserDocumentUtils
    {
        public static string DOCUMENT_DIRECTORY_SEPARATOR = "\\";
        
        static string DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_NAME = "Document Name";
        static string DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_TYPE = "Document Type";
        static string DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_SOURCE_PATH = "Document Source Path";
        static string DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_PATH = "Document Path";
        static string DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_SIZE = "Document Size";
        static string DOCUMENT_TABLE_COLUMN_NAME_DATE_ADDED = "Date Added";
        static string DOCUMENT_TABLE_COLUMN_NAME_LAST_UPDATE_DATE = "Last Document Update";
                
        public static DataTable createDocumentTable(string documentNameAndExtention, string documentPath, string documentSourcePath)
        {
            DataTable documentTable = new DataTable(documentNameAndExtention);

            #region columns
            int fileNameColumnIndex = documentTable.Columns.Count;
            documentTable.Columns.Add(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_NAME, typeof(string));

            int fileTypeColumnIndex = documentTable.Columns.Count;
            documentTable.Columns.Add(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_TYPE, typeof(string));

            int fileSourcePathColumnIndex = documentTable.Columns.Count;
            documentTable.Columns.Add(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_SOURCE_PATH, typeof(string));

            int filePathColumnIndex = documentTable.Columns.Count;
            documentTable.Columns.Add(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_PATH, typeof(string));

            int fileSizeColumnIndex = documentTable.Columns.Count;
            documentTable.Columns.Add(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_SIZE, typeof(string));

            int dateAddedColumnIndex = documentTable.Columns.Count;
            documentTable.Columns.Add(DOCUMENT_TABLE_COLUMN_NAME_DATE_ADDED, typeof(DateTime));

            int lastUpdateDateColumnIndex = documentTable.Columns.Count;
            documentTable.Columns.Add(DOCUMENT_TABLE_COLUMN_NAME_LAST_UPDATE_DATE, typeof(DateTime));
            #endregion

            string documentName = "";
            string documentExtension = "";
            if (documentNameAndExtention.Contains(".") && documentNameAndExtention.LastIndexOf(".") + 1 < documentNameAndExtention.Length)
            {
                documentName = documentNameAndExtention.Substring(0, documentNameAndExtention.LastIndexOf("."));
                documentExtension = documentNameAndExtention.Substring(documentNameAndExtention.LastIndexOf(".") + 1);
            }
            
            try
            {
                DateTime now = DateTime.Now;
                FileInfo fileInfo = new FileInfo(documentSourcePath);
                
                DataRow row = documentTable.NewRow();
                row[fileNameColumnIndex] = documentName;
                row[fileTypeColumnIndex] = documentExtension.ToUpper();
                row[fileSourcePathColumnIndex] = documentSourcePath;
                row[filePathColumnIndex] = documentPath;
                row[fileSizeColumnIndex] = byteSizeToString(fileInfo.Length);
                row[dateAddedColumnIndex] = now;
                row[lastUpdateDateColumnIndex] = now;
                documentTable.Rows.Add(row);

                documentTable.AcceptChanges();
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Error occured while adding a new Document: " + exc.Message);
                return null;
            }
            return documentTable;
        }

        internal static void updateDocumentTablesFilePaths(List<string> documentTableNames, string newProjectDirectory, GestionDonneesHUB2SIM donnees)
        {
            if (documentTableNames == null)
                return;
            try
            {
                string initialFilePath = "";
                foreach (string documentTableName in documentTableNames)
                {
                    DataTable documentTable = donnees.getTable("Input", documentTableName);
                    if (documentTable == null)
                        continue;                    
                    int filePathColumnIndex = documentTable.Columns.IndexOf(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_PATH);
                    if (filePathColumnIndex == -1)
                        continue;
                    foreach (DataRow row in documentTable.Rows)
                    {
                        if (row[filePathColumnIndex] == null || !row[filePathColumnIndex].ToString().Contains(DOCUMENT_DIRECTORY_SEPARATOR))
                            continue;
                        initialFilePath = row[filePathColumnIndex].ToString();                        
                        string filePathInProject = newProjectDirectory + "\\Data\\" + GlobalNames.DOCUMENTS_DIRECTORY_NAME
                            + initialFilePath.Substring(initialFilePath.LastIndexOf(DOCUMENT_DIRECTORY_SEPARATOR));
                        row[filePathColumnIndex] = filePathInProject;
                    }
                }
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Project saving procedure: Error while updating the paths from the Documents tables."
                    + " The saving procedure will continue without updating the Documents tables. " + exc.Message);//+errror type
            }
        }

        internal static void updateDocumentTableWithGivenFilePath(DataTable documentTable, string filePath, string sourceFilePath)
        {
            if (documentTable == null || documentTable.Rows.Count == 0)
                return;
            int filePathColumnIndex = documentTable.Columns.IndexOf(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_PATH);
            int sourceFilePathColumnIndex = documentTable.Columns.IndexOf(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_SOURCE_PATH);
            int fileSizeColumnIndex = documentTable.Columns.IndexOf(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_SIZE);
            int lastUpdateDateColumnIndex = documentTable.Columns.IndexOf(DOCUMENT_TABLE_COLUMN_NAME_LAST_UPDATE_DATE);

            if (sourceFilePathColumnIndex == -1 || filePathColumnIndex == -1 || fileSizeColumnIndex == -1 || lastUpdateDateColumnIndex == -1)
                return;

            try
            {
                FileInfo fileInfo = new FileInfo(sourceFilePath);

                documentTable.Rows[0][filePathColumnIndex] = filePath;
                documentTable.Rows[0][sourceFilePathColumnIndex] = sourceFilePath;
                documentTable.Rows[0][fileSizeColumnIndex] = byteSizeToString(fileInfo.Length);
                documentTable.Rows[0][lastUpdateDateColumnIndex] = DateTime.Now;
                documentTable.AcceptChanges();
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Error occured while updating a Document: " + exc.Message);                
            }
        }

        internal static void copyDocumentsFromOldProjectDirectoryIntoNewProject(string oldProjectDocumentsDirectoryPath, string newProjectDocumentsDirectoryPath,
            List<string> desiredFileNamesWithExtension)
        {
            copyFilesFromDirectory(oldProjectDocumentsDirectoryPath, newProjectDocumentsDirectoryPath, false, desiredFileNamesWithExtension);
        }

        internal static void copyDocumentsFromTemporaryDirectoryIntoNewProject(string tempDocumentsDirectoryPath, string newProjectDocumentsDirectoryPath,
            List<string> desiredFileNamesWithExtension)
        {
            copyFilesFromDirectory(tempDocumentsDirectoryPath, newProjectDocumentsDirectoryPath, false, desiredFileNamesWithExtension);
        }

        /// <summary>
        /// If the list of desired files is null then all the files will be copied. The destination directory cand be created as hidden.
        /// </summary>
        /// <param name="sourceDirectoryPath"></param>
        /// <param name="destinationDirectoryPath"></param>
        /// <param name="hideDirectory"></param>
        /// <param name="desiredFileNamesWithExtension"></param>
        /// <returns></returns>
        internal static bool copyFilesFromDirectory(string sourceDirectoryPath, string destinationDirectoryPath, bool hideDirectory, 
            List<string> desiredFileNamesWithExtension)
        {
            if (sourceDirectoryPath == null || destinationDirectoryPath == null)
                return false;
            if (!Directory.Exists(sourceDirectoryPath))
                return false;            
            DirectoryInfo di = null;
            if (!Directory.Exists(destinationDirectoryPath))
            {
                try
                {
                    di = Directory.CreateDirectory(destinationDirectoryPath);
                }
                catch (Exception ex)
                {
                    if (ex is System.IO.IOException || ex is System.UnauthorizedAccessException
                        || ex is System.ArgumentException || ex is System.ArgumentNullException
                        || ex is System.IO.PathTooLongException || ex is System.IO.DirectoryNotFoundException
                        || ex is System.NotSupportedException)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Error while saving the project : Unable to create the directory " + destinationDirectoryPath + ". \t\r" + ex.Message);
                        return false;
                    }
                }
                if (hideDirectory)
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            string[] files = null;
            try
            {
                files = Directory.GetFiles(sourceDirectoryPath);
            }
            catch (Exception ex)
            {
                if (ex is System.IO.IOException || ex is System.UnauthorizedAccessException
                    || ex is System.ArgumentException || ex is System.ArgumentNullException
                    || ex is System.IO.PathTooLongException || ex is System.IO.DirectoryNotFoundException)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while saving the project : Unable to retrieve the files from the old directory (" 
                        + sourceDirectoryPath + ").\t\r" + ex.Message);
                    return false;
                }
            }
            string fileName = "";
            string destFile = "";
            // Copy the files and overwrite destination files if they already exist.
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                try
                {
                    fileName = Path.GetFileName(s);
                    //exclude other files if a list of desired files is provided
                    if (desiredFileNamesWithExtension != null && !desiredFileNamesWithExtension.Contains(fileName))
                        continue;
                }
                catch (System.ArgumentException ex)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while saving the project : Unable to retrieve the file name from the old directory for " + s + ".\t\r" + ex.Message);
                    return false;
                }
                try
                {
                    destFile = Path.Combine(destinationDirectoryPath, fileName);
                }
                catch (Exception ex)
                {
                    if (ex is System.ArgumentException || ex is System.ArgumentNullException)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Error while saving the project : Unable to create the destination path (" + destinationDirectoryPath + ").\t\r" + ex.Message);
                        return false;
                    }
                }
                try
                {
                    File.Copy(s, destFile, true);
                }
                catch (Exception ex)
                {
                    if (ex is System.IO.IOException || ex is System.UnauthorizedAccessException
                        || ex is System.ArgumentException || ex is System.ArgumentNullException
                        || ex is System.IO.PathTooLongException || ex is System.IO.DirectoryNotFoundException
                        || ex is System.NotSupportedException || ex is System.IO.FileNotFoundException)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Error while saving the project : Unable to copy the file(" + s + ") into the new directory("
                            + destFile + ").\t\r" + ex.Message);
                        return false;
                    }
                }
            }
            return true;
        }


        internal static bool deleteDirectory(string directoryPath, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                Directory.Delete(directoryPath, true);
            }
            catch (Exception exc)
            {
                errorMessage = "Error while saving the project. Could not delete the temporary Documents directory. " + exc.Message;
                OverallTools.ExternFunctions.PrintLogFile(errorMessage);
                return false;
            }
            return true;
        }

        internal static string getFilePathFromDocumentTable(DataTable documentTable)
        {
            if (documentTable == null || documentTable.Rows.Count == 0 || documentTable.Columns.IndexOf(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_PATH) == -1)
                return "";
            int filePathColumnIndex = documentTable.Columns.IndexOf(DOCUMENT_TABLE_COLUMN_NAME_DOCUMENT_PATH);
            if (documentTable.Rows[0][filePathColumnIndex] == null)
                return "";
            return documentTable.Rows[0][filePathColumnIndex].ToString();
        }

        internal static string getProjectDocumentsDirectoryPath(string projectPath)
        {
            return projectPath + "Data\\" + GlobalNames.DOCUMENTS_DIRECTORY_NAME;
        }

        internal static string getTemporaryDocumentsDirectoryPath(string tempPath)
        {
            return tempPath + GlobalNames.DOCUMENTS_DIRECTORY_NAME;
        }

        static string[] sizeMeasurementUnits = { "B", "KB", "MB", "GB", "TB" };
        internal static string byteSizeToString(double initialNbBytes)
        {
            double nbOfUnits = initialNbBytes;
            int order = 0;
            while (nbOfUnits >= 1024 && order < sizeMeasurementUnits.Length - 1)
            {
                order++;
                nbOfUnits = nbOfUnits / 1024;
            }
            double result = Math.Floor(nbOfUnits * 100) / 100;
            return String.Format("{0:0.##} {1}", result, sizeMeasurementUnits[order]);            
        }
    }
}
