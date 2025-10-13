namespace bodimabackend.Models.DTOs
{
    public class CreatePropertyRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
    }
}
