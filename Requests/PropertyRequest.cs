using System.ComponentModel.DataAnnotations;

namespace EstatePredict.Requests;

public class CreatePropertyRequest
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    [Required]
    [Range(1, 100_000, ErrorMessage = "Area must be between 1 and 100,000 m².")]
    public decimal Area { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Number of rooms must be between 1 and 100.")]
    public int NumberOfRooms { get; set; }

    [Required]
    [Range(1800, 2100, ErrorMessage = "Year built must be between 1800 and 2100.")]
    public int YearBuilt { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Current price must be greater than 0.")]
    public decimal CurrentPrice { get; set; }

    [Required]
    public int LocationId { get; set; }

    [Required]
    public int PropertyTypeId { get; set; }

    [Required]
    public int UserId { get; set; }
}

public class UpdatePropertyRequest
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    [Required]
    [Range(1, 100_000, ErrorMessage = "Area must be between 1 and 100,000 m².")]
    public decimal Area { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Number of rooms must be between 1 and 100.")]
    public int NumberOfRooms { get; set; }

    [Required]
    [Range(1800, 2100, ErrorMessage = "Year built must be between 1800 and 2100.")]
    public int YearBuilt { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Current price must be greater than 0.")]
    public decimal CurrentPrice { get; set; }

    [Required]
    public int LocationId { get; set; }

    [Required]
    public int PropertyTypeId { get; set; }
}