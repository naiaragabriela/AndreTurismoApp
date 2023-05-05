using System.Data.SqlClient;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\projeto-agencia-turismo-ADO\src\banco\TourismAgencyADO.mdf";

        public int Add(Hotel hotel)
        {
            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Hotel.INSERT, new
                {
                    hotel.Name,
                    hotel.Cost,
                    hotel.DtRegistration,
                    IdAddress = hotel.Address.Id
                });
            }
            return result;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Hotel> GetAll()
        {
            using (SqlConnection db = new(strConn))
            {
                db.Open();
                IEnumerable<Hotel> hotel = db.Query<Hotel, Address, City, Hotel>(Hotel.SELECT, (hotel, address, city) =>
                {
                    address.City = city;
                    hotel.Address = address;
                    return hotel;
                }, splitOn: "SplitAddress, SplitCity");

                return (List<Hotel>)hotel;
            }
        }

        public int Update(Hotel hotel)
        {
            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Hotel.UPDATE, new
                {
                    hotel.Name,
                    hotel.Cost,
                    hotel.DtRegistration,
                    IdAddress = hotel.Address.Id
                });
            }
            return result;
        }
    }
}
