namespace XMLApplication
{
    // Only this data is required for running appplication. Any implementation is accepted.
    interface ICurrency 
    {
        string Name { get; }

        decimal ForexBuying { get; }

        decimal ForexSelling { get; }

        decimal BanknoteBuying { get; }

        decimal BanknoteSelling { get; }
    }
}
