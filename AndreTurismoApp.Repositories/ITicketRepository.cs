using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface ITicketRepository
    {
        int Add(Ticket ticket);

        List<Ticket> GetAll();

        int Update(Ticket ticket);

        int Delete(int id);
    }
}
