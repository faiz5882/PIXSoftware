using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.Prompt.Liege
{
    class DayWithTimeIntervals
    {
        internal DayOfWeek dayOfWeek { get; set; }
        internal List<HourMinuteTimeInterval> hourMinuteIntervals = new List<HourMinuteTimeInterval>();

        public DayWithTimeIntervals(DayOfWeek _dayOfWeek,
            List<HourMinuteTimeInterval> _hourMinuteIntervals)
        {
            dayOfWeek = _dayOfWeek;
            hourMinuteIntervals.AddRange(_hourMinuteIntervals);
        }

        internal class HourMinuteTimeInterval
        {
            internal int startHour { get; set; }
            internal int startMinute { get; set; }
            internal int endHour { get; set; }
            internal int endMinute { get; set; }

            public HourMinuteTimeInterval(int _startHour, int _startMinute,
                int _endHour, int _endMinute)
            {
                startHour = _startHour;
                startMinute = _startMinute;
                endHour = _endHour;
                endMinute = _endMinute;
            }
        }
    }
}
