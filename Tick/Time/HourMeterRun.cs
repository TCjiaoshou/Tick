using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Time
{
    class HourMeterRun:TimeRun
    {
        Time _time;
        public Time GetTime() => _time;
        public HourMeterRun(HourMeter hourMeter) : base()
        {
            _time = hourMeter.GetTime();
        }

        public delegate void HourMeterEvent(object sender);
        public event HourMeterEvent HourMeterEnd;
        public event HourMeterEvent HourMetering;

        protected override void TimerHandler(object sender, EventArgs e)
        {
            _time--;
            if (HourMetering != null)
                HourMetering.Invoke(this);
            if (_time == new Time(0,0,0))
            {
                if (HourMeterEnd != null)
                    HourMeterEnd.Invoke(this);
                Close();
            }
        }
    }
}
