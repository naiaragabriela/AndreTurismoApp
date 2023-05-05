using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public int Add(Client client)
        {
            return _clientRepository.Add(client);
        }

        public List<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }


        public bool Update(Client client)
        {
            return _clientRepository.Update(client);
        }

        public bool Delete(int id)
        {
            return _clientRepository.Delete(id);
        }
    }
}
