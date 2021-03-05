using System;
using System.Collections.Generic;
using System.Text;

namespace SIMCORE_TOOL.Classes
{
    #region la classe qui permet de gérer la version d'enregistrement utilisée.
    public class VersionManager
    {
        #region Les différents variables de la classe.
        private String sVersion;
        private int iMajor;
        private int iMinor;
        private bool bBHS_;
        public bool isBHS
        {
            get
            {
                return bBHS_;
            }
            set
            {
                bBHS_ = value;
            }
        }
        #endregion
        #region Les constructeurs de la classe.
        public VersionManager(String Value)
        {
            bBHS_ = false;
            if (Value == null)
            {
                iMajor = 1;
                iMinor = 0;
                return;
            }
            sVersion = Value;
            String[] tsVersion = Value.Split('.');
            if (tsVersion.Length != 2)
            {//Erreur, le numéro de version n'est pas un numéro valide.
                iMajor = 1;
                iMinor = 0;
                sVersion = "1.0";
                return;
            }
            if ((!Int32.TryParse(tsVersion[0], out iMajor)) ||
                (!Int32.TryParse(tsVersion[1], out iMinor)))
            {//Erreur les numéros de version ne sont pas lisibles.
                iMajor = 1;
                iMinor = 0;
                sVersion = "1.0";
                return;
            }
        }
        public VersionManager(int iMajor_, int iMinor_)
        {
            iMajor = iMajor_;
            iMinor = iMinor_;
            sVersion = iMajor.ToString() + "." + iMinor.ToString();
            bBHS_ = false;
        }
        public VersionManager(VersionManager vmOld)
        {
            iMajor = vmOld.iMajor;
            iMinor = vmOld.iMinor;
            sVersion = iMajor.ToString() + "." + iMinor.ToString();
            bBHS_ = false;
        }
        #endregion
        #region Les fonctions surchargées
        public override string ToString()
        {
            return iMajor.ToString() + "." + iMinor.ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public bool isVersion(int iMajor_, int iMinor_)
        {
            return ((iMajor == iMajor_) && (iMinor == iMinor_));
        }
        public override bool Equals(object obj)
        {
            return ((VersionManager)this) == ((VersionManager)obj);
        }
        #region les fonctions de comparaison des versions
        public static bool operator <(VersionManager vmFirst, VersionManager vmSecond)
        {
            if (vmSecond.iMajor > vmFirst.iMajor)
                return true;
            if (vmSecond.iMajor == vmFirst.iMajor)
            {
                if (vmSecond.iMinor > vmFirst.iMinor)
                    return true;
            }
            return false;
        }
        public static bool operator >(VersionManager vmFirst, VersionManager vmSecond)
        {
            return (vmSecond < vmFirst);
        }
        public static bool operator <=(VersionManager vmFirst, VersionManager vmSecond)
        {
            if (vmSecond.iMajor > vmFirst.iMajor)
                return true;
            if (vmSecond.iMajor == vmFirst.iMajor)
            {
                if (vmSecond.iMinor >= vmFirst.iMinor)
                    return true;
            }
            return false;
        }
        public static bool operator >=(VersionManager vmFirst, VersionManager vmSecond)
        {
            return (vmSecond <= vmFirst);
        }
        public static bool operator ==(VersionManager vmFirst, VersionManager vmSecond)
        {
            if (((Object)vmFirst == null) && ((Object)vmSecond == null))
                return true;
            if ((((Object)vmFirst == null) && ((Object)vmSecond != null)) ||
                 (((Object)vmFirst != null) && ((Object)vmSecond == null)))
                return false;

            if ((vmSecond.iMajor == vmFirst.iMajor) && (vmSecond.iMinor == vmFirst.iMinor))
                return true;
            return false;
        }
        public static bool operator !=(VersionManager vmFirst, VersionManager vmSecond)
        {
            if ((vmSecond.iMajor != vmFirst.iMajor) || (vmSecond.iMinor != vmFirst.iMinor))
                return true;
            return false;
        }
        #endregion
        #endregion
        internal void IncrecementMinorVersion()
        {
            iMinor++;
        }
        internal void IncrecementMajorVersion()
        {
            iMajor++;
            iMinor = 0;
        }
        internal void DecrecementMinorVersion()
        {
            iMinor--;
            if (iMinor < 0)
            {
                iMinor = 100;
                iMajor--;
            }
        }
    }
    #endregion
}
