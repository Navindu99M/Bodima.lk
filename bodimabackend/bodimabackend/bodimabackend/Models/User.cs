namespace bodimabackend.Models
{
    public class User
    {
        public int UserId { get; set; }  // Primary Key
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Boarder" or "Landlord"
        public string PhoneNumber { get; set; }

        // Navigation Properties
        public ICollection<Property> Properties { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
