namespace EstatePredict.DTOs;

public record PredictionDTO
{
    public int Id { get; set; }

    public int UserId { get; set; } 
    public string UserFullName { get; set; } = string.Empty;

    public int PropertyId { get; set; }
    public string PropertyTitle { get; set; } = string.Empty;
    public decimal PropertyArea { get; set; }

    public int TargetYear { get; set; }

    public decimal PredictedPrice { get; set; }
    public decimal PredictedPricePerSquareMeter { get; set; }
    public double? ConfidenceScore { get; set; }
    public string? ModelVersion { get; set; }

    public DateTime CreatedAt { get; set; }
}