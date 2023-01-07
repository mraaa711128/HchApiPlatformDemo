namespace HchApiPlatform.Options
{
    public class DatabaseOptions
    {
        public const string Database = "Database";
        public const string UnimaxHO = "UnimaxHO";
        public const string UnimaxHI = "UnimaxHI";
        public const string PlatformDB = "PlatformDB";

        public Unimax Unimax { get; set; }
        public Platform Platform { get; set; }
    }

    public class Unimax
    {

        public string Ho { get; set; }
        public string Hi { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
    }

    public class Platform
    {

        public string Connection { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }

    }
}