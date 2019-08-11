using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.DateTime;

using Tick.Time;

namespace Tick.Configure
{
    static class Data
    {
        #region Log
        static Data()
        {
            _appLog = new List<string>();
            _liftLog = new List<string>();
            _rightLog = new List<string>();
            _competitionLog = new List<string>();
        }
        private static List<string> _appLog;
        private static List<string> _liftLog;
        private static List<string> _rightLog;
        private static List<string> _competitionLog;

        public static List<string> AppLog => _appLog;
        public static void AddAppLog(string context) => _appLog.MyAdd(context);

        public static List<string> LiftLog => _liftLog;
        public static void AddLiftLog(string context) => _liftLog.MyAdd(context);

        public static List<string> RightLog => _rightLog;
        public static void AddRightLog(string context) => _rightLog.MyAdd(context);

        public static List<string> CompetitionLog => _competitionLog;
        public static void AddCompetitionLog(string context) => _competitionLog.MyAdd(context);

        public static void ClearCompetitionLog()
        {
            LiftLog.Clear();
            RightLog.Clear();
            CompetitionLog.Clear();
        }
        private static void MyAdd(this List<string> list, string str)
        {
            list.Add($"[{Now.Hour}-{Now.Minute.ToString("d2")}-{Now.Second.ToString("d2")}]->{str}\n");
        }
        #endregion



        #region Time
        public static HourMeter HourMeter { get; set; } = new HourMeter(3, 0, 0);         //计时器   
        public static Timer Timer { get; set; }     //定时器
        public static Delay Delayer => new Delay(0, 0, 3);    //延时器
        public static Overtime OverTimer { get; set; } = new Overtime(0, 0, 0);       //加时器

        #endregion
    }
}
