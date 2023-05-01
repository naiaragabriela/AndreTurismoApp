namespace AndreTurismoApp.Models
{
    public class Ticket
    {
        #region Constant
        public static readonly string INSERT = "INSERT INTO Ticket (IdOrigin, IdDestination, DtRegistration, Cost) " +
                       "VALUES (@IdOrigin, @IdDestination, @DtRegistration, @Cost); select cast(scope_identity() as int)";


        public static readonly string SELECT = "SELECT[Ticket].[Id] AS Id, [Cost], [Ticket].[DtRegistration]," +
         " [Ticket].[IdOrigin] AS SplitOrigin, [AddressOrigin].[Id] AS Id, [AddressOrigin].[Street], [AddressOrigin].[Number]," +
            " [AddressOrigin].[Neighborhood], [AddressOrigin].[PostalCode], [AddressOrigin].[Complement], [AddressOrigin].[DtRegistration], " +
            "[CityOrigin].[Id] AS SplitCityOrigin, [CityOrigin].[Id] AS Id, [CityOrigin].[Name], [CityOrigin].[DtRegistration], " +
            "[Ticket].[IdDestination] AS SplitDestination, [AddressDestination].[Id] AS Id, [AddressDestination].[Street]," +
            " [AddressDestination].[Number] ,[AddressDestination].[Neighborhood], [AddressDestination].[PostalCode], [AddressDestination].[Complement]," +
            " [AddressDestination].[DtRegistration], [CityDestination].[Id] AS SplitCityDestination, [CityDestination].[Id] AS Id, " +
            "[CityDestination].[Name], [CityDestination].[DtRegistration] " +
             "FROM[Ticket]" +
            "JOIN[Address] AddressOrigin ON IdOrigin = AddressOrigin.Id " +
            "JOIN[City] CityOrigin ON AddressOrigin.IdCity = CityOrigin.Id " +
            "JOIN[Address] AddressDestination ON IdDestination = AddressDestination.Id " +
            "JOIN[City] CityDestination ON AddressDestination.IdCity = CityDestination.Id";


        public static readonly string UPDATE = "UPDATE Ticket SET " +
                                               "Origin = @IdOrigin, " +
                                               "Destination = @IdDestination, " +
                                               "Cost = @Cost " +
                                                "WHERE Id = @id";


        public static readonly string DELETE = "DELETE FROM Ticket WHERE id =@id";

        #endregion

        #region Properties
        public int Id { get; set; }
        public Address Origin { get; set; }
        public int OriginId { get; set; }
        public Address Destination { get; set; }
        public int DestinationId { get; set; }
        public DateTime DtRegistration { get; set; }
        public decimal Cost { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return "Id da Passagem: " + Id +
                   "\nCusto da Passagem: " + Cost +
                   "\nData de Registro da Passagem: " + DtRegistration +
                   "\nOrigem da Passagem: " + Origin.ToString() +
                   "\nDestino da Passagem: " + Destination.ToString();
        }
        #endregion
    }
}
