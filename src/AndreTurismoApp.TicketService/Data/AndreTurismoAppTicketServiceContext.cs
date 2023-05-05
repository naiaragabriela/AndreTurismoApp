using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.TicketService.Data
{
    public class AndreTurismoAppTicketServiceContext : DbContext
    {
        public AndreTurismoAppTicketServiceContext(DbContextOptions<AndreTurismoAppTicketServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Ticket> Ticket { get; set; } = default!;
    }
}
