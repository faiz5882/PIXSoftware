using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections;
using SIMCORE_TOOL.com.crispico.gantt;
using System.Windows.Forms;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.DataManagement;
using System.Reflection;

namespace SIMCORE_TOOL.com.crispico.processFlow
{
    class ProcessFlowServices 
    {
        public const string sTerminalName = "Terminal";
        public const string sLevelName = "Pax Level";/*
        internal static string sMainNodeName = "Airport";
        internal static string sExtension = "pax";
        public const string sBHSName = "Baggage Handling System";
        internal const string sCheckIn = "Check In";
        public const string sCheckInGroup = "Check In Group";
        internal const string sModelIn = "Pax In";
        internal const string sModelOut = "Pax Out";
        internal const string sModelInGroup = "Pax In Group";
        internal const string sModelOutGroup = "Pax Out Group";
        internal const string sShoppingArea = "Shopping Area";
        internal const string sShoppingAreaGroup = "Shopping Area Group";
        */
        public static Dictionary<String, String> getAllGroupsPaxProcessTimes(PAX2SIM pax2sim)
        {
            Dictionary<String, String> allGroupsPaxProcessTimesDictionary = new Dictionary<String, String>();
            DataTable processTimesTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.Times_ProcessTableName);
            if (processTimesTable == null)  // PAX key flag disabled
            {
                return allGroupsPaxProcessTimesDictionary;
            }
            if (processTimesTable.Rows.Count > 0)
            {
                String groupName = "";
                String distributionType = "";
                String distributionParam1 = "";
                String distributionParam2 = "";
                String distributionParam3 = "";
                String allDistributionData = "";

                foreach (DataRow row in processTimesTable.Rows)
                {
                    groupName = row[GlobalNames.sProcessTable_Items].ToString();
                    distributionType = row[GlobalNames.sProcessTable_Distrib_1].ToString();
                    distributionParam1 = row[GlobalNames.sProcessTable_Param1_1].ToString();
                    distributionParam2 = row[GlobalNames.sProcessTable_Param2_1].ToString();
                    distributionParam3 = row[GlobalNames.sProcessTable_Param3_1].ToString();
                    allDistributionData = distributionType + " (" + distributionParam1 + " "
                                            + distributionParam2 + " " + distributionParam3 + ")";
                    if (groupName != "")
                        allGroupsPaxProcessTimesDictionary.Add(groupName, allDistributionData);
                }
            }
            return allGroupsPaxProcessTimesDictionary;
        }

        public static Dictionary<String, String> getAllGroupsBaggProcessTimes(PAX2SIM pax2sim)
        {
            Dictionary<String, String> allGroupsBaggProcessTimesDictionary = new Dictionary<String, String>();
            DataTable processTimesTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.Times_ProcessTableName);
            if (processTimesTable == null)  // PAX key flag disabled
            {
                return allGroupsBaggProcessTimesDictionary;
            }
            if (processTimesTable.Rows.Count > 0)
            {
                String groupName = "";
                String distributionType = "";
                String distributionParam1 = "";
                String distributionParam2 = "";
                String distributionParam3 = "";
                String allDistributionData = "";

                foreach (DataRow row in processTimesTable.Rows)
                {
                    groupName = row[GlobalNames.sProcessTable_Items].ToString();
                    distributionType = row[GlobalNames.sProcessTable_Distrib_2].ToString();
                    distributionParam1 = row[GlobalNames.sProcessTable_Param1_2].ToString();
                    distributionParam2 = row[GlobalNames.sProcessTable_Param2_2].ToString();
                    distributionParam3 = row[GlobalNames.sProcessTable_Param3_2].ToString();
                    allDistributionData = distributionType + " (" + distributionParam1 + " "
                                            + distributionParam2 + " " + distributionParam3 + ")";
                    if (groupName != "")
                        allGroupsBaggProcessTimesDictionary.Add(groupName, allDistributionData);
                }
            }
            return allGroupsBaggProcessTimesDictionary;
        }

        public static Dictionary<String, String> getAllGroupsCapacities(PAX2SIM pax2sim)
        {
            Dictionary<String, String> allGroupsCapacitiesDictionary = new Dictionary<String, String>();
            DataTable groupQueuesCapacitiesTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.Group_QueuesName);
            if (groupQueuesCapacitiesTable == null) // PAX key flag disabled
            {
                return allGroupsCapacitiesDictionary;
            }
            if (groupQueuesCapacitiesTable.Rows.Count > 0)
            {
                String groupName = "";
                String groupQueueCapacity = "";

                foreach (DataRow row in groupQueuesCapacitiesTable.Rows)
                {
                    groupName = row[GlobalNames.sCapaQueue_Element].ToString();
                    groupQueueCapacity = row[GlobalNames.sCapaQueue_QueueCapacity].ToString();
                    allGroupsCapacitiesDictionary.Add(groupName, groupQueueCapacity);
                }
            }
            return allGroupsCapacitiesDictionary;
        }

        public static Dictionary<String, String> getAllGroupDescriptions(PAX2SIM pax2sim)
        {
            Dictionary<String, String> allGroupsDescriptionsDictionary = new Dictionary<String, String>();
            ArrayList groupNames = pax2sim.DonneesEnCours.getAllGroups();
            ArrayList groupDescriptions = pax2sim.DonneesEnCours.getAllGroupsDescriptions();

            if (groupNames.Count == groupDescriptions.Count)
            {
                for (int i = 0; i < groupNames.Count; i++)
                {
                    String groupName = groupNames[i].ToString();
                    String groupDescription = groupDescriptions[i].ToString();
                    allGroupsDescriptionsDictionary.Add(groupName, groupDescription);
                }
            }
            return allGroupsDescriptionsDictionary;
        }

        // << Task #8789 Pax2Sim - ProcessFlow - update Group details
        public static Dictionary<String, String> getAllGroupsDelayTimeDistributions(PAX2SIM pax2sim)
        {
            Dictionary<String, String> delayTimeDistributionsDictionary = new Dictionary<String, String>();
            DataTable processTimesTable = pax2sim.DonneesEnCours.getTable("Input", GlobalNames.Times_ProcessTableName);

            if (processTimesTable != null && processTimesTable.Rows.Count > 0)
            {
                String groupName = "";
                String referenceTime = "";
                String distributionType = "";
                String firstParam = "";
                String secondParam = "";
                String thirdParam = "";
                String completeDistribution = "";

                foreach (DataRow row in processTimesTable.Rows)
                {
                    groupName = row[GlobalNames.sProcessTable_Items].ToString();
                    referenceTime = row[GlobalNames.sProcessTable_WaitingTimeReference].ToString();
                    distributionType = row[GlobalNames.sProcessTable_Distrib_3].ToString();
                    firstParam = row[GlobalNames.sProcessTable_Param1_3].ToString();
                    secondParam = row[GlobalNames.sProcessTable_Param2_3].ToString();
                    thirdParam = row[GlobalNames.sProcessTable_Param3_3].ToString();

                    completeDistribution = referenceTime + " " + distributionType + " ("
                        + firstParam + " " + secondParam + " " + thirdParam + ")";

                    if (groupName != "")
                        delayTimeDistributionsDictionary.Add(groupName, completeDistribution);
                }
            }
            return delayTimeDistributionsDictionary;
        }
        // >> Task #8789 Pax2Sim - ProcessFlow - update Group details

        public static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static String serializeItineraryObjectsForXml(byte[] byteArray)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(byteArray.GetType());
            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    serializer.Serialize(writer, byteArray);
                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.InnerException.Message + "\n" + ex.InnerException.StackTrace);
                }
                return "";
            }
        }

        public static Dictionary<String, TreeNode> getAirportNodesDictionary(TreeView treeView, PAX2SIM pax2Sim)
        {
            Dictionary<String, TreeNode> airportNodesDictionary = new Dictionary<String, TreeNode>();

            foreach (TreeNode terminal in treeView.Nodes[0].Nodes)
            {
                if (((TreeViewTag)terminal.Tag).AirportObjectType != sTerminalName)
                    continue;
                TreeViewTag tvtTerminal = (TreeViewTag)terminal.Tag;

                String terminalKey = "T" + tvtTerminal.Index.ToString();
                airportNodesDictionary.Add(terminalKey, terminal);

                foreach (TreeNode level in terminal.Nodes)
                {
                    if (((TreeViewTag)level.Tag).AirportObjectType != sLevelName)
                        continue;
                    TreeViewTag tvtLevel = (TreeViewTag)level.Tag;

                    String levelKey = terminalKey + "L" + tvtLevel.Index.ToString();
                    airportNodesDictionary.Add(levelKey, level);

                    foreach (TreeNode group in level.Nodes)
                    {
                        TreeViewTag tvtGroup = (TreeViewTag)group.Tag;

                        String groupKey = levelKey + "_" + tvtGroup.AirportObjectType + " " + tvtGroup.Index;
                        airportNodesDictionary.Add(groupKey, group);

                        foreach (TreeNode desk in group.Nodes)
                        {
                            TreeViewTag tvtDesk = (TreeViewTag)desk.Tag;

                            String deskKey = levelKey + "_" + tvtDesk.AirportObjectType + " " + tvtDesk.Index;
                            airportNodesDictionary.Add(deskKey, desk);
                        }
                    }
                }
            }
            return airportNodesDictionary;
        }

        /// <summary>
        /// Used to save the content of the BackgroundImage directory when saving the project.
        /// Searches for all the backgroung image files in the old location of the project
        ///  and copies them in the new location.
        /// </summary>
        /// <param name="newProjectPath">new location for the project</param>
        /// <param name="oldProjectPath">old location for the project</param>
        /// <param name="errorList">the application's list of errors</param>
        /// <returns></returns>
        public static bool saveBackgroundImageFilesForProcessFlows(string newProjectPath, string oldProjectPath, ArrayList errorList)
        {
            String newBackgroundFilePath = "";
            String oldBackgroundFilePath = "";
            
            String backgroundFileOldDirectoryPath = oldProjectPath + "\\Data\\" + Model.BACKGROUND_IMAGE_DIRECTORY_NAME + "\\";
            //check if the project has any background image saved(if the directory was created)
            if (Directory.Exists(backgroundFileOldDirectoryPath))
            {
                DirectoryInfo backgroundFilesOldDirectory = new DirectoryInfo(backgroundFileOldDirectoryPath);
                FileInfo[] backgroundFiles = backgroundFilesOldDirectory.GetFiles();

                String backgroundDirectoryNewPath = newProjectPath + "\\Data\\" + Model.BACKGROUND_IMAGE_DIRECTORY_NAME;

                if (backgroundFiles.Length > 0)
                {
                    DirectoryInfo dir = null;
                    if (!Directory.Exists(backgroundDirectoryNewPath))
                    {
                        try
                        {
                            dir = Directory.CreateDirectory(backgroundDirectoryNewPath);
                        }
                        catch (Exception ex)
                        {
                            if (ex is System.IO.IOException || ex is System.UnauthorizedAccessException
                                || ex is System.ArgumentException || ex is System.ArgumentNullException
                                || ex is System.IO.PathTooLongException || ex is System.IO.DirectoryNotFoundException
                                || ex is System.NotSupportedException)
                            {
                                errorList.Add("ErrG001 : a problem appeared while saving the project : Unable to create the directory BackgroundImage \t\r" + ex.Message);
                                OverallTools.ExternFunctions.PrintLogFile("Exception caught while creating the directory BackgroundImage " + ex.Message);
                                return false;
                            }
                        }
                        dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    }
                    try
                    {
                        foreach (FileInfo backgroundFile in backgroundFiles)
                        {
                            oldBackgroundFilePath = oldProjectPath + "\\Data\\" + Model.BACKGROUND_IMAGE_DIRECTORY_NAME + "\\" + backgroundFile.Name;
                            newBackgroundFilePath = newProjectPath + "\\Data\\" + Model.BACKGROUND_IMAGE_DIRECTORY_NAME + "\\" + backgroundFile.Name;
                            if (File.Exists(oldBackgroundFilePath))
                                File.Copy(oldBackgroundFilePath, newBackgroundFilePath, true);
                        }
                    }
                    catch (Exception e)
                    {
                        errorList.Add("ErrG001 : a problem appeared while saving the project : Unable to copy the Background Image files for the Process Flow. \t\r" + e.Message);
                        OverallTools.ExternFunctions.PrintLogFile("Exception caught while copying the Background Image "
                            + "files for the Process Flow. " + e.Message);
                        return false;
                    }
                }
            }
            return true;
        }

        public static void saveAllProcessFlowsFromProject(Dictionary<String, List<ItineraryData>> globalItineraryDataListDictionary,
                                                          String currentProcessFlowName, GestionDonneesHUB2SIM doneesEnCours,
                                                          bool exceptSelected)
        {
            foreach (KeyValuePair<String, List<ItineraryData>> pair in globalItineraryDataListDictionary)
            {
                String processFlowName = pair.Key;
                if (exceptSelected && processFlowName == currentProcessFlowName)
                    continue;
                List<ItineraryData> itineraryGroupList = pair.Value;
                if (itineraryGroupList != null)
                {
                    String groupsFilePath = doneesEnCours.getDossierEnregistrement() + "Data\\"
                        + Model.itineraryGroupsTextFilePrefix + processFlowName + Model.itineraryGroupsTextFileExtention;
                    
                    // << Bug #8210 Pax2Sim - ProcessFlow - save issue                    
                    if (doneesEnCours.getDossierEnregistrement() == "\\")
                        continue;
                    // >> Bug #8210 Pax2Sim - ProcessFlow - save issue

                    try
                    {
                        System.IO.StreamWriter writer = new StreamWriter(groupsFilePath);
                        foreach (ItineraryData group in itineraryGroupList)
                        {
                            writer.WriteLine(group.name + ";" + group.description + ";" + group.itineraryDataType
                                + ";" + group.x + ";" + group.y + ";" + group.width + ";" + group.height + ";" + group.paxProcessTimes
                                + ";" + group.bagProcessTimes + ";" + group.capacity + ";" + group.firstGradientColor + ";" + group.firstGradientAlpha
                                 + ";" + group.secondGradientColor + ";" + group.secondGradientAlpha + ";" + group.borderColor
                                 + ";" + group.gradientAngle
                                 + ";" + group.delayTimeDistribution);// << Task #8789 Pax2Sim - ProcessFlow - update Group details
                        }
                        writer.Close();
                        writer.Dispose();
                    }
                    catch (Exception e)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Exception while saving process flows " + e.Message);
                    }
                    
                }
            }
        }
        
        /// <summary>
        /// Saves the .txt files that contain the bendpoints for the connections. Each file
        /// is related to a Process Flow.
        /// </summary>
        /// <param name="newProjectPath">The new path where the project will be saved </param>
        /// <param name="oldProjectPath">The path of the project before saving</param>
        /// <param name="errorList">The application's error list</param>
        public static void saveConnectionBendpointsFiles(string newProjectPath, string oldProjectPath, ArrayList errorList,
                                                         GestionDonneesHUB2SIM doneesEnCours)
        {
            if (newProjectPath == null || oldProjectPath == null)
                return;
            NormalTable itineraryTable = doneesEnCours.GetTable("Input", GlobalNames.ItineraryTableName);
            List<string> filtersForItineraryTable = itineraryTable.GetFilters();

            List<string> partialFilePathsForFilters = new List<string>();
            for (int i = 0; i < filtersForItineraryTable.Count; i++)
            {
                String filterName = filtersForItineraryTable[i];
                String partialFilePath = "\\Data\\" + Model.itineraryConnectionsBendPointsTextFilePrefix
                    + filterName + Model.itineraryConnectionsBendPointsFileExtention;
                partialFilePathsForFilters.Add(partialFilePath);
            }
            String itineraryTableFilePath = "\\Data\\" + Model.itineraryConnectionsBendPointsTextFilePrefix
                + GlobalNames.ItineraryTableName + Model.itineraryConnectionsBendPointsFileExtention;
            partialFilePathsForFilters.Add(itineraryTableFilePath);

            for (int j = 0; j < partialFilePathsForFilters.Count; j++)
            {
                String partialFilePath = partialFilePathsForFilters[j];
                String fileOldPath = oldProjectPath + partialFilePath;
                String fileNewPath = newProjectPath + partialFilePath;

                try
                {
                    if (File.Exists(fileOldPath))
                        File.Copy(fileOldPath, fileNewPath, true);
                }
                catch(Exception e)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Exception caught while copying " +
                        "ItineraryConnectionsBendpoints_*.txt files. " + e.Message);
                }
            } 
        }

        // << Task #8819 Pax2Sim - ProcessFlow - save Details parameters
        /// <summary>
        /// The file will have the following format for each line:
        /// parameterName:parameterValue
        /// </summary>
        /// <param name="processFlowName">The name of the processFlow for which the params are saved.</param>
        /// <param name="processFlowParameters">The processFlow's params</param>
        /// <param name="donneesEnCours">Provides access to diffrent functions used here.</param>
        public static void saveProcessFlowShowDetailsParameters(String processFlowName, 
            ProcessFlowParameters processFlowParameters, GestionDonneesHUB2SIM donneesEnCours)
        {
            String processFlowParamsFilePath = "";
            String projectDirectoryPath = donneesEnCours.getDossierEnregistrement();

            if (projectDirectoryPath != "\\")
            {
                processFlowParamsFilePath = projectDirectoryPath + "Data\\"
                    + Model.itineraryParamsTextFilePrefix + processFlowName + Model.itineraryParamsTextFileExtension;
                try
                {
                    System.IO.StreamWriter writer = new StreamWriter(processFlowParamsFilePath);
                    if (writer != null)
                    {
                        foreach (PropertyInfo parameter in processFlowParameters.GetType().GetProperties())
                        {
                            object parameterValue = parameter.GetValue(processFlowParameters, null);
                            writer.WriteLine(parameter.Name + ":" + parameterValue.ToString());
                        }
                        writer.Close();
                        writer.Dispose();
                    }
                }
                catch (Exception exc)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Exception while saving the process flow parameters for " 
                        + processFlowName + ". " + exc.Message);
                }
            }
        }

        public static ProcessFlowParameters getProcessFlowParametersFromFile(String processFlowName,
            GestionDonneesHUB2SIM donneesEnCours)
        {
            ProcessFlowParameters processFlowParameters = new ProcessFlowParameters();
            String projectDirectoryPath = donneesEnCours.getDossierEnregistrement();
            String processFlowParamsFilePath = "";
            
            if (projectDirectoryPath != "\\")
            {
                processFlowParamsFilePath = projectDirectoryPath + "Data\\"
                    + Model.itineraryParamsTextFilePrefix + processFlowName + Model.itineraryParamsTextFileExtension;
                if (File.Exists(processFlowParamsFilePath))
                {
                    try
                    {
                        System.IO.StreamReader reader = new StreamReader(processFlowParamsFilePath);
                        String paramInfoLine = "";

                        if (reader != null)
                        {
                            while ((paramInfoLine = reader.ReadLine()) != null)
                            {
                                string parameterName = "";
                                bool parameterValue;
                                string[] result = paramInfoLine.Split(':');

                                parameterName = result[0];

                                if (Boolean.TryParse(result[1], out parameterValue))
                                {
                                    if (processFlowParameters.GetType() != null
                                        && processFlowParameters.GetType().GetProperty(parameterName) != null)
                                    {
                                        processFlowParameters.GetType().GetProperty(parameterName)
                                            .SetValue(processFlowParameters, parameterValue, null);
                                    }
                                }
                            }
                            reader.Close();
                            reader.Dispose();
                        }
                    }
                    catch (System.Reflection.AmbiguousMatchException amExc)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("More than one member match the given parameter. "
                            + "Exception caught while reading the process flow parameters for "
                            + processFlowName + ". " + amExc.Message);
                    }
                    catch (System.OutOfMemoryException oomExc)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Out of memory while reading the process flow parameters for "
                            + processFlowName + ". " + oomExc.Message);
                    }
                    catch (Exception exc)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Exception while reading the process flow parameters for "
                            + processFlowName + ". " + exc.Message);
                    }
                }
            }

            return processFlowParameters;
        }

        public static void saveProcessFlowShowDetailsParametersFiles(string newProjectPath, string oldProjectPath,
            ArrayList errorList, GestionDonneesHUB2SIM doneesEnCours)
        {
            if (newProjectPath != null && oldProjectPath != null)
            {                
                if (PAX2SIM.processFlowParametersDictionary != null && PAX2SIM.processFlowParametersDictionary.Count > 0)
                {
                    foreach (KeyValuePair<String, ProcessFlowParameters> pair in PAX2SIM.processFlowParametersDictionary)
                    {
                        String retrievedProcessFlowName = pair.Key;
                        String partialFilePath = "\\Data\\" + Model.itineraryParamsTextFilePrefix
                            + retrievedProcessFlowName + Model.itineraryParamsTextFileExtension;
                        String fileOldPath = oldProjectPath + partialFilePath;
                        String fileNewPath = newProjectPath + partialFilePath;

                        try
                        {
                            if (File.Exists(fileOldPath))
                                File.Copy(fileOldPath, fileNewPath, true);
                        }
                        catch (Exception e)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("Exception caught while copying " +
                                "ItineraryParameters_ItineraryTable_*.txt files. " + e.Message);
                            return;
                        }
                    }
                }
            }
            else if (newProjectPath != null)    //when saving a new project for the first time
            {
                if (PAX2SIM.processFlowParametersDictionary != null && PAX2SIM.processFlowParametersDictionary.Count > 0)
                {
                    foreach (KeyValuePair<String, ProcessFlowParameters> pair in PAX2SIM.processFlowParametersDictionary)
                    {
                        String retrievedProcessFlowName = pair.Key;
                        ProcessFlowParameters parameters = pair.Value;
                        if (parameters != null)
                        {
                            String processFlowParamsFilePath = newProjectPath + "\\Data\\"
                                + Model.itineraryParamsTextFilePrefix
                                + retrievedProcessFlowName
                                + Model.itineraryParamsTextFileExtension;
                            try
                            {
                                System.IO.StreamWriter writer = new StreamWriter(processFlowParamsFilePath);
                                if (writer != null)
                                {
                                    foreach (PropertyInfo parameter in parameters.GetType().GetProperties())
                                    {
                                        object parameterValue = parameter.GetValue(parameters, null);
                                        writer.WriteLine(parameter.Name + ":" + parameterValue.ToString());
                                    }
                                    writer.Close();
                                    writer.Dispose();
                                }
                            }
                            catch (Exception exc)
                            {
                                OverallTools.ExternFunctions.PrintLogFile("Exception while saving the process flow parameters"
                                    + "for all process flows." + exc.Message);
                            }
                        }
                    }
                }
            }
        }
        // >> Task #8819 Pax2Sim - ProcessFlow - save Details parameters

    }
}
