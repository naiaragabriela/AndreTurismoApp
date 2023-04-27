namespace AndreTurismoApp.Models
{
    public class City
    {
        #region Constant

        public static readonly string INSERT = "INSERT INTO CITY (NameCity, DtRegistration) VALUES (@NameCity,@DtRegistration);" +
            "select cast(scope_identity() as int)";

        public static readonly string SELECT = "SELECT Id, NameCity, DtRegistration FROM CITY";

        public static readonly string UPDATE = "UPDATE CITY SET NameCity = @NameCity where Id = @id";

        public static readonly string DELETE = "DELETE FROM CITY WHERE Id = @Id";
        #endregion



        #region Properties

        public int Id { get; set; }
        public string NameCity { get; set; }
        public DateTime DtRegistration { get; set; }
        #endregion

    }
}