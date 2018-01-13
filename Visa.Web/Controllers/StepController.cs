using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visa.Domain;
using Visa.Service;

namespace Visa.Web.Controllers
{
    public class StepController : Controller
    {
        // GET: Step
        public ActionResult Index()
        {
            return View();
        }
        [Route("{region}/Step/Know_Your_Visa_Type")]
        public ActionResult Know_Your_Visa_Type(string region)
        {
            HowToApply model = HowService.GetModel(region, 1);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [Route("{region}/Step/How_To_Apply")]
        public ActionResult How_To_Apply(string region)
        {
            HowToApply model = HowService.GetModel(region, 2);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [Route("{region}/Step/After_Submission")]
        public ActionResult After_Submission(string region)
        {
            HowToApply model = HowService.GetModel(region, 3);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
    }
}