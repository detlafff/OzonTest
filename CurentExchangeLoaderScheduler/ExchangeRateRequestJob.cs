using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CnbApiClient;
using CnbApiClient.Parsers;
using CoreLibrary.BusinessEntities;
using DataBaseModel;
using Quartz;

namespace CurrentExchangeLoaderScheduler
{
    public class ExchangeRateRequestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

                var rawData = await GetRawRateData();
                if (rawData.Length < 1)
                    return;

                var rates = DailyDataParser.Parse(rawData);
                if (rates.Count < 1)
                    return;


                SaveRateDataToDb(rates);
            }
            catch (JobExecutionException)
            {
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void SaveRateDataToDb(List<ExchangeRate> rates)
        {
            using (var db = new AppDbContext(Settings.Instance.ConnectionString))
            {
                var existRates = db.ExchangeRates.Where(x => x.Date == rates.FirstOrDefault().Date);

                foreach (var newExchangeRate in rates)
                {
                    var existRate = existRates.FirstOrDefault(x => x.Currency == newExchangeRate.Currency);

                    if (existRate == null)
                    {
                        db.ExchangeRates.Add(newExchangeRate);
                    }
                    else
                    {
                        existRate.Amount = newExchangeRate.Amount;
                        existRate.Rate = newExchangeRate.Rate;
                        existRate.UpdateDate = DateTime.Now;
                    }
                }

                db.SaveChanges();

                Console.WriteLine($"Add/update to db {rates.Count} current currency exchange rate.");
            }
        }

        private static async Task<string[]> GetRawRateData()
        {
            var service = new CnbApiClient.CnbService(
                new Uri("https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing"));

            var rawData = await service.GetRawDailyCurrencyByDate(DateTime.Now);
            return rawData;
        }
    }
}