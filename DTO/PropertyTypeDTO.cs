namespace EstatePredict.DTOs;

public record PropertyTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PropertyCount { get; set; }
}