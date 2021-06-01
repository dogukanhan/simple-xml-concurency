namespace XMLApplication
{
    // Concreate Payment interface for abstracting buy and sell methods.
    interface IPayment
    {
        decimal Buy(ICurrency source, ICurrency target, decimal amount);
        decimal Sell(ICurrency source, ICurrency target, decimal amount);
    }
}
