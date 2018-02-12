using System.IO;
using Newtonsoft.Json;
using WebScraper.ScraperApi.Models;

namespace WebScraper.Helpers
{
    /// <summary>
    /// Helper methods to facilitate file IO operations.
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Cancatenate root and file name and return the full file path.
        /// </summary>
        /// <param name="guidString">String value of guid.</param>
        /// <returns>File path.</returns>
        public static string GetFilePath(string guidString)
        {
            var filePath = Path.Combine(@"C:\_toDelete", guidString + ".json");
            return filePath;
        }

        /// <summary>
        /// Deserialize directly from .json file.
        /// </summary>
        /// <param name="filePath">Full json file path.</param>
        /// <returns>Response content.</returns>
        public static V1WikipicsUriTagGetOKResponseContent FetchResultsFromFile(string filePath)
        {
            using (var file = File.OpenText(filePath))
            {
                var serializer = new JsonSerializer();
                return (V1WikipicsUriTagGetOKResponseContent)serializer.Deserialize(file, typeof(V1WikipicsUriTagGetOKResponseContent));
            }
        }
    }
}