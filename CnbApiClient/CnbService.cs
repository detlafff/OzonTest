using System;
using System.Net;
using System.Threading.Tasks;
using CoreLibrary.Exception;
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

        public async  Task<string[]> GetRawCurrencyByYear(int year)
        {
            var request = new RestRequest(YearExchangeSegmentUrl)
            {
                Method = Method.GET
            };

            request.AddParameter("year", year, ParameterType.QueryString);

            var response = await _restClient.ExecuteGetTaskAsync<IRestResponse>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new CnbServiceRequestException($"Error on get yearly data {year} from CnbService.\nHttpStatus {response.StatusCode} Content {response.Content}");

            return response.Content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public async Task<string[]> GetRawDailyCurrencyByDate(DateTime date)
        {
            var request = new RestRequest(DailyExchangeSegmentUrl)
            {
                Method =  Method.GET
            };

            request.AddParameter("date", date.ToString("dd.MM.yyyy"), ParameterType.QueryString);

            var response = await _restClient.ExecuteGetTaskAsync<IRestResponse>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new CnbServiceRequestException($"Error on get daily data {date:d} from CnbService.\nHttpStatus {response.StatusCode} Content {response.Content}");

            return response.Content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        
    }
}
