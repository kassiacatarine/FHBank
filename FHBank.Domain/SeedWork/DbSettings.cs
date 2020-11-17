namespace FHBank.Domain.SeedWork
{
    public class DbSettings
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    return $@"mongodb://{Host}:{Port}";

                if (Port != 0)
                    return $@"mongodb://{User}:{Password}@{Host}:{Port}";

                return $@"mongodb+srv://{User}:{Password}@{Host}";
            }
        }
    }
}
