namespace bodimabackend.Models
{
    public class Property
    {
        public int PropertyId { get; set; }  // Primary Key
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal PricePerMonth { get; set; }
        public bool IsAvailable { get; set; }

        // Foreign Key
        public int OwnerId { get; set; }

        // Navigation Properties
        public User Owner { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
