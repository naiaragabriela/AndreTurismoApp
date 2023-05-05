using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService()
        {
            _clientRepository = new ClientRepository();
        }

        public int Add(Customer client)
        {
            return _clientRepository.Add(client);
        }

        public List<Customer> GetAll()
        {
            return _clientRepository.GetAll();
        }


        public bool Update(Customer client)
        {
            return _clientRepository.Update(client);
        }

        public bool Delete(int id)
        {
            return _clientRepository.Delete(id);
        }
    }
}
