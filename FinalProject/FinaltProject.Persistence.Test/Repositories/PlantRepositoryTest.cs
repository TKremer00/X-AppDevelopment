using FinalProject.Persistence.Models;
using FinalProject.Persistence.Repositories;
using NUnit.Framework;

namespace FinaltProject.Persistence.Test.Repositories
{
    public class PlantRepositoryTest
    {
        private readonly PlantRepository _repository;

        public PlantRepositoryTest()
        {
            var context = ContextHelper.GenerateContext();
            _repository = new PlantRepository(context);
        }

        [Test]
        public async Task TestGetMostRecentAsync_Always_Return_Most_Recent_Async()
        {
            var plant = CreateFakePlant();
            await _repository.AddAsync(plant);
            await _repository.SaveAsync();

            var mostRecent = await _repository.GetMostRecentAsync(3);
            Assert.Multiple(() =>
            {
                Assert.That(mostRecent, Is.Not.Null);
                Assert.That(mostRecent.Any(), Is.True);
                Assert.That(mostRecent, Has.Count.EqualTo(1));
                Assert.That(mostRecent.First(), Is.EqualTo(plant));
            });
        }

        private static Plant CreateFakePlant(int id = 0, DateTime? createdAt = null)
        {
            if (!createdAt.HasValue)
            {
                createdAt = DateTime.Now;
            }

            return new Plant
            {
                Id = id,
                LatinPlantName = Guid.NewGuid().ToString(),
                PlantName = Guid.NewGuid().ToString(),
                MinTemperature = 0,
                MaxTemperature = 0,
                MinHumidity = 0,
                MaxHumidity = 0,
                ImageUrl = Guid.NewGuid().ToString(),
                CreatedAt = createdAt.Value,
            };
        }
    }
}
