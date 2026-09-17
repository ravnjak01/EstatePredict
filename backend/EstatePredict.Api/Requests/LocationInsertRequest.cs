namespace EstatePredict.Api.Requests.Location;

public class LocationInsertRequest
{
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Municipality { get; set; } = null!;
}