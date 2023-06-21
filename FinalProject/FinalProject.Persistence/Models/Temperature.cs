namespace FinalProject.Persistence.Models
{
    public class Temperature
    {
        public int Id { get; set; }

        public byte[] Value { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LocalTime => CreatedAt.ToLocalTime();

        public int IntValue => Value.First();
    }
}
