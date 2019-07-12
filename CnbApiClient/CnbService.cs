using System;
using RestSharp;

namespace CnbApiClient
{
    public class CnbService 
    {
        private readonly IRestClient _restClient;
        private const string YearExchangeSegmentUrl = "year.txt";
        private const string DailyExchangeSegmentUrl = "daily.txt";

        public CnbService(Uri uri)
        {
            _restClient = new RestClient(uri);
        }

        public string[] GetRawCurrencyByYear(int year)
        {
            var request = new RestRequest(YearExchangeSegmentUrl)
            {
                Method = Method.GET
            };

            request.AddParameter("year", year, ParameterType.QueryString);

            var response = _restClient.Execute(request);

            return response.Content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] GetRawDailyCurrencyByDate(DateTime date)
        {
            var request = new RestRequest(DailyExchangeSegmentUrl)
            {
                Method =  Method.GET
            };

            request.AddParameter("date", date.ToString("dd.MM.yyyy"), ParameterType.QueryString);

            var response = _restClient.Execute(request);

            return response.Content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        
    }
}
