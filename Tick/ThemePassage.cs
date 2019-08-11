using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick
{
     class ThemePassage
    {

        public static void ThemeConvert(Theme theme)
        {
            MainWindow.ThemeConvert(theme);
            OrdinaryPage.TickContorlTheme(theme);
        }
        public static void IsCheckedLoad()
        {
            SettingPage.CheckThemeIsChecked();
        }
    }
}
