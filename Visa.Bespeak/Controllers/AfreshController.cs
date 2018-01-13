using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Visa.Domain;
using Visa.Service;

namespace Visa.Bespeak.Controllers
{
    public class AfreshController : Controller
    {
        // GET: Afresh
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Find()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Find(FormCollection fc)
        {
            string number = fc["number"];
            string passport = fc["passport"];
            string email = fc["email"];
            string phone = fc["phone"];
            List<Expression<Func<ApplyView, bool>>> listexp = new List<Expression<Func<ApplyView, bool>>>();
            if (number != "")
                listexp.Add(q => q.ApplyNumber.Contains(number));
            if (passport != "")
                listexp.Add(q => q.Passport.Contains(passport));
            if (email != "")
                listexp.Add(q => q.Email.Contains(email));
            if (phone != "")
                listexp.Add(q => q.Cellphone.Contains(phone));
            listexp.Add(q => q.ApStatus==0);
            List<ApplyView> lsitems = ApplyService.GetApplyViewList(listexp);
            if (lsitems.Count <= 0)
            {
                return Json(new { status = "2", message = "no" });
            }
            DateTime datezx = lsitems[0].AddTime;
            if ((DateTime.Now - datezx).TotalHours < 24)
                return Json(new { status = "3", message = "no" });

            return Json(new { status = "1", message = "ok" });
        }
        public ActionResult List()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            List<ApplyList> lsitems = ApplyService.GetApplyList(model.Id);
            return View(lsitems);
        }
    }
}