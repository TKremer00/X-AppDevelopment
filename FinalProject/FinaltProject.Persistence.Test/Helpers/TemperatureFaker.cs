using FinalProject.Data.Models;

namespace FinaltProject.Persistence.Test.Helpers
{
    internal class TemperatureFaker : IFaker<Temperature>
    {
        public Temperature generate()
        {
            return new Temperature()
            {
                Value = new byte[] { (byte)(Faker.RandomNumber.Next(20) + 10), 0, 0, 0 }
            };
        }
    }
}
