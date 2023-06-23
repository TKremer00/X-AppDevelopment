using FinalProject.Core.ObservableModels;
using FinalProject.Data.Enums;
using FinalProject.Data.Extensions;
using FinalProject.Data.Interfaces;

namespace FinalProject.Core.Extensions
{
    public static class PreferenceExtensions
    {
        public static int GetSetting(this IPreferencesWrapper preferences, Settings setting)
        {
            return preferences.Get(setting.ToString(), 0);
        }

        public static void SetSetting(this IPreferencesWrapper preferences, SettingModel setting)
        {
            preferences.Set(setting.Setting.ToString(), setting.ChosenIndex);
        }

        public static TemperatureUnits GetTemperatureUnit(this IPreferencesWrapper preferences)
        {
            return (TemperatureUnits)preferences.GetSetting(Settings.TemperatureUnit);
        }

        public static UpdateEnviromentSpeeds GetUpdateEnviromentSpeedsSetting(this IPreferencesWrapper preferences)
        {
            return (UpdateEnviromentSpeeds)preferences.GetSetting(Settings.UpdateEnviromentSpeed);
        }

        public static int GetUpdateEnviromentSpeed(this IPreferencesWrapper preferences)
        {
            return preferences.GetUpdateEnviromentSpeedsSetting().GetSecconds();
        }
    }
}
