namespace GuiderPro.Api.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
