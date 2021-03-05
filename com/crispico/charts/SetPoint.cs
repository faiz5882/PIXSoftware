using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nevron.GraphicsCore;

namespace SIMCORE_TOOL.com.crispico.charts
{
    public class SetPoint
    {
        public double numericStartValue { get; set; }

        public double numericEndValue { get; set; }

        public DateTime dateStartValue { get; set; }

        public DateTime dateEndValue { get; set; }

        public bool isArea { get; set; }

        public bool isActivated { get; set; }

        public string axis { get; set; }

        public NStrokeStyle strokeColor { get; set; }
        
        public NStrokeStyle fillColor { get; set; }

        public ChartUtils.CHART_POSITIONS chartPosition { get; set; }

        public override string ToString()
        {
            return chartPosition.ToString() + " | " + axis + " | " + numericStartValue + " | " + numericEndValue 
                + " | " + dateStartValue + " | " + dateEndValue + " | is area: " + isArea + " | is activated: " + isActivated;
        }

        public SetPoint clone()
        {
            SetPoint clone = new SetPoint();
            clone.numericStartValue = this.numericStartValue;
            clone.numericEndValue = this.numericEndValue;
            clone.dateStartValue = this.dateStartValue;
            clone.dateEndValue = this.dateEndValue;
            clone.isArea = this.isArea;
            clone.isActivated = this.isActivated;
            if (this.axis != null)
                clone.axis = (string)this.axis.Clone();
            if (this.strokeColor != null)
                clone.strokeColor = (NStrokeStyle)this.strokeColor.Clone();
            if (this.fillColor != null)
                clone.fillColor = (NStrokeStyle)this.fillColor.Clone();
            clone.chartPosition = this.chartPosition;
            return clone;
        }
    }
}
