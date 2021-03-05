using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.com.crispico.inputs.flight_group_rules;
using SIMCORE_TOOL.com.crispico.FlightPlansUtils;

namespace SIMCORE_TOOL.com.crispico.inputs.deterministic_transfer_distribution
{
    public partial class DeterministicTransferDistributionEditor : Form
    {
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_TECHNICAL_NAME = "Arrival_Transfer_Distribution_Deterministic";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_DISPLAYED_NAME = "Arrival Transfer Distribution Deterministic";

        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_PLAN_NAME = "Flight Plan Name";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_ID = "Flight ID";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_COMPLETE_DATE = "Complete Date";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_NB = "Flight Nb";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_CATEGORY = "Flight Category";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRLINE = "Airline";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRPORT = "Airport";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_1 = "User 1";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_2 = "User 2";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_3 = "User 3";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_4 = "User 4";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_5 = "User 5";
        public const string DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_TRANSFER_BAGS_DISTRIBUTION = "Transfer Bags Distribution";

        public static List<DeterministicTransferDistributionParameter> getParametersFromGivenTableByFlightPlan(DataTable flightPlan,
            DataTable deterministicTransferDistributionTable, DataTable flightGroupRulesTable)
        {
            List<DeterministicTransferDistributionParameter> parameters = new List<DeterministicTransferDistributionParameter>();
            if (deterministicTransferDistributionTable == null || flightGroupRulesTable == null)
                return parameters;

            List<FlightGroupRulesParameter> flightGroupRules = FlightGroupRulesEditor.getParametersFromGivenTable(flightGroupRulesTable);
            List<FlightAttribute> flights = retrieveFlightsByFlightPlan(flightPlan);

            #region column indexes
            int columnIndexFlightPlan = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_PLAN_NAME);
            int columnIndexFlightID = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_ID);
            int columnIndexFlightCompleteDate = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_COMPLETE_DATE);
            int columnIndexFlightNb = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_NB);
            int columnIndexFlightCategory = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_CATEGORY);
            int columnIndexAirline = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRLINE);
            int columnIndexAirport = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRPORT);
            int columnIndexUser1 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_1);
            int columnIndexUser2 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_2);
            int columnIndexUser3 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_3);
            int columnIndexUser4 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_4);
            int columnIndexUser5 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_5);
            int columnIndexTransferBagsDistribution = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_TRANSFER_BAGS_DISTRIBUTION);

            List<int> indexes = new List<int>(new int[] { columnIndexFlightPlan, columnIndexFlightID, columnIndexFlightCompleteDate, columnIndexFlightNb, columnIndexFlightCategory,
                                                            columnIndexAirline,columnIndexAirport, columnIndexUser1, columnIndexUser2, columnIndexUser3,columnIndexUser4, 
                                                            columnIndexUser5, columnIndexTransferBagsDistribution  });
            foreach (int index in indexes)
            {
                if (index == -1)
                    return parameters;
            }
            #endregion

            foreach (DataRow row in deterministicTransferDistributionTable.Rows)
            {
                DeterministicTransferDistributionParameter parameter = new DeterministicTransferDistributionParameter();
                
                if (row[columnIndexFlightPlan] == null || row[columnIndexFlightPlan].ToString() != flightPlan.TableName)
                    continue;
                
                int id = -1;
                if (row[columnIndexFlightID] == null || !Int32.TryParse(row[columnIndexFlightID].ToString(), out id))
                    continue;

                parameter.flightPlanTableName = row[columnIndexFlightPlan].ToString();

                FlightAttribute flight = null;
                foreach (FlightAttribute f in flights)
                {
                    if (f.flightId == id)
                    {
                        flight = f;
                        break;
                    }
                }
                if (flight == null)
                    continue;
                parameter.flight = flight;

                if (row[columnIndexTransferBagsDistribution] != null)
                {
                    parameter.deterministicValues = getDeterministicValuesByDeterministicTransferTableCellContent(row[columnIndexTransferBagsDistribution].ToString());
                    foreach (RuleAndDeterministicValues ruleAndValues in parameter.deterministicValues)
                    {
                        foreach (FlightGroupRulesParameter givenRuleParam in flightGroupRules)
                        {
                            if (givenRuleParam.ruleName == ruleAndValues.rule.ruleName)
                            {
                                ruleAndValues.rule = givenRuleParam;
                                break;
                            }
                        }
                    }
                }
                parameters.Add(parameter);
            }
            return parameters;
        }

        /// <summary>
        /// k = flight plan name
        /// v = the list of the flight plan's flights
        /// </summary>
        private Dictionary<string, List<FlightAttribute>> flightsByFlightPlan = new Dictionary<string, List<FlightAttribute>>();

        private DataTable deterministicTransferDistributionTable;

        private int editedRowIndex = -1;

        /// <summary>
        /// kept per editor session => we save into the table only when the OK button is clicked.
        /// k = Flight plan name
        /// v = list of DeterministicTransferDistributionParameter -> list of pairs { flight - list of pairs { deterministic value - rule } }
        /// </summary>
        private Dictionary<string, List<DeterministicTransferDistributionParameter>> sessionParametersByFlightPlan
            = new Dictionary<string, List<DeterministicTransferDistributionParameter>>();

        public DeterministicTransferDistributionEditor(List<DataTable> arrivalFlightPlans, DataTable flighGroupRulesTable, DataTable deterministicTransferDistributionTable)
        {
            initUI();

            this.deterministicTransferDistributionTable = deterministicTransferDistributionTable;

            List<FlightGroupRulesParameter> flightGroupRulesParameters = retrieveFlightGroupRulesParameters(flighGroupRulesTable);
            addRulesGroupBoxes(rulesPanel, flightGroupRulesParameters);

            flightsByFlightPlan = retrieveFlightsByFlightPlan(arrivalFlightPlans);            

            // list with the parameters obtained in the constructor by parsing the Deterministic Transfer Distribution table
            List<DeterministicTransferDistributionParameter> initialParameters 
                = getParametersFromDeterministicTransferDistributionTable(deterministicTransferDistributionTable);
            initializeSessionParameters(initialParameters);

            initFlightPlanComboBox();
        }

        public DeterministicTransferDistributionEditor(List<DataTable> arrivalFlightPlans, DataTable flighGroupRulesTable, DataTable deterministicTransferDistributionTable,
            int editedRowIndex)
        {
            initUI();

            this.editedRowIndex = editedRowIndex;
            this.deterministicTransferDistributionTable = deterministicTransferDistributionTable;

            List<FlightGroupRulesParameter> flightGroupRulesParameters = retrieveFlightGroupRulesParameters(flighGroupRulesTable);
            addRulesGroupBoxes(rulesPanel, flightGroupRulesParameters);

            flightsByFlightPlan = retrieveFlightsByFlightPlan(arrivalFlightPlans);

            List<DeterministicTransferDistributionParameter> initialParameters
                = getParametersFromDeterministicTransferDistributionTable(deterministicTransferDistributionTable);
            initializeSessionParameters(initialParameters);

            initFlightPlanComboBox();

            string selectedFlightPlanName = getFlightPlanNameByDeterministicTableRowIndex(editedRowIndex, deterministicTransferDistributionTable);
            int selectedFlightId = getFlightIdByDeterministicTableRowIndex(editedRowIndex, deterministicTransferDistributionTable);
            
            selecFlightPlan(selectedFlightPlanName);
            selectFlight(selectedFlightId);
        }
                
        private void initUI()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        private string getFlightPlanNameByDeterministicTableRowIndex(int editedRowIndex, DataTable deterministicTransferDistributionTable)
        {
            if (deterministicTransferDistributionTable == null || editedRowIndex >= deterministicTransferDistributionTable.Rows.Count || editedRowIndex < 0)
                return string.Empty;
            DataRow row = deterministicTransferDistributionTable.Rows[editedRowIndex];
            int indexColumnFlightPlanName = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_PLAN_NAME);
            if (indexColumnFlightPlanName == -1 || row[indexColumnFlightPlanName] == null)
                return string.Empty;
            return row[indexColumnFlightPlanName].ToString();
        }

        private int getFlightIdByDeterministicTableRowIndex(int editedRowIndex, DataTable deterministicTransferDistributionTable)
        {
            if (deterministicTransferDistributionTable == null || editedRowIndex >= deterministicTransferDistributionTable.Rows.Count || editedRowIndex < 0)
                return Int32.MinValue;
            DataRow row = deterministicTransferDistributionTable.Rows[editedRowIndex];
            int indexColumnFlightId = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_ID);
            int id = -1;
            if (indexColumnFlightId == -1 || row[indexColumnFlightId] == null || !Int32.TryParse(row[indexColumnFlightId].ToString(), out id))
                return Int32.MinValue;
            return id;
        }

        private void selecFlightPlan(string selectedFlightPlanName)
        {
            if (selectedFlightPlanName == null)
                return;
            if (flightPlanComboBox.Items.Contains(selectedFlightPlanName))
                flightPlanComboBox.SelectedIndex = flightPlanComboBox.Items.IndexOf(selectedFlightPlanName);
        }

        private void selectFlight(int selectedFlightId)
        {
            foreach (object item in flightIdComboBox.Items)
            {
                int itemAsInt = -1;
                if (item == null || !Int32.TryParse(item.ToString(), out itemAsInt))
                    continue;
                if (itemAsInt == selectedFlightId)
                {
                    flightIdComboBox.SelectedIndex = flightIdComboBox.Items.IndexOf(item);
                    return;
                }
            }
        }

        private List<FlightGroupRulesParameter> retrieveFlightGroupRulesParameters(DataTable flighGroupRulesTable)
        {
            return FlightGroupRulesEditor.getParametersFromGivenTable(flighGroupRulesTable);
            /*
            List<FlightGroupRulesParameter> result = new List<FlightGroupRulesParameter>();
            if (flighGroupRulesTable == null)
                return result;
            foreach (DataRow row in flighGroupRulesTable.Rows)
                result.Add(FlightGroupRulesEditor.getParameterFromTableRow(row));
            return result;*/
        }

/*
        private Dictionary<string, List<FlightAttribute>> retrieveFlightsByFlightPlan(List<DataTable> flightPlans)
        {
            Dictionary<string, List<FlightAttribute>> result = new Dictionary<string, List<FlightAttribute>>();
            foreach (DataTable flightPlan in flightPlans)
            {
                List<FlightAttribute> flightAttributes = new List<FlightAttribute>();
                result.Add(flightPlan.TableName, flightAttributes);

                #region column indexes
                int columnIndexFlightId = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);

                int columnIndexFlightDate = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
                int columnIndexFlightTime = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
                if (columnIndexFlightTime == -1)                
                    columnIndexFlightTime = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_STA);                
                int columnIndexAirline = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
                int columnIndexFlightNb = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
                int columnIndexAirport = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
                int columnIndexFlightCategory = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
                int columnIndexAircraftType = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);

                int columnIndexUser1 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
                int columnIndexUser2 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
                int columnIndexUser3 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
                int columnIndexUser4 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
                int columnIndexUser5 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
                #endregion

                if (columnIndexFlightId == -1 || columnIndexFlightDate == -1 || columnIndexFlightTime == -1 || columnIndexAirline == -1
                    || columnIndexFlightNb == -1 || columnIndexAirport == -1 || columnIndexFlightCategory == -1 || columnIndexAircraftType == -1
                    || columnIndexUser1 == -1 || columnIndexUser2 == -1 || columnIndexUser3 == -1 || columnIndexUser4 == -1 || columnIndexUser5 == -1)
                {
                    continue;
                }
                foreach (DataRow row in flightPlan.Rows)
                {
                    FlightAttribute flightAttribute = new FlightAttribute();
                    int id = -1;
                    if (columnIndexFlightId != -1 && row[columnIndexFlightId] != null && int.TryParse(row[columnIndexFlightId].ToString(), out id))                    
                        flightAttribute.flightId = id;
                    
                    DateTime flightDate = DateTime.MinValue;
                    if (columnIndexFlightDate != -1 && row[columnIndexFlightDate] != null && DateTime.TryParse(row[columnIndexFlightDate].ToString(), out flightDate))
                        flightAttribute.flightDate = flightDate;
                    
                    TimeSpan flightTime = TimeSpan.MinValue;
                    if (columnIndexFlightTime != -1 && row[columnIndexFlightTime] != null && TimeSpan.TryParse(row[columnIndexFlightTime].ToString(), out flightTime))
                        flightAttribute.flightTime = flightTime;
                        
                    if (columnIndexAirline != -1 && row[columnIndexAirline] != null)
                        flightAttribute.airlineCode = row[columnIndexAirline].ToString();
                        
                    if (columnIndexFlightNb != -1 && row[columnIndexFlightNb] != null)
                        flightAttribute.flightNb = row[columnIndexFlightNb].ToString();
                       
                    if (columnIndexAirport != -1 && row[columnIndexAirport] != null)
                        flightAttribute.airportCode = row[columnIndexAirport].ToString();
                        
                    if (columnIndexFlightCategory != -1 && row[columnIndexFlightCategory] != null)
                        flightAttribute.flightCategory = row[columnIndexFlightCategory].ToString();
                        
                    if (columnIndexAircraftType != -1 && row[columnIndexAircraftType] != null)
                        flightAttribute.aircraftType = row[columnIndexAircraftType].ToString();
                    
                    #region User 1..5
                    if (columnIndexUser1 != -1 && row[columnIndexUser1] != null)
                        flightAttribute.user1 = row[columnIndexUser1].ToString();
                    if (columnIndexUser2 != -1 && row[columnIndexUser2] != null)
                        flightAttribute.user2 = row[columnIndexUser2].ToString();
                    if (columnIndexUser3 != -1 && row[columnIndexUser3] != null)
                        flightAttribute.user3 = row[columnIndexUser3].ToString();
                    if (columnIndexUser4 != -1 && row[columnIndexUser4] != null)
                        flightAttribute.user4 = row[columnIndexUser4].ToString();
                    if (columnIndexUser5 != -1 && row[columnIndexUser5] != null)
                        flightAttribute.user5 = row[columnIndexUser5].ToString();
                    #endregion
                    flightAttributes.Add(flightAttribute);
                }
            }
            return result;
        }
*/
        private Dictionary<string, List<FlightAttribute>> retrieveFlightsByFlightPlan(List<DataTable> flightPlans)
        {
            Dictionary<string, List<FlightAttribute>> result = new Dictionary<string, List<FlightAttribute>>();
            foreach (DataTable flightPlan in flightPlans)
            {
                List<FlightAttribute> flightAttributes = retrieveFlightsByFlightPlan(flightPlan);
                result.Add(flightPlan.TableName, flightAttributes);
            }
            return result;
        }

        public static List<FlightAttribute> retrieveFlightsByFlightPlan(DataTable flightPlan)
        {
            List<FlightAttribute> flightAttributes = new List<FlightAttribute>();

            #region column indexes
            int columnIndexFlightId = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);

            int columnIndexFlightDate = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            int columnIndexFlightTime = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
            if (columnIndexFlightTime == -1)
                columnIndexFlightTime = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
            int columnIndexAirline = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int columnIndexFlightNb = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int columnIndexAirport = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            int columnIndexFlightCategory = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int columnIndexAircraftType = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);

            int columnIndexUser1 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
            int columnIndexUser2 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
            int columnIndexUser3 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
            int columnIndexUser4 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
            int columnIndexUser5 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
            #endregion

            if (columnIndexFlightId == -1 || columnIndexFlightDate == -1 || columnIndexFlightTime == -1 || columnIndexAirline == -1
                || columnIndexFlightNb == -1 || columnIndexAirport == -1 || columnIndexFlightCategory == -1 || columnIndexAircraftType == -1
                || columnIndexUser1 == -1 || columnIndexUser2 == -1 || columnIndexUser3 == -1 || columnIndexUser4 == -1 || columnIndexUser5 == -1)
            {
                return flightAttributes;
            }
            foreach (DataRow row in flightPlan.Rows)
            {
                FlightAttribute flightAttribute = new FlightAttribute();
                int id = -1;
                if (columnIndexFlightId != -1 && row[columnIndexFlightId] != null && int.TryParse(row[columnIndexFlightId].ToString(), out id))
                    flightAttribute.flightId = id;

                DateTime flightDate = DateTime.MinValue;
                if (columnIndexFlightDate != -1 && row[columnIndexFlightDate] != null && DateTime.TryParse(row[columnIndexFlightDate].ToString(), out flightDate))
                    flightAttribute.flightDate = flightDate;

                TimeSpan flightTime = TimeSpan.MinValue;
                if (columnIndexFlightTime != -1 && row[columnIndexFlightTime] != null && TimeSpan.TryParse(row[columnIndexFlightTime].ToString(), out flightTime))
                    flightAttribute.flightTime = flightTime;

                if (columnIndexAirline != -1 && row[columnIndexAirline] != null)
                    flightAttribute.airlineCode = row[columnIndexAirline].ToString();

                if (columnIndexFlightNb != -1 && row[columnIndexFlightNb] != null)
                    flightAttribute.flightNb = row[columnIndexFlightNb].ToString();

                if (columnIndexAirport != -1 && row[columnIndexAirport] != null)
                    flightAttribute.airportCode = row[columnIndexAirport].ToString();

                if (columnIndexFlightCategory != -1 && row[columnIndexFlightCategory] != null)
                    flightAttribute.flightCategory = row[columnIndexFlightCategory].ToString();

                if (columnIndexAircraftType != -1 && row[columnIndexAircraftType] != null)
                    flightAttribute.aircraftType = row[columnIndexAircraftType].ToString();

                #region User 1..5
                if (columnIndexUser1 != -1 && row[columnIndexUser1] != null)
                    flightAttribute.user1 = row[columnIndexUser1].ToString();
                if (columnIndexUser2 != -1 && row[columnIndexUser2] != null)
                    flightAttribute.user2 = row[columnIndexUser2].ToString();
                if (columnIndexUser3 != -1 && row[columnIndexUser3] != null)
                    flightAttribute.user3 = row[columnIndexUser3].ToString();
                if (columnIndexUser4 != -1 && row[columnIndexUser4] != null)
                    flightAttribute.user4 = row[columnIndexUser4].ToString();
                if (columnIndexUser5 != -1 && row[columnIndexUser5] != null)
                    flightAttribute.user5 = row[columnIndexUser5].ToString();
                #endregion
                flightAttributes.Add(flightAttribute);
            }
            return flightAttributes;
        }

        #region User interface
        
        private void addRulesGroupBoxes(Panel parent, List<FlightGroupRulesParameter> flightGroupRulesParameters)
        {
            int width = 590;
            int height = 60;
            int x = 3;
            for (int i = 0; i < flightGroupRulesParameters.Count; i++)
            {
                FlightGroupRulesParameter rule = flightGroupRulesParameters[i];
                GroupBox ruleGb = new GroupBox();
                parent.Controls.Add(ruleGb);
                
                ruleGb.Text = rule.ruleName;
                ruleGb.Tag = rule.ruleName;

                ruleGb.Size = new Size(width, height);

                int y = 3;
                if (i > 0)
                    y += height * i + 10;
                ruleGb.Location = new Point(x, y);

                Label label = new Label();
                label.Text = "Transfer Amount";
                ruleGb.Controls.Add(label);
                label.Location = new Point(105, 30);
                label.AutoSize = true;

                TextBox textBox = new TextBox();
                ruleGb.Controls.Add(textBox);
                textBox.Size = new Size(135, 20);
                textBox.Location = new Point(255, 27);
                textBox.TextChanged += new EventHandler(rulesTextBox_TextChanged);
            }
        }
                
        private void initFlightPlanComboBox()
        {
            flightPlanComboBox.Items.Clear();
            foreach (string flightPlanName in flightsByFlightPlan.Keys)
                flightPlanComboBox.Items.Add(flightPlanName);
            
            if (flightPlanComboBox.Items.Count > 0)
                flightPlanComboBox.SelectedIndex = 0;            
        }

        private void flightPlanComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == null || sender.GetType() != typeof(ComboBox))
                return;
            ComboBox senderCb = (ComboBox)sender;
            if (senderCb.SelectedItem == null || !flightsByFlightPlan.ContainsKey(senderCb.SelectedItem.ToString()))
                return;
            List<FlightAttribute> flights = flightsByFlightPlan[senderCb.SelectedItem.ToString()];
            updateFlightIDComboBox(flights);
        }

        private void updateFlightIDComboBox(List<FlightAttribute> flights)
        {
            flightIdComboBox.Items.Clear();
            foreach (FlightAttribute flight in flights)
                flightIdComboBox.Items.Add(flight.flightId);
            
            if (flightIdComboBox.Items.Count > 0)
                flightIdComboBox.SelectedIndex = 0;            
        }

        private void flightIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == null || sender.GetType() != typeof(ComboBox))
                return;
            ComboBox senderCb = (ComboBox)sender;
            int flightID = -1;
            if (senderCb.SelectedItem == null || !Int32.TryParse(senderCb.SelectedItem.ToString(), out flightID))
                return;
            if (flightPlanComboBox.SelectedItem == null || !flightsByFlightPlan.ContainsKey(flightPlanComboBox.SelectedItem.ToString()))
                return;

            List<FlightAttribute> flights = flightsByFlightPlan[flightPlanComboBox.SelectedItem.ToString()];
            FlightAttribute flight = getFlightFromListByFlightID(flightID, flights);
            if (flight != null)
            {
                updateFlightInformationPanel(flight);
                if (sessionParametersByFlightPlan.ContainsKey(flightPlanComboBox.SelectedItem.ToString()))
                {
                    List<DeterministicTransferDistributionParameter> deterministicParamByFlightPlan = sessionParametersByFlightPlan[flightPlanComboBox.SelectedItem.ToString()];
                    DeterministicTransferDistributionParameter deterministicParam = null;
                    foreach (DeterministicTransferDistributionParameter p in deterministicParamByFlightPlan)
                    {
                        if (p.flight.flightId == flight.flightId)
                        {
                            deterministicParam = p;
                            break;
                        }
                    }
                    if (deterministicParam != null)
                        updateRulesPanel(deterministicParam.deterministicValues);
                    else
                        updateRulesPanel(null);
                }
                else
                {
                    updateRulesPanel(null);
                }
            }
        }

        private FlightAttribute getFlightFromListByFlightID(int flightID, List<FlightAttribute> flights)
        {
            foreach (FlightAttribute flight in flights)
            {
                if (flight.flightId == flightID)
                    return flight;
            }
            return null;
        }

        private void updateFlightInformationPanel(FlightAttribute flight)
        {
            DateTime completeDate = flight.flightDate.Add(flight.flightTime);
            flightDateTextBox.Text = completeDate.ToString("dd/MM/yyyy hh:mm");
            airlineTextBox.Text = flight.airlineCode;
            airportTextBox.Text = flight.airportCode;
            flightNumberTextBox.Text = flight.flightNb;
            flightCategoryTextBox.Text = flight.flightCategory;
            aircraftTypeTextBox.Text = flight.aircraftType;
        }
        
        private void updateRulesPanel(List<RuleAndDeterministicValues> deterministicParamValues)
        {
            if (deterministicParamValues == null || deterministicParamValues.Count == 0)
            {
                foreach (Control control in rulesPanel.Controls)
                {
                    if (control.GetType() != typeof(GroupBox))
                        continue;
                    GroupBox gb = (GroupBox)control;
                    foreach (Control gbChild in gb.Controls)
                    {
                        if (gbChild.GetType() != typeof(TextBox))
                            continue;
                        ((TextBox)gbChild).TextChanged -= new EventHandler(rulesTextBox_TextChanged);
                        ((TextBox)gbChild).Text = "";
                        ((TextBox)gbChild).TextChanged += new EventHandler(rulesTextBox_TextChanged);
                    }
                }
            }
            else
            {
                foreach (RuleAndDeterministicValues paramAndValue in deterministicParamValues)
                {
                    TextBox tb = getTextBoxByRuleName(paramAndValue.rule.ruleName, rulesPanel);
                    if (tb != null)
                    {
                        tb.TextChanged -= new EventHandler(rulesTextBox_TextChanged);
                        tb.Text = paramAndValue.nbTransferBags.ToString();
                        tb.TextChanged += new EventHandler(rulesTextBox_TextChanged);
                    }
                }
            }
        }

        private TextBox getTextBoxByRuleName(string ruleName, Panel rulesPanel)
        {
            TextBox result = null;
            foreach (Control control in rulesPanel.Controls)
            {
                if (control.GetType() != typeof(GroupBox))
                    continue;
                GroupBox gb = (GroupBox)control;
                if (gb.Tag == null || gb.Tag.ToString() != ruleName)
                    continue;
                foreach (Control gbChild in gb.Controls)
                {
                    if (gbChild.GetType() != typeof(TextBox))
                        continue;
                    result = (TextBox)gbChild;
                    break;
                }
            }
            return result;
        }
        #endregion

        private List<DeterministicTransferDistributionParameter> getParametersFromDeterministicTransferDistributionTable(DataTable deterministicTransferDistributionTable)
        {
            List<DeterministicTransferDistributionParameter> parameters = new List<DeterministicTransferDistributionParameter>();

            #region column indexes
            int columnIndexFlightPlan = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_PLAN_NAME);
            int columnIndexFlightID = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_ID);
            int columnIndexFlightCompleteDate = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_COMPLETE_DATE);
            int columnIndexFlightNb = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_NB);
            int columnIndexFlightCategory = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_CATEGORY);
            int columnIndexAirline = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRLINE);
            int columnIndexAirport = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRPORT);
            int columnIndexUser1 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_1);
            int columnIndexUser2 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_2);
            int columnIndexUser3 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_3);
            int columnIndexUser4 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_4);
            int columnIndexUser5 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_5);
            int columnIndexTransferBagsDistribution = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_TRANSFER_BAGS_DISTRIBUTION);

            List<int> indexes = new List<int>(new int[] { columnIndexFlightPlan, columnIndexFlightID, columnIndexFlightCompleteDate, columnIndexFlightNb, columnIndexFlightCategory,
                                                            columnIndexAirline,columnIndexAirport, columnIndexUser1, columnIndexUser2, columnIndexUser3,columnIndexUser4, 
                                                            columnIndexUser5, columnIndexTransferBagsDistribution  });
            foreach (int index in indexes)
            {
                if (index == -1)
                    return parameters;
            }
            #endregion

            foreach (DataRow row in deterministicTransferDistributionTable.Rows)
            {
                DeterministicTransferDistributionParameter parameter = new DeterministicTransferDistributionParameter();
                
                if (row[columnIndexFlightPlan] == null || !flightsByFlightPlan.ContainsKey(row[columnIndexFlightPlan].ToString()))
                    continue;
                int id = -1;
                if (row[columnIndexFlightID] == null || !Int32.TryParse(row[columnIndexFlightID].ToString(), out id))
                    continue;

                string flightPlanName = row[columnIndexFlightPlan].ToString();
                parameter.flightPlanTableName = flightPlanName;

                List<FlightAttribute> flightPlanFlights = flightsByFlightPlan[flightPlanName];
                FlightAttribute flight = null;
                foreach (FlightAttribute f in flightPlanFlights)
                {
                    if (f.flightId == id)
                    {
                        flight = f;
                        break;
                    }
                }
                parameter.flight = flight;

                if (row[columnIndexTransferBagsDistribution] != null)
                    parameter.deterministicValues = getDeterministicValuesByDeterministicTransferTableCellContent(row[columnIndexTransferBagsDistribution].ToString());
                parameters.Add(parameter);
            }
            return parameters;
        }

        private const string RULE_VALUE_SEPARATOR = ":";
        private const string RULE_VALUE_PAIR_SEPARATOR = ",";
        public static List<RuleAndDeterministicValues> getDeterministicValuesByDeterministicTransferTableCellContent(string rulesWithValues)
        {
            List<RuleAndDeterministicValues> rulesWithDeterministicValues = new List<RuleAndDeterministicValues>();
            string[] rulesWithValuesSplitted = rulesWithValues.Split(new string[] { RULE_VALUE_PAIR_SEPARATOR }, StringSplitOptions.None);
            foreach (string ruleWithValue in rulesWithValuesSplitted)
            {
                RuleAndDeterministicValues flightGroupRuleDeterministicValues = new RuleAndDeterministicValues();

                string[] singleRule = ruleWithValue.Split(new string[] { RULE_VALUE_SEPARATOR }, StringSplitOptions.None);
                if (singleRule.Length != 2 || singleRule[0] == null || singleRule[1] == null)
                    continue;
                FlightGroupRulesParameter rule = new FlightGroupRulesParameter();
                rule.ruleName = singleRule[0].Trim();
                int value = -1;
                if (Int32.TryParse(singleRule[1].Trim(), out value))
                {
                    flightGroupRuleDeterministicValues.rule = rule;
                    flightGroupRuleDeterministicValues.nbTransferBags = value;

                    rulesWithDeterministicValues.Add(flightGroupRuleDeterministicValues);
                }
            }
            return rulesWithDeterministicValues;
        }

        private void initializeSessionParameters(List<DeterministicTransferDistributionParameter> initialParameters)
        {
            sessionParametersByFlightPlan = new Dictionary<string, List<DeterministicTransferDistributionParameter>>();
            foreach (DeterministicTransferDistributionParameter initialParam in initialParameters)
            {
                if (!sessionParametersByFlightPlan.ContainsKey(initialParam.flightPlanTableName))
                {
                    List<DeterministicTransferDistributionParameter> paramsByFlightPlan = new List<DeterministicTransferDistributionParameter>();
                    paramsByFlightPlan.Add(initialParam);
                    sessionParametersByFlightPlan.Add(initialParam.flightPlanTableName, paramsByFlightPlan);
                }
                else
                {
                    List<DeterministicTransferDistributionParameter> paramsByFlightPlan = sessionParametersByFlightPlan[initialParam.flightPlanTableName];
                    paramsByFlightPlan.Add(initialParam);
                }
            }
        }

        private void rulesTextBox_TextChanged(object sendet, EventArgs e)
        {
            saveParametersButton.Enabled = true;
        }

        private void saveParametersButton_Click(object sender, EventArgs e)
        {
            if (flightPlanComboBox.SelectedItem == null)
                return;
            string currentFlightPlanName = flightPlanComboBox.SelectedItem.ToString();
            int currentFlightID = -1;
            if (flightIdComboBox.SelectedItem == null || !Int32.TryParse(flightIdComboBox.SelectedItem.ToString(), out currentFlightID))
                return;
            if (!flightsByFlightPlan.ContainsKey(currentFlightPlanName))
                return;

            FlightAttribute currentFlight = null;
            List<FlightAttribute> currentFlightPlanFlights = flightsByFlightPlan[currentFlightPlanName];
            foreach (FlightAttribute flight in currentFlightPlanFlights)
            {
                if (flight.flightId == currentFlightID)
                {
                    currentFlight = flight;
                    break;
                }
            }
            if (currentFlight == null)
                return;

            DeterministicTransferDistributionParameter currentParameter = null;
            if (sessionParametersByFlightPlan.ContainsKey(currentFlightPlanName))
            {
                List<DeterministicTransferDistributionParameter> currentParameters = sessionParametersByFlightPlan[currentFlightPlanName];
                foreach (DeterministicTransferDistributionParameter p in currentParameters)
                {
                    if (p.flight != null && p.flight.flightId == currentFlight.flightId)
                    {
                        currentParameter = p;
                        break;
                    }
                }
                if (currentParameter == null)
                {
                    currentParameter = new DeterministicTransferDistributionParameter();
                    currentParameter.flight = currentFlight;
                    currentParameter.flightPlanTableName = currentFlightPlanName;
                    currentParameters.Add(currentParameter);
                }
            }
            else
            {
                List<DeterministicTransferDistributionParameter> currentParameters = new List<DeterministicTransferDistributionParameter>();
                sessionParametersByFlightPlan.Add(currentFlightPlanName, currentParameters);
                
                currentParameter = new DeterministicTransferDistributionParameter();
                currentParameter.flight = currentFlight;
                currentParameter.flightPlanTableName = currentFlightPlanName;
                currentParameters.Add(currentParameter);                
            }

            if (currentParameter == null)
                return;

            currentParameter.deterministicValues = new List<RuleAndDeterministicValues>();
            foreach (Control ruleGb in rulesPanel.Controls)
            {
                if (ruleGb.GetType() != typeof(GroupBox) || ruleGb.Tag == null)
                    continue;
                string ruleName = ((GroupBox)ruleGb).Tag.ToString();
                TextBox ruleTb = null;
                foreach (Control ruleGbChild in ruleGb.Controls)
                {
                    if (ruleGbChild.GetType() == typeof(TextBox))
                    {
                        ruleTb = (TextBox)ruleGbChild;
                        break;
                    }
                }
                int ruleTbValue = -1;
                if (ruleTb != null && (Int32.TryParse(ruleTb.Text, out ruleTbValue) || ruleTb.Text == string.Empty))
                {
                    RuleAndDeterministicValues ruleAndValue = new RuleAndDeterministicValues();
                    ruleAndValue.rule = new FlightGroupRulesParameter();
                    ruleAndValue.rule.ruleName = ruleName;
                    if (ruleTb.Text == string.Empty)
                        ruleTbValue = 0;
                    ruleAndValue.nbTransferBags = ruleTbValue;
                    currentParameter.deterministicValues.Add(ruleAndValue);
                }                
            }
            saveParametersButton.Enabled = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!proceedAfterSaveParametersCheck())
            {
                DialogResult = DialogResult.None;
                return;
            }

            #region column indexes
            int columnIndexFlightPlan = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_PLAN_NAME);
            int columnIndexFlightID = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_ID);
            int columnIndexFlightCompleteDate = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_COMPLETE_DATE);
            int columnIndexFlightNb = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_NB);
            int columnIndexFlightCategory = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_CATEGORY);
            int columnIndexAirline = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRLINE);
            int columnIndexAirport = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRPORT);
            int columnIndexUser1 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_1);
            int columnIndexUser2 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_2);
            int columnIndexUser3 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_3);
            int columnIndexUser4 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_4);
            int columnIndexUser5 = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_5);
            int columnIndexTransferBagsDistribution = deterministicTransferDistributionTable.Columns.IndexOf(DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_TRANSFER_BAGS_DISTRIBUTION);

            List<int> indexes = new List<int>(new int[] { columnIndexFlightPlan, columnIndexFlightID, columnIndexFlightCompleteDate, columnIndexFlightNb, columnIndexFlightCategory,
                                                            columnIndexAirline,columnIndexAirport, columnIndexUser1, columnIndexUser2, columnIndexUser3,columnIndexUser4, 
                                                            columnIndexUser5, columnIndexTransferBagsDistribution  });
            foreach (int index in indexes)
            {
                if (index == -1)
                    return;
            }
            #endregion

            deterministicTransferDistributionTable.Clear();
            foreach (KeyValuePair<string, List<DeterministicTransferDistributionParameter>> pair in sessionParametersByFlightPlan)
            {
                string flightPlanName = pair.Key;
                List<DeterministicTransferDistributionParameter> parametersByFlightPlan = pair.Value;
                foreach (DeterministicTransferDistributionParameter parameter in parametersByFlightPlan)
                {
                    DataRow newRow = deterministicTransferDistributionTable.NewRow();
                    newRow[columnIndexFlightPlan] = parameter.flightPlanTableName;

                    newRow[columnIndexFlightID] = parameter.flight.flightId;
                    newRow[columnIndexFlightCompleteDate] = parameter.flight.flightDate.Add(parameter.flight.flightTime);
                    newRow[columnIndexFlightNb] = parameter.flight.flightNb;
                    newRow[columnIndexFlightCategory] = parameter.flight.flightCategory;
                    newRow[columnIndexAirline] = parameter.flight.airlineCode;
                    newRow[columnIndexAirport] = parameter.flight.airportCode;
                    newRow[columnIndexUser1] = parameter.flight.user1;
                    newRow[columnIndexUser2] = parameter.flight.user2;
                    newRow[columnIndexUser3] = parameter.flight.user3;
                    newRow[columnIndexUser4] = parameter.flight.user4;
                    newRow[columnIndexUser5] = parameter.flight.user5;

                    string transferBagDistributionString = getTransferBagDistributionAsTableFormat(parameter.deterministicValues);
                    newRow[columnIndexTransferBagsDistribution] = transferBagDistributionString;

                    deterministicTransferDistributionTable.Rows.Add(newRow);
                }
            }
        }

        private string getTransferBagDistributionAsTableFormat(List<RuleAndDeterministicValues> deterministicValues)
        {
            string result = string.Empty;
            for (int i = 0; i < deterministicValues.Count; i++)
            {
                RuleAndDeterministicValues ruleAndValue = deterministicValues[i];
                result += ruleAndValue.rule.ruleName + RULE_VALUE_SEPARATOR + " " + ruleAndValue.nbTransferBags;
                if (i < deterministicValues.Count - 1)
                    result += RULE_VALUE_PAIR_SEPARATOR + " ";
            }
            return result;
        }

        private bool proceedAfterSaveParametersCheck()
        {
            if (!saveParametersButton.Enabled)
                return true;
            DialogResult dr = MessageBox.Show("The current parameters are not saved. Do you want to proceed?", "Distribution parameters",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            bool proceed = dr == DialogResult.Yes;
            if (proceed)
                saveParametersButton.Enabled = false;            
            return proceed;
        }


    }
}
