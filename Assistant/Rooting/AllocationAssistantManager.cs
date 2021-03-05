using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.Assistant
{
    public partial class AllocationAssistantManager : Form
    {
        // >> Task #15781 Various TreeView assistans improvements C#1_1

        protected enum PLANNING_RESOURCE_TYPES
        {
            PASSPORT_CHECK, SECURITY_CHECK, TRANSFER, USER_PROCESS, NONE
        };
        // << Task #15781 Various TreeView assistans improvements C#1_1

        #region Les informations contenues dans ce formulaire
        List<List<GroupAllocation>> lesAllocations;
        ArrayList GroupName;
        DataTable AllocTable;
        List<String> FlightCategories;
        List<String> AirlineCodes;
        List<String> FPATable;
        List<String> FPDTable;
        bool bDateInvalid;
        //<< Task #7405 - new Desk and extra information for Pax
        GestionDonneesHUB2SIM donneesEnCours;
        DataManagement.NormalTable userAttributesTable;
        Dictionary<String, DataManagement.NormalTable> distributionForUserAttribTables;        
        //>> Task #7405 - new Desk and extra information for Pax
        #endregion

        Dictionary<string, string[]> resourceTypeStationsByTerminalNb = new Dictionary<string, string[]>();   // >> Task #15781 Various TreeView assistans improvements C#1_1
        static PLANNING_RESOURCE_TYPES currentPlanningResourceType = PLANNING_RESOURCE_TYPES.NONE;

        #region Constructeurs et fonction d'initialisation.
        public AllocationAssistantManager(DataTable AllocTable_, 
            DataTable FlightCategories_,
            DataTable AirlineCodes_,
            DataTable FPATable_,
            DataTable FPDTable_,
            bool bPassport,
            //<< Task #7405 - new Desk and extra information for Pax            
            GestionDonneesHUB2SIM pDonneesEnCours
            //>> Task #7405 - new Desk and extra information for Pax
            )
        {
            InitializeComponent();
            bDateInvalid = false;
            OverallTools.FonctionUtiles.MajBackground(this);
            AllocTable = AllocTable_;
            GroupName = new ArrayList();
            FlightCategories = new List<String>();
            AirlineCodes = new List<String>();
            FPATable = new List<String>();
            FPDTable = new List<String>();
            lesAllocations = new List<List<GroupAllocation>>();
            for (int i = 0; i < FlightCategories_.Rows.Count; i++)
            {
                FlightCategories.Add(FonctionsType.getString( FlightCategories_.Rows[i][0]));
            }
            for (int i = 0; i < AirlineCodes_.Rows.Count; i++)
            {
                AirlineCodes.Add(FonctionsType.getString(AirlineCodes_.Rows[i][0]));
            }
            for (int i = 0; i < FPATable_.Rows.Count; i++)
            {
                FPATable.Add("A_"+ FonctionsType.getString(FPATable_.Rows[i][0]));
            }
            for (int i = 0; i < FPDTable_.Rows.Count; i++)
            {
                FPDTable.Add("D_" + FonctionsType.getString(FPDTable_.Rows[i][0]));
            }

            if (AllocTable_.Rows.Count != 0)
            {
                dt_BeginDate.Value = (DateTime)AllocTable_.Rows[0][0];
                dt_EndDate.Value = (DateTime)AllocTable_.Rows[AllocTable_.Rows.Count - 1][0];
                if (AllocTable_.Rows.Count > 1)
                    tb_Step.Text = OverallTools.DataFunctions.MinuteDifference((DateTime)AllocTable_.Rows[0][0], (DateTime)AllocTable_.Rows[1][0]).ToString();
                else
                    tb_Step.Text = "15";
            }
            else
            {
                dt_BeginDate.Value = DateTime.Now.Date;
                dt_EndDate.Value = DateTime.Now.Date.AddDays(1);
                tb_Step.Text = "15";
            }
            // << Task #8539 Pax2Sim - Allocation Manager - missing information
            Dictionary<String, String> allGroupsDescriptionsDictionary = new Dictionary<String, String>();
            ArrayList groupNames = pDonneesEnCours.getAllGroups();
            ArrayList groupDescriptions = pDonneesEnCours.getAllGroupsDescriptions();

            if (groupNames != null && groupDescriptions != null 
                && groupNames.Count == groupDescriptions.Count)
            {
                for (int i = 0; i < groupNames.Count; i++)
                {
                    String groupName = groupNames[i].ToString();
                    String groupDescription = groupDescriptions[i].ToString();
                    allGroupsDescriptionsDictionary.Add(groupName, groupDescription);
                }
            }
            // >> Task #8539 Pax2Sim - Allocation Manager - missing information
            foreach (DataColumn colonne in AllocTable_.Columns)
            {
                if (colonne.ColumnName != "Time")
                {
                    // << Task #8539 Pax2Sim - Allocation Manager - missing information
                    String description = "";
                    String formatedDescription = "";
                    if (allGroupsDescriptionsDictionary.TryGetValue(colonne.ColumnName, out description)
                        && description != null && description != "")
                    {
                        formatedDescription = " (" + description + ")";
                    }
                    listBox1.Items.Add(colonne.ColumnName + formatedDescription);
                    //listBox1.Items.Add(colonne.ColumnName);
                    // >> Task #8539 Pax2Sim - Allocation Manager - missing information
                    GroupName.Add(colonne.ColumnName);
                    lesAllocations.Add(new List<GroupAllocation>());
                }
            }
            //<< Task #7405 - new Desk and extra information for Pax
            this.donneesEnCours = pDonneesEnCours;
            this.userAttributesTable = donneesEnCours.GetTable("Input", GlobalNames.sUserAttributesTableName);
            this.distributionForUserAttribTables = new Dictionary<string, SIMCORE_TOOL.DataManagement.NormalTable>();
            if (this.userAttributesTable != null)
            {
                foreach (DataRow dr in this.userAttributesTable.Table.Rows)
                {
                    String distribTableName = dr[GlobalNames.sUserAttributes_ColumnName].ToString();
                    // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                    if (GlobalNames.nonUserAttributesExceptionsList.Contains(distribTableName))
                        continue;
                    // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                    DataManagement.NormalTable distributionTable = donneesEnCours.GetTable("Input", distribTableName);
                    if (distributionTable != null)
                        this.distributionForUserAttribTables.Add(distribTableName, distributionTable);
                    // add the column to the Allocation table
                    DataGridViewColumn  newCol = new DataGridViewColumn();
                    DataGridViewCell cell = AllocationTable.Columns[0].CellTemplate; 
                    newCol.CellTemplate = cell;
                    newCol.HeaderText = distribTableName;
                    AllocationTable.Columns.Add(newCol);                    
                }
            }
            InitializeAllocation(OverallTools.DataFunctions.convertToAllocTable(AllocTable_, bPassport, this.donneesEnCours));
            //>> Task #7405 - new Desk and extra information for Pax
            
            // >> Task #15781 Various TreeView assistans improvements C#1_1            
            currentPlanningResourceType = getCurrentPlanningResourceTypeByPlanningTableName(AllocTable_.TableName);
            if (currentPlanningResourceType != PLANNING_RESOURCE_TYPES.NONE)
            {
                resourceTypeStationsByTerminalNb = new Dictionary<string, string[]>();
                string[] terminalNames = donneesEnCours.getTerminal();
                foreach (string terminalName in terminalNames)
                {
                    string[] stations = getStationsByPlanningResourceType(currentPlanningResourceType, terminalName);
                    if (stations != null && !resourceTypeStationsByTerminalNb.ContainsKey(terminalName))
                        resourceTypeStationsByTerminalNb.Add(terminalName, stations);
                }
            }
            // << Task #15781 Various TreeView assistans improvements C#1_1
        }
                
        private void InitializeAllocation(DataTable laTable)
        {
            foreach (DataRow ligne in laTable.Rows)
            {
                for (int i = 0; i < GroupName.Count; i++)
                {
                    if (GroupName[i].ToString() == ligne.ItemArray[0].ToString())
                    {
                        DateTime BeginDate;
                        DateTime EndDate;
                        int StationsNumber;

                        GroupAllocation grpAlloc = new GroupAllocation();
                        DateTime.TryParse(ligne.ItemArray[1].ToString(), out BeginDate);
                        DateTime.TryParse(ligne.ItemArray[2].ToString(), out EndDate);
                        if (BeginDate == EndDate)
                            continue;
                        Int32.TryParse(ligne.ItemArray[3].ToString(), out StationsNumber);
                        grpAlloc.BeginDate = BeginDate;
                        grpAlloc.EndDate = EndDate;
                        grpAlloc.StationsNumber = StationsNumber;
                        grpAlloc.TimeType = ligne.ItemArray[4].ToString();
                        grpAlloc.PassportType = ligne.ItemArray[5].ToString();
                        grpAlloc.ClassType = ligne.ItemArray[6].ToString();
                        grpAlloc.FlightCategory = ligne.ItemArray[7].ToString();
                        grpAlloc.Airlines = ligne.ItemArray[8].ToString();
                        grpAlloc.FlightIdentification = ligne.ItemArray[9].ToString();
                        //<< Task #7405 - new Desk and extra information for Pax
                        //extra columns from user attribute tables = nb rows of the user attribute table 
                        // => extra indexes to search the converted table
                        int nbOfExtraColumns = -1;
                        int index = 9;
                        if (this.userAttributesTable != null)
                            nbOfExtraColumns = this.userAttributesTable.Table.Rows.Count;
                        // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                        int nbOfNonUserAttributesColumns = 0;
                        if (this.userAttributesTable != null)
                        {
                            foreach (DataRow rw in this.userAttributesTable.Table.Rows)
                            {
                                if (rw[GlobalNames.sUserAttributes_ColumnName] != null 
                                    && GlobalNames.nonUserAttributesExceptionsList
                                                    .Contains(rw[GlobalNames.sUserAttributes_ColumnName].ToString()))
                                {
                                    nbOfNonUserAttributesColumns++;
                                }
                            }
                            nbOfExtraColumns = nbOfExtraColumns - nbOfNonUserAttributesColumns;
                        }
                        // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                        if (nbOfExtraColumns > 0)
                        {
                            foreach (DataRow dr in this.userAttributesTable.Table.Rows)
                            {                                
                                String distribTableName = dr[GlobalNames.sUserAttributes_ColumnName].ToString();
                                String values = "";
                                // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                                if (GlobalNames.nonUserAttributesExceptionsList.Contains(distribTableName))
                                    continue;
                                // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                                index++;
                                if (ligne.ItemArray[index] != null)
                                    values = ligne.ItemArray[index].ToString();
                                grpAlloc.userAttributeValuesDictionary.Add(distribTableName, values);
                            }
                        }
                        //>> Task #7405 - new Desk and extra information for Pax

                        lesAllocations[i].Add(grpAlloc);
                    }
                }
            }
        }

        #endregion

        private PLANNING_RESOURCE_TYPES getCurrentPlanningResourceTypeByPlanningTableName(string planningTableName) // >> Task #15781 Various TreeView assistans improvements C#1_1
        {
            if (planningTableName == null)
                return PLANNING_RESOURCE_TYPES.NONE;
            if (planningTableName == "Alloc_PassportCheckTable")
                return PLANNING_RESOURCE_TYPES.PASSPORT_CHECK;
            else if (planningTableName == "Alloc_SecurityCheckTable")
                return PLANNING_RESOURCE_TYPES.SECURITY_CHECK;
            else if (planningTableName == "Alloc_TransferDeskTable")
                return PLANNING_RESOURCE_TYPES.TRANSFER;
            else if (planningTableName == "Alloc_UserProcessCheckTable")
                return PLANNING_RESOURCE_TYPES.USER_PROCESS;
            return PLANNING_RESOURCE_TYPES.NONE;
        }

        private string[] getStationsByPlanningResourceType(PLANNING_RESOURCE_TYPES planningResourceType, string terminalName)
        {
            string[] stations = null;
            if (planningResourceType == PLANNING_RESOURCE_TYPES.PASSPORT_CHECK)
                stations = donneesEnCours.getPassportCheckDesks(terminalName);
            else if (planningResourceType == PLANNING_RESOURCE_TYPES.SECURITY_CHECK)
                stations = donneesEnCours.getSecurityCheckDesks(terminalName);
            else if (planningResourceType == PLANNING_RESOURCE_TYPES.TRANSFER)
                stations = donneesEnCours.getTransferDesks(terminalName);
            else if (planningResourceType == PLANNING_RESOURCE_TYPES.USER_PROCESS)
                stations = donneesEnCours.getUserProcessDesks(terminalName);
            return stations;
        }
        #region la class GroupAllocation
        internal class GroupAllocation
        {
            private DateTime beginDate;
            private DateTime endDate;
            private int stationsNumber;
            private String timeType;
            private String passportType;
            private String classType;
            private String flightCategory;
            private String airlines;
            private String flightIdentification;
            //<< Task #7405 - new Desk and extra information for Pax            
            public Dictionary<String, String> userAttributeValuesDictionary = new Dictionary<string,string>();
            //>> Task #7405 - new Desk and extra information for Pax

            internal GroupAllocation(DateTime beginDate, DateTime endDate, int stationsNumber, String timeType, String passportType,
                String classType, String flightCategory, String airlines, String flightIdentification, Dictionary<String, String> userAttributeValuesDictionary)
            {
                this.beginDate = beginDate;
                this.endDate = endDate;
                this.stationsNumber = stationsNumber;
                this.timeType = timeType;
                this.passportType = passportType;
                this.classType = classType;
                this.flightCategory = flightCategory;
                this.airlines = airlines;
                this.flightIdentification = flightIdentification;
                //<< Task #7405 - new Desk and extra information for Pax  
              
                this.userAttributeValuesDictionary = new Dictionary<string,string>(userAttributeValuesDictionary);
                //>> Task #7405 - new Desk and extra information for Pax
            }

            internal GroupAllocation(DateTime beginDate, DateTime endDate, String sLine, bool bPassport, List<string> userAttributeNames)
            {
                this.beginDate = beginDate;
                this.endDate = endDate;
                String[] valueList = sLine.Split(';');
                this.stationsNumber = 0;
                this.timeType = GlobalNames.sAllocation_NotApplicable;
                this.passportType = GlobalNames.sAllocation_All;
                this.classType = GlobalNames.sAllocation_All;
                this.flightCategory = GlobalNames.sAllocation_All;
                this.airlines = GlobalNames.sAllocation_All;
                this.flightIdentification = GlobalNames.sAllocation_All;
                //<< Task #7405 - new Desk and extra information for Pax                
                //initialize the user attribute values dictionary with default value (All)                
                /*for (int i = 0; i < userAttributeNames.Count; i++)
                {
                    String userAttribute = userAttributeNames[i];
                    this.userAttributeValuesDictionary.Add(userAttribute, "All");
                }*/
                int nbOfExtraAttributes = userAttributeNames.Count;
                //>> Task #7405 - new Desk and extra information for Pax
                if (valueList.Length != 7 + nbOfExtraAttributes)    //>> Task #7405 - new Desk and extra information for Pax
                {
                    if (valueList.Length == 1)
                    {
                        valueList = valueList[0].Split(',');
                        if (valueList.Length == 1)
                        {
                            #region Pour les postes transfer
                            this.stationsNumber = FonctionsType.getInt(valueList[0]);
                            #endregion
                        }
                        else if (valueList.Length == 2)
                        {
                            #region Pour les postes security ou Passport
                            this.stationsNumber = FonctionsType.getInt(valueList[1]);
                            if (bPassport)
                            {
                                int iType = FonctionsType.getInt(valueList[0]);
                                if (iType == 1)
                                    this.passportType = GlobalNames.sAllocation_Local;
                                else if (iType == 2)
                                    this.passportType = GlobalNames.sAllocation_NotLocal;
                            }
                            else
                            {
                                int iType = FonctionsType.getInt(valueList[0]);
                                if (iType == 1)
                                    this.classType = GlobalNames.sAllocation_FB;
                                else if (iType == 2)
                                    this.classType = GlobalNames.sAllocation_Eco;
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    this.stationsNumber = FonctionsType.getInt(valueList[0]);
                    if (valueList[1] != "")
                        this.timeType = FonctionsType.getString(valueList[1]);
                    if (valueList[2] != "")
                        this.passportType = FonctionsType.getString(valueList[2]);
                    if (valueList[3] != "")
                        this.classType = FonctionsType.getString(valueList[3]);
                    if (valueList[4] != "")
                        this.flightCategory = FonctionsType.getString(valueList[4]);
                    if (valueList[5] != "")
                        this.airlines = FonctionsType.getString(valueList[5]);
                    if (valueList[6] != "")
                        this.flightIdentification = FonctionsType.getString(valueList[6]);
                    //<< Task #7405 - new Desk and extra information for Pax                    
                    if (valueList.Length == 7 + nbOfExtraAttributes)
                    {
                        int index = 6;
                        for (int i = 1; i <= nbOfExtraAttributes; i++)
                        {
                            if (valueList[index + i] != "All")
                            {
                                this.userAttributeValuesDictionary.Remove(userAttributeNames[i - 1]);
                                this.userAttributeValuesDictionary.Add(userAttributeNames[i - 1], FonctionsType.getString(valueList[index + i]));
                            }
                        }                        
                    }
                    //>> Task #7405 - new Desk and extra information for Pax
                }
            }
            internal GroupAllocation()
            {
            }

            #region setters and getters
            internal DateTime BeginDate
            {
                get
                {
                    return beginDate;
                }
                set
                {
                    beginDate = value;
                }
            }
            internal DateTime EndDate
            {
                get
                {
                    return endDate;
                }
                set
                {
                    endDate = value;
                }
            }
            internal int StationsNumber
            {
                get
                {
                    return stationsNumber;
                }
                set
                {
                    stationsNumber = value;
                }
            }
            internal String TimeType
            {
                get
                {
                    return timeType;
                }
                set
                {
                    timeType = value;
                }
            }
            internal String PassportType
            {
                get
                {
                    if (passportType == null)
                        return GlobalNames.sAllocation_All;
                    return passportType;
                }
                set
                {
                    passportType = value;
                }
            }
            internal int PassportTypeNumber
            {
                get
                {
                    if (passportType == GlobalNames.sAllocation_Local)
                        return 1;
                    if (passportType == GlobalNames.sAllocation_NotLocal)
                        return 2;
                    return 3;
                }
            }
            internal String ClassType
            {
                get
                {
                    if (classType == null)
                        return GlobalNames.sAllocation_All;
                    return classType;
                }
                set
                {
                    classType = value;
                }
            }
            internal int ClassTypeNumber
            {
                get
                {
                    if (classType == GlobalNames.sAllocation_FB)
                        return 1;
                    if (classType == GlobalNames.sAllocation_Eco)
                        return 2;
                    return 3;
                }
            }
            internal String FlightCategory
            {
                get
                {
                    if (flightCategory == null)
                        return GlobalNames.sAllocation_All;
                    return flightCategory;
                }
                set
                {
                    flightCategory = value;
                }
            }
            internal int FlightCategoryStatus
            {
                get
                {
                    if (flightCategory == GlobalNames.sAllocation_All)
                        return 0;

                    String[] lsObjets = flightCategory.Split(',');
                    bool bPositive = false;
                    foreach (String sObjet in lsObjets)
                    {
                        if (!sObjet.StartsWith("!"))
                        {
                            bPositive = true;
                            break;
                        }
                    }
                    if (bPositive)
                        return 1;
                    return -1;
                }
            }
            internal String Airlines
            {
                get
                {
                    if (airlines == null)
                        return GlobalNames.sAllocation_All;
                    return airlines;
                }
                set
                {
                    airlines = value;
                }
            }
            internal int AirlinesStatus
            {
                get
                {
                    if (airlines == GlobalNames.sAllocation_All)
                        return 0;
                    String[] lsObjets = airlines.Split(',');
                    bool bPositive = false;
                    foreach (String sObjet in lsObjets)
                    {
                        if (!sObjet.StartsWith("!"))
                        {
                            bPositive = true;
                            break;
                        }
                    }
                    if (bPositive)
                        return 1;
                    return -1;
                }
            }
            internal String FlightIdentification
            {
                get
                {
                    if (flightIdentification == null)
                        return GlobalNames.sAllocation_All;
                    return flightIdentification;
                }
                set
                {
                    flightIdentification = value;
                }
            }
            internal int FlightIdentificationStatus
            {
                get
                {
                    if (flightIdentification == GlobalNames.sAllocation_All)
                        return 0;
                    String[] lsObjets = flightIdentification.Split(',');
                    bool bPositive = false;
                    foreach (String sObjet in lsObjets)
                    {
                        if (!sObjet.StartsWith("!"))
                        {
                            bPositive = true;
                            break;
                        }
                    }
                    if (bPositive)
                        return 1;
                    return -1;
                }
            }
            #endregion

            /// <summary>
            /// Overload method that allow the user to compare 2 instance of \ref GroupAllocation
            /// /!\ It doesn't compare the dates but all the other informations.
            /// </summary>
            /// <param name="ga1"></param>
            /// <param name="ga2"></param>
            /// <returns></returns>
            public static Boolean operator == (GroupAllocation ga1, GroupAllocation ga2)
            {
                if(( ((Object)ga1) == null) && (((Object)ga2)==null))
                    return true;
                if ((((Object)ga1) == null) || (((Object)ga2) == null))
                    return false;
                if (ga1.stationsNumber != ga2.stationsNumber)
                    return false;

                if (ga1.timeType != ga2.timeType)
                    return false;

                if (ga1.passportType != ga2.passportType)
                    return false;

                if (ga1.classType != ga2.classType)
                    return false;
                if (ga1.flightCategory != ga2.flightCategory)
                    return false;
                if (ga1.airlines != ga2.airlines)
                    return false;
                if (ga1.flightIdentification != ga2.flightIdentification)
                    return false;
                if (ga1.userAttributeValuesDictionary.Count != ga2.userAttributeValuesDictionary.Count)
                    return false;
                //<< Task #7405 - new Desk and extra information for Pax                
                foreach (KeyValuePair<string, string> pair in ga1.userAttributeValuesDictionary)
                {
                    if (!ga2.userAttributeValuesDictionary.ContainsKey(pair.Key))
                        return false;
                    else
                        if (ga2.userAttributeValuesDictionary[pair.Key] != pair.Value)
                            return false;
                }
                foreach (KeyValuePair<string, string> pair in ga2.userAttributeValuesDictionary)
                {
                    if (!ga1.userAttributeValuesDictionary.ContainsKey(pair.Key))
                        return false;
                    else
                        if (ga1.userAttributeValuesDictionary[pair.Key] != pair.Value)
                            return false;
                }
                //>> Task #7405 - new Desk and extra information for Pax
                return true;
            }
            public static Boolean operator !=(GroupAllocation ga1, GroupAllocation ga2)
            {
                //<< Task #7405 - new Desk and extra information for Pax                
                return !(ga1 == ga2);
                //>> Task #7405 - new Desk and extra information for Pax
                /*
                if ((ga1 == null) && (ga2 == null))
                    return false;
                if ((ga1 == null) || (ga2 == null))
                    return true;
                if (ga1.stationsNumber != ga2.stationsNumber)
                    return true;

                if (ga1.timeType != ga2.timeType)
                    return true;

                if (ga1.passportType != ga2.passportType)
                    return true;

                if (ga1.classType != ga2.classType)
                    return true;
                if (ga1.flightCategory != ga2.flightCategory)
                    return true;
                if (ga1.airlines != ga2.airlines)
                    return true;
                if (ga1.flightIdentification != ga2.flightIdentification)
                    return true;
                return false;*/
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                if (obj.GetType() != typeof(GroupAllocation))
                    return false;
                return this == (GroupAllocation)obj;
            }
        }
        /*
        public class GroupAllocationComparer : IComparer<GroupAllocation>
        {
            public int Compare(GroupAllocation x, GroupAllocation y)
            {
                return (x.BeginDate - y.BeginDate).Ticks; 
            }
        }
         */
        #endregion

        #region Les boutons Add / Edit / Delete / Delete All
        private void Add_btn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            if (bDateInvalid)
            {
                MessageBox.Show("Wrong values for the dates", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int step;
            int.TryParse(tb_Step.Text, out step);

            string selectedGroup = "";  // >> Task #15781 Various TreeView assistans improvements C#1_1
            if (listBox1.SelectedItem != null)
                selectedGroup = listBox1.SelectedItem.ToString();

            //<< Task #7405 - new Desk and extra information for Pax            
            Assistant.SIM_AllocationElement newElement = new SIMCORE_TOOL.Assistant.SIM_AllocationElement(dt_BeginDate.Value,
                dt_EndDate.Value, step, FlightCategories, AirlineCodes, FPATable, FPDTable, donneesEnCours, resourceTypeStationsByTerminalNb, selectedGroup);
            //>> Task #7405 - new Desk and extra information for Pax
            if (newElement.ShowDialog() == DialogResult.OK)
            {
                GroupAllocation grpAlloc = new GroupAllocation();
                grpAlloc.BeginDate = newElement.BeginDate;
                grpAlloc.EndDate = newElement.EndDate;
                grpAlloc.StationsNumber = newElement.StationsNumber;
                grpAlloc.TimeType = newElement.TimeType;
                grpAlloc.PassportType = newElement.PassportType;
                grpAlloc.FlightCategory = newElement.FlightCategories;
                grpAlloc.ClassType = newElement.ClassType;
                grpAlloc.Airlines = newElement.Airlines;
                grpAlloc.FlightIdentification = newElement.FlightIdentification;
                //<< Task #7405 - new Desk and extra information for Pax
                grpAlloc.userAttributeValuesDictionary = new Dictionary<string, string>(newElement.userAttributeValuesDictionary);
                //>> Task #7405 - new Desk and extra information for Pax

                lesAllocations[listBox1.SelectedIndex].Add(grpAlloc);
                AddAllocation(grpAlloc);
                //AllocationTable.Rows.Add(new Object[] { newElement.BeginDate, newElement.EndDate, newElement.StationsNumber, newElement.TimeType,
                //newElement.PassportType,newElement.FlightCategories, newElement.ClassType, newElement.Airlines, newElement.FlightIdentification });
            }
        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            if (AllocationTable.SelectedRows.Count != 1)
                return;
            if (bDateInvalid)
            {
                MessageBox.Show("Wrong values for the dates", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int step;
            int.TryParse(tb_Step.Text, out step);

            string selectedGroup = "";  // >> Task #15781 Various TreeView assistans improvements C#1_1
            if (listBox1.SelectedItem != null)
                selectedGroup = listBox1.SelectedItem.ToString();
            
            //<< Task #7405 - new Desk and extra information for Pax
            Assistant.SIM_AllocationElement newElement = new SIMCORE_TOOL.Assistant.SIM_AllocationElement(dt_BeginDate.Value,
                dt_EndDate.Value, step, FlightCategories, AirlineCodes, FPATable, FPDTable, donneesEnCours, resourceTypeStationsByTerminalNb, selectedGroup);
            //>> Task #7405 - new Desk and extra information for Pax
            GroupAllocation gaAllocation = lesAllocations[listBox1.SelectedIndex][AllocationTable.SelectedRows[0].Index];
            newElement.BeginDate = gaAllocation.BeginDate;
            newElement.EndDate = gaAllocation.EndDate;
            newElement.StationsNumber = gaAllocation.StationsNumber;
            newElement.TimeType = gaAllocation.TimeType;
            newElement.PassportType = gaAllocation.PassportType;
            newElement.FlightCategories = gaAllocation.FlightCategory;
            newElement.ClassType = gaAllocation.ClassType;
            newElement.Airlines = gaAllocation.Airlines;
            newElement.FlightIdentification = gaAllocation.FlightIdentification;
            //<< Task #7405 - new Desk and extra information for Pax
            newElement.userAttributeValuesDictionary = new Dictionary<string, string>(gaAllocation.userAttributeValuesDictionary);
            //>> Task #7405 - new Desk and extra information for Pax
            if (newElement.ShowDialog() == DialogResult.OK)
            {
                GroupAllocation grpAlloc = new GroupAllocation();
                grpAlloc.BeginDate = newElement.BeginDate;
                grpAlloc.EndDate = newElement.EndDate;
                grpAlloc.StationsNumber = newElement.StationsNumber;
                grpAlloc.TimeType = newElement.TimeType;
                grpAlloc.PassportType = newElement.PassportType;
                grpAlloc.FlightCategory = newElement.FlightCategories;
                grpAlloc.ClassType = newElement.ClassType;
                grpAlloc.Airlines = newElement.Airlines;
                grpAlloc.FlightIdentification = newElement.FlightIdentification;
                //<< Task #7405 - new Desk and extra information for Pax
                grpAlloc.userAttributeValuesDictionary = new Dictionary<string, string>(newElement.userAttributeValuesDictionary);
                //>> Task #7405 - new Desk and extra information for Pax

                lesAllocations[listBox1.SelectedIndex].Add(grpAlloc);
                AddAllocation(grpAlloc);
            }
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1 || AllocationTable.SelectedRows.Count == 0)
                return;
            if (bDateInvalid)
            {
                MessageBox.Show("Wrong values for the dates", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<int> RowToDelete = new List<int>();
            foreach (DataGridViewRow ligne in AllocationTable.SelectedRows)
            {
                if (ligne.Cells[0].Value == null)
                    return;
                else
                    RowToDelete.Add(ligne.Index);
            }
            while (RowToDelete.Count != 0)
            {
                AllocationTable.Rows.RemoveAt(RowToDelete[0]);
                lesAllocations[listBox1.SelectedIndex].RemoveAt(RowToDelete[0]);
                RowToDelete.RemoveAt(0);
            }
            /*
            if(lst_Groups.SelectedIndex ==-1)
                return;
            ArrayList RowToDelete = new ArrayList();
            foreach (DataGridViewRow ligne in dataGridView1.SelectedRows)
            {
                RowToDelete.Add(ligne.Cells["Begin"].Value);
            }
            while (RowToDelete.Count != 0)
            {
                lesAllocations[lst_Groups.SelectedIndex].deleteAt((DateTime)RowToDelete[0]);
                RowToDelete.RemoveAt(0);
            }
            fillTable();*/
        }

        private void DeleteAll_btn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            if (bDateInvalid)
            {
                MessageBox.Show("Wrong values for the dates", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lesAllocations[listBox1.SelectedIndex].Clear();
            AllocationTable.Rows.Clear();
        }
        public void AddAllocation(DateTime dtBegin, DateTime dtEnd)
        {
            //Il faut vérifier le contenu de la table et supprimer tout ce qui n'est pas dans le moule.
            for (int i = 0; i < lesAllocations.Count; i++)
            {
                for (int j = 0; j < lesAllocations[i].Count; j++)
                {
                    GroupAllocation alloc = lesAllocations[i][j];
                    if ((alloc.EndDate <= dtBegin) || (alloc.BeginDate >= dtEnd))
                    {
                        //Il faut supprimer l'entrée.
                        lesAllocations[i].RemoveAt(j);
                        if( i != 0)
                            i--;
                    }
                    else
                    {
                        if (alloc.BeginDate < dtBegin)
                        {
                            alloc.BeginDate = dtBegin;
                        }
                        else if (alloc.EndDate > dtEnd)
                        {
                            alloc.EndDate = dtEnd;
                        }
                    }
                }
            }
        }
        #endregion

        #region Les liste Box et DataGridView
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllocationTable.Rows.Clear();
            bool bEnabled = ( listBox1.SelectedIndex>=0);
            Add_btn.Enabled = bEnabled;
            DeleteAll_btn.Enabled = bEnabled;
            if (listBox1.SelectedIndex == -1)
                return;
            if (bEnabled)
            {
                //<< Task #7405 - new Desk and extra information for Pax                
                fillTable();
                /*
                for (int i = 0; i < lesAllocations[listBox1.SelectedIndex].Count; i++)
                {
                    AllocationTable.Rows.Add(new Object[] { lesAllocations[listBox1.SelectedIndex][i].BeginDate, lesAllocations[listBox1.SelectedIndex][i].EndDate,
                                                        lesAllocations[listBox1.SelectedIndex][i].StationsNumber, lesAllocations[listBox1.SelectedIndex][i].TimeType,
                                                        lesAllocations[listBox1.SelectedIndex][i].PassportType, lesAllocations[listBox1.SelectedIndex][i].ClassType,
                                                        lesAllocations[listBox1.SelectedIndex][i].FlightCategory,lesAllocations[listBox1.SelectedIndex][i].Airlines,
                                                        lesAllocations[listBox1.SelectedIndex][i].FlightIdentification});

                }
                */
                //>> Task #7405 - new Desk and extra information for Pax
            }
        }
        private void AllocationTable_SelectionChanged(object sender, EventArgs e)
        {
            Delete_btn.Enabled = AllocationTable.SelectedRows.Count > 0;
            Edit_btn.Enabled = AllocationTable.SelectedRows.Count == 1;
        }
        #endregion


        private void dt_BeginDate_Leave(object sender, EventArgs e)
        {
            if ((dt_BeginDate.Focused) || (dt_EndDate.Focused) || (tb_Step.Focused))
                return;
            if (Cancel_btn.Focused)
                return;

            if (dt_BeginDate.Value >= dt_EndDate.Value)
            {
                MessageBox.Show("Wrong values for the dates", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bDateInvalid = true;
                return;
            }
            Int32 StepValue;
            if ((!Int32.TryParse(tb_Step.Text, out StepValue)) || (StepValue <= 0))
            {
                MessageBox.Show("Wrong values for the step value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bDateInvalid = true;
                return;
            }

            Int32 minuteDifference = (Int32)OverallTools.DataFunctions.MinuteDifference(dt_BeginDate.Value, dt_EndDate.Value);
            dt_EndDate.Value = dt_EndDate.Value.AddMinutes(-minuteDifference % StepValue);

            AddAllocation(dt_BeginDate.Value, dt_EndDate.Value);
            DateTime dt_i = dt_BeginDate.Value.AddSeconds(-dt_BeginDate.Value.Second);
        }

        #region Bouton OK
        private void Ok_btn_Click(object sender, EventArgs e)
        {
            if (!groupBox1.Enabled)
            {
                MessageBox.Show("The step is not valid ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
        }

        #endregion
        
        public String getAllocation(DateTime Creneau, int GroupeNumber)
        {
            GroupAllocation Aiguille = null;
            //<< Task #7405 - new Desk and extra information for Pax
            int nbOfUserAttribElements = 0;
            String allocationForInterval = "0;N/A;All;All;All;All;All";
            //>> Task #7405 - new Desk and extra information for Pax

            foreach (GroupAllocation alloc in lesAllocations[GroupeNumber])
            {                
                if ((alloc.BeginDate <= Creneau) && (alloc.EndDate >Creneau) )
                {
                    Aiguille = alloc;
                    break;
                }
            }
            //<< Task #7405 - new Desk and extra information for Pax            
            if (userAttributesTable != null)
            {
                nbOfUserAttribElements = userAttributesTable.Table.Rows.Count;
                // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                int nbOfNonUserAttributesElements = 0;
                foreach (DataRow row in userAttributesTable.Table.Rows)
                {
                    if (row[GlobalNames.sUserAttributes_ColumnName] != null
                        && GlobalNames.nonUserAttributesExceptionsList
                                        .Contains(row[GlobalNames.sUserAttributes_ColumnName].ToString()))
                    {
                        nbOfNonUserAttributesElements++;
                    }
                }
                nbOfUserAttribElements = nbOfUserAttribElements - nbOfNonUserAttributesElements;
                // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                for (int i = 0; i < nbOfUserAttribElements; i++)
                    allocationForInterval = allocationForInterval + ";All";
            }
            //>> Task #7405 - new Desk and extra information for Pax

            if (Aiguille == null)
                return allocationForInterval;
            else
            {
                allocationForInterval = Aiguille.StationsNumber.ToString() + ";" +
                                        Aiguille.TimeType.ToString() + ";" +
                                        Aiguille.PassportType.ToString() + ";" +
                                        Aiguille.ClassType.ToString() + ";" +
                                        Aiguille.FlightCategory.ToString() + ";" +
                                        Aiguille.Airlines.ToString() + ";" +
                                        Aiguille.FlightIdentification.ToString();
                foreach (KeyValuePair<string, string> pair in Aiguille.userAttributeValuesDictionary)
                {
                    String value = "All";
                    if (pair.Value != null && pair.Value != "")
                        value = pair.Value;
                    allocationForInterval = allocationForInterval + ";" + value;
                }
            }
            return allocationForInterval;
        }

        private void AddAllocation(GroupAllocation structure)
        {

            //ArrayList contenant les différentes structures qui doivent être modifiées
            List<GroupAllocation> al_dtARemodele = new List<GroupAllocation>();
            GroupAllocation alloc;
            for (int i = 0; i < lesAllocations[listBox1.SelectedIndex].Count; i++)
            {
                alloc = (GroupAllocation)lesAllocations[listBox1.SelectedIndex][i];

                if (((alloc.BeginDate < structure.BeginDate) && (alloc.EndDate > structure.BeginDate)) ||
                   ((alloc.EndDate > structure.EndDate) && (alloc.BeginDate <= structure.EndDate)) ||
                   ((alloc.BeginDate >= structure.BeginDate) && (alloc.EndDate <= structure.EndDate)))
                {
                    al_dtARemodele.Add(lesAllocations[listBox1.SelectedIndex][i]);
                    lesAllocations[listBox1.SelectedIndex].RemoveAt(i);
                    i--;
                }
            }
            lesAllocations[listBox1.SelectedIndex].Add(structure);
            if (al_dtARemodele.Count == 0)
            {
                sortTable();
                fillTable();
                return;
            }

            while (al_dtARemodele.Count != 0)
            {
                int iIndex = indexDateMinimum(al_dtARemodele);
                alloc = (GroupAllocation)al_dtARemodele[iIndex];
                al_dtARemodele.RemoveAt(iIndex);
                if ((structure.BeginDate <= alloc.BeginDate) && (structure.EndDate >= alloc.EndDate))
                {
                    continue;
                }
                else if ((structure.BeginDate > alloc.BeginDate) && (structure.EndDate < alloc.EndDate))
                {
                    //il faut ajouter Deux nouvelles allocations.
                    //<< Task #7405 - new Desk and extra information for Pax                    
                    lesAllocations[listBox1.SelectedIndex].Add(new GroupAllocation(alloc.BeginDate, structure.BeginDate,alloc.StationsNumber,alloc.TimeType,alloc.PassportType, alloc.ClassType, alloc.FlightCategory,alloc.Airlines, alloc.FlightIdentification, alloc.userAttributeValuesDictionary));
                    lesAllocations[listBox1.SelectedIndex].Add(new GroupAllocation(structure.EndDate, alloc.EndDate, alloc.StationsNumber, alloc.TimeType, alloc.PassportType, alloc.ClassType, alloc.FlightCategory, alloc.Airlines, alloc.FlightIdentification, alloc.userAttributeValuesDictionary));
                    //>> Task #7405 - new Desk and extra information for Pax
                }
                else if (structure.BeginDate > alloc.BeginDate)
                {
                    //le début de cette nouvelle ligne doit être modifié.
                    alloc.EndDate = structure.BeginDate;
                    lesAllocations[listBox1.SelectedIndex].Add(alloc);
                }
                else if (structure.EndDate < alloc.EndDate)
                {
                    alloc.BeginDate = structure.EndDate;
                    lesAllocations[listBox1.SelectedIndex].Add(alloc);
                }
            }
            sortTable();
            fillTable();
        }

        private int indexDateMinimum(List<GroupAllocation> alListe)
        {
            DateTime min = ((GroupAllocation)alListe[0]).BeginDate;
            int iIndex = 0;
            for (int i = 1; i < alListe.Count; i++)
            {
                if (min > ((GroupAllocation)alListe[i]).BeginDate)
                {
                    min = ((GroupAllocation)alListe[i]).BeginDate;
                    iIndex = i;
                }
            }
            return iIndex;
        }

        #region Gestion du tri dela liste
        private void sortTable()
        {
            for (int i = 0; i < lesAllocations[listBox1.SelectedIndex].Count; i++)
            {
                if (i != lesAllocations[listBox1.SelectedIndex].Count-1)
                {
                    if (lesAllocations[listBox1.SelectedIndex][i].BeginDate > lesAllocations[listBox1.SelectedIndex][i + 1].BeginDate)
                    {
                        GroupAllocation tmpAlloc = lesAllocations[listBox1.SelectedIndex][i];
                        lesAllocations[listBox1.SelectedIndex][i] = lesAllocations[listBox1.SelectedIndex][i + 1];
                        lesAllocations[listBox1.SelectedIndex][i + 1] = tmpAlloc;
                        i = 0;
                    }
                }
            }
        }
        #endregion

        public void fillTable()
        {
            AllocationTable.Rows.Clear();

            foreach (GroupAllocation alloc in lesAllocations[listBox1.SelectedIndex])
            {
                //<< Task #7405 - new Desk and extra information for Pax                
                //AllocationTable.Rows.Add(new Object[] { alloc.BeginDate , alloc.EndDate, alloc.StationsNumber, alloc.TimeType, alloc.PassportType, alloc.ClassType, alloc.FlightCategory, alloc.Airlines, alloc.FlightIdentification});
                Object[] newObj = new Object[AllocationTable.Columns.Count];
                newObj[0] = alloc.BeginDate;
                newObj[1] = alloc.EndDate;
                newObj[2] = alloc.StationsNumber;
                newObj[3] = alloc.TimeType;
                newObj[4] = alloc.PassportType;
                newObj[5] = alloc.ClassType;
                newObj[6] = alloc.FlightCategory;
                newObj[7] = alloc.Airlines;
                newObj[8] = alloc.FlightIdentification;
                int nbOfExtraColumns = -1;
                int index = 8;
                if (this.userAttributesTable != null)
                    nbOfExtraColumns = this.userAttributesTable.Table.Rows.Count;
                // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                int nbOfNonUserAttributesColumns = 0;
                if (this.userAttributesTable != null)
                {
                    foreach (DataRow rw in this.userAttributesTable.Table.Rows)
                    {
                        if (rw[GlobalNames.sUserAttributes_ColumnName] != null &&
                            GlobalNames.nonUserAttributesExceptionsList
                                        .Contains(rw[GlobalNames.sUserAttributes_ColumnName].ToString()))
                        {
                            nbOfNonUserAttributesColumns++;
                        }
                    }
                    nbOfExtraColumns = nbOfExtraColumns - nbOfNonUserAttributesColumns;
                }
                // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                if (nbOfExtraColumns > 0)
                {
                    foreach (DataRow dr in this.userAttributesTable.Table.Rows)
                    {
                        
                        String distribTableName = dr[GlobalNames.sUserAttributes_ColumnName].ToString();
                        // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant                        
                        if (GlobalNames.nonUserAttributesExceptionsList.Contains(distribTableName))
                            continue;
                        // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                        String value = "";
                        index++;
                        alloc.userAttributeValuesDictionary.TryGetValue(distribTableName, out value);
                        newObj[index] = value;
                    }
                }
                AllocationTable.Rows.Add(newObj);
                //>> Task #7405 - new Desk and extra information for Pax
            }
        }

        private void tb_Step_TextChanged(object sender, EventArgs e)
        {
            if (FonctionsType.getInt(tb_Step.Text) <= 0)
                tb_Step.BackColor = Color.Red;
            else
                tb_Step.BackColor = Color.White;
            groupBox1.Enabled = (tb_Step.BackColor == Color.White);
        }

        private void AllocationAssistantManager_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!groupBox1.Enabled)
            {
                if (MessageBox.Show("Are you sure that you want to exit the form. (The unsaved data will be lost) ?", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                return;
            }
            DialogResult res = MessageBox.Show("Would you like to save your change ?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            switch (res)
            {
                case DialogResult.Yes:
                    AllocTable.Rows.Clear();
                    DateTime dt_i = dt_BeginDate.Value;
                    Int32 StepValue;
                    if (!Int32.TryParse(tb_Step.Text, out StepValue))
                    {
                        MessageBox.Show("Wrong values for the step value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }
                    while (dt_i <= dt_EndDate.Value)
                    {
                        DataRow ligne = AllocTable.NewRow();
                        ligne[0] = dt_i;
                        for (int i = 0; i < lesAllocations.Count; i++)
                        {
                            ligne[i + 1] = getAllocation(dt_i, i);
                        }
                        AllocTable.Rows.Add(ligne);
                        dt_i = dt_i.AddMinutes(StepValue);
                    }
                    AllocTable.AcceptChanges();
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
