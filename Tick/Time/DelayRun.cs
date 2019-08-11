using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tick.Time
{
    class DelayRun : TimeRun
    {
        public delegate void DelayEvent(object sender);
        public event DelayEvent DelayEnd;
        public event DelayEvent Delaing;
        Time _time;
        public DelayRun(Delay delay) : base() => _time = delay.GetTime();
        protected override void TimerHandler(object sender, EventArgs e)
        {
            _time--;
            if (Delaing != null)
                Delaing.Invoke(this);
            if (_time == new Time(0,0,0))
            {
                if (DelayEnd != null)
                    DelayEnd.Invoke(this);
                Close();
            }
        }
    }
}
