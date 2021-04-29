namespace Exchange.Models
{
    public class ConnectionString
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class StaticConnectionString 
    {
        public static string ConnectionString { get; set; }
    }
}
