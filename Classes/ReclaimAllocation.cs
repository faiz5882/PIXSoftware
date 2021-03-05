using System;
using System.Collections.Generic;
using System.Text;

namespace SIMCORE_TOOL.Classes
{
    class ReclaimAllocation
    {

        #region The names of the tables that should be present in user Data to be able to use the dynamic reclaim allocation.
        internal static String[] tsTablesIndexedOnTerminal = new String[] { "CapaQueues.txt", 
            "GroupQueues.txt", 
            "StepsPlan.txt", 
            "Structure.txt", 
            GlobalNames.sBHS_CI_Collectors + ".txt",
            GlobalNames.sBHS_General + ".txt",
            GlobalNames.sBHS_ArrivalInfeed_Groups + ".txt",
            GlobalNames.sBHS_CI_Groups + ".txt",
            GlobalNames.sBHS_TransferInfeed_Groups + ".txt",
            GlobalNames.sBHS_CI_Routing + ".txt",
            GlobalNames.sBHS_HBS3_Routing + ".txt",
            GlobalNames.sBHS_Transfer_Routing + ".txt",
            GlobalNames.sBHS_Flow_Split + ".txt",
            GlobalNames.sBHS_Process + ".txt",
            GlobalNames.sBHS_Arrival_Containers + ".txt",    
            GlobalNames.OCT_MakeUpTableName + ".txt"
        };

        internal static String[] tsTablesParametersForReclaim = new String[] { "GeneralParameters.txt", 
            "LastDeskBeforeReclaim.txt", 
            "ReclaimParameters.txt" };

        #endregion

        #region Fonctions to check is the current parameters for the simulation is able to handle the dynamic reclaim allocation
        /// <summary>
        /// Function that permits to check that the UserData contains the needed tables for a dynamic
        /// allocation of Reclaim with the Automod model. For this to be valid, the UserData needs to
        /// contains tables contained in \ref tsTablesIndexedOnTerminal and \ref tsTablesParametersForReclaim
        /// </summary>
        /// <param name="pudUserData">The user data parameters for a scenario.</param>
        /// <returns>Boolean that indicates if the actual configuration is able to simulate the dynamic reclaim allocation.</returns>
        internal static bool AllocationDynnamicIsAvailable(ParamUserData pudUserData)
        {
            return (getAvailableTerminalForDynnamicAllocation(pudUserData) != null);
        }

        /// <summary>
        /// Function that permits to get the different terminals that are valid for a dynamic
        /// allocation of Reclaim with the Automod model. For this to be valid, the UserData needs to
        /// contains tables contained in \ref tsTablesIndexedOnTerminal and \ref tsTablesParametersForReclaim
        /// </summary>
        /// <param name="pudUserData">The user data parameters for a scenario.</param>
        /// <returns>The list of indexes for terminals that are valids for the dynamic allocation.</returns>
        internal static List<Int32> getAvailableTerminalForDynnamicAllocation(ParamUserData pudUserData)
        {
            if (pudUserData == null)
                return null;

            List<Int32> lsReturn = new List<Int32>();
            int iNbTerminaux = GestionDonneesHUB2SIM.htSizeAutomodModel[PAX2SIM.sTerminalName];
            for (int i = 1; i <= iNbTerminaux; i++)
            {
                bool bValid = true;
                String sPrefixe = GlobalNames.sBHS_Prefixe + i.ToString() + "_";
                foreach (String sName in tsTablesIndexedOnTerminal)
                {
                    if (pudUserData.getUserData(sPrefixe + sName) == null)
                    {
                        bValid = false;
                        break;
                    }
                }
                if (!bValid)
                    continue;
                foreach (String sName in tsTablesParametersForReclaim)
                {
                    if (pudUserData.getUserData(sName) == null)
                    {
                        bValid = false;
                        break;
                    }
                }
                if (!bValid)
                    continue;
                lsReturn.Add(i);
            }
            if (lsReturn.Count == 0)
                return null;
            return lsReturn;
        }

        #endregion

        #region Fonctions to generate dictionnary with the tables to save in specific folders for the dynamic reclaim allocation
        /// <summary>
        /// Fonction used to generate a dictionnary with all the tables that should be saved in the user Data
        /// directory in the simulation folder.
        /// </summary>
        /// <param name="UserData">The actual UserData parameters</param>
        /// <returns>A simple dictionary with only the user data from \ref UserData that would be stored in the User Data directory of the simulation model.</returns>
        internal static Dictionary<String,String> getUserDataForUserDirectory(Dictionary<String,String>  UserData)
        {
            Dictionary<String,String> dssTmp = new Dictionary<string,string>();
                foreach (String sName in tsTablesParametersForReclaim)
                {
                    dssTmp.Add(sName,UserData[sName]);
                }
            return dssTmp;
        }
        /// <summary>
        /// Fonction used to generate a dictionnary with all the tables that should be saved in the Data
        /// directory in the simulation folder.
        /// </summary>
        /// <param name="UserData">The actual UserData parameters</param>
        /// <returns>A simple dictionary with only the user data from \ref UserData that would be saved in the Data directory of the simulation model.</returns>
        internal static Dictionary<String, String> getUserDataForDataDirectory(Dictionary<String, String> UserData, int iTerminal)
        {
            Dictionary<String, String> dssTmp = new Dictionary<string, string>();

            int iNbTerminaux = GestionDonneesHUB2SIM.htSizeAutomodModel[PAX2SIM.sTerminalName];
            String sPrefixe = GlobalNames.sBHS_Prefixe + iTerminal.ToString() + "_";
            foreach (String sName in tsTablesIndexedOnTerminal)
            {
                dssTmp.Add(sPrefixe + sName, UserData[sPrefixe + sName]);
            }
            return dssTmp;
        }
        #endregion

        #region Fonction that will be used to save the User Data for the dynamic reclaim allocation in the normal directory
        /// <summary>
        /// Fonction that will save the tables used for the dynamic reclaim allocation in the normal directory dor the simulation model.
        /// </summary>
        /// <param name="dmiInput">The \ref DataManagerInput that contains all the user data</param>
        /// <param name="Directory">The actual directory used to store the UserData</param>
        /// <param name="htDefaults">The parameters for the simulation (to know what version of UserData to save in these directoryes).</param>
        public static void SaveUserDataForScenario(DataManagement.DataManagerInput dmiInput, String Directory, Dictionary<String, String> htDefaults, int iTerminal)
        {
            if (dmiInput == null)
                return;
            if (htDefaults == null)
                return;
            Dictionary<String, String> dssUserDataToUserData = getUserDataForUserDirectory(htDefaults);
            dmiInput.SaveUserDataForSimulation(Directory, dssUserDataToUserData);

            Dictionary<String, String> dssUserDataToData = getUserDataForDataDirectory(htDefaults, iTerminal);
            String sPath = Directory.Substring(0,Directory.Length-5);
            dmiInput.SaveUserDataForSimulation(sPath, dssUserDataToData);
        }
        #endregion
    }
}
