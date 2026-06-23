using EstatePredict.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePredict.Models.Entities
{
    public class Prediction
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } 

        [Required]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public Property Property { get; set; } = null!;

        // Godina za koju se vrši predikcija
        [Required]
        public int TargetYear { get; set; }

        // Procijenjena cijena
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PredictedPrice { get; set; }

        // Procijenjena cijena po m²
        [Column(TypeName = "decimal(18,2)")]
        public decimal PredictedPricePerSquareMeter { get; set; }

        // Tačnost modela (ako je dostupna)
        public double? ConfidenceScore { get; set; }

        // Naziv modela
        [MaxLength(100)]
        public string? ModelVersion { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}