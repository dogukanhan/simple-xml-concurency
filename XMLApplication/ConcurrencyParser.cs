using System;
using System.Collections.Generic;
using System.Xml;

namespace XMLApplication
{
    public static class ConcurrencyParser
    {
        internal static SortedDictionary<string, ICurrency> Parse(string fileName)
        {
            SortedDictionary<string,ICurrency> sortedDictionary = new SortedDictionary<string, ICurrency>();

            XmlDocument xd = new XmlDocument();
            xd.Load(fileName);

            XmlNodeList nodelist = xd.SelectSingleNode("/Tarih_Date").ChildNodes;
            foreach (XmlNode node in nodelist) 
            {

                string name, currencyName, code, currencyCode;
                int unit, crossOrder;
                decimal forexBuying, forexSelling, banknoteSelling, banknoteBuying, crossRateUSD, crossRateOther;


                code = node.Attributes["Kod"].Value;
                currencyCode = node.Attributes["CurrencyCode"].Value;

                unit = int.Parse(node.SelectSingleNode("Unit").InnerText);
                name = node.SelectSingleNode("Isim").InnerText;
                currencyName = node.SelectSingleNode("CurrencyName").InnerText;
                decimal.TryParse(node.SelectSingleNode("ForexBuying").InnerText, out forexBuying);
                decimal.TryParse(node.SelectSingleNode("ForexSelling").InnerText, out forexSelling);
                decimal.TryParse(node.SelectSingleNode("BanknoteSelling").InnerText, out banknoteSelling);
                decimal.TryParse(node.SelectSingleNode("BanknoteBuying").InnerText, out banknoteBuying);
                decimal.TryParse(node.SelectSingleNode("CrossRateUSD").InnerText, out crossRateUSD);
                decimal.TryParse(node.SelectSingleNode("CrossRateOther").InnerText, out crossRateOther);
                int.TryParse(node.Attributes["CrossOrder"].Value, out crossOrder);

                banknoteBuying /= 10_000;
                banknoteSelling /= 10_000;
                forexBuying /= 10_000;
                forexSelling /= 10_000;

                var currency = new Currency(crossOrder,code, currencyCode, unit
                    , name, currencyName, forexBuying, forexSelling, 
                    banknoteBuying, banknoteSelling,crossRateUSD,crossRateOther);

                sortedDictionary.Add(code, currency);
            }

            return sortedDictionary;
        }
    }
}
