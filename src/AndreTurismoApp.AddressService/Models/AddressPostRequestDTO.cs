namespace AndreTurismoApp.AddressService.Models
{
    public class AddressPostRequestDTO
    {
        public int CityId { get; set; }
        public int Number { get; set; }
        public string PostalCode { get; set; }
        public string? Complement { get; set; }
    }
}
