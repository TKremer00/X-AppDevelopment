using FinalProject.Data.Models;
using FinalProject.Test.Helper.Fakers;

namespace FinaltProject.Test.Helper.Fakers
{
    public class PlantFaker : FakerBase<Plant>
    {
        public override Plant generate()
        {
            return new Plant()
            {
                LatinPlantName = Faker.Name.First(),
                PlantName = Faker.Name.Last(),
                MinTemperature = Faker.RandomNumber.Next(20) + 10,
                MaxTemperature = Faker.RandomNumber.Next(30) + 20,
                MinHumidity = Faker.RandomNumber.Next(50) + 35,
                MaxHumidity = Faker.RandomNumber.Next(65) + 50,
                ImageUrl = Faker.Internet.Url(),
                CreatedAt = Faker.Identification.DateOfBirth(),
            };
        }
    }
}
