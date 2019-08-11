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
using Microsoft.Win32;

using Tick.Configure;

namespace Tick
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        private enum Status : int
        {
            Timer,
            HourMeter,
            OverTime,
        }


        Status statusEnum;

        public SettingPage()
        {
            InitializeComponent();

            checkThemeEvent += checkThemeContorlFuna;

        }
        private void Page_Load(object sender, RoutedEventArgs e)
        {
            string[] lage = { "English", "中文" };
            foreach(string str in lage)
            {
                comboLanguage.Items.Add(str);
            }
            statusEnum = Status.HourMeter;

            txtMusicPathShow.Text = Configure.Status.MusicPath;

            showTime(Data.HourMeter.GetTime());
            new Languages.ControlLoad().Loaded(this);
        }

        #region page convert
        private void btnOrdinary_Click(object sender, RoutedEventArgs e)
        {
            btnOrdinary.IsEnabled = false;
            btnCompetition.IsEnabled = true;
            MainWindow.ShowPage(MainWindow.OrdinaryPage);
        }
        private void btnCompetition_Click(object sender, RoutedEventArgs e)
        {
            btnCompetition.IsEnabled = false;
            btnOrdinary.IsEnabled = true;
            MainWindow.ShowPage(MainWindow.CompetitionPage);
        }
        #endregion
        #region show time
        private void btnOverTime_Click(object sender, RoutedEventArgs e)
        {
            statusEnum = Status.OverTime;
            showTime(Data.OverTimer.GetTime());
            MainWindow.OperateMessage(Message.SetOvertime);
        }
        private void btnHourMeter_Click(object sender, RoutedEventArgs e)
        {
            statusEnum = Status.HourMeter;
            showTime(Data.HourMeter.GetTime());
            MainWindow.OperateMessage(Message.SetHourMeter);
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            statusEnum = Status.Timer;
            showTime(new Time.Time(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            MainWindow.OperateMessage(Message.SetTimer);
        }
        private void showTime(Time.Time time) => timeContext.Text = $"{time.Hour.ToString()}:{time.Minute.ToString("d2")}:{time.Second.ToString("d2")}";
        #endregion
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string[] strs = timeContext.Text.Split(':');
            try
            {
                Time.Time time = new Time.Time(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]));
                switch (statusEnum)
                {
                    case Status.Timer:
                        Data.Timer = new Time.Timer(time); break;
                    case Status.OverTime:
                        Data.OverTimer = new Time.Overtime(time); break;
                    case Status.HourMeter:
                        Data.HourMeter = new Time.HourMeter(time); break;
                }
                OrdinaryPage.Updata();
            }
            catch { MainWindow.OperateMessage(Message.ParameterError); }
        }

        private void checkDelayer_Click(object sender, RoutedEventArgs e) =>
            Setting.isDelayer = (bool)((CheckBox)sender).IsChecked;
        private void checkTimer_Click(object sender, RoutedEventArgs e) =>
            btnTimer.IsEnabled = Setting.isTimer = (bool)((CheckBox)sender).IsChecked;
        private void checkOverTime_Click(object sender, RoutedEventArgs e) =>
            btnOverTime.IsEnabled = Setting.isOverTimer = (bool)((CheckBox)sender).IsChecked;

        private void checkIsShowRecord_Click(object sender, RoutedEventArgs e)
        {
            if (!Setting.isShowRecord)
            {
                Setting.isShowRecord = true;
                OrdinaryPage.isShowRecord = true;
            }
            else
            {
                Setting.isShowRecord = false;
                OrdinaryPage.isShowRecord = false;
            }
        }

        private void btnSelectMusicFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = ".wav";
            fd.Filter = "骚音乐|*.wav|all file|*.*";
            if (fd.ShowDialog() == true)
            {
                Configure.Status.MusicPath = fd.FileName;
                txtMusicPathShow.Text = fd.FileName;
            }
        }

        private void btnApplicationLogShow_Click(object sender, RoutedEventArgs e)
        {
            new ShowLogWindow().Show();
        }

        private void checkThemeConvert(object sender, RoutedEventArgs e)
        {
            if (!Configure.Status.isDark)
            {
                Configure.Status.isDark = true;
                ThemePassage.ThemeConvert(Theme.Dark);
            }
            else
            {
                Configure.Status.isDark = false;
                ThemePassage.ThemeConvert(Theme.Light);
            }
        }

        #region isChecked
        public static void CheckThemeIsChecked() => checkThemeEvent.Invoke();
        private static event checkThemeHandler checkThemeEvent;
        private delegate void checkThemeHandler();
        private void checkThemeContorlFuna() => checkTheme.IsChecked = Configure.Status.isDark;
        #endregion

        private void btnLanguage_Click(object sender, RoutedEventArgs e)
        {
            var xml = new ConfigXmlPassage(Config.ConfigXmlPath);
            switch (comboLanguage.Text)
            {
                case "English":
                    xml.SetLanguage(Languages.In.Region.English);break;
                case "中文":
                    xml.SetLanguage(Languages.In.Region.China);break;
                default:
                    throw new Exception("language erroe");
            }
        }
    }
}
