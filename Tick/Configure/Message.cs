using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Configure
{
    public class Message
    {
        public static string TimeInvalid = "时间以无效";
        public static string ParameterError = "参数错误";
        public static string SetHourMeter = "设置到计时";
        public static string SetOvertime = "设置加时";
        public static string SetTimer = "设置定时";
        public static string ExportFile { get; set; }
        public static string Complet { get; set; }
        public static string Start { get; set; }
        public static string End { get; set; }
        public static string Stop { get; set; }
        public static string Save { get; set; }
        public static string Competition { get; set; }
        public static string Ordinary { get; set; }
        public static string DelayStart { get; set; }
    }
}
