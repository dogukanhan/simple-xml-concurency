using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace XMLApplication
{

    /// <summary>
    /// This class converts the given xml to data strutucture.
    /// </summary>
    public static class ConcurrencyParser
    {
        /// <summary>
        /// Convert given xml to currency data structure.
        /// </summary>
        /// <param name="filePath">XML data's filePath</param>
        /// <returns>SortedDictionary where key is the code of the currency, value is the ICurrency parsed from xml</returns>
        internal static SortedDictionary<string, ICurrency> Parse(string filePath)
        {
            SortedDictionary<string,ICurrency> sortedDictionary = new SortedDictionary<string, ICurrency>();

            XmlDocument xd = new XmlDocument();
            xd.Load(filePath);

            XmlNodeList nodelist = xd.SelectSingleNode("/Tarih_Date").ChildNodes;
            foreach (XmlNode node in nodelist) 
            {
                // Culturel info for parsing tr-TR
                CultureInfo cultureInfo = new CultureInfo("en-US");
                
                string name, currencyName, code, currencyCode;
                int unit, crossOrder;
                decimal forexBuying, forexSelling, banknoteSelling, banknoteBuying, crossRateUSD, crossRateOther;


                code = node.Attributes["Kod"].Value;
                currencyCode = node.Attributes["CurrencyCode"].Value;

                unit = int.Parse(node.SelectSingleNode("Unit").InnerText, NumberStyles.Any, cultureInfo);
                name = node.SelectSingleNode("Isim").InnerText;
                currencyName = node.SelectSingleNode("CurrencyName").InnerText;
                decimal.TryParse(node.SelectSingleNode("ForexBuying").InnerText, NumberStyles.Any,  cultureInfo,out forexBuying);
                decimal.TryParse(node.SelectSingleNode("ForexSelling").InnerText, NumberStyles.Any, cultureInfo, out forexSelling);
                decimal.TryParse(node.SelectSingleNode("BanknoteSelling").InnerText, NumberStyles.Any, cultureInfo, out banknoteSelling);
                decimal.TryParse(node.SelectSingleNode("BanknoteBuying").InnerText, NumberStyles.Any, cultureInfo, out banknoteBuying);
                decimal.TryParse(node.SelectSingleNode("CrossRateUSD").InnerText, NumberStyles.Any, cultureInfo,  out crossRateUSD);
                decimal.TryParse(node.SelectSingleNode("CrossRateOther").InnerText, NumberStyles.Any, cultureInfo, out crossRateOther);
                int.TryParse(node.Attributes["CrossOrder"].Value, NumberStyles.Any, cultureInfo, out crossOrder);
              

                var currency = new Currency(crossOrder,code, currencyCode, unit
                    , name, currencyName, forexBuying, forexSelling, 
                    banknoteBuying, banknoteSelling,crossRateUSD,crossRateOther);

                sortedDictionary.Add(code, currency);
            }

            return sortedDictionary;
        }
    }
}
