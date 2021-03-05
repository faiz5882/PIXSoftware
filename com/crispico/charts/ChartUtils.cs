using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Nevron.GraphicsCore;

namespace SIMCORE_TOOL.com.crispico.charts
{
    public class ChartUtils
    {
        public enum CHART_POSITIONS : int { TopLeft = 0, TopRight = 1, BottomLeft = 2, BottomRight = 3 };

        public static List<SetPoint> retrieveSetPoints(List<double> Value_, List<double> Value2_, List<DateTime> beginDateTime_, List<DateTime> endDateTime_,
            List<bool> isArea_, List<bool> isActivated_, List<string> axis_, List<CHART_POSITIONS> chartPositions, ArrayList listSetPointStrokeColor_,
            ArrayList listSetPointFillColor_)
        {
            List<SetPoint> setPoints = new List<SetPoint>();
            for (int i = 0; i < Value_.Count; i++)
            {
                SetPoint setPoint = getSetPointByIndex(i, Value_, Value2_, beginDateTime_, endDateTime_, isArea_, isActivated_, axis_, 
                                                        chartPositions, listSetPointStrokeColor_, listSetPointFillColor_);
                if (setPoint != null)
                    setPoints.Add(setPoint);
            }
            return setPoints;
        }

        private static SetPoint getSetPointByIndex(int setPointIndex, List<double> Value_, List<double> Value2_, List<DateTime> beginDateTime_, 
            List<DateTime> endDateTime_, List<bool> isArea_, List<bool> isActivated_, List<string> axis_, List<CHART_POSITIONS> chartPositions, 
            ArrayList listSetPointStrokeColor_, ArrayList listSetPointFillColor_)
        {
            if (!validIndexForList(Value_, setPointIndex) || !validIndexForList(Value2_, setPointIndex) || !validIndexForList(beginDateTime_, setPointIndex)
                || !validIndexForList(endDateTime_, setPointIndex) || !validIndexForList(listSetPointStrokeColor_, setPointIndex)
                || !validIndexForList(listSetPointFillColor_, setPointIndex) || !validIndexForList(isArea_, setPointIndex)
                || !validIndexForList(isActivated_, setPointIndex) || !validIndexForList(axis_, setPointIndex) || !validIndexForList(chartPositions, setPointIndex))
            {
                return null;
            }
            SetPoint setPoint = new SetPoint();
            setPoint.numericStartValue = Value_[setPointIndex];
            setPoint.numericEndValue = Value2_[setPointIndex];
            setPoint.dateStartValue = beginDateTime_[setPointIndex];
            setPoint.dateEndValue = endDateTime_[setPointIndex];
            setPoint.isArea = isArea_[setPointIndex];
            setPoint.isActivated = isActivated_[setPointIndex];
            setPoint.axis = axis_[setPointIndex];
            setPoint.strokeColor = (NStrokeStyle)listSetPointStrokeColor_[setPointIndex];
            setPoint.fillColor = (NStrokeStyle)listSetPointFillColor_[setPointIndex];
            setPoint.chartPosition = chartPositions[setPointIndex];
            return setPoint;
        }

        private static bool validIndexForList(IList list, int index)
        {
            if (list.Count > 0 && index >= 0 && index < list.Count)
                return true;
            return false;
        }
    }
}
