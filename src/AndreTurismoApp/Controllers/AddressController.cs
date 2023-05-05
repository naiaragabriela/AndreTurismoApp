using AndreTurismoApp.ExternalService;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly ExternalAddressService _addressService;

        public AddressController(ExternalAddressService services)
        {
            _addressService = services;
        }

        [HttpPost(Name = "InsertAddress")]
        public async Task<ActionResult> Add(Address address)
        {
            var statusCode = (int)await _addressService.PostAddress(address);

            return StatusCode(statusCode);
        }

        [HttpGet(Name = "GetAllAddress")]
        public async Task<List<Address>> GetAll()
        {
            var response = await _addressService.GetAddress();
            
            return response;
        }

        [HttpPut(Name = "UpdateAddress")]
        public async Task<ActionResult> Update(Address address)
        {
            var statusCode = (int)await _addressService.PutAddress(address);

            return StatusCode(statusCode);
        }

        [HttpDelete(Name = "DeleteAddress")]
        public async Task<ActionResult> Delete(int  id)
        {
            var statusCode = (int)await _addressService.DeleteAddress(id);

            return StatusCode(statusCode);
        }
    }

}
