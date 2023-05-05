using AndreTurismoApp.CityService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Rabbit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AndreTurismoApp.CityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly AndreTurismoAppCityServiceContext _context;
        private readonly Producer _producer;
        public CitiesController(AndreTurismoAppCityServiceContext context, Producer producer)
        {
            _context = context;
            _producer = producer;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity(string? city)
        {
            if (_context.City == null)
            {
                return NotFound();
            }

            var context = _context.City.AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                context = context.Where(x => x.Name.Contains(city));
            }
            return await context.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            if (_context.City == null)
            {
                return NotFound();
            }

            var city = await _context.City.FindAsync(id);

            return city == null ? NotFound() : city;
        }


        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            if (_context.City == null)
            {
                return Problem("Entity set 'AndreTurismoAppCityServiceContext.City'  is null.");
            }

            //_context.City.Add(city);
            //await _context.SaveChangesAsync();

            _producer.Send("city", JsonConvert.SerializeObject(city));

            //return CreatedAtAction("GetCity", new { id = city.Id }, city);

            return Accepted();
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            if (_context.City == null)
            {
                return NotFound();
            }

            var city = await _context.City.FindAsync(id);
           
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return (_context.City?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
