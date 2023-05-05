using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.AddressService.Data
{
    public class AndreTurismoAppAddressServiceContext : DbContext
    {
        public AndreTurismoAppAddressServiceContext(DbContextOptions<AndreTurismoAppAddressServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Address> Address { get; set; } = default!;
    }
}
