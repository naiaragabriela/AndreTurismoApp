using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService()
        {
            _ticketRepository = new TicketRepository();
        }
        public int Add(Ticket ticket)
        {

            return _ticketRepository.Add(ticket);
        }
        public List<Ticket> GetAll()
        {
            return _ticketRepository.GetAll();
        }

        public int Update(Ticket ticket)
        {

            return _ticketRepository.Update(ticket);
        }

        public int Delete(int id)
        {
            return _ticketRepository.Delete(id);
        }
    }
}
