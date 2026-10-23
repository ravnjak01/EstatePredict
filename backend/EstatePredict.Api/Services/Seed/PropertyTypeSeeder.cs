using EstatePredict.Api.Data;
using EstatePredict.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Data.Seeders;

public static class PropertyTypeSeeder
{
    public static async Task SeedAsync(EstatePredictContext context)
    {
        string[] propertyTypes =
        {
            "Apartment",
            "House",
            "Studio",
            "Penthouse"
        };

        foreach (string name in propertyTypes)
        {
            bool exists = await context.PropertyTypes
                .AnyAsync(p => p.Name == name);

            if (!exists)
            {
                context.PropertyTypes.Add(new PropertyType
                {
                    Name = name
                });
            }
        }

        await context.SaveChangesAsync();
    }
}