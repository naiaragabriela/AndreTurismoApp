using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class TicketRepository: ITicketRepository
    {
        readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\projeto-agencia-turismo-ADO\src\banco\TourismAgencyADO.mdf";

        public int Add(Ticket ticket)
        {
            int result = 0;

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Ticket.INSERT, new
                {
                    IdOrigin = ticket.Origin.Id,
                    IdDestination = ticket.Destination.Id,
                    DtRegistration = ticket.DtRegistration,
                    CostTicket = ticket.CostTicket

                });
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                result = (int)db.Execute(Ticket.DELETE, id);
            }
            return result;
        }

        public List<Ticket> GetAll()
        {
            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                var ticket = db.Query<Ticket, Address, City, Address, City, Ticket>(Ticket.SELECT, (ticket, addressOrigin, cityOrigin,
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

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Ticket.UPDATE, new
                {
                    IdOrigin = ticket.Origin.Id,
                    IdDestination = ticket.Destination.Id,
                    DtRegistration = ticket.DtRegistration,
                    CostTicket = ticket.CostTicket

                });
            }
            return result;
        }
    }
}
