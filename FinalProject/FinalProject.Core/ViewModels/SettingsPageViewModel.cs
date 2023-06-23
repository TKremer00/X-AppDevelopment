using CommunityToolkit.Mvvm.Input;
using FinalProject.Core.Extensions;
using FinalProject.Core.Helpers;
using FinalProject.Core.ObservableModels;
using FinalProject.Data.Enums;
using FinalProject.Data.Interfaces;

namespace FinalProject.Core.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private readonly SettingModel _temperatureSetting;
        private readonly SettingModel _updateEnviromentSpeedSetting;

        public SettingsPageViewModel()
        {
            _temperatureSetting = new(Settings.TemperatureUnit, Enum.GetNames<TemperatureUnits>());
            _updateEnviromentSpeedSetting =
                new(Settings.UpdateEnviromentSpeed, Enum.GetNames<UpdateEnviromentSpeeds>().Select(SanitizeUpdateEnviromentSpeedNames).ToArray());
            UpdateSettingsCommand = new AsyncRelayCommand(HandleUpdateSettingsCommand);
        }

        public SettingModel TemperatureSetting => _temperatureSetting;

        public SettingModel UpdateEnviromentSpeedSetting => _updateEnviromentSpeedSetting;

        public AsyncRelayCommand UpdateSettingsCommand { get; }

        private string SanitizeUpdateEnviromentSpeedNames(string enviromentSpeed)
        {
            var updateSetting = enviromentSpeed.Split("_");
            return $"{updateSetting[1]} {updateSetting[0].ToLowerInvariant()}";
        }

        private async Task HandleUpdateSettingsCommand()
        {
            var preferences = Application.Current.GetRequiredService<IPreferencesWrapper>();
            preferences.SetSetting(TemperatureSetting);
            preferences.SetSetting(UpdateEnviromentSpeedSetting);

            await ToasterHelper.Show("Updated Settings");

            await RoutingHelper.NavigateBackAsync();
        }
    }
}
