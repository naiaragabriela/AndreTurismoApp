﻿using System.Data.SqlClient;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\adm\source\repos\AndreTurismoApp\database\TourismAgency.mdf";

        public int Add(City city)
        {
            int result = 0;

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                result = (int)db.ExecuteScalar(City.INSERT, city);
            }
            return result;
        }

        public List<City> GetAll()
        {
            using (SqlConnection db = new(strConn))
            {
                db.Open();
                var city = db.Query<City>(City.SELECT);
                return (List<City>)city;
            }
        }

        public bool Update(City city)
        {
            bool result = false;

            using (SqlConnection db = new(strConn))
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

            using (SqlConnection db = new(strConn))
            {
                db.Open();
                db.Execute(City.DELETE, id);
                result = true;
            }
            return result;

        }
    }

}