using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.PackageService.Data
{
    public class AndreTurismoAppPackageServiceContext : DbContext
    {
        public AndreTurismoAppPackageServiceContext(DbContextOptions<AndreTurismoAppPackageServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Package> Package { get; set; } = default!;
    }
}
