using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismo.App.HotelService.Data;
using AndreTurismoApp.Models;
using AndreTurismo.App.HotelService.Models;

namespace AndreTurismo.App.HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AndreTurismoAppHotelServiceContext _context;

        public HotelsController(AndreTurismoAppHotelServiceContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel(string? name, string? cep, string? city)
        {
            if (_context.Hotel == null)
            {
                return new List<Hotel>();
            }
            var context = _context.Hotel.Include(hotel => hotel.Address).AsQueryable();

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

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel
                .Include(hotel => hotel.Address)
                .ThenInclude(address => address.City)
                .Where(hotel => hotel.Id == id)
                .SingleOrDefaultAsync();

            return hotel == null ? (ActionResult<Hotel>)NotFound() : (ActionResult<Hotel>)hotel;
        }

        // PUT: api/Hotels/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelPutRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var hotel = await _context.Hotel.FindAsync(id);

            if (hotel == null)
            {
                return NotFound("ID invalido!!");
            }

            hotel.Name = request.Name;
            hotel.Cost = request.Cost;

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(HotelPostRequestDTO request)
        {
            if (_context.Hotel == null)
            {
                return Problem("Entity set 'AndreTurismoAppHotelServiceContext.Hotel'  is null.");
            }
            Hotel hotel = new Hotel()
            {
                Name = request.Name,
                Cost = request.Cost,
                AddressId = request.AddressId
            };

            HotelResponseDTO response = new()
            {
                Id = hotel.Id,
                Name = hotel.Name
            };

            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, response);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
