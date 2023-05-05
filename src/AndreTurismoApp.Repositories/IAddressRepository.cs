using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface IAddressRepository
    {
        int Add(Address address);

        List<Address> GetAll();

        bool Update(Address address);

        bool Delete(int id);

    }
}
