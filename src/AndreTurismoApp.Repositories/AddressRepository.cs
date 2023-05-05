using System.Data.SqlClient;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\projeto-agencia-turismo-ADO\src\banco\TourismAgencyADO.mdf";

        public int Add(Address address)
        {

            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Address.INSERT, new
                {
                    address.Street,
                    address.Number,
                    address.Neighborhood,
                    address.PostalCode,
                    address.Complement,
                    address.DtRegistration,
                    IdCity = address.City.Id
                });
            }
            return result;
        }

        public List<Address> GetAll()
        {
            using (SqlConnection db = new(strConn))
            {
                db.Open();

                IEnumerable<Address> address = db.Query<Address, City, Address>(Address.SELECT, (address, city) =>
                {
                    address.City = city;
                    return address;
                }, splitOn: "SplitIdCity");



                return (List<Address>)address;
            }
        }

        public bool Delete(int id)
        {
            bool result = false;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                _ = db.Execute(Address.DELETE, id);
                result = true;
            }
            return result;
        }


        public bool Update(Address address)
        {

            bool result = false;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                _ = db.Execute(Address.UPDATE, new
                {
                    address.Street,
                    address.Number,
                    address.Neighborhood,
                    address.PostalCode,
                    address.Complement,
                    address.DtRegistration,
                    IdCity = address.City.Id
                });
                result = true;
            }
            return result;
        }
    }
}
