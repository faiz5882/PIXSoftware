using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.generalUse
{
    class Coordinate
    {
        private double _rowIndex;
        private double _cellIndex;

        public double rowIndex 
        { 
            get { return _rowIndex; } 
        }

        public double cellIndex 
        { 
            get { return _cellIndex; } 
        }

        public Coordinate(double pRowIndex, double pCellIndex)
        {
            _rowIndex = pRowIndex;
            _cellIndex = pCellIndex;
        }
    }
}
