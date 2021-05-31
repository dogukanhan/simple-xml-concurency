using System;
using System.Collections.Generic;


namespace XMLApplication
{
    class Program
    {
        private SortedDictionary<string, ICurrency> currencies;
        private CacheManager cacheManager;

        Program(){
            this.cacheManager = new CacheManager();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.MainMenu();
        }

        void MainMenu(){
            Console.WriteLine("Started");
            currencies = cacheManager.LoadData();
            Console.WriteLine("Please enter currency to process -h to help");
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
    
        void CurrencyOperation(bool buy){ 
            
            Console.Write("Source Currency: ");
            string source = Console.ReadLine();
            while (!currencies.ContainsKey(source)){
                Console.WriteLine("Wrong input Avaible Codes");
                ListCurrencies(false);
                Console.Write("Source Currency: ");
                source = Console.ReadLine();
            }

            Console.Write("Target Currency: ");
            string target = Console.ReadLine();
            while (!currencies.ContainsKey(target))
            {
                Console.WriteLine("Wrong input Avaible Codes");
                ListCurrencies(false);
                Console.Write("Source Currency: ");
                target = Console.ReadLine();
            }

            ICurrency sourceCurrency = currencies[source];
            ICurrency targetCurrency = currencies[target];

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

            decimal amount;
            Console.Write("Amount: ");
            string s = Console.ReadLine();
            while(!decimal.TryParse(s, out amount)){
                Console.WriteLine("Please enter a valid decimal");
                s = Console.ReadLine();
            }

            IPayment payment;
            if(result == "Banknote"){
                payment = new BanknotePayment();
            }else if(result == "Forex"){
                payment = new ForexPayment();
            }else{
                Console.WriteLine("Payment value error");
                return;
            }

            decimal total;
            if(buy){
                total = payment.Buy(currencies[source], currencies[target], amount);
            }else{
                total = payment.Sell(currencies[source], currencies[target], amount);
            }
            Console.WriteLine("Total= " + total);
        }
        void HelpMenu(){
            Console.WriteLine("-b : buy");
            Console.WriteLine("-h : to help");
            Console.WriteLine("-s : sell");
            Console.WriteLine("-r : reflesh cache");
            Console.WriteLine("-l : list currencies");
            Console.WriteLine("-ld : list currencies with detailed values over TR");
        }
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
