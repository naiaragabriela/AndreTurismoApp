using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.ClientService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.ExternalService;
using AndreTurismoApp.Services;
using System.ComponentModel.DataAnnotations;

namespace AndreTurismoApp.ClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AndreTurismoAppClientServiceContext _context;
        private readonly ExternalAddressService _externalAddressService;
        private readonly ExternalCityService _externalCityService;

        public ClientsController(AndreTurismoAppClientServiceContext context, ExternalAddressService externalAddressService, ExternalCityService externalCityService)
        {
            _context = context;
            _externalCityService = externalCityService;
            _externalAddressService = externalAddressService;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
          if (_context.Client == null)
          {
              return NotFound();
          }
            return await _context.Client.Include(client=> client.Address).ThenInclude(address=>address.City).ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
          if (_context.Client == null)
          {
              return NotFound();
          }
            var client = await _context.Client.Include(client=>client.Address).ThenInclude(address=>address.City).Where(client => client.Id == id).FirstOrDefaultAsync();

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Update(client.Address);
            _context.Update(client);

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(string cep, int number, int idCity,string name, string phone)
        {
          if (_context.Client == null)
          {
              return Problem("Entity set 'AndreTurismoAppClientServiceContext.Client'  is null.");
          }
            var post = PostOfficesService.GetAddress(cep).Result;

            var city = _externalCityService.GetCityById(idCity).Result;


            Client client = new Client()
            {
                Name = name,
                Phone = phone,
                Address =
                {
                Street = post.Street,
                Number = number,
                Neighborhood = post.Neighborhood,
                PostalCode = cep,
                Complement = " ",
                IdCity = city.Id
                }
            };

            _context.Client.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
