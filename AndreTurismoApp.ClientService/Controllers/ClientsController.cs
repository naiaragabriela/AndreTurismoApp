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
using System.Runtime.ConstrainedExecution;
using AndreTurismoApp.ClientService.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net;

namespace AndreTurismoApp.ClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AndreTurismoAppClientServiceContext _context;

        public ClientsController(AndreTurismoAppClientServiceContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient(string? name, string? cep, string? city)
        {
            if (_context.Client == null)
            {
                return new List<Client>();
            }

            var context = _context.Client.Include(x => x.Address).AsQueryable();

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

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client
                .Include(client => client.Address)
                .ThenInclude(address => address.City)
                .Where(client => client.Id == id)
                .FirstOrDefaultAsync();

            return client == null ? (ActionResult<Client>)NotFound() : (ActionResult<Client>)client;
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientPutRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound("ID invalido!!");
            }

            client.Name = request.Name;
            client.Phone = request.Phone;

            _context.Entry(client).State = EntityState.Modified;
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
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientPostRequestDTO? request)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'AndreTurismoAppClientServiceContext.Client'  is null.");
            }

            Client client = new Client()
            {
                Name = request.Name,
                Phone = request.Phone,
                AddressId = request.AddressId,
            };

            _context.Client.Add(client);
            await _context.SaveChangesAsync();


            ClientResponseDTO response = new()
            {
                Id = client.Id,
                Name = client.Name
            };

            return CreatedAtAction("GetClient", new { id = client.Id }, response);
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
