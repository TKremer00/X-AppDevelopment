using FinalProject.Persistence.Models;

namespace FinaltProject.Persistence.Test.Helpers
{
    internal class PlantFaker : IFaker<Plant>
    {
        public Plant generate()
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
