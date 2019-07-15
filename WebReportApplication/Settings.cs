using System;
using System.Collections.Generic;
using System.IO;
using CoreLibrary.BusinessEntities;
using Microsoft.Extensions.Configuration;

namespace WebReportApplication
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

                    var list = new List<Currencies>();
                    configuration.GetSection("currenciesForReport").Bind(list);

                    _instance = new Settings()
                    {
                        ConnectionString = configuration.GetConnectionString("Storage"),
                        CurrenciesForReport = list.ToArray()
                    };

                    Console.WriteLine(); 

                }

                return _instance;
            }
        }

        public string ConnectionString { get; private set; }
        public Currencies[] CurrenciesForReport { get; private set; }
    }
}