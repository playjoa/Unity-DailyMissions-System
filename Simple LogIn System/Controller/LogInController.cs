using SimpleLogIn.Data;
using UnityEngine;
using Utils.Tools;

namespace SimpleLogIn.Controller
{
    public static class LogInController
    {
        public static int StartUpCount { get; private set; }
        public static int DailyLogInCount { get; private set; }
        public static bool IsNewDailyLogIn { get; private set; }

        private static string CurrentDate => System.DateTime.Now.ToString("M/d/yyyy");
        private static string LastPlayerLogInDate => PlayerPrefs.GetString(LogInStatics.LAST_DAY_LOGIN_PREFS);
        
        public static void Initiate()
        {
            if (NewDayLogIn())
                RegisterNewDailyLogIn();

            PlayerPrefsExtension.IncInt(LogInStatics.STARTUP_COUNT_PREFS);
            
            StartUpCount = PlayerPrefs.GetInt(LogInStatics.STARTUP_COUNT_PREFS);
            DailyLogInCount = PlayerPrefs.GetInt(LogInStatics.DAILY_LOGIN_COUNT_PREFS);
        }

        private static bool NewDayLogIn()
        {
            if (LastPlayerLogInDate.Equals(CurrentDate)) return false;
            PlayerPrefs.SetString(LogInStatics.LAST_DAY_LOGIN_PREFS, CurrentDate);
            return true;
        }

        private static void RegisterNewDailyLogIn()
        {
            PlayerPrefsExtension.IncInt(LogInStatics.DAILY_LOGIN_COUNT_PREFS);
            IsNewDailyLogIn = true;
        }
    }
}