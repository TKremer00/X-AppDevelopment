namespace Friendsbook.Persistence.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public string? Image { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string HouseNumber { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName => $"{Firstname} {Lastname}";
    }
}
