namespace EstatePredict.Api.Requests.Property;

public class PropertyInsertRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public decimal Area { get; set; }
    public int NumberOfRooms { get; set; }
    public int YearBuilt { get; set; }

    public bool HasParking { get; set; }
    public bool HasLift { get; set; }

    public string Condition { get; set; } = null!;
    public decimal CurrentPrice { get; set; }

    public int UserId { get; set; }

    public int LocationId { get; set; }
    public int PropertyTypeId { get; set; }
}