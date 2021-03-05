using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Vinci
{
    class FlightResourcesStats
    {
        public int flightId { get; set; }
        public int nbPaxProcessedAtCheckIn { get; set; }
        public int nbPaxMissedAtCheckIn { get; set; }
        public int nbPaxStoppedAtCheckIn { get; set; }
        
        public int nbPaxProcessedAtBoardingGate { get; set; }
        public int nbPaxMissedAtBoardingGate { get; set; }
        public int nbPaxStoppedAtBoardingGate { get; set; }
        
        public int nbPaxProcessedAtReclaim { get; set; }
        public int nbPaxMissedAtReclaim { get; set; }
        public int nbPaxStoppedAtReclaim { get; set; }
        
        public int nbPaxProcessedAtArrivalGate { get; set; }
        public int nbPaxMissedAtArrivalGate { get; set; }
        public int nbPaxStoppedAtArrivalGate { get; set; }

        public int sumOfMissed { get; set; }
        public double percOfMissed { get; set; }
        public int sumOfStopped { get; set; }
        public double percOfStopped { get; set; }

        public double minutesFromEntryToBgMinValue { get; set; }
        public double minutesFromEntryToBgAvgValue { get; set; }
        public double minutesFromEntryToBgMaxValue { get; set; }

        public double minutesFromEntryToBgLevel1Value { get; set; }
        public double minutesFromEntryToBgLevel2Value { get; set; }
        public double minutesFromEntryToBgLevel3Value { get; set; }

        public List<double> minutesFromEntryToBg = new List<double>();

        public double minutesFromArrGateToBagClaimMinValue { get; set; }
        public double minutesFromArrGateToBagClaimAvgValue { get; set; }
        public double minutesFromArrGateToBagClaimMaxValue { get; set; }

        public double minutesFromArrGateToBagClaimLevel1Value { get; set; }
        public double minutesFromArrGateToBagClaimLevel2Value { get; set; }
        public double minutesFromArrGateToBagClaimLevel3Value { get; set; }

        public List<double> minutesFromArrGateToBagClaim = new List<double>();
    }
}
