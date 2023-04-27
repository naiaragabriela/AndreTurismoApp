using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Client
    {
            #region Constant
            public readonly static string INSERT = "INSERT INTO CLIENT (Name, Phone, DtRegistration, IdAddress) " +
                        "VALUES (@Name, @Phone, @DtRegistration,@IdAddress); select cast(scope_identity() as int)";



            public readonly static string SELECT = "SELECT[Client].[Id] AS Id, [Name], [Phone],[Client].[DtRegistration],[AddressClient].[Id] AS SplitAddress," +
              "[AddressClient].[Id] AS Id, [Street],[Number],[Neighborhood],[PostalCode],[Complement],[AddressClient].[DtRegistration]," +
              "[AddressCity].[Id] AS SplitCity, [AddressCity].[Id] AS Id, [AddressCity].[NameCity], [AddressCity].[DtRegistration] " +
               "FROM[Client] JOIN[Address] AddressClient ON IdAddress = AddressClient.Id " +
               "JOIN[City] AddressCity ON AddressClient.IdCity= AddressCity.Id";


            public readonly static string UPDATE = "UPDATE CLIENT SET " +
                                                   "Name = @Name," +
                                                   "Phone = @Phone," +
                                                   "Address= IdAddress " +
                                                  " WHERE Id = @id";


            public readonly static string DELETE = "DELETE FROM CLIENT WHERE id =@id";

            #endregion

            #region Properties

            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Address Address { get; set; }
            public DateTime DtRegistration { get; set; }

            #endregion

            #region Methods
            public override string ToString()
            {
                return "Id do Cliente: " + Id +
                       "\nNome: " + Name +
                       "\nPhone: " + Phone +
                       "\nData de Registro do Cliente: " + DtRegistration +
                       "\nEndereço: " + Address.ToString();
            }
            #endregion
    }
    
}
