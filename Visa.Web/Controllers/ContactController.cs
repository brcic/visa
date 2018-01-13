using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Visa.Web.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [Route("{region}/Contact")]
        public ActionResult Index(string region)
        {
            return View();
        }
    }
}