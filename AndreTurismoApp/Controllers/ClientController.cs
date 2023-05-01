using System.Net;
using AndreTurismoApp.ExternalService;
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
        private readonly ExternalClientService _clientService;

        public ClientController(ExternalClientService service)
        {
            _clientService = service;
        }

        [HttpPost(Name = "InsertClient")]
        public async Task<ActionResult> Add(Client client)
        {
            var statusCode = (int)await _clientService.PostClient(client);

            return StatusCode(statusCode);
        }

        [HttpGet(Name = "GetAllClient")]
        public async Task<List<Client>> GetAll()
        {
            var response = await _clientService.GetClient();
            return response;
        }

        [HttpPut(Name = "UpdateAllClient")]
        public async Task<ActionResult> Update(Client client)
        {
            var statusCode = (int)await _clientService.PutClient(client);

            return StatusCode(statusCode);
        }

        [HttpDelete(Name = "DeleteAllClient")]
        public async Task<ActionResult> Delete(int id)
        {
            var statusCode = (int)await _clientService.DeleteClient(id);

            return StatusCode(statusCode);
        }

    }
}
