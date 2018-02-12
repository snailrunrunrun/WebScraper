using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Quartz;
using WebScraper.ScraperApi.Models;
using Newtonsoft.Json;

namespace WebScraper.Helpers
{
    /// <inheritdoc />
    /// <summary>
    /// The scraping job is defined in this class.
    /// </summary>
    public class ScrapingJob : IJob
    {
        /// <summary>
        /// Keep a private in-memory index of the scraped results.
        /// </summary>
        private static ConcurrentDictionary<string, string> resultsDic = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Keep a private in-memory list of what jobs are currently on the schedule.
        /// </summary>
        private static ConcurrentDictionary<string, string> scheduledTasks = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Getter and setter for resultsDisc.
        /// </summary>
        public static ConcurrentDictionary<string, string> ResultsDic
        {
            get => resultsDic;
            set => resultsDic = value;
        }

        /// <summary>
        /// Getter and setter for scheduledTasks.
        /// </summary>
        public static ConcurrentDictionary<string, string> ScheduledTasks
        {
            get => scheduledTasks;
            set => scheduledTasks = value;
        }

        /// <inheritdoc />
        public Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var url = dataMap.GetString("url");
            var tag = dataMap.GetString("tag");
            var uri = dataMap.GetString("uri");
            var scrappedResults = ScrapForResults(url, tag);
            var fileGuidId = WriteToJson(scrappedResults);

            var uriTag = string.Concat(uri, "-", tag);
            ResultsDic.TryAdd(string.Concat(uriTag), fileGuidId);
            ScheduledTasks.TryRemove(uriTag, out _);

            return Task.Delay(0);
        }
        
        /// <summary>
        /// Serialize scraped results into Json and write to .json file.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static string WriteToJson(V1WikipicsUriTagGetOKResponseContent result)
        {
            var guidString = Guid.NewGuid().ToString();
            var filePath = FileHelper.GetFilePath(guidString);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(result));

            return guidString;
        }

        /// <summary>
        /// Scrape for provided uri and html tag.
        /// </summary>
        /// <param name="uri">Uri.</param>
        /// <param name="tag">Html tag.</param>
        /// <returns>Response content.</returns>
        public static V1WikipicsUriTagGetOKResponseContent ScrapForResults(string uri, string tag)
        {
            var web = new HtmlWeb();
            var document = web.Load(uri);
            
            var anchorLinks = document.DocumentNode.SelectNodes("//"+tag).Where(node => node.InnerHtml.Contains(".jpg"));
            var result = new V1WikipicsUriTagGetOKResponseContent {Images = new List<string>()};

            foreach (var item in anchorLinks)
            {
                var src = item.FirstChild.Attributes["src"].Value;
                result.Images.Add(@"https:" + src);
            }

            var title = document.DocumentNode.SelectNodes("//h1").FirstOrDefault();
            result.Title = title?.InnerHtml;
            result.DatetimeUTC = DateTime.UtcNow.ToString("o");

            return result;
        }
    }
}