using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CoreLibrary.BusinessEntities;
using CoreLibrary.Helpers;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;


namespace WebReportApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("JSON")]
        public ActionResult<ResponseDto> GetJson(int year, int month)
        {
            var res = GetReportData(year, month);

            return res;
        }

        [HttpGet("txt")]
        public ActionResult<string> GetTxt(int year, int month)
        {
            var res = GetReportData(year, month);


            var listOfString = new List<string>()
            {
                $"Year: {res.year}, month: {res.month}",
                "Week periods:"
            };

            listOfString.AddRange(res.WeekPeriods
                .Select(x => $"{x.Days}: { string.Join("", x.CurrencyItems.Select(c => c.ToString()).ToArray())}"));

            return string.Join("\n",listOfString);
        }



        private ResponseDto GetReportData(int year, int month)
        {
            var res = new ResponseDto()
            {
                year = year,
                month = new DateTime(year, month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("us")),
                WeekPeriods = new List<WeekPeriod>()
            };

            using (var db = new AppDbContext(@"Server=HOME-ПК\SQLEXPRESS;Database=ozonTestDb;Trusted_Connection=True;"))
            {
                var currencyCodes = Settings.Instance.CurrenciesForReport;

                var entries = db.ExchangeRates
                    .Where(x => x.Date.Year == year
                                && x.Date.Month == month
                                && currencyCodes.Contains(x.Currency))
                    .ToList();

                var weekGroups = entries.Select(x =>
                        new
                        {
                            week = x.Date.WeekOfMonth(),
                            currency = x.Currency,
                            rate = x.Rate / x.Amount
                        })
                    .GroupBy(x => x.week.Number)
                    .OrderBy(x => x.Key);

                foreach (var g in weekGroups)
                {
                    var currencyItems = g
                        .GroupBy(x => x.currency)
                        .Select(x => new CurrencyItem()
                        {
                            Code = x.Key.ToString(),
                            Max = x.Max(c => c.rate),
                            Min = x.Min(c => c.rate),
                            Media = x.Average(c => c.rate)
                        }).ToArray();

                    res.WeekPeriods.Add(new WeekPeriod()
                    {
                        Days = $"{g.FirstOrDefault()?.week.FirstDay}..{g.FirstOrDefault()?.week.LastDay}",
                        CurrencyItems = currencyItems
                    });
                }
            }

            return res;
        }
    }

    
}