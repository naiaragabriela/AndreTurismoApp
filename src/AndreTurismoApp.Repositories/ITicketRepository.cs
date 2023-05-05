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
