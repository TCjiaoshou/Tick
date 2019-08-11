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


// English not good!
namespace Tick
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        enum PageShow : int
        {
            Timer,
            Log,
            Set
        }


        public MainWindow()
        {
            SetFile.ImportFile();// 导入保存的设置
            InitializeComponent();
            //注册事件
            opertitionMessageEvent += opertition;
            showPageEvent += timerHandlerFun;
            themeEvent += themeFuna;
        }

        private void nightMode(object sender, TimingMessageEvent e)
        {
            if (!Status.isDark)
            {
                Status.isDark = true;
                ThemePassage.ThemeConvert(Theme.Dark);
                ThemePassage.IsCheckedLoad();
            }
        }

        Color dark = (Color)ColorConverter.ConvertFromString("#FF212121");
        Color light = Colors.White;

        //const string settingPage = @"/Tick;component/Page/SettingPage.xaml";
        //const string logPage = @"/Tick;component/Page/RecordPage.xaml";
        public const string CompetitionPage = @"/Tick;component/Page/CompetitionPage.xaml";
        public const string OrdinaryPage = @"/Tick;component/Page/OrdinaryPage.xaml";
        //public static string TimerPage { get; set; } = OrdinaryPage;


        public static void OperateMessage(string context)
        {
            opertitionMessageEvent.Invoke(context);
        }
        public static void ShowPage(string path)
        {
            showPageEvent.Invoke(path);
        }

        #region Window Loaded and Close Event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Status.isAutoChandeTheme)
            {
                btnAutoNightMode.IsChecked = Status.isAutoChandeTheme;
                autoNightMode();
            }
            else
            {
                ConfigXmlPassage xml = new ConfigXmlPassage(Configure.Config.ConfigXmlPath);
                if (xml.GetTheme().Equals(Xml.XmlContext.Theme.Dark))
                {
                    Status.isDark = true;
                    ThemePassage.ThemeConvert(Theme.Dark);
                }
                else
                {
                    Status.isDark = false;
                    ThemePassage.ThemeConvert(Theme.Light);
                }
            }
            ThemePassage.IsCheckedLoad();
            new Languages.ControlLoad().Loaded(this);
            Data.AddAppLog("application runing");
        }

        private void btnAutoNightMode_Click(object sender, RoutedEventArgs e)
        {
            Status.isAutoChandeTheme = (bool)btnAutoNightMode.IsChecked;
            if (Status.isAutoChandeTheme)
            {
                autoNightMode();
                ThemePassage.IsCheckedLoad();
            }
        }

        private void autoNightMode()
        {
            if (DateTime.Now.Hour >= 18)
            {
                Status.isDark = true;
                ThemePassage.ThemeConvert(Theme.Dark);
            }
            else
            {
                Status.isDark = false;
                ThemePassage.ThemeConvert(Theme.Light);
                TickControl control = new TickControl();
                control.AutoNightMode += nightMode;
                control.AutoNightModeStart();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ConfigXmlPassage xml = new ConfigXmlPassage(Configure.Config.ConfigXmlPath);
            if (Status.isDark)
            {
                xml.SetTheme(Xml.XmlContext.Theme.Dark);
            }
            else
            {
                xml.SetTheme(Xml.XmlContext.Theme.Light);
            }

            Data.AddAppLog("application close");
            if (Setting.AutoExportLog)
            {
                ShowLogWindow.ExportFile();
            }
            SetFile.ExportFile();
            Process.GetCurrentProcess().Kill();
        }
        #endregion

        #region Window Close and Minimized Event
        private void btnCloseWin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizedWin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Opertition Message Show Event
        private static event message opertitionMessageEvent;
        private delegate void message(string str);
        void opertition(string str) => txtOperateMessage.Text = str;
        #endregion

        #region Page 
        private delegate void timerHandler(string page);
        private static event timerHandler showPageEvent;
        private void timerHandlerFun(string page) =>
            frameTimer.Source = new Uri(page, UriKind.Relative);
        #endregion

        #region ThemeConvert
        public static void ThemeConvert(Theme theme)
        {
            themeEvent.Invoke(theme);
        }
        private static event themeHandler themeEvent;
        private delegate void themeHandler(Theme theme);
        private void themeFuna(Theme theme)
        {
            if (theme == Theme.Dark)
            {
                masterContainer.Background = new SolidColorBrush(dark);
            }
            if (theme == Theme.Light)
            {
                masterContainer.Background = new SolidColorBrush(light);
            }
        }
        #endregion

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            pageShow(PageShow.Timer);
        }
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            pageShow(PageShow.Log);
        }
        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            pageShow(PageShow.Set);
        }
        private void pageShow(PageShow page)
        {
            frameTimer.Visibility = Visibility.Hidden;
            frameLog.Visibility = Visibility.Hidden;
            frameSet.Visibility = Visibility.Hidden;
            switch (page)
            {
                case PageShow.Timer:
                    frameTimer.Visibility = Visibility.Visible; break;
                case PageShow.Log:
                    frameLog.Visibility = Visibility.Visible; break;
                case PageShow.Set:
                    frameSet.Visibility = Visibility.Visible; break;
            }
        }
    }
}
