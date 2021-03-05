using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.com.crispico.text_gantt
{
    public class TextGanttParameters
    {
        private const double DEFAULT_SAMPLING_STEP = 1;

        private TimeInterval fpiDefaultTimeInterval;        
        private bool useManualTimeInterval;
        private TimeInterval manualTimeInterval;        
        private double samplingStep;

        public static TextGanttParameters getDefaultParameters(TimeInterval defaultFpiTimeInterval)
        {
            return new TextGanttParameters(defaultFpiTimeInterval, false, TimeInterval.EMPTY_TIME_INTERVAL, DEFAULT_SAMPLING_STEP);
        }

        public TimeInterval getFpiDefaultTimeInterval()
        {
            return fpiDefaultTimeInterval;
        }

        public bool getUseManualTimeInterval()
        {
            return useManualTimeInterval;
        }

        public TimeInterval getTimeInterval()
        {
            if (useManualTimeInterval)
                return manualTimeInterval;
            return fpiDefaultTimeInterval;
        }

        public double getSamplingStep()
        {
            return samplingStep;
        }

        // plain setters
        public void setManualTimeInterval(TimeInterval value)
        {
            this.manualTimeInterval = value;
        }

        public void setUseManualTimeInterval(bool value)
        {
            this.useManualTimeInterval = value;
        }

        public void setSamplingStep(double value)
        {
            this.samplingStep = value;
        }

        public TextGanttParameters(TimeInterval defaultFpiTimeInterval, bool useManualTimeInterval, TimeInterval manualTimeInterval, double samplingStep)
        {
            this.fpiDefaultTimeInterval = defaultFpiTimeInterval;
            this.useManualTimeInterval = useManualTimeInterval;
            this.manualTimeInterval = manualTimeInterval;            
            this.samplingStep = samplingStep;
        }


    }
}
