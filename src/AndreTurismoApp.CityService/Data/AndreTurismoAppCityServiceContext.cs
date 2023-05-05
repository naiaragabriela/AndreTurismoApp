using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.CityService.Data
{
    public class AndreTurismoAppCityServiceContext : DbContext
    {
        public AndreTurismoAppCityServiceContext(DbContextOptions<AndreTurismoAppCityServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.City> City { get; set; } = default!;
    }
}
