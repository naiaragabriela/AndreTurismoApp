using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.PackageService.Data
{
    public class AndreTurismoAppPackageServiceContext : DbContext
    {
        public AndreTurismoAppPackageServiceContext (DbContextOptions<AndreTurismoAppPackageServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Package> Package { get; set; } = default!;
    }
}
