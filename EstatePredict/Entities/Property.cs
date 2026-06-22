using EstatePredict.Models.Entities;
using System;
using System.Collections.Generic; // Potrebno za ICollection

namespace EstatePredict.Entities
{
    public class Property
    {
        public int Id { get; set; }

        public string UserId { get; set; } 
        public ApplicationUser User { get; set; } = null!;

        public int LocationId { get; set; }
        public Location Location { get; set; } = null!;
        public decimal Area { get; set; }

        public int NumberOfRooms { get; set; }
        public int Floor { get; set; }
        public int YearBuilt { get; set; }
        public bool HasParking { get; set; }
        public bool HasLift { get; set; }

        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; } = null!;

        public string Condition { get; set; } = string.Empty;

        public decimal CurrentPrice { get; set; }

        public DateTime CreatedAt { get; set; }

  
        public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    }
}