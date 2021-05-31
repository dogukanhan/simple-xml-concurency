namespace XMLApplication
{
    interface ICurrency 
    {
        string Name { get; }

        decimal ForexBuying { get; }

        decimal ForexSelling { get; }

        decimal BanknoteBuying { get; }

        decimal BanknoteSelling { get; }
    }
}
