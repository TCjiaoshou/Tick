using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Time
{
    class Delay
    {
        public Delay() : this(0, 0, 3) { }
        public Delay(int second) : this(0, 0, second) { }
        public Delay(int minute, int second) : this(0, minute, second) { }
        public Delay(int hour,int minute,int second)
        {
            _time = new Time(hour, minute, second);
        }
        public Delay(Time time)
        {
            _time = time;
        }
        public Time _time;
        public Time GetTime() => _time;
    }
}
