namespace EstatePredict.DTO
{
    
    public class PropertyDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Area { get; set; }
        public int NumberOfRooms { get; set; }
        public int YearBuilt { get; set; }
        public decimal CurrentPrice { get; set; }

        // Flattened location for convenience
        public int LocationId { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Municipality { get; set; } = string.Empty;

        // Flattened property type
        public int PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}
