using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class CityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService()
        {
            _cityRepository = new CityRepository();
        }
        public int Add(City city)
        {
            return _cityRepository.Add(city);

        }

        public List<City> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public bool Update(City city)
        {
            return _cityRepository.Update(city);
        }


        public bool Delete(int id)
        {
            return _cityRepository.Delete(id);
        }
    }
}