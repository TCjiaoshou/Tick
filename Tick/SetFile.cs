using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tick.Configure;

namespace Tick
{
    class SetFile
    {
        private SetFile() { }


        //export related set
        public static void ExportFile()
        {
            string data = $"{Data.HourMeter.GetTime().Hour}:{Data.HourMeter.GetTime().Minute}:{Data.HourMeter.GetTime().Second}," +
                $"{Data.OverTimer.GetTime().Hour}:{Data.OverTimer.GetTime().Minute}:{Data.OverTimer.GetTime().Second}";

            string set = $"{Setting.AutoExportLog},{Status.isAutoChandeTheme}";
            write(Config.ApplicationDataPath, data);
            write(Config.ApplicationSetPath, set);
            write(Config.ApplicationMusicPath, Status.MusicPath);
        }

        //import raleted set
        public static void ImportFile()
        {
            string strMusic = read(Config.ApplicationMusicPath);
            if (!string.IsNullOrEmpty(strMusic))
            {
                Status.MusicPath = strMusic;
            }

            string strSet = read(Config.ApplicationSetPath);
            if (!string.IsNullOrEmpty(strSet))
            {
                string[] boolData = strSet.Split(',');
                try
                {
                    Setting.AutoExportLog = Convert.ToBoolean(boolData[0]);
                    Status.isAutoChandeTheme = Convert.ToBoolean(boolData[1]);
                }
                catch { }
            }

            string data = read(Config.ApplicationDataPath);
            if (!string.IsNullOrEmpty(data))
            {
                string[] datas = data.Split(',');
                try
                {
                    string[] values = datas[0].Split(':');
                    Data.HourMeter = new Time.HourMeter(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2]));
                    values = datas[1].Split(':');
                    Data.OverTimer = new Time.Overtime(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2]));
                }
                catch { }
            }
        }

        #region funcation
        private static void write(string path, string data)
        {
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            using (var stream = new StreamWriter(file))
            {
                stream.Write(data);
                stream.Flush();
            }
        }
        private static string read(string path)
        {
            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            string str = null;
            using (var stream = new StreamReader(file))
            {
                str = stream.ReadToEnd();
            }
            return str;
        }
        #endregion
    }
}
