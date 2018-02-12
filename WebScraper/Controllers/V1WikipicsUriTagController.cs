using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebScraper.Helpers;
using WebScraper.ScraperApi.Models;

namespace WebScraper.ScraperApi
{
    public partial class V1WikipicsUriTagController : IV1WikipicsUriTagController
    {
        /// <summary>
        /// Root directory where to store scraped results in .json format.
        /// </summary>
        private const string _rootDir = @"C:\_toDelete";

        /// <summary>
        /// Initializes an instance of <see cref="V1WikipicsUriTagController"/>.
        /// </summary>
        public V1WikipicsUriTagController()
        {
            if (!Directory.Exists(_rootDir))
            {
                Directory.CreateDirectory(_rootDir);
            }
        }

        /// <summary>
		/// Scrape provided web url and html tag and return resutls. - /v1/wikipics/{uri}/{tag}
		/// </summary>
		/// <param name="uri">Wiki url.</param>
		/// <param name="tag">A valid html tag (case-insensative).</param>
		/// <returns>MultipleV1WikipicsUriTagGet</returns>
        public IHttpActionResult Get([FromUri] string uri,[FromUri] string tag)
        {
            var result = new MultipleV1WikipicsUriTagGet();
            var wikiUrl = UrlHelper.GetWikiUrl(uri);

            // hash uri + tag
            var uriTag = string.Concat(uri, "-", tag);

            if (ScrappingJob.ScheduledTasks.ContainsKey(uriTag))
            {
                return Content(HttpStatusCode.Accepted, new V1WikipicsUriTagGetAcceptedResponseContent
                {
                    Status = "Results not available yet. Please try again later."
                });
            }

            if (ScrappingJob.ResultsDic.ContainsKey(uriTag))
            {
                var fileGuid = ScrappingJob.ResultsDic[uriTag];
                var filePath = FileHelper.GetFilePath(fileGuid);
                var responseContent = FileHelper.FetchResultsFromFile(filePath);
                result.V1WikipicsUriTagGetOKResponseContent = responseContent;
                return Ok(result.V1WikipicsUriTagGetOKResponseContent);
            }
            
            if (UrlHelper.IsValidUrl(wikiUrl))
            {
                ScrappingJob.ScheduledTasks.TryAdd(uriTag, "");

                Task.Run( () => ScheduleScrapingTaskAsync(wikiUrl, uri, tag) );
                
                return Content(HttpStatusCode.Accepted, new V1WikipicsUriTagGetAcceptedResponseContent
                {
                    Status = "A new scrapping task is scheduled."
                });
            }
            
            return Content(HttpStatusCode.BadRequest, new V1WikipicsUriTagGetBadRequestResponseContent
            {
                Status = "Bad url."
            });
        }

        /// <summary>
        /// Schedule a scrapping job.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="uri"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static async Task ScheduleScrapingTaskAsync(string url, string uri, string tag)
        {
            // construct a scheduler factory
            var props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            var schedFact = new StdSchedulerFactory(props);

            // get a scheduler
            var sched = await schedFact.GetScheduler();
            await sched.Start();

            // define the job and tie it to our ScrappingJob class
            var job = JobBuilder.Create<ScrappingJob>()
                .WithIdentity("myJob", "group1")
                .UsingJobData("url", url)
                .UsingJobData("tag", tag)
                .UsingJobData("uri", uri)
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .WithRepeatCount(0)
                    )
                .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}
