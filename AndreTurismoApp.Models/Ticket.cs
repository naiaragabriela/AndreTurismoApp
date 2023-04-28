using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Ticket
    {
        #region Constant
        public readonly static string INSERT = "INSERT INTO Ticket (IdOrigin, IdDestination, DtRegistration, CostTicket) " +
                       "VALUES (@IdOrigin, @IdDestination, @DtRegistration, @CostTicket); select cast(scope_identity() as int)";


        public readonly static string SELECT = "SELECT[Ticket].[Id] AS Id, [CostTicket], [Ticket].[DtRegistration]," +
         " [Ticket].[IdOrigin] AS SplitOrigin, [AddressOrigin].[Id] AS Id, [AddressOrigin].[Street], [AddressOrigin].[Number]," +
            " [AddressOrigin].[Neighborhood], [AddressOrigin].[PostalCode], [AddressOrigin].[Complement], [AddressOrigin].[DtRegistration], " +
            "[CityOrigin].[Id] AS SplitCityOrigin, [CityOrigin].[Id] AS Id, [CityOrigin].[NameCity], [CityOrigin].[DtRegistration], " +
            "[Ticket].[IdDestination] AS SplitDestination, [AddressDestination].[Id] AS Id, [AddressDestination].[Street]," +
            " [AddressDestination].[Number] ,[AddressDestination].[Neighborhood], [AddressDestination].[PostalCode], [AddressDestination].[Complement]," +
            " [AddressDestination].[DtRegistration], [CityDestination].[Id] AS SplitCityDestination, [CityDestination].[Id] AS Id, " +
            "[CityDestination].[NameCity], [CityDestination].[DtRegistration] " +
             "FROM[Ticket]" +
            "JOIN[Address] AddressOrigin ON IdOrigin = AddressOrigin.Id " +
            "JOIN[City] CityOrigin ON AddressOrigin.IdCity = CityOrigin.Id " +
            "JOIN[Address] AddressDestination ON IdDestination = AddressDestination.Id " +
            "JOIN[City] CityDestination ON AddressDestination.IdCity = CityDestination.Id";


        public readonly static string UPDATE = "UPDATE Ticket SET " +
                                               "Origin = @IdOrigin, " +
                                               "Destination = @IdDestination, " +
                                               "CostTicket = @CostTicket " +
                                                "WHERE Id = @id";


        public readonly static string DELETE = "DELETE FROM Ticket WHERE id =@id";

        #endregion

        #region Properties
        public int Id { get; set; }
        public Address Origin { get; set; }
        public Address Destination { get; set; }
        public DateTime DtRegistration { get; set; }
        public decimal CostTicket { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return "Id da Passagem: " + Id +
                   "\nCusto da Passagem: " + CostTicket +
                   "\nData de Registro da Passagem: " + DtRegistration +
                   "\nOrigem da Passagem: " + Origin.ToString() +
                   "\nDestino da Passagem: " + Destination.ToString();
        }
        #endregion
    }
}
