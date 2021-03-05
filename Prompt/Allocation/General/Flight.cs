using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIMCORE_TOOL.Prompt.Liege;

namespace SIMCORE_TOOL.Prompt.Allocation.General
{
    class Flight
    {
        internal int id { get; set; }
        internal DateTime date { get; set; }
        internal TimeSpan time { get; set; }
        internal string airlineCode { get; set; }
        internal string flightNumber { get; set; }
        internal string airportCode { get; set; }
        internal string flightCategory { get; set; }
        internal string aircraftType { get; set; }
        public Boolean tsa { get; set; }
        public Int32 nbSeats { get; set; }

        public int parkingTerminalNb { get; set; }

        public String user1 { get; set; }
        public String user2 { get; set; }
        public String user3 { get; set; }
        public String user4 { get; set; }
        public String user5 { get; set; }

        public string allocationCode
        {
            get
            {
                if (user5 == null || user5.Length == 0)
                    return "";
                return user5.Substring(0, 1);                
            }
        }

        public int minutesFromArrivalToDeparture { get; set; }

        internal TimeInterval occupiedInterval { get; set; }
        internal List<Resource> allocatedResources = new List<Resource>();

        public override string ToString()
        {
            DateTime flightCompleteDate = date.Add(time);
            return id + " - " + flightCompleteDate + " - " + flightNumber;
        }
                
        public List<Resource> getAllocatedResourcesByOccupationInterval(TimeInterval occupationInterval)
        {
            List<Resource> resourcesFound = new List<Resource>();
            foreach (Resource allocatedResource in allocatedResources)
            {
                if (allocatedResource.isAllocatedToFlightAtOccupationInterval(this, occupationInterval)
                    && !resourcesFound.Contains(allocatedResource))
                {
                    resourcesFound.Add(allocatedResource);
                }
            }
            return resourcesFound;
        }

        
    }
}
