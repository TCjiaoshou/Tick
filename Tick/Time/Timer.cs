using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Time
{
    class Timer
    {
        public Timer() : this(DateTime.Now) { }
        public Timer(DateTime date) : this(new Time(date.Hour, date.Minute, date.Second)) { }
        public Timer(Time time)
        {
            _time = time;
            Checked();
        }
        Time _time;
        public Time GetTime() => _time;

        //检查时间的有效性
        public bool Checked()
        {
            if (_time.Hour >= DateTime.Now.Hour && _time.Minute >= DateTime.Now.Minute && _time.Second >= DateTime.Now.Second)
                return true;
            return false;
        }

        #region operator overload
        public static bool operator ==(Timer timer,DateTime date)
        {
            if (timer._time.Hour == date.Hour && timer._time.Minute == date.Minute && timer._time.Second == date.Second)
                return true;
            return false;
        }
        public static bool operator !=(Timer timer, DateTime date) => !(timer == date);
        public static bool operator ==(DateTime date,Timer timer) => timer == date;
        public static bool operator !=(DateTime date, Timer timer) => !(timer == date);
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
