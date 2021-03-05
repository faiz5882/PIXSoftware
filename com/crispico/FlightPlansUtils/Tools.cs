using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.com.crispico.general_allocation.constants;
using SIMCORE_TOOL.com.crispico.general_allocation.parameter;
using System.Data;

namespace SIMCORE_TOOL.com.crispico.FlightPlansUtils
{
    internal static class Tools
    {
        internal static class FlightGroupingRulesCheck
        {
            internal const int DEFAULT_INTEGER_VALUE = int.MinValue;
            internal static int getIntegerValueByP2SAttribute(GeneralAllocationConstants.P2S_ATTRIBUTES p2sAttribute, FlightAttribute flightAttribute)
            {
                if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.FLIGHT_ID)
                {
                    return flightAttribute.flightId;
                }
                return DEFAULT_INTEGER_VALUE;
            }

            internal const string DEFAULT_STRING_VALUE = "DEFAULT_EXCEPTION_VALUE";
            internal static string getStringValueByP2SAttribute(GeneralAllocationConstants.P2S_ATTRIBUTES p2sAttribute, FlightAttribute flightAttribute)
            {
                if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.ARR_OR_DEP)
                {
                    return flightAttribute.arrOrDep.ToString();
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.AIRCRAFT_TYPE)
                {
                    return flightAttribute.aircraftType;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.AIRLINE)
                {
                    return flightAttribute.airlineCode;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.AIRPORT)
                {
                    return flightAttribute.airportCode;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.FLIGHT_CATEGORY)
                {
                    return flightAttribute.flightCategory;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.FLIGHT_NUMBER)
                {
                    return flightAttribute.flightNb;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.USER_1)
                {
                    return flightAttribute.user1;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.USER_2)
                {
                    return flightAttribute.user2;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.USER_3)
                {
                    return flightAttribute.user3;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.USER_4)
                {
                    return flightAttribute.user4;
                }
                else if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.USER_5)
                {
                    return flightAttribute.user5;
                }
                return DEFAULT_STRING_VALUE;
            }

            internal static bool flightAttributeIntegerValueFollowsFlightGroupingRuleCondition(int flightAttributeValue, FlightGroupingRuleCondition condition)
            {
                bool followsRule = false;
                if (condition == null || flightAttributeValue == DEFAULT_INTEGER_VALUE)
                {
                    return followsRule;
                }
                if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.EQUAL_TO)
                {
                    foreach (string inputValue in condition.inputValues)
                    {
                        int inputValueAsInt = -1;
                        if (int.TryParse(inputValue, out inputValueAsInt) && flightAttributeValue == inputValueAsInt)
                        {
                            followsRule = true;
                            break;
                        }
                    }
                }
                else if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.CONTAINS)
                {
                    foreach (string inputValue in condition.inputValues)
                    {
                        if (flightAttributeValue.ToString().Contains(inputValue))
                        {
                            followsRule = true;
                            break;
                        }
                    }
                }
                else if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.STARTS_WITH)
                {
                    foreach (string inputValue in condition.inputValues)
                    {
                        if (flightAttributeValue.ToString().StartsWith(inputValue))
                        {
                            followsRule = true;
                            break;
                        }
                    }
                }
                else if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.ENDS_WITH)
                {
                    foreach (string inputValue in condition.inputValues)
                    {
                        if (flightAttributeValue.ToString().EndsWith(inputValue))
                        {
                            followsRule = true;
                            break;
                        }
                    }
                }
                return followsRule;
            }

            internal static bool flightAttributeStringValueFollowsFlightGroupingRuleCondition(string flightAttributeValue, FlightGroupingRuleCondition condition)
            {
                bool followsRule = false;
                if (condition == null || flightAttributeValue == DEFAULT_STRING_VALUE)
                {
                    return followsRule;
                }
                if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.EQUAL_TO)
                {
                    foreach (string inputValue in condition.inputValues)
                    {
                        if (flightAttributeValue == inputValue)
                        {
                            followsRule = true;
                            break;
                        }
                    }
                }
                else if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.CONTAINS)
                {
                    foreach (string inputValue in condition.inputValues)
                    {
                        if (flightAttributeValue.Contains(inputValue))
                        {
                            followsRule = true;
                            break;
                        }
                    }
                }
                else if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.STARTS_WITH)
                {
                    foreach (string inputValue in condition.inputValues)
                    {
                        if (flightAttributeValue.StartsWith(inputValue))
                        {
                            followsRule = true;
                            break;
                        }
                    }
                }
                else if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.ENDS_WITH)
                {
                    foreach (string inputValue in condition.inputValues)
                    {
                        if (flightAttributeValue.EndsWith(inputValue))
                        {
                            followsRule = true;
                            break;
                        }
                    }
                }
                return followsRule;
            }
        }

        internal static class FlightPlanParametersTools
        {
            internal static List<FlightConfiguration> getFlightConfigurationsListFromFPParametersTable(DataTable flightPlanParametersTable)
            {
                List<FlightConfiguration> configurations = new List<FlightConfiguration>();
                if (flightPlanParametersTable == null)
                {
                    return configurations;
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
                        return configurations;
                    }
                }
                #endregion

                foreach (DataRow row in flightPlanParametersTable.Rows)
                {
                    FlightConfiguration configuration = new FlightConfiguration();

                    FlightAttribute flightAttribute = new FlightAttribute();
                    configuration.flightAttribute = flightAttribute;

                    #region flight attributes
                    int flightId = -1;
                    if (row[columnIndexFlightId] == null || row[columnIndexAorD] == null || !int.TryParse(row[columnIndexFlightId].ToString(), out flightId))
                    {
                        continue;
                    }
                    flightAttribute.flightId = flightId;

                    FlightAttribute.ARR_OR_DEP_FLIGHT_TAG flightArrOrDep = FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE;
                    try
                    {
                        flightArrOrDep = (FlightAttribute.ARR_OR_DEP_FLIGHT_TAG)Enum
                            .Parse(typeof(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG), row[columnIndexAorD].ToString());
                    }
                    catch (Exception ex)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Error while retrieving the selected Flight Plan parameter from the table \""
                            + flightPlanParametersTable.TableName + "\". " + ex.Message);
                        continue;
                    }
                    if (flightArrOrDep == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE)
                    {
                        continue;
                    }
                    flightAttribute.arrOrDep = flightArrOrDep;

                    DateTime flightDate = DateTime.MinValue;
                    if (row[columnIndexFlightDate] == null || !DateTime.TryParse(row[columnIndexFlightDate].ToString(), out flightDate))
                    {
                        continue;
                    }
                    flightAttribute.flightDate = flightDate;

                    TimeSpan flightTime = TimeSpan.MinValue;
                    if (row[columnIndexFlightTime] == null || !TimeSpan.TryParse(row[columnIndexFlightTime].ToString(), out flightTime))
                    {
                        continue;
                    }
                    flightAttribute.flightTime = flightTime;

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
                    #region tsa, cbp, no bsm
                    bool value;
                    if (row[columnIndexTsa] != null && bool.TryParse(row[columnIndexTsa].ToString(), out value))
                    {
                        flightAttribute.tsa = value;
                    }
                    if (row[columnIndexCbp] != null && bool.TryParse(row[columnIndexCbp].ToString(), out value))
                    {
                        flightAttribute.cbp = value;
                    }
                    if (row[columnIndexNoBsm] != null && bool.TryParse(row[columnIndexNoBsm].ToString(), out value))
                    {
                        flightAttribute.noBsm = value;
                    }
                    #endregion

                    #region user 1..5
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

                    #endregion

                    FlightParameter flightParameter = new FlightParameter();
                    configuration.flightParameter = flightParameter;

                    #region flight parameters
                    double paramValue;
                    #region pax params
                    if (row[columnIndexNbOrigEcoPax] != null && double.TryParse(row[columnIndexNbOrigEcoPax].ToString(), out paramValue))
                    {
                        flightParameter.nbOrigEcoPax = paramValue;
                    }
                    if (row[columnIndexNbOrigFbPax] != null && double.TryParse(row[columnIndexNbOrigFbPax].ToString(), out paramValue))
                    {
                        flightParameter.nbOrigFbPax = paramValue;
                    }

                    if (row[columnIndexNbTransferEcoPax] != null && double.TryParse(row[columnIndexNbTransferEcoPax].ToString(), out paramValue))
                    {
                        flightParameter.nbTransferEcoPax = paramValue;
                    }
                    if (row[columnIndexNbTransferFbPax] != null && double.TryParse(row[columnIndexNbTransferFbPax].ToString(), out paramValue))
                    {
                        flightParameter.nbTransferFbPax = paramValue;
                    }

                    if (row[columnIndexNbTermEcoPax] != null && double.TryParse(row[columnIndexNbTermEcoPax].ToString(), out paramValue))
                    {
                        flightParameter.nbTermEcoPax = paramValue;
                    }
                    if (row[columnIndexNbTermFbPax] != null && double.TryParse(row[columnIndexNbTermFbPax].ToString(), out paramValue))
                    {
                        flightParameter.nbTermFbPax = paramValue;
                    }
                    #endregion
                    #region bags params
                    if (row[columnIndexNbOrigEcoBags] != null && double.TryParse(row[columnIndexNbOrigEcoBags].ToString(), out paramValue))
                    {
                        flightParameter.nbOrigEcoBags = paramValue;
                    }
                    if (row[columnIndexNbOrigFbBags] != null && double.TryParse(row[columnIndexNbOrigFbBags].ToString(), out paramValue))
                    {
                        flightParameter.nbOrigFbBags = paramValue;
                    }

                    if (row[columnIndexNbTransferEcoBags] != null && double.TryParse(row[columnIndexNbTransferEcoBags].ToString(), out paramValue))
                    {
                        flightParameter.nbTransferEcoBags = paramValue;
                    }
                    if (row[columnIndexNbTransferFbBags] != null && double.TryParse(row[columnIndexNbTransferFbBags].ToString(), out paramValue))
                    {
                        flightParameter.nbTransferFbBags = paramValue;
                    }

                    if (row[columnIndexNbTermEcoBags] != null && double.TryParse(row[columnIndexNbTermEcoBags].ToString(), out paramValue))
                    {
                        flightParameter.nbTermEcoBags = paramValue;
                    }
                    if (row[columnIndexNbTermFbBags] != null && double.TryParse(row[columnIndexNbTermFbBags].ToString(), out paramValue))
                    {
                        flightParameter.nbTermFbBags = paramValue;
                    }
                    #endregion
                    #endregion

                    configurations.Add(configuration);
                }
                return configurations;
            }

            internal static FlightConfiguration getFlightConfigurationByArrOrDepAndFlightId(FlightAttribute.ARR_OR_DEP_FLIGHT_TAG arrOrDep, int flightId,
                List<FlightConfiguration> flightConfigurations)
            {
                FlightConfiguration configuration = null;
                if (flightConfigurations == null || arrOrDep == FlightAttribute.ARR_OR_DEP_FLIGHT_TAG.NONE)
                {
                    return configuration;
                }
                foreach (FlightConfiguration givenConfig in flightConfigurations)
                {
                    if (givenConfig.flightAttribute == null || givenConfig.flightParameter == null)
                    {
                        continue;
                    }
                    if (givenConfig.flightAttribute.arrOrDep == arrOrDep 
                        && givenConfig.flightAttribute.flightId == flightId)
                    {
                        configuration = givenConfig;
                        break;
                    }
                }
                return configuration;
            }
        }

    }
}
