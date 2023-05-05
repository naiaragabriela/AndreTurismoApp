namespace AndreTurismoApp.PackageService.Models
{
    public class PackagePostRequestDTO
    {
        public int HotelId { get; set; }
        public int TicketId { get; set; }
        public decimal Cost { get; set; }
        public int CustomerId { get; set; }
    }
}
