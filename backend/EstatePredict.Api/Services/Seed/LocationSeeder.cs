using EstatePredict.Api.Data;
using EstatePredict.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Data.Seeders;

public static class LocationSeeder
{
    public static async Task SeedAsync(EstatePredictContext context)
    {
        var locations = new List<Location>
        {
            // Serbia - Belgrade
            new() { Country = "Serbia", City = "Belgrade", Municipality = "Stari Grad" },
            new() { Country = "Serbia", City = "Belgrade", Municipality = "Zvezdara" },
            new() { Country = "Serbia", City = "Belgrade", Municipality = "Novi Beograd" },
            new() { Country = "Serbia", City = "Belgrade", Municipality = "Palilula" },

            // Serbia - Novi Sad
            new() { Country = "Serbia", City = "Novi Sad", Municipality = "Stari Grad" },
            new() { Country = "Serbia", City = "Novi Sad", Municipality = "Liman" },
            new() { Country = "Serbia", City = "Novi Sad", Municipality = "Grbavica" },
            new() { Country = "Serbia", City = "Novi Sad", Municipality = "Detelinara" },

            // Bosnia and Herzegovina - Sarajevo
            new() { Country = "Bosnia and Herzegovina", City = "Sarajevo", Municipality = "Centar" },
            new() { Country = "Bosnia and Herzegovina", City = "Sarajevo", Municipality = "Ilidža" },
            new() { Country = "Bosnia and Herzegovina", City = "Sarajevo", Municipality = "Stari Grad" },
            new() { Country = "Bosnia and Herzegovina", City = "Sarajevo", Municipality = "Novi Grad" },

            // Bosnia and Herzegovina - Banja Luka
            new() { Country = "Bosnia and Herzegovina", City = "Banja Luka", Municipality = "Obilićevo" },
            new() { Country = "Bosnia and Herzegovina", City = "Banja Luka", Municipality = "Starčevica" },
            new() { Country = "Bosnia and Herzegovina", City = "Banja Luka", Municipality = "Borik" },

            // Bosnia and Herzegovina - Tuzla
            new() { Country = "Bosnia and Herzegovina", City = "Tuzla", Municipality = "Sjenjak" },
            new() { Country = "Bosnia and Herzegovina", City = "Tuzla", Municipality = "Stupine" },
            new() { Country = "Bosnia and Herzegovina", City = "Tuzla", Municipality = "Centar" },

            // Bosnia and Herzegovina - Zenica
            new() { Country = "Bosnia and Herzegovina", City = "Zenica", Municipality = "Crkvice" },
            new() { Country = "Bosnia and Herzegovina", City = "Zenica", Municipality = "Centar" },

            // Bosnia and Herzegovina - Mostar
            new() { Country = "Bosnia and Herzegovina", City = "Mostar", Municipality = "Zalik" },
            new() { Country = "Bosnia and Herzegovina", City = "Mostar", Municipality = "Centar" },

            // Croatia - Zagreb
            new() { Country = "Croatia", City = "Zagreb", Municipality = "Donji Grad" },
            new() { Country = "Croatia", City = "Zagreb", Municipality = "Trešnjevka" },

            // Croatia - Rijeka
            new() { Country = "Croatia", City = "Rijeka", Municipality = "Trsat" },
            new() { Country = "Croatia", City = "Rijeka", Municipality = "Centar" },

            // Croatia - Split
            new() { Country = "Croatia", City = "Split", Municipality = "Meje" },
            new() { Country = "Croatia", City = "Split", Municipality = "Bačvice" },
            new() { Country = "Croatia", City = "Split", Municipality = "Split 3" }
        };

        foreach (var location in locations)
        {
            bool exists = await context.Locations.AnyAsync(l =>
                l.Country == location.Country &&
                l.City == location.City &&
                l.Municipality == location.Municipality);

            if (!exists)
            {
                context.Locations.Add(location);
            }
        }

        await context.SaveChangesAsync();
    }
}
