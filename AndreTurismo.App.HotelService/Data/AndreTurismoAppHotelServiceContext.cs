using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismo.App.HotelService.Data
{
    public class AndreTurismoAppHotelServiceContext : DbContext
    {
        public AndreTurismoAppHotelServiceContext (DbContextOptions<AndreTurismoAppHotelServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Hotel> Hotel { get; set; } = default!;
    }
}
