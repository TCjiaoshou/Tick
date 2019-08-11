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
using System.IO;
using Microsoft.Win32;
using static System.DateTime;

using Tick.Configure;

namespace Tick
{
    /// <summary>
    /// RecordPage.xaml 的交互逻辑
    /// </summary>
    public partial class RecordPage : Page
    {
        private int UserAPlanIndex = 0;
        private int UserBPlanIndex = 0;

        public RecordPage()
        {
            InitializeComponent();

            competitionLogEvent += LogAdd;
            userALogEvent += UserARecord_Click;
            userBLogEvent += UserBRecord_Click;
            clearEvent += clearFuna;

        }
        private void Page_Load(object sender, RoutedEventArgs e)
        {
            new Languages.ControlLoad().Loaded(this);
        }

        private string getDateTimeToString(string str)
        {
            string[] strs = str.Split(':');
            StringBuilder result = new StringBuilder();
            foreach (string c in strs)
            {
                switch (c)
                {
                    case "Y":
                        result.Append($"{Now.Year}-"); break;
                    case "M":
                        result.Append($"{Now.Month}-"); break;
                    case "D":
                        result.Append($"{Now.Day}"); break;
                    case "h":
                        result.Append($"{Now.Hour}-"); break;
                    case "m":
                        result.Append($"{Now.Minute.ToString("d2")}-"); break;
                    case "s":
                        result.Append($"{Now.Second.ToString("d2")}"); break;
                    default:
                        break;

                }
            }
            return result.ToString();
        }

        #region record user log event
        public static void UserALogAdd()
        {
            userALogEvent.Invoke(null, new RoutedEventArgs());
        }
        private static event userARecordHandler userALogEvent;
        private delegate void userARecordHandler(object s,RoutedEventArgs e);
        private void UserARecord_Click(object sender, RoutedEventArgs e)
        {
            if (UserAPlanIndex == comboUserAPlan.Items.Count)
            {
                UserAPlanIndex = 0;
                return;
            }
            string str = $"{comboUserAPlan.Items[UserAPlanIndex]}";
            txtUserALog.AppendText($"[{getDateTimeToString("h:m:s")}]:{str}\n");
            Data.AddLiftLog(str);
            UserAPlanIndex++;
        }
        public static void UserBLogAdd()
        {
            userBLogEvent.Invoke(null,new RoutedEventArgs());
        }
        private static event userBRecordHandler userBLogEvent;
        private delegate void userBRecordHandler(object s, RoutedEventArgs e);
        private void UserBRecord_Click(object sender, RoutedEventArgs e)
        {
            if (UserBPlanIndex == comboUserBPlan.Items.Count)
            {
                UserBPlanIndex = 0;
                return;
            }
            string str = $"{comboUserBPlan.Items[UserBPlanIndex]}";
            txtUserBLog.AppendText($"[{getDateTimeToString("h:m:s")}]:{str}\n");
            Data.AddRightLog(str);
            UserBPlanIndex++;
        }
        #endregion

        #region open file to ComboBox Item
        private void ExportUserAPlan_Click(object sender, RoutedEventArgs e)
        {
            toComboBoxItem(comboUserAPlan);
        }
        private void ExportUserBPlan_Click(object sender, RoutedEventArgs e)
        {
            toComboBoxItem(comboUserBPlan);
        }

        private void toComboBoxItem(ComboBox comboBox)
        {
            OpenFileDialog dialog = new OpenFileDialog();  //选择文件路径窗口事件函数
            dialog.DefaultExt = ".txt";
            dialog.Filter = "神秘文件|.txt|all file|*.*";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;//获取文件路径
                string[] strs = null;
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                using (StreamReader stream = new StreamReader(file))
                {
                    strs = stream.ReadToEnd().Split(new char[] { ' ', '，', ',', ';', '；' });
                }
                if (strs != null && strs[0] != "")
                {
                    comboBox.Items.Clear();
                    foreach (string str in strs)
                    {
                        comboBox.Items.Add(str);
                    }
                }
            }
        }
        #endregion

        #region record competition log staric method
        public static void CompetitionLogAdd(string context)
        {
            Data.AddCompetitionLog(context);
            competitionLogEvent.Invoke(context);
        }

        private static event competitionHandler competitionLogEvent;
        private delegate void competitionHandler(string context);
        private void LogAdd(string context)
        {
            txtCompetitionLog.AppendText($"[{getDateTimeToString("h:m:s")}]:"+context + "\n");
        }
        #endregion

        #region three Button event, export log 
        private void ExportUserALog_Click(object sender,RoutedEventArgs e)
        {
            string path = Config.LiftRecrdFolderPath + $"{getDateTimeToString("Y:M:D")} {txtUserA}";
            ExportLog(path, Data.LiftLog);
        }
        private void ExportUserBLog_Click(object sender, RoutedEventArgs e)
        {
            string path = Config.RightRecrdFolderPath + $"{getDateTimeToString("Y:M:D")} {txtUserB}";
            ExportLog(path, Data.RightLog);
        }
        private void ExportCompetitionLog_Click(object snder, RoutedEventArgs e)
        {
            string path = Config.CompatitionRecordFolderPath + $"{getDateTimeToString("Y:M:D")}";
            ExportLog(path, Data.CompetitionLog);
        }
        private void ExportLog(string path,List<string> strs)
        {
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            using (var stream=new StreamWriter(file))
            {
                foreach(string str in strs)
                {
                    stream.WriteLine(str);
                }
                stream.Flush();
                MainWindow.OperateMessage($"log path:{path}");
            }
        }
        #endregion

        public static void Clear()
        {
            clearEvent.Invoke();
        }
        private static event clearHandler clearEvent;
        private delegate void clearHandler();
        private void clearFuna()
        {
            txtUserALog.Text = "";
            txtUserBLog.Text = "";
            txtCompetitionLog.Text = "";
            Data.ClearCompetitionLog();
        }
    }
}
