using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace SIMCORE_TOOL.com.crispico.general_allocation.constants
{
    public static class GeneralAllocationConstants
    {
        internal enum P2S_ATTRIBUTES
        {
            [Description("Default value")]
            DEFAULT,   //the default value - it is always the 1st value of a C# enumeration
            [Description("FPA or FPD")]
            ARR_OR_DEP,
            [Description("Flight id")]
            FLIGHT_ID,
            [Description("Airline code")]
            AIRLINE,
            [Description("Flight number")]
            FLIGHT_NUMBER,
            [Description("Airport code")]
            AIRPORT,
            [Description("Flight category")]
            FLIGHT_CATEGORY,
            [Description("Aircraft type")]
            AIRCRAFT_TYPE,
            [Description("User 1")]
            USER_1,
            [Description("User 2")]
            USER_2,
            [Description("User 3")]
            USER_3,
            [Description("User 4")]
            USER_4,
            [Description("User 5")]
            USER_5
        };

        internal static List<P2S_ATTRIBUTES> INTEGER_P2S_ATTRIBUTES = new List<P2S_ATTRIBUTES> { P2S_ATTRIBUTES.FLIGHT_ID };
        internal static List<P2S_ATTRIBUTES> STRING_P2S_ATTRIBUTES = new List<P2S_ATTRIBUTES> { P2S_ATTRIBUTES.ARR_OR_DEP, P2S_ATTRIBUTES.AIRLINE, P2S_ATTRIBUTES.FLIGHT_NUMBER,
            P2S_ATTRIBUTES.AIRPORT, P2S_ATTRIBUTES.FLIGHT_CATEGORY, P2S_ATTRIBUTES.AIRCRAFT_TYPE, P2S_ATTRIBUTES.USER_1, P2S_ATTRIBUTES.USER_2, P2S_ATTRIBUTES.USER_3,
            P2S_ATTRIBUTES.USER_4, P2S_ATTRIBUTES.USER_5};

        internal enum OPERATORS
        {
            [Description("Default value")]
            DEFAULT,   //the default value - it is always the 1st value of a C# enumeration
            [Description("=")]
            EQUAL_TO,
            [Description("Contains")]
            CONTAINS,
            [Description("Starts with")]
            STARTS_WITH,
            [Description("Ends with")]
            ENDS_WITH
        };

        public static string getDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }
            //Tries to find a DescriptionAttribute for a potential friendly name for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }

        public static T getValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {                
                return default(T);
            }
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }            
            return default(T);
        }

        internal static Dictionary<P2S_ATTRIBUTES, string> p2sAttributesFlightPlanColumnsDictionary = new Dictionary<P2S_ATTRIBUTES, string>()
        {
            {P2S_ATTRIBUTES.FLIGHT_ID, GlobalNames.sFPD_A_Column_ID}, {P2S_ATTRIBUTES.AIRLINE, GlobalNames.sFPD_A_Column_AirlineCode}, 
            {P2S_ATTRIBUTES.FLIGHT_NUMBER, GlobalNames.sFPD_A_Column_FlightN}, {P2S_ATTRIBUTES.AIRPORT, GlobalNames.sFPD_A_Column_AirportCode}, 
            {P2S_ATTRIBUTES.FLIGHT_CATEGORY, GlobalNames.sFPD_A_Column_FlightCategory}, {P2S_ATTRIBUTES.AIRCRAFT_TYPE, GlobalNames.sFPD_A_Column_AircraftType}, 
            {P2S_ATTRIBUTES.USER_1, GlobalNames.sFPD_A_Column_User1}, {P2S_ATTRIBUTES.USER_2, GlobalNames.sFPD_A_Column_User2}, 
            {P2S_ATTRIBUTES.USER_3, GlobalNames.sFPD_A_Column_User3}, {P2S_ATTRIBUTES.USER_4, GlobalNames.sFPD_A_Column_User4}, 
            {P2S_ATTRIBUTES.USER_5, GlobalNames.sFPD_A_Column_User5}
        };

        internal static Dictionary<string, P2S_ATTRIBUTES> flightPlanColumnsP2sAttributesDictionary = new Dictionary<string, P2S_ATTRIBUTES>()
        {
            {GlobalNames.sFPD_A_Column_ID, P2S_ATTRIBUTES.FLIGHT_ID}, {GlobalNames.sFPD_A_Column_AirlineCode, P2S_ATTRIBUTES.AIRLINE}, 
            {GlobalNames.sFPD_A_Column_FlightN, P2S_ATTRIBUTES.FLIGHT_NUMBER}, {GlobalNames.sFPD_A_Column_AirportCode, P2S_ATTRIBUTES.AIRPORT}, 
            {GlobalNames.sFPD_A_Column_FlightCategory, P2S_ATTRIBUTES.FLIGHT_CATEGORY}, {GlobalNames.sFPD_A_Column_AircraftType, P2S_ATTRIBUTES.AIRCRAFT_TYPE}, 
            {GlobalNames.sFPD_A_Column_User1, P2S_ATTRIBUTES.USER_1}, {GlobalNames.sFPD_A_Column_User2, P2S_ATTRIBUTES.USER_2}, 
            {GlobalNames.sFPD_A_Column_User3, P2S_ATTRIBUTES.USER_3}, {GlobalNames.sFPD_A_Column_User4, P2S_ATTRIBUTES.USER_4}, 
            {GlobalNames.sFPD_A_Column_User5, P2S_ATTRIBUTES.USER_5}
        };


    }
}
