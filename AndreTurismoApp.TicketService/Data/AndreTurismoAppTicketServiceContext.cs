using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.TicketService.Data
{
    public class AndreTurismoAppTicketServiceContext : DbContext
    {
        public AndreTurismoAppTicketServiceContext (DbContextOptions<AndreTurismoAppTicketServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Ticket> Ticket { get; set; } = default!;
    }
}
