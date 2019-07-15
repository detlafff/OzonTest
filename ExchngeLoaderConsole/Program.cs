using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CnbApiClient.Parsers;
using DataBaseModel;
using Microsoft.Extensions.Configuration;

namespace ExchngeLoaderConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var year = GetYearForLoadingData();

                var rawData = await GetRawExchangeRatesFromCnbService(year);

                SaveExchangeRatesToDb(year, rawData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        private static void SaveExchangeRatesToDb(int year, string[] rawData)
        {
            using (var db = new AppDbContext(Settings.Instance.ConnectionString))
            {
                db.ExchangeRates.RemoveRange(db.ExchangeRates.Where(x => x.Date.Year == year));

                var list = HistoricalDataParser.Parse(rawData);

                db.ExchangeRates.AddRange(list);

                db.SaveChanges();

                Console.WriteLine($"Add to db {list.Count} currency exchange rate per {year} year.");
            }
        }

        private static  async  Task<string[]> GetRawExchangeRatesFromCnbService(int year)
        {
            var service = new CnbApiClient.CnbService(
                new Uri("https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing"));
            return await service.GetRawCurrencyByYear(year);
        }

        private static int GetYearForLoadingData()
        {
            Console.WriteLine("Input year for loading");
            var yearStr = Console.ReadLine();

            if (!int.TryParse(yearStr, out int year))
            {
                Console.WriteLine("Not valid year data");
                Console.ReadLine();
                return year;
            }

            return year;
        }
    }
}
