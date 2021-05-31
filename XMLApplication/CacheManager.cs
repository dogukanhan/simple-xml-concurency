using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace XMLApplication
{
    class CacheManager
    {
        const string DOWNLOAD_URL = "https://www.tcmb.gov.tr/kurlar/today.xml";
        const string CACHE_FOLDER = "./";
        const string CACHE_FILE_NAME = ".cached.xml";
        public SortedDictionary<string, ICurrency> RefleshCache()
        {
            Console.WriteLine("Reflesh Cache");
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                string response = client.DownloadString(DOWNLOAD_URL);
                File.WriteAllText(CachedFilePath(), response, System.Text.Encoding.UTF8);
                Console.WriteLine("Completed");
                return LoadFromCache();
            }
        }

        SortedDictionary<string, ICurrency> LoadFromCache()
        {
            return ConcurrencyParser.Parse(CachedFilePath());
        }
        public SortedDictionary<string, ICurrency> LoadData()
        {
            if (File.Exists(CachedFilePath()))
            {

                DateTime dateTimeNow = DateTime.Now;
                DateTime lastWriteDateTime = File.GetLastWriteTime(CachedFilePath());

                if (dateTimeNow.Date == lastWriteDateTime.Date)
                {

                    // Same day but cache is before the update time
                    if (lastWriteDateTime.Hour < 15
                      && (dateTimeNow.Hour >= 15 && dateTimeNow.Millisecond >= 30))
                    {
                        return RefleshCache();
                    }
                    // Same day but cache is before the update time
                    else if ((lastWriteDateTime.Hour == 15 && lastWriteDateTime.Minute < 30)
                    && (dateTimeNow.Hour >= 15 && dateTimeNow.Millisecond >= 30))
                    {
                        return RefleshCache();
                    }else{
                        return LoadFromCache();
                    }
                }
                else
                {
                    // Different Days
                    return RefleshCache();
                }
            }
            else
            { // no cache file;
                return RefleshCache();
            }
        }

        string CachedFilePath()
        {
            return CACHE_FOLDER + CACHE_FILE_NAME;
        }

    }
}
