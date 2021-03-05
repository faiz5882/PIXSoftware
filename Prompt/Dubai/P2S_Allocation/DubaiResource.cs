using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.Prompt.Dubai.P2S_Allocation
{
    class DubaiResource
    {
        internal int id { get; set; }           // ex: 22
        internal string name { get; set; }      // ex: Parking_22
        internal string code { get; set; }      // ex: P_22        
                
        /// <summary>
        /// True if when the check is done the Resource has at least one flight allocated in the given interval.
        /// </summary>
        public bool isOccupied(TimeInterval currentOccupationInterval)
        {            
            if (flightAllocations != null
                && flightAllocations.Count > 0)
            {
                //foreach (DubaiFlight flightAllocation in flightAllocations)
                //{
                //    if (currentOccupationInterval.intersectsTimeInterval(flightAllocation.occupationInterval))
                //    {
                //        return true;
                //    }
                //}
            }
            return false;            
        }

        public List<TimeInterval> initialAvailableTimeIntervals = new List<TimeInterval>();//to keep the initial intervals - will not be used in the allocation
        public List<TimeInterval> availableTimeIntervals = new List<TimeInterval>();
        public List<DubaiFlight> flightAllocations = new List<DubaiFlight>();

        public DubaiResource(int _id, string _name, string _code,
            List<TimeInterval> _initialAvailableTimeIntervals)
        {
            id = _id;
            name = _name;
            code = _code;
            

            initialAvailableTimeIntervals.AddRange(_initialAvailableTimeIntervals);
            initialAvailableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));

            availableTimeIntervals.AddRange(_initialAvailableTimeIntervals);
            availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
        }

        public bool hasAvailableInterval(TimeInterval occupiedTimeInterval)
        {
            availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
            foreach (TimeInterval availableInterval in availableTimeIntervals)
            {
                if (availableInterval.containsTimeInterval(occupiedTimeInterval))
                    return true;
            }
            return false;
        }
        
        public void allocateFlightToResource(DubaiFlight allocatedFlight, int delayBetweenFlightsInMinutes)
        {
            //add flight to allocated flights list
            //FlightAllocation flightAllocation 
            //    = new FlightAllocation(allocatedFlight, this, allocatedFlight.occupiedInterval);
            //flightAllocations.Add(flightAllocation);
            //remove occupied interval from available
            removeOccupiedIntervalFromAvailableIntervalsWhenContaining(allocatedFlight.occupiedInterval);
            //remove delay(after allocated flight) interval from available if necessary
            if (delayBetweenFlightsInMinutes > 0)
                addDelayAfterTheAllocatedFlight(allocatedFlight.occupiedInterval, delayBetweenFlightsInMinutes);
        }
        
        public void removeOccupiedIntervalFromAvailableIntervalsWhenContaining(TimeInterval occupiedTimeInterval)
        {
            availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
            TimeInterval intervalContainingOccupiedTimeInterval = null;

            int i = 0;
            for (i = 0; i < availableTimeIntervals.Count; i++)
            {
                TimeInterval currentInterval = availableTimeIntervals[i];
                if (currentInterval.containsTimeInterval(occupiedTimeInterval))
                {
                    intervalContainingOccupiedTimeInterval = currentInterval;
                    break;
                }
            }
            if (intervalContainingOccupiedTimeInterval != null)
            {
                availableTimeIntervals.RemoveAt(i);
                List<TimeInterval> remainingAvailableIntervals
                    = intervalContainingOccupiedTimeInterval.splitByInteriorTimeInterval(occupiedTimeInterval);
                if (!remainingAvailableIntervals.Contains(TimeInterval.EMPTY_TIME_INTERVAL))
                {
                    availableTimeIntervals.AddRange(remainingAvailableIntervals);
                    availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
                }
            }
        }

        private void addDelayAfterTheAllocatedFlight(TimeInterval occupiedTimeInterval, int delayBetweenFlightsInMinutes)
        {
            DateTime delayEndDate = occupiedTimeInterval.toDate.AddMinutes(delayBetweenFlightsInMinutes);
            TimeInterval delayInterval = new TimeInterval(occupiedTimeInterval.toDate, delayEndDate);
            removeOccupiedIntervalFromAvailableIntervalsWhenIntersecting(delayInterval);
        }

        private void removeOccupiedIntervalFromAvailableIntervalsWhenIntersecting(TimeInterval occupiedTimeInterval)
        {
            availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
            List<TimeInterval> intervalsIntersectingOccupiedTimeInterval = new List<TimeInterval>();

            int i = 0;
            for (i = 0; i < availableTimeIntervals.Count; i++)
            {
                TimeInterval currentInterval = availableTimeIntervals[i];
                if (currentInterval.intersectsTimeInterval(occupiedTimeInterval))
                {
                    intervalsIntersectingOccupiedTimeInterval.Add(currentInterval);                    
                }
            }
            if (intervalsIntersectingOccupiedTimeInterval.Count > 0)
            {
                foreach (TimeInterval interval in intervalsIntersectingOccupiedTimeInterval)
                {
                    if (availableTimeIntervals.Contains(interval))
                    {
                        availableTimeIntervals.Remove(interval);
                        List<TimeInterval> remainingAvailableIntervals
                            = interval.splitByIntersectingTimeInterval(occupiedTimeInterval);
                        if (!remainingAvailableIntervals.Contains(TimeInterval.EMPTY_TIME_INTERVAL))
                        {
                            availableTimeIntervals.AddRange(remainingAvailableIntervals);
                            availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
                        }
                    }
                }
            }
        }

        
    }
}
