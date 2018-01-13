using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visa.Domain;
using Visa.Service;

namespace Visa.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Route("{region}/Home")]
        public ActionResult Index(string region)
        {

            List<KeyValuePair<string, string>> lsmenu = Visa.Service.TypeService.GetVisaList(region);
           
            ViewData["vmenu"] = lsmenu;
            List<DownloadForm> lsd = DownloadServce.GetFormBy(region);
            ViewData["vform"] = lsd;
            return View();
        }
    }
}