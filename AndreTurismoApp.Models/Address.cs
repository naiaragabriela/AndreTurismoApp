using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Address
    {
        #region Constant

        public static readonly string INSERT = "INSERT INTO ADDRESS(Street, Number, Neighborhood, PostalCode, Complement, DtRegistration, IdCity) " +
                    "VALUES (@Street, @Number, @Neighborhood, @PostalCode, @Complement, @DtRegistration, @IdCity); " +
                    "select cast(scope_identity() as int)";



        public static readonly string SELECT = "SELECT[Address].[Id] AS Id, [Street],[Number],[Neighborhood],[PostalCode],[Complement],[Address].[DtRegistration]," +
                                               "[addressCity].[Id] AS SplitIdCity,[addressCity].[Id], [addressCity].[NameCity], [addressCity].[DtRegistration]" +
                                                "FROM[Address] JOIN[City] addressCity ON address.IdCity = addressCity.Id";

        public static readonly string UPDATE = "UPDATE ADDRESS SET Street = @Street" +
                                               "Number = @Number, " +
                                               "Neighborhood = @Neighborhood, " +
                                               "PostalCode = @PostalCode, " +
                                               "Complement = @Complement, " +
                                               "City = @IdCity " +
                                               "where id = @id";

        public static readonly string DELETE = "DELETE FROM ADDRESS WHERE Id = @Id";
        #endregion

        #region Properties 
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Neighborhood { get; set; }
        public string PostalCode { get; set; }
        public string Complement { get; set; }
        public City City { get; set; }
        public DateTime DtRegistration { get; set; }
        #endregion

    }
}
