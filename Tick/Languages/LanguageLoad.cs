using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Tick.Languages.In;

namespace Tick.Languages
{
    public class LanguageLoad
    {
        private static RootObject language = new RootObject();
        public static RootObject Language => language;
        public LanguageLoad(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            string data;
            using (StreamReader stream = new StreamReader(file))
            {
                data = stream.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(data))
                language = JsonConvert.DeserializeObject<RootObject>(data);
            else
                throw new Exception("lost language.json file");
        }
    }
}
