using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Define_Step_Analysis : Form
    {
        public class AnalyseStep
        {
            #region Les variables de la classe.
            private int iAirportFlow_;
            private int iPassengerDistribution_;
            private int iPassengerDistributionLine_;
            private int iOccupation_;
            private int iUtilization_;
            private int iRemaining_;
            private int iRemainingLine_;
            #endregion

            #region les accesseurs de la classe.
            public int AirportFlow
            {
                get
                {
                    return iAirportFlow_;
                }
            }
            public int PassengerDistribution
            {
                get
                {
                    return iPassengerDistribution_;
                }
            }
            public int PassengerDistributionLine
            {
                get
                {
                    return iPassengerDistributionLine_;
                }
            }
            public int Occupation
            {
                get
                {
                    return iOccupation_;
                }
            }
            public int Utilization
            {
                get
                {
                    return iUtilization_;
                }
            }
            public int Remaining
            {
                get
                {
                    return iRemaining_;
                }
            }
            public int RemainingLine
            {
                get
                {
                    return iRemainingLine_;
                }
            }
            #endregion

            #region Les constructeurs de la classe.
            public AnalyseStep(int iAirportFlow, int iPassengerDistribution,
                               int iPassengerDistributionLine, int iOccupation,
                               int iUtilization, int iRemaining, int iRemainingLine)
            {
                iAirportFlow_ = iAirportFlow;
                iPassengerDistribution_ = iPassengerDistribution;
                iPassengerDistributionLine_ = iPassengerDistributionLine;
                iOccupation_ = iOccupation;
                iUtilization_ = iUtilization;
                iRemaining_ = iRemaining;
                iRemainingLine_ = iRemainingLine; 
                checkValues();
            }
            public AnalyseStep(String sAirportFlow, String sPassengerDistribution,
                   String sPassengerDistributionLine, String sOccupation,
                   String sUtilization, String sRemaining, String sRemainingLine )
            {
                Int32.TryParse(sAirportFlow, out iAirportFlow_);
                Int32.TryParse(sPassengerDistribution, out iPassengerDistribution_);
                Int32.TryParse(sPassengerDistributionLine, out iPassengerDistributionLine_);
                Int32.TryParse(sOccupation, out iOccupation_);
                Int32.TryParse(sUtilization, out iUtilization_);
                Int32.TryParse(sRemaining, out iRemaining_);
                Int32.TryParse(sRemainingLine, out iRemainingLine_);
                checkValues();
            }
            private void checkValues()
            {
                if (iAirportFlow_ == 0)
                    iAirportFlow_ = 15;
                if (iPassengerDistribution_ == 0)
                    iPassengerDistribution_ = 15;
                if (iPassengerDistributionLine_ == 0)
                    iPassengerDistributionLine_ = 5;
                if (iOccupation_ == 0)
                    iOccupation_ = 15;
                if (iUtilization_ == 0)
                    iUtilization_ = 15;
                if (iRemaining_ == 0)
                    iRemaining_ = 15;
                if (iRemainingLine_ == 0)
                    iRemainingLine_ = 5;
            }
            #endregion
        }
        private void Initialize(AnalyseStep step)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            if (step == null)
                step = new AnalyseStep(0, 0, 0, 0, 0, 0, 0);
            cb_AirportFlow.Text = step.AirportFlow.ToString();
            cb_PassengerDistrib.Text = step.PassengerDistribution.ToString();
            cb_PassengerDistributionLines.Text = step.PassengerDistributionLine.ToString();
            cb_Occupation.Text = step.Occupation.ToString();
            cb_Utilization.Text = step.Utilization.ToString();
            cb_Remaining.Text = step.Remaining.ToString();
            cb_RemainingLines.Text = step.RemainingLine.ToString();
        }
        public SIM_Define_Step_Analysis()
        {
            Initialize(null);
        }
        public SIM_Define_Step_Analysis(AnalyseStep step)
        {
            Initialize(step);
        }
        public AnalyseStep GetAnalyseStep()
        {
            return new AnalyseStep(cb_AirportFlow.Text, cb_PassengerDistrib.Text, cb_PassengerDistributionLines.Text,
                cb_Occupation.Text, cb_Utilization.Text, cb_Remaining.Text, cb_RemainingLines.Text);
        }
    }
}