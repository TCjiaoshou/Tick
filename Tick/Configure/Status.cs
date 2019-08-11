using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Configure
{
    class Status
    {
        public static bool isShowTick { get; set; }
        public static bool isStart { get; set; }
        public static bool isEnd { get; set; } = true;
        public static bool isNowOvertime { get; set; }
        public static bool isDark { get; set; }
        public static bool isAutoChandeTheme { get; set; }
        public static string MusicPath { get; set; } = Config.DefaultMusicPath;
        public static string Language { get; set; }

    }
}
