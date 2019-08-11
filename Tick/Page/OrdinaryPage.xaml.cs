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
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

using Tick.Configure;
using Tick.Time;
namespace Tick
{
    /// <summary>
    /// OrdinaryTimerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OrdinaryPage : Page
    {
        private Time.Time userTime;
        HourMeterRun hourMeterRun = new HourMeterRun(Data.HourMeter);
        DelayRun delayRun = new DelayRun(Data.Delayer);
        TimerRun timerRun = new TimerRun(Data.Timer);
        OvertimeRun overtimeRun = new OvertimeRun(Data.OverTimer);

        SoundPlayer sp = new SoundPlayer();

        public OrdinaryPage()
        {
            InitializeComponent();

            Data.AddAppLog("ordinary page show");
            updataFun();

            showRecordEvent += showRecordFun;
            updataEvent += updataFun;
            tickContorlThemeEvent += tickContorlThemeFuna;

            if (Setting.isShowRecord)
            {
                btnRecordA.Visibility = Visibility.Visible;
                btnRecordB.Visibility = Visibility.Visible;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (iconStart.Kind == PackIconKind.Play)
            {
                iconStart.Kind = PackIconKind.Pause;
                btnReset.IsEnabled = false;
                Status.isEnd = false;
                if (Setting.isTimer)
                {
                    if (timerRun.Check())
                    {
                        timerRun.Start();
                        return;
                    }
                    else
                    {
                        MainWindow.OperateMessage(Message.TimeInvalid);
                    }
                }
                else if (Setting.isDelayer)
                {
                    delayRun.Start();
                }
                else
                {
                    hourMeterRun.Start();
                }
                Status.isStart = true;
            }
            else
            {
                if (Status.isNowOvertime)
                {
                    overtimeRun.Close();
                }
                else if (!Status.isStart)
                {
                    timerRun.Close();
                }
                else
                {
                    hourMeterRun.Close();
                }
                iconStart.Kind = PackIconKind.Play;
                btnReset.IsEnabled = true;
                Status.isStart = false;
                if (Status.isEnd)
                {
                    sp.Stop();
                }
            }

        }
        #region time run event
        private void timerEndEvent(object sender)
        {
            hourMeterRun.Start();
            Status.isStart = true;
        }
        private void delayEndEvent(object sender)
        {
            hourMeterRun.Start();
        }
        private void hourMeteringEvent(object sender)
        {
            HourMeterRun _hourMeterRun = sender as HourMeterRun;
            timeShow(_hourMeterRun.GetTime());
            progressBar.Value++;
            userTimeShow();
        }
        private void hourMeterEndEvent(object sendser)
        {
            if (Setting.isOverTimer)
            {
                Status.isNowOvertime = true;
                overtimeRun.Start();
            }
            else
            {
                musicPlay();
            }
        }
        private void overtimingEvent(object sender)
        {
            OvertimeRun _overtimeRun = sender as OvertimeRun;
            timeShow(_overtimeRun.GetTime());
            userTimeShow();
        }
        private void overtimeEndEvent(object sender)
        {
            musicPlay();
        }
        private void userTimeShow()
        {
            userTime++;
            userTimeShow(ref userTime);
        }
        private void userTimeShow(ref Time.Time time)
        {
            digiteHour.Value = time.Hour;
            digiteMinute.Value = time.Minute;
            digiteSecond.Value = time.Second;
        }
        private void timeShow(Time.Time timeContext)
        {
            showHour.Text = timeContext.Hour.ToString();
            showMinute.Text = timeContext.Minute.ToString("d2");
            showSecond.Text = timeContext.Second.ToString("d2");
        }
        private void musicPlay()
        {
            iconStart.Kind = PackIconKind.Stop;
            Status.isEnd = true;
            try
            {
                sp.SoundLocation = Status.MusicPath;
                sp.Play();
            }
            catch { }
        }

        #endregion

        #region convert record button event
        public static bool isShowRecord
        {
            set => showRecordEvent.Invoke(value);
        }
        private static event showRecordHandler showRecordEvent;
        private delegate void showRecordHandler(bool b);
        void showRecordFun(bool b)
        {
            if (b)
            {
                btnRecordA.Visibility = Visibility.Visible;
                btnRecordB.Visibility = Visibility.Visible;
            }
            else
            {
                btnRecordA.Visibility = Visibility.Hidden;
                btnRecordB.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Updata
        public static void Updata() => updataEvent.Invoke();
        private static event updataHandler updataEvent;
        private delegate void updataHandler();
        void updataFun()
        {
            userTime = new Time.Time();
            userTimeShow(ref userTime);

            Status.isNowOvertime = false;

            hourMeterRun = new HourMeterRun(Data.HourMeter);
            delayRun = new DelayRun(Data.Delayer);
            timerRun = new TimerRun(Data.Timer);
            overtimeRun = new OvertimeRun(Data.OverTimer);

            timerRun.TimerEnd += timerEndEvent;
            delayRun.DelayEnd += delayEndEvent;
            hourMeterRun.HourMeterEnd += hourMeterEndEvent;
            hourMeterRun.HourMetering += hourMeteringEvent;
            overtimeRun.OvertimeEnd += overtimeEndEvent;
            overtimeRun.Overtiming += overtimingEvent;

            showHour.Text = Data.HourMeter.GetTime().Hour.ToString();
            showMinute.Text = Data.HourMeter.GetTime().Minute.ToString("d2");
            showSecond.Text = Data.HourMeter.GetTime().Second.ToString("d2");

            digiteHour.Value = 0;
            digiteMinute.Value = 0;
            digiteSecond.Value = 0;

            progressBar.Maximum = Data.HourMeter.GetTime().Hour * 3600 + Data.HourMeter.GetTime().Minute * 60 + Data.HourMeter.GetTime().Second;
            progressBar.Value = 0;
        }
        #endregion
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            updataFun();
            RecordPage.Clear();
        }

        #region record click
        private void btnRecordA_Click(object sender, RoutedEventArgs e)
        {
            RecordPage.UserALogAdd();
        }
        private void btnRecordB_Click(object sender, RoutedEventArgs e)
        {
            RecordPage.UserBLogAdd();
        }
        #endregion

        #region Hidden Control
        private void btnShowTick_Click(object sender, RoutedEventArgs e)
        {
            if (Status.isShowTick)
            {
                Status.isShowTick = false;
                gridMasterTimer.Visibility = Visibility.Visible;
                gridMinTimer.Visibility = Visibility.Visible;

                btnStart.Visibility = Visibility.Visible;
                btnReset.Visibility = Visibility.Visible;
                if (Setting.isShowRecord)
                {
                    btnRecordA.Visibility = Visibility.Visible;
                    btnRecordB.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Status.isShowTick = true;
                gridMasterTimer.Visibility = Visibility.Hidden;
                gridMinTimer.Visibility = Visibility.Hidden;

                btnStart.Visibility = Visibility.Hidden;
                btnReset.Visibility = Visibility.Hidden;
                if (Setting.isShowRecord)
                {
                    btnRecordA.Visibility = Visibility.Hidden;
                    btnRecordB.Visibility = Visibility.Hidden;
                }
            }
        }
        #endregion

        #region tick contorl theme
        public static void TickContorlTheme(Theme theme)
        {
            tickContorlThemeEvent.Invoke(theme);
        }
        private static event tickContorlThemeHandler tickContorlThemeEvent;
        private delegate void tickContorlThemeHandler(Theme theme);
        public void tickContorlThemeFuna(Theme theme)
        {
            if (theme == Theme.Dark)
            {
                myTick.Dark();
            }
            if (theme == Theme.Light)
            {
                myTick.Light();
            }
        }
        #endregion

        ~OrdinaryPage()
        {
            Data.AddAppLog("ordinary pag close");
        }
    }
}
