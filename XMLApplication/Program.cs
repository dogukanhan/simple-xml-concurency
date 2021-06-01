using System;
using System.Collections.Generic;


namespace XMLApplication
{
    // Main program for user interactions.
    class Program
    {
        // Concurecy data's stored in this variable.
        private SortedDictionary<string, ICurrency> currencies;

        // Cache manager provides an interface for accesing the data
        private CacheManager cacheManager;

        Program(){
            this.cacheManager = new CacheManager();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.MainMenu();
        }

        // MainMenu controls users interactions.
        void MainMenu(){
            Console.WriteLine("Started");

            currencies = cacheManager.LoadData(); // Load data from cache.

            Console.WriteLine("Please enter currency to process -h to help");

            // Take user inputs until -1
            while (true){
                Console.Write("Input: ");
                string line = Console.ReadLine();
               
                if(line == "-1")
                    break;
                else if(line == "-h"){
                    HelpMenu();
                }
                else if(line == "-l"){
                    ListCurrencies(false);
                }else if( line == "-ld"){
                    ListCurrencies(true);
                }else if(line == "-b"){
                    CurrencyOperation(true);
                }else if (line == "-s")
                {
                    CurrencyOperation(false);
                }else if (line == "-r"){
                    currencies = cacheManager.RefleshCache();
                }
                else{
                    Console.WriteLine("Unknown command");
                }
            }
           
            Console.WriteLine("Finished");
        }
    
        // This method is used for buying or selling a currency.
        void CurrencyOperation(bool buy){ 
            
            // Take source currency.
            Console.Write("Source Currency: ");
            string source = Console.ReadLine();
            while (!currencies.ContainsKey(source)){
                Console.WriteLine("Wrong input Avaible Codes");
                ListCurrencies(false);
                Console.Write("Source Currency: ");
                source = Console.ReadLine();
            }

            // Take target currency.
            Console.Write("Target Currency: ");
            string target = Console.ReadLine();
            while (!currencies.ContainsKey(target))
            {
                Console.WriteLine("Wrong input Avaible Codes");
                ListCurrencies(false);
                Console.Write("Source Currency: ");
                target = Console.ReadLine();
            }

            // Retrieve ICurrencies from user inputs.
            ICurrency sourceCurrency = currencies[source];
            ICurrency targetCurrency = currencies[target];

            // Check if the currencies support banknote and forex selling | buying.
            bool banknote = true, forex= true;

            if(buy){
                if(sourceCurrency.BanknoteSelling == 0 || targetCurrency.BanknoteBuying == 0){
                    Console.WriteLine("Banknote selling is not supported for this currency");
                    banknote = false;
                }

                if (sourceCurrency.ForexSelling == 0 || targetCurrency.ForexBuying == 0)
                {
                    Console.WriteLine("Forex selling is not supported for this currency");
                    forex= false;
                }
            }else{
                if (sourceCurrency.BanknoteBuying == 0 || targetCurrency.BanknoteSelling == 0)
                {
                    Console.WriteLine("Banknote selling is not supported for this currency");
                    banknote = false;
                }

                if (sourceCurrency.ForexBuying == 0 || targetCurrency.ForexSelling == 0)
                {
                    Console.WriteLine("Forex selling is not supported for this currency");
                    forex = false;
                }
            }

            // Convert boolean variables to string and write message if any not supported.
            string result;
            if (!forex && !banknote){
                Console.WriteLine("Sorry, there is no avaible selling option for this currency");
                return;
            }else if(forex && !banknote){
                result = "Forex";
            }else if(!forex && banknote){
                result = "Banknote";
            }else{
                Console.Write("Banknote or Forex?: ");
                result = Console.ReadLine();
                while (result != "Banknote" || result != "Forex")
                {
                    Console.WriteLine("Wrong input avaible options Banknote or Forex");
                    result = Console.ReadLine();
                }
            }

            // Take amount for convertion.
            decimal amount;
            Console.Write("Amount: ");
            string s = Console.ReadLine();
            while(!decimal.TryParse(s, out amount)){
                Console.WriteLine("Please enter a valid decimal");
                s = Console.ReadLine();
            }

            // Convert string variable to IPayment.
            IPayment payment;
            if(result == "Banknote"){
                payment = new BanknotePayment();
            }else if(result == "Forex"){
                payment = new ForexPayment();
            }else{
                Console.WriteLine("Payment value error");
                return;
            }

            // Calculate total output for given variables.
            decimal total;
            if(buy){
                total = payment.Buy(currencies[source], currencies[target], amount);
            }else{
                total = payment.Sell(currencies[source], currencies[target], amount);
            }
            Console.WriteLine("Total= " + total);
        }

        // Print help menu to user.
        void HelpMenu(){
            Console.WriteLine("-b : buy");
            Console.WriteLine("-h : to help");
            Console.WriteLine("-s : sell");
            Console.WriteLine("-r : reflesh cache");
            Console.WriteLine("-l : list currencies");
            Console.WriteLine("-ld : list currencies with detailed values over TR");
            Console.WriteLine("-1 : to exit program.");

        }

        // Print Currencies and details if detailed is true.
        void ListCurrencies(bool detailed){
            Console.WriteLine("Code | TR | Name");
            foreach (KeyValuePair<string, ICurrency> entry in currencies)
            {
                string line;
                if(detailed){
                     if(entry.Value.BanknoteBuying != 0){
                        line = entry.Key + "  : " + (entry.Value.BanknoteBuying) + " : " + entry.Value.Name;
                     }else{
                        line = entry.Key + "  : Kapalı : " + entry.Value.Name;
                    }
                }
                else{
                    line = entry.Key + "  : " + entry.Value.Name;
                }
                Console.WriteLine(line);
            }
        }
    }
}
