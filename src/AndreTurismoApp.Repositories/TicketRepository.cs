using System.Data.SqlClient;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\projeto-agencia-turismo-ADO\src\banco\TourismAgencyADO.mdf";

        public int Add(Ticket ticket)
        {
            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Ticket.INSERT, new
                {
                    IdOrigin = ticket.Origin.Id,
                    IdDestination = ticket.Destination.Id,
                    ticket.DtRegistration,
                    ticket.Cost

                });
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = db.Execute(Ticket.DELETE, id);
            }
            return result;
        }

        public List<Ticket> GetAll()
        {
            using (SqlConnection db = new(strConn))
            {
                db.Open();
                IEnumerable<Ticket> ticket = db.Query<Ticket, Address, City, Address, City, Ticket>(Ticket.SELECT, (ticket, addressOrigin, cityOrigin,
                    addressDestination, cityDestination) =>
                {
                    addressOrigin.City = cityOrigin;
                    ticket.Origin = addressOrigin;
                    addressDestination.City = cityDestination;
                    ticket.Destination = addressDestination;

                    return ticket;
                }, splitOn: "SplitOrigin, SplitCityOrigin, SplitDestination, SplitCityDestination");

                return (List<Ticket>)ticket;
            }
        }
        public int Update(Ticket ticket)
        {

            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Ticket.UPDATE, new
                {
                    IdOrigin = ticket.Origin.Id,
                    IdDestination = ticket.Destination.Id,
                    ticket.DtRegistration,
                    ticket.Cost

                });
            }
            return result;
        }
    }
}
