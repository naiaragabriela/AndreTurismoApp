using AndreTurismoApp.Models;

namespace AndreTurismoApp.ClientService.Models
{
    public class ClientPostRequestDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int AddressId { get; set; }
    }
}
