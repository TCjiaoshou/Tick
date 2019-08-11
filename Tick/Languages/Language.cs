using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick.Languages.In
{
    public class RootObject
    {
        public string Master { get; set; }
        public string Log { get; set; }
        public string Set { get; set; }
        public string Export { get; set; }
        public string Record { get; set; }
        public string Competition { get; set; }
        public string Ordinary { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Stop { get; set; }
        public string Plan { get; set; }
        public string Save { get; set; }
        public string Timer { get; set; }
        public string Delay { get; set; }
        public string HourMeter { get; set; }
        public string Overtime { get; set; }
        public string Select { get; set; }
        public string AppLog { get; set; }
        public List<string> DefaultPlan { get; set; }
        public Configure.Message Message { get; set; }
    }
}
