using AndreTurismoApp.ExternalService;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ExternalCityService _cityService;
        public CityController(ExternalCityService service)
        {
            _cityService = service;
        }

        [HttpPost( Name = "InsertCity")]
        public async Task<ActionResult>Add(City city)
        {
            var statusCode = (int)await _cityService.PostCity(city);

            return StatusCode(statusCode);
        }

        [HttpGet(Name = "GetAllCity")]
        public async Task<List<City>> GetAll()
        {
            var response = await _cityService.GetCity();

            return response;
        }

        [HttpPut(Name = "UpdateCity")]
        public async Task<ActionResult>Update(City city)
        {
            var statusCode = (int)await _cityService.PutCity(city);

            return StatusCode(statusCode);
        }

        [HttpDelete(Name = "DeleteCity")]
        public async Task<ActionResult>Delete(int id)
        {
            var statusCode = (int)await _cityService.DeleteCity(id);

            return StatusCode(statusCode);
        }
    }
}
