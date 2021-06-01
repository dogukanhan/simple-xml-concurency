namespace XMLApplication
{
    /// <summary>
    /// Forex Only Buying and Selling implementations. Uses forex values for curencies' values.
    /// </summary>
    class ForexPayment : IPayment
    {
        /// <inheritdoc />
        public decimal Buy(ICurrency source, ICurrency target, decimal amount)
        {
            return (source.ForexBuying / target.ForexSelling) * amount;
        }

        /// <inheritdoc />
        public decimal Sell(ICurrency source, ICurrency target, decimal amount)
        {
            return (target.ForexSelling / source.ForexBuying) * amount;
        }
    }
}
