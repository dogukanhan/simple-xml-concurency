namespace XMLApplication
{
    class ForexPayment : IPayment
    {
        public decimal Buy(ICurrency source, ICurrency target, decimal amount)
        {
            return (source.ForexBuying / target.ForexSelling) * amount;
        }

        public decimal Sell(ICurrency source, ICurrency target, decimal amount)
        {
            return (target.ForexSelling / source.ForexBuying) * amount;
        }
    }
}
