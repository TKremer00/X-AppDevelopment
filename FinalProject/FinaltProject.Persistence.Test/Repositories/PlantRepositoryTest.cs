using FinalProject.Persistence.Models;
using FinalProject.Persistence.Repositories;
using FinaltProject.Persistence.Test.Helpers;
using NUnit.Framework;

namespace FinaltProject.Persistence.Test.Repositories
{
    public class PlantRepositoryTest : BaseRepositoryTester<Plant, PlantRepository>
    {
        public PlantRepositoryTest() : base(new PlantFaker(), new PlantRepository(ContextHelper.GenerateContext()))
        {
        }

        [Test]
        public async Task TestGetMostRecentAsync_Always_Return_Most_Recent_Async()
        {
            var plant = _faker.generate();
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

    }
}
