using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.com.crispico.general_allocation.model;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class Resource
    {
        internal int id { get; set; }           // ex: 1
        internal string name { get; set; }      // ex: Parking_22
        internal string code { get; set; }      // ex: P_22
        internal int codeId { get; set; }       // ex: 22

        internal int terminalNb { get; set; }
        internal string type { get; set; }
        
        public List<TimeInterval> initialAvailableTimeIntervals = new List<TimeInterval>();//to keep the initial intervals - will not be used in the allocation
        public List<TimeInterval> availableTimeIntervals = new List<TimeInterval>();
        public List<FlightAllocation> flightAllocations = new List<FlightAllocation>();

        public Resource pier { get; set; }

        public Zone zone { get; set; }

        public Resource(int _id, string _name, string _code, int _codeId,
            List<TimeInterval> _initialAvailableTimeIntervals)
        {
            id = _id;
            name = _name;
            code = _code;
            codeId = _codeId;

            initialAvailableTimeIntervals.AddRange(_initialAvailableTimeIntervals);
            initialAvailableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));

            availableTimeIntervals.AddRange(_initialAvailableTimeIntervals);
            availableTimeIntervals.Sort((x, y) => DateTime.Compare(x.fromDate, y.fromDate));
        }

        public Resource(int id, string resourceType, int terminalNb)
        {
            this.id = id;
            this.type = resourceType;
            this.terminalNb = terminalNb;
            this.name = type + "_" + id + " / T_" + terminalNb;
        }

        /// <summary>
        /// True if when the check is done all Flights that are allocated to the resource are Schengen.
        /// False is at least one is Non-Schengen or if the resource doesn't have any flight allocated.
        /// </summary>
        public bool isCurrentlyOccupiedOnlyBySchengen(TimeInterval currentOccupationInterval)
        {
            if (isOccupied(currentOccupationInterval))
            {
                foreach (FlightAllocation flightAllocation in flightAllocations)
                {
                    if (currentOccupationInterval.intersectsTimeInterval(flightAllocation.occupationInterval))
                    {
                        Flight allocatedFlight = flightAllocation.allocatedFlight;
                        if (!allocatedFlight.isSchengen())
                            return false;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// True if when the check is done all Flights that are allocated to the resource are Non-Schengen.
        /// False is at least one is Schengen or if the resource doesn't have any flight allocated.
        /// </summary>
        public bool isCurrentlyOccupiedOnlyByNonSchengen(TimeInterval currentOccupationInterval)
        {
            if (isOccupied(currentOccupationInterval))
            {
                foreach (FlightAllocation flightAllocation in flightAllocations)
                {
                    if (currentOccupationInterval.intersectsTimeInterval(flightAllocation.occupationInterval))
                    {
                        Flight allocatedFlight = flightAllocation.allocatedFlight;
                        if (allocatedFlight.isSchengen())
                            return false;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// True if when the check is done the Resource has at least one flight allocated in the given interval.
        /// </summary>
        public bool isOccupied(TimeInterval currentOccupationInterval)
        {
            if (flightAllocations != null
                && flightAllocations.Count > 0)
            {
                foreach (FlightAllocation flightAllocation in flightAllocations)
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
                if (availableInterval.containsTimeInterval(adjustedOccupiedTimeInterval))//occupiedTimeInterval))
                    return true;
            }
            return false;
        }
        public void allocateFlightToResource(Flight allocatedFlight, TimeInterval occupiedInterval,
            int delayBetweenFlightsInMinutes)
        {
            //add flight to allocated flights list
            FlightAllocation flightAllocation
                = new FlightAllocation(allocatedFlight, this, occupiedInterval);
            flightAllocations.Add(flightAllocation);
            //remove occupied interval from available
            removeOccupiedIntervalFromAvailableIntervalsWhenIntersecting(occupiedInterval);//removeOccupiedIntervalFromAvailableIntervalsWhenContaining(occupiedInterval);
            //remove delay(after allocated flight) interval from available if necessary
            if (delayBetweenFlightsInMinutes > 0)
                addDelayAfterTheAllocatedFlight(occupiedInterval, delayBetweenFlightsInMinutes);
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

        public bool isAllocatedToFlightAtOccupationInterval(Flight flight,
            TimeInterval occupationInterval)
        {
            if (flight == null || occupationInterval == null)
                return false;
            if (flightAllocations != null && flightAllocations.Count > 0)
            {
                foreach (FlightAllocation flightAlloc in flightAllocations)
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

        public bool isAvailableForFlight(Flight flight, TimeInterval occupationInterval,
            List<Flight> ignoredFlights)
        {
            if (flight == null || occupationInterval == null
                || ignoredFlights == null)
            {
                return false;
            }
            foreach (FlightAllocation flightAllocation in flightAllocations)
            {
                if (!ignoredFlights.Contains(flightAllocation.allocatedFlight)
                    && flightAllocation.occupationInterval.intersectsTimeInterval(occupationInterval))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
