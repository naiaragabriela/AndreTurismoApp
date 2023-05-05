using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.ClientService.Data
{
    public class AndreTurismoAppClientServiceContext : DbContext
    {
        public AndreTurismoAppClientServiceContext (DbContextOptions<AndreTurismoAppClientServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Client> Client { get; set; } = default!;
    }
}
