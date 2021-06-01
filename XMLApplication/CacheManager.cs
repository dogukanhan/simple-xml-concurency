using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace XMLApplication
{
    // CacheManager provides an interface for string data.xml and refleshing it if the data is old.
    class CacheManager
    {
        const string DOWNLOAD_URL = "https://www.tcmb.gov.tr/kurlar/today.xml";
        const string CACHE_FOLDER = "./";
        const string CACHE_FILE_NAME = ".cached.xml";

        // Reflesh cache if the data is not valid or user requested.
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

        // Load data from reading cache file.
        SortedDictionary<string, ICurrency> LoadFromCache()
        {
            return ConcurrencyParser.Parse(CachedFilePath());
        }

        // Load data retreives cache file it valid or refleshes cache and retrieves fresh data.
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

        // Cachedfilepath.
        string CachedFilePath()
        {
            return CACHE_FOLDER + CACHE_FILE_NAME;
        }

    }
}
