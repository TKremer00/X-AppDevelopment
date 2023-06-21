using FinalProject.Data.Models;
using FinalProject.Persistence.Repositories;
using FinaltProject.Persistence.Test;
using FinaltProject.Persistence.Test.Helpers;
using FinaltProject.Persistence.Test.Repositories;
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
    }
}
