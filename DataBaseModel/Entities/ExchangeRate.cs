using System;
using System.ComponentModel;

namespace DataBaseModel.Entities
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Currencies Currency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }

    public enum Currencies
    {
        [Description("Australia dollar")]
        Aud = 1,
        [Description("Brazil real")]
        Brl = 2,
        [Description("Bulgaria  lev")]
        Bgn = 3,
        [Description("Canada  dollar")]
        Cad = 4,
        [Description("China  renminbi")]
        Cny = 5,
        [Description("Croatia  kuna")]
        Hrk = 6,
        [Description("Denmark  krone")]
        Dkk = 7,
        [Description("EMU  euro")]
        Eur = 8,
        [Description("Hongkong  dollar")]
        Hkd = 9,
        [Description("Hungary  forint")]
        Huf = 10,
        [Description("Iceland  krona")]
        Isk = 11,
        [Description("IMF  SDR")]
        Xdr = 12,
        [Description("India  rupee")]
        Inr = 13,
        [Description("Indonesia  rupiah")]
        Idr = 14,
        [Description("Israel  shekel")]
        Ils = 15,
        [Description("Japan  yen")]
        Jpy = 16,
        [Description("Malaysia  ringgit")]
        Myr = 17,
        [Description("Mexico  peso")]
        Mxn = 18,
        [Description("New Zealand dollar")]
        Nzd = 19,
        [Description("Norway  krone")]
        Nok = 20 ,
        [Description("Philippines  peso")]
        Php = 21,
        [Description("Poland  zloty")]
        Pln = 22,
        [Description("Romania  new leu")]
        Ron = 23,
        [Description("Russia  rouble")]
        Rub = 24,
        [Description("Singapore  dollar")]
        Sgd = 25,
        [Description("South Africa rand")]
        Zar = 26,
        [Description("South Korea won")]
        Krw = 27,
        [Description("Sweden  krona")]
        Sek = 28,
        [Description("Switzerland  franc")]
        Chf = 29,
        [Description("Thailand  baht")]
        Thb = 30,
        [Description("Turkey  lira")]
        Try = 31,
        [Description("United Kingdom pound")]
        Gbp = 32,
        [Description("USA  dollar")]
        Usd = 33
    }
}