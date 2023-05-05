namespace AndreTurismo.App.HotelService.Models
{
    public class HotelPostRequestDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AddressId { get; set; }
        public decimal Cost { get; set; }
    }
}
