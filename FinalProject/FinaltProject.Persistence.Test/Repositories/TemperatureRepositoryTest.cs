using FinalProject.Data.Models;
using FinalProject.Persistence.Repositories;
using FinaltProject.Persistence.Test.Repositories;
using FinaltProject.Test.Helper;
using FinaltProject.Test.Helper.Fakers;
using NUnit.Framework;

namespace FinalProject.Persistence.Test.Repositories
{
    public class TemperatureRepositoryTest : BaseRepositoryTester<Temperature, TemperatureRepository>
    {
        public TemperatureRepositoryTest() : base(new TemperatureFaker())
        {
        }

        [SetUp]
        public override void SetUp()
        {
            _repository = new TemperatureRepository(ContextHelper.GenerateContext());
        }

        [Test]
        public async Task TestGetMostRecentAsync_Always_Return_Most_Recent_Async()
        {
            var count = 3;
            var temperatures = await AddItemsToDatabaseAsync(count * 2);

            var oldestItems = await _repository.GetLastAsync(count);
            var lastItems = temperatures.OrderByDescending(x => x.CreatedAt).Take(count).ToArray();

            Assert.Multiple(() =>
            {
                Assert.That(oldestItems, Is.Not.Null);
                Assert.That(oldestItems.Any(), Is.True);
                Assert.That(oldestItems, Has.Count.EqualTo(count));
                Assert.That(oldestItems, Is.EquivalentTo(lastItems));
            });
        }
    }
}
