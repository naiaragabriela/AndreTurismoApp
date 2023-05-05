namespace AndreTurismoApp.Models
{
    public class Hotel
    {
        #region Constant
        public static readonly string INSERT = "INSERT INTO HOTEL(Name, IdAddress, Cost, DtRegistration) " +
                       "VALUES (@Name, @IdAddress, @Cost, @DtRegistration); select cast(scope_identity() as int)";




        public static readonly string SELECT = "SELECT [Hotel].[Id] AS Id, [Hotel].[Name], [Hotel].[Cost],[Hotel].[DtRegistration]," +
            "[AddressHotel].[Id] AS SplitAddress, [AddressHotel].[Id] AS Id, [Street],[Number],[Neighborhood],[PostalCode],[Complement]," +
            "[AddressHotel].[DtRegistration],[AddressCity].[Id] AS SplitCity, [AddressCity].[Id] AS Id, [AddressCity].[Name], [AddressCity].[DtRegistration] " +
            "FROM [Hotel] JOIN [Address] AddressHotel ON IdAddress = AddressHotel.Id " +
            "JOIN [City] AddressCity ON AddressHotel.IdCity= AddressCity.Id";

        public static readonly string UPDATE = "UPDATE Hotel SET " +
                                               "Name = @Name" +
                                               "Address = @IdAdress" +
                                               "Cost = @Cost" +
                                              " WHERE Id = @id";


        public static readonly string DELETE = "DELETE FROM Hotel WHERE Id =@id";

        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public Address? Address { get; set; }
        public int AddressId { get; set; }
        public decimal Cost { get; set; }
        public DateTime DtRegistration { get; set; }

        #endregion

        #region Methods
        public override string ToString()
        {
            return "Id do Hotel: " + Id +
                   "Nome do Hotel:" + Name +
                   "\nCusto do Hotel: " + Cost +
                   "\nData de Registro do Hotel: " + DtRegistration +
                   "\nEndereço: " + Address.ToString();
        }
        #endregion

    }
}
