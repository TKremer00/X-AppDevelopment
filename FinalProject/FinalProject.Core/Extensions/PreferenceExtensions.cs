using FinalProject.Core.ObservableModels;
using FinalProject.Data.Enums;

namespace FinalProject.Core.Extensions
{
    public static class PreferenceExtensions
    {
        public static int GetSetting(this IPreferences preferences, Settings setting)
        {
            return preferences.Get(setting.ToString(), 0);
        }

        public static void SetSetting(this IPreferences preferences, SettingModel setting)
        {
            preferences.Set(setting.Setting.ToString(), setting.ChosenIndex);
        }

        public static TemperatureUnits GetTemperatureUnit(this IPreferences preferences)
        {
            return (TemperatureUnits)preferences.GetSetting(Settings.TemperatureUnit);
        }

        public static UpdateEnviromentSpeeds GetUpdateEnviromentSpeedsSetting(this IPreferences preferences)
        {
            return (UpdateEnviromentSpeeds)preferences.GetSetting(Settings.UpdateEnviromentSpeed);
        }

        public static int GetUpdateEnviromentSpeed(this IPreferences preferences)
        {
            return preferences.GetUpdateEnviromentSpeedsSetting().GetSecconds();
        }

    }
}
