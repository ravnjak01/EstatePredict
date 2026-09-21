namespace EstatePredict.Api.Requests.Prediction;

public class PredictionRequest
{
    public decimal Area { get; set; }

    public int NumberOfRooms { get; set; }

    public int YearBuilt { get; set; }

    public bool HasParking { get; set; }

    public bool HasLift { get; set; }

    public string Condition { get; set; } = null!;

    public int LocationId { get; set; }

    public int PropertyTypeId { get; set; }
}