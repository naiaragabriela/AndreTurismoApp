using AndreTurismoApp.Models;

namespace AndreTurismoApp.TicketService.Models
{
    public class TicketPostRequestDTO
    {
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public decimal Cost { get; set; }
    }
}
