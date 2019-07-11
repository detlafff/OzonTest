using System;
using System.Collections.Generic;
using RestSharp;

namespace CnbApiClient
{
    public class CnbService 
    {
        private IRestClient restClient;
        private static string YearExchangeSegmentUrl = "year.txt";

        public CnbService(Uri uri)
        {
            restClient = new RestClient(uri);
        }

        public string[] GetRawCurrecyByYear(int year)
        {
            var request = new RestRequest(YearExchangeSegmentUrl)
            {
                Method = Method.GET
            };

            request.AddParameter("year", year, ParameterType.QueryString);

            var response = restClient.Execute(request);

            return response.Content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
