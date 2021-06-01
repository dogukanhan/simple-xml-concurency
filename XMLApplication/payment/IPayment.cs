namespace XMLApplication
{
    /// <summary>
    /// Concreate Payment interface for abstracting buy and sell methods.
    /// </summary>
    interface IPayment
    {
        /// <summary>
        /// Calculate how much costs when buying target from source
        /// </summary>
        /// <param name="source">Source Currency for buying</param>
        /// <param name="target">Target Currency for buying</param>
        /// <param name="amount">How much will spend from source</param>
        /// <returns>How much can buy target Currency</returns>
        decimal Buy(ICurrency source, ICurrency target, decimal amount);

        /// <summary>
        /// Calculate how much costs when selling target from source
        /// </summary>
        /// <param name="source">Source Currency for selling</param>
        /// <param name="target">Target Currency for selling</param>
        /// <param name="amount">How much source is selled</param>
        /// <returns>How much target currency amount will be retreived</returns>
        decimal Sell(ICurrency source, ICurrency target, decimal amount);
    }
}
