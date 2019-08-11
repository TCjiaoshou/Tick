using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Time
{
    class TimerRun:TimeRun
    {
        public delegate void TimerEvent(object sender);
        public event TimerEvent TimerEnd;
        public event TimerEvent Timering;
        Timer _time;
        public TimerRun(Timer timer) : base()
        {
            _time = timer;
        }
        public bool Check() => _time.Checked();
        protected override void TimerHandler(object sender, EventArgs e)
        {
            if (Timering != null)
                Timering.Invoke(this);
            if (_time == DateTime.Now)
            {
                if (TimerEnd != null)
                    TimerEnd.Invoke(this);
                Close();
            }
        }
    }
}
