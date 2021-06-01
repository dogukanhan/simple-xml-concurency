namespace XMLApplication
{
    /// <summary>
    /// All information can be retrevied from xml data.
    /// </summary>
    public class Currency : ICurrency
    {
        /// <summary>
        /// An unknown value from xml
        /// </summary>
        public int CrossOrder { get; }

        /// <summary>
        /// Three ASCII char used for defining currency.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Same as Code value in the currency
        /// <inheritdoc cref="Code"/> 
        /// </summary>
        public string CurrencyCode { get;}

        /// <summary>
        /// An unknown value from xml.
        /// </summary>
        public int Unit { get; }

        /// <inheritdoc/> 
        public string Name { get; } 

        /// <summary>
        /// English name of the currency.
        /// </summary>
        public string CurrencyName { get; }

        /// <inheritdoc/> 
        public decimal ForexBuying { get; }

        /// <inheritdoc/> 
        public decimal ForexSelling { get; }

        /// <inheritdoc/> 
        public decimal BanknoteBuying { get; }

        /// <inheritdoc/> 
        public decimal BanknoteSelling { get; }

        /// An unknown value from xml
        public decimal CrossRateUSD { get; }

        /// An unknown value from xml
        public decimal CrossRateOther { get; }

        /// <summary>
        /// A required way to creating Currency object.
        /// </summary>
        /// <param name="crossOrder"> <see cref="CrossOrder"/> </param>
        /// <param name="code">This is code</param>
        /// <param name="currencyCode"></param>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <param name="currencyName"></param>
        /// <param name="forexBuying"></param>
        /// <param name="forexSelling"></param>
        /// <param name="banknoteBuying"></param>
        /// <param name="banknoteSelling"></param>
        /// <param name="crossRateUSD"></param>
        /// <param name="crossRateOther"></param>
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
