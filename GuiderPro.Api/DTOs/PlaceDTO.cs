namespace GuiderPro.Api.DTOs
{
    public class PlaceDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<int> TagIds { get; set; } = [];
    }
}
