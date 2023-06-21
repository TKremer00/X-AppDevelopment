namespace FinalProject.Data.Models
{
    public class Plant
    {
        public int Id { get; set; }

        public string LatinPlantName { get; set; } = null!;

        public string PlantName { get; set; } = null!;

        public int MinTemperature { get; set; }

        public int MaxTemperature { get; set; }

        public int MinHumidity { get; set; }

        public int MaxHumidity { get; set; }

        public string ImageUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LocalTime => CreatedAt.ToLocalTime();
    }
}
