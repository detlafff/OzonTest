using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CoreLibrary.BusinessEntities;
using CoreLibrary.Exception;

namespace CnbApiClient.Parsers
{
    public static class HistoricalDataParser
    {
        public static List<ExchangeRate> Parse(string[] rawData)
        {
            var res = new List<ExchangeRate>();

            if (rawData == null
                || rawData.Length < 2)
                return res;


            var tableHeaders = rawData[0].Split('|');
            if (!tableHeaders[0].Equals("Date"))
                throw new NotValidInputDataException("Word 'Date' is missing.");

            var listOfCodes = tableHeaders
                .Skip(1)
                .Select(x =>new CodePerAmount(x))
                .ToArray();

            for (int i = 1; i < rawData.Length; i++)
            {
                var ratesWithDate = rawData[i].Split("|").Select(x => x.Trim()).ToArray();
                if (ratesWithDate.Length - 1 != listOfCodes.Length)
                {
                    Console.WriteLine($"Not valid row '{rawData[i]}'");
                    continue;
                }

                if (!DateTime.TryParseExact(ratesWithDate[0]
                    , "dd'.'MM'.'yyyy"
                    , CultureInfo.InvariantCulture
                    , DateTimeStyles.None
                    , out DateTime date))
                {
                    Console.WriteLine($"Not valid date '{rawData[i]}'");
                    continue;
                }

                var rates = ratesWithDate.Skip(1).ToArray();

                for (int y = 0; y < rates.Length; y++)
                {
                    if (!Decimal.TryParse(rates[y], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal rate))
                    {
                        Console.WriteLine($"Not valid exchange rate '{rates[y]}' in row {rawData[i]}");
                        continue;
                    }

                    res.Add(new ExchangeRate()
                    {
                        Date = date,
                        Amount = listOfCodes[y].Amount,
                        Currency = listOfCodes[y].Code,
                        Rate = rate
                    });
                }
            }

            return res;
        }

        struct CodePerAmount
        {
            public CodePerAmount(string rawString) : this()
            {
                var list = rawString.Split(" ");

                if (!Int32.TryParse(list[0], out int amount))
                    throw new NotValidInputDataException($"Couldn't parse currency amount.");
                Amount = amount;

                if (!Enum.TryParse(list[1], true, out Currencies code))
                    throw new NotValidInputDataException($"Unknown currency code {list[0]}");
                Code = code;
            }

            public Currencies Code { get;  }
            public int Amount { get;  }
        }
    }
}