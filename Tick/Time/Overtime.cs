using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Time
{
    class Overtime
    {
        public Overtime() : this(0, 0, 0) { }
        public Overtime(int second) : this(0, 0, second) { }
        public Overtime(int minute, int second) : this(0, minute, second) { }
        public Overtime(int hour, int minute, int second)
        {
            _time = new Time(hour, minute, second);
        }
        public Overtime(Time time)
        {
            _time = time;
        }

        Time _time;
        public Time GetTime() => _time;
    }
}
