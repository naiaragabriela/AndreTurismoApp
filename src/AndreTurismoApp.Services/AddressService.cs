using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class AddressService
    {

        private readonly IAddressRepository _addressRepository;
        public AddressService()
        {
            _addressRepository = new AddressRepository();
        }
        public int Add(Address address)
        {
            return _addressRepository.Add(address);
        }

        public List<Address> GetAll()
        {
            return _addressRepository.GetAll();
        }

        public bool Update(Address address)

        {
            return _addressRepository.Update(address);
        }

        public bool Delete(int id)
        {
            return _addressRepository.Delete(id);
        }

    }

}
