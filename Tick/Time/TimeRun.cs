using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tick.Time
{
    abstract class TimeRun : IDisposable
    {
        DispatcherTimer _timer;
        public TimeRun()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerHandler;
        }

        protected abstract void TimerHandler(object sender, EventArgs e);

        public void Start() => _timer.Start();
        public void Close() => _timer.Stop();
        public void Dispose()
        {
            Close();
        }

    }
}
