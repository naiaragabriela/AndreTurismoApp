using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class CityService
    {
        private readonly ICityRepository _repository;
        public CityService()
        {
            _repository = new CityRepository();
        }
        public int Add(City city)
        {
            return _repository.Add(city);

        }

        public List<City> GetAll()
        {
            return _repository.GetAll();
        }

        public bool Update(City city)
        {
            return _repository.Update(city);
        }


        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}