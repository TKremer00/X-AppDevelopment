using CommunityToolkit.Mvvm.ComponentModel;
using FinalProject.Data.Enums;
using FinalProject.Data.Models;

namespace FinalProject.Core.ObservableModels
{
    public class ObservablePlant : ObservableObject
    {
        private readonly object _withinBoundsLock;
        private bool _isTemperatureWithinBounds;
        private bool _isHumidityWithinBounds;

        public ObservablePlant(Plant plant)
        {
            Plant = plant;
            _withinBoundsLock = new object();
        }

        public Plant Plant { get; }

        public PlantAmbientEnviroment AmbientEnviromentStatus => (_isTemperatureWithinBounds, _isHumidityWithinBounds) switch
        {
            (true, true) => PlantAmbientEnviroment.Good,
            (false, false) => PlantAmbientEnviroment.Bad,
            _ => PlantAmbientEnviroment.Partial,
        };

        public void UpdateTemperature(int temperature)
        {
            lock (_withinBoundsLock)
            {
                _isTemperatureWithinBounds = temperature < Plant.MinTemperature || temperature < Plant.MaxTemperature;
                OnPropertyChanged(nameof(AmbientEnviromentStatus));
            }
        }

        public void UpdateHumidity(int humidity)
        {
            lock (_withinBoundsLock)
            {
                _isHumidityWithinBounds = humidity < Plant.MinHumidity || humidity < Plant.MaxHumidity;
                OnPropertyChanged(nameof(AmbientEnviromentStatus));
            }
        }
    }
}
