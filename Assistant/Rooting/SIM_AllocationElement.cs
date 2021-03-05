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
    public partial class SIM_AllocationElement : Form
    {
        private DateTime endDate;
        private String timeType;
        private String passportType;
        private String classType;
        private List<String> flightCategories;
        private List<String> airlines;
        private List<String> flightIdentification;
        //dictionary string + list<string>
        //dictionary string + string = userValue + selected value for userValue (setter)
        //<< Task #7405 - new Desk and extra information for Pax
        GestionDonneesHUB2SIM donneesEnCours;
        DataManagement.NormalTable userAttributesTable;
        Dictionary<String, DataManagement.NormalTable> distributionForUserAttribTables;
        //used to send back the selected values by distribution table to the Allocation Assistant Manager
        public Dictionary<string, string> userAttributeValuesDictionary = new Dictionary<string, string>();
        //used to send to the selection editor the list of all values related to the specific distribution table 
        Dictionary<string, List<string>> allValuesByDistributionTable = new Dictionary<string, List<string>>();
        //>> Task #7405 - new Desk and extra information for Pax

        Dictionary<string, List<string>> allValuesForPaxGroupsUserAttributes = new Dictionary<string, List<string>>(); // >> Task #10764 Pax2Sim - new User attributes for Groups
        

        #region setters and getters
        internal DateTime BeginDate
        {
            get
            {
                return FonctionsType.getDate(cb_beginDate.Text);
            }
            set
            {
                cb_beginDate.Text = value.ToString();
            }
        }
        internal DateTime EndDate
        {
            get
            {
                return FonctionsType.getDate(cb_endDate.Text);
            }
            set
            {
                cb_endDate.Text = value.ToString();
            }
        }
        internal int StationsNumber
        {
            get

            {
                return FonctionsType.getInt(cb_StationsNumber.Text);
            }
            set
            {
                cb_StationsNumber.Text = value.ToString();
            }
        }
        internal String TimeType
        {
            get
            {
                if (cb_Time.Text == cb_Time.Items[0].ToString())
                    return cb_Time.Text;
                return tb_TimeValue.Text;
            }
            set
            {
                if (value == cb_Time.Items[0].ToString())
                    cb_Time.SelectedIndex = 0;
                else
                {
                    cb_Time.SelectedIndex = 1;
                    tb_TimeValue.Text = value;
                }
            }
        }
        internal String PassportType
        {
            get
            {
                return passportType;
            }
            set
            {

                if (value == "All")
                    radioButton1.Checked = true;
                else if (value == "Local")
                    radioButton2.Checked = true;
                else
                    radioButton3.Checked = true;
            }
        }
        internal String ClassType
        {
            get
            {
                return classType;
            }
            set
            {
                if (value == "All")
                    radioButton4.Checked = true;
                else if (value == "F&B")
                    radioButton6.Checked = true;
                else
                    radioButton5.Checked = true;
            }
        }
        internal String FlightCategories
        {
            get
            {
                return tb_FlightCategories.Text;
            }
            set
            {
                tb_FlightCategories.Text = value;
            }
        }
        internal String Airlines
        {
            get
            {
                return tb_Airlines.Text;
            }
            set
            {
                tb_Airlines.Text = value;
            }
        }
        internal String FlightIdentification
        {
            get
            {
                return tb_FlightId.Text;
            }
            set
            {
                tb_FlightId.Text = value;
            }
        }
        #endregion

        #region Constructeurs
        public SIM_AllocationElement(DateTime BeginDate, DateTime EndDate, int Step, List<String> FlightCategories_, List<String> AirlineCodes_, List<String> FPATable_,
            List<String> FPDTable_, GestionDonneesHUB2SIM pDonneesEnCours, Dictionary<string, string[]> resourceTypeStationsByTerminalNb, string selectedGroup)  // >> Task #15781 Various TreeView assistans improvements C#1_1
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            cb_StationsNumber.SelectedIndex = 0;
            StationsNumber = 0;
            cb_Time.SelectedIndex = 0;
            //<< Task #7405 - new Desk and extra information for Pax            
            cb_Time.Items.Clear();
            cb_Time.Items.AddRange(new object[] { GlobalNames.sAllocation_NotApplicable, "Select"});
            //>> Task #7405 - new Desk and extra information for Pax
            TimeType = GlobalNames.sAllocation_NotApplicable;
            tb_TimeValue.Visible = false;
            flightCategories = FlightCategories_;
            airlines = AirlineCodes_;
            flightIdentification = FPATable_;
            flightIdentification = new List<string>();
            flightIdentification.AddRange(FPATable_);
            flightIdentification.AddRange(FPDTable_);

            DateTime dt_i = BeginDate.AddSeconds(-BeginDate.Second);
            while (dt_i <= EndDate.AddMinutes(1))
            {
                cb_beginDate.Items.Add(dt_i.ToString());
                cb_endDate.Items.Add(dt_i.ToString());
                dt_i = dt_i.AddMinutes(Step);
            }
            cb_endDate.Items.RemoveAt(cb_endDate.Items.Count - 1);

            cb_beginDate.SelectedIndex = 0;
            cb_endDate.SelectedIndex = cb_endDate.Items.Count - 1;
            //<< Task #7405 - new Desk and extra information for Pax
            this.donneesEnCours = pDonneesEnCours;
            this.userAttributesTable = donneesEnCours.GetTable("Input", GlobalNames.sUserAttributesTableName);
            this.distributionForUserAttribTables = new Dictionary<string,SIMCORE_TOOL.DataManagement.NormalTable>();
            if (this.userAttributesTable != null)
            {
                cb_userAttributes.Items.Clear();
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
                    cb_userAttributes.Items.Add(distribTableName);
                    // if we are in Add mode the dictionary will not contain anything => 
                    // we fill the dictionary with all the user attributes values set to All (the default value)
                    if (!userAttributeValuesDictionary.ContainsKey(distribTableName))
                        userAttributeValuesDictionary.Add(distribTableName, "All");
                }
                setAllValuesFromDistributionTables();
                addAllValuesForPaxGroupsUserAttributes();   // >> Task #10764 Pax2Sim - new User attributes for Groups                
            }
            //>> Task #7405 - new Desk and extra information for Pax
            setUpStationsNumberCombobox(resourceTypeStationsByTerminalNb, selectedGroup);   // >> Task #15781 Various TreeView assistans improvements C#1_1
        }
        
        private void setUpStationsNumberCombobox(Dictionary<string, string[]> resourceTypeStationsByTerminalNb, string selectedGroup)
        {
            int nbStations = 0;

            int groupTerminalNb = -1;
            if (!Int32.TryParse(selectedGroup.Substring(1, 1), out groupTerminalNb))
                return;

            foreach (KeyValuePair<string, string[]> terminalStationsPair in resourceTypeStationsByTerminalNb)
            {
                string terminalName = terminalStationsPair.Key;
                int terminalNb = -1;
                if (donneesEnCours.UseAlphaNumericForFlightInfo)
                {
                    int start = terminalName.IndexOf("(") + 1;
                    int end = terminalName.IndexOf(":");
                    if (start == -1 || end == -1 || start >= terminalName.Length
                        || !Int32.TryParse(terminalName.Substring(start, end - start), out terminalNb))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!Int32.TryParse(terminalName.Substring(0, 1), out terminalNb))
                        continue;
                }
                if (terminalNb == groupTerminalNb)
                {
                    nbStations = terminalStationsPair.Value.Length;
                    break;
                }
            }
            cb_StationsNumber.Items.Clear();
            for (int i = 0; i <= nbStations; i++)
                cb_StationsNumber.Items.Add(i);
        }


        //<< Task #7405 - new Desk and extra information for Pax
        private void setAllValuesFromDistributionTables()
        {
            foreach (KeyValuePair<string, DataManagement.NormalTable> pair in distributionForUserAttribTables)
            {
                String distributionTableName = pair.Key;
                DataManagement.NormalTable distributionTable = pair.Value;
                DataManagement.ExceptionTable exceptionTable = null;
                List<string> distributionTableValues = new List<string>();
                if (distributionTable is DataManagement.ExceptionTable)
                    exceptionTable = (DataManagement.ExceptionTable)distributionTable;
                if (distributionTableName.Equals(GlobalNames.flightSubcategoriesTableName)) // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
                {
                    foreach (DataRow dr in distributionTable.Table.Rows)
                    {
                        String value = dr[GlobalNames.flightSubcategoryColumnName].ToString();
                        distributionTableValues.Add(value);
                    }
                    allValuesByDistributionTable.Add(distributionTableName, distributionTableValues);
                }
                else if (exceptionTable != null)
                {
                    addExceptionValuesToDictionary(exceptionTable, distributionTableValues);
                    addExceptionValuesToDictionary(exceptionTable.ExceptionAirlineFB, distributionTableValues);
                    addExceptionValuesToDictionary(exceptionTable.ExceptionAirline, distributionTableValues);
                    addExceptionValuesToDictionary(exceptionTable.ExceptionFB, distributionTableValues);
                    addExceptionValuesToDictionary(exceptionTable.ExceptionFC, distributionTableValues);
                    addExceptionValuesToDictionary(exceptionTable.ExceptionFCFB, distributionTableValues);
                    addExceptionValuesToDictionary(exceptionTable.ExceptionFlight, distributionTableValues);
                    addExceptionValuesToDictionary(exceptionTable.ExceptionFlightFB, distributionTableValues);

                    allValuesByDistributionTable.Add(distributionTableName, distributionTableValues);
                }
                else
                {
                    foreach (DataRow dr in distributionTable.Table.Rows)
                    {
                        String value = dr[GlobalNames.sUserAttributes_DistributionTableColumnName_Value].ToString();
                        distributionTableValues.Add(value);
                    }
                    allValuesByDistributionTable.Add(distributionTableName, distributionTableValues);
                }
            }
        }
        private void addExceptionValuesToDictionary(DataManagement.NormalTable table, List<string> distributionTableValues)
        {
            if (table != null)
                {
                    foreach (DataRow dr in table.Table.Rows)
                    {
                        String value = dr[GlobalNames.sUserAttributes_DistributionTableColumnName_Value].ToString();
                        if (!distributionTableValues.Contains(value))
                            distributionTableValues.Add(value);
                    }                    
                }
        }
        //>> Task #7405 - new Desk and extra information for Pax

        // >> Task #10764 Pax2Sim - new User attributes for Groups
        private void addAllValuesForPaxGroupsUserAttributes()
        {
            List<String> paxGroupList = donneesEnCours.getAllTerminalNbGroupNbPairsByGroupType(GlobalNames.CHECK_IN_GROUP_TYPE);
            if (allValuesByDistributionTable.ContainsKey(GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE))
            {
                allValuesByDistributionTable[GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE] = paxGroupList;
            }
            else
            {
                allValuesByDistributionTable.Add(GlobalNames.PAX_CI_GROUP_USER_ATTRIBUTE, paxGroupList);
            }
           
            paxGroupList = donneesEnCours.getAllTerminalNbGroupNbPairsByGroupType(GlobalNames.RECLAIM_GROUP_TYPE);
            if (allValuesByDistributionTable.ContainsKey(GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE))
            {
                allValuesByDistributionTable[GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE] = paxGroupList;
            }
            else
            {
                allValuesByDistributionTable.Add(GlobalNames.PAX_RECLAIM_GROUP_USER_ATTRIBUTE, paxGroupList);
            }

            paxGroupList = donneesEnCours.getAllTerminalNbGroupNbPairsByGroupType(GlobalNames.TRANSFER_GROUP_TYPE);
            if (allValuesByDistributionTable.ContainsKey(GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE))
            {
                allValuesByDistributionTable[GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE] = paxGroupList;
            }
            else
            {
                allValuesByDistributionTable.Add(GlobalNames.PAX_TRANSFER_GROUP_USER_ATTRIBUTE, paxGroupList);
            }

            paxGroupList = donneesEnCours.getAllTerminalNbGroupNbPairsByGroupType(GlobalNames.BOARDING_GATE_GROUP_TYPE);
            if (allValuesByDistributionTable.ContainsKey(GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE))
            {
                allValuesByDistributionTable[GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE] = paxGroupList;
            }
            else
            {
                allValuesByDistributionTable.Add(GlobalNames.PAX_BOARDING_GATE_GROUP_USER_ATTRIBUTE, paxGroupList);
            }
        }
        // << Task #10764 Pax2Sim - new User attributes for Groups
        #endregion

        #region Others
        private void FlightParam_btn_Click(object sender, EventArgs e)
        {
            Assistant.SIM_Flight_Parameters flightParam;
            if(((Button)sender).Name == "FlightCategories_btn")
            {    
                flightParam = new SIM_Flight_Parameters("FC", ToArrayList(tb_FlightCategories.Text), flightCategories);
                flightParam.Text = "Flight Categories";
                if (flightParam.ShowDialog() == DialogResult.OK)
                    tb_FlightCategories.Text = ToString(flightParam.VisibleList);
            }
            else if (((Button)sender).Name == "Airlines_btn")
            {
                flightParam = new SIM_Flight_Parameters("A", ToArrayList(tb_Airlines.Text), airlines);
                flightParam.Text = "Airlines";
                if (flightParam.ShowDialog() == DialogResult.OK)
                    tb_Airlines.Text = ToString(flightParam.VisibleList);
            }
            //<< Task #7405 - new Desk and extra information for Pax            
            else if (((Button)sender).Name == "userAttributeValues_btn")
            {
                String userAttribute = cb_userAttributes.Text;
                if (userAttribute != null && userAttribute != "")
                {
                    List<string> allValues = new List<string>();
                    if (allValuesByDistributionTable.TryGetValue(userAttribute, out allValues))
                    {
                        flightParam = new SIM_Flight_Parameters("UAV", ToArrayList(tb_userAttributesValue.Text), allValues);
                        flightParam.Text = "User Attributes Values";
                        if (flightParam.ShowDialog() == DialogResult.OK)
                        {
                            String valueReturned = ToString(flightParam.VisibleList);
                            if (valueReturned == "")
                                valueReturned = "All";
                            tb_userAttributesValue.Text = valueReturned;
                            if (userAttributeValuesDictionary.ContainsKey(userAttribute))
                            {
                                userAttributeValuesDictionary.Remove(userAttribute);
                            }
                            userAttributeValuesDictionary.Add(userAttribute, valueReturned);
                        }
                    }
                    else if (allValuesForPaxGroupsUserAttributes.TryGetValue(userAttribute, out allValues))
                    {
                        // >> Task #10764 Pax2Sim - new User attributes for Groups
                        flightParam = new SIM_Flight_Parameters("UAV", ToArrayList(tb_userAttributesValue.Text), allValues);
                        flightParam.Text = "User Attributes Values";
                        if (flightParam.ShowDialog() == DialogResult.OK)
                        {
                            String valueReturned = ToString(flightParam.VisibleList);
                            if (valueReturned == "")
                                valueReturned = "All";
                            tb_userAttributesValue.Text = valueReturned;
                            //userAttributeValuesDictionary.Remove(userAttribute);
                            //userAttributeValuesDictionary.Add(userAttribute, valueReturned);
                        }
                        // << Task #10764 Pax2Sim - new User attributes for Groups
                    }                    
                }
            }
            //>> Task #7405 - new Desk and extra information for Pax
            else
            {
                flightParam = new SIM_Flight_Parameters("FP", ToArrayList(tb_FlightId.Text), flightIdentification);
                flightParam.Text = "Flight Identification";
                if (flightParam.ShowDialog() == DialogResult.OK)
                    tb_FlightId.Text = ToString(flightParam.VisibleList);
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
                return;
            if (((TextBox)sender).Text == "")
                ((TextBox)sender).Text = "All";
        }
        internal String[] ToArrayList(String chaine)
        {
            if (chaine == "All")
                return new String[0];
            return  chaine.Split(',');
        }

        internal String ToString(List<String> chaine)
        {
            if (chaine.Count == 0)
                return "";
            String tmpSrg = "";
            for (int i = 0; i < chaine.Count; i++)
            {
                tmpSrg += chaine[i];
                if(i != chaine.Count-1)
                    tmpSrg += ",";
            }
            return tmpSrg;
        }
        #endregion

        #region Dates
        private void cb_Time_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Time.SelectedIndex == 1)
            {
                tb_TimeValue.Visible = true;
                tb_TimeValue.Focus();
            }
            else
            {
                tb_TimeValue.Visible = false;
                tb_TimeValue.BackColor = Color.White;
            }
        }

        private void tb_TimeValue_TextChanged(object sender, EventArgs e)
        {
            double timeValue;
            if (double.TryParse(tb_TimeValue.Text, out timeValue))
            {
                tb_TimeValue.BackColor = Color.White;
                timeType = tb_TimeValue.Text;
            }
            else
                tb_TimeValue.BackColor = Color.Red;
        }

        private void cb_beginDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //beginDate = FonctionsType.getDate(cb_beginDate.Text, typeof(String));
            String sSelected = cb_endDate.Text;
            cb_endDate.Items.Clear();
            cb_endDate.Text = "";
            bool bBegin = false;
            bool bPresent = false;

            for (int i = 0; i < cb_beginDate.Items.Count; i++)
            {
                if (cb_beginDate.Items[i].ToString() == cb_beginDate.Text)
                {
                    bBegin = true;
                    continue;
                }
                if (!bBegin)
                    continue;
                cb_endDate.Items.Add(cb_beginDate.Items[i].ToString());
                if(sSelected == cb_beginDate.Items[i].ToString())
                {
                    bPresent = true;
                }
            }
            if (bPresent)
                cb_endDate.Text = sSelected;
            else
                cb_endDate.Text = cb_endDate.Items[0].ToString();
        }

        private void cb_endDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            endDate = FonctionsType.getDate(cb_endDate.Text, typeof(String));
        }
        #endregion

        #region Station Number
        private void cb_StationsNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*int value;
            int.TryParse(cb_StationsNumber.Text, out value);
            stationsNumber = value;*/
        }
        #endregion
        
        private void SIM_AllocationElement_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to save the changes ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (res == System.Windows.Forms.DialogResult.No)
                return;
            if (res == System.Windows.Forms.DialogResult.Cancel)
            {
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            if (tb_FlightCategories.Text == "" || tb_Airlines.Text == "" || tb_FlightId.Text == "")
            {
                MessageBox.Show("Some Fields are not properly filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
            else if (tb_TimeValue.BackColor == Color.Red)
            {
                MessageBox.Show("Wrong time value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
            if (radioButton1.Checked)
                passportType = "All";
            else if (radioButton2.Checked)
                passportType = "Local";
            else
                passportType = "Not Local";

            if (radioButton4.Checked)
                classType = "All";
            else if (radioButton6.Checked)
                classType = "F&B";
            else
                classType = "Eco";
        }

        //<< Task #7405 - new Desk and extra information for Pax        
        private void cb_userAttributesSelectedIndexChange(object sender, EventArgs e)
        {
            //by default the user attribute is set to All
            tb_userAttributesValue.Text = "All";
            String userAttribute = ((ComboBox)sender).SelectedItem.ToString();
            if (userAttribute != null && userAttribute != "")
            {
                foreach (KeyValuePair<string, string> pair in userAttributeValuesDictionary)
                {
                    if (pair.Key == userAttribute)
                    {
                        if (pair.Value != "")
                            tb_userAttributesValue.Text = pair.Value;                        
                    }                    
                }
            }
        }
        //>> Task #7405 - new Desk and extra information for Pax
    }
}
