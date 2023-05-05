using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface ICityRepository
    {
        int Add(City city);

        List<City> GetAll();

        bool Update(City city);

        bool Delete(int id);
    }
}
