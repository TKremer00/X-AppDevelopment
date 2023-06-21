using FinalProject.Data.Models;
using FinalProject.Persistence.Repositories;

namespace FinalProject.Core.Services
{
    public class TemperatureService
    {
        private readonly TemperatureRepository _repository;

        public TemperatureService(TemperatureRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Temperature>> GetTemperaturesAsync() => _repository.GetAllAsync();

        public Task<List<Temperature>> GetLastTemperaturesAsync(int last) => _repository.GetLastAsync(last);
    }
}
