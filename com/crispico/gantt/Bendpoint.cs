using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.gantt
{
    /// <summary>
    /// Stores the coordinetes of a bending point from a Connection
    /// </summary>
    public class Bendpoint
    {
        public int x { get; set; }

        public int y { get; set; }

        public String connectionSourceName { get; set; }

        public String connectionTargetName { get; set; }

        public Bendpoint()
        {
            this.x = 0;
            this.y = 0;
            this.connectionSourceName = "";
            this.connectionTargetName = "";
        }
    }
}
