using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.com.crispico.general_allocation.assistant;
using SIMCORE_TOOL.com.crispico.general_allocation.constants;
using SIMCORE_TOOL.com.crispico.general_allocation.parameter;

namespace SIMCORE_TOOL.com.crispico.FlightPlansUtils
{
    public partial class FPParametersEditor : Form
    {
        private DataTable selectedFPParametersTable;
        private DataTable rootFPA;
        private DataTable rootFPD;

        private bool editModeOnly = false;

        private List<FlightAttribute> flightAttributesList = new List<FlightAttribute>();
        private List<FlightAttribute> filteredFlightAttributesList = new List<FlightAttribute>();
        private List<FlightConfiguration> tempFlightConfigurations = new List<FlightConfiguration>();

        private FlightAttribute.ARR_OR_DEP_FLIGHT_TAG currentArrOrDepSelection
        {
            get
            {
                if (arrivalOrDepartureComboBox.SelectedItem != null
                    && arrivalOrDepartureComboBox.SelectedItem.GetType() == typeof(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG))
                {
                    return (FlightAttribute.ARR_OR_DEP_FLIGHT_TAG)arrivalOrDepartureComboBox.SelectedItem;
                }
                return FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE;
            }
        }

        private int currentFlightIdSelection
        {
            get
            {
                int result = -1;
                if (flightIdComboBox.SelectedItem == null
                    || !int.TryParse(flightIdComboBox.SelectedItem.ToString(), out result))
                {
                    result = -1;
                }
                return result;
            }
        }

        public FPParametersEditor(DataTable pFPParametersTable, DataTable pRootArrivalFlightPlan, DataTable pRootDepartureFlightPlan,
            FlightAttribute.ARR_OR_DEP_FLIGHT_TAG selectedFlightArrOrDep, int selectedFlightId)
        {
            initializeDesign();

            this.Text = "Modify the \"" + pFPParametersTable.TableName + "\" table";

            selectedFPParametersTable = pFPParametersTable;            
            rootFPA = pRootArrivalFlightPlan;
            rootFPD = pRootDepartureFlightPlan;
            
            editModeOnly = true;
            multiEditCheckBox.Enabled = false;

            if (selectedFlightArrOrDep == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.A)
            {
                flightAttributesList.AddRange(getFlightAttributesListByFlightPlan(rootFPA, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.A, selectedFlightId).ToArray());
            }
            else if (selectedFlightArrOrDep == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.D)
            {
                flightAttributesList.AddRange(getFlightAttributesListByFlightPlan(rootFPD, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.D, selectedFlightId).ToArray());
            }

            tempFlightConfigurations = getFlightConfigurationsListByFPParametersTable(selectedFPParametersTable, selectedFlightId);

            setArrivalOrDepartureComboBox(!editModeOnly, flightAttributesList);
        }

        public FPParametersEditor(DataTable pFPParametersTable, DataTable pRootArrivalFlightPlan, DataTable pRootDepartureFlightPlan)
        {
            initializeDesign();

            this.Text = "Modify the \"" + pFPParametersTable.TableName + "\" table";

            selectedFPParametersTable = pFPParametersTable;
            rootFPA = pRootArrivalFlightPlan;
            rootFPD = pRootDepartureFlightPlan;

            if (rootFPA != null)
            {
                flightAttributesList.AddRange(getFlightAttributesListByFlightPlan(rootFPA, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.A, -1).ToArray());
            }
            if (rootFPD != null)
            {
                flightAttributesList.AddRange(getFlightAttributesListByFlightPlan(rootFPD, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.D, -1).ToArray());
            }
            tempFlightConfigurations = getFlightConfigurationsListByFPParametersTable(selectedFPParametersTable, -1);

            setArrivalOrDepartureComboBox(!editModeOnly, flightAttributesList);
        }

        private void initializeDesign()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        private List<FlightAttribute> getFlightAttributesListByFlightPlan(DataTable flightPlan, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG arrivalOrDeparture,
            int searchedFlightId)
        {
            List<FlightAttribute> flightsAttributes = new List<FlightAttribute>();
            if (flightPlan == null)
            {
                return flightsAttributes;
            }

            #region column indexes
            int columnIndexFlightId = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            
            int columnIndexFlightDate = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            int columnIndexFlightTime = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
            if (columnIndexFlightTime == -1)
            {
                columnIndexFlightTime = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
            }
            int columnIndexAirline = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            int columnIndexFlightNb = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int columnIndexAirport = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            int columnIndexFlightCategory = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int columnIndexAircraftType = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);

            int columnIndexTsa = flightPlan.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
            int columnIndexNoBsm = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
            int columnIndexCbp = flightPlan.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);

            int columnIndexUser1 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
            int columnIndexUser2 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
            int columnIndexUser3 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
            int columnIndexUser4 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
            int columnIndexUser5 = flightPlan.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
            #endregion

            foreach (DataRow row in flightPlan.Rows)
            {
                bool tableStructureIsValid = true;
                FlightAttribute attributes = new FlightAttribute();
                attributes.arrOrDep = arrivalOrDeparture;
                int id = -1;
                if (columnIndexFlightId != -1 && row[columnIndexFlightId] != null
                    && int.TryParse(row[columnIndexFlightId].ToString(), out id))
                {
                    attributes.flightId = id;
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (searchedFlightId != -1 && searchedFlightId != id)
                {
                    continue;
                }
                DateTime flightDate = DateTime.MinValue;
                if (tableStructureIsValid && columnIndexFlightDate != -1)
                {
                    if (row[columnIndexFlightDate] != null && DateTime.TryParse(row[columnIndexFlightDate].ToString(), out flightDate))
                    {
                        attributes.flightDate = flightDate;
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                TimeSpan flightTime = TimeSpan.MinValue;
                if (tableStructureIsValid && columnIndexFlightTime != -1)
                {
                    if (row[columnIndexFlightTime] != null
                        && TimeSpan.TryParse(row[columnIndexFlightTime].ToString(), out flightTime))
                    {
                        attributes.flightTime = flightTime;
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexAirline != -1)
                {
                    if (row[columnIndexAirline] != null)
                    {
                        attributes.airlineCode = row[columnIndexAirline].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexFlightNb != -1)
                {
                    if (row[columnIndexFlightNb] != null)
                    {
                        attributes.flightNb = row[columnIndexFlightNb].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexAirport != -1)
                {
                    if (row[columnIndexAirport] != null)
                    {
                        attributes.airportCode = row[columnIndexAirport].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexFlightCategory != -1)
                {
                    if (row[columnIndexFlightCategory] != null)
                    {
                        attributes.flightCategory = row[columnIndexFlightCategory].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexAircraftType != -1)
                {
                    if (row[columnIndexAircraftType] != null)
                    {
                        attributes.aircraftType = row[columnIndexAircraftType].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (columnIndexTsa == -1 && columnIndexNoBsm == -1 && columnIndexCbp == -1)
                {
                    tableStructureIsValid = false;
                }

                #region Tsa, Cbp, NoBsm
                bool boolResult = false;
                if (tableStructureIsValid && columnIndexTsa != -1)
                {

                    if (row[columnIndexTsa] != null && bool.TryParse(row[columnIndexTsa].ToString(), out boolResult))
                    {
                        attributes.tsa = boolResult;
                    }
                }
                if (tableStructureIsValid && columnIndexCbp != -1)
                {

                    if (row[columnIndexCbp] != null && bool.TryParse(row[columnIndexCbp].ToString(), out boolResult))
                    {
                        attributes.cbp = boolResult;
                    }
                }
                if (tableStructureIsValid && columnIndexNoBsm != -1)
                {

                    if (row[columnIndexNoBsm] != null && bool.TryParse(row[columnIndexNoBsm].ToString(), out boolResult))
                    {
                        attributes.noBsm = boolResult;
                    }
                }
                #endregion

                #region User 1..5
                if (tableStructureIsValid && columnIndexUser1 != -1)
                {
                    if (row[columnIndexUser1] != null)
                    {
                        attributes.user1 = row[columnIndexUser1].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexUser2 != -1)
                {
                    if (row[columnIndexUser2] != null)
                    {
                        attributes.user2 = row[columnIndexUser2].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexUser3 != -1)
                {
                    if (row[columnIndexUser3] != null)
                    {
                        attributes.user3 = row[columnIndexUser3].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexUser4 != -1)
                {
                    if (row[columnIndexUser4] != null)
                    {
                        attributes.user4 = row[columnIndexUser4].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                if (tableStructureIsValid && columnIndexUser5 != -1)
                {
                    if (row[columnIndexUser5] != null)
                    {
                        attributes.user5 = row[columnIndexUser5].ToString();
                    }
                }
                else
                {
                    tableStructureIsValid = false;
                }
                #endregion

                if (tableStructureIsValid)
                {
                    flightsAttributes.Add(attributes);
                }
            }
            return flightsAttributes;
        }

        private List<FlightConfiguration> getFlightConfigurationsListByFPParametersTable(DataTable fpParametersTable, int searchedFlightId)
        {
            List<FlightConfiguration> flightConfigurations = new List<FlightConfiguration>();
            if (fpParametersTable == null)
            {
                return flightConfigurations;
            }
            #region column indexes
            List<int> indexes = new List<int>();
            #region flight attributes
            int columnIndexAorD = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_ARR_OR_DEP);
            indexes.Add(columnIndexAorD);
            int columnIndexFlightId = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_FLIGHT_ID);
            indexes.Add(columnIndexFlightId);

            int columnIndexFlightDate = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            indexes.Add(columnIndexFlightDate);
            int columnIndexFlightTime = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_FLIGHT_TIME);
            indexes.Add(columnIndexFlightTime);

            int columnIndexAirline = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            indexes.Add(columnIndexAirline);
            int columnIndexFlightNb = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            indexes.Add(columnIndexFlightNb);
            int columnIndexAirport = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            indexes.Add(columnIndexAirport);
            int columnIndexFlightCategory = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            indexes.Add(columnIndexFlightCategory);
            int columnIndexAircraft = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
            indexes.Add(columnIndexAircraft);

            int columnIndexTsa = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
            indexes.Add(columnIndexTsa);
            int columnIndexCbp = fpParametersTable.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
            indexes.Add(columnIndexCbp);
            int columnIndexNoBsm = fpParametersTable.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
            indexes.Add(columnIndexNoBsm);

            int columnIndexUser1 = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
            indexes.Add(columnIndexUser1);
            int columnIndexUser2 = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
            indexes.Add(columnIndexUser2);
            int columnIndexUser3 = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
            indexes.Add(columnIndexUser3);
            int columnIndexUser4 = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
            indexes.Add(columnIndexUser4);
            int columnIndexUser5 = fpParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
            indexes.Add(columnIndexUser5);
            #endregion

            #region flight parameters
            int columnIndexNbOrigEcoPax = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_ECO_PAX);
            indexes.Add(columnIndexNbOrigEcoPax);
            int columnIndexNbOrigFbPax = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_FB_PAX);
            indexes.Add(columnIndexNbOrigFbPax);

            int columnIndexNbTransferEcoPax = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_ECO_PAX);
            indexes.Add(columnIndexNbTransferEcoPax);
            int columnIndexNbTransferFbPax = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_FB_PAX);
            indexes.Add(columnIndexNbTransferFbPax);

            int columnIndexNbTermEcoPax = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TERM_ECO_PAX);
            indexes.Add(columnIndexNbTermEcoPax);
            int columnIndexNbTermFbPax = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TERM_FB_PAX);
            indexes.Add(columnIndexNbTermFbPax);

            int columnIndexNbOrigEcoBags = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_ECO_BAGS);
            indexes.Add(columnIndexNbOrigEcoBags);
            int columnIndexNbOrigFbBags = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_FB_BAGS);
            indexes.Add(columnIndexNbOrigFbBags);

            int columnIndexNbTransferEcoBags = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_ECO_BAGS);
            indexes.Add(columnIndexNbTransferEcoBags);
            int columnIndexNbTransferFbBags = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_FB_BAGS);
            indexes.Add(columnIndexNbTransferFbBags);

            int columnIndexNbTermEcoBags = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TERM_ECO_BAGS);
            indexes.Add(columnIndexNbTermEcoBags);
            int columnIndexNbTermFbBags = fpParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TERM_FB_BAGS);
            indexes.Add(columnIndexNbTermFbBags);
            #endregion
            foreach (int index in indexes)
            {
                if (index == -1)
                {
                    return flightConfigurations;
                }
            }
            #endregion
            for (int i = 0; i < fpParametersTable.Rows.Count; i ++)
            {
                DataRow row = fpParametersTable.Rows[i];
                FlightConfiguration flightConfiguration = new FlightConfiguration();
                FlightAttribute flightAttribute = new FlightAttribute();
                flightConfiguration.flightAttribute = flightAttribute;
                FlightParameter flightParameter = new FlightParameter();
                flightConfiguration.flightParameter = flightParameter;

                #region attributes
                if (row[columnIndexAorD] == null || row[columnIndexFlightId] == null)
                {
                    continue;
                }
                if (row[columnIndexAorD] != null)
                {
                    try
                    {
                        flightAttribute.arrOrDep = (FlightAttribute.ARR_OR_DEP_FLIGHT_TAG)Enum
                            .Parse(typeof(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG), row[columnIndexAorD].ToString());
                    }
                    catch (Exception e)
                    {
                        int rowNb = i + 1;
                        OverallTools.ExternFunctions.PrintLogFile("Error while retrieving the Flight Plan parameters from the table \"" 
                            + fpParametersTable.TableName + "\" at row nb " + rowNb + ". " + e.Message);
                        continue;
                    }
                }
                int id = -1;
                if (int.TryParse(row[columnIndexFlightId].ToString(), out id))
                {
                    flightAttribute.flightId = id;
                }
                else
                {
                    continue;
                }
                if (searchedFlightId != -1 && searchedFlightId != id)
                {
                    continue;
                }
                DateTime date = DateTime.MinValue;
                if (row[columnIndexFlightDate] != null && DateTime.TryParse(row[columnIndexFlightDate].ToString(), out date))
                {
                    flightAttribute.flightDate = date;
                }
                TimeSpan time = TimeSpan.MinValue;
                if (row[columnIndexFlightTime] != null && TimeSpan.TryParse(row[columnIndexFlightTime].ToString(), out time))
                {
                    flightAttribute.flightTime = time;
                }
                if (row[columnIndexAirline] != null)
                {
                    flightAttribute.airlineCode = row[columnIndexAirline].ToString();
                }
                if (row[columnIndexFlightNb] != null)
                {
                    flightAttribute.flightNb = row[columnIndexFlightNb].ToString();
                }
                if (row[columnIndexAirport] != null)
                {
                    flightAttribute.airportCode = row[columnIndexAirport].ToString();
                }
                if (row[columnIndexFlightCategory] != null)
                {
                    flightAttribute.flightCategory = row[columnIndexFlightCategory].ToString();
                }
                if (row[columnIndexAircraft] != null)
                {
                    flightAttribute.aircraftType = row[columnIndexAircraft].ToString();
                }

                bool tempBoolValue = false;
                if (row[columnIndexTsa] != null && bool.TryParse(row[columnIndexTsa].ToString(), out tempBoolValue))
                {
                    flightAttribute.tsa = tempBoolValue;
                }
                if (row[columnIndexCbp] != null && bool.TryParse(row[columnIndexCbp].ToString(), out tempBoolValue))
                {
                    flightAttribute.cbp = tempBoolValue;
                }
                if (row[columnIndexNoBsm] != null && bool.TryParse(row[columnIndexNoBsm].ToString(), out tempBoolValue))
                {
                    flightAttribute.noBsm = tempBoolValue;
                }

                if (row[columnIndexUser1] != null)
                {
                    flightAttribute.user1 = row[columnIndexUser1].ToString();
                }
                if (row[columnIndexUser2] != null)
                {
                    flightAttribute.user2 = row[columnIndexUser2].ToString();
                }
                if (row[columnIndexUser3] != null)
                {
                    flightAttribute.user3 = row[columnIndexUser3].ToString();
                }
                if (row[columnIndexUser4] != null)
                {
                    flightAttribute.user4 = row[columnIndexUser4].ToString();
                }
                if (row[columnIndexUser5] != null)
                {
                    flightAttribute.user5 = row[columnIndexUser5].ToString();
                }
                #endregion

                #region parameters
                double tempDoubleValue = -1;

                #region nb pax
                if (row[columnIndexNbOrigEcoPax] != null && double.TryParse(row[columnIndexNbOrigEcoPax].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbOrigEcoPax = tempDoubleValue;
                }
                if (row[columnIndexNbOrigFbPax] != null && double.TryParse(row[columnIndexNbOrigFbPax].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbOrigFbPax = tempDoubleValue;
                }

                if (row[columnIndexNbTransferEcoPax] != null && double.TryParse(row[columnIndexNbTransferEcoPax].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbTransferEcoPax = tempDoubleValue;
                }
                if (row[columnIndexNbTransferFbPax] != null && double.TryParse(row[columnIndexNbTransferFbPax].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbTransferFbPax = tempDoubleValue;
                }

                if (row[columnIndexNbTermEcoPax] != null && double.TryParse(row[columnIndexNbTermEcoPax].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbTermEcoPax = tempDoubleValue;
                }
                if (row[columnIndexNbTermFbPax] != null && double.TryParse(row[columnIndexNbTermFbPax].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbTermFbPax = tempDoubleValue;
                }
                #endregion

                #region nb bags
                if (row[columnIndexNbOrigEcoBags] != null && double.TryParse(row[columnIndexNbOrigEcoBags].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbOrigEcoBags = tempDoubleValue;
                }
                if (row[columnIndexNbOrigFbBags] != null && double.TryParse(row[columnIndexNbOrigFbBags].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbOrigFbBags = tempDoubleValue;
                }

                if (row[columnIndexNbTransferEcoBags] != null && double.TryParse(row[columnIndexNbTransferEcoBags].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbTransferEcoBags = tempDoubleValue;
                }
                if (row[columnIndexNbTransferFbBags] != null && double.TryParse(row[columnIndexNbTransferFbBags].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbTransferFbBags = tempDoubleValue;
                }

                if (row[columnIndexNbTermEcoBags] != null && double.TryParse(row[columnIndexNbTermEcoBags].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbTermEcoBags = tempDoubleValue;
                }
                if (row[columnIndexNbTermFbBags] != null && double.TryParse(row[columnIndexNbTermFbBags].ToString(), out tempDoubleValue))
                {
                    flightParameter.nbTermFbBags = tempDoubleValue;
                }
                #endregion

                #endregion

                flightConfigurations.Add(flightConfiguration);
            }

            return flightConfigurations;
        }

        private void setArrivalOrDepartureComboBox(bool isEnabled, List<FlightAttribute> flightsAttributesList)
        {
            arrivalOrDepartureComboBox.Items.Clear();
            arrivalOrDepartureComboBox.Enabled = isEnabled;
            foreach (FlightAttribute flightAttributes in flightsAttributesList)
            {
                if (!arrivalOrDepartureComboBox.Items.Contains(flightAttributes.arrOrDep))
                {
                    arrivalOrDepartureComboBox.Items.Add(flightAttributes.arrOrDep);
                }
            }
            if (arrivalOrDepartureComboBox.Items.Count > 0)
            {
                arrivalOrDepartureComboBox.SelectedIndex = 0;
            }
        }

        private void setFlightIdComboBox(bool isEnabled, List<FlightAttribute> flightsAttributesList, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG arrivalOrDeparture)
        {
            flightIdComboBox.Items.Clear();
            flightIdComboBox.Enabled = isEnabled;
            foreach (FlightAttribute flightAttributes in flightsAttributesList)
            {
                if (flightAttributes.arrOrDep == arrivalOrDeparture 
                    && !flightIdComboBox.Items.Contains(flightAttributes.flightId))
                {
                    flightIdComboBox.Items.Add(flightAttributes.flightId);
                }
            }
            if (flightIdComboBox.Items.Count > 0)
            {
                flightIdComboBox.SelectedIndex = 0;
            }
        }

        private FlightAttribute.ARR_OR_DEP_FLIGHT_TAG previousArrOrDep = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE;
        private void arrivalOrDepartureComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFlightIdComboBox(arrivalOrDepartureComboBox.Enabled, flightAttributesList, currentArrOrDepSelection);

            previousArrOrDep = currentArrOrDepSelection;

            enableParametersByFlowType(currentArrOrDepSelection);
        }
                
        private void enableParametersByFlowType(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG currentArrOrDepSelection)
        {
            bool enableDeparture = (currentArrOrDepSelection == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.D);            
            enableParameterLabelAndTextBoxByFlowType(labelOrigEcoPax, nbOrigEcoPaxTextBox, enableDeparture);
            enableParameterLabelAndTextBoxByFlowType(labelOrigFbPax, nbOrigFbPaxTextBox, enableDeparture);

            enableParameterLabelAndTextBoxByFlowType(labelTermEcoPax, nbTermEcoPaxTextBox, !enableDeparture);
            enableParameterLabelAndTextBoxByFlowType(labelTermFbPax, nbTermFbPaxTextBox, !enableDeparture);

            enableParameterLabelAndTextBoxByFlowType(labelOrigEcoBags, nbOriginatingEcoBagsTextBox, enableDeparture);
            enableParameterLabelAndTextBoxByFlowType(labelOrigFbBags, nbOriginatingFbBagsTextBox, enableDeparture);

            enableParameterLabelAndTextBoxByFlowType(labelTerminatingEcoBags, nbTerminatingEcoBagsTextBox, !enableDeparture);
            enableParameterLabelAndTextBoxByFlowType(labelTerminatingFbBags, nbTerminatingFbBagsTextBox, !enableDeparture);
        }
        private void enableParameterLabelAndTextBoxByFlowType(Label parameterLabel, TextBox parameterTextBox,
            bool enableParameter)
        {
            if (parameterLabel == null || parameterTextBox == null)
            {
                return;
            }
            parameterLabel.Enabled = enableParameter;
            parameterTextBox.Enabled = enableParameter;
            parameterTextBox.TextChanged -= parameterTextBox_TextChanged;
            if (!parameterTextBox.Enabled)
            {
                parameterTextBox.Text = "";
            }
            parameterTextBox.TextChanged += parameterTextBox_TextChanged;
        }

        private void enableAllParameters(TabControl flightPlanParametersTabControl)
        {
            if (flightPlanParametersTabControl == null)
            {
                return;
            }
            foreach (TabPage page in flightPlanParametersTabControl.TabPages)
            {
                foreach (Control control in page.Controls)
                {
                    control.Enabled = true;
                }
            }
        }

        private int previousFlightId = -1;
        private void flightIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!checkForUnsavedChanges())
            {
                return;
            }
            
            setCurrentFlightSelectionAttributesPanel(currentArrOrDepSelection, currentFlightIdSelection, flightAttributesList);
            setCurrentFlightSelectionParametersPanel(currentArrOrDepSelection, currentFlightIdSelection, tempFlightConfigurations);
            
            previousFlightId = currentFlightIdSelection;
            saveParameterButton.Enabled = false;
        }

        private void setCurrentFlightSelectionAttributesPanel(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG arrivalOrDeparture, int flightId, 
            List<FlightAttribute> flightAttributesList)
        {
            if (arrivalOrDeparture == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE || flightId < 0)
            {
                clearCurrentFlightSelectionAttributesPanel();
                return;
            }
            FlightAttribute attributesByCurrentFlightSelection = null;
            foreach (FlightAttribute attributes in flightAttributesList)
            {
                if (attributes.arrOrDep == arrivalOrDeparture && attributes.flightId == flightId)
                {
                    attributesByCurrentFlightSelection = attributes;
                    break;
                }
            }
            if (attributesByCurrentFlightSelection != null)
            {
                DateTime completeFlightDate = attributesByCurrentFlightSelection.flightDate.Add(attributesByCurrentFlightSelection.flightTime);
                flightDateTextBox.Text = completeFlightDate.ToString("dd/MM/yyyy HH:mm");
                airlineTextBox.Text = attributesByCurrentFlightSelection.airlineCode;
                airportTextBox.Text = attributesByCurrentFlightSelection.airportCode;

                flightNumberTextBox.Text = attributesByCurrentFlightSelection.flightNb;
                flightCategoryTextBox.Text = attributesByCurrentFlightSelection.flightCategory;
                aircraftTypeTextBox.Text = attributesByCurrentFlightSelection.aircraftType;
            }
            else
            {
                clearCurrentFlightSelectionAttributesPanel();
            }
        }
                
        private void clearCurrentFlightSelectionAttributesPanel()
        {
            flightDateTextBox.Text = "";
            airlineTextBox.Text = "";
            airportTextBox.Text = "";

            flightNumberTextBox.Text = "";
            flightCategoryTextBox.Text = "";
            aircraftTypeTextBox.Text = "";
        }

        private void setCurrentFlightSelectionParametersPanel(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG currentArrOrDepSelection, int currentFlightIdSelection, 
            List<FlightConfiguration> tempFlightConfigurations)
        {
            if (currentArrOrDepSelection == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE || currentFlightIdSelection == -1)
            {
                clearFlightSelectionParametersPanel();
                return;
            }
            FlightConfiguration currentFlightSelectionConfiguration = getFlightConfigurationFromFlightConfigurationsList(currentFlightIdSelection, 
                currentArrOrDepSelection, tempFlightConfigurations);
            if (currentFlightSelectionConfiguration != null)
            {
                fillFlightSelectionParametersPanel(currentFlightSelectionConfiguration.flightParameter);
            }
            else
            {
                clearFlightSelectionParametersPanel();
            }
        }

        private void fillFlightSelectionParametersPanel(FlightParameter flightParameter)
        {
            if (flightParameter == null)
            {                
                return;
            }
            nbOrigEcoPaxTextBox.Text = flightParameter.nbOrigEcoPax.ToString();
            nbOrigFbPaxTextBox.Text = flightParameter.nbOrigFbPax.ToString();

            nbTransferringEcoPaxTextBox.Text = flightParameter.nbTransferEcoPax.ToString();
            nbTransferringFbPaxTextBox.Text = flightParameter.nbTransferFbPax.ToString();

            nbTermEcoPaxTextBox.Text = flightParameter.nbTermEcoPax.ToString();
            nbTermFbPaxTextBox.Text = flightParameter.nbTermFbPax.ToString();

            nbOriginatingEcoBagsTextBox.Text = flightParameter.nbOrigEcoBags.ToString();
            nbOriginatingFbBagsTextBox.Text = flightParameter.nbOrigFbBags.ToString();

            nbTransferringEcoBagsTextBox.Text = flightParameter.nbTransferEcoBags.ToString();
            nbTransferringFbBagsTextBox.Text = flightParameter.nbTransferFbBags.ToString();

            nbTerminatingEcoBagsTextBox.Text = flightParameter.nbTermEcoBags.ToString();
            nbTerminatingFbBagsTextBox.Text = flightParameter.nbTermFbBags.ToString();
        }

        private void clearFlightSelectionParametersPanel()
        {
            nbOrigEcoPaxTextBox.Text = "";
            nbOrigFbPaxTextBox.Text = "";

            nbTransferringEcoPaxTextBox.Text = "";
            nbTransferringFbPaxTextBox.Text = "";

            nbTermEcoPaxTextBox.Text = "";
            nbTermFbPaxTextBox.Text = "";

            nbOriginatingEcoBagsTextBox.Text = "";
            nbOriginatingFbBagsTextBox.Text = "";

            nbTransferringEcoBagsTextBox.Text = "";
            nbTransferringFbBagsTextBox.Text = "";

            nbTerminatingEcoBagsTextBox.Text = "";
            nbTerminatingFbBagsTextBox.Text = "";
        }

        private FlightParameter getFlightParameterFromEditor(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG flightArrOrDep)
        {
            FlightParameter inputFlightParameter = new FlightParameter();
            if (flightArrOrDep == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.D)
            {
                inputFlightParameter.nbOrigEcoPax = getNumberFromTextBox(nbOrigEcoPaxTextBox);
                inputFlightParameter.nbOrigFbPax = getNumberFromTextBox(nbOrigFbPaxTextBox);

                inputFlightParameter.nbOrigEcoBags = getNumberFromTextBox(nbOriginatingEcoBagsTextBox);
                inputFlightParameter.nbOrigFbBags = getNumberFromTextBox(nbOriginatingFbBagsTextBox);

                inputFlightParameter.nbTermEcoPax = 0;
                inputFlightParameter.nbTermFbPax = 0;

                inputFlightParameter.nbTermEcoBags = 0;
                inputFlightParameter.nbTermFbBags = 0;
            }
            else if (flightArrOrDep == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.A)
            {
                inputFlightParameter.nbOrigEcoPax = 0;
                inputFlightParameter.nbOrigFbPax = 0;

                inputFlightParameter.nbTermEcoPax = getNumberFromTextBox(nbTermEcoPaxTextBox);
                inputFlightParameter.nbTermFbPax = getNumberFromTextBox(nbTermFbPaxTextBox);

                inputFlightParameter.nbOrigEcoBags = 0;
                inputFlightParameter.nbOrigFbBags = 0;

                inputFlightParameter.nbTermEcoBags = getNumberFromTextBox(nbTerminatingEcoBagsTextBox);
                inputFlightParameter.nbTermFbBags = getNumberFromTextBox(nbTerminatingFbBagsTextBox);
            }
            inputFlightParameter.nbTransferEcoPax = getNumberFromTextBox(nbTransferringEcoPaxTextBox);
            inputFlightParameter.nbTransferFbPax = getNumberFromTextBox(nbTransferringFbPaxTextBox);
                                    
            inputFlightParameter.nbTransferEcoBags = getNumberFromTextBox(nbTransferringEcoBagsTextBox);
            inputFlightParameter.nbTransferFbBags = getNumberFromTextBox(nbTransferringFbBagsTextBox);

            return inputFlightParameter;
        }

        private static double getNumberFromTextBox(TextBox textBox)
        {
            double result = double.NaN;
            if (textBox == null || !double.TryParse(textBox.Text, out result))
            {
                //when trying to parse the result variable might be changed when the parse fails => we reset it to NaN
                result = double.NaN;
            }
            return result;
        }

        private void parameterTextBox_FocusOut(object sender, EventArgs e)
        {
            TextBox senderTextBox = (TextBox)sender;            
            if (sender == null)
            {
                return;
            }
            double result = -1;
            if (!double.TryParse(senderTextBox.Text, out result))
            {
                MessageBox.Show("Invalid input: \"" + senderTextBox.Text + "\". Please input a number.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderTextBox.Text = "0";
                return;
            }
        }

        private void parameterTextBox_TextChanged(object sender, EventArgs e)
        {
            saveParameterButton.Enabled = true;
        }

        private void updateParameterButton_Click(object sender, EventArgs e)
        {
            if (multiEditCheckBox.Checked)
            {
                bool saveOk = true;
                foreach (FlightAttribute flightAttribute in filteredFlightAttributesList)
                {
                    if (!saveOrUpdateFlightConfiguration(flightAttribute.flightId, flightAttribute.arrOrDep, tempFlightConfigurations))
                    {
                        saveOk = false;
                    }
                }                
                saveParameterButton.Enabled = !saveOk;                
            }
            else
            {
                saveParameterButton.Enabled = !saveOrUpdateFlightConfiguration(currentFlightIdSelection, currentArrOrDepSelection, tempFlightConfigurations);
            }
        }

        private bool saveOrUpdateFlightConfiguration(int flightId, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG flightArrOrDep,
            List<FlightConfiguration> tempFlightConfigurations)
        {
            FlightAttribute flightArttribute = getFlightAttributeFromFlightAttributesList(flightId, flightArrOrDep, flightAttributesList);
            if (flightArttribute == null)
            {
                // no flight with the given id and A/D exists in the FPA/D loaded when entering the editor
                MessageBox.Show("The " + flightArrOrDep + " flight " + flightId + " could not be found in the corresponding Flight Plan. Please check the Flight Plans.", 
                    "Flight not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FlightParameter inputFlightParameter = getFlightParameterFromEditor(flightArrOrDep);
            if (inputFlightParameter.hasNonNumericalFields)
            {
                MessageBox.Show("Some parameters are non-numerical. Please update.", "Invalid parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            FlightConfiguration flightConfigurationFromTempList = getFlightConfigurationFromFlightConfigurationsList(flightId, flightArrOrDep, tempFlightConfigurations);
            if (flightConfigurationFromTempList != null)
            {
                flightConfigurationFromTempList.flightParameter.updateParameterByGivenFlightParameter(inputFlightParameter);
            }
            else
            {
                FlightConfiguration newFlightConfiguration = new FlightConfiguration(flightArttribute, inputFlightParameter);
                tempFlightConfigurations.Add(newFlightConfiguration);                
            }
            return true;
        }

        private FlightAttribute getFlightAttributeFromFlightAttributesList(int givenFlightId, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG givenFlightArrOrDep,
            List<FlightAttribute> flightAttributesList)
        {
            FlightAttribute flightAttribute = null;
            if (flightAttributesList == null)
            {
                return flightAttribute;
            }
            foreach (FlightAttribute givenFlightAttribute in flightAttributesList)
            {
                if (givenFlightAttribute.flightId == givenFlightId
                    && givenFlightAttribute.arrOrDep == givenFlightArrOrDep)
                {
                    flightAttribute = givenFlightAttribute;
                    break;
                }
            }
            return flightAttribute;
        }

        private FlightConfiguration getFlightConfigurationFromFlightConfigurationsList(int givenFlightId, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG givenFlightArrOrDep,
            List<FlightConfiguration> flightConfigurationsList)
        {
            FlightConfiguration flightConfiguration = null;
            if (flightConfigurationsList == null)
            {
                return flightConfiguration;
            }
            foreach (FlightConfiguration givenFlightConfiguration in flightConfigurationsList)
            {
                if (givenFlightConfiguration.flightAttribute.flightId == givenFlightId
                    && givenFlightConfiguration.flightAttribute.arrOrDep == givenFlightArrOrDep)
                {
                    flightConfiguration = givenFlightConfiguration;
                    break;
                }
            }
            return flightConfiguration;
        }

        private void resetFlightIdAndArrOrDepComboBoxes(int flightId, FlightAttribute.ARR_OR_DEP_FLIGHT_TAG arrOrDep)
        {
            arrivalOrDepartureComboBox.SelectedIndexChanged -= new EventHandler(arrivalOrDepartureComboBox_SelectedIndexChanged);
            if (arrivalOrDepartureComboBox.Items.Contains(arrOrDep))
            {
                arrivalOrDepartureComboBox.SelectedItem = arrOrDep;
            }
            arrivalOrDepartureComboBox.SelectedIndexChanged += new EventHandler(arrivalOrDepartureComboBox_SelectedIndexChanged);

            flightIdComboBox.SelectedIndexChanged -= new EventHandler(flightIdComboBox_SelectedIndexChanged);
            setFlightIdComboBox(arrivalOrDepartureComboBox.Enabled, flightAttributesList, currentArrOrDepSelection);
            if (flightIdComboBox.Items.Contains(flightId))
            {
                flightIdComboBox.SelectedItem = flightId;
            }
            flightIdComboBox.SelectedIndexChanged += new EventHandler(flightIdComboBox_SelectedIndexChanged);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!checkForUnsavedChanges())
            {
                DialogResult = DialogResult.None;
                return;
            }
            updateFlightPlanParametersTable(selectedFPParametersTable, tempFlightConfigurations);
        }
        
        private bool checkForUnsavedChanges()
        {
            if (multiEditCheckBox.Checked)
            {
                if (saveParameterButton.Enabled)
                {
                    DialogResult res = MessageBox.Show("The parameters changes for the selected flights are not saved. Do you want to save?",
                        "Save flight paremeters", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        bool saveOk = true;
                        foreach (FlightAttribute flightAttribute in filteredFlightAttributesList)
                        {
                            if (!saveOrUpdateFlightConfiguration(flightAttribute.flightId, flightAttribute.arrOrDep, tempFlightConfigurations))
                            {
                                saveOk = false;
                            }
                        }
                        saveParameterButton.Enabled = !saveOk;
                    }
                }
            }
            else
            {
                if (previousFlightId != -1 && previousArrOrDep != FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE
                    && saveParameterButton.Enabled)
                {
                    DialogResult res = MessageBox.Show("The parameters changes for flight " + previousArrOrDep + " " + previousFlightId
                            + " are not saved. Do you want to save?", "Save flight paremeters", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        if (!saveOrUpdateFlightConfiguration(previousFlightId, previousArrOrDep, tempFlightConfigurations))
                        {
                            resetFlightIdAndArrOrDepComboBoxes(previousFlightId, previousArrOrDep);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void updateFlightPlanParametersTable(DataTable flightPlanParametersTable, List<FlightConfiguration> tempFlightConfigurations)
        {
            if (flightPlanParametersTable == null || tempFlightConfigurations == null)
            {
                return;
            }
            #region column indexes
            List<int> indexes = new List<int>();

            #region flight attributes
            int columnIndexAorD = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_ARR_OR_DEP);
            indexes.Add(columnIndexAorD);
            int columnIndexFlightId = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_FLIGHT_ID);
            indexes.Add(columnIndexFlightId);

            int columnIndexFlightDate = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            indexes.Add(columnIndexFlightDate);
            int columnIndexFlightTime = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_FLIGHT_TIME);
            indexes.Add(columnIndexFlightTime);

            int columnIndexAirline = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            indexes.Add(columnIndexAirline);
            int columnIndexFlightNb = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            indexes.Add(columnIndexFlightNb);
            int columnIndexAirport = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            indexes.Add(columnIndexAirport);
            int columnIndexFlightCategory = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            indexes.Add(columnIndexFlightCategory);
            int columnIndexAircraft = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
            indexes.Add(columnIndexAircraft);

            int columnIndexTsa = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_Column_TSA);
            indexes.Add(columnIndexTsa);
            int columnIndexCbp = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPA_Column_CBP);
            indexes.Add(columnIndexCbp);
            int columnIndexNoBsm = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPA_Column_NoBSM);
            indexes.Add(columnIndexNoBsm);

            int columnIndexUser1 = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
            indexes.Add(columnIndexUser1);
            int columnIndexUser2 = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
            indexes.Add(columnIndexUser2);
            int columnIndexUser3 = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
            indexes.Add(columnIndexUser3);
            int columnIndexUser4 = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
            indexes.Add(columnIndexUser4);
            int columnIndexUser5 = flightPlanParametersTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_User5);
            indexes.Add(columnIndexUser5);
            #endregion

            #region flight parameters
            int columnIndexNbOrigEcoPax = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_ECO_PAX);
            indexes.Add(columnIndexNbOrigEcoPax);
            int columnIndexNbOrigFbPax = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_FB_PAX);
            indexes.Add(columnIndexNbOrigFbPax);

            int columnIndexNbTransferEcoPax = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_ECO_PAX);
            indexes.Add(columnIndexNbTransferEcoPax);
            int columnIndexNbTransferFbPax = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_FB_PAX);
            indexes.Add(columnIndexNbTransferFbPax);

            int columnIndexNbTermEcoPax = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TERM_ECO_PAX);
            indexes.Add(columnIndexNbTermEcoPax);
            int columnIndexNbTermFbPax = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TERM_FB_PAX);
            indexes.Add(columnIndexNbTermFbPax);

            int columnIndexNbOrigEcoBags = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_ECO_BAGS);
            indexes.Add(columnIndexNbOrigEcoBags);
            int columnIndexNbOrigFbBags = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_FB_BAGS);
            indexes.Add(columnIndexNbOrigFbBags);

            int columnIndexNbTransferEcoBags = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_ECO_BAGS);
            indexes.Add(columnIndexNbTransferEcoBags);
            int columnIndexNbTransferFbBags = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_FB_BAGS);
            indexes.Add(columnIndexNbTransferFbBags);

            int columnIndexNbTermEcoBags = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TERM_ECO_BAGS);
            indexes.Add(columnIndexNbTermEcoBags);
            int columnIndexNbTermFbBags = flightPlanParametersTable.Columns.IndexOf(FPParametersTableConstants.COLUMN_NAME_NB_TERM_FB_BAGS);
            indexes.Add(columnIndexNbTermFbBags);
            #endregion
            
            foreach (int index in indexes)
            {
                if (index == -1)
                {
                    return;
                }
            }
            #endregion

            if (editModeOnly)
            {
                foreach (FlightConfiguration flightConfiguration in tempFlightConfigurations)
                {
                    foreach (DataRow row in flightPlanParametersTable.Rows)
                    {
                        int flightIdFromRow = -1;
                        if (row[columnIndexFlightId] == null || row[columnIndexAorD] == null || !int.TryParse(row[columnIndexFlightId].ToString(), out flightIdFromRow))
                        {
                            continue;
                        }
                        FlightAttribute.ARR_OR_DEP_FLIGHT_TAG flightArrOrDepFromRow = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE;
                        try
                        {
                            flightArrOrDepFromRow = (FlightAttribute.ARR_OR_DEP_FLIGHT_TAG)Enum
                                .Parse(typeof(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG), row[columnIndexAorD].ToString());
                        }
                        catch (Exception ex)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("Error while retrieving the selected Flight Plan parameter from the table \""
                                + flightPlanParametersTable.TableName + "\". " + ex.Message);
                            continue;
                        }
                        if (flightArrOrDepFromRow == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE)
                        {
                            continue;
                        }
                        if (flightIdFromRow == flightConfiguration.flightAttribute.flightId && flightArrOrDepFromRow == flightConfiguration.flightAttribute.arrOrDep)
                        {
                            row[columnIndexNbOrigEcoPax] = flightConfiguration.flightParameter.nbOrigEcoPax;
                            row[columnIndexNbOrigFbPax] = flightConfiguration.flightParameter.nbOrigFbPax;

                            row[columnIndexNbTransferEcoPax] = flightConfiguration.flightParameter.nbTransferEcoPax;
                            row[columnIndexNbTransferFbPax] = flightConfiguration.flightParameter.nbTransferFbPax;

                            row[columnIndexNbTermEcoPax] = flightConfiguration.flightParameter.nbTermEcoPax;
                            row[columnIndexNbTermFbPax] = flightConfiguration.flightParameter.nbTermFbPax;

                            row[columnIndexNbOrigEcoBags] = flightConfiguration.flightParameter.nbOrigEcoBags;
                            row[columnIndexNbOrigFbBags] = flightConfiguration.flightParameter.nbOrigFbBags;

                            row[columnIndexNbTransferEcoBags] = flightConfiguration.flightParameter.nbTransferEcoBags;
                            row[columnIndexNbTransferFbBags] = flightConfiguration.flightParameter.nbTransferFbBags;

                            row[columnIndexNbTermEcoBags] = flightConfiguration.flightParameter.nbTermEcoBags;
                            row[columnIndexNbTermFbBags] = flightConfiguration.flightParameter.nbTermFbBags;

                            break;
                        }
                    }
                }
            }
            else
            {
                flightPlanParametersTable.Rows.Clear();
                foreach (FlightConfiguration flightConfiguration in tempFlightConfigurations)
                {
                    DataRow newRow = flightPlanParametersTable.NewRow();

                    newRow[columnIndexAorD] = flightConfiguration.flightAttribute.arrOrDep;
                    newRow[columnIndexFlightId] = flightConfiguration.flightAttribute.flightId;

                    newRow[columnIndexFlightDate] = flightConfiguration.flightAttribute.flightDate;
                    newRow[columnIndexFlightTime] = flightConfiguration.flightAttribute.flightTime;
                    
                    newRow[columnIndexAirline] = flightConfiguration.flightAttribute.airlineCode;
                    newRow[columnIndexFlightNb] = flightConfiguration.flightAttribute.flightNb;
                    newRow[columnIndexAirport] = flightConfiguration.flightAttribute.airportCode;
                    newRow[columnIndexFlightCategory] = flightConfiguration.flightAttribute.flightCategory;
                    newRow[columnIndexAircraft] = flightConfiguration.flightAttribute.aircraftType;

                    newRow[columnIndexTsa] = flightConfiguration.flightAttribute.tsa;
                    newRow[columnIndexCbp] = flightConfiguration.flightAttribute.cbp;
                    newRow[columnIndexNoBsm] = flightConfiguration.flightAttribute.noBsm;

                    newRow[columnIndexUser1] = flightConfiguration.flightAttribute.user1;
                    newRow[columnIndexUser2] = flightConfiguration.flightAttribute.user2;
                    newRow[columnIndexUser3] = flightConfiguration.flightAttribute.user3;
                    newRow[columnIndexUser4] = flightConfiguration.flightAttribute.user4;
                    newRow[columnIndexUser5] = flightConfiguration.flightAttribute.user5;

                    newRow[columnIndexNbOrigEcoPax] = flightConfiguration.flightParameter.nbOrigEcoPax;
                    newRow[columnIndexNbOrigFbPax] = flightConfiguration.flightParameter.nbOrigFbPax;

                    newRow[columnIndexNbTransferEcoPax] = flightConfiguration.flightParameter.nbTransferEcoPax;
                    newRow[columnIndexNbTransferFbPax] = flightConfiguration.flightParameter.nbTransferFbPax;

                    newRow[columnIndexNbTermEcoPax] = flightConfiguration.flightParameter.nbTermEcoPax;
                    newRow[columnIndexNbTermFbPax] = flightConfiguration.flightParameter.nbTermFbPax;

                    newRow[columnIndexNbOrigEcoBags] = flightConfiguration.flightParameter.nbOrigEcoBags;
                    newRow[columnIndexNbOrigFbBags] = flightConfiguration.flightParameter.nbOrigFbBags;

                    newRow[columnIndexNbTransferEcoBags] = flightConfiguration.flightParameter.nbTransferEcoBags;
                    newRow[columnIndexNbTransferFbBags] = flightConfiguration.flightParameter.nbTransferFbBags;

                    newRow[columnIndexNbTermEcoBags] = flightConfiguration.flightParameter.nbTermEcoBags;
                    newRow[columnIndexNbTermFbBags] = flightConfiguration.flightParameter.nbTermFbBags;

                    flightPlanParametersTable.Rows.Add(newRow);
                }
            }
            flightPlanParametersTable.AcceptChanges();
        }

        private void multiEditCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            selectionPanel.Enabled = !multiEditCheckBox.Checked;
            flightAttributesPanel.Enabled = !multiEditCheckBox.Checked;
            multiEditPanel.Enabled = multiEditCheckBox.Checked;

            clearCurrentFlightSelectionAttributesPanel();
            clearPreviewDataGrid();

            if (multiEditCheckBox.Checked)
            {
                enableAllParameters(flightParametersTabControl);
            }
            else
            {
                enableParametersByFlowType(currentArrOrDepSelection);
            }
        }

        FlightGroupingRule previousFlightGroupingRule = null;
        private void filterFlightsButton_Click(object sender, EventArgs e)
        {
            Dictionary<GeneralAllocationConstants.P2S_ATTRIBUTES, List<string>> flightPlanData = PAX2SIM.getFlightPlanData(rootFPA, rootFPD);
            FlightGroupingRulesAssistant flightGroupingRulesAssistant = new FlightGroupingRulesAssistant(flightPlanData, previousFlightGroupingRule);
            if (flightGroupingRulesAssistant.ShowDialog() == DialogResult.OK)
            {
                previousFlightGroupingRule = flightGroupingRulesAssistant.resultedFlightGroupingRule;
                //filter the flightAttributesList using the rules
                filteredFlightAttributesList = filterFlightAttributesList(flightAttributesList, flightGroupingRulesAssistant.resultedFlightGroupingRule);
                //update the preview text box
                setUpPreviewDataGrid(filteredFlightAttributesList);
                saveParameterButton.Enabled = true;
            }
        }

        private void setUpPreviewDataGrid(List<FlightAttribute> filteredFlightAttributesList)
        {
            DataTable source = new DataTable();
            #region column indexes
            int columnIndexAD = source.Columns.Count;
            source.Columns.Add("A/D", typeof(string));
            
            int columnIndexID = source.Columns.Count;
            source.Columns.Add("ID", typeof(int));

            int columnIndexDate = source.Columns.Count;
            source.Columns.Add("Date", typeof(DateTime));

            int columnIndexAirline = source.Columns.Count;
            source.Columns.Add("Airline", typeof(string));

            int columnIndexFlightNb = source.Columns.Count;
            source.Columns.Add("Flight Nb", typeof(string));

            int columnIndexAirport = source.Columns.Count;
            source.Columns.Add("Airport", typeof(string));

            int columnIndexFlightCategory = source.Columns.Count;
            source.Columns.Add("Flight category", typeof(string));

            int columnIndexAircraft = source.Columns.Count;
            source.Columns.Add("Aircraft", typeof(string));

            int columnIndexUser1 = source.Columns.Count;
            source.Columns.Add("User1", typeof(string));
            
            int columnIndexUser2 = source.Columns.Count;
            source.Columns.Add("User2", typeof(string));
            
            int columnIndexUser3 = source.Columns.Count;
            source.Columns.Add("User3", typeof(string));
            
            int columnIndexUser4 = source.Columns.Count;
            source.Columns.Add("User4", typeof(string));
            
            int columnIndexUser5 = source.Columns.Count;
            source.Columns.Add("User5", typeof(string));
            #endregion

            foreach (FlightAttribute flightAttribute in filteredFlightAttributesList)
            {
                DataRow newRow = source.NewRow();
                
                #region data
                newRow[columnIndexAD] = flightAttribute.arrOrDep;
                newRow[columnIndexID] = flightAttribute.flightId;

                DateTime completeDate = flightAttribute.flightDate.Add(flightAttribute.flightTime);
                newRow[columnIndexDate] = completeDate;

                newRow[columnIndexAirline] = flightAttribute.airlineCode;
                newRow[columnIndexFlightNb] = flightAttribute.flightNb;
                newRow[columnIndexAirport] = flightAttribute.airportCode;
                newRow[columnIndexFlightCategory] = flightAttribute.flightCategory;
                newRow[columnIndexAircraft] = flightAttribute.aircraftType;

                newRow[columnIndexUser1] = flightAttribute.user1;
                newRow[columnIndexUser2] = flightAttribute.user2;
                newRow[columnIndexUser3] = flightAttribute.user3;
                newRow[columnIndexUser4] = flightAttribute.user4;
                newRow[columnIndexUser5] = flightAttribute.user5;
                #endregion

                source.Rows.Add(newRow);
            }
            previewDataGridView.DataSource = source;

            previewDataGridView.Columns["A/D"].Frozen = true;
            previewDataGridView.Columns["ID"].Frozen = true;
        }

        private void clearPreviewDataGrid()
        {
            previewDataGridView.DataSource = null;
        }

        private List<FlightAttribute> filterFlightAttributesList(List<FlightAttribute> flightAttributesList, FlightGroupingRule flightGroupingRule)
        {
            List<FlightAttribute> filteredFlightAttributesList = new List<FlightAttribute>();
            foreach (FlightAttribute flightAttribute in flightAttributesList)
            {
                if (flightAttributeFollowsFlightGroupingRule(flightAttribute, flightGroupingRule))
                {
                    filteredFlightAttributesList.Add(flightAttribute);
                }
            }
            return filteredFlightAttributesList;
        }

        private bool flightAttributeFollowsFlightGroupingRule(FlightAttribute flightAttribute, FlightGroupingRule flightGroupingRule)
        {
            bool followsRule = false;
            foreach (FlightGroupingRuleCondition condition in flightGroupingRule.ruleConditions)
            {
                if (GeneralAllocationConstants.INTEGER_P2S_ATTRIBUTES.Contains(condition.p2sAttribute))
                {
                    int flightAttributeIntegerValue = Tools.FlightGroupingRulesCheck.getIntegerValueByP2SAttribute(condition.p2sAttribute, flightAttribute);
                    followsRule = Tools.FlightGroupingRulesCheck.flightAttributeIntegerValueFollowsFlightGroupingRuleCondition(flightAttributeIntegerValue, condition);
                    if (!followsRule)
                    {   
                        break;
                    }
                }
                else if (GeneralAllocationConstants.STRING_P2S_ATTRIBUTES.Contains(condition.p2sAttribute))
                {
                    string flightAttributeStringValue = Tools.FlightGroupingRulesCheck.getStringValueByP2SAttribute(condition.p2sAttribute, flightAttribute);
                    followsRule = Tools.FlightGroupingRulesCheck.flightAttributeStringValueFollowsFlightGroupingRuleCondition(flightAttributeStringValue, condition);
                    if (!followsRule)
                    {
                        break;
                    }
                }
            }
            return followsRule;
        }

    }
}
