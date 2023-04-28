using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = new AddressService();
        }

        [HttpPost(Name = "InsertAddress")]
        public int Add(Address address)
        {
            return _addressService.Add(address);
        }

        [HttpGet(Name = "GetAllAddress")]
        public List<Address> GetAll()
        {
            return _addressService.GetAll();
        }

        [HttpPut(Name = "UpdateAllAddress")]
        public bool Update(Address address)
        {

            return _addressService.Update(address);
        }

        [HttpDelete(Name = "GetAllAddress")]
        public bool Delete(Address address)
        {
            return _addressService.Delete(address);
        }
    }

}
