namespace bodimabackend.Models
{
    public class Booking
    {
        public int BookingId { get; set; }  // Primary Key
        public int PropertyId { get; set; } // FK
        public int UserId { get; set; }     // FK
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        // Navigation Properties
        public Property Property { get; set; }
        public User User { get; set; }
    }
}
