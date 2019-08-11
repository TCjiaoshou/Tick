using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tick
{
    public struct TimingSpan
    {
        public TimingSpan(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }

        #region operator ==

        public static bool operator ==(DateTime date, TimingSpan timing) => timing == date;
        public static bool operator !=(DateTime date, TimingSpan timing) => timing != date;

        public static bool operator ==(TimingSpan timing, DateTime date)
        {
            if (date.Hour == timing.Hour && date.Minute == timing.Minute && date.Second == timing.Second)
                return true;
            else
                return false;
        }
        public static bool operator !=(TimingSpan timing, DateTime date) => !(timing == date);

        public static bool operator ==(TimingSpan timing, TimingSpan date)
        {
            if (date.Hour == timing.Hour && date.Minute == timing.Minute && date.Second == timing.Second)
                return true;
            else
                return false;
        }
        public static bool operator !=(TimingSpan timing, TimingSpan date) => !(timing == date);

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


    public class TimingMessageEvent : EventArgs
    {
        public TimingMessageEvent(DateTime date, string text, ulong count)
        {
            Date = date;
            Context = text;
            Count = count;
        }

        public DateTime Date { get; }
        public string Context { get; }
        public ulong Count { get; }
    }

    /// <summary>
    /// TickControl.xaml 的交互逻辑
    /// </summary>
    public partial class TickControl : UserControl
    {
        private ulong count = 0; //runing time
        private ulong check = 1800;//30 minute

        const decimal number1 = 0.1M;
        const decimal number2 = 0.5M;

        private decimal hourHand;
        private decimal minuteHand;
        private decimal secondHand;

        public TickControl()
        {
            InitializeComponent();
            #region master
            //txtDay.Text = DateTime.Now.ToString("yy-MM-dd");                        //output Year Month and Day in today (not refresh tomorrew)
            digiteYear.Value = DateTime.Now.Year % 100;
            digiteMonth.Value = DateTime.Now.Month;
            digiteDay.Value = DateTime.Now.Day;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                //txtTime.Text = DateTime.Now.ToString("HH : mm : ss");               //output timer
                digiteHour.Value = DateTime.Now.Hour;
                digiteMinute.Value = DateTime.Now.Minute;
                digiteSecond.Value = DateTime.Now.Second;

                secondHand = DateTime.Now.Second * 6;                                   //get now second angle
                minuteHand = DateTime.Now.Minute * 6 + DateTime.Now.Second * number1;   //get now minute angle
                hourHand = DateTime.Now.Hour * 30;
                hourHand = (hourHand >= 360 ? hourHand - 360 : hourHand) + DateTime.Now.Minute * number2;      //get now hour angle

                rectangleHour.Angle = (double)hourHand;
                rectangleMinute.Angle = (double)minuteHand;
                rectangleSecond.Angle = (double)secondHand;


                count++;
                //timer output log event invoke
                if (LogMessage != null && count % check == 0)
                {
                    string str = $"Angle:Hour->{hourHand},Minute->{minuteHand},Second->{secondHand},Date->{hourHand / 30}:{minuteHand / 6}:{secondHand / 6}";
                    LogMessage.Invoke(this, new TimingMessageEvent(DateTime.Now, str, count));
                }
                //auto change night mode event invoke
                if (isNightModeStart && AutoNightMode != null)
                {
                    if (DateTime.Now == new TimingSpan(18, 0, 0))
                    {
                        isNightModeStart = false;
                        AutoNightMode.Invoke(this, new TimingMessageEvent(DateTime.Now, null, count));
                    }
                }
            };
            #endregion
            timer.Start();
        }
        //background image change 
        private readonly BitmapImage DaytimeDial = new BitmapImage(new Uri("/Tick;component/Image/DaytimeTick.png", UriKind.Relative));
        private readonly BitmapImage NightDial = new BitmapImage(new Uri("/Tick;component/Image/NightTick.png", UriKind.Relative));
        public void Light() => imgDial.Source = DaytimeDial;
        public void Dark() => imgDial.Source = NightDial;
        //check tick event
        public event EventHandler<TimingMessageEvent> LogMessage;
        //auto change night mode event
        public event EventHandler<TimingMessageEvent> AutoNightMode;
        private bool isNightModeStart = false;
        public void AutoNightModeStart() => isNightModeStart = true;
        public void AutoNightModeSpot() => isNightModeStart = false;
    }

    public delegate void EventHandler<LonMessageEvent>(object sender, TimingMessageEvent e);
}
