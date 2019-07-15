using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CoreLibrary.BusinessEntities;
using CoreLibrary.Exception;

namespace CnbApiClient.Parsers
{
    public class DailyDataParser
    {
        public static List<ExchangeRate> Parse(string[] rawData)
        {
            var res = new List<ExchangeRate>();

            if (rawData == null
                || rawData.Length < 3)
                return res;

            var dateStr = (rawData[0].Split("#").FirstOrDefault()
                          ??string.Empty)
                            .Trim();

            if (!DateTime.TryParseExact(dateStr
                , "dd MMM yyyy"
                , CultureInfo.InvariantCulture
                , DateTimeStyles.None
                , out DateTime date))
            {
                throw new NotValidInputDataException($"Not valid date '{rawData[0]}'");
            }

            

            foreach (var rowString in rawData.Skip(2))
            {
                var fields = rowString.Split("|").ToArray();

                if (!int.TryParse(fields[2], out int amount))
                {
                    Console.WriteLine($"Couldn't parse currency amount in row {rowString}.");
                    continue;
                }

                if (!Enum.TryParse(fields[3], true, out Currencies currencyCode))
                {
                    Console.WriteLine($"Unknown currency code in row {rowString}");
                    continue;
                }

                if (!Decimal.TryParse(fields[4], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal rate))
                {
                    Console.WriteLine($"Not valid exchange rate in row {rowString}");
                    continue;
                }

                res.Add(new ExchangeRate()
                {
                    Amount = amount,
                    Rate = rate,
                    Currency = currencyCode,
                    Date = date
                });
            }

            return res;
        }

    }
}