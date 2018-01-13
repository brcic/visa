using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visa.Domain;
using Visa.Service;

namespace Visa.Web.Controllers
{
    public class FaqController : Controller
    {
        // GET: Faq
        [Route("{region}/Faq")]
        public ActionResult Index(string region)
        {
            vArticle model = ArticleService.GetModel(region, 8);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
    }
}