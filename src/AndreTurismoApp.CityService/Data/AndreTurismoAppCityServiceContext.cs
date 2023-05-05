using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.CityService.Data
{
    public class AndreTurismoAppCityServiceContext : DbContext
    {
        public AndreTurismoAppCityServiceContext (DbContextOptions<AndreTurismoAppCityServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.City> City { get; set; } = default!;
    }
}
