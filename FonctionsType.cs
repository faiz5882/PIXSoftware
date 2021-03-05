using System;
using System.Collections.Generic;
using System.Text;

namespace SIMCORE_TOOL
{
    internal class FonctionsType
    {
        #region Fonctions pour tester le type d'un objet. et convertir vers un type simple.
        #region Fonction de test de type
        internal static bool isDouble(Type type)
        {
            return (type == typeof(Double)) || (type == typeof(double));
        }
        internal static bool isInt(Type type)
        {
            return (type == typeof(int)) || (type == typeof(Int32)) || (type == typeof(Int16)) || (type == typeof(Int64));
        }
        internal static bool isByte(Type type)
        {
            return (type == typeof(byte));
        }
        internal static bool isDate(Type type)
        {
            return (type == typeof(DateTime));
        }
        internal static bool isTime(Type type)
        {
            return (type == typeof(TimeSpan));
        }
        internal static bool isBoolean(Type type)
        {
            return (type == typeof(Boolean)) || (type == typeof(bool));
        }
        internal static bool isString(Type type)
        {
            return (type == typeof(string)) || (type == typeof(String));
        }
        #endregion


        internal static Double getDouble(Object obj)
        {
            return getDouble(obj, obj.GetType());
        }
        internal static Double getDouble(Object obj, Type type)
        {
            if (isInt(obj.GetType()))
                return (Double)((int)obj);
            if (isString(obj.GetType()))
            {
                Double dResultat;
                if (!Double.TryParse(obj.ToString(), out dResultat))
                {
                    CastError(type.ToString(), "Double");
                    return 0;
                }
                return dResultat;
            }
            if (!isDouble(obj.GetType()))
            {
                CastError(type.ToString(), "Double");
                return 0;
            }
            return (Double)obj;
        }

        internal static int getInt(Object obj)
        {
            return getInt(obj, obj.GetType());
        }
        internal static int getInt(Object obj, Type type)
        {
            if (isDouble(obj.GetType()))
                return (int)(getDouble(obj, obj.GetType()));
            if (isString(obj.GetType()))
            {
                int iResultat;
                if (!Int32.TryParse(obj.ToString(), out iResultat))
                {
                    CastError(obj.GetType().ToString(), "int");
                    return 0;
                }
                return iResultat;
            }
            if (isByte(obj.GetType()))
            {
                return (int)((Byte)obj);
            }
            if (!isInt(obj.GetType()))
            {
                CastError(obj.GetType().ToString(), "int");
                return 0;
            }
            if (obj.GetType() == typeof(int))
                return (int)obj;
            if (obj.GetType() == typeof(Int32))
                return (Int32)obj;
            if (obj.GetType() == typeof(Int64))
                return (int)(Int64)obj;
            if (obj.GetType() == typeof(Int16))
                return (Int16)obj;
            CastError(obj.GetType().ToString(), "int");
            return 0;
        }
        internal static DateTime getDate(Object obj)
        {
            return getDate(obj, obj.GetType());
        }
        internal static DateTime getDate(Object obj, Type type)
        {
            if (isString(obj.GetType()))
            {
                DateTime dt;
                if (DateTime.TryParse((String)obj, out dt))
                    return dt;
                else
                {
                    CastError(obj.GetType().ToString(), "DateTime");
                    return DateTime.MinValue;
                }
            }
            if (!isDate(obj.GetType()))
            {
                CastError(obj.GetType().ToString(), "DateTime");
                return DateTime.MinValue;
            }
            return (DateTime)obj;
        }
        internal static TimeSpan getTime(Object obj)
        {
            return getTime(obj, obj.GetType());
        }
        internal static TimeSpan getTime(Object obj, Type type)
        {
            if (isString(obj.GetType()))
            {
                TimeSpan res;
                if(TimeSpan.TryParse(obj.ToString(), out res))
                    return res;
            }
            if (!isTime(obj.GetType()))
            {
                CastError(obj.GetType().ToString(), "TimeSpan");
                return TimeSpan.MinValue;
            }
            return (TimeSpan)obj;
        }
        internal static bool getBoolean(Object obj)
        {
            return getBoolean(obj, obj.GetType());
        }
        internal static bool getBoolean(Object obj, Type type)
        {
            if (type == typeof(String))
            {
                bool bResult;
                if (!Boolean.TryParse(obj.ToString(), out bResult))
                {
                    CastError(obj.GetType().ToString(), "bool");
                    return false;
                }
                return bResult;
            }
            if (!isBoolean(obj.GetType()))
            {
                CastError(obj.GetType().ToString(), "bool");
                return false;
            }
            return (bool)obj;
        }
        internal static String getString(Object obj)
        {
            return getString(obj, obj.GetType());
        }
        internal static String getString(Object obj, Type type)
        {
            if (isDate(obj.GetType()))
                return ((DateTime)obj).ToShortDateString();
            return obj.ToString();
        }

        internal static Object getType(Object obj, Type type)
        {
            if (isInt(type))
                return getInt(obj);
            if (isDouble(type))
                return getDouble(obj);

            if (isDate(type))
                return getDate(obj);
            if (isTime(type))
                return getTime(obj);
            if (isBoolean(type))
                return getBoolean(obj);
            if (isString(type))
                return getString(obj);
            return obj;
        }
        /// <summary>
        /// Fonction pour logger l'erreur de cast.
        /// </summary>
        /// <param name="fromType">Type d'origine</param>
        /// <param name="toType">Type de cast</param>
        private static void CastError(String fromType, String toType)
        {
            OverallTools.ExternFunctions.PrintLogFile("Err0221, Impossible to cast " + fromType + " to " + toType);
        }

        // << Task #8986 Pax2Sim - Filters - Expression functions
        internal static Double getSQRT(Object obj, Type type)
        {
            Double givenValue = getDouble(obj, type);
            return Math.Sqrt(givenValue);
        }

        internal static Double getSquare(Object obj, Type type)
        {
            Double givenValue = getDouble(obj, type);
            return Math.Pow(givenValue, 2);
        }
        // >> Task #8986 Pax2Sim - Filters - Expression functions

        // >> Task #10010 Pax2Sim - Filters - Ceiling function
        internal static Double getCeiling(Object obj, Type type)
        {
            Double givenValue = getDouble(obj, type);
            return Math.Ceiling(givenValue);
        }
        // << Task #10010 Pax2Sim - Filters - Ceiling function
        #endregion
    }
}
