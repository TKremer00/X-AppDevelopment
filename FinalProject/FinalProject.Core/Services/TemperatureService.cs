using FinalProject.Communication.Communication;
using FinalProject.Data.Enums;
using FinalProject.Data.Models;
using FinalProject.Persistence.Repositories;

namespace FinalProject.Core.Services
{
    public class TemperatureService
    {
        private readonly TemperatureRepository _repository;

        public TemperatureService(TemperatureRepository repository, IBluetoothNotifier bluetoothNotifier)
        {
            _repository = repository;
            bluetoothNotifier.SensorDataChanged += SensorDataChanged;
        }

        private async void SensorDataChanged(object sender, SensorData e)
        {
            if (e.Characteristic != Characteristics.Temperature)
            {
                return;
            }

            await _repository.AddAsync(new Temperature() { Value = e.RawData });
#if !DEBUG
            await _repository.SaveAsync();
#endif
        }

        public Task<List<Temperature>> GetTemperaturesAsync() => _repository.GetAllAsync();

        public Task<List<Temperature>> GetLastTemperaturesAsync(int last) => _repository.GetLastAsync(last);
    }
}
