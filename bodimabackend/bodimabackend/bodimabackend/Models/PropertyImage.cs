namespace bodimabackend.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; }


        // Navigation
        public Property Property { get; set; }
    }
}
