using FinalProject.Communication.Communication;
using FinalProject.Core.Helpers;
using FinalProject.Data.Enums;
using FinalProject.Data.Models;
using FinalProject.Persistence.Repositories;

namespace FinalProject.Core.Services
{
    public class TemperatureService
    {
        private readonly TemperatureRepository _repository;
        private readonly UpdateHelper _temperatureUpdateHelper;

        public TemperatureService(TemperatureRepository repository, IBluetoothNotifier bluetoothNotifier)
        {
            _repository = repository;
            _temperatureUpdateHelper = new();
            bluetoothNotifier.SensorDataChanged += SensorDataChanged;
        }

        private async void SensorDataChanged(object sender, SensorData e)
        {
            if (e.Characteristic != Characteristics.Temperature || !_temperatureUpdateHelper.CanUpdate())
            {
                return;
            }

            await _repository.AddAsync(new Temperature() { Value = e.RawData });
            await _repository.SaveAsync();
        }

        public Task<List<Temperature>> GetTemperaturesAsync() => _repository.GetAllAsync();

        public Task<List<Temperature>> GetLastTemperaturesAsync(int last) => _repository.GetLastAsync(last);
    }
}
