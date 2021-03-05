using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.processFlow
{
    /// <summary>
    /// Is used to hold diffrent ProcessFlow parameters. 
    /// Ex.: to record the show (groups/connections) details check-boxes' state
    /// </summary>
    public class ProcessFlowParameters
    {
        /// <summary>
        /// Retains the value for the parameter that 
        /// shows/hider the names for the Groups on the ProcessFlow.
        /// </summary>
        public Boolean showNames { get; set; }

        /// <summary>
        /// Retains the value for the parameter that 
        /// shows/hides the descriptions for the Groups on the ProcessFlow.
        /// </summary>
        public Boolean showDescriptions { get; set; }

        /// <summary>
        /// Retains the value for the parameter that
        /// shows/hides the distributions for the Pax/Bag process times.
        /// Applies to the Groups on the ProcessFlow.
        /// </summary>
        public Boolean showProcessTimes { get; set; }

        /// <summary>
        /// Retains the value for the parameter that
        /// shows/hides the Queue capacity for the Groups on the ProcessFlow.
        /// </summary>
        public Boolean showAreaCapacity { get; set; }

        /// <summary>
        /// Retains the value for the parameter that
        /// shows/hides the distributions for the waiting time before
        /// entering the Groups' Queues.
        /// </summary>
        public Boolean showDelayTimesDistribution { get; set; }

        /// <summary>
        /// Retains the value for the parameter that
        /// shows/hides the weights for the Connections on the ProcessFlow.
        /// </summary>
        public Boolean showWeights { get; set; }

        /// <summary>
        /// Retains the value for the parameter that
        /// shows/hides the distribution for the travel times
        /// between Groups.Related to Connections.
        /// </summary>
        public Boolean showTravelTimes { get; set; }

        /// <summary>
        /// Retains the value for the parameter that
        /// makes the Connections on the itinerary invisible 
        /// </summary>
        public Boolean setInvisibleConnections { get; set; }

        /// <summary>
        /// Retains the value for the parameter that
        /// makes the Connections on the itinerary slightly visible.
        /// </summary>
        public Boolean setShaddowConnections { get; set; }

        public ProcessFlowParameters()
        {
        }

        public ProcessFlowParameters(bool pShowNames, bool pShowDescription, bool pShowProcessTimes,
            bool pShowAreaCapacity, bool pShowDelayTimeDistribution, bool pShowWeights, bool pShowTravelTimes,
            bool pSetInvisibleConnections, bool pSetShaddowConnections)
        {
            this.showNames = pShowNames;
            this.showDescriptions = pShowDescription;
            this.showProcessTimes = pShowProcessTimes;
            this.showAreaCapacity = pShowAreaCapacity;
            this.showDelayTimesDistribution = pShowDelayTimeDistribution;
            this.showWeights = pShowWeights;
            this.showTravelTimes = pShowTravelTimes;
            this.setInvisibleConnections = pSetInvisibleConnections;
            this.setShaddowConnections = pSetShaddowConnections;
        }
    }
}
