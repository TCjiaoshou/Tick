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
using System.Diagnostics;

using Tick.Configure;
namespace Tick
{
    /// <summary>
    /// CompetitionTimerPage.xaml 的交互逻辑
    /// </summary>
    public partial class CompetitionPage : Page
    {
        private struct Time
        {
            public Time(int H, int M, int S)
            {
                Hour = H;
                Minute = M;
                Second = S;
            }
            public int Hour;
            public int Minute;
            public int Second;

            public static bool operator ==(Time time, int value)
            {
                if (time.Hour == value && time.Minute == value && time.Second == value)
                    return true;
                else
                    return false;
            }
            public static bool operator !=(Time time, int value) => !(time == value);

            public static bool operator ==(Time time, DateTime date)
            {
                if (time.Hour == date.Hour && time.Minute == date.Minute && time.Second == date.Second)
                    return true;
                else
                    return false;
            }
            public static bool operator !=(Time time, DateTime date) => !(time == date);

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        //private const uint masterTime = 10_800;     // 3 hour = 3600 * 3 * 1000 = 10,800,000 millisecond
        private const string Begin = "Start";
        private const string End = "End";

        Time endTime;

        private Time time;
        private uint overtime = 0;
        DispatcherTimer timer;

        public CompetitionPage()
        {
            InitializeComponent();

            time = new Time(3, 0, 0);
            TimerFun timerFun = Countdown;
            bool startOvertime = false;
            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
              {
                  timerFun(ref time);
                  if (overtime != 0 && time == 0)
                  {
                      startOvertime = true;
                      timerFun = PositiveTiming;
                  }
                  else if (!startOvertime && time == 0)
                  {
                      timer.Stop();
                  }
                  else if(startOvertime && time.Minute == overtime)
                  {
                      timer.Stop();
                  }
                  if (endTime == DateTime.Now)
                  {
                      Data.AddAppLog($"Time: {time.Hour}-{time.Minute}-{time.Second} .Deviation: {time.Hour*3600+time.Minute*60+time.Second} s");
                  }
              };


        }

        //计时函数委托
        private delegate void TimerFun(ref Time time);
        //倒计时
        private void Countdown(ref Time time)
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
            txtTime.Text = $"{time.Hour}:{time.Minute}:{time.Second}";
        }
        //正计时
        private void PositiveTiming(ref Time time)
        {
            time.Second++;
            if(time.Second==60)
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
            txtMinTime.Text = $"{time.Hour}:{time.Minute}:{time.Second}";
        }

        #region get overtime, and content millisecond
        private void setOvertime(uint value)
        {
            overtime = value;
            comboTem.IsDropDownOpen = false;
            if (value != 0)
                MainWindow.OperateMessage($"Overtime {value} Minute");
            else
                MainWindow.OperateMessage("Close Overtime!");
        }
        private void btnZero_Click(object sender, RoutedEventArgs e) { setOvertime(0); }
        private void btnFive_Click(object sender, RoutedEventArgs e) { setOvertime(5); }

        private void btnTen_Click(object sender, RoutedEventArgs e) { setOvertime(10); }

        private void btnFifteen_Click(object sender, RoutedEventArgs e) { setOvertime(15); }

        private void btnTwenty_Click(object sender, RoutedEventArgs e) { setOvertime(20); }

        private void btnTwentyFive_Click(object sender, RoutedEventArgs e) { setOvertime(25); }

        private void btnThirty_Click(object sender, RoutedEventArgs e) { setOvertime(30); }

        private void btnOpenCombo_Click(object sender, RoutedEventArgs e) => comboTem.IsDropDownOpen = true;
        #endregion

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (btnStart.Content.Equals(Begin))
            {
                string str = "这是一个误差比较小的功能，但为了减小误差禁用了很多功能和漂亮的UI" +
                            "\n一但开始中途就不能暂停，直至计时结束" +
                            "\n但可以设置加时选项" +
                            "\n准备好了吗";
                //message check!!
                if (MessageBox.Show(str, "使用规则", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    btnStart.Content = End;
                    btnStart.Visibility = Visibility.Hidden;
                    MainWindow.OperateMessage("比赛开始");
                    timer.Start();

                    endTime = new Time(DateTime.Now.Hour + 3, DateTime.Now.Minute, DateTime.Now.Second);
                }
            }
            else
            {
                btnStart.Content = Begin;
                original();
            }
        }
        private void original()
        {
            //txtMilisecond.Text = ".00";
            //txtMinMillisecond.Text = ".01";
            txtMinTime.Text = "0:00:00";
            txtTime.Text = "3:00:00";
            time.Hour = 3;
            time.Minute = 0;
            time.Second = 0;
            gridMinTimer.Visibility = Visibility.Hidden;
        }
    }
}
