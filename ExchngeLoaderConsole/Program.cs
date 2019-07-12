using System;
using System.Linq;

namespace ExchngeLoaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var servise = new CnbApiClient.CnbService(
                new Uri("https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing"));
            var ans = servise.GetRawCurrencyByYear(2018);
            ans.ToList().ForEach(Console.WriteLine);
            Console.ReadLine();
        }

        //private static void AutoMapperConfigure()
        //{
        //    AutoMapper.Mapper.Initialize(cfg =>
        //    {
        //        cfg.AddProfile<WebApiProfile>();
        //    });
        //}

        //public class WebApiProfile : Profile
        //{
        //    public WebApiProfile()
        //    {
        // 
        //    }
        //}
    }
}
