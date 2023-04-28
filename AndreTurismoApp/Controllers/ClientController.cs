using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = new ClientService();
        }

        [HttpPost(Name = "InsertAddress")]
        public int Add(Client client)
        {
            return _clientService.Add(client);
        }

        [HttpGet(Name = "GetAllAddress")]
        public List<Client> GetAll()
        {
            return _clientService.GetAll();
        }

        [HttpPut(Name = "UpdateAllAddress")]
        public bool Update(Client client)
        {

            return _clientService.Update(client);
        }
        public bool Delete(int id)
        {
            return _clientService.Delete(id);
        }

    }
}
