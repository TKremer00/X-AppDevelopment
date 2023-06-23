using FinalProject.Data.Models;
using FinalProject.Test.Helper.Fakers;

namespace FinaltProject.Test.Helper.Fakers
{
    public class TemperatureFaker : FakerBase<Temperature>
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
