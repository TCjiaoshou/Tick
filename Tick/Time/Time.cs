using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Time
{
    public struct Time
    {
        public Time(int hour = 0, int minute = 0, int second = 0)
        {
            _hour = hour;
            _minute = minute;
            _second = second;
        }

        #region GetSet
        private int _hour;
        private int _minute;
        private int _second;
        public int Hour
        {
            get => _hour;
            set
            {
                if (value > 24)
                    throw new Exception("hour out of range,range:0~24");
                _hour = value;
            }
        }
        public int Minute
        {
            get => _minute;
            set
            {
                if (value > 60)
                    throw new Exception("minute out of range,range:0~60");
                _minute = value;
            }
        }
        public int Second
        {
            get => _second;
            set
            {
                if (value > 60)
                    throw new Exception("Second out of range,range:0~60");
                _second = value;
            }
        }
        #endregion

        #region operator overload

        public static Time operator ++(Time time)
        {
            time.Second++;
            if (time.Second == 60)
            {
                time.Minute++;
                time.Second = 0;

                if (time.Minute == 60)
                    time.Minute = 0;
            }
            if (time.Minute == 0 && time.Second == 0)
            {
                time.Hour++;
            }

            return new Time(time.Hour, time.Minute, time.Second);
        }
        public static Time operator --(Time time)
        {
            if (time.Minute == 0 && time.Second == 0)
            {
                time.Hour--;
                time.Minute = 60;
            }
            if (time.Second == 0)
            {
                time.Minute--;
                time.Second = 60;
            }
            time.Second--;

            return new Time(time.Hour, time.Minute, time.Second);
        }
        public static bool operator ==(Time time1, Time time2)
        {
            if (time1.Hour == time2.Hour && time1.Minute == time2.Minute && time1.Second == time2.Second)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Time time1, Time time2) => !(time1 == time2);

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
