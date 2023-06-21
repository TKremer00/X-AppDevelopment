using FinalProject.Persistence.Models;
using FinalProject.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Persistence.Extension
{
    public static class DatabaseFacadeExtension
    {
        public static async Task SeedIfEmpty(this DatabaseFacade _, IServiceProvider service)
        {
            var plantRepo = service.GetRequiredService<PlantRepository>();
            var plants = await plantRepo.GetAllAsync();

            if (!plants.Any())
            {
                await plantRepo.AddAsync(new Plant
                {
                    LatinPlantName = "Abies alba",
                    PlantName = "Silver fir",
                    CreatedAt = DateTime.Now,
                    ImageUrl = "https://perenual.com/storage/species_image/1_abies_alba/small/1536px-Abies_alba_SkalitC3A9.jpg",
                    MaxHumidity = 40,
                    MinHumidity = 30,
                    MaxTemperature = 20,
                    MinTemperature = 10
                });

                await plantRepo.AddAsync(new Plant
                {
                    LatinPlantName = "Abies alba 'Pyramidalis'",
                    PlantName = "European silver fir",
                    CreatedAt = DateTime.Now,
                    ImageUrl = "https://perenual.com/storage/species_image/2_abies_alba_pyramidalis/small/49255769768_df55596553_b.jpg",
                    MaxHumidity = 40,
                    MinHumidity = 30,
                    MaxTemperature = 20,
                    MinTemperature = 10
                });
            }

            var temperatureRepo = service.GetRequiredService<TemperatureRepository>();
            var temperature = await temperatureRepo.GetAllAsync();

            if (!temperature.Any())
            {
                var random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    var temp = random.Next(20) + 18;
                    await temperatureRepo.AddAsync(new Temperature { Value = new byte[] { (byte)temp, 0, 0, 0 }, CreatedAt = DateTime.Now.AddMinutes(-i) });
                }
            }

            await plantRepo.SaveAsync();
        }
    }
}
