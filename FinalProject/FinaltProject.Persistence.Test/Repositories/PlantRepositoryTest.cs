using FinalProject.Data.Models;
using FinalProject.Persistence.Repositories;
using FinaltProject.Persistence.Test.Helpers;
using NUnit.Framework;

namespace FinaltProject.Persistence.Test.Repositories
{
    public class PlantRepositoryTest : BaseRepositoryTester<Plant, PlantRepository>
    {
        public PlantRepositoryTest() : base(new PlantFaker())
        {
        }

        [SetUp]
        public override void SetUp()
        {
            _repository = new PlantRepository(ContextHelper.GenerateContext());
        }

        [Test]
        public async Task TestGetMostRecentAsync_Always_Return_Most_Recent_Async()
        {
            var count = 3;
            var plants = await AddItemsToDatabaseAsync(count * 2);


            var mostRecent = await _repository.GetMostRecentAsync(3);
            var expectedItems = plants.OrderByDescending(x => x.CreatedAt).Take(count).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(mostRecent, Is.Not.Null);
                Assert.That(mostRecent.Any(), Is.True);
                Assert.That(mostRecent, Has.Count.EqualTo(count));
                Assert.That(mostRecent, Is.EquivalentTo(expectedItems));
            });
        }

    }
}
