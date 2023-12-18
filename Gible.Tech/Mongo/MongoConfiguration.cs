namespace Gible.Tech.Mongo
{
    public class MongoConfiguration
    {
        public string MongoConnectionString { get; private set; }
        public string DatabaseName { get; private set; }

        public MongoConfiguration()
        {
            MongoConnectionString = "mongodb://127.0.0.1:27017";
            DatabaseName = "Gible";
#if DEBUG
            DatabaseName += "DEBUG";
#endif
        }
    }
}
