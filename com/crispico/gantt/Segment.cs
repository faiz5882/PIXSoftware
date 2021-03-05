using System;
using System.Collections.Generic;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.gantt
{
    /**
     * A segment must be linked to a task
     */ 
    public class Segment
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int percentComplete { get; set; }
        //public Task task { get; set; }
        public String color { get; set; }
        public String leftText { get; set; }
        public String middleText { get; set; }
        public Boolean drawComposed { get; set; }
        // can be: 0 = Economy class resources / 1 = normal resources / 2 = F&B class resources
        public int position { get; set; }
        public int id { get; set; }
        public String classType { get; set; }
        public int startEco { get; set; }
        public int endEco { get; set; }
        public int startFB { get; set; }
        public int endFB { get; set; }
        // used to determine if the segment represents an arrival or departure flight
        // useful when moving flights on resources that
        //take into account the arrival and departure flight plans
        public String arrivalDeparture { get; set; }
        // stores the first dest and last desk that the resource occupies
        // for non-range resources the end desk will be set by default to -1        
        public int startDesk { get; set; }
        public int endDesk { get; set; }
        // desk format in AlphaNumerical mode
        public String startEcoAN { get; set; }
        public String endEcoAN { get; set; }
        public String startFBAN { get; set; }
        public String endFBAN { get; set; }
        public String startDeskAN { get; set; }
        public String endDeskAN { get; set; }

        
        public Segment()
        {
            start = DateTime.MaxValue;
            end = DateTime.MinValue;
            percentComplete = 0;
            //task = null;
            color = "0xFA0000";
            leftText = ""; // pt flight => alte info ex. terminal...
            middleText = "";//nume
            drawComposed = false;
            position = Model.NORMAL;
            id = 0;
            classType = null;
            // desk number in Numerical mode
            startEco = -1;
            endEco = -1;
            startFB = -1;
            endFB = -1;
            startDesk = -1;
            endDesk = -1;
            // desk number in AlphaNumerical mode
            startEcoAN = "";
            endEcoAN = "";
            startFBAN = "";
            endFBAN = "";
            startDeskAN = "";
            endDeskAN = "";
        }
        
        public Segment(DateTime pStart, DateTime pEnd, int pPercentComplete, Task pTask,
                         String pColor, String pLeftText, String pMiddleText, Boolean pDrawComposed, int pPosition)
        {
            start = pStart;
            end = pEnd;
            percentComplete = pPercentComplete;
            //task = pTask;
            if (pColor != "" && pColor != null)
                color = pColor;
            else
                color = "0xFA0000";
            leftText = pLeftText;
            middleText = pMiddleText;
            drawComposed = pDrawComposed;
            if (pPosition != 0 || pPosition != 1 || pPosition != 2)
                position = 1;
            else
                position = pPosition;
        }

        public Segment clone()
        {
            Segment segmentClone = new Segment();
            segmentClone.id = this.id;
            segmentClone.start = this.start;
            segmentClone.end = this.end;

            segmentClone.classType = this.classType;
            segmentClone.startDesk = this.startDesk;
            segmentClone.endDesk = this.endDesk;
            segmentClone.startEco = this.startEco;
            segmentClone.endEco = this.endEco;
            segmentClone.startFB = this.startFB;
            segmentClone.endFB = this.endFB;

            segmentClone.startDeskAN = this.startDeskAN;
            segmentClone.endDeskAN = this.endDeskAN;
            segmentClone.startEcoAN = this.startEcoAN;
            segmentClone.endEcoAN = this.endEcoAN;
            segmentClone.startFBAN = this.startFBAN;
            segmentClone.endFBAN = this.endFBAN;

            segmentClone.color = this.color;
            segmentClone.leftText = this.leftText;
            segmentClone.middleText = this.middleText;
            segmentClone.arrivalDeparture = this.arrivalDeparture;

            segmentClone.drawComposed = this.drawComposed;
            segmentClone.position = this.position;
            segmentClone.percentComplete = this.percentComplete;

            return segmentClone;
        }
    }
}