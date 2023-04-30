using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        public int Add(Hotel hotel)
        {

            return new HotelService().Add(hotel);
        }

        public List<Hotel> GetAll()
        {
            return new HotelService().GetAll();
        }

        public int Update(Hotel hotel)

        {
            return new HotelService().Update(hotel);
        }

        public int Delete(int id)
        {
            return new HotelService().Delete(id);
        }
    }
}
