using System.IO;
using Microsoft.Extensions.Configuration;

namespace ExchngeLoaderConsole
{
    public  class Settings
    {
        

        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    IConfigurationRoot configuration = builder.Build();

                    _instance = new Settings() {ConnectionString = configuration.GetConnectionString("Storage")};
                }

                return _instance;
            }
        }

        public string ConnectionString { get; private set; }
    }
}