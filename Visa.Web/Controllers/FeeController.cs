using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visa.Domain;
using Visa.Service;

namespace Visa.Web.Controllers
{
    public class FeeController : Controller
    {
        // GET: Fee
        [Route("{region}/Fee")]
        public ActionResult Index(string region)
        {
            vArticle model = ArticleService.GetModel(region,7);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
    }
}