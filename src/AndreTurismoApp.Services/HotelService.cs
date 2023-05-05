using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class HotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService()
        {
            _hotelRepository = new HotelRepository();
        }

        public int Add(Hotel hotel)
        {
            return _hotelRepository.Add(hotel);

        }

        public List<Hotel> GetAll()
        {
            return _hotelRepository.GetAll();
        }

        public int Update(Hotel hotel)
        {

            return _hotelRepository.Update(hotel);
        }

        public int Delete(int id)
        {
            return _hotelRepository.Delete(id);
        }


    }
}

