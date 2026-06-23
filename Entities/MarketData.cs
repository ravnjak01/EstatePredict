using EstatePredict.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePredict.Models.Entities
{
    public class MarketData
    {
        public int Id { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public Location Location { get; set; } = null!;

        [Required]
        public int PropertyTypeId { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; } = null!;

        // Godina na koju se podaci odnose
        [Required]
        public int Year { get; set; }

        // Prosječna cijena po m²
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AveragePricePerSquareMeter { get; set; }

        // Prosječna ukupna cijena
        [Column(TypeName = "decimal(18,2)")]
        public decimal AveragePrice { get; set; }

        // Godišnji rast cijena (%)
        public double PriceGrowthRate { get; set; }

        // Inflacija (%)
        public double InflationRate { get; set; }

        // Indeks potražnje (0-100)
        public double DemandIndex { get; set; }

        // Broj evidentiranih prodaja
        public int NumberOfSales { get; set; }
    }
}