namespace EstatePredict.Api.Entities;

public class Location
{
    public int Id { get; set; }

    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Municipality { get; set; } = null!;

    public ICollection<Property> Properties { get; set; } = new List<Property>();
}