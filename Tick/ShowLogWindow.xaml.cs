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
using System.Windows.Shapes;
using System.IO;
using static System.DateTime;

using Tick.Configure;

namespace Tick
{
    /// <summary>
    /// ShowLogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowLogWindow : Window
    {
        public ShowLogWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExportFile();
        }

        public static void ExportFile()
        {
            string path = Config.LogFolderPath + $"{Now.Year}-{Now.Month}-{Now.Day}.txt";
            var file = new FileStream(path, FileMode.Create, FileAccess.Write);
            using (var stream = new StreamWriter(file))
            {
                foreach (string str in Data.AppLog)
                {
                    stream.WriteLine(str);
                }
                stream.Flush();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkAtuo.IsChecked = Setting.AutoExportLog;
            if (Data.AppLog.Count != 0)
            {
                foreach (string str in Data.AppLog)
                {
                    txtContext.AppendText(str);
                }
            }
            else
            {
                txtContext.TextAlignment = TextAlignment.Center;
                txtContext.Text = "NULL";
            }
        }

        private void CheckAtuo_Checked(object sender, RoutedEventArgs e)
        {
            Setting.AutoExportLog = (bool)checkAtuo.IsChecked;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void gridMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    DragMove();
                }
                catch { }
            }
        }
    }
}
