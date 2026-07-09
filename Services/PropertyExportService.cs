using EstatePredict.Api.Database;
using EstatePredict.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

public class PropertyExportService : IPropertyExportService
{
    private readonly EstatePredictContext _context;

    public PropertyExportService(EstatePredictContext context)
    {
        _context = context;
    }

    public async Task<byte[]> ExportPropertiesToCsvAsync()
    {
        var properties = await _context.Properties
            .Include(p => p.Location)
            .Include(p => p.PropertyType)
            .ToListAsync();

        var sb = new StringBuilder();

        sb.AppendLine("price,area,rooms,bathrooms,city,latitude,longitude,propertyType,yearBuilt");

        foreach (var p in properties)
        {
            var line = string.Join(",",
              p.CurrentPrice,
              p.Area,
              p.NumberOfRooms,
              p.Floor,
              p.YearBuilt,
              p.HasParking ? 1 : 0,
              p.HasLift ? 1 : 0,
              Escape(p.Location?.City),
              Escape(p.Location?.Municipality),
              Escape(p.PropertyType?.Name),
              MapCondition(p.Condition)
          );

            sb.AppendLine(line);
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private string Escape(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "";

        value = value.Replace("\"", "\"\"");

        return $"\"{value}\"";
    }

    private int MapCondition(string condition)
    {
        return condition.ToLower() switch
        {
            "new" => 3,
            "renovated" => 2,
            "used" => 1,
            _ => 0
        };
    }
}