using FinalProject.Core.Services;
using FinalProject.Core.ValidationModels;
using FinalProject.Data.Enums;
using FinalProject.Data.Interfaces;
using FinalProject.Data.Models;
using FinalProject.Persistence.Repositories;
using FinaltProject.Test.Helper;
using FinaltProject.Test.Helper.Fakers;
using Moq;
using NUnit.Framework;

namespace FinalProject.Core.Test.Services
{
    public class PlantServiceTest
    {
        private PlantService _service;
        private PlantRepository _repository;
        private IFaker<Plant> _faker;

        public PlantServiceTest()
        {
            _faker = new PlantFaker();
        }

        [SetUp]
        public void SetUp()
        {
            _repository = new PlantRepository(ContextHelper.GenerateContext());
            _service = new PlantService(_repository);
        }

        [Test]
        public async Task AddPlantTest()
        {
            var preferences = new Mock<IPreferencesWrapper>();
            preferences.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<int>())).Returns(1);
            var entity = _faker.generate();

            var validatedPlant = new PlantValidation(preferences.Object)
            {
                LatinPlantName = entity.LatinPlantName,
                PlantName = entity.PlantName,
                ImageUrl = entity.ImageUrl,
            };

            validatedPlant.TemperatureRanges.ChosenIndex = (int)TemperatureRanges._10_till_20;
            validatedPlant.HumidityRanges.ChosenIndex = (int)HumidityRanges._80_till_90;

            await _service.SavePlant(validatedPlant);

            var items = await _service.GetPlantsAsync();
            Assert.Multiple(() =>
            {
                Assert.That(items, Is.Not.Null);
                Assert.That(items.Any(), Is.True);
                Assert.That(items, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public async Task TestGetMostRecentAsync_Always_Return_Most_Recent_Async()
        {
            var count = 2;
            var plants = await AddItemsToDatabaseAsync(count * 2);

            var mostRecent = await _service.GetMostRecentAsync();
            var expectedItems = plants.OrderBy(x => x.CreatedAt).Take(count).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(mostRecent, Is.Not.Null);
                Assert.That(mostRecent.Any(), Is.True);
                Assert.That(mostRecent, Has.Count.EqualTo(count));
                Assert.That(mostRecent, Is.EquivalentTo(expectedItems));
            });
        }

        protected async Task<Plant[]> AddItemsToDatabaseAsync(int amount)
        {
            var items = _faker.generate(10);

            foreach (var item in items)
            {
                await _repository.AddAsync(item);
            }

            await _repository.SaveAsync();
            return items;
        }
    }
}
