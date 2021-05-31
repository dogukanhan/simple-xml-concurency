namespace XMLApplication
{
    interface IPayment
    {
        decimal Buy(ICurrency source, ICurrency target, decimal amount);
        decimal Sell(ICurrency source, ICurrency target, decimal amount);
    }
}
