using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public int Add(Ticket ticket)
        {

            return new TicketService().Add(ticket);
        }

        public List<Ticket> GetAll()
        {
            return new TicketService().GetAll();
        }

        public int Update(Ticket ticket)
        {
            return new TicketService().Update(ticket);
        }

        public int Delete(int id)
        {
            return new TicketService().Delete(id);
        }
    }
}
