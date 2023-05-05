using System.Data.SqlClient;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class CityRepository: ICityRepository
    {

        readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\projeto-agencia-turismo-ADO\src\banco\TourismAgencyADO.mdf";

        public int Add(City city)
        {
            int result = 0;

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(City.INSERT, city);
            }
            return result;
        }

        public List<City> GetAll()
        {
            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                var city = db.Query<City>(City.SELECT);
                return (List<City>)city;
            }
        }

        public bool Update(City city)
        {
            bool result = false;

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                db.Execute(City.UPDATE, city);
                result = false;          
            }
            return result;

        }

        public bool Delete(int id)
        {
            bool result = false;

            using (var db = new SqlConnection(strConn))
            {
                db.Open();
                db.Execute(City.DELETE, id);
                result = true;
            }
            return result;

        }
    }

}