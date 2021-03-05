using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.Prompt.Allocation.General
{
    class Resource
    {
        internal int id { get; set; }
        internal string code { get; set; }

        internal string zone { get; set; }
        internal string concourse { get; set; }
        internal string allowedAllocationCode { get; set; }
        internal int allowedTerminalNb { get; set; }
        internal bool isRemote { get; set; }

        public List<TimeInterval> initialAvailableTimeIntervals = new List<TimeInterval>(); //to keep the initial intervals - will not be used in the allocation
        public List<TimeInterval> availableTimeIntervals = new List<TimeInterval>();
        public List<FlightResourceAllocation> flightAllocations = new List<FlightResourceAllocation>();

        public Resource(int _id, string _code,
            string _zone, string _concourse, string _allowedAllocationCode, int _allowedTerminalNb, bool _isRemote,
            List<TimeInterval> _initialAvailableTimeIntervals)
        {
            id = _id;            
            code = _code;

            zone = _zone;
            concourse = _concourse;
            allowedAllocationCode = _allowedAllocationCode;
            allowedTerminalNb = _allowedTerminalNb;
            isRemote = _isRemote;

            initialAvailableTimeIntervals.AddRange(_initialAvailableTimeIntervals);
            initialAvailableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));

            availableTimeIntervals.AddRange(_initialAvailableTimeIntervals);
            availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
        }

        public override string ToString()
        {
            return code;
        }

        /// <summary>
        /// True if when the check is done the Resource has at least one flight allocated in the given interval.
        /// </summary>
        public bool isOccupied(TimeInterval currentOccupationInterval)
        {
            if (flightAllocations != null
                && flightAllocations.Count > 0)
            {
                foreach (FlightResourceAllocation flightAllocation in flightAllocations)
                {
                    if (currentOccupationInterval.intersectsTimeInterval(flightAllocation.occupationInterval))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool hasAvailableInterval(TimeInterval occupiedTimeInterval)
        {
            //quick fix for v6.177 overlapping flights with  F1.endDate = F2.startDate
            TimeInterval adjustedOccupiedTimeInterval = new TimeInterval(occupiedTimeInterval.fromDate.AddMinutes(1), occupiedTimeInterval.toDate);

            availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
            foreach (TimeInterval availableInterval in availableTimeIntervals)
            {
                if (availableInterval.containsTimeInterval(adjustedOccupiedTimeInterval))
                    return true;
            }
            return false;
        }

        public void allocateFlightToResource(Flight allocatedFlight, TimeInterval occupiedInterval,
            int delayBetweenFlightsInMinutes)
        {
            //add flight to allocated flights list
            FlightResourceAllocation flightAllocation
                = new FlightResourceAllocation(allocatedFlight, this, occupiedInterval);
            flightAllocations.Add(flightAllocation);
            //remove occupied interval from available
            removeOccupiedIntervalFromAvailableIntervalsWhenIntersecting(occupiedInterval);//removeOccupiedIntervalFromAvailableIntervalsWhenContaining(occupiedInterval);
            //remove delay(after allocated flight) interval from available if necessary
            if (delayBetweenFlightsInMinutes > 0)
                addDelayAfterTheAllocatedFlight(occupiedInterval, delayBetweenFlightsInMinutes);
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

        private void addDelayAfterTheAllocatedFlight(TimeInterval occupiedTimeInterval, int delayBetweenFlightsInMinutes)
        {
            DateTime delayEndDate = occupiedTimeInterval.toDate.AddMinutes(delayBetweenFlightsInMinutes);
            TimeInterval delayInterval = new TimeInterval(occupiedTimeInterval.toDate, delayEndDate);
            removeOccupiedIntervalFromAvailableIntervalsWhenIntersecting(delayInterval);
        }

        public bool isAllocatedToFlightAtOccupationInterval(Flight flight,
            TimeInterval occupationInterval)
        {
            if (flight == null || occupationInterval == null)
                return false;
            if (flightAllocations != null && flightAllocations.Count > 0)
            {
                foreach (FlightResourceAllocation flightAlloc in flightAllocations)
                {
                    if (flightAlloc.allocatedFlight.id == flight.id
                        && flightAlloc.occupationInterval.fromDate == occupationInterval.fromDate
                        && flightAlloc.occupationInterval.toDate == occupationInterval.toDate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
