using AndreTurismoApp.Models;
using AndreTurismoApp.TicketService.Data;
using AndreTurismoApp.TicketService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.TicketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly AndreTurismoAppTicketServiceContext _context;

        public TicketsController(AndreTurismoAppTicketServiceContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket(
            string? postalCodeOrigin, 
            string? cityOrigin,
            string? postalCodeDestination, 
            string? cityDestination)
        {
            if (_context.Ticket == null)
            {
                return new List<Ticket>();
            }

            var context = _context.Ticket
                .Include(ticket => ticket.Origin)
                .ThenInclude(origin => origin.City)
                .Include(x => x.Destination).
                ThenInclude(destination => destination.City)
                .AsQueryable();

            if (!string.IsNullOrEmpty(postalCodeOrigin))
            {
                context = context.Where(x => x.Origin.PostalCode.Equals(postalCodeOrigin));
            }

            if (!string.IsNullOrEmpty(cityOrigin))
            {
                context = context.Where(x => x.Origin.City.Name.Equals(cityOrigin));
            }

            if (!string.IsNullOrEmpty(postalCodeDestination))
            {
                context = context.Where(x => x.Origin.PostalCode.Equals(postalCodeDestination));
            }

            if (!string.IsNullOrEmpty(cityDestination))
            {
                context = context.Where(x => x.Origin.City.Name.Equals(cityDestination));
            }

            return await context.ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }

           var ticket = await _context.Ticket
                .Include(ticket => ticket.Origin)
                .ThenInclude(origin => origin.City)
                .Include(ticket => ticket.Destination)
                .ThenInclude(destination => destination.City)
                .Where(ticket => ticket.Id == id)
                .SingleOrDefaultAsync();

            return ticket == null ? NotFound() : ticket;
        }

        // PUT: api/Tickets/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, TicketPutRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var ticket = await _context.Ticket.FindAsync(id);


            if (ticket == null)
            {
                return NotFound("ID invalido!!");
            }

            ticket.Cost = request.Cost;

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(TicketPostRequestDTO request)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'AndreTurismoAppTicketServiceContext.Ticket'  is null.");
            }

            Ticket ticket = new()
            {
                OriginId = request.OriginId,
                DestinationId = request.DestinationId,
                Cost = request.Cost,
            };

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            Ticket response = new()
            {
                Id = ticket.Id,
            };

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, response);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
