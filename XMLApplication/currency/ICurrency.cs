namespace XMLApplication
{
    /// <summary>
    /// Concreate currency for currency data.
    /// </summary>
    interface ICurrency 
    {
        /// <summary>
        /// Turkish name of the currency.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Forex Buying value for currency.
        /// </summary>
        decimal ForexBuying { get; }

        /// <summary>
        /// Forex Selling Value for currency.
        /// </summary>
        decimal ForexSelling { get; }

        /// <summary>
        /// Banknote buying for currency.
        /// </summary>
        decimal BanknoteBuying { get; }

        /// <summary>
        /// Banknote selling for currency/
        /// </summary>
        decimal BanknoteSelling { get; }
    }
}
