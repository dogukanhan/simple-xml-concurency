using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace XMLApplication
{
    /// <summary>
    /// CacheManager provides an interface for string data.xml and refleshing it if the data is old.
    /// </summary>
    class CacheManager
    {

        /// <summary>
        /// Download URL for xml data.
        /// </summary>
        const string DOWNLOAD_URL = "https://www.tcmb.gov.tr/kurlar/today.xml";

        /// <summary>
        /// Cache folder will be used to store xml
        /// </summary>
        const string CACHE_FOLDER = "./";

        /// <summary>
        /// Cache file name wil be used to store xml
        /// </summary>
        const string CACHE_FILE_NAME = ".cached.xml";

        /// <summary>
        /// Reflesh cache if the data is not valid or user requested.
        /// </summary>
        /// <returns>Fresh data parsed with using <see cref="ConcurrencyParser"/> class</returns>
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

        /// <summary>
        /// Load data from reading cache file
        /// </summary>
        /// <returns>Parsed xml data from cache file.</returns>
        SortedDictionary<string, ICurrency> LoadFromCache()
        {
            return ConcurrencyParser.Parse(CachedFilePath());
        }

        /// <summary>
        /// Load data retreives cache file it valid or refleshes cache and retrieves fresh data.
        /// </summary>
        /// <returns></returns>
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
