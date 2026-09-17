namespace EstatePredict.Api.Entities;

public class Prediction
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int PropertyId { get; set; }
    public Property Property { get; set; } = null!;

    public decimal PredictedPrice { get; set; }

    public decimal PredictedPricePerSquareMeter { get; set; }

    public decimal? ConfidenceScore { get; set; }

    public string ModelVersion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}