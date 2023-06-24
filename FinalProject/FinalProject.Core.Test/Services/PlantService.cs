using FinalProject.Persistence.Repositories;
using FinaltProject.Test.Helper;
using NUnit.Framework;

namespace FinalProject.Core.Test.Services
{
    public class PlantService
    {
        private PlantRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new PlantRepository(ContextHelper.GenerateContext());
        }

        [Test]
        public async Task AddPlantTest()
        {

        }
    }
}
