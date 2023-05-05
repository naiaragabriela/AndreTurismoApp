using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.ClientService.Data
{
    public class AndreTurismoAppClientServiceContext : DbContext
    {
        public AndreTurismoAppClientServiceContext(DbContextOptions<AndreTurismoAppClientServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Customer> Customer { get; set; } = default!;
    }
}
