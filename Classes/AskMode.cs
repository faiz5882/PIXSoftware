//#define UNIKEY_PROTECTION
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Globalization;

namespace SIMCORE_TOOL.Classes
{

    #region La classe qui se charge d'interroger la clef afin de voir quel est le mode de fonctionnement.
    internal class AskMode
    {

#if!(UNIKEY_PROTECTION)
        unsafe private Byte* bResults;
#else
        private Byte[] bResults;
#endif
        #region Les valeurs pouvant être prise et leur signification.

        private const Byte bFirstHUB = 0x01;
        private const Byte bFirstPAX = 0x02;
        private const Byte bFirstBHS = 0x04;
        private const Byte bFirstAirside = 0x08;
        private const Byte bFirstTMS = 0x10;
        private const Byte bFirstPKG = 0x20;
        private const Byte bFirstSodexi = 0x40;
        private const Byte bFirstNA3 = 0x80;

        private const Byte bSecondStatic = 0x01;
        private const Byte bSecondDynamic = 0x02;
        private const Byte bSecondRuntime = 0x04;
        private const Byte bSecondBHS_MeanFlows = 0x08;
        private const Byte bSecondBHS_PaxPlan = 0x10;
        private const Byte bSecondAllocation = 0x20;
        private const Byte bSecondBHS_BagPlan = 0x40;
        private const Byte bSecondAnimatedQueues = 0x80;

        private const Byte bThirdNA1 = 0x01;
        private const Byte bThirdNA2 = 0x02;
        private const Byte bThirdNA3 = 0x04;
        private const Byte bThirdNA4 = 0x08;
        private const Byte bThirdNA5 = 0x10;
        private const Byte bThirdNA6 = 0x20;
        private const Byte bThirdNA7 = 0x40;
        private const Byte bThirdNA8 = 0x80;
        #endregion

        #region La fonction utilisées pour interroger la clef
#if!(UNIKEY_PROTECTION)
        [DllImport("FonctionsCritiques.dll")]
        unsafe public static extern Byte* u(int o);
#endif
        #endregion

        // << Task #8279 Pax2Sim - Protection Key - key details function
        #region Dll functions used to get the protection key details
        [DllImport("UniKey.dll")]
        public static extern uint UniKey_Find(ref ushort pHandle, ref uint pPassword1, ref uint pPassword2);
        [DllImport("UniKey.dll")]
        public static extern uint UniKey_User_Logon(ref ushort pHandle, ref ushort pPassword1, ref ushort pPassword2);        
        [DllImport("UniKey.dll")]
        public static extern uint UniKey_Get_Time(ref ushort pHandle, ref uint pYear, ref uint pMonth, ref ushort pDay, ref ushort pHour, ref ushort pMinute, ref ushort pSecond);
        [DllImport("UniKey.dll")]
        public static extern uint UniKey_Read_UpdateTag(ref ushort pHandle, ref uint pUpdateTag);                
        [DllImport("UniKey.dll")]
        public static extern uint UniKey_Check_Time_Module_Now(ref ushort pHandle, ref uint pModule, ref uint lp2, ref ushort p1);                
        //retcode constants
        public const int ERROR_UNIKEY_NEED_OPEN = 212;
        public const int ERROR_UNIKEY_COMPARE_TIME_MODULE = 233;
        public const int ERROR_UNIKEY_TIME_MODULE_OVERDUR = 235;
        #endregion

        #region Error messages for protection key access
        const String HARDWAREKEY_NOT_FOUND = "Protection key not found.";
        const String LOG_ON_FAILED = "Failed to log to the protection key.";
        const String GET_DATE_TIME_FAILED = "Failed to retrieve the UniKey Date.";
        const String GET_REMAINING_TIME_FAILED = "Failet to retrieve remaining time until the Unikey expiration.";
        const String GET_UPDATE_TAG_FAILED = "Failed to retrieve the update digit.";
        const String READ_MEMORY_FAILED = "Failed to read the memory.";

        #endregion
        // >> Task #8279 Pax2Sim - Protection Key - key details function
        [DllImport("UniKey.dll")]
        public static extern long UniKey_Read_Memory(ref ushort pHandle, ref ushort pStartAddress, ref ushort pBufferLength, byte[] pBuffer);  // >> Task #13051 Pax2Sim - Unikey Log

        public bool HUB
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[0] & bFirstHUB) == bFirstHUB;
                }
            }
        }
        public bool PAX
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[0] & bFirstPAX) == bFirstPAX;
                }
            }
        }
        public bool BHS
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[0] & bFirstBHS) == bFirstBHS;
                }
            }
        }
        public bool PKG
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[0] & bFirstPKG) == bFirstPKG;
                }
            }
        }
        
        public bool SODEXI
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[0] & bFirstSodexi) == bFirstSodexi;
                }
            }
        }

        
        public bool Airside
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[0] & bFirstAirside) == bFirstAirside;
                }
            }
        }
        public bool TMS
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[0] & bFirstTMS) == bFirstTMS;
                }
            }
        }
        public bool[] Perimeters
        {
            get
            {
                return new Boolean[] { HUB, PAX, BHS, Airside, TMS,PKG };
            }
        }

        public bool Static
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[1] & bSecondStatic) == bSecondStatic;
                }
            }
        }
        public bool Dynamic
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[1] & bSecondDynamic) == bSecondDynamic;
                }
            }
        }
        public bool Runtime
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[1] & bSecondRuntime) == bSecondRuntime;
                }
            }
        }
        public bool BHS_MeanFlows
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[1] & bSecondBHS_MeanFlows) == bSecondBHS_MeanFlows;
                }
            }
        }
        public bool BHS_PaxPlan
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[1] & bSecondBHS_PaxPlan) == bSecondBHS_PaxPlan;
                }
            }
        }
        public bool AllocationTools
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[1] & bSecondAllocation) == bSecondAllocation;
                }
            }
        }
        public bool BHS_BagPlan
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[1] & bSecondBHS_BagPlan) == bSecondBHS_BagPlan;
                }
            }
        }
        public bool AnimatedQueues
        {
            get
            {
                unsafe
                {
                    if (bResults == null)
                        return false;
                    return (bResults[1] & bSecondAnimatedQueues) == bSecondAnimatedQueues;
                }
            }
        }



        public AskMode()
        {
            unsafe
            {
                bResults = null;

#if!(UNIKEY_PROTECTION)
                bResults = (Byte*)u(0);
#else
                OverallTools.Unikey.TestDongle(2, out bResults);
#endif
            }
        }
        
        // >> Task #13763 Pax2Sim - .pax project file
        internal static uint unikeyHardwareId;
        internal static string lastFourUnikeyMemoryDigits = "";
        // << Task #13763 Pax2Sim - .pax project file

        // << Task #8279 Pax2Sim - Protection Key - key details function        
        public static void logProtectionKeyDetails()
        {
            ushort handle = 0;
            uint password1 = 10159;
            uint password2 = 48838;
            uint retcode = 0;

            //OverallTools.ExternFunctions.PrintLogFile(System.Environment.NewLine);
            OverallTools.ExternFunctions.PrintLogFile("<<--------Protection Key Details--------------" + System.Environment.NewLine);

            //Check if there is a key connected.
            retcode = UniKey_Find(ref handle, ref password1, ref password2);
            if (retcode != 0)
            {
                OverallTools.ExternFunctions.PrintLogFile(HARDWAREKEY_NOT_FOUND + " Error Id: " + retcode + ". " + System.Environment.NewLine);
                OverallTools.ExternFunctions.PrintLogFile(">>--------Protection Key Details--------------" + System.Environment.NewLine);
                return;
            }
            uint hardwareId = password1; //password1 is an in/out parameter -> stores the protection key id
            password1 = 10159; //after storing the hardwareId reset the variable to hold the password
            OverallTools.ExternFunctions.PrintLogFile("Unikey Unique Hardware Id: " + hardwareId + System.Environment.NewLine);
            unikeyHardwareId = hardwareId;  // >> Task #13763 Pax2Sim - .pax project file

            //Open Key
            ushort p1 = 10159;
            ushort p2 = 48838;
            retcode = UniKey_User_Logon(ref handle, ref p1, ref p2);
            if (retcode != 0)
            {
                OverallTools.ExternFunctions.PrintLogFile(LOG_ON_FAILED + " Error Id: " + retcode + ". " + System.Environment.NewLine);
                OverallTools.ExternFunctions.PrintLogFile(">>--------Protection Key Details--------------" + System.Environment.NewLine);
                return;
            }
            //Retrieve the Key Date+Time
            uint module = 1;
            ushort day = 0;
            uint month = 0;
            uint year = 0;
            ushort hour = 0;
            ushort minute = 0;
            ushort second = 0;

            retcode = UniKey_Get_Time(ref handle, ref year, ref month, ref day, ref hour, ref minute, ref second);
            if (retcode != 0)
            {
                OverallTools.ExternFunctions.PrintLogFile(GET_DATE_TIME_FAILED + " Error Id: " + retcode + System.Environment.NewLine);
            }
            else
            {
                OverallTools.ExternFunctions.PrintLogFile("Current Unikey Date (DD/MM/YY HH:MM:SS): " +
                                day + "/" + month + "/" + year + "/ " + hour + ":" + minute + ":" + second + System.Environment.NewLine);
            }
            //Retrieve remaining time
            uint remainingDays = 0;
            ushort remainingHours = 0;

            retcode = UniKey_Check_Time_Module_Now(ref handle, ref module, ref remainingDays, ref remainingHours);
            if (retcode != 0)
            {
                if (retcode == ERROR_UNIKEY_NEED_OPEN)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Expiration date info: The key is not open. Need to open the Key before acquiring informantion."
                        + System.Environment.NewLine);
                }
                else if (retcode == ERROR_UNIKEY_COMPARE_TIME_MODULE)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Expiration date info: The specific time is before the module’s start time."
                        + System.Environment.NewLine);
                }
                else if (retcode == ERROR_UNIKEY_TIME_MODULE_OVERDUR)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Expiration date info: The specific time is later than the module’s end time, or the module is expired"
                        + System.Environment.NewLine);
                }
            }
            else
                OverallTools.ExternFunctions.PrintLogFile("Remaining Time to expiration (Days / Hours): "
                                + remainingDays + " / " + remainingHours + System.Environment.NewLine);

            //Update Tag
            uint updateTag = 0;
            retcode = UniKey_Read_UpdateTag(ref handle, ref updateTag);
            if (retcode != 0)
                OverallTools.ExternFunctions.PrintLogFile(GET_UPDATE_TAG_FAILED + " Error Id: " + retcode + System.Environment.NewLine);
            else
                OverallTools.ExternFunctions.PrintLogFile("Update Digit: " + updateTag + System.Environment.NewLine);

            // >> Task #13051 Pax2Sim - Unikey Log
            //Memory
            ushort startAddress = 0;
            ushort bufferLength = 48;
            byte[] buffer = new byte[48];
            try
            {
                long memoryReadingRetcode = UniKey_Read_Memory(ref handle, ref startAddress, ref bufferLength, buffer);
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile(READ_MEMORY_FAILED + " Error: " + e.Message + System.Environment.NewLine);
            }
            writeMemoryDataToLogFile(buffer);
            // << Task #13051 Pax2Sim - Unikey Log

            OverallTools.ExternFunctions.PrintLogFile(">>--------Protection Key Details--------------" + System.Environment.NewLine);

            warnAboutExpiry(remainingDays, remainingHours); // << Task #8748 Pax2Sim - protection key warning message before expiration

        }
        // >> Task #8279 Pax2Sim - Protection Key - key details function

        // >> Task #13051 Pax2Sim - Unikey Log
        private static void writeMemoryDataToLogFile(byte[] buffer)
        {
            if (buffer == null)
                return;

            string hex = BitConverter.ToString(buffer);
            if (hex.IndexOf("-") != -1)
            {
                OverallTools.ExternFunctions.PrintLogFile("Memory Data");

                string[] hexes = hex.Split('-');
                string line = "";
                if (hexes.Length >= 48)
                {
                    for (int i = 0; i < hexes.Length; i++)
                    {
                        if ((i + 1) % 16 == 0)
                        {
                            line += hexes[i];
                        }
                        else
                        {
                            line += hexes[i] + "-";
                        }
                        if (i == 15)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("From  0 to 15: " + line);
                            line = "";
                        }
                        else if (i == 31)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("From 16 to 31: " + line);
                            line = "";
                        }
                        else if (i == 47)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("From 32 to 47: " + line + System.Environment.NewLine);
                            line = "";
                        }

                        // >> Task #13763 Pax2Sim - .pax project file                        
                        if (i == 34)
                            lastFourUnikeyMemoryDigits += hexes[i] + "_";
                        else if (i == 35)
                            lastFourUnikeyMemoryDigits += hexes[i];
                        // << Task #13763 Pax2Sim - .pax project file
                    }
                }
            }
            
        }
        // << Task #13051 Pax2Sim - Unikey Log

        // << Task #8748 Pax2Sim - protection key warning message before expiration
        private static void warnAboutExpiry(uint remainingDays, ushort remainingHours)
        {
            String warningMessage = "";
            if ((remainingDays == 30 || remainingDays == 15 || remainingDays <= 10) && (remainingDays > 0))
            {
                warningMessage = "Your protection key license will expire in " + remainingDays
                    + " days and " + remainingHours + " hours.";
                MessageBox.Show(warningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (remainingDays == 0 && remainingHours > 0)
            {
                warningMessage = "Your protection key license will expire in " + remainingHours + " hours.";
                MessageBox.Show(warningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // >> Task #8748 Pax2Sim - protection key warning message before expiration

        // >> Task #13051 Pax2Sim - Unikey Log        
        public static void logPax2SimAssemblyVersion()
        {
            OverallTools.ExternFunctions.PrintLogFile("PAX2SIM Version: " + OverallTools.AssemblyActions.AssemblyVersion 
                                                        + System.Environment.NewLine);            
        }
        // << Task #13051 Pax2Sim - Unikey Log
    }
    #endregion
}
