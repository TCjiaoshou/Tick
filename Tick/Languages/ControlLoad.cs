using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tick.Languages.In;

namespace Tick.Languages
{
    class ControlLoad
    {
        static ControlLoad()
        {
            new Region(new ConfigXmlPassage(Configure.Config.ConfigXmlPath).GetLanguage());
            new LanguageLoad(Configure.Config.LanguageFile + Configure.Status.Language);
        }
        public ControlLoad() { }
        public void Loaded(MainWindow mainWindow)
        {
            mainWindow.pageTimer.Content = LanguageLoad.Language.Timer;
            mainWindow.pageLog.Content = LanguageLoad.Language.Log;
            mainWindow.pageSet.Content = LanguageLoad.Language.Set;
        }
        public void Loaded(RecordPage page)
        {
            page.txtCompetition.Text = LanguageLoad.Language.Competition;
            page.btnCenterExport.Content = LanguageLoad.Language.Export;
            page.btnLeftExport.Content = LanguageLoad.Language.Export;
            page.btnRightExport.Content = LanguageLoad.Language.Export;
            page.btnLeftImport.Content = LanguageLoad.Language.Plan;
            page.btnRightImport.Content = LanguageLoad.Language.Plan;
            foreach(string str in LanguageLoad.Language.DefaultPlan)
            {
                page.comboUserAPlan.Items.Add(str);
                page.comboUserBPlan.Items.Add(str);
            }
        }
        public void Loaded(SettingPage page)
        {
            page.btnAppLog.Content = LanguageLoad.Language.AppLog;
            page.btnHourMeter.Content = LanguageLoad.Language.HourMeter;
            page.btnOverTime.Content = LanguageLoad.Language.Overtime;
            page.btnSave.Content = LanguageLoad.Language.Save;
            page.btnSelect.Content = LanguageLoad.Language.Select;
            page.btnTimer.Content = LanguageLoad.Language.Timer;
            page.checkDelay.Content = LanguageLoad.Language.Delay;
            page.checkOvertime.Content = LanguageLoad.Language.Overtime;
            page.checkRecord.Content = LanguageLoad.Language.Record;
            page.checkTimer.Content = LanguageLoad.Language.Timer;
            page.btnLanguage.Content = LanguageLoad.Language.Save;
        }
    }
}
