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
        private CityService _cityService;
        public CityController()
        {
            _cityService = new CityService();
        }

        [HttpPost( Name = "InsertCity")]
        public int Add(City city)
        {
            return _cityService.Add(city);
        }

        [HttpGet(Name = "GetAllCity")]
        public List<City> GetAll()
        {
            return _cityService.GetAll();
        }

        [HttpPut(Name = "UpdateCity")]
        public bool Update(City city)
        {
            return _cityService.Update(city);
        }

        [HttpDelete(Name = "DeleteCity")]
        public bool Delete(int id)
        {
            return _cityService.Delete(id);
        }
    }
}
