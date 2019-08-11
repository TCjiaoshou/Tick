using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Time
{
    class OvertimeRun:TimeRun
    {
        Time _time = new Time();
        Time _overtime;
        public Time GetTime() => _time;
        public OvertimeRun(Overtime overtime) : base()
        {
            _overtime = overtime.GetTime();
        }

        public delegate void OvertimeEvent(object sender);
        public event OvertimeEvent OvertimeEnd;
        public event OvertimeEvent Overtiming;

        protected override void TimerHandler(object sender, EventArgs e)
        {
            _time++;
            if (Overtiming != null)
                Overtiming.Invoke(this);
            if (_time == _overtime)
            {
                if (OvertimeEnd != null)
                    OvertimeEnd.Invoke(this);
                Close();
            }
        }
    }
}
