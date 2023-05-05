using AndreTurismoApp.Models;
using AndreTurismoApp.PackageService.Data;
using AndreTurismoApp.PackageService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.PackageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly AndreTurismoAppPackageServiceContext _context;

        public PackagesController(AndreTurismoAppPackageServiceContext context)
        {
            _context = context;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage(string? nameHotel, int? ticketId, string? nameClient)
        {
            if (_context.Package == null)
            {
                return new List<Package>();
            }

            var context = _context.Package
                  .Include(package => package.Hotel)
                  .ThenInclude(hotel => hotel.Address)
                  .ThenInclude(address => address.City)
                  .Include(package => package.Ticket)
                  .ThenInclude(ticket => ticket.Origin)
                  .ThenInclude(origin => origin.City)
                  .Include(package => package.Ticket)
                  .ThenInclude(ticket => ticket.Destination)
                  .ThenInclude(destination => destination.City)
                  .Include(package => package.Client)
                  .ThenInclude(client => client.Address)
                  .ThenInclude(address => address.City)
                  .AsQueryable();

            if (!string.IsNullOrEmpty(nameHotel))
            {
                context = context.Where(x => x.Hotel.Name.Equals(nameHotel));
            }

            if (ticketId is not 0 and not null)
            {
                context = context.Where(x => x.Ticket.Id.Equals(ticketId));
            }

            if (!string.IsNullOrEmpty(nameClient))
            {
                context = context.Where(x => x.Client.Name.Equals(nameClient));
            }

            return await context.ToListAsync();

        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }

           var package = await _context.Package
                  .Include(package => package.Hotel)
                  .ThenInclude(hotel => hotel.Address)
                  .ThenInclude(address => address.City)
                  .Include(package => package.Ticket)
                  .ThenInclude(ticket => ticket.Origin)
                  .ThenInclude(origin => origin.City)
                  .Include(package => package.Ticket)
                  .ThenInclude(ticket => ticket.Destination)
                  .ThenInclude(destination => destination.City)
                  .Include(package => package.Client)
                  .ThenInclude(client => client.Address)
                  .ThenInclude(address => address.City)
                  .Where(package => package.Id == id)
                  .SingleOrDefaultAsync();

            return package == null ? (ActionResult<Package>)NotFound() : (ActionResult<Package>)package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, PackagePutRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var package = await _context.Package.FindAsync(id);

            if (package == null)
            {
                return NotFound("ID invalido!!");
            }

            package.Cost = request.Cost;

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                 await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
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

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(PackagePostRequestDTO request)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'AndreTurismoAppPackageServiceContext.Package'  is null.");
            }

            Package package = new()
            {
                HotelId = request.HotelId,
                CustomerId = request.CustomerId,
                TicketId = request.TicketId,
                Cost = request.Cost
            };

             _context.Package.Add(package);
            await _context.SaveChangesAsync();

            Package response = new()
            {
                Id = package.Id,
                CustomerId = package.CustomerId,
            };

            return CreatedAtAction("GetPackage", new { id = package.Id }, response);
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            Package? package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
