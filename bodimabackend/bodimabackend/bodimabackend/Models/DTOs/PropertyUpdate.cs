namespace bodimabackend.Models.DTOs
{
    public class PropertyUpdate
    {
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal PricePerMonth { get; set; }
        public bool IsAvailable { get; set; }
        //public List<string> Images { get; set; }   // only image URLs
    }
}
