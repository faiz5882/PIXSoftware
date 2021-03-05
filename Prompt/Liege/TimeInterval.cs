using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    public class TimeInterval
    {
        //static because we want to point to the same memory location so we can check in a list using the Contains method
        internal static TimeInterval EMPTY_TIME_INTERVAL = new TimeInterval(DateTime.MinValue, DateTime.MinValue);
        
        internal DateTime fromDate { get; set; }
        internal DateTime toDate { get; set; }


        override public string ToString()
        {
            return fromDate + " - " + toDate;
        }

        internal double timeIntervalLengthInMinutes
        {
            get
            {
                if (toDate > fromDate)
                {
                    TimeSpan span = toDate.Subtract(fromDate);
                    if (span != null)
                        return span.TotalMinutes;
                }
                return 0;
            }
        }
        internal double timeIntervalLengthInSeconds
        {
            get
            {
                if (toDate > fromDate)
                {
                    TimeSpan span = toDate.Subtract(fromDate);
                    if (span != null)
                        return span.TotalSeconds;
                }
                return 0;
            }
        }
        public TimeInterval(DateTime _fromDate, DateTime _toDate)
        {
            fromDate = _fromDate;
            toDate = _toDate;
        }

        public bool containsTimeInterval(TimeInterval checkedTimeInterval)
        {
            if (checkedTimeInterval.fromDate > checkedTimeInterval.toDate)
                return false;

            if (fromDate <= checkedTimeInterval.fromDate
                && toDate >= checkedTimeInterval.toDate)
            {
                return true;
            }
            return false;
        }

        public bool intersectsTimeInterval(TimeInterval checkedTimeInterval)
        {
            if (checkedTimeInterval.fromDate > checkedTimeInterval.toDate)
                return false;

            if (containsTimeInterval(checkedTimeInterval))
                return true;

            if (checkedTimeInterval.toDate < fromDate
                || checkedTimeInterval.fromDate > toDate)
            {
                return false;
            }
            return true;
        }

        public TimeInterval getIntersectionTimeInterval(TimeInterval intersectingTimeInterval)
        {
            TimeInterval resultedTimeInterval = EMPTY_TIME_INTERVAL;
            if (intersectsTimeInterval(intersectingTimeInterval))
            {
                if (containsTimeInterval(intersectingTimeInterval))
                    resultedTimeInterval = intersectingTimeInterval;
                if (intersectingTimeInterval.containsTimeInterval(this))
                    resultedTimeInterval = this;

                if (fromDate >= intersectingTimeInterval.fromDate)
                { 
                    if (toDate <= intersectingTimeInterval.toDate)
                        resultedTimeInterval = new TimeInterval(fromDate, toDate);
                    else
                        resultedTimeInterval = new TimeInterval(fromDate, intersectingTimeInterval.toDate);
                }
                else if (toDate >= intersectingTimeInterval.fromDate)
                {
                    if (toDate <= intersectingTimeInterval.toDate)
                        resultedTimeInterval = new TimeInterval(intersectingTimeInterval.fromDate, toDate);
                    else
                        resultedTimeInterval = new TimeInterval(intersectingTimeInterval.fromDate, intersectingTimeInterval.toDate);
                }
            }
            return resultedTimeInterval;
        }

        public bool containsDateTime(DateTime checkedDateTime)
        {
            if (fromDate <= checkedDateTime
                && toDate >= checkedDateTime)
            {
                return true;
            }
            return false;
        }

        public List<TimeInterval> splitByInteriorTimeInterval(TimeInterval interiorTimeInterval)
        {
            List<TimeInterval> resultedTimeIntervals = new List<TimeInterval>();
            resultedTimeIntervals.Add(this);

            if (this.containsTimeInterval(interiorTimeInterval))
            {
                resultedTimeIntervals.Clear();
                if (interiorTimeInterval.fromDate == this.fromDate && interiorTimeInterval.toDate == this.toDate)
                {
                    resultedTimeIntervals.Add(EMPTY_TIME_INTERVAL);
                }
                else if (interiorTimeInterval.fromDate == this.fromDate)
                {
                    TimeInterval lastPartTimeInterval = new TimeInterval(interiorTimeInterval.toDate.AddMinutes(1), this.toDate);
                    resultedTimeIntervals.Add(lastPartTimeInterval);
                }
                else if (interiorTimeInterval.toDate == this.toDate)
                {
                    TimeInterval firstPartTimeInterval = new TimeInterval(this.fromDate, interiorTimeInterval.fromDate.AddMinutes(-1));
                    resultedTimeIntervals.Add(firstPartTimeInterval);
                }
                else
                {
                    TimeInterval firstPartTimeInterval = new TimeInterval(this.fromDate, interiorTimeInterval.fromDate.AddMinutes(-1));
                    resultedTimeIntervals.Add(firstPartTimeInterval);
                    TimeInterval lastPartTimeInterval = new TimeInterval(interiorTimeInterval.toDate.AddMinutes(1), this.toDate);
                    resultedTimeIntervals.Add(lastPartTimeInterval);
                }
            }
            return resultedTimeIntervals;
        }

        public List<TimeInterval> splitByIntersectingTimeInterval(TimeInterval intersectingTimeInterval)
        {
            List<TimeInterval> resultedTimeIntervals = new List<TimeInterval>();
            resultedTimeIntervals.Add(this);

            if (this.intersectsTimeInterval(intersectingTimeInterval))
            {
                resultedTimeIntervals.Clear();
                if (intersectingTimeInterval.fromDate <= this.fromDate && intersectingTimeInterval.toDate >= this.toDate)
                {
                    resultedTimeIntervals.Add(EMPTY_TIME_INTERVAL);
                }
                else if (intersectingTimeInterval.fromDate < this.fromDate
                        && intersectingTimeInterval.toDate > this.fromDate)
                {
                    TimeInterval lastPartTimeInterval = new TimeInterval(intersectingTimeInterval.toDate.AddMinutes(1), this.toDate);
                    resultedTimeIntervals.Add(lastPartTimeInterval);
                }
                else if (intersectingTimeInterval.fromDate < this.toDate
                        && intersectingTimeInterval.toDate > this.toDate)
                {
                    TimeInterval firstPartTimeInterval = new TimeInterval(this.fromDate, intersectingTimeInterval.fromDate.AddMinutes(-1));
                    resultedTimeIntervals.Add(firstPartTimeInterval);
                }
                else
                {
                    //the intersecting interval is contained
                    resultedTimeIntervals = splitByInteriorTimeInterval(intersectingTimeInterval);                    
                }
            }
            return resultedTimeIntervals;
        }

    }
}
