using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class Flight
    {
        internal const string SCHENGEN_FLIGHT_CATEGORY_PREFIX = "SCH";
        //internal const string NON_SCHENGEN_FLIGHT_CATEGORY_PREFIX = "NSCH";      

        internal static List<string> twoParkingStandsAircraftBodyCategories
            = new List<string>(new string[] { AircraftType.AIRCTAFT_BODY_CATEGORY_E, AircraftType.AIRCTAFT_BODY_CATEGORY_F });

        internal int id { get; set; }
        internal DateTime date { get; set; }
        internal TimeSpan time { get; set; }
        internal string airlineCode { get; set; }
        internal string flightNumber { get; set; }
        internal string airportCode { get; set; }
        internal string flightCategory { get; set; }
        internal string aircraftType { get; set; }
        internal int nbSeats { get; set; }

        internal bool tsa { get; set; }
        internal bool noBSM { get; set; }
        internal bool cbp { get; set; }

        internal int flightPlanGateTerminalNb { get; set; }
        internal int flightPlanParkingTerminalNb { get; set; }
        internal int flightPlanParkingStandNb { get; set; }
        internal int flightPlanRunwayNb { get; set; }

        internal int flightPlanArrivalGateNb { get; set; }
        internal int flightPlanReclaimTerminalNb { get; set; }
        internal int flightPlanReclaimDeskNb { get; set; }
        internal int flightPlanInfeedTerminalNb { get; set; }
        internal int flightPlanArrivalInfeedStartDeskNb { get; set; }
        internal int flightPlanArrivalInfeedEndDeskNb { get; set; }
        internal int flightPlanTransferInfeedNb { get; set; }

        internal int flightPlanCheckInTerminalNb { get; set; }
        internal int flightPlanEcoCheckInStartDeskNb { get; set; }
        internal int flightPlanEcoCheckInEndDeskNb { get; set; }
        internal int flightPlanFBCheckInStartDeskNb { get; set; }
        internal int flightPlanFBCheckInEndDeskNb { get; set; }

        internal int flightPlanEcoBagDropStartDeskNb { get; set; }
        internal int flightPlanEcoBagDropEndDeskNb { get; set; }
        internal int flightPlanFBBagDropStartDeskNb { get; set; }
        internal int flightPlanFBBagDropEndDeskNb { get; set; }

        internal int flightPlanBoardingGateNb { get; set; }

        internal int flightPlanMupTerminalNb { get; set; }
        internal int flightPlanEcoMupStartDeskNb { get; set; }
        internal int flightPlanEcoMupEndDeskNb { get; set; }
        internal int flightPlanFBMupStartDeskNb { get; set; }
        internal int flightPlanFBMupEndDeskNb { get; set; }
        

        internal int fpAsBasisResourceToAllocateStartIndex { get; set; }
        internal int fpAsBasisResourceToAllocateEndIndex { get; set; }

        internal int dummyFlightBoardingGateNb { get; set; }
        internal int dummyFlightParkingStandNb { get; set; }

        internal string user1 { get; set; }
        internal string user2 { get; set; }
        internal string user3 { get; set; }
        internal string user4 { get; set; }
        internal string user5 { get; set; }

        internal string specificGroundHandler { get; set; }

        internal TimeInterval occupiedInterval { get; set; }
        internal TimeInterval pierOccupiedInterval { get; set; }
        internal List<Resource> allocatedResources = new List<Resource>();

        internal ResourceOccupationNeed entireAllocationOccupationNeed { get; set; }
        internal ResourceOccupationNeed partialAllocationOccupationNeed { get; set; }

        internal Flight linkedFlight { get; set; }

        internal string aircraftBodyCategory { get; set; }

        internal bool allocatedFromFlightPlan { get; set; }
        internal bool withResourceFromFPnotFoundInAvailable { get; set; }
        internal bool noSpaceOnFPIndicatedResource { get; set; }
        internal bool noSpaceOnPierOfFPIndicatedResource { get; set; }
        internal bool notAllowedToUseTheResourceFromFP { get; set; }

        internal bool allocatedWithAlgorithm { get; set; }
        internal bool allocatedWithAlgorithmByBreakingSchengenConstraint { get; set; }
        internal bool allocatedWithAlgorithmByAllowingBoardingRoomOverlapping { get; set; }
        internal bool allocatedUsingOCT { get; set; }
        internal bool allocatedUsingOCTExceptions { get; set; }

        internal bool noSpaceOnPier { get; set; }

        internal List<Flight> overlappingFlights = new List<Flight>();

        internal int nbNeededResources { get; set; }

        public bool isSchengen()
        {
            if (flightCategory != null
                && flightCategory.StartsWith(SCHENGEN_FLIGHT_CATEGORY_PREFIX))
            {
                return true;
            }
            return false;
        }

        public bool needsTwoParkingStands()
        {
            if (aircraftBodyCategory != null
                && twoParkingStandsAircraftBodyCategories.Contains(aircraftBodyCategory))
            {
                return true;
            }
            return false;
        }

        public bool needsTwoBoardingRooms(int nbSeatsLowerLimitForLargeAircrafts)
        {
            if (nbSeats >= nbSeatsLowerLimitForLargeAircrafts)
                return true;
            return false;
        }

        public bool canBeOverlappedAtCheckIn()
        {
            if (entireAllocationOccupationNeed != null
                && entireAllocationOccupationNeed.nbAdditionalStations > 0)
            {
                return true;
            }
            return false;
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

        public List<Resource> getResourcesAllocatedWhileOverlapping(List<Resource> allocatedResourcesInGivenInterval)
        {
            List<Resource> otherResources = new List<Resource>();
            foreach (Resource allocatedResource in allocatedResources)
            {
                if (!allocatedResourcesInGivenInterval.Contains(allocatedResource))                
                    otherResources.Add(allocatedResource);
            }
            return otherResources;
        }
    }
}
