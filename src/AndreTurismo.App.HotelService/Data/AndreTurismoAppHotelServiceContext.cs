using Microsoft.EntityFrameworkCore;

namespace AndreTurismo.App.HotelService.Data
{
    public class AndreTurismoAppHotelServiceContext : DbContext
    {
        public AndreTurismoAppHotelServiceContext(DbContextOptions<AndreTurismoAppHotelServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Hotel> Hotel { get; set; } = default!;
    }
}
