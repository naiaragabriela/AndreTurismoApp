using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface ICustomerRepository
    {

        int Add(Customer client);

        List<Customer> GetAll();

        bool Update(Customer client);

        bool Delete(int id);

    }
}
