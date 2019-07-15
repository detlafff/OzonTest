using System.Collections.Generic;

namespace WebReportApplication.Controllers
{
    public class ResponseDto
    {
        public int year { get; set; }
        public string month { get; set; }
        public List<WeekPeriod> WeekPeriods { get; set; }
    }

    public class WeekPeriod
    {
        public string Days { get; set; }
        public CurrencyItem[] CurrencyItems { get; set; }

        
    }

    public class CurrencyItem
    {
        public string Code { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Media { get; set; }

        public override string ToString()
        {
            return $"{Code} - max: {Max}, min: {Min}, median: {Media};";
        }
    }
}