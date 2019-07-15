using System;

namespace CoreLibrary.BusinessEntities
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Currencies Currency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}