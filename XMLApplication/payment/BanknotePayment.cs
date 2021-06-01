namespace XMLApplication
{
    /// <summary>
    /// Banknote payment implementations.
    /// </summary>
    class BanknotePayment : IPayment
    {
        /// <inheritdoc />
        public decimal Buy(ICurrency source, ICurrency target, decimal amount)
        {
            return (source.BanknoteBuying / target.BanknoteSelling) * amount;
        }

        /// <inheritdoc />
        public decimal Sell(ICurrency source, ICurrency target, decimal amount)
        {
            return (target.BanknoteSelling / source.BanknoteBuying) * amount;
        }
    }
}
