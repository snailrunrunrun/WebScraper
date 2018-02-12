// Template: Controller Interface (ApiControllerInterface.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebScraper.ScraperApi.Models;


namespace WebScraper.ScraperApi
{
    public interface IV1WikipicsUriTagController
    {

        IHttpActionResult Get([FromUri] string uri,[FromUri] string tag);
    }
}
