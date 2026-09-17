namespace EstatePredict.Api.Entities;

public class Property
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public decimal Area { get; set; }

    public int NumberOfRooms { get; set; }

    public int YearBuilt { get; set; }

    public bool HasParking { get; set; }

    public bool HasLift { get; set; }

    public string Condition { get; set; } = null!;

    // Actual/current price of the property.
    // This is the target value for training data.
    public decimal CurrentPrice { get; set; }

    // User
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    // Location
    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;

    // Property type
    public int PropertyTypeId { get; set; }
    public PropertyType PropertyType { get; set; } = null!;

    public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}