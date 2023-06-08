using CommunityToolkit.Mvvm.Input;
using FinalProject.Core.Enums;

namespace FinalProject.Core.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {

        private int _chosenTemperatureUnitIndex;
        private string[] _temperatureUnits;
        private int _chosenUpdateEnviromentSpeedIndex;
        private string[] _updateEnviromentSpeeds;

        public SettingsPageViewModel()
        {
            _temperatureUnits = Enum.GetNames<TemperatureUnits>();
            _updateEnviromentSpeeds = Enum.GetNames<UpdateEnviromentSpeeds>().Select(x =>
            {
                var updateSetting = x.Split("_");
                return $"{updateSetting[1]} {updateSetting[0].ToLowerInvariant()}";
            }).ToArray();

            UpdateSettingsCommand = new RelayCommand(HandleUpdateSettingsCommand);
        }

        public int ChosenTemperatureUnitIndex
        {
            get => _chosenTemperatureUnitIndex;
            set => SetProperty(ref _chosenTemperatureUnitIndex, value);
        }

        public string[] TemperatureUnits => _temperatureUnits;

        public int ChosenUpdateEnviromentSpeedIndex
        {
            get => _chosenUpdateEnviromentSpeedIndex;
            set => SetProperty(ref _chosenUpdateEnviromentSpeedIndex, value);
        }

        public string[] UpdateEnviromentSpeeds => _updateEnviromentSpeeds;

        public RelayCommand UpdateSettingsCommand { get; }

        private void HandleUpdateSettingsCommand()
        {
            throw new NotImplementedException();
        }
    }
}
