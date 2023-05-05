using AndreTurismoApp.ExternalService;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ExternalHotelService _hotelService;

        public HotelController(ExternalHotelService service)
        {
            _hotelService = service;
        }

        [HttpPost(Name = "InsertHotel")]
        public async Task<ActionResult> Add(Hotel hotel)
        {
            int statusCode = (int)await _hotelService.PostHotel(hotel);

            return StatusCode(statusCode);
        }

        [HttpGet(Name = "GetAllHotel")]
        public async Task<List<Hotel>> GetAll()
        {
            List<Hotel> response = await _hotelService.GetHotel();
            return response;
        }

        [HttpPut(Name = "UpdateHotel")]
        public async Task<ActionResult> Update(Hotel hotel)

        {
            int statusCode = (int)await _hotelService.PutHotel(hotel);

            return StatusCode(statusCode);
        }

        [HttpDelete(Name = "DeleteHotel")]
        public async Task<ActionResult> Delete(int id)
        {
            int statusCode = (int)await _hotelService.DeleteHotel(id);

            return StatusCode(statusCode);
        }
    }
}
