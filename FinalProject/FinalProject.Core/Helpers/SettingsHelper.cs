using FinalProject.Core.Enums;
using FinalProject.Core.ObservableModels;

namespace FinalProject.Core.Helpers
{
    public static class SettingsHelper
    {
        public static void SetSetting(Settings setting, int value)
        {
            Preferences.Default.Set(setting.ToString(), value);
        }

        public static void SetSetting(SettingModel settingModel)
        {
            SetSetting(settingModel.Setting, settingModel.ChosenIndex);
        }

        public static int GetSetting(Settings setting)
        {
            return Preferences.Default.Get(setting.ToString(), 0);
        }

        public static TemperatureUnits GetTemperatureUnitSetting()
        {
            return (TemperatureUnits)GetSetting(Settings.TemperatureUnit);
        }
    }
}
