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
