using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Time
{
    class HourMeter
    {
        public HourMeter() : this(3, 0, 0) { }
        public HourMeter(Time time)
        {
            _time = time;
        }
        public HourMeter(int hour, int minute, int second)
        {
            _time = new Time(hour, minute, second);
        }
        Time _time;
        public Time GetTime() => _time;
    }
}
