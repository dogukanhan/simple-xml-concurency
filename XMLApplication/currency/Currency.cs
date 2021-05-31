namespace XMLApplication
{
    public class Currency : ICurrency
    {
        public int CrossOrder { get; }

        public string Code { get; }

        public string CurrencyCode { get;}

        public int Unit { get; }

        public string Name { get; } 

        public string CurrencyName { get; }

        public decimal ForexBuying { get; }

        public decimal ForexSelling { get; }

        public decimal BanknoteBuying { get; }

        public decimal BanknoteSelling { get; }

        public decimal CrossRateUSD { get; }

        public decimal CrossRateOther { get; }

        public Currency(int crossOrder, string code, string currencyCode, int unit, string name, string currencyName, decimal forexBuying, decimal forexSelling, decimal banknoteBuying, decimal banknoteSelling, decimal crossRateUSD, decimal crossRateOther)
        {
            CrossOrder = crossOrder;
            Code = code;
            CurrencyCode = currencyCode;
            Unit = unit;
            Name = name;
            CurrencyName = currencyName;
            ForexBuying = forexBuying;
            ForexSelling = forexSelling;
            BanknoteBuying = banknoteBuying;
            BanknoteSelling = banknoteSelling;
            CrossRateUSD = crossRateUSD;
            CrossRateOther = crossRateOther;
        }
    }
}
