using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.com.crispico.general_allocation.model
{
    class Zone
    {
        public int terminalNb { get; set; }

        private List<Resource> resources = new List<Resource>();

        public string nameByTerminal
        {
            get
            {
                return "T_" + terminalNb;
            }
        }

        public List<Resource> getResources()
        {
            return resources;
        }

        public Zone(int terminalNb)
        {
            this.terminalNb = terminalNb;
            this.resources = new List<Resource>(resources);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Zone other = (Zone)obj;
            return this.terminalNb == other.terminalNb;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            //hash = hash * 23 + ((this.terminalNb == null) ? 0 : this.terminalNb.GetHashCode());
            hash = hash * 23 + this.terminalNb.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return nameByTerminal;
        }

    }
}
