namespace database.DAL
{
    public class Dal_Helper
    {
        public static string Constr = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build()
        .GetConnectionString("Mystring");
    }
}
