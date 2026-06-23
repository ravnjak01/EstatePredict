using System.ComponentModel.DataAnnotations;

namespace EstatePredict.Requests;

public class CreatePredictionRequest
{
    [Required]
    public int UserId { get; set; } 

    [Required]
    public int PropertyId { get; set; }

    [Required]
    [Range(2025, 2100, ErrorMessage = "Target year must be between 2025 and 2100.")]
    public int TargetYear { get; set; }
}