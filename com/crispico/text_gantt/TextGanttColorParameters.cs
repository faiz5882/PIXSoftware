using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SIMCORE_TOOL.com.crispico.text_gantt
{
    public class TextGanttColorParameters
    {
        private static TextGanttColorParameters DEFAULT_PARAMETERS;

        private bool byDefaultColor;
        private bool byAirline;
        private bool byFlightCategory;
        private bool byGroundHandler;
        private bool byOverlap;

        public static TextGanttColorParameters getDefaultParameters()
        {
            if (DEFAULT_PARAMETERS == null)
                DEFAULT_PARAMETERS = new TextGanttColorParameters(true, false, false, false, false);
            return DEFAULT_PARAMETERS;
        }

        public bool getByDefaultColor()
        {
            return byDefaultColor;
        }

        public bool getByAirline()
        {
            return byAirline;
        }
        public bool getByFlightCategory()
        {
            return byFlightCategory;
        }
        public bool getByGroundHandler()
        {
            return byGroundHandler;
        }
        public bool getByOverlap()
        {
            return byOverlap;
        }

        public TextGanttColorParameters(bool byDefaultColor, bool byAirline, bool byFlightCategory, bool byGroundHandler, bool byOverlap)
        {
            this.byDefaultColor = byDefaultColor;
            this.byAirline = byAirline;
            this.byFlightCategory = byFlightCategory;
            this.byGroundHandler = byGroundHandler;
            this.byOverlap = byOverlap;
        }
    }
}
