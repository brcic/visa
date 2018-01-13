using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visa.Domain;
using Visa.Service;

namespace Visa.Web.Controllers
{
    public class HolidayController : Controller
    {
        // GET: Holiday
        [Route("{region}/Holiday")]
        public ActionResult Index(string region)
        {
            vArticle model = ArticleService.GetModel(region, 5);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
    }
}