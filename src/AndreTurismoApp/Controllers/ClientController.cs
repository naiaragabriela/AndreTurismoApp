using AndreTurismoApp.ExternalService;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ExternalCustomerService _clientService;

        public ClientController(ExternalCustomerService service)
        {
            _clientService = service;
        }

        [HttpPost(Name = "InsertClient")]
        public async Task<ActionResult> Add(Customer client)
        {
            int statusCode = (int)await _clientService.PostCustomer(client);

            return StatusCode(statusCode);
        }

        [HttpGet(Name = "GetAllClient")]
        public async Task<List<Customer>> GetAll()
        {
            List<Customer> response = await _clientService.GetCustomers();

            return response;
        }

        [HttpPut(Name = "UpdateClient")]
        public async Task<ActionResult> Update(Customer client)
        {
            int statusCode = (int)await _clientService.PutCustomer(client);

            return StatusCode(statusCode);
        }

        [HttpDelete(Name = "DeleteClient")]
        public async Task<ActionResult> Delete(int id)
        {
            int statusCode = (int)await _clientService.DeleteCustomer(id);

            return StatusCode(statusCode);
        }

    }
}
