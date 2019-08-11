using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Languages.In
{
    class Region
    {
        private class RegionPath
        {
            public const string zh_CN = "Chinese.json";
            public const string en_US = "English.json";
        }
        public Region(string region)
        {
            switch (region)
            {
                case China:
                    Configure.Status.Language = RegionPath.zh_CN; break;
                case English:
                    Configure.Status.Language = RegionPath.en_US; break;
            }
        }
        public const string China = "China";
        public const string English = "English";
    }
}
