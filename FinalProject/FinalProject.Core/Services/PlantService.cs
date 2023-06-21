using FinalProject.Core.ValidationModels;
using FinalProject.Data.Models;
using FinalProject.Persistence.Repositories;

namespace FinalProject.Core.Services
{
    public class PlantService
    {
        private const int MOST_RECENT_ITEMS_COUNT = 2;
        private readonly PlantRepository _repository;

        public PlantService(PlantRepository repository)
        {
            _repository = repository;
        }

        public async Task SavePlant(PlantValidation plant)
        {
            if (plant.HasErrors)
            {
                throw new ArgumentException("The plant should not have any errors when saving to the database");
            }

            await _repository.AddAsync(plant.ConvertToModel());
            await _repository.SaveAsync();
        }

        public Task<List<Plant>> GetPlantsAsync() => _repository.GetAllAsync();

        public Task<List<Plant>> GetMostRecentAsync() => _repository.GetMostRecentAsync(MOST_RECENT_ITEMS_COUNT);
    }
}
