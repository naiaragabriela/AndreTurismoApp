using System.Data.SqlClient;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\projeto-agencia-turismo-ADO\src\banco\TourismAgencyADO.mdf";

        public int Add(Package package)
        {
            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Package.INSERT, new
                {
                    IdHotel = package.Hotel.Id,
                    IdTicket = package.Ticket.Id,
                    package.DtRegistration,
                    package.Cost,
                    IdClient = package.Client.Id,
                });
            }
            return result;
        }

        public List<Package> GetAll()
        {
            using SqlConnection db = new(strConn);
            db.Open();

            var types = new[] { typeof(Package), typeof(Customer), typeof(Address), typeof(City), typeof(Hotel), typeof(Address), typeof(City), typeof(Ticket), typeof(Address), typeof(City), typeof(Address), typeof(City) };
            
           var pack = db.Query<Package>(Package.SELECT, types, (obj) =>
            {
                var package = obj[0] as Package;
                var client = obj[1] as Customer;
                var addressClient = obj[2] as Address;
                var cityClient = obj[3] as City;
                var hotel = obj[4] as Hotel;
                var addressHotel = obj[5] as Address;
                var cityHotel = obj[6] as City;
                var ticket = obj[7] as Ticket;
                var addressOrigin = obj[8] as Address;
                var cityOrigin = obj[9] as City;
                var addressDestination = obj[10] as Address;
                var cityDestination = obj[11] as City;

                client.Address = addressClient;
                addressClient.City = cityClient;
                package.Client = client;
                hotel.Address = addressHotel;
                addressHotel.City = cityHotel;
                package.Hotel = hotel;
                ticket.Origin = addressOrigin;
                addressOrigin.City = cityOrigin;
                ticket.Destination = addressDestination;
                addressDestination.City = cityDestination;
                package.Ticket = ticket;

                return package;
            }, splitOn: "SplitClient, SplitAddressClient,SplitCityClient,SplitHotel,SplitAddressHotel,SplitCityHotel,SplitTicket,SplitOrigin,SplitCityOrigin,SplitDestination,SplitCityDestination");
            return (List<Package>)pack;
        }

        public int Update(Package package)
        {
            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Package.UPDATE, new
                {
                    HotelId = package.Hotel.Id,
                    TicketId = package.Ticket.Id,
                    package.DtRegistration,
                    package.Cost,
                    ClientId = package.Client.Id,
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
                result = db.Execute(Package.DELETE, id);
            }
            return result;
        }
    }
}
