using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.FlightPlansUtils
{
    class FlightParameter
    {
        internal double nbOrigEcoPax { get; set; }
        internal double nbOrigFbPax { get; set; }

        internal double nbTransferEcoPax { get; set; }
        internal double nbTransferFbPax { get; set; }

        internal double nbTermEcoPax { get; set; }
        internal double nbTermFbPax { get; set; }

        internal double nbOrigEcoBags { get; set; }
        internal double nbOrigFbBags { get; set; }

        internal double nbTransferEcoBags { get; set; }
        internal double nbTransferFbBags { get; set; }

        internal double nbTermEcoBags { get; set; }
        internal double nbTermFbBags { get; set; }

        public bool hasNonNumericalFields
        {
            get
            {
                return double.IsNaN(this.nbOrigEcoPax) || double.IsNaN(this.nbOrigFbPax) || double.IsNaN(this.nbTransferEcoPax) || double.IsNaN(nbTransferFbPax)
                        || double.IsNaN(this.nbTermEcoPax) || double.IsNaN(this.nbTermFbBags) || double.IsNaN(this.nbOrigEcoBags) || double.IsNaN(this.nbOrigFbBags)
                        || double.IsNaN(this.nbTransferEcoBags) || double.IsNaN(nbTransferFbBags) || double.IsNaN(this.nbTermEcoBags) || double.IsNaN(this.nbTermFbBags);
            }
        }

        public FlightParameter()
        {
        }

        public FlightParameter(double pNbOrigEcoPax, double pNbOrigFbPax, double pNbTransferEcoPax, double pNbTransferFbPax, double pNbTermEcoPax, double pNbTermFbPax, 
            double pNbOrigEcoBags, double pNbOrigFbBags, double pNbTransferEcoBags, double pNbTransferFbBags, double pNbTermEcoBags, double pNbTermFbBags)
        { 
            nbOrigEcoPax = pNbOrigEcoPax;
            nbOrigFbPax = pNbOrigFbPax;

            nbTransferEcoPax = pNbTransferEcoPax;
            nbTransferFbPax = pNbTransferFbPax;

            nbTermEcoPax = pNbTermEcoPax;
            nbTermFbPax = pNbTermFbPax;

            nbOrigEcoBags = pNbOrigEcoBags;
            nbOrigFbBags = pNbOrigFbBags;

            nbTransferEcoBags = pNbTransferEcoBags;
            nbTransferFbBags = pNbTransferFbBags;

            nbTermEcoBags = pNbTermEcoBags;
            nbTermFbBags = pNbTermFbBags;
        }

        public bool hasSameParametersAsGivenFPParameter(FlightParameter givenFlightParameter)
        {
            if (givenFlightParameter == null)
            {
                return false;
            }
            if (hasSameNbOfPax(givenFlightParameter) && hasSameNbOfBags(givenFlightParameter))
            {
                return true;
            }
            return false;
        }

        private bool hasSameNbOfPax(FlightParameter givenFlightParameter)
        {
            if (givenFlightParameter == null)
            {
                return false;
            }
            if (this.nbOrigEcoPax != givenFlightParameter.nbOrigEcoPax || this.nbOrigFbPax != givenFlightParameter.nbOrigFbPax
                || this.nbTransferEcoPax != givenFlightParameter.nbTransferEcoPax || nbTransferFbPax != givenFlightParameter.nbTransferFbPax
                || this.nbTermEcoPax != givenFlightParameter.nbTermEcoPax || this.nbTermFbPax != givenFlightParameter.nbTermFbPax)
            {
                return false;
            }
            return true;
        }

        private bool hasSameNbOfBags(FlightParameter givenFlightParameter)
        {
            if (givenFlightParameter == null)
            {
                return false;
            }
            if (this.nbOrigEcoBags != givenFlightParameter.nbOrigEcoBags || this.nbOrigFbBags != givenFlightParameter.nbOrigFbBags
                || this.nbTransferEcoBags != givenFlightParameter.nbTransferEcoBags || nbTransferFbBags != givenFlightParameter.nbTransferFbBags
                || this.nbTermEcoBags != givenFlightParameter.nbTermEcoBags || this.nbTermFbBags != givenFlightParameter.nbTermFbBags)
            {
                return false;
            }
            return true;
        }


        internal void updateParameterByGivenFlightParameter(FlightParameter inputFlightParameter)
        {
            if (inputFlightParameter == null)
            {
                return;
            }
            this.nbOrigEcoPax = inputFlightParameter.nbOrigEcoPax;
            this.nbOrigFbPax = inputFlightParameter.nbOrigFbPax;

            this.nbTransferEcoPax = inputFlightParameter.nbTransferEcoPax;
            this.nbTransferFbPax = inputFlightParameter.nbTransferFbPax;

            this.nbTermEcoPax = inputFlightParameter.nbTermEcoPax;
            this.nbTermFbPax = inputFlightParameter.nbTermFbPax;

            this.nbOrigEcoBags = inputFlightParameter.nbOrigEcoBags;
            this.nbOrigFbBags = inputFlightParameter.nbOrigFbBags;

            this.nbTransferEcoBags = inputFlightParameter.nbTransferEcoBags;
            this.nbTransferFbBags = inputFlightParameter.nbTransferFbBags;

            this.nbTermEcoBags = inputFlightParameter.nbTermEcoBags;
            this.nbTermFbBags = inputFlightParameter.nbTermFbBags;
        }
    }
}
