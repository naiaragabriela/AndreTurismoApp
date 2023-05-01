using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.AddressService.Models;
using AndreTurismoApp.ExternalService;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.AddressService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AndreTurismoAppAddressServiceContext _context;

        public AddressesController(AndreTurismoAppAddressServiceContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress(string? cep, string? city, string? neighborhood)
        {
            if (_context.Address == null)
            {
                return new List<Address>();
            }

            var context = _context.Address.Include(a => a.City).AsQueryable();

            if (!string.IsNullOrEmpty(cep))
            {
                context = context.Where(x => x.PostalCode.Equals(cep));
            }


            if (!string.IsNullOrEmpty(city))
            {
                context = context.Where(x => x.City.Name.Equals(city));
            }

            if (!string.IsNullOrEmpty(neighborhood))
            {
                context = context.Where(x => x.Neighborhood.Equals(neighborhood));
            }

            return await context.ToListAsync();
        }


        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }

            var address = await _context.Address.Include(a => a.City).Where(a => a.Id == id).SingleOrDefaultAsync();

            return address == null ? (ActionResult<Address>)NotFound() : (ActionResult<Address>)address;
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAddress(int id, AddressPutRequestDTO request)
        {

            var postofficeResult = await PostOfficesService.GetAddress(request.PostalCode);

            if (postofficeResult == null)
            {
                return BadRequest("CEP invalido!!");
            }

            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound("ID invalido!!");
            }

            address.Neighborhood = postofficeResult.Neighborhood;
            address.Number = request.Number;
            address.Complement = request.Complement;
            address.Street = postofficeResult.Street;
            address.PostalCode = request.PostalCode;

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(AddressPostRequestDTO request)
        {

            if (request.PostalCode.Length != 8)
            {
                return BadRequest("CEP é invalido!!");
            }

            if (_context.Address == null)
            {
                return Problem("Entity set 'AndreTurismoAppAddressServiceContext.Address'  is null.");
            }

           var post = await PostOfficesService.GetAddress(request.PostalCode);

            Address address = new()
            {
                Street = post.Street,
                Number = request.Number,
                Neighborhood = post.Neighborhood,
                PostalCode = request.PostalCode,
                Complement = string.Empty,
                CityId = request.CityId,
            };

            _context.Address.Add(address);

             await _context.SaveChangesAsync();

            AddressResponseDTO response = new()
            {
                Id = address.Id,
                PostalCode = address.PostalCode
            };

            return CreatedAtAction("GetAddress", new { id = response.Id }, response);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

             _context.Address.Remove(address);

             await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
