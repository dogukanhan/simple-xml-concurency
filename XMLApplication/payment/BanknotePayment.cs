namespace XMLApplication
{
    class BanknotePayment : IPayment
    {
        public decimal Buy(ICurrency source, ICurrency target, decimal amount)
        {
            return (source.BanknoteBuying / target.BanknoteSelling) * amount;
        }

        public decimal Sell(ICurrency source, ICurrency target, decimal amount)
        {
            return (target.BanknoteSelling / source.BanknoteBuying) * amount;
        }
    }
}
