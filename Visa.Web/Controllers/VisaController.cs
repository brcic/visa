using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visa.Domain;
using Visa.Service;

namespace Visa.Web.Controllers
{
    public class VisaController : Controller
    {
        // GET: Visa
        [Route("{region}/Visa/{types}")]
        public ActionResult Index(string region, string types)
        {
            VisaType model = TypeService.GetModel(region, types.Replace("-"," "));
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
    }
}