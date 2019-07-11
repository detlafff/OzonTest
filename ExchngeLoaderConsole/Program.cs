using System;

namespace ExchngeLoaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var servise = new CnbApiClient.CnbService(
                new Uri("https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing"));
            var ans = servise.GetRawCurrecyByYear(2018);
            Console.WriteLine(ans);
            Console.ReadLine();
        }
    }
}
