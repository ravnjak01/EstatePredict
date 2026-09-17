namespace EstatePredict.Api.DTO.Location;

public class LocationResponse
{
    public int Id { get; set; }

    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Municipality { get; set; } = null!;
}