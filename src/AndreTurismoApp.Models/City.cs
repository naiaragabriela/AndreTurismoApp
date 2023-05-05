namespace AndreTurismoApp.Models
{
    public class City
    {
        #region Constant

        public static readonly string INSERT = "INSERT INTO CITY (Name, DtRegistration) VALUES (@Name,@DtRegistration);" +
            "select cast(scope_identity() as int)";

        public static readonly string SELECT = "SELECT Id, Name, DtRegistration FROM CITY";

        public static readonly string UPDATE = "UPDATE CITY SET Name = @Name where Id = @id";

        public static readonly string DELETE = "DELETE FROM CITY WHERE Id = @Id";
        #endregion



        #region Properties

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DtRegistration { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return "Id da cidade: " + Id +
                "\nNome da cidade: " + Name +
                "\nData do registro da Cidade: " + DtRegistration;
        }
        #endregion

    }
}