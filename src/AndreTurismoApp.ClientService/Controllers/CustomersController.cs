using AndreTurismoApp.ClientService.Data;
using AndreTurismoApp.ClientService.Models;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.ClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AndreTurismoAppClientServiceContext _context;

        public CustomersController(AndreTurismoAppClientServiceContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(string? name, string? cep, string? city)
        {
            if (_context.Client == null)
            {
                return new List<Customer>();
            }

            IQueryable<Customer> context = _context.Client.Include(x => x.Address).ThenInclude(address => address.City).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                context = context.Where(x => x.Name.Equals(name));
            }

            if (!string.IsNullOrEmpty(cep))
            {
                context = context.Where(x => x.Address.PostalCode.Equals(cep));
            }

            if (!string.IsNullOrEmpty(city))
            {
                context = context.Where(x => x.Address.City.Name.Equals(city));
            }

            return await context.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            Customer? client = await _context.Client
                .Include(client => client.Address)
                .ThenInclude(address => address.City)
                .Where(client => client.Id == id)
                .SingleOrDefaultAsync();

            return client == null ? (ActionResult<Customer>)NotFound() : (ActionResult<Customer>)client;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, ClientPutRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            Customer? client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound("ID invalido!!");
            }

            client.Name = request.Name;
            client.Phone = request.Phone;

            _context.Entry(client).State = EntityState.Modified;
            try
            {
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(ClientPostRequestDTO? request)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'AndreTurismoAppClientServiceContext.Client'  is null.");
            }

            Customer client = new()
            {
                Name = request.Name,
                Phone = request.Phone,
                AddressId = request.AddressId,
            };

            _ = _context.Client.Add(client);
            _ = await _context.SaveChangesAsync();


            ClientResponseDTO response = new()
            {
                Id = client.Id,
                Name = client.Name
            };

            return CreatedAtAction("GetClient", new { id = client.Id }, response);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            Customer? client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _ = _context.Client.Remove(client);
            _ = await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
