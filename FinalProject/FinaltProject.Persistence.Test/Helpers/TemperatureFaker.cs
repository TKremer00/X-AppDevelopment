using FinalProject.Data.Models;
using FinalProject.Persistence.Test.Helpers;

namespace FinaltProject.Persistence.Test.Helpers
{
    internal class TemperatureFaker : FakerBase<Temperature>
    {
        public override Temperature generate()
        {
            return new Temperature()
            {
                Value = new byte[] { (byte)(Faker.RandomNumber.Next(20) + 10), 0, 0, 0 },
                CreatedAt = Faker.Identification.DateOfBirth(),
            };
        }
    }
}
