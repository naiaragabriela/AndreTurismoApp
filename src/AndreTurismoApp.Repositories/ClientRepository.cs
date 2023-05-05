using System.Data.SqlClient;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\projeto-agencia-turismo-ADO\src\banco\TourismAgencyADO.mdf";

        public int Add(Customer client)
        {
            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Customer.INSERT, new
                {
                    client.Name,
                    client.Phone,
                    client.DtRegistration,
                    IdAddress = client.Address.Id,
                });
            }
            return result;

        }

        public bool Delete(int id)
        {
            bool result = false;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                _ = db.Execute(Customer.DELETE, id);
                result = true;
            }
            return result;
        }

        public List<Customer> GetAll()
        {
            using (SqlConnection db = new(strConn))
            {
                db.Open();
                IEnumerable<Customer> client = db.Query<Customer, Address, City, Customer>(Customer.SELECT, (client, address, city) =>
                {
                    address.City = city;
                    client.Address = address;
                    return client;
                }, splitOn: "SplitAddress,SplitCity");

                return (List<Customer>)client;
            };
        }

        public bool Update(Customer client)
        {
            bool result = false;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                _ = db.Execute(Customer.UPDATE, client);
                result = true;
            }
            return result;
        }
    }
}
