using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visa.Domain;
using Visa.Service;

namespace Visa.Web.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        [Route("{region}/Service")]
        public ActionResult Index(string region)
        {
            vArticle model = ArticleService.GetModel(region, 2);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
    }
}