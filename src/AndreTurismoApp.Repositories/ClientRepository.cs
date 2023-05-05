using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class ClientRepository: IClientRepository
    {
        readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\projeto-agencia-turismo-ADO\src\banco\TourismAgencyADO.mdf";

        public int Add(Client client)
        {
            int result = 0;

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(Client.INSERT, new
                {
                    Name = client.Name,
                    Phone = client.Phone,
                    DtRegistration = client.DtRegistration,
                    IdAddress = client.Address.Id,
                });
            }
            return result;

        }

        public bool Delete(int id)
        {
            bool result = false;

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                db.Execute(Client.DELETE, id);
                result = true;
            }
            return result;
        }

        public List<Client> GetAll()
        {
            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                var client = db.Query<Client, Address, City, Client>(Client.SELECT, (client, address, city) =>
                {
                    address.City = city;
                    client.Address = address;
                    return client;
                }, splitOn: "SplitAddress,SplitCity");

                return (List<Client>)client;
            };
        }

        public bool Update(Client client)
        {
            bool result = false;

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                db.Execute(Client.UPDATE, client);
                result = true;
            }
            return result;
        }
    }
}
